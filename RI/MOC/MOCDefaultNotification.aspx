<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="MOCDefaultNotification.aspx.vb" Inherits="MOC_MOCDefaultNotification" 
Title="Default MOC Notification" Trace="false" EnableViewState="true" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="_cntMain" ContentPlaceHolderID="_cphMain" Runat="Server">
<Asp:UpdatePanel id="_udpLocation" runat="server" updatemode="Conditional">
    <ContentTemplate>
        
    <ajaxToolkit:CascadingDropDown id="_cddlFacility" runat="server" category="SiteID"
        loadingtext="" prompttext="    " servicemethod="GetFacilityList" servicepath="~/CascadingLists.asmx"
        targetcontrolid="_ddlFacility" usecontextkey="true">
    </ajaxToolkit:CascadingDropDown>
    <ajaxToolkit:CascadingDropDown id="_cddlBusUNit" runat="server" category="BusinessUnit"
        loadingtext="" prompttext="    " servicemethod="GetBusinessUnit" servicepath="~/CascadingLists.asmx"
        targetcontrolid="_ddlBusinessUnit" parentcontrolid="_ddlFacility">
    </ajaxToolkit:CascadingDropDown>
     <ajaxToolkit:CascadingDropDown id="_cddlArea" runat="server" category="Area"
        loadingtext="" prompttext="    " servicemethod="GetArea" servicepath="~/CascadingLists.asmx"
        targetcontrolid="_ddlArea" parentcontrolid="_ddlBusinessUnit">
    </ajaxToolkit:CascadingDropDown>
    <ajaxToolkit:CascadingDropDown id="_cddlLineBreak" runat="server" category="Line"
        loadingtext="" prompttext="    " servicemethod="GetLine" servicepath="~/CascadingLists.asmx"
        targetcontrolid="_ddlLineBreak" parentcontrolid="_ddlArea">
    </ajaxToolkit:CascadingDropDown>
           
    <table style="width: 100%" cellpadding="0" cellspacing="2">
        <tr class="Border">
            <td align="center">
            <asp:Label ID="_lblDesc" runat="server"
             Text="Listing will show the default approvers and informed users that are populated when an MOC is initiated."></asp:Label><br />
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="2" cellspacing="0" style="width: 100%">
                    <tr class="Border">
                        <td valign="top" width="30%">
                            <asp:Label ID="_lblFacility" runat="server"
                                Text="<%$RIResources:Shared,Facility %>"></asp:Label><br />
                            <asp:DropDownList ID="_ddlFacility" runat="server" AutoPostBack="true"
                                Width="90%">
                            </asp:DropDownList>
                                            </td>
                        <td valign="top" width="20%">
                            <asp:Label ID="_lblBusinessUNit" runat="server" Text="<%$RIResources:Shared,BusinessUnit %>"></asp:Label><br />
                            <asp:DropDownList ID="_ddlBusinessUnit" 
                                 Width="90%" Visible="true" AutoPostBack="true"
                                runat="server" />
                        <td valign="top" width="20%">
                            <asp:Label ID="Label1" runat="server" Text="<%$RIResources:Shared,Area %>"></asp:Label><br />
                            <asp:DropDownList ID="_ddlArea" CausesValidation="true" AutoPostBack="true"
                                Width="98%" runat="server" />
                        </td>
                        <td valign="top" width="20%">
                            <asp:Label ID="_lblLine" runat="server" Text="<%$RIResources:Shared,Line %>"></asp:Label><br />
                            <asp:DropDownList ID="_ddlLineBreak" AutoPostBack="true"
                                Width="98%" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView Width="100%" CssClass="Border" ID="_gvL1Approvers" runat="server" 
        AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="USERNAME" EmptyDataText="No L1 Approvers">
        <Columns>
            <asp:TemplateField ItemStyle-Width="20%" HeaderText="L1 Approvers"
                HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
            <ItemTemplate>
                <center>
                    <asp:Label ID="_lbName" runat="server" Text='<%# Bind("FullName") %>'></asp:Label></center>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="20%" HeaderText="Business Unit"
                HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
            <ItemTemplate>
                <center>
                    <asp:Label ID="_lbBusUnit" runat="server" Text='<%# Bind("risuperarea") %>'></asp:Label></center>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="20%" HeaderText="Area"
                HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
            <ItemTemplate>
                <center>
                    <asp:Label ID="_lbArea" runat="server" Text='<%# Bind("subarea") %>'></asp:Label></center>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="20%" HeaderText="Area"
                HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
            <ItemTemplate>
                <center>
                    <asp:Label ID="_lbLine" runat="server" Text='<%# Bind("area") %>'></asp:Label></center>
            </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>  
    <br /> 
    <asp:GridView Width="100%" CssClass="Border" ID="_gvL2Approvers" runat="server" 
        AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="USERNAME" EmptyDataText="No L2 Approvers">
        <Columns>
            <asp:TemplateField ItemStyle-Width="20%" HeaderText="L2 Approvers"
                HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
            <ItemTemplate>
                <center>
                    <asp:Label ID="_lbName" runat="server" Text='<%# Bind("FullName") %>'></asp:Label></center>
            </ItemTemplate>
            </asp:TemplateField>             
            <asp:TemplateField ItemStyle-Width="20%" HeaderText="Business Unit"
                HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
            <ItemTemplate>
                <center>
                    <asp:Label ID="_lbBusUnit" runat="server" Text='<%# Bind("risuperarea") %>'></asp:Label></center>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="20%" HeaderText="Area"
                HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
            <ItemTemplate>
                <center>
                    <asp:Label ID="_lbArea" runat="server" Text='<%# Bind("subarea") %>'></asp:Label></center>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="20%" HeaderText="Area"
                HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
            <ItemTemplate>
                <center>
                    <asp:Label ID="_lbLine" runat="server" Text='<%# Bind("area") %>'></asp:Label></center>
            </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>   
    <br />
    <asp:GridView Width="100%" CssClass="Border" ID="_gvL3Approvers" runat="server" 
        AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="USERNAME" EmptyDataText="No L3 Approvers" >
        <Columns>
            <asp:TemplateField ItemStyle-Width="20%" HeaderText="L3 Approvers"
                HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
            <ItemTemplate>
                <center>
                    <asp:Label ID="_lbName" runat="server" Text='<%# Bind("FullName") %>'></asp:Label></center>
            </ItemTemplate>
            </asp:TemplateField>             
            <asp:TemplateField ItemStyle-Width="20%" HeaderText="Business Unit"
                HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
            <ItemTemplate>
                <center>
                    <asp:Label ID="_lbBusUnit" runat="server" Text='<%# Bind("risuperarea") %>'></asp:Label></center>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="20%" HeaderText="Area"
                HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
            <ItemTemplate>
                <center>
                    <asp:Label ID="_lbArea" runat="server" Text='<%# Bind("subarea") %>'></asp:Label></center>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="20%" HeaderText="Area"
                HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
            <ItemTemplate>
                <center>
                    <asp:Label ID="_lbLine" runat="server" Text='<%# Bind("area") %>'></asp:Label></center>
            </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>   
    <br />
    <asp:GridView Width="100%" CssClass="Border" ID="_gvInformed" runat="server" 
        AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="USERNAME">
        <Columns>
            <asp:TemplateField ItemStyle-Width="20%" HeaderText="Informed"
                HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
            <ItemTemplate>
                <center>
                    <asp:Label ID="_lbName" runat="server" Text='<%# Bind("FullName") %>'></asp:Label></center>
            </ItemTemplate>
            </asp:TemplateField>             
            <asp:TemplateField ItemStyle-Width="20%" HeaderText="Business Unit"
                HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
            <ItemTemplate>
                <center>
                    <asp:Label ID="_lbBusUnit" runat="server" Text='<%# Bind("risuperarea") %>'></asp:Label></center>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="20%" HeaderText="Area"
                HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
            <ItemTemplate>
                <center>
                    <asp:Label ID="_lbArea" runat="server" Text='<%# Bind("subarea") %>'></asp:Label></center>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="20%" HeaderText="Area"
                HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
            <ItemTemplate>
                <center>
                    <asp:Label ID="_lbLine" runat="server" Text='<%# Bind("area") %>'></asp:Label></center>
            </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>   
 </ContentTemplate>             
 </Asp:UpdatePanel>
</asp:Content>

