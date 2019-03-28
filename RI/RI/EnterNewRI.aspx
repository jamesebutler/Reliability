<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="EnterNewRI.aspx.vb"
    Inherits="RI_EnterNewRI" Title="RI:Enter New RI" Trace="false" EnableViewState="true"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" EnableViewStateMac="false" %>

<%@ Register Src="~/RI/User Controls/ucFailureClassification.ascx" TagName="ucFailureClassification"
    TagPrefix="FailureClassification" %>
<%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="_contentMain" ContentPlaceHolderID="_cphMain" runat="Server">
    <karamasoft:ultimatespell id="_spellhidden" runat="server" showaddbutton="false"
        showspellbutton="false" ignoredisabledcontrols="true" showmodal="true" showoptions="true"
        enabletheming="false" spellasyoutype="false">
    </karamasoft:ultimatespell>
    <Asp:UpdatePanel id="_udpLocation" runat="server" updatemode="Conditional">
        <ContentTemplate>
            <ajaxToolkit:CascadingDropDown ID="_cddlFacility" runat="server" Category="SiteID"
                LoadingText="..." PromptText="    "
                ServiceMethod="GetFacilityList" ServicePath="~/CascadingLists.asmx"
                TargetControlID="_ddlFacility" UseContextKey="true">
            </ajaxToolkit:CascadingDropDown>
            <ajaxToolkit:CascadingDropDown ID="_cddlBusArea" runat="server" Category="BusArea"
                LoadingText="" PromptText="    "
                ServiceMethod="GetBusArea" ServicePath="~/CascadingLists.asmx"
                TargetControlID="_ddlBusinessUnit" ParentControlID="_ddlFacility">
            </ajaxToolkit:CascadingDropDown>
            <ajaxToolkit:CascadingDropDown ID="_cddlLineBreak" runat="server" Category="LineBreak"
                LoadingText="" PromptText="    "
                ServiceMethod="GetLineBreak" ServicePath="~/CascadingLists.asmx"
                TargetControlID="_ddlLineBreak" ParentControlID="_ddlBusinessUnit">
            </ajaxToolkit:CascadingDropDown>
            <ajaxToolkit:CascadingDropDown ID="_cddlAnalysisLead" runat="server" Category="Leader"
                LoadingText="" PromptText="    " 
                ServiceMethod="GetAnalysisLeader" ServicePath="~/CascadingLists.asmx"
                UseContextKey="true" TargetControlID="_ddlAnalysisLead" ParentControlID="_ddlBusinessUnit">
            </ajaxToolkit:CascadingDropDown>
            <ajaxToolkit:CascadingDropDown ID="_cddlTrigger" runat="server" Category="Trigger"
                LoadingText="" PromptText="    " UseContextKey="true"
                 ServiceMethod="GetTrigger" ServicePath="~/CascadingLists.asmx"
                TargetControlID="_ddlTrigger" ParentControlID="_ddlBusinessUnit">
            </ajaxToolkit:CascadingDropDown>
            <ajaxToolkit:CascadingDropDown ID="_cddlCrew" runat="server" Category="Trigger" LoadingText=""
                PromptText="    "          ServiceMethod="GetCrew" ServicePath="~/CascadingLists.asmx"
                TargetControlID="_ddlCrew" ParentControlID="_ddlFacility">
            </ajaxToolkit:CascadingDropDown>
            <ajaxToolkit:CascadingDropDown ID="_cddlShift" runat="server" Category="Trigger"
                LoadingText="" PromptText="    "    ServiceMethod="GetShift" ServicePath="~/CascadingLists.asmx"
                TargetControlID="_ddlShift" ParentControlID="_ddlFacility">
            </ajaxToolkit:CascadingDropDown>
            <ajaxToolkit:CascadingDropDown ID="_cddlFailedMaterial" runat="server" Category="FailedLocation"
                LoadingText="" PromptText="    " 
                 ServiceMethod="GetFailedMaterial" ServicePath="~/CascadingLists.asmx"
                TargetControlID="_ddlFailedMaterial" ParentControlID="_ddlFacility">
            </ajaxToolkit:CascadingDropDown>
            <ajaxToolkit:CascadingDropDown ID="_cddlPerson" runat="server" Category="Siteid"
				LoadingText="[Loading People...]" PromptText="   " ServiceMethod="GetEmployee"
				ServicePath="~/CascadingLists.asmx" TargetControlID="_ddlPerson" ParentControlID="_ddlFacility">
			</ajaxToolkit:CascadingDropDown>
            <asp:Button ID="_btnViewSearch" runat="server" Text="<%$RIResources:Shared,SwitchRI %>"
                Visible="false" />
            <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" >
                <tr class="Border">
                    <td style="width: 33%">
                        <asp:Label ID="Label1" runat="server" Text='<%$RIResources:Shared,Analysis Lead %>'
                            EnableViewState="false" />&nbsp;&nbsp;
                            <asp:DropDownList ID="_ddlAnalysisLead" Width="200"
                                runat="server" onchange="onChangeAnalysisLeader(this);self.focus();"  >
                            </asp:DropDownList>
                            
                        <asp:TextBox ReadOnly="true" BackColor="lightgray" runat="server" ID="_txtAnalysisLead"
                            Width="200" Visible="false"></asp:TextBox>
                    </td>
                    <td style="width: 33%">
                        <asp:Label ID="Label3" runat="server" Text='<%$RIResources:Shared,Analysis Completed %>'
                            EnableViewState="false" />&nbsp;&nbsp;<asp:TextBox ID="_txtAnalysisCompleted" runat="server"
                                Width="100" ReadOnly="true" Enabled="true" BackColor="lightGray"></asp:TextBox>
                    </td>
                    <td style="width: 33%">
                        <Asp:UpdatePanel ID="_udpActions" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%$RIResources:Shared,Actions Completed %>'
                                    EnableViewState="false" />&nbsp;&nbsp;<asp:TextBox ID="_txtCorrectiveActionsCompleted"
                                        runat="server" Width="100" ReadOnly="true" Enabled="true" BackColor="lightGray"></asp:TextBox>
                            </ContentTemplate>
                        </Asp:UpdatePanel>
                    </td>
                </tr>
                <tr class="Border" runat="server" id="_locationRow">
                    <td >
                        <span class="ValidationError">*</span><asp:Label ID="_lblFacility" runat="server"
                            Text='<%$ RIResources:Shared,Facility %>'></asp:Label><br />
                        <asp:DropDownList ID="_ddlFacility" onchange="updateFunctionalLocation();updateSecurity();updateAnalysisLeader();self.focus();"
                            CausesValidation="true" runat="server" AutoPostBack="false" Width="90%"  >
                        </asp:DropDownList>
                        
                        <asp:RequiredFieldValidator ValidationGroup="EnterNewRI" ID="_rfvFacility" runat="server"
                            Display="none" ControlToValidate="_ddlFacility" ErrorMessage="<%$ RIResources:Shared,SelectFacility %>"
                            EnableClientScript="true" SetFocusOnError="true" Text="<%$ RIResources:Shared,SelectFacility %>"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <span class="ValidationError">*</span><asp:Label ID="Label2" runat="server" Text='<%$ RIResources:Shared,busarea %>'></asp:Label><br />
                        <asp:DropDownList ID="_ddlBusinessUnit" CausesValidation="true" AutoPostBack="false"
                            EnableViewState="false" Width="90%" onchange="updateFunctionalLocation();updateConstrainedArea();updateSecurity();updateAnalysisLeader();self.focus();"
                            Visible="true" runat="server"  />
                        <asp:RequiredFieldValidator ValidationGroup="EnterNewRI" ID="_rfvBusinessUnit" runat="server"
                            Display="none" ControlToValidate="_ddlBusinessUnit" ErrorMessage="<%$ RIResources:Shared,SelectBusinessUnit %>"
                            EnableClientScript="true" SetFocusOnError="true" Text="<%$ RIResources:Shared,SelectBusinessUnit %>"></asp:RequiredFieldValidator></td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text='<%$ RIResources:Shared,LineLineBreak %>'></asp:Label><br />
                        <asp:DropDownList ID="_ddlLineBreak" CausesValidation="true" AutoPostBack="false" 
                            Width="98%" runat="server" onchange="updateFunctionalLocation();updateConstrainedArea();updateSecurity();updateAnalysisLeader();self.focus();" />
                    </td>
                </tr>
                <%-- <tr class="Border">
                    <td colspan="2">
                        <IP:FunctionalLocation ID="_functionalLocationTree" runat="server" />
                    </td>
                    <td><asp:Label ID="_lblConstrainedAreas" runat="server" Text="<%$RIResources:Shared, Constrained Areas%>"> %>"</asp:Label>
                        <asp:RadioButtonList ID="_rblConstrainedAreas" RepeatLayout="Table" RepeatDirection="Horizontal" runat="server">
                             <asp:ListItem Text="<%$RIResources:Shared, Yes %>" Value="Yes"></asp:ListItem>
                             <asp:ListItem Text="<%$ RiResources:Shared,No%>" Value="No"></asp:ListItem>
                            </asp:RadioButtonList>        
                    </td>
                </tr>--%>
                <tr class="Border">
                    <td style="width: 100%" colspan="3">
                        <div style="float: left">
                            <IP:FunctionalLocation ID="_functionalLocationTree" runat="server" />
                        </div>
                        <div style="float: left; margin-left: 10px; margin-top: 5px;">
                            <table>
                                <tr>
                                   
                                    <td>
                                        <asp:Label ID="_lblFLCriticality" runat="server" Text='<%$ RIResources:Shared,Criticality %>'></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="_ddlCriticality" runat="server" onchange="SetClassificationCriticality(this.value);">
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
                                    <%--<td style="width: 60px;">
                                        &nbsp;</td>--%>
                                    <%--<td>
                                        <asp:Label ID="_lblConstrainedAreas" runat="server" Text='<%$ RIResources:Shared,Constrained Areas %>'></asp:Label></td>
                                    <td>
                                        <asp:RadioButtonList ID="_rblConstrainedAreas" RepeatLayout="Table" RepeatDirection="Horizontal" Enabled="False"
                                            runat="server" onclick="SetClassificationConstrainedArea(GetConstrainedArea());">
                                            <asp:ListItem Text="<%$ RIResources:Shared,Yes %>" Value="Yes"></asp:ListItem>
                                            <asp:ListItem Text="<%$ RIResources:Shared,No %>" Value="No" Selected="true"></asp:ListItem>
                                        </asp:RadioButtonList></td>--%>
                                </tr>
                            </table>
                        </div>
                    </td>
 <%--                   <td style="width:33%; white-space:nowrap" >
 --%>                   <%--<asp:Label ID="_lblCrew" runat="server" Text='<%$ RIResources:Shared,Crew %>' EnableViewState="false" />&nbsp;<asp:DropDownList
                            ID="_ddlCrew" runat="server" Width="80"  >
                        </asp:DropDownList>&nbsp;&nbsp;
                        <asp:Label ID="_lblShift" runat="server" Text='<%$ RIResources:Shared,Shift %>' EnableViewState="false" />&nbsp;<asp:DropDownList
                            ID="_ddlShift" runat="server" Width="80"  >
                        </asp:DropDownList>--%>
                    
                       <%--<table>
                       <tr>
                       <td>
                        <asp:Label ID="_lblVerification" Text='<%$ RIResources:Shared,Verification %>' runat="server" />
                        <asp:RadioButtonList ID="_rblVerification"  TextAlign="Left" RepeatLayout="table" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" >
                             <asp:ListItem Text='<%$ RIResources:Shared,Yes %>' Value="Yes"></asp:ListItem>
                             <asp:ListItem Text='<%$ RIResources:Shared,No %>' Value="No"></asp:ListItem>
                         </asp:RadioButtonList></td>
                       <td  valign="top"> <asp:Label ID="_lblPerson" Text='<%$ RIResources:Shared,Responsible Person %>' runat="server" />
                        <asp:DropDownList ID="_ddlPerson"  Enabled="false" runat="server" ></asp:DropDownList></td>
                        <td valign="top">
                        <asp:Label ID="_lblWeeksAfter" Text='<%$ RIResources:Shared,Weeks After %>' runat="server" />
                        <asp:TextBox ID="_tbWeeksAfter" Width="50" Enabled="false" runat="server" ></asp:TextBox>
                        </td>
                        </tr>
                       </table> --%>
                
                </tr>
                <tr class="Border">
                    <td style="width: 66%" colspan="2">
                        <IP:StartEndCalendar ShowTime="true" ID="_startEndCal" runat="server" />
                    </td>
                    <td style="width: 33%">
                        <asp:Label ID="_lblDowntime" runat="server" Text='<%$ RIResources:Shared,Downtime %>'
                            EnableViewState="false" />&nbsp;<asp:TextBox ID="_txtDownTime" Width="50" runat="server" onchange="updateSRRDowntime(this.value);" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                            TargetControlID="_txtDownTime" FilterType="custom" ValidChars="1234567890">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:Button ID="_btnCalculateDowntime" Visible="true" OnClientClick="calculateDowntime();return false;"
                            runat="server" Text="<%$ RIResources:Shared,Calculate %>" /><br />
                        <asp:RegularExpressionValidator ID="_revDowntime" runat="server" SetFocusOnError="true"
                            ControlToValidate="_txtDownTime" ValidationGroup="EnterNewRI" EnableClientScript="true"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr class="Border">
                    <td colspan="2">
                        <span class="ValidationError">*</span><asp:Label ID="_lblIncidentTitle" runat="server"
                            Text='<%$ RIResources:Shared,IncidentTitle %>' EnableViewState="false" />&nbsp;<br />
                        <%--                        <asp:TextBox ID="_txtIncidentTitle" runat="server" MaxLength="200" Width="800"></asp:TextBox>
