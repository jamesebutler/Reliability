<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="OutageScope.aspx.vb"
    Inherits="RI_Outage" Title="Outage Scope" EnableViewState="True"
    EnableViewStateMac="false" ViewStateEncryptionMode="never" Trace="false" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<%@ Register Namespace="RealWorld.Grids" TagPrefix="rwg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<script language="vbscript" runat="server"> 
    Sub ScopeCheck_ServerValidate(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) _
Handles CustomValidator1.ServerValidate
        'If args.Value / 2 = CInt(args.Value / 2) Then
        'args.IsValid = True
        'Else
        'args.IsValid = False
        'End If
        args.IsValid = True
    End Sub
</script>

<asp:Content ID="_contentScope" ContentPlaceHolderID="_cphMain" runat="Server">
    <Asp:UpdatePanel ID="_udpScope" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

<script language="JavaScript" type="text/javascript">
  <!--
    function ScopeCheck(sender, args)
    {
      var Desc = document.getElementById("<%= _tbNewDesc.ClientID %>");
      var Sort = document.getElementById("<%= _tbNewSort.ClientID %>");
	  var vsSummary = document.getElementById("<%= _vsSummary.ClientID %>");
      if (Desc!=null && Sort!=null)
        {
            if (Desc.value.length>0 && Sort.value.length==0 )
            {
		    //Missing Sort
		    args.IsValid = false;
		    sender.errormessage = "Sort Order is required.";

           	return;
		    }
	        if (Desc.value.length==0 && Sort.value.length>0 )
	        {
		    //Missing Desc
		    args.IsValid = false;
   		    sender.errormessage = "Description is required.";

           	return;
		    }
	        if (Desc.value.length==0 && Sort.value.length==0 )
	        {
		    //Missing Desc
		    args.IsValid = false;
   		    var msg = "Sort Order and Description are required.";
   		    sender.errormessage = msg;

           	return;
		    }
 	    }	
      args.IsValid = true;
    }
   -->
            </script>

            <RWG:BulkEditGridView ShowHeader="false" ID="_gvScope" runat="server"
                AutoGenerateColumns="False" DataKeyNames="outagescopeseqid" EnableInsert="False"
                SaveButtonID="" Width="100%" CellPadding="1" EnableViewState="true">
                <columns>
                    <asp:TemplateField>
                        <edititemtemplate>
                    <asp:Table runat=server width ="100%" borderwidth="2">
                    <asp:TableRow>
                        <asp:TableCell width="5%" VerticalAlign="top"> 
                            <asp:Label ID="_lblSort" runat="server" Text='<%$ RIResources:Shared,Sort%>'></asp:Label><br />
                            <asp:TextBox id="_tbSort" runat="server" width="90%" MaxLength="5" Text='<%# Container.DataItem("sortorder") %>' OnTextChanged="_gvScope.HandleRowChanged">
                            </asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="_fbeSort" runat="server"
                                TargetControlID="_tbSort" FilterType="custom" ValidChars="1234567890">
                            </ajaxToolkit:FilteredTextBoxExtender>
                       <asp:HiddenField ID="_rowChanged" runat="server" OnValueChanged="_gvScope.HandleRowChanged" />
                        </asp:TableCell>
                        <asp:TableCell width="95%" VerticalAlign="top">
                            <asp:Label ID="_lblDesc" runat="server" Text='<%$ RIResources:Shared,Description %>'></asp:Label><br />
                            <IP:AdvancedTextBox id="_tbDesc" runat="server" TextMode="SingleLine" ExpandHeight="true" style="font-family:Verdana; font-size:12px" rows="1" maxlength="1000" width="95%" Text='<%# Container.DataItem("description") %>' OnTextChanged="_gvScope.HandleRowChanged"></IP:AdvancedTextBox>
   				        </asp:TableCell>
                    </asp:TableRow>
   				    </asp:table>   				   
                        </edititemtemplate>
                        <headerstyle cssclass="Border" width="100%" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <itemtemplate>
                    <center><asp:button id="_lnkBtnDelete" runat="server" CommandName="Delete" text='<%$riResources:Shared,Delete%>' datatextfield="outagescopeseqid"></asp:button></center>
			    
                </itemtemplate>
                    </asp:TemplateField>
                </columns>
                <headerstyle backcolor="#CCCC99" forecolor="Black" wrap="False" />
                <rowstyle cssclass="Border" />
                <alternatingrowstyle cssclass="BorderSecondary" />
            </RWG:BulkEditGridView>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="_fbeNewSort" runat="server"
                                TargetControlID="_tbNewSort" FilterType="custom" ValidChars="1234567890">
                            </ajaxToolkit:FilteredTextBoxExtender>
