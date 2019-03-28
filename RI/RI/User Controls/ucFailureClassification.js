// JScript File

function GetClassificationCriticality(){
    if (rblCriticalityRating!=null){
        var value=0;
        //score = score + rblConstrainedArea.value + rblCriticalityRating.value + rblLifeExpectancy.value + rblEquipmentCare.value;
        
        for (i=0;i<5;i++){
            var cr = document.getElementById(rblCriticalityRating.id + "_"+i);
            if (cr!=null){if(cr.checked==true)value+=parseInt(cr.value)}
        }
     }   
    return value;
}
function GetClassificationConstrainedArea(){
    if (rblConstrainedArea!=null){
        var value=0;
        //score = score + rblConstrainedArea.value + rblCriticalityRating.value + rblLifeExpectancy.value + rblEquipmentCare.value;
        
        for (i=0;i<5;i++){
            var ca = document.getElementById(rblConstrainedArea.id + "_"+i);
            if (ca!=null){if(ca.checked==true)value+=parseInt(ca.value)}
        }
     }   
     return value
}
function SetClassificationCriticality(value){
    if (rblCriticalityRating!=null){
        for (i=0;i<5;i++){
            var cr = document.getElementById(rblCriticalityRating.id + "_"+i);
            if (cr!=null){if(cr.value==value)cr.checked=true}
        }
     }   
     CalculateClassificationScore();
}
function SetClassificationConstrainedArea(value){
    if (value=="Constrained Area")
        value=5
    else 
        value=3
    if (rblConstrainedArea!=null){
        for (i=0;i<5;i++){
            var ca = document.getElementById(rblConstrainedArea.id + "_"+i);
            if (ca!=null){if(ca.value==value)ca.checked=true}
        }
     }   
     CalculateClassificationScore();
}
function CalculateClassificationScore(){    
        if (divFailureClassPointerT1!=null && divFailureClassPointerT2!=null && divFailureClassPointerT3!=null && rblConstrainedArea!=null && rblCriticalityRating!=null && rblLifeExpectancy!=null && rblEquipmentCare!=null && lblClassificationTierValue!=null){
            var score=0;
            //score = score + rblConstrainedArea.value + rblCriticalityRating.value + rblLifeExpectancy.value + rblEquipmentCare.value;
            
            for (i=0;i<5;i++){
            var ca = document.getElementById(rblConstrainedArea.id + "_"+i);
            var cr = document.getElementById(rblCriticalityRating.id + "_"+i);
            var le = document.getElementById(rblLifeExpectancy.id + "_"+i);
            var ec = document.getElementById(rblEquipmentCare.id + "_"+i);
            
            if (ca!=null){if(ca.checked==true)score+=parseInt(ca.value)}
            if (cr!=null){if(cr.checked==true)score+=parseInt(cr.value)}
            if (le!=null){if(le.checked==true)score+=parseInt(le.value)}
            if (ec!=null){if(ec.checked==true)score+=parseInt(ec.value)}
           
        }   
        divFailureClassPointerT1.style.display='none';
        divFailureClassPointerT2.style.display='none';
        divFailureClassPointerT3.style.display='none';     
        if (score>=19){
            divFailureClassPointerT1.style.display='';
            //lblClassificationTierValue.outerText=TierOneFailure;
            lblClassificationTierValue.innerHTML=TierOneFailure;
        } 
        else if(score>=14){
            divFailureClassPointerT2.style.display='';
            //lblClassificationTierValue.outerText=TierTwoFailure;
            lblClassificationTierValue.innerHTML=TierTwoFailure;
        }
        else if (score>=8){
            divFailureClassPointerT3.style.display='';
            //lblClassificationTierValue.outerText=TierThreeFailure;
            lblClassificationTierValue.innerHTML=TierThreeFailure;
        }
        else{
            //I'm not 100% sure that we should display the arrow when the score is less than 8
            divFailureClassPointerT3.style.display='';
            //lblClassificationTierValue.outerText=TierThreeFailure;
            lblClassificationTierValue.innerHTML=TierThreeFailure;
        }
        if (totalClassifiactionValue!=null) 
            totalClassifiactionValue.value = score;
                          
    }
}