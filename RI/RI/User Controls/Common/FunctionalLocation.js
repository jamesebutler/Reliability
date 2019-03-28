// JScript File

    var lastCheckedNode=null;
    
    function select(){
    var obj = window.event.srcElement;
    var treeNodeFound = false;
    if (obj.tagName == "A"){
        if (obj.target=="populated") return false
        else return true;}
    else if (obj.tagName =="IMG"){
        return true;}
    else if (obj.type=="checkbox" && obj.tagName=="INPUT"){
        if (lastCheckedNode!=null){
            lastCheckedNode.checked=false;}
        lastCheckedNode=obj;
        alert(obj.title);return true;
}
    }
    
   

    
        function customClickhandler(){
		try{
			var txtAutoComplete = $get("ctl00__cphMain__functionalLocationTree__txtFunctionalLocation");
			var txtAutoComplete2 = $get("ctl00__cphMain__functionalLocationTree__txtFunctionalLocation2");
			if (txtAutoComplete!=null && txtAutoComplete2!=null){
				var modifiedText = txtAutoComplete.value.split('*');
				if (modifiedText != null){
					if (modifiedText.length>=2){
						txtAutoComplete.value = modifiedText[0].trim();
						txtAutoComplete2.innerHTML  = modifiedText[1].trim();
					}
				}
			}
			}
		catch(err){
		
		}
        }      

    function PopupWin(mypage,myname,w,h,scroll,menu,fullscreen){
	        var win = null;
	        var LeftPosition = (screen.width) ? (screen.width-w)/2 : 0;
	        var TopPosition = (screen.height) ? (screen.height-h)/2 : 0;
	        if (fullscreen=="yes"){w=screen.width;h=screen.height-100;LeftPosition=0;TopPosition=0;}
	        settings ='height='+h+',width='+w+',top='+TopPosition+',left='+LeftPosition+',scrollbars='+scroll+',resizable,menubar='+menu
	        win = window.open(mypage,myname,settings)
	      
        }
        function DisplayFunctionalLocation(filename,parent)
        {        
        if (parent != null)
        {
         var val = eval("window.document.forms(0)."+parent+".value");
        }
        PopupWin(filename+"&val="+val,"FunctionalLocation",500,500,"yes","no");
        }
        // Notify ScriptManager that this is the end of the script.
//if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();