<asp:Table ID="_tblNewRow" runat="server" Width="100%" BorderStyle="Outset">
                <asp:TableRow CssClass="Header">
                    <asp:TableCell Width="100%" ColumnSpan="3" Style="text-align: left">
                        <asp:Label ID="_lblEntry" SkinID="LabelWhite" runat="server" Text='<%$ RIResources:Shared,Enter Scope %>'></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow VerticalAlign="Top">
                    <asp:TableCell Width="10%">
                        <span class="ValidationError">*</span>
                        <asp:Label ID="_lblNewSort" runat="server" Text='<%$ RIResources:Shared,Sort %>'></asp:Label><br />
                        <asp:TextBox ID="_tbNewSort" runat="server" Style="font-family: Verdana;
                            font-size: 12px" MaxLength="5" Width="90%"></asp:TextBox>
                     </asp:TableCell>
                     <asp:TableCell width="90%">
                        <span class="ValidationError">*</span>
                        <asp:Label ID="_lblNewDesc" runat="server" Text='<%$ RIResources:Shared,Description %>'></asp:Label><br />
                        <IP:AdvancedTextBox ID="_tbNewDesc" runat="server" TextMode="SingleLine" ExpandHeight="true"
                            Style="font-family: Verdana; font-size: 12px" Rows="3" MaxLength="1000" Width="95%"
                            CausesValidation="true"></IP:AdvancedTextBox>
                        <asp:CustomValidator ID="CustomValidator1" ValidateEmptyText="true" ValidationGroup="OutageScope"
                            runat="server" ClientValidationFunction="DescCheck" ControlToValidate="_tbNewDesc"
                            Display="None" EnableClientScript="true" ErrorMessage="Please enter required fields."
                            Text="Please Enter a Description" SetFocusOnError="true" />
                    </asp:TableCell>
               </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <span class="ValidationError">*</span><asp:Label ID="Label5" runat="server" Text='<%$ RIResources:ValidateRequiredFields %>'
                            ForeColor="Red"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br />
            <center>
                <IP:SpellCheck ID="UltimateSpell1" runat="server">
                </IP:SpellCheck>
                <asp:Button ID="_btnSave" runat="server" Text="<%$RIResources:Shared,Save%>" ValidationGroup="ActionItem"
                    CausesValidation="True" />
            </center>
            <asp:ValidationSummary ID="_vsSummary" runat="server" CssClass="ValidationError"
                DisplayMode="BulletList" ValidationGroup="OutageScope" HeaderText="<%$ RIResources:Shared,Please provide data for all required fields%>"
                ShowSummary="false" EnableClientScript="true" ShowMessageBox="false" />
            <IP:MessageBox ID="_messageBox" runat="server" ButtonType="OKCancel" />
        </ContentTemplate>
    </Asp:UpdatePanel>
    <center>
        <asp:Button ID="_btnSaveClose" runat="server" Text="<%$RIResources:Shared,SaveClose%>"
            ValidationGroup="OutageScope" OnClientClick="BeginPostBack();" />
    </center>
</asp:Content>
