namespace PatSI
{
    partial class frmRegLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegLogin));
            this.txbYzm = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbJqh = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReg = new System.Windows.Forms.Button();
            this.btnTryUse = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txbYzm
            // 
            this.txbYzm.Location = new System.Drawing.Point(65, 73);
            this.txbYzm.Name = "txbYzm";
            this.txbYzm.Size = new System.Drawing.Size(376, 55);
            this.txbYzm.TabIndex = 9;
            this.txbYzm.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "注册码：";
            // 
            // txbJqh
            // 
            this.txbJqh.Location = new System.Drawing.Point(65, 29);
            this.txbJqh.Multiline = false;
            this.txbJqh.Name = "txbJqh";
            this.txbJqh.ReadOnly = true;
            this.txbJqh.Size = new System.Drawing.Size(376, 21);
            this.txbJqh.TabIndex = 7;
            this.txbJqh.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "机器号：";
            // 
            // btnReg
            // 
            this.btnReg.Location = new System.Drawing.Point(332, 143);
            this.btnReg.Name = "btnReg";
            this.btnReg.Size = new System.Drawing.Size(109, 31);
            this.btnReg.TabIndex = 12;
            this.btnReg.Text = "注册";
            this.btnReg.UseVisualStyleBackColor = true;
            this.btnReg.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnTryUse
            // 
            this.btnTryUse.Location = new System.Drawing.Point(65, 143);
            this.btnTryUse.Name = "btnTryUse";
            this.btnTryUse.Size = new System.Drawing.Size(115, 31);
            this.btnTryUse.TabIndex = 13;
            this.btnTryUse.Text = "继续试用";
            this.btnTryUse.UseVisualStyleBackColor = true;
            this.btnTryUse.Click += new System.EventHandler(this.btnTryUse_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(202, 143);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 31);
            this.button1.TabIndex = 14;
            this.button1.Text = "复制机器号";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // frmRegLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 187);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnTryUse);
            this.Controls.Add(this.btnReg);
            this.Controls.Add(this.txbYzm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txbJqh);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRegLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统注册";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRegLogin_FormClosing);
            this.Load += new System.EventHandler(this.frmRegLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txbYzm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txbJqh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReg;
        private System.Windows.Forms.Button btnTryUse;
        private System.Windows.Forms.Button button1;
    }
}