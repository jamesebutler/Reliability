<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GanttViewerAnyChart.aspx.vb" Inherits="Outage_GanttViewerAnyChart" %>

<%@ Register Src="../RI/User Controls/Common/ucOutageGanttChart.ascx" TagName="ucOutageGanttChart"
	TagPrefix="IP" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
  <link href="~/App_Themes/Print.css" rel="stylesheet" media="print" type="text/css" />	
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
		<IP:ucOutageGanttChart Visible="false" ID="_OutageGanttChart" runat="server" />
    </div>
    </form>
</body>
</html>
