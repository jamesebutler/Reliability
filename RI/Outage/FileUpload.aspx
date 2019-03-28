<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="FileUpload.aspx.vb" Inherits="_FileUpload" %>
<%@ MasterType VirtualPath="~/RI.master" %>

<asp:Content ID="_cphMain" ContentPlaceHolderID="_cphMain" runat="Server">
    <script language="javascript" type="text/javascript" for="window" event="onunload">
<!--
    return window_onunload()
    // -->
    </script>

    <script language="javascript" type="text/javascript">
<!--

    function window_onunload() {
        try{
            //alert(gNewMOCFlag);
            //if (gNewMOCFlag = 'Y')
            {
                window.opener.location.href = "entermoc.aspx?mocnumber=" + gMOCNumber;
                //window.opener.location.reload();
            }}
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
	  
    var MultiFileUpload = {
        toggleAttachments:function(fileId,linkId,value) 
        {
            var fileId = document.getElementById(fileId);
            var linkId = document.getElementById(linkId);
            if (fileId!=null && linkId!=null){
                if (value==1){
                    fileId.style.display='none';
                    linkId.style.display='';
                }
                else{
                    fileId.style.display='';
                    linkId.style.display='none';
                }
            }
        }
    }
    // -->
    </script>

    <contenttemplate>
    <br />
    <asp:Panel ID="_pnlAttachments" runat="server">

        <asp:RadioButtonList ID="_rblFileAttachments" RepeatColumns="2" runat="server" RepeatDirection="horizontal">
        <asp:ListItem Text="<%$RIResources:Shared, File Attachments%>" Value="1" Selected="true"></asp:ListItem>
        <asp:ListItem Text="<%$RIResources:Shared, Link Attachments%>" Value="2"></asp:ListItem>
    </asp:RadioButtonList>
    <asp:Panel ID="_pnlFileAttachments" GroupingText="" runat="server" ScrollBars="None">
        <div id="_divFileAttachments" runat="server">
            <asp:Table ID="_tblFileAttachments" runat="server" BorderWidth="0" CellSpacing="1"
                CellPadding="0" Width="950px">
                <%--Width="900px"--%>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                        <asp:Label ID="_lblFileAttachmentInstructions" Text="<%$RIResources:Shared, To attach a file to this record. Select a file, enter a description and click Upload File.%>"
                            runat="server" Font-Bold="true"></asp:Label><br />
                        <br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="150px">
                        <asp:Label ID="_lblFileToUpload" runat="server" Text="<%$RIResources:Shared, FileToUpload%>" /></asp:TableCell>
                    <asp:TableCell>
                        <asp:FileUpload ID="fileUpEx" Style="cursor:pointer;"  runat="server" Height="30px" Width="600px" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="_lblFileDescription" runat="server" Text="<%$RIResources:Shared, Description%>"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="1">
                        <IP:AdvancedTextBox ExpandHeight="true" ID="_txtFileDescription" runat="server" Width="98%"
                            Style="max-width: 600px" TextMode="MultiLine" Rows="4" MaxLength="500" Wrap="true"></IP:AdvancedTextBox>
                    </asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" ColumnSpan="2">
                        <asp:button ID="_btnUpload" onclientclick="BeginPostBack()" runat="server" OnClick="btnUpload_Click" Text="<%$RIResources:Shared,Upload %>" /> 
                        <asp:Button ID="_btnUploadFile" runat="server" Text="<%$RIResources:Shared, Upload File%>"
                            Width="100px" Visible="false" />&nbsp;<asp:Button ID="_btnCancel" runat="server" Text="<%$RIResources:Shared, Cancel%>"
                                OnClientClick="$(window.location).attr('href', window.location); return false;" />&nbsp;&nbsp;<IP:SpellCheck
                                    ID="SpellCheck1" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                        <asp:Label ID="_lblFileUploadStatus" runat="server"></asp:Label><br />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </asp:Panel>
    <asp:Panel ID="_pnlLinkAttachments" runat="server" GroupingText="">
        <div id="_divLinkAttachments" runat="server">
            <asp:Table ID="Table1" runat="server" BorderWidth="0" CellSpacing="1" CellPadding="0"
                Width="650px">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                        <asp:Label ID="_lblLinkInstructions" Text="<%$RIResources:Shared, To attach a link to this record. Enter a link, enter a description and click Attach Link.%>"
                            runat="server" Style="text-align: center"></asp:Label><br />
                        <br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="150px">
                        <asp:Label ID="_lblLinkToAttach" runat="server" Text="<%$RIResources:Shared, URL%>" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <%--<asp:TextBox ID="_txtLinkToAttach" runat="server"></asp:TextBox>--%>
                        <IP:AdvancedTextBox ExpandHeight="true" ID="_txtLinkToAttach" runat="server" Width="98%"
                            Style="max-width: 600px" TextMode="MultiLine" Rows="2" MaxLength="500" Wrap="true"></IP:AdvancedTextBox>
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="_lblLinkDescription" runat="server" Text="<%$RIResources:Shared, Description%>"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="1">
                        <%--<asp:TextBox ID="_txtLinkDescription" runat="server"></asp:TextBox>--%>
                        <IP:AdvancedTextBox ExpandHeight="true" ID="_txtLinkDescription" runat="server" Width="98%"
                            Style="max-width: 600px" TextMode="MultiLine" Rows="4" MaxLength="500" Wrap="true"></IP:AdvancedTextBox>
                    </asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center">
                        <asp:Button ID="_btnAttachLink" runat="server" Text="<%$RIResources:Shared, Attach Link%>" OnClick="btnUploadLink_Click"/>&nbsp;<asp:Button
                            ID="_btnCancelLink" runat="server" Text="<%$RIResources:Shared, Cancel%>" OnClientClick="$(window.location).attr('href', window.location); return false;" />&nbsp;&nbsp;<IP:SpellCheck
                                ID="_btnSpellCheck" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                        <asp:Label ID="_lblLinkStatus" runat="server"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </asp:Panel>
</asp:Panel>

    <br /> 
    <asp:label ID="_lblStatus" runat="server"></asp:label><br />
    <asp:Label ID="_lblFileStatus" runat="server" ></asp:Label>
    <br />
    <br />
    <asp:datagrid id="_dlFileList" runat="server" Width="90%" BackColor="#CCCC99" AutoGenerateColumns="false" DataKeyField="filename" 
        HeaderStyle-ForeColor="black"   >
        <Columns>		
		<asp:TemplateColumn ItemStyle-HorizontalAlign="Left" HeaderText="<%$RIResources:Shared,Filename/Link %>"
	        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="45%" HeaderStyle-Font-Bold="true">
			<ItemTemplate>			                          
                <a target="_blank" href="<%# IIf(Eval("savedFileName").ToString() = "", GetFileLocation("", Eval("Location")), Eval("savedFileName"))%>"><%#Eval("FileName")%></a><br />
<%--                GetFileLocation(Eval("savedFileName"),Eval("Location")))%>">ttt</a><br />
--%>  			    
			</ItemTemplate>
        </asp:TemplateColumn> 
		<asp:boundcolumn DataField="Description" HeaderText="<%$RIResources:Shared,Description %>">
		    <itemstyle width="45%" />
		    <HeaderStyle Font-Bold="True" />
		</asp:boundcolumn>
		<asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
			<ItemTemplate>
                <asp:button id="_lnkBtnDelete" runat="server" CommandName="Delete" Text="<%$RIResources:Shared,Delete %>"></asp:button>
			</ItemTemplate>
		</asp:TemplateColumn>
      </Columns>
    </asp:datagrid>
    <br />
</contenttemplate>
</asp:Content>
