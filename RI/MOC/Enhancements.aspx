<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="Enhancements.aspx.vb" Inherits="MOC_MOCEnhancements" 
Title="MOC Enhancements" Trace="false" EnableViewState="true" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="_cntEnhancements" ContentPlaceHolderID="_cphMain" Runat="Server">
 <center><table style="width: 100%" cellpadding="0" cellspacing="2">
        <tr class="Border">
            <td style="width: 800px; ">
            <b>11/2013 Enhancements</b><br />
            </td></tr>                
        <tr class="Border">
            <td align="center" style="width: 800px;">
                <br />
            My MOC's<br />
            New Page that lists all pending MOC's for signed on user.<br />
        </td></tr>
        <tr class="Border">
            <td align="center" style="width: 800px;">
                <br />
            View/Update<br />
            Added MOC Number to search options<br />
        </td></tr>
        <tr class="Border">
            <td align="center" style="width: 800px;">
                <br />
            Enter MOC<br />
            New field - Estimated Costs<br />
            Added status to system checklist tasks <br />
        </td></tr>
        <tr class="Border">
            <td align="center" style="width: 800px;">
                <br />
            Email<br />            
            Added MOC Initiator to emails generated from the web site.<br />
        </td></tr>
        <tr><td><br />   </td></tr>   
        <tr class="Border">
            <td style="width: 800px; ">
            <b>3/2013 Enhancements</b><br />
            </td></tr>                
        <tr class="Border">
            <td align="center" style="width: 800px;">
                <br />
            Approvals<br />
            Ability to select roles or individual as an approver.  If role is selected, only one person from that role needs 
            to respond in order for the MOC to proceed. <br />
        </td></tr>
        <tr class="Border">
            <td align="center" style="width: 800px;">
                <br />
            View Page <br />
            Initiator drop down list dependent on selected facility<br />
        </td></tr>
        <tr class="Border">
            <td style="width: 800px;">
                <br />
            Default Approvers <br />
            Ability to have default approvers for Business Unit Area, Classification and Categories<br />
        </td></tr>
        <tr class="Border">
            <td style="width: 800px">
                <br />
            USD Ticket Generation <br />
            USD Ticket will be generated for PI and or Proficy MOCs, once they are approved.  The ticket will 
            go to the local MEAS team.<br />
        </td>  </tr>
        <tr><td><br />   </td></tr>   
        <tr class="Border">
            <td style="width: 800px;">
            <b>11/2012 Enhancements</b><br /></td></tr>
                
        <tr class="Border">
            <td style="width: 800px;">
                <br />
            MOC Locked<br />
            Once an MOC has been initiated and has approvers assigned, certain fields can no longer be changed.  <br />
Once an MOC has been approved, changes can only be made to the approver list and system checklist.  <br />
This is to prevent the original intent of the MOC from being changed.  Comments have been added so user can add any additional comments.<br />
 </td></tr>
    <tr class="Border">
        <td style="width: 800px; ">
            <br />
                Added No Approvers status - MOC has been entered but no Approvers have been assigned. 
        </td>
    </tr>
    <tr class="Border">
        <td style="width: 800px; ">
            <br />
            Only MOC initiator can remove approvers/informed users from the MOC.<br />
        </td>
    </tr>
    <tr class="Border">
        <td style="width: 800px;">
            <br />Changed Endorser to Informed<br />
        </td>
    </tr>
    <tr class="Border">
        <td style="width: 800px;">
            <br />Added Attachment count to button<br />
        </td>
    </tr>
    <tr class="Border">
        <td style="width: 800px;">
            <br />Added Notification to Menu - allows users to see the default approvers/informed users that show up when a MOC is initiated.<br />
        </td>
    </tr>
    <tr class="Border">
        <td style="width: 800px;">
        </td>
    </tr>
    </table></center>
</asp:Content>

