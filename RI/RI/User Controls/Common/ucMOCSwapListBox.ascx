<%@ Control Language="VB" CodeFile="ucMOCSwapListBox.ascx.vb" Inherits="ucMOCSwapList"  %>
<%@ Register Src="~/RI/User Controls/Common/UcMTTResponsible.ascx" TagName="Responsible" TagPrefix="IP" %>
<table cellpadding="2" cellspacing="2" border="0" width="100%">
    <tr style="font-size:8pt">
        <td style="width: 30%;font-size:8pt">
            <asp:Label ID="_lblAllFields" Width="100%" runat="server" Font-Size="8pt" Text="<%$RIResources:Shared,Available Approvers/Informed %>"></asp:Label></td>
        <td style="width: 20%">
        </td>
        <td style="width: 30%">
            <asp:Label ID="_lblSelectedFields" Width="100%" runat="server" Font-Size="8pt" Text="<% $RIResources:Shared,Selected Approvers/Informed%>"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 30%;font-size:8pt" valign="top">
            <asp:ListBox ID="_lbAllFields" SelectionMode="Multiple" runat="server" Font-Size="8pt"
                Rows="33" Width="95%"></asp:ListBox></td>
        <td valign="middle" align="center" width="20%">
            <asp:Button ID="_btnMoveSelected" runat="server" Text="<%$RIResources:BUTTONTEXT,MoveSelected %>" Width="125" Font-Size="8pt" /><br />
            <asp:Button ID="_btnMoveAll" runat="server" Text="<%$RIResources:BUTTONTEXT,MoveAll %>" Width="125" Visible="false" Font-Size="8pt" /><br />
            <br />
            <asp:Button ID="_btnRemoveSelected" runat="server" Text="<%$RIResources:BUTTONTEXT,RemoveSelected %>" Width="125" Font-Size="8pt" /><br />
            <asp:Button ID="_btnRemoveAll" runat="server" Text="<%$RIResources:BUTTONTEXT,RemoveAll %>" Width="125" Font-Size="8pt" /><br />
        </td>
        
        
        <td style="width: 30%" valign="top">
            
            <asp:RadioButton ID="_rbL1Approvers" Checked="true" runat="server" GroupName="SendList" Text="<%$RIResources:Shared,First Level Approvers %>" Font-Size="8pt" /><br />
            <asp:ListBox Width="95%" ID="_lbApproversL1" CssClass="Border" SelectionMode="Multiple" runat="server" Font-Size="8pt" Rows=7></asp:ListBox><br />
           
             <asp:RadioButton ID="_rbL2Approvers" runat="server" GroupName="SendList" Text="<%$RIResources:Shared,Second Level Approvers %>" Font-Size="8pt" /><br />
            <asp:ListBox Width="95%" ID="_lbApproversL2" SelectionMode="Multiple" runat="server" Font-Size="8pt" Rows=7></asp:ListBox><br />
            
            <asp:RadioButton ID="_rbL3Approvers" runat="server" GroupName="SendList" Text="<%$RIResources:Shared,Third Level Approvers %>" Font-Size="8pt"/><br />
            <asp:ListBox Width="95%" ID="_lbApproversL3" SelectionMode="Multiple" runat="server" Font-Size="8pt" Rows=7></asp:ListBox><br />
            
            <asp:RadioButton ID="_rbInformed" runat="server" GroupName="SendList" Text="Informed:" Font-Size="8pt" /><br />
            <asp:ListBox Width="95%" ID="_lbInformed" SelectionMode="Multiple" runat="server" Font-Size="8pt" Rows=7></asp:ListBox>

        </td>
    </tr>
    
</table>

<asp:HiddenField ID="_hdfAllFields" runat="server" />
<asp:HiddenField ID="_hdSelectedL1Fields" runat="server" />
<asp:HiddenField ID="_hdSelectedL2Fields" runat="server" />
<asp:HiddenField ID="_hdSelectedL3Fields" runat="server" />
<asp:HiddenField ID="_hdSelectedInformed" runat="server" />
