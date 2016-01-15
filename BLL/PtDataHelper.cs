#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2015 @ CPIC Corp
*	CLR 版本: 4.0.30319.225
*	文 件 名: PtDataHelper.cs
*	创 建 人: xiwenlei(xiwenlei)
*	创建日期: 2015/4/14 22:02:32
*	版    本: V1.0
*	备注描述: $Myparameter1$           
*
* 修改历史: 
*   ****NO_1:
*	修 改 人: 
*	修改日期: 
*	描    述: $Myparameter1$           
******************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBA;
using BLL.Model;

/// <summary>
/// BLL 的摘要说明
/// </summary>
namespace BLL
{
    /// <summary>
    ///PtDataHelper 的摘要说明
    /// </summary>
    public class PtDataHelper
    {
        private static TbFiledList tbShow_base = new TbFiledList("show_base");

        public static TbFiledList TbShow_base
        {
            get { return PtDataHelper.tbShow_base; }
            set { PtDataHelper.tbShow_base = value; }
        }


        static PtDataHelper()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //

        }

        public static int getPtDataCount(string strWhere)
        {
            int nRs = 0;
            try
            {
                if ((!strWhere.Trim().Equals("")) && (!strWhere.ToLower().Contains(" where ")))
                {
                    strWhere = " where " + strWhere;
                }

                string strSql = string.Format("select count(*) from {0} a1 {1}", tbShow_base.StrTbName, strWhere);
                nRs = Convert.ToInt32(MySqlDbAccess.ExecuteScalar(CommandType.Text, strSql));
            }
            catch (Exception ex)
            {
            }

            return nRs;
        }

        public static DataTable getPtData(string strWhere, string strOdery, int nPageIdx, int nPageSize, string strDbTy)
        {
            DataTable dt = null;

            if ((!strWhere.Trim().Equals("")) && (!strWhere.ToLower().Contains(" where ")))
            {
                strWhere = " where " + strWhere;
            }
            if (!strOdery.Trim().Equals(""))
            {
                strOdery = " order by " + strOdery;
            }

            //string strSql = string.Format("select (@rowIdx:=@rowIdx+1) AS 序号,{0} from {1} a,(SELECT @rowIdx:={3}) b {2} {5} limit {3},{4}", tbShow_base.ToShowString(strDbTy),
            //    tbShow_base.StrTbName, strWhere, (nPageIdx - 1) * nPageSize, nPageSize, strOdery);
            string strSql = string.Format("select (@rowIdx:=@rowIdx+1) AS 序号,{0} {1} {2} {5} limit {3},{4}",
                tbShow_base.ToFiledFromString(strDbTy, string.Format("(SELECT @rowIdx:={0}) b ", (nPageIdx - 1) * nPageSize), strWhere),
                "", "", (nPageIdx - 1) * nPageSize, nPageSize, strOdery);
            dt = MySqlDbAccess.GetDataTable(CommandType.Text, strSql);
            return dt;
        }

        public static DataTable getPtData(string strWhere, string strOdery, string strTbName)
        {
            DataTable dt = null;

            if (!strWhere.Trim().Equals(""))
            {
                strWhere = " where " + strWhere;
            }
            if (!strOdery.Trim().Equals(""))
            {
                strOdery = " order by " + strOdery;
            }

            string strSql = string.Format("select {0} from {1}  {2} {3}", "*",
                strTbName, strWhere, strOdery);
            dt = MySqlDbAccess.GetDataTable(CommandType.Text, strSql);
            return dt;
        }

        public static DataTable getPtData(string strFileds, string strWhere, string strOdery, string strTbName)
        {
            DataTable dt = null;

            if (!strWhere.Trim().Equals(""))
            {
                strWhere = " where " + strWhere;
            }
            if (!strOdery.Trim().Equals(""))
            {
                strOdery = " order by " + strOdery;
            }

            string strSql = string.Format("select {0} from {1}  {2} {3}", strFileds,
                strTbName, strWhere, strOdery);
            dt = MySqlDbAccess.GetDataTable(CommandType.Text, strSql);
            return dt;
        }

