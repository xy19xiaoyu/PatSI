namespace PatSI
{
    partial class frmDbConfig
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.listOther = new System.Windows.Forms.ListBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.listQY = new System.Windows.Forms.ListBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.listTime = new System.Windows.Forms.ListBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.listBase = new System.Windows.Forms.ListBox();
            this.groupBox4.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.groupBox9);
            this.groupBox4.Controls.Add(this.groupBox8);
            this.groupBox4.Controls.Add(this.groupBox7);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Location = new System.Drawing.Point(195, 115);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(626, 228);
            this.groupBox4.TabIndex = 38;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "自动标引";
            // 
            // groupBox9
            // 
            this.groupBox9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox9.Controls.Add(this.listOther);
            this.groupBox9.Location = new System.Drawing.Point(473, 22);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox9.Size = new System.Drawing.Size(142, 192);
            this.groupBox9.TabIndex = 41;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "其它信息";
            // 
            // listOther
            // 
            this.listOther.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listOther.FormattingEnabled = true;
            this.listOther.ItemHeight = 12;
            this.listOther.Items.AddRange(new object[] {
            "第一申请人",
            "申请人类型",
            "第一发明人",
            "是否公知技术"});
            this.listOther.Location = new System.Drawing.Point(5, 19);
            this.listOther.Name = "listOther";
            this.listOther.Size = new System.Drawing.Size(132, 168);
            this.listOther.TabIndex = 11;
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox8.Controls.Add(this.listQY);
            this.groupBox8.Location = new System.Drawing.Point(322, 22);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox8.Size = new System.Drawing.Size(142, 192);
            this.groupBox8.TabIndex = 40;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "区域信息";
            // 
            // listQY
            // 
            this.listQY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listQY.FormattingEnabled = true;
            this.listQY.ItemHeight = 12;
            this.listQY.Items.AddRange(new object[] {
            "洲际",
            "国家",
            "省",
            "省(合并计划单列市）",
            "市",
            "区县",
            "区域",
            "是否国外来华",
            "三国",
            "五国"});
            this.listQY.Location = new System.Drawing.Point(5, 19);
            this.listQY.Name = "listQY";
            this.listQY.Size = new System.Drawing.Size(132, 168);
            this.listQY.TabIndex = 10;
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox7.Controls.Add(this.listTime);
            this.groupBox7.Location = new System.Drawing.Point(171, 22);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox7.Size = new System.Drawing.Size(142, 192);
            this.groupBox7.TabIndex = 39;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "时间信息";
            // 
            // listTime
            // 
            this.listTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTime.FormattingEnabled = true;
            this.listTime.ItemHeight = 12;
            this.listTime.Items.AddRange(new object[] {
            "申请年",
            "公开年",
            "授权年",
            "失效年",
            "最早优先权年",
            "公开年差",
            "授权年差",
            "专利年龄"});
            this.listTime.Location = new System.Drawing.Point(5, 19);
            this.listTime.Name = "listTime";
            this.listTime.Size = new System.Drawing.Size(132, 168);
            this.listTime.TabIndex = 9;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox6.Controls.Add(this.listBase);
            this.groupBox6.Location = new System.Drawing.Point(20, 22);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox6.Size = new System.Drawing.Size(142, 192);
            this.groupBox6.TabIndex = 38;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "基础标引";
            // 
            // listBase
            // 
            this.listBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBase.FormattingEnabled = true;
            this.listBase.ItemHeight = 12;
            this.listBase.Items.AddRange(new object[] {
            "专利类型",
            "专利类型(PCT)",
            "主权利要求字数",
            "发明人数量",
            "申请人数量",
            "同族数量",
            "国家数量",
            "申请国",
            "公开国"});
            this.listBase.Location = new System.Drawing.Point(5, 19);
            this.listBase.Name = "listBase";
            this.listBase.Size = new System.Drawing.Size(132, 168);
            this.listBase.TabIndex = 8;
            // 
            // frmDbConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 458);
            this.Controls.Add(this.groupBox4);
            this.Name = "frmDbConfig";
            this.Text = "数据库连接设置";
            this.groupBox4.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.ListBox listOther;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ListBox listQY;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ListBox listTime;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ListBox listBase;

    }
}