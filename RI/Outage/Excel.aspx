<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="Excel.aspx.vb" Inherits="Excel"%>
 <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Export to Excel</title>
    <script type="text/javascript" language="javascript">
        function closeWindow(){
            window.close();
        }
        closeWindow();
    </script>
</head>
<body>
    <!--<form id="form1" runat="server">-->
        <asp:GridView ID="_grdExcel" runat="server" >
            <FooterStyle BackColor="#CCCCCC" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
    <!--</form>-->
</body>
</html>
