
Partial Class Outage_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        RI.SharedFunctions.ResponseRedirect("~/Outage/Outage.aspx")
    End Sub
End Class
