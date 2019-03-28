<ValidationProperty("DateValue")> Partial Class ucMOCDate
    Inherits System.Web.UI.UserControl

    Public Event CalendarUpdated(ByVal sender As Object, ByVal args As System.EventArgs)
    Public Property AllowManualDate() As Boolean
        Get
            If ViewState("AllowManualDate") IsNot Nothing Then
                Return CType(ViewState("AllowManualDate"), Boolean)
            Else
                Return False
            End If
        End Get
        Set(ByVal value As Boolean)
            ViewState("AllowManualDate") = value
        End Set
    End Property

    Public Property HeaderText() As String
        Get
            If ViewState("HeaderText") IsNot Nothing Then
                Return CType(ViewState("HeaderText"), String)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("HeaderText") = value
        End Set
    End Property
    Public Property RequiredField() As Boolean
        Get
            Return Me._lblRequired.Visible
        End Get
        Set(ByVal value As Boolean)
            Me._lblRequired.Visible = value
        End Set
    End Property
    Public Property ValidationGroup() As String
        Get
            Return Me._txtDate.ValidationGroup
        End Get
        Set(ByVal value As String)
            If value.Length > 0 Then
                Me._txtDate.ValidationGroup = value
            End If
        End Set
    End Property
    Public ReadOnly Property DateTime() As Date
        Get
            Return GetDateTime()
        End Get
    End Property

    Public Property DateLabel() As String
        Get
            Return Me._lblDate.Text
        End Get
        Set(ByVal value As String)
            Me._lblDate.Text = value
        End Set
    End Property

    Public Property DateValue() As String
        Get
            Dim _StartDate As String = String.Empty
            Dim _StartDateNew As Date
            Dim dtformatInfo As System.Globalization.DateTimeFormatInfo = System.Globalization.CultureInfo.CreateSpecificCulture("EN-US").DateTimeFormat

            If Me._hdfDateValue.Value.Length = 0 Then
                'If Me._txtStartDate.Text.Length = 0 Then
                'If mDefaultStartDate Is Nothing Then
                _StartDate = "" 'IP.Bids.Localization.DateTime.GetLocalizedDateTime(Now, "EN-US", "d") 'Today.ToShortDateString
                'Else
                '  _StartDate = mDefaultStartDate
                'End If
                _hdfDateValue.Value = _StartDate
                _txtDate.ToolTip = _hdfDateValue.Value
                _txtDate.Text = ""
                '_StartDateNew = Date.Parse(_hdfDateValue.Value, dtformatInfo)
                Return ""
            Else
                _StartDate = _hdfDateValue.Value
                _StartDateNew = Date.Parse(_hdfDateValue.Value, dtformatInfo)
                _txtDate.ToolTip = _hdfDateValue.Value
                _txtDate.Text = IP.Bids.Localization.DateTime.GetLocalizedDateTime(_StartDateNew, Threading.Thread.CurrentThread.CurrentCulture.Name, "d")
                _txtDaysAfter.Enabled = False
                Return IP.Bids.Localization.DateTime.GetLocalizedDateTime(_StartDateNew, "EN-US", "d")

            End If
            '_txtDate.Text = IP.Bids.Localization.DateTime.GetLocalizedDateTime(_StartDateNew, Threading.Thread.CurrentThread.CurrentCulture.Name, "d")

            ' Return IP.Bids.Localization.DateTime.GetLocalizedDateTime(_StartDateNew, "EN-US", "d")
            
        End Get
        Set(ByVal value As String)
            'Value is always an English date
            If value.Length = 0 Then
                _txtDate.Text = ""
                Me._hdfDateValue.Value = ""
                ViewState.Add("txtDate" & _txtDate.ClientID, "")
                Exit Property
            End If

            If IsDate(value) Then
                Dim dt As DateTime = value
                If ViewState("txtDate" & _txtDate.ClientID) Is Nothing Then
                    ViewState.Add("txtDate" & _txtDate.ClientID, dt.Date)
                Else
                    ViewState.Add("txtDate" & _txtDate.ClientID, _txtDate.Text)
                End If
                _txtDate.Text = dt.Date
                Me._hdfDateValue.Value = IP.Bids.Localization.DateTime.GetLocalizedDateTime(dt.Date, "EN-US", "d")

            Else
                _txtDate.Text = ""
                ViewState.Add("txtDate" & _txtDate.ClientID, "")
            End If
        End Set
    End Property
    Public Property DaysAfter() As String
        Get
            Return Me._txtDaysAfter.Text
        End Get
        Set(ByVal value As String)
            Me._txtDaysAfter.Text = value
        End Set
    End Property

    Protected Sub Page_CalendarUpdated(ByVal sender As Object, ByVal args As System.EventArgs) Handles Me.CalendarUpdated

    End Sub

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "MOCDate_" & Me.ID, Page.ResolveClientUrl("~/ri/User Controls/Common/MOCDate.js"))
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
'        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "MOCDate_" & 'me.id) Then 
Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "MOCDate_" & me.id, Page.ResolveClientUrl("~/ri/User Controls/Common/MOCDate.js"))
        'ScriptManager.RegisterClientScriptInclude(Page, Page.GetType, "MOCDate", Page.ResolveClientUrl("~/ri/User Controls/Common/MOCDate.js"))

        _txtDate.Attributes.Add("onchange", "dateComplete('" + _txtDate.ClientID + "');")
        _txtDaysAfter.Attributes.Add("onchange", "enableField('" & Me._txtDaysAfter.ClientID & "','" & Me._imgCalendar.ClientID & "');")
        'Me._lbClear.Attributes.Add("onclientclick", "clearDate('" + _txtDate.ClientID + "');")
        _lbClear.OnClientClick = "clearDate('" & Me._txtDate.ClientID & "','" & Me._hdfDateValue.ClientID & "');"
        _txtDaysAfter.Attributes.Add("onblur", "clearDate('" & Me._txtDate.ClientID & "','" & Me._hdfDateValue.ClientID & "');")

        SetupPopupCalendar()
        If AllowManualDate = True Then
            Me._txtDate.ReadOnly = False
        Else
            'Me._txtDate.ReadOnly = True
            Me._txtDate.Attributes.Add("ReadOnly", "ReadOnly")
        End If
        'Dim LastDate As String = String.Empty
        'If ViewState("txtDate" & _txtDate.ClientID) IsNot Nothing Then
        '    LastDate = ViewState("txtDate" & _txtDate.ClientID)
        'End If
        'If DateValue <> LastDate Then
        '    RaiseEvent CalendarUpdated(sender, e)
        'End If
    End Sub
    Private Function GetDateTime() As Date
        Dim dt As Date = Nothing
        If IsDate(Me.DateValue) Then
            dt = Me.DateValue
            ' dt = datepart(DateInterval.Day ,dt,Microsoft.VisualBasic.FirstDayOfWeek.Friday ,FirstWeekOfYear.FirstFourDays 
            dt = New Date(dt.Year, dt.Month, dt.Day)
        End If
        Return dt
    End Function
    Private Sub SetupPopupCalendar()
        Dim sb As New StringBuilder
        Dim currStartDate As String = Me._txtDate.Text
        If Not IsDate(currStartDate) Then
            currStartDate = Now().ToShortDateString
        End If
        sb.Append("DisplayCalendar('")
        sb.Append(Page.ResolveUrl("~/PopupCalendar.aspx"))
        sb.Append("?parent=")
        sb.Append(Me._txtDate.ClientID.ToString) 'Me._txtDate.UniqueID.ToString)
        sb.Append("','")
        sb.Append(Me._txtDate.ClientID.ToString) '(Me._txtDate.UniqueID.ToString)
        sb.Append("','")
        sb.Append(Me._hdfDateValue.ClientID.ToString) '(Me._hdfDateValue.UniqueID.ToString)
        sb.Append("', null,'")
        sb.Append(HeaderText)
        sb.Append("');return false;")
        _imgCalendar.OnClientClick = sb.ToString
    End Sub

    Protected Sub _txtDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _txtDate.TextChanged
        Dim LastDate As String = String.Empty
        If ViewState("txtDate" & _txtDate.ClientID) IsNot Nothing Then
            LastDate = ViewState("txtDate" & _txtDate.ClientID)
        End If
        If _txtDate.Text <> LastDate Then ' If DateValue <> LastDate Then
            RaiseEvent CalendarUpdated(sender, e)
        End If
    End Sub
End Class
