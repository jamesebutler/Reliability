
Partial Class RI_User_Controls_ucTimePicker
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CreateTimePicker()
        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "TimePicker") Then Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "TimePicker", Page.ResolveClientUrl("~/ri/User Controls/Common/TimePicker.js"))
    End Sub

    Private Sub CreateTimePicker()
        Me._tblTimePickerMain.CssClass = "TimeSelector"
        'Me._tblTimePickerMain.Attributes.Add("TextBox", Me._txtTime.ClientID.ToString)
        Me._txtTime.ReadOnly = True
        Me._txtDate.ReadOnly = True
        Me._btnUpdate.OnClientClick = "updateTime('" & Me._txtTime.ClientID & "');return false;"
        Me._imgClock.Attributes.Add("onclick", "currentTime('" & Me._btnTime.ClientID & "');")
        Me._btnTime.OnClientClick = "updateTimeToCurrentTime('" & Me._txtTime.ClientID & "');return false;"
        Me._btnclear.onclientclick = "clearTime('" & Me._txtTime.ClientID & "');return false;"
        For j As Integer = 0 To 23 Step 4
            Dim tbr As New TableRow
            For i As Integer = 0 To 3
                Dim tblcell As New TableCell
                tblcell.Text = (i + j).ToString
                tbr.Cells.Add(tblcell)
                tbr.Cells(i).Attributes.Add("onmouseover", "javascript:changeHoursColor(this,'HoverColor')")
                tbr.Cells(i).Attributes.Add("onmouseout", "javascript:changeHoursColor(this,'TimeBackColor')")
                tbr.Cells(i).Attributes.Add("onclick", "javascript:selectHoursColor(this,'SelectedColor')")
                tbr.Cells(i).Style.Add("cursor", "hand")
                tbr.Cells(i).CssClass = "TimeBackColor"
                tbr.Cells(i).Attributes.Add("OriginalCSS", "TimeBackColor")
                tbr.Cells(i).Attributes.Add("TimeValue", (i + j).ToString)
            Next
            Me._tblTimePicker.Rows.Add(tbr)
        Next
        For j As Integer = 0 To 59 Step 10
            Dim tbr As New TableRow

            For i As Integer = 0 To 9
                Dim tblcell As New TableCell
                tblcell.Text = (i + j).ToString
                tbr.Cells.Add(tblcell)
                If (i + j) Mod 5 = 0 Then
                    tbr.Cells(i).Attributes.Add("onmouseover", "javascript:changeMinutesColor(this,'HoverColor')")
                    tbr.Cells(i).Attributes.Add("onmouseout", "javascript:changeMinutesColor(this,'TimePopularItemColor')")
                    tbr.Cells(i).Attributes.Add("onclick", "javascript:selectMinutesColor(this,'SelectedColor')")
                    tbr.Cells(i).Style.Add("cursor", "hand")
                    tbr.Cells(i).CssClass = "TimePopularItemColor"
                    tbr.Cells(i).Attributes.Add("OriginalCSS", "TimePopularItemColor")
                    tbr.Cells(i).Attributes.Add("TimeValue", (i + j).ToString)
                Else
                    tbr.Cells(i).Attributes.Add("onmouseover", "javascript:changeMinutesColor(this,'HoverColor')")
                    tbr.Cells(i).Attributes.Add("onmouseout", "javascript:changeMinutesColor(this,'TimeBackColor')")
                    tbr.Cells(i).Attributes.Add("onclick", "javascript:selectMinutesColor(this,'SelectedColor')")
                    tbr.Cells(i).Style.Add("cursor", "hand")
                    tbr.Cells(i).CssClass = "TimeBackColor"
                    tbr.Cells(i).Attributes.Add("OriginalCSS", "TimeBackColor")
                    tbr.Cells(i).Attributes.Add("TimeValue", (i + j).ToString)
                End If
            Next
            Me._tblTimePickerMinutes.Rows.Add(tbr)
        Next

    End Sub


End Class
