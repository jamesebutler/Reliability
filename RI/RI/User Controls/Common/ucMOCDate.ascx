<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Control Language="VB" AutoEventWireup="false" EnableViewState="true" CodeFile="ucMOCDate.ascx.vb"
	Inherits="ucMOCDate" %>
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

function clearDate(strClientID,strHfClientID)
{
    var objTextbox = $get(strClientID);
    var objHFTextBox = $get(strHfClientID);
    var strDateText = objTextbox.value;
    
   if (strDateText.length==0)
    {return false}
   else
    {objTextbox.value = "";
    objHFTextBox.value = "";}
}

</script>
<table cellpadding="2" cellspacing="0">
	<tr>
		<td>
			<asp:Label ID="Label1" runat="server" Visible="false" SkinID="None" CssClass="ValidationError">*</asp:Label><asp:Label
				ID="Label2" runat="server" Style="font-family: Verdana; font-size: 8pt" Text="<%$ RIResources:Shared,Days After Approval %>"></asp:Label>&nbsp;</td>
		<td>
			<asp:TextBox ID="_txtDaysAfter" runat="server" CssClass="DateRange" ReadOnly="false"></asp:TextBox>		
			<ajaxToolkit:FilteredTextBoxExtender ID="_fbeDaysAfter" runat="server"
                 TargetControlID="_txtDaysAfter" FilterType="custom" ValidChars="1234567890">
                        </ajaxToolkit:FilteredTextBoxExtender>
           <asp:HiddenField ID="HiddenField1" runat="server" />
		</td>
	</tr>
	<tr>
		<td>	<asp:Label ID="_lblRequired" runat="server" Visible="false" SkinID="None" CssClass="ValidationError">*</asp:Label>
		    <asp:Label ID="_lblDate" runat="server" Style="font-family: Verdana; font-size: 8pt" Text="<%$ RIResources:Shared,due Date %>"></asp:Label>&nbsp;</td>
		<td>
			<span id="second"><asp:TextBox ID="_txtDate" runat="server" CssClass="DateRange" ReadOnly="false"></asp:TextBox>		
			<asp:HiddenField ID="_hdfDateValue" runat="server" />
			<asp:ImageButton ID="_imgCalendar" ImageUrl="~/Images/calendar.gif" runat="server" />
			<asp:LinkButton ID="_lbClear" runat="Server" Text="Clear" Visible=false></asp:LinkButton></span>
		</td>	
	</tr>
</table>

<%--<ajaxToolkit:CalendarExtender PopupButtonID="_imgCalendar" CssClass="Calendar" ID="_calDate" 
 TargetControlID="_txtDate" runat="server" EnabledOnClient=true Animated=true>
</ajaxToolkit:CalendarExtender>--%>
