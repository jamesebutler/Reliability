<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="UsingMyHelp.aspx.vb" Inherits="RI_UsingHelp" title="RI:Using Help" %>
<%@ OutputCache Duration="60"  VaryByParam="none" %>
<%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="_cphMain" ContentPlaceHolderID="_cphMain" runat="Server">   
    <table border="1" class="help" cellpadding="2" cellspacing="0" style="width: 90%;
        height: 80%" align="center">
        <tr>
            <th><asp:Localize ID="Localize1" runat="server" Text="<%$RIResources:Shared,DefinitionProblemReportingTitle %>"></asp:Localize></th>
            <th rowspan="3" width="10px">            </th>
            <th><asp:Localize ID="Localize2" runat="server" Text="<%$RIResources:Shared,DefinitionUsingMyHelpTitle %>"></asp:Localize></th>
        </tr>
        <tr>
            <td>
            </td>
            <td align="right">
                <asp:HyperLink ID="_imgMyHelp" runat="server" NavigateUrl="http://MyHelp" Target="_blank"
                    ImageUrl="~/Images/MyHelpSm.gif" /><br />
            </td>
        </tr>
        <tr>
            <td width="50%" valign="top">
                <asp:Localize ID="Localize3" runat="server" Text="<%$RIResources:Shared,DefinitionProblemReporting %>"></asp:Localize>
                &nbsp;<asp:HyperLink ID="HyperLink1" NavigateUrl="Mailto:Shanda.Bittick@ipaper.com"
                    runat="server">Shanda.Bittick@ipaper.com</asp:HyperLink>
            </td>
            <td width="50%" valign="top">
                <asp:Localize ID="Localize4" runat="server" Text="<%$RIResources:Shared,DefinitionUsingMyHelp %>"></asp:Localize>
            </td>
        </tr>
    </table>
</asp:Content>


