using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BLL.Import;
using BLL.DBLinq;
using BLL.UIHelper;
using IData;
using System.IO;
using DAL;
using BLL;
using BLL.Template;


namespace PatSI
{
    public partial class DBIN : Form
    {
        private Func<bool> ac;
        private IAsyncResult ar;
        private IDataImport import;
        private string type;
        private int ztid;
        private ImportTemplate tmplate;
        private string strname;
        public DBIN()
        {
            InitializeComponent();
            ZTListHelper.IniZTDropDownList(cmbZTList);
            TemplateListHelper.IniDropDownList(dgwtemplate, "", "");
            //cmbtmp.Enabled = false;
        }
        public DBIN(string name, int zid, string type)
        {
            this.type = type;
            this.strname = name;
            //inifilter(zid);
            InitializeComponent();
            ZTListHelper.IniZTDropDownList(cmbZTList, zid, name, type);
            //TemplateListHelper.IniDropDownList(dgwtemplate, "", "");
        }


        private void btnImport_Click(object sender, EventArgs e)
        {
            if (txtFileName.Text.Trim() == "")
            {
                MessageBox.Show("请选择数据源！", "提示");
                return;
            }
            if (this.txtTemplate.Text.Trim() == "")
            {
                MessageBox.Show("请选择模板！", "提示");
                return;
            }
            ztinfo zt = (ztinfo)cmbZTList.SelectedItem;
            ztid = zt.ID;
            strname = zt.Name;
            type = zt.DbType;
            string ztname = zt.Name;
            ztname = ztname.Replace(zt.DbType.ToUpper(), "").Trim();
            if (Path.GetExtension(this.txtFileName.Text.Trim()).IndexOf(".xls") >= 0)
            {
                import = new ExcelImport(txtFileName.Text.Trim(), ztname, ztid, type, tmplate);
            }
            else
            {
                switch (zt.DbType.ToUpper())
                {
                    case "CPRS":
                        import = new CPRSImport(txtFileName.Text.Trim(), ztname, ztid);
                        break;
                    case "WPI":
                        import = new WPIImport(txtFileName.Text.Trim(), ztname, ztid);
                        break;
                    case "EPODOC":
                        import = new EPODOCImport(txtFileName.Text.Trim(), ztname, ztid);
                        break;
                }
            }
            import.Skip = this.rbSkip.Checked;
            import.SetMaxProcess += new SetMaxValueEventHander(SetMax);
            import.ShowProcess += new ValueChangedEventHandler(ShowProcess);
            //import.Import();
            ac = new Func<bool>(import.Import);
            ar = ac.BeginInvoke(CallBack, ac);
            //MessageBox.Show("开始导入");                    
            //btnCen.Enabled = true;
            this.btnImport.Enabled = false;
        }
        private void CallBack(IAsyncResult ar)
        {
            Func<bool> ac = ar.AsyncState as Func<bool>;
            ac.EndInvoke(ar);
        }
        private void SetMax(object sender, int max)
        {
            //this.toolStripProgressBar1.Maximum = max;
            System.Windows.Forms.MethodInvoker invoker = () =>
            {
                this.tssProcess.Maximum = max;
            };
            if (this.statusStrip1.InvokeRequired)
            {
                this.statusStrip1.Invoke(invoker);
            }
            else
            {
                invoker();
            }
        }
        private void ShowProcess(object sneder, int value, int skip, int overwirte, string status)
        {

            System.Windows.Forms.MethodInvoker invoker = () =>
            {
                this.tssProcess.Value = value;
                this.tssMsg.Text = (((float)value) / this.tssProcess.Maximum).ToString("0.00%");
                this.StatusMsg.Text = value + "/" + this.tssProcess.Maximum;
                this.tssStatus.Text = status;
                if (status == "导入完毕")
                {
                    BLL.DBLinq.ZTHelper.UpdataSum(ztid);
                    string message;
                    if (rbSkip.Checked)
                    {
                        message = string.Format("导入完毕,共：[{0}] 条记录,成功：[{1}]条,跳过：{2}条,\n是否进行专利浏览", this.tssProcess.Maximum, value, skip);
                    }
                    else
                    {
                        message = string.Format("导入完毕,共：[{0}] 条记录,成功：[{1}]条,覆盖：{2}条,\n是否进行专利浏览", this.tssProcess.Maximum, value, overwirte);
                    }
                    if (MessageBox.Show(this, message, "导入完毕", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {

                        //inifilter(ztid);

                        try
                        {
                            frmPtList frmdbin = new frmPtList(strname, ztid, type);
                            frmdbin.MdiParent = this.MdiParent;
                            frmdbin.Show();
                            this.Close();
                        }
                        catch (Exception)
                        {

                        }
                    }
                    else
                    {
                        if (value == this.tssProcess.Maximum)
                        {
                            System.Windows.Forms.MethodInvoker invoker1 = () =>
                            {
                                this.btnImport.Enabled = true;
                            };
                            if (this.btnImport.InvokeRequired)
                            {
                                this.btnImport.Invoke(invoker1);
                            }
                            else
                            {
                                invoker1();
                            }
                        }
                    }
                }
            };
            if (this.statusStrip1.InvokeRequired)
            {
                this.statusStrip1.Invoke(invoker);
            }
            else
            {
                invoker();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.panTemplate.Location = new Point(groupTmplate.Location.X + txtTemplate.Location.X, groupTmplate.Location.Y + this.txtTemplate.Location.Y + this.txtTemplate.Height);
            this.panTemplate.Width = this.txtTemplate.Width;
            this.panTemplate.Visible = true;
            this.panTemplate.Update();
            this.Update();

        }

        private void list_template_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex == -1) return;
            if (e.ColumnIndex == -1) return;
            ztinfo zt = (ztinfo)cmbZTList.SelectedItem;
            string type = zt.DbType;
            string id = this.dgwtemplate.Rows[e.RowIndex].Cells["Id"].Value.ToString();
            string tmpname = this.dgwtemplate.Rows[e.RowIndex].Cells["name"].Value.ToString();

            if (Path.GetExtension(this.txtFileName.Text.Trim()).IndexOf(".xls") >= 0)
            {
                ExcelProView ex = new ExcelProView(this.txtFileName.Text, tmpname, type, id);
                if (ex.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.tmplate = ex.tmp;
                    this.txtTemplate.Text = tmplate.Name;
                    this.panTemplate.Visible = false;
                }
            }
            else
            {
                this.txtTemplate.Text = tmpname;
                this.panTemplate.Visible = false;
            }
        }

        private void cmbZTList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ztinfo zt = (ztinfo)cmbZTList.SelectedItem;
            string type = zt.DbType;
            TemplateListHelper.IniDropDownList(dgwtemplate, type, "");

        }

