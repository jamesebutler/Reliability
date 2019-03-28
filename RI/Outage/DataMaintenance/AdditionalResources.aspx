<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="AdditionalResources.aspx.vb"
    Inherits="RI_DataMaintenance_ResourceManagement" Title="Outage:Additional Resource" %>

<%@ MasterType VirtualPath="~/RI.master" %> 
<%@ Register Namespace="RealWorld.Grids" TagPrefix="rwg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">
    <Asp:UpdatePanel ID="_udpResources" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

        <script language="JavaScript" type="text/javascript">
        <!--
        function ConfirmDelete(resourceseqid,rowNum, obj){
	        if(resourceseqid!=null){
	            var msg = localizedText.ConfirmDelete;
	            var actionJS = "DeleteGridRow('"+ resourceseqid+"','" +rowNum+"','"+obj+"');";
		        confirmMessage(msg,'ctl00__ConfirmMessage', actionJS);		
	        }
        } 

        function DeleteGridRow(resourceseqid,rowNum, obj){
            var gvTable = document.getElementById (obj);
             rowNum=parseInt(rowNum)+2;
            if (rowNum<10){rowNum = "0" + rowNum;}
            obj = obj+"_ctl"+rowNum+"_";
            var gv = document.getElementById (obj+"_lblTblPerson");
            if (gv!=null){
                if (gv.parentNode.parentNode.rowIndex!=null){
                ret = OutageCascadingLists.DeleteResource(resourceseqid,OnSucceeded,OnFailed,OnFailed);
                    gvTable.deleteRow(gv.parentNode.parentNode.rowIndex);
                }
            }
           for (i=1;i<gvTable.rows.length;i++){
                if ((i % 2)==0){
                   gvTable.rows[i].className="Border";
                }
                else{
                gvTable.rows[i].className="BorderSecondary";
                }
            }
            return false;
        }
            
        function OnSucceeded(arg){
            }
        
        function OnFailed(arg){
            }
            
        -->
        </script>
            
            <asp:Panel ID="_pnlDataManagement" runat="server" DefaultButton="_btnAdd">
            <ajaxToolkit:CascadingDropDown ID="_cddlFacility" runat="server" Category="SiteID"
				LoadingText="[Loading Facilities...]" PromptText="    " ServiceMethod="GetFacilityList"
				ServicePath="~/outage/OutageCascadingLists.asmx" TargetControlID="_ddlFacility" ContextKey="All"
				UseContextKey="true">
			</ajaxToolkit:CascadingDropDown>
            <ajaxToolkit:CascadingDropDown ID="_cddlCoordinator" runat="server" Category="Person"
				LoadingText="[Loading People...]" PromptText="   " ServiceMethod="GetPerson"
				ServicePath="~/outage/OutageCascadingLists.asmx" TargetControlID="_ddlPerson" ParentControlID="_ddlFacility">
			</ajaxToolkit:CascadingDropDown>

            <table id="_tblSelection">
                    <tr>
                        <td>
                            <asp:Label ID="_lblFacility" runat="server" Text="<%$RIResources:Shared,SelectFacility%>"></asp:Label></td>
                        <td>
                            <asp:Label ID="_lblPerson" runat="server" Text="<%$RIResources:Shared,Person%>"></asp:Label></td>
                        <td>
                            <asp:Label ID="_lblDiscipline" runat="server" Text="<%$RIResources:Shared,Discipline%>"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="_ddlFacility" runat="server" AutoPostBack="false"></asp:DropDownList></td>
                        <td>
                            <asp:DropDownList ID="_ddlPerson" runat="server" AutoPostBack="false"></asp:DropDownList></td>
                        <td>
                            <asp:TextBox ID="_txtDiscipline" runat="server" AutoPostBack="false" Width="400px"></asp:TextBox></td>
                        <td>
                            <asp:Button ID="_btnAdd" runat="server" UseSubmitBehavior="true" Text="<%$RIResources:Shared,Add%>" />
                        </td>
                    </tr>
                </table>
                
                <RWG:BulkEditGridView ID="_gvMain" runat="server"
                AutoGenerateColumns="False" DataKeyNames="resourceseqid" EnableInsert="False"
                SaveButtonID="" Width="85%" CellPadding="1" EnableViewState="true">
                    <HeaderStyle BackColor="#CCCC99" ForeColor="Black" Wrap="False" HorizontalAlign="Left"/>
                    <RowStyle CssClass="BorderSecondary" VerticalAlign="Top" />
                    <AlternatingRowStyle CssClass="Border" VerticalAlign="Top" />
                    <EmptyDataRowStyle CssClass="BorderSecondary" VerticalAlign="Top" />
                    <columns>
               <%-- <asp:GridView ID="_gvMain" runat="server" AutoGenerateColumns="False" Width='100%'
                    DataKeyNames="resourceseqid" AutoGenerateEditButton="true"
                    EnableSortingAndPagingCallbacks="false" AllowSorting="true">
                    <HeaderStyle BackColor="#CCCC99" ForeColor="Black" Wrap="False" />
                    <RowStyle CssClass="BorderSecondary" VerticalAlign="Top" />
                    <AlternatingRowStyle CssClass="Border" VerticalAlign="Top" />
                    <EmptyDataRowStyle CssClass="BorderSecondary" VerticalAlign="Top" />
                    <Columns>--%>
                        <asp:TemplateField HeaderText="<%$ RIResources:Shared,Person %>" >
                            <edititemtemplate>
                                <asp:label ID="_lblTblPerson" runat="server" Text='<%# Bind("resourcename") %>'></asp:label>
                                <asp:HiddenField ID="_rowChanged" runat="server" OnValueChanged="_gvMain.HandleRowChanged" />
                            </edititemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ RIResources:Shared,Discipline %>" >
                            <edititemtemplate>
                                <asp:TextBox ID="_txtTblDiscipline" runat="server" Width="400px" Text='<%# Bind("Discipline") %>'></asp:textbox>
                            </edititemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <itemtemplate>
                                <asp:button id="_lnkBtnDelete" runat="server" CommandName="Delete" text='<%$riResources:Shared,Delete%>' datatextfield="resourceseqid"></asp:button>
			                </itemtemplate>
                        </asp:TemplateField>  
                    </Columns>
                        <%--<asp:TemplateField ShowHeader="false">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnSelect" runat="server" CausesValidation="False" CommandName="Edit"
                                    ImageUrl="~/images/pencil.png" ToolTip="<%$ RIResources:Shared,Edit %>" CommandArgument="<%# Container.DataItemIndex  %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ RIResources:Shared,Person %>" >
                            <ItemTemplate>
                                <asp:Label ID="_lblLocaleName" runat="server" Text='<%# (container.dataitem("resourcename")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="<%$ RIResources:Shared,Discipline %>">
                            <ItemTemplate>
                                <asp:Label ID="_lblEnglishValue" runat="server" ToolTip='<%# Bind("Discipline") %>' Text='<%# Bind("Discipline") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                       
                        <asp:TemplateField HeaderText="<%$ RIResources:Shared,Comments %>" SortExpression="ResourceValue" ItemStyle-Width="45%">
                            <ItemTemplate>
                                <asp:Label ID="_lblTranslatedValue" runat="server" ToolTip='<%# Bind("ResourceSeqID") %>' Text='<%# Bind("ResourceSeqID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        
                    <EmptyDataTemplate>
                        <asp:Label ID="_lblNoRecordsFound" runat="server" Text='<%$ RIResources:Shared,NoRecordsFound %>'></asp:Label>
                    </EmptyDataTemplate>
                    </RWG:BulkEditGridView>
                <center><asp:Button ID="_btnSave" runat="server" Text="<%$RIResources:Shared,Save%>" /></center>

            </asp:Panel>
        </ContentTemplate>
    </Asp:UpdatePanel>
</asp:Content>
