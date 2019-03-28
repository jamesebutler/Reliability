<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UcLdapLogin.ascx.vb"
    Inherits="RI_User_Controls_UcLdapLogin" %>
<%--<Asp:UpdatePanel ID="_udpLogin" runat="server" RenderMode="Inline">
    <ContentTemplate>--%>
<ajaxtoolkit:modalpopupextender id="_mpeLogin" runat="server" backgroundcssclass="modalBackground"
    targetcontrolid="_loginStatus" okcontrolid="_btnHideLogin" cancelcontrolid="_btnHideLogin"
    popupcontrolid="_pnLogin" dropshadow="true" repositionmode="RepositionOnWindowResizeAndScroll">
</ajaxtoolkit:modalpopupextender>
<asp:Panel ID="_pnlLoginContainer" HorizontalAlign="Right" runat="server" Style="display: inline;
    text-align: right" Width="350px">
    <asp:Label ID="_lblWelcome" Font-Bold="false" runat="server" Font-Size="0.8em"></asp:Label>
    <asp:HyperLink ID="_loginStatus" NavigateUrl="#" Font-Underline="true" ForeColor="black"
        runat="server" Text="<%$RIResources:Global,SwitchUser %>" Font-Bold="true" />&nbsp;
    <%-- <asp:LinkButton ID="_logOut" OnClick="LogOut" Font-Underline="true" ForeColor="black"
        Text="<%$RIResources:Global,Logout %>" runat="server" Visible="false" />--%>
    <%-- <asp:LinkButton ID="_runAs" Font-Underline="true" ForeColor="blue" Text="<%$RIResources:Global,RunAs %>"
        runat="server" />--%>
</asp:Panel>
<asp:Panel ID="_pnLogin" runat="server" Style="display: none; width: 400px;" CssClass="modalPopup">
    <asp:Panel ID="_pnlLoginImpersonate" runat="server" GroupingText="<%$RIResources:Global,Login %>">
        <table border="0" cellpadding="0" style="width: 100%">
            <tr>
                <td align="Left">
                    <asp:Label ID="UserNameLabel" runat="server" ForeColor="black" Font-Bold="true" Text="<%$RIResources:Global,UserName %>">:</asp:Label>
                </td>
                <td align="Left">
                    <asp:TextBox ID="UserName" runat="server" Width="190px" Font-Bold="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                        ErrorMessage="" ToolTip="<%$RIResources:Global,UserNameRequired %>"
                        ValidationGroup="login">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="Left">
                    <asp:Label ID="PasswordLabel" ForeColor="black" runat="server" AssociatedControlID="Password"
                        Font-Bold="true" Text="<%$RIResources:Global,Password %>"></asp:Label>
                </td>
                <td align="Left">
                    <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="190px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                        ErrorMessage="" ToolTip="<%$RIResources:Global,PasswordRequired %>"
                        ValidationGroup="login">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="Left">
                    <asp:Label ID="DomainLabel" ForeColor="black" runat="server" AssociatedControlID="_ddlDomain"
                        Font-Bold="true" Text="<%$RIResources:Global,Domain %>"></asp:Label>
                </td>
                <td align="Left">
                    <asp:DropDownList ID="_ddlDomain" runat="server">
                    <asp:ListItem Text="GRAPHICPKG" Value="GRAPHICPKG"></asp:ListItem>
                        <asp:ListItem Text="NA" Value="NA" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="center" style="color: red" colspan="2">
                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" CommandArgument="Login"
                        Text="<%$RIResources:Global,Login %>" ValidationGroup="login" />&nbsp;
                    <input type="button" id="_btnCancel2" value="<%$RIResources:Global,Cancel %>" runat="server"
                        class="Button" onmouseover="this.className='Buttonhover';" onmouseout="this.className='Button';" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Label ID="_lblMessageToUser" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
<div style="visibility: hidden; display: none;">
    <input type="button" id="_btnHideLogin" onclick="return false" runat="server" value="Hide Login" />
</div>
<%-- </ContentTemplate>
</Asp:UpdatePanel>--%>
