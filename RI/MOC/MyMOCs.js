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
            if (cbl !=null){
                cbl.checked=curValue;
            }
         }
            
    }

function CreateTaskHeader(MocNum,type,activity,insertUser,createdDate){
    var SiteId = facilityClient.value;
    var title = titleClient.value;
    var startDateTxt = startDate.value;
    var endDateTxt = endDate.value;
    var BusUnit = busArea.value;
    var Line = lineBreak.value;
    var Desc = description.value;
    
    ret = TaskTrackerHeader.CreateMTTTaskHeader(title,MocNum,'MOC',startDateTxt,endDateTxt,SiteId,BusUnit,Line,Desc,type,activity,insertUser,createdDate, OnMTTComplete, OnALTimeOut, OnALError);
}

    