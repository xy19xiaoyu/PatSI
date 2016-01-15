using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CNDataBank.Compress.Compression;

namespace RegAdmin
{
    public partial class frmRegHis : Form
    {
        private frmRegHis()
        {
            InitializeComponent();
        }

        private string strRegKeyFile = "";

        public frmRegHis(string _strKeyFile)
        {
            InitializeComponent();
            strRegKeyFile = _strKeyFile;
        }



        private void frmRegHis_Load(object sender, EventArgs e)
        {
            try
            {
                string strRegAllInfo = File.ReadAllText(strRegKeyFile);


                PzCompression pzCom = PzCompressionHelper.getPzCompression(CompressionType.GZip);
                int nIdx = 0;

                byte[] byTmp;
                using (FileStream fsReg = new FileStream(strRegKeyFile, FileMode.Open, FileAccess.Read))
                {
                    while (true)
                    {
                        byTmp = new byte[4];
                        if (fsReg.Read(byTmp, 0, 4) < 1) break;

                        int nLen = BitConverter.ToInt32(byTmp, 0);
                        byTmp = new byte[nLen];
                        fsReg.Read(byTmp, 0, nLen);

                        textBox1.Text += Encoding.Unicode.GetString(pzCom.DeCompress(byTmp)) + "\r\n";

                        nIdx++;
                    }
                }
                label1.Text = string.Format("共授权完成{0}台机器", nIdx);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
