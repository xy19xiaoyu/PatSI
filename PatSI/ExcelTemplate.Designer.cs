namespace PatSI
{
    partial class ExcelTemplate
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkEPO_PN = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkEPO_AN = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkWPI_PN = new System.Windows.Forms.CheckBox();
            this.chkWPI_AN = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTemple = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.TextBox();
            this.syscol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExcelCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.splitChar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.syscolumnname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(825, 537);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.syscol,
            this.ExcelCol,
            this.splitChar,
            this.syscolumnname});
            this.dataGridView1.Location = new System.Drawing.Point(6, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(446, 481);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DataSourceChanged += new System.EventHandler(this.dataGridView1_DataSourceChanged);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Location = new System.Drawing.Point(458, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(522, 481);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "复杂数据格式设置";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkEPO_PN);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.chkEPO_AN);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.chkWPI_PN);
            this.panel1.Controls.Add(this.chkWPI_AN);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(516, 461);
            this.panel1.TabIndex = 0;
            // 
            // chkEPO_PN
            // 
            this.chkEPO_PN.AutoSize = true;
            this.chkEPO_PN.Enabled = false;
            this.chkEPO_PN.Location = new System.Drawing.Point(32, 202);
            this.chkEPO_PN.Name = "chkEPO_PN";
            this.chkEPO_PN.Size = new System.Drawing.Size(378, 16);
            this.chkEPO_PN.TabIndex = 12;
            this.chkEPO_PN.Text = "公开号字段为EPODOC原始数据格式,从公开号列提取公开号、公开日";
            this.chkEPO_PN.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(53, 224);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "e.g.: CA2861410 A1 20130808";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(53, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "e.g.: CA20132861410 20130111";
            // 
            // chkEPO_AN
            // 
            this.chkEPO_AN.AutoSize = true;
            this.chkEPO_AN.Enabled = false;
            this.chkEPO_AN.Location = new System.Drawing.Point(32, 162);
            this.chkEPO_AN.Name = "chkEPO_AN";
            this.chkEPO_AN.Size = new System.Drawing.Size(378, 16);
            this.chkEPO_AN.TabIndex = 9;
            this.chkEPO_AN.Text = "申请号字段为EPODOC原始数据格式,从申请号列提取申请号、申请日";
            this.chkEPO_AN.UseVisualStyleBackColor = true;
            this.chkEPO_AN.CheckedChanged += new System.EventHandler(this.chkEPO_AN_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(53, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(443, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "e.g.: US2015098795 A1 20150409 DW201527 WO2015052175 A1 20150416 DW201527";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(53, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(317, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "e.g.: US20140507619 20141006; WO2014EP71424 20141007";
            // 
            // chkWPI_PN
            // 
            this.chkWPI_PN.AutoSize = true;
            this.chkWPI_PN.Enabled = false;
            this.chkWPI_PN.Location = new System.Drawing.Point(32, 116);
            this.chkWPI_PN.Name = "chkWPI_PN";
            this.chkWPI_PN.Size = new System.Drawing.Size(360, 16);
            this.chkWPI_PN.TabIndex = 6;
            this.chkWPI_PN.Text = "公开号字段为WPI原始数据格式,从公开号列提取公开号、公开日";
            this.chkWPI_PN.UseVisualStyleBackColor = true;
            this.chkWPI_PN.CheckedChanged += new System.EventHandler(this.chkWPI_PN_CheckedChanged);
            // 
            // chkWPI_AN
            // 
            this.chkWPI_AN.AutoSize = true;
            this.chkWPI_AN.Enabled = false;
            this.chkWPI_AN.Location = new System.Drawing.Point(32, 76);
            this.chkWPI_AN.Name = "chkWPI_AN";
            this.chkWPI_AN.Size = new System.Drawing.Size(360, 16);
            this.chkWPI_AN.TabIndex = 5;
            this.chkWPI_AN.Text = "申请号字段为WPI原始数据格式,从申请号列提取申请号、申请日";
            this.chkWPI_AN.UseVisualStyleBackColor = true;
            this.chkWPI_AN.CheckedChanged += new System.EventHandler(this.chkWPI_AN_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(6, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(986, 507);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "导入字段设置";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(212, 541);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "模板名称：";
            // 
            // txtTemple
            // 
            this.txtTemple.Location = new System.Drawing.Point(281, 537);
            this.txtTemple.Name = "txtTemple";
            this.txtTemple.Size = new System.Drawing.Size(538, 21);
            this.txtTemple.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(12, 541);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "数据类型：";
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(905, 537);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 18;
            this.button4.Text = "上一步";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // cmbType
            // 
            this.cmbType.Location = new System.Drawing.Point(79, 537);
            this.cmbType.Name = "cmbType";
            this.cmbType.ReadOnly = true;
            this.cmbType.Size = new System.Drawing.Size(127, 21);
            this.cmbType.TabIndex = 19;
            // 
            // syscol
            // 
            this.syscol.DataPropertyName = "ExcelColumnName";
            this.syscol.HeaderText = "Excel列";
            this.syscol.Name = "syscol";
            // 
            // ExcelCol
            // 
            this.ExcelCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ExcelCol.HeaderText = "系统数据项";
            this.ExcelCol.Items.AddRange(new object[] {
            "申请号",
            "申请日",
            "公开号",
            "公开日",
            "授权号",
            "授权日",
            "专利名称",
            "分类号",
            "",
            ""});
            this.ExcelCol.Name = "ExcelCol";
            // 
            // splitChar
            // 
            this.splitChar.DataPropertyName = "SplitChar";
            this.splitChar.HeaderText = "分割符";
            this.splitChar.Name = "splitChar";
            this.splitChar.Visible = false;
            // 
            // syscolumnname
            // 
            this.syscolumnname.DataPropertyName = "SystemColumnShowName";
            this.syscolumnname.HeaderText = "ExcelColumnName";
            this.syscolumnname.Name = "syscolumnname";
            this.syscolumnname.Visible = false;
            // 
            // ExcelTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 570);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtTemple);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Name = "ExcelTemplate";
            this.Text = "Excel导入模板";
            this.Load += new System.EventHandler(this.ExcelTemplate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkWPI_PN;
        private System.Windows.Forms.CheckBox chkWPI_AN;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkEPO_AN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkEPO_PN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTemple;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox cmbType;
        private System.Windows.Forms.DataGridViewTextBoxColumn syscol;
        private System.Windows.Forms.DataGridViewComboBoxColumn ExcelCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn splitChar;
        private System.Windows.Forms.DataGridViewTextBoxColumn syscolumnname;
    }
}