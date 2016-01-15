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
    public class EPODOCIndex
    {
        public static Dictionary<string, CfGCountry> zhouguo = new Dictionary<string, CfGCountry>();
        static EPODOCIndex()
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

        public static STDT AutoIndex(ShowBase sb, out List<STPa> pa, out List<STIV> iv, out List<STIpc> ic, out List<STPR> pr, out List<STFML> fml, out List<STCPc> cpc)
        {
            STDT st = new STDT() { ZTID = sb.ZTID, SiD = sb.SiD, ImportDate = sb.ImportDate };
            pa = new List<STPa>();
            iv = new List<STIV>();
            ic = new List<STIpc>();
            pr = new List<STPR>();
            fml = new List<STFML>();
            cpc = new List<STCPc>();
            int i = 0;
            //申请号
            if (!string.IsNullOrEmpty(sb.An))
            {
                string[] ans = sb.An.Trim().Split(' ');
                switch (ans.Length)
                {
                    case 2:
                        st.An = ans[0];
                        if (ans[1].FormatDate().GetYear() != 1800)
                        {
                            st.Ad = ans[1].FormatDate();
                            st.AdY = st.Ad.GetYear();
                        }
                        break;
                    case 1:
                        st.An = ans[0];
                        break;
                }
            }
            if (!string.IsNullOrEmpty(sb.PN))
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
                            st.PDy = st.PD.GetYear();
                        }
                        break;
                    case 2:
                        st.PN = pns[0];
                        if (pns[1].FormatDate().GetYear() != 1800)
                        {
                            st.PD = pns[1].FormatDate();
                            st.PDy = st.PD.GetYear();
                        }
                        break;
                    case 1:
                        st.PN = pns[0];
                        break;
                }
            }
            if (!string.IsNullOrEmpty(st.An))
            {
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
                    st.GJ = zhouguo[gj].GJ;
                    st.ZHoU = zhouguo[gj].ZHoU;
                }
                else
                {
                    st.GJ = "未知";
                    st.ZHoU = "未知";
                }
            }
            if (sb.FaMN != null)
            {
                fml.Add(new STFML() { ZTID = sb.ZTID, SiD = sb.SiD, FMLid = Convert.ToInt32(sb.FaMN) });
            }
            if (!string.IsNullOrEmpty(sb.Ipc))
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
                    st.FIpc = ic[0].Ipc;
                    st.IpcSum = (SByte)ic.Count;
                }
            }
            if (!string.IsNullOrEmpty(sb.CPc))
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
                    st.FCPc = cpc[0].CPc;
                    st.CPcSum = (SByte)ic.Count;
                }
            }

            if (!string.IsNullOrEmpty(sb.Pa))
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
                    st.FPa = pa[0].Pa;
                    st.PaSum = (SByte)pa.Count;
                }
                if (pa.Count > 1)
                {
                    st.IsHeZUO = 1;
                }
            }
            if (!string.IsNullOrEmpty(sb.IV))
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
                    st.FIn = iv[0].IV;
                    st.InSum = (SByte)iv.Count;
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
                        string gj = items[0].Substring(0, 2).ToUpper();
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
            if (!string.IsNullOrEmpty(sb.OpD))
            {
                if (sb.OpD.FormatDate().GetYear() != 1800)
                {
                    st.OpD = sb.OpD.FormatDate();
                    st.OpDy = st.OpD.ToString().GetYear();
                }
            }
            if (pr.Count > 0)
            {
                st.OprC = pr[0].GJ;
            }



            return st;

        }
    }
}

