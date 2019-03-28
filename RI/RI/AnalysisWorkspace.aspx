<%@ Page Language="VB" MasterPageFile="../RI.master" AutoEventWireup="false" CodeFile="../RI/AnalysisWorkspace.aspx.vb" Inherits="WorkspaceAnalysis" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<%@ Register Namespace="RealWorld.Grids" TagPrefix="rwg" %>
<asp:Content ID="_cphMain" ContentPlaceHolderID="_cphMain" runat="Server">
<Asp:UpdatePanel ID="_udpAW" runat="server" UpdateMode="Conditional">
<ContentTemplate>
     <asp:Table runat="server" ID="Table2" Width="100%">                
    <asp:TableRow CssClass="Header">
    <asp:TableCell Width="50%">
        <asp:Button ID="_btnExampleTop" runat="server" Text="<%$RIResources:Shared,Example %>" width="100%" />
    </asp:TableCell>
    <asp:TableCell Width="50%">
        <asp:Button ID="_btnPrintTop" runat="server" Text="<%$RIResources:Shared,PrintPage %>" width="100%" />
    </asp:TableCell>
    </asp:TableRow>
    </asp:Table>
    <br />
    <center>
        <asp:Label ID="_lblInstructions" runat="server" Style="font-weight: bold; font-size: 9pt;
            font-family: Verdana" Text="<%$RIResources:Shared,AWInstructions %>" Width="100%"></asp:Label>
        <br />
    </center>
    <asp:FormView ID="_gvEvent" runat="server" BackColor="#CCCC99" DataKeyNames="rcfaevent"
        DefaultMode="Edit" EnableViewState="true" HeaderStyle-ForeColor="black" Width="100%" >
        <EditItemTemplate>
            <asp:Table runat="server" CellPadding="2" CellSpacing="1" Style="width: 100%;">
                <asp:TableRow CssClass="Header">
                    <asp:TableCell Style="width: 100%; text-align: center">
                        <asp:Label ID="_lblEvent" SkinID="LabelWhite" runat="server" Text='<%$RIResources:Shared,Event%>'></asp:Label><br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow CssClass="Border">
                    <asp:TableCell Style="width: 100%; text-align: center">
                        <asp:Label ID="_lblAWEvent" runat="server" Text='<%$RIResources:Shared,AWEvent%>'></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow CssClass="Border">
                    <asp:TableCell>
                        <IP:AdvancedTextBox ID="_txtrcfaevent" runat="server" Rows="2" Style="font-family: Verdana;
                            font-size: 12px;" ExpandHeight="True" Text='<%# Container.DataItem("rcfaevent") %>' TextMode="MultiLine"
                            Width="100%" MaxLength="4000"></IP:AdvancedTextBox>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </EditItemTemplate>
    </asp:FormView>
    <asp:Table runat="server" ID="_tblHeading">                
        <asp:TableRow CssClass="Header">
            <asp:TableCell Width="5%"></asp:TableCell>
            <asp:TableCell width="27%" Style="text-align: center">
                <asp:Label ID="_lblEvent" SkinID="LabelWhite" runat="server" Text='<%$RIResources:Shared,Modes%>'></asp:Label><br />
            </asp:TableCell>
            <asp:TableCell width="27%" Style="text-align: center">
                <asp:Label ID="_lblCause" SkinID="LabelWhite" runat="server" Text='<%$RIResources:Shared,FailureCause%>'></asp:Label><br />
            </asp:TableCell>
            <asp:TableCell width="30%" Style="text-align: center">
                <asp:Label ID="_lblVerification" SkinID="LabelWhite" runat="server" Text='<%$RIResources:Shared,Verification%>'></asp:Label><br />
            </asp:TableCell>
            <asp:TableCell Width="5%"> 
                <asp:Label ID="_lbl" SkinID="LabelWhite" runat="server" Text=''></asp:Label><br />
            </asp:TableCell>
        </asp:TableRow>
        </asp:Table>
    <rwg:BulkEditGridView ID="_dgWorkspace" runat="server" AutoGenerateColumns="False"
       CellPadding="3" DataKeyNames="RCFAAW_SEQ_ID" EnableInsert="True"
        HeaderStyle-BackColor="#CCCC99" HeaderStyle-ForeColor="black" HeaderStyle-Wrap="false"
        Width="100%">
        <Columns>
            <asp:TemplateField HeaderStyle-Width="5%" HeaderText='<%$RIResources:Shared,Item%>'>
            <edititemtemplate>
                <asp:TextBox id="_txtSortOrder" runat="server" style="font-family:Verdana; font-size:12px" width="85%" maxlength="3" text='<%# Container.DataItem("sortorder") %>'></asp:TextBox>
				<ajaxToolkit:FilteredTextBoxExtender ID="_ftbe_sort" runat="server"
                TargetControlID="_txtSortOrder" FilterType="numbers" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
			</edititemtemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="30%" HeaderText='<%$RIResources:Shared,AWModes%>'>
                <edititemtemplate>
				<IP:AdvancedTextBox id="_txtFailureMode" runat="server" style="font-family:Verdana; font-size:12px" rows="2" maxlength="4000" width="95%" TextMode="multiline" ExpandHeight="True" Text='<%# Container.DataItem("rcfafailuremode") %>'></IP:AdvancedTextBox>
			</edititemtemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="30%" HeaderText='<%$RIResources:Shared,AWFailureCause%>'>
                <edititemtemplate>
				<IP:AdvancedTextBox id="_txtHypothesis" runat="server" style="font-family:Verdana; font-size:12px" rows="2" maxlength="4000" width="95%" TextMode="multiline" ExpandHeight="True" Text='<%# Container.DataItem("rcfafailurecause") %>'></IP:AdvancedTextBox>
			</edititemtemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="30%" HeaderText='<%$RIResources:Shared,AWVerification%>'>
                <edititemtemplate>
				<IP:AdvancedTextBox id="_txtVerification" runat="server" style="font-family:Verdana; font-size:12px" rows="2" maxlength="4000" width="95%" TextMode="multiline" ExpandHeight="True" Text='<%# Container.DataItem("rcfaverification") %>'></IP:AdvancedTextBox>
			</edititemtemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="0%">
                <edititemtemplate>
				<asp:TextBox id="_txtSeqid" visible="false" runat="server" text='<%# Container.DataItem("rcfaaw_seq_id") %>'></asp:TextBox>
			</edititemtemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <itemtemplate>
                <asp:button id="_lnkBtnDelete" runat="server" CommandName="Delete" text='<%$RIResources:Shared,Delete%>' datatextfield="rcfaaw_seq_id"></asp:button>
			</itemtemplate> 
            </asp:TemplateField>
        </Columns>
        <selectedrowstyle cssclass="BorderSecondary" />
        <pagerstyle backcolor="#CCCC99" forecolor="Black" horizontalalign="Right" />
        <alternatingrowstyle cssclass="Border" />
    </rwg:BulkEditGridView>

    <asp:Table runat="server" ID="_tblNewRowHeader">                
        <asp:TableRow CssClass="Header">
            <asp:TableCell Width="10%"></asp:TableCell>
            <asp:TableCell width="30%" Style="text-align: center">
                <asp:Label ID="_lblNewMode" SkinID="LabelWhite" runat="server" Text='<%$RIResources:Shared,Modes%>'></asp:Label><br />
            </asp:TableCell>
            <asp:TableCell width="30%" Style="text-align: center">
                <asp:Label ID="_lblNewCause" SkinID="LabelWhite" runat="server" Text='<%$RIResources:Shared,FailureCause%>'></asp:Label><br />
            </asp:TableCell>
            <asp:TableCell width="30%" Style="text-align: center">
                <asp:Label ID="_lblNewVeri" SkinID="LabelWhite" runat="server" Text='<%$RIResources:Shared,Verification%>'></asp:Label><br />
            </asp:TableCell>
        </asp:TableRow>
        </asp:Table>

        <asp:Table ID="_tblNewRow" runat="server" Width="100%">
        <asp:TableHeaderRow CssClass="Border" HorizontalAlign="Center">
            <asp:TableCell Width="10%" >
                <asp:Label ID="_lblHeaderTask2" runat="server" BackColor="#CCCC99" Text='<%$RIResources:Shared,Item%>'></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="30%">
                <asp:Label ID="Label1" runat="server" BackColor="#CCCC99" Text="<%$RIResources:Shared,AWModes%>"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="30%">
                <asp:Label ID="Label2" runat="server" BackColor="#CCCC99" Text='<%$RIResources:Shared,AWFailureCause%>'></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="30%">
                <asp:Label ID="Label3" runat="server" BackColor="#CCCC99" Text='<%$RIResources:Shared,AWVerification%>'></asp:Label>
            </asp:TableCell>
        </asp:TableHeaderRow>
        <asp:TableRow BackColor="#CCCC99" >            
            <asp:TableCell>
                <asp:TextBox ID="_tbSortOrder" runat="server" style="font-family:Verdana; font-size:12px" maxlength="3" width="85%" Text=""></asp:TextBox>
				<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                TargetControlID="_tbSortOrder" FilterType="numbers" ValidChars="1234567890"></ajaxToolkit:FilteredTextBoxExtender>
            </asp:TableCell>
            <asp:TableCell>
                <IP:AdvancedTextBox ID="_tbFailureMode" runat="server" style="font-family:Verdana; font-size:12px" rows="2" ExpandHeight="true" maxlength="4000" width="95%" TextMode="multiline" Text=""></IP:AdvancedTextBox>
            </asp:TableCell>
            <asp:TableCell>
                <IP:AdvancedTextBox ID="_tbFailureCause" runat="server" style="font-family:Verdana; font-size:12px" rows="2" ExpandHeight="true" maxlength="4000" width="95%" TextMode="multiline" Text=""></IP:AdvancedTextBox>
            </asp:TableCell>
            <asp:TableCell>
                <IP:AdvancedTextBox ID="_tbVerification" runat="server" style="font-family:Verdana; font-size:12px" rows="2" ExpandHeight="true" maxlength="4000" width="95%" TextMode="multiline" Text=""></IP:AdvancedTextBox>
            </asp:TableCell>
            </asp:TableRow>
    </asp:Table>    

    <center>
        <IP:SpellCheck id="btnSpellCheck" runat="server"></IP:SpellCheck>
        <asp:Button ID="_btnAddMode" runat="server" Text="<%$RIResources:Shared,AddMode%>"/>
        <%--<asp:Button ID="_btnSave" runat="server" Text="<%$Resources:Shared,lblSaveCloseAW%>" />--%>
    </center>
</ContentTemplate>
</Asp:UpdatePanel>
<center><asp:Button ID="_btnSaveClose" runat="server" Text="<%$RIResources:Shared,SaveCloseAW%>" />&nbsp;</center>
    <center>&nbsp;</center>
<asp:Table runat="server" ID="Table1" Width="100%">                
    <asp:TableRow CssClass="Header">
    <asp:TableCell Width="50%">
        <asp:Button ID="_btnExampleBtm" runat="server" Text="<%$RIResources:Shared,Example%>" width="100%"/>
    </asp:TableCell>
    <asp:TableCell Width="50%">
        <asp:Button ID="_btnPrintBtm" runat="server" Text="<%$RIResources:Shared,PrintPage%>" width="100%" />
    </asp:TableCell>
</asp:TableRow>
</asp:Table>
</asp:Content>

