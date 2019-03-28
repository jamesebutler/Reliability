<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucIncidentClassification2.ascx.vb" ClientIDMode="Static"
    Inherits="ucIncidentClassification2" %>

 <%--   <script type="text/javascript">

        function ProcessChanged(ddl,ddl2) {
           // var ddl_Type = document.getElementById(ddl.id);
            //$find(ddl).set_ParentControlID('');
           // $find(ddl)._onParentChange(null, true);

            $find(ddl).set_ParentControlID('_ddlProcess');
            //$find(ddl).set_serviceMethod('GetProcessTypeList');
            //$find(ddl).set_SelectedValue('','');

            $find(ddl)._onParentChange(null, true);
            //$find(ddl2)._onParentChange(null, true);
        }

        function TypeChanged(ddl) {
            //var ddl_Type = document.getElementById(ddl.id);

            //$find(ddl).set_ParentControlID('');
            //$find(ddl)._onParentChange(null, true);

            $find(ddl).set_ParentControlID('_ddlType');
            //$find(ddl).set_serviceMethod('GetProcessList');
            $find(ddl)._onParentChange(null, true);
        }
    </script>--%>

<Asp:UpdatePanel ID="_udpIncidentType" runat="server" >
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
                <td>
                    <asp:Label ID="_lblPrevention" runat="server" Text='<%$RIResources:Shared,Prevention %>'
                        EnableViewState="false" /><br />
                    <asp:DropDownList ID="_ddlPrevention" Width="99%" runat="server" onchange="self.focus();" /></td>
            </tr>
        </table>
        <%--<asp:Table ID="Table2" runat="server" CellPadding="0" CellSpacing="0" BackColor="white"
            EnableViewState="true" style="overflow:hidden; width:100%; table-layout:fixed" Width="100%">                       
            <asp:TableRow CssClass="Border" VerticalAlign="top" Width = "100%">
                <asp:TableCell Width="20%">
                    </asp:TableCell>
                <asp:TableCell Width="20%">
                    </asp:TableCell>
                <asp:TableCell Width="20%">
                    </asp:TableCell>
                <asp:TableCell Width="20%">
                    </asp:TableCell>
                <asp:TableCell Width="20%">
                    </asp:TableCell>
            </asp:TableRow>
        </asp:Table>--%>
        <ajaxToolkit:CascadingDropDown ID="_cddlProcess" runat="server" Category="Process"
            LoadingText="..." PromptText="<%$RIResources:Shared,SelectProcess %>" ServiceMethod="GetProcessListFirst"
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
        <ajaxToolkit:CascadingDropDown ID="_ccdlPrevention" runat="server" Category="Prevention"
            LoadingText="[Loading prevention...]" PromptText="<%$RIResources:Shared,SelectPrevention %>"
            ServiceMethod="GetPrevention" ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlPrevention">
        </ajaxToolkit:CascadingDropDown>
    </ContentTemplate>
</Asp:UpdatePanel>
