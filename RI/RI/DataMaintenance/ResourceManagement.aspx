<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="ResourceManagement.aspx.vb"
    Inherits="RI_DataMaintenance_ResourceManagement" Title="Untitled Page" %>


<%@ MasterType VirtualPath="~/RI.master" %> 
<%@ Register Namespace="RealWorld.Grids" TagPrefix="rwg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">
    <Asp:UpdatePanel ID="_udpResources" runat="server" UpdateMode="Always">
        <ContentTemplate>

            <script type="text/javascript">
        function RowChanged(rowNum){
            var obj = document.getElementById ("_lblChanged"+rowNum);
            if (obj!=null){
                obj.style.display=''; 
            }
        }
            function displayResourceEditor(url,title){
	            if (url!=null){
			            var win=dhtmlmodal.open("resourceeditor", "iframe", url, title, "width=800px,height=600px,resize=1,scrolling=1,center=1", "recal");		
	            }
	            win.onclose=function(){ 
	                window.location.reload (true);
	               return true
	            }
            } 
            </script>

            <asp:Panel ID="_pnlDataManagement" runat="server" DefaultButton="_btnSearchFor">
                <table id="_tblFilter">
                    <tr>
                        <td>
                            <asp:Label ID="_lblSearchFor" runat="server" Text="<%$RIResources:Shared,SearchFor%>"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="_txtSearchFor" runat="server" AutoPostBack="false" Width="400px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="_btnSearchFor" runat="server" UseSubmitBehavior="true" Text="<%$RIResources:Shared,Search%>" /></td>
                        <td>
                            <asp:DropDownList ID="_ddlPages" runat="server" AutoPostBack="true">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="_lblFilterByLocale" runat="server" Text="<%$RIResources:Shared,Language%>"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="_ddlLocaleName" runat="server" AutoPostBack="true">
                            </asp:DropDownList></td>
                        <td colspan="2">
                            <asp:CheckBox ID="_cbDisplayNullTranslations" AutoPostBack="true" runat="server"
                                Text="<%$RIResources:Shared,Display Null Translations Only%>" /></td>
                    </tr>
                </table>
                <asp:GridView ID="_gvMain" runat="server" AutoGenerateColumns="False" Width='100%'
                    DataKeyNames="ResourceKey,LOCALEID,RESOURCETYPEID,ID" AutoGenerateEditButton="false"
                    EnableSortingAndPagingCallbacks="false" AllowSorting="true">
                    <HeaderStyle BackColor="#CCCC99" ForeColor="Black" Wrap="False" />
                    <RowStyle CssClass="BorderSecondary" VerticalAlign="Top" />
                    <AlternatingRowStyle CssClass="Border" VerticalAlign="Top" />
                    <EmptyDataRowStyle CssClass="BorderSecondary" VerticalAlign="Top" />
                    <Columns>
                        <asp:TemplateField ShowHeader="false">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnSelect" runat="server" CausesValidation="False" CommandName="Edit"
                                    ImageUrl="~/images/pencil.png" ToolTip="<%$ RIResources:Shared,Edit %>" CommandArgument="<%# Container.DataItemIndex  %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="250px" HeaderText="<%$ RIResources:Shared,Language %>" SortExpression="LocaleName">
                            <ItemTemplate>
                                <asp:Label ID="_lblLocaleName" runat="server" Text='<%# DisplayFullName(container.dataitem("localename")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="<%$ RIResources:Shared,Translated Phrase %>" SortExpression="ResourceValue" ItemStyle-Width="45%">
                            <ItemTemplate>
                                <asp:Label ID="_lblEnglishValue" runat="server" ToolTip='<%# Bind("ResourceKey") %>' Text='<%# Bind("ENGLISHVALUE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                       
                        <asp:TemplateField HeaderText="<%$ RIResources:Shared,Translated Phrase %>" SortExpression="ResourceValue" ItemStyle-Width="45%">
                            <ItemTemplate>
                                <asp:Label ID="_lblTranslatedValue" runat="server" ToolTip='<%# Bind("ResourceKey") %>' Text='<%# Bind("ResourceValue") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="_lblNoRecordsFound" runat="server" Text='<%$ RIResources:Shared,NoRecordsFound %>'></asp:Label>
                    </EmptyDataTemplate>
                </asp:GridView>
                <%--<center>
                    <asp:Button ID="_btnUpdate" Text="<%$RIResources:Shared,Update%>" runat="server" />
                    &nbsp;
                    <asp:Button ID="_btnCancel" runat="server" Text="<%$RIResources:Shared,Cancel%>" /></center>--%>
            </asp:Panel>
            <asp:Panel ID="_pnlEditData" runat="server" Width="700px" CssClass="modalPopup">
                <asp:HiddenField ID="_hdfResourceKey" runat="server" />
                <asp:HiddenField ID="_hdfLocaleID" runat="server" />
                <asp:HiddenField ID="_hdfResourceTypeID" runat="server" />
                <asp:HiddenField ID="_hdfApplicationID" runat="server" />
                <table border="0" width="100%">
                    <tr>
                        <td colspan="2">
                            <IP:Banner ID="Banner3" runat="server" BannerMessage="<%$RIResources:Shared,Translation Management %>"
                                DisplayPopupBanner="true" />
                        </td>
                    </tr>
                    <tr class="BorderSecondary">
                        <td style="min-width: 100px;">
                            <asp:Label ID="_lblEditLocaleName" runat="server" Text="<%$ RIResources:Shared,Language %>"></asp:Label>
                        </td>
                        <td style="width: 90%">
                            <asp:Label ID="_lblEditLocalValue" runat="server"></asp:Label></td>
                    </tr>
                    <tr class="Border">
                        <td valign="top">
                            <asp:Label ID="_lblEditEnglish" runat="server" Text="<%$ RIResources:Shared,English Phrase %>"></asp:Label></td>
                        <td valign="top">
                            <asp:Panel ID="_pnlEnglishValue" Width="100%" BorderWidth="0" runat="server" Height="100px"
                                ScrollBars="auto">
                                <asp:Label ID="_lblEditEnglishValue" runat="server" Text=""></asp:Label></asp:Panel>
                        </td>
                    </tr>
                    <tr class="BorderSecondary">
                        <td valign="top">
                            <asp:Label ID="_lblTranslated" runat="server" Text="<%$ RIResources:Shared,Translated Phrase %>"></asp:Label>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="_txtTranslatedValue" BorderWidth="1" BorderColor="Black" runat="server"
                                Text='<%# Bind("ResourceValue") %>' Width="100%" Height="100px" TextMode="MultiLine" />
                        </td>
                    </tr>
                    <tr class="Border">
                        <td colspan="2" align="center">
                            <asp:Button ID="_btnSave" runat="server" Text="<%$RIResources:Shared,Update%>" /><asp:Button
                                ID="_btnCancel" runat="server" Text="<%$RIResources:Shared,Cancel%>" /></td>
                    </tr>
                </table>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="_mpeExcelSelect" runat="server" TargetControlID="_btnDisplayEdit"
                PopupControlID="_pnlEditData" BackgroundCssClass="modalBackground" DropShadow="true"
                OkControlID="_btnHiddenCancel" CancelControlID="_btnHiddenCancel">
            </ajaxToolkit:ModalPopupExtender>
            <div style="display: none">
                <asp:Button ID="_btnDisplayEdit" runat="server" Text="Edit" />
                <asp:Button ID="_btnHiddenCancel" runat="server" Text="Cancel" />
            </div>
        </ContentTemplate>
    </Asp:UpdatePanel>
</asp:Content>
