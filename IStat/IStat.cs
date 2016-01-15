using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatCfg;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data;
using DAL;
namespace IStat
{
    public interface IStat
    {
        string strSQL
        {
            get;
            set;
        }

        DataTable Dt
        {
            get;
            set;
        }

        string Name
        {
            get;
            set;
        }

        string Zid
        {
            get;
        }

        int Id
        {
            get;
            set;
        }

        cfg Config
        {
            get;
            set;
        }

        Chart msChart
        {
            get;
            set;
        }

        DataGridView Gridview
        {
            get;
            set;
        }

        string GetStatSQL();

        bool Stat();

        bool ExportExcel(string FileName, string ImageFileName);

        bool DrawingLineChart();

        bool DrawingPicChart();

        bool DrawingColumnChart();

        bool ShowDataGridView();

        bool DrawingStackedColumnChart();

        bool DrawingPointChart();
        bool ShowRightDataGridView(DataGridView rightgrid, ChartColumn col, string zid);

    }
}
