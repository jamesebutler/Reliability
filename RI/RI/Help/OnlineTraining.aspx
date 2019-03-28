<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="OnlineTraining.aspx.vb" Inherits="RI_Help_OnlineTraining" title="RI:Online Training" EnableTheming="true" StylesheetTheme="RIBlackGold" 	Theme="RIBlackGold"%>
<%@ MasterType VirtualPath="~/RI.master" %>

<asp:Content ID="Content1"  ContentPlaceHolderID="_cphMain" Runat="Server">
  <center>  
  <br />
  <asp:Label   BackColor="lightYellow"  BorderWidth="2" Width="75%" Font-Size="Medium" Text="Select a demo you would like to run by clicking on the Demo Name. This will open another window and start the demo immediately. When the demo window opens, you can use the navigation bars at the bottom to stop/start/page through the demo." runat="server"></asp:Label>
  
  <br /><br /><br /><br />
  
  <asp:GridView Width="75%"   CssClass="Border"  BorderColor="Black" BorderWidth="2"
					ID="_gvDemoList" runat="server" AutoGenerateColumns="False" DataKeyNames="DEMONAME"
					EnableViewState="false" AllowSorting="true" ShowFooter="true" EnableSortingAndPagingCallbacks="false">
					<Columns>
						<asp:HyperLinkField DataTextField="DEMONAME" HeaderText="Demo Name" 
							DataNavigateUrlFields="DEMOFILENAME" DataNavigateUrlFormatString="{0}"
							Target="_blank" />
						<asp:BoundField DataField="DEMODESC" HeaderText="Demo Description" />
						<asp:BoundField  Visible="false" DataField="DEMOFILENAME" HeaderText="Demo File" />
					</Columns>
					<FooterStyle HorizontalAlign="Right" Font-Bold="true" />
				</asp:GridView></center>
</asp:Content>

