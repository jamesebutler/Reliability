// JScript File

    <!--        
        function customClickhandler(obj,reverse){
        if (reverse==null || reverse==false){
			var first=0;
			var second=1;
		}
		else{
			var first=1;
			var second=0;
		}
        
        if (obj ==null){return false}
			var AutoComplete = obj;
			var AutoComplete2 = $get(obj.id+'2');
			var modifiedText = AutoComplete.value.split('*');
			if (modifiedText != null){
				if (modifiedText.length>=2){
					if (reverse==null || reverse==false){
						AutoComplete.value = modifiedText[first].trim();
						}
					else {
						AutoComplete.value = modifiedText[second].trim();
					}
					if (ECEquipmentid!=null){
						ECEquipmentid.value=modifiedText[first].trim();
					}
					if (AutoComplete2 !=null){	
						if(AutoComplete2.type=="text"){					
							AutoComplete2.value  = modifiedText[second].trim();						
						}
						else{
							AutoComplete2.innerHTML  = modifiedText[second].trim();							
						}
						
					 }
				}
			}
        }      
        function SelectedEquipment(desc,id,idObj,txtObj,btnClose){
			var AutoComplete = idObj; 
			var AutoComplete2 = txtObj; 
			
			if (desc.length==0|| desc =="undefined") 
				desc="";
			if (id.length==0|| id=="undefined")
				id="";			
			if (AutoComplete !=null) {
				AutoComplete.value = id;
			}
			if (ECEquipmentid!=null){
						ECEquipmentid.value=id;
					}
			if (AutoComplete2 !=null) {
				AutoComplete2.innerHTML = desc;
			}
			var closeButton = btnClose;
			if (closeButton!=null){
				closeButton.click();
				document.getElementById('ctl00__btnCloseBusy').click();
			}
			
//			if (grdECSearch!=null){
//				grdECSearch.style.display='none';
//			}
			if (ECResults!=null){
				ECResults.innerHTML='';
			}
        }
        function SetFunctionalLocationContext(key){
			var auto1 = $find('autoCompleteBehavior1');
			var auto2 = $find('autoCompleteBehavior2');
			var auto3 = $find('autoCompleteBehavior3');
			
			if (auto1!=null){
				auto1.set_contextKey(key);
			}
			if (auto2!=null){
				auto2.set_contextKey(key);
			}
			if (auto3!=null){
				auto3.set_contextKey(key);
			}
        }
        
        function performEquipmentSearch(){
		try{
			var site = facilityClient;
			if (busArea!=null){
				var bus = busArea.value;
			}
			if (lineBreak!=null){
				var line = lineBreak.value;
			}
			if (site!=null){
				//ret = FunctionalLocationLookup.GetDeleteAccess(site.value,currentUserName, OnSecurityComplete, OnSecurityTimeOut, OnSecurityError);
				if (ECResults!=null){
					ECResults.innerHTML= "<h2>Searching...</h2>";
					ECResults.className='dynamicPopulate_Updating';
				}
				//dynamicPopulate_Updating
				ret = FunctionalLocationLookup.PerformEquipmentSearch(site.value, bus, line,ECCriticality.value, ECEquipClass.value, ECEquipType.value, ECEquipmentid.value, ECEquipmentDesc.value, ECLimit.value,OnECComplete, OnECTimeOut, OnECError);
			}
			return(true);
		}
		catch(err){
	
		}
	}
    //-->
	function OnECTimeOut(arg) {
		alert("TimeOut encountered when calling PerformEquipmentSearch.");
	}

	function OnECError(arg) {
		alert("Error encountered when calling PerformEquipmentSearch.");
	}
	function OnECComplete(arg) {
	try{	
		if (ECResults!=null){
			ECResults.className='';
			ECResults.innerHTML= arg;
			ECResults.style.display='';
			}
		}
	catch(err){

	}
}

    var lastCheckedNode=null;
    
    function oldtreeSelect(){
    var obj = window.event.srcElement;
    var treeNodeFound = false;
    var equipID="";
    var equipDesc="";
        
    if (obj.tagName == "A"){
        //if (obj.target=="populated") return false
        //else return true;
        if (lastCheckedNode!=null){
			if(lastCheckedNode.checked==true){
				lastCheckedNode.checked=false;}
			}
        obj.checked=true;
        
        var objID = obj.id+'CheckBox';
        objID=objID.replace("_tvFunctionalLocationt","_tvFunctionalLocationn")
        var parentcb = document.getElementById(objID);
        
        if (parentcb!=null){
			parentcb.checked=true;
			lastCheckedNode=parentcb;
			lastCheckedNode.backgroundColor="";
        }
        var modifiedText = obj.innerHTML.split('*');      
		if (modifiedText != null){
			if (modifiedText.length>=2){
				equipID=modifiedText[0];
				equipDesc=modifiedText[1];	
			}
		
		}
        SelectedEquipment(equipDesc,equipID,ECFunctionalLocationSearch,ECFunctionalLocationSearchDesc,null);
        //obj.style.backgroundColor="#ff0000"
        }
    else if (obj.tagName =="IMG"){
        return true;}
    else if (obj.type=="checkbox" && obj.tagName=="INPUT"){
        if (lastCheckedNode!=null){
            if(lastCheckedNode.checked==true && lastCheckedNode.id!=obj.id){
				lastCheckedNode.checked=false;}
			}
        lastCheckedNode=obj;
        var modifiedText = obj.title.split('*');      
		if (modifiedText != null){
			if (modifiedText.length>=2){
				equipID=modifiedText[0];
				equipDesc=modifiedText[1];	
			}
		
		}
        SelectedEquipment(equipDesc,equipID,ECFunctionalLocationSearch,ECFunctionalLocationSearchDesc,null);
        //alert(obj.title);
        return true;
}
    }    
   
