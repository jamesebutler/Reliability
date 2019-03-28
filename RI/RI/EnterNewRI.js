// JScript File
function CalculateClass(_cost,_life,_planned,_repair,_chronic,_display,_totalClass,_avgClass,_failureClass){               
    try{
        var i=0;
        var totalCls = 0;
        var avgCls = 0;
        for (i=0;i<5;i++){
            var cost = document.getElementById(_cost + "_"+i);
            var life = document.getElementById(_life + "_"+i);
            var planned = document.getElementById(_planned + "_"+i);
            var repair = document.getElementById(_repair + "_"+i);
            var chronic = document.getElementById(_chronic + "_"+i);
            
            if (cost!=null && life!=null && planned!=null && repair !=null && chronic !=null){
                if (cost.checked==true) totalCls+=i+1
                if (life.checked==true) totalCls+=i+1
                if (repair.checked==true) totalCls+=i+1
                if (chronic.checked==true) totalCls+=i+1
                if (planned.checked==true) totalCls+=i+1
            }       
        }        
        avgCls = Math.round(totalCls/5)-1;
        var display = document.getElementById(_display + "_"+avgCls);
        if (display!=null)display.checked=true;
        var totalClass = document.getElementById(_totalClass);
        if (totalClass!=null)totalClass.value=totalCls;
        var avgClass = document.getElementById(_avgClass);
        if (avgClass!=null) avgClass.value=totalCls/5;
        var failureClass = document.getElementById (_failureClass);
        if (failureClass!=null) failureClass.value =totalCls/5;   
    }
    catch(err){
    
    }                           
    }
 
 
 function OnSecurityTimeOut(arg) {
	alert("TimeOut OnSecurityTimeOut");
}

function OnSecurityError(arg) {
	alert("Error encountered OnSecurityError");
}

function OnALTimeOut(arg) {
	alert("TimeOut OnALTimeOut");
}

function OnALError(arg) {
	alert("Error OnALError");
}

function OnTimeOut(arg) {
alert("TimeOut encountered when calling Functional Location.");
}

function OnError(arg) {
alert("Error encountered when calling Functional Location.");
}
function updateSecurity(){
		try{
			var site = facilityClient
			if (site!=null){
				ret = FunctionalLocationLookup.GetDeleteAccess(site.value,currentUserName, OnSecurityComplete, OnSecurityTimeOut, OnSecurityError);
			}
			return(true);
		}
		catch(err){
	
		}
	}
	

function OnSecurityComplete(arg) {
	try{	
		var site = facilityClient;
		if (facilitiesAllowedToUpdate == 'AL' || facilitiesAllowedToUpdate == 'TY' || facilitiesAllowedToUpdate == site.value) {
			if (btnSubmit!=null){
				btnSubmit.style.display="";
			}
			if (btnSpell!=null){
				btnSpell.style.display="";
			}
		}
		else{
			if (btnSubmit!=null){
				btnSubmit.style.display="none";
			}
			if (btnSpell!=null){
				btnSpell.style.display="none";
			}
		}
		if (arg>=3){//user can delete
			if (btnDelete!=null){		
				btnDelete.style.display="";
			}	
		}
		else{ //user can not delete
			if (btnDelete!=null){		
				btnDelete.style.display="none";
			}	
		} 
		//refreshTreeView();
	}
	catch(err){

	}
}
function onChangeAnalysisLeader(ddl){
	try{
		if (ddl!=null){
			if (ddl.value.length>0){
				//if (lastAnalysisLead.length==0){lastAnalysisLead=ddl.value}
				if (lastAnalysisLead!=ddl.value){
					if (imgTextForEmail!=null){
						imgTextForEmail.click();
					}
					lastAnalysisLead=ddl.value;
				}
				
			}
			if (txtAnalysisLead!=null){				
				var tmp = ddl.options[ddl.selectedIndex].text.split(',');
				if (tmp.length>1){
					txtAnalysisLead.value=tmp[1].trim() + ' ' + tmp[0].trim();
				}				
			}
		}
	}
	catch(err){
	
	}
	return true
}

function updateAnalysisLeader() {
	try{
		var site = facilityClient;	
		if (site!=null && busArea!=null && lineBreak !=null){
			ret = FunctionalLocationLookup.GetNotificationType(site.value,busArea.value,lineBreak.value,currentUserName, OnALComplete, OnALTimeOut, OnALError);
		}
		return true		 					 	
	}
	catch(err){

	}
}

