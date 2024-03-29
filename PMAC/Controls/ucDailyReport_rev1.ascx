﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDailyReport_rev1.ascx.cs" Inherits="Controls_ucDailyReport_rev1" %>

<div class="row">
    
        <div class="col-sm-3">

            <telerik:RadListBox ID="listBoxSites" CssClass="RadListBox1" runat="server" Width="99%" Height="160px"
                SelectionMode="Multiple" AllowTransfer="True" TransferToID="listBoxSelectedSites" AutoPostBackOnTransfer="True"
                AllowReorder="True" AutoPostBackOnReorder="True" EnableDragAndDrop="True" DataSourceID="SitesDataSource" DataTextField="SiteAliasName" DataValueField="SiteId">
            </telerik:RadListBox>

            <asp:ObjectDataSource ID="SitesDataSource" runat="server"
                OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetSitesByDisplayGroup" TypeName="PMAC.BLL.SiteBL">
                <SelectParameters>
                    <asp:ControlParameter ControlID="mnuDisplayGroup" Name="displayGroup"
                        PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
          <div class="col-sm-3">

            <telerik:RadListBox ID="listBoxSelectedSites" CssClass="RadListBox2" runat="server" Width="99%" Height="160px"
                SelectionMode="Multiple" AllowReorder="True" AutoPostBackOnReorder="True" EnableDragAndDrop="True">

                <ButtonSettings TransferButtons="All"></ButtonSettings>

            </telerik:RadListBox>

        </div>
         <div class="col-sm-6">
             <div class="row">
                <div class="col-sm-4">
                    <asp:Label ID="lbFrom" runat="server" Text="Từ ngày"></asp:Label>:</div>
                <div class="col-sm-6">
                    <telerik:RadDatePicker ID="dtmStart" runat="server" Culture="en-GB">
                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                        <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"></DateInput>

                        <DatePopupButton ImageUrl="" HoverImageUrl="" ForeColor="White" BackColor="White"></DatePopupButton>
                    </telerik:RadDatePicker>
                </div>
             </div>
             <div class="row Padding">
                 <div class="col-sm-4">
                    <asp:Label ID="lbTo" runat="server" Text="Đến ngày"></asp:Label>:</div>
                    <div class="col-sm-4">
                        <telerik:RadDatePicker ID="dtmEnd" runat="server" Culture="en-GB">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                            <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"></DateInput>

                            <DatePopupButton ImageUrl="" HoverImageUrl="" ForeColor="White" BackColor="White"></DatePopupButton>
                        </telerik:RadDatePicker>
                    </div>
             </div>
                 <div class="row Padding">
                     <div class="col-sm-12 ">
                        <telerik:RadButton ID="btnView" runat="server" Text="  Xem  "
                            OnClick="btnView_Click">
                        </telerik:RadButton>
                         &nbsp;
        
                        <telerik:RadButton ID="btnExport" runat="server" Text="Export"
                            OnClick="btnExport_Click">
                        </telerik:RadButton>
                       </div>
    
    </div>


        </div>

    </div>


<div class="rowGridData">

<telerik:RadMenu ID="mnuDisplayGroup" runat="server" DataFieldID="Group"
    DataSourceID="DisplayGroupsDataSource" DataTextField="Description"
    DataValueField="Group" Flow="Vertical"
    OnItemClick="mnuDisplayGroup_ItemClick" Visible="False">
</telerik:RadMenu>
<asp:ObjectDataSource ID="DisplayGroupsDataSource" runat="server"
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDisplayGroups"
    TypeName="PMAC.BLL.DisplayGroupBL"></asp:ObjectDataSource>
