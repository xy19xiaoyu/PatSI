using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;

namespace BLL.dblinq
{
    public class DBSchemaAutoUpdata
    {
        public static bool CheckAndUpData()
        {
            DataTable dt = DBA.MySqlDbAccess.GetDataTable(CommandType.Text, "select * from cfg_dbversion");
            string[] files = Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory + "MySqlScript");
            List<int> versionhis = new List<int>();
            foreach (var file in files)
            {
                try
                {
                    versionhis.Add(Convert.ToInt32(Path.GetFileNameWithoutExtension(file)));
                }
                catch (Exception)
                {
                    continue;
                }
            }
            int newserion = versionhis.Max();
            int exeversion = Convert.ToInt32(dt.Rows[0]["dbversion"].ToString());
            string updatatime = dt.Rows[0]["updatetime"].ToString();
            if (newserion == exeversion)
            {
                return true;
            }
            else
            {
                using (MySqlConnection conn = DBA.MySqlDbAccess.GetMySqlConnection())
                {
                    conn.Open();
                    for (int i = exeversion + 1; i <= newserion; i++)
                    {
                        string sql = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "MySqlScript\\" + i, Encoding.GetEncoding("utf-8"));
                        MySqlScript script = new MySqlScript(conn);
                        script.Query = sql;
                        int count = script.Execute();
                        DBA.MySqlDbAccess.ExecNoQuery(CommandType.Text, string.Format("update cfg_dbversion set dbversion={0},updatetime='{1}'", i, DateTime.Now));

                        //try
                        //{
                        //}
                        //catch (Exception ex)
                        //{
                        //    throw ex;
                        //}

                    }
                }
                return true;
            }
        }
    }
}
