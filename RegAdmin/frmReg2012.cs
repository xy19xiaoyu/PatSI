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
    public partial class frmReg2012 : Form
    {
        public frmReg2012()
        {
            InitializeComponent();
        }

        private static string shFile = Path.Combine(Application.StartupPath, "授权记录X.txt");
        //private static string shFile = Path.Combine(Application.StartupPath, "rhx.dat");
        private static string strRegKeyFile = Path.Combine(Application.StartupPath, "Regx.dll");

        private void radYP_CheckedChanged(object sender, EventArgs e)
        {
            txbJqh.Text = RegUtility.GetDiskNumber();
            txbYzm.Text = RegUtility.GetRegNumber(txbJqh.Text);
        }

        private void radCpu_CheckedChanged(object sender, EventArgs e)
        {
            txbJqh.Text = RegUtility.GetCpuNumber();
            txbYzm.Text = RegUtility.GetRegNumber(txbJqh.Text);
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            txbJqh.Text = RegUtility.GetDiskCPUNumber();
            txbYzm.Text = RegUtility.GetRegNumber(txbJqh.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //txbYzm.Text = DBA.Code.encrypt("I173387EFIE8");

            if (String.IsNullOrEmpty(txbJqh.Text))
            {
                MessageBox.Show("请完整填写注册信息！");
                txbJqh.Focus();
                return;
            }
            else
            {
                //原机器码
                string strDePcNm = DBA.Code.DecryptionAll(txbJqh.Text.Trim());

                //机器授权码
                string strPcNm_regNm = RegUtility.GetRegNumber(strDePcNm);

                //生成时间
                string strC_Date = DateTime.Now.ToString("yyyy-MM-dd");  //yyyy-MM-dd HH:mm:ss
                string strSqjz_Date = dtPkSqEnd.Value.ToString("yyyy-MM-dd");

                //加密机器授权码及数据使用授权码[x|BegData|x|x]
                txbYzm.Text = DBA.Code.encrypt(strPcNm_regNm + strC_Date + strSqjz_Date);

                //复制注册码
                Clipboard.SetText(this.txbYzm.Text.Trim());
            }

            //增加授权记录
            using (StreamWriter sw = new StreamWriter(shFile, true))
            {
                //分别是时间、机器号、授权信息、注册码
                string strRegDtAndUInfo = DateTime.Now.ToString() + " 为 " + txbUserInfo.Text.Trim();
                string strRegJQH = "机器号：" + txbJqh.Text.Trim();
                string strRegZCM = "授权码：" + txbYzm.Text;

                string strRegAllInfo = string.Format("{0}\r\n{1}\r\n{2}\r\n{3}", strRegDtAndUInfo, strRegJQH, strRegZCM, "-------------------------------------------------");
                sw.WriteLine(strRegAllInfo);

                //加密                
                //using (StreamWriter swReg = new StreamWriter(strRegKeyFile, true))
                //{
                //    PzCompression pzCom = PzCompressionHelper.getPzCompression(CompressionType.GZip);
                //    byte[] byTmp = pzCom.CompressToByte(strRegAllInfo);
                //    swReg.WriteLine(swReg);
                //}

                using (FileStream fsReg = new FileStream(strRegKeyFile, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fsReg.Position = fsReg.Length;

                    PzCompression pzCom = PzCompressionHelper.getPzCompression(CompressionType.GZip);
                    byte[] byTmp = pzCom.CompressToByte(strRegAllInfo);

                    byte[] byLen = System.BitConverter.GetBytes(byTmp.Length);

                    fsReg.Write(byLen, 0, byLen.Length);
                    fsReg.Write(byTmp, 0, byTmp.Length);
                }
            }

        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {

            txbJqh.Text = String.Empty;
            txbYzm.Text = String.Empty;
            txbUserInfo.Text = String.Empty;
        }

        private void btnShowHis_Click(object sender, EventArgs e)
        {
            try
            {
                //System.Diagnostics.Process.Start(shFile);
                frmRegHis frm = new frmRegHis(strRegKeyFile);
                frm.ShowDialog();
            }
            catch (Exception)
            {

                MessageBox.Show("授权记录文件不存在，请先至少进行一次授权！");
            }

        }

        private void frmReg2012_Load(object sender, EventArgs e)
        {
            dtPkSqEnd.Value = new DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.txbYzm.Text.Trim());
            MessageBox.Show("复制成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }
}