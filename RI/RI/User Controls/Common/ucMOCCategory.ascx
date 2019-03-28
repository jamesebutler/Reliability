<script language="JavaScript" type="text/javascript">
function fnCheckParent(val, sender)
{
    var cbl = document.getElementById(sender);                                  
    if (cbl.value = val)
    {
        cbl.checked=true;
       }
}

function fnUnCheckChild(sender, count)
{
    var cbl = document.getElementById(sender);                                  

    if (cbl!=null){
        for (i=0;i<count;i++){
            var ca = document.getElementById(sender + "_"+i);
            if (ca!=null) {ca.checked=false}
        }
     }   
}
</script>

<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucMOCCategory.ascx.vb" Inherits="ucMOCCategory" EnableViewState="true" %>

<%--  <asp:Table ID="_tblMOCCategory" runat="server" CellPadding="2" CellSpacing="2" BackColor="white"
    Style="width: 98%" EnableViewState="true"> 
    
    <asp:TableRow ID="_trHeading"   CssClass="Header" runat="server">
    <asp:TableCell HorizontalAlign="left" Width="100%" ColumnSpan="2" >
	<asp:Label ID="_lblCategory" runat="server" Width="100%" Text="<%$RIResources:Shared,Category %>"
               SkinID="LabelWhite" EnableViewState="false" />
    </asp:TableCell>
    </asp:TableRow>
  </asp:Table>--%>
<table cellpadding="2" cellspacing="0" border="0" width="100%">
    <tr class="Header">
        <th colspan="3" align="left">			
        <asp:Label ID="_lblCategory" runat="server" Width="25%" Text="<%$RIResources:Shared,Category %>"
           SkinID="LabelWhite" EnableViewState="false" />
        </th>
    </tr>
</table>    

<asp:Panel id="_Pnl" runat="server">
    <asp:Repeater ID="parentRepeater" runat="server"  OnItemDataBound="parentRepeater_ItemDataBound">
    <ItemTemplate>
        <div class='Category'>
    <asp:table runat="server" BorderStyle="Solid" BorderWidth="1" >
        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top" Width="200Px" CssClass="Border" ColumnSpan="1" >
                <asp:CheckBox id="_cbCategory" runat="server" Text='<%#  RI.SharedFunctions.LocalizeValue(DataBinder.Eval(Container.DataItem,"MOCCategory")) %>'> </asp:CheckBox>   
            </asp:TableCell>
            <asp:TableCell VerticalAlign="Top" CssClass="Border" >  
                <asp:CheckBoxList id="_cblSubCategory" runat="server" RepeatColumns="6" ></asp:CheckBoxList>
                <asp:DropDownList ID="_ddlSubCategory" runat="server"  ></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
    </asp:table></div>
    </ItemTemplate>
    </asp:Repeater> 
</asp:Panel>