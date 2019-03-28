<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="AuditTracker.aspx.vb" Inherits="RI_Help_AuditTracker" title="Untitled Page" %>
<%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" Runat="Server">
	<table width=100% cellpadding=2 cellspacing=1 border=0>
		<tr class="Border">
			<td colspan=2><IP:DateRange ID=_dateRange runat=server DisplayAsDropDown=true SelectedDateRange=EnteredLast7Days></IP:DateRange> </td>						
		</tr>
		<tr class="Border">
			<td colspan=2 valign=top><asp:Label ID=_lblDesc runat=server Text="Description:"></asp:Label>&nbsp;<asp:TextBox ID=_txtDesc runat=server Width="80%"></asp:TextBox></td>
		</tr>
		<tr class="Border" >
			<td width="100%" colspan=2><asp:Label ID=_lblEvents runat=server Text="Events:"></asp:Label>&nbsp;
			<asp:Panel ID=_pnlEvents BackColor=white ScrollBars=vertical runat=server Height=100px BorderWidth=1>
			<asp:CheckBoxList ID=_cblEvents runat=server RepeatColumns=4 Width="98%" RepeatDirection=Horizontal RepeatLayout=table></asp:CheckBoxList>
			</asp:Panel>
			</td>			
		</tr>
		<tr class="BorderWhite"><td colspan=2 align=center><asp:Button ID=_btnSearch runat=server Text="Display Audit Events" /></td></tr>
	</table>
	<Asp:UpdatePanel ID=_udpAudit runat=server UpdateMode=always>
		<Triggers>		
			<asp:AsyncPostBackTrigger  ControlID="_btnSearch"  EventName="Click" />
		</Triggers>
		<ContentTemplate>
			<asp:GridView AutoGenerateColumns=false AllowSorting=False Width="100%" ID=_grvAudit runat=server CssClass="Border" BorderColor="Black" BorderWidth="2">
			<Columns>
				<asp:BoundField ItemStyle-VerticalAlign=top  DataField="Proc_Name" HeaderText="Event Name" />
				<asp:BoundField ItemStyle-VerticalAlign=top DataField="Proc_date" HeaderText="Event Date" DataFormatString="{0:g}" HtmlEncode="false" />
				<asp:BoundField ItemStyle-VerticalAlign=top DataField="Proc_desc" HeaderText="Event Description"
							HtmlEncode="true" HeaderStyle-Width="70%" ItemStyle-Width="300px"   />				
			</Columns> 
			<RowStyle CssClass="BorderWhite" />
			<AlternatingRowStyle CssClass="BorderSecondary" />
			</asp:GridView> 
			
		</ContentTemplate>
	</Asp:UpdatePanel>
</asp:Content>

