<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="EnterOutage.aspx.vb"
	Inherits="Outage_EnterOutage" Title="RI:Enter Outage" Trace="false" EnableViewState="true" EnableEventValidation="false" %>
<%@ MasterType VirtualPath="~/RI.master" %>

<asp:Content ID="_contentMain" ContentPlaceHolderID="_cphMain" runat="Server">
    <style type="text/css"> 
        .cssPager td 
        { 
            background-color: #CCC; 
            font-size: 16px; 
            padding-left: 6px; 
            padding-right: 6px; 
            border-color: #CCD;
        } 
    </style> 
     
    <Asp:UpdatePanel id="_udpLocation" runat="server" updatemode="Conditional">
		<ContentTemplate>
            <asp:Table ID="_tblOutageStatus" runat="server" CellPadding="2" CellSpacing="2" BackColor="white"
                Style="width: 98%" EnableViewState="true" > 
                <asp:TableHeaderRow CssClass="Header">
                    <asp:TableHeaderCell  HorizontalAlign="left">
                        <span class="ValidationError">*</span><asp:Label ID="_lblOutageCategory" runat="server" EnableViewState="false" Text="<%$RIResources:OUTAGE,SDCategory %>"
                            SkinID="LabelWhite"></asp:Label>
                    </asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableRow CssClass="Border">
                    <asp:TableCell CssClass="Border"> 
                        <asp:RadioButtonList ID="_rblSDCategory" runat="server" RepeatDirection="Horizontal"  >
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
                           </asp:RadioButtonList>
               				<asp:RequiredFieldValidator  ValidationGroup="EnterOutage" ID="_rfvSDCategory" runat="server" Display="none"
							ControlToValidate="_rblSDCategory" ErrorMessage="<%$RIResources:Outage,SelectSDCategory %>" EnableClientScript="true"
        					SetFocusOnError="true" Text="<%$RIResources:Outage,SelectSDCategory %>"></asp:RequiredFieldValidator>

                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            
            <asp:Panel ID="_pnlLocation" runat="server">
                <asp:Table ID="_tblSite" runat="server" BorderWidth="0" CellPadding="2" CellSpacing="0" BackColor="white" EnableViewState="true">
					<asp:TableRow CssClass="Border">
					    <asp:TableCell ID="_tcFacility" Width="33%" BackColor="Black" >
                            <span class="ValidationError">*</span><asp:Label ID="_lblFacility" BackColor="Black" SkinID="LabelWhite"  runat="server"
								Text="<%$RIResources:Shared,Facility %>"></asp:Label>						
					    </asp:TableCell>
						<asp:TableCell ID="_tcBusinessUnitArea" Width="33%" BackColor="Black" >
							<asp:Label ID="_lblBusinessUnitArea" SkinID="LabelWhite" runat="server" Text="<%$RIResources:Shared,BusinessUnitArea %>"></asp:Label><br />
						</asp:TableCell>
					</asp:TableRow>
					<asp:TableRow CssClass="Border">
						<asp:TableCell ID="_tcFacilityddl" VerticalAlign=Top >
						    <asp:DropDownList ID="_ddlFacility" runat="server" CausesValidation="true" AutoPostBack="true" Width="150pt" >
							</asp:DropDownList>
							<asp:RequiredFieldValidator ValidationGroup="EnterOutage" ID="_rfvFacility" runat="server" Display="none" ControlToValidate="_ddlFacility"
								ErrorMessage="<%$RIResources:Shared,SelectFacility %>" EnableClientScript="true" SetFocusOnError="true"
								Text="<%$RIResources:Shared,SelectFacility %>"></asp:RequiredFieldValidator>
					    </asp:TableCell>
						<asp:TableCell ID="_tcBusinessUnit">
							<div style="overflow-Y:scroll; WIDTH:725px; HEIGHT:140px;">
                            <asp:checkboxlist ID="_lbBusinessUnit" CausesValidation="false" AutoPostBack="false"
								runat="server"  />
                            </div>
                        </asp:TableCell>
					</asp:TableRow>
				</asp:Table>
			</asp:Panel>
			
			<asp:Table ID="_tbl1" runat="server" BorderWidth="0" CellPadding="2" CellSpacing="2"
			    BackColor="white" Style="width: 99%; overflow: hidden;">
				<asp:TableRow CssClass="Border">
					    <asp:TableCell ID="_tc1" colspan="5" BackColor="Black" >
                            <asp:Label ID="_lbDates" BackColor="Black" SkinID="LabelWhite" runat="server" Text="Date/Cost"></asp:Label>						
					    </asp:TableCell>
					</asp:TableRow>
				<asp:TableRow CssClass="Border">
					<asp:TableCell ID="_PlannedCalendar" runat="server" ColumnSpan="2" Width="50%">
						<IP:StartEndCalendar id="_startEnd" runat="server" showtime="true" AddDateLabel="Planned" />
 					</asp:TableCell>
					<asp:TableCell Style="width: 18%">
                       <asp:Label ID="_lblDowntime" runat="server" Text="<%$RIResources:Shared,Downtime %>"
                           EnableViewState="false" /><br /><asp:TextBox ID="_txtDownTime" Width="80" runat="server"></asp:TextBox>
                       <ajaxToolkit:FilteredTextBoxExtender ID="_fbePlannedDowntime" runat="server"
                           TargetControlID="_txtDownTime" FilterType="custom" ValidChars=".,1234567890">
                       </ajaxToolkit:FilteredTextBoxExtender>&nbsp;
                       <asp:Button ID="_btnCalculateDowntime" Visible="true" OnClientClick="calculateDowntime();return false;"
                           runat="server" Text="<%$RIResources:Shared,Calculate %>" />
					</asp:TableCell>
					<asp:TableCell Width="15%">
						<asp:label ID="_lblPlannedCost" runat="server" Text="Planned Indirect<br />Cost US$"/>&nbsp;&nbsp;
						<asp:TextBox ID="_txtPlannedCost" runat="server" MaxLength="10" Width="100"></asp:TextBox>
						<ajaxToolkit:FilteredTextBoxExtender ID="_fbePlannedCost" runat="server"
                           TargetControlID="_txtPlannedCost" FilterType="custom" ValidChars="1234567890">
                       </ajaxToolkit:FilteredTextBoxExtender>
					</asp:TableCell>
				    <asp:TableCell Width="15%">
						<asp:label ID="_lblPlannedCapital" runat="server" Text="Planned Capital<br />Cost US$"/>&nbsp;&nbsp;
						<asp:TextBox ID="_txtPlannedCapital" runat="server" MaxLength="10" Width="100"></asp:TextBox>
						<ajaxToolkit:FilteredTextBoxExtender ID="_fbePlannedCapital" runat="server"
                           TargetControlID="_txtPlannedCapital" FilterType="custom" ValidChars="1234567890">
                       </ajaxToolkit:FilteredTextBoxExtender>
					</asp:TableCell>
				</asp:TableRow>
                <asp:TableRow CssClass="Border" ID="_trProposed" Visible="false" >
                    <asp:TableCell ColumnSpan="2" Width="45%" ID="_tbProposed" >
                        <IP:StartEndCalendar id="_ProposedStartEnd" runat="server" showtime="true" AddDateLabel="Proposed" />
                    </asp:TableCell>
					<asp:TableCell ColumnSpan="3" >
                        <asp:HyperLink id="_hlMOC" runat="server" Text="MOC" NavigateUrl="~/MOC/EnterMOC.aspx?MOCNumber" Target="_self" ></asp:HyperLink>
 					</asp:TableCell>
                </asp:TableRow>
				<asp:TableRow CssClass="Border">
					<asp:TableCell ColumnSpan="2" Width="45%">
						<IP:StartEndCalendar id="_ActualStartEnd" runat="server" showtime="true" AddDateLabel="Actual" />
					</asp:TableCell>
					<asp:TableCell Style="width: 18%">
                       <asp:Label ID="_lblActualDowntime" runat="server" Text="<%$RIResources:Shared,Downtime %>"
                           EnableViewState="false" /><br /><asp:TextBox ID="_txtActualDowntime" Width="80" runat="server"></asp:TextBox>
                       <ajaxToolkit:FilteredTextBoxExtender ID="_fbeActualDowntime" runat="server"
                           TargetControlID="_txtActualDowntime" FilterType="custom" ValidChars=".,1234567890">
                       </ajaxToolkit:FilteredTextBoxExtender>&nbsp;
                       <asp:Button ID="_btnActualDowntime" Visible="true" OnClientClick="calculateActualDowntime();return false;"
                           runat="server" Text="<%$RIResources:Shared,Calculate %>" />
					</asp:TableCell>
					<asp:TableCell Width="15%">
						<asp:label ID="_lblActualCost" runat="server" Text="Actual Indirect<br />Cost US$"/>&nbsp;&nbsp;
						<asp:TextBox ID="_txtActualCost" runat="server" MaxLength="10" Width="100"></asp:TextBox>
						<ajaxToolkit:FilteredTextBoxExtender ID="_fbeActualCost" runat="server"
                           TargetControlID="_txtActualCost" FilterType="custom" ValidChars="1234567890">
                       </ajaxToolkit:FilteredTextBoxExtender>
					</asp:TableCell>
				    <asp:TableCell Width="15%">
						<asp:label ID="_lblActualCapital" runat="server" Text="Actual Capital<br />Cost US$"/>&nbsp;&nbsp;
						<asp:TextBox ID="_txtActualCapital" runat="server" MaxLength="10" Width="100"></asp:TextBox>
						<ajaxToolkit:FilteredTextBoxExtender ID="_fbeActualCapital" runat="server"
                           TargetControlID="_txtActualCapital" FilterType="custom" ValidChars="1234567890">
                       </ajaxToolkit:FilteredTextBoxExtender>
					</asp:TableCell>
				</asp:TableRow>
				
				<asp:TableRow CssClass="Border">
					<asp:TableCell ColumnSpan="2">
						<span class="ValidationError">*</span><asp:Label ID="_lblTitle" runat="server"
							Text="<%$RIResources:Shared,Title %>" EnableViewState="false" />&nbsp;<asp:TextBox
							ID="_txtTitle" runat="server" Width="500"></asp:TextBox>
						<asp:RequiredFieldValidator ValidationGroup="EnterOutage" ID="_rfvTitle" runat="server" Display="none"
							ControlToValidate="_txtTitle" ErrorMessage="<%$RIResources:Shared,EnterTitle %>" EnableClientScript="true"
							SetFocusOnError="true" Text="<%$RIResources:Shared,EnterTitle %>"></asp:RequiredFieldValidator>
					</asp:TableCell>
					<asp:TableCell ColumnSpan="3" width="40%">
						<span class="ValidationError">*</span><asp:Label ID="_lblOutageCoord" runat="server" Text="<%$RIResources:Outage,OutageCoord %>"
							EnableViewState="false" />&nbsp;&nbsp;<asp:DropDownList ID="_ddlOutageCoord" 
							runat="server">
							</asp:DropDownList>
               				<asp:RequiredFieldValidator ValidationGroup="EnterOutage" ID="_rfvCoord" runat="server" Display="none" Enabled="true"
							ControlToValidate="_ddlOutageCoord" ErrorMessage="<%$RIResources:Outage,ValidateOutageCoord %>" EnableClientScript="true"
        					SetFocusOnError="true" Text="<%$RIResources:Outage,ValidateOutageCoord %>"></asp:RequiredFieldValidator>							
					</asp:TableCell>
				</asp:TableRow>
				<asp:TableRow CssClass="Border">
					<asp:TableCell ColumnSpan="5" HorizontalAlign="left">
						<asp:Label ID="_lblDescription" runat="server" Text="<%$RIResources:Shared,Description %>"
							EnableViewState="false" />
						<div>
							<IP:AdvancedTextBox ID="_txtDescription" runat="server" ExpandHeight="true"
								Width="98%" Rows="2" TextMode="MultiLine" MaxLength="4000" style="width: 98%; max-width:1200px;font-size:12px;color:Black;font-family:Verdana;"/></div>
					</asp:TableCell>
				</asp:TableRow>					                
				<asp:TableRow CssClass="Border">
					<asp:TableCell ColumnSpan="5" HorizontalAlign="left">
						<asp:Label ID="_lblComments" runat="server" Text="<%$RIResources:Shared,Comments %>"
							EnableViewState="false" />
						<div>
							<IP:AdvancedTextBox ID="_txtComments" runat="server" ExpandHeight="true"
								Width="98%" Rows="2" TextMode="MultiLine" MaxLength="4000" style="width: 98%; max-width:1200px;font-size:12px;color:Black;font-family:Verdana;"/></div>
					</asp:TableCell>
				</asp:TableRow>					                
				<asp:TableRow CssClass="Border">
					<asp:TableCell ColumnSpan="5" HorizontalAlign="left">
				        <asp:Label ID="_lblTemplates" runat="server" Text="<%$RIResources:OUTAGE,TaskTemplate %>"></asp:Label>&nbsp;&nbsp;
						<asp:DropDownList ID="_ddlTemplateTasks" runat="server" Visible="true" Enabled="true" ></asp:DropDownLIst>
						&nbsp;&nbsp;<asp:Button ID="_btnViewTemplateTasks" Text="<%$RIResources:Shared,View %>" runat="server" CausesValidation="false" EnableViewState="true"/>
					    &nbsp;&nbsp;<asp:Label ID="_lblTemplateTaskMsg" runat="server" Font-Italic="true"></asp:Label>
					</asp:TableCell>
				</asp:TableRow>			
				</asp:Table>		
			<asp:Table ID="_tbl3" runat="server" BorderWidth="0" CellPadding="2" CellSpacing="2"
			    BackColor="white" Style="width: 99%; overflow: hidden;">
                <asp:TableHeaderRow CssClass="Header">
                    <asp:TableHeaderCell  HorizontalAlign="left" ColumnSpan="4">
                        <asp:Label ID="_lblAnnual" runat="server" EnableViewState="false" Text="<%$RIResources:OUTAGE,AnnualOutage %>"
                            SkinID="LabelWhite"></asp:Label>
                    </asp:TableHeaderCell>
                </asp:TableHeaderRow>
				<asp:TableRow CssClass="Border" >
					<asp:TableCell HorizontalAlign="left" VerticalAlign="Top" Width="40%">
					    <asp:Label ID="_lbAnnualOutage" runat="server" EnableViewState="false" Text="<%$RIResources:OUTAGE,AnnualOutageCheck %>"></asp:Label>
						&nbsp;&nbsp;<asp:CheckBox ID="_cbAnnualOutage" runat="server"></asp:CheckBox>	
					</asp:TableCell>
				    <asp:TableCell Width="60%" >
					    <IP:DateTime id="_AssessDate" runat="server" displaytime="false" DateLabel="<%$RIResources:OUTAGE,AssessmentDate %>" AllowManual="True"/>
                        <asp:Label ID="_lblMRCOELead" runat="server" Text="<%$RIResources:Outage,MRLead %>"
							EnableViewState="false" />&nbsp;&nbsp;<asp:DropDownList ID="_ddlMRLead" runat="server">
						    </asp:DropDownList>
    				</asp:TableCell>
			    </asp:TableRow>
                <%--<asp:TableHeaderRow CssClass="Header">
                    <asp:TableHeaderCell  HorizontalAlign="left" ColumnSpan="3">
                        <asp:Label ID="_lblTG" runat="server" EnableViewState="false" Text="<%$RIResources:OUTAGE,TGOutage %>"
                            SkinID="LabelWhite"></asp:Label>
                    </asp:TableHeaderCell>
                </asp:TableHeaderRow>
				<asp:TableRow CssClass="Border"  >
						<asp:TableCell Width="33%" HorizontalAlign="left" VerticalAlign="Top" ColumnSpan="3">
							    <asp:Label ID="_lbTGCehck" runat="server" EnableViewState="false" Text="<%$RIResources:OUTAGE,TGOutageCheck %>"></asp:Label>
								&nbsp;&nbsp;<asp:CheckBox ID="_cbTGOutage" runat="server"></asp:CheckBox>	
					    </asp:TableCell>
			    </asp:TableRow>--%>
			</asp:Table>


         <%--<asp:Panel ID="_pnl" runat="server" Visible="false">
            <div id="AddContractor" style="width: 100%; text-align: left;" class="Header"></div>
         </asp:Panel>--%>
                
         <asp:Panel ID="_pnlContractor" runat="server" style="display:none;">
            <input type="text" runat="server"  id="_lblConflictChanged" name="_lblConflictChanged" style="display:none"></>
            <div id="SelectedContractor" style="width: 100%; text-align: left;" class="Header" runat="server">
                &nbsp;<asp:Label ID="_lblContractors" runat="server" EnableViewState="false" Text="<%$RIResources:OUTAGE,Contractors %>"
                SkinID="LabelWhite"></asp:Label>
            </div>
            <asp:GridView Width="100%" CssClass="Border" BorderWidth="3"
                ID="_gvContractor" runat="server" AutoGenerateColumns="False" AllowSorting="False"
                ShowFooter="False" DataKeyNames="contractorseqid">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="7%" HeaderText="<%$RIResources:Global,Delete %>"
                        HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
                        <ItemTemplate>
                            <asp:Button ID="_btnContractorDelete" CommandName="Delete" runat="server"
                                Text="<%$RIResources:Global,Delete %>" />
                                <ajaxToolkit:ConfirmButtonExtender ID="_cbeContractorDelete" runat="server" ConfirmText="<%$RIResources:Shared,ConfirmDelete %>"
                                TargetControlID="_btnContractorDelete">
                                </ajaxToolkit:ConfirmButtonExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:Global,Company %>"
                            HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
                        <ItemTemplate>
                            <asp:Label ID="_lblCompany" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="15%" HeaderText="<%$RIResources:Global,TypeOfWork %>"
                        HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
                        <ItemTemplate>
                            <asp:Label ID="_lblSeqid" runat="server" Text='<%# Bind("contractorseqid") %>' Visible="false"></asp:Label>
                            <asp:Label ID="_lblcontractor" runat="server" Text='<%# Bind("personname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$RIResources:Shared,Pre-Work and Post-Work Dates are defaulted to Outage start/end dates.  Please update.%>" ItemStyle-Width="30%"
                        HeaderStyle-Font-Underline="true">
                        <ItemTemplate >
       					    <center>
       						<IP:StartEndCalendar id="_ContractorstartEnd" runat="server" showtime="false" changedatelabel="false" startdate='<%# Bind("startdate") %>' enddate='<%# Bind("enddate") %>' defaultstartdate='' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="8%" HeaderText="<%$RIResources:Global,HeadCount %>" HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
                        <ItemTemplate>
                            <center><asp:TextBox ID="_txtHeadCount" Width="20" runat="server" Text='<%# Bind("headcount") %>'></asp:TextBox></center>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                    TargetControlID="_txtHeadCount" FilterType="custom" ValidChars=",.1234567890">
                                </ajaxToolkit:FilteredTextBoxExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="40%" HeaderText="<%$RIResources:Global,Comments %>" HeaderStyle-Font-Underline="true">
                        <ItemTemplate>
                                <center>
                                    <IP:AdvancedTextBox ID="_txtCommentsContractor" runat="server" ExpandHeight="true" Text='<%# Bind("comments") %>'
                                        Width="95%" TextMode="MultiLine" MaxLength="4000" style="width: 98%;font-size:12px;color:Black;font-family:Verdana;"/></center>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField> 
                        <ItemTemplate> 
                            <tr title="Conflict"> 
                                <td colspan="100%"> 
                                    <div id="div<%# Eval("contractorseqid") %>" style=" padding-left: 2em; padding-right: 2em; text-align: left;" >

                                    <%--<RWG:BulkEditGridView ShowHeader="true" ID="_gvConflicts" runat="server" ForeColor="#333333" Width="100%" EmptyDataText="NO CONFLICTS" EmptyDataRowStyle-BackColor="lightgreen"
                                                    AutoGenerateColumns="False" DataKeyNames="ContractorseqID" EnableInsert="False" 
                                                    SaveButtonID="" EnableViewState="true" OnRowDataBound="ConflictRow">

     --%>                               <asp:GridView ID="_gvConflicts" runat="server" ForeColor="#333333" Width="100%"  EmptyDataText="NO CONFLICTS" EmptyDataRowStyle-BackColor="lightgreen"
                                             AutoGenerateColumns="false" DataKeyNames="ContractorseqID" ShowHeader="true" OnRowDataBound="ConflictRow"> 
                                            <HeaderStyle BackColor="lightgray" /> 
                                            <AlternatingRowStyle CssClass="dataTableAlt" /> 
                                            <RowStyle BackColor="LightGray"/>
                                            
                                            <Columns > 
                                                <asp:TemplateField HeaderText="Conflict Status" HeaderStyle-Width="22%">
                                                <ItemTemplate>
                                                    <IMG id="bulletimg" runat=server SRC="../Images/bullet_red.png" />
                                                    <asp:DropDownList runat="server" ID="_ddlConflictStatus" SelectedValue='<%# Bind("conflictstatus") %>'>
                                                        <asp:ListItem Text="CONFLICT" value="C" style="color: red;"></asp:ListItem>
                                                        <asp:ListItem Text="INVESTIGATING" Value="I" style="color:darkkhaki;"></asp:ListItem>
                                                        <asp:ListItem Text="INVESTIGATED-NO CONFLICT" Value="N" style="color:green;"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Comment" headerstyle-width="15%">
                                                <ItemTemplate>
                                                    <IP:AdvancedTextBox ID="_txtConflictComment" runat="server" ExpandHeight="true" Text='<%# Bind("conflictcomment") %>'
                                                    Width="95%" MaxLength="4000" style="width: 90%;font-size:12px;color:Black;font-family:Verdana;"/>
                                                    <asp:label runat="Server" id="_lbConflictOutage" Visible="false" text='<%# Bind("conflictoutagenumber")%>'></asp:label></center>
                                                </ItemTemplate>
                                                </asp:TemplateField>                                                

                                                <asp:BoundField DataField="sitename" HeaderText="Site" HeaderStyle-Width="10%"  /> 
                                                <asp:BoundField DataField="outagetitle" HeaderText="Title" HeaderStyle-Width="30%"  /> 
                                                <asp:BoundField DataField="prework_startdate" HeaderText="Start Date" DataFormatString="{0:d}" /> 
                                                <asp:BoundField DataField="postwork_enddate" HeaderText="End Date" DataFormatString="{0:d}"/> 
                                                <asp:BoundField DataField="coordinator" HeaderText="Coordinator" headerstyle-width="10%"/> 
                                
                                            </Columns> 
                                        <%--</RWG:BulkEditGridView>--%>
                                            </asp:GridView> 
           
                                        </div>
                                    </td> 
                                </tr> 
                            </ItemTemplate> 
                    </asp:TemplateField> 
                </Columns>
                     
            </asp:GridView>
        </asp:Panel>

         <asp:Panel ID="_pnlResources" runat="server" style="display:none;">
                <div id="Div2" style="width: 100%; text-align: left;" class="Header">
                    &nbsp;<asp:Label ID="_lblResources" runat="server" EnableViewState="false" Text="<%$RIResources:OUTAGE,Resources %>"
                            SkinID="LabelWhite"></asp:Label>
