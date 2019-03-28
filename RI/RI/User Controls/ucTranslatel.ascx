<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucTranslatel.ascx.vb"
    Inherits="RI_User_Controls_ucTranslatel" %>

<script type="text/javascript" src="http://www.google.com/jsapi"></script>

<script type="text/javascript">
    google.load("language", "1");
    //google.setOnLoadCallback(init);

    function init() {
      var src = document.getElementById('<%=src.clientid %>');
      var dst = document.getElementById('<%=dst.clientid %>');
      var i=0;
      for (l in google.language.Languages) {
        var lng = l.toLocaleUpperCase();
        var lngCode = google.language.Languages[l];
        if (lngCode=='en' || lngCode=='ru'){
            if (google.language.isTranslatable(lngCode)) {
                src.options.add(new Option(lng, lngCode));
                dst.options.add(new Option(lng, lngCode));
            }
        }
      }
      src.selectedIndex=0;
      dst.selectedIndex=1;      
    }
     function translateText() {
      var value = document.getElementById('<%=source.clientid%>').value;
      var src = document.getElementById('<%=src.clientid%>').value;
      var dest = document.getElementById('<%=dst.clientid%>').value;
      google.language.translate(value, src, dest, translateResult);
      return false;
    }
        

    function translateResult(result) {
      var resultBody = document.getElementById("results_body");
      if (result.translation) {
        resultBody.innerHTML = result.translation;
      } else {
        resultBody.innerHTML = '<span style="color:red">Error Translating</span>';
      }
    }
function displayTranslationWindow(objName){
    var obj = document.getElementById(objName);
    var source=document.getElementById('<%=source.clientid%>');
    var trigger = document.getElementById('<%=_imgTriggerTranslation.clientid%>');
    if (obj!=null && source!=null){
        source.value  = obj.value;
    }
    trigger.click();
    
    //url=url+"?source="+source;
    //displayModalPopUpWindow(url,"Translation","Translation",800,200);
}
</script>

<asp:Panel ID="_pnlTranslation" runat="server" CssClass="modalPopup" style="display:none;">
    <table width="100%">
        <tr>
            <td style="width: 50%">
                <IP:AdvancedTextBox ID="source" runat="server" Width="100%" Wrap="true" ExpandHeight="false" Height="150px"
                    TextMode="multiLine"></IP:AdvancedTextBox>
            </td>
            <td style="width:5%">&nbsp;</td>
            <td valign="top" style="width: 45%">
                <b>
                    <asp:Label ID="_lblTranslation" runat="server" Text="<%$RIResources:Shared,Translation%>"></asp:Label>:</b><br />
                <div id="results_body">
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="3" nowrap="nowrap">
                <%--<asp:Label ID="Label1" runat="server" Text="<%$RIResources:Shared,From%>"></asp:Label>:</b>--%>
                <select name="src" id="src" runat="server">
                </select>
                <b>
                    <asp:Label ID="Label2" runat="server" Text=" >> "></asp:Label></b>
                <select name="dst" id="dst" runat="server">
                </select>
                <asp:Button ID="_btnSubmit" Text="<%$RIResources:Shared,Translate%>" runat="server"
                    OnClientClick="return translateText();" />
            </td>
        </tr>
        <tr>
            <td colspan="3" align="right">
                <asp:Button ID="_btnCloseTranslation" runat="server" Text='<%$RIResources:Shared,Close %>' /></td>
        </tr>
    </table>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="_mpeTranslation" runat="server" TargetControlID="_imgTriggerTranslation"
    PopupControlID="_pnlTranslation" BackgroundCssClass="modalBackground" DropShadow="true"
    OkControlID="_btnCloseTranslation" CancelControlID="_btnCloseTranslation">
</ajaxToolkit:ModalPopupExtender>
<div visible="false" style="display: none; visibility: hidden">
    <asp:ImageButton ImageUrl="~/Images/question.gif" runat="server" ID="_imgTriggerTranslation"
        Visible="true" />
</div>
