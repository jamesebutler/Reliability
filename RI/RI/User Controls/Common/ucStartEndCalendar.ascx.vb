Option Explicit On
Option Strict On

Partial Class ucStartEndCalendar
    Inherits System.Web.UI.UserControl

    Public Event CalendarUpdated()

    Private mCalendarCommitScript As String = String.Empty
    Public Property CalendarCommitScript() As String
        Get
            Return mCalendarCommitScript
        End Get
        Set(ByVal value As String)
            mCalendarCommitScript = value
        End Set
    End Property

    Private mShowTime As Boolean = False
    Public Property ShowTime() As Boolean
        Get
            Return mShowTime
        End Get
        Set(ByVal value As Boolean)
            mShowTime = value
        End Set
    End Property
    Public Property Enabled() As Boolean
        Get
            Return Me._imgStartCal.Enabled
        End Get
        Set(ByVal value As Boolean)
            Me._imgStartCal.Enabled = value
            Me._imgEndCal.Enabled = value
            Me._ddlEndHrs.Enabled = value
            Me._ddlStartHrs.Enabled = value
            Me._ddlEndMins.Enabled = value
            Me._ddlStartMins.Enabled = value
        End Set
    End Property
    Private mChangeDateLabel As Boolean = False
    Public Property ChangeDateLabel() As Boolean
        Get
            Return mChangeDateLabel
        End Get
        Set(ByVal value As Boolean)
            mChangeDateLabel = value
        End Set
    End Property
    Private mAddDateLabel As String
    Public Property AddDateLabel() As String
        Get
            Return mAddDateLabel
        End Get
        Set(ByVal value As String)
            mAddDateLabel = value
        End Set
    End Property
    Private mClearDate As Boolean = False
    Public Property ClearDate() As Boolean
        Get
            Return mClearDate
        End Get
        Set(ByVal value As Boolean)
            mClearDate = value
        End Set
    End Property
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
            '
            Dim _StartDate As String = String.Empty
            Dim _StartDateNew As Date
            Dim dtformatInfo As System.Globalization.DateTimeFormatInfo = System.Globalization.CultureInfo.CreateSpecificCulture("EN-US").DateTimeFormat

            If Me._hdfStartDateValue.Value.Length = 0 Then
                'If Me._txtStartDate.Text.Length = 0 Then
                If mDefaultStartDate Is Nothing Then
                    _StartDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Now, "EN-US", "d") 'Today.ToShortDateString
                Else
                    _StartDate = mDefaultStartDate
                End If
                _hdfStartDateValue.Value = _StartDate
                _txtStartDate.ToolTip = _hdfStartDateValue.Value
                _StartDateNew = DateTime.Parse(_hdfStartDateValue.Value, dtformatInfo)
            Else
                _StartDate = _hdfStartDateValue.Value
                _StartDateNew = DateTime.Parse(_hdfStartDateValue.Value, dtformatInfo)
                _txtStartDate.ToolTip = _hdfStartDateValue.Value
            End If
            _txtStartDate.Text = IP.Bids.Localization.DateTime.GetLocalizedDateTime(_StartDateNew, Threading.Thread.CurrentThread.CurrentCulture.Name, "d")
            If Me.ShowTime = True Then
                'If _StartDateNew.TimeOfDay.ToString = "00:00:00" Then
                _StartDateNew = New Date(_StartDateNew.Year, _StartDateNew.Month, _StartDateNew.Day, CInt(Me._ddlStartHrs.SelectedValue), CInt(Me._ddlStartMins.SelectedValue), 0)
                'End If
                Return IP.Bids.Localization.DateTime.GetLocalizedDateTime(_StartDateNew, "EN-US", "G")
            Else
                Return IP.Bids.Localization.DateTime.GetLocalizedDateTime(_StartDateNew, "EN-US", "d")
            End If
        End Get
        Set(ByVal value As String)
            Dim dtformatInfo As System.Globalization.DateTimeFormatInfo = System.Globalization.CultureInfo.CreateSpecificCulture("EN-US").DateTimeFormat
            If RI.SharedFunctions.isEnglishDate(value) Then
                'Dim dt As DateTime = CDate(value)
                Dim dt As DateTime = Date.Parse(value, dtformatInfo)
                'Dim dt As DateTime = CDate(IP.Bids.Localization.DateTime.GetLocalizedDateTime(value, Threading.Thread.CurrentThread.CurrentCulture.Name))
                _hdfStartDateValue.Value = IP.Bids.Localization.DateTime.GetLocalizedDateTime(dt.Date, "EN-US") 'CStr(dt.Date)
                _txtStartDate.Text = IP.Bids.Localization.DateTime.GetLocalizedDateTime(dt.Date, Threading.Thread.CurrentThread.CurrentCulture.Name, "d")
                _txtStartDate.ToolTip = _hdfStartDateValue.Value

                If Me.ShowTime = True Then
                    Dim hr As String = dt.Hour.ToString
                    Dim min As String = dt.Minute.ToString

                    If hr.Length = 1 Then hr = "0" & hr
                    If min.Length = 1 Then min = "0" & min
                    If _ddlStartHrs.Items.FindByValue(hr) IsNot Nothing Then
                        _ddlStartHrs.SelectedValue = hr
                    Else
                        _ddlStartHrs.Items.Insert(0, hr)
                        _ddlStartHrs.SelectedValue = hr
                    End If
                    If _ddlStartMins.Items.FindByValue(min) IsNot Nothing Then
                        _ddlStartMins.SelectedValue = min
                    Else
                        _ddlStartMins.Items.Insert(0, min)
                        _ddlStartMins.SelectedValue = min
                    End If
                End If
            End If
        End Set
    End Property
    Public Property EndDate() As String
        Get
            Dim _EndDateNew As Date
            Dim dtformatInfo As System.Globalization.DateTimeFormatInfo = System.Globalization.CultureInfo.CreateSpecificCulture("EN-US").DateTimeFormat

            Dim _EndDate As String = String.Empty
            If Me._hdfEndDateValue.Value.Length = 0 Then
                'If Me._txtEndDate.Text.Length = 0 Then
                If mDefaultEndDate Is Nothing Then
                    _EndDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Now, "EN-US", "d") 'Today.ToShortDateString
                Else
                    _EndDate = mDefaultEndDate
                End If
                '_txtEndDate.Text = _EndDate
                _hdfEndDateValue.Value = _EndDate
                _EndDateNew = DateTime.Parse(_hdfEndDateValue.Value, dtformatInfo)
            Else
                _EndDate = _hdfEndDateValue.Value
                _EndDateNew = DateTime.Parse(_hdfEndDateValue.Value, dtformatInfo)
            End If
            _txtEndDate.Text = IP.Bids.Localization.DateTime.GetLocalizedDateTime(_EndDate, Threading.Thread.CurrentThread.CurrentCulture.Name, "d")
            _txtEndDate.ToolTip = _hdfEndDateValue.Value
            'Else
            'If IsDate(_hdfEndDateValue.Value) Then
            '    'If IsDate(_txtEndDate.Text) Then
            '    If Me.ShowTime = True Then
            '        Dim dt As DateTime = CDate(_hdfEndDateValue.Value)
            '        'Dim dt As DateTime = CDate(_txtEndDate.Text)
            '        dt = New Date(dt.Year, dt.Month, dt.Day, CInt(Me._ddlEndHrs.SelectedValue), CInt(Me._ddlEndMins.SelectedValue), 0)
            '        _EndDate = dt.ToString
            '    Else
            '        _EndDate = _hdfEndDateValue.Value
            '        '_EndDate = _txtEndDate.Text
            '    End If
            'Else
            '    '_EndDate = _txtEndDate.Text
            '    _EndDate = _hdfEndDateValue.Value
            'End If
            'End If
            If Me.ShowTime = True Then
                'If _EndDateNew.TimeOfDay.ToString = "00:00:00" Then
                _EndDateNew = New Date(_EndDateNew.Year, _EndDateNew.Month, _EndDateNew.Day, CInt(Me._ddlEndHrs.SelectedValue), CInt(Me._ddlEndMins.SelectedValue), 0)
                'End If
                Return IP.Bids.Localization.DateTime.GetLocalizedDateTime(_EndDateNew, "EN-US", "G")
            Else
                Return IP.Bids.Localization.DateTime.GetLocalizedDateTime(_EndDateNew, "EN-US", "d")
            End If
        End Get
        Set(ByVal value As String)
            Dim dtformatInfo As System.Globalization.DateTimeFormatInfo = System.Globalization.CultureInfo.CreateSpecificCulture("EN-US").DateTimeFormat
            If RI.SharedFunctions.isEnglishDate(value) Then
                'Dim dt As DateTime = CDate(value)
                '_hdfEndDateValue.Value = CStr(dt.Date)
                'Dim dt As DateTime = CDate(IP.Bids.Localization.DateTime.GetLocalizedDateTime(value, Threading.Thread.CurrentThread.CurrentCulture.Name))
                Dim dt As DateTime = Date.Parse(value, dtformatInfo)
                _hdfEndDateValue.Value = IP.Bids.Localization.DateTime.GetLocalizedDateTime(dt.Date, "EN-US") 'CStr(dt.Date)
                _txtEndDate.Text = IP.Bids.Localization.DateTime.GetLocalizedDateTime(dt.Date, Threading.Thread.CurrentThread.CurrentCulture.Name, "d")
                _txtEndDate.ToolTip = _hdfEndDateValue.Value
                '_txtEndDate.Text = CStr(dt.Date)
                If Me.ShowTime = True Then
                    Dim hr As String = dt.Hour.ToString
                    Dim min As String = dt.Minute.ToString

                    If hr.Length = 1 Then hr = "0" & hr
                    If min.Length = 1 Then min = "0" & min
                    If _ddlEndHrs.Items.FindByValue(hr) IsNot Nothing Then
                        _ddlEndHrs.SelectedValue = hr
                    Else
                        _ddlEndHrs.Items.Insert(0, hr)
                        _ddlEndHrs.SelectedValue = hr
                    End If
                    If _ddlEndMins.Items.FindByValue(min) IsNot Nothing Then
                        _ddlEndMins.SelectedValue = min
                    Else
                        _ddlEndMins.Items.Insert(0, min)
                        _ddlEndMins.SelectedValue = min
                    End If

                    'If _ddlEndHrs.Items.FindByValue(hr) IsNot Nothing Then _ddlEndHrs.SelectedValue = hr
                    'If _ddlEndMins.Items.FindByValue(min) IsNot Nothing Then _ddlEndMins.SelectedValue = min
                End If
            End If
        End Set
    End Property
    
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'If Page.IsPostBack Then
        '    If Request.Form(Me._txtStartDate.UniqueID) IsNot Nothing Then
        '        StartDate = Request.Form(Me._txtStartDate.UniqueID)
        '    End If
        '    If Request.Form(Me._txtEndDate.UniqueID) IsNot Nothing Then
        '        EndDate = Request.Form(Me._txtEndDate.UniqueID)
        '    End If           
        'End If

    End Sub
    Private Sub SetupPopupCalendar()
        Dim sb As New StringBuilder
        Dim currStartDate As String = Me._hdfStartDateValue.Value  'Me._txtStartDate.Text
        If Not RI.SharedFunctions.isEnglishDate(currStartDate) Then 'IsDate(currStartDate) Then
            If Me.ShowTime = True Then
                currStartDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Now, "EN-US", "G") 'Now().ToShortDateString
            Else
                currStartDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Now, "EN-US", "d") 'Now().ToShortDateString
            End If
        End If
            StartDate = currStartDate
            sb.Append("DisplayCalendar('")
            sb.Append(Page.ResolveUrl("~/PopupCalendar.aspx"))
            sb.Append("?parent=")
        sb.Append(Me._txtStartDate.ClientID.ToString)
            sb.Append("','")
        sb.Append(Me._txtStartDate.ClientID.ToString)
            sb.Append("','")
            sb.Append(CalendarCommitScript)
            sb.Append("');return false;")
            _imgStartCal.OnClientClick = sb.ToString

            sb.Length = 0
            Dim currEndDate As String = Me._hdfEndDateValue.Value  'Me._txtEndDate.Text
            If Not RI.SharedFunctions.isEnglishDate(currEndDate) Then
                currEndDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Now, "EN-US", "d") 'Now().ToShortDateString
            End If
            EndDate = currEndDate
            sb.Append("DisplayCalendar('")
            sb.Append(Page.ResolveUrl("~/PopupCalendar.aspx"))
            sb.Append("?parent=")
        sb.Append(Me._txtEndDate.ClientID.ToString)
            sb.Append("','")
        sb.Append(Me._txtEndDate.ClientID.ToString)
            sb.Append("','")
            sb.Append(CalendarCommitScript)
            sb.Append("');return false;")
            _imgEndCal.OnClientClick = sb.ToString
    End Sub
    Private Sub SetupPopupCalendar(ByVal StartEnd As Boolean)
        Dim sb As New StringBuilder
        Dim currStartDate As String = Me._hdfStartDateValue.Value  'Me._txtStartDate.Text
        If Not RI.SharedFunctions.isEnglishDate(currStartDate) Then 'IsDate(currStartDate) Then
            If Me.ShowTime = True Then
                currStartDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Now, "EN-US", "G") 'Now().ToShortDateString
            Else
                currStartDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Now, "EN-US", "d") 'Now().ToShortDateString
            End If
        End If
        StartDate = currStartDate
        Dim currEndDate As String = Me._hdfEndDateValue.Value
        If Not RI.SharedFunctions.isEnglishDate(currEndDate) Then
            currEndDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Now, "EN-US") ' Now().ToShortDateString
        End If
        EndDate = currEndDate
        sb.Append("DisplayStartEndCalendar('")
        sb.Append(Page.ResolveClientUrl("~/PopupStartEndCalendar.aspx"))
        sb.Append("?StartParent=")
        sb.Append(Me._txtStartDate.ClientID.ToString)
        sb.Append("&EndParent=")
        sb.Append(Me._txtEndDate.ClientID.ToString)
        sb.Append("','")
        sb.Append(Me._txtStartDate.ClientID.ToString)
        sb.Append("','")
        sb.Append(Me._txtEndDate.ClientID.ToString)
        sb.Append("','")
        sb.Append(Me._hdfStartDateValue.ClientID.ToString)
        sb.Append("','")
        sb.Append(Me._hdfEndDateValue.ClientID.ToString)
        sb.Append("','")
        sb.Append(CalendarCommitScript)
        sb.Append("'); return false;")
        _imgStartCal.OnClientClick = sb.ToString

            'sb.Length = 0
            'Dim currEndDate As String = Me._txtEndDate.Text
            'If Not IsDate(currEndDate) Then
            '    currEndDate = Now().ToShortDateString
            'End If
            'EndDate = currEndDate
            'sb.Append("DisplayCalendar('")
            'sb.Append(Page.ResolveUrl("~/PopupStartEndCalendar.aspx"))
            'sb.Append("?parent=")
            'sb.Append(Me._txtEndDate.UniqueID.ToString)
            'sb.Append("','")
            'sb.Append(Me._txtEndDate.UniqueID.ToString)
            'sb.Append("','")
            'sb.Append(CalendarCommitScript)
            'sb.Append("');return false;")
        _imgEndCal.OnClientClick = sb.ToString
    End Sub
    Public Sub RefreshCalendarOnClick()
        SetupPopupCalendar(True)
    End Sub
    Public Sub ResetCalendar()
        StartDate = mDefaultStartDate
        EndDate = mDefaultEndDate
    End Sub
    Private Sub SetupCalendar()
        If ClearDate = True Then
            mDefaultEndDate = Nothing
        End If
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
        End If

        
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack Then
            If Request.Form(Me._hdfStartDateValue.UniqueID) IsNot Nothing Then
                Dim start As String = Request.Form(Me._hdfStartDateValue.UniqueID)
                StartDate = start
            End If
            If Request.Form(Me._hdfEndDateValue.UniqueID) IsNot Nothing Then
                Dim endDt As String
                endDt = Request.Form(Me._hdfEndDateValue.UniqueID)
                EndDate = endDt
            End If
        End If
        Me._txtStartDate.ReadOnly = True
        Me._txtEndDate.ReadOnly = True

        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "StartEndCalendar") Then Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "StartEndCalendar", Page.ResolveClientUrl("~/ri/User Controls/Common/StartEndCalendar.js"))
        SetupPopupCalendar(True)

        'If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "DHTMLWindow") Then
        '    Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "DHTMLWindow", Page.ResolveClientUrl("~/windowfiles/dhtmlwindow.js"))
        'End If
        'If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "ModalWindow") Then
        '    Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "ModalWindow", Page.ResolveClientUrl("~/modalfiles/modal.js"))
        'End If
        DisplayTime()
       
    End Sub
    Private Sub DisplayTime()
        Dim iploc As New IP.Bids.Localization.WebLocalization()
        If Me.ShowTime = True Then
            Me._lblStartTime.Visible = True
            Me._ddlStartHrs.Visible = True
            Me._ddlStartMins.Visible = True
            Me._lblEndTime.Visible = True
            Me._ddlEndHrs.Visible = True
            Me._ddlEndMins.Visible = True
            PopulateCalTime()
            If Me.ChangeDateLabel = True Then
                Me._lblEndDate.Text = iploc.GetResourceValue("Expiration Date")
                Me._lblStartDate.Text = iploc.GetResourceValue("Implementation Date")
            Else
                'Me._lblEndDate.Text = Resources.Shared.lblEnd
                'Me._lblStartDate.Text = Resources.Shared.lblStart
            End If

        Else
            Me._lblStartTime.Visible = False
            Me._ddlStartHrs.Visible = False
            Me._ddlStartMins.Visible = False
            Me._lblEndTime.Visible = False
            Me._ddlEndHrs.Visible = False
            Me._ddlEndMins.Visible = False

            If Me.ChangeDateLabel = True Then
                Me._lblEndDate.Text = iploc.GetResourceValue("Expiration Date")
                Me._lblStartDate.Text = iploc.GetResourceValue("Implementation Date")
            Else
                _lblEndDate.Text = iploc.GetResourceValue("EndDate")
                _lblStartDate.Text = iploc.GetResourceValue("StartDate")
            End If
        End If

        If AddDateLabel <> "" Then
            _lblStartDate.Text = iploc.GetResourceValue(AddDateLabel) & " " & iploc.GetResourceValue("Start")
            _lblEndDate.Text = iploc.GetResourceValue(AddDateLabel) & " " & iploc.GetResourceValue("End")
        End If
    End Sub
    Private Sub PopulateCalTime()
        Dim hrs As String = String.Empty
        Dim mins As String = String.Empty
        Dim selectedStartHrs As String = _ddlStartHrs.SelectedValue
        Dim selectedStartMins As String = _ddlStartMins.SelectedValue
        Dim selectedEndHrs As String = _ddlEndHrs.SelectedValue
        Dim selectedEndMins As String = _ddlEndMins.SelectedValue

        Me._ddlStartHrs.Items.Clear()
        Me._ddlStartMins.Items.Clear()
        Me._ddlEndHrs.Items.Clear()
        Me._ddlEndMins.Items.Clear()

        For i As Integer = 0 To 23
            hrs = i.ToString
            If hrs.Length = 1 Then hrs = "0" & hrs
            _ddlStartHrs.Items.Add(hrs)
            _ddlEndHrs.Items.Add(hrs)
        Next

        For i As Integer = 0 To 59
            mins = i.ToString
            If mins.Length = 1 Then mins = "0" & mins
            _ddlStartMins.Items.Add(mins)
            _ddlEndMins.Items.Add(mins)
        Next

        If Request.Form(Me._ddlStartHrs.UniqueID) IsNot Nothing Then
            _ddlStartHrs.SelectedValue = Request.Form(Me._ddlStartHrs.UniqueID)
        Else
            _ddlStartHrs.SelectedValue = selectedStartHrs
        End If
        If Request.Form(Me._ddlStartMins.UniqueID) IsNot Nothing Then
            _ddlStartMins.SelectedValue = Request.Form(Me._ddlStartMins.UniqueID)
        Else
            _ddlStartMins.SelectedValue = selectedStartMins
        End If

        If Request.Form(Me._ddlEndHrs.UniqueID) IsNot Nothing Then
            _ddlEndHrs.SelectedValue = Request.Form(Me._ddlEndHrs.UniqueID)
        Else
            _ddlEndHrs.SelectedValue = selectedEndHrs
        End If
        If Request.Form(Me._ddlEndMins.UniqueID) IsNot Nothing Then
            _ddlEndMins.SelectedValue = Request.Form(Me._ddlEndMins.UniqueID)
        Else
            _ddlEndMins.SelectedValue = selectedEndMins
        End If

    End Sub

    Public Sub ClearCalendar()
        ClearDate = True
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ClearDate = True Then
            Me._txtEndDate.Text = ""
            Me._txtStartDate.Text = ""
            Me._hdfStartDateValue.Value = ""
            Me._hdfEndDateValue.Value = ""

            Me._ddlEndHrs.SelectedValue = "00"
            Me._ddlEndMins.SelectedValue = "00"
            Me._ddlStartHrs.SelectedValue = "00"
            Me._ddlStartMins.SelectedValue = "00"
        End If
    End Sub
End Class
