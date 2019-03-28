// JScript File

var MOCSwapList =  {   
    // Compare two options within a list by VALUES    
        compareOptionValues:function(a,b)
        { 
          // Radix 10: for numeric values
          // Radix 36: for alphanumeric values
          var sA = parseInt( a.value, 36 );  
          var sB = parseInt( b.value, 36 );  
          return sA - sB;
        },

        // Compare two options within a list by TEXT
        compareOptionText:function(a,b)
        { 
          // Radix 10: for numeric values
          // Radix 36: for alphanumeric values
          var sA = parseInt( a.text, 36 );  
          var sB = parseInt( b.text, 36 );  
          return sA - sB;
        },

        //function selectAll(lbList1,lbList2,allHidden,selectedHidden)
        selectAll:function(lbList1,lbList2,lbList3,lbList4,lbList5,allHidden,selectedHidden,secondarySelectedHidden,L3SelectedHidden,endorserSelectedHidden)
        {
           lbList1 = document.getElementById(lbList1);
           lbList2 = document.getElementById(lbList2);
           lbList3 = document.getElementById(lbList3);
           lbList4 = document.getElementById(lbList4);
           lbList5 = document.getElementById(lbList5);
           //lbList6 = document.getElementById(lbList6);
           //lbList7 = document.getElementById(lbList7);
           allHidden = document.getElementById(allHidden);
           selectedHidden = document.getElementById(selectedHidden);
           secondarySelectedHidden = document.getElementById(secondarySelectedHidden);
           L3SelectedHidden = document.getElementById(L3SelectedHidden);
           //L4SelectedHidden = document.getElementById(L4SelectedHidden);
           //L5SelectedHidden = document.getElementById(L5SelectedHidden);
           endorserSelectedHidden = document.getElementById(endorserSelectedHidden);
           
            allHidden.value="";
            selectedHidden.value="";
            secondarySelectedHidden.value="";
            L3SelectedHidden.value="";
            //L4SelectedHidden.value="";
            //L5SelectedHidden.value="";
            endorserSelectedHidden.value="";
            
            for( var i = 0; i< lbList1.options.length ; i++) 
              { 
                if ( lbList1.options[i] !== null )  
                {
                 //lbList1.options[i].selected = true;//==true;
                 allHidden.value+=","+lbList1.options[i].value;      
                }
              }
              for( var i = 0; i< lbList2.options.length ; i++) 
              { 
                if ( lbList2.options[i] !== null ) 
                {
                 lbList2.options[i].selected = true;//==true;
                 selectedHidden.value+=","+lbList2.options[i].value;
                }
              }
              for (var i=0; i<lbList3.options.length; i++)
              {
                if (lbList3.options[i] !== null)
                {
                    lbList3.options[i].selected = true;
                    secondarySelectedHidden.value+=","+lbList3.options[i].value;
                }
              }
              for (var i=0; i<lbList4.options.length; i++)
              {
                if (lbList4.options[i] !== null)
                {
                    lbList4.options[i].selected = true;
                    L3SelectedHidden.value+=","+lbList4.options[i].value;
                }
              }
//              for (var i=0; i<lbList5.options.length; i++)
//              {
//                if (lbList5.options[i] !== null)
//                {
//                    lbList5.options[i].selected = true;
//                    L4SelectedHidden.value+=","+lbList5.options[i].value;
//                }
//              }
//              for (var i=0; i<lbList6.options.length; i++)
//              {
//                if (lbList6.options[i] !== null)
//                {
//                    lbList6.options[i].selected = true;
//                    L5SelectedHidden.value+=","+lbList6.options[i].value;
//                }
//              }
              for (var i=0; i<lbList5.options.length; i++)
              {
                if (lbList5.options[i] !== null)
                {
                    lbList5.options[i].selected = true;
                    endorserSelectedHidden.value+=","+lbList5.options[i].value;
                }
              }
             /* allHidden = allHidden.substr(1);
              selectedHidden = selectedHidden.substr(1);
              alert(allHidden);
              alert(selectedHidden);*/
        },
   
        
        // Dual list move function
        moveDualList:function( lbList1, lbList2, moveAll, sortList ) 
        {
          // Do nothing if nothing is selected
          if (  ( lbList1.selectedIndex == -1 ) && ( moveAll == false )   )
          {
            return;
          }

          // Need arrays to determine which items to move and remove
          removeList2 = new Array();
          // Array to get what is already in existing list
          newlbList2 = new Array( lbList2.options.length );
          var len = 0;

          for( len = 0; len < lbList2.options.length; len++ ) 
          {
            if ( lbList2.options[ len ] !== null )
            {
              newlbList2[ len ] = new Option( lbList2.options[ len ].text, lbList2.options[ len ].value, lbList2.options[ len ].defaultSelected, lbList2.options[ len ].selected );
            }
          }

          for( var i = 0; i < lbList1.options.length; i++ ) 
          { 
                //continue if option selected
                if ( lbList1.options[i].selected == true )
                {
                    // get the value of the option that was selected
                    lbList1Value = lbList1.options[i].value;
                    var lbList1ValueSplit = lbList1Value.split("/");
                    
                    // The Role Seq id is in the 2nd position of the array
                    var role = lbList1ValueSplit[1];
                       
                    // if value is numeric, then it's a role
                    if (isNaN(role) == false)
                    {
                        var a = 0;
                        newList2 = new Array();
          
                        for( var j = 0; j < lbList1.options.length; j++ ) 
                        { 
                            var lbList1ValueSplit = lbList1.options[j].value.split("/");
                            var role2 = lbList1ValueSplit[1];
                            //alert(role2);
                            
                            if (role == role2)
                            {
                                //lbList1.options[j].selected = true;
                                newList2[a] = new Option( lbList1.options[j].text, lbList1.options[j].value);
                                lbList1.options[j].selected = true;  
                                removeList2[a] = new Option(lbList1.options[j].index, lbList1.options[j].value)
                                         
                                len++;
                                a++;
                             }
                                        //}
                        }
                    }
                    else
                    {
                        newList2 = new Array();
                        if ( lbList1.options[i] !== null && ( lbList1.options[i].selected == true || moveAll ) )
                        {
                            // Statements to perform if option is selected
                            // Incorporate into new list
                            //// if ( lbList1.options[i].text.indexOf("*") == -1 ) 
                            //// {
          
                            //newlbList2[len] = var role = lbList1.options[i].value
                       
                            // if one of the values in the selected item is numeric, then it's a role
                            if (isNaN(role) !== false)
                            {
                                newlbList2[len] = new Option( lbList1.options[i].text, lbList1.options[i].value, lbList1.options[i].defaultSelected, lbList1.options[i].selected );
                                len++;
                            }
                            //lbList1.options[i] = null;
                            ////}
                        }
                    }
                }
            }

        // Sort out the new destination list
        //newlbList2.sort( compareOptionValues );   // BY VALUES
        //newlbList2.sort( compareOptionText );   // BY TEXT

        // Populate the destination with the items from the new array
        comboList = newlbList2.concat(newList2);
        //else
        //{ comboList = newlbList2;
        //}
        for ( var j = 0; j < comboList.length; j++ ) 
        {
            if ( comboList[j] !== null )
            {
                lbList2.options[j] = comboList[j];
                //lbList2.options[j].selected=false;
            }
        }

        // Erase source list selected elements
        for ( var i = lbList1.options.length - 1; i >= 0; i-- ) 
        { 
                if ( lbList1.options[i].text.indexOf("*") == -1 ) 
                {
                    if ( lbList1.options[i] !== null && ( lbList1.options[i].selected == true || moveAll ) )
                {
                    // Erase Source
                    lbList1.options[i]       = null;
                }
            }
        }
          
          //for (var b = removeList2.length; b>=0; b--)
          //{
          //lbList1.options[b]       = null;
          //}
          if (sortList==true){
            //sortTheList(lbList2);
            //MOCSwapList.sortSelect(lbList2,true);
          }
        }, // End of moveDualList()

        //function move(f,bDir,sName) 
        move:function(f,bDir,sName)
        {
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
        },

        //function sortFuncAsc(record1, record2) 
        sortFuncAsc:function(record1,record2)
        {
            var value1 = record1.optText.toLowerCase();
            var value2 = record2.optText.toLowerCase();
            if (value1 > value2) return(1);
            if (value1 < value2) return(-1);
            return(0);
        },
    
    
        //function sortSelect(selectToSort, ascendingOrder) 
        sortSelect:function(selectToSort,ascendingOrder)
        {
            if (arguments.length == 1) ascendingOrder = true;    // default to ascending sort

            // copy options into an array
            var myOptions = [];
            for (var loop=0; loop<selectToSort.options.length; loop++) {
                myOptions[loop] = { optText:selectToSort.options[loop].text, optValue:selectToSort.options[loop].value };
            }

            // sort array
            if (ascendingOrder) {
                myOptions.sort(MOCSwapList.sortFuncAsc);
            } else {
                myOptions.sort(MOCSwapList.sortFuncDesc);
            }

            // copy sorted options from array back to select box
            selectToSort.options.length = 0;
            for (var loop=0; loop<myOptions.length; loop++) {
                var optObj = document.createElement('option');
                optObj.text = myOptions[loop].optText;
                optObj.value = myOptions[loop].optValue;
                selectToSort.options.add(optObj);
            }
        },


        //function sortTheList(lbList1) {
        sortTheList:function(lbList1)
        {
            //var lb = document.getElementById('mylist');
            arrTexts = new Array();
            arrValues = new Array();

            for(i=0; i<lbList1.length; i++)  {
              arrTexts[i] = lbList1.options[i].text;
              arrValues[i] = lbList1.options[i].value;
            }

            arrTexts.sort();

            for(i=0; i<lbList1.length; i++)  {
                  lbList1.options[i].text = arrTexts[i];
                  lbList1.options[i].value = arrTexts[i];
                  lbList1.options[i].selected=false;
            }
        },
        
        // Dual list move ALL function
        moveAllDualList:function( lbList1, lbList2, moveAll, sortList ) 
        {
          // Do nothing if nothing is selected
          if (  ( lbList1.selectedIndex == -1 ) && ( moveAll == false )   )
          {
            return;
          }

          newlbList2 = new Array( lbList2.options.length );
          var len = 0;

          for( len = 0; len < lbList2.options.length; len++ ) 
          {
            if ( lbList2.options[ len ] !== null )
            {
              newlbList2[ len ] = new Option( lbList2.options[ len ].text, lbList2.options[ len ].value, lbList2.options[ len ].defaultSelected, lbList2.options[ len ].selected );
            }
          }

          for( var i = 0; i < lbList1.options.length; i++ ) 
          { 
            if ( lbList1.options[i] !== null && ( lbList1.options[i].selected == true || moveAll ) )
            {
               // Statements to perform if option is selected
               // Incorporate into new list
               newlbList2[ len ] = new Option( lbList1.options[i].text, lbList1.options[i].value, lbList1.options[i].defaultSelected, lbList1.options[i].selected );
               len++;
            }
          }

          // Populate the destination with the items from the new array
          for ( var j = 0; j < newlbList2.length; j++ ) 
          {
            if ( newlbList2[ j ] !== null )
            {
              lbList2.options[ j ] = newlbList2[ j ];
              lbList2.options[j].selected=false;
            }
          }

          // Erase source list selected elements
          for( var i = lbList1.options.length - 1; i >= 0; i-- ) 
          { 
            if ( lbList1.options[i].text.indexOf("*") == -1 ) 
                {
                 if ( lbList1.options[i] !== null && ( lbList1.options[i].selected == true || moveAll ) )
            {
               // Erase Source
               //lbList1.options[i].value = "";
               //lbList1.options[i].text  = "";
               lbList1.options[i]       = null;

            }}
          }
          
          if (sortList==true){
            //sortTheList(lbList2);
            MOCSwapList.sortSelect(lbList2,true);
          }
        } 
        // End of moveAllDualList()
    };

//  End -->
// Notify ScriptManager that this is the end of the script.
//if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();

