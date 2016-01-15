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
    public partial class frmAddByItem : Form
    {
        public frmAddByItem()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strByWord = this.txbWord.Text.ToString().Trim();//获取标引词        
            if (strByWord == "")//判断是否为空
            {
                MessageBox.Show("请填写标引词！", "温馨提示：");
                txbWord.Focus();
                return;
            }

            foreach (var item in lstByWord.Items)
            {
                if (item.ToString().Equals(strByWord))
                {
                    MessageBox.Show("该标引词已存在,请确认！", "温馨提示：");
                    txbWord.Focus();
                    return;
                }
            }

            lstByWord.Items.Add(strByWord);
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {            
            this.Close();
        }

        private void btnDelSel_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection indices = lstByWord.SelectedIndices;
            int count = indices.Count;
            lstByWord.BeginUpdate();
            for (int i = 0; count != 0; i++)
            {
                lstByWord.Items.RemoveAt(indices[0]);
                count--;
            }
            lstByWord.EndUpdate();
        }

        private void btnDelAll_Click(object sender, EventArgs e)
        {
            lstByWord.Items.Clear();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string strByName = this.txbByItemName.Text.ToString().Trim();//获取标引项名称     
            string strByWords = "";

            if (strByName == "")//判断是否为空
            {
                MessageBox.Show("请填写标引项名称！", "温馨提示：");
                txbByItemName.Focus();
                return;
            }

            if (lstByWord.Items.Count == 0)//判断是否为空
            {
                MessageBox.Show("请添加标引词！", "温馨提示：");
                txbWord.Focus();
                return;
            }

            List<string> lstVal=new List<string>();
            foreach (var item in lstByWord.Items)
            {
                lstVal.Add(item.ToString());
            }

            bool bRs = IdexItemMag.AddNewKey(strByName, lstVal, BLL.SysMag.Login.StrLoginUserID);

            if (bRs)
            {
                MessageBox.Show("添加成功！", "温馨提示：");
            }
            else
            {
                MessageBox.Show("添加失败，请重试！", "温馨提示：");
            }
            this.DialogResult = DialogResult.OK;

        }


    }
}
