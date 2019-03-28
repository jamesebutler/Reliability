Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Devart.Data.Oracle
Imports System.Data
Imports System.Text
Imports RI
Imports AjaxControlToolkit


<WebService(Namespace:="http://RI/")> _
<System.Web.Script.Services.ScriptService()> _
Public Class MOCWS
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)> _
    Public Function GetMOCQuestions(ByVal siteID As String, ByVal bustype As String, ByVal division As String, ByVal classID As String, ByVal type As String) As String

        Dim values As New Generic.List(Of String)

        Dim ds As DataSet = Nothing
        Dim paramCollection As New OracleParameterCollection
        Try

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_type"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = type '"3"
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_TypeSeqID"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = classID '"3"
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_SiteID"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = siteID '"GT"
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Bustype"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = bustype
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Division"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = division
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsQuestionList"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "MOCMaint.GetQuestionList", "MOCQuestions", 0)

            Dim dr As DataTableReader = ds.CreateDataReader
            Dim dt As New Data.DataTable

            If dr IsNot Nothing Then
                dt.Load(dr)
            End If

            Dim data As String = RI.SharedFunctions.GetJson(dt)
            Return data

        Catch ex As Exception
            Throw New DataException("GetClassQuestionList", ex)
            Return Nothing
        Finally
            If Not ds Is Nothing Then ds = Nothing
        End Try

    End Function

    <WebMethod(EnableSession:=True)> _
    Public Function GetDivMOCQuestions(ByVal siteID As String, ByVal bustype As String, ByVal division As String, ByVal classID As String, ByVal type As String) As String

        Dim values As New Generic.List(Of String)

        Dim ds As DataSet = Nothing
        Dim paramCollection As New OracleParameterCollection
        Try

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_type"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = type '"3"
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_TypeSeqID"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = classID '"3"
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_SiteID"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = siteID '"GT"
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Bustype"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = bustype
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Division"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = division
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsQuestionList"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "MOCMaint.GetDivQuestionList", "MOCDivQuestions", 0)

            Dim dr As DataTableReader = ds.CreateDataReader
            Dim dt As New Data.DataTable

            If dr IsNot Nothing Then
                dt.Load(dr)
            End If

            Dim data As String = RI.SharedFunctions.GetJson(dt)
            Return data

        Catch ex As Exception
            Throw New DataException("GetClassQuestionList", ex)
            Return Nothing
        Finally
            If Not ds Is Nothing Then ds = Nothing
        End Try

    End Function
    <WebMethod()> _
    Public Function SaveQuestions(type As String, classseq As String, bustype As String, division As String, site As String, username As String, data As String) As String

        Dim classid As String = classseq
        Dim siteid As String = site
        Dim questions = data
        'MsgBox(questions)

        Try
            ''    '    'Dim ds As DataSet = Nothing
            Dim part As String
            Dim strTitle As String = String.Empty,
                strType As String = String.Empty,
                strQuestionID As String = String.Empty,
                strInactive As String = String.Empty,
                strSubTitle As String = String.Empty
            Dim records = questions.Replace("],[", "~").Replace("[", "").Replace("]", "")
            Dim splitrecords = records.Split("~")
            Dim sortorder As Integer = 0

            For Each part In splitrecords
                sortorder = sortorder + 1
                Using rdr As New IO.StringReader(part)
                    Using parser As New FileIO.TextFieldParser(rdr)
                        parser.TextFieldType = FileIO.FieldType.Delimited
                        parser.Delimiters = New String() {","}
                        parser.HasFieldsEnclosedInQuotes = True
                        Dim fields() As String = parser.ReadFields()

                        For i As Integer = 0 To fields.Length - 1
                            'strClassCat = fields(0)
                            strTitle = fields(0)
                            'strSubTitle = fields(1)
                            strType = fields(1)
                            strInactive = fields(2)
                            strQuestionID = fields(3)
                        Next
                    End Using
                End Using

                Dim outstatus As String = "0"

                Dim paramCollection As New OracleParameterCollection

                Dim param As New OracleParameter

                param = New OracleParameter
                param.ParameterName = "in_type"
                param.OracleDbType = OracleDbType.VarChar
                param.Value = type
                param.Direction = ParameterDirection.Input
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_questionseqid"
                param.OracleDbType = OracleDbType.VarChar
                param.Value = strQuestionID
                param.Direction = ParameterDirection.Input
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_questiontitle"
                param.OracleDbType = OracleDbType.NVarChar
                param.Value = strTitle
                param.Direction = ParameterDirection.Input
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_questionsubtitle"
                param.OracleDbType = OracleDbType.NVarChar
                param.Value = strSubTitle
                param.Direction = ParameterDirection.Input
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_questiontype"
                param.OracleDbType = OracleDbType.VarChar
                param.Value = strType
                param.Direction = ParameterDirection.Input
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_TypeSeqID"
                param.OracleDbType = OracleDbType.VarChar
                param.Value = classid
                param.Direction = ParameterDirection.Input
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_bustype"
                param.OracleDbType = OracleDbType.VarChar
                param.Value = bustype
                param.Direction = ParameterDirection.Input
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_division"
                param.OracleDbType = OracleDbType.VarChar
                param.Value = division
                param.Direction = ParameterDirection.Input
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_siteid"
                param.OracleDbType = OracleDbType.VarChar
                param.Value = siteid
                param.Direction = ParameterDirection.Input
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_username"
                param.OracleDbType = OracleDbType.VarChar
                param.Value = username
                param.Direction = ParameterDirection.Input
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_inactive"
                param.OracleDbType = OracleDbType.VarChar
                param.Value = strInactive
                param.Direction = ParameterDirection.Input
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_sortorder"
                param.OracleDbType = OracleDbType.VarChar
                param.Value = sortorder
                param.Direction = ParameterDirection.Input
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "out_status"
                param.OracleDbType = OracleDbType.Number
                param.Direction = ParameterDirection.Output
                paramCollection.Add(param)

                Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "MOCMaint.SaveQuestions")

                If returnStatus <> 0 Then
                    Return "Error"

                End If
                'ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "MOCMaint.SaveQuestions", "Save", 0)

                'End If
            Next

            Return "OK"
            '    outstatus = RI.MOCSharedFunctions.ExecuteSQL("update refemployee set siteid ='TE' where username = 'AALBRIN'")
            'Return ds
            'Try
            'Return returnStatus
        Catch ex As Exception
            Throw New DataException("SaveQuestions", ex)
            RI.SharedFunctions.HandleError("error")
            Return Nothing
            'Finally
            '   If Not ds Is Nothing Then ds = Nothing
        End Try

        Return Nothing

    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function GetBusType(ByVal username As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = username
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsBusType"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "MOCMAINT.BusType"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.MOCMAINT.GetBusType", key, 0)
            Dim dt As New Data.DataTable

            Dim dr As DataTableReader = ds.CreateDataReader
            If dr IsNot Nothing Then
                dt.Load(dr)
            End If

            Dim data As String = RI.SharedFunctions.GetJson(dt)
            Return data

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("GetBusType", , ex)
            Return Nothing
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function

    <WebMethod(EnableSession:=True)> _
    Public Function GetDivision(ByVal bustype As String, ByVal username As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            param = New OracleParameter
            param.ParameterName = "in_bustype"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = bustype '"AALBRIN" 
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = username
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsDivision"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "MOCMAINT.FacilityList" & bustype
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.MOCMAINT.GetDivision", key, 0)
            Dim dt As New Data.DataTable

            Dim dr As DataTableReader = ds.CreateDataReader
            If dr IsNot Nothing Then
                dt.Load(dr)
            End If

            Dim data As String = RI.SharedFunctions.GetJson(dt)
            Return data

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("GetDivisionByUser", , ex)
            Return Nothing
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function

    <WebMethod(EnableSession:=True)> _
    Public Function GetFacility(ByVal division As String, ByVal username As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            param = New OracleParameter
            param.ParameterName = "in_division"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = division '"AALBRIN" 
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = username
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsFacility"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "MOCMAINT.GETFACILITY" & division
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.MOCMAINT.GETFACILITY", key, 0)
            Dim dt As New Data.DataTable

            Dim dr As DataTableReader = ds.CreateDataReader
            If dr IsNot Nothing Then
                dt.Load(dr)
            End If

            Dim data As String = RI.SharedFunctions.GetJson(dt)
            Return data

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("GetFacility", , ex)
            Return Nothing
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function

    <WebMethod(EnableSession:=True)> _
    Public Function GetFacilityInfo(ByVal siteid As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = siteid '"AALBRIN" 
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsResult"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "MOCMAINT.GetFacilityInfo" & siteid
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.MOCMAINT.GetFacilityInfo", key, 0)
            Dim dt As New Data.DataTable

            Dim dr As DataTableReader = ds.CreateDataReader
            If dr IsNot Nothing Then
                dt.Load(dr)
            End If

            Dim data As String = RI.SharedFunctions.GetJson(dt)
            Return data

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("GetFacilityInfo", , ex)
            Return Nothing
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function
    Public Shared Function GetUserProfile() As CurrentUserProfile
        Dim userProfile As CurrentUserProfile = Nothing
        Dim username As String
        username = CurrentUserProfile.GetCurrentUser
        Dim ses As System.Web.SessionState.HttpSessionState = HttpContext.Current.Session
        If ses IsNot Nothing Then
            If ses.Item(Replace(username, "\", "_")) IsNot Nothing Then
                userProfile = CType(ses.Item(Replace(username, "\", "_")), CurrentUserProfile)
            End If
        End If
        If userProfile Is Nothing Then userProfile = New CurrentUserProfile
        If Not userProfile Is Nothing Then
            userProfile.SetProfileDefaults()
            ses.Add(Replace(username, "\", "_"), userProfile)
        End If
        Return userProfile

    End Function

    'Public Function GetDivisionList(ByVal knownCategoryValues As String, ByVal category As String, ByVal contextKey As String) As CascadingDropDownNameValue()
    '    Dim values As New Generic.List(Of CascadingDropDownNameValue)
    '    Dim ds As System.Data.DataSet = Nothing
    '    Dim dr As System.Data.DataTableReader = Nothing

    '    Try
    '        If contextKey Is Nothing Then contextKey = String.Empty
    '        ds = PopulateFacility()
    '        If ds IsNot Nothing Then
    '            If ds.Tables.Count >= 1 Then
    '                dr = ds.Tables(0).CreateDataReader
    '            End If
    '            If contextKey = "All" Then
    '                values.Add(New CascadingDropDownNameValue(RI.SharedFunctions.LocalizeValue("All"), "AL"))
    '                contextKey = String.Empty
    '            End If

    '            Dim selectedDivision As String = "All"
    '            Dim knownCategoryValuesDictionary As New StringDictionary
    '            If knownCategoryValues IsNot Nothing AndAlso knownCategoryValues.Length > 0 Then
    '                knownCategoryValues = CleanKnownCategoryValues(knownCategoryValues)
    '                knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
    '                If (knownCategoryValuesDictionary.ContainsKey("Division")) Then
    '                    selectedDivision = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("Division"))
    '                End If
    '            End If
    '            Do While dr.Read()
    '                If dr.Item("Division") = selectedDivision OrElse selectedDivision = "All" Then
    '                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("SiteId"))
    '                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("SiteName"))
    '                    If contextKey.Length > 0 And contextKey <> "AL" Then
    '                        If contextKey.Contains(val) Then values.Add(New CascadingDropDownNameValue(desc, val))
    '                    Else
    '                        values.Add(New CascadingDropDownNameValue(desc, val))
    '                    End If
    '                End If
    '            Loop
    '        End If

    '    Catch ex As Exception
    '        RI.SharedFunctions.HandleError("GetFacilityList", , ex)
    '    Finally
    '        GetFacilityList = RI.SharedFunctions.LocalizeDropDownNameValue(values.ToArray)
    '        If ds IsNot Nothing Then
    '            ds = Nothing
    '        End If
    '        If dr IsNot Nothing Then
    '            dr.Close()
    '            dr = Nothing
    '        End If
    '    End Try

    'End Function
End Class