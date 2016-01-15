using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Template
{
    [Serializable]
    public class ColumnMapping
    {
        public ColumnMapping()
        {
            excelColumnName = "";
            systemColumnName = "";
            forget = false;
            autoindex = false;
            splitChar = "";
            tablename = "";
            coltype = "";
        }
        private string excelColumnName;

        public string ExcelColumnName
        {
            get { return excelColumnName; }
            set { excelColumnName = value; }
        }

        private string systemColumnShowName;

        public string SystemColumnShowName
        {
            get { return systemColumnShowName; }
            set { systemColumnShowName = value; }
        }
        private string systemColumnName;

        public string SystemColumnName
        {
            get { return systemColumnName; }
            set { systemColumnName = value; }
        }
        private bool forget;

        public bool Forget
        {
            get { return forget; }
            set { forget = value; }
        }
        private bool autoindex;

        public bool Autoindex
        {
            get { return autoindex; }
            set { autoindex = value; }
        }
        private string splitChar;

        public string SplitChar
        {
            get { return splitChar; }
            set { splitChar = value; }
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
    }
}
