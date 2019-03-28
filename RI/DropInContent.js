
/********************************************
*DropInContent.js
*This script has been created to allow for scheduled messages to be displayed within a webpage
*Last Modified by: Michael Pope 2/2/2007 - Updated code to show examples of use and to separate the configuration
				code from the functional code
*********************************************/

/*Example usage */
/*
<style>
	#dropinboxv2cover{
	width: 340px; //change width to desired 
	height: 120px;  //change height to desired. REMOVE if you wish box to be content's natural height 
	position:absolute; //Don't change below 4 rules
	z-index: 100;
	overflow:hidden;
	visibility: hidden;
	}

	#dropinboxv2{
	width: 330px; //change width to above width-20. 
	height: 100px; //change height to above height-20. REMOVE if you wish box to be content's natural height
	border: 2px solid cccccc; //Customize box appearance
	background-color: lightyellow;
	padding: 4px;
	position:absolute; //Don't change below 3 rules 
	left: 0;
	top: 0;
}
</style>

//<script src="dropincontent.js"></script>
//<script language=javascript>displaymsg('Website name','Leave this parameter blank to use the defaultWebsiteMsg');</script>
*/



 
   

var msgEndDate=new Date(2008,1,4,18,00);	//Note: the month is 0 based (0 to 11)
var msgStartDate=new Date(2008,1,3,01,00);    //Javascript dates  (yyyy,mm,dd,hh,mm,ss)
var defaultWebsiteMsg = 'Please be advised that the <b>{website}</b> will be unavailable on <b>Monday March 3, 2008 between 3:00pm (cst) and 3:30pm (cst)</b> for scheduled maintenance.';
//var defaultWebsiteMsg = 'Please be advised that the <b>{website}</b> will not be available on <b>' + getString(msgStartDate) + ' ' + getTime(msgStartDate)   + ' and ' + getString(msgEndDate) + ' ' + getTime(msgEndDate)  + '</b> for scheduled maintenance.';
var dropboxleft=150 //set left position of box (in px)
var dropboxtop=2 //set top position of box (in px)
var dropspeed=30 //set speed of drop animation (larger=faster)
var displaymode="always" 
//Specify display mode. 3 possible values are:
//1) "always"- This makes the fade-in box load each time the page is displayed
//2) "oncepersession"- This uses cookies to display the fade-in box only once per browser session
//3) integer (ie: 5)- Finally, you can specify an integer to display the box randomly via a frequency of 1/integer...
// For example, 2 would display the box about (1/2) 50% of the time the page loads.


/***********************************************
* Amazon style Drop-in content box- © Dynamic Drive DHTML code library (www.dynamicdrive.com)
* Visit DynamicDrive.com for hundreds of DHTML scripts
* This notice must stay intact for legal use
* Go to http://www.dynamicdrive.com/ for full source code
***********************************************/

////////////////////////////Don't edit beyond here///////////////////////////////////////////////


if (parseInt(displaymode)!=NaN)
var random_num=Math.floor(Math.random()*displaymode)
var ie=document.all
var dom=document.getElementById

function initboxv2(previewmode){
if (previewmode!=true) previewmode=false
if (MessageCanBeDisplayed(msgStartDate,msgEndDate)==false && previewmode==false)	return	
		
if (!dom&&!ie)
return

crossboxcover=(dom)?document.getElementById("dropinboxv2cover") : document.all.dropinboxv2cover
crossbox=(dom)?document.getElementById("dropinboxv2"): document.all.dropinboxv2
scroll_top=(ie)? truebody().scrollTop : window.pageYOffset
crossbox.height=crossbox.offsetHeight
crossboxcover.style.height=parseInt(crossbox.height)+"px"
crossbox.style.top=crossbox.height*(-1)+"px"
crossboxcover.style.left=dropboxleft+"px"
crossboxcover.style.top=dropboxtop+"px"
crossboxcover.style.visibility=(dom||ie)? "visible" : "show"
dropstart=setInterval("dropinv2()",50)
}

function dropinv2(){
scroll_top=(ie)? truebody().scrollTop : window.pageYOffset
if (parseInt(crossbox.style.top)<0){
crossboxcover.style.top=scroll_top+dropboxtop+"px"
crossbox.style.top=parseInt(crossbox.style.top)+dropspeed+"px"
}
else{
clearInterval(dropstart)
crossbox.style.top=0
}
}

