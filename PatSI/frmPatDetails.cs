using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL;
using BLL.UIHelper;
using BLL.Index;
using DAL;

namespace PatSI
{
    public partial class frmPatDetails : Form
    {
        private string strSidKey = "";
        private string strDbType = "";
        private DataTable dtSource = null;
        private int nCuurentIdx = -1;
        private int nRsCount = 0;
        private string strZtId = "";
        public frmPatDetails(string _strDbType, DataTable _dtSource, int _nIdx, string _strZid)
        {
            InitializeComponent();
            //strSidKey = _strSidKey;
            strDbType = _strDbType;
            nCuurentIdx = _nIdx;
            dtSource = _dtSource;
            strZtId = _strZid;
            nRsCount = dtSource.Rows.Count;
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            InitBaseData(nCuurentIdx - 1);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            InitBaseData(nCuurentIdx + 1);
        }

        private void frmPatDetails_Load(object sender, EventArgs e)
        {
            InitTag();
            InitBaseData(nCuurentIdx);
        }

        private void InitTag()
        {


            switch (strDbType)
            {
                case "CPRS":
                    this.Height = 696;
                    this.Width = 771;
                    tabEnBaseInfo.Parent = null;
                    tabEnAutoIdxInfor.Parent = null;

                    #region Cn自动标引
                    txbBySl.Tag = "byz_sum|int";
                    txbCLzs.Tag = "cls_char_sum|int";
                    txbGkn.Tag = "pdy|int";
                    txbGknc.Tag = "pdy_def|int";
                    cbxSfgwlh.Tag = "isguowai|int";
                    cbxSfgz.Tag = "isgongzhi|int";
                    cbxSfhzsq.Tag = "ishezuo|int";
                    txbGj.Tag = "gj|str";
                    txbSheng.Tag = "sheng|str";
                    txbSheng2.Tag = "sheng1|str";
                    txbQx.Tag = "quxian|str";
                    txbShi.Tag = "shi|str";
                    txbSqn.Tag = "gdy|int";
                    txbSqn1.Tag = "ady|int";
                    txbSqnc.Tag = "gdy_def|int";
                    txbYzSl.Tag = "yz_sum|int";
                    txbZcpc.Tag = "f_cpc|str";
                    txbZfmr.Tag = "f_in|str";
                    txbZIpc.Tag = "f_ipc|str";
                    txbZj.Tag = "zhou|str";
                    txbZlLx.Tag = "type|str";
                    txbZllx2.Tag = "type1|str";
                    txbZlnl.Tag = "age|int";
                    txbZsqr.Tag = "f_pa|str";
                    txbZsqrLx.Tag = "f_pa_type|str";
                    txbZzflzt.Tag = "lg|str";
                    txbCpcSl.Tag = "cpc_sum|int";
                    txbIpcSl.Tag = "ipc_sum|int";
                    txbfmrSl.Tag = "in_sum|int";
                    txbSqrSl.Tag = "pa_sum|int";
                    txbJjqq.Tag = "quyu|str";
                    txbFljzn.Tag = "lg|str";
                    #endregion
                    break;
                case "WPI":
                case "EPODOC":
                    this.Height = 621;
                    this.Width = 771;
                    tabCprsBaseInfor.Parent = null;
                    tabAutoIdxInfor.Parent = null;

                    #region En自动标引
                    txbEnAdy.Tag = "ady|int";
                    txbEnPdY.Tag = "pdy|int";
                    txbEnPrY.Tag = "";
                    txbEnOldPrY.Tag = "opdy|int";
                    txbEnPnYC.Tag = "pdy_def|int";
                    txbEnGnYC.Tag = "gdy_def|int";
                    txbEnFmlNm.Tag = "fml_sum|int";
                    txbEnCcNm.Tag = "gj_sum|int";
                    txbEnYzNm.Tag = "yz_sum|int";
                    txbEnPA1.Tag = "f_pa|str";
                    txbEnPaNm.Tag = "pa_sum|int";
                    cbxEnSfhzSq.Tag = "ishezuo|int";
                    txbEnIn1.Tag = "f_in|str";
                    txbEnInNm.Tag = "in_sum|int";
                    txbEnZj.Tag = "zhou|str";
                    cbxEnSf3J.Tag = "issanju|int";
                    cbxEnSf5j.Tag = "iswuju|int";
                    txbEnOldPrC.Tag = "oprc|str";
                    txbEnIpc1.Tag = "f_ipc|str";
                    txbEnCpc1.Tag = "f_cpc|str";
                    txbEnDmc1.Tag = "f_dmc|str";
                    txbEnIpcNm.Tag = "ipc_sum|int";
                    txbEnCpcNm.Tag = "cpc_sum|int";
                    txbEnDmcNm.Tag = "dmc_sum|int";
                    txbEnEndLg.Tag = "lg|str";
                    txbEnZlNl.Tag = "age|int";
                    //cbxEnGzjs.Tag = "isgongzhi|int";
                    //txbEnZlWcY.Tag = "";

                    txbEnAp_gjs.Tag = "ap_gjs|str";
                    txbEnPn_gjs.Tag = "Pn_gjs|str";

                    #endregion
                    break;
            }

            #region  人工标引控制初始化
            //序号,a.id,a.idx_key as 标引项,b.Lname as 创建人, 标引词
            DataTable dt = IdexItemMag.getSysKey("", "", 1, int.MaxValue);

            tabRgby.Controls.Clear();

            int nPoint_Y_Step = 73;
            int nTmp = 12 - nPoint_Y_Step;
            foreach (DataRow drItem in dt.Rows)
            {
                GroupBox grb = new GroupBox();
                grb.Name = drItem["id"].ToString();
                grb.Text = drItem["标引项"].ToString();
                grb.Height = 61;
                grb.Width = 736;

                tabRgby.Controls.Add(grb);

                nTmp += nPoint_Y_Step;
                grb.Location = new Point(8, nTmp);

                CheckedListBox chkLbx = new CheckedListBox();

                grb.Controls.Add(chkLbx);
                chkLbx.Dock = DockStyle.Fill;
                chkLbx.MultiColumn = true;
                chkLbx.HorizontalScrollbar = true;

                List<DAL.IDXVAL> lisVal = IdexItemMag.getVal(int.Parse(drItem["id"].ToString()));

                chkLbx.DataSource = lisVal;
                chkLbx.ValueMember = "id";
                chkLbx.DisplayMember = "val";
            #endregion
            }
        }

