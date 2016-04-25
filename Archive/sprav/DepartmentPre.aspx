<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DepartmentPre.aspx.cs" Inherits="WebArchiveR6.DepartmentPre" %>

<%@ Register Assembly="Trirand.Web, Version=4.5.4.0, Culture=neutral, PublicKeyToken=e2819dc449af3295" Namespace="Trirand.Web.UI.WebControls" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="server">
    <form id="form1" runat="server">
        <h3 class="page-header-custom">
            <input name="texttreepre" type="text" id="texttreepre" placeholder="Поиск" onkeydown="if(event.keyCode==13){SetTreeText($('#texttreepre').val());}"
                class="form-control" style="width: 99%; margin-bottom: 5px;" value='<%=(Session["texttreepre"] ?? "").ToString()%>'>
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
                <ToolBarSettings>
                    <CustomButtons>
                        <cc1:JQGridToolBarButton ButtonIcon="ui-icon-plus" OnClick="ToNew" Text="Новая запись &nbsp;&nbsp;" ToolTip="" />
                        <cc1:JQGridToolBarButton ButtonIcon="ui-icon-pencil" OnClick="ToEdit" Text="Редактировать &nbsp;&nbsp;" ToolTip="" />
                        <cc1:JQGridToolBarButton ButtonIcon="ui-icon-extlink" OnClick="ToLine" Text="Линейный справочник &nbsp;&nbsp;" ToolTip="" />
                    </CustomButtons>
                </ToolBarSettings>
                <TreeGridSettings Enabled="true" />
                <AppearanceSettings Caption="Справочники / Подразделения" ShrinkToFit="False" />
                <ClientSideEvents RowDoubleClick="ToEdit" />
            </cc1:JQGrid>
    </form>
    <script type='text/javascript'>
        $(window).bind('resize', function () {
            var grid = jQuery('#cph_jqDepart');
            grid.setGridWidth($(window).width() - 9);
            grid.setGridHeight($(window).height() - 155);
        }).trigger('resize');

        jQuery(document).ready(function () { $(window).resize(); });

        function ToEdit() {
            var grid = jQuery('#cph_jqDepart');
            var id = grid.jqGrid('getGridParam', 'selrow');
            if (id > 0) {
                document.location.href = ('/sprav/' + '<%= BaseName%>' + '/department?id=' + id + '&act=view');
    }
    else alert('Выберите запись');
    return false;
}

function ToNew() {
    document.location.href = ('/sprav/' + '<%= BaseName%>' + '/department?id=0&act=add');
        }

        function ToLine() {
            document.location.href = ('/sprav/' + '<%= BaseName%>' + '/department');
        }

        function SetTreeText(texttree) {
            $.ajax({ url: '/ajax/setses.aspx?key=texttreepre&value=' + escape(texttree), type: 'POST', success: function (html) { jQuery('#cph_jqDepart').trigger('reloadGrid'); } });
        }
    </script>
</asp:Content>
