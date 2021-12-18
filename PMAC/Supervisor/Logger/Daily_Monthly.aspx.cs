﻿using PMAC.BLL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Supervisor_Logger_Daily_Monthly : System.Web.UI.Page
{
    LanguageBL languageBL = new LanguageBL();

    private static string language = "vi";

    protected void Page_Load(object sender, EventArgs e)
    {
        // Pi-solution
        if (!IsPostBack)
        {
            language = (Session["myapplication.language"] == null) ? "vi" : Session["myapplication.language"].ToString();

            List<t_LanguageTranslate> list = languageBL.FindAll(x => x.Language == language && x.Noted.Contains("/Supervisor/Logger/Daily_Monthly.aspx")).ToList();

            // Language - Pi-solution
            var var1 = list.Where(x => x.ControlId.Contains("Tab1")).FirstOrDefault();
            RadTabStrip1.Tabs[0].Text = (var1 == null) ? "Dữ Liệu Giờ" : "Dữ Liệu Giờ";
            var var2 = list.Where(x => x.ControlId.Contains("Tab2")).FirstOrDefault();
            RadTabStrip1.Tabs[1].Text = (var2 == null) ? "Dữ Liệu Ngày" : "Dữ Liệu Ngày";
            var var3 = list.Where(x => x.ControlId.Contains("Tab3")).FirstOrDefault();
            RadTabStrip1.Tabs[2].Text = (var3 == null) ? "Dữ Liệu Tháng" : "Dữ Liệu Tháng";

            var var01 = list.Where(x => x.ControlId.Contains("lbTitle")).FirstOrDefault();
            lbTitle.Text = (var01 == null) ? "" : var01.Contents;
        }
    }
    
}