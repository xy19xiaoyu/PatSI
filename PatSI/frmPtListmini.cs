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
    public partial class frmPtListmin : Form
    {

        public frmPtListmin(string name, int zid, string type, string sqlwhere, string sqlcount)
        {
            this.sqlwhere = sqlwhere;
            this.sqlcount = sqlcount;
            this.ztid = zid.ToString();
            this.strDbType = type;
            InitializeComponent();
            btnQuery_Click(null, null);
        }
        private string ztid;
        private string sqlwhere;
        private string sqlcount;
        private int nPageCount = 1;
        private int nCuurentPage = -1;
        private int nPageSize = 50;
        private int nPtCount = 0;
        private string strOrderShowText = "";
        private string strOrder = "";
        private string strDbType = "";
        private void frmPtList_Load(object sender, EventArgs e)
        {
            this.cbxPageSize.Text = nPageSize.ToString();
        }

        public void bind()
        {
            groupBox2.Text = "";
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            LoadData(false);
        }

        private void LoadData(bool isRefresh)
        {
            nPtCount = Convert.ToInt32(DBA.MySqlDbAccess.ExecuteScalar(CommandType.Text, sqlcount));
            nPageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(nPtCount) / Convert.ToDouble(nPageSize)));
            if (nPageCount < 1)
            {
                nCuurentPage = 0;

                btnSY.Enabled = false;
                btnPre.Enabled = false;
                btnNext.Enabled = false;
                btnEnd.Enabled = false;

                groupBox2.Text = string.Format("共{0}条数据", nPtCount, nCuurentPage, nPageCount);
                dgvListData.DataSource = null;
                dgvListData.Refresh();
            }
            else
            {
                nCuurentPage = isRefresh ? nCuurentPage : 1;
                BingData(nCuurentPage, nPageSize);
            }
        }

        private void BingData(int nPageidx, int nPageSize)
        {
            if (nPageCount == 0)
                return;

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
            DataTable dt;
            using (MySql.Data.MySqlClient.MySqlConnection conn = DBA.MySqlDbAccess.GetMySqlConnection())
            {
                conn.Open();
                string sql = string.Format("SELECT @rowIdx:={0}; ", (nPageidx - 1) * nPageSize);
                DBA.MySqlDbAccess.ExecNoQuery(conn, CommandType.Text, sql);
                dt = DBA.MySqlDbAccess.GetDataTable(conn, CommandType.Text, sqlwhere + string.Format(" limit {0},{1}", (nPageidx - 1) * nPageSize, nPageSize));
                dgvListData.DataSource = dt;
            }

            nCuurentPage = nPageidx;
            groupBox2.Text = string.Format("共{0}条数据,第{1}页/共{2}页,[双击]可查看专利的详细信息.", nPtCount, nCuurentPage, nPageCount);
        }

        private void dgvListData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            frmPatDetails frm = new frmPatDetails(strDbType, (DataTable)dgvListData.DataSource, e.RowIndex + 1, ztid);
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
        }





        private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            frmPatDetails frm = new frmPatDetails(strDbType, (DataTable)dgvListData.DataSource, dgvListData.SelectedRows[0].Index + 1, ztid);
            frm.ShowDialog();
            this.Cursor = Cursors.Default;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            By.frmRuleMag frm = new By.frmRuleMag();
            frm.ShowDialog();
        }

        private void dgvListData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvListData.ColumnCount > 0)
            {
                dgvListData.Columns[dgvListData.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }



        private void Chk_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgvListData.Rows)
            {
                dr.Cells[0].Value = ((CheckBox)dgvListData.Controls.Find("checkboxHeader", true)[0]).Checked;
            }
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
                string excelname = System.Windows.Forms.Application.StartupPath + "\\Excel\\pt_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";

                DataTable dt = PtDataHelper.getPtData(string.Format("a1.ztid={0} and a1.sid in ({1})", ztid, strPIDs), "", 1, nPageSize, strDbType);
                NPOIHelper.Export(dt, excelname, "专利数据", "");

                if (MessageBox.Show("导出成功,是否查看导出文件所在目录?", "导出提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("explorer", "/select," + excelname);

                }
            }
        }
        private void dgvListData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvListData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
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

    }
}
