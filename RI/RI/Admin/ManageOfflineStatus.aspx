<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="ManageOfflineStatus.aspx.vb"
    Inherits="RI_Admin_ManageOfflineStatus" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">
    <center>
        <h2>
            <asp:Literal ID="_lblManageOfflineTitle" runat="server"></asp:Literal></h2>
    </center>
    <asp:CheckBox ID="_cbWebsiteOffline" runat="server" /><br />
    <br />
    <asp:Label ID="_lblEnterOfflineMessage" runat="server"></asp:Label><br />
    <IP:AdvancedTextBox ID="_txtOfflineMessage" TextMode="multiLine" ExpandHeight="true"
        runat="server" Width="600px" /><br />
    <asp:Button ID="_btnUpdateStatus" runat="server" />
</asp:Content>
