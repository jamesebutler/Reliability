<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDateRange.ascx.vb" Inherits="RI_User_Controls_Common_ucDateRange" %>
<%@ Register Src="~/RI/User Controls/Common/ucStartEndCalendar.ascx" TagName="StartEndCalendar" TagPrefix="IP"%>
    <asp:Table ID="tblMain" runat="server" CellPadding="0" CellSpacing="0" Width="100%" BorderWidth="0">     
    <asp:TableRow CssClass="Border">
        <asp:TableCell ID="_tcCalendar" VerticalAlign="top" Wrap="false" Width="58%">
        <IP:StartEndCalendar ID="_StartEndCalendar" runat="server"  />
        </asp:TableCell> 
        <asp:TableCell VerticalAlign="top" Wrap="false" Width="42%">
        <asp:Label ID="_lblDateRange" runat="server" Text="<%$ RIResources:Shared,DateRange %>"
                    EnableViewState="false"></asp:Label> &nbsp;
                   <asp:DropDownList AutoPostBack="false" ID="_ddlDateRange" runat="server">
                    </asp:DropDownList>
                    <asp:RadioButtonList ID="_rblDateRange" runat="server"  AutoPostBack="false" RepeatColumns="3" RepeatDirection="vertical" RepeatLayout="table">
                    </asp:RadioButtonList>
                    </asp:TableCell> 
    </asp:TableRow>     
</asp:Table>



