using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Win32;

namespace RegLib
{
    public class reginfo
    {
        private static DateTime _FirstUsedTime;
        private static string _DiskNumber;
        private static string _SerialNo;
        private static string xyFile;

        //增加当前版本年和ipc以及是否过滤,这里的年有两个，一个是程序配置中的年份版本，还有一个是从注册信息中取得的年度，需要对比校验
        /// <summary>
        /// 程序中的配置的年份
        /// </summary>
        private static string _Cyear;

        /// <summary>
        /// 程序中的配置的年份
        /// </summary>
        public static string Cyear
        {
            get { return _Cyear; }
            set { _Cyear = value; }
        }

        /// <summary>
        /// 机器的授权码
        /// </summary>
        private static string strPC_SQM;

        /// <summary>
        /// 机器注册表中的授权码
        /// </summary>
        public static string StrPC_SQM
        {
            get { return reginfo.strPC_SQM; }
            set { reginfo.strPC_SQM = value; }
        }

        /// <summary>
        /// 程序中配置的国别
        /// </summary>
        private static string strCC;

        /// <summary>
        /// 程序中配置的国别
        /// </summary>
        public static string StrLocCC
        {
            get { return strCC; }
            set { strCC = value; }
        }

        private static string _Vyear;
        private static string _Vipcs;
        private static string _Visfiltered;
        ///用户的当前选择  0过滤后数据 1垃圾数据 
        private static string _UserSelectedData = "0";
        private static string _UserSelectedDataSource;

        /// <summary>
        /// 注册表中记录的年份
        /// </summary>
        public static string VYear
        {
            get { return _Vyear; }
            set { _Vyear = value; }
        }

        public static string VIpcs
        {
            get { return _Vipcs; }
            set { _Vipcs = value; }
        }

        /// <summary>
        /// 0 是没有垃圾数据的权限
        /// 1 是垃圾数据
        /// ==1 弹对话框
        /// 用户选A-H  UserSelected ==0
        /// 用户选垃圾数据 UserSelected ==1
        /// </summary>
        public static string IsFiltered
        {
            get { return _Visfiltered; }
            set { _Visfiltered = value; }
        }
        /// <summary>
        /// 用户的当前选择选择的数据库；多项以;分隔; 如[US2005;EP2005];
        /// </summary>
        public static string UserSelectedDataSource
        {
            get
            {
                if (string.IsNullOrEmpty(_UserSelectedDataSource))
                {
                    _UserSelectedDataSource = "";
                }
                return _UserSelectedDataSource;
            }
            set { _UserSelectedDataSource = value; }
        }


        /// <summary>
        /// 用户的当前数据选择  [0]可利用数据 [1]被清洗掉数据 [2]潜在壁垒数据 [9]全数据
        /// </summary>
        public static string UserSelectedData
        {
            get
            {
                if (_UserSelectedData == string.Empty)
                {
                    _UserSelectedData = "0";
                }
                return _UserSelectedData;
            }
            set { _UserSelectedData = value; }
        }



        //增加使用年份和最后使用时间
        private static int _UsedYear;
        private static DateTime _LastUsedTime;

        /// <summary>
        /// 一次授权使用期限,2015版前为1年，15版为2年
        /// </summary>
        private static int nRegDay = 365 * 9;

        public static int UsedYear
        {
            get { return _UsedYear; }
            set { _UsedYear = value; }
        }

        public static DateTime LastUsedTime
        {
            get { return _LastUsedTime; }
            set { _LastUsedTime = value > _LastUsedTime ? value : _LastUsedTime; }
        }

        public static DateTime FirstUsedTime
        {
            get { return _FirstUsedTime; }
            set { _FirstUsedTime = value; }
        }

        public static string SerialNo
        {
            get { return _SerialNo; }
            set { _SerialNo = value; }
        }

        /// <summary>
        /// 机器码(未加密前)
        /// </summary>
        public static string DiskNumber
        {
            get { return _DiskNumber; }
            set { _DiskNumber = value; }
        }

        /// <summary>
        /// 2014年起通过网络授权时，附带国家与年代信息
        /// </summary>
        private static string _DiskNumber_2014;

        /// <summary>
        /// 2014年起通过网络授权时，附带国家与年代信息
        /// </summary>
        public static string DiskNumber_2014
        {
            get { return reginfo._DiskNumber_2014; }
        }

