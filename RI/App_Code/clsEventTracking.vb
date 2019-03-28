Imports Microsoft.VisualBasic
Imports Devart.Data.Oracle
'<Serializable()> _
'    Public Class clsEventTracking
'        Private mName As String
'        Private mText As String
'        Private mDataValue As String
'        Private mDS As System.Data.DataTableReader

'        Public Property DataTextField() As String
'            Get
'                Return mText
'            End Get
'            Set(ByVal value As String)
'                mText = value
'            End Set
'        End Property
'        Public Property DataValueField() As String
'            Get
'                Return mDataValue
'            End Get
'            Set(ByVal value As String)
'                mDataValue = value
'            End Set
'        End Property
'        Public Property DataSource() As System.Data.DataTableReader
'            Get
'                Return mDS
'            End Get
'            Set(ByVal value As System.Data.DataTableReader)
'                mDS = value
'            End Set
'        End Property

'End Class

<Serializable()> _
Public Class clsEventUpdate
    Private mSchedDT As String
    Private mType As String
    Private mCause As String
    Private mProcess As String
    Private mComponent As String
    Private mRINumber As String
    Private mUsername As String
    Private mCrew As String
    Private mStartDate As String
    Private mEndDate As String
    Private mComment As String

    Public Property SchedDT() As String
        Get
            Return mSchedDT
        End Get
        Set(ByVal value As String)
            mSchedDT = value
        End Set
    End Property
    Public Property Type() As String
        Get
            Return mType
        End Get
        Set(ByVal value As String)
            mType = value
        End Set
    End Property
    Public Property Cause() As String
        Get
            Return mCause
        End Get
        Set(ByVal value As String)
            mCause = value
        End Set
    End Property
    Public Property Process() As String
        Get
            Return mProcess
        End Get
        Set(ByVal value As String)
            mProcess = value
        End Set
    End Property
    Public Property Component() As String
        Get
            Return mComponent
        End Get
        Set(ByVal value As String)
            mComponent = value
        End Set
    End Property
    Public Property RINumber() As String
        Get
            Return mRINumber
        End Get
        Set(ByVal value As String)
            mRINumber = value
        End Set
    End Property
    Public Property Username() As String
        Get
            Return mUsername
        End Get
        Set(ByVal value As String)
            mUsername = value
        End Set
    End Property
    Public Property Crew() As String
        Get
            Return mCrew
        End Get
        Set(ByVal value As String)
            mCrew = value
        End Set
    End Property

    Public Property startDate() As String
        Get
            Return mStartDate
        End Get
        Set(ByVal value As String)
            mStartDate = value
        End Set
    End Property

    Public Property endDate() As String
        Get
            Return mEndDate
        End Get
        Set(ByVal value As String)
            mEndDate = value
        End Set
    End Property

    Public Property comment() As String
        Get
            Return mComment
        End Get
        Set(ByVal value As String)
            mComment = value
        End Set
    End Property

    Public Function SaveEvent() As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters
        Try
            '       in_SiteID  IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_RINumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RINumber
            paramCollection.Add(param)

            '      in_SchedDT IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_schedunsched"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.SchedDT
            paramCollection.Add(param)

            '      in_Type IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Type"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Type
            paramCollection.Add(param)

            '      in_Cause IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Cause"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Cause
            paramCollection.Add(param)

            '      in_Process IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Process"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Process
            paramCollection.Add(param)

            '      in_Component IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Component"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Component
            paramCollection.Add(param)

            '      in_UserName IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_UserName"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Username
            paramCollection.Add(param)

            '      in_Comment IN nvarchar2,
            param = New OracleParameter
            param.ParameterName = "in_Comment"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.comment
            paramCollection.Add(param)

            '      in_Crew IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Crew"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Crew
            paramCollection.Add(param)

            '      out_status OUT number
            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.EventTracking.EventUpdate")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving RINumber=[" & Me.RINumber & "] ORA " & returnStatus)
            End If
        Catch ex As Exception
            Throw
        End Try
        Return RINumber
    End Function

    Public Function SaveSplitEvent() As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters
        Try
            '       in_SiteID  IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_RINumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RINumber
            paramCollection.Add(param)

            '      in_StartDate IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_startdate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.startDate
            paramCollection.Add(param)

            '      in_EndDate IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_enddate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.endDate
            paramCollection.Add(param)

            '      in_Type IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Type"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Type
            paramCollection.Add(param)

            '      in_Cause IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Cause"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Cause
            paramCollection.Add(param)

            '      in_Process IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Process"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Process
            paramCollection.Add(param)

            '      in_Component IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Component"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Component
            paramCollection.Add(param)


            '      in_Crew IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Crew"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Crew
            paramCollection.Add(param)

            '      in_UserName IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_UserName"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Username
            paramCollection.Add(param)

            ''      in_)Crew IN varchar2,
            'param = New OracleParameter
            'param.ParameterName = "in_Crew"
            'param.OracleDbType = OracleDbType.VarChar
            'param.Direction = Data.ParameterDirection.Input
            'param.Value = Me.Crew
            'paramCollection.Add(param)

            '      out_status OUT number
            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.EventTracking.SaveSplitEvent")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving RINumber=[" & Me.RINumber & "] ORA " & returnStatus)
            End If
        Catch ex As Exception
            Throw
        End Try
        Return RINumber
    End Function

    Public Function RemoveSplitEvent() As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters
        Try
            '       in_SiteID  IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_RINumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RINumber
            paramCollection.Add(param)

            '      in_UserName IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_UserName"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Username
            paramCollection.Add(param)

            '      out_status OUT number
            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.EventTracking.RemoveSplitEvent")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving RINumber=[" & Me.RINumber & "] ORA " & returnStatus)
            End If
        Catch ex As Exception
            Throw
        End Try
        Return RINumber
    End Function

