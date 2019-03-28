// JScript File
function GetNewMotorPrice(clientID){
		try{
			var _hp = eval('ddlHP_'+clientID);
			var _rpm = eval('ddlRPM_'+clientID);
			var _motorType = eval('motorType_'+clientID);
			if (_hp!=null && _rpm!=null && _motorType!=null){
				if (_hp.selectedIndex>0 && _rpm.selectedIndex>0)
					var ret = NEMA.GetNewMotorPrice(_motorType,_hp.value,_rpm.value,clientID, OnMotorPriceComplete, OnMotorPriceTimeOut, OnMotorPriceError);
			}
			return(true);
		}
		catch(err){
	
		}
	}

function OnMotorPriceTimeOut(arg) {
alert("TimeOut encountered when calling Say Hello.");
}

function OnMotorPriceError(arg) {
alert("Error encountered when calling Say Hello.");
}

function OnMotorPriceComplete(arg) {
	try{	
		var txtNewMotorPrice = eval('txtNewMotorPrice_'+arg.ClientID);
		var txtOldEfficiency = eval('txtOldEfficiency_'+arg.ClientID);
		var txtNewEfficiency = eval('txtNewEfficiency_'+arg.ClientID);
		
		if (txtNewMotorPrice!=null){
			txtNewMotorPrice.value=addCommas(arg.Price.toFixed(2));
		}
		if (txtOldEfficiency!=null){
			txtOldEfficiency.value=arg.Efficiency900;
		}
		
		if (txtNewEfficiency!=null){
			txtNewEfficiency.value=arg.Efficiency;
		}
	}
	catch(err){

	}
}
function addCommas(nStr)
{
	nStr += '';
	x = nStr.split('.');
	x1 = x[0];
	x2 = x.length > 1 ? '.' + x[1] : '';
	var rgx = /(\d+)(\d{3})/;
	while (rgx.test(x1)) {
		x1 = x1.replace(rgx, '$1' + ',' + '$2');
	}
	return x1 + x2;
}

function validateEfficiency(sender, args){
    if (args.Value>60 || args.value==0){
        args.IsValid=true;
        sender.IsValid=true;}
    else{
        args.Value=0;
        args.IsValid = false;
        sender.IsValid=false;}
    return
}
