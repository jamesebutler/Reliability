<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDateRangeSelector.ascx.vb"
    Inherits="RI_User_Controls_Common_ucDateRangeSelector" %>

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

<input type="hidden" id="_startDate" name="_startDate" runat="server" />
<input type="hidden" id="_endDate" name="_endDate" runat="server" />
<input type="hidden" id="_startDateValue" name="_startDateValue" runat="server" />
<input type="hidden" id="_endDateValue" name="_endDateValue" runat="server" />
<table border="0" cellpadding="2" cellspacing="0" width="98%" height="98%">
    <tr>
        <td style="width: 50%">
            <asp:Panel ID="_pnlStartDate" runat="server" BackColor="#999999" Visible="true" HorizontalAlign="center"
                GroupingText="<%$RIResources:Global,StartDate %>" Width="100%">
                <table cellpadding="1" cellspacing="1" width="98%">
                    <tr>
                        <td>
                            <asp:DropDownList onfocus="OK2focus=false;" onblur="ddlBlur();" ID="_ddlStartMonth"
                                runat="server" AutoPostBack="true" EnableViewState="true">
                            </asp:DropDownList></td>
                        <td>
                            <asp:TextBox ID="_txtStartDay" runat="server" ReadOnly="true" Width="20"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList onfocus="OK2focus=false;" onblur="ddlBlur();" ID="_ddlStartYear"
                                runat="server" AutoPostBack="true" EnableViewState="true">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:LinkButton ID="_lnkStartPrevYear" Text="<<" runat="server"></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton
                                ID="_lnkStartPrevMonth" Text="<" runat="server"></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton
                                    ID="_btnStartToday" Text="<%$RIResources:Global,Today %>" runat="server"></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton
                                        ID="_lnkStartNextMonth" Text=">" runat="server"></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton
                                            ID="_lnkStartNextYear" Text=">>" runat="server"></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Calendar ID="_calStartDate" runat="server" BackColor="White" BorderColor="#999999"
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
        </td>
        <td style="width: 50%">
            <asp:Panel ID="_pnlEndDate" runat="server" BackColor="#999999" Visible="true" HorizontalAlign="center"
                GroupingText="<%$RIResources:Global,EndDate %>" Width="100%">
                <table cellpadding="1" cellspacing="1" width="98%">
                    <tr>
                        <td>
                            <asp:DropDownList onfocus="OK2focus=false;" onblur="ddlBlur();" ID="_ddlEndMonth"
                                runat="server" AutoPostBack="true" EnableViewState="true">
                            </asp:DropDownList></td>
                        <td>
                            <asp:TextBox ID="_txtEndDay" runat="server" ReadOnly="true" Width="20"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList onfocus="OK2focus=false;" onblur="ddlBlur();" ID="_ddlEndYear"
                                runat="server" AutoPostBack="true" EnableViewState="true">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:LinkButton ID="_lnkEndPrevYear" Text="<<" runat="server"></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton
                                ID="_lnkEndPrevMonth" Text="<" runat="server"></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton
                                    ID="_btnEndToday" Text="<%$RIResources:Global,Today %>" runat="server"></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton
                                        ID="_lnkEndNextMonth" Text=">" runat="server"></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton
                                            ID="_lnkEndNextYear" Text=">>" runat="server"></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Calendar ID="_calEndDate" runat="server" BackColor="White" BorderColor="#999999"
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
        </td>
    </tr>
    <%--<tr>
		<td></td>
		<td><asp:Button ID=_btnAcceptChanges runat=server Text="Accept" />&nbsp;
		<asp:Button ID=_btnClose runat=server Text="Close" />
		</td>
	</tr>--%>
</table>
<%--	<select id="selMonth" name="selMonth">
		<option value="" selected> </option>
		<option value="1">January</option>
		<option value="2">February</option>
		<option value="3">March</option>
		<option value="4">April</option>
		<option value="5">May</option>
		<option value="6">June</option>
		<option value="7">July</option>
		<option value="8">August</option>
		<option value="9">September</option>
		<option value="10">October</option>
		<option value="11">November</option>
		<option value="12">December</option>
	</select>

	<select name="selDay" id="selDay">
		<option value="" selected> </option>
		<option value="1">1</option>
		<option value="2">2</option>
		<option value="3">3</option>
		<option value="4">4</option>
		<option value="5">5</option>
		<option value="6">6</option>
		<option value="7">7</option>
		<option value="8">8</option>
		<option value="9">9</option>
		<option value="10">10</option>
		<option value="11">11</option>
		<option value="12">12</option>
		<option value="13">13</option>
		<option value="14">14</option>
		<option value="15">15</option>
		<option value="16">16</option>
		<option value="17">17</option>
		<option value="18">18</option>
		<option value="19">19</option>
		<option value="20">20</option>
		<option value="21">21</option>
		<option value="22">22</option>
		<option value="23">23</option>
		<option value="24">24</option>
		<option value="25">25</option>
		<option value="26">26</option>
		<option value="27">27</option>
		<option value="28">28</option>
		<option value="29">29</option>
		<option value="30">30</option>
		<option value="31">31</option>
	</select>

	<select name="selYear" id="selYear">
		<option value="" selected> </option>
		<option value="2006">2006</option>
		<option value="2007">2007</option>
		<option value="2008">2008</option>
	</select>
<br />
<div id="cal1Container" class="yui-skin-sam" style="border:1px;"></div>
<script type="text/javascript">
	YAHOO.namespace("example.calendar");

	YAHOO.example.calendar.init = function() {
	
		function handleSelect(type,args,obj) {
			var dates = args[0]; 
			var date = dates[0];
			var year = date[0], month = date[1], day = date[2];

			var selMonth = document.getElementById("selMonth");
			var selDay = document.getElementById("selDay");
			var selYear = document.getElementById("selYear");
			
			selMonth.selectedIndex = month;
			selDay.selectedIndex = day;

			for (var y=0;y<selYear.options.length;y++) {
				if (selYear.options[y].text == year) {
					selYear.selectedIndex = y;
					break;
				}
			}
		}

		function updateCal() {
			var selMonth = document.getElementById("selMonth");
			var selDay = document.getElementById("selDay");
			var selYear = document.getElementById("selYear");
			
			var month = parseInt(selMonth.options[selMonth.selectedIndex].value);
			var day = parseInt(selDay.options[selDay.selectedIndex].value);
			var year = parseInt(selYear.options[selYear.selectedIndex].value);
			
			if (! isNaN(month) && ! isNaN(day) && ! isNaN(year)) {
				var date = month + "/" + day + "/" + year;

				YAHOO.example.calendar.cal1.select(date);
				YAHOO.example.calendar.cal1.cfg.setProperty("pagedate", month + "/" + year);
				YAHOO.example.calendar.cal1.render();
			}
		}

		YAHOO.example.calendar.cal1 = new YAHOO.widget.Calendar("cal1","cal1Container", 
																	{ mindate:"1/1/2006",
																	  maxdate:"12/31/2008" });
		YAHOO.example.calendar.cal1.selectEvent.subscribe(handleSelect, YAHOO.example.calendar.cal1, true);
		YAHOO.example.calendar.cal1.render();

		YAHOO.util.Event.addListener(["selMonth","selDay","selYear"], "change", updateCal);
	}

	YAHOO.util.Event.onDOMReady(YAHOO.example.calendar.init);
</script>

<div style="clear:both" ></div>--%>
