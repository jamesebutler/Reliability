
Partial Class RI_Help_ErrorPage
    Inherits RIBasePage
    Public Const ERRORCOUNT = "ErrorCount"
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner("Error Page")
        IncrementErrorCount()
    End Sub

    Private Sub IncrementErrorCount()
        Dim currentErrorCount = Application.Item(ERRORCOUNT)
        If currentErrorCount IsNot Nothing AndAlso IsNumeric(currentErrorCount) Then
            currentErrorCount += 1
            Application.Lock()
            Application.Item(ERRORCOUNT) = currentErrorCount
            Application.UnLock()
        Else
            currentErrorCount = 1
            Application.Lock()
            Application.Add(ERRORCOUNT, currentErrorCount)
            Application.UnLock()
        End If
        _lblErrorCount.Text = currentErrorCount
        If currentErrorCount >= 1000 then'1000 Then
            'Restart the website
            RI.SharedFunctions.InsertAuditRecord("RI", "UnloadAppDomain has been called to restart the application")
            System.Web.HttpRuntime.UnloadAppDomain()
        End If
    End Sub
End Class
