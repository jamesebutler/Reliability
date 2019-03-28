Imports System.Web
Imports System.Data
Imports Devart.Data.Oracle

Public Class clsNEMAMotor
    Inherits System.Web.Services.WebService

    Public Shared Function PopulateHP(ByVal MotorType As String) As Data.DataTableReader
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Try

            param = New OracleParameter
            param.ParameterName = "inMotorType"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = MotorType
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsHP"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RELADMIN.NEMAMOTOR.GETHP"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "RELADMIN.NEMAMOTOR.GETHP", key, 0)
            If ds IsNot Nothing Then dr = ds.CreateDataReader
        Catch ex As Exception
            ds = Nothing
            dr = Nothing
            RI.SharedFunctions.HandleError("PopulateHP", , ex)
        Finally
            PopulateHP = dr
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function
    Public Shared Function PopulateRPM(ByVal MotorType As String, ByVal HP As String) As Data.DataTableReader
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Try

            param = New OracleParameter
            param.ParameterName = "inMotorType"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = MotorType
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "inHP"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = HP
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsRPM"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RELADMIN.NEMAMOTOR.GETRPM"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "RELADMIN.NEMAMOTOR.GETRPM", key, 0)
            If ds IsNot Nothing Then dr = ds.CreateDataReader
        Catch ex As Exception
            ds = Nothing
            dr = Nothing
            RI.SharedFunctions.HandleError("PopulateRPM", , ex)
        Finally
            PopulateRPM = dr
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function
    Public Shared Function GetNewMotorPrice(ByVal MotorType As String, ByVal HP As Decimal, ByVal RPM As Integer) As Devart.Data.Oracle.OracleDataReader
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim dr As Devart.Data.Oracle.OracleDataReader = Nothing
        Try

            param = New OracleParameter
            param.ParameterName = "inHP"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = HP
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "inRPM"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = RPM
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "inMotorType"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = MotorType
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsPrice"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RELADMIN.NEMAMOTOR.GetNewMotorPrice"

            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "RELADMIN.NEMAMOTOR.GetNewMotorPrice")
            'If ds IsNot Nothing Then dr = ds.CreateDataReader
        Catch ex As Exception
            dr = Nothing
            RI.SharedFunctions.HandleError("GetNewMotorPrice", , ex)
        Finally
            GetNewMotorPrice = dr
        End Try
    End Function

    Public Shared Function GetEfficiency(ByVal MotorType As String, ByVal HP As Decimal, ByVal RPM As Integer) As Devart.Data.Oracle.OracleDataReader
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim dr As Devart.Data.Oracle.OracleDataReader = Nothing
        Try

            param = New OracleParameter
            param.ParameterName = "inHP"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = HP
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "inRPM"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = RPM
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "inMotorType"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = MotorType
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsEfficiency"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "RELADMIN.NEMAMOTOR.GETEFFICIENCY")
        Catch ex As Exception
            dr = Nothing
            RI.SharedFunctions.HandleError("GetEfficiency", , ex)
        Finally
            GetEfficiency = dr            
        End Try
    End Function

    Public Shared Function GetNEMAUsers(ByVal USERNAME As String) As Devart.Data.Oracle.OracleDataReader
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim dr As Devart.Data.Oracle.OracleDataReader = Nothing
        Try
            param = New OracleParameter
            param.ParameterName = "IN_USERNAME"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = ParameterDirection.Input
            param.Value = USERNAME
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "RSNEMAUSERS"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RELADMIN.NEMAMOTOR.GETNEMAUSERS"
            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "RELADMIN.NEMAMOTOR.GETNEMAUSERS")
        Catch ex As Exception
            dr = Nothing
            RI.SharedFunctions.HandleError("GetNEMAUsers", , ex)
        Finally
            GetNEMAUsers = dr
        End Try
    End Function

End Class
