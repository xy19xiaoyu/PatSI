using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using StatCfg;
namespace Stat.LG
{
    //技术生命周期
    public class ST_LG_03 : IStatAdapter
    {
        public ST_LG_03(string name, cfg config)
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
            string where = " and lg='授权有效'";
            string tables = "";
            string sql = "select {0},{1} from {2} where {3} {4} {5} group by {6} {7}";
            List<string> tablenames = new List<string>();
            //tablenames.Add("st_iv");
            strzhibiao = " count(distinct st_dt.sid) as '申请数量' ";

            columns = string.Format("{0}.{1} as '{2}'", Config.GroupCS.TableName, Config.GroupCS.ColName, Config.GroupCS.ShowName);
            groupby = string.Format("{0}.{1}", Config.GroupCS.TableName, Config.GroupCS.ColName, Config.GroupCS.ShowName);
            tablenames.Add(Config.GroupCS.TableName);
            //tablenames.AddRange(Config.TableNames);

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
            where += string.Format(" and {0}.{1} in({2})", Config.GroupCS.TableName, Config.GroupCS.ColName, Config.TopCS);

            string tmpsql = string.Format(sql, columns, strzhibiao, tables, join, wherezt, where, groupby, orderby);

            return tmpsql;
        }
    }
}
