<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucJQDate.ascx.vb"   Inherits="RI_User_Controls_Common_ucJQDate" %>


<div>
    <asp:Label ID="_lblRequiredField" Text="*" CssClass="labelerror" runat="server"></asp:Label>
    <asp:Label ID="_lblDateFrom" runat="server"  Width="170px" Text="<% $RIResources:Shared,From %>"></asp:Label>
    <asp:Literal ID="_lineBreakFrom" runat="server" Text="<br />"></asp:Literal>
    <asp:TextBox ID="_txtDateFrom" runat="server" Width="130px"></asp:TextBox>

</div>
