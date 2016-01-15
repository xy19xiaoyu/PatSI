using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;

namespace BLL.DBServer
{
    public class MySQLServer
    {
        public static bool Start()
        {
            ServiceController[] scs = ServiceController.GetServices(".");
            var ss = from x in scs
                     where x.ServiceName == "MySQL-FZ"
                     select x;
            if (ss.Count() == 0)
            {
                throw new Exception("检测到你未安装数据库服务,请参照使用手册安装数据库服务");
            }
            else
            {
                try
                {
                    ServiceController sc = ss.First();
                    sc.Refresh();
                    if (sc.Status != ServiceControllerStatus.Running)
                    {
                        sc.Start();
                        sc.Refresh();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("启动数据库服务失败", ex);
                }
            }
        }
    }
}
