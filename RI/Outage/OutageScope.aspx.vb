Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Devart.Data.Oracle
Imports System.Web.Mail

Partial Class RI_Outage
    Inherits RIBasePage

    Dim userProfile As RI.CurrentUserProfile = Nothing

    Public Property OutageNumber() As String
        Get
            If mOutageNumber.Length = 0 Then mOutageNumber = Request.QueryString("OutageNumber")
            Return mOutageNumber
        End Get
        Set(ByVal value As String)
            mOutageNumber = value
        End Set
    End Property
    Private mOutageNumber As String = String.Empty
    
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("MajorScope", True))
        Master.ShowPopupMenu()
        'Master.HideMenu()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'RI.SharedFunctions.DisablePageCache(Page.Response)
        userProfile = RI.SharedFunctions.GetUserProfile
        'ScriptManager.RegisterClientScriptInclude(Me._udpAW, _udpAW.GetType, "ActionItems", Page.ResolveClientUrl("~/RI/ActionItems.js"))
        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "OutageScope") Then
            Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "OutageScope", Page.ResolveClientUrl("~/outage/OutageScope.js"))
        End If
        Dim sc As ScriptManager
        sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
        If sc IsNot Nothing Then
            Dim loService As New ServiceReference
            loService.InlineScript = False
            loService.Path = "~/RISharedWS.asmx"
            sc.Services.Add(loService)
        End If


        If Not Page.IsPostBack Then
            GetScope()
        End If
    End Sub

    Private Sub GetScope()
        Dim dr As OracleDataReader = Nothing
        Try
            dr = GetScopeDRFromPackage()

            'Depending on the contents of ds, show table to add first record and bind data
            If dr IsNot Nothing Then

                _gvScope.DataSource = dr
                _gvScope.DataBind()
            End If
        Catch ex As Exception
            Throw
        Finally
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
        End Try
    End Sub

    Private Function GetScopeDRFromPackage() As OracleDataReader
        Dim dr As OracleDataReader = Nothing
        Dim paramCollection As New OracleParameterCollection

        Try

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_outagenumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = OutageNumber
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsScope"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "outage.GetOutageScope")
        Catch ex As Exception
            Throw New DataException("GetScope", ex)
            Return Nothing
        Finally
            GetScopeDRFromPackage = dr
        End Try
    End Function



    Protected Sub _gvScope_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _gvScope.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                'Now, reference the Button control that the Delete ButtonColumn has been rendered to 
                Dim deleteButton As Button = TryCast(e.Row.FindControl("_lnkBtnDelete"), Button)
                If deleteButton IsNot Nothing Then
                    'We can now add the onclick event handler                
                    deleteButton.OnClientClick = "Javascript:ConfirmDelete('" & e.Row.DataItem("outagescopeseqid").ToString & "'," & e.Row.RowIndex & ",'" & Me._gvScope.ClientID & "');return false;"
                End If
                Dim hdRowChange As HiddenField = TryCast(e.Row.FindControl("_rowChanged"), HiddenField)
            End If
        Catch
            Throw
        End Try
    End Sub
    Protected Sub _btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnSave.Click

        UpdateScope()

    End Sub
    Protected Sub _btnSaveClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnSaveClose.Click

        UpdateScope()

        'Response.Write("<script language='javascript'> { try{window.opener.location.reload(true);}catch(err){}window.close(); }</script>")
        Response.Write("<script language='javascript'> { try{window.opener.updateItemCounts();}catch(err){}window.close(); }</script>")
        'updateItemCounts

    End Sub
    Protected Sub UpdateScope()
        Dim i As Integer = 0
        Dim intScopeSeqId, intRow As Integer
        Dim strDesc, strSort As String
        Dim tbDesc, tbSort As TextBox

        Try
            'Check whether we are dealing with the first record.  If so, you will not have a seqid so pass in a "0"
            If Me._tblNewRow.Visible = "true" Then
                If Me._tbNewDesc.Text <> Nothing And Me._tbNewSort.Text <> Nothing Then
                    strDesc = Me._tbNewDesc.Text
                    strSort = Me._tbNewSort.Text
                    Dim status As String = UpdateScopePackage(strDesc, strSort, 0)
                End If
            End If
            For i = 0 To _gvScope.DirtyRows.Count - 1
                intRow = _gvScope.DirtyRows.Item(i).DataItemIndex
                intScopeSeqId = Me._gvScope.DataKeys.Item(intRow).Value
                tbDesc = CType(Me._gvScope.Rows(intRow).FindControl("_tbDesc"), TextBox)
                tbSort = CType(Me._gvScope.Rows(intRow).FindControl("_tbSort"), TextBox)
                strDesc = tbDesc.Text
                strSort = tbSort.Text

                UpdateScopePackage(strDesc, strSort, intScopeSeqId)

            Next

            Me._tbNewDesc.Text = ""
            Me._tbNewSort.Text = ""

            GetScope()
            'PopulateNewResource()
        Catch
            Throw
        End Try
    End Sub
    Private Function UpdateScopePackage(ByVal strDesc As String, ByVal strSort As String, ByVal intScopeSeqId As String) As String

        Dim dr As OracleDataReader = Nothing
        Dim status As String
        Dim paramCollection As New OracleParameterCollection

        Try

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_outagescopeseqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = intScopeSeqId
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_outagenumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = OutageNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_sortorder"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strSort
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_desc"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strDesc
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_userid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userProfile.Username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Outage.UpdateScope")
            If status <> "0" Then
                Throw New DataException("UpdateScopePackage Oracle Error:" & status)
            End If
        Catch ex As Exception
            Throw New DataException("UpdateScopePackage", ex)
            status = -1
        Finally
            If Not dr Is Nothing Then dr = Nothing
        End Try
        Return status
    End Function

    'Sub DeleteAction(ByVal strSeqId As String)

    '    Dim dr As OracleDataReader = Nothing
    '    Dim paramCollection As New OracleParameterCollection

    '    Try
    '        Dim param As New OracleParameter

    '        param = New OracleParameter
    '        param.ParameterName = "in_actionid"
    '        param.OracleDbType = OracleDbType.VarChar
    '        param.Direction = Data.ParameterDirection.Input
    '        param.Value = strSeqId
    '        paramCollection.Add(param)
    '        Dim status As String
    '        status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "riactions.deleteaction")

    '    Catch ex As Exception
    '        Throw New DataException("DeleteAction", ex)
    '    Finally
    '        If Not dr Is Nothing Then dr = Nothing
    '    End Try

    'End Sub


    'Protected Sub _gvCorrectiveActions_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles _gvCorrectiveActions.RowDeleting
    '    Try
    '        Dim strSeqid As Integer = _
    '            Convert.ToInt32(_gvCorrectiveActions.DataKeys(e.RowIndex).Value)

    '        DeleteAction(strSeqid)

    '        GetActions()

    '    Catch ex As Exception
    '        Throw New Exception("_gvCorrectiveActions.DeleteCommand", ex.InnerException)
    '    End Try

    'End Sub

    'Sub SendEMail(ByVal strResource As String, ByVal strPriority As String, ByVal strTaskDesc As String, ByVal strComments As String, ByVal strWO As String, ByVal dtEstDate As String)
    '    Dim Body As String
    '    Dim Email As String = Nothing
    '    Dim dr As OracleDataReader = Nothing
    '    Dim urlHost As String

    '    Try
    '        dr = GetEmail(strResource)
    '        If Not dr Is Nothing Then
    '            dr.Read()
    '            If dr.HasRows = True Then
    '                If Not dr.Item("email") Is Nothing Then
    '                    Email = CStr(dr.Item("email"))
    '                End If
    '            End If
    '        End If

    '        If Request.UserHostAddress = "127.0.0.1" Then
    '            urlHost = "Http://ridev/ri/ri/"
    '        Else
    '            If Request.ServerVariables("HTTP_HOST").ToLower.Contains("ridev") Then
    '                urlHost = "Http://ridev/ri/ri/"
    '            ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritest") Then
    '                urlHost = "Http://ritest/ri/ri/"
    '            Else
    '                urlHost = "Http://ri/ri/ri/"
    '            End If

    '        End If

    '        Dim ToAddress As String = Email
    '        Dim FromAddress As String = userProfile.Email

    '        '(1) Create the MailMessage instance
    '        'Dim mm As New System.Net.Mail.MailMessage("RootCause.FailureAnalysis@ipaper.com", ToAddress)
    '        'Dim mm As New System.Net.Mail.MailMessage(FromAddress, ToAddress)

    '        '(2) Assign the MailMessage's properties
    '        'mm.Subject = "New RCFA Corrective Action Item Assigned to you - " & strTaskDesc
    '        Body = "<html>"
    '        Body = Body & "<head>"
    '        Body = Body & "<title>" & Master.RIRESOURCES.GetResourceValue("Assigned") & "</title>"
    '        Body = Body & "</head>"
    '        Body = Body & "<body bgcolor=""FFFFFF"">"
    '        Body = Body & "<p><font size = ""2"" face=""Arial""><strong>"
    '        Body = Body & "<br>" & Master.RIRESOURCES.GetResourceValue("The following Action Item has been assigned to you:") & "</strong></p>"
    '        Body = Body & "<ul>"
    '        Body = Body & "<br></br><strong><li>" & Master.RIRESOURCES.GetResourceValue("Task Description") & ": </strong>" & Trim(strTaskDesc)
    '        If strWO <> "" Then
    '            Body = Body & "<br></br><strong><li>" & Master.RIRESOURCES.GetResourceValue("Work Order") & ":  " & "</strong>" & Trim(strWO)
    '        End If
    '        Body = Body & "<strong><li>" & Master.RIRESOURCES.GetResourceValue("Priority") & ": " & "</strong>" & Master.RIRESOURCES.GetResourceValue(Trim(strPriority))
    '        Body = Body & "<strong><li>" & Master.RIRESOURCES.GetResourceValue("Due Date") & ": " & "</strong>" & IP.Bids.Localization.DateTime.GetLocalizedDateTime(Trim(dtEstDate))
    '        Body = Body & "<strong><li>" & Master.RIRESOURCES.GetResourceValue("Additional Information") & ": " & "</strong>" & Trim(strComments)
    '        Body = Body & "</ul>"
    '        Body = Body & "<p><br></br><TD><A HREF=" & urlHost & "EnterNewRI.aspx?RINumber=" & riNumber & ">" & Master.RIRESOURCES.GetResourceValue("Click here to Review Incident and Update Corrective Action Task") & "</A></TD>" & "</br></TR></p>"
    '        Body = Body & "</body>"
    '        Body = Body & "</html>"

    '        'mm.Body = Body
    '        'mm.IsBodyHtml = True

    '        '(3) Create the SmtpClient object
    '        'Dim smtp As New System.Net.Mail.SmtpClient

    '        RI.SharedFunctions.SendEmail(ToAddress, FromAddress, Master.RIRESOURCES.GetResourceValue("New Action Item Assigned to you from") & " " & Master.RIRESOURCES.GetResourceValue("Incident") & " " & riNumber, Body, userProfile.FullName)
    '        '(4) Send the MailMessage (will use the Web.config settings)
    '        'If RI.SharedFunctions.isEmail(mm.To.ToString) And RI.SharedFunctions.isEmail(mm.From.ToString) Then
    '        'smtp.Send(mm)
    '        'Else
    '        'RI.SharedFunctions.InsertAuditRecord("Corrective Actions Send Email", "Unable to send an email for " & mm.To.ToString & Body)
    '        'End If
    '    Catch ex As Exception
    '        Throw
    '    Finally
    '        If dr IsNot Nothing Then
    '            dr.Close()
    '            dr = Nothing
    '        End If
    '    End Try

    'End Sub
    'Function GetEmail(ByVal strResource As String) As OracleDataReader

    '    Dim cmdSQL As OracleCommand = Nothing
    '    Dim connection As String = String.Empty
    '    Dim provider As String = String.Empty
    '    Dim daData As OracleDataAdapter = Nothing
    '    Dim cnConnection As Devart.Data.Oracle.OracleConnection = Nothing
    '    Dim dr As OracleDataReader = Nothing
    '    Dim paramCollection As New OracleParameterCollection

    '    Try
    '        If connection.Length = 0 Then
    '            connection = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
    '        End If
    '        If provider.Length = 0 Then
    '            provider = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ProviderName
    '        End If

    '        cmdSQL = New OracleCommand
    '        With cmdSQL
    '            cnConnection = New OracleConnection(connection)
    '            cnConnection.Open()
    '            .Connection = cnConnection
    '            .CommandText = "rimaint.rigetemail"
    '            .CommandType = CommandType.StoredProcedure

    '            Dim param As New OracleParameter

    '            param = New OracleParameter
    '            param.ParameterName = "in_username"
    '            param.OracleDbType = OracleDbType.VarChar
    '            param.Direction = Data.ParameterDirection.Input
    '            param.Value = strResource
    '            .Parameters.Add(param)

    '            param = New OracleParameter
    '            param.ParameterName = "out_email"
    '            param.OracleDbType = OracleDbType.Cursor
    '            param.Direction = Data.ParameterDirection.Output
    '            .Parameters.Add(param)
    '        End With

    '        dr = cmdSQL.ExecuteReader(CommandBehavior.CloseConnection)

    '    Catch ex As Exception
    '        Return Nothing
    '        Throw New DataException("GetEmail", ex)
    '    Finally
    '        GetEmail = dr
    '        If Not daData Is Nothing Then daData = Nothing
    '        If Not cmdSQL Is Nothing Then cmdSQL = Nothing
    '    End Try

    'End Function
    'Private Function GetNewResourceList() As DataTable
    '    'Call the package to populate the New Resource ddl

    '    Dim ds As DataSet = Nothing

    '    Try
    '        If dtPersonList Is Nothing Then
    '            ds = GetPersonDSFromPackage()
    '            If ds IsNot Nothing Then
    '                dtPersonList = ds.Tables(0)
    '            End If
    '        End If
    '    Catch
    '        Throw
    '    Finally
    '        If ds IsNot Nothing Then ds = Nothing
    '        GetNewResourceList = dtPersonList
    '    End Try
    'End Function
    'Protected Sub PopulateNewResource()
    '    'Call the package to populate the New Resource ddl
    '    Try
    '        _ddlNewResource.DataSource = GetNewResourceList()
    '        _ddlNewResource.DataBind()
    '        _ddlNewResource.Items.Insert(0, "")
    '        _ddlNewResource.SelectedIndex = "0"
    '    Catch
    '        Throw
    '    End Try

    'End Sub

    'Protected Sub _ddlSort_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlSort.TextChanged
    '    GetActions()
    'End Sub

    'Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
    '    'GetEmployeeList
    '    Dim sb As New StringBuilder
    '    sb.Append("GetEmployeeList('{0}','{1}');")
    '    ScriptManager.RegisterStartupScript(Me._udpScope, _udpScope.GetType, "ResourceList", String.Format(sb.ToString, siteID, Me._ddlNewResource.ClientID), True)
    'End Sub

    'Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
    '    dtPersonList = Nothing
    'End Sub

    'Protected Sub _gvCorrectiveActions_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles _gvCorrectiveActions.RowEditing

    'End Sub
End Class
