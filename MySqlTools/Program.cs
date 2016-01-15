using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySqlTools
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {

                MySqlClient msc = new MySqlClient();
                Console.WriteLine("数据库安装向导");
                Console.WriteLine("1：安装并启动数据库服务");
                Console.WriteLine("2：停止并删除数据库服务");
                Console.WriteLine("3：启动数据库服务");
                Console.WriteLine("4：停止数据库服务");
                Console.WriteLine("退出请输入其它");
                Console.Write("请选择功能：");
                string intput = Console.ReadLine();
                switch (intput)
                {
                    case "1":
                        msc.New_Install();
                        break;
                    case "2":
                        msc.New_unInstall();
                        break;
                    case "3":
                        msc.Start();
                        break;
                    case "4":
                        msc.Stop();
                        break;
                    default:
                        return;

                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
