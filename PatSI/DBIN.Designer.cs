namespace PatSI
{
    partial class DBIN
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupTmplate = new System.Windows.Forms.GroupBox();
            this.txtTemplate = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnFileBrower = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cmbZTList = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssProcess = new System.Windows.Forms.ToolStripProgressBar();
            this.tssMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnImport = new System.Windows.Forms.Button();
            this.panTemplate = new System.Windows.Forms.Panel();
            this.dgwtemplate = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dbtype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filetype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rbSkip = new System.Windows.Forms.RadioButton();
            this.rbOverWrite = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupTmplate.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panTemplate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwtemplate)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupTmplate
            // 
            this.groupTmplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupTmplate.Controls.Add(this.txtTemplate);
            this.groupTmplate.Controls.Add(this.button1);
            this.groupTmplate.Location = new System.Drawing.Point(3, 140);
            this.groupTmplate.Name = "groupTmplate";
            this.groupTmplate.Size = new System.Drawing.Size(610, 64);
            this.groupTmplate.TabIndex = 36;
            this.groupTmplate.TabStop = false;
            this.groupTmplate.Text = "模板";
            // 
            // txtTemplate
            // 
            this.txtTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTemplate.Location = new System.Drawing.Point(49, 20);
            this.txtTemplate.Name = "txtTemplate";
            this.txtTemplate.Size = new System.Drawing.Size(473, 21);
            this.txtTemplate.TabIndex = 7;
            this.txtTemplate.TextChanged += new System.EventHandler(this.txtTemplate_TextChanged);
            this.txtTemplate.Enter += new System.EventHandler(this.txtTemplate_Enter);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(525, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "浏览";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtFileName);
            this.groupBox2.Controls.Add(this.btnFileBrower);
            this.groupBox2.Location = new System.Drawing.Point(3, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(610, 64);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据源";
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.Location = new System.Drawing.Point(49, 20);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(473, 21);
            this.txtFileName.TabIndex = 4;
            this.txtFileName.TextChanged += new System.EventHandler(this.txtFileName_TextChanged);
            // 
            // btnFileBrower
            // 
            this.btnFileBrower.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFileBrower.Location = new System.Drawing.Point(525, 19);
            this.btnFileBrower.Name = "btnFileBrower";
            this.btnFileBrower.Size = new System.Drawing.Size(75, 23);
            this.btnFileBrower.TabIndex = 5;
            this.btnFileBrower.Text = "浏览";
            this.btnFileBrower.UseVisualStyleBackColor = true;
            this.btnFileBrower.Click += new System.EventHandler(this.btnFileBrower_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.cmbZTList);
            this.groupBox5.Location = new System.Drawing.Point(3, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(610, 63);
            this.groupBox5.TabIndex = 33;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "专题库";
            // 
            // cmbZTList
            // 
            this.cmbZTList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbZTList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZTList.FormattingEnabled = true;
            this.cmbZTList.Location = new System.Drawing.Point(49, 22);
            this.cmbZTList.Name = "cmbZTList";
            this.cmbZTList.Size = new System.Drawing.Size(545, 20);
            this.cmbZTList.TabIndex = 0;
            this.cmbZTList.SelectedIndexChanged += new System.EventHandler(this.cmbZTList_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssStatus,
            this.toolStripStatusLabel1,
            this.StatusMsg,
            this.toolStripStatusLabel2,
            this.tssProcess,
            this.tssMsg});
            this.statusStrip1.Location = new System.Drawing.Point(0, 312);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(618, 22);
            this.statusStrip1.TabIndex = 40;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssStatus
            // 
            this.tssStatus.AutoSize = false;
            this.tssStatus.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.tssStatus.Name = "tssStatus";
            this.tssStatus.Size = new System.Drawing.Size(67, 17);
            this.tssStatus.Text = "准备";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Enabled = false;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel1.Text = "|";
            // 
            // StatusMsg
            // 
            this.StatusMsg.AutoSize = false;
            this.StatusMsg.Name = "StatusMsg";
            this.StatusMsg.Size = new System.Drawing.Size(150, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // tssProcess
            // 
            this.tssProcess.Name = "tssProcess";
            this.tssProcess.Size = new System.Drawing.Size(300, 16);
            // 
            // tssMsg
            // 
            this.tssMsg.AutoSize = false;
            this.tssMsg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tssMsg.Name = "tssMsg";
            this.tssMsg.Size = new System.Drawing.Size(45, 17);
            this.tssMsg.Text = "0%";
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Location = new System.Drawing.Point(531, 274);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 39;
            this.btnImport.Text = "导入(&I)";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // panTemplate
            // 
            this.panTemplate.Controls.Add(this.dgwtemplate);
            this.panTemplate.Location = new System.Drawing.Point(1, 900);
            this.panTemplate.Name = "panTemplate";
            this.panTemplate.Padding = new System.Windows.Forms.Padding(3);
            this.panTemplate.Size = new System.Drawing.Size(492, 134);
            this.panTemplate.TabIndex = 42;
            // 
            // dgwtemplate
            // 
            this.dgwtemplate.AllowUserToAddRows = false;
            this.dgwtemplate.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwtemplate.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgwtemplate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwtemplate.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.path,
            this.name,
            this.dbtype,
            this.filetype});
            this.dgwtemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgwtemplate.Location = new System.Drawing.Point(3, 3);
            this.dgwtemplate.Name = "dgwtemplate";
            this.dgwtemplate.ReadOnly = true;
            this.dgwtemplate.RowHeadersVisible = false;
            this.dgwtemplate.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgwtemplate.RowTemplate.Height = 23;
            this.dgwtemplate.Size = new System.Drawing.Size(486, 128);
            this.dgwtemplate.TabIndex = 0;
            this.dgwtemplate.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.list_template_CellClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // path
            // 
            this.path.DataPropertyName = "path";
            this.path.HeaderText = "模板地址";
            this.path.Name = "path";
            this.path.ReadOnly = true;
            this.path.Visible = false;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "模板名称";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 285;
            // 
            // dbtype
            // 
            this.dbtype.DataPropertyName = "dbtype";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dbtype.DefaultCellStyle = dataGridViewCellStyle2;
            this.dbtype.HeaderText = "数据类型";
            this.dbtype.Name = "dbtype";
            this.dbtype.ReadOnly = true;
            // 
            // filetype
            // 
            this.filetype.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.filetype.DataPropertyName = "filetype";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.filetype.DefaultCellStyle = dataGridViewCellStyle3;
            this.filetype.HeaderText = "文件类型";
            this.filetype.Name = "filetype";
            this.filetype.ReadOnly = true;
            // 
            // rbSkip
            // 
            this.rbSkip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rbSkip.AutoSize = true;
            this.rbSkip.Checked = true;
            this.rbSkip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbSkip.Location = new System.Drawing.Point(247, 32);
            this.rbSkip.Name = "rbSkip";
            this.rbSkip.Size = new System.Drawing.Size(47, 16);
            this.rbSkip.TabIndex = 43;
            this.rbSkip.TabStop = true;
            this.rbSkip.Text = "跳过";
            this.rbSkip.UseVisualStyleBackColor = true;
            this.rbSkip.CheckedChanged += new System.EventHandler(this.rbSkip_CheckedChanged);
            // 
            // rbOverWrite
            // 
            this.rbOverWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rbOverWrite.AutoSize = true;
            this.rbOverWrite.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbOverWrite.Location = new System.Drawing.Point(316, 33);
            this.rbOverWrite.Name = "rbOverWrite";
            this.rbOverWrite.Size = new System.Drawing.Size(47, 16);
            this.rbOverWrite.TabIndex = 44;
            this.rbOverWrite.Text = "覆盖";
            this.rbOverWrite.UseVisualStyleBackColor = true;
            this.rbOverWrite.CheckedChanged += new System.EventHandler(this.rbOverWrite_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbSkip);
            this.groupBox1.Controls.Add(this.rbOverWrite);
            this.groupBox1.Location = new System.Drawing.Point(4, 205);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(610, 64);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "重复数据";
            // 
            // DBIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 334);
            this.Controls.Add(this.panTemplate);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupTmplate);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DBIN";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据导入";
            this.Load += new System.EventHandler(this.DBIN_Load);
            this.groupTmplate.ResumeLayout(false);
            this.groupTmplate.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panTemplate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwtemplate)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupTmplate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cmbZTList;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnFileBrower;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssMsg;
        private System.Windows.Forms.ToolStripProgressBar tssProcess;
        private System.Windows.Forms.ToolStripStatusLabel StatusMsg;
        private System.Windows.Forms.ToolStripStatusLabel tssStatus;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Panel panTemplate;
        private System.Windows.Forms.DataGridView dgwtemplate;
        private System.Windows.Forms.TextBox txtTemplate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn path;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn dbtype;
        private System.Windows.Forms.DataGridViewTextBoxColumn filetype;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.RadioButton rbSkip;
        private System.Windows.Forms.RadioButton rbOverWrite;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}