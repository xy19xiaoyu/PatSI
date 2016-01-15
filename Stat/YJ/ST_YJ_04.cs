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
    public class ST_YJ_04 : IStatAdapter
    {


        public ST_YJ_04(string name, cfg config)
            : base(name, config)
        {

        }

        public ST_YJ_04(string name)
            : base(name)
        {

        }

        public override string GetStatSQL()
        {


            return "";
        }
        public override bool Stat()
        {

            string sql1 = string.Format(@"select count(DISTINCT st_dt.sid) from st_dt ,st_pa ,
(select pa from  st_dt ,st_pa 
where st_dt.sid = st_pa.sid 
and st_dt.ztid ={0} 
and (st_dt.isguowai is null or st_dt.isguowai=0)  
and st_dt.type='发明专利'
group by pa
order by count(DISTINCT st_dt.sid) desc
LIMIT 10) as c
where st_dt.sid = st_pa.sid  and st_pa.pa	 = c.pa
and st_dt.ztid ={0}", Zid);
            string sql2 = string.Format(@"select count(DISTINCT st_dt.sid) from st_dt ,st_pa ,
(select pa from  st_dt ,st_pa 
where st_dt.sid = st_pa.sid 
and st_dt.ztid ={0} 
and st_dt.isguowai=1  
and st_dt.type='发明专利'
group by pa
order by count(DISTINCT st_dt.sid) desc
LIMIT 10) as c
where st_dt.sid = st_pa.sid  and st_pa.pa	 = c.pa
and st_dt.ztid ={0}", Zid);
            Dt = new DataTable();

            Dt.Columns.Add("国内前十申请人发明量");
            Dt.Columns.Add("国外来华前十申请人发明量");
            Dt.Columns.Add("预警阈值");
            Dt.Columns.Add("是否预警");
            Dt.Columns.Add("预警公式");
            DataRow row = Dt.NewRow();

            Double int1 = 0;
            Double.TryParse(DBA.MySqlDbAccess.ExecuteScalar(CommandType.Text, sql1).ToString(), out int1);
            Double int2 = 0;
            Double.TryParse(DBA.MySqlDbAccess.ExecuteScalar(CommandType.Text, sql2).ToString(), out int2);


            row[0] = int1;
            row[1] = int2;
            row[2] = "<1";
            if (int2 == 0)
            {
                row[3] = "NaN";
                row[4] = "国内前十申请人发明量/国外来华前十申请人发明量";
            }
            else
            {
                double d1 = int1 / int2;
                if (d1 < 1d)
                {
                    row[3] = "是";
                }
                else
                {
                    row[3] = "否";
                }
                row[4] = "国内前十申请人发明量/国外来华前十申请人发明量=" + d1.ToString("0.00");
            }
            Dt.Rows.Add(row);
            return true;
        }
    }
}
