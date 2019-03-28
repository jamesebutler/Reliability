

$(function () {
    $("#ctl00__cphMain__MOCType__rblType input").click(function (e) {
        if ($("#ctl00__cphMain__hfMOCType")[0].value == "Trial/Temporary")
            //$(this).next().text() != "Trial/Temporary") 
            {
            $("#ctl00__cphMain__divExpirationDate").hide();
        }
        else
            {
            $("#ctl00__cphMain__divExpirationDate").show();
            }
        if ($("input:radio[name='ctl00$_cphMain$_MOCType$_rblType']:checked").val() == "Permanent"
             && $("#ctl00__cphMain__hfMOCType")[0].value == "Trial/Temporary") {
            $find("BePopup2").show();
            return false;
        }
    else {
                return true;
    } 
    });

});

$(function () {
    $("#ctl00__cphMain__MOCType__rblType input").click(function (e) {
           if ($("input:radio[name='ctl00$_cphMain$_MOCType$_rblType']:checked").val() == "Permanent"
            && $("#ctl00__cphMain__hfMOCType")[0].value == "Trial/Temporary") {
                $find("BePopup2").show();
                return false;
            }
            else {
                return true;
            }
    })
});

$(document).ready(function () {

    if (divisionClient.value != 'NAIPG')
        $(".Category").each(function () 
        {
                $(".Category:contains('Market')").hide();
            })
        else
        {
            $(".Category:contains('Market')").show();
                }
 });



function ShowModalPopup() {

        if ($("input:radio[name='ctl00$_cphMain$_MOCType$_rblType']:checked").val() == "Permanent"
        && $("#ctl00__cphMain__hfMOCType")[0].value == "Trial/Temporary") {
        $find("BePopup2").show();
        return false;
    }
    else {
        return true;
    }
}
;


function HideModalPopup() {

    $find("BePopup2").hide();
    $("input:radio[name='ctl00$_cphMain$_MOCType$_rblType']:checked").val() == "Trial/Temporary";

    return true;

}

function HideApproverMP() {

    $find("bePopup").hide();

    return false;

}


function updateFL() {
	try{
		var site = facilityClient;	
//		alert('You have reached updateApproved site= {'+site.value+'} {'+busArea.value+'} {'+lineBreak.value+'} {'+currentUserName.value+'}');
		if (site!=null && busArea!=null && lineBreak !=null){
			ret = FunctionalLocationLookup.GetNotificationType(site.value,busArea.value,lineBreak.value,currentUserName, OnALComplete, OnALTimeOut, OnALError);
	}
		return true		 					 	
	}
	catch(err){

	}
}

function OnALTimeOut(arg) {
	alert("TimeOut OnALTimeOut");
}

function OnALError(arg) {
	        alert(error.get_message());
}

function OnALComplete(arg) {
	try{		
		if (arg.length>0){
			if (lblCommentsForEmail!=null){
				lblCommentsForEmail.style.display="";
			}
			if (txtForEmail!=null){
				txtForEmail.style.display="";
			}
		}
		else{
			if (lblCommentsForEmail!=null){
				lblCommentsForEmail.style.display="none";
			}
			if (txtForEmail!=null){
				txtForEmail.style.display="none";
			}
		}
				var modifiedText = arg.split('*');
				if (modifiedText != null){
				}
			
		}
	catch(err){
	}
}

function OnMTTComplete(arg) {
	try{
		if (arg.length>0){
    		if (location.hostname == 'localhost') {
	    	//alert(location.hostname);	
    		    win = viewPopUp('http://gpitasktracker.graphicpkg.com/Popups/TaskList.aspx?HeaderNumber=' + arg + '&RefSite=MOC&InFrame=false&AllowEdit=true&ShowHeaderInfo=false', 'YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES', 'MTT');
            }
            else if (location.hostname == 'ridev.ipaper.com' || location.hostname == 'ridev') {
                win = viewPopUp('http://gpitasktracker.graphicpkg.com/Popups/TaskList.aspx?HeaderNumber=' + arg + '&RefSite=MOC&InFrame=false&AllowEdit=true&ShowHeaderInfo=false', 'YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES', 'MTT');
	        }
            else if (location.hostname == 'ritest.ipaper.com' || location.hostname == 'ritest') {
                win = viewPopUp('http://gpitasktracker.graphicpkg.com/Popups/TaskList.aspx?HeaderNumber=' + arg + '&RefSite=MOC&InFrame=false&AllowEdit=true&ShowHeaderInfo=false', 'YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES', 'MTT');
	        }
	        else 
            {
                win = viewPopUp('http://gpitasktracker.graphicpkg.com/Popups/TaskList.aspx?HeaderNumber=' + arg + '&RefSite=MOC&InFrame=false&AllowEdit=true&ShowHeaderInfo=false', 'YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES', 'MTT');
	        }
		}}
	catch(err){
	}
}

