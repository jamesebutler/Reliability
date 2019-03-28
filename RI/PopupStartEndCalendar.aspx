<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PopupStartEndCalendar.aspx.vb" Inherits="PopupStartEndCalendar" %>
<%@ Register Src="RI/User Controls/Common/ucDateRangeSelector.ascx" TagName="ucDateRangeSelector"
	TagPrefix="IP" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Date Range</title>
</head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>
    <ip:ucDateRangeSelector ID="UcDateRangeSelector1" runat="server" />
    </div>
    </form>
</body>
</html>
