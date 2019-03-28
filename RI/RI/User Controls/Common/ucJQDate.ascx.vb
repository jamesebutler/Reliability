
Option Explicit On
Option Strict On
Imports Resources

Partial Class RI_User_Controls_Common_ucJQDate
    Inherits System.Web.UI.UserControl
    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    ScriptManager.RegisterStartupScript(Page, Page.GetType, "PerformanceDateTimePicker", "$('.PerformanceDateTimePicker').datetimepicker();", True)
    'End Sub


#Region "Custom Events"
    Public Event DateChanged(ByVal sender As TextBox)
Public Event TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
#End Region

#Region "Fields"
Private mShowFromDate As Boolean
Private mShowLabelsOnSameLine As Boolean
    Private mRequired As Boolean = True
    Private mEnabled As Boolean = True
    Private mAllowPostback As Boolean
    Private mMaxDate As Date = New Date(2030, 1, 1)
Private mMinDate As Date = New Date(1901, 1, 1)
#End Region

#Region "Public Properties"
Public Property MaxDate() As Date
    Get
        Return mMaxDate
    End Get
    Set(ByVal value As Date)
        mMaxDate = value
    End Set
End Property
Public Property MinDate() As Date
    Get
        Return mMinDate
    End Get
    Set(ByVal value As Date)
        mMinDate = value
    End Set
End Property
Public Property AllowPostBack() As Boolean
    Get
        Return mAllowPostback
    End Get
    Set(ByVal value As Boolean)
        mAllowPostback = value
    End Set
End Property

Public Property ShowLabelsOnSameLine() As Boolean
    Get
        Return mShowLabelsOnSameLine
    End Get
    Set(ByVal value As Boolean)
        mShowLabelsOnSameLine = value
    End Set
End Property
Public Property FromLabel() As String
    Get
        Return Me._lblDateFrom.Text
    End Get
    Set(ByVal value As String)
        Me._lblDateFrom.Text = value
    End Set
End Property
Public Property ShowFromDate() As Boolean
    Get
        Return mShowFromDate
    End Get
    Set(ByVal value As Boolean)
        mShowFromDate = value
    End Set
End Property
    Public Property Required() As Boolean
        Get
            Return mRequired
        End Get
        Set(ByVal value As Boolean)
            mRequired = value
        End Set
    End Property
    Public Property StartDate() As String
        Get
            If _txtDateFrom.Text.Length > 0 Then
                Dim dateValue = _txtDateFrom.Text.Split(CChar(" "))

            End If
            If IsDate(Me._txtDateFrom.Text) Then
                Return CDate(Me._txtDateFrom.Text).ToShortDateString
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            If Me._txtDateFrom.Text <> CStr(Value) AndAlso Value.Length > 0 Then
                _txtDateFrom.Text = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Value, Threading.Thread.CurrentThread.CurrentCulture.Name, "d")
                '_txtDateFrom.Text = (IP.Bids.Localization.DateTime.GetLocalizedDateTime(Value, DirectCast(System.Threading.Thread.CurrentThread.CurrentCulture, System.Globalization.CultureInfo).Name.ToLower, IP.Bids.SharedFunctions.GetLocalizedDateFormat))
                RaiseEvent TextChanged(Me._txtDateFrom, Nothing)
            Else
                Me._txtDateFrom.Text = CStr(Value)
            End If

        End Set
    End Property

    Public Property Enabled() As Boolean
    Get
        Return mEnabled
    End Get
    Set(ByVal value As Boolean)
        mEnabled = value
    End Set
End Property

Public ReadOnly Property StartDateUniqueId() As String
    Get
        Return _txtDateFrom.UniqueID
    End Get
End Property

#End Region

Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Dim sm As ScriptManager = ScriptManager.GetCurrent(Page)
    Dim lang As String = System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToLower
        'If ShowFromDate Then
        If sm.IsInAsyncPostBack Then
            ScriptManager.RegisterStartupScript(Me.Page, Page.GetType, "JQDateRange_" & Me.ClientID, AddJQuerySingleDatePickerJS(), True)
            ScriptManager.RegisterStartupScript(Me.Page, Page.GetType, "JQDate_" & Me.ClientID & "_" & lang, GetLocalizeDateLabels(), True)
        Else
            If Not Page.ClientScript.IsStartupScriptRegistered(Page.GetType, "JQDateRange_" & Me.ClientID) Then
                Page.ClientScript.RegisterStartupScript(Page.GetType, "JQDateRange_" & Me.ClientID, AddJQuerySingleDatePickerJS(), True)
            End If
            Page.ClientScript.RegisterStartupScript(Page.GetType, "JQDate_" & Me.ClientID & "_" & lang, GetLocalizeDateLabels(), True)
        End If

            Me.ShowLabelsOnSameLine = True

        'End If

        If Page.IsPostBack Then

            StartDate = Request.Form(_txtDateFrom.UniqueID)
        End If

        If Required = True Then
            Me._lblRequiredField.Visible = True
        Else
            Me._lblRequiredField.Visible = False
        End If

        If ShowLabelsOnSameLine Then
        Me._lineBreakFrom.Visible = False
    Else
        Me._lineBreakFrom.Visible = True
    End If


        Try

        Me._txtDateFrom.ReadOnly = True

    Catch
        Throw
    End Try
