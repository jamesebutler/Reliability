<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucFunctionalLocationTree.ascx.vb"
	Inherits="RI.RI_ucFunctionalLocationTree" %>
<%--<asp:Label ID="_divTest" CssClass="autocompletelabel" SkinID="none" runat="server" BackColor="White"  ToolTip="Type the first couple of numbers for the Functional Location" />
<Asp:UpdatePanel ID="_udpFunctionalLocation" runat="server" UpdateMode="Conditional">
	<ContentTemplate>
		<table>
			<tr>
				<td>
					<asp:Label ID="_lblFunctionalLocation" runat="server" Text="<%$Resources:Shared,lblFunctionalLocation%>"></asp:Label></td>
				<td>
					<asp:TextBox onchange="customClickhandler();" ID="_txtFunctionalLocation" Width="500px" runat="server" Visible="true"></asp:TextBox><asp:Label
						runat="server" ID="_txtFunctionalLocation2" />
				</td>
			</tr>
		</table>
		<%--ScriptPath="CustomAutoCompleteBehavior.js"-- %>
		<ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="_txtFunctionalLocation"
			WatermarkText="Enter Functional Location" WatermarkCssClass="watermarked" />
		
		<ajaxToolkit:AutoCompleteExtender runat="server" ID="AutoCompleteExtender1" TargetControlID="_txtFunctionalLocation" 
			ServicePath="AutoComplete.asmx" ServiceMethod="GetListOfEquipmentIDs" MinimumPrefixLength="1"
			CompletionInterval="1" EnableCaching="true" CompletionSetCount="300" BehaviorID="autoCompleteBehavior"
			 UseContextKey=true
			CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
			CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters="">
			
			</ajaxToolkit:AutoCompleteExtender> 
		<asp:HiddenField ID="_hdTarget" runat="server" />
		<asp:HiddenField ID="_hdPopup" runat="server" />
		<ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="_hdTarget"
			PopupControlID="_hdPopup" Position="Bottom" />
	</ContentTemplate>
</Asp:UpdatePanel>
--%>

<script language="javascript">
    var lastCheckedNode=null;
    
    function treeSelect(){
    var obj = window.event.srcElement;
    var treeNodeFound = false;
    if (obj.tagName == "A"){
        //if (obj.target=="populated") return false
        //else return true;
        if (lastCheckedNode!=null){
            lastCheckedNode.checked=false;}
        
        obj.checked=true;
        
        var objID = obj.id+'CheckBox';
        objID=objID.replace("_tvFunctionalLocationt","_tvFunctionalLocationn")
        var parentcb = document.getElementById(objID);
        
        if (parentcb!=null){
			parentcb.checked=true;
			lastCheckedNode=parentcb;
			lastCheckedNode.backgroundColor="";
        }
        //obj.style.backgroundColor="#ff0000"
        }
    else if (obj.tagName =="IMG"){
        return true;}
    else if (obj.type=="checkbox" && obj.tagName=="INPUT"){
        if (lastCheckedNode!=null){
            lastCheckedNode.checked=false;}
        lastCheckedNode=obj;
        //alert(obj.title);
        return true;
}
    }    
   
</script>

<asp:Button ID="_btnTree" runat="server" Text="Functional Tree" />
<ajaxToolkit:ModalPopupExtender ID="_mpeTree" runat="server" TargetControlID="_btnTree"
	PopupControlID="_pnlFunctionalLocation" BackgroundCssClass="modalBackground"
	DropShadow="true" OkControlID="_btnClose" CancelControlID="_btnClose">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel EnableTheming="false" ID="_pnlFunctionalLocation" runat="server" BackColor="Gray"
	BorderColor="black" BorderWidth="1" Width="99%"  Style="display: none;">
	<asp:Table ID="_tblFunctional" CellPadding="2" CellSpacing="0" runat="server" Width="100%">
		<asp:TableHeaderRow CssClass="Header">
			<asp:TableHeaderCell HorizontalAlign="Left">
				<asp:Label ID="_lblHeader" SkinID="LabelWhite" runat="server" Text="<%$RIResources:Shared,FunctionalLocation%>"></asp:Label></asp:TableHeaderCell><asp:TableHeaderCell
					HorizontalAlign="Right">
					<asp:Button ID="_btnClose" runat="server" Text="<%$RIResources:Shared,Close %>" />
				</asp:TableHeaderCell>
		</asp:TableHeaderRow>
		<asp:TableRow CssClass="BorderWhite">
			<asp:TableCell ColumnSpan="2" EnableTheming="false">
				<asp:Panel ID="_pnlTree" runat=server ScrollBars="Auto" Height="300px">
					<asp:TreeView BorderWidth="1" CssClass="TreeViewBackground" SelectedNodeStyle-BorderWidth="3"
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
