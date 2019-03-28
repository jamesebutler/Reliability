<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="ImpersonateUser.aspx.vb" Inherits="RI_Help_ImpersonateUser" title="RI:Impersonate User" %>
<%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" Runat="Server">
    <div style="text-align: center">
        <table border="0" cellpadding="2" cellspacing="0" style="width: 400px">
            <tr>
                <td>
                    <asp:Label ID="_lblUser" runat="server" Text="User:"></asp:Label>
                    <asp:DropDownList ID="_ddlUser" runat="server">
                    </asp:DropDownList>&nbsp;<asp:Button ID="_btnImpersonateUser" runat="server" Text="Impersonate User" /></td>
            </tr>
            <tr>
                <td style="text-align: center;">
                    </td>
            </tr>
            <tr>
                <td>
                    <div ID="_divUserProfile" runat="server"></div>               
                </td>
            </tr>
        </table>
    </div>

</asp:Content>

