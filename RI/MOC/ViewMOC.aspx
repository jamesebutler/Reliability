<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="ViewMOC.aspx.vb"
	Inherits="MOC" EnableTheming="true" StylesheetTheme="RIBlackGold" Theme="RIBlackGold"
	EnableEventValidation="false" Title="International Paper: MOC" Trace="false"
	MaintainScrollPositionOnPostback="true" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<%@ Register Src="~/RI/User Controls/Common/ucMOCStatus.ascx" TagName="ucMOCStatus" TagPrefix="IP" %>

<asp:Content ID="_contentMain" ContentPlaceHolderID="_cphMain" EnableViewState="true" runat="Server">

	<IP:SiteLocation ID="_siteLocation" runat="server"/>
	<IP:DateRange ID="_DateRange" runat="server" />
    <IP:ucMOCStatus runat="server" ID="MOCStatus" name="MOCStatus" DisplayMode="Search" />
	<IP:MOCType ID="_MOCType" runat="server" DisplayMode="Search" />

    <asp:table runat="server"> 
        <asp:TableRow ID="_tr"  CssClass="Header" runat="server">
            <asp:TableCell HorizontalAlign="left"><asp:Label ID="_lbMoc" runat="server" Text="<%$RIResources:Shared,MOCNumber %>"
                SkinID="LabelWhite" Width="120"/>  
                 <asp:TextBox ID="_tbMocNumber" runat="server"></asp:TextBox></asp:TableCell><asp:TableCell HorizontalAlign="left" >  
		        <asp:Label ID="_lbOwner" runat="server" Text="<%$RIResources:Shared,MOC Owner %>"
                SkinID="LabelWhite" />&nbsp;
                <ajaxToolkit:CascadingDropDown ID="_cddlOwner" runat="server" Category="Trigger" LoadingText="[Loading Owner...]"
                    PromptText="   " ServiceMethod="GetMOCOwner" ServicePath="~/CascadingLists.asmx"
                    TargetControlID="_ddlOwner" ParentControlID="ctl00__cphMain__siteLocation__ddlFacility">
                </ajaxToolkit:CascadingDropDown>
                 <asp:DropDownList ID="_ddlOwner" runat="server"></asp:DropDownList></asp:TableCell><asp:TableCell HorizontalAlign="left" Width="25%" >  
		        <asp:Label ID="_lbInitiator" runat="server" Width="60" Text="<%$RIResources:Shared,Initiator %>"
                SkinID="LabelWhite" />
                <ajaxToolkit:CascadingDropDown ID="_cddlInitiator" runat="server" Category="Trigger" LoadingText="[Loading Initiator...]"
                    PromptText="   " ServiceMethod="GetMOCInitiator" ServicePath="~/CascadingLists.asmx"
                    TargetControlID="_ddlInitiator" ParentControlID="ctl00__cphMain__siteLocation__ddlFacility">
                </ajaxToolkit:CascadingDropDown>
                 <asp:DropDownList ID="_ddlInitiator" runat="server"></asp:DropDownList></asp:TableCell><%--		    <asp:TableCell HorizontalAlign="left"><asp:Label runat="server" Text="<%$RIResources:Shared,Status %>"
                SkinID="LabelWhite" Width="120"/>  
                 <asp:DropDownList ID="_ddlStatus" runat="server"></asp:DropDownList></asp:TableCell>--%></asp:TableRow><asp:TableRow ID="TableRow1"  CssClass="Header" runat="server">
            <asp:TableCell HorizontalAlign="left" ColumnSpan="3">
                <asp:Label ID="_lblTitleSearch" runat="server" SkinID="LabelWhite" Text="<%$ RIResources:Shared,Title Search %>"></asp:Label>
                    &nbsp;&nbsp;<asp:TextBox runat="server" ID="_txtTitleSearch" width="30%" MaxLength="50" ></asp:TextBox>
                </asp:TableCell></asp:TableRow></asp:table><IP:MOCClass ID="MOCClass" runat="server" DisplayMode="Search" />
	<IP:MOCCategory ID="MOCCategory" runat="server" DisplayMode="Search" />

    
	<Asp:UpdatePanel ChildrenAsTriggers="true" ID="_upViewScreen" runat="server" EnableViewState="true"
		UpdateMode="always">
		<ContentTemplate>
		<asp:Label ID="_lblRecordCount" runat="server" Text="<%$RIResources:Shared,RecordCount %>" BackColor="Black"
		ForeColor="White"></asp:Label><asp:Label ID="_lblRecCount" runat="server" Text="0" BackColor="Black" ForeColor="White"></asp:Label><div style="text-align: center">
				<asp:Button ID="_btnViewUpdate" Text="<%$RIResources:Shared,ViewUpdate %>" runat="server" />
				<asp:Button ID="_btnExcel" Text="<%$RIResources:Shared,Excel %>" runat="server" />
			</div> 
			<br />
			<asp:GridView ID="_gvMOCListing" runat="server" CssClass="Border" BorderColor="Black"
				BorderWidth="2" AutoGenerateColumns="False" DataKeyNames="MOCNUMBER" EnableViewState="false"
				AllowSorting="true" Width="100%" HeaderStyle-BorderColor="red">
				<Columns>
					<asp:BoundField DataField="SiteName" HeaderText="<%$RIResources:Shared,Facility %>"
						HtmlEncode="false" SortExpression="sitename" />
					<asp:BoundField DataField="STARTDATE" HeaderText="<%$RIResources:Shared,Implementation Date %>" DataFormatString="{0:d}"
						HtmlEncode="false" SortExpression="startdate,enddate" />
					<asp:BoundField DataField="RISUPERAREA" HeaderText="<%$RIResources:Shared,BusinessUnit %>"
						SortExpression="risuperarea, startdate, enddate" />
					<asp:BoundField DataField="SUBAREA" HeaderText="<%$RIResources:Shared,Area %>"
						SortExpression="subarea, startdate, enddate" />
					<asp:BoundField DataField="Area" HeaderText="<%$RIResources:Shared,Line %>" SortExpression="area, startdate, enddate" />
					<asp:HyperLinkField DataTextField="MOCNumber" HeaderText="<%$RIResources:Shared,Report %>"
							SortExpression="mocnumber, startdate, enddate" DataNavigateUrlFields="MOCNUMBER" DataNavigateUrlFormatString="http://gpimv.graphicpkg.com/cereporting/CrystalReportDisplay.aspx?Report=MOCSummary&mocNumber={0}"
							Target="_blank" />
					<asp:HyperLinkField DataTextField="Title" HeaderText="<%$RIResources:Shared,Title %>"
						DataNavigateUrlFields="MOCNumber" DataNavigateUrlFormatString="~/MOC/EnterMOC.aspx?MOCNumber={0}"
						Target="_self" SortExpression="title,sitename" />
