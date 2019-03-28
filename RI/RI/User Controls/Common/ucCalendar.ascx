<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucCalendar.ascx.vb" Inherits="_ucCalendar" %>
<table border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <asp:Label ID="_lblStartDate" Text='<%$RIResources:Global,StartDate %>' runat="server"></asp:Label>&nbsp;<asp:TextBox
                ID="_txtStartDate" runat="server" CssClass="DateRange"></asp:TextBox>
        </td>
        <td valign="baseline" width="34">
            <asp:Image runat="server" ID="_imgStartCal" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom" />
        </td>
        <td width="40">
            <asp:Image ID="_imgSpacer" ImageUrl="~/Images/blank.gif" runat="server" Width="40"
                Height="1" />
        </td>
        <td>
            <asp:Label ID="_lblEndDate" Text='<%$RIResources:Global,EndDate %>' runat="server"></asp:Label>&nbsp;<asp:TextBox
                ID="_txtEndDate" EnableViewState="true" runat="server" CssClass="DateRange"></asp:TextBox>
        </td>
        <td width="34">
            <asp:Image runat="server" ID="_imgEndCal" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom" />
        </td>
    </tr>
</table>
<ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="_imgStartCal"
    Position="bottom" PopupControlID="_pnlDate">
</ajaxToolkit:PopupControlExtender>
<ajaxToolkit:PopupControlExtender ID="PopupControlExtender2" runat="server" PopupControlID="_pnlEndDate"
    Position="bottom" TargetControlID="_imgEndCal">
</ajaxToolkit:PopupControlExtender>
<asp:Panel ID="_pnlDate" runat="server" BackColor="#999999" CssClass="popupControl"
    Visible="true" HorizontalAlign="center" Width="160px">
    <asp:DropDownList ID="_ddlMonthStartDate" runat="server" AutoPostBack="true" EnableViewState="true">
    </asp:DropDownList>
    <asp:DropDownList ID="_ddlYearStartDate" runat="server" AutoPostBack="true" EnableViewState="true">
    </asp:DropDownList>
    <asp:Calendar ID="_calStartDate" runat="server" BackColor="White" BorderColor="#999999"
        CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
        ForeColor="Black" Width="160px" ShowTitle="false">
        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
        <SelectorStyle BackColor="#CCCCCC" />
        <WeekendDayStyle BackColor="#FFFFCC" />
        <OtherMonthDayStyle ForeColor="#808080" />
        <NextPrevStyle VerticalAlign="Bottom" />
        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
        <TitleStyle BackColor="#999999" Font-Size="7pt" BorderColor="Black" Font-Bold="True" />
    </asp:Calendar>
</asp:Panel>
<asp:Panel ID="_pnlEndDate" runat="server" BackColor="#999999" CssClass="popupControl"
    Visible="true" Width="160px" HorizontalAlign="Center">
    <asp:DropDownList ID="_ddlMonthEndDate" runat="server" AutoPostBack="true" EnableViewState="true">
    </asp:DropDownList>
    <asp:DropDownList ID="_ddlYearEndDate" runat="server" AutoPostBack="true" EnableViewState="true">
    </asp:DropDownList>
    <asp:Calendar ID="_calEndDate" runat="server" BackColor="White" BorderColor="#999999"
        CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
        ForeColor="Black" Width="160px" ShowTitle="false" BorderWidth="1">
        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
        <SelectorStyle BackColor="#CCCCCC" />
        <WeekendDayStyle BackColor="#FFFFCC" />
        <OtherMonthDayStyle ForeColor="#808080" />
        <NextPrevStyle VerticalAlign="Bottom" />
        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
        <TitleStyle BackColor="#999999" Font-Size="7pt" BorderColor="Black" Font-Bold="True" />
    </asp:Calendar>
</asp:Panel>
