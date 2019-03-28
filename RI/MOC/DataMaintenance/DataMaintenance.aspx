<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="DataMaintenance.aspx.vb" Inherits="MOCData_Maintenance" title="MOC:Data Maintenance" %>
<%@ Register Assembly="RIDataMaintenance" Namespace="RIDataMaintenance" TagPrefix="IP" %>
<%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" Runat="Server">
    <style>
       .MaintenanceGridHeader {
            background-color: black;
            color: white;
            line-height: 30px;
        }
       .MaintenanceGridRow{
           background-color:#CCCC99;
       }
       .MaintenanceGridAlternatingRow{
           background-color:white;
       }
       .MaintenanceInstructionalText{
            border-style: solid;
            padding: 10px;
            border-width: 2px;
            text-align:center;
       }
       .MaintenanceInstructionalText  span{
           font-size:1.5em;
           color:black;
           border-width:0px;
       }

    </style>
    <asp:UpdatePanel ID="_udpMaintenance" runat="server" UpdateMode="Always">
        <ContentTemplate>
                <center><IP:DataMaintenaceSelector ID="_RIDataMaintenance" ApplicationText="Application" Width="900px" SiteText="Site" GridCssClass="MaintenanceGridRow" AlternatingRowCssClass="MaintenanceGridAlternatingRow" headerstylecssclass="MaintenanceGridHeader" runat="server" InstructionCssClass="MaintenanceInstructionalText" DefaultApplication="MOC"/></center>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" Runat="Server">
    <ContentTemplate>
    <br />
    <br />
        <asp:Table ID="_tblOutageStatus" runat="server" CellPadding="2" CellSpacing="2" BackColor="white" Style="width: 50%" > 
            <asp:TableRow CssClass="Border">
                <asp:TableCell CssClass="Border"> 
                <center>
                    <asp:HyperLink ID="_hlApprovers" runat="server" NavigateUrl="~/MOC/DataMaintenance/MOCDefaultApprovers.aspx">Default Approvers</asp:HyperLink>
                </center>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow CssClass="Border">
                <asp:TableCell CssClass="Border"> 
                <center>
                    <asp:HyperLink ID="_hlMOCTaskTemplates" runat="server" NavigateUrl="~/MOC/DataMaintenance/MOCTaskTemplates.aspx">Task Templates</asp:HyperLink>
                </center>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        
    </ContentTemplate>
</asp:Content>--%>

