// JScript File


function GetSubCategory (cblID) {
    //var SiteID = document.getElementById(ApproverSiteID).value;
    //alert(SiteID);
    
    MOCSubCategoryDDL=cblID;
    ret = RIMOCSharedWS.GetSubCategory(44, OnPopulateSubCategory, OnALTimeOut, OnALError);
    }
    
var MOCSubCategoryDDL=null;
function OnPopulateSubCategory(List){
    try{
        //obj= document.getElementById(obj);
        var obj;
            obj=MOCSubCategoryDDL;
            obj= document.getElementById(obj);
            obj.length=0;
        if(obj!=null){
       // var selectedIndex = obj.selectedIndex;        
           // if(obj.length<=2){            
                for (var i=0; i < List.length;++i){
                    var resource = List[i].split('::');
                    addOption(obj, resource[1], resource[0]);
                }
                 return true;
            //}
              return false;
        }
   }   
   catch(e){}
}

function OnALTimeOut(arg) {
	alert("TimeOut OnALTimeOut");
}

function OnALError(arg) {
	alert("Error OnALError");
}


