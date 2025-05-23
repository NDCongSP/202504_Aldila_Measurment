﻿using Dapper;
using EasyScada.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Serilog;
using System.IO;
using EasyScada.Winforms.Controls;
using System.Runtime.InteropServices;


namespace GiamSat.Scada
{
    public partial class Form1 : Form
    {
        private EasyDriverConnector _easyDriverConnector;

        private Timer _timer = new Timer();

        private bool _isLoaded = false;
        private bool _isReOpenApp = false;//biến dùng để báo ap bị tắt mở lại, vào lấy lại ZIndex cũ trước khi tắt phần mềm để log profile tiếp

        private string _formActive = "ARROW";

        List<Configs> _configs = new List<Configs>();
        Configs _configItem;

        string _fileName;
        string _filePath;

        private double _valueSensor1Old = 0, _valueSensor2Old = 0, _valueSensor3Old = 0;
        private double _valueSensor1 = 0, _valueSensor2 = 0, _valueSensor3 = 0;

        private bool _newTransaction = false;//biến để check mỗi lần giá trị đo từ 0 thay đổi, thì kích hoặt đo.

        TagValueChangedEventArgs _tagS1, _tagS2, _tagS3;

        List<int> _asciiCodeList = new List<int>();

        public Form1()
        {
            InitializeComponent();

            // Example ASCII codes (separated by spaces)
            string asciiInput = "72 101 108 108 111 32 87 111 114 108 100";

            foreach (var item in asciiInput.Split(' '))
            {
                if (int.TryParse(item, out int code))
                {
                    _asciiCodeList.Add(code);
                }
            }

            // Convert ASCII to string
            var re = ConvertASCIIToString(_asciiCodeList);

            #region Đọc file cấu hình
            // Automatically use the same directory as the executable
            _fileName = "ConfigSystem.json";
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _fileName);

