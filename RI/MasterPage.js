

function CheckForm(valSummary, valTrigger)
{ 
try{
		if (!Page_IsValid)
		{
			var s = new StringBuilder;
			
			//var s = "";
			for (i=0; i<Page_Validators.length; i++) {
				if (!Page_Validators[i].isvalid && 
					 typeof(Page_Validators[i].errormessage) === "string") {
					//s += "<li>" + Page_Validators[i].errormessage + "</li>";
					s.Append("<li>")
					s.Append(Page_Validators[i].errormessage)
					s.Append("</li>")
				}
			}
			//if (s !== "")
			if (s.buffer.length>0)
			{
				
				var summary = document.getElementById(valSummary)
				if (summary!=null){
					summary.innerHTML=s.ConvertToString();
					document.getElementById('ctl00__btnCloseBusy').click();
					document.getElementById(valTrigger).click ();
					Page_IsValid=true;
					return false;
				}
				//document.getElementById('=_ValidationSummary').innerHTML = s;
				//document.getElementById('SummaryValidation').style.display = 'inline';
			}
		} 
		else{
			Page_IsValid=true;
			return true;
		}
	   
		}
	/*else{
		Page_IsValid=false;
			return true;
	}*/
	
	catch (err)
	{
    
	}
	
}
        function DisplayFunctionalLocation(url,argument){
            window.showModalDialog(url,argument,"dialogHeight: 400px; dialogWidth: 500px; dialogTop: px; dialogLeft: px; edge: Raised; center: Yes; resizable: Yes; status: No;");
        }
        
        function PopupWindow(mypage,myname,w,h,scroll,menu,fullscreen){       
	        var win = null;
	        var LeftPosition = (screen.width) ? (screen.width-w)/2 : 0;
	        var TopPosition = (screen.height) ? (screen.height-h)/2 : 0;	       
	        if (fullscreen=="true"){w=screen.width;h=screen.height-100;LeftPosition=0;TopPosition=0;fullscreen="yes";}
	        settings ='status=yes,height='+h+',width='+w+',top='+TopPosition+',left='+LeftPosition+',scrollbars='+scroll+',resizable,menubar='+menu;
	        win = window.open(mypage,myname,settings)
	        if (win!=null) try {win.focus();} catch (err) {}
	      return win;
        }
        function DisplayPrintWindow(filename)
        {
        PopupWindow(filename,"PrintWindow",600,600,"yes","yes");
        }
        
        function ResetForm()
        {        
            document.forms[0].reset();
        }      
        function ExportExcel(fileName)
        {
            PopupWindow(fileName,"Excel",600,600,"yes","yes");
        }
        function PrintThisPage()
        {
            var NoPrintDiv = document.getElementById('NoPrint');
            if (NoPrintDiv!=null){
                NoPrintDiv.style.display="none";        
                window.print();
                NoPrintDiv.style.display="";
            }       
        }
        function ScrollControlInView(obj)
      {
		try{
         var el = document.getElementById(obj)
         var hdfScroll = document.getElementById ('_hdfScrollPos');
         if (el !== null)
         {        
            el.scrollIntoView();
            el.focus();
            if (hdfScroll!=null){
                hdfScroll.value = obj;
            }
         }         
         else if(hdfScroll!=null){
            if (hdfScroll.value!=''){
                var ct = document.getElementById(hdfScroll.value);
                 if (ct !== null)
                 {        
                    ct.scrollIntoView();
                    ct.focus();
                    if (hdfScroll!=null){
                        hdfScroll.value = ct;
                    }
                 }      
            }
         }
         }
         catch(err){
         
         }
      }

    function LoadEvents(){
		try{
        var prm = Sys.WebForms.PageRequestManager.getInstance();        
        if (!prm.get_isInAsyncPostBack())
          {
//              prm.add_initializeRequest(InitializeRequest);
              prm.add_beginRequest(BeginRequest);
//              prm.add_pageLoading(PageLoading);
//              prm.add_pageLoaded(PageLoaded);
              prm.add_endRequest(EndRequest);
          }
          }
          catch(err){
          
          }

    }
    function EndRequest(sender, args) {
		try{
			//$get('ClientEvents').innerHTML += "PRM:: End of async request.<br/>";
			//ScrollControlInView();
			EnableSubmitButtons();
			cursor_clear();
			document.getElementById('ctl00__btnCloseBusy').click();
			Page_HasBeenSubmitted=false;
		}
		catch(err){
		
		}
    } 
    function BeginRequest(sender, args) {
		try{
			//$get('ClientEvents').innerHTML += "PRM:: End of async request.<br/>";
			Page_HasBeenSubmitted=true;
			cursor_wait();
			document.getElementById('ctl00__imbBusy').click();	
			DisableSubmitButtons();		
		}
		catch(err){
		
		}     
    }
    
    function BeginPostBack(){
		cursor_wait();
		document.getElementById('ctl00__imbBusy').click();	
    }
    
    function cursor_wait() {
		try{    
			document.body.style.cursor = 'wait';//url('Atlas_Indicator.ani')//'wait';
		}
		catch(err){
		
		}
    }
    function cursor_clear() {
		try{
			document.body.style.cursor = 'default';
		}
		catch(err){
		
		}
    }


      function CloseThisPage()
      {
		try{
		//alert('close');
			window.close();
		}
		catch(err){
		
		}
      }
      
      function GetPrintContent()
      {
            var PrintDiv =  document.getElementById('printcontent');
            var ContentDiv =  window.opener.document.getElementById('contentstart');
            //var SubTitleDiv = document.getElementById('divTitle');
            //var OpenerSubTitleDiv = window.opener.document.getElementById('divTitle');
            var innerHtml = ContentDiv.innerHTML.replace("runat=server"); 
            PrintDiv.innerHTML = innerHtml;            
            //SubTitleDiv.innerHTML = OpenerSubTitleDiv.innerHTML;
      
      }
      

     function SaveChangesBeforeLeaving(url,submitBtn,msg){        
        
	    try{
			var answer = confirm(msg);
	        if (answer){
				document.getElementById (submitBtn).setAttribute ("url",url)			
	            document.getElementById(submitBtn).click();		       
	        }	 
	        else{
				return answer
	        }
	        if (url.length>0){
	            location.href = url;      
	        }
	    }
	    catch (ex){
	    }
     }    

