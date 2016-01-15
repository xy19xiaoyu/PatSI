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
    public class ST_ZT_09 : IStatAdapter
    {
        public ST_ZT_09(string name, cfg config)
            : base(name, config)
        {

        }
        public override bool Stat()
        {
            Dt = new DataTable();
            Dt.Columns.Add("申请人");
            Dt.Columns.Add("专利数");
            Dt.Columns.Add("合作专利数");
            Dt.Columns.Add("合作者数量");
            Dt.Columns.Add("合作者");
            Dt.Columns.Add("合作次数");

            DataTable tmpdt = DBA.MySqlDbAccess.GetDataTable(CommandType.Text, GetStatSQL());
            foreach (DataRow tmprow in tmpdt.Rows)
            {

                string pa = tmprow[0].ToString();
                string zlsum = tmprow[1].ToString();
                int hezuosum = 0;



                string gethezuosum;
                string gethezhe;
                if (Config.GroupPA.TableName.ToUpper() != "ST_DT")
                {
                    gethezuosum = string.Format("select count(distinct {0}.sid) from {0},st_dt where  {0}.sid = st_dt.sid and {0}.{1} ='{2}' and st_dt.ztid={3} and ishezuo=1", Config.GroupPA.TableName, Config.GroupPA.ColName, pa, Config.Zid);
                    gethezhe = string.Format("select st_pa.{0},count(distinct st_pa.sid) as '专利数' from st_pa ,(select sid FROM st_pa where st_pa.{0}='{1}' and st_pa.ztid ={2}) as st_pa1 where st_pa.sid = st_pa1.sid and st_pa.{0} <> '{1}' and st_pa.ztid ={2} group by st_pa.{0} order by 专利数 desc", Config.GroupPA.ColName, pa, Config.Zid);
                }
                else
                {
                    gethezuosum = string.Format("select count(distinct {0}.sid) from st_dt where {0}.{1} ='{2}' and st_dt.ztid={3} and ishezuo=1", Config.GroupPA.TableName, Config.GroupPA.ColName, pa, Config.Zid);
                    gethezhe = string.Format("select st_pa.{0},count(distinct st_pa.sid) as '专利数' from st_pa ,(select sid FROM st_pa where st_pa.{0}='{1}' and st_pa.ztid ={2}) as st_pa1 where st_pa.sid = st_pa1.sid and st_pa.{0} <> '{1}' and st_pa.ztid ={2} group by st_pa.{0} order by 专利数 desc", "pa", pa, Config.Zid);
                }
                hezuosum = Convert.ToInt32(DBA.MySqlDbAccess.ExecuteScalar(CommandType.Text, gethezuosum));

                DataTable tbins = DBA.MySqlDbAccess.GetDataTable(CommandType.Text, gethezhe);
                if (tbins.Rows.Count == 0)
                {
                    DataRow row = Dt.NewRow();
                    row[0] = pa;
                    row[1] = zlsum;
                    row[2] = 0; ;
                    row[3] = 0;
                    row[4] = "";
                    row[5] = "";
                    Dt.Rows.Add(row);
                }
                else
                {
                    foreach (DataRow inrow in tbins.Rows)
                    {
                        DataRow row = Dt.NewRow();

                        string strpa = "";
                        string intpasum = "";
                        strpa = inrow[0].ToString();
                        intpasum = inrow[1].ToString();
                        if (strpa.Trim() == "" && intpasum == "0")
                        {
                            row[0] = pa;
                            row[1] = zlsum;
                            row[2] = 0; ;
                            row[3] = 0;
                            row[4] = "";
                            row[5] = "";
                        }
                        else
                        {
                            row[0] = pa;
                            row[1] = zlsum;
                            row[2] = hezuosum;
                            row[3] = tbins.Rows.Count;
                            row[4] = strpa;
                            row[5] = intpasum;
                        }
                        Dt.Rows.Add(row);
                    }
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
            strzhibiao = "count(distinct st_dt.sid) as '申请数量' ";


            columns += string.Format("{0}.{1} as '{2}'", Config.GroupPA.TableName, Config.GroupPA.ColName, Config.GroupPA.ShowName);
            groupby += string.Format("{0}.{1}", Config.GroupPA.TableName, Config.GroupPA.ColName, Config.GroupPA.ShowName);

            tablenames.Add(Config.GroupPA.TableName);

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
            where += string.Format(" and {0}.{1} in({2})", Config.GroupPA.TableName, Config.GroupPA.ColName, Config.TopPA);
            string tmpsql = string.Format(sql, columns, strzhibiao, tables, join, wherezt, where, groupby, orderby);

            return tmpsql;
        }
        public override bool ExportExcel(string FileName, string ImageFileName)
        {
            ExcelHelper eh = new ExcelHelper();
            eh.DataTable2ExcelFileAndMerge(Dt, FileName, new List<int>() { 0, 1, 2 });
            return true;
        }
    }
}
