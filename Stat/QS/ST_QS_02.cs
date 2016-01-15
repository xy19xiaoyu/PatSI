using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using StatCfg;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;
namespace Stat.QS
{
    //技术生命周期
    public class ST_QS_02 : IStatAdapter
    {
        public ST_QS_02(string name, cfg config)
            : base(name, config)
        {

        }
        public override string GetStatSQL()
        {
            string zid = Config.Zid;
            string strzhibiao = "";
            string columns = "";
            string join = "";
            string groupby = "";
            string wherezt = "st_dt.ztid =" + Config.Zid;
            string where = "";
            string tables = "";
            string sql = "select {0},{1} from {2} where {3} {4} {5} group by {6} ";
            List<string> tablenames = new List<string>();

            tablenames.Add("st_pa");
            if (Config.UseCPY == false)
            {
                strzhibiao = " count(distinct st_dt.sid) as '申请量' ,count(distinct st_pa.pa) as '申请人数量' ";


            }
            else
            {
                strzhibiao = " count(distinct st_dt.sid) as '申请量' ,count(distinct st_dt.cpy) as '申请人数量' ";
            }



            columns = string.Format("cast({0}.{1} as char) as '{2}'", Config.GroupYear.TableName, Config.GroupYear.ColName, Config.GroupYear.ShowName);
            groupby = string.Format("{0}.{1}", Config.GroupYear.TableName, Config.GroupYear.ColName, Config.GroupYear.ShowName);
            tablenames.Add(Config.GroupYear.TableName);
            tablenames.AddRange(Config.TableNames);



            tablenames = tablenames.Distinct().ToList<string>();
            tablenames.Remove("st_dt");
            tablenames.Add("st_dt");
            
            foreach (var tb in tablenames)
            {
                tables += tb + ",";
            }
            tables = tables.Trim(',');

            if (tablenames.Count > 1)
            {
                for (int i = 1; i < tablenames.Count; i++)
                {
                    if (join == "")
                    {
                        join += string.Format("{0}.sid={1}.sid", tablenames[0], tablenames[i]);
                    }
                    else
                    {
                        join += string.Format(" and {0}.sid={1}.sid", tablenames[0], tablenames[i]);
                    }

                }
            }
            if (!String.IsNullOrEmpty(Config.FilterSQL))
            {
                tables += ", (" + Config.FilterSQL + " ) as Filter ";
                if (join == "")
                {
                    join += string.Format("{0}.sid = Filter.sid", tablenames[0]);
                }
                else
                {
                    join += string.Format(" and {0}.sid = Filter.sid", tablenames[0]);
                }

            }
            if (join == "")
            {
                wherezt = string.Format("st_dt.ztid={0}", zid);
            }
            else
            {
                wherezt = string.Format(" and st_dt.ztid={0}", zid);
            }

            where = string.Format(" and {0}.{1} in({2})", Config.GroupYear.TableName, Config.GroupYear.ColName, Config.TopYear);

            string tmpsql = string.Format(sql, columns, strzhibiao, tables, join, wherezt, where, groupby);

            return tmpsql;
        }

        public override bool DrawingLineChart()
        {
            msChart.Series.Clear();
            this.msChart.Series.Add("生命周期");
            for (int i = 1; i < Dt.Columns.Count; i++)
            {
                this.msChart.Series["生命周期"].ChartType = Config.ChartType;
                msChart.Series["生命周期"].MarkerStyle = MarkerStyle.Circle; //圆点
                msChart.Series["生命周期"].MarkerSize = 9;
                msChart.Series["生命周期"].BorderWidth = 3;
            }
            foreach (DataRow row in Dt.Rows)
            {
                this.msChart.Series["生命周期"].Points.Add(new System.Windows.Forms.DataVisualization.Charting.DataPoint(Convert.ToDouble(row[1].ToString()), Convert.ToDouble(row[2].ToString())) { Label = row[0].ToString() + "年" });
            }
            return true;
        }

    }
}
