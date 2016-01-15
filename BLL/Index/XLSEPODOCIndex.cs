using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using xyExtensions;
using AddressExtensions;
using System.Text.RegularExpressions;

namespace BLL.Index
{
    public class XLSEPODOCIndex
    {
        public static Dictionary<string, CfGCountry> zhouguo = new Dictionary<string, CfGCountry>();
        static XLSEPODOCIndex()
        {
            if (zhouguo.Count == 0)
            {
                MySqlDataContext db = new MySqlDataContext();
                foreach (var x in db.CfGCountry)
                {
                    if (zhouguo.ContainsKey(x.DAimA)) continue;
                    zhouguo.Add(x.DAimA, x);
                }
            }
        }
        public static Regex regPr = new Regex("\\d{4}-\\d{1,2}-\\d{1,2}");
        public static Regex regap = new Regex("\\[[^\\]]*\\]");
        private static Regex regnoap = new Regex("(Cont of|Based on|Div Ex)");
        private static Regex regwogj = new Regex("(?<gj>[A-Za-z]{2})");
        private static Regex regPnsplit = new Regex("DW\\d{3,8}");
        private static Regex regspace = new Regex("\\s{2,}");

        public static Dictionary<string, bool> noindex = new Dictionary<string, bool>();
        public static void initmp(Template.ImportTemplate tmp)
        {
            if (noindex.Count == 0)
            {
                foreach (var x in tmp.ColumnMapping.Values)
                {
                    if (x.Tablename.ToUpper() == "ST_DT")
                    {
                        string colname = x.SystemColumnShowName;
                        if (noindex.ContainsKey(colname)) continue;
                        noindex.Add(colname, false);
                    }
                }
            }
        }

