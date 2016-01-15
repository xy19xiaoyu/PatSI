using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using xyExtensions;
using AddressExtensions;
using System.Text.RegularExpressions;
using BLL.Import;

namespace BLL.Index
{
    public class XLSCPRSIndex
    {
        public static Dictionary<string, CfGCountry> zhouguo = new Dictionary<string, CfGCountry>();
        public static Dictionary<string, string> quyu = new Dictionary<string, string>();
        static XLSCPRSIndex()
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
            if (quyu.Count == 0)
            {
                MySqlDataContext db = new MySqlDataContext();
                foreach (var x in db.CfGQUYU)
                {
                    if (quyu.ContainsKey(x.DiZHi)) continue;
                    quyu.Add(x.DiZHi, x.QUYU);
                }
            }
        }
        public static Regex regPr = new Regex("\\d{4}年\\d{1,2}月\\d{1,2}日");
        public static Regex regYear = new Regex("(?<lgdata>\\d{4}年\\d{1,2}月\\d{1,2}日)");
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
        public static STDT AutoIndex(ShowBase sb, STDT st, out List<STPa> pa, out List<STIV> iv, out List<STIpc> ic, out List<STPR> pr, out List<STQY> qy)
        {

            pa = new List<STPa>();
            iv = new List<STIV>();
            ic = new List<STIpc>();
            pr = new List<STPR>();
            qy = new List<STQY>();
            st.SiD = sb.SiD;
            //申请号
            if (sb.An != null)
            {
                st.An = sb.An;
                if (sb.Ad.FormatDate().GetYear() != 1800)
                {
                    st.Ad = sb.Ad.FormatDate();
                    if (!noindex.ContainsKey("申请年"))
                    {
                        st.AdY = Convert.ToInt32(st.Ad.GetYear());
                    }

                }
            }
            if (!string.IsNullOrEmpty(sb.PN))
            {
                //公开
                st.PN = sb.PN;
                if (sb.PD.FormatDate().GetYear() != 1800)
                {
                    st.PD = sb.PD.FormatDate();
                    if (!noindex.ContainsKey("公开年"))
                    {
                        st.PDy = st.PD.GetYear();
                    }
                    if (!noindex.ContainsKey("公开年差"))
                    {
                        if (st.PD != null && st.Ad != null)
                        {
                            st.PDyDef = IDataImportAdapter.GetYearDeff(st.PD, st.Ad);
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(sb.GN))
            {
                //公告
                st.GN = sb.GN;
                if (sb.Gd.FormatDate().GetYear() != 1800)
                {
                    st.Gd = sb.Gd.FormatDate();
                    if (!noindex.ContainsKey("授权年"))
                    {
                        st.GdY = sb.Gd.GetYear();
                    }
                    if (!noindex.ContainsKey("授权年差"))
                    {
                        if (st.Gd != null && st.Ad != null)
                        {
                            st.GdYDef = IDataImportAdapter.GetYearDeff(st.Gd, st.Ad);
                        }
                    }
                }
            }
            //PCT
            st.PcTIn = sb.PcDIn.FormatDate();
            st.PcTAn = sb.PcTAn;
            st.PcTAd = sb.PcTAd.FormatDate();
            st.PcTPN = sb.PcTPN;
            st.PcTPD = sb.PcTPN.FormatDate();
            //母案
            st.MAd = sb.MAd;

            st.DesPageSum = sb.DesPageSum.ToSbyte();
            st.PiCSum = sb.PiCSum.ToSbyte();
            st.ClMPageSum = sb.ClMSum.ToSbyte();

            //页数字数
            if (!noindex.ContainsKey("权利要求1字数"))
            {
                if (!string.IsNullOrEmpty(sb.ClM))
                {
                    st.ClSCharSum = sb.ClM.Length;
                }
            }
            //专利类型
            //专利类型(含PCT)

            string type = "";
            string type1 = "";
            char ctype = '1';
            switch (st.An.Length)
            {
                case 8:
                case 9:
                    ctype = st.An[2];
                    break;
                case 12:
                case 13:
                    ctype = st.An[4];
                    break;

            }
            switch (ctype)
            {
                case '1':
                    type = "发明专利";
                    type1 = "发明专利";
                    break;
                case '2':
                    type = "实用新型";
                    type1 = "实用新型";
                    break;
                case '3':
                    type = "外观专利";
                    type1 = "外观专利";
                    break;
                case '8':
                    type = "发明专利";
                    type1 = "发明专利PCT";
                    break;
                case '9':
                    type = "实用新型";
                    type1 = "实用新型PCT";
                    break;
            }
            if (!noindex.ContainsKey("专利类型"))
            {
                st.Type = type;
            }
            if (!noindex.ContainsKey("专利类型(含PCT)"))
            {
                st.Type1 = type1;
            }

            if (type != "发明专利")
            {
                st.GN = st.PN;
                st.Gd = st.PD;
                st.GdY = st.PDy;
                st.GdYDef = st.GdYDef;
            }

            #region 法律状态
            //因费用终止公告日
            string lg = "";
            if (sb.LG != null)
            {
                if (sb.LG != "")
                {
                    Match mh = regYear.Match(sb.LG);
                    if (mh.Success)
                    {
                        if (!noindex.ContainsKey("法律截止年"))
                        {
                            st.LGYear = Convert.ToInt32(mh.Groups["lgdata"].Value.ToString().Left(4));
                        }
                    }
                    if (sb.Gd != "")
                    {
                        lg = "授权失效";
                    }
                    else
                    {
                        lg = "无效";
                    }
                }
                else
                {
                    if (type == "实用新型" || type == "外观专利")
                    {
                        lg = "授权有效";
                    }
                    else
                    {
                        if (sb.Gd != "")
                        {
                            lg = "授权有效";
                        }
                        else
                        {
                            lg = "在审";
                        }
                    }

                }
                if (!noindex.ContainsKey("最终法律状态"))
                {
                    st.LG = lg;
                }

                int age = 0;
                switch (lg)
                {
                    case "在审":
                        st.IsGongZHi = 0;
                        break;
                    case "授权有效":
                        if (st.AdY.HasValue)
                        {
                            age = DateTime.Now.Year - st.AdY.Value;
                            if (age == 0) age = 1;
                        }
                        st.IsGongZHi = 0;
                        break;
                    case "无效":
                        st.IsGongZHi = 1;
                        break;
                    case "授权失效":
                        if (st.LGYear.HasValue)
                        {
                            age = st.LGYear.Value - st.AdY.Value;
                            if (age == 0) age = 1;
                        }
                        else
                        {
                            Console.WriteLine("x");
                        }
                        st.IsGongZHi = 1;
                        break;
                }
                if (!noindex.ContainsKey("专利年龄"))
                {
                    st.Age = (sbyte)age;
                }
            }
            //if (age == 0) age = 1;

            if (st.Gd != null && st.PD != null)
            {
                st.SCzQ = IDataImportAdapter.GetYearDeff(st.Gd.ToString(), st.PD.ToString());
            }

            #endregion
            int i = 0;
            #region 申请人
            if (sb.Pa != null)
            {
                string[] pas = sb.Pa.Split("、;；".ToArray());
                int tmpsum = 0;
                foreach (var strpa in pas)
                {
                    if (strpa.Trim() == "") continue;
                    i++;
                    STPa tmppa = new STPa() { ZTID = sb.ZTID, SiD = sb.SiD, Pa = strpa, PaType = strpa.GetPaType(), Sort = (SByte)i };
                    if (tmppa.PaType != "个人")
                    {
                        tmpsum++;
                    }
                    pa.Add(tmppa);
                }
                if (pa.Count > 0)
                {
                    if (!noindex.ContainsKey("主申请人"))
                    {
                        st.FPa = pa[0].Pa;
                    }
                    if (!noindex.ContainsKey("主申请人类型"))
                    {
                        st.FPaType = pa[0].PaType;
                    }
                    if (!noindex.ContainsKey("申请人数量"))
                    {
                        st.PaSum = (SByte)pa.Count;
                    }
                }
                if (tmpsum > 1)
                {
                    st.IsHeZUO = (SByte)1;
                }
            }
            #endregion
            #region 发明人
            if (sb.IV != null)
            {
                string[] ins = sb.IV.Split("、;；".ToArray());
                i = 0;
                foreach (var strin in ins)
                {
                    if (strin.Trim() == "") continue;
                    i++;
                    iv.Add(new STIV() { ZTID = sb.ZTID, SiD = sb.SiD, IV = strin, Sort = (SByte)i });
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
            #endregion
            #region IPC
            if (sb.Ipc != null)
            {
                //IPC
                string[] ipcs = sb.Ipc.Split("、;；".ToArray());
                i = 0;
                foreach (var ipc in ipcs)
                {
                    if (ipc.Trim() == "") continue;
                    string atripc = ipc.Replace("  ", " ").FormatIPC();
                    STIpc tmpipc = new STIpc() { ZTID = sb.ZTID, SiD = sb.SiD, Ipc = atripc };
                    if (type == "外观专利")
                    {
                        tmpipc.Ipc3 = atripc.Left(2);
                        tmpipc.Ipc4 = atripc.Left(5);
                        tmpipc.Ipc7 = atripc.Left(7);
                    }
                    else
                    {

                        tmpipc.Ipc1 = atripc.Left(1);
                        tmpipc.Ipc3 = atripc.Left(3);
                        tmpipc.Ipc4 = atripc.Left(4);
                        tmpipc.Ipc7 = atripc.Left(7);
                    }
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
            #endregion
            #region 洲国省市区县-经济区域

            string sheng1 = "未知";
            string sheng = "未知";
            string shi = "未知";
            string quxian = "未知";
            string gj = "未知";
            string zhou = "未知";
            if (!string.IsNullOrEmpty(sb.AddR))
            {
                string[] diqu = sb.AddR.AddressToShengShiQuXianAddress();
                sheng1 = diqu[1];
                shi = diqu[2];
                quxian = diqu[3];
            }
            int tmpindex = sb.ShEng.IndexOf("(");
            string daima = "";
            if (tmpindex >= 0)
            {
                sheng = sb.ShEng.Substring(0, tmpindex);
                try
                {
                    daima = sb.ShEng.Substring(tmpindex + 1, 2);
                }
                catch (Exception)
                {
                    daima = "";
                }
            }
            if (zhouguo.ContainsKey(daima))
            {
                CfGCountry cfgc = zhouguo[daima];
                sheng1 = cfgc.ShEng1;
                gj = cfgc.GJ;
                zhou = cfgc.ZHoU;
            }
            if (gj != "中国")
            {
                st.IsGuOwAi = 1;
            }


            List<string> Listquyu = new List<string>();
            if (quyu.ContainsKey(zhou))
            {
                Listquyu.Add(quyu[zhou]);
            }
            if (quyu.ContainsKey(gj))
            {
                Listquyu.Add(quyu[gj]);
            }
            if (quyu.ContainsKey(sheng))
            {
                Listquyu.Add(quyu[sheng]);
            }
            if (quyu.ContainsKey(sheng1))
            {
                Listquyu.Add(quyu[sheng1]);
            }
            if (quyu.ContainsKey(shi))
            {
                Listquyu.Add(quyu[shi]);
            }
            if (quyu.ContainsKey(quxian))
            {
                Listquyu.Add(quyu[quxian]);
            }
            Listquyu = Listquyu.Distinct().ToList<string>();
            foreach (var s in Listquyu)
            {
                qy.Add(new STQY() { ZTID = sb.ZTID, SiD = sb.SiD, QY = s });
            }

            if (!noindex.ContainsKey("省"))
            {
                st.ShEng = sheng;

            }
            if (!noindex.ContainsKey("省(计划单列市）"))
            {
                st.ShEng1 = sheng1;

            }
            if (!noindex.ContainsKey("市"))
            {
                st.ShI = shi;

            }
            if (!noindex.ContainsKey("区县"))
            {
                st.QUXian = quxian;

            }
            if (!noindex.ContainsKey("洲际"))
            {
                st.ZHoU = zhou;

            }
            if (!noindex.ContainsKey("国家"))
            {
                st.GJ = gj;
            }


            #endregion
            #region 优先权
            if (sb.PR != null)
            {
                string[] aryprs = sb.PR.Split("\t".ToArray());
                i = 0;
                int opd = 0;
                string oprc = "";
                foreach (var strpr in aryprs)
                {
                    string tmpstrpr = strpr.Trim();
                    if (tmpstrpr == "") continue;
                    i++;
                    string[] itmes = regPr.Split(tmpstrpr);
                    string prcy = itmes[0];

                    string prno = itmes[1];
                    string prdt = "";
                    int tmpopd = DateTime.Now.Year;
                    try
                    {
                        prdt = tmpstrpr.Substring(prcy.Length, tmpstrpr.Length - prcy.Length - prno.Length).FormatDate();
                        tmpopd = Convert.ToInt32(prdt);
                        if (i == 1)
                        {
                            opd = tmpopd;
                            oprc = prcy;
                        }
                        else
                        {
                            if (opd < tmpopd)
                            {
                                opd = tmpopd;
                                oprc = prcy;
                            }
                        }

                        STPR tmpr = new STPR() { ZTID = sb.ZTID, SiD = sb.SiD };
                        tmpr.An = prno;
                        tmpr.Ad = prdt;
                        tmpr.GJ = prcy;

                        pr.Add(tmpr);
                    }
                    catch (Exception ex)
                    {
                        //todonothing;
                        //tmpopd = st.AdY.Value;
                    }


                }
                if (pr.Count > 0)
                {
                    int year = opd.ToString().GetYear();
                    if (year != 1800)
                    {
                        st.OpD = opd.ToString();
                        st.OpDy = opd.ToString().GetYear();
                    }
                    st.OprC = oprc;

                }
            }
            #endregion
            return st;

        }


    }
}

