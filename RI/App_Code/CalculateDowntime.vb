Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

''' <summary>
''' This is a web service that Calculates the difference between two dates
''' </summary>
''' <remarks></remarks>
<WebService(Namespace:="http://RI/")> _
<System.Web.Script.Services.ScriptService()> _
Public Class CalculateDowntime
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function Calculate(ByVal startDate As String, ByVal endDate As String) As String
        Dim diff As TimeSpan
        Dim dt As Decimal = 0

        If HttpContext.Current.Request.Cookies("SelectedCulture") IsNot Nothing Then
            RI.SharedFunctions.SetCulture(HttpContext.Current.Request.Cookies("SelectedCulture").Value)
        End If
        If IsDate(startDate) And IsDate(endDate) Then
            'If RI.SharedFunctions.isEnglishDate(startDate) And RI.SharedFunctions.isEnglishDate(endDate) Then
            If Date.Parse(startDate).Date <= Date.Parse(endDate).Date Then
                Dim endDt As DateTime = endDate
                Dim startDT As DateTime = startDate
                diff = endDt.Subtract(startDT)
                dt = CStr(Math.Round(diff.TotalMinutes / 60, 2))
            Else
                dt = CStr(0)
            End If
        ElseIf RI.SharedFunctions.isEnglishDate(startDate) And RI.SharedFunctions.isEnglishDate(endDate) Then
            Dim dtformatInfo As System.Globalization.DateTimeFormatInfo = System.Globalization.CultureInfo.CreateSpecificCulture("EN-US").DateTimeFormat

            If Date.Parse(startDate, dtformatInfo).Date <= Date.Parse(endDate, dtformatInfo).Date Then
                Dim endDt As DateTime = Date.Parse(endDate, dtformatInfo) '.Date
                Dim startDT As DateTime = Date.Parse(startDate, dtformatInfo) '.Date
                diff = endDt.Subtract(startDT)
                dt = CStr(Math.Round(diff.TotalMinutes / 60, 2))
            Else
                dt = CStr(0)
            End If

        End If
        dt = CDec(dt).ToString
        Return IP.Bids.Localization.Numbers.GetLocalizedNumber(dt)
    End Function
End Class
