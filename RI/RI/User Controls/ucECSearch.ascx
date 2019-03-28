<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucECSearch.ascx.vb" Inherits="ucECSearch" %>
<table>
	<tr>
		<td valign="middle">
			<asp:Button ID="_btnTree" runat="server" Text="<%$RIResources:Shared,FunctionalLocation%>" />
			</td>
			
		<td valign="top">
			<asp:TextBox onchange="customClickhandler(this);" ID="_txtFunctionalLocationSearch"
				Width="400px" runat="server" Visible="true"  MaxLength=30></asp:TextBox>
			<asp:Button ID="_btnSearch" Text="<%$RIResources:Shared,Search%>" runat="server" />&nbsp;&nbsp;			 
		</td>
	</tr>
	<tr>
		<td>
		</td>
		<td>
			<asp:Label runat="server" ID="_txtFunctionalLocationSearch2" /></td>
	</tr>
</table>
<ajaxToolkit:ModalPopupExtender ID="_mpeSearch" runat="server" TargetControlID="_btnSearch"
	PopupControlID="_pnlEquipmentSearch" BackgroundCssClass="modalBackground" DropShadow="true"
	OkControlID="_btnCloseEquipmentSearch" CancelControlID="_btnCloseEquipmentSearch">
</ajaxToolkit:ModalPopupExtender>
<ajaxToolkit:AutoCompleteExtender runat="server" ID="_aceFunctionalLocation2" TargetControlID="_txtFunctionalLocationSearch"
	ServicePath="~/ri/User Controls/Common/AutoComplete.asmx" ServiceMethod="GetListOfEquipmentIDs"
	MinimumPrefixLength="2" CompletionInterval="1" EnableCaching="false" CompletionSetCount="300"
	BehaviorID="autoCompleteBehavior1" UseContextKey="true" CompletionListCssClass="autocomplete_completionListElement"
	CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
	DelimiterCharacters="">
</ajaxToolkit:AutoCompleteExtender>
<asp:Panel ID="_pnlEquipmentSearch" runat="server" Visible="true"  DefaultButton="_btnViewUpdate" CssClass="modalPopup"
	Width="98%" ScrollBars="none" Style="display: none;">
	<table border="0" cellpadding="2" cellspacing="2" style="width: 98%">
		<tr>
			<th colspan="3">
				<IP:Banner ID="_bannerHeader" runat="server" BannerMessage="<%$RIResources:Shared,FunctionalLocation%>"
					DisplayPopupBanner="true" />
			</th>
		</tr>
		<tr class="BorderSecondary">
			<td align="left" width="33%">
				<asp:Label ID="_lblCriticality" runat="server" Text="<%$ RIResources:Shared,Criticality %>"></asp:Label><br />
				<asp:DropDownList Width="200" ID="_ddlCriticality" runat="server" AutoPostBack="false">
				</asp:DropDownList>
			</td>
			<td align="left" width="33%">
				<asp:Label ID="_lblEquipmentClass" runat="server" Text="<%$ RIResources:Shared,EquipmentClass %>"></asp:Label><br />
				<asp:DropDownList Width="200" ID="_ddlEquipmentClass" AutoPostBack="false" runat="server">
				</asp:DropDownList>
			</td>
			<td align="left" width="33%">
				<asp:Label ID="_lblEquipmentType" runat="server" Text="<%$ RIResources:Shared,EquipmentType %>"></asp:Label><br />
				<asp:DropDownList Width="200" ID="_ddlEquipmentType" AutoPostBack="false" runat="server">
				</asp:DropDownList>
			</td>
		</tr>
		<tr class="Border">
			<td colspan="4" style="text-align: left;">
				<table width="100%" cellpadding="0" cellspacing="0">
					<tr>
						<td width="50%">
							<asp:Label ID="_lblFunctionalLocation" runat="server" Text="<%$ RIResources:Shared,FunctionalLocation %>"></asp:Label>
							<br />
							<asp:TextBox ID="_txtFunctionalLocation" onchange="customClickhandler(this);performEquipmentSearch();" runat="server"
								AutoPostBack="false" Width="90%"></asp:TextBox></td>
						<td width="50%">
							<asp:Label ID="_lblTitleDescription" runat="server" Text="<%$ RIResources:Shared,TitleDescription %>"></asp:Label>
							<br />
							<asp:TextBox ID="_txtFunctionalLocation2" onchange="customClickhandler(this,true);performEquipmentSearch();"
								runat="server" AutoPostBack="false" Width="90%"></asp:TextBox></td>
					</tr>
				</table>
			</td>
		</tr>
		<tr class="BorderSecondary">
			<td colspan="4" style="text-align: center">
				<asp:Label ID="_lblLimit" runat="server" Text="<%$ RIResources:Shared,Limit %>"></asp:Label>
				<asp:DropDownList ID="_ddlLimit" runat="server" Width="100px">
				</asp:DropDownList>
				<asp:Button ID="_btnViewUpdate"  UseSubmitBehavior="false" runat="server" Text="<%$ RIResources:Shared,Search %>"
					EnableViewState="false" />
			</td>
		</tr>
		<tr>
			<td colspan="4" align="center">
				<asp:Panel ID="_pnlResults" Width="98%" runat="server" ScrollBars=both Height="300px">
				</asp:Panel>
			</td>
		</tr>
		<tr>
			<td colspan="3">
				<asp:Button ID="_btnCloseEquipmentSearch" runat="server" Text="<%$ RIResources:Shared,Close%>"/></td>
		</tr>
	</table>