<%--                    <asp:Button ID="_btnResources" runat="server" Text="Resources" />
--%>                </div>
                <asp:GridView Width="100%" CssClass="Border" BorderWidth="3"
                    ID="_gvResources" runat="server" AutoGenerateColumns="False" AllowSorting="False"
                    ShowFooter="False" DataKeyNames="resourceseqid">
                    <Columns>
                       <asp:TemplateField ItemStyle-Width="7%" HeaderText="<%$RIResources:Global,Delete %>"
                            HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
                            <ItemTemplate>
                                <asp:Button ID="_btnResourceDelete" CommandName="Delete" runat="server"
                                    Text="<%$RIResources:Global,Delete %>" />
                                <ajaxToolkit:ConfirmButtonExtender ID="_cbeResourceDelete" runat="server" ConfirmText="<%$RIResources:Shared,ConfirmDelete %>"
                                    TargetControlID="_btnResourceDelete">
                                </ajaxToolkit:ConfirmButtonExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="25%" HeaderText="<%$RIResources:Shared,Resource %>"
                            HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
                            <ItemTemplate>
                                <asp:Label ID="_lblResourceSeqid" runat="server" Text='<%# Bind("resourceseqid") %>' Visible="false"></asp:Label>
                                <asp:Label ID="_lblResource" runat="server" Text='<%# Bind("Person") %>'></asp:Label>
                             </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$RIResources:Shared,Start and End Dates are defaulted to Outage start/end dates.  Please update.%>" ItemStyle-Width="30%"
                            HeaderStyle-Font-Underline="true">
                            <ItemTemplate>
       							<center>
       							<IP:StartEndCalendar id="_ResourcesstartEnd" runat="server" showtime="false" changedatelabel="false" startdate='<%# Bind("startdate") %>' enddate='<%# Bind("enddate") %>' defaultstartdate='' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="50%" HeaderText="<%$RIResources:Global,Comments %>" HeaderStyle-Font-Underline="true">
                            <ItemTemplate>
                                <center>
                                    <IP:AdvancedTextBox ID="_txtCommentsResource" runat="server" ExpandHeight="true" Text='<%# Bind("resourcecomments") %>'
                                        Width="95%" TextMode="MultiLine" MaxLength="4000" style="width: 98%;font-size:12px;color:Black;font-family:Verdana;"/></center>
                           </ItemTemplate>
                        </asp:TemplateField>
          
                    </Columns>
                </asp:GridView>
            </asp:Panel>

            <asp:Table ID="_tblAssessment" runat="server" BorderWidth="0" CellPadding="2" CellSpacing="2"
			    BackColor="white" Style="width: 99%; overflow: hidden;">
                <asp:TableHeaderRow CssClass="Header">
                    <asp:TableHeaderCell  HorizontalAlign="left" ColumnSpan="4">
                        <asp:Label ID="_lbAssessTitle" runat="server" EnableViewState="false" Text="<%$RIResources:OUTAGE,Assessment %>"
                            SkinID="LabelWhite"></asp:Label>
                    </asp:TableHeaderCell>
                </asp:TableHeaderRow>
				<asp:TableRow CssClass="Border" >
					<asp:TableCell HorizontalAlign="left" VerticalAlign="Top" Width="25%" Visible="false">
					    <asp:Label ID="_lbFEPATitle" runat="server" EnableViewState="false" Text="<%$RIResources:OUTAGE,FEPAScore %>"/>
						&nbsp;&nbsp;<asp:TextBox ID="_tbFEPAScore" runat="server" MaxLength="3" width="30pt"></asp:TextBox>
						<ajaxToolkit:FilteredTextBoxExtender ID="_fbeFEPAScore" runat="server"
                            TargetControlID="_tbFEPAScore" FilterType="custom" ValidChars="1234567890">
                        </ajaxToolkit:FilteredTextBoxExtender>	
    				</asp:TableCell>
				    <asp:TableCell Width="25%" Visible="false">
					    <asp:Label ID="_lbTGMCMFTitle" runat="server" Text="<%$RIResources:OUTAGE,TGMCMFScore %>"/>
                        &nbsp;&nbsp;<asp:TextBox ID="_tbTGMCMFScore" runat="server" MaxLength="3" width="30pt"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="_fbeTGMCMFScore" runat="server"
                            TargetControlID="_tbTGMCMFScore" FilterType="custom" ValidChars="1234567890">
                        </ajaxToolkit:FilteredTextBoxExtender>
    				</asp:TableCell>
				    <asp:TableCell Width="40%" >
					    <asp:Label ID="_lbOverallTitle" runat="server" Text="<%$RIResources:OUTAGE,OverallScore %>"/>
                        &nbsp;&nbsp;<asp:TextBox ID="_tbOverallScore" runat="server" MaxLength="3" width="30pt"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="_fbeOverallScore" runat="server"
                            TargetControlID="_tbOverallScore" FilterType="custom" ValidChars="1234567890">
                        </ajaxToolkit:FilteredTextBoxExtender>
    				</asp:TableCell>
				    <asp:TableCell Width="60%" >
					    <asp:Label ID="_lbCommIssuesTitle" runat="server" Text="<%$RIResources:OUTAGE,CommIssuesCnt %>"/>
                        &nbsp;&nbsp;<asp:TextBox ID="_tbCommIssuesCnt" runat="server" MaxLength="3" width="30pt"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="_fbeCommIssuesCnt" runat="server"
                            TargetControlID="_tbCommIssuesCnt" FilterType="custom" ValidChars="1234567890">
                        </ajaxToolkit:FilteredTextBoxExtender>
    				</asp:TableCell>
			    </asp:TableRow>
			    <asp:TableRow CssClass="Border">
					<asp:TableCell ColumnSpan="4" HorizontalAlign="left">
						<asp:Label ID="_lbTGComments" runat="server" Text="<%$RIResources:Shared,Comments %>"
							EnableViewState="false" />
						<div>
							<IP:AdvancedTextBox ID="_tbTGComments" runat="server" ExpandHeight="true"
								Width="98%" Rows="2" TextMode="MultiLine" MaxLength="4000" style="width: 98%;font-size:12px;color:Black;font-family:Verdana;"/></div>
					</asp:TableCell>
				</asp:TableRow>
            </asp:Table>
			
 			<asp:Table ID="_tblInfo" runat="server" BorderWidth="0" CellPadding="2" CellSpacing="2"
				BackColor="white" Style="width: 99%">
                <asp:TableRow CssClass="Border">
					<asp:TableCell Width="25%">
						<asp:Label ID="_lblCreatedBy" runat="server"></asp:Label>
						</asp:TableCell>
					<asp:TableCell Width="25%">
						<asp:Label ID="_lblCreatedDate" runat="server"></asp:Label></asp:TableCell>
					<asp:TableCell Width="25%">
						<asp:Label ID="_lblUpdatedBy" runat="server"></asp:Label></asp:TableCell>
					<asp:TableCell Width="25%">
						<asp:Label ID="_lblLastUpdateDate" runat="server"></asp:Label></asp:TableCell>
				</asp:TableRow>
			</asp:Table>
			
			<center>
				<IP:SpellCheck ID="_btnSpell" runat="server" ControlIdsToCheck="_txtTitle,_txtDescription" />
				<asp:Button ID="_btnSubmit" Text="<%$RIResources:Outage,SubmitOutage %>" runat="server" ValidationGroup="EnterOutage" />
				<asp:Button ID="_btnDelete" runat="server" Text="<%$RIResources:Shared,Delete %>" />
				<IP:IncidentHistory ID="_btnHistory" runat="server" applid="Outage" />
				<asp:Button ID="_btnCritique" runat="server" Text="<%$RIResources:Shared,Critique %>" />
			    <ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" ConfirmText="<%$RIResources:Outage,ConfirmDeleteOutage %>" TargetControlID="_btnDelete">
			    </ajaxToolkit:ConfirmButtonExtender>

			<asp:Panel ID="_pnlUpdateButtons" runat="server" HorizontalAlign="center" Visible="true" BorderWidth="0">
				<IP:MessageBox ID="_messageBox" runat="server" ButtonType="OKCancel" />
                <asp:Button ID="_btnMajorScope" runat="server" Text="<%$RIResources:Shared,MajorScope %>" />      
                <asp:Button ID="_btnShowContractorSLB" runat="server" Text="<%$RIResources:Shared,Contractors %>" />             
                 <asp:Button ID="_btnResources" runat="server" Text="<%$RIResources:Shared,Resources %>" />
                <asp:Button ID="_btnTasks" Text="<%$RIResources:Shared,Task Items %>" runat="server" ValidationGroup="EnterOutage"/>
				<asp:Button ID="_btnAttachments" runat="server" Text="<%$RIResources:Shared,Attachments %>" ValidationGroup="EnterOutage" />		
                <asp:Button ID="_btnScorecard" runat="server" Text="<%$RIResources:Shared,Outage Scorecard %>" visible="false"/>
			</asp:Panel>
			            
            <br />
            
            <span class="ValidationError">*Required Fields</span>
   			</center>
   			
			<asp:ValidationSummary ID="_vsSummary" runat="server" CssClass="ValidationError"
				DisplayMode="BulletList" ValidationGroup="EnterOutage" HeaderText="<%$RIResources:Shared,RequiredFields %>" ShowSummary="false" EnableClientScript="true" ShowMessageBox="false" />

           
            

            <asp:Panel ID="_pnlSwapListBox" runat="server" CssClass="modalPopup" Style="display: none">
                <IP:SwapListBox ID="_slbContractorList" AvailableTextField="<%$RIResources:Shared,Available Contractors%>" SelectedTextField="<%$RIResources:Shared,Selected Contractors%>"
                    runat="server" LocalizeData="false" />
                 <div style="text-align: center">
                    <asp:Button ID="_btnAdd" runat="server" Text="<%$RIResources:Shared,Add Contractor%>" />&nbsp;&nbsp;
                    <asp:Button ID="_btnCancel" runat="server" Text="<%$RIResources:Shared,Cancel%>" /></div>
            </asp:Panel>
 
            <ajaxToolkit:ModalPopupExtender ID="_mpeSwapList" runat="server" TargetControlID="_btnShowContractorSLB"
                PopupControlID="_pnlSwapListBox" OkControlID="_btnCancel" CancelControlID="_btnCancel"
                BackgroundCssClass="modalBackground" DropShadow="true">
            </ajaxToolkit:ModalPopupExtender>
            
            <asp:Panel ID="_pnlSwapListBox2" runat="server" CssClass="modalPopup" Style="display: none">
                <IP:SwapListBox ID="_slbResourceList" AvailableTextField="<%$RIResources:Shared,Available Resources%>" SelectedTextField="<%$RIResources:Shared,Selected Resources%>"
                    runat="server" LocalizeData="false" />
                 <div style="text-align: center">
                    <asp:Button ID="_btnAddResource" runat="server" Text="<%$RIResources:Shared,Add Resource%>" />&nbsp;&nbsp;
                    <asp:Button ID="_btnCancel2" runat="server" Text="<%$RIResources:Shared,Cancel%>" /></div>
           </asp:Panel>
           
            <ajaxToolkit:ModalPopupExtender ID="_mpeSwapList2" runat="server" TargetControlID="_btnResources"
                PopupControlID="_pnlSwapListBox2" OkControlID="_btnCancel2" CancelControlID="_btnCancel2"
                BackgroundCssClass="modalBackground" DropShadow="true">
            </ajaxToolkit:ModalPopupExtender>
            
             <div style="display: none">
            <asp:button id="_btnOutageDateChangeHidden" runat="server" text="DateChangeButton" />
            </div>
            
             <ajaxToolkit:ModalPopupExtender ID="_mpeOutageDateChange" runat="server" TargetControlID="_btnOutageDateChangeHidden"
                PopupControlID="_pnlOutageDateChange"   BackgroundCssClass="modalBackground" DropShadow="true">     
                </ajaxToolkit:ModalPopupExtender>
                <%--OkControlID="_btnCancel3" CancelControlID="_btnCancel3"--%>
            
            <asp:Panel ID="_pnlOutageDateChange" runat="Server" HorizontalAlign="Center" Style="display: none" CssClass="modalPopup" >
