<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Settings.aspx.cs" Inherits="ArchNet.Settings" %>

<%@ MasterType VirtualPath="~/Site.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="server">
    <div class="row">
        <div class="col-md-3">
        </div>
        <div class="col-md-6">
            <br />
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    
                        Сервис / Настройки
                    <button type="button" style="background: transparent;border: 0; padding: 0;float: right;" onclick="window.close()" tittle="Закрыть эту страницу"><span class="hi hi-remove"></span></button>
                </div>
                <div class="panel-body">
                    При двойном клике в списке &nbsp;
                    <div class="btn-group">
                        <button type="button" id="double_click_action" class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown"
                            aria-expanded="false">
                            <%
                                string buf = ArchNet.faFunc.GetUserSetting("double_click_action"); 
                                if (buf == "file") Response.Write("Открывать файл");
                                else Response.Write("Открывать карточку");  
                            %>
                            <span class="caret"></span>
                        </button>
                        <ul class="dropdown-menu" role="menu">
                            <li><a href="#" onclick="$('#double_click_action').html('Открывать файл <span class=\'caret\'></span>');Save('double_click_action','file');">Открывать файл (По умолчанию)</a></li>
                            <li><a href="#" onclick="$('#double_click_action').html('Открывать карточку <span class=\'caret\'></span>');Save('double_click_action','card');">Открывать карточку</a></li>
                        </ul>
                    </div>
                    &nbsp;<span id="double_click_action_saved" class="gi gi-ok_2" style="display:none;color:#007cb0;"></span>
                    <hr />
                </div>
            </div>
        </div>
        <div class="col-md-3">
        </div>
    </div>
    <script type='text/javascript'>
    function Save(k,v){
        $.ajax({ url: '/ajax/setsetting.aspx?key=' + k + '&value=' + escape(v), type: 'POST', success: function (html) { $('#' + k + '_saved').show().fadeOut(3000);}});
    }
    </script>
</asp:Content>
