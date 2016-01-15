using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.Index;

namespace PatSI.By
{
    public partial class frmRuleMag : Form
    {
        private int nPageCount = 1;
        private int nCuurentPage = -1;
        private int nPageSize = 20;
        private int nPtCount = 0;

        public frmRuleMag()
        {
            InitializeComponent();
        }

        private void btnNewByItem_Click(object sender, EventArgs e)
        {
            By.frmAddByItem frm = new By.frmAddByItem();
            if (frm.ShowDialog().Equals(DialogResult.OK))
            {
                LoadData(true);
            }

            //System.Configuration.ConfigurationManager.ConnectionStrings["SqliteServerStr"].ConnectionString = "";
        }

        private void frmRuleMag_Load(object sender, EventArgs e)
        {
            this.cbxPageSize.Text = nPageSize.ToString();
            LoadData(false);
        }

        private void LoadData(bool isRefresh)
        {
            nPtCount = IdexItemMag.getSysKeyCount("");
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

            dgvListData.DataSource = IdexItemMag.getSysKey("", "", nPageidx, nPageSize);

            nCuurentPage = nPageidx;
            groupBox2.Text = string.Format("共{0}条数据,第{1}页/共{2}页.", nPtCount, nCuurentPage, nPageCount);
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

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DataGridViewCell cell = (DataGridViewCell)dgvListData.SelectedRows[0].Cells["Idx"];
            string strPID = cell.Value.ToString();

            if (IdexItemMag.DelKey(int.Parse(strPID)))
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

        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DataGridViewCell cell = (DataGridViewCell)dgvListData.SelectedRows[0].Cells["Idx"];
            string strPID = cell.Value.ToString();
            string strKeyName = dgvListData.SelectedRows[0].Cells["标引项"].Value.ToString();

            By.frmEditByItem frm = new By.frmEditByItem(int.Parse(strPID), strKeyName);
            if (frm.ShowDialog().Equals(DialogResult.OK))
            {
                LoadData(true);
            }

            this.Cursor = Cursors.Default;
        }

        private void dgvListData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvListData.Columns.Count > 0)
            {
                dgvListData.Columns[dgvListData.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

    }
}
