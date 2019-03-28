<%@ Page Language="VB" MasterPageFile="~/RI.master"  AutoEventWireup="false" CodeFile="Project.aspx.vb" Inherits="Project"
    EnableTheming="true" StylesheetTheme="RIBlackGold" Theme="RIBlackGold"
    EnableEventValidation="false" Title="International Paper: Outage Planner" 
    Trace="false" %>

    
<%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" EnableViewState="true" runat="Server">
    <embed src="../Outage/Outage%20Schedule.pdf" type="application/Acrobat" width="100%" height="100%"> </embed> 
</asp:Content>