            // Check if file exists
            if (!File.Exists(_filePath))
            {
                MessageBox.Show("Không tìm thấy file cấu hình, vào cấu hình lại.", "CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                using (var nf = new frmSettings())
                {
                    nf.ShowDialog();
                }
            }

            // Read the JSON file
            string jsonContent = File.ReadAllText(_filePath);
            _configs = JsonConvert.DeserializeObject<List<Configs>>(jsonContent);

            foreach (var item in _configs)
            {
                _cbSelectConfig.Items.Add(item.ConfigName);
            }

            _cbSelectConfig.SelectedIndexChanged += (s, o) =>
            {
                ComboBox cb = (ComboBox)s;
                var configName = _cbSelectConfig.SelectedItem.ToString();
                _configItem = _configs.FirstOrDefault(x => x.ConfigName == configName);
                if (_configItem == null)
                {
                    MessageBox.Show("Không tìm thấy cấu hình này.", "CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _labUnitS1.Text = _configItem.Config.Unit;
                _labUnitS2.Text = _configItem.Config.Unit;
                _labUnitS3.Text = _configItem.Config.Unit;
            };
            //_cbSelectConfig.SelectedIndex = 0;
            _cbSelectConfig.SelectedItem = _configs.FirstOrDefault().ConfigName;
            #endregion

            #region Khởi tạo easy drirver connector
            _easyDriverConnector = new EasyDriverConnector();
            _easyDriverConnector.ConnectionStatusChaged += _easyDriverConnector_ConnectionStatusChaged;
            _easyDriverConnector.BeginInit();
            _easyDriverConnector.EndInit();
            _labSriverStatus.Text = _easyDriverConnector.ConnectionStatus.ToString();

            _easyDriverConnector.Started += _easyDriverConnector_Started;
            if (_easyDriverConnector.IsStarted)
            {
                _easyDriverConnector_Started(null, null);
            }
            #endregion

            _btnArrow.Click += _btnArrow_Click;

            _btnApple.Click += (s, o) =>
            {
                _formActive = "APPLE";

                GlobalVariable.InvokeIfRequired(this, () =>
                {
                    _btnArrow.BackColor = Color.DarkGray;
                    _btnApple.BackColor = Color.GreenYellow;

                    _groupBoxArrowZone.Visible = false;
                    _groupBoxArrowResult.Visible = false;
                    _groupBoxApple.Visible = true;
                });
            };

            _btnSettings.Click += (s, o) =>
            {
                using (var nL = new frmLogin())
                {
                    nL.ShowDialog();

                    if (nL.DialogResult == DialogResult.OK)
                    {
                        using (var nf = new frmSettings())
                        {
                            nf.ShowDialog();

                            // Read the JSON file
                            string jsonContent1 = File.ReadAllText(_filePath);
                            _configs = JsonConvert.DeserializeObject<List<Configs>>(jsonContent1);

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

                            // Set the selected item to the first one in the list
                            _cbSelectConfig.SelectedItem = nf.ConfigName;

                            //_labUnitS1.Text = _configItem.Config.Unit;
                            //_labUnitS2.Text = _configItem.Config.Unit;
                            //_labUnitS3.Text = _configItem.Config.Unit;

                            //SENSOR_1_ValueChanged(null, _tagS1);
                            //SENSOR_2_ValueChanged(null, _tagS2);
                            //SENSOR_3_ValueChanged(null, _tagS3);
                        }
                    }
                }
            };

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

                _labUnitS1.Text = _configItem.Config.Unit;
                _labUnitS2.Text = _configItem.Config.Unit;
                _labUnitS3.Text = _configItem.Config.Unit;

                SENSOR_1_ValueChanged(null, _tagS1);
                SENSOR_2_ValueChanged(null, _tagS2);
                SENSOR_3_ValueChanged(null, _tagS3);
            };

            //reset these control results.
            GlobalVariable.InvokeIfRequired(this, () =>
            {
                _labArrowZone.Text = _labArrowResultHead.Text = _labAppleResult.Text = null;
                _labArrowZone.BackColor = _labArrowResultHead.BackColor = _labAppleResult.BackColor = Color.White;

                _labArrowValueFinal.Text = _labArrowValueHead.Text = _labAppleValueFinal.Text = "0";
                _labArrowValueFinal.ForeColor = _labArrowValueHead.ForeColor = _labAppleValueFinal.ForeColor = Color.Black;
            });

            Load += Form1_Load;
            FormClosing += Form1_FormClosing;
        }

        private void _btnArrow_Click(object sender, EventArgs e)
        {
            _formActive = "AROW";

            GlobalVariable.InvokeIfRequired(this, () =>
            {
                _btnApple.BackColor = Color.DarkGray;
                _btnArrow.BackColor = Color.GreenYellow;

                _groupBoxArrowZone.Visible = true;
                _groupBoxArrowResult.Visible = true;
                _groupBoxApple.Visible = false;
            });
        }

        private void _easyDriverConnector_ConnectionStatusChaged(object sender, ConnectionStatusChangedEventArgs e)
        {
            GlobalVariable.InvokeIfRequired(this, () =>
            {
                //_pnStatus.Text = e.NewStatus.ToString();
                _labSriverStatus.BackColor = GetConnectionStatusColor(e.NewStatus);
                _labSriverStatus.Text = _easyDriverConnector.ConnectionStatus.ToString();
            });
        }

        private Color GetConnectionStatusColor(ConnectionStatus status)
        {
            switch (status)
            {
                case ConnectionStatus.Connected:
                    return Color.Lime;
                case ConnectionStatus.Connecting:
                case ConnectionStatus.Reconnecting:
                    return Color.Orange;
                case ConnectionStatus.Disconnected:
                    return Color.Red;
                default:
                    return Color.White;
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn tắt app?!", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            _easyDriverConnector.ConnectionStatusChaged -= _easyDriverConnector_ConnectionStatusChaged;
            _easyDriverConnector.Started -= _easyDriverConnector_Started;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region Serilog initial
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs/AppLog.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
            #endregion

            _btnArrow_Click(null, null);

            _timer.Interval = 500;
            _timer.Tick += _timer_Tick;
            _timer.Enabled = true;
        }

        #region Events
        private async void _timer_Tick(object sender, EventArgs e)
        {
            Timer t = (Timer)sender;
            try
            {
                t.Enabled = false;
                GlobalVariable.InvokeIfRequired(this, () => { _labTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); });

                #region Kiểm tra
                if (_newTransaction)
                {
                    if (_formActive == "APPLE")
                    {
                        AppleCheck();
                    }
                    else
                    {
                        ArrowCheck();
                    }

                    //nếu cả 3 giá trị của sensor đều = 0 thì reset biến _newTransaction để báo hiện máy không có đo, ngắt kiểm tra.
                    if ((_valueSensor1 <= 0 && _valueSensor2 <= 0 && _valueSensor3 <= 0)
                        || _valueSensor1 >= _configItem.Config.ValueActive
                        || _valueSensor2 >= _configItem.Config.ValueActive
                        || _valueSensor3 >= _configItem.Config.ValueActive
                        )
                    {
                        _newTransaction = false;

                        GlobalVariable.InvokeIfRequired(this, () =>
                        {
                            _labArrowZone.Text = _labArrowResultHead.Text = _labAppleResult.Text = null;
                            _labArrowZone.BackColor = _labArrowResultHead.BackColor = _labAppleResult.BackColor = Color.White;

                            _labArrowValueFinal.Text = _labArrowValueHead.Text = _labAppleValueFinal.Text = "0";
                            _labArrowValueFinal.ForeColor = _labArrowValueHead.ForeColor = _labAppleValueFinal.ForeColor = Color.Black;
                        });
                    }
                }
                #endregion
            }
            catch (Exception ex) { Log.Error(ex, "From _timer_Tick"); }
            finally
            {
                t.Enabled = true;
            }
        }

        private void _easyDriverConnector_Started(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
            //foreach (var item in _ovensInfo)
            {
                //easyDriverConnector1.GetTag($"{item.Path}/Temperature").QualityChanged += Temperature_QualityChanged;
                _easyDriverConnector.GetTag($"Local Station/Channel1/Device/SENSOR_1").ValueChanged += SENSOR_1_ValueChanged;
                _easyDriverConnector.GetTag($"Local Station/Channel1/Device/SENSOR_2").ValueChanged += SENSOR_2_ValueChanged;
                _easyDriverConnector.GetTag($"Local Station/Channel1/Device/SENSOR_3").ValueChanged += SENSOR_3_ValueChanged;

                SENSOR_1_ValueChanged(_easyDriverConnector.GetTag($"Local Station/Channel1/Device/SENSOR_1")
                    , new TagValueChangedEventArgs(_easyDriverConnector.GetTag($"Local Station/Channel1/Device/SENSOR_1")
                    , "", _easyDriverConnector.GetTag($"Local Station/Channel1/Device/SENSOR_1").Value));
                SENSOR_2_ValueChanged(_easyDriverConnector.GetTag($"Local Station/Channel1/Device/SENSOR_2")
                   , new TagValueChangedEventArgs(_easyDriverConnector.GetTag($"Local Station/Channel1/Device/SENSOR_2")
                   , "", _easyDriverConnector.GetTag($"Local Station/Channel1/Device/SENSOR_2").Value));
                SENSOR_3_ValueChanged(_easyDriverConnector.GetTag($"Local Station/Channel1/Device/SENSOR_3")
                   , new TagValueChangedEventArgs(_easyDriverConnector.GetTag($"Local Station/Channel1/Device/SENSOR_3")
                   , "", _easyDriverConnector.GetTag($"Local Station/Channel1/Device/SENSOR_3").Value));
            }
        }

        #region Event tag value change

        private void SENSOR_1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                _tagS1 = e;
                var path = e?.Tag.Parent.Path;

                var newValue = double.TryParse(e.NewValue, out double value) ? value : 0;

                _valueSensor1 = Math.Round(newValue * _configItem.Config.Gain + _configItem.Config.Offset, _configItem.Config.DecimalNum);
                _valueSensor1 = _valueSensor1 < 0 ? 0 : _valueSensor1;

                GlobalVariable.InvokeIfRequired(this, () => { _labValueS1.Text = _valueSensor1.ToString(); });

                //nếu giá trị cảm biến có sự thay đổi thì kích hoạt lại _newTransaction để vào kiểm tra tiếp.
                //tất cả giá trị của các sensor < hơn  giá trị kích hoạt thì mới cho phép đo
                if (_valueSensor1 > 0 && _valueSensor1 < _configItem.Config.ValueActive
                    && _valueSensor2 < _configItem.Config.ValueActive && _valueSensor3 < _configItem.Config.ValueActive)
                {
                    _newTransaction = true;
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void SENSOR_2_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                _tagS2 = e;
                var path = e.Tag.Parent.Path;

                var newValue = double.TryParse(e.NewValue, out double value) ? value : 0;

                _valueSensor2 = Math.Round(newValue * _configItem.Config.Gain + _configItem.Config.Offset, _configItem.Config.DecimalNum);
                _valueSensor2 = _valueSensor2 < 0 ? 0 : _valueSensor2;

                GlobalVariable.InvokeIfRequired(this, () => { _labValueS2.Text = _valueSensor2.ToString(); });

                //nếu giá trị cảm biến có sự thay đổi thì kích hoạt lại _newTransaction để vào kiểm tra tiếp.
                //tất cả giá trị của các sensor < hơn  giá trị kích hoạt thì mới cho phép đo
                if (_valueSensor2 > 0 && _valueSensor1 < _configItem.Config.ValueActive
                    && _valueSensor2 < _configItem.Config.ValueActive && _valueSensor3 < _configItem.Config.ValueActive)
                {
                    _newTransaction = true;
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void SENSOR_3_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                _tagS3 = e;
                var path = e.Tag.Parent.Path;

                var newValue = double.TryParse(e.NewValue, out double value) ? value : 0;

                _valueSensor3 = Math.Round(newValue * _configItem.Config.Gain + _configItem.Config.Offset, _configItem.Config.DecimalNum);

                _valueSensor3 = _valueSensor3 < 0 ? 0 : _valueSensor3;


                GlobalVariable.InvokeIfRequired(this, () => { _labValueS3.Text = _valueSensor3.ToString(); });

                //nếu giá trị cảm biến có sự thay đổi thì kích hoạt lại _newTransaction để vào kiểm tra tiếp.
                //tất cả giá trị của các sensor < hơn  giá trị kích hoạt thì mới cho phép đo
                if (_valueSensor3 > 0 && _valueSensor1 < _configItem.Config.ValueActive
                    && _valueSensor2 < _configItem.Config.ValueActive && _valueSensor3 < _configItem.Config.ValueActive)
                {
                    _newTransaction = true;
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        #endregion

        #endregion

        #region Methods
        private void AppleCheck()
        {
            string sensorView = string.Empty;
            string dataMax = _configItem.Config.AppleSettings.DataMax == true ? "Data lớn nhất" : "Data nhỏ nhất";
            var sensorCount = _configItem.Config.AppleSettings.Sensors.Count;//số lượng sensor được chọn để kiểm tra.
            double valueFinal = 0;
            List<double> valueZoneList = new List<double>();
            EnumApple_Ok_NG result = EnumApple_Ok_NG.NG;
            string limit = string.Empty;

            var sensors = _configItem.Config.AppleSettings.Sensors.ToList();
            foreach (var item in sensors)
            {
                if (item == EnumSensor.SENSOR_1)
                    valueZoneList.Add(_valueSensor1);
                else if (item == EnumSensor.SENSOR_2)
                    valueZoneList.Add(_valueSensor2);
                else
                    valueZoneList.Add(_valueSensor3);

                if (string.IsNullOrEmpty(sensorView))
                {
                    sensorView = item.ToString();
                }
                else
                    sensorView = string.Join(", ", sensorView, item.ToString());
            }

            //lấy giá trị lớn nhất hoặc nhỏ nhất trong các giá trị của cảm biến để phân zone dựa vào biến cài đặt DataMax.
            valueFinal = _configItem.Config.AppleSettings.DataMax == true ? valueZoneList.Max() : valueZoneList.Min();

            //kiểm tra
            foreach (var item in _configItem.Config.AppleSettings.Zones)
            {
                if (valueFinal >= item.FromValue && valueFinal <= item.ToValue)
                {
                    result = item.ZoneName;
                    limit = $"{item.ZoneName.ToString()} từ {item.FromValue} đến {item.ToValue}";
                    break;
                }
            }

            GlobalVariable.InvokeIfRequired(this, () =>
            {
                _labAppleResult.Text = result.ToString();
                _labAppleValueFinal.Text = $"+ Sensors: {sensorView}\n+ {dataMax}\n+ {limit}\n+ Giá trị sensor phân zone: {valueFinal.ToString()}";

                if (result == EnumApple_Ok_NG.OK)
                {
                    _labAppleResult.BackColor = Color.Green;
                    _labAppleValueFinal.ForeColor = Color.Green;
                }
                else
                {
                    _labAppleResult.BackColor = Color.Red;
                    _labAppleValueFinal.ForeColor = Color.Red;
                }
            });
        }

        private void ArrowCheck()
        {
            string sensorView = string.Empty;
            string dataMax = _configItem.Config.ArrowSettings.DataMax == true ? "Data lớn nhất" : "Data nhỏ nhất";
            string zoneLimit = string.Empty;

            var sensorCount = _configItem.Config.ArrowSettings.Sensors.Count;//số lượng sensor được chọn để kiểm tra.
            double valueFinal = 0;//giá trị cuối cùng dùng để phân Zone.
            List<double> valueZoneList = new List<double>();//list chứa các giá trị của các sensor được chọn để phân zone.
            EnumArrowZoneName zone = EnumArrowZoneName.V1;//tên zone cuối cùng được xác định.

            double compareValue = 0;//kiểm tra OK/NG
            string compareDisplay = string.Empty;

            //lấy các giá trị của cảm biến được chọn phân ZONE.
            var sensors = _configItem.Config.ArrowSettings.Sensors.ToList();
            foreach (var item in sensors)
            {
                if (item == EnumSensor.SENSOR_1)
                    valueZoneList.Add(_valueSensor1);
                else if (item == EnumSensor.SENSOR_2)
                    valueZoneList.Add(_valueSensor2);
                else
                    valueZoneList.Add(_valueSensor3);

                if (string.IsNullOrEmpty(sensorView))
                {
                    sensorView = item.ToString();
                }
                else
                    sensorView = string.Join(", ", sensorView, item.ToString());
            }
            //kiểm tra xem phần chọn các sensor phân zone theo điều kiện, nếu thỏa mãn thì add thêm vào.
            var sensorsConditons = _configItem.Config.ArrowSettings.ArrowChooseSensorAddCompareSettings.Sensors.ToList();
            foreach (var item in sensorsConditons)
            {
                if (item == EnumSensor.SENSOR_1 && _valueSensor1 > _configItem.Config.ArrowSettings.ArrowChooseSensorAddCompareSettings.ConditionsValue)
                {
                    valueZoneList.Add(_valueSensor1);

                    if (string.IsNullOrEmpty(sensorView))
                    {
                        sensorView = item.ToString();
                    }
                    else
                        sensorView = string.Join(", ", sensorView, item.ToString());
                }
                else if (item == EnumSensor.SENSOR_2 && _valueSensor2 > _configItem.Config.ArrowSettings.ArrowChooseSensorAddCompareSettings.ConditionsValue)
                {
                    valueZoneList.Add(_valueSensor2);

                    if (string.IsNullOrEmpty(sensorView))
                    {
                        sensorView = item.ToString();
                    }
                    else
                        sensorView = string.Join(", ", sensorView, item.ToString());
                }
                else if (item == EnumSensor.SENSOR_3 && _valueSensor3 > _configItem.Config.ArrowSettings.ArrowChooseSensorAddCompareSettings.ConditionsValue)
                {
                    valueZoneList.Add(_valueSensor3);

                    if (string.IsNullOrEmpty(sensorView))
                    {
                        sensorView = item.ToString();
                    }
                    else
                        sensorView = string.Join(", ", sensorView, item.ToString());
                }
            }

            //lấy giá trị lớn nhất hoặc nhỏ nhất trong các giá trị của cảm biến để phân zone dựa vào biến cài đặt DataMax.
            valueFinal = _configItem.Config.ArrowSettings.DataMax == true ? valueZoneList.Max() : valueZoneList.Min();

            //kiểm tra
            foreach (var item in _configItem.Config.ArrowSettings.Zones)
            {
                if (valueFinal >= item.FromValue && valueFinal <= item.ToValue)
                {
                    zone = item.ZoneName;
                    zoneLimit = $"{zone.ToString()}: Từ {item.FromValue} đến {item.ToValue}";
                    break;
                }
            }

            //tính giá trị OK/NG.
            if (_configItem.Config.ArrowSettings.S3_S1OrS1_S3)
            {
                compareValue = Math.Round(_valueSensor3 - _valueSensor1, _configItem.Config.DecimalNum);
                compareDisplay = "Sensor 3 - Sensor 1";
            }
            else
            {
                compareValue = Math.Round(_valueSensor1 - _valueSensor3, _configItem.Config.DecimalNum);
                compareDisplay = "Sensor 1 - Sensor 3";
            }

            GlobalVariable.InvokeIfRequired(this, () =>
            {
                //ZONE
                _labArrowZone.Text = zone.ToString();
                _labArrowValueFinal.Text = $"+ Sensors: {sensorView}\n+ {dataMax}\n+ Giá trị đặt: {_configItem.Config.ArrowSettings.ArrowChooseSensorAddCompareSettings.ConditionsValue}\n+ {zoneLimit}\n+ Giá trị sensor phân zone: {valueFinal.ToString()}";

                if (zone == EnumArrowZoneName.V1)
                    _labArrowZone.BackColor = Color.White;
                else if (zone == EnumArrowZoneName.V3)
                    _labArrowZone.BackColor = Color.Yellow;
                else if (zone == EnumArrowZoneName.V6)
                    _labArrowZone.BackColor = Color.Red;
                else if (zone == EnumArrowZoneName.V9)
                    _labArrowZone.BackColor = Color.Green;
                else if (zone == EnumArrowZoneName.VSS)
                    _labArrowZone.BackColor = Color.Aqua;

                //OK/NG
                if (_configItem.Config.ActiveCheckHeadStraight)
                {
                    _labArrowValueHead.Text = $"+ {compareDisplay}\n+ Giá trị đặt: {_configItem.Config.ArrowSettings.CompareValue}\n+ Giá trị tính toán: {compareValue.ToString()}";
                    if (compareValue >= _configItem.Config.ArrowSettings.CompareValue)
                    {
                        _labArrowResultHead.BackColor = Color.Green;
                        _labArrowResultHead.Text = "OK";
                        _labArrowValueHead.ForeColor = Color.Green;
                    }
                    else
                    {
                        _labArrowResultHead.BackColor = Color.Red;
                        _labArrowResultHead.Text = "NG";
                        _labArrowValueHead.ForeColor = Color.Red;
                    }
                }
                else
                {
                    _labArrowResultHead.BackColor = Color.White;
                    _labArrowResultHead.Text = _labArrowValueHead.Text = string.Empty;
                    _labArrowValueHead.ForeColor = Color.Black;
                }
            });
        }

        private string ConvertASCIIToString(List<int> ascii)
        {
            // Convert ASCII to string
            string result = new string(
                ascii
                    .Select(code => (char)code)
                    .ToArray()
            );

            return result;
        }
        #endregion
    }
}