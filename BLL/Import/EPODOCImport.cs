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
using xyExtensions;

namespace BLL.Import
{
    public class EPODOCImport : IDataImportAdapter
    {

        private Regex regRowSplit = new Regex("\\d{1,10}/\\d{1,10} - \\(C\\) EPODOC / EPO");
        private Regex MarkReplece = new Regex("\\s{1,2}(AB|AN|AP|CCA|CCI|CT|CTNP|CTSI|EC|FAMN|FI|FT|IC|ICAI|IN|NPR|OPD|PA|PD|PN|PR|RF|RFAP|RFNP|RID|TI|UC)\\s{0,4}-\\s");
        //private Regex regcharRNT = new Regex("(\\r|\\n|#[/]?CMT#)*");
        private Regex regcharRNT = new Regex("(\\r|\\n)*");
        private Regex regsharp = new Regex("\\#CMT\\#[^\\#]*\\#/CMT\\#");
        private Regex regspace = new Regex("\\s{4,}");
        private Regex rowRege = new Regex("【(?<colname>[^】]*)】(?<colval>[^【]*)");
        private string kunkaojin = Encoding.GetEncoding("utf-8").GetString(new byte[] { 0xEF, 0xBF, 0xBD, 0xEF, 0xBF, 0xBD });
        public override event ValueChangedEventHandler ShowProcess;
        public override event SetMaxValueEventHander SetMaxProcess;
        public EPODOCImport(string filename, string ztname, int zid)
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
            List<STFML> fmls = new List<STFML>();
            List<STCPc> cpcs = new List<STCPc>();
            int count = arys.Length - 1;
            SetMaxProcess(this, count);
            Thimport = new Thread(() =>
            {
                //第一行为空
                for (int i = 1; i <= count; i++)
                {
                    string tmp = regsharp.Replace(arys[i], "");
                    tmp = regspace.Replace(regcharRNT.Replace(tmp, ""), " ");
                    tmp = MarkReplece.Replace(tmp, "【$1】").Trim();
                    MatchCollection mhs = rowRege.Matches(tmp);
                    ShowBase sb = new ShowBase() { An = "", SiD = "", Ipc = "", Pa = "", IV = "", PN = "" };
                    sb.ZTID = ZTId;
                    sb.ImportDate = DateTime.Now;

                    foreach (Match m in mhs)
                    {
                        string colname = m.Groups["colname"].Value;
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
                            case "IC":
                            case "CCA":
                            case "CCI":
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
                            case "DS":
                                sb.DS = val;
                                break;
                            case "DMC":
                                sb.DMc = val;
                                break;
                            case "FAMN":
                                sb.FaMN = val;
                                break;
                            case "CT":
                                sb.Ct = val;
                                break;
                            case "CTNP":
                                sb.CtNP = val;
                                break;
                            case "FI":
                                sb.FI = val;
                                break;
                            case "FT":
                                sb.FT = val;
                                break;
                            case "RF":
                                sb.RF = val;
                                break;
                            case "RFAP":
                                sb.RFaP = val;
                                break;
                            case "RFNP":
                                sb.RFNP = val;
                                break;
                            case "EC":
                                sb.Ec = val;
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
                    List<STFML> fml = new List<STFML>();
                    List<STCPc> cpc = new List<STCPc>();
                    dts.Add(EPODOCIndex.AutoIndex(sb, out pa, out iv, out ipc, out pr, out fml, out cpc));
                    pas.AddRange(pa);
                    ivs.AddRange(iv);
                    ipcs.AddRange(ipc);
                    prs.AddRange(pr);
                    fmls.AddRange(fml);
                    cpcs.AddRange(cpc);
                    if (i % 100 == 0)
                    {

                        //Thread.Sleep(1000);
                        //todo:标引
                        //todo:标引信息入库

                        //todo:基本信息入库
                        //BulkInsert(sbs);
                        BulkInsert(sbs, pas, ivs, ipcs, prs, fml, dts, cpcs);
                        sbs.Clear();
                        pas.Clear();
                        ivs.Clear();
                        ipcs.Clear();
                        prs.Clear();
                        dts.Clear();
                        fmls.Clear();
                        cpcs.Clear();
                        ShowProcess(this, i, Skip_sum, Overwrite_sum, "导入");
                    }

                }
                if (sbs.Count > 0)
                {
                    BulkInsert(sbs, pas, ivs, ipcs, prs, fmls, dts, cpcs);
                    sbs.Clear();
                    pas.Clear();
                    ivs.Clear();
                    ipcs.Clear();
                    prs.Clear();
                    dts.Clear();
                    fmls.Clear();
                    cpcs.Clear();
                }

                ShowProcess(this, count, Skip_sum, Overwrite_sum, "导入完毕");

                Sr.Close();
                Sr.Dispose();

            });
            Thimport.Start();
            return true;
        }
        private bool BulkInsert(List<ShowBase> sbs, List<STPa> pa, List<STIV> iv, List<STIpc> ic, List<STPR> pr, List<STFML> fml, List<STDT> dt, List<STCPc> cpc)
        {


            using (MySqlDataContext db = new MySqlDataContext())
            {

                db.ShowBase.BulkInsertPageSize = 100;
                db.STDT.BulkInsertPageSize = 100;
                db.STPa.BulkInsertPageSize = 100;
                db.STIV.BulkInsertPageSize = 100;
                db.STPR.BulkInsertPageSize = 100;
                db.STIpc.BulkInsertPageSize = 100;
                db.STFML.BulkInsertPageSize = 100;
                db.STCPc.BulkInsertPageSize = 100;

                db.ShowBase.BulkInsert(sbs);
                db.STDT.BulkInsert(dt);
                db.STPa.BulkInsert(pa);
                db.STIV.BulkInsert(iv);
                db.STPR.BulkInsert(pr);
                db.STIpc.BulkInsert(ic);
                db.STFML.BulkInsert(fml);
                db.STCPc.BulkInsert(cpc);


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
