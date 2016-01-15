using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using System.Data;

namespace Stat
{
    public class RightGridHelper
    {
        public static bool ShowRightDataGridView(DataGridView rightgrid, ChartColumn col, string zid)
        {
            string sql = "";

            DataTable dt = null;
            if (col.ShowName.IndexOf("年") >= 0)
            {
                if (col.TableName == "st_dt")
                {
                    sql = string.Format("select {0}.{1} as '{2}',count(distinct {0}.sid) as 专利数 from {0} where {0}.ztid={3} group by {0}.{1} order by {0}.{1} asc", col.TableName, col.ColName, col.ShowName, zid);
                }
                else
                {
                    sql = string.Format("select {0}.{1} as '{2}',count(distinct {0}.sid) as 专利数 from {0},st_dt where {0}.sid=st_dt.sid  and st_dt.ztid={3} group by {0}.{1} order by {0}.{1} asc", col.TableName, col.ColName, col.ShowName, zid);
                }
                dt = DBA.MySqlDbAccess.GetDataTable(CommandType.Text, sql);
                dt = DataTableHelper.ReadDateTable(dt);
            }

            else
            {
                if (col.TableName == "st_dt")
                {
                    sql = string.Format("select {0}.{1} as '{2}',count(distinct {0}.sid) as 专利数 from {0} where  ztid={3} group by {0}.{1} order by count(distinct {0}.sid) desc", col.TableName, col.ColName, col.ShowName, zid);
                }
                else
                {
                    sql = string.Format("select {0}.{1} as '{2}',count(distinct {0}.sid) as 专利数 from {0},st_dt where {0}.sid=st_dt.sid and st_dt.ztid={3} group by {0}.{1} order by count(distinct {0}.sid) desc", col.TableName, col.ColName, col.ShowName, zid);
                }
                dt = DBA.MySqlDbAccess.GetDataTable(CommandType.Text, sql);
            }
            string[] types = null;
            switch (col.ShowName)
            {
                case "专利类型(含PCT)":
                    types = new string[] { "发明专利", "实用新型", "外观专利", "发明专利PCT", "实用新型PCT" };
                    break;
                case "专利类型":
                    types = new string[] { "发明专利", "实用新型", "外观专利" };
                    break;
                case "申请人类型":
                case "第一申请人类型":
                    types = new string[] { "企业", "科研院所", "高校", "事业单位", "个人" };
                    break;
                case "法律状态":
                    types = new string[] { "在审", "授权有效", "授权失效", "无效" };
                    break;
            }
            if (types != null)
            {

                DataTable tmpdt = new DataTable();
                tmpdt.Columns.Add(col.ShowName);
                tmpdt.Columns.Add("专利数");
                foreach (var type in types)
                {
                    DataRow tmprow = tmpdt.NewRow();
                    var x = from y in dt.AsEnumerable()
                            where y[0].ToString() == type
                            select y;
                    if (x.Count() == 0)
                    {
                        tmprow[0] = type;
                        tmprow[1] = 0;
                    }
                    else
                    {
                        tmprow[0] = type;
                        tmprow[1] = x.First()[1].ToString();
                    }
                    tmpdt.Rows.Add(tmprow);
                }
                rightgrid.DataSource = tmpdt;
            }
            else
            {
                rightgrid.DataSource = dt;
            }
            rightgrid.Columns[rightgrid.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            return true;
        }
        public static bool ShowRightDataGridView(DataGridView rightgrid, ChartColumn col, string zid, string zhibiao)
        {
            string sql = "";

            DataTable dt = null;

            switch (zhibiao)
            {
                case "专利存活期":
                    sql = string.Format("select {0}.{1} as '{2}',count(distinct {0}.sid) as 专利数 from {0} where  ztid={3} group by {0}.{1} order by {0}.{1}  desc", col.TableName, col.ColName, col.ShowName, zid);
                    break;
                case "审查周期":
                    sql = string.Format("select {0}.{1} as '{2}',count(distinct {0}.sid) as 专利数 from {0} where  ztid={3}  and type='发明专利' group by {0}.{1} order by {0}.{1}  desc", col.TableName, col.ColName, col.ShowName, zid);
                    break;
                case "有效专利维持期":
                    sql = string.Format("select {0}.{1} as '{2}',count(distinct {0}.sid) as 专利数 from {0} where ztid={3}  and lg='授权有效' group by {0}.{1} order by {0}.{1}  desc", col.TableName, col.ColName, col.ShowName, zid);
                    break;
                case "公知技术":
                    if (col.TableName == "st_dt")
                    {
                        sql = string.Format("select {0}.{1} as '{2}',count(distinct {0}.sid) as 专利数 from {0} where ztid={3} and st_dt.isgongzhi=1 group by {0}.{1} order by count(distinct sid) desc", col.TableName, col.ColName, col.ShowName, zid);
                    }
                    else
                    {
                        sql = string.Format("select {0}.{1} as '{2}',count(distinct {0}.sid) as 专利数 from {0},st_dt where {0}.sid=st_dt.sid and st_dt.ztid={3} and st_dt.isgongzhi=1 group by {0}.{1} order by count(distinct {0}.sid) desc", col.TableName, col.ColName, col.ShowName, zid);
                    }
                    break;
                case "":
                    break;
                default:
                    break;

            }


            dt = DBA.MySqlDbAccess.GetDataTable(CommandType.Text, sql);

            rightgrid.DataSource = dt;
            rightgrid.Columns[rightgrid.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            return true;
        }
        public static bool ShowRightDataGridViewPACount(DataGridView rightgrid, ChartColumn col, string zid)
        {
            string sql = "";
            DataTable dt = null;
            if (col.TableName == "st_dt")
            {
                sql = string.Format("select {0}.{1} as '{2}',count(distinct st_dt.sid) as 专利数, avg(st_dt.pa_sum) as '平均申请人个数' from {0}  where ztid={3} group by {0}.{1} order by count(distinct sid) desc", col.TableName, col.ColName, col.ShowName, zid);
            }
            else
            {
                sql = string.Format("select {0}.{1} as '{2}',count(distinct {0}.sid) as 专利数, avg(st_dt.pa_sum) as '平均申请人个数' from {0},st_dt where {0}.sid=st_dt.sid and st_dt.ztid={3} group by {0}.{1} order by count(distinct {0}.sid) desc", col.TableName, col.ColName, col.ShowName, zid);
            }
            dt = DBA.MySqlDbAccess.GetDataTable(CommandType.Text, sql);

            rightgrid.DataSource = dt;

            rightgrid.Columns[rightgrid.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            return true;
        }
        public static string SelectTopN(DataGridView dgw, int topn)
        {
            string selecttext = ",";
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if (i >= topn)
                {
                    dgw.Rows[i].Cells[0].Value = false;
                }
                else
                {
                    dgw.Rows[i].Cells[0].Value = true;
                    selecttext += dgw.Rows[i].Cells[1].Value + ",";
                }
            }
            return selecttext.Trim(',');
        }
        public static string GetRightdgwSelected(DataGridView dgw)
        {
            string selected = ",";
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if ((bool)dgw.Rows[i].Cells[0].Value == true)
                {
                    selected += dgw.Rows[i].Cells[1].Value.ToString() + ",";
                }

            }
            return selected.Trim(',');
        }
    }
}