        public static T Clone<T>(T source)
        {
            var dcs = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
            using (var ms = new System.IO.MemoryStream())
            {
                dcs.WriteObject(ms, source);
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                return (T)dcs.ReadObject(ms);
            }
        }
        public static bool DelPt(string _strSid, string _strZid)
        {
            bool bRs = false;

            string strSqlTmp = @"delete from show_base where sid='{0}' and ztid={1};"
                            + "delete from st_ans where sid='{0}' and ztid={1};"
                            + "delete from st_cpc where sid='{0}' and ztid={1};"
                            + "delete from st_ct where sid='{0}' and ztid={1};"
                            + "delete from st_dc where sid='{0}' and ztid={1};"
                            + "delete from st_dt where sid='{0}' and ztid={1};"
                            + "delete from st_ec where sid='{0}' and ztid={1};"
                            + "delete from st_fml where sid='{0}' and ztid={1};"
                            + "delete from st_ipc where sid='{0}' and ztid={1};"
                            + "delete from st_iv where sid='{0}' and ztid={1};"
                            + "delete from st_pa where sid='{0}' and ztid={1};"
                            + "delete from st_pns where sid='{0}' and ztid={1};"
                            + "delete from st_pr where sid='{0}' and ztid={1};"
                            + "update st_ztlist set app_sum=app_sum-1 where id={1};";

            if (_strSid.Contains("','"))
            {
                int nCount = _strSid.Split(',').Count();
                strSqlTmp = @"delete from show_base where sid in ({0}) and ztid={1};"
                           + "delete from st_ans where sid in ({0}) and ztid={1};"
                           + "delete from st_cpc where sid in ({0}) and ztid={1};"
                           + "delete from st_ct where sid in ({0}) and ztid={1};"
                           + "delete from st_dc where sid in ({0}) and ztid={1};"
                           + "delete from st_dt where sid in ({0}) and ztid={1};"
                           + "delete from st_ec where sid in ({0}) and ztid={1};"
                           + "delete from st_fml where sid in ({0}) and ztid={1};"
                           + "delete from st_ipc where sid in ({0}) and ztid={1};"
                           + "delete from st_iv where sid in ({0}) and ztid={1};"
                           + "delete from st_pa where sid in ({0}) and ztid={1};"
                           + "delete from st_pns where sid in ({0}) and ztid={1};"
                           + "delete from st_pr where sid in ({0}) and ztid={1};"
                           + "update st_ztlist set app_sum=app_sum-" + nCount.ToString() + " where id={1};";
            }

            try
            {

                MySqlDbAccess.ExecNoQuery(CommandType.Text, string.Format(strSqlTmp, _strSid, _strZid));

                bRs = true;
            }
            catch (Exception ex)
            {
                bRs = false;
            }
            return bRs;
        }

