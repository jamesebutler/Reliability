<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="Reporting.aspx.vb"
    Inherits="Outage_Reporting" Trace="false" Title="International Paper: Outage Reporting" EnableSessionState="true" EnableViewState="false" %>
<%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server" >    
<%--    <Asp:UpdatePanel ID="_upReporting" ChildrenAsTriggers="false" runat="server" UpdateMode="Conditional">
--%>	
    <Asp:UpdatePanel ID="_upSite" runat="server" UpdateMode="Conditional">
		<Triggers>
			<asp:AsyncPostBackTrigger ControlID="_ddlReportList" EventName="SelectedIndexChanged"  /> 
		</Triggers>
        <ContentTemplate>
            <table width="100%" cellpadding="2" cellspacing="2" border="0">
                <tr class="Header">
                    <td style="height: 17px; text-align: left">
                        <asp:Label ID="_lblCaption" runat="server" Text="<%$RIResources:Shared,ReportListing %>" SkinID="LabelWhite" EnableViewState="false"></asp:Label></td>
                </tr>
                <tr class="Border">
                    <td align="left">
                        <asp:Label ID="_lblReport" runat="server" Text="<%$RIResources:Shared,Report %>" EnableViewState="false"></asp:Label>&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="_ddlReportList" AutoPostBack="true" runat="server"
                            EnableViewState="true" Width=25%>
                        </asp:DropDownList>
                        <asp:ImageButton ImageUrl="~/Images/question.gif" OnClientClick="ShowMyModalPopup();return false;"
                            runat="server" ID="_imbHelp2" Visible="true" EnableViewState="false" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="_lblReportSortValue"
                            Visible="false" runat="server" Text="<%$RIResources:Shared,ReportSortValue %>" EnableViewState="true"></asp:Label>&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="_ddlReportSortValue" Visible="false" AutoPostBack="false"
                            runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
        <asp:Table ID="_tblOutageStatus" runat="server" CellPadding="2" CellSpacing="2" BackColor="white"
        Style="width: 100%;" EnableViewState="true" Visible="false">
        <asp:TableHeaderRow CssClass="Header">
            <asp:TableHeaderCell HorizontalAlign="left">
                <asp:Label ID="_lblOutageStatus" runat="server" EnableViewState="false" Text="<%$RIResources:OUTAGE,SDCategory %>"
                    SkinID="LabelWhite"></asp:Label>
            </asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow CssClass="Border">
            <asp:TableCell CssClass="Border">
                <asp:CheckBoxList ID="_cblSDCategory" runat="server" RepeatDirection="Horizontal" style="display:inline">
                    <asp:ListItem Text="<%$RIResources:Outage,AllOutages %>" Value="All" Selected="True">
                    </asp:ListItem>
                    <asp:ListItem Text="<%$RIResources:Outage,BlackMill %>" Value="Black Mill (No Power/Steam)">
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
            BackColor="white" Style="width: 98%" EnableViewState="false" Visible="false">
            <asp:TableHeaderRow CssClass="Header">
                <asp:TableHeaderCell ColumnSpan="3" HorizontalAlign="left">
                    <asp:Label ID="_lblSite" runat="server" Text="Site" SkinID="LabelWhite"></asp:Label>
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow CssClass="Border">
                <asp:TableCell ColumnSpan="3" Width="100%" Style="width: 100%">
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow  CssClass="Border"  >
            <asp:TableCell ColumnSpan="3" Width="100%" Style="width: 100%">
            </asp:TableCell></asp:TableRow>
        </asp:Table>

        <IP:SiteLocation ID="_ucSiteLocation" runat="server" Visible="false" HideLineBreak="true" />

    <asp:panel ID="_pnlCalendarSearch" runat="server">
      <IP:DateRange ID="_DateRange" runat="server" Visible="false" EnableViewState="true"  />
    </asp:panel>
    
    <%--<asp:panel ID="_pnlCalendarYear" runat="server" Visible="false">
        <asp:Label ID="_lblCalendarYear" runat="server" Text="Year" SkinID="LabelWhite"></asp:Label>
        <asp:DropDownList runat="server" ID="_ddlCalendarYear" EnableViewState="true"  >
            <asp:ListItem Text="2012"></asp:ListItem>
            <asp:ListItem Text="2013"></asp:ListItem>
        </asp:DropDownList>
    </asp:panel>--%>
    
 
     <asp:Table ID="_tblMain" BackColor="White" runat="server" CssClass="Main" CellPadding="2"
            CellSpacing="2" Width="98%" Visible="false">
        <asp:TableRow ID="_tr1" CssClass="Header" >
            <asp:TableCell Width="50%"></asp:TableCell><asp:TableCell Width="50%"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="_tr2" CssClass="Border"   >
                <asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="_tr3" CssClass="Header">
                <asp:TableHeaderCell></asp:TableHeaderCell><asp:TableHeaderCell></asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow ID="_tr4" CssClass="Border"  >
                <asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="_tr5" CssClass="Header" >
                <asp:TableHeaderCell></asp:TableHeaderCell><asp:TableHeaderCell></asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow ID="_tr6" CssClass="Border" >
                <asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="_tr7" CssClass="Header">
                <asp:TableHeaderCell></asp:TableHeaderCell><asp:TableHeaderCell></asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow ID="_tr8" CssClass="Border" >
                <asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="_tr9" CssClass="Border"  >
                <asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        
    <br />
        <asp:Panel ID="_pnlCoord" runat="server" Visible="false">
        <asp:Table ID="_tblMisc" runat="server" BorderWidth="0" CellPadding="2" CellSpacing="2"
            BackColor="white" Style="width: 98%" EnableViewState="true" >
            <asp:TableRow CssClass="Border" ><asp:TableCell >
            <asp:Label ID="_lblOutageCoord" runat="server" Text="<%$RIResources:Outage,OutageCoord %>" ></asp:Label>
                &nbsp;&nbsp;<asp:DropDownList runat="server" ID="_ddlOutageCoord" EnableViewState="true"  ></asp:DropDownList>
            </asp:TableCell>
            </asp:TableRow>
            </asp:Table>
        </asp:Panel>

        <asp:Panel ID="_pnlContractor" runat="server" Visible="false">
            <asp:Table ID="Table1" runat="server" BorderWidth="0" CellPadding="2" CellSpacing="2"
            BackColor="white" Style="width: 98%" EnableViewState="false" >
             <asp:TableRow CssClass="Border" ><asp:TableCell>
                <asp:Label ID="_lblContractor" runat="server" Text="<%$RIResources:Outage,Contractor %>"></asp:Label>
                &nbsp;&nbsp;<asp:DropDownList runat="server" ID="_ddlContractor" Visible="true">
                </asp:DropDownList>
            </asp:TableCell></asp:TableRow>
            </asp:Table>
        </asp:Panel>
        
        <asp:Panel ID="_pnlAnnual" runat="server" Visible="false">
        <asp:Table ID="Table2" runat="server" BorderWidth="0" CellPadding="2" CellSpacing="2"
            BackColor="white" Style="width: 98%" EnableViewState="false" >
             <asp:TableRow CssClass="Border"><asp:TableCell ColumnSpan="2" HorizontalAlign="left">
			    <asp:Label ID="_lbAnnualOutage" runat="server" EnableViewState="false" Text="<%$RIResources:OUTAGE,AnnualOutageOnly %>" ></asp:Label>
					&nbsp;<asp:CheckBox ID="_cbAnnualOutage" runat="server" ></asp:CheckBox>	
			</asp:TableCell></asp:TableRow>
        </asp:Table>
        </asp:Panel>
        
         <asp:Panel ID="_pnlOutageNumber" runat="server" Visible="false">
        <asp:Table ID="Table3" runat="server" BorderWidth="0" CellPadding="2" CellSpacing="2"
            BackColor="white" Style="width: 98%" EnableViewState="false" >
             <asp:TableRow CssClass="Border"><asp:TableCell ColumnSpan="2" HorizontalAlign="left">
			    <asp:Label ID="_lbOutageNumber" runat="server" EnableViewState="false" Text="<%$RIResources:OUTAGE,OutageNumber %>" ></asp:Label>
					&nbsp;<asp:TextBox ID="_tbOutageNumber" runat="server" ></asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="_feOutageNumber" runat="server" TargetControlID="_tbOutageNumber"
                    FilterType="custom" ValidChars="1234567890">
                </ajaxToolkit:FilteredTextBoxExtender>
						
			</asp:TableCell></asp:TableRow>
        </asp:Table>
        </asp:Panel>
        
<%--        <ajaxToolkit:CascadingDropDown ID="_cddlCoordinator" runat="server" Category="Leader"
            LoadingText="[Loading Coordinator...]" PromptText="   " ServiceMethod="GetOutageCoord"
            ServicePath="~/CascadingLists.asmx" TargetControlID="_ddlOutageCoord" ParentControlID="ctl00__cphMain__Outage_Reporting__ucsiteLocation__ddlFacility">
        </ajaxToolkit:CascadingDropDown>                                    
--%>        
        <asp:Panel ID="_pnlButtons" runat="server" Visible="False">
            <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                <tr>
                <td align="center">
                    <asp:Button Text="<%$RIResources:ButtonText,SubmitReport %>" ID="_btnSubmit" runat="server" />
                    <asp:Button Text="<%$RIResources:ButtonText,ResetCriteria %>" runat="server" ID="_btnReset" />
                </td>
                </tr>
            </table>
        </asp:Panel>
   
        <asp:Table ID="_tblTaskSearch" runat="server" CellPadding="4" CellSpacing="1" BackColor="white"
            BorderWidth="1" Width="100%" Visible="true">
        </asp:Table>

        </ContentTemplate>
    </Asp:UpdatePanel>
            

</asp:Content>
