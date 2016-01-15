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
    public class ST_YJ_02 : IStatAdapter
    {


        public ST_YJ_02(string name, cfg config)
            : base(name, config)
        {

        }

        public ST_YJ_02(string name)
            : base(name)
        {

        }

        public override string GetStatSQL()
        {


            return "";
        }
        public override bool Stat()
        {
            string sql1 = "select count(distinct st_dt.sid)  as  国内发明量 from st_dt where ztid=" + Zid + " and (isguowai is null or isguowai=0)  and type='发明专利'";
            string sql2 = "select count(distinct st_dt.sid)  as  国外来华发明量 from st_dt where ztid=" + Zid + " and isguowai=1 and type='发明专利'";

            Dt = new DataTable();
            Dt.Columns.Add("国内发明量");
            Dt.Columns.Add("国外来华发明量");
            Dt.Columns.Add("预警阈值");
            Dt.Columns.Add("是否预警");
            Dt.Columns.Add("预警公式");
            DataRow row = Dt.NewRow();

            double int1 = Convert.ToDouble(DBA.MySqlDbAccess.ExecuteScalar(CommandType.Text, sql1));
            double int2 = Convert.ToDouble(DBA.MySqlDbAccess.ExecuteScalar(CommandType.Text, sql2));
            row[0] = int1;
            row[1] = int2;
            row[2] = ">5";
            if (int2 == 0)
            {
                row[3] = "NaN";
                row[4] = "国内发明量/国外来华发明量=NaN";
            }
            else
            {
                double d1 = int1 / int2;
                if (d1 > 5d)
                {
                    row[3] = "是";
                }
                else
                {
                    row[3] = "否";
                }
                row[4] = "国内发明量/国外来华发明量=" + d1.ToString("0.00");
            }
            Dt.Rows.Add(row);
            return true;
        }
    }
}
