<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="CacheViewer_old.aspx.vb"
    Inherits="RI_Admin_Cache" Title="International Paper: Cache Viewer" %>
<%@ MasterType VirtualPath="~/RI.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">
    <p>
        <b>The contents of the ASP.NET application cache are:</b></p>
    <Asp:UpdatePanel ID="_udpCache" runat="server" UpdateMode="always">
        <ContentTemplate>
            <asp:Label ID="RemoveMessage" runat="server" />
            <table cellpadding="2" cellspacing="1" border="0">
                <tr>
                    <td>
                        <asp:GridView ID="_grdCache2" CssClass="help" HorizontalAlign="Center" CellSpacing="2"
                            AllowSorting="true" HeaderStyle-HorizontalAlign="center" runat="server" AutoGenerateColumns="false">
                            <Columns>
                                <asp:CommandField ButtonType="link" ShowDeleteButton="true" />
                                <asp:BoundField HeaderText="Name (Key)" DataField="Key" />
                                <asp:BoundField HeaderText="Data Type" DataField="Value" />
                            </Columns>
                            <EmptyDataTemplate>
                                <p>
                                    There are no items stored in Cache</p>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="_btnRefresh" Text="Refresh" runat="server" />&nbsp;&nbsp;<asp:Button
                            ID="_btnDeleteAll" Text="Delete All Cache Items" runat="server" /></td>
                </tr>
            </table>
            <br />
            <ajaxToolkit:ConfirmButtonExtender ID="_cbeDeleteAll" runat="server" TargetControlID="_btnDeleteAll"
                ConfirmText="Are you sure you want to Delete All of the items from the Cache?" />
        </ContentTemplate>
    </Asp:UpdatePanel>
</asp:Content>
