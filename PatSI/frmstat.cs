using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.UIHelper;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using StatCfg;
using Stat.QS;
using Stat.QY;
using Stat.ZT;
using Stat;
using DAL;
using IStat;
using Stat.JS;
using Stat.LG;
using ExcelLib;
using System.Diagnostics;

namespace PatSI
{
    public partial class frmstat : Form
    {
        public string dbtype;
        public string dbname;
        public int zid;
        public int index = 0;
        private DataTable dt;
        private cfg config = new cfg();
        IStat.IStat st;
        private string stname;




        public frmstat()
        {
            InitializeComponent();
            showdata.CellPainting += new DataGridViewCellPaintingEventHandler(showdata_CellPainting);
        }
        public frmstat(string dbname, int zid, string dbtype)
        {
            InitializeComponent();
            this.dbname = dbname;
            this.dbtype = dbtype;
            this.zid = zid;
            config.Zid = zid.ToString();
            this.tabRight.SelectedIndex = 1;
            this.numQY.Enabled = this.rbQYTop.Checked;

            showdata.CellPainting += new DataGridViewCellPaintingEventHandler(showdata_CellPainting);


        }

        private void Form2_Load(object sender, EventArgs e)
        {
            tabRight.TabPages.Clear();
            tabRight.TabPages.Add(tbChartConfig);
            resizeMiddlePanel();
            STUIHelper.initLeftTree(this.twLeft, dbtype);
            ChartPaletteHelper.IniChartPalette(cmbChartPalette);
            STUIHelper.iniLabelPoint(cmbLabelPoint);
            cmbLabelStyle.SelectedIndex = 0;
            this.Text = string.Format("统计分析-{0}", dbname);
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            int width = e.X;

            if (width > 350)
            {
                width = 350;
            }
            if (width < 150)
            {
                width = 23;

            }
            this.panLeft.Width = width;
            resizeMiddlePanel();
        }

        private void splZY_DoubleClick(object sender, EventArgs e)
        {

            if (splZY.Location.X > 23 && splZY.Location.X <= 220)
            {
                this.panLeft.Width = 23;
            }
            else if (splZY.Location.X > 220 && splZY.Location.X < 350)
            {
                this.panLeft.Width = 350;
            }
            else if (splZY.Location.X == 350 || splZY.Location.X == 23)
            {
                this.panLeft.Width = 220;
            }
            resizeMiddlePanel();
        }


        private void showLine1(DataTable dt)
        {
            this.chart1.Series.Clear();
            this.chart1.Series.Add("生命周期");
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                this.chart1.Series["生命周期"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series["生命周期"].MarkerStyle = MarkerStyle.Circle; //圆点
                chart1.Series["生命周期"].MarkerSize = 9;
                chart1.Series["生命周期"].BorderWidth = 3;
                this.chart1.Series["生命周期"].IsValueShownAsLabel = true;
            }
            index = 0;
            System.Threading.Thread.Sleep(100 * 3);
            timer1.Enabled = true;
        }


        private void panLeft_Resize(object sender, EventArgs e)
        {
            resizeMiddlePanel();
        }
        private void spRight_DoubleClick(object sender, EventArgs e)
        {
            this.spRight.Visible = false;
            resizeMiddlePanel();
        }

        private void resizeMiddlePanel(int intpanlrigh = 250)
        {
            int intspleft = 0;
            int intspright = 0;
            int intpanleft = panLeft.Width;

            if (splZY.Visible)
            {
                intspleft = 5;
            }
            if (spRight.Visible)
            {
                intspright = 5;
            }
            else
            {
                intpanlrigh = 35;
            }
            pnlMiddle.Width = this.Width - intspleft - intspright - intpanleft - intpanlrigh;
        }





