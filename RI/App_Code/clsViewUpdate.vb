Imports Microsoft.VisualBasic
Imports Devart.Data.Oracle
Imports System.Data

<Serializable()> _
Public Class clsData
    Private mName As String
    Private mText As String
    Private mDataValue As String
    Private mDS As System.Data.DataTableReader

    Public Property DataTextField() As String
        Get
            Return mText
        End Get
        Set(ByVal value As String)
            mText = value
        End Set
    End Property
    Public Property DataValueField() As String
        Get
            Return mDataValue
        End Get
        Set(ByVal value As String)
            mDataValue = value
        End Set
    End Property
    Public Property DataSource() As System.Data.DataTableReader
        Get
            Return mDS
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mDS = value
        End Set
    End Property

End Class

<Serializable()> _
Public Class clsNameValue

    Private mFieldName As String
    Private mFieldValue As String

    Public Sub New(ByVal fieldName As String, ByVal fieldValue As String)
        mFieldName = fieldName
        mFieldValue = fieldValue
    End Sub
    Public Sub New()

    End Sub

    Public ReadOnly Property FieldName() As String
        Get
            Return mFieldName
        End Get
    End Property

    Public ReadOnly Property FieldValue() As String
        Get
            Return mFieldValue
        End Get
    End Property
End Class

