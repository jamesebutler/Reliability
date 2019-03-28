
Partial Class RI_User_Controls_Common_ucDateRangeSelector
    Inherits System.Web.UI.UserControl
    Private Enum DateType
        StartDate = 0
        EndDate = 1
    End Enum
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Page.ClientScript.RegisterClientScriptInclude("yahoo-dom-event", Page.ResolveClientUrl("~/ri/user controls/common/yahoo-dom-event.js?_yuiversion=2.3.1"))
        'Page.ClientScript.RegisterClientScriptInclude("yahoo-calendar", Page.ResolveClientUrl("~/ri/user controls/common/calendar.js?_yuiversion=2.3.1"))
        Dim todayEnglishDate As String = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Now, "EN-US")
        If Not Page.IsPostBack Then
            PopulateMonthYear()

            If Request.QueryString("StartDate") IsNot Nothing And Request.QueryString("EndDate") IsNot Nothing Then
                Dim dateToSet As String = Request.QueryString("StartDate")
                If dateToSet IsNot Nothing Then
                    If RI.SharedFunctions.isEnglishDate(dateToSet) Then
                        SetCalendarDate(dateToSet, DateType.StartDate)
                    Else
                        SetCalendarDate(todayEnglishDate, DateType.StartDate)
                    End If
                End If
                dateToSet = Request.QueryString("EndDate")
                If dateToSet IsNot Nothing Then
                    If RI.SharedFunctions.isEnglishDate(dateToSet) Then
                        SetCalendarDate(dateToSet, DateType.EndDate)
                    Else
                        SetCalendarDate(todayEnglishDate, DateType.EndDate)
                    End If
                End If
            Else
                SetCalendarDate(todayEnglishDate, DateType.StartDate)
                SetCalendarDate(todayEnglishDate, DateType.EndDate)

            End If
        End If
    End Sub
    Private Sub SetCalendarDate(ByVal dateValue As String, ByVal dt As DateType)
        If RI.SharedFunctions.isEnglishDate(dateValue) Then
            'Dim dtValue As Date = CDate(IP.Bids.Localization.DateTime.GetLocalizedDateTime(dateValue, Threading.Thread.CurrentThread.CurrentCulture.Name))
            Dim dtformatInfo As System.Globalization.DateTimeFormatInfo = System.Globalization.CultureInfo.CreateSpecificCulture("EN-US").DateTimeFormat
            Dim dtValue As DateTime = DateTime.Parse(dateValue, dtformatInfo)
            Dim yr As String = dtValue.Year  'Year(dateValue)
            Dim mt As String = dtValue.Month 'Month(dateValue)
            Dim dayvalue As String = dtValue.Day 'Day(dateValue)
            If yr.Length = 2 Then yr = "20" & yr.ToString
            Select Case dt
                Case DateType.StartDate
                    If Me._ddlStartMonth.Items.FindByValue(mt) IsNot Nothing Then
                        Me._ddlStartMonth.ClearSelection()
                        Me._ddlStartMonth.Items.FindByValue(mt).Selected = True
                    End If
                    If _ddlStartYear.Items.FindByValue(yr) IsNot Nothing Then
                        _ddlStartYear.ClearSelection()
                        _ddlStartYear.Items.FindByValue(yr).Selected = True
                    End If
                    _txtStartDay.Text = dayvalue
                    Me._calStartDate.TodaysDate = dtValue.Date 'dateValue 'Now.Date
                    Me._calStartDate.SelectedDate = dtValue.Date 'dateValue
                Case DateType.EndDate
                    If Me._ddlEndMonth.Items.FindByValue(mt) IsNot Nothing Then
                        Me._ddlEndMonth.ClearSelection()
                        Me._ddlEndMonth.Items.FindByValue(mt).Selected = True
                    End If
                    If _ddlEndYear.Items.FindByValue(yr) IsNot Nothing Then
                        _ddlEndYear.ClearSelection()
                        _ddlEndYear.Items.FindByValue(yr).Selected = True
                    End If
                    _txtEndDay.Text = dayvalue
                    Me._calEndDate.TodaysDate = dtValue.Date 'dateValue 'Now.Date
                    Me._calEndDate.SelectedDate = dtValue.Date 'dateValue
            End Select
            If _calStartDate.SelectedDate > _calEndDate.SelectedDate Then
                SetCalendarDate(IP.Bids.Localization.DateTime.GetLocalizedDateTime(_calStartDate.SelectedDate, "EN-US"), DateType.EndDate)
            End If
        End If

        _endDate.Value = _calEndDate.SelectedDate
        _startDate.Value = _calStartDate.SelectedDate
        _endDateValue.Value = IP.Bids.Localization.DateTime.GetLocalizedDateTime(_calEndDate.SelectedDate, "EN-US", "d") '_calEndDate.SelectedDate
        _startDateValue.Value = IP.Bids.Localization.DateTime.GetLocalizedDateTime(_calStartDate.SelectedDate, "EN-US", "d")
    End Sub
    Private Sub PopulateMonthYear()
        Me._ddlStartMonth.Items.Clear()
        Me._ddlStartYear.Items.Clear()
        Me._ddlEndMonth.Items.Clear()
        Me._ddlEndYear.Items.Clear()

        For i As Integer = 1999 To Year(Today) + 5
            Me._ddlStartYear.Items.Add(i.ToString)
            Me._ddlEndYear.Items.Add(i.ToString)
        Next
        For i As Integer = 1 To 12
            Me._ddlStartMonth.Items.Add(New ListItem(MonthName(i), CStr(i)))
            Me._ddlEndMonth.Items.Add(New ListItem(MonthName(i), CStr(i)))
        Next
    End Sub


    'Protected Sub _btnToday_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnToday.Click
    '    SetCalendarDate(Now.Date)
    '    ReturnValue()
    'End Sub
    Private Function CreateValidDate(ByVal MonthValue As Integer, ByVal dayValue As Integer, ByVal yearValue As Integer, ByVal dt As DateType) As Date
        'Dim newDate As String = MonthValue & "/" & dayValue & "/" & yearValue
        Dim newDate As Date
        Try
            newDate = New DateTime(yearValue, MonthValue, dayValue)
        Catch ex As Exception
            newDate = Now.Date
        End Try
        If Not IsDate(newDate) Then
            'newDate = MonthValue & "/" & 1 & "/" & yearValue
            newDate = New DateTime(yearValue, MonthValue, 1)
            If Not IsDate(newDate) Then
                newDate = Now.Date
            End If
        End If
        SetCalendarDate(IP.Bids.Localization.DateTime.GetLocalizedDateTime(newDate, "EN-US"), dt)
        Return newDate
    End Function

    'Private Sub ReturnValue()
    '    Dim sb As New StringBuilder
    '    If Request.QueryString IsNot Nothing Then
    '        Dim parentControl As String = Request.QueryString("Parent")
    '        sb.Append("<script language='javascript'>window.opener.document.forms(0).")
    '        sb.Append(parentControl)
    '        sb.Append(".value='")
    '        sb.Append(Me._calDate.SelectedDate.ToShortDateString)
    '        sb.Append("';self.close()</script>")
    '        If Not Page.ClientScript.IsStartupScriptRegistered(Page.GetType, "SetValue") Then
    '            Page.ClientScript.RegisterStartupScript(Page.GetType, "SetValue", sb.ToString)
    '        End If
    '        'ScriptManager.RegisterStartupScript(Me.Page, Page.GetType, "SetValue", sb.ToString, False)
    '    End If


    'End Sub


    'Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
    '    If RI.SharedFunctions.CausedPostBack(Me._calDate.ID) Then
    '        ReturnValue()
    '    End If
    'End Sub


    Protected Sub _ddlStartMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlStartMonth.SelectedIndexChanged
        Dim curDate As Date = Me._calStartDate.SelectedDate
        Dim newDate As Date = DateAdd(DateInterval.Month, (_ddlStartMonth.SelectedValue - _calStartDate.SelectedDate.Month), _calStartDate.SelectedDate)
        CreateValidDate(newDate.Month, newDate.Day, newDate.Year, DateType.StartDate)
    End Sub

    Protected Sub _ddlEndMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlEndMonth.SelectedIndexChanged
        Dim curDate As Date = Me._calEndDate.SelectedDate
        Dim newDate As Date = DateAdd(DateInterval.Month, (_ddlEndMonth.SelectedValue - _calEndDate.SelectedDate.Month), _calEndDate.SelectedDate)
        CreateValidDate(newDate.Month, newDate.Day, newDate.Year, DateType.EndDate)
    End Sub

    Protected Sub _ddlStartYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlStartYear.SelectedIndexChanged
        Dim curDate As Date = Me._calStartDate.SelectedDate
        Dim newDate As Date = DateAdd(DateInterval.Year, (_ddlStartYear.SelectedValue - _calStartDate.SelectedDate.Year), _calStartDate.SelectedDate)
        CreateValidDate(newDate.Month, newDate.Day, newDate.Year, DateType.StartDate)
    End Sub
    Protected Sub _ddlendYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlEndYear.SelectedIndexChanged
        Dim curDate As Date = Me._calEndDate.SelectedDate
        Dim newDate As Date = DateAdd(DateInterval.Year, (_ddlEndYear.SelectedValue - _calEndDate.SelectedDate.Year), _calEndDate.SelectedDate)
        CreateValidDate(newDate.Month, newDate.Day, newDate.Year, DateType.EndDate)
    End Sub
    
    Protected Sub _lnkStartNextMonth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _lnkStartNextMonth.Click
        Dim curMonth As Integer = Me._ddlStartMonth.SelectedValue
        Dim curYear As Integer = Me._ddlStartYear.SelectedValue
        Dim dt As Date = _calStartDate.TodaysDate
        If curMonth < 12 Then
            curMonth += 1
            dt = DateAdd(DateInterval.Month, 1, dt)
        Else
            curMonth = 1
            curYear += 1
            dt = New Date(curYear, curMonth, dt.Day)
        End If
        curYear = dt.Year
        curMonth = dt.Month
        CreateValidDate(curMonth, dt.Day, curYear, DateType.StartDate)
    End Sub

    Protected Sub _lnkStartNextYear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _lnkStartNextYear.Click
        Dim curMonth As Integer = Me._ddlStartMonth.SelectedValue
        Dim curYear As Integer = Me._ddlStartYear.SelectedValue
        curYear += 1
        CreateValidDate(curMonth, _calStartDate.TodaysDate.Day, curYear, DateType.StartDate)
    End Sub

    Protected Sub _lnkStartPrevMonth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _lnkStartPrevMonth.Click
        Dim curMonth As Integer = Me._ddlStartMonth.SelectedValue
        Dim curYear As Integer = Me._ddlStartYear.SelectedValue
        Dim dt As Date = _calStartDate.TodaysDate
        If curMonth > 1 Then           
            dt = DateAdd(DateInterval.Month, -1, dt)
        Else
            curMonth = 12
            curYear -= 1
            dt = New Date(curYear, curMonth, dt.Day)
        End If
        curYear = dt.Year
        curMonth = dt.Month
        CreateValidDate(curMonth, dt.Day, curYear, DateType.StartDate)
    End Sub

    Protected Sub _lnkStartPrevYear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _lnkStartPrevYear.Click
        Dim curMonth As Integer = Me._ddlStartMonth.SelectedValue
        Dim curYear As Integer = Me._ddlStartYear.SelectedValue
        curYear -= 1
        CreateValidDate(curMonth, _calStartDate.TodaysDate.Day, curYear, DateType.StartDate)
    End Sub

    Protected Sub _lnkEndNextMonth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _lnkEndNextMonth.Click
        Dim curMonth As Integer = Me._ddlEndMonth.SelectedValue
        Dim curYear As Integer = Me._ddlEndYear.SelectedValue
        Dim dt As Date = _calEndDate.TodaysDate
        If curMonth < 12 Then
            dt = DateAdd(DateInterval.Month, 1, dt)
        Else
            curMonth = 1
            curYear += 1
            dt = New Date(curYear, curMonth, dt.Day)
        End If
        curYear = dt.Year
        curMonth = dt.Month
        CreateValidDate(curMonth, dt.Day, curYear, DateType.EndDate)
    End Sub

    Protected Sub _lnkEndNextYear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _lnkEndNextYear.Click
        Dim curMonth As Integer = Me._ddlEndMonth.SelectedValue
        Dim curYear As Integer = Me._ddlEndYear.SelectedValue
        curYear += 1
        CreateValidDate(curMonth, _calEndDate.TodaysDate.Day, curYear, DateType.EndDate)
    End Sub

    Protected Sub _lnkEndPrevMonth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _lnkEndPrevMonth.Click
        Dim curMonth As Integer = Me._ddlEndMonth.SelectedValue
        Dim curYear As Integer = Me._ddlEndYear.SelectedValue
        Dim dt As Date = _calEndDate.TodaysDate
        If curMonth > 1 Then
            dt = DateAdd(DateInterval.Month, -1, dt)
        Else
            curMonth = 12
            curYear -= 1
            dt = New Date(curYear, curMonth, dt.Day)
        End If
        curYear = dt.Year
        curMonth = dt.Month
        CreateValidDate(curMonth, dt.Day, curYear, DateType.EndDate)
    End Sub

    Protected Sub _lnkEndPrevYear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _lnkEndPrevYear.Click
        Dim curMonth As Integer = Me._ddlEndMonth.SelectedValue
        Dim curYear As Integer = Me._ddlEndYear.SelectedValue
        curYear -= 1
        CreateValidDate(curMonth, _calEndDate.TodaysDate.Day, curYear, DateType.EndDate)
    End Sub

    Protected Sub _calStartDate_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _calStartDate.SelectionChanged
        Dim curDate As Date = Me._calStartDate.SelectedDate
        CreateValidDate(curDate.Month, curDate.Day, curDate.Year, DateType.StartDate)
    End Sub

    Protected Sub _calEndDate_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _calEndDate.SelectionChanged
        Dim curDate As Date = Me._calEndDate.SelectedDate
        CreateValidDate(curDate.Month, curDate.Day, curDate.Year, DateType.EndDate)
    End Sub

    Protected Sub _btnStartToday_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnStartToday.Click
        Me.SetCalendarDate(IP.Bids.Localization.DateTime.GetLocalizedDateTime(Now.Date, "EN-US"), DateType.StartDate)
    End Sub

    Protected Sub _btnEndToday_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnEndToday.Click
        Me.SetCalendarDate(IP.Bids.Localization.DateTime.GetLocalizedDateTime(Now.Date, "EN-US"), DateType.EndDate)
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
       
    End Sub
End Class
