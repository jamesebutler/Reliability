// JScript File
var showTemplateDDL = "false";

function EnableOutageCoordValidator(state)

        {
            //alert("Please select Coordinator");
            var checkbox = document.getElementById('ctl00__cphMain__rfvCoord');
            //var ddlLead = document.getElementById('ctl00__cphMain__ddlMRLead')
            ValidatorEnable(checkbox, state);
		    //document.getElementById('ctl00__cphMain__AssessDate__txtDate').enabled=false;
 //           if(document.getElementById('ctl00__cphMain__cbAnnualOutage').checked)         
 //               {document.getElementById('ctl00__cphMain__test').disabled = false;
//                     
 //               ddlLead.disabled = false;     
 //               document.getElementById('ctl00__cphMain__AssessDate__imgCalendar').disabled = false;     }
//            else         
//                {document.getElementById('ctl00__cphMain__test').disabled = true;     
//                
//                document.getElementById('ctl00__cphMain__AssessDate__imgCalendar').disabled = true;     }
        }

function onCompleteCalculateDowntime(arg)
{
    downtime.value=arg;
}

function OnCalculateDowntimeTimeOut(arg){alert('OnCalculateDowntimeTimeOut Error' + arg);}
function OnCalculateDowntimeError(arg){alert('OnCalculateDowntimeError Error' + arg);}

function calculateDowntime() 
{
    ret = CalculateDowntime.Calculate(startDate.value + " " + startHrs.value+":"+startMins.value+":00",endDate.value + " " + endHrs.value+":"+endMins.value+":00", onCompleteCalculateDowntime, OnCalculateDowntimeTimeOut, OnCalculateDowntimeError);
	return false; // form should never submit, returns false
}


function calculateActualDowntime() 
{
    ret = CalculateDowntime.Calculate(ActualStartDate.value + " " + ActualStartHrs.value+":"+ActualStartMins.value+":00",ActualEndDate.value + " " + ActualEndHrs.value+":"+ActualEndMins.value+":00", onCompleteCalculateActualDowntime, OnCalculateDowntimeTimeOut, OnCalculateDowntimeError);
	return false; // form should never submit, returns false
}

function onCompleteCalculateActualDowntime(arg)
{
    ActualDowntime.value=arg;
}


function roundNumber(num, dec) 
{
	var result = Math.round(num*Math.pow(10,dec))/Math.pow(10,dec);
	return result;
}

function showArea(rad)    
{        
    var list = document.getElementById(rad);
    var options = list.getElementsByTagName("input");
    for( x = 0; x < options.length; ++x )
    {
        if( options[x].type == "radio" && options[x].checked )
        {
            if( options[x].value == "Field Day" || options[x].value == "Partial Mill" )
            {  
                businessUnit.style.display="block";
                businessUnitArea.style.display="block";
                //document.getElementById("ctl00__cphMain__tcBusinessUnitArea").style.display="block";
                //document.getElementById("ctl00__cphMain__tcBusinessUnit").style.display="block";
            }
            else if (businessUnit!=null)
            {
                businessUnit.style.display="none";
                businessUnitArea.style.display="none";
                //document.getElementById("ctl00__cphMain__tcBusinessUnitArea").style.display="none";
                //document.getElementById("ctl00__cphMain__tcBusinessUnit").style.display="none";
            }
        break;
        }
    }
}

