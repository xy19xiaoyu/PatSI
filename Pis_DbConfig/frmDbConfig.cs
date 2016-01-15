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
using MySql.Data.MySqlClient;
using System.IO;

namespace Pis_DbConfig
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
            XmlDocument doc = new XmlDocument();
            doc.Load("PatSI.exe.config");
            XmlNode root = doc.SelectSingleNode("configuration");
            XmlNode node = root.SelectSingleNode("connectionStrings/add[@name='MySqlServerStr']");
            XmlElement el = node as XmlElement;
            el.SetAttribute("connectionString",
                string.Format("Server={0};Database=ztst;Uid={1};Pwd={2}; charset=utf8;Allow User Variables=True",
                txbDbName.Text.Trim(), txbUs.Text.Trim(), txbPw.Text.Trim()));
            doc.Save("PatSI.exe.config");

            CreateDb();
            MessageBox.Show("设置成功！");
        }

        private bool  CreateDb()
        {
            bool bRs = false;

            string connStr = string.Format("server={0};user={1};database=;password={2};charset=utf8;Allow User Variables=True", txbDbName.Text.Trim(), txbUs.Text.Trim(), txbPw.Text.Trim());
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = File.ReadAllText("createDb.sql");
                MySqlScript script = new MySqlScript(conn);

                script.Query = sql;
                //script.Delimiter = "??";
                int count = script.Execute();
                
                Console.WriteLine("Executed " + count + " statement(s)");
                //script.Delimiter = ";";
                conn.Close();
                Console.WriteLine("Delimiter: " + script.Delimiter);
                Console.WriteLine("Query: " + script.Query);


                /////////////
                connStr = string.Format("server={0};user={1};database=ztst;password={2};charset=utf8;Allow User Variables=True", txbDbName.Text.Trim(), txbUs.Text.Trim(), txbPw.Text.Trim());
                conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                sql = File.ReadAllText("ztst.sql");
                script = new MySqlScript(conn);
                script.Query = sql;
                //script.Delimiter = "??";
                count = script.Execute();
                Console.WriteLine("Executed " + count + " statement(s)");
                //script.Delimiter = ";";
                conn.Close();
            }
            catch (Exception ex)
            {
                bRs = false;
                Console.WriteLine(ex.ToString());
            }

            //conn.Close();
            Console.WriteLine("Done.");
            return bRs;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
