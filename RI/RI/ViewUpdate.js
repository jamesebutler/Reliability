// JScript File 
//There is also an incidenttype.js which calls some of the same functions. 

 function setLabel(lbl,cb,val){
        var display='';
        if (cb.checked==1){
            display=val;
        }
        lbl.value=display;
    }
    function unCheckAll(list,count){
        if (list!=null){
            var allList = document.getElementById(list.id+'_' + 0);
            if (allList!=null && event.srcElement.id==allList.id && (allList.parentElement.outerText=='All' ||allList.parentElement.outerText=='No'||allList.parentElement.outerText=='Both' )){
                for (var i=1; i<count;i++){  
                    var rbl = document.getElementById(list.id+'_' + i);                                  
                    if (rbl != null){           
                        rbl.checked=false;
                    }
                }
            }
            else{
                if (allList!=null && (allList.parentElement.outerText=='All' ||allList.parentElement.outerText=='No'||allList.parentElement.outerText=='Both' )){
                    allList.checked=false;
                }
            }
        }
    }
    
    function unCheckNo(list,count){
        if (list!=null){
        var selectedList=null;
            if (event.srcElement.id!=""){
                selectedList = document.getElementById(event.srcElement.id);
            }
            else if(event.srcElement.htmlFor!=""){
                selectedList = document.getElementById(event.srcElement.htmlFor);
            }
            if (selectedList!=null){
                if (selectedList.parentElement.outerText=="No"){              
                    for (var i=0; i<count;i++){  
                        var rbl = document.getElementById(list.id+'_' + i);                                  
                        if (rbl != null && rbl.id!=selectedList.id){           
                            rbl.checked=false;
                        }
                    }                   
                }
                else if(selectedList.parentElement.outerText=="All"||selectedList.parentElement.outerText=="Both"){
                    for (var i=0; i<count;i++){  
                        var rbl = document.getElementById(list.id+'_' + i);                                  
                        if (rbl != null && rbl.id!=selectedList.id && rbl.parentElement.outerText!="No"){           
                            if (selectedList.checked==true)
                                rbl.checked=true;
                            else
                                rbl.checked=false;
                        }
                        else if(rbl.parentElement.outerText=="No"){
                            rbl.checked=false;
                        }
                    }     
                }
                else{ //Uncheck All and No
                    for (var i=0; i<count;i++){  
                        var rbl = document.getElementById(list.id+'_' + i);                                  
                        if (rbl != null && rbl.id!=selectedList.id && (rbl.parentElement.outerText=="No"||rbl.parentElement.outerText=="All"||rbl.parentElement.outerText=="Both")){           
                            rbl.checked=false;
                        }                        
                    }     
                }
            }
           
        }
    }
    function checkAll(list,count){
       unCheckNo(list,count);
    }
    
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
            //var cbl = document.getElementById (event.srcElement.id);
            if (cbl !=null){
                cbl.checked=curValue;
            }
         }
            
    }
    function UnselectRadio(rd,count){
        if (rd!=null){           
            for (var i=0; i<count;i++){
                var rbl = document.getElementById(rd.id+'_' + i);    
                if (rbl != null && event.srcElement.id==rbl.id){           
                    if (rbl.checked==true){rbl.checked=false}
                 }
            }
        }
    }
    
    function SetDivision(facilityClient){
	if (division!=null){
		division.selectedIndex=0;
	}
	hideIncidentListing();
	if (facilityClient.length>0){	
		var ret = CascadingLists.GetSiteDivision(facilityClient,OnComplete, OnTimeOut, OnError);	
	}

//	if (incidentListing!=null){
//		incidentListing.style.display="none";
//		incidentListing.style.visibility="hidden";
//		incidentListing.innerHTML="<span>mjp</span>";
//	}		
	
	
	return(true);
}

function showIncidentListing(){
var bhIncidentListing = $find("bhIncidentListing");
	if (bhIncidentListing!=null){
		bhIncidentListing.Collapsed =false;
		bhIncidentListing.set_Collapsed(false);
	}	
}
function hideIncidentListing(){
var bhIncidentListing = $find("bhIncidentListing");
	if (bhIncidentListing!=null){
		bhIncidentListing.Collapsed =true;
		bhIncidentListing.set_Collapsed(true);
	}	
}
function OnComplete(arg) {	
	var divisionClient = $find("bhDivision");
	if (divisionClient!=null && site!=null){
		if (site.value!="AL"){
		divisionClient.selectedValue = arg;
		divisionClient._selectedValue=arg;
		}
	}	        
}

function OnTimeOut(arg) {
alert("TimeOut OnSecurityTimeOut");
}

function OnError(arg) {
alert("Error encountered OnSecurityError");
}

function HideExcelSelect() {
	try{
		document.getElementById('ctl00__cphMain__btnCancel').click()
	}
	catch(err){
	}
}