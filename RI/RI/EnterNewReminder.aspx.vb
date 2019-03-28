
Partial Class RI_EnterNewReminder
    Inherits RIBasePage

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Response.Redirect("../../ARESriindex.asp", True)
        Master.SetBanner("Reliability New Reminder")
    End Sub
End Class
