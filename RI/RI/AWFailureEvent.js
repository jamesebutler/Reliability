// JScript File

       
function ShowNewModeTextBox(type) {       
    //Create an input type dynamically.     
    var element = document.createElement("input");       
    
    //Assign different attributes to the element.     
    element.setAttribute("type", type);     
    element.setAttribute("value", type);     
    element.setAttribute("name", type);         
    
    var foo = document.getElementById("fooBar");       
    //alert(foo);
    //Append the element in page (in span).     
    
    foo.appendChild(element);   
} 

function ShowNextTextBox(currTB,nextTB) {       
    //Check if currentTB has text    
    var Textbox_Curr = document.getElementById(currTB);
    var Textbox_Next = document.getElementById(nextTB);
    
    if ( Textbox_Curr.value != "" )
    {
       Textbox_Next.style.display = "block";
    }  
    
    //var foo = document.getElementById("fooBar");       
    //alert(foo);
    //Append the element in page (in span).     
    
    //foo.appendChild(element);   
} 


function DisableEnable(objddl,objtb) {
        var DropDown_Event = document.getElementById(objddl);
        var Textbox_Total = document.getElementById(objtb);
        //var DropDown_Date = document.getElementById("<%= DropDown_Date.ClientID %>")
        //var Textbox_Date = document.getElementById("<%= Textbox_Date.ClientID %>")


//alert(Textbox_Total.value);
//alert(DropDown_Event.value);
        //DropDown_Event.disabled = true;
        
        if (Textbox_Total.value != "") {
            DropDown_Event.disabled = true;
        }

        else {
            DropDown_Event.disabled = false;
        }
        
        if (DropDown_Event.value != "") {

            Textbox_Total.disabled = true;
        }

        else {
            Textbox_Total.disabled = false;
        }

        
    }

function Reviewed(ctrl, UserName) {        
var isCheck = ctrl.checked;        
WebService.GetFailureModeCause(UserName,isCheck);    }
        