<Serializable()> _
Public Class clsViewSearch
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
    Public Property Person() As String
        Get
            Return mPerson
        End Get
        Set(ByVal value As String)
            mPerson = value
        End Set
    End Property
    Public Property Trigger() As String
        Get
            Return mTrigger
        End Get
        Set(ByVal value As String)
            mTrigger = value
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
    Public Property Prevention() As String
        Get
            Return mPrevention
        End Get
        Set(ByVal value As String)
            mPrevention = value
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
    Public Property AutomatedReminderAreas() As String
        Get
            Return mAutomatedReminderAreas
        End Get
        Set(ByVal value As String)
            mAutomatedReminderAreas = value
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
    Public Property PhysicalCauses() As String
        Get
            Return mPhysicalCauses
        End Get
        Set(ByVal value As String)
            mPhysicalCauses = value
        End Set
    End Property
    Public Property LatentCauses() As String
        Get
            Return mLatentCauses
        End Get
        Set(ByVal value As String)
            mLatentCauses = value
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
    Public Property RCFAStatus() As String
        Get
            Return mRCFAStatus
        End Get
        Set(ByVal value As String)
            mRCFAStatus = value
        End Set
    End Property

    Public Property ActionDue() As String
        Get
            Return mActionDue
        End Get
        Set(ByVal value As String)
            mActionDue = value
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
    Public Property RCFALeader() As String
        Get
            Return mRCFALeader
        End Get
        Set(ByVal value As String)
            mRCFALeader = value
        End Set
    End Property

    Public Property FinancialImpact() As String
        Get
            Return mFinancialImpact
        End Get
        Set(ByVal value As String)
            mFinancialImpact = value
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
    Public Property PPR() As String
        Get
            Return mPPR
        End Get
        Set(ByVal value As String)
            mPPR = value
        End Set
    End Property
    Public Property Recordable() As String
        Get
            Return mRecordable
        End Get
        Set(ByVal value As String)
            mRecordable = value
        End Set
    End Property

    Public Property RCFA() As String
        Get
            Return mRCFA
        End Get
        Set(ByVal value As String)
            mRCFA = value
        End Set
    End Property
    Public Property SRR() As String
        Get
            Return mSRR
        End Get
        Set(ByVal value As String)
            mSRR = value
        End Set
    End Property

    Public Property Chronic() As String
        Get
            Return mChronic
        End Get
        Set(ByVal value As String)
            mChronic = value
        End Set
    End Property

    Public Property CertifiedKill() As String
        Get
            Return mCertifiedKill
        End Get
        Set(ByVal value As String)
            mCertifiedKill = value
        End Set
    End Property

    Public Property Quality() As String
        Get
            Return mQuality
        End Get
        Set(ByVal value As String)
            mQuality = value
        End Set
    End Property

    Public Property HumanCauses() As String
        Get
            Return mHumanCauses
        End Get
        Set(ByVal value As String)
            mHumanCauses = value
        End Set
    End Property

    Public Property SchedUnsched() As String
        Get
            Return mSchedUnsched
        End Get
        Set(ByVal value As String)
            mSchedUnsched = value
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
    Public Property SelectStatement() As String
        Get
            Return mSelect
        End Get
        Set(ByVal value As String)
            mSelect = value
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
    Public Property IRISNumber() As String
        Get
            Return mIRISNumber
        End Get
        Set(ByVal value As String)
            mIRISNumber = value
        End Set
    End Property
    Public Property Safety() As String
        Get
            Return mSafety
        End Get
        Set(ByVal value As String)
            mSafety = value
        End Set
    End Property


    Public Property ConstrainedAreas() As String
        Get
            Return mConstrainedAreas
        End Get
        Set(ByVal value As String)
            mConstrainedAreas = value
        End Set
    End Property
    Public Property Verification() As String
        Get
            Return mVerification
        End Get
        Set(ByVal value As String)
            mVerification = value
        End Set
    End Property

    Public Property Criticality() As Integer
        Get
            Return mCriticality
        End Get
        Set(ByVal value As Integer)
            mCriticality = value
        End Set
    End Property

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
        GetIncidentListing()
        Return SearchData
    End Function
    Public Function GetDataTable() As Data.DataTable
        GetIncidentListing(True)
        Return SearchDT
    End Function
    Private Sub GetIncidentListing(Optional ByVal createDataTable As Boolean = False)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = "N"
        Dim iploc As New IP.Bids.Localization.WebLocalization()
        'Check input paramaters

        Try
            'If inActiveFlag = True Then
            '    ActiveFlag = "Y"
            'Else
            '    ActiveFlag = "N"
            'End If

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Facility
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_inactiveflag"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = ActiveFlag
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
            param.ParameterName = "in_Trigger"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Trigger
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_RCFAStatus"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RCFAStatus
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_ActionDue"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = ActionDue
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_RINumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RINumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_TitleSearch"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Title
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_RCFALeader"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RCFALeader
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Crew"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Crew
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Shift"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Shift
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_FinancialImpact"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = FinancialImpact
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_RTS"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RTS
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_PPR"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = PPR
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Recordable"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Recordable
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_RCFA"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RCFA
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Chronic"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Chronic
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_CertifiedKill"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = CertifiedKill
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Quality"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Quality
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Safety"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Safety
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Type"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Type
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Cause"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Cause
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Prevention"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Prevention
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Process"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Process
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Component"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Component
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_PhysicalCauses"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = PhysicalCauses
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_LatentCauses"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = LatentCauses
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_HumanCauses"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = HumanCauses
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_OrderBy"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = OrderBy
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_AndOr"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = AndOr
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Select"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SelectStatement
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_IRISNumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = IRISNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_locale"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = iploc.CurrentLocale.ToString()
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_constrainedareas"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = ConstrainedAreas
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_criticality"
            param.OracleDbType = OracleDbType.Integer
            param.Direction = Data.ParameterDirection.Input
            param.Value = Criticality
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "in_verification"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Verification
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_SRR"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SRR
            'If SRR.Length > 0 Then
            '    If SRR = "Any Constrained Process DT => 16 Hr" Then
            '        param.Value = "DNT"
            '    Else
            '        If SRR = "Financial Impact >= $250,000" Then
            '            param.Value = "FIN"
            '        Else
            '            param.Value = "BTH"
            '        End If
            '    End If
            'Else
            '    param.Value = ""
            'End If
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_schedunsched"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SchedUnsched
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "RS"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)


            'Dim key As String = "RIViewUpdateSearch_" & Facility & "_" & Division & "_" & ActiveFlag
            'If createDataTable = True Then
            '    ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RIView.IncidentListingDDL", "", 0)
            '    Me.SearchDS = ds
            '    Me.SearchData = CType(ds.Tables(0).CreateDataReader, OracleDataReader)
            'Else
            Dim dr As Devart.Data.Oracle.OracleDataReader = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "Reladmin.RIView.IncidentListingDDL")
            'If ds IsNot Nothing Then
            'If ds.Tables.Count = 1 Then

            If dr IsNot Nothing Then
                If createDataTable = True Then
                    Dim dt As New DataTable
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
    Private mComponent As String
    Private mAutomatedReminderAreas As String
    Private mCrew As String
    Private mShift As String
    Private mPhysicalCauses As String
    Private mLatentCauses As String
    Private mStartDate As String
    Private mEndDate As String
    Private mDateRange As String
    Private mRCFAStatus As String
    Private mActionDue As String
    Private mTitle As String
    Private mRCFALeader As String
    Private mFinancialImpact As String
    Private mRecordable As String
    Private mRTS As String
    Private mPPR As String
    Private mRCFA As String
    Private mSRR As String
    Private mChronic As String
    Private mCertifiedKill As String
    Private mQuality As String
    Private mHumanCauses As String
    Private mOrderBy As String
    Private mAndOr As String
    Private mSelect As String
    Private mSafety As String
    Private mRINumber As String
    Private mIRISNumber As String
    Private mConstrainedAreas As String = String.Empty
    Private mVerification As String = String.Empty
    Private mCriticality As Integer
    Private mSchedUnsched As String

End Class

