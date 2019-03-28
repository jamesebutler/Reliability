Option Explicit On
Option Strict On

Partial Class _ucCalendar
    Inherits System.Web.UI.UserControl

    Public Event CalendarUpdated()

    Public Property ShowCalendar() As Boolean
        Get
            Return CBool(Session("ShowCalendar"))
        End Get
        Set(ByVal value As Boolean)
            Session("ShowCalendar") = value
            Me.Visible = value
            'If value = True Then
            SetupCalendar()
            'End If

        End Set
    End Property
    Private mDefaultStartDate As String '= Today.ToShortDateString
    Public WriteOnly Property DefaultStartDate() As String
        Set(ByVal value As String)
            mDefaultStartDate = value
        End Set
    End Property
    Private mDefaultEndDate As String '= Today.ToShortDateString
    Public WriteOnly Property DefaultEndDate() As String
        Set(ByVal value As String)
            mDefaultEndDate = value
        End Set
    End Property
    Public Property StartDate() As String
        Get
            Dim _StartDate As String = String.Empty
            If Me._txtStartDate.Text.Length = 0 Then
                If mDefaultStartDate Is Nothing Then
                    _StartDate = Today.ToShortDateString
                Else
                    _StartDate = mDefaultStartDate
                End If
                _txtStartDate.Text = _StartDate
                Me.SetStartMonthYear()
            Else
                _StartDate = _txtStartDate.Text
            End If
            Return _StartDate
        End Get
        Set(ByVal value As String)
            _txtStartDate.Text = value
            Me.SetStartMonthYear()
            'If IsDate(value) And Not Request.Form("__EventTarget").Contains("ddlMonthStartDate") Then
            '    If Me._ddlMonthStartDate.Items.FindByValue(CStr(Month(CDate(value)))) IsNot Nothing Then
            '        'Me._ddlMonthStartDate.Items.FindByValue(Month(StartDate)).Selected = True
            '        'Me._ddlMonthStartDate.SelectedValue = Month(value)
            '        Me._ddlMonthStartDate.ClearSelection()
            '        Me._ddlMonthStartDate.Items.FindByValue(CStr(Month(CDate(value)))).Selected = True
            '    End If
            '    If Me._ddlYearStartDate.Items.FindByValue(CStr(Year(CDate(value)))) IsNot Nothing Then
            '        'Me._ddlYearStartDate.Items.FindByValue(Year(StartDate)).Selected = True
            '        Me._ddlYearStartDate.ClearSelection()
            '        Me._ddlYearStartDate.Items.FindByValue(CStr(Year(CDate(value)))).Selected = True
            '    End If
            'End If
        End Set
    End Property
    Public Property EndDate() As String
        Get
            Dim _EndDate As String = String.Empty
            If Me._txtEndDate.Text.Length = 0 Then
                If mDefaultEndDate Is Nothing Then
                    _EndDate = Today.ToShortDateString
                Else
                    _EndDate = mDefaultEndDate
                End If

                _txtEndDate.Text = _EndDate
                Me.SetEndMonthYear()
            Else
                _EndDate = _txtEndDate.Text
            End If
            Return _EndDate
        End Get
        Set(ByVal value As String)
            _txtEndDate.Text = value
            Me.SetEndMonthYear()
            'If IsDate(value) And Not Request.Form("__EventTarget").Contains("ddlMonthEndDate") Then
            '    If Me._ddlMonthEndDate.Items.FindByValue(CStr(Month(CDate(value)))) IsNot Nothing Then
            '        'Me._ddlMonthStartDate.Items.FindByValue(Month(StartDate)).Selected = True
            '        'Me._ddlMonthEndDate.SelectedValue = Month(value)
            '        Me._ddlMonthEndDate.ClearSelection()
            '        Me._ddlMonthEndDate.Items.FindByValue(CStr(Month(CDate(value)))).Selected = True
            '    End If
            '    If Me._ddlYearEndDate.Items.FindByValue(CStr(Year(CDate(value)))) IsNot Nothing Then
            '        'Me._ddlYearStartDate.Items.FindByValue(Year(StartDate)).Selected = True
            '        'Me._ddlYearEndDate.SelectedValue = Year(value)
            '        Me._ddlYearEndDate.ClearSelection()
            '        Me._ddlYearEndDate.Items.FindByValue(CStr(Year(CDate(value)))).Selected = True
            '    End If
            'End If
        End Set
    End Property
    Protected Sub _calStartDate_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _calStartDate.SelectionChanged
        StartDate = _calStartDate.SelectedDate.ToShortDateString()
        If DateTime.Compare(CDate(StartDate), CDate(EndDate)) > 0 Then
            'If StartDate > EndDate Then
            EndDate = CStr(_calStartDate.SelectedDate.AddDays(1))
        End If
        PopupControlExtender1.Cancel()
        If ShowCalendar = False Then ShowCalendar = True
        RaiseEvent CalendarUpdated()
    End Sub

    Protected Sub _calEndDate_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _calEndDate.SelectionChanged
        EndDate = _calEndDate.SelectedDate.ToShortDateString()

        If DateTime.Compare(CDate(StartDate), CDate(EndDate)) > 0 Then
            'If StartDate > EndDate Then
            StartDate = CStr(_calEndDate.SelectedDate.AddDays(-1))
        End If
        PopupControlExtender2.Cancel()
        If ShowCalendar = False Then ShowCalendar = True
        RaiseEvent CalendarUpdated()
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Page.IsPostBack Then
            PopulateMonthYear()
            If Request.Form(Me._txtStartDate.UniqueID) IsNot Nothing Then
                StartDate = Request.Form(Me._txtStartDate.UniqueID)
            End If
            If Request.Form(Me._txtEndDate.UniqueID) IsNot Nothing Then
                EndDate = Request.Form(Me._txtEndDate.UniqueID)
            End If

        End If
    End Sub
    Public Sub ResetCalendar()
        StartDate = mDefaultStartDate
        EndDate = mDefaultEndDate
    End Sub
    Private Sub SetupCalendar()
        If Page.IsPostBack Then

            Me.Visible = ShowCalendar
            Me._txtStartDate.ReadOnly = True
            Me._txtEndDate.ReadOnly = True

            If StartDate.Length = 0 Then
                StartDate = mDefaultStartDate
            End If
            If EndDate.Length = 0 Then
                EndDate = mDefaultEndDate
            End If
            'Me._calStartDate.TodaysDate = CDate(StartDate)
        End If
    End Sub
   
    Private Sub PopulateMonthYear()
        Me._ddlMonthEndDate.Items.Clear()
        Me._ddlMonthStartDate.Items.Clear()
        Me._ddlYearEndDate.Items.Clear()
        Me._ddlYearEndDate.Items.Clear()

        For i As Integer = Year(Today) - 11 To Year(Today) + 15
            Me._ddlYearEndDate.Items.Add(i.ToString)
            Me._ddlYearStartDate.Items.Add(i.ToString)
        Next
        For i As Integer = 1 To 12
            Me._ddlMonthStartDate.Items.Add(New ListItem(MonthName(i), CStr(i)))
            Me._ddlMonthEndDate.Items.Add(New ListItem(MonthName(i), CStr(i)))
        Next
        'drpCalMonth.Items.FindByValue(MonthName(DateTime.Now.Month)).Selected = True
        

    End Sub
    Private Sub SetStartMonthYear()
        Dim _Date As String = StartDate

        If IsDate(_Date) And Not Request.Form("__EventTarget").Contains("ddlMonthStartDate") Then
            If Me._ddlMonthStartDate.Items.FindByValue(CStr(Month(CDate(_Date)))) IsNot Nothing Then
                Me._ddlMonthStartDate.ClearSelection()
                Me._ddlMonthStartDate.Items.FindByValue(CStr(Month(CDate(_Date)))).Selected = True
            End If
            If Me._ddlYearStartDate.Items.FindByValue(CStr(Year(CDate(_Date)))) IsNot Nothing Then
                Me._ddlYearStartDate.ClearSelection()
                Me._ddlYearStartDate.Items.FindByValue(CStr(Year(CDate(_Date)))).Selected = True
            End If
        End If
        Me._calStartDate.TodaysDate = CDate(StartDate)
    End Sub
    Private Sub SetEndMonthYear()
        Dim _Date As String = EndDate

        If IsDate(_Date) And Not Request.Form("__EventTarget").Contains("ddlMonthEndDate") Then
            If Me._ddlMonthEndDate.Items.FindByValue(CStr(Month(CDate(_Date)))) IsNot Nothing Then
                Me._ddlMonthEndDate.ClearSelection()
                Me._ddlMonthEndDate.Items.FindByValue(CStr(Month(CDate(_Date)))).Selected = True
            End If
            If Me._ddlYearEndDate.Items.FindByValue(CStr(Year(CDate(_Date)))) IsNot Nothing Then
                Me._ddlYearEndDate.ClearSelection()
                Me._ddlYearEndDate.Items.FindByValue(CStr(Year(CDate(_Date)))).Selected = True
            End If
        End If
        Me._calEndDate.TodaysDate = CDate(EndDate)
    End Sub

    Protected Sub _ddlMonthEndDate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlMonthEndDate.SelectedIndexChanged
      
        If Page.IsPostBack And RI.SharedFunctions.CausedPostBack(_ddlMonthEndDate.ID) Then
            Me._calEndDate.TodaysDate = DateSerial(CInt(_ddlYearEndDate.SelectedValue), CInt(_ddlMonthEndDate.SelectedValue), Me._calEndDate.TodaysDate.Day)
            '_calEndDate.TodaysDate = CDate(_ddlMonthEndDate.SelectedItem.Value & " 1, " & _ddlYearEndDate.SelectedItem.Value)
        End If
    End Sub

    Protected Sub _ddlYearEndDate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlYearEndDate.SelectedIndexChanged        
        If Page.IsPostBack And RI.SharedFunctions.CausedPostBack(_ddlYearEndDate.ID) Then
            If Request.Form(_ddlYearEndDate.UniqueID) IsNot Nothing Then
                '_calEndDate.TodaysDate = CDate(_ddlMonthEndDate.SelectedItem.Value & " 1, " & Request.Form(_ddlYearEndDate.UniqueID))
                Me._calEndDate.TodaysDate = DateSerial(CInt(Request.Form(_ddlYearEndDate.UniqueID)), CInt(_ddlMonthEndDate.SelectedValue), Me._calEndDate.TodaysDate.Day)
            Else
                Me._calEndDate.TodaysDate = DateSerial(CInt(_ddlYearEndDate.SelectedValue), CInt(_ddlMonthEndDate.SelectedValue), Me._calEndDate.TodaysDate.Day)
                '_calEndDate.TodaysDate = CDate(_ddlMonthEndDate.SelectedItem.Value & " 1, " & _ddlYearEndDate.SelectedItem.Value)
            End If
            EndDate = CStr(_calEndDate.TodaysDate)
        End If
    End Sub

    Protected Sub _ddlMonthStartDate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlMonthStartDate.SelectedIndexChanged
        If Page.IsPostBack And RI.SharedFunctions.CausedPostBack(_ddlMonthStartDate.ID) Then
            '_calStartDate.TodaysDate = CDate(_ddlMonthStartDate.SelectedItem.Value & " 1, " & _ddlYearStartDate.SelectedItem.Value)
            Me._calStartDate.TodaysDate = DateSerial(CInt(_ddlYearStartDate.SelectedValue), CInt(_ddlMonthStartDate.SelectedValue), Me._calStartDate.TodaysDate.Day)
        End If
    End Sub

    Protected Sub _ddlYearStartDate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlYearStartDate.SelectedIndexChanged
        If Page.IsPostBack And RI.SharedFunctions.CausedPostBack(_ddlYearStartDate.ID) Then
            If Request.Form(_ddlYearStartDate.UniqueID) IsNot Nothing Then
                ' _calStartDate.TodaysDate = CDate(_ddlMonthStartDate.SelectedItem.Value & " 1, " & Request.Form(_ddlYearStartDate.UniqueID))
                Me._calStartDate.TodaysDate = DateSerial(CInt(Request.Form(_ddlYearStartDate.UniqueID)), CInt(_ddlMonthStartDate.SelectedValue), Me._calStartDate.TodaysDate.Day)
            Else
                ' _calStartDate.TodaysDate = CDate(_ddlMonthStartDate.SelectedItem.Value & " 1, " & _ddlYearStartDate.SelectedItem.Value)
                Me._calStartDate.TodaysDate = DateSerial(CInt(_ddlYearStartDate.SelectedValue), CInt(_ddlMonthStartDate.SelectedValue), Me._calStartDate.TodaysDate.Day)
            End If
            StartDate = CStr(_calStartDate.TodaysDate)
        End If
    End Sub
  
End Class