        public static bool CopyPt(string _strSid, string _strZid, string _strDesZtId)
        {
            bool bRs = false;

            string strSqlTmp = @"delete from show_base where sid='{0}' and ztid={1};"
                            + "delete from st_ans where sid='{0}' and ztid={1};"
                            + "delete from st_cpc where sid='{0}' and ztid={1};"
                            + "delete from st_ct where sid='{0}' and ztid={1};"
                            + "delete from st_dc where sid='{0}' and ztid={1};"
                            + "delete from st_dt where sid='{0}' and ztid={1};"
                            + "delete from st_ec where sid='{0}' and ztid={1};"
                            + "delete from st_fml where sid='{0}' and ztid={1};"
                            + "delete from st_ipc where sid='{0}' and ztid={1};"
                            + "delete from st_iv where sid='{0}' and ztid={1};"
                            + "delete from st_pa where sid='{0}' and ztid={1};"
                            + "delete from st_pns where sid='{0}' and ztid={1};"
                            + "delete from st_pr where sid='{0}' and ztid={1};"
                            + "update st_ztlist set app_sum=app_sum+1 where id={2};";

            try
            {

                //MySqlDbAccess.ExecNoQuery(CommandType.Text, string.Format(strSqlTmp, _strSid, _strZid));

                using (MySqlDataContext db = new MySqlDataContext())
                {
                    DAL.ShowBase stbase = db.ShowBase.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                    if (stbase != null)
                    {
                        DAL.ShowBase tmp = Clone(stbase);
                        tmp.ZTID = int.Parse(_strDesZtId);
                        tmp.ID = 0;
                        db.ShowBase.InsertOnSubmit(tmp);
                    }

                    DAL.STAnS stans = db.STAnS.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                    if (stans != null)
                    {
                        DAL.STAnS tmp = Clone(stans);
                        tmp.ZTID = int.Parse(_strDesZtId);
                        tmp.ID = 0;
                        db.STAnS.InsertOnSubmit(tmp);
                    }

                    DAL.STCPc stcpc = db.STCPc.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                    if (stcpc != null)
                    {
                        DAL.STCPc tmp = Clone(stcpc);
                        tmp.ZTID = int.Parse(_strDesZtId);
                        tmp.ID = 0;
                        db.STCPc.InsertOnSubmit(tmp);
                    }

                    DAL.STCt stct = db.STCt.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                    if (stct != null)
                    {
                        DAL.STCt tmp = Clone(stct);
                        tmp.ZTID = int.Parse(_strDesZtId);
                        tmp.ID = 0;
                        db.STCt.InsertOnSubmit(tmp);
                    }

                    DAL.STDc stdc = db.STDc.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                    if (stdc != null)
                    {
                        DAL.STDc tmp = Clone(stdc);
                        tmp.ZTID = int.Parse(_strDesZtId);
                        tmp.ID = 0;
                        db.STDc.InsertOnSubmit(tmp);
                    }

                    DAL.STDT stdt = db.STDT.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                    if (stdt != null)
                    {
                        DAL.STDT tmp = Clone(stdt);
                        tmp.ZTID = int.Parse(_strDesZtId);
                        tmp.ID = 0;
                        db.STDT.InsertOnSubmit(tmp);
                    }

                    DAL.STEc stec = db.STEc.FirstOrDefault(x => x.Sid == _strSid && x.ZTID.Value.ToString() == _strZid);
                    if (stec != null)
                    {
                        DAL.STEc tmp = Clone(stec);
                        tmp.ZTID = int.Parse(_strDesZtId);
                        tmp.ID = 0;
                        db.STEc.InsertOnSubmit(tmp);
                    }

                    DAL.STFML stfml = db.STFML.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                    if (stfml != null)
                    {
                        DAL.STFML tmp = Clone(stfml);
                        tmp.ZTID = int.Parse(_strDesZtId);
                        tmp.ID = 0;
                        db.STFML.InsertOnSubmit(tmp);
                    }

                    DAL.STIpc stipc = db.STIpc.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                    if (stipc != null)
                    {
                        DAL.STIpc tmp = Clone(stipc);
                        tmp.ZTID = int.Parse(_strDesZtId);
                        tmp.ID = 0;
                        db.STIpc.InsertOnSubmit(tmp);
                    }

                    DAL.STIV stiv = db.STIV.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                    if (stiv != null)
                    {
                        DAL.STIV tmp = Clone(stiv);
                        tmp.ZTID = int.Parse(_strDesZtId);
                        tmp.ID = 0;
                        db.STIV.InsertOnSubmit(tmp);
                    }

                    DAL.STPa stpa = db.STPa.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                    if (stpa != null)
                    {
                        DAL.STPa tmp = Clone(stpa);
                        tmp.ZTID = int.Parse(_strDesZtId);
                        tmp.ID = 0;
                        db.STPa.InsertOnSubmit(tmp);
                    }

                    DAL.STPNS stpns = db.STPNS.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                    if (stpns != null)
                    {
                        DAL.STPNS tmp = Clone(stpns);
                        tmp.ZTID = int.Parse(_strDesZtId);
                        tmp.ID = 0;
                        db.STPNS.InsertOnSubmit(tmp);
                    }

                    DAL.STPR stpr = db.STPR.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                    if (stpr != null)
                    {
                        DAL.STPR tmp = Clone(stpr);
                        tmp.ZTID = int.Parse(_strDesZtId);
                        tmp.ID = 0;
                        db.STPR.InsertOnSubmit(tmp);
                    }

                    DAL.STZTList stztlist = db.STZTList.FirstOrDefault(x => x.ID == int.Parse(_strDesZtId));
                    if (stztlist != null)
                    {
                        stztlist.AppSum = stztlist.AppSum == null ? 1 : stztlist.AppSum.Value + 1;
                    }

                    db.SubmitChanges();
                }

                bRs = true;
            }
            catch (Exception ex)
            {
                bRs = false;
            }
            return bRs;
        }

