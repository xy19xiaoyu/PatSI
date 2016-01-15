using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IData;
using System.IO;
using System.Threading;
using System.Collections;
using System.Data;
using MySql.Data.MySqlClient;
using DBA;
using log4net;
namespace BLL.Import
{
    public class IDataImportAdapter : IData.IDataImport, IDisposable
    {
        private bool skip;
        private string filename;

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }
        private DataType type;
        private Thread thimport;
        private int skip_sum;

        public ILog log = LogManager.GetLogger("Import");

        public int Skip_sum
        {
            get { return skip_sum; }
            set { skip_sum = value; }
        }


        private int overwrite_sum;

        public int Overwrite_sum
        {
            get { return overwrite_sum; }
            set { overwrite_sum = value; }
        }


        public Thread Thimport
        {
            get { return thimport; }
            set { thimport = value; }
        }
        private StreamReader sr;

        public StreamReader Sr
        {
            get { return sr; }
            set { sr = value; }
        }
        private Encoding encodeing;

        public Encoding Encodeing
        {
            get { return encodeing; }
            set { encodeing = value; }
        }
        private string ztname;
        private int ztid;

        public IDataImportAdapter(string filename, string ztname, int zid)
        {
            skip_sum = 0;
            overwrite_sum = 0;
            this.ZTId = zid;
            this.filename = filename;
            sids = new Dictionary<string, bool>();
            DataTable dt = DBA.MySqlDbAccess.GetDataTable(CommandType.Text, "select sid from st_dt where ztid=" + zid);
            foreach (DataRow row in dt.Rows)
            {
                string sid = row[0].ToString();
                if (sids.ContainsKey(sid)) continue;
                sids.Add(sid, true);
            }
        }

        public bool Skip
        {
            get
            {
                return skip;
            }
            set
            {
                skip = value;
            }
        }


        public string FilePath
        {
            get
            {
                return filename;
            }
            set
            {
                this.filename = value;
            }
        }

        public DataType Type
        {
            get
            {
                return type;
            }
            set
            {
                this.type = value;
            }
        }


        public string ztName
        {
            get
            {
                return ztname;
            }
            set
            {
                ztname = value;
            }
        }

        public int ZTId
        {
            get
            {
                return ztid;
            }
            set
            {
                ztid = value;
            }
        }

        public virtual event IData.ValueChangedEventHandler ShowProcess;

        public virtual event IData.SetMaxValueEventHander SetMaxProcess;

        public virtual bool Import()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (Sr != null)
            {
                Sr.Close();
                Sr.Dispose();
            }
        }

        private Dictionary<string, bool> sids;
        public Dictionary<string, bool> Sids
        {
            get
            {
                return sids;
            }
            set
            {
                sids = value;
            }
        }

        public bool CheckExist(string sid)
        {
            return sids.ContainsKey(sid);
        }

        public bool DelExist(string zid, string sid)
        {
            string st_ipc = string.Format("delete from st_ipc where ztid={0} and sid='{1}'", ztid, sid);
            string st_pa = string.Format("delete from  st_pa where ztid={0} and sid='{1}'", ztid, sid);
            string st_pr = string.Format("delete from  st_pr where ztid={0} and sid='{1}'", ztid, sid);
            string show_base = string.Format("delete from  show_base where ztid={0} and sid='{1}'", ztid, sid);
            string st_iv = string.Format("delete from  st_iv where ztid={0} and sid='{1}'", ztid, sid);
            string st_pns = string.Format("delete from st_pns where ztid={0} and sid='{1}'", ztid, sid);
            string st_ans = string.Format("delete from st_ans where ztid={0} and sid='{1}'", ztid, sid);
            string st_dmc = string.Format("delete from  st_dc where ztid={0} and sid='{1}'", ztid, sid);
            string st_fml = string.Format("delete from  st_fml where ztid={0} and sid='{1}'", ztid, sid);
            string st_dt = string.Format("delete from st_dt where ztid={0} and sid='{1}'", ztid, sid);


            using (MySqlConnection conn = MySqlDbAccess.GetMySqlConnection())
            {
                conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();
                MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, st_ipc);
                MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, st_pa);
                MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, st_pr);
                MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, show_base);
                MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, st_iv);
                MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, st_pns);
                MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, st_ans);
                MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, st_dmc);
                MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, st_fml);
                MySqlDbAccess.ExecNoQuery(trans, CommandType.Text, st_dt);
                trans.Commit();

            }
            return true;
        }
        public static int GetYearDeff(string maxday, string minday)
        {
            if (maxday == null || minday == null) return 0;
            if (maxday == "" || minday == "") return 0;
            try
            {
                DateTime dt1 = new DateTime(Convert.ToInt32(maxday.Substring(0, 4)), Convert.ToInt32(maxday.Substring(4, 2)), Convert.ToInt32(maxday.Substring(6, 2)));
                DateTime dt2 = new DateTime(Convert.ToInt32(minday.Substring(0, 4)), Convert.ToInt32(minday.Substring(4, 2)), Convert.ToInt32(minday.Substring(6, 2)));
                return (dt1 - dt2).Days / 30;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
