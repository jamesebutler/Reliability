// JScript File


// Compare two options within a list by VALUES

function compareOptionValues(a, b) 
{ 
  // Radix 10: for numeric values
  // Radix 36: for alphanumeric values
  var sA = parseInt( a.value, 36 );  
  var sB = parseInt( b.value, 36 );  
  return sA - sB;
}

// Compare two options within a list by TEXT
function compareOptionText(a, b) 
{ 
  // Radix 10: for numeric values
  // Radix 36: for alphanumeric values
  var sA = parseInt( a.text, 36 );  
  var sB = parseInt( b.text, 36 );  
  return sA - sB;
}

function selectAll(srcList,destList,allHidden,selectedHidden){
   srcList = document.getElementById(srcList);
   destList = document.getElementById(destList);
   allHidden = document.getElementById(allHidden);
   selectedHidden = document.getElementById(selectedHidden);
   
    allHidden.value="";
    selectedHidden.value="";
    for( var i = 0; i< srcList.options.length ; i++) 
      { 
        if ( srcList.options[i] != null )  
        {
         srcList.options[i].selected = true;//==true;
         allHidden.value+=","+srcList.options[i].value;      
        }
      }
      for( var i = 0; i< destList.options.length ; i++) 
      { 
        if ( destList.options[i] != null ) 
        {
         destList.options[i].selected = true;//==true;
         selectedHidden.value+=","+destList.options[i].value;
        }
      }
     /* allHidden = allHidden.substr(1);
      selectedHidden = selectedHidden.substr(1);
      alert(allHidden);
      alert(selectedHidden);*/
}
// Dual list move function
function moveDualList( srcList, destList, moveAll, sortList ) 
{
  // Do nothing if nothing is selected
  if (  ( srcList.selectedIndex == -1 ) && ( moveAll == false )   )
  {
    return;
  }
  newDestList = new Array( destList.options.length );
  var len = 0;

  for( len = 0; len < destList.options.length; len++ ) 
  {
    if ( destList.options[ len ] != null )
    {
      newDestList[ len ] = new Option( destList.options[ len ].text, destList.options[ len ].value, destList.options[ len ].defaultSelected, destList.options[ len ].selected );
    }
  }

  for( var i = 0; i < srcList.options.length; i++ ) 
  { 
    if ( srcList.options[i] != null && ( srcList.options[i].selected == true || moveAll ) )
    {
       // Statements to perform if option is selected
       // Incorporate into new list
       newDestList[ len ] = new Option( srcList.options[i].text, srcList.options[i].value, srcList.options[i].defaultSelected, srcList.options[i].selected );
       len++;
    }
  }

  // Sort out the new destination list
  //newDestList.sort( compareOptionValues );   // BY VALUES
  //newDestList.sort( compareOptionText );   // BY TEXT

  // Populate the destination with the items from the new array
  for ( var j = 0; j < newDestList.length; j++ ) 
  {
    if ( newDestList[ j ] != null )
    {
      destList.options[ j ] = newDestList[ j ];
      destList.options[j].selected=false;
    }
  }

  // Erase source list selected elements
  for( var i = srcList.options.length - 1; i >= 0; i-- ) 
  { 
    if ( srcList.options[i] != null && ( srcList.options[i].selected == true || moveAll ) )
    {
       // Erase Source
       //srcList.options[i].value = "";
       //srcList.options[i].text  = "";
       srcList.options[i]       = null;
    }
  }
  
  if (sortList==true){
    //sortTheList(destList);
    sortSelect(destList,true);
  }
} // End of moveDualList()

function move(f,bDir,sName) {
  //var el = f.elements["ro_lst" + sName]
  var el = sName//f.elements["this.form."+sName]
  
  var idx = el.selectedIndex
  if (idx==-1) 
    alert("You must first select the item to reorder.")
  else {
    var nxidx = idx+( bDir? -1 : 1)
    if (nxidx<0) nxidx=el.length-1
    if (nxidx>=el.length) nxidx=0
    var oldVal = el[idx].value
    var oldText = el[idx].text
    el[idx].value = el[nxidx].value
    el[idx].text = el[nxidx].text
    el[nxidx].value = oldVal
    el[nxidx].text = oldText
    el.selectedIndex = nxidx
  }
}

function sortFuncAsc(record1, record2) {
            var value1 = record1.optText.toLowerCase();
            var value2 = record2.optText.toLowerCase();
            if (value1 > value2) return(1);
            if (value1 < value2) return(-1);
            return(0);
        }
function sortSelect(selectToSort, ascendingOrder) {
            if (arguments.length == 1) ascendingOrder = true;    // default to ascending sort

            // copy options into an array
            var myOptions = [];
            for (var loop=0; loop<selectToSort.options.length; loop++) {
                myOptions[loop] = { optText:selectToSort.options[loop].text, optValue:selectToSort.options[loop].value };
            }

            // sort array
            if (ascendingOrder) {
                myOptions.sort(sortFuncAsc);
            } else {
                myOptions.sort(sortFuncDesc);
            }

            // copy sorted options from array back to select box
            selectToSort.options.length = 0;
            for (var loop=0; loop<myOptions.length; loop++) {
                var optObj = document.createElement('option');
                optObj.text = myOptions[loop].optText;
                optObj.value = myOptions[loop].optValue;
                selectToSort.options.add(optObj);
            }
        }

function sortTheList(srcList) {
//var lb = document.getElementById('mylist');
arrTexts = new Array();
arrValues = new Array();

for(i=0; i<srcList.length; i++)  {
  arrTexts[i] = srcList.options[i].text;
  arrValues[i] = srcList.options[i].value;
}

arrTexts.sort();

for(i=0; i<srcList.length; i++)  {
  srcList.options[i].text = arrTexts[i];
  srcList.options[i].value = arrTexts[i];
  srcList.options[i].selected=false;
}
}


//  End -->
// Notify ScriptManager that this is the end of the script.
//if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();

