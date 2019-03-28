<%@ Page Language="VB" EnableTheming="false" MaintainScrollPositionOnPostback="true"
	AutoEventWireup="false" CodeFile="FunctionalLocation.aspx.vb" Inherits="RI_FunctionalLocation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Functional Location</title>

	<script language="javascript" type="text/javascript">
	var lastCheckedNode=null;
	
	function treeSelect(){
    var obj = window.event.srcElement;
    var treeNodeFound = false;
    var equipID="";
    var equipDesc="";
    var hdEquipID = document.getElementById ("_hdFunctionalLocation");
    
    if (obj.tagName == "A"){
        //if (obj.target=="populated") return false
        //else return true;
        if (lastCheckedNode!=null){
			if(lastCheckedNode.checked==true){
				lastCheckedNode.checked=false;}
			}
        obj.checked=true;
        
        var objID = obj.id+'CheckBox';
        objID=objID.replace("_tvFunctionalLocationt","_tvFunctionalLocationn")
        var parentcb = document.getElementById(objID);
        
        if (parentcb!=null){
			parentcb.checked=true;
			lastCheckedNode=parentcb;
			lastCheckedNode.backgroundColor="";
        }
        var modifiedText = obj.innerHTML.split('*');    
        alert(obj.innerHTML);  
		if (modifiedText != null){
			if (modifiedText.length>=2){
				equipID=modifiedText[0];
				equipDesc=modifiedText[1];	
			}
			if (hdEquipID!=null){
				hdEquipID.value = obj.innerHTML;
			}
		
		}
        //SelectedEquipment(equipDesc,equipID,ECFunctionalLocationSearch,ECFunctionalLocationSearchDesc,null);
        //obj.style.backgroundColor="#ff0000"
        }
    else if (obj.tagName =="IMG"){
        return true;}
    else if (obj.type=="checkbox" && obj.tagName=="INPUT"){
        if (lastCheckedNode!=null){
            if(lastCheckedNode.checked==true && lastCheckedNode.id!=obj.id){
				lastCheckedNode.checked=false;}
			}
        lastCheckedNode=obj;
        var modifiedText = obj.title.split('*');      
		if (modifiedText != null){
			if (modifiedText.length>=2){
				equipID=modifiedText[0];
				equipDesc=modifiedText[1];	
			}
			if (hdEquipID!=null){
				hdEquipID.value = obj.title;
			}
		
		}
        //SelectedEquipment(equipDesc,equipID,ECFunctionalLocationSearch,ECFunctionalLocationSearchDesc,null);
        //alert(obj.title);
        return true;
}
    }    

//function TreeviewExpandCollapseAll(treeViewId, expandAll) //pass true/false for expand/collapse all
//    {
//         var displayState = (expandAll == true ? "none" : "block");
//         var treeView = document.getElementById(treeViewId);
//         if(treeView)
//         {
//             var treeLinks = treeView.getElementsByTagName("a");
//             var nodeCount = treeLinks.length;
//             var flag = true;
//            
//             for(i=0;i<nodeCount;i++)
//             {
//                  if(treeLinks[i].firstChild.tagName)
//                  {
//                      if(treeLinks[i].firstChild.tagName.toLowerCase() == "img")
//                      {
//                        var node = treeLinks[i];
//                        var level = parseInt(treeLinks[i].id.substr(treeLinks[i].id.length - 1),10);
//                        var childContainer = GetParentByTagName("table", node).nextSibling;
//                       
//                       if (childContainer!=null)
//                       {
//						   if(flag)
//							{
//								if(childContainer.style.display == displayState)
//								{
//									//The below function comes with the asp.net treeview client library that is
//									//embedded in your page containing a asp.net treeview control
//									var data = eval(treeViewId +"_Data");
//									if (data!=null){
//										TreeView_ToggleNode(data,level,node,'r',childContainer);
//									}
//								}
//								flag = false;
//							}
//							else
//							{
//								if(childContainer.style.display == displayState){
//									var dataID = eval(treeViewId +"_Data");
//									if (dataID!=null){
//										TreeView_ToggleNode(dataID,level,node,'l',childContainer);
//									}
//								
//								}
//							}
//						}
//                     }
//                  }
//             }//for loop ends
//         }
//   }