function dismissboxv2(){
if (window.dropstart) clearInterval(dropstart)
crossboxcover.style.visibility="hidden"
}

function truebody(){
return (document.compatMode && document.compatMode!="BackCompat")? document.documentElement : document.body
}

function get_cookie(Name) {
var search = Name + "="
var returnvalue = ""
if (document.cookie.length > 0) {
offset = document.cookie.indexOf(search)
if (offset !== -1) {
offset += search.length
end = document.cookie.indexOf(";", offset)
if (end === -1)
end = document.cookie.length;
returnvalue=unescape(document.cookie.substring(offset, end))
}
}
return returnvalue;
}


if (displaymode=="oncepersession" && get_cookie("droppedinv2")=="" || displaymode=="always" || parseInt(displaymode)!=NaN && random_num==0){

if (window.addEventListener)
window.addEventListener("load", initboxv2, false)
else if (window.attachEvent)
window.attachEvent("onload", initboxv2)
else if (document.getElementById || document.all)
window.onload=initboxv2
if (displaymode=="oncepersession")
document.cookie="droppedinv2=yes"
}

//Desc: MessageCanBeDisplayed determines if the message box should be displayed
//Return: True - Show message, False - Hide message
function MessageCanBeDisplayed(startDate,endDate){
	var mydate=new Date();
	//alert('mydate='+mydate+ '\nstDate='+startDate+'\nexDate='+endDate);
	if((mydate < endDate) && (mydate > startDate))
		return true
	else
		return false
}

function makeArray() {
  var args = makeArray.arguments;
  for (var i = 0; i < args.length; i++) {
    this[i] = args[i];
  }
  this.length = args.length;
}

function fixDate(date) {
  var base = new Date(0);
  var skew = base.getTime();
  if (skew > 0)
    date.setTime(date.getTime() - skew);
}

function isDate(sDate) {
	var scratch = new Date(sDate);
	if (scratch.toString() === "NaN") {
		return false;
	} 
	else {
		return true;
	}
}

function getString(date) {
  var months = new makeArray("January", "February",
"March", "April", "May", "June", "July", "August",
"September", "October", "November", "December");
  return months[date.getMonth()] + " " + date.getDate() +
", " + ((date.getYear() < 100) ? "19" : "") + date.getYear();
}

function getTime(date){
	var hours = date.getHours();
	var minutes = date.getMinutes();
	var seconds = date.getSeconds();
	
	
	if(hours.length==1) hr = " 0";
	if(minutes.lenth==1) min="0";
	if(seconds.length==1) sec="0 ";
	
	hours = date.getHours();
	if (hours >= 12) {
		hours = (hours === 12) ? 12 : hours - 12; mm = " PM";
	}
	else {
		hours = (hours === 0) ? 12 : hours; mm = " AM";
	}
	minutes = date.getMinutes();
	if (minutes < 10){
		mytime = ":0" + minutes;
	}
	else {
		mytime = ":" + minutes;
	};
	return hours + mytime+ mm;
}

//Desc: Displays the provided message or the default message
// - This is the only function that should be called from the webpage
// - message will only be displayed if MessageCanBeDisplayed is true
function displaymsg(website,msg,startDate,endDate,writeOutDiv){
	var div;	
	startDate= eval(startDate);
	endDate=eval(endDate);
	if (isDate(startDate) && isDate(endDate)){
		msgStartDate = startDate;
		msgEndDate = endDate;
	}
	if(website.length==0)website='RI Website';
	if(msg.length==0){
		msg = defaultWebsiteMsg.replace(/{website}/g,website);	
	}
	
	buildMsg(msg,writeOutDiv);	
}
function buildMsg(msg,writeOutDiv){
	var div;
	var leftOuterDiv;
	var rightOuterDiv;
	outerDiv='<div id="dropinboxv2cover"><div id="dropinboxv2">';
	div=msg;	
	div+='<p align="right"><input type="button" onClick="dismissboxv2();return false" id="_btnCloseIt" name="_btnCloseIt" value="Close" class="Buttonhover" style="cursor: hand;" /></p>';
	//div+='<p align="right"><a href="#" style="color:black;" onClick="dismissboxv2();return false"><b>Close It</b></a></p>';	
	rightOuterDiv='</div></div>';	
	if (writeOutDiv==null||writeOutDiv==true){
		document.write(outerDiv);
		document.write (div);	
		document.write(rightOuterDiv);
	}
	return div; 
}