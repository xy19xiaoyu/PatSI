using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using IStat;
using StatCfg;
using System.Data;

namespace Stat.CN
{
    public class ST_CN_02 : IStatAdapter
    {


        public ST_CN_02(string name, cfg config)
            : base(name, config)
        {

        }

        public ST_CN_02(string name)
            : base(name)
        {

        }

        public override string GetStatSQL()
        {
            string strzhibiao = "";
            string columns = "";
            string join = "";
            string groupby = "";
            string wherezt = "st_dt.ztid =" + Zid;

            string tables = "";
            string sql = "select {0},{1} from {2} where {3} {4} {5} group by {6}";
            List<string> tablenames = new List<string>();

            strzhibiao = " count(distinct st_dt.sid) as '申请量' ";

            columns = string.Format("cast({0}.{1} as char) as '{2}'", Config.GroupYear.TableName, Config.GroupYear.ColName, Config.GroupYear.ShowName);
            groupby = string.Format("{0}.{1}", Config.GroupYear.TableName, Config.GroupYear.ColName, Config.GroupYear.ShowName);

            columns += string.Format(",{0}.gj as '国家'", Config.GroupYear.TableName);
            groupby += string.Format(",{0}.gj", Config.GroupYear.TableName);

            tablenames.Add(Config.GroupYear.TableName);
            //tablenames.AddRange(Config.TableNames);
            string where = "";
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
                wherezt = string.Format("st_dt.ztid={0}", Config.Zid);
            }
            else
            {
                wherezt = string.Format(" and st_dt.ztid={0}", Config.Zid);
            }
            where += string.Format(" and {0}.{1} in({2})", Config.GroupYear.TableName, Config.GroupYear.ColName, Config.TopYear);
            where += string.Format(" and {0}.gj in ('中国','美国') ", Config.GroupYear.TableName);
            string tmpsql = string.Format(sql, columns, strzhibiao, tables, join, wherezt, where, groupby);

            return tmpsql;
        }
        public override bool ShowRightDataGridView(DataGridView rightgrid, ChartColumn col, string zid)
        {
            string sql = "";
            DataTable dt = null;

            if (col.TableName == "st_dt")
            {
                sql = string.Format("select {0}.{1} as '{2}',count(distinct {0}.sid) as 专利数 from {0} where ztid={3} and {0}.gj in ('中国','美国') group by {0}.{1} order by  {0}.{1} asc", col.TableName, col.ColName, col.ShowName, zid);
            }
            else
            {
                sql = string.Format("select {0}.{1} as '{2}',count(distinct {0}.sid) as 专利数 from {0},st_dt where {0}.sid=st_dt.sid and st_dt.ztid={3}  and {0}.gj in ('中国','美国') group by {0}.{1} order by {0}.{1} asc", col.TableName, col.ColName, col.ShowName, zid);
            }
            dt = DBA.MySqlDbAccess.GetDataTable(CommandType.Text, sql);
            rightgrid.DataSource = dt;
            rightgrid.Columns[rightgrid.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            return true;
        }
    }
}
