<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="Data_Maintenance.aspx.vb" Inherits="Data_Maintenance" title="RI:Data Maintenance" %>
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
                <center><IP:DataMaintenaceSelector ID="_RIDataMaintenance" ApplicationText="Application" Width="900px" SiteText="Site" GridCssClass="MaintenanceGridRow" AlternatingRowCssClass="MaintenanceGridAlternatingRow" headerstylecssclass="MaintenanceGridHeader" runat="server" InstructionCssClass="MaintenanceInstructionalText" DefaultApplication="RI"/></center>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

