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
    public class XLSWPIIndex
    {
        public static Dictionary<string, CfGCountry> zhouguo = new Dictionary<string, CfGCountry>();
        static XLSWPIIndex()
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
        private static Regex regnoap = new Regex("^\\[[^\\]]*\\]$");
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
        public static STDT AutoIndex(ShowBase sb, STDT st, out List<STPa> pa, out List<STIV> iv, out List<STIpc> ic, out List<STPR> pr, out List<STPNS> pn, out List<STAnS> an, out List<STDc> dc)
        {
            st.ZTID = sb.ZTID;
            st.SiD = sb.SiD;
            pa = new List<STPa>();
            iv = new List<STIV>();
            ic = new List<STIpc>();
            pr = new List<STPR>();
            pn = new List<STPNS>();
            an = new List<STAnS>();
            dc = new List<STDc>();
            int i = 0;

            if (sb.An != null)
            {
                string[] ans = sb.An.Split(';');


                List<string> gjs = new List<string>();
                List<string> tmpans = new List<string>();
                string zhou = "";
                foreach (var stran in ans)
                {
                    if (stran.Trim() == "") continue;

                    i++;

                    string tmpstran = "";
                    string tmpstrad = "";
                    //如果是Cont of|Based on 是优先权或者公开号 不记录
                    if (regnoap.Match(stran.Trim()).Success) continue;
                    string[] arytmpstran = regspace.Replace(regap.Replace(stran.Trim(), "").Trim(), " ").Trim().Split(' ');
                    if (arytmpstran.Length == 2)
                    {
                        tmpstran = arytmpstran[0].Trim();
                        tmpstrad = arytmpstran[1].Trim();
                    }
                    else
                    {
                        tmpstran = arytmpstran[0].Trim();
                    }

                    if (tmpans.Contains(tmpstran)) continue;
                    tmpans.Add(tmpstran);
                    STAnS tmpan = new STAnS() { ZTID = sb.ZTID, SiD = sb.SiD };
                    tmpan.An = tmpstran;
                    if (tmpstrad.GetYear() != 1800)
                    {
                        tmpan.Ad = tmpstrad;
                        tmpan.AdY = tmpstrad.GetYear().ToString();
                    }
                    tmpan.Sort = (SByte)i;
                    string gj = tmpstran.Left(2).ToUpper();
                    if (gj == "WO")
                    {
                        Match mh = regwogj.Match(tmpstran.Substring(2));
                        if (mh.Success)
                        {
                            gj = mh.Groups["gj"].Value;
                            if (!zhouguo.ContainsKey(gj))
                            {
                                gj = "WO";
                            }
                        }
                    }
                    if (!gjs.Contains(gj))
                    {
                        gjs.Add(gj);
                    }
                    if (zhouguo.ContainsKey(gj))
                    {
                        tmpan.GJ = zhouguo[gj].GJ;
                        if (zhou == "")
                        {
                            zhou = zhouguo[gj].ZHoU;
                        }
                    }
                    else
                    {
                        tmpan.GJ = "未知";
                        zhou = "未知";
                    }
                    an.Add(tmpan);
                }
                if (an.Count > 0)
                {
                    st.An = an[0].An;
                    if (an[0].Ad.FormatDate().GetYear() != 1800)
                    {
                        st.Ad = an[0].Ad.FormatDate();
                        if (!noindex.ContainsKey("申请年"))
                        {
                            st.AdY = Convert.ToInt32(st.Ad.GetYear());
                        }
                    }
                    if (!noindex.ContainsKey("国家"))
                    {
                        st.GJ = an[0].GJ;
                    }
                    if (!noindex.ContainsKey("洲际"))
                    {
                        st.ZHoU = zhou;
                    }
                }
                if (gjs.Count > 3)
                {
                    if (gjs.Contains("US") && gjs.Contains("WO") && gjs.Contains("JP"))
                    {
                        if (!noindex.ContainsKey("是否三局"))
                        {
                            st.IsSanJU = 1;
                        }
                    }
                }
                if (gjs.Count > 5)
                {
                    if (gjs.Contains("US") && gjs.Contains("WO") && gjs.Contains("JP") && gjs.Contains("CN") && gjs.Contains("KR"))
                    {
                        if (!noindex.ContainsKey("是否五局"))
                        {
                            st.IsWuJU = 1;
                        }
                    }
                }
                string tmpgjs = "";
                foreach (var x in gjs)
                {
                    tmpgjs += x + ";";
                }
                st.ApGJS = tmpgjs.Trim(';');
                if (!noindex.ContainsKey("同族数量"))
                {
                    st.FMLSum = an.Count;
                }
                if (!noindex.ContainsKey("国家数量"))
                {
                    st.GJSum = (SByte)gjs.Count;
                }
            }
            if (sb.PN != null)
            {
                //公开  
                string[] pns = regPnsplit.Split(sb.PN);
                i = 0;
                List<string> pngjs = new List<string>();
                foreach (var strpn in pns)
                {
                    i++;
                    if (strpn.Trim() == "") continue;
                    string tmpns = strpn.Trim();
                    STPNS tmppn = new STPNS() { ZTID = sb.ZTID, SiD = sb.SiD };
                    string tmpstrpn = "";
                    string tmpstrpd = "";
                    string[] arytmpns = regspace.Replace(tmpns, " ").Split(' ');
                    switch (arytmpns.Length)
                    {
                        case 3:
                            tmpstrpn = arytmpns[0] + arytmpns[1];
                            tmpstrpd = arytmpns[2];
                            break;
                        case 2:
                            tmpstrpn = arytmpns[0];
                            tmpstrpd = arytmpns[1];
                            break;
                        case 1:
                            tmpstrpn = arytmpns[0];
                            break;
                    }
                    tmppn.PN = tmpstrpn;
                    if (tmpstrpd.FormatDate().GetYear() != 1800)
                    {
                        tmppn.PD = tmpstrpd.FormatDate();
                        tmppn.PDy = tmppn.PD.GetYear().ToString();
                    }
                    tmppn.Sort = (SByte)i;
                    string gj = tmppn.PN.Left(2);
                    if (!pngjs.Contains(gj))
                    {
                        pngjs.Add(gj);
                    }
                    if (zhouguo.ContainsKey(gj))
                    {
                        tmppn.GJ = zhouguo[gj].GJ;
                    }
                    else
                    {
                        tmppn.GJ = "未知";
                    }

                    pn.Add(tmppn);

                }
                if (pn.Count > 0)
                {
                    st.PN = pn[0].PN;
                    if (pn[0].PD.GetYear() != 1800)
                    {
                        st.PD = pn[0].PD;
                        if (!noindex.ContainsKey("公开年"))
                        {
                            st.PDy = pn[0].PD.GetYear();
                        }
                    }

                }
                string tmpgjs = "";
                foreach (var x in pngjs)
                {
                    tmpgjs += x + ";";
                }
                st.PNGJS = tmpgjs.Trim(';');

            }
            if (sb.Ipc != null)
            {

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

            if (sb.Pa != null)
            {
                //申请人
                string[] pas = sb.Pa.Split("(".ToArray());
                int tmpsum = 0;
                foreach (var strpa in pas)
                {
                    if (strpa.Trim() == "") continue;
                    i++;
                    string[] arystrpa = strpa.Split(')');
                    string tmppa = "";
                    string cpy = "";
                    if (arystrpa.Length == 2)
                    {
                        tmppa = arystrpa[1].Trim();
                        cpy = arystrpa[0].Trim();
                    }
                    else
                    {
                        tmppa = arystrpa[1].Trim();
                    }
                    STPa tmpobjpa = new STPa() { ZTID = sb.ZTID, SiD = sb.SiD, Pa = tmppa, CPY = cpy, Sort = (SByte)i };
                    if (!string.IsNullOrEmpty(cpy))
                    {
                        if (tmpobjpa.CPY.IndexOf("-I") >= 0)
                        {

                        }
                        else
                        {
                            tmpsum++;
                        }
                    }
                    pa.Add(tmpobjpa);

                }
                if (pa.Count > 0)
                {
                    if (!noindex.ContainsKey("主申请人"))
                    {
                        st.FPa = pa[0].Pa;
                    }
                    if (!noindex.ContainsKey("是否合作申请"))
                    {
                        st.PaSum = (SByte)pa.Count;
                    }
                }
                if (tmpsum > 1)
                {
                    if (!noindex.ContainsKey("申请年"))
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
            if (sb.Dc != null)
            {
                //德温特分类
                List<string> arydc = sb.Dc.Split(" ".ToArray()).ToList<string>();
                List<string> listdc = new List<string>();
                for (int j = 0; j < arydc.Count; j++)
                {
                    if (arydc[j].Length > 3)
                    {
                        for (int tmp = 0; tmp < arydc[j].Length; tmp += 3)
                        {
                            listdc.Add(arydc[j].Substring(tmp, 3));
                        }
                    }
                    else
                    {
                        listdc.Add(arydc[j]);
                    }
                }
                listdc = listdc.Distinct<string>().ToList<string>();
                i = 0;
                foreach (var strdc in listdc)
                {
                    if (strdc.Trim() == "") continue;
                    i++;
                    dc.Add(new STDc() { ZTID = sb.ZTID, SiD = sb.SiD, Dc = strdc, Sort = (SByte)i });
                }
                if (dc.Count > 0)
                {
                    if (!noindex.ContainsKey("DC数量"))
                    {
                        st.DMcSum = (SByte)dc.Count;
                    }
                    if (!noindex.ContainsKey("主DC"))
                    {
                        st.FDMc = dc[0].Dc;
                    }
                }

            }
            if (sb.PR != null)
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
                        STPR tmpr = new STPR() { ZTID = sb.ZTID, SiD = sb.SiD };
                        tmpr.An = prno;
                        tmpr.Ad = prdt;
                        if (zhouguo.ContainsKey(gj))
                        {
                            tmpr.GJ = zhouguo[gj].GJ;
                        }
                        else
                        {
                            tmpr.GJ = "未知";
                        }
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

