<%@ Application Language="VB" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.IO.Compression" %>

<script runat="server">
 
	Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
		Dim url As String = Request.Url.PathAndQuery
  
        Try
            Dim requestedUrl As String = System.IO.Path.GetFileName(Request.PhysicalPath)
            WebsiteIsOffline(requestedUrl)
            If url.ToUpper.Contains("RIINDEX.ASP") Then
                HttpContext.Current.RewritePath("~/RI/ENTERNEWRI.ASPX")
            ElseIf url.ToUpper.Contains("RIVIEW.ASP") Then
                HttpContext.Current.RewritePath("~/RI/VIEWUPDATESEARCH.ASPX")
            ElseIf url.ToUpper.Contains("NEWRIINDEX.ASP") Then
                HttpContext.Current.RewritePath("~/RI/ENTERNEWRI.ASPX")
            ElseIf url.ToUpper.Contains("RCFA REPORT SELECTION.ASP") Then
                HttpContext.Current.RewritePath("~/RI/REPORTING.ASPX")
            ElseIf url.ToUpper.Contains("ARESRIINDEX.ASP") Then
                HttpContext.Current.RewritePath("~/RI/ENTERNEWRI.ASPX")
            End If
		
            Dim app As HttpApplication = DirectCast(sender, HttpApplication)
            Dim acceptEncoding As String = app.Request.Headers("Accept-Encoding")
            Dim prevUncompressedStream As IO.Stream = app.Response.Filter

            If acceptEncoding Is Nothing OrElse acceptEncoding.Length = 0 Then
                Exit Sub
            End If

            acceptEncoding = acceptEncoding.ToLower()
            Dim ajaxHeaders As String = app.Request.Headers.[Get]("X-MicrosoftAjax")
            Dim allowCompression As Boolean = False
            If app.Request.RawUrl.Contains(".aspx") And Not app.Request.RawUrl.Contains("CorrectiveActionItems.aspx") And Not app.Request.RawUrl.Contains("Excel.aspx") Then
                allowCompression = True
                'ElseIf app.Request.RawUrl.Contains(".css") Then
                'allowCompression = True
                'ElseIf app.Request.RawUrl.Contains(".gif") Then
                'allowCompression = True
                'ElseIf app.Request.RawUrl.Contains(".jpg") Then
                'allowCompression = True
                'ElseIf app.Request.RawUrl.Contains(".aspx") Then
                'ElseIf app.Request.RawUrl.Contains(".aspx") Then
                'ElseIf app.Request.RawUrl.Contains(".aspx") Then
			
            End If
            'allowCompression = False
            If String.IsNullOrEmpty(ajaxHeaders) And allowCompression Then
		
                If acceptEncoding.Contains("gzip") Then
                    ' gzip 
                    ' safety measure starts 
                    Dim compress As String = app.Request.QueryString("compress")
                    If compress IsNot Nothing AndAlso compress = "false" Then
                        Exit Sub
                    End If
                    ' safety measure ends 
                    app.Response.Filter = New IO.Compression.GZipStream(prevUncompressedStream, CompressionMode.Compress)
                    app.Response.AppendHeader("Content-Encoding", "gzip")
                ElseIf acceptEncoding.Contains("deflate") Then
                    ' defalte 
                    ' safety measure starts 
                    Dim compress As String = app.Request.QueryString("compress")
                    If compress IsNot Nothing AndAlso compress = "false" Then
                        Exit Sub
                    End If
                    ' safety measure ends 
                    app.Response.Filter = New IO.Compression.DeflateStream(prevUncompressedStream, CompressionMode.Compress)
                    app.Response.AppendHeader("Content-Encoding", "deflate")
                End If
            End If
            'InitCulture()
        Catch ex As Exception
            Server.ClearError()
        End Try
	
	End Sub
	
    Sub WebsiteIsOffline(ByVal requestedUrl As String)
        
        Dim offline As Boolean = Convert.ToBoolean(Application("IsOffline"))
        If Not requestedUrl.ToLower.Contains(".aspx") Then Exit Sub
        If offline Then
            ' We are in offline mode...
            ' See if the user is requesting one of the "safe" pages
           
            Dim safePages As String() = {"ManageOfflineStatus.aspx", "TemporarilyOfflineMessage.aspx", "RIBanner.aspx", "RIPopupBanner.aspx", "KeepSessionAlive.aspx"}

            For Each safePage As String In safePages
                If String.Compare(requestedUrl, safePage, True) = 0 Then
                    Exit Sub ' match found
                End If
            Next

            ' If we reach here, we need to send the user to TemporarilyOfflineMessage.aspx
            Response.Redirect("~/ri/Admin/TemporarilyOfflineMessage.aspx", True)
            'Server.Transfer("~/ri/Admin/TemporarilyOfflineMessage.aspx", False)
        End If

    End Sub
	Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
		' Code that runs on application startup
		Dim ServiceName As String = "Database ({0})"
		ServiceName = String.Format(ServiceName, RI.SharedFunctions.GetDatabaseName())
		Application.Add("ServiceName", ServiceName)
        Application("OfflineMessage") = "This website is offline."
        Application("IsOffline") = False
	End Sub
    
	Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
		' Code that runs on application shutdown
	End Sub
        
	Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
		' Code that runs when an unhandled error occurs
		'Log Errors Here
		RI.SharedFunctions.HandleError()
	End Sub

	Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
		' Code that runs when a new session is started
		Session.Timeout = 180
		Response.AddHeader("refresh", CStr(CInt(Session.Timeout *60) + 5) & ";url=" & Request.Url.AbsolutePath)
        
	End Sub

	Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
		' Code that runs when a session ends. 
		' Note: The Session_End event is raised only when the sessionstate mode
		' is set to InProc in the Web.config file. If session mode is set to StateServer 
		' or SQLServer, the event is not raised.
        'RI.SharedFunctions.InsertAuditRecord("Session_End",Request.Url.AbsolutePath)
	End Sub
    Private Sub InitCulture()
        Dim userprofile As RI.CurrentUserProfile = Nothing
        Dim defaultCulture As String = ""
        Dim selectedCulture As String = ""
        Dim cultureBeingUsed As String = ""
        Dim allKeys() As String = Request.Form.AllKeys
        Dim cultureIsSet As Boolean

        Try

            'Look to see if the user has selected a different language
            For i As Integer = 0 To allKeys.Length - 1
                If allKeys(i) IsNot Nothing Then
                    If allKeys(i).Contains("_rblLanguages") Then
                        If Request.Form(allKeys(i).ToString) IsNot Nothing And Request.Form(allKeys(i).ToString).Length > 0 Then
                            selectedCulture = Request.Form(allKeys(i).ToString)
                        End If
                        Exit For
                    End If
                End If
            Next

            If selectedCulture.Length > 0 Then
                cultureBeingUsed = RI.SharedFunctions.InitCulture(selectedCulture)
                If cultureBeingUsed <> "Auto" And cultureBeingUsed.Length > 0 Then
                    cultureIsSet = True
                Else
                    cultureIsSet = False
                End If
            End If

            If cultureIsSet = False Then
                'Populate the current culture from the User Profile table
                userprofile = RI.SharedFunctions.GetUserProfile
                If userprofile IsNot Nothing Then defaultCulture = userprofile.DefaultLanguage
                userprofile = Nothing

                cultureBeingUsed = RI.SharedFunctions.InitCulture(defaultCulture)
                If cultureBeingUsed <> "Auto" And cultureBeingUsed.Length > 0 Then
                    cultureIsSet = True
                Else
                    cultureIsSet = False
                End If
            End If

            If cultureIsSet = False Then
                '     Me.UICulture = "Auto"
                '    Me.Culture = "Auto"
            End If
        Catch
            Throw
        Finally
            userprofile = Nothing
        End Try
    End Sub
</script>