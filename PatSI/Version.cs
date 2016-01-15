using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using log4net;
namespace PatSI
{
    public partial class Version : Form
    {
        private ILog log = log4net.LogManager.GetLogger("System");
        public Version()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void showversion()
        {
            DataTable dt = DBA.MySqlDbAccess.GetDataTable(CommandType.Text, "select * from cfg_dbversion");
            int exeversion = Convert.ToInt32(dt.Rows[0]["dbversion"].ToString());
            string[] files = Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory + "MySqlScript");
            List<int> versionhis = new List<int>();
            foreach (var file in files)
            {
                try
                {
                    versionhis.Add(Convert.ToInt32(Path.GetFileNameWithoutExtension(file)));
                }
                catch (Exception)
                {
                    continue;
                }
            }
            int newserion = versionhis.Max();
            dbversion_new.Text = newserion.ToString();
            dbversion_now.Text = exeversion.ToString();

            sysversion.Text = Program.Version;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("此操作会清空软件中的所有数据,如果有数据,请做好数据备份(导出)!您是否继续?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = DBA.MySqlDbAccess.GetMySqlConnection())
                    {

                        conn.Open();

                        string sql = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "xy.dll", Encoding.GetEncoding("utf-8"));
                        MySqlScript script = new MySqlScript(conn);

                        script.Query = sql;
                        int count = script.Execute();
                    }
                    MessageBox.Show("数据库已更新至最新版本！", "提示");
                    log.Info("执行数据库重置:" + DateTime.Now.ToString());
                    showversion();
                }
                catch (Exception ex)
                {
                    log.Error(ex.ToString());
                }
            }
        }

        private void Version_Load(object sender, EventArgs e)
        {
            showversion();
        }
    }
}