</asp:Panel>
<ajaxToolkit:AutoCompleteExtender runat="server" ID="_aceFunctionalLocation" TargetControlID="_txtFunctionalLocation"
	ServicePath="~/ri/User Controls/Common/AutoComplete.asmx" ServiceMethod="GetListOfEquipmentIDs"
	MinimumPrefixLength="2" CompletionInterval="1" EnableCaching="false" CompletionSetCount="300"
	BehaviorID="autoCompleteBehavior2" UseContextKey="true" CompletionListCssClass="autocomplete_completionListElement"
	CompletionListItemCssClass="autocomplete_listItem" FirstRowSelected="true" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
	DelimiterCharacters="">
</ajaxToolkit:AutoCompleteExtender>
<ajaxToolkit:AutoCompleteExtender runat="server" ID="_aceFunctionalDescription" TargetControlID="_txtFunctionalLocation2"
	ServicePath="~/ri/User Controls/Common/AutoComplete.asmx" ServiceMethod="GetListOfEquipmentDescriptions"
	MinimumPrefixLength="2" CompletionInterval="1" FirstRowSelected="true" EnableCaching="false"
	CompletionSetCount="300" BehaviorID="autoCompleteBehavior3" UseContextKey="true"
	CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
	CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters="">
</ajaxToolkit:AutoCompleteExtender>

<ajaxToolkit:ModalPopupExtender ID="_mpeTree" Enabled=false runat="server" TargetControlID="_btnTree"
	PopupControlID="_pnlFunctionalLocation" BackgroundCssClass="modalBackground"
	DropShadow="true" OkControlID="_btnClose" CancelControlID="_btnClose">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel EnableTheming="false" ID="_pnlFunctionalLocation" runat="server" BackColor="Gray"
	BorderColor="black" BorderWidth="1" Width="99%" Style="display: none;">
	<asp:Table ID="_tblFunctional" CellPadding="2" CellSpacing="0" runat="server" Width="100%">
		<asp:TableHeaderRow CssClass="Header">
			<asp:TableHeaderCell HorizontalAlign="Left">
				<asp:Label ID="_lblHeader" SkinID="LabelWhite" runat="server" Text="<%$RIResources:Shared,FunctionalLocation%>"></asp:Label></asp:TableHeaderCell><asp:TableHeaderCell
					HorizontalAlign="Right">
					<asp:Button ID="_btnClose" runat="server" Text="<%$RIResources:Shared,ApplySelection %>" />
				</asp:TableHeaderCell>
		</asp:TableHeaderRow>
		<asp:TableRow CssClass="BorderWhite">
			<asp:TableCell ColumnSpan="2" EnableTheming="false">
				<asp:Panel ID="_pnlTree" runat="server" ScrollBars="Auto" Height="300px">														
					<asp:TreeView  EnableViewState=false BorderWidth="1" CssClass="TreeViewBackground" SelectedNodeStyle-BorderWidth="3"
						EnableTheming="false" ID="_tvFunctionalLocation" ExpandDepth="0" runat="server"
						PopulateNodesFromClient="true" ShowCheckBoxes="All" ShowLines="True" ShowExpandCollapse="true"
						EnableClientScript="true" OnTreeNodePopulate="PopulateNode" onclick="treeSelect()">
						<HoverNodeStyle BackColor="green" CssClass="BorderWhite" />
						<NodeStyle ChildNodesPadding="1" NodeSpacing="2" CssClass="TreeView" />
					</asp:TreeView>
				</asp:Panel>
			</asp:TableCell>
		</asp:TableRow>
	</asp:Table>
</asp:Panel>
