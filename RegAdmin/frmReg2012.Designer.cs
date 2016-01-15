namespace RegAdmin
{
    partial class frmReg2012
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReg2012));
            this.radYP = new System.Windows.Forms.RadioButton();
            this.radCpu = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnKeyFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnShowHis = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txbUserInfo = new System.Windows.Forms.RichTextBox();
            this.txbJqh = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.txbYzm = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dtPkSqEnd = new System.Windows.Forms.DateTimePicker();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // radYP
            // 
            this.radYP.AutoSize = true;
            this.radYP.Location = new System.Drawing.Point(636, 29);
            this.radYP.Name = "radYP";
            this.radYP.Size = new System.Drawing.Size(59, 16);
            this.radYP.TabIndex = 4;
            this.radYP.TabStop = true;
            this.radYP.Text = "硬盘号";
            this.radYP.UseVisualStyleBackColor = true;
            this.radYP.Visible = false;
            this.radYP.CheckedChanged += new System.EventHandler(this.radYP_CheckedChanged);
            // 
            // radCpu
            // 
            this.radCpu.AutoSize = true;
            this.radCpu.Location = new System.Drawing.Point(636, 65);
            this.radCpu.Name = "radCpu";
            this.radCpu.Size = new System.Drawing.Size(53, 16);
            this.radCpu.TabIndex = 5;
            this.radCpu.TabStop = true;
            this.radCpu.Text = "CPU号";
            this.radCpu.UseVisualStyleBackColor = true;
            this.radCpu.Visible = false;
            this.radCpu.CheckedChanged += new System.EventHandler(this.radCpu_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtPkSqEnd);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.btnKeyFile);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btnShowHis);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txbUserInfo);
            this.groupBox3.Controls.Add(this.txbJqh);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.btnClearAll);
            this.groupBox3.Controls.Add(this.txbYzm);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(535, 260);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            // 
            // btnKeyFile
            // 
            this.btnKeyFile.Location = new System.Drawing.Point(405, 216);
            this.btnKeyFile.Name = "btnKeyFile";
            this.btnKeyFile.Size = new System.Drawing.Size(116, 29);
            this.btnKeyFile.TabIndex = 14;
            this.btnKeyFile.Text = "生成Key文件...";
            this.btnKeyFile.UseVisualStyleBackColor = true;
            this.btnKeyFile.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "用户标识：";
            // 
            // btnShowHis
            // 
            this.btnShowHis.Location = new System.Drawing.Point(92, 217);
            this.btnShowHis.Name = "btnShowHis";
            this.btnShowHis.Size = new System.Drawing.Size(75, 28);
            this.btnShowHis.TabIndex = 13;
            this.btnShowHis.Text = "查看记录";
            this.btnShowHis.UseVisualStyleBackColor = true;
            this.btnShowHis.Click += new System.EventHandler(this.btnShowHis_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "机器号：";
            // 
            // txbUserInfo
            // 
            this.txbUserInfo.Location = new System.Drawing.Point(71, 21);
            this.txbUserInfo.Multiline = false;
            this.txbUserInfo.Name = "txbUserInfo";
            this.txbUserInfo.Size = new System.Drawing.Size(183, 21);
            this.txbUserInfo.TabIndex = 12;
            this.txbUserInfo.Text = "";
            // 
            // txbJqh
            // 
            this.txbJqh.Location = new System.Drawing.Point(71, 57);
            this.txbJqh.Multiline = false;
            this.txbJqh.Name = "txbJqh";
            this.txbJqh.Size = new System.Drawing.Size(450, 46);
            this.txbJqh.TabIndex = 1;
            this.txbJqh.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "注册码：";
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(179, 216);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(75, 29);
            this.btnClearAll.TabIndex = 9;
            this.btnClearAll.Text = "清除内容";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // txbYzm
            // 
            this.txbYzm.Location = new System.Drawing.Point(71, 118);
            this.txbYzm.Multiline = false;
            this.txbYzm.Name = "txbYzm";
            this.txbYzm.Size = new System.Drawing.Size(450, 78);
            this.txbYzm.TabIndex = 3;
            this.txbYzm.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(270, 215);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 29);
            this.button1.TabIndex = 6;
            this.button1.Text = "生成并复制注册码";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(268, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "授权截止日期：";
            // 
            // dtPkSqEnd
            // 
            this.dtPkSqEnd.Location = new System.Drawing.Point(353, 21);
            this.dtPkSqEnd.Name = "dtPkSqEnd";
            this.dtPkSqEnd.Size = new System.Drawing.Size(168, 21);
            this.dtPkSqEnd.TabIndex = 16;
            // 
            // frmReg2012
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 260);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.radCpu);
            this.Controls.Add(this.radYP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReg2012";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "生成注册码";
            this.Load += new System.EventHandler(this.frmReg2012_Load);
            this.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radYP;
        private System.Windows.Forms.RadioButton radCpu;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnKeyFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnShowHis;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txbUserInfo;
        private System.Windows.Forms.RichTextBox txbJqh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.RichTextBox txbYzm;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtPkSqEnd;
    }
}