        private void txtFileName_TextChanged(object sender, EventArgs e)
        {
            string file = txtFileName.Text.Trim();
            if (file == "") return;
            string filetype = Path.GetExtension(this.txtFileName.Text.Trim()).Trim('.');
            ztinfo zt = (ztinfo)cmbZTList.SelectedItem;
            string type = zt.DbType;
            TemplateListHelper.IniDropDownList(dgwtemplate, type, filetype);
        }

        private void txtTemplate_Enter(object sender, EventArgs e)
        {

            string file = txtFileName.Text.Trim();
            if (file == "") return;
            string filetype = Path.GetExtension(this.txtFileName.Text.Trim()).Trim('.');
            ztinfo zt = (ztinfo)cmbZTList.SelectedItem;
            string type = zt.DbType;
            TemplateListHelper.IniDropDownList(dgwtemplate, type, filetype);
            this.panTemplate.Location = new Point(groupTmplate.Location.X + txtTemplate.Location.X, groupTmplate.Location.Y + this.txtTemplate.Location.Y + this.txtTemplate.Height);
            this.panTemplate.Width = this.txtTemplate.Width;
            this.panTemplate.Visible = true;
            this.panTemplate.Update();
            this.Update();
        }

        private void btnFileBrower_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtFileName.Text = ofd.FileName;
            }
        }

        private void rbSkip_CheckedChanged(object sender, EventArgs e)
        {
            showbold();
        }

        private void rbOverWrite_CheckedChanged(object sender, EventArgs e)
        {
            showbold();
        }

        private void DBIN_Load(object sender, EventArgs e)
        {
            showbold();
        }
        private void showbold()
        {
            if (rbSkip.Checked)
            {
                this.rbSkip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.rbOverWrite.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            else
            {
                this.rbSkip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.rbOverWrite.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
        }
        private void inifilter(int zid)
        {

        }

        private void txtTemplate_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
