<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="ErrorPage.aspx.vb"
    Inherits="RI_Help_ErrorPage" Title="Untitled Page" %>

<%--<%@OutputCache Duration="240" VaryByParam="none" %>--%>
<%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">
    <center><h2>
		Application Error (<asp:Label ID="_lblErrorCount" runat="server"></asp:Label>)</h2>
	<p>
		An error has occured in the application.
	</p></center>
    <table border="1" class="help" cellpadding="2" cellspacing="0" style="width: 90%; height: 80%"
        align="center">
        <tr>
            <th>Problem Reporting</th>
           
            
        </tr>
        
        <tr>
            <td></td>
            <%--<td align="right">
                <asp:HyperLink ID="_imgMyHelp" runat="server" NavigateUrl="http://MyHelp" Target="_blank"
                    ImageUrl="~/Images/MyHelpSm.gif" /><br />
            </td>--%>
        </tr>
        
        <tr>
            <td width="50%" valign="top">
                <br />
                All problem calls directed to GPI Help Desk number: 1-800-329-9630
				<br />

                <br />
                If you experience any problems that are not handled in timely manner or routed correctly,
				please contact &nbsp;<asp:HyperLink ID="HyperLink1" NavigateUrl="Mailto:Josh.Haber@graphicpkg.com"
                    runat="server">Josh.Haber@graphicpkg.com</asp:HyperLink>
            </td>
            <%--<td width="50%" valign="top">
                <br />
                Log in with your network login and password (MYKEYS)
				<br />
                <br />
                Choose RFS, (for Enhancement or new request), Incident/Problem or Inquiry for how
				to perform a certain task.
				<br />
                <br />
                Choose application affected: Reliability (RI/RCFA), Type in the Description and
				Click on Submit A page of articles that may answer the question will be displayed.
				Click on Submit if none of the articles answer the question.
            </td>--%>
        </tr>
    </table>
</asp:Content>
