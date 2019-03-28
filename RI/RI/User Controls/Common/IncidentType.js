
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
    
    function disableClick(rdb){
       
        return false;
       
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
                else if(selectedList.parentElement.outerText=="All" || selectedList.parentElement.outerText=="Все" || selectedList.parentElement.outerText=="Todas" || selectedList.parentElement.outerText=="Tous" || selectedList.parentElement.outerText=="Both"){
                    for (var i=0; i<count;i++){  
                        var rbl = document.getElementById(list.id+'_' + i);                                  
                        if (rbl != null && rbl.id!=selectedList.id && rbl.parentElement.outerText!="No" && rbl.parentElement.outerText!="Нет" && rbl.parentElement.outerText!="Não"){           
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
                        if (rbl != null && rbl.id!=selectedList.id && (rbl.parentElement.outerText=="No"||rbl.parentElement.outerText=="All" ||
                        rbl.parentElement.outerText=="Нет"||rbl.parentElement.outerText=="Все"||rbl.parentElement.outerText=="Não"||rbl.parentElement.outerText=="Todas" ||
                        rbl.parentElement.outerText=="Tous"||rbl.parentElement.outerText=="Both")){           
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
    
    
// Notify ScriptManager that this is the end of the script.
//if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();

