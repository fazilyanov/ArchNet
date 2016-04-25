<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NtdStart.aspx.cs" Inherits="WebArchiveR6.NtdStart" %>
<%@ MasterType VirtualPath="~/Site.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="server">
    <div class="panel-body">
        <form id="form1" runat="server">
            <div class="row">
                &nbsp;&nbsp;
            </div>
            <div class="row">
                &nbsp;
            </div>
            <div class="row">
                <div class="col-sm-3"></div>
                <div class="col-sm-4">
                    <div class="list-group">
                        <a href="#" class="list-group-item active" style="padding:5px 0px 5px 10px;font-size:16px;">Категории:</a>
                        <%Response.Write(Session["ntd_category_list"]);%>
                    </div>
                </div>
                <div class="col-sm-2">
                    <div class="list-group">
                        <a href="#" class="list-group-item active" style="padding:5px 0px 5px 10px; font-size:16px;" >Справочники:</a>
                        <%Response.Write(Session["ntd_sprav_list"]);%>
                    </div>
                </div>
            </div>
        </form>
         <script type='text/javascript'>
        jQuery(document).ready(function () {
            $('.navbar-brand').html('Корпоративный фонд нормативно-технической документации (Фонд НТД)');
            $('.navbar-brand').attr('href', '/ntdstart');
            $('.navbar-right').html('');
        });
    </script>
    </div>
</asp:Content>
