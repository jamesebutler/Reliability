<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucMTTResponsible.ascx.vb"
    Inherits="ucMTTResponsible" ClassName="ucMTTResponsible" %>
<asp:Table ID="_tblResponsible" runat="server" BorderWidth="0" CellPadding="0" CellSpacing="2"
    BackColor="white" Style="width: 98%;">
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label ID="_lblFacility" Width="60" runat="server" Font-Size="8" Text="<%$RIResources: Shared,Facility %>"></asp:Label>
            <asp:DropDownList ID="_ddlFacility" runat="server" Width="270" >
            </asp:DropDownList><br />
            <asp:Label ID="_lblResponsible" Width="100" runat="server" Font-Size="8" Text="<%$RIResources: Shared,Responsible Person%>"></asp:Label>
            <asp:DropDownList ID="_ddlResponsible" runat="server" Width="250" Font-Size="8">
            </asp:DropDownList>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>

<ajaxToolkit:CascadingDropDown ID="_cddlMOCFacility" runat="server" Category="SiteID"
    LoadingText="[Loading Facilities...]" PromptText="    " ServiceMethod="GetFacilityList"
    ServicePath="~/CascadingLists.asmx" TargetControlID="_ddlFacility" ContextKey="All"
    UseContextKey="true">
</ajaxToolkit:CascadingDropDown>
<ajaxToolkit:CascadingDropDown ID="_cddlMOCResponsible" runat="server" Category="Responsible"
    LoadingText="[Loading Responsible...]" PromptText="    " ServiceMethod="GetMTTPerson"
    ServicePath="~/CascadingLists.asmx" TargetControlID="_ddlResponsible" ParentControlID="_ddlFacility">
</ajaxToolkit:CascadingDropDown>
