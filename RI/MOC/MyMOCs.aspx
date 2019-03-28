<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="MyMOCs.aspx.vb"
	Inherits="MOC_MyMOCs" EnableTheming="true" StylesheetTheme="RIBlackGold" Theme="RIBlackGold"
	EnableEventValidation="false" Title="International Paper: MOC" Trace="false"
	MaintainScrollPositionOnPostback="true" %>

<%@ MasterType VirtualPath="~/RI.master" %>

<asp:Content ID="_contentMain" ContentPlaceHolderID="_cphMain" EnableViewState="true" runat="Server">

	<Asp:UpdatePanel ChildrenAsTriggers="true" ID="_upViewScreen" runat="server" EnableViewState="true"
		UpdateMode="always">
		<ContentTemplate>
        <span id="_spMOCReqAttention" runat="server" BackColor="#CCCCCC"  >
            <asp:Label ID="_lbReqAttention" runat="server" Text="<%$ RIResources:Shared,MOCS Requiring Your Attention %>" Font-Bold="True" Font-Size="Large"></asp:Label>
        <span id="_spImplOverride" runat="server" style="border: thin solid #000000;">
        <asp:Label ID="_lbImplOverride" runat="server" Text="0" BackColor="#CCCCCC" Font-Size="10pt" Width="100%" Font-Italic="True" ></asp:Label>
	        <br />		
			<asp:GridView ID="_gvImplOverride" runat="server"  CssClass="BorderSecondary" AutoGenerateColumns="False" DataKeyNames="MOCNUMBER" EnableViewState="false"
				BorderWidth="2" Width="100%" HeaderStyle-BorderColor="red" HeaderStyle-Font-Bold="True" HeaderStyle-HorizontalAlign="Left" GridLines="None" >
				<Columns>
				    <asp:HyperLinkField HeaderText="MOC" HeaderStyle-horizontalalign="Left" DataTextField="MOCNUMBER" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left"
						DataNavigateUrlFields="MOCNUMBER" DataNavigateUrlFormatString="~/MOC/EnterMOC.aspx?MOCNumber={0}"
						Target="_self" />
                    <asp:TemplateField ItemStyle-Width="5%" HeaderStyle-horizontalalign="Left" HeaderText="<%$RIResources:Shared,Date %>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblDate" runat="server" Font-Bold="false" Text='<%# Eval("EventDate", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="25%" HeaderStyle-horizontalalign="Left" HeaderText="<%$RIResources:Shared,Title %>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblTitle" runat="server" Font-Bold="false" Text='<%# Bind("TITLE") %>'></asp:Label><br />
                            <asp:Label ID="_lbDescription" runat="server" Font-Bold="false" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderStyle-horizontalalign="Left" HeaderText="<%$RIResources:MOC,Initiator/Owner %>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblInitiator" runat="server" Font-Bold="false" Text='<%# Bind("initiatorname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
			</asp:GridView>
            
        </span>		
        <br /><span id="_spComplOverride" runat="server" style="border: thin solid #000000;">
                <asp:Label ID="_lbComplOverride" runat="server" Text="0" BackColor="#CCCCCC" Font-Size="10pt" Width="100%" Font-Italic="True" BorderColor="Black" ></asp:Label>
			<asp:GridView ID="_gvComplOverride" runat="server" CssClass="BorderSecondary"
				BorderWidth="2" AutoGenerateColumns="False" DataKeyNames="MOCNUMBER" EnableViewState="false"
				Width="100%" HeaderStyle-BorderColor="red" HeaderStyle-Font-Bold="True" 
                GridLines="None" >
				<Columns>
				    <asp:HyperLinkField HeaderText="MOC" HeaderStyle-horizontalalign="Left" DataTextField="MOCNUMBER" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left"
						DataNavigateUrlFields="MOCNUMBER" DataNavigateUrlFormatString="~/MOC/EnterMOC.aspx?MOCNumber={0}"
						Target="_self" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                    </asp:HyperLinkField>
                    <asp:TemplateField ItemStyle-Width="5%" HeaderStyle-horizontalalign="Left" HeaderText="<%$RIResources:Shared,Date %>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblDate" runat="server" Font-Bold="false" Text='<%# Eval("EventDate", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                    </asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="25%" HeaderStyle-horizontalalign="Left" HeaderText="<%$RIResources:Shared,Title %>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblTitle" runat="server" Font-Bold="false" Text='<%# Bind("TITLE") %>'></asp:Label><br />
                            <asp:Label ID="_lbDescription" runat="server" Font-Bold="false" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderStyle-horizontalalign="Left" HeaderText="<%$RIResources:MOC,Initiator/Owner %>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblInitiator" runat="server" Font-Bold="false" Text='<%# Bind("initiatorname") %>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>
                    </Columns>
			    <HeaderStyle BorderColor="Red" Font-Bold="True" />
			</asp:GridView>
        </span>	
