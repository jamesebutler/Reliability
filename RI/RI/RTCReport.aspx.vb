
Partial Class RTCReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Master.DisplayBanner = False
        Master.MasterPageWidth = "100%"
        Dim mnu As New MenuItemCollection
        mnu.Add(New MenuItem("Configuration", Nothing, Nothing, "javascript:DisplayConfig('" & Me._btnConfig.ClientID & "');"))
        mnu.Add(New MenuItem("Help", Nothing, Nothing, "javascript:ShowMyModalPopup();"))
        Master.ShowPopupMenu(mnu, 0, True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load      
        If Me._rblInterval.SelectedItem IsNot Nothing Then
            Me._timer.Interval = Me._rblInterval.SelectedValue
        Else
            Me._timer.Interval = 60000 '60 seconds = 60,000
            Me._rblInterval.SelectedValue = 60000

        End If

        RI.SharedFunctions.DisablePageCache(Response)

        'Master.Page.Header.Attributes.Add("http-equiv", "refresh")
        'Master.Page.Header.Attributes.Add("content", "300")
        Dim sb As String = "http://ridev/CEReporting/frmCrystalReport.aspx?Param=&Report=ActionItems&ReportTitle=Action%20Items&Division=PandC%20Papers&Site=Courtland&BusUnit=All&Area=All&LineSystem=All&BusType=PM&LineBreak=All&InactiveFlag=N&Person=All&PersonName=All&Status=Incomplete&IncidentType=All&Recordable=No&RCFA=No&Chronic=No&TopChronic=No&Safety=No&Quality=No&AutoRemind=No&EstDueDate=All&Overdue=No&Next%207%20Days=No&Next%2014%20Days=No&Next%2030%20Days=No&StartDate=1/1/2007&EndDate=7/6/2007"
        Me._ifrReport.Attributes.Add("src", sb) '"http://ridev/CEReporting/frmCrystalReport.aspx?Param=" & Replace(sb.ToString, " ", "%20"))
    End Sub
End Class
