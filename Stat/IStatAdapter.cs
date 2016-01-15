using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using System.Data;
using StatCfg;
using DAL;
using ExcelLib;
namespace Stat
{
    public class IStatAdapter : IStat.IStat
    {
        private string name;
        private int id;
        private string zid;
        private string strsql;

        private Chart chart;
        private DataGridView dgw;
        private DataTable dt;
        private DataTable dt1;

        public DataTable Dt1
        {
            get { return dt1; }
            set { dt1 = value; }
        }
        private cfg config;
        public IStatAdapter(string name)
        {
            this.name = name;
        }
        public IStatAdapter(string name, cfg config)
        {
            this.name = name;
            this.config = config;
        }

        public string strSQL
        {
            get
            {
                return strsql;
            }
            set
            {
                strsql = value;
            }
        }

        public DataTable Dt
        {
            get
            {
                return dt;
            }
            set
            {
                dt = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Zid
        {
            get
            {
                return config.Zid;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public StatCfg.cfg Config
        {
            get
            {
                return config;
            }
            set
            {
                config = value;
            }
        }

        public Chart msChart
        {
            get
            {
                return chart;
            }
            set
            {
                chart = value;
            }
        }

        public DataGridView Gridview
        {
            get
            {
                return dgw;
            }
            set
            {
                dgw = value;
            }
        }

        public virtual string GetStatSQL()
        {
            return strsql;
        }

        public virtual bool Stat()
        {
            strSQL = GetStatSQL();
            dt = DBA.MySqlDbAccess.GetDataTable(CommandType.Text, strSQL);
            return true;
        }

        public virtual bool ExportExcel(string FileName, string ImageFileName)
        {
            NPOIHelper.Export(dt, FileName, "", ImageFileName);
            return true;
        }

        public virtual bool DrawingLineChart()
        {
            this.msChart.Series.Clear();
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                this.msChart.Series.Add(dt.Columns[i].ColumnName);
            }
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                this.msChart.Series[dt.Columns[i].ColumnName].ChartType = Config.ChartType;
                msChart.Series[dt.Columns[i].ColumnName].BorderWidth = 3;
            }
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    this.msChart.Series[dt.Columns[i].ColumnName].Points.Add(new System.Windows.Forms.DataVisualization.Charting.DataPoint(Convert.ToDouble(row[0].ToString()), Convert.ToDouble(row[i].ToString())));
                }
            }
            iniLabPoint();
            return true;
        }

        public virtual bool DrawingPicChart()
        {
            this.msChart.Series.Clear();
            this.msChart.Series.Add("Default");
            msChart.Series["Default"].ChartType = Config.ChartType;
            msChart.Series["Default"]["PieLabelStyle"] = Config.PieLabelStyle;

            //if (config.ChartType == SeriesChartType.Column)
            //{
            //    msChart.Series["Default"].LegendText = dt.Columns[1].ColumnName;
            //}

            int count = dt.Rows.Count;
            double[] yValues = new double[count];
            string[] xValues = new string[count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                if (row[0].ToString() == "")
                {
                    row[0] = "未知";
                }
                DataPoint p = new DataPoint(0D, Convert.ToDouble(row[1].ToString()));
                if (Config.ChartType != SeriesChartType.Column)
                {
                    p.IsValueShownAsLabel = true;
                    p.IsVisibleInLegend = true;
                    p.Label = row[0].ToString() + "(" + row[2].ToString() + ")";
                    p.LegendText = row[0].ToString();
                }
                msChart.Series["Default"].Points.Add(p);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                msChart.Series[0].Points[i].AxisLabel = dt.Rows[i][0].ToString();
            }
            iniLabPoint();
            return true;
        }

        public virtual bool DrawingColumnChart()
        {
            this.msChart.Series.Clear();
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName.IndexOf("占比") > 0) continue;

