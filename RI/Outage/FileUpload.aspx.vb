Imports System.Data
Imports Devart.Data.Oracle


Partial Class _FileUpload
    Inherits System.Web.UI.Page

    Public Function GetUploadURL(ByVal strfile As String) As String
        Dim strURL As String
        Dim strServername As String = Request("Server_Name").ToLower
        'If you are running on your local pc, the files will be saved to ridev.
        'Changed to store uploaded files on reliability file server 08/27/14

        If strServername.Contains("localhost") Or strServername.Contains("ridev") Or strServername.Contains("ritest") Then
            'strServername = "http://s02arelprd01/meas/development/ri/uploads/"
            strServername = "http://GPIAZRELFPRD01"
        Else
            'strServername = "http://s02arelprd01/meas/production/ri/uploads/"
            strServername = "http://GPIAZRELFPRD01"
        End If

        strURL = strServername & strfile
        Return strURL
    End Function
    ''' <summary>
    ''' The path the uploaded files are saved to depends on the server that the app is running on.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetUploadPath() As String
        '11/30/07 - ALA - There is an uploads within the RI directory on each server.
        'The server.mappath will get the path the page is currently run from.  
        Dim strServername As String = Request("Server_Name").ToLower
        Dim filepath As String
        filepath = Server.MapPath("")
        filepath = UCase(filepath)
        'Response.Write rinumber
        If strServername.Contains("localhost") Or strServername.Contains("ridev") Or strServername.Contains("ritest") Then
            filepath = "\\GPIAZRELFPRD01\uploads_ri\"
        Else
            filepath = "\\GPIAZRELFPRD01\uploads_ri\"
        End If

        Return filepath
    End Function
    ''' <summary>
    ''' Stores the selected OutageNumber in Session
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns the OutageNumber</returns>
    ''' <remarks></remarks>
    Public Property outageNumber() As String
        Get
            Return Session("outageNumber")
        End Get
        Set(ByVal value As String)
            Session("outageNumber") = value
        End Set
    End Property

    Private Sub SetupPage()
        With Me._rblFileAttachments
            If .Items.Count >= 2 Then
                If Me._rblFileAttachments.Items(0).Selected = True Then
                    Me._pnlLinkAttachments.Style.Item("display") = "none"
                    Me._pnlFileAttachments.Style.Item("display") = ""
                ElseIf Me._rblFileAttachments.Items(1).Selected = True Then
                    Me._pnlFileAttachments.Style.Item("display") = "none"
                    Me._pnlLinkAttachments.Style.Item("display") = ""
                End If
            End If
        End With
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("FileUploadTitle", True, "OUTAGE"))
        'Master.SetBanner(Resources.Outage.lblFileUploadTitle, True)
        Master.HideMainMenu()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ScriptManager.RegisterStartupScript(Me.Page, Page.GetType, "GetGlobalJSVarOutage", GetGlobalJSVarOutage, True)

        If Not Page.IsPostBack Then

            Dim filepath As String = GetUploadPath()
            Dim strFile As String = Request.QueryString("file") '-- if something was passed to the file querystring  

            If strFile <> "" Then 'get absolute path of the file  

                Dim path As String = filepath & strFile 'get file object as FileInfo  
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path) '-- if the file exists on the server  
                If file.Exists Then
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                    '        Response.AddHeader("Content-Length", file.Length.ToString())
                    Response.ContentType = "application/octet-stream"
                    Response.WriteFile(file.FullName)
                    Response.End() 'if file does not exist  
                Else
                    Response.Write("This file does not exist.")
                End If 'nothing in the URL as HTTP GET  
                With Me._rblFileAttachments
                    If .Items.Count > 0 Then
                        .Items(0).Selected = True
                        .Items(1).Selected = False
                    End If
                End With
                Me.SetupPage()

            Else
                Me.SetupPage()
            End If

            'Set RINumber to whatever the current incident is
            outageNumber = Request.QueryString("outageNumber")
            Session("OutageNumber") = outageNumber
            'Get Listing of Current Attachments for incident
            GetAttachments()
        End If

    End Sub

    ''' <summary>
    ''' Handles the Upload click event.  
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Calls the Attachment Package and saves the file to the appropriate path.</remarks>
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If fileUpEx.HasFile = True Then
            Try

                Dim filepath As String = GetUploadPath()
                Dim file As String = Replace(fileUpEx.FileName, "#", "no")
                'Dim file As String = fileUpEx.FileName
                Dim filedesc As String = Me._txtFileDescription.Text.ToString

                'Call procedure to update the tblrcfaattachment table
                UpdateAttachmentListWithPackage(outageNumber & file, filepath & outageNumber & file, filedesc, "I", filepath)

                'save the file to the server   
                fileUpEx.PostedFile.SaveAs(filepath & outageNumber & file)

                'Update Status
                _lblStatus.Text = "File Saved to: " & filepath & outageNumber & file

                'Need to get current attachments for incident
                GetAttachments()

                'Blank out description text field
                _txtFileDescription.Text = ""

            Catch ex As Exception
                _lblStatus.Text = "Error Saving file"
            End Try
        Else
            _lblStatus.Text = "You have not specified a file."
        End If

    End Sub

    ''' <summary>
    ''' Handles the Upload click event.  
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Calls the Attachment Package and saves the file to the appropriate path.</remarks>
    Protected Sub btnUploadLink_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If Me._txtLinkToAttach.Text <> "" Then
            Try

                Dim link As String = Me._txtLinkToAttach.Text.ToString 'Replace(fileUpEx.FileName, "#", "no")
                Dim linkdesc As String = Me._txtLinkDescription.Text.ToString

                'Call procedure to update the tblrcfaattachment table
                UpdateAttachmentListWithPackage(link, "", linkdesc, "I", link)


                'Update Status
                _lblStatus.Text = "Link Saved to: " & link

                'Need to get current attachments for incident
                GetAttachments()

                'Blank out description text field
                _txtLinkDescription.Text = ""

                Me._rblFileAttachments.Items(0).Selected = True
            Catch ex As Exception
                _lblStatus.Text = "Error Saving link"
            End Try
        Else
            _lblStatus.Text = "You have not specified a link."
        End If

    End Sub


    Sub GetAttachments()
        Dim ds As DataSet = Nothing

        ds = GetAttachmentListDSFromPackage()

        'Depending on the contents of ds, show message and bind data
        If ds Is Nothing Then
            _lblFileStatus.Text = String.Format(Master.RIRESOURCES.GetResourceValue("FileUploadNoFiles", True, "Shared"))
        Else
            _lblFileStatus.Text = String.Format(Master.RIRESOURCES.GetResourceValue("FileUploadCurrentFiles", True, "Shared")) & " " & outageNumber

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
            param.ParameterName = "in_outagenumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = outageNumber
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsAttachments"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "outage.GetAttachments", "Attachments", 0)

        Catch ex As Exception
            Throw New DataException("GetAttachmentListDSFromPackage", ex)
            Return Nothing
        Finally
            GetAttachmentListDSFromPackage = ds
            If Not ds Is Nothing Then ds = Nothing
        End Try
    End Function
    Private Sub UpdateAttachmentListWithPackage(ByVal file As String, ByVal savedfile As String, ByVal filedescr As String, ByVal action As String, ByVal location As String)
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
            param.ParameterName = "in_outagenumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = outageNumber
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
            param.ParameterName = "in_location"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = location
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

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "outage.UpdateAttachments")

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
    Public Function GetFileLocation(ByVal file As String, ByVal location As String) As String
        If file.Length > 0 And location.Length > 0 Then 'Attachment is a file
            Return file 'String.Format(CultureInfo.CurrentCulture, "{1}{0}", file, UploadsUrl)
        Else 'Attachment is a URL
            If location.StartsWith("www", StringComparison.CurrentCulture) Then
                location = "http://" & location
            End If
            Return location
        End If

    End Function
    Protected Sub _dlFileList_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles _dlFileList.DeleteCommand

        Dim filepath As String = GetUploadPath()
        Dim Filename As String

        Try
            Filename = _dlFileList.DataKeys(e.Item.ItemIndex)

            System.IO.File.Delete(filepath & Filename)

            'Call Package to delete record from DB
            UpdateAttachmentListWithPackage(Filename, "", "", "D", "")

            _lblStatus.Text = Filename & " File was deleted."
            _txtFileDescription.Text = ""

            GetAttachments()

        Catch ex As Exception
            _lblStatus.Text = "Error Deleting file"
            Throw New Exception("_dlFileList_DeleteCommand", ex.InnerException)
        End Try

    End Sub

    ''' <summary>
    ''' Assigns an onclick event that prompts the user for deleting a file.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Sub _dlFileList_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles _dlFileList.ItemDataBound
        ' First, make sure we're NOT dealing with a Header or Footer row
        If e.Item.ItemType <> ListItemType.Header And
             e.Item.ItemType <> ListItemType.Footer Then
            'Now, reference the LinkButton control that the Delete ButtonColumn 
            'has been rendered to
            Dim deleteButton As Button = e.Item.FindControl("_lnkBtnDelete")

            'We can now add the onclick event handler
            deleteButton.Attributes("onclick") = "javascript:return " &
                       "confirm('Are you sure you want to delete file " &
                       DataBinder.Eval(e.Item.DataItem, "filename") & "?')"
        End If
    End Sub



    ' Function is used to create a global outagenumber variable.  This is used to allow for users to
    ' add attachments when first creating an outage record.
    Private Function GetGlobalJSVarOutage() As String
        Dim sb As New StringBuilder

        sb.Append("var gOutageNumber= ('")
        sb.Append(Request.QueryString("outageNumber"))
        sb.Append("');")
        sb.AppendLine()

        Return sb.ToString
    End Function

    Protected Sub _rblFileAttachments_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles _rblFileAttachments.PreRender
        For Each l As ListItem In Me._rblFileAttachments.Items
            If l.Value = "1" Then
                l.Attributes.Add("onclick", "MultiFileUpload.toggleAttachments('" & _pnlFileAttachments.ClientID & "','" & _pnlLinkAttachments.ClientID & "',0);")
            ElseIf l.Value = "2" Then
                l.Attributes.Add("onclick", "MultiFileUpload.toggleAttachments('" & _pnlFileAttachments.ClientID & "','" & _pnlLinkAttachments.ClientID & "',1);")
            End If
        Next
    End Sub
End Class
