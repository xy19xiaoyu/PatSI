#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2015 @ CPIC Corp
*	CLR 版本: 4.0.30319.225
*	文 件 名: PatDetails.cs
*	创 建 人: xiwenlei(xiwenlei)
*	创建日期: 2015/5/7 10:13:07
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

/// <summary>
/// BLL.UIHelper 的摘要说明
/// </summary>
namespace BLL.UIHelper
{
    /// <summary>
    ///PatDetails 的摘要说明
    /// </summary>
    public class PatDetails
    {
        public PatDetails()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static DataTable getBaseData(string _strSid, string _strDbType)
        {
            DataTable dt = null;
            try
            {
                string strIsFiled = MySqlDbAccess.ExecuteScalar(CommandType.Text, string.Format("select cfg_stcolumn_filed from cfg_dbtype where dbty='{0}'", _strDbType)).ToString();

                string strSql = MySqlDbAccess.ExecuteScalar(CommandType.Text, string.Format(@"SELECT GROUP_CONCAT(CONCAT_WS(' as ',col_name,showname) order by id asc separator ',') as fileds 
                                            FROM cfg_stcolumn where tb_name='show_base' and {0}=true", strIsFiled)).ToString();
                dt = MySqlDbAccess.GetDataTable(CommandType.Text, string.Format("select {0} from show_base", strSql));
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        
    }
}
