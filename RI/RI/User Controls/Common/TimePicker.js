// JScript File
var selectedHoursRow=0;
var selectedHoursCol=0;
var selectedMinsRow=0;
var selectedMinsCol=0;
var selectedMinsObj;
var selectedHoursObj;
var prevSelectedHoursObj;
var prevSelectedMinsObj;
var prevMinsCSS;
var prevHrsCSS;
var selectedHours=0;
var selectedMins=0;

function changeHoursColor(trObj,css){                          
    if((trObj.cellIndex==selectedHoursCol && trObj.parentElement.rowIndex==selectedHoursRow)!=true)
    {
        trObj.className = css;       
    }
}  
function changeMinutesColor(trObj,css){                          
    if((trObj.cellIndex==selectedMinsCol && trObj.parentElement.rowIndex==selectedMinsRow)!=true)
    {
        trObj.className = css;
    }
}  

function selectHoursColor(trObj,css){

selectedHoursCol=trObj.cellIndex;
selectedHoursRow=trObj.parentElement.rowIndex;
if (prevSelectedHoursObj!=null){prevSelectedHoursObj.className =  prevSelectedHoursObj.OriginalCSS;}
prevSelectedHoursObj = trObj;
trObj.className = css;
selectedHours = trObj.TimeValue;
}
function selectMinutesColor(trObj,css){
selectedMinsCol=trObj.cellIndex;
selectedMinsRow=trObj.parentElement.rowIndex;
if (prevSelectedMinsObj!=null){prevSelectedMinsObj.className =  prevSelectedMinsObj.OriginalCSS;}
prevSelectedMinsObj = trObj;
trObj.className = css;
selectedMins = trObj.TimeValue;

}
function updateTime(txtObj){
var txt = document.getElementById(txtObj);
if (txt!=null){
    if(selectedHours.toString().length==1) selectedHours = '0'+selectedHours;
    if(selectedMins.toString().length==1) selectedMins = '0'+selectedMins;
    txt.value=selectedHours + ':' + selectedMins
    cancelPopup();
    }
}

function clearTime(txtObj){
var txt = document.getElementById(txtObj);
if (txt!=null){    
    txt.value='';
    cancelPopup();
    }

}

function updateTimeToCurrentTime(txtObj){
    var now = new Date();
    var hours = now.getHours();
    var minutes = now.getMinutes();
    if (hours.toString().length==1)hours="0"+hours;
    if (minutes.toString().length==1)minutes="0"+minutes;
    selectedHours = hours;
    selectedMins = minutes;
    updateTime(txtObj);
}
function cancelPopup(){
    AjaxControlToolkit.PopupControlBehavior.__VisiblePopup.hidePopup(); 
    return false;
}

function currentTime(obj){
    var btn = document.getElementById (obj);
    var now = new Date();
    var hours = now.getHours();
    var minutes = now.getMinutes();
    if (hours.toString().length==1)hours="0"+hours;
    if (minutes.toString().length==1)minutes="0"+minutes;
    var currTime = hours +":"+ minutes.toString();
    if (btn !=null){btn.innerHTML="Time:"+currTime;}
}
// Notify ScriptManager that this is the end of the script.
//if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();

