<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="Outage.aspx.vb"
    Inherits="RI_Outage" EnableTheming="true" StylesheetTheme="RIBlackGold" Theme="RIBlackGold"
    EnableEventValidation="false" Title="International Paper: Outage Planner" Trace="false"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="../RI/User Controls/Common/ucOutageGanttChart.ascx" TagName="ucOutageGanttChart"
    TagPrefix="IP" %>
<%@ Register Assembly="RealWorld.Grids" Namespace="RealWorld.Grids.RealWorld.Grids"
    TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" EnableViewState="true"
    runat="Server">
    <asp:Table ID="_tblOutageStatus" runat="server" CellPadding="2" CellSpacing="2" BackColor="white"
        Style="width: 45%" EnableViewState="true">
        <asp:TableHeaderRow CssClass="Header">
            <asp:TableHeaderCell HorizontalAlign="left">
                <asp:Label ID="_lblOutageStatus" runat="server" EnableViewState="false" Text="<%$RIResources:OUTAGE,SDCategory %>"
                    SkinID="LabelWhite"></asp:Label>
            </asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow CssClass="Border">
            <asp:TableCell CssClass="Border">
                <asp:CheckBoxList ID="_cblSDCategory" runat="server" RepeatDirection="Horizontal" >
                    <asp:ListItem Text="<%$RIResources:Outage,AllOutages %>" Value="All" Selected="True">
                    </asp:ListItem>
                    <asp:ListItem Text="<%$RIResources:Shared,BlackMill %>" Value="Black Mill (No Power/Steam)">
                            </asp:ListItem>
                            <asp:ListItem Text="<%$RIResources:Outage,ColdMill %>" Value="Cold Mill (No Steam)">
                            </asp:ListItem>
                             <asp:ListItem Text="<%$RIResources:Outage,TotalMill %>" Value="Total Mill (Utilities Available)">
                            </asp:ListItem>
                            <asp:ListItem Text="<%$RIResources:Outage,PartialMill %>" Value="Partial Mill">
                            </asp:ListItem>
                            <asp:ListItem Text="<%$RIResources:Outage,FieldDay %>" Value="Field Day">
                            </asp:ListItem>
                            <asp:ListItem Text="<%$RIResources:Outage,MajorProject %>" Value="Major Project">
                            </asp:ListItem>
                            <asp:ListItem Text="<%$RIResources:Outage,LOO%>" Value="LOO">
                            </asp:ListItem>
                            <asp:ListItem Text="<%$RIResources:Outage,Turbine Generator%>" Value="TG">
                            </asp:ListItem>
                </asp:CheckBoxList>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <asp:Table ID="_tblSite" runat="server" BorderWidth="0" CellPadding="2" CellSpacing="2"
            BackColor="white" Style="width: 98%" EnableViewState="false">
        <asp:TableRow CssClass="Header">
            <asp:TableCell ColumnSpan="3" HorizontalAlign="left">
                <asp:Label ID="_lblSite" runat="server" text="<%$RIResources:Shared,Site %>" SkinID="LabelWhite"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    <asp:TableRow><asp:TableCell><IP:SiteLocation ID="_siteLocation" runat="server" HideLineBreak="true" />
    </asp:TableCell></asp:TableRow>
         <asp:TableRow CssClass="Header">
            <asp:TableCell ColumnSpan="3" HorizontalAlign="left">
                <asp:Label ID="_lblDate" runat="server" text="<%$RIResources:Shared,Date %>" SkinID="LabelWhite"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    <asp:TableRow><asp:TableCell><IP:DateRange ID="_DateRange" runat="server" />
     </asp:TableCell></asp:TableRow></asp:Table>
    
    <asp:Table ID="_tblMiscellaneous" runat="server" CellPadding="2" CellSpacing="2"
        BackColor="white" Style="width: 98%" EnableViewState="true">
        <asp:TableRow CssClass="Border" >
            <asp:TableCell ColumnSpan="3">
                <asp:Label ID="_lblTitleSearch" runat="server" Text="<%$RIResources:Shared,Title %>"></asp:Label>
                &nbsp;<asp:TextBox ID="_tbTitleSearch" runat="server" Width="80%"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow CssClass="Border" >
            <asp:TableCell ColumnSpan="3">
                <asp:Label ID="_lblOutageCoord" runat="server" Text="<%$RIResources:Outage,OutageCoord %>"></asp:Label>
                &nbsp;&nbsp;<asp:DropDownList runat="server" ID="_ddlOutageCoord">
                </asp:DropDownList>
            </asp:TableCell>
        </asp:tablerow>
        <asp:TableRow CssClass="Border" >
            <asp:TableCell Width="50%">
                <asp:Label ID="_lblContractor" runat="server" Text="<%$RIResources:Outage,Contractor %>"></asp:Label>
                &nbsp;&nbsp;<asp:DropDownList runat="server" ID="_ddlContractor">
                </asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell Width="50%">
                <asp:Label ID="_lblResoruce" runat="server" Text="<%$RIResources:Outage,Resources %>"></asp:Label>
                &nbsp;&nbsp;<asp:DropDownList runat="server" ID="_ddlResources">
                </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        	<asp:TableRow CssClass="Border">
	        <asp:TableCell ColumnSpan="3" HorizontalAlign="left">
			    <asp:Label ID="_lbAnnualOutage" runat="server" EnableViewState="false" Text="<%$RIResources:OUTAGE,AnnualOutageOnly %>"></asp:Label>
					&nbsp;<asp:CheckBox ID="_cbAnnualOutage" runat="server" ></asp:CheckBox>	
			</asp:TableCell>
		</asp:TableRow>
