Imports System.Data
Imports Devart.Data.Oracle

Partial Class RI_DataMaintenance_ResourceManagement
    Inherits RIBasePage
    Dim userProfile As RI.CurrentUserProfile = Nothing

    Dim selectedFacility As String = String.Empty
    Dim mOrderBy As String = "ResourceValue"
    Dim mSortDirection As String = " Asc"

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
        Master.ShowOutageMenu()
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("Additional Resources"))
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        userProfile = RI.SharedFunctions.GetUserProfile
        Dim sc As ScriptManager
        sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
        If sc IsNot Nothing Then
            Dim loService As New ServiceReference
            loService.InlineScript = False
            loService.Path = "~/Outage/OutageCascadingLists.asmx"
            sc.Services.Add(loService)
        End If

        selectedFacility = userProfile.DefaultFacility
        Me._cddlFacility.SelectedValue = selectedFacility

        If Not Page.IsPostBack Then
            PopulateGrid()
        End If
    End Sub

    Private Sub PopulateGrid()
        Dim dr As OracleDataReader

        dr = GetResourcesDRFromPackage()
        Me._gvMain.DataSource = dr
        Me._gvMain.DataBind()
    End Sub

    Private Function GetResourcesDRFromPackage() As OracleDataReader
        Dim dr As OracleDataReader = Nothing
        Dim paramCollection As New OracleParameterCollection

        Try
            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "rsResources"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "OUTAGEMAINT.RESOURCES")

        Catch ex As Exception
            Throw New DataException("GetResourcesDRFromPackage", ex)
            Return Nothing
        Finally
            GetResourcesDRFromPackage = dr
        End Try
    End Function

    Private Sub UpdateAdditionalResources(ByVal intResourceSeq, ByVal strDiscipline)
        Dim dr As OracleDataReader = Nothing
        Dim paramCollection As New OracleParameterCollection

        Try

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "INResourceSeqID"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = intResourceSeq
            'param.Value = Me._hdfApplicationID.Value
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "INDiscipline"
            param.OracleDbType = OracleDbType.VarChar
            ' param.Value = "Test"
            param.Value = strDiscipline
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "inUpdateBy"
            param.OracleDbType = OracleDbType.VarChar
            ' param.Value = "Test"
            param.Value = userProfile.Username
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "OUTAGEMAINT.UPDATERESOURCES")

        Catch ex As Exception
            Throw New DataException("UPDATERESOURCES", ex)
        Finally
        End Try
    End Sub
    Private Sub AddAdditionalResources()
        Dim dr As OracleDataReader = Nothing
        Dim paramCollection As New OracleParameterCollection

        Try

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "INUserName"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = Me._ddlPerson.SelectedValue
            'param.Value = Me._hdfApplicationID.Value
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "INDiscipline"
            param.OracleDbType = OracleDbType.VarChar
            'param.Value = "Test"
            param.Value = Me._txtDiscipline.Text
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "INCreatedBy"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = userProfile.Username
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "OUTAGEMAINT.ADDRESOURCES")

        Catch ex As Exception
            Throw New DataException("ADDRESOURCES", ex)
        Finally
        End Try
    End Sub

    Protected Sub _gvMain_DataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _gvMain.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Now, reference the Button control that the Delete ButtonColumn has been rendered to 
            Dim deleteButton As Button = TryCast(e.Row.FindControl("_lnkBtnDelete"), Button)
            If deleteButton IsNot Nothing Then
                'We can now add the onclick event handler                
                deleteButton.OnClientClick = "Javascript:ConfirmDelete('" & e.Row.DataItem("resourceseqid").ToString & "'," & e.Row.RowIndex & ",'" & Me._gvMain.ClientID & "');return false;"
            End If
            Dim hdRowChange As HiddenField = TryCast(e.Row.FindControl("_rowChanged"), HiddenField)
            If hdRowChange IsNot Nothing Then
                Dim rowChangedJS As String = String.Format("document.getElementById('{0}').value='changed';", hdRowChange.ClientID)
                Dim txtDiscipline As TextBox = TryCast(e.Row.FindControl("_txtDiscipline"), TextBox)
                If txtDiscipline IsNot Nothing Then
                    txtDiscipline.Attributes.Add("onChange", rowChangedJS)
                End If
            End If
        End If

    End Sub


    Protected Sub _btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnAdd.Click
        AddAdditionalResources()
        PopulateGrid()
    End Sub


    Protected Sub _btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnSave.Click
        Dim i As Integer = 0
        Dim intResourceSeq, intRow As Integer
        Dim strDiscipline As String
        Dim tbDiscipline As TextBox

        For i = 0 To _gvMain.DirtyRows.Count - 1
            intRow = _gvMain.DirtyRows.Item(i).DataItemIndex
            intResourceSeq = Me._gvMain.DataKeys.Item(intRow).Value
            tbDiscipline = CType(Me._gvMain.Rows(intRow).FindControl("_txtTblDiscipline"), TextBox)
            strDiscipline = tbDiscipline.Text

            UpdateAdditionalResources(intResourceSeq, strDiscipline)
        Next

    End Sub
End Class
