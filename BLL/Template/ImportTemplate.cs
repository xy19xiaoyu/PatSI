using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Template
{
    [Serializable]
    public class ImportTemplate
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string type;


        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private int firstRowNum;

        public int FirstRowNum
        {
            get { return firstRowNum; }
            set { firstRowNum = value; }
        }
        private bool hasHeadColumn;

        public bool HasHeadColumn
        {
            get { return hasHeadColumn; }
            set { hasHeadColumn = value; }
        }
        private Dictionary<string, ColumnMapping> columnMapping = new Dictionary<string, ColumnMapping>();

        public Dictionary<string, ColumnMapping> ColumnMapping
        {
            get { return columnMapping; }
            set { columnMapping = value; }
        }

        private bool isWPIDefault;

        public bool IsWPIDefault
        {
            get { return isWPIDefault; }
            set { isWPIDefault = value; }
        }
        private bool isEPODCODefault;

        public bool IsEPODCODefault
        {
            get { return isEPODCODefault; }
            set { isEPODCODefault = value; }
        }


    }
}