End Class
<Serializable()> _
Public Class clsEventView
    Public Property Facility() As String
        Get
            Return mFacility
        End Get
        Set(ByVal value As String)
            mFacility = value
        End Set
    End Property
    Public Property FacilityDivision() As String
        Get
            Return mFacilityDivision
        End Get
        Set(ByVal value As String)
            mFacilityDivision = value
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
    Public Property LineBreak() As String
        Get
            Return mLineBreak
        End Get
        Set(ByVal value As String)
            mLineBreak = value
        End Set
    End Property

    Public Property Type() As String
        Get
            Return mType
        End Get
        Set(ByVal value As String)
            mType = value
        End Set
    End Property
    Public Property Cause() As String
        Get
            Return mCause
        End Get
        Set(ByVal value As String)
            mCause = value
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

    Public Property Process() As String
        Get
            Return mProcess
        End Get
        Set(ByVal value As String)
            mProcess = value
        End Set
    End Property
    Public Property defaultProcess() As String
        Get
            Return mDefaultProcess
        End Get
        Set(ByVal value As String)
            mDefaultProcess = value
        End Set
    End Property
    Public Property Component() As String
        Get
            Return mComponent
        End Get
        Set(ByVal value As String)
            mComponent = value
        End Set
    End Property
    Public Property Crew() As String
        Get
            Return mCrew
        End Get
        Set(ByVal value As String)
            mCrew = value
        End Set
    End Property
    Public Property Shift() As String
        Get
            Return mShift
        End Get
        Set(ByVal value As String)
            mShift = value
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
    Public Property EventStatus() As String
        Get
            Return mEventStatus
        End Get
        Set(ByVal value As String)
            mEventStatus = value
        End Set
    End Property

    Public Property RTS() As String
        Get
            Return mRTS
        End Get
        Set(ByVal value As String)
            mRTS = value
        End Set
    End Property

    Public Property RINumber() As String
        Get
            Return mRINumber
        End Get
        Set(ByVal value As String)
            mRINumber = value
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
    Public Property SearchData() As OracleDataReader
        Get
            Return mSearchData
        End Get
        Set(ByVal value As OracleDataReader)
            mSearchData = value
        End Set
    End Property
    Public Property SearchDT() As Data.DataTable
        Get
            Return mSearchDT
        End Get
        Set(ByVal value As Data.DataTable)
            mSearchDT = value
        End Set
    End Property
    Public Function Search() As OracleDataReader
        'Perform Search 
        GetEvents(True)
        Return SearchData
    End Function
    Public Function GetDataTable() As Data.DataTable
        GetEvents(True)
        Return SearchDT
    End Function
    Public Function GetEventsDataTable() As Data.DataTable
        GetSplitEvents(True)
        Return SearchDT
    End Function

    Public Function SearchSplitEvents() As OracleDataReader
        'Perform Search 
        GetEvents(True)
        Return SearchData
    End Function
    'Public Sub New()
    '    GetEvents(True)
    'End Sub

    Private Sub GetEvents(Optional ByVal createDataTable As Boolean = False)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = "N"
        Dim iploc As New IP.Bids.Localization.WebLocalization()

        Try

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Facility
            paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "in_Division"
            'param.OracleDbType = OracleDbType.VarChar
            'param.Direction = Data.ParameterDirection.Input
            'param.Value = Division
            'paramCollection.Add(param)

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
            param.ParameterName = "in_LineBreak"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = LineBreak
            paramCollection.Add(param)

            StartDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(StartDate, "EN-US", "G")
            param = New OracleParameter
            param.ParameterName = "in_StartDate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = StartDate
            paramCollection.Add(param)

            EndDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(EndDate, "EN-US", "G")
            param = New OracleParameter
            param.ParameterName = "in_EndDate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = EndDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_EventType"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Type
            paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "in_RTS"
            'param.OracleDbType = OracleDbType.VarChar
            'param.Direction = Data.ParameterDirection.Input
            'param.Value = RTS
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "in_locale"
            'param.OracleDbType = OracleDbType.VarChar
            'param.Direction = Data.ParameterDirection.Input
            'param.Value = iploc.CurrentLocale.ToString()
            'paramCollection.Add(param)
            param = New OracleParameter
            param.ParameterName = "RS"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)


            Dim key As String = "EventsView_" & Facility & "_" & BusinessUnit & "_" & Area & "_" & Line & "_" & Type & "_" & StartDate & "_" & EndDate
            Dim dr As OracleDataReader = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "Reladmin.EventTracking.EventListing")
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

    Private Sub GetSplitEvents(Optional ByVal createDataTable As Boolean = False)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim iploc As New IP.Bids.Localization.WebLocalization()

        Try

            param = New OracleParameter
            param.ParameterName = "in_rinumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RINumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_EventType"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Type
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "RS"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "SplitEventsView_" & RINumber
            Dim dr As OracleDataReader = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "Reladmin.EventTracking.SplitEventListing")
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
    Public Function ExcelSearch() As OracleDataReader
        GetExcelListing()
        Return ExcelData
    End Function
    Private Sub GetExcelListing(Optional ByVal createDataTable As Boolean = False)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = "N"
        Dim iploc As New IP.Bids.Localization.WebLocalization()

        'Check input paramaters
        Try

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Facility
            paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "in_Division"
            'param.OracleDbType = OracleDbType.VarChar
            'param.Direction = Data.ParameterDirection.Input
            'param.Value = Division
            'paramCollection.Add(param)

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
            param.ParameterName = "in_LineBreak"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = LineBreak
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_StartDate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = StartDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_EndDate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = EndDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_EventType"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Type
            paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "in_locale"
            'param.OracleDbType = OracleDbType.VarChar
            'param.Direction = Data.ParameterDirection.Input
            'param.Value = iploc.CurrentLocale.ToString()
            'paramCollection.Add(param)
            param = New OracleParameter
            param.ParameterName = "RS"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)


            Dim key As String = "EventsExcelSearch_" & Facility & "_" & BusinessUnit & "_" & Area & "_" & Line & "_" & Type & "_" & StartDate & "_" & EndDate
            Dim dr As OracleDataReader = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "Reladmin.EventTracking.ExcelEventListing")

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
            'End If
        Catch ex As Exception
            Throw
        End Try

    End Sub


    Private mSearchDT As Data.DataTable
    Private mSearchData As OracleDataReader
    Private mFacility As String
    Private mFacilityDivision As String
    Private mDivision As String
    Private mBusinessUnit As String
    Private mArea As String
    Private mLine As String
    Private mLineBreak As String
    Private mPerson As String
    Private mTrigger As String
    Private mType As String
    Private mCause As String
    Private mPrevention As String
    Private mProcess As String
    Private mDefaultProcess As String
    Private mComponent As String
    Private mAutomatedReminderAreas As String
    Private mCrew As String
    Private mShift As String
    Private mStartDate As String
    Private mEndDate As String
    Private mDateRange As String
    Private mEventStatus As String
    Private mTitle As String
    Private mRecordable As String
    Private mRTS As String
    Private mRINumber As String

    Public Sub New()

    End Sub
End Class
