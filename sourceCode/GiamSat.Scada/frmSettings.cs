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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiamSat.Scada
{
    public partial class frmSettings : Form
    {
        private ConfigModel _model;
        string _fileName;
        string _filePath;

        public frmSettings()
        {
            InitializeComponent();

            _btnSave.Click += (s, o) =>
            {
                try
                {
                    string defaultJson = JsonConvert.SerializeObject(_model, Formatting.Indented);
                    File.WriteAllText(_filePath, defaultJson);
                    MessageBox.Show("LƯU CÀI ĐẶT THÀNH CÔNG.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            #region Đăng ký các sự kiện của các controls để cập nhật các giá trị cài đặt mới.
            #region Arow    
            #region CHỌN SENSOR SO SÁNH PHÂN ZONE
            _checkBoxArrowSensor1.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                var isExisst = _model.ArrowSettings?.Sensors?.FirstOrDefault(x => x == EnumSensor.SENSOR_1);

                if (ck.Checked)
                {
                    if (isExisst == null || isExisst == 0) _model.ArrowSettings?.Sensors.Add(EnumSensor.SENSOR_1);

                    //kiểm tra xem ở bên chọn sensor phân zone theo điều kiện có chọn sensor này thì xóa đi, và khóa control chọn sensor nay lại.
                    var check = _model.ArrowSettings?.ArrowChooseSensorAddCompareSettings?.Sensors?
                                            .FirstOrDefault(x => x == EnumSensor.SENSOR_1);

                    if (check != null) _model.ArrowSettings?.ArrowChooseSensorAddCompareSettings?.Sensors?.Remove(EnumSensor.SENSOR_1);

                    GlobalVariable.InvokeIfRequired(this, () =>
                    {
                        _checkBoxArrowSensor1Conditions.Enabled = false;
                        _checkBoxArrowSensor1Conditions.Checked = false;
                    });
                }
                else
                {
                    if (isExisst != null) _model.ArrowSettings?.Sensors.Remove(EnumSensor.SENSOR_1);

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

                var isExisst = _model.ArrowSettings?.Sensors?.FirstOrDefault(x => x == EnumSensor.SENSOR_2);

                if (ck.Checked)
                {
                    if (isExisst == null || isExisst == 0) _model.ArrowSettings?.Sensors.Add(EnumSensor.SENSOR_2);

                    //kiểm tra xem ở bên chọn sensor phân zone theo điều kiện có chọn sensor này thì xóa đi, và khóa control chọn sensor nay lại.
                    var check = _model.ArrowSettings?.ArrowChooseSensorAddCompareSettings?.Sensors?
                                            .FirstOrDefault(x => x == EnumSensor.SENSOR_2);

                    if (check != null) _model.ArrowSettings?.ArrowChooseSensorAddCompareSettings?.Sensors?.Remove(EnumSensor.SENSOR_2);

                    GlobalVariable.InvokeIfRequired(this, () =>
                    {
                        _checkBoxArrowSensor2Conditions.Enabled = false;
                        _checkBoxArrowSensor2Conditions.Checked = false;
                    });
                }
                else
                {
                    if (isExisst != null) _model.ArrowSettings?.Sensors.Remove(EnumSensor.SENSOR_2);

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

                var isExisst = _model.ArrowSettings?.Sensors?.FirstOrDefault(x => x == EnumSensor.SENSOR_3);

                if (ck.Checked)
                {
                    if (isExisst == null || isExisst == 0) _model.ArrowSettings?.Sensors.Add(EnumSensor.SENSOR_3);

                    //kiểm tra xem ở bên chọn sensor phân zone theo điều kiện có chọn sensor này thì xóa đi, và khóa control chọn sensor nay lại.
                    var check = _model.ArrowSettings?.ArrowChooseSensorAddCompareSettings?.Sensors?
                                            .FirstOrDefault(x => x == EnumSensor.SENSOR_3);

                    if (check != null) _model.ArrowSettings?.ArrowChooseSensorAddCompareSettings?.Sensors?.Remove(EnumSensor.SENSOR_3);

                    GlobalVariable.InvokeIfRequired(this, () =>
                    {
                        _checkBoxArrowSensor3Conditions.Enabled = false;
                        _checkBoxArrowSensor3Conditions.Checked = false;
                    });
                }
                else
                {
                    if (isExisst != null) _model.ArrowSettings?.Sensors.Remove(EnumSensor.SENSOR_3);

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
                    _model.ArrowSettings.DataMax = true;

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
                    _model.ArrowSettings.DataMax = false;

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

                var isExist = _model.ArrowSettings?.ArrowChooseSensorAddCompareSettings.Sensors.FirstOrDefault(x => x == EnumSensor.SENSOR_1);

                if (ck.Checked)
                {
                    if (isExist == null || isExist == 0) _model.ArrowSettings?.ArrowChooseSensorAddCompareSettings?.Sensors?.Add(EnumSensor.SENSOR_1);
                }
                else
                {
                    if (isExist != null) _model.ArrowSettings?.ArrowChooseSensorAddCompareSettings.Sensors?.Remove(EnumSensor.SENSOR_1);
                }
            };

            _checkBoxArrowSensor2Conditions.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                var isExist = _model.ArrowSettings?.ArrowChooseSensorAddCompareSettings.Sensors.FirstOrDefault(x => x == EnumSensor.SENSOR_2);

                if (ck.Checked)
                {
                    if (isExist == null || isExist == 0) _model.ArrowSettings?.ArrowChooseSensorAddCompareSettings?.Sensors?.Add(EnumSensor.SENSOR_2);
                }
                else
                {
                    if (isExist != null) _model.ArrowSettings?.ArrowChooseSensorAddCompareSettings.Sensors?.Remove(EnumSensor.SENSOR_2);
                }
            };

            _checkBoxArrowSensor3Conditions.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                var isExist = _model.ArrowSettings?.ArrowChooseSensorAddCompareSettings.Sensors.FirstOrDefault(x => x == EnumSensor.SENSOR_3);

                if (ck.Checked)
                {
                    if (isExist == null || isExist == 0) _model.ArrowSettings?.ArrowChooseSensorAddCompareSettings?.Sensors?.Add(EnumSensor.SENSOR_3);
                }
                else
                {
                    if (isExist != null) _model.ArrowSettings?.ArrowChooseSensorAddCompareSettings.Sensors?.Remove(EnumSensor.SENSOR_3);
                }
            };

            _txtArrowConditionsValue.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _model.ArrowSettings.ArrowChooseSensorAddCompareSettings.ConditionsValue = double.TryParse(t.Text, out double value) ? value : 0;
            };
            #endregion

            #region Zones
            _txtFromV1.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _model.ArrowSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumArrowZoneName.V1).FromValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };
            _txtToV1.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _model.ArrowSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumArrowZoneName.V1).ToValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };

            _txtFromV3.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _model.ArrowSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumArrowZoneName.V3).FromValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };
            _txtToV3.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _model.ArrowSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumArrowZoneName.V3).ToValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };

            _txtFromV6.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _model.ArrowSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumArrowZoneName.V6).FromValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };
            _txtToV6.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _model.ArrowSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumArrowZoneName.V6).ToValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };

            _txtFromV9.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _model.ArrowSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumArrowZoneName.V9).FromValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };
            _txtToV9.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _model.ArrowSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumArrowZoneName.V9).ToValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };

            _txtFromVSS.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _model.ArrowSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumArrowZoneName.VSS).FromValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };
            _txtToVSS.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _model.ArrowSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumArrowZoneName.VSS).ToValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };
            #endregion

            #region PHÁN ĐỊNH ĐẦU THẲNG OK/NG
            _checkBoxArrowS3_S1.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                if (ck.Checked)
                {
                    _model.ArrowSettings.S3_S1OrS1_S3 = true;

                    GlobalVariable.InvokeIfRequired(this, () =>
                    {
                        _checkBoxArrowS1_S3.Checked = false;
                    });
                }
            };
            _checkBoxArrowS1_S3.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                _model.ArrowSettings.DataMax = ck.Checked;

                if (ck.Checked)
                {
                    _model.ArrowSettings.S3_S1OrS1_S3 = false;

                    GlobalVariable.InvokeIfRequired(this, () =>
                    {
                        _checkBoxArrowS3_S1.Checked = false;
                    });
                }
            };

            _txtArrowCompareValue.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _model.ArrowSettings.CompareValue = double.TryParse(t.Text, out double value) ? value : 0;
            };
            #endregion
            #endregion

            #region APPLE    
            #region CHỌN SENSOR SO SÁNH PHÂN ZONE
            _checkBoxAppleSensor1.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                var isExisst = _model.AppleSettings?.Sensors?.FirstOrDefault(x => x == EnumSensor.SENSOR_1);

                if (ck.Checked)
                {
                    if (isExisst == null || isExisst == 0) _model.AppleSettings?.Sensors.Add(EnumSensor.SENSOR_1);
                }
                else
                {
                    if (isExisst != null) _model.AppleSettings?.Sensors.Remove(EnumSensor.SENSOR_1);
                }
            };

            _checkBoxAppleSensor2.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                var isExisst = _model.AppleSettings?.Sensors?.FirstOrDefault(x => x == EnumSensor.SENSOR_2);

                if (ck.Checked)
                {
                    if (isExisst == null || isExisst == 0) _model.AppleSettings?.Sensors.Add(EnumSensor.SENSOR_2);
                }
                else
                {
                    if (isExisst != null) _model.AppleSettings?.Sensors.Remove(EnumSensor.SENSOR_2);
                }
            };

            _checkBoxAppleSensor3.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                var isExisst = _model.AppleSettings?.Sensors?.FirstOrDefault(x => x == EnumSensor.SENSOR_3);

                if (ck.Checked)
                {
                    if (isExisst == null || isExisst == 0) _model.AppleSettings?.Sensors.Add(EnumSensor.SENSOR_3);
                }
                else
                {
                    if (isExisst != null) _model.AppleSettings?.Sensors.Remove(EnumSensor.SENSOR_3);
                }
            };

            #region Data lớn nhất nhỏ nhất
            _checkBoxAppleDataMax.CheckedChanged += (s, o) =>
            {
                CheckBox ck = (CheckBox)s;

                if (ck.Checked)
                {
                    _model.AppleSettings.DataMax = true;

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
                    _model.AppleSettings.DataMax = false;

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
                _model.AppleSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumApple_Ok_NG.OK).FromValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };
            _txtToOK.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _model.AppleSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumApple_Ok_NG.OK).ToValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };

            _txtFromNG.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _model.AppleSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumApple_Ok_NG.NG).FromValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };
            _txtToNG.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _model.AppleSettings.Zones.FirstOrDefault(x => x.ZoneName == EnumApple_Ok_NG.NG).ToValue
                                        = double.TryParse(t.Text, out double value) ? value : 0;
            };
            #endregion
            #endregion

            _txtDecimalNum.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _model.DecimalNum = int.TryParse(t.Text, out int value) ? value : 0;
            };

            _txtGain.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _model.Gain = double.TryParse(t.Text, out double value) ? value : 0;
            };

            _txtOffset.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _model.Offset = double.TryParse(t.Text, out double value) ? value : 0;
            };

            _txtUnit.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;
                _model.Unit = t.Text;
            };
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
                    ArrowSettings = arrowSettings,
                    AppleSettings = appleSettings
                };

                string defaultJson = JsonConvert.SerializeObject(configSystem, Formatting.Indented);
                File.WriteAllText(_filePath, defaultJson);
                Console.WriteLine("File created successfully.\n");
            }

            // Read the JSON file
            string jsonContent = File.ReadAllText(_filePath);
            _model = JsonConvert.DeserializeObject<ConfigModel>(jsonContent);

            ShowControl();
        }

        void ShowControl()
        {
            GlobalVariable.InvokeIfRequired(this, () =>
            {
                _txtDecimalNum.Text = _model.DecimalNum.ToString();
                _txtTagPath.Text = _model.TagPath;
                _txtUnit.Text = _model.Unit;
                _txtGain.Text = _model.Gain.ToString();
                _txtOffset.Text = _model.Offset.ToString();

                #region Apple
                var sensorsAppleSensors = _model.AppleSettings?.Sensors?.ToList();
                foreach (var item in sensorsAppleSensors)
                {
                    if (item == EnumSensor.SENSOR_1)
                        _checkBoxAppleSensor1.Checked = item == EnumSensor.SENSOR_1 ? true : false;
                    else if (item == EnumSensor.SENSOR_2)
                        _checkBoxAppleSensor2.Checked = item == EnumSensor.SENSOR_2 ? true : false;
                    else
                        _checkBoxAppleSensor3.Checked = item == EnumSensor.SENSOR_3 ? true : false;
                }

                if (_model.AppleSettings.DataMax)
                {
                    _checkBoxAppleDataMax.Checked = true;
                    _checkBoxAppleDataMin.Checked = false;
                }
                else
                {
                    _checkBoxAppleDataMax.Checked = false;
                    _checkBoxAppleDataMin.Checked = true;
                }

                foreach (var item in _model.AppleSettings.Zones)
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
                var sensorsArrowSensors = _model.ArrowSettings?.Sensors?.ToList();
                foreach (var item in sensorsArrowSensors)
                {
                    if (item == EnumSensor.SENSOR_1)
                        _checkBoxArrowSensor1.Checked = item == EnumSensor.SENSOR_1 ? true : false;
                    else if (item == EnumSensor.SENSOR_2)
                        _checkBoxArrowSensor2.Checked = item == EnumSensor.SENSOR_2 ? true : false;
                    else
                        _checkBoxArrowSensor3.Checked = item == EnumSensor.SENSOR_3 ? true : false;
                }

                if (_model.ArrowSettings.DataMax)
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
                foreach (var item in _model.ArrowSettings?.Zones)
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
                _txtArrowConditionsValue.Text = _model.ArrowSettings?.ArrowChooseSensorAddCompareSettings.ConditionsValue.ToString();

                var sensorsArrowConditionSensors = _model.ArrowSettings?.ArrowChooseSensorAddCompareSettings.Sensors?.ToList();
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
                if ((bool)(_model.ArrowSettings?.S3_S1OrS1_S3))
                {
                    _checkBoxArrowS3_S1.Checked = true;
                    _checkBoxArrowS1_S3.Checked = false;
                }
                else
                {
                    _checkBoxArrowS3_S1.Checked = false;
                    _checkBoxArrowS1_S3.Checked = true;
                }

                _txtArrowCompareValue.Text = _model.ArrowSettings?.CompareValue.ToString();
                #endregion
                #endregion
            });
        }
    }
}
