<%@ Page Title="" Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="LongRunningScript.aspx.vb" Inherits="LongRunningScript" %>

<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" Runat="Server">
<script type='text/javascript' src='http://code.jquery.com/jquery-1.5.2.js'></script>
<script type='text/javascript'>//<![CDATA[ 
$(window).load(function(){
 var repeat = 0;
 var timeout = 0;

 function longRun() {
     for (var i = 0; i < 20000; i++) {
         //nothing
     }
     repeat++;
     $('#log').append('<div><SELECT ID="TESTSEL' + repeat + '"><OPTION>SELECT #' + repeat + '</OPTION></SELECT><SELECT ID="TESTSEL' + repeat + '"><OPTION>SELECT #' + repeat + '</OPTION></SELECT><SELECT ID="TESTSEL' + repeat + '"><OPTION>SELECT #' + repeat + '</OPTION></SELECT></div>');


 }

 function heavyTask() {
     if (repeat < 2000) {
         y = longRun();

         if (timeout == 25) {
             timeout = 0;
             setTimeout(heavyTask, 1000);
         } else {
             timeout++;
             heavyTask();
         }
     } else {
         $('#test').html("done!");
     }
 }

 heavyTask();
});//]]>  

</script>
<div id="test"></div>
<div id="log"></div>
</asp:Content>