                this.msChart.Series.Add(dt.Columns[i].ColumnName);
            }
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName.IndexOf("占比") > 0) continue;

                this.msChart.Series[dt.Columns[i].ColumnName].ChartType = Config.ChartType;
            }
            for (int j = 1; j < dt.Columns.Count; j++)
            {
                int count = dt.Rows.Count;
                double[] yValues = new double[count];
                string[] xValues = new string[count];
                if (dt.Columns[j].ColumnName.IndexOf("占比") > 0) continue;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    if (row[0].ToString() == "")
                    {
                        row[0] = "未知";
                    }
                    yValues[i] = Convert.ToDouble(row[j].ToString());
                    xValues[i] = row[0].ToString();
                }
                msChart.Series[dt.Columns[j].ColumnName].Points.DataBindXY(xValues, yValues);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                msChart.Series[0].Points[i].AxisLabel = dt.Rows[i][0].ToString();
            }

            iniLabPoint();
            return true;
        }

        public bool DrawingStackedColumnChart()
        {
            this.msChart.Series.Clear();

            for (int j = 1; j < dt.Columns.Count; j++)
            {
                string name = dt.Columns[j].ToString();
                Series tmp = new Series(name) { ChartType = Config.ChartType };
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    if (row[0].ToString() == "")
                    {
                        row[0] = "未知";
                    }

                    tmp.Points.AddY(Convert.ToDouble(row[j].ToString()));

                }

                this.msChart.Series.Add(tmp);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                msChart.Series[0].Points[i].AxisLabel = dt.Rows[i][0].ToString();
            }
            iniLabPoint();
            return true;
        }

        public virtual bool ShowDataGridView()
        {
            //this.dgw.Columns.Clear();
            //if (dt1 == null) dt1 = dt;
            this.dgw.DataSource = dt;
            for (int i = 0; i < dgw.Columns.Count - 1; i++)
            {
                dgw.Columns[dgw.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            }

            return true;
        }






        public bool DrawingPointChart()
        {
            Double minnum = DateTime.Now.Year;
            Double maxnum = DateTime.Now.Year;
            Double tmpminnum;
            this.msChart.Series.Clear();
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                this.msChart.Series.Add(dt.Columns[i].ColumnName);
            }
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                this.msChart.Series[dt.Columns[i].ColumnName].ChartType = Config.ChartType; ;
                msChart.Series[dt.Columns[i].ColumnName].MarkerStyle = MarkerStyle.Star5; //圆点
                msChart.Series[dt.Columns[i].ColumnName].MarkerSize = 15; //圆点

                msChart.Series[dt.Columns[i].ColumnName].BorderWidth = 3;
            }
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    tmpminnum = Convert.ToDouble(dt.Rows[j][i].ToString());
                    if (tmpminnum < minnum)
                    {
                        minnum = tmpminnum;
                    }
                    if (tmpminnum > maxnum)
                    {
                        maxnum = tmpminnum;
                    }
                    this.msChart.Series[dt.Columns[i].ColumnName].Points.AddY(tmpminnum);
                }
                msChart.Series[0].Points[j].AxisLabel = dt.Rows[j][0].ToString();
            }

            this.msChart.ChartAreas[0].AxisY.Maximum = maxnum;
            this.msChart.ChartAreas[0].AxisY.Minimum = minnum - 10;
            iniLabPoint();
            return true;
        }

        public void iniLabPoint()
        {
            foreach (Series series in msChart.Series)
            {
                if (config.LabPoint != "None")
                {
                    series.IsValueShownAsLabel = true;
                    if (config.LabPoint != "Auto")
                    {
                        series["LabelStyle"] = config.LabPoint;
                    }
                }
                else
                {
                    series.IsValueShownAsLabel = false;
                }
            }

        }

        public virtual bool ShowRightDataGridView(DataGridView rightgrid, ChartColumn col, string zid)
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
                    sql = string.Format("select {0}.{1} as '{2}',count(distinct {0}.sid) as 专利数 from {0},st_dt where {0}.sid=st_dt.sid and st_dt.ztid={3} group by {0}.{1} order by {0}.{1} asc", col.TableName, col.ColName, col.ShowName, zid);
                }
                dt = DBA.MySqlDbAccess.GetDataTable(CommandType.Text, sql);
                dt = DataTableHelper.ReadDateTable(dt);
            }

            else
            {
                if (col.TableName == "st_dt")
                {
                    sql = string.Format("select {0}.{1} as '{2}',count(distinct {0}.sid) as 专利数 from {0} where ztid={3} group by {0}.{1} order by count(distinct {0}.sid) desc", col.TableName, col.ColName, col.ShowName, zid);
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
    }
}
