using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBA;
using System.Data;
using MySql.Data.MySqlClient;
using log4net;
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace BLL
{
    public class ZTHelper
    {
        private static ILog log = LogManager.GetLogger("System");
        /// <summary>
        /// 判断专题库是否存在
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static bool ExistZTDB(string Name)
        {
            MySqlParameter parm = new MySqlParameter("?name", Name);
            if (MySqlDbAccess.ExecuteScalar(CommandType.Text, "select count(*) from st_ztlist where name=?name and isdel=0", parm).ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        /// <summary>
        /// 添加专题库
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Desc"></param>
        /// <returns></returns>
        public static bool AddZTDB(string Name, string Desc, string type)
        {
            MySqlParameter[] parms = new MySqlParameter[]
            {
                new MySqlParameter("?name",Name),
                new MySqlParameter("?des",Desc),
                new MySqlParameter("?dbtype",type),
                new MySqlParameter("?createuserid",UserHelper.user.UserId),
                new MySqlParameter("?createtime",DateTime.Now.ToString())
            };
            if (MySqlDbAccess.ExecNoQuery(CommandType.Text, "insert into st_ztlist (name,des,dbtype,createuserid,createtime,isdel) values (?name,?des,?dbtype,?createuserid,?createtime,0)", parms) == 0)
            {
                return false;
            }
            else
            {
                log.Info(string.Format("添加专题库：{0},描述:{1}, 类型:{2}\t成功\t" + DateTime.Now.ToString(), Name, Desc, type));
                return true;
            }
        }

        /// <summary>
        /// 得到专题库列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetZTList()
        {
            return MySqlDbAccess.GetDataTable(CommandType.Text, "select * ,'系统管理员' as 'createusername' from st_ztlist where isdel =0");
        }
        /// <summary>
        /// 删除专题库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DelZTDB(string ztid, string ztname)
        {
            //string st_ipc = string.Format("delete from st_ipc where ztid={0}", ztid);
            //string st_pa = string.Format("delete from  st_pa where ztid={0}", ztid);
            //string st_pr = string.Format("delete from  st_pr where ztid={0}", ztid);
            //string show_base = string.Format("delete from  show_base where ztid={0}", ztid);
            //string st_iv = string.Format("delete from  st_iv where ztid={0}", ztid);
            //string st_pns = string.Format("delete from st_pns where ztid={0}", ztid);
            //string st_ans = string.Format("delete from st_ans where ztid={0}", ztid);
            //string st_dmc = string.Format("delete from  st_dc where ztid={0}", ztid);
            //string st_fml = string.Format("delete from  st_fml where ztid={0}", ztid);
            //string st_dt = string.Format("delete from st_dt where ztid={0}", ztid);
            //string st_ztlist = string.Format("delete from st_ztlist where id={0}", ztid);
            string st_ztlist = string.Format("update st_ztlist set isdel=1 where id={0}", ztid);


            using (MySqlConnection conn = MySqlDbAccess.GetMySqlConnection())
            {
                conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();
                //MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, st_ipc);
                //MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, st_pa);
                //MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, st_pr);
                //MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, show_base);
                //MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, st_iv);
                //MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, st_pns);
                //MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, st_ans);
                //MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, st_dmc);
                //MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, st_fml);
                //MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, st_dt);
                MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, st_ztlist);
                trans.Commit();

            }
            log.Info("删除专题库：" + ztname + "\t成功\t" + DateTime.Now.ToString());
            return true;
        }

        /// <summary>
        /// 修改专题库
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Name"></param>
        /// <param name="Desc"></param>
        /// <returns></returns>
        public static bool EditZTDB(string id, string Name, string Desc, string type)
        {
            MySqlParameter[] parms = new MySqlParameter[]
            {
                new MySqlParameter("?name",Name),
                new MySqlParameter("?des",Desc),
                new MySqlParameter("?id",id)
            };
            if (MySqlDbAccess.ExecNoQuery(CommandType.Text, "update st_ztlist set name = ?name,des=?des where id=?id", parms) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
