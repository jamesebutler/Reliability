<%@ Page Language="VB" AutoEventWireup="false" CodeFile="KeepSessionAlive.aspx.vb"
    Inherits="KeepSessionAlive" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Timer</title>
</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:Timer ID="_tmrKeepAlive" runat="server" Interval="600000" />
        <asp:UpdatePanel ID="_udpTimer" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Label ID="_lblTime" Font-Names="Verdana" Font-Size="0.8em" runat="server" Visible="false"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
