Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Devart.Data.Oracle
Imports System.Web.Mail

Partial Class RI_RCFACorrectiveActionItems
    Inherits RIBasePage

    Dim userProfile As RI.CurrentUserProfile = Nothing

    ''' <summary>
    ''' Stores the selected RINumber in Session
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns the RiNumber</returns>
    ''' <remarks></remarks>
    Public Property riNumber() As String
        Get
            If mRINumber.Length = 0 Then mRINumber = Request.QueryString("RINumber")
            Return mRINumber
        End Get
        Set(ByVal value As String)
            mRINumber = value
        End Set
    End Property
    Private mRINumber As String = String.Empty
    Private msiteID As String = String.Empty    
    Public Property siteID() As String
        Get
            If msiteID.Length = 0 Then msiteID = Request.QueryString("Siteid")
            Return msiteID
        End Get
        Set(ByVal value As String)
            msiteID = value
        End Set
    End Property
    Private dtPersonList As DataTable = Nothing

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("Action Items", True))
        'Master.SetBanner(Master.RIRESOURCES.GetResourceValue("ViewIncidents"))
        Master.ShowPopupMenu()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load        

        RI.SharedFunctions.DisablePageCache(Page.Response)        
        userProfile = RI.SharedFunctions.GetUserProfile        
        ScriptManager.RegisterClientScriptInclude(Me._udpAW, _udpAW.GetType, "ActionItems", Page.ResolveClientUrl("~/RI/ActionItems.js"))
        Dim sc As ScriptManager
        sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
        If sc IsNot Nothing Then
            Dim loService As New ServiceReference
            loService.InlineScript = False
            loService.Path = "~/RISharedWS.asmx"
            sc.Services.Add(loService)
        End If
        If Not Page.IsPostBack Then
            GetActions()
        End If
        Me._dtNewCompDate.HeaderText = Master.RIRESOURCES.GetResourceValue("SELECT DATE COMPLETED")
        Me._dtNewEstDate.HeaderText = Master.RIRESOURCES.GetResourceValue("SELECT ESTIMATED COMPLETION DATE")
    End Sub
   
    Private Sub GetActions()
        Dim dr As OracleDataReader = Nothing
        Try
            dr = GetActionsDRFromPackage()

            'Depending on the contents of ds, show table to add first record and bind data
            If dr Is Nothing Then
                Me._ddlSort.Visible = "False"
            Else
                _gvCorrectiveActions.DataSource = dr
                _gvCorrectiveActions.DataBind()
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

    Private Function GetActionsDRFromPackage() As OracleDataReader
        Dim dr As OracleDataReader = Nothing
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
            param.ParameterName = "in_sortorder"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = _ddlSort.SelectedValue
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsActions"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "RIActions.GetActions")
        Catch ex As Exception
            Throw New DataException("GetActions", ex)
            Return Nothing
        Finally
            GetActionsDRFromPackage = dr            
        End Try
    End Function

   

    Protected Sub _gvCorrectiveActions_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _gvCorrectiveActions.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                'Now, reference the Button control that the Delete ButtonColumn has been rendered to 
                Dim deleteButton As Button = TryCast(e.Row.FindControl("_lnkBtnDelete"), Button)
                If deleteButton IsNot Nothing Then
                    'We can now add the onclick event handler                
                    deleteButton.OnClientClick = "Javascript:ConfirmDelete('" & e.Row.DataItem("actionnumber").ToString & "'," & e.Row.RowIndex & ",'" & Me._gvCorrectiveActions.ClientID & "');return false;"
                End If
                Dim hdRowChange As HiddenField = TryCast(e.Row.FindControl("_rowChanged"), HiddenField)
                If hdRowChange IsNot Nothing Then
                    Dim rowChangedJS As String = String.Format("document.getElementById('{0}').value='changed';", hdRowChange.ClientID)
                    Dim ddlResource As DropDownList = TryCast(e.Row.FindControl("_ddlResource"), DropDownList)
                    If ddlResource IsNot Nothing Then
                        ddlResource.Attributes.Add("onChange", rowChangedJS)
                    End If
                    Dim ddlPriority As DropDownList = TryCast(e.Row.FindControl("_ddlPriority"), DropDownList)
                    If ddlPriority IsNot Nothing Then
                        ddlPriority.Attributes.Add("onChange", rowChangedJS)
                        Dim priority As String = RI.SharedFunctions.DataClean(e.Row.DataItem("Priority"))
                        If ddlPriority.Items.FindByValue(priority) IsNot Nothing Then
                            ddlPriority.SelectedValue = priority
                        End If
                    End If
                    Dim ddlRepeat As DropDownList = TryCast(e.Row.FindControl("_ddlRepeat"), DropDownList)
                    If ddlRepeat IsNot Nothing Then
                        ddlRepeat.Attributes.Add("onChange", rowChangedJS)
                        Dim repeatunits As String = RI.SharedFunctions.DataClean(e.Row.DataItem("repeatunits"))
                        If ddlRepeat.Items.FindByValue(repeatunits) IsNot Nothing Then
                            ddlRepeat.SelectedValue = repeatunits
                        End If
                    End If
                    Dim ddl As DropDownList = TryCast(e.Row.FindControl("_ddlResource"), DropDownList)
                    If ddlResource IsNot Nothing Then
                        If e.Row.DataItem("username").ToString.Length > 0 Then
                            ddlResource.Items.Add(New ListItem(e.Row.DataItem("Person").ToString, e.Row.DataItem("username").ToString))
                            ddlResource.SelectedIndex = 1
                        End If
                        ddlResource.Attributes.Add("onfocus", "copyResourceList('" & Me._ddlNewResource.ClientID & "',this);")
                    End If
                End If
                Dim CompDate As RI_User_Controls_Common_ucDateTime = TryCast(e.Row.FindControl("_actCompDate"), RI_User_Controls_Common_ucDateTime)
                Dim EstDate As RI_User_Controls_Common_ucDateTime = TryCast(e.Row.FindControl("_estDueDate"), RI_User_Controls_Common_ucDateTime)
                If CompDate IsNot Nothing Then
                    CompDate.HeaderText = Master.RIRESOURCES.GetResourceValue("SELECT DATE COMPLETED")
                End If
                If EstDate IsNot Nothing Then
                    EstDate.HeaderText = Master.RIRESOURCES.GetResourceValue("SELECT ESTIMATED COMPLETION DATE")
                End If
                'Me._dtNewCompDate.HeaderText = Master.RIRESOURCES.GetResourceValue("SELECT DATE COMPLETED")
                'Me._dtNewEstDate.HeaderText = Master.RIRESOURCES.GetResourceValue("SELECT ESTIMATED COMPLETION DATE")
            End If
        Catch
            Throw
        End Try
    End Sub
    Private Function GetPersonDSFromPackage() As DataSet
        Dim ds As DataSet = Nothing
        Dim paramCollection As New OracleParameterCollection

        Try

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = siteID
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsPerson"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "RI.PersonDDL", "Person_" & siteID, 3)

        Catch ex As Exception
            Throw New DataException("GetPerson", ex)
            Return Nothing
        Finally
            GetPersonDSFromPackage = ds
            If Not ds Is Nothing Then ds = Nothing
        End Try
    End Function
    Protected Sub _btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnSave.Click

        UpdateActionItems()
        'GetActions()

    End Sub
    Protected Sub _btnSaveClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnSaveClose.Click

        UpdateActionItems()

        'Response.Write("<script language='javascript'> { try{window.opener.location.reload(true);}catch(err){}window.close(); }</script>")
        Response.Write("<script language='javascript'> { try{window.opener.updateItemCounts();}catch(err){}window.close(); }</script>")
        'updateItemCounts

    End Sub
    Protected Sub UpdateActionItems()
        Dim i As Integer = 0
        Dim intActionNumber, intRow As Integer
        Dim strPriority, strResource, strWO As String
        Dim strRepeat, strRepeatUnitQty, strTaskDesc, strComments As String
        Dim tbEstDate, tbCompDate As TextBox
        Dim tbTaskDesc, tbComments, tbWO, tbRepeatUnitQty As TextBox
        Dim ddlResource, ddlPriority, ddlRepeat As DropDownList
        Dim dtEstDate, dtCompDate As String
        Dim ucEstDate, ucCompDate As UserControl

        Try
            'Check whether we are dealing with the first record.  If so, you will not have a seqid so pass in a "0"
            If Me._tblNewRow.Visible = "true" Then
                If Me._tbNewTaskDesc.Text <> Nothing And Me._ddlNewPriority.SelectedValue <> "" And Me._dtNewEstDate.DateValue <> Nothing Then
                    strTaskDesc = Me._tbNewTaskDesc.Text
                    strComments = Me._tbNewComments.Text
                    strPriority = Me._ddlNewPriority.SelectedValue
                    strWO = Me._txtNewWO.Text
                    strResource = Request.Form(Me._ddlNewResource.UniqueID)
                    dtEstDate = Me._dtNewEstDate.DateValue
                    If IsDBNull(dtEstDate) Then dtEstDate = DBNull.Value.ToString
                    dtCompDate = Me._dtNewCompDate.DateValue
                    If IsDBNull(dtCompDate) Then dtCompDate = DBNull.Value.ToString
                    strRepeatUnitQty = _txtNewRepeatUnitQty.Text
                    strRepeat = Me._ddlNewRepeatUnits.SelectedValue
                    Dim status As String = UpdateActionsPackage(strResource, strPriority, strTaskDesc, strComments, 0, strWO, strRepeat, strRepeatUnitQty, dtEstDate, dtCompDate)
                    If status = "0" Then SendEMail(strResource, strPriority, strTaskDesc, strComments, strWO, dtEstDate)
                End If
            End If
            For i = 0 To _gvCorrectiveActions.DirtyRows.Count - 1
                intRow = _gvCorrectiveActions.DirtyRows.Item(i).DataItemIndex
                intActionNumber = Me._gvCorrectiveActions.DataKeys.Item(intRow).Value
                ddlResource = CType(Me._gvCorrectiveActions.Rows(intRow).FindControl("_ddlResource"), DropDownList)
                ddlPriority = CType(Me._gvCorrectiveActions.Rows(intRow).FindControl("_ddlPriority"), DropDownList)
                ddlRepeat = CType(Me._gvCorrectiveActions.Rows(intRow).FindControl("_ddlRepeat"), DropDownList)
                ucEstDate = CType(Me._gvCorrectiveActions.Rows(intRow).FindControl("_estDueDate"), UserControl)
                ucCompDate = CType(Me._gvCorrectiveActions.Rows(intRow).FindControl("_actCompDate"), UserControl)
                tbTaskDesc = CType(Me._gvCorrectiveActions.Rows(intRow).FindControl("_tbTask"), TextBox)
                tbWO = CType(Me._gvCorrectiveActions.Rows(intRow).FindControl("_tbWO"), TextBox)
                tbComments = CType(Me._gvCorrectiveActions.Rows(intRow).FindControl("_tbComments"), TextBox)
                tbRepeatUnitQty = CType(Me._gvCorrectiveActions.Rows(intRow).FindControl("_tbRepeatUnit"), TextBox)
                strResource = Request.Form(ddlResource.UniqueID)
                strPriority = ddlPriority.SelectedValue
                strRepeat = ddlRepeat.SelectedValue
                strTaskDesc = tbTaskDesc.Text
                strWO = tbWO.Text
                strComments = tbComments.Text
                strRepeatUnitQty = tbRepeatUnitQty.Text
                tbEstDate = CType(ucEstDate.FindControl("_txtDate"), TextBox)
                dtEstDate = tbEstDate.Text
                If IsDBNull(dtEstDate) Or Not IsDate(dtEstDate) Then dtEstDate = DBNull.Value.ToString
                tbCompDate = CType(ucCompDate.FindControl("_txtDate"), TextBox)
                dtCompDate = tbCompDate.Text
                If IsDBNull(dtCompDate) Or Not IsDate(dtCompDate) Then dtCompDate = DBNull.Value.ToString

                UpdateActionsPackage(strResource, strPriority, strTaskDesc, strComments, intActionNumber, strWO, strRepeat, strRepeatUnitQty, dtEstDate, dtCompDate)

            Next

            Me._ddlNewResource.SelectedIndex = "0"
            Me._ddlNewPriority.SelectedValue = ""
            Me._txtNewWO.Text = ""
            Me._tbNewTaskDesc.Text = ""
            Me._tbNewComments.Text = ""
            Me._ddlNewRepeatUnits.SelectedValue = ""
            Me._txtNewRepeatUnitQty.Text = ""
            Me._dtNewEstDate.DateValue = ""
            Me._dtNewCompDate.DateValue = ""

            GetActions()
            PopulateNewResource()
        Catch
            Throw
        End Try
    End Sub
    Private Function UpdateActionsPackage(ByVal strResource As String, ByVal strPriority As String, ByVal strTaskDesc As String, ByVal strComments As String, ByVal ActionId As String, ByVal strWO As String, ByVal strRepeatUnits As String, ByVal strRepeatUnitsQty As String, ByVal dtEstDate As String, ByVal dtCompDate As String) As String

        Dim dr As OracleDataReader = Nothing
        Dim status As String
        Dim paramCollection As New OracleParameterCollection

        Try

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_actionid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = ActionId
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_rinumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = riNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_taskdesc"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strTaskDesc
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_estcompdate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input

            param.Value = IP.Bids.Localization.DateTime.GetLocalizedDateTime(dtEstDate, "EN-US", "d")
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_actcompdate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = IP.Bids.Localization.DateTime.GetLocalizedDateTime(dtCompDate, "EN-US", "d")
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_priority"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strPriority
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_comments"
            param.OracleDbType = OracleDbType.NClob
            param.Direction = Data.ParameterDirection.Input
            param.Value = strComments
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_resource"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strResource
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_wonumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strWO
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_repeatunits"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strRepeatUnits
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_repeatunitsqty"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strRepeatUnitsQty
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

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "riactions.IncidentActions")
            If status <> "0" Then
                Throw New DataException("UpdateActionsPackage Oracle Error:" & status)
            End If
        Catch ex As Exception
            Throw New DataException("UpdateActionsPackage", ex)
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

    Sub SendEMail(ByVal strResource As String, ByVal strPriority As String, ByVal strTaskDesc As String, ByVal strComments As String, ByVal strWO As String, ByVal dtEstDate As String)
        Dim Body As String
        Dim Email As String = Nothing
        Dim dr As OracleDataReader = Nothing
        Dim urlHost As String

        Try
            dr = GetEmail(strResource)
            If Not dr Is Nothing Then
                dr.Read()
                If dr.HasRows = True Then
                    If Not dr.Item("email") Is Nothing Then
                        Email = CStr(dr.Item("email"))
                    End If
                End If
            End If

            If Request.UserHostAddress = "127.0.0.1" Then
                urlHost = "Http://ridev/ri/ri/"
            Else
                If Request.ServerVariables("HTTP_HOST").ToLower.Contains("ridev") Then
                    urlHost = "Http://ridev/ri/ri/"
                ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritest") Then
                    urlHost = "Http://ritest/ri/ri/"
                Else
                    urlHost = "Http://ri/ri/ri/"

                End If

            End If

            Dim ToAddress As String = Email
            Dim FromAddress As String = userProfile.Email

            '(1) Create the MailMessage instance
            'Dim mm As New System.Net.Mail.MailMessage("RootCause.FailureAnalysis@ipaper.com", ToAddress)
            'Dim mm As New System.Net.Mail.MailMessage(FromAddress, ToAddress)

            '(2) Assign the MailMessage's properties
            'mm.Subject = "New RCFA Corrective Action Item Assigned to you - " & strTaskDesc
            Body = "<html>"
            Body = Body & "<head>"
            Body = Body & "<title>" & Master.RIRESOURCES.GetResourceValue("Assigned") & "</title>"
            Body = Body & "</head>"
            Body = Body & "<body bgcolor=""FFFFFF"">"
            Body = Body & "<p><font size = ""2"" face=""Arial""><strong>"
            Body = Body & "<br>" & Master.RIRESOURCES.GetResourceValue("The following Action Item has been assigned to you:") & "</strong></p>"
            Body = Body & "<ul>"
            Body = Body & "<br></br><strong><li>" & Master.RIRESOURCES.GetResourceValue("Task Description") & ": </strong>" & Trim(strTaskDesc)
            If strWO <> "" Then
                Body = Body & "<br></br><strong><li>" & Master.RIRESOURCES.GetResourceValue("Work Order") & ":  " & "</strong>" & Trim(strWO)
            End If
            Body = Body & "<strong><li>" & Master.RIRESOURCES.GetResourceValue("Priority") & ": " & "</strong>" & Master.RIRESOURCES.GetResourceValue(Trim(strPriority))
            Body = Body & "<strong><li>" & Master.RIRESOURCES.GetResourceValue("Due Date") & ": " & "</strong>" & IP.Bids.Localization.DateTime.GetLocalizedDateTime(Trim(dtEstDate))
            Body = Body & "<strong><li>" & Master.RIRESOURCES.GetResourceValue("Additional Information") & ": " & "</strong>" & Trim(strComments)
            Body = Body & "</ul>"
            Body = Body & "<p><br></br><TD><A HREF=" & urlHost & "EnterNewRI.aspx?RINumber=" & riNumber & ">" & Master.RIRESOURCES.GetResourceValue("Click here to Review Incident and Update Corrective Action Task") & "</A></TD>" & "</br></TR></p>"
            Body = Body & "</body>"
            Body = Body & "</html>"

            'mm.Body = Body
            'mm.IsBodyHtml = True

            '(3) Create the SmtpClient object
            'Dim smtp As New System.Net.Mail.SmtpClient

            RI.SharedFunctions.SendEmail(ToAddress, FromAddress, Master.RIRESOURCES.GetResourceValue("New Action Item Assigned to you from") & " " & Master.RIRESOURCES.GetResourceValue("Incident") & " " & riNumber, Body, userProfile.FullName)
            '(4) Send the MailMessage (will use the Web.config settings)
            'If RI.SharedFunctions.isEmail(mm.To.ToString) And RI.SharedFunctions.isEmail(mm.From.ToString) Then
            'smtp.Send(mm)
            'Else
            'RI.SharedFunctions.InsertAuditRecord("Corrective Actions Send Email", "Unable to send an email for " & mm.To.ToString & Body)
            'End If
        Catch ex As Exception
            Throw
        Finally
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
        End Try

    End Sub
    Function GetEmail(ByVal strResource As String) As OracleDataReader

        Dim cmdSQL As OracleCommand = Nothing
        Dim connection As String = String.Empty
        Dim provider As String = String.Empty
        Dim daData As OracleDataAdapter = Nothing
        Dim cnConnection As Devart.Data.Oracle.OracleConnection = Nothing
        Dim dr As OracleDataReader = Nothing
        Dim paramCollection As New OracleParameterCollection

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
                .CommandText = "rimaint.rigetemail"
                .CommandType = CommandType.StoredProcedure

                Dim param As New OracleParameter

                param = New OracleParameter
                param.ParameterName = "in_username"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = strResource
                .Parameters.Add(param)

                param = New OracleParameter
                param.ParameterName = "out_email"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = Data.ParameterDirection.Output
                .Parameters.Add(param)
            End With

            dr = cmdSQL.ExecuteReader(CommandBehavior.CloseConnection)

        Catch ex As Exception
            Return Nothing
            Throw New DataException("GetEmail", ex)
        Finally
            GetEmail = dr
            If Not daData Is Nothing Then daData = Nothing
            If Not cmdSQL Is Nothing Then cmdSQL = Nothing
        End Try

    End Function
    Private Function GetNewResourceList() As DataTable
        'Call the package to populate the New Resource ddl

        Dim ds As DataSet = Nothing

        Try
            If dtPersonList Is Nothing Then
                ds = GetPersonDSFromPackage()
                If ds IsNot Nothing Then
                    dtPersonList = ds.Tables(0)
                End If
            End If
        Catch
            Throw
        Finally
            If ds IsNot Nothing Then ds = Nothing
            GetNewResourceList = dtPersonList
        End Try
    End Function
    Protected Sub PopulateNewResource()
        'Call the package to populate the New Resource ddl
        Try           
            _ddlNewResource.DataSource = GetNewResourceList()
            _ddlNewResource.DataBind()
            _ddlNewResource.Items.Insert(0, "")
            _ddlNewResource.SelectedIndex = "0"
        Catch
            Throw
        End Try

    End Sub

    Protected Sub _ddlSort_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlSort.TextChanged
        GetActions()
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        'GetEmployeeList
        Dim sb As New StringBuilder
        sb.Append("GetEmployeeList('{0}','{1}');")
        ScriptManager.RegisterStartupScript(Me._udpAW, _udpAW.GetType, "ResourceList", String.Format(sb.ToString, siteID, Me._ddlNewResource.ClientID), True)
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        dtPersonList = Nothing
    End Sub

    Protected Sub _gvCorrectiveActions_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles _gvCorrectiveActions.RowEditing

    End Sub
End Class
