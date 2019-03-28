<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucMOCTypes.ascx.vb"
    Inherits="ucMOCTypes" EnableViewState="true" %>

<table cellpadding="2" cellspacing="0" border="0" width="100%">
    <tr class="Header">
        <th colspan="3" align="left">			
            <asp:Label ID="_lblMOCType" runat="server" Width="25%" Text="<%$RIResources:MOC,TypeChange %>"
                SkinID="LabelWhite" EnableViewState="false" />
        </th>
    </tr>
    <tr class="Border">
        <td>
            <asp:RadioButtonList ID="_rblType" RepeatLayout="flow"  runat="server"> </asp:RadioButtonList>
            <asp:CheckBoxList ID="_cblType" RepeatLayout="flow"  runat="server"> </asp:CheckBoxList>
        </td>
    </tr>
</table> 
