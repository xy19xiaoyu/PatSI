using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stat.ZT;
using Stat.QY;
using Stat.JS;
using Stat.LG;
using Stat.QS;

using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using Stat.CN;
using Stat.US;
using Stat.EP;
using Stat.YJ;

namespace Stat
{
    public static class StatFactury
    {
        private static Dictionary<string, IStat.IStat> stats;

        private static Dictionary<string, IStat.IStat> Stats
        {
            get { return stats; }
            set { stats = value; }
        }
        public static IStat.IStat CreateStata(string name, StatCfg.cfg confg, Chart chart, DataGridView dgw)
        {
            if (stats.ContainsKey(name))
            {
                IStat.IStat st = stats[name];
                st.Config = confg;
                st.msChart = chart;
                st.Gridview = dgw;

                return st;


            }
            else
            {
                throw new ArgumentException("未找到名为:" + name + "的统计");
            }
        }
        static StatFactury()
        {
            stats = new Dictionary<string, IStat.IStat>();
            stats.Add("专利趋势分析", new ST_QS_01("专利趋势分析", null));
            stats.Add("技术生命周期分析", new ST_QS_02("技术生命周期分析", null));
            stats.Add("发明人增速趋势分析", new ST_QS_03("发明人增速趋势分析", null));
            stats.Add("专利类型分布趋势分析", new ST_QS_04("专利类布趋势分析", null));
            stats.Add("区域专利申请比重分析", new ST_QY_01("区域专利申请比重分析", null));
            stats.Add("区域专利申请趋势分析", new ST_QY_02("区域专利申请趋势分析", null));
            stats.Add("区域专利申请人分析", new ST_QY_03("区域专利申请趋势分析", null));
            stats.Add("区域专利发明人分析", new ST_QY_04("区域专利发明人分析", null));
            stats.Add("区域专利技术构成分析", new ST_QY_05("区域专利技术构成分析", null));
            stats.Add("区域首次专利申请分析", new ST_QY_06("区域首次专利申请分析", null));
            stats.Add("申请人专利排名分析", new ST_ZT_01("申请人专利排名分析", null));
            stats.Add("申请人类型分析", new ST_ZT_02("申请人类型分析", null));
            stats.Add("申请人趋势分析", new ST_ZT_03("申请人趋势分析", null));
            stats.Add("申请人首次专利申请分析", new ST_ZT_04("申请人首次专利申请分析", null));
            stats.Add("申请人专利类型分析", new ST_ZT_05("申请人专利类型分析", null));
            stats.Add("申请人专利布局动向分析", new ST_ZT_06("申请人专利布局动向分析", null));
            stats.Add("申请人专利技术构成分析", new ST_ZT_07("申请人专利技术构成分析", null));
            stats.Add("申请人研发阵容分析", new ST_ZT_08("申请人研发阵容分析", null));
            stats.Add("申请人专利合作分析", new ST_ZT_09("申请人专利合作分析", null));
            stats.Add("发明人专利排名分析", new ST_ZT_10("发明人专利排名分析", null));
            stats.Add("发明人专利技术构成分析", new ST_ZT_11("发明人专利技术构成分析", null));
            stats.Add("专利技术构成分析", new ST_JS_01("专利技术构成分析", null));
            stats.Add("专利技术趋势分析", new ST_JS_02("专利技术趋势分析", null));
            stats.Add("专利技术区域分析", new ST_JS_03("专利技术区域分析", null));
            stats.Add("专利技术申请人分析", new ST_JS_04("发明人专利技术构成分析", null));
            stats.Add("专利法律状态分析", new ST_LG_01("专利法律状态分析", null));
            stats.Add("专利存活期分析", new ST_LG_02("专利存活期分析", null));
            stats.Add("有效专利维持期分析", new ST_LG_03("专利存活期分析", null));
            stats.Add("申请人法律状态分析", new ST_LG_04("申请人法律状态分析", null));
            stats.Add("区域法律状态分析", new ST_LG_05("区域法律状态分析", null));
            stats.Add("技术领域法律状态分析", new ST_LG_06("技术领域法律状态分析", null));
            stats.Add("公知技术统计分析", new ST_LG_07("公知技术统计分析", null));
            stats.Add("专利审查周期分析", new ST_LG_08("公知技术统计分析", null));
            stats.Add("中国本土专利趋势", new ST_CN_01("中国本土专利趋势", null));
            stats.Add("中美市场专利趋势", new ST_CN_02("中美市场专利趋势", null));
            stats.Add("中欧市场专利趋势", new ST_CN_03("中欧市场专利趋势", null));
            stats.Add("中国市场重点技术排行榜", new ST_CN_04("中国市场重点技术排行榜", null));
            stats.Add("中国市场专利申请人排行榜", new ST_CN_05("中国市场专利申请人排行榜", null));
            stats.Add("中国市场布局国家排行榜", new ST_CN_06("中国市场布局国家排行榜", null));

            stats.Add("美国本土专利趋势", new ST_US_01("美国本土专利趋势", null));
            stats.Add("美欧市场专利趋势", new ST_US_02("美欧市场专利趋势", null));
            stats.Add("美国市场重点技术排行榜", new ST_US_03("美国市场重点技术排行榜", null));
            stats.Add("美国市场专利申请人排行榜", new ST_US_04("美国市场专利申请人排行榜", null));
            stats.Add("美国市场布局国家排行榜", new ST_US_05("美国市场布局国家排行榜", null));

            stats.Add("欧洲本土专利趋势", new ST_EP_01("欧洲本土专利趋势", null));
            stats.Add("德国市场专利趋势", new ST_EP_02("德国市场专利趋势", null));
            stats.Add("欧洲市场重点技术排行榜", new ST_EP_03("欧洲市场重点技术排行榜", null));
            stats.Add("欧洲市场专利申请人排行榜", new ST_EP_04("欧洲市场专利申请人排行榜", null));
            stats.Add("欧洲市场布局国家排行榜", new ST_EP_05("欧洲市场布局国家排行榜", null));

            stats.Add("国内专利储备不足预警", new ST_YJ_01("国内专利储备不足预警", null));
            stats.Add("国内专利数量泡沫预警", new ST_YJ_02("国内专利数量泡沫预警", null));
            stats.Add("国内专利创新增速预警", new ST_YJ_03("国内专利创新增速预警", null));
            stats.Add("国内领军者专利创新力预警", new ST_YJ_04("国内领军者专利创新力预警", null));
            stats.Add("国内专利组合集中度预警", new ST_YJ_05("国内专利组合集中度预警", null));
            stats.Add("国内专利技术差距预警", new ST_YJ_06("国内专利技术差距预警", null));
            stats.Add("国外重点国家布局预警", new ST_YJ_07("国外重点国家布局预警", null));
            stats.Add("美国市场专利壁垒预警", new ST_YJ_08("美国市场专利壁垒预警", null));
            stats.Add("欧洲市场专利壁垒预警", new ST_YJ_09("欧洲市场专利壁垒预警", null));
            stats.Add("日本市场专利壁垒预警", new ST_YJ_10("日本市场专利壁垒预警", null));











        }
    }
}
