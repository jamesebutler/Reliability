<%@ Page Language="VB" AutoEventWireup="false" StylesheetTheme="RIBlackGold" CodeFile="ViewList.aspx.vb"
    Inherits="RI_ViewList2" EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>International Paper: View List</title>
</head>
<body style="margin-left: 0; margin-top: 0">
    <form id="form1" runat="server">
        <asp:Panel ID="_pnlIncidentListing" HorizontalAlign="Center" Width="100%" Visible="false"
            runat="server" ScrollBars="None">
            <asp:GridView Width="100%" style="table-layout:fixed" CssClass="Border" BorderColor="Black" BorderWidth="2"
                ID="_gvIncidentListing" runat="server" AutoGenerateColumns="False" DataKeyNames="RINUMBER"
                EnableViewState="false" AllowSorting="false" ShowFooter="true" EnableSortingAndPagingCallbacks="false">
                <Columns>
                    <%--<asp:BoundField DataField="EVENTDATE" HeaderText="<%$RIResources:Shared,EventDate%>" SortExpression="EVENTDATE"
							DataFormatString="{0:d}" HtmlEncode="false" />--%>
                    <asp:TemplateField HeaderText="<%$RIResources:Shared,EventDate %>" SortExpression="EVENTDATE">
                        <ItemTemplate>
                            <asp:Literal ID="Literal1" runat="server" Text='<%#Convert.ToDateTime(Eval("EVENTDATE")).ToShortDateString( )%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SITENAME" HeaderText="<%$RIResources:Shared,Site%>" SortExpression="SITENAME" />
                    <asp:BoundField DataField="RISUPERAREA" HeaderText="<%$RIResources:Shared,BusinessUnit%>"
                        SortExpression="RISUPERAREA" />
                    <asp:BoundField DataField="SUBAREA" HeaderText="<%$RIResources:Shared,Area%>" SortExpression="SUBAREA" />
                    <asp:BoundField DataField="AREA" HeaderText="<%$RIResources:Shared,Line%>" SortExpression="AREA" />
                    <asp:BoundField DataField="RINUMBER" HeaderText="<%$RIResources:Shared,RINumber%>"
                        SortExpression="RINUMBER" />
                    <%--<asp:HyperLinkField DataTextField="INCIDENT" HeaderText="<%$RIResources:Shared,IncidentTitle%>"
                        SortExpression="INCIDENT" DataNavigateUrlFields="RINUMBER" DataNavigateUrlFormatString="~/RI/EnterNewRI.aspx?RINumber={0}"
                        Target="_top" />--%>
                    <asp:TemplateField HeaderText="<%$RIResources:Shared,IncidentTitle%>">
                        <ItemTemplate>
                            <a target="_top" href="Javascript:viewIncident('<%#string.format(Page.ResolveClientUrl("~/RI/EnterNewRI.aspx?RINumber={0}"),EVAL("RINumber")) %>');">
                                <%#Eval("Incident")%>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$RIResources:Shared,TypeExecutiveSummary%>">
                        <ItemTemplate>
                            <a target="_blank" href="<%#string.format(Page.ResolveClientUrl("../../CEReporting/frmCrystalReport.aspx?Report=ExecutiveSummary&RINumber={0}&LocaleName={1}"),EVAL("RINumber"),System.Threading.Thread.CurrentThread.CurrentCulture) %>">
                                <%#Eval("RCFA_TYPE")%>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:HyperLinkField DataTextField="RCFA_TYPE" HeaderText="<%$RIResources:Shared,TypeExecutiveSummary%>"
                        SortExpression="RCFA_TYPE" DataNavigateUrlFields="RINUMBER" DataNavigateUrlFormatString="//ridev/CEReporting/frmCrystalReport.aspx?Report=ExecutiveSummary&RINumber={0}"
                        Target="_blank" />--%>
                </Columns>
                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
            </asp:GridView>
        </asp:Panel>
    </form> 
   
</body>
</html>
