<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Person1c.aspx.cs" Inherits="WebArchiveR6.Person1c" %>
<%@ Register Assembly="Trirand.Web, Version=4.5.4.0, Culture=neutral, PublicKeyToken=e2819dc449af3295" Namespace="Trirand.Web.UI.WebControls" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Site.master" %>


<%@ Register assembly="Trirand.Web" namespace="Trirand.Web.UI.WebControls" tagprefix="cc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="server">
    <form id="form1" runat="server">
        <h3 class="page-header-custom">
            <input name="textuser1c" type="text" id="textuser1c" placeholder="Поиск" onkeydown="if(event.keyCode==13){SetTreeText($('#textuser1c').val());}"
                class="form-control" style="width: 99%; margin-bottom: 5px;" value='<%=(Session["textuser1c"] ?? "").ToString()%>'>
            <cc1:JQGrid
                ID="jqUser"
                runat="server"
                Width="600px"
                AutoWidth="True"
                ColumnReordering="True"
                Height="500px"
                EnableViewState="False"
                OnDataRequesting="jqUser_DataRequesting">
                <Columns>
                    <cc1:JQGridColumn DataField="id" Width="250" Editable="False" PrimaryKey="True" DataType="String" HeaderText="ID 1C" />
                    <cc1:JQGridColumn DataField="fio" Width="150" DataType="String" HeaderText="ФИО кр." />
                    <cc1:JQGridColumn DataField="fio_full" Width="200" DataType="String" HeaderText="ФИО" />
                    <cc1:JQGridColumn DataField="state" Width="100" DataType="String" HeaderText="Статус" />
                    <cc1:JQGridColumn DataField="id_depart" Width="250" DataType="String" HeaderText="" Visible="false" />
                    <cc1:JQGridColumn DataField="departname" Width="200" DataType="String" HeaderText="Подразделение в базе" />

                    <cc1:JQGridColumn DataField="department_ID" Width="250" DataType="String" HeaderText="Подразделение" Visible="false" />
                    <cc1:JQGridColumn DataField="department" Width="200" DataType="String" HeaderText="Подразделение" />
                    <cc1:JQGridColumn DataField="organization" Width="200" DataType="String" HeaderText="Организация" />
                    <cc1:JQGridColumn DataField="ID_1C_NP" Width="250" DataType="String" HeaderText="ID 1C Физ.лицо" />
                </Columns>
                <PagerSettings PageSize="100" PageSizeOptions="[10,20,30,50,100]" />
                <AppearanceSettings Caption="Выбор из справочника сотрудников 1С" ShrinkToFit="False" />
                <ClientSideEvents RowDoubleClick="ReturnSelected" />
            </cc1:JQGrid>
    </form>
    <script type='text/javascript'>
        $(window).bind('resize', function () {
            var grid = jQuery('#cph_jqUser');
            grid.setGridWidth($(window).width() - 8);
            grid.setGridHeight($(window).height() - 145);
        }).trigger('resize');

        jQuery(document).ready(function () { $(window).resize(); });

        function SetTreeText(text) {
            $.ajax({ url: '/ajax/setses.aspx?key=textuser1c&value=' + escape(text), type: 'POST', success: function (html) { jQuery('#cph_jqUser').trigger('reloadGrid'); } });
        }
        function ReturnSelected() {
            var grid = jQuery('#cph_jqUser');
            var id = grid.jqGrid('getGridParam', 'selrow');
            if (id != null) {
                var row = grid.getRowData(id);
                //alert(id);
                //alert(row.treetext);
                window.opener.changeBut(id, row.fio, row.fio_full, row.id_depart, row.departname);
                self.close();
            } else alert('Выберите запись');
            return false;
        }

    </script>
</asp:Content>