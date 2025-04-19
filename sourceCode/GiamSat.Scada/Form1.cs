using Dapper;
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

        private ConfigModel _configValue;
        string _fileName;
        string _filePath;

        private double _valueSensor1Old = 0, _valueSensor2Old = 0, _valueSensor3Old = 0;
        private double _valueSensor1 = 0, _valueSensor2 = 0, _valueSensor3 = 0;

        private bool _newTransaction = false;//biến để check mỗi lần giá trị đo từ 0 thay đổi, thì kích hoặt đo.

        public Form1()
        {
            InitializeComponent();

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
            _configValue = JsonConvert.DeserializeObject<ConfigModel>(jsonContent);

            _labUnitS1.Text = _configValue.Unit;
            _labUnitS2.Text = _configValue.Unit;
            _labUnitS3.Text = _configValue.Unit;
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
                using (var nf = new frmSettings())
                {
                    nf.ShowDialog();

                    // Read the JSON file
                    string jsonContent1 = File.ReadAllText(_filePath);
                    _configValue = JsonConvert.DeserializeObject<ConfigModel>(jsonContent1);

                    _labUnitS1.Text = _configValue.Unit;
                    _labUnitS2.Text = _configValue.Unit;
                    _labUnitS3.Text = _configValue.Unit;
                }
            };

            //reset these control results.
            GlobalVariable.InvokeIfRequired(this, () =>
            {
                _labArrowZone.Text = _labArrowResult.Text = _labAppleResult.Text = null;
                _labArrowZone.BackColor = _labArrowResult.BackColor = _labAppleResult.BackColor = Color.White;

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
            MessageBox.Show("Bạn chắc chắn muốn tắt app?!", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            _easyDriverConnector.ConnectionStatusChaged -= _easyDriverConnector_ConnectionStatusChaged;
            _easyDriverConnector.Started -= _easyDriverConnector_Started;
            //e.Cancel = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region Serilog initial
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
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
                    if (_valueSensor1 == 0 && _valueSensor2 == 0 && _valueSensor3 == 0)
                    {
                        _newTransaction = false;

                        GlobalVariable.InvokeIfRequired(this, () =>
                        {
                            _labArrowZone.Text = _labArrowResult.Text = _labAppleResult.Text = null;
                            _labArrowZone.BackColor = _labArrowResult.BackColor = _labAppleResult.BackColor = Color.White;

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
                var path = e.Tag.Parent.Path;

                var newValue = double.TryParse(e.NewValue, out double value) ? value : 0;

                _valueSensor1 = Math.Round(newValue * _configValue.Gain + _configValue.Offset, _configValue.DecimalNum);

                GlobalVariable.InvokeIfRequired(this, () => { _labValueS1.Text = _valueSensor1.ToString(); });

                //nếu giá trị cảm biến có sự thay đổi thì kích hoạt lại _newTransaction để vào kiểm tra tiếp.
                if (_valueSensor1 != 0) _newTransaction = true;
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void SENSOR_2_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e.Tag.Parent.Path;

                var newValue = double.TryParse(e.NewValue, out double value) ? value : 0;

                _valueSensor2 = Math.Round(newValue * _configValue.Gain + _configValue.Offset, _configValue.DecimalNum);

                GlobalVariable.InvokeIfRequired(this, () => { _labValueS2.Text = _valueSensor2.ToString(); });

                //nếu giá trị cảm biến có sự thay đổi thì kích hoạt lại _newTransaction để vào kiểm tra tiếp.
                if (_valueSensor2 != 0) _newTransaction = true;
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void SENSOR_3_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e.Tag.Parent.Path;

                var newValue = double.TryParse(e.NewValue, out double value) ? value : 0;

                _valueSensor3 = Math.Round(newValue * _configValue.Gain + _configValue.Offset, _configValue.DecimalNum);

                GlobalVariable.InvokeIfRequired(this, () => { _labValueS3.Text = _valueSensor3.ToString(); });

                //nếu giá trị cảm biến có sự thay đổi thì kích hoạt lại _newTransaction để vào kiểm tra tiếp.
                if (_valueSensor3 != 0) _newTransaction = true;
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        #endregion

        #endregion

        #region Methods
        private void AppleCheck()
        {
            var sensorCount = _configValue.AppleSettings.Sensors.Count;//số lượng sensor được chọn để kiểm tra.
            double valueFinal = 0;
            List<double> valueCompare = new List<double>();
            EnumApple_Ok_NG result = EnumApple_Ok_NG.NG;

            var sensors = _configValue.AppleSettings.Sensors.ToList();
            foreach (var item in sensors)
            {
                if (item == EnumSensor.SENSOR_1)
                    valueCompare.Add(_valueSensor1);
                else if (item == EnumSensor.SENSOR_2)
                    valueCompare.Add(_valueSensor2);
                else
                    valueCompare.Add(_valueSensor3);
            }

            //lấy giá trị lớn nhất hoặc nhỏ nhất trong các giá trị của cảm biến để phân zone dựa vào biến cài đặt DataMax.
            valueFinal = _configValue.AppleSettings.DataMax == true ? valueCompare.Max() : valueCompare.Min();

            //kiểm tra
            foreach (var item in _configValue.AppleSettings.Zones)
            {
                if (valueFinal >= item.FromValue && valueFinal <= item.ToValue)
                {
                    result = item.ZoneName;
                    break;
                }
            }

            GlobalVariable.InvokeIfRequired(this, () =>
            {
                _labAppleResult.Text = result.ToString();
                _labAppleValueFinal.Text = valueFinal.ToString();

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
            //var sensorCount = _configValue.AppleSettings.Sensors.Count;//số luongj sensor được chọn để kiểm tra.
            //double valueFinal = 0;

            //EnumApple_Ok_NG result = EnumApple_Ok_NG.NG;

            //if (sensorCount == 1)
            //{
            //    var s = _configValue.AppleSettings.Sensors.FirstOrDefault();

            //    switch (s)
            //    {
            //        case EnumSensor.SENSOR_1:
            //            valueFinal = _valueSensor1;
            //            break;
            //        case EnumSensor.SENSOR_2:
            //            valueFinal = _valueSensor2;
            //            break;
            //        case EnumSensor.SENSOR_3:
            //            valueFinal = _valueSensor3;
            //            break;
            //        default:
            //            break;
            //    }

            //    foreach (var item in _configValue.AppleSettings.Zones)
            //    {
            //        if (valueFinal >= item.FromValue && valueFinal <= item.ToValue)
            //        {
            //            result = item.ZoneName;
            //            break;
            //        }
            //    }

            //    GlobalVariable.InvokeIfRequired(this, () =>
            //    {
            //        _labAppleResult.Text = result.ToString();

            //        if (result == EnumApple_Ok_NG.OK)
            //            _labAppleResult.BackColor = Color.Green;
            //        else
            //            _labAppleResult.BackColor = Color.Red;
            //    });
            //}
        }
        #endregion
    }
}