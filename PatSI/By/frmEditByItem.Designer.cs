namespace PatSI.By
{
    partial class frmEditByItem
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
            this.label1 = new System.Windows.Forms.Label();
            this.txbByItemName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDelSel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lstByWord = new System.Windows.Forms.ListBox();
            this.txbWord = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnUpKeyName = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "标引项名称:";
            // 
            // txbByItemName
            // 
            this.txbByItemName.Location = new System.Drawing.Point(89, 23);
            this.txbByItemName.Name = "txbByItemName";
            this.txbByItemName.Size = new System.Drawing.Size(177, 21);
            this.txbByItemName.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.btnUpKeyName);
            this.groupBox1.Controls.Add(this.btnDelSel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lstByWord);
            this.groupBox1.Controls.Add(this.txbWord);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.txbByItemName);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(393, 240);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // btnDelSel
            // 
            this.btnDelSel.Location = new System.Drawing.Point(272, 167);
            this.btnDelSel.Name = "btnDelSel";
            this.btnDelSel.Size = new System.Drawing.Size(102, 23);
            this.btnDelSel.TabIndex = 9;
            this.btnDelSel.Text = "删除选中";
            this.btnDelSel.UseVisualStyleBackColor = true;
            this.btnDelSel.Click += new System.EventHandler(this.btnDelSel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 203);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "标引词:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(87, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(233, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "*支持汉字、字母、数字,最多支持20个字符";
            // 
            // lstByWord
            // 
            this.lstByWord.FormattingEnabled = true;
            this.lstByWord.ItemHeight = 12;
            this.lstByWord.Location = new System.Drawing.Point(89, 66);
            this.lstByWord.Name = "lstByWord";
            this.lstByWord.Size = new System.Drawing.Size(177, 124);
            this.lstByWord.TabIndex = 3;
            this.lstByWord.SelectedIndexChanged += new System.EventHandler(this.lstByWord_SelectedIndexChanged);
            // 
            // txbWord
            // 
            this.txbWord.Location = new System.Drawing.Point(89, 200);
            this.txbWord.Name = "txbWord";
            this.txbWord.Size = new System.Drawing.Size(177, 21);
            this.txbWord.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "标引词列表:";
            // 
            // btnUpKeyName
            // 
            this.btnUpKeyName.Location = new System.Drawing.Point(272, 21);
            this.btnUpKeyName.Name = "btnUpKeyName";
            this.btnUpKeyName.Size = new System.Drawing.Size(102, 23);
            this.btnUpKeyName.TabIndex = 10;
            this.btnUpKeyName.Text = "修改名称";
            this.btnUpKeyName.UseVisualStyleBackColor = true;
            this.btnUpKeyName.Click += new System.EventHandler(this.btnUpKeyName_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(326, 198);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(48, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "添加";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(272, 198);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(48, 23);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "修改";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // frmEditByItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 240);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditByItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编缉标引项";
            this.Load += new System.EventHandler(this.frmEditByItem_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbByItemName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstByWord;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbWord;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDelSel;
        private System.Windows.Forms.Button btnUpKeyName;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button button3;
    }
}