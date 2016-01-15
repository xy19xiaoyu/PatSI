using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using IStat;
using StatCfg;
using System.Data;

namespace Stat.YJ
{
    public class ST_YJ_09 : IStatAdapter
    {


        public ST_YJ_09(string name, cfg config)
            : base(name, config)
        {

        }

        public ST_YJ_09(string name)
            : base(name)
        {

        }

        public override string GetStatSQL()
        {


            return "";
        }
        public override bool Stat()
        {
            string sql1 = "select count(distinct st_dt.sid)   from st_dt,st_pns where st_dt.sid = st_pns.sid and  st_dt.ztid=" + Zid + " and st_pns.gj ='欧洲专利局'";
            if (DBA.MySqlDbAccess.ExecuteScalar(CommandType.Text, "select dbtype from st_ztlist where id=" + Zid.ToString()).ToString() == "EPODOC")
            {
                sql1 = "select count(distinct st_dt.sid)   from st_dt where st_dt.ztid=" + Zid + " and st_dt.gj ='欧洲专利局'";
            }
            string sql2 = "select count(distinct st_dt.sid)   from st_dt where  st_dt.ztid=" + Zid + "";

            double int1 = Convert.ToDouble(DBA.MySqlDbAccess.ExecuteScalar(CommandType.Text, sql1));
            double int2 = Convert.ToDouble(DBA.MySqlDbAccess.ExecuteScalar(CommandType.Text, sql2));

            Dt = new DataTable();
            Dt.Columns.Add("EP专利家族数量");
            Dt.Columns.Add("总专利家族数量");
            Dt.Columns.Add("预警阈值");
            Dt.Columns.Add("是否预警");
            Dt.Columns.Add("预警公式");

            DataRow row = Dt.NewRow();

            row[0] = int1;
            row[1] = int2;
            row[2] = ">0.4";
            if (int2 == 0)
            {
                row[3] = "NaN";
                row[4] = "US专利家族数量/专利家族数量=NaN";
            }
            else
            {
                double d1 = int1 / int2;
                if (d1 > 0.4d)
                {
                    row[3] = "是";
                }
                else
                {
                    row[3] = "否";
                }
                row[4] = "EP专利家族数量/专利家族数量=" + d1.ToString("0.00");
            }

            Dt.Rows.Add(row);
            return true;
        }
    }
}