<%--		        <div style="width:100%; text-align:center; border-style:solid; border-width:thin;" >  
--%>		    <div style="text-align: center ; font-size:large; " >
<%--		        <asp:Label ID="_lblOutageDateChange" runat="Server" Text="The Outage Start or End date has changed. Open tasks and future contractor dates will be changed in accordance to the number of days the Outage Dates changed.<br/><br/>Please enter a comment regarding why the dates were changed."></asp:Label>--%>
		        <asp:Label ID="_lblOutageDateChange" runat="Server" Text="The Outage Start or End date has changed. Open tasks and future contractor dates will be changed in accordance to the number of days the Outage Dates changed.<br/><br/>Please enter a comment regarding why the dates were changed."></asp:Label>
                <br /><br />
  <%--              <asp:Literal ID="_litComment" runat="Server" Text="Comment"></asp:Literal>
  --%>              </div>
                
                <div>
				<IP:AdvancedTextBox ID="_tbDateChangeComments" runat="server" ExpandHeight="true"
					Width="98%" Rows="2" TextMode="MultiLine" MaxLength="2000" style="width: 98%;font-size:12px;color:Black;font-family:Verdana;"/></div>
				<asp:HiddenField ID="_hfDateComment" runat="server" />
				<asp:HiddenField ID="_hfDateChangeFlag" runat="server" />