        private static string strSqEndDate = "";

        /// <summary>
        /// 授权截止时间
        /// </summary>
        public static string StrSqEndDate
        {
            get { return strSqEndDate; }
            set { strSqEndDate = value; }
        }

        private static DateTime dtCreatDate;

        public static DateTime DtCreatDate
        {
            get { return reginfo.dtCreatDate; }
            set { reginfo.dtCreatDate = value; }
        }
        private static DateTime dtSqEndDate;

        public static DateTime DtSqEndDate
        {
            get { return reginfo.dtSqEndDate; }
            set { reginfo.dtSqEndDate = value; }
        }



        public static void ReadRegInfo()
        {
            //2011.4.26 修改为从注册表获取
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
            RegistryKey aimdir = software.OpenSubKey("IPSI", true);

            string tmp;
            string[] arytmp;

            //如果已经有使用过软件，通过注册表读取第一次使用时间+硬盘序列号+序列号
            if (aimdir != null && aimdir.GetValue("PatSI") != null)
            {
                try
                {
                    tmp = aimdir.GetValue("PatSI").ToString();
                    tmp = encryptDecryptStr(tmp);


                    //如果仅用|分割，在本身就含有|字符的时候可能会出现问题
                    string[] sep = new string[] { "@|#" };
                    arytmp = tmp.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                    //arytmp = tmp.Split('|');

                    if (arytmp.Length >= 3)
                    {
                        //第一次使用时间
                        _FirstUsedTime = Convert.ToDateTime(arytmp[0]);
                        //最后一次使用时间
                        _LastUsedTime = Convert.ToDateTime(arytmp[1]);
                        //序列号
                        _SerialNo = arytmp[2];

                        #region 取生成时间和机器码

                        string strCreatDate = String.Empty;

                        strCreatDate = _SerialNo.Substring(_SerialNo.Length - 20, 10);
                        StrSqEndDate = _SerialNo.Substring(_SerialNo.Length - 10);
                        StrPC_SQM = _SerialNo.Substring(0, _SerialNo.Length - 20);

                        #endregion
                        dtCreatDate = DateTime.Parse(strCreatDate);
                        dtSqEndDate = DateTime.Parse(StrSqEndDate).AddDays(1);

                        //DateTime dtWebHost

                        ////防止调整客户端时间，造成一直不过期的现象，要求重新注册
                        //if (!(DateTime.Now > _LastUsedTime && _LastUsedTime > _FirstUsedTime))
                        //{
                        //    #region 采取策略是强制过期还是保留使用时间差？

                        //    //使用强制过期,不满年直接变为满年
                        //    TimeSpan ts = (_LastUsedTime - _FirstUsedTime).Duration();
                        //    _FirstUsedTime = DateTime.Now.AddDays(-(ts.Days / nRegDay + 1) * nRegDay);
                        //    _LastUsedTime = DateTime.Now;

                        //    #endregion
                        //}

                    }
                    else
                    {   //针对仅有第一次试用时间，无注册码的情况
                        //_FirstUsedTime = Convert.ToDateTime(tmp);

                        //第一次使用时间
                        _FirstUsedTime = Convert.ToDateTime(arytmp[0]);
                        //最后一次使用时间
                        _LastUsedTime = Convert.ToDateTime(arytmp[1]);
                    }
                }
                catch (Exception)
                {
                    //如果注册表有键无值或者无法被正常处理，说明注册信息被破坏，直接将_FirstUsedTime改为随机年，造成使用年份变化，要求重新注册
                    Random r = new Random();
                    int i = r.Next(2, 9);
                    _FirstUsedTime = DateTime.Now.AddDays(-nRegDay * i);
                    _LastUsedTime = DateTime.Now;
                    _SerialNo = String.Empty;
                }
            }
            else
            {
                //如果没有键但存在检索历史记录文件，说明删除过注册表信息，需要重新注册
                string shFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SearchHistory.txt");
                if (false && File.Exists(shFile))
                {
                    //将第一次使用时间置为检索记录文件创建时间，最后一次使用时间置为检索记录文件最后访问时间
                    FileInfo fi = new FileInfo(shFile);
                    _FirstUsedTime = fi.CreationTime;
                    _LastUsedTime = fi.LastAccessTime;
                    _SerialNo = String.Empty;
                }
                //否则就是第一次使用
                else
                {
                    _FirstUsedTime = DateTime.Now;
                    _LastUsedTime = DateTime.Now;
                    _SerialNo = String.Empty;
                }
            }

            //使用年份
            _UsedYear = ((int)(_LastUsedTime - _FirstUsedTime).TotalDays) / nRegDay + 1;
            //读取网卡Mac地址并加上使用年份
            _DiskNumber = _UsedYear + RegUtility.GetCpuNumber();


            //string strTmpYY = ((char)((Convert.ToInt32(_Cyear, 10) - 2001) + 'A')).ToString();
            _DiskNumber_2014 = _DiskNumber; // strCC + strTmpYY + _DiskNumber;

        }

