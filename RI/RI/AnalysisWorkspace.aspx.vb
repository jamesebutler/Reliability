Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.io
Imports Devart.Data.Oracle

Partial Class WorkspaceAnalysis
    'Inherits System.Web.UI.Page
    Inherits RIBasePage

    ''' <summary>
    ''' Gets the selected RINumber from the querystring
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns the RiNumber</returns>
    ''' <remarks></remarks>
    Public Property riNumber() As String
        Get
            Return Request.QueryString("riNumber")
        End Get
        Set(ByVal value As String)
            Request.QueryString("riNumber") = value
        End Set
    End Property

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("AWTitle", True))
        Master.HideMainMenu()

        'Me._btnExampleTop.OnClientClick = Master.GetPopupWindowJS("~/RI/Help/Analysis Workspace Help.htm", , 773, 400, True)

        Me._btnExampleTop.OnClientClick = Master.GetPopupWindowJS("~/RI/files/Analysis Workspace Help" & Master.RIRESOURCES.CurrentLocale & ".pdf", , 1100, 700, True) & ";return false;"
        Me._btnExampleBtm.OnClientClick = Master.GetPopupWindowJS("~/RI/files/Analysis Workspace Help" & Master.RIRESOURCES.CurrentLocale & ".pdf", , 1100, 700, True) & ";return false;"
        Me._btnPrintTop.OnClientClick = "javascript:window.print();" & "return false;"
        Me._btnPrintBtm.OnClientClick = "javascript:window.print();" & "return false;"
        'Dim newMenuCol As New MenuItemCollection
        'newMenuCol.Add(New MenuItem("Example", "Example", "~\RI\Images\checkbox.gif", "~/RI/Help/Analysis Workspace Help.htm", "_blank"))
        'Master.ShowPopupMenu(newMenuCol)

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'riNumber = Request.QueryString("RINumber") '"24141" '"24131"
            GetAW()
        End If
    End Sub

    Sub GetAW()
        Dim ds As DataSet = Nothing

        ds = GetAnalysisWorkspaceDSFromPackage()

        'Depending on the contents of ds, show table to add first record and bind data
        If ds.Tables(0).Rows.Count <> 0 Then
            _gvEvent.DataSource = ds.Tables(0)
            _gvEvent.DataBind()
            _dgWorkspace.DataSource = ds.Tables(1)
            _dgWorkspace.DataBind()
            If ds.Tables(1).Rows.Count = 0 Then
                Me._tblNewRow.Visible = "true"
                Me._tblNewRowHeader.Visible = "true"
                Me._tbFailureMode.Text = ""
                Me._tbFailureCause.Text = ""
                Me._tbVerification.Text = ""
                Me._tbSortOrder.Text = ""
                Me._tblHeading.Visible = "false"
            Else
                Me._tblNewRow.Visible = "false"
                Me._tblNewRowHeader.Visible = "false"
                Me._tblHeading.Visible = "true"
            End If
        End If

    End Sub
    'Returns 2 recordsets - one the event field from tblriincident and the other the analysis workspace fields.
    Private Function GetAnalysisWorkspaceDSFromPackage() As DataSet
        Dim ds As DataSet = Nothing
        Dim paramCollection As New OracleParameterCollection

        Try

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_rinumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = riNumber
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsAWEvent"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsAW"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "RIMaint.GetAnalysisWorkspace", "NewAnalysisWorkspace", 0)

        Catch ex As Exception
            Throw New DataException("GetAnalysisWorkspace", ex)
            Return Nothing
        Finally
            GetAnalysisWorkspaceDSFromPackage = ds
            If Not ds Is Nothing Then ds = Nothing
        End Try
    End Function

    Protected Sub _dgWorkspace_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _dgWorkspace.RowDataBound
        'Now, reference the Button control that the Delete ButtonColumn 
        'has been rendered to
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim deleteButton As Button = e.Row.FindControl("_lnkBtnDelete")

            'Add the onclick event handler
            deleteButton.Attributes("onclick") = "javascript:return " & _
                       "confirm('" & Master.RIRESOURCES.GetResourceValue("ConfirmDelete", True) & "')"
            '.Are you sure you want to delete this record?')"
        End If
    End Sub
    Function GetUserid() As String
        Dim userProfile As RI.CurrentUserProfile = Nothing
        userProfile = RI.SharedFunctions.GetUserProfile
        Return userProfile.Username
    End Function

    Protected Sub _btnAddMode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnAddMode.Click
        UpdateAWEventPackage()

        checkUpdate()

        GetAW()
    End Sub

    Sub UpdateAWEventPackage()

        Dim dr As OracleDataReader = Nothing
        Dim paramCollection As New OracleParameterCollection
        Dim strRCFAEvent, status As String
        Dim tbRCFAEvent As TextBox = CType(Me._gvEvent.Row.FindControl("_txtrcfaevent"), TextBox)

        Try

            strRCFAEvent = tbRCFAEvent.Text

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_rinumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = riNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_rcfaevent"
            param.OracleDbType = OracleDbType.NClob
            param.Direction = Data.ParameterDirection.Input
            param.Value = strRCFAEvent
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_userid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = GetUserid()
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "rimaint.analysisworkspaceevent")
            If status <> 0 Then
                Throw New DataException("UpdateAWEventPackage")
            End If

        Catch ex As Exception
            Throw New DataException("UpdateAWEventPackage", ex)
        Finally
            If Not dr Is Nothing Then dr = Nothing
        End Try

    End Sub
    'Routine will check from any updated rows.
    Sub checkUpdate()
        Dim i As Integer = 0
        Dim intRecordCnt, intRow As Integer
        Dim strFailureMode, strFailureCause, strVerification As String
        Dim strSortOrder, Seqid As String
        Dim tbSortOrder, tbMode, tbCause, tbVerification, tbSeqId As TextBox
        Dim tble As Table

        For i = 0 To _dgWorkspace.DirtyRows.Count - 1
            intRow = _dgWorkspace.DirtyRows.Item(i).DataItemIndex
            tbSortOrder = CType(Me._dgWorkspace.Rows(intRow).Cells(0).Controls(1), TextBox)
            tbMode = CType(Me._dgWorkspace.Rows(intRow).Cells(1).Controls(1), TextBox)
            tbCause = CType(Me._dgWorkspace.Rows(intRow).Cells(2).Controls(1), TextBox)
            tbVerification = CType(Me._dgWorkspace.Rows(intRow).Cells(3).Controls(1), TextBox)
            tbSeqId = CType(Me._dgWorkspace.Rows(intRow).Cells(4).Controls(1), TextBox)

            strFailureMode = tbMode.Text
            strFailureCause = tbCause.Text
            strVerification = tbVerification.Text
            strSortOrder = tbSortOrder.Text
            Seqid = tbSeqId.Text

            UpdateAWPackage(strFailureMode, strFailureCause, strVerification, strSortOrder, Seqid)

        Next

        'Check whether we are dealing with the first record.  If so, you will not have a seqid so pass in a "1"
        If Me._tblNewRow.Visible = "true" Then
            If Me._tbFailureMode.Text <> Nothing Or _tbFailureCause.Text <> Nothing Or _tbVerification.Text <> Nothing Then
                strFailureMode = _tbFailureMode.Text
                strFailureCause = _tbFailureCause.Text
                strVerification = _tbVerification.Text
                strSortOrder = _tbSortOrder.Text

                UpdateAWPackage(strFailureMode, strFailureCause, strVerification, strSortOrder, "1")
            End If

        Else
            'Check for the existence of child table which is where the insertrow is created.
            If Not CType(_dgWorkspace.Controls(0), Table) Is Nothing Then
                tble = CType(_dgWorkspace.Controls(0), Table)
                intRecordCnt = tble.Rows.Count - 1
                tbSortOrder = CType(tble.Rows(intRecordCnt).Cells(0).Controls(1), Control)
                tbMode = CType(tble.Rows(intRecordCnt).Cells(1).Controls(1), TextBox)
                tbCause = CType(tble.Rows(intRecordCnt).Cells(2).Controls(1), TextBox)
                tbVerification = CType(tble.Rows(intRecordCnt).Cells(3).Controls(1), TextBox)

                strFailureMode = tbMode.Text
                strFailureCause = tbCause.Text
                strVerification = tbVerification.Text
                strSortOrder = tbSortOrder.Text

                If strFailureMode <> "" Or strFailureCause <> "" Or strVerification <> "" Then

                    UpdateAWPackage(strFailureMode, strFailureCause, strVerification, strSortOrder, "1")

                End If
            End If
        End If

    End Sub

    Sub UpdateAWPackage(ByVal strfailuremode As String, ByVal strFailureCause As String, ByVal strVerification As String, ByVal strSortOrder As String, ByVal SeqId As String)

        Dim dr As OracleDataReader = Nothing
        Dim status As String
        Dim paramCollection As New OracleParameterCollection
        Dim strRCFAEvent As String
        Dim tbRCFAEvent As TextBox = CType(Me._gvEvent.Row.FindControl("_txtrcfaevent"), TextBox)

        Try
            strRCFAEvent = tbRCFAEvent.Text

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_seqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SeqId
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_rinumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = riNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_rcfafailuremode"
            param.OracleDbType = OracleDbType.NClob
            param.Direction = Data.ParameterDirection.Input
            param.Value = strfailuremode
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_rcfafailurecause"
            param.OracleDbType = OracleDbType.NClob
            param.Direction = Data.ParameterDirection.Input
            param.Value = strFailureCause
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_rcfaverification"
            param.OracleDbType = OracleDbType.NClob
            param.Direction = Data.ParameterDirection.Input
            param.Value = strVerification
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_sortorder"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strSortOrder
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_userid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = GetUserid()
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "rimaint.analysisworkspace")

        Catch ex As Exception
            Throw New DataException("UpdateAWPackage", ex)
        Finally
            If Not dr Is Nothing Then dr = Nothing
        End Try

    End Sub

    Sub DeleteAWPackage(ByVal strSeqId As String)

        Dim dr As OracleDataReader = Nothing
        Dim paramCollection As New OracleParameterCollection

        Try
            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_seqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strSeqId
            paramCollection.Add(param)
            Dim status As String
            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "rimaint.deleteanalysisworkspace")

        Catch ex As Exception
            Throw New DataException("DeleteAWPackage", ex)
        Finally
            If Not dr Is Nothing Then dr = Nothing
        End Try

    End Sub

    Protected Sub _dgWorkspace_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles _dgWorkspace.RowDeleting

        Try
            Dim strSeqid As Integer = _
                Convert.ToInt32(_dgWorkspace.DataKeys(e.RowIndex).Value)

            DeleteAWPackage(strSeqid)

            GetAW()

        Catch ex As Exception
            Throw New Exception("_dgWorkspace_DeleteCommand", ex.InnerException)
        End Try
    End Sub

    Protected Sub _btnSaveClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnSaveClose.Click
        UpdateAWEventPackage()

        checkUpdate()

        Response.Write("<script language='javascript'> { try{window.opener.updateItemCounts();}catch(err){}window.close(); }</script>")

    End Sub

End Class
