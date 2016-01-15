using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using StatCfg;
using System.Drawing;

namespace BLL.UIHelper
{
    public class STUIHelper
    {


        public static void initLeftTree(TreeView tw, string dbtype)
        {
            MySqlDataContext db = new MySqlDataContext();
            tw.Nodes.Clear();
            switch (dbtype.ToUpper())
            {
                case "CPRS":
                    var group = from g in db.ChartGroup
                                where g.CN == 1
                                select g;
                    foreach (ChartGroup x in group)
                    {
                        var chridren = from y in MySqlHelper.DataContext.ChartItems
                                       where y.ChartGroup == x.ChartGroup1 && y.CN == 1
                                       select y;
                        TreeNode node = new TreeNode(x.ChartGroup1);
                        if (chridren.Count() > 0)
                        {

                            foreach (var tmp in chridren)
                            {
                                TreeNode tmpnode = new TreeNode(tmp.ItemName);
                                tmpnode.ToolTipText = tmp.ItemDesC;
                                if (x.ID > 5 || tmp.ItemName.IndexOf("自定义") > 0)
                                {
                                    tmpnode.ForeColor = Color.Gray;
                                    tmpnode.Text = tmpnode.Text + "(二期)";
                                }
                                node.Nodes.Add(tmpnode);
                            }

                        }
                        if (x.ID <= 5)
                        {
                            node.Expand();
                        }
                        else
                        {
                            node.ForeColor = Color.Gray;
                            node.Text = node.Text + "(二期)";

                        }
                        tw.Nodes.Add(node);

                    }
                    break;
                case "WPI":
                    var group1 = from g in db.ChartGroup
                                 where g.Wpi == 1
                                 select g;
                    foreach (ChartGroup x in group1)
                    {
                        var chridren = from y in MySqlHelper.DataContext.ChartItems
                                       where y.ChartGroup == x.ChartGroup1 && y.Wpi == 1
                                       select y;
                        TreeNode node = new TreeNode(x.ChartGroup1);
                        if (chridren.Count() > 0)
                        {

                            foreach (var tmp in chridren)
                            {
                                TreeNode tmpnode = new TreeNode(tmp.ItemName);
                                tmpnode.ToolTipText = tmp.ItemDesC;
                                if (x.ID > 5 || tmp.ItemName.IndexOf("自定义") > 0)
                                {
                                    tmpnode.ForeColor = Color.Gray;
                                    tmpnode.Text = tmpnode.Text + "(二期)";
                                }
                                node.Nodes.Add(tmpnode);
                            }

                        }
                        if (x.ID <= 5)
                        {
                            node.Expand();
                        }
                        else
                        {
                            node.ForeColor = Color.Gray;
                            node.Text = node.Text + "(二期)";

                        }
                        tw.Nodes.Add(node);
                    }
                    break;
                case "EPODOC":
                    var group2 = from g in db.ChartGroup
                                 where g.EPodOC == 1
                                 select g;
                    foreach (ChartGroup x in group2)
                    {
                        var chridren = from y in MySqlHelper.DataContext.ChartItems
                                       where y.ChartGroup == x.ChartGroup1 && y.EPodOC == 1
                                       select y;
                        TreeNode node = new TreeNode(x.ChartGroup1);
                        if (chridren.Count() > 0)
                        {

                            foreach (var tmp in chridren)
                            {
                                TreeNode tmpnode = new TreeNode(tmp.ItemName);
                                tmpnode.ToolTipText = tmp.ItemDesC;
                                if (x.ID > 5 || tmp.ItemName.IndexOf("自定义") > 0)
                                {
                                    tmpnode.ForeColor = Color.Gray;
                                    tmpnode.Text = tmpnode.Text + "(二期)";
                                }
                                node.Nodes.Add(tmpnode);
                            }

                        }
                        if (x.ID <= 5)
                        {
                            node.Expand();
                        }
                        else
                        {
                            node.ForeColor = Color.Gray;
                            node.Text = node.Text + "(二期)";

                        }
                        tw.Nodes.Add(node);

                    }
                    break;
            }
        }
        /// <summary>
        /// 根据统计分析类型 确定 图标的类型
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="dbtype"></param>
        /// <param name="itemname">
        /// 年代
        /// 区域
        /// 申请人
        /// 发明人
        /// 技术分类
        /// 专利类型
        /// </param>
        public static void initCharColumn(ComboBox cmb, string dbtype, string itemname)
        {
            using (MySqlDataContext db = new MySqlDataContext())
            {
                cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                cmb.Items.Clear();
                List<ChartColumn> res = null;
                switch (dbtype.ToUpper())
                {
                    case "CPRS":
                        res = (from tmp in db.ChartColumn
                               where tmp.CN == 1 && tmp.ItemName == itemname
                               select tmp).ToList<ChartColumn>();
                        break;
                    case "WPI":
                        res = (from tmp in db.ChartColumn
                               where tmp.Wpi == 1 && tmp.ItemName == itemname
                               select tmp).ToList<ChartColumn>();
                        break;
                    case "EPODOC":
                        res = (from tmp in db.ChartColumn
                               where tmp.EPodOC == 1 && tmp.ItemName == itemname
                               select tmp).ToList<ChartColumn>();
                        break;
                }

                foreach (var tmp in res)
                {
                    cmb.Items.Add(tmp);
                }
                cmb.DisplayMember = "ShowName";
                if (cmb.Items.Count > 0)
                {
                    cmb.SelectedIndex = 0;
                }
            }

        }
        /// <summary>
        /// 根据统计分析类型 确定 图标的类型
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="dbtype"></param>
        /// <param name="itemname"></param>
        public static void initCharYearColumn(ComboBox cmb)
        {
            using (MySqlDataContext db = new MySqlDataContext())
            {
                cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                cmb.Items.Clear();
                List<ChartColumn> res = null;

                res = (from tmp in db.ChartColumn
                       where tmp.ID == 1
                       select tmp).ToList<ChartColumn>();


                foreach (var tmp in res)
                {
                    cmb.Items.Add(tmp);
                }
                cmb.DisplayMember = "ShowName";
                if (cmb.Items.Count > 0)
                {
                    cmb.SelectedIndex = 0;
                }
            }

        }