//Force checkboxlist to act like radio button list where only one option can be checked
function CheckBoxToRadio(list,count){
        var obj = window.event.srcElement;
        var selectedId;
        if (obj.id==""){selectedId=obj.htmlFor;}
        else{selectedId=obj.id}
         if (list!=null){
            
            var cbl = document.getElementById (selectedId);
            if (cbl !=null){
                var curValue=cbl.checked;
            }
            var allList = document.getElementById(list.id+'_' + 0);
            for (var i=0;i<count;i++){
                var rbl = document.getElementById(list.id+'_' + i);                                  
                if (rbl != null){           
                    rbl.checked=false;
                }
            }   
            if (cbl !=null){
                cbl.checked=curValue;
            }
         }
            
    }

function viewPopUp(url,msg,windowname){
	if(url!=null){
		    window.open(url,"Window1","width=1200px,height=900px,resizable=yes,scrollbars=yes,top=1,left=1");
	}
} 
   
function updateDivision() {
    try {
        var site = facilityClient;
        divisionClient.text = site.value;
        divisionClient.value = site.value;
        //alert('You have reached updateApproved site= {' + site.value + '} {' + busArea.value + '} {' + lineBreak.value + '} {' + currentUserName.value + '}');
        ret = RIMOCSharedWS.GetSiteDivision(site.value, OnDivision, OnALTimeOut, OnALError);
        //if (site != null && busArea != null && lineBreak != null) {
          //  ret = FunctionalLocationLookup.GetNotificationType(site.value, busArea.value, lineBreak.value, currentUserName, OnALComplete, OnALTimeOut, OnALError);
        //}
        //divisionClient.text = ret;
        //divisionClient.value = ret;

        return true
    }
    catch (err) {

    }
}

function OnDivision(arg) {
    try {
        if (arg.length > 0) {
            divisionClient.text = arg;
            divisionClient.value = arg;
            if (divisionClient.value != 'NAIPG')
                $(".Category").each(function () {
                    $(".Category:contains('Market')").hide();
                })
            else {
                $(".Category:contains('Market')").show();
            }
        }
    }
    catch (err) {
    }
}

//function CreateTaskHeader(Title,MocNum,moc,StartDate,SiteId,BusUnit,Line,Desc,insertUser,StartDate){
function CreateTaskHeader(MocNum,type,activity,insertUser,createdDate){
    var SiteId = facilityClient.value;
    var title = titleClient.value;
    var startDateTxt = createdDate;
    var endDateTxt = createdDate;
    
    var BusUnit = busArea.value;
    var Line = lineBreak.value;
    var Desc = description.value;
    
    //ret = TaskTrackerHeader.CreateMTTTaskHeader(Title,MocNum,moc,StartDate,SiteId,BusUnit,Line,Desc,insertUser,StartDate, OnMTTComplete, OnALTimeOut, OnALError);
    ret = TaskTrackerHeader.CreateMTTTaskHeader(title,MocNum,'MOC',startDateTxt,endDateTxt,SiteId,BusUnit,Line,Desc,type,activity,insertUser,createdDate, OnMTTComplete, OnALTimeOut, OnALError);
}

function GetEmployeeBySite2 (ApproverSiteID, lbID) {
    var SiteID = document.getElementById(ApproverSiteID).value;
    //alert(SiteID);
    
    MOCApproversDDL=lbID;
    //ret = RIMOCSharedWS.GetEmployeeListBySite(SiteID, OnPopulateAvailApprover, OnSucceeded, OnALError);
    //RIMOCSharedWS.GetMTTResponsible(SiteID, OnPopulateAvailApprover, OnSucceeded,   OnALError);
    RIMOCSharedWS.GetMTTResponsible(SiteID, MOCApproversDDL, OnSucceeded, OnALError);
    }
    
var MOCApproversDDL=null;
function OnPopulateAvailApprover(EmployeeList){
    try{
        var obj;
            obj=MOCApproversDDL;
            obj= document.getElementById(obj);
            obj.length=0;
        if(obj!=null){
                for (var i=0; i < EmployeeList.length;++i){
                    var resource = EmployeeList[i].split('::');
                    addOption(obj, resource[1], resource[0]);
                }
                 return true;
              return false;
        }
   }   
   catch(e){}
}

