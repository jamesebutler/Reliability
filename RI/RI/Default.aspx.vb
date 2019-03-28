
Partial Class RI_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Server.Transfer("Reporting.aspx")
       
    End Sub
    
End Class
