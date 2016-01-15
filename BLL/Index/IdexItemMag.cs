#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2015 @ CPIC Corp
*	CLR 版本: 4.0.30319.225
*	文 件 名: IdexItemMag.cs
*	创 建 人: xiwenlei(xiwenlei) $ chenxiaoyu(xy)
*	创建日期: 2015/5/11 18:18:13
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
using System.Text;
using DAL;
using System.Linq;
using BLL.DBLinq;
using System.Data;
using DBA;

/// <summary>
/// 为CPRS2010组件，提供对检索、检索历史及用户的相关操作类
/// </summary>
namespace BLL.Index
{
    /// <summary>
    ///IdexItemMag 的摘要说明
    /// </summary>
    public class IdexItemMag
    {
        public IdexItemMag()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }




        public static bool AddNewKey(Dictionary<IDXKey, List<IDXVAL>> _dicNewKey)
        {
            bool bRs = false;

            return bRs;
        }

        public static bool AddNewKey(string strKeyName, List<string> _strValues, string strCreatUID)
        {
            bool bRs = false;
            //insert INTO idx_key VALUES(0,0,'212121',1,now());select @@IDENTITY;
            //BLL.SysMag.Login.StrLoginUserID
            string strKeyID = DBA.MySqlDbAccess.ExecuteScalar(System.Data.CommandType.Text,
                string.Format("insert INTO idx_key VALUES(0,0,'{0}',{1},now());select @@IDENTITY;", strKeyName, strCreatUID)).ToString();

            if (!string.IsNullOrEmpty(strKeyID))
            {
                List<IDXVAL> idxKeyVal = new List<IDXVAL>();
                foreach (string strItem in _strValues)
                {
                    idxKeyVal.Add(new IDXVAL()
                    {
                        CreateUser = int.Parse(strCreatUID),
                        KeyID = int.Parse(strKeyID),
                        VAL = strItem,
                        Pid = 0,
                        AutoIDXContent = "",
                        CreateTime = DateTime.Now
                    });
                }
                using (MySqlDataContext db = new MySqlDataContext())
                {

                    db.IDXVAL.BulkInsertPageSize = 100;
                    db.IDXVAL.BulkInsert(idxKeyVal);
                    db.SubmitChanges();
                }

                bRs = true;
            }
            return bRs;
        }

        public static bool AddNewVal(string strValName, string strKeyID, string strCreatUID, out int _nValID)
        {
            bool bRs = false;
            _nValID = -1;
            //insert INTO idx_key VALUES(0,0,'212121',1,now());select @@IDENTITY;
            //BLL.SysMag.Login.StrLoginUserID
            try
            {
               _nValID=int.Parse(DBA.MySqlDbAccess.ExecuteScalar(System.Data.CommandType.Text,
                   string.Format("insert INTO idx_val(pid,keyid,val,auto_idx_content,createuser,createtime) VALUES(0,{0},'{1}','',{2},now());select @@IDENTITY;", strKeyID, strValName, strCreatUID)).ToString());
                bRs = true;
            }
            catch (Exception ex)
            {
            }

            return bRs;
        }

        public static bool UpdateKeyItem(string strKeyName, List<string> _strAddValues, string strCreatUID, string _strKeyID)
        {
            bool bRs = false;
            //insert INTO idx_key VALUES(0,0,'212121',1,now());select @@IDENTITY;
            //BLL.SysMag.Login.StrLoginUserID
            string strKeyID = _strKeyID;

            DBA.MySqlDbAccess.ExecNoQuery(System.Data.CommandType.Text,
               string.Format("update idx_key set idx_key='{0}',createuser={1} where id={2} ", strKeyName, strCreatUID, strKeyID));

            if (!string.IsNullOrEmpty(strKeyID))
            {
                List<IDXVAL> idxKeyVal = new List<IDXVAL>();
                foreach (string strItem in _strAddValues)
                {
                    idxKeyVal.Add(new IDXVAL()
                    {
                        CreateUser = int.Parse(strCreatUID),
                        KeyID = int.Parse(strKeyID),
                        VAL = strItem,
                        Pid = 0,
                        AutoIDXContent = "",
                        CreateTime = DateTime.Now
                    });
                }
                using (MySqlDataContext db = new MySqlDataContext())
                {

                    db.IDXVAL.BulkInsertPageSize = 100;
                    db.IDXVAL.BulkInsert(idxKeyVal);
                    db.SubmitChanges();
                }

                bRs = true;
            }
            return bRs;
        }

        public static bool UpdateKey(string strKeyName, string strCreatUID, string _strKeyID)
        {
            bool bRs = false;
            //insert INTO idx_key VALUES(0,0,'212121',1,now());select @@IDENTITY;
            //BLL.SysMag.Login.StrLoginUserID
            string strKeyID = _strKeyID;

            try
            {
                DBA.MySqlDbAccess.ExecNoQuery(System.Data.CommandType.Text,
                   string.Format("update idx_key set idx_key='{0}',createuser={1} where id={2} ", strKeyName, strCreatUID, strKeyID));


                bRs = true;
            }
            catch (Exception ex)
            {
                bRs = false;
            }

            return bRs;
        }

        public static bool UpdateVal(string strValName, string strCreatUID, string _strValID)
        {
            bool bRs = false;
            //insert INTO idx_key VALUES(0,0,'212121',1,now());select @@IDENTITY;
            //BLL.SysMag.Login.StrLoginUserID          

            try
            {
                DBA.MySqlDbAccess.ExecNoQuery(System.Data.CommandType.Text,
                   string.Format("update idx_val set val='{0}',createuser={1} where id={2}", strValName, strCreatUID, _strValID));


                bRs = true;
            }
            catch (Exception ex)
            {
                bRs = false;
            }

            return bRs;
        }