End Sub

'Private Function AddJQueryDatePickerJS() As String
'    Dim sbDatePicker As New StringBuilder
'    Dim lang As String = ""

'    If Page IsNot Nothing AndAlso Page.Culture IsNot Nothing AndAlso Page.Culture.Length > 0 Then
'        Select Case DirectCast(System.Threading.Thread.CurrentThread.CurrentCulture, System.Globalization.CultureInfo).Name.ToLower
'            Case "en-us"
'                lang = "" 'Default
'            Case "ru-ru"
'                lang = "ru"
'            Case "fr-fr"
'                lang = "fr"
'            Case "pt-br"
'                lang = "pt-br"
'            Case Else
'                lang = DirectCast(System.Threading.Thread.CurrentThread.CurrentCulture, System.Globalization.CultureInfo).Name.ToLower
'        End Select
'    End If
'    With sbDatePicker
'        .Append("$(function() {$( "".selector"" ).datepicker({});")
'        .Append(" var dates = $('input[type=text][id*=" & Me._txtDateFrom.ClientID & "],input[type=text][id*=" & Me._txtDateTo.ClientID & " ]').datepicker({	")
'        '.Append("		defaultDate: """",")
'        .Append("		defaultDate: ""+1w"",")
'        .Append("		changeMonth: true,")
'        .Append("		numberOfMonths: 1,")
'        .Append("		changeYear: true,")
'        .Append("		showOtherMonths: true,")
'        .Append("		showButtonPanel: false,")
'        .Append("       selectOtherMonths: true,")
'        .Append("		buttonImageOnly: false,")
'        .Append("		minDate:new Date(2007, 1 - 1, 1),")
'        .Append("		maxDate:new Date(2030, 1 - 1, 1),")
'        '.Append("		dateFormat:""M d, yy "",") 'MJP added
'        .Append("     altformat:""yy-mm-dd"",")
'        .Append("		dateFormat:""" & RI.SharedFunctions.GetLocalizedJQueryDateFormat & """,")
'        .Append("     regional: """ & lang & """,")
'        .Append("		onSelect: function( selectedDate ) {")
'        .Append("		    var dateFrom = dates[0].id;")
'        .Append("			var option = this.id == dateFrom ? ""minDate"" : ""maxDate"",")
'        .Append("				instance = $( this ).data( ""datepicker"" );")
'        .Append("				date = $.datepicker.parseDate(")
'        .Append("					instance.settings.dateFormat ||")
'        .Append("					$.datepicker._defaults.dateFormat,")
'        .Append("					selectedDate, instance.settings );")
'        .Append("			dates.not( this ).datepicker( ""option"", option, date );")
'        .Append("		}")
'        .Append("	});")
'        .Append("});")
'    End With
'    Return sbDatePicker.ToString
'End Function
Private Function AddJQuerySingleDatePickerJS(Optional ByVal maxDate As String = "1/1/2030") As String
    Dim sbDatePicker As New StringBuilder
    Dim jsMaxDate As String = String.Empty
    Dim jsMinDate As String = String.Empty
        Dim lang As String = ""
        Dim dtFormat As String = ""

        If Page IsNot Nothing AndAlso Page.Culture IsNot Nothing AndAlso Page.Culture.Length > 0 Then
        Select Case DirectCast(System.Threading.Thread.CurrentThread.CurrentCulture, System.Globalization.CultureInfo).Name.ToLower
            Case "en-us"
                    lang = "" 'Default
                    dtFormat = "mm/dd/yy"
                Case "fr-fr"
                    lang = "fr"
                    dtFormat = "dd/mm/yy"
                Case "pt-br"
                    lang = "pt-br"
                    dtFormat = "dd/mm/yy"
                Case "ru-ru"
                    lang = "ru"
                    dtFormat = "dd.mm.yy"
                Case "pl-pl"
                    lang = "pl"
                    dtFormat = "yy-mm-dd"
                Case Else
                    lang = DirectCast(System.Threading.Thread.CurrentThread.CurrentCulture, System.Globalization.CultureInfo).Name.ToLower
        End Select
    End If

    If IsDate(maxDate) Then
        Dim dt As Date = CDate(maxDate)
        jsMaxDate = "          maxDate:new Date(" & dt.Year & "," & dt.Month - 1 & "," & dt.Day & "),"
    End If
    If IsDate(MinDate) Then
        If MinDate.Year < Now.Year - 7 Then
            MinDate = New Date(Now.Year - 7, 1, 1)
        End If
        jsMinDate = "          minDate:new Date(" & MinDate.Year & "," & MinDate.Month - 1 & "," & MinDate.Day & "),"
    End If
    With sbDatePicker
        .Append("$(function() {")
        .Append("$( ""#" & Me._txtDateFrom.ClientID & """ ).datepicker( {")
        .Append("     changeYear: true,")
        .Append("		defaultDate: ""+1w"",")
        .Append("     numberOfMonths: 1,")
        .Append("     showButtonPanel: false,")
        .Append("     showOtherMonths: true,")
        .Append("     selectOtherMonths: true,")
        .Append("     regional: """ & lang & """,")
            .Append("     dateFormat:""" & dtFormat & """,")
            .Append("     altformat:""yy-mm-dd"",")
            '   .Append("	  dateFormat:""" & RI.SharedFunctions.GetLocalizedJQueryDateFormat & """,") 'MJP added
            .Append(jsMaxDate)
        .Append(jsMinDate)
        '.Append("	  maxDate:new Date(2030, 1 - 1, 1),")
        .Append("     changeMonth: true});")
        .Append("});")
    End With
    Return sbDatePicker.ToString
End Function

Private Function GetLocalizeDateLabels() As String
    Dim sb As New StringBuilder
    Dim lang As String = System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToLower
    Dim ipResources As New IP.Bids.Localization.WebLocalization()

    sb.AppendLine("jQuery(function($){")
    sb.AppendFormat("$.datepicker.regional['{0}'] = ", Left(lang, 2).ToLower)
    sb.Append("{")
        'sb.AppendFormat(" closeText:  '{0}',", ipResources.GetResourceValue(CloseText))
        'sb.AppendFormat(" prevText:   '&#x3c;{0}',", ipResources.GetResourceValue(PrevText))
        'sb.AppendFormat(" nextText:   '{0}&#x3e;',", ipResources.GetResourceValue(NextText))
        'sb.AppendFormat(" currentText:  '{0}',", ipResources.GetResourceValue(CurrentText))
        'sb.AppendFormat(" now:  '{0}',", ipResources.GetResourceValue("Now"))
        sb.AppendFormat(" monthNames:[{0}],", ConvertCollectionToDelimitedList(GetLocalizedMonthNames))
        sb.AppendFormat(" monthNamesShort:[{0}],", ConvertCollectionToDelimitedList(GetLocalizedMonthNames)) 'ConvertCollectionToDelimitedList(GetLocalizedShortMonthNames))
        sb.AppendFormat(" dayNames:[{0}],", ConvertCollectionToDelimitedList(GetLocalizedDayNames))
    sb.AppendFormat(" dayNamesShort:[{0}],", ConvertCollectionToDelimitedList(GetLocalizedShortDayNames))
    sb.AppendFormat(" dayNamesMin:[{0}],", ConvertCollectionToDelimitedList(GetLocalizedTwoCharDayNames))
    sb.Append(" weekHeader: 'Wk',")
        'sb.Append(" dateFormat: 'd M yy',")
        sb.Append(" firstDay: 1,")
    sb.Append(" isRTL: false,")
    sb.Append(" showMonthAfterYear: false,")
    sb.Append(" yearSuffix:  ''};")
    sb.AppendFormat(" 	$.datepicker.setDefaults($.datepicker.regional['{0}']);", Left(lang, 2).ToLower)
    sb.Append(" });")
    Return sb.ToString
End Function

Private Function GetLocalizedMonthNames() As SortedList
    Dim listOfMonths As New SortedList
    For i As Integer = 1 To 12
        listOfMonths.Add(i, MonthName(i))
    Next
    Return listOfMonths
End Function

Private Function GetLocalizedShortMonthNames() As SortedList
    Dim listOfMonths As New SortedList
    For i As Integer = 1 To 12
        listOfMonths.Add(i, MonthName(i, True))
    Next
    Return listOfMonths
End Function

Private Function GetLocalizedDayNames() As SortedList
    Dim listOfDays As New SortedList
    For i As Integer = 1 To 7
        listOfDays.Add(i, WeekdayName(i))
    Next
    Return listOfDays
End Function

Private Function GetLocalizedShortDayNames() As SortedList
    Dim listOfDays As New SortedList
    For i As Integer = 1 To 7
        listOfDays.Add(i, WeekdayName(i, True))
    Next
    Return listOfDays
End Function

Private Function GetLocalizedTwoCharDayNames() As SortedList
    Dim listOfDays As New SortedList
    For i As Integer = 1 To 7
        listOfDays.Add(i, Left(WeekdayName(i, True), 2))
    Next
    Return listOfDays
End Function

Private Function ConvertCollectionToDelimitedList(ByVal input As SortedList) As String
    Dim sb As New StringBuilder

    For Each item As DictionaryEntry In input
        If sb.Length > 0 Then sb.Append(",")
        sb.AppendFormat("'{0}'", item.Value)
    Next
    Return sb.ToString
End Function


Public Function ListOfClientIds() As Array
    Dim list() As String = {_txtDateFrom.ClientID}
    Return list
End Function

Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
    If Enabled = False Then
        Me._txtDateFrom.Enabled = False
    Else
        Me._txtDateFrom.Enabled = True
    End If
    Me._txtDateFrom.AutoPostBack = AllowPostBack
    If Me.Attributes.Item("onFocus") IsNot Nothing Then
        Me._txtDateFrom.Attributes.Add("onFocus", Me.Attributes.Item("onFocus"))
    End If
End Sub

Protected Sub _txtDateFrom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _txtDateFrom.TextChanged
    RaiseEvent TextChanged(sender, e)
End Sub

End Class
