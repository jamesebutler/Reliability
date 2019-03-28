<%@ Page Title="" Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="EnterMOC.aspx.vb"
    Inherits="MOC_EnterMOCNew" EnableViewState="true" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<%@ Register Src="~/RI/User Controls/Common/ucMOCStatus.ascx" TagName="ucMOCStatus" TagPrefix="IP" %>
<%@ Register Src="~/RI/User Controls/Common/ucMOCSwapListBox.ascx" TagName="ucMOCSwapListBox" TagPrefix="IP" %>
<%@ Register Src="~/RI/User Controls/Common/UcMTTResponsible.ascx" TagName="Responsible" TagPrefix="IP" %>
<%@ Register Src="~/RI/User Controls/Common/UcJQDate.ascx" TagName="JQDate" TagPrefix="IP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">
    <asp:UpdatePanel ID="_udpLocation" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
          <asp:HiddenField ID="_hfMOCType" runat="server" />
          <ajaxToolkit:CascadingDropDown ID="_cddlFacility" runat="server" Category="SiteID"
                LoadingText="" PromptText='<%# IIf(CBool(Eval("IsBlocked")), "", "[Please select]") %>' ServiceMethod="GetFacilityList" ServicePath="~/CascadingLists.asmx"
                TargetControlID="_ddlFacility" UseContextKey="true"></ajaxToolkit:CascadingDropDown>

            <ajaxToolkit:CascadingDropDown ID="_cddlBusArea" runat="server" Category="BusArea"
                LoadingText="" PromptText="    " ServiceMethod="GetBusArea" ServicePath="~/CascadingLists.asmx"
                TargetControlID="_ddlBusinessUnit" ParentControlID="_ddlFacility"></ajaxToolkit:CascadingDropDown>
            <ajaxToolkit:CascadingDropDown ID="_cddlLineBreak" runat="server" Category="LineBreak"
                LoadingText="" PromptText="    " ServiceMethod="GetLineBreak" ServicePath="~/CascadingLists.asmx"
                TargetControlID="_ddlLineBreak" ParentControlID="_ddlBusinessUnit"></ajaxToolkit:CascadingDropDown>
            <ajaxToolkit:CascadingDropDown ID="_ccdlOwner" runat="server" Category="Employee"
                LoadingText="" PromptText="    " ServiceMethod="GetEmployee" ServicePath="~/CascadingLists.asmx"
                TargetControlID="_ddlOwner" ParentControlID="_ddlFacility"></ajaxToolkit:CascadingDropDown>

            <asp:Table ID="table" runat="server" Width="100%" CellPadding="0" CellSpacing="0">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Table ID="_tblDesc" runat="server" CellPadding="2" CellSpacing="0" Width="100%">
                            <asp:TableRow CssClass="Border" HorizontalAlign="center">
                                <asp:TableCell ColumnSpan="3">
                                    <asp:Label ID="_lblDesc" runat="server" ForeColor="#8A2020" EnableViewState="false" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                        <asp:Table ID="_tblMain" runat="server" CellPadding="0" CellSpacing="0" Width="100%">
                            <asp:TableRow CssClass="Border">
                                <asp:TableCell Width="20%" VerticalAlign="Top">
                                    <span class="ValidationError">*</span>
                                    <asp:Label ID="_lbOwner" runat="server" Text='<%$RIResources:Shared,MOC Owner %>' EnableViewState="false" />
                                    &nbsp;
                                    <asp:DropDownList ID="_ddlOwner" Width="200" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ValidationGroup="EnterMOC" ID="_rfvOwner" runat="server"
                                        Display="none" ControlToValidate="_ddlOwner" ErrorMessage="<%$RIResources:Shared,SelectMOCOwner %>"
                                        EnableClientScript="true" SetFocusOnError="true" Text="<%$RIResources:Shared,SelectMOCOwner %>"></asp:RequiredFieldValidator>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <IP:JQDate runat="server" id="_tbMOCImplementationDate" FromLabel='<%$RIResources:Shared,Implementation Date %>' Required="false"></IP:JQDate>
                                    <%--<asp:Label runat="server" ID="_lbImpDate" Width="35%" Visible="false" Text="<%$RIResources:MOC,Implementation Date%>"></asp:Label>&nbsp;&nbsp;
