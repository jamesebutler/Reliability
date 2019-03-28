
Partial Class PopupCalendar
    Inherits RIBasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim todayEnglishDate As String = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Now, "EN-US", "d")
        If Not Page.IsPostBack Then
            PopulateMonthYear()

            If Request.QueryString IsNot Nothing Then                
                Dim dateToSet As String = Request.QueryString("date")
                If dateToSet IsNot Nothing Then
                    If RI.SharedFunctions.isEnglishDate(dateToSet) Then
                        SetCalendarDate(dateToSet)
                    Else
                        SetCalendarDate(todayEnglishDate)
                    End If
                End If
            End If        
        End If
    End Sub
    Private Sub SetCalendarDate(ByVal dateValue As string)
        If RI.SharedFunctions.isEnglishDate(dateValue) Then
            Dim dtformatInfo As System.Globalization.DateTimeFormatInfo = System.Globalization.CultureInfo.CreateSpecificCulture("EN-US").DateTimeFormat
            Dim dtValue As DateTime = DateTime.Parse(dateValue, dtformatInfo)

            Dim yr As String = dtValue.Year
            Dim mt As String = dtValue.Month
            Dim dayvalue As String = dtValue.Day
            If yr.Length = 2 Then yr = "20" & yr.ToString
            'If mt.Length = 1 Then mt = "0" & mt.ToString
            If Me._ddlMonth.Items.FindByValue(mt) IsNot Nothing Then
                Me._ddlMonth.ClearSelection()
                Me._ddlMonth.Items.FindByValue(mt).Selected = True
            End If
            If Me._ddlYear.Items.FindByValue(yr) IsNot Nothing Then
                _ddlYear.ClearSelection()
                Me._ddlYear.Items.FindByValue(yr).Selected = True
            End If
            Me._txtDay.Text = dayvalue
            Me._calDate.TodaysDate = dtValue.Date  'Now.Date
            Me._calDate.SelectedDate = dtValue.Date
            '_Date.Value = _calDate.SelectedDate
            _DateValue.Value = IP.Bids.Localization.DateTime.GetLocalizedDateTime(_calDate.SelectedDate, "EN-US", "d")
            _Date.Value = _calDate.SelectedDate
        End If
    End Sub
    Private Sub PopulateMonthYear()
        Me._ddlMonth.Items.Clear()
        Me._ddlYear.Items.Clear()

        For i As Integer = 1999 To Year(Today) + 5
            Me._ddlYear.Items.Add(i.ToString)
        Next
        For i As Integer = 1 To 12
            Me._ddlMonth.Items.Add(New ListItem(MonthName(i), CStr(i)))
        Next
    End Sub

    Protected Sub _ddlMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlMonth.SelectedIndexChanged
        Dim curDate As Date = Me._calDate.SelectedDate
        Dim newDate As Date = DateAdd(DateInterval.Month, (_ddlMonth.SelectedValue - _calDate.SelectedDate.Month), curDate)
        CreateValidDate(newDate.Month, newDate.Day, newDate.Year)
    End Sub

    Protected Sub _ddlYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlYear.SelectedIndexChanged
        Dim curDate As Date = Me._calDate.SelectedDate
        Dim newDate As Date = DateAdd(DateInterval.Year, (_ddlYear.SelectedValue - _calDate.SelectedDate.Year), curDate)
        CreateValidDate(newDate.Month, newDate.Day, newDate.Year)
    End Sub

    Protected Sub _lnkNextMonth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _lnkNextMonth.Click
        Dim curMonth As Integer = Me._ddlMonth.SelectedValue
        Dim curYear As Integer = Me._ddlYear.SelectedValue
        Dim dt As Date = _calDate.TodaysDate

        If curMonth < 12 Then
            curMonth += 1
            dt = DateAdd(DateInterval.Month, 1, dt)
        Else
            curMonth = 1
            curYear += 1
            dt = New Date(curYear, curMonth, dt.Day)
            'dt = DateAdd(DateInterval.Year, 1, dt)
        End If
        curMonth = dt.Month
        curYear = dt.Year
        CreateValidDate(curMonth, dt.Day, curYear)
        'SetCalendarDate(curMonth & "/" & _calDate.TodaysDate.Day.ToString & "/" & curYear)
    End Sub

    Protected Sub _lnkPrevMonth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _lnkPrevMonth.Click
        Dim curMonth As Integer = Me._ddlMonth.SelectedValue
        Dim curYear As Integer = Me._ddlYear.SelectedValue
        Dim dt As Date = _calDate.TodaysDate

        If curMonth > 1 Then
            curMonth -= 1
            dt = DateAdd(DateInterval.Month, -1, dt)
        Else
            curMonth = 12
            curYear -= 1
            dt = New Date(curYear, curMonth, dt.Day)
            'dt = DateAdd(DateInterval.Year, -1, dt)
        End If
        curMonth = dt.Month
        curYear = dt.Year
        CreateValidDate(curMonth, dt.Day, curYear)
        'SetCalendarDate(curMonth & "/" & _calDate.TodaysDate.Day.ToString & "/" & curYear)
    End Sub
   
    Protected Sub _lnkPrevYear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _lnkPrevYear.Click
        Dim curMonth As Integer = Me._ddlMonth.SelectedValue
        Dim curYear As Integer = Me._ddlYear.SelectedValue
        curYear -= 1
        CreateValidDate(curMonth, _calDate.TodaysDate.Day, curYear)
        'SetCalendarDate(curMonth & "/" & _calDate.TodaysDate.Day.ToString & "/" & curYear)

    End Sub

    Protected Sub _lnkNextYear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _lnkNextYear.Click
        Dim curMonth As Integer = Me._ddlMonth.SelectedValue
        Dim curYear As Integer = Me._ddlYear.SelectedValue
        curYear += 1
        CreateValidDate(curMonth, _calDate.TodaysDate.Day, curYear)
        'SetCalendarDate(curMonth & "/" & _calDate.TodaysDate.Day.ToString & "/" & curYear)
    End Sub

    Protected Sub _btnToday_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnToday.Click
        Dim todayEnglishDate As String = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Now, "EN-US", "d")
        SetCalendarDate(todayEnglishDate)

        ' ReturnValue()
    End Sub
    Private Function CreateValidDate(ByVal MonthValue As Integer, ByVal dayValue As Integer, ByVal yearValue As Integer) As Date
        'Dim newDate As String = MonthValue & "/" & dayValue & "/" & yearValue
        'If Not IsDate(newDate) Then
        '    newDate = MonthValue & "/" & 1 & "/" & yearValue
        '    If Not IsDate(newDate) Then
        '        newDate = Now.Date
        '    End If
        'End If
        'SetCalendarDate(newDate)
        Dim newDate As Date
        Try
            newDate = New DateTime(yearValue, MonthValue, dayValue)

            If Not IsDate(newDate) Then
                'newDate = MonthValue & "/" & 1 & "/" & yearValue
                newDate = New DateTime(yearValue, MonthValue, 1)
                If Not IsDate(newDate) Then
                    newDate = Now.Date
                End If
            End If
            SetCalendarDate(IP.Bids.Localization.DateTime.GetLocalizedDateTime(newDate, "EN-US"))
        Catch
            Throw
        End Try
        Return newDate
    End Function
   
    Private Sub ReturnValue()
        Dim sb As New StringBuilder
        If Request.QueryString IsNot Nothing Then
            Dim parentControl As String = Request.QueryString("Parent")
            sb.Append("<script language='javascript'>window.opener.document.forms(0).")
            sb.Append(parentControl)
            sb.Append(".value='")
            sb.Append(Me._calDate.SelectedDate.ToShortDateString)
            sb.Append("';self.close()</script>")
            If Not Page.ClientScript.IsStartupScriptRegistered(Page.GetType, "SetValue") Then
                Page.ClientScript.RegisterStartupScript(Page.GetType, "SetValue", sb.ToString)
            End If
            'ScriptManager.RegisterStartupScript(Me.Page, Page.GetType, "SetValue", sb.ToString, False)
        End If


    End Sub


    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        'If RI.SharedFunctions.CausedPostBack(Me._calDate.ID) Then
        '    ReturnValue()
        'End If
    End Sub

    
    Protected Sub _calDate_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _calDate.SelectionChanged
        Dim curDate As Date = Me._calDate.SelectedDate
        SetCalendarDate(IP.Bids.Localization.DateTime.GetLocalizedDateTime(curDate, "EN-US"))
    End Sub
End Class
