<%@ Control EnableTheming="false" Language="VB" AutoEventWireup="false" CodeFile="ucStartEndCalendar.ascx.vb"
    Inherits="ucStartEndCalendar" %>
<table border="0" cellpadding="0" cellspacing="0" style="text-align: left">
    <tr>
        <td>
            <asp:Label ID="_lblStartDate" Text='<%$RIResources:Shared,Start %>' runat="server"></asp:Label>&nbsp;<asp:TextBox
                ID="_txtStartDate" runat="server" CssClass="DateRange"></asp:TextBox>
            <asp:HiddenField ID="_hdfStartDateValue" runat="server" />
        </td>
        <td valign="baseline">
            <asp:ImageButton runat="server" ID="_imgStartCal" ImageUrl="~/Images/calendar.gif"
                ImageAlign="Bottom" />
        </td>
        <td nowrap=nowrap>
            &nbsp;
            <asp:Label ID="_lblStartTime" runat="server" Text="<%$RIResources:Global,Time %>"></asp:Label>
            <asp:DropDownList ID="_ddlStartHrs" runat="server">
            </asp:DropDownList>&nbsp;
            <asp:DropDownList ID="_ddlStartMins" runat="server">
            </asp:DropDownList>
        </td>
        <td style="width: 10px">
            &nbsp;</td>
        <td>
            <asp:Label ID="_lblEndDate" Text='<%$RIResources:Shared,End %>' runat="server"></asp:Label>&nbsp;<asp:TextBox
                ID="_txtEndDate" EnableViewState="true" runat="server" CssClass="DateRange"></asp:TextBox>
            <asp:HiddenField ID="_hdfEndDateValue" runat="server" />
        </td>
        <td>
            <asp:ImageButton runat="server" ID="_imgEndCal" ImageUrl="~/Images/calendar.gif" />
        </td>
        <td nowrap=nowrap>
            &nbsp;
            <asp:Label ID="_lblEndTime" runat="server" Text="<%$RIResources:Global,Time %>"></asp:Label>
            <asp:DropDownList ID="_ddlEndHrs" runat="server">
            </asp:DropDownList>&nbsp;
            <asp:DropDownList ID="_ddlEndMins" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
</table>
