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
    public class ST_YJ_03 : IStatAdapter
    {


        public ST_YJ_03(string name, cfg config)
            : base(name, config)
        {

        }

        public ST_YJ_03(string name)
            : base(name)
        {

        }

        public override string GetStatSQL()
        {


            return "";
        }
        public override bool Stat()
        {
            int year = DateTime.Now.Year - 5;
            string near5 = "";

            for (int i = year; i <= DateTime.Now.Year; i++)
            {
                near5 += year + ",";
            }
            near5 = near5.Trim(',');
            string sql1 = "select ady,count(distinct st_dt.sid) from st_dt where ztid=" + Zid + " and (isguowai is null or isguowai=0)  and type='发明专利' and ady in(" + near5 + ") order by ady asc";
            string sql2 = "select ady,count(distinct st_dt.sid)  from st_dt where ztid=" + Zid + " and isguowai=1 and type='发明专利'and ady in(" + near5 + ") order by ady asc";

            Dt = new DataTable();
            Dt.Columns.Add("国内近5年发明平均增长率");
            Dt.Columns.Add("国外来华近5年发明平均增长率");
            Dt.Columns.Add("预警阈值");
            Dt.Columns.Add("是否预警");
            Dt.Columns.Add("预警公式");
            DataRow row = Dt.NewRow();
            DataTable dt1 = DBA.MySqlDbAccess.GetDataTable(CommandType.Text, sql1);
            DataTable dt2 = DBA.MySqlDbAccess.GetDataTable(CommandType.Text, sql2);
            double int1 = 0;
            double int2 = 0;
            try
            {
                int1 = Convert.ToDouble(zzl(dt1));
                int2 = Convert.ToDouble(zzl(dt2));
            }
            catch (Exception ex)
            {
                row[0] = int1;
                row[1] = int2;
                row[2] = "<1";
                row[3] = "NaN";
                row[4] = "国内近5年发明平均增长率/国外来华近5年发明平均增长率=NaN";
                Dt.Rows.Add(row);
                return true;
            }
            row[0] = int1;
            row[1] = int2;
            row[2] = 1;
            if (int2 == 0)
            {
                row[3] = "NaN";
                row[4] = "国内近5年发明平均增长率/国外来华近5年发明平均增长率=NaN";
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
                row[4] = "国内近5年发明平均增长率/国外来华近5年发明平均增长率=" + d1.ToString("0.00");
            }

            Dt.Rows.Add(row);
            return true;
        }
        private string zzl(DataTable adsums)
        {
            List<int> sums = new List<int>();
            List<int> values = new List<int>();
            List<int> lstyear = new List<int>();
            int year = DateTime.Now.Year - 5;
            for (int i = year; i <= DateTime.Now.Year; i++)
            {
                var x = from tmp in adsums.AsEnumerable()
                        where tmp["ady"].ToString() == i.ToString()
                        select tmp["数量"].ToString();
                if (x.Count() > 0)
                {
                    lstyear.Add(i);
                    values.Add(Convert.ToInt32(x.First()));
                }
                else
                {
                }
            }
            string zzl = "";
            if (adsums.Rows.Count > 1)
            {

                int vf = values.First();
                int yf = lstyear.First();
                int vl = values.Last();
                int yl = lstyear.Last();
                int years = yl - yf;
                if (years != 0)
                {
                    double gen = 1.0 / years;
                    double num = Convert.ToDouble(vl) / vf;
                    double zs = System.Math.Pow(num, gen) - 1;
                    zzl = zs.ToString();
                }
                else
                {
                    zzl = "NA";
                }

            }
            else
            {
                zzl = "NA";
            }
            return zzl;
        }
    }
}
