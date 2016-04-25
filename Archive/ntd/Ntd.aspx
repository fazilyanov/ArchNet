<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ntd.aspx.cs" Inherits="WebArchiveR6.Ntd" %>

<%@ MasterType VirtualPath="~/Site.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="server">
    <form id="form1" runat="server" method="post" enctype="multipart/form-data"></form>
    <%=list.JS%>
    <style type="text/css">
    .ui-jqgrid tr.jqgrow td 
{ 
    word-wrap: break-word; /* IE 5.5+ and CSS3 */ 
    white-space: pre-wrap; /* CSS3 */ 
    white-space: -moz-pre-wrap; /* Mozilla, since 1999 */
     white-space: -pre-wrap; /* Opera 4-6 */ 
     white-space: -o-pre-wrap; /* Opera 7 */ 
     overflow: hidden; 
     height: auto; 
     vertical-align: middle; 
     padding-top: 3px; 
     padding-bottom: 3px; 

}   
 th.ui-th-column div {  word-wrap: break-word; /* IE 5.5+ and CSS3 */ white-space: pre-wrap; /* CSS3 */ white-space: -moz-pre-wrap; /* Mozilla, since 1999 */ white-space: -pre-wrap; /* Opera 4-6 */ white-space: -o-pre-wrap; /* Opera 7 */ overflow: hidden; height: auto; vertical-align: middle; padding-top: 3px; padding-bottom: 3px;  }
        </style>
</asp:Content>