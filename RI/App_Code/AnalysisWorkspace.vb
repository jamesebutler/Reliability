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
Public Class AnalysisWorkspace
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
    Public Function GetFailureModeCause(ByVal knownCategoryValues As String, ByVal category As String, ByVal contextKey As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim ModeSeqID As String = String.Empty

        Try
            knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            If Not knownCategoryValuesDictionary.ContainsKey("ModeSeqID") Then
                Return Nothing
            Else
                ModeSeqID = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("SiteID"))
            End If
            If contextKey Is Nothing Then contextKey = String.Empty
            ds = PopulateModeCauses(ModeSeqID)
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
            RI.SharedFunctions.HandleError("GetFacilityList", , ex)
        Finally
            GetFailureModeCause = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
            If ds IsNot Nothing Then
                ds = Nothing
            End If
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
        End Try

    End Function
    Private Function PopulateModeCauses(ByVal FailureModeSeqid As String) As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New OracleParameter
            param.ParameterName = "in_failuremodeseqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = FailureModeSeqid
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsFailureCause"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RI.FailureCause" & FailureModeSeqid
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINewIncident.FailureCause", key, 8)

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("PopulateModeCauses", , ex)
        Finally
            PopulateModeCauses = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function

    Shared Function CleanKnownCategoryValues(ByVal knownCategoryValues As String) As String
        Return Replace(knownCategoryValues, "undefined:", "SeqID:")
    End Function
End Class
