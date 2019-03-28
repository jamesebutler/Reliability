// JScript File
    function PopupCalendar(mypage,myname,w,h,scroll,menu,fullscreen){
	       try{
	        var win = null;
	        var LeftPosition = (screen.width) ? (screen.width-w)/2 : 0;
	        var TopPosition = (screen.height) ? (screen.height-h)/2 : 0;
	        if (fullscreen=="yes"){w=screen.width;h=screen.height-100;LeftPosition=0;TopPosition=0;}
	        settings ='height='+h+',width='+w+',top='+TopPosition+',left='+LeftPosition+',scrollbars='+scroll+',resizable=no,menubar='+menu
	        win = window.open(mypage,myname,settings)
	      }
	      catch(err){
	      
	      }
        }
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
				var win=dhtmlmodal.open("StartEnd", "iframe", url, localizedText.SelectStartEnd, "width=480px,height=230px,resize=1,scrolling=0,center=1", "recal");		
			
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
				var win=dhtmlmodal.open("StartEnd", "iframe", url, headerText, "width=240px,height=230px,resize=1,scrolling=1,center=1", "recal");		
			
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