<%--                <asp:TextBox ID="_tbDateChangeComment" runat="server" Width="50"  ></asp:TextBox>
--%>                <br />
                <asp:Button ID="_btnOutageDateChange" runat="server" Text="<%$RIResources:Shared,OK%>" />&nbsp;&nbsp;
           
             </asp:Panel>
 		</ContentTemplate>
	</Asp:UpdatePanel>
              
             
	
    <ajaxToolkit:modalpopupextender id="_mpeTemplates" BehaviorID="mpe" runat="server" 
	    targetcontrolid="ButtonTemplateTasks" popupcontrolid="_pnlTemplateTasks" 
	    backgroundcssclass="modalBackground" DropShadow="true" cancelcontrolid='btnCancel'>
    </ajaxToolkit:modalpopupextender>

    <asp:button id="ButtonTemplateTasks" runat="server" text="Button" style="display:none"/>
            
    <asp:panel id="_pnlTemplateTasks" runat="Server" HorizontalAlign="Center" style="height:600px; overflow:auto; width:auto;" CssClass="modalPopup">
        <asp:ObjectDataSource runat="server" ID="_objDS"></asp:ObjectDataSource>
    
        <Asp:UpdatePanel id="_udptasks" runat="server" updatemode="Conditional" >
		<ContentTemplate>
            <div style="width:100%; text-align:center; border-style:solid; border-width:thin;" >
                <asp:Table runat="server" ID="_tbl2">
                    <asp:TableHeaderRow CssClass="Header">
                        <asp:TableHeaderCell  HorizontalAlign="center">
                            <asp:Label ID="_lbTemplateTitle" runat="server" CssClass="Heading" Text="<%$RIResources:Shared,TaskTemplate%>" SkinID="LabelWhite"></asp:Label><br />
                        </asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>
                <asp:Label ID="_lblTemplateHeading" runat="server" 
                    Text="<%$RIResources:Shared,TGOutageTemplateInstructions%>"></asp:Label>
                <br />
                <br />
                <div style="width:100%; text-align:center; " >
                    <center>
                    <asp:Button id="_btnCreateAll" runat="server" Text="<%$RIResources:Shared,Create All Tasks%>" ValidationGroup="EnterOutage" />&nbsp;
                    
                        <asp:Button id="_btnCreate" runat="server" Text="<%$RIResources:Shared,Create Selected Tasks%>" ValidationGroup="EnterOutage" />&nbsp;