        private void tbLeft_Click(object sender, EventArgs e)
        {
            this.panLeft.Width = 220;
            resizeMiddlePanel();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            config.PieLabelStyle = picLabelStyles[cmbLabelStyle.Text];
            try
            {
                st = StatFactury.CreateStata(this.lib_Name.Text, config, chart1, showdata);
            }
            catch (Exception ex)
            {
                MessageBox.Show("请在左侧选择统计指标后，点击【生成表图】", "提示");
                return;
            }
            pnlChart.Visible = true;
            btnExport.Visible = true;
            resizeMiddlePanel();
            List<string> cols;
            List<string> rows;
            List<int> colindexs;
            this.chart1.Titles[0].Text = this.lib_Name.Text;
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.IntervalOffset = 1;
            chart1.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
            switch (this.lib_Name.Text)
            {
                #region 趋势
                case "专利趋势分析":
                    st.Stat();
                    st.Dt = DataTableHelper.ReadDateTable(st.Dt);
                    st.DrawingLineChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "技术生命周期分析":
                    st.Stat();
                    st.Dt = DataTableHelper.ReadDateTable(st.Dt);
                    this.chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                    chart1.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
                    chart1.ChartAreas[0].AxisX.Interval = 0;
                    chart1.ChartAreas[0].AxisX.IntervalOffset = 0;
                    st.DrawingLineChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.ShowDataGridView();
                    //dt = st.Dt;
                    //showLine1(st.Dt);
                    break;
                case "发明人增速趋势分析":
                    st.Stat();
                    st.Dt = DataTableHelper.ReadDateTable(st.Dt);
                    st.DrawingLineChart();
                    st.Dt = DataTableHelper.AddZengSu(st.Dt, new List<int>() { 1 });
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();

                    break;
                case "专利类型分布趋势分析":
                    st.Stat();
                    cols = config.TopType.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.ExchangeRowColumn(st.Dt, cols);
                    st.Dt = DataTableHelper.ReadDateTable(st.Dt);
                    st.DrawingLineChart();
                    colindexs = new List<int>();
                    for (int i = 1; i <= cols.Count; i++)
                    {
                        colindexs.Add(i);
                    }
                    this.showdata.Columns.Clear();
                    st.Dt = DataTableHelper.AddZengSu(st.Dt, colindexs);
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                #endregion
                #region 区域

                case "区域专利申请比重分析":
                    st.Stat();
                    st.Dt = DataTableHelper.AddPercentColumn(st.Dt, new List<int>() { 1 });
                    st.DrawingPicChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    grpPic.Visible = true;
                    break;
                case "区域专利申请趋势分析":
                    st.Stat();
                    st.Dt = DataTableHelper.ExchangeRowColumn(st.Dt);
                    st.Dt = DataTableHelper.ReadDateTable(st.Dt);
                    st.DrawingLineChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "区域专利申请人分析":
                    st.Stat();
                    cols = config.TopPA.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.ExchangeRowColumn(st.Dt, cols);
                    st.DrawingColumnChart();
                    rows = config.TopQY.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.FillColumn(st.Dt, rows);
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "区域专利发明人分析":
                    st.Stat();
                    cols = config.TopIN.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.ExchangeRowColumn(st.Dt, cols);
                    st.DrawingColumnChart();
                    rows = config.TopQY.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.FillColumn(st.Dt, rows);
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "区域专利技术构成分析":
                    st.Stat();
                    cols = config.TopIPC.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.ExchangeRowColumn(st.Dt, cols);
                    st.DrawingColumnChart();
                    rows = config.TopQY.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.FillColumn(st.Dt, rows);
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "区域首次专利申请分析":
                    st.Stat();
                    st.DrawingPointChart();
                    rows = config.TopQY.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.FillColumn(st.Dt, rows);
                    st.ShowDataGridView();
                    break;

                #endregion
                #region 申请人
                //申请人专利排名分析
                case "申请人专利排名分析":
                    st.Stat();
                    grpPic.Visible = true;
                    rows = config.TopPA.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.FillColumn(st.Dt, rows);
                    st.Dt = DataTableHelper.AddPercentColumn(st.Dt, new List<int>() { 1 });
                    st.DrawingPicChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "申请人类型分析":
                    st.Stat();
                    cols = config.TopPAType.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.FillColumn(st.Dt, cols);
                    st.DrawingColumnChart();
                    st.Dt = DataTableHelper.AddPercentColumn(st.Dt, new List<int>() { 1 });
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "申请人趋势分析":
                    st.Stat();
                    cols = config.TopPA.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.ExchangeRowColumn(st.Dt, cols);
                    st.DrawingLineChart();
                    rows = config.TopYear.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.FillColumn1(st.Dt, rows);
                    st.Dt = DataTableHelper.ReadDateTable(st.Dt);
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "申请人首次专利申请分析":
                    st.Stat();
                    st.DrawingPointChart();
                    rows = config.TopPA.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.FillColumn(st.Dt, rows);
                    st.ShowDataGridView();
                    break;
                case "申请人专利类型分析":
                    st.Stat();
                    cols = config.TopType.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.ExchangeRowColumn(st.Dt, cols);
                    st.DrawingColumnChart();
                    rows = config.TopPA.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.FillColumn(st.Dt, rows);
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                //申请人专利布局动向分析
                case "申请人专利布局动向分析":
                    st.Stat();
                    cols = config.TopQY.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.ExchangeRowColumn(st.Dt, cols);
                    st.DrawingColumnChart();
                    rows = config.TopPA.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.FillColumn(st.Dt, rows);
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "申请人专利技术构成分析":
                    st.Stat();
                    st.Dt = DataTableHelper.ExchangeRowColumn(st.Dt);
                    st.DrawingStackedColumnChart();
                    rows = config.TopPA.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.FillColumn(st.Dt, rows);
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "申请人研发阵容分析":
                    st.Stat();
                    rows = config.TopPA.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.FillColumn(st.Dt, rows);
                    st.ShowDataGridView();
                    pnlChart.Visible = false;
                    break;
                case "申请人专利合作分析":
                    st.Stat();
                    rows = config.TopPA.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.FillColumn(st.Dt, rows);
                    st.ShowDataGridView();
                    pnlChart.Visible = false;
                    break;
                case "发明人专利排名分析":
                    st.Stat();
                    grpPic.Visible = true;
                    rows = config.TopIN.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.FillColumn(st.Dt, rows);
                    st.Dt = DataTableHelper.AddPercentColumn(st.Dt, new List<int>() { 1 });
                    st.DrawingPicChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "发明人专利技术构成分析":
                    st.Stat();
                    st.Dt = DataTableHelper.ExchangeRowColumn(st.Dt);
                    st.DrawingStackedColumnChart();
                    rows = config.TopIN.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.FillColumn(st.Dt, rows);
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;

                #endregion
                #region 技术
                case "专利技术构成分析":
                    st.Stat();
                    grpPic.Visible = true;
                    st.Dt = DataTableHelper.AddPercentColumn(st.Dt, new List<int>() { 1 });
                    st.DrawingPicChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "专利技术趋势分析":
                    st.Stat();
                    st.Dt = DataTableHelper.ExchangeRowColumn(st.Dt);
                    st.Dt = DataTableHelper.ReadDateTable(st.Dt);
                    st.DrawingLineChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "专利技术区域分析":
                    st.Stat();
                    st.Dt = DataTableHelper.ExchangeRowColumn(st.Dt);
                    st.DrawingStackedColumnChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "专利技术申请人分析":
                    st.Stat();
                    st.Dt = DataTableHelper.ExchangeRowColumn(st.Dt);
                    st.DrawingStackedColumnChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;

                #endregion
                #region 法律状态
                case "专利法律状态分析":
                    st.Stat();
                    cols = config.TopLG.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.FillColumn(st.Dt, cols);
                    grpPic.Visible = true;
                    st.Dt = DataTableHelper.AddPercentColumn(st.Dt, new List<int>() { 1 });
                    st.DrawingPicChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "专利存活期分析":
                    st.Stat();
                    grpPic.Visible = true;
                    st.Dt = DataTableHelper.AddPercentColumn(st.Dt, new List<int>() { 1 });
                    st.DrawingPicChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "有效专利维持期分析":
                    st.Stat();
                    grpPic.Visible = true;
                    st.Dt = DataTableHelper.AddPercentColumn(st.Dt, new List<int>() { 1 });
                    st.DrawingPicChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "申请人法律状态分析":
                    st.Stat();
                    cols = config.TopLG.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.ExchangeRowColumn(st.Dt, cols);
                    st.DrawingStackedColumnChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "区域法律状态分析":
                    st.Stat();
                    grpPic.Visible = true;
                    cols = config.TopLG.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.ExchangeRowColumn(st.Dt, cols);
                    st.DrawingStackedColumnChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "技术领域法律状态分析":
                    st.Stat();
                    grpPic.Visible = true;
                    cols = config.TopLG.Replace("'", "").Split(',').ToList<string>();
                    st.Dt = DataTableHelper.ExchangeRowColumn(st.Dt, cols);
                    st.DrawingStackedColumnChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "公知技术统计分析":
                    st.Stat();
                    st.Dt = DataTableHelper.AddPercentColumn(st.Dt, new List<int>() { 1 });
                    st.DrawingPicChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "专利审查周期分析":
                    st.Stat();
                    st.Dt = DataTableHelper.AddPercentColumn(st.Dt, new List<int>() { 1 });
                    st.DrawingPicChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "法律状态自定义分析":
                    break;
                case "中国本土专利趋势":
                    st.Stat();
                    grpPic.Visible = true;
                    st.Dt = DataTableHelper.ReadDateTable(st.Dt);
                    st.DrawingLineChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "中美市场专利趋势":
                    st.Stat();
                    grpPic.Visible = true;
                    cols = new List<string>() { "中国", "美国" };
                    st.Dt = DataTableHelper.ExchangeRowColumn(st.Dt, cols);
                    st.Dt = DataTableHelper.ReadDateTable(st.Dt);
                    st.DrawingLineChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "中欧市场专利趋势":
                    st.Stat();
                    cols = new List<string>() { "中国", "欧洲专利局" };
                    st.Dt = DataTableHelper.ExchangeRowColumn(st.Dt, cols);
                    st.Dt = DataTableHelper.ReadDateTable(st.Dt);
                    st.DrawingLineChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "中国市场重点技术排行榜":
                    st.Stat();
                    grpPic.Visible = true;
                    st.Dt = DataTableHelper.AddPercentColumn(st.Dt, new List<int>() { 1 });
                    st.DrawingPicChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "中国市场专利申请人排行榜":
                    st.Stat();
                    grpPic.Visible = true;
                    st.Dt = DataTableHelper.AddPercentColumn(st.Dt, new List<int>() { 1 });
                    st.DrawingPicChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "中国市场布局国家排行榜":
                    st.Stat();
                    grpPic.Visible = true;
                    st.Dt = DataTableHelper.AddPercentColumn(st.Dt, new List<int>() { 1 });
                    st.DrawingPicChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;


                case "美国本土专利趋势":
                    st.Stat();
                    grpPic.Visible = true;
                    st.Dt = DataTableHelper.ReadDateTable(st.Dt);
                    st.DrawingLineChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "美欧市场专利趋势":
                    st.Stat();
                    cols = new List<string>() { "美国", "欧洲专利局" };
                    st.Dt = DataTableHelper.ExchangeRowColumn(st.Dt, cols);
                    st.Dt = DataTableHelper.ReadDateTable(st.Dt);
                    st.DrawingLineChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "美国市场重点技术排行榜":
                    st.Stat();
                    grpPic.Visible = true;
                    st.Dt = DataTableHelper.AddPercentColumn(st.Dt, new List<int>() { 1 });
                    st.DrawingPicChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "美国市场专利申请人排行榜":
                    st.Stat();
                    grpPic.Visible = true;
                    st.Dt = DataTableHelper.AddPercentColumn(st.Dt, new List<int>() { 1 });
                    st.DrawingPicChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "美国市场布局国家排行榜":
                    st.Stat();
                    grpPic.Visible = true;
                    st.Dt = DataTableHelper.AddPercentColumn(st.Dt, new List<int>() { 1 });
                    st.DrawingPicChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "欧洲本土专利趋势":
                    st.Stat();
                    grpPic.Visible = true;
                    st.Dt = DataTableHelper.ReadDateTable(st.Dt);
                    st.DrawingLineChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "德国市场专利趋势":
                    st.Stat();
                    cols = new List<string>() { "德国" };
                    st.Dt = DataTableHelper.ExchangeRowColumn(st.Dt, cols);
                    st.Dt = DataTableHelper.ReadDateTable(st.Dt);
                    st.DrawingLineChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "欧洲市场重点技术排行榜":
                    st.Stat();
                    grpPic.Visible = true;
                    st.Dt = DataTableHelper.AddPercentColumn(st.Dt, new List<int>() { 1 });
                    st.DrawingPicChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "欧洲市场专利申请人排行榜":
                    st.Stat();
                    grpPic.Visible = true;
                    st.Dt = DataTableHelper.AddPercentColumn(st.Dt, new List<int>() { 1 });
                    st.DrawingPicChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "欧洲市场布局国家排行榜":
                    st.Stat();
                    grpPic.Visible = true;
                    st.Dt = DataTableHelper.AddPercentColumn(st.Dt, new List<int>() { 1 });
                    st.DrawingPicChart();
                    st.Dt = DataTableHelper.AddSumRow(st.Dt);
                    st.Dt = DataTableHelper.AddSumColumn(st.Dt);
                    st.ShowDataGridView();
                    break;
                case "国内专利储备不足预警":
                case "国内专利数量泡沫预警":
                case "国内专利创新增速预警":
                case "国内领军者专利创新力预警":
                case "国内专利组合集中度预警":
                case "美国市场专利壁垒预警":
                case "欧洲市场专利壁垒预警":
                case "日本市场专利壁垒预警":
                    pnlChart.Visible = false;
                    st.Stat();
                    st.ShowDataGridView();
                    break;
                case "国内专利技术差距预警":
                case "国外重点国家布局预警":
                    st.Stat();
                    grpPic.Visible = true;
                    st.DrawingColumnChart();
                    st.ShowDataGridView();
                    break;
                #endregion

            }
        }

