<%@ Control EnableTheming="false" Language="VB" AutoEventWireup="false" CodeFile="ucStartEndDates.ascx.vb" Inherits="RI_User_Controls_Common_ucStartEndDates" %>
<script>
    $("#_tbFromDate").datetimepicker({
    onChangeDateTime: function(dp, $input){
        var dt2 = $('#_tbEndDate');
        var minDate = $('#_tbFromDate').val();
        var currentDate = new Date();
        var targetDate = moment(minDate).toDate();
        var dateDifference = currentDate - targetDate;
        var minLimitDate = moment(dateDifference).format('YYYY/MM/DD');
        var endDate = moment(dt2.val()).toDate();
        if((currentDate - endDate) >= (currentDate - targetDate))
            dt2.datetimepicker({
                'value': minDate
            });
        dt2.datetimepicker({
            'minDate': '-'+minLimitDate
        });
    }
});
    $("#_tbEndDate").datetimepicker({
        format: 'DD MMM YYYY hh:mm A'
});

    </script>

<table border="0" cellpadding="0" cellspacing="0" style="text-align: left" width="40%">
    <tr>
        <td width="10%">
	        <asp:Label ID="_lbStartDate" runat="server" Text="<%$RIResources:Shared,Start%>" ></asp:Label><br />  
            <asp:TextBox ID="_tbFromDate" runat="server" cssclass="PerformanceDateTimePicker" ClientIDMode="Static"></asp:TextBox>
        </td>
        <td width="10%"> 
            <asp:Label ID="_lbEndDate" runat="server" Text="<%$RIResources:Shared,End%>" ></asp:Label><br />  
            <asp:TextBox ID="_tbToDate" runat="server" ClientIDMode="Static"></asp:TextBox>
        </td>
         
        <td width="10%"> 
            <asp:Label ID="_lbRange" runat="server" Text="<%$RIResources:Shared,DateRange%>" ></asp:Label><br />  
                <asp:DropDownList AutoPostBack="false" ID="_ddlDateRange" runat="server">
                </asp:DropDownList>
        </td>
       
    </tr>
</table>