<br />
        <span id="_spApprovedNotImplemented" runat="server" style="border: thin solid #000000;">
                <asp:Label ID="_lbApproveNotImpl" runat="server" Text="0" BackColor="#CCCCCC" Font-Size="10pt" Width="100%" Font-Italic="True" BorderColor="Black" ></asp:Label>
			<asp:GridView ID="_gvApprovedNotImpl" runat="server" CssClass="BorderSecondary"
				BorderWidth="2" AutoGenerateColumns="False" DataKeyNames="MOCNUMBER" EnableViewState="false"
				AllowSorting="true" Width="100%" HeaderStyle-BorderColor="red" HeaderStyle-Font-Bold="True" 
                GridLines="None" >
				<Columns>
				    <asp:HyperLinkField HeaderText="MOC" HeaderStyle-horizontalalign="Left" DataTextField="MOCNUMBER" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left"
						DataNavigateUrlFields="MOCNUMBER" DataNavigateUrlFormatString="~/MOC/EnterMOC.aspx?MOCNumber={0}"
						Target="_self" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                    </asp:HyperLinkField>
                    <asp:TemplateField ItemStyle-Width="5%" HeaderStyle-horizontalalign="Left" HeaderText="<%$RIResources:Shared,Date %>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblDate" runat="server" Font-Bold="false" Text='<%# Eval("EventDate", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                    </asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="25%" HeaderStyle-horizontalalign="Left" HeaderText="<%$RIResources:Shared,Title %>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblTitle" runat="server" Font-Bold="false" Text='<%# Bind("TITLE") %>'></asp:Label><br />
                            <asp:Label ID="_lbDescription" runat="server" Font-Bold="false" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderStyle-horizontalalign="Left" HeaderText="<%$RIResources:MOC,Initiator/Owner %>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblInitiator" runat="server" Font-Bold="false" Text='<%# Bind("initiatorname") %>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>
                    </Columns>
			    <HeaderStyle BorderColor="Red" Font-Bold="True" />
			</asp:GridView>
        </span>	