        private void chkFirstPA_CheckedChanged(object sender, EventArgs e)
        {
            //stconfg.UseFirstPA = chkFirstPA.Checked;
        }

        private void chkFirstIN_CheckedChanged(object sender, EventArgs e)
        {
            //stconfg.UseFirstIN = chkFirstIN.Checked;
        }
        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    if (index >= dt.Rows.Count)
        //    {
        //        timer1.Enabled = false;
        //        return;
        //    }
        //    DataRow row = dt.Rows[index];
        //    this.chart1.Series["生命周期"].Points.Add(new System.Windows.Forms.DataVisualization.Charting.DataPoint(Convert.ToDouble(row[1].ToString()), Convert.ToDouble(row[2].ToString())) { Label = row[0].ToString() + "年" });
        //    index++;
        //}

        private void tbChartConfig_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.spRight.Visible = false;
            resizeMiddlePanel();
        }



        private void tabRight_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.spRight.Visible = true;
            if (tabRight.SelectedTab == null) return;
            if ("区域选项,申请人选项,发明人选项,技术分类选项,法律状态选项".Contains(tabRight.SelectedTab.Text))
            {
                resizeMiddlePanel(400);
            }
            else
            {
                resizeMiddlePanel();
            }

        }
        #region 区域

        private void cmbQY_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChartColumn group = (ChartColumn)cmbQY.SelectedItem;
            try
            {
                IStat.IStat st = StatFactury.CreateStata(this.lib_Name.Text, config, chart1, showdata);
                st.ShowRightDataGridView(dgwQY, group, zid.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR", "提示");
                return;
            }
            config.GroupQY = group;

        }
        private void chkTOP_CheckedChanged(object sender, EventArgs e)
        {
            this.numQY.Enabled = this.rbQYTop.Checked;
            if (rbQYTop.Checked)
            {
                config.TopQY = RightGridHelper.SelectTopN(dgwQY, (int)this.numQY.Value);
            }
        }
        private void dgwQY_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex < 0) return;
            if ((bool)dgwQY.Rows[e.RowIndex].Cells[0].Value == true)
            {
                dgwQY.Rows[e.RowIndex].Cells[0].Value = false;
            }
            else
            {
                dgwQY.Rows[e.RowIndex].Cells[0].Value = true;
            }
            config.TopQY = RightGridHelper.GetRightdgwSelected(dgwQY);
        }

        private void numTOP_ValueChanged(object sender, EventArgs e)
        {
            if (rbQYTop.Checked)
            {
                config.TopQY = RightGridHelper.SelectTopN(dgwQY, (int)this.numQY.Value);
            }
        }

        private void radQYSelectALL_CheckedChanged(object sender, EventArgs e)
        {
            RightGridHelper.SelectTopN(dgwQY, dgwQY.Rows.Count);
            config.TopQY = RightGridHelper.GetRightdgwSelected(dgwQY);
            this.numQY.Enabled = this.rbQYTop.Checked;
        }

        private void rbQYTop_CheckedChanged(object sender, EventArgs e)
        {
            this.numQY.Enabled = this.rbQYTop.Checked;
            if (rbQYTop.Checked)
            {
                config.TopQY = RightGridHelper.SelectTopN(dgwQY, (int)this.numQY.Value);
            }
        }

        private void dgwQY_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            config.TopQY = RightGridHelper.SelectTopN(dgwQY, (int)this.numQY.Value);
        }
        #endregion

        #region 申请人右侧

        private void cmbPA_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChartColumn group = (ChartColumn)cmbPA.SelectedItem;
            switch (this.lib_Name.Text)
            {
                case "申请人专利合作分析":
                    RightGridHelper.ShowRightDataGridViewPACount(dgwPA, group, zid.ToString());
                    break;
                case "公知技术统计分析":
                    RightGridHelper.ShowRightDataGridView(dgwPA, group, zid.ToString(), "公知技术");
                    break;
                default:
                    try
                    {
                        IStat.IStat st = StatFactury.CreateStata(this.lib_Name.Text, config, chart1, showdata);
                        st.ShowRightDataGridView(dgwPA, group, zid.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR", "提示");
                        return;
                    }
                    break;
            }
            rbPAtop.Checked = true;
            config.GroupPA = group;
        }

        private void rbPAselectall_CheckedChanged(object sender, EventArgs e)
        {
            config.TopPA = RightGridHelper.SelectTopN(dgwPA, dgwPA.Rows.Count);
            this.numPA.Enabled = this.rbPAtop.Checked;
        }

        private void rbPAtop_CheckedChanged(object sender, EventArgs e)
        {
            this.numPA.Enabled = this.rbPAtop.Checked;
            config.TopPA = RightGridHelper.SelectTopN(dgwPA, (int)numPA.Value);
        }

        private void numPA_ValueChanged(object sender, EventArgs e)
        {
            config.TopPA = RightGridHelper.SelectTopN(dgwPA, (int)numPA.Value);
        }

        private void dgwPA_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex < 0) return;
            if ((bool)dgwPA.Rows[e.RowIndex].Cells[0].Value == true)
            {
                dgwPA.Rows[e.RowIndex].Cells[0].Value = false;
            }
            else
            {
                dgwPA.Rows[e.RowIndex].Cells[0].Value = true;
            }
            config.TopPA = RightGridHelper.GetRightdgwSelected(dgwPA);
        }
        private void dgwPA_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            config.TopPA = RightGridHelper.SelectTopN(dgwPA, (int)this.numPA.Value);
        }
        #endregion
        #region 发明人
        private void cmbIN_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChartColumn group = (ChartColumn)cmbIN.SelectedItem;
            RightGridHelper.ShowRightDataGridView(dgwIN, group, zid.ToString());
            config.GroupIN = group;
            rbINtop.Checked = true;
        }

        private void rbINselectall_CheckedChanged(object sender, EventArgs e)
        {
            config.TopIN = RightGridHelper.SelectTopN(dgwIN, dgwIN.Rows.Count);
        }

        private void rbINtop_CheckedChanged(object sender, EventArgs e)
        {
            this.numIN.Enabled = this.rbINtop.Checked;
            config.TopIN = RightGridHelper.SelectTopN(dgwIN, (int)numIN.Value);
        }

        private void dgwIN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex < 0) return;
            if ((bool)dgwIN.Rows[e.RowIndex].Cells[0].Value == true)
            {
                dgwIN.Rows[e.RowIndex].Cells[0].Value = false;
            }
            else
            {
                dgwIN.Rows[e.RowIndex].Cells[0].Value = true;
            }
            config.TopIN = RightGridHelper.GetRightdgwSelected(dgwIN);
        }

        private void numIN_ValueChanged(object sender, EventArgs e)
        {
            config.TopIN = RightGridHelper.SelectTopN(dgwIN, (int)numIN.Value);
        }

        private void dgwIN_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            config.TopIN = RightGridHelper.SelectTopN(dgwIN, (int)this.numIN.Value);
        }
        #endregion
        #region  IPC
        private void cmbIPC_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChartColumn group = (ChartColumn)cmbIPC.SelectedItem;
            try
            {
                IStat.IStat st = StatFactury.CreateStata(this.lib_Name.Text, config, chart1, showdata);
                st.ShowRightDataGridView(dgwIPC, group, zid.ToString());
            }
            catch (Exception ex)
            {

            }
            config.GroupIPC = group;
            rbIPCtop.Checked = true;
        }

        private void rbIPCselectall_CheckedChanged(object sender, EventArgs e)
        {
            config.TopIPC = RightGridHelper.SelectTopN(dgwIPC, dgwIPC.Rows.Count);
            this.numIPC.Enabled = this.rbIPCtop.Checked;
        }

        private void rbIPCtop_CheckedChanged(object sender, EventArgs e)
        {
            this.numIPC.Enabled = this.rbIPCtop.Checked;
            config.TopIPC = RightGridHelper.SelectTopN(dgwIPC, (int)numIPC.Value);
        }

        private void dgwIPC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex < 0) return;
            if ((bool)dgwIPC.Rows[e.RowIndex].Cells[0].Value == true)
            {
                dgwIPC.Rows[e.RowIndex].Cells[0].Value = false;
            }
            else
            {
                dgwIPC.Rows[e.RowIndex].Cells[0].Value = true;
            }
            config.TopIPC = RightGridHelper.GetRightdgwSelected(dgwIPC);
        }

        private void numIPC_ValueChanged(object sender, EventArgs e)
        {
            config.TopIPC = RightGridHelper.SelectTopN(dgwIPC, (int)numIPC.Value);
        }


        private void dgwIPC_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            config.TopIPC = RightGridHelper.SelectTopN(dgwIPC, (int)this.numIPC.Value);
        }
        #endregion

        #region  Year
        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChartColumn group = (ChartColumn)cmbYear.SelectedItem;
            try
            {
                IStat.IStat st = StatFactury.CreateStata(this.lib_Name.Text, config, chart1, showdata);
                st.ShowRightDataGridView(dgwYear, group, zid.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR", "提示");
                return;
            }

            config.GroupYear = group;
            rbYearselectall.Checked = true;

        }

        private void rbYearselectall_CheckedChanged(object sender, EventArgs e)
        {
            config.TopYear = RightGridHelper.SelectTopN(dgwYear, dgwYear.Rows.Count);
            this.numYear.Enabled = this.rbYeartop.Checked;
        }

        private void rbYeartop_CheckedChanged(object sender, EventArgs e)
        {
            this.numYear.Enabled = this.rbYeartop.Checked;
            config.TopYear = RightGridHelper.SelectTopN(dgwYear, (int)numYear.Value);
        }

        private void dgwYear_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex < 0) return;
            if ((bool)dgwYear.Rows[e.RowIndex].Cells[0].Value == true)
            {
                dgwYear.Rows[e.RowIndex].Cells[0].Value = false;
            }
            else
            {
                dgwYear.Rows[e.RowIndex].Cells[0].Value = true;
            }
            config.TopYear = RightGridHelper.GetRightdgwSelected(dgwYear);
        }

        private void numYear_ValueChanged(object sender, EventArgs e)
        {
            config.TopYear = RightGridHelper.SelectTopN(dgwYear, (int)numYear.Value);
        }


        private void dgwYear_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (rbYearselectall.Checked == true)
            {
                config.TopYear = RightGridHelper.SelectTopN(dgwYear, (int)dgwYear.Rows.Count);
            }
            else
            {
                config.TopYear = RightGridHelper.SelectTopN(dgwYear, (int)this.numYear.Value);
            }
        }
        #endregion

        #region  Type
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChartColumn group = (ChartColumn)cmbType.SelectedItem;
            RightGridHelper.ShowRightDataGridView(dgwType, group, zid.ToString());
            config.GroupType = group;
            rbTypetop.Checked = true;
        }

        private void rbTypeselectall_CheckedChanged(object sender, EventArgs e)
        {
            config.TopType = RightGridHelper.SelectTopN(dgwType, dgwType.Rows.Count);
            this.numType.Enabled = this.rbTypetop.Checked;
        }

        private void rbTypetop_CheckedChanged(object sender, EventArgs e)
        {
            this.numType.Enabled = this.rbTypetop.Checked;
            config.TopType = RightGridHelper.SelectTopN(dgwType, (int)numType.Value);
        }

        private void dgwType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex < 0) return;
            if ((bool)dgwType.Rows[e.RowIndex].Cells[0].Value == true)
            {
                dgwType.Rows[e.RowIndex].Cells[0].Value = false;
            }
            else
            {
                dgwType.Rows[e.RowIndex].Cells[0].Value = true;
            }
            config.TopType = RightGridHelper.GetRightdgwSelected(dgwType);
        }

        private void numType_ValueChanged(object sender, EventArgs e)
        {
            config.TopType = RightGridHelper.SelectTopN(dgwType, (int)numType.Value);
        }


        private void dgwType_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            config.TopType = RightGridHelper.SelectTopN(dgwType, (int)this.numType.Value);
        }
        #endregion

        #region  PAType
        private void cmbPAType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChartColumn group = (ChartColumn)cmbPAType.SelectedItem;
            RightGridHelper.ShowRightDataGridView(dgwPAType, group, zid.ToString());
            config.GroupPAType = group;
            rbPATypetop.Checked = true;
        }

        private void rbPATypeselectall_CheckedChanged(object sender, EventArgs e)
        {
            config.TopPAType = RightGridHelper.SelectTopN(dgwPAType, dgwPAType.Rows.Count);
            this.numPAType.Enabled = this.rbPATypetop.Checked;
        }

        private void rbPATypetop_CheckedChanged(object sender, EventArgs e)
        {
            this.numPAType.Enabled = this.rbPATypetop.Checked;
            config.TopPAType = RightGridHelper.SelectTopN(dgwPAType, (int)numPAType.Value);
        }

        private void dgwPAType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex < 0) return;
            if ((bool)dgwPAType.Rows[e.RowIndex].Cells[0].Value == true)
            {
                dgwPAType.Rows[e.RowIndex].Cells[0].Value = false;
            }
            else
            {
                dgwPAType.Rows[e.RowIndex].Cells[0].Value = true;
            }
            config.TopPAType = RightGridHelper.GetRightdgwSelected(dgwPAType);
        }

        private void numPAType_ValueChanged(object sender, EventArgs e)
        {
            config.TopPAType = RightGridHelper.SelectTopN(dgwPAType, (int)numPAType.Value);
        }


        private void dgwPAType_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            config.TopPAType = RightGridHelper.SelectTopN(dgwPAType, (int)this.numPAType.Value);

        }
        #endregion

        private void showdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 对第1列相同单元格进行合并
            if (!config.MergeColumnIndex.Contains(e.ColumnIndex)) return;
            if (e.RowIndex != -1)
            {
                using
                    (
                    Brush gridBrush = new SolidBrush(this.showdata.GridColor),
                    backColorBrush = new SolidBrush(e.CellStyle.BackColor)
                    )
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        // 清除单元格
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                        if (e.ColumnIndex == 0)
                        {
                            // 画 Grid 边线（仅画单元格的底边线和右边线）
                            //   如果下一行和当前行的数据不同，则在当前的单元格画一条底边线
                            if (e.RowIndex < showdata.Rows.Count - 1 && showdata.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value.ToString() != e.Value.ToString())
                            {
                                e.Graphics.DrawLine(
                                    gridLinePen,
                                    e.CellBounds.Left,
                                    e.CellBounds.Bottom - 1,
                                    e.CellBounds.Right - 1,
                                    e.CellBounds.Bottom - 1
                                );
                            }
                            if (e.RowIndex == showdata.Rows.Count - 1)
                            {
                                e.Graphics.DrawLine(
                                    gridLinePen,
                                    e.CellBounds.Left,
                                    e.CellBounds.Bottom - 1,
                                    e.CellBounds.Right - 1,
                                    e.CellBounds.Bottom - 1
                               );
                            }
                            // 画右边线
                            e.Graphics.DrawLine(
                                gridLinePen,
                                e.CellBounds.Right - 1,
                                e.CellBounds.Top,
                                e.CellBounds.Right - 1,
                                e.CellBounds.Bottom
                            );

                            // 画（填写）单元格内容，相同的内容的单元格只填写第一个
                            if (e.Value != null)
                            {
                                if (e.RowIndex > 0 && showdata.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString() == e.Value.ToString())
                                { }
                                else
                                {
                                    e.Graphics.DrawString((String)e.Value, e.CellStyle.Font,
                                        Brushes.Black, e.CellBounds.X + 2,
                                        e.CellBounds.Y + 5, StringFormat.GenericDefault);

                                }
                            }
                        }
                        else
                        {
                            // 画 Grid 边线（仅画单元格的底边线和右边线）
                            //   如果下一行和当前行的数据不同，则在当前的单元格画一条底边线
                            if (e.RowIndex < showdata.Rows.Count - 1 && showdata.Rows[e.RowIndex + 1].Cells[0].Value.ToString() != showdata.Rows[e.RowIndex].Cells[0].Value.ToString())
                            {
                                e.Graphics.DrawLine(
                                      gridLinePen,
                                      e.CellBounds.Left,
                                      e.CellBounds.Bottom - 1,
                                      e.CellBounds.Right - 1,
                                      e.CellBounds.Bottom - 1
                                 );
                            }
                            if (e.RowIndex == showdata.Rows.Count - 1)
                            {
                                e.Graphics.DrawLine(
                                    gridLinePen,
                                    e.CellBounds.Left,
                                    e.CellBounds.Bottom - 1,
                                    e.CellBounds.Right - 1,
                                    e.CellBounds.Bottom - 1
                               );
                            }
                            // 画右边线
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                                e.CellBounds.Top, e.CellBounds.Right - 1,
                                e.CellBounds.Bottom);

                            // 画（填写）单元格内容，相同的内容的单元格只填写第一个
                            if (e.Value != null)
                            {
                                if (e.RowIndex > 0 && e.RowIndex <= showdata.Rows.Count - 1 && showdata.Rows[e.RowIndex - 1].Cells[0].Value.ToString() == showdata.Rows[e.RowIndex].Cells[0].Value.ToString())
                                { }
                                else
                                {
                                    e.Graphics.DrawString((String)e.Value, e.CellStyle.Font,
                                        Brushes.Black, e.CellBounds.X + 2,
                                        e.CellBounds.Y + 5, StringFormat.GenericDefault);

                                }
                            }
                        }
                        e.Handled = true;
                    }
                }
            }
        }

        #region  LG
        private void cmbLG_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChartColumn group = (ChartColumn)cmbLG.SelectedItem;
            RightGridHelper.ShowRightDataGridView(dgwLG, group, zid.ToString());
            config.GroupLG = group;
            rbLGtop.Checked = true;
        }

        private void rbLGselectall_CheckedChanged(object sender, EventArgs e)
        {
            config.TopLG = RightGridHelper.SelectTopN(dgwLG, dgwLG.Rows.Count);
            this.numLG.Enabled = this.rbLGtop.Checked;
        }

        private void rbLGtop_CheckedChanged(object sender, EventArgs e)
        {
            this.numLG.Enabled = this.rbLGtop.Checked;
            config.TopLG = RightGridHelper.SelectTopN(dgwLG, (int)numLG.Value);
        }

        private void dgwLG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex < 0) return;
            if ((bool)dgwLG.Rows[e.RowIndex].Cells[0].Value == true)
            {
                dgwLG.Rows[e.RowIndex].Cells[0].Value = false;
            }
            else
            {
                dgwLG.Rows[e.RowIndex].Cells[0].Value = true;
            }
            config.TopLG = RightGridHelper.GetRightdgwSelected(dgwLG);
        }

        private void numLG_ValueChanged(object sender, EventArgs e)
        {
            config.TopLG = RightGridHelper.SelectTopN(dgwLG, (int)numLG.Value);
        }


        private void dgwLG_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            config.TopLG = RightGridHelper.SelectTopN(dgwLG, (int)this.numLG.Value);

        }
        #endregion
        #region  CS
        private void cmbCS_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChartColumn group = (ChartColumn)cmbCS.SelectedItem;
            switch (this.lib_Name.Text)
            {

                case "专利存活期分析":
                    RightGridHelper.ShowRightDataGridView(dgwCS, group, zid.ToString(), "专利存活期");
                    break;
                case "有效专利维持期分析":
                    RightGridHelper.ShowRightDataGridView(dgwCS, group, zid.ToString(), "有效专利维持期");
                    break;
                case "专利审查周期分析":
                    RightGridHelper.ShowRightDataGridView(dgwCS, group, zid.ToString(), "审查周期");
                    break;
                default:
                    RightGridHelper.ShowRightDataGridView(dgwCS, group, zid.ToString());
                    break;
            }
            rbCStop.Checked = true;
            config.GroupCS = group;
        }

        private void rbCSselectall_CheckedChanged(object sender, EventArgs e)
        {
            config.TopCS = RightGridHelper.SelectTopN(dgwCS, dgwCS.Rows.Count);
            this.numCS.Enabled = this.rbCStop.Checked;
        }

        private void rbCStop_CheckedChanged(object sender, EventArgs e)
        {
            this.numCS.Enabled = this.rbCStop.Checked;
            config.TopCS = RightGridHelper.SelectTopN(dgwCS, (int)numCS.Value);
        }

        private void dgwCS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex < 0) return;
            if ((bool)dgwCS.Rows[e.RowIndex].Cells[0].Value == true)
            {
                dgwCS.Rows[e.RowIndex].Cells[0].Value = false;
            }
            else
            {
                dgwCS.Rows[e.RowIndex].Cells[0].Value = true;
            }
            config.TopCS = RightGridHelper.GetRightdgwSelected(dgwCS);
        }

        private void numCS_ValueChanged(object sender, EventArgs e)
        {
            config.TopCS = RightGridHelper.SelectTopN(dgwCS, (int)numCS.Value);
        }


        private void dgwCS_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            config.TopCS = RightGridHelper.SelectTopN(dgwCS, (int)this.numCS.Value);

        }
        #endregion

        #region 图表类型选择
        private void cmbChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChartType mscp = (ChartType)cmbChartType.SelectedItem;
            config.ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), mscp.CharType, true);

            switch (config.ChartType)
            {
                case SeriesChartType.Column:
                    if (st != null)
                    {
                        st.DrawingColumnChart();
                    }
                    break;
                default:
                    foreach (Series series in chart1.Series)
                    {
                        series.ChartType = config.ChartType;
                    }
                    break;
            }

            if (config.ChartType == SeriesChartType.Pie || config.ChartType == SeriesChartType.Doughnut)
            {
                grpPic.Visible = true;
            }
            else
            {
                grpPic.Visible = false;
            }
        }
        #endregion

        private void cmbChartPalette_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                msChartPalette mscp = (msChartPalette)cmbChartPalette.SelectedItem;
                if (mscp.Type == "0")
                {
                    this.chart1.Palette = (ChartColorPalette)Enum.Parse(typeof(ChartColorPalette), mscp.Name, true);
                }
                else
                {
                    this.chart1.Palette = ChartColorPalette.None;
                    this.chart1.PaletteCustomColors = mscp.Colors;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ShowLegend_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Legends[0].Enabled = ShowLegend.Checked;
        }

        private void ckShowPointLabel_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Series series in chart1.Series)
            {

                series["LabelStyle"] = "";

            }
        }




        private void cmbLabelPoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelPoint LabPoint = (LabelPoint)cmbLabelPoint.SelectedItem;
            config.LabPoint = LabPoint.Name;
            foreach (Series series in chart1.Series)
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
        Dictionary<string, string> picLabelStyles = new Dictionary<string, string>();
        private void cmbLabelStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (picLabelStyles.Count == 0)
            {
                picLabelStyles.Add("内侧", "Inside");
                picLabelStyles.Add("外侧", "Outside");
                picLabelStyles.Add("无", "Disabled");
            }
            config.PieLabelStyle = picLabelStyles[cmbLabelStyle.Text];
            foreach (Series series in chart1.Series)
            {
                series["PieLabelStyle"] = config.PieLabelStyle;
            }
        }
        #region 3D
        private void ck3D_CheckedChanged(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].Area3DStyle.Enable3D = ck3D.Checked;
            grp3D.Visible = ck3D.Checked;
            chart1.ChartAreas[0].Area3DStyle.Inclination = (int)Inclination.Value;
            chart1.ChartAreas[0].Area3DStyle.Rotation = (int)Rotation.Value;
        }


        private void Inclination_ValueChanged(object sender, EventArgs e)
        {
            if (Inclination.Value > 90)
                Inclination.Value = -90;
            if (Inclination.Value < -90)
                Inclination.Value = 90;

            chart1.ChartAreas[0].Area3DStyle.Inclination = (int)Inclination.Value;
        }

        private void Rotation_ValueChanged(object sender, EventArgs e)
        {
            if (Rotation.Value > 180)
                Rotation.Value = -180;
            if (Rotation.Value < -180)
                Rotation.Value = 180;

            chart1.ChartAreas[0].Area3DStyle.Rotation = (int)Rotation.Value;
        }
        #endregion
        private void cmbLabelPoint_DataSourceChanged(object sender, EventArgs e)
        {
            LabelPoint LabPoint = (LabelPoint)cmbLabelPoint.SelectedItem;
            config.LabPoint = LabPoint.Name;
            foreach (Series series in chart1.Series)
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            string excelname = System.Windows.Forms.Application.StartupPath + "\\Excel\\" + dbname + "\\" + stname + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
            string imgname = System.Windows.Forms.Application.StartupPath + "\\ChartImg\\" + dbname + "\\" + stname + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg";
            if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\ChartImg\\" + dbname + "\\"))
            {
                System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\ChartImg\\" + dbname + "\\");
            }
            if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Excel\\" + dbname + "\\"))
            {
                System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Excel\\" + dbname + "\\");
            }
            chart1.SaveImage(imgname, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Jpeg);
            this.st.ExportExcel(excelname, imgname);

            if (MessageBox.Show("导出成功,是否查看导出文件所在目录?", "导出提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("explorer", "/select," + excelname);

            }

        }

        private void btnCreate_MouseMove(object sender, MouseEventArgs e)
        {
            btnCreate.Image = global::PatSI.Properties.Resources.shengchengtubiao;
        }

        private void btnCreate_MouseLeave(object sender, EventArgs e)
        {
            btnCreate.Image = global::PatSI.Properties.Resources.shengchengtubiao1;
        }

        private void btnExport_MouseLeave(object sender, EventArgs e)
        {
            btnExport.Image = global::PatSI.Properties.Resources.output1;
        }

        private void btnExport_MouseMove(object sender, MouseEventArgs e)
        {
            btnExport.Image = global::PatSI.Properties.Resources.output;
        }

        private void showdata_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < showdata.Columns.Count; i++)
            {
                showdata.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //if (i == showdata.Columns.Count - 1)
                //{
                //    //showdata.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //}
                //else
                //{
                //    showdata.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.;
                //}
            }

        }

        private void frmstat_ResizeEnd(object sender, EventArgs e)
        {
            resizeMiddlePanel();
        }

        private void twLeft_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
        }

        private void twLeft_AfterSelect(object sender, TreeViewEventArgs e)
        {

            //pnlChart.Visible = false;
            //pnldgw.Visible = false;
            if (e.Node.Level == 0) return;
            string itemname = e.Node.Text;
            twLeft.SelectedNode = e.Node;
            this.lib_Name.Text = itemname;
            st = null;
            config = new cfg() { Zid = zid.ToString(), MergeColumnIndex = new List<int>() { } };

            #region 过滤
            config.FilterSQL = "";
            config.FilterDt = null;
            this.dgw_filter.DataSource = config.FilterDt;
            #endregion


            this.ck3D.Visible = true;

            this.btnCreate.Visible = true;
            this.btnExport.Visible = false;

            #region 重置图标
            {
                this.stname = itemname;
                this.chart1.Series.Clear();
                this.showdata.Columns.Clear();
                this.chart1.Titles[0].Text = "请设置指标，生成表图";
                foreach (var x in chart1.ChartAreas)
                {
                    x.AxisY.Maximum = Double.NaN;
                    x.AxisY.Minimum = Double.NaN;
                }

            }
            #endregion

            #region  右侧配置

            config.Group1 = null;
            config.Group2 = null;
            config.Groupby = null;
            config.GroupCS = null;
            config.GroupPA = null;
            config.GroupIN = null;
            config.GroupIPC = null;
            config.GroupQY = null;
            config.GroupYear = null;
            config.GroupLG = null;
            config.GroupType = null;

            #endregion

            #region 右侧操作区
            {
                this.rbYeartop.Checked = true;
                numYear.Value = 10;

                rbQYTop.Checked = true;
                numQY.Value = 10;

                numPA.Value = 10;
                rbPAtop.Checked = true;

                numIN.Value = 10;
                rbINtop.Checked = true;

                numIPC.Value = 10;
                rbIPCtop.Checked = true;

                tabRight.TabPages.Clear();
                tabRight.TabPages.Add(tbChartConfig);
            }
            #endregion
            if (itemname.IndexOf("(二期)") >= 0 || e.Node.Parent.Text.IndexOf("(二期)") >= 0)
            {
                this.btnCreate.Visible = false;
                this.btnExport.Visible = false;
                MessageBox.Show("暂未实现，敬请期待！", "提示");
                return;
            }
            switch (e.Node.Parent.Text.Replace("(二期)", ""))
            {

                case "趋势分析":
                    this.rbYeartop.Checked = true;
                    numYear.Value = 10;
                    tabRight.TabPages.Add(tabYear);
                    STUIHelper.initCharColumn(cmbYear, dbtype, "年代");
                    switch (itemname)
                    {
                        case "专利趋势分析":
                            break;
                        case "技术生命周期分析":
                            this.ck3D.Visible = false;
                            break;
                        case "发明人增速趋势分析":
                            break;
                        case "专利类型分布趋势分析":
                            tabRight.TabPages.Add(tabType);
                            STUIHelper.initCharColumn(cmbType, dbtype, "专利类型");
                            break;
                    }
                    break;
                case "区域分析":

                    tabRight.TabPages.Add(tabQY);
                    STUIHelper.initCharColumn(cmbQY, dbtype, "区域");
                    switch (itemname)
                    {
                        case "区域专利申请比重分析":
                            break;
                        case "区域专利申请趋势分析":

                            tabRight.TabPages.Insert(1, tabYear);
                            STUIHelper.initCharYearColumn(cmbYear);
                            break;
                        case "区域专利申请人分析":

                            tabRight.TabPages.Insert(2, tabPA);
                            STUIHelper.initCharColumn(cmbPA, dbtype, "申请人");
                            break;
                        case "区域专利发明人分析":
                            tabRight.TabPages.Insert(2, tabIN);
                            STUIHelper.initCharColumn(cmbIN, dbtype, "发明人");
                            break;
                        case "区域专利技术构成分析":
                            tabRight.TabPages.Insert(2, tabIPC);
                            STUIHelper.initCharColumn(cmbIPC, dbtype, "技术分类");
                            break;
                        case "区域首次专利申请分析":
                            break;
                        case "区域专利动向分析":
                            break;
                    }
                    break;
                case "主体分析":
                    tabRight.TabPages.Add(tabPA);
                    STUIHelper.initCharColumn(cmbPA, dbtype, "申请人");
                    switch (itemname)
                    {
                        case "申请人专利排名分析":
                            break;
                        case "申请人趋势分析":
                            tabRight.TabPages.Insert(1, tabYear);
                            STUIHelper.initCharYearColumn(cmbYear);
                            break;
                        case "申请人专利布局动向分析":
                            tabRight.TabPages.Add(tabQY);
                            STUIHelper.initCharColumn(cmbQY, dbtype, "区域");
                            break;
                        case "申请人专利技术构成分析":
                            tabRight.TabPages.Add(tabIPC);
                            STUIHelper.initCharColumn(cmbIPC, dbtype, "技术分类");
                            break;
                        case "申请人类型分析":
                            tabRight.TabPages.Remove(tabPA);
                            tabRight.TabPages.Add(tabPAType);
                            STUIHelper.initCharColumn(cmbPAType, dbtype, "申请人类型");
                            break;
                        case "申请人专利类型分析":
                            tabRight.TabPages.Add(tabType);
                            STUIHelper.initCharColumn(cmbType, dbtype, "专利类型");
                            break;
                        case "申请人研发阵容分析":
                            config.MergeColumnIndex = new List<int>() { 0, 1 };
                            break;
                        case "申请人专利合作分析":
                            config.MergeColumnIndex = new List<int>() { 0, 1, 2 };
                            break;
                        case "申请人首次专利申请分析":
                            break;
                        case "发明人专利排名分析":
                            tabRight.TabPages.Remove(tabPA);
                            tabRight.TabPages.Add(tabIN);
                            STUIHelper.initCharColumn(cmbIN, dbtype, "发明人");
                            break;
                        case "发明人专利技术构成分析":
                            tabRight.TabPages.Remove(tabPA);
                            tabRight.TabPages.Add(tabIN);
                            STUIHelper.initCharColumn(cmbIN, dbtype, "发明人");
                            tabRight.TabPages.Add(tabIPC);
                            STUIHelper.initCharColumn(cmbIPC, dbtype, "技术分类");
                            break;
                        case "主体自定义分析":
                            break;
                    }
                    break;
                case "技术分析":
                    tabRight.TabPages.Add(tabIPC);
                    STUIHelper.initCharColumn(cmbIPC, dbtype, "技术分类");
                    switch (itemname)
                    {
                        case "专利技术构成分析":
                            break;
                        case "专利技术趋势分析":
                            tabRight.TabPages.Insert(1, tabYear);
                            STUIHelper.initCharColumn(cmbYear, dbtype, "年代");
                            break;
                        case "专利技术区域分析":
                            tabRight.TabPages.Add(tabQY);
                            STUIHelper.initCharColumn(cmbQY, dbtype, "区域");
                            break;
                        case "专利技术申请人分析":
                            tabRight.TabPages.Add(tabPA);
                            STUIHelper.initCharColumn(cmbPA, dbtype, "申请人");
                            break;
                    }
                    break;
                case "法律状态分析":

                    switch (itemname)
                    {
                        //审查周期,专利存活期,专利维持期
                        case "专利法律状态分析":
                            tabRight.TabPages.Add(tabLG);
                            STUIHelper.initCharColumn(cmbLG, dbtype, "法律状态");
                            break;
                        case "专利存活期分析":
                            tabCS.Text = "专利存活期";
                            grpCS.Text = "专利存活期";
                            grpCS1.Text = "专利存活期";
                            tabRight.TabPages.Add(tabCS);
                            STUIHelper.initCharColumn(cmbCS, dbtype, "专利存活期");
                            break;
                        case "有效专利维持期分析":
                            tabCS.Text = "有效专利维持期";
                            grpCS.Text = "有效专利维持期";
                            grpCS1.Text = "有效专利维持期";
                            tabRight.TabPages.Add(tabCS);
                            STUIHelper.initCharColumn(cmbCS, dbtype, "有效专利维持期");
                            break;
                        case "申请人法律状态分析":
                            tabRight.TabPages.Add(tabLG);
                            STUIHelper.initCharColumn(cmbLG, dbtype, "法律状态");
                            tabRight.TabPages.Add(tabPA);
                            STUIHelper.initCharColumn(cmbPA, dbtype, "申请人");
                            break;
                        case "区域法律状态分析":
                            tabRight.TabPages.Add(tabLG);
                            STUIHelper.initCharColumn(cmbLG, dbtype, "法律状态");
                            tabRight.TabPages.Add(tabQY);
                            STUIHelper.initCharColumn(cmbQY, dbtype, "区域");
                            break;
                        case "技术领域法律状态分析":
                            tabRight.TabPages.Add(tabLG);
                            STUIHelper.initCharColumn(cmbLG, dbtype, "法律状态");
                            tabRight.TabPages.Add(tabIPC);
                            STUIHelper.initCharColumn(cmbIPC, dbtype, "技术分类");
                            break;
                        case "公知技术统计分析":
                            tabRight.TabPages.Add(tabPA);
                            STUIHelper.initCharColumn(cmbPA, dbtype, "申请人");
                            break;
                        case "专利审查周期分析":
                            tabCS.Text = "审查周期";
                            grpCS.Text = "审查周期";
                            grpCS1.Text = "审查周期";
                            tabRight.TabPages.Add(tabCS);
                            STUIHelper.initCharColumn(cmbCS, dbtype, "审查周期");
                            break;
                        case "法律状态自定义分析":
                            break;
                    }
                    break;
                case "中国市场专利分析":
                    switch (itemname)
                    {
                        case "中国本土专利趋势":
                            tabRight.TabPages.Insert(1, tabYear);
                            STUIHelper.initCharColumn(cmbYear, dbtype, "年代");
                            break;
                        case "中美市场专利趋势":
                            tabRight.TabPages.Insert(1, tabYear);
                            STUIHelper.initCharColumn(cmbYear, dbtype, "年代");
                            break;
                        case "中欧市场专利趋势":
                            tabRight.TabPages.Insert(1, tabYear);
                            STUIHelper.initCharColumn(cmbYear, dbtype, "年代");
                            break;
                        case "中国市场重点技术排行榜":
                            tabRight.TabPages.Insert(1, tabIPC);
                            STUIHelper.initCharColumn(cmbIPC, dbtype, "技术分类");
                            break;
                        case "中国市场专利申请人排行榜":
                            tabRight.TabPages.Insert(1, tabPA);
                            STUIHelper.initCharColumn(cmbPA, dbtype, "申请人");
                            break;
                        case "中国市场布局国家排行榜":
                            tabRight.TabPages.Insert(1, tabQY);
                            STUIHelper.initCharColumn(cmbQY, dbtype, "国家");
                            break;
                    }
                    break;
                case "美国市场专利分析":
                    switch (itemname)
                    {
                        case "美国本土专利趋势":
                            tabRight.TabPages.Insert(1, tabYear);
                            STUIHelper.initCharColumn(cmbYear, dbtype, "年代");
                            break;
                        case "美欧市场专利趋势":
                            tabRight.TabPages.Insert(1, tabYear);
                            STUIHelper.initCharColumn(cmbYear, dbtype, "年代");
                            break;
                        case "美国市场重点技术排行榜":
                            tabRight.TabPages.Insert(1, tabIPC);
                            STUIHelper.initCharColumn(cmbIPC, dbtype, "技术分类");
                            break;
                        case "美国市场专利申请人排行榜":
                            tabRight.TabPages.Insert(1, tabPA);
                            STUIHelper.initCharColumn(cmbPA, dbtype, "申请人");
                            break;
                        case "美国市场布局国家排行榜":
                            tabRight.TabPages.Insert(1, tabQY);
                            STUIHelper.initCharColumn(cmbQY, dbtype, "国家");
                            break;

                    }
                    break;
                case "欧洲市场专利分析":
                    switch (itemname)
                    {
                        case "欧洲本土专利趋势":
                            tabRight.TabPages.Insert(1, tabYear);
                            STUIHelper.initCharColumn(cmbYear, dbtype, "年代");
                            break;
                        case "德国市场专利趋势":
                            tabRight.TabPages.Insert(1, tabYear);
                            STUIHelper.initCharColumn(cmbYear, dbtype, "年代");
                            break;
                        case "欧洲市场重点技术排行榜":
                            tabRight.TabPages.Insert(1, tabIPC);
                            STUIHelper.initCharColumn(cmbIPC, dbtype, "技术分类");
                            break;
                        case "欧洲市场专利申请人排行榜":
                            tabRight.TabPages.Insert(1, tabPA);
                            STUIHelper.initCharColumn(cmbPA, dbtype, "申请人");
                            break;
                        case "欧洲市场布局国家排行榜":
                            tabRight.TabPages.Insert(1, tabQY);
                            STUIHelper.initCharColumn(cmbQY, dbtype, "国家");
                            break;
                    }
                    break;
                case "国内市场专利预警分析":
                    switch (itemname)
                    {
                        case "国内专利储备不足预警":
                        case "国内专利数量泡沫预警":
                        case "国内专利创新增速预警":
                        case "国内领军者专利创新力预警":
                        case "国内专利组合集中度预警":
                            tabRight.TabPages.Remove(tbChartConfig);
                            break;
                        case "国内专利技术差距预警":
                        case "国外重点国家布局预警":
                            break;
                    }
                    break;
                case "海外市场专利预警分析":
                    switch (itemname)
                    {
                        case "美国市场专利壁垒预警":
                        case "欧洲市场专利壁垒预警":
                        case "日本市场专利壁垒预警":
                            tabRight.TabPages.Remove(tbChartConfig);
                            break;
                    }
                    break;
                case "自定义分析":
                    break;
                default:
                    STUIHelper.initCharColumn(cmbYear, dbtype, itemname);
                    STUIHelper.initCharColumn(cmbQY, dbtype, itemname);
                    break;
            }
            tabRight.TabPages.Add(tabFilter);
            STUIHelper.initChartType(cmbChartType, itemname);
            //STUIHelper.initChartZhiBiao(cklist_zhibiao, dbtype, itemname);
            tabRight.SelectedIndex = 1;
        }

        private void tabRight_DrawItem(object sender, DrawItemEventArgs e)
        {

            Pen p = new Pen(Color.Black);



            //获取TabControl主控件的工作区域
            Rectangle rec = tabRight.ClientRectangle;

            //新建一个StringFormat对象，用于对标签文字的布局设置
            StringFormat StrFormat = new StringFormat();
            StrFormat.LineAlignment = StringAlignment.Center;// 设置文字垂直方向居中
            StrFormat.Alignment = StringAlignment.Center;// 设置文字水平方向居中    

            SolidBrush bruFont;
            Rectangle b = new Rectangle(e.Bounds.X + 5, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height);
            if (e.Index == tabRight.SelectedIndex)
            {
                bruFont = new SolidBrush(Color.Blue);// 标签字体颜色
                Font font = new System.Drawing.Font("微软雅黑", 9F, FontStyle.Bold);//设置标签字体样式                                
                e.Graphics.DrawString(this.tabRight.TabPages[e.Index].Text, font, bruFont, b);
            }
            else
            {
                bruFont = new SolidBrush(Color.Black);// 标签字体颜色
                Font font = new System.Drawing.Font("微软雅黑", 8F);//设置标签字体样式                
                e.Graphics.DrawString(this.tabRight.TabPages[e.Index].Text, font, bruFont, e.Bounds);
            }




        }

        private void tbLeft_DrawItem(object sender, DrawItemEventArgs e)
        {
            Pen p = new Pen(Color.Black);



            //获取TabControl主控件的工作区域
            Rectangle rec = tbLeft.ClientRectangle;

            //新建一个StringFormat对象，用于对标签文字的布局设置
            StringFormat StrFormat = new StringFormat();
            StrFormat.LineAlignment = StringAlignment.Center;// 设置文字垂直方向居中
            StrFormat.Alignment = StringAlignment.Center;// 设置文字水平方向居中    

            SolidBrush bruFont;
            Rectangle b = new Rectangle(e.Bounds.X + 5, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height);
            if (e.Index == tbLeft.SelectedIndex)
            {
                bruFont = new SolidBrush(Color.Blue);// 标签字体颜色
                Font font = new System.Drawing.Font("微软雅黑", 9F, FontStyle.Bold);//设置标签字体样式                                
                e.Graphics.DrawString(this.tbLeft.TabPages[e.Index].Text, font, bruFont, b);
            }
            else
            {
                bruFont = new SolidBrush(Color.Black);// 标签字体颜色
                Font font = new System.Drawing.Font("微软雅黑", 8F);//设置标签字体样式                
                e.Graphics.DrawString(this.tbLeft.TabPages[e.Index].Text, font, bruFont, e.Bounds);
            }
        }


        private void tabRight_Click(object sender, EventArgs e)
        {
            this.spRight.Visible = true;
            if (tabRight.SelectedTab == null) return;
            if ("区域选项,申请人选项,发明人选项,技术分类选项,法律状态选项".Contains(tabRight.SelectedTab.Text))
            {
                resizeMiddlePanel(400);
            }
            else
            {
                resizeMiddlePanel();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

            frmFilter frm = new frmFilter(dbname, zid, dbtype, config.FilterDt);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                config.FilterSQL = frm.allsql;
                config.FilterDt = frm.dt;
                this.dgw_filter.DataSource = config.FilterDt;
                btnCreate_Click(null, null);
            }
            else
            {
                config.FilterSQL = "";
                config.FilterDt = frm.dt;
                this.dgw_filter.DataSource = config.FilterDt;
                btnCreate_Click(null, null);
            }
        }


        private void showdata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //得到行名称
            //得到列名称
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == 0 || e.ColumnIndex == showdata.Columns.Count - 1) return;
            if (e.RowIndex == showdata.Rows.Count - 1) return;
            if (showdata.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "0") return;
            if (showdata.Columns[e.ColumnIndex].HeaderText.IndexOf("增长率") >= 0) return;
            ChartColumn columnname1 = null;
            ChartColumn columnname2 = null;
            string type1 = "char";
            string type2 = "char";
            string value1 = "";
            string value2 = "";
            switch (this.lib_Name.Text)
            {
                #region 趋势
                case "专利趋势分析":
                    columnname1 = config.GroupYear;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    type1 = "int";
                    break;
                case "技术生命周期分析":
                    if (e.ColumnIndex >= 2)
                    {
                        MessageBox.Show("对不起，本列不支持数据钻取！", "提示");
                        return;
                    }
                    columnname1 = config.GroupYear;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    type1 = "int";
                    break;
                case "专利类型分布趋势分析":
                    columnname1 = config.GroupYear;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    type1 = "int";
                    columnname2 = config.GroupType;
                    value2 = showdata.Columns[e.ColumnIndex].HeaderText;
                    break;
                #endregion
                #region 区域
                case "区域专利申请比重分析":
                    columnname1 = config.GroupQY;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    break;
                case "区域专利申请趋势分析":
                    columnname1 = config.GroupYear;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    type1 = "int";
                    columnname2 = config.GroupQY;
                    value2 = showdata.Columns[e.ColumnIndex].HeaderText;
                    break;
                case "区域专利申请人分析":
                    columnname1 = config.GroupQY;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    columnname2 = config.GroupPA;
                    value2 = showdata.Columns[e.ColumnIndex].HeaderText;
                    break;
                case "区域专利发明人分析":
                    columnname1 = config.GroupQY;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    columnname2 = config.GroupIN;
                    value2 = showdata.Columns[e.ColumnIndex].HeaderText;
                    break;
                case "区域专利技术构成分析":
                    columnname1 = config.GroupQY;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    columnname2 = config.GroupIPC;
                    value2 = showdata.Columns[e.ColumnIndex].HeaderText;
                    break;
                case "区域首次专利申请分析":
                    columnname1 = config.GroupQY;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    columnname2 = new ChartColumn() { ColName = "ady", TableName = "st_dt" };
                    value2 = showdata.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    break;

                #endregion
                #region 申请人
                //申请人专利排名分析
                case "申请人专利排名分析":
                    columnname1 = config.GroupPA;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    break;
                case "申请人类型分析":
                    columnname1 = config.GroupPAType;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    break;
                case "申请人趋势分析":
                    columnname1 = config.GroupYear;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    type1 = "int";
                    columnname2 = config.GroupPA;
                    value2 = showdata.Columns[e.ColumnIndex].HeaderText;
                    break;
                case "申请人首次专利申请分析":
                    columnname1 = config.GroupPA;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    columnname2 = new ChartColumn() { ColName = "ady", TableName = "st_dt" };
                    value2 = showdata.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    type2 = "int";
                    break;
                case "申请人专利类型分析":
                    columnname1 = config.GroupPA;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    columnname2 = config.GroupType;
                    value2 = showdata.Columns[e.ColumnIndex].HeaderText;
                    break;
                //申请人专利布局动向分析
                case "申请人专利布局动向分析":
                    columnname1 = config.GroupPA;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    columnname2 = config.GroupQY;
                    value2 = showdata.Columns[e.ColumnIndex].HeaderText;
                    break;
                case "申请人专利技术构成分析":
                    columnname1 = config.GroupPA;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    columnname2 = config.GroupIPC;
                    value2 = showdata.Columns[e.ColumnIndex].HeaderText;
                    break;
                case "申请人研发阵容分析":
                    if (e.ColumnIndex >= 2)
                    {
                        MessageBox.Show("对不起，本列不支持数据钻取！", "提示");
                        return;
                    }
                    break;
                case "申请人专利合作分析":
                    if (e.ColumnIndex >= 2)
                    {
                        MessageBox.Show("对不起，本列不支持数据钻取！", "提示");
                        return;
                    }
                    break;
                case "发明人专利排名分析":
                    columnname1 = config.GroupIN;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    break;
                case "发明人专利技术构成分析":
                    columnname1 = config.GroupIN;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    columnname2 = config.GroupIPC;
                    value2 = showdata.Columns[e.ColumnIndex].HeaderText;
                    break;

                #endregion
                #region 技术
                case "专利技术构成分析":
                    columnname1 = config.GroupIPC;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    break;
                case "专利技术趋势分析":
                    columnname1 = config.GroupYear;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    type1 = "int";
                    columnname2 = config.GroupIPC;
                    value2 = showdata.Columns[e.ColumnIndex].HeaderText;
                    break;
                case "专利技术区域分析":
                    columnname1 = config.GroupIPC;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    columnname2 = config.GroupQY;
                    value2 = showdata.Columns[e.ColumnIndex].HeaderText;
                    break;
                case "专利技术申请人分析":
                    columnname1 = config.GroupIPC;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    columnname2 = config.GroupPA;
                    value2 = showdata.Columns[e.ColumnIndex].HeaderText;
                    break;

                #endregion
                #region 法律状态
                case "专利法律状态分析":
                    columnname1 = config.GroupLG;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    break;
                case "专利存活期分析":
                    columnname1 = config.GroupCS;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    type1 = "int";
                    break;
                case "有效专利维持期分析":
                    columnname1 = config.GroupCS;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    type1 = "int";
                    break;
                case "申请人法律状态分析":
                    columnname1 = config.GroupPA;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    columnname2 = config.GroupLG;
                    value2 = showdata.Columns[e.ColumnIndex].HeaderText;
                    break;
                case "区域法律状态分析":
                    columnname1 = config.GroupQY;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    columnname2 = config.GroupLG;
                    value2 = showdata.Columns[e.ColumnIndex].HeaderText;
                    break;
                case "技术领域法律状态分析":
                    columnname1 = config.GroupQY;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    columnname2 = config.GroupLG;
                    value2 = showdata.Columns[e.ColumnIndex].HeaderText;
                    break;
                case "公知技术统计分析":
                    columnname1 = config.GroupPA;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    columnname2 = new ChartColumn() { ColName = "isgongzhi", TableName = "st_dt" };
                    value2 = "1";
                    type2 = "int";
                    break;
                case "专利审查周期分析":
                    columnname1 = config.GroupCS;
                    value1 = showdata.Rows[e.RowIndex].Cells[0].Value.ToString();
                    type1 = "int";
                    break;
                case "法律状态自定义分析":
                    break;
                case "中国本土专利趋势":
                    break;
                case "中美市场专利趋势":
                    break;
                case "中欧市场专利趋势":
                    break;
                case "中国市场重点技术排行榜":
                    break;
                case "中国市场专利申请人排行榜":
                    break;
                case "中国市场布局国家排行榜":
                    break;
                #endregion

            }
            List<string> tables = new List<string>();
            string where1 = "";
            string where2 = "";
            if (columnname1 != null)
            {
                tables.Add(columnname1.TableName);
                if (type1 == "int")
                {
                    where1 = string.Format(" and {0}.{1} ={2} ", columnname1.TableName, columnname1.ColName, value1);
                }
                else
                {
                    where1 = string.Format(" and {0}.{1} ='{2}' ", columnname1.TableName, columnname1.ColName, value1);
                }
            }
            if (columnname2 != null)
            {
                tables.Add(columnname2.TableName);
                if (type2 == "int")
                {
                    where2 = string.Format(" and {0}.{1} ={2} ", columnname2.TableName, columnname2.ColName, value2);
                }
                else
                {
                    where2 = string.Format(" and {0}.{1} ='{2}' ", columnname2.TableName, columnname2.ColName, value2);
                }
            }

            tables.Add("show_Base");
            tables.Add("st_dt");
            tables = tables.Distinct<string>().ToList<string>();
            StringBuilder sbtable = new StringBuilder();
            StringBuilder sbjoin = new StringBuilder();
            foreach (var table in tables)
            {
                sbtable.Append(table + ",");
                if (table == "st_dt") continue;
                sbjoin.Append("st_dt.sid = " + table + ".sid and ");
            }
            string strtable = sbtable.ToString(0, sbtable.Length - 1);
            string strjoin = sbjoin.ToString(0, sbjoin.Length - 4);

            if (!String.IsNullOrEmpty(config.FilterSQL))
            {
                strtable += ", (" + config.FilterSQL + " ) as Filter ";
                if (strjoin == "")
                {
                    strjoin += string.Format("{0}.sid = Filter.sid", tables[0]);
                }
                else
                {
                    strjoin += string.Format(" and {0}.sid = Filter.sid", tables[0]);
                }

            }

            string select = "  (@rowIdx:=@rowIdx+1) as '序号',st_dt.sid,st_dt.an as '申请号',st_dt.pn as '公开号',show_Base.Title as '标题'";
            string where0 = " and st_dt.ztid=" + zid;
            string sql = string.Format("select {0} from {1} where {2} {3} {4} {5}", select, strtable, strjoin, where0, where1, where2);
            string sql1 = string.Format("select count(distinct st_dt.sid) from {1} where {2} {3} {4} {5}", select, strtable, strjoin, where0, where1, where2);
            frmPtListmin frm = new frmPtListmin(dbname, zid, dbtype, sql, sql1);
            frm.ShowDialog();
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void dgw_filter_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void showdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        #region 图表配色
        //private void comboBoxChartColorPalttte_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.chart1.Palette = (ChartColorPalette)Enum.Parse(typeof(ChartColorPalette), cmbChartPalette.Text, true);
        //}

        #endregion


    }
}