        public static bool CopyPt(List<string> lstSid, string _strZid, string _strDesZtId)
        {
            bool bRs = false;

            try
            {

                //MySqlDbAccess.ExecNoQuery(CommandType.Text, string.Format(strSqlTmp, _strSid, _strZid));

                using (MySqlDataContext db = new MySqlDataContext())
                {
                    foreach (string _strSid in lstSid)
                    {
                        DAL.ShowBase stbase = db.ShowBase.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                        if (stbase != null)
                        {
                            DAL.ShowBase tmp = Clone(stbase);
                            tmp.ZTID = int.Parse(_strDesZtId);
                            tmp.ID = 0;
                            db.ShowBase.InsertOnSubmit(tmp);
                        }

                        DAL.STAnS stans = db.STAnS.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                        if (stans != null)
                        {
                            DAL.STAnS tmp = Clone(stans);
                            tmp.ZTID = int.Parse(_strDesZtId);
                            tmp.ID = 0;
                            db.STAnS.InsertOnSubmit(tmp);
                        }

                        DAL.STCPc stcpc = db.STCPc.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                        if (stcpc != null)
                        {
                            DAL.STCPc tmp = Clone(stcpc);
                            tmp.ZTID = int.Parse(_strDesZtId);
                            tmp.ID = 0;
                            db.STCPc.InsertOnSubmit(tmp);
                        }

                        DAL.STCt stct = db.STCt.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                        if (stct != null)
                        {
                            DAL.STCt tmp = Clone(stct);
                            tmp.ZTID = int.Parse(_strDesZtId);
                            tmp.ID = 0;
                            db.STCt.InsertOnSubmit(tmp);
                        }

                        DAL.STDc stdc = db.STDc.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                        if (stdc != null)
                        {
                            DAL.STDc tmp = Clone(stdc);
                            tmp.ZTID = int.Parse(_strDesZtId);
                            tmp.ID = 0;
                            db.STDc.InsertOnSubmit(tmp);
                        }

                        DAL.STDT stdt = db.STDT.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                        if (stdt != null)
                        {
                            DAL.STDT tmp = Clone(stdt);
                            tmp.ZTID = int.Parse(_strDesZtId);
                            tmp.ID = 0;
                            db.STDT.InsertOnSubmit(tmp);
                        }

                        DAL.STEc stec = db.STEc.FirstOrDefault(x => x.Sid == _strSid && x.ZTID.Value.ToString() == _strZid);
                        if (stec != null)
                        {
                            DAL.STEc tmp = Clone(stec);
                            tmp.ZTID = int.Parse(_strDesZtId);
                            tmp.ID = 0;
                            db.STEc.InsertOnSubmit(tmp);
                        }

                        DAL.STFML stfml = db.STFML.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                        if (stfml != null)
                        {
                            DAL.STFML tmp = Clone(stfml);
                            tmp.ZTID = int.Parse(_strDesZtId);
                            tmp.ID = 0;
                            db.STFML.InsertOnSubmit(tmp);
                        }

                        DAL.STIpc stipc = db.STIpc.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                        if (stipc != null)
                        {
                            DAL.STIpc tmp = Clone(stipc);
                            tmp.ZTID = int.Parse(_strDesZtId);
                            tmp.ID = 0;
                            db.STIpc.InsertOnSubmit(tmp);
                        }

                        DAL.STIV stiv = db.STIV.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                        if (stiv != null)
                        {
                            DAL.STIV tmp = Clone(stiv);
                            tmp.ZTID = int.Parse(_strDesZtId);
                            tmp.ID = 0;
                            db.STIV.InsertOnSubmit(tmp);
                        }

                        DAL.STPa stpa = db.STPa.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                        if (stpa != null)
                        {
                            DAL.STPa tmp = Clone(stpa);
                            tmp.ZTID = int.Parse(_strDesZtId);
                            tmp.ID = 0;
                            db.STPa.InsertOnSubmit(tmp);
                        }

                        DAL.STPNS stpns = db.STPNS.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                        if (stpns != null)
                        {
                            DAL.STPNS tmp = Clone(stpns);
                            tmp.ZTID = int.Parse(_strDesZtId);
                            tmp.ID = 0;
                            db.STPNS.InsertOnSubmit(tmp);
                        }

                        DAL.STPR stpr = db.STPR.FirstOrDefault(x => x.SiD == _strSid && x.ZTID.Value.ToString() == _strZid);
                        if (stpr != null)
                        {
                            DAL.STPR tmp = Clone(stpr);
                            tmp.ZTID = int.Parse(_strDesZtId);
                            tmp.ID = 0;
                            db.STPR.InsertOnSubmit(tmp);
                        }

                        DAL.STZTList stztlist = db.STZTList.FirstOrDefault(x => x.ID == int.Parse(_strDesZtId));
                        if (stztlist != null)
                        {
                            stztlist.AppSum = stztlist.AppSum == null ? 1 : stztlist.AppSum.Value + 1;
                        }
                    }

                    db.SubmitChanges();
                }

                bRs = true;
            }
            catch (Exception ex)
            {
                bRs = false;
            }
            return bRs;
        }

