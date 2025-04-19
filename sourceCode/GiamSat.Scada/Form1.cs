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
                }
            };

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

                SENSOR_1_ValueChanged(_easyDriverConnector.GetTag($"Local Station/Channel1/Device/SENSOR_1")
                    , new TagValueChangedEventArgs(_easyDriverConnector.GetTag($"Local Station/Channel1/Device/SENSOR_1")
                    , "", _easyDriverConnector.GetTag($"Local Station/Channel1/Device/SENSOR_1").Value));
            }
        }

        #region Event tag value change

        private void SENSOR_1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e.Tag.Parent.Path;

                //foreach (var item in _displayRealtime)
                //{
                //    if (item.Path == path)
                //    {
                //        //Debug.WriteLine($"{path}/Tempperature: {e.NewValue}");
                //        item.Temperature = double.TryParse(e.NewValue, out double value) ? Math.Round(value * GlobalVariable.ConfigSystem.Gain, 1) : item.Temperature;

                //        return;
                //    }
                //}
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        #endregion

        #endregion
    }
}