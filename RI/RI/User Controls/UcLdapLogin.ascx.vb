Option Explicit On
Option Strict On

Imports RI

Partial Class RI_User_Controls_UcLdapLogin
    Inherits System.Web.UI.UserControl
    ''' <summary>
    ''' Attempts to log the current user out of the website
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub LogOut(ByVal sender As Object, ByVal e As System.EventArgs)
        'Session.Clear()
        'Session.Add("UserLoggedOut", "true")
        'mCurrentUserInfo = Nothing
        'SetWelcomeText(IPResources.GetResourceValue("PleaseLogin"), "")
        ''Me._mpeLogin.Show()
        'Me._logOut.Visible = False
        'Me._loginStatus.Text = IPResources.GetResourceValue("Login")
        ''Me._udpLogin.Update()
        'IP.Bids.SharedFunctions.ResponseRedirect("~/default.aspx?TargetURL=" & Page.AppRelativeVirtualPath)
        ''HttpContext.Current.ApplicationInstance.CompleteRequest()
    End Sub

    Public Sub SetCurrentUserLabel(ByVal currentUser As String, ByVal fullName As String)
        _lblWelcome.Text = GetDataBaseName() & ",  Current User: " & fullName & " (" & currentUser & ")"
    End Sub

    Public Function GetDataBaseName() As String
        If Application.Item("Servicename") IsNot Nothing Then
            Return CStr(Application.Item("Servicename"))
        Else
            Return "Unknown"
        End If
    End Function

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'Dim btn As HtmlInputButton = CType(Me._login.FindControl("_btnCancel2"), HtmlInputButton)
            'If btn IsNot Nothing Then
            _btnCancel2.Attributes.Add("onclick", "javascript:$get('" & Me._btnHideLogin.ClientID & "').click();return false;")

        If RI.SharedFunctions.IsAdminUser(My.User.Name) Then
            Password.Enabled = False
            PasswordRequired.Enabled = False
        End If

end sub

    Protected Sub LoginButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoginButton.Click
        Dim currentProfile As New CurrentUserProfile
        Dim currentUser As String = CurrentUserProfile.GetCurrentUser

        Try
            Session.Clear()
            Session.Remove("CurrentUser")
            Session.Add("CurrentUser", _ddlDomain.SelectedValue & "\" & UserName.Text)
            Session.Remove("clsSearch")
            currentProfile.AuthenticateUser(_ddlDomain.SelectedValue & "\" & UserName.Text, Password.Text)

            Try
                Response.Redirect(Page.AppRelativeVirtualPath, False)
                HttpContext.Current.ApplicationInstance.CompleteRequest()
                'Response.Redirect("~/ri/ViewUpdateSearch.aspx")
            Catch
                Server.ClearError()
            End Try

        Catch ex As Exception
            Me.FailureText.Text = ex.Message
            Session.Remove("CurrentUser")
            'Me.AuthenticateUser()
            Me._mpeLogin.Show()
        Finally

        End Try


    End Sub
End Class
