Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports AjaxControlToolkit
Imports System.Data
Imports Devart.Data.Oracle

<WebService(Namespace:="http://RI/")> _
<System.Web.Script.Services.ScriptService()> _
Public Class IndicentCauses
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function GetTypeList(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary

        Try
            ds = PopulateCauses()
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 5 Then
                    knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
                    Dim dv As Data.DataView = ds.Tables(0).DefaultView
                    If Not knownCategoryValuesDictionary.ContainsKey("Process") Then
                        For i As Integer = 0 To dv.Count - 1
                            Dim val As String = CleanData(dv.Item(i).Item("Cause"))
                            values.Add(New CascadingDropDownNameValue(dv.Item(i).Item("Cause"), val))
                        Next
                    Else
                        Dim rowFilter As String = "process='{0}'"
                        dv.RowFilter = String.Format(rowFilter, UnCleanData(knownCategoryValuesDictionary("Process")))

                        For i As Integer = 0 To dv.Count - 1
                            Dim val As String = CleanData(dv.Item(i).Item("Cause"))
                            values.Add(New CascadingDropDownNameValue(dv.Item(i).Item("Cause"), val))
                        Next
                        dv = Nothing
                    End If

                End If

                '    dr = ds.Tables(0).CreateDataReader
                'End If
                'Do While dr.Read()
                '    Dim val As String = CleanData(dr.Item("Cause"))
                '    values.Add(New CascadingDropDownNameValue(dr.Item("Cause"), val))
                'Loop
            End If

        Catch ex As Exception
            Throw
        Finally
            GetTypeList = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
            If ds IsNot Nothing Then
                ds = Nothing
            End If
        End Try

    End Function

    <WebMethod()> _
    Public Function GetProcessTypeList(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Try
            ds = PopulateCauses()
            'Get a dictionary of known category/value pairs
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 5 Then
                    'knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
                    'If Not knownCategoryValuesDictionary.ContainsKey("Process") Then
                    '    Dim dv1 As Data.DataView = ds.Tables(0).DefaultView
                    '    'dv.RowFilter = String.Format(rowFilter, UnCleanData(knownCategoryValuesDictionary("Process")))

                    '    For i As Integer = 0 To dv1.Count - 1
                    '        Dim val As String = CleanData(dv1.Item(i).Item("Cause"))
                    '        values.Add(New CascadingDropDownNameValue(dv1.Item(i).Item("Cause"), val))
                    '    Next
                    '    dv1 = Nothing
                    '    'Return Nothing
                    'End If
                    knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
                    If Not knownCategoryValuesDictionary.ContainsKey("Process") Then
                        Return Nothing
                    End If
                    Dim rowFilter As String = "process='{0}'"
                    Dim dv As Data.DataView = ds.Tables(0).DefaultView
                    dv.RowFilter = String.Format(rowFilter, UnCleanData(knownCategoryValuesDictionary("Process")))

                    For i As Integer = 0 To dv.Count - 1
                        Dim val As String = CleanData(dv.Item(i).Item("Cause"))
                        values.Add(New CascadingDropDownNameValue(dv.Item(i).Item("Cause"), val))
                    Next
                    dv = Nothing
                End If
            End If
        Catch ex As Exception
            Throw
        Finally
            GetProcessTypeList = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
            If ds IsNot Nothing Then ds = Nothing
        End Try


    End Function
    <WebMethod()> _
    Public Function GetPrevention(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing

        Try
            ds = PopulateCauses()
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 5 Then
                    dr = ds.Tables(2).CreateDataReader
                End If
                Do While dr.Read()
                    Dim val As String = CleanData(dr.Item("Prevention"))
                    values.Add(New CascadingDropDownNameValue(dr.Item("Prevention"), val))
                Loop
            End If

        Catch ex As Exception
            Throw
        Finally
            GetPrevention = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
            If ds IsNot Nothing Then
                ds = Nothing
            End If
        End Try

    End Function
    <WebMethod()> _
    Public Function GetProcessList(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()

        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Try
            'Dim sql As String = "Select cause,Process From tblRICauseProcess Where Process <> 'All' and bustype='PM' and cause='{0}' Order By Process"
            ds = PopulateCauses()
            'Get a dictionary of known category/value pairs
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 5 Then
                    knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
                    If Not knownCategoryValuesDictionary.ContainsKey("Causes") Then
                        Return Nothing
                    End If

                    Dim rowFilter As String = "cause='{0}'"
                    Dim dv As Data.DataView = ds.Tables(3).DefaultView
                    dv.RowFilter = String.Format(rowFilter, UnCleanData(knownCategoryValuesDictionary("Causes")))                

                    For i As Integer = 0 To dv.Count - 1
                        Dim val As String = CleanData(dv.Item(i).Item("Process"))
                        values.Add(New CascadingDropDownNameValue(dv.Item(i).Item("Process"), val))
                    Next
                    dv = Nothing                  
                End If
            End If
        Catch ex As Exception
            Throw
        Finally
            GetProcessList = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
            If ds IsNot Nothing Then ds = Nothing
        End Try


    End Function

    <WebMethod()> _
    Public Function GetProcessListFirst(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()

        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing

        Try
            ds = PopulateCauses()

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 5 Then
                    dr = ds.Tables(3).CreateDataReader
                    Do While dr.Read
                        Dim val As String = CleanData(dr.Item("Process"))
                        values.Add(New CascadingDropDownNameValue(dr.Item("Process"), val))
                    Loop
                End If
            End If
        Catch ex As Exception
            Throw
        Finally
            GetProcessListFirst = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
            If ds IsNot Nothing Then ds = Nothing
        End Try


    End Function
    <WebMethod()> _
    Public Function GetReasonList(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary

        Try            
            ds = PopulateCauses()
            'Get a dictionary of known category/value pairs
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 5 Then
                    'Get a dictionary of known category/value pairs
                    knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)

                    If Not knownCategoryValuesDictionary.ContainsKey("Causes") Then
                        Return Nothing
                    End If

                    Dim rowFilter As String = "cause='{0}'"
                    Dim dv As Data.DataView = ds.Tables(1).DefaultView
                    dv.RowFilter = String.Format(rowFilter, UnCleanData(knownCategoryValuesDictionary("Causes")))

                    For i As Integer = 0 To dv.Count - 1
                        Dim val As String = CleanData(dv.Item(i).Item("Reason"))
                        values.Add(New CascadingDropDownNameValue(dv.Item(i).Item("Reason"), val))
                    Next
                    dv = Nothing
                End If
            End If

        Catch ex As Exception
            Throw
        Finally
            GetReasonList = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
        End Try
    End Function

    <WebMethod()> _
    Public Function GetComponentList(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()

        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary

        Try
            ds = PopulateCauses()
            'Get a dictionary of known category/value pairs
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 5 Then
                    'Get a dictionary of known category/value pairs
                    knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)

                    If Not knownCategoryValuesDictionary.ContainsKey("Causes") Or Not knownCategoryValuesDictionary.ContainsKey("Process") Then
                        'If Not knownCategoryValuesDictionary.ContainsKey("Process") Then
                        Return Nothing
                    End If

                    Dim rowFilter As String = "cause='{0}' and process='{1}'"
                    Dim dv As Data.DataView = ds.Tables(4).DefaultView
                    dv.RowFilter = String.Format(rowFilter, UnCleanData(knownCategoryValuesDictionary("Causes")), UnCleanData(knownCategoryValuesDictionary("Process")))

                    For i As Integer = 0 To dv.Count - 1
                        Dim val As String = CleanData(dv.Item(i).Item("Component"))
                        values.Add(New CascadingDropDownNameValue(dv.Item(i).Item("Component"), val))
                    Next
                    dv = Nothing
                End If
            End If
        Catch ex As Exception
        Finally
            GetComponentList = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
        End Try
    End Function

    <WebMethod()> _
    Public Function GetEventComponentList(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()

        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary

        Try
            'ds = PopulateCauses()
            ds = PopulateEventCauses()
            'Get a dictionary of known category/value pairs
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 3 Then
                    'Get a dictionary of known category/value pairs
                    knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)

                    If Not knownCategoryValuesDictionary.ContainsKey("Causes") Or Not knownCategoryValuesDictionary.ContainsKey("Process") Then
                        'If Not knownCategoryValuesDictionary.ContainsKey("Process") Then
                        Return Nothing
                    End If

                    Dim rowFilter As String = "processcause='{0}'"
                    Dim dv As Data.DataView = ds.Tables(3).DefaultView
                    dv.RowFilter = String.Format(rowFilter, UnCleanData(knownCategoryValuesDictionary("Process")))

                    For i As Integer = 0 To dv.Count - 1
                        Dim val As String = CleanData(dv.Item(i).Item("Component"))
                        values.Add(New CascadingDropDownNameValue(dv.Item(i).Item("Component"), val))
                    Next
                    dv = Nothing
                End If
            End If
        Catch ex As Exception
        Finally
            GetEventComponentList = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
        End Try
    End Function
    <WebMethod()> _
    Public Function GetEventReasonList(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary

        Try
            'ds = PopulateCauses()
            ds = PopulateEventCauses()
            'Get a dictionary of known category/value pairs
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 3 Then
                    'Get a dictionary of known category/value pairs
                    knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)

                    If Not knownCategoryValuesDictionary.ContainsKey("Process") Then
                        Return Nothing
                    End If

                    Dim rowFilter As String = "cause='{0}'"
                    Dim dv As Data.DataView = ds.Tables(1).DefaultView
                    dv.RowFilter = String.Format(rowFilter, Mid(UnCleanData(knownCategoryValuesDictionary("Process")), InStr(knownCategoryValuesDictionary("Process"), "-") + 2))

                    For i As Integer = 0 To dv.Count - 1
                        Dim val As String = CleanData(dv.Item(i).Item("Reason"))
                        values.Add(New CascadingDropDownNameValue(dv.Item(i).Item("Reason"), val))
                    Next
                    dv = Nothing
                End If
            End If

        Catch ex As Exception
            Throw
        Finally
            GetEventReasonList = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
        End Try
    End Function

    Private Function CleanData(ByVal val As String) As String
        val = Replace(val, ":", ">")
        Return val
    End Function

    Private Function UnCleanData(ByVal val As String) As String
        val = Replace(val, ">", ":")
        Return val
    End Function
    Private Function PopulateCauses() As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            param = New OracleParameter
            param.ParameterName = "rsType"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsCause"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsPrevention"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsProcess"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsComponent"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RINEWINCIDENT.DropDownDDLCauses"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.DropDownDDLCauses", key, 8)

            If ds IsNot Nothing Then
                'If ds.Tables.Count >= 5 Then
                '    'rsType
                '    mType.DataSource = ds.Tables(0).CreateDataReader
                '    mType.DataTextField = "Cause"
                '    mType.DataValueField = "Cause"

                '    'rsCause
                '    mCause.DataSource = ds.Tables(1).CreateDataReader
                '    mCause.DataTextField = "Reason"
                '    mCause.DataValueField = "Reason"

                '    'rsPrevention
                '    mPrevention.DataSource = ds.Tables(2).CreateDataReader
                '    mPrevention.DataTextField = "Prevention"
                '    mPrevention.DataValueField = "Prevention"


                '    'rsProcess
                '    mEquipmentProcess.DataSource = ds.Tables(3).CreateDataReader
                '    mEquipmentProcess.DataTextField = "Process"
                '    mEquipmentProcess.DataValueField = "Process"

                '    'rsComponent
                '    mComponent.DataSource = ds.Tables(4).CreateDataReader
                '    mComponent.DataTextField = "Component"
                '    mComponent.DataValueField = "Component"


                'End If
            End If

        Catch ex As Exception
            Throw
        Finally
            PopulateCauses = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function

    Private Function PopulateEventCauses() As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            param = New OracleParameter
            param.ParameterName = "rsType"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsCause"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsComponent"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "EventTracking.DropDownDDLCauses"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.EventTracking.DropDownDDLCauses", key, 0)

        Catch ex As Exception
            Throw
        Finally
            PopulateEventCauses = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function

    Private Function PopulateEventProcess(siteid As String, businessunit As String, area As String) As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            param = New OracleParameter
            param.ParameterName = "in_SiteID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = siteid '"OR"
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_BusinessUnit"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = businessunit
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Area"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = area
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsProcess"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "EventTracking.DropDownDDLProcess"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.EventTracking.DropDownDDLProcess", key, 0)

        Catch ex As Exception
            Throw
        Finally
            PopulateEventProcess = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function
    <WebMethod()> _
    Public Function GetProcessTypeFirstList(ByVal knownCategoryValues As String, ByVal category As String, ByVal contextKey As String) As CascadingDropDownNameValue()

        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim SiteId As String = String.Empty
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim BusUnit As String = String.Empty
        Dim Area As String = String.Empty

        Try
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)

            If Not knownCategoryValuesDictionary.ContainsKey("siteid") Or Not knownCategoryValuesDictionary.ContainsKey("businessunit") Then
                Return Nothing
            Else
                SiteId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("siteid"))
                BusUnit = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("businessunit"))
                Area = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("area"))
                ds = PopulateEventProcess(SiteId, BusUnit, Area)

                If ds IsNot Nothing Then
                    If ds.Tables.Count >= 1 Then
                        dr = ds.Tables(0).CreateDataReader
                        Do While dr.Read
                            Dim val As String = CleanData(dr.Item("Process"))
                            values.Add(New CascadingDropDownNameValue(dr.Item("Process"), val))
                        Loop
                    End If
                End If
            End If

        Catch ex As Exception
            Throw
        Finally
            GetProcessTypeFirstList = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
            If ds IsNot Nothing Then ds = Nothing
        End Try


    End Function

    Shared Function CleanKnownCategoryValues(ByVal knownCategoryValues As String) As String
        Return Replace(knownCategoryValues, "undefined:", "SiteID:")
    End Function
End Class
