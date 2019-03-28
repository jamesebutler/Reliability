Option Explicit On
Option Strict On

Imports Microsoft.VisualBasic
imports devart.data.oracle
Imports RI.SharedFunctions
<Serializable()> _
Public Class clsEnterOutage
    Public Structure OutageSecurity
        Dim DeleteOutages As Boolean
        Dim SaveOutages As Boolean
    End Structure

    Enum PageMode
        Update = 1
        NewOutage = 2
    End Enum

    Private mOutageSecurity As OutageSecurity
    Public ReadOnly Property IncidentSecurity() As OutageSecurity
        Get
            Return mOutageSecurity
        End Get
    End Property
    Private mCurrentPageMode As PageMode = PageMode.NewOutage
    Public ReadOnly Property CurrentPageMode() As PageMode
        Get
            Return mCurrentPageMode
        End Get
    End Property
    Private Sub GetData(ByVal userName As String, ByVal facility As String, ByVal inActiveFlag As String, Optional ByVal division As String = "", Optional ByVal busArea As String = "", Optional ByVal lineBreak As String = "")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters

        Try
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
            param.Value = inActiveFlag
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Division"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = division
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Username"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsFacility"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsBusUnitArea"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsPerson"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMRLead"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsAuthLevel"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsOutageTemplate"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "Outage.Dropdownddl_" & facility & "_" & division & "_" & inActiveFlag & "_" & userName
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.NewEntryDropdownddl", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 5 Then
                    'Facility                    
                    mFacility.DataSource = ds.Tables(0).CreateDataReader
                    mFacility.DataTextField = "SiteName"
                    mFacility.DataValueField = "SiteId"

                    'rsBusinessUnitArea                     
                    mBusinessUnitArea.DataSource = ds.Tables(1).CreateDataReader
                    mBusinessUnitArea.DataTextField = "BusUnitArea"
                    mBusinessUnitArea.DataValueField = "BusUnitArea"

                    'rsPerson                   
                    mOutageCoordinator.DataSource = ds.Tables(2).CreateDataReader
                    mOutageCoordinator.DataTextField = "person"
                    mOutageCoordinator.DataValueField = "username"

                    'rsMRLead                  
                    mMRLead.DataSource = ds.Tables(3).CreateDataReader
                    mMRLead.DataTextField = "lead"
                    mMRLead.DataValueField = "username"

                    'rsAuthLevel()
                    'We're not 100% sure about security for outage.   So for now,
                    ' we will keep the selection in the package and just always set the authority level to yes
                    mAuthLevel = "YES"
                    mAuthLevelid = 3

                    Dim dr As Data.DataTableReader = ds.Tables(4).CreateDataReader
                    If dr IsNot Nothing Then
                        dr.Read()
                        If dr.HasRows Then
                            mOutageSecurity.DeleteOutages = True
                        Else
                            mOutageSecurity.DeleteOutages = False
                        End If

                    End If

                    'rsOutageTemplate                  
                    mOutageTemplate.DataSource = ds.Tables(5).CreateDataReader
                    mOutageTemplate.DataTextField = "externalref"
                    mOutageTemplate.DataValueField = "taskheaderseqid"

                End If
            End If

        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
            paramCollection = Nothing
        End Try
    End Sub

    Public ReadOnly Property BusinessUnitArea() As clsData
        Get
            Return mBusinessUnitArea
        End Get
    End Property
    Public ReadOnly Property Line() As clsData
        Get
            Return mLine
        End Get
    End Property
    Public ReadOnly Property Facility() As clsData
        Get
            Return mFacility
        End Get
    End Property
    Public ReadOnly Property AuthLevel() As String
        Get
            Return mAuthLevel
        End Get
    End Property
    Public ReadOnly Property AuthLevelID() As Integer
        Get
            Return mAuthLevelid
        End Get
    End Property
    Public ReadOnly Property OutageCoordinator() As clsData
        Get
            Return mOutageCoordinator
        End Get
    End Property
    Public ReadOnly Property MRLead() As clsData
        Get
            Return mMRLead
        End Get
    End Property
    Public ReadOnly Property OutageTemplate() As clsData
        Get
            Return mOutageTemplate
        End Get
    End Property
    Public ReadOnly Property TemplateTasks() As clsData
        Get
            Return mTemplateTasks
        End Get
    End Property
    Private mAuthLevelid As Integer
    Private mAuthLevel As String
    Private mFacility As New clsData
    Private mBusinessUnitArea As New clsData
    Private mLine As New clsData
    Private mOutageTemplate As New clsData
    Private mTemplateTasks As New clsData
    Private mOutageCoordinator As New clsData
    Private mMRLead As New clsData

    Public Sub New(ByVal userName As String, ByVal facility As String, ByVal inActiveFlag As String, Optional ByVal division As String = "", Optional ByVal busArea As String = "", Optional ByVal lineBreak As String = "", Optional ByVal outageNumber As String = "")

        GetData(userName, facility, inActiveFlag, division, busArea, lineBreak)
        If outageNumber.Length = 0 Then
            Me.mCurrentPageMode = PageMode.NewOutage
        Else
            Me.mCurrentPageMode = PageMode.Update
        End If
    End Sub

End Class

