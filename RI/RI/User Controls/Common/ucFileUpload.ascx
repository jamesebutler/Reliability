<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucFileUpload.ascx.vb"
    Inherits="RI_User_Controls_ucFileUpload" %>
&nbsp;<br />
<table style="width: 100%">
    <tr>
        <td style="width: 100px" valign=top>
            <asp:Label ID="_lblFileName" runat="server" Text="Filename:"></asp:Label></td>
        <td>
            <asp:FileUpload ID="_fileUpload" runat="server" Width="280px" /><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="_fileUpload" SetFocusOnError="True" Width="234px" Display="Dynamic">Please select a valid file</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="width: 100px">
            <asp:Label ID="_lblFileDesc" runat="server" Text="Description:"></asp:Label></td>
        <td>
            <asp:TextBox ID="_txtFileDesc" runat="server" Width="200px"></asp:TextBox>
            <asp:Button ID="_btnUpload" runat="server" Text="Button" /></td>
    </tr>
    <tr>
        <td style="width: 100px">
        </td>
        <td>
        </td>
    </tr>
     
</table>
