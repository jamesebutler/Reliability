Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Devart.Data.Oracle
Imports System.Data

<WebService(Namespace:="http://RI/")> _
<System.Web.Script.Services.ScriptService()> _
Public Class RISharedWS
    Inherits System.Web.Services.WebService

    <WebMethod(EnableSession:=True)> _
      Public Function DeleteScope(ByVal OutageScopeSeqId As String) As String

        Dim dr As Devart.Data.Oracle.OracleDataReader = Nothing
        Dim paramCollection As New OracleParameterCollection
        Dim status As String
        Dim userProfile As RI.CurrentUserProfile = Nothing
        userProfile = RI.SharedFunctions.GetUserProfile

        Try
            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_OutageSeqId"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = OutageScopeSeqId
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "outage.DeleteOutageScope")
            If status <> "0" Then
                Throw New DataException("DeleteOutageScope Oracle Error:" & status)
            End If
            Dim ses As System.Web.SessionState.HttpSessionState = HttpContext.Current.Session
            'ses.Remove("ActionItems")
        Catch ex As Exception
            Throw New DataException("DeleteOutageScope", ex)
        Finally
            If Not dr Is Nothing Then dr = Nothing
        End Try
        Return status
    End Function

    <WebMethod(EnableSession:=True)> _
      Public Function GetEmployeeListBySite(ByVal siteID As String) As String()

        Dim values As New Generic.List(Of String)

        Dim ds As DataSet = Nothing
        Dim paramCollection As New OracleParameterCollection
        Try

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = siteID
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsPerson"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "RI.PersonDDL", "Person_" & siteID, 3)

            Dim dr As DataTableReader = ds.CreateDataReader


            If dr IsNot Nothing Then
                Do While dr.Read
                    If dr.Item("UserName") IsNot DBNull.Value Then
                        values.Add(dr.Item("UserName") & "::" & RI.SharedFunctions.DataClean(dr.Item("Person")))
                    End If
                Loop
            End If
        Catch ex As Exception
            Throw New DataException("GetPerson", ex)
            Return Nothing
        Finally
            GetEmployeeListBySite = values.ToArray
            If Not ds Is Nothing Then ds = Nothing
        End Try
    End Function

    <WebMethod(EnableSession:=True)> _
       Public Function DeleteOutageTemplateTask(ByVal TaskSeqId As String) As String

        Dim dr As Devart.Data.Oracle.OracleDataReader = Nothing
        Dim paramCollection As New OracleParameterCollection
        Dim status As String
        Dim userProfile As RI.CurrentUserProfile = Nothing
        userProfile = RI.SharedFunctions.GetUserProfile

        Try
            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "inMTTTaskSeqId"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = TaskSeqId
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "inUserID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userProfile.Username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "outageMaint.DeleteTemplateTask")
            If status <> "0" Then
                Throw New DataException("DeleteOutageTemplateTask Oracle Error:" & status)
            End If
            Dim ses As System.Web.SessionState.HttpSessionState = HttpContext.Current.Session

        Catch ex As Exception
            Throw New DataException("DeleteOutageTamplateTask", ex)
        Finally
            If Not dr Is Nothing Then dr = Nothing
        End Try
        Return status
    End Function

    <WebMethod(EnableSession:=True)> _
   Public Function DeleteMOCTemplateTask(ByVal TaskSeqId As String) As String

        Dim dr As Devart.Data.Oracle.OracleDataReader = Nothing
        Dim paramCollection As New OracleParameterCollection
        Dim status As String
        Dim userProfile As RI.CurrentUserProfile = Nothing
        userProfile = RI.SharedFunctions.GetUserProfile

        Try
            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_taskitem"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = TaskSeqId
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userProfile.Username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "moctasktemplatemaint.DeleteTemplateTask")
            If status <> "0" Then
                Throw New DataException("DeleteMOCTemplateTask Oracle Error:" & status)
            End If
            Dim ses As System.Web.SessionState.HttpSessionState = HttpContext.Current.Session

        Catch ex As Exception
            Throw New DataException("DeleteMOCTemplateTask", ex)
        Finally
            If Not dr Is Nothing Then dr = Nothing
        End Try
        Return status
    End Function
End Class