        public static bool DelKey(int _nKeyID)
        {
            bool bRs = false;

            string strSqlTmp = @"delete from idx_key where id={0};"
                            + "delete from idx_val where keyid={0};"
                            + "delete from idx_keyval where keyid={0};";
            try
            {

                MySqlDbAccess.ExecNoQuery(CommandType.Text, string.Format(strSqlTmp, _nKeyID));

                bRs = true;
            }
            catch (Exception ex)
            {
                bRs = false;
            }
            return bRs;
        }

        public static bool DelKeyVal(int _nValID)
        {
            bool bRs = false;

            string strSqlTmp = @"delete from idx_val where id={0};"
                            + "delete from idx_keyval where valid={0};";
            try
            {

                MySqlDbAccess.ExecNoQuery(CommandType.Text, string.Format(strSqlTmp, _nValID));

                bRs = true;
            }
            catch (Exception ex)
            {
                bRs = false;
            }
            return bRs;
        }

        public static int getSysKeyCount(string strWhere)
        {
            int nRs = 0;
            try
            {
                if (!strWhere.Trim().Equals(""))
                {
                    strWhere = " where " + strWhere;
                }

                string strSql = string.Format("select count(*) from idx_key {0}", strWhere);
                nRs = Convert.ToInt32(MySqlDbAccess.ExecuteScalar(CommandType.Text, strSql));
            }
            catch (Exception ex)
            {
            }
            return nRs;
        }


        public static DataTable getSysKey(string strWhere, string strOdery, int nPageIdx, int nPageSize)
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

            string strSqlTmp = @"SELECT (@rowIdx:=@rowIdx+1) AS 序号,a.id,a.idx_key as 标引项,b.Lname as 创建人,
                        (SELECT GROUP_CONCAT(val ORDER BY	id ASC SEPARATOR ',') FROM idx_val WHERE keyid = a.id)AS 标引词 ,
                        a.createtime as 创建时间 FROM idx_key a,sys_user b,(SELECT @rowIdx:={2}) c where a.createuser=b.id {0} {1} limit {2},{3}";

            string strSql = string.Format(strSqlTmp, strWhere, strOdery, (nPageIdx - 1) * nPageSize, nPageSize);
            dt = MySqlDbAccess.GetDataTable(CommandType.Text, strSql);
            return dt;
        }


        public static List<IDXVAL> getVal(int _nKeyId)
        {
            List<IDXVAL> idxKeyVal = null;

            MySqlDataContext db = MySqlHelper.DataContext;
            var rs = from tmp in db.IDXVAL
                     where tmp.KeyID == _nKeyId
                     select tmp;
            idxKeyVal = rs.ToList<IDXVAL>();

            return idxKeyVal;
        }

        public static List<IDXKeyVAL> getListKeyVal(string _strSid, int _nZid)
        {
            List<IDXKeyVAL> idxKeyVal = new List<IDXKeyVAL>();
            try
            {
                MySqlDataContext db = MySqlHelper.DataContext;
                var rs = from tmp in db.IDXKeyVAL
                         where tmp.SiD == _strSid && tmp.ZID.Value == _nZid
                         select tmp;
                idxKeyVal = rs.ToList<IDXKeyVAL>();
            }
            catch (Exception ex)
            {
            }

            return idxKeyVal;
        }

        public static Dictionary<int, List<int>> getKeyVal(string _strSid, int _nZid)
        {
            Dictionary<int, List<int>> idxKeyVal = new Dictionary<int, List<int>>();

            MySqlDataContext db = MySqlHelper.DataContext;
            var rs = getListKeyVal(_strSid, _nZid);

            foreach (var item in rs)
            {
                if (!idxKeyVal.Keys.Contains(item.KeyID.Value))
                {
                    idxKeyVal.Add(item.KeyID.Value, new List<int>());
                }
                idxKeyVal[item.KeyID.Value].Add(item.Valid.Value);
            }

            return idxKeyVal;
        }

        public static bool SaveKeyVal(string _strSid, int _nZid, Dictionary<int, List<int>> _dicKeyVal, string strCreatUID)
        {
            bool bRs = false;

            //List<IDXKeyVAL> idxKeyVal = new List<IDXKeyVAL>();
            //foreach (string strItem in _strValues)
            //{
            //    idxKeyVal.Add(new IDXVAL()
            //    {
            //        CreateUser = int.Parse(strCreatUID),
            //        KeyID = int.Parse(strKeyID),
            //        VAL = strItem,
            //        Pid = 0,
            //        AutoIDXContent = "",
            //        CreateTime = DateTime.Now
            //    });
            //}
            //using (MySqlDataContext db = new MySqlDataContext())
            //{

            //    db.IDXVAL.BulkInsertPageSize = 100;
            //    db.IDXVAL.BulkInsert(idxKeyVal);
            //    db.SubmitChanges();
            //}

            return bRs;
        }

        public static bool SaveKeyVal(List<IDXKeyVAL> _idxKeyVal)
        {
            bool bRs = false;

            using (MySqlDataContext db = new MySqlDataContext())
            {
                db.IDXKeyVAL.BulkInsertPageSize = 100;
                db.IDXKeyVAL.BulkInsert(_idxKeyVal);
                db.SubmitChanges();
            }
            bRs = true;
            return bRs;
        }

        public static bool UpdateKeyVal(string _strSid, int _nZid, List<IDXKeyVAL> _idxKeyVal)
        {
            bool bRs = false;

            DBA.MySqlDbAccess.ExecNoQuery(System.Data.CommandType.Text,
               string.Format("delete from  idx_keyval where sid='{0}' and zid={1};", _strSid, _nZid));

            bRs = SaveKeyVal(_idxKeyVal);

            return bRs;
        }
    }
}
