'Option Explicit On
'Option Strict On

Partial Class RI_User_Controls_Common_ucStartEndDates
    Inherits System.Web.UI.UserControl

    Public Event DateRangeChanged()
    Private mStartDate As DateValue
    Private mEndDate As DateValue
    Private mSelectedDateRange As range

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

    'Public ReadOnly Property StartDateText() As String
    '    Get
    '        Return IP.Bids.Localization.DateTime.GetLocalizedDateTime(mStartDate.Value, System.Threading.Thread.CurrentThread.CurrentCulture.Name, "G") & IP.Bids.Localization.DateTime.GetLocalizedDateTime(mStartDate.Value, System.Threading.Thread.CurrentThread.CurrentCulture.Name, "t")
    '    End Get
    'End Property

    Public ReadOnly Property EndDateText() As String
        Get
            Return IP.Bids.Localization.DateTime.GetLocalizedDateTime(mEndDate.Value, System.Threading.Thread.CurrentThread.CurrentCulture.Name, "G")
        End Get
    End Property
    Public Property StartDate() As String
        Get
            If _tbFromDate.Text.Length > 0 Then
                Dim dateValue = _tbFromDate.Text.Split(CChar(" "))
            End If
            If IsDate(Me._tbFromDate.Text) Then
                Return CDate(Me._tbFromDate.Text)
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            'If Me._tbFromDate.Text <> CStr(Value) AndAlso Value.Length > 0 Then
            '_tbFromDate.Text = (IP.Bids.Localization.DateTime.GetLocalizedDateTime(Value, DirectCast(System.Threading.Thread.CurrentThread.CurrentCulture, System.Globalization.CultureInfo).Name.ToLower))
            'Else
            Me._tbFromDate.Text = CStr(Value)
            'End If

        End Set
    End Property
 
    Public Property EndDate() As String
        Get
            If IsDate(Me._tbToDate.Text) Then
                Return CDate(Me._tbToDate.Text)
            Else
                Return ""
            End If

        End Get
        Set(ByVal Value As String)
            If Me._tbToDate.Text <> CStr(Value) AndAlso Value.Length > 0 Then
                _tbToDate.Text = (IP.Bids.Localization.DateTime.GetLocalizedDateTime(Value, DirectCast(System.Threading.Thread.CurrentThread.CurrentCulture, System.Globalization.CultureInfo).Name.ToLower))
            Else
                _tbToDate.Text = CStr(Value)
            End If
        End Set
    End Property

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If _ddlDateRange.Items.Count = 0 Then
            If Not Page.IsPostBack Then
                Me.SetDateRange(Me.SelectedDateRange)
            Else
                Me.SetDateRange(-1)
            End If
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me._ddlDateRange.Items.Count = 0 Then
            PopulateDateRange()
        End If
        If Not Page.IsPostBack Then
            'If Me._tbFromDate.Text.Length = 0 And Me._tbToDate.Text.Length = 0 Then
            'StartDate = Now.ToShortDateString
            'EndDate = Now.ToShortDateString
            'End If
            'Else

            '    If Request.Form(_tbFromDate.UniqueID) IsNot Nothing And Request.Form(_tbToDate.UniqueID) IsNot Nothing Then
            '        StartDate = Request.Form(_tbFromDate.UniqueID)
            '        EndDate = Request.Form(_tbToDate.UniqueID)
            '    ElseIf Request.Form(_tbFromDate.UniqueID) IsNot Nothing Then
            '        StartDate = Request.Form(_tbFromDate.UniqueID)
            '    ElseIf Request.Form(_tbToDate.UniqueID) IsNot Nothing Then
            '        EndDate = Request.Form(_tbToDate.UniqueID)
            '    End If
            'End If
        End If
        If Not Page.ClientScript.IsClientScriptBlockRegistered(Page.GetType, "JQDateRange") Then
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType, "JQDateRange", GetDateRangeJS(), True)
        End If
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "PerformanceDateTimePicker", "$('.PerformanceDateTimePicker').datetimepicker();", True)
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "PerformanceDateTimePicker2", "$('.PerformanceDateTimePicker2').datetimepicker();", True)
        ScriptManager.RegisterStartupScript(Me.Page, Page.GetType, "JQDateRange_" & Me.ClientID, AddJQueryDatePickerJS(), True)

        _ddlDateRange.Attributes.Add("onchange", "ChangeDateRange('" & Me._tbFromDate.ClientID & "','" & _tbToDate.ClientID & "',this.value);")
        'Page.ClientScript.RegisterStartupScript(Page.GetType, "JQDateRange_" & Me.ID, AddJQueryDatePickerJS(), True)
        
    End Sub
    Private Function GetDateRangeJS() As String
        Dim sb As New StringBuilder
        sb.Append("function ChangeDateRange(startDate,endDate,range)<<startDate = document.getElementById(startDate);endDate = document.getElementById(endDate);")
        sb.Append("if (startDate!=null && endDate!=null)<<")
        sb.Append("switch (range)<< ")
        sb.Append("case '1':startDate.value={0};endDate.value={1};break;")
        sb.Append("case '2':startDate.value={2};endDate.value={3};break;")
        sb.Append("case '3':startDate.value={4};endDate.value={5};break;")
        sb.Append("case '4':startDate.value={6};endDate.value={7};break;")
        sb.Append("case '5':startDate.value={8};endDate.value={9};break;")
        sb.Append("case '6':startDate.value={10};endDate.value={11};break;")
        sb.Append("case '7':startDate.value={12};endDate.value={13};break;")
        sb.Append("case '8':startDate.value={14};endDate.value={15};break;")
        sb.Append("case '9':startDate.value={16};endDate.value={17};break;")
        sb.Append("case '10':startDate.value={18};endDate.value={19};break;")
        sb.Append("case '11':startDate.value={20};endDate.value={21};break;")
        sb.Append("case '12':startDate.value={22};endDate.value={23};break;")
        sb.Append("case '13':startDate.value={24};endDate.value={25};break;")
        sb.Append("case '14':startDate.value={26};endDate.value={27};break;")
        sb.Append("case '15':startDate.value={28};endDate.value={29};break;")
        sb.Append("default:startDate.value={30};endDate.value={31};break;")
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
                "','" & Now.AddDays(-7).ToShortDateString & "','" & Now() & _
                "','" & DateSerial(Year(Now) - 1, 1, 1) & "','" & DateSerial(Year(Now) - 1, 12, 31) & _
                "','" & DateSerial(Year(Now), 1, 1) & "','" & DateSerial(Year(Now), 12, 31) & _
                "','" & Now.AddYears(-1).ToShortDateString & "','" & Now.ToShortDateString & _
                "','" & DateSerial(Year(Now), Month(Now), 1) & "','" & DateSerial(Year(Now), Month(Now), 1).AddMonths(1).AddDays(-1) & _
                "','" & Format(Now.AddHours(-8), "MM/dd/yyyy hh:mm") & "','" & Format(Now, "MM/dd/yyyy hh:mm") & _
                "','" & Format(Now.AddHours(-16), "MM/dd/yyyy hh:mm") & "','" & Format(Now, "MM/dd/yyyy hh:mm") & _
                "','" & Format(Now.AddHours(-24), "MM/dd/yyyy hh:mm") & "','" & Format(Now, "MM/dd/yyyy hh:mm") & _
                "','',''," & _
                "'" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), Month(Now) - 1, 1), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), Month(Now), 0), False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 1, 1), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(CDate(Now.ToShortDateString), False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 1, 1), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 4, 0), False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 4, 1), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 7, 0), False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 7, 1), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 10, 0), False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 10, 1), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 13, 0), False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(CDate(Now.AddDays(-7).ToShortDateString), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish((Now), False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now) - 1, 1, 1), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now) - 1, 12, 31), False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 1, 1), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), 12, 31), False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(CDate(Now.AddYears(-1).ToShortDateString), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(CDate(Now.ToShortDateString), False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), Month(Now), 1), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(DateSerial(Year(Now), Month(Now), 1).AddMonths(1).AddDays(-1), False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(Now.AddHours(-8), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(Now, False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(Now.AddHours(-16), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(Now, False) & _
                "','" & RI.SharedFunctions.FormatDateTimeToEnglish(Now.AddHours(-24), False) & "','" & RI.SharedFunctions.FormatDateTimeToEnglish(Now, False) & _
                "','',''"

        Dim args As Object() = Split(jsArgs, ",")

        js = String.Format(js, args)
        js = js.Replace("<<", "{")
        js = js.Replace(">>", "}")
        Return js

    End Function


    Private Function AddJQueryDatePickerJS() As String
        Dim sbDatePicker As New StringBuilder

        With sbDatePicker
            .Append("$(function() {$( "".selector"" ).datetimepicker({});")
            .Append(" var dates = $('input[type=text][id*=" & Me._tbFromDate.ClientID & "],input[type=text][id*=" & Me._tbToDate.ClientID & " ]').datetimepicker({	")
            .Append("		changeMonth: true,")
            '.Append("		ampm: true,")
            '    .Append("		showButtonPanel: false,")
            '    .Append("       selectOtherMonths: true,")
            '    .Append("		buttonImageOnly: false,")
            '    .Append("		minDate:new Date(2007, 1 - 1, 1),")
            '    .Append("		maxDate:new Date(2030, 1 - 1, 1),")
            .Append("		onSelect: function( selectedDate ) {")
            .Append("		    var dateFrom = dates[0].id;")
            '.Append("           var dateTo = dates[0].id;")
            '.Append("			dateFrom.datepicker( ""option"", ""maxDate"", selectedDate );")
            '.Append("			dateTo.datepicker( ""option"", ""minDate"", selectedDate );")
            .Append("           var option = this.id == dateFrom ? ""minDate"" : ""maxDate"",")
            .Append("				instance = $( this ).data( ""datepicker"" );")
            .Append("				date = $.datepicker.parseDate(")
            .Append("					instance.settings.dateFormat ||")
            .Append("					$.datepicker._defaults.dateFormat,")
            .Append("					selectedDate, instance.settings );")
            .Append("			dates.not( this ).datepicker( ""option"", option, date );")
            .Append("		}")
            .Append("	});")
            .Append("});")

        End With

        Return sbDatePicker.ToString
    End Function

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
        Last8Hours = 14
        Last16Hours = 15
        Last24Hours = 16
    End Enum
    Private Sub DateRangeChange(ByVal selectedValue As range)
        SetDateRange(selectedValue)
    End Sub
    Private Sub SetDateRange(ByVal dtRange As range)
        Try
            'Dim todaysDate As Date = Format(Now, "MM/dd/yyyy HH:mm")
            Dim todaysDate As Date = Now()
            PopulateDateRange()

            If Me._ddlDateRange.Items.FindByValue(CStr(dtRange)) IsNot Nothing Then
                Me._ddlDateRange.ClearSelection()
                Me._ddlDateRange.Items.FindByValue(CStr(dtRange)).Selected = True
            End If

            Select Case dtRange
                Case range.Last8Hours
                    mStartDate.Value = todaysDate.AddHours(-8)
                    mEndDate.Value = todaysDate
                Case range.Last16Hours
                    mStartDate.Value = todaysDate.AddHours(-16)
                    mEndDate.Value = todaysDate
                Case range.Last24Hours
                    mStartDate.Value = todaysDate.AddHours(-24)
                    mEndDate.Value = todaysDate
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
                    mEndDate.Value = todaysDate
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
                Case Else
                    mStartDate.Value = Nothing
                    mEndDate.Value = Nothing
            End Select
            If mStartDate.Value <> Nothing And mEndDate.Value <> Nothing Then
                StartDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(mStartDate.Value, "EN-US")
                EndDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(mEndDate.Value, "EN-US")
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub
    Protected Sub _ddlDateRange_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlDateRange.SelectedIndexChanged
        If _ddlDateRange.SelectedIndex > 0 Then
            DateRangeChange(CType(_ddlDateRange.SelectedValue, range))
        End If
    End Sub
    Public Property SelectedDateRange() As range
        Get
            Return mSelectedDateRange
        End Get
        Set(ByVal value As range)
            mSelectedDateRange = value
            Me.SetDateRange(value)
        End Set
    End Property

    Private Sub PopulateDateRange()
        Dim iploc As New IP.Bids.Localization.WebLocalization()
        Me._ddlDateRange.Items.Clear()

        Me._ddlDateRange.Items.Add(New ListItem(" ", " "))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last 8 Hours"), CStr(range.Last8Hours)))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last 16 Hours"), CStr(range.Last16Hours)))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last 24 Hours"), CStr(range.Last24Hours)))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Entered Last 7 Days"), CStr(range.EnteredLast7Days)))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Current Month"), CStr(range.CurrentMonth)))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last Month"), CStr(range.LastMonth)))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last 3 Months"), CStr(range.Last3Months)))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last 12 Months"), CStr(range.Last12Months)))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last Year"), CStr(range.LastYear)))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Year To Date"), CStr(range.YearToDate)))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("1st Quarter"), CStr(range.FirstQuarter)))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("2nd Quarter"), CStr(range.SecondQuarter)))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("3rd Quarter"), CStr(range.ThirdQuarter)))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("4th Quarter"), CStr(range.FourthQuarter)))
        Me._ddlDateRange.Visible = True

    End Sub

    'Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
    '    If RI.SharedFunctions.CausedPostBack(Me._ddlDateRange.ID) Then
    '        RaiseEvent DateRangeChanged()
    '    End If
    'End Sub

    Public Structure DateValue
        Public Text As String
        Public Value As String
    End Structure

End Class
