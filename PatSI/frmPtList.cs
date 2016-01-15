using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.DBLinq;
using BLL.Model;
using BLL;
using BLL.UIHelper;
using ExcelLib;

namespace PatSI
{
    public partial class frmPtList : Form
    {
        public frmPtList()
        {
            InitializeComponent();
            ZTListHelper.IniZTDropDownList(cmbZtLst);
            bind();
        }
        public frmPtList(string name, int zid, string type)
        {
            InitializeComponent();
            ZTListHelper.IniZTDropDownList(cmbZtLst, zid, name, type);

            btnQuery_Click(null, null);
        }
        private int nPageCount = 1;
        private int nCuurentPage = -1;
        private int nPageSize = 50;
        private int nPtCount = 0;
        private string strOrderShowText = "";
        private string strOrder = "";
        private string strDbType = "";
        private string strZtID = "";
        private void frmPtList_Load(object sender, EventArgs e)
        {
            this.cbxPageSize.Text = nPageSize.ToString();

            //建立个矩形，等下计算 CheckBox 嵌入 GridView 的位置
            Rectangle rect = dgvListData.GetCellDisplayRectangle(0, -1, true);
            rect.X = rect.Location.X + rect.Width / 4 - 9;
            rect.Y = rect.Location.Y + (rect.Height / 2 - 9);

            //CheckBox cbHeader = new CheckBox();
            cbHeader.Name = "checkboxHeader";
            cbHeader.Size = new Size(18, 18);
            cbHeader.Location = rect.Location;
            cbHeader.Text = "全选";
            //全选要设定的事件
            cbHeader.CheckedChanged += new EventHandler(Chk_CheckedChanged);

            //将 CheckBox 加入到 dataGridView
            dgvListData.Controls.Add(cbHeader);

        }

        public void bind()
        {
            groupBox2.Text = "";
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            ztinfo zt = (ztinfo)cmbZtLst.SelectedItem;
            int ztid = zt.ID;
            strDbType = zt.DbType;
            strZtID = ztid.ToString();
            LoadData(false);
        }

