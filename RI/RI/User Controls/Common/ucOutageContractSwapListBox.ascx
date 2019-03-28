<%@ Control Language="VB" CodeFile="ucOutageContractSwapListBox.ascx.vb" Inherits="ucContractorSwapList" %>
<table cellpadding="2" cellspacing="2" border="0" width="100%">
    <tr>
        <td style="width: 40%">
            <asp:Label ID="_lblAllFields" Width="200px" runat="server" Text="Available Contractors"></asp:Label></td>
        <td style="width: 20%">
        </td>
        <td style="width: 40%">
            <asp:Label ID="_lblSelectedFields" Width="200px" runat="server" Text="Selected Contractors"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 40%" valign="bottom">
            <asp:ListBox Width="100%" ID="_lbAllFields" SelectionMode="Multiple" runat="server"
                Rows="12"></asp:ListBox></td>
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
</table>
<asp:HiddenField ID="_hdfAllFields" runat="server" />
<asp:HiddenField ID="_hdSelectedFields" runat="server" />
<asp:HiddenField ID="_hdSelectedSecondaryFields" runat="server" />
