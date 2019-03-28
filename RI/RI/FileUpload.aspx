<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="FileUpload.aspx.vb"
    Inherits="_RIFileUpload" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="_cphMain" ContentPlaceHolderID="_cphMain" runat="Server">

    <script language="javascript" type="text/javascript">
<!--

function window_onunload() {
	try{
	    BeginPostBack();
		window.opener.updateItemCounts();
		window.close();		
	}
	catch(err){
	}
}

function triggerFileUpload()
	  {
	    var fileUp = document.getElementById("<%=fileUpEx.clientid %>");
		if (fileUp!=null){
		    fileUp.focus();
		    fileUp.click();
		    return true;
		}
	  }
<%--function FileSelected(obj){
    if (obj!=null){
        var txt = document.getElementById("< %=_txtFile.clientid %>");
        if (txt!=null){
            txt.value=obj.value;
            obj.value="";
        }
    }
}--%>
// -->
    </script>

    <Asp:UpdatePanel ID="_udpAttachments" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="_btnUpload" />
        </Triggers>
        <ContentTemplate>
            <br />
            <table style="background-color: #CCCC99; border: 1; width: 90%" cellpadding="2" cellspacing="1">
                <tr>
                    <td colspan="2" align="center">
                        <asp:Label ID="_lblInstructions" runat="server" Text="<%$RIResources:Shared,FileUploadInstructions %>"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 15%" align="right">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$RIResources:Shared,FileToUpload %>" /></td>
                    <td>
                        <div style="position: relative;">
                            <asp:FileUpload ID="fileUpEx" Style="cursor:pointer;"   runat="server" Height="30px" Width="600px" />
                            <br />
                            <div style="position: absolute; top: 0px; left: 0px; z-index: 99;">
                                <%--<asp:TextBox ID="_txtFile" runat="server" Width="520px"></asp:TextBox>--%>
                              <%--  <asp:Button ID="_btnFileUpload" runat="server" Text="<%$RIResources:Shared,Browse %>"
                                    OnClientClick="Javascript:return false;" />--%>
                            </div>
                        </div>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%" align="right">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$RIResources:Shared,Description %>" /></td>
                    <td>
                        <asp:TextBox ID="_txtDescr" runat="server" Width="700px" MaxLength="100"></asp:TextBox></td>
                    <tr align="Center">
                        <td colspan="2" align="center">
                            <%--<asp:Button ID="_btnUpload" OnClientClick="BeginPostBack()" runat="server" OnClick="btnUpload_Click"
                                Text="<%$RIResources:Shared,Upload %>" />
                            <asp:Button ID="_btnClose" runat="server" Text="<%$RIResources:Shared,Close %>" OnClientClick="return window_onunload()" />--%>
                        </td>
                    </tr>
                </tr>
            </table>
        </ContentTemplate>
    </Asp:UpdatePanel>
    <center>
        <asp:Button ID="_btnUpload" OnClientClick="BeginPostBack()" runat="server" OnClick="btnUpload_Click"
            Text="<%$RIResources:Shared,Upload %>" />
        <asp:Button ID="_btnClose" runat="server" Text="<%$RIResources:Shared,Close %>" OnClientClick="BeginPostBack();" /></center>
    <br />
    <Asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always"><ContentTemplate>
    <asp:Label ID="_lblStatus" runat="server"></asp:Label><br />
    <asp:Label ID="_lblFileStatus" runat="server"></asp:Label>
    <br />
    <br />
    <asp:DataGrid ID="_dlFileList" runat="server" Width="90%" BackColor="#CCCC99" AutoGenerateColumns="false"
        DataKeyField="filename" HeaderStyle-ForeColor="black">
        <Columns>
            <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" HeaderText="<%$RIResources:Shared,Filename %>"
                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="45%" HeaderStyle-Font-Bold="true">
                <ItemTemplate>
                    <a href="<%#GetUploadURL(eval("FileName"))%>" target="_blank">
                        <%#eval("FileName") %>
                    </a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="Description" HeaderText="<%$RIResources:Shared,Description %>">
                <ItemStyle Width="45%" />
                <HeaderStyle Font-Bold="True" />
            </asp:BoundColumn>
            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Button ID="_lnkBtnDelete" runat="server" CommandName="Delete" Text="<%$RIResources:Shared,Delete %>">
                    </asp:Button>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
    <br />
    </ContentTemplate> </Asp:UpdatePanel>
</asp:Content>
