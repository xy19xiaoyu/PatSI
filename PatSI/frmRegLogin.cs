#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2010 @ CPIC Corp
*	CLR 版本: 2.0.50727.3603
*	文 件 名: frmRegLogin.cs
*	创 建 人: xiwenlei(xiwl)
*	创建日期: 2010-5-20 17:06:19
*	版    本: V1.0
*	备注描述: $Myparameter1$           
*
* 修改历史: 
*   ****NO_1:
*	修 改 人: 
*	修改日期: 
*	描    述: $Myparameter1$           
******************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RegLib;
using Lib = RegLib;


namespace PatSI
{
    public partial class frmRegLogin : Form
    {
        public frmRegLogin()
        {
            InitializeComponent();
        }

        reginfo reg = new reginfo();

        private void frmRegLogin_Load(object sender, EventArgs e)
        {
            IniteRegInfor();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strRegNumber = RegUtility.GetRegNumber(reginfo.DiskNumber);

            if (txbYzm.Text.Trim() == "")//判断用户名是否为空
            {
                MessageBox.Show("请输入注册码！", "温馨提示：");
                txbYzm.Focus();
                return;
            }

            //注册码
            string zcm = DBA.Code.DecryptionAll(txbYzm.Text.Trim());


            //防止利用相同注册码重复注册延长使用时间
            if (reginfo.SerialNo == zcm)
            {
                MessageBox.Show("请勿使用相同注册码重复注册", "系统注册", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            //增加一个万用注册码
            if (txbYzm.Text.Trim() == @"gGHCJGCFREGDFBBIMEFDTEHEQIHEMBEH\CEH_CHBhIGIaIDERHCBdBIF[DBEcHFFECCI")
            {
                MessageBox.Show("恭喜您，注册成功！您获得了此系统的授权", "系统注册", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                //保存注册信息
                reginfo.WriteSerialNo(zcm);
                this.Close();
                this.DialogResult = DialogResult.OK;
            }
            else
            {

                //zcm.Substring(zcm.Length-10)  注册码生成日期 yyyy-mm-dd
                string strPcNm = zcm.Substring(0, zcm.Length - 20);
                //这里还得加个软件当前年版本和注册码年版本的校验！比如当前2006来个2007的注册码

                if (strRegNumber.Equals(strPcNm))
                {
                    //保存注册信息
                    Lib.reginfo.WriteSerialNo(zcm);
                    Lib.reginfo.ReadRegInfo();

                    MessageBox.Show("恭喜您，注册成功！", "系统注册", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                    this.DialogResult = DialogResult.OK;
                }
                else
                {

                    MessageBox.Show("请确认您的输入是否正确！", "注册失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        /// <summary>
        /// 信息检测
        /// </summary>
        private void IniteRegInfor()
        {
            int nHasDays = reg.GetDays();

            //2011.5.11 ywy修改，去掉试用功能
            nHasDays = 0;  //屏闭试用功能
            btnTryUse.Visible = false;

            //txbJqh.Text = DBA.Code.encrypt(Lib.reginfo.DiskNumber);
            txbJqh.Text = DBA.Code.encrypt(Lib.reginfo.DiskNumber_2014);
            btnTryUse.Text = string.Format("继续试用({0})天", nHasDays);

            ////如果已注册，则进入主页面
            //if (reg.CheckRegedit())
            //{
            //    this.Close();
            //    this.DialogResult = DialogResult.OK;                
            //}

            if (nHasDays < 1)
            {
                btnTryUse.Text = "继续试用(0)天";
                btnTryUse.Enabled = false;
            }
        }

        private void btnTryUse_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.OK;

        }


        private void frmRegLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //复制机器号
            Clipboard.SetText(this.txbJqh.Text.Trim());
        }
    }
}
