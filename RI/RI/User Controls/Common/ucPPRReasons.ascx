<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucPPRReasons.ascx.vb"
    Inherits="ucPPRReasons" %>

<script type="text/javascript">   

        function ActiveTabChanged(sender, e) {
 
        }               

        var HighlightAnimations = {};
        function Highlight(el) {
            if (HighlightAnimations[el.uniqueID] == null) {
                HighlightAnimations[el.uniqueID] = AjaxControlToolkit.Animation.createAnimation({
                    AnimationName : "color",
                    duration : 0.5,
                    property : "style",
                    propertyKey : "backgroundColor",
                    startValue : "#FFFF90",
                    endValue : "#FFFFFF"
                }, el);
            }
            HighlightAnimations[el.uniqueID].stop();
            HighlightAnimations[el.uniqueID].play();
        }
        
     
</script>
<%--
<Asp:UpdatePanel ID="_udpTabs" runat="server" UpdateMode=Always >
    <ContentTemplate>--%>
        <ajaxToolkit:TabContainer CssClass="ajax__tab_xp"  runat="server" ID="Tabs" Height="118px">
            <ajaxToolkit:TabPanel runat="Server" ID="Panel1" HeaderText="Downtime">
                <ContentTemplate>
                    <asp:RadioButtonList ID="_rblDowntime" RepeatColumns=2 runat="server" CellSpacing=5 AutoPostBack="false" RepeatLayout="Table">
                        <asp:ListItem Text="All Data" Value=""></asp:ListItem>
                    </asp:RadioButtonList>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel runat="Server" ID="Panel3" HeaderText="Paper Loss">
                <ContentTemplate>
                    <asp:RadioButtonList ID="_rblPaperLoss" RepeatColumns=2 runat="server" CellSpacing=5 AutoPostBack="false" RepeatLayout="Table">
                        <asp:ListItem Text="All Data" Value=""></asp:ListItem>
                    </asp:RadioButtonList>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel runat="Server" ID="Panel2" HeaderText="Slab Loss">
                <ContentTemplate>
                    <asp:RadioButtonList ID="_rblSlabLoss" RepeatColumns=2 CellSpacing=5 runat="server" AutoPostBack="false" RepeatLayout="Table">
                        <asp:ListItem Text="All Data" Value=""></asp:ListItem>
                    </asp:RadioButtonList>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
       <%-- Current Tab:
        <asp:Label runat="server" ID="CurrentTab" /><br />--%>
        <asp:HiddenField ID="_selectedReason" runat="server" />
<%--  </ContentTemplate>
</Asp:UpdatePanel>--%>
