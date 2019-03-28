Imports System.Data
Imports Devart.Data.Oracle

Partial Class RI_EditActionItems
    Inherits RIBasePage

    Dim userProfile As RI.CurrentUserProfile = Nothing
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.ShowPopupMenu(New MenuItemCollection, , True)
        Master.DisplayBanner = False
        Master.ContentHeight = "100px"
    End Sub

    Private Function GetNewResourceList() As DataTable
        'Call the package to populate the New Resource ddl

        Dim ds As DataSet = Nothing

        Try
            ds = GetPersonDSFromPackage()            
        Catch ex As Exception
            Throw
        Finally           
            GetNewResourceList = ds.Tables(0)
            If ds IsNot Nothing Then ds = Nothing
        End Try
    End Function

    Private Function GetPersonDSFromPackage() As DataSet
        Dim ds As DataSet = Nothing
        Dim paramCollection As New OracleParameterCollection
        Dim siteID As String = IIf(Request.QueryString("SiteID") IsNot Nothing, Request.QueryString("SiteID"), "")
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        userProfile = RI.SharedFunctions.GetUserProfile
        If Not Page.IsPostBack Then
            Me._ddlNewResource.DataSource = GetNewResourceList()
            Me._ddlNewResource.DataBind()
            Session.Remove("ActionItems")
            If Request.QueryString("ActionNumber") IsNot Nothing And Request.QueryString("RINumber") IsNot Nothing Then
                PopulateActionItem(Request.QueryString("ActionNumber"), Request.QueryString("RINumber"))
            End If
        Else
            Page.ClientScript.RegisterStartupScript(Page.GetType, "CloseWin", "parent.closeActionItemWindow();", True)
        End If
    End Sub

    Private Sub PopulateActionItem(ByVal ActionNumber As String, ByVal RINumber As String)
        Dim dr As DataTableReader = GetActionsDRFromPackage(ActionNumber, RINumber)
        If dr IsNot Nothing Then
            dr.Read()
            If dr.HasRows Then
                Dim actionData As ActionItemData
                With actionData
                    .Resource = RI.SharedFunctions.DataClean(dr.Item("UserName"))
                    .Priority = RI.SharedFunctions.DataClean(dr.Item("priority"))
                    .EstDate = IIf(RI.SharedFunctions.DataClean(dr.Item("estcompdate")).Length = 0, "", RI.SharedFunctions.DataClean(dr.Item("estcompdate")))
                    .CompDate = IIf(RI.SharedFunctions.DataClean(dr.Item("actcompdate")).Length = 0, "", RI.SharedFunctions.DataClean(dr.Item("actcompdate")))
                    .EstDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(.EstDate, "EN-US")
                    .CompDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(.CompDate, "EN-US")
                    '.CompDate = CDate(IP.Bids.Localization.DateTime.GetLocalizedDateTime(RI.SharedFunctions.DataClean(dr.Item("actcompdate"))))
                    .WO = RI.SharedFunctions.DataClean(dr.Item("wonumber"))
                    .TaskDesc = RI.SharedFunctions.DataClean(dr.Item("taskdescription"))
                    .Comments = RI.SharedFunctions.DataClean(dr.Item("comments"))
                    .RepeatUnitQty = RI.SharedFunctions.DataClean(dr.Item("repeatunitsqty"))
                    .RepeatUnits = RI.SharedFunctions.DataClean(dr.Item("repeatunits"))

                    If Me._ddlNewResource.Items.FindByValue(.Resource) IsNot Nothing Then
                        _ddlNewResource.SelectedValue = .Resource
                    End If
                    If Me._ddlNewPriority.Items.FindByValue(.Priority) IsNot Nothing Then
                        _ddlNewPriority.SelectedValue = .Priority
                    End If
                    Me._dtNewEstDate.DateValue = .EstDate
                    Me._dtNewCompDate.DateValue = .CompDate
                    Me._txtNewWO.Text = .WO
                    Me._tbNewTaskDesc.Text = .TaskDesc
                    Me._tbNewComments.Text = .Comments
                    If Me._ddlNewRepeatUnits.Items.FindByValue(.RepeatUnits) IsNot Nothing Then
                        Me._ddlNewRepeatUnits.SelectedValue = .RepeatUnits
                    End If
                    Me._txtNewRepeatUnitQty.Text = .RepeatUnitQty
                End With
            End If
        End If
    End Sub
    Private Function GetActionsDRFromPackage(ByVal ActionNumber As String, ByVal RINumber As String) As Data.DataTableReader
        Dim dr As Data.DataTableReader = Nothing
        Dim ds As Data.DataSet = Nothing
        Dim paramCollection As New OracleParameterCollection

        Try

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_rinumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = RINumber
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_actionnumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = ActionNumber
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsActions"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)


            If ds Is Nothing Then
                ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "RIActions.GetActionItem", "GetActionItem", 0)
            End If

            'dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "RIActions.GetActions")

        Catch ex As Exception
            Throw New DataException("GetActionItem", ex)
            Return Nothing
        Finally
            GetActionsDRFromPackage = ds.CreateDataReader
            'If Not dr Is Nothing Then
            '    'dr.Close()
            '    dr = Nothing
            'End If

        End Try
    End Function
    Private Structure ActionItemData
        Public ActionNumber As String
        Public RINumber As String
        Public SiteID As String
        Public Resource As String
        Public Priority As String
        Public EstDate As String
        Public CompDate As String
        Public WO As String
        Public TaskDesc As String
        Public Comments As String
        Public RepeatUnitQty As String
        Public RepeatUnits As String
    End Structure

    Protected Sub _btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnSave.Click
        If Page.IsValid Then
            Dim actionData As ActionItemData
            With actionData
                .ActionNumber = RI.SharedFunctions.DataClean(Request.QueryString("ActionNumber"))
                .Comments = Me._tbNewComments.Text
                .CompDate = IIf(RI.SharedFunctions.DataClean(Me._dtNewCompDate.DateValue).Length = 0, DBNull.Value.ToString, Me._dtNewCompDate.DateValue)
                .EstDate = IIf(RI.SharedFunctions.DataClean(Me._dtNewEstDate.DateValue).Length = 0, DBNull.Value.ToString, Me._dtNewEstDate.DateValue)
                .Priority = Me._ddlNewPriority.SelectedValue
                .RepeatUnitQty = Me._txtNewRepeatUnitQty.Text
                .RepeatUnits = Me._ddlNewRepeatUnits.SelectedValue
                .Resource = Me._ddlNewResource.SelectedValue
                .RINumber = Request.QueryString("RINumber")
                .SiteID = Request.QueryString("SiteID")
                .TaskDesc = Me._tbNewTaskDesc.Text
                .WO = Me._txtNewWO.Text
            End With
            UpdateActionsPackage(actionData)
        End If
    End Sub
    Private Sub UpdateActionsPackage(ByVal actionData As ActionItemData)

        Dim dr As OracleDataReader = Nothing
        Dim status As String
        Dim paramCollection As New OracleParameterCollection

        Try

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_actionid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = actionData.ActionNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_rinumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = actionData.RINumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_taskdesc"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = actionData.TaskDesc
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_estcompdate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = actionData.EstDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_actcompdate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = actionData.CompDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_priority"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = actionData.Priority
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_comments"
            param.OracleDbType = OracleDbType.NClob
            param.Direction = Data.ParameterDirection.Input
            param.Value = actionData.Comments
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_resource"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = actionData.Resource
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_wonumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = actionData.WO
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_repeatunits"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = actionData.RepeatUnits
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_repeatunitsqty"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = actionData.RepeatUnitQty
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
            If actionData.ActionNumber.Length = 0 Then SendEMail(actionData)
        Catch ex As Exception
            Throw New DataException("UpdateActionsPackage", ex)
        Finally
            If Not dr Is Nothing Then dr = Nothing
        End Try

    End Sub
    Private Sub SendEMail(ByVal actionData As ActionItemData)
        Dim Body As String
        Dim Email As String = Nothing
        Dim dr As OracleDataReader = Nothing
        Dim urlHost As String

        Try
            dr = GetEmail(actionData.Resource)
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
            Body = Body & "<title>Assigned</title>"
            Body = Body & "</head>"
            Body = Body & "<body bgcolor=""FFFFFF"">"
            Body = Body & "<p><font size = ""2"" face=""Arial""><strong>"
            Body = Body & "<br>The following Action Item has been assigned to you:" & "</strong></p>"
            Body = Body & "<ul>"
            Body = Body & "<br></br><strong><li>Task Description:  " & "</strong>" & Trim(actionData.TaskDesc)
            If actionData.WO <> "" Then
                Body = Body & "<br></br><strong><li>Work Order:  " & "</strong>" & Master.RIRESOURCES.GetResourceValue(Trim(actionData.WO))
            End If
            Body = Body & "<strong><li>Priority: " & "</strong>" & Master.RIRESOURCES.GetResourceValue(Trim(actionData.Priority))
            Body = Body & "<strong><li> Due Date: " & "</strong>" & IP.Bids.Localization.DateTime.GetLocalizedDateTime(Trim(actionData.EstDate))
            Body = Body & "<strong><li>Additional Information: " & "</strong>" & Trim(actionData.Comments)
            Body = Body & "</ul>"
            Body = Body & "<p><br></br><TD><A HREF=" & urlHost & "EnterNewRI.aspx?RINumber=" & actionData.RINumber & ">" & "Click here to Review Incident and Update Action Task</A></TD>" & "</br></TR></p>"
            Body = Body & "</body>"
            Body = Body & "</html>"

            'mm.Body = Body
            'mm.IsBodyHtml = True

            '(3) Create the SmtpClient object
            'Dim smtp As New System.Net.Mail.SmtpClient

            RI.SharedFunctions.SendEmail(ToAddress, FromAddress, "New RCFA Action Item Assigned to you for Incident Number " & actionData.RINumber, Body, userProfile.FullName)
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
    Protected Sub CustomValidator1_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
        If Me._ddlNewResource.SelectedValue.Length > 0 And Me._tbNewTaskDesc.Text.Length > 0 And Me._dtNewEstDate.DateValue.Length > 0 Then
            args.IsValid = True
        Else
            args.IsValid = False
        End If
    End Sub
End Class
