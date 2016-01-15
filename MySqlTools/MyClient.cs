using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
namespace MySqlTools
{
    public class MySqlClient : IDisposable
    {
        public Process p;
        private string BasePath;
        private string Myini;
        private string exepath;
        private string subpath = "winx32\\";
        private string DBServerName = "eDaiXi_MySQL";
        private static log4net.ILog log = log4net.LogManager.GetLogger("MySqlClient");
        public MySqlClient()
        {
            if (Environment.Is64BitOperatingSystem)
            {
                subpath = "winx64\\";
            }
            BasePath = System.AppDomain.CurrentDomain.BaseDirectory + subpath;
            exepath = BasePath + "bin\\mysqld.exe";
            p = new Process();
            p.StartInfo.FileName = "cmd.exe";//要执行的程序名称 
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;//可能接受来自调用程序的输入信息 
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息 
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口 
            p.Start();//启动程序    

            p.StandardOutput.ReadLine();
            p.StandardOutput.ReadLine();
            p.StandardOutput.ReadLine();

        }
        public bool IniConfig()
        {
            Myini = BasePath + "my.ini";
            Myini = Myini.Replace("\\", "/");
            string strMyini = "";
            using (StreamReader sr = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "\\My.ini", Encoding.GetEncoding("gb2312")))
            {
                strMyini = sr.ReadToEnd();
            }
            using (StreamWriter sw = new StreamWriter(Myini, false, Encoding.UTF8))
            {
                sw.Write(string.Format(strMyini, BasePath.Replace("\\", "/")));
            }
            return true;
        }

        public bool New_Install()
        {
            try
            {
                log.Debug(exepath);
                string Arguments = string.Format("--install {1} --defaults-file=\"{0}\"", Myini, DBServerName);
                log.Debug(Arguments);
                ProcessStartInfo ps = new ProcessStartInfo();
                ps.FileName = exepath;
                ps.Arguments = Arguments;
                ps.CreateNoWindow = false;
                ps.UseShellExecute = false;
                Process p = new Process();
                p.StartInfo = ps;
                p.Start();
                Console.Write("数据库服务安装中");
                for (int i = 0; i < 100000000; i++)
                {

                    if (p.HasExited)
                    {
                        break;
                    }
                    else
                    {
                        if (i % 6 == 0 && i != 0)
                        {
                            Console.Write("\b \b\b \b\b \b\b \b\b \b\b \b");
                        }
                        Console.Write(".");
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                Console.WriteLine("数据库服务安装中\n");
                Start();
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return false;
            }
        }

        public bool New_unInstall()
        {
            try
            {
                Stop();
                string Arguments = string.Format("--remove {0}", DBServerName);
                ProcessStartInfo ps = new ProcessStartInfo();
                ps.FileName = exepath;
                ps.Arguments = Arguments;
                ps.CreateNoWindow = false;
                ps.UseShellExecute = false;

                Process p = new Process();
                p.StartInfo = ps;
                p.Start();
                Console.Write("数据库服务删除中");
                for (int i = 0; i < 100000000; i++)
                {

                    if (p.HasExited)
                    {
                        break;
                    }
                    else
                    {
                        if (i % 6 == 0 && i != 0)
                        {
                            Console.Write("\b \b\b \b\b \b\b \b\b \b\b \b");
                        }
                        Console.Write(".");
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                Console.WriteLine("数据库服务删除成功\n");
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return false;
            }
        }

        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }


        public bool Start()
        {
            ServiceController[] scs = ServiceController.GetServices(".");
            var ss = from x in scs
                     where x.ServiceName == DBServerName
                     select x;
            if (ss.Count() == 0)
            {
                Console.WriteLine("未找到数据库服务");
                return true;
            }
            else
            {
                ServiceController sc = ss.First();
                sc.Refresh();
                Console.Write("数据库服务正在启动");
                if (sc.Status != ServiceControllerStatus.Running)
                {
                    sc.Start();
                }
                for (int i = 0; i < 100000; i++)
                {
                    sc.Refresh();
                    if (sc.Status == ServiceControllerStatus.Running)
                    {
                        Console.Write("\n");
                        break;
                    }
                    else
                    {
                        if (i % 60 == 0 && i != 0)
                        {
                            Console.Write("\b \b\b \b\b \b\b \b\b \b\b \b");
                        }
                        if (i % 10 == 0)
                        {
                            Console.Write(".");
                        }
                        System.Threading.Thread.Sleep(100);
                    }
                }
                Console.WriteLine("数据库服务已经启动");
                return true;
            }
        }
        public bool ReStart()
        {
            Stop();
            Start();
            return true;
        }
        public bool Stop()
        {
            ServiceController[] scs = ServiceController.GetServices(".");
            var ss = from x in scs
                     where x.ServiceName == DBServerName
                     select x;
            if (ss.Count() == 0)
            {
                Console.WriteLine("未找到数据库服务");
                return true;
            }
            else
            {
                ServiceController sc = ss.First();
                sc.Refresh();
                Console.Write("正在停止数据库服务");
                if (sc.Status != ServiceControllerStatus.Stopped)
                {
                    sc.Stop();
                }
                for (int i = 0; i < 100000; i++)
                {
                    sc.Refresh();
                    if (sc.Status == ServiceControllerStatus.Stopped)
                    {
                        Console.Write("\n");
                        break;
                    }
                    else
                    {
                        if (i % 60 == 0 && i != 0)
                        {
                            Console.Write("\b \b\b \b\b \b\b \b\b \b\b \b");
                        }
                        if (i % 10 == 0)
                        {
                            Console.Write(".");
                        }
                        System.Threading.Thread.Sleep(100);
                    }
                }

                Console.WriteLine("数据库服务已经停止");
                return true;
            }
        }
        public void Dispose()
        {
            p.Close();
        }
    }
}
