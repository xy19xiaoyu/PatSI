using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IData;
using BLL.Template;
using log4net;
using System.Data;
using DAL;
using System.Threading;
using BLL.Index;

namespace BLL.Import
{
    public class ExcelImport : IDataImportAdapter
    {
        private ImportTemplate Template;
        public override event ValueChangedEventHandler ShowProcess;
        public override event SetMaxValueEventHander SetMaxProcess;

        private DataTable dt = new DataTable();
        public ExcelImport(string filename, string ztname, int zid, string type, ImportTemplate temp)
            : base(filename, ztname, zid)
        {
            this.Template = temp;
            this.Type = (DataType)Enum.Parse(typeof(DataType), type);
            dt = ExcelLib.NPOIHelper.Excel2DataTable(Filename, Template.HasHeadColumn, 1, Template.FirstRowNum);
        }
        public override bool Import()
        {
            bool result = false;
            switch (Type)
            {
                case DataType.CPRS:
                    result = CNImport();
                    break;
                case DataType.EPODOC:
                    result = EPODocImport();
                    break;
                case DataType.WPI:
                    result = WPIImport();
                    break;
            }
            return true;
        }
        public bool CNImport()
        {
            XLSCPRSIndex.initmp(Template);
            List<DAL.ShowBase> sbs = new List<DAL.ShowBase>();
            List<DAL.STPa> pas = new List<DAL.STPa>();
            List<DAL.STIV> ivs = new List<DAL.STIV>();
            List<DAL.STIpc> ipcs = new List<DAL.STIpc>();
            List<DAL.STPR> prs = new List<DAL.STPR>();
            List<DAL.STDT> dts = new List<DAL.STDT>();
            List<DAL.STQY> qys = new List<DAL.STQY>();

            int count = dt.Rows.Count;
            SetMaxProcess(this, count);
            Thimport = new Thread(() =>
            {
                #region 处理一个DataTable
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ShowBase sb = new ShowBase();
                    sb.ZTID = ZTId;
                    sb.ImportDate = DateTime.Now;
                    STDT st = new STDT() { ZTID = sb.ZTID, ImportDate = sb.ImportDate };
                    #region 处理一条数据
                    foreach (DataColumn col in dt.Columns)
                    {

                        string colname = col.ColumnName;
                        string val = dt.Rows[i][col].ToString();
                        if (!Template.ColumnMapping.ContainsKey(colname)) continue;
                        ColumnMapping mapping = Template.ColumnMapping[colname];
                        string mappingcolname = mapping.SystemColumnShowName;
                        string tbname = mapping.Tablename;
                        int? intval = null;
                        sbyte? sbval = null;
                        sbyte ssb = 0;
                        if (SByte.TryParse(val.Replace("是", "1"), out ssb))
                        {
                            sbval = ssb;
                        }

                        int iit = 0;
                        if (int.TryParse(val, out iit))
                        {
                            intval = iit;
                        }

                        #region STDT
                        if (tbname.ToUpper() == "ST_DT")
                        {

                            switch (mappingcolname)
                            {
                                case "申请年":
                                    st.AdY = intval;
                                    break;
                                case "公开年":
                                    st.PDy = intval;
                                    break;
                                case "授权年":
                                    st.GdY = intval;
                                    break;
                                case "主申请人":
                                    st.FPa = val;
                                    break;
                                case "主发明人":
                                    st.FIn = val;
                                    break;
                                case "主IPC":
                                    st.FIpc = val;
                                    break;
                                case "权利要求1字数":
                                    st.ClSCharSum = intval;
                                    break;
                                case "专利类型":
                                    st.Type = val;
                                    break;
                                case "专利类型(含PCT)":
                                    st.Type1 = val;
                                    break;
                                case "主申请人类型":
                                    st.FPaType = val;
                                    break;
                                case "是否合作申请":
                                    st.IsHeZUO = sbval;
                                    break;
                                case "洲际":
                                    st.ZHoU = val;
                                    break;
                                case "是否国外来华":
                                    st.IsGuOwAi = sbval;
                                    break;
                                case "国家":
                                    st.GJ = val;
                                    break;
                                case "省":
                                    st.ShEng = val;
                                    break;
                                case "省(计划单列市）":
                                    st.ShEng1 = val;
                                    break;
                                case "市":
                                    st.ShI = val;
                                    break;
                                case "区县":
                                    st.QUXian = val;
                                    break;
                                case "经济区域":
                                    st.QUYU = val;
                                    break;
                                case "最终法律状态":
                                    st.LG = val;
                                    break;
                                case "法律截止年":
                                    st.LGYear = intval; ;
                                    break;
                                case "公开年差":
                                    st.PDyDef = intval; ;
                                    break;
                                case "授权年差":
                                    st.GdYDef = intval; ;
                                    break;
                                case "专利年龄":
                                    st.Age = sbval;
                                    break;
                                case "是否公知技术":
                                    st.IsGongZHi = sbval;
                                    break;
                                case "申请人数量":
                                    st.PaSum = sbval;
                                    break;
                                case "发明人数量":
                                    st.InSum = sbval;
                                    break;
                                case "IPC数量":
                                    st.IpcSum = sbval;
                                    break;
                                default:
                                    break;

                            }
                        }
                        #endregion
                        #region showbase
                        switch (mappingcolname)
                        {
                            case "申请号":
                                sb.An = val;
                                sb.SiD = val;
                                break;
                            case "发明名称":
                            case "标题":
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
                            case "省":
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
                            case "公告日":
                            case "授权日":
                                if (string.IsNullOrEmpty(sb.Gd))
                                {
                                    sb.Gd = val;
                                }
                                break;
                            case "授权号":
                            case "公告号":
                                if (string.IsNullOrEmpty(sb.GN))
                                {
                                    sb.GN = val;
                                }
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
                        #endregion

                    }
                    #endregion
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

                    st = XLSCPRSIndex.AutoIndex(sb, st, out pa, out iv, out ipc, out pr, out qy);
                    dts.Add(st);
                    pas.AddRange(pa);
                    ivs.AddRange(iv);
                    ipcs.AddRange(ipc);
                    prs.AddRange(pr);
                    qys.AddRange(qy);

                    if (i % 100 == 0)
                    {
                        //todo:基本信息入库
                        CNBulkInsert(sbs, pas, ivs, ipcs, prs, dts, qys);
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
                #endregion

                if (sbs.Count > 0)
                {
                    CNBulkInsert(sbs, pas, ivs, ipcs, prs, dts, qys);
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

            });
            Thimport.Start();
            return true;
        }
        public bool WPIImport()
        {
            XLSWPIIndex.initmp(Template);
            List<ShowBase> sbs = new List<ShowBase>();
            List<STPa> pas = new List<STPa>();
            List<STIV> ivs = new List<STIV>();
            List<STIpc> ipcs = new List<STIpc>();
            List<STPR> prs = new List<STPR>();
            List<STDT> dts = new List<STDT>();
            List<STDc> dcs = new List<STDc>();
            List<STPNS> pns = new List<STPNS>();
            List<STAnS> ans = new List<STAnS>();

            int count = dt.Rows.Count;
            SetMaxProcess(this, count);
            Thimport = new Thread(() =>
            {
                //
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ShowBase sb = new ShowBase();
                    sb.ZTID = ZTId;
                    sb.ImportDate = DateTime.Now;
                    STDT st = new STDT() { ZTID = sb.ZTID, SiD = sb.SiD, ImportDate = sb.ImportDate };

                    #region 一行记录
                    foreach (DataColumn col in dt.Columns)
                    {

                        string colname = col.ColumnName;
                        string val = dt.Rows[i][col].ToString();
                        if (!Template.ColumnMapping.ContainsKey(colname)) continue;
                        ColumnMapping mapping = Template.ColumnMapping[colname];
                        string mappingcolname = mapping.SystemColumnShowName;
                        string tbname = mapping.Tablename;

                        int? intval = null;
                        sbyte? sbval = null;

                        int iit = 0;
                        sbyte ssb = 0;
                        if (sbyte.TryParse(val, out ssb))
                        {
                            sbval = ssb;
                        }
                        if (int.TryParse(val, out iit))
                        {
                            intval = iit;
                        }
                        #region STDT

                        if (tbname.ToUpper() == "ST_DT")
                        {
                            switch (mappingcolname)
                            {
                                case "申请年":
                                    st.AdY = intval;
                                    break;
                                case "公开年":
                                    st.AdY = intval;
                                    break;
                                case "最早优先权年":
                                    st.OpDy = intval;
                                    break;
                                case "主申请人":
                                    st.FPa = val;
                                    break;
                                case "主发明人":
                                    st.FIn = val;
                                    break;
                                case "主IPC":
                                    st.FIpc = val;
                                    break;
                                case "主CPC":
                                    st.FCPc = val;
                                    break;
                                case "主DC":
                                    st.FDMc = val;
                                    break;
                                case "是否合作申请":
                                    st.IsHeZUO = sbval;
                                    break;
                                case "洲际":
                                    st.ZHoU = val;
                                    break;
                                case "是否三局":
                                    st.IsSanJU = sbval;
                                    break;
                                case "是否五局":
                                    st.IsWuJU = sbval;
                                    break;
                                case "国家":
                                    st.GJ = val;
                                    break;
                                case "公开年差":
                                    st.PDyDef = intval;
                                    break;
                                case "申请人数量":
                                    st.PaSum = sbval;
                                    break;
                                case "发明人数量":
                                    st.InSum = sbval;
                                    break;
                                case "IPC数量":
                                    st.IpcSum = sbval;
                                    break;
                                case "CPC数量":
                                    st.CPcSum = sbval;
                                    break;
                                case "DC数量":
                                    st.DMcSum = sbval;
                                    break;
                                case "同族数量":
                                    st.FMLSum = sbval;
                                    break;
                                case "国家数量":
                                    st.GJSum = sbval;
                                    break;
                                case "引证数量":
                                    st.YZSum = sbval;
                                    break;
                                case "被引证数量":
                                    st.ByZSum = sbval;
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion
                        #region SHOWBASE
                        switch (mappingcolname)
                        {
                            case "AN":
                            case "入藏号":
                                sb.SiD = val;
                                break;
                            case "申请号(原始)":
                            case "申请号":
                                sb.An = val;
                                break;
                            case "标题":
                            case "专利名称":
                                sb.Title = val;
                                break;
                            case "IPC":
                                if (sb.Ipc == null)
                                {
                                    sb.Ipc = val;
                                }
                                else
                                {
                                    sb.Ipc += ";" + val;
                                }

                                break;
                            case "申请人":
                                sb.Pa = val;
                                break;
                            case "申请人代码":
                                sb.CPY = val;
                                break;
                            case "最早优先权日":
                                sb.OpD = val;
                                break;
                            case "摘要":
                                sb.ABs = val;
                                break;
                            case "发明人":
                                sb.IV = val;
                                break;
                            case "公开号(原始)":
                            case "公开号":
                                sb.PN = val;
                                break;
                            case "优先权":
                                sb.PR = val;
                                break;
                            case "公开日":
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
                            default:
                                break;
                        }
                        #endregion
                    }
                    #endregion
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
                    st = XLSWPIIndex.AutoIndex(sb, st, out pa, out iv, out ipc, out pr, out pn, out an, out dc);
                    dts.Add(st);
                    pas.AddRange(pa);
                    ivs.AddRange(iv);
                    ipcs.AddRange(ipc);
                    prs.AddRange(pr);
                    pns.AddRange(pn);
                    ans.AddRange(an);
                    dcs.AddRange(dc);
                    if (i % 100 == 0)
                    {
                        WPIBulkInsert(sbs, pas, ivs, ipcs, prs, dts, pns, ans, dcs);
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
                    WPIBulkInsert(sbs, pas, ivs, ipcs, prs, dts, pns, ans, dcs);
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
            });
            Thimport.Start();
            return true;
        }
        public bool EPODocImport()
        {

            List<ShowBase> sbs = new List<ShowBase>();
            List<STPa> pas = new List<STPa>();
            List<STIV> ivs = new List<STIV>();
            List<STIpc> ipcs = new List<STIpc>();
            List<STPR> prs = new List<STPR>();
            List<STDT> dts = new List<STDT>();
            List<STFML> fmls = new List<STFML>();
            List<STCPc> cpcs = new List<STCPc>();

            int count = dt.Rows.Count;
            SetMaxProcess(this, count);
            Thimport = new Thread(() =>
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ShowBase sb = new ShowBase();
                    sb.ZTID = ZTId;
                    sb.ImportDate = DateTime.Now;
                    STDT st = new STDT() { ZTID = sb.ZTID, ImportDate = sb.ImportDate };

                    #region 处理一条数据
                    foreach (DataColumn col in dt.Columns)
                    {
                        string colname = col.ColumnName;
                        string val = dt.Rows[i][col].ToString();
                        if (!Template.ColumnMapping.ContainsKey(colname)) continue;
                        ColumnMapping mapping = Template.ColumnMapping[colname];
                        string mappingcolname = mapping.SystemColumnShowName;
                        string tbname = mapping.Tablename;
                        int? intval = null;
                        sbyte? sbval = null;
                        sbyte ssb = 0;
                        if (SByte.TryParse(val.Replace("是", "1"), out ssb))
                        {
                            sbval = ssb;
                        }

                        int iit = 0;
                        if (int.TryParse(val, out iit))
                        {
                            intval = iit;
                        }

                        #region STDT
                        if (tbname.ToUpper() == "ST_DT")
                        {
                            switch (mappingcolname)
                            {
                                case "申请年":
                                    st.AdY = intval;
                                    break;
                                case "公开年":
                                    st.PDy = intval;
                                    break;
                                case "最早优先权年":
                                    st.OpDy = intval;
                                    break;
                                case "主申请人":
                                    st.FPa = val;
                                    break;
                                case "主发明人":
                                    st.FIn = val;
                                    break;
                                case "主IPC":
                                    st.FIpc = val;
                                    break;
                                case "主CPC":
                                    st.FCPc = val;
                                    break;
                                case "是否合作申请":
                                    st.IsHeZUO = sbval;
                                    break;
                                case "洲际":
                                    st.ZHoU = val;
                                    break;
                                case "国家":
                                    st.GJ = val;
                                    break;
                                case "公开年差":
                                    st.PDyDef = intval;
                                    break;
                                case "申请人数量":
                                    st.PaSum = sbval;
                                    break;
                                case "发明人数量":
                                    st.InSum = sbval;
                                    break;
                                case "IPC数量":
                                    st.IpcSum = sbval;
                                    break;
                                case "CPC数量":
                                    st.CPcSum = sbval;
                                    break;
                                case "引证数量":
                                    st.YZSum = sbval;
                                    break;
                                case "被引证数量":
                                    st.ByZSum = sbval;
                                    break;
                                default:
                                    break;

                            }
                        }
                        #endregion
                        #region SHOWBASE
                        switch (mappingcolname)
                        {
                            case "AN":
                            case "入藏号":
                                sb.SiD = val;
                                break;
                            case "申请号":
                                sb.An = val;
                                break;
                            case "申请日":
                                sb.Ad = val;
                                break;
                            case "标题":
                                sb.Title = val;
                                break;
                            case "IPC":
                                if (sb.Ipc == null)
                                {
                                    sb.Ipc = val;
                                }
                                else
                                {
                                    sb.Ipc += ";" + val;
                                }
                                break;
                            case "CPC":
                                if (sb.CPc == null)
                                {
                                    sb.CPc = val;
                                }
                                else
                                {
                                    sb.CPc += ";" + val;
                                }

                                break;
                            case "申请人":
                                sb.Pa = val;
                                break;
                            case "申请人代码":
                                sb.CPY = val;
                                break;
                            case "最早优先权日":
                                sb.OpD = val;
                                break;
                            case "摘要":
                                sb.ABs = val;
                                break;
                            case "发明人":
                                sb.IV = val;
                                break;
                            case "公开号":
                                sb.PN = val;
                                break;
                            case "优先权":
                                sb.PR = val;
                                break;
                            case "公开日":
                                sb.PD = val;
                                break;
                            case "DS":
                                sb.DS = val;
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
                        #endregion
                    }
                    #endregion
                    #region 标引
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
                    st = XLSEPODOCIndex.AutoIndex(sb, st, out pa, out iv, out ipc, out pr, out fml, out cpc);
                    dts.Add(st);
                    pas.AddRange(pa);
                    ivs.AddRange(iv);
                    ipcs.AddRange(ipc);
                    prs.AddRange(pr);
                    fmls.AddRange(fml);
                    cpcs.AddRange(cpc);
                    #endregion

                    if (i % 100 == 0)
                    {

                        EPODOCBulkInsert(sbs, pas, ivs, ipcs, prs, fml, dts, cpcs);
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
                    EPODOCBulkInsert(sbs, pas, ivs, ipcs, prs, fmls, dts, cpcs);
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


            });
            Thimport.Start();
            return true;
        }
        private bool CNBulkInsert(List<ShowBase> sbs, List<STPa> pa, List<STIV> iv, List<STIpc> ic, List<STPR> pr, List<STDT> dt, List<STQY> qy)
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
        private bool WPIBulkInsert(List<ShowBase> sbs, List<STPa> pa, List<STIV> iv, List<STIpc> ic, List<STPR> pr, List<STDT> dt, List<STPNS> pn, List<STAnS> an, List<STDc> dc)
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
        private bool EPODOCBulkInsert(List<ShowBase> sbs, List<STPa> pa, List<STIV> iv, List<STIpc> ic, List<STPR> pr, List<STFML> fml, List<STDT> dt, List<STCPc> cpc)
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

    }
}
