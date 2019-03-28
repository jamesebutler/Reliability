<%@ Control Language="VB" CodeFile="ucSwapListBox.ascx.vb" Inherits="ucSwapList" %>
<table cellpadding="2" cellspacing="2" border="0" width="100%">
    <tr>
        <td width="40%">
            <asp:Label ID="_lblAllFields" Width="200px" runat="server" Text="<%$RIResources:GLOBAL,availablefields %>"></asp:Label></td>
        <td width="20%">
        </td>
        <td width="40%">
            <asp:Label ID="_lblSelectedFields" Width="200px" runat="server" Text="<%$RIResources:GLOBAL,selectedfields %>"></asp:Label></td>
    </tr>
    <tr>
        <td width="40%">
            <asp:ListBox Width="100%" ID="_lbAllFields" SelectionMode="Multiple" runat="server"
                Rows="10"></asp:ListBox></td>
        <td valign="middle" align="center" width="20%">
            <asp:Button ID="_btnMoveSelected" runat="server" Text="<%$RIResources:BUTTONTEXT,MoveSelected %>"
                Width="150" /><br />
            <asp:Button ID="_btnMoveAll" runat="server" Text="<%$RIResources:BUTTONTEXT,MoveAll %>"
                Width="150" /><br />
            <br />
            <asp:Button ID="_btnRemoveSelected" runat="server" Text="<%$RIResources:BUTTONTEXT,RemoveSelected %>"
                Width="150" /><br />
            <asp:Button ID="_btnRemoveAll" runat="server" Text="<%$RIResources:BUTTONTEXT,RemoveAll %>"
                Width="150" /><br />
        </td>
        <td width="40%">
            <asp:ListBox Width="100%" ID="_lbSelectedFields" SelectionMode="Multiple" runat="server"
                Rows="10"></asp:ListBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
        </td>
        <td width="40%" align="center">
            <asp:Button ID="_btnMoveUp" runat="server" Text="<%$RIResources:BUTTONTEXT,MoveUp %>" />&nbsp;<asp:Button
                ID="_btnMoveDown" Text="<%$RIResources:BUTTONTEXT,MoveDown %>" runat="server" /></td>
    </tr>
</table>
<asp:HiddenField ID="_hdfAllFields" runat="server" />
<asp:HiddenField ID="_hdSelectedFields" runat="server" />
