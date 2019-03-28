<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="Reporting.aspx.vb"
    Inherits="MOC_Reporting" Trace="false" Title="International Paper: MOC Reporting" EnableSessionState="true" EnableViewState="false" %>
<%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">    
    <Asp:UpdatePanel ID="_upReporting" ChildrenAsTriggers="false" runat="server" UpdateMode="Conditional">
		<Triggers>
            
			<asp:AsyncPostBackTrigger ControlID="_ddlReportList" EventName="SelectedIndexChanged"  /> 
		</Triggers>
        <ContentTemplate>
            <table width="100%" cellpadding="2" cellspacing="2" border="0">
                <tr class="Header">
                    <td style="height: 17px; text-align: left">
                        <asp:Label ID="_lblCaption" runat="server" Text="<%$RIResources:Shared,ReportListing %>" SkinID="LabelWhite" EnableViewState="false"></asp:Label></td>
                </tr>
                <tr class="Border">
                    <td align="left">
                        <asp:Label ID="_lblReport" runat="server" Text="<%$RIResources:Shared,Report %>" EnableViewState="false"></asp:Label>&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="_ddlReportList" AutoPostBack="true" runat="server"
                            EnableViewState="true">
                        </asp:DropDownList>
                        <asp:ImageButton ImageUrl="~/Images/question.gif" OnClientClick="ShowMyModalPopup();return false;"
                            runat="server" ID="_imbHelp2" Visible="true" EnableViewState="false" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="_lblReportSortValue"
                            Visible="false" runat="server" Text="<%$RIResources:Shared,ReportSortValue %>" EnableViewState="true"></asp:Label>&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="_ddlReportSortValue" Visible="false" AutoPostBack="false"
                            runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <IP:MOCReportSelector ID="_ucMOCReports" runat="server" Visible="false" />
             
        </ContentTemplate>
    </Asp:UpdatePanel>
</asp:Content>
