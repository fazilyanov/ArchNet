<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FillByBarcode.aspx.cs" Inherits="WebArchiveR6.FillByBarcode" %>
<%@ Register Assembly="Trirand.Web, Version=4.5.4.0, Culture=neutral, PublicKeyToken=e2819dc449af3295"
    Namespace="Trirand.Web.UI.WebControls" TagPrefix="cc1" %>
<%@MasterType VirtualPath="~/Site.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="server">

    <form id="form1" runat="server">
    <h3 class="page-header-custom"></h3>
    <div class="row">
    <div class="col-md-3" style="width: 601px;float:left;">
    <asp:TextBox ID="tbBarcode" runat="server"  class="form-control" Width="200" style="float:left;"></asp:TextBox>
    <asp:Button ID="btnAdd" runat="server" Text="Добавить" class="btn btn-xs btn-primary" style="float:left;" onclick="btnAdd_Click" />
    <asp:Label  ID="Label1" runat="server"></asp:Label>

    </div></div>
    <h3 class="page-header-custom"></h3>
    <cc1:JQGrid 
        ID="jqBarcode" 
        runat="server" 
        Width="600px"
        AutoWidth="True" 
        ColumnReordering="True"
        Height="500px" 
        EnableViewState="False" 
        ondatarequesting="jqBarcode_DataRequesting" 
        onrowdeleting="jqBarcode_RowDeleting">
        <Columns>
            <cc1:JQGridColumn DataField="id" Width="100" Editable="False" PrimaryKey="True"  DataType="Int" HeaderText="Код ЭА"/>
            <cc1:JQGridColumn DataField="barcode" Width="150" DataType="String" HeaderText="Штрихкод"/>
            <cc1:JQGridColumn DataField="date_doc" Width="250" DataType="System.String" HeaderText="Дата док."/>
            <cc1:JQGridColumn DataField="doctree" Width="300" DataType="System.String" HeaderText="Документ"/>
            <cc1:JQGridColumn DataField="frm" Width="250" DataType="System.String" HeaderText="Контрагент"/>    
            <cc1:JQGridColumn DataField="num_doc" Width="250" DataType="System.String" HeaderText="Номер докум."/>    
        </Columns>
        
        <EditDialogSettings Modal="True" Width="520" />
        <AddDialogSettings Modal="True" Width= "520"  />
        <PagerSettings PageSize="500" PageSizeOptions="[500]" />
        <ToolBarSettings ShowDeleteButton="True">
        <CustomButtons>
                <cc1:JQGridToolBarButton OnClick="ExportToExcel" Text="Экспорт &nbsp;&nbsp;" ToolTip="Выгрузка данных в Excel"/>
            </CustomButtons>
        </ToolBarSettings>
        <AppearanceSettings Caption="Поиск по штрихкоду"  ShowRowNumbers="True"
            ShrinkToFit="False" />
        <ExportSettings ExportDataRange="Filtered" />
    </cc1:JQGrid>
     <asp:Button runat="server" ID="ExportToExcelButton" Text="Export to Excel" OnClick="ExportToExcel" CssClass="hidebutton" />
    </form>
    <script type='text/javascript'>
        jQuery(document).ready(function () { $('#cph_tbBarcode').focus(); });

        function ExportToExcel(id) {
            var grid = jQuery("#<%= jqBarcode.ClientID %>");
            var exp_btn = jQuery("#<%= ExportToExcelButton.ClientID %>");
            exp_btn.click();
             return true;
        }
    </script>
</asp:Content>
