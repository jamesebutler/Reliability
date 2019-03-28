// -------------------------------------------------------------------
// DHTML Modal window- By Dynamic Drive, available at: http://www.dynamicdrive.com
// v1.0: Script created Feb 27th, 07'
// v1.01 May 5th, 07' Minor change to modal window positioning behavior (not a bug fix)
// REQUIRES: DHTML Window Widget (v1.01 or higher): http://www.dynamicdrive.com/dynamicindex8/dhtmlwindow/
// -------------------------------------------------------------------

if (typeof dhtmlwindow=="undefined")
alert('ERROR: Modal Window script requires all files from "DHTML Window widget" in order to work!')

try{
var dhtmlmodal={
veilstack: 0,
_saveDesableSelect : new Array(),
open:function(t, contenttype, contentsource, title, attr, recalonload){
	var d=dhtmlwindow //reference dhtmlwindow object	
	this.interVeil=document.getElementById("interVeil") //Reference "veil" div
	this.veilstack++ //var to keep track of how many modal windows are open right now
	this.loadveil()
	this.hideSelect()
	if (typeof recalonload!="undefined" && recalonload=="recal" && d.scroll_top==0)
		d.addEvent(window, function(){dhtmlmodal.loadveil()}, "load")
		var t=d.open(t, contenttype, contentsource, title, attr, recalonload)
	t.controls.firstChild.style.display="none" //Disable "minimize" button	
	t.controls.lastChild.style.display="none"
	t.controls.onclick=function(){dhtmlmodal.forceclose(this._parent);dhtmlmodal.close(t)} //OVERWRITE default control action with new one
	t.buttons.onclick=function(){dhtmlmodal.forceclose(this._parent);dhtmlmodal.close(t)} //OVERWRITE default control action with new one
	t.show=function(){dhtmlmodal.show(this)} //OVERWRITE default t.show() method with new one
	t.hide=function(){dhtmlmodal.close(this)} //OVERWRITE default t.hide() method with new one
	t.onclose=function(){dhtmlmodal.forceclose(this._parent)} //OVERWRITE default t.hide() method with new one	
return t
},


loadveil:function(){
	var d=dhtmlwindow
	d.getviewpoint()
	this.docheightcomplete=(d.standardbody.offsetHeight>d.standardbody.scrollHeight)? d.standardbody.offsetHeight : d.standardbody.scrollHeight
	this.interVeil.style.width=d.docwidth+"px" //set up veil over page
	this.interVeil.style.height=this.docheightcomplete+"px" //set up veil over page
	this.interVeil.style.left=0 //Position veil over page
	this.interVeil.style.top=0 //Position veil over page
	this.interVeil.style.visibility="visible" //Show veil over page
	this.interVeil.style.display="block" //Show veil over page
},

adjustveil:function(){ //function to adjust veil when window is resized
	if (this.interVeil && this.interVeil.style.display=="block") //If veil is currently visible on the screen
		this.	loadveil() //readjust veil
},


close:function(t){ //user initiated function used to close modal window
	try{
		t.contentDoc=(t.contentarea.datatype=="iframe")? window.frames["_iframe-"+t.id].document : t.contentarea //return reference to modal window DIV (or document object in the case of iframe
	}
	catch (e){
	
	}
	
	var closewinbol=dhtmlwindow.close(t)
	if (closewinbol){ //if var returns true
		this.veilstack--
		if (this.veilstack==0) //if this is the only modal window visible on the screen, and being closed
			this.interVeil.style.display="none"
 }
},

forceclose:function(t){ //function attached to default "close" icon of window to bypass "onclose" event, and just close window
	dhtmlwindow.rememberattrs(t) //remember window's dimensions/position on the page before closing
	t.style.display="none"
	this.veilstack--
		if (this.veilstack<=0) //if this is the only modal window visible on the screen, and being closed
			this.interVeil.style.display="none"
	this.showSelect()
},

show:function(t){
	dhtmlmodal.veilstack++
	dhtmlmodal.loadveil()
	dhtmlwindow.show(t)
},

hideSelect:function(){
	//IE6 Bug with SELECT element always showing up on top
        i = 0;
        if ((Sys.Browser.agent === Sys.Browser.InternetExplorer) && (Sys.Browser.version < 7)) {
            //Save SELECT in PopUp
//            var tagSelectInPopUp = new Array();
//            for (var j = 0; j < this._tagWithTabIndex.length; j++) {
//                tagElements = this._foregroundElement.getElementsByTagName('SELECT');
//                for (var k = 0 ; k < tagElements.length; k++) {
//                    tagSelectInPopUp[i] = tagElements[k];
//                    i++;
//                }
//            }

            i = 0;
            this._saveDesableSelect = new Array();
            tagElements = document.getElementsByTagName('SELECT');
            for (var k = 0 ; k < tagElements.length; k++) {
                //if (tagSelectInPopUp.indexOf(tagElements[k]) == -1)  {					
                    this._saveDesableSelect[i] = {tag: tagElements[k], visib: tagElements[k].disabled} ;
                    //tagElements[k].disabled = true;
                    tagElements[k].style.visibility = 'hidden';
                    // END EDE
                    i++;
                //}
            }
        }
},
showSelect:function(){
	//IE6 Bug with SELECT element always showing up on top
        if ((Sys.Browser.agent === Sys.Browser.InternetExplorer) && (Sys.Browser.version < 7)) {
            tagElements = document.getElementsByTagName('SELECT');
            for (var k = 0 ; k < this._saveDesableSelect.length; k++) {
                // EDE
                //this._saveDesableSelect[k].tag.style.visibility = this._saveDesableSelect[k].visib;
                //this._saveDesableSelect[k].tag.disabled = this._saveDesableSelect[k].visib;
                //tagElements[k].disabled = false;
                tagElements[k].style.visibility = 'visible';
                // END EDE
            }
        }
}
} //END object declaration
}
catch(ex){
alert('error:'+ex);
}

//Code below has been added to the master page
document.write('<div id="interVeil"></div>')
dhtmlwindow.addEvent(window, function(){if (typeof dhtmlmodal!="undefined") dhtmlmodal.adjustveil()}, "resize")