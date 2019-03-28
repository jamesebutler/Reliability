Imports Devart.Data.Oracle

<Serializable()> _
Public Class clsOutageViewSearch
    Public Property Facility() As String
        Get
            Return mFacility
        End Get
        Set(ByVal value As String)
            mFacility = value
        End Set
    End Property
    Public Property BusinessUnit() As String
        Get
            Return mBusinessUnit
        End Get
        Set(ByVal value As String)
            mBusinessUnit = value
        End Set
    End Property
    Public Property Area() As String
        Get
            Return mArea
        End Get
        Set(ByVal value As String)
            mArea = value
        End Set
    End Property
    Public Property Line() As String
        Get
            Return mLine
        End Get
        Set(ByVal value As String)
            mLine = value
        End Set
    End Property
    Public Property Person() As String
        Get
            Return mPerson
        End Get
        Set(ByVal value As String)
            mPerson = value
        End Set
    End Property
    Public Property Contractor() As String
        Get
            Return mContractor
        End Get
        Set(ByVal value As String)
            mContractor = value
        End Set
    End Property
    Public Property Division() As String
        Get
            Return mDivision
        End Get
        Set(ByVal value As String)
            mDivision = value
        End Set
    End Property
    Public Property StartDate() As String
        Get
            Return mStartDate
        End Get
        Set(ByVal value As String)
            mStartDate = value
        End Set
    End Property
    Public Property EndDate() As String
        Get
            Return mEndDate
        End Get
        Set(ByVal value As String)
            mEndDate = value
        End Set
    End Property
    Public Property DateRange() As String
        Get
            Return mDateRange
        End Get
        Set(ByVal value As String)
            mDateRange = value
        End Set
    End Property
    Public Property SDCategory() As String
        Get
            Return mSDCategory
        End Get
        Set(ByVal value As String)
            mSDCategory = value
        End Set
    End Property

    Public Property OutageCoord() As String
        Get
            Return mOutageCoord
        End Get
        Set(ByVal value As String)
            mOutageCoord = value
        End Set
    End Property
    Public Property Resources() As String
        Get
            Return mResources
        End Get
        Set(ByVal value As String)
            mResources = value
        End Set
    End Property
    Public ReadOnly Property ResourcesList() As clsData
        Get
            Return mResourcesList
        End Get
    End Property
    Public ReadOnly Property ContractorList() As clsData
        Get
            Return mContractorList
        End Get
    End Property
    Public Property AnnualFlag() As String
        Get
            Return mAnnualFlag
        End Get
        Set(ByVal value As String)
            mAnnualFlag = value
        End Set
    End Property

    Public Property OrderBy() As String
        Get
            Return mOrderBy
        End Get
        Set(ByVal value As String)
            mOrderBy = value
        End Set
    End Property
    Public Property AndOr() As String
        Get
            Return mAndOr
        End Get
        Set(ByVal value As String)
            mAndOr = value
        End Set
    End Property
    Public Property Title() As String
        Get
            Return mTitle
        End Get
        Set(ByVal value As String)
            mTitle = value
        End Set
    End Property
    Public Property SelectStatement() As String
        Get
            Return mSelect
        End Get
        Set(ByVal value As String)
            mSelect = value
        End Set
    End Property

    'Private Property SearchDT() As Data.DataTable
    '    Get
    '        Return mSearchDT
    '    End Get
    '    Set(ByVal value As Data.DataTable)
    '        mSearchDT = value
    '    End Set
    'End Property
    Public Property SearchData() As OracleDataReader
        Get
            Return mSearchData
        End Get
        Set(ByVal value As OracleDataReader)
            mSearchData = value
        End Set
    End Property
    Public Property ExcelData() As OracleDataReader
        Get
            Return mSearchData
        End Get
        Set(ByVal value As OracleDataReader)
            mSearchData = value
        End Set
    End Property
    Public Function Search() As OracleDataReader
        'Perform Search 
        GetOutageListing()
        Return SearchData
    End Function
    Private Sub GetOutageListing(Optional ByVal createDataTable As Boolean = False)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        'Dim ds As System.Data.DataSet = Nothing
        'Dim activeFlag As String = "N"
        Dim iploc As New IP.Bids.Localization.WebLocalization()

        'Check input paramaters

        Try

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Facility
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Division"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Division
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
            param.ParameterName = "in_Line"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Line
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Title"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Title
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_StartDate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = StartDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_EndDate"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = EndDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_SDCategory"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SDCategory
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_OutageCoord"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = OutageCoord
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Contractor"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Contractor
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Annual"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = AnnualFlag
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Locale"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = iploc.CurrentLocale.ToString()
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Resource"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Resources
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_OrderBy"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = OrderBy
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_AndOr"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = AndOr
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "RS"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            'Dim key As String = "OutageViewSearch_" & Facility & "_" & Division & "_" & BusinessUnit & "_" & Area & "_" & Line & "_" & Title & "_" & OutageCoord & "_" & SDCategory & "_" & StartDate & "_" & EndDate & "_" & OrderBy
            'ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.OutageListing", key, 0)
            Dim dr As OracleDataReader = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "Reladmin.Outage.OutageListing")

            If dr IsNot Nothing Then
                If createDataTable = True Then
                    Dim dt As New Data.DataTable
                    dt.Load(dr)
                    'Me.SearchDT = dt
                End If

                Me.SearchData = dr 'ds.Tables(0).CreateDataReader
                'dr.Close()
                'dr = Nothing
            Else
                SearchData = Nothing
            End If

            'If ds IsNot Nothing Then

            '    If ds.Tables.Count = 1 Then
            '        Me.SearchData = ds.Tables(0).CreateDataReader
            '    Else
            '        SearchData = Nothing
            '    End If
            'End If
            'If Me.SearchData.HasRows = False Then
            '    HttpRuntime.Cache.Remove(key)
            'End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function Gantt() As OracleDataReader
        'Perform Search 
        GetOutageGanttListing()
        Return SearchData
    End Function
    Private Sub GetOutageGanttListing(Optional ByVal createDataTable As Boolean = False)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        'Dim ds As System.Data.DataSet = Nothing
        'Dim activeFlag As String = "N"

        'Check input paramaters

        Try

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Facility
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Division"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Division
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_BusinessUnit"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = BusinessUnit
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Area"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Area
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Line"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Line
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Title"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Title
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_StartDate"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = StartDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_EndDate"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = EndDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_SDCategory"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SDCategory
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_OutageCoord"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = OutageCoord
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Annual"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = AnnualFlag
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_OrderBy"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = OrderBy
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_AndOr"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = AndOr
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "RSBlock"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "RSGroup"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            'Dim key As String = "GanttOutageView" ' & Facility & "_" & Division & "_" & BusinessUnit & "_" & Area & "_" & Line & "_" & Title & "_" & OutageCoord & "_" & SDCategory & "_" & StartDate & "_" & EndDate & "_" & OrderBy
            'ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.OutageGanttListing", key, 5)
            Dim dr As OracleDataReader = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "Reladmin.Outage.OutageGanttListing")

            If dr IsNot Nothing Then
                If createDataTable = True Then
                    Dim dt As New Data.DataTable
                    dt.Load(dr)
                    'Me.SearchDT = dt
                End If

                Me.SearchData = dr 'ds.Tables(0).CreateDataReader
                'dr.Close()
                'dr = Nothing
            Else
                SearchData = Nothing
            End If
            'Dim strXML As String
            'strXML = ds.GetXml
            'If ds IsNot Nothing Then

            '    If ds.Tables.Count = 2 Then

            '        Me.SearchData = ds.Tables(1).CreateDataReader
            '        strXML = ds.GetXml()
            '    Else
            '        SearchData = Nothing
            '    End If
            'End If
            'If Me.SearchData.HasRows = False Then
            '    HttpRuntime.Cache.Remove(key)
            'End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function ExcelSearch() As OracleDataReader
        GetExcelListing()
        Return ExcelData
    End Function
    Private Sub GetExcelListing(Optional ByVal createDataTable As Boolean = False)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        'Dim ds As System.Data.DataSet = Nothing
        'Dim ActiveFlag As String = "N"
        Dim iploc As New IP.Bids.Localization.WebLocalization()

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Facility
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Division"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Division
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_BusinessUnit"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = BusinessUnit
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Area"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Area
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Line"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Line
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Title"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Title
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_StartDate"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = StartDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_EndDate"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = EndDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_SDCategory"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SDCategory
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_OutageCoord"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = OutageCoord
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Contractor"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Contractor
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Annual"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = AnnualFlag
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Locale"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = iploc.CurrentLocale.ToString()
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Resource"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Resources
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_OrderBy"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = OrderBy
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_AndOr"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = AndOr
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "RS"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            'Dim key As String = "OutageExcelSearch_" & Facility & "_" & Division & "_" & BusinessUnit & "_" & Area & "_" & Line & "_" & Title & "_" & OutageCoord & "_" & SDCategory & "_" & StartDate & "_" & EndDate & "_" & OrderBy
            'ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.OutageExcelListing", key, 1)
            Dim dr As OracleDataReader = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "Reladmin.Outage.OutageExcelListing")

            If dr IsNot Nothing Then
                If createDataTable = True Then
                    Dim dt As New Data.DataTable
                    dt.Load(dr)
                    'Me.SearchDT = dt
                End If

                Me.SearchData = dr 'ds.Tables(0).CreateDataReader
                'dr.Close()
                'dr = Nothing
            Else
                SearchData = Nothing
            End If
            'If ds IsNot Nothing Then
            '    If ds.Tables.Count = 1 Then
            '        Me.ExcelData = ds.Tables(0).CreateDataReader
            '    Else
            '        ExcelData = Nothing
            '    End If
            'End If
            'If Me.ExcelData.HasRows = False Then
            '    HttpRuntime.Cache.Remove(key)
            'End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub GetData(ByVal facility As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        'Dim activeFlag As String = String.Empty

        'Check input paramaters

        Try
            'If inActiveFlag = True Then
            '    ActiveFlag = "Y"
            'Else
            '    ActiveFlag = "N"
            'End If

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = facility
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_inactiveflag"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = ""
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Division"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Division
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsDivision"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsFacility"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsBusinessUnit"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsArea"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsLine"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsLineBreak"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsPerson"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsContractor"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsResources"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "ViewDropdownddl_" & facility & "_" & Division & "_" & ""
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.ViewDropdownddl", key, 3)


            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    'Contractor                   
                    ContractorList.DataSource = ds.Tables(7).CreateDataReader
                    ContractorList.DataTextField = "contractor"
                    ContractorList.DataValueField = "contractorseqid"

                    'M&R Lead                   
                    ResourcesList.DataSource = ds.Tables(8).CreateDataReader
                    ResourcesList.DataTextField = "lead"
                    ResourcesList.DataValueField = "username"

                End If
            End If

        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                ds = Nothing
            End If

        End Try
    End Sub

    'Private mSearchDT As Data.DataTable
    Private mSearchData As OracleDataReader
    '    Private mSearchData As System.Data.DataTableReader
    Private mFacility As String
    Private mDivision As String
    Private mBusinessUnit As String
    Private mArea As String
    Private mLine As String
    Private mPerson As String
    Private mContractor As String
    Private mStartDate As String
    Private mEndDate As String
    Private mDateRange As String
    Private mSDCategory As String
    Private mOutageCoord As String
    Private mOrderBy As String
    Private mAndOr As String
    Private mTitle As String
    Private mSelect As String
    Private mAnnualFlag As String
    Private mResources As String
    ReadOnly mResourcesList As New clsData
    ReadOnly mContractorList As New clsData

    Public Sub New()
        GetData(Facility)
    End Sub
