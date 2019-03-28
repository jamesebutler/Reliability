<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDropDownList.ascx.vb" Inherits="RI_User_Controls_Common_ucDropDownList" %>

<asp:Table ID=_tblMain BorderWidth=1 BorderColor=black runat=server CellPadding=0 CellSpacing=1>
    <asp:TableRow BackColor=white>
        <asp:TableCell HorizontalAlign=left><asp:Label ID=_lblSelectedText runat=server Text="Selected value goes here" Width=100%></asp:Label></asp:TableCell>         
        <asp:TableCell Width=2><asp:Button SkinID="" ID=_btnDropDown runat=server /></asp:TableCell>
    </asp:TableRow>    
</asp:Table> 
<asp:Panel CssClass="Popup" BorderWidth=1 BorderStyle=Inset runat=server ID=_pnlPopup ScrollBars=Vertical Width=200>
<asp:ListBox ID=_lblList runat=server Width=90%>
    <asp:ListItem Text="" Value=""></asp:ListItem>
</asp:ListBox>
</asp:Panel>

 <ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="_lblSelectedText"
            Position=Bottom OffsetY="0" PopupControlID="_pnlPopup">
        </ajaxToolkit:PopupControlExtender>
      
 