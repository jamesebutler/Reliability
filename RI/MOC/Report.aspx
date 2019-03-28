<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="Report.aspx.vb"
    Inherits="MOC_Report" Title="MOC Report" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">    
    <Asp:UpdatePanel runat="server" UpdateMode="always" ID="_udpReport">
        <ContentTemplate>
            <div id="_divReport" runat="server"></div>
        </ContentTemplate>
    </Asp:UpdatePanel>
</asp:Content>
