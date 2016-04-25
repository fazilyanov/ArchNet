<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="ArchNet.ErrorPages.FatalError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="server">
<div class="alert alert-danger" style="padding:30px;margin:50px;"><% Response.Write(_message); %><br/><br/>
<button type="button" class="btn btn-default" onclick="history.back();">
  <span class="glyphicon glyphicon-arrow-left"></span> Назад
</button></div>
</asp:Content>
