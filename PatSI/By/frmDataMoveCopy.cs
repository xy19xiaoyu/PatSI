using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.DBLinq;
using BLL;

namespace PatSI.By
{
    public partial class frmDataMoveCopy : Form
    {
        private string strDbType = "";
        private int nZtID = -1;
        private string strMoveCopy = "";
        private string strSID = "";
        public frmDataMoveCopy(int zid, string _sid, string type, string _strMoveCopy)
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
            strDbType = type;
            nZtID = zid;
            strSID = _sid;
            strMoveCopy = _strMoveCopy;
            bind();
        }

        private void frmDataMoveCopy_Load(object sender, EventArgs e)
        {

        }

        public void bind()
        {
            DataTable dt = BLL.DBLinq.ZTHelper.GetZTList(strDbType, nZtID);
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.ClearSelection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells[1];
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DataGridViewCell cell = (DataGridViewCell)dataGridView1.SelectedRows[0].Cells["id"];
            string strNztID = cell.Value.ToString();
            switch (strMoveCopy)
            {
                case "COPY":
                    bool bCopyRs = false;
                    if (strSID.Contains("','"))
                    {
                        bCopyRs = PtDataHelper.CopyPt(strSID.Replace("'", "").Split(',').ToList<string>(), nZtID.ToString(), strNztID);
                    }
                    else
                    {
                        bCopyRs = PtDataHelper.CopyPt(strSID, nZtID.ToString(), strNztID);
                    }
                    if (bCopyRs)
                    {
                        MessageBox.Show("数据复制成功！", "温馨提示：");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("数据复制失败,请重试！", "温馨提示：");
                    }
                    break;
                case "MOVE":
                    if (PtDataHelper.MovePt(strSID, nZtID.ToString(), strNztID))
                    {
                        MessageBox.Show("数据移动成功！", "温馨提示：");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("数据移动失败,请重试！", "温馨提示：");
                    }
                    break;
            }
            this.Cursor = Cursors.Default;
        }
    }
}
