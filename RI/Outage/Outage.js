// JScript File

function displayGanttWindow(url){
	if (url!=null){
			var win=dhtmlmodal.open("GanttChart", "iframe", url, "Gantt View", "width=850px,height=350px,resize=1,scrolling=1,center=1", "recal");		
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
                if (selectedList.parentElement.outerText=="No" || selectedList.parentElement.outerText=="Нет" || selectedList.parentElement.outerText=="Não"){              
                    for (var i=0; i<count;i++){  
                        var rbl = document.getElementById(list.id+'_' + i);                                  
                        if (rbl != null && rbl.id!=selectedList.id){           
                            rbl.checked=false;  
                        }
                    }                   
                }
                else if(selectedList.parentElement.outerText=="All Outages" || selectedList.parentElement.outerText=="Все" || selectedList.parentElement.outerText=="Todas as Paradas"){
                    for (var i=0; i<count;i++){  
                        var rbl = document.getElementById(list.id+'_' + i);                                  
                        if (rbl != null && rbl.id!=selectedList.id && rbl.parentElement.outerText!="No"){           
                            if (selectedList.checked==true)
                                rbl.checked=true;
                            else
                                rbl.checked=false;
                        }
                        else if(rbl.parentElement.outerText=="No" ||rbl.parentElement.outerText=="Нет"||rbl.parentElement.outerText=="Não"){
                            rbl.checked=false;
                        }
                    }     
                }
                else{ //Uncheck All and No
                    for (var i=0; i<count;i++){  
                        var rbl = document.getElementById(list.id+'_' + i);                                  
                        if (rbl != null && rbl.id!=selectedList.id && (rbl.parentElement.outerText=="No"||rbl.parentElement.outerText=="All Outages" ||
                        rbl.parentElement.outerText=="Нет"||rbl.parentElement.outerText=="Все"||rbl.parentElement.outerText=="Não"||rbl.parentElement.outerText=="Todas as Paradas")){           
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
   
	function calcIframeHeight(obj)
	{
	  //find the height of the internal page
		obj = document.getElementById(obj);
		if (obj!=null){ 
			//calculate height 
			var the_height=obj.contentWindow.document.body.scrollHeight+40; 

			//minimal height of 600 
			//if (the_height < 600) {the_height=600} 

			//change height 
			obj.height= the_height; 
		}

	}
	
    function copyDDL (sourceDDL, targetDDL) 
    { 
        // passed in as strings
        var ddl1 = document.getElementById(sourceDDL);
        var ddl2 = document.getElementById(targetDDL);
            if (ddl2.length<=2){
                for (var i=0; i < ddl1.options.length; i++) {
                    addOption(ddl2,ddl1.options[i].text,ddl1.options[i].value);
            }
        }
    }
    
    function addOption(object,text,value)
    {
        var defaultSelected = false;
        var selected = false;
        var optionName = new Option(text, value, defaultSelected, selected)
        object.options[object.length] = optionName;
    }