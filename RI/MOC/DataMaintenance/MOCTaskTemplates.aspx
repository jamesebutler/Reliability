<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="MOCTaskTemplates.aspx.vb"
    Inherits="RI_MOC" Title="MOC Task Templates" EnableViewState="True"
    EnableViewStateMac="false" ViewStateEncryptionMode="never" Trace="false" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<%@ Register Src="~/RI/User Controls/Common/UcMTTResponsible.ascx" TagName="Responsible" TagPrefix="IP" %>

<asp:Content ID="_contentTemplate" ContentPlaceHolderID="_cphMain" runat="Server">
    <asp:UpdatePanel ID="_pnlTemplateHeading" runat="Server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="display: none">
                <asp:DropDownList ID="_ddlHiddenRoles" runat="server">
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
            </div>
            
            <asp:HiddenField ID="_hfTaskHeaderSeqid" runat="server" Value='<%# Bind("TaskHeaderSeqid") %>' />

            <asp:Table ID="_tblHeader" runat="server" CellPadding="2" CellSpacing="0" Style="width: 100%">
                <asp:TableRow CssClass="Header">
                    <asp:TableCell Width="100%" ColumnSpan="5" Style="text-align: left">
                        <asp:Label ID="Label1" SkinID="LabelWhite" runat="server" Text="<%$RIResources:Shared,Select Classification or Category to add/update existing templates %>"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow CssClass="Border" ID="_trClassCat">
                    <asp:TableCell>
                        <asp:RadioButtonList ID="_rblMaintType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                            <asp:ListItem Value="Classification" Text="<%$RIResources:Shared,Classification %>" Selected="true"></asp:ListItem>
                            <asp:ListItem Value="Category" Text="<%$RIResources:Shared,Category %>"></asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow CssClass="Border" ID="_trClass">
                    <asp:TableCell ID="_tcClass" VerticalAlign="top" Visible="true">
                        <asp:Label ID="_lblClassification" runat="server" Text="<%$RIResources:Shared,Classification %>" Visible="false"></asp:Label>
                        <asp:DropDownList ID="_ddlClass" runat="server" AutoPostBack="true" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow CssClass="Border" ID="_trCat">
                    <asp:TableCell ID="_tcCategory" VerticalAlign="top" Visible="true">
                        <asp:Label ID="_lblCategory" runat="server" Text="<%$RIResources:Shared,Category %>" Visible="false"></asp:Label>
                        <asp:DropDownList ID="_ddlCategory" runat="server" AutoPostBack="true" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <div style="width: 100%; text-align: center;">
                <IP:MessageBox ID="_messageBox" runat="server" ButtonType="OKCancel" />
                <br />
                <div style="width: 100%; text-align: center;">
                    <center>
                        <asp:Button ID="_btnSaveTemplate" runat="server" Text="Save Changes" Visible="true" />&nbsp;
                    <asp:Label ID="_lblStatus" runat="server" ForeColor="Red"></asp:Label>
                    </center>
                </div>
            </div>

            <asp:Panel ID="_pnlTemplateTasks" runat="Server" Style="height: auto; margin-top: auto; margin-bottom: auto; overflow: auto;">
                <asp:Table ID="_tblNewRow" runat="server" Width="100%" BorderStyle="Outset" Visible="true">
                    <asp:TableRow CssClass="Header">
                        <asp:TableCell Width="100%" ColumnSpan="5" Style="text-align: left">
                            <asp:Label ID="_lblEntry" SkinID="LabelWhite" runat="server" Text="<%$RIResources:Shared,New Task %>"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Top">
                        <asp:TableCell Width="50%">
                            <span class="ValidationError">*</span>
                            <asp:Label ID="_lblNewTitle" runat="server" Text="<%$RIResources:Shared,Title/Description %>" Style="font-family: Verdana; font-size: 8pt"></asp:Label><br />
                            <asp:TextBox ID="_tbNewTitle" runat="server" Style="font-family: Verdana; font-size: 8pt" Width="90%" MaxLength="80"></asp:TextBox>
                            <br />
                            <IP:AdvancedTextBox ID="_tbNewDesc" runat="server" TextMode="SingleLine" ExpandHeight="true"
                                Style="font-family: Verdana; font-size: 8pt" Rows="3" MaxLength="1000" Width="95%">
                            </IP:AdvancedTextBox>
                        </asp:TableCell>
                        <asp:TableCell Width="10%">
                            <span class="ValidationError">*</span>
                            <asp:Label ID="_lblNewPriority" runat="server" Text="<%$RIResources:Shared,Priority%>" Style="font-family: Verdana; font-size: 8pt"></asp:Label><br />
                            <asp:DropDownList ID="_ddlNewPriority" runat="server" Font-Size="8">
                                <asp:ListItem Text="Low" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Medium" Value="2"></asp:ListItem>
                                <asp:ListItem Text="High" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell Width="10%">
                            <IP:MOCDate id="_NewDate" runat="server" visible="false"></IP:MOCDate>
                            <asp:Label ID="_lbNewDaysAfter" runat="server" Text="<%$RIResources:Shared,Days After Approval%>" Style="font-family: Verdana; font-size: 8pt"></asp:Label><br />
                            <span id="first">
                                <asp:TextBox ID="_tbNewDaysAfter" runat="server" Font-Bold="false" Font-Size="8" Width="20pt" MaxLength="3" /></span><br />
                            <ajaxToolkit:FilteredTextBoxExtender ID="_fbeNewDaysAfter" runat="server"
                                TargetControlID="_tbNewDaysAfter" FilterType="custom" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                            <%--<asp:Label ID="_lbNewDaysAfter" runat="server" Text="<%$RIResources:Shared,Days After Approval%>" Style="font-family: Verdana; font-size: 8pt"></asp:Label><br />
                        <span id="first"><asp:textbox id="_tbNewDaysAfter" runat="server" Font-Bold="false" font-size="8" Width="20pt" MaxLength="3" /></span><br />
                        <ajaxToolkit:FilteredTextBoxExtender ID="_fbeNewDaysAfter" runat="server"
                            TargetControlID="_tbNewDaysAfter" FilterType="custom" ValidChars="1234567890">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:Label ID="_lbNewDueDate" runat="server" Text="<%$RIResources:Shared,Due Date%>" Style="font-family: Verdana; font-size: 8pt"> OR </asp:Label>
                        <span id="second"><IP:DateTime ID="_newDueDate" enabled="false" DisplayTime="false" runat="server" DateLabel="" /></span>--%>
                        </asp:TableCell><asp:TableCell Visible="false">
                            <asp:Label ID="_lblNewResponsible" runat="server" Text="<%$RIResources:Shared,Responsible Person%>" Style="font-family: Verdana; font-size: 8pt"></asp:Label><br />
                            <asp:DropDownList ID="_ddlNewResponsible" runat="server" Width="100%" Font-Size="8">
                            </asp:DropDownList>
                        </asp:TableCell><asp:TableCell>
                            <IP:Responsible runat="server" ID="_ucNewResponsible"></IP:Responsible>
                        </asp:TableCell><asp:TableCell>
                            <asp:Label ID="_lblNewRequired" runat="server" Text="<%$RIResources:Shared,Required%>" Style="font-family: Verdana; font-size: 8pt"></asp:Label><br />
                            <asp:CheckBox ID="_cbNewRequired" runat="server" Font-Size="8" Width="20pt" />
                        </asp:TableCell><asp:TableCell>
                            <asp:Button ID="_btnNewTask" runat="server" Text='<%$ RIResources:Shared,Add%>' Visible="false"></asp:Button>
                        </asp:TableCell><asp:TableCell></asp:TableCell></asp:TableRow></asp:Table><br /><left>
            <asp:Label ID="_lblTemplateHeading" runat="server" Text="<%$RIResources:Shared,Current Tasks%>" Visible="false"></asp:Label></left><RWG:BulkEditGridView ID="_grvTasksPage" runat="server" CssClass="Border" AutoGenerateColumns="false"
                    BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" RowStyle-VerticalAlign="top"
                    Width="100%" Font-Size="8" EnableViewState="true" DataKeyNames="taskitemseqid" EmptyDataText="No Existing Tasks for ">
                    <headerstyle cssclass="LockHeader" font-size="8" />

                    <columns>
                    <asp:TemplateField HeaderText="<%$RIResources:Shared,Title/Description %>" ItemStyle-HorizontalAlign="left" ItemStyle-Width="60%" >
                        <EditItemTemplate>
                            <asp:textbox id="_tbTitle" runat="server" Font-Bold="false" Style="font-family: Verdana; font-size: 8pt"  Text='<%# Bind("title") %>' Width="95%" MaxLength="80" /><br />
                            <IP:AdvancedTextbox id="_tbDesc" runat="server" Style="font-family: Verdana; font-size: 8pt"  Text='<%# Bind("description") %>' ExpandHeight="true"
							Width="98%" Rows="2" TextMode="MultiLine" MaxLength="4000"  />
							<asp:RequiredFieldValidator id="_rfvTitle" runat="server" ControlToValidate="_tbTitle"
                            ErrorMessage="Title is a required field." ForeColor="Red"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="<%$RIResources:Shared,Priority %>" ItemStyle-HorizontalAlign="center" ItemStyle-Width="10%" HeaderStyle-Font-Size="8" >
                        <EditItemTemplate>
                            <asp:DropDownList id="_ddlPriority" runat="server" font-size="8" selectedvalue='<%# Bind("priority") %>' >
                                <asp:ListItem Text="Low" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Medium" Value="2"></asp:ListItem>
                                <asp:ListItem Text="High" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="<%$RIResources:Shared,Days After Approval %>" ItemStyle-HorizontalAlign="center" ItemStyle-Width="10%" HeaderStyle-Font-Size="8">
                        <EditItemTemplate>
                            <asp:textbox id="_tbDaysAfter" runat="server" Text='<%# Bind("daysafter") %>' Font-Bold="false" font-size="8" Width="20pt" MaxLength="3" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="_fbeDaysAfter" runat="server"
                                TargetControlID="_tbDaysAfter" FilterType="custom" ValidChars="1234567890">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator id="_rfvDaysAfter" runat="server" ControlToValidate="_tbDaysAfter"
                            ErrorMessage="Days After is a required field." ForeColor="Red"></asp:RequiredFieldValidator>
                        <IP:MOCDate id="_MOCDate" runat="server" DaysAfter='<%# Bind("daysafter") %>' DateValue='<%# Bind("duedate") %>' visible ="false"></IP:MOCDate>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="<%$RIResources:Shared,Responsible Person%>" ItemStyle-Width="10%" HeaderStyle-Font-Size="8">
                        <ItemTemplate>
                            <asp:DropDownList ID="_ddlResponsible" runat="server" Width="100%" Font-Size="8" visible="false">
                            </asp:DropDownList>
                            <ip:Responsible runat="server" id="_ucResponsible" FacilityValue='<%# Bind("resproleplantcode") %>' responsiblevalue='<%# Bind("responsibleusername") %>' autopostback="false" OnUserChanged="_grvTasksPage.HandleRowChanged"></ip:Responsible>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="<%$RIResources:Shared,Required%>" ItemStyle-Width="5%" HeaderStyle-Font-Size="8">
                        <ItemTemplate>
                            <asp:checkbox ID="_cbRequired" runat="server" Width="100%" Font-Size="8" checked='<%# Bind("required_flag") %>'>
                            </asp:checkbox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField>
                        <itemtemplate>
                        <center><asp:button id="_btnDelete" runat="server" CommandName="DeleteRow" text='<%$riResources:Shared,Delete%>' datatextfield="taskitemseqid"></asp:button></center>
                        <asp:HiddenField ID="_hfTaskItemSeqID" runat="server" Value='<%# Bind("TaskItemSeqid") %>' />
		                <asp:HiddenField ID="_hfResponsibleRoleSeqid" runat="server" Value='<%# Bind("RESPONSIBLEROLESEQID") %>' />
                        </itemtemplate>
                    </asp:TemplateField>
                    
                 </columns>
                </RWG:BulkEditGridView></asp:Panel></ContentTemplate></asp:UpdatePanel></asp:Content>