        private void InitBaseData(int _nIdx)
        {
            if (nRsCount == 0)
                return;

            if (_nIdx > nRsCount)
            {
                _nIdx = nRsCount;
            }
            else if (_nIdx < 1)
            {
                _nIdx = 1;
            }

            btnPre.Enabled = true;
            btnNext.Enabled = true;

            if (_nIdx == 1)
            {
                btnPre.Enabled = false;
            }

            if (_nIdx == nRsCount)
            {
                btnNext.Enabled = false;
            }
            nCuurentIdx = _nIdx;
            txbDis.Text = string.Format("{0}/{1}", nCuurentIdx, nRsCount);

            switch (strDbType)
            {
                case "CPRS":
                    InitCnBaseData();
                    break;
                case "WPI":
                case "EPODOC":
                    InitEnBaseData();
                    break;
            }
        }

        private void InitCnBaseData()
        {
            string strSid = dtSource.Rows[nCuurentIdx - 1]["sid"].ToString();
            strSidKey = strSid;
            DataTable dt = PtDataHelper.getPtData(string.Format("sid='{0}' and ztid={1}", strSid, strZtId), "", "show_base");
            //DataTable dt = PatDetails.getBaseData(strSidKey, strDbType);
            if (dt.Rows.Count < 1)
            {
                return;
            }
            txbAB.Text = dt.Rows[0]["abs"].ToString();
            txbAD.Text = dt.Rows[0]["ad"].ToString();
            txbAG.Text = dt.Rows[0]["dljg"].ToString();
            txbAN.Text = dt.Rows[0]["an"].ToString();
            txbAT.Text = dt.Rows[0]["dlr"].ToString();
            txbCO.Text = dt.Rows[0]["sheng"].ToString();
            txbDZ.Text = dt.Rows[0]["addr"].ToString();
            txbGD.Text = dt.Rows[0]["gd"].ToString();
            txbGN.Text = dt.Rows[0]["gn"].ToString();
            txbIC.Text = dt.Rows[0]["ipc"].ToString();
            txbIN.Text = dt.Rows[0]["iv"].ToString();
            txbMC.Text = "";
            txbPA.Text = dt.Rows[0]["pa"].ToString();
            txbPR.Text = dt.Rows[0]["cpy"].ToString();
            txbPD.Text = dt.Rows[0]["pd"].ToString();
            txbPN.Text = dt.Rows[0]["pn"].ToString();
            txbTI.Text = dt.Rows[0]["title"].ToString();
            txbCl1.Text = dt.Rows[0]["clm"].ToString();

            InitByCnData();
        }

