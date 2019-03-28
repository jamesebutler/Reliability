<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucMOCReportSelection.ascx.vb" Inherits="ucMOCReports" EnableViewState="true" %>

        <asp:Table ID="_tblSite" runat="server" BorderWidth="0" CellPadding="2" CellSpacing="2"
            BackColor="white" Style="width: 98%" EnableViewState="false">
            <asp:TableHeaderRow CssClass="Header">
                <asp:TableHeaderCell ColumnSpan="3" HorizontalAlign="left">
                    <asp:Label ID="Label1" runat="server" Text="Site" SkinID="LabelWhite"></asp:Label>
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow CssClass="Border">
                <asp:TableCell ColumnSpan="3" Width="100%" Style="width: 100%">
                    <%--<asp:DropDownList ID="_ddlSiteDivision" SkinID="SiteDropdown" Visible="false" runat="server">
                    </asp:DropDownList>--%>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow  CssClass="Border"  >
            <asp:TableCell ColumnSpan="3" Width="100%" Style="width: 100%">
            </asp:TableCell></asp:TableRow>
        </asp:Table>
        
        <IP:SiteLocation ID="_ucSiteLocation" runat="server" Visible="false"/>
        <Asp:UpdatePanel ID="_upSite" UpdateMode="Always" runat="server">

    <ContentTemplate>
    <%--</ContentTemplate>
</Asp:UpdatePanel>
<Asp:UpdatePanel ID="_udpReportParameters" UpdateMode="Always" runat="server">
    <ContentTemplate>--%>
        <asp:Table ID="_tblMain" BackColor="White" runat="server" CssClass="Main" CellPadding="2"
            CellSpacing="2" Width="98%">
            <asp:TableRow ID="_tr1" CssClass="Header" >
                <asp:TableCell Width="50%"></asp:TableCell><asp:TableCell Width="50%"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="_tr2" CssClass="Border"   >
                <asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="_tr3" CssClass="Header">
                <asp:TableHeaderCell></asp:TableHeaderCell><asp:TableHeaderCell></asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow ID="_tr4" CssClass="Border"  >
                <asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="_tr5" CssClass="Header" >
                <asp:TableHeaderCell></asp:TableHeaderCell><asp:TableHeaderCell></asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow ID="_tr6" CssClass="Border" >
                <asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="_tr7" CssClass="Header">
                <asp:TableHeaderCell></asp:TableHeaderCell><asp:TableHeaderCell></asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow ID="_tr8" CssClass="Border" >
                <asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="_tr9" CssClass="Border"  >
                <asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        
        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%" Class="Main" >
            <tr>
                <td align="left">
                <IP:MOCCategory ID="_ucMOCCategory" runat="server"  DisplayMode="Search"/>
                </td>
            </tr>
        </table>
            
        <asp:table ID="Table1" runat="server"> 
            <asp:TableRow ID="_trMocDetail" CssClass="Header" runat="server" Visible="false">
                <asp:TableCell HorizontalAlign="left">
                    <asp:Label ID="Label2" runat="server" Text="<%$RIResources:Shared,MOCNumber %>" SkinID="LabelWhite"/>&nbsp;  
                    <asp:TextBox ID="_tbMOCNumber" runat="server"></asp:TextBox></asp:TableCell>
                <asp:TableCell HorizontalAlign="left" Width="33%" >  
		            <asp:Label ID="_lbInitiator" runat="server" Text="<%$RIResources:Shared,Initiator %>"
                    SkinID="LabelWhite" />&nbsp;
                    <asp:DropDownList ID="_ddlInitiator" runat="server"></asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="left" Width="33%" >  
		            <asp:Label ID="Label3" runat="server" Text="<%$RIResources:Shared,Approver %>"
                    SkinID="LabelWhite" />&nbsp;
                    <asp:DropDownList ID="_ddlApprover" runat="server"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
        </asp:table>
        
        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%" Class="Main" >
            <tr>
                <td align="center">
                    <asp:Button Text="<%$RIResources:ButtonText,SubmitReport %>" ID="_btnSubmit" runat="server" />
                    <asp:Button Text="<%$RIResources:ButtonText,ResetCriteria %>" runat="server" ID="_btnReset" />
                </td>
            </tr>
        </table>

        <br />
        <center>
        <asp:Panel ID="_pnlHelp" runat="server" CssClass="modalPopup" Width="800" Height="200" Visible=false>
            <table border="1" class="help" cellpadding="2" cellspacing="0" style="width: 100%;
                height: 100%" align="center">
                <tr>
                    <th height=14px>
                        Instructions</th>
                </tr>                
                <tr>
                    <td width="100%" height="100%" valign="top" align=left>
                       <asp:Literal ID=_liInstructions runat=server Text=""></asp:Literal>
                    </td>
                </tr>                
            </table>
        </asp:Panel> 
        </center>

        <IP:StartEndCalendar ID="_ucCalendar" runat="server" Visible="false" EnableViewState="true"  ChangeDateLabel="true" />
        <IP:DateRange ID="_DateRange" runat="server" Visible="false" EnableViewState="true"  ChangeDateLabel="true"/>
        <IP:MOCClass ID="_ucMOCClassification" runat="server"  DisplayMode="Search" Visible="false"/>
        <IP:MOCType id="_ucMOCType" runat="server" displaymode="Search" visible="false" />         
        <ajaxToolkit:CascadingDropDown ID="_cddlInitiator" runat="server" Category="Trigger" LoadingText="[Loading Initiators...]"
	        PromptText="   " ServiceMethod="GetPerson" ServicePath="~/CascadingLists.asmx"
			TargetControlID="_ddlInitiator" ParentControlID="ctl00__cphMain__ucMOCReports__ucSiteLocation__ddlFacility">
		</ajaxToolkit:CascadingDropDown>
        <ajaxToolkit:CascadingDropDown ID="_cddlApprover" runat="server" Category="Trigger" LoadingText="[Loading Approvers...]"
	        PromptText="   " ServiceMethod="GetEmployee" ServicePath="~/CascadingLists.asmx"
			TargetControlID="_ddlApprover" ParentControlID="ctl00__cphMain__ucMOCReports__ucSiteLocation__ddlFacility">
		</ajaxToolkit:CascadingDropDown>
		
   </ContentTemplate>
</Asp:UpdatePanel>
