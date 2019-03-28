Imports System.Data
Imports Devart.Data.Oracle


Partial Class _RIFileUpload
    'Inherits System.Web.UI.Page
    Inherits RIBasePage

    Public Function GetUploadURL(ByVal strfile As String) As String
        'Dim strURL As String
        'Dim strServername As String = Request("Server_Name").ToLower
        ''If you are running on your local pc, the files will be saved to ridev.
        'If strServername.Contains("localhost") Or strServername.Contains("ridev") Or strServername.Contains("ritest") Then
        '    strServername = "http://s02arelprd01/meas/development/ri/uploads/"
        'Else
        '    strServername = "http://s02arelprd01/meas/production/ri/uploads/"
        'End If

        'If strServername.Contains("localhost") Then
        '    'strServername = "http://ridev/Uploads/"
        '    strServername = "http://s02arelprd01/meas/development/ri/uploads/"
        'Else
        '    strServername = "http://" & strServername & "/Uploads/"
        'End If

        Return GetUploadPath() & strfile
    End Function
    Public Function GetUploadPath() As String
        '11/30/07 - ALA - There is an uploads within the RI directory on each server.
        'The server.mappath will get the path the page is currently run from.  
        Dim strServername As String = Request("Server_Name").ToLower
        Dim filepath As String
        filepath = Server.MapPath("")
        filepath = UCase(filepath)
        'Response.Write(filepath)
        If strServername.Contains("localhost") Or strServername.Contains("ridev") Or strServername.Contains("ritest") Then
            filepath = "\\GPIAZRELFPRD01\uploads_ri\"
            'filepath = "\\s02awid03\ridev\uploads\"
            'filepath = Replace(filepath, "RI\RI", "RI\Uploads\")
        Else
            filepath = "\\GPIAZRELFPRD01\uploads_ri\"
            'If Request.ServerVariables("HTTP_HOST").ToLower.Contains("ridev") Then
            '    filepath = Replace(filepath, "RIDEV\RI\RI", "RIDEV\Uploads\")
            'ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritest") Then
            '    filepath = Replace(filepath, "RITEST\RI\RI", "RITEST\Uploads\")
            'ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritrain") Then
            '    filepath = Replace(filepath, "RITRAIN\RI\RI", "RITRAIN\Uploads\")
            'Else
            '    filepath = Replace(filepath, "RI\RI\RI", "RI\Uploads\")
            'End If
        End If

        'Response.Write(filepath)
        'Response.Write("<br>" & strServername)
        Return filepath
    End Function

    ''' <summary>
    ''' Stores the selected RINumber in Session
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns the RiNumber</returns>
    ''' <remarks></remarks>
    Public Property riNumber() As String
        Get
            Return Session("riNumber")
        End Get
        Set(ByVal value As String)
            Session("riNumber") = value
        End Set
    End Property

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("FileUploadTitle", True, "Shared"))
        Master.HideMainMenu()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Dim filepath As String = GetUploadPath()
            Dim strFile As String = Request.QueryString("file") '-- if something was passed to the file querystring  

            If strFile <> "" Then
                'get absolute path of the file  
                Dim path As String = filepath & strFile 'get file object as FileInfo  
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path) '-- if the file exists on the server  

                If file.Exists Then
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                    Response.AddHeader("Content-Length", file.Length.ToString())
                    Response.ContentType = "application/octet-stream"
                    Response.WriteFile(file.FullName)
                    Response.End()
                Else
                    Response.Write("This file does not exist.")
                End If
            End If

            'Set RINumber to whatever the current incident is
            riNumber = Request.QueryString("RINumber")
            'Get Listing of Current Attachments for incident
            GetRIAttachments()
        End If

    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If fileUpEx.HasFile = True Then
            Dim fileLocation As String = String.Empty
            Try

                Dim filepath As String = GetUploadPath()
                Dim file As String = Replace(fileUpEx.FileName, "#", "no")
                Dim filedesc As String = Me._txtDescr.Text.ToString

                'Call procedure to update the tblrcfaattachment table
                fileLocation = filepath & riNumber & file
                UpdateAttachmentListWithPackage(riNumber & file, filepath & riNumber & file, filedesc, "I")

                'save the file to the server   
                fileUpEx.PostedFile.SaveAs(fileLocation)

                'Update Status
                _lblStatus.Text = Master.RIRESOURCES.GetResourceValue("FileLocation") & " " & fileLocation

                'Need to get current attachments for incident
                GetRIAttachments()

                'Blank out description text field
                _txtDescr.Text = ""

            Catch ex As Exception
                _lblStatus.Text = Master.RIRESOURCES.GetResourceValue("Error") & fileLocation & "<br>" & ex.Message & "<br>" & Server.MapPath("")
            End Try
        Else
            _lblStatus.Text = Master.RIRESOURCES.GetResourceValue("You have not specified a file")
        End If

    End Sub

    Sub GetRIAttachments()
        Dim ds As DataSet = Nothing

        ds = GetAttachmentListDSFromPackage()

        'Depending on the contents of ds, show message and bind data
        If ds Is Nothing Then
            _lblFileStatus.Text = String.Format(Master.RIRESOURCES.GetResourceValue("FileUploadNoFiles", True, "Shared"))
        Else
            _lblFileStatus.Text = String.Format(Master.RIRESOURCES.GetResourceValue("FileUploadCurrentFiles", True, "Shared")) & " " & riNumber

            _dlFileList.DataSource = ds
            _dlFileList.DataBind()

        End If

    End Sub
    Private Function GetAttachmentListDSFromPackage() As DataSet
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
            param.ParameterName = "rsAttachments"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "rimaint.RIGetAttachments", "RIAttachments", 0)

        Catch ex As Exception
            Throw New DataException("GetAttachmentListDSFromPackage", ex)
            Return Nothing
        Finally
            GetAttachmentListDSFromPackage = ds
            If Not ds Is Nothing Then ds = Nothing
        End Try
    End Function
    Private Sub UpdateAttachmentListWithPackage(ByVal file As String, ByVal savedfile As String, ByVal filedescr As String, ByVal action As String)
        Dim dr As OracleDataReader = Nothing
        Dim status As String
        Dim userId As String
        Dim paramCollection As New OracleParameterCollection

        Dim userProfile As RI.CurrentUserProfile = Nothing
        userProfile = RI.SharedFunctions.GetUserProfile
        userId = userProfile.Username

        Try
            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_rinumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = riNumber
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_filename"
            param.OracleDbType = OracleDbType.NVarChar
            param.Value = file
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_savedfilename"
            param.OracleDbType = OracleDbType.NVarChar
            param.Value = savedfile
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_description"
            param.OracleDbType = OracleDbType.NVarChar
            param.Value = filedescr
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_userid"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = userId
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_action"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = action
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "rimaint.riAttachments")

            If status <> 0 Then
                Me._lblFileStatus.Text = status
                Exit Sub
            End If

        Catch ex As Exception
            Throw New DataException("GetAttachmentListDSFromPackage", ex)
        Finally
            If Not dr Is Nothing Then dr = Nothing
        End Try
    End Sub

    Protected Sub _dlFileList_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles _dlFileList.DeleteCommand

        Dim filepath As String = GetUploadPath()
        Dim Filename As String

        Try
            Filename = _dlFileList.DataKeys(e.Item.ItemIndex)

            System.IO.File.Delete(filepath & Filename)

            'Call Package to delete record from DB
            UpdateAttachmentListWithPackage(Filename, "", "", "D")

            _lblStatus.Text = Filename & " " & Master.RIRESOURCES.GetResourceValue("FileDeleted", True)
            _txtDescr.Text = ""

            GetRIAttachments()

        Catch ex As Exception
            _lblStatus.Text = "Error Deleting file"
            Throw New Exception("_dlFileList_DeleteCommand", ex.InnerException)
        End Try

    End Sub

    Sub _dlFileList_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles _dlFileList.ItemDataBound
        ' First, make sure we're NOT dealing with a Header or Footer row
        If e.Item.ItemType <> ListItemType.Header And _
             e.Item.ItemType <> ListItemType.Footer Then
            'Now, reference the LinkButton control that the Delete ButtonColumn 
            'has been rendered to
            Dim deleteButton As Button = e.Item.FindControl("_lnkBtnDelete")

            'We can now add the onclick event handler
            deleteButton.Attributes("onclick") = "javascript:return " & _
                       "confirm('" & Master.RIRESOURCES.GetResourceValue("ConfirmFileDelete", True) & _
                       DataBinder.Eval(e.Item.DataItem, "filename") & "?')"
        End If
    End Sub

    Protected Sub _btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnClose.Click
        Response.Write("<script language='javascript'> { try{window.opener.updateItemCounts();}catch(err){}window.close(); }</script>")
    End Sub
End Class
