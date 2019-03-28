
Partial Class RI_Admin_ManageOfflineStatus
    Inherits RIBasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        _lblEnterOfflineMessage.Text = "Enter the offline message"
        Master.SetBanner("Manage Offline Status")
        Me._btnUpdateStatus.Text = "Update"
        If Not Page.IsPostBack Then
            Dim isOffline As Boolean = Convert.ToBoolean(Application.Item("IsOffline"))
            If isOffline = True Then
                Me._cbWebsiteOffline.Checked = True
                Me._cbWebsiteOffline.Text = "Website is currently offline"
            Else
                Me._cbWebsiteOffline.Checked = False
                Me._cbWebsiteOffline.Text = "Website is currently online"
            End If
            Me._txtOfflineMessage.Text = RI.SharedFunctions.DataClean(Application.Item("OfflineMessage"), "This website is offline.")
        End If
    End Sub

    Protected Sub _btnUpdateStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnUpdateStatus.Click
        Application.Lock()
        Application.Item("IsOffline") = Me._cbWebsiteOffline.Checked = True
        Application.Item("OfflineMessage") = Me._txtOfflineMessage.Text
        Application.UnLock()
        'If Me._cbWebsiteOffline.Checked = True Then Server.Transfer("~/ri/Admin/TemporarilyOfflineMessage.aspx")
    End Sub
End Class
