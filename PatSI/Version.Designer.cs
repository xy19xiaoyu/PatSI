namespace PatSI
{
    partial class Version
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
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lab_new = new System.Windows.Forms.Label();
            this.dbversion_new = new System.Windows.Forms.Label();
            this.dbversion_now = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.sysversion = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(208, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(181, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "数据库重置并更新至最新版本";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dbversion_now);
            this.groupBox1.Controls.Add(this.dbversion_new);
            this.groupBox1.Controls.Add(this.lab_new);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 76);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "当前数据库版本：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "最新数据库版本：";
            // 
            // lab_new
            // 
            this.lab_new.AutoSize = true;
            this.lab_new.Location = new System.Drawing.Point(138, 49);
            this.lab_new.Name = "lab_new";
            this.lab_new.Size = new System.Drawing.Size(0, 12);
            this.lab_new.TabIndex = 4;
            // 
            // dbversion_new
            // 
            this.dbversion_new.AutoSize = true;
            this.dbversion_new.Location = new System.Drawing.Point(139, 49);
            this.dbversion_new.Name = "dbversion_new";
            this.dbversion_new.Size = new System.Drawing.Size(17, 12);
            this.dbversion_new.TabIndex = 5;
            this.dbversion_new.Text = "  ";
            // 
            // dbversion_now
            // 
            this.dbversion_now.AutoSize = true;
            this.dbversion_now.Location = new System.Drawing.Point(138, 26);
            this.dbversion_now.Name = "dbversion_now";
            this.dbversion_now.Size = new System.Drawing.Size(17, 12);
            this.dbversion_now.TabIndex = 6;
            this.dbversion_now.Text = "  ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.sysversion);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(12, 94);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(409, 60);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "软件";
            // 
            // sysversion
            // 
            this.sysversion.AutoSize = true;
            this.sysversion.Location = new System.Drawing.Point(123, 26);
            this.sysversion.Name = "sysversion";
            this.sysversion.Size = new System.Drawing.Size(17, 12);
            this.sysversion.TabIndex = 6;
            this.sysversion.Text = "  ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(138, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 12);
            this.label5.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "当前软件版本：";
            // 
            // Version
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 161);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Version";
            this.Text = "版本信息";
            this.Load += new System.EventHandler(this.Version_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lab_new;
        private System.Windows.Forms.Label dbversion_now;
        private System.Windows.Forms.Label dbversion_new;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label sysversion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
    }
}