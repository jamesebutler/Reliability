<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucCultureSelection.ascx.vb"
    Inherits="ucCultureSelection" %>
<asp:Label ID="_lblCurrentLanguage" runat="server" Text="Select a Language:"></asp:Label>&nbsp;
<asp:LinkButton ID="_lnkChangeLanguage" runat="server" Text="<%$RIResources:Global,Chooselanguage %>"
    OnClientClick="Javascript:return false"></asp:LinkButton>
<ajaxToolkit:ModalPopupExtender ID="_mpeLanguageSelection" runat="server" TargetControlID="_lnkChangeLanguage"
    PopupControlID="_pnlLanguageSelection" BackgroundCssClass="modalBackground" DropShadow="true"
    OkControlID="_btnCancel" CancelControlID="_btnCancel">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="_pnlLanguageSelection" runat="server" Width="400px" ScrollBars="none"
    Style="display: none;" CssClass="modalPopup">
    <table cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td class="Border">
                <asp:Label ID="_lblChooseLanguage" runat="server" Text="<%$RIResources:ButtonText,Chooselanguage %>"></asp:Label></td>
        </tr>
        <tr>
            <td>
                <asp:RadioButtonList ID="_rblLanguages" AutoPostBack="false" runat="server" RepeatColumns="1">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Button ID="_btnOK"  runat="Server" Text="<%$RIResources:ButtonText,OK %>" />
                <asp:Button ID="_btnCancel" runat="Server" Text="<%$RIResources:ButtonText,Cancel %>" />
            </td>
        </tr>
    </table>
    <%--<asp:datalist ID="_rptLanguages" runat="server" RepeatColumns="2" RepeatLayout="Table" RepeatDirection="Horizontal" >
        <ItemTemplate>
            
        </ItemTemplate>
        <FooterTemplate>
            <div style="display:none;"></div>
        </FooterTemplate> 
    </asp:datalist>--%>
</asp:Panel>
