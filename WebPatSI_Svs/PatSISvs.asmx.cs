using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;

namespace WebPatSI_Svs
{
    /// <summary>
    /// PatSISvs 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class PatSISvs : System.Web.Services.WebService
    {

        [WebMethod]
        public string getLastVersion()
        {
            string strRs = "";
            try
            {
                strRs = File.ReadLines(HttpContext.Current.Server.MapPath("VersionCfg.txt")).ToList()[0];
            }
            catch (Exception ex)
            {
                strRs = "";
            }
            return strRs;
        }

        [WebMethod]
        public DateTime getWebHostDateTime()
        {
            return DateTime.Now;
        }
    }
}
