#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2010 @ CPIC Corp
*	CLR 版本: 2.0.50727.3603
*	文 件 名: RegUtility.cs
*	创 建 人: xiwenlei(xiwl)
*	创建日期: 2010-5-20 15:08:18
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
using System.Web;
using System.Management;
using Microsoft.Win32;
using System.Text;

/// <summary>
///获取硬盘号和CPU号,根据号生成注册码
/// </summary>
public class RegUtility
{
    /// <summary>
    /// 构造函数逻辑
    /// </summary>
    public RegUtility()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //      
    }

    /// <summary>
    /// 静态构造函数
    /// </summary>
    static RegUtility()
    {
        //把机器码存入数组中
        SetIntCode();//初始化127位数组
    }

    #region 获取机器号码
    /// <summary>
    /// 获取机器号码
    /// </summary>
    /// <returns></returns>
    public static string GetDiskCPUNumber()
    {
        string strDiskCPUNumber = "";
        // 取得设备硬盘的卷标号
        string strDisk = null;
        ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
        ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
        disk.Get();
        strDisk = disk.GetPropertyValue("VolumeSerialNumber").ToString();
        //获得CPU的序列号
        string strCPU = null;
        ManagementClass myCpu = new ManagementClass("win32_Processor");
        ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
        foreach (ManagementObject myObject in myCpuConnection)
        {
            strCPU = myObject.Properties["Processorid"].Value.ToString();
            break;
        }
        return strDiskCPUNumber = strCPU + strDisk;
    }

    public static string GetDiskNumber()
    {
        // 取得设备硬盘的序列号
        string strDisk = null;
        ManagementClass cimobject = new ManagementClass("win32_diskdrive");
        ManagementObjectCollection moc = cimobject.GetInstances();
        foreach (ManagementObject myObject in moc)
        {
            strDisk = myObject.Properties["model"].Value.ToString();
            break;
        }

        return strDisk;
    }
    public static string GetCpuNumber()
    {
        ////获得CPU的序列号
        //string strCPU = null;
        //ManagementClass myCpu = new ManagementClass("win32_Processor");
        //ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
        //foreach (ManagementObject myObject in myCpuConnection)
        //{
        //    strCPU = myObject.Properties["Processorid"].Value.ToString();
        //    break;
        //}
        //return strCPU;

        //2011.4.26ywy修改，用取网卡号替代取CPU号，后者很慢还会重复
        try
        {
            string stringMAC = "";
            ManagementClass MC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection MOC = MC.GetInstances();

            foreach (ManagementObject MO in MOC)
            {
                //if ((bool)MO["IPEnabled"] == true)
                //{
                stringMAC = MO["MACAddress"].ToString().Replace(":", "");
                break;

                //}
            }
            return stringMAC;
        }
        catch
        {
            return GetDiskCPUNumber();
        }
    }
    #endregion

    

    //private static int[] intCode = new int[127];//用于存密钥       
    //private static int[] intNumber = new int[25];//用于存机器码的Ascii值
    //private static char[] Charcode = new char[25];//存储机器码字

    private static int[] intCode = new int[127];//用于存密钥       
    private static int[] intNumber = new int[50];//用于存机器码的Ascii值
    private static char[] Charcode = new char[50];//存储机器码字

    #region ======算法=====
    private static void SetIntCode()//给数组赋值个小于10的随机数
    {
        #region 加密算法
        try
        {
            //Random ra = new Random();//随机生成数
            for (int i = 1; i < intCode.Length; i++)
            {
                //intCode[i] = ra.Next(0, 9);
                intCode[i] = i % 7;
            }
        }
        catch (Exception ee)
        {
            //MessageBox.Show(ee.Message);
        }
        #endregion
    }
    #endregion

    //问题，这个算法当参数如BFEBFBFF000006F6BCFDB288，再多任何一位就算不出来了，是由于上面的intNumber 和intcode都只定义了25位
    #region 生成注册码
    /// <summary>
    /// 获取注册码
    /// </summary>
    /// <param name="strDiskAndCPUNumber">硬盘CPU号</param>
    /// <returns></returns>
    public static string GetRegNumber(string strDiskAndCPUNumber)
    {
        string strLoginNumber = "";
        try
        {
            ////把机器码存入数组中
            //SetIntCode();//初始化127位数组
            for (int i = 1; i < strDiskAndCPUNumber.Length + 1; i++)//把机器码存入数组中
            {
                if (i > strDiskAndCPUNumber.Length)
                {
                    break;
                }
                Charcode[i] = Convert.ToChar(strDiskAndCPUNumber.Substring(i - 1, 1));
            }//
            for (int j = 1; j < intNumber.Length; j++)//把字符的ASCII值存入一个整数组中。
            {
                intNumber[j] = intCode[Convert.ToInt32(Charcode[j])] + Convert.ToInt32(Charcode[j]);
            }
            string strAsciiName = "";//用于存储机器码

            # region 反序
            //for (int j = strDiskAndCPUNumber.Length; j > 0; j--)
            //{
            //    //MessageBox.Show((Convert.ToChar(intNumber[j])).ToString());
            //    if (intNumber[j] >= 48 && intNumber[j] <= 57)//判断字符ASCII值是否0－9之间
            //    {
            //        strAsciiName += Convert.ToChar(intNumber[j]).ToString();
            //    }
            //    else if (intNumber[j] >= 65 && intNumber[j] <= 90)//判断字符ASCII值是否A－Z之间
            //    {
            //        strAsciiName += Convert.ToChar(intNumber[j]).ToString();
            //    }
            //    else if (intNumber[j] >= 97 && intNumber[j] <= 122)//判断字符ASCII值是否a－z之间
            //    {
            //        strAsciiName += Convert.ToChar(intNumber[j]).ToString();
            //    }
            //    else//判断字符ASCII值不在以上范围内
            //    {
            //        if (intNumber[j] > 122)//判断字符ASCII值是否大于z
            //        { strAsciiName += Convert.ToChar(intNumber[j] - 10).ToString(); }
            //        else if (intNumber[j] > 8)
            //        {
            //            strAsciiName += Convert.ToChar(intNumber[j] - 9).ToString();
            //        }
            //        else
            //        {
            //            strAsciiName += Convert.ToChar(intNumber[j]).ToString();
            //        }
            //    }
            //}
            #endregion

            # region 反序中间添加
            for (int j = strDiskAndCPUNumber.Length; j > 0; j--)
            {
                //MessageBox.Show((Convert.ToChar(intNumber[j])).ToString());
                if (intNumber[j] >= 48 && intNumber[j] <= 57)//判断字符ASCII值是否0－9之间
                {
                    strAsciiName = strAsciiName.Insert(strAsciiName.Length / 2, Convert.ToChar(intNumber[j]).ToString());
                }
                else if (intNumber[j] >= 65 && intNumber[j] <= 90)//判断字符ASCII值是否A－Z之间
                {
                    strAsciiName = strAsciiName.Insert(strAsciiName.Length / 2, Convert.ToChar(intNumber[j]).ToString());
                }
                else if (intNumber[j] >= 97 && intNumber[j] <= 122)//判断字符ASCII值是否a－z之间
                {
                    strAsciiName = strAsciiName.Insert(strAsciiName.Length / 2, Convert.ToChar(intNumber[j]).ToString());
                }
                else//判断字符ASCII值不在以上范围内
                {
                    if (intNumber[j] > 122)//判断字符ASCII值是否大于z
                    { strAsciiName += Convert.ToChar(intNumber[j] - 10).ToString(); }
                    else if (intNumber[j] > 8)
                    {
                        strAsciiName = strAsciiName.Insert(strAsciiName.Length / 2, Convert.ToChar(intNumber[j] - 9).ToString());
                    }
                    else
                    {
                        strAsciiName = strAsciiName.Insert(strAsciiName.Length / 2, Convert.ToChar(intNumber[j]).ToString());
                    }
                }
            }
            #endregion

            //tbd:可以再进行反序


            strLoginNumber = strAsciiName;//得到注册码     
        }
        catch (Exception ex) { }
        return strLoginNumber;
    }
    #endregion

    # region 字符串反排
    #endregion

   
}
