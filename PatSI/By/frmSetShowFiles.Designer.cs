namespace PatSI.By
{
    partial class frmSetShowFiles
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
            this.chkBase = new System.Windows.Forms.CheckBox();
            this.chkLstBoxFiled = new System.Windows.Forms.CheckedListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkIndexFiled = new System.Windows.Forms.CheckBox();
            this.chkLstBoxAutoIndex = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkBase);
            this.groupBox1.Controls.Add(this.chkLstBoxFiled);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(389, 190);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本字段";
            // 
            // chkBase
            // 
            this.chkBase.AutoSize = true;
            this.chkBase.Location = new System.Drawing.Point(61, 0);
            this.chkBase.Name = "chkBase";
            this.chkBase.Size = new System.Drawing.Size(48, 16);
            this.chkBase.TabIndex = 1;
            this.chkBase.Text = "全选";
            this.chkBase.UseVisualStyleBackColor = true;
            this.chkBase.CheckedChanged += new System.EventHandler(this.chkBase_CheckedChanged);
            // 
            // chkLstBoxFiled
            // 
            this.chkLstBoxFiled.CheckOnClick = true;
            this.chkLstBoxFiled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkLstBoxFiled.FormattingEnabled = true;
            this.chkLstBoxFiled.HorizontalScrollbar = true;
            this.chkLstBoxFiled.Location = new System.Drawing.Point(3, 17);
            this.chkLstBoxFiled.MultiColumn = true;
            this.chkLstBoxFiled.Name = "chkLstBoxFiled";
            this.chkLstBoxFiled.ScrollAlwaysVisible = true;
            this.chkLstBoxFiled.Size = new System.Drawing.Size(383, 170);
            this.chkLstBoxFiled.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(314, 400);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(216, 400);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkIndexFiled);
            this.groupBox2.Controls.Add(this.chkLstBoxAutoIndex);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 190);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(389, 190);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标引字段";
            // 
            // chkIndexFiled
            // 
            this.chkIndexFiled.AutoSize = true;
            this.chkIndexFiled.Location = new System.Drawing.Point(62, 0);
            this.chkIndexFiled.Name = "chkIndexFiled";
            this.chkIndexFiled.Size = new System.Drawing.Size(48, 16);
            this.chkIndexFiled.TabIndex = 2;
            this.chkIndexFiled.Text = "全选";
            this.chkIndexFiled.UseVisualStyleBackColor = true;
            this.chkIndexFiled.CheckedChanged += new System.EventHandler(this.chkIndexFiled_CheckedChanged);
            // 
            // chkLstBoxAutoIndex
            // 
            this.chkLstBoxAutoIndex.CheckOnClick = true;
            this.chkLstBoxAutoIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkLstBoxAutoIndex.FormattingEnabled = true;
            this.chkLstBoxAutoIndex.HorizontalScrollbar = true;
            this.chkLstBoxAutoIndex.Location = new System.Drawing.Point(3, 17);
            this.chkLstBoxAutoIndex.MultiColumn = true;
            this.chkLstBoxAutoIndex.Name = "chkLstBoxAutoIndex";
            this.chkLstBoxAutoIndex.ScrollAlwaysVisible = true;
            this.chkLstBoxAutoIndex.Size = new System.Drawing.Size(383, 170);
            this.chkLstBoxAutoIndex.TabIndex = 0;
            // 
            // frmSetShowFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 435);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetShowFiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置显示字段";
            this.Load += new System.EventHandler(this.frmSetShowFiles_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox chkLstBoxFiled;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox chkLstBoxAutoIndex;
        private System.Windows.Forms.CheckBox chkBase;
        private System.Windows.Forms.CheckBox chkIndexFiled;
    }
}