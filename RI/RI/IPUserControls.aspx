<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="IPUserControls.aspx.vb"
    Inherits="RI_IPUserControls" Title="IP User Controls" EnableEventValidation="false" %>

<%--<%@ Register Assembly="AutoSizeDropDown" Namespace="AutoSizeDropDown" TagPrefix="IP" %>
--%><%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">
    <Asp:UpdatePanel ID="_udpanel" runat="server" UpdateMode="conditional">
        <ContentTemplate>
            <table width="100%" cellpadding="2" cellspacing="2">
               <%-- <tr>
                    <td colspan="2">
                        <table><tr><td>
                        <IP:AutoSizeDropDown ID="_ddlTest1" runat="server" Width="100px">
                        </IP:AutoSizeDropDown></td><td>
                        <IP:AutoSizeDropDown ID="_ddlTest2" runat="server" Width="100px">
                        </IP:AutoSizeDropDown></td><td>
                        <IP:AutoSizeDropDown ID="_ddlTest3" runat="server" Width="100px">
                        </IP:AutoSizeDropDown></td><td>
                        <IP:AutoSizeDropDown ID="_ddlTest4" runat="server" Width="100px">
                        </IP:AutoSizeDropDown></td><td>
                        <IP:AutoSizeDropDown ID="_ddlTest5" runat="server" Width="100px">
                        </IP:AutoSizeDropDown></td>
                        </tr></table>
                    </td>
                </tr>--%>
                <tr class="Border">
                    <td width="50%" valign="top">
                        <b>&lt;IP:DateRange</b> <i>SelectedDateRange=Last3Months</i> ID=_DateRange runat=server/&gt;
                    </td>
                    <td width="50%">
                        <b>DateRange:</b>&nbsp;The DateRange control allows you to set a date range by using
                        the start and end date calendars or by selecting a daterange.<br />
                        <br />
                        <b>SelectedDateRange:</b> List of available date ranges
                    </td>
                </tr>
                <tr class="BorderSecondary">
                    <td colspan="2">
                        <IP:DateRange SelectedDateRange="Last3Months" ID="_DateRange" runat="server" />
                    </td>
                </tr>
                <tr class="Border">
                    <td>
                        &lt;<b>IP:DateTime</b> ID="_DateTime" runat=server /&gt;</td>
                    <td>
                        <b>DateTime:</b>&nbsp;The DateTime controls allows for the selection of the date
                        and time.</td>
                </tr>
                <tr class="BorderSecondary">
                    <td colspan="2">
                        <IP:DateTime ID="_DateTime" DateLabel="Date:" runat="server" />
                    </td>
                </tr>
                <tr class="Border">
                    <td>
                        &lt;<b>IP:DateTime</b> ID="_DateTime" <b>DisplayTime=false</b> runat=server /&gt;</td>
                    <td>
                        <b>DateTime:</b>&nbsp;The DateTime controls allows for the selection of the date
                        and time.</td>
                </tr>
                <tr class="BorderSecondary">
                    <td colspan="2">
                        <IP:DateTime ID="DateTime1" DisplayTime="false" DateLabel="Date:" runat="server" />
                    </td>
                </tr>
                <tr class="Border">
                    <td>
                        &lt;<b>IP:StartEndCalendar</b> ID=_startEndCalendar runat=server /&gt;
                    </td>
                    <td>
                        <b>StartEndCalendar:</b>&nbsp;The StartEndCalendar control allows for the selection
                        of the Start and End Date</td>
                </tr>
                <tr class="BorderSecondary">
                    <td colspan="2">
                        <IP:StartEndCalendar ID="_startEndCalendar" runat="server" />
                    </td>
                </tr>
                <tr class="Border">
                    <td>
                        &lt;<b>IP:StartEndCalendar</b> ID=_startEndCalendar runat=server ShowTime=true /&gt;
                    </td>
                    <td>
                        <b>StartEndCalendar:</b>&nbsp;The StartEndCalendar control allows for the selection
                        of the Start and End Date</td>
                </tr>
                <tr class="BorderSecondary">
                    <td colspan="2">
                        <IP:StartEndCalendar ID="StartEndCalendar1" ShowTime="true" runat="server" />
                    </td>
                </tr>
                <tr class="Border">
                    <td>
                        &lt;<b>ip:swaplistbox</b> id=_swapListBox runat=server/&gt;</td>
                    <td>
                        <b>Swaplistbox:</b>&nbsp;The Swaplistbox control contains two single column lists,
                        side by side. Four buttons are place between the columns: Move Selected, Move All,
                        Remove Selected, Remove All. This control is generally used to pick a subset of
                        unique entries from a larger set of data.</td>
                </tr>
                <tr class="BorderSecondary" valign="top">
                    <td colspan="2">
                        <IP:SwapListBox ID="_swapListBox" runat="server"></IP:SwapListBox>
                    </td>
                </tr>
                <tr class="Border" valign="top">
                    <td>
                        &lt;<b>ip:AdvancedTextBox</b> id=_AdvancedTextBox <i>MaxLength=50</i> <i>ExpandHeight=true</i>
                        TextMode=multiLine runat=server/&gt;</td>
                    <td>
                        <b>AdvancedTextBox:</b>&nbsp;The AdvancedTextBox control is an extended TextBox
                        control that contains all of the features of a traditional TextBox control with
                        the addition of two features:<br />
                        <b>MaxLength</b> (Integer) - Gets or sets the maximum number of characters allowed
                        in the text box .<br />
                        <b>ExpandHeight</b> (Boolean) - Allows the control to expand or collapse based on
                        the contents of the textbox.</td>
                </tr>
                <tr class="BorderSecondary" valign="top">
                    <td colspan="2">
                        <IP:AdvancedTextBox ID="_AdvancedTextBox" Width="200" MaxLength="50" ExpandHeight="true"
                            TextMode="multiLine" runat="server" Text="thisd sentene haz mistakes"></IP:AdvancedTextBox>
                        <IP:AdvancedTextBox ID="_AdvancedTextBox2" Width="200" MaxLength="50" ExpandHeight="true"
                            TextMode="multiLine" runat="server" Text="woud be nice iff wee had spellchecum"></IP:AdvancedTextBox>
                        <IP:AdvancedTextBox ID="_AdvancedTextBox3" Width="200" MaxLength="50" ExpandHeight="true"
                            TextMode="multiLine" runat="server" Text="thisd sentene haz mistakes"></IP:AdvancedTextBox>
                    </td>
                </tr>
                <tr class="Border" valign="top">
                    <td>
                        &lt;<b>IP:SpellCheck</b> ID=SpellCheck1 runat=server <i>ControlIdsToCheck="_AdvancedTextBox,_AdvancedTextBox2,_AdvancedTextBox3"</i>
                        /&gt;</td>
                    <td>
                        <b>SpellCheck:</b>&nbsp;The SpellCheck button performs spell checking on the specified
                        controls.<br />
                        <b>ControlIdsToCheck</b>&nbsp;(string) - Gets or sets the comma separated list of
                        controls to spell check.
                    </td>
                </tr>
                <tr class="BorderSecondary">
                    <td colspan="2">
                        <IP:SpellCheck ID="_btnSpellCheck" runat="server" ControlIdsToCheck="_AdvancedTextBox,_AdvancedTextBox2,_AdvancedTextBox3" />
                    </td>
                </tr>
                <tr class="Border" valign="top">
                    <td>
                        &lt;<b>IP:DisplayExcel</b> ID=_displayExcel runat=server <i>ButtonText="Display Excel"
                            ShowButton=true</i> /&gt;</td>
                    <td>
                        <b>DisplayExcel:</b>&nbsp;The DisplayExcel control will export a dataset or OracleDataReader
                        to excel.<br />
                        <b>ButtonText</b>&nbsp;(string) - Text that will be displayed on the button.<br />
                        <b>ShowButton</b>&nbsp;(boolean) - Determine if the DisplayExcel button should be
                        displayed. (Default is false)<br />
                        <pre>
'Here's an example of how to use the control
Protected Sub _displayExcel_DisplayExcel_Click() _
	Handles _displayExcel.DisplayExcel_Click
	Dim sql As String = Resources.Sql.sqlFacility
	Dim ds As System.Data.DataSet = Nothing
	ds = RI.SharedFunctions.GetOracleDataSet(sql)
	Me._displayExcel.DisplayExcel(ds)
	ds = Nothing
End Sub
                </pre>
                    </td>
                </tr>
                <tr class="BorderSecondary">
                    <td colspan="2">
                        <IP:DisplayExcel ID="_displayExcel" runat="server" ButtonText="Display Excel" ShowButton="true" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </Asp:UpdatePanel>
</asp:Content>
