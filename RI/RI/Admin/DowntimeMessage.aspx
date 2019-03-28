<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="DowntimeMessage.aspx.vb"
	Inherits="RI_Admin_DowntimeMessage" Title="Untitled Page" EnableEventValidation="false"
	ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">

	<script type="text/javascript">
		function showPreview(){
			var msg = $get("<%= _messageBox.clientid%>");
			var startend = "<%=_startEnd.clientid %>";
			var startHrs = $get(startend+"__ddlStartHrs");
			var startMins = $get(startend+"__ddlStartMins");
			var endHrs = $get(startend+"__ddlEndHrs");
			var endMins = $get(startend+"__ddlEndMins");
			var startDate = $get(startend+"__txtStartDate");
			var endDate = $get(startend+"__txtEndDate");
			var startDt = startDate.value.split('/');
			var endDt = endDate.value.split('/');
			try {
			//if (MessageCanBeDisplayed(new Date(startDt[2],startDt[0]-1,startDt[1],startHrs.value,startMins.value),new Date(endDt[2],endDt[0]-1,endDt[1],endHrs.value,endMins.value))){
				dropinboxv2.innerHTML = buildMsg(msg.value,false);
				displaymsg('RI Website',msg.value,new Date(startDt[2],startDt[0]-1,startDt[1],startHrs.value,startMins.value),new Date(endDt[2],endDt[0]-1,endDt[1],endHrs.value,endMins.value),false);
				initboxv2(true);
			//}
			//else{
			//	alert('Please enter Start and End dates that are in the future');
			//}
			return false;
			}
			
			catch(e){
			
			}
		}
	</script>

	<Asp:UpdatePanel ID="_udpDownTimeMessage" runat="server" UpdateMode="Always">
		<ContentTemplate>
			<center>
				<asp:Label ID="Label1" BackColor="lightYellow" BorderWidth="2" Width="75%" Font-Size="Medium"
					Text="Instructions:" runat="server"></asp:Label></center>
			<br />
			<br />
			<table width="98%" border="0" align="center" cellpadding="2" cellspacing="2" class="Border">
				<tr>
					<td align="LEFT" nowrap="nowrap">
						<b>Set Post Date:</b></td>
					<td align="center">
						<IP:StartEndCalendar ID="_startEnd" runat="server" ShowTime="true" ShowCalendar="true" />
					</td>
				</tr>
				<tr>
					<td align="LEFT" valign="top">
						<b>Message:</b></td>
					<td align="center" valign="top">
						<IP:AdvancedTextBox ID="_messageBox" runat="server" Width="90%" TextMode="MultiLine"
							ExpandHeight="true" MaxLength="300"></IP:AdvancedTextBox></td>
				</tr>
				<tr>
					<td align="LEFT">
						<b>Enable Message:</b><asp:CheckBox ID="_cbShowMessage" runat="server" Checked="true" /></td>
					<td align="center">
						<asp:Button ID="_btnPreview" runat="server" Text="Preview" OnClientClick="showPreview();return false;" />&nbsp;&nbsp;
						<asp:Button ID="_btnSubmit" runat="server" Text="Submit" />&nbsp;&nbsp;
						<asp:Button ID="_btnCancel" runat="server" Text="Cancel" />
					</td>
				</tr>
			</table>
			
		</ContentTemplate>
	</Asp:UpdatePanel>
</asp:Content>
