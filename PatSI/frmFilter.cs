using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.UIHelper;
using DAL;
using xyExtensions;
using MySql.Data.MySqlClient;
using BLL;

namespace PatSI
{

    public partial class frmFilter : Form
    {
        public List<string> sids = new List<string>();
        public List<string> content;
        public string dbtype;
        public string dbname;
        public int zid;
        public DataTable dt = new DataTable();
        public string allsql;
        public frmFilter()
        {
            InitializeComponent();
        }
        public frmFilter(string dbname, int zid, string dbtype)
        {
            InitializeComponent();
            dt.Columns.AddRange(new DataColumn[] { 
                new DataColumn("Id"),
                new DataColumn("luoji"),
                new DataColumn("Tablename"),
                new DataColumn("Colname"),
                new DataColumn("Tabl_ColName"),
                new DataColumn("Colopererator"),
                new DataColumn("sql")
            });
            this.dataGridView1.AutoGenerateColumns = false;

            this.dbname = dbname;
            this.dbtype = dbtype;
            this.zid = zid;
        }
        public frmFilter(string dbname, int zid, string dbtype, DataTable dt)
        {

            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
            this.dbname = dbname;
            this.dbtype = dbtype;
            this.zid = zid;

            if (dt == null)
            {
                dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[] { 
                new DataColumn("Id"),
                new DataColumn("luoji"),
                new DataColumn("Tablename"),
                new DataColumn("Colname"),
                new DataColumn("Tabl_ColName"),
                new DataColumn("Colopererator"),
                new DataColumn("sql")
                });
                this.dt = dt;
            }
            else
            {
                this.dt = dt;
                if (dt.Rows.Count > 0)
                {
                    BindData();
                    ff();
                }
            }
        }

