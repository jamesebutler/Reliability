<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="ViewUpdateSearch.aspx.vb"
    Inherits="RI_ViewUpdateSearch" EnableTheming="true" StylesheetTheme="RIBlackGold"
    Theme="RIBlackGold" EnableEventValidation="false" Title="International Paper: Reliability Reporting"
    Trace="false" MaintainScrollPositionOnPostback="true" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Table ID="_tblbutton1" runat="server">
                    <asp:TableRow>
                        <asp:TableCell Width="40%">
                        </asp:TableCell>
                        <asp:TableCell Width="60%">
                            <asp:Button ID="_btnViewUpdate1" OnClientClick="showIncidentListing();" OnClick="_btnViewUpdate_Click"
                                Text="<%$RIResources:Shared,ViewUpdate%>" runat="server" />
                            <asp:Button ID="_btnExcel1" Text="<%$RIResources:Shared,Excel%>" runat="server" />
                        </asp:TableCell></asp:TableRow>
                </asp:Table>
                <IP:SiteLocation ID="_siteLocation" runat="server" />
                <ajaxToolkit:CascadingDropDown ID="_cddlCrew" runat="server" Category="Trigger" LoadingText="[Loading Crew...]"
                    PromptText="   " ServiceMethod="GetCrew" ServicePath="~/CascadingLists.asmx"
                    TargetControlID="_ddlCrew" ParentControlID="ctl00__cphMain__siteLocation__ddlFacility">
                </ajaxToolkit:CascadingDropDown>
                <ajaxToolkit:CascadingDropDown ID="_cddlShift" runat="server" Category="Trigger"
                    LoadingText="[Loading Shift...]" PromptText="   " ServiceMethod="GetShift" ServicePath="~/CascadingLists.asmx"
                    TargetControlID="_ddlShift" ParentControlID="ctl00__cphMain__siteLocation__ddlFacility">
                </ajaxToolkit:CascadingDropDown>
                <ajaxToolkit:CascadingDropDown ID="_cddlRCFALeader" runat="server" Category="Leader"
                    LoadingText="[Loading RCFA Leader...]" PromptText="   " ServiceMethod="GetPerson"
                    ServicePath="~/CascadingLists.asmx" TargetControlID="_ddlRCFALeader" ParentControlID="ctl00__cphMain__siteLocation__ddlFacility">
                </ajaxToolkit:CascadingDropDown>
                <ajaxToolkit:CascadingDropDown ID="_cddlTrigger" runat="server" Category="Trigger"
                    LoadingText="[Loading Trigger...]" PromptText="   " ServiceMethod="GetTrigger" UseContextKey="true"
                    ServicePath="~/CascadingLists.asmx" TargetControlID="_ddlTrigger" ParentControlID="ctl00__cphMain__siteLocation__ddlFacility">
                </ajaxToolkit:CascadingDropDown>
                <%-- <IP:DateRange ID="_DateRange" runat="server" /> --%>
                <table width="100%" class="Border">
                    <tr>
                        <td width="60%">
                            <IP:DateRange ID="_DateRange" runat="server" />
                        </td>
                        <td width="20%">
                            <asp:Label ID="_lblFLCriticality" runat="server" Text='<%$ RIResources:Shared,Criticality %>'></asp:Label>
                            <asp:DropDownList ID="_ddlCriticality" runat="server">
                                <asp:ListItem Text="" Value="" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            </asp:DropDownList></td>
                             <td width="20%">                        
                            <asp:Label ID="_lblVerification" runat="server" Text='<%$ RIResources:Shared,Verification %>'></asp:Label>
                            <asp:Checkbox  ID="_cbVerification" runat="server"  ></asp:Checkbox>
                            </td>
                       
                   </tr>
                </table>
                        <%--<td>
                            <asp:Label ID="_lblConstrainedAreas" runat="server" Text="<%$RIResources:Shared,Constrained Areas %>"></asp:Label></td>
                        <td>
                         <asp:CheckBox ID="_cbConstrainedAreas" runat="server"   >
                                </asp:CheckBox>--%>
                           
                           <%-- <asp:CheckBoxList ID="_cbConstrainedAreas" runat="server" RepeatColumns="2" RepeatLayout="table"
                                RepeatDirection="Horizontal" onclick="unCheckNo(this,2);">
                                <asp:ListItem Text="<%$RIResources:Yes%>" Value="YES"></asp:ListItem>
                                <asp:ListItem Text="<%$RIResources:No %>" Value="No"></asp:ListItem></asp:CheckBoxList>--%>
                       
                    
    <IP:IncidentType ID="_IncidentType" runat="server" DisplayMode="Search" SearchMode="ANDStatement" />
    <asp:Table ID="_tblRCFAStatus" runat="server" CellPadding="2" CellSpacing="2" BackColor="white"
        Style="width: 98%" EnableViewState="true">
        <asp:TableHeaderRow CssClass="Header">
            <asp:TableHeaderCell Width="60%" HorizontalAlign="left">
                <asp:Label ID="_lblRCFAStatus" runat="server" EnableViewState="false" Text="<%$RIResources:Shared,RCFA Status%>"
                    SkinID="LabelWhite"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableHeaderCell Width="40%" HorizontalAlign="left">
                <%--<asp:Label ID="_lblActionDue" runat="server" EnableViewState="false" Text="<%$RIResources:Shared,Action Due%>"
                    SkinID="LabelWhite"></asp:Label>--%>
            </asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow CssClass="Border">
            <asp:TableCell CssClass="Border">
                <asp:CheckBoxList ID="_cblRCFAStatus" runat="server" RepeatDirection="Vertical" RepeatColumns="3">
                    <asp:ListItem Text="<%$RIResources:All%>" Value="All">
                    </asp:ListItem>
                    <asp:ListItem Text="<%$RIResources:Unassigned%>" Value="Unassigned">
                    </asp:ListItem>
                    <asp:ListItem Text="<%$RIResources:Open%>" Value="Open">
                    </asp:ListItem>
                    <asp:ListItem Text="<%$RIResources:Analysis in Progress%>" Value="Analysis in Progress">
                    </asp:ListItem>
                    <asp:ListItem Text="<%$RIResources:Analysis Complete%>" Value="Analysis Complete">
                    </asp:ListItem>
                    <asp:ListItem Text="<%$RIResources:Analysis & Actions Complete%>" Value="Analysis & Actions Complete">
                    </asp:ListItem>
                </asp:CheckBoxList>
            </asp:TableCell>
            <asp:TableCell CssClass="Border">
                <%--<asp:CheckBoxList ID="_cblActionDue" runat="server" RepeatDirection="Vertical" RepeatColumns="3">
                    <asp:ListItem Text="<%$RIResources:All%>" Value="All">
                    </asp:ListItem>
                    <asp:ListItem Text="<%$RIResources:Overdue%>" Value="Overdue">
                    </asp:ListItem>
                    <asp:ListItem Text="<%$RIResources:Next 7 Days%>" Value="Next 7 Days">
                    </asp:ListItem>
                    <asp:ListItem Text="<%$RIResources:Next 14 Days%>" Value="Next 14 Days">
                    </asp:ListItem>
                    <asp:ListItem Text="<%$RIResources:Next 30 Days%>" Value="Next 30 Days">
                    </asp:ListItem>--%>
                </asp:CheckBoxList>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:Table ID="Table6" runat="server" CellPadding="0" CellSpacing="2" BackColor="white"
        EnableViewState="true">
        <asp:TableHeaderRow CssClass="Header">
            <asp:TableHeaderCell ColumnSpan="5" HorizontalAlign="left">
                <asp:Label ID="_lblIncidentClassificationHeader" runat="server" Text="<%$RIResources:Shared,Incident Classification%>"
                    SkinID="LabelWhite" EnableViewState="false"></asp:Label>
            </asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow CssClass="Border">
            <asp:TableCell ColumnSpan="5">
                <asp:Label ID="_lblTrigger" runat="server" Text='<%$RIResources:Shared,Trigger%>'
                    EnableViewState="false" />&nbsp;
                <asp:DropDownList ID="_ddlTrigger" runat="server" Width="80%"  style="max-width:600px">
                </asp:DropDownList></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <IP:IncidentClassification2 runat="server" ID="_IncidentClassification" AutoPostBack="false" />
    <asp:Table ID="_tblRootCauses" runat="server" CellPadding="2" CellSpacing="2" BackColor="white"
        Style="width: 100%" EnableViewState="true">
        <%--<asp:TableHeaderRow CssClass="Header">
            <asp:TableHeaderCell ColumnSpan="3" HorizontalAlign="left">
                <asp:Label ID="_lblRootCauses" runat="server" EnableViewState="false" Text="<%$ Resources:Shared,lblRootCauses %>"
                    SkinID="LabelWhite"></asp:Label>
            </asp:TableHeaderCell>
        </asp:TableHeaderRow>--%>
        <asp:TableRow CssClass="Border" VerticalAlign="top">
            <asp:TableCell Width="40%">
                <asp:Label ID="_lblPhysicalCauses" runat="server" Text="<%$ RIResources:Shared,Physical Causes %>">
                </asp:Label><br />
                <asp:DropDownList ID="_ddlPhysicalCauses" runat="server">
                </asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell Width="30%">
                <asp:Label ID="_lblLatentCauses" runat="server" Text="<%$ RIResources:Shared,Latent Causes %>"
                    EnableViewState="false"></asp:Label><br />
                <asp:DropDownList ID="_ddlLatentCauses" runat="server">
                </asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell Width="30%">
                <asp:Label ID="_lblHumanCauses" runat="server" Text="<%$ RIResources:Shared,Human Causes %>"
                    EnableViewState="false"></asp:Label><br />
                <asp:DropDownList ID="_ddlHumanCauses" runat="server">
                </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <%--</asp:Table>
                <asp:Table ID="_tblMiscellaneous" runat="server" CellPadding="2" CellSpacing="2"
                    BackColor="white" Style="width: 100%" EnableViewState="true">--%>
        <asp:TableRow CssClass="Border">
            <asp:TableCell Width="40%">
                <asp:Label ID="_lblRINumber" runat="server" Text="<%$ RIResources:Shared,RINumber %>">
                </asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox runat="server" ID="_txtRINumber" MaxLength="10"></asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="_feRINumber" runat="server" TargetControlID="_txtRINumber"
                    FilterType="custom" ValidChars="1234567890">
                </ajaxToolkit:FilteredTextBoxExtender>
            </asp:TableCell>
            <asp:TableCell Width="30%">
                <asp:Label ID="_lblCrew" runat="server" Text="<%$ RIResources:Shared,Crew %>"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList runat="server" ID="_ddlCrew">
                </asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell Width="30%">
                <asp:Label ID="_lblRCFAActionLeader" runat="server" Text="<%$ RIResources:Shared,Analysis Lead %>"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList runat="server" ID="_ddlRCFALeader">
                </asp:DropDownList></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow CssClass="Border">
            <asp:TableCell Width="40%">
                <asp:Label ID="_lblTitleSearch" runat="server" Text="<%$ RIResources:Shared,Title Search %>"></asp:Label>
                &nbsp;&nbsp;<asp:TextBox runat="server" ID="_txtTitleSearch"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Width="30%">
                <asp:Label ID="_lblShift" runat="server" Text="<%$ RIResources:Shared,Shift %>"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList runat="server" ID="_ddlShift">
                </asp:DropDownList></asp:TableCell>
            <asp:TableCell Width="30%">
                <asp:Label ID="_lblFinancialImpact" runat="server" Text="<%$ RIResources:Shared,Financial Impact %>"></asp:Label>
                &nbsp;&nbsp;<asp:TextBox runat="server" ID="_txtFinancialImpact"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <Asp:UpdatePanel ID="_upViewScreen" runat="server" EnableViewState="true" UpdateMode="Always">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="_btnExcel1" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="_btnViewUpdate1" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="_btnOK" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <asp:Table runat="server">
                <asp:TableRow>
                    <asp:TableCell Width="40%">&nbsp;					
                    </asp:TableCell>
                    <asp:TableCell Width="60%">
                        <asp:Button ID="_btnViewUpdate" OnClientClick="showIncidentListing();" Text="<%$RIResources:Shared,ViewUpdate%>"
                            runat="server" />
                        <asp:Button ID="_btnExcel" Text="<%$RIResources:Shared,Excel%>" runat="server" />
                    </asp:TableCell></asp:TableRow>
            </asp:Table>
            <ajaxToolkit:CollapsiblePanelExtender ID="_cpeIncidentListing" runat="server" CollapseControlID="_imgIncidentListing"
                ExpandControlID="_imgIncidentListing" BehaviorID="bhIncidentListing" TargetControlID="_pnlIncidentListing">
            </ajaxToolkit:CollapsiblePanelExtender>
            <div style="visibility: hidden; display: none">
                <asp:Image ID="_imgIncidentListing" BorderWidth="4" runat="server" ImageUrl="~/Images/blank.gif" />
            </div>
            <asp:Panel ID="_pnlIncidentListing" runat="server" HorizontalAlign="center">
                <div style="text-align: left">
                    <asp:Label ID="_lblRecordCount" runat="server" Style="text-align: left" Text="<%$RIResources:Shared,RecordCount%>"
                        BackColor="Black" ForeColor="White"></asp:Label>
                    <asp:Label ID="_lblRecCount" runat="server" Style="text-align: left" Text="0" BackColor="Black"
                        ForeColor="White"></asp:Label></div>
                <br />
                <asp:GridView Width="100%" CssClass="Border" BorderColor="Black" BorderWidth="2"
                    ID="_gvIncidentListing" runat="server" AutoGenerateColumns="False" DataKeyNames="RINUMBER"
                    EnableViewState="false" AllowSorting="true" ShowFooter="true" EnableSortingAndPagingCallbacks="false"
                    Style="table-layout: fixed">
                    <Columns>
                        <%--<asp:BoundField DataField="EVENTDATE" HeaderText="<%$RIResources:Shared,EventDate%>" SortExpression="EVENTDATE"
							DataFormatString="{0:d}" HtmlEncode="false" />--%>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="<%$RIResources:Shared,EventDate %>"
                            SortExpression="EVENTDATE">
                            <ItemTemplate>
                                <asp:Literal ID="Literal1" runat="server" Text='<%#Convert.ToDateTime(Eval("EVENTDATE")).ToShortDateString( )%>'></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="SITENAME" HeaderText="<%$RIResources:Shared,Site%>"
                            SortExpression="SITENAME" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="RISUPERAREA" HeaderText="<%$RIResources:Shared,BusinessUnit%>"
                            SortExpression="RISUPERAREA" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="SUBAREA" HeaderText="<%$RIResources:Shared,Area%>"
                            SortExpression="SUBAREA" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="AREA" HeaderText="<%$RIResources:Shared,Line%>"
                            SortExpression="AREA" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="RINUMBER" HeaderText="<%$RIResources:Shared,RINumber%>"
                            SortExpression="RINUMBER" />
                        <asp:HyperLinkField ItemStyle-HorizontalAlign="Left" DataTextField="INCIDENT" HeaderText="<%$RIResources:Shared,IncidentTitle%>"
                            SortExpression="INCIDENT" DataNavigateUrlFields="RINUMBER" DataNavigateUrlFormatString="~/RI/EnterNewRI.aspx?RINumber={0}"
                            Target="_self" />
                        <asp:TemplateField HeaderText="<%$RIResources:Shared,TypeExecutiveSummary%>" SortExpression="RCFA_TYPE">
                            <ItemTemplate>
                                <a target="_blank" href="<%#String.Format(Page.ResolveClientUrl("http://gpimv.graphicpkg.com/cereporting/CrystalReportDisplay.aspx?Report=ExecutiveSummary&RINumber={0}&LocaleName={1}"), Eval("RINumber"), System.Threading.Thread.CurrentThread.CurrentCulture) %>">
                                    <%#Eval("RCFA_TYPE")%>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:HyperLinkField ItemStyle-HorizontalAlign="Left" DataTextField="RCFA_TYPE" HeaderText="<%$RIResources:Shared,TypeExecutiveSummary%>"
                                        SortExpression="RCFA_TYPE" DataNavigateUrlFields="RINUMBER" DataNavigateUrlFormatString="../../CEReporting/frmCrystalReport.aspx?Report=ExecutiveSummary&RINumber={0}"
                                        Target="_blank" />--%>
                        <%--            <asp:HyperLinkField DataTextField="RCFA_TYPE" HeaderText="Type" SortExpression="RCFA_TYPE" DataNavigateUrlFields="RINUMBER"   DataNavigateUrlFormatString="../../CEReporting/frmCrystalReport.aspx?Report=ExecutiveSummary?RINumber={0}"  Target="_blank" />
