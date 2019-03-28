<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PopupCalendar.aspx.vb" Inherits="PopupCalendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Select a Date</title>

    <script language="javascript">
        var ftimer = null;
        var OK2focus = true;

        function setBlur()
        {
            ftimer = setTimeout('if(OK2focus)self.focus()', 100);
        }
        function ddlBlur()
        {
            OK2focus=true;
            //self.onblur();
            if (self!=null) self.focus();
        }
    </script>

</head>
<body leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
        <input type="hidden" id="_Date" name="_Date" runat="server" />
        <input type="hidden" id="_DateValue" name="_DateValue" runat="server" />
        <asp:Panel ID="_pnlDate" runat="server" BackColor="#999999" Visible="true" HorizontalAlign="center"
            Width="100%">
            <table cellpadding="1" cellspacing="1" width="98%" height="98%">
                <tr>
                    <td>
                        <asp:DropDownList ID="_ddlMonth" runat="server" AutoPostBack="true" EnableViewState="true">
                        </asp:DropDownList></td>
                    <td>
                        <asp:TextBox ID="_txtDay" runat="server" ReadOnly="true" Width="20"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="_ddlYear" runat="server" AutoPostBack="true" EnableViewState="true">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:LinkButton ID="_lnkPrevYear" Text="<<" runat="server"></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton
                            ID="_lnkPrevMonth" Text="<" runat="server"></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton
                                ID="_btnToday" Text="<%$RIResources:Shared,Today%>" runat="server"></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton
                                    ID="_lnkNextMonth" Text=">" runat="server"></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton
                                        ID="_lnkNextYear" Text=">>" runat="server"></asp:LinkButton></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Calendar ID="_calDate" runat="server" BackColor="White" BorderColor="#999999"
                            CellPadding="1" Font-Names="Verdana" Font-Size="10pt" ForeColor="Black" Width="100%"
                            Height="100%" ShowTitle="False">
                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <SelectorStyle BackColor="#CCCCCC" />
                            <WeekendDayStyle BackColor="#FFFFCC" />
                            <OtherMonthDayStyle ForeColor="Gray" />
                            <NextPrevStyle VerticalAlign="Bottom" />
                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="10pt" />
                            <TitleStyle BackColor="#999999" Font-Size="10pt" BorderColor="Black" Font-Bold="True" />
                        </asp:Calendar>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
