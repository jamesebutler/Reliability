<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucIncidentClassification.ascx.vb"
    Inherits="ucIncidentClassification" %>
<Asp:UpdatePanel ID="_udpIncidentType" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Table ID="Table2" runat="server" CellPadding="0" CellSpacing="2" BackColor="white"
            EnableViewState="true">
            <asp:TableHeaderRow CssClass="Header">
                <asp:TableHeaderCell ColumnSpan="5" HorizontalAlign="left">
                    <asp:Label ID="_lblIncidentClassificationHeader" runat="server" Text="<%$RIResources:Shared,IncidentClassification%>"
                        SkinID="LabelWhite" EnableViewState="false"></asp:Label>
                </asp:TableHeaderCell>
            </asp:TableHeaderRow> 
            <asp:TableRow CssClass="Border">
                <asp:TableCell ColumnSpan="5">
                    <asp:Label ID="_lblTrigger" runat="server" Text='<%$RIResources:Shared,Trigger%>'
                        EnableViewState="false" />&nbsp;
                    <asp:DropDownList ID="_ddlTrigger"  runat="server" Width="80%">
                    </asp:DropDownList></asp:TableCell>
            </asp:TableRow>
            <%--<asp:TableRow CssClass="Border">
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
            </asp:TableRow>--%>
            <asp:TableRow CssClass="Border" VerticalAlign="top">
                <asp:TableCell>
                    <asp:Label ID="_lblCause" runat="server" Text='<%$RIResources:Shared,Type %>' EnableViewState="false" /><br />
                    <asp:DropDownList ID="_ddlType" runat="server" Width="190" /></asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="_lblEquipmentProcess" runat="server" Text='<%$RIResources:Shared,EquipmentProcess %>'
                        EnableViewState="false" /><br />
                    <asp:DropDownList ID="_ddlProcess" runat="server" Width="170" /></asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="_lblComponent" runat="server" Text='<%$RIResources:Shared,Component %>'
                        EnableViewState="false" /><br />
                    <asp:DropDownList ID="_ddlComponent" runat="server" Width="170" /></asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="_lblReason" runat="server" Text='<%$RIResources:Shared,Cause %>'
                        EnableViewState="false" /><br />
                    <asp:DropDownList ID="_ddlCause" runat="server" Width="250" /></asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="_lblPrevention" runat="server" Text='<%$RIResources:Shared,Prevention %>'
                        EnableViewState="false" /><br />
                    <asp:DropDownList ID="_ddlPrevention" runat="server" Width="170" /></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </ContentTemplate>
</Asp:UpdatePanel>