<%--                        <asp:Button id="_btnCancel3" runat="server" text="Cancel" style="display:none"/>
--%>                        <asp:HiddenField ID="_hfTasksCreated" runat="server" />
		            <input type="button" id="btnCancel" onclick="Hide()" value="Close" /><br />
		            <asp:Label id="_lblTaskStatus" runat="server" ForeColor="Red"></asp:Label>
                    </center>
		        </div>
                <br />

            <div id="_divNewTemplate" runat="server">
            <asp:CheckBox ID="_cbSaveTemplate" runat="server" Visible="false" Text="Check Here if you would like to save any changes you made to the area roles to a NEW outage task template.  Template will be saved when you click Save Tasks." />
            <br />
            <asp:Label ID="_lbNewTemplateName" visible="false" runat="server" Text="<%$RIResources:Shared,New Template Name%>"></asp:Label>
            <asp:TextBox ID="_tbTemplateName" Visible="false" runat="server" Width="20%" MaxLength="20"></asp:TextBox>
                </div>
            </div>  
            <div style="display:none">
                <asp:DropDownList ID="_ddlHiddenRoles" runat="server">
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
            </div>
            
            <asp:Panel id="_pnlTemplateTasks2" runat="Server" style="height:auto; margin-top:auto; margin-bottom:auto; overflow:auto;">
                <asp:GridView ID="_grvTasksPage" runat="server" CssClass="Border" AllowPaging="true" PageSize="100"
                    BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" RowStyle-VerticalAlign="top"
                    Width="100%" Font-Size="8" EnableViewState="true" OnPageIndexChanging="_grvTasksPage_PageIndexChanging" DataKeyNames="taskitemseqid">
                    <PagerSettings Position="TopAndBottom"  />
                    <PagerStyle HorizontalAlign = "Right" CssClass="cssPager" />
                    
                <HeaderStyle CssClass="LockHeader" Font-Size="8" />
                    <EmptyDataTemplate>
                        <asp:Label ID="_lblNoRecordsFound" runat="server" Text='<%$RIResources:Shared,NoRecordsFound %>'></asp:Label>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$RIResources:Shared,Create%>" ItemStyle-HorizontalAlign="center" HeaderStyle-Font-Size="8">
                        <ItemTemplate>
                        <asp:HiddenField ID="_hfRoleCount" runat="server" Value='<%# Bind("taskitemrolecnt") %>' />
                        <asp:HiddenField ID="_hfRoleSeqId" runat="server" Value='<%# Bind("taskitemroleseqid") %>' />
                        <asp:HiddenField ID="_hfTaskItemSeqID" runat="server" Value='<%# Bind("TaskItemSeqid") %>' />
                            <asp:CheckBox Visible="true" ID="_cbStatus" runat="server" Checked="true" />
                        </ItemTemplate>
                        </asp:TemplateField>
                          
                        <asp:TemplateField HeaderText="<%$RIResources:Shared,Title/Description%>" ItemStyle-HorizontalAlign="center" ItemStyle-Width="20%" HeaderStyle-Font-Size="8" >
                            <ItemTemplate>
                                <asp:label id="_lblTitle" runat="server" Font-Bold="false" font-size="8" Text='<%# Bind("title") %>' /><br />
                                <asp:label id="_lblDesc" runat="server" Font-Bold="false" font-size="8" Text='<%# Bind("description") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="WeeksBefore" HeaderText="<%$RIResources:Shared,Weeks Before%>" ItemStyle-HorizontalAlign="center"  HeaderStyle-Font-Size="8"/>
                        <asp:BoundField DataField="WeeksAfter" HeaderText="<%$RIResources:Shared,Weeks After%>" HeaderStyle-Font-Size="8"/>
