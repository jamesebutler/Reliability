<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucBanner.ascx.vb" Inherits="RI_User_Controls_Common_ucBanner" %>
<asp:Panel ID="_pnlBanner" runat="server" ScrollBars="none" Width="100%" BackColor=Black>
<asp:Image ID="_imgLogo" SkinID="Logo" runat="server" ImageAlign="Middle" EnableViewState="false" /><asp:Image EnableViewState="false" ID="Image1"
						runat="server" ImageUrl="~/images/blank.gif" Width="90" BorderWidth="0" Height="5" /><asp:Image ImageAlign="Middle" EnableViewState="false" runat="server" ID="_imgHeader"
					ImageUrl="~/images/blank.gif" /><asp:Image EnableViewState="false" ID="_imgRightSpacer" runat="server" ImageUrl="~/images/blank.gif"
					Width="192" BorderWidth="0" Height="5" /><asp:Image EnableViewState="false" ID="_imgBlank1"
						runat="server" ImageUrl="~/images/blank.gif" Width="1" BorderWidth="0" Height="5" />	
</asp:Panel>
