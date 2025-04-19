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


namespace GiamSat.Scada
{
    public partial class Form1 : Form
    {
        private Timer _timer = new Timer();

        private bool _isLoaded = false;
        private bool _isReOpenApp = false;//biến dùng để báo ap bị tắt mở lại, vào lấy lại ZIndex cũ trước khi tắt phần mềm để log profile tiếp

        public Form1()
        {
            InitializeComponent();

            Load += Form1_Load;
            FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Không tắt app này!", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            //e.Cancel = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region Serilog initial
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
            #endregion

            #region Get thong tin chuong
            GetOvensInfo();
            #endregion

            easyDriverConnector1.Started += EasyDriverConnector1_Started;

            _timer.Interval = 5000;
            _timer.Tick += _timer_Tick;
            _timer.Enabled = true;
        }

        #region Methods
        /// <summary>
        /// Khởi tạo data ban đầu.
        /// </summary>
        private void GetOvensInfo()
        {
            try
            {
                //Path = $"Local Station/Channel{chanel}/Oven{i}"
            }
            catch (Exception ex) { Log.Error(ex, "Initialize"); }
        }

        private void RefreshConfig()
        {
            try
            {
               
            }
            catch (Exception ex) { Log.Error(ex, "Initialize"); }
        }
        #endregion

        #region Events
        private async void _timer_Tick(object sender, EventArgs e)
        {
            Timer t = (Timer)sender;
            try
            {
                t.Enabled = false;

                if (_isLoaded)
                {
                    //lấy các thông số config
                    RefreshConfig();

                    #region Kiểm tra kết nối đến easy driver server
                    GlobalVariable.InvokeIfRequired(this, () =>
                    {
                        _labSriverStatus.Text = easyDriverConnector1.ConnectionStatus.ToString();
                        if (easyDriverConnector1.ConnectionStatus == ConnectionStatus.Connected)
                        {
                            _pnStatus.BackColor = Color.Green;
                        }
                        else
                        {
                            _pnStatus.BackColor = Color.Red;
                        }
                    });
                    #endregion
                }
            }
            catch (Exception ex) { Log.Error(ex, "From _timer_Tick"); }
            finally
            {
                t.Enabled = true;
            }
        }

