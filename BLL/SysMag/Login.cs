#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2015 @ CPIC Corp
*	CLR 版本: 4.0.30319.225
*	文 件 名: Login.cs
*	创 建 人: xiwenlei(xiwenlei)
*	创建日期: 2015/5/6 17:02:21
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
using System.Data.SqlClient;
using DBA;
using MySql.Data.MySqlClient;

/// <summary>
/// BLL.SysMag 的摘要说明
/// </summary>
namespace BLL.SysMag
{
    /// <summary>
    ///Login 的摘要说明
    /// </summary>
    public class Login
    {
        private static string strLoginLname = "";

        /// <summary>
        /// 当前登录用户名
        /// </summary>
        public static string StrLoginLname
        {
            get { return strLoginLname; }
            set { strLoginLname = value; }
        }
        private static string strLoginSname = "";

        /// <summary>
        /// 当前登录用户显示名称
        /// </summary>
        public static string StrLoginSname
        {
            get { return strLoginSname; }
            set { strLoginSname = value; }
        }
        private static string strLoginUserID = "";

        /// <summary>
        /// 当前登录用户ID
        /// </summary>
        public static string StrLoginUserID
        {
            get { return strLoginUserID; }
            set { strLoginUserID = value; }
        }

        //初始化cbo控件
        public static DataTable Select_YongHu(string _strLname)
        {
            string strSql = "select * from sys_user where Lname=@Lname";
            //准备调用的对应参数
            MySqlParameter[] SQlCMDpas ={                                            
             new MySqlParameter("@Lname",MySqlDbType.String),
                                        };
            SQlCMDpas[0].Value = _strLname;
            
            //实例化DAL层对应的类，调用DAL类，传入参数
            DataTable dt = MySqlDbAccess.GetDataTable(CommandType.Text, strSql,SQlCMDpas); ;
            return dt;
        }

        public static DataTable Select_YongHu_MiMa(string _strLname, string _strPw)
        {
            string strSql = "select * from sys_user where Lname=@Lname and pw=@pw";
            //准备调用DAL层方法的对应参数
            MySqlParameter[] SQlCMDpas ={                                            
             new MySqlParameter("@Lname",MySqlDbType.String),
             new MySqlParameter("@pw",MySqlDbType.String)
                                        };
            SQlCMDpas[0].Value = _strLname;
            SQlCMDpas[1].Value = _strPw;
            //实例化DAL层对应的类，调用DAL类，传入参数
            DataTable dt = MySqlDbAccess.GetDataTable(CommandType.Text, strSql, SQlCMDpas); ;
            return dt;
        }
    }
}
