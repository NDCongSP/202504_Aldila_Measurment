
namespace GiamSat.Scada
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this._labTime = new System.Windows.Forms.Label();
            this._labSriverStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._btnArrow = new System.Windows.Forms.Button();
            this._btnApple = new System.Windows.Forms.Button();
            this._btnSettings = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._labValueS1 = new System.Windows.Forms.Label();
            this._labUnitS1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._labValueS2 = new System.Windows.Forms.Label();
            this._labUnitS2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this._labValueS3 = new System.Windows.Forms.Label();
            this._labUnitS3 = new System.Windows.Forms.Label();
            this._groupBoxArrowZone = new System.Windows.Forms.GroupBox();
            this._labArrowValueFinal = new System.Windows.Forms.Label();
            this._labArrowZone = new System.Windows.Forms.Label();
            this._groupBoxArrowResult = new System.Windows.Forms.GroupBox();
            this._labArrowValueHead = new System.Windows.Forms.Label();
            this._labArrowResultHead = new System.Windows.Forms.Label();
            this._groupBoxApple = new System.Windows.Forms.GroupBox();
            this._labAppleValueFinal = new System.Windows.Forms.Label();
            this._labAppleResult = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this._cbSelectConfig = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this._groupBoxArrowZone.SuspendLayout();
            this._groupBoxArrowResult.SuspendLayout();
            this._groupBoxApple.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // _labTime
            // 
            this._labTime.AutoSize = true;
            this._labTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labTime.Location = new System.Drawing.Point(866, 774);
            this._labTime.Name = "_labTime";
            this._labTime.Size = new System.Drawing.Size(183, 20);
            this._labTime.TabIndex = 2;
            this._labTime.Text = "dd/MM/YYYY HH:mm:ss";
            this._labTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _labSriverStatus
            // 
            this._labSriverStatus.AutoSize = true;
            this._labSriverStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labSriverStatus.Location = new System.Drawing.Point(132, 774);
            this._labSriverStatus.Name = "_labSriverStatus";
            this._labSriverStatus.Size = new System.Drawing.Size(98, 20);
            this._labSriverStatus.TabIndex = 5;
            this._labSriverStatus.Text = "Driver status";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 774);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "TT kết nối Driver:";
            // 
            // _btnArrow
            // 
            this._btnArrow.Location = new System.Drawing.Point(12, 12);
            this._btnArrow.Name = "_btnArrow";
            this._btnArrow.Size = new System.Drawing.Size(82, 30);
            this._btnArrow.TabIndex = 8;
            this._btnArrow.Text = "AROW";
            this._btnArrow.UseVisualStyleBackColor = true;
            // 
            // _btnApple
            // 
            this._btnApple.Location = new System.Drawing.Point(113, 12);
            this._btnApple.Name = "_btnApple";
            this._btnApple.Size = new System.Drawing.Size(82, 30);
            this._btnApple.TabIndex = 9;
            this._btnApple.Text = "APPLE";
            this._btnApple.UseVisualStyleBackColor = true;
            // 
            // _btnSettings
            // 
            this._btnSettings.Location = new System.Drawing.Point(214, 12);
            this._btnSettings.Name = "_btnSettings";
            this._btnSettings.Size = new System.Drawing.Size(82, 30);
            this._btnSettings.TabIndex = 10;
            this._btnSettings.Text = "CÀI ĐẶT";
            this._btnSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._labValueS1);
            this.groupBox1.Controls.Add(this._labUnitS1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(22, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 214);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SENSOR - 1";
            // 
            // _labValueS1
            // 
            this._labValueS1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._labValueS1.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labValueS1.ForeColor = System.Drawing.Color.Black;
            this._labValueS1.Location = new System.Drawing.Point(19, 69);
            this._labValueS1.Name = "_labValueS1";
            this._labValueS1.Size = new System.Drawing.Size(275, 125);
            this._labValueS1.TabIndex = 1;
            this._labValueS1.Text = "12.234";
            this._labValueS1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _labUnitS1
            // 
            this._labUnitS1.BackColor = System.Drawing.SystemColors.Control;
            this._labUnitS1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._labUnitS1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labUnitS1.ForeColor = System.Drawing.Color.Red;
            this._labUnitS1.Location = new System.Drawing.Point(19, 34);
            this._labUnitS1.Name = "_labUnitS1";
            this._labUnitS1.Size = new System.Drawing.Size(275, 35);
            this._labUnitS1.TabIndex = 0;
            this._labUnitS1.Text = "label1";
            this._labUnitS1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._labValueS2);
            this.groupBox2.Controls.Add(this._labUnitS2);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(364, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(318, 214);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SENSOR - 2";
            // 
            // _labValueS2
            // 
            this._labValueS2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._labValueS2.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labValueS2.ForeColor = System.Drawing.Color.Black;
            this._labValueS2.Location = new System.Drawing.Point(19, 69);
            this._labValueS2.Name = "_labValueS2";
            this._labValueS2.Size = new System.Drawing.Size(275, 125);
            this._labValueS2.TabIndex = 1;
            this._labValueS2.Text = "12.234";
            this._labValueS2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _labUnitS2
            // 
            this._labUnitS2.BackColor = System.Drawing.SystemColors.Control;
            this._labUnitS2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._labUnitS2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labUnitS2.ForeColor = System.Drawing.Color.Red;
            this._labUnitS2.Location = new System.Drawing.Point(19, 34);
            this._labUnitS2.Name = "_labUnitS2";
            this._labUnitS2.Size = new System.Drawing.Size(275, 35);
            this._labUnitS2.TabIndex = 0;
            this._labUnitS2.Text = "label1";
            this._labUnitS2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this._labValueS3);
            this.groupBox3.Controls.Add(this._labUnitS3);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(706, 40);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(318, 214);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "SENSOR - 3";
            // 
            // _labValueS3
            // 
            this._labValueS3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._labValueS3.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labValueS3.ForeColor = System.Drawing.Color.Black;
            this._labValueS3.Location = new System.Drawing.Point(19, 69);
            this._labValueS3.Name = "_labValueS3";
            this._labValueS3.Size = new System.Drawing.Size(275, 125);
            this._labValueS3.TabIndex = 1;
            this._labValueS3.Text = "12.234";
            this._labValueS3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _labUnitS3
            // 
            this._labUnitS3.BackColor = System.Drawing.SystemColors.Control;
            this._labUnitS3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._labUnitS3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labUnitS3.ForeColor = System.Drawing.Color.Red;
            this._labUnitS3.Location = new System.Drawing.Point(19, 34);
            this._labUnitS3.Name = "_labUnitS3";
            this._labUnitS3.Size = new System.Drawing.Size(275, 35);
            this._labUnitS3.TabIndex = 0;
            this._labUnitS3.Text = "label1";
            this._labUnitS3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _groupBoxArrowZone
            // 
            this._groupBoxArrowZone.Controls.Add(this._labArrowValueFinal);
            this._groupBoxArrowZone.Controls.Add(this._labArrowZone);
            this._groupBoxArrowZone.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this._groupBoxArrowZone.Location = new System.Drawing.Point(22, 38);
            this._groupBoxArrowZone.Name = "_groupBoxArrowZone";
            this._groupBoxArrowZone.Size = new System.Drawing.Size(318, 345);
            this._groupBoxArrowZone.TabIndex = 12;
            this._groupBoxArrowZone.TabStop = false;
            this._groupBoxArrowZone.Text = "ZONE";
            // 
            // _labArrowValueFinal
            // 
            this._labArrowValueFinal.BackColor = System.Drawing.SystemColors.Control;
            this._labArrowValueFinal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._labArrowValueFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this._labArrowValueFinal.ForeColor = System.Drawing.Color.Black;
            this._labArrowValueFinal.Location = new System.Drawing.Point(19, 34);
            this._labArrowValueFinal.Name = "_labArrowValueFinal";
            this._labArrowValueFinal.Size = new System.Drawing.Size(275, 117);
            this._labArrowValueFinal.TabIndex = 3;
            this._labArrowValueFinal.Text = "Sensors: SENSOR_1-SENSOR_2-SENSOR_3\\nData lớn nhất\\nGiá trị sensor phân zone: 10." +
    "234";
            this._labArrowValueFinal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _labArrowZone
            // 
            this._labArrowZone.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this._labArrowZone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._labArrowZone.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labArrowZone.ForeColor = System.Drawing.Color.Black;
            this._labArrowZone.Location = new System.Drawing.Point(19, 151);
            this._labArrowZone.Name = "_labArrowZone";
            this._labArrowZone.Size = new System.Drawing.Size(275, 177);
            this._labArrowZone.TabIndex = 1;
            this._labArrowZone.Text = "VSS";
            this._labArrowZone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _groupBoxArrowResult
            // 
            this._groupBoxArrowResult.Controls.Add(this._labArrowValueHead);
            this._groupBoxArrowResult.Controls.Add(this._labArrowResultHead);
            this._groupBoxArrowResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this._groupBoxArrowResult.Location = new System.Drawing.Point(703, 38);
            this._groupBoxArrowResult.Name = "_groupBoxArrowResult";
            this._groupBoxArrowResult.Size = new System.Drawing.Size(318, 345);
            this._groupBoxArrowResult.TabIndex = 13;
            this._groupBoxArrowResult.TabStop = false;
            this._groupBoxArrowResult.Text = "ĐẦU THẲNG";
            // 
            // _labArrowValueHead
            // 
            this._labArrowValueHead.BackColor = System.Drawing.SystemColors.Control;
            this._labArrowValueHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._labArrowValueHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this._labArrowValueHead.ForeColor = System.Drawing.Color.Red;
            this._labArrowValueHead.Location = new System.Drawing.Point(19, 34);
            this._labArrowValueHead.Name = "_labArrowValueHead";
            this._labArrowValueHead.Size = new System.Drawing.Size(275, 117);
            this._labArrowValueHead.TabIndex = 4;
            this._labArrowValueHead.Text = "label3";
            this._labArrowValueHead.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _labArrowResultHead
            // 
            this._labArrowResultHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._labArrowResultHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labArrowResultHead.ForeColor = System.Drawing.Color.Black;
            this._labArrowResultHead.Location = new System.Drawing.Point(19, 151);
            this._labArrowResultHead.Name = "_labArrowResultHead";
            this._labArrowResultHead.Size = new System.Drawing.Size(275, 177);
            this._labArrowResultHead.TabIndex = 1;
            this._labArrowResultHead.Text = "OK";
            this._labArrowResultHead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _groupBoxApple
            // 
            this._groupBoxApple.Controls.Add(this._labAppleValueFinal);
            this._groupBoxApple.Controls.Add(this._labAppleResult);
            this._groupBoxApple.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this._groupBoxApple.Location = new System.Drawing.Point(361, 38);
            this._groupBoxApple.Name = "_groupBoxApple";
            this._groupBoxApple.Size = new System.Drawing.Size(318, 345);
            this._groupBoxApple.TabIndex = 14;
            this._groupBoxApple.TabStop = false;
            this._groupBoxApple.Text = "ZONE";
            // 
            // _labAppleValueFinal
            // 
            this._labAppleValueFinal.BackColor = System.Drawing.SystemColors.Control;
            this._labAppleValueFinal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._labAppleValueFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this._labAppleValueFinal.ForeColor = System.Drawing.Color.Red;
            this._labAppleValueFinal.Location = new System.Drawing.Point(19, 34);
            this._labAppleValueFinal.Name = "_labAppleValueFinal";
            this._labAppleValueFinal.Size = new System.Drawing.Size(275, 117);
            this._labAppleValueFinal.TabIndex = 2;
            this._labAppleValueFinal.Text = "label1";
            this._labAppleValueFinal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _labAppleResult
            // 
            this._labAppleResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._labAppleResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labAppleResult.ForeColor = System.Drawing.Color.Black;
            this._labAppleResult.Location = new System.Drawing.Point(19, 151);
            this._labAppleResult.Name = "_labAppleResult";
            this._labAppleResult.Size = new System.Drawing.Size(275, 177);
            this._labAppleResult.TabIndex = 1;
            this._labAppleResult.Text = "OK";
            this._labAppleResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.groupBox4.Location = new System.Drawing.Point(12, 59);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1037, 271);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "GIÁ TRỊ SENSOR";
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.groupBox5.Controls.Add(this._groupBoxApple);
            this.groupBox5.Controls.Add(this._groupBoxArrowZone);
            this.groupBox5.Controls.Add(this._groupBoxArrowResult);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.groupBox5.Location = new System.Drawing.Point(15, 353);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1037, 405);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "KẾT QUẢ ĐO";
            // 
            // _cbSelectConfig
            // 
            this._cbSelectConfig.FormattingEnabled = true;
            this._cbSelectConfig.Location = new System.Drawing.Point(823, 17);
            this._cbSelectConfig.Name = "_cbSelectConfig";
            this._cbSelectConfig.Size = new System.Drawing.Size(226, 21);
            this._cbSelectConfig.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(636, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "Chọn cấu hình để đo:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 804);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._cbSelectConfig);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this._btnSettings);
            this.Controls.Add(this._btnApple);
            this.Controls.Add(this._btnArrow);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._labSriverStatus);
            this.Controls.Add(this._labTime);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Measurement App";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this._groupBoxArrowZone.ResumeLayout(false);
            this._groupBoxArrowResult.ResumeLayout(false);
            this._groupBoxApple.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label _labTime;
        private System.Windows.Forms.Label _labSriverStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button _btnArrow;
        private System.Windows.Forms.Button _btnApple;
        private System.Windows.Forms.Button _btnSettings;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label _labUnitS1;
        private System.Windows.Forms.Label _labValueS1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label _labValueS2;
        private System.Windows.Forms.Label _labUnitS2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label _labValueS3;
        private System.Windows.Forms.Label _labUnitS3;
        private System.Windows.Forms.GroupBox _groupBoxArrowZone;
        private System.Windows.Forms.Label _labArrowZone;
        private System.Windows.Forms.GroupBox _groupBoxArrowResult;
        private System.Windows.Forms.Label _labArrowResultHead;
        private System.Windows.Forms.GroupBox _groupBoxApple;
        private System.Windows.Forms.Label _labAppleResult;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label _labArrowValueFinal;
        private System.Windows.Forms.Label _labArrowValueHead;
        private System.Windows.Forms.Label _labAppleValueFinal;
        private System.Windows.Forms.ComboBox _cbSelectConfig;
        private System.Windows.Forms.Label label1;
    }
}

