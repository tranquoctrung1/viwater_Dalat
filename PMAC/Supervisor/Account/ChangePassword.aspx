<%@ Page Title="" Language="C#" MasterPageFile="~/Supervisor/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Supervisor_Account_ChangePassword" %>

<%@ Register src="../../Controls/ucChangePassword.ascx" tagname="ucChangePassword" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<div id="title"><div style="margin-left:10px; font-size:20px">Đổi mật khẩu</div>
    </div>--%>
    <h2>
        Đổi mật khẩu
    </h2>
    <div id="main">
        <uc1:ucChangePassword ID="ucChangePassword1" runat="server" />
    </div>
</asp:Content>

