function fnCheckParent(val, sender)
{
    var cbl = document.getElementById(sender);                                  
    if (cbl.value = val)
    {
        cbl.checked=true;
       }
    
}
function fnUnCheckChild(sender, count)
{
    var cbl = document.getElementById(sender);                                  

    if (cbl!=null){
        for (i=0;i<count;i++){
            var ca = document.getElementById(sender + "_"+i);
            if (ca!=null) {ca.checked=false}
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
                else if(selectedList.parentElement.outerText=="All"){
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
                        if (rbl != null && rbl.id!=selectedList.id && (rbl.parentElement.outerText=="No"||rbl.parentElement.outerText=="All")){           
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
   