function addOption(selectbox,text,value )
{
    if(text.length>0 && value.length>0){
        var optn = document.createElement("OPTION");
        optn.text = text;
        optn.value = value;
        selectbox.options.add(optn);
    }
}

function GetEmployee (EmployeeSiteID, lbEmployees) {
    var SiteID = document.getElementById(EmployeeSiteID).value;
    //alert(SiteID);
    
    EmployeeDDL=lbEmployees;

    //RIMOCSharedWS.GetMTTResponsible(SiteID, EmployeeDDL, OnSucceeded, OnALError);
    RIMOCSharedWS.GetDDLData2(SiteID, EmployeeDDL, OnSucceeded, OnALError);
    }
    
function  OnSucceeded (data) {
       var ddlUser;
       var i;
        if (data!==null){
            ddlUser = $get(data[0].Text);//(_ddlResponsibleUser);
        }
        
        
        if (ddlUser !== null && data.length>0) {        
            //ResponsibleUser.ClearOptionsFastAlt(ddlUser.id); // 
            ddlUser.options.length=0;
            for (i = 1; i < data.length; ++i) {
                addOption3(ddlUser, data[i]);
            }
            ddlUser.selectedIndex = 0;
            ddlUser.focus();
        }
        return data;
    }
    

function addOption3 (selectbox, datavalue) {
        var data = datavalue;
        var optn;
        if (data.Text.length > 0 && data.Value.length > 0) {
            optn = document.createElement("OPTION");
            optn.text = data.Text;
            optn.value = data.Value;
            //optn.title = data.Text;
            if (data.Attributes.Keys.title !== null)
            {
            var title = data.Attributes.Keys.title;
            //alert(title);
            optn.title = title;
            }
            if (data.Attributes.CssStyle.Value !== null) {
                var resource = data.Attributes.CssStyle.Value.split(';');
                if (resource !== null) {
                    for (i = 0; i < resource.length; i++) {
                        if (resource[i] !== null) {
                            var attr = resource[i].split(':');
                        }
                    }
                }
            }
            selectbox.options.add(optn);
        }
        else if (data.Value.length > 0) {

            optn = document.createElement("OPTION");
            optn.text = data.Text;
            optn.value = data.Value;
            //optn.disabled = true;
            selectbox.options.add(optn);
        }
}
   
function HideSystemDetail(sender, ControlID)    
    {
     //alert(document.getElementById(ControlID));
     var system = document.getElementById(sender);
     //alert(system);
     document.getElementById(ControlID).style.display="none";
    }

function ShowSystemDetail(sender, ControlID, FacilityID, DefaultFacilityDDL, DefaultFacility)    
    {        
    
    copyFacilityList(DefaultFacilityDDL,FacilityID,DefaultFacility);
    
    EmployeeDDL=sender;
    GetEmployee (FacilityID, EmployeeDDL);
    
    document.getElementById(ControlID).style.display="block";
    //ValidatorEnable(document.getElementById(validatorId), false); 
    }

function copyFacilityList(src,dest,defaultFacility){
    try{
        if (src!=null && dest!=null){
            //if (dest.length<=2){
                src = document.getElementById(src);
                dest = document.getElementById(dest);
                for (var i=0; i< src.length; ++i){
                     addOption(dest, src.options[i].text, src.options[i].value);  
                }
                dest.value=defaultFacility;
                //dest.onblur=function(){this.selectedIndex=5;this.onblur=null;};
                document.body.focus();
            //}
        }
    }
    catch(e){
    
    }
}