function ShowMyModalPopup() {
	try{
		document.getElementById('ctl00__imbHelp').click()
	}
	catch(err){
	}
}
   
function dispHelp(){
try{
    var keyCode = event.keyCode;
    if(keyCode === 112){   
    ShowMyModalPopup();  
    }
    }
catch (err)
    {
    
    }
}

function disableDefault(){
	try{
    event.returnValue = false;
    return false;
    }
    catch(err)
    {
    } 
}

function DisableButton(buttonElem) {
	buttonElem.value = 'Please Wait...';
	buttonElem.disabled = true;
}
function StringBuilder() {
this.buffer = [];
}
StringBuilder.prototype.Append = function(str) {
this.buffer[this.buffer.length] = str;
};
StringBuilder.prototype.ConvertToString = function() {
return this.buffer.join('');
};

//var Page_HasBeenSubmitted=false;
var _saveDisableSubmitButtons = new Array();
function DisableSubmitButtons(){
	var ct=0;
	
	try{
		var tagElements = document.getElementsByTagName('INPUT');
		for (var k = 0 ; k < tagElements.length; k++) {
			if (tagElements[k].type=='submit'){
				if (tagElements[k].disabled==false){
					_saveDisableSubmitButtons[ct] = tagElements[k].id;
					ct++;
					tagElements[k].disabled=true;
					tagElements[k].style.visibility = 'hidden';
				}			
			}
		}
	}
	catch(ex){
	
	}	
}

