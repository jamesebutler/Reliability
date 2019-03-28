
Partial Class Outage_GanttViewerAnyChart
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.EnableViewState = False
        'Response.Clear()
        'Response.Buffer = True
        'Response.Charset = ""
        'Response.ContentType = "application/vnd.ms-excel"
        'Response.ContentEncoding = System.Text.Encoding.UTF7
        Me._OutageGanttChart.Visible = True
        Me._OutageGanttChart.GetGanttChart()
        'Response.Write(_OutageGanttChart.GanttChartData)
        'Response.Flush()
        'Response.Close()
        'Response.End()
    End Sub
End Class
