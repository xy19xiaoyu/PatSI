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
    public class ST_YJ_05 : IStatAdapter
    {


        public ST_YJ_05(string name, cfg config)
            : base(name, config)
        {

        }

        public ST_YJ_05(string name)
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
and  st_dt.isguowai=1  
and st_dt.type='发明专利'
group by pa
order by count(DISTINCT st_dt.sid) desc
LIMIT 10) as c
where st_dt.sid = st_pa.sid  and st_pa.pa	 = c.pa
and st_dt.ztid ={0}", Zid);
            string sql13 = string.Format(@"select count(DISTINCT st_dt.sid) from st_dt ,st_pa ,
(select pa from  st_dt ,st_pa 
where st_dt.sid = st_pa.sid 
and st_dt.ztid ={0} 
and (st_dt.isguowai is null or st_dt.isguowai=0)  
and st_dt.type='发明专利'
group by pa
order by count(DISTINCT st_dt.sid) desc
LIMIT 3) as c
where st_dt.sid = st_pa.sid  and st_pa.pa	 = c.pa
and st_dt.ztid ={0}", Zid);
            string sql23 = string.Format(@"select count(DISTINCT st_dt.sid) from st_dt ,st_pa ,
(select pa from  st_dt ,st_pa 
where st_dt.sid = st_pa.sid 
and st_dt.ztid ={0} 
and st_dt.isguowai=1  
and st_dt.type='发明专利'
group by pa
order by count(DISTINCT st_dt.sid) desc
LIMIT 3) as c
where st_dt.sid = st_pa.sid  and st_pa.pa	 = c.pa
and st_dt.ztid ={0}", Zid);
            Dt = new DataTable();
            Dt.Columns.Add("国内前三申请人专利数量");
            Dt.Columns.Add("国内前十申请人专利数量");
            Dt.Columns.Add("国外前三申请人专利数量");
            Dt.Columns.Add("国外前十申请人专利数量");
            Dt.Columns.Add("预警阈值");
            Dt.Columns.Add("是否预警");
            Dt.Columns.Add("预警公式");
            DataRow row = Dt.NewRow();

            Double int1 = Convert.ToDouble(DBA.MySqlDbAccess.ExecuteScalar(CommandType.Text, sql1));
            Double int2 = Convert.ToDouble(DBA.MySqlDbAccess.ExecuteScalar(CommandType.Text, sql2));
            Double int13 = Convert.ToDouble(DBA.MySqlDbAccess.ExecuteScalar(CommandType.Text, sql13));
            Double int23 = Convert.ToDouble(DBA.MySqlDbAccess.ExecuteScalar(CommandType.Text, sql23));


            row[0] = int13;
            row[1] = int1;
            row[2] = int23;
            row[3] = int2;
            row[4] = "<0.25";
            if (int2 == 0 || int1 == 0)
            {
                row[5] = "NaN";
                row[6] = "国内前三申请人发明量/前十申请人发明量【除以】国外来华前三申请人发明量/前十申请人发明量=NaN";
            }
            else
            {
                double d1 = int1 / int2;
                if (d1 < 0.25)
                {
                    row[5] = "是";
                }
                else
                {
                    row[5] = "否";
                }
                row[6] = "国内前三申请人发明量/前十申请人发明量【除以】国外来华前三申请人发明量/前十申请人发明量=" + d1.ToString("0.00");
            }
            Dt.Rows.Add(row);
            return true;
        }
    }
}
