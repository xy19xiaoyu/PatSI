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
    public partial class frmEditByItem : Form
    {
        private int nKeyId = 0;

        private List<DAL.IDXVAL> lisVal = null;

        public frmEditByItem(int _nKeyID, string strKeyName)
        {
            InitializeComponent();

            nKeyId = _nKeyID;
            this.txbByItemName.Text = strKeyName;
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
                if (((DAL.IDXVAL)item).VAL.Equals(strByWord))
                {
                    MessageBox.Show("该标引词已存在,请确认！", "温馨提示：");
                    txbWord.Focus();
                    return;
                }
            }
            int nOutValID = -1;
            bool bRs = IdexItemMag.AddNewVal(txbWord.Text.Trim(), nKeyId.ToString(), BLL.SysMag.Login.StrLoginUserID, out nOutValID);
            if (bRs)
            {
                this.lstByWord.SelectedValueChanged -= this.lstByWord_SelectedIndexChanged;

                lstByWord.BeginUpdate();
                DAL.IDXVAL valItem = new DAL.IDXVAL();
                valItem.ID = nOutValID;
                valItem.VAL = txbWord.Text.Trim();
                lisVal.Add(valItem);
                lstByWord.DataSource = null;
                lstByWord.DataSource = lisVal;
                lstByWord.ValueMember = "id";
                lstByWord.DisplayMember = "val";
                lstByWord.EndUpdate();

                MessageBox.Show("添加成功！", "温馨提示：");
                this.lstByWord.SelectedIndexChanged += new System.EventHandler(this.lstByWord_SelectedIndexChanged);
            }
            else
            {
                MessageBox.Show("添加失败，请重试！", "温馨提示：");
            }
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelSel_Click(object sender, EventArgs e)
        {
            if (lstByWord.SelectedIndex < 0)
            {
                MessageBox.Show("请选择要删除的数据！", "温馨提示：");
                return;
            }

            int nValID = lisVal[lstByWord.SelectedIndex].ID;
            lstByWord.BeginUpdate();
            bool bRs = IdexItemMag.DelKeyVal(nValID);
            if (bRs)
            {
                this.lstByWord.SelectedValueChanged -= this.lstByWord_SelectedIndexChanged;

                lisVal.RemoveAt(lstByWord.SelectedIndex);
                lstByWord.DataSource = null;
                lstByWord.DataSource = lisVal;
                lstByWord.ValueMember = "id";
                lstByWord.DisplayMember = "val";

                MessageBox.Show("删除成功！", "温馨提示：");

                this.lstByWord.SelectedIndexChanged += new System.EventHandler(this.lstByWord_SelectedIndexChanged);
            }
            else
            {
                MessageBox.Show("删除失败，请重试！", "温馨提示：");
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

            List<string> lstVal = new List<string>();
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

        private void frmEditByItem_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void InitData()
        {
            lisVal = IdexItemMag.getVal(nKeyId);

            lstByWord.DataSource = lisVal;
            lstByWord.ValueMember = "id";
            lstByWord.DisplayMember = "val";
        }

        private void btnUpKeyName_Click(object sender, EventArgs e)
        {
            bool bRs = IdexItemMag.UpdateKey(txbByItemName.Text.Trim(), BLL.SysMag.Login.StrLoginUserID, nKeyId.ToString());
            if (bRs)
            {
                MessageBox.Show("修改成功！", "温馨提示：");
            }
            else
            {
                MessageBox.Show("修改失败，请重试！", "温馨提示：");
            }

        }

        private void lstByWord_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstByWord.SelectedIndex < 0)
            {
                return;
            }
            txbWord.Text = lisVal[lstByWord.SelectedIndex].VAL;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lstByWord.SelectedIndex < 0)
            {
                MessageBox.Show("请选择要修改的数据！", "温馨提示：");
                return;
            }

            string strByWord = this.txbWord.Text.ToString().Trim();//获取标引词        
            if (strByWord == "")//判断是否为空
            {
                MessageBox.Show("请填写标引词！", "温馨提示：");
                txbWord.Focus();
                return;
            }

            foreach (var item in lstByWord.Items)
            {
                if (((DAL.IDXVAL)item).VAL.Equals(strByWord))
                {
                    MessageBox.Show("该标引词已存在,请确认！", "温馨提示：");
                    txbWord.Focus();
                    return;
                }
            }

            bool bRs = IdexItemMag.UpdateVal(strByWord, BLL.SysMag.Login.StrLoginUserID, lisVal[lstByWord.SelectedIndex].ID.ToString());
            if (bRs)
            {
                this.lstByWord.SelectedValueChanged -= this.lstByWord_SelectedIndexChanged;
                lstByWord.BeginUpdate();
                lisVal[lstByWord.SelectedIndex].VAL = strByWord;
                lstByWord.DataSource = null;
                lstByWord.DataSource = lisVal;
                lstByWord.ValueMember = "id";
                lstByWord.DisplayMember = "val";
                lstByWord.EndUpdate();

                MessageBox.Show("修改成功！", "温馨提示：");

                this.lstByWord.SelectedIndexChanged += new System.EventHandler(this.lstByWord_SelectedIndexChanged);
            }
            else
            {
                MessageBox.Show("修改失败，请重试！", "温馨提示：");
            }

        }



    }
}
