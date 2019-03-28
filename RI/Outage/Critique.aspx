<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="Critique.aspx.vb"
    Inherits="RI_Outage" Title="Outage Critique" EnableViewState="True"
    EnableViewStateMac="false" ViewStateEncryptionMode="never" Trace="false" EnableEventValidation="false"  %>

<%@ MasterType VirtualPath="~/RI.master" %>
<%@ Register Namespace="RealWorld.Grids" TagPrefix="rwg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

  


<asp:Content ID="_contentCritique" ContentPlaceHolderID="_cphMain" runat="Server">
    
      <script type="text/javascript" >  
      function CritiqueTableSorter(gridId) {
      $(document).ready(function() { 
          $('#' + gridId).tablesorter({
              debug:false
           /*   headers:{
                  2:{
                      sorter:"select"
                  },
                   3:{
                      sorter:"select"
                  }
              },
              textExtraction: {
    2: function(node) {
        return $(node).find('option:selected').text();
    },
    3: function(node) {
        return $(node).find('option:selected').text();
    }}*/
          })
      })
  } 
  </script>
    <Asp:UpdatePanel ID="_udpCritique" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <asp:Label ID="_lblOutage"   Text="Outage: " Font-Bold="true" runat="server" ></asp:Label>
            <asp:Label ID="_lblOutageNumber"  Font-Bold="true" runat="server" ></asp:Label>
            <asp:Label ID="_lblOutageTitle"    Font-Bold="true" runat="server" ></asp:Label>
          
                <asp:Button ID="_btnSave1"  runat="server" Text="<%$RIResources:Shared,Save%>" />
                 <asp:Button ID="_btnSaveClose1" visible="false" runat="server" Text="<%$RIResources:Shared,SaveClose%>"  />  
           <%-- &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
           <%-- <span class="ValidationError">*</span>--%>
            <asp:Label ID="Label5" runat="server" Visible="false" Text='<%$ RIResources:ValidateRequiredFields %>' ForeColor="Red"></asp:Label>
                   <br /><br />
            
            
               <asp:Table ID="_tblPerfRec" runat="server" Width="100%"  BorderStyle="Outset">
                <asp:TableRow CssClass="Header" >
                    <asp:TableCell Width="100%" ColumnSpan="8" Style="text-align: left">
                        <asp:Label ID="Label15" SkinID="LabelWhite" runat="server" Text="Performance to Timeline"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>              
            </asp:Table>
             
             <RWG:BulkEditGridView ShowHeader="true" ID="_gvcritiqueperformance" runat="server"
                AutoGenerateColumns="False" DataKeyNames="performancetypeseqid" EnableInsert="False"
                SaveButtonID="" Width="100%" CellPadding="1" EnableViewState="true" AllowSorting="false">
                <columns>
               <asp:TemplateField ItemStyle-Width="20%" HeaderText="Critical Path/Constrained Area" SortExpression="Critical Path/Constrained Area">
                        <itemtemplate>                 
                           <asp:Label id="_lblPerformanceType" runat="server" width="90%"  Text='<%# Container.DataItem("PerformanceType") %>' > </asp:Label>
 		                </itemtemplate>
  	                     </asp:TemplateField>
                      
 	                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Planned Start" SortExpression="Planned Start">
                        <itemtemplate>       
                            <asp:TextBox id="_tbPlannedStart" cssclass="PerformanceDateTimePicker" runat="server" width="90%" Style="text-align: center"  Text='<%# Container.DataItem("PlannedStartDate") %>' > </asp:TextBox>
                         </itemtemplate>
  	                    </asp:TemplateField>

	                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Planned End" SortExpression="Planned End">
                        <itemtemplate>           
 	                    <asp:TextBox id="_tbPlannedEnd" cssclass="PerformanceDateTimePicker" runat="server" width="90%" Style="text-align: center"  Text='<%# Container.DataItem("PlannedEndDate") %>' >   </asp:TextBox>
                           </itemtemplate>
  	                    </asp:TemplateField>

	                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Actual Start" SortExpression="Actual Start">
                        <itemtemplate>        
	                    <asp:TextBox id="_tbActualStart" cssclass="PerformanceDateTimePicker" runat="server" width="90%" Style="text-align: center"  Text='<%# Container.DataItem("ActualStartDate") %>' >   </asp:TextBox>
                       </itemtemplate>
  	                     </asp:TemplateField>
   
 	                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Actual End" SortExpression="Actual End">
                        <itemtemplate>         
  		                <asp:TextBox id="_tbActualEnd" cssclass="PerformanceDateTimePicker" runat="server" width="90%" Style="text-align: center" Text='<%# Container.DataItem("ActualEndDate") %>' >  </asp:TextBox>
                        </itemtemplate>
  	                    </asp:TemplateField>
  
	                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Planned Hrs" SortExpression="Planned Hrs">
                        <itemtemplate>          
	                    <asp:Label id="_lblPlanned" runat="server" width="90%" Style="text-align: center"  Text='<%# Container.DataItem("PlannedHrs") %>' >    </asp:Label>
                        </itemtemplate>
  	                    </asp:TemplateField>

	                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Actual Hrs" SortExpression="Actual Hrs">
                        <itemtemplate>             
	                    <asp:Label id="_lblActual" runat="server" width="90%" Style="text-align: center"  Text='<%# Container.DataItem("ActualHrs") %>' > </asp:Label>
                          </itemtemplate>
  	                       </asp:TemplateField>

 	                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Variance Hrs" SortExpression="Variance Hrs">
                        <itemtemplate>          
	                    <asp:Label id="_lblVarianceHr" runat="server" width="90%" Style="text-align: center"  Text='<%# Container.DataItem("VarianceHrs") %>' >  </asp:Label>
 	                    </itemtemplate>
  	                    </asp:TemplateField>
                            
	                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Variance Percent" SortExpression="Variance Percent">
                        <itemtemplate>          
	                    <asp:Label id="_lblVariancePerc" runat="server" width="90%" Style="text-align: center"  Text='<%# Container.DataItem("VariancePercent")  & "%" %>'>  </asp:Label>
                       </itemtemplate>
  	                    </asp:TemplateField>
                    
	                    <asp:TemplateField>
                        <itemtemplate>
                        <center>
 	                    <asp:linkbutton id="_lnkBtnPerformance" text="Delete" Visible="true" runat="server" CommandName="Delete"  datatextfield="performancetypeseqid" >                         
                        <span class="ui-icon ui-icon-trash" title="Delete"  /> 
                       </asp:linkbutton></center> 
                </itemtemplate>
                    </asp:TemplateField>
                </columns>
                <headerstyle backcolor="#CCCC99" forecolor="Black" wrap="False" />
                <rowstyle cssclass="Border" />
                <alternatingrowstyle cssclass="BorderSecondary" />
            </RWG:BulkEditGridView>

           
            <asp:Table ID="_tblNewPerf" runat="server" Width="100%"  BorderStyle="Outset">
            <asp:TableRow VerticalAlign="Top" BackColor="LightGray">   	                    
             <asp:TableCell  Width="20%">
	            <span class="ValidationError"></span>
	            <asp:Label ID="_lblPerf" runat="server" Text="Critical Path"></asp:Label><br />
                   <IP:AdvancedTextBox ID="_tbNewPerformanceItem" runat="server" TextMode="SingleLine" ExpandHeight="true"
                            Style="font-family: Verdana; font-size: 12px" Rows="1" MaxLength="100" Width="95%"
                            CausesValidation="false"></IP:AdvancedTextBox>
                </asp:TableCell>
                <asp:TableCell  Width="30%">
                <span class="ValidationError"></span>
                <asp:Label ID="_lblConstrainedArea" runat="server" Text="Constrained Area"></asp:Label><br />
	            <asp:DropDownList ID="_ddlNewBusUnitArea" runat="server" Style="font-family: Verdana;
                            font-size: 12px"  Width="95%"></asp:DropDownList> 
                  </asp:TableCell>
                   <asp:TableCell  Width="10%">
                   <asp:Label ID="Label19" runat="server" Text="Planned Start"></asp:Label><br />
                   <asp:TextBox id="_tbNewPlannedStart" cssclass="PerformanceDateTimePicker" runat="server" width="95%" Style="text-align: center"   >
                            </asp:TextBox></asp:TableCell>
                            <asp:TableCell width="10%" VerticalAlign="top"> 
                         <asp:Label ID="Label20" runat="server" Text="Planned End"></asp:Label><br />  
                          <asp:TextBox id="_tbNewPlannedEnd" cssclass="PerformanceDateTimePicker" runat="server" width="95%" Style="text-align: center"  >
                            </asp:TextBox></asp:TableCell>
                            
	                <asp:TableCell Width="10%">
	                <asp:Label ID="Label21" runat="server" Text="Actual Start"></asp:Label><br />  
	                 <asp:TextBox id="_tbNewActualStart" cssclass="PerformanceDateTimePicker" runat="server" width="95%" Style="text-align: center"   >
                            </asp:TextBox></asp:TableCell>
                            <asp:TableCell width="10%" VerticalAlign="top"> 
                        <asp:Label ID="Label22" runat="server" Text="Actual End"></asp:Label><br />  
                             <asp:TextBox id="_tbNewActualEnd" cssclass="PerformanceDateTimePicker" runat="server" width="95%" Style="text-align: center"  >
                            </asp:TextBox></asp:TableCell>
                             <asp:TableCell Width="20%" Style="text-align: center">
                    <br /><asp:Button ID="_btnAddPerf"  runat="server" Text="<%$RIResources:Shared,Add%>" />
                    </asp:TableCell> 
                         <%--<asp:TableCell Width="20%">   </asp:TableCell>--%>
                            </asp:TableRow>
                             </asp:Table>
            
            
            
            <asp:Table ID="Table1" runat=server width ="100%" borderwidth="1"> 
            <asp:TableRow CssClass="Header" >
                    <asp:TableCell Width="20%" ColumnSpan="7" Style="text-align: left">
                        <asp:Label ID="_lblCost" width="90%" SkinID="LabelWhite" runat="server" Text="Maintenance Spending"></asp:Label>
                    </asp:TableCell></asp:TableRow>
            
                    <asp:TableRow BackColor="LightGray">
                    <asp:TableCell width="10%" VerticalAlign="top"> 
                    <asp:Label ID="_lblPlannedCost"  Text="Planned Cost US$"  Font-Bold="true" runat="server" ></asp:Label>
                    </asp:TableCell>
                     <asp:TableCell width="10%" VerticalAlign="top"> 
                     <asp:TextBox id="_tbPlannedCost" runat="server" width="90%"  >
                            </asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="_fbePlannedCost" runat="server" TargetControlID="_tbPlannedCost" FilterType="custom" ValidChars=",.1234567890"> </ajaxToolkit:FilteredTextBoxExtender>	
                        </asp:TableCell>
                         <asp:TableCell width="10%" VerticalAlign="top"> 
                          <asp:Label ID="_lblActualCost"  Text="Actual Cost US$"  Font-Bold="true" runat="server" ></asp:Label>
                          </asp:TableCell>
                          <asp:TableCell width="10%" VerticalAlign="top"> 
                           <asp:TextBox id="_tbActualCost" runat="server" width="90%"  ></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="_fbeActualCost" runat="server" TargetControlID="_tbActualCost" FilterType="custom" ValidChars=",.1234567890"> </ajaxToolkit:FilteredTextBoxExtender>	
                        </asp:TableCell>
                        <asp:TableCell width="13%" VerticalAlign="top"> 
                          <asp:Label ID="_lblCostVariance"  Text="Cost Variance US$"  Font-Bold="true" runat="server" ></asp:Label>
                          </asp:TableCell>
                          <asp:TableCell width="10%" VerticalAlign="top"> 
                           <asp:Label id="_tbCostVariance"  runat="server" width="90%"  >
                            </asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="37%" Columnspan="1" Style="text-align: left"> </asp:TableCell>
                    </asp:TableRow>
   				    </asp:table>   	
            
           
           
           
              
             
              <asp:Table  runat="server" width ="100%" borderwidth="2">       
                <asp:TableRow CssClass="Header">
                    <asp:TableCell Width="10%"  Style="text-align: left">
                    <asp:Label ID="Label16" SkinID="LabelWhite" runat="server" Text="Maintenance Job Efficiency"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="20%"  Style="text-align: center">
                        <asp:Label ID="Label3" SkinID="LabelWhite" runat="server" Text="Planned"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="20%"  Style="text-align: center">
                        <asp:Label ID="Label4" SkinID="LabelWhite" runat="server" Text="Completed"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="20%" Style="text-align: center">
                        <asp:Label ID="Label6" SkinID="LabelWhite" runat="server" Text="Completed Percentage"></asp:Label>
                    </asp:TableCell>
                     <asp:TableCell Width="20%"  Style="text-align: center">
                        <asp:Label ID="Label1" SkinID="LabelWhite" runat="server" Text="Not Worked"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
             </asp:Table>
             
                                   
              <RWG:BulkEditGridView ShowHeader="true" ID="_gvcritiqueWO" runat="server"
                AutoGenerateColumns="False" DataKeyNames="outagecritiquewoseqid" EnableInsert="False"
                SaveButtonID="" Width="100%" CellPadding="1" EnableViewState="true">
                <columns>
                     <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left" Headerstyle-HorizontalAlign="left" HeaderText="Work Order Completion" >
                        <itemtemplate>   
                     <asp:Label ID="_lblWOType" runat="server" Text='<%# Container.DataItem("WorkOrderType") %>' > </asp:Label>
                     </itemtemplate> </asp:TemplateField>
                     <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center" Itemstyle-Backcolor="LightGray" Headerstyle-Backcolor="LightGray" HeaderText="Mechanical">
                        <itemtemplate>  
                      <asp:TextBox id="_tbPlannedMechanical" runat="server" Style="text-align: center" width="50%" MaxLength="5" Text='<%# Container.DataItem("PlannedMechanicalCount") %>' > </asp:TextBox>
                       <asp:Label ID="_lblPercent_Mech" runat="server" Text='%' > </asp:Label>
                       <ajaxToolkit:FilteredTextBoxExtender ID="_fbePlannedMechanical" runat="server" TargetControlID="_tbPlannedMechanical" FilterType="custom" ValidChars=".1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                             </itemtemplate> </asp:TemplateField>
                     <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center" Itemstyle-Backcolor="LightGray" Headerstyle-Backcolor="LightGray" HeaderText="Elect/Inst." >
                        <itemtemplate> 
                         <asp:TextBox id="_tbPlannedElectrical" runat="server" Style="text-align: center" width="50%" MaxLength="5" Text='<%# Container.DataItem	("PlannedElectricalCount") %>' > </asp:TextBox>
                         <asp:Label ID="_lblPercent_Elect" runat="server" Text='%' > </asp:Label>
                         <ajaxToolkit:FilteredTextBoxExtender ID="_fbePlannedElectrical" runat="server" TargetControlID="_tbPlannedElectrical" FilterType="custom" ValidChars=".1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                        </itemtemplate> </asp:TemplateField> 
                      <asp:TemplateField ItemStyle-Width="5%"  ItemStyle-HorizontalAlign="center" Itemstyle-Backcolor="LightGray" Headerstyle-Backcolor="LightGray" HeaderText="Contracted" >
                        <itemtemplate> 
                        <asp:TextBox id="_tbPlannedContracted" runat="server" Style="text-align: center" width="50%" MaxLength="5" Text='<%# Container.DataItem("PlannedContractedCount") %>' > </asp:TextBox>
                        <asp:Label ID="_lblPercent_Cont" runat="server" Text='%' > </asp:Label>
                        <ajaxToolkit:FilteredTextBoxExtender ID="_fbePlannedContracted" runat="server" TargetControlID="_tbPlannedContracted" FilterType="custom" ValidChars=".1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                        </itemtemplate> </asp:TemplateField> 
                        <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center" Itemstyle-Backcolor="LightGray" Headerstyle-Backcolor="LightGray" HeaderText="Total" >
                        <itemtemplate> 
                        <asp:Label id="_lblTotalPlanned" runat="server" Style="text-align: center" width="50%" MaxLength="5" Text='<%# Container.DataItem("TotalPlanned") %>' > </asp:Label>
                        <asp:Label ID="_lblPercent_Total" runat="server" Text='%' > </asp:Label>
                        </itemtemplate> </asp:TemplateField>
                         
                      <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center" Itemstyle-Backcolor="#CCCC99" Headerstyle-Backcolor="#CCCC99" HeaderText="Mechanical" >
                        <itemtemplate> 
                        <asp:TextBox id="_tbCompletedMechanical" runat="server" Style="text-align: center" width="50%" MaxLength="5" Text='<%# Container.DataItem("CompletedMechanicalCount") %>' > </asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="_fbeCompletedMechanical" runat="server" TargetControlID="_tbCompletedMechanical" FilterType="custom" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                        </itemtemplate> </asp:TemplateField> 
                      <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center"  Itemstyle-Backcolor="#CCCC99"  Headerstyle-Backcolor="#CCCC99" HeaderText="Elect/Inst." >
                        <itemtemplate> 
                        <asp:TextBox id="_tbCompletedElectrical" runat="server" Style="text-align: center" width="50%" MaxLength="5" Text='<%# Container.DataItem("CompletedElectricalCount") %>' > </asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="_fbeCompletedElectrical" runat="server" TargetControlID="_tbCompletedElectrical" FilterType="custom" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                        </itemtemplate> </asp:TemplateField>
                      <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center"  Itemstyle-Backcolor="#CCCC99" Headerstyle-Backcolor="#CCCC99" HeaderText="Contracted" >
                        <itemtemplate> 
                        <asp:TextBox id="_tbCompletedContracted" runat="server" Style="text-align: center" width="50%" MaxLength="5" Text='<%# Container.DataItem("CompletedContractedCount") %>' > </asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="_fbeCompletedContracted" runat="server" TargetControlID="_tbCompletedContracted" FilterType="custom" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                        </itemtemplate> </asp:TemplateField> 
                        <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center"  Itemstyle-Backcolor="#CCCC99" Headerstyle-Backcolor="#CCCC99" HeaderText="Total" >
                        <itemtemplate> 
                        <asp:Label id="_lblTotalCompleted" runat="server" Style="text-align: center" width="50%" MaxLength="5" Text='<%# Container.DataItem("TotalComplete") %>' > </asp:Label>
                        </itemtemplate> </asp:TemplateField>
                        
                        <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center"  Itemstyle-Backcolor="LightGray" Headerstyle-Backcolor="LightGray" HeaderText="Mechanical" >
                        <itemtemplate>  
                        <asp:Label id="_tbCompletedMechanicalPercent" runat="server" Style="text-align: center" width="50%" MaxLength="5" readonly="true" Text='<%# Container.DataItem("CompletedMechanicalPercent") & "%" %>' > </asp:Label>
                        </itemtemplate> </asp:TemplateField> 
                      <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center"  Itemstyle-Backcolor="LightGray"  Headerstyle-Backcolor="LightGray" HeaderText="Elect/Inst." >
                        <itemtemplate> 
                        <asp:Label id="_tbCompletedElectricalPercent" runat="server" Style="text-align: center" width="50%" MaxLength="5" readonly="true" Text='<%# Container.DataItem("CompletedElectricalPercent") & "%" %>' > </asp:Label>
                       </itemtemplate> </asp:TemplateField> 
                      <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center"  Itemstyle-Backcolor="LightGray" Headerstyle-Backcolor="LightGray" HeaderText="Contracted" >
                        <itemtemplate> 
                        <asp:Label id="_tbCompletedContractedPercent" runat="server" Style="text-align: center" width="50%" MaxLength="5" readonly="true" Text='<%# Container.DataItem("CompletedContractedPercent") & "%"  %>' > </asp:Label>
                 </itemtemplate> </asp:TemplateField>
                 <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center"  Itemstyle-Backcolor="LightGray" Headerstyle-Backcolor="LightGray" HeaderText="Total" >
                        <itemtemplate> 
                        <asp:Label id="_tbTotalPercent" runat="server" Style="text-align: center" width="50%" MaxLength="5" Text='<%# Container.DataItem("TotalPercent")  & "%" %>' > </asp:Label>
                        </itemtemplate> </asp:TemplateField> 
                        
                         <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center"  Itemstyle-Backcolor="#CCCC99" Headerstyle-Backcolor="#CCCC99" HeaderText="Mechanical" >
                        <itemtemplate>  
                        <asp:Label id="_tbMechanicalNotWorked" runat="server" Style="text-align: center" width="50%" MaxLength="5" readonly="true" Text='<%# Container.DataItem("MechNotWorked")  %>' > </asp:Label>
                        </itemtemplate> </asp:TemplateField> 
                      <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center"  Itemstyle-Backcolor="#CCCC99" Headerstyle-Backcolor="#CCCC99" HeaderText="Elect/Inst." >
                        <itemtemplate> 
                        <asp:Label id="_tbElectricalNotWorked" runat="server" Style="text-align: center" width="50%" MaxLength="5" readonly="true" Text='<%# Container.DataItem("ElectNotWorked")  %>' > </asp:Label>
                       </itemtemplate> </asp:TemplateField> 
                      <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center"  Itemstyle-Backcolor="#CCCC99" Headerstyle-Backcolor="#CCCC99" HeaderText="Contracted" >
                        <itemtemplate> 
                        <asp:Label id="_tbContractedNotWorked" runat="server" Style="text-align: center" width="50%" MaxLength="5" readonly="true" Text='<%# Container.DataItem("ContractedNotWorked")  %>' > </asp:Label>
                 </itemtemplate> </asp:TemplateField>
                 <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center"  Itemstyle-Backcolor="#CCCC99"  Headerstyle-Backcolor="#CCCC99" HeaderText="Total" >
                        <itemtemplate> 
                        <asp:Label id="_tbTotalNotWorked" runat="server" Style="text-align: center" width="50%" MaxLength="5" Text='<%# Container.DataItem("TotalNotWorked") %>' > </asp:Label>
                        </itemtemplate> </asp:TemplateField> 
                 </columns>
                <headerstyle backcolor="#CCCC99" forecolor="Black" wrap="False" />
                <rowstyle cssclass="Border" />
                <alternatingrowstyle cssclass="BorderSecondary" />
            </RWG:BulkEditGridView>
            
            <asp:Table runat="server" width ="100%" borderwidth="2">
                 <asp:TableRow CssClass="Header">
                    <asp:TableCell Width="100%"  Style="text-align: left">
                        <asp:Label ID="_lblQA" SkinID="LabelWhite" runat="server" Text="QA/QC and Startup Checklist Performance"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
             </asp:Table>
             
                                             
              <RWG:BulkEditGridView ShowHeader="false" ID="_gvcritiqueQA" runat="server"
                AutoGenerateColumns="False" DataKeyNames="outagecritiquequalityseqid" EnableInsert="False"
                SaveButtonID="" Width="100%" CellPadding="1" EnableViewState="true">
                <columns>
                    <asp:TemplateField>
                        <edititemtemplate>
                    <asp:Table runat=server width ="50%" borderwidth="2"> 
                    <asp:TableRow >
                     <asp:TableCell width="10%" VerticalAlign="top"> 
                     <asp:Label ID="_lblQAPlanned" runat="server" Text='<%# Container.DataItem("PlannedQuestion") %>' > </asp:Label></asp:TableCell>
                     <asp:TableCell width="10%" VerticalAlign="top">
                      <asp:TextBox id="_tbQAPlannedCount" runat="server" Style="text-align: center" width="50%"  Text='<%# Container.DataItem("QualityPlannedCount") %>' > </asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="_fbeQAPlannedCount" runat="server"
                            TargetControlID="_tbQAPlannedCount" FilterType="custom" ValidChars="1234567890"> </ajaxToolkit:FilteredTextBoxExtender>	
                            </asp:TableCell>
                         <asp:TableCell width="10%" VerticalAlign="top">
                         <asp:Label ID="_lblQAComplete" runat="server" Text='<%# Container.DataItem("CompleteQuestion") %>' > </asp:Label></asp:TableCell>
                     <asp:TableCell width="10%" VerticalAlign="top">
                      <asp:TextBox id="_tbQACompleteCount" runat="server" Style="text-align: center" width="50%"  Text='<%# Container.DataItem("QualityCompleteCount") %>' > </asp:TextBox>
                       <ajaxToolkit:FilteredTextBoxExtender ID="_fbeQACompleteCount" runat="server"
                            TargetControlID="_tbQACompleteCount" FilterType="custom" ValidChars="1234567890"> </ajaxToolkit:FilteredTextBoxExtender>	
                             </asp:TableCell>
                        <asp:TableCell width="10%" VerticalAlign="top">
                         <asp:Label ID="_lblQAPercent" runat="server" Text='<%# Container.DataItem("PercentQuestion") %>' > </asp:Label></asp:TableCell>
                     <asp:TableCell width="10%" VerticalAlign="top">
                      <asp:Label id="_tbQAPercentCount" runat="server" Style="text-align: center" width="50%"  Text='<%# Container.DataItem("QualityPercentCount") & "%" %>' > </asp:Label>
                          </asp:TableCell>
                        <asp:TableCell width="10%" VerticalAlign="top">
                        </asp:TableCell>
                    </asp:TableRow>
   	            </asp:table>   				   
                        
                        </edititemtemplate>
                        <headerstyle cssclass="Border" width="100%" />
                    </asp:TemplateField>
                </columns>
                <headerstyle backcolor="#CCCC99" forecolor="Black" wrap="False" />
                <rowstyle cssclass="Border" />
                <alternatingrowstyle cssclass="BorderSecondary" />
            </RWG:BulkEditGridView>
              
              
               <asp:Table ID="_tblProcessAvailability" runat="server" Width="50%" BackColor="#CCCC99" BorderStyle="Outset">
                <asp:TableRow CssClass="Header" >
                    <asp:TableCell Width="20%" ColumnSpan="1" Style="text-align: left">
                        <asp:Label ID="Label17" width="90%" SkinID="LabelWhite" runat="server" Text="Downtime After Process Area Startup - excluding LOO (minutes)"></asp:Label>
                    </asp:TableCell>
                     
                </asp:TableRow>
             </asp:Table>
             <asp:Table ID="_tblNewAvailRec" runat="server" Width="100%"  BorderStyle="Outset">
                
                <asp:TableRow VerticalAlign="Top" BackColor="LightGray">
                
                
                 <asp:TableCell  Width="15%" VerticalAlign="Bottom">
                <asp:Label ID="Label7" runat="server" Text="Business Unit-Area-Line"></asp:Label><br />
	            <asp:DropDownList ID="_ddlNewBusUnitAreaAvail" runat="server" Style="font-family: Verdana;
                            font-size: 12px"  Width="95%"></asp:DropDownList> 
                  </asp:TableCell>
                   <asp:TableCell  Width="5%" HorizontalAlign="Center">
                   <asp:Label ID="Label8" runat="server" Style="text-align: center"  Text="HR 0-12<br />(DT in min)"></asp:Label><br />
                   <asp:TextBox id="_tbNewDowntime1" runat="server" width="50%" Style="text-align: center" MaxLength="5"  >
                            </asp:TextBox>
                   <ajaxToolkit:FilteredTextBoxExtender ID="_fbeNewDowntime1" runat="server" TargetControlID="_tbNewDowntime1" FilterType="custom" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                    </asp:TableCell>
                    <asp:TableCell  Width="5%" HorizontalAlign="Center">
                   <asp:Label ID="Label9" runat="server" Text="HR 12-24<br />(DT in min)"></asp:Label><br />
                   <asp:TextBox id="_tbNewDowntime2" runat="server" width="50%" Style="text-align: center"  MaxLength="5" >
                            </asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="_fbeNewDowntime2" runat="server" TargetControlID="_tbNewDowntime2" FilterType="custom" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                    </asp:TableCell>
                    <asp:TableCell  Width="5%" HorizontalAlign="Center">
                   <asp:Label ID="Label10" runat="server" Text="HR 24-36<br />(DT in min)"></asp:Label><br />
                   <asp:TextBox id="_tbNewDowntime3" runat="server" width="50%" Style="text-align: center"  MaxLength="5" >
                            </asp:TextBox> <ajaxToolkit:FilteredTextBoxExtender ID="_fbeNewDowntime3" runat="server" TargetControlID="_tbNewDowntime3" FilterType="custom" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                    </asp:TableCell>
                     <asp:TableCell  Width="5%" HorizontalAlign="Center">
                   <asp:Label ID="Label11" runat="server" Text="HR 36-48<br />(DT in min)"></asp:Label><br />
                   <asp:TextBox id="_tbNewDowntime4" runat="server" width="50%" Style="text-align: center" MaxLength="5"  >
                            </asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="_fbeNewDowntime4" runat="server" TargetControlID="_tbNewDowntime4" FilterType="custom" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                    </asp:TableCell>
                    <asp:TableCell  Width="5%" HorizontalAlign="Center">
                   <asp:Label ID="Label12" runat="server" Text="HR 48-60<br />(DT in min)"></asp:Label><br />
                   <asp:TextBox id="_tbNewDowntime5" runat="server" width="50%" Style="text-align: center"  MaxLength="5" >
                            </asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="_fbeNewDowntime5" runat="server" TargetControlID="_tbNewDowntime5" FilterType="custom" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                    </asp:TableCell>
                    <asp:TableCell  Width="5%" HorizontalAlign="Center">
                   <asp:Label ID="Label13" runat="server" Text="HR 60-72<br />(DT in min)"></asp:Label><br />
                   <asp:TextBox id="_tbNewDowntime6" runat="server" width="50%" Style="text-align: center"  MaxLength="5" >
                            </asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="_fbeNewDowntime6" runat="server" TargetControlID="_tbNewDowntime6" FilterType="custom" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                    </asp:TableCell>
                    <asp:TableCell  Width="10%" HorizontalAlign="Center">
                   <asp:Label ID="Label14" runat="server" Text="Total Downtime 30 Days<br />(DT in min)"></asp:Label><br />
                   <asp:TextBox id="_tbNewDowntime7" runat="server" width="50%" Style="text-align: center"  MaxLength="5" >
                            </asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="_fbeNewDowntime7" runat="server" TargetControlID="_tbNewDowntime7" FilterType="custom" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                    </asp:TableCell>    
                   
                  <asp:TableCell Width="10%" Style="text-align: center">
                    <br /><asp:Button ID="_btnAddAvail"  runat="server" Text="<%$RIResources:Shared,Add%>" />
                    </asp:TableCell>                        
               </asp:TableRow>          
            </asp:Table>

            <RWG:BulkEditGridView ShowHeader="true" ID="_gvcritiqueavailability" runat="server"
                AutoGenerateColumns="False" DataKeyNames="availabilityseqid" EnableInsert="False"
                SaveButtonID="" Width="100%" CellPadding="1" EnableViewState="true">
                <columns>
                
                <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="center" HeaderText="BusinessUnit-Area-Line" >
                        <itemtemplate>   
                     <asp:Label ID="_lblBusUnitAreaLine" runat="server" Text='<%# Container.DataItem("businessunit_area_line")  %>' > </asp:Label>
                     </itemtemplate> </asp:TemplateField>
                     <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center" HeaderText="HR 0-12<br />(DT in min)" >
                        <itemtemplate>   
                     <asp:Textbox ID="_tbDowntime1" runat="server" Style="text-align: center" Text='<%# Container.DataItem("downtime_segment1_min") %>' MaxLength="5"> </asp:Textbox>
                                              <ajaxToolkit:FilteredTextBoxExtender ID="_fbeDowntime1" runat="server" TargetControlID="_tbDowntime1" FilterType="custom" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
