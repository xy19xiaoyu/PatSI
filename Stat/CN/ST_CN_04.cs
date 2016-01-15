using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using StatCfg;
using System.Data;
namespace Stat.CN
{
    //技术生命周期
    public class ST_CN_04 : IStatAdapter
    {
        public ST_CN_04(string name, cfg config)
            : base(name, config)
        {

        }
        public override string GetStatSQL()
        {
            string strzhibiao = "";
            string columns = "";
            string join = "";
            string groupby = "";
            string orderby = "order by count(distinct st_dt.sid) desc";
            string wherezt = "st_dt.ztid =" + Config.Zid;
            string where = "";
            string tables = "";
            string sql = "select {0},{1} from {2} where {3} {4} {5} group by {6} {7}";
            List<string> tablenames = new List<string>();
            //tablenames.Add("st_iv");
            strzhibiao = " count(distinct st_dt.sid) as '申请数量' ";

            columns = string.Format("{0}.{1} as '{2}'", Config.GroupIPC.TableName, Config.GroupIPC.ColName, Config.GroupIPC.ShowName);
            groupby = string.Format("{0}.{1}", Config.GroupIPC.TableName, Config.GroupIPC.ColName, Config.GroupIPC.ShowName);
            tablenames.Add(Config.GroupIPC.TableName);
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
                wherezt = string.Format("st_dt.ztid={0}", Zid);
            }
            else
            {
                wherezt = string.Format(" and st_dt.ztid={0}", Zid);
            }
            where += string.Format(" and {0}.{1} in({2})", Config.GroupIPC.TableName, Config.GroupIPC.ColName, Config.TopIPC);
            where += string.Format(" and st_dt.gj='中国' ");

            string tmpsql = string.Format(sql, columns, strzhibiao, tables, join, wherezt, where, groupby, orderby);

            return tmpsql;
        }
        public override bool ShowRightDataGridView(DataGridView rightgrid, ChartColumn col, string zid)
        {
            string sql = "";
            DataTable dt = null;

            if (col.TableName == "st_dt")
            {
                sql = string.Format("select {0}.{1} as '{2}',count(distinct {0}.sid) as 专利数 from {0} where and ztid={3} and {0}.gj in ('中国') group by {0}.{1} order by  count(distinct {0}.sid) desc", col.TableName, col.ColName, col.ShowName, zid);
            }
            else
            {
                sql = string.Format("select {0}.{1} as '{2}',count(distinct {0}.sid) as 专利数 from {0},st_dt where {0}.sid=st_dt.sid and st_dt.ztid={3}  and st_dt.gj in ('中国') group by {0}.{1} order by count(distinct {0}.sid) desc", col.TableName, col.ColName, col.ShowName, zid);
            }
            dt = DBA.MySqlDbAccess.GetDataTable(CommandType.Text, sql);
            rightgrid.DataSource = dt;
            rightgrid.Columns[rightgrid.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            return true;
        }
    }
}
