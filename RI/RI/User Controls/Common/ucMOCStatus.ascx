<%@ Control Language="VB" AutoEventWireup="true" CodeFile="ucMOCStatus.ascx.vb"
    Inherits="ucMOCStatus" EnableViewState="true" %>

<%-- ALA 4/4/2017 
        The default status is Draft.  Only On Hold should be enabled when Draft selected.
        On Hold - Only Draft and Remove from Hold should be enabled.  
        Implemented - Enabled for MOCs that are No Approvers, Approved or Approval Requested.  
            Should not be enabled for MOCs that are Not Approved.
            If Implemented selected, On Hold should be enabled.
--%>

<table cellpadding="2" cellspacing="0" border="0" width="100%">
    <tr class="Header">
        <th colspan="1" align="left">
            <asp:Label ID="_lblMOCStatus" runat="server" Text="<%$RIResources:MOC,Status %>"
                SkinID="LabelWhite" EnableViewState="false" />
            <asp:HyperLink ID="_hypStatusDefinition" CssClass="LabelLink" runat="server"
                ImageUrl="../../../Images/question.gif"></asp:HyperLink>

        </th>
    </tr>
    <tr class="Border">
        <td>
            <asp:DropDownList ID="_ddlStatus" runat="server"  >
                <asp:ListItem Text="<%$ RIResources:Shared,Draft%>" Value="Draft" disabled="disabled"></asp:ListItem>
                <asp:ListItem Text="<%$ RIResources:Shared,On Hold%>" Value="On Hold" disabled="disabled" ></asp:ListItem>
                <asp:ListItem Text="<%$ RIResources:Shared,Remove From Hold%>" Value="Remove From Hold" disabled="disabled"></asp:ListItem>
                <asp:ListItem Text="<%$ RIResources:Shared,Not Approved%>" Value="Not Approved" disabled="disabled"></asp:ListItem>
                <asp:ListItem Text="<%$ RIResources:Shared,No Approvers%>" Value="No Approvers" disabled="disabled"></asp:ListItem>
                <asp:ListItem Text="<%$ RIResources:Shared,Approval Requested%>" Value="Approval Requested" disabled="disabled"></asp:ListItem>
                <asp:ListItem Text="<%$ RIResources:Shared,Approved%>" Value="Approved" disabled="disabled"></asp:ListItem>
                <asp:ListItem Text="<%$ RIResources:Shared,Implemented%>" Value="Implemented" disabled="disabled"></asp:ListItem>
                <asp:ListItem Text="<%$ RIResources:Shared,Completed%>" Value="Completed" disabled="disabled"></asp:ListItem>
            </asp:DropDownList>

            <asp:CheckBoxList ID="_cblStatus" runat="server" Visible="false" RepeatDirection="vertical" RepeatColumns="10" Width="90%">
<%--                <asp:ListItem Text="<%$ RIResources:Shared,Draft%>" Value="Draft"></asp:ListItem>
                <asp:ListItem Text="<%$ RIResources:Shared,On Hold%>" Value="On Hold"></asp:ListItem>
                <asp:ListItem Text="<%$ RIResources:Shared,Remove From Hold%>" Value="Remove From Hold"></asp:ListItem>
                <asp:ListItem Text="<%$ RIResources:Shared,Not Approved%>" Value="Not Approved" ></asp:ListItem>
                <asp:ListItem Text="<%$ RIResources:Shared,No Approvers%>" Value="No Approvers" ></asp:ListItem>
                <asp:ListItem Text="<%$ RIResources:Shared,Approval Requested%>" Value="Approval Requested"></asp:ListItem>
                <asp:ListItem Text="<%$ RIResources:Shared,Approved%>" Value="Approved" ></asp:ListItem>
                <asp:ListItem Text="<%$ RIResources:Shared,Implemented%>" Value="Implemented"></asp:ListItem>
                <asp:ListItem Text="<%$ RIResources:Shared,Completed%>" Value="Completed" ></asp:ListItem>--%>

            </asp:CheckBoxList>
        </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="_lbInfo" runat="server" BackColor="red"></asp:Label></td>
        </tr>
    
</table>
<div id="_divStatusDefinition" class="modalPopup" style="display: none">
    <span style="text-align: left" class="ContentHeader">
        <asp:Literal ID="Literal1" runat="Server" Text="Status"></asp:Literal></span>
    <asp:Localize ID="_StatusDef" runat="server" Text="<%$ RIResources:Shared,StatusDefinition %>"></asp:Localize>
</div>