End Class

'<Serializable()> _
'Public Class clsOutageViewUpdate
'    Private Sub GetData(ByVal facility As String)
'        Dim paramCollection As New OracleParameterCollection
'        Dim param As New OracleParameter
'        Dim ds As System.Data.DataSet = Nothing
'        Dim ActiveFlag As String = String.Empty

'        'Check input paramaters

'        Try
'            'If inActiveFlag = True Then
'            '    ActiveFlag = "Y"
'            'Else
'            '    ActiveFlag = "N"
'            'End If

'            param = New OracleParameter
'            param.ParameterName = "in_siteid"
'            param.oracledbtype = oracledbtype.VarChar
'            param.Direction = Data.ParameterDirection.Input
'            param.Value = facility
'            paramCollection.Add(param)

'            'param = New OracleParameter
'            'param.ParameterName = "in_inactiveflag"
'            'param.oracledbtype = oracledbtype.VarChar
'            'param.Direction = Data.ParameterDirection.Input
'            'param.Value = inActiveFlag
'            'paramCollection.Add(param)

'            'param = New OracleParameter
'            'param.ParameterName = "in_Division"
'            'param.oracledbtype = oracledbtype.VarChar
'            'param.Direction = Data.ParameterDirection.Input
'            'param.Value = division
'            'paramCollection.Add(param)

