using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiamSat.Scada
{
    public partial class frmSettings : Form
    {
        public string ConfigName { get; set; } = string.Empty;

        List<Configs> _configs = new List<Configs>();
        Configs _configItem;

        string _fileName;
        string _filePath;

        bool _isUpdate = true;

        public frmSettings()
        {
            InitializeComponent();

            _btnSave.Click += (s, o) =>
            {
                try
                {
                    if (string.IsNullOrEmpty(_txtProfileName.Text))
                    {
                        MessageBox.Show("Vui lòng nhập tên cấu hình.", "CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!_isUpdate && _configs.FirstOrDefault(x => x.ConfigName == _txtProfileName.Text) != null)
                    {
                        MessageBox.Show("Tên cấu hình đã tồn tại.", "CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!_isUpdate)
                    {
                        _configs.Add(_configItem);
                    }

                    string defaultJson = JsonConvert.SerializeObject(_configs, Formatting.Indented);
                    File.WriteAllText(_filePath, defaultJson);
                    MessageBox.Show($"LƯU CÀI ĐẶT CHO {_configItem.ConfigName} THÀNH CÔNG.");

                    // Read the JSON file
                    GlobalVariable.InvokeIfRequired(this, () =>
                    {
                        // Clear the existing items in the ComboBox
                        _cbSelectConfig.Items.Clear();

                        // Add the updated items to the ComboBox
                        foreach (var item in _configs)
                        {
                            _cbSelectConfig.Items.Add(item.ConfigName);
                        }
                    });

                    //this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            _btnChangePass.Click += (s, o) =>
            {
                using (var nf = new frmChangePass())
                {
                    nf.ShowDialog();
                }
            };

            _btnAddNewConfig.Click += (s, o) =>
            {
                _configItem = new Configs();

                //var c = _configs.FirstOrDefault();

                //_configItem.Config = c.Config;

                _isUpdate = false;

                ShowControl(_configItem);
            };

            _btnDelete.Click += (s, o) =>
            {
                if (_configItem == null)
                {
                    MessageBox.Show("Vui lòng chọn cấu hình cần xóa.", "CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa cầu hỉnh {_configItem.ConfigName}?", "XÁC NHẬN", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                _configs.Remove(_configItem);

                string defaultJson = JsonConvert.SerializeObject(_configs, Formatting.Indented);
                File.WriteAllText(_filePath, defaultJson);
                MessageBox.Show("LƯU CÀI ĐẶT THÀNH CÔNG.");

                FrmSettings_Load(null, null);
            };

            #region Đăng ký các sự kiện của các controls để cập nhật các giá trị cài đặt mới.
            #region Arow    
            #region CHỌN SENSOR SO SÁNH PHÂN ZONE
            _checkBoxArrowSensor1.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                var isExisst = _configItem.Config.ArrowSettings?.Sensors?.FirstOrDefault(x => x == EnumSensor.SENSOR_1);

                if (ck.Checked)
                {
                    if (isExisst == null || isExisst == 0) _configItem.Config.ArrowSettings?.Sensors.Add(EnumSensor.SENSOR_1);

                    //kiểm tra xem ở bên chọn sensor phân zone theo điều kiện có chọn sensor này thì xóa đi, và khóa control chọn sensor nay lại.
                    var check = _configItem.Config.ArrowSettings?.ArrowChooseSensorAddCompareSettings?.Sensors?
                                            .FirstOrDefault(x => x == EnumSensor.SENSOR_1);

                    if (check != null) _configItem.Config.ArrowSettings?.ArrowChooseSensorAddCompareSettings?.Sensors?.Remove(EnumSensor.SENSOR_1);

                    GlobalVariable.InvokeIfRequired(this, () =>
                    {
                        _checkBoxArrowSensor1Conditions.Enabled = false;
                        _checkBoxArrowSensor1Conditions.Checked = false;
                    });
                }
                else
                {
                    if (isExisst != null) _configItem.Config.ArrowSettings?.Sensors.Remove(EnumSensor.SENSOR_1);

                    //mở khóa control chọn sensor lại cho phần chọn sensor phân zone theo điều kiện.
                    GlobalVariable.InvokeIfRequired(this, () =>
                    {
                        _checkBoxArrowSensor1Conditions.Enabled = true;
                    });
                }
            };

            _checkBoxArrowSensor2.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                var isExisst = _configItem.Config.ArrowSettings?.Sensors?.FirstOrDefault(x => x == EnumSensor.SENSOR_2);

                if (ck.Checked)
                {
                    if (isExisst == null || isExisst == 0) _configItem.Config.ArrowSettings?.Sensors.Add(EnumSensor.SENSOR_2);

                    //kiểm tra xem ở bên chọn sensor phân zone theo điều kiện có chọn sensor này thì xóa đi, và khóa control chọn sensor nay lại.
                    var check = _configItem.Config.ArrowSettings?.ArrowChooseSensorAddCompareSettings?.Sensors?
                                            .FirstOrDefault(x => x == EnumSensor.SENSOR_2);

                    if (check != null) _configItem.Config.ArrowSettings?.ArrowChooseSensorAddCompareSettings?.Sensors?.Remove(EnumSensor.SENSOR_2);

                    GlobalVariable.InvokeIfRequired(this, () =>
                    {
                        _checkBoxArrowSensor2Conditions.Enabled = false;
                        _checkBoxArrowSensor2Conditions.Checked = false;
                    });
                }
                else
                {
                    if (isExisst != null) _configItem.Config.ArrowSettings?.Sensors.Remove(EnumSensor.SENSOR_2);

                    //mở khóa control chọn sensor lại cho phần chọn sensor phân zone theo điều kiện.
                    GlobalVariable.InvokeIfRequired(this, () =>
                    {
                        _checkBoxArrowSensor2Conditions.Enabled = true;
                    });
                }
            };

            _checkBoxArrowSensor3.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                var isExisst = _configItem.Config.ArrowSettings?.Sensors?.FirstOrDefault(x => x == EnumSensor.SENSOR_3);

                if (ck.Checked)
                {
                    if (isExisst == null || isExisst == 0) _configItem.Config.ArrowSettings?.Sensors.Add(EnumSensor.SENSOR_3);

                    //kiểm tra xem ở bên chọn sensor phân zone theo điều kiện có chọn sensor này thì xóa đi, và khóa control chọn sensor nay lại.
                    var check = _configItem.Config.ArrowSettings?.ArrowChooseSensorAddCompareSettings?.Sensors?
                                            .FirstOrDefault(x => x == EnumSensor.SENSOR_3);

                    if (check != null) _configItem.Config.ArrowSettings?.ArrowChooseSensorAddCompareSettings?.Sensors?.Remove(EnumSensor.SENSOR_3);

                    GlobalVariable.InvokeIfRequired(this, () =>
                    {
                        _checkBoxArrowSensor3Conditions.Enabled = false;
                        _checkBoxArrowSensor3Conditions.Checked = false;
                    });
                }
                else
                {
                    if (isExisst != null) _configItem.Config.ArrowSettings?.Sensors.Remove(EnumSensor.SENSOR_3);

                    //mở khóa control chọn sensor lại cho phần chọn sensor phân zone theo điều kiện.
                    GlobalVariable.InvokeIfRequired(this, () =>
                    {
                        _checkBoxArrowSensor3Conditions.Enabled = true;
                    });
                }
            };

            #region Data lớn nhất nhỏ nhất
            _checkBoxArrowDataMax.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                if (ck.Checked)
                {
                    _configItem.Config.ArrowSettings.DataMax = true;

                    GlobalVariable.InvokeIfRequired(this, () =>
                    {
                        _checkBoxArrowDataMin.Checked = false;
                    });
                }
            };
            _checkBoxArrowDataMin.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                if (ck.Checked)
                {
                    _configItem.Config.ArrowSettings.DataMax = false;

                    GlobalVariable.InvokeIfRequired(this, () =>
                    {
                        _checkBoxArrowDataMax.Checked = false;
                    });
                }
            };
            #endregion
            #endregion

            #region CHỌN SENSOR SO SÁNH PHÂN ZONE CÓ ĐIỀU KIỆN
            _checkBoxArrowSensor1Conditions.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                var isExist = _configItem.Config.ArrowSettings?.ArrowChooseSensorAddCompareSettings.Sensors.FirstOrDefault(x => x == EnumSensor.SENSOR_1);

                if (ck.Checked)
                {
                    if (isExist == null || isExist == 0) _configItem.Config.ArrowSettings?.ArrowChooseSensorAddCompareSettings?.Sensors?.Add(EnumSensor.SENSOR_1);
                }
                else
                {
                    if (isExist != null) _configItem.Config.ArrowSettings?.ArrowChooseSensorAddCompareSettings.Sensors?.Remove(EnumSensor.SENSOR_1);
                }
            };

            _checkBoxArrowSensor2Conditions.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                var isExist = _configItem.Config.ArrowSettings?.ArrowChooseSensorAddCompareSettings.Sensors.FirstOrDefault(x => x == EnumSensor.SENSOR_2);

                if (ck.Checked)
                {
                    if (isExist == null || isExist == 0) _configItem.Config.ArrowSettings?.ArrowChooseSensorAddCompareSettings?.Sensors?.Add(EnumSensor.SENSOR_2);
                }
                else
                {
                    if (isExist != null) _configItem.Config.ArrowSettings?.ArrowChooseSensorAddCompareSettings.Sensors?.Remove(EnumSensor.SENSOR_2);
                }
            };

            _checkBoxArrowSensor3Conditions.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                var isExist = _configItem.Config.ArrowSettings?.ArrowChooseSensorAddCompareSettings.Sensors.FirstOrDefault(x => x == EnumSensor.SENSOR_3);

                if (ck.Checked)
                {
                    if (isExist == null || isExist == 0) _configItem.Config.ArrowSettings?.ArrowChooseSensorAddCompareSettings?.Sensors?.Add(EnumSensor.SENSOR_3);
                }
                else
                {
                    if (isExist != null) _configItem.Config.ArrowSettings?.ArrowChooseSensorAddCompareSettings.Sensors?.Remove(EnumSensor.SENSOR_3);
                }
            };

            _txtArrowConditionsValue.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.ArrowSettings.ArrowChooseSensorAddCompareSettings.ConditionsValue = double.TryParse(t.Text, out double value) ? value : 0;
            };
            #endregion

            #region Zones
            _txtFromV1.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.ArrowSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumArrowZoneName.V1).FromValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };
            _txtToV1.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.ArrowSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumArrowZoneName.V1).ToValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };

            _txtFromV3.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.ArrowSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumArrowZoneName.V3).FromValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };
            _txtToV3.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.ArrowSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumArrowZoneName.V3).ToValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };

            _txtFromV6.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.ArrowSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumArrowZoneName.V6).FromValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };
            _txtToV6.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.ArrowSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumArrowZoneName.V6).ToValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };

            _txtFromV9.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.ArrowSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumArrowZoneName.V9).FromValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };
            _txtToV9.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.ArrowSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumArrowZoneName.V9).ToValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };

            _txtFromVSS.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.ArrowSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumArrowZoneName.VSS).FromValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };
            _txtToVSS.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.ArrowSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumArrowZoneName.VSS).ToValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };
            #endregion

            #region PHÁN ĐỊNH ĐẦU THẲNG OK/NG
            _checkBoxArrowS3_S1.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                if (ck.Checked)
                {
                    _configItem.Config.ArrowSettings.S3_S1OrS1_S3 = true;

                    GlobalVariable.InvokeIfRequired(this, () =>
                    {
                        _checkBoxArrowS1_S3.Checked = false;
                    });
                }
            };
            _checkBoxArrowS1_S3.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                _configItem.Config.ArrowSettings.DataMax = ck.Checked;

                if (ck.Checked)
                {
                    _configItem.Config.ArrowSettings.S3_S1OrS1_S3 = false;

                    GlobalVariable.InvokeIfRequired(this, () =>
                    {
                        _checkBoxArrowS3_S1.Checked = false;
                    });
                }
            };

            _txtArrowCompareValue.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.ArrowSettings.CompareValue = double.TryParse(t.Text, out double value) ? value : 0;
            };
            #endregion
            #endregion

            #region APPLE    
            #region CHỌN SENSOR SO SÁNH PHÂN ZONE
            _checkBoxAppleSensor1.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                var isExisst = _configItem.Config.AppleSettings?.Sensors?.FirstOrDefault(x => x == EnumSensor.SENSOR_1);

                if (ck.Checked)
                {
                    if (isExisst == null || isExisst == 0) _configItem.Config.AppleSettings?.Sensors.Add(EnumSensor.SENSOR_1);
                }
                else
                {
                    if (isExisst != null) _configItem.Config.AppleSettings?.Sensors.Remove(EnumSensor.SENSOR_1);
                }
            };

            _checkBoxAppleSensor2.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                var isExisst = _configItem.Config.AppleSettings?.Sensors?.FirstOrDefault(x => x == EnumSensor.SENSOR_2);

                if (ck.Checked)
                {
                    if (isExisst == null || isExisst == 0) _configItem.Config.AppleSettings?.Sensors.Add(EnumSensor.SENSOR_2);
                }
                else
                {
                    if (isExisst != null) _configItem.Config.AppleSettings?.Sensors.Remove(EnumSensor.SENSOR_2);
                }
            };

            _checkBoxAppleSensor3.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                var isExisst = _configItem.Config.AppleSettings?.Sensors?.FirstOrDefault(x => x == EnumSensor.SENSOR_3);

                if (ck.Checked)
                {
                    if (isExisst == null || isExisst == 0) _configItem.Config.AppleSettings?.Sensors.Add(EnumSensor.SENSOR_3);
                }
                else
                {
                    if (isExisst != null) _configItem.Config.AppleSettings?.Sensors.Remove(EnumSensor.SENSOR_3);
                }
            };

            #region Data lớn nhất nhỏ nhất
            _checkBoxAppleDataMax.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                if (ck.Checked)
                {
                    _configItem.Config.AppleSettings.DataMax = true;

                    GlobalVariable.InvokeIfRequired(this, () =>
                    {
                        _checkBoxAppleDataMin.Checked = false;
                    });
                }
            };
            _checkBoxAppleDataMin.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                if (ck.Checked)
                {
                    _configItem.Config.AppleSettings.DataMax = false;

                    GlobalVariable.InvokeIfRequired(this, () =>
                    {
                        _checkBoxAppleDataMax.Checked = false;
                    });
                }
            };
            #endregion
            #endregion
            #region Zones
            _txtFromOK.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.AppleSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumApple_Ok_NG.OK).FromValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };
            _txtToOK.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.AppleSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumApple_Ok_NG.OK).ToValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };

            _txtFromNG.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.AppleSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumApple_Ok_NG.NG).FromValue
                                    = double.TryParse(t.Text, out double value) ? value : 0;
            };
            _txtToNG.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.AppleSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumApple_Ok_NG.NG).ToValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };
            #endregion
            #endregion

            #region Systems
            _txtProfileName.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.ConfigName = t.Text;
            };

            _txtDecimalNum.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.DecimalNum = int.TryParse(t.Text, out int value) ? value : 0;
            };

            _txtGain.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.Gain = double.TryParse(t.Text, out double value) ? value : 0;
            };

            _txtOffset.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.Offset = double.TryParse(t.Text, out double value) ? value : 0;
            };

            _txtUnit.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.Unit = t.Text;
            };

            _txtValueActive.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _configItem.Config.ValueActive = double.TryParse(t.Text, out double value) ? value : 0;
            };

            _checkBoxOnOffCheckHeadStraight.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                _configItem.Config.ActiveCheckHeadStraight = ck.Checked;
            };
            #endregion
            #endregion

            Load += FrmSettings_Load;
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            // Automatically use the same directory as the executable
            _fileName = "ConfigSystem.json";
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _fileName);

            // Check if file exists
            if (!File.Exists(_filePath))
            {
                Console.WriteLine("File does not exist. Creating default JSON file...");

                var arrowZone = new List<ArrowZone>();
                arrowZone.Add(new ArrowZone() { FromValue = 0, ToValue = 1, ZoneName = EnumArrowZoneName.V1 });
                arrowZone.Add(new ArrowZone() { FromValue = 1.1, ToValue = 2, ZoneName = EnumArrowZoneName.V3 });
                arrowZone.Add(new ArrowZone() { FromValue = 2.1, ToValue = 3, ZoneName = EnumArrowZoneName.V6 });
                arrowZone.Add(new ArrowZone() { FromValue = 3.1, ToValue = 5, ZoneName = EnumArrowZoneName.V9 });
                arrowZone.Add(new ArrowZone() { FromValue = 4.1, ToValue = 5, ZoneName = EnumArrowZoneName.VSS });

                var arrowSettings = new ArrowSettingsModel()
                {
                    Sensors = new List<EnumSensor>(),
                    DataMax = true,
                    Zones = arrowZone,
                    ArrowChooseSensorAddCompareSettings = new ArrowChooseSensorAddCompare() { Sensors = new List<EnumSensor>(), ConditionsValue = 5 },
                    S3_S1OrS1_S3 = true,
                    CompareValue = 1.1
                };

                var appleZone = new List<AppleZone>();
                appleZone.Add(new AppleZone() { FromValue = 0, ToValue = 1, ZoneName = EnumApple_Ok_NG.OK });
                appleZone.Add(new AppleZone() { FromValue = 1.1, ToValue = 50, ZoneName = EnumApple_Ok_NG.NG });

                var appleSettings = new AppleSettingsModel()
                {
                    Sensors = new List<EnumSensor>(),
                    DataMax = true,
                    Zones = appleZone
                };

                var configSystem = new ConfigModel()
                {
                    DecimalNum = 3,
                    TagPath = "Local Station/Channel1/Device",
                    Gain = 1,
                    Offset = 0,
                    Unit = "mil",
                    ValueActive = 600,
                    ActiveCheckHeadStraight = true,
                    ArrowSettings = arrowSettings,
                    AppleSettings = appleSettings
                };

                _configs.Add(new Configs()
                {
                    ConfigName = "Cấu hình 1",
                    Config = configSystem
                });

                string defaultJson = JsonConvert.SerializeObject(_configs, Formatting.Indented);
                File.WriteAllText(_filePath, defaultJson);
                Console.WriteLine("File created successfully.\n");
            }

            // Read the JSON file
            string jsonContent = File.ReadAllText(_filePath);
            _configs = JsonConvert.DeserializeObject<List<Configs>>(jsonContent);

            _cbSelectConfig.Items.Clear();
            foreach (var item in _configs)
            {
                _cbSelectConfig.Items.Add(item.ConfigName);
            }

            _cbSelectConfig.SelectedIndexChanged += (s, o) =>
            {
                ComboBox cb = (ComboBox)s;
                var configName = _cbSelectConfig.SelectedItem.ToString();

                _configItem = new Configs();
                _configItem = _configs.FirstOrDefault(x => x.ConfigName == configName);
                if (_configItem == null)
                {
                    MessageBox.Show("Không tìm thấy cấu hình này.", "CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                configName = _configItem.ConfigName;

                _isUpdate = true;

                ShowControl(_configItem);
            };
            //_cbSelectConfig.SelectedIndex = 0;
            _cbSelectConfig.SelectedItem = _configs.FirstOrDefault().ConfigName;

            //ShowControl(_configs.FirstOrDefault());
        }

        void ShowControl(Configs model)
        {
            GlobalVariable.InvokeIfRequired(this, () =>
            {
                _txtProfileName.Text = model.ConfigName;

                _txtDecimalNum.Text = model.Config.DecimalNum.ToString();
                _txtTagPath.Text = model.Config.TagPath;
                _txtUnit.Text = model.Config.Unit;
                _txtGain.Text = model.Config.Gain.ToString();
                _txtOffset.Text = model.Config.Offset.ToString();
                _txtValueActive.Text = model.Config.ValueActive.ToString();
                _checkBoxOnOffCheckHeadStraight.Checked = model.Config.ActiveCheckHeadStraight;

                #region Apple
                var sensorsAppleSensors = _configItem.Config.AppleSettings?.Sensors?.ToList();
                foreach (var item in sensorsAppleSensors)
                {
                    if (item == EnumSensor.SENSOR_1)
                        _checkBoxAppleSensor1.Checked = item == EnumSensor.SENSOR_1 ? true : false;
                    else if (item == EnumSensor.SENSOR_2)
                        _checkBoxAppleSensor2.Checked = item == EnumSensor.SENSOR_2 ? true : false;
                    else
                        _checkBoxAppleSensor3.Checked = item == EnumSensor.SENSOR_3 ? true : false;
                }

                if (model.Config.AppleSettings.DataMax)
                {
                    _checkBoxAppleDataMax.Checked = true;
                    _checkBoxAppleDataMin.Checked = false;
                }
                else
                {
                    _checkBoxAppleDataMax.Checked = false;
                    _checkBoxAppleDataMin.Checked = true;
                }

                foreach (var item in model.Config.AppleSettings.Zones)
                {
                    if (item.ZoneName == EnumApple_Ok_NG.OK)
                    {
                        _txtFromOK.Text = item.FromValue.ToString();
                        _txtToOK.Text = item.ToValue.ToString();
                    }
                    else
                    {
                        _txtFromNG.Text = item.FromValue.ToString();
                        _txtToNG.Text = item.ToValue.ToString();
                    }
                }
                #endregion

                #region Arrow
                #region chọn sensor so sánh phân zone
                var sensorsArrowSensors = model.Config.ArrowSettings?.Sensors?.ToList();
                foreach (var item in sensorsArrowSensors)
                {
                    if (item == EnumSensor.SENSOR_1)
                        _checkBoxArrowSensor1.Checked = item == EnumSensor.SENSOR_1 ? true : false;
                    else if (item == EnumSensor.SENSOR_2)
                        _checkBoxArrowSensor2.Checked = item == EnumSensor.SENSOR_2 ? true : false;
                    else
                        _checkBoxArrowSensor3.Checked = item == EnumSensor.SENSOR_3 ? true : false;
                }

                if (model.Config.ArrowSettings.DataMax)
                {
                    _checkBoxArrowDataMax.Checked = true;
                    _checkBoxArrowDataMin.Checked = false;
                }
                else
                {
                    _checkBoxArrowDataMax.Checked = false;
                    _checkBoxArrowDataMin.Checked = true;
                }
                #endregion
                #region ZONE
                foreach (var item in model.Config.ArrowSettings?.Zones)
                {
                    if (item.ZoneName == EnumArrowZoneName.V1)
                    {
                        _txtFromV1.Text = item.FromValue.ToString();
                        _txtToV1.Text = item.ToValue.ToString();
                    }
                    else if (item.ZoneName == EnumArrowZoneName.V3)
                    {
                        _txtFromV3.Text = item.FromValue.ToString();
                        _txtToV3.Text = item.ToValue.ToString();
                    }
                    else if (item.ZoneName == EnumArrowZoneName.V6)
                    {
                        _txtFromV6.Text = item.FromValue.ToString();
                        _txtToV6.Text = item.ToValue.ToString();
                    }
                    else if (item.ZoneName == EnumArrowZoneName.V9)
                    {
                        _txtFromV9.Text = item.FromValue.ToString();
                        _txtToV9.Text = item.ToValue.ToString();
                    }
                    else
                    {
                        _txtFromVSS.Text = item.FromValue.ToString();
                        _txtToVSS.Text = item.ToValue.ToString();
                    }
                }
                #endregion
                #region CHỌN SENSOR SO SÁNH PHÂN ZONE CÓ ĐIỀU KIỆN
                _txtArrowConditionsValue.Text = model.Config.ArrowSettings?.ArrowChooseSensorAddCompareSettings.ConditionsValue.ToString();

                var sensorsArrowConditionSensors = model.Config.ArrowSettings?.ArrowChooseSensorAddCompareSettings.Sensors?.ToList();
                foreach (var item in sensorsArrowConditionSensors)
                {
                    if (item == EnumSensor.SENSOR_1)
                        _checkBoxArrowSensor1Conditions.Checked = item == EnumSensor.SENSOR_1 ? true : false;
                    else if (item == EnumSensor.SENSOR_2)
                        _checkBoxArrowSensor2Conditions.Checked = item == EnumSensor.SENSOR_2 ? true : false;
                    else
                        _checkBoxArrowSensor3Conditions.Checked = item == EnumSensor.SENSOR_3 ? true : false;
                }
                #endregion

                #region Phân định đầu thẳng OK/NG
                if ((bool)(_configItem.Config.ArrowSettings?.S3_S1OrS1_S3))
                {
                    _checkBoxArrowS3_S1.Checked = true;
                    _checkBoxArrowS1_S3.Checked = false;
                }
                else
                {
                    _checkBoxArrowS3_S1.Checked = false;
                    _checkBoxArrowS1_S3.Checked = true;
                }

                _txtArrowCompareValue.Text = _configItem.Config.ArrowSettings?.CompareValue.ToString();
                #endregion
                #endregion
            });
        }
    }
}
