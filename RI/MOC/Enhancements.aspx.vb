Imports Devart.Data.Oracle

Partial Class MOC_MOCEnhancements
    Inherits RIBasePage


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.ShowMOCMenu()
        Master.SetBanner("MOC Enhancements")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
End Class
