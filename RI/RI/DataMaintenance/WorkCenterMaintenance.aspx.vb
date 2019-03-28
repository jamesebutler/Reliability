Imports System.Data
Imports Devart.Data.Oracle

Partial Class RI_DataMaintenance_WorkCenter
    Inherits RIBasePage

    Dim mOrderBy As String = "PlantCode"
    Dim mSortDirection As String = " Asc"
    Dim mPageCount As Integer = 0
    Dim mRecordCount As Integer = 0

    Dim sbNonPrintableRows As New StringBuilder
    Dim userProfile As RI.CurrentUserProfile = Nothing

    Public Property RecordCount() As Integer
        Get
            Return mRecordCount
        End Get
        Set(ByVal value As Integer)
            mRecordCount = value
        End Set
    End Property
    Public Property OrderBy() As String
        Get
            If ViewState.Item("OrderBy") IsNot Nothing Then
                mOrderBy = ViewState.Item("OrderBy")
            End If
            Return mOrderBy
        End Get
        Set(ByVal value As String)
            mOrderBy = value
            ViewState.Add("OrderBy", mOrderBy)
        End Set
    End Property
    Public Property SortDesc() As String
        Get
            If ViewState.Item("SortDirection") IsNot Nothing Then
                mSortDirection = ViewState.Item("SortDirection")
            End If
            Return mSortDirection
        End Get
        Set(ByVal value As String)
            mSortDirection = value
            ViewState.Add("SortDirection", mSortDirection)
        End Set
    End Property

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Master.SetBanner(Master.RIRESOURCES.GetResourceValue("Work Center Maintenance"))
        Master.HideMainMenu()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        userProfile = RI.SharedFunctions.GetUserProfile
        If Not Page.IsPostBack Then
            PopulateFacility()
            'Dim strSite As String = Request("SiteID")
            'If strSite = "" Then
            '    Me._ddlFacility.SelectedValue = userProfile.DefaultFacility
            'Else
            '    Me._ddlFacility.SelectedValue = strSite
            'End If
            'Response.Write("Here" & strSite)
            PopulateGrid()
        End If
    End Sub

    Private Sub PopulateGrid()
        Dim dr As OracleDataReader

        dr = GetWorkCenterDRFromPackage()
        Me._gvMain.DataSource = dr
        Me._gvMain.DataBind()
        If RecordCount > 0 Then
            If Me._ddlFacility.SelectedValue <> "All" Then
                _gvMain.Columns(1).Visible = False
                _gvMain.Columns(2).ItemStyle.Width = Unit.Percentage(50)
                _gvMain.Columns(3).ItemStyle.Width = Unit.Percentage(50)
            Else
                _gvMain.Columns(1).Visible = True
                _gvMain.Columns(1).ItemStyle.Width = Unit.Percentage(20)
                _gvMain.Columns(2).ItemStyle.Width = Unit.Percentage(40)
                _gvMain.Columns(3).ItemStyle.Width = Unit.Percentage(40)
            End If
        End If
    End Sub

    
    Private Function GetWorkCenterDRFromPackage() As OracleDataReader
        Dim dr As OracleDataReader = Nothing
        Dim paramCollection As New OracleParameterCollection
        
        Try

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "IN_FACILITY"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = _ddlFacility.SelectedValue.ToUpper
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsWorkCenter"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "RIMaintenanceDB.GETWorkCenterByFacility")
        Catch ex As Exception
            Throw New DataException("GETWorkCenterByFacility", ex)
            Return Nothing
        Finally
            GetWorkCenterDRFromPackage = dr
        End Try
    End Function

    Private Sub UpdateOrAddWorkCenter(ByVal WorkCenter As String, ByVal BusUnit As String)
        Dim dr As OracleDataReader = Nothing
        Dim paramCollection As New OracleParameterCollection
        Dim showNullTranslations As Integer = 0

        Try

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "IN_Facility"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = _ddlFacility.SelectedValue.ToUpper
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_BusUnit"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = BusUnit
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_WorkCenter"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = WorkCenter
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "IN_Username"
            'param.OracleDbType = OracleDbType.Integer
            'param.Value = 1
            'param.Direction = ParameterDirection.Input
            'paramCollection.Add(param)

            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "RIMAINTENANCEDB.UpdateorAddWorkCenter")


        Catch ex As Exception
            Throw New DataException("UpdateOrAddWorkCenter", ex)
        Finally
        End Try
    End Sub

    Private Sub DeleteWorkCenter(ByVal WorkCenter As String, ByVal BusUnit As String)
        Dim dr As OracleDataReader = Nothing
        Dim paramCollection As New OracleParameterCollection
        Dim showNullTranslations As Integer = 0
        
        Try

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "IN_Facility"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = _ddlFacility.SelectedValue.ToUpper
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_BusUnit"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = BusUnit
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_WorkCenter"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = WorkCenter
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = userProfile.Username
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "RIMAINTENANCEDB.DeleteWorkCenter")


        Catch ex As Exception
            Throw New DataException("DeleteWorkCenter", ex)
        Finally
        End Try
    End Sub
    Protected Sub _gvMain_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Footer Or e.Row.RowType = DataControlRowType.EmptyDataRow Then
            Dim cmbNewType As DropDownList = DirectCast(e.Row.FindControl("_ddlNewBusUnit"), DropDownList)

            Dim ds As DataSet = Nothing
            Dim io As New System.IO.StringWriter
            Dim ipLoc As New IP.Bids.Localization.WebLocalization()

            ds = GetBusUnitDSFromPackage()
            If ds IsNot Nothing Then
                cmbNewType.DataTextField = "risuperarea"
                cmbNewType.DataValueField = "risuperarea"
                cmbNewType.DataSource = ds.Tables(0).CreateDataReader
                cmbNewType.DataBind()
                cmbNewType.Items.Insert(0, New ListItem(ipLoc.GetResourceValue("All", False, "Shared"), "All"))

                'If _ddlFacility.Items.FindByValue(selectedValue) IsNot Nothing Then
                '_ddlFacility.SelectedValue = selectedValue
                'End If

            End If
        End If

    End Sub
    Protected Sub _gvMain_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        _gvMain.EditIndex = -1
        'e.Cancel = True
    End Sub

    Protected Sub _gvMain_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        _gvMain.EditIndex = e.NewEditIndex
        'Me._gvMain.Rows.Item(e.NewEditIndex).RowState = DataControlRowState.Edit
        e.Cancel = True
        '_ddlPages.Items.Clear()
        PopulateGrid()

    End Sub
    Protected Sub _gvMain_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles _gvMain.Sorting
        Dim sortDir As SortDirection = SortDirection.Ascending
        If e.SortExpression = Me.OrderBy Then 'Sorting previously sorted column
            'Change Sort order
            If Me.SortDesc = " Asc" Then
                Me.SortDesc = " Desc"
                sortDir = SortDirection.Descending
            Else
                Me.SortDesc = " Asc"
                sortDir = SortDirection.Ascending
            End If
        Else 'Sorting new column in ascending order
            Me.SortDesc = " Desc"
        End If

        OrderBy = e.SortExpression
        PopulateGrid()
        RI.SharedFunctions.AddGlyph(_gvMain, _gvMain.HeaderRow, e.SortExpression, sortDir)
    End Sub

    Private Sub PopulateFacility()
        Dim ds As DataSet = Nothing
        Dim io As New System.IO.StringWriter
        Dim ipLoc As New IP.Bids.Localization.WebLocalization()
        Dim strSiteid As String = Request.QueryString("SiteID")
        Try
            ds = GetFacilityDSFromPackage()
            If ds IsNot Nothing Then
                Me._ddlFacility.DataTextField = "Sitename"
                Me._ddlFacility.DataValueField = "Siteid"
                Me._ddlFacility.DataSource = ds.Tables(0).CreateDataReader
                Me._ddlFacility.DataBind()

                Me._ddlFacility.Items.Insert(0, New ListItem(ipLoc.GetResourceValue("All", False, "Shared"), "All"))
                If _ddlFacility.Items.FindByValue(strSiteID) IsNot Nothing Then
                    _ddlFacility.ClearSelection()
                    _ddlFacility.Items.FindByValue(strSiteID).Selected = True
                End If

                'If _ddlFacility.Items.FindByValue(selectedValue) IsNot Nothing Then
                '_ddlFacility.SelectedValue = selectedValue
                'End If

            End If
        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then ds = Nothing
        End Try
    End Sub
    Private Function GetFacilityDSFromPackage() As DataSet
        Dim cmdSQL As OracleCommand = Nothing
        Dim connection As String = String.Empty
        Dim provider As String = String.Empty
        Dim ds As DataSet = Nothing
        Dim daData As OracleDataAdapter = Nothing
        Dim cnConnection As OracleConnection = Nothing

        Try
            If Cache.Item("RI_Facility") IsNot Nothing Then
                ds = CType(Cache.Item("RI_Facility"), DataSet)
                Exit Try
            End If
            If connection.Length = 0 Then
                connection = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
            End If
            If provider.Length = 0 Then
                provider = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ProviderName
            End If
            cmdSQL = New OracleCommand
            With cmdSQL
                cnConnection = New OracleConnection(connection)
                cnConnection.Open()
                .Connection = cnConnection
                .CommandText = "RINEWINCIDENT.FacilityList"
                .CommandType = CommandType.StoredProcedure
                Dim param As New OracleParameter

                param = New OracleParameter
                param.ParameterName = "rsFacility"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = ParameterDirection.Output
                .Parameters.Add(param)

            End With

            ds = New DataSet()
            ds.EnforceConstraints = False
            daData = New OracleDataAdapter(cmdSQL)
            daData.Fill(ds)
            ds.EnforceConstraints = True

            If ds IsNot Nothing Then Cache.Insert("RI_Facility", ds, Nothing, DateTime.Now.AddHours(6), TimeSpan.Zero)
        Catch ex As Exception
            Cache.Remove("RI_Facility")
            Throw New DataException("GetFacilityDSFromPackage", ex)
            Return Nothing
        Finally
            GetFacilityDSFromPackage = ds
            If Not daData Is Nothing Then daData = Nothing
            If Not ds Is Nothing Then ds = Nothing
            If Not cmdSQL Is Nothing Then cmdSQL = Nothing
        End Try
    End Function
    Private Function GetBusUnitDSFromPackage() As DataSet
        Dim cmdSQL As OracleCommand = Nothing
        Dim connection As String = String.Empty
        Dim provider As String = String.Empty
        Dim ds As DataSet = Nothing
        Dim daData As OracleDataAdapter = Nothing
        Dim cnConnection As OracleConnection = Nothing

        Try
            If connection.Length = 0 Then
                connection = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
            End If
            If provider.Length = 0 Then
                provider = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ProviderName
            End If
            cmdSQL = New OracleCommand
            With cmdSQL
                cnConnection = New OracleConnection(connection)
                cnConnection.Open()
                .Connection = cnConnection
                .CommandText = "rimaintenancedb.GetBusinessUnit"
                .CommandType = CommandType.StoredProcedure
                Dim param As New OracleParameter

                param = New OracleParameter
                param.ParameterName = "IN_facility"
                param.OracleDbType = OracleDbType.VarChar
                param.Value = userProfile.DefaultFacility
                param.Direction = ParameterDirection.Input
                .Parameters.Add(param)

                param = New OracleParameter
                param.ParameterName = "rsBusinessUnit"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = ParameterDirection.Output
                .Parameters.Add(param)

            End With

            ds = New DataSet()
            ds.EnforceConstraints = False
            daData = New OracleDataAdapter(cmdSQL)
            daData.Fill(ds)
            ds.EnforceConstraints = True

            If ds IsNot Nothing Then Cache.Insert("RI_FacilityBusUit", ds, Nothing, DateTime.Now.AddHours(6), TimeSpan.Zero)
        Catch ex As Exception
            Cache.Remove("RI_FacilityBusUit")
            Throw New DataException("GetBusUnitDSFromPackage", ex)
            'If Not conCust Is Nothing Then conCust = Nothing
            Return Nothing
        Finally
            GetBusUnitDSFromPackage = ds
            If Not daData Is Nothing Then daData = Nothing
            If Not ds Is Nothing Then ds = Nothing
            If Not cmdSQL Is Nothing Then cmdSQL = Nothing
        End Try
    End Function
    Protected Sub _gvMain_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName.Equals("Insert") Then
            Dim txtNewWorkCenter As TextBox = DirectCast(_gvMain.FooterRow.FindControl("_txtNewWorkCenter"), TextBox)
            Dim ddlNewBusinessUnit As DropDownList = DirectCast(_gvMain.FooterRow.FindControl("_ddlNewBusUnit"), DropDownList)
            UpdateOrAddWorkCenter(txtNewWorkCenter.Text, ddlNewBusinessUnit.SelectedValue)
            PopulateGrid()
        End If

        If e.CommandName.Equals("EmptyInsert") Then
            Dim txtNewWorkCenter As TextBox = DirectCast(_gvMain.Controls(0).Controls(0).FindControl("_txtNewWorkCenter"), TextBox)
            Dim ddlNewBusinessUnit As DropDownList = DirectCast(_gvMain.Controls(0).Controls(0).FindControl("_ddlNewBusUnit"), DropDownList)
            UpdateOrAddWorkCenter(txtNewWorkCenter.Text, ddlNewBusinessUnit.SelectedValue)
            PopulateGrid()
        End If

    End Sub
    Protected Sub _gvMain_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim txtWorkCenter As Label = DirectCast(_gvMain.Rows(e.RowIndex).FindControl("_lblWorkCenter"), Label)
        Dim txtBusinessUnit As Label = DirectCast(_gvMain.Rows(e.RowIndex).FindControl("_lblBusUnit"), Label)
        DeleteWorkCenter(txtWorkCenter.Text, txtBusinessUnit.Text)
        PopulateGrid()
    End Sub

    Protected Sub _ddlFacility_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlFacility.SelectedIndexChanged
        PopulateGrid()
    End Sub
End Class
