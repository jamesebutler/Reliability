Imports System.Data
Imports Devart.Data.Oracle

Partial Class RI_DataMaintenance_ResourceManagement
    Inherits RIBasePage

    Dim mPageSize As Integer = 20
    Dim mRowStart As Integer = 1
    Dim mRowEnd As Integer = mPageSize
    Dim mResourceFilter As String = ""
    Dim mOrderBy As String = "ResourceValue"
    Dim mSortDirection As String = " Asc"
    Dim mPageCount As Integer = 0
    Dim mRecordCount As Integer = 0

    Dim sbNonPrintableRows As New StringBuilder

    Public Property PageCount() As Integer
        Get
            Return mPageCount
        End Get
        Set(ByVal value As Integer)
            mPageCount = value
        End Set
    End Property

    Public Property RecordCount() As Integer
        Get
            Return mRecordCount
        End Get
        Set(ByVal value As Integer)
            mRecordCount = value
        End Set
    End Property
    Public Property RowStart() As Integer
        Get
            If ViewState.Item("RowStart") IsNot Nothing Then
                mRowStart = DirectCast(ViewState.Item("RowStart"), Integer)
            End If
            Return mRowStart
        End Get
        Set(ByVal value As Integer)
            mRowStart = value
            ViewState.Add("RowStart", mRowStart)
        End Set
    End Property
    Public Property RowEnd() As Integer
        Get
            If ViewState.Item("RowEnd") IsNot Nothing Then
                mRowEnd = DirectCast(ViewState.Item("RowEnd"), Integer)
            End If
            Return mRowEnd
        End Get
        Set(ByVal value As Integer)
            mRowEnd = value
            ViewState.Add("RowEnd", mRowEnd)
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

    Public Property ResourceFilter() As String
        Get
            If ViewState.Item("ResourceFilter") IsNot Nothing Then
                mResourceFilter = ViewState.Item("ResourceFilter")
            End If
            Return mResourceFilter
        End Get
        Set(ByVal value As String)
            mResourceFilter = value
            ViewState.Add("ResourceFilter", mResourceFilter)
        End Set
    End Property

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("Translation Management"))
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            PopulateLanguageNames()
            PopulateGrid()

        End If
    End Sub

    Private Sub PopulateGrid()
        Dim dr As OracleDataReader

        dr = GetResourcesDRFromPackage()
        Me._gvMain.DataSource = dr
        Me._gvMain.DataBind()
        If RecordCount > 0 Then
            Me._ddlPages.Visible = True
            If Me._ddlLocaleName.SelectedValue <> "All" Then
                _gvMain.Columns(1).Visible = False
                _gvMain.Columns(2).ItemStyle.Width = Unit.Percentage(50)
                _gvMain.Columns(3).ItemStyle.Width = Unit.Percentage(50)
            Else
                _gvMain.Columns(1).Visible = True
                _gvMain.Columns(1).ItemStyle.Width = Unit.Percentage(20)
                _gvMain.Columns(2).ItemStyle.Width = Unit.Percentage(40)
                _gvMain.Columns(3).ItemStyle.Width = Unit.Percentage(40)
            End If
        Else
            Me._ddlPages.Visible = False
        End If
    End Sub

    Private Sub PopulateLanguageNames()
        _ddlLocaleName.items.clear()
        For Each item As DictionaryEntry In Master.RIRESOURCES.ApplicationLocaleList
            _ddlLocaleName.Items.Add(New ListItem(DisplayFullName(item.Key), item.Key))
        Next
        _ddlLocaleName.Items.Insert(0, New ListItem(Master.RIRESOURCES.GetResourceValue("All"), "All"))
    End Sub
    Private Function GetResourcesDRFromPackage() As OracleDataReader
        Dim dr As OracleDataReader = Nothing
        Dim paramCollection As New OracleParameterCollection
        Dim showNullTranslations As Integer = 0

        Try
            If _cbDisplayNullTranslations.Checked = True Then
                showNullTranslations = 1
            Else
                showNullTranslations = 0
            End If

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "IN_MAINLOCALE"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = _ddlLocaleName.SelectedValue.ToUpper
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_SHOWNULLTRANSLATIONS"
            param.OracleDbType = OracleDbType.Integer
            param.Value = showNullTranslations
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_ORDERBY"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = OrderBy & SortDesc
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_STARTROW"
            param.OracleDbType = OracleDbType.Integer
            param.Value = RowStart
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_ENDROW"
            param.OracleDbType = OracleDbType.Integer
            param.Value = RowEnd
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_FILTER"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = Me._txtSearchFor.Text.Trim
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "ref_cursor"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "LOCALIZATION.GETCOMPARISONRESOURCEDATA")
        Catch ex As Exception
            Throw New DataException("GetResourcesDRFromPackage", ex)
            Return Nothing
        Finally
            GetResourcesDRFromPackage = dr
        End Try
    End Function

    Private Sub UpdateOrAddResources()
        Dim dr As OracleDataReader = Nothing
        Dim paramCollection As New OracleParameterCollection
        Dim showNullTranslations As Integer = 0

        Try            

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "IN_RESOURCETYPEID"
            param.OracleDbType = OracleDbType.Integer
            param.Value = Me._hdfResourceTypeID.Value
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_LOCALEID"
            param.OracleDbType = OracleDbType.Integer
            param.Value = Me._hdfLocaleID.Value
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_RESOURCEKEY"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = Me._hdfResourceKey.Value
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_RESOURCEVALUE"
            param.OracleDbType = OracleDbType.NVarChar
            param.Value = Me._txtTranslatedValue.Text
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_COMMENTS"
            param.OracleDbType = OracleDbType.NVarChar
            param.Value = "Updated by " & My.User.Name
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_APPLICATIONID"
            param.OracleDbType = OracleDbType.Integer
            param.Value = Me._hdfApplicationID.Value
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_OVERWRITE"
            param.OracleDbType = OracleDbType.Integer
            param.Value = 1
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "LOCALIZATION.UPDATEORADDRESOURCES")


        Catch ex As Exception
            Throw New DataException("UPDATEORADDRESOURCES", ex)
        Finally         
        End Try
    End Sub
    'Protected Sub _btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnNext.Click
    '    RowStart = RowStart + 20
    '    RowEnd = RowEnd + 20
    '    PopulateGrid()
    'End Sub

    'Protected Sub _btnPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnPrevious.Click
    '    RowStart = RowStart - 20
    '    RowEnd = RowEnd - 20
    '    PopulateGrid()
    'End Sub

    Private Sub PopulatePages()
        Dim pager As String = "{0} {1} {2} {3}"

        If PageCount > 0 Then
            With Me._ddlPages
                .Items.Clear()
                For i As Integer = 1 To PageCount
                    Dim pagerText As String = String.Format(pager, Master.RIRESOURCES.GetResourceValue("Page"), i, Master.RIRESOURCES.GetResourceValue("Of"), PageCount)
                    .Items.Add(New ListItem(pagerText, i))
                Next

            End With
        End If

    End Sub

    Protected Sub _gvMain_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles _gvMain.RowCancelingEdit
        _gvMain.EditIndex = -1
        e.Cancel = True
    End Sub

    Protected Sub _gvMain_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles _gvMain.RowCommand
        If e.CommandName = "Edit" Then
            Dim key As String = String.Empty
            If IsNumeric(e.CommandArgument.ToString) Then 'Row Number
                key = Me._gvMain.DataKeys(e.CommandArgument.ToString).Value

                If key.Length > 0 Then
                    'Display Edit for Key
                    _gvMain.EditIndex = e.CommandArgument.ToString
                    Me._gvMain.Rows.Item(e.CommandArgument.ToString).RowState = DataControlRowState.Edit
                    ''e.Cancel = True                   
                    _ddlPages.Items.Clear()
                    PopulateGrid()
                End If
            End If
        End If

    End Sub

    Public Shared Function ContainsNonPrintableCharacter(ByVal strEdit As String) As Boolean
        strEdit = RI.SharedFunctions.DataClean(strEdit, "")
        Dim newString As String = Regex.Replace(strEdit, "[\x00-\x1f]", "").Trim
        If newString = strEdit Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function DisplayFullName(ByVal value As Object) As String
        Dim fullName As String
        Dim ci As System.Globalization.CultureInfo
        Try
            value = RI.SharedFunctions.DataClean(value, "?")
                ci = System.Globalization.CultureInfo.GetCultureInfo(value)

            If ci.EnglishName <> ci.NativeName Then
                fullName = ci.EnglishName & ", " & ci.NativeName
            Else
                fullName = ci.EnglishName
            End If
        Catch
            fullName = "Invalid Locale - " & value
            'Throw
        End Try
        Return fullName
    End Function

    
    Protected Sub _gvMain_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _gvMain.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'If ContainsNonPrintableCharacter(e.Row.DataItem("ResourceValue")) = False And ContainsNonPrintableCharacter(RI.SharedFunctions.DataClean(e.Row.DataItem("SecondaryValue"))) = False Then
            '    e.Row.Visible = False
            'Else
            '    sbNonPrintableRows.AppendLine(e.Row.DataItem("localename") & "--" & e.Row.DataItem("resourcetype") & "--" & e.Row.DataItem("resourcekey"))

            'End If
           

            'Dim txtSecond As TextBox = e.Row.FindControl("_txtSecondaryValue")
            'If txtSecond IsNot Nothing Then
            '    txtSecond.Attributes.Add("onChange", "RowChanged(" & e.Row.DataItem("rn") & ");")
            'End If
            '<%# DisplayFullName(Container.DataItem("

            If e.Row.RowIndex = 1 Then
                RecordCount = CType(e.Row.DataItem("rowcount"), Integer)
                If RecordCount Mod mPageSize > 0 Then
                    PageCount = RecordCount \ mPageSize + 1
                Else
                    PageCount = RecordCount \ mPageSize
                End If
                If Me._ddlPages.Items.Count = 0 Then
                    Me.PopulatePages()
                End If
            End If

        End If
    End Sub

    Protected Sub _ddlPages_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlPages.SelectedIndexChanged
        '1    1   20 = 1 * 20 -19 =>1 + 19
        '2    21  30 = 2 * 20 -19 =>21 +19
        '10
        RowStart = _ddlPages.SelectedValue * mPageSize - (mPageSize - 1)
        RowEnd = _ddlPages.SelectedValue * mPageSize
        PopulateGrid()
    End Sub

    Protected Sub _gvMain_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles _gvMain.RowEditing
        'Me._gvMain.Rows.Item(e.NewEditIndex).RowState = DataControlRowState.Edit
        e.Cancel = True
        '_ddlPages.Items.Clear()
        'PopulateGrid()

    End Sub
    Protected Sub _gvMain_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _gvMain.RowCreated
        If e.Row.RowState = DataControlRowState.Edit Or e.Row.RowState = 5 Then
            If e.Row.DataItem IsNot Nothing Then
                Me._lblEditEnglishValue.Text = RI.SharedFunctions.DataClean(e.Row.DataItem("EnglishValue"))  'Rows (e.CommandArgument.ToString ).DataItem
                Me._txtTranslatedValue.Text = RI.SharedFunctions.DataClean(e.Row.DataItem("ResourceValue"))
                _lblEditLocalValue.Text = DisplayFullName(RI.SharedFunctions.DataClean(e.Row.DataItem("Localename")))
                'ResourceKey,LOCALEID,RESOURCETYPEID,ID

                Me._hdfLocaleID.Value = Me._gvMain.DataKeys.Item(e.Row.DataItemIndex).Values.Item("LOCALEID").ToString
                Me._hdfResourceKey.Value = Me._gvMain.DataKeys.Item(e.Row.DataItemIndex).Values.Item("ResourceKey").ToString
                Me._hdfResourceTypeID.Value = Me._gvMain.DataKeys.Item(e.Row.DataItemIndex).Values.Item("RESOURCETYPEID").ToString
                Me._hdfApplicationID.Value = Me._gvMain.DataKeys.Item(e.Row.DataItemIndex).Values.Item("ID").ToString
                _mpeExcelSelect.Show()
                _gvMain.EditIndex = -1
            End If
        End If

    End Sub
    Protected Sub _gvMain_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles _gvMain.RowUpdating

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
        RowStart = 1
        RowEnd = mPageSize
        Me._ddlPages.SelectedValue = 1
        PopulateGrid()
        RI.SharedFunctions.AddGlyph(_gvMain, _gvMain.HeaderRow, e.SortExpression, sortDir)
    End Sub
    Protected Sub _gvMain_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles _gvMain.SelectedIndexChanging

    End Sub
    Protected Sub _btnSearchFor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnSearchFor.Click
        RowStart = 1
        RowEnd = mPageSize
        Me._ddlPages.SelectedValue = 1
        _ddlPages.Items.Clear()
        PopulateGrid()
    End Sub

    Protected Sub _btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnCancel.Click
       
        '_ddlPages.Items.Clear()
        'PopulateGrid()
        Me._gvMain.EditIndex = -1
        Me._mpeExcelSelect.Hide()
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        'Response.Write(sbNonPrintableRows.ToString)
    End Sub

    Protected Sub _ddlLocaleName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlLocaleName.SelectedIndexChanged
        RowStart = 1
        RowEnd = mPageSize
        Me._ddlPages.SelectedValue = 1
        _ddlPages.Items.Clear()
        PopulateGrid()
    End Sub

    Protected Sub _cbDisplayNullTranslations_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cbDisplayNullTranslations.CheckedChanged
        RowStart = 1
        RowEnd = mPageSize
        Me._ddlPages.SelectedValue = 1
        _ddlPages.Items.Clear()
        PopulateGrid()
    End Sub


    Protected Sub _btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnSave.Click
        UpdateOrAddResources()
        Master.RIRESOURCES.ClearResourceCache()
        _ddlPages.Items.Clear()
        PopulateGrid()
    End Sub
End Class
