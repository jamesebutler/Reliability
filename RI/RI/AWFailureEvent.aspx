<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="AWFailureEvent.aspx.vb" Inherits="RI_Workspace" title="Analysis Workspace" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<%@ Register Namespace="RealWorld.Grids" TagPrefix="rwg" %>

<asp:Content ID="_contentMain" ContentPlaceHolderID="_cphMain" Runat="Server">
    <asp:Button ID="_btnAdd" runat="server" text="<%$RIResources:Shared,Add Event %>" />
    <asp:Button ID="_btnList" runat="server" text="<%$RIResources:Shared,Return to List %>" visible="false"/>
    <IP:SpellCheck id="btnSpellCheck" runat="server" visible="false"></IP:SpellCheck>
    <asp:Button id="_btnAddModes" runat="server" Text="<%$RIResources:Shared,Save%>" visible="false"/>
    <asp:Button id="_btnFailureModeUpdate" runat="server" Text="<%$RIResources:Shared,Save %>" visible="false"/>

    <asp:HiddenField id="_hfFailureEvent" runat="server"/>
    <asp:Label ID="_lbInstructions" runat="server" Text=""></asp:Label>    
    
    <div id="_divNewAWObjects" runat="server">
    <table id="_tblEvent" runat="server" width="95%" visible="false">
        <tr id="_trFailureEvent" class="Header"     >
            <td align="left" style="width:25%"><asp:Label ID="_lbFailureEvent" runat="server" SkinID="labelWhite" Text="<%$RIResources:Shared,Failure Event %>"/></td>
            <td align="left" colspan="2"><asp:Label ID="_lbFailure" runat="server" SkinID="labelWhite" Text="<%$RIResources:Shared,Define the problem or the Failure by selecting from Drop down list or entering your description.%>"/></td>
        </tr>
        <tr>
            <td colspan="2"><asp:DropDownList ID="_ddlFailureEvent" runat="server" EnableViewState="true" AutoPostBack="true">
            </asp:DropDownList>&nbsp;OR&nbsp;<IP:AdvancedTextBox ID="_tbFailureEvent" runat="server" Style="font-family: Verdana;
                            font-size: 12px;" expandheight="true" width="70%" rows="2" textmode="MultiLine" AutoPostBack="true" ></IP:AdvancedTextBox>
            </td>
            <td style="width:5%" valign="top"><asp:Button ID="_btnAddEvent" runat="server" Text="<%$RIResources:Shared,Next%>" Visible="false"/></td>
        </tr>
    </table>
    
    <table id="_tblMode" runat="server" visible="false" width="95%">
        <tr class="Header">
            <td  align="left" style="width:25%">
            <asp:Label ID="_lblFailureMode" runat="server" SkinID="labelWhite" Text="<%$RIResources:Shared,Failure Mode %>"/>
            </td>
            <td align="left"><asp:Label ID="_lblFailureModeDesc" runat="server" SkinID="labelWhite" Text="<%$RIResources:Shared,List ALL the Ways This Type of Failure Event Can Occur or Has Ocurred in the Past. Enter Additional Ways.%>"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:CheckBoxList ID="_cblFailureMode" runat="server" RepeatDirection="Horizontal" ></asp:CheckBoxList>
                <IP:AdvancedTextBox ID="_tbFailureModeOther" runat="server" Style="font-family: Verdana;
                            font-size: 12px;" expandheight="true" width="98%" rows="2" textmode="MultiLine" maxlength="2000" text='<%# Bind("failuremodedesc") %>' ></IP:AdvancedTextBox><br />
                <asp:TextBox ID="_tbFailureModeOther2" runat="server" Width="95%" MaxLength="2000" style="display:none;font-family: Verdana;"></asp:TextBox>
                <asp:TextBox ID="_tbFailureModeOther3" runat="server" Width="95%" MaxLength="2000" style="display:none;font-family: Verdana;"></asp:TextBox>
                <asp:TextBox ID="_tbFailureModeOther4" runat="server" Width="95%" MaxLength="2000" style="display:none;font-family: Verdana;"></asp:TextBox>
                <asp:TextBox ID="_tbFailureModeOther5" runat="server" Width="95%" MaxLength="2000" style="display:none;font-family: Verdana;"></asp:TextBox>
            </td>
            </tr>
    </table>
    
    <%--<table id="_tblProvenFailureCause" runat="server" visible="false" width="100%">
        <tr class="Header">
            <td align="left" style="width:25%">
            <asp:Label ID="Label4" runat="server" SkinID="labelWhite" Text="<%$RIResources:RI,Proven Failure Cause(s) %>"/>
            </td>
            <td align="left"><asp:Label ID="Label5" runat="server" SkinID="labelWhite" Text="<%$RIResources:Shared,List Proven Failure Causes for Failure Modes selected/entered.%>"/></td>
        </tr>
        <tr>
            <td style="width:95%" colspan="2">
            <asp:GridView ID="_gvFailureCause" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="Border">
            <Columns>
                <asp:TemplateField HeaderText="<%$RIResources:Shared,Failure Mode%>" ItemStyle-VerticalAlign="top" ItemStyle-Width="25%">
                <ItemTemplate>
                    <asp:HiddenField ID="_hfRIFailureModeSeq" runat="server" Value='<%# Bind("rifailuremodeseqid") %>' />
                    <asp:HiddenField ID="_hfFailureModeSeq" runat="server" Value='<%# Bind("failuremodeseqid") %>' />
                    <asp:Label ID="_lbMode" runat="server" text='<%# FixCrLf(Eval("failuremodedesc")) %>'> </asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$RIResources:Shared,Proven Failure Cause%>" >
                <Itemtemplate>
                    <asp:CheckBoxList ID="_cblFailureCause" runat="server" RepeatDirection="horizontal"></asp:CheckBoxList>
                    <IP:AdvancedTextBox id="_tbFailureCauseOther" runat="server" Style="font-family: Verdana;
                            font-size: 12px;" Width="98%" rows="2" textmode="MultiLine" maxlength="4000" text='<%# FixCrLf(Eval("failurecausedesc")) %>'></IP:AdvancedTextBox>
                    <IP:AdvancedTextBox id="_tbFailureCauseOther2" runat="server" Style="font-family: Verdana; font-size: 12px; display:none;" width="98%" rows="2" textmode="MultiLine" maxlength="4000"></IP:AdvancedTextBox>
                    <IP:AdvancedTextBox id="_tbFailureCauseOther3" runat="server" Style="font-family: Verdana; font-size: 12px; display:none;" width="98%" rows="2" textmode="MultiLine" maxlength="4000"></IP:AdvancedTextBox>
                    <IP:AdvancedTextBox id="_tbFailureCauseOther4" runat="server" Style="font-family: Verdana; font-size: 12px; display:none;" width="98%" rows="2" textmode="MultiLine" maxlength="4000"></IP:AdvancedTextBox>
                    <IP:AdvancedTextBox id="_tbFailureCauseOther5" runat="server" Style="font-family: Verdana; font-size: 12px; display:none;" width="98%" rows="2" textmode="MultiLine" maxlength="4000"></IP:AdvancedTextBox>
                </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            </asp:GridView>
            </td>
            <td style="width:5%" valign="top"><asp:Button id="_btnAddCauses" runat="server" Text="<%$RIResources:Shared,Next%>" visible="false" /></td>
        </tr>
    </table>

    <table id="_tblMostLikelyFailureCause" runat="server" visible="false" width="100%">
        <tr class="Header">
            <td align="left" style="width:25%">
            <asp:Label ID="Label6" runat="server" SkinID="labelWhite" Text="<%$RIResources:Shared,Most Likely Proven Failure Cause(s)%>"></asp:Label>
            </td>
            <td align="left"><asp:Label ID="Label7" runat="server" SkinID="labelWhite" Text="<%$RIResources:Shared,List Possible Reasons for the MOST LIKELY Proven Failure Cause%>"></asp:Label></td>
        </tr>
        <tr><td style="width:95%" colspan="2">
            <asp:GridView ID="_gvFailureReason" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="Border">
            <Columns>
            <asp:TemplateField HeaderText="<%$RIResources:Shared,Proven Failure Cause%>" ItemStyle-Width="25%" ItemStyle-VerticalAlign="top">
            <ItemTemplate>
                <asp:HiddenField ID="_hfRIFailureCauseSeq" runat="server" Value='<%# Bind("rifailurecauseseqid") %>' />
                <asp:HiddenField ID="_hfFailureCauseSeq" runat="server" Value='<%# Bind("failureCauseseqid") %>' />
                    <asp:label id="_lbFailureCauseOther" runat="server" Text='<%# FixCrLf(Eval("failurecausedesc")) %>'></asp:label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$RIResources:Shared,Possible Reasons%>">
                <Itemtemplate>
                    <asp:CheckBoxList ID="_cblFailureCauseReas" runat="server" RepeatDirection="horizontal"></asp:CheckBoxList>
                    <IP:AdvancedTextBox id="_tbFailureReasonOther" runat="server" Width="98%" rows="2" textmode="MultiLine" maxlength="4000" text='<%# FixCrLf(Eval("causereason")) %>' style="font-family: Verdana; font-size: 12px; "></IP:AdvancedTextBox>
                    <IP:AdvancedTextBox id="_tbFailureReasonOther2" runat="server" width="98%" rows="2" textmode="MultiLine" maxlength="4000" style="font-family: Verdana; font-size: 12px; display:none;"></IP:AdvancedTextBox>
                    <IP:AdvancedTextBox id="_tbFailureReasonOther3" runat="server" width="98%" rows="2" textmode="MultiLine" maxlength="4000" style="font-family: Verdana; font-size: 12px; display:none;"></IP:AdvancedTextBox>
                    <IP:AdvancedTextBox id="_tbFailureReasonOther4" runat="server" width="98%" rows="2" textmode="MultiLine" maxlength="4000" style="font-family: Verdana; font-size: 12px; display:none;"></IP:AdvancedTextBox>
                    <IP:AdvancedTextBox id="_tbFailureReasonOther5" runat="server" width="98%" rows="2" textmode="MultiLine" maxlength="4000" style="font-family: Verdana; font-size: 12px; display:none;"></IP:AdvancedTextBox>
                </ItemTemplate>
                </asp:TemplateField></Columns>
            </asp:GridView></td>
            <td style="width:5%" valign="top"><asp:Button id="_btnAddReasons" runat="server" Text="<%$RIResources:Shared,Next%>" visible="false" /></td>
        </tr>
    </table>--%>
    
    <%--<table  id="_tblRootCause" runat="server" visible="false" width="100%">   
        <tr class="Header">
            <td align="left" style="width:25%">
            <asp:Label ID="_lbRootCause" runat="server" SkinID="labelWhite" Text="<%$RIResources:Shared,Verification/Root Cause %>"/>
            </td>
            <td align="left"><asp:Label ID="_lbRootCauseInstr" runat="server" SkinID="labelWhite" Text="<%$RIResources:Shared,For the Most Likey Proven Failure Causes. List Possible Root Causes.%>"/></td>
        </tr>
        <tr>
            <td style="width:95%" colspan="2">
            <asp:GridView ID="_gvFailureCauseReason" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="Border">
            <Columns>
            <asp:TemplateField HeaderText="<%$RIResources:Shared,Failure Reason%>" ItemStyle-Width="25%" ItemStyle-VerticalAlign="top">
            <ItemTemplate>
                <asp:HiddenField ID="_hfRIFailureCauseReasSeq" runat="server" Value='<%# Bind("RIFAILURECAUSEREASSEQID") %>' />
                <asp:HiddenField ID="_hfFailureCauseReasSeq" runat="server" Value='<%# Bind("FAILURECAUSEREASSEQID") %>' />
                <asp:label id="_lbReason" runat="server" Text='<%# FixCrLf(Eval("causereason")) %>' Style="word-break: break-all" ></asp:label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$RIResources:Shared,Root Cause %>">
            <ItemTemplate>
                <asp:CheckBoxList ID="_cblFailureRootCause" runat="server" RepeatDirection="horizontal"></asp:CheckBoxList>
                <IP:AdvancedTextBox id="_tbRootCause" runat="server" expandheight="true" width="98%"
                    rows="2" textmode="MultiLine" maxlength="4000" text='<%# FixCrLf(Eval("rootcausedescription")) %>' style="font-family: Verdana; font-size: 12px; "></IP:AdvancedTextBox>
            </ItemTemplate>
            </asp:TemplateField>
            </Columns>
            </asp:GridView>
            </td>
            <td style="width:5%" valign="top"><asp:Button id="_btnAddRootCause" runat="server" Text="<%$RIResources:Shared,Finish %>" visible="false" /></td>
        </tr>

    </table>--%>
    </div>


    <div id="_divUpdateAWObjects" runat="server" visible="false">
    
    <table id="_tbEventUpdate" runat="server" width="90%">
        <tr class="Header">
            <td align="left" >
            <asp:Label ID="_lbEventUpdate" runat="server" SkinID="labelWhite" Text="<%$RIResources:Shared,Failure Event %>"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="_ddlFailureEventUpdate" runat="server" EnableViewState="true" Enabled="false"></asp:DropDownList>
                <IP:AdvancedTextBox  ID="_tbFailureEventUpdate" runat="server" Style="font-family: Verdana;
                    font-size: 12px;" rows="2" textmode="MultiLine" maxlength="2000" Width="75%" expandheight="true" autopostback="true"></IP:AdvancedTextBox >
            </td>
        </tr>
    </table>
    
    <table id="_tbModeUpdate" runat="server" visible="true" width="100%">
        <tr class="Header">
            <td align="left" style="width:25%">
                <asp:Label ID="_lbModeUpdate" runat="server" SkinID="labelWhite" Text="<%$RIResources:Shared,Failure Mode %>"/>
            </td>
            <td align="left">
                <asp:Label ID="_lbModeUpdateDesc" runat="server" SkinID="labelWhite" Text="<%$RIResources:Shared,List ALL the Ways This Type of Failure Event Can Occur or Has Ocurred in the Past. Enter Additional Ways.%>"/>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:CheckBoxList ID="_cblFailureModeUpdate" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="modeChange" ></asp:CheckBoxList>
                <asp:GridView DataKeyNames="rifailuremodeseqid" ID="_gvFailureModeUpdate" runat="server" CssClass="Border" AutoGenerateColumns="false" Width="100%" ShowHeader="false">
                <Columns>
                <asp:TemplateField ItemStyle-Width="25%">
                    <ItemTemplate>
                        <asp:HiddenField ID="_hfRIFailureModeSeq" runat="server" Value='<%# Bind("rifailuremodeseqid") %>' />
                        <IP:AdvancedTextBox id="_tbFailureModeUpdate" runat="server" Width="98%" rows="2" textmode="MultiLine" maxlength="2000" text='<%# Bind("failuremodeother") %>' Style="font-family: Verdana;
                            font-size: 12px;" ontextchanged="modeChange"></IP:AdvancedTextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                </Columns>
                </asp:GridView>
            </td>
            <td style="width:5%" valign="top">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <IP:AdvancedTextBox id="_tbFailureModeUpdate2" runat="server" Width="98%" rows="2" textmode="MultiLine" maxlength="2000" Style="font-family: Verdana;
                    font-size: 12px;" ontextchanged="modeChange"></IP:AdvancedTextBox>
            </td>
        </tr> 
    </table>
    
    <table id="_tbCauseUpdate" runat="server" width="100%">
        <tr class="Header">
            <td align="left" style="width:25%">
                <asp:Label ID="_lbCauseUpdate" runat="server" SkinID="labelWhite" Text="<%$RIResources:RI,Proven Failure Cause(s) %>"/>
            </td>
            <td align="left">
                <asp:Label ID="_lbCauseUpdatedesc" runat="server" SkinID="labelWhite" Text="<%$RIResources:Shared,List Proven Failure Causes for Failure Modes selected/entered.%>"/>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <asp:Repeater ID="_rpFailureCauseUpdate" runat="server">
                <ItemTemplate>
                    <asp:Table ID="Table1" runat="server" CssClass="Border" BorderColor="White" borderwidth="3" Width="100%">
                        <asp:TableRow>            
                            <asp:TableCell>
                            <asp:HiddenField ID="_hfRIFailureModeSeq" runat="server" Value='<%# Bind("rifailuremodeseqid") %>' />
                            <asp:HiddenField ID="_hfFailureModeSeq" runat="server" Value='<%# Bind("failureModeseqid") %>' />
                            <asp:Label ID="_lbFailureMode" runat="server" text='<%# FixCrLf(Eval("failuremodeother")) %>' > </asp:Label>
                            <asp:Label ID="_lbFailureModeDesc" runat="server" text='<%# FixCrLf(Eval("failuremodedesc")) %>' > </asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                            <asp:GridView ID="_gvfailureCauseUpdate" runat="server" Width="100%" AutoGenerateColumns="false" ShowHeader="false" OnRowDataBound="_gvFailureCauseUpdateBind">
                            <Columns>
                                <asp:TemplateField ItemStyle-VerticalAlign="top" ItemStyle-Width="25%" ShowHeader="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="_hfRIFailureCauseSeq" runat="server" Value='<%# Bind("rifailurecauseseqid") %>' />
                                    <asp:HiddenField ID="_hfFailureCauseSeq" runat="server" Value='<%# Bind("failureCauseseqid") %>' />
                                    <asp:HiddenField ID="_hfFailureModeSeq" runat="server" Value='<%# Bind("failureModeseqid") %>' />
                                    <asp:CheckBoxList ID="_cblFailureCauseUpdate" runat="server" RepeatDirection="horizontal" OnSelectedIndexChanged="causeChange" ></asp:CheckBoxList>
                                    <IP:AdvancedTextBox id="_tbFailureCauseOtherUpdate" runat="server" Width="98%" rows="2" textmode="MultiLine" maxlength="2000" text='<%# Bind("failurecauseother") %>' expandheight="true" Style="font-family: Verdana; font-size: 12px;"  ontextchanged="causeChange"></IP:AdvancedTextBox>                                    
                                </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            </asp:GridView>
                            <asp:CheckBoxList ID="_cblFailureCauseUpdateNew" runat="server" RepeatDirection="horizontal" OnSelectedIndexChanged="causeChange" ></asp:CheckBoxList>
                            <IP:AdvancedTextBox id="_tbFailureCauseOtherUpdate2" runat="server" Width="98%" rows="2" textmode="MultiLine" maxlength="2000" Style="font-family: Verdana;
                                font-size: 12px;" ontextchanged="causeChange"></IP:AdvancedTextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </ItemTemplate>
            </asp:Repeater>
            </td>  
            <td style="width:5%" valign="top"><asp:Button id="_btnFailureCauseUpdate" runat="server" Text="<%$RIResources:Shared,Update %>" Visible="false"/></td>
        </tr>
    </table>

    <table id="_tbReasonUpdate" runat="server" width="100%">
        <tr class="Header">
            <td  align="left" style="width:25%">
                <asp:Label ID="_lbReasonUpdate" runat="server" SkinID="labelWhite" Text="<%$RIResources:RI,Failure Reason %>"/>/>
            </td>
            <td align="left">
                <asp:Label ID="_lbReasonUpdateDesc" runat="server" SkinID="labelWhite" Text="<%$RIResources:Shared,List Possible Reasons for the MOST LIKELY Proven Failure Cause%>"/>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <asp:Repeater ID="_rpFailureReasonUpdate" runat="server">
                <ItemTemplate>
                    <asp:Table ID="Table1" runat="server" CssClass="Border" BorderColor="White" borderwidth="3" Width="100%">
                        <asp:TableRow>            
                            <asp:TableCell>
                            <asp:HiddenField ID="_hfRIFailureCauseSeq" runat="server" Value='<%# Bind("rifailurecauseseqid") %>' />
                            <asp:HiddenField ID="_hfFailureCauseSeq" runat="server" Value='<%# Bind("failurecauseseqid") %>' />
                            <asp:Label ID="_lbFailureCause" runat="server" text='<%# FixCrLf(Eval("failurecausedesc")) %>' > </asp:Label>
                            <asp:Label ID="Label11" runat="server" text='<%# FixCrLf(Eval("failurecauseother")) %>' > </asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                            <asp:GridView ID="_gvfailureReasonUpdate" runat="server" Width="100%" AutoGenerateColumns="false" ShowHeader="false" OnRowDataBound="_gvFailureReasonUpdateBind">
                            <Columns>
                                <asp:TemplateField ItemStyle-VerticalAlign="top" ItemStyle-Width="25%" ShowHeader="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="_hfRIFailureReasonSeq" runat="server" Value='<%# Bind("rifailurecausereasseqid") %>' />
                                    <asp:HiddenField ID="_hfFailureReasonSeq" runat="server" Value='<%# Bind("failurecausereasseqid") %>' />
                                    <asp:HiddenField ID="_hfFailureCauseSeq" runat="server" Value='<%# Bind("failurecauseseqid") %>' />
                                    <asp:CheckBoxList ID="_cblFailureReasonUpdate" runat="server" RepeatDirection="horizontal" OnSelectedIndexChanged="reasonChange" ></asp:CheckBoxList>
                                    <IP:AdvancedTextBox id="_tbFailureReasonOtherUpdate" runat="server" expandheight="true" Width="98%" rows="2" textmode="MultiLine" maxlength="2000" text='<%# Bind("causereason") %>' Style="font-family: Verdana;
                                        font-size: 12px;" ontextchanged="reasonChange"></IP:AdvancedTextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            </asp:GridView>
                              <asp:CheckBoxList ID="_cblFailureReasonUpdateNew" runat="server" RepeatDirection="horizontal" OnSelectedIndexChanged="reasonChange" ></asp:CheckBoxList>
                              <IP:AdvancedTextBox id="_tbFailureReasonOtherUpdate2" runat="server" expandheight="true" Width="98%" rows="2" textmode="MultiLine" maxlength="2000" Style="font-family: Verdana;
                            font-size: 12px;" ontextchanged="reasonChange"></IP:AdvancedTextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </ItemTemplate>
            </asp:Repeater>
            </td>  
            <td style="width:5%" valign="top"><asp:Button id="_btnFailureReasonUpdate" runat="server" Text="<%$RIResources:Shared,Update %>" Visible="false"/></td>
        </tr>
    </table>

    <table id="_tblRootCauseUpdate" runat="server" width="100%">
        <tr class="Header">
            <td align="left" style="width:25%">
                <asp:Label ID="_lbRootCauseUpdate" runat="server" SkinID="labelWhite" Text="<%$RIResources:Shared,Verification/Root Cause %>"/>/>
            </td>
            <td align="left">
                <asp:Label ID="_lbRootCauseUpdateDesc" runat="server" SkinID="labelWhite" Text="<%$RIResources:Shared,For the Most Likey Proven Failure Causes. List Possible Root Causes.%>"/>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <asp:Repeater ID="_rpFailureRootCauseUpdate" runat="server">
                <ItemTemplate>
                    <asp:Table ID="Table1" runat="server" CssClass="Border" BorderColor="White" borderwidth="3" Width="100%">
                        <asp:TableRow>            
                            <asp:TableCell>
                            <asp:HiddenField ID="_hfRIFailureCauseReasSeq" runat="server" Value='<%# Bind("RIFAILURECAUSEREASSEQID") %>' />
                            <asp:HiddenField ID="_hfFailureCauseReasSeq" runat="server" Value='<%# Bind("FAILURECAUSEREASSEQID") %>' />
                            <asp:Label ID="_lbFailureCauseReas" runat="server" text='<%# FixCrLf(Eval("causereason")) %>' > </asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                            <asp:GridView ID="_gvfailureRootCauseUpdate" runat="server" Width="100%" AutoGenerateColumns="false" ShowHeader="false" OnRowDataBound="_gvFailureRootCauseUpdateBind">
                            <Columns>
                                <asp:TemplateField ItemStyle-VerticalAlign="top" ItemStyle-Width="25%" ShowHeader="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="_hfRIFailureRootCauseSeq" runat="server" Value='<%# Bind("rifailurerootcauseseqid") %>' />
                                    <asp:HiddenField ID="_hfFailureReasonSeq" runat="server" Value='<%# Bind("failurecausereasseqid") %>' />
                                    <asp:CheckBoxList ID="_cblFailureRootCauseUpdate" runat="server" RepeatDirection="horizontal" OnSelectedIndexChanged="SaveRootCauses" ></asp:CheckBoxList>
                                    <IP:AdvancedTextBox id="_tbFailureRootCauseOtherUpdate" runat="server" expandheight="true" Width="98%" rows="2" textmode="MultiLine" maxlength="2000" text='<%# Bind("rootcausedescription") %>' Style="font-family: Verdana;
                                        font-size: 12px;"></IP:AdvancedTextBox> 
                                </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            </asp:GridView>
                            <asp:CheckBoxList ID="_cblFailureRootCauseUpdateNew" runat="server" RepeatDirection="horizontal" OnSelectedIndexChanged="SaveRootCauses" ></asp:CheckBoxList>
                            <IP:AdvancedTextBox id="_tbFailureRootCauseOtherUpdate2" runat="server" expandheight="true" Width="98%" rows="2" textmode="MultiLine" maxlength="2000" Style="font-family: Verdana;
                                font-size: 12px;"></IP:AdvancedTextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </ItemTemplate>
            </asp:Repeater>
            </td>  
            <td style="width:5%" valign="top"><asp:Button id="_btnFailureRootCauseUpdate" runat="server" Text="<%$RIResources:Shared,Update %>" Visible="false"/></td>
        </tr>
    </table>
  
    </div>
           
    <%--<asp:GridView ID="_gvAW" runat="server">
       <Columns>
                <asp:TemplateField HeaderText="Failure Event" ItemStyle-VerticalAlign="top">
                <ItemTemplate>
                
                    <asp:HiddenField ID="_hfRIFailureEventSeq" runat="server" Value='<%# Bind("rifailureeventseqid") %>' />
                    <asp:Label ID="_lbFailureEvent" runat="server" text='<%# Bind("failureeventdesc") %>' Font-Size="14"> </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="top">
                <ItemTemplate>
                     <asp:GridView ID="_gvAWMode" runat="server">
                        <Columns>
                        <asp:TemplateField HeaderText="Failure Mode" ItemStyle-VerticalAlign="top">
                            <ItemTemplate>
                                <asp:HiddenField ID="_hfRIFailureModeSeq" runat="server" Value='<%# Bind("rifailuremodeseqid") %>' />
                                <asp:label id="_lbFailureModeOther" runat="server" text='<%# Bind("failuremodedesc") %>' Visible="true" ></asp:label>
                            </ItemTemplate>
                         </asp:TemplateField>
                         </Columns></asp:GridView>
                     </ItemTemplate>
                     </asp:TemplateField>
                <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="top">
                <ItemTemplate>
                     <asp:GridView ID="_gvAWCause" runat="server">
                <Columns>
                <asp:TemplateField HeaderText="Failure Mode" ItemStyle-VerticalAlign="top">
                <ItemTemplate>
                <asp:HiddenField ID="_hfRIFailureCauseSeq" runat="server" Value='<%# Bind("rifailurecauseseqid") %>' />
                    <asp:label id="_lbFailureCauseOther" runat="server" text='<%# Bind("failurecausedesc") %>' Visible="true"></asp:label>
                </ItemTemplate></asp:TemplateField>
                
                </Columns></asp:GridView>
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
   </asp:GridView>   --%>

    <asp:Repeater ID="_rpAW" runat="server" onitemcommand="_rpAW_ItemCommand">
        <ItemTemplate>
            <asp:Table runat="server" CssClass="Border" BorderColor="White" borderwidth="5" Width="100%">
            <asp:TableRow cssclass="Header">            
            <asp:TableCell  HorizontalAlign="left" >
                <asp:Label ID="_lbFailureEvent2" runat="server" SkinID="labelWhite" Text="<%$RIResources:Shared,Failure Event %>"/>
            </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>            
           
            <asp:TableCell>
                <asp:HiddenField ID="_hfRIFailureEventSeq2" runat="server" Value='<%# Bind("rifailureeventseqid") %>' />
                <asp:Label ID="_lbFailureEvent4" runat="server" text='<%# FixCrLf(Eval("failureeventdesc")) %>' > </asp:Label>
                <br />
                <asp:LinkButton ID="_btnEditEvent" runat="server" Text="<%$RIResources:Shared,Edit %>" CommandName="Edit" />&nbsp;
                <asp:LinkButton ID="_btnDeleteEvent" runat="server" Text="<%$RIResources:Shared,Delete %>" CommandName="Delete" CommandArgument='<%# Bind("rifailureeventseqid") %>'/>
                <ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" confirmtext="<%$RIResources:Shared,ConfirmDelete %>"
                                targetcontrolid="_btnDeleteEvent">
                </ajaxToolkit:ConfirmButtonExtender><br />
            </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow><asp:TableCell>
                <asp:gridview ID="_gvAWModes" runat="server"  CssClass="Border" BorderColor="Black"
                    BorderWidth="1" AutoGenerateColumns="False" DataKeyNames="rifailuremodeseqid" EnableViewState="false"
                    Width="100%" >
                    <Columns>
                        <asp:TemplateField HeaderText="<%$RIResources:Shared,Failure Mode %>" HeaderStyle-CssClass="Header" HeaderStyle-ForeColor="White" ItemStyle-VerticalAlign="top" ItemStyle-Width="15%" ItemStyle-Font-Bold="false">
                            <ItemTemplate>
                                <asp:Label ID="_lbModes" runat="server" Text='<%# FixCrLf(Eval("failuremodedesc")) %>' Font-Bold="false" Style="word-break: break-all" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="_tbEventUpdate" runat="server" Text='<%# FixCrLf(Eval("failureeventdesc")) %>' ></asp:TextBox>
                                <asp:TextBox ID="_tbModeUpdate" runat="server" Text='<%# FixCrLf(Eval("failuremodedesc")) %>' ></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$RIResources:Shared,FailureCause %>" HeaderStyle-CssClass="Header" HeaderStyle-ForeColor="White" ItemStyle-VerticalAlign="top" ItemStyle-Width="20%" >
                            <ItemTemplate>
                           
                                <asp:Label ID="_lbCauses" runat="server" Text='<%# FixCrLf(Eval("failurecausedesc")) %>' Font-Bold="false"/>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$RIResources:Shared,Failure Reason %>" ItemStyle-Width="30%" HeaderStyle-CssClass="Header" HeaderStyle-ForeColor="White" ItemStyle-VerticalAlign="top" >
                            <ItemTemplate>
                                <asp:Label ID="_lbCauseReasons" runat="server" Text='<%# FixCrLf(Eval("causereason"))  %>' Font-Bold="false" Style="word-break: break-all" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$RIResources:Shared,Verification/Root Cause %>" HeaderStyle-CssClass="Header" HeaderStyle-ForeColor="White" ItemStyle-VerticalAlign="top" ItemStyle-Width="30%" >
                            <ItemTemplate>
                                <asp:Label ID="_lbRootCauses" runat="server" Text='<%# FixCrLf(Eval("rootcausedescription")) %>' Font-Bold="false" Style="word-break: break-all" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </asp:TableCell></asp:TableRow>
            </asp:Table>
        </ItemTemplate>
    </asp:Repeater>
  
</asp:Content>

