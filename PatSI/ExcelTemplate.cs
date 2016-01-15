using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.Template;
using DAL;
using BLL;

namespace PatSI
{
    public partial class ExcelTemplate : Form
    {
        private ImportTemplate tmp;
        public List<string> colName;
        public ExcelTemplate()
        {
            InitializeComponent();
        }
        public ExcelTemplate(ImportTemplate importtmp)
            : this()
        {
            this.tmp = importtmp;
            this.txtTemple.Text = tmp.Name;
            this.cmbType.Text = tmp.Type;
            if (tmp.Type.ToUpper() == "WPI")
            {
                chkWPI_AN.Checked = true;
                chkWPI_PN.Checked = true;
            }
            else if (tmp.Type.ToUpper() == "EPODOC")
            {
                chkEPO_AN.Checked = true;
                chkEPO_PN.Checked = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }



        private void chkAUTOindex_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnEPODOC_Click(object sender, EventArgs e)
        {
            chkEPO_AN.Checked = true;
            chkEPO_PN.Checked = true;


            chkWPI_AN.Checked = !chkEPO_AN.Checked;
            chkWPI_PN.Checked = !chkEPO_AN.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chkWPI_AN.Checked = true;
            chkWPI_PN.Checked = true;

            chkEPO_AN.Checked = !chkWPI_AN.Checked;
            chkEPO_PN.Checked = !chkWPI_AN.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tmp.ColumnMapping.Clear();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewComboBoxCell cell = row.Cells[1] as DataGridViewComboBoxCell;
                string excelname = row.Cells[0].Value.ToString();
                string splitchar = row.Cells[2].Value.ToString();
                if (cell != null)
                {
                    if (cell.Value.ToString() == "忽略") continue;
                    string colname = cell.Value.ToString();
                    CfGSTColumn configst = (from x in ddl
                                            where x.ShownAme == colname
                                            select x).First();

                    ColumnMapping tmpmapping = new ColumnMapping();
                    tmpmapping.SplitChar = splitchar;
                    tmpmapping.SystemColumnName = configst.ColName;
                    tmpmapping.Tablename = configst.TBName;
                    tmpmapping.Coltype = configst.ClType;
                    tmpmapping.ExcelColumnName = excelname;
                    tmpmapping.SystemColumnShowName = configst.ShownAme;
                    tmp.ColumnMapping.Add(excelname, tmpmapping);
                }
            }
            tmp.IsWPIDefault = chkWPI_AN.Checked;
            tmp.IsEPODCODefault = chkEPO_AN.Checked;

            if (tmp.Name != this.txtTemple.Text.Trim())
            {
                TemplateHelper.Delete(tmp.Type + "_" + tmp.Name);
            }
            tmp.Name = this.txtTemple.Text;
            tmp.Type = this.cmbType.Text;
            TemplateHelper.SaveTemplate(tmp);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        public List<CfGSTColumn> ddl;
        private void ExcelTemplate_Load(object sender, EventArgs e)
        {
            //if (tmp.ColumnMapping.Count == 0)
            //{
            foreach (var s in colName)
            {
                if (tmp.ColumnMapping.ContainsKey(s)) continue;
                tmp.ColumnMapping.Add(s, new ColumnMapping() { ExcelColumnName = s, SystemColumnShowName = s });
            }
            //}

            using (MySqlDataContext con = new MySqlDataContext())
            {
                switch (tmp.Type.ToUpper())
                {
                    case "CPRS":
                        ddl = (from x in con.CfGSTColumn
                               where x.IsCN == 1 && x.IsiDX == 1
                               orderby x.ShownAme
                               select x).ToList<CfGSTColumn>();
                        break;
                    case "WPI":
                        ddl = (from x in con.CfGSTColumn
                               where x.IsWpi == 1 && x.IsiDX == 1
                               orderby x.ShownAme
                               select x).ToList<CfGSTColumn>();
                        break;
                    case "EPODOC":
                        ddl = (from x in con.CfGSTColumn
                               where x.IsEPodOC == 1 && x.IsiDX == 1
                               orderby x.ShownAme
                               select x).ToList<CfGSTColumn>();
                        break;
                }

            }
            ddl.Insert(0, new CfGSTColumn() { ShownAme = "忽略" });
            this.ExcelCol.DataSource = ddl;
            this.ExcelCol.DisplayMember = "ShownAme";

            dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = tmp.ColumnMapping.Values.ToList<ColumnMapping>();


        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string syscolumnname = row.Cells["syscolumnname"].Value.ToString();
                if (syscolumnname == "")
                {
                    syscolumnname = row.Cells[0].Value.ToString();
                }
                DataGridViewComboBoxCell cell = row.Cells[1] as DataGridViewComboBoxCell;
                if (cell != null)
                {
                    bool hasfind = false;
                    foreach (var x in cell.Items)
                    {
                        CfGSTColumn item = x as CfGSTColumn;
                        if (item.ShownAme == syscolumnname)
                        {
                            cell.Value = item.ShownAme;
                            hasfind = true;
                            break;
                        }
                    }
                    if (!hasfind)
                    {
                        cell.Value = "忽略";
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chkWPI_AN_CheckedChanged(object sender, EventArgs e)
        {

            chkWPI_PN.Checked = chkWPI_AN.Checked;

            chkEPO_AN.Checked = !chkWPI_AN.Checked;
            chkEPO_PN.Checked = !chkWPI_AN.Checked;
        }

        private void chk_wpi_pn_sys_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkWPI_PN_CheckedChanged(object sender, EventArgs e)
        {

            chkWPI_PN.Checked = chkWPI_AN.Checked;

            chkEPO_AN.Checked = !chkWPI_AN.Checked;
            chkEPO_PN.Checked = !chkWPI_AN.Checked;
        }

        private void chkEPO_AN_CheckedChanged(object sender, EventArgs e)
        {

            chkEPO_PN.Checked = chkEPO_AN.Checked;


            chkWPI_AN.Checked = !chkEPO_AN.Checked;
            chkWPI_PN.Checked = !chkEPO_AN.Checked;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
