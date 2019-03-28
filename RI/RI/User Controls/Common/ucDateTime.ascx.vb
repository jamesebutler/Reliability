<ValidationProperty("DateValue")> Partial Class RI_User_Controls_Common_ucDateTime
    Inherits System.Web.UI.UserControl

    Public Event CalendarUpdated(ByVal sender As Object, ByVal args As System.EventArgs)
    Public Property DisplayTime() As Boolean
        Get
            If ViewState("DisplayTime") IsNot Nothing Then
                Return CType(ViewState("DisplayTime"), Boolean)
            Else
                Return True 'Default
            End If
        End Get
        Set(ByVal value As Boolean)
            ViewState("DisplayTime") = value
        End Set
    End Property
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
    Public Property Hours() As String
        Get
            Return Me._ddlHrs.SelectedValue
        End Get
        Set(ByVal value As String)
            If _ddlHrs.Items.Count = 0 Then Me.PopulateCalTime()
            If value.Length = 1 Then value = "0" & value
            If Me._ddlHrs.Items.FindByValue(value) IsNot Nothing Then
                _ddlHrs.SelectedValue = value
            End If
        End Set
    End Property

    Public Property Minutes() As String
        Get
            Return Me._ddlMins.SelectedValue
        End Get
        Set(ByVal value As String)
            If _ddlMins.Items.Count = 0 Then Me.PopulateCalTime()
            If value.Length = 1 Then value = "0" & value
            If Me._ddlMins.Items.FindByValue(value) IsNot Nothing Then
                Me._ddlMins.SelectedValue = value
            End If
        End Set
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
            'If IsDate(IP.Bids.Localization.DateTime.GetLocalizedDateTime(_txtDate.Text)) Then
            '    Dim dt As DateTime = _txtDate.Text
            '    'ViewState.Add("txtDate" & _txtDate.ClientID, dt.Date)
            '    Return dt.Date
            'Else
            '    'ViewState.Add("txtDate" & _txtDate.ClientID, "")
            '    Return ""
            'End If
            Return IP.Bids.Localization.DateTime.GetLocalizedDateTime(Me._hdfDateValue.Value, "EN-US", "d")
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
                Me.Hours = dt.Hour
                Me.Minutes = dt.Minute

            Else
                _txtDate.Text = ""
                ViewState.Add("txtDate" & _txtDate.ClientID, "")
            End If
            'Dim englishDate As Date = IP.Bids.Localization.DateTime.GetLocalizedDateTime(value, "EN-US", "d") 'Convert.ToDateTime(value, System.Globalization.CultureInfo.CreateSpecificCulture("EN-US"))
            'Dim newDate As Date = New Date(englishDate.Year, englishDate.Month, englishDate.Day)

            'If IsDate(newDate) Then
            '    Dim dt As DateTime = newDate
            '    If ViewState("txtDate" & _txtDate.ClientID) Is Nothing Then
            '        ViewState.Add("txtDate" & _txtDate.ClientID, dt.Date)
            '    Else
            '        ViewState.Add("txtDate" & _txtDate.ClientID, _txtDate.Text)
            '    End If
            '    _txtDate.Text = dt.Date
            '    Me.Hours = dt.Hour
            '    Me.Minutes = dt.Minute
            'Else
            '    _txtDate.Text = ""
            '    ViewState.Add("txtDate" & _txtDate.ClientID, "")
            'End If
        End Set
    End Property
    Private Sub DisplayorHideTime(ByVal value As Boolean)
        Me._ddlHrs.Visible = value
        Me._ddlMins.Visible = value
        Me._lblTime.Visible = value
    End Sub

    Protected Sub Page_CalendarUpdated(ByVal sender As Object, ByVal args As System.EventArgs) Handles Me.CalendarUpdated

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If DisplayTime = True Then
            If Me._ddlHrs.Items.Count = 0 And Me._ddlMins.Items.Count = 0 Then
                PopulateCalTime()
            End If
        Else
            DisplayorHideTime(DisplayTime)
        End If
       
        ScriptManager.RegisterClientScriptInclude(Page, Page.GetType, "StartEndCalendar", Page.ResolveClientUrl("~/ri/User Controls/Common/StartEndCalendar.js"))
        'Add call for checking the year when user only enters 2 digits
        Me._txtDate.Attributes.Add("onchange", "dateComplete('" + _txtDate.ClientID + "');")
        'If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "DHTMLWindow") Then
        'ScriptManager.RegisterClientScriptInclude(Page, Page.GetType, "DHTMLWindow", Page.ResolveClientUrl("~/windowfiles/dhtmlwindow.js"))
        ''End If
        ''If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "ModalWindow") Then
        'ScriptManager.RegisterClientScriptInclude(Page, Page.GetType, "ModalWindow", Page.ResolveClientUrl("~/modalfiles/modal.js"))
        'End If

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
    Private Function GetDateTime() As DateTime
        Dim dt As Date = Nothing
        If IsDate(Me.DateValue) Then
            dt = Me.DateValue
            ' dt = datepart(DateInterval.Day ,dt,Microsoft.VisualBasic.FirstDayOfWeek.Friday ,FirstWeekOfYear.FirstFourDays 
            dt = New Date(dt.Year, dt.Month, dt.Day, Me.Hours, Me.Minutes, 0)
        End If
        Return dt
    End Function
    Private Sub PopulateCalTime()
        Dim hrs As String = String.Empty
        Dim mins As String = String.Empty

        Me._ddlHrs.Items.Clear()
        Me._ddlMins.Items.Clear()
        For i As Integer = 0 To 23
            hrs = i.ToString
            If hrs.Length = 1 Then hrs = "0" & hrs
            _ddlHrs.Items.Add(hrs)
        Next

        For i As Integer = 0 To 59
            mins = i.ToString
            If mins.Length = 1 Then mins = "0" & mins
            _ddlMins.Items.Add(mins)
        Next

        If Request.Form(Me._ddlHrs.UniqueID) IsNot Nothing Then
            _ddlHrs.SelectedValue = Request.Form(Me._ddlHrs.UniqueID)
        End If
        If Request.Form(Me._ddlMins.UniqueID) IsNot Nothing Then
            _ddlMins.SelectedValue = Request.Form(Me._ddlMins.UniqueID)
        End If

    End Sub
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