'            param = New OracleParameter
'            param.ParameterName = "rsDivision"
'            param.oracledbtype = oracledbtype.Cursor
'            param.Direction = Data.ParameterDirection.Output
'            paramCollection.Add(param)

'            param = New OracleParameter
'            param.ParameterName = "rsFacility"
'            param.oracledbtype = oracledbtype.Cursor
'            param.Direction = Data.ParameterDirection.Output
'            paramCollection.Add(param)

'            param = New OracleParameter
'            param.ParameterName = "rsBusinessUnit"
'            param.oracledbtype = oracledbtype.Cursor
'            param.Direction = Data.ParameterDirection.Output
'            paramCollection.Add(param)

'            param = New OracleParameter
'            param.ParameterName = "rsArea"
'            param.oracledbtype = oracledbtype.Cursor
'            param.Direction = Data.ParameterDirection.Output
'            paramCollection.Add(param)

'            param = New OracleParameter
'            param.ParameterName = "rsLine"
'            param.oracledbtype = oracledbtype.Cursor
'            param.Direction = Data.ParameterDirection.Output
'            paramCollection.Add(param)

'            param = New OracleParameter
'            param.ParameterName = "rsLineBreak"
'            param.oracledbtype = oracledbtype.Cursor
'            param.Direction = Data.ParameterDirection.Output
'            paramCollection.Add(param)

