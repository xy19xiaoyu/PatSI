using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL;
using DAL;

namespace PatSI
{
    public partial class ZTListNew : Form
    {
        public ZTListNew()
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
            bind();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            NewZT frmnewzt = new NewZT();
            if (frmnewzt.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bind();
            }
        }
        public void bind()
        {
            DataTable dt = ZTHelper.GetZTList();
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (e.RowIndex == -1 || e.ColumnIndex == -1) return;
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                //cell.ContextMenuStrip = contextMenuStrip1;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(string.Format("请选择要编辑的数据"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataGridViewRow datarow = dataGridView1.SelectedRows[0];
            string name = (datarow.Cells["name"].Value == null ? "" : datarow.Cells["name"].Value.ToString());
            string des = (datarow.Cells["des"].Value == null ? "" : datarow.Cells["des"].Value.ToString());
            string type = (datarow.Cells["type"].Value == null ? "CPRS" : datarow.Cells["type"].Value.ToString());
            NewZT frm = new NewZT(datarow.Cells["Id"].Value.ToString(), name, des, type);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bind();
            }
        }


        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(string.Format("请选择要删除的数据"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataGridViewRow datarow = dataGridView1.SelectedRows[0];
            string name = (datarow.Cells["name"].Value == null ? "" : datarow.Cells["name"].Value.ToString());
            string des = (datarow.Cells["des"].Value == null ? "" : datarow.Cells["des"].Value.ToString());
            string type = (datarow.Cells["type"].Value == null ? "CPRS" : datarow.Cells["type"].Value.ToString());
            if (MessageBox.Show("您确定要删除专题库 【" + name + "】 吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    if (ZTHelper.DelZTDB(datarow.Cells["Id"].Value.ToString(), name))
                    {
                        bind();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误");
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(string.Format("请选择要导入的数据库"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataGridViewRow datarow = dataGridView1.SelectedRows[0];
            string name = (datarow.Cells["name"].Value == null ? "" : datarow.Cells["name"].Value.ToString());
            string des = (datarow.Cells["des"].Value == null ? "" : datarow.Cells["des"].Value.ToString());
            string type = (datarow.Cells["type"].Value == null ? "CPRS" : datarow.Cells["type"].Value.ToString());
            int id = Convert.ToInt32(datarow.Cells["Id"].Value.ToString());


            if (MDIMain.Ztid != id && MDIMain.Ztid != 0)
            {
                if (MessageBox.Show(string.Format("您正在使用专题库:【{0}】，{1}是:关闭现有专题库，打开新专题库【{2}】{1}否：继续使用当前专题库:【{0}】", MDIMain.Ztname, Environment.NewLine, name), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    //关闭所有页面
                    foreach (Form form in ((MDIMain)this.Parent.Parent).MdiChildren)
                    {
                        if (form is ZTListNew)
                        {
                            continue;
                        }
                        else
                        {
                            form.Close();
                        }
                    }
                    //切换专题库
                    MDIMain.Ztid = id;
                    MDIMain.Ztname = name;
                    MDIMain.Dbtype = type;
                    ((MDIMain)this.MdiParent).tssZTName.Text = "当前选择专题库：" + MDIMain.Ztname;
                }
            }
            else
            {
                MDIMain.Ztid = id;
                MDIMain.Ztname = name;
                MDIMain.Dbtype = type;
                ((MDIMain)this.MdiParent).tssZTName.Text = "当前选择专题库：" + MDIMain.Ztname;
            }

            DBIN frmdbin = new DBIN(MDIMain.Ztname, MDIMain.Ztid, MDIMain.Dbtype);
            frmdbin.MdiParent = this.MdiParent;
            frmdbin.Show();
            this.Close();

        }

        private void btnIndex_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(string.Format("请选择要标引的数据库"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataGridViewRow datarow = dataGridView1.SelectedRows[0];
            string name = (datarow.Cells["name"].Value == null ? "" : datarow.Cells["name"].Value.ToString());
            string des = (datarow.Cells["des"].Value == null ? "" : datarow.Cells["des"].Value.ToString());
            string type = (datarow.Cells["type"].Value == null ? "CPRS" : datarow.Cells["type"].Value.ToString());
            int id = Convert.ToInt32(datarow.Cells["Id"].Value.ToString());


            if (MDIMain.Ztid != id && MDIMain.Ztid != 0)
            {
                if (MessageBox.Show(string.Format("您正在使用专题库:【{0}】，{1}是:关闭现有专题库，打开新专题库【{2}】{1}否：继续使用当前专题库:【{0}】", MDIMain.Ztname, Environment.NewLine, name), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    //关闭所有页面
                    foreach (Form form in ((MDIMain)this.Parent.Parent).MdiChildren)
                    {
                        if (form is ZTListNew)
                        {
                            continue;
                        }
                        else
                        {
                            form.Close();
                        }
                    }
                    //inifilter(id);
                    //切换专题库
                    MDIMain.Ztid = id;
                    MDIMain.Ztname = name;
                    MDIMain.Dbtype = type;
                    ((MDIMain)this.MdiParent).tssZTName.Text = "当前选择专题库：" + MDIMain.Ztname;
                }
            }
            else
            {
                //inifilter(id);
                MDIMain.Ztid = id;
                MDIMain.Ztname = name;
                MDIMain.Dbtype = type;
                ((MDIMain)this.MdiParent).tssZTName.Text = "当前选择专题库：" + MDIMain.Ztname;
            }

            foreach (Form form in ((MDIMain)this.Parent.Parent).MdiChildren)
            {
                if (form is frmPtList)
                {

                    form.Activate();
                    this.Close();
                    return;

                }
            }


            frmPtList frmdbin = new frmPtList(MDIMain.Ztname, MDIMain.Ztid, MDIMain.Dbtype);
            frmdbin.MdiParent = this.MdiParent;
            frmdbin.Show();
            this.Close();


        }

        private void btnST_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(string.Format("请选择要统计分析的数据库"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }




            DataGridViewRow datarow = dataGridView1.SelectedRows[0];
            string name = (datarow.Cells["name"].Value == null ? "" : datarow.Cells["name"].Value.ToString());
            string des = (datarow.Cells["des"].Value == null ? "" : datarow.Cells["des"].Value.ToString());
            string type = (datarow.Cells["type"].Value == null ? "CPRS" : datarow.Cells["type"].Value.ToString());
            int id = Convert.ToInt32(datarow.Cells["Id"].Value.ToString());


            if (MDIMain.Ztid != id && MDIMain.Ztid != 0)
            {
                if (MessageBox.Show(string.Format("您正在使用专题库:【{0}】，{1}是:关闭现有专题库，打开新专题库【{2}】{1}否：继续使用当前专题库:【{0}】", MDIMain.Ztname, Environment.NewLine, name), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    //关闭所有页面
                    foreach (Form form in ((MDIMain)this.Parent.Parent).MdiChildren)
                    {
                        if (form is ZTListNew)
                        {
                            continue;
                        }
                        else
                        {
                            form.Close();
                        }
                    }
                    //切换专题库
                   // inifilter(id);
                    MDIMain.Ztid = id;
                    MDIMain.Ztname = name;
                    MDIMain.Dbtype = type;
                    ((MDIMain)this.MdiParent).tssZTName.Text = "当前选择专题库：" + MDIMain.Ztname;
                }
            }
            else
            {
                //inifilter(id);
                MDIMain.Ztid = id;
                MDIMain.Ztname = name;
                MDIMain.Dbtype = type;
                ((MDIMain)this.MdiParent).tssZTName.Text = "当前选择专题库：" + MDIMain.Ztname;
            }

            foreach (Form form in ((MDIMain)this.Parent.Parent).MdiChildren)
            {
                if (form is frmstat)
                {
                    form.Activate();

                    this.Close();
                    return;
                }
            }

            frmstat st = new frmstat(MDIMain.Ztname, MDIMain.Ztid, MDIMain.Dbtype);
            st.MdiParent = this.MdiParent;
            st.WindowState = FormWindowState.Maximized;
            st.Show();
            this.Close();


        }

        private void btnMove_Click(object sender, EventArgs e)
        {

        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(string.Format("请选择要导出的数据库!"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataGridViewRow datarow = dataGridView1.SelectedRows[0];
            string name = (datarow.Cells["name"].Value == null ? "" : datarow.Cells["name"].Value.ToString());
            string des = (datarow.Cells["des"].Value == null ? "" : datarow.Cells["des"].Value.ToString());
            string type = (datarow.Cells["type"].Value == null ? "CPRS" : datarow.Cells["type"].Value.ToString());
            int id = Convert.ToInt32(datarow.Cells["Id"].Value.ToString());
            int nPtNum = datarow.Cells["app_sum"].Value == null ? 0 : Convert.ToInt32(datarow.Cells["app_sum"].Value.ToString());

            if (nPtNum == 0)
            {
                MessageBox.Show(string.Format("您选择的专利库中没有数据可以导出!"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            By.frmSetShowFiles frm = new By.frmSetShowFiles(type.ToUpper());
            frm.bIsSetExportFileds = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string excelname = "";

                SaveFileDialog saveDlg = new SaveFileDialog();
                saveDlg.InitialDirectory = System.Windows.Forms.Application.StartupPath + "\\Excel";
                saveDlg.Filter = "Excel 文件(*.xls;*.xlsx)|*.xls;*.xlsx";
                saveDlg.FileName = "pt_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
                if (saveDlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                string strFileName = saveDlg.FileName.Substring(0, saveDlg.FileName.LastIndexOf('.')); ;
                string strExtension = saveDlg.FileName.Substring(saveDlg.FileName.LastIndexOf('.'));

                int nSplitFlag = 10000; //拆分文件,每x条数据导出一个excel文件;

                for (int nIdx = 1; (nIdx - 1) * nSplitFlag < nPtNum; nIdx++)
                {
                    excelname = nPtNum > nSplitFlag ? strFileName + "_"+nIdx.ToString() + strExtension : strFileName + "" + strExtension;

                    DataTable dt = PtDataHelper.getPtData(string.Format("a1.ztid={0}", id), "", nIdx, nSplitFlag, type.ToUpper());
                    if (dt != null)
                    {
                        dt.Columns.Remove("sid");
                    }
                    ExcelLib.NPOIHelper.Export(dt, excelname, "专利数据", "");
                }

                if (MessageBox.Show("导出成功,是否查看导出文件所在目录?", "导出提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("explorer", "/select," + excelname);
                }
            }
        }
        private void inifilter(int zid)
        {
           
        }

    }
}
