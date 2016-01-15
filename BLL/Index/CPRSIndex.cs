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
    public class CPRSIndex
    {
        public static Dictionary<string, CfGCountry> zhouguo = new Dictionary<string, CfGCountry>();
        public static Dictionary<string, string> quyu = new Dictionary<string, string>();
        static CPRSIndex()
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

        public static STDT AutoIndex(ShowBase sb, out List<STPa> pa, out List<STIV> iv, out List<STIpc> ic, out List<STPR> pr, out List<STQY> qy)
        {
            STDT st = new STDT() { ZTID = sb.ZTID, SiD = sb.SiD, ImportDate = sb.ImportDate };
            pa = new List<STPa>();
            iv = new List<STIV>();
            ic = new List<STIpc>();
            pr = new List<STPR>();
            qy = new List<STQY>();
            //申请号
            if (sb.An != null)
            {
                st.An = sb.An;
                if (sb.Ad.FormatDate().GetYear() != 1800)
                {
                    st.Ad = sb.Ad.FormatDate();
                    st.AdY = Convert.ToInt32(st.Ad.GetYear());
                }
            }
            if (sb.PN != null)
            {
                //公开
                st.PN = sb.PN;
                if (sb.PD.FormatDate().GetYear() != 1800)
                {
                    st.PD = sb.PD.FormatDate();
                    st.PDy = st.PD.GetYear();
                    if (st.PD != null && st.Ad != null)
                    {
                        st.PDyDef = IDataImportAdapter.GetYearDeff(st.PD, st.Ad);
                    }
                }
            }
            if (sb.GN != null)
            {
                //公告
                st.GN = sb.GN;
                if (sb.Gd.FormatDate().GetYear() != 1800)
                {
                    st.Gd = sb.Gd.FormatDate();
                    st.GdY = sb.Gd.GetYear();
                    if (st.Gd != null && st.Ad != null)
                    {
                        st.GdYDef = IDataImportAdapter.GetYearDeff(st.Gd, st.Ad);
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

            //页数字数
            st.DesPageSum = sb.DesPageSum.ToSbyte();
            st.PiCSum = sb.PiCSum.ToSbyte();
            st.ClMPageSum = sb.ClMSum.ToSbyte();
            if (sb.ClM != null)
            {
                st.ClSCharSum = sb.ClM.Length;
            }



            //专利类型
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

            st.Type = type;
            st.Type1 = type1;

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
                        st.LGYear = Convert.ToInt32(mh.Groups["lgdata"].Value.ToString().Left(4));
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
                st.LG = lg;

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
                st.Age = (sbyte)age;
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
                    st.FPa = pa[0].Pa;
                    st.FPaType = pa[0].PaType;
                    st.PaSum = (SByte)pa.Count;
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
                    st.FIn = iv[0].IV;
                    st.InSum = (SByte)iv.Count;
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
                    st.FIpc = ic[0].Ipc;
                    st.IpcSum = (SByte)ic.Count;
                }
            }
            #endregion
            #region 洲国省市区县-经济区域

            if (!string.IsNullOrEmpty(sb.AddR))
            {
                string[] diqu = sb.AddR.AddressToShengShiQuXianAddress();
                st.ShEng1 = diqu[1];
                st.ShI = diqu[2];
                st.QUXian = diqu[3];
            }
            else
            {
                st.ShEng1 = "未知";
                st.ShI = "未知";
                st.QUXian = "未知";


            }
            int tmpindex = sb.ShEng.IndexOf("(");
            string daima = "";
            if (tmpindex >= 0)
            {
                st.ShEng = sb.ShEng.Substring(0, tmpindex);
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
                st.ShEng1 = cfgc.ShEng1;
                st.GJ = cfgc.GJ;
                st.ZHoU = cfgc.ZHoU;
            }
            else
            {
                st.ShEng1 = "未知";
                st.GJ = "未知";
                st.ZHoU = "未知";
            }
            if (st.GJ != "中国")
            {
                st.IsGuOwAi = 1;
            }
            if (string.IsNullOrEmpty(st.GJ))
            {
                st.GJ = "未知";
            }
            if (string.IsNullOrEmpty(st.ZHoU))
            {
                st.ZHoU = "未知";
            }
            if (string.IsNullOrEmpty(st.ShEng))
            {
                st.ShEng = "未知";
            }
            if (string.IsNullOrEmpty(st.ShEng1))
            {
                st.ShEng1 = "未知";
            }
            if (string.IsNullOrEmpty(st.ShI))
            {
                st.ShI = "未知";
            }
            if (string.IsNullOrEmpty(st.QUXian))
            {
                st.QUXian = "未知";
            }

            List<string> Listquyu = new List<string>();
            if (quyu.ContainsKey(st.ZHoU))
            {
                Listquyu.Add(quyu[st.ZHoU]);
            }
            if (quyu.ContainsKey(st.GJ))
            {
                Listquyu.Add(quyu[st.GJ]);
            }
            if (quyu.ContainsKey(st.ShEng))
            {
                Listquyu.Add(quyu[st.ShEng]);
            }
            if (quyu.ContainsKey(st.ShEng1))
            {
                Listquyu.Add(quyu[st.ShEng1]);
            }
            if (quyu.ContainsKey(st.ShI))
            {
                Listquyu.Add(quyu[st.ShI]);
            }
            if (quyu.ContainsKey(st.QUXian))
            {
                Listquyu.Add(quyu[st.QUXian]);
            }
            Listquyu = Listquyu.Distinct().ToList<string>();
            foreach (var s in Listquyu)
            {
                qy.Add(new STQY() { ZTID = sb.ZTID, SiD = sb.SiD, QY = s });
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

