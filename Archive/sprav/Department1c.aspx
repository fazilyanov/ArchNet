<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Department1c.aspx.cs" Inherits="WebArchiveR6.Department1c" %>
<%@ Register Assembly="Trirand.Web, Version=4.5.4.0, Culture=neutral, PublicKeyToken=e2819dc449af3295" Namespace="Trirand.Web.UI.WebControls" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Site.master" %>


<%@ Register assembly="Trirand.Web" namespace="Trirand.Web.UI.WebControls" tagprefix="cc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="server">
    <form id="form1" runat="server">
        <h3 class="page-header-custom">
            <input name="texttree1c" type="text" id="texttree1c" placeholder="Поиск" onkeydown="if(event.keyCode==13){SetTreeText($('#texttree1c').val());}"
                class="form-control" style="width: 99%; margin-bottom: 5px;" value='<%=(Session["texttree1c"] ?? "").ToString()%>'>
            <cc1:JQGrid
                ID="jqDepart"
                runat="server"
                Width="600px"
                AutoWidth="True"
                ColumnReordering="True"
                Height="500px"
                EnableViewState="False"
                OnDataRequesting="jqDepart_DataRequesting">
                <Columns>
                    <cc1:JQGridColumn DataField="id" Width="100" Editable="False" PrimaryKey="True" DataType="String" HeaderText="ID" Visible="false" />
                    <cc1:JQGridColumn DataField="treetext" Width="600" DataType="String" HeaderText="Наименование" />
                </Columns>
                <TreeGridSettings Enabled="true" />
                <AppearanceSettings Caption="Выбор из структуры 1С" ShrinkToFit="False" />
                <ClientSideEvents RowDoubleClick="ReturnSelected" />
            </cc1:JQGrid>
    </form>
    <script type='text/javascript'>
        $(window).bind('resize', function () {
            var grid = jQuery('#cph_jqDepart');
            grid.setGridWidth($(window).width() - 8);
            grid.setGridHeight($(window).height() - 145);
        }).trigger('resize');

        jQuery(document).ready(function () { $(window).resize(); });

        function SetTreeText(texttree) {
            $.ajax({ url: '/ajax/setses.aspx?key=texttree1c&value=' + escape(texttree), type: 'POST', success: function (html) { jQuery('#cph_jqDepart').trigger('reloadGrid'); } });
        }
        function ReturnSelected() {
            var grid = jQuery('#cph_jqDepart');
            var id = grid.jqGrid('getGridParam', 'selrow');
            if (id != null) {
                var row = grid.getRowData(id);
                //alert(id);
                //alert(row.treetext);
                window.opener.changeBut(id, row.treetext);
                self.close();
            } else alert('Выберите запись');
            return false;
        }

    </script>
</asp:Content>