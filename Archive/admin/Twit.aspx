<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Twit.aspx.cs" Inherits="WebArchiveR6.Twit" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="server">
    <div class="panel-body">
        <form id="form1" runat="server">
            <asp:TextBox ID="tb1" runat="server" class="form-control" style="width:600px;"></asp:TextBox>
            <br />
            <asp:Button ID="add_as_ver" runat="server" Text="Сохранить" class="btn btn-xs btn-primary" Style="width: 133px;" OnClick="twitthis" />
        </form>
        <br />
        <input style="width:100%;" value="В период с 9:00 до 9:05 (МСК) приложение будет обновлено, <br /> во избежание потери данных, сохраните все несохраненные данные." onclick="$('#cph_tb1').val($(this).val());"/>
        <input style="width:100%;" value="В период с 14:00 до 14:05 (МСК) приложение будет обновлено, <br /> во избежание потери данных,  сохраните все несохраненные данные." onclick="$('#cph_tb1').val($(this).val());" />
    </div>
</asp:Content>

