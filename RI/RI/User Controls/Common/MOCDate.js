        function DisplayCalendar(filename,parent, valueParent,commitScript,headerText)
        {        
			try{
				var dt = eval("window.document.forms(0)."+valueParent+".value");
				var ct='';
				if(commitScript!=null&&commitScript!='')
					ct = eval("window.parent." + commitScript);
				if (ct==null) ct='';
				 //PopupCalendar(filename+"&Date="+dt+ct,"PrintWindow",204,160,"no","no");
				 if (headerText==null) headerText='Select Date';
				 displaySingleCalendarWindow(filename+"&Date="+dt+ct,parent,valueParent,headerText);
				}
			catch(err){
			
			}
		} 
		
		function DisplayStartEndCalendar(filename,startParent,endParent,startValue,endValue,commitScript)
        {        
			try{
				var dt = eval("window.document.forms(0)."+startValue+".value");
				var enddt = eval("window.document.forms(0)."+endValue+".value");
				var ct='';
				if(commitScript!=null&&commitScript!='')
					ct = eval("window.parent." + commitScript);
				if (ct==null) ct='';
				//PopupCalendar(filename+"&Date="+dt+enddt+ct,"PrintWindow",430,200,"no","no");
				displayCalendarWindow(filename+"&StartDate="+dt+"&EndDate="+enddt+ct,startParent,endParent,startValue,endValue);
				} 
			catch(err){
			
			}
		} 
		
		function displayCalendarWindow(url,startParent,endParent,startValue,endValue){
			if (url!=null){
				var win=dhtmlmodal.open("StartEnd", "iframe", url, localizedText.SelectStartEnd, "width=450px,height=210px,resize=1,scrolling=1,center=1", "recal");		
			
				win.onclose=function(){ 
//				//Run custom code when window is being closed (return false to cancel action):
				//document.getElementById('ctl00__btnCloseBusy').click();
				try{
					var theform = this.contentDoc.forms[0];			
					var startDate = document.getElementById(startParent);
					var endDate = document.getElementById(endParent);
					var startDateValue= document.getElementById(startValue);
					var endDateValue= document.getElementById(endValue);
					startDate.value = theform.UcDateRangeSelector1__startDate.value;					
					endDate.value = theform.UcDateRangeSelector1__endDate.value;
					startDateValue.value = theform.UcDateRangeSelector1__startDateValue.value;					
					endDateValue.value = theform.UcDateRangeSelector1__endDateValue.value;		
					startDate.title=startDateValue.value;
					endDate.title=endDateValue.value;		
					}
				catch(err){}
				return true//window.confirm("Close window 1?")
				}			
			}
		}
		function displaySingleCalendarWindow(url,startParent,valueParent,headerText){
			    if (url!=null){
				var win=dhtmlmodal.open("StartEnd", "iframe", url, headerText, "width=225px,height=210px,resize=1,scrolling=1,center=1", "recal");		
			
				win.onclose=function(){ 
//				//Run custom code when window is being closed (return false to cancel action):
				//document.getElementById('ctl00__btnCloseBusy').click();
				try{
					var theform = this.contentDoc.forms[0];			
					var startDate = document.getElementById(startParent);
					var startDateValue = document.getElementById(valueParent);
					startDate.value = theform._Date.value;		
					startDateValue.value = theform._DateValue.value;
								
					}
				catch(err){}
				return true//window.confirm("Close window 1?")
				}			
			}
		}
        // Notify ScriptManager that this is the end of the script.
//if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();


function enableField(sender,parent) {
    alert(sender);
    var daysAfterTextBox = document.getElementById(sender);
    var calendarImage = document.getElementById(parent);
    if (daysAfterTextBox.value.length > 0) {
        calendarImage.disabled = true;
        }
    else {
        calendarImage.disabled = false;
    }
}				

//function clearField(sender) {
    //alert(sender);
  //  var calendarTextBox = document.getElementById(sender);
    //if (calendarTextBox.value.length > 0) {
      //  calendarTextBox.value = "";
        //}
//}				