<Serializable()> _
Public Class clsViewUpdate
    Private Sub GetData(ByVal facility As String, ByVal inActiveFlag As String, Optional ByVal division As String = "")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters

        Try
            'If inActiveFlag = True Then
            '    ActiveFlag = "Y"
            'Else
            '    ActiveFlag = "N"
            'End If

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = facility
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_inactiveflag"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = inActiveFlag
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Division"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = division
            paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsDivision"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsFacility"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsBusinessUnit"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsArea"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsLine"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsLineBreak"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsPerson"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsTrigger"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

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

            'param = New OracleParameter
            'param.ParameterName = "rsAutomatedReminderAreas"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsCrew"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsShift"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsPhysicalCauses"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsLatentCauses"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsHumanCauses"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RIViewUpdate_" & facility & "_" & division & "_" & inActiveFlag
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RIView.Dropdownddl", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 8 Then
                    'Division                    
                    'mDivision.DataSource = ds.Tables(0).CreateDataReader
                    'mDivision.DataTextField = "Division"
                    'mDivision.DataValueField = "Division"

                    ''Facility                    
                    'mFacility.DataSource = ds.Tables(1).CreateDataReader
                    'mFacility.DataTextField = "SiteName"
                    'mFacility.DataValueField = "SiteId"
                    'mFacilityDivision.DataSource = ds.Tables(1).CreateDataReader
                    'mFacilityDivision.DataTextField = "Division"
                    'mFacilityDivision.DataValueField = "Division"

                    ''Business Unit                    
                    'mBusinessUnit.DataSource = ds.Tables(2).CreateDataReader
                    'mBusinessUnit.DataTextField = "RISuperArea"
                    'mBusinessUnit.DataValueField = "RISuperArea"

                    ''Area                    
                    'mArea.DataSource = ds.Tables(3).CreateDataReader
                    'mArea.DataTextField = "SubArea"
                    'mArea.DataValueField = "SubArea"

                    ''Line                    
                    'mLine.DataSource = ds.Tables(4).CreateDataReader
                    'mLine.DataTextField = "Area"
                    'mLine.DataValueField = "Area"

                    ''Line Break                    
                    'mLineBreak.DataSource = ds.Tables(5).CreateDataReader
                    'mLineBreak.DataTextField = "LineBreak"
                    'mLineBreak.DataValueField = "LineBreak"

                    ''Person                    
                    'mPerson.DataSource = ds.Tables(6).CreateDataReader
                    'mPerson.DataTextField = "Person"
                    'mPerson.DataValueField = "UserName"

                    ''Trigger                    
                    'mTrigger.DataSource = ds.Tables(7).CreateDataReader
                    'mTrigger.DataTextField = "TriggerDesc"
                    'mTrigger.DataValueField = "TriggerDesc"

                    'Type                    
                    mType.DataSource = ds.Tables(0).CreateDataReader
                    mType.DataTextField = "Cause"
                    mType.DataValueField = "Cause"

                    'Cause                    
                    mCause.DataSource = ds.Tables(1).CreateDataReader
                    mCause.DataTextField = "Reason"
                    mCause.DataValueField = "Reason"

                    'Prevention                    
                    mPrevention.DataSource = ds.Tables(2).CreateDataReader
                    mPrevention.DataTextField = "Prevention"
                    mPrevention.DataValueField = "Prevention"

                    'Process                    
                    mProcess.DataSource = ds.Tables(3).CreateDataReader
                    mProcess.DataTextField = "Process"
                    mProcess.DataValueField = "Process"

                    'Component                    
                    mComponent.DataSource = ds.Tables(4).CreateDataReader
                    mComponent.DataTextField = "Component"
                    mComponent.DataValueField = "Component"

                    'AutomatedReminderAreas                     
                    'mAutomatedReminderAreas.DataSource = ds.Tables(13).CreateDataReader
                    'mAutomatedReminderAreas.DataTextField = "Reminder_Category"
                    'mAutomatedReminderAreas.DataValueField = "Reminder_Category"

                    ''Crew                    
                    'mCrew.DataSource = ds.Tables(14).CreateDataReader
                    'mCrew.DataTextField = "Crew"
                    'mCrew.DataValueField = "Crew"

                    ''Shift                    
                    'mShift.DataSource = ds.Tables(15).CreateDataReader
                    'mShift.DataTextField = "Shift"
                    'mShift.DataValueField = "Shift"

                    'Physical Causes                        
                    mPhysicalCauses.DataSource = ds.Tables(5).CreateDataReader
                    mPhysicalCauses.DataTextField = "Phys_Cause"
                    mPhysicalCauses.DataValueField = "Phys_Cause"

                    'Latent Causes                    
                    mLatentCauses.DataSource = ds.Tables(6).CreateDataReader
                    mLatentCauses.DataTextField = "Latent_Cause"
                    mLatentCauses.DataValueField = "Latent_Cause"

                    'Human Causes                    
                    mHumanCauses.DataSource = ds.Tables(7).CreateDataReader
                    mHumanCauses.DataTextField = "Human_Cause"
                    mHumanCauses.DataValueField = "Human_Cause"
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

    Public ReadOnly Property Facility() As clsData
        Get
            Return mFacility
        End Get
    End Property
    Public ReadOnly Property FacilityDivision() As clsData
        Get
            Return mFacilityDivision
        End Get
    End Property
    Public ReadOnly Property BusinessUnit() As clsData
        Get
            Return mBusinessUnit
        End Get
    End Property
    Public ReadOnly Property Area() As clsData
        Get
            Return mArea
        End Get
    End Property
    Public ReadOnly Property Line() As clsData
        Get
            Return mLine
        End Get
    End Property
    Public ReadOnly Property LineBreak() As clsData
        Get
            Return mLineBreak
        End Get
    End Property
    Public ReadOnly Property Person() As clsData
        Get
            Return mPerson
        End Get
    End Property
    Public ReadOnly Property Trigger() As clsData
        Get
            Return mTrigger
        End Get
    End Property
    Public ReadOnly Property Type() As clsData
        Get
            Return mType
        End Get
    End Property
    Public ReadOnly Property Cause() As clsData
        Get
            Return mCause
        End Get
    End Property
    Public ReadOnly Property Division() As clsData
        Get
            Return mDivision
        End Get
    End Property
    Public ReadOnly Property Prevention() As clsData
        Get
            Return mPrevention
        End Get
    End Property
    Public ReadOnly Property Process() As clsData
        Get
            Return mProcess
        End Get
    End Property
    Public ReadOnly Property Component() As clsData
        Get
            Return mComponent
        End Get
    End Property
    Public ReadOnly Property AutomatedReminderAreas() As clsData
        Get
            Return mAutomatedReminderAreas
        End Get
    End Property
    Public ReadOnly Property Crew() As clsData
        Get
            Return mCrew
        End Get
    End Property
    Public ReadOnly Property Shift() As clsData
        Get
            Return mShift
        End Get
    End Property
    Public ReadOnly Property PhysicalCauses() As clsData
        Get
            Return mPhysicalCauses
        End Get
    End Property
    Public ReadOnly Property LatentCauses() As clsData
        Get
            Return mLatentCauses
        End Get
    End Property
    Public ReadOnly Property HumanCauses() As clsData
        Get
            Return mHumanCauses
        End Get
    End Property

    Private mFacility As New clsData
    Private mFacilityDivision As New clsData
    Private mDivision As New clsData
    Private mBusinessUnit As New clsData
    Private mArea As New clsData
    Private mLine As New clsData
    Private mLineBreak As New clsData
    Private mPerson As New clsData
    Private mTrigger As New clsData
    Private mType As New clsData
    Private mCause As New clsData
    Private mPrevention As New clsData
    Private mProcess As New clsData
    Private mComponent As New clsData
    Private mAutomatedReminderAreas As New clsData
    Private mCrew As New clsData
    Private mShift As New clsData
    Private mPhysicalCauses As New clsData
    Private mLatentCauses As New clsData
    Private mHumanCauses As New clsData




    Public Sub New(ByVal facility As String, ByVal inActiveFlag As String, Optional ByVal division As String = "")
        GetData(facility, inActiveFlag, division)
    End Sub


End Class
Public Class clsExcelSelect

    Private Sub GetExcelSelect(ByVal User As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters

        Try

            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = User
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsAvailColumns"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "rsUsedColumns"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)


            Dim key As String = "ViewUpdateExcelSelect_" & User
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RIView.ExcelSelectDDL", key, 0)
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 2 Then
                    mColumnName.DataSource = ds.Tables(0).CreateDataReader
                    mColumnName.DataTextField = "DISPLAY_COLUMN_NAME"
                    mColumnName.DataValueField = "DISPLAY_COLUMN_NAME"

                    'Facility                    
                    mColumnList.DataSource = ds.Tables(1).CreateDataReader
                    mColumnList.DataTextField = "COLUMN_LIST"
                    mColumnList.DataValueField = "COLUMN_LIST"
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

    Public ReadOnly Property ColumnName() As clsData
        Get
            Return mColumnName
        End Get
    End Property
    Public ReadOnly Property ColumnList() As clsData
        Get
            Return mColumnList
        End Get
    End Property
    Private mColumnName As New clsData
    Private mColumnList As New clsData
    Public Sub New(ByVal User As String)
        GetExcelSelect(User)
    End Sub

End Class