using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IData;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using DAL;
using BLL.Index;
using BLL.Template;

namespace BLL.Import
{
    public class WPIImport : IDataImportAdapter
    {
        private string kunkaojin = Encoding.GetEncoding("utf-8").GetString(new byte[] { 0xEF, 0xBF, 0xBD, 0xEF, 0xBF, 0xBD });
        private Regex regRowSplit = new Regex("\\d{1,10}/\\d{1,10} - \\(C\\) WPI / Thomson");
        private Regex MarkReplece = new Regex("\\s{1,2}(AB\\s{2}|AN\\s{2}|AP\\s{2}|CPY\\s|DC\\s{2}|DS\\s{2}|ICAI|ICCI|IN\\s{2}|OPD\\s|PA\\s{2}|PD\\s{2}|PN\\s{2}|PR\\s{2}|TI\\s{2}|MC\\s{2}|IW\\s{2})-");
        private Regex regcharRNT = new Regex("(\\r|\\n|#[/]?CMT#)*");
        private Regex regspace = new Regex("\\s{4,}");
        private Regex rowRege = new Regex("【(?<colname>[^】]*)】(?<colval>[^【]*)");

        public override event ValueChangedEventHandler ShowProcess;
        public override event SetMaxValueEventHander SetMaxProcess;

        public WPIImport(string filename, string ztname, int zid)
            : base(filename, ztname, zid)
        {
            this.Type = DataType.CPRS;
            Encodeing = Encoding.UTF8;
            Sr = new StreamReader(new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read), Encodeing);
        }

        public override bool Import()
        {
            string s = Sr.ReadToEnd().Replace(kunkaojin, "");
            TemplateHelper tmph = new TemplateHelper("");
            string[] arys = regRowSplit.Split(s);
            List<ShowBase> sbs = new List<ShowBase>();
            List<STPa> pas = new List<STPa>();
            List<STIV> ivs = new List<STIV>();
            List<STIpc> ipcs = new List<STIpc>();
            List<STPR> prs = new List<STPR>();
            List<STDT> dts = new List<STDT>();
            List<STDc> dcs = new List<STDc>();
            List<STPNS> pns = new List<STPNS>();
            List<STAnS> ans = new List<STAnS>();
            int count = arys.Length - 1;
            SetMaxProcess(this, count);
            Thimport = new Thread(() =>
            {
                //第一行为空
                for (int i = 1; i <= count; i++)
                {
                    string tmp = regspace.Replace(regcharRNT.Replace(arys[i], ""), " ");
                    tmp = MarkReplece.Replace(tmp, "【$1】").Trim();
                    MatchCollection mhs = rowRege.Matches(tmp);
                    ShowBase sb = new ShowBase();
                    sb.ZTID = ZTId;
                    sb.ImportDate = DateTime.Now;

                    foreach (Match m in mhs)
                    {
                        string colname = m.Groups["colname"].Value.Trim();
                        string val = m.Groups["colval"].Value.Trim().Trim('\t');
                        string mappingcolname = tmph.getMappingColumnName(colname);
                        switch (mappingcolname)
                        {
                            case "AN":
                                sb.SiD = val;
                                break;
                            case "AP":
                                sb.An = val;
                                break;
                            case "TI":
                                sb.Title = val;
                                break;
                            case "ICAI":
                            case "ICCI":
                                if (sb.Ipc == null)
                                {
                                    sb.Ipc = val;
                                }
                                else
                                {
                                    sb.Ipc += ";" + val;
                                }

                                break;
                            case "PA":
                                sb.Pa = val;
                                break;
                            case "CPY":
                                sb.CPY = val;
                                break;
                            case "OPD":
                                sb.OpD = val;
                                break;
                            case "AB":
                                sb.ABs = val;
                                break;
                            case "IN":
                                sb.IV = val;
                                break;
                            case "PN":
                                sb.PN = val;
                                break;
                            case "PR":
                                sb.PR = val;
                                break;
                            case "PD":
                                sb.PD = val;
                                break;
                            case "DC":
                                sb.Dc = val;
                                break;
                            case "DMC":
                                sb.DMc = val;
                                break;
                            case "DS":
                                break;
                            case "YZ":
                                break;
                            case "BY":
                                break;
                            case "CPC":
                                sb.CPc = val;
                                break;
                            case "":
                                break;
                            default:
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
                    List<STAnS> an = new List<STAnS>();
                    List<STPNS> pn = new List<STPNS>();
                    List<STDc> dc = new List<STDc>();
                    dts.Add(WPIIndex.AutoIndex(sb, out pa, out iv, out ipc, out pr, out pn, out an, out dc));
                    pas.AddRange(pa);
                    ivs.AddRange(iv);
                    ipcs.AddRange(ipc);
                    prs.AddRange(pr);
                    pns.AddRange(pn);
                    ans.AddRange(an);
                    dcs.AddRange(dc);
                    if (i % 100 == 0)
                    {

                        //Thread.Sleep(1000);
                        //todo:标引
                        //todo:标引信息入库

                        //todo:基本信息入库
                        //BulkInsert(sbs);
                        BulkInsert(sbs, pas, ivs, ipcs, prs, dts, pns, ans, dcs);
                        sbs.Clear();
                        pas.Clear();
                        ivs.Clear();
                        ipcs.Clear();
                        prs.Clear();
                        dts.Clear();
                        pns.Clear();
                        ans.Clear();
                        dcs.Clear();
                        ShowProcess(this, i, Skip_sum, Overwrite_sum, "导入");

                    }

                }
                if (sbs.Count > 0)
                {
                    BulkInsert(sbs, pas, ivs, ipcs, prs, dts, pns, ans, dcs);
                    sbs.Clear();
                    pas.Clear();
                    ivs.Clear();
                    ipcs.Clear();
                    prs.Clear();
                    dts.Clear();
                    pns.Clear();
                    ans.Clear();
                    dcs.Clear();
                }


                ShowProcess(this, count, Skip_sum, Overwrite_sum, "导入完毕");

                Sr.Close();
                Sr.Dispose();

            });
            Thimport.Start();
            return true;
        }
        private bool BulkInsert(List<ShowBase> sbs, List<STPa> pa, List<STIV> iv, List<STIpc> ic, List<STPR> pr, List<STDT> dt, List<STPNS> pn, List<STAnS> an, List<STDc> dc)
        {


            using (MySqlDataContext db = new MySqlDataContext())
            {

                db.ShowBase.BulkInsertPageSize = 100;
                db.STDT.BulkInsertPageSize = 100;
                db.STPa.BulkInsertPageSize = 100;
                db.STIV.BulkInsertPageSize = 100;
                db.STPR.BulkInsertPageSize = 100;
                db.STIpc.BulkInsertPageSize = 100;
                db.STPNS.BulkInsertPageSize = 100;
                db.STAnS.BulkInsertPageSize = 100;
                db.STDc.BulkInsertPageSize = 100;

                db.ShowBase.BulkInsert(sbs);
                db.STDT.BulkInsert(dt);
                db.STPa.BulkInsert(pa);
                db.STIV.BulkInsert(iv);
                db.STPR.BulkInsert(pr);
                db.STIpc.BulkInsert(ic);
                db.STPNS.BulkInsert(pn);
                db.STAnS.BulkInsert(an);
                db.STDc.BulkInsert(dc);
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
    }
}
