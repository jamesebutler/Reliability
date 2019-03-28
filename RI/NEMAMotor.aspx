<%@ Page Language="VB" MasterPageFile="~/NEMA.master" AutoEventWireup="false" CodeFile="NEMAMotor.aspx.vb"
    Inherits="NEMAMotor" Title="Untitled Page" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="ri/User Controls/common/ucDiagram.ascx" TagName="ucDiagram" TagPrefix="IP" %>
<%@ MasterType VirtualPath="~/NEMA.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">

    <script type="text/javascript">
//Override the BeginRequest and EndRequest that exists in the MasterPage.js file
function BeginRequest(){
    Page_HasBeenSubmitted=true;
	cursor_wait();
	//document.getElementById('ctl00__imbBusy').click();	
	//DisableSubmitButtons();	
}
function EndRequest(){ 
	//DisplayPositionArrow();
	//EnableSubmitButtons();
	cursor_clear();
	//document.getElementById('ctl00__btnCloseBusy').click();
	Page_HasBeenSubmitted=false;
} 

function DisplayPositionArrow(){
	var btn = $get('<%=btnNemaMove.clientid %>');
	if (btn!=null){btn.click();}
	var btn2 = $get('<%=btnAboveNemaMove.clientid %>');
	if (btn2!=null){btn2.click();}
}

function ActiveTabChanged(sender, e) {
		var activeTab = sender.get_activeTab();
		
		if (activeTab!=null){
			if (activeTab._tabIndex==0){
				var btn = $get('<%=btnNemaMove.clientid %>');
				if (btn!=null){btn.click();}
			}
			else {
				var btn2 = $get('<%=btnAboveNemaMove.clientid %>');
				if (btn2!=null){btn2.click();}
			}
		}
    }
    
    function VerifyCurrency(field){
        if (field.value<=0||field.value.length==0){
            alert('A Repair Cost value that is <0 will default to 0.');
        }
    }
    
    function VerifyEfficiency(field){
        if (field.value<60){
            alert('Efficiency should be >= 60');
        }
    }
    function parseDecimal(field)
      {
            var currency = /^\d*(?:\.\d{0,2})?$/;
            var onlyCurrency = /^(\d*(?:\.\d{0,2})?)[\s\S]*$/;
            
            if(!currency.test(field.value))
            {
                  field.value = field.value.substr(0,field.value.length-1);
            }
      }
function displayViewListWindow(url,title){
	if (url!=null){
			var win=dhtmlmodal.open("viewlist", "iframe", url, title, "width=850px,height=350px,resize=1,scrolling=1,center=1", "recal");		
	}
} 
    </script>

    <%--<Asp:UpdatePanel ID="_udpTabNEMA" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <center>
        <Asp:UpdatePanel ID="_udpNema" runat="server" UpdateMode="always">
            <ContentTemplate>
                <ajaxToolkit:TabContainer ID="_tabNEMA" runat="server" AutoPostBack="false" ActiveTabIndex="1" CssClass="yui"
                    OnClientActiveTabChanged="ActiveTabChanged">
                    <ajaxToolkit:TabPanel ID="_tabAboveNEMA" runat="server" HeaderText="Above NEMA Frame Motor Repair vs. Replace Model">
                        <ContentTemplate>
                            <table width="100%" border="0" cellpadding="2">
                                <tr>
                                    <td width="469px" height="600px">
                                        <h2>
                                            Above NEMA Frame Motor<br />Repair vs Replace Model</h2>                                       
                                        <IP:ucDiagram ID="_ucDiagramAboveNema" runat="server" CurrentMotorType="AboveNEMA" /><br />
                                        <%--<div style="text-align:left">*If the motor efficiency is not stated on the old motor nameplate, the energy hurdle evaluation cannot be performed.  Unless the motor energy efficiency can be determined or estimated, the old motor should be considered of lower efficiency than a new motor.  This should be taken into consideration when evaluating if the motor should be repaired or replaced.</div>--%>
                                    </td>
                                    <td width="*">
                                        <div class="noprint">
                                            <asp:Image runat="server" ID="_imgAboveNemaDiagram" ImageUrl="~/NEMA/NEMA2_files/ANema_Steps0.jpg"
                                                AlternateText="Above Nema" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="_tabRegularNEMA" runat="server" HeaderText="NEMA Frame Motor Repair vs. Replace Model">
                        <ContentTemplate>
                            <table width="100%" border="0" cellpadding="2">
                                <tr>
                                    <td width="469px" height="600px">
                                        <h2>
                                            NEMA Frame Motor Repair vs Replace Model
                                            </h2>
                                        
                                        <IP:ucDiagram ID="_ucDiagram" runat="server" Visible="true" CurrentMotorType="NEMA" />
                                    </td>
                                    <td width="*">
                                        <div class="noprint">
                                            <asp:Image runat="server" ID="_imgNemaDiagram" ImageUrl="~/NEMA/NEMA2_files/Nema_Steps0.jpg"
                                                AlternateText="Nema" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
                </center>
                <Asp:UpdatePanel ID="_udpNemaMove" runat="server" UpdateMode="always">
                    <ContentTemplate>
                        <input type="button" id="btnNemaMove" name="btnNemaMove" runat="server" style="display: none"
                            value="move" />
                        <input type="button" id="btnAboveNemaMove" name="btnAboveNemaMove" runat="server"
                            style="display: none" value="move" />
                            <%--<asp:Button ID="_btnShowData" runat="server" Text="NEMA Data" />--%>
                    </ContentTemplate>
                </Asp:UpdatePanel>
            </ContentTemplate>
        </Asp:UpdatePanel>
        
        <%-- </ContentTemplate>
    </Asp:UpdatePanel>--%>
</asp:Content>
