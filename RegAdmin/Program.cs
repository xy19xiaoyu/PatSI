using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CNDataBank.Compress.Compression;
using System.IO;
using RegLib;

namespace RegAdmin
{
    static class Program
    {
        public static string strComputNm = RegUtility.GetCpuNumber();

        public static int nRegCuuIdx = 0;

        public static int nRegMaxNm = 0;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmReg2012());

            //启动程序
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //#if !RELEASE  //DEBUG RELEASE

            //            if (!File.Exists(Path.Combine(Application.StartupPath, "Reg.dll")) || !File.Exists(Path.Combine(Application.StartupPath, "Regx.dll")))
            //            {
            //                MessageBox.Show("必要文件丢失，系统无法运行", "注册码生成器", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                return;
            //            }

            //            //注册检测
            //            if (!check_Reg())
            //            {
            //                frmRegLogin frm = new frmRegLogin();
            //                if (!frm.ShowDialog().Equals(DialogResult.OK))
            //                {
            //                    return;
            //                }
            //            }

            //#endif
            Application.Run(new frmReg2012());
        }


        static bool check_Reg()
        {
            bool bRt = true;
            try
            {
                string strRegNum = Regedit.GetRegedit("RegNm2013");
                string strRegeditRegCode = "";
                bRt = DeCompRegNum(strRegNum, out strRegeditRegCode, out nRegCuuIdx, out nRegMaxNm);
                if (strRegNum.Equals("") || !strRegeditRegCode.Equals(strComputNm))
                {
                    bRt = false;
                }
            }
            catch (Exception ex)
            {
                bRt = false;
            }

            return bRt;
        }

        public static bool DeCompRegNum(string _strRegText, out string _strRegCode, out int _nCurrID, out int _nMaxID)
        {
            bool bRt = true;
            _strRegCode = "";
            try
            {
                PzCompression pzCom = PzCompressionHelper.getPzCompression(CompressionType.GZip);

                string strRegNumDeCompress = pzCom.DeCompress(_strRegText);
                _strRegCode = strRegNumDeCompress.Substring(8);

                //读取授权码中可以授权的数量
                _nCurrID = Convert.ToInt32(strRegNumDeCompress.Substring(0, 4));
                _nMaxID = Convert.ToInt32(strRegNumDeCompress.Substring(4, 4));

                if (_nCurrID < 0 || _nMaxID < 1 || _nCurrID > _nMaxID)
                {
                    _nCurrID = 0;
                    _nMaxID = 0;
                    bRt = false;
                }
                else if (_nCurrID == _nMaxID)
                {
                    bRt = false;
                }
            }
            catch (Exception ex)
            {
                _nCurrID = 0;
                _nMaxID = 0;
                bRt = false;
            }

            return bRt;
        }
    }
}