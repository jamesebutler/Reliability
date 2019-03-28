// JScript File

        function ConfirmDelete(actionNumber,rowNum, obj){
	        if(actionNumber!=null){
	            var msg = localizedText.ConfirmDelete;
	            var actionJS = "DeleteGridRow('"+ actionNumber+"','" +rowNum+"','"+obj+"');";
		        confirmMessage(msg,'ctl00__ConfirmMessage', actionJS);		
	        }
        } 

            function DeleteGridRow(actionNumber,rowNum, obj){
            var gvTable = document.getElementById (obj);
             rowNum=parseInt(rowNum)+2;
            if (rowNum<10){rowNum = "0" + rowNum;}
            obj = obj+"_ctl"+rowNum+"_";
            var gv = document.getElementById (obj+"_lblResource");
            if (gv!=null){
                if (gv.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex!=null){
                ret = RIMOCSharedWS.DeleteMOCAction(actionNumber,OnSucceeded, OnFailed,OnFailed);
                    gvTable.deleteRow(gv.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex);
                }
            }
           for (i=1;i<gvTable.rows.length;i++){
                if ((i % 2)==0){
                   gvTable.rows[i].className="Border";
                }
                else{
                gvTable.rows[i].className="BorderSecondary";
                }
            }
            return false;
            }
            
            function OnSucceeded(arg){
            }
            function OnFailed(arg){
            }
            
function closeAndUpdate(){
    try{window.opener.updateItemCounts(); }
    catch(err){}
    try{window.close();}
    catch(err){}
    
}
function displayActionItemWindow(url,title){
    if (url!=null){
		var win=dhtmlmodal.open("actionItem", "iframe", url, title, "width=1000px,height=350px,resize=1,scrolling=1,center=1", "recal");		
		//setTimeout("dhtmlmodal.close("+win+")",30000);
		
	} 
}       
function closeActionItemWindow(){
    document.getElementById('_btnClose').click();
    document.getElementById('_btnRefresh').click();
    //window.location = window.location;
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

function setSelectedIndex(obj,index){
    if (obj!=null){
        obj.selectedIndex=index;
    }
}

function populateResourceList(obj){
    try{
        //obj= document.getElementById(obj);
        
        if(obj==null){
            obj=ResourceDDL;
            obj= document.getElementById(obj);
            }
        if(obj!=null){
        var selectedIndex = obj.selectedIndex;        
            if(obj.length<=2){            
                for (var i=0; i < EmployeeList.length;++i){
                    var resource = EmployeeList[i].split('::');
                    addOption(obj, resource[1], resource[0]);
                }
                 //obj.onblur=function(){this.selectedIndex=selectedIndex;this.onblur=null};
                 //document.body.focus();
                 return true;
            }
        
       return false;
        }
   }   
   catch(e){}
}

function copyResourceList(src,dest){
    try{
        if (src!=null && dest!=null){
            if (dest.length<=2){
                src = document.getElementById(src);
                for (var i=0; i< src.length; ++i){
                     addOption(dest, src.options[i].text, src.options[i].value);  
                }
                dest.onblur=function(){this.selectedIndex=0;this.onblur=null;};
                document.body.focus();
            }
        }
    }
    catch(e){
    
    }
}

var EmployeeList=null;
var ResourceDDL=null;
function GetEmployeeList(siteID, obj){
//GetEmployeeListBySite
    ResourceDDL=obj;
    ret = RIMOCSharedWS.GetEmployeeListBySite(siteID,function(arg){EmployeeList = arg;populateResourceList();}, OnFailed,OnFailed);
}