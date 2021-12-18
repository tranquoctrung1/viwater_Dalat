<%@ Page Language="C#" MasterPageFile="~/Supervisor/MasterPage.master" AutoEventWireup="true" CodeFile="TableDetailLoggerNP.aspx.cs" Inherits="Supervisor_Logger_TableDetailLoggerNP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <style type="text/css">
        .auto-style1 {
            width: 100%;
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

    #body tr.disconnect 
    {
        background: #ff7675;
    }
    #example thead, #example tfoot
    {
        color: #0984e3;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Bảng dữ liệu chi tiết
    </h2>
    <div class="container-fluid2">
        <div class="row">
            <div class="col">
                <table id="example" class=" table table-hover"  style="width: 100%; text-align: center; font-weight: 500">
                    <thead>
                        <tr>
                            <th>STT</th>
                            <th>Vị trí</th>
                            <th>Thời gian truyền dữ liệu</th>
                            <th>Trạng thái
                            </th>
                            <th>P01 (bar)</th>
                            <th>P02 (bar)</th>
                            <th>BAT (V)</th>
                            <th>P04 (bar)</th>
                            <th>Flow01 (m<sup>3</sup>/h)</th>
                            <th>Flow02 (m<sup>3</sup>/h)</th>
                            <th>Pulse01 (m<sup>3</sup>)</th>
                            <th>Pulse02 (m<sup>3</sup>)</th>
                        </tr>
                    </thead>
                    <tbody id="body" >
                        <div class="loading">
                            <img id="loading" class="hide" src="../../_imgs/ajax-loader.gif" />
                        </div>
                    </tbody>
                    <tfoot>
                        <tr>
                            <th>STT</th>
                            <th>Vị trí</th>
                            <th>Thời gian truyền dữ liệu</th>
                            <th>Trạng thái
                            </th>
                            <th>P01 (bar)</th>
                            <th>P02 (bar)</th>
                            <th>BAT (V)</th>
                            <th>P04 (bar)</th>
                            <th>Flow01 (m<sup>3</sup>/h)</th>
                            <th>Flow02 (m<sup>3</sup>/h)</th>
                            <th>Pulse01 (m<sup>3</sup>)</th>
                            <th>Pulse02 (m<sup>3</sup>)</th>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
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
        //let reload = document.getElementById('reload');
        let table;
        let data = [];
        function checkExistsData(data) {
            if (data.length > 0)
                return true;
            return false;
        }
        function isEmpty(obj) {
            for (var prop in obj) {
                if (obj.hasOwnProperty(prop))
                    return false;
            }
            return true;
        }

        function getData() {
            loadingElement.classList.add('show');
            loadingElement.classList.remove('hide');

            var hostname = window.location.origin;
            if (hostname.indexOf("localhost") < 0)
                hostname = hostname + "/VivaServices/";
            else
                hostname = "http://localhost:63111";
            let url = `${hostname}/Service1.svc/GetListSiteForDetailTable`;

            axios.get(url).then((res) => {
                loadingElement.classList.add('hide');
                loadingElement.classList.remove('show');

                createBody(res.data.GetListSiteForDetailTableResult);
                //$('#example').DataTable({
                //    "pagingType": "full_numbers"
                //});
                $('#example').DataTable({
                    initComplete: function () {
                        this.api().columns([3]).every(function () {
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
            }).catch(err => console.log(err))
        }

        function createBody(data) {
            let body = document.getElementById('body');
            let content = "";
            if (checkExistsData(data)) {
                for (let item of data) {
                    let contenttd = "";
                    let classColor = false;
                    if (isEmpty(item) == false && item != null && item != undefined) {
                        contenttd += `<td>${CatchNullToBlank(item.NumberOrdered)}</td>`;
                        contenttd += `<td>${CatchNullToBlank(item.Location)}</td>`;
                        contenttd += ` <td> ${convertDateTime(item.TimeStamp)}</td>`;
                        if (item.Status == true) {
                            contenttd += `<td>Disconnected</td>`;
                            classColor = true;
                        }
                        else if (item.Status == false) {
                            contenttd += `<td>Connected</td>`;
                            classColor = false;
                        }
                        else {
                            contenttd += `<td></td>`;
                            classColor = false;
                        }
                        contenttd += `<td>${CatchNullToBlank(item.P01)}</td>`;
                        contenttd += `<td>${CatchNullToBlank(item.P02)}</td>`;
                        contenttd += `<td>${CatchNullToBlank(item.Bat)}</td>`;
                        contenttd += `<td>${CatchNullToBlank(item.P04)}</td>`;
                        contenttd += `<td>${CatchNullToBlank(item.Flow01)}</td>`;
                        contenttd += `<td>${CatchNullToBlank(item.Flow02)}</td>`;
                        contenttd += `<td>${CatchNullToBlank(item.Pulse01)}</td>`;
                        contenttd += `<td>${CatchNullToBlank(item.Pulse02)}</td>`;
                    }

                    if (classColor == true) {
                        content += `<tr class="disconnect">${contenttd}</tr>`
                    }
                    else {
                        content += `<tr>${contenttd}</tr>`
                    }
                }
            }
            else {
                content = `<tr><td colspan="12">Không có dữ liệu</td</tr>`
            }
            body.innerHTML = content;
        }


        function convertDateTime(datetime) {
            if (datetime != null && datetime != undefined) {

                let date = new Date(parseInt(datetime.substr(6)));

                let year = date.getFullYear();
                let month =
                    date.getMonth() + 1 >= 10
                        ? date.getMonth() + 1
                        : `0${date.getMonth() + 1}`;
                let day = date.getDate() >= 10 ? date.getDate() : `0${date.getDate()}`;
                let hours = date.getHours() >= 10 ? date.getHours() : `0${date.getHours()}`;
                let minute =
                    date.getMinutes() >= 10 ? date.getMinutes() : `0${date.getMinutes()}`;
                let second =
                    date.getSeconds() >= 10 ? date.getSeconds() : `0${date.getSeconds()}`;

                return `${day}/${month}/${year} ${hours}:${minute}:${second}`;
            }
            return "";
        }

        getData();

        function CatchNullToBlank(data) {
            try {
                return data.toString();
            }
            catch (err) {
                return "";
            }
        }

    </script>
</asp:Content>
