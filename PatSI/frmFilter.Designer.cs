namespace PatSI
{
    partial class frmFilter
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbLuoJi = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.cmbvalue = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbFilterType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbColName = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.luoji = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Colopererator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tablename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tabl_ColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sql = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.lab_sum = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSubMit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmbLuoJi);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.cmbvalue);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbFilterType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbColName);
            this.groupBox1.Location = new System.Drawing.Point(9, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(890, 57);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据筛选";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // cmbLuoJi
            // 
            this.cmbLuoJi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLuoJi.FormattingEnabled = true;
            this.cmbLuoJi.Items.AddRange(new object[] {
            "并且",
            "或者"});
            this.cmbLuoJi.Location = new System.Drawing.Point(79, 24);
            this.cmbLuoJi.Name = "cmbLuoJi";
            this.cmbLuoJi.Size = new System.Drawing.Size(84, 20);
            this.cmbLuoJi.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "逻辑关系";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(766, 23);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "添加筛选条件(&A)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmbvalue
            // 
            this.cmbvalue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbvalue.FormattingEnabled = true;
            this.cmbvalue.Location = new System.Drawing.Point(585, 23);
            this.cmbvalue.Name = "cmbvalue";
            this.cmbvalue.Size = new System.Drawing.Size(157, 20);
            this.cmbvalue.TabIndex = 16;
            this.cmbvalue.Visible = false;
            this.cmbvalue.SelectedIndexChanged += new System.EventHandler(this.cmbvalue_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(585, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(157, 21);
            this.textBox1.TabIndex = 14;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(526, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "筛选条件";
            // 
            // cmbFilterType
            // 
            this.cmbFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterType.FormattingEnabled = true;
            this.cmbFilterType.Location = new System.Drawing.Point(432, 24);
            this.cmbFilterType.Name = "cmbFilterType";
            this.cmbFilterType.Size = new System.Drawing.Size(84, 20);
            this.cmbFilterType.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(373, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "筛选方式";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(191, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "数据项";
            // 
            // cmbColName
            // 
            this.cmbColName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColName.FormattingEnabled = true;
            this.cmbColName.Location = new System.Drawing.Point(238, 24);
            this.cmbColName.Name = "cmbColName";
            this.cmbColName.Size = new System.Drawing.Size(121, 20);
            this.cmbColName.TabIndex = 9;
            this.cmbColName.SelectedIndexChanged += new System.EventHandler(this.cmbColName_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(9, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(890, 354);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "筛选";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.luoji,
            this.colname,
            this.Colopererator,
            this.tablename,
            this.Tabl_ColName,
            this.sql});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(884, 334);
            this.dataGridView1.TabIndex = 1;
            // 
            // id
            // 
            this.id.DataPropertyName = "Id";
            this.id.HeaderText = "序号";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // luoji
            // 
            this.luoji.DataPropertyName = "luoji";
            this.luoji.HeaderText = "逻辑";
            this.luoji.Name = "luoji";
            this.luoji.ReadOnly = true;
            // 
            // colname
            // 
            this.colname.DataPropertyName = "Colname";
            this.colname.HeaderText = "数据项";
            this.colname.Name = "colname";
            this.colname.ReadOnly = true;
            // 
            // Colopererator
            // 
            this.Colopererator.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Colopererator.DataPropertyName = "Colopererator";
            this.Colopererator.HeaderText = "筛选条件";
            this.Colopererator.Name = "Colopererator";
            this.Colopererator.ReadOnly = true;
            // 
            // tablename
            // 
            this.tablename.DataPropertyName = "Tablename";
            this.tablename.HeaderText = "Tablename";
            this.tablename.Name = "tablename";
            this.tablename.ReadOnly = true;
            this.tablename.Visible = false;
            // 
            // Tabl_ColName
            // 
            this.Tabl_ColName.DataPropertyName = "Tabl_ColName";
            this.Tabl_ColName.HeaderText = "Tabl_ColName";
            this.Tabl_ColName.Name = "Tabl_ColName";
            this.Tabl_ColName.ReadOnly = true;
            this.Tabl_ColName.Visible = false;
            // 
            // sql
            // 
            this.sql.DataPropertyName = "sql";
            this.sql.HeaderText = "sql";
            this.sql.Name = "sql";
            this.sql.ReadOnly = true;
            this.sql.Visible = false;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(131, 79);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 10;
            this.monthCalendar1.Visible = false;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.lab_sum);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.btnSubMit);
            this.groupBox3.Location = new System.Drawing.Point(12, 432);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(884, 55);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "筛选结果";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(640, 20);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 20;
            this.button3.Text = "删除";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // lab_sum
            // 
            this.lab_sum.AutoSize = true;
            this.lab_sum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_sum.ForeColor = System.Drawing.Color.Red;
            this.lab_sum.Location = new System.Drawing.Point(299, 25);
            this.lab_sum.Name = "lab_sum";
            this.lab_sum.Size = new System.Drawing.Size(0, 12);
            this.lab_sum.TabIndex = 19;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(721, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "清空筛选";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnSubMit
            // 
            this.btnSubMit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubMit.Location = new System.Drawing.Point(802, 21);
            this.btnSubMit.Name = "btnSubMit";
            this.btnSubMit.Size = new System.Drawing.Size(75, 23);
            this.btnSubMit.TabIndex = 16;
            this.btnSubMit.Text = "筛选";
            this.btnSubMit.UseVisualStyleBackColor = true;
            this.btnSubMit.Click += new System.EventHandler(this.btnSubMit_Click);
            // 
            // frmFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 493);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmFilter";
            this.Text = "数据筛选";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmFilter_FormClosed);
            this.Load += new System.EventHandler(this.frmFilter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbFilterType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbColName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.ComboBox cmbvalue;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSubMit;
        private System.Windows.Forms.Label lab_sum;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cmbLuoJi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn luoji;
        private System.Windows.Forms.DataGridViewTextBoxColumn colname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Colopererator;
        private System.Windows.Forms.DataGridViewTextBoxColumn tablename;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tabl_ColName;
        private System.Windows.Forms.DataGridViewTextBoxColumn sql;
        private System.Windows.Forms.Button button3;

    }
}