        /// <summary>
        /// 初始化统计字段类型
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="dbtyp"></param>
        /// <param name="itemname"></param>
        public static void initChartType(ComboBox cmb, string itemname)
        {
            using (MySqlDataContext db = new MySqlDataContext())
            {
                cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                cmb.Items.Clear();
                var res = from tmp in db.ChartType
                          where tmp.ItemName == itemname
                          select tmp;
                foreach (var tmp in res)
                {
                    cmb.Items.Add(tmp);
                }
                cmb.DisplayMember = "ShowName";
                cmb.ValueMember = "ItemName";
                if (cmb.Items.Count > 0)
                {
                    cmb.SelectedIndex = 0;
                }
            }
        }

        public static void initChartZhiBiao(CheckedListBox cklst, string dbtype, string itemname)
        {
            using (MySqlDataContext db = new MySqlDataContext())
            {
                cklst.Items.Clear();
                List<ChartZHIbiAO> res = null;
                switch (dbtype.ToUpper())
                {
                    case "CPRS":
                        res = (from tmp in db.ChartZHIbiAO
                               where tmp.CN == 1 && tmp.ItemName == itemname
                               select tmp).ToList<ChartZHIbiAO>();
                        break;
                    case "WPI":
                        res = (from tmp in db.ChartZHIbiAO
                               where tmp.Wpi == 1 && tmp.ItemName == itemname
                               select tmp).ToList<ChartZHIbiAO>();
                        break;
                    case "EPODOC":
                        res = (from tmp in db.ChartZHIbiAO
                               where tmp.EPodOC == 1 && tmp.ItemName == itemname
                               select tmp).ToList<ChartZHIbiAO>();
                        break;
                }
                foreach (var tmp in res)
                {
                    cklst.Items.Add(tmp);
                }
                cklst.DisplayMember = "ShowName";
                if (cklst.Items.Count > 0)
                {
                    cklst.SetItemChecked(0, true);
                    cklst.SelectedIndex = 0;

                }
            }
        }

        public static void iniLabelPoint(ComboBox cmb)
        {
            cmb.Items.Clear();
            List<LabelPoint> lp = new List<LabelPoint>();
            cmb.Items.Add(new LabelPoint() { ShowName = "自动", Name = "Auto" });
            cmb.Items.Add(new LabelPoint() { ShowName = "无", Name = "None" });
            cmb.Items.Add(new LabelPoint() { ShowName = "左上", Name = "TopLeft" });
            cmb.Items.Add(new LabelPoint() { ShowName = "上", Name = "Top" });
            cmb.Items.Add(new LabelPoint() { ShowName = "右上", Name = "TopRight" });
            cmb.Items.Add(new LabelPoint() { ShowName = "右", Name = "Right" });
            cmb.Items.Add(new LabelPoint() { ShowName = "右下", Name = "BottomRight" });
            cmb.Items.Add(new LabelPoint() { ShowName = "下", Name = "Bottom" });
            cmb.Items.Add(new LabelPoint() { ShowName = "左下", Name = "BottomLeft" });
            cmb.Items.Add(new LabelPoint() { ShowName = "左", Name = "Left" });
            cmb.Items.Add(new LabelPoint() { ShowName = "中间", Name = "Center" });

            cmb.DisplayMember = "ShowName";
            cmb.ValueMember = "Name";
            cmb.SelectedIndex = 1;

        }


        public static void iniFilterColumn(ComboBox cmb, string dbtype)
        {
            using (MySqlDataContext db = new MySqlDataContext())
            {
                cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                cmb.Items.Clear();
                List<CfGSTColumn> res = null;
                switch (dbtype.ToUpper())
                {
                    case "CPRS":
                        res = (from tmp in db.CfGSTColumn
                               where tmp.IsCN == 1 && tmp.IsDel == 0
                               orderby tmp.ShownAme
                               select tmp).ToList<CfGSTColumn>();
                        break;
                    case "WPI":
                        res = (from tmp in db.CfGSTColumn
                               where tmp.IsWpi == 1 && tmp.IsDel == 0
                               orderby tmp.ShownAme
                               select tmp).ToList<CfGSTColumn>();
                        break;
                    case "EPODOC":
                        res = (from tmp in db.CfGSTColumn
                               where tmp.IsEPodOC == 1 && tmp.IsDel == 0
                               orderby tmp.ShownAme
                               select tmp).ToList<CfGSTColumn>();
                        break;
                }

                foreach (var tmp in res)
                {
                    cmb.Items.Add(tmp);
                }
                cmb.DisplayMember = "showname";
                if (cmb.Items.Count > 0)
                {
                    cmb.SelectedIndex = 0;
                }
            }
        }

    }
}
