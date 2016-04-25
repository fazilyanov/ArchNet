<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LetGetCards.aspx.cs" Inherits="WebArchiveR6.LetGetCards" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CellPadding="4" ForeColor="#333333" 
            GridLines="Vertical" onpageindexchanged="GridView1_PageIndexChanged" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" 
            onselectedindexchanging="GridView1_SelectedIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDS1" runat="server" 
            ConnectionString="Data Source=FAZLPC7\SQLEXPRESS;Initial Catalog=archive;User ID=fazl;Password=52772;Max Pool Size=200;Connect Timeout=60;" 
            SelectCommand="SELECT id, date_upd, date_doc, id_doctype, num_doc, [content], id_frm_contr, prim, id_user, summ, docpack, id_prjcode, id_perf, id_depart FROM zao_stg_archive WHERE (id &lt;= 30)">
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
