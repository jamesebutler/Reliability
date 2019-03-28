Option Strict On
Option Explicit On

Imports System.Data
Imports System.Data.Common
Imports Devart.Data.Oracle

Partial Class ucECSearch
    Inherits System.Web.UI.UserControl

    Public Event ResetControl()
    Public Event PerformSearch(ByVal xml As String, ByVal sortDesc As String)
    Public Event HideSearchResults()
    Public Event NoRecords()

   


    Public Property Text() As String
        Get
            Return Me._txtFunctionalLocationSearch.Text
        End Get
        Set(ByVal value As String)
            Me._txtFunctionalLocationSearch.Text = value
        End Set
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

    Private mBusinessUnit As String = String.Empty
    Public Property BusinessUnit() As String
        Get
            Return mBusinessUnit.Trim
        End Get
        Set(ByVal value As String)
            mBusinessUnit = value
        End Set
    End Property

    Private mArea As String = String.Empty
    Public Property Area() As String
        Get
            Return mArea.Trim
        End Get
        Set(ByVal value As String)
            mArea = value
        End Set
    End Property

    Private mLine As String = String.Empty
    Public Property Line() As String
        Get
            Return mLine.Trim
        End Get
        Set(ByVal value As String)
            mLine = value
        End Set
    End Property

    Public Property Criticality() As String
        Get
            If Me._ddlCriticality.SelectedItem IsNot Nothing Then
                Return Me._ddlCriticality.SelectedValue.Trim
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            If Me._ddlCriticality.Items.FindByValue(value) IsNot Nothing Then
                Me._ddlCriticality.SelectedValue = value
            End If
        End Set
    End Property

    Public Property EquipmentClass() As String
        Get
            If Me._ddlEquipmentClass.SelectedItem IsNot Nothing Then
                Return Me._ddlEquipmentClass.SelectedValue.Trim
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            If Me._ddlEquipmentClass.Items.FindByValue(value) IsNot Nothing Then
                Me._ddlEquipmentClass.SelectedValue = value
            End If
        End Set
    End Property

    Public Property EquipmentType() As String
        Get
            If Me._ddlEquipmentType.SelectedItem IsNot Nothing Then
                Return Me._ddlEquipmentType.SelectedValue.Trim
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            If Me._ddlEquipmentType.Items.FindByValue(value) IsNot Nothing Then
                Me._ddlEquipmentType.SelectedValue = value
            End If
        End Set
    End Property

    Public Property FunctionalLocation() As String
        Get
            Return Me._txtFunctionalLocation.Text.Trim
        End Get
        Set(ByVal value As String)
            Me._txtFunctionalLocation.Text = value
        End Set
    End Property

    Public Property EquipmentDescription() As String
        Get
            Return Me._txtFunctionalLocation2.Text.Trim
        End Get
        Set(ByVal value As String)
            Me._txtFunctionalLocation2.Text = value
        End Set
    End Property

    Private mDivision As String = String.Empty
    Public Property Division() As String
        Get
            Return mDivision.Trim
        End Get
        Set(ByVal value As String)
            mDivision = value
        End Set
    End Property


    ''' <summary>
    ''' Retrieves all of the data required for the dropdown boxes and populates the dropdowns
    ''' </summary>
    ''' <remarks>All of the data is stored in application state for Application("CacheLimit") minutes</remarks>   
    Private Sub PopulateDropDownData()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New OracleParameter
            param.ParameterName = "rsEquipClass"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsEquipType"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "EquipmentSearchDDL"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RI.EquipmentSearchDDL", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 2 Then                    
                    Me._ddlEquipmentClass.DataSource = ds.Tables(0)
                    Me._ddlEquipmentClass.DataTextField = "equipclass"
                    Me._ddlEquipmentClass.DataValueField = "equipclass"

                    Me._ddlEquipmentType.DataSource = ds.Tables(1)
                    Me._ddlEquipmentType.DataTextField = "equiptype"
                    Me._ddlEquipmentType.DataValueField = "equiptype"

                    Me._ddlEquipmentClass.DataBind()
                    Me._ddlEquipmentType.DataBind()
                    Me._ddlEquipmentClass.Items.Insert(0, New ListItem("", ""))
                    Me._ddlEquipmentType.Items.Insert(0, New ListItem("", ""))
                End If
            End If

            _ddlCriticality.Items.Clear()
            _ddlCriticality.Items.Add(New ListItem("", ""))
            For i As Integer = 1 To 9
                Me._ddlCriticality.Items.Add(i.ToString)
            Next

            Me._ddlLimit.Items.Clear()
            For i As Integer = 100 To 1000 Step 100
                _ddlLimit.Items.Add(i.ToString)
            Next

        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Sub

    Private Sub PerformEquipmentSearch()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = Me.Facility
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_bustype"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = Me.BusinessUnit
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_area"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = Me.Area
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_line"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = Me.Line
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_criticality"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = Me.Criticality
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_equipclass"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = Me.EquipmentClass
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_equiptype"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = Me.EquipmentType
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_fl"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = Me.FunctionalLocation
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_desc"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = Me.EquipmentDescription
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_topN"
            param.OracleDbType = OracleDbType.Integer
            param.Value = Me._ddlLimit.SelectedValue
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rs"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "EquipmentSearchDDL"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.equipment_dtl_mill", key, 0)

            If ds IsNot Nothing Then
                'If ds.Tables.Count >= 1 Then
                '    _grvData.Visible = True
                '    _grvData.DataSource = ds.Tables(0)
                '    _grvData.DataBind()
                'End If
            End If

        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Sub
    Public Sub SetContextKey()
        Dim contextKey As String = Facility & "@@" & Me.BusinessUnit & "@@" & Me.Area & "@@" & Me.Line
        Me._aceFunctionalLocation.ContextKey = contextKey
        Me._aceFunctionalDescription.ContextKey = contextKey
        Me._aceFunctionalLocation2.ContextKey = contextKey
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.PopulateDropDownData()
            'Me.PopulateTreeRoot("0627-01-00-010-030-015")
            'Me.PopulateTreeRoot()
        End If
        SetContextKey()
        If Not Page.ClientScript.IsStartupScriptRegistered(Page.GetType, "ucECSearchGlobals") Then
            Dim sb As New StringBuilder           
            sb.Append(GetGlobalJSVar)
            Page.ClientScript.RegisterStartupScript(Page.GetType, "ucECSearchGlobals", sb.ToString, True)
        End If
        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "ucECSearch") Then
            Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "ucECSearch", Page.ResolveClientUrl("~/ri/User Controls/ucECSearch.js"))
        End If
        Me._btnViewUpdate.OnClientClick = "performEquipmentSearch();return false;"
        Me._ddlCriticality.Attributes.Add("onChange", "performEquipmentSearch();return false;")
        Me._ddlEquipmentClass.Attributes.Add("onChange", "performEquipmentSearch();return false;")
        Me._ddlEquipmentType.Attributes.Add("onChange", "performEquipmentSearch();return false;")
        Me._ddlLimit.Attributes.Add("onChange", "performEquipmentSearch();return false;")

        'var grdECSearch = $get('<%=_grvData.clientid%>');
        Me._btnTree.OnClientClick = "displayFunctionalTreeWindow('" & Page.ResolveClientUrl("~/FunctionalLocation.aspx") & "'); return false;"
    End Sub

    Protected Sub _btnViewUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnViewUpdate.Click
        'Me.PerformEquipmentSearch()
        'Me._mpeSearch.Hide()
        'Me._mpeSearch.Show()
    End Sub
    Public Function GetSelectedEquipmentJS(ByVal Equipment As String, ByVal id As String) As String
        Dim sb As New StringBuilder
        sb.Append("Javascript:SelectedEquipment('")
        sb.Append(Equipment)
        sb.Append("','")
        sb.Append(id.ToString)
        sb.Append("','")
        sb.Append(Me._txtFunctionalLocationSearch.ClientID)
        sb.Append("','")
        sb.Append(Me._txtFunctionalLocationSearch2.ClientID)
        sb.Append("','")
        sb.Append(Me._btnCloseEquipmentSearch.ClientID)
        sb.Append("');")
        Return sb.ToString
    End Function

    'Protected Sub _grvData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _grvData.RowDataBound
    '    Try
    '        'If (e.Row.RowState = DataControlRowState.Normal Or e.Row.RowState = DataControlRowState.Alternate Or e.Row.RowState = DataControlRowState.Selected) And e.Row.RowType = DataControlRowType.DataRow Then
    '        '    Dim SelectButton As LinkButton = CType(e.Row.Cells(1).Controls(0), LinkButton)
    '        '    Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)
    '        '    If rowView("EquipmentDesc") IsNot DBNull.Value Then
    '        '        Dim Title As String = RI.SharedFunctions.DataClean(CType(rowView("EquipmentDesc"), String))
    '        '        SelectButton.Text = Title
    '        '    Else
    '        '        SelectButton.Text = "Missing Description"
    '        '    End If
    '        'End If
    '    Catch ex As Exception
    '        Throw New Exception("_dvData_RowDataBound", ex.InnerException)
    '    End Try
    'End Sub
    Public Sub SetEquipmentDescription(ByVal equipmentId As String)
        If equipmentId.Length = 0 Then Exit Sub
        Dim dr As System.Data.DataTableReader = Nothing
        Dim sql As String = "Select equipmentId,equipmentDesc FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid and a.siteid='{1}' and b.webapp = 'Yes' and EquipmentID like '{0}%' and rownum=1 order by EquipmentID"
        sql = String.Format(sql, equipmentId, Facility)

        dr = RI.SharedFunctions.GetOracleDataSet(sql).CreateDataReader
        Do While dr.Read
            Me._txtFunctionalLocationSearch.Text = RI.SharedFunctions.DataClean(dr.Item("equipmentId"))
            Me._txtFunctionalLocationSearch2.Text = RI.SharedFunctions.DataClean(dr.Item("equipmentDesc"))
        Loop
    End Sub
    Public Sub PopulateFunctionalLocation(ByVal siteID As String)
        If siteID.Length = 0 Then Exit Sub
        Dim dr As System.Data.DataTableReader = Nothing
        Dim sql As String = "select rcfaflid,siteid,sitename from refsite where siteid='{0}' and rcfaflid is not null and cmmstype in('SAPPM','Exegete')  "
        sql = String.Format(sql, siteID)
        Me._txtFunctionalLocationSearch.Text = String.Empty
        Me._txtFunctionalLocationSearch2.Text = String.Empty
        dr = RI.SharedFunctions.GetOracleDataSet(sql).CreateDataReader
        Do While dr.Read
            Me._txtFunctionalLocationSearch.Text = RI.SharedFunctions.DataClean(dr.Item("rcfaflid"))
            Me._txtFunctionalLocationSearch2.Text = RI.SharedFunctions.DataClean(dr.Item("siteName"))
        Loop
    End Sub
    Public Sub PopulateFunctionalLocation(ByVal SiteId As String, ByVal BusArea As String)
        Dim dr As DataTableReader = Nothing
        Dim ds As DataSet = Nothing
        Try
            If BusArea.Length = 0 Then
                PopulateFunctionalLocation(SiteId)
                Exit Sub
            End If
            Dim sql As String = "Select FLID FROM RefSiteArea where RISuperArea|| ' - ' ||Subarea='{0}'  and siteid='{1}' and FLID is not null"
            sql = String.Format(sql, BusArea, SiteId)
            Dim key As String = "RI_FUNCTIONAL_LOCATION_" & RI.SharedFunctions.CreateKey(sql)
            If Cache.Item(key) IsNot Nothing Then
                ds = CType(Cache.Item(key), DataSet)
            Else
                ds = RI.SharedFunctions.GetOracleDataSet(sql)
            End If
            If ds IsNot Nothing Then
                If ds.Tables(0).DefaultView.Count > 0 Then
                    'Cache.Insert(key, ds, Nothing, DateTime.Now.AddHours(12), TimeSpan.Zero)
                End If
                dr = ds.Tables(0).CreateDataReader
                If dr IsNot Nothing Then
                    dr.Read()
                    If dr.HasRows Then
                        Me._txtFunctionalLocationSearch.Text = RI.SharedFunctions.DataClean(dr.Item("flid"))
                        SetEquipmentDesc(RI.SharedFunctions.DataClean(dr.Item("flid")))
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New ApplicationException("Exception Occured")
        Finally
            If ds IsNot Nothing Then ds = Nothing
            If dr IsNot Nothing Then dr = Nothing
        End Try
    End Sub
    Public Sub PopulateFunctionalLocation(ByVal SiteId As String, ByVal BusArea As String, ByVal LineBreak As String)
        Dim dr As DataTableReader = Nothing
        Dim ds As DataSet = Nothing
        Try

            If BusArea.Length = 0 Then
                PopulateFunctionalLocation(SiteId)
                Exit Sub
            ElseIf LineBreak.Length = 0 Then
                PopulateFunctionalLocation(SiteId, BusArea)
                Exit Sub
            End If
            Dim sql As String = "Select FLID,RISuperArea|| ' - ' ||Subarea BusArea,area|| ' - ' ||LineBreak LineBreak FROM RefSiteArea where RISuperArea|| ' - ' ||Subarea='{0}' and area|| ' - ' ||linebreak='{1}' and siteid='{2}' and FLID is not null"
            sql = String.Format(sql, BusArea, LineBreak, SiteId)
            Dim key As String = "RI_FUNCTIONAL_LOCATION_" & RI.SharedFunctions.CreateKey(sql)
            If Cache.Item(key) IsNot Nothing Then
                ds = CType(Cache.Item(key), DataSet)
            Else
                ds = RI.SharedFunctions.GetOracleDataSet(sql)
            End If
            If ds IsNot Nothing Then
                If ds.Tables(0).DefaultView.Count > 0 Then
                    'Cache.Insert(key, ds, Nothing, DateTime.Now.AddHours(12), TimeSpan.Zero)
                End If
                dr = ds.Tables(0).CreateDataReader
                If dr IsNot Nothing Then
                    dr.Read()
                    If dr.HasRows Then
                        Me._txtFunctionalLocationSearch.Text = RI.SharedFunctions.DataClean(dr.Item("flid"))
                        SetEquipmentDesc(RI.SharedFunctions.DataClean(dr.Item("flid")))
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New ApplicationException("Exception Occured")
        Finally
            If ds IsNot Nothing Then ds = Nothing
            If dr IsNot Nothing Then dr = Nothing
        End Try
    End Sub

    Public Sub SetEquipmentDesc(ByVal flid As String)
        If flid.Length = 0 Then Exit Sub
        Dim dr As System.Data.DataTableReader = Nothing
        Dim sql As String = "Select equipmentdesc FROM refequipment where equipmentid='{0}'"
        sql = String.Format(sql, flid)

        Me._txtFunctionalLocationSearch2.Text = ""
        dr = RI.SharedFunctions.GetOracleDataSet(sql).CreateDataReader
        Do While dr.Read
            Me._txtFunctionalLocationSearch2.Text = RI.SharedFunctions.DataClean(dr.Item("equipmentdesc"))
        Loop
        If dr IsNot Nothing Then
            dr.Close()
            dr = Nothing
        End If
    End Sub

    Private Function GetGlobalJSVar() As String
        Dim sb As New StringBuilder
        sb.AppendLine()
        sb.Append("var ECFacility='")
        sb.Append(Facility)
        sb.Append("';")
        sb.AppendLine()
        sb.Append("var ECBusiness = '")
        sb.Append(BusinessUnit)
        sb.Append("';")
        sb.AppendLine()
        sb.Append("var ECArea ='")
        sb.Append(Area)
        sb.Append("';")
        sb.AppendLine()
        sb.Append("var ECLine ='")
        sb.Append(Line)
        sb.Append("';")
        sb.AppendLine()        
        sb.Append("var ECCriticality = $get('")
        sb.Append(Me._ddlCriticality.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var ECEquipClass = $get('")
        sb.Append(Me._ddlEquipmentClass.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var ECEquipType =$get('")
        sb.Append(Me._ddlEquipmentType.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var ECEquipmentid =$get('")
        sb.Append(Me._txtFunctionalLocation.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var ECEquipmentDesc =$get('")
        sb.Append(Me._txtFunctionalLocation2.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var ECResults = $get('")
        sb.Append(_pnlResults.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var ECLimit = $get('")
        sb.Append(Me._ddlLimit.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var ECClose = $get('")
        sb.Append(Me._btnCloseEquipmentSearch.ClientID)
        sb.Append("');")
        sb.Append("var ECTreeClose = $get('")
        sb.Append(Me._btnClose.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var ECFunctionalLocationSearch = $get('")
        sb.Append(Me._txtFunctionalLocationSearch.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var ECFunctionalLocationSearchDesc = $get('")
        sb.Append(Me._txtFunctionalLocationSearch2.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var ECTree = $get('")
        sb.Append(Me._tvFunctionalLocation.ClientID)
        sb.Append("');")
        sb.Append("var ECBtnTree = $get('")
        sb.Append(Me._btnTree.ClientID)
        sb.Append("');")
        sb.AppendLine()
        '_txtFunctionalLocationSearch
        Return sb.ToString
    End Function
    Public Sub PopulateTreeRoot(ByVal EquipmentId As String)
        Dim EquipIDs As String = GetParentPath(EquipmentId)        
        PopulateTreeRoot()
        Dim dr As OracleDataReader = Nothing
        Dim node As TreeNode = Nothing
        Dim sql As String = "Select distinct equipmentid,equipmentdesc FROM RefEQUIPMENT a WHERE  equipmentid in ({0}) order by equipmentid"
        sql = String.Format(sql, EquipIDs)
        dr = RI.SharedFunctions.GetOracleDataReader(sql)
        Dim valuePath As String = String.Empty

        If _tvFunctionalLocation.Nodes(0) IsNot Nothing Then
            _tvFunctionalLocation.Nodes(0).PopulateOnDemand = False
            node = _tvFunctionalLocation.Nodes(0)
        End If
        If dr IsNot Nothing Then
            Do While dr.Read
                If dr.Item("equipmentid") IsNot Nothing Then                    
                    If node IsNot Nothing Then
                        node = AddChildNodes(node, CStr(dr.Item("equipmentid")))
                        'If node.Parent IsNot Nothing Then
                        '    node.Parent.Expand()
                        'End If
                    End If
                    If valuePath.Length > 0 Then
                        valuePath = valuePath & "/" & CStr(dr.Item("equipmentid"))
                    Else
                        valuePath = CStr(dr.Item("equipmentid"))
                    End If
                    If node IsNot Nothing Then
                        'node = _tvFunctionalLocation.FindNode(valuePath)
                    End If
                    'node = _tvFunctionalLocation.FindNode(valuePath)
                End If
            Loop
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
        End If
        _tvFunctionalLocation.Nodes(0).Expand()       
    End Sub
    'Function GetNode(ByVal value As String) As TreeNode
    '    For i As Integer = 0 To Me._tvFunctionalLocation.Nodes.Count

    '    Next
    'End Function
    Public Sub PopulateTreeRoot()
        Dim dr As DataTableReader = Nothing
        Dim ds As DataSet = Nothing
        Dim sql As String

        sql = "Select distinct siteid,sitename,rcfaflid, cmmstype FROM refsite a  where rcfaflid<>' ' and siteid='{0}' and webapp='Yes'   order by sitename"
        sql = String.Format(sql, Facility)
        Dim key As String = "RI_FUNCTIONAL_LOCATION_" & RI.SharedFunctions.CreateKey(sql)
        If Cache.Item(key) IsNot Nothing Then
            ds = CType(Cache.Item(key), DataSet)
        Else
            ds = RI.SharedFunctions.GetOracleDataSet(sql)
        End If
        If ds IsNot Nothing Then
            If ds.Tables(0).DefaultView.Count > 0 Then
                'Cache.Insert(key, ds, Nothing, DateTime.Now.AddHours(12), TimeSpan.Zero)
            End If
            Dim dv As DataView = ds.Tables(0).DefaultView
            dr = dv.ToTable.CreateDataReader
        End If
        _tvFunctionalLocation.Nodes.Clear()
        Dim newNode As TreeNode       

        If dr IsNot Nothing Then          
            If dr.HasRows Then
                Dim parent As Integer = 0
                While dr.Read
                    Dim nodeText As String = RI.SharedFunctions.DataClean(dr.Item("sitename".ToString)) 'dr.Item("rcfaflid".ToString) & " * " & RI.SharedFunctions.DataClean(dr.Item("sitename".ToString))
                    If RI.SharedFunctions.DataClean(dr.Item("cmmstype")).ToUpper = "SAPPM" Or RI.SharedFunctions.DataClean(dr.Item("cmmstype")) = String.Empty Then
                        newNode = New TreeNode(nodeText, CStr(dr.Item("rcfaflid")))
                    ElseIf RI.SharedFunctions.DataClean(dr.Item("cmmstype")).ToUpper = "EXEGETE" Then
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
   
    Public Function AddChildNodes(ByVal currentNode As TreeNode, ByVal currentValue As String, Optional ByVal nextValue As String = "") As TreeNode
        Dim dr As DataTableReader 'OracleDataReader 
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
            sql = "Select distinct rcfaflid,a.siteid,b.sitename,equipmentid,equipmentdesc,(Select  count(equipmentid) FROM RefEQUIPMENT c, refsite b WHERE c.siteid = b.siteid and b.webapp = 'Yes' and c.equipmentid like a.equipmentid||'%' and (instr(equipmentid,'-',1,1)>0 and instr(equipmentid,'-',1,2)=0)) childNodes  FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid and b.webapp = 'Yes' and length(equipmentid)=3 and a.siteid='{0}'   order by b.sitename,equipmentid"
            sql = String.Format(sql, siteid)
        ElseIf Mid(currentValue, 1, 1) = "$" Then
            Dim siteid As String = Mid(currentValue, 2, 2)
            'sql = "Select  rcfaflid,a.siteid,b.sitename,equipmentid,equipmentdesc  FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid and b.webapp = 'Yes' and  (instr(equipmentid,'-',1,1)>0 and instr(equipmentid,'-',1,3)=0) and a.siteid='{0}'   order by b.sitename,equipmentid"
            sql = "Select distinct a.siteid,b.sitename,substr(equipmentid,1,3) equipmentid,b.sitename||' ('||substr(equipmentid,1,3)||'%)' equipmentdesc,decode((Select distinct count(equipmentid) FROM RefEQUIPMENT c, refsite b WHERE c.siteid = b.siteid and c.siteid='{0}' and b.webapp = 'Yes' and c.equipmentid like a.equipmentid||'%'),0,0,1) childNodes FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid and b.webapp = 'Yes' and  a.siteid='{0}' order by b.sitename,equipmentid"
            sql = String.Format(sql, siteid)
            showCheckBoxes = False
        ElseIf Mid(currentNode.ValuePath, 1, 1) = "$" Then
            Dim siteid As String = Mid(currentNode.ValuePath, 2, 2)
            'sql = "Select  rcfaflid,a.siteid,b.sitename,equipmentid,equipmentdesc  FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid and b.webapp = 'Yes' and  (instr(equipmentid,'-',1,1)>0 and instr(equipmentid,'-',1,3)=0) and a.siteid='{0}'   order by b.sitename,equipmentid"
            sql = "Select distinct a.siteid,b.sitename,equipmentid,equipmentdesc,(Select  count(equipmentid) FROM RefEQUIPMENT c, refsite b WHERE c.siteid = b.siteid and b.webapp = 'Yes' and c.equipmentid like a.equipmentid||'%' and (instr(equipmentid,'-',1,{1})>0 and instr(equipmentid,'-',1,{1})=0)) childNodes FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid and b.webapp = 'Yes' and  a.siteid='{0}' and equipmentid like '{1}-%' order by b.sitename,equipmentid"
            sql = String.Format(sql, siteid, currentValue)
            potentialChildNodes = False
        Else
            sql = "Select distinct rcfaflid,a.siteid,b.sitename,equipmentid,equipmentdesc,(Select  count(equipmentid) FROM RefEQUIPMENT c, refsite b WHERE c.siteid = b.siteid and b.webapp = 'Yes' and c.equipmentid like a.equipmentid||'%' and (instr(equipmentid,'-',1,{2})>0 and instr(equipmentid,'-',1,{3})=0)) childNodes  FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid and b.webapp = 'Yes' and equipmentid like'{0}-%' and (instr(equipmentid,'-',1,{1})>0 and instr(equipmentid,'-',1,{2})=0)   order by b.sitename,equipmentid"
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
                        Dim newNode As TreeNode = New TreeNode(newValue & " * " & RI.SharedFunctions.DataClean(dr.Item("equipmentdesc".ToString)), newValue)

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
                        End If
                        'currentValue = CStr(dr.Item("equipmentid"))
                    End If
                End While
            Else
                currentNode.Target = "populated"
                currentNode.NavigateUrl = "http://#"
            End If
        End If
        If NextNode IsNot Nothing Then
            If NextNode.ChildNodes.Count > 0 Then NextNode.Expand()
            Return NextNode
        Else
            Return currentNode.Parent
        End If
    End Function
    Public Sub PopulateNode(ByVal source As Object, ByVal e As TreeNodeEventArgs)
        Dim currentNode As TreeNode = e.Node '_tvFunctionalLocation.SelectedNode
        Dim currentValue As String = e.Node.Value ' Me._tvFunctionalLocation.SelectedValue
        AddChildNodes(currentNode, currentValue)
    End Sub
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
    Private Sub ReturnValue(ByVal selectedValue As String)
        Dim sb As New StringBuilder
        If Request.QueryString IsNot Nothing Then
            Dim parentControl As String = Request.QueryString("Parent")
            Dim targetField As String = Request.QueryString("targetField")
            sb.Append("<script language='javascript'>window.opener.document.forms(0).")
            sb.Append(parentControl)
            sb.Append(".value='")
            sb.Append(Text)
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

    'Protected Sub _btnTree_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnTree.Click
    '    Me.PopulateTreeRoot()
    '    Me._mpeTree.Show()
    'End Sub
End Class