function updateBusUnitAreaLine() {
	try{
		var site = facility;	
		if (site!=null){
			ret = OutageCascadingLists.PopulateBusinessUnitAreaLine(site.value,busArea.value,lineBreak.value, OnComplete, OnTimeOut, OnError);				
			var bus='';
			var area='';
			var line='';
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

	}}
	
function OnMTTComplete(arg) {
      try{
            if (arg.length>0){
            if (location.hostname == 'localhost') {
            //alert(location.hostname);   
            //win = viewPopUp('http://ridev /TaskTracker/TaskDetails.aspx?HeaderNumber=' + arg + '&TaskNumber=-1&RefSite=MOC','YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES' ,'MTT');
                win = viewPopUp('http://gpitasktracker.graphicpkg.com/Popups/TaskList.aspx?HeaderNumber=' + arg + '&RefSite=Outage&InFrame=false&AllowEdit=true&ShowHeaderInfo=false', 'YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES', 'MTT');
            }
            else if (location.hostname == 'ridev' || location.hostname=='ridev.ipaper.com') {
                win = viewPopUp('http://gpitasktracker.graphicpkg.com/Popups/TaskList.aspx?HeaderNumber=' + arg + '&RefSite=Outage&InFrame=false&AllowEdit=true&ShowHeaderInfo=false', 'YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES', 'MTT');
              }
              else if (location.hostname == 'ritest' || location.hostname=='ritest.ipaper.com') {
                  win = viewPopUp('http://gpitasktracker.graphicpkg.com/Popups/TaskList.aspx?HeaderNumber=' + arg + '&RefSite=Outage&InFrame=false&AllowEdit=true&ShowHeaderInfo=false', 'YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES', 'MTT');
              }
              else 
            {
                  win = viewPopUp('http://gpitasktracker.graphicpkg.com/Popups/TaskList.aspx?HeaderNumber=' + arg + '&RefSite=Outage&InFrame=false&AllowEdit=true&ShowHeaderInfo=false', 'YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES', 'MTT');
              }
            }}
      catch(err){
      }
}

function OnALTimeOut(arg) {
	alert("TimeOut OnALTimeOut");
}

function OnALError(arg) {
	alert("Error OnALError");
}

function CheckBoxToRadio(list,count)
{
    var obj = window.event.srcElement;
    var selectedId;
    if (obj.id=="")
    {
        selectedId=obj.htmlFor;
    }
    else
    {
        selectedId=obj.id
    }
    if (list!=null)
    {
        var cbl = document.getElementById (selectedId);
        if (cbl !=null)
        {
            var curValue=cbl.checked;
        }
        
        var allList = document.getElementById(list.id+'_' + 0);
        for (var i=0;i<count;i++)
        {
            var rbl = document.getElementById(list.id+'_' + i);                                  
            if (rbl != null)
            {           
                rbl.checked=false;
            }
        }   
            if (cbl !=null)
            {
                cbl.checked=curValue;
            }
    }
}

function viewPopUp(url,msg,windowname)
{
	if(url!=null)
	{
		    window.open(url,"Window1","width=1600px,height=1100px,resizable=yes,scrollbars=yes,top=0,left=0");
		 //   window.open(url,"Window1","fullscreen=yes,toolbar=yes,resizable=yes,scrollbars=yes,top=100,left=140");
	}
} 

//function CreateTaskHeader(Title,MocNum,moc,StartDate,SiteId,BusUnit,Line,Desc,insertUser,StartDate){
function CreateTaskHeader(title,OutageNum,startDate,endDate,siteId,type,activity,insertUser,createdDate)
{
    var Desc = description.value;

    ret = TaskTrackerHeader.CreateMTTTaskHeader(title,OutageNum,'Outage',startDate,endDate,siteId,'','',Desc,type,activity,insertUser,createdDate, OnMTTComplete, OnALTimeOut, OnALError);
}

function ChangeImage(ddlId, imgId)
        {
             var ddlControlName = document.getElementById(ddlId.id);
            
             if(ddlControlName.value == "C")  //it depends on which value Selection do u want to hide or show your textbox 
             {
                 document.getElementById(imgId.id).src = '../Images/bullet_red.png';
               
             }
             else if (ddlControlName.value == "I")
             {
                 document.getElementById(imgId.id).src = '../Images/bullet_yellow.png';
                
             }
             else if (ddlControlName.value == "N")
             {
                 document.getElementById(imgId.id).src = '../Images/bullet_green.png';
                
             }
        } 

function ConflictFieldChanged()
        {
            
            //var lblControlName = lblConflictChanged);
            
            lblConflictChanged.value = "Y";
            return false;
        }

//function mttRoles(){
    //var Desc = description.value;

  //  ret = TaskTrackerHeader.getMTTPeople('0627', OnMTTComplete, OnALTimeOut, OnALError);
//}

function EnableButtons(btnid,ddlid) {
          var button1 = document.getElementById(btnid);
          var ddl = document.getElementById(ddlid);

          if (ddl.value == "")
          {
          button1.style.display="none";
          }
          else
          {
          button1.style.display="";
          }
      }


function CheckRequired(txtid,ddlid)    
{        
    var title = document.getElementById(txtid);
    var ddl = document.getElementById(ddlid);

    if (title.value == "")
    {
    showTemplateDDL = "false";
    }
   
    if (showTemplateDDL == "true")
    {
        ddl.disabled = false;
    }
    else
    {
    showTemplateDDL = "true";
    }

}

function checkOutageType(rad,txtTitle,ddlCoord,ddl1)    
{        
    var list = document.getElementById(rad);
    var tb = document.getElementById(txtTitle);
    var ddlCoord = document.getElementById(ddlCoord);
    var ddl = document.getElementById(ddl1);
    var options = list.getElementsByTagName("input");
    for( x = 0; x < options.length; ++x )
    {
        if( options[x].type == "radio" && options[x].checked )
        {
        if (tb.value != "" && ddlCoord.value != "")
            {
            showTemplateDDL = "true";
            }
            else
            {
            showTemplateDDL = "false";
            }
        break;
        }
    }
            
    if (showTemplateDDL == "true")
    {
        ddl.disabled = false;
    }
    else
    {
        ddl.disabled = true;
    }

}

function disableButton() 
{
    document.getElementById('ctl00__cphMain__btnViewTemplateTasks').style.display="";
}

function Hide() 
{
     //   var tb = document.getElementById(lblid);
     //   var ddl = document.getElementById(ddl1);
    
    //if (tb.value == "")
   // {
    //    Refresh();
    //}
    //else
    {
      $find("mpe").hide();
      Refresh();
     //  ddl.disabled = true;
    }
}
    
    function Refresh()
    {
   window.location.reload(); 
    }
 

