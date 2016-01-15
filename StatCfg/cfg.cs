using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data;

namespace StatCfg
{
    public class cfg
    {
        private string zid;

        public string Zid
        {
            get { return zid; }
            set { zid = value; }
        }

        private bool useFMl;

        public bool UseFMl
        {
            get { return useFMl; }
            set { useFMl = value; }
        }
        private bool useFirstPA;

        public bool UseFirstPA
        {
            get { return useFirstPA; }
            set { useFirstPA = value; }
        }
        private bool useFirstIN;

        public bool UseFirstIN
        {
            get { return useFirstIN; }
            set { useFirstIN = value; }
        }
        private bool useFirstIPC;

        public bool UseFirstIPC
        {
            get { return useFirstIPC; }
            set { useFirstIPC = value; }
        }
        private bool useFirstCPC;

        public bool UseFirstCPC
        {
            get { return useFirstCPC; }
            set { useFirstCPC = value; }
        }
        private bool useCPY;

        public bool UseCPY
        {
            get { return useCPY; }
            set { useCPY = value; }
        }
        private bool addSum;

        public bool AddSum
        {
            get { return addSum; }
            set { addSum = value; }
        }

        private int startYear;

        public int StartYear
        {
            get { return startYear; }
            set { startYear = value; }
        }
        private int endYear;

        public int EndYear
        {
            get { return endYear; }
            set { endYear = value; }
        }
        private bool istype1 = false;

        public bool isType1 { get; set; }

        private string columns;

        public string Columns
        {
            get { return columns; }
            set { columns = value; }
        }
        private string groupby;

        public string Groupby
        {
            get { return groupby; }
            set { groupby = value; }
        }
        private List<string> tablenames = new List<string>();

        public List<string> TableNames
        {
            get { return tablenames; }
            set { tablenames = value; }
        }
        private string topGroup1;

        public string TopGroup1
        {
            get { return topGroup1; }
            set { topGroup1 = value; }
        }
        private ChartColumn group1;

        public ChartColumn Group1
        {
            get { return group1; }
            set { group1 = value; }
        }
        private string topGroup2;

        public string TopGroup2
        {
            get { return topGroup2; }
            set { topGroup2 = value; }
        }
        private ChartColumn group2;

        public ChartColumn Group2
        {
            get { return group2; }
            set { group2 = value; }
        }
        private ChartColumn groupCS;

        public ChartColumn GroupCS
        {
            get { return groupCS; }
            set { groupCS = value; }
        }
        private string topCS;

        public string TopCS
        {
            get { return topCS; }
            set { topCS = value; }
        }
        private string topPa;
        public string TopPA
        {
            get { return "'" + topPa.Replace(",", "','") + "'"; }
            set { topPa = value; }
        }
        private string topIN;

        public string TopIN
        {
            get { return "'" + topIN.Replace(",", "','") + "'"; }
            set { topIN = value; }
        }
        private string topIPC;

        public string TopIPC
        {
            get { return "'" + topIPC.Replace(",", "','") + "'"; }
            set { topIPC = value; }
        }
        private string topQY;

        public string TopQY
        {
            get { return "'" + topQY.Replace(",", "','") + "'"; }
            set { topQY = value; }
        }
        private string topType;

        public string TopType
        {
            get { return "'" + topType.Replace(",", "','") + "'"; }
            set { topType = value; }
        }

        private string topYear;
        public string TopYear
        {
            get { return topYear; }
            set { topYear = value; }
        }
        private string topPAType;

        public string TopPAType
        {
            get { return "'" + topPAType.Replace(",", "','") + "'"; }
            set { topPAType = value; }
        }
        private string topLG;

        public string TopLG
        {
            get { return "'" + topLG.Replace(",", "','") + "'"; }
            set { topLG = value; }
        }

        private ChartColumn groupLG;

        public ChartColumn GroupLG
        {
            get { return groupLG; }
            set { groupLG = value; }
        }
        private ChartColumn groupPA;

        public ChartColumn GroupPA
        {
            get { return groupPA; }
            set { groupPA = value; }
        }
        private ChartColumn groupIN;

        public ChartColumn GroupIN
        {
            get { return groupIN; }
            set { groupIN = value; }
        }
        private ChartColumn groupQY;

        public ChartColumn GroupQY
        {
            get { return groupQY; }
            set { groupQY = value; }
        }
        private ChartColumn groupIPC;

        public ChartColumn GroupIPC
        {
            get { return groupIPC; }
            set { groupIPC = value; }
        }
        private ChartColumn groupYear;

        public ChartColumn GroupYear
        {
            get { return groupYear; }
            set { groupYear = value; }
        }
        private ChartColumn groupType;

        public ChartColumn GroupType
        {
            get { return groupType; }
            set { groupType = value; }
        }

        private ChartColumn groupPAType;

        public ChartColumn GroupPAType
        {
            get { return groupPAType; }
            set { groupPAType = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        private List<int> mergeColumnIndex;
        /// <summary>
        /// 合并列
        /// </summary>
        public List<int> MergeColumnIndex
        {
            get { return mergeColumnIndex; }
            set { mergeColumnIndex = value; }
        }

        private SeriesChartType chartType;

        public SeriesChartType ChartType
        {
            get { return chartType; }
            set { chartType = value; }
        }

        private string labPoint;

        public string LabPoint
        {
            get
            {
                if (string.IsNullOrEmpty(labPoint))
                {
                    return "None";
                }
                else
                {
                    return labPoint;
                }
            }
            set { labPoint = value; }
        }
        private string pieLabelStyle;

        public string PieLabelStyle
        {
            get { return pieLabelStyle; }
            set { pieLabelStyle = value; }
        }

        private string filterSQL;

        public string FilterSQL
        {
            get { return filterSQL; }
            set { filterSQL = value; }
        }
        private DataTable filterDt;

        public DataTable FilterDt
        {
            get { return filterDt; }
            set { filterDt = value; }
        }
    }
}