--%>
                        <%--            <asp:BoundField DataField="RCFA_TYPE" HeaderText="Type" SortExpression="RCFA_TYPE" HtmlEncode="true"  />
--%>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderText="<%$RIResources:Shared,Cost %>"
                            SortExpression="COST">
                            <ItemTemplate>
                                <asp:Literal ID="_Cost" runat="server" Text="0"></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderText="<%$RIResources:Shared,Financial Impact %>"
                            SortExpression="TOTCOST">
                            <ItemTemplate>
                                <asp:Literal ID="_TotCost" runat="server" Text='0'></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                </asp:GridView>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="_mpeExcelSelect" runat="server" TargetControlID="_btnExcel"
                PopupControlID="_pnlExcelSelect" BackgroundCssClass="modalBackground" DropShadow="true"
                OkControlID="_btnCancel" CancelControlID="_btnCancel">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="_pnlExcelSelect" runat="server" CssClass="modalPopup" Width="800"
                Style="display: none;">
                <IP:SwapListBox ID="_sblSelectColumns" LocalizeData="true" runat="server" />
                <p style="text-align: center;">
                    <asp:Button ID="_btnOK" OnClientClick="HideExcelSelect();" runat="server" Text="<%$RIResources:Shared,ViewExcel%>">
                    </asp:Button>
                    <asp:Button ID="_btnCancel" runat="server" Text="<%$RIResources:Shared,Cancel%>"></asp:Button>
                </p>
            </asp:Panel>
        </ContentTemplate>
    </Asp:UpdatePanel>
    </td> </tr> </table>
</asp:Content>
