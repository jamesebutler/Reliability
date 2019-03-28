<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucOutageLocation.ascx.vb"
    Inherits="ucOutageLocation" %>
<asp:Table ID="_tblSite" runat="server" BorderWidth="0" CellPadding="0" CellSpacing="2"
    BackColor="white" Style="width: 98%" EnableViewState="true">    
    <asp:TableRow CssClass="Border">
        <asp:TableCell Width="25%" > 
            <asp:Label ID="_lblDivision"  runat="server" Text="<%$ RIResources:Shared,Division %>"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="_ddlDivision" runat="server" Width="160">
            </asp:DropDownList></asp:TableCell>
        <asp:TableCell Width="35%" >
            <asp:Label ID="_lblFacility" runat="server" Text="<%$ RIResources:Shared,Facility %>"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="_ddlFacility" runat="server" Width="270">
            </asp:DropDownList></asp:TableCell>
        <asp:TableCell Width="40%" >
            <asp:Label ID="_lblBusinessUnit"  runat="server" Text="<%$ RIResources:Shared,BusinessUnit %>"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="_ddlBusinessUnit"  runat="server" Width="250">
            </asp:DropDownList></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow CssClass="Border">
        <asp:TableCell Width="25%" CssClass="Border">
            <asp:Label ID="_lblArea" runat="server" Text="<%$ RIResources:Shared,Area %>"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="_ddlArea"  runat="server" Width="160">
            </asp:DropDownList>
        </asp:TableCell>
        <asp:TableCell Width="35%" >
            <asp:Label ID="_lblLine" runat="server" Text="<%$ RIResources:Shared,lLine %>"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList  ID="_ddlLine"  runat="server" Width="270">
            </asp:DropDownList>
        </asp:TableCell>
        <asp:TableCell Width="40%" >
           <%-- <asp:Label ID="_lblLineBreak" runat="server" Text="<%$ Resources:Shared,lblLineBreak %>"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="_ddlLineBreak"  runat="server" Width="250">
            </asp:DropDownList>--%>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
