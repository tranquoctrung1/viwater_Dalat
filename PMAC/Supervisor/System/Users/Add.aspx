﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Supervisor/MasterPage.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Supervisor_Users_Add" %>

<%@ Register src="../../../Controls/System/Users/Add.ascx" tagname="ucUserManagementAdd" tagprefix="uc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <h2>
        <asp:Label ID="lbTitle" runat="server" Text="Thêm người dùng"></asp:Label>
    </h2>
    <div id="main">
        <uc1:ucUserManagementAdd ID="ucUserManagementAdd" runat="server" />
    </div>
</asp:Content>

