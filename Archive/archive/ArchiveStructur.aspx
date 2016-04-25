<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ArchiveStructur.aspx.cs" Inherits="WebArchiveR6.ArchiveStructur" %>

<%@ Register Assembly="Trirand.Web, Version=4.5.4.0, Culture=neutral, PublicKeyToken=e2819dc449af3295"
    Namespace="Trirand.Web.UI.WebControls" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Site.master" %>
<%@ Register assembly="Trirand.Web" namespace="Trirand.Web.UI.WebControls" tagprefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="server">
    <form id="form1" runat="server"><div>&nbsp;</div>
        <cc1:JQGrid ID="jqArchiveStructur" runat="server" Width="900px" ColumnReordering="True"
            Height="600px" EnableViewState="False" OnDataRequesting="jqArchiveStructur_DataRequesting" 
             >
            <Columns>
                <cc1:JQGridColumn DataField="id" Width="58" Editable="False" PrimaryKey="True" DataType="Int" HeaderText="Код ЭА" />
                <cc1:JQGridColumn DataField="treetext" Width="108" DataType="System.String" HeaderText="Номер докум." />
                <cc1:JQGridColumn DataField="doctree" Width="230" DataType="System.String" HeaderText="Документ" />
                <cc1:JQGridColumn DataField="date_doc" Width="63" DataType="System.String" HeaderText="Дата док." />
                <cc1:JQGridColumn DataField="doctype" Width="230" DataType="System.String" HeaderText="Вид докум." />
                <cc1:JQGridColumn DataField="frm" Width="230" DataType="System.String" HeaderText="Контрагент" />
                <cc1:JQGridColumn DataField="summ" Width="100" DataType="System.String" HeaderText="Сумма" DataFormatString="{0:n2}" />
                <cc1:JQGridColumn DataField="docpack" Width="58" DataType="String" HeaderText="Пакет" />
                <cc1:JQGridColumn DataField="prjcode" Width="58" DataType="String" HeaderText="Код проекта" />
                <cc1:JQGridColumn DataField="perf" Width="100" DataType="System.String" HeaderText="Исполнитель" />
                <cc1:JQGridColumn DataField="state" Width="100" DataType="System.String" HeaderText="Состояние" />
                <cc1:JQGridColumn DataField="content" Width="100" DataType="System.String" HeaderText="Содержание" />
                <cc1:JQGridColumn DataField="depart" Width="100" DataType="System.String" HeaderText="Получатель" />
                <cc1:JQGridColumn DataField="prim" Width="100" DataType="System.String" HeaderText="Примечание" />
            </Columns>
            <AppearanceSettings Caption="Структура" />
            <TreeGridSettings Enabled="true"  />
            <ToolBarSettings>
                <CustomButtons>
                   <cc1:JQGridToolBarButton OnClick="ToCard" Text="Карточка &nbsp;&nbsp;" ToolTip="Перейти к соотвествующей карточке документа"/>
                </CustomButtons>
            </ToolBarSettings>

            <ClientSideEvents RowDoubleClick="ToCard" />

        </cc1:JQGrid>
    </form>
    <script type='text/javascript'>
        $(window).bind('resize', function () 
        {
            var grid = jQuery('#cph_jqArchiveStructur');
            grid.setGridWidth($(window).width()-20);
            grid.setGridHeight($(window).height()-160);
        }).trigger('resize');
        
        function ToCard() {
            var grid = jQuery('#cph_jqArchiveStructur');
            var id = grid.jqGrid('getGridParam', 'selrow');
            if (id > 0) 
            {
                window.open('/archive/' + '<% Response.Write(Master.cur_basename);%>' + '/srch?id=' + id + '&act=view'); 
            } 
            else alert('Выберите запись'); 
            return false;
        }

        jQuery(document).ready(function () {
            $(window).resize();
        });
    </script>
</asp:Content>
