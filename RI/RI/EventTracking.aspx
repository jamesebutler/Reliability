<%@ Page Language="vb" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="EventTracking.aspx.vb"
    Inherits="RI_EventTracking" Title="International Paper: RI" Trace="false"
    EnableTheming="true" StylesheetTheme="RIBlue" Theme="RIBlue"
    EnableEventValidation="false"
    MaintainScrollPositionOnPostback="true" EnableViewState="true" %>


<%@ MasterType VirtualPath="~/RI.master" %>
<%@ Register Namespace="RealWorld.Grids" TagPrefix="rwg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/RI/User Controls/Common/ucStartEndDates.ascx" TagName="StartEndDate" TagPrefix="IP" %>
<%--<%@ Register Src="~/RI/User Controls/ucEventClassification.ascx" TagName="Classification" TagPrefix="IP" %>--%>


<asp:Content ID="_cntEvent" ContentPlaceHolderID="_cphMain" runat="Server">

    <IP:SiteLocation ID="_siteLocation" runat="server" HideDivision="true" usecontextkey="true" />
    <asp:UpdatePanel ID="_upEvents" runat="server">
        <ContentTemplate>
            <table width="100%" class="Border">
                <tr>
                    <td>
                        <IP:DateRange ID="_DateRange" runat="server" showtime="true" visible="false" />
                        <IP:StartEndDate ID="_startEndDates" runat="server" />

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="test"></asp:Label>
                        <asp:Label runat="server" ID="Label1"></asp:Label>
                        <asp:Label runat="server" ID="Label2"></asp:Label>
                    </td>
                </tr>
            </table>

            <asp:Table runat="server">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Button ID="_btnViewUpdate" Text="<%$RIResources:Shared,ViewUpdate%>" runat="server" />&nbsp;
                    <asp:Button ID="_btnExcel" Text="<%$RIResources:Shared,Excel%>" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="_btnSaveEvents" Text="<%$RIResources:Shared,Save Events%>" runat="server" BackColor="red" />&nbsp;
                    <asp:Button ID="_btnAddEvent" Text="<%$RIResources:Shared,Add Events%>" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Table ID="_tblEvents" runat="server" Width="100%" BorderStyle="Outset">
                <asp:TableRow CssClass="Header">
                    <asp:TableCell Width="85%" ColumnSpan="5" Style="text-align: left">
                        <asp:Label ID="Label15" SkinID="LabelWhite" runat="server" Text="Events"></asp:Label>
                        <asp:RadioButtonList ID="_rblEventShow" runat="server" RepeatLayout="Table" RepeatColumns="3" ForeColor="White" AutoPostBack="true">
                            <asp:ListItem Text="Unanswered" Value="Unanswered" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Answered" Value="Answered"></asp:ListItem>
                            <asp:ListItem Text="All" Value="All"></asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <b>
                <table width="100%">
                    <tr>
                        <td align="center" width="15%">Area/Line</td>
                        <td width="10%" align="center">Start/End</td>
                        <td style="width: 45px;">Crew</td>
                        <td style="width: 150px;">Scheduled/Unsched</td>
                        <td style="width: 150px;">Process</td>
                        <td style="width: 140px; text-align: left">Type</td>
                        <td style="width: 150px;">Component</td>
                        <td>Reason</td>
                    </tr>
                </table>
            </b>
            <RWG:BulkEditGridView ID="_gvEvents" runat="server" AutoGenerateColumns="False" DataKeyNames="RINUMBER" ShowHeader="False"
                EnableViewState="true" Width="100%" BorderStyle="Solid" AlternatingRowStyle-BorderColor="blue">
                <Columns>
                    <%--<asp:BoundField DataField="RINumber" />--%>
                    <asp:TemplateField ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="_tbAreaLine" runat="server" Width="100%" Style="text-align: center" Enabled="false" Text='<%# Container.DataItem("bua")%>'> </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <asp:Label ID="_tbStartDate" runat="server" Width="100%" Style="text-align: center" Enabled="false" Text='<%# Container.DataItem("StartDate")%>'> </asp:Label>
                            <asp:Label ID="_tbEndDate" runat="server" Width="100%" Style="text-align: center" Enabled="false" Text='<%# Container.DataItem("EndDate") %>'>   </asp:Label>
                            <center><asp:LinkButton runat="server" ID="_lbtnSplit" Text="Split Event" CommandName="Select" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                        Visible='<%# Container.DataItem("NoSplits")%>'></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="_lbtnRemoveSplit" Text="Remove Split" CommandName="Remove" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                Visible='<%# Container.DataItem("HasSplits")%>'></asp:LinkButton>
                        </center>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:ButtonField Text="Split" CommandName="Select" Visible="false" />

                    <asp:TemplateField ItemStyle-Width="100%">
                        <ItemTemplate>
                            <asp:HiddenField ID="_hdnRowCount" runat="server" />
                            <asp:Panel ID="_pnlParent" runat="server" Visible="true" EnableViewState="true">
                                <table>
                                    <col width="50px" />
                                    <col width="150px" />
                                    <col width="150px" />
                                    <col width="150px" />
                                    <col width="150px" />
                                    <col width="250px" />
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="_ddlCrew" runat="server" OnSelectIndexChanged="_gvEvents.HandleRowChanged" />
                                            <ajaxToolkit:CascadingDropDown ID="_cddlCrew1" runat="server" Category="Crew" LoadingText="..."
                                                PromptText="   " ServiceMethod="GetCrew" ServicePath="~/CascadingLists.asmx"
                                                TargetControlID="_ddlCrew" ParentControlID="ctl00__cphMain__siteLocation__ddlFacility" SelectedValue='<%# Container.DataItem("crew")%>'></ajaxToolkit:CascadingDropDown>
                                        </td>

                                        <td>
                                            <asp:DropDownList runat="server" ID="_ddlSchedUnsched" OnSelectIndexChanged="_gvEvents.HandleRowChanged">
                                                <asp:ListItem Text="Scheduled" Value="Yes"></asp:ListItem>
                                                <asp:ListItem Text="Unscheduled" Value="No"></asp:ListItem>
                                            </asp:DropDownList>

                                        </td>

                                        <td>
                                            <asp:DropDownList ID="_ddlProcess" Width="99%" runat="server" onchange="self.focus();" />
                                            <ajaxToolkit:CascadingDropDown ID="_cddlProcess1" runat="server" Category="Process"
                                                LoadingText="..." PromptText="<%$RIResources:Shared,SelectProcess %>" ServiceMethod="GetProcessTypeFirstList"
                                                UseContextKey="true" ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlProcess" ParentControlID="ctl00__cphMain__siteLocation__ddlArea" SelectedValue='<%# Container.DataItem("process")%>'></ajaxToolkit:CascadingDropDown>
                                        </td>

                                        <td>
                                            <asp:DropDownList ID="_ddlType" Width="99%" runat="server" onchange="self.focus();" OnSelectIndexChanged="_gvEvents.HandleRowChanged" />
                                            <ajaxToolkit:CascadingDropDown ID="_cddlTypes1" runat="server" Category="Causes" LoadingText="..."
                                                PromptText="<%$RIResources:Shared,SelectType %>" ServiceMethod="GetProcessTypeList"
                                                ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlType" ParentControlID="_ddlProcess" SelectedValue='<%# Container.DataItem("cause")%>'></ajaxToolkit:CascadingDropDown>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="_ddlComponent" Width="99%" runat="server" onchange="self.focus();" />
                                            <ajaxToolkit:CascadingDropDown ID="_cddlComponent1" runat="server" Category="Component"
                                                LoadingText="..." PromptText="<%$RIResources:Shared,SelectComponent %>" ServiceMethod="GetComponentList"
                                                ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlComponent" ParentControlID="_ddlType" SelectedValue='<%# Container.DataItem("component")%>'></ajaxToolkit:CascadingDropDown>
                                        </td>

                                        <td>
                                            <asp:DropDownList ID="_ddlReason" Width="99%" runat="server" onchange="self.focus();" />
                                            <ajaxToolkit:CascadingDropDown ID="_cddlReason1" runat="server" Category="Reason"
                                                LoadingText="..." PromptText="<%$RIResources:Shared,SelectCause %>" ServiceMethod="GetReasonList"
                                                ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlReason" ParentControlID="_ddlType" SelectedValue='<%# Container.DataItem("reason")%>'></ajaxToolkit:CascadingDropDown>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <IP:AdvancedTextBox ID="_atbComments" runat="server" expandheight="true" ontextChanged="_gvEvents.HandleRowChanged"
                                                width="100%" rows="1" style="font-size: 12px; color: Black; font-family: Verdana;" textmode="MultiLine" maxlength="2000" text='<%# Container.DataItem("ricomment")%>' />

                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                                       
                            <RWG:BulkEditGridView ID="_gvSplitEventDetail" runat="server" AutoGenerateColumns="False" DataKeyNames="RINUMBER"
                                Width="100%" BackColor="silver" BorderStyle="None" >
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" ShowHeader="false">
                                        <EditItemTemplate>
                                            <table width="95%">
                                                <tr>
                                                    <td rowspan="3" width="10%">
                                                        <asp:Label ID="_tbStartDate" runat="server" Width="100%" Enabled="false" Text='<%# Container.DataItem("StartDate")%>'> </asp:Label>
                                                        <asp:Label ID="_tbEndDate" runat="server" Width="100%" Enabled="false" Text='<%# Container.DataItem("EndDate") %>'>   </asp:Label>
                                                        <asp:HiddenField ID="_rowChanged" runat="server" OnValueChanged="_gvEvents.HandleRowChanged" />
                                                    </td>

                                                    <td>
                                                        <asp:HiddenField ID="_hdnSplitCrew" runat="server" ClientIDMode="static" />
                                                        <asp:DropDownList ID="_ddlSplitCrew" Width="99%" runat="server" onchange="controlChanged(this,_hdnSplitCrew);" ViewStateMode="Enabled" />
                                                        <ajaxToolkit:CascadingDropDown ID="_cddlCrew" runat="server" Category="Crew" LoadingText="..."
                                                            PromptText="   " ServiceMethod="GetCrew" ServicePath="~/CascadingLists.asmx"
                                                            TargetControlID="_ddlSplitCrew" ParentControlID="ctl00__cphMain__siteLocation__ddlFacility" SelectedValue='<%# Container.DataItem("crew")%>'></ajaxToolkit:CascadingDropDown>
                                                    </td>

                                                    <td>
                                                        <asp:DropDownList runat="server" ID="_ddlSplitSchedUnsched">
                                                            <asp:ListItem Text="Scheduled" Value="Yes"></asp:ListItem>
                                                            <asp:ListItem Text="Unscheduled" Value="No"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>

                                                    <td>
                                                        <asp:DropDownList ID="_ddlSplitProcess" Width="99%" runat="server" onchange="self.focus();" OnSelectIndexChanged="_gvEvents.HandleRowChanged" />
                                                        <ajaxToolkit:CascadingDropDown ID="_cddlProcess" runat="server" Category="Process"
                                                            LoadingText="..." PromptText="<%$RIResources:Shared,SelectProcess %>" ServiceMethod="GetProcessTypeFirstList"
                                                            UseContextKey="true" ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlSplitProcess" SelectedValue='<%# Container.DataItem("process")%>'
                                                            ParentControlID="ctl00__cphMain__siteLocation__ddlArea"></ajaxToolkit:CascadingDropDown>
                                                    </td>

                                                    <td>
                                                        <asp:DropDownList ID="_ddlSplitType" Width="99%" runat="server" onchange="self.focus();" OnSelectIndexChanged="_gvEvents.HandleRowChanged" />
                                                        <ajaxToolkit:CascadingDropDown ID="_cddlTypes" runat="server" Category="Causes" LoadingText="..."
                                                            PromptText="<%$RIResources:Shared,SelectType %>" ServiceMethod="GetProcessTypeList"
                                                            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlSplitType" ParentControlID="_ddlSplitProcess" SelectedValue='<%# Container.DataItem("cause")%>'></ajaxToolkit:CascadingDropDown>
                                                    </td>

                                                    <td>
                                                        <asp:DropDownList ID="_ddlSplitComponent" Width="99%" runat="server" onchange="self.focus();" />
                                                        <ajaxToolkit:CascadingDropDown ID="_cddlComponent" runat="server" Category="Component"
                                                            LoadingText="..." PromptText="<%$RIResources:Shared,SelectComponent %>" ServiceMethod="GetComponentList"
                                                            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlSplitComponent" ParentControlID="_ddlSplitType" SelectedValue='<%# Container.DataItem("component")%>'></ajaxToolkit:CascadingDropDown>
                                                    </td>

                                                    <td>
                                                        <asp:DropDownList ID="_ddlSplitReason" Width="99%" runat="server" onchange="self.focus();" />
                                                        <ajaxToolkit:CascadingDropDown ID="_cddlReason" runat="server" Category="Reason"
                                                            LoadingText="..." PromptText="<%$RIResources:Shared,SelectCause %>" ServiceMethod="GetReasonList"
                                                            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlSplitReason" ParentControlID="_ddlSplitType" SelectedValue='<%# Container.DataItem("reason")%>'></ajaxToolkit:CascadingDropDown>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <IP:AdvancedTextBox ID="_atbSplitComment" runat="server" expandheight="true" ontextChanged="_gvEvents.HandleRowChanged"
                                                            rows="1" width="100%" style="font-size: 12px; color: Black; font-family: Verdana;" textmode="MultiLine" maxlength="2000" text='<%# Container.DataItem("ricomment")%>' />

                                                    </td>
                                                </tr>
                                            </table>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </RWG:BulkEditGridView>

                    </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </RWG:BulkEditGridView>


            <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Style="display: none" ClientIDMode="static">

                <asp:Label ID="_lbSplitRINumber" runat="server"></asp:Label>
                <asp:Label Font-Bold="true" ID="_lbSplit" runat="server" Text="Split Events - "></asp:Label>
                <asp:Label ID="_lbBUA" runat="server" Text=""></asp:Label>
                <asp:HiddenField ID="_hdnStartDate" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="_hdnEndDate" runat="server" ClientIDMode="Static" />
                <asp:Label ID="_lbParentStartDate" runat="server" ClientIDMode="Static" />
                -
                <asp:Label ID="_lbParentEndDate" runat="server" ClientIDMode="Static" />

                <br />
                <table align="center" width="100%">
                    <tr>

                        <td>
                            <asp:HiddenField ID="_hdnStart1" runat="server" ClientIDMode="static" />
                            <asp:HiddenField ID="_hdnEnd1" runat="server" ClientIDMode="static" />
                            <asp:TextBox ID="_tbStart" ClientIDMode="static" ReadOnly="true" runat="server" Width="25%" Style="text-align: center"> </asp:TextBox>
                            <asp:TextBox ID="_tbEnd" ClientIDMode="static" runat="server" Width="25%" Style="text-align: center" onchange="onChangeEndDate(this,_tbStart2,_hdnStart2,_tbEnd2,_hdnEnd1,_hdnEndDate,_hdnEnd2, _hdnStartDate);self.focus();"> </asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender runat="server"
                                ID="_meeTbEnd"
                                TargetControlID="_tbEnd"
                                Mask="99/99/99 99:99"
                                MaskType="DateTime"
                                UserDateFormat="DayMonthYear"
                                UserTimeFormat="TwentyFourHour"
                                CultureDateFormat="DMY"
                                CultureDatePlaceholder="/"
                                CultureTimePlaceholder=":"></ajaxToolkit:MaskedEditExtender>
                            <asp:DropDownList ID="_ddlCrew1" Width="10%" runat="server" onchange="self.focus();" OnSelectIndexChanged="_gvEvents.HandleRowChanged" />
                            <ajaxToolkit:CascadingDropDown ID="_cddlCrew" runat="server" Category="Crew" LoadingText="..."
                                PromptText="   " ServiceMethod="GetCrew" ServicePath="~/CascadingLists.asmx"
                                TargetControlID="_ddlCrew1" ParentControlID="ctl00__cphMain__siteLocation__ddlFacility"></ajaxToolkit:CascadingDropDown>
                            <asp:DropDownList runat="server" ID="_ddlSchedUnsched1">
                                <asp:ListItem Text="Scheduled" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="Unscheduled" Value="No"></asp:ListItem>
                            </asp:DropDownList>
                            <table width="100%">
                                <tr class="Border">

                                    <td>
                                        <asp:Label ID="_lblEquipmentProcess" runat="server" Text='<%$RIResources:Shared,EquipmentProcess %>'/><br />
                                        <asp:DropDownList ID="_ddlSplitProcess1" Width="99%" runat="server" onchange="self.focus();" />
                                        <ajaxToolkit:CascadingDropDown ID="_cddlProcess" runat="server" Category="Process"
                                            LoadingText="..." PromptText="<%$RIResources:Shared,SelectProcess %>" ServiceMethod="GetProcessTypeFirstList"
                                            UseContextKey="true" ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlSplitProcess1"
                                            ParentControlID="ctl00__cphMain__siteLocation__ddlBusinessUnit"></ajaxToolkit:CascadingDropDown>
                                    </td>

                                    <td>
                                        <asp:Label ID="_lblCause" runat="server" Text='<%$RIResources:Shared,Type %>' EnableViewState="false" /><br />
                                        <asp:DropDownList ID="_ddlSplitType1" Width="99%" runat="server" onchange="self.focus();" />
                                        <ajaxToolkit:CascadingDropDown ID="_cddlTypes" runat="server" Category="Causes" LoadingText="..."
                                            PromptText="<%$RIResources:Shared,SelectType %>" ServiceMethod="GetProcessTypeList"
                                            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlSplitType1" ParentControlID="_ddlSplitProcess1"></ajaxToolkit:CascadingDropDown>
                                    </td>

                                    <td>
                                        <asp:Label ID="_lblComponent" runat="server" Text='<%$RIResources:Shared,Component %>' EnableViewState="true" /><br />
                                        <asp:DropDownList ID="_ddlSplitComponent1" Width="99%" runat="server" onchange="self.focus();" />
                                        <ajaxToolkit:CascadingDropDown ID="_cddlComponent" runat="server" Category="Component"
                                            LoadingText="..." PromptText="<%$RIResources:Shared,SelectComponent %>" ServiceMethod="GetComponentList"
                                            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlSplitComponent1" ParentControlID="_ddlSplitType1"></ajaxToolkit:CascadingDropDown>
                                    </td>

                                    <td>
                                        <asp:Label ID="_lblReason" runat="server" Text='<%$RIResources:Shared,Cause %>' /><br />
                                        <asp:DropDownList ID="_ddlSplitReason1" Width="99%" runat="server" onchange="self.focus();" />
                                        <ajaxToolkit:CascadingDropDown ID="_cddlReason" runat="server" Category="Reason"
                                            LoadingText="..." PromptText="<%$RIResources:Shared,SelectCause %>" ServiceMethod="GetReasonList"
                                            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlSplitReason1" ParentControlID="_ddlSplitType1"></ajaxToolkit:CascadingDropDown>
                                    </td>
                                </tr>
                            </table>
                            <br />

                        </td>
                    </tr>
                    <tr>

                        <td>
                            <asp:HiddenField ID="_hdnStart2" runat="server" ClientIDMode="static" />
                            <asp:HiddenField ID="_hdnEnd2" runat="server" ClientIDMode="static" />
                            <asp:TextBox ID="_tbStart2" runat="server" ReadOnly="true" Width="25%" Style="text-align: center" ClientIDMode="static" BehaviorID="splitStart2"> </asp:TextBox>
                            <asp:TextBox ID="_tbEnd2" ClientIDMode="static" runat="server" Width="25%" Style="text-align: center" onchange="onChangeEndDate(this,_tbStart3,_hdnStart3,_tbEnd3,_hdnEnd2,_hdnEndDate,_hdnEnd3 );self.focus();"> </asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender runat="server"
                                ID="meeDateTime"
                                TargetControlID="_tbEnd2"
                                Mask="99/99/99 99:99"
                                MaskType="DateTime"
                                UserDateFormat="DayMonthYear"
                                UserTimeFormat="TwentyFourHour"
                                CultureDateFormat="DMY"
                                CultureDatePlaceholder="/"
                                CultureTimePlaceholder=":"></ajaxToolkit:MaskedEditExtender>

                            <asp:DropDownList ID="_ddlCrew2" ClientIDMode="AutoID" Width="10%" runat="server" onchange="self.focus();" OnSelectIndexChanged="_gvEvents.HandleRowChanged" />
                            <ajaxToolkit:CascadingDropDown ID="CascadingDropDown1" runat="server" Category="Crew" LoadingText="..."
                                PromptText="   " ServiceMethod="GetCrew" ServicePath="~/CascadingLists.asmx"
                                TargetControlID="_ddlCrew2" ParentControlID="ctl00__cphMain__siteLocation__ddlFacility"></ajaxToolkit:CascadingDropDown>
                            <asp:DropDownList runat="server" ID="_ddlSchedUnsched2">
                                <asp:ListItem Text="Scheduled" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="Unscheduled" Value="No"></asp:ListItem>
                            </asp:DropDownList>

                            <table width="100%">
                                <tr class="Border">

                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text='<%$RIResources:Shared,EquipmentProcess %>'
                                            EnableViewState="true" /><br />
                                        <asp:DropDownList ID="_ddlSplitProcess2" Width="99%" runat="server" onchange="self.focus();" />
                                        <ajaxToolkit:CascadingDropDown ID="CascadingDropDown3" runat="server" Category="Process"
                                            LoadingText="..." PromptText="<%$RIResources:Shared,SelectProcess %>" ServiceMethod="GetProcessTypeFirstList"
                                            UseContextKey="true" ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlSplitProcess2"
                                            ParentControlID="ctl00__cphMain__siteLocation__ddlBusinessUnit"></ajaxToolkit:CascadingDropDown>
                                    </td>

                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%$RIResources:Shared,Type %>' EnableViewState="false" /><br />
                                        <asp:DropDownList ID="_ddlSplitType2" Width="99%" runat="server" onchange="self.focus();" />
                                        <ajaxToolkit:CascadingDropDown ID="CascadingDropDown4" runat="server" Category="Causes" LoadingText="..."
                                            PromptText="<%$RIResources:Shared,SelectType %>" ServiceMethod="GetProcessTypeList"
                                            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlSplitType2" ParentControlID="_ddlSplitProcess2"></ajaxToolkit:CascadingDropDown>
                                    </td>

                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text='<%$RIResources:Shared,Component %>' EnableViewState="true" /><br />
                                        <asp:DropDownList ID="_ddlSplitComponent2" Width="99%" runat="server" onchange="self.focus();" />
                                        <ajaxToolkit:CascadingDropDown ID="CascadingDropDown5" runat="server" Category="Component"
                                            LoadingText="..." PromptText="<%$RIResources:Shared,SelectComponent %>" ServiceMethod="GetComponentList"
                                            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlSplitComponent2" ParentControlID="_ddlSplitType2"></ajaxToolkit:CascadingDropDown>
                                    </td>

                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text='<%$RIResources:Shared,Cause %>' EnableViewState="true" /><br />
                                        <asp:DropDownList ID="_ddlSplitReason2" Width="99%" runat="server" onchange="self.focus();" />
                                        <ajaxToolkit:CascadingDropDown ID="CascadingDropDown6" runat="server" Category="Reason"
                                            LoadingText="..." PromptText="<%$RIResources:Shared,SelectCause %>" ServiceMethod="GetReasonList"
                                            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlSplitReason2" ParentControlID="_ddlSplitType2"></ajaxToolkit:CascadingDropDown>
                                    </td>
                                </tr>
                            </table>
                            <br />

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="_hdnStart3" runat="server" ClientIDMode="static" />
                            <asp:HiddenField ID="_hdnEnd3" runat="server" ClientIDMode="static" />
                            <asp:TextBox ID="_tbStart3" runat="server" ClientIDMode="Static" ReadOnly="true" Style="text-align: center" Width="25%"> </asp:TextBox>
                            <asp:TextBox ID="_tbEnd3" ReadOnly="true" runat="server" ClientIDMode="Static" Style="text-align: center" Width="25%"> </asp:TextBox>
                            <asp:DropDownList ID="_ddlCrew3" runat="server" ClientIDMode="AutoID" onchange="self.focus();" OnSelectIndexChanged="_gvEvents.HandleRowChanged" Width="10%" />
                            <ajaxToolkit:CascadingDropDown ID="CascadingDropDown2" runat="server" Category="Crew" LoadingText="..." ParentControlID="ctl00__cphMain__siteLocation__ddlFacility" PromptText="   " ServiceMethod="GetCrew" ServicePath="~/CascadingLists.asmx" TargetControlID="_ddlCrew3" />
                            <asp:DropDownList ID="_ddlSchedUnsched3" runat="server">
                                <asp:ListItem Text="Scheduled" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="Unscheduled" Value="No"></asp:ListItem>
                            </asp:DropDownList>
                            <table width="100%">
                                <tr class="Border">

                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text='<%$RIResources:Shared,EquipmentProcess %>'
                                            EnableViewState="true" /><br />
                                        <asp:DropDownList ID="_ddlSplitProcess3" Width="99%" runat="server" onchange="self.focus();" />
                                        <ajaxToolkit:CascadingDropDown ID="CascadingDropDown7" runat="server" Category="Process"
                                            LoadingText="..." PromptText="<%$RIResources:Shared,SelectProcess %>" ServiceMethod="GetProcessTypeFirstList"
                                            UseContextKey="true" ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlSplitProcess3"
                                            ParentControlID="ctl00__cphMain__siteLocation__ddlBusinessUnit"></ajaxToolkit:CascadingDropDown>
                                    </td>

                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text='<%$RIResources:Shared,Type %>' EnableViewState="true" /><br />
                                        <asp:DropDownList ID="_ddlSplitType3" Width="99%" runat="server" onchange="self.focus();" />
                                        <ajaxToolkit:CascadingDropDown ID="CascadingDropDown8" runat="server" Category="Causes" LoadingText="..."
                                            PromptText="<%$RIResources:Shared,SelectType %>" ServiceMethod="GetProcessTypeList"
                                            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlSplitType3" ParentControlID="_ddlSplitProcess3"></ajaxToolkit:CascadingDropDown>
                                    </td>

                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text='<%$RIResources:Shared,Component %>' EnableViewState="true" /><br />
                                        <asp:DropDownList ID="_ddlSplitComponent3" Width="99%" runat="server" onchange="self.focus();" />
                                        <ajaxToolkit:CascadingDropDown ID="CascadingDropDown9" runat="server" Category="Component"
                                            LoadingText="..." PromptText="<%$RIResources:Shared,SelectComponent %>" ServiceMethod="GetComponentList"
                                            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlSplitComponent3" ParentControlID="_ddlSplitType3"></ajaxToolkit:CascadingDropDown>
                                    </td>

                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text='<%$RIResources:Shared,Cause %>' EnableViewState="true" /><br />
                                        <asp:DropDownList ID="_ddlSplitReason3" Width="99%" runat="server" onchange="self.focus();" />
                                        <ajaxToolkit:CascadingDropDown ID="CascadingDropDown10" runat="server" Category="Reason"
                                            LoadingText="..." PromptText="<%$RIResources:Shared,SelectCause %>" ServiceMethod="GetReasonList"
                                            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlSplitReason3" ParentControlID="_ddlSplitType3"></ajaxToolkit:CascadingDropDown>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="_btnUpdateSplits" runat="server" Text="Update" Style="display: none" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                        </td>
                    </tr>
                </table>

            </asp:Panel>

            <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>

            <ajaxToolkit:ModalPopupExtender ID="popup" runat="server" DropShadow="false" PopupControlID="pnlAddEdit" TargetControlID="lnkFake"
                BackgroundCssClass="modalBackground" CancelControlID="btnCancel" BehaviorID="bePopup">
            </ajaxToolkit:ModalPopupExtender>


            <%--<asp:Panel ID="_pnlAddEvent" runat="server" CssClass="modalPopup" Style="display: none" ClientIDMode="static">
                
                <asp:Label Font-Bold="true" ID="Label4" runat="server" Text="Add Event" ></asp:Label>
                
                <br />
                <table align="center">
                    <tr>
                        <td>
                            <IP:StartEndDate ID="_NewStartEnd" runat="server" ClientIDMode="static" />

                            <asp:DropDownList ID="_ddlNewCrew" Width="10%" runat="server" />
                            <ajaxToolkit:CascadingDropDown ID="CascadingDropDown3" runat="server" Category="Crew" LoadingText="..."
                                PromptText="   " ServiceMethod="GetCrew" ServicePath="~/CascadingLists.asmx"
                                TargetControlID="_ddlNewCrew" ParentControlID="ctl00__cphMain__siteLocation__ddlFacility"></ajaxToolkit:CascadingDropDown>
                            <asp:DropDownList runat="server" id="_ddlNewSchedUnsched" >
                                <asp:ListItem Text="Scheduled" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="Unscheduled" Value="No"></asp:ListItem>
                            </asp:DropDownList>
                            <IP:Classification ID="_NewClassification" ClientIDMode="AutoID" runat="server" />
                            <br />
                    
                        </td>
                    </tr>
                    <tr>
                            <td>
                                <asp:Button ID="_btnNewAdd" runat="server" Text="Add" Style="display=none" />
                                <asp:Button ID="_btnNewCancel" runat="server" Text="Cancel" />
                            </td>
                        </tr>
 
                </table>

            </asp:Panel>

            <asp:LinkButton ID="_lbFake2" runat="server"></asp:LinkButton>

            <ajaxToolkit:ModalPopupExtender ID="_mpeAddEvent" runat="server" DropShadow="false" PopupControlID="_pnlAddEvent" TargetControlID="_lbFake2"
                BackgroundCssClass="modalBackground" CancelControlID="_btnNewCancel" BehaviorID="beNewPopup">
            </ajaxToolkit:ModalPopupExtender>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
