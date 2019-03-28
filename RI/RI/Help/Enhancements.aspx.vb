Imports RI
Partial Class RI_Help_Enhancements
    Inherits RIBasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Redirect("../../../enhancements.htm", True)
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner("Reliability Enhancements")
    End Sub
End Class
