Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Devart.Data.Oracle
Imports System.Data

<WebService(Namespace:="http://RI/", Description:="Insert or Update", Name:="RIUpdate Service")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class RIUpdate
    Inherits System.Web.Services.WebService

    ''' <summary>
    ''' Assigns the IRIS number to the specified RINumber
    ''' </summary>
    ''' <param name="IRISNumber">Long - A valid IRIS Number</param>
    ''' <param name="RINumber">Long - A valid RI Number</param>
    ''' <returns>Returns a dataset containing the status codes for the IRIS update
    ''' ReturnStatus 0 -- StatusDescription = "Incident has been updated"
    ''' ReturnStatus 200 -- StatusDescription = "RInumber does not exist in the database"
    ''' ReturnStatus 300 -- StatusDescription = "IRIS number already populated and will not be overlayed"
    ''' ReturnStatus Else -- StatusDescription = "Oracle Error"
    ''' </returns>
    ''' <remarks></remarks>
    <WebMethod(Description:="Assigns the IRIS number to the specified RINumber")> _
    Public Function UpdateIncident(ByVal IRISNumber As Long, ByVal RINumber As Long) As DataSet
        Dim ds As System.Data.DataSet = Nothing
        Dim paramCollection As New Devart.Data.Oracle.OracleParameterCollection
        Dim param As New Devart.Data.Oracle.OracleParameter

        Try

            If IRISNumber > 0 And RINumber > 0 Then
                param = New OracleParameter
                param.ParameterName = "in_RINumber"
                param.OracleDbType = OracleDbType.Number
                param.Direction = Data.ParameterDirection.Input
                param.Value = RINumber
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_IRISNumber"
                param.OracleDbType = OracleDbType.Number
                param.Direction = Data.ParameterDirection.Input
                param.Value = IRISNumber
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "out_status"
                param.OracleDbType = OracleDbType.Number
                param.Direction = Data.ParameterDirection.Output
                paramCollection.Add(param)

                Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.IRISUPDATE.UpdateIncident")
                Dim dt As New DataTable
                Dim dr As DataRow = dt.NewRow
                Dim StatusDescription As String = String.Empty
                Select Case returnStatus
                    Case 0
                        StatusDescription = "Incident has been updated"
                    Case 200
                        StatusDescription = "RInumber does not exist in the database"
                    Case 300
                        StatusDescription = "IRIS number already populated and will not be overlayed"
                    Case Else
                        StatusDescription = "Oracle Error"
                End Select

                dt.Columns.Add("RINumber")
                dt.Columns.Add("IRISNumber")
                dt.Columns.Add("Out_Status")
                dt.Columns.Add("Status_Description")
                dr.Item("RINumber") = RINumber
                dr.Item("IRISNumber") = IRISNumber
                dr.Item("Out_Status") = returnStatus

                dr.Item("Status_Description") = StatusDescription
                dt.Rows.Add(dr)
                ds = New DataSet
                ds.Tables.Add(dt)
            End If
        Catch ex As Exception
            Throw New Exception("Update Incident Web Service Error", ex.InnerException)
            RI.SharedFunctions.HandleError()
            ds = Nothing
        Finally
            UpdateIncident = ds
            If ds IsNot Nothing Then ds = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Gets a dataset that contains the incident data for the specified RI Number
    ''' </summary>
    ''' <param name="RINumber">Long - Valid RI Number</param>
    ''' <returns>Returns a dataset containing the status codes for the IRIS update
    ''' ReturnStatus 0 -- StatusDescription = "Incident has been created"
    ''' ReturnStatus 200 -- StatusDescription = "RInumber does not exist in the database"
    ''' ReturnStatus 300 -- StatusDescription = "IRIS number already populated and will not be overlayed"
    ''' ReturnStatus Else -- StatusDescription = "Oracle Error"
    ''' </returns>
    ''' <remarks></remarks>
    <WebMethod(Description:="Gets a dataset that contains the incident data for the specified RI Number")> _
    Public Function GetIncident(ByVal RINumber As Long) As DataSet
        Dim ds As System.Data.DataSet = Nothing
        Try
            If RINumber > 0 Then
                Dim sql As String = "select * from tblRIincident where rinumber = '{0}' order by rinumber"
                sql = String.Format(sql, RINumber)
                If IsNumeric(RINumber) Then
                    ds = RI.SharedFunctions.GetOracleDataSet(sql)
                End If
            End If
        Catch ex As Exception
            ds = Nothing
            Throw New Exception("Get Incident Web Service Error", ex.InnerException)
            RI.SharedFunctions.HandleError()
        Finally
            GetIncident = ds
        End Try
    End Function
    ''' <summary>
    ''' Creates a new incident for the specified IRIS number
    ''' </summary>
    ''' <param name="Site">Facility Name (i.e Courtland)</param>
    ''' <param name="IncidentDate">String - Valid Date (i.e. 07/01/2007)</param>
    ''' <param name="IncidentTime">String - HH:MM (i.e 14:22)</param>
    ''' <param name="BusinessUnit">String - Millwide</param>
    ''' <param name="Area">String - Safety or Environmental</param>
    ''' <param name="Title">String - Title of the Incident</param>
    ''' <param name="Description">String - Description of the Incident</param>
    ''' <param name="UserName">String - Authenticated User Name (i.e. MJPOPE)</param>
    ''' <param name="IRISNumber">Integer</param>
    ''' <param name="IncidentType">String</param>
    ''' <returns>Returns a dataset that contains the new RI Number</returns>
    ''' <remarks></remarks>
    <WebMethod(Description:="Creates a new incident for the specified IRIS number")> _
    Public Function CreateIncident(ByVal Site As String, ByVal IncidentDate As String, ByVal IncidentTime As String, ByVal BusinessUnit As String, ByVal Area As String, ByVal Title As String, ByVal Description As String, ByVal UserName As String, ByVal IRISNumber As Long, ByVal IncidentType As String) As DataSet
        Dim ds As System.Data.DataSet = Nothing
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim startDate As DateTime

        Try

            If IsDate(IncidentDate) Then
                startDate = FormatDateTime(IncidentDate, DateFormat.ShortDate)
            Else
                startDate = FormatDateTime(Now(), DateFormat.ShortDate)
            End If

            If Area.ToLower <> "safety" And Area.ToLower <> "environmental" Then
                'Do Nothing right now
            End If
            'If BusinessUnit.ToLower <> "other area" Then BusinessUnit = "Other Area"
            BusinessUnit = "Millwide" 'MJP Changed on 11/11/2010
            If IRISNumber > 0 Then
                param = New OracleParameter
                param.ParameterName = "in_Site"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = Site
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_EventDate"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = startDate & " " & IncidentTime
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_BusinessUnit"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = BusinessUnit
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_Area"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = Area
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_Title"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = Title
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_Description"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = Description
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_UserName"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = UserName
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_IRISNumber"
                param.OracleDbType = OracleDbType.Number
                param.Direction = Data.ParameterDirection.Input
                param.Value = IRISNumber
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_IncidentType"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = IncidentType
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "out_RINumber"
                param.OracleDbType = OracleDbType.Number
                param.Direction = Data.ParameterDirection.Output
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "out_status"
                param.OracleDbType = OracleDbType.Number
                param.Direction = Data.ParameterDirection.Output
                paramCollection.Add(param)

                Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.IRISUPDATE.CreateIncident")
                Dim dt As New DataTable
                Dim dr As DataRow = dt.NewRow
                Dim StatusDescription As String = String.Empty
                Select Case returnStatus
                    Case 0
                        StatusDescription = "Incident has been created"
                        'Case 200
                        '    StatusDescription = "RInumber does not exist in the database"
                        'Case 300
                        '    StatusDescription = "IRIS number already populated and will not be overlayed"
                    Case Else
                        StatusDescription = "Oracle Error"
                End Select


                dt.Columns.Add("RINumber")
                dt.Columns.Add("IRISNumber")
                dt.Columns.Add("Out_Status")
                dt.Columns.Add("Status_Description")
                dr.Item("RINumber") = paramCollection.Item("out_RINumber").Value
                dr.Item("IRISNumber") = IRISNumber
                dr.Item("Out_Status") = returnStatus

                dr.Item("Status_Description") = StatusDescription
                ds = New DataSet
                dt.Rows.Add(dr)
                ds.Tables.Add(dt)
            End If
        Catch ex As Exception
            Throw New Exception("Create Incident Web Service Error", ex.InnerException)
            RI.SharedFunctions.HandleError()
            ds = Nothing
        Finally
            CreateIncident = ds
            If ds IsNot Nothing Then ds = Nothing
        End Try
    End Function


End Class