function GetCustomers() {
    var SiteID = document.getElementById(EmployeeSiteID).value;
    //alert(SiteID);

    EmployeeDDL = lbEmployees;

    //RIMOCSharedWS.GetMTTResponsible(SiteID, EmployeeDDL, OnSucceeded, OnALError);
    RIMOCSharedWS.GetCustomers(OnSucceeded, OnALError);
}
 


 //function hideshow(v) {
 //           if (document.getElementById(v).style.display == "block") {
 //               document.getElementById(v).style.display = "none";
 //           }
 //           else if (document.getElementById(v).style.display == "none") {
 //           document.getElementById(v).style.display = "block";
 //           }
 //        }

 function ValidateDaysAfter(source, args) 
    {
        args.IsValid = true; 
        var chkBox = document.getElementById(source.chkId);
        var txtBox = document.getElementById(source.txtDaysAfter);
    
        var chkListinputs = chkBox.getElementsByTagName("input");

        for (var i=0;i<chkListinputs .length;i++)   
        {     
            if (chkListinputs [i].checked)     
            {
                var elementArray = chkBox.getElementsByTagName('label');
                for (var j=0; j<elementArray.length; j++)  //Loop thru checkboxlist items
                {
                    var currentElementRef = elementArray[i];
                    //alert('#' + i + ': ' + currentElementRef.innerHTML);
                    if (currentElementRef.innerHTML == "Yes") //User has selected Yes for System
                    {
                        if (txtBox.value.length > 0)
                            {
                                args.IsValid = true; 
                            }
                        else 
                            { 
                            args.IsValid = false; 
                            return;
                            }
                    }
                }
            }
        }
    }
    
    function ValidatePerson(source, args) 
    {
        var chkBox2 = document.getElementById(source.chkId);
        //var txtBox2 = document.getElementById(source.txtDaysAfter.value);
        var ddlPerson2 = document.getElementById(source.ddlPerson);
    
        var chkListinputs = chkBox2.getElementsByTagName("input");

        for (var i=0;i<chkListinputs .length;i++)   
        {     
            if (chkListinputs [i].checked)     
            {
                var elementArray = chkBox2.getElementsByTagName('label');
                for (var j=0; j<elementArray.length; j++)  //Loop thru checkboxlist items
                {
                    var currentElementRef = elementArray[i];
                    //alert('#' + i + ': ' + currentElementRef.innerHTML);
                    if (currentElementRef.innerHTML == "Yes") //User has selected Yes for System
                    {
                           //     alert(ddlPerson2.selectedIndex);
                        if (ddlPerson2.value != "-1") // && ddlPerson2.selectedIndex != 0)
                                {
                                args.IsValid = true; 
                                return;
                                }
                                else 
                                { 
                                args.IsValid = false; 
                                return;
                                } 
                            }
                        else 
                            { 
                            args.IsValid = true; 
                            return;
                            }
                    }
                }
            }
        }


function GetApprover (peopleID, hfID) {
    var peopleID2 = document.getElementById(peopleID).value;
    var hfID2 = document.getElementById(hfID);
    hfID2.value = peopleID2;
    //alert(hfID2.value);
    
    }

function GetEmployeeWRoles (EmployeeSiteID, lbEmployees) {
    var SiteID = document.getElementById(EmployeeSiteID).value;
    //alert(SiteID);
    
    EmployeeDDL=lbEmployees;

    //RIMOCSharedWS.GetMTTResponsible(SiteID, EmployeeDDL, OnSucceeded, OnALError);
    RIMOCSharedWS.GetMTTResponsible(SiteID, EmployeeDDL, OnSucceeded, OnALError);
    }

function enableField(sender,parent,child) {
    //alert(sender);
    var daysAfterTextBox = document.getElementById(sender);
    var calendarImage = document.getElementById(parent);
    var calendarDate = document.getElementById(child);
    if (daysAfterTextBox.value.length > 0) {
        calendarImage.disabled = true;
        calendarDate.disabled = true;
        daysAfterTextBox.disabled = false;
        }
    else 
    if (calendarDate.value.length > 0) {
        calendarImage.disabled = false;
        calendarDate.disabled = false;
        daysAfterTextBox.disabled = true;
        }
    else {
        calendarImage.disabled = false;
        calendarDate.disabled = false;
        daysAfterTextBox.disabled = true;
        daysAfterTextBox.value = "";
    }
   
}


function ValidateMarketChannel(source, args) {

    if (divisionClient.value == 'NAIPG') {

        var checkboxes = $("input[type=checkbox]");

        var message = "";

        checkboxes.each(function () {

            if ($(this).is(":checked")) {

                var label = $(this).closest("td").find("label").eq(0);

                message += "Value: " + $(this).val() +

                    " Text: " + label.html() + "\n";

            }

        });

        var checkboxes2 = $("[id*=SubCategory] option:selected");

        var message2 = "";

        checkboxes2.each(function () {

            if ($(this).is(":selected")) {

                var label = $(this).closest("td").find("option").eq(0);

                message2 += "Value: " + $(this).val() +

                    " Text: " + label.html() + "\n";

            }

        });


        var pattern = new RegExp("Finished Product Trial");

        if (pattern.test(message))
            {

            if (message2.match( /Trades|NAP|Domestic|Export/g)) // var pattern2 = new RegExp("(Trades|NAP|Domestic|Export)");

                {

                    args.IsValid = true;
                    return;

                }

            else

                {
                    args.IsValid = false;
                    $find("MarketChannel").show();
                    return;

            }

        }
    }
};


function HideMCModalPopup() {

    $find("MarketChannel").hide();

    return false;

};


