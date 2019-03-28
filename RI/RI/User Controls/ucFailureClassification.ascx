<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucFailureClassification.ascx.vb"
    Inherits="RI_User_Controls_ucFailureClassification" %>
<table border="0" width="100%" cellpadding="2" cellspacing="1">
    <tr class="Border">
        <td>
            <asp:Label ID="_lblFailureClassHeader" runat="server" SkinID="LabelButton" Width="350"></asp:Label>&nbsp;&nbsp;
            <asp:Label ID="_lblAvgFailureClass" runat="server" Text="<%$RIResources: Shared,TotalClassification %>"></asp:Label>&nbsp;
            <asp:TextBox ID="_txtFailureClass" BackColor="lightGray" Style="vertical-align: middle"
                runat="server" Width="30" ReadOnly="true" Text="0"></asp:TextBox>
            <asp:Label ID="_lblClassificationTier" runat="server" Text="<%$ RIResources:Shared,Classification Tier %>"></asp:Label>:&nbsp;
            <b><asp:Label ID="_lblClassificationTierValue" runat="server" ></asp:Label></b>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="_pnlFailureClass" runat="server">
                <table width="100%" border="0" cellpadding="0" cellspacing="1" style="background-color: black;
                    text-align: left">
                    <tr class="Header">
                        <td colspan="2">
                            &nbsp;</td>
                    </tr>
                    <tr class="Border" style="min-height: 50px;">
                        <td style="width: 40%" valign="top">
                            <asp:Label ID="_lblConstrainedArea" runat="server" Text='<%$ RIResources:Shared,Constrained Areas %>' Font-Bold="true"></asp:Label>
                            <asp:RadioButtonList ID="_rblConstrainedArea" runat="server" Enabled="False" onclick="SetConstrainedArea(GetClassificationConstrainedArea());CalculateClassificationScore();">
                                <asp:ListItem Text='<%$ RIResources:Shared,Equipment or Process in Constrained Area %>' Value="5"></asp:ListItem>
                                <asp:ListItem Text='<%$ RIResources:Shared,Equipment or Process in Non-constrained Area %>' Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td rowspan="4" valign="top">
                            <table width="100%" border="0" style="height: 500px; background-color: black" cellpadding="0"
                                cellspacing="1">
                                <tr class="Border" style="min-height: 33%;">
                                    <td style="width: 250px" valign="top" align="right">
                                        <div id="_divFailureClassPointerT1" runat="server" style="display: none">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="_lblFailureClassPointer" runat="server" Text='<%$ RIResources:Shared,You Are Here%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Image ID="_imgFailureClassPointer" runat="server" ImageUrl="~/Images/RightArrow.png" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                    <td rowspan="3" valign="top" align="center" style="background-image: url(../Images/ClassificationColorBar.jpg);
                                        background-repeat: no-repeat;">
                                        <asp:Image ID="_imgSpacer" runat="server" ImageUrl="~/Images/blank.gif" Height="500px"
                                            Width="70px" Style="" />
                                    </td>
                                    <td valign="top" style="padding-top: 40px; padding-left: 10px;">
                                        <asp:Label ID="_lblTierOneFailure" runat="server" Text='<%$ RIResources:Shared,Tier One Failure%>' Font-Bold="true"></asp:Label>
                                        <ul>
                                            <li>
                                                <asp:Label ID="_lblFailureT1a" runat="server" Text='<%$ RIResources:Shared,High Impact%>'></asp:Label></li>
                                            <li>
                                                <asp:Label ID="_lblFailureT1b" runat="server" Text='<%$ RIResources:Shared,RCFA Required%>'></asp:Label></li>
                                        </ul>
                                    </td>
                                </tr>
                                <tr class="Border" style="min-height: 33%">
                                    <td valign="top" align="right">
                                        <div id="_divFailureClassPointerT2" runat="server" style="display: none">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text='<%$ RIResources:Shared,You Are Here%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/RightArrow.png" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                    <td valign="top" style="padding-top: 40px; padding-left: 10px;">
                                        <asp:Label ID="_TierTwoFailure" runat="server" Text='<%$ RIResources:Shared,Tier Two Failure%>'>
                                        </asp:Label>
                                        <ul>
                                            <li>
                                                <asp:Label ID="_lblFailureT2a" runat="server" Text='<%$ RIResources:Shared,Moderate Impact%>'></asp:Label></li>
                                            <li>
                                                <asp:Label ID="_lblFailureT2b" runat="server" Text='<%$ RIResources:Shared,Reliability Engineer evaluates to determine RCFA need.%>'></asp:Label></li>
                                            <li>
                                                <asp:Label ID="_lblFailureT2c" runat="server" Text='<%$ RIResources:Shared,RCFA As Time Allows%>'></asp:Label></li>
                                        </ul>
                                    </td>
                                </tr>
                                <tr class="Border" style="min-height: 33%">
                                    <td valign="top" align="right">
                                        <div id="_divFailureClassPointerT3" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" Text='<%$ RIResources:Shared,You Are Here%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/RightArrow.png" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                    <td valign="top" style="padding-top: 40px; padding-left: 10px;">
                                        <asp:Label ID="Label1" runat="server" Text="Tier Three Failure">
                                        </asp:Label>
                                        <ul>
                                            <li>
                                                <asp:Label ID="_lblFailureT3a" runat="server" Text='<%$ RIResources:Shared,Little to No Impact%>'></asp:Label></li>
                                            <li>
                                                <asp:Label ID="_lblFailureT4a" runat="server" Text='<%$ RIResources:Shared,RCFA Not Required%>'></asp:Label></li>
                                        </ul>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="Border" style="min-height: 50px;">
                        <td valign="top">
                            <asp:Label ID="_lblCriticalityRating" runat="server" Text='<%$ RIResources:Shared,Criticality Rating%>' Font-Bold="true"></asp:Label>
                            <asp:RadioButtonList ID="_rblCriticalityRating" runat="server" onclick="SetCriticality(GetClassificationCriticality());CalculateClassificationScore();">
                                <asp:ListItem Text='<%$ RIResources:Shared,Critical [A] or [9]%>' Value="9"></asp:ListItem>
                                <asp:ListItem Text='<%$ RIResources:Shared,Critical [B] or [5]%>' Value="5"></asp:ListItem>
                                <asp:ListItem Text='<%$ RIResources:Shared,Critical [C] or [1]%>' Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="Border" style="min-height: 50px;">
                        <td valign="top">
                            <asp:Label ID="_lblLifeExpectancy" runat="server" Text='<%$ RIResources:Shared,LIFE EXPECTANCY BASED ON EQUIPMENT OR ORIGINAL DESIGN LIFE%>'
                                Font-Bold="true"></asp:Label>
                            <asp:RadioButtonList ID="_rblLifeExpectancy" runat="server" onclick="CalculateClassificationScore();">
                                <asp:ListItem Text='<%$ RIResources:Shared,Infant Mortality%>' Value="5"></asp:ListItem>
                                <asp:ListItem Text='<%$ RIResources:Shared,25% of Life%>' Value="4"></asp:ListItem>
                                <asp:ListItem Text='<%$ RIResources:Shared,50% of Life%>' Value="3"></asp:ListItem>
                                <asp:ListItem Text='<%$ RIResources:Shared,75% of Life%>' Value="2"></asp:ListItem>
                                <asp:ListItem Text='<%$ RIResources:Shared,100% of Life%>' Value="1"></asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr class="Border" style="min-height: 50px;">
                        <td valign="top">
                            <asp:Label ID="_lblEquipmentCare" runat="server" Text='<%$ RIResources:Shared,Equipment Care Strategy%>' Font-Bold="true"></asp:Label>
                            <asp:RadioButtonList ID="_rblEquipmentCare" runat="server" onclick="CalculateClassificationScore();">
                                <asp:ListItem Text='<%$ RIResources:Shared,Undetected Failure/Unscheduled Downtime%>' Value="5"></asp:ListItem>
                                <asp:ListItem Text='<%$ RIResources:Shared,Detected Failure/Unscheduled Downtime, Break-in or add-on work%>'
                                    Value="3"></asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
<ajaxToolkit:CollapsiblePanelExtender ID="_cpeFailureClass" runat="Server" TargetControlID="_pnlFailureClass"
    Collapsed="True" CollapseControlID="_lblFailureClassHeader" ExpandControlID="_lblFailureClassHeader"
    SuppressPostBack="true" TextLabelID="_lblFailureClassHeader" CollapsedText="<%$RIResources:Shared,ShowFailureClass%>"
    ExpandedText="<%$RIResources:Shared,HideFailureClass%>" ScrollContents="false"
    CollapsedSize="0" />
