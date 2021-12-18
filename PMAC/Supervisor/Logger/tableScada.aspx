<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Supervisor/MasterPage.master" CodeFile="tableScada.aspx.cs" Inherits="Supervisor_Logger_tableScada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .bb {
            font-weight: bold;
            color: #0A5BC4;
        }


        #loading {
            width: 50px;
            height: 50px
        }

            #loading.hide {
                display: none;
            }

            #loading.show {
                display: inline
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        <asp:Label ID="lbTitle" runat="server" Text="Bảng dữ liệu tổng quan điểm Scada"></asp:Label></h2>

    <div id="tablePlaceHolder">
        <table id="example" class=" table table-hover" style="width: 100%; text-align: center; font-weight: 500">
            <thead>
                <tr>
                    <th>STT</th>
                    <th>Vị trí</th>
                    <th>Tên điểm</th>
                    <th>Áp Lực trước (m)
                    </th>
                    <th>Áp lực sau (m)</th>
                    <th>Lưu lượng thuận (m <sup>3</sup>/h)</th>
                    <th>Lưu lượng nghịch (m <sup>3</sup>/h)</th>
                    <th>Pin (V)</th>
                    <th>Nhiệt Độ (V)</th>
                    <th>Index (m <sup>3</sup>/h)</th>
                </tr>
            </thead>
            <tbody id="body">
                <div class="loading">
                    <img id="loading" class="hide" src="../../_imgs/ajax-loader.gif" />
                </div>
            </tbody>
            <tfoot>
                <tr>
                    <th>STT</th>
                    <th>Vị trí</th>
                    <th>Tên điểm</th>
                    <th>Áp Lực trước (m)
                    </th>
                    <th>Áp lực sau (m)</th>
                    <th>Lưu lượng thuận (m <sup>3</sup>/h)</th>
                    <th>Lưu lượng nghịch (m <sup>3</sup>/h)</th>
                   <th>Pin (V)</th>
                    <th>Nhiệt Độ (V)</th>
                    <th>Index (m <sup>3</sup>/h)</th>
                </tr>
            </tfoot>
        </table>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="https://cdn.jsdelivr.net/npm/axios/dist/axios.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.23/js/jquery.dataTables.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.7.0/js/dataTables.buttons.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.7.0/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.7.0/js/buttons.print.min.js"></script>

    <script>
        let loadingElement = document.getElementById('loading');
        let body = document.getElementById('body');

        var hostname = window.location.origin;
        if (hostname.indexOf("localhost") < 0)
            hostname = hostname + "/VivaServices/Service1.svc";
        else
            hostname = "http://localhost:63111/Service1.svc";

        function getData() {
            let url = `${hostname}/getsitesbyuid/admin`;
            let order = 0;
            let length = 0;

            loadingElement.classList.add('show');
            loadingElement.classList.remove('hide');

            axios.get(url).then(res => {
                length = res.data.GetSitesByUidResult.length;
                for (let site of res.data.GetSitesByUidResult) {
                    if (site.LoggerId != null && site.LoggerId != undefined && site.LoggerId.toString().trim() != "") {
                        let url2 = `${hostname}/GetChannels/${site.LoggerId}`;

                        axios.get(url2).then(res => {
                            let obj = {};
                            obj.STT = ++order;
                            obj.Location = site.Location;
                            obj.SiteAliasName = site.SiteAliasName;
                            obj.Pressure1 = "";
                            obj.Pressure2 = "";
                            obj.Forward = "";
                            obj.Reverse = "";
                            obj.Battery = "";
                            obj.Temp = "";
                            obj.Index = "";

                            for (let channel of res.data.GetChannelsResult) {

                                if (channel.Press1 == true) {
                                    if (channel.Value != null) {
                                        obj.Pressure1 = channel.Value;
                                    }
                                }
                                else if (channel.Press2 == true) {
                                    if (channel.Value != null) {
                                        obj.Pressure2 = channel.Value;

                                    }

                                }
                                else if (channel.Flow == true) {
                                    if (channel.ChannelId != null && channel.ChannelId != undefined && channel.ChannelId.trim() != "") {
                                        let splitStringChannel = channel.ChannelId.split('_')[1];
                                        if (splitStringChannel == "02") {
                                            if (channel.Value != null) {
                                                obj.Forward = channel.Value;
                                                if (channel.LastIndex != null) {
                                                    if (obj.Index == "") {
                                                        obj.Index = channel.LastIndex;
                                                    }
                                                    else {
                                                        obj.Index += channel.LastIndex;
                                                    }
                                                }

                                            }

                                        }
                                        else if (splitStringChannel == "03") {
                                            if (channel.Value != null) {
                                                obj.Reverse = channel.Value;
                                                if (channel.LastIndex != null) {
                                                    if (obj.Index == "") {
                                                        obj.Index = channel.LastIndex;
                                                    }
                                                    else {
                                                        obj.Index += channel.LastIndex;
                                                    }
                                                }

                                            }

                                        }
                                    }

                                }
                                else if (channel.Unit == "C") {
                                    if (channel.Value != null) {
                                        obj.Temp = channel.Value;
                                    }
                                }
                                else if (channel.Unit == "V") {
                                    if (channel.Value != null) {
                                        obj.Battery = channel.Value;
                                    }
                                }
                            }
                            loadingElement.classList.add('hide');
                            loadingElement.classList.remove('show');

                            loadDataToTable(obj);
                            if (order == length) {
                                createDataTable();
                            }
                        }).catch(err => console.log(err));
                    }
                }



            }).catch(err => console.log(err))
        }

        function loadDataToTable(data) {

            let trNode = document.createElement("tr");
            for (let pro in data) {

                let tdNode = document.createElement('td');
                let tdTextNode = document.createTextNode(data[pro]);
                tdNode.appendChild(tdTextNode);
                trNode.appendChild(tdNode);
            }

            body.appendChild(trNode);
        }

        function createDataTable() {
            $('#example').DataTable({
                initComplete: function () {
                    this.api().columns([2]).every(function () {
                        var column = this;
                        var select = $('<select><option value=""></option></select>')
                            .appendTo($(column.footer()).empty())
                            .on('change', function () {
                                var val = $.fn.dataTable.util.escapeRegex(
                                    $(this).val()
                                );
                                column
                                    .search(val ? '^' + val + '$' : '', true, false)
                                    .draw();
                            });
                        column.data().unique().sort().each(function (d, j) {
                            select.append('<option value="' + d + '">' + d + '</option>')
                        });
                    });
                },
                dom: 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Bang_Du_Lieu_Chi_Tiet'
                    },
                    {
                        extend: 'csvHtml5',
                        title: 'Bang_Du_Lieu_Chi_Tiet'
                    },
                    {
                        extend: 'pdfHtml5',
                        title: 'Bang_Du_Lieu_Chi_Tiet'
                    }
                ]
            });
        }

        getData();

    </script>
</asp:Content>
