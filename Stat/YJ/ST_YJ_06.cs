using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using IStat;
using StatCfg;
using System.Data;

namespace Stat.YJ
{
    public class ST_YJ_06 : IStatAdapter
    {


        public ST_YJ_06(string name, cfg config)
            : base(name, config)
        {

        }

        public ST_YJ_06(string name)
            : base(name)
        {

        }

        public override string GetStatSQL()
        {


            return "";
        }
        public override bool Stat()
        {


            Dt = new DataTable();
            Dt.Columns.Add(" ");
            Dt.Columns.Add("中美");
            Dt.Columns.Add("中日");
            Dt.Columns.Add("中欧");
            Dt.Columns.Add("中韩");
            DataRow row = Dt.NewRow();
            int oyear = 0;
            int cyear = 0;
            int jyear = 0;
            int hyear = 0;
            int uyear = 0;
            object oo = DBA.MySqlDbAccess.ExecuteScalar(CommandType.Text, "select min(ady) from st_dt where ztid=" + Zid + " and type='发明专利' and  zhou = '欧洲'");
            if (oo != null)
            {
                oyear = Convert.ToInt32(oo);
            }
            object oc = DBA.MySqlDbAccess.ExecuteScalar(CommandType.Text, "select min(ady) from st_dt where ztid=" + Zid + " and type='发明专利' and  gj = '中国'");
            if (oc != null)
            {
                cyear = Convert.ToInt32(oc);
            }

            object oj = DBA.MySqlDbAccess.ExecuteScalar(CommandType.Text, "select min(ady) from st_dt where ztid=" + Zid + " and type='发明专利' and  gj = '日本'");
            if (oj != null)
            {
                jyear = Convert.ToInt32(oj);
            }

            object oh = DBA.MySqlDbAccess.ExecuteScalar(CommandType.Text, "select min(ady) from st_dt where ztid=" + Zid + " and type='发明专利' and  gj = '韩国'");
            if (oh != null)
            {
                hyear = Convert.ToInt32(oh);
            }

            object ou = DBA.MySqlDbAccess.ExecuteScalar(CommandType.Text, "select min(ady) from st_dt where ztid=" + Zid + " and type='发明专利' and  gj = '美国'");
            if (ou != null)
            {
                uyear = Convert.ToInt32(ou);
            }
            row[0] = "首件申请年差";
            row[1] = cyear - uyear;
            row[2] = cyear - jyear;
            row[3] = cyear - oyear;
            row[4] = cyear - hyear;
            Dt.Rows.Add(row);
            return true;
        }
    }
}
