<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ChangeDoctype.aspx.cs" Inherits="WebArchiveR6.ChangeDoctype" %>
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

               
               
                <div class="col-sm-4"></div>
                <div class="col-sm-3">
                    <%Response.Write(list);%>
                </div>
                <div class="col-sm-3">
                </div>
               
            </div>
        </form>
    </div>
    <%--<div id="version" style="position: absolute; right: 10px; bottom: 5px; font-size: 12px;"></div>
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
