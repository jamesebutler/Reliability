<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucSpellcheck.ascx.vb"
    Inherits="RI_User_Controls_Common_ucSpellcheck" %>
<Karamasoft:UltimateSpell ID="_spellIncidentDescription" runat="server" ShowAddButton="false"
    ShowSpellButton="false" IgnoreDisabledControls="true" ShowModal="true" ShowOptions="true"
    EnableTheming="false" SpellAsYouType="false">
</Karamasoft:UltimateSpell>
<asp:Button ID="_btnSpell" Text="<%$RIResources:Shared,SpellCheck %>" EnableViewState="false"
    runat="server" />
