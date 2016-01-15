namespace PatSI
{
    partial class frmPtList
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnstat = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbZtLst = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSY = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPre = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbHeader = new System.Windows.Forms.CheckBox();
            this.dgvListData = new System.Windows.Forms.DataGridView();
            this.选择 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Idx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.编辑 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.删除 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxOrder = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxPageSize = new System.Windows.Forms.ComboBox();
            this.btnImportSelect = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.标引ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.复制到ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移动到ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnMovePt = new System.Windows.Forms.Button();
            this.btnCopyPt = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListData)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.btnstat);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbZtLst);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(936, 63);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(743, 23);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "数据筛选(&F)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnstat
            // 
            this.btnstat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnstat.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnstat.Location = new System.Drawing.Point(840, 23);
            this.btnstat.Name = "btnstat";
            this.btnstat.Size = new System.Drawing.Size(90, 23);
            this.btnstat.TabIndex = 5;
            this.btnstat.Text = "统计分析(&S)";
            this.btnstat.UseVisualStyleBackColor = true;
            this.btnstat.Click += new System.EventHandler(this.btnstat_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(553, 23);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(90, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "标引规则维护";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(210, 23);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(90, 23);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.Text = "查看";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "专题名称:";
            // 
            // cmbZtLst
            // 
            this.cmbZtLst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZtLst.FormattingEnabled = true;
            this.cmbZtLst.Location = new System.Drawing.Point(64, 26);
            this.cmbZtLst.Name = "cmbZtLst";
            this.cmbZtLst.Size = new System.Drawing.Size(130, 20);
            this.cmbZtLst.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(647, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "设置显示字段";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSY
            // 
            this.btnSY.Location = new System.Drawing.Point(309, 20);
            this.btnSY.Name = "btnSY";
            this.btnSY.Size = new System.Drawing.Size(55, 23);
            this.btnSY.TabIndex = 7;
            this.btnSY.Text = "首页";
            this.btnSY.UseVisualStyleBackColor = true;
            this.btnSY.Click += new System.EventHandler(this.btnSY_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(484, 20);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(55, 23);
            this.btnEnd.TabIndex = 6;
            this.btnEnd.Text = "末页";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(424, 20);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(55, 23);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "下一页";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPre
            // 
            this.btnPre.Location = new System.Drawing.Point(369, 20);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(50, 23);
            this.btnPre.TabIndex = 4;
            this.btnPre.Text = "上一页";
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cbHeader);
            this.groupBox2.Controls.Add(this.dgvListData);
            this.groupBox2.ForeColor = System.Drawing.Color.Blue;
            this.groupBox2.Location = new System.Drawing.Point(0, 131);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(936, 411);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "共XXX条,双击可查看专利的详细信息.";
            // 
            // cbHeader
            // 
            this.cbHeader.AutoSize = true;
            this.cbHeader.Location = new System.Drawing.Point(40, 24);
            this.cbHeader.Name = "cbHeader";
            this.cbHeader.Size = new System.Drawing.Size(48, 16);
            this.cbHeader.TabIndex = 10;
            this.cbHeader.Text = "全选";
            this.cbHeader.UseVisualStyleBackColor = true;
            // 
            // dgvListData
            // 
            this.dgvListData.AllowUserToAddRows = false;
            this.dgvListData.AllowUserToDeleteRows = false;
            this.dgvListData.ColumnHeadersHeight = 25;
            this.dgvListData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvListData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.选择,
            this.Idx,
            this.编辑,
            this.删除});
            this.dgvListData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListData.Location = new System.Drawing.Point(3, 17);
            this.dgvListData.Name = "dgvListData";
            this.dgvListData.ReadOnly = true;
            this.dgvListData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvListData.RowTemplate.Height = 23;
            this.dgvListData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListData.Size = new System.Drawing.Size(930, 391);
            this.dgvListData.TabIndex = 9;
            this.dgvListData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListData_CellClick);
            this.dgvListData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListData_CellContentClick);
            this.dgvListData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListData_CellDoubleClick);
            this.dgvListData.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvListData_CellMouseClick);
            this.dgvListData.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvListData_DataBindingComplete);
            // 
            // 选择
            // 
            this.选择.HeaderText = "   ";
            this.选择.Name = "选择";
            this.选择.ReadOnly = true;
            this.选择.Width = 55;
            // 
            // Idx
            // 
            this.Idx.DataPropertyName = "SID";
            this.Idx.HeaderText = "SID";
            this.Idx.Name = "Idx";
            this.Idx.ReadOnly = true;
            this.Idx.Visible = false;
            // 
            // 编辑
            // 
            this.编辑.HeaderText = "编辑";
            this.编辑.Name = "编辑";
            this.编辑.ReadOnly = true;
            this.编辑.Text = "编辑";
            this.编辑.UseColumnTextForButtonValue = true;
            this.编辑.Visible = false;
            // 
            // 删除
            // 
            this.删除.HeaderText = "删除";
            this.删除.Name = "删除";
            this.删除.ReadOnly = true;
            this.删除.Text = "删除";
            this.删除.UseColumnTextForButtonValue = true;
            this.删除.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.cbxOrder);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.cbxPageSize);
            this.groupBox3.Controls.Add(this.btnSY);
            this.groupBox3.Controls.Add(this.btnEnd);
            this.groupBox3.Controls.Add(this.btnNext);
            this.groupBox3.Controls.Add(this.btnPre);
            this.groupBox3.Location = new System.Drawing.Point(1, 65);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(544, 60);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "排序:";
            // 
            // cbxOrder
            // 
            this.cbxOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOrder.FormattingEnabled = true;
            this.cbxOrder.Items.AddRange(new object[] {
            "申请号降序",
            "申请号升序",
            "申请日降序",
            "申请日升序",
            "公开日降序",
            "公开日升序"});
            this.cbxOrder.Location = new System.Drawing.Point(60, 23);
            this.cbxOrder.Name = "cbxOrder";
            this.cbxOrder.Size = new System.Drawing.Size(133, 20);
            this.cbxOrder.TabIndex = 11;
            this.cbxOrder.SelectedIndexChanged += new System.EventHandler(this.cbxOrder_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(284, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "条";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "每页";
            // 
            // cbxPageSize
            // 
            this.cbxPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPageSize.FormattingEnabled = true;
            this.cbxPageSize.Items.AddRange(new object[] {
            "20",
            "50",
            "100",
            "150",
            "200"});
            this.cbxPageSize.Location = new System.Drawing.Point(235, 23);
            this.cbxPageSize.Name = "cbxPageSize";
            this.cbxPageSize.Size = new System.Drawing.Size(48, 20);
            this.cbxPageSize.TabIndex = 8;
            this.cbxPageSize.SelectedIndexChanged += new System.EventHandler(this.cbxPageSize_SelectedIndexChanged);
            // 
            // btnImportSelect
            // 
            this.btnImportSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportSelect.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnImportSelect.Location = new System.Drawing.Point(292, 20);
            this.btnImportSelect.Name = "btnImportSelect";
            this.btnImportSelect.Size = new System.Drawing.Size(90, 23);
            this.btnImportSelect.TabIndex = 7;
            this.btnImportSelect.Text = "导出所选(&O)";
            this.btnImportSelect.UseVisualStyleBackColor = true;
            this.btnImportSelect.Click += new System.EventHandler(this.btnImportSelect_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看ToolStripMenuItem,
            this.标引ToolStripMenuItem,
            this.toolStripMenuItem4,
            this.复制到ToolStripMenuItem,
            this.移动到ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 120);
            // 
            // 查看ToolStripMenuItem
            // 
            this.查看ToolStripMenuItem.Name = "查看ToolStripMenuItem";
            this.查看ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.查看ToolStripMenuItem.Text = "查看";
            this.查看ToolStripMenuItem.Click += new System.EventHandler(this.查看ToolStripMenuItem_Click);
            // 
            // 标引ToolStripMenuItem
            // 
            this.标引ToolStripMenuItem.Name = "标引ToolStripMenuItem";
            this.标引ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.标引ToolStripMenuItem.Text = "标引";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(121, 6);
            // 
            // 复制到ToolStripMenuItem
            // 
            this.复制到ToolStripMenuItem.Name = "复制到ToolStripMenuItem";
            this.复制到ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.复制到ToolStripMenuItem.Text = "复制到...";
            this.复制到ToolStripMenuItem.Click += new System.EventHandler(this.复制到ToolStripMenuItem_Click);
            // 
            // 移动到ToolStripMenuItem
            // 
            this.移动到ToolStripMenuItem.Name = "移动到ToolStripMenuItem";
            this.移动到ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.移动到ToolStripMenuItem.Text = "移动到...";
            this.移动到ToolStripMenuItem.Click += new System.EventHandler(this.移动到ToolStripMenuItem_Click_1);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnDel);
            this.groupBox4.Controls.Add(this.btnMovePt);
            this.groupBox4.Controls.Add(this.btnCopyPt);
            this.groupBox4.Controls.Add(this.btnImportSelect);
            this.groupBox4.Location = new System.Drawing.Point(546, 65);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(390, 60);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(197, 20);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(90, 23);
            this.btnDel.TabIndex = 10;
            this.btnDel.Text = "删除选中";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnMovePt
            // 
            this.btnMovePt.Location = new System.Drawing.Point(101, 20);
            this.btnMovePt.Name = "btnMovePt";
            this.btnMovePt.Size = new System.Drawing.Size(90, 23);
            this.btnMovePt.TabIndex = 9;
            this.btnMovePt.Text = "移动到...";
            this.btnMovePt.UseVisualStyleBackColor = true;
            this.btnMovePt.Click += new System.EventHandler(this.btnMovePt_Click);
            // 
            // btnCopyPt
            // 
            this.btnCopyPt.Location = new System.Drawing.Point(7, 20);
            this.btnCopyPt.Name = "btnCopyPt";
            this.btnCopyPt.Size = new System.Drawing.Size(90, 23);
            this.btnCopyPt.TabIndex = 8;
            this.btnCopyPt.Text = "复制到...";
            this.btnCopyPt.UseVisualStyleBackColor = true;
            this.btnCopyPt.Click += new System.EventHandler(this.btnCopyPt_Click);
            // 
            // frmPtList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 542);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmPtList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据列表";
            this.Load += new System.EventHandler(this.frmPtList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListData)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvListData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbZtLst;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.Button btnSY;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbxPageSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxOrder;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 标引ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.Button btnstat;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem 复制到ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移动到ToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbHeader;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 选择;
        private System.Windows.Forms.DataGridViewTextBoxColumn Idx;
        private System.Windows.Forms.DataGridViewButtonColumn 编辑;
        private System.Windows.Forms.DataGridViewButtonColumn 删除;
        private System.Windows.Forms.Button btnImportSelect;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnMovePt;
        private System.Windows.Forms.Button btnCopyPt;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}