<Serializable()> _
Public Class clsCurrentOutage
    Public Property SiteID() As String
        Get
            Return mSiteID
        End Get
        Set(ByVal value As String)
            mSiteID = value.Trim
        End Set
    End Property
    Private mSiteID As String
    Public Property SiteName() As String
        Get
            Return mSiteName
        End Get
        Set(ByVal value As String)
            mSiteName = value.Trim
        End Set
    End Property
    Private mSiteName As String
    Public Property StartDate() As String
        Get
            Return mStartDate
        End Get
        Set(ByVal value As String)
            mStartDate = value.Trim
        End Set
    End Property
    Private mStartDate As String
    Public Property EndDate() As String
        Get
            Return mEndDate
        End Get
        Set(ByVal value As String)
            mEndDate = value.Trim
        End Set
    End Property
    Private mEndDate As String
    Public Property ActualStartDate() As String
        Get
            Return mActualStartDate
        End Get
        Set(ByVal value As String)
            mActualStartDate = value.Trim
        End Set
    End Property
    Private mActualStartDate As String
    Public Property ActualEndDate() As String
        Get
            Return mActualEndDate
        End Get
        Set(ByVal value As String)
            mActualEndDate = value.Trim
        End Set
    End Property
    Private mActualEndDate As String
    Public Property ProposedStartDate() As String
        Get
            Return mProposedStartDate
        End Get
        Set(ByVal value As String)
            mProposedStartDate = value.Trim
        End Set
    End Property
    Private mProposedStartDate As String
    Public Property ProposedEndDate() As String
        Get
            Return mProposedEndDate
        End Get
        Set(ByVal value As String)
            mProposedEndDate = value.Trim
        End Set
    End Property
    Private mProposedEndDate As String
    Public Property BusinessUnit() As String
        Get
            Return mBusinessUnit
        End Get
        Set(ByVal value As String)
            mBusinessUnit = value.Trim
        End Set
    End Property
    Private mBusinessUnit As String
    Public Property Area() As String
        Get
            Return mArea
        End Get
        Set(ByVal value As String)
            mArea = value.Trim
        End Set
    End Property
    Private mArea As String
    Public Property Line() As String
        Get
            Return mLine
        End Get
        Set(ByVal value As String)
            mLine = value.Trim
        End Set
    End Property
    Private mLine As String
    Public Property Title() As String
        Get
            Return mTitle
        End Get
        Set(ByVal value As String)
            mTitle = value.Trim
        End Set
    End Property
    Private mTitle As String
    Private mOutageNumber As String
    Public Property OutageNumber() As String
        Get
            Return mOutageNumber
        End Get
        Set(ByVal value As String)
            mOutageNumber = value.Trim
        End Set
    End Property
    Private mChangeDateFlag As String
    Public Property ChangeDateFlag() As String
        Get
            Return mChangeDateFlag
        End Get
        Set(ByVal value As String)
            mChangeDateFlag = value.Trim
        End Set
    End Property
    Private mChangeDateMsg As String
    Public Property ChangeDateMsg() As String
        Get
            Return mChangeDateMsg
        End Get
        Set(ByVal value As String)
            mChangeDateMsg = value.Trim
        End Set
    End Property
    Private mTotalCost As String
    Public Property Description() As String
        Get
            Return mDescription
        End Get
        Set(ByVal value As String)
            mDescription = value.Trim
        End Set
    End Property
    Private mDescription As String
    Public Property ActualDowntime() As String
        Get
            Return mActualDowntime
        End Get
        Set(ByVal value As String)
            mActualDowntime = value.Trim
        End Set
    End Property
    Private mActualDowntime As String
    Public Property Downtime() As String
        Get
            Return mDowntime
        End Get
        Set(ByVal value As String)
            mDowntime = value.Trim
        End Set
    End Property
    Private mDowntime As String
    Public Property UserName() As String
        Get
            Return mUserName
        End Get
        Set(ByVal value As String)
            mUserName = value.Trim
        End Set
    End Property
    Private mUserName As String

    Public Property OutageCoordinator() As String
        Get
            Return mOutageCoordinator
        End Get
        Set(ByVal value As String)
            mOutageCoordinator = value.Trim
        End Set
    End Property
    Private mOutageCoordinator As String
    Public Property SDCategory() As String
        Get
            Return mSDCategory
        End Get
        Set(ByVal value As String)
            mSDCategory = value.Trim
        End Set
    End Property
    Private mSDCategory As String

    Public Property CoordName() As String
        Get
            Return mCoordName
        End Get
        Set(ByVal value As String)
            mCoordName = value
        End Set
    End Property
    Private mCoordName As String
    Private mInsertUsername As String
    Public Property InsertUsername() As String
        Get
            Return mInsertUsername
        End Get
        Set(ByVal value As String)
            mInsertUsername = value
        End Set
    End Property

    Private mInsertDate As String
    Public Property InsertDate() As String
        Get
            Return mInsertDate
        End Get
        Set(ByVal value As String)
            mInsertDate = value
        End Set
    End Property

    Private mLastUpdatedBy As String
    Public Property LastUpdatedBy() As String
        Get
            Return mLastUpdatedBy
        End Get
        Set(ByVal value As String)
            mLastUpdatedBy = value
        End Set
    End Property

    Private mLastUpdatedDate As String
    Public Property LastUpdatedDate() As String
        Get
            Return mLastUpdatedDate
        End Get
        Set(ByVal value As String)
            mLastUpdatedDate = value
        End Set
    End Property
    Private mContractorStartDate As String
    Public Property ContractorStartDate() As String
        Get
            Return mContractorStartDate
        End Get
        Set(ByVal value As String)
            mContractorStartDate = value
        End Set
    End Property
    Private mContractorEndDate As String
    Public Property ContractorEndDate() As String
        Get
            Return mContractorEndDate
        End Get
        Set(ByVal value As String)
            mContractorEndDate = value
        End Set
    End Property
    Private mContractorSeqID As String
    Public Property ContractorSeqID() As String
        Get
            Return mContractorSeqID
        End Get
        Set(ByVal value As String)
            mContractorSeqID = value
        End Set
    End Property
    Private mContractorTypeSeqID As String
    Public Property ContractorTypeSeqID() As String
        Get
            Return mContractorTypeSeqID
        End Get
        Set(ByVal value As String)
            mContractorTypeSeqID = value
        End Set
    End Property
    Private mContractorComments As String
    Public Property ContractorComments() As String
        Get
            Return mContractorComments
        End Get
        Set(ByVal value As String)
            mContractorComments = value
        End Set
    End Property
    Private mContractorDT As Data.DataTableReader

    Public Property ContractorDT() As Data.DataTableReader
        Get
            Return mContractorDT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mContractorDT = value
        End Set
    End Property
    Private mContractorHeadCount As Integer
    Public Property ContractorHeadCount() As Integer
        Get
            Return mContractorHeadCount
        End Get
        Set(ByVal value As Integer)
            mContractorHeadCount = value
        End Set
    End Property
    Private mContractorConflictStatus As String
    Public Property ContractorConflictStatus() As String
        Get
            Return mContractorConflictStatus
        End Get
        Set(ByVal value As String)
            mContractorConflictStatus = value
        End Set
    End Property
    Private mResourceDT As Data.DataTableReader

    Public Property ResourceDT() As Data.DataTableReader
        Get
            Return mResourceDT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mResourceDT = value
        End Set
    End Property
    Private mMTTTaskHeaderID As String
    Public Property MTTTaskHeaderID() As String
        Get
            Return mMTTTaskHeaderID
        End Get
        Set(ByVal value As String)
            mMTTTaskHeaderID = value
        End Set
    End Property
    Private mMTTTemplateID As String
    Public Property MTTTemplateID() As String
        Get
            Return mMTTTemplateID
        End Get
        Set(ByVal value As String)
            mMTTTemplateID = value
        End Set
    End Property
    Private mOutageTemplate As String
    Public Property OutageTemplate() As String
        Get
            Return mOutageTemplate
        End Get
        Set(ByVal value As String)
            mOutageTemplate = value
        End Set
    End Property
    Private mAnnualFlag As String
    Public Property AnnualFlag() As String
        Get
            Return mAnnualFlag
        End Get
        Set(ByVal value As String)
            mAnnualFlag = value
        End Set
    End Property
    Private mAssessmentDate As String
    Public Property AssessmentDate() As String
        Get
            Return mAssessmentDate
        End Get
        Set(ByVal value As String)
            mAssessmentDate = value.Trim
        End Set
    End Property
    Private mMRLead As String
    Public Property MRLead() As String
        Get
            Return mMRLead
        End Get
        Set(ByVal value As String)
            mMRLead = value
        End Set
    End Property
    Private mTaskCount As String
    Public Property TaskCount() As String
        Get
            Return mTaskCount
        End Get
        Set(ByVal value As String)
            mTaskCount = value
        End Set
    End Property
    Public Property TemplateTasks() As String
        Get
            Return mTemplateTasks
        End Get
        Set(ByVal value As String)
            mTemplateTasks = value.Trim
        End Set
    End Property
    Private mTemplateTasks As String
    Public Property TemplateMsg() As String
        Get
            Return mTemplateMsg
        End Get
        Set(ByVal value As String)
            mTemplateMsg = value.Trim
        End Set
    End Property
    Private mTemplateMsg As String
    Public Property TaskEmails() As String
        Get
            Return mTaskEmails
        End Get
        Set(ByVal value As String)
            mTaskEmails = value.Trim
        End Set
    End Property
    Private mTaskEmails As String
    Public Property CoordEmail() As String
        Get
            Return mCoordEmail
        End Get
        Set(ByVal value As String)
            mCoordEmail = value.Trim
        End Set
    End Property
    Private mCoordEmail As String
    Private mResourceStartDate As String
    Public Property ResourceStartDate() As String
        Get
            Return mResourceStartDate
        End Get
        Set(ByVal value As String)
            mResourceStartDate = value
        End Set
    End Property
    Private mResourceEndDate As String
    Public Property ResourceEndDate() As String
        Get
            Return mResourceEndDate
        End Get
        Set(ByVal value As String)
            mResourceEndDate = value
        End Set
    End Property
    Private mResourceSeqID As String
    Public Property ResourceSeqID() As String
        Get
            Return mResourceSeqID
        End Get
        Set(ByVal value As String)
            mResourceSeqID = value
        End Set
    End Property
    Private mResourceComments As String
    Public Property ResourceComments() As String
        Get
            Return mResourceComments
        End Get
        Set(ByVal value As String)
            mResourceComments = value
        End Set
    End Property
    Private mComments As String
    Public Property Comments() As String
        Get
            Return mComments
        End Get
        Set(ByVal value As String)
            mComments = value
        End Set
    End Property
    Private mScopeCount As String
    Public Property ScopeCount() As String
        Get
            Return mScopeCount
        End Get
        Set(ByVal value As String)
            mScopeCount = value
        End Set
    End Property
    Private mCost As String
    Public Property Cost() As String
        Get
            Return mCost
        End Get
        Set(ByVal value As String)
            mCost = value
        End Set
    End Property
    Private mActualCost As String
    Public Property ActualCost() As String
        Get
            Return mActualCost
        End Get
        Set(ByVal value As String)
            mActualCost = value
        End Set
    End Property
    Private mPlannedCapitalCost As String
    Public Property PlannedCapitalCost() As String
        Get
            Return mPlannedCapitalCost
        End Get
        Set(ByVal value As String)
            mPlannedCapitalCost = value
        End Set
    End Property
    Private mActualCapitalCost As String
    Public Property ActualCapitalCost() As String
        Get
            Return mActualCapitalCost
        End Get
        Set(ByVal value As String)
            mActualCapitalCost = value
        End Set
    End Property
    Private mFEPAScore As String
    Public Property FEPAScore() As String
        Get
            Return mFEPAScore
        End Get
        Set(ByVal value As String)
            mFEPAScore = value
        End Set
    End Property
    Private mTGMCMFScore As String
    Public Property TGMCMFScore() As String
        Get
            Return mTGMCMFScore
        End Get
        Set(ByVal value As String)
            mTGMCMFScore = value
        End Set
    End Property
    Private mOverallScore As String
    Public Property OverallScore() As String
        Get
            Return mOverallScore
        End Get
        Set(ByVal value As String)
            mOverallScore = value
        End Set
    End Property
    Private mCommercialIssuesCnt As String
    Public Property CommercialIssuesCnt() As String
        Get
            Return mCommercialIssuesCnt
        End Get
        Set(ByVal value As String)
            mCommercialIssuesCnt = value
        End Set
    End Property
    Private mTGComments As String
    Public Property TGComments() As String
        Get
            Return mTGComments
        End Get
        Set(ByVal value As String)
            mTGComments = value
        End Set
    End Property
    Private mRolesDT As clsData 'Data.DataTableReader

    Public Property RolesDT() As clsData
        Get
            Return mRolesDT
        End Get
        Set(ByVal value As clsData)
            mRolesDT = value
        End Set
    End Property
    Private mMOCNumber As String
    Public Property MocNumber() As String
        Get
            Return mMOCNumber
        End Get
        Set(ByVal value As String)
            mMOCNumber = value
        End Set
    End Property
    Public Property ApproverEmails() As String
        Get
            Return mApproverEmails
        End Get
        Set(ByVal value As String)
            mApproverEmails = value.Trim
        End Set
    End Property
    Private mApproverEmails As String
    Public Shared Sub DeleteCurrentOutage(ByVal outageNumber As Integer, ByVal userName As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_OutageNUMBER"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = outageNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_USERNAME"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.Outage.DELETEOutage")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Deleting " & outageNumber)
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub
    Public Shared Sub DeleteOutageContractor(ByVal OutageNumber As Integer, ByVal userName As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_OutageNUMBER"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = OutageNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_ContractorSeqID"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.Outage.DeleteOutageContractor")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Deleting Outage Contractor:" & userName & ", Outage Number:" & OutageNumber)
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Public Function InsertOutageContractor(ByVal OutageNumber As String, ByVal contractorseqid As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim connection As String = String.Empty
        Dim provider As String = String.Empty

        Dim userNameArray As String() = contractorseqid.Split(CChar(","))


        'Check input paramaters
        Try

            For i As Integer = 0 To userNameArray.Length - 1
                paramCollection = New OracleParameterCollection

                param = New OracleParameter
                param.ParameterName = "IN_OUTAGENUMBER"
                param.oracledbtype = oracledbtype.Integer
                param.Direction = Data.ParameterDirection.Input
                param.Value = OutageNumber
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "IN_contractorseqid"
                param.oracledbtype = oracledbtype.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = userNameArray(i)
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "out_status"
                param.oracledbtype = oracledbtype.Integer
                param.Direction = Data.ParameterDirection.Output
                paramCollection.Add(param)
                Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.outage.InsertOutageContractor")
                If CDbl(returnStatus) <> 0 Then
                    Throw New Data.DataException("Error Inserting Outage Contractor " & OutageNumber)
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
        Return ""
    End Function
    Public Function SaveOutageContractor(ByVal outagenumber As String, ByVal CONTRACTORSEQID As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_OUTAGENUMBER"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = outagenumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_CONTRACTORSEQID"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = CONTRACTORSEQID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_StartDate"
            param.OracleDbType = OracleDbType.Date
            param.Direction = Data.ParameterDirection.Input
            param.Value = CDate(Me.ContractorStartDate)
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_EndDate"
            param.oracledbtype = oracledbtype.Date
            param.Direction = Data.ParameterDirection.Input
            param.Value = CDate(Me.ContractorEndDate)
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_COMMENTS"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.ContractorComments
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_HEADCOUNT"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.ContractorHeadCount
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_username"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.UserName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.outage.SaveOutageContractor")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Outage Contractor " & outagenumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveContractorConflict(ByVal outagenumber As String, ByVal CONTRACTORSEQID As String, ByVal conflictoutagenumber As String, ByVal Status As String, ByVal Comments As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_OUTAGENUMBER"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = outagenumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_CONTRACTORSEQID"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = CONTRACTORSEQID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_CONFLICTOUTAGENUMBER"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = conflictoutagenumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_ConflictStatus"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Status
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_COMMENTS"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Comments
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_username"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.UserName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.outage.SaveOutageContractorConflict")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Outage Contractor Conflict " & outagenumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveOutage() As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        'Dim ds As System.Data.DataSet  = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters
        Try

            param = New OracleParameter
            param.ParameterName = "in_SiteID"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.SiteID
            paramCollection.Add(param)

            Dim startDate As String = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Me.StartDate, "EN-US", "G")
            param = New OracleParameter
            param.ParameterName = "in_StartDate"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            ' param.Value = startDate
            param.Value = Me.StartDate
            'param.Value = FormatDateTime(CDate(Me.StartDate), DateFormat.ShortDate) & " " & FormatDateTime(CDate(Me.StartDate), DateFormat.ShortTime)
            paramCollection.Add(param)

            Dim endDate As String = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Me.EndDate, "EN-US", "G")
            param = New OracleParameter
            param.ParameterName = "in_EndDate"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            'param.Value = endDate
            param.Value = Me.EndDate
            'param.Value = FormatDateTime(CDate(Me.EndDate), DateFormat.ShortDate) & " " & FormatDateTime(CDate(Me.EndDate), DateFormat.ShortTime)
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_BusinessUnit"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.BusinessUnit
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Area"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Area
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Line"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Line
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Title"
            param.oracledbtype = oracledbtype.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Title
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Description"
            param.oracledbtype = oracledbtype.NClob
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Description
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_SDCategory"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.SDCategory
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_OutageCoord"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.OutageCoordinator
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_UserName"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.UserName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_OutageNumber"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Input
            If Me.OutageNumber.Length = 0 Then
                param.Value = System.DBNull.Value
            Else
                param.Value = OutageNumber
            End If
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Downtime"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Replace(Me.Downtime, ",", "")
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_MTTTemplate"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.OutageTemplate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_AnnualFlag"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.AnnualFlag
            paramCollection.Add(param)
            ''param.Value = RI.SharedFunctions.DataClean(Me.RINumber, System.DBNull.Value)
            '      out_OutageNumber OUT number,
            '      in_AssessmentDate IN varchar2,
            Dim assessDate As String = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Me.AssessmentDate, "EN-US", "G")
            'assessDate = Me.AssessmentDate
            param = New OracleParameter
            param.ParameterName = "in_AssessmentDate"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = assessDate
            paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "in_TemplateTasks"
            'param.oracledbtype = oracledbtype.VarChar
            'param.Direction = Data.ParameterDirection.Input
            'param.Value = TemplateTasks
            'paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_MRLead"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = MRLead
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Comments"
            param.oracledbtype = oracledbtype.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Comments
            paramCollection.Add(param)

            Dim ActualStartDate As String = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Me.ActualStartDate, "EN-US", "G")
            param = New OracleParameter
            param.ParameterName = "in_ActualStartDate"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = ActualStartDate
            paramCollection.Add(param)

            Dim ActualEndDate As String = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Me.ActualEndDate, "EN-US", "G")
            param = New OracleParameter
            param.ParameterName = "in_ActualEndDate"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = ActualEndDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_ActualDowntime"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Replace(Me.ActualDowntime, ",", "")
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Cost"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Cost
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_ActualCost"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = ActualCost
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_FEPAScore"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = FEPAScore
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_TGMCMFScore"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = TGMCMFScore
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_OverallScore"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = OverallScore
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_CommIssuesCnt"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = CommercialIssuesCnt
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_TGComment"
            param.oracledbtype = oracledbtype.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = TGComments
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_PlannedCapitalCost"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = PlannedCapitalCost
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_ActualCapitalCost"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = ActualCapitalCost
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_OutageNumber"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_ChangeDateFlag"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Output
            param.Size = 40
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_ChangeDateMsg"
            param.oracledbtype = oracledbtype.NVarChar
            param.Direction = Data.ParameterDirection.Output
            param.Size = 4000
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "out_status"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.Outage.SaveOutage")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving " & Me.OutageNumber)
            Else
                Me.OutageNumber = CStr(paramCollection.Item("out_OutageNumber").Value)
                Me.ChangeDateFlag = CStr(paramCollection.Item("out_ChangeDateFlag").Value)
                Me.ChangeDateMsg = CStr(paramCollection.Item("out_ChangeDateMsg").Value)
            End If
        Catch ex As Exception
            Throw
        End Try
        Return OutageNumber
    End Function

    Public Function SaveOutageComment() As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        'Dim ds As System.Data.DataSet  = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters
        Try

            param = New OracleParameter
            param.ParameterName = "in_OutageNumber"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            If Me.OutageNumber.Length = 0 Then
                param.Value = System.DBNull.Value
            Else
                param.Value = OutageNumber
            End If
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Comments"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Comments
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.Outage.SaveOutageComment")
            Return returnStatus
            If returnStatus <> "0" Then
                Throw New Data.DataException("Error Saving " & Me.OutageNumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveTemplateTasks(ByVal outagenumber As String, ByVal templatetask As String, ByVal roletasks As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_OutageNumber"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = outagenumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_templatetaskseq"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = templatetask
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_templatetaskroleseq"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = roletasks
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.outage.SaveOutageTemplateTasks")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Outage Template Tasks " & outagenumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ReplicateOutageTasks(ByVal templateseqid As Integer, ByVal outagenumber As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_OutageNumber"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = outagenumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_TemplateSeqId"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = templateseqid
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.MTTOutageReplication.ReplicateOutageTasks")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Outage Template Tasks " & outagenumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveOutageEntireTemplate(ByVal outagenumber As String, ByVal templateseq As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_OutageNumber"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = outagenumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_templateseq"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = templateseq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.outage.SaveOutageEntireTemplate")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Entire Outage Template " & outagenumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CreateNewOutageMTTTemplate(ByVal username As String, ByVal outagenumber As String, ByVal desc As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_Username"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_OutageNumber"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = outagenumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_templatedesc"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = desc
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.MTTOutageReplication.createnewoutagetemplate")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving New Outage Template " & outagenumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Sub GetOutage()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        Try
            If Not IsNumeric(OutageNumber) Then Throw New Data.DataException("Invalid Outage Number was specified - " & OutageNumber)

            param = New OracleParameter
            param.ParameterName = "in_outagenumber"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = OutageNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rs"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsOutageContractors"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsOutageResources"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "NewOutage.GetOutageRecord_" & OutageNumber
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.GETOutage", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 2 Then
                    Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader
                    Dim sb As New StringBuilder
                    If dr IsNot Nothing Then
                        If dr.HasRows Then
                            dr.Read()
                            With Me

                                .OutageCoordinator = DataClean(dr.Item("outagecoordusername"))
                                .MRLead = DataClean(dr.Item("mrleadusername"))
                                .CoordEmail = DataClean(dr.Item("email"))
                                .TaskEmails = DataClean(dr.Item("taskemails"))
                                .ApproverEmails = DataClean(dr.Item("approveremail"))

                                'Location
                                .SiteID = DataClean(dr.Item("Siteid"))
                                .SiteName = DataClean(dr.Item("SiteName"))

                                '***********Don't like using the itemarray property - need to change
                                If ds.Tables(0).Rows.Count > 1 Then
                                    Dim i As Integer
                                    For i = 0 To ds.Tables(0).Rows.Count - 1
                                        If i = 0 Then
                                            .BusinessUnit = DataClean(ds.Tables(0).Rows.Item(i).ItemArray(13)) & "-" & DataClean(ds.Tables(0).Rows.Item(i).ItemArray(14)) & "-" & DataClean(ds.Tables(0).Rows.Item(i).ItemArray(15))
                                        Else
                                            .BusinessUnit = .BusinessUnit & "," & DataClean(ds.Tables(0).Rows.Item(i).ItemArray(13)) & "-" & DataClean(ds.Tables(0).Rows.Item(i).ItemArray(14)) & "-" & DataClean(ds.Tables(0).Rows.Item(i).ItemArray(15))
                                        End If    '.BusinessUnit = .BusinessUnit & "," & DataClean(dr.Item("risuperarea")) & "-" & DataClean(dr.Item("Subarea")) & "-" & DataClean(dr.Item("Area"))
                                    Next
                                Else
                                    .BusinessUnit = DataClean(dr.Item("risuperarea")) & "-" & DataClean(dr.Item("Subarea")) & "-" & DataClean(dr.Item("Area"))
                                End If
                                .Area = DataClean(dr.Item("SubArea"))
                                .Line = DataClean(dr.Item("Area"))
                                'If .Line.IndexOf("-") < 0 Then .Line = .Line & " - None"
                                'Outage
                                .StartDate = DataClean(dr.Item("StartDate"))
                                .EndDate = DataClean(dr.Item("EndDate"))
                                If .EndDate.Length = 0 Then .EndDate = .StartDate
                                .StartDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(.StartDate, "EN-US")
                                .EndDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(.EndDate, "EN-US")

                                .ActualStartDate = DataClean(dr.Item("ActualStartDate"))
                                .ActualEndDate = DataClean(dr.Item("ActualEndDate"))
                                If .ActualEndDate.Length = 0 Then .ActualEndDate = .ActualStartDate
                                .ActualStartDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(.ActualStartDate, "EN-US")
                                .ActualEndDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(.ActualEndDate, "EN-US")

                                .ProposedStartDate = DataClean(dr.Item("ProposedStartDate"))
                                .ProposedEndDate = DataClean(dr.Item("ProposedEndDate"))
                                .ProposedStartDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(.ProposedStartDate, "EN-US")
                                .ProposedEndDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(.ProposedEndDate, "EN-US")

                                'Don't forget about the hours and minutes
                                .Downtime = DataClean(dr.Item("Downtime"))
                                .ActualDowntime = DataClean(dr.Item("ActualDowntime"))
                                .Title = DataClean(dr.Item("OutageTitle"))
                                .Description = DataClean(dr.Item("OutageDescription"))
                                .Comments = DataClean(dr.Item("Comments"))
                                .SDCategory = DataClean(dr.Item("shutdowncategory"))
                                .TGComments = DataClean(dr.Item("TGComment"))
                                .Cost = DataClean(dr.Item("cost"))
                                .ActualCost = DataClean(dr.Item("actualcost"))
                                .MocNumber = DataClean(dr.Item("mocnumber"))
                                .PlannedCapitalCost = DataClean(dr.Item("plannedcapitalcost"))
                                .ActualCapitalCost = DataClean(dr.Item("actualcapitalcost"))
                                'Creation
                                .UserName = DataClean(dr.Item("insertusername"))
                                .InsertUsername = GetPersonName(DataClean(dr.Item("insertusername")))
                                .InsertDate = CleanDate(dr.Item("insertdate"), DateFormat.ShortDate)
                                .LastUpdatedBy = GetPersonName(DataClean(dr.Item("updateusername")))
                                .LastUpdatedDate = CleanDate(dr.Item("updatedate"), DateFormat.ShortDate)

                                'MTTTask Header Link
                                .MTTTaskHeaderID = DataClean(dr.Item("mtttaskheaderseqid"))
                                .TaskCount = DataClean(dr.Item("taskcount"))

                                'MTT Outage Template
                                .MTTTemplateID = DataClean(dr.Item("mtttemplateseqid"))

                                'Annual Outage Flag
                                .AnnualFlag = DataClean(dr.Item("AnnualOutageFlag"))

                                'Prep Assessment Date
                                .AssessmentDate = CleanDate(dr.Item("prepassessmentdate"), DateFormat.ShortDate)

                                'Template Message
                                .TemplateMsg = DataClean(dr.Item("TaskMsg"))

                                'Scope Count
                                .ScopeCount = DataClean(dr.Item("scopecount"))

                                'FEPA Score
                                .FEPAScore = DataClean(dr.Item("fepascore"))

                                'TGMCMF Score
                                .TGMCMFScore = DataClean(dr.Item("tgmcmfscore"))

                                'Overall Score
                                .OverallScore = DataClean(dr.Item("overallscore"))

                                'Commercial Issues Pending
                                .CommercialIssuesCnt = DataClean(dr.Item("commercialissuescnt"))

                            End With
                            'Me.GetContractors()
                            'Me.GetRoles()
                        End If

                        Me.GetContractors()
                        Dim dr1 As Data.DataTableReader = ds.Tables(1).CreateDataReader
                        If dr1 IsNot Nothing Then
                            Me.ContractorDT = dr1
                        End If
                        Dim drResource As Data.DataTableReader = ds.Tables(2).CreateDataReader
                        If drResource IsNot Nothing Then
                            Me.ResourceDT = drResource
                        End If


                    End If

                End If
            End If

        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then ds = Nothing
        End Try
    End Sub
    Private Function GetContractors() As Data.DataTableReader
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As Data.DataTableReader = Nothing
        'Check input paramaters

        Try
            param = New OracleParameter
            param.ParameterName = "IN_OutageNUMBER"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.OutageNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "RSContractors"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "GetContractors" & OutageNumber

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.GetContractors", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count = 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If
            End If

        Catch ex As Exception
            Throw
            dr = Nothing
        Finally
            If ds IsNot Nothing Then
                ds = Nothing
            End If
            GetContractors = dr
        End Try
    End Function

    Public Shared Sub DeleteOutageResource(ByVal OutageNumber As Integer, ByVal ResourceSeqID As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_OutageNUMBER"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = OutageNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_ResourceSeqID"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = ResourceSeqID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.Outage.DeleteOutageResource")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Deleting Outage Resource:" & ResourceSeqID & ", Outage Number:" & OutageNumber)
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Public Function InsertOutageResources(ByVal OutageNumber As String, ByVal resourceseqid As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim connection As String = String.Empty
        Dim provider As String = String.Empty

        Dim userNameArray As String() = resourceseqid.Split(CChar(","))

        'Check input paramaters
        Try

            For i As Integer = 0 To userNameArray.Length - 1
                paramCollection = New OracleParameterCollection

                param = New OracleParameter
                param.ParameterName = "IN_OUTAGENUMBER"
                param.oracledbtype = oracledbtype.Integer
                param.Direction = Data.ParameterDirection.Input
                param.Value = OutageNumber
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "IN_resourceseqid"
                param.oracledbtype = oracledbtype.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = userNameArray(i)
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "out_status"
                param.oracledbtype = oracledbtype.Integer
                param.Direction = Data.ParameterDirection.Output
                paramCollection.Add(param)
                Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.outage.InsertOutageResource")
                If CDbl(returnStatus) <> 0 Then
                    Throw New Data.DataException("Error Inserting Outage Resource " & OutageNumber)
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
        Return ""
    End Function
    Public Function SaveOutageResource(ByVal outagenumber As String, ByVal RESOURCESEQID As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_OUTAGENUMBER"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = outagenumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_RESOURCESEQID"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RESOURCESEQID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_StartDate"
            param.oracledbtype = oracledbtype.Date
            param.Direction = Data.ParameterDirection.Input
            param.Value = CDate(Me.ResourceStartDate)
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_EndDate"
            param.oracledbtype = oracledbtype.Date
            param.Direction = Data.ParameterDirection.Input
            param.Value = CDate(Me.ResourceEndDate)
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_COMMENTS"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.ResourceComments
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_username"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.UserName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.outage.SaveOutageResource")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Outage Resource " & outagenumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetPersonName(ByVal user As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty
        Dim ret As String = "Unknown"
        'Check input paramaters

        Try
            param = New OracleParameter
            param.ParameterName = "in_username"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = user
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsPersonName"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "GetPersonName_" & user
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.GetPersonName", key, 24)

            If ds IsNot Nothing Then
                If ds.Tables.Count = 1 Then
                    Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader
                    If dr IsNot Nothing Then
                        If dr.HasRows Then
                            dr.Read()
                            With Me
                                'Person Name
                                ret = DataClean(dr.Item("FirstName")) & " " & DataClean(dr.Item("LastName"))
                            End With
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                ds = Nothing
            End If
            GetPersonName = ret
        End Try
    End Function
    Private Function GetRoles() As Data.DataTableReader
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As Data.DataTableReader = Nothing
        'Check input paramaters

        Try
            param = New OracleParameter
            param.ParameterName = "rsRoles"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "GetMTTRoles_"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.GetMTTRoles", key, 2)

            If ds IsNot Nothing Then
                If ds.Tables.Count = 1 Then
                    dr = ds.Tables(0).CreateDataReader

                End If
            End If

        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                ds = Nothing
            End If
            GetRoles = dr
        End Try
    End Function
    Public Sub New(ByVal OutageNum As String)
        If OutageNum IsNot Nothing And OutageNum.Length > 0 Then
            OutageNumber = OutageNum
            GetOutage()
        End If
    End Sub
    Public Sub New()
        OutageNumber = String.Empty
    End Sub
End Class

<Serializable()> _
Public Class clsOutageTemplateEmail
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
    Public Function Search(ByVal taskheadernumber As String) As OracleDataReader
        'Perform Search 
        GetEmailRecords(taskheadernumber, True)
        Return SearchData
    End Function
    Public Function GetDataTable(ByVal outagenumber As String) As Data.DataTable
        GetEmailRecords(outagenumber, True)
        Return SearchDT
    End Function
    Public Sub GetEmailRecords(ByVal OutageNumber As String, Optional ByVal createDataTable As Boolean = False)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters

        Try
            param = New OracleParameter
            param.ParameterName = "in_OutageNbr"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input

            param.Value = OutageNumber
            'param.Value = 11862
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsEmailTasks"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)


            'ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.mttOUTAGEreplication.SelectEmailOutageTasks", key, 3)
            Dim dr As OracleDataReader = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "Reladmin.mttOUTAGEreplication.SelectEmailOutageTasks")

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
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
            paramCollection = Nothing
        End Try
    End Sub

    Private mSearchDT As Data.DataTable
    Private mSearchData As OracleDataReader

End Class

<Serializable()> _
Public Class clsOutageConflicts
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
    Public Function Search(ByVal OutageNumber As String) As OracleDataReader
        'Perform Search 
        GetOutageConflicts(OutageNumber, True)
        Return SearchData
    End Function
    Public Function GetDataTable(ByVal OutageNumber As String) As Data.DataTable
        GetOutageConflicts(OutageNumber, True)
        Return SearchDT
    End Function
    Public Sub GetOutageConflicts(ByVal OutageNumber As String, Optional ByVal createDataTable As Boolean = False)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters

        Try
            param = New OracleParameter
            param.ParameterName = "in_OutageNumber"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = OutageNumber
            'param.Value = 11862
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsOutageConflicts"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)


            'ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.mttOUTAGEreplication.SelectEmailOutageTasks", key, 3)
            Dim dr As OracleDataReader = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "Reladmin.Outage.GetOutageConflicts")

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
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
            paramCollection = Nothing
        End Try
    End Sub

    Private mSearchDT As Data.DataTable
    Private mSearchData As OracleDataReader

End Class
<Serializable()> _
Public Class clsOutageTemplateSecurity
    Public Structure OutageSecurity
        Dim UpdateTemplate As Boolean
    End Structure

    Private mOutageSecurity As OutageSecurity
    Public ReadOnly Property IncidentSecurity() As OutageSecurity
        Get
            Return mOutageSecurity
        End Get
    End Property
    Private Sub GetData(ByVal userName As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters

        Try
            param = New OracleParameter
            param.ParameterName = "in_Username"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsTemplateSecurity"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "Outage.OutageTemplateSecurity_" & userName
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.OutageTemplateSecurity", key, 2)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then

                    Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader

                    If dr IsNot Nothing Then
                        dr.Read()
                        If dr.HasRows Then
                            mOutageSecurity.UpdateTemplate = True
                        Else
                            mOutageSecurity.UpdateTemplate = False
                        End If

                    End If

                End If
            End If

        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
            paramCollection = Nothing
        End Try
    End Sub

    Public Sub New(ByVal userName As String)

        GetData(userName)

    End Sub

End Class
<Serializable()> _
Public Class clsCritique
    Private mCritiqueItemDT As Data.DataTableReader

    Public Property CritiqueItemDT() As Data.DataTableReader
        Get
            Return mCritiqueItemDT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mCritiqueItemDT = value
        End Set
    End Property
    Private mCritiquePerformanceDT As Data.DataTableReader

    Public Property CritiquePerformanceDT() As Data.DataTableReader
        Get
            Return mCritiquePerformanceDT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mCritiquePerformanceDT = value
        End Set
    End Property
    Private mCritiqueWODT As Data.DataTableReader

    Public Property CritiqueWODT() As Data.DataTableReader
        Get
            Return mCritiqueWODT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mCritiqueWODT = value
        End Set
    End Property
    Private mCritiqueQADT As Data.DataTableReader

    Public Property CritiqueQADT() As Data.DataTableReader
        Get
            Return mCritiqueQADT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mCritiqueQADT = value
        End Set
    End Property
    Private mCritiqueAvailDT As Data.DataTableReader

    Public Property CritiqueAvailDT() As Data.DataTableReader
        Get
            Return mCritiqueAvailDT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mCritiqueAvailDT = value
        End Set
    End Property
    Public Property OutageNumber() As String
        Get
            'If mOutageNumber.Length = 0 Then mOutageNumber = Request.QueryString("OutageNumber")
            Return mOutageNumber
        End Get
        Set(ByVal value As String)
            mOutageNumber = value
        End Set
    End Property
    Private mOutageNumber As String = String.Empty
    Public Property ActualHrs() As String
        Get
            Return mActualHrs
        End Get
        Set(ByVal value As String)
            mActualHrs = value.Trim
        End Set
    End Property
    Private mActualHrs As String
    Public Property PlannedHrs() As String
        Get
            Return mPlannedHrs
        End Get
        Set(ByVal value As String)
            mPlannedHrs = value.Trim
        End Set
    End Property
    Private mPlannedHrs As String
    Public Property VarianceHrs() As String
        Get
            Return mVarianceHrs
        End Get
        Set(ByVal value As String)
            mVarianceHrs = value.Trim
        End Set
    End Property
    Private mVarianceHrs As String
    Public Property VariancePercent() As String
        Get
            Return mVariancePercent
        End Get
        Set(ByVal value As String)
            mVariancePercent = value.Trim
        End Set
    End Property
    Private mVariancePercent As String
    Public Property OutageTitle() As String
        Get
            Return mOutageTitle
        End Get
        Set(ByVal value As String)
            mOutageTitle = value.Trim
        End Set
    End Property
    Private mOutageTitle As String
    Public Property Cost() As Integer
        Get
            Return mCost
        End Get
        Set(ByVal value As Integer)
            mCost = value
        End Set
    End Property
    Private mCost As Integer
    Public Property ActualCost() As Integer
        Get
            Return mActualCost
        End Get
        Set(ByVal value As Integer)
            mActualCost = value
        End Set
    End Property
    Private mActualCost As Integer
    Public Property CostVariance() As Integer
        Get
            Return mCostVariance
        End Get
        Set(ByVal value As Integer)
            mCostVariance = value
        End Set
    End Property
    Private mCostVariance As Integer
    Public Property Downtime1() As String
        Get
            Return mDowntime1
        End Get
        Set(ByVal value As String)
            mDowntime1 = value.Trim
        End Set
    End Property
    Private mDowntime1 As String
    Public Property Downtime2() As String
        Get
            Return mDowntime2
        End Get
        Set(ByVal value As String)
            mDowntime2 = value.Trim
        End Set
    End Property
    Private mDowntime2 As String
    Public Property Downtime3() As String
        Get
            Return mDowntime3
        End Get
        Set(ByVal value As String)
            mDowntime3 = value.Trim
        End Set
    End Property
    Private mDowntime3 As String
    Public Property Downtime4() As String
        Get
            Return mDowntime4
        End Get
        Set(ByVal value As String)
            mDowntime4 = value.Trim
        End Set
    End Property
    Private mDowntime4 As String
    Public Property Downtime5() As String
        Get
            Return mDowntime5
        End Get
        Set(ByVal value As String)
            mDowntime5 = value.Trim
        End Set
    End Property
    Private mDowntime5 As String
    Public Property Downtime6() As String
        Get
            Return mDowntime6
        End Get
        Set(ByVal value As String)
            mDowntime6 = value.Trim
        End Set
    End Property
    Private mDowntime6 As String
    Public Property Downtime7() As String
        Get
            Return mDowntime7
        End Get
        Set(ByVal value As String)
            mDowntime7 = value.Trim
        End Set
    End Property
    Private mDowntime7 As String
    Public Property UserName() As String
        Get
            Return mUserName
        End Get
        Set(ByVal value As String)
            mUserName = value.Trim
        End Set
    End Property
    Private mUserName As String
    Public Property PerformanceItemDesc() As String
        Get
            Return mPerformanceItemDesc
        End Get
        Set(ByVal value As String)
            mPerformanceItemDesc = value.Trim
        End Set
    End Property
    Private mPerformanceItemDesc As String
    Public Property PerformancePlannedStart() As String
        Get
            Return mPerformancePlannedStart
        End Get
        Set(ByVal value As String)
            mPerformancePlannedStart = value.Trim
        End Set
    End Property
    Private mPerformancePlannedStart As String
    Public Property PerformancePlannedEnd() As String
        Get
            Return mPerformancePlannedEnd
        End Get
        Set(ByVal value As String)
            mPerformancePlannedEnd = value.Trim
        End Set
    End Property
    Private mPerformancePlannedEnd As String
    Public Property PerformanceActualStart() As String
        Get
            Return mPerformanceActualStart
        End Get
        Set(ByVal value As String)
            mPerformanceActualStart = value.Trim
        End Set
    End Property
    Private mPerformanceActualStart As String
    Public Property PerformanceActualEnd() As String
        Get
            Return mPerformanceActualEnd
        End Get
        Set(ByVal value As String)
            mPerformanceActualEnd = value.Trim
        End Set
    End Property
    Private mPerformanceActualEnd As String
    Public Property CritiqueCategory() As String
        Get
            Return mCritiqueCategory
        End Get
        Set(ByVal value As String)
            mCritiqueCategory = value.Trim
        End Set
    End Property
    Private mCritiqueCategory As String
    Public Property CritiqueSubCategory() As String
        Get
            Return mCritiqueSubCategory
        End Get
        Set(ByVal value As String)
            mCritiqueSubCategory = value.Trim
        End Set
    End Property
    Private mCritiqueSubCategory As String
    Public Property BusinessUnit() As String
        Get
            Return mBusinessUnit
        End Get
        Set(ByVal value As String)
            mBusinessUnit = value.Trim
        End Set
    End Property
    Private mBusinessUnit As String
    Public Property BusinessUnitAreaLine() As String
        Get
            Return mBusinessUnitAreaLine
        End Get
        Set(ByVal value As String)
            mBusinessUnitAreaLine = value.Trim
        End Set
    End Property
    Private mBusinessUnitAreaLine As String

    Public Property CritiqueItemDesc() As String
        Get
            Return mCritiqueItemDesc
        End Get
        Set(ByVal value As String)
            mCritiqueItemDesc = value.Trim
        End Set
    End Property
    Private mCritiqueItemDesc As String
    Public Property PlannedMech() As String
        Get
            Return mPlannedMech
        End Get
        Set(ByVal value As String)
            mPlannedMech = value.Trim
        End Set
    End Property
    Private mPlannedMech As String
    Public Property PlannedElec() As String
        Get
            Return mPlannedElec
        End Get
        Set(ByVal value As String)
            mPlannedElec = value.Trim
        End Set
    End Property
    Private mPlannedElec As String
    Public Property PlannedCont() As String
        Get
            Return mPlannedCont
        End Get
        Set(ByVal value As String)
            mPlannedCont = value.Trim
        End Set
    End Property
    Private mPlannedCont As String
    Public Property CompletedMech() As String
        Get
            Return mCompletedMech
        End Get
        Set(ByVal value As String)
            mCompletedMech = value.Trim
        End Set
    End Property
    Private mCompletedMech As String
    Public Property CompletedElec() As String
        Get
            Return mCompletedElec
        End Get
        Set(ByVal value As String)
            mCompletedElec = value.Trim
        End Set
    End Property
    Private mCompletedElec As String
    Public Property CompletedCont() As String
        Get
            Return mCompletedCont
        End Get
        Set(ByVal value As String)
            mCompletedCont = value.Trim
        End Set
    End Property
    Private mCompletedCont As String
    Public Property QACompleteCount() As String
        Get
            Return mQACompleteCount
        End Get
        Set(ByVal value As String)
            mQACompleteCount = value.Trim
        End Set
    End Property
    Private mQACompleteCount As String
    Public Property QAPlannedCount() As String
        Get
            Return mQAPlannedCount
        End Get
        Set(ByVal value As String)
            mQAPlannedCount = value.Trim
        End Set
    End Property
    Private mQAPlannedCount As String
    'Public Property BusUnit() As String
    '    Get
    '        Return mBusUnit
    '    End Get
    '    Set(ByVal value As String)
    '        mBusUnit = value.Trim
    '    End Set
    'End Property
    'Private mBusUnit As String
    Public mBusUnit As Data.DataTable
    Public mBusUnitAreaLine As Data.DataTable

    Private Sub GetCritique()

        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        ' OutageNumber = "3335"
        param = New OracleParameter
        param.ParameterName = "in_outagenumber"
        param.oracledbtype = oracledbtype.VarChar
        param.Value = OutageNumber
        param.Direction = Data.ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsCritique"
        param.oracledbtype = oracledbtype.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsOutagePerformance"
        param.oracledbtype = oracledbtype.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsWorkOrder"
        param.oracledbtype = oracledbtype.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)


        param = New OracleParameter
        param.ParameterName = "rsQuality"
        param.oracledbtype = oracledbtype.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)


        param = New OracleParameter
        param.ParameterName = "rsOutage"
        param.oracledbtype = oracledbtype.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsBusUnit"
        param.oracledbtype = oracledbtype.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsBusUnitAreaLine"
        param.oracledbtype = oracledbtype.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsAvailability"
        param.oracledbtype = oracledbtype.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)



        Try

            Dim key As String = "OutageCritique.GetOutageCritique_" & OutageNumber
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage_Critique.GETOutageCritique", key, 0)

            If ds IsNot Nothing Then
                'Dim dr As OracleDataReader = Nothing
                Dim dr1 As Data.DataTableReader = ds.Tables(0).CreateDataReader

                'Depending on the contents of ds, show table to add first record and bind data
                If dr1 IsNot Nothing Then
                    CritiqueItemDT = dr1
                    '_gvcritique.DataSource = dr1
                    '_gvcritique.DataBind()
                End If
                'dr1.Close()

                Dim dr2 As Data.DataTableReader = ds.Tables(1).CreateDataReader
                If dr2 IsNot Nothing Then
                    CritiquePerformanceDT = dr2
                    '_gvcritiqueperformance.DataSource = dr2
                    '_gvcritiqueperformance.DataBind()
                End If
                'dr2.Close()
            End If


            Dim dr3 As Data.DataTableReader = ds.Tables(2).CreateDataReader

            'Depending on the contents of ds, show table to add first record and bind data
            If dr3 IsNot Nothing Then
                CritiqueWODT = dr3
                ' _gvcritiqueWO.DataSource = dr3
                ' _gvcritiqueWO.DataBind()
            End If
            ' dr3.Close()


            Dim dr4 As Data.DataTableReader = ds.Tables(3).CreateDataReader

            'Depending on the contents of ds, show table to add first record and bind data
            If dr4 IsNot Nothing Then
                CritiqueQADT = dr4
                '_gvcritiqueQA.DataSource = dr4
                '_gvcritiqueQA.DataBind()
            End If
            ' dr4.Close()

            Dim dr5 As Data.DataTableReader = ds.Tables(4).CreateDataReader
            If dr5 IsNot Nothing Then
                If dr5.HasRows Then
                    dr5.Read()

                    With Me
                        '.ActualHrs = dr2.Item("actualhrs")
                        '.PlannedHrs = dr2.Item("plannedhrs")
                        '.VarianceHrs = dr2.Item("variancehrs")
                        '.VariancePercent = dr2.Item("variancepercent")
                        .OutageTitle = DataClean(dr5.Item("outagetitle"))
                        .Cost = CInt(dr5.Item("cost"))
                        .ActualCost = CInt(dr5.Item("actualcost"))
                    End With
                    CostVariance = ActualCost - Cost
                End If
            End If
            ' dr5.Close()

            'Me._lblOutageTitle.Text = OutageTitle
            'Me._lblOutageNumber.Text = " (" & OutageNumber & ") "
            ' Me._tbActualCost.Text = ActualCost.ToString("C")
            ' Me._tbPlannedCost.Text = Cost.ToString("C")
            'CostVariance = ActualCost - Cost
            ' Me._tbCostVariance.Text = CostVariance.ToString("C")



            'If dr6 IsNot Nothing Then
            '    Me._ddlContractor.DataSource = dr6
            '    Me._ddlContractor.DataBind()
            'End If
            'dr6.Close()

            mBusUnit = ds.Tables(5)
            mBusUnitAreaLine = ds.Tables(6)
            'mBusUnit.DataTextField = "busunit"
            'mBusUnit.DataValueField = "busunit"

            'RI.SharedFunctions.BindList(Me._ddlContractor, Me.mContractor, False, True)


            Dim dr7 As Data.DataTableReader = ds.Tables(7).CreateDataReader
            If dr7 IsNot Nothing Then
                CritiqueAvailDT = dr7
                ' _gvcritiqueavailability.DataSource = dr7
                ' _gvcritiqueavailability.DataBind()
            End If
            ' dr7.Close()

        Catch ex As Exception
            Throw
        Finally
            'If dr1 IsNot Nothing Then
            '    dr1.Close()
            '    dr1 = Nothing
            'End If
        End Try
    End Sub
    Public Sub New(ByVal OutageNum As String)
        If OutageNum IsNot Nothing And OutageNum.Length > 0 Then
            OutageNumber = OutageNum
            GetCritique()
        End If
    End Sub
    Public Function SaveCritiqueAvail(ByVal outagenumber As String, ByVal AVAILABILITYSEQID As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_OUTAGENUMBER"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = outagenumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_AVAILABILITYSEQID"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = AVAILABILITYSEQID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_DOWNTIME1"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Downtime1
            paramCollection.Add(param)
            param = New OracleParameter
            param.ParameterName = "IN_DOWNTIME2"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Downtime2
            paramCollection.Add(param)
            param = New OracleParameter
            param.ParameterName = "IN_DOWNTIME3"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Downtime3
            paramCollection.Add(param)
            param = New OracleParameter
            param.ParameterName = "IN_DOWNTIME4"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Downtime4
            paramCollection.Add(param)
            param = New OracleParameter
            param.ParameterName = "IN_DOWNTIME5"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Downtime5
            paramCollection.Add(param)
            param = New OracleParameter
            param.ParameterName = "IN_DOWNTIME6"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Downtime6
            paramCollection.Add(param)
            param = New OracleParameter
            param.ParameterName = "IN_DOWNTIME7"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Downtime7
            paramCollection.Add(param)



            param = New OracleParameter
            param.ParameterName = "IN_BUSUNITAREALINE"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.BusinessUnitAreaLine
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "IN_USER"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.UserName
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "out_status"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.outage_critique.SaveCritiqueAvail")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Outage Resource " & outagenumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCritiquePerformance(ByVal outagenumber As String, ByVal PERFORMANCETYPESEQID As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_OUTAGENUMBER"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = outagenumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_PERFORMANCETYPESEQID"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = PERFORMANCETYPESEQID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_PERFORMANCEITEMDESC"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.PerformanceItemDesc
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_PERFORMANCEPLANNEDSTART"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.PerformancePlannedStart
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_PERFORMANCEPLANNEDEND"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.PerformancePlannedEnd
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "IN_PERFORMANCEACTUALSTART"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.PerformanceActualStart
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_PERFORMANCEACTUALEND"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.PerformanceActualEnd
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_USER"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.UserName
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "out_status"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.outage_critique.SaveCritiquePerf")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Outage Critique " & outagenumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Shared Sub DeleteCritiqueAvail(ByVal AVAILABILITYSEQID As String, ByVal userName As String)

        Dim paramCollection As New OracleParameterCollection

        Try
            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "IN_AVAILABILITYSEQID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = AVAILABILITYSEQID
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "IN_USER"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userName
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim status As String
            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.outage_critique.DeleteCritiqueAvail")

        Catch ex As Exception
            Throw New Data.DataException("DeleteCritiqueAvail", ex)
        End Try

    End Sub

    Public Shared Sub DeleteCritiquePerf(ByVal PERFORMANCETYPESEQID As String, ByVal userName As String)

        Dim paramCollection As New OracleParameterCollection

        Try
            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "IN_PERFORMANCETYPESEQID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = PERFORMANCETYPESEQID
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "IN_USER"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userName
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim status As String
            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.outage_critique.DeleteCritiquePerf")

        Catch ex As Exception
            Throw New Data.DataException("DeleteCritiquePerf", ex)
        End Try

    End Sub
    Public Function SaveCritiqueItem(ByVal outagenumber As String, ByVal OUTAGECRITIQUESEQID As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_OUTAGENUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = outagenumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_OUTAGECRITIQUESEQID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = OUTAGECRITIQUESEQID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_CATEGORYSEQID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.CritiqueCategory
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "IN_SUBCATEGORYSEQID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.CritiqueSubCategory
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_BUSUNIT"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.BusinessUnit
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_DESC"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.CritiqueItemDesc
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_USER"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.UserName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.outage_critique.SaveCritiqueItem")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Outage Critique " & outagenumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Shared Sub DeleteCritique(ByVal OUTAGECRITIQUESEQID As String, ByVal userName As String)

        Dim paramCollection As New OracleParameterCollection

        Try
            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "IN_OUTAGECRITIQUESEQID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = OUTAGECRITIQUESEQID
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "IN_USER"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userName
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim status As String
            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.outage_critique.DeleteCritique")

        Catch ex As Exception
            Throw New Data.DataException("DeleteCritique", ex)
        End Try

    End Sub
    Public Function SaveCritiqueWO(ByVal outagenumber As String, ByVal OUTAGECRITIQUEWOSEQID As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_OUTAGENUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = outagenumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_OUTAGECRITIQUEWOSEQID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = OUTAGECRITIQUEWOSEQID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_PLANNEDMECH"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.PlannedMech
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "IN_PLANNEDELEC"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.PlannedElec
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_PLANNEDCONT"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.PlannedCont
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_COMPLETEDMECH"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.CompletedMech
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_COMPLETEDELEC"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.CompletedElec
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_COMPLETEDCONT"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.CompletedCont
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_USER"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.UserName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.outage_critique.SaveCritiqueWO")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Outage Work Order Counts" & outagenumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCritiqueCost(ByVal outagenumber As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_OUTAGENUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = outagenumber
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "IN_COST"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Cost
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_ACTUALCOST"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.ActualCost
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "IN_USER"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.UserName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.outage_critique.SaveCritiqueCost")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Outage Cost" & outagenumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCritiqueQA(ByVal outagenumber As String, ByVal OUTAGECRITIQUEQUALITYSEQID As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_OUTAGENUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = outagenumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_OUTAGECRITIQUEQUALITYSEQID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = OUTAGECRITIQUEQUALITYSEQID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_QAPLANNEDCOUNT"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.QAPlannedCount
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_QACOMPLETECOUNT"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.QACompleteCount
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_USER"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.UserName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.outage_critique.SaveCritiqueQA")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Outage Critique " & outagenumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
