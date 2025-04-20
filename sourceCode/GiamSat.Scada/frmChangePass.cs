using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiamSat.Scada
{
    public partial class frmChangePass : Form
    {
        string _fileName;
        string _filePath;

        string _oldPass = string.Empty;
        string _newPass = string.Empty;
        string _newPassRe = string.Empty;   

        LoginModel _userLogin = new LoginModel();

        public frmChangePass()
        {
            InitializeComponent();

            // Automatically use the same directory as the executable
            _fileName = "Loggin.json";
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _fileName);

            // Check if file exists
            if (!File.Exists(_filePath))
            {
                // Create a new file with default content
                var newUser = new LoginModel
                {
                    UserName = MD5Hasher.EncryptToMD5("admin"),
                    Password = MD5Hasher.EncryptToMD5("123@456")
                };

                string defaultJson = JsonConvert.SerializeObject(newUser, Formatting.Indented);
                File.WriteAllText(_filePath, defaultJson);
            }

            // Read the JSON file
            string jsonContent = File.ReadAllText(_filePath);
            _userLogin = JsonConvert.DeserializeObject<LoginModel>(jsonContent);

            _txtPassword.PasswordChar = _txtNewPass.PasswordChar = _txtNewPassRe.PasswordChar = '*';

            _txtPassword.TextChanged += (s, e) =>
            {
               TextBox t=(TextBox)s;

                _oldPass = t.Text;
            };
            _txtNewPass.TextChanged += (s, e) =>
            {
                TextBox t = (TextBox)s;

                _newPass = t.Text;
            };
            _txtNewPassRe.TextChanged += (s, e) =>
            {
                TextBox t = (TextBox)s;

                _newPassRe = t.Text;
            };

            _btnLogin.Click += _btnLogin_Click;
        }

        private void _btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_oldPass) || string.IsNullOrEmpty(_newPass) || string.IsNullOrEmpty(_newPassRe))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin mật khẩu.");
                return;
            }

            if (!MD5Hasher.CompareWithMD5(_oldPass, _userLogin.Password))
            {
                MessageBox.Show("Mật khẩu cũ không chính xác.");
                return;
            }

            if (_newPass != _newPassRe)
            {
                MessageBox.Show("Mật khẩu mới không chính xác.");
                return;
            }

            // Create a new file with default content
            var newUser = new LoginModel
            {
                UserName = MD5Hasher.EncryptToMD5("admin"),
                Password = MD5Hasher.EncryptToMD5(_newPass)
            };

            string defaultJson = JsonConvert.SerializeObject(newUser, Formatting.Indented);
            File.WriteAllText(_filePath, defaultJson);

            MessageBox.Show("Đổi mật khẩu thành công.");
            this.Close();
        }
    }
}
