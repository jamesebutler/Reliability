
Imports System
Imports Devart.Data.Oracle
Imports RI


<Serializable()> _
Public Class clsDemoList
    Public Property SearchData() As Devart.Data.Oracle.OracleDataReader
        Get
            Return mSearchData
        End Get
        Set(ByVal value As Devart.Data.Oracle.OracleDataReader)
            mSearchData = value
        End Set
    End Property
    Private Property SearchDT() As System.Data.DataTable
        Get
            Return mSearchDT
        End Get
        Set(ByVal value As System.Data.DataTable)
            mSearchDT = value
        End Set
    End Property
    Public Function Search() As Devart.Data.Oracle.OracleDataReader
        'Perform Search 
        GetDemoList()
        Return SearchData
    End Function
    Public Function GetDataTable() As System.Data.DataTable
        GetDemoList(True)
        Return SearchDT
    End Function
    Private Sub GetDemoList(Optional ByVal createDataTable As Boolean = False)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = "N"

        'Check input paramaters

        Try
            'If inActiveFlag = True Then
            '    ActiveFlag = "Y"
            'Else
            '    ActiveFlag = "N"
            'End If



            param = New OracleParameter
            param.ParameterName = "rsDemoList"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)


            'Dim key As String = "RIViewUpdateSearch_" & Facility & "_" & Division & "_" & ActiveFlag
            'If createDataTable = True Then
            '    ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RIView.IncidentListingDDL", "", 0)
            '    Me.SearchDS = ds
            '    Me.SearchData = CType(ds.Tables(0).CreateDataReader, OracleDataReader)
            'Else
            Dim dr As Devart.Data.Oracle.OracleDataReader = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "Reladmin.RI.DemoList")
            'If ds IsNot Nothing Then
            'If ds.Tables.Count = 1 Then

            If dr IsNot Nothing Then
                If createDataTable = True Then
                    Dim dt As New Data.DataTable
                    dt.Load(dr)
                    Me.SearchDT = dt
                End If

                Me.SearchData = dr 'ds.Tables(0).CreateDataReader
                'dr.Close()
                'dr = Nothing
            Else
                SearchData = Nothing
            End If
            'End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private mSearchDT As Data.DataTable
    Private mSearchData As Devart.Data.Oracle.OracleDataReader

End Class

Public Class clsOutageDemoList
    Public Property SearchData() As Devart.Data.Oracle.OracleDataReader
        Get
            Return mSearchData
        End Get
        Set(ByVal value As Devart.Data.Oracle.OracleDataReader)
            mSearchData = value
        End Set
    End Property
    Private Property SearchDT() As Data.DataTable
        Get
            Return mSearchDT
        End Get
        Set(ByVal value As Data.DataTable)
            mSearchDT = value
        End Set
    End Property
    Public Function Search() As Devart.Data.Oracle.OracleDataReader
        'Perform Search 
        GetDemoList()
        Return SearchData
    End Function
    Public Function GetDataTable() As Data.DataTable
        GetDemoList(True)
        Return SearchDT
    End Function
    Private Sub GetDemoList(Optional ByVal createDataTable As Boolean = False)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Try
            param = New OracleParameter
            param.ParameterName = "rsDemoList"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim dr As Devart.Data.Oracle.OracleDataReader = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "Reladmin.Outage.DemoList")

            If dr IsNot Nothing Then
                If createDataTable = True Then
                    Dim dt As New Data.DataTable
                    dt.Load(dr)
                    Me.SearchDT = dt
                End If

                Me.SearchData = dr
            Else
                SearchData = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private mSearchDT As Data.DataTable
    Private mSearchData As Devart.Data.Oracle.OracleDataReader

End Class
Public Class clsMOCDemoList
    Public Property SearchData() As Devart.Data.Oracle.OracleDataReader
        Get
            Return mSearchData
        End Get
        Set(ByVal value As Devart.Data.Oracle.OracleDataReader)
            mSearchData = value
        End Set
    End Property
    Private Property SearchDT() As Data.DataTable
        Get
            Return mSearchDT
        End Get
        Set(ByVal value As Data.DataTable)
            mSearchDT = value
        End Set
    End Property
    Public Function Search() As Devart.Data.Oracle.OracleDataReader
        'Perform Search 
        GetDemoList()
        Return SearchData
    End Function
    Public Function GetDataTable() As Data.DataTable
        GetDemoList(True)
        Return SearchDT
    End Function
    Private Sub GetDemoList(Optional ByVal createDataTable As Boolean = False)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Try
            param = New OracleParameter
            param.ParameterName = "rsDemoList"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim dr As Devart.Data.Oracle.OracleDataReader = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "Reladmin.viewmoc.DemoList")

            If dr IsNot Nothing Then
                If createDataTable = True Then
                    Dim dt As New Data.DataTable
                    dt.Load(dr)
                    Me.SearchDT = dt
                End If

                Me.SearchData = dr
            Else
                SearchData = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private mSearchDT As Data.DataTable
    Private mSearchData As Devart.Data.Oracle.OracleDataReader

End Class