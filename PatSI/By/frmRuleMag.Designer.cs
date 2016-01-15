namespace PatSI.By
{
    partial class frmRuleMag
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNewByItem = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxPageSize = new System.Windows.Forms.ComboBox();
            this.btnSY = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPre = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvListData = new System.Windows.Forms.DataGridView();
            this.选择 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Idx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.编辑 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.删除 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListData)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.编辑ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 70);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.编辑ToolStripMenuItem.Text = "编辑";
            this.编辑ToolStripMenuItem.Click += new System.EventHandler(this.编辑ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // btnNewByItem
            // 
            this.btnNewByItem.Location = new System.Drawing.Point(3, 7);
            this.btnNewByItem.Name = "btnNewByItem";
            this.btnNewByItem.Size = new System.Drawing.Size(75, 23);
            this.btnNewByItem.TabIndex = 2;
            this.btnNewByItem.Text = "新建标引项";
            this.btnNewByItem.UseVisualStyleBackColor = true;
            this.btnNewByItem.Click += new System.EventHandler(this.btnNewByItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(329, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "条";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(247, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 16;
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
            this.cbxPageSize.Location = new System.Drawing.Point(280, 10);
            this.cbxPageSize.Name = "cbxPageSize";
            this.cbxPageSize.Size = new System.Drawing.Size(48, 20);
            this.cbxPageSize.TabIndex = 15;
            this.cbxPageSize.SelectedIndexChanged += new System.EventHandler(this.cbxPageSize_SelectedIndexChanged);
            // 
            // btnSY
            // 
            this.btnSY.Location = new System.Drawing.Point(354, 7);
            this.btnSY.Name = "btnSY";
            this.btnSY.Size = new System.Drawing.Size(55, 23);
            this.btnSY.TabIndex = 14;
            this.btnSY.Text = "首页";
            this.btnSY.UseVisualStyleBackColor = true;
            this.btnSY.Click += new System.EventHandler(this.btnSY_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(532, 7);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(55, 23);
            this.btnEnd.TabIndex = 13;
            this.btnEnd.Text = "末页";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(471, 7);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(55, 23);
            this.btnNext.TabIndex = 12;
            this.btnNext.Text = "下一页";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPre
            // 
            this.btnPre.Location = new System.Drawing.Point(415, 7);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(50, 23);
            this.btnPre.TabIndex = 11;
            this.btnPre.Text = "上一页";
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvListData);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.ForeColor = System.Drawing.Color.Blue;
            this.groupBox2.Location = new System.Drawing.Point(0, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(598, 277);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "共XXX.";
            // 
            // dgvListData
            // 
            this.dgvListData.AllowUserToAddRows = false;
            this.dgvListData.AllowUserToDeleteRows = false;
            this.dgvListData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.选择,
            this.Idx,
            this.编辑,
            this.删除});
            this.dgvListData.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvListData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListData.Location = new System.Drawing.Point(3, 17);
            this.dgvListData.Name = "dgvListData";
            this.dgvListData.ReadOnly = true;
            this.dgvListData.RowTemplate.Height = 23;
            this.dgvListData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListData.Size = new System.Drawing.Size(592, 257);
            this.dgvListData.TabIndex = 9;
            this.dgvListData.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvListData_DataBindingComplete);
            // 
            // 选择
            // 
            this.选择.HeaderText = "选择";
            this.选择.Name = "选择";
            this.选择.ReadOnly = true;
            this.选择.Visible = false;
            this.选择.Width = 40;
            // 
            // Idx
            // 
            this.Idx.DataPropertyName = "ID";
            this.Idx.HeaderText = "id";
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
            // frmRuleMag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 313);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxPageSize);
            this.Controls.Add(this.btnSY);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPre);
            this.Controls.Add(this.btnNewByItem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRuleMag";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "标引项维护";
            this.Load += new System.EventHandler(this.frmRuleMag_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNewByItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxPageSize;
        private System.Windows.Forms.Button btnSY;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvListData;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 选择;
        private System.Windows.Forms.DataGridViewTextBoxColumn Idx;
        private System.Windows.Forms.DataGridViewButtonColumn 编辑;
        private System.Windows.Forms.DataGridViewButtonColumn 删除;
    }
}