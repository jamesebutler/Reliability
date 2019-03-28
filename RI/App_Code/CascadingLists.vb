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
Public Class CascadingLists
    Inherits System.Web.Services.WebService

    ''' <summary>
    ''' Gets the list of values for the Facility Dropdown List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <param name="contextKey"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=0, enableSession:=True), Script.Services.ScriptMethod()> _
    Public Function GetFacilityList(ByVal knownCategoryValues As String, ByVal category As String, ByVal contextKey As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing

        Try
            If contextKey Is Nothing Then contextKey = String.Empty
            ds = PopulateFacility()
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If
                If contextKey = "All" Then
                    values.Add(New CascadingDropDownNameValue(RI.SharedFunctions.LocalizeValue("All"), "AL"))
                    contextKey = String.Empty
                End If

                Dim selectedDivision As String = "All"
                Dim knownCategoryValuesDictionary As New StringDictionary
                If knownCategoryValues IsNot Nothing AndAlso knownCategoryValues.Length > 0 Then
                    knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
                    knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
                    If (knownCategoryValuesDictionary.ContainsKey("Division")) Then
                        selectedDivision = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("Division"))
                    End If
                End If
                Do While dr.Read()
                    If dr.Item("Division") = selectedDivision OrElse selectedDivision = "All" Then
                        Dim val As String = RI.SharedFunctions.DataClean(dr.Item("SiteId"))
                        Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("SiteName"))
                        If contextKey.Length > 0 And contextKey <> "AL" Then
                            If contextKey.Contains(val) Then values.Add(New CascadingDropDownNameValue(desc, val))
                        Else
                            values.Add(New CascadingDropDownNameValue(desc, val))
                        End If
                    End If
                Loop
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetFacilityList", , ex)
        Finally
            GetFacilityList = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
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
    ''' Gets the list of values for the Business Unit/Area Dropdown List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=0, enableSession:=True), Script.Services.ScriptMethod()> _
    Public Function GetBusArea(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim SiteId As String = String.Empty

        Try
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            If Not (knownCategoryValuesDictionary.ContainsKey("SiteID") Or knownCategoryValuesDictionary.ContainsKey("Undefined")) Then
                Return Nothing
            ElseIf knownCategoryValuesDictionary.ContainsKey("SiteID") Then
                SiteId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("SiteID"))
            Else
                SiteId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("Undefined"))
            End If
            ds = PopulateBusArea(SiteId)
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If
                
                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("BusArea"))
                    values.Add(New CascadingDropDownNameValue(val, val))
                Loop
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetBusArea", , ex)
        Finally
            GetBusArea = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray, " - ")
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
    ''' Gets the list of values for the Analysis Leader Dropdown List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=0, enableSession:=True), Script.Services.ScriptMethod()> _
  Public Function GetAnalysisLeader(ByVal knownCategoryValues As String, ByVal category As String, ByVal contextKey As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim SiteId As String = String.Empty
        Dim BusArea As String = String.Empty
        Dim leader() As String = Nothing
        Try
            If contextKey IsNot Nothing Then
                leader = contextKey.Split("|")
                If leader.Length = 2 Then
                    Dim leadername() As String = leader(1).Split(" ")
                    If leadername.Length >= 2 Then
                        leader(1) = leadername(1) & ", " & leadername(0)
                    End If
                End If
            End If
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            If Not knownCategoryValuesDictionary.ContainsKey("SiteID") Or Not knownCategoryValuesDictionary.ContainsKey("BusArea") Then
                Return Nothing
            Else
                SiteId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("SiteID"))
                BusArea = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("BusArea"))
            End If
            ds = PopulateAnalysisLeader(SiteId, BusArea)
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If
                If leader IsNot Nothing Then
                    If leader.Length >= 2 Then
                        values.Add(New CascadingDropDownNameValue(leader(1), leader(0), True))
                    End If
                End If
                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("UserName"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("Leader"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetAnalysisLeader", , ex)
        Finally
            GetAnalysisLeader = values.ToArray 'RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
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
    ''' Gets the list of values for the Person Dropdown List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=0, enableSession:=True), Script.Services.ScriptMethod()> _
  Public Function GetPerson(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim SiteId As String = String.Empty
        Try
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            If Not knownCategoryValuesDictionary.ContainsKey("SiteID") Then
                Return Nothing
            Else
                SiteId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("SiteID"))
            End If
            ds = PopulatePerson(SiteId)
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If

                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("UserName"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("Person"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetPerson", , ex)
        Finally
            GetPerson = values.ToArray 'RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
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
    ''' Gets the list of values for the Employee Dropdown List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=0, enableSession:=True), Script.Services.ScriptMethod()> _
    Public Function GetEmployee(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim SiteId As String = String.Empty
        Try
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            If Not knownCategoryValuesDictionary.ContainsKey("SiteID") Then
                Return Nothing
            Else
                SiteId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("SiteID"))
            End If
            ds = PopulateEmployee(SiteId)
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If

                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("UserName"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("Person"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetEmployee", , ex)
        Finally
            GetEmployee = values.ToArray 'RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
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
    ''' Gets the list of values for the Line Break Dropdown List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=0, enableSession:=True), Script.Services.ScriptMethod()> _
  Public Function GetLineBreak(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim SiteId As String = String.Empty
        Dim BusArea As String = String.Empty
        Try
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            If Not knownCategoryValuesDictionary.ContainsKey("SiteID") Or Not knownCategoryValuesDictionary.ContainsKey("BusArea") Then
                Return Nothing
            Else
                SiteId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("SiteID"))
                BusArea = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("BusArea"))
            End If
            ds = PopulateLineLineBreak(SiteId, BusArea)
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If
                'values.Add(New CascadingDropDownNameValue("All", "All", True))

                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("LineBreak"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("LineBreak"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetLineBreak", , ex)
        Finally
            GetLineBreak = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
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
    ''' Gets the list of values for the Facilit Trigger Dropdown List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=0, enableSession:=True), Script.Services.ScriptMethod()> _
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

    ''' <summary>
    ''' Gets the list of values for the Division Dropdown List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=0, enableSession:=True), Script.Services.ScriptMethod()> _
    Public Function GetDivision(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        'Dim knownCategoryValuesDictionary As New StringDictionary
        Dim siteId As String = "Al" 'String.Empty

        Try
            'knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            'knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            'If Not knownCategoryValuesDictionary.ContainsKey("SiteID") Then
            '    Return Nothing
            'Else
            '    siteId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("SiteID"))
            'End If

            ds = PopulateSiteDropDown(siteId)
            Dim defaultDivision As String = Me.GetSiteDivision(siteId)
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If

                Do While dr.Read()
                    Dim val As String = Trim(dr.Item("Division"))
                    Dim desc As String = Trim(dr.Item("Division"))
                    Dim defaultValue As Boolean = False
                    If val.ToUpper.Trim = defaultDivision.ToUpper.Trim Then 'And siteId.ToUpper <> "AL" Then
                        'values.Add(New CascadingDropDownNameValue(desc, val, defaultValue))
                        values.Add(New CascadingDropDownNameValue(desc, val, True))
                        ' ElseIf siteId.ToUpper = "AL" Then
                    Else
                        values.Add(New CascadingDropDownNameValue(desc, val))
                    End If
                Loop
                'values.Add(New CascadingDropDownNameValue("All", "AL"))
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetDivision", , ex)
        Finally
            GetDivision = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
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
    ''' Gets the list of values for the Business Unit Dropdown List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=0, enableSession:=True), Script.Services.ScriptMethod()> _
    Public Function GetBusinessUnit(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim siteId As String = String.Empty

        Try
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            If Not knownCategoryValuesDictionary.ContainsKey("SiteID") Then
                Return Nothing
            Else
                siteId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("SiteID"))
            End If

            ds = PopulateSiteDropDown(siteId)
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(2).CreateDataReader
                End If
                 values.Add(New CascadingDropDownNameValue(RI.SharedFunctions.LocalizeValue("All"), "All", False))
                Dim riResources As New IP.Bids.Localization.WebLocalization
                Dim localizeData As New IP.Bids.Localization.DataLocalization(riResources)
                riResources = Nothing
                localizeData = Nothing
                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("RISUPERAREA"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("RISUPERAREA"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop

            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetBusinessUnit", , ex)
        Finally
            GetBusinessUnit = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
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
    ''' Gets the list of values for the Area Dropdown List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=0, enableSession:=True), Script.Services.ScriptMethod()> _
  Public Function GetArea(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim siteId As String = String.Empty
        Dim businessUnit As String = String.Empty

        Try
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            If Not knownCategoryValuesDictionary.ContainsKey("SiteID") Then
                Return Nothing
            Else
                siteId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("SiteID"))
            End If
            If knownCategoryValuesDictionary.ContainsKey("BusinessUnit") Then
                businessUnit = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("BusinessUnit"))
                If businessUnit = "All" Then businessUnit = String.Empty
            End If


            ds = PopulateSiteDropDown(siteId, businessUnit)
            If ds IsNot Nothing Then
                If businessUnit.Length = 0 Then
                    If ds.Tables.Count >= 4 Then dr = ds.Tables(3).CreateDataReader
                Else
                    If ds.Tables.Count >= 1 Then dr = ds.Tables(0).CreateDataReader
                End If
                values.Add(New CascadingDropDownNameValue(RI.SharedFunctions.LocalizeValue("All"), "All", False))

                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("SUBAREA"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("SUBAREA"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetArea", , ex)
        Finally
            GetArea = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
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
    ''' Gets the list of values for the Line Dropdown List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=0, enableSession:=True), Script.Services.ScriptMethod()> _
  Public Function GetLine(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim siteId As String = String.Empty
        Dim businessUnit As String = String.Empty
        Dim Area As String = String.Empty

        Try
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            If Not knownCategoryValuesDictionary.ContainsKey("SiteID") Then
                Return Nothing
            Else
                siteId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("SiteID"))
            End If
            If knownCategoryValuesDictionary.ContainsKey("BusinessUnit") Then
                businessUnit = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("BusinessUnit"))
                If businessUnit = "All" Then businessUnit = String.Empty
            End If
            If knownCategoryValuesDictionary.ContainsKey("Area") Then
                Area = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("Area"))
                If Area = "All" Then Area = String.Empty
            End If

            ds = PopulateSiteDropDown(siteId, businessUnit, Area)
            If ds IsNot Nothing Then
                If businessUnit.Length = 0 And Area.Length = 0 Then
                    If ds.Tables.Count >= 5 Then dr = ds.Tables(4).CreateDataReader
                ElseIf Area.Length = 0 Then
                    If ds.Tables.Count >= 2 Then dr = ds.Tables(1).CreateDataReader
                Else
                    If ds.Tables.Count >= 1 Then dr = ds.Tables(0).CreateDataReader
                End If
                values.Add(New CascadingDropDownNameValue(RI.SharedFunctions.LocalizeValue("All"), "All", False))

                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("AREA"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("AREA"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetLine", , ex)
        Finally
            GetLine = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
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
    ''' Gets the list of values for the Line Break Dropdown List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=0, enablesession:=True), Script.Services.ScriptMethod()> _
 Public Function GetLineBreaks(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim siteId As String = String.Empty
        Dim businessUnit As String = String.Empty
        Dim Line As String = String.Empty
        Dim Area As String = String.Empty

        Try
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            If Not knownCategoryValuesDictionary.ContainsKey("SiteID") Then
                Return Nothing
            Else
                siteId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("SiteID"))
            End If
            If knownCategoryValuesDictionary.ContainsKey("BusinessUnit") Then
                businessUnit = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("BusinessUnit"))
                If businessUnit = "All" Then businessUnit = String.Empty
            End If
            If knownCategoryValuesDictionary.ContainsKey("Area") Then
                Area = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("Area"))
                If Area = "All" Then Area = String.Empty
            End If
            If knownCategoryValuesDictionary.ContainsKey("Line") Then
                Line = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("Line"))
                If Line = "All" Then Line = String.Empty
            End If

            ds = PopulateSiteDropDown(siteId, businessUnit, Area, Line)
            If ds IsNot Nothing Then
                If businessUnit.Length = 0 And Area.Length = 0 And Line.Length = 0 Then
                    If ds.Tables.Count >= 5 Then dr = ds.Tables(5).CreateDataReader
                ElseIf Area.Length = 0 And Line.Length = 0 Then
                    If ds.Tables.Count >= 2 Then dr = ds.Tables(2).CreateDataReader
                Else
                    If ds.Tables.Count >= 1 Then dr = ds.Tables(0).CreateDataReader
                End If
                values.Add(New CascadingDropDownNameValue(RI.SharedFunctions.LocalizeValue("All"), "All", True))

                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("LineBreak"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("LineBreak"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetLineBreaks", , ex)
        Finally
            GetLineBreaks = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
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
    ''' Gets the Default Division for the specified Facility
    ''' </summary>
    ''' <param name="SiteId"></param>
    ''' <returns>String - Returns the Default Division for the specified Facility</returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=0, enablesession:=True), Script.Services.ScriptMethod()> _
    Public Function GetSiteDivision(ByVal SiteId As String) As String
        Dim ds As DataSet = PopulateFacility()
        Dim ret As String = String.Empty
        Dim dr As DataTableReader = Nothing

        Try
            If ds IsNot Nothing Then
                dr = ds.Tables(0).CreateDataReader

                Do While dr.Read
                    If dr.Item("SiteID") IsNot Nothing Then
                        If dr.Item("SiteID") = SiteId Then
                            ret = dr.Item("Division")
                        End If
                    End If
                Loop
            End If
        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetSiteDivision", , ex)
        Finally
            If ds IsNot Nothing Then
                ds = Nothing
            End If
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
            GetSiteDivision = ret
        End Try
    End Function

    ''' <summary>
    ''' Gets the list of values for the Crew Dropdown List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=0, enablesession:=True), Script.Services.ScriptMethod()> _
  Public Function GetCrew(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim siteId As String = String.Empty

        Try
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            If Not knownCategoryValuesDictionary.ContainsKey("SiteID") Then
                Return Nothing
            Else
                siteId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("SiteID"))
            End If

            ds = PopulateCrewDropDown(siteId)
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If

                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("crew"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("crew"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetCrew", , ex)
        Finally
            GetCrew = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
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
    ''' Gets the list of values for the Shift Dropdown List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=0, enablesession:=True), Script.Services.ScriptMethod()> _
  Public Function GetShift(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim siteId As String = String.Empty

        Try
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            If Not knownCategoryValuesDictionary.ContainsKey("SiteID") Then
                Return Nothing
            Else
                siteId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("SiteID"))
            End If

            ds = PopulateShift(siteId)
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If

                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("shift"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("shift"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetShift", , ex)
        Finally
            GetShift = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
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
    ''' Gets the list of values for the Failed Material Dropdown List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=0, enablesession:=True), Script.Services.ScriptMethod()> _
 Public Function GetFailedMaterial(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim siteId As String = String.Empty

        Try
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            If Not knownCategoryValuesDictionary.ContainsKey("SiteID") Then
                Return Nothing
            Else
                siteId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("SiteID"))
            End If

            ds = PopulateFailedMaterial(siteId)
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If

                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("FailedLocation"))
                    Dim desc As String = val 'RI.SharedFunctions.DataClean(dr.Item("FailedLocation"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetFailedMaterial", , ex)
        Finally
            GetFailedMaterial = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
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
    ''' Gets the list of values for the Outage Coord Dropdown List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=0, enableSession:=True), Script.Services.ScriptMethod()> _
  Public Function GetOutageCoord(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim SiteId As String = String.Empty
        Try
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            If Not knownCategoryValuesDictionary.ContainsKey("SiteID") Then
                Return Nothing
            Else
                SiteId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("SiteID"))
            End If
            ds = PopulateOutageCoord(SiteId)
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If

                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("UserName"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("Person"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetOutageCoord", , ex)
        Finally
            GetOutageCoord = values.ToArray 'RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
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
    ''' Populates a data set for the Business Unit Area Dropdown list
    ''' </summary>
    ''' <param name="SiteID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PopulateBusArea(ByVal SiteID As String) As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SiteID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsBusinessUnitArea"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RINEWINCIDENT.BusinessUnitArea_" & SiteID
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.BusinessUnitArea", key, 8)

        Catch ex As Exception
            RI.SharedFunctions.HandleError("PopulateBusArea", , ex)
        Finally
            PopulateBusArea = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If            
        End Try
    End Function

    Private Function PopulateCrewDropDown(ByVal SiteID As String) As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SiteID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsCrew"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RINEWINCIDENT.Crew_" & SiteID
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.Crew", key, 8)

        Catch ex As Exception
            RI.SharedFunctions.HandleError("PopulateCrewDropDown", , ex)
        Finally
            PopulateCrewDropDown = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function

    Private Function PopulateFailedMaterial(ByVal SiteID As String) As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SiteID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsFailedMaterial"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RINEWINCIDENT.FailedMaterial_" & SiteID
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.FailedMaterial", key, 8)

        Catch ex As Exception
            RI.SharedFunctions.HandleError("PopulateFailedMaterial", , ex)
        Finally
            PopulateFailedMaterial = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function

    Private Function PopulateShift(ByVal SiteID As String) As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SiteID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsShift"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RINEWINCIDENT.Shift_" & SiteID
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.Shift", key, 8)

        Catch ex As Exception
            Throw
        Finally
            PopulateShift = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function

    Private Function PopulateAnalysisLeader(ByVal SiteID As String, ByVal BusArea As String) As System.Data.DataSet
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
            param.ParameterName = "rsAnalysisLeader"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RI.AnalysisLeader_" & SiteID & "_" & BusArea
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RI.AnalysisLeader", key, 8)

        Catch ex As Exception
            RI.SharedFunctions.HandleError("PopulateAnalysisLeader", , ex)
        Finally
            PopulateAnalysisLeader = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function

    Private Function PopulatePerson(ByVal SiteID As String) As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SiteID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsPerson"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RIVIEW.Person_" & SiteID
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RIVIEW.Person", key, 8)

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("PopulatePerson", , ex)
        Finally
            PopulatePerson = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function
    Private Function PopulateEmployee(ByVal SiteID As String) As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SiteID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsPerson"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RI.PersonDDL_" & SiteID
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RI.PersonDDL", key, 8)

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("PopulateEmployee", , ex)
        Finally
            PopulateEmployee = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function

    Private Function PopulateLineLineBreak(ByVal SiteID As String, ByVal BusArea As String) As System.Data.DataSet
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
            param.ParameterName = "rsLineLineBreak"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RI.LineLineBreak_" & SiteID & "_" & BusArea
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RI.LineLineBreak", key, 8)

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("PopulateLineLineBreak", , ex)
        Finally
            PopulateLineLineBreak = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
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

    Private Function PopulateFacility() As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            param = New OracleParameter
            param.ParameterName = "rsFacility"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RINEWINCIDENT.FacilityList"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.FacilityList", key, 24)

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("PopulateFacility", , ex)
        Finally
            PopulateFacility = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function

    Private Function PopulateSiteDropDown(ByVal SiteID As String) As DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim key As String = String.Empty

        Try
            key = "ReportDDL_" & SiteID

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SiteID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_inactiveflag"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = "N"
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Division"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = String.Empty
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsDivision"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsFacility"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsBusinessUnit"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsArea"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsLine"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsLinebreak"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsPerson"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RI.ReportDDL", key, 8)

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("PopulateSiteDropDown", , ex)
        Finally
            PopulateSiteDropDown = ds
            If Not ds Is Nothing Then ds = Nothing
        End Try
    End Function
    Private Function PopulateSiteDropDown(ByVal SiteID As String, ByVal BusinessUnit As String) As DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim key As String = String.Empty

        Try

            If BusinessUnit.Length = 0 Then
                ds = PopulateSiteDropDown(SiteID)
                Return ds
                Exit Function
            End If

            key = "BusinessUnitDDL_" & SiteID & "_" & BusinessUnit

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SiteID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_inactiveflag"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = "N"
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Division"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = String.Empty
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_businessUnit"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = BusinessUnit
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsArea"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsLine"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsLinebreak"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RI.BusinessUnitDDL", key, 8)

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("PopulateSiteDropDown", , ex)
        Finally
            PopulateSiteDropDown = ds
            If Not ds Is Nothing Then ds = Nothing
        End Try
    End Function

    Private Function PopulateSiteDropDown(ByVal SiteID As String, ByVal BusinessUnit As String, ByVal Area As String) As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        If BusinessUnit.Length = 0 And Area.Length = 0 Then
            ds = PopulateSiteDropDown(SiteID)
            Return ds
            Exit Function
        ElseIf Area.Length = 0 Then
            ds = PopulateSiteDropDown(SiteID, BusinessUnit)
            Return ds
            Exit Function
        End If

        Try
            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SiteID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_inactiveflag"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = "N"
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Division"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = String.Empty
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_businessUnit"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = BusinessUnit
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_area"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = Area
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsLine"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RI.LineDDL"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RI.LineDDL", key, 0)

        Catch ex As Exception
            RI.SharedFunctions.HandleError("PopulateSiteDropDown", , ex)
        Finally
            PopulateSiteDropDown = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function

    Private Function PopulateSiteDropDown(ByVal SiteID As String, ByVal BusinessUnit As String, ByVal Area As String, ByVal Line As String) As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        If BusinessUnit.Length = 0 And Area.Length = 0 And Line.Length = 0 Then
            ds = PopulateSiteDropDown(SiteID)
            Return ds
            Exit Function
        ElseIf Area.Length = 0 And Line.Length = 0 Then
            ds = PopulateSiteDropDown(SiteID, BusinessUnit)
            Return ds
            Exit Function
        ElseIf Line.Length = 0 Then
            'Line = System.DBNull
            '    ds = PopulateSiteDropDown(SiteID, BusinessUnit, Area)
            '    Return ds
            '    Exit Function
        End If

        Try
            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SiteID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_inactiveflag"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = "N"
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Division"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = String.Empty
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_businessUnit"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = BusinessUnit
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_area"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = Area
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_line"
            param.OracleDbType = OracleDbType.VarChar
            If Line.Length > 0 Then
                param.Value = Line
            End If
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsLineBreak"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RI.LineBreakDDL"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RI.LineBreakDDL", key, 0)

        Catch ex As Exception
            RI.SharedFunctions.HandleError("PopulateSiteDropDown", , ex)
        Finally
            PopulateSiteDropDown = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function
    Private Function PopulateOutageCoord(ByVal SiteID As String) As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SiteID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsPerson"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "Outage.COORD_" & SiteID
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.Person", key, 3)

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("PopulateOutageCoord", , ex)
        Finally
            PopulateOutageCoord = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function
    ''' <summary>
    ''' Gets the list of values for the MTT Person Dropdown List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=0, enableSession:=True), Script.Services.ScriptMethod()> _
  Public Function GetMTTPerson(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr1 As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim SiteId As String = String.Empty
        Dim roleDescription As String = String.Empty
        Try
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            If Not knownCategoryValuesDictionary.ContainsKey("SiteID") Then
                Return Nothing
            Else
                SiteId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("SiteID"))
            End If
            ds = PopulateMTTPerson(SiteId)

            'Dim dr As DataTableReader
            If ds.Tables.Count >= 1 Then
                dr1 = ds.Tables(0).CreateDataReader
            End If

            Do While dr1.Read()
                Dim val As String = RI.SharedFunctions.DataClean(dr1.Item("Name"))
                Dim desc As String = RI.SharedFunctions.DataClean(dr1.Item("Username"))

                Dim spaceChar As String = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                If dr1.Item("RoleDescription") <> roleDescription Then 'New Group
                    'No Roleseqid indicates individual
                    roleDescription = dr1.Item("RoleDescription")
                    If dr1.Item("RoleSeqID") IsNot DBNull.Value Then
                        val = dr1.Item("RoleSeqID")
                    Else
                        val = ""
                        desc = roleDescription
                    End If
                    values.Add(New CascadingDropDownNameValue(roleDescription, val))
                End If

                'If dr1.Item("RoleSeqID") IsNot DBNull.Value Then
                desc = Server.HtmlDecode(spaceChar & dr1.Item("Name"))
                val = dr1.Item("UserName") '& "/" & dr.Item("UserName")
                values.Add(New CascadingDropDownNameValue(desc, val))
                'Else
                'values.Add(New CascadingDropDownNameValue(desc, val))
                'End If

                ' If roleDescription.Length > 0 Then
                'values.Add(New CascadingDropDownNameValue(desc, val))

                'roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:black; font-size:Larger;")
                'End If
                'ddlList.Add(roleItem)
                ' Else
                '    roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:black;")
                '   roleItem.Attributes.Add("disabled", "true")
                '  ddlList.Add(roleItem)
                'End If

                'desc = Server.HtmlDecode(spaceChar & dr1.Item("Name"))
                'If dr1.Item("RoleSeqid").ToString <> "" Then
                ' val = dr1.Item("RoleSeqID")
                'Else
                'val = dr1.Item("UserName")
                ' End If
                'values.Add(New CascadingDropDownNameValue(desc, val))


                'End If
            Loop

            '        Dim spaceChar As String = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
            '            If dr.Item("RoleDescription") <> roleDescription Then 'New Group
            '            'No Roleseqid indicates individual
            '            Dim roleItem As New ListItem
            '            roleDescription = dr.Item("RoleDescription")
            '            roleItem.Text = dr.Item("RoleDescription").ToUpper
            '            If dr.Item("RoleSeqID") IsNot DBNull.Value Then
            '                roleItem.Value = dr.Item("RoleSeqID") '& "/" & dr.Item("UserName")
            '            End If

            '            If ddlList.Count > 0 Then
            '                Dim blankItem As New ListItem
            '                With blankItem
            '                    .Attributes.Add("disabled", "true")
            '                    .Text = ""
            '                    .Value = -1
            '                End With
            '                ddlList.Add(blankItem)
            '            End If

            '            If roleDescription.Length > 0 Then
            '                roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:black; font-size:Larger;")
            '                ddlList.Add(roleItem)
            '            Else
            '                roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:black;")
            '                roleItem.Attributes.Add("disabled", "true")
            '                ddlList.Add(roleItem)
            '            End If

            '        End If

            '  Loop

            'End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetMTTPerson", , ex)
        Finally
            GetMTTPerson = values.ToArray
            If ds IsNot Nothing Then
                ds = Nothing
            End If
            If dr1 IsNot Nothing Then
                dr1.Close()
                dr1 = Nothing
            End If
        End Try
    End Function
    ''' <summary>
    ''' Gets the list of values for the MOC Initiator Dropdown List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=0, enableSession:=True), Script.Services.ScriptMethod()>
    Public Function GetMOCInitiator(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim SiteId As String = String.Empty
        Try
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            If Not knownCategoryValuesDictionary.ContainsKey("SiteID") Then
                Return Nothing
            Else
                SiteId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("SiteID"))
            End If
            ds = PopulateMOCInitiator(SiteId)
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If

                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("UserName"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("Person"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetMOCInitiator", , ex)
        Finally
            GetMOCInitiator = values.ToArray
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
    ''' Gets the list of values for the MOC Owners Dropdown List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(CacheDuration:=0, EnableSession:=True), Script.Services.ScriptMethod()>
    Public Function GetMOCOwner(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim SiteId As String = String.Empty
        Try
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            If Not knownCategoryValuesDictionary.ContainsKey("SiteID") Then
                Return Nothing
            Else
                SiteId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("SiteID"))
            End If
            ds = PopulateMOCOwner(SiteId)
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If

                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("OwnerUserName"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("Person"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetMOCOwner", , ex)
        Finally
            GetMOCOwner = values.ToArray
            If ds IsNot Nothing Then
                ds = Nothing
            End If
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
        End Try
    End Function
    Private Function PopulateMTTPerson(ByVal SiteID As String) As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = SiteID
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsResponsibleList"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "NewMOC.GetResponsibleList", "MOCPerson" & SiteID, 2)

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("PopulateMTTPerson", , ex)
        Finally
            PopulateMTTPerson = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Function

    Private Function PopulateMOCInitiator(ByVal SiteID As String) As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = SiteID
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCInitiator"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "ViewMOC.GetMOCInitiator", "MOCInitiator" & SiteID, 2)

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("PopulateMOCInitiator", , ex)
        Finally
            PopulateMOCInitiator = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Function
    Private Function PopulateMOCOwner(ByVal SiteID As String) As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = SiteID
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCOwner"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "ViewMOC.GetMOCOwner", "MOCOwner" & SiteID, 2)

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("PopulateMOCOwner", , ex)
        Finally
            PopulateMOCOwner = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Function
    ''' <summary>
    ''' Gets the list of values for the Outage Critique Categories
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks> 
    <Services.WebMethod(cacheduration:=1800, enableSession:=True), Script.Services.ScriptMethod()> _
     Public Function GetCritiqueCategory(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing

        Try

            ds = PopulateOutageCritiqueCategory()
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If


                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("CritiqueCategorySeqId"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("CritiqueCategory"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetCritiqueCategory", , ex)
        Finally
            GetCritiqueCategory = values.ToArray 'RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
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
    ''' Gets the list of values for the Business Unit/Area Dropdown List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=1800, enableSession:=True), Script.Services.ScriptMethod()> _
    Public Function GetCritiqueSubCategory(ByVal knownCategoryValues As String, ByVal category As String, ByVal contextKey As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim CritiqueCategorySeqId As String = String.Empty
        Dim Outagenumber As String = String.Empty

        Try
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            If Not (knownCategoryValuesDictionary.ContainsKey("CritiqueCategory") Or knownCategoryValuesDictionary.ContainsKey("Undefined")) Then
                Return Nothing
            ElseIf knownCategoryValuesDictionary.ContainsKey("CritiqueCategory") Then
                CritiqueCategorySeqId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("CritiqueCategory"))
            Else
                CritiqueCategorySeqId = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("Undefined"))
            End If

            If contextKey.Length > 0 Then
                Outagenumber = contextKey
            End If

            ds = PopulateOutageCritiqueSubCategory(Outagenumber, CritiqueCategorySeqId)
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If

                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("CritiqueSubCategorySeqId"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("CritiqueSubCategory"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetCritiqueSubCategory", , ex)
        Finally
            GetCritiqueSubCategory = values.ToArray 'RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray, " - ")
            If ds IsNot Nothing Then
                ds = Nothing
            End If
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
        End Try

    End Function

    Private Function PopulateOutageCritiqueCategory() As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            param = New OracleParameter
            param.ParameterName = "rsCritiqueCategory"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Outage_Critique.GetCritiqueCategory", "CritiqueCategory", 2)

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("PopulateOutageCritiqueCategory", , ex)
        Finally
            PopulateOutageCritiqueCategory = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Function

    Private Function PopulateOutageCritiqueSubCategory(ByVal OutageNumber As String, ByVal CritiqueCategorySeqID As String) As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            param = New OracleParameter
            param.ParameterName = "in_Outagenumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = OutageNumber
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_CritiqueCategorySeqId"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = CritiqueCategorySeqID
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "rsCritiqueSubCategory"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Outage_Critique.GetCritiqueSubCategory", "CritiqueSubCategory" & CritiqueCategorySeqID, 2)

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("PopulateOutageCritiqueSubCategory", , ex)
        Finally
            PopulateOutageCritiqueSubCategory = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Function
   
    Shared Function CleanKnownCategoryValues(ByVal knownCategoryValues As String) As String
        Return Replace(knownCategoryValues, "undefined:", "SiteID:")
    End Function
    'Protected Overrides Sub Finalize()
    '    MyBase.Finalize()
    'End Sub
End Class
