Imports System.Web
Imports System.Web.Services
Imports AjaxControlToolkit
Imports System.Web.Services.Protocols
Imports System.Collections.Generic
Imports System.Data
Imports Devart.Data.Oracle

<WebService(Namespace:="http://gpitasktracker.graphicpkg.com/WebServices/TaskTracker.asmx")>
<System.Web.Script.Services.ScriptService()> _
Public Class TaskTrackerHeader
    Inherits System.Web.Services.WebService
    ''' <summary>
    ''' Creates a new mtt task header record for the MOC
    ''' </summary>
    ''' <param name="Title">String - Title of the MOC</param>
    ''' <param name="ExtRef">String - SeqID or Number of source record</param>
    ''' <param name="ExtSource">String - MOC or Reliability Incident</param>
    ''' <param name="StartDate">String - Valid Date (i.e. 07/01/2007)</param>
    ''' <param name="SiteID">SiteID (i.e CT)</param>
    ''' <param name="BusinessUnit">String - Finished Products </param>
    ''' <param name="Line">String - None</param>
    ''' <param name="Description">String - Description of the MOC</param>
    ''' <param name="CreatedBy">String - User Name (i.e. MJPOPE)</param>
    ''' <param name="CreatedDate">String - Valid Date (i.e. 07/01/2007)</param>
    ''' <returns>Returns a dataset that contains the new Task Header Seq ID</returns>
    ''' <remarks></remarks>
    <WebMethod(Description:="Creates a new mtt task header record.")> _
    Public Function CreateMTTTaskHeader(ByVal Title As String, ByVal ExtRef As String, ByVal ExtSource As String, ByVal StartDate As String, ByVal EndDate As String, ByVal SiteID As String, ByVal BusinessUnit As String, ByVal Line As String, ByVal Description As String, ByVal Type As String, ByVal Activity As String, ByVal CreatedBy As String, ByVal CreatedDate As String) As String
        Dim strTaskHeaderSeqID As String = ""
        'Dim paramCollection As New OracleParameterCollection
        'Dim param As New OracleParameter
        Dim startDateFmt, createdDateFmt, endDateFmt As DateTime

        Try

            If IsDate(StartDate) Then
                startDateFmt = FormatDateTime(StartDate, DateFormat.ShortDate)
            Else
                startDateFmt = FormatDateTime(Now(), DateFormat.ShortDate)
            End If
            If IsDate(EndDate) Then
                endDateFmt = FormatDateTime(EndDate, DateFormat.ShortDate)
            Else
                endDateFmt = FormatDateTime(Now(), DateFormat.ShortDate)
            End If
            If IsDate(CreatedDate) Then
                createdDateFmt = FormatDateTime(CreatedDate, DateFormat.ShortDate)
            Else
                createdDateFmt = FormatDateTime(Now(), DateFormat.ShortDate)
            End If

            'response.write("here")
            'MsgBox("before web ref")
            RI.SharedFunctions.InsertAuditRecord("before web ref", "RI:" & ExtRef & " " & Now.ToString)

            Dim ws As New TaskTrackerWS.TaskTracker
            ws.Credentials = New System.Net.NetworkCredential("james.butler", "!8450Millhaven", "na")


            'MsgBox("Create WEB ref")
            ''RI.SharedFunctions.InsertAuditRecord("created web ref", "created web ref")

            ws.UseDefaultCredentials = True
            'ws.Credentials = System.Net.CredentialCache.DefaultCredentials
            RI.SharedFunctions.InsertAuditRecord("TaskTracker- CreateMTTTaskHeader - Credentials", ws.Credentials.ToString)
            'MsgBox("got credentials")
            ws.PreAuthenticate = True
            ws.Timeout = -1
            Dim url As String
            url = ws.Url.ToString
            RI.SharedFunctions.InsertAuditRecord("TaskTracker:CreateMTTTaskHeader - Credentials", "RI:" & ExtRef & " " & ws.Credentials.ToString & " " & Now.ToString)
            'MsgBox("PreAuthenticate")
            strTaskHeaderSeqID = ws.CreateMTTTaskHeader(Title, ExtRef, ExtSource, startDateFmt.ToString, endDateFmt.ToString, SiteID, BusinessUnit, Line, Description, Type, Activity, CreatedBy, createdDateFmt.ToString)
            RI.SharedFunctions.InsertAuditRecord("TaskTracker:CreateMTTTaskHeader - ws.CreateMTTTaskHeader", "RI:" & ExtRef & " " & "strTaskHeaderSeqID #" & strTaskHeaderSeqID & " " & Now.ToString)


            'strTaskHeaderSeqID = ws.TestIRISTaskHeader(CreatedBy)
            '' RI.SharedFunctions.InsertAuditRecord("create successful", "create successful")


        Catch ex As Exception
            RI.SharedFunctions.InsertAuditRecord(Now.ToString & " TaskTracker:CreateMTTTaskHeader - Error ", "RI:" & ExtRef & " " & ex.ToString)
            Throw New Exception("Create MTT Task Header Web Service Error", ex.InnerException)
            RI.SharedFunctions.HandleError()
        Finally
            CreateMTTTaskHeader = strTaskHeaderSeqID
        End Try
    End Function


    '  ''' <summary>
    '  ''' Creates a new mtt task header record for the MOC
    '  ''' </summary>
    '  ''' <param name="Title">String - Title of the MOC</param>
    '  ''' <param name="ExtRef">String - SeqID or Number of source record</param>
    '  ''' <param name="ExtSource">String - MOC or Reliability Incident</param>
    '  ''' <param name="StartDate">String - Valid Date (i.e. 07/01/2007)</param>
    '  ''' <param name="SiteID">SiteID (i.e CT)</param>
    '  ''' <param name="BusinessUnit">String - Finished Products </param>
    '  ''' <param name="Line">String - None</param>
    '  ''' <param name="Description">String - Description of the MOC</param>
    '  ''' <param name="CreatedBy">String - User Name (i.e. MJPOPE)</param>
    '  ''' <param name="CreatedDate">String - Valid Date (i.e. 07/01/2007)</param>
    '  ''' <returns>Returns a dataset that contains the new Task Header Seq ID</returns>
    '  ''' <remarks></remarks>
    '  <WebMethod(Description:="Creates a new mtt task header record.")> _
    '  Public Function UpdateIRISTaskHeader(ByVal Title As String, ByVal ExtRef As String, ByVal ExtSource As String, ByVal StartDate As String, ByVal EndDate As String, ByVal SiteID As String, ByVal BusinessUnit As String, ByVal Line As String, ByVal Description As String, ByVal Type As String, ByVal Activity As String, ByVal CreatedBy As String, ByVal CreatedDate As String) As String
    '      Dim strTaskHeaderSeqID As String = ""
    '      'Dim paramCollection As New OracleParameterCollection
    '      'Dim param As New OracleParameter
    '      Dim startDateFmt, createdDateFmt, endDateFmt As DateTime

    '      Try

    '          If IsDate(StartDate) Then
    '              startDateFmt = FormatDateTime(StartDate, DateFormat.ShortDate)
    '          Else
    '              startDateFmt = FormatDateTime(Now(), DateFormat.ShortDate)
    '          End If
    '          If IsDate(EndDate) Then
    '              endDateFmt = FormatDateTime(EndDate, DateFormat.ShortDate)
    '          Else
    '              endDateFmt = FormatDateTime(Now(), DateFormat.ShortDate)
    '          End If
    '          If IsDate(CreatedDate) Then
    '              createdDateFmt = FormatDateTime(CreatedDate, DateFormat.ShortDate)
    '          Else
    '              createdDateFmt = FormatDateTime(Now(), DateFormat.ShortDate)
    '          End If

    '          Dim createdByUserName() As String = CreatedBy.Split("\")
    '          If createdByUserName.Length > 1 Then
    '              CreatedBy = createdByUserName(0)
    '          End If

    '          Dim ws As New TaskTrackerWS.TaskTracker
    '          ws.Credentials = System.Net.CredentialCache.DefaultCredentials
    '          ws.PreAuthenticate = True
    '          strTaskHeaderSeqID = ws.CreateMTTTaskHeader(Title, ExtRef, ExtSource, startDateFmt, endDateFmt, SiteID, BusinessUnit, Line, Description, Type, Activity, CreatedBy, createdDateFmt)

    '      Catch ex As Exception
    '          Throw New Exception("Create MTT Task Header Web Service Error", ex.InnerException)
    '          RI.SharedFunctions.HandleError()
    '      Finally
    '          UpdateIRISTaskHeader = strTaskHeaderSeqID
    '      End Try
    '  End Function

    '  <WebMethod(Description:="Creates the default mtt task header for IRIS.")> _
    'Public Function StartIRISTaskHeader(ByVal createdBy As String) As String
    '      Dim strTaskHeaderSeqID As String = ""
    '      'Dim paramCollection As New OracleParameterCollection
    '      'Dim param As New OracleParameter
    '      Dim startDateFmt, createdDateFmt, endDateFmt As DateTime
    '      Dim Title As String = "New IRIS Event"
    '      Dim ExtRef As String = "-1"
    '      Dim ExtSource As String = "IRIS"
    '      Dim StartDate As String = Now.ToShortDateString
    '      Dim EndDate As String = Now.ToShortDateString
    '      Dim SiteID As String = "Memphis Towers"
    '      Dim BusinessUnit As String = "Millwide - Safety"
    '      Dim Line As String = ""
    '      Dim Description As String = ""
    '      Dim Type As String = "Health & Safety"
    '      Dim Activity As String = "Incident"
    '      Dim CreatedDate As String = Now.ToShortDateString
    '      Try

    '          If IsDate(StartDate) Then
    '              startDateFmt = FormatDateTime(StartDate, DateFormat.ShortDate)
    '          Else
    '              startDateFmt = FormatDateTime(Now(), DateFormat.ShortDate)
    '          End If
    '          If IsDate(EndDate) Then
    '              endDateFmt = FormatDateTime(EndDate, DateFormat.ShortDate)
    '          Else
    '              endDateFmt = FormatDateTime(Now(), DateFormat.ShortDate)
    '          End If
    '          If IsDate(CreatedDate) Then
    '              createdDateFmt = FormatDateTime(CreatedDate, DateFormat.ShortDate)
    '          Else
    '              createdDateFmt = FormatDateTime(Now(), DateFormat.ShortDate)
    '          End If

    '          Dim createdByUserName() As String = createdBy.Split("\")
    '          If createdByUserName.Length > 1 Then
    '              createdBy = createdByUserName(0)
    '          End If
    '          Dim ws As New TaskTrackerWS.TaskTracker
    '          ws.Credentials = System.Net.CredentialCache.DefaultCredentials
    '          ws.PreAuthenticate = True
    '          strTaskHeaderSeqID = ws.CreateMTTTaskHeader(Title, ExtRef, ExtSource, startDateFmt, endDateFmt, SiteID, BusinessUnit, Line, Description, Type, Activity, createdBy, createdDateFmt)

    '      Catch ex As Exception
    '          Throw New Exception("Create MTT Task Header Web Service Error", ex.InnerException)
    '          RI.SharedFunctions.HandleError()
    '      Finally
    '          StartIRISTaskHeader = strTaskHeaderSeqID
    '      End Try
    '  End Function
End Class