</itemtemplate> </asp:TemplateField>
                     <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center" HeaderText="HR 12-24<br />(DT in min)" >
                        <itemtemplate>   
                     <asp:Textbox ID="_tbDowntime2" runat="server" Style="text-align: center" Text='<%# Container.DataItem("downtime_segment2_min") %>' MaxLength="5"> </asp:Textbox>
                                              <ajaxToolkit:FilteredTextBoxExtender ID="_fbeDowntime2" runat="server" TargetControlID="_tbDowntime2" FilterType="custom" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
</itemtemplate> </asp:TemplateField>
                     <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center" HeaderText="HR 24-36<br />(DT in min)" >
                        <itemtemplate>   
                     <asp:Textbox ID="_tbDowntime3" runat="server" Style="text-align: center" Text='<%# Container.DataItem("downtime_segment3_min") %>' MaxLength="5" > </asp:Textbox>
                                              <ajaxToolkit:FilteredTextBoxExtender ID="_fbeDowntime3" runat="server" TargetControlID="_tbDowntime3" FilterType="custom" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
</itemtemplate> </asp:TemplateField>
                     <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center" HeaderText="HR 36-48<br />(DT in min)" >
                        <itemtemplate>   
                     <asp:Textbox ID="_tbDowntime4" runat="server" Style="text-align: center" Text='<%# Container.DataItem("downtime_segment4_min") %>' MaxLength="5"> </asp:Textbox>
                                              <ajaxToolkit:FilteredTextBoxExtender ID="_fbeDowntime4" runat="server" TargetControlID="_tbDowntime4" FilterType="custom" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
