
Partial Class RI_ReportRedirect
    Inherits RIBasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Redirect("ReportSelector.aspx", False)
    End Sub
End Class