        private void InitEnBaseData()
        {
            string strSid = dtSource.Rows[nCuurentIdx - 1]["sid"].ToString();
            strSidKey = strSid;
            string strSqlFileds = @"ipc,cpc,dmc,cpy,pa,iv,addr,pr,opd,abs,title,
                                    (select GROUP_CONCAT(an ORDER BY sort ASC SEPARATOR ';') FROM  st_ans where sid=a.sid) as an,
                                    (select GROUP_CONCAT(ad ORDER BY sort ASC SEPARATOR ';') FROM  st_ans where sid=a.sid) as ad,
                                    (select GROUP_CONCAT(pn ORDER BY sort ASC SEPARATOR ';') FROM  st_pns where sid=a.sid) as pn,
                                    (select GROUP_CONCAT(pd ORDER BY sort ASC SEPARATOR ';') FROM  st_pns where sid=a.sid) as pd ";
            if (strDbType.Equals("EPODOC"))
            {
                strSqlFileds = @"ipc,cpc,dmc,cpy,pa,iv,addr,pr,opd,abs,title,pd,
                                    (select an  FROM  st_dt where ztid=a.ztid and sid=a.sid  limit 0,1) as an,
                                    (select ad  FROM  st_dt where ztid=a.ztid and sid=a.sid  limit 0,1) as ad,
                                    (select pn  FROM  st_dt where ztid=a.ztid and sid=a.sid  limit 0,1) as pn ";
            }

            DataTable dt = PtDataHelper.getPtData(strSqlFileds, string.Format("sid='{0}' and ztid={1}", strSid, strZtId), "", "show_base a");
            //DataTable dt = PatDetails.getBaseData(strSidKey, strDbType);
            if (dt.Rows.Count < 1)
            {
                return;
            }
            txbEnTi.Text = dt.Rows[0]["title"].ToString();
            txbEnAN.Text = dt.Rows[0]["an"].ToString();
            txbEnAD.Text = dt.Rows[0]["ad"].ToString();
            txbEnPb.Text = dt.Rows[0]["pn"].ToString();
            txbEnPD.Text = dt.Rows[0]["pd"].ToString();
            txbEnIpc.Text = dt.Rows[0]["ipc"].ToString();
            txbEnCpc.Text = dt.Rows[0]["cpc"].ToString();
            txbEnDmc.Text = dt.Rows[0]["dmc"].ToString();
            txbEnCpy.Text = dt.Rows[0]["cpy"].ToString();
            txbEnPA.Text = dt.Rows[0]["pa"].ToString();
            txbEnIN.Text = dt.Rows[0]["iv"].ToString();
            txbEnAddres.Text = dt.Rows[0]["addr"].ToString();
            txbEnPR.Text = dt.Rows[0]["pr"].ToString();
            txbEnOPD.Text = dt.Rows[0]["opd"].ToString();
            txbEnAB.Text = dt.Rows[0]["abs"].ToString().Replace("NOVELTY :", "");

            InitByEnData();
        }