function EnableSubmitButtons(){	
	try{
		for (var k = 0 ; k < _saveDisableSubmitButtons.length; k++) {
			var btn = document.getElementById(_saveDisableSubmitButtons[k]);
			if (btn!=null) {btn.style.visibility = 'visible';btn.disabled=false}
			/*if (tagElements[k].type=='submit'){
				if (tagElements[k].disabled==false){
					_saveDisableSubmitButtons[ct] = tagElements[k].id;
					ct++;
					tagElements[k].disabled=true;
				}			
			}*/
		}
	}
	catch(ex){
	
	}	
}
function customHandler(desc,page,line,chr)  {
 alert(
  'JavaScript error occurred! \n'
 +'The error was handled by '
 +'a customized error handler.\n'
 +'\nError description: \t'+desc
 +'\nPage address:      \t'+page
 +'\nLine number:       \t'+line
 )
 return true
} 
//LoadEvents();
//window.onerror=customHandler;
function UnLoadPage(){
//	if (ConfirmBeforeLeave){
//		return "You have attempted to leave this page.  If you have made any changes to the fields without clicking the Save button, your changes will be lost.  Are you sure you want to exit this page?";
//	}
//	else{
//		return true
//	}
}
//function displayViewListWindow(url){
//	if (url!=null){
//			var win=dhtmlmodal.open("viewlist", "iframe", url, "View List 2", "width=800px,height=350px,resize=1,scrolling=1,center=1", "recal");		
//	}
//}

function DisplayMissingKeys(url){
            var win=dhtmlmodal.open("viewlist", "iframe", url, "Missing Resources", "width=600px,height=350px,resize=1,scrolling=1,center=1", "recal");		
        }
        
function viewIncident(url,msg){
	if(url!=null){
		//ConfirmBeforeLeave=true;
		//if (msg==null){msg="You have attempted to leave this page.  If you have made any changes to the fields without clicking the Save button, your changes will be lost.  Are you sure you want to exit this page?"}
		if (msg==null){msg=localizedText.ConfirmRedirect}
		//if (confirm(msg)){
		if (confirmMessage(msg,'ctl00__ConfirmMessage', "confirmYes('"+url+"');")){
		//"You have attempted to leave this page.  If you have made any changes to the fields without clicking the Save button, your changes will be lost.  Are you sure you want to exit this page?")){
		    //parent.window.location = url;
		}
	}
} 
function confirmYes(url){
    parent.window.location = url;
}
function confirmMessage(message, messageID, yesHandler, noHandler){
    var msg = document.getElementById(messageID+'__divMessage');
    var trigger = document.getElementById(messageID+'__imbMessageBoxTrigger');
    eval('var returnValue'+messageID+'=-1;');
    //var ok = document.getElementById(messageID+'__btnOK');
    //var cancel = document.getElementById(messageID+'__btnClose');
    var mpeConfirm = $find(messageID+'__mpeMessage');
    //ctl00__ConfirmMessage__btnOK
    if (mpeConfirm!=null){
        mpeConfirm._onOkScript = yesHandler;
        mpeConfirm._onCancelScript=noHandler;
    }
  
    if (msg!=null){
        msg.innerHTML= message;
    }
   
    
    if (trigger!=null){
        trigger.click();
    }
}

if(document.getElementById) {
	window.alert = function(txt) {
		createCustomAlert(txt);
	}
}

function createCustomAlert(message){
//_AlertMessage
var msg = document.getElementById('ctl00__AlertMessage__divMessage');
var trigger = document.getElementById('ctl00__AlertMessage__imbMessageBoxTrigger');
//var header = document.getElementById('ctl00__AlertMessage__bannerTitle__imgHeader');

if (msg!=null){
        msg.innerHTML= message;
    }

if (trigger!=null){
        trigger.click();
    }
}


function pausecomp(millis) 
{
    var date = new Date();
    var curDate = null;

    do { curDate = new Date(); } 
    while(curDate-date < millis);
} 

function displayModalPopUpWindow(url,name,title,w,h){
		if (url!=null){
			var win=dhtmlmodal.open(name, "iframe", url, title, "width="+w+"px,height="+h+"px,resize=1,scrolling=1,center=1", "recal");		
		}
	}
	


function isChecked(list){
	var cbIsChecked=false;
	var count=1000; //We should never hit this number
        if (list!=null){
            var allList = document.getElementById(list);
            if (allList!=null){
                for (var i=0; i<count;i++){  //Loop through all of the child check boxes
                    var cb = document.getElementById(allList.id+'_' + i);                                  
                    if (cb !== null){    
						if (cb.checked==true)       
							cbIsChecked=true;
                    }
                    else{
						return cbIsChecked;
                    }
                }
            }            
        }
        return cbIsChecked;
    }
    

// var localizedText = new function(){
//  this.Close = "Close Me";
//}
 
 