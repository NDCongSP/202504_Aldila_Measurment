using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiamSat.Scada
{
    public partial class frmLogin : Form
    {
        string _fileName;
        string _filePath;

        LoginModel _loginModel = new LoginModel();
        LoginModel _userLogin = new LoginModel();

        bool _isLogin = false;

        public frmLogin()
        {
            InitializeComponent();

            _txtPassword.PasswordChar = '*';

            _txtUserName.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;

                _loginModel.UserName = t.Text;
            };
            _txtPassword.TextChanged += (s, o) =>
            {
                TextBox t = (TextBox)s;

                _loginModel.Password = t.Text;
            };

            _btnLogin.Click += (s, o) =>
            {
                if (string.IsNullOrEmpty(_loginModel.UserName) || string.IsNullOrEmpty(_loginModel.Password))
                {
                    MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu");
                    return;
                }

                if (MD5Hasher.CompareWithMD5(_loginModel.UserName, _userLogin.UserName) && MD5Hasher.CompareWithMD5(_loginModel.Password, _userLogin.Password))
                {
                    //MessageBox.Show("Đăng nhập thành công");
                    _isLogin = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng");
                    _isLogin = false;
                }
            };

            Load += FrmLogin_Load;
            FormClosing += FrmLogin_FormClosing;
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isLogin)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            //string original = "123@456";
            //string hash = MD5Hasher.EncryptToMD5(original);

            //Console.WriteLine("Original: " + original);
            //Console.WriteLine("MD5 Hash: " + hash);

            //bool isMatch = MD5Hasher.CompareWithMD5("mypassword", hash);
            //Console.WriteLine("Match? " + isMatch);

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
        }
    }
}
