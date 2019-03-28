<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="EditActionItems.aspx.vb"
    Inherits="RI_EditActionItems" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">

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
            if (Resource.selectedIndex>=0)
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

    <asp:Table ID="_tblNewRow" runat="server" Width="100%" BorderWidth="0">
        <asp:TableRow CssClass="Header">
            <asp:TableCell Width="100%" ColumnSpan="5" Style="text-align: left">
                <asp:Label ID="_lblEntry" SkinID="LabelWhite" runat="server" Text='<%$ RIResources:Shared,Enter New Action Item %>'></asp:Label><br />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="22%">
                <span class="ValidationError">*</span>
                <asp:Label ID="_lblNewResource" runat="server" Text='<%$ RIResources:Shared,Resource %>'></asp:Label><br />
                <asp:DropDownList ID="_ddlNewResource" runat="server" DataTextField="person" DataValueField="username"
                    CausesValidation="true">
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
                <asp:Label ID="_lblNewWO" runat="server" Text='<%$ RIResources:Shared,Workorder %>'></asp:Label><br />
                <asp:TextBox ID="_txtNewWO" runat="server" Width="75pt" MaxLength="10" Text="" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <span class="ValidationError">*</span><asp:Label ID="_lblNewTaskDesc" runat="server"
                    Text='<%$ RIResources:Shared,TaskDesc %>'></asp:Label><br />
                <IP:AdvancedTextBox ID="_tbNewTaskDesc" runat="server" TextMode="MultiLine" ExpandHeight="true"
                    Style="font-family: Verdana; font-size: 12px" Rows="3" MaxLength="4000" Width="95%"
                    CausesValidation="true"></IP:AdvancedTextBox>
                <%--        	    <asp:CustomValidator runat="server" id="_cvResource" 
			    ValidationGroup="ActionItem" ClientValidationFunction="TaskDescCheck" Display="None"
                ErrorMessage="Please enter required fields" Text="Please Select a Resource" 
                ControlToValidate="_ddlNewResource" EnableClientScript="true"/>
--%>
                <asp:CustomValidator ID="CustomValidator1" ValidateEmptyText="true" ValidationGroup="ActionItem"
                    runat="server" ClientValidationFunction="TaskDescCheck" ControlToValidate="_tbNewTaskDesc"
                    Display="None" EnableClientScript="true" ErrorMessage="Please enter required fields."
                    Text="Please Enter a Desc" SetFocusOnError="true" />
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:Label ID="_lblNewComments" runat="server" Text='<%$ RIResources:Shared,Comments %>'></asp:Label><br />
                <IP:AdvancedTextBox ID="_tbNewComments" runat="server" Style="font-family: Verdana;
                    font-size: 12px" Rows="3" MaxLength="4000" Width="95%" TextMode="multiline" ExpandHeight="True"></IP:AdvancedTextBox></asp:TableCell>
            <asp:TableCell RowSpan="1">
                <asp:Label ID="Label11" runat="server" Text='<%$ RIResources:Shared,Repeat Action %>'></asp:Label><br />
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                    TargetControlID="_txtNewRepeatUnitQty" FilterType="custom" ValidChars="1234567890">
                </ajaxToolkit:FilteredTextBoxExtender>
                <asp:TextBox ID="_txtNewRepeatUnitQty" runat="server" Width="20pt" MaxLength="3" /><br />
                <asp:Label ID="Label12" runat="server" Text='<%$ RIResources:Shared,How Often?%>'></asp:Label><br />
                <asp:DropDownList ID="_ddlNewRepeatUnits" runat="server">
                    <asp:ListItem Value="" Text="" />
                    <asp:ListItem Value="Days" Text="<%$ RIResources:Shared,Day%>" />
                    <asp:ListItem Value="Weeks" Text="<%$ RIResources:Shared,Week%>" />
                    <asp:ListItem Value="Months" Text="<%$ RIResources:Shared,Month%>" />
                    <asp:ListItem Value="Years" Text="<%$ RIResources:Shared,Year%>" />
                </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="5">
                <asp:Label ID="Label5" runat="server" Text='<%$ RIResources:Shared,RequiredFields%>'
                    ForeColor="Red"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="5" HorizontalAlign="center">
                <asp:Button ID="_btnSave" runat="server" Text="<%$ RIResources:Shared,SAVECLOSEAI%>"
                    CausesValidation="true" ValidationGroup="ActionItem" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:ValidationSummary ID="_vsSummary" runat="server" CssClass="ValidationError"
        DisplayMode="BulletList" ValidationGroup="ActionItem" HeaderText="Please provide data for all required fields."
        ShowSummary="false" EnableClientScript="true" ShowMessageBox="false" />
    <IP:MessageBox ID="_messageBox" runat="server" ButtonType="OKCancel" />
</asp:Content>
