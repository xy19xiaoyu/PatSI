using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IData;
using System.Text.RegularExpressions;
using System.Threading;
using DAL;
using BLL.Template;
using BLL.Index;
using xyExtensions;
using AddressExtensions;


namespace BLL.Import
{
    public class CPRSImport : IDataImportAdapter
    {
        private List<string> otherstring = new List<string>();

        private Regex regRowSplit = new Regex("-*第\\s*\\d{1,20}篇/共\\s*\\d{1,20}篇-*");
        private Regex regcharRNT = new Regex("(\\r|\\n)*");
        private Regex regspace = new Regex("\\s{4,}");
        private Regex rowRege = new Regex("【(?<colname>[^】]*)】(?<colval>[^【]*)");

        public override event ValueChangedEventHandler ShowProcess;
        public override event SetMaxValueEventHander SetMaxProcess;

        public CPRSImport(string filename, string ztname, int zid)
            : base(filename, ztname, zid)
        {
            otherstring.Add(Encoding.GetEncoding("utf-8").GetString(new byte[] { 0xEF, 0xBF, 0xBD, 0xEF, 0xBF, 0xBD }));
            Encodeing = Encoding.GetEncoding("gb2312");
            Sr = new StreamReader(new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read), Encodeing);
        }

        public override bool Import()
        {
            string s = Sr.ReadToEnd();

            TemplateHelper tmph = new TemplateHelper("");
            string[] arys = regRowSplit.Split(s);
            List<DAL.ShowBase> sbs = new List<DAL.ShowBase>();
            List<DAL.STPa> pas = new List<DAL.STPa>();
            List<DAL.STIV> ivs = new List<DAL.STIV>();
            List<DAL.STIpc> ipcs = new List<DAL.STIpc>();
            List<DAL.STPR> prs = new List<DAL.STPR>();
            List<DAL.STDT> dts = new List<DAL.STDT>();
            List<DAL.STQY> qys = new List<DAL.STQY>();

            int count = arys.Length - 1;
            SetMaxProcess(this, count);
            Thimport = new Thread(() =>
            {
                //第一行为空
                for (int i = 1; i <= arys.Length - 1; i++)
                {
                    string tmp = regspace.Replace(regcharRNT.Replace(arys[i].Trim(), ""), "");
                    foreach (string x in otherstring)
                    {
                        tmp = tmp.Replace(x, "");
                    }
                    tmp = RemoveZaoZi(tmp);
                    MatchCollection mhs = rowRege.Matches(tmp);
                    ShowBase sb = new ShowBase();
                    sb.ZTID = ZTId;
                    sb.ImportDate = DateTime.Now;

                    foreach (Match m in mhs)
                    {
                        string colname = m.Groups["colname"].Value;
                        string val = m.Groups["colval"].Value.Trim().Trim('\t');
                        string mappingcolname = tmph.getMappingColumnName(colname);
                        switch (mappingcolname)
                        {
                            case "申请号":
                                sb.An = val;
                                sb.SiD = val;
                                break;
                            case "发明名称":
                                sb.Title = val;
                                break;
                            case "摘要":
                                sb.ABs = val;
                                break;
                            case "权利要求":
                                sb.ClM = val;
                                break;
                            case "国际分类号":
                                sb.Ipc = val;
                                break;
                            case "范畴分类号":
                                sb.FCfL = val;
                                break;
                            case "国家/省市":
                                sb.ShEng = val;
                                break;
                            case "申请人":
                                sb.Pa = val;
                                break;
                            case "联系地址":
                                sb.AddR = val;
                                break;
                            case "邮编":
                                sb.Zip = val;
                                break;
                            case "代理机构":
                                sb.DLJG = val;
                                break;
                            case "代理人":
                                sb.DLr = val;
                                break;
                            case "代理机构地址":
                                sb.DLJGAddR = val;
                                break;
                            case "发明人":
                                sb.IV = val;
                                break;
                            case "申请日":
                                sb.Ad = val;
                                break;
                            case "公开号":
                                sb.PN = val;
                                break;
                            case "公开日":
                                sb.PD = val;
                                break;
                            //case "授权公告日":
                            //    sb.Gd = val;
                            //    break;
                            case "公告日":
                                sb.Gd = val;
                                break;
                            //case "授权日":
                            //    sb.Gd = val;
                            //    break;
                            case "公告号":
                                sb.GN = val;
                                break;
                            case "优先权":
                                sb.PR = val;
                                break;
                            case "审批历史":
                                sb.LG = val;
                                break;
                            case "附图数":
                                sb.PiCSum = val;
                                break;
                            case "页数":
                                sb.DesPageSum = val;
                                break;
                            case "权利要求项数":
                                sb.ClMSum = val;
                                break;
                        }
                    }
                    if (CheckExist(sb.SiD))
                    {
                        if (Skip)
                        {

                            Skip_sum++;
                            continue;
                        }
                        else
                        {
                            DelExist(sb.ZTID.ToString(), sb.SiD.ToString());
                            Overwrite_sum++;
                        }

                    }
                    else
                    {
                        Sids.Add(sb.SiD, true);
                    }
                    sbs.Add(sb);

                    List<STPa> pa = new List<STPa>();
                    List<STIV> iv = new List<STIV>();
                    List<STIpc> ipc = new List<STIpc>();
                    List<STPR> pr = new List<STPR>();
                    List<STQY> qy = new List<STQY>();
                    dts.Add(CPRSIndex.AutoIndex(sb, out pa, out iv, out ipc, out pr, out qy));
                    pas.AddRange(pa);
                    ivs.AddRange(iv);
                    ipcs.AddRange(ipc);
                    prs.AddRange(pr);
                    qys.AddRange(qy);

                    if (i % 100 == 0)
                    {

                        //Thread.Sleep(1000);
                        //todo:标引
                        //todo:标引信息入库

                        //todo:基本信息入库
                        BulkInsert(sbs, pas, ivs, ipcs, prs, dts, qys);
                        sbs.Clear();
                        pas.Clear();
                        ivs.Clear();
                        ipcs.Clear();
                        prs.Clear();
                        dts.Clear();
                        qys.Clear();
                        ShowProcess(this, i, Skip_sum, Overwrite_sum, "导入");
                    }

                }
                if (sbs.Count > 0)
                {
                    BulkInsert(sbs, pas, ivs, ipcs, prs, dts, qys);
                    sbs.Clear();
                    pas.Clear();
                    ivs.Clear();
                    ipcs.Clear();
                    prs.Clear();
                    dts.Clear();
                    qys.Clear();
                }

                log.Info(string.Format("导入完毕： 专题库：{0},ID：{1},类型：,{2}总量：{3},跳过：{4}", ztName, ZTId, Type, count, Skip_sum));
                ShowProcess(this, count, Skip_sum, Overwrite_sum, "导入完毕");

                Sr.Close();
                Sr.Dispose();


            });
            Thimport.Start();
            return true;
        }
        private bool BulkInsert(List<ShowBase> sbs, List<STPa> pa, List<STIV> iv, List<STIpc> ic, List<STPR> pr, List<STDT> dt, List<STQY> qy)
        {


            using (MySqlDataContext db = new MySqlDataContext())
            {

                db.ShowBase.BulkInsertPageSize = 100;
                db.STDT.BulkInsertPageSize = 100;
                db.STPa.BulkInsertPageSize = 100;
                db.STIV.BulkInsertPageSize = 100;
                db.STPR.BulkInsertPageSize = 100;
                db.STIpc.BulkInsertPageSize = 100;
                db.STQY.BulkInsertPageSize = 100;

                db.ShowBase.BulkInsert(sbs);
                db.STDT.BulkInsert(dt);
                db.STPa.BulkInsert(pa);
                db.STIV.BulkInsert(iv);
                db.STPR.BulkInsert(pr);
                db.STIpc.BulkInsert(ic);
                db.STQY.BulkInsert(qy);
                db.SubmitChanges();
            }
            return true;
        }
        private bool BulkInsert(List<ShowBase> sbs)
        {
            MySqlDataContext db = new MySqlDataContext();
            db.ShowBase.BulkInsertPageSize = 100;
            db.ShowBase.BulkInsert(sbs);
            db.SubmitChanges();
            sbs.Clear();
            return true;
        }

        private static string RemoveZaoZi(string s)
        {
            char sp = (char)32;
            char[] ss = s.ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                if (ss[i] > 40891 && ss[i] < 59243)
                {
                    ss[i] = sp;
                }
            }
            return new string(ss);
        }
    }
}

