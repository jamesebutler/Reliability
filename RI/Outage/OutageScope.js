// JScript File

    function ConfirmDelete(OutageScopeSeqId,rowNum, obj){
        if(OutageScopeSeqId!=null){
            var msg = localizedText.ConfirmDelete;
            var actionJS = "DeleteGridRow('"+ OutageScopeSeqId+"','" +rowNum+"','"+obj+"');";
	        confirmMessage(msg,'ctl00__ConfirmMessage', actionJS);		
        }
    } 

    function DeleteGridRow(OutageScopeSeqId,rowNum, obj){
        var gvTable = document.getElementById (obj);
        rowNum=parseInt(rowNum)+2;
        if (rowNum<10){rowNum = "0" + rowNum;}
        obj = obj+"_ctl"+rowNum+"_";
        var gv = document.getElementById (obj+"_lblSort");
        if (gv!=null){
            if (gv.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex!=null){
            ret = RISharedWS.DeleteScope(OutageScopeSeqId,OnSucceeded, OnFailed,OnFailed);
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