<br />
        <span id="_spPending" runat="server" BackColor="#CCCCCC" style="border: thin solid #000000;" >
			        <asp:Label ID="_lblRecCount" runat="server" Text="0" BackColor="#CCCCCC" Font-Size="10pt" Width="100%" Font-Italic="True" BorderColor="Black"></asp:Label>
	        <br />		
			<asp:GridView ID="_gvMOCListing" runat="server" CssClass="BorderSecondary" BorderColor="Black"
				BorderWidth="2" AutoGenerateColumns="False" DataKeyNames="approval_seqid" EnableViewState="false"
				AllowSorting="true" Width="100%" HeaderStyle-BorderColor="red" HeaderStyle-Font-Bold="True"
                GridLines="None" >
				<Columns>
				    <asp:HyperLinkField HeaderText="MOC" DataTextField="MOCNUMBER" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
						DataNavigateUrlFields="MOCNUMBER" DataNavigateUrlFormatString="~/MOC/EnterMOC.aspx?MOCNumber={0}"
						Target="_self" />
                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="<%$RIResources:Shared,Date %>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblDate" runat="server" Font-Bold="false" Text='<%# Eval("EventDate", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="25%" HeaderText="<%$RIResources:Shared,Title %>" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblTitle" runat="server" Font-Bold="false" Text='<%# Bind("TITLE") %>'></asp:Label><br />
                            <asp:Label ID="_lbDescription" runat="server" Font-Bold="false" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:MOC,Status %>" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:HiddenField ID="_hfInitiatorEmail" runat="server" Value='<%# Bind("EMAIL") %>' />
                            <asp:Label ID="_lblType" runat="server" Font-Bold="false" Text='<%# Bind("APPROVAL_TYPE") %>'></asp:Label>
                            <asp:Label ID="_lblMOCNUmber" runat="server" visible="false" Text='<%# Bind("MOCNUmber") %>'></asp:Label>
                            <asp:label id="_lbLevel" runat="server" Text='<%# Bind("approval_level") %>' Visible="False"></asp:label>
				        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:MOC,Initiator %>" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblInitiator" runat="server" Font-Bold="false" Text='<%# Bind("initiatorname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="<%$RIResources:MOC,ApproveReview %>" ItemStyle-Width="10%" Itemstyle-Font-Bold="false" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                         <ItemTemplate>
                            <asp:CheckBoxList Font-Bold="false" ID="_cbApproval" runat="server" RepeatDirection="Horizontal" onclick="CheckBoxToRadio(this,2)">
                                <asp:ListItem Text="<%$RIResources:Shared,Yes %>" Value="Y" ></asp:ListItem>
                            </asp:CheckBoxList>
                        </ItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField HeaderText="<%$RIResources:MOC,Comments %>" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                         <ItemTemplate>
                            <asp:TextBox ID="_tbComment" runat="server" Font-Bold="false"  Width="90%">
                            </asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
				</Columns>
			</asp:GridView>
			<div style="text-align: center">
				<asp:Button ID="_btnSave" Text="<%$RIResources:Shared,Save %>" runat="server" />
			</div> 
        </span>
        </span>
            <br />
        <asp:Label ID="_lblInterestHeader" runat="server" Text="<%$ RIResources:Shared,MOCs You May Be Interested In %>" Font-Bold="True" Font-Size="Large" Visible="false"></asp:Label>

        <span id="_spDrafts" runat="server">
			<br /><asp:Label ID="_lbDrafts" runat="server" Text="0" BackColor="#CCCCCC" Font-Size="10pt" Width="100%" Font-Italic="True" ></asp:Label>
	        <br />		
			<asp:GridView ID="_gvMOCDraftListing" runat="server" CssClass="Border" BorderColor="Black"
				BorderWidth="2" AutoGenerateColumns="False" DataKeyNames="MOCNUMBER" EnableViewState="false"
				AllowSorting="true" Width="100%" HeaderStyle-BorderColor="red" HeaderStyle-Font-Bold="True" >
				<Columns>
				    <asp:HyperLinkField HeaderText="MOC" DataTextField="MOCNUMBER" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
						DataNavigateUrlFields="MOCNUMBER" DataNavigateUrlFormatString="~/MOC/EnterMOC.aspx?MOCNumber={0}"
						Target="_self" />
                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="<%$RIResources:Shared,Date %>" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="_lblDate" runat="server" Font-Bold="false" Text='<%# Eval("EventDate", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="25%" HeaderText="<%$RIResources:Shared,Title %>">
                        <ItemTemplate>
                            <asp:Label ID="_lblTitle" runat="server" Font-Bold="false" Text='<%# Bind("TITLE") %>'></asp:Label><br />
                            <asp:Label ID="_lbDescription" runat="server" Font-Bold="false" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:MOC,Initiator/Owner %>">
                        <ItemTemplate>
                            <asp:Label ID="_lblInitiator" runat="server" Font-Bold="false" Text='<%# Bind("initiatorname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
			</asp:GridView>
            <br />		
			<br />
        </span>
        <span id="_spOnHold" runat="server">
            <br />		
                <asp:Label ID="_lbOnHold" runat="server" Text="0" BackColor="#CCCCCC" Font-Size="10pt" Width="100%" Font-Italic="True" ></asp:Label>
	        <br />		
			<asp:GridView ID="_gvMOCOnHoldListing" runat="server" CssClass="Border" BorderColor="Black"
				BorderWidth="2" AutoGenerateColumns="False" DataKeyNames="MOCNUMBER" EnableViewState="false"
				AllowSorting="true" Width="100%" HeaderStyle-BorderColor="red" HeaderStyle-Font-Bold="True" >
				<Columns>
				    <asp:HyperLinkField HeaderText="MOC" DataTextField="MOCNUMBER" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
						DataNavigateUrlFields="MOCNUMBER" DataNavigateUrlFormatString="~/MOC/EnterMOC.aspx?MOCNumber={0}"
						Target="_self" />
                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="<%$RIResources:Shared,Date %>" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="_lblDate" runat="server" Font-Bold="false" Text='<%# Eval("EventDate", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="25%" HeaderText="<%$RIResources:Shared,Title %>">
                        <ItemTemplate>
                            <asp:Label ID="_lblTitle" runat="server" Font-Bold="false" Text='<%# Bind("TITLE") %>'></asp:Label><br />
                            <asp:Label ID="_lbDescription" runat="server" Font-Bold="false" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:MOC,Initiator/Owner %>">
                        <ItemTemplate>
                            <asp:Label ID="_lblInitiator" runat="server" Font-Bold="false" Text='<%# Bind("initiatorname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
			</asp:GridView>
            <br />
			<br />
        </span>
        <span id="_spPendingOwner" runat="server">
            <asp:Label ID="_lbPendingOwner" runat="server" Text="0" BackColor="#CCCCCC" Font-Size="10pt" Width="100%" Font-Italic="True" ></asp:Label>
	        <br />		
			<asp:GridView ID="_gvPendingOwner" runat="server" CssClass="Border" BorderColor="Black"
				BorderWidth="2" AutoGenerateColumns="False" DataKeyNames="MOCNUMBER" EnableViewState="false"
				AllowSorting="true" Width="100%" HeaderStyle-BorderColor="red" HeaderStyle-Font-Bold="True" >
				<Columns>
				    <asp:HyperLinkField HeaderText="MOC" DataTextField="MOCNUMBER" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
						DataNavigateUrlFields="MOCNUMBER" DataNavigateUrlFormatString="~/MOC/EnterMOC.aspx?MOCNumber={0}"
						Target="_self" />
                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="<%$RIResources:Shared,Date %>" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="_lblDate" runat="server" Font-Bold="false" Text='<%# Eval("EventDate", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="25%" HeaderText="<%$RIResources:Shared,Title %>">
                        <ItemTemplate>
                            <asp:Label ID="_lblTitle" runat="server" Font-Bold="false" Text='<%# Bind("TITLE") %>'></asp:Label><br />
                            <asp:Label ID="_lbDescription" runat="server" Font-Bold="false" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:MOC,Initiator/Owner %>">
                        <ItemTemplate>
                            <asp:Label ID="_lblInitiator" runat="server" Font-Bold="false" Text='<%# Bind("initiatorname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
			</asp:GridView>
        </span>
		</ContentTemplate>
	</Asp:UpdatePanel>
	
<%--	<div class="labellogin">
                    <IP:IPLogin ID="_login" runat="server" />
                </div>--%>
	<%--<ajaxToolkit:CascadingDropDown ID="_cddlCoordinator" runat="server" Category="Leader"
		LoadingText="[Loading Coordinator...]" PromptText="   " ServiceMethod="GetPerson"
		ServicePath="~/CascadingLists.asmx" TargetControlID="_ddlPlanner" ParentControlID="ctl00__cphMain__siteLocation__ddlFacility">
	</ajaxToolkit:CascadingDropDown>--%>
	
</asp:Content>
