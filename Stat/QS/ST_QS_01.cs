using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using IStat;
using StatCfg;

namespace Stat.QS
{
    public class ST_QS_01 : IStatAdapter
    {


        public ST_QS_01(string name, cfg config)
            : base(name, config)
        {

        }

        public ST_QS_01(string name)
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
            string where = "";
            string tables = "";
            string sql = "select {0},{1} from {2} where {3} {4} {5} group by {6}";
            List<string> tablenames = new List<string>();



            strzhibiao = " count(distinct st_dt.sid) as '申请量' ";

            columns = string.Format("cast({0}.{1} as char) as '{2}'", Config.GroupYear.TableName, Config.GroupYear.ColName, Config.GroupYear.ShowName);
            groupby = string.Format("{0}.{1}", Config.GroupYear.TableName, Config.GroupYear.ColName, Config.GroupYear.ShowName);
            tablenames.Add(Config.GroupYear.TableName);
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
                wherezt = string.Format("st_dt.ztid={0}", Config.Zid);
            }
            else
            {
                wherezt = string.Format(" and st_dt.ztid={0}", Config.Zid);
            }

            where = string.Format(" and {0}.{1} in({2})", Config.GroupYear.TableName, Config.GroupYear.ColName, Config.TopYear);

            string tmpsql = string.Format(sql, columns, strzhibiao, tables, join, wherezt, where, groupby);

            return tmpsql;
        }
    }
}
