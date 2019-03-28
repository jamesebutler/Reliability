// JScript File

function IncidentTypeOnClick(listCount){ 
    if (event.srcElement.id=='ctl00__cphMain__ucSiteDropdowns__cblIncidentType_0') //User checked All
    {
        for (i = 1; i<listCount;i++)
            {
            var cbl = document.getElementById('ctl00__cphMain__ucSiteDropdowns__cblIncidentType_' + i);   
            if (cbl != null){
                cbl.checked=false;
            }
        }
      } 
      else {
            var cbl = document.getElementById('ctl00__cphMain__ucSiteDropdowns__cblIncidentType_0' );   
            if (cbl != null){
                cbl.checked=false;
            }
       }
}

function EstimatedDueDateOnClick(listCount){ 
    if (event.srcElement.id=='ctl00__cphMain__ucSiteDropdowns__cblDateRange_0') //User checked All
    {
        for (i = 1; i<listCount;i++)
            {
            var cbl = document.getElementById('ctl00__cphMain__ucSiteDropdowns__cblDateRange_' + i);   
            if (cbl != null){
                cbl.checked=false;
            }
        }
      } 
      else {
            var cbl = document.getElementById('ctl00__cphMain__ucSiteDropdowns__cblDateRange_0' );   
            if (cbl != null){
                cbl.checked=false;
            }
       }
}

function DateRange_SelectedIndexChanged(listCount,startDate,endDate){
        for (i=0; i<listCount; i++)
        {
            var rdb = document.getElementById('ctl00__cphMain__ucSiteDropdowns__rblDateRange_'+i);
            if (rdb != null){
                if (rdb.checked==true){
                    alert (rdb.value);
                    var sDate = document.getElementById(startDate);
                    var eDate = document.getElementById(endDate);
                }
            }
        }
}