</itemtemplate> </asp:TemplateField>
                     <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center" HeaderText="HR 48-60<br />(DT in min)" >
                        <itemtemplate>   
                     <asp:Textbox ID="_tbDowntime5" runat="server" Style="text-align: center" Text='<%# Container.DataItem("downtime_segment5_min") %>' MaxLength="5" > </asp:Textbox>
                                              <ajaxToolkit:FilteredTextBoxExtender ID="_fbeDowntime5" runat="server" TargetControlID="_tbDowntime5" FilterType="custom" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
</itemtemplate> </asp:TemplateField>
                     <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center" HeaderText="HR 60-72<br />(DT in min)" >
                        <itemtemplate>   
                     <asp:Textbox ID="_tbDowntime6" runat="server" Style="text-align: center" Text='<%# Container.DataItem("downtime_segment6_min") %>' MaxLength="5"> </asp:Textbox>
                                              <ajaxToolkit:FilteredTextBoxExtender ID="_fbeDowntime6" runat="server" TargetControlID="_tbDowntime6" FilterType="custom" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
</itemtemplate> </asp:TemplateField>
                     <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="center" HeaderText="Total Downtime<br/>First 72 Hours" >
                        <itemtemplate>   
                     <asp:Label ID="_lblTotalFirstHrs" runat="server"  Style="text-align: center" Text='<%# Container.DataItem("TOTAL_FIRST72HRS_MIN") %>' > </asp:Label>
                     </itemtemplate> </asp:TemplateField>
                     <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="center" HeaderText="Availability<br/>First 72 Hours" >
                        <itemtemplate>   
                     <asp:Label ID="_lbl72houravailability" runat="server" Style="text-align: center" Text='<%# Container.DataItem("first72hoursavailability") & "%" %>' > </asp:Label>
                     </itemtemplate> </asp:TemplateField>
                     <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="center" HeaderText="Total Downtime<br/>First 30 days<br />(DT in min)" >
                        <itemtemplate>   
                     <asp:Textbox ID="_tbDowntime30Days" runat="server" Style="text-align: center" Text='<%# Container.DataItem("downtime_segment7_min") %>' MaxLength="5"> </asp:Textbox>
                                              <ajaxToolkit:FilteredTextBoxExtender ID="_fbeDowntime30Days" runat="server" TargetControlID="_tbDowntime30Days" FilterType="custom" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
