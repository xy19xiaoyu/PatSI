using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL;
using BLL.Model;

namespace PatSI.By
{
    public partial class frmSetShowFiles : Form
    {
        private string strDbType = "";
        public bool bIsSetExportFileds = false;
        public frmSetShowFiles(string _strDbTy)
        {
            InitializeComponent();
            strDbType = _strDbTy;
        }

        private void frmSetShowFiles_Load(object sender, EventArgs e)
        {
            this.Text = bIsSetExportFileds ? "设置导出字段" : "设置显示字段";

            chkLstBoxFiled.Items.Clear();
            chkLstBoxAutoIndex.Items.Clear();

            CheckBox ck = null;
            foreach (var item in PtDataHelper.TbShow_base.Values)
            {
                if (!item.ReadOnly)
                {
                    if (item.StrDbTy == "" || item.StrDbTy.Contains(strDbType))
                    {
                        if (item.BIsAutoIndexFiled)
                        {
                            chkLstBoxAutoIndex.Items.Add(item, item.IsShow);
                        }
                        else
                        {
                            chkLstBoxFiled.Items.Add(item, item.IsShow);
                        }
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TbFiled_Cfg tbFiledItem = null;
            for (int i = 0; i < chkLstBoxFiled.Items.Count; i++)
            {
                tbFiledItem = (TbFiled_Cfg)chkLstBoxFiled.Items[i];
                PtDataHelper.TbShow_base[tbFiledItem.StrTbName + tbFiledItem .StrDbTy+ tbFiledItem.StrFiledName].IsShow = chkLstBoxFiled.GetItemChecked(i);
            }

            for (int i = 0; i < chkLstBoxAutoIndex.Items.Count; i++)
            {
                tbFiledItem = (TbFiled_Cfg)chkLstBoxAutoIndex.Items[i];
                PtDataHelper.TbShow_base[tbFiledItem.StrTbName + tbFiledItem.StrDbTy + tbFiledItem.StrFiledName].IsShow = chkLstBoxAutoIndex.GetItemChecked(i);
            }

            if (bIsSetExportFileds == false)
            {
                MessageBox.Show("设置成功！", this.Text, MessageBoxButtons.OK);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void chkBase_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chkLstBoxFiled.Items.Count; i++)
            {
                chkLstBoxFiled.SetItemChecked(i, chkBase.Checked);
            }


        }

        private void chkIndexFiled_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chkLstBoxAutoIndex.Items.Count; i++)
            {
                chkLstBoxAutoIndex.SetItemChecked(i, chkIndexFiled.Checked);
            }
        }
    }
}
