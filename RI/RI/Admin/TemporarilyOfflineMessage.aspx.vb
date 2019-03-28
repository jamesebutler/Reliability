
Partial Class RI_Admin_TemporarilyOfflineMessage
    Inherits RIBasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ProcessPage()
    End Sub
    Public Sub ProcessPage()
        Dim isOffline As Boolean = Convert.ToBoolean(Application.Item("IsOffline"))
        If isOffline = True Then
            Me._lblOfflineMessage.InnerHtml = RI.SharedFunctions.DataClean(Application.Item("OfflineMessage"), "The RI application is currently offline.")
            Master.ShowPopupMenu(Nothing, 0, True)
            Me._lblOfflineTitle.Text = "The RI application is currently offline."
            Master.SetBanner("Website is Offline")
        Else
            Server.Transfer("~/default.aspx")
        End If
    End Sub
End Class
