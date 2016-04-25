<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckByBarcode.aspx.cs" Inherits="WebArchiveR6.CheckByBarcode" %>

<%@ Register Assembly="Trirand.Web, Version=4.5.4.0, Culture=neutral, PublicKeyToken=e2819dc449af3295"
    Namespace="Trirand.Web.UI.WebControls" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="server">
    <form id="form1" runat="server">
        <h3 class="page-header-custom"></h3>
        <div class="row">
            <div class="col-md-3" style="width: 601px; float: left;">
                <asp:TextBox ID="tbBarcode" runat="server" class="form-control" Width="200" Style="float: left;"></asp:TextBox>
                <asp:Button ID="btnAdd" runat="server" Text="Найти" class="btn btn-xs btn-primary" Style="float: left;" OnClick="btnAdd_Click" />
                <asp:Label ID="Label1" runat="server"></asp:Label>

            </div>
        </div>
        <h3 class="page-header-custom"></h3>
        <cc1:JQGrid
            ID="jqCheck"
            runat="server"
            Width="600px"
            AutoWidth="True"
            ColumnReordering="True"
            Height="500px"
            EnableViewState="False"
            OnDataRequesting="jqCheck_DataRequesting">
            <Columns>
                <cc1:JQGridColumn DataField="id" Width="100" Editable="False" PrimaryKey="True" DataType="Int" HeaderText="Код ЭА" />
                <cc1:JQGridColumn DataField="barcode" Width="150" DataType="String" HeaderText="Штрихкод" />
                <cc1:JQGridColumn DataField="date_trans" Width="250" HeaderText="Самая ранняя дата получения." DataFormatString="{0:dd.MM.yyyy}" />
                <cc1:JQGridColumn DataField="status" Width="300" DataType="System.String" HeaderText="Статус основной версии" />
                <cc1:JQGridColumn DataField="file" DataType="System.String" HeaderText="Статус основной версии" Visible="false" />
            </Columns>
            <EditDialogSettings Modal="True" Width="520" />
            <AddDialogSettings Modal="True" Width="520" />
            <PagerSettings PageSize="500" PageSizeOptions="[500]" />
            <ToolBarSettings>
                <CustomButtons>
                    <cc1:JQGridToolBarButton OnClick="ToCard" Text="Открыть карточку &nbsp;&nbsp;" ToolTip="Перейти к соотвествующей карточке документа" />
                    <cc1:JQGridToolBarButton OnClick="ToFile" Text="Открыть файл &nbsp;&nbsp;" ToolTip="Открыть файл документа" />
                    <cc1:JQGridToolBarButton OnClick="GoToFileDown" Text="Скачать все файлы &nbsp;&nbsp;" ToolTip="Выгрузить файлы соответствующие документам в списке" ButtonIcon="ui-icon-arrowthickstop-1-s" />
                    <cc1:JQGridToolBarButton OnClick="ExportToExcel" Text="Экспорт &nbsp;&nbsp;" ToolTip="Выгрузка данных в Excel" />
                </CustomButtons>
            </ToolBarSettings>
            <AppearanceSettings Caption="Поиск по штрихкоду" ShowRowNumbers="True"
                ShrinkToFit="False" />
            <ExportSettings ExportDataRange="Filtered" />
            <ClientSideEvents RowDoubleClick="ToCard" />
        </cc1:JQGrid>
        <asp:Button runat="server" ID="ExportToExcelButton" Text="Export to Excel" OnClick="ExportToExcel" CssClass="hidebutton" />
    </form>
    <script type='text/javascript'>
        jQuery(document).ready(function () { $('#cph_tbBarcode').focus(); });

        function ToCard() {
            var grid = jQuery('#cph_jqCheck');
            var id = grid.jqGrid('getGridParam', 'selrow');
            if (id > 0) {
                window.open('/archive/' + '<% Response.Write(cb);%>' + '/srch?id=' + id + '&act=view');
            }
            else alert('Выберите запись');
            return false;
        }

        function ExportToExcel(id) {
            var grid = jQuery("#<%= jqCheck.ClientID %>");
            var exp_btn = jQuery("#<%= ExportToExcelButton.ClientID %>");
            exp_btn.click();
            return true;
        }

        function GoToFileDown() {

            var grid = jQuery('#cph_jqCheck');
            var rc = grid.jqGrid('getGridParam', 'reccount');
            var v = grid.getRowData();
            var f = '';
            if (rc > 0) {
                for (var i = 0; i < v.length; i++) {
                    f += v[i]['id'] + '=' + v[i]['file'] + '|';
                }
                f = f.substring(0, f.length - 1);

                $.ajax({
                    url: '/Archive/GetFileMultiList.aspx?b=<%Response.Write(cb);%>',
                    type: 'POST',
                    data: f,
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (html) { window.open('/Archive/GetFileMultiList.aspx?b=<%Response.Write(cb);%>'); },
                    error: function (request, status, error) { alert(request.responseText); }
                });
            }
            else alert('Нет записей для выгрузки'); return false;
        }

        function ToFile() {
            var grid = jQuery('#cph_jqCheck');
            var id = grid.jqGrid('getGridParam', 'selrow');
            if (id > 0) {
                var row = grid.getRowData(id);
                var f = row.file;
                if (f != '') window.open('/Archive/GetFile.aspx?id=' + id + '&b=<%Response.Write(cb);%>' + '&f=' + f + '&k=' + $.md5(id));
                else alert('Файл не указан');
            }
            else alert('Выберите запись'); return false;
        }

    </script>
</asp:Content>