function refreshTreeView(){
	var site = facilityClient;
	if (site!=null){
	cursor_wait();
		ret = FunctionalLocationLookup.RefreshTreeView(site.value,site.options[site.selectedIndex].text,OnTreeViewComplete, OnTreeViewTimeOut, OnTreeViewError);				
	}
}
function OnTreeViewTimeOut(arg) {
		alert("TimeOut encountered when calling PerformEquipmentSearch.");
		cursor_clear();
	}

function OnTreeViewError(arg) {
		alert("Error encountered when calling PerformEquipmentSearch.");
		cursor_clear();
	}

function OnTreeViewComplete(arg){
	cursor_clear();
	if (ECTree!=null){
		ECTree.innerHTML = arg;
	}
}
function displayFunctionalTreeWindow(url){
	if (url!=null){
		//document.getElementById('ctl00__imbBusy').click();
		//var win=dhtmlwindow.open("functionaltree", "iframe", url, "Functional Location Tree", "width=800px,height=350px,resize=1,scrolling=1,center=1", "recal");		
		var site = facilityClient;
		if (ECFunctionalLocationSearch!=null && site!=null){
			url = url + "?Equipid="+ECFunctionalLocationSearch.value+"&SiteID="+site.value;
			var title="Functional Location Tree";
			if (localizedText!=null){
			    if (localizedText.FunctionalLocationTree!=null){
			        title = localizedText.FunctionalLocationTree
			    }
			}
			var win=dhtmlmodal.open("functionaltree", "iframe", url, title, "width=800px,height=350px,resize=1,scrolling=1,center=1", "recal");
		} 
	
		
		win.onclose=function(){ 
//			//Run custom code when window is being closed (return false to cancel action):
			//document.getElementById('ctl00__btnCloseBusy').click();
			try{var theform = this.contentDoc.forms[0];
			var equipid=theform._hdFunctionalLocation.value;			
			var modifiedText = equipid.split('*');      
			if (modifiedText != null && equipid.length>0){
				if (modifiedText.length>=2){
					var equipID=modifiedText[0];
					var equipDesc=modifiedText[1];	
				}
				SelectedEquipment(equipDesc,equipID,ECFunctionalLocationSearch,ECFunctionalLocationSearchDesc,null);
			}       
			theform.innerhtml="";			
			}catch(err){
			alert(err);
			}
			return true//window.confirm("Close window 1?")
		}
	}
}