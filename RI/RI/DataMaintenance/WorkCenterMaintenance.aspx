<%@ Page Language="VB" Masterpagefile="~/RI.master" AutoEventWireup="false" CodeFile="WorkCenterMaintenance.aspx.vb"
    Inherits="RI_DataMaintenance_WorkCenter" Title="WorkCenter" %>


<%@ MasterType VirtualPath="~/RI.master" %> 
<%@ Register Namespace="RealWorld.Grids" TagPrefix="rwg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="_CntMain" ContentPlaceHolderID="_cphMain" runat="Server">
    <Asp:UpdatePanel id="_udpWorkCenter" runat="server" updatemode="Always">
        <ContentTemplate>

           <asp:Panel ID="_pnlDataManagement" runat="server">
                <table id="_tblFilter">
                    <tr>
                        <td>
                            <asp:Label ID="_lblFacility" runat="server" Text="<%$RIResources:Shared,Facility%>"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="_ddlFacility" runat="server" AutoPostBack="true" >
                            </asp:DropDownList></td>
                    </tr>
                </table>
                <asp:GridView ID="_gvMain" runat="server" AutoGenerateColumns="false" Width='90%' 
                    DataKeyNames="plantcode,busunit,busunit_code"
                    OnRowCancelingEdit="_gvMain_RowCancelingEdit" 
                    OnRowDataBound="_gvMain_RowDataBound" 
                    ShowFooter="True" 
                    OnRowCommand="_gvMain_RowCommand" 
                    OnRowDeleting="_gvMain_RowDeleting">
 
                    <HeaderStyle BackColor="#CCCC99" ForeColor="Black" Wrap="False" />
                    <RowStyle CssClass="BorderSecondary" VerticalAlign="Top" />
                    <AlternatingRowStyle CssClass="Border" VerticalAlign="Top" />
                    <EmptyDataRowStyle CssClass="BorderSecondary" VerticalAlign="Top" />
                    <FooterStyle CssClass="BorderSecondary" VerticalAlign="Top"  />
                    
                    <Columns>
                  
                        <%--<asp:TemplateField ShowHeader="false">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnSelect" runat="server" CausesValidation="False" CommandName="Edit"
                                    ImageUrl="~/images/pencil.png" ToolTip="<%$ RIResources:Shared,Edit %>" CommandArgument="<%# Container.DataItemIndex  %>" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField ItemStyle-Width="40%" ItemStyle-Height="15pt" HeaderText="<%$ RIResources:Shared,businessunit %>" SortExpression="plantcode">
                            <EditItemTemplate> 
                                <asp:Label ID="_txtBusUnit" runat="server" Text='<%# Bind("busunit") %>'></asp:Label> 
                            </EditItemTemplate> 
                            <FooterTemplate> 
                                <asp:DropDownList ID="_ddlNewBusUnit" runat="server" ></asp:DropDownList> 
                            </FooterTemplate> 
                            <ItemTemplate>
                                <asp:Label ID="_lblBusUnit" runat="server" Text='<%# (container.dataitem("busunit")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="<%$ RIResources:Shared,Business Unit Code%>" SortExpression="busunit_code" ItemStyle-Width="25%">
                            <EditItemTemplate> 
                                <asp:Label ID="_txtBusUnitCode" runat="server" Text='<%# Bind("busunit_code") %>'></asp:Label> 
                            </EditItemTemplate> 
                            <ItemTemplate>
                                <asp:Label ID="_lblBusUnitCode" runat="server" ToolTip='<%# Bind("busunit_code") %>' Text='<%# Bind("busunit_code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                       
                        <asp:TemplateField HeaderText="<%$ RIResources:Shared,Work Center %>" SortExpression="work_center" ItemStyle-Width="25%">
                            <EditItemTemplate> 
                                <asp:TextBox ID="_txtWorkCenter" runat="server" Text='<%# Bind("work_center") %>'></asp:TextBox> 
                            </EditItemTemplate> 
                            <FooterTemplate> 
                                <asp:TextBox ID="_txtNewWorkCenter" runat="server" MaxLength="8" ></asp:TextBox> 
                            </FooterTemplate> 
                            <ItemTemplate>
                                <asp:Label ID="_lblWorkCenter" runat="server" ToolTip='<%# Bind("work_center") %>' Text='<%# Bind("work_center") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" HeaderStyle-HorizontalAlign="Left"> 
                <ItemTemplate> 
<%--                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton> 
--%>                    <asp:LinkButton ID="_lnkDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton> 
                </ItemTemplate> 
                 <EditItemTemplate> 
                    <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton> 
                    <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton> 
                </EditItemTemplate> 
                <FooterTemplate> 
                    <asp:LinkButton ID="_lnkAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Insert"></asp:LinkButton> 
                </FooterTemplate> 
           </asp:TemplateField> 
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="_lblNoRecordsFound" runat="server" Text='<%$ RIResources:Shared,NoRecordsFound %>'></asp:Label>
            <br />
            <asp:Label ID="_lbEmptyBUA" runat="server" text="<%$ RIResources:Shared,businessunit %>" ></asp:Label>
            <asp:DropDownList ID="_ddlNewBusUnit" runat="server" ></asp:DropDownList> 
            <asp:Label ID="_lbEmptyWC" runat="server" text="<%$ RIResources:Shared,Work Center%>" ></asp:Label>
            <asp:TextBox ID="_txtNewWorkCenter" runat="server"  MaxLength="8"  ></asp:TextBox> 
            <asp:Button ID="_btnInsert" runat="Server" Text="Insert" CommandName="EmptyInsert" UseSubmitBehavior="False" />
        </EmptyDataTemplate>
        </asp:GridView>
                <%--<center>
                    <asp:Button ID="_btnUpdate" Text="<%$RIResources:Shared,Update%>" runat="server" />
                    &nbsp;
                    <asp:Button ID="_btnCancel" runat="server" Text="<%$RIResources:Shared,Cancel%>" /></center>--%>
            </asp:Panel>
        </ContentTemplate>
    </Asp:UpdatePanel>
</asp:Content>
