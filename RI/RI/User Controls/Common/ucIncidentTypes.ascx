<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucIncidentTypes.ascx.vb"
    Inherits="ucIncidentTypes" EnableViewState="true" %>
<table cellpadding="2" cellspacing="1" border="0" width="100%">
    <tr class="Header" valign="top">
        <th colspan="5" align="left">
            <asp:Label ID="_lblIncidentType" runat="server" Width="20%" Text='<%$ RIResources:Shared,Incident Type %>'
                SkinID="LabelWhite" EnableViewState="false" />
            <asp:Label SkinID="LabelWhite" ID="_lblDisplayresults" runat="server" Text='<%$ RIResources:Shared,DisplayResults %>' />&nbsp;
            <%--"Display Results with:" --%>
            <asp:RadioButton ID="_rblIncidentTypeOr" CssClass="LabelWhite" runat="server" GroupName="AndOr"
                Text='<%$ RIResources:Shared,AnySelectedItem %>' />
            <%-- "Any Selected Items (OR)"--%>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:RadioButton ID="_rblIncidentTypeAnd" CssClass="LabelWhite" runat="server" GroupName="AndOr"
                Text='<%$ RIResources:Shared,AllSelectedItem %>' />
            <%--"All Selected Items (AND)"--%>
        </th>
    </tr>
    <tr class="Border">
        <td style="width:20%" valign="top">
            <asp:Label ID="_lblRTS" runat="server" Text='<%$ RIResources:Shared,RTS %>' EnableViewState="false" /><br />
            <asp:RadioButtonList ID="_rblRTS" runat="server">
            </asp:RadioButtonList>
            <asp:CheckBoxList ID="_cblRTS" runat="server">
            </asp:CheckBoxList>
        </td>
        <td style="width:20%" valign="top">
            <asp:Label ID="_lblPPR" runat="server" Text='<%$ RIResources:Shared,PPR %>' EnableViewState="false" /><br />
            <asp:RadioButtonList ID="_rblPPR" runat="server">
            </asp:RadioButtonList>
            <asp:CheckBoxList ID="_cblPPR" runat="server">
            </asp:CheckBoxList>
        </td>
        <td style="width:20%" valign="top">
            <asp:Label ID="_lblReliabilityRecordable" runat="server" Text='<%$ RIResources:Shared,Reliability Recordable %>'
                EnableViewState="false" />&nbsp;&nbsp;<asp:HyperLink CssClass="LabelLink" ID="_hypRecordableDefinition"
                    runat="server" Text='<%$ RIResources:Shared,Definition %>'></asp:HyperLink><br />
            <asp:RadioButtonList ID="_rblRecordable" runat="server" onclick="calculateSRR();">
            </asp:RadioButtonList>
            <asp:CheckBoxList ID="_cblRecordable" runat="server">
            </asp:CheckBoxList>
        </td>
        <td style="width:20%" valign="top">
            <asp:Label ID="_lblChronic" runat="server" Text='<%$ RIResources:Shared,Chronic %>'
                EnableViewState="false" />&nbsp;&nbsp;
            <asp:HyperLink ID="_hypChronic" CssClass="LabelLink" runat="server" Text='<%$ RIResources:Shared,Managing %>'></asp:HyperLink>
            &nbsp;&nbsp;<b><asp:Literal ID="Literal1" runat="server" Text="<%$ RIResources:Shared,PCF%>" /></b>
            <br />
            <asp:RadioButtonList ID="_rblChronic" RepeatLayout="table" RepeatColumns="2" runat="server">
            </asp:RadioButtonList>
            <asp:CheckBoxList ID="_cblChronic" RepeatLayout="table" RepeatColumns="3" runat="server">
            </asp:CheckBoxList>
        </td>
        <td valign="top">
            
                <%--<asp:RadioButtonList ID="_rblConstrainedAreas" RepeatLayout="flow" runat="server">
            </asp:RadioButtonList>--%>
            <asp:CheckboxList ID="_cblConstrainedAreas" RepeatLayout="flow" runat="server">  </asp:CheckboxList>
            <asp:Label ID="_lblConstrainedAreas" runat="server" Text='<%$RIResources:Shared,Constrained Area %>'
                EnableViewState="false" />
        </td>
    </tr>   
    <tr class="Border">
        <td valign="top">
            <asp:Label ID="_lblQuality" runat="server" Text='<%$RIResources:Shared,Quality %>'
                EnableViewState="false" /><br />
            <asp:CheckBoxList ID="_cblQuality" RepeatLayout="flow" runat="server">
            </asp:CheckBoxList>
        </td>
        <td valign="top">
            <asp:Label ID="_lblRCFA" runat="server" Text='<%$ RIResources:Shared,RCFA %>' EnableViewState="false" /><br />
            <asp:RadioButtonList ID="_rblRCFA" RepeatLayout="flow" runat="server">
            </asp:RadioButtonList>
            <asp:CheckBoxList ID="_cblRCFA" RepeatLayout="flow" runat="server">
            </asp:CheckBoxList>
        </td>
        <td style="width:20%" valign="top">
            <asp:Label ID="_lblSRR" runat="server" Text='<%$ RIResources:Shared,SRR %>'
                EnableViewState="false" />&nbsp;&nbsp;
            <%--<asp:RadioButtonList ID="_rblSRR" RepeatLayout="table" RepeatColumns="1" runat="server">
            </asp:RadioButtonList>--%>
            <asp:CheckBoxList ID="_cblSRR" RepeatLayout="table" RepeatColumns="1" runat="server">
            </asp:CheckBoxList>
        </td>
        <td valign="top">
            <asp:Label ID="_lblCertifiedKill" runat="server" Text='<%$RIResources:Shared,Certified Kill %>'
                EnableViewState="false" />&nbsp;&nbsp;<asp:HyperLink ID="_hplCertifiedKillDefinition"
                    CssClass="LabelLink" runat="server" Text='<%$ RIResources:Shared,Definition %>'></asp:HyperLink><br />
            <asp:RadioButtonList ID="_rblCertifiedKill" RepeatLayout="flow" runat="server">
            </asp:RadioButtonList>
            <asp:CheckBoxList ID="_cblCertifiedKill" RepeatLayout="flow" runat="server">
            </asp:CheckBoxList>
        </td>
                
        <td valign="top">
            <asp:RadioButtonList ID="_rblSchedUnsched" RepeatLayout="flow" runat="server">
            </asp:RadioButtonList>
            <asp:CheckBoxList ID="_cblSchedUnsched" RepeatLayout="flow" runat="server">
            </asp:CheckBoxList>
        </td>
    </tr>   

    <tr class="Border">
        <td colspan="5" valign="top">
            <asp:Label ID="_lblSafety" runat="server" Text='<%$RIResources:Shared,EHS %>' SkinID="LabelButton"
                EnableViewState="false" Width="150px" />
            <asp:Label ID="_lblIRIS" runat="server" Text="<%$RIResources:Shared,IRIS Number %>"
                EnableViewState="false" />
            <asp:TextBox ID="_txtIRIS" MaxLength="11" runat="server"></asp:TextBox><br />
        </td>
        <ajaxToolkit:FilteredTextBoxExtender ID="_feIRIS" runat="server" TargetControlID="_txtIRIS"
            FilterType="custom" ValidChars="1234567890">
        </ajaxToolkit:FilteredTextBoxExtender>
    </tr>
