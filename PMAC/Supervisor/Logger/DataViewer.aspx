<%@ Page Language="C#" MasterPageFile="~/Supervisor/MasterPage.master" AutoEventWireup="true" CodeFile="DataViewer.aspx.cs" Inherits="Supervisor_Logger_DataViewer" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/amcharts/amcharts.js"></script>
    <script src="../../js/amcharts/serial.js"></script>
    <script src="../../js/amcharts/exporting/amexport.js"></script>
    <script src="../../js/amcharts/exporting/canvg.js"></script>
    <script src="../../js/amcharts/exporting/filesaver.js"></script>
    <script src="../../js/amcharts/exporting/jspdf.js"></script>
    <script src="../../js/amcharts/exporting/jspdf.plugin.addimage.js"></script>
    <script src="../../js/amcharts/exporting/rgbcolor.js"></script>
    <script src="../../js/randomColor.js"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css" >
    <script src="https://code.jquery.com/jquery-3.5.1.min.js" ></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.bundle.min.js" ></script>

    <style type="text/css">
        div.haha {
            width: 100%;
            height: 300px;
            overflow: auto;
            overflow-x: hidden
        }

            div.haha table th {
                background: #1BACE1;
                font-family: Arial;
                color: #fff;
                position: -webkit-sticky;
                position: sticky;
                top: -1px;
                font-size: small;
            }

            div.haha tfoot tr td {
                background: #1BACE1;
                font-family: Arial;
                color: #fff;
                position: -webkit-sticky;
                position: sticky;
                bottom: 0;
            }

            div.haha table th, div.haha table td {
                text-align: center;
                padding: 5px 7px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2 id="title" runat="server">Dữ liệu online</h2>
    <div class="container-fluid2">
        <%--Chọn site và kênh xem--%>
        <div class="row">
            <div class="col-lg-6 col-md-12 mt-1">
                <div class="row">
                    <div class="col-md-6 col-sm-12 mt-2">

                        <telerik:RadComboBox ID="cboMonitoringSites" runat="server" AllowCustomText="True"
                            AutoPostBack="true" DataSourceID="ObjectDataSource2" DataTextField="SiteId"
                            DataValueField="LoggerId" EnableLoadOnDemand="True" Filter="StartsWith"
                            HighlightTemplatedItems="True" DropDownWidth="320px" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                            <HeaderTemplate>
                                <table cellpadding="0" cellspacing="0" style="width: 320px">
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:Label ID="locationId_header" runat="server" Text="SiteId"></asp:Label>
                                        </td>
                                        <td style="width: 210px">
                                            <asp:Label ID="location_header" runat="server" Text="Location"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0" style="width: 320px">
                                    <tr>
                                        <td style="width: 100px"><%#DataBinder.Eval(Container.DataItem,"SiteId") %></td>
                                        <td style="width: 210px"><%#DataBinder.Eval(Container.DataItem,"Location") %></td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:RadComboBox>
                        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server"
                            OldValuesParameterFormatString="original_{0}" SelectMethod="GetSites"
                            TypeName="PMAC.BLL.SiteBL"></asp:ObjectDataSource>
                    </div>
                    <div class="col-md-6 col-sm-12 ">
                       <%-- <asp:LinkButton ID="btnChannelViewConfig" runat="server"
                            OnClientClick="btnChannelViewConfig_Click();return false;">
                            <asp:Label ID="lbbtnChannelViewConfig" runat="server" Text="Lọc kênh xem"></asp:Label>:
                        </asp:LinkButton>--%>
                       <button type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#exampleModal">
                          Lọc kênh xem
                        </button>

                    </div>
                </div>

            </div>
            <div class="col-lg-6 col-md-12 mt-1">
                <div class="row">
                    <div class="col-md-4 col-sm-12 mt-2">
                        <telerik:RadDateTimePicker ID="dtmStart" runat="server" Culture="en-GB">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                            <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%" EmptyMessage="Bắt đầu"></DateInput>

                            <DatePopupButton ImageUrl="" HoverImageUrl="" ForeColor="White" BackColor="White"></DatePopupButton>
                        </telerik:RadDateTimePicker>
                    </div>
                    <div class="col-md-4 col-sm-12 mt-2">
                        <telerik:RadDateTimePicker ID="dtmEnd" runat="server" Culture="en-GB">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                            <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%" EmptyMessage="Kết thúc"></DateInput>

                            <DatePopupButton ImageUrl="" HoverImageUrl="" ForeColor="White" BackColor="White"></DatePopupButton>
                        </telerik:RadDateTimePicker>
                    </div>
                    <div class="col-md-4 col-sm-12 mt-2">
                        <button type="button" onclick="btnView_OnClientClick()" class="btn btn-primary btn-sm">Xem</button>
                    </div>
                </div>
            </div>
        </div>
        <%--Card từng kênh--%>
        <div class="row mt-2" id="channel_cards">
        </div>

        <div class="row">
            <div class="col-lg-8 col-md-12" id="c_col">
                <div id="chart_canvas" style="height: 350px"></div>
            </div>
            <div class="col-lg-4 col-md-12" id="t_col">
                <div class="row">
                    <div class="col-12">
                        <div id="data_table" class="haha"></div>
                    </div>
                    <div class="col-12 mt-1">
                        <a class="btn btn-success" id="btnExportXls" style="color: white; display: none"><i class="fa fa-file-excel-o"></i>Export</a>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Lọc kênh</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body" id="bodyModal">
            
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Đóng</button>
          </div>
        </div>
      </div>
    </div>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <telerik:RadWindow ID="winChangeChannelView" runat="server"
                Height="400px" Modal="True" Behaviors="Close" OnClientClose="winChangeChannelView_OnClientClose"
                ReloadOnShow="True" Style="display: none; overflow-y: hidden" Title="Kênh thể hiện"
                VisibleStatusbar="False" Width="280px">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboMonitoringSites">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboMonitoringSites" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

    <telerik:RadCodeBlock runat="server">
        <%--Xuất table thành excel--%>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/axios/0.21.0/axios.min.js"></script>

        <script type="text/javascript">
            var url;
            var table;
            var listChannel = [];
            var hostname = window.location.origin;
            if (hostname.indexOf("localhost") < 0)
                hostname = hostname + "/VivaServices/Service1.svc";
            else
                hostname = "http://localhost:63111/Service1.svc";

            var urlGetMonitoringCardInfos = hostname + '/GetCardSite/';
            var urlGetMonitoringChannelDataBySite = hostname + '/GetMultipleDataChannel';
            var urlGetListChannel = hostname + '/GetChannelBySiteId/'

            //Cards
            function GetUnixStartDate() {
                var sd = $find('<%= dtmStart.ClientID %>');
                var selected_sd = sd.get_selectedDate();
                if (selected_sd == null || selected_sd == 'undefined') {
                    selected_sd = new Date();
                    selected_sd.setHours(0, 0, 0, 0);
                }
                console.log(selected_sd);
                return toUnixSeconds(selected_sd);
            }

            function GetUnixEndDate() {
                var ed = $find('<%= dtmEnd.ClientID %>');
                var selected_ed = ed.get_selectedDate();
                if (selected_ed == null || selected_ed == 'undefined') {
                    selected_ed = new Date();
                }
                console.log(selected_ed);
                return toUnixSeconds(selected_ed);
            }


            function CardsLoad(siteID, start, end) {
                //var a = fromOADate(start);
                //var b = fromOADate(end);
                var url = urlGetMonitoringCardInfos + siteID + '/' + start + '/' + end;
                var html = '';  

                axios.get(url).then(function (res) {
                    
                    for (let c of res.data.GetCardSiteResult) {
                        if (CheckIsDisplay(c.ChannelId) > - 1) {
                            html +=
                                '<div class="col-xl-3 col-md-6 mb-4" id="card-' + c.ChannelId + '">' +
                                '<div class="card border-left-primary shadow h-100" style="border-left: 5px solid #74b9ff">' +
                                '<div class="card-body">' +
                                '<div class="row no-gutters align-items-center">' +
                                '<div class="col-7 mr-2">' +
                                '<div class="h5 font-weight-bold text-primary mb-1">' + c.ChannelId + ' (' + c.Unit + ')</div>' +
                                '<div class="h6 font-weight-bold text-success mb-1">' + c.InstantValue + '</div>' +
                                '<div class="text-xs">' + parsedDateFormatted(c.InstantTime) + '</div>' +
                                '</div>' +
                                '<div class="col-4">' +
                                '<div class="row">' +
                                '<div class="col-12 text-xs text-primary text-center " style="font-weight: bold">MAX</div>' +
                                '<div class="col-12 text-center"  style="font-weight: bold">' + c.MaxValue + '</div>' +
                                '<div class="col-12 text-xs text-primary text-center"  style="font-weight: bold">MIN</div>' +
                                '<div class="col-12 text-center"  style="font-weight: bold">' + c.MinValue + '</div>' +
                                '</div></div>' +
                                '</div>' +
                                '</div>' +
                                '</div>' +
                                '</div>' +
                                '</div>';
                        }
                        else {
                            html +=
                                '<div class="col-xl-3 col-md-6 mb-4 d-none" id="card-' + c.ChannelId + '">' +
                                '<div class="card border-left-primary shadow h-100" style="border-left: 5px solid #74b9ff">' +
                                '<div class="card-body">' +
                                '<div class="row no-gutters align-items-center">' +
                                '<div class="col-7 mr-2">' +
                                '<div class="h5 font-weight-bold text-primary mb-1">' + c.ChannelId + ' (' + c.Unit + ')</div>' +
                                '<div class="h6 font-weight-bold text-success mb-1">' + c.InstantValue + '</div>' +
                                '<div class="text-xs">' + parsedDateFormatted(c.InstantTime) + '</div>' +
                                '</div>' +
                                '<div class="col-4">' +
                                '<div class="row">' +
                                '<div class="col-12 text-xs text-primary text-center " style="font-weight: bold">MAX</div>' +
                                '<div class="col-12 text-center"  style="font-weight: bold">' + c.MaxValue + '</div>' +
                                '<div class="col-12 text-xs text-primary text-center"  style="font-weight: bold">MIN</div>' +
                                '<div class="col-12 text-center"  style="font-weight: bold">' + c.MinValue + '</div>' +
                                '</div></div>' +
                                '</div>' +
                                '</div>' +
                                '</div>' +
                                '</div>' +
                                '</div>';
                        }

                    }

                    document.getElementById('channel_cards').innerHTML = html;
                })
                .catch(err => console.log(err));

            }
            ////Cards

            function CheckIsDisplay(channelid) {
                return listChannel.indexOf(channelid);
            }

            //Charts
           function drawChart(siteId, start, end) {
                channels = [];
                //channels.push(channel);
                colors = [];
                mreds = [];
                mblues = [];

               var url = urlGetMonitoringChannelDataBySite + "/" + siteId + "/" + start + "/" + end;
               axios.get(url).then(function (res) {
                   let tempData = res.data.GetMultipleDataChannelResult;
                   if (tempData.length > 0) {
                       let max = tempData[0].length;
                       let index = 0;
                       for (let i = 0; i < tempData.length; i++) {
                           if (max < tempData[i]) {
                               max = tempData[i].length;
                               index = i;
                           }
                           if (tempData[i][0] != undefined) {
                               channels.push(tempData[i][0].ChannelID);
                           }
                       }

                       let dataForChart = [];
                       for (let i = 0; i < max; i++) {
                           let obj = {};
                           obj.TimeStamp = ConverDate(tempData[index][i].TimeStamp);
                           for (let j = 0; j < tempData.length; j++) {
                               try {
                                   if (tempData[j][i].Value != null) {
                                       obj[`${tempData[j][i].ChannelID}`] = tempData[j][i].Value;
                                   }
                               }
                               catch (err) {
                                   console.log(err);
                               }

                           }
                           dataForChart.push(obj);
                       }


                       dataForChart = dataForChart.sort(function (a, b) { return a.TimeStamp - b.TimeStamp})

                       chartData = dataForChart;
                       //SERIAL CHART
                       chart = new AmCharts.AmSerialChart();
                       chart.pathToImages = "../../js/amcharts/images/";
                       chart.dataProvider = chartData;
                       chart.categoryField = "TimeStamp";
                       chart.balloon.bulletSize = 5;
                       //TITLE
                       //chart.addTitle(channel.ChannelName + ' (' + channel.Unit + ')');
                       //ZOOM
                       chart.addListener("dataUpdated", zoomChart);
                       //AXES
                       //X
                       var categoryAxis = chart.categoryAxis;
                       categoryAxis.parseDates = true;
                       categoryAxis.minPeriod = "mm";
                       categoryAxis.dashLength = 1;
                       categoryAxis.minorGridEnabled = true;
                       categoryAxis.twoLineMode = true;
                       categoryAxis.dateFormats = [{
                           period: 'fff',
                           format: 'JJ:NN:SS'
                       }, {
                           period: 'ss',
                           format: 'JJ:NN:SS'
                       }, {
                           period: 'mm',
                           format: 'JJ:NN'
                       }, {
                           period: 'hh',
                           format: 'JJ:NN'
                       }, {
                           period: 'DD',
                           format: 'DD'
                       }, {
                           period: 'WW',
                           format: 'DD'
                       }, {
                           period: 'MM',
                           format: 'YYYY'
                       }, {
                           period: 'YYYY',
                           format: 'YYYY'
                       }];

                       categoryAxis.axisColor = "#DADADA";
                       categoryAxis.gridAlpha = 0.15;
                       //AXE
                       //Y1
                       valueAxis = new AmCharts.ValueAxis();
                       valueAxis.axisColor = 'blue';
                       valueAxis.axisThickness = 1;
                       chart.addValueAxis(valueAxis);
                       //Y2 

                       colors = ['#F44336', '#9C27B0', '#3F51B5', '#EC407A', '#03A9F4', '#009688', '#4CAF50', '#8BC34A', '#CDDC39', '#FFEB3B', '#FFA726', '#FF7043', '#795548', '#616161', '#546E7A', '#00695C'];

                       for (let i = 0; i < channels.length; i++) {
                           var color = randomColor();
                           var graph = new AmCharts.AmGraph();
                           graph.id = channels[i];
                           //graph.valueAxis = valueAxisFeed;
                           graph.title = channels[i];
                           graph.valueField = channels[i];
                           graph.bullet = "round";
                           graph.bulletBorderColor = colors[i];
                           graph.bulletBorderThickness = 2;
                           graph.bulletBorderAlpha = 1;
                           graph.bulletSize = 8;
                           graph.lineThickness = 1;
                           graph.lineColor = colors[i];
                           graph.hideBulletsCount = 50;
                           graph.forceGap = true;

                           if (CheckIsDisplay(channels[i]) > -1) {
                               graph.hidden = false
                           }
                           else {
                               graph.hidden = true;
                           }

                           chart.addGraph(graph);
                       }
                      

                       // CURSOR
                       var chartCursor = new AmCharts.ChartCursor();
                       chartCursor.categoryBalloonDateFormat = "MMM DD, YYYY JJ:NN";
                       chart.addChartCursor(chartCursor);
                       // SCROLLBAR
                       //var chartScrollbar = new AmCharts.ChartScrollbar();
                       //chartScrollbar.autoGridCount = true;
                       //chartScrollbar.scrollbarHeight = 20;
                       //chart.addChartScrollbar(chartScrollbar);
                       // LEGEND
                       var legend = new AmCharts.AmLegend();
                       legend.marginLeft = 110;
                       legend.useGraphSettings = true;
                       chart.addLegend(legend);
                       //MOUSE
                       chart.mouseWheelZoomEnabled = true;
                       chart.mouseWheelScrollEnabled = true;
                       chart.creditsPosition = "bottom-right";
                       //EXPORT
                       chart.amExport = {
                           top: 21,
                           right: 21,
                           buttonColor: '#EFEFEF',
                           buttonRollOverColor: '#DDDDDD',
                           exportPNG: true,
                           exportJPG: true,
                           exportPDF: true,
                           exportSVG: true
                       }
                       // WRITE
                       chart.write('chart_canvas');
                       CreateDataTable(channels.length, chartData);
                   }
                   

               }).catch(err => console.log(err));

            };

            function zoomChart() {
                // different zoom methods can be used - zoomToIndexes, zoomToDates, zoomToCategoryValues
                chart.zoomToIndexes(chartData.length - chartData.length, chartData.length - 1);
            }
            ////Charts

            //DataTable
            function CreateDataTable(channelLength, chartData) {
                var header = "";
                var body = "";

                var descChartData = chartData.sort(function (a, b) {
                    return new Date(b.TimeStamp) - new Date(a.TimeStamp)
                });

                if (descChartData.length > 0) {
                    //Show Export Button
                    $('#btnExportXls').show();
                }
                else $('#btnExportXls').hide();

                for (let i = 0; i < descChartData.length; i++) {
                    if (i == 0) {
                        for (let pro of Object.getOwnPropertyNames(descChartData[0])) {
                            header += `<th>${pro}</th>`;
                        }
                    }
                    else {
                        body += `<tr>`;
                        for (let pro in descChartData[i]) {
                            if (pro == "TimeStamp") {
                                body += `<td>${dateToString(descChartData[i][pro])}</td>`
                            }
                            else {
                                body += `<td>${descChartData[i][pro]}</td>`;
                            }
                        }
                        body += `</tr>`;
                    }
                }
                table = '<table class="table table-bordered dataTable no-footer" id="dataTable" cellspacing="0" style="width: 100%;overflow-y:auto" role="grid" aria-describedby="dataTable_info">' +
                    '<thead>' + header +
                    '</thead>' +
                    '<tbody>' + body +
                    '</tbody>' +
                    '</table > ';
                $('#data_table').html(table);
            }
            ////DataTable

            //Support
            function toUnixSeconds(datetime) {
                return Math.floor(datetime.getTime() / 1000);
            }

            function parsedDateFormatted(date) {
                //var date = new Date(parseInt(strDate.substr(6)));
                //let stringSplit = date.toString().split("-");
                //let year = parseInt(stringSplit[0]);
                //let month = parseInt(stringSplit[1]) < 10 ? `0${parseInt(stringSplit[1])}` : parseInt(stringSplit[1]);
                //let stringSplit2 = stringSplit[2].split("T");
                //let day = parseInt(stringSplit2[0]) < 10 ? `0${parseInt(stringSplit2[0])}` : parseInt(stringSplit2[0]);
                //let stringSplit3 = stringSplit2[1].split(":");
                //let hours = parseInt(stringSplit3[0]) < 10 ? `0${parseInt(stringSplit3[0])}` : parseInt(stringSplit3[0]);
                //let minutes = parseInt(stringSplit3[1]) < 10 ? `0${parseInt(stringSplit3[1])}` : parseInt(stringSplit3[1]);
                //let seconds = parseInt(stringSplit3[2]) < 10 ? `0${parseInt(stringSplit3[2])}` : parseInt(stringSplit3[2]);
                if (date != null && date != undefined) {
                    var parsedDate = new Date(parseInt(date.substr(6)));
                    var jsDate = new Date(parsedDate);

                    return jsDate.getDate() + '/' + (jsDate.getMonth() + 1) + '/' + jsDate.getFullYear() + ' ' + jsDate.getHours() + ':' + jsDate.getMinutes() + ':' + jsDate.getSeconds();

                }
                else {
                    return "";
                }
            }

            function dateToString(date) {
                var dd = String(date.getDate()).padStart(2, '0');
                var MM = String(date.getMonth() + 1).padStart(2, '0'); //January is 0!
                var yyyy = date.getFullYear();
                var hh = String(date.getHours()).padStart(2, '0');
                var mm = String(date.getMinutes()).padStart(2, '0');
                var ss = String(date.getSeconds()).padStart(2, '0');

                return dd + '/' + MM + '/' + yyyy + ' ' + hh + ':' + mm + ':' + ss;
            }

            function ConverDate(date) {
                //let stringSplit = date.toString().split("-");
                //let year = parseInt(stringSplit[0]);
                //let month = parseInt(stringSplit[1]) < 10 ? `0${parseInt(stringSplit[1])}` : parseInt(stringSplit[1]);
                //let stringSplit2 = stringSplit[2].split("T");
                //let day = parseInt(stringSplit2[0]) < 10 ? `0${parseInt(stringSplit2[0])}` : parseInt(stringSplit2[0]);
                //let stringSplit3 = stringSplit2[1].split(":");
                //let hours = parseInt(stringSplit3[0]) < 10 ? `0${parseInt(stringSplit3[0])}` : parseInt(stringSplit3[0]);
                //let minutes = parseInt(stringSplit3[1]) < 10 ? `0${parseInt(stringSplit3[1])}` : parseInt(stringSplit3[1]);
                //let seconds = parseInt(stringSplit3[2]) < 10 ? `0${parseInt(stringSplit3[2])}` : parseInt(stringSplit3[2]);

                //return new Date(year, month -1, day, hours, minutes, seconds);

                var parsedDate = new Date(parseInt(date.substr(6)));
                var jsDate = new Date(parsedDate);

                return jsDate;
            }

            function btnView_OnClientClick() {
                var cboSite = $find('<%= cboMonitoringSites.ClientID %>');
                if (cboSite.get_text()) {
                    var start = GetUnixStartDate();
                    var end = GetUnixEndDate();
                    //Cards
                    CardsLoad(cboSite.get_text(), start, end);
                    //Chart
                    drawChart(cboSite.get_text(), start, end);
                    //Event Export Button
                    $('#btnExportXls').off('click').on('click', function () {
                        exportToXLS(cboSite.get_text(), table, "Export_Du_Lieu");
                    })
                }
                else {
                    alert('Chưa chọn site');
                }
            }

            function btnChannelViewConfig_Click() {
                var cboSite = $find('<%= cboMonitoringSites.ClientID %>');
                if (cboSite.get_value()) {
                    var win = $find('<%= winChangeChannelView.ClientID %>');
                    win.setUrl('ChannelViewer.aspx?ADAM_ID=' + cboSite.get_value());
                    win.show();
                }
                else {
                    alert('Chưa chọn site');
                }
            }

            function winChangeChannelView_OnClientClose(sender, args) {
                setTimeout(btnView_OnClientClick, 500);

            }

            function exportToXLS(title, table, fileName) {
                if (typeof fileName !== 'string' || Object.prototype.toString.call(fileName) !== '[object String]') {
                    throw new Error('Invalid input type: exportToCSV(String)');
                }

                const TEMPLATE_XLS = `
                    <html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40">
                    <meta http-equiv="content-type" content="application/vnd.ms-excel; charset=UTF-8"/>
                    <head><!--[if gte mso 9]><xml>
                    <x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{title}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml>
                    <![endif]--></head>
                    <body>{table}</body></html>`;
                const MIME_XLS = 'application/vnd.ms-excel;base64,';

                const parameters = {
                    title: title,
                    table: table,
                };
                const computeOutput = TEMPLATE_XLS.replace(/{(\w+)}/g, (x, y) => parameters[y]);

                const computedXLS = new Blob([computeOutput], {
                    type: MIME_XLS,
                });
                const xlsLink = window.URL.createObjectURL(computedXLS);
                downloadFile(xlsLink, fileName);
            }

            function downloadFile(output, fileName) {
                const link = document.createElement('a');
                document.body.appendChild(link);
                link.download = fileName;
                link.href = output;
                link.click();
            }

            function LoadListChannel(siteid) {
                let url = urlGetListChannel + siteid;

                let bodyModal = document.getElementById('bodyModal');

                bodyModal.innerHTML = "";
                axios.get(url).then(function (res) {
                    let content = "";
                    console.log(res.data.GetChannelBySiteIdResult)
                    for (let item of res.data.GetChannelBySiteIdResult) {
                        content += `<div class="custom-control custom-switch">
                                      <input type="checkbox" class="custom-control-input" id="${item.ChannelId}" data-channel="${item.ChannelId}" checked>
                                      <label class="custom-control-label" for="${item.ChannelId}">${item.ChannelName}</label>
                                    </div>`;

                        listChannel.push(item.ChannelId);
                    }
                    bodyModal.innerHTML = content;
                    AllowChangeCheckBox();
                }).catch(err => console.log(err))
            }

            function AllowChangeCheckBox() {
                let checkBoxs = document.getElementsByClassName('custom-control-input');

                for (let checkbox of checkBoxs) {
                    checkbox.addEventListener('change', function (e) {
                        if (checkbox.checked == true) {
                            let cardElement = document.getElementById(`card-${checkbox.dataset.channel}`);
                            listChannel.push(checkbox.dataset.channel);
                            if (cardElement.classList.contains('d-none')) {
                                cardElement.classList.remove('d-none');
                            }
                            for (let graph of chart.graphs) {
                                if (graph.id == checkbox.dataset.channel) {
                                    chart.showGraph(graph);
                                    break;
                                }
                            }
                        }
                        else {
                            let cardElement = document.getElementById(`card-${checkbox.dataset.channel}`);
                            if (!cardElement.classList.contains('d-none')) {
                                cardElement.classList.add('d-none');
                            }

                            for (let graph of chart.graphs) {
                                if (graph.id == checkbox.dataset.channel) {
                                    chart.hideGraph(graph);
                                    break;
                                }
                            }

                            let indexOfElement = listChannel.indexOf(checkbox.dataset.channel);
                            if (indexOfElement > -1) {
                                listChannel.splice(indexOfElement, 1);
                            }
                        }

                    })
                }
            }

            $(document).ready(function () {
                var cboSite = $find('<%= cboMonitoringSites.ClientID %>');
                var dtmStart = $find('<%= dtmStart.ClientID %>');
                var dtmEnd = $find('<%= dtmEnd.ClientID %>');
                var site = cboSite.get_text();
                var start = GetUnixStartDate();
                var end = GetUnixEndDate();
                LoadListChannel(site);
                CardsLoad(site, start, end);
                drawChart(site, start, end);
                $('#btnExportXls').off('click').on('click', function () {
                    exportToXLS(cboSite.get_text(), table, cboSite.get_text());
                })
                setInterval(function () {
                    if (dtmStart.get_selectedDate() == null && dtmEnd.get_selectedDate() == null) {
                        CardsLoad(site, start, end);
                        drawChart(site, start, end);
                    }
                }, 30000);
                //$('#dataTable').resize()
            })

            function OnClientSelectedIndexChanged(sender, eventArgs) {
                var newItem = eventArgs.get_item();

                var cboSite = $find('<%= cboMonitoringSites.ClientID %>');
                var dtmStart = $find('<%= dtmStart.ClientID %>');
                var dtmEnd = $find('<%= dtmEnd.ClientID %>');
                var site = cboSite.get_text();
                var start = GetUnixStartDate();
                var end = GetUnixEndDate();
                LoadListChannel(site);
                CardsLoad(site, start, end);
                drawChart(site, start, end);
                $('#btnExportXls').off('click').on('click', function () {
                    exportToXLS(cboSite.get_text(), table, cboSite.get_text());
                })
                setInterval(function () {
                    if (dtmStart.get_selectedDate() == null && dtmEnd.get_selectedDate() == null) {
                        CardsLoad(site, start, end);
                        drawChart(site, start, end);
                    }
                }, 30000);
            }

            function GetStatus(InstantStatus) {
                var status = 'error';
                switch (InstantStatus) {
                    case '00': status = 'success'; break;
                    case '01': status = 'warning'; break;
                }
                return status;
            }


            ////Support
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
