namespace GiamSat.Scada
{
    partial class frmChangePass
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangePass));
            this._btnLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._txtPassword = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this._txtNewPass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._txtNewPassRe = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _btnLogin
            // 
            this._btnLogin.Location = new System.Drawing.Point(60, 231);
            this._btnLogin.Name = "_btnLogin";
            this._btnLogin.Size = new System.Drawing.Size(128, 23);
            this._btnLogin.TabIndex = 4;
            this._btnLogin.Text = "XÁC NHẬN";
            this._btnLogin.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(30, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 31);
            this.label1.TabIndex = 22;
            this.label1.Text = "ĐỔI MẬT KHẨU";
            // 
            // _txtPassword
            // 
            this._txtPassword.Location = new System.Drawing.Point(60, 92);
            this._txtPassword.Name = "_txtPassword";
            this._txtPassword.Size = new System.Drawing.Size(128, 20);
            this._txtPassword.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(60, 72);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "MẬT KHẨU";
            // 
            // _txtNewPass
            // 
            this._txtNewPass.Location = new System.Drawing.Point(63, 141);
            this._txtNewPass.Name = "_txtNewPass";
            this._txtNewPass.Size = new System.Drawing.Size(128, 20);
            this._txtNewPass.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "MẬT KHẨU MỚI";
            // 
            // _txtNewPassRe
            // 
            this._txtNewPassRe.Location = new System.Drawing.Point(63, 193);
            this._txtNewPassRe.Name = "_txtNewPassRe";
            this._txtNewPassRe.Size = new System.Drawing.Size(128, 20);
            this._txtNewPassRe.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "NHẬP LẠI MẬT KHẨU MỚI";
            // 
            // frmChangePass
            // 
            this.AcceptButton = this._btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 276);
            this.Controls.Add(this._txtNewPassRe);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._txtNewPass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._btnLogin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._txtPassword);
            this.Controls.Add(this.label11);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChangePass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Pass";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _btnLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _txtPassword;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox _txtNewPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _txtNewPassRe;
        private System.Windows.Forms.Label label3;
    }
}