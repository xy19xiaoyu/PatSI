using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PatSI
{
    public partial class MDIMain : Form
    {
        public MDIMain()
        {
            InitializeComponent();
        }
        private static int ztid;

        public static int Ztid
        {
            get { return MDIMain.ztid; }
            set { MDIMain.ztid = value; }
        }
        private static string ztname;

        public static string Ztname
        {
            get { return MDIMain.ztname; }
            set { MDIMain.ztname = value; }
        }
        private static string dbtype;

        public static string Dbtype
        {
            get { return MDIMain.dbtype; }
            set { MDIMain.dbtype = value; }
        }



        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 专题库管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form is ZTListNew)
                {
                    form.Show();
                    return;
                }
            }
            ZTListNew frmnewzt = new ZTListNew();
            frmnewzt.MdiParent = this;
            frmnewzt.Show();
        }



        private void 管理GToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form is ExcelTemplate)
                {
                    form.Show();
                    return;
                }
            }
            ExcelTemplate tmp = new ExcelTemplate();
            tmp.Show();
        }

        private void 人工标引RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form is frmPtList)
                {
                    form.Show();
                    return;
                }
            }
            if (MDIMain.Ztname != null)
            {
                frmPtList frm = new frmPtList(MDIMain.Ztname, MDIMain.Ztid, MDIMain.Dbtype);
                frm.Show();
            }
            else
            {
                MessageBox.Show("请先选择专题库", "提示");
            }
        }

        private void MDIMain_Load(object sender, EventArgs e)
        {
            //InitSqlTest();
            tsLabUser.Text = string.Format("{0}[{1}]", BLL.SysMag.Login.StrLoginLname, BLL.SysMag.Login.StrLoginSname);
            foreach (Form form in this.MdiChildren)
            {
                if (form is ZTListNew)
                {
                    form.Show();
                    return;
                }
            }

            ZTListNew ztlist = new ZTListNew();
            ztlist.MdiParent = this;
            ztlist.Show();

        }

        private void InitSqlTest()
        {
            try
            {
                string strSql = "select (@rowIdx:=@rowIdx+1) AS 序号,a.id from cfg_stcolumn a,(SELECT @rowIdx:=0) b limit 1,1";
                DataTable dt = DBA.MySqlDbAccess.GetDataTable(CommandType.Text, strSql);
            }
            catch (Exception ex)
            {
            }
        }



        private void 统计分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            foreach (Form form in this.MdiChildren)
            {
                if (form is frmstat)
                {
                    form.Show();
                    return;
                }
            }
            if (MDIMain.Ztname != null)
            {
                frmstat st = new frmstat(MDIMain.Ztname, MDIMain.Ztid, MDIMain.Dbtype);
                st.MdiParent = this;
                st.WindowState = FormWindowState.Maximized;
                st.Show();
            }
            else
            {
                MessageBox.Show("请先选择专题库", "提示");
            }
        }

        private void 导入数据AToolStripMenuItem_Click(object sender, EventArgs e)
        {

            foreach (Form form in this.MdiChildren)
            {
                if (form is DBIN)
                {
                    form.Show();
                    return;
                }
            }
            DBIN dbin;
            if (MDIMain.Ztname != null)
            {
                dbin = new DBIN(MDIMain.Ztname, MDIMain.Ztid, MDIMain.Dbtype);
                dbin.Show();
            }
            else
            {
                MessageBox.Show("请先选择专题库", "提示");
            }

        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 标引规则维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            By.frmRuleMag frm = new By.frmRuleMag();
            frm.ShowDialog();
        }

        private void 版本信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Version frm = new Version();
            frm.Show();
        }
    }
}