        private void LoadData(bool isRefresh)
        {
            
            string strWhereTmp = string.IsNullOrEmpty(strfilterSql) ? string.Format(" where a1.ztid={0}", strZtID) :
                string.Format(",({1}) f1 where a1.ztid={0} and a1.sid=f1.sid", strZtID, strfilterSql);
            nPtCount = PtDataHelper.getPtDataCount(strWhereTmp);

            nPageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(nPtCount) / Convert.ToDouble(nPageSize)));
            if (nPageCount < 1)
            {
                nCuurentPage = 1;

                btnSY.Enabled = false;
                btnPre.Enabled = false;
                btnNext.Enabled = false;
                btnEnd.Enabled = false;

                groupBox2.Text = string.Format("共{0}条数据", nPtCount, nCuurentPage, nPageCount);
                //dgvListData.DataSource = null;
                //dgvListData.Refresh();

                nPageCount = 1;
                BingData(nCuurentPage, nPageSize);
            }
            else
            {
                nCuurentPage = isRefresh ? nCuurentPage : 1;
                BingData(nCuurentPage, nPageSize);
            }
        }

        private void BingData(int nPageidx, int nPageSize)
        {
            //if (nPageCount == 0)
            //    return;

            if (nPageidx > nPageCount)
            {
                nPageidx = nPageCount;
            }
            else if (nPageidx < 1)
            {
                nPageidx = 1;
            }

            btnSY.Enabled = true;
            btnPre.Enabled = true;
            btnNext.Enabled = true;
            btnEnd.Enabled = true;

            if (nPageidx == 1)
            {
                btnSY.Enabled = false;
                btnPre.Enabled = false;
            }

            if (nPageidx == nPageCount)
            {
                btnNext.Enabled = false;
                btnEnd.Enabled = false;
            }
            ztinfo zt = (ztinfo)cmbZtLst.SelectedItem;
            int ztid = zt.ID;

            switch (cbxOrder.Text)
            {
                case "申请号降序":
                    strOrder = "a1.an desc";
                    break;
                case "申请号升序":
                    strOrder = "a1.an";
                    break;
                case "申请日降序":
                    //strOrder = "ad desc";
                    strOrder = "date_format(REPLACE(REPLACE(REPLACE(a1.ad,'年','-'),'月','-'),'日',''),'%Y-%m-%d') desc";
                    break;
                case "申请日升序":
                    //strOrder = "ad ";
                    strOrder = "date_format(REPLACE(REPLACE(REPLACE(a1.ad,'年','-'),'月','-'),'日',''),'%Y-%m-%d')";
                    break;
                case "公开日降序":
                    //strOrder = "pd desc";
                    strOrder = "date_format(REPLACE(REPLACE(REPLACE(a1.pd,'年','-'),'月','-'),'日',''),'%Y-%m-%d') desc";
                    break;
                case "公开日升序":
                    //strOrder = "pd ";
                    strOrder = "date_format(REPLACE(REPLACE(REPLACE(a1.pd,'年','-'),'月','-'),'日',''),'%Y-%m-%d')";
                    break;
                default:
                    strOrder = "a1.id";
                    break;
            }
            strOrderShowText = cbxOrder.Text;

            
            string strWhereTmp = string.IsNullOrEmpty(strfilterSql) ? string.Format(" where a1.ztid={0}", strZtID) :
                string.Format(",({1}) f1 where a1.ztid={0} and a1.sid=f1.sid", strZtID, strfilterSql);

            dgvListData.DataSource = PtDataHelper.getPtData(strWhereTmp, strOrder, nPageidx, nPageSize, strDbType);

            nCuurentPage = nPageidx;
            groupBox2.Text = string.Format("共{0}条数据,第{1}页/共{2}页,[双击]可查看专利的详细信息.", nPtCount, nCuurentPage, nPageCount);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            By.frmSetShowFiles frm = new By.frmSetShowFiles(strDbType);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnQuery_Click(sender, e);
            }
        }

        private void dgvListData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvListData.Rows[e.RowIndex].Cells[0];
            Boolean flag = Convert.ToBoolean(cell.Value);
            if (flag == true)
            {
                cell.Value = false;
            }
            else
            {
                cell.Value = true;
            }
        }

        private void dgvListData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            //DataGridViewCell cell = (DataGridViewCell)dgvListData.Rows[e.RowIndex].Cells["Idx"];
            //string strPID = cell.Value.ToString();
            //frmPatDetails frm = new frmPatDetails(strPID,strDbType);

            frmPatDetails frm = new frmPatDetails(strDbType, (DataTable)dgvListData.DataSource, e.RowIndex + 1, strZtID);
            //frm.MdiParent = this;
            frm.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnSY_Click(object sender, EventArgs e)
        {
            BingData(1, nPageSize);
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            BingData(nCuurentPage - 1, nPageSize);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            BingData(nCuurentPage + 1, nPageSize);
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            BingData(nPageCount, nPageSize);
        }

        private void cbxPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxPageSize.Text.Equals(nPageSize.ToString()))
            {
                return;
            }

            nPageSize = int.Parse(cbxPageSize.Text);
            LoadData(true);
            //BingData(nCuurentPage, nPageSize);
        }

        private void cbxOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxOrder.Text.Equals(strOrderShowText))
            {
                return;
            }
            //LoadData(true);
            BingData(nCuurentPage, nPageSize);
        }

        private void dgvListData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (e.RowIndex == -1 || e.ColumnIndex == -1) return;
                dgvListData.ClearSelection();
                dgvListData.Rows[e.RowIndex].Selected = true;
                DataGridViewCell cell = dgvListData.Rows[e.RowIndex].Cells[e.ColumnIndex];
                //cell.ContextMenuStrip = contextMenuStrip1;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DataGridViewCell cell = (DataGridViewCell)dgvListData.SelectedRows[0].Cells["Idx"];
            string strPID = cell.Value.ToString();

            //frmPatDetails frm = new frmPatDetails(strPID,strDbType);
            frmPatDetails frm = new frmPatDetails(strDbType, (DataTable)dgvListData.DataSource, dgvListData.SelectedRows[0].Index + 1, strZtID);
            //frm.MdiParent = this;
            frm.ShowDialog();

            this.Cursor = Cursors.Default;
        }

        private void dgvListData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DataGridViewCell cell = (DataGridViewCell)dgvListData.SelectedRows[0].Cells["Idx"];
            string strPID = cell.Value.ToString();

            if (PtDataHelper.DelPt(strPID, strZtID))
            {
                MessageBox.Show("数据删除成功！", "温馨提示：");
                LoadData(true);
            }
            else
            {
                MessageBox.Show("数据删除失败,请重试！", "温馨提示：");
            }

            this.Cursor = Cursors.Default;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            By.frmRuleMag frm = new By.frmRuleMag();
            frm.ShowDialog();
        }

        private void dgvListData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //if (dgvListData.ColumnCount > 0)
            //{
            //    dgvListData.Columns[dgvListData.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //}
            //foreach (DataGridViewColumn col in dgvListData.Columns)
            //{
            //    if (col.HeaderText.IndexOf("是否") >= 0)
            //    {
            //        foreach (DataGridViewRow row in dgvListData.Rows)
            //        {
            //            //if (row.Cells[col.HeaderText].Value.ToString() == "1")
            //            //{
            //            //    row.Cells[col.HeaderText].Value = "是";
            //            //}
            //            //else
            //            //{
            //            //    row.Cells[col.HeaderText].Value = "否";
            //            //}
            //        }
            //    }
            //}
        }

        private void btnstat_Click(object sender, EventArgs e)
        {
            ztinfo zt = (ztinfo)cmbZtLst.SelectedItem;
            int ztid = zt.ID;
            strDbType = zt.DbType;
            strZtID = ztid.ToString();
            frmstat st = new frmstat(zt.Name, ztid, strDbType);
            st.MdiParent = this.MdiParent;
            st.WindowState = FormWindowState.Maximized;
            st.Show();
            this.Close();
        }


        private void Chk_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgvListData.Rows)
            {
                dr.Cells[0].Value = ((CheckBox)dgvListData.Controls.Find("checkboxHeader", true)[0]).Checked;
            }
        }

        string strfilterSql = "";
        private void button2_Click(object sender, EventArgs e)
        {
            ztinfo zt = (ztinfo)cmbZtLst.SelectedItem;
            int ztid = zt.ID;
            strDbType = zt.DbType;
            strZtID = ztid.ToString();
            frmFilter frm = new frmFilter(zt.Name, ztid, strDbType);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //MessageBox.Show(frm.allsql);
                strfilterSql = frm.allsql;
                LoadData(false);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            

        }

        private void 复制到ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DataGridViewCell cell = (DataGridViewCell)dgvListData.SelectedRows[0].Cells["Idx"];
            string strPID = cell.Value.ToString();

            By.frmDataMoveCopy frm = new By.frmDataMoveCopy(int.Parse(strZtID), strPID, strDbType, "COPY");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData(true);
            }

            this.Cursor = Cursors.Default;

        }

        private void 移动到ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DataGridViewCell cell = (DataGridViewCell)dgvListData.SelectedRows[0].Cells["Idx"];
            string strPID = cell.Value.ToString();

            By.frmDataMoveCopy frm = new By.frmDataMoveCopy(int.Parse(strZtID), strPID, strDbType, "MOVE");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData(true);
            }

            this.Cursor = Cursors.Default;
        }

        private void btnImportSelect_Click(object sender, EventArgs e)
        {
            string strPIDs = "";
            foreach (DataGridViewRow dr in dgvListData.Rows)
            {
                if (dr.Cells[0].Value != null && Convert.ToBoolean(dr.Cells[0].Value))
                {
                    strPIDs += ",'" + ((DataGridViewCell)dr.Cells["Idx"]).Value.ToString() + "'";

                }
            }

            strPIDs = strPIDs.TrimStart(',');

            if (strPIDs == "")
            {
                MessageBox.Show("请选择要导出的数据！", "温馨提示：");
                return;
            }

            By.frmSetShowFiles frm = new By.frmSetShowFiles(strDbType);
            frm.bIsSetExportFileds = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string excelname = "";

                saveFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath + "\\Excel";
                saveFileDialog1.Filter = "Excel 文件(*.xls;*.xlsx)|*.xls;*.xlsx";
                saveFileDialog1.FileName = "pt_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
                if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                excelname = saveFileDialog1.FileName;

                //DataTable dt = PtDataHelper.getPtData(string.Format(",fun_filter f1 where a1.ztid={0} and a1.sid in ({1}) and a1.sid=f1.sid ", strZtID, strPIDs), "", 1, nPageSize, strDbType);
                string strWhereTmp = string.IsNullOrEmpty(strfilterSql) ? string.Format(" where a1.ztid={0} and a1.sid in ({1}) ", strZtID, strPIDs) :
                            string.Format(",({1}) f1 where a1.ztid={0} and a1.sid in ({2}) and a1.sid=f1.sid", strZtID, strfilterSql, strPIDs);
                DataTable dt = PtDataHelper.getPtData(strWhereTmp, "", 1, nPageSize, strDbType);
                if (dt != null)
                {
                    dt.Columns.Remove("sid");
                }
                NPOIHelper.Export(dt, excelname, "专利数据", "");

                if (MessageBox.Show("导出成功,是否查看导出文件所在目录?", "导出提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("explorer", "/select," + excelname);

                }
            }
        }

        private void btnCopyPt_Click(object sender, EventArgs e)
        {
            string strPIDs = "";
            foreach (DataGridViewRow dr in dgvListData.Rows)
            {
                if (dr.Cells[0].Value != null && Convert.ToBoolean(dr.Cells[0].Value))
                {
                    strPIDs += ",'" + ((DataGridViewCell)dr.Cells["Idx"]).Value.ToString() + "'";

                }
            }

            strPIDs = strPIDs.TrimStart(',');

            if (strPIDs == "")
            {
                MessageBox.Show("请选择要复制的数据！", "温馨提示：");
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            By.frmDataMoveCopy frm = new By.frmDataMoveCopy(int.Parse(strZtID), strPIDs, strDbType, "COPY");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData(true);
            }

            this.Cursor = Cursors.Default;

        }

        private void btnMovePt_Click(object sender, EventArgs e)
        {
            string strPIDs = "";
            foreach (DataGridViewRow dr in dgvListData.Rows)
            {
                if (dr.Cells[0].Value != null && Convert.ToBoolean(dr.Cells[0].Value))
                {
                    strPIDs += ",'" + ((DataGridViewCell)dr.Cells["Idx"]).Value.ToString() + "'";

                }
            }

            strPIDs = strPIDs.TrimStart(',');

            if (strPIDs == "")
            {
                MessageBox.Show("请选择要移动的数据！", "温馨提示：");
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            By.frmDataMoveCopy frm = new By.frmDataMoveCopy(int.Parse(strZtID), strPIDs, strDbType, "MOVE");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData(true);
            }

            this.Cursor = Cursors.Default;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            string strPIDs = "";
            foreach (DataGridViewRow dr in dgvListData.Rows)
            {
                if (dr.Cells[0].Value != null && Convert.ToBoolean(dr.Cells[0].Value))
                {
                    strPIDs += ",'" + ((DataGridViewCell)dr.Cells["Idx"]).Value.ToString() + "'";

                }
            }

            strPIDs = strPIDs.TrimStart(',');

            if (strPIDs == "")
            {
                MessageBox.Show("请选择要删除的数据！", "温馨提示：");
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            if (PtDataHelper.DelPt(strPIDs, strZtID))
            {
                MessageBox.Show("数据删除成功！", "温馨提示：");
                LoadData(true);
            }
            else
            {
                MessageBox.Show("数据删除失败,请重试！", "温馨提示：");
            }

            this.Cursor = Cursors.Default;
        }
    }
}
