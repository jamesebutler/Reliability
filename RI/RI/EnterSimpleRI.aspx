<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="EnterSimpleRI.aspx.vb"
    Inherits="RI_EnterSimpleRI" Title="RI:Enter Simple" Trace="false" EnableViewState="true"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" EnableViewStateMac="false" 
    EnableTheming="true" StylesheetTheme="RIBlue" Theme="RIBlue"%>
    
<%@ MasterType VirtualPath="~/RI.master" %>

        <asp:Content ID="_contentMain" ContentPlaceHolderID="_cphMain" runat="Server">
    <karamasoft:ultimatespell id="_spellhidden" runat="server" showaddbutton="false"
        showspellbutton="false" ignoredisabledcontrols="true" showmodal="true" showoptions="true"
        enabletheming="false" spellasyoutype="false">
    </karamasoft:ultimatespell>
    <asp:updatepanel id="_udpLocation" runat="server" updatemode="Conditional">
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

            <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" >
                <tr class="Border" runat="server" id="_locationRow">
                    <td >
                        <span class="ValidationError">*</span><asp:Label ID="_lblFacility" runat="server"
                            Text='<%$ RIResources:Shared,Facility %>'></asp:Label><br />
                        <asp:DropDownList ID="_ddlFacility" onchange="updateSecurity();self.focus();"
                            CausesValidation="true" runat="server" AutoPostBack="false" Width="90%"  >
                        </asp:DropDownList>
                        
                        <asp:RequiredFieldValidator ValidationGroup="EnterDT" ID="_rfvFacility" runat="server"
                            Display="none" ControlToValidate="_ddlFacility" ErrorMessage="<%$ RIResources:Shared,SelectFacility %>"
                            EnableClientScript="true" SetFocusOnError="true" Text="<%$ RIResources:Shared,SelectFacility %>"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <span class="ValidationError">*</span><asp:Label ID="Label2" runat="server" Text='<%$ RIResources:Shared,busarea %>'></asp:Label><br />
                        <asp:DropDownList ID="_ddlBusinessUnit" CausesValidation="true" AutoPostBack="false"
                            EnableViewState="false" Width="90%" onchange="updateSecurity();self.focus();"
                            Visible="true" runat="server"  />
                        <asp:RequiredFieldValidator ValidationGroup="EnterDT" ID="_rfvBusinessUnit" runat="server"
                            Display="none" ControlToValidate="_ddlBusinessUnit" ErrorMessage="<%$ RIResources:Shared,SelectBusinessUnit %>"
                            EnableClientScript="true" SetFocusOnError="true" Text="<%$ RIResources:Shared,SelectBusinessUnit %>"></asp:RequiredFieldValidator></td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text='<%$ RIResources:Shared,LineLineBreak %>'></asp:Label><br />
                        <asp:DropDownList ID="_ddlLineBreak" CausesValidation="true" AutoPostBack="false" 
                            Width="98%" runat="server" onchange="updateSecurity();self.focus();" />
                    </td>
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
                            ControlToValidate="_txtDownTime" ValidationGroup="EnterDT" EnableClientScript="true"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr class="Border">
                    <td colspan="2">
                        <span class="ValidationError">*</span><asp:Label ID="_lblIncidentTitle" runat="server"
                            Text='<%$ RIResources:Shared,Downtime Title %>' EnableViewState="false" />&nbsp;<br />
                        <%--                        <asp:TextBox ID="_txtIncidentTitle" runat="server" MaxLength="200" Width="800"></asp:TextBox>
--%>
                        <IP:AdvancedTextBox SkinID="Translate" TextLink="<%$ RIResources:Shared,Translate %>"
                            ID="_txtIncidentTitle" runat="server" TextMode="multiLine" Rows="1" ExpandHeight="true"
                            Width="90%" MaxLength="200" />
                        <asp:RequiredFieldValidator ValidationGroup="EnterDT" ID="_rfvIncidentTitle" runat="server"
                            Display="none" ControlToValidate="_txtIncidentTitle" ErrorMessage="<%$ RIResources:Shared,EnterTitle %>"
                            EnableClientScript="true" SetFocusOnError="true" Text="<%$ RIResources:Shared,EnterTitle %>"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="_rblSchedUnsched" runat="server" RepeatColumns="2" CssClass="Label" Font-Italic="False">
                            <asp:ListItem Text="<%$ RIResources:Shared,Unscheduled%>" Value="Unscheduled" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="<%$ RIResources:Shared,Scheduled%>" Value="Scheduled"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                <tr class="Header">
                    <td align="left">
                        <asp:Label ID="_lblIncidentClassificationHeader" runat="server" Text="<%$RIResources:Shared,Incident Classification%>"
                            SkinID="LabelWhite" EnableViewState="false"></asp:Label>
                    </td>
                </tr>
                <tr class="Border">
                    <td>
                        <IP:IncidentClassification2 ID="_IncidentClassification" runat="server"/>
                    </td>
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
                        <IP:SpellCheck ID="_btnSpell" ControlIdsToCheck="_txtIncidentTitle"
                            runat="server" />
                        <asp:Button ID="_btnSubmit" SkinID="ButtonBlack" Height="30px" runat="server" ValidationGroup="EnterDT" />
                        <asp:Button ID="_btnNewDT" SkinID="ButtonBlack" Height="30px" runat="server" Visible="false" Text="<%$RIResources:Shared,Add Downtime%>"/>
                        <asp:Button ID="_btnDelete" CausesValidation="false" runat="server" Visible="false" Text="<%$RIResources:Shared,Delete%>" />
                        <ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" ConfirmText="<%$RIResources:Shared,ConfirmDeleteIncident %>"
                            TargetControlID="_btnDelete">
                        </ajaxToolkit:ConfirmButtonExtender>
                        <asp:ValidationSummary ID="_vsSummary" runat="server" CssClass="ValidationError"
                            DisplayMode="BulletList" ValidationGroup="EnterDT" HeaderText="<%$RIResources:Shared,RequiredFields %>"
                            ShowSummary="false" EnableClientScript="true" ShowMessageBox="false" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:updatepanel>
</asp:Content>
