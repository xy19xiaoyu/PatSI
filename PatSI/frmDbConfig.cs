using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;
using System.Configuration;

namespace PatSI
{
    public partial class frmDbConfig : Form
    {
        public frmDbConfig()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ////Properties.Settings connset = Properties.Settings.Default;

            ////System.Configuration.ConfigurationManager.ConnectionStrings["MySqlServerStr"].ConnectionString =
            ////    string.Format("Server={0};Database=ztst;Uid={1};Pwd={2}; charset=utf8;Allow User Variables=True",
            ////    txbDbName.Text.Trim(), txbUs.Text.Trim(), txbPw.Text.Trim());

            ////XmlDocument doc = new XmlDocument();
            ////doc.Load("..//..//App.config");
            ////XmlNode root = doc.SelectSingleNode("configuration");
            ////XmlNode node = root.SelectSingleNode("connectionStrings/add[@name=’MySqlServerStr’]");
            ////XmlElement el = node as XmlElement;
            ////el.SetAttribute("MySqlServerStr", "Data Source=orcl;Persist Security Info=True;User ID=xhfoc;Password=foc;Unicode=True");
            ////doc.Save("..//..//App.config");

            //MessageBox.Show(System.Configuration.ConfigurationManager.ConnectionStrings["MySqlServerStr"].ConnectionString);
            ////MessageBox.Show(DBA.MySqlDbAccess.ConnStr);

            ////------------
            ////读取程序集的配置文件
            //string assemblyConfigFile = Assembly.GetEntryAssembly().Location;
            //string appDomainConfigFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            ////获取ConnectionStrings节点
            


            ////删除name，然后添加新值
            //config.ConnectionStrings.ConnectionStrings["MySqlServerStr"].ConnectionString=
            //    string.Format("Server={0};Database=ztst;Uid={1};Pwd={2}; charset=utf8;Allow User Variables=True",
            //    txbDbName.Text.Trim(), txbUs.Text.Trim(), txbPw.Text.Trim());
            

            ////保存配置文件
            //config.Save();

            //ConfigurationManager.RefreshSection("connectionStrings"); 

            //MessageBox.Show(System.Configuration.ConfigurationManager.ConnectionStrings["MySqlServerStr"].ConnectionString);
            ////MessageBox.Show(DBA.MySqlDbAccess.ConnStr);
            //Application.Restart();
            
        }
    }
}
