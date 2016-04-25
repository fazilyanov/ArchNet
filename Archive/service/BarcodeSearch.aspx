<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="BarcodeSearch.aspx.cs" Inherits="WebArchiveR6.BarcodeSearch" %>

<%@ Register Assembly="Trirand.Web, Version=4.5.4.0, Culture=neutral, PublicKeyToken=e2819dc449af3295"
    Namespace="Trirand.Web.UI.WebControls" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Site.master" %>
<%@ Register assembly="Trirand.Web" namespace="Trirand.Web.UI.WebControls" tagprefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="server">
    <form id="form1" runat="server">
    <h3 class="page-header-custom">
    </h3>
    <table>
        <tr>
            <td style="vertical-align: top; padding-left: 10px;" >
                Штрихкоды для поиска
                <br />
                <asp:TextBox ID="tbIDS" runat="server"  class="form-control" Width="225" Rows="20" TextMode="MultiLine" Height="390"></asp:TextBox>
                <br />
                <asp:Button ID="btnFind" runat="server" Text="Искать" class="btn btn-xs btn-primary"
                    OnClick="btnFind_Click" />
                <br /><br />
                Результат
                <br />
                <asp:TextBox ID="result" runat="server"  class="form-control" Width="225" Rows="10" TextMode="MultiLine" Height="200"></asp:TextBox>
                
            </td>
            <td style="vertical-align: top; padding: 20px;">
                <cc1:JQGrid ID="jqBarcodeSearch" runat="server" Width="1200px" ColumnReordering="True"
                    Height="600px" EnableViewState="False" OnDataRequesting="jqBarcodeSearch_DataRequesting"
                    OnRowDeleting="jqBarcodeSearch_RowDeleting">
                    <Columns>
                        <cc1:JQGridColumn DataField="id" Width="58" Editable="False" PrimaryKey="True" DataType="Int"
                            HeaderText="Код ЭА" />
                        <cc1:JQGridColumn DataField="num_doc" Width="108" DataType="System.String" HeaderText="Номер докум." />
                        <cc1:JQGridColumn DataField="docpack" Width="58" DataType="String" HeaderText="Пакет" />
                        <cc1:JQGridColumn DataField="date_doc" Width="63" DataType="System.String" HeaderText="Дата док." />
                        <cc1:JQGridColumn DataField="doctree" Width="230" DataType="System.String" HeaderText="Документ" />
                        <cc1:JQGridColumn DataField="frm" Width="230" DataType="System.String" HeaderText="Контрагент" />
                        <cc1:JQGridColumn DataField="summ" Width="100" DataType="System.String" HeaderText="Сумма" />
                        <%--<cc1:JQGridColumn DataField="file" Width="100" DataType="System.String" HeaderText="Файл" />--%>
                        <cc1:JQGridColumn DataField="barcode" Width="100" DataType="System.String" HeaderText="Штрихкод" />
                        <cc1:JQGridColumn DataField="status" Width="100" DataType="System.String" HeaderText="Статус" />
                        <cc1:JQGridColumn DataField="id_frm_contr_1c" Width="110" DataType="System.String" HeaderText="ID 1C Контрагента" />
                       
                    </Columns>
                    <EditDialogSettings Modal="True" Width="520" />
                    <AddDialogSettings Modal="True" Width="520" />
                    <PagerSettings PageSize="5000" PageSizeOptions="[5000]" />
                    <ToolBarSettings ShowDeleteButton="True">
                        <CustomButtons>
                            <cc1:JQGridToolBarButton OnClick="ExportToExcel" Text="Экспорт &nbsp;&nbsp;" ToolTip="Выгрузка данных в Excel" />
                        </CustomButtons>
                        <CustomButtons>
                            <cc1:JQGridToolBarButton OnClick="ClearGrid" Text="Очистить &nbsp;&nbsp;" ToolTip="Полностью очистить список" />
                        </CustomButtons>
                       
                    </ToolBarSettings>
                    <AppearanceSettings Caption="Результаты поиска" ShowRowNumbers="True" ShrinkToFit="False" />
                    <ExportSettings ExportDataRange="Filtered" />
                </cc1:JQGrid>
                <asp:Button runat="server" ID="ExportToExcelButton" Text="Export to Excel" OnClick="ExportToExcel"
                    CssClass="hidebutton" />
                <asp:Button runat="server" ID="ClearGridButton" Text="Clear Grid Button" OnClick="ClearGrid"
                    CssClass="hidebutton" />
                
            </td>
        </tr>
    </table>
    </form>
    <script type='text/javascript'>
       <%-- jQuery(document).ready(function () {


            grid.bind("#<%= jqBarcodeSearch.ClientID %>", function (e, data) {
                var iCol1 = 0,
                    iCol2 = 0,
                    iRow = 0;
                var cm = grid.jqGrid('getGridParam', 'colModel'),
                    i = 0,
                    l = cm.length;
                for (; i < l; i++) {
                    if (cm[i].name === 'space') {
                        iCol1 = i;
                    }
                }
                var cRows = this.rows.length;
                var iRow;
                var row;
                var className;
                for (iRow = 0; iRow < cRows; iRow++) {
                    row = this.rows[iRow];
                    var x1 = $(row.cells[iCol1]);
                    if (x1[0].innerText.trim() == '!!!') {
                        x1[0].className = 'redbgcell';
                    }
                }
            });
        });--%>

        function ExportToExcel(id) {
            var grid = jQuery("#<%= jqBarcodeSearch.ClientID %>");
            var exp_btn = jQuery("#<%= ExportToExcelButton.ClientID %>");
            exp_btn.click();
            return true;
        }
        function ClearGrid() {
            var _btn = jQuery("#<%= ClearGridButton.ClientID %>");
            _btn.click();
            return true;
        }
        <%--function SetAccept() {
            var _btn = jQuery("#<%= SetAcceptButton.ClientID %>");
            _btn.click();
            return true;
        }--%>

        function GoToFileDown() {
            var grid = jQuery("#<%= jqBarcodeSearch.ClientID %>");
            var rc = grid.jqGrid('getGridParam', 'reccount');
            var v = grid.getRowData();
            var f = '';
            if (rc > 0) {
                for (var i = 0; i < v.length; i++) {
                    f += v[i]['id'] + '=' + v[i]['file'] + '|';
                }
                f = f.substring(0, f.length - 1);

                $.ajax({
                    url: '/Archive/GetFileMultiList.aspx?b=<%= Master.cur_basename %>',
                    type: 'POST',
                    data: f,
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (html) { window.open('/Archive/GetFileMultiList.aspx?b=<%= Master.cur_basename %>'); },
                    error: function (request, status, error) { alert(request.responseText); }
                });


            }
            else alert('Нет записей для выгрузки');
            return false;
        }

    </script>
</asp:Content>
