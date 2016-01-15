
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace PatSI
{
    public class Filter
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private CfGSTColumn cfg;

        public CfGSTColumn Cfg
        {
            get { return cfg; }
            set { cfg = value; }
        }

        private string luoji;

        public string Luoji
        {
            get { return luoji; }
            set { luoji = value; }
        }
        
        private string colname;

        public string Colname
        {
            get { return colname; }
            set { colname = value; }
        }
        private string colopererator;

        public string Colopererator
        {
            get { return colopererator; }
            set { colopererator = value; }
        }
        private string tablename;

        public string Tablename
        {
            get { return tablename; }
            set { tablename = value; }
        }
        private string coltype;

        public string Coltype
        {
            get { return coltype; }
            set { coltype = value; }
        }
        private string tabl_ColName;

        public string Tabl_ColName
        {
            get { return tabl_ColName; }
            set { tabl_ColName = value; }
        }
        private string sql;

        public string Sql
        {
            get { return sql; }
            set { sql = value; }
        }
    }
}
