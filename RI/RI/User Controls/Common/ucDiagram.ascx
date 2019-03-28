<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDiagram.ascx.vb" Inherits="User_Controls_ucDiagram" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:MultiView ID="_mvDiagram" runat="server">
    <asp:View ID="_vwDecision" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 469px;">
            <tr>
                <td align="center" style="width: 469px; height: 386px; background-color: #ffffff;
                    background-image: url(Images/Decision.gif); background-repeat: no-repeat; text-align: center">
                    <asp:Label ID="_lblDecision" Font-Size="16px" Font-Bold="True" runat="server" Text="Are there any extenuating circumstances such as delivery, special frame, etc. which prevent new motor purchase?"
                        Width="389px" Style="margin-left: 40px; margin-right: 40px;"></asp:Label>
                    <asp:Label ID="_lblSuggestedDecision" runat="server" Font-Size="16px" ForeColor="red"
                        Width="389px" Style="margin-left: 40px; margin-right: 40px;"></asp:Label>
                    <center>
                        <asp:RadioButtonList ID="_rblDecisionYesNo" Font-Size="20px" Font-Bold="true" RepeatDirection="Horizontal"
                            runat="server">
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </center>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="_vwProcess" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 469px;">
            <tr>
                <td align="center" style="width: 100%; height: 240px; background-color: #cccccc;
                    background-image: url(Images/Process.gif); background-repeat: no-repeat">
                    <asp:Label ID="_lblProcess" Style="margin-left: 20px; margin-right: 20px;" Font-Size="16px"
                        Font-Bold="True" runat="server" Text="Are there any extenuating circumstances such as delivery, special frame, etc. which prevent new motor purchase?"
                        Width="429px"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="_vwPredefinedProcess" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 469px;">
            <tr>
                <td align="center" style="width: 469px; height: 167px; background-color: #ffffff;
                    background-image: url(Images/PredefinedProcess.gif); background-repeat: no-repeat">
                    <asp:Label ID="_lblPredefinedProcess" Font-Size="16px" Font-Bold="True" runat="server"
                        Text="Are there any extenuating circumstances such as delivery, special frame, etc. which prevent new motor purchase?"
                        Width="429px" Style="margin-left: 20px; margin-right: 20px;"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="_vwData" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 469px;">
            <tr>
                <td align="center" style="width: 469px; height: 200px; background-color: #ffffff;
                    background-image: url(Images/Data.gif); background-repeat: no-repeat">
                    <asp:Label ID="_lblData" Font-Size="16px" Font-Bold="True" runat="server" Text="Are there any extenuating circumstances such as delivery, special frame, etc. which prevent new motor purchase?"
                        Width="269px" Style="margin-left: 100px; margin-right: 100px; text-align: center"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="_vwInput" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 469px;">
            <tr>
                <td align="center" style="width: 100%; height: 240px; background-color: #cccccc;
                    background-image: url(Images/Process.gif); background-repeat: no-repeat;">
                    <asp:Label ID="_lblInput" Style="margin-left: 0px; margin-right: 0px;" Font-Size="18px"
                        Font-Bold="True" runat="server" Width="429px"></asp:Label>
                    <table cellpadding="2" cellspacing="3" border="0" style="width: 400px;">
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="_lblHP" Style="text-align: left" Font-Size="16px" Font-Bold="True"
                                    runat="server" Text="* Motor HP" Width="150px"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="_ddlHP" runat="server" Width="100px">
                                </asp:DropDownList>
                                <asp:TextBox ID="_txtHP" runat="server" Width="100px"></asp:TextBox>
                                <ajaxtoolkit:filteredtextboxextender id="_rtbeHP" runat="server" filtermode="ValidChars"
                                    filtertype="Numbers,Custom" targetcontrolid="_txtHP" validchars=".,">
                                </ajaxtoolkit:filteredtextboxextender>
                                <asp:RequiredFieldValidator ID="_rfvHP" runat="server" ControlToValidate="_ddlHP"
                                    EnableClientScript="true" ErrorMessage="Motor HP is a required field" Text="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="_lblRPM" Style="text-align: left" Font-Size="16px" Font-Bold="True"
                                    runat="server" Text="* Motor RPM" Width="150px"></asp:Label>&nbsp;
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="_ddlRPM" runat="server" Width="100px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="_rfvRPM" runat="server" ControlToValidate="_ddlRPM"
                                    EnableClientScript="true" ErrorMessage="Motor RPM is a required field" Text="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <%--   <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="_lblRepairPrice" Style="text-align: left" Font-Size="16px" Font-Bold="True"
                                    runat="server" Text="Repair Price" Width="150px"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="_txtRepairPrice" runat="server" Width="98px" onKeyUp="javascript:parseDecimal(this);"
                                    onChange="javascript:parseDecimal(this);"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="_ftbRepairPrice" runat="server" FilterMode="ValidChars"
                                    FilterType="Numbers,Custom" TargetControlID="_txtRepairPrice" ValidChars=".,">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="_rfvRepairPrice" runat="server" ControlToValidate="_txtRepairPrice"
                                    EnableClientScript="true" ErrorMessage="Repair Price is a required field" Text="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="_lblEfficiency" Style="text-align: left" Font-Size="16px" Font-Bold="True"
                                    runat="server" Text="% Efficiency" Width="150px"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="_txtEfficiency" runat="server" Width="98px" onKeyUp="javascript:parseDecimal(this);"
                                    onChange="javascript:parseDecimal(this);" Text="0"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="_rfvEfficiency" runat="server" ControlToValidate="_txtEfficiency" EnableClientScript="true"
                                        ErrorMessage="Efficiency is a required field" Text="*"></asp:RequiredFieldValidator>%
                                (i.e. 88.5)
                                <asp:CustomValidator ID="_cvEfficiency" runat="server" ClientValidationFunction="validateEfficiency"
                                    EnableClientScript="true" ControlToValidate="_txtEfficiency" Text="Efficiency should be >= 60%"></asp:CustomValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="_ftbEfficiency" runat="server" FilterMode="ValidChars"
                                    FilterType="Numbers,Custom" TargetControlID="_txtEfficiency" ValidChars=".">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left" valign="top">
                                <asp:Label ID="_lblEfficiencyMessage" runat="server" Text="" Visible="false"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="_lblNewMotorPrice" Style="text-align: left" Font-Size="16px" Font-Bold="True"
                                    runat="server" Text="New Motor Price"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="_txtNewMotorPrice" runat="server" Width="98px" BackColor="lightGray"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="_rfvNewMotorPrice" runat="server" ControlToValidate="_txtNewMotorPrice"
                                    EnableClientScript="true" ErrorMessage="New Motor Price is a required field"
                                    Text="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="_lblOldEfficiency" Style="text-align: left" Font-Size="16px" Font-Bold="True"
                                    runat="server" Text="New Motor % Efficiency"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="_txtNewEfficiency" runat="server" Width="98px" BackColor="lightGray"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="_rfvNewEfficiency" runat="server" ControlToValidate="_txtNewEfficiency"
                                    EnableClientScript="true" ErrorMessage="New Efficiency is a required field" Text="*"></asp:RequiredFieldValidator>
                                <div style="display: none;">
                                    <asp:TextBox ID="_txtOldEfficiency" runat="server" Width="98px" BackColor="lightGray"></asp:TextBox>
                                </div>
                            </td>
                        </tr>--%>
                    </table>
                    <ajaxtoolkit:cascadingdropdown id="_cddHP" runat="server" prompttext="Select HP"
                        servicemethod="GetHP" servicepath="~/NEMA.asmx" targetcontrolid="_ddlHP" category="HP"
                        usecontextkey="true">
                    </ajaxtoolkit:cascadingdropdown>
                    <ajaxtoolkit:cascadingdropdown id="_cddRPM" runat="server" prompttext="Select RPM"
                        servicemethod="GetRPM" servicepath="~/NEMA.asmx" targetcontrolid="_ddlRPM" category="RPM"
                        parentcontrolid="_ddlHP" usecontextkey="true">
                    </ajaxtoolkit:cascadingdropdown>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="_vwAutoDecision" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 469px;">
            <tr>
                <td align="center" style="width: 469px; height: 386px; background-color: #ffffff;
                    background-image: url(Images/Decision.gif); background-repeat: no-repeat">
                    <asp:Label ID="_lblAutoDecision" Font-Size="16px" Font-Bold="True" runat="server"
                        Text="Are there any extenuating circumstances such as delivery, special frame, etc. which prevent new motor purchase?"
                        Width="389px" Style="margin-left: 40px; margin-right: 40px;"></asp:Label>
                    <asp:Label ID="_lblAutoAnswer" runat="server" Font-Size="X-Large" ForeColor="red"
                        Width="389px" Style="margin-left: 40px; margin-right: 40px;"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="_vwReport" runat="server">
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <div id="_divReportData" runat="Server">
                    </div>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="_vwPromptOldEfficiency" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 469px;">
            <tr>
                <td align="center" style="width: 100%; height: 240px; background-color: #cccccc;
                    background-image: url(Images/Process.gif); background-repeat: no-repeat;">
                    <asp:Label ID="_lblInputOldEfficiency" Style="margin-left: 0px; margin-right: 0px;" Font-Size="18px"
                        Font-Bold="True" runat="server" Width="429px"></asp:Label>
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="_lblEfficiency" Style="text-align: left" Font-Size="16px" Font-Bold="True"
                                    runat="server" Text="% Efficiency" Width="150px"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="_txtEfficiency" runat="server" Width="98px" onKeyUp="javascript:parseDecimal(this);"
                                    onChange="javascript:parseDecimal(this);" Text="0"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="_rfvEfficiency" runat="server" ControlToValidate="_txtEfficiency" EnableClientScript="true"
                                        ErrorMessage="Efficiency is a required field" Text="*"></asp:RequiredFieldValidator>%
                                (i.e. 88.5)
                                <asp:CustomValidator ID="_cvEfficiency" runat="server" ClientValidationFunction="validateEfficiency"
                                    EnableClientScript="true" ControlToValidate="_txtEfficiency" Text="Efficiency should be >= 60%"></asp:CustomValidator>
                                <ajaxtoolkit:filteredtextboxextender id="_ftbEfficiency" runat="server" filtermode="ValidChars"
                                    filtertype="Numbers,Custom" targetcontrolid="_txtEfficiency" validchars=".">
                                </ajaxtoolkit:filteredtextboxextender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left" valign="top">
                                <asp:Label ID="_lblEfficiencyMessage" runat="server" Text="" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="_vwPromptRepairPrice" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 469px;">
            <tr>
                <td align="center" style="width: 100%; height: 240px; background-color: #cccccc;
                    background-image: url(Images/Process.gif); background-repeat: no-repeat;">
                    <asp:Label ID="_lblInputRepairPrice" Style="margin-left: 0px; margin-right: 0px;" Font-Size="18px"
                        Font-Bold="True" runat="server" Width="429px"></asp:Label>
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="_lblRepairPrice" Style="text-align: left" Font-Size="16px" Font-Bold="True"
                                    runat="server" Text="Repair Price" Width="150px"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="_txtRepairPrice" runat="server" Width="98px" onKeyUp="javascript:parseDecimal(this);"
                                    onChange="javascript:parseDecimal(this);"></asp:TextBox>
                                <ajaxtoolkit:filteredtextboxextender id="_ftbRepairPrice" runat="server" filtermode="ValidChars"
                                    filtertype="Numbers,Custom" targetcontrolid="_txtRepairPrice" validchars=".,">
                                </ajaxtoolkit:filteredtextboxextender>
                                <asp:RequiredFieldValidator ID="_rfvRepairPrice" runat="server" ControlToValidate="_txtRepairPrice"
                                    EnableClientScript="true" ErrorMessage="Repair Price is a required field" Text="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="_vwPromptNewMotorPrice" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 469px;">
            <tr>
                <td align="center" style="width: 100%; height: 240px; background-color: #cccccc;
                    background-image: url(Images/Process.gif); background-repeat: no-repeat;">
                    <asp:Label ID="_lblInputNewPrice" Style="margin-left: 0px; margin-right: 0px;" Font-Size="18px"
                        Font-Bold="True" runat="server" Width="429px"></asp:Label>
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="_lblNewMotorPrice" Style="text-align: left" Font-Size="16px" Font-Bold="True"
                                    runat="server" Text="New Motor Price"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="_txtNewMotorPrice" runat="server" Width="98px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="_rfvNewMotorPrice" runat="server" ControlToValidate="_txtNewMotorPrice"
                                    EnableClientScript="true" ErrorMessage="New Motor Price is a required field"
                                    Text="*"></asp:RequiredFieldValidator>
                                     <ajaxtoolkit:filteredtextboxextender id="_ftbNewMotorPrice" runat="server" filtermode="ValidChars"
                                    filtertype="Numbers,Custom" targetcontrolid="_txtNewMotorPrice" validchars=".,">
                                </ajaxtoolkit:filteredtextboxextender>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="_vwPromptNewEfficiency" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 469px;">
            <tr>
                <td align="center" style="width: 100%; height: 240px; background-color: #cccccc;
                    background-image: url(Images/Process.gif); background-repeat: no-repeat;">
                    <asp:Label ID="_lblInputNewEfficiency" Style="margin-left: 0px; margin-right: 0px;" Font-Size="18px"
                        Font-Bold="True" runat="server" Width="429px"></asp:Label>
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td align="left">
                                <asp:Label ID="_lblOldEfficiency" Style="text-align: left" Font-Size="16px" Font-Bold="True"
                                    runat="server" Text="New Motor % Efficiency"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="_txtNewEfficiency" runat="server" Width="98px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="_rfvNewEfficiency" runat="server" ControlToValidate="_txtNewEfficiency"
                                    EnableClientScript="true" ErrorMessage="New Efficiency is a required field" Text="*"></asp:RequiredFieldValidator>
                                <ajaxtoolkit:filteredtextboxextender id="_ftbNewEfficiency" runat="server" filtermode="ValidChars"
                                    filtertype="Numbers,Custom" targetcontrolid="_txtNewEfficiency" validchars=".">
                                </ajaxtoolkit:filteredtextboxextender>
                                <div style="display: none;">
                                    <asp:TextBox ID="_txtOldEfficiency" runat="server" Width="98px"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:View>
