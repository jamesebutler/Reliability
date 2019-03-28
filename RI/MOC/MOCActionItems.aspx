<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="MOCActionItems.aspx.vb"
    Inherits="RI_MOCActionItems" Title="MOC Action Items" EnableViewState="True"
    EnableViewStateMac="false" ViewStateEncryptionMode="never" Trace="false" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/RI.master" %>

<script language="vbscript" runat="server"> 
    Sub TaskDescCheck_ServerValidate(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) _
Handles CustomValidator1.ServerValidate
        'If args.Value / 2 = CInt(args.Value / 2) Then
        'args.IsValid = True
        'Else
        'args.IsValid = False
        'End If
        args.IsValid = True
    End Sub
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">
    <Asp:UpdatePanel ID="_udpActionItem" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <script language="JavaScript" type="text/javascript">
  <!--
    function TaskDescCheck(sender, args)
    {
      var TaskDesc = document.getElementById("<%= _tbNewTaskDesc.ClientID %>");
      var EstDate = document.getElementById("ctl00__cphMain__dtNewEstDate__txtDate");
      var Resource = document.getElementById("<%= _ddlNewResource.ClientID %>");
	  var vsSummary = document.getElementById("<%= _vsSummary.ClientID %>");
      if (TaskDesc!=null && EstDate!=null && Resource!=null)
        {
            if (Resource.selectedIndex>0)
            {
            if (TaskDesc.value.length>0 && EstDate.value.length==0 )
            {
		    //Missing Date
		    args.IsValid = false;
		    sender.errormessage = "Estimated Due Date is required.";

           	return;
		    }
	        if (TaskDesc.value.length==0 && EstDate.value.length>0 )
	        {
		    //Missing Desc
		    args.IsValid = false;
   		    sender.errormessage = "Task Description is required.";

           	return;
		    }
	        if (TaskDesc.value.length==0 && EstDate.value.length==0 )
	        {
		    //Missing Desc
		    args.IsValid = false;
   		    var msg = "Estimate Due Date and Task Description are required.";
   		    sender.errormessage = msg;
   		    // Task Description are required.";

           	return;
		    }
		    }
 	    }	
      args.IsValid = true;
    }
   -->
            </script>

            <asp:Label ID="_lblSort" runat="server" Text='<%$ RIResources:Shared,Sort Action Items By%>'></asp:Label>
            <asp:DropDownList ID="_ddlSort" runat="server" AutoPostBack="True">
                <asp:ListItem Value="EstCompDate" Text="<%$ RIResources:Shared,EstDueDate%>" />
                <asp:ListItem Value="Username" Text="<%$ RIResources:Shared,Resource%>" />
                <asp:ListItem Value="Priority" Text="<%$ RIResources:Shared,Priority%>" />
            </asp:DropDownList><br />
            <br />
            <RWG:BulkEditGridView ShowHeader="false" ID="_gvCorrectiveActions" runat="server"
                AutoGenerateColumns="False" DataKeyNames="actionnumber" EnableInsert="False"
                SaveButtonID="" Width="100%" CellPadding="1" EnableViewState="true">
                <columns>
                    <asp:TemplateField>
                        <edititemtemplate>
                    <asp:Table runat=server width ="100%" borderwidth="2">
                    <asp:TableRow>
                        <asp:TableCell width="23%" colspan="2" VerticalAlign="top"> 
                            <asp:Label ID="_lblResource" runat="server" Text='<%$ RIResources:Shared,Resource%>'></asp:Label><br />
                            <asp:DropDownList id="_ddlResource" runat="server" width="250px"
                            DataTextField="person" DataValueField="username">
                            </asp:DropDownList>
                          <asp:HiddenField ID="_rowChanged" runat="server" OnValueChanged="_gvCorrectiveActions.HandleRowChanged" />
                        </asp:TableCell>
                        <asp:TableCell width="23%" VerticalAlign="top">
                            <asp:Label ID="_lblPriority" runat="server" Text='<%$ RIResources:Shared,Priority %>'></asp:Label><br />
                            <asp:DropDownList id="_ddlPriority" runat="server" OnTextChanged="_gvCorrectiveActions.HandleRowChanged">
                                <asp:ListItem Value="Low" Text="<%$ RIResources:Shared,Low %>" />
                                <asp:ListItem Value="Medium" Text="<%$ RIResources:Shared,Medium %>" />
                                <asp:ListItem Value="High" Text="<%$ RIResources:Shared,High %>" />
                            </asp:DropDownList>
                       </asp:TableCell>
   				        <asp:TableCell width="23%" VerticalAlign="top">
                            <asp:Label ID="_lblDueDate" runat="server" Text='<%$ RIResources:Shared,EstDueDate %>'></asp:Label><br />
                            <IP:DateTime ID="_estDueDate" OnCalendarUpdated="_gvCorrectiveActions.HandleRowChanged" DisplayTime="false" runat="server"  DateValue='<%# Container.DataItem("estcompdate") %>' DateLabel="" /></asp:TableCell>
                        <asp:TableCell width="23%" VerticalAlign="top">
                            <asp:Label ID="_lblCompDate" runat="server" Text='<%$ RIResources:Shared,CompDate %>'></asp:Label><br />
                            <IP:DateTime ID="_actCompDate" DisplayTime="false" AllowManualDate="true" runat="server" DateValue='<%# Container.DataItem("actcompdate") %>'  OnCalendarUpdated="_gvCorrectiveActions.HandleRowChanged" DateLabel="" />
                        </asp:TableCell>
                        <asp:TableCell VerticalAlign="top">
                            <asp:Label ID="_lblWO" runat="server" Text='<%$ RIResources:Shared,Work Order %>' ></asp:Label><br />
                            <asp:TextBox id="_tbWO" runat="server" width="75pt" maxlength="10" text='<%# Container.DataItem("wonumber") %>' OnTextChanged="_gvCorrectiveActions.HandleRowChanged"/></asp:TableCell>
                    </asp:TableRow>
                   <asp:TableRow>    
                        <asp:TableCell colspan="3" VerticalAlign="top">
                            <asp:Label ID="_lblTaskDesc" runat="server" Text='<%$ RIResources:Shared,Task Description %>'></asp:Label><br />
                            <IP:AdvancedTextBox id="_tbTask" runat="server" TextMode="MultiLine" ExpandHeight="true" style="font-family:Verdana; font-size:12px" rows="3" maxlength="4000" width="95%" Text='<%# Container.DataItem("taskdescription") %>' OnTextChanged="_gvCorrectiveActions.HandleRowChanged"></IP:AdvancedTextBox>
   				        </asp:TableCell>
                    <asp:TableCell colspan="2" VerticalAlign="top">
                        <asp:Label ID="_lblComments" runat="server" Text='<%$ RIResources:Shared,Comments %>'></asp:Label><br />
                      	<IP:AdvancedTextBox id="_tbComments" runat="server" style="font-family:Verdana; font-size:12px" rows="3" maxlength="4000" width="95%" TextMode="multiline" ExpandHeight="True" Text='<%# Container.DataItem("comments") %>' OnTextChanged="_gvCorrectiveActions.HandleRowChanged"></IP:AdvancedTextBox></asp:TableCell>
                    </asp:TableRow>
   				    </asp:table>   				   
                </edititemtemplate>
                        <headerstyle cssclass="Border" width="100%" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <itemtemplate>
                    <center><asp:button id="_lnkBtnDelete" runat="server" CommandName="Delete" text='<%$riResources:Shared,Delete%>' datatextfield="actionnumber"></asp:button></center>
			    
                </itemtemplate>
                    </asp:TemplateField>
                </columns>
                <headerstyle backcolor="#CCCC99" forecolor="Black" wrap="False" />
                <rowstyle cssclass="Border" />
                <alternatingrowstyle cssclass="BorderSecondary" />
            </RWG:BulkEditGridView>
            <asp:Table ID="_tblNewRow" runat="server" Width="100%" BorderStyle="Outset">
                <asp:TableRow CssClass="Header">
                    <asp:TableCell Width="100%" ColumnSpan="9" Style="text-align: left">
                        <asp:Label ID="_lblEntry" SkinID="LabelWhite" runat="server" Text='<%$ RIResources:Shared,Enter New Action Item %>'></asp:Label><br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="22%">
                        <span class="ValidationError">*</span>
                        <asp:Label ID="_lblNewResource" runat="server" Text='<%$ riResources:Shared,Resource %>'></asp:Label><br />
                        <asp:DropDownList ID="_ddlNewResource" Width="250px" runat="server" DataTextField="person"
                            DataValueField="username" CausesValidation="true">
                            <asp:ListItem Value="00" Text="" Enabled="true" />
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell Width="22%">
                        <span class="ValidationError">*</span>
                        <asp:Label ID="_lblNewPriority" runat="server" Text='<%$ RIResources:Shared,Priority %>'></asp:Label><br />
                        <asp:DropDownList ID="_ddlNewPriority" runat="server">
                            <asp:ListItem Value="Low" Text="<%$ RIResources:Shared,Low %>" />
                            <asp:ListItem Value="Medium" Text="<%$ RIResources:Shared,Medium %>" />
                            <asp:ListItem Value="High" Text="<%$ RIResources:Shared,High %>" />
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell>
                        <span class="ValidationError">*</span>
                        <asp:Label ID="_lblNewEstDate" runat="server" Text="<%$ RIResources:Shared,EstDueDate %>"></asp:Label><br />
                        <IP:DateTime ID="_dtNewEstDate" DisplayTime="false" runat="server" DateLabel="" />
                    </asp:TableCell>
                    <asp:TableCell Width="22%">
                        <asp:Label ID="_lblNewCompDate" runat="server" Text='<%$ RIResources:Shared,CompDate %>'></asp:Label><br />
                        <IP:DateTime ID="_dtNewCompDate" DisplayTime="false" runat="server" DateLabel=""
                            AllowManualDate="true" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Label ID="_lblNewWO" runat="server" Text='<%$ RIResources:Shared,Work Order %>'></asp:Label><br />
                        <asp:TextBox ID="_txtNewWO" runat="server" Width="75pt" MaxLength="10" Text="" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <span class="ValidationError">*</span><asp:Label ID="_lblNewTaskDesc" runat="server"
                            Text='<%$ RIResources:Shared,Task Description %>'></asp:Label><br />
                        <IP:AdvancedTextBox ID="_tbNewTaskDesc" runat="server" TextMode="MultiLine" ExpandHeight="true"
                            Style="font-family: Verdana; font-size: 12px" Rows="3" MaxLength="4000" Width="95%"
                            CausesValidation="true"></IP:AdvancedTextBox>
                        <asp:CustomValidator ID="CustomValidator1" ValidateEmptyText="true" ValidationGroup="ActionItem"
                            runat="server" ClientValidationFunction="TaskDescCheck" ControlToValidate="_tbNewTaskDesc"
                            Display="None" EnableClientScript="true" ErrorMessage="Please enter required fields."
                            Text="Please Enter a Desc" SetFocusOnError="true" />
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="2">
                        <asp:Label ID="_lblNewComments" runat="server" Text='<%$ RIResources:Shared,Comments %>'></asp:Label><br />
                        <IP:AdvancedTextBox ID="_tbNewComments" runat="server" Style="font-family: Verdana;
                            font-size: 12px" Rows="3" MaxLength="4000" Width="95%" TextMode="multiline" ExpandHeight="True"></IP:AdvancedTextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3">
                        <span class="ValidationError">*</span><asp:Label ID="Label5" runat="server" Text='<%$ RIResources:ValidateRequiredFields %>'
                            ForeColor="Red"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br />
            <center>
                <IP:SpellCheck ID="UltimateSpell1" runat="server">
                </IP:SpellCheck>
                <asp:Button ID="_btnSave" runat="server" Text="<%$RIResources:Shared,AddAI%>" ValidationGroup="ActionItem"
                    CausesValidation="True" />
            </center>
            <asp:ValidationSummary ID="_vsSummary" runat="server" CssClass="ValidationError"
                DisplayMode="BulletList" ValidationGroup="ActionItem" HeaderText="<%$ RIResources:Shared,Please provide data for all required fields%>"
                ShowSummary="false" EnableClientScript="true" ShowMessageBox="false" />
            <IP:MessageBox ID="_messageBox" runat="server" ButtonType="OKCancel" />
        </ContentTemplate>
    </Asp:UpdatePanel>
    <center>
        <asp:Button ID="_btnSaveClose" runat="server" Text="<%$RIResources:Shared,SaveCloseAI%>"
            ValidationGroup="ActionItem" OnClientClick="BeginPostBack();" />
    </center>
</asp:Content>