        static reginfo()
        {
            _SerialNo = "";
            ReadRegInfo();
            //重新保存，实际上是为了每次更新最后访问时间
            WriteSerialNo(_SerialNo);
        }



        /// <summary>
        /// 写序列号
        /// </summary>
        /// <param name="SerialNo"></param>
        /// <returns></returns>
        public static void WriteSerialNo(string SerialNo)
        {
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
            RegistryKey aimdir = software.OpenSubKey("IPSI", true);
            if (aimdir == null)
            {
                aimdir = software.CreateSubKey("IPSI");
            }
            LastUsedTime = DateTime.Now;
            aimdir.SetValue("PatSI", encryptDecryptStr(_FirstUsedTime.ToString("yyyy-MM-dd HH:mm:ss") + "@|#" + _LastUsedTime.ToString("yyyy-MM-dd HH:mm:ss") + "@|#" + SerialNo));

        }

        /// <summary>
        /// 得到试用剩余天数
        /// </summary>
        /// <returns></returns>
        public int GetDays()
        {
            return GetDays(30);
        }

        /// <summary>
        /// 距授权码有效期剩余天数
        /// </summary>
        /// <param name="_nDay"></param>
        /// <returns></returns>
        public int GetDays(int _nDay)
        {
            return (int)(FirstUsedTime.AddDays(_nDay) - DateTime.Now).TotalDays;
        }


        /// <summary>
        /// 判断是否注册
        /// </summary>
        /// <returns></returns>
        public bool CheckRegedit(DateTime dtWebHost, bool bIsWebHost)
        {
            try
            {
                string regCode = RegUtility.GetRegNumber(_DiskNumber);
                //if (_SerialNo == "I173387EFIE8" || (!String.IsNullOrEmpty(regCode) && _SerialNo.Equals(regCode)))
                //改为校验前面的硬件ID注册码以及配置年和注册码取出来的年部分（避免买了一年的多IPC版本，把注册码复制到多年用）
                if (_SerialNo == "I1733817EEIE8JFF0")
                {
                    return true;
                }
                else if ((!String.IsNullOrEmpty(regCode) && StrPC_SQM.Equals(regCode)))
                {
                    bool bRs = true;
                    if (bIsWebHost)
                    {
                        bRs = dtWebHost < DtSqEndDate ? true : false;
                    }
                    else
                    {
                        //防止调整客户端时间，造成一直不过期的现象，要求重新注册
                        if (!(DateTime.Now >= _LastUsedTime && _LastUsedTime > _FirstUsedTime))
                        {
                            #region 采取策略是强制过期还是保留使用时间差？
                            ////使用强制过期,不满年直接变为满年
                            //TimeSpan ts = (_LastUsedTime - _FirstUsedTime).Duration();
                            //_FirstUsedTime = DateTime.Now.AddDays(-(ts.Days / nRegDay + 1) * nRegDay);
                            //_LastUsedTime = DateTime.Now;
                            bRs = false;
                            #endregion
                        }
                        else
                        {
                            bRs = DateTime.Now < DtSqEndDate ? true : false;
                        }
                    }
                    return bRs;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Encode(string content)
        {
            return content;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string DeCode(string content)
        {
            return content;
        }

        #region 字符串异或加密
        public static string encryptDecryptStr(string p)
        {
            byte key = 123;
            byte[] bs = Encoding.Default.GetBytes(p);
            for (int i = 0; i < bs.Length; i++)
            {
                bs[i] = (byte)(bs[i] ^ key);
            }
            return Encoding.Default.GetString(bs);
        }
        #endregion
    }
}
