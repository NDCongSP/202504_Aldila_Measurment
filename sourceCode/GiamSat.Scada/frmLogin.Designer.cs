namespace GiamSat.Scada
{
    partial class frmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this._txtPassword = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this._txtUserName = new System.Windows.Forms.TextBox();
            this._lab = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._btnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _txtPassword
            // 
            this._txtPassword.Location = new System.Drawing.Point(72, 161);
            this._txtPassword.Name = "_txtPassword";
            this._txtPassword.Size = new System.Drawing.Size(128, 20);
            this._txtPassword.TabIndex = 17;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(72, 141);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "MẬT KHẨU";
            // 
            // _txtUserName
            // 
            this._txtUserName.Location = new System.Drawing.Point(72, 97);
            this._txtUserName.Name = "_txtUserName";
            this._txtUserName.Size = new System.Drawing.Size(128, 20);
            this._txtUserName.TabIndex = 15;
            // 
            // _lab
            // 
            this._lab.AutoSize = true;
            this._lab.Location = new System.Drawing.Point(72, 77);
            this._lab.Name = "_lab";
            this._lab.Size = new System.Drawing.Size(96, 13);
            this._lab.TabIndex = 14;
            this._lab.Text = "TÊN ĐĂNG NHẬP";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(72, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 31);
            this.label1.TabIndex = 18;
            this.label1.Text = "ĐĂNG NHẬP";
            // 
            // _btnLogin
            // 
            this._btnLogin.Location = new System.Drawing.Point(72, 204);
            this._btnLogin.Name = "_btnLogin";
            this._btnLogin.Size = new System.Drawing.Size(128, 23);
            this._btnLogin.TabIndex = 19;
            this._btnLogin.Text = "XÁC NHẬN";
            this._btnLogin.UseVisualStyleBackColor = true;
            // 
            // frmLogin
            // 
            this.AcceptButton = this._btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 260);
            this.Controls.Add(this._btnLogin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._txtPassword);
            this.Controls.Add(this.label11);
            this.Controls.Add(this._txtUserName);
            this.Controls.Add(this._lab);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _txtPassword;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox _txtUserName;
        private System.Windows.Forms.Label _lab;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _btnLogin;
    }
}