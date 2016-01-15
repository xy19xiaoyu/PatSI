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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnClose_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strLName = this.txbUname.Text.ToString().Trim();//获取用户名
            string strPw = this.txbPwd.Text.ToString().Trim();//获取密码
            if (strLName == "")//判断用户名是否为空
            {
                MessageBox.Show("请填写登录名！", "温馨提示：");
                txbUname.Focus();
                return;
            }

            if (strPw == "")//判断用户名是否为空
            {
                MessageBox.Show("请填写登录密码！", "温馨提示：");
                txbPwd.Focus();
                return;
            }


            //链接数据库，查看是否存在该用户
            DataTable dt = BLL.SysMag.Login.Select_YongHu(strLName);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("您输入的登录名不存在，请检查！", "温馨提示：");
                txbUname.Clear();
                txbPwd.Clear();
            }
            else if (dt.Rows.Count == 1)
            {
                if (dt.Rows[0]["flag"].ToString().Trim().Equals("0"))
                {
                    MessageBox.Show("该登录账号尚未激活或已停用，请与管理员联系！", "温馨提示：");
                    return;
                }

                //链接数据库，查看用户和密码是否正确
                if (dt.Rows[0]["pw"].ToString().Trim().Equals(strPw))
                {
                    BLL.SysMag.Login.StrLoginLname = strLName;
                    BLL.SysMag.Login.StrLoginSname = dt.Rows[0]["Sname"].ToString().Trim();
                    BLL.SysMag.Login.StrLoginUserID = dt.Rows[0]["id"].ToString().Trim();

                    this.Hide();
                    new MDIMain().ShowDialog();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("密码错误,请重新输入！", "温馨提示：");
                    txbPwd.Focus();
                }
            }
            else
            {
                MessageBox.Show("您的登录名存在异常，请与管理员联系！", "温馨提示：");
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txbUname.Focus();
            pictureBox5.Parent = this;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = global::PatSI.Properties.Resources.btnlogin1;
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = global::PatSI.Properties.Resources.btn_login;
        }

        private void txbPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void txbUname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txbPwd.Focus();
            }
        }

    }
}
