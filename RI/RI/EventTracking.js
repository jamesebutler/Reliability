// JScript File
function onChangeEndDate(tbEnd, tbStart, hdStart, tbEnd2, hdEnd, hdnParentEndDate, lastHdEnd, hdnParentStartDate) {
    var parentStartDate = document.getElementById('_hdnStartDate').value;
    var parentEndDate = document.getElementById('_hdnEndDate').value;

    var passedEndDate = tbEnd.value;
    var newStartDate = tbStart.value;
    
    var passedDateParsed = Date.parse(passedEndDate);
    var parentStartDateParsed = Date.parse(parentStartDate);
    var parentEndDateParsed = Date.parse(parentEndDate);

    if (passedDateParsed > parentEndDateParsed || passedDateParsed < parentStartDateParsed) {
        tbEnd.value = hdnParentEndDate.value;

    }
    
    else {
        tbStart.value = passedEndDate;
        if (passedEndDate != hdnParentEndDate.value) {
            tbEnd2.value = hdnParentEndDate.value;
        }
        hdStart.value = passedEndDate;
        if (lastHdEnd != null) {
            lastHdEnd.value = hdnParentEndDate.value
        }
    }

}


function controlChanged(ddlControl, hdnControl) {
    hdnControl.value = ddlControl.value;
    }

function Hide() {
    $find("bePopup").hide();
}
function viewPopUp(url, msg, windowname) {
    if (url != null) {
        window.open(url, "Window1", "width=1200px,height=900px,resizable=yes,scrollbars=yes,top=1,left=1");
    }
}





function showDetail() {
    $find("bePopup").show();
    return false;
}

function IsValidNumber() {
    if ($(this).val() == "") {
        //$(this).val("0");
        alert("Please enter valid value!");
        $(this).focus();
    }
    else if ($.isNumeric($(this).val()) == false) {
        alert("Please enter valid value!");
        $(this).focus();
    }
}


//$('.date_picker').datetimepicker({
//    inline: true,
//    dateFormat: 'dd/mm/yy',
//    onSelect: function (selectedDate) {
//        var id = $(this).attr("id");
//        if (id == "txtStartdate") {
//            $("#txtEndDate").datetimepicker('option', 'minDate', selectedDate);
//        }
//        else {
//            $("#txtStartdate").datetimepicker('option', 'maxDate', selectedDate);
//        }
//    }
//});
//$("#txtEndDate").datetimepicker('option', 'minDate', new Date($.now());
//$("#txtStartdate").datetimepicker('option', 'maxDate', new Date($.now());

