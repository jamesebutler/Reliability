
Partial Class PageNotFound
    Inherits RIBasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Master.SetBanner("Page Not Found")
        If Request("err") IsNot Nothing Then
            Me._divPageNotFound.InnerHtml = "<h2>" & Request("err") & "</h2>"
        End If
    End Sub
End Class
