using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IData
{
    public enum DataType
    {
        CPRS = 1,
        WPI = 2,
        EPODOC = 3
    }
    public interface IDataImport : IDisposable
    {
        bool Skip { get; set; }
        string ztName { get; set; }
        int ZTId { get; set; }
        string FilePath { get; set; }
        DataType Type { get; set; }
        Dictionary<string, bool> Sids { get; set; }
        bool Import();
        bool CheckExist(string sid);
        bool DelExist(string zid, string sid);
        event ValueChangedEventHandler ShowProcess;
        event SetMaxValueEventHander SetMaxProcess;
    }

}
