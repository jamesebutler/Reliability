<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucSiteLocation.ascx.vb"
    Inherits="ucSiteLocation"  %>

<asp:Table ID="_tblSite" runat="server" BorderWidth="0" CellPadding="0" CellSpacing="2"
    BackColor="white" Style="width: 98%">
    <asp:TableRow>
        <asp:TableCell ColumnSpan="4">
            <asp:Label ID="_lblDivision" Width="60" runat="server" Text="<%$RIResources: Shared,Division %>"></asp:Label>
            <asp:DropDownList ID="_ddlDivision" runat="server" Width="250">
            </asp:DropDownList>           
            <asp:Label ID="_lblFacility" Width="60" runat="server" Text="<%$RIResources: Shared,Facility %>"></asp:Label>
            <asp:DropDownList ID="_ddlFacility" runat="server" Width="270">
            </asp:DropDownList>
            <asp:Label ID="_lblBusinessUnit" Width="100" runat="server" Text="<%$RIResources: Shared,BusinessUnit %>"></asp:Label>
            <asp:DropDownList ID="_ddlBusinessUnit" runat="server" Width="250">
            </asp:DropDownList>
            <asp:Label ID="_lblArea" Width="60" runat="server" Text="<%$RIResources: Shared,Area %>"></asp:Label>
            <asp:DropDownList ID="_ddlArea" runat="server" Width="160">
            </asp:DropDownList>
            <asp:Label ID="_lblLine" Width="60" runat="server" Text="<%$RIResources: Shared,Line %>"></asp:Label>
            <asp:DropDownList ID="_ddlLine" runat="server" Width="270">
            </asp:DropDownList>
            <asp:Label ID="_lblLineBreak" runat="server" Width="100" Text="<%$RIResources: Shared,LineBreak %>"></asp:Label>
            <asp:DropDownList ID="_ddlLineBreak" runat="server" Width="250">
            </asp:DropDownList>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
<ajaxToolkit:CascadingDropDown ID="_cddlFacility" runat="server" Category="SiteID"
    LoadingText="[Loading Facilities...]" PromptText="    " ServiceMethod="GetFacilityList"
    ServicePath="~/CascadingLists.asmx" TargetControlID="_ddlFacility" ContextKey="All"
    UseContextKey="true" ParentControlID="_ddlDivision">
</ajaxToolkit:CascadingDropDown>
<ajaxToolkit:CascadingDropDown ID="_cddlDivision" runat="server" Category="Division"
    LoadingText="[Loading Divisions...]" PromptText="    " ServiceMethod="GetDivision"
    ServicePath="~/CascadingLists.asmx" BehaviorID="bhDivision" TargetControlID="_ddlDivision"
   ContextKey ="All" UseContextKey="true">
</ajaxToolkit:CascadingDropDown>

<ajaxToolkit:CascadingDropDown ID="_cddlBusinessUnit" runat="server" Category="BusinessUnit"
    LoadingText="[Loading Business Units...]" PromptText="    " ServiceMethod="GetBusinessUnit"
    ServicePath="~/CascadingLists.asmx" TargetControlID="_ddlBusinessUnit" ParentControlID="_ddlFacility">
</ajaxToolkit:CascadingDropDown>
<ajaxToolkit:CascadingDropDown ID="_cddlArea" runat="server" Category="Area" LoadingText="[Loading Area...]"
    PromptText="    " ServiceMethod="GetArea" ServicePath="~/CascadingLists.asmx"
    TargetControlID="_ddlArea" ParentControlID="_ddlBusinessUnit">
</ajaxToolkit:CascadingDropDown>
<ajaxToolkit:CascadingDropDown ID="_cddlLine" runat="server" Category="Line" LoadingText="[Loading Line...]"
    PromptText="    " ServiceMethod="GetLine" ServicePath="~/CascadingLists.asmx"
    TargetControlID="_ddlLine" ParentControlID="_ddlArea">
</ajaxToolkit:CascadingDropDown>
<ajaxToolkit:CascadingDropDown ID="_cddlLineBreak" runat="server" Category="LineBreak"
    LoadingText="[Loading Line...]" PromptText="    " ServiceMethod="GetLineBreaks"
    ServicePath="~/CascadingLists.asmx" TargetControlID="_ddlLineBreak" ParentControlID="_ddlLine">
</ajaxToolkit:CascadingDropDown>