        private void InitByCnData()
        {
            string strSid = dtSource.Rows[nCuurentIdx - 1]["sid"].ToString();
            strSidKey = strSid;
            DataTable dt = PtDataHelper.getPtData(string.Format("sid='{0}' and ztid={1}", strSid, strZtId), "", "st_dt");
            if (dt.Rows.Count < 1)
            {
                btnUpdate.Tag = "";
                return;
            }
            btnUpdate.Tag = dt.Rows[0]["id"].ToString();

            txbBySl.Text = dt.Rows[0]["byz_sum"].ToString();
            txbCLzs.Text = dt.Rows[0]["cls_char_sum"].ToString();

            txbGkn.Text = dt.Rows[0]["pdy"].ToString();
            txbGknc.Text = dt.Rows[0]["pdy_def"].ToString();


            cbxSfgwlh.Text = (dt.Rows[0]["isguowai"].ToString().Equals("0") || dt.Rows[0]["isguowai"].ToString() == "") ? "否" : "是";
            cbxSfgz.Text = (dt.Rows[0]["isgongzhi"].ToString().Equals("0") || dt.Rows[0]["isgongzhi"].ToString() == "") ? "否" : "是";
            cbxSfhzsq.Text = (dt.Rows[0]["ishezuo"].ToString().Equals("0") || dt.Rows[0]["ishezuo"].ToString() == "") ? "否" : "是";

            txbGj.Text = dt.Rows[0]["gj"].ToString();
            txbSheng.Text = dt.Rows[0]["sheng"].ToString();
            txbSheng2.Text = dt.Rows[0]["sheng1"].ToString();
            txbShi.Text = dt.Rows[0]["shi"].ToString();
            txbQx.Text = dt.Rows[0]["quxian"].ToString();
            txbSqn1.Text = dt.Rows[0]["ady"].ToString();
            txbSqn.Text = dt.Rows[0]["gdy"].ToString();
            txbSqnc.Text = dt.Rows[0]["gdy_def"].ToString();
            txbYzSl.Text = dt.Rows[0]["yz_sum"].ToString();
            txbZcpc.Text = dt.Rows[0]["f_cpc"].ToString();
            txbZfmr.Text = dt.Rows[0]["f_in"].ToString();
            txbZIpc.Text = dt.Rows[0]["f_ipc"].ToString();
            txbZj.Text = dt.Rows[0]["zhou"].ToString();
            txbZlLx.Text = dt.Rows[0]["type"].ToString();
            txbZllx2.Text = dt.Rows[0]["type1"].ToString();
            txbZlnl.Text = dt.Rows[0]["age"].ToString();
            txbZsqr.Text = dt.Rows[0]["f_pa"].ToString();
            txbZsqrLx.Text = dt.Rows[0]["f_pa_type"].ToString();
            txbZzflzt.Text = dt.Rows[0]["lg"].ToString();
            txbCpcSl.Text = dt.Rows[0]["cpc_sum"].ToString();
            txbIpcSl.Text = dt.Rows[0]["ipc_sum"].ToString();

            txbfmrSl.Text = dt.Rows[0]["in_sum"].ToString();
            txbSqrSl.Text = dt.Rows[0]["pa_sum"].ToString();
            txbJjqq.Text = dt.Rows[0]["quyu"].ToString();
            txbFljzn.Text = dt.Rows[0]["lg"].ToString();

            InitRgByData();
        }

        private void InitByEnData()
        {
            string strSid = dtSource.Rows[nCuurentIdx - 1]["sid"].ToString();
            strSidKey = strSid;
            DataTable dt = PtDataHelper.getPtData(string.Format("sid='{0}' and ztid={1}", strSid, strZtId), "", "st_dt");
            if (dt.Rows.Count < 1)
            {
                btnUpdate.Tag = "";
                return;
            }
            btnUpdate.Tag = dt.Rows[0]["id"].ToString();


            txbEnAdy.Text = dt.Rows[0]["ady"].ToString();
            txbEnPdY.Text = dt.Rows[0]["pdy"].ToString();
            txbEnPrY.Text = "";//pr 表
            txbEnOldPrY.Text = dt.Rows[0]["opdy"].ToString();
            txbEnPnYC.Text = dt.Rows[0]["pdy_def"].ToString();
            txbEnGnYC.Text = dt.Rows[0]["gdy_def"].ToString();
            txbEnFmlNm.Text = dt.Rows[0]["fml_sum"].ToString();
            txbEnCcNm.Text = dt.Rows[0]["gj_sum"].ToString();
            txbEnYzNm.Text = dt.Rows[0]["yz_sum"].ToString();
            txbEnPA1.Text = dt.Rows[0]["f_pa"].ToString();
            txbEnPaNm.Text = dt.Rows[0]["pa_sum"].ToString();
            cbxEnSfhzSq.Text = dt.Rows[0]["ishezuo"].ToString().Equals("1") ? "是" : "否";
            txbEnIn1.Text = dt.Rows[0]["f_in"].ToString();
            txbEnInNm.Text = dt.Rows[0]["in_sum"].ToString();
            txbEnZj.Text = dt.Rows[0]["zhou"].ToString();
            cbxEnSf3J.Text = dt.Rows[0]["issanju"].ToString().Equals("1") ? "是" : "否";
            cbxEnSf5j.Text = dt.Rows[0]["iswuju"].ToString().Equals("1") ? "是" : "否";
            txbEnOldPrC.Text = dt.Rows[0]["oprc"].ToString();
            txbEnIpc1.Text = dt.Rows[0]["f_ipc"].ToString();
            txbEnCpc1.Text = dt.Rows[0]["f_cpc"].ToString();
            txbEnDmc1.Text = dt.Rows[0]["f_dmc"].ToString();
            txbEnIpcNm.Text = dt.Rows[0]["ipc_sum"].ToString();
            txbEnCpcNm.Text = dt.Rows[0]["cpc_sum"].ToString();
            txbEnDmcNm.Text = dt.Rows[0]["dmc_sum"].ToString();
            txbEnEndLg.Text = dt.Rows[0]["lg"].ToString();
            txbEnZlNl.Text = dt.Rows[0]["age"].ToString();
            //cbxEnGzjs.Text = dt.Rows[0]["isgongzhi"].ToString().Equals("1") ? "是" : "否";
            //txbEnZlWcY.Text = "";

            txbEnAp_gjs.Text = dt.Rows[0]["ap_gjs"].ToString();
            txbEnPn_gjs.Text = dt.Rows[0]["pn_gjs"].ToString();

            txbBySl.Text = dt.Rows[0]["byz_sum"].ToString();
            txbCLzs.Text = dt.Rows[0]["cls_char_sum"].ToString();

            txbGkn.Text = dt.Rows[0]["pdy"].ToString();
            txbGknc.Text = dt.Rows[0]["pdy_def"].ToString();

            InitRgByData();
        }

        private void InitRgByData()
        {

            foreach (Control cItem in tabRgby.Controls)
            {
                if (!(cItem is GroupBox))
                {
                    continue;
                }

                GroupBox grb = (GroupBox)cItem;

                CheckedListBox chkboxTmp = (CheckedListBox)grb.Controls[0];

                for (int i = 0; i < chkboxTmp.Items.Count; i++)
                {
                    chkboxTmp.SetItemChecked(i, false);
                }
            }

            List<IDXKeyVAL> lstKeyVal = IdexItemMag.getListKeyVal(strSidKey, int.Parse(strZtId));
            foreach (var item in lstKeyVal)
            {
                CheckedListBox chkboxTmp = (CheckedListBox)(tabRgby.Controls.Find(item.KeyID.Value.ToString(), false)[0].Controls[0]);

                for (int i = 0; i < chkboxTmp.Items.Count; i++)
                {
                    IDXVAL ValItem = (IDXVAL)chkboxTmp.Items[i];

                    if (item.Valid.Value == ValItem.ID)
                    {
                        chkboxTmp.SetItemChecked(i, true);
                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedTab.Name)
            {
                case "tabCprsBaseInfor":
                    this.Height = 696;
                    this.Width = 771;
                    btnUpdate.Enabled = false;
                    break;
                case "tabEnBaseInfo":
                    btnUpdate.Enabled = false;
                    this.Height = 621;
                    this.Width = 771;
                    break;
                case "tabAutoIdxInfor": //"自动标引信息":
                    this.Height = 507;
                    this.Width = 771;
                    btnUpdate.Enabled = true;
                    break;
                case "tabEnAutoIdxInfor": //"自动标引信息":
                    this.Height = 527;
                    this.Width = 771;
                    btnUpdate.Enabled = true;
                    break;
                case "tabRgby":
                    InitRgByData();
                    btnUpdate.Enabled = true;
                    break;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedTab.Name)
            {
                case "tabAutoIdxInfor": //"Cn自动标引信息":
                    SaveAutoIdx(tabAutoIdxInfor);
                    break;
                case "tabEnAutoIdxInfor": //"En自动标引信息":
                    SaveAutoIdx(tabEnAutoIdxInfor);
                    break;
                case "tabRgby":
                    SaveRgIdx();
                    break;
            }
        }

        private void SaveAutoIdx(TabPage tbPage)
        {
            string strUpFileds = "";
            string[] strFiled = null;
            string strValue = "";

            foreach (var item in tbPage.Controls)
            {
                if (((Control)item).Tag != null && ((Control)item).Tag.ToString() == "")
                {
                    continue;
                }

                if (item is TextBox)
                {
                    strFiled = ((TextBox)item).Tag.ToString().Split('|');
                    strValue = ((TextBox)item).Text;


                    if (strFiled[1] == "int")
                    {
                        strValue = strValue.Equals("") ? "0" : strValue;
                        strUpFileds += string.Format(",{0}={1}", strFiled[0], strValue);
                    }
                    else
                    {

                        strUpFileds += string.Format(",{0}='{1}'", strFiled[0], strValue);
                    }
                }
                else if (item is ComboBox)
                {
                    strFiled = ((ComboBox)item).Tag.ToString().Split('|');
                    strValue = ((ComboBox)item).Text.Equals("否") ? "0" : "1";

                    if (strFiled[1] == "int")
                    {
                        strValue = strValue.Equals("") ? "0" : strValue;
                        strUpFileds += string.Format(",{0}={1}", strFiled[0], strValue);
                    }
                    else
                    {
                        strUpFileds += string.Format(",{0}='{1}'", strFiled[0], strValue);
                    }
                }
            }
            if (strUpFileds == "")
            {
                return;
            }

            strUpFileds = strUpFileds.TrimStart(',');

            if (PtDataHelper.UpdatePt(btnUpdate.Tag.ToString(), strUpFileds, "st_dt"))
            {
                MessageBox.Show("数据修改成功！", "温馨提示：");
            }
            else
            {
                MessageBox.Show("数据修改失败,请重试！", "温馨提示：");
            }
        }

        private void SaveRgIdx()
        {
            List<IDXKeyVAL> idxKeyVal = new List<IDXKeyVAL>();

            foreach (Control cItem in tabRgby.Controls)
            {
                if (!(cItem is GroupBox))
                {
                    continue;
                }

                GroupBox grb = (GroupBox)cItem;

                CheckedListBox chkboxTmp = (CheckedListBox)grb.Controls[0];

                foreach (IDXVAL idxValItem in chkboxTmp.CheckedItems)
                {

                    idxKeyVal.Add(new IDXKeyVAL()
                    {

                        IDXUser = int.Parse(BLL.SysMag.Login.StrLoginUserID),
                        KeyID = idxValItem.KeyID.Value,
                        Valid = idxValItem.ID,
                        SiD = strSidKey,
                        ZID = int.Parse(strZtId),
                        IDXDate = DateTime.Now
                    });
                }
            }

            if (IdexItemMag.UpdateKeyVal(strSidKey, int.Parse(strZtId), idxKeyVal))
            {
                MessageBox.Show("数据修改成功！", "温馨提示：");
            }
            else
            {
                MessageBox.Show("数据修改失败,请重试！", "温馨提示：");
            }
        }
    }
}
