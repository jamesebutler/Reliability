<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="MOCDefaultApprovers.aspx.vb"
    Inherits="MOC_DefaultApprovers" Title="Management of Change" Trace="false" EnableViewState="true"
    EnableEventValidation="false"%>

<%@ MasterType VirtualPath="~/RI.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="_contentMain" ContentPlaceHolderID="_cphMain" runat="Server">
    
    <asp:UpdatePanel id="_udpLocation" runat="server" updatemode="Conditional">
    <ContentTemplate>

    <ajaxToolkit:CascadingDropDown id="_cddlFacility1" runat="server" category="SiteID"
        loadingtext="" prompttext="    " servicemethod="GetFacilityList" servicepath="~/CascadingLists.asmx"
        targetcontrolid="_ddlFacility" usecontextkey="true">
    </ajaxToolkit:CascadingDropDown>
    <ajaxToolkit:CascadingDropDown id="_cddlFacility" runat="server" category="SiteID"
        loadingtext="" prompttext="    " servicemethod="GetFacilityList" servicepath="~/CascadingLists.asmx"
        targetcontrolid="_ddlFacility2" usecontextkey="true">
    </ajaxToolkit:CascadingDropDown>
    <ajaxToolkit:CascadingDropDown id="_cddlBusUNit" runat="server" category="BusinessUnit"
        loadingtext="" prompttext="    " servicemethod="GetBusinessUnit" servicepath="~/CascadingLists.asmx"
        targetcontrolid="_ddlBusinessUnit" parentcontrolid="_ddlFacility">
    </ajaxToolkit:CascadingDropDown>
     <ajaxToolkit:CascadingDropDown id="_cddlArea" runat="server" category="Area"
        loadingtext="" prompttext="    " servicemethod="GetArea" servicepath="~/CascadingLists.asmx"
        targetcontrolid="_ddlArea" parentcontrolid="_ddlBusinessUnit">
    </ajaxToolkit:CascadingDropDown>
    <ajaxToolkit:CascadingDropDown id="_cddlLineBreak" runat="server" category="Line"
        loadingtext="" prompttext="    " servicemethod="GetLine" servicepath="~/CascadingLists.asmx"
        targetcontrolid="_ddlLineBreak" parentcontrolid="_ddlArea">
    </ajaxToolkit:CascadingDropDown>
    <ajaxToolkit:CascadingDropDown id="_cddlPeople" runat="server" category="Person"
        loadingtext="" prompttext="    " servicemethod="GetMTTPerson" servicepath="~/CascadingLists.asmx"
        targetcontrolid="_ddlPeople" parentcontrolid="_ddlFacility2">
    </ajaxToolkit:CascadingDropDown>

    <asp:Label runat="server" ID="_lblMainHeading"></asp:Label><br /><br />
    <asp:Label runat="server" ID="_lblHeading"></asp:Label>
    <asp:DropDownList ID="_ddlFacility" runat="server" AutoPostBack="true" Visible="true"></asp:DropDownList> 
    
    <asp:RadioButtonList ID="_rblMaintType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
        <asp:ListItem Value="Business Unit" Text="<%$RIResources:Shared,BusinessUnit %>" Selected="true"></asp:ListItem>
        <asp:ListItem Value="Classification" Text="<%$RIResources:Shared,Classification %>"></asp:ListItem>
        <asp:ListItem Value="Category" Text="<%$RIResources:Shared,Category %>"></asp:ListItem>
     </asp:RadioButtonList>   
        
    <asp:table runat="server" ID="_tblApprover" cellpadding="2" cellspacing="0" style="width: 100%">
        <asp:TableRow cssclass="Border">
            <asp:TableCell ID="_tcClass" verticalalign="top" Visible="false">
                <asp:Label ID="_lblClassification" runat="server" Text="<%$RIResources:Shared,Classification %>"></asp:Label><br />
                <asp:DropDownList ID="_ddlClass" runat="server" />
            </asp:TableCell>
            <asp:TableCell ID="_tcCategory" verticalalign="top" Visible="false">
                <asp:Label ID="_lblCategory" runat="server" Text="<%$RIResources:Shared,Category %>"></asp:Label><br />
                <asp:DropDownList ID="_ddlCategory" runat="server" />
            </asp:TableCell>
            <asp:TableCell verticalalign="top">
                <asp:Label ID="_lbl" runat="server" Text="<%$RIResources:MOC,AvailApprover %>"></asp:Label><br />
                <asp:DropDownList ID="_ddlFacility2" runat="server" Width="35%" > </asp:DropDownList>
                <asp:DropDownList ID="_ddlPeople" runat="server"></asp:DropDownList>
                <asp:HiddenField ID="_hfPeople" runat="server" />
            </asp:TableCell>
            <asp:TableCell ID="_tcBusinessUnit" verticalalign="top" Visible="true">
                <asp:Label ID="_lblBusinessUNit" runat="server" Text="<%$RIResources:Shared,BusinessUnit %>"></asp:Label><br />
                <asp:DropDownList ID="_ddlBusinessUnit" 
                     Width="90%" Visible="true" AutoPostBack="false"
                    runat="server" />
            </asp:TableCell>
            <asp:TableCell ID="_tcArea" verticalalign="top" Visible="true">
                <asp:Label ID="_lblArea" runat="server" Text="<%$RIResources:Shared,Area %>"></asp:Label><br />
                <asp:DropDownList ID="_ddlArea" CausesValidation="true" AutoPostBack="false"
                    runat="server" />
            </asp:TableCell>
            <asp:TableCell ID="_tcLine" verticalalign="top" Visible="true">
                <asp:Label ID="_lblLine" runat="server" Text="<%$RIResources:Shared,Line %>"></asp:Label><br />
                <asp:DropDownList ID="_ddlLineBreak" AutoPostBack   ="false"
                    runat="server" />
            </asp:TableCell>
            <asp:TableCell verticalalign="top"><asp:Label ID="Label1" runat="server" Text="<%$RIResources:MOC,Approval Level %>"></asp:Label><br />
                <asp:DropDownList runat="server" ID="_ddlApproval">
                    <asp:ListItem Value="L1" Text="<%$RIResources:MOC,Level1 %>"></asp:ListItem>
                    <asp:ListItem value="L2" Text="<%$RIResources:MOC,Level2 %>"></asp:ListItem>
                    <asp:ListItem value="L3" Text="<%$RIResources:MOC,Level3 %>"></asp:ListItem>
                    <asp:ListItem value="E" Text="<%$RIResources:Shared,Informed%>"></asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell> 
            <asp:TableCell Width="8%" verticalalign="top">
                <asp:Label ID="_lblRequired" runat="server" Text="<%$RIResources:Shared,Required %>"></asp:Label><br />
                <asp:CheckBox ID="_cbRequired" runat="server"/>
            </asp:TableCell>
            <asp:TableCell Width="8%">
                <asp:Button ID="_btnAdd" runat="server" Text="<%$RIResources:Shared,Add %>"/>
            </asp:TableCell>
        </asp:TableRow>        
    </asp:table>

    <br />
    <asp:GridView ID="_gvClass" runat="server" AutoGenerateColumns="False" Width="100%" align="Center"
        class="Border" DataKeyNames="classnotify_seqid" Visible="false" EmptyDataText="NO DEFAULT APPROVERS FOR CLASSIFICATIONS" >
        <Columns>
        <asp:BoundField DataField="MocClassification" HeaderText="<%$RIResources:Shared,Classification %>" />
        <asp:BoundField DataField="NotifyType" HeaderText="<%$RIResources:MOC,ApprovalLevel %>"/>
         <asp:BoundField DataField="Fullname" HeaderText="<%$RIResources:MOC,RolePerson %>" />
        <asp:TemplateField HeaderText="<%$RIResources:Shared,Required %>" >
            <ItemTemplate>
                <asp:CheckBox runat="Server" Enabled="false" checked='<%# Eval("required").ToString().Equals("Y") %>'/>
            </itemtemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="_btnApproverDelete" CommandName="Delete" runat="server" cssclass="button"
                    Text="<%$RIResources:Global,Delete %>" />
                <ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" confirmtext="<%$RIResources:Shared,ConfirmDelete %>"
                    TargetControlID="_btnApproverDelete">
                </ajaxToolkit:ConfirmButtonExtender>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns> 
    </asp:GridView>
    
    
    <asp:GridView ID="_gvCategory" runat="server" AutoGenerateColumns="False" Width="100%" align="Center"
          CssClass="Border" DataKeyNames="categorynotify_seqid" Visible="false" EmptyDataText="NO DEFAULT APPROVERS FOR CATEGORIES">
        <Columns>
        <asp:BoundField DataField="MocCategory" HeaderText="<%$RIResources:Shared,Category %>"    />
        <asp:BoundField DataField="NotifyType" HeaderText="<%$RIResources:MOC,ApprovalLevel %>" />
        <asp:BoundField DataField="Fullname" HeaderText="<%$RIResources:MOC,RolePerson %>" />
        <asp:TemplateField HeaderText="<%$RIResources:Shared,Required %>" >
            <ItemTemplate>
                <asp:CheckBox ID="_cbRequired" runat="Server" Enabled="false" checked='<%# Eval("required").ToString().Equals("Y") %>'/>
            </itemtemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="_btnCatDelete" CommandName="Delete" runat="server"
                    Text="<%$RIResources:Global,Delete %>" />
                <ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" confirmtext="<%$RIResources:Shared,ConfirmDelete %>"
                    TargetControlID="_btnCatDelete">
                </ajaxToolkit:ConfirmButtonExtender>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
    </asp:GridView>
    
    <br />
    <asp:Panel ID="_pnlBUA" runat="server">
    <asp:Table ID="_tblL1Header" runat="Server" CssClass="Border" >            
    <asp:TableRow CssClass="Header">
        <asp:TableCell HorizontalAlign="left">       
            <asp:Label ID="_lblL1Header" runat="server" EnableViewState="false" SkinID="LabelWhite" />
        </asp:TableCell>
    </asp:TableRow>
    </asp:Table>

    <ajaxToolkit:CollapsiblePanelExtender id="_cpeComment" runat="Server" targetcontrolid="_pnlL1Approver"
            collapsed="False" CollapseControlID="_lblL1Header" ExpandControlID="_lblL1Header"
            SuppressPostBack="True" TextLabelID="_lblL1Header" CollapsedText="<%$RIResources:Shared,Show L1 Approvers %>"
            expandedtext="<%$RIResources:Shared,Hide L1 Approvers %>" ScrollContents="false" />
    
    <asp:Panel ID="_pnlL1Approver" runat="server" Width="100%" HorizontalAlign="Left">
        <asp:Table ID="_tblL1Approver" runat="Server">            
            <asp:TableRow >
                <asp:TableCell HorizontalAlign="left">       
                <asp:GridView Width="100%" CssClass="Border" ID="_gvL1" runat="server" 
                    AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="notify_seqid" EmptyDataText="<%$RIResources:MOC,No Level 1 Approvers %>" ShowHeader="true">
                <EmptyDataRowStyle Font-Bold="true" />
            <Columns>
                <asp:BoundField DataField="FullName" HeaderText="<%$RIResources:MOC,Approver %>" HeaderStyle-Width="25%"     />
                <asp:BoundField DataField="risuperarea" HeaderText="<%$RIResources:Shared,BusinessUnit %>" HeaderStyle-Width="20%"  />
                <asp:BoundField DataField="subarea" HeaderText="<%$RIResources:Shared,Area %>" HeaderStyle-Width="20%"  />
                <asp:BoundField DataField="area" HeaderText="<%$RIResources:Shared,Line %>" HeaderStyle-Width="20%"  />
                <asp:TemplateField HeaderText="<%$RIResources:Shared,Required %>" >
                    <ItemTemplate>
                        <asp:CheckBox ID="_cbL1Required" runat="Server" Enabled="false" checked='<%# Eval("required").ToString().Equals("Y") %>'/>
                    </itemtemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="_btnApproverDelete" CommandName="Delete" runat="server" CssClass="Button"
                            Text="<%$RIResources:Global,Delete %>" />
                        <ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" confirmtext="<%$RIResources:Shared,ConfirmDelete %>"
                            TargetControlID="_btnApproverDelete">
                        </ajaxToolkit:ConfirmButtonExtender>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                </asp:GridView>            
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
    
    <asp:Table ID="_tblL2Header" runat="Server" CssClass="Border" >            
    <asp:TableRow CssClass="Header">
        <asp:TableCell HorizontalAlign="left">       
            <asp:Label ID="_lblL2Header" runat="server" EnableViewState="false" SkinID="LabelWhite" />
        </asp:TableCell>
    </asp:TableRow>
    </asp:Table>

    <ajaxToolkit:CollapsiblePanelExtender id="_cpeL2" runat="Server" targetcontrolid="_pnlL2Approver"
            collapsed="False" CollapseControlID="_lblL2Header" ExpandControlID="_lblL2Header"
            SuppressPostBack="True" TextLabelID="_lblL2Header" CollapsedText="<%$RIResources:Shared,Show L2 Approvers %>"
            expandedtext="<%$RIResources:Shared,Hide L2 Approvers %>" ScrollContents="false" />
        
    <asp:Panel ID="_pnlL2Approver" runat="server" Width="100%" HorizontalAlign="Left">
        <asp:Table ID="_tblL2Approver" runat="Server">            
            <asp:TableRow >
                <asp:TableCell HorizontalAlign="left">       
                <asp:GridView Width="100%" CssClass="Border" ID="_gvL2" runat="server" 
                    AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="notify_seqid" EmptyDataText="<%$RIResources:MOC,No Level 2 Approvers %>" ShowHeader="true">
                <EmptyDataRowStyle Font-Bold="true" />
            <Columns>
                <asp:BoundField DataField="FullName" HeaderText="<%$RIResources:MOC,Approver %>" HeaderStyle-Width="25%"     />
                <asp:BoundField DataField="risuperarea" HeaderText="<%$RIResources:Shared,BusinessUnit %>" HeaderStyle-Width="20%"  />
                <asp:BoundField DataField="subarea" HeaderText="<%$RIResources:Shared,Area %>" HeaderStyle-Width="20%"  />
                <asp:BoundField DataField="area" HeaderText="<%$RIResources:Shared,Line %>" HeaderStyle-Width="20%"  />
                <asp:TemplateField HeaderText="<%$RIResources:Shared,Required %>" >
                    <ItemTemplate>
                        <asp:CheckBox ID="_cbL2Required" runat="Server" Enabled="false" checked='<%# Eval("required").ToString().Equals("Y") %>'/>
                    </itemtemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="_btnApproverDelete" CommandName="Delete" runat="server" CssClass="Button"
                            Text="<%$RIResources:Global,Delete %>" />
                        <ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" confirmtext="<%$RIResources:Shared,ConfirmDelete %>"
                            TargetControlID="_btnApproverDelete">
                        </ajaxToolkit:ConfirmButtonExtender>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                </asp:GridView>            
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
    
    <asp:Table ID="_tblL3Header" runat="Server" CssClass="Border" >            
    <asp:TableRow CssClass="Header">
        <asp:TableCell HorizontalAlign="left">       
            <asp:Label ID="_lblL3Header" runat="server" EnableViewState="false" SkinID="LabelWhite" />
        </asp:TableCell>
    </asp:TableRow>
    </asp:Table>

    <ajaxToolkit:CollapsiblePanelExtender id="_cpeL3" runat="Server" targetcontrolid="_pnlL3Approver"
            collapsed="False" CollapseControlID="_lblL3Header" ExpandControlID="_lblL3Header"
            SuppressPostBack="True" TextLabelID="_lblL3Header" CollapsedText="<%$RIResources:Shared,Show L3 Approvers %>"
            expandedtext="<%$RIResources:Shared,Hide L3 Approvers %>" ScrollContents="false" />
        
    <asp:Panel ID="_pnlL3Approver" runat="server" Width="100%" HorizontalAlign="Left">
        <asp:Table ID="_tblL3Approver" runat="Server">            
            <asp:TableRow >
                <asp:TableCell HorizontalAlign="left">       
                <asp:GridView Width="100%" CssClass="Border" ID="_gvL3" runat="server" 
                    AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="notify_seqid" EmptyDataText="<%$RIResources:MOC,No Level 3 Approvers %>" ShowHeader="true">
                <EmptyDataRowStyle Font-Bold="true" />
            <Columns>
                <asp:BoundField DataField="FullName" HeaderText="<%$RIResources:MOC,Approver %>" HeaderStyle-Width="25%"     />
                <asp:BoundField DataField="risuperarea" HeaderText="<%$RIResources:Shared,BusinessUnit %>" HeaderStyle-Width="20%"  />
                <asp:BoundField DataField="subarea" HeaderText="<%$RIResources:Shared,Area %>" HeaderStyle-Width="20%"  />
                <asp:BoundField DataField="area" HeaderText="<%$RIResources:Shared,Line %>" HeaderStyle-Width="20%"  />
                <asp:TemplateField HeaderText="<%$RIResources:Shared,Required %>" >
                    <ItemTemplate>
                        <asp:CheckBox ID="_cbL3Required" runat="Server" Enabled="false" checked='<%# Eval("required").ToString().Equals("Y") %>'/>
                    </itemtemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="_btnApproverDelete" CommandName="Delete" runat="server" CssClass="Button"
                            Text="<%$RIResources:Global,Delete %>" />
                        <ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" confirmtext="<%$RIResources:Shared,ConfirmDelete %>"
                            TargetControlID="_btnApproverDelete">
                        </ajaxToolkit:ConfirmButtonExtender>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                </asp:GridView>            
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
    
