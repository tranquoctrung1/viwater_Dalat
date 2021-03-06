<%@ page title="" language="C#" masterpagefile="~/Consumer/MasterPage.master" autoeventwireup="true" inherits="Consumer_Logger_Daily_Monthy, App_Web_fhgam4td" theme="Default" %>

<%@ Register Src="~/Controls/ucDailyReportConsumer.ascx" TagPrefix="uc1" TagName="ucDailyReportConsumer" %>
<%@ Register Src="~/Controls/ucMontlyReportConsumer.ascx" TagPrefix="uc1" TagName="ucMontlyReportConsumer" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Dữ liệu logger</h2>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Dữ liệu ngày">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Dữ liệu tháng">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server">
        <telerik:RadPageView ID="RadPageView1" runat="server" Selected="true">
            <br />
            <uc1:ucDailyReportConsumer runat="server" id="ucDailyReportConsumer" />
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView2" runat="server">
            <br />
            <uc1:ucMontlyReportConsumer runat="server" id="ucMontlyReportConsumer" />
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>

