﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AUTO.Utility;
using System.Data;
using System.Text;
using AUTO.DAL;
using AUTO.BLL;
using AUTO.Model;

namespace AUTO.youth_admin.Download
{
    public partial class Download : BasePage
    {
        YouthDownloadsBLL download_bll = new YouthDownloadsBLL();

        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(hfPageIndex.Value))
            {
                pageIndex = Convert.ToInt32(hfPageIndex.Value);
            }
            if (!String.IsNullOrEmpty(hfPageSize.Value))
            {
                pageSize = Convert.ToInt32(hfPageSize.Value);
            }
            if (!IsPostBack)
            {
                bindDownloadCol();
                bindDownload();
            }
        }

        /// <summary>
        /// 绑定文件列表
        /// </summary>
        protected void bindDownload()
        {
            pageTotal = download_bll.GetRecordCount(" 1=1 " + GetWhereSql());
            DataSet ds = download_bll.GetListByPage(" 1=1 " + GetWhereSql(), pageSize, pageIndex, " upload_time desc");
            /*
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string is_check = ds.Tables[0].Rows[i]["is_check"].ToString();
                if (String.IsNullOrEmpty(is_check) || is_check.Trim().Equals("N"))
                {
                    ds.Tables[0].Rows[i]["is_check"] = "未审核";
                }
                else
                {
                    ds.Tables[0].Rows[i]["is_check"] = "已审核";
                }
            }
            */
            gvwData.DataSource = ds;
            gvwData.DataKeyNames = new String[] { "download_id" };
            gvwData.DataBind();
        }

        /// <summary>
        /// 获取筛选条件
        /// </summary>
        /// <returns></returns>
        private string GetWhereSql()
        {
            StringBuilder str = new StringBuilder();
            if (txtWord.Text.Trim() != "")
            {
                str.Append(" and download_title like '%" + txtWord.Text.Trim() + "%' ");
            }
            /*
            if (ddlCheck.SelectedValue.Equals("1"))
            {
                str.Append(" and is_check='Y' ");
            }
            else if (ddlCheck.SelectedValue.Equals("0"))
            {
                str.Append(" and is_check='N' ");
            }
            */
            if (!ddl_download_col.SelectedValue.Equals("0"))
            {
                str.Append(" and download_father_id=" + int.Parse(ddl_download_col.SelectedValue.ToString()));
            }
            if (str.Length == 0)
            {
                str.Append(" and 1=1 ");
            }
            return str.ToString();
        }

        protected void bindDownloadCol()
        {
            AUTO.DAL.YouthDownloadsColDAL download_col_dal = new DAL.YouthDownloadsColDAL();
            DataSet ds = download_col_dal.GetDownloadsCol();
            ddl_download_col.DataValueField = "download_column_id";
            ddl_download_col.DataTextField = "download_column_name";
            ddl_download_col.DataSource = ds;
            ddl_download_col.DataBind();
            ddl_download_col.Items.Insert(0, new ListItem("", "0"));
        }

        //日期格式转换
        protected string FormatTime(DateTime time)
        {
            string str = time.ToString("yyyy-MM-dd");
            return str;
        }

        //标题过长，进行截取
        protected string CutString(string strToCut)
        {
            if (strToCut.Length > 20)
            {
                strToCut = strToCut.Substring(0, 19).ToString() + "...";
            }
            return strToCut;
        }

        //搜索文件
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            bindDownload();
        }

        //添加文件
        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("DownloadAdd.aspx");
        }

        //分页
        protected void btnHide_Click(object sender, EventArgs e)
        {
            bindDownload();
        }
        /*
        //审核文件
        protected void lbtnCheck_Click(object sender, EventArgs e)
        {
            int success = 0;
            YouthDownload model;
            for (int i = 0; i < gvwData.Rows.Count; i++)
            {
                CheckBox cbx = (CheckBox)gvwData.Rows[i].FindControl("chkbOne");
                if (cbx.Checked == true)
                {
                    int id = Convert.ToInt32(gvwData.DataKeys[i].Value);
                    model = new YouthDownload();
                    model.Download_id = id;
                    model.Checker = Session[Constant.adminName].ToString();
                    model.Check_time = DateTime.Today;
                    model.Is_check = "Y";
                    download_bll.CheckDownload(model);
                    success++;
                }
            }
            bindDownload();
            String message = "成功审核" + success + "条记录！";
            MyUtil.ShowMessage(this, message);
        }

        //反审核文件
        protected void lbtnReCheck_Click(object sender, EventArgs e)
        {
            int success = 0;
            YouthDownload model;
            for (int i = 0; i < gvwData.Rows.Count; i++)
            {
                CheckBox cbx = (CheckBox)gvwData.Rows[i].FindControl("chkbOne");
                if (cbx.Checked == true)
                {
                    int id = Convert.ToInt32(gvwData.DataKeys[i].Value);
                    model = new YouthDownload();
                    model.Download_id = id;
                    model.Rechecker = Session[Constant.adminName].ToString();
                    model.Recheck_time = DateTime.Today;
                    model.Is_check = "N";
                    download_bll.ReCheckDownload(model);
                    success++;
                }
            }
            bindDownload();
            String message = "成功取消审核" + success + "条记录！";
            MyUtil.ShowMessage(this, message);
        }
        */
        //删除文件
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            int success = 0;
            for (int i = 0; i < gvwData.Rows.Count; i++)
            {
                CheckBox cbx = (CheckBox)gvwData.Rows[i].FindControl("chkbOne");
                if (cbx.Checked == true)
                {
                    int id = Convert.ToInt32(gvwData.DataKeys[i].Value);
                    download_bll.DelDownload(id);
                    success++;
                }
            }
            bindDownload();
            String message = "成功删除" + success + "条记录！";
            MyUtil.ShowMessage(this, message);
        }
        /*
        //编辑
        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;
            int download_id = Convert.ToInt32(lbtn.CommandArgument);
            Response.Redirect("DownloadUpd.aspx?download_id=" + download_id);
        }
        */
    }
}