</asp:MultiView>
<div class="noprint" style="text-align: center; width: 469px">
    <asp:Button ID="_btnBack" Text="Back" runat="server" />
    <asp:Button ID="_btnNext" Text="Next" runat="server" />
    <asp:Button ID="_btnCancel" Text="Cancel" runat="server" />
    <asp:ImageButton ID="_imgHelp" ImageUrl="~/Images/question.gif" runat="server" AlternateText="Help" />
    <asp:HyperLink ID="_lnkNemapdf" runat="server" NavigateUrl="~/NEMA/NemaMotors.pdf"
        Target="_blank" Text="PDF Diagram"></asp:HyperLink>
</div>
<div>
    <%-- <div id="_divREC" style="text-align:left;">
    <ul><li><b>repair and energy cost hurdle</b> = <br />((new motor cost - repair cost)&#45;(2 x annual energy savings)) > 0	</li> <li>
    <b>annual energy savings</b> = <br />(new efficiency (%) &#45; old efficiency (%)) x motor hp x $340
    </li> </ul> 
    <hr />
    
    </div>--%>
</div>
<ajaxtoolkit:modalpopupextender backgroundcssclass="modalBackground" cancelcontrolid="_btnCloseHelp"
    okcontrolid="_btnCloseHelp" targetcontrolid="_imgHelp" popupcontrolid="_pnlHelp"
    runat="server" id="_mpeHelp">
</ajaxtoolkit:modalpopupextender>
<asp:Panel ID="_pnlHelp" runat="server" Style="display: none" BorderWidth="1" BorderColor="Black"
    BackColor="#E8EEF7" HorizontalAlign="Left">
    <pre style="font-size: 12px">(1) repair and energy cost hurdle = [(new motor cost - repair cost) – (2 x annual energy savings)] > 0	
	annual energy savings = [new motor efficiency (%) – old motor efficiency (%)] x motor hp x $340

	example:  100 hp motor - - new motor efficiency = 95.1%  old motor efficiency = 93.2%
	annual savings = [0.951 – 0.932] x 100 x 340 = $646 /year energy savings

(2)  extenuating circumstances include:
	-frame casting is broken and needs repair or has been welded in past
	-other major mechanical issues including damaged shaft, bad rabbet fits, excessive rust, rotor
	     damage/issues, etc.
	-motor has been rewound previously at least 2 times
	-motor has history of poor reliability
	-motor is an open design and application can accommodate a TEFC motor
	-motor is misapplied for application and different motor should be used; old motor 
	     can be considered for service as a spare for other applications
						</pre>
    <asp:Button ID="_btnCloseHelp" runat="server" Text="Close" />
</asp:Panel>
<div style="visibility: hidden; display: none">
    <asp:Label ID="_lblCurrentStep" runat="server"></asp:Label>
    <asp:Label ID="_lblYesNextStep" runat="server"></asp:Label>
    <asp:Label ID="_lblNoNextStep" runat="server"></asp:Label>
    <asp:Label ID="_lblPreviousStep" runat="server"></asp:Label>
</div>
<%--<asp:DropDownList ID=_ddlSwitch runat=server AutoPostBack=true>
	<asp:ListItem Text="0"></asp:ListItem>
	<asp:ListItem Text="1"></asp:ListItem>
	<asp:ListItem Text="2"></asp:ListItem>
	<asp:ListItem Text="3"></asp:ListItem>
</asp:DropDownList>--%>
