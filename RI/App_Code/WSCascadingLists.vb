Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports AjaxControlToolkit
Imports System.Data
Imports Devart.Data.Oracle

''' <summary>
''' CascadingDropDown is an ASP.NET AJAX extender that can be attached to an ASP.NET DropDownList control to get automatic population of a set of DropDownList controls. Each time the selection of one the DropDownList controls changes, the CascadingDropDown makes a call to a specified web service to retrieve the list of values for the next DropDownList in the set. 
''' </summary>
''' <remarks>Example Code - http://asp.net/ajax/control-toolkit/live/</remarks>
<WebService(Namespace:="http://RI/")> _
<System.Web.Script.Services.ScriptService()> _
Public Class wsCascadingLists
    Inherits System.Web.Services.WebService

    ''' <summary>
    ''' Gets the list of failure events for the Dropdown List
    ''' </summary>
    <Services.WebMethod(cacheduration:=0, enableSession:=True), Script.Services.ScriptMethod()> _
    Public Function GetFailureEvent(ByVal knownCategoryValues As String, ByVal category As String, ByVal contextKey As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing

        Try
            If contextKey Is Nothing Then contextKey = String.Empty
            ds = PopulateFailureEvent()
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If

                If contextKey = "All" Then
                    'values.Add(New CascadingDropDownNameValue(RI.SharedFunctions.LocalizeValue("All"), "AL"))
                    contextKey = String.Empty
                End If

                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("failureeventseqid"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("failureeventdesc"))
                    If contextKey.Length > 0 And contextKey <> "AL" Then
                        If contextKey.Contains(val) Then values.Add(New CascadingDropDownNameValue(desc, val))
                    Else
                        values.Add(New CascadingDropDownNameValue(desc, val))
                    End If

                Loop


            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetFailureEvent", , ex)
        Finally
            GetFailureEvent = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
            If ds IsNot Nothing Then
                ds = Nothing
            End If
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
        End Try

    End Function


    ''' <summary>
    ''' Gets the list of values for the Failure Modes
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=0, enableSession:=True), Script.Services.ScriptMethod()> _
  Public Function GetFailureMode(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim FailureEventSeqID As String = String.Empty
        Try
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            If Not knownCategoryValuesDictionary.ContainsKey("FailureEventSeqID") Then
                Return Nothing
            Else
                FailureEventSeqID = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("FailureEventSeqID"))
            End If
            ds = PopulateFailureMode(FailureEventSeqID)
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If


                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("failuremodeseqid"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("failuremodedesc"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetFailureMode", , ex)
        Finally
            GetFailureMode = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray) 'values.ToArray 
            If ds IsNot Nothing Then
                ds = Nothing
            End If
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
        End Try
    End Function
    Shared Function CleanKnownCategoryValues(ByVal knownCategoryValues As String) As String
        Return Replace(knownCategoryValues, "undefined:", "FailureEventSeqID:")
    End Function

    Public Function GetTrigger(ByVal knownCategoryValues As String, ByVal category As String, ByVal contextKey As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim SiteId As String = String.Empty
        Dim BusArea As String = String.Empty
        Dim LineBreak As String = String.Empty
        Try
            If contextKey Is Nothing Then contextKey = String.Empty
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            If Not knownCategoryValuesDictionary.ContainsKey("SiteID") Then 'Or Not knownCategoryValuesDictionary.ContainsKey("BusArea") Then
                Return Nothing
            Else
                SiteId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("SiteID"))
                BusArea = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("BusArea"))
                LineBreak = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("LineBreak"))
            End If
            ds = PopulateTrigger(SiteId, BusArea, LineBreak)
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If

                If contextKey.Length > 0 Then
                    values.Add(New CascadingDropDownNameValue(RI.SharedFunctions.LocalizeValue(contextKey), contextKey, True))
                    contextKey = String.Empty
                End If

                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("TriggerDesc"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("TriggerDesc"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
            End If



            'Do While dr.Read()
            '    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("SiteId"))
            '    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("SiteName"))
            '    If contextKey.Length > 0 And contextKey <> "AL" Then
            '        If contextKey.Contains(val) Then values.Add(New CascadingDropDownNameValue(desc, val))
            '    Else
            '        values.Add(New CascadingDropDownNameValue(desc, val))
            '    End If

            'Loop
            'End If


        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetTrigger", , ex)
        Finally
            GetTrigger = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
            If ds IsNot Nothing Then
                ds = Nothing
            End If
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
        End Try

    End Function


    Private Function PopulateTrigger(ByVal SiteID As String, ByVal BusArea As String, ByVal LineBreak As String) As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New OracleParameter
            param.ParameterName = "inSiteId"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SiteID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "inBusArea"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = BusArea
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "inLine"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = LineBreak
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsTriggerList"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RI.TriggerList_" & SiteID & "_" & BusArea & "_" & LineBreak
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RI.TriggerList", key, 8)

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("PopulateTrigger", , ex)
        Finally
            PopulateTrigger = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function

    Private Function PopulateFailureEvent() As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            param = New OracleParameter
            param.ParameterName = "rsFailureEvent"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RINEWINCIDENT.FailureEvent"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.FailureEvent", key, 0)

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("PopulateFailureEvent", , ex)
        Finally
            PopulateFailureEvent = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function

    Private Function PopulateFailureMode(ByVal FailureEventSeq As String) As DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim key As String = String.Empty

        Try
            key = "FailureModeDDL_" & FailureEventSeq

            param = New OracleParameter
            param.ParameterName = "in_failureeventseqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = FailureEventSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsFailureMode"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINewIncident.FailureMode", key, 0)

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("PopulateFailureMode", , ex)
        Finally
            PopulateFailureMode = ds
            If Not ds Is Nothing Then ds = Nothing
        End Try
    End Function

End Class