</table>
<div id="_divCertifiedKillDefinition" class="modalPopup" style="display: none">
                <%--<span style="text-align: left" class="ContentHeader">Repair Costs:</span>
				<p>
					Materials and Labor (both in-house and contracted)</p>--%>
                <span style="text-align: left" class="ContentHeader">
                    <asp:Literal runat="Server" Text="<%$RIResources:Shared,Certified Kill%>"></asp:Literal></span>
                <asp:Localize ID="_CertifiedKillDef" runat="server" Text="<%$RIResources:Shared,DefinitionCertifiedKill %>"></asp:Localize>
                
            </div>
 <div id="_divCriteriaCostDefinition" class="modalPopup" style="display: none">
                <%--<span style="text-align: left" class="ContentHeader">Repair Costs:</span>
				<p>
					Materials and Labor (both in-house and contracted)</p>--%>
                <span style="text-align: left" class="ContentHeader">
                    <asp:Literal  runat="Server" Text="<%$RIResources:Shared,Criteria Cost%>"></asp:Literal></span>
                 <asp:Localize ID="_CriteriaCostDef1" runat="server" Text="<%$RIResources:Shared,DefinitionCriteriaCost1 %>"></asp:Localize>
                <asp:Localize ID="_CriteriaCostDef2" runat="server" Text="<%$RIResources:Shared,DefinitionCriteriaCost2 %>"></asp:Localize>
                
            </div>
<ajaxToolkit:CollapsiblePanelExtender ID="_cpeEHS" runat="Server" TargetControlID="_pnlEHS"
    Collapsed="True" CollapseControlID="_lblSafety" ExpandControlID="_lblSafety"
    SuppressPostBack="true" TextLabelID="_lblSafety" CollapsedText="<%$RIResources:Shared,ShowEHS %>"
    ExpandedText="<%$RIResources:Shared,HideEHS %>" ScrollContents="false" CollapsedSize="0" />
<asp:Panel ID="_pnlEHS" runat="server" Width="100%" HorizontalAlign="Center" Height="0"
    CssClass="collapsePanel">
    <asp:Table ID="Table4" runat="server" Width="100%">
        <asp:TableRow CssClass="Border">
            <asp:TableCell HorizontalAlign="left">
                <asp:CheckBoxList ID="_cblSafety" RepeatLayout="table" Width="100%" runat="server">
                </asp:CheckBoxList>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Panel>