        private void frmFilter_Load(object sender, EventArgs e)
        {

            STUIHelper.iniFilterColumn(cmbColName, dbtype);
            cmbLuoJi.SelectedIndex = 0;
            BindData();
        }
        private void BindData()
        {
            this.dataGridView1.DataSource = dt;
            int i = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                i++;
                row.Cells[0].Value = i;

            }
            this.dataGridView1.Update();
        }
        private void btnSubMit_Click(object sender, EventArgs e)
        {
            if (allsql != "")
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmbColName_SelectedIndexChanged(object sender, EventArgs e)
        {
            CfGSTColumn col = (CfGSTColumn)cmbColName.SelectedItem;
            this.textBox1.Text = "";
            List<string> filtertype = null;
            List<string> yesorno = new List<string>();
            switch (col.ClType)
            {
                case "int":
                    filtertype = new List<string> { "等于", "不等于", "大于", "小于" };
                    break;
                case "varchar":
                    filtertype = new List<string> { "等于", "不等于", "包含", "不包含", "前方一致", "后方一致" };
                    break;
                case "tinyint":
                    filtertype = new List<string> { "等于", "不等于", "大于", "小于" };
                    break;
            }
            cmbFilterType.Items.Clear();
            switch (col.ShownAme)
            {
                case "是否合作申请":
                case "是否国外来华":
                case "是否三局":
                case "是否五局":
                case "是否公知技术":
                    filtertype = new List<string> { "等于" };
                    yesorno = new List<string>() { "是", "否" };
                    break;
                case "专利类型":
                    filtertype = new List<string> { "等于", "不等于" };
                    yesorno = new List<string>() { "发明专利", "实用新型", "外观专利" };
                    break;
                case "专利类型(含PCT)":
                    filtertype = new List<string> { "等于", "不等于" };
                    yesorno = new List<string>() { "发明专利", "发明专利PCT", "实用新型", "实用新型PCT", "外观专利" };
                    break;
                case "申请人类型":
                case "主申请人类型":
                    filtertype = new List<string> { "等于", "不等于" };
                    yesorno = new List<string>() { "企业", "高校", "科研单位", "事业单位", "个人" };
                    break;

            }
            if (col.ShownAme.IndexOf("日") >= 0)
            {
                filtertype = new List<string> { "等于", "不等于", "大于", "小于" };
            }
            if (yesorno.Count > 0)
            {
                cmbvalue.Items.Clear();
                cmbvalue.Visible = true;
                textBox1.Visible = false;
                textBox1.Text = "";
                foreach (var s in yesorno)
                {
                    cmbvalue.Items.Add(s);
                }
                cmbvalue.SelectedIndex = 0;
            }
            else
            {
                cmbvalue.Visible = false;
                textBox1.Visible = true;
            }
            foreach (var s in filtertype)
            {
                cmbFilterType.Items.Add(s);
            }
            cmbFilterType.SelectedIndex = 0;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            CfGSTColumn col = (CfGSTColumn)cmbColName.SelectedItem;
            if (col.ShownAme.IndexOf("日") >= 0)
            {
                monthCalendar1.Visible = true;
                monthCalendar1.Top = this.textBox1.Top + this.textBox1.Height + 10;
                monthCalendar1.Left = textBox1.Left + 10;
                monthCalendar1.Update();
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {


            this.textBox1.Text = e.Start.ToString("yyyy年MM月dd日");


            monthCalendar1.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbvalue_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox1.Text = cmbvalue.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //inifilter(zid);
            BindData();
            // haschange = true;


        }
        private void inifilter(int zid)
        {
            
        }

        private void frmFilter_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (allsql != "")
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("请输入过滤条件", "提示");
                return;
            }
            monthCalendar1.Visible = false;
            CfGSTColumn col = (CfGSTColumn)cmbColName.SelectedItem;
            string filtertype = cmbFilterType.Text;
            string stroperator = "";
            string values = this.textBox1.Text.Trim();
            if (col.ShownAme.IndexOf("") > 0)
            {
                if (col.TBName != "show_base")
                {
                    values = values.FormatDate();
                }
            }
            switch (filtertype)
            {
                case "等于":
                    stroperator = "=";
                    break;
                case "不等于":
                    stroperator = "<>";
                    break;
                case "大于":
                    stroperator = ">";
                    break;
                case "小于":
                    stroperator = "<";
                    break;
                case "包含":
                    stroperator = "like";
                    values = "%" + values + "%";
                    break;
                case "不包含":
                    stroperator = "not like";
                    values = "%" + values + "%";
                    break;
                case "前方一致":
                    stroperator = "like";
                    values = values + "%";
                    break;
                case "后方一致":
                    stroperator = "like";
                    values = "%" + values;
                    break;
            }
            string sql = "";
            switch (col.ShownAme)
            {
                case "是否合作申请":
                case "是否国外来华":
                case "是否三局":
                case "是否五局":
                case "是否公知技术":
                    if (values == "是")
                    {
                        values = "1";
                        sql = string.Format("{0}.{1} {2} {3}", col.TBName, col.ColName, stroperator, values);
                    }
                    if (values == "否")
                    {
                        values = "0";
                        sql = string.Format("({0}.{1} {2} {3} or {0}.{1} is null)", col.TBName, col.ColName, stroperator, values);
                    }
                    break;
                default:
                    switch (col.ClType)
                    {
                        case "varchar":
                            values = "'" + values + "'";
                            break;
                    }

                    sql = string.Format("{0}.{1} {2} {3}", col.TBName, col.ColName, stroperator, values);
                    break;
            }

            var res = from x in dt.AsEnumerable()
                      where x["Tabl_ColName"].ToString() == col.ColName && x["Tablename"].ToString() == col.TBName
                      select x;
            DataRow f;
            if (res.Count() == 0)
            {
                f = dt.NewRow();

            }
            else
            {
                f = res.First();
            }
            string andor = " and ";
            if (cmbLuoJi.Text == "或者")
            {
                andor = " or ";
            }
            f["luoji"] = cmbLuoJi.Text;
            f["Colname"] = col.ShownAme;
            f["Tablename"] = col.TBName;
            f["Tabl_ColName"] = col.ColName;

            if (res.Count() == 0)
            {
                f["Sql"] += sql;
                f["Colopererator"] += col.ShownAme + " " + filtertype + " " + values + " ";
            }
            else
            {
                f["Sql"] += andor + sql;
                f["Colopererator"] += cmbLuoJi.Text + " " + col.ShownAme + " " + filtertype + " " + values + " ";
            }
            if (res.Count() == 0)
            {
                dt.Rows.Add(f);
            }
            BindData();
            ff();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dt.Rows.Clear();
            allsql = "";
            BindData();
            ff();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dt.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            BindData();
            ff();
        }


        private void ff()
        {
            if (dt.Rows.Count == 0)
            {
                allsql = "";
                return;
            }
            List<string> tables = (from x in dt.AsEnumerable()
                                   select x["Tablename"].ToString()).Distinct().ToList<string>();

            if (!tables.Contains("st_dt"))
            {
                tables.Insert(0, "st_dt");
            }

            string sql = "select  distinct(st_dt.sid) from ";
            string sql2 = "select  count(distinct(st_dt.sid)) from ";
            string tablenames = "";
            string join = "st_dt.ztid=" + zid;
            foreach (var tb in tables)
            {
                tablenames += tb + ",";
            }
            tablenames = tablenames.Trim(',');

            if (tables.Count > 1)
            {
                for (int i = 1; i < tables.Count; i++)
                {
                    join += string.Format(" and {0}.sid={1}.sid", tables[0], tables[i]);
                }
            }
            string where = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string andor = " and ";
                string tmplj = dt.Rows[i]["luoji"].ToString();
                if (tmplj == "或者")
                {
                    andor = " or ";
                }
                if (i == 0) andor = " and  ";
                where += andor + "(" + dt.Rows[i]["sql"].ToString() + ")";
            }
            allsql = sql + tablenames + " where " + join + where;
            string countsql = sql2 + tablenames + " where " + join + where;

            this.lab_sum.Text = "共：" + DBA.MySqlDbAccess.ExecuteScalar(CommandType.Text, countsql) + "条";

            // MessageBox.Show(allsql);
        }


    }
}
