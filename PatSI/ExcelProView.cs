using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.Template;

namespace PatSI
{
    public partial class ExcelProView : Form
    {
        private string FileName;
        private string TmpName;
        public ImportTemplate tmp;
        public ExcelProView()
        {
            InitializeComponent();
        }
        public ExcelProView(string ExcelFileName, string tmpName, string type, string id)
            : this()
        {
            tmp = TemplateHelper.getTemplate(tmpName, type);
            tmp.Id = id;
            this.FileName = ExcelFileName;
            this.TmpName = tmpName;
            this.chkfirstrowiscolname.Checked = tmp.HasHeadColumn;
            this.numFirstRow.Value = tmp.FirstRowNum;

            showTOP10Excel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExcelTemplate et = new ExcelTemplate(tmp);
            et.colName = columnname;
            if (et.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.tmp.HasHeadColumn = chkfirstrowiscolname.Checked;
            try
            {
                showTOP10Excel();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：" + ex.Message, "错误");
            }
        }
        private List<string> columnname = new List<string>();
        private void showTOP10Excel()
        {
            List<string> tmpcol = new List<string>();
            DataTable dt = ExcelLib.NPOIHelper.Excel2DataTable(FileName, this.chkfirstrowiscolname.Checked, 1, (int)this.numFirstRow.Value, 15);
            foreach (DataColumn column in dt.Columns)
            {
                tmpcol.Add(column.ColumnName);
            }
            this.dataGridView1.DataSource = dt;
            this.columnname = tmpcol;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            tmp.FirstRowNum = (int)this.numFirstRow.Value;
            showTOP10Excel();
        }
    }
}
