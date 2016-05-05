<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ArchNet.Default" %>

<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="server">
    <div class="panel-body">

<!--

░░░░░░░░░░░░────▄▄▄▄▄▄────░░░░░░░░░░░░░░░░
░░░░░░░░░░░░──▄▀░░░░░░▀▄──░░░░░░░░░░░░░░░░
░░░░░░░░░░░░─▐░░▄▀░░▀▄░░▌─░░░░░░░░░░░░░░░░
░░░░░░░░░░░░─▐░▐░░░░░░░░▌─░░░░░░░░░░░░░░░░
░░░░░░░░░░░░─▐░░▄▀▀▀▀▄░░▌─░░░░░░░░░░░░░░░░
░░░░░░░░░░░░──▀▄░░░░░░▄▀──░░░░░░░░░░░░░░░░
░░░░░░░░░░░░────▀▀▀▀▀▀────░░░░░░░░░░░░░░░░

░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░█▀▀▀▄▀▀▀█ █▀▀▀█ █▄──█ █▀▀▄ █▀▀█ █─────█░░░
░░░█───█───█ █───█ █─█─█ █──█ █▄▄█ █▄▄▄▄▄█░░░
░░░█───────█ █▄▄▄█ █──▀█ █▄▄▀ █──█ ───█───░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░

    


        <form id="form1" runat="server">
            <div class="row">
                &nbsp;&nbsp;
            </div>
            <div class="row">
                &nbsp;
            </div>
            <div class="row">

                <%if (Master.cur_basename == "dbselect" || Master.cur_basename == "")
                  { %>
                <div class="col-sm-3"></div>
                <div class="col-sm-3">
                    <div class="list-group">
                        <a href="#" class="list-group-item  navbar-default" style="font-size: 15px;">Базы:</a>
                        <%Response.Write(Session["listbase"]);%>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="list-group">
                        <a href="#" class="list-group-item  navbar-default" style="font-size: 15px;">Справочники (общие):</a>
                        <%Response.Write(Session["listsprav"]);%>
                    </div>

                </div>
                <%}
                  else //if (!string.IsNullOrEmpty(Master.cur_basename))
                  {%>
                <div class="col-sm-3"></div>
                <div class="col-sm-3">
                    <% Response.Write(Session[Master.cur_basename + "_listpage"]);%>
                </div>
                <div class="col-sm-3">
                    <div class="list-group">
                        <a href="#" class="list-group-item navbar-default" style="font-size: 15px;">Справочники:</a>
                        <%Response.Write((Session[Master.cur_basename + "_listsprav"] ?? "").ToString());%>
                        <%Response.Write((Session["listsprav"] ?? "").ToString());%>
                    </div>
                </div>
                <%}%>
            </div>
        </form>
    </div>
    -->
    <%--
    <script type="text/javascript">
        var ua = detect.parse(navigator.userAgent);
        var ver = "";
        if (ua.browser.name != "IE 11") {
            ver = "Желательно использовать последнюю <br/>версию браузера Internet Explorer - 11 <br/><b>";
        }
        ver += "Текущая система: " + ua.browser.name + " (" + ua.os.name + ")";
        $("#version").html(ver);
    </script>--%>
</asp:Content>