        public static bool MovePt(string _strSid, string _strZid, string _strDesZtId)
        {
            bool bRs = false;

            string strSqlTmp = @"update show_base set ztid='{2}' where sid='{0}' and ztid={1};"
                            + "update  st_ans set ztid='{2}' where sid='{0}' and ztid={1};"
                            + "update  st_cpc set ztid='{2}' where sid='{0}' and ztid={1};"
                            + "update  st_ct  set ztid='{2}' where sid='{0}' and ztid={1};"
                            + "update  st_dc  set ztid='{2}' where sid='{0}' and ztid={1};"
                            + "update  st_dt set ztid='{2}' where sid='{0}' and ztid={1};"
                            + "update  st_ec set ztid='{2}' where sid='{0}' and ztid={1};"
                            + "update  st_fml set ztid='{2}' where sid='{0}' and ztid={1};"
                            + "update  st_ipc set ztid='{2}' where sid='{0}' and ztid={1};"
                            + "update  st_iv set ztid='{2}' where sid='{0}' and ztid={1};"
                            + "update  st_pa set ztid='{2}' where sid='{0}' and ztid={1};"
                            + "update  st_pns set ztid='{2}' where sid='{0}' and ztid={1};"
                            + "update  st_pr set ztid='{2}' where sid='{0}' and ztid={1};"
                            + "update st_ztlist set app_sum=if(isnull(app_sum),1,app_sum-1) where id={1};"
                            + "update st_ztlist set app_sum=if(isnull(app_sum),1,app_sum+1) where id={2};";

            if (_strSid.Contains("','"))
            {
                int nCount = _strSid.Split(',').Count();

                strSqlTmp = @"update show_base set ztid='{2}' where sid in ({0}) and ztid={1};"
                           + "update  st_ans set ztid='{2}' where sid in ({0}) and ztid={1};"
                           + "update  st_cpc set ztid='{2}' where sid in ({0}) and ztid={1};"
                           + "update  st_ct  set ztid='{2}' where sid in ({0}) and ztid={1};"
                           + "update  st_dc  set ztid='{2}' where sid in ({0}) and ztid={1};"
                           + "update  st_dt set ztid='{2}' where sid in ({0}) and ztid={1};"
                           + "update  st_ec set ztid='{2}' where sid in ({0}) and ztid={1};"
                           + "update  st_fml set ztid='{2}' where sid in ({0}) and ztid={1};"
                           + "update  st_ipc set ztid='{2}' where sid in ({0}) and ztid={1};"
                           + "update  st_iv set ztid='{2}' where sid in ({0}) and ztid={1};"
                           + "update  st_pa set ztid='{2}' where sid in ({0}) and ztid={1};"
                           + "update  st_pns set ztid='{2}' where sid in ({0}) and ztid={1};"
                           + "update  st_pr set ztid='{2}' where sid in ({0}) and ztid={1};"
                           + string.Format("update st_ztlist set app_sum=if(isnull(app_sum),{0},app_sum-{0}) where id={1};", nCount, "{1}")
                           + string.Format("update st_ztlist set app_sum=if(isnull(app_sum),{0},app_sum+{0}) where id={1};", nCount, "{2}");
            }
            else
            {
                _strSid = _strSid.Replace("'", "");
            }

            try
            {

                MySqlDbAccess.ExecNoQuery(CommandType.Text, string.Format(strSqlTmp, _strSid, _strZid, _strDesZtId));

                bRs = true;
            }
            catch (Exception ex)
            {
                bRs = false;
            }
            return bRs;
        }

        public static bool UpdatePt(string _strId, string _strUpFileds, string _strTb)
        {
            bool bRs = false;

            string strSql = string.Format("update {0} set {1} where id={2}", _strTb, _strUpFileds, _strId);
            try
            {
                MySqlDbAccess.ExecNoQuery(CommandType.Text, strSql);

                bRs = true;
            }
            catch (Exception ex)
            {
                bRs = false;
            }
            return bRs;
        }
    }
}
