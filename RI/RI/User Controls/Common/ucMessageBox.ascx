<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucMessageBox.ascx.vb"
	Inherits="ucMessageBox" %>
<%@ Register Src="~/RI/User Controls/Common/ucBanner.ascx" TagName="Banner" TagPrefix="IP" %>
<%--<Asp:UpdatePanel ID="_udpMessageBox" runat="server" UpdateMode="always">
	<ContentTemplate>--%>
		<asp:Panel ID="_pnlMessageBox" runat="server" Width="800" Height="200"
			Style="display: none;">			
			<table border="0" class="help" cellpadding="0" cellspacing="0" style="width: 100%;
				height: 100%" align="center">
				<tr>
					<th>
				<IP:Banner ID="_bannerTitle" runat="server" DisplayPopupBanner="true" />		
					</th>
				</tr>				
				<tr>
					<td width="100%" style="height:100%" valign="top">
						<br />
						<div id="_divMessage" runat="server">
						</div>
					</td>
				</tr>
				<tr>
					<td style="height: 42px">
						<p style="text-align: right;">
							<asp:Button ID="_btnOK" runat="server" Text="OK" Width="70"></asp:Button>&nbsp;<asp:Button
								ID="_btnClose" runat="server" Text="Close" Width="70"></asp:Button>
						</p>
					</td>
				</tr>
			</table>
			</asp:Panel>
			<ajaxToolkit:ModalPopupExtender ID="_mpeMessage" runat="server" TargetControlID="_imbMessageBoxTrigger"
				PopupControlID="_pnlMessageBox" BackgroundCssClass="modalBackground" DropShadow="true"
				OkControlID="_btnClose" CancelControlID="_btnClose">
			</ajaxToolkit:ModalPopupExtender>
			<div visible="false" style="display: none; visibility: hidden">
				<asp:ImageButton ImageUrl="~/Images/question.gif" runat="server" ID="_imbMessageBoxTrigger"
					Visible="true" />
			</div>
			
		
	<%--</ContentTemplate>
</Asp:UpdatePanel>--%>