<%--					<asp:HyperLinkField DataTextField="OutageTitle" HeaderText='<%$ Resources:Shared,lblTitle %>'
						DataNavigateUrlFields="MOCNumber" DataNavigateUrlFormatString="~/MOC/EnterMOC.aspx?MOCNumber={0}"
						Target="_self" />
--%>
                    <asp:BoundField DataField="person" HeaderText="<%$RIResources:Shared,Initiator %>" SortExpression="person, startdate, enddate" />
					<asp:BoundField DataField="MOCType" HeaderText="<%$RIResources:Shared,TypeChange %>"
						SortExpression="moctype,sitename,startdate,enddate" />
					<asp:BoundField DataField="Savings" HeaderText="<%$RIResources:Shared,Savings %>" DataFormatString="{0:c0}" HtmlEncode="false" 
					    ItemStyle-HorizontalAlign='Right' SortExpression="savings, sitename,startdate,enddate" />
                    <asp:BoundField DataField="Status" HeaderText="<%$RIResources:Shared,Status %>" SortExpression="Status, startdate, enddate" />
				</Columns>
			</asp:GridView>
	
		</ContentTemplate>
	</Asp:UpdatePanel>
<%--	<div class="labellogin">
                    <IP:IPLogin ID="_login" runat="server" />
                </div>--%>
	<%--<ajaxToolkit:CascadingDropDown ID="_cddlCoordinator" runat="server" Category="Leader"
		LoadingText="[Loading Coordinator...]" PromptText="   " ServiceMethod="GetPerson"
		ServicePath="~/CascadingLists.asmx" TargetControlID="_ddlPlanner" ParentControlID="ctl00__cphMain__siteLocation__ddlFacility">
	</ajaxToolkit:CascadingDropDown>--%>
	
</asp:Content>