--%>
                        <IP:AdvancedTextBox SkinID="Translate" TextLink="<%$ RIResources:Shared,Translate %>"
                            ID="_txtIncidentTitle" runat="server" TextMode="multiLine" Rows="1" ExpandHeight="true"
                            Width="90%" MaxLength="200" />
                    <asp:RequiredFieldValidator ValidationGroup="EnterNewRI" ID="_rfvIncidentTitle" runat="server"
                        Display="none" ControlToValidate="_txtIncidentTitle" ErrorMessage="<%$ RIResources:Shared,EnterTitle %>"
                        EnableClientScript="true" SetFocusOnError="true" Text="<%$ RIResources:Shared,EnterTitle %>"></asp:RequiredFieldValidator></td>
                    <td>
                    
                    <asp:Label ID="_lblCrew" runat="server" Text='<%$ RIResources:Shared,Crew %>' EnableViewState="false" />&nbsp;<asp:DropDownList
                            ID="_ddlCrew" runat="server" Width="80"  >
                        </asp:DropDownList>&nbsp;&nbsp;
                        <asp:Label ID="_lblShift" runat="server" Text='<%$ RIResources:Shared,Shift %>' EnableViewState="false" />&nbsp;<asp:DropDownList
                            ID="_ddlShift" runat="server" Width="80"  >
                        </asp:DropDownList>
                    
                    
                        </td>
                </tr>
                <tr class="Border">
                    <td colspan="3">
                        <asp:Label ID="_lblDescription" runat="server" Text='<%$ RIResources:Shared,Description %>'
                            EnableViewState="false" /><br />
                        <div>
                            <IP:AdvancedTextBox SkinID="Translate" TextLink="<%$ RIResources:Shared,Translate %>"
                                ID="_txtIncidentDescription" runat="server" ExpandHeight="true" Rows="2" TextMode="MultiLine"
                                MaxLength="4000" Width="98%" Style="width: 98%" /></div>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                <tr class="Border">
                    <td>
                        <asp:Label ID="lblCost" runat="server" Text='<%$RIResources:Shared,Cost %>' EnableViewState="false" />
                        <asp:HyperLink ID="_hypRepairCostDefinition" CssClass="LabelLink" runat="server"
                            Text='<%$RIResources:Shared,Definition %>'></asp:HyperLink><br />
                        <b>US$</b><asp:TextBox ID="_txtCost" runat="server" MaxLength="10" Width="100"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="_feCost" runat="server" TargetControlID="_txtCost"
                            FilterType="custom" ValidChars="1234567890">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:Label ID="_lblFinancialImpact" runat="server" Text='<%$RIResources:Shared,Financial Impact %>'
                            EnableViewState="false" />
                        <asp:HyperLink ID="_hypFinancialImpactDefinition" runat="server" Text='<%$RIResources:Shared,Definition %>'
                            CssClass="LabelLink"></asp:HyperLink><br />
                        <b>US$</b><asp:TextBox ID="_txtFinancialImpact" MaxLength="10" runat="server" Width="100" OnChange="updateSRRFINCL(this.value);" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                            TargetControlID="_txtFinancialImpact" FilterType="custom" ValidChars="1234567890">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td style="width: 10px">
                    </td>
                    <td style="width: 1px; ">
                    </td>
                    <td style="width: 10px">
                    </td>
                    <td>
                        <asp:Label ID="_lblAnnualizedSavings" runat="server" Text='<%$RIResources:Shared,Annualized Savings %>'></asp:Label><br />
                        <b>US$</b><asp:TextBox ID="_txtAnnualizedSavings" runat="server" MaxLength="10" Width="100"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="_ftbAnnualizedSavings" runat="server" TargetControlID="_txtAnnualizedSavings"
                            FilterType="custom" ValidChars="1234567890">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:Label ID="_lblCapital" runat="server" Text='<%$RIResources:Shared,Capital %>'
                            EnableViewState="false" /><br />
                        <asp:DropDownList ID="_ddlCapital" Width="100" runat="server">
                        </asp:DropDownList></td>
                    <td>
                        <asp:Label ID="_lblCostofSolution" runat="server" Text='<%$RIResources:Shared,Cost of Solution %>'
                            EnableViewState="false" /><br />
                        <b>US$</b><asp:TextBox ID="_txtCostofSolution" MaxLength="10" runat="server" Width="100"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="_febCostofSolution" runat="server" TargetControlID="_txtCostofSolution"
                            FilterType="Numbers">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
            </table>
            <Asp:UpdatePanel ID="_updincidenttype" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <IP:IncidentType ID="_incidentType" runat="server" DisplayMode="Enter" />
                </ContentTemplate>
            </Asp:UpdatePanel>
            <FailureClassification:ucFailureClassification ID="_FailureClassification" runat="server" />                            
            <%--<table border="0" width="100%" cellpadding="2" cellspacing="1">
                <tr class="Border">
                    <td>
                        <asp:Label ID="_lblFailureClassHeader" runat="server" SkinID="LabelButton" Width="350"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="_lblAvgFailureClass" runat="server" Text="<%$RIResources: Shared,TotalClassification %>"></asp:Label>&nbsp;
                        <asp:TextBox ID="_txtFailureClass" BackColor="lightGray" Style="vertical-align: middle"
                            runat="server" Width="30" ReadOnly="true"></asp:TextBox>
                            <asp:Label  ID="_lblClassificationTier" runat="server" Text="<%$ RIResources:Shared,Classification Tier %>"></asp:Label>:&nbsp;
                                        <asp:Label  ID="_lblClassificationTierValue" runat="server" Text="<%$ RIResources:Shared,Tier One Failure %>"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Panel ID="_pnlFailureClass" runat="server" HorizontalAlign="Center" Width="99%"
                            CssClass="collapsePanel" Height="0">
                            
                            <asp:Table ID="Table1" runat="server" Width="100%">
                                <asp:TableRow CssClass="Header">
                                    <asp:TableHeaderCell ColumnSpan="3">
                                        <asp:Label SkinID="LabelWhite" ID="_lblTotalClassification" Text='<%$ RIResources:Shared,TotalClassification %>'
                                            runat="server"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="_txtTotalClassification"
                                                runat="server" ReadOnly="true" BackColor="lightGray" Width="30"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label SkinID="LabelWhite" ID="_lblAverageClassification" Text='<%$ RIResources:Shared,AverageClassification %>'
                                            runat="server"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="_txtAverageClassification"
                                                runat="server" BackColor="lightGray" ReadOnly="true" Width="30"></asp:TextBox>
                                        
                                    </asp:TableHeaderCell>
                                </asp:TableRow>
                                <asp:TableRow CssClass="Border">
                                    <asp:TableCell Width="40%"><asp:Literal Text="<%$ RIResources:Shared,CostLowImpact %>" runat="server"></asp:Literal></asp:TableCell>
                                    <asp:TableCell Width="20%">
                                        <fieldset style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; padding-top: 2px">
                                            <legend><b>
                                                <asp:Literal ID="Literal20" Text="<%$ RIResources:Shared,Cost %>" runat="server"></asp:Literal></b></legend>
                                            <asp:RadioButtonList ID="_rblClassCost" runat="server" RepeatDirection="horizontal">
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </fieldset>
                                    </asp:TableCell>
                                    <asp:TableCell Width="40%">
                                        <asp:Literal ID="Literal3" Text="<%$ RIResources:Shared,CostHighImpact %>" runat="server"></asp:Literal></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow CssClass="Border">
                                    <asp:TableCell>
                                        <asp:Literal ID="Literal4" Text="<%$ RIResources:Shared,ClassLifeLowImpact %>" runat="server"></asp:Literal></asp:TableCell>
                                    <asp:TableCell>
                                        <fieldset>
                                            <legend>
                                                <asp:Literal ID="Literal5" Text="<%$ RIResources:Shared,LifeExpectancy %>" runat="server"></asp:Literal></legend>
                                            <asp:RadioButtonList ID="_rblClassLife" runat="server" RepeatDirection="horizontal">
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </fieldset>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Literal ID="Literal6" Text="<%$ RIResources:Shared,ClassLifeHighImpact %>" runat="server"></asp:Literal></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow CssClass="Border">
                                    <asp:TableCell>
                                        <asp:Literal ID="Literal7" Text="<%$ RIResources:Shared,PlannedLowImpact %>" runat="server"></asp:Literal></asp:TableCell>
                                    <asp:TableCell>
                                        <fieldset>
                                            <legend>
                                                <asp:Literal ID="Literal8" Text="<%$ RIResources:Shared,Planned %>" runat="server"></asp:Literal></legend>
                                            <asp:RadioButtonList ID="_rblClassPlanned" runat="server" RepeatDirection="horizontal">
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </fieldset>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Literal ID="Literal9" Text="<%$ RIResources:Shared,PlannedHighImpact %>" runat="server"></asp:Literal></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow CssClass="Border">
                                    <asp:TableCell>
                                        <asp:Literal ID="Literal10" Text="<%$ RIResources:Shared,RepairLowImpact %>" runat="server"></asp:Literal></asp:TableCell>
                                    <asp:TableCell>
                                        <fieldset style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; padding-top: 2px">
                                            <legend><b>
                                                <asp:Literal ID="Literal11" Text="<%$ RIResources:Shared,RepairTime %>" runat="server"></asp:Literal></b></legend>
                                            <asp:RadioButtonList ID="_rblClassRepair" runat="server" RepeatDirection="horizontal">
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </fieldset>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Literal ID="Literal12" Text="<%$ RIResources:Shared,RepairHighImpact %>" runat="server"></asp:Literal></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow CssClass="Border">
                                    <asp:TableCell>
                                        <asp:Literal ID="Literal13" Text="<%$ RIResources:Shared,ChronicLowImpact %>" runat="server"></asp:Literal></asp:TableCell>
                                    <asp:TableCell>
                                        <fieldset>
                                            <legend>
                                                <asp:Literal ID="Literal14" Text="<%$ RIResources:Shared,Chronic %>" runat="server"></asp:Literal></legend>
                                            <asp:RadioButtonList ID="_rblClassChronic" runat="server" RepeatDirection="horizontal">
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </fieldset>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Literal ID="Literal15" Text="<%$ RIResources:Shared,ChronicHighImpact %>" runat="server"></asp:Literal></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow CssClass="BorderSecondary">
                                    <asp:TableCell>
                                        <asp:Literal ID="Literal16" Text="<%$ RIResources:Shared,ClassDisplayLowImpact %>"
                                            runat="server"></asp:Literal></asp:TableCell>
                                    <asp:TableCell>
                                        <fieldset>
                                            <asp:RadioButtonList ID="_rblClassDisplay" runat="server" RepeatDirection="horizontal">
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </fieldset>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Literal ID="Literal17" Text="<%$ RIResources:Shared,ClassDisplayHighImpact %>"
                                            runat="server"></asp:Literal></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow CssClass="BorderSecondary">
                                    <asp:TableCell ColumnSpan="3" HorizontalAlign="center">
                                        <asp:Image ID="_imgClassCategory" runat="server" AlternateText="" /></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
           --%>
            <%-- 
            <ajaxToolkit:CollapsiblePanelExtender ID="_cpeFailureClass" runat="Server" TargetControlID="_pnlFailureClass"
                Collapsed="True" CollapseControlID="_lblFailureClassHeader" ExpandControlID="_lblFailureClassHeader"
                SuppressPostBack="true" TextLabelID="_lblFailureClassHeader" CollapsedText="<%$RIResources:Shared,ShowFailureClass%>"
                ExpandedText="<%$RIResources:Shared,HideFailureClass%>" ScrollContents="false"
                CollapsedSize="0" />--%>
            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                <tr class="Header">
                    <td align="left">
                        <asp:Label ID="_lblIncidentClassificationHeader" runat="server" Text="<%$RIResources:Shared,Incident Classification%>"
                            SkinID="LabelWhite" EnableViewState="false"></asp:Label>
                    </td>
                </tr>
                <tr class="Border">
                    <td>
                        <asp:Label ID="_lblTrigger" runat="server" Text='<%$RIResources:Shared,Trigger%>'
                            EnableViewState="false" />&nbsp;
                        <asp:DropDownList ID="_ddlTrigger" runat="server" Width="80%" style="max-width:600px"  >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="Border">
                    <td>
                        <IP:IncidentClassification2 ID="_IncidentClassification" runat="server" />
                    </td>
                </tr>
                <tr class="Header">
                    <td align="left">
                        <asp:Label ID="_lblDocumentCauses" Style="cursor: hand;" runat="server" SkinID="LabelWhite"></asp:Label>
                    </td>
                </tr>
            </table>
            <ajaxToolkit:CollapsiblePanelExtender ID="_cpeDocumentCauses" runat="Server" TargetControlID="_pnlDocumentCauses"
                Collapsed="True" CollapseControlID="_lblDocumentCauses" ExpandControlID="_lblDocumentCauses"
                SuppressPostBack="true" TextLabelID="_lblDocumentCauses" CollapsedText="+ Show Document Causes"
                ExpandedText="- Hide Document Causes" ScrollContents="false" />
            <asp:Panel ID="_pnlDocumentCauses" runat="server" Width="99%" HorizontalAlign="Center">
                <asp:Table ID="Table4" runat="server" Width="100%">
                    <asp:TableRow CssClass="Border">
                        <asp:TableCell HorizontalAlign="left">
                            <asp:Label ID="_lblPhysical" runat="server" Text='<%$RIResources:Shared,Physical Causes%>'></asp:Label>&nbsp;&nbsp;
                            <asp:HyperLink ID="_hypPhysicalDef" runat="server" CssClass="LabelLink" Text='<%$RIResources:Shared,Definition%>' /><br />
                            <asp:CheckBoxList ID="_cblPhysical" RepeatDirection="horizontal" RepeatLayout="table"
                                RepeatColumns="4" Width="100%" runat="server">
                            </asp:CheckBoxList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow CssClass="Border">
                        <asp:TableCell HorizontalAlign="Left">
                            <asp:Label ID="_lblOtherPhysical" runat="server" Text='<%$RIResources:Shared,Other Physical Cause%>'></asp:Label><br />
                            <IP:AdvancedTextBox SkinID="Translate" TextLink="<%$ RIResources:Shared,Translate %>"
                                ID="_txtOtherPhysical" runat="server" Width="60%" ExpandHeight="true"></IP:AdvancedTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow CssClass="Border">
                        <asp:TableCell HorizontalAlign="left">
                            <asp:Label ID="Label7" runat="server" Text='<%$RIResources:Shared,Human Causes%>'></asp:Label>&nbsp;&nbsp;
                            <asp:HyperLink ID="_hypDefHumanCauses" runat="server" CssClass="LabelLink" Text='<%$RIResources:Shared,Definition%>' /><br />
                            <asp:CheckBoxList ID="_cblHuman" RepeatDirection="horizontal" RepeatLayout="table"
                                RepeatColumns="4" Width="100%" runat="server">
                            </asp:CheckBoxList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow CssClass="Border">
                        <asp:TableCell HorizontalAlign="Left">
                            <asp:Label ID="Label9" runat="server" Text='<%$RIResources:Shared,Other Human Cause%>'></asp:Label><br />
                            <IP:AdvancedTextBox SkinID="Translate" TextLink="<%$ RIResources:Shared,Translate %>"
                                ID="_txtOtherHuman" runat="server" Width="60%" ExpandHeight="true"></IP:AdvancedTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow CssClass="Border">
                        <asp:TableCell HorizontalAlign="left">
                            <asp:Label ID="Label6" runat="server" Text='<%$RIResources:Shared,Latent Causes%>'></asp:Label>&nbsp;&nbsp;
                            <asp:HyperLink ID="_hypDefLatentCauses" runat="server" CssClass="LabelLink" Text='<%$RIResources:Shared,Definition%>' /><br />
                            <asp:CheckBoxList ID="_cblLatentCauses" RepeatDirection="horizontal" RepeatLayout="table"
                                RepeatColumns="4" Width="100%" runat="server">
                            </asp:CheckBoxList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow CssClass="Border">
                        <asp:TableCell HorizontalAlign="Left">
                            <asp:Label ID="Label8" runat="server" Text='<%$RIResources:Shared,Other Latent Cause%>'></asp:Label><br />
                            <IP:AdvancedTextBox SkinID="Translate" TextLink="<%$ RIResources:Shared,Translate %>"
                                ID="_txtOtherLatent" runat="server" Width="60%" ExpandHeight="true"></IP:AdvancedTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>
            <table border="0" width="100%" cellpadding="2" cellspacing="1">
                <tr class="Border">
                    <td valign="top">
                        <asp:Label ID="_lblLocationofFailedMaterial" runat="server" Text='<%$RIResources:Shared,Location of Failed Material%>'
                            EnableViewState="false" /><br />
                        <asp:DropDownList ID="_ddlFailedMaterial" runat="server" Width="80%"  >
                        </asp:DropDownList></td>
                    <td valign="top">
                        <asp:Label ID="_lblConditionsInfluencingFailure" runat="server" Text='<%$RIResources:Shared,Conditions Influencing Failure%>'
                            EnableViewState="false" /><br />
                        <IP:AdvancedTextBox SkinID="Translate" TextLink="<%$ RIResources:Shared,Translate %>"
                            ID="_txtConditionsInfluencingFailure" runat="server" TextMode="multiLine" Rows="1"
                            Width="80%" ExpandHeight="true" MaxLength="1200" /></td>
                </tr>
                <tr class="Border">
                    <td style="width: 50%" valign="top">
                        <asp:Label ID="_lblPeopletoInterview" runat="server" Text='<%$RIResources:Shared,People To Interview%>'
                            EnableViewState="false" /><br />
                        <IP:AdvancedTextBox SkinID="Translate" TextLink="<%$ RIResources:Shared,Translate %>"
                            ID="_txtPeopleToInterview" runat="server" TextMode="multiLine" Rows="1" ExpandHeight="true"
                            Width="80%" MaxLength="255" /></td>
                    <td style="width: 50%" valign="top">
                        <asp:Label ID="_lblTeamMembers" runat="server" Text='<%$RIResources:Shared,Team Members%>'
                            EnableViewState="false" /><br />
                        <IP:AdvancedTextBox SkinID="Translate" TextLink="<%$ RIResources:Shared,Translate %>"
                            ID="_txtTeamMembers" ExpandHeight="true" Rows="1" TextMode="MultiLine" runat="server"
                            Width="80%" MaxLength="255" /></td>
                </tr>
            </table>
            
            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                <tr class="Header">
                    <td align="left" colspan="8">
                        <asp:Label ID="_lblVerification" runat="server" Text="<%$RIResources:Shared,Verification%>"
                            SkinID="LabelWhite" EnableViewState="false" ></asp:Label>
                    </td>
                </tr>
                       <tr class="Border">
                       <td   valign="top" width="8%">
 <%--                       <asp:Label ID="Label10" Text='<%$ RIResources:Shared,Verification %>' runat="server" />