<br />
<telerik:RadGrid ID="grv" runat="server" AutoGenerateColumns="False"
    CellSpacing="0" GridLines="None" ShowFooter="True"
    OnItemDataBound="grv_ItemDataBound" AllowSorting="True" OnNeedDataSource="grv_NeedDataSource" OnSortCommand="grv_SortCommand"
    >
    <ClientSettings>
        <Selecting CellSelectionMode="None" />
    </ClientSettings>
    <ExportSettings ExportOnlyData="True" HideStructureColumns="True" IgnorePaging="True" OpenInNewWindow="True">
        <Csv ColumnDelimiter="VerticalBar" />
    </ExportSettings>
    <AlternatingItemStyle HorizontalAlign="Center" />
    <MasterTableView DataKeyNames="SiteId" ClientDataKeyNames="SiteId">
        <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False" ShowExportToExcelButton="True" ShowRefreshButton="False" />
        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
            <HeaderStyle Width="20px" />
        </RowIndicatorColumn>
        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column"
            Visible="True" Created="True">
            <HeaderStyle Width="20px" />
        </ExpandCollapseColumn>
        <Columns>
            <telerik:GridBoundColumn DataField="TimeStamp" DataFormatString="{0:dd/MM/yyyy}"
                FilterControlAltText="Filter column3 column" HeaderText="Ngày"
                UniqueName="Time">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="SiteAliasName"
                FilterControlAltText="Filter column13 column" HeaderText="Mã vị trí (alias name)"
                UniqueName="column13">
                <ItemStyle HorizontalAlign="Left" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="MaxFlowRate"
                FilterControlAltText="Filter column14 column" HeaderText="LLMax(m3/h)" DataFormatString="{0:0.00}"
                UniqueName="column14">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="MinFlowRate"
                FilterControlAltText="Filter column15 column" HeaderText="LLMin(m3/h)" DataFormatString="{0:0.00}"
                UniqueName="column17">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Output" DataFormatString="{0:0.00}"
                FilterControlAltText="Filter column2 column" HeaderText="Sản lượng (m3)"
                UniqueName="Output" Aggregate="Sum" FooterAggregateFormatString="Tổng: {0:0.00} [m3]">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="MaxPressure"
                FilterControlAltText="Filter column15 column" HeaderText="ALMax(m)" DataFormatString="{0:0.00}"
                UniqueName="column15">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="MinPressure"
                FilterControlAltText="Filter column16 column" HeaderText="ALMin(m)" DataFormatString="{0:0.00}"
                UniqueName="column16">
            </telerik:GridBoundColumn>
            <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn2 column"
                HeaderText="Trending" UniqueName="lnkColumn">
                <ItemTemplate>
                    <asp:HyperLink ID="lnkChart" runat="server" Text="Đồ thị"></asp:HyperLink>
                    |
                    <asp:HyperLink ID="lnkTable" runat="server" Text="Bảng"></asp:HyperLink>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn DataField="SiteId"
                FilterControlAltText="Filter SiteId column" HeaderText="SiteId"
                UniqueName="SiteId" Visible="False">
            </telerik:GridBoundColumn>
        </Columns>
        <EditFormSettings>
            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
            </EditColumn>
        </EditFormSettings>

        <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
    </MasterTableView>
    <HeaderStyle BackColor="#25A0DA" ForeColor="White" HorizontalAlign="Center" />
    <FooterStyle BackColor="#25A0DA" ForeColor="White" HorizontalAlign="Center" />
    <ItemStyle HorizontalAlign="Center" />

    <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>

    <FilterMenu EnableImageSprites="False">
    </FilterMenu>
</telerik:RadGrid>
</div>
<%--<telerik:GridBoundColumn DataField="Location" 
                                        FilterControlAltText="Filter column1 column" HeaderText="Vị trí" 
                                        UniqueName="column1">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>--%>
<telerik:RadNotification ID="ntf" runat="server">
</telerik:RadNotification>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <telerik:RadWindow ID="winTable" runat="server" Height="540px"
            Modal="True" ReloadOnShow="True" Style="display: none; z-index: 7001"
            Title="Bảng dữ liệu chi tiết" VisibleStatusbar="False" Width="960px" ShowContentDuringLoad="False">
        </telerik:RadWindow>
        <telerik:RadWindow ID="winChart" runat="server" Height="581px"
            Modal="True" ReloadOnShow="True" Style="display: none; z-index: 7001"
            Title="Chart" VisibleStatusbar="False" Width="1000px" ShowContentDuringLoad="False">
        </telerik:RadWindow>
    </Windows>

</telerik:RadWindowManager>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function ShowTableForm(si, dt, rowIndex) {
            var grid = $find("<%= grv.ClientID %>");
            var rowControl = grid.get_masterTableView().get_dataItems()[rowIndex].get_element();
            grid.get_masterTableView().selectItem(rowControl, true);
            window.radopen("DetailTable.aspx?si=" + si + "&dt=" + dt, "winTable");
            return false;
        }
        function ShowChartFormDaily(si, dt, uid, rowIndex) {
            var grid = $find("<%= grv.ClientID %>");
            var rowControl = grid.get_masterTableView().get_dataItems()[rowIndex].get_element();
            grid.get_masterTableView().selectItem(rowControl, true);
            window.radopen("DetailChart_rev1.aspx?si=" + si + "&dt=" + dt + "&uid=" + uid + "&ty=daily", "winChart");
            return false;
        }
    </script>
</telerik:RadCodeBlock>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="listBoxSites">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="listBoxSites" UpdatePanelCssClass="" />
                <telerik:AjaxUpdatedControl ControlID="listBoxSelectedSites" UpdatePanelCssClass="" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="listBoxSelectedSites">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="listBoxSites" UpdatePanelCssClass="" />
                <telerik:AjaxUpdatedControl ControlID="listBoxSelectedSites" UpdatePanelCssClass="" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="btnView">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="listBoxSites" UpdatePanelCssClass="" />
                <telerik:AjaxUpdatedControl ControlID="listBoxSelectedSites" UpdatePanelCssClass="" />
                <telerik:AjaxUpdatedControl ControlID="grv" UpdatePanelCssClass="" />
                <telerik:AjaxUpdatedControl ControlID="ntf" UpdatePanelCssClass="" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="mnuDisplayGroup">
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="grv">
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
