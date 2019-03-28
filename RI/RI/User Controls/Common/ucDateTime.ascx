<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Control Language="VB" AutoEventWireup="false" EnableViewState="true" CodeFile="ucDateTime.ascx.vb"
	Inherits="RI_User_Controls_Common_ucDateTime" %>
<script type="text/javascript">
function dateComplete(strClientID)
{
    var blnReturnDate = false;
    var objTextbox = $get(strClientID);
    var strDateText = objTextbox.value;
    var arrDate = strDateText.split("/");
    var stryear;
   
   if (strDateText.length==0){return false}
    // Check the length of the year, if 2 then re-calculate
    if (arrDate[2].replace("__", "").length == 2)
    {
        blnReturnDate = true;
        // Replace the editmask characters
        strYear = arrDate[2].replace("__", "");
        if (parseInt(strYear) >= 49)
        {
            strYear = "19" + strYear;
        }
        else
        {
            strYear = "20" + strYear;
        }
    }
   
    if (blnReturnDate == true)
    {
        objTextbox.value = arrDate[0] + "/" + arrDate[1] + "/" + strYear;
        //alert(objTextbox.value);
    }
   
}
</script>
<table cellpadding="2" cellspacing="0">
	<tr>
		<td>
			<asp:Label ID="_lblRequired" runat="server" Visible="false" SkinID="None" CssClass="ValidationError">*</asp:Label><asp:Label
				ID="_lblDate" runat="server" Text="<%$ RIResources:Shared,Date %>"></asp:Label>&nbsp;</td>
		<td>
			<asp:TextBox ID="_txtDate" runat="server" CssClass="DateRange" ReadOnly="false"></asp:TextBox>			
			<asp:HiddenField ID="_hdfDateValue" runat="server" />
			</td>
		<td>
			<asp:ImageButton ID="_imgCalendar" ImageUrl="~/Images/calendar.gif" runat="server" />
			<%--<ajaxToolkit:MaskedEditExtender ID="_meeDate" runat="server" AcceptAMPM="false" ClearTextOnInvalid="true"
	        MaskType="Date" UserDateFormat="MonthDayYear" TargetControlID="_txtDate" Mask="99/99/9999"
	        DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" AutoComplete="false" />--%>

<%--<ajaxToolkit:MaskedEditValidator ID="_meeValidator" runat="server" ControlExtender="_meeDate"
	ControlToValidate="_txtDate" InvalidValueMessage="Date is invalid" Display=Static
	TooltipMessage="" IsValidEmpty="true" EnableClientScript="true" SetFocusOnError="true"
	EmptyValueBlurredText="*"  InvalidValueBlurredMessage="*" />--%>
			</td>
		<td>
			&nbsp;&nbsp;<asp:Label ID="_lblTime" runat="server" Text="<%$ RIResources:Shared,Time %>"></asp:Label></td>
		<td>
			<asp:DropDownList ID="_ddlHrs" runat="server">
			</asp:DropDownList></td>
		<td>
			<asp:DropDownList ID="_ddlMins" runat="server">
			</asp:DropDownList></td>
	</tr>
</table>

<%--<ajaxToolkit:CalendarExtender PopupButtonID="_imgCalendar" CssClass="Calendar" ID="_calDate" 
 TargetControlID="_txtDate" runat="server" EnabledOnClient=true Animated=true>
</ajaxToolkit:CalendarExtender>--%>