--%>                        <asp:RadioButtonList ID="_rblVerification"  TextAlign="Left" RepeatLayout="table" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" >
                             <asp:ListItem Text='<%$ RIResources:Shared,Yes %>' Value="Yes"></asp:ListItem>
                             <asp:ListItem Text='<%$ RIResources:Shared,No %>' Value="No"></asp:ListItem>
                         </asp:RadioButtonList></td>
                       <td  valign="top" width="12%"> <asp:Label ID="_lblPerson" Text='<%$ RIResources:Shared,Responsible %>' runat="server" />
                        <asp:DropDownList ID="_ddlPerson"  Enabled="true" runat="server" ></asp:DropDownList>
                        <asp:TextBox ReadOnly="true" BackColor="lightgray" runat="server" ID="_tbPerson" Width="200" Visible="false"></asp:TextBox></td>
                        <td  valign="top" width="10%"><asp:Label ID="_lblWeeksAfter" Text='<%$ RIResources:Shared,Weeks After %>' runat="server" />
                        <asp:TextBox ID="_tbWeeksAfter" Width="50"  Enabled="true"  runat="server" ></asp:TextBox></td>
                        <ajaxToolkit:FilteredTextBoxExtender ID="_ftbWeeksAfter" runat="server" TargetControlID="_tbWeeksAfter"
                            FilterType="custom" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                         <td   valign="top" width=10%"> <asp:Label ID="_lblDueDate" Text='<%$ RIResources:Shared,Due Date %>' runat="server" />
                        <asp:TextBox ID="_tbDueDate" Width="70" Enabled="false" runat="server" ></asp:TextBox></td>
                        <td  valign="top" width="10%"><asp:Label ID="_lblClosedDate" Text='<%$ RIResources:Shared,Closed Date %>' runat="server" />
                        <asp:TextBox ID="_tbClosedDate" Width="70" Enabled="false" runat="server" ></asp:TextBox>  
                        </td>
                        <td valign="top" width="4%">
                        <asp:Label ID="VerComment" Text='<%$ RIResources:Shared,Comments %>' runat="server" /> </td>
                        <td valign="top" width="44%">                     
                        <IP:AdvancedTextBox ID="_tbVerComment"  Enabled="false" TextMode="multiLine" Rows="2" ExpandHeight="false" Width="500" runat="server" /> </td> 
                        <td valign="top" > <asp:Button ID="_btnUpdateVerifyTask" runat="server" Visible="true" Text='<%$RIResources:Shared,Update Verification Task %>' /></td>
                        </tr>        
            </table>
            
            <table width="100%" border="0" cellpadding="2" cellspacing="1">
                <tr class="Border">
                    <td style="width: 25%">
                        <asp:Label ID="_lblCreatedBy" runat="server"></asp:Label></td>
                    <td style="width: 25%">
                        <asp:Label ID="_lblCreatedDate" runat="server"></asp:Label></td>
                    <td style="width: 25%">
                        <asp:Label ID="_lblUpdatedBy" runat="server"></asp:Label></td>
                    <td style="width: 25%">
                        <asp:Label ID="_lblLastUpdateDate" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <%--<asp:button ID=_btnS pell runat=server  Enabled=false Text="Spell Check" />--%>
                        <IP:SpellCheck ID="_btnSpell" ControlIdsToCheck="_txtIncidentTitle,_txtConditionsInfluencingFailure,_txtIncidentDescription,_txtOtherPhysical,_txtOtherHuman,_txtOtherLatent"
                            runat="server" />
                        <asp:Button ID="_btnSubmit" SkinID="ButtonBlack" Height="30px" runat="server" ValidationGroup="EnterNewRI" />
                        <asp:Button ID="_btnDelete" CausesValidation="false" runat="server" Text="<%$RIResources:Shared,Delete%>" />
                        <IP:IncidentHistory ID="_btnHistory" runat="server" />
                        <ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" ConfirmText="<%$RIResources:Shared,ConfirmDeleteIncident %>"
                            TargetControlID="_btnDelete">
                        </ajaxToolkit:ConfirmButtonExtender>
                        <div id="_divPhysicalDef" class="modalPopup" style="display: none">
                            <span style="text-align: left" class="ContentHeader">
                                <asp:Literal ID="Literal18" runat="server" Text='<%$RIResources:Shared,Physical Causes%>'></asp:Literal></span>
                            <p align="left">
                                <asp:Literal ID="Literal1" runat="server" Text='<%$RIResources:Shared,DefPhysicalRootCauses%>'></asp:Literal></p>
                        </div>
                        <div id="_divHumanCausesDef" class="modalPopup" style="display: none">
                            <span style="text-align: left" class="ContentHeader">
                                <asp:Literal ID="Literal21" runat="server" Text='<%$RIResources:Shared,Human Causes%>'></asp:Literal></span>
                            <p align="left">
                                <asp:Literal ID="Literal22" runat="server" Text='<%$RIResources:Shared,DefHumanCauses%>'></asp:Literal></p>
                        </div>
                        <div id="_divLatentDef" class="modalPopup" style="display: none">
                            <span style="text-align: left" class="ContentHeader">
                                <asp:Literal ID="Literal19" runat="server" Text='<%$RIResources:Shared,Latent Causes%>'></asp:Literal></span>
                            <p align="left">
                                <asp:Literal ID="Literal2" runat="server" Text='<%$RIResources:Shared,DefLatentRootCauses%>'></asp:Literal></p>
                        </div>
                        <ajaxToolkit:ModalPopupExtender CacheDynamicResults="true" ID="_mpeChangeAnalysis"
                            runat="server" TargetControlID="_imgChangeAnalysis" PopupControlID="_pnlChangeAnalysis"
                            BackgroundCssClass="modalBackground" DropShadow="true" OkControlID="_btnChangeAnalysisCancel"
                            CancelControlID="_btnChangeAnalysisCancel">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:ValidationSummary ID="_vsSummary" runat="server" CssClass="ValidationError"
                            DisplayMode="BulletList" ValidationGroup="EnterNewRI" HeaderText="<%$RIResources:Shared,RequiredFields %>"
                            ShowSummary="false" EnableClientScript="true" ShowMessageBox="false" />
                        <asp:Panel ID="_pnlChangeAnalysis" runat="server" CssClass="modalPopup" Style="display: none;">
                            <IP:Banner ID="Banner2" runat="server" BannerMessage="<%$RIResources:Shared,ChangeAnalysis%>"
                                DisplayPopupBanner="true" />
                            <asp:Table ID="Table2" runat="server" Width="100%" CellPadding="2" CellSpacing="0">
                                <asp:TableRow CssClass="Border">
                                    <asp:TableCell>
                                        <asp:Label ID="_lblChangeAnalysiMsg" runat="server"></asp:Label><br />
                                        <IP:AdvancedTextBox SkinID="Translate" TextLink="<%$ RIResources:Shared,Translate %>"
                                            ID="_txtChangeAnalysisComments" MaxLength="100" runat="server" ExpandHeight="true"
                                            Width="100%" TextMode="multiLine"></IP:AdvancedTextBox>
                                        <br />
                                        <br />
                                        <center>
                                            <IP:SpellCheck ID="SpellCheckChangeAnalysis" ControlIdsToCheck="_txtChangeAnalysisComments"
                                                runat="server" />
                                            <asp:Button ID="_btnChangeAnalysisOK" runat="server" Text="<%$RIResources:BUTTONTEXT,OK %>"
                                                Width="125" /><asp:Button ID="_btnChangeAnalysisCancel" runat="server" Text="<%$RIResources:BUTTONTEXT,Cancel %>"
                                                    Width="125" /></center>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <div style="visibility: hidden; display: none">
                            <asp:Image ID="_imgChangeAnalysis" runat="server" ImageUrl="~/Images/blank.gif" />
                            <asp:Image ID="_imgTextForEmail" runat="server" ImageUrl="~/Images/blank.gif" />
                        </div>
                        <ajaxToolkit:ModalPopupExtender CacheDynamicResults="true" ID="_mpeCommentsForEmail"
                            runat="server" TargetControlID="_imgTextForEmail" PopupControlID="_pnlEmailText"
                            BackgroundCssClass="modalBackground" DropShadow="true" OkControlID="_btnCancelEmailText"
                            CancelControlID="_btnCancelEmailText">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="_pnlEmailText" runat="server" CssClass="modalPopup" Style="display: none;">
                            <IP:Banner ID="Banner3" runat="server" BannerMessage="<%$RIResources:Shared,CommentsforEmail %>"
                                DisplayPopupBanner="true" />
                            <asp:Label ID="_lblCommentsForEmail" Text="<%$RIResources:Shared,AnalysisLeaderComments %>"
                                runat="server"></asp:Label><br />
                            <IP:AdvancedTextBox ID="_txtForEmail" runat="server" Width="80%" ExpandHeight="true"
                                TextMode="MultiLine"></IP:AdvancedTextBox><br />
                            <br />
                            <center>
                                <IP:SpellCheck ID="_btnSpellCheckEmail" ControlIdsToCheck="_txtForEmail" runat="server" />
                                <asp:Button ID="_btnCancelEmailText" runat="server" Text="OK" Width="125" />
                            </center>
                        </asp:Panel>
                        <Asp:UpdatePanel ID="_udpUpdateMenu" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <IP:MessageBox ID="_messageBox" runat="server" ButtonType="OKCancel" />
                                <asp:Panel ID="_pnlUpdateButtons" runat="server" HorizontalAlign="center" Visible="false"
                                    BorderWidth="0">
                                    <div style="visibility: hidden; display: none">
                                        <input type="submit" id="_btnRefresh" name="_btnRefresh" value='Refresh' />
                                    </div>
                                    <asp:Button ID="_btnAnalysisWorkspace" runat="server" Text='<%$RIResources:Shared,AWTitle %>' />
                                    <asp:Button ID="_btnCauses" runat="server" Text='<%$RIResources:Shared,DocumentCauses %>' />
                                    <asp:Button ID="_btnAttachments" runat="server" Text='<%$RIResources:Shared,Attachments %>' />
                                    <asp:Button ID="_btnActionItems" runat="server" Text='<%$RIResources:Shared,Action Items %>' />
                                    <br />
                                    <asp:Button ID="_btnMarkAnalysisComplete" runat="server" Text="<%$RIResources:Shared,Mark Analysis Complete %>"
                                        ValidationGroup="EnterNewRI" /><!--Mark Analysis Complete/Email-->
                                    <%--<asp:Button id="_btnSendAnalysisComplete" runat="server" Text="Send" style="visibility:hidden; display:none" />--%>
                                    <asp:Button ID="_btnExecutiveSummary" runat="server" Text="<%$RIResources:Shared,Executive Summary %>" /><!--Executive Summary-->
                                </asp:Panel>
                            </ContentTemplate>
                        </Asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <div id="_divRepairCostDefinition" class="modalPopup" style="display: none">
                <%--<span style="text-align: left" class="ContentHeader">Repair Costs:</span>
				<p>
					Materials and Labor (both in-house and contracted)</p>--%>
                <span style="text-align: left" class="ContentHeader">
                    <asp:Literal runat="Server" Text="<%$RIResources:Shared,Cost %>"></asp:Literal></span>
                <asp:Localize ID="_costDef" runat="server" Text="<%$RIResources:Shared,CostDefinition %>"></asp:Localize>
            </div>
            <div id="_divFinancialImpactDefinition" class="modalPopup" style="display: none">
                <span style="text-align: left" class="ContentHeader">
                    <asp:Literal runat="Server" Text="<%$RIResources:Shared,Financial Impact %>"></asp:Literal></span>
                <asp:Localize ID="_totcostDef" runat="server" Text="<%$RIResources:Shared,DefinitionTotalFinancialImpact %>"></asp:Localize>
                <%--<ul>
					<li>Repair Costs (Materials/Labor both in-house and contracted) </li>
					<li>Substitution Costs (Example: Gas for Oil) </li>
					<li>Loss of Direct Cost Materials (Fiber, Energy, Chemicals, Finishing Materials) </li>
					<li>Collateral Damage</li>
				</ul>--%>
                <%--<p>
                    The Total Financial Impact for the Reliability Recordable Incident is the Cost plus
                    the Profitability Loss.
                    <br>
                    <br />
                    The Profitability Loss is the amount of profit contribution lost due to not having
                    made production as a result of the Reliability Recordable Incident. This is typically
                    calculated as the amount of lost production multiplied by a standard profit contribution
                    for the product.</p>--%>
            </div>
        </ContentTemplate>
    </Asp:UpdatePanel>
</asp:Content>
