using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using StatCfg;
using System.Data;
using ExcelLib;
namespace Stat.ZT
{
    public class ST_ZT_08 : IStatAdapter
    {
        public ST_ZT_08(string name, cfg config)
            : base(name, config)
        {

        }
        public override bool Stat()
        {
            Dt = new DataTable();
            Dt.Columns.Add("申请人");
            Dt.Columns.Add("发明人个数");
            Dt.Columns.Add("前五发明人");
            Dt.Columns.Add("发明人专利数");

            DataTable tmpdt = DBA.MySqlDbAccess.GetDataTable(CommandType.Text, GetStatSQL());
            foreach (DataRow tmprow in tmpdt.Rows)
            {

                string pa = tmprow[0].ToString();
                int insum = 0;



                string getinsum = string.Format("select count(*) from {0} where {0}.{1} ='{2}' and ztid={3}", Config.GroupPA.TableName, Config.GroupPA.ColName, pa, Config.Zid);
                insum = Convert.ToInt32(DBA.MySqlDbAccess.ExecuteScalar(CommandType.Text, getinsum));
                tmprow[1] = insum;
                string gettop5in = string.Format("select st_iv.iv,count(distinct st_iv.sid) from st_iv,{0} where st_iv.sid =  {0}.sid and  {0}.{1} ='{2}' and st_iv.ztid={3} group by st_iv.iv order by count(distinct st_iv.sid) desc limit 5", Config.GroupPA.TableName, Config.GroupPA.ColName, pa, Config.Zid);
                DataTable tbins = DBA.MySqlDbAccess.GetDataTable(CommandType.Text, gettop5in);
                foreach (DataRow inrow in tbins.Rows)
                {
                    DataRow row = Dt.NewRow();

                    string strin = "";
                    string inzlsum = "";
                    strin = inrow[0].ToString();
                    inzlsum = inrow[1].ToString();
                    row[0] = pa;
                    row[1] = insum;
                    row[2] = strin;
                    row[3] = inzlsum;
                    Dt.Rows.Add(row);
                }


            }
            return true;
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


            columns += string.Format("{0}.{1} as '{2}'", Config.GroupPA.TableName, Config.GroupPA.ColName, Config.GroupPA.ShowName);
            groupby += string.Format("{0}.{1}", Config.GroupPA.TableName, Config.GroupPA.ColName, Config.GroupPA.ShowName);

            tablenames.Add(Config.GroupPA.TableName);

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
            where += string.Format(" and {0}.{1} in({2})", Config.GroupPA.TableName, Config.GroupPA.ColName, Config.TopPA);
            string tmpsql = string.Format(sql, columns, strzhibiao, tables, join, wherezt, where, groupby, orderby);

            return tmpsql;
        }
        public override bool ExportExcel(string FileName, string ImageFileName)
        {
            ExcelHelper eh = new ExcelHelper();
            eh.DataTable2ExcelFileAndMerge(Dt, FileName, new List<int>() { 0, 1 });
            return true;
        }
    }
}
