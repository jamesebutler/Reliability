<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucTimePicker.ascx.vb"
    Inherits="RI_User_Controls_ucTimePicker" %>

<table border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <asp:Label ID="_lblDate" Text='Start Date' runat="server"></asp:Label>&nbsp;<asp:TextBox
                ID="_txtDate" runat="server" CssClass="DateRange"></asp:TextBox>
        </td>
        <td valign="baseline" width="34">
            <asp:Image runat="server" ID="_imgCal" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom" />
        </td>
        <td width="5">
            <asp:Image ID="_imgSpacer" ImageUrl="~/Images/blank.gif" runat="server" Width="5"
                Height="1" />
        </td>
        <td align="right">
            <nobr>
                <asp:Label ID="_lblEndDate" Text='Time:' runat="server"></asp:Label>&nbsp;<asp:TextBox
                    ID="_txtTime" runat="server" CssClass="Datetime"></asp:TextBox>
            </nobr>
        </td>
        <td width="34" valign="baseline">
            <asp:Image runat="server" ID="_imgClock" ImageUrl="~/Images/clock.gif" ImageAlign="Bottom" />
        </td>
    </tr>
</table>

<ajaxToolkit:PopupControlExtender ID="PopupControlExtender2" runat="server" PopupControlID="_pnlTime"
    Position="bottom" TargetControlID="_imgClock">
</ajaxToolkit:PopupControlExtender>
<ajaxToolkit:CalendarExtender PopupButtonID="_imgCal" CssClass="Calendar" ID="_calDate" 
 TargetControlID="_txtDate" runat="server" EnabledOnClient=true Animated=true>
</ajaxToolkit:CalendarExtender>
<asp:Panel Visible="true" ID="_pnlTime" runat="server" BorderColor="#646464" BorderWidth=1 BackColor="#999999" CssClass="popupControl">
    <Asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode=conditional>
        <ContentTemplate>
            <asp:Table ID="_tblTimePickerMain" BorderWidth=1  runat="server">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Hours</asp:TableHeaderCell><asp:TableHeaderCell>Minutes</asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Table ID="_tblTimePicker" runat="server">
                        </asp:Table>
                    </asp:TableCell><asp:TableCell>
                        <asp:Table ID="_tblTimePickerMinutes" runat="server">
                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableFooterRow>
                    <asp:TableCell HorizontalAlign=left ColumnSpan=2>
                        <span style="text-align:left"> 
                            <asp:LinkButton Text="Current Time:" runat=server ID=_btnTime CssClass="TimeButtons"></asp:LinkButton>
                        </span>
                        &nbsp;&nbsp;&nbsp;
                        <span style="text-align:right">
                        <asp:LinkButton ID=_btnUpdate runat=server Text="Update" CssClass="TimeButtons" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID=_btnCancel runat=server CssClass="TimeButtons" OnClientClick="cancelPopup();return false;" Text="Cancel" />                   
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID=_btnClear runat=server CssClass="TimeButtons" Text="Clear" />                   
                        </span> 
                        </asp:TableCell>
                </asp:TableFooterRow>
            </asp:Table>
          
        </ContentTemplate>
    </Asp:UpdatePanel>
</asp:Panel>