        public static STDT AutoIndex(ShowBase sb, STDT st, out List<STPa> pa, out List<STIV> iv, out List<STIpc> ic, out List<STPR> pr, out List<STFML> fml, out List<STCPc> cpc)
        {
            st.ZTID = sb.ZTID;
            st.SiD = sb.SiD;

            pa = new List<STPa>();
            iv = new List<STIV>();
            ic = new List<STIpc>();
            pr = new List<STPR>();
            fml = new List<STFML>();
            cpc = new List<STCPc>();
            int i = 0;
            //申请号
            if (sb.An != null)
            {
                string[] ans = sb.An.Trim().Split(' ');
                switch (ans.Length)
                {
                    case 2:
                        st.An = ans[0];
                        if (ans[1].FormatDate().GetYear() != 1800)
                        {
                            st.Ad = ans[1].FormatDate();
                            if (!noindex.ContainsKey("申请年"))
                            {
                                st.AdY = st.Ad.GetYear();
                            }
                        }
                        break;
                    case 1:
                        st.An = ans[0];
                        st.Ad = sb.Ad.FormatDate();
                        if (!noindex.ContainsKey("申请年"))
                        {
                            st.AdY = st.Ad.GetYear();
                        }
                        break;
                }
            }
            if (sb.PN != null)
            {
                //公开 
                string[] pns = sb.PN.Trim().Split(' ');
                switch (pns.Length)
                {
                    case 3:
                        st.PN = pns[0] + pns[1];
                        if (pns[2].FormatDate().GetYear() != 1800)
                        {
                            st.PD = pns[2].FormatDate();
                            if (!noindex.ContainsKey("公开年"))
                            {
                                st.PDy = st.PD.GetYear();
                            }
                        }
                        break;
                    case 2:
                        st.PN = pns[0];
                        if (pns[1].FormatDate().GetYear() != 1800)
                        {
                            st.PD = pns[1].FormatDate();
                            if (!noindex.ContainsKey("公开年"))
                            {
                                st.PDy = st.PD.GetYear();
                            }
                        }
                        break;
                    case 1:
                        st.PN = pns[0];
                        st.PD = sb.PD.FormatDate();
                        if (!noindex.ContainsKey("公开年"))
                        {
                            st.PDy = st.PD.GetYear();
                        }
                        break;
                }
            }
            string gj = st.An.Left(2).ToUpper();
            gj = st.An.Substring(0, 2).ToUpper();
            if (gj == "WO")
            {
                Match mh = regwogj.Match(st.An.Substring(2));
                if (mh.Success)
                {
                    gj = mh.Groups["gj"].Value;
                    if (!zhouguo.ContainsKey(gj))
                    {
                        gj = "WO";
                    }
                }

            }

            if (zhouguo.ContainsKey(gj))
            {
                if (!noindex.ContainsKey("国家"))
                {
                    st.GJ = zhouguo[gj].GJ;
                }
                if (!noindex.ContainsKey("洲际"))
                {
                    st.ZHoU = zhouguo[gj].ZHoU;
                }
            }
            else
            {
                if (!noindex.ContainsKey("国家"))
                {
                    st.GJ = "未知";
                }
                if (!noindex.ContainsKey("洲际"))
                {
                    st.ZHoU = "未知";
                }
            }
            if (sb.FaMN != null)
            {
                fml.Add(new STFML() { ZTID = sb.ZTID, SiD = sb.SiD, FMLid = Convert.ToInt32(sb.FaMN) });
            }
            if (sb.Ipc != null)
            {
                //IPC           
                string[] aryipcs = sb.Ipc.Split(';');
                i = 0;
                foreach (var stripc in aryipcs)
                {
                    if (stripc == "") continue;
                    i++;
                    string strtmpipc = stripc.Trim().FormatIPC();
                    STIpc tmpipc = new STIpc() { ZTID = sb.ZTID, SiD = sb.SiD };
                    tmpipc.Ipc = strtmpipc.Trim();
                    tmpipc.Ipc1 = strtmpipc.Left(1);
                    tmpipc.Ipc3 = strtmpipc.Left(3);
                    tmpipc.Ipc4 = strtmpipc.Left(4);
                    tmpipc.Ipc7 = strtmpipc.Left(7);
                    tmpipc.Sort = (SByte)i;
                    ic.Add(tmpipc);
                }
                if (ic.Count > 0)
                {
                    if (!noindex.ContainsKey("主IPC"))
                    {
                        st.FIpc = ic[0].Ipc;
                    }
                    if (!noindex.ContainsKey("IPC数量"))
                    {
                        st.IpcSum = (SByte)ic.Count;
                    }
                }
            }
            if (sb.CPc != null)
            {
                //IPC           
                string[] arycpcs = sb.CPc.Split(';');
                i = 0;
                foreach (var strcpc in arycpcs)
                {
                    if (strcpc == "") continue;
                    i++;
                    string strtmpcpc = strcpc.Trim().FormatIPC();
                    STCPc tmpcpc = new STCPc() { ZTID = sb.ZTID, SiD = sb.SiD };
                    tmpcpc.CPc = strtmpcpc.Trim();
                    tmpcpc.CPc1 = strtmpcpc.Left(1);
                    tmpcpc.CPc3 = strtmpcpc.Left(3);
                    tmpcpc.CPc4 = strtmpcpc.Left(4);
                    tmpcpc.CPc7 = strtmpcpc.Left(7);
                    tmpcpc.Sort = (SByte)i;
                    cpc.Add(tmpcpc);
                }
                if (cpc.Count > 0)
                {
                    if (!noindex.ContainsKey("主CPC"))
                    {
                        st.FCPc = cpc[0].CPc;
                    }
                    if (!noindex.ContainsKey("CPC数量"))
                    {
                        st.CPcSum = (SByte)ic.Count;
                    }
                }
            }

            if (sb.Pa != null)
            {
                //申请人
                string[] pas = sb.Pa.Split(";".ToArray());
                foreach (var strpa in pas)
                {
                    if (strpa.Trim() == "") continue;
                    i++;
                    pa.Add(new STPa() { ZTID = sb.ZTID, SiD = sb.SiD, Pa = strpa.Trim(), Sort = (SByte)i });
                }
                if (pa.Count > 0)
                {
                    if (!noindex.ContainsKey("主申请人"))
                    {
                        st.FPa = pa[0].Pa;
                    }
                    if (!noindex.ContainsKey("申请人数量"))
                    {
                        st.PaSum = (SByte)pa.Count;
                    }
                }
                if (pa.Count > 1)
                {
                    if (!noindex.ContainsKey("是否合作申请"))
                    {
                        st.IsHeZUO = 1;
                    }
                }
            }
            if (sb.IV != null)
            {
                //发明人
                string[] ins = sb.IV.Split("、;；".ToArray());
                i = 0;
                foreach (var strin in ins)
                {
                    if (strin.Trim() == "") continue;
                    i++;
                    iv.Add(new STIV() { ZTID = sb.ZTID, SiD = sb.SiD, IV = strin.Trim(), Sort = (SByte)i });
                }
                if (iv.Count > 0)
                {
                    if (!noindex.ContainsKey("主发明人"))
                    {
                        st.FIn = iv[0].IV;
                    }
                    if (!noindex.ContainsKey("发明人数量"))
                    {
                        st.InSum = (SByte)iv.Count;
                    }
                }
            }
            if (!string.IsNullOrEmpty(sb.PR))
            {
                //优先权
                string[] aryprs = sb.PR.Split(";".ToArray());
                i = 0;
                foreach (var strpr in aryprs)
                {
                    string tmpstrpr = strpr.Trim();
                    if (tmpstrpr == "") continue;
                    i++;
                    string[] items = tmpstrpr.Split(' ');
                    if (items.Length == 2)
                    {

                        string prno = items[0];
                        string prdt = items[1];
                        gj = items[0].Substring(0, 2).ToUpper();
                        if (gj == "WO")
                        {
                            Match mh = regwogj.Match(prno.Substring(2));
                            if (mh.Success)
                            {
                                gj = mh.Groups["gj"].Value;
                                if (!zhouguo.ContainsKey(gj))
                                {
                                    gj = "WO";
                                }
                            }
                        }
                        if (zhouguo.ContainsKey(gj))
                        {
                            CfGCountry cfgc = zhouguo[gj];
                            gj = cfgc.GJ;
                        }

                        STPR tmpr = new STPR() { ZTID = sb.ZTID, SiD = sb.SiD };

                        tmpr.An = prno;
                        tmpr.Ad = prdt;
                        tmpr.GJ = gj;
                        pr.Add(tmpr);
                    }
                }
            }
            if (sb.OpD != null)
            {
                if (sb.OpD.FormatDate().GetYear() != 1800)
                {
                    if (!noindex.ContainsKey("最早优先权日"))
                    {
                        st.OpD = sb.OpD.FormatDate();
                    }
                    if (!noindex.ContainsKey("最早优先权年"))
                    {
                        st.OpDy = st.OpD.ToString().GetYear();
                    }
                }
            }
            if (pr.Count > 0)
            {
                if (!noindex.ContainsKey("最早优先权国"))
                {
                    st.OprC = pr[0].GJ;
                }
            }



            return st;

        }
    }
}

