
Imports System.data
Namespace RI
    Partial Class RI_ucFunctionalLocationTree
        Inherits System.Web.UI.UserControl

        'Public Property Text() As String
        '    Get
        '        Return Me._txtFunctionalLocation.Text
        '    End Get
        '    Set(ByVal value As String)
        '        Me._txtFunctionalLocation.Text = value
        '    End Set
        'End Property
        'Private mSiteid As String = String.Empty
        'Public Property SiteID() As String
        '    Get
        '        Return mSiteid
        '    End Get
        '    Set(ByVal value As String)
        '        mSiteid = value
        '        If value.Length > 0 Then
        '            Me.AutoCompleteExtender1.ContextKey = value
        '        End If
        '    End Set
        'End Property
        'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '    Dim sb As New StringBuilder
        '    sb.Append("DisplayFunctionalLocation('")
        '    sb.Append(Page.ResolveUrl("~/FunctionalLocation.aspx"))
        '    sb.Append("?parent=")
        '    sb.Append(Me._txtFunctionalLocation.UniqueID.ToString)
        '    sb.Append("&targetField=")
        '    sb.Append(Me._txtFunctionalLocation2.UniqueID.ToString)
        '    sb.Append("','")
        '    sb.Append(Me._txtFunctionalLocation.UniqueID.ToString)        
        '    sb.Append("');return false;")
        '    'Me._btnFunctionalLocation.OnClientClick = sb.ToString
        '    If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "StartEndCalendar") Then
        '        Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "StartEndCalendar", Page.ResolveClientUrl("~/ri/User Controls/Common/FunctionalLocation.js"))
        '    End If
        '    'sb.Length = 0
        '    ''sb.Append("eval(""")
        '    'sb.Append(Me._txtFunctionalLocation2.ClientID)
        '    'sb.Append(".value=")
        '    'sb.Append(Me._txtFunctionalLocation.ClientID)
        '    'sb.Append(".value")
        '    'Me._txtFunctionalLocation.Attributes.Add("onMouseup", sb.ToString)
        '    'Me._txtFunctionalLocation.Attributes.Add("onKeyup", sb.ToString)
        '    If SiteID.Length > 0 Then
        '        Me.AutoCompleteExtender1.ContextKey = SiteID
        '    End If
        'End Sub
        'Public Sub SetEquipmentDescription(ByVal equipmentId As String)
        '    If equipmentId.Length = 0 Then Exit Sub
        '    Dim dr As System.Data.DataTableReader = Nothing
        '    Dim sql As String = "Select equipmentId,equipmentDesc FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid and b.webapp = 'Yes' and EquipmentID like '{0}%' and rownum=1 order by EquipmentID"
        '    sql = String.Format(sql, equipmentId)

        '    dr = RI.SharedFunctions.GetOracleDataSet(sql).CreateDataReader
        '    Do While dr.Read
        '        Me._txtFunctionalLocation.Text = RI.SharedFunctions.DataClean(dr.Item("equipmentId"))
        '        Me._txtFunctionalLocation2.Text = RI.SharedFunctions.DataClean(dr.Item("equipmentDesc"))
        '    Loop
        'End Sub
        'Public Sub PopulateFunctionalLocation(ByVal siteID As String)
        '    If siteID.Length = 0 Then Exit Sub
        '    Dim dr As System.Data.DataTableReader = Nothing
        '    Dim sql As String = "select rcfaflid,siteid,sitename from refsite where siteid='{0}' and rcfaflid is not null and cmmstype in('SAPPM','Exegete')  "
        '    sql = String.Format(sql, siteID)
        '    Me._txtFunctionalLocation.Text = String.Empty
        '    Me._txtFunctionalLocation2.Text = String.Empty
        '    dr = RI.SharedFunctions.GetOracleDataSet(sql).CreateDataReader
        '    Do While dr.Read
        '        Me._txtFunctionalLocation.Text = RI.SharedFunctions.DataClean(dr.Item("rcfaflid"))
        '        Me._txtFunctionalLocation2.Text = RI.SharedFunctions.DataClean(dr.Item("siteName"))
        '    Loop
        'End Sub
        'Public Sub PopulateFunctionalLocation(ByVal SiteId As String, ByVal BusArea As String)
        '    Dim dr As DataTableReader = Nothing
        '    Dim ds As DataSet = Nothing
        '    Try
        '        If BusArea.Length = 0 Then
        '            PopulateFunctionalLocation(SiteId)
        '            Exit Sub             
        '        End If
        '        Dim sql As String = "Select FLID FROM RefSiteArea where RISuperArea|| ' - ' ||Subarea='{0}'  and siteid='{1}' and FLID is not null"
        '        sql = String.Format(sql, BusArea, SiteId)
        '        Dim key As String = "RI_FUNCTIONAL_LOCATION_" & RI.SharedFunctions.CreateKey(sql)
        '        If Cache.Item(key) IsNot Nothing Then
        '            ds = CType(Cache.Item(key), DataSet)
        '        Else
        '            ds = RI.SharedFunctions.GetOracleDataSet(sql)
        '        End If
        '        If ds IsNot Nothing Then
        '            If ds.Tables(0).DefaultView.Count > 0 Then
        '                Cache.Insert(key, ds, Nothing, DateTime.Now.AddHours(12), TimeSpan.Zero)
        '            End If
        '            dr = ds.Tables(0).CreateDataReader
        '            If dr IsNot Nothing Then
        '                dr.Read()
        '                If dr.HasRows Then
        '                    Me._txtFunctionalLocation.Text = RI.SharedFunctions.DataClean(dr.Item("flid"))
        '                    SetEquipmentDesc(RI.SharedFunctions.DataClean(dr.Item("flid")))
        '                End If
        '            End If
        '        End If
        '    Catch ex As Exception
        '        Throw New ApplicationException("Exception Occured")
        '    Finally
        '        If ds IsNot Nothing Then ds = Nothing
        '        If dr IsNot Nothing Then dr = Nothing
        '    End Try
        'End Sub
        'Public Sub PopulateFunctionalLocation(ByVal SiteId As String, ByVal BusArea As String, ByVal LineBreak As String)
        '    Dim dr As DataTableReader = Nothing
        '    Dim ds As DataSet = Nothing
        '    Try

        '        If BusArea.Length = 0 Then
        '            PopulateFunctionalLocation(SiteId)
        '            Exit Sub
        '        ElseIf LineBreak.Length = 0 Then
        '            PopulateFunctionalLocation(SiteId, BusArea)
        '            Exit Sub
        '        End If
        '        Dim sql As String = "Select FLID,RISuperArea|| ' - ' ||Subarea BusArea,area|| ' - ' ||LineBreak LineBreak FROM RefSiteArea where RISuperArea|| ' - ' ||Subarea='{0}' and area|| ' - ' ||linebreak='{1}' and siteid='{2}' and FLID is not null"
        '        sql = String.Format(sql, BusArea, LineBreak, SiteId)
        '        Dim key As String = "RI_FUNCTIONAL_LOCATION_" & RI.SharedFunctions.CreateKey(sql)
        '        If Cache.Item(key) IsNot Nothing Then
        '            ds = CType(Cache.Item(key), DataSet)
        '        Else
        '            ds = RI.SharedFunctions.GetOracleDataSet(sql)
        '        End If
        '        If ds IsNot Nothing Then
        '            If ds.Tables(0).DefaultView.Count > 0 Then
        '                Cache.Insert(key, ds, Nothing, DateTime.Now.AddHours(12), TimeSpan.Zero)
        '            End If
        '            dr = ds.Tables(0).CreateDataReader
        '            If dr IsNot Nothing Then
        '                dr.Read()
        '                If dr.HasRows Then
        '                    Me._txtFunctionalLocation.Text = RI.SharedFunctions.DataClean(dr.Item("flid"))
        '                    SetEquipmentDesc(RI.SharedFunctions.DataClean(dr.Item("flid")))
        '                End If
        '            End If
        '        End If
        '    Catch ex As Exception
        '        Throw New ApplicationException("Exception Occured")
        '    Finally
        '        If ds IsNot Nothing Then ds = Nothing
        '        If dr IsNot Nothing Then dr = Nothing
        '    End Try
        'End Sub

        'Public Sub SetEquipmentDesc(ByVal flid As String)
        '    If flid.Length = 0 Then Exit Sub
        '    Dim dr As System.Data.DataTableReader = Nothing
        '    Dim sql As String = "Select equipmentdesc FROM refequipment where equipmentid='{0}'"
        '    sql = String.Format(sql, flid)

        '    Me._txtFunctionalLocation2.Text = ""
        '    dr = RI.SharedFunctions.GetOracleDataSet(sql).CreateDataReader
        '    Do While dr.Read
        '        Me._txtFunctionalLocation2.Text = RI.SharedFunctions.DataClean(dr.Item("equipmentdesc"))
        '    Loop
        '    If dr IsNot Nothing Then
        '        dr.Close()
        '        dr = Nothing
        '    End If
        'End Sub
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
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                Dim EquipmentID As String
                Me._tvFunctionalLocation.ID = "_tvFunctionalLocation"
                If Request.QueryString("val") IsNot Nothing Then
                    EquipmentID = Request.QueryString("val") '"0627"
                Else
                    EquipmentID = "" '"0627"
                End If
                If EquipmentID.Length > 0 Then
                    Me.PopulateTreeRoot(EquipmentID)
                Else
                    Me.PopulateTreeRoot()
                End If
            End If
        End Sub
        Public Sub PopulateTreeRoot(ByVal EquipmentId As String)
            Dim dr As DataTableReader = Nothing
            Dim ds As DataSet = Nothing
            'Dim rowFilter As String = String.Format("SiteID='{0}'", SiteID)
            Dim EquipmentWhere As String = " and  EquipmentId ='{0}'"
            Dim rowFilter As String = String.Format("EquipmentId='{0}'", EquipmentId)
            Dim sql As String = String.Empty
            If EquipmentId.Length > 0 Then
                If EquipmentId.LastIndexOf("-") = EquipmentId.Length - 1 Then
                    EquipmentId = Left(EquipmentId, EquipmentId.Length - 1)
                End If
                EquipmentWhere = String.Format(EquipmentWhere, EquipmentId)
                sql = "select distinct * from refequipment where equipmentid is not null {0}"
                sql = String.Format(sql, EquipmentWhere)
            End If


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
                'dv.RowFilter = rowFilter
                dr = dv.ToTable.CreateDataReader
            End If

            If dr IsNot Nothing Then
                ' _tvFunctionalLocation.Nodes.Clear()
                '_lblFunctionalLocation.Visible = True
                '_txtFunctionalLocation.Visible = True
                '_btnFunctionalLocation.Visible = True
                If dr.HasRows Then
                    Dim parent As Integer = 0
                    While dr.Read
                        'node.Text = "<span onclick='return false;'>"+NodeLabel+"</span>";
                        Dim nodeText As String = dr.Item("Equipmentid".ToString) & " * " & RI.SharedFunctions.DataClean(dr.Item("EquipmentDesc".ToString))
                        Dim newNode As TreeNode = New TreeNode(nodeText, dr.Item("Equipmentid".ToString))
                        'newNode.SelectAction = TreeNodeSelectAction.SelectExpand
                        'newNode.Target = "MJP"
                        newNode.PopulateOnDemand = True
                        newNode.SelectAction = TreeNodeSelectAction.Expand
                        If dr.Item("Equipmentid") IsNot Nothing Then
                            'If dr.Item("equipmentid").ToString.Length = 4 Then
                            'If InStrCount(dr.Item("equipmentid"), "-") = 0 Or dr.Item("equipmentid") = EquipmentId Then
                            Me._tvFunctionalLocation.Nodes.Add(newNode) 'New TreeNode(dr.Item("equipmentid".ToString) & "__" & RI.SharedFunctions.DataClean(dr.Item("equipmentdesc".ToString)), dr.Item("equipmentid".ToString)))
                            parent = Me._tvFunctionalLocation.Nodes.Count - 1
                            'Else
                            '    If dr.Item("equipmentid") IsNot Nothing Then
                            '        Me._tvFunctionalLocation.Nodes(parent).ChildNodes.Add(newNode) 'New TreeNode(dr.Item("equipmentid".ToString) & "__" & RI.SharedFunctions.DataClean(dr.Item("equipmentdesc".ToString)), dr.Item("equipmentid".ToString)))
                            '    End If
                            'End If
                        End If
                    End While
                End If
                If Me._tvFunctionalLocation IsNot Nothing Then
                    'Me._tvFunctionalLocation.ExpandAll()
                End If
            End If
        End Sub
        Public Sub PopulateTreeRoot()
            Dim dr As DataTableReader = Nothing
            Dim ds As DataSet = Nothing
            'Dim rowFilter As String = String.Format("SiteID='{0}'", SiteID)
            Dim sql As String

            sql = "Select siteid,sitename,rcfaflid, cmmstype FROM refsite a  where rcfaflid<>' ' and webapp='Yes'   order by sitename"

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
                'dv.RowFilter = rowFilter
                dr = dv.ToTable.CreateDataReader
            End If

            If dr IsNot Nothing Then
                ' _tvFunctionalLocation.Nodes.Clear()
                '_lblFunctionalLocation.Visible = True
                '_txtFunctionalLocation.Visible = True
                '_btnFunctionalLocation.Visible = True
                If dr.HasRows Then
                    Dim parent As Integer = 0
                    Dim riLoc As New IP.Bids.Localization.WebLocalization
                    While dr.Read
                        'node.Text = "<span onclick='return false;'>"+NodeLabel+"</span>";
                        Dim nodeText As String = riLoc.GetResourceValue(RI.SharedFunctions.DataClean(dr.Item("sitename".ToString))) 'dr.Item("rcfaflid".ToString) & " * " & RI.SharedFunctions.DataClean(dr.Item("sitename".ToString))
                        Dim newNode As TreeNode
                        If RI.SharedFunctions.DataClean(dr.Item("cmmstype")).ToUpper = "SAPPM" Or RI.SharedFunctions.DataClean(dr.Item("cmmstype")) = String.Empty Then
                            newNode = New TreeNode(nodeText, dr.Item("rcfaflid".ToString))
                        ElseIf RI.SharedFunctions.DataClean(dr.Item("cmmstype")).ToUpper = "EXEGETE" Then
                            newNode = New TreeNode("* " & nodeText, "*" & dr.Item("SiteId"))
                        Else
                            newNode = New TreeNode("$ " & nodeText, "$" & dr.Item("SiteId"))
                        End If
                        newNode.SelectAction = TreeNodeSelectAction.Expand
                        'newNode.Target = "MJP"                    
                        newNode.PopulateOnDemand = True
                        newNode.ShowCheckBox = False
                        If dr.Item("rcfaflid") IsNot Nothing Then
                            'If dr.Item("equipmentid").ToString.Length = 4 Then
                            'If InStrCount(dr.Item("equipmentid"), "-") = 0 Or dr.Item("equipmentid") = EquipmentId Then
                            Me._tvFunctionalLocation.Nodes.Add(newNode) 'New TreeNode(dr.Item("equipmentid".ToString) & "__" & RI.SharedFunctions.DataClean(dr.Item("equipmentdesc".ToString)), dr.Item("equipmentid".ToString)))
                            parent = Me._tvFunctionalLocation.Nodes.Count - 1
                            'Else
                            '    If dr.Item("equipmentid") IsNot Nothing Then
                            '        Me._tvFunctionalLocation.Nodes(parent).ChildNodes.Add(newNode) 'New TreeNode(dr.Item("equipmentid".ToString) & "__" & RI.SharedFunctions.DataClean(dr.Item("equipmentdesc".ToString)), dr.Item("equipmentid".ToString)))
                            '    End If
                            'End If
                        End If
                    End While
                End If
                If Me._tvFunctionalLocation IsNot Nothing Then
                    'Me._tvFunctionalLocation.ExpandAll()
                End If
            End If
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
        Sub PopulateNode(ByVal source As Object, ByVal e As TreeNodeEventArgs)
            Dim dr As DataTableReader 'OracleDataReader        
            Dim currentValue As String = e.Node.Value ' Me._tvFunctionalLocation.SelectedValue
            Dim parentValue As String = currentValue
            Dim newValue As String = String.Empty
            Dim currentNode As TreeNode = e.Node '_tvFunctionalLocation.SelectedNode
            Dim currentLevel As Integer = currentValue.Length
            Dim sql As String
            Dim newNodeCount As Integer = 0
            Dim showCheckBoxes As Boolean = True
            Dim ds As DataSet = Nothing

            If Mid(currentValue, 1, 1) = "*" Then
                Dim siteid As String = Mid(currentValue, 2, 2)
                sql = "Select  rcfaflid,a.siteid,b.sitename,equipmentid,equipmentdesc  FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid and b.webapp = 'Yes' and length(equipmentid)=3 and a.siteid='{0}'   order by b.sitename,equipmentid"
                sql = String.Format(sql, siteid)
            ElseIf Mid(currentValue, 1, 1) = "$" Then
                Dim siteid As String = Mid(currentValue, 2, 2)
                'sql = "Select  rcfaflid,a.siteid,b.sitename,equipmentid,equipmentdesc  FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid and b.webapp = 'Yes' and  (instr(equipmentid,'-',1,1)>0 and instr(equipmentid,'-',1,3)=0) and a.siteid='{0}'   order by b.sitename,equipmentid"
                sql = "Select distinct a.siteid,b.sitename,substr(equipmentid,1,3) equipmentid,b.sitename||' ('||substr(equipmentid,1,3)||'%)' equipmentdesc FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid and b.webapp = 'Yes' and  a.siteid='{0}' order by b.sitename,equipmentid"
                sql = String.Format(sql, siteid)
                showCheckBoxes = False
            ElseIf Mid(e.Node.ValuePath, 1, 1) = "$" Then
                Dim siteid As String = Mid(e.Node.ValuePath, 2, 2)
                'sql = "Select  rcfaflid,a.siteid,b.sitename,equipmentid,equipmentdesc  FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid and b.webapp = 'Yes' and  (instr(equipmentid,'-',1,1)>0 and instr(equipmentid,'-',1,3)=0) and a.siteid='{0}'   order by b.sitename,equipmentid"
                sql = "Select distinct a.siteid,b.sitename,equipmentid,equipmentdesc FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid and b.webapp = 'Yes' and  a.siteid='{0}' and equipmentid like '{1}-%' order by b.sitename,equipmentid"
                sql = String.Format(sql, siteid, currentValue)
            Else
                sql = "Select  rcfaflid,a.siteid,b.sitename,equipmentid,equipmentdesc  FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid and b.webapp = 'Yes' and equipmentid like'{0}-%' and (instr(equipmentid,'-',1,{1})>0 and instr(equipmentid,'-',1,{2})=0)   order by b.sitename,equipmentid"
                currentLevel = Me.InStrCount(currentValue, "-")
                sql = String.Format(sql, currentValue, currentLevel + 1, currentLevel + 2)
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
                            newValue = dr.Item("Equipmentid")
                            Dim relationship As NodeRelationship
                            If newNodeCount = 1 Then ' first record is a child
                                relationship = NodeRelationship.Child
                            Else
                                relationship = NodeRelationship.Sibling
                            End If
                            'relationship = Me.DetermineRelationship(parentValue, currentValue, newValue)
                            Dim newNodeIndex As Integer = -1
                            Dim newNode As TreeNode = New TreeNode(newValue & " * " & RI.SharedFunctions.DataClean(dr.Item("equipmentdesc".ToString)), newValue)
                            newNode.SelectAction = TreeNodeSelectAction.Expand
                            newNode.PopulateOnDemand = True
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
                            currentValue = dr.Item("equipmentid")
                        End If
                    End While
                Else
                    currentNode.Target = "populated"
                    currentNode.NavigateUrl = "http://#"
                    'currentNode.SelectAction = TreeNodeSelectAction.Select
                End If
            End If
            'Me._tvFunctionalLocation.CollapseAll()
            '_tvFunctionalLocation.SelectedNodeStyle.BackColor = Drawing.Color.Green
            '_tvFunctionalLocation.SelectedNode.Select()
            'Me._tvFunctionalLocation.SelectedNode.ExpandAll()
            'Dim script As String
            ' script = "window.resizeTo({0},{0})"
            ' script = String.Format(script, Me._pnlFunctionalLocation.Width.ToString, Me._pnlFunctionalLocation.Height)
            'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "resize", script, True)

        End Sub
        Private Sub ReturnValue(ByVal selectedValue As String)
            Dim sb As New StringBuilder
            If Request.QueryString IsNot Nothing Then
                Dim parentControl As String = Request.QueryString("Parent")
                Dim targetField As String = Request.QueryString("targetField")
                sb.Append("<script language='javascript'>window.opener.document.forms(0).")
                sb.Append(parentControl)
                sb.Append(".value='")
                sb.Append(selectedText)
                ' sb.Append("';window.opener.document.forms(0).")
                'sb.Append(targetField)
                'sb.Append(".innerHTML='")
                'sb.Append(selectedText)            
                sb.Append("';window.opener.customClickhandler();window.resizeTo(")
                sb.Append("document.body.clientWidth+30,500)")
                sb.Append(";</script>")
                'Page.ClientScript.RegisterStartupScript()
                'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "SetFunctionalLocationValue", sb.ToString, True)
                If Not Page.ClientScript.IsClientScriptBlockRegistered(Page.GetType, "SetFunctionalLocationValue") Then
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType, "SetFunctionalLocationValue", sb.ToString, False)
                End If
                _lblHeader.Text = RI.SharedFunctions.LocalizeValue("FunctionalLocation") & ": " & selectedValue
            End If
        End Sub
        Private Sub AddChildrenNodes()
            Dim dr As DataTableReader 'OracleDataReader        
            Dim currentValue As String = Me._tvFunctionalLocation.SelectedValue
            Dim parentValue As String = currentValue
            Dim newValue As String = String.Empty
            Dim currentNode As TreeNode = _tvFunctionalLocation.SelectedNode
            Dim currentLevel As Integer = currentValue.Length
            'Dim sql As String = "Select  rcfaflid,a.siteid,b.sitename,equipmentid,equipmentdesc  FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid and b.webapp = 'Yes' and equipmentid like'{0}-%' and length(equipmentid)<={1}   order by b.sitename,equipmentid"
            'Dim sql As String = "Select  rcfaflid,a.siteid,b.sitename,equipmentid,equipmentdesc  FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid and b.webapp = 'Yes' and equipmentid like'{0}-%' and (instr(equipmentid,'-',1,{1})>0 )   order by b.sitename,equipmentid"
            Dim sql As String = "Select  rcfaflid,a.siteid,b.sitename,equipmentid,equipmentdesc  FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid and b.webapp = 'Yes' and equipmentid like'{0}-%' and (instr(equipmentid,'-',1,{1})>0 and instr(equipmentid,'-',1,{2})=0)   order by b.sitename,equipmentid"
            'currentNode.SelectAction = TreeNodeSelectAction.Expand
            Dim ds As DataSet = Nothing
            'currentNode.Target = "populated"
            'currentNode.NavigateUrl = "http://#"

            'Select Case currentLevel
            '    Case 7
            '        currentLevel = 26 '10
            '    Case 10
            '        currentLevel = 26 '14
            '    Case 14
            '        currentLevel = 26 '18
            '    Case 18
            '        currentLevel = 26 '22
            '    Case 22
            '        currentLevel = 26
            '    Case 26
            '        Exit Sub
            'End Select
            currentLevel = Me.InStrCount(currentValue, "-")
            sql = String.Format(sql, currentValue, currentLevel + 1, currentLevel + 9)
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
                            newValue = dr.Item("Equipmentid")
                            Dim relationship As NodeRelationship = Me.DetermineRelationship(parentValue, currentValue, newValue)
                            Dim newNodeIndex As Integer = -1
                            Dim newNode As TreeNode = New TreeNode(newValue & " * " & RI.SharedFunctions.DataClean(dr.Item("equipmentdesc".ToString)), newValue)
                            newNode.SelectAction = TreeNodeSelectAction.None
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
                            currentValue = dr.Item("equipmentid")
                        End If
                    End While
                Else
                    currentNode.Target = "populated"
                    currentNode.NavigateUrl = "http://#"
                    'currentNode.SelectAction = TreeNodeSelectAction.Select
                End If
            End If
            'Me._tvFunctionalLocation.CollapseAll()
            _tvFunctionalLocation.SelectedNodeStyle.BackColor = Drawing.Color.Green
            _tvFunctionalLocation.SelectedNode.Select()
            Me._tvFunctionalLocation.SelectedNode.ExpandAll()
            'Dim script As String
            ' script = "window.resizeTo({0},{0})"
            ' script = String.Format(script, Me._pnlFunctionalLocation.Width.ToString, Me._pnlFunctionalLocation.Height)
            'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "resize", script, True)
        End Sub
    End Class
End Namespace