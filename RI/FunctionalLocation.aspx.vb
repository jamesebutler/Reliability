Imports System.Data
Imports Devart.Data.Oracle

Partial Class RI_FunctionalLocation
    Inherits System.Web.UI.Page
    Public ReadOnly Property selectedValue() As String
        Get
            If Me._tvFunctionalLocation.SelectedValue IsNot Nothing Then
                Return Me._tvFunctionalLocation.SelectedValue
            Else
                Return ""
            End If
        End Get
    End Property
    Public ReadOnly Property selectedText() As String
        Get
            If Me._tvFunctionalLocation.SelectedNode.Text IsNot Nothing Then
                Return Me._tvFunctionalLocation.SelectedNode.Text
            Else
                Return ""
            End If
        End Get
    End Property
    Private mFacility As String = String.Empty
    Public Property Facility() As String
        Get
            Return mFacility.Trim
        End Get
        Set(ByVal value As String)
            mFacility = value
        End Set
    End Property

    Private mCMMSType As String = String.Empty
    Public Property CMMSType() As String
        Get
            Return mCMMSType
        End Get
        Set(ByVal value As String)
            mCMMSType = value
        End Set
    End Property
    Private mEquipmentid As String = String.Empty
    Public Property EquipmentID() As String
        Get
            Return mEquipmentid
        End Get
        Set(ByVal value As String)
            mEquipmentid = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("Siteid") IsNot Nothing Then
                Facility = Request.QueryString("Siteid")
            Else
                Facility = String.Empty
            End If
            If Facility.Length = 0 Then
                Response.Write("<span style='color:Red'>Please select a Facility</span>")
                Exit Sub
            End If
            If Request.QueryString("Equipid") IsNot Nothing Then
                EquipmentID = Request.QueryString("Equipid")
                If EquipmentID.Length > 0 Then
                    Me.PopulateTreeRoot(EquipmentID)
                Else
                    Me.PopulateTreeRoot()
                End If
            Else
                EquipmentID = String.Empty
                Me.PopulateTreeRoot()
            End If

        End If
        Dim sb As New StringBuilder
        sb.Append("expandAll('")
        sb.Append(Me._tvFunctionalLocation.ClientID)
        sb.Append("');")
        'sb.Append("',true);")

        Page.ClientScript.RegisterStartupScript(Page.GetType, "ExpandAll", sb.ToString, True)
    End Sub
    Public Sub PopulateNode(ByVal source As Object, ByVal e As TreeNodeEventArgs)
        Dim currentNode As TreeNode = e.Node '_tvFunctionalLocation.SelectedNode
        Dim currentValue As String = e.Node.Value ' Me._tvFunctionalLocation.SelectedValue
        AddChildNodes(currentNode, currentValue)
    End Sub
    Public Sub PopulateTreeRoot()
        Dim dr As DataTableReader = Nothing
        Dim ds As DataSet = Nothing
        Dim sql As String

        sql = "Select distinct siteid,sitename,rcfaflid, cmmstype FROM refsite a  where rcfaflid<>' ' and siteid='{0}'    order by sitename"
        sql = String.Format(sql, Facility)
        Dim key As String = "RI_FUNCTIONAL_LOCATION_" & RI.SharedFunctions.CreateKey(sql)
        If Cache.Item(key) IsNot Nothing Then
            ds = CType(Cache.Item(key), DataSet)
        Else
            ds = RI.SharedFunctions.GetOracleDataSet(sql)
        End If
        If ds IsNot Nothing Then
            If ds.Tables(0).DefaultView.Count > 0 Then
                Cache.Insert(key, ds, Nothing, DateTime.Now.AddHours(12), TimeSpan.Zero)
            End If
            Dim dv As DataView = ds.Tables(0).DefaultView
            dr = dv.ToTable.CreateDataReader
        End If
        _tvFunctionalLocation.Nodes.Clear()
        Dim newNode As TreeNode
        Dim riLoc As New IP.Bids.Localization.WebLocalization
        If dr IsNot Nothing Then
            If dr.HasRows Then
                Dim parent As Integer = 0
                While dr.Read
                    Dim nodeText As String = riLoc.GetResourceValue(RI.SharedFunctions.DataClean(dr.Item("sitename".ToString))) 'dr.Item("rcfaflid".ToString) & " * " & RI.SharedFunctions.DataClean(dr.Item("sitename".ToString))
                    CMMSType = RI.SharedFunctions.DataClean(dr.Item("cmmstype")).ToUpper
                    If CMMSType = "SAPPM" Or CMMSType = String.Empty Then
                        newNode = New TreeNode(nodeText, CStr(dr.Item("rcfaflid")))
                    ElseIf CMMSType = "EXEGETE" Then
                        newNode = New TreeNode(nodeText, "*" & CStr(dr.Item("SiteId")))
                    Else
                        newNode = New TreeNode(nodeText, "$" & CStr(dr.Item("SiteId")))
                    End If
                    newNode.SelectAction = TreeNodeSelectAction.Expand
                    newNode.PopulateOnDemand = True
                    newNode.ShowCheckBox = False
                    If dr.Item("rcfaflid") IsNot Nothing Then
                        Me._tvFunctionalLocation.Nodes.Add(newNode)
                        parent = Me._tvFunctionalLocation.Nodes.Count - 1
                    End If
                End While
                If dr IsNot Nothing Then
                    dr.Close()
                    dr = Nothing
                End If
            Else
                'no rows
                RI.SharedFunctions.InsertAuditRecord("PopulateTreeRoot", "The following sql statement returned 0 rows: " & sql)
            End If
        End If

    End Sub
    Public Sub PopulateTreeRoot(ByVal EquipmentId As String)
        Dim EquipIDs As String = GetParentPath(EquipmentId.Trim)
        'Facility = "CT"
        PopulateTreeRoot()
        Dim dr As OracleDataReader = Nothing
        Dim node As TreeNode = Nothing
        Dim sql As String = "Select distinct equipmentid,equipmentdesc FROM RefEQUIPMENT a WHERE  equipmentid in ({0}) order by equipmentid"
        sql = String.Format(sql, EquipIDs)
        Dim valuePath As String = String.Empty

        If _tvFunctionalLocation.Nodes.Count > 0 Then
            If _tvFunctionalLocation.Nodes(0) IsNot Nothing Then
                _tvFunctionalLocation.Nodes(0).PopulateOnDemand = False
                node = _tvFunctionalLocation.Nodes(0)
                If Mid(_tvFunctionalLocation.Nodes(0).ValuePath, 1, 1) = "$" Or Mid(_tvFunctionalLocation.Nodes(0).ValuePath, 1, 1) = "*" Then
                    node = AddChildNodes(_tvFunctionalLocation.Nodes(0), _tvFunctionalLocation.Nodes(0).Value, EquipmentId)
                    Exit Sub
                End If
            End If
        End If

        Dim EquipID As New Collections.Hashtable
        dr = RI.SharedFunctions.GetOracleDataReader(sql)
        If dr IsNot Nothing Then
            Dim idCount As Integer = 0
            Do While dr.Read

                If dr.Item("equipmentid") IsNot Nothing Then
                    EquipID(idCount) = CStr(dr.Item("equipmentid"))
                    idCount = idCount + 1
                End If
            Loop
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
            For i As Integer = 0 To EquipID.Count - 1
                If node IsNot Nothing Then
                    If i + 1 < EquipID.Count Then
                        node = AddChildNodes(node, CStr(EquipID(i)), CStr(EquipID(i + 1)))
                        If node IsNot Nothing Then
                            node.PopulateOnDemand = False
                        End If
                    Else
                        node = AddChildNodes(node, CStr(EquipID(i)))
                        If node IsNot Nothing Then
                            node.PopulateOnDemand = False
                        End If
                    End If
                    'If node.Parent IsNot Nothing Then
                    '    node.Parent.Expand()
                    'End If
                End If
                'If valuePath.Length > 0 Then
                '    valuePath = valuePath & "/" & CStr(dr.Item("equipmentid"))
                'Else
                '    valuePath = CStr(dr.Item("equipmentid"))
                'End If
                'If node IsNot Nothing Then
                '    'node = _tvFunctionalLocation.FindNode(valuePath)
                'End If
                'node = _tvFunctionalLocation.FindNode(valuePath)
            Next
            If EquipID.Count = 0 Then
                If node.ChildNodes.Count = 0 Then node.PopulateOnDemand = True
                Exit Sub
            End If

        End If
        If node IsNot Nothing Then
            node.Selected = True
            node.Checked = True
            node.Expand()
        End If
        '_tvFunctionalLocation.Nodes(0).Expand()
    End Sub
    Enum NodeRelationship
        Child = 0
        Sibling = 1
        ParentSibling = 2
        NoReleation = -1
    End Enum
    Private Function InStrCount(ByVal value As String, ByVal delimiter As String) As Integer
        Dim tmpArray As String() = Split(value, delimiter)
        Return tmpArray.Length - 1
    End Function
    Private Function DetermineRelationship(ByVal parent As String, ByVal curValue As String, ByVal newValue As String) As NodeRelationship
        If curValue.Length = newValue.Length And InStr(curValue, parent) > 0 Then
            Return NodeRelationship.Sibling
        ElseIf newValue.Length > curValue.Length And InStrCount(curValue, "-") + 1 <= InStrCount(newValue, "-") Then
            Return NodeRelationship.Child
        ElseIf newValue.Length < curValue.Length Then
            Return NodeRelationship.ParentSibling
        Else
            Return NodeRelationship.NoReleation
        End If
    End Function

    Public Function AddChildNodes(ByVal currentNode As TreeNode, ByVal currentValue As String, Optional ByVal nextValue As String = "") As TreeNode
        Dim dr As DataTableReader
        Dim parentValue As String = currentValue
        Dim newValue As String = String.Empty
        Dim NextNode As TreeNode = Nothing
        Dim currentLevel As Integer = currentValue.Length
        Dim sql As String
        Dim newNodeCount As Integer = 0
        Dim showCheckBoxes As Boolean = True
        Dim ds As DataSet = Nothing
        Dim potentialChildNodes As Boolean = True

        If Mid(currentValue, 1, 1) = "*" Then
            Dim siteid As String = Mid(currentValue, 2, 2)
            sql = "Select distinct rcfaflid,a.siteid,b.sitename,equipmentid,equipmentdesc,criticality,(Select  count(equipmentid) FROM RefEQUIPMENT c, refsite b WHERE c.siteid = b.siteid  and c.equipmentid like a.equipmentid||'%' and (instr(equipmentid,'-',1,1)>0 and instr(equipmentid,'-',1,2)=0)) childNodes  FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid  and length(equipmentid)=3 and a.siteid='{0}'   order by b.sitename,equipmentid"
            sql = String.Format(sql, siteid)
        ElseIf Mid(currentValue, 1, 1) = "$" Then
            Dim siteid As String = Mid(currentValue, 2, 2)
            'sql = "Select  rcfaflid,a.siteid,b.sitename,equipmentid,equipmentdesc  FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid and  (instr(equipmentid,'-',1,1)>0 and instr(equipmentid,'-',1,3)=0) and a.siteid='{0}'   order by b.sitename,equipmentid"
            sql = "Select distinct a.siteid,b.sitename,substr(equipmentid,1,3) equipmentid,criticality,b.sitename||' ('||substr(equipmentid,1,3)||'...)' equipmentdesc,decode((Select distinct count(equipmentid) FROM RefEQUIPMENT c, refsite b WHERE c.siteid = b.siteid and c.siteid='{0}'  and c.equipmentid like a.equipmentid||'%'),0,0,1) childNodes FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid  and  a.siteid='{0}' order by b.sitename,equipmentid"
            sql = String.Format(sql, siteid)
            showCheckBoxes = False
        ElseIf Mid(currentNode.ValuePath, 1, 1) = "$" Then
            Dim siteid As String = Mid(currentNode.ValuePath, 2, 2)
            'sql = "Select  rcfaflid,a.siteid,b.sitename,equipmentid,equipmentdesc  FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid  = 'Yes' and  (instr(equipmentid,'-',1,1)>0 and instr(equipmentid,'-',1,3)=0) and a.siteid='{0}'   order by b.sitename,equipmentid"
            sql = "Select distinct a.siteid,b.sitename,equipmentid,equipmentdesc,criticality,(Select  count(equipmentid) FROM RefEQUIPMENT c, refsite b WHERE c.siteid = b.siteid  and c.equipmentid like a.equipmentid||'%' and (instr(equipmentid,'-',1,{1})>0 and instr(equipmentid,'-',1,{1})=0)) childNodes FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid  and  a.siteid='{0}' and equipmentid like '{1}%' order by b.sitename,equipmentid"
            sql = String.Format(sql, siteid, currentValue)
            potentialChildNodes = False
        Else
            sql = "Select distinct rcfaflid,a.siteid,b.sitename,equipmentid,equipmentdesc,criticality,(Select  count(equipmentid) FROM RefEQUIPMENT c, refsite b WHERE c.siteid = b.siteid and c.equipmentid like a.equipmentid||'%' and (instr(equipmentid,'-',1,{2})>0 and instr(equipmentid,'-',1,{3})=0)) childNodes  FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid  and equipmentid like'{0}-%' and (instr(equipmentid,'-',1,{1})>0 and instr(equipmentid,'-',1,{2})=0)   order by b.sitename,equipmentid"
            currentLevel = Me.InStrCount(currentValue, "-")
            sql = String.Format(sql, currentValue, currentLevel + 1, currentLevel + 2, currentLevel + 3)
        End If


        'Dim key As String = "FunctionalLocation_" & RI.SharedFunctions.CreateKey(sql)
        Dim key As String = "RI_FUNCTIONAL_LOCATION_" & RI.SharedFunctions.CreateKey(sql)

        If Cache.Item(key) IsNot Nothing Then
            ds = CType(Cache.Item(key), DataSet)
        Else
            ds = RI.SharedFunctions.GetOracleDataSet(sql)
            If ds.Tables(0).DefaultView.Count > 0 Then
                Cache.Insert(key, ds, Nothing, DateTime.Now.AddHours(12), TimeSpan.Zero)
            End If
        End If
        dr = ds.Tables(0).CreateDataReader 'RI.SharedFunctions.GetOracleDataReader(sql)
        If dr IsNot Nothing Then
            If dr.HasRows Then
                While dr.Read
                    If dr.Item("equipmentid") IsNot Nothing Then
                        newNodeCount += 1
                        newValue = CStr(dr.Item("Equipmentid"))
                        Dim relationship As NodeRelationship
                        If newNodeCount = 1 Then ' first record is a child
                            relationship = NodeRelationship.Child
                        Else
                            relationship = NodeRelationship.Sibling
                        End If
                        'relationship = Me.DetermineRelationship(parentValue, currentValue, newValue)
                        Dim newNodeIndex As Integer = -1
                        Dim newNode As TreeNode = New TreeNode(newValue & " * " & RI.SharedFunctions.DataClean(dr.Item("equipmentdesc".ToString) & " * " & RI.SharedFunctions.DataClean(dr.Item("Criticality"))), newValue)

                        'newNode.PopulateOnDemand = True
                        newNode.SelectAction = TreeNodeSelectAction.Expand
                        If dr.Item("childNodes") IsNot Nothing Then
                            If CStr(dr.Item("childNodes")) = "0" Then
                                potentialChildNodes = False
                            Else
                                potentialChildNodes = True
                            End If
                        Else
                            potentialChildNodes = True
                        End If
                        If potentialChildNodes = False Then

                            newNode.PopulateOnDemand = False
                        Else
                            If newNode.ChildNodes.Count = 0 Then
                                newNode.PopulateOnDemand = True
                            Else
                                newNode.PopulateOnDemand = False
                            End If
                        End If

                        newNode.ShowCheckBox = showCheckBoxes

                        Select Case relationship
                            Case NodeRelationship.Child
                                currentNode.ChildNodes.Add(newNode) 'New TreeNode(dr.Item("equipmentid".ToString) & "__" & RI.SharedFunctions.DataClean(dr.Item("equipmentdesc".ToString)), dr.Item("equipmentid".ToString)))
                                currentNode = currentNode.ChildNodes.Item(currentNode.ChildNodes.Count - 1)
                            Case NodeRelationship.Sibling
                                currentNode.Parent.ChildNodes.Add(newNode) 'New TreeNode(dr.Item("equipmentid".ToString) & "__" & RI.SharedFunctions.DataClean(dr.Item("equipmentdesc".ToString)), dr.Item("equipmentid".ToString)))
                                currentNode = currentNode.Parent.ChildNodes.Item(currentNode.Parent.ChildNodes.Count - 1)
                            Case NodeRelationship.ParentSibling
                                currentNode.Parent.Parent.ChildNodes.Add(newNode)
                                currentNode = currentNode.Parent.Parent.ChildNodes.Item(currentNode.Parent.Parent.ChildNodes.Count - 1)
                        End Select
                        If nextValue.Length > 0 And newValue = nextValue Then
                            NextNode = currentNode
                            'NextNode.Checked = True
                        End If
                        'currentNode.Selected = True
                        'currentNode.Expanded = True
                        'currentValue = CStr(dr.Item("equipmentid"))
                    End If
                End While
                If dr IsNot Nothing Then
                    dr.Close()
                    dr = Nothing
                End If
                If ds IsNot Nothing Then
                    ds = Nothing
                End If
            Else
                currentNode.Target = "populated"
                currentNode.NavigateUrl = "http://#"
            End If
        End If
        If NextNode IsNot Nothing Then
            Return NextNode
        Else
            Return currentNode.Parent
        End If
    End Function
    Private Function GetParentPath(ByVal equipmentID As String) As String
        Dim arr() As String = Split(equipmentID, "-")
        Dim sb As New StringBuilder
        Dim equipID As String = String.Empty

        For i As Integer = 0 To arr.Length - 1
            If sb.Length > 0 Then sb.Append(",")
            sb.Append("'")
            If equipID.Length > 0 Then
                equipID = equipID & "-" & arr(i)
            Else
                equipID = arr(i)
            End If
            sb.Append(equipID)
            sb.Append("'")
        Next
        Return sb.ToString
    End Function
    'Private Sub ReturnValue(ByVal selectedValue As String)
    '    Dim sb As New StringBuilder
    '    If Request.QueryString IsNot Nothing Then
    '        Dim parentControl As String = Request.QueryString("Parent")
    '        Dim targetField As String = Request.QueryString("targetField")
    '        sb.Append("<script language='javascript'>window.opener.document.forms(0).")
    '        sb.Append(parentControl)
    '        sb.Append(".value='")
    '        sb.Append(selectedText)
    '        ' sb.Append("';window.opener.document.forms(0).")
    '        'sb.Append(targetField)
    '        'sb.Append(".innerHTML='")
    '        'sb.Append(selectedText)            
    '        sb.Append("';window.opener.customClickhandler();window.resizeTo(")
    '        sb.Append("document.body.clientWidth+30,500)")
    '        sb.Append(";</script>")
    '        'Page.ClientScript.RegisterStartupScript()
    '        'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "SetFunctionalLocationValue", sb.ToString, True)
    '        If Not Page.ClientScript.IsClientScriptBlockRegistered(Page.GetType, "SetFunctionalLocationValue") Then
    '            Page.ClientScript.RegisterClientScriptBlock(Page.GetType, "SetFunctionalLocationValue", sb.ToString, False)
    '        End If
    '        _lblHeader.Text = Resources.Shared.lblFunctionalLocation & ": " & selectedValue
    '    End If
    'End Sub




    Protected Sub _tvFunctionalLocation_TreeNodeDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles _tvFunctionalLocation.TreeNodeDataBound
        e.Node.Expanded = True
    End Sub
End Class
