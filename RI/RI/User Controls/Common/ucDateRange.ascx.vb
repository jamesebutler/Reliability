Imports Resources

<ValidationProperty("SelectedDateRange")> _
Partial Class RI_User_Controls_Common_ucDateRange
    Inherits System.Web.UI.UserControl

    Private mStartDate As DateValue
    Private mEndDate As DateValue
    Private mSelectedDateRange As range
    Private mDisplayAsDropDown As Boolean = True
    Private mShowFutureDates As Boolean = True
    Public Event DateRangeChanged()

    Public Property ShowFutureDates() As Boolean
        Get
            Return mShowFutureDates
        End Get
        Set(ByVal value As Boolean)
            mShowFutureDates = value
        End Set
    End Property
    Public Property DisplayAsDropDown() As Boolean
        Get
            Return mDisplayAsDropDown
        End Get
        Set(ByVal value As Boolean)
            mDisplayAsDropDown = value
        End Set
    End Property
    Public ReadOnly Property ClearSelection() As String
        Get
            Return "clearButtons();"
        End Get
    End Property
    Public Property StartDate() As String
        Get
            'mStartDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(mStartDate, "en-US", "d")
            'mStartDate.Text = IP.Bids.Localization.DateTime.GetLocalizedDateTime(mStartDate.Value, System.Threading.Thread.CurrentThread.CurrentCulture.Name, "d")
            Try
                'Return IP.Bids.Localization.DateTime.GetLocalizedDateTime(mStartDate.Value, "EN-US", "d")
                Return mStartDate.Value
            Catch
                Throw
            End Try
        End Get
        Set(ByVal value As String)
            Try
                'Value is always an English date
                Dim englishDate As Date = Convert.ToDateTime(value, System.Globalization.CultureInfo.CreateSpecificCulture("EN-US"))
                Dim newDate As Date = New Date(englishDate.Year, englishDate.Month, englishDate.Day)
                'Now localize Englishdate
                mStartDate.Value = IP.Bids.Localization.DateTime.GetLocalizedDateTime(newDate)
                _StartEndCalendar.StartDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(mStartDate.Value, "EN-US")
            Catch
                Throw
            End Try
        End Set
    End Property

    Public ReadOnly Property StartDateText() As String
        Get
            Return IP.Bids.Localization.DateTime.GetLocalizedDateTime(mStartDate.Value, System.Threading.Thread.CurrentThread.CurrentCulture.Name, "d")
        End Get
    End Property

    Public Property EndDate() As String
        Get
            'mEndDate.Text = IP.Bids.Localization.DateTime.GetLocalizedDateTime(mEndDate.Value, System.Threading.Thread.CurrentThread.CurrentCulture.Name, "d")
            Try
                Return mEndDate.Value
                'Return IP.Bids.Localization.DateTime.GetLocalizedDateTime(mEndDate.Value, "EN-US", "d") 'RI.SharedFunctions.FormatDateTimeToEnglish(mEndDate.Value)
                'Return IP.Bids.Localization.DateTime.GetLocalizedDateTime(mEndDate.Value, System.Threading.Thread.CurrentThread.CurrentCulture.Name, "d")
            Catch
                Throw
            End Try
        End Get
        Set(ByVal value As String)
            Try
                'Value is always an English date
                Dim englishDate As Date = Convert.ToDateTime(value, System.Globalization.CultureInfo.CreateSpecificCulture("EN-US"))
                Dim newDate As Date = New Date(englishDate.Year, englishDate.Month, englishDate.Day)
                'Now localize Englishdate
                mEndDate.Value = IP.Bids.Localization.DateTime.GetLocalizedDateTime(newDate)
                _StartEndCalendar.EndDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(mEndDate.Value, "EN-US")
            Catch
                Throw
            End Try
        End Set
    End Property

    Public ReadOnly Property EndDateText() As String
        Get
            Return IP.Bids.Localization.DateTime.GetLocalizedDateTime(mEndDate.Value, System.Threading.Thread.CurrentThread.CurrentCulture.Name, "d")
        End Get
    End Property

    Private mChangeDateLabel As Boolean = False
    Public Property ChangeDateLabel() As Boolean
        Get
            Return mChangeDateLabel
        End Get
        Set(ByVal value As Boolean)
            mChangeDateLabel = value
            _StartEndCalendar.ChangeDateLabel = value
        End Set
    End Property

    <PersistenceMode(PersistenceMode.InnerDefaultProperty)> _
        Public Property SelectedDateRange() As range
        Get
            Return mSelectedDateRange
            'Return Me._rblDateRange.SelectedValue
        End Get
        Set(ByVal value As range)
            mSelectedDateRange = value
            Me.SetDateRange(value)
        End Set
    End Property

    Private Sub DateRangeChange(ByVal selectedValue As range)
        SetDateRange(selectedValue)
    End Sub
    Public Enum range
        LastMonth = 1
        Last3Months = 2
        LastYearToDate = 3
        YearToDate = 4
        FirstQuarter = 5
        SecondQuarter = 6
        ThirdQuarter = 7
        FourthQuarter = 8
        EnteredLast7Days = 9
        LastYear = 10
        EndOfYear = 11
        Last12Months = 12
        CurrentMonth = 13
        TMinus15MthToTMinus3Mth = 14
    End Enum
    Private Sub SetDateRange(ByVal dtRange As range)
        Try
            Dim todaysDate As Date = Now
            If Me.DisplayAsDropDown = False Then
                If _rblDateRange.Items.Count = 0 Then
                    PopulateDateRange()
                End If
                If Me._rblDateRange.Items.FindByValue(dtRange) IsNot Nothing Then
                    Me._rblDateRange.Items.FindByValue(dtRange).Selected = True
                End If
            Else
                If _ddlDateRange.Items.Count = 0 Then
                    PopulateDateRange()
                End If
                If Me._ddlDateRange.Items.FindByValue(dtRange) IsNot Nothing Then
                    Me._ddlDateRange.ClearSelection()
                    Me._ddlDateRange.Items.FindByValue(dtRange).Selected = True
                End If
            End If
            Select Case dtRange
                Case range.LastMonth
                    mStartDate.Value = DateSerial(Year(todaysDate), Month(todaysDate) - 1, 1)
                    mEndDate.Value = DateSerial(Year(todaysDate), Month(todaysDate), 0)
                Case range.Last3Months
                    mStartDate.Value = DateSerial(Year(todaysDate), Month(todaysDate) - 3, 1)
                    mEndDate.Value = DateSerial(Year(todaysDate), Month(todaysDate), 0)
                Case range.LastYearToDate '"last year to date"
                    mStartDate.Value = DateSerial(Year(todaysDate) - 1, 1, 1)
                    mEndDate.Value = todaysDate.ToShortDateString
                Case range.YearToDate '"year to date"
                    mStartDate.Value = DateSerial(Year(todaysDate), 1, 1)
                    mEndDate.Value = todaysDate.ToShortDateString
                Case range.FirstQuarter '"1st quarter"
                    mStartDate.Value = DateSerial(Year(todaysDate), 1, 1)
                    mEndDate.Value = DateSerial(Year(todaysDate), 4, 0)
                Case range.SecondQuarter '"2nd quarter"
                    mStartDate.Value = DateSerial(Year(todaysDate), 4, 1)
                    mEndDate.Value = DateSerial(Year(todaysDate), 7, 0)
                Case range.ThirdQuarter '"3rd quarter"
                    mStartDate.Value = DateSerial(Year(todaysDate), 7, 1)
                    mEndDate.Value = DateSerial(Year(todaysDate), 10, 0)
                Case range.FourthQuarter '"4th quarter"
                    mStartDate.Value = DateSerial(Year(todaysDate), 10, 1)
                    mEndDate.Value = DateSerial(Year(todaysDate), 13, 0)
                Case range.EnteredLast7Days '"entered last 7 days"
                    mStartDate.Value = todaysDate.AddDays(-7).ToShortDateString  'DateSerial(Year(todaysDate), Month(todaysDate), -7)
                    mEndDate.Value = todaysDate.ToShortDateString
                Case range.LastYear
                    mStartDate.Value = DateSerial(Year(todaysDate) - 1, 1, 1)
                    mEndDate.Value = DateSerial(Year(todaysDate) - 1, 12, 31)
                Case range.EndOfYear
                    mStartDate.Value = DateSerial(Year(todaysDate), 1, 1)
                    mEndDate.Value = DateSerial(Year(todaysDate), 12, 31)
                Case range.Last12Months
                    mStartDate.Value = Now.AddYears(-1).ToShortDateString
                    mEndDate.Value = todaysDate.ToShortDateString
                Case range.CurrentMonth
                    mStartDate.Value = DateSerial(Year(todaysDate), Month(todaysDate), 1)
                    mEndDate.Value = DateSerial(Year(todaysDate), Month(todaysDate), 1).AddMonths(1).AddDays(-1)
                Case range.TMinus15MthToTMinus3Mth
                    mStartDate.Value = Now.AddMonths(-15).ToShortDateString
                    mEndDate.Value = Now.AddMonths(-3).ToShortDateString
                Case Else
                    mStartDate.Value = Nothing
                    mEndDate.Value = Nothing
            End Select
            If mStartDate.Value <> Nothing And mEndDate.Value <> Nothing Then
                Me._StartEndCalendar.StartDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(mStartDate.Value, "EN-US")
                Me._StartEndCalendar.EndDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(mEndDate.Value, "EN-US")
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Me.DisplayAsDropDown = False Then
            If _rblDateRange.Items.Count = 0 Then
                ' PopulateDateRange()
                If Not Page.IsPostBack Then
                    Me.SetDateRange(Me.SelectedDateRange)
                Else
                    Me.SetDateRange(-1)
                End If
            End If
        Else
            If _ddlDateRange.Items.Count = 0 Then
                ' PopulateDateRange()
                If Not Page.IsPostBack Then
                    Me.SetDateRange(Me.SelectedDateRange)
                Else
                    Me.SetDateRange(-1)
                End If
            End If
        End If
    End Sub
    Private Sub PopulateDateRange()
        Dim iploc As New IP.Bids.Localization.WebLocalization()
        Me._ddlDateRange.Items.Clear()
        Select Case mDisplayAsDropDown

            Case True
                Me._ddlDateRange.Items.Add(New ListItem(" ", " "))
                Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Current Month"), range.CurrentMonth))
                Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last Month"), range.LastMonth))
                Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last 3 Months"), range.Last3Months))
                Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last 12 Months"), range.Last12Months))
                Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last Year"), range.LastYear))
                Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Year To Date"), range.YearToDate))
                Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("1st Quarter"), range.FirstQuarter, mShowFutureDates))
                Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("2nd Quarter"), range.SecondQuarter, mShowFutureDates))
                Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Entered Last 7 Days"), range.EnteredLast7Days, mShowFutureDates))
                Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("3rd Quarter"), range.ThirdQuarter, mShowFutureDates))
                Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("4th Quarter"), range.FourthQuarter, mShowFutureDates))
                Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("T Minus 15 Months"), range.TMinus15MthToTMinus3Mth))
                Me._ddlDateRange.Visible = True
                Me._rblDateRange.Visible = False

            Case False
                Me._rblDateRange.Items.Add(New ListItem(" ", " "))
                Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Current Month"), range.CurrentMonth))
                Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last Month"), range.LastMonth))
                Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last 3 Months"), range.Last3Months))
                Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last 12 Months"), range.Last12Months))
                Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last Year"), range.LastYear))
                Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Year To Date"), range.YearToDate))
                Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("1st Quarter"), range.FirstQuarter))
                Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("2nd Quarter"), range.SecondQuarter))
                Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Entered Last 7 Days"), range.EnteredLast7Days))
                Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("3rd Quarter"), range.ThirdQuarter))
                Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("4th Quarter"), range.FourthQuarter))
                Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Today Minus 15 Months to Today Minus 3 Months"), range.TMinus15MthToTMinus3Mth))
                Me._ddlDateRange.Visible = False
                Me._rblDateRange.Visible = True
        End Select

    End Sub

    Protected Sub _rblDateRange_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _rblDateRange.SelectedIndexChanged
        If _rblDateRange.SelectedIndex >= 0 Then
            DateRangeChange(_rblDateRange.SelectedValue)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            PopulateDateRange()
            Me._StartEndCalendar.CalendarCommitScript = Me.ClearSelection
            If Not Page.ClientScript.IsClientScriptBlockRegistered(Page.GetType, "DateRange") Then Page.ClientScript.RegisterClientScriptBlock(Page.GetType, "DateRange", GetDateRangeJS(), True)
            'ScriptManager.RegisterClientScriptBlock(Me.Page, Page.GetType, "DateRange", GetDateRangeJS(), True)
            Dim _txtStartDateId As String = Me._StartEndCalendar.FindControl("_txtStartDate").clientid '.UniqueID
            Dim _txtEndDateId As String = Me._StartEndCalendar.FindControl("_txtEndDate").clientid '.
            Dim _txtStartDateValueId As String = Me._StartEndCalendar.FindControl("_hdfStartDateValue").clientid '.
            Dim _txtEndDateValueId As String = Me._StartEndCalendar.FindControl("_hdfEndDateValue").clientid '.
            Me._rblDateRange.Attributes.Add("onclick", "ChangeDate('" & _txtStartDateId & "','" & _txtEndDateId & "',this.value,'" & _txtStartDateValueId & "','" & _txtEndDateValueId & "');return false;")
            Me._ddlDateRange.Attributes.Add("onchange", "ChangeDate('" & _txtStartDateId & "','" & _txtEndDateId & "',this.value,'" & _txtStartDateValueId & "','" & _txtEndDateValueId & "');return false;")
            'If Request.Form(Me._ddlDateRange.UniqueID) IsNot Nothing Then 
            '    If Request.Form(Me._ddlDateRange.UniqueID).Length > 0 And IsNumeric(Request.Form(Me._ddlDateRange.UniqueID)) Then
            '        Me.SetDateRange(Request.Form(Me._ddlDateRange.UniqueID))
            '    Else
            '        Me._ddlDateRange.SelectedIndex = -1
            '    End If
            'End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Protected Sub _StartEndCalendar_CalendarUpdated() Handles _StartEndCalendar.CalendarUpdated
        mStartDate.Value = _StartEndCalendar.StartDate
        mEndDate.Value = _StartEndCalendar.EndDate
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        If RI.SharedFunctions.CausedPostBack(Me._rblDateRange.ID) Or RI.SharedFunctions.CausedPostBack(Me._ddlDateRange.ID) Then
            RaiseEvent DateRangeChanged()
        End If
    End Sub

    Protected Sub _StartEndCalendar_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles _StartEndCalendar.Load
        mStartDate.Value = _StartEndCalendar.StartDate
        mEndDate.Value = _StartEndCalendar.EndDate
        If Me.DisplayAsDropDown = False Then
            Dim sb As New StringBuilder
            sb.Append("function clearButtons(){")
            sb.Append(" for (i=0; i < ")
            sb.Append(Me._rblDateRange.Items.Count)
            sb.Append("; i++) {")
            sb.Append("var buttonGroup=document.getElementById('")
            sb.Append(Me._rblDateRange.ClientID.ToString)
            sb.Append("_'+i);")
            sb.Append("if (buttonGroup.checked == true) {buttonGroup.checked = false}}} ")
            If Not Page.ClientScript.IsClientScriptBlockRegistered(Page.GetType, "clearButtons") Then Page.ClientScript.RegisterClientScriptBlock(Page.GetType, "clearButtons", sb.ToString, True)
        Else
            Dim sb As New StringBuilder
            sb.Append("function clearButtons(){")
            sb.Append(" for (i=0; i < ")
            sb.Append(Me._ddlDateRange.Items.Count)
            sb.Append("; i++) {")
            sb.Append("var buttonGroup=document.getElementById('")
            sb.Append(Me._ddlDateRange.ClientID.ToString)
            sb.Append("');")
            sb.Append("if (buttonGroup!=null){buttonGroup.selectedIndex=0;}}} ")
            If Not Page.ClientScript.IsClientScriptBlockRegistered(Page.GetType, "clearButtons2") Then Page.ClientScript.RegisterClientScriptBlock(Page.GetType, "clearButtons2", sb.ToString, True)
        End If
    End Sub

    Protected Sub _ddlDateRange_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlDateRange.SelectedIndexChanged
        If _ddlDateRange.SelectedIndex > 0 Then
            DateRangeChange(_ddlDateRange.SelectedValue)
        End If
    End Sub
    Private Function GetDateRangeJS() As String
        Dim sb As New StringBuilder
        sb.Append("function ChangeDate(startDate,endDate,range,startDateValue,endDateValue)<<startDate = document.getElementById(startDate);endDate = document.getElementById(endDate);startDateValue=document.getElementById(startDateValue);endDateValue=document.getElementById(endDateValue);")
        sb.Append("if (startDate!=null && endDate!=null)<<")
        sb.Append("switch (range)<< ")
        sb.Append("case '1':startDate.value={0};endDate.value={1};startDateValue.value={30};endDateValue.value={31};break;")
        sb.Append("case '2':startDate.value={2};endDate.value={3};startDateValue.value={32};endDateValue.value={33};break;")
        sb.Append("case '3':startDate.value={4};endDate.value={5};startDateValue.value={34};endDateValue.value={35};break;")
        sb.Append("case '4':startDate.value={6};endDate.value={7};startDateValue.value={36};endDateValue.value={37};break;")
        sb.Append("case '5':startDate.value={8};endDate.value={9};startDateValue.value={38};endDateValue.value={39};break;")
        sb.Append("case '6':startDate.value={10};endDate.value={11};startDateValue.value={40};endDateValue.value={41};break;")
        sb.Append("case '7':startDate.value={12};endDate.value={13};startDateValue.value={42};endDateValue.value={43};break;")
        sb.Append("case '8':startDate.value={14};endDate.value={15};startDateValue.value={44};endDateValue.value={45};break;")
        sb.Append("case '9':startDate.value={16};endDate.value={17};startDateValue.value={46};endDateValue.value={47};break;")
        sb.Append("case '10':startDate.value={18};endDate.value={19};startDateValue.value={48};endDateValue.value={49};break;")
        sb.Append("case '11':startDate.value={20};endDate.value={21};startDateValue.value={50};endDateValue.value={51};break;")
        sb.Append("case '12':startDate.value={22};endDate.value={23};startDateValue.value={52};endDateValue.value={53};break;")
        sb.Append("case '13':startDate.value={24};endDate.value={25};startDateValue.value={54};endDateValue.value={55};break;")
        sb.Append("case '14':startDate.value={26};endDate.value={27};startDateValue.value={56};endDateValue.value={57};break;")
        sb.Append("default:startDate.value={28};endDate.value={29};startDateValue.value={58};endDateValue.value={59};break;")
        sb.Append(">>")
        sb.Append(">>>>")
        Dim js As String = sb.ToString

        Dim jsArgs As String = _
                "'" & DateSerial(Year(Now), Month(Now) - 1, 1) & "','" & DateSerial(Year(Now), Month(Now), 0) & _
                "','" & DateSerial(Year(Now), Month(Now) - 3, 1) & "','" & DateSerial(Year(Now), Month(Now), 0) & _
                "','" & DateSerial(Year(Now) - 1, 1, 1) & "','" & Now.ToShortDateString & _
                "','" & DateSerial(Year(Now), 1, 1) & "','" & Now.ToShortDateString & _
                "','" & DateSerial(Year(Now), 1, 1) & "','" & DateSerial(Year(Now), 4, 0) & _
                "','" & DateSerial(Year(Now), 4, 1) & "','" & DateSerial(Year(Now), 7, 0) & _
                "','" & DateSerial(Year(Now), 7, 1) & "','" & DateSerial(Year(Now), 10, 0) & _
                "','" & DateSerial(Year(Now), 10, 1) & "','" & DateSerial(Year(Now), 13, 0) & _
                "','" & Now.AddDays(-7).ToShortDateString & "','" & Now.ToShortDateString & _
                "','" & DateSerial(Year(Now) - 1, 1, 1) & "','" & DateSerial(Year(Now) - 1, 12, 31) & _
                "','" & DateSerial(Year(Now), 1, 1) & "','" & DateSerial(Year(Now), 12, 31) & _
                "','" & Now.AddYears(-1).ToShortDateString & "','" & Now.ToShortDateString & _
                "','" & DateSerial(Year(Now), Month(Now), 1) & "','" & DateSerial(Year(Now), Month(Now), 1).AddMonths(1).AddDays(-1) & _
                "','" & Now.AddMonths(-15).ToShortDateString & "','" & Now.AddMonths(-3).ToShortDateString & _
                "','',''," & _
                "'" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), Month(Now) - 1, 1), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), Month(Now), 0), False) & _
                 "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), Month(Now) - 3, 1), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), Month(Now), 0), False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now) - 1, 1, 1), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(Now.ToShortDateString, False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 1, 1), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(Now.ToShortDateString, False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 1, 1), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 4, 0), False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 4, 1), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 7, 0), False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 7, 1), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 10, 0), False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 10, 1), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 13, 0), False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(Now.AddDays(-7).ToShortDateString, False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(Now.ToShortDateString, False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now) - 1, 1, 1), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now) - 1, 12, 31), False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 1, 1), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 12, 31), False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(Now.AddYears(-1).ToShortDateString, False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(Now.ToShortDateString, False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), Month(Now), 1), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), Month(Now), 1).AddMonths(1).AddDays(-1), False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(Now.AddMonths(-15), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(Now.AddMonths(-3), False) & _
                "','',''"

        Dim args As Object() = Split(jsArgs, ",")

        js = String.Format(js, args)
        js = js.Replace("<<", "{")
        js = js.Replace(">>", "}")
        Return js
        'Case range.CurrentMonth
        'mStartDate = DateSerial(Year(todaysDate), Month(todaysDate), 1)
        'mEndDate = DateSerial(Year(todaysDate), Month(todaysDate), 1).AddMonths(1).AddDays(-1)
    End Function
    Public Structure DateValue
        Public Text As String
        Public Value As String
    End Structure

End Class



