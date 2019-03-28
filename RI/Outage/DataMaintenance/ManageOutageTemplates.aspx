<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="ManageOutageTemplates.aspx.vb"
    Inherits="RI_Outage" Title="Outage Templates" EnableViewState="True"
    EnableViewStateMac="false" ViewStateEncryptionMode="never" Trace="false" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<%@ Register Namespace="RealWorld.Grids" TagPrefix="RWG" %>

<asp:Content ID="_contentTemplate" ContentPlaceHolderID="_cphMain" runat="Server">
    <Asp:UpdatePanel id="_pnlTemplateHeading" runat="Server" UpdateMode="Conditional" >
        <ContentTemplate>
        <asp:Table ID="_tblOutageStatus" runat="server" CellPadding="2" CellSpacing="2" BackColor="white" Style="width: 98%" EnableViewState="true" > 
            <asp:TableRow CssClass="Border">
                <asp:TableCell CssClass="Border"> 
                    <asp:Label ID="_lbTemplate" runat="server" Text="<%$RIResources:OUTAGE,TaskTemplate %>"></asp:Label>&nbsp;&nbsp;
                    <asp:DropDownList ID="_ddlTemplateTasks" runat="server" Visible="true" AutoPostBack="true" EnableViewState="true">
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        
 	    <div style="display:none">
            <asp:DropDownList ID="_ddlHiddenRoles" runat="server">
                <asp:ListItem></asp:ListItem>
            </asp:DropDownList>
        </div>

        <div id="_divNewTemplate" runat="server" style="display:none">
        <asp:Label ID="Label10" runat="server" Text="To Create a New Template, enter a template name and the fields for a task.  Click Save."></asp:Label><br />
        <asp:Label ID="Label11" runat="server" Text="New Template Name"></asp:Label>&nbsp;
        <asp:TextBox ID="_tbNewTemplate" runat="server" Width="50%" MaxLength="40" Text=" "></asp:TextBox>
        </div>
        
        <asp:Panel id="_pnlTemplateTasks" runat="Server" style="height:auto; margin-top:auto; margin-bottom:auto; overflow:auto;">
            <asp:Table ID="_tblNewRow" runat="server" Width="100%" BorderStyle="Outset" Visible="false">
                <asp:TableRow CssClass="Header">
                    <asp:TableCell Width="100%" ColumnSpan="5" Style="text-align: left">
                        <asp:Label ID="_lblEntry" SkinID="LabelWhite" runat="server" Text='Add Template Task'></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow VerticalAlign="Top">
                    <asp:TableCell Width="50%">
                        <span class="ValidationError">*</span>
                        <asp:Label ID="_lblNewTitle" runat="server" Text='Title/Description' Style="font-family: Verdana; font-size: 8pt"></asp:Label><br />
                        <asp:TextBox ID="_tbNewTitle" runat="server" Style="font-family: Verdana; font-size: 8pt" Width="90%" MaxLength="80"></asp:TextBox>
                        <br />
                        <IP:AdvancedTextBox ID="_tbNewDesc" runat="server" TextMode="SingleLine" ExpandHeight="true"
                            Style="font-family: Verdana; font-size: 8pt" Rows="3" MaxLength="1000" Width="95%">
                        </IP:AdvancedTextBox>
                    </asp:TableCell>
                    <asp:TableCell width="10%">
                        <asp:Label ID="_lbNewWeeksBefore" runat="server" Text='Weeks Before' Style="font-family: Verdana; font-size: 8pt"></asp:Label><br />
                        <asp:textbox id="_tbNewWeeksBefore" runat="server" Font-Bold="false" font-size="8" Width="20pt" MaxLength="3" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="_fbeNewWeeksBefore" runat="server"
                            TargetControlID="_tbNewWeeksBefore" FilterType="custom" ValidChars="1234567890">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </asp:TableCell>
                    <asp:TableCell width="10%">
                        <asp:Label ID="_lbNewWeeksAfter" runat="server" Text='Weeks After' Style="font-family: Verdana; font-size: 8pt"></asp:Label><br />
                        <asp:textbox id="_tbNewWeeksAfter" runat="server" Font-Bold="false" font-size="8" Width="20pt" MaxLength="3" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="_fbeNewWeeksAfter" runat="server"
                            TargetControlID="_tbNewWeeksAfter" FilterType="custom" ValidChars="1234567890">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </asp:TableCell>
                    <asp:TableCell width="10%">
                        <asp:Label ID="_lblNewLeadTime" runat="server" Text='Lead Time (Days)' Style="font-family: Verdana; font-size: 8pt"></asp:Label><br />
                        <asp:textbox id="_tbNewLeadTime" runat="server" Font-Bold="false" font-size="8" Width="20pt" MaxLength="3" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="_fbeNewLeadTime" runat="server"
                                    TargetControlID="_tbNewLeadTime" FilterType="custom" ValidChars="1234567890">
                            </ajaxToolkit:FilteredTextBoxExtender>
                    </asp:TableCell>
                    <asp:TableCell>
                            <asp:Label ID="_lblNewPrimaryRole" runat="server" Text='Role Description' Style="font-family: Verdana; font-size: 8pt"></asp:Label><br />
                            <asp:DropDownList ID="_ddlNewPrimaryRole" runat="server" Width="100%" Font-Size="8" >
                            </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell>
                            <asp:Button ID="_btnNewTask" runat="server" Text='<%$ RIResources:Shared,Add%>'></asp:Button>
                    </asp:TableCell>
                </asp:TableRow>
                              
            </asp:Table>
            
            <div style="width:100%; text-align:center;" >
            <IP:MessageBox ID="_messageBox" runat="server" ButtonType="OKCancel" />
            <br />
            <div style="width:100%; text-align:center; " >
                <center>
                    <asp:Button id="_btnSaveTemplate" runat="server" Text="Save Changes to Current Template" Visible="false" />&nbsp;
                    <asp:Button id="_btnNewCurrent" runat="server" Text="Save Current Template to NEW Template" Visible="false"/>&nbsp;
		            <asp:Button ID="_btnNewTemplate" Text="New Template" runat="server" EnableViewState="true" Visible="false"/><br />
		            <asp:Label id="_lblStatus" runat="server" ForeColor="Red"></asp:Label>
                </center>
		    </div>  
        </div>
           
            <br />
            <center>
            <asp:Label ID="_lblTemplateHeading" runat="server"></asp:Label>
            </center>
            <RWG:BulkEditGridView ID="_grvTasksPage" runat="server" CssClass="Border"  AutoGenerateColumns="false"
                BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" RowStyle-VerticalAlign="top"
                Width="100%" Font-Size="8" EnableViewState="true" OnPageIndexChanging="_grvTasksPage_PageIndexChanging" DataKeyNames="taskitemseqid" >
                <PagerSettings Position="TopAndBottom"  />
                <PagerStyle HorizontalAlign = "Right" CssClass="cssPager" />
                
                <HeaderStyle CssClass="LockHeader" Font-Size="8" />
                
                <Columns>
                    <asp:TemplateField HeaderText="Title/Description" ItemStyle-HorizontalAlign="left" ItemStyle-Width="50%" HeaderStyle-Font-Size="8" >
                        <EditItemTemplate>
                            <asp:textbox id="_tbTitle" runat="server" Font-Bold="false" font-size="8" Text='<%# Bind("title") %>' Width="95%" MaxLength="80" /><br />
                            <IP:AdvancedTextbox id="_tbDesc" runat="server" font-size="8" Text='<%# Bind("description") %>' ExpandHeight="true"
							Width="98%" Rows="2" TextMode="MultiLine" MaxLength="4000"  />
							<asp:RequiredFieldValidator id="_rfvTitle" runat="server" ControlToValidate="_tbTitle"
                            ErrorMessage="Title is a required field." ForeColor="Red"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Weeks Before" ItemStyle-HorizontalAlign="center" ItemStyle-Width="5%" HeaderStyle-Font-Size="8" >
                        <EditItemTemplate>
                            <asp:textbox id="_tbWeeksBefore" runat="server" Font-Bold="false" font-size="8" Width="20pt" Text='<%# Bind("weeksbefore") %>' MaxLength="3" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="_fbeWeeksBefore" runat="server"
                                TargetControlID="_tbWeeksBefore" FilterType="custom" ValidChars="1234567890">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:CustomValidator id="CustomValidator1" runat="server" ControlToValidate="_tbWeeksBefore" 
                                ErrorMessage="Either Weeks Before or Weeks After must have value."
                                ClientValidationFunction="validateLength"></asp:CustomValidator>
                        </EditItemTemplate>
                        </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Weeks After" ItemStyle-HorizontalAlign="center" ItemStyle-Width="5%" HeaderStyle-Font-Size="8" >
                        <EditItemTemplate>
                            <asp:textbox id="_tbWeeksAfter" runat="server" Font-Bold="false" font-size="8" Width="20pt" Text='<%# Bind("weeksafter") %>' MaxLength="3" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="_fbeWeeksAfter" runat="server"
                                TargetControlID="_tbWeeksAfter" FilterType="custom" ValidChars="1234567890">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Lead Time (Days)" ItemStyle-HorizontalAlign="center" ItemStyle-Width="5%" HeaderStyle-Font-Size="8" >
                        <EditItemTemplate>
                            <asp:textbox id="_tbLeadTime" runat="server" Font-Bold="false" font-size="8" Width="20pt" Text='<%# Bind("leadtime") %>' MaxLength="3" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="_fbeLeadTime" runat="server"
                                TargetControlID="_tbLeadTime" FilterType="custom" ValidChars="1234567890">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Primary Role" ItemStyle-Width="15%" HeaderStyle-Font-Size="8">
                        <ItemTemplate>
                            <asp:DropDownList ID="_ddlPrimaryRole" runat="server" Width="100%" Font-Size="8" >
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Area Role(s)" ItemStyle-Width="15%" HeaderStyle-Font-Size="8">
                        <ItemTemplate>
                            <asp:DropDownList ID="_ddlTemplateRole1" runat="server" Width="100%" Font-Size="8">
                            <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <br />                           
                            <asp:DropDownList ID="_ddlTemplateRole2" runat="server" Width="100%" Font-Size="8">
                            <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:DropDownList ID="_ddlTemplateRole3" runat="server" Width="100%" Font-Size="8">
                            <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        
                            <asp:DropDownList ID="_ddlTemplateRole4" runat="server" Width="100%" Font-Size="8">
                            <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="_ddlTemplateRole5" runat="server" Width="100%" Font-Size="8">
                            <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <itemtemplate>
                        <center><asp:button id="_btnDelete" runat="server" CommandName="DeleteRow" text='<%$riResources:Shared,Delete%>' datatextfield="taskitemseqid"></asp:button></center>
                        <asp:HiddenField ID="_hfRoleCount" runat="server" Value='<%# Bind("taskitemrolecnt") %>' />
                        <asp:HiddenField ID="_hfRoleSeqId" runat="server" Value='<%# Bind("taskitemroleseqid") %>' />
                        <asp:HiddenField ID="_hfPrimaryRoleSeqid" runat="server" Value='<%# Bind("RESPONSIBLEROLESEQID") %>' />
                        <asp:HiddenField ID="_hfTaskItemSeqID" runat="server" Value='<%# Bind("TaskItemSeqid") %>' />
		                </itemtemplate>
                    </asp:TemplateField>
                </Columns>
            </RWG:BulkEditGridView>

            <ajaxToolkit:ModalPopupExtender ID="_mpeCopyTemplate" runat="server" TargetControlID="_btnNewCurrent"
                PopupControlID="_pnlCopyTemplate" OkControlID="_btnCancel" CancelControlID="_btnCancel"
                BackgroundCssClass="modalBackground" DropShadow="true">
            </ajaxToolkit:ModalPopupExtender>
            
            <asp:Panel ID="_pnlCopyTemplate" runat="server" CssClass="modalPopup" Style="display: none; text-align: center;">
                <asp:Table ID="Table1" runat="Server">
                    <asp:TableRow BackColor="black"><asp:TableCell>
                        <asp:Label ID="Label8" runat="server" skinID="LabelWhite" Text="Please Enter A Template Name"></asp:Label>
                    </asp:TableCell></asp:TableRow>
                    <asp:TableRow><asp:TableCell>
                        <asp:Label ID="Label7" runat="server" Text="New Template Name"></asp:Label>&nbsp;
                        <asp:TextBox ID="_tbTemplateName" runat="server" Width="50%" MaxLength="40"></asp:TextBox>
                    </asp:TableCell></asp:TableRow>
                    <asp:TableRow><asp:TableCell>
                        <asp:Button ID="_btnCreateNewCurrentTemplate" runat="server" Text="Create" />&nbsp;
                        <asp:Button ID="_btnCancel" runat="server" Text="Cancel" />    
                    </asp:TableCell></asp:TableRow>
                </asp:Table>
            </asp:Panel>
            <asp:RequiredFieldValidator ID="_rfvTemplateDesc" runat="server" Display="none"
				ControlToValidate="_tbTemplateName" ErrorMessage="Template Name is required." EnableClientScript="true"
    			setFocusOnError="true" Text="Template Name is Required">
            </asp:RequiredFieldValidator>
            

            <ajaxToolkit:ModalPopupExtender ID="_mpeNewTemplate" runat="server" TargetControlID="_btnNewTemplate"
                PopupControlID="_pnlNewTemplate" CancelControlID="_btnCancelNew"
                BackgroundCssClass="modalBackground" DropShadow="true">
            </ajaxToolkit:ModalPopupExtender>
            
            <asp:Panel ID="_pnlNewTemplate" runat="server" CssClass="modalPopup" Style="display: none; text-align: center;">
                <asp:Table runat="Server">
                    <asp:TableRow BackColor="black"><asp:TableCell>
                        <asp:Label ID="Label9" runat="server" SkinID="LabelWhite" Text="Please Enter A Template Name"></asp:Label>
                    </asp:TableCell></asp:TableRow>
                    <asp:TableRow><asp:Tablecell>
                        <asp:Label ID="Label6" runat="server" Text="New Template Name"></asp:Label>&nbsp;
                        <asp:TextBox ID="_tbNewTemplateName" runat="server" Width="50%" MaxLength="40" Text=" "></asp:TextBox>
                    </asp:Tablecell></asp:TableRow>
                    <asp:TableRow><asp:Tablecell>
                        <asp:Button ID="_btnCreateTemplate" runat="server" Text="Create" />&nbsp;
                        <asp:Button ID="_btnCancelNew" runat="server" Text="Cancel" /> 
                    </asp:Tablecell></asp:TableRow>
                </asp:Table>
            </asp:Panel> 
            
            <%--<asp:CustomValidator ID="_cfvNewTemplate" runat="server" 
            ControlToValidate="_tbNewTemplateName" ErrorMessage="Template Name is required." ValidateEmptyText=True>
            </asp:CustomValidator>--%>
            
            <asp:RequiredFieldValidator ID="_rfvNewTemplate" runat="server" Display="none"
				ControlToValidate="_tbNewTemplateName" ErrorMessage="Template Name is required."
				setFocusOnError="true" Text="Template Name is Required" Enabled="false">
    	    </asp:RequiredFieldValidator>
            
            
        </asp:Panel> 
           
       </ContentTemplate>
    </Asp:UpdatePanel>
</asp:Content>
