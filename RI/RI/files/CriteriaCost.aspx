<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CriteriaCost.aspx.vb" Inherits="RI_CriteriaCost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Untitled Page</title>
	
</head>
<body leftmargin=5 topmargin=0>
	<form id="form1" runat="server">
		<div class="Content">
			<span style="text-align:left" class="ContentHeader">
				Reliability Recordable Criteria (Paper Mills):</span>
			<br /><br />
				<b>Incident with Financial Impact of $25,000 or more</b>
			
			<ul type="square">
				<li>Repair Costs (Materials/Labor both in-house and contracted) </li>
				<li>Substitution Costs (Example: Gas for Oil) </li>
				<li>Loss of Direct Cost Materials (Fiber, Energy, Chemicals, Finishing Materials) </li>
				<li>Collateral Damage</li></ul>
						
				<b>Incident with 3 hours outage for Major Process</b>
			<ul type="square">
				<li>Unscheduled downtime means that the repair is not in the weekly schedule.</li><li>
					3 hrs of Cumulative Downtime events over a 24 hour period for common cause is a
					recordable.</li><li>Multiple events occurring concurrently during a continuous downtime
						event count as one recordable. The recordable should be entered for the most significant
						contributor. </li>
				<li>All 3 hour events are a recordable unless the decision to delay the repair was made
					on a non bottlenecked process to minimize repair cost. In this case, total repair
					time must be &lt; 3 hrs to not be a recordable. </li>
				<li>Major processes are those that would be presented on the flow sheet of the mill's
					manufacturing process. </li>
				<li>Swapping to an inline spare is not a recordable unless it takes 3 hours to switch
					it or it meets the cost criteria.</li></ul>
			<%--<br />
			<p align="left" class="ContentHeader">
				Reliability Recordable Criteria (Wood Products):</p>
			<ul type="square">
				<li>2 hours of downtime and/or $5,000 in total costs. </li>
				<li>2 hour delay in a scheduled maintenance outage. </li>
				<li>1 hour delay in between shift maintenance outages.</li></ul>--%>
			
			<span style="text-align:left" class="ContentHeader">
				Calculating the Cost of Reliability Incident:&nbsp;
			</span>
			<p align="left">
				The total cost of a reliability incident includes the repair or replacement costs
				of the equipment, the loss of direct cost materials (fiber/raw materials/chemicals/energy)
				due to the failure, the incremental costs associated with the incident such as burning
				gas instead of bark, and any collateral damage caused by the failure.</p>
			<span style="text-align:left" class="ContentHeader">
				Difference between Cost of Relibility Incident and Total Financial Impact:
			</span>
			<p>
				The total financial impact is the Reliability Incident’s Costs plus the lost profitability
				due to failure impact on finished tons. For example, if a caustic plant failure
				shuts down the pulp mill and the mill’s machine production is eventually reduced
				by 100 tons, then the lost profitability is the contribution of those 100 tons.</p>			
		</div>
	</form>
</body>
</html>