<%--                                    <asp:TextBox ID="_tbMOCImplementationDate" CssClass="PerformanceDateTimePicker" runat="server" Width="40%" Style="text-align: left">   </asp:TextBox>
                                    <br />--%>
                                    <asp:Label ID="_lbCompDate" runat="server" Width="175px" Text='<%$RIResources:Shared,Completion Date%>'
                                        EnableViewState="false" Visible="false" />
                                    <asp:TextBox ID="_tbCompDate" runat="server" Width="130px"
                                        Enabled="true" BackColor="lightGray" Visible="false"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ID="_tcStatus">
                                    <IP:ucMOCStatus runat="server" ID="MOCStatus" name="MOCStatus" DisplayMode="Enter" Visible="false" AllowPostBack="true" EnableValidation="false"  />
                                </asp:TableCell>

                            </asp:TableRow>
                            <asp:TableRow CssClass="Border">
                                <asp:TableCell VerticalAlign="top" Width="30%">
                                    <span class="ValidationError">*</span>
                                    <asp:textbox ID="_tbDivision"  runat="server" AutoPostBack="false"
                                        Width="90%" style="display: none;" >
                                    </asp:textbox>
                                    <asp:Label ID="_lblFacility" runat="server"
                                        Text="<%$RIResources:Shared,Facility %>" EnableViewState="false"></asp:Label><br />
                                    <asp:DropDownList ID="_ddlFacility" CausesValidation="true" runat="server" AutoPostBack="false"
                                        Width="90%" Enabled='<%# CBool(Eval("IsBlocked")) %>' onchange="updateDivision();">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ValidationGroup="EnterMOC" ID="_rfvFacility" runat="server"
                                        Display="none" ControlToValidate="_ddlFacility" ErrorMessage="<%$RIResources:Shared,SelectFacility %>"
                                        EnableClientScript="true" SetFocusOnError="true" Text="<%$RIResources:Shared,SelectFacility %>"></asp:RequiredFieldValidator>
                                </asp:TableCell>
                                <asp:TableCell VerticalAlign="top" Width="30%">
                                    <span class="ValidationError">*</span><asp:Label ID="_lblBusUnit" runat="server" Text="<%$RIResources:Shared,BusArea %>" EnableViewState="false"></asp:Label><br />
                                    <asp:DropDownList ID="_ddlBusinessUnit" CausesValidation="true" AutoPostBack="false"
                                        EnableViewState="false" Width="90%" onchange="updateFL();" Visible="true"
                                        runat="server" Enabled='<%# CBool(Eval("IsBlocked")) %>' />
                                    <asp:RequiredFieldValidator ValidationGroup="EnterMOC" ID="_rfvBusinessUnit" runat="server"
                                        Display="none" ControlToValidate="_ddlBusinessUnit" ErrorMessage="<%$RIResources:Shared,SelectBusinessUnit %>"
                                        EnableClientScript="true" SetFocusOnError="true" Text="<%$RIResources:Shared,SelectBusinessUnit %>"></asp:RequiredFieldValidator>
                                </asp:TableCell>
                                <asp:TableCell VerticalAlign="top" Width="30%">
                                    <asp:Label ID="_lblLine" runat="server" Text="<%$RIResources:Shared,LineLineBreak %>" EnableViewState="false"></asp:Label><br />
                                    <asp:DropDownList ID="_ddlLineBreak" CausesValidation="true" AutoPostBack="false"
                                        onchange="updateFL();" Width="98%" runat="server" Enabled='<%# CBool(Eval("IsBlocked")) %>' />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow CssClass="Border" ID="_FL" runat="server">
                    <asp:TableCell>
                        <div style="float: left">
                            <IP:FunctionalLocation id="_functionalLocationTree" runat="server" />
                        </div>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow CssClass="Border" ID="_trMOCType" runat="server">
                    <asp:TableCell>
                        <IP:MOCType id="_MOCType" runat="server" name="moctype" displaymode="Enter" autopostback="true" />
                        <span id="_divExpirationDate" style="display: none;" runat="server">
                            <asp:Label runat="server" ID="_lbExpirDate" Text="<%$RIResources:MOC,Expiration Date%>" Visible="false"></asp:Label>
                            <IP:JQDate runat="server" id="_tbExpirationDate" FromLabel='<%$RIResources:Shared,Expiration Date %>' Required="false"></IP:JQDate>
                            <asp:TextBox ID="_tbExpirationDate2" visible="false" CssClass="PerformanceDateTimePicker" runat="server" Width="15%" Style="text-align: left">   </asp:TextBox>
                        </span>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <asp:Table ID="_tbDetail" runat="server" BorderWidth="0" CellPadding="2" CellSpacing="0"
                            BackColor="white" Style="width: 100%; overflow: hidden;">

                            <asp:TableRow CssClass="Border">
                                <asp:TableCell ID="_tcTitle" runat="server" Width="45%" ColumnSpan="2">
                                    <span class="ValidationError">*</span>
                                    <asp:Label ID="_lblTitle" runat="server" Text="<%$RIResources:MOC,Title %>" EnableViewState="false" />&nbsp;
                            <asp:TextBox ID="_txtTitle" runat="server" Width="85%" OnTextChanged="FieldChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="EnterMOC" ID="_rfvTitle" runat="server"
                                        Display="none" ControlToValidate="_txtTitle" ErrorMessage="<%$RIResources:Shared,EnterTitle %>"
                                        EnableClientScript="true" SetFocusOnError="true" Text="<%$RIResources:Shared,EnterTitle %>"></asp:RequiredFieldValidator>
                                </asp:TableCell>
                                <asp:TableCell Width="15%" ColumnSpan="2">
                                    <asp:Panel ID="Panel1" runat="server" Visible="true">
                                        <asp:Label ID="_lbWorkOrder" runat="server" Text="<%$RIResources:MOC,Work Order %>" />&nbsp;
                                        <asp:TextBox ID="_txtWorkOrder" runat="server" EnableViewState="false" MaxLength="10" />&nbsp;
                                    </asp:Panel>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="_trDesc" runat="server" CssClass="Border">
                                <asp:TableCell HorizontalAlign="left" ColumnSpan="5">
                                    <asp:Label ID="_lblDescription" runat="server" Text="<%$RIResources:MOC,DescJustification %>"
                                        EnableViewState="false" />
                                    <div>
                                        <IP:AdvancedTextBox id="_txtDescription" runat="server" expandheight="true" width="98%"
                                            rows="2" textmode="MultiLine" maxlength="4000" style="width: 98%; font-size: 12px; color: Black; font-family: Verdana;"
                                            ontextchanged="FieldChanged" />
                                    </div>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="_trImpact" runat="server" CssClass="Border" Style="width: 99%;">
                                <asp:TableCell Width="60%" HorizontalAlign="left" VerticalAlign="Bottom">
                                    <asp:Label ID="_lblImpact" runat="server" Text="<%$RIResources:MOC,PotentialImpact %>"
                                        EnableViewState="false" />
                                    <br />
                                    <IP:AdvancedTextBox id="_txtImpact" runat="server" expandheight="true" width="98%"
                                        rows="2" textmode="MultiLine" maxlength="1000" style="width: 98%; font-size: 12px; color: Black; font-family: Verdana;" />
                                </asp:TableCell>
                                <asp:TableCell HorizontalAlign="left" Width="15%" VerticalAlign="Top" ColumnSpan="2">
                                    <asp:Label ID="_lblMOCSavings" runat="server" EnableViewState="false" Text="<%$RIResources:MOC,EstSavings %>"></asp:Label>
                                    <br />
                                    <b>$</b><asp:TextBox ID="_txtSavings" MaxLength="10" runat="server"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="_feSavings" runat="server" TargetControlID="_txtSavings"
                                        FilterType="custom" ValidChars="()-1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                                </asp:TableCell>
                                <asp:TableCell HorizontalAlign="left" Width="15%" VerticalAlign="Top" ColumnSpan="2">
                                    <asp:Label ID="_lblMOCCosts" runat="server" EnableViewState="false" Text="<%$RIResources:MOC,EstCosts %>"></asp:Label>
                                    <br />
                                    <b>$</b><asp:TextBox ID="_tbCosts" MaxLength="10" runat="server" OnTextChanged="FieldChanged"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="_feCosts" runat="server" TargetControlID="_tbCosts"
                                        FilterType="custom" ValidChars="()-1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Table ID="_tblCommentHeader" runat="Server" CssClass="Border">
                            <asp:TableRow CssClass="Header">
                                <asp:TableCell HorizontalAlign="left">
                                    <asp:Label ID="_lblComment" runat="server" EnableViewState="false" SkinID="LabelWhite" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>

                        <ajaxToolkit:CollapsiblePanelExtender ID="_cpeComment" runat="Server" TargetControlID="_pnlComments"
                            Collapsed="False" CollapseControlID="_lblComment" ExpandControlID="_lblComment"
                            SuppressPostBack="True" TextLabelID="_lblComment" CollapsedText="<%$RIResources:Shared,ShowComments %>"
                            ExpandedText="<%$RIResources:Shared,HideComments %>" ScrollContents="false" />

                        <asp:Panel ID="_pnlComments" runat="server" Width="100%" HorizontalAlign="Left">
                            <asp:Table ID="_tblComments" runat="Server">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="left">
                                        <asp:GridView CssClass="Border"
                                            ID="_gvComments" runat="server" AutoGenerateColumns="False"
                                            ShowFooter="False" DataKeyNames="USERNAME">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="40%" HeaderText="<%$RIResources:Shared,Comments %>"
                                                    HeaderStyle-Font-Underline="true">
                                                    <ItemTemplate>
                                                        <IP:AdvancedTextBox ID="_txtComments2" runat="server" expandheight="true" text='<%# Bind("comments") %>'
                                                            readonly="True" enabled="True" width="95%" style="font-size: 12px; color: Black; font-family: Verdana;" textmode="MultiLine" maxlength="2000" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:MOC,Date %>"
                                                    HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
                                                    <ItemTemplate>
                                                        <center>
                                <asp:Label ID="_lbCommentDate" runat="server" Text='<%# Bind("lastupdatedate") %>' EnableViewState="false"></asp:Label></center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:MOC,Created By %>"
                                                    HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
                                                    <ItemTemplate>
                                                        <center>
                                <asp:Label ID="_lbCommentUsername" runat="server" Text='<%# Bind("username") %>' EnableViewState="false"></asp:Label></center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow CssClass="Border">
                                    <asp:TableCell HorizontalAlign="left" ColumnSpan="3">
                                        <asp:Label ID="_lblNewComment" runat="server" Text="<%$RIResources:Shared,Comments%>"
                                            EnableViewState="false" />&nbsp;
                <IP:AdvancedTextBox id="_txtCommentsNew" runat="server" expandheight="true"
                    textmode="MultiLine" maxlength="2000" style="width: 90%; font-size: 12px; color: Black; font-family: Verdana;" />
                                        <br />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>


                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Table ID="_tblClassification" runat="server" CellPadding="2" CellSpacing="0"
                            BackColor="white" Style="width: 100%" EnableViewState="true">
                            <asp:TableRow CssClass="Border">
                                <asp:TableCell ColumnSpan="3" Width="100%" Wrap="false">
                                    <IP:MOCClass id="_MOCClass" runat="server" displaymode="Enter" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow CssClass="Header">
                    <asp:TableCell HorizontalAlign="left">
                        <a id="ClassQuestions" href="#ClassQuestions"></a>
                        <asp:Label ID="_lblClassQuestions" runat="server" Text="<%$RIResources:MOC,Classification Questions%>" EnableViewState="false" SkinID="LabelWhite" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <ajaxToolkit:CollapsiblePanelExtender ID="_cpeClassQuestions" runat="Server" TargetControlID="_pnlClassQuestions"
                Collapsed="False" CollapseControlID="_lblClassQuestions" ExpandControlID="_lblClassQuestions"
                SuppressPostBack="True" TextLabelID="_lblClassQuestions" CollapsedText="<%$RIResources:Shared,Show Classification Questions %>"
                ExpandedText="<%$RIResources:Shared,Hide Classification Questions %>" ScrollContents="false" />

            <asp:Panel ID="_pnlClassQuestions" runat="server" Width="100%" HorizontalAlign="Left">
                <asp:Table ID="_tblClassQuestions" runat="Server" BackColor="white">
                    <asp:TableRow ID="_trClassQuestions">
                        <asp:TableCell>
                            <RWG:BulkEditGridView ID="_gvClassQuestions" runat="server" Width="100%" CssClass="Border" BorderColor="Black" BorderWidth="2px" AutoGenerateColumns="False"
                                ShowFooter="False" DataKeyNames="mocquestion_seqid" ShowHeader="false" BackColor="Silver">
                                <Columns>
                                    <asp:TemplateField ShowHeader="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="50%" HeaderText="Question" HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <asp:Label ID="_lblTitle" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="30%">
                                        <EditItemTemplate>
                                            <asp:CheckBoxList ID="_cbAnswer" runat="server" RepeatDirection="Horizontal" onclick="CheckBoxToRadio(this,2)" OnSelectedIndexChanged="_gvClassQuestions.HandleRowChanged" Visible='<%# Container.DataItem("yesnoquestion")%>'>
                                                <asp:ListItem Text="<%$RIResources:Shared,Yes %>" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="<%$RIResources:Shared,No %>" Value="N"></asp:ListItem>
                                            </asp:CheckBoxList>
                                            <IP:AdvancedTextBox id="_tbAnswer" runat="server" expandheight="true" text='<%# Bind("answer")%>' rows="1"
                                                width="95%" textmode="MultiLine" maxlength="2000" style="font-size: 12px; color: Black; font-family: Verdana;" Visible='<%# Container.DataItem("textquestion")%>' ontextChanged="_gvClassQuestions.HandleRowChanged" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <%--                                <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:MOC,Answered Date %>"
                                    HeaderStyle-Font-Underline="true">
                                    <ItemTemplate>
                                        <asp:Label ID="_lblClassAnswerDate" runat="server" Text='<%#RI.SharedFunctions.CleanDate(Eval("updatedate"),dateformat.ShortDate) %>' EnableViewState="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                </Columns>
                            </RWG:BulkEditGridView>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>
            <asp:Table runat="server">
                <asp:TableRow ID="_trCat" runat="server" CssClass="Border">
                    <asp:TableCell ColumnSpan="3" Width="100%" Wrap="false" BorderColor="black" BorderWidth="1">
                        <IP:MOCCategory id="_MOCCat" runat="server" displaymode="Search"/>
                        <asp:CustomValidator ID="_catVal" runat="server"
                            ClientValidationFunction="ValidateMarketChannel" ValidationGroup="EnterMOC"
                            ErrorMessage="" Text="<%$RIResources:Shared,ValidateRequiredFields%>" Display="Dynamic"></asp:CustomValidator>
                     
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow CssClass="Header">
                    <asp:TableCell HorizontalAlign="left">
                        <a id="CatQuestions" href="#CatQuestions"></a>
                        <asp:Label ID="_lblCatQuestions" runat="server" Text="<%$RIResources:MOC,Category Questions%>" EnableViewState="false" SkinID="LabelWhite" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <ajaxToolkit:CollapsiblePanelExtender ID="_cpeCatQuestions" runat="Server" TargetControlID="_pnlCatQuestions"
                Collapsed="False" CollapseControlID="_lblCatQuestions" ExpandControlID="_lblCatQuestions"
                SuppressPostBack="True" TextLabelID="_lblCatQuestions" CollapsedText="<%$RIResources:Shared,Show Category Questions %>"
                ExpandedText="<%$RIResources:Shared,Hide Category Questions %>" ScrollContents="false" />

            <asp:Panel ID="_pnlCatQuestions" runat="server" Width="100%" HorizontalAlign="Left">
                <asp:Table ID="_tblCatQuestions" runat="Server" BackColor="white">
                    <asp:TableRow ID="_trCatQuestions">
                        <asp:TableCell>
                            <RWG:BulkEditGridView ID="_gvCatQuestions" runat="server" Width="100%" CssClass="Border" BorderColor="Black" BorderWidth="2px" AutoGenerateColumns="False"
                                ShowFooter="False" DataKeyNames="mocquestion_seqid" ShowHeader="false" BackColor="Silver">
                                <Columns>
                                    <asp:TemplateField ShowHeader="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="50%" HeaderText="Question" HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <asp:Label ID="_lblTitle" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="30%">
                                        <EditItemTemplate>
                                            <asp:CheckBoxList ID="_cbAnswer" runat="server" RepeatDirection="Horizontal" onclick="CheckBoxToRadio(this,2)" OnSelectedIndexChanged="_gvCatQuestions.HandleRowChanged" Visible='<%# Container.DataItem("yesnoquestion")%>'>
                                                <asp:ListItem Text="<%$RIResources:Shared,Yes %>" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="<%$RIResources:Shared,No %>" Value="N"></asp:ListItem>
                                            </asp:CheckBoxList>
                                            <IP:AdvancedTextBox id="_tbAnswer" runat="server" expandheight="true" text='<%# Bind("answer")%>' rows="1"
                                                width="95%" textmode="MultiLine" maxlength="2000" style="font-size: 12px; color: Black; font-family: Verdana;" Visible='<%# Container.DataItem("textquestion")%>' ontextChanged="_gvCatQuestions.HandleRowChanged" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </RWG:BulkEditGridView>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>
            <asp:Panel ID="_pnlApprovals" runat="server" Visible="false">
                <div style="width: 100%; text-align: left; color: White;" class="Header">
                    <asp:Button ID="_btnShowAddApprover" runat="server" Text="<%$RIResources:MOC,Add Approvers %>" OnClientClick="$find('bePopup').show();return false;" />
                    <%--            <asp:Button ID="Button1" runat="server" Text="Add Approvers" OnClientClick="$find('bePopup').show();GetEmployee('" & me._ddlApproverFacilityNew & "','me._slbApprovalNotificationList.AvailableListID');return false;" />
                    --%>
                    <asp:HyperLink ID="_hypApproval" CssClass="LabelLink" runat="server"
                        Text='Workflow' ImageUrl="../Images/question.gif"></asp:HyperLink><br />
                    <a id="Approval" href="#Approval"></a>
                </div>
                <asp:Table ID="_tblApprovers" runat="Server" BackColor="white">
                    <asp:TableRow CssClass="Header" ID="_trL1Header">
                        <asp:TableCell HorizontalAlign="left">
                            <asp:Label ID="_lblL1Approvers" runat="server" Text="<%$RIResources:MOC,First Level Approvers%>" EnableViewState="false" SkinID="LabelWhite" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="_trL1Grid">
                        <asp:TableCell>
                            <asp:GridView ID="_gvApprovalsL1" runat="server" Width="100%" CssClass="Border" BorderColor="Black" BorderWidth="2px" AutoGenerateColumns="False"
                                ShowFooter="False" DataKeyNames="approval_seqid" EmptyDataText="<%$RIResources:MOC,No Approvers %>" EmptyDataRowStyle-BackColor="silver">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="8%" HeaderText="Role" HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="_lblRole" runat="server" Text='<%# Bind("roledescription") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField visible="False">
                        <ItemTemplate>
                            <asp:Label ID="_lblUnique" runat="server" Text='<%# Bind("uniqueUSERNAME") %>' ></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>--%>
                                    <asp:TemplateField ItemStyle-Width="15%" HeaderText="<%$RIResources:MOC,ApproverName %>"
                                        HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <asp:Label ID="_lblApproverL1Name" runat="server" Text='<%# Bind("personname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="_lblRoleUserNames" runat="server" Text='<%# Bind("roleusernames") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$RIResources:MOC,Approved %>" ItemStyle-Width="10%"
                                        HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <asp:CheckBoxList ID="_cbApproval" runat="server" RepeatDirection="Horizontal" onclick="CheckBoxToRadio(this,2)">
                                                <asp:ListItem Text="<%$RIResources:Shared,Yes %>" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="<%$RIResources:Shared,No %>" Value="N"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:MOC,ApprovalDate %>"
                                        HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <center>
                        <asp:Label ID="_lblApproverL1Date" runat="server" Text='<%#RI.SharedFunctions.CleanDate(Eval("approvedate"), DateFormat.ShortDate) %>' EnableViewState="false"></asp:Label>
                        </center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="55%" HeaderText="<%$RIResources:Shared,Comments %>"
                                        HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <center>
                        <IP:AdvancedTextBox id="_txtComments" runat="server" expandheight="true" text='<%# Bind("comments") %>'
                        width="95%" textmode="MultiLine" maxlength="2000" style="font-size: 12px; color: Black; font-family: Verdana;" />
                        </center>
                                            <asp:Label Font-Italic="true" ID="_lblResponse" runat="server" Text='<%# Bind("roleresponse") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$RIResources:Global,Required %>"
                                        HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <center>
                        <asp:Label ID="_lblRequired" runat="server" Text='<%# Bind("required_flag") %>' EnableViewState="false"></asp:Label>
                        </center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="_btnApproverDelete" CommandName="Delete" OnClick="FieldChanged" runat="server"
                                                Text="<%$RIResources:Global,Remove %>" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" ConfirmText="<%$RIResources:Shared,ConfirmDelete %>"
                                                TargetControlID="_btnApproverDelete"></ajaxToolkit:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow CssClass="Header" ID="_trL2Header">
                        <asp:TableCell HorizontalAlign="left">
                            <asp:Label ID="_lblL2Approvers" runat="server" Text="<%$RIResources:MOC,Second Level Approvers %>"
                                EnableViewState="false" SkinID="LabelWhite" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="_trL2Grid">
                        <asp:TableCell>
                            <asp:GridView Width="100%" CssClass="Border" BorderColor="Black" BorderWidth="2px"
                                ID="_gvApprovalsL2" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                ShowFooter="False" DataKeyNames="approval_seqid" EmptyDataText="<%$RIResources:MOC,No Approvers %>" EmptyDataRowStyle-BackColor="silver">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="15%" HeaderText="<%$RIResources:MOC,ApproverName %>"
                                        HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <asp:Label ID="_lblApproverL2Name" runat="server" Text='<%# Bind("personname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="_lblRoleUserNames" runat="server" Text='<%# Bind("roleusernames") %>' EnableViewState="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$RIResources:MOC,Approved %>" ItemStyle-Width="10%"
                                        HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <asp:CheckBoxList ID="_cbApprovalL2" runat="server" RepeatDirection="Horizontal" onclick="CheckBoxToRadio(this,2)"
                                                OnSelectedIndexChanged="FieldChanged">
                                                <asp:ListItem Text="<%$RIResources:Shared,Yes %>" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="<%$RIResources:Shared,No %>" Value="N"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:MOC,ApprovalDate %>"
                                        HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <center>
                                <asp:Label ID="_lblApproverL2Date" runat="server" Text='<%#RI.SharedFunctions.CleanDate(Eval("approvedate"), DateFormat.ShortDate) %>' EnableViewState="false"></asp:Label></center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="55%" HeaderText="<%$RIResources:Shared,Comments %>"
                                        HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <center>
                                <IP:advancedtextbox id="_txtCommentsL2" runat="server" expandheight="true" text='<%# Bind("comments") %>'
                                    width="95%" textmode="MultiLine" maxlength="2000" style="font-size: 12px; color: Black; font-family: Verdana;" /></center>
                                            <asp:Label Font-Italic="true" ID="_lblResponseL2" runat="server" Text='<%# Bind("roleresponse") %>' EnableViewState="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$RIResources:Global,Required %>"
                                        HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <center>
                                <asp:Label ID="_lblRequiredL2" runat="server" Text='<%# Bind("required_flag") %>' EnableViewState="false"></asp:Label></center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="_btnApproverL2Delete" CommandName="Delete" OnClick="FieldChanged" runat="server"
                                                Text="<%$RIResources:Global,Remove %>" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" ConfirmText="<%$RIResources:Shared,ConfirmDelete %>"
                                                TargetControlID="_btnApproverL2Delete"></ajaxToolkit:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow CssClass="Header" ID="_trL3Header">
                        <asp:TableCell HorizontalAlign="left">
                            <asp:Label ID="_lblL3Approvers" runat="server" Text="<%$RIResources:MOC,Third Level Approvers %>"
                                EnableViewState="false" SkinID="LabelWhite" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="_trL3Grid">
                        <asp:TableCell>
                            <asp:GridView Width="100%" CssClass="Border" BorderColor="Black" BorderWidth="2px"
                                ID="_gvApprovalsL3" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                ShowFooter="False" DataKeyNames="approval_seqid" EmptyDataText="<%$RIResources:MOC,No Approvers %>" EmptyDataRowStyle-BackColor="silver">
                                <Columns>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="_lblRoleUserNames" runat="server" Text='<%# Bind("roleusernames") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" HeaderText="<%$RIResources:MOC,ApproverName %>"
                                        HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <asp:Label ID="_lblApprovername" runat="server" Text='<%# Bind("personname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$RIResources:MOC,Approved %>" ItemStyle-Width="10%"
                                        HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <asp:CheckBoxList ID="_cbApprovalL3" runat="server" RepeatDirection="Horizontal" onclick="CheckBoxToRadio(this,2)"
                                                OnSelectedIndexChanged="FieldChanged">
                                                <asp:ListItem Text="<%$RIResources:Shared,Yes %>" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="<%$RIResources:Shared,No %>" Value="N"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:MOC,ApprovalDate %>"
                                        HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <center>
                                <asp:Label ID="_lblApproverL3Date" runat="server" Text='<%#RI.SharedFunctions.CleanDate(Eval("approvedate"), DateFormat.ShortDate) %>' EnableViewState="false"></asp:Label></center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="55%" HeaderText="<%$RIResources:Shared,Comments %>"
                                        HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <center>
                                <IP:AdvancedTextBox id="_txtCommentsL3" runat="server" expandheight="true" text='<%# Bind("comments") %>'
                                    width="95%" textmode="MultiLine" maxlength="2000" style="font-size: 12px; color: Black; font-family: Verdana;" /></center>
                                            <asp:Label Font-Italic="true" ID="_lblResponseL3" runat="server" Text='<%# Bind("roleresponse") %>' EnableViewState="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$RIResources:Global,Required %>"
                                        HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <center>
                                <asp:Label ID="_lblRequiredL3" runat="server" Text='<%# Bind("required_flag") %>' EnableViewState="false"></asp:Label></center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="_btnApproverL3Delete" CommandName="Delete" OnClick="FieldChanged" runat="server"
                                                Text="<%$RIResources:Global,Remove %>" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" ConfirmText="<%$RIResources:Shared,ConfirmDelete %>"
                                                TargetControlID="_btnApproverL3Delete"></ajaxToolkit:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow CssClass="Header" ID="_trInformedHeader">
                        <asp:TableCell HorizontalAlign="left">
                            <asp:Label ID="_lblInformed" runat="server" Text="<%$RIResources:MOC,Informed %>" EnableViewState="false" SkinID="LabelWhite" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="_trInformedGrid">
                        <asp:TableCell>
                            <asp:GridView Width="100%" CssClass="Border" BorderColor="Black" BorderWidth="2px"
                                ID="_gvInformed" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                ShowFooter="False" DataKeyNames="approval_seqid" EmptyDataText="<%$RIResources:MOC,No Informed %>" EmptyDataRowStyle-BackColor="silver">
                                <Columns>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="_lblUnique" runat="server" Text='<%# Bind("uniqueUSERNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="_lblRoleUserNames" runat="server" Text='<%# Bind("roleusernames") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" HeaderText="<%$RIResources:MOC,Informed Name %>"
                                        HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <asp:Label ID="_lblInformed" runat="server" Text='<%# Bind("personname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$RIResources:MOC,Reviewed %>" ItemStyle-Width="10%"
                                        HeaderStyle-Font-Underline="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="_cbReview" runat="server" Text="<%$RIResources:Shared,Reviewed %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:MOC,Review Date %>"
                                        HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <center>
                                <asp:Label ID="_lbReviewDate" runat="server" Text='<%#RI.SharedFunctions.CleanDate(Eval("approvedate"), DateFormat.ShortDate) %>' EnableViewState="false"></asp:Label></center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="55%" HeaderText="<%$RIResources:Shared,Comments %>"
                                        HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <center>
                                <IP:AdvancedTextBox id="_txtCommentsE" runat="server" expandheight="true" text='<%# Bind("comments") %>'
                                    width="95%" textmode="MultiLine" maxlength="2000" style="font-size: 12px; color: Black; font-family: Verdana;" /></center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$RIResources:Shared,Required %>" HeaderStyle-Font-Underline="true">
                                        <ItemTemplate>
                                            <center> <asp:Label ID="_lblRequired" runat="server" Text='<%# Bind("required_flag") %>'></asp:Label></center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="_btnInformedDelete" CommandName="Delete" OnClick="FieldChanged" runat="server"
                                                Text="<%$RIResources:Global,Remove %>" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" ConfirmText="<%$RIResources:Shared,ConfirmDelete %>"
                                                TargetControlID="_btnInformedDelete"></ajaxToolkit:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>
            <asp:Table ID="_tblChecklist" runat="server" CellPadding="2" CellSpacing="0" BackColor="white" Style="width: 100%" EnableViewState="false">
                <asp:TableRow CssClass="Header">
                    <asp:TableCell HorizontalAlign="left">
                        <asp:Label ID="_lblChecklist" Style="cursor: hand;" runat="server" SkinID="LabelWhite"
                            Width="200" EnableViewState="false"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <ajaxToolkit:CollapsiblePanelExtender ID="_cpeChecklist" runat="Server" TargetControlID="_pnlChecklist"
                Collapsed="False" CollapseControlID="_lblChecklist" ExpandControlID="_lblChecklist"
                SuppressPostBack="True" TextLabelID="_lblChecklist" CollapsedText="<%$RIResources:MOC,ShowChecklist %>"
                ExpandedText="<%$RIResources:MOC,HideChecklist %>" ScrollContents="false" />

            <asp:Panel ID="_pnlChecklist" runat="server" Width="100%" HorizontalAlign="Left" Visible="true">
                <asp:Label ID="_lblSystemTitle" runat="server" Text="<%$RIResources:MOC,SystemChanges %>" EnableViewState="false"></asp:Label><asp:HyperLink ID="_hypSystemDefinition" CssClass="LabelLink" runat="server"
                    ImageUrl="../Images/question.gif"></asp:HyperLink><br />
                <asp:DataList ID="_dlSystem" runat="server" Width="100%" CssClass="Border" RepeatLayout="Table"
                    RepeatColumns="3" RepeatDirection="Horizontal" ItemStyle-Font-Strikeout="false"
                    GridLines="Both" ItemStyle-Width="30%" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <asp:Label ID="_lblSystem" runat="server" Visible="true" Text='<%# RI.SharedFunctions.LocalizeValue(DataBinder.Eval(Container.DataItem, "MOCSystem")) %>'></asp:Label><asp:Label ID="_lblSystemSeq" Visible="false" runat="server" Text='<%# Bind("mocsystem_seq_id") %>'></asp:Label><asp:HiddenField ID="_hdftaskitem" Visible="false" runat="server"></asp:HiddenField>
                        <asp:CheckBoxList ID="_cblSystem" runat="server" RepeatDirection="Horizontal" onclick="CheckBoxToRadio(this,2)"
                            OnSelectedIndexChanged="SystemChanged">
                            <asp:ListItem Text="<%$RIResources:Shared,Yes %>" Value="Y"></asp:ListItem>
                            <asp:ListItem Text="<%$RIResources:Shared,No %>" Value="N" Selected="True"></asp:ListItem>
                        </asp:CheckBoxList><asp:Table ID="_tblSystemDetail" runat="server">
                            <asp:TableRow>
                                <asp:TableCell Width="60%">
                                    <asp:DropDownList ID="_ddlSystemFacility" runat="server" Width="100%"></asp:DropDownList>
                                </asp:TableCell><asp:TableCell>
                                    <asp:DropDownList ID="_ddlSystemPerson" runat="server"></asp:DropDownList>
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="EnterMOC"
                                        ClientValidationFunction="ValidatePerson" ErrorMessage="" Text="<%$RIResources:Shared,ValidateRequiredFields%>" Display="Dynamic">
                                    </asp:CustomValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="_lblPriority" runat="server" Text="<%$RIResources:Shared,Priority %>" EnableViewState="false"></asp:Label>&nbsp;&nbsp;
                                    <asp:DropDownList ID="_ddlPriority" runat="server">
                                        <asp:ListItem Value="1" Text="<%$RIResources:Shared,Low %>"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="<%$RIResources:Shared,Medium %>"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="<%$RIResources:Shared,High %>"></asp:ListItem>
                                    </asp:DropDownList>
                                </asp:TableCell><asp:TableCell>
                                    <asp:Label ID="_lblDaysAfter" runat="server" Text="<%$RIResources:Shared,Days after Approval %>" EnableViewState="false"></asp:Label>&nbsp;
                                    <asp:TextBox ID="_txtDaysAfter" runat="server" Width="20"></asp:TextBox>&nbsp;
                                    <asp:Label ID="_lblStatus" runat="server" Text="" EnableViewState="false"></asp:Label>&nbsp;
                                    <ajaxToolkit:FilteredTextBoxExtender ID="_feDaysAfter" runat="server" TargetControlID="_txtDaysAfter"
                                        FilterType="custom" ValidChars="1234567890/" Enabled="true"></ajaxToolkit:FilteredTextBoxExtender>
                                    <br />
                                    <asp:CustomValidator ID="_systemVal" runat="server"
                                        ClientValidationFunction="ValidateDaysAfter" ValidationGroup="EnterMOC"
                                        ErrorMessage="" Text="<%$RIResources:Shared,ValidateRequiredFields%>" Display="Dynamic"></asp:CustomValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="2">
                                    <asp:Label ID="_lbSysTitle" runat="server" Text="<%$RIResources:Shared,Title %>" EnableViewState="false"></asp:Label>&nbsp;
                                    <IP:AdvancedTextBox id="_tbSysTitle" runat="server" expandheight="true" rows="1"  
                                    width="75%" textmode="MultiLine" maxlength="90" style="font-size: 12px; color: Black; font-family: Verdana;" /></center>
                                </asp:TableCell>
                            </asp:TableRow>

                        </asp:Table>
                    </ItemTemplate>
                </asp:DataList>
            </asp:Panel>

            <asp:Table ID="_tblTempTaskTitle" runat="server" CellPadding="2" CellSpacing="0"
                BackColor="white" Style="width: 100%" EnableViewState="true" Visible="false">
                <asp:TableRow CssClass="Header">
                    <asp:TableCell HorizontalAlign="left">
                        <asp:Label ID="_lbPendingTasks" runat="server" Text="<%$RIResources:MOC,Pending Template Tasks %>" SkinID="LabelWhite" EnableViewState="false"></asp:Label>
                        <asp:HyperLink ID="_hypMOCTemplateTasks" CssClass="LabelLink" runat="server" ImageUrl="../Images/question.gif"></asp:HyperLink><br />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Table ID="_tblTempTasks" runat="Server" Style="width: 100%" Visible="false">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="left">
                        <asp:GridView CssClass="Border" BorderColor="Black" BorderWidth="2px" Width="100%"
                            ShowFooter="False" ID="_gvPendingTemplateTasks" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="taskitemseqid" EmptyDataText="<%$RIResources:MOC,No Pending Template Tasks%>" EmptyDataRowStyle-BackColor="silver">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="60%" HeaderText="<%$RIResources:Shared,Title/Description %>"
                                    HeaderStyle-Font-Underline="true">
                                    <ItemTemplate>
                                        <asp:Label ID="_lbTitle" runat="server" Text='<%# Bind("title") %>' EnableViewState="false"></asp:Label></center>
                        <IP:AdvancedTextBox ID="_tbDescription" runat="server" expandheight="true" text='<%# Bind("description") %>'
                            readonly="True" enabled="True" width="95%" style="font-size: 12px; color: Black; font-family: Verdana;" textmode="MultiLine" maxlength="4000" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:Shared,Responsible %>"
                                    HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <center>
                            <asp:Label ID="_lbResponsible" runat="server" Text='<%# Bind("responsible") %>' EnableViewState="false"></asp:Label></center>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:Shared, Days After Approval %>"
                                    HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <center>
                            <asp:Label ID="_lbDaysAfter" runat="server" Text='<%# Bind("daysafter") %>' EnableViewState="false"></asp:Label></center>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <br />
            <asp:Table ID="_tblUpdate" runat="server" BorderWidth="0" CellPadding="2" CellSpacing="2" BackColor="white" Style="width: 99%" Visible="false">
                <asp:TableRow CssClass="Border">
                    <asp:TableCell Width="25%">
                        <asp:Label ID="_lblCreatedBy" runat="server" EnableViewState="false"></asp:Label>
                    </asp:TableCell><asp:TableCell Width="25%">
                        <asp:Label ID="_lblCreatedDate" runat="server" EnableViewState="false"></asp:Label>
                    </asp:TableCell><asp:TableCell Width="25%">
                        <asp:Label ID="_lblUpdatedBy" runat="server" EnableViewState="false"></asp:Label>
                    </asp:TableCell><asp:TableCell Width="25%">
                        <asp:Label ID="_lblLastUpdateDate" runat="server" EnableViewState="false"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>



            <center>
            <IP:SpellCheck id="_btnSpell" runat="server" ControlIdsToCheck="_txtTitle,_txtDescription,_txtImpact" />
            <asp:Button ID="_btnSubmit" clientidmode="static" Text="<%$RIResources:MOC,Submit %>" runat="server" ValidationGroup="EnterMOC" Font-Size="Large" OnClientClick="if(Page_ClientValidate('EnterMOC')) return ShowModalPopup();"></asp:Button>
            <asp:Button ID="_btnInitiate" Text="<%$RIResources:MOC,Submit MOC %>" runat="server" ValidationGroup="EnterMOC" Visible="false" Font-Size="Large" />
            <asp:Button ID="_btnDelete" runat="server" Text="<%$RIResources:Shared,Delete %>" />
            </center>
            <ajaxToolkit:ConfirmButtonExtender ID="_cbeDeletePage" runat="server" ConfirmText="<%$RIResources:Shared,ConfirmDelete %>"
                TargetControlID="_btnDelete"></ajaxToolkit:ConfirmButtonExtender>

            <asp:ValidationSummary ID="_vsSummary" runat="server" CssClass="ValidationError"
                DisplayMode="BulletList" ValidationGroup="EnterMOC" HeaderText="<%$RIResources:Shared,RequiredFields %>"
                ShowSummary="false" EnableClientScript="true" ShowMessageBox="false" />

            <asp:Panel ID="_pnlUpdateButtons" runat="server" HorizontalAlign="center" Visible="false" BorderWidth="0">
                <asp:Button ID="_btnAttachment" runat="server" Text='<%$RIResources:Shared,Attachments %>'
                    ValidationGroup="EnterMOC" />
                <asp:Button ID="_btnMOCActionItems" runat="server" Text='<%$RIResources:Shared,Task Items %>'
                    ValidationGroup="EnterMOC" />
                <asp:Button ID="_btnDetailReport" runat="server" Text="<%$RIResources:MOC,MOCSummary %>" />
            </asp:Panel>
            <br />




            <ajaxToolkit:ModalPopupExtender ID="_mpeSwapList" runat="server" TargetControlID="_btnShowList"
                PopupControlID="_pnlSwapListBox" CancelControlID="_btnDummy" BehaviorID="bePopup"
                BackgroundCssClass="modalBackground" DropShadow="true">
            </ajaxToolkit:ModalPopupExtender>

            <div style="display: none">
                <asp:Button ID="_btnShowList" runat="server" Text="Show List" />
                <asp:Button ID="_btnDummy" runat="server" Text="Cancel" Style="display: none" />
            </div>

            <asp:Panel ID="_pnlSwapListBox" runat="server" CssClass="modalPopup" Width="900px" Style="overflow: auto;" Height="625px">
                <asp:Label ID="_lblNewFacility" runat="server" Text="<%$RIResources:Shared,Facility %>" EnableViewState="false"></asp:Label><br />
                <asp:DropDownList ID="_ddlApproverFacilityNew" runat="server" Width="220px" />
                <IP:ucMOCSwapListBox ID="_slbApprovalNotificationList" runat="server" />
                <center>
                    <asp:Button ID="_btnSaveDraft" runat="server" Text="<%$RIResources:Shared,Save As Draft %>" onClick="_btnOkSaveApprovers_Click" />
                    <asp:Button ID="_btnOkSaveApprovers" runat="server" Text="<%$RIResources:Shared,Submit MOC %>" Font-Size="Large"  />
                    <asp:Button ID="_btnCancel" runat="server" Text="<%$RIResources:Shared,Cancel %>" OnClientClick="return HideApproverMP()" />
                </center>
            </asp:Panel>


            <ajaxToolkit:ModalPopupExtender ID="_mpeSelectTemplateTasks" runat="server"
                TargetControlID="Button1" PopupControlID="_pnlTemplateTasks"
                BackgroundCssClass="modalBackground"
                DropShadow="true" />

            <div style="display: none">
                <asp:Button ID="Button1" runat="server" Text="Show List" />
                <asp:Button ID="Button2" runat="server" Text="Cancel" Style="display: none" />
                <asp:Button ID="Button3" runat="server" Text="Cancel1" Style="display: none" />
            </div>

            <asp:Panel ID="_pnlTemplateTasks" runat="server" CssClass="modalPopup" Width="900px" >
                <asp:Table ID="Table1" runat="Server" Width="90%">
                    <asp:TableRow HorizontalAlign="Center">
                        <asp:TableCell>
                            <asp:Button ID="_btnCreateTasks" runat="server" Text="<%$RIResources:Shared, Create Tasks %>" />&nbsp;&nbsp;
                            <asp:Button ID="_btnCloseTasks" runat="server" Text="<%$RIResources:Shared, Close %>" Visible="false" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br />
                <asp:Table ID="_tblTemplateTasksDaysAfter" runat="Server" Width="90%">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell HorizontalAlign="center">
                            <asp:Literal ID="_ltDaysAfter" runat="server"
                                Text="<%$RIResources:Shared,MOC TEMPLATE TASK MESSAGE FOR TASKS WITH DAYS AFTER%>"></asp:Literal>
                        </asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="left">
                            <asp:GridView CssClass="Border" ID="_gvTemplateTasksDaysAfter" runat="server" AutoGenerateColumns="False"
                                ShowFooter="False" DataKeyNames="taskitemseqid" Width="100%">
                                <HeaderStyle CssClass="LockHeader" Font-Size="8" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:Shared,Create %>"
                                        ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="_hfTaskItemSeqID" runat="server" Value='<%# Bind("TaskItemSeqid") %>' />
                                            <asp:CheckBox ID="_cbCreate" runat="server" Checked="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="60%" HeaderText="<%$RIResources:Shared,Title/Description %>"
                                        ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <asp:Label ID="_lbTitle" runat="server" Text='<%# Bind("title") %>' EnableViewState="false"></asp:Label></center>
                            <IP:AdvancedTextBox ID="_tbDescription" runat="server" expandheight="true" text='<%# Bind("description") %>'
                                enabled="True" width="95%" style="font-size: 12px; color: Black; font-family: Verdana;" textmode="MultiLine" maxlength="4000" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:Shared,Responsible %>"
                                        HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <center>
                                <asp:Label ID="_lbResponsible" runat="server" Text='<%# Bind("responsibleusername") %>' Visible="false"></asp:Label></center>
                                            <IP:Responsible runat="server" ID="_ucNewResponsible" ResponsibleValue='<%# Bind("responsibleusername") %>' FacilityValue='<%# Bind("resproleplantcode") %>'></IP:Responsible>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="5%" HeaderStyle-Wrap="true" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <center>
                                <IP:MOCDate id="_ucMOCDate" runat="server" AllowManualDate=True DaysAfter='<%# Bind("daysafter") %>'></IP:MOCDate>                                </center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>


            <ajaxToolkit:ModalPopupExtender ID="_mpeTrialtoPerm" BehaviorID="BePopup2" runat="server"
                BackgroundCssClass="modalBackground" PopupControlID="pnlPopup" TargetControlID="lnkDummy">
            </ajaxToolkit:ModalPopupExtender>

            <div style="display: none">
                <asp:Button ID="lnkDummy" runat="server"></asp:Button>
                <asp:Button ID="Button4" runat="server"></asp:Button>
            </div>
            
            <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Width="650px" Style="display: none; overflow: auto;" Height="200px">
                <center>
                <br />
                <asp:Button ID="_btnSaveMOC" runat="server"  Style="display: none" Text="<%$RIResources:Shared,Move MOC to Permanent%>" />
                <asp:Button ID="_btnCopyMOC" runat="server" Style="display: none" Text="<%$RIResources:Shared,Copy MOC to Permanent and Resubmit for Approval%>"  />
                <asp:Button ID="_btnCancelPopup" runat="server" Text="Cancel" OnClientClick="return HideModalPopup()"/>
                </center>

            </asp:Panel>

            <IP:MessageBox id="_messageBox" runat="server" buttontype="OKCancel" />

            <div style="display:none;">
                <asp:Button ID="btnDummy2" runat="server" Text="Edit" Style="display: none;" /></div>
            <ajaxtoolkit:ModalPopupExtender ID="_mpeNotAuthorized" runat="server" TargetControlID="btnDummy2" PopupControlID="PnlModal" BackgroundCssClass="modalBackground">
            </ajaxtoolkit:ModalPopupExtender>
            <asp:Panel ID="PnlModal" runat="server" Width="600px" Height="100px" CssClass="modalPopup">
                    <br /><center>
                    <asp:label ID="lblAuth" runat="server" Text="You are not Authorized to view this MOC. Please contact your Facility Administrator. "></asp:label> <br /><br />
                    <asp:Button ID="_btnUnauthorized" runat="server" Text="OK" />

                          </center>
             </asp:Panel>

            
            <ajaxToolkit:ModalPopupExtender ID="_mpeMarketChannel" BehaviorID="MarketChannel" runat="server"
                BackgroundCssClass="modalBackground" PopupControlID="pnlMCPopup" TargetControlID="_btnMCDummy">
            </ajaxToolkit:ModalPopupExtender>

            <div style="display: none">
                <asp:Button ID="_btnMCDummy" runat="server"></asp:Button>
            </div>

            <asp:Panel ID="pnlMCPopup" runat="server" CssClass="modalPopup" Width="650px" Style="display: none; overflow: auto;" Height="150px">
                <center>
                <br />
                <asp:Label id="_lbMarketChannel" runat="server" text ="You must select a Market Channel for Product Trial MOC's"></asp:Label>
                    <br /><br />
                <asp:Button ID="_btnOk" runat="server" Text="OK" OnClientClick="return HideMCModalPopup()"/>
                </center>

            </asp:Panel>

        </ContentTemplate>

    </asp:UpdatePanel>


    <div id="_divSystemDefinition" class="modalPopup" style="display: none">
        <span style="text-align: left" class="ContentHeader">
            <asp:Literal ID="Literal1" runat="Server" Text="System"></asp:Literal></span><asp:Localize ID="_SystemDef" runat="server" Text="<%$ RIResources:Shared,SystemDefinition %>"></asp:Localize>
    </div>
    <div id="_divMOCDefinition" class="modalPopup" style="display: none">
        <span style="text-align: left" class="ContentHeader">
            <asp:Literal ID="Literal2" runat="Server" Text="System"></asp:Literal></span><asp:Localize ID="Localize1" runat="server" Text="<%$ RIResources:Shared,MOCTempTaskDefinition %>"></asp:Localize>
    </div>

</asp:Content>