</itemtemplate> </asp:TemplateField>
                     <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="center" HeaderText="Availability<br/>First 30 Days" >
                        <itemtemplate>   
                     <asp:Label ID="_lbl30dayAvailability" runat="server" Style="text-align: center" Text='<%# Container.DataItem("first30daysavailability") & "%" %>' > </asp:Label>
                     </itemtemplate> </asp:TemplateField>
                    
                   
                       
                   
                    <asp:TemplateField>
                        <itemtemplate>
                    <center>
                   <asp:linkbutton id="_lnkBtnAvailability" text="Delete" Visible="true" runat="server" CommandName="Delete"  datatextfield="availabilityseqid" >                         
                        <span class="ui-icon ui-icon-trash" title="Delete"  /> 
                       </asp:linkbutton></center>
			    
                </itemtemplate>
                    </asp:TemplateField>
                </columns>
                <headerstyle backcolor="#CCCC99" forecolor="Black" wrap="False" />
                <rowstyle cssclass="Border" />
                <alternatingrowstyle cssclass="BorderSecondary" />
            </RWG:BulkEditGridView>
           
           
                     
                      
                      <ajaxToolkit:CascadingDropDown ID="_cddlNewCritiqueCategory" runat="server" Category="CritiqueCategory"
                                LoadingText="..." PromptText="    "
                                ServiceMethod="GetCritiqueCategory" ServicePath="~/CascadingLists.asmx"
                                TargetControlID="_ddlNewCritiqueCategory" UseContextKey="true">
                                </ajaxToolkit:CascadingDropDown>
                                
                      <ajaxToolkit:CascadingDropDown ID="_cddlNewCritiqueSubCategory" runat="server" Category="CritiqueSubCategory"
                        LoadingText="" PromptText="    " UseContextkey="true"
                        ServiceMethod="GetCritiqueSubCategory" ServicePath="~/CascadingLists.asmx"
                        TargetControlID="_ddlNewCritiqueSubCategory" ParentControlID="_ddlNewCritiqueCategory">
                        </ajaxToolkit:CascadingDropDown>
                        
                     
          
               <%--  <ajaxToolkit:FilteredTextBoxExtender ID="_fbeNewSort" runat="server"
                                TargetControlID="_tbNewSort" FilterType="custom" ValidChars="1234567890">
                            </ajaxToolkit:FilteredTextBoxExtender>--%>
                            
               <asp:Table ID="_tblNewRow" runat="server" Width="100%"  BorderStyle="Outset">
                <asp:TableRow CssClass="Header">
                    <asp:TableCell Width="100%" ColumnSpan="5" Style="text-align: left">
                        <asp:Label ID="_lblEntry" SkinID="LabelWhite" runat="server" Text="Enter New Critique Item:"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow VerticalAlign="Top" BackColor="LightGray">

                   
                    
                    <asp:TableCell width="30%">
                        <span class="ValidationError"></span>
                        <asp:Label ID="_lblNewDesc" runat="server" Text='<%$ RIResources:Shared,Description %>'></asp:Label><br />
                        <asp:Textbox ID="_tbNewDesc" runat="server" CssClass="textExpand" TextMode="MultiLine" Rows="2" Wrap="true"
                        MaxLength="1000" Width="95%"></asp:Textbox>
                        <asp:CustomValidator ID="CustomValidator1" ValidateEmptyText="true" ValidationGroup="Critique"
                            runat="server" ClientValidationFunction="DescCheck" ControlToValidate="_tbNewDesc"
                            Display="None" EnableClientScript="true" ErrorMessage="Please enter required fields."
                            Text="Please Enter a Description" SetFocusOnError="true" />
                    </asp:TableCell>
                     <asp:TableCell Width="20%">
                            <asp:Label ID="_lblNewBusUnit" runat="server" Text="BusUnit"></asp:Label><br />
                              <asp:DropDownList ID="_ddlNewBusUnit" runat="server" Style="font-family: Verdana;
                            font-size: 12px"  Width="90%">
                            </asp:DropDownList></asp:TableCell>
                    <asp:TableCell Width="20%">
                        <span class="ValidationError"></span>
                        <asp:Label ID="_lblNewCritiqueCategory" runat="server" Text="Critique Category"></asp:Label><br />
                        <asp:DropDownList ID="_ddlNewCritiqueCategory" runat="server" Style="font-family: Verdana;
                            font-size: 12px"  Width="90%">
                            </asp:DropDownList>
                     </asp:TableCell>
                     <asp:TableCell Width="20%">
                        <span class="ValidationError"></span>
                        <asp:Label ID="_lblNewCritiqueSubCategory" runat="server" Text="Critique SubCategory"></asp:Label><br />
                        <asp:DropDownList ID="_ddlNewCritiqueSubCategory" runat="server" Style="font-family: Verdana;
                            font-size: 12px"  Width="90%"> </asp:DropDownList>
                            </asp:TableCell>                         
                      <asp:TableCell Width="10%" Style="text-align: center">
                    <br /><asp:Button ID="_btnAdd" runat="server" Text="<%$RIResources:Shared,Add%>" />
                    </asp:TableCell> 
               </asp:TableRow>
                
            </asp:Table>
            
            <asp:Table ID="Table3" runat="server" width ="100%" borderwidth="2">
                         <asp:TableRow CssClass="Header">
                    <asp:TableCell Width="100%" ColumnSpan="4" Style="text-align: left">
                        <asp:Label ID="_lblCritiqueItems" SkinID="LabelWhite" runat="server" Text="Current Critique Items" ></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
             </asp:Table>
             
           
             
              <RWG:BulkEditGridView ShowHeader="true" ID="_gvcritique" runat="server"
                AutoGenerateColumns="False" DataKeyNames="outagecritiqueseqid" EnableInsert="False"
                SaveButtonID="" Width="100%" CellPadding="1" EnableViewState="true" AllowSorting="false" cssclass="tablesorter">
                <columns> 
                <asp:TemplateField ItemStyle-Width="45%" HeaderText="Description" HeaderStyle-Font-Underline="true" HeaderStyle-ForeColor="blue" >
  		                <itemtemplate>    
                            <asp:TextBox id="_tbDesc" runat="server"  CssClass="textExpand" TextMode="MultiLine" Rows="1" Wrap="true" width="1000px"  maxlength="1000" Text='<%# Container.DataItem("description") %>' ></asp:TextBox>
   		                </itemtemplate>  
	            </asp:TemplateField>
	            <asp:TemplateField ItemStyle-Width="20%" HeaderText="Business Unit"  SortExpression="Business Unit" HeaderStyle-Font-Underline="true" HeaderStyle-ForeColor="blue" >
  		                <itemtemplate>                
                         <asp:DropDownList id="_ddlBusUnit"  runat="server"  Width="95%" >   </asp:DropDownList>
                        </itemtemplate>   
	           </asp:TemplateField>	                 
	                <asp:TemplateField ItemStyle-Width="15%" HeaderText="Critique Category" SortExpression="Critique Category" HeaderStyle-Font-Underline="true" HeaderStyle-ForeColor="blue" >
                        <itemtemplate>                 
                             <asp:DropDownlist id="_ddlCritiqueCategory" runat="server"   Width="95%"   > </asp:Dropdownlist>
                                 <ajaxToolkit:CascadingDropDown ID="_cddlCritiqueCategory" runat="server" Category="CritiqueCategory"
                                LoadingText="..." PromptText="    "
                                ServiceMethod="GetCritiqueCategory" ServicePath="~/CascadingLists.asmx"
                                TargetControlID="_ddlCritiqueCategory" UseContextKey="true">
                                </ajaxToolkit:CascadingDropDown>
                                
                      
                        </itemtemplate>
  	            </asp:TemplateField>
  	            <asp:TemplateField ItemStyle-Width="15%" HeaderText="Critique SubCategory" SortExpression="Critique SubCategory" HeaderStyle-Font-Underline="true" HeaderStyle-ForeColor="blue" >	
	                <itemtemplate>                
                         <asp:DropDownList id="_ddlCritiqueSubCategory" runat="server" Width="95%"   >   </asp:DropDownList>
                        <ajaxToolkit:CascadingDropDown ID="_cddlCritiqueSubCategory" runat="server" Category="CritiqueSubCategory"
                        LoadingText="" PromptText="    " UseContextkey="true" ContextKey='<%# Container.DataItem("OutageNumber") %>'
                        ServiceMethod="GetCritiqueSubCategory" ServicePath="~/CascadingLists.asmx"
                        TargetControlID="_ddlCritiqueSubCategory" ParentControlID="_ddlCritiqueCategory">
                        </ajaxToolkit:CascadingDropDown>
                        </itemtemplate>   
	           </asp:TemplateField>
	           
                       
	          
                    <asp:TemplateField>
                        <itemtemplate>
                    <center>
			     <asp:linkbutton id="_lnkBtnCritiqueItems" text="Delete" Visible="true" runat="server" CommandName="Delete"  datatextfield="outagecritiqueseqid" >                         
                        <span class="ui-icon ui-icon-trash" title="Delete"  /> 
                       </asp:linkbutton></center>
                </itemtemplate>
                    </asp:TemplateField>
                </columns>
                <headerstyle backcolor="#CCCC99" forecolor="Black" wrap="False" />
                <rowstyle cssclass="Border" />
                <alternatingrowstyle cssclass="BorderSecondary" />
            </RWG:BulkEditGridView>
              
            <br />
            <center>
                <IP:SpellCheck ID="UltimateSpell1" runat="server">
                </IP:SpellCheck>
                <asp:Button ID="_btnSave"  runat="server" Text="<%$RIResources:Shared,Save%>" ValidationGroup="ActionItem"
                    CausesValidation="True" />
                <asp:Button ID="_btnReport" visible="true" runat="server" Text="<%$RIResources:Shared,Report%>" CausesValidation="False" />
            </center>
            <asp:ValidationSummary ID="_vsSummary" runat="server" CssClass="ValidationError"
                DisplayMode="BulletList" ValidationGroup="OutageScope" HeaderText="<%$ RIResources:Shared,Please provide data for all required fields%>"
                ShowSummary="false" EnableClientScript="true" ShowMessageBox="false" />
            <IP:MessageBox ID="_messageBox" runat="server" ButtonType="OKCancel" />
        </ContentTemplate>
    </Asp:UpdatePanel>
    <center>
        <asp:Button ID="_btnSaveClose" runat="server" visible="false" Text="<%$RIResources:Shared,SaveClose%>"  />
</center>
   
  
   
   <script language="JavaScript" type="text/javascript"> 
    $('#basic_example_1').datetimepicker();
    $('.PerformanceDateTimePicker').datetimepicker();
    </script>
    
</asp:Content>