//    //utility function to get the container of an element by tagname
//   function GetParentByTagName(parentTagName, childElementObj)
//   {
//      var parent = childElementObj.parentNode;
//      if (parent!=null){
//		while(parent.tagName.toLowerCase() != parentTagName.toLowerCase())
//		{
//			parent = parent.parentNode;
//		}
//	}
//    return parent;   
//   }

function expandAll(treeViewId)
    {
         var treeView = document.getElementById(treeViewId);
         var treeLinks = treeView.getElementsByTagName("a");
         var j = true;
         for(i=0;i<treeLinks.length;i++)
         {
             try{
              if(treeLinks[i].firstChild.tagName == "IMG")
              {
                var node = treeLinks[i];
                var level = parseInt(treeLinks[i].id.substr(treeLinks[i].id.length - 1),10);
                var childContainer = document.getElementById(treeLinks[i].id + "Nodes");
                
                if (childContainer!=null){
				   if(j)
					{
						if(childContainer.style.display == "none")
						TreeView_ToggleNode(eval(treeViewId +"_Data"),level,node,'r',childContainer);
						j = false;
					}
					else
					{
						if(childContainer.style.display == "none")
						TreeView_ToggleNode(eval(treeViewId +"_Data"),level,node,'l',childContainer);
					}
				}
				
              }
              }
				catch(err){}
          }
   }           
	</script>

</head>
<body topmargin="0" leftmargin="0">
	<form id="frmFunctionalLocation" runat="server">
		<div>
			<input type="hidden" id="_hdFunctionalLocation" />
			<asp:Panel EnableTheming="false" ID="_pnlFunctionalLocation" runat="server" BackColor="Gray"
				BorderColor="black" BorderWidth="0" Width="99%">
				<asp:Table ID="_tblFunctional" CellPadding="2" CellSpacing="0" runat="server" Width="100%">
					<%--<asp:TableHeaderRow CssClass="Header">
						<asp:TableHeaderCell HorizontalAlign="Left">
							<asp:Label ID="_lblHeader" SkinID="LabelWhite" runat="server" Text="<%$Resources:Shared,lblFunctionalLocation%>"></asp:Label></asp:TableHeaderCell><asp:TableHeaderCell
								HorizontalAlign="Right">
								<asp:Button ID="_btnClose" runat="server" Text="<%$Resources:Shared,lblApplySelection %>" OnClientClick="this.opener.win.hide();return false;" />
							</asp:TableHeaderCell>
					</asp:TableHeaderRow>--%>
					<asp:TableRow CssClass="BorderWhite">
						<asp:TableCell ColumnSpan="2" EnableTheming="false">
							<asp:Panel ID="_pnlTree" runat="server" BorderWidth=0 ScrollBars="Auto" Height="100%">
								<asp:TreeView EnableViewState="false" BorderWidth="0" CssClass="TreeViewBackground"
									SelectedNodeStyle-BorderWidth="3" EnableTheming="false" ID="_tvFunctionalLocation"
									ExpandDepth="0" runat="server" PopulateNodesFromClient="true" ShowCheckBoxes="All"
									ShowLines="True" ShowExpandCollapse="true" EnableClientScript="true" OnTreeNodePopulate="PopulateNode"
									onclick="treeSelect()">
									<HoverNodeStyle BackColor="green" CssClass="BorderWhite" />
									<NodeStyle ChildNodesPadding="1" NodeSpacing="2" CssClass="TreeView" />
								</asp:TreeView>
							</asp:Panel>
						</asp:TableCell>
					</asp:TableRow>
				</asp:Table>
			</asp:Panel>
		</div>
	</form>
</body>
</html>
