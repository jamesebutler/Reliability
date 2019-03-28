Option Explicit On
Partial Class MOC_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Server.Transfer("~/MOC/MyMocs.aspx")
    End Sub

  
End Class
