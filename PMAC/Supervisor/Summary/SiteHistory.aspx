<%@ Page Title="" Language="C#" MasterPageFile="~/Supervisor/MasterPage.master" AutoEventWireup="true" CodeFile="SiteHistory.aspx.cs" Inherits="Supervisor_Summary_SiteHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="title">
        <div class="div_title_left">Lịch sử điểm lắp đặt</div>
        <div class="div_center">
        </div>
    </div>
    <div id="main">
        <ul class="multiple_columns" style="width:50%">
            <li class="text_grid_2_columns">Mã vị trí (tag name):
            </li>
            <li class="control_grid_2_columns">
                <telerik:RadComboBox ID="cboSiteIDs" runat="server">
                </telerik:RadComboBox>
            </li>
            <li class="text_grid_2_columns">
            </li>
            <li class="control_grid_2_columns">
                <telerik:RadButton ID="btnView" runat="server" Text="Xem">
                </telerik:RadButton>
                <telerik:RadButton ID="btnExport" runat="server" Text="Export">
                </telerik:RadButton>
            </li>
        </ul>
    </div>
</asp:Content>

