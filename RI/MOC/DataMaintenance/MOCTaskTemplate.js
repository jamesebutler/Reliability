// JScript File

    function ConfirmDelete(TaskSeqId,rowNum, obj){
        if(TaskSeqId!=null){
            var msg = localizedText.ConfirmDelete;
            var actionJS = "DeleteGridRow('"+ TaskSeqId+"','" +rowNum+"','"+obj+"');";
	        confirmMessage(msg,'ctl00__ConfirmMessage', actionJS);		
        }
    } 

    function DeleteGridRow(TaskSeqId,rowNum, obj){
        var gvTable = document.getElementById (obj);
        rowNum=parseInt(rowNum)+2;
        if (rowNum<10){rowNum = "0" + rowNum;}
            obj = obj+"_ctl"+rowNum+"_";
        
        var gv = document.getElementById (obj+"_tbTitle");
        if (gv!=null){
           if (gv.parentNode.parentNode.rowIndex!=null){
            //alert(gv.parentNode.parentNode.rowIndex);
            ret = RISharedWS.DeleteMOCTemplateTask(TaskSeqId, OnSucceeded, OnFailed, OnFailed);
                       
            gvTable.deleteRow(gv.parentNode.parentNode.rowIndex);
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
    //alert(arg);
    }
    function OnFailed(arg){
    alert(arg);
    }

  function validateLength(oSrc, args){
   args.IsValid = (args.Value.length >= 1);
}