</asp:Table>
    <ajaxToolkit:CascadingDropDown ID="_cddlCoordinator" runat="server" Category="Leader"
        LoadingText="[Loading Coordinator...]" PromptText="   " ServiceMethod="GetOutageCoord"
        ServicePath="~/CascadingLists.asmx" TargetControlID="_ddlOutageCoord" ParentControlID="ctl00__cphMain__siteLocation__ddlFacility">
    </ajaxToolkit:CascadingDropDown>


    <Asp:UpdatePanel ChildrenAsTriggers="true" ID="_upViewScreen" runat="server" EnableViewState="true"
        UpdateMode="always">
        <ContentTemplate>
            <asp:Label ID="_lblRecordCount" runat="server" Text="<%$RIResources:Shared,RecordCount %>"
                BackColor="Black" ForeColor="White"></asp:Label>
            <asp:Label ID="_lblRecCount" runat="server" Text="0" BackColor="Black" ForeColor="White"></asp:Label>
            <div style="text-align: center">
                <asp:Button ID="_btnViewUpdate" Text="<%$RIResources:Shared,ViewUpdate %>" runat="server" />
                <asp:Button ID="_btnExcel" Text="<%$RIResources:Shared,Excel %>" runat="server" />
                <asp:Button ID="_btnGantt" Text="<%$RIResources:Outage,GanttView %>" runat="server" />
            </div>
            <br />
            <cc1:GroupingGridView ID="_gvOutageListing" runat="server" CssClass="Border" BorderColor="Black"
                BorderWidth="2" AutoGenerateColumns="False" DataKeyNames="OutageNUMBER" EnableViewState="false"
                AllowSorting="true" Width="100%" GroupingDepth="10" HeaderStyle-BorderColor="red">
                <Columns>
<%--                    <asp:BoundField DataField="STARTDATE" HeaderText="<%$RIResources:Shared,StartDate %>"
                        dataformatstring="{0:d}" htmlencode="false" SortExpression="startdate,enddate,sitename,outagetitle,shutdowncategory,outagenumber" />
                    <asp:BoundField DataField="ENDDATE" HeaderText="<%$RIResources:Shared,EndDate %>"
                        DataFormatString="{0:d}" HtmlEncode="false" SortExpression="enddate,sitename,outagetitle,shutdowncategory,outagenumber" />
--%>
                    <asp:BoundField DataField="OUTAGENUMBER" HeaderText="<%$RIResources:Shared,Number %>"
                        visible="True" />
                    <asp:TemplateField HeaderText="<%$RIResources:Shared,Report%>" SortExpression="Outagenumber">
                            <ItemTemplate>
                                <a target="_blank" href="<%#String.Format(Page.ResolveClientUrl("http://gpimv.graphicpkg.com/cereporting/CrystalReportDisplay.aspx?Report=OutageDetailSummary&OutageNumber={0}"), Eval("OutageNumber")) %>">
                                <%--<a target="_blank" href="<%#string.format(Page.ResolveClientUrl("../../CEReporting/CrystalReportDisplay.aspx?Report=OutageDetailSummary&OutageNumber={0}"),EVAL("OutageNumber")) %>">--%>
                                    Report
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="<%$RIResources:Shared,StartDate %>"
                            SortExpression="startdate,enddate,sitename,outagetitle,shutdowncategory,outagenumber">
                            <ItemTemplate>
                                <asp:Literal ID="Literal1" runat="server" Text='<%#Convert.ToDateTime(Eval("startdate")).ToShortDateString( )%>'></asp:Literal>
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="<%$RIResources:Shared,EndDate %>"
                            SortExpression="enddate,sitename,outagetitle,shutdowncategory,outagenumber">
                            <ItemTemplate>
                                <asp:Literal ID="Literal2" runat="server" Text='<%#Convert.ToDateTime(Eval("enddate")).ToShortDateString( )%>'></asp:Literal>
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SITENAME" HeaderText="<%$RIResources:Shared,Facility %>"
                        SortExpression="sitename,startdate, enddate,outagetitle,shutdowncategory,outagenumber" />
                    <asp:HyperLinkField DataTextField="OutageTitle" HeaderText="<%$RIResources:Shared,Title %>"
                        DataNavigateUrlFields="OutageNumber" DataNavigateUrlFormatString="~/Outage/EnterOutage.aspx?OutageNumber={0}"
                        Target="_self" />
                    <asp:BoundField DataField="SHUTDOWNCATEGORY" HeaderText="<%$RIResources:Outage,SDCategory %>"
                        SortExpression="shutdowncategory,startdate,enddate,sitename,outagetitle,outagenumber" />
                    <asp:BoundField DataField="Person" HeaderText='<%$ RIResources:Outage,OutageCoord %>' />
                    <%--<asp:BoundField DataField="Contractor" HeaderText='<%$ RIResources:Outage,Contractor %>' />
                    --%><asp:HyperLinkField DataTextField="filename" HeaderText="<%$RIResources:Shared,Attachments %>"
                        DataNavigateUrlFields="filename" DataNavigateUrlFormatString="~/Outage/FileUpload.aspx?file={0}"
                        Target="_blank" />
                    <asp:BoundField DataField="busunitarea" HeaderText="<%$RIResources:Shared,BusinessUnitArea %>"
                        SortExpression="risuperarea,subarea,area,startdate, enddate,sitename,outagetitle,outagenumber" />
                </Columns>
            </cc1:GroupingGridView>

            <iframe src="GanttViewerAnyChart.aspx" style="display: none;" runat="server" id="_ifrGantt"
                width='100%' frameborder="0" scrolling="auto"></iframe>
        </ContentTemplate>
    </Asp:UpdatePanel>
</asp:Content>
