<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucEventClassification.ascx.vb" ClientIDMode="Static"
    Inherits="ucEventClassification" %>

<Asp:UpdatePanel ID="_udpEventType" runat="server" >
    <ContentTemplate>
        <table cellpadding="0" cellspacing="0" width="100%" style="table-layout: fixed; overflow: hidden">
            <col width="20%" />
            <col width="20%" />
            <col width="20%" />
            <col width="20%" />
            <col width="20%" />
            <tr class="Border">
                <td>
                    <asp:Label ID="_lblEquipmentProcess" runat="server" Text='<%$RIResources:Shared,EquipmentProcess %>'
                        EnableViewState="false" /><br />
                    <asp:DropDownList ID="_ddlProcess" Width="99%" runat="server" onchange="self.focus()"  /></td>
                <td>
                    <asp:Label ID="_lblCause" runat="server" Text='<%$RIResources:Shared,Type %>' EnableViewState="false" /><br />
                    <asp:DropDownList ID="_ddlType" Width="99%" runat="server" onchange="self.focus()" /></td>
                <td>
                    <asp:Label ID="_lblComponent" runat="server" Text='<%$RIResources:Shared,Component %>'
                        EnableViewState="false" /><br />
                    <asp:DropDownList ID="_ddlComponent" Width="99%" runat="server"  /></td>
                <td>
                    <asp:Label ID="_lblReason" runat="server" Text='<%$RIResources:Shared,Cause %>' EnableViewState="false" /><br />
                    <asp:DropDownList ID="_ddlCause" Width="99%" runat="server" onchange="self.focus();" /></td>
            </tr>
        </table>
        <ajaxToolkit:CascadingDropDown ID="_cddlProcess" runat="server" Category="Process"
            LoadingText="..." PromptText="<%$RIResources:Shared,SelectProcess %>" ServiceMethod="GetProcessTypeFirstList"
            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlProcess" >
        </ajaxToolkit:CascadingDropDown>
        <ajaxToolkit:CascadingDropDown ID="_cddlTypes" runat="server" Category="Causes" LoadingText="..."
            PromptText="<%$RIResources:Shared,SelectType %>" ServiceMethod="GetProcessTypeList"
            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlType" ParentControlID="_ddlProcess" >
        </ajaxToolkit:CascadingDropDown>
        <ajaxToolkit:CascadingDropDown ID="_cddlComponent" runat="server" Category="Component"
            LoadingText="..." PromptText="<%$RIResources:Shared,SelectComponent %>" ServiceMethod="GetComponentList"
            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlComponent" ParentControlID="_ddlType">
        </ajaxToolkit:CascadingDropDown>
        <ajaxToolkit:CascadingDropDown ID="_ccdlReasons" runat="server" Category="Reason"
            LoadingText="..." PromptText="<%$RIResources:Shared,SelectCause %>" ServiceMethod="GetReasonList"
            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlCause" ParentControlID="_ddlType">
        </ajaxToolkit:CascadingDropDown>
    </ContentTemplate>
</Asp:UpdatePanel>
