<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="MOCApprovals.aspx.vb"
    Inherits="RI_MOCAApprovals" Title="MOC Approvals" EnableViewState="True"
    EnableViewStateMac="false" ViewStateEncryptionMode="never" Trace="false" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">
    <Asp:UpdatePanel ID="_udpActionItem" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <table cellpadding="2" cellspacing="2" border="0" width="100%">
    <tr style="font-size:12pt">
        <td style="width: 40%;font-size:8pt">
            <asp:Label ID="_lblFacility" Width="220px" runat="server" Text="Facility"></asp:Label>
            <asp:DropDownList runat="server" ID="_ddlFacility"></asp:DropDownList></td>
    </tr>
    <tr style="font-size:12pt">
        <td style="width: 40%;font-size:8pt">
            <asp:Label ID="Label1" Width="220px" runat="server" Text="Available Approvers/Informed"></asp:Label></td>
        <td style="width: 20%">
        </td>
        <td style="width: 40%">
            <asp:Label ID="_lblSelectedFields" Width="220px" runat="server" Text="Selected Approvers/Informed<br />* Required"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 40%" valign="bottom">
            <asp:ListBox Width="100%" ID="_lbAllFields" SelectionMode="Multiple" runat="server"
                Rows="23" ></asp:ListBox></td>
        <td valign="middle" align="center" width="20%">
            <asp:Button ID="_btnMoveSelected" runat="server" Text="<%$RIResources:BUTTONTEXT,MoveSelected %>"
                Width="125" /><br />
            <asp:Button ID="_btnMoveAll" runat="server" Text="<%$RIResources:BUTTONTEXT,MoveAll %>"
                Width="125" /><br />
            <br />
            <asp:Button ID="_btnRemoveSelected" runat="server" Text="<%$RIResources:BUTTONTEXT,RemoveSelected %>"
                Width="125" /><br />
            <asp:Button ID="_btnRemoveAll" runat="server" Text="<%$RIResources:BUTTONTEXT,RemoveAll %>"
                Width="125" /><br />
        </td>
        <td style="width: 40%" valign="bottom">
            <asp:RadioButton ID="_rbL1Approvers" Checked="true" runat="server" GroupName="SendList"
                Text="First Level Approvers:" /><br />
            <asp:ListBox Width="100%" ID="_lbApproversL1" CssClass="Border" SelectionMode="Multiple" runat="server"></asp:ListBox><br />
            <br />
            <asp:RadioButton ID="_rbL2Approvers" runat="server" GroupName="SendList" Text="Second Level Approvers:" /><br />
            <asp:ListBox Width="100%" ID="_lbApproversL2" SelectionMode="Multiple" runat="server"></asp:ListBox><br />
            <br />
            <asp:RadioButton ID="_rbL3Approvers" runat="server" GroupName="SendList" Text="Third Level Approvers:" /><br />
            <asp:ListBox Width="100%" ID="_lbApproversL3" SelectionMode="Multiple" runat="server"></asp:ListBox><br />
            <br />
            <asp:RadioButton ID="_rbInformed" runat="server" GroupName="SendList" Text="Informed:" /><br />
            <asp:ListBox Width="100%" ID="_lbInformed" SelectionMode="Multiple" runat="server"></asp:ListBox>
        </td>
    </tr>
</table>
        </ContentTemplate>
    </Asp:UpdatePanel>
    <center>
        <asp:Button ID="_btnSaveClose" runat="server" Text="Save Approvals"
             OnClientClick="BeginPostBack();" />
    </center>
</asp:Content>