<%--    <asp:GridView Width="100%" CssClass="Border" ID="_gvL1Approvers" runat="server" 
        AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="notify_seqid" EmptyDataText="<%$RIResources:MOC,No Level 1 Approvers %>" ShowHeader="true">
        <EmptyDataRowStyle Font-Bold="true" />
        <Columns>
        <asp:BoundField DataField="NOTIFYTYPE" HeaderText="<%$RIResources:MOC,Level 1 Approvers %>" HeaderStyle-Width="25%"     />
        <asp:BoundField DataField="FullName" HeaderText="<%$RIResources:MOC,Level 1 Approvers %>" HeaderStyle-Width="25%"     />
        <asp:BoundField DataField="risuperarea" HeaderText="<%$RIResources:Shared,BusinessUnit %>" HeaderStyle-Width="20%"  />
        <asp:BoundField DataField="subarea" HeaderText="<%$RIResources:Shared,Area %>" HeaderStyle-Width="20%"  />
        <asp:BoundField DataField="area" HeaderText="<%$RIResources:Shared,Line %>" HeaderStyle-Width="20%"  />
        <asp:TemplateField HeaderText="<%$RIResources:Shared,Required %>" >
            <ItemTemplate>
                <asp:CheckBox ID="_" runat="Server" Enabled="false" checked='<%# Eval("required").ToString().Equals("Y") %>'/>
            </itemtemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="_btnApproverDelete" CommandName="Delete" runat="server" CssClass="Button"
                    Text="<%$RIResources:Global,Delete %>" />
                <ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" confirmtext="<%$RIResources:Shared,ConfirmDelete %>"
                    TargetControlID="_btnApproverDelete">
                </ajaxToolkit:ConfirmButtonExtender>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
    </asp:GridView>  
    <br />
    <asp:GridView Width="100%" CssClass="Border" ID="_gvL2Approvers" runat="server" 
        AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="notify_seqid" EmptyDataText="<%$RIResources:MOC,No Level 2 Approvers %>" >
        <EmptyDataRowStyle Font-Bold="true" />
        <Columns>
        <asp:BoundField DataField="FullName" HeaderText="<%$RIResources:MOC,Level 2 Approvers %>" HeaderStyle-Width="25%"     />
        <asp:BoundField DataField="risuperarea" HeaderText="<%$RIResources:Shared,BusinessUnit %>" HeaderStyle-Width="20%"  />
        <asp:BoundField DataField="subarea" HeaderText="<%$RIResources:Shared,Area %>" HeaderStyle-Width="20%"  />
        <asp:BoundField DataField="area" HeaderText="<%$RIResources:Shared,Line %>" HeaderStyle-Width="20%"  />
             <asp:TemplateField HeaderText="<%$RIResources:Shared,Required %>" >
            <ItemTemplate>
                <asp:CheckBox ID="CheckBox1" runat="Server" Enabled="false" checked='<%# Eval("required").ToString().Equals("Y") %>'/>
            </itemtemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="_btnApproverDelete" CommandName="Delete" runat="server"
                    Text="<%$RIResources:Global,Delete %>" />
                <ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" confirmtext="<%$RIResources:Shared,ConfirmDelete %>"
                    TargetControlID="_btnApproverDelete">
                </ajaxToolkit:ConfirmButtonExtender>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
    </asp:GridView>  
    <br />
    <asp:GridView Width="100%" CssClass="Border" ID="_gvL3Approvers" runat="server" 
        AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="notify_seqid" EmptyDataText="<%$RIResources:MOC,No Level 3 Approvers %>" >
        <EmptyDataRowStyle Font-Bold="true" />
        <Columns>
        <asp:BoundField DataField="FullName" HeaderText="<%$RIResources:MOC,Level 3 Approvers %>" HeaderStyle-Width="25%"     />
        <asp:BoundField DataField="risuperarea" HeaderText="<%$RIResources:Shared,BusinessUnit %>" HeaderStyle-Width="20%"  />
        <asp:BoundField DataField="subarea" HeaderText="<%$RIResources:Shared,Area %>" HeaderStyle-Width="20%"  />
        <asp:BoundField DataField="area" HeaderText="<%$RIResources:Shared,Line %>" HeaderStyle-Width="20%"  />
            <asp:TemplateField HeaderText="<%$RIResources:Shared,Required %>" >
            <ItemTemplate>
                <asp:CheckBox ID="CheckBox1" runat="Server" Enabled="false" checked='<%# Eval("required").ToString().Equals("Y") %>'/>
            </itemtemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="_btnApproverDelete" CommandName="Delete" runat="server"
                    Text="<%$RIResources:Global,Delete %>" />
                <ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" confirmtext="<%$RIResources:Shared,ConfirmDelete %>"
                    TargetControlID="_btnApproverDelete">
                </ajaxToolkit:ConfirmButtonExtender>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
    </asp:GridView>   --%>   
    <br />
    <asp:GridView Width="100%" CssClass="Border" ID="_gvInformed" runat="server" 
        AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="notify_seqid" EmptyDataText="<%$RIResources:MOC,No Informed %>">
        <EmptyDataRowStyle Font-Bold="true" />
        <Columns>
        <asp:BoundField DataField="FullName" HeaderText="<%$RIResources:Shared,Informed Name %>" HeaderStyle-Width="25%"    />
        <asp:BoundField DataField="risuperarea" HeaderText="<%$RIResources:Shared,BusinessUnit %>" HeaderStyle-Width="20%" />
        <asp:BoundField DataField="subarea" HeaderText="<%$RIResources:Shared,Area %>" HeaderStyle-Width="20%" />
        <asp:BoundField DataField="area" HeaderText="<%$RIResources:Shared,Line %>" HeaderStyle-Width="20%" />
            <asp:TemplateField HeaderText="<%$RIResources:Shared,Required %>" >
            <ItemTemplate>
                <asp:CheckBox ID="CheckBox1" runat="Server" Enabled="false" checked='<%# Eval("required").ToString().Equals("Y") %>'/>
            </itemtemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="_btnApproverDelete" CommandName="Delete" runat="server" 
                    Text="<%$RIResources:Global,Delete %>" />
                <ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" confirmtext="<%$RIResources:Shared,ConfirmDelete %>"
                    TargetControlID="_btnApproverDelete">
                </ajaxToolkit:ConfirmButtonExtender>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
    </asp:GridView>      
    </asp:Panel>
    
    </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
