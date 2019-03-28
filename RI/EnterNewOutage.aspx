<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="EnterNewOutage.aspx.vb"
	Inherits="RI_EnterNewOutage" Title="RI:Enter New Outage" Trace="false" EnableViewState="true" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="_contentMain" ContentPlaceHolderID="_cphMain" runat="Server">
	<Asp:UpdatePanel ID="_udpLocation" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<asp:Button ID="_btnViewSearch" runat="server" Text="Switch RI" Visible="false" />
			<asp:Panel ID="_pnlLocation" runat="server">
				<asp:Table ID="_tblSite" runat="server" BorderWidth="0" CellPadding="2" CellSpacing="0"
					BackColor="white" EnableViewState="true">
					<asp:TableRow CssClass="Border">
						<asp:TableCell Width="33%"> 
							<asp:Label ID="_lblFacility" runat="server" Text='<%$ RIResources:Shared,Facility %>'></asp:Label><br />
							<asp:DropDownList ID="_ddlFacility" runat="server" AutoPostBack="true" Width="90%">
							</asp:DropDownList>
						</asp:TableCell>
						<asp:TableCell Width="33%">
							<asp:Label ID="Label2" runat="server" Text='Bus/Area'></asp:Label><br />
							<asp:DropDownList ID="_ddlBusinessUnit" AutoPostBack="true" EnableViewState="true"
								Width="90%" Visible="true" runat="server" />
						</asp:TableCell>
						<asp:TableCell Width="33%">
							<asp:Label ID="Label5" runat="server" Text='<%$ RIResources:Shared,LineLineBreak %>'></asp:Label><br />
							<asp:DropDownList ID="_ddlLineBreak" AutoPostBack="true" Width="98%" runat="server" />
						</asp:TableCell>
					</asp:TableRow>
				</asp:Table>
			</asp:Panel>
			<asp:Panel ID="_pnlIncident" runat="server">
				<asp:Table ID="Table1" runat="server" BorderWidth="0" CellPadding="2" CellSpacing="2"
					BackColor="white" Style="width: 99%; overflow: hidden;" EnableViewState="false">
					<asp:TableRow CssClass="Border">
						<asp:TableCell Width="33%" Style="width: 33%">
							<IP:DateTime ID="_startDateTime" DateLabel='<%$ RIResources:Shared,IncidentStart %>'
								runat="server" />
						</asp:TableCell>
						<asp:TableCell Width="33%" Style="width: 33%">
							<IP:DateTime ID="_endDateTime" DateLabel='<%$ RIResources:Shared,IncidentEnd %>'
								runat="server" />
						</asp:TableCell>
						<asp:TableCell Width="33%" Style="width: 33%">
							<asp:Label ID="_lblDowntime" runat="server" Text='<%$ RIResources:Shared,Downtime %>'
								EnableViewState="false" />&nbsp;<asp:TextBox ID="_txtDownTime" Width="80" runat="server"></asp:TextBox>
							<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
								TargetControlID="_txtDownTime" FilterType="custom" ValidChars=",.1234567890">
							</ajaxToolkit:FilteredTextBoxExtender>
							<asp:Button ID="_btnCalculateDowntime" runat="server" Text="Calculate" />
						</asp:TableCell>
					</asp:TableRow>
					<asp:TableRow CssClass="Border">
						<asp:TableCell ColumnSpan="3">
							<asp:Label ID="_lblIncidentTitle" runat="server" Text="Outage Title" EnableViewState="false" />&nbsp;<asp:TextBox
								ID="_txtIncidentTitle" runat="server" Width="500"></asp:TextBox></asp:TableCell>
					</asp:TableRow>
					<asp:TableRow CssClass="Border">
						<asp:TableCell ColumnSpan="3" HorizontalAlign="left">
							<asp:Label ID="_lblDescription" runat="server" Text='<%$ RIResources:Shared,Description %>'
								EnableViewState="false" />
							<div>
								<IP:AdvancedTextBox ID="_txtIncidentDescription" runat="server" ExpandHeight="true"
									Width="98%" Style="width: 98%" Rows="2" TextMode="MultiLine" MaxLength="4000" /></div>
							<%--<IP:UltimateSpell ID=_spellIncidentDescription runat=server ShowAddButton=true ShowSpellButton=false IgnoreDisabledControls=true ShowModal=false  ShowOptions=true EnableTheming=false  SpellAsYouType=true ControlIdsToCheck="_txtIncidentDescription,_txtConditionsInfluencingFailure" SpellButton-SkinID="Button" ></IP:UltimateSpell>--%>
						</asp:TableCell>
					</asp:TableRow>
				</asp:Table>
				<asp:Table ID="_tblRCFAStatus" runat="server" CellPadding="2" CellSpacing="2" BackColor="white"
					Style="width: 98%" EnableViewState="true">
					<asp:TableHeaderRow CssClass="Header">
						<asp:TableHeaderCell HorizontalAlign="left">
							<asp:Label ID="_lblRCFAStatus" runat="server" EnableViewState="false" Text="Shutdown Category"
								SkinID="LabelWhite"></asp:Label>
						</asp:TableHeaderCell>
					</asp:TableHeaderRow>
					<asp:TableRow CssClass="Border">
						<asp:TableCell CssClass="Border">
							<asp:RadioButtonList ID="_rblcblRCFAStatus" runat="server" RepeatDirection="Horizontal">
								<asp:ListItem Text="Black Mill (No Power/Steam)" Value="Black Mill (No Power/Steam)">
								</asp:ListItem>
								<asp:ListItem Text="Cold Mill (No Steam)" Value="Cold Mill (No Steam)">
								</asp:ListItem>
								<asp:ListItem Text="Total Mill (Utilities Available)" Value="Total Mill (Utilities Available)">
								</asp:ListItem>
								<asp:ListItem Text="Partial Mill" Value="Partial Mill">
								</asp:ListItem>
							</asp:RadioButtonList>
						</asp:TableCell>
					</asp:TableRow>
				</asp:Table>
				<asp:Table ID="_tblMiscellaneous" runat="server" CellPadding="2" CellSpacing="2"
					BackColor="white" Style="width: 98%" EnableViewState="true">
					<%--<asp:TableHeaderRow CssClass="Header">
            <asp:TableHeaderCell ColumnSpan="3" HorizontalAlign="left">
                <asp:Label ID="_lblOther" runat="server" EnableViewState="false" Text="<%$ Resources:Shared,lblOther %>"
                    SkinID="LabelWhite"></asp:Label>
            </asp:TableHeaderCell>
        </asp:TableHeaderRow>--%>
					<asp:TableRow CssClass="Border">
						<asp:TableCell Width="50%">
							<asp:Label ID="_lblRCFAActionLeader" runat="server" Text="Outage Coordinator"></asp:Label>
							&nbsp;&nbsp;<asp:DropDownList runat="server" ID="_ddlRCFALeader">
							</asp:DropDownList></asp:TableCell>
					</asp:TableRow>
				</asp:Table>
				<asp:Table ID="Table2" runat="server" BorderWidth="0" CellPadding="2" CellSpacing="2"
					BackColor="white" Style="width: 99%">
					<asp:TableRow CssClass="Border">
						<asp:TableCell Width="25%">
							<asp:Label ID="_lblCreatedBy" runat="server"></asp:Label></asp:TableCell>
						<asp:TableCell Width="25%">
							<asp:Label ID="_lblCreatedDate" runat="server"></asp:Label></asp:TableCell>
						<asp:TableCell Width="25%">
							<asp:Label ID="_lblUpdatedBy" runat="server"></asp:Label></asp:TableCell>
						<asp:TableCell Width="25%">
							<asp:Label ID="_lblLastUpdateDate" runat="server"></asp:Label></asp:TableCell>
					</asp:TableRow>
				</asp:Table>
			</asp:Panel>
			<div align="center">
				<asp:Button ID="_btnSubmit" Text="Submit Outage Plan" runat="server" />&nbsp;&nbsp;
				<asp:Button ID="_btnDelete" runat="server" Text="<%$RIResources:Shared,Delete%>" />
				<asp:Button ID="_btnAttachments" runat="server" Text='<%$RIResources:Shared,Attachments %>' />
			</div>
			<ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" ConfirmText="<%$RIResources:Shared,ConfirmDelete %>"
				TargetControlID="_btnDelete"> 
			</ajaxToolkit:ConfirmButtonExtender>
			<%--<asp:Panel ID="_pnlUpdateButtons" runat="server" HorizontalAlign="center" Visible="false">
            </asp:Panel>
            --%>
			<asp:Panel ID="_pnlViewSearch" runat="server" CssClass="ModalPopup" ScrollBars="none">
				<asp:Panel ID="_pnlGrid" runat="server" ScrollBars="vertical">
					<asp:GridView CssClass="Border" Width="99%" BorderColor="Black" BorderWidth="2" ID="_gvIncidentListing"
						runat="server" AutoGenerateColumns="False" DataKeyNames="RINUMBER" EnableViewState="false"
						AllowSorting="false">
						<Columns>
							<asp:BoundField DataField="EVENTDATE" HeaderText="Event Date" SortExpression="EVENTDATE"
								DataFormatString="{0:d}" HtmlEncode="false" />
							<asp:BoundField DataField="SITENAME" HeaderText="Site" SortExpression="SITENAME" />
							<asp:BoundField DataField="RISUPERAREA" HeaderText="Business Unit" SortExpression="RISUPERAREA" />
							<asp:BoundField DataField="SUBAREA" HeaderText="Area" SortExpression="SUBAREA" />
							<asp:BoundField DataField="AREA" HeaderText="Line/System" SortExpression="AREA" />
							<asp:BoundField DataField="RINUMBER" HeaderText="RI Number" SortExpression="RINUMBER" />
							<asp:HyperLinkField DataTextField="INCIDENT" HeaderText="Incident Title" SortExpression="INCIDENT"
								DataNavigateUrlFields="RINUMBER" DataNavigateUrlFormatString="~/RI/EnterNewRI.aspx?RINumber={0}"
								Target="_self" />
							<asp:HyperLinkField DataTextField="RCFA_TYPE" HeaderText="Type" SortExpression="RCFA_TYPE"
								DataNavigateUrlFields="RINUMBER" DataNavigateUrlFormatString="~/CEReporting/frmCrystalReport.aspx?Report=ExecutiveSummary?RINumber={0}"
								Target="_blank" />
							<asp:BoundField DataField="COST" HeaderText="Cost" SortExpression="COST" ItemStyle-HorizontalAlign="Right"
								DataFormatString="{0:c0}" HtmlEncode="false" />
							<asp:BoundField DataField="TOTCOST" HeaderText="Financial Impact" SortExpression="TOTCOST"
								ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c0}" HtmlEncode="false" />
						</Columns>
					</asp:GridView>
				</asp:Panel>
				<%-- <asp:Button ID="_btnClose" runat="server" Text="Close" />--%>
			</asp:Panel>
			<ajaxToolkit:ModalPopupExtender ID="_mpeViewSearch" runat="server" TargetControlID="_btnViewSearch"
				PopupControlID="_pnlViewSearch" BackgroundCssClass="modalBackground" DropShadow="true"
				OkControlID="_btnClose" CancelControlID="_btnClose">
			</ajaxToolkit:ModalPopupExtender>
		</ContentTemplate>
	</Asp:UpdatePanel>
</asp:Content>