        private void EasyDriverConnector1_Started(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
            foreach (var item in _ovensInfo)
            {
                easyDriverConnector1.GetTag($"{item.Path}/Temperature").QualityChanged += Temperature_QualityChanged;

                easyDriverConnector1.GetTag($"{item.Path}/Temperature").ValueChanged += Temperature_ValueChanged;
                easyDriverConnector1.GetTag($"{item.Path}/DigitalInput1Status").ValueChanged += DigitalInput1Status_ValueChanged;
                easyDriverConnector1.GetTag($"{item.Path}/ProfileNumber_CurrentStatus").ValueChanged += ProfileNumber_CurrentStatus_ValueChanged;
                easyDriverConnector1.GetTag($"{item.Path}/ProfileStepNumber_CurrentStatus").ValueChanged += ProfileStepNumber_CurrentStatus_ValueChanged;
                easyDriverConnector1.GetTag($"{item.Path}/ProfileStepType_CurrentStatus").ValueChanged += ProfileStepType_CurrentStatus_ValueChanged;
                easyDriverConnector1.GetTag($"{item.Path}/HoursRemaining_CurrentStatus").ValueChanged += HoursRemaining_CurrentStatus_ValueChanged;
                easyDriverConnector1.GetTag($"{item.Path}/MinutesRemaining_CurrentStatus").ValueChanged += MinutesRemaining_CurrentStatus_ValueChanged;
                easyDriverConnector1.GetTag($"{item.Path}/SecondsRemaining_CurrentStatus").ValueChanged += SecondsRemaining_CurrentStatus_ValueChanged;
                easyDriverConnector1.GetTag($"{item.Path}/EndSetPointCh1_CurrentStatus").ValueChanged += EndSetPointCh1_CurrentStatus_ValueChanged;

                #region Comment
                //easyDriverConnector1.GetTag($"{item.Path}/Profile1Name_C1").ValueChanged += Profile1Name_C1_ValueChanged;
                //easyDriverConnector1.GetTag($"{item.Path}/Profile1Name_C2").ValueChanged += Profile1Name_C2_ValueChanged;
                //easyDriverConnector1.GetTag($"{item.Path}/Profile1Name_C3").ValueChanged += Profile1Name_C3_ValueChanged;
                //easyDriverConnector1.GetTag($"{item.Path}/Profile1Name_C4").ValueChanged += Profile1Name_C4_ValueChanged;
                //easyDriverConnector1.GetTag($"{item.Path}/Profile1Name_C5").ValueChanged += Profile1Name_C5_ValueChanged;
                //easyDriverConnector1.GetTag($"{item.Path}/Profile1Name_C6").ValueChanged += Profile1Name_C6_ValueChanged;
                //easyDriverConnector1.GetTag($"{item.Path}/Profile1Name_C7").ValueChanged += Profile1Name_C7_ValueChanged;
                //easyDriverConnector1.GetTag($"{item.Path}/Profile1Name_C8").ValueChanged += Profile1Name_C8_ValueChanged;
                //easyDriverConnector1.GetTag($"{item.Path}/Profile1Name_C9").ValueChanged += Profile1Name_C9_ValueChanged;
                //easyDriverConnector1.GetTag($"{item.Path}/Profile1Name_C10").ValueChanged += Profile1Name_C10_ValueChanged;

                //easyDriverConnector1.GetTag($"{item.Path}/Profile2Name_C1").ValueChanged += Profile2Name_C1_ValueChanged;
                //easyDriverConnector1.GetTag($"{item.Path}/Profile2Name_C2").ValueChanged += Profile2Name_C2_ValueChanged;
                //easyDriverConnector1.GetTag($"{item.Path}/Profile2Name_C3").ValueChanged += Profile2Name_C3_ValueChanged;
                //easyDriverConnector1.GetTag($"{item.Path}/Profile2Name_C4").ValueChanged += Profile2Name_C4_ValueChanged;
                //easyDriverConnector1.GetTag($"{item.Path}/Profile2Name_C5").ValueChanged += Profile2Name_C5_ValueChanged;
                //easyDriverConnector1.GetTag($"{item.Path}/Profile2Name_C6").ValueChanged += Profile2Name_C6_ValueChanged;
                //easyDriverConnector1.GetTag($"{item.Path}/Profile2Name_C7").ValueChanged += Profile2Name_C7_ValueChanged;
                //easyDriverConnector1.GetTag($"{item.Path}/Profile2Name_C8").ValueChanged += Profile2Name_C8_ValueChanged;
                //easyDriverConnector1.GetTag($"{item.Path}/Profile2Name_C9").ValueChanged += Profile2Name_C9_ValueChanged;
                //easyDriverConnector1.GetTag($"{item.Path}/Profile2Name_C10").ValueChanged += Profile2Name_C10_ValueChanged;

                //Profile1Name_C1_ValueChanged(easyDriverConnector1.GetTag($"{item.Path}/Profile1Name_C1")
                //   , new TagValueChangedEventArgs(easyDriverConnector1.GetTag($"{item.Path}/Profile1Name_C1")
                //   , "", easyDriverConnector1.GetTag($"{item.Path}/Profile1Name_C1").Value));
                #endregion

                Temperature_QualityChanged(easyDriverConnector1.GetTag($"{item.Path}/Temperature")
             , new TagQualityChangedEventArgs(easyDriverConnector1.GetTag($"{item.Path}/Temperature")
             , Quality.Uncertain, easyDriverConnector1.GetTag($"{item.Path}/Temperature").Quality));

                Temperature_ValueChanged(easyDriverConnector1.GetTag($"{item.Path}/Temperature")
                    , new TagValueChangedEventArgs(easyDriverConnector1.GetTag($"{item.Path}/Temperature")
                    , "", easyDriverConnector1.GetTag($"{item.Path}/Temperature").Value));

                DigitalInput1Status_ValueChanged(easyDriverConnector1.GetTag($"{item.Path}/DigitalInput1Status")
                    , new TagValueChangedEventArgs(easyDriverConnector1.GetTag($"{item.Path}/DigitalInput1Status")
                    , "", easyDriverConnector1.GetTag($"{item.Path}/DigitalInput1Status").Value));

                ProfileNumber_CurrentStatus_ValueChanged(easyDriverConnector1.GetTag($"{item.Path}/ProfileNumber_CurrentStatus")
                    , new TagValueChangedEventArgs(easyDriverConnector1.GetTag($"{item.Path}/ProfileNumber_CurrentStatus")
                    , "", easyDriverConnector1.GetTag($"{item.Path}/ProfileNumber_CurrentStatus").Value));

                ProfileStepType_CurrentStatus_ValueChanged(easyDriverConnector1.GetTag($"{item.Path}/ProfileStepType_CurrentStatus")
                    , new TagValueChangedEventArgs(easyDriverConnector1.GetTag($"{item.Path}/ProfileStepType_CurrentStatus")
                    , "", easyDriverConnector1.GetTag($"{item.Path}/ProfileStepType_CurrentStatus").Value));

                HoursRemaining_CurrentStatus_ValueChanged(easyDriverConnector1.GetTag($"{item.Path}/HoursRemaining_CurrentStatus")
                    , new TagValueChangedEventArgs(easyDriverConnector1.GetTag($"{item.Path}/HoursRemaining_CurrentStatus")
                    , "", easyDriverConnector1.GetTag($"{item.Path}/HoursRemaining_CurrentStatus").Value));

                MinutesRemaining_CurrentStatus_ValueChanged(easyDriverConnector1.GetTag($"{item.Path}/MinutesRemaining_CurrentStatus")
                    , new TagValueChangedEventArgs(easyDriverConnector1.GetTag($"{item.Path}/MinutesRemaining_CurrentStatus")
                    , "", easyDriverConnector1.GetTag($"{item.Path}/MinutesRemaining_CurrentStatus").Value));

                SecondsRemaining_CurrentStatus_ValueChanged(easyDriverConnector1.GetTag($"{item.Path}/SecondsRemaining_CurrentStatus")
                    , new TagValueChangedEventArgs(easyDriverConnector1.GetTag($"{item.Path}/SecondsRemaining_CurrentStatus")
                    , "", easyDriverConnector1.GetTag($"{item.Path}/SecondsRemaining_CurrentStatus").Value));

                EndSetPointCh1_CurrentStatus_ValueChanged(easyDriverConnector1.GetTag($"{item.Path}/EndSetPointCh1_CurrentStatus")
                    , new TagValueChangedEventArgs(easyDriverConnector1.GetTag($"{item.Path}/EndSetPointCh1_CurrentStatus")
                    , "", easyDriverConnector1.GetTag($"{item.Path}/EndSetPointCh1_CurrentStatus").Value));

                ProfileStepNumber_CurrentStatus_ValueChanged(easyDriverConnector1.GetTag($"{item.Path}/ProfileStepNumber_CurrentStatus")
                   , new TagValueChangedEventArgs(easyDriverConnector1.GetTag($"{item.Path}/ProfileStepNumber_CurrentStatus")
                   , "", easyDriverConnector1.GetTag($"{item.Path}/ProfileStepNumber_CurrentStatus").Value));
            }


            //GlobalVariable.InvokeIfRequired(this, () =>
            //{
            //    _labSriverStatus.Text = easyDriverConnector1.ConnectionStatus.ToString();
            //    if (easyDriverConnector1.ConnectionStatus == ConnectionStatus.Connected)
            //    {
            //        _pnStatus.BackColor = Color.Green;
            //    }
            //    else
            //    {
            //        _pnStatus.BackColor = Color.Red;
            //    }
            //});
        }

