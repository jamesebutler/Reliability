<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucHistory.ascx.vb" Inherits="RI_User_Controls_Common_ucHistory" %>
<%@ Register Src="~/RI/User Controls/Common/ucBanner.ascx" TagName="Banner" TagPrefix="IP" %>
<asp:Button ID="_btnHistory" runat="server" Text="<%$RIResources:Shared,History%>" />
<asp:Panel ID="_pnlHistory" runat="server" CssClass="modalPopup" ScrollBars="None"
    Width="95%" Style="display: none;">
    <IP:Banner ID="_bannerHistory" BannerMessage="<%$RIResources:Shared,History%>"
        runat="server" DisplayPopupBanner="true" />
    <asp:Panel ID="_pnlBody" runat="server" ScrollBars="Vertical">
        <asp:GridView ID="_grvHistory" CssClass="Border" Width="100%" runat="server">
            <HeaderStyle CssClass="LockHeader" />
            <EmptyDataTemplate>
                <asp:Label ID="_lblNoRecordsFound" runat="server" Text='<%$RIResources:Shared,NoRecordsFound %>'></asp:Label>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="RINumber" HeaderText="<%$RIResources:Shared,Key %>" />
                <asp:BoundField DataField="FullName" HeaderText="<%$RIResources:Shared,FullName %>" />
                <asp:BoundField DataField="UpdateDate" HeaderText="<%$RIResources:Shared,Update Date %>" />
                <asp:BoundField DataField="Description" HeaderText="<%$RIResources:Shared,Description %>" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <p style="text-align: right;">
        <asp:Button ID="_btnClose" runat="server" Text="<%$RIResources:Shared,Close %>"></asp:Button>
    </p>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="_mpeHistory" runat="server" TargetControlID="_btnHistory"
    PopupControlID="_pnlHistory" BackgroundCssClass="modalBackground" DropShadow="true"
    OkControlID="_btnClose" CancelControlID="_btnClose">
</ajaxToolkit:ModalPopupExtender>
