<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="TemporarilyOfflineMessage.aspx.vb"
    Inherits="RI_Admin_TemporarilyOfflineMessage" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">
    <style>

div.Offline {
    background-color:#ffffcc;
    padding-top:10px;
    padding-bottom:10px;
    padding-left:10px;
    padding-right:10px;    
    border-style:solid;
    border-color:Black;
    border-width:1px;
}

    </style>
    <Asp:UpdatePanel ID="_udpRefresh" runat="server">
        <ContentTemplate>
            <asp:Timer Enabled="true" ID="_tmr" runat="server" Interval="100000">
            </asp:Timer>
            <div class="Offline">
                <center>
                    <h2>
                        <asp:Literal ID="_lblOfflineTitle" runat="server"></asp:Literal></h2>
                    <div id="_lblOfflineMessage" runat="server">
                    </div>
                </center>
            </div>
        </ContentTemplate>
    </Asp:UpdatePanel>
</asp:Content>