        #region Event tag value change

        private async void Temperature_QualityChanged(object sender, TagQualityChangedEventArgs e)
        {
            try
            {
                var path = e.Tag.Parent.Path;
                var deviceName = e.Tag.Parent.Name;
                var al = deviceName.Substring(4);

                foreach (var item in _displayRealtime)
                {
                    if (item.Path == path)
                    {
                        item.ConnectionStatus = e.NewQuality == Quality.Good ? 1 : 0;

                        if (item.ConnectionStatus == 0)
                        {
                            item.AlarmDescription = "Mất kết nối đến lò";
                            //await easyDriverConnector1.WriteTagAsync($"Local Station/Channel4/PLC/AL{al}", "2", WritePiority.High);
                            _alarmEnable[item.OvenId - 1] = true;
                            _alarmValue[item.OvenId - 1] = "2";
                            item.SerienStatus = 1;
                        }
                        else
                        {
                            item.AlarmDescription = null;
                            //await easyDriverConnector1.WriteTagAsync($"Local Station/Channel4/PLC/AL{al}", "0", WritePiority.High);
                            _alarmEnable[item.OvenId - 1] = true;
                            _alarmValue[item.OvenId - 1] = "0";
                            item.SerienStatus = 0;
                        }

                        Debug.WriteLine($"Alarm description {item.OvenId}:{item.AlarmDescription}");
                        return;
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private void Temperature_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e.Tag.Parent.Path;

                foreach (var item in _displayRealtime)
                {
                    if (item.Path == path)
                    {
                        //Debug.WriteLine($"{path}/Tempperature: {e.NewValue}");
                        item.Temperature = double.TryParse(e.NewValue, out double value) ? Math.Round(value * GlobalVariable.ConfigSystem.Gain, 1) : item.Temperature;

                        return;
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private async void DigitalInput1Status_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e.Tag.Parent.Path;
                var deviceName = e.Tag.Parent.Name;
                var al = deviceName.Substring(4);

                foreach (var item in _displayRealtime)
                {
                    if (item.Path == path)
                    {
                        //Watlow tra tin hieu ve khi co tác động là 0, còn không tác động là 1.
                        item.DoorStatus = e.NewValue == "1" ? 1 : 0;

                        if (e.NewValue == "0")
                        {
                            item.AlarmDescription = "Cửa lò mở";

                            if (item.Status == 1)
                            {
                                //await easyDriverConnector1.WriteTagAsync($"Local Station/Channel4/PLC/AL{al}", "1", WritePiority.High);
                                _alarmEnable[item.OvenId - 1] = true;
                            }
                        }
                        else
                        {
                            item.AlarmDescription = null;

                            if (item.Status == 1)
                            {
                                //await easyDriverConnector1.WriteTagAsync($"Local Station/Channel4/PLC/AL{al}", "0", WritePiority.High);
                                _alarmEnable[item.OvenId - 1] = false;
                            }
                        }
                        return;
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private void EndSetPointCh1_CurrentStatus_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e.Tag.Parent.Path;

                foreach (var item in _displayRealtime)
                {
                    if (item.Path == path)
                    {
                        //item.SetPoint = double.TryParse(e.NewValue, out double value) ? value : item.SetPoint;
                        return;
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private void SecondsRemaining_CurrentStatus_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e.Tag.Parent.Path;

                foreach (var item in _displayRealtime)
                {
                    if (item.Path == path)
                    {
                        item.StatusTimeBegin = DateTime.Now;

                        item.SecondsRemaining_CurrentStatus = int.TryParse(e.NewValue, out int value) ? value : item.SecondsRemaining_CurrentStatus;

                        if (item.CountSecondTagChange > 1000) item.CountSecondTagChange = 1;

                        item.CountSecondTagChange += 1;

                        return;
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private void MinutesRemaining_CurrentStatus_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e.Tag.Parent.Path;

                foreach (var item in _displayRealtime)
                {
                    if (item.Path == path)
                    {
                        item.MinutesRemaining_CurrentStatus = int.TryParse(e.NewValue, out int value) ? value : item.MinutesRemaining_CurrentStatus;
                        return;
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private void HoursRemaining_CurrentStatus_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e.Tag.Parent.Path;

                foreach (var item in _displayRealtime)
                {
                    if (item.Path == path)
                    {
                        item.HoursRemaining_CurrentStatus = int.TryParse(e.NewValue, out int value) ? value : item.HoursRemaining_CurrentStatus;
                        return;
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private void ProfileStepType_CurrentStatus_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e.Tag.Parent.Path;

                foreach (var item in _displayRealtime)
                {
                    if (item.Path == path)
                    {
                        item.ProfileStepType_CurrentStatus = e.NewValue == "1" ? EnumProfileStepType.RampTime
                                                            : e.NewValue == "2" ? EnumProfileStepType.RampRate
                                                            : e.NewValue == "3" ? EnumProfileStepType.Soak
                                                            : e.NewValue == "4" ? EnumProfileStepType.Jump
                                                            : EnumProfileStepType.End;
                        return;
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private void ProfileStepNumber_CurrentStatus_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e.Tag.Parent.Path;

                foreach (var item in _displayRealtime)
                {
                    if (item.Path == path)
                    {
                        if (item.OvenId == 11)
                        {
                            var a = 11;
                        }

                        item.ProfileStepNumber_CurrentStatus = int.TryParse(e.NewValue, out int value) ? value : item.ProfileStepNumber_CurrentStatus;
                        var profile = item.OvenInfo.Profiles.FirstOrDefault(x => x.Id == item.ProfileNumber_CurrentStatus);
                        item.ProfileName = profile?.Name;
                        var step = profile?.Steps.FirstOrDefault(x => x.Id == item.ProfileStepNumber_CurrentStatus);

                        if (item.SetPoint > 0)
                        {
                            if (item.ProfileStepNumber_CurrentStatus != 1)
                            {
                                item.SetPointLastStep = item.SetPoint;
                                item.LastStepType = item.StepName;
                                item.EndStep = 1;
                            }
                            else
                            {
                                item.SetPointLastStep = item.Temperature;
                                item.LastStepType = 0;
                                item.EndStep = 0;
                            }

                            _isReOpenApp = false;
                        }
                        else
                        {
                            _isReOpenApp = true;
                            var lastStep = profile?.Steps.FirstOrDefault(x => x.Id == item.ProfileStepNumber_CurrentStatus - 1);

                            if (lastStep != null)
                            {
                                item.SetPointLastStep = lastStep.SetPoint;
                                item.LastStepType = lastStep.StepType;
                            }
                            else
                            {
                                item.SetPointLastStep = item.Temperature;
                                item.LastStepType = 0;
                            }
                        }

                        item.BeginTimeAlarm = DateTime.Now;
                        item.BeginTimeOfStep = DateTime.Now;
                        item.TempBeginStep = item.Temperature;
                        item.IsCheckAlarm = false;
                        item.AlarmFlag = false;
                        item.AlarmFlagLastStep = false;
                        //item.StatusFlag = false;

                        //cập nhật các thông số cảu step mới vào để chạy

                        if (step != null)
                        {
                            item.StepName = step.StepType;
                            item.Hours = (int)(step?.Hours); item.Minutes = (int)(step?.Minutes); item.Seconds = (int)(step?.Seconds);
                            item.SetPoint = (double)(step?.SetPoint);

                            item.TempRange = Math.Round(Math.Abs((item.SetPoint - item.Temperature)), 2);
                        }

                        _isLoaded = true;//báo đã khởi tạo xong cho phép các task chạy

                        Debug.WriteLine($"{item.OvenName} - {item.StepName} - EndStep: {item.EndStep} - Last set point: {item.SetPointLastStep} - set point: {item.SetPoint}");
                        return;
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private void ProfileNumber_CurrentStatus_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e.Tag.Parent.Path;

                foreach (var item in _displayRealtime)
                {
                    if (item.Path == path)
                    {
                        item.ProfileNumber_CurrentStatus = int.TryParse(e.NewValue, out int value) ? value : item.ProfileNumber_CurrentStatus;

                        var profile = item.OvenInfo.Profiles.FirstOrDefault(x => x.Id == item.ProfileNumber_CurrentStatus);
                        item.ProfileName = profile?.Name;
                        item.LevelUp = (double)(profile?.LevelUp);
                        item.LevelDown = (double)(profile?.LevelDown);
                        return;
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private void Profile1Name_C1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e.Tag.Parent.Path;

                foreach (var item in _displayRealtime)
                {
                    if (item.Path == path)
                    {
                        var c = (char)(int.TryParse(e.NewValue, out int value) ? value : 32);

                        Debug.WriteLine($"{e.Tag.Path}:{c}");
                        return;
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        #endregion
        #endregion
    }
}
