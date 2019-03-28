<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="Multilingual.aspx.vb"
    Inherits="Multilingual" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">
    <Asp:UpdatePanel ID="_udpanel" runat="server" UpdateMode="conditional">
        <ContentTemplate>
            <table width="100%" cellpadding="2" cellspacing="2">
                <tr class="BorderSecondary">
                    <td colspan="2">
                        <IP:StartEndCalendar ID="StartEndCalendar1" ShowTime="true" runat="server" />
                    </td>
                </tr>
                <tr class="Border">
                    <td style="width: 50%; vertical-align: top">
                        <asp:Calendar ID="_cal" Width="400px" runat="server"></asp:Calendar>
                    </td>
                    <td style="width: 50%">
                        <IP:SwapListBox ID="_swaplist" runat="server" />
                    </td>
                </tr>
                <tr class="BorderSecondary">
                    <td style="width: 50%; vertical-align: top">
                        <asp:ListBox ID="_lblBusinessunit" runat="server" Height="100"></asp:ListBox><br />
                        <asp:TextBox ID="_txtCostField" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 50%">
                        <asp:DropDownList ID="_ddlAreas" runat="server">
                        </asp:DropDownList>
                        <asp:CheckBoxList ID="_cblAreas" runat="server">
                        </asp:CheckBoxList>
                        <asp:RadioButtonList ID="_rblAreas" runat="server">
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr class="Border" valign="top">
                    <td colspan="2">
                        <asp:GridView Width="100%" CssClass="Border" BorderColor="Black" BorderWidth="2px"
                            ID="_gv" runat="server" AutoGenerateColumns="False" DataKeyNames="RINUMBER" EnableViewState="False"
                            AllowSorting="False" ShowFooter="True">
                            <Columns>
                                <asp:TemplateField HeaderText="<%$RIResources:Shared,EventDate %>" SortExpression="EVENTDATE">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("EVENTDATE") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Width="100px" Text='<%#Convert.ToDateTime(Eval("EVENTDATE")).ToShortDateString( )%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SITENAME" HeaderText="<%$RIResources:Shared,Site %>" SortExpression="SITENAME" />
                                <asp:BoundField DataField="RISUPERAREA" HeaderText="<%$RIResources:Shared,BusinessUnit %>" SortExpression="RISUPERAREA" />
                                <asp:BoundField DataField="SUBAREA" HeaderText="<%$RIResources:Shared,Area %>" SortExpression="SUBAREA" />
                                <asp:BoundField DataField="AREA" HeaderText="<%$RIResources:Shared,Line %>" SortExpression="AREA" />
                                <asp:BoundField DataField="RINUMBER" HeaderText="<%$RIResources:Shared,RINumber %>" SortExpression="RINUMBER" />
                                <asp:HyperLinkField DataTextField="INCIDENT" HeaderText="<%$RIResources:Shared,IncidentTitle %>" SortExpression="INCIDENT"
                                    DataNavigateUrlFields="RINUMBER" DataNavigateUrlFormatString="~/RI/EnterNewRI.aspx?RINumber={0}"
                                    Target="_self" />
                                <asp:HyperLinkField DataTextField="RCFA_TYPE" HeaderText="<%$RIResources:Shared,TypeExecutiveSummary%>"
                                    SortExpression="RCFA_TYPE" DataNavigateUrlFields="RINUMBER" DataNavigateUrlFormatString="../../CEReporting/frmCrystalReport.aspx?Report=ExecutiveSummary&amp;RINumber={0}"
                                    Target="_blank" />
                                <asp:BoundField DataField="COST" HeaderText="<%$RIResources:Shared,Cost %>" SortExpression="COST" 
                                    HtmlEncode="False">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TOTCOST" HeaderText="<%$RIResources:Shared,FinancialImpact %>" SortExpression="TOTCOST"
                                    DataFormatString="{0:c0}" HtmlEncode="False">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle HorizontalAlign="Right" Font-Bold="True" />
                        </asp:GridView>
                    </td>
                </tr>
                <%--<tr class="Border" valign="top">
                    <td>
                        &lt;<b>IP:DisplayExcel</b> ID=_displayExcel runat=server <i>ButtonText="Display Excel"
                            ShowButton=true</i> /&gt;</td>
                    <td>
                        <b>DisplayExcel:</b>&nbsp;The DisplayExcel control will export a dataset or OracleDataReader
                        to excel.<br />
                        <b>ButtonText</b>&nbsp;(string) - Text that will be displayed on the button.<br />
                        <b>ShowButton</b>&nbsp;(boolean) - Determine if the DisplayExcel button should be
                        displayed. (Default is false)<br />
                        <pre>
'Here's an example of how to use the control
Protected Sub _displayExcel_DisplayExcel_Click() _
	Handles _displayExcel.DisplayExcel_Click
	Dim sql As String = Resources.Sql.sqlFacility
	Dim ds As System.Data.DataSet = Nothing
	ds = RI.SharedFunctions.GetOracleDataSet(sql)
	Me._displayExcel.DisplayExcel(ds)
	ds = Nothing
End Sub
                </pre>
                    </td>
                </tr>--%>
                <tr class="BorderSecondary">
                    <td colspan="2">
                        <IP:DisplayExcel ID="_displayExcel" runat="server" ButtonText="<%$RIResources:Global,DisplayExcel %>"
                            ShowButton="true" />
                        <asp:Button ID="_btnExportResources" runat="server" Text="<%$RIResources:ExportResources %>" />
                        <asp:HyperLink ID="_lnkResourceFile" Target="_blank" runat="server"></asp:HyperLink>
                    </td>
                </tr>
            </table>
         
        </ContentTemplate>
    </Asp:UpdatePanel>
</asp:Content>
