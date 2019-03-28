<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NEMAData.aspx.vb" Inherits="NEMA_NEMAData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin-top:0; margin-left:0px; ">
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="_pnlNemaData" runat="server" CssClass="modalPopup" Width='99%' 
                ScrollBars="none">
                <asp:GridView ID="_gvNEMAData" runat="server" Width="98%" AllowSorting="True" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="false" ShowEditButton="True" />
                        <asp:BoundField DataField="HP" HeaderText="HP" ReadOnly="True" SortExpression="HP" />
                        <asp:BoundField DataField="SPEED" HeaderText="SPEED" ReadOnly="True" SortExpression="SPEED" />
                        <asp:BoundField DataField="NETPRICE" HeaderText="NETPRICE" SortExpression="NETPRICE" />
                        <asp:BoundField DataField="NEMATYPE" HeaderText="NEMATYPE" ReadOnly="True" SortExpression="NEMATYPE" />
                        <asp:BoundField DataField="EFFICIENCY" HeaderText="EFFICIENCY" SortExpression="EFFICIENCY" />
                        <asp:BoundField DataField="OLDEFFICIENCY" HeaderText="OLDEFFICIENCY" SortExpression="OLDEFFICIENCY" />
                        <asp:BoundField DataField="FRAMESIZE" HeaderText="FRAMESIZE" SortExpression="FRAMESIZE" />
                    </Columns>
                    <RowStyle  CssClass="Border"/>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle  CssClass="BorderSecondary"/>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connectionRCFATST %>"
                    DeleteCommand='DELETE FROM "TBLNEMAPRICING" WHERE "HP" = :original_HP AND "SPEED" = :original_SPEED AND "NEMATYPE" = :original_NEMATYPE AND "NETPRICE" = :original_NETPRICE AND "EFFICIENCY" = :original_EFFICIENCY AND "OLDEFFICIENCY" = :original_OLDEFFICIENCY AND "FRAMESIZE" = :original_FRAMESIZE'
                    InsertCommand='INSERT INTO "TBLNEMAPRICING" ("HP", "SPEED", "NETPRICE", "NEMATYPE", "EFFICIENCY", "OLDEFFICIENCY", "FRAMESIZE") VALUES (:HP, :SPEED, :NETPRICE, :NEMATYPE, :EFFICIENCY, :OLDEFFICIENCY, :FRAMESIZE)'
                    ProviderName="<%$ ConnectionStrings:connectionRCFATST.ProviderName %>" SelectCommand='SELECT "HP", "SPEED", "NETPRICE", "NEMATYPE", "EFFICIENCY", "OLDEFFICIENCY", "FRAMESIZE" FROM "TBLNEMAPRICING" ORDER BY "HP", "SPEED"'
                    UpdateCommand='UPDATE "TBLNEMAPRICING" SET "NETPRICE" = :NETPRICE, "EFFICIENCY" = :EFFICIENCY, "OLDEFFICIENCY" = :OLDEFFICIENCY, "FRAMESIZE" = :FRAMESIZE WHERE "HP" = :original_HP AND "SPEED" = :original_SPEED AND "NEMATYPE" = :original_NEMATYPE' ConflictDetection="CompareAllValues" OldValuesParameterFormatString="original_{0}">
                    <DeleteParameters>
                        <asp:Parameter Name="original_HP" Type="Decimal" />
                        <asp:Parameter Name="original_SPEED" Type="Decimal" />
                        <asp:Parameter Name="original_NEMATYPE" Type="String" />
                        <asp:Parameter Name="original_NETPRICE" Type="Decimal" />
                        <asp:Parameter Name="original_EFFICIENCY" Type="Decimal" />
                        <asp:Parameter Name="original_OLDEFFICIENCY" Type="Decimal" />
                        <asp:Parameter Name="original_FRAMESIZE" Type="String" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="NETPRICE" Type="Decimal" />
                        <asp:Parameter Name="EFFICIENCY" Type="Decimal" />
                        <asp:Parameter Name="OLDEFFICIENCY" Type="Decimal" />
                        <asp:Parameter Name="FRAMESIZE" Type="String" />
                        <asp:Parameter Name="original_HP" Type="Decimal" />
                        <asp:Parameter Name="original_SPEED" Type="Decimal" />
                        <asp:Parameter Name="original_NEMATYPE" Type="String" />
                        <asp:Parameter Name="original_NETPRICE" Type="Decimal" />
                        <asp:Parameter Name="original_EFFICIENCY" Type="Decimal" />
                        <asp:Parameter Name="original_OLDEFFICIENCY" Type="Decimal" />
                        <asp:Parameter Name="original_FRAMESIZE" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="HP" Type="Decimal" />
                        <asp:Parameter Name="SPEED" Type="Decimal" />
                        <asp:Parameter Name="NETPRICE" Type="Decimal" />
                        <asp:Parameter Name="NEMATYPE" Type="String" />
                        <asp:Parameter Name="EFFICIENCY" Type="Decimal" />
                        <asp:Parameter Name="OLDEFFICIENCY" Type="Decimal" />
                        <asp:Parameter Name="FRAMESIZE" Type="String" />
                    </InsertParameters>
                </asp:SqlDataSource>
            </asp:Panel>
            &nbsp; &nbsp;
        </div>
    </form>
</body>
</html>
