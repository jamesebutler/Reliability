<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucPPRMillMachineSelection.ascx.vb" Inherits="ucPPRMillMachineSelection" %>
<table width=100% cellpadding=2 cellspacing=0>    
    <tr>        
        <td>
            <asp:Panel ID=_pnlMach runat=server BorderWidth=1 BackColor=white BorderColor=black ScrollBars=auto Height=150px>
               <asp:CheckBoxList ID=_cblMachines runat=server RepeatColumns=1 RepeatLayout=Table AutoPostBack=false>                    
                </asp:CheckBoxList>                
            </asp:Panel>
        </td>
    </tr>
</table>
