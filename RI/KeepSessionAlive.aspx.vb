
Partial Class KeepSessionAlive
    Inherits System.Web.UI.Page

    Protected Sub _tmrKeepAlive_Tick1(ByVal sender As Object, ByVal e As System.EventArgs) Handles _tmrKeepAlive.Tick

    End Sub

    Protected Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
        Me._tmrKeepAlive.Enabled = False      
        Server.ClearError()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If HttpContext.Current.Request.ServerVariables("SERVER_NAME").ToLower.Contains("localhost") Then _tmrKeepAlive.Enabled = False
        Me._lblTime.Text = "Page Last Refreshed: " & FormatDateTime(Now, DateFormat.GeneralDate)
        If Session.Item("LastRefresh") IsNot Nothing Then
            Session.Remove("LastRefresh")            
        End If
        Session.Add("LastRefresh", Now)
    End Sub
End Class
