Imports RI
Partial Class MOC_Report
    Inherits RIBasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim r As ReportParameters = CType(Session("CrystalReport"), ReportParameters)
        Dim sb As New StringBuilder
        Dim ReportPage As String = String.Empty
        Try
            If r IsNot Nothing Then
                For i As Integer = 0 To r.Count - 1
                    sb.Append("&" & r.Item(i).name & "=" & Server.UrlEncode(r.Item(i).value))
                Next
                If r.Contains("ReportPage") = True Then
                    ReportPage = r.Item(r.IndexOf("ReportPage")).value
                End If

                'CAC 09/09/2013 Updated for BI4 upgrade
                'If ReportPage.Length = 0 Then ReportPage = "frmCrystalReport.aspx"
                If ReportPage.Length = 0 Then ReportPage = "CrystalReportDisplay.aspx"
                If Request.UserHostAddress = "127.0.0.1" Or Request.UserHostAddress = "http://s29edev13/riajax" Then
                    Response.Redirect("http://ridev/CEReporting/" & ReportPage & "?Param=" & Replace(sb.ToString, " ", "%20"))
                Else
                    'Response.Write("Request.UserHostAddress=" & Request.UserHostAddress)
                    'Server.Transfer("../CEReporting/frmCrystalReport.aspx?Param=" & Replace(sb.ToString, " ", "%20"))
                    Response.Redirect("http://gpimv.graphicpkg.com/cereporting/" & ReportPage & "?Param=" & Replace(sb.ToString, " ", "%20"), True)
                End If
            End If
        Catch ex As Threading.ThreadAbortException
            Server.ClearError()
        Catch ex As Exception
            Throw
            _divReport.InnerHtml = ex.Message.ToString
        End Try
    End Sub
End Class