function OnALComplete(arg) {
	try{		
		if (arg.length>0){
			if (ddlAnalysisLead!=null){
				ddlAnalysisLead.disabled=false;
				ddlAnalysisLead.style.display="";
			}
			if (txtAnalysisLead!=null){
				txtAnalysisLead.style.display="none";
			}
			if (lblCommentsForEmail!=null){
				lblCommentsForEmail.style.display="";
			}
			if (txtForEmail!=null){
				txtForEmail.style.display="";
			}
		}
		else{
			if (ddlAnalysisLead!=null){
				ddlAnalysisLead.disabled=false;
				ddlAnalysisLead.style.display="none";
			}
			if (txtAnalysisLead!=null){
				txtAnalysisLead.style.display="";
			}
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

function onCompleteCalculateDowntime(arg){
    downtime.value=arg;
    updateSRRDowntime(arg);
}
function calculateDowntime() {

    ret = CalculateDowntime.Calculate(startDate.value + " " + startHrs.value+":"+startMins.value+":00",endDate.value + " " + endHrs.value+":"+endMins.value+":00", onCompleteCalculateDowntime, OnALTimeOut, OnALError);

	/*date1 = new Date();
	date2 = new Date();
	diff  = new Date();

    //date1temp = new Date(startDate.
	date1temp = new Date(startDate.value + " " + startHrs.value+":"+startMins.value+":00");
	date1.setTime(date1temp.getTime());

	date2temp = new Date(endDate.value + " " + endHrs.value+":"+endMins.value+":00");
	date2.setTime(date2temp.getTime());

	// sets difference date to difference of first date and second date
    if (date1>date2){
		alert('EndDate {'+date2+'} should be greater than StartDate {'+date1+'}');
		downtime.value='';
		return false;
    }
	diff.setTime(Math.abs(date1.getTime() - date2.getTime()));

	timediff = diff.getTime();

	//weeks = Math.floor(timediff / (1000 * 60 * 60 * 24 * 7));
	//timediff -= weeks * (1000 * 60 * 60 * 24 * 7);

	//days = Math.floor(timediff / (1000 * 60 * 60 * 24)); 
	//timediff -= days * (1000 * 60 * 60 * 24);

	hours = Math.floor(timediff / (1000 * 60 * 60)); 
	timediff -= hours * (1000 * 60 * 60);

	//mins = Math.floor(timediff / (1000 * 60)); 
	//timediff -= mins * (1000 * 60);
	//mins = mins/60;
	//secs = Math.floor(timediff / 1000); 
	//timediff -= secs * 1000;
	mins = (timediff / (1000 * 60))/60;
	mins = roundNumber(mins,2);
	if (mins>0){
		//hours = hours + "." + mins;//Math.round(mins*100,2);
		hours = hours+mins; 
	}
	
	
	var difference = parseFloat(hours);
	downtime.value = roundNumber(difference,2);*/
	return false; // form should never submit, returns false
}

function roundNumber(num, dec) {
	var result = Math.round(num*Math.pow(10,dec))/Math.pow(10,dec);
	return result;
}

function updateFunctionalLocation() {
	try{ 
		var site = facilityClient;	
		if (site!=null && busArea!=null && lineBreak!=null){
			ret = FunctionalLocationLookup.PopulateFunctionalLocation(site.value,busArea.value,lineBreak.value, OnComplete, OnTimeOut, OnError);				
			var bus='';
			var area='';
			var line='';
			if (site.value=='') {
				if (ECBtnTree!=null){
					ECBtnTree.disabled=true;
				}											
			}
			else{
				if (ECBtnTree!=null){
					ECBtnTree.disabled=false;
				}
			}
			if (busArea.value.length>0){
				var tmp = busArea.value.split(' - ');
				bus = tmp[0];
				area = tmp[1];
			}
			if (lineBreak.value.length>0){
				var tmp2 = lineBreak.value.split(' - ');
				line = tmp2[0];
			}

			SetFunctionalLocationContext(site.value+'@@'+bus+'@@'+area+'@@'+line);
			return(true);
			}
		}
	catch(err){

	}
}

function OnComplete(arg) {
	try{	
		var modifiedText = arg.split('*');    
		if (modifiedText != null && txtAutoComplete!=null && txtAutoComplete2!=null && ddlCriticality!=null ){
			if (modifiedText.length>=2){
                txtAutoComplete.value = modifiedText[0].trim();
                txtAutoComplete2.innerHTML  = modifiedText[1].trim();
                if (ECEquipmentid!=null){
						ECEquipmentid.value=modifiedText[0].trim();
					}
				ddlCriticality.value=modifiedText[2].trim();
            }
            else{
				txtAutoComplete.value = '';
                txtAutoComplete2.innerHTML  = '';
                if (ECEquipmentid!=null){
					ECEquipmentid.value='';
				}
				ddlCriticality.selectedValue=0;
            }
		}
	}
	catch(err){
	
	}
}

function updateConstrainedArea() {
	try{ 
		var site = facilityClient;	
		if (site!=null && busArea!=null && lineBreak!=null){
			ret = FunctionalLocationLookup.PopulateConstrainedArea(site.value,busArea.value,lineBreak.value, OnCompleteConstrainedArea, OnTimeOut, OnError);				
			
			return(true);
			}
		}
	catch(err){

	}
}



function OnCompleteConstrainedArea(value) {
	try{	
		
    if (rblConstrainedAreas!=null){
        for (i=0;i<2;i++){
            var ca = document.getElementById(rblConstrainedAreas.id + "_"+i);
            if (ca!=null)
            {if (value=="Constrained Area")
                ca.checked=true 
             else 
                ca.checked=false }
        }
        SetClassificationConstrainedArea(GetConstrainedArea());
     }   
	}
	catch(err){
	
	}
}

function calculateSRR() {
	try{	
	
    updateSRRDowntime(downtime.value);
    updateSRRFINCL(finclimpact.value);
		
//    if (rblConstrainedAreas!=null){
//        for (i=0;i<2;i++){
//            var ca = document.getElementById(rblConstrainedAreas.id + "_"+i);
//            if (ca!=null)
//            {if (value=="Constrained Area")
//                ca.checked=true 
//             else 
//                ca.checked=false }
//        }
//        SetClassificationConstrainedArea(GetConstrainedArea());
//     }   
	}
	catch(err){
	
	}
}

function updateSRRFINCL(value){
    try{
    
    var recordable=GetRecordable();
    var txt1="Financial Impact >= $100000"
    var txt2="Financial Impact >= $250000"
    if (value>=100000 && recordable=="Yes")   
      {if (cblSRR!=null){
        for (i=0;i<3;i++){
            var ca = document.getElementById(cblSRR.id + "_"+i);
            if (ca!=null){if(ca.parentElement.outerText==txt1) ca.checked=true }                  
        }
      }   
     }
     else
     { if (cblSRR!=null){
        for (i=0;i<3;i++){
            var ca = document.getElementById(cblSRR.id + "_"+i);
            if (ca!=null){if(ca.parentElement.outerText==txt1) ca.checked=false}
          }
       }   
      }
     if (value>=250000 && recordable=="Yes")   
      {if (cblSRR!=null){
        for (i=0;i<3;i++){
            var ca = document.getElementById(cblSRR.id + "_"+i);
            if (ca!=null){if(ca.parentElement.outerText==txt2) ca.checked=true }                  
        }
      }   
     }
     else
     { if (cblSRR!=null){
        for (i=0;i<3;i++){
            var ca = document.getElementById(cblSRR.id + "_"+i);
            if (ca!=null){if(ca.parentElement.outerText==txt2) ca.checked=false}
          }
       }   
      }
       
     }
      catch(err){
	
	}
}

function updateSRRDowntime(dt){
try{

    var recordable=GetRecordable();
    var txt="Any Process DT >= 16 Hr"
    var dt2 = dt.replace(",",".")
    if (dt2>=16  && recordable=="Yes")   
        {if (cblSRR!=null){
            for (i=0;i<3;i++){
            var ca = document.getElementById(cblSRR.id + "_"+i);
            if (ca!=null){if(ca.parentElement.outerText==txt) ca.checked=true }
            }
           }
         }
     else 
       {if (cblSRR!=null){
            for (i=0;i<3;i++){
            var ca = document.getElementById(cblSRR.id + "_"+i);
            if (ca!=null){if(ca.parentElement.outerText==txt) ca.checked=false}
        }
      }
    } 
   }
    catch(err){
	
	}  
}


// Notify ScriptManager that this is the end of the script.
//if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();

 function displayViewListWindow(url,title){
	if (url!=null){
			var win=dhtmlmodal.open("viewlist", "iframe", url, title, "width=850px,height=350px,resize=1,scrolling=1,center=1", "recal");		
	}
} 

function viewPopUp(url,msg,windowname){
	if(url!=null){
	var actionJS = "var win=window.open('" +url+"','"+windowname+"','width=1200px,height=1000px,resizable=yes,scrollbars=yes,top=1,left=1'); if (win!=null) try {win.focus();} catch (err) {}";
	//var actionJS = "window.open('" +url+"','"+windowname+"','width=1200px,height=1000px,resizable=yes,scrollbars=yes,top=1,left=1');";
		//ConfirmBeforeLeave=true;
		confirmMessage(msg,'ctl00__ConfirmMessage', actionJS);
		//if (confirm("You have attempted to leave this page.  If you have made any changes to the fields without clicking the Save button, your changes will be lost.  Are you sure you want to exit this page?")){
		   // parent.window.location = url;
		    //window.open(url,"Window1","width=1200px,height=1000px,resizable=yes,scrollbars=yes,top=1,left=1");
		//}
	}
} 

function updateItemCounts(){
	try{
	$get('_btnRefresh').click();
	}
	catch(ex){
		alert('Error Refreshing the page'+ex);
	}
}

function GetClassificationCriticality(){
    if (ddlCriticality!=null){
        var value=0;
        value = ddlCriticality.options[ddlCriticality.selectedIndex].value;        
     }   
    return value;
}
function GetConstrainedArea(){
    if (rblConstrainedAreas!=null){
        var value="";
        
        for (i=0;i<2;i++){
            var ca = document.getElementById(rblConstrainedAreas.id + "_"+i);
            if (ca!=null){if(ca.checked==true)value+="Constrained Area" } //ca.value}
        }
     }   
     return value
}
function GetRecordable(){
    if (rblRecordable!=null){
        var value="";
        for (i=0;i<2;i++){
            var ca = document.getElementById(rblRecordable.id + "_"+i);
            if (ca!=null){if(ca.checked==true)value+=ca.value } //ca.value
            }
     }   
     return value
}
function SetCriticality(value){
    if (ddlCriticality!=null){
        for (i=0;i<ddlCriticality.options.length;i++){
            if (ddlCriticality.options[i].value == value){
                ddlCriticality.options[i].selected=true;
            }
        }
     }   
}
function SetConstrainedArea(value){
    if (value==5) value="Yes"
    else value="No"
    if (rblConstrainedAreas!=null){
        for (i=0;i<2;i++){
            var ca = document.getElementById(rblConstrainedAreas.id + "_"+i);
            if (ca!=null){if(ca.value==value)ca.checked=true}
        }
     }   
}
function CreateTaskHeader(RINum,insertUser,createdDate){
    var SiteId = facilityClient.value;
    var title = titleClient.value;
    var startDateTxt = startDate.value;
    var endDateTxt = endDate.value;
    var BusUnit = busArea.value;
    var Line = lineBreak.value;
    var Desc = description.value;
    
    //ret = TaskTrackerHeader.CreateMTTTaskHeader(Title,MocNum,moc,StartDate,SiteId,BusUnit,Line,Desc,insertUser,StartDate, OnMTTComplete, OnALTimeOut, OnALError);
    ret = TaskTrackerHeader.CreateMTTTaskHeader(title,RINum,'RELIABILITY INCIDENT',startDateTxt,endDateTxt,SiteId,BusUnit,Line,Desc,'RELIABILITY','INCIDENT',insertUser,createdDate, OnMTTComplete, OnALTimeOut, OnALError);
}
function OnMTTComplete(arg) {
	try{
		if (arg.length>0){
    		if (location.hostname == 'localhost') {
	    	//alert(location.hostname);	
            //win = viewPopUp('http://ridev/TaskTracker/TaskDetails.aspx?HeaderNumber=' + arg + '&TaskNumber=-1&RefSite=MOC','YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES' ,'MTT');
    		    win = viewPopUp('http://gpitasktracker.graphicpkg.com/Popups/TaskList.aspx?HeaderNumber=' + arg + '&RefSite=RI&InFrame=false&AllowEdit=true&ShowHeaderInfo=false', 'YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES', 'MTT');
            }
            else if (location.hostname == 'ridev' || location.hostname=='ridev.ipaper.com' ) {
                win = viewPopUp('http://gpitasktracker.graphicpkg.com/Popups/TaskList.aspx?HeaderNumber=' + arg + '&RefSite=RI&InFrame=false&AllowEdit=true&ShowHeaderInfo=false', 'YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES', 'MTT');
	        }
	        else if (location.hostname == 'ritest' || location.hostname=='ritest.ipaper.com') {
	            win = viewPopUp('http://gpitasktracker.graphicpkg.com/Popups/TaskList.aspx?HeaderNumber=' + arg + '&RefSite=RI&InFrame=false&AllowEdit=true&ShowHeaderInfo=false', 'YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES', 'MTT');
	        }
	        else 
            {
	            win = viewPopUp('http://gpitasktracker.graphicpkg.com/Popups/TaskList.aspx?HeaderNumber=' + arg + '&RefSite=RI&InFrame=false&AllowEdit=true&ShowHeaderInfo=false', 'YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES', 'MTT');
	        }
		}}
	catch(err){
	}
}