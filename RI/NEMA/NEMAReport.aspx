<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="NEMAReport.aspx.vb"
    Inherits="NEMA_NEMAReport" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">
    <table width='100%' border="0">
        <tr>
            <td valign="top" style="width: 50%">
                <asp:Literal ID="_litNEMAReport" runat="server"></asp:Literal>
            </td>
            <td valign="top" style="width: 50%">
               <iframe id="_ifrNEMA" frameborder="0" src="NEMA2.htm" runat="server" width="100%"
                                            height="400px"></iframe>
            </td>
        </tr>
    </table>
</asp:Content>
