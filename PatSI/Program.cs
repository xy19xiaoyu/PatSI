using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using BLL.dblinq;
using BLL.DBServer;
using System.IO;
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace PatSI
{
    static class Program
    {
        public static string Version = "V1.0";
        public static string StartPath = System.IO.Directory.GetCurrentDirectory();
        public static DateTime dtWebHost = DateTime.Now;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BLL.SysMag.Login.StrLoginLname = "admin";
            BLL.SysMag.Login.StrLoginSname = "系统管理员";
            BLL.SysMag.Login.StrLoginUserID = "1";
            try
            {
                //MySQLServer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            //启动程序
            ScdMain();
        }


        //方法一：禁止多个进程运行
        private static void FstMain()
        {
            bool ret;
            Mutex m_Ctmt = new Mutex(true, "MutexInstance", out ret);
            if (ret)
            {
                MainStart();
            }
        }


        //方法二：禁止多个进程运行,并当重复运行时激活以前的进程
        public static void ScdMain()
        {
            //Get   the   running   instance.  //得到正在运行的例程     
            Process instance = RunningInstance();
            if (instance == null)
            {
                //System.Windows.Forms.Application.EnableVisualStyles();
                ////这两行实现   XP   可视风格          
                //System.Windows.Forms.Application.DoEvents();
                ////There   isn't   another   instance,   show   our   form.           
                //Application.Run(new Main_Form());
                MainStart();
            }
            else
            {
                //There   is   another   instance   of   this   process.  
                //处理发现的例程
                HandleRunningInstance(instance);
            }
        }

        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            //Loop   through   the   running   processes   in   with   the   same   name       
            foreach (Process process in processes)
            {        //Ignore   the   current   process           
                if (process.Id != current.Id)
                {
                    //Make   sure   that   the   process   is   running   from   the   exe   file.               
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        //Return   the   other   process   instance.                   
                        return process;
                    }
                }
            }
            //No   other   instance   was   found,   return   null.     
            return null;
        }

        public static void HandleRunningInstance(Process instance)
        {
            //Make  sure the window is not minimized or maximized   确保窗口没有被最小化或最大化   
            //0不可见但仍然运行,1居中,2最小化,3最大化
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);
            //Set   the   real   intance   to   foreground   window    
            SetForegroundWindow(instance.MainWindowHandle);
        }

        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1; //0不可见但仍然运行,1居中,2最小化,3最大化


        private static void MainStart()
        {
            //启动程序
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if !DEBUG     //DEBUG RELEASE
            //系统说明
            //frmLoading frmLd = new frmLoading();
            //frmLd.ShowDialog();

            checkVersion frmcv = new checkVersion();
            if (frmcv.ShowDialog() == DialogResult.Abort)
            {
                return;
            }

            //注册检测
            bool bIsHost = false;
            try
            {
                UPServer.PatSISvsSoapClient ws = new UPServer.PatSISvsSoapClient();
                dtWebHost = ws.getWebHostDateTime();
                bIsHost = true;
            }
            catch (Exception ex)
            {
                bIsHost = false;
            }

            RegLib.reginfo reg = new RegLib.reginfo();
            //using (StreamWriter sw = new StreamWriter("log.txt", true))
            //{
            //    sw.WriteLine("登录：" + DateTime.Now.ToString());
            //    sw.WriteLine("最后一次使用时间：" + RegLib.reginfo.LastUsedTime.ToString());
            //    sw.WriteLine("授权截止时间：" + RegLib.reginfo.DtSqEndDate.ToString());
            //    sw.WriteLine("机器码：" + RegLib.reginfo.DiskNumber.ToString());
            //    sw.WriteLine("授权码：" + RegLib.reginfo.StrPC_SQM.ToString());
            //}
            //
            if (!reg.CheckRegedit(dtWebHost, bIsHost))
            {
                frmRegLogin frm = new frmRegLogin();
                if (!frm.ShowDialog().Equals(DialogResult.OK))
                {
                    return;
                }
            }
            try
            {
                DBSchemaAutoUpdata.CheckAndUpData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


#endif
           // Application.Run(new frmFilter("ccc",1,"CPRS"));

            Application.Run(new MDIMain());
        }



    }
}