'            param = New OracleParameter
'            param.ParameterName = "rsPerson"
'            param.oracledbtype = oracledbtype.Cursor
'            param.Direction = Data.ParameterDirection.Output
'            paramCollection.Add(param)

'            param = New OracleParameter
'            param.ParameterName = "rsContractor"
'            param.oracledbtype = oracledbtype.Cursor
'            param.Direction = Data.ParameterDirection.Output
'            paramCollection.Add(param)

'            Dim key As String = "OutageViewUpdate_" & facility & "_"
'            'Dim key As String = "OutageViewUpdate_" & facility & "_" & Division & "_" & inActiveFlag
'            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.Dropdownddl", key, 3)

'            If ds IsNot Nothing Then
'                If ds.Tables.Count >= 8 Then
'                    'Division                    
'                    mDivision.DataSource = ds.Tables(0).CreateDataReader
'                    mDivision.DataTextField = "Division"
'                    mDivision.DataValueField = "Division"

'                    'Facility                    
'                    mFacility.DataSource = ds.Tables(1).CreateDataReader
'                    mFacility.DataTextField = "SiteName"
'                    mFacility.DataValueField = "SiteId"
'                    'mFacilityDivision.DataSource = ds.Tables(1).CreateDataReader
'                    'mFacilityDivision.DataTextField = "Division"
'                    'mFacilityDivision.DataValueField = "Division"

'                    'Business Unit                    
'                    mBusinessUnit.DataSource = ds.Tables(2).CreateDataReader
'                    mBusinessUnit.DataTextField = "RISuperArea"
'                    mBusinessUnit.DataValueField = "RISuperArea"

'                    'Area                    
'                    mArea.DataSource = ds.Tables(3).CreateDataReader
'                    mArea.DataTextField = "SubArea"
'                    mArea.DataValueField = "SubArea"

'                    'Line                    
'                    mLine.DataSource = ds.Tables(4).CreateDataReader
'                    mLine.DataTextField = "Area"
'                    mLine.DataValueField = "Area"

'                    'Line Break                    
'                    mLineBreak.DataSource = ds.Tables(5).CreateDataReader
'                    mLineBreak.DataTextField = "LineBreak"
'                    mLineBreak.DataValueField = "LineBreak"

'                    'Person                    
'                    mPerson.DataSource = ds.Tables(6).CreateDataReader
'                    mPerson.DataTextField = "Person"
'                    mPerson.DataValueField = "UserName"

