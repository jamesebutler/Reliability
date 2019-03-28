<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="RTCReport.aspx.vb"
	Inherits="RTCReport" Title="Reliability Report" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">

	<script language="javascript">
	function DisplayConfig(obj){
			var config = $get(obj);
			if (config!=null){
				config.click();
			}
	}
	</script>

	<Asp:UpdatePanel ID="_udpReport" runat="server">
		<ContentTemplate>
			<iframe width="110%"  title="RTC" height="800" frameborder="0" runat="server" id="_ifrReport">
				<h1>
					Now Loading...</h1>
			</iframe>
			<asp:Timer ID="_timer" runat="server">
			</asp:Timer>
			<ajaxToolkit:ModalPopupExtender ID="_mpeConfig" runat="server" TargetControlID="_btnConfig"
				PopupControlID="_pnlConfig" BackgroundCssClass="modalBackground" DropShadow="false"
				OkControlID="_btnClose" CancelControlID="_btnClose">
			</ajaxToolkit:ModalPopupExtender>
			<asp:Panel ID="_pnlConfig" runat="server" Width="400"  CssClass="modalPopup">
				<IP:Banner ID="_bannerHeader" runat="server" BannerMessage="Configuration" DisplayPopupBanner="true" />
				<ajaxToolkit:TabContainer ID="_tblCon" Width=100% runat="server" BorderWidth=1 BackColor=red>
					<ajaxToolkit:TabPanel ID="_tab1" Width=100% runat="server" BorderWidth=0 BackColor=white HeaderText="Refresh Rate">
						<ContentTemplate>
							<asp:Panel ID=_pnlTab1 runat=server >
							<asp:RadioButtonList ID="_rblInterval" runat="server" AutoPostBack="false" RepeatColumns="3"
								RepeatDirection="Horizontal" RepeatLayout="table">
								<asp:ListItem Text="1 minute" Value="60000"></asp:ListItem>
								<asp:ListItem Text="2 minutes" Value="120000"></asp:ListItem>
								<asp:ListItem Text="3 minutes" Value="180000"></asp:ListItem>
								<asp:ListItem Text="4 minutes" Value="240000"></asp:ListItem>
								<asp:ListItem Text="5 minutes" Value="300000"></asp:ListItem>
								<asp:ListItem Text="10 minutes" Value="600000"></asp:ListItem>
							</asp:RadioButtonList></asp:Panel></ContentTemplate>
					</ajaxToolkit:TabPanel>
					<ajaxToolkit:TabPanel ID="TabPanel1" runat="server"  HeaderText="Reports">
						<ContentTemplate>
							<asp:DropDownList ID=_ddlReports runat=server>
								<asp:ListItem Text="Reports"></asp:ListItem>
							</asp:DropDownList>
							</ContentTemplate>
					</ajaxToolkit:TabPanel>
				</ajaxToolkit:TabContainer>
				<asp:Button ID="_btnApply" runat="server" Text="Apply Changes" />
				<asp:Button ID="_btnClose" runat="server" Text="Close" />
			</asp:Panel>
			<asp:Button ID="_btnConfig" runat="server" Text="Config" />
		</ContentTemplate>
	</Asp:UpdatePanel>
</asp:Content>
