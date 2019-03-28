<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucMOCClassification.ascx.vb"
    Inherits="ucMOCClass" EnableViewState="true" %>

<%--    <asp:Table ID="_tblMOCClass" runat="server" CellPadding="2" CellSpacing="2" BackColor="white"
    Style="width: 98%" EnableViewState="true" > 
    
        <asp:TableRow ID="TableRow1"   CssClass="Header" runat="server">
            <asp:TableCell HorizontalAlign="left" Width="50%" >
	        <asp:Label ID="_lblClass" runat="server" Width="25%" Text="<%$RIResources:Shared,Classification %>"
               SkinID="LabelWhite" EnableViewState="false" />
            </asp:TableCell>
        </asp:TableRow>
    
        <asp:TableRow CssClass="Border">
            <asp:TableCell HorizontalAlign="left" Width="50%" >
            <asp:CheckBoxList ID="_cblClass" runat="server" />
            </asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow CssClass="Border">
            <asp:TableCell HorizontalAlign="left" Width="50%" >
            <asp:RadioButtonList ID="_rblClass" runat="server" />
            </asp:TableCell>
        </asp:TableRow>

    </asp:Table> --%>
   <table cellpadding="2" cellspacing="0" border="0" width="100%">
    <tr class="Header">
             <th colspan="3" align="left">			
	        <asp:Label ID="_lblClass" runat="server" Width="25%" Text="<%$RIResources:Shared,Classification %>"
               SkinID="LabelWhite" EnableViewState="false" />
            </th>
        </tr>
    
        <tr class="Border">
            <td>
            <asp:CheckBoxList ID="_cblClass" runat="server" />
            </td>
        </tr>
        
        <tr class="Border">
            <td>
            <asp:RadioButtonList ID="_rblClass" runat="server" />
            </td>
        </tr>

    </table> 