'                    'Contractor                   
'                    mContractor.DataSource = ds.Tables(7).CreateDataReader
'                    mContractor.DataTextField = "Person"
'                    mContractor.DataValueField = "UserName"

'                End If
'            End If

'        Catch ex As Exception
'            Throw
'        Finally
'            If ds IsNot Nothing Then
'                ds = Nothing
'            End If

'        End Try
'    End Sub

'    Public ReadOnly Property Facility() As clsData
'        Get
'            Return mFacility
'        End Get
'    End Property
'    Public ReadOnly Property BusinessUnit() As clsData
'        Get
'            Return mBusinessUnit
'        End Get
'    End Property
'    Public ReadOnly Property Area() As clsData
'        Get
'            Return mArea
'        End Get
'    End Property
'    Public ReadOnly Property Line() As clsData
'        Get
'            Return mLine
'        End Get
'    End Property
'    Public ReadOnly Property LineBreak() As clsData
'        Get
'            Return mLineBreak
'        End Get
'    End Property
'    Public ReadOnly Property Person() As clsData
'        Get
'            Return mPerson
'        End Get
'    End Property
'    Public ReadOnly Property Contractor() As clsData
'        Get
'            Return mContractor
'        End Get
'    End Property
'    Public ReadOnly Property Division() As clsData
'        Get
'            Return mDivision
'        End Get
'    End Property

'    Private mFacility As New clsData
'    Private mDivision As New clsData
'    Private mBusinessUnit As New clsData
'    Private mArea As New clsData
'    Private mLine As New clsData
'    Private mLineBreak As New clsData
'    Private mPerson As New clsData
'    Private mContractor As New clsData


'    Public Sub New(ByVal facility As String, ByVal inActiveFlag As String, Optional ByVal division As String = "")
'        'GetData(facility, inActiveFlag, division)
'    End Sub

'End Class

'<Serializable()> _
'Public Class clsContractor
'    Public ReadOnly Property Contractor() As clsData
'        Get
'            Return mContractor
'        End Get
'    End Property
'    'Public Property Contractor() As clsData
'    '    Get
'    '        Return mContractor
'    '    End Get
'    '    Set(ByVal value As clsData)
'    '        mContractor = value
'    '    End Set
'    'End Property

'    Private Property SearchDT() As Data.DataTable
'        Get
'            Return mSearchDT
'        End Get
'        Set(ByVal value As Data.DataTable)
'            mSearchDT = value
'        End Set
'    End Property
'    Public Property SearchData() As OracleDataReader
'        Get
'            Return mSearchData
'        End Get
'        Set(ByVal value As OracleDataReader)
'            mSearchData = value
'        End Set
'    End Property


'    Private Sub GetData()
'        Dim paramCollection As New OracleParameterCollection
'        Dim param As New OracleParameter
'        Dim ds As System.Data.DataSet = Nothing

'        'Check input paramaters

'        Try
'            param = New OracleParameter
'            param.ParameterName = "rsContractor"
'            param.oracledbtype = oracledbtype.Cursor
'            param.Direction = Data.ParameterDirection.Output
'            paramCollection.Add(param)

'            Dim key As String = "Contractor"
'            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.Contractorddl", key, 0)

'            '        If ds.Tables.Count >= 1 Then
'            '            'Contractor                    
'            '            _ddlContractor.DataSource = ds.Tables(0).CreateDataReader
'            '            _ddlContractor.DataTextField = "Contractor"
'            '            _ddlContractor.DataValueField = "ContractorSeqId"
'            '            _ddlContractor.DataBind()
'            '            _ddlContractor.Items.Insert(0, "")
'            '        End If
'            If ds IsNot Nothing Then
'                If ds.Tables.Count >= 1 Then
'                    'Contractor                   
'                    mContractor.DataSource = ds.Tables(0).CreateDataReader
'                    mContractor.DataTextField = "Contractor"
'                    mContractor.DataValueField = "ContractorSeqid"

'                End If
'            End If

'        Catch ex As Exception
'            Throw
'        Finally
'            If ds IsNot Nothing Then
'                ds = Nothing
'            End If

'        End Try
'    End Sub

'    Private mSearchDT As Data.DataTable
'    Private mSearchData As OracleDataReader
'    Private mFacility As String
'    Private mContractor As New clsData

'    Public Sub New()
'        GetData()
'    End Sub
'End Class