<%--                        <asp:BoundField DataField="RoleDescription" HeaderText="<%$RIResources:Shared,Primary Role%>" ItemStyle-Font-Size="8" HeaderStyle-Font-Size="8"/>
--%>                        <asp:TemplateField HeaderText="<%$RIResources:Shared,Primary Role%>" ItemStyle-HorizontalAlign="center" ItemStyle-Width="20%" HeaderStyle-Font-Size="8" >
                            <ItemTemplate>
                                <asp:label id="_lblPrimaryRole" runat="server" Font-Bold="false" font-size="8" Text='<%# RI.SharedFunctions.LocalizeValue(DataBinder.Eval(Container.DataItem,"RoleDescription")) %>' /><br />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$RIResources:Shared,Area Role%>" ItemStyle-Width="15%" HeaderStyle-Font-Size="8">
                            <ItemTemplate>
                                <asp:DropDownList ID="_ddlTemplateRole" runat="server" Width="100%" Font-Size="8"  >
                                <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="<%$RIResources:Shared,Area Role%>" ItemStyle-Width="15%" HeaderStyle-Font-Size="8">
                            <ItemTemplate>
                                <asp:DropDownList ID="_ddlTemplateRole2" runat="server" Width="100%" Font-Size="8">
                                <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="<%$RIResources:Shared,Area Role%>" ItemStyle-Width="15%" HeaderStyle-Font-Size="8">
                            <ItemTemplate>
                                <asp:DropDownList ID="_ddlTemplateRole3" runat="server" Width="100%" Font-Size="8">
                                <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="<%$RIResources:Shared,Area Role%>" ItemStyle-Width="15%" HeaderStyle-Font-Size="8">
                            <ItemTemplate>
                                <asp:DropDownList ID="_ddlTemplateRole4" runat="server" Width="100%" Font-Size="8">
                                <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="<%$RIResources:Shared,Area Role%>" ItemStyle-Width="15%" HeaderStyle-Font-Size="8">
                            <ItemTemplate>
                                <asp:DropDownList ID="_ddlTemplateRole5" runat="server" Width="100%" Font-Size="8">
                                <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                
            </asp:Panel> 
                         
       </ContentTemplate>
    </Asp:UpdatePanel>
    </asp:panel>
</asp:Content>


