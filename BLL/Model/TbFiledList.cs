#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2015 @ CPIC Corp
*	CLR 版本: 4.0.30319.225
*	文 件 名: TbFiledList.cs
*	创 建 人: xiwenlei(xiwenlei)
*	创建日期: 2015/4/15 12:25:40
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
using System.Xml.Linq;
using System.IO;

/// <summary>
/// BLL.Model 的摘要说明
/// </summary>
namespace BLL.Model
{
    /// <summary>
    ///TbFiledList 的摘要说明
    /// </summary>
    public class TbFiledList : Dictionary<string, TbFiled_Cfg>
    {
        public Dictionary<string, string> dicTbNames = new Dictionary<string, string>();
        public TbFiledList(string strTbName)
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
            this.strTbName = strTbName;
            dicTbNames.Add(strTbName, "1");
            InitCfg();
        }

        private void Add(string key, TbFiled_Cfg value)
        {
            base.Add(key, value);
        }

        public void Add(TbFiled_Cfg value)
        {
            Add(value.StrTbName + value.StrDbTy + value.StrFiledName, value);
        }

        private void InitCfg()
        {
            string strCfgFile = string.Format(System.Windows.Forms.Application.StartupPath + @"\Cfg\{0}_cfg.xml", strTbName);

            if (File.Exists(strCfgFile))
            {
                XDocument xRoot = XDocument.Parse(File.ReadAllText(strCfgFile));

                var nodes =
                         from el in xRoot.Descendants("Filed")
                         select el;
                foreach (var node in nodes)
                {
                    try
                    {
                        TbFiled_Cfg tbFiledItem = new TbFiled_Cfg(node.Attribute("name").Value, node.Attribute("nameCn").Value, node.Attribute("isShow").Value
                            , node.Attribute("ReadOnly").Value);

                        tbFiledItem.StrDbTy = node.Attribute("dbTy").Value;

                        tbFiledItem.BIsAutoIndexFiled = node.Attribute("isAutoIndex") != null && node.Attribute("isAutoIndex").Value.Trim().Equals("1") ? true : false;

                        tbFiledItem.StrTbName = node.Attribute("tbName") != null && node.Attribute("tbName").Value.Trim() != "" ?
                            node.Attribute("tbName").Value.Trim() : this.strTbName;

                        if (!dicTbNames.Keys.Contains(tbFiledItem.StrTbName))
                        {
                            dicTbNames.Add(tbFiledItem.StrTbName, (dicTbNames.Keys.Count + 1).ToString());
                        }

                        this.Add(tbFiledItem);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }


        private string strTbName = "";

        public string StrTbName
        {
            get { return strTbName; }
            set { strTbName = value; }
        }

        public string ToString()
        {
            string strRs = "";
            foreach (TbFiled_Cfg item in this.Values)
            {
                strRs += string.Format(",{0} as {1}", item.StrFiledName, item.StrFiledNameCn);
            }
            strRs = strRs.TrimStart(',');
            return strRs;
        }

        public string ToShowString(string strDbTy)
        {

            string strRs = "";
            foreach (TbFiled_Cfg item in this.Values)
            {
                if (item.IsShow && (item.StrDbTy == "" || item.StrDbTy.Contains(strDbTy)))
                {
                    strRs += string.Format(",{0} as {1}", item.StrFiledName, item.StrFiledNameCn);
                }
            }
            strRs = strRs.TrimStart(',');
            return strRs;
        }

        public string ToFiledFromString(string strDbTy, string strFromSql, string strWhere)
        {
            string strRs = "";

            List<string> lstUserTb = new List<string>();
            foreach (TbFiled_Cfg item in this.Values)
            {
                if (item.IsShow && (item.StrDbTy == "" || item.StrDbTy.Contains(strDbTy)))
                {
                    strRs += item.StrTbName.Equals("-") || (item.StrFiledName.StartsWith("(") && item.StrFiledName.EndsWith(")")) ?
                        string.Format(",{0} as '{1}'", item.StrFiledName, item.StrFiledNameCn) :
                        string.Format(",a{2}.{0} as '{1}'", item.StrFiledName, item.StrFiledNameCn, dicTbNames[item.StrTbName]);

                    if (!item.StrTbName.Equals("-") && !lstUserTb.Contains(item.StrTbName))
                    {
                        lstUserTb.Add(item.StrTbName);
                    }
                }
            }
            strRs = strRs.TrimStart(',') + " from ";
            foreach (string strItem in lstUserTb)
            {
                strRs += strItem + " a" + dicTbNames[strItem] + ",";
            }
            strRs = (strRs + strFromSql).TrimEnd(',');

            if (lstUserTb.Count > 1)
            {
                strRs += string.IsNullOrEmpty(strWhere) ? " where 1=1 " : strWhere;
                for (int nIdx = 1; nIdx < lstUserTb.Count; nIdx++)
                {
                    strRs += string.Format(" and a{0}.ztid=a{1}.ztid and a{0}.sid=a{1}.sid", dicTbNames[lstUserTb[0]], dicTbNames[lstUserTb[nIdx]]);
                }
            }
            else
            {
                strRs += strWhere;
            }

            return strRs;
        }

    }
}
