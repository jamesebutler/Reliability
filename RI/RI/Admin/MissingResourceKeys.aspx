<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MissingResourceKeys.aspx.vb"
    Inherits="RI_Admin_MissingResourceKeys" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="_gvMissingKeys" runat="server" Width="90%">
            </asp:GridView>
            <IP:DisplayExcel ID="_displayExcel" runat="server" ButtonText="<%$RIResources:Global,DisplayExcel %>"
                ShowButton="true" />
        </div>
    </form>
</body>
</html>
