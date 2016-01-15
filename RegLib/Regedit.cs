#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2012 @ CPIC Corp
*	CLR 版本: 2.0.50727.3615
*	文 件 名: Regedit.cs
*	创 建 人: xiwenlei(xiwl)
*	创建日期: 2012-5-31 9:24:13
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
using Microsoft.Win32;

/// <summary>
/// Lib 的摘要说明
/// </summary>
namespace RegLib
{
    /// <summary>
    ///Regedit 的摘要说明
    /// </summary>
    public class Regedit
    {
        public Regedit()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 写序列号
        /// </summary>
        /// <param name="SerialNo"></param>
        /// <returns></returns>
        public static bool WriteRegedit(string _strKey, string _strValue)
        {
            bool bIsRes = false;
            try
            {
                RegistryKey hkml = Registry.LocalMachine;
                RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
                RegistryKey aimdir = software.OpenSubKey("Cnpat", true);
                if (aimdir == null)
                {
                    aimdir = software.CreateSubKey("Cnpat");
                }
                aimdir.SetValue(_strKey, _strValue);

                bIsRes = true;
            }
            catch (Exception ex)
            {
            }
            return bIsRes;
        }


        /// <summary>
        /// 写序列号
        /// </summary>
        /// <param name="SerialNo"></param>
        /// <returns></returns>
        public static string GetRegedit(string _strKey)
        {
            string strRetu = "";
            try
            {
                //2011.4.26 修改为从注册表获取
                RegistryKey hkml = Registry.LocalMachine;
                RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
                RegistryKey aimdir = software.OpenSubKey("Cnpat", true);
                strRetu = aimdir.GetValue(_strKey).ToString();
            }
            catch (Exception ex)
            {

            }
            return strRetu;
        }
    }
}
