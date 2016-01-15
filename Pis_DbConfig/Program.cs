using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Pis_DbConfig
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //string s = PoJieSiQuanJia.PoJieSiQuanJia.encrypt("Server=192.168.131.80;Database=ztst;Uid=root;Pwd=sa@123456;charset=utf8;Allow User Variables=True");
            string s1 = PoJieSiQuanJia.PoJieSiQuanJia.encrypt("Server=127.0.0.1;Database=ztst;Uid=root;Pwd=sa@123456;charset=utf8;Allow User Variables=True");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmDbConfig());
        }
    }
}
