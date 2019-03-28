Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports AjaxControlToolkit
Imports System.Data
Imports Devart.Data.Oracle

''' <summary>
''' CascadingDropDown is an ASP.NET AJAX extender that can be attached to an ASP.NET DropDownList control to get automatic population of a set of DropDownList controls. Each time the selection of one the DropDownList controls changes, the CascadingDropDown makes a call to a specified web service to retrieve the list of values for the next DropDownList in the set. 
''' </summary>
''' <remarks></remarks>
<WebService(Namespace:="http://RI/")> _
<System.Web.Script.Services.ScriptService()> _
Public Class OutageCascadingLists
    Inherits System.Web.Services.WebService

    <WebMethod(EnableSession:=True)> _
  Public Function DeleteResource(ByVal ResourceSeqId As String) As String

        Dim dr As Devart.Data.Oracle.OracleDataReader = Nothing
        Dim paramCollection As New OracleParameterCollection
        Dim status As String
        Dim userProfile As RI.CurrentUserProfile = Nothing
        userProfile = RI.SharedFunctions.GetUserProfile

        Try
            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "inResourceSeqID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = ResourceSeqId
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "inUserID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userProfile.Username
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "outagemaint.deleteresource")
        Catch ex As Exception
            Throw New DataException("DeleteResource", ex)
        Finally
            If Not dr Is Nothing Then dr = Nothing
        End Try
        Return status
    End Function
    ''' <summary>
    ''' Gets the list of values for the Facility List
    ''' </summary>
    ''' <param name="knownCategoryValues"></param>
    ''' <param name="category"></param>
    ''' <param name="contextKey"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
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
                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("SiteId"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("SiteName"))
                    If contextKey.Length > 0 And contextKey <> "AL" Then
                        If contextKey.Contains(val) Then values.Add(New CascadingDropDownNameValue(desc, val))
                    Else
                        values.Add(New CascadingDropDownNameValue(desc, val))
                    End If

                Loop
            End If

        Catch ex As Exception
            Throw
        Finally
            GetFacilityList = values.ToArray
            If ds IsNot Nothing Then
                ds = Nothing
            End If
        End Try

    End Function



    <WebMethod()> _
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
            Throw
        Finally
            GetPerson = values.ToArray
            If ds IsNot Nothing Then
                ds = Nothing
            End If
        End Try

    End Function
    
    <WebMethod()> _
    Public Function GetDivision(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
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
            Dim defaultDivision As String = Me.GetSiteDivision(siteId)
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If
                Do While dr.Read()
                    Dim val As String = dr.Item("Division")
                    Dim desc As String = dr.Item("Division")
                    Dim defaultValue As Boolean = False
                    If val.ToUpper = defaultDivision.ToUpper Then defaultValue = True
                    values.Add(New CascadingDropDownNameValue(desc, val, defaultValue))
                Loop
            End If

        Catch ex As Exception
            Throw
        Finally
            GetDivision = values.ToArray
            If ds IsNot Nothing Then
                ds = Nothing
            End If
        End Try

    End Function

    <WebMethod()> _
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
                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("RISUPERAREA"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("RISUPERAREA"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
            End If

        Catch ex As Exception
            Throw
        Finally
            GetBusinessUnit = values.ToArray
            If ds IsNot Nothing Then
                ds = Nothing
            End If
        End Try

    End Function

    <WebMethod()> _
  Public Function GetArea(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
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
                    dr = ds.Tables(3).CreateDataReader
                End If
                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("SUBAREA"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("SUBAREA"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
            End If

        Catch ex As Exception
            Throw
        Finally
            GetArea = values.ToArray
            If ds IsNot Nothing Then
                ds = Nothing
            End If
        End Try

    End Function

    <WebMethod()> _
  Public Function GetLine(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
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
                If ds.Tables.Count >= 5 Then
                    dr = ds.Tables(4).CreateDataReader
                End If
                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("AREA"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("AREA"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
            End If

        Catch ex As Exception
            Throw
        Finally
            GetLine = values.ToArray
            If ds IsNot Nothing Then
                ds = Nothing
            End If
        End Try

    End Function
    <WebMethod()> _
        Public Function GetSiteDivision(ByVal SiteId As String) As String
        Dim ds As DataSet = PopulateFacility()
        Dim ret As String = String.Empty
        If ds IsNot Nothing Then
            Dim dr As DataTableReader = ds.Tables(0).CreateDataReader
            Do While dr.Read
                If dr.Item("SiteID") IsNot Nothing Then
                    If dr.Item("SiteID") = SiteId Then
                        ret = dr.Item("Division")
                    End If
                End If
            Loop
        End If
        Return ret
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

            Dim key As String = "Outage.Person" & SiteID
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.Person", key, 3)

        Catch ex As Exception
            Throw
        Finally
            PopulatePerson = ds
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

            Dim key As String = "Outage.FacilityList"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.FacilityList", key, 3)

        Catch ex As Exception
            Throw
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
            key = "OutageViewUpdate_" & SiteID

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

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.Dropdownddl", key, 3)

        Catch ex As Exception
            ds = Nothing
            Throw New DataException("GetSiteDropDown", ex)
        Finally
            PopulateSiteDropDown = ds
            If Not ds Is Nothing Then ds = Nothing
        End Try
    End Function
    Shared Function CleanKnownCategoryValues(ByVal knownCategoryValues As String) As String
        Return Replace(knownCategoryValues, "undefined:", "SiteID:")
    End Function
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    <Script.Services.ScriptMethod()> _
    Public Function PopulateBusinessUnitAreaLine(ByVal SiteId As String, ByVal BusArea As String, ByVal LineBreak As String) As String
        Dim dr As DataTableReader = Nothing
        Dim ds As DataSet = Nothing
        Dim items As String = String.Empty
        Dim busAreaWhere As String = String.Empty
        Dim lineBreakWhere As String = String.Empty
        Try
            Dim sql As String = "Select * FROM RefSiteArea "
            'If BusArea.Length > 0 Then
            'busAreaWhere = String.Format(" and RISuperArea|| ' - ' ||Subarea='{0}'", BusArea)
            'End If
            'If LineBreak.Length > 0 Then
            'If InStr(LineBreak, "-") = 0 Then LineBreak = LineBreak & " - None"
            'lineBreakWhere = String.Format(" and area|| ' - ' ||linebreak='{0}'", LineBreak)
            'End If
            sql = String.Format(sql, SiteId)
            Dim key As String = "OUTAGE_BusUnitArea_" & RI.SharedFunctions.CreateKey(sql)

            ds = RI.SharedFunctions.GetOracleDataSet(sql)

            If ds IsNot Nothing Then
                dr = ds.Tables(0).CreateDataReader
                If dr IsNot Nothing Then
                    dr.Read()
                    If dr.HasRows Then
                        items = RI.SharedFunctions.DataClean(dr.Item("subarea")) & " * " & RI.SharedFunctions.DataClean(dr.Item("subarea"))
                    End If
                End If
            End If
            Return items
        Catch ex As Exception
            Throw New ApplicationException("Exception Occured")
        Finally

            If ds IsNot Nothing Then ds = Nothing
            If dr IsNot Nothing Then dr = Nothing
        End Try
    End Function
End Class
