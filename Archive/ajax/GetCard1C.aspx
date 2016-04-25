<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetCard1C.aspx.cs" Inherits="WebArchiveR6.GetCard1C" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
		body {
			background: #F5F5F5;
			font-size: 12px;
			padding: 1px;
			display: block;
			margin: 8px;
			font-family:Tahoma;
		}
		a{text-decoration: none;color: #000000;}
		table{border-collapse: collapse;border-spacing:0;}
		td,th {padding: 8px;}
		th {font-size: 16px;padding: 18px;}
		td {border: solid 1px #79b7e7;}
		.col{background-color: #eaf4fd;font-weight: 600;}
	</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <%=result %>
    </div>
    </form>
</body>
</html>
