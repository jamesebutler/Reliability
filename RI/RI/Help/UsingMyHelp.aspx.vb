Imports RI
Partial Class RI_UsingHelp
    Inherits RIBasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("UsingMyHelpHeader", True))
        'Master.SetBanner("Reliability Using My-Help")
    End Sub
End Class
