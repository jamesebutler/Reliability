Option Explicit On
Option Strict On

Imports Microsoft.VisualBasic
Imports Devart.Data.Oracle
Imports RI.SharedFunctions
<Serializable()> _
Public Class clsEnterMOC
    '    Public Structure MOCSecurity
    '        Dim DeleteMOCs As Boolean
    '        Dim UpdateMOCs As Boolean
    '        Dim SaveMOCs As Boolean
    '    End Structure

    '    Enum PageMode
    '        Update = 1
    '        NewMOC = 2
    '    End Enum

    '#Region "Fields"
    '    Private mMOCSecurity As MOCSecurity
    '#End Region
    '#Region "Properties"
    '    Public ReadOnly Property IncidentSecurity() As MOCSecurity
    '        Get
    '            Return mMOCSecurity
    '        End Get
    '    End Property
    '#End Region


    'Private mCurrentPageMode As PageMode = PageMode.NewMOC
    'Public ReadOnly Property CurrentPageMode() As PageMode
    '    Get
    '        Return mCurrentPageMode
    '    End Get
    'End Property
    Public ReadOnly Property Category() As clsData
        Get
            Return mCategory
        End Get
    End Property
    Private mCategory As New clsData
    Public ReadOnly Property SubCategory() As clsData
        Get
            Return mSubCategory
        End Get
    End Property
    Private mSubCategory As New clsData
    Public ReadOnly Property Classification() As clsData
        Get
            Return mClassification
        End Get
    End Property
    Private mClassification As New clsData
    Public ReadOnly Property System() As clsData
        Get
            Return mSystem
        End Get
    End Property
    Private mSystem As New clsData
    Private mSystemDT As Data.DataTableReader

    Public Property SystemDT() As Data.DataTableReader
        Get
            Return mSystemDT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mSystemDT = value
        End Set
    End Property
    Private Sub GetData(ByVal userName As String, ByVal facility As String, ByVal inActiveFlag As String, Optional ByVal division As String = "", Optional ByVal busArea As String = "", Optional ByVal lineBreak As String = "", Optional ByVal MOCNumber As String = "")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters

        Try
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

            param = New OracleParameter
            param.ParameterName = "in_Username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userName
            paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsFacility"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsBusUnitArea"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsPerson"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsAuthLevel"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCCategory"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCClassification"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCSystem"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "NewMOC.NewMOCDDL" & facility & "_" & division & "_" & inActiveFlag & "_" & userName
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.newmoc.NewMOCDDL", key, 3)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 4 Then
                    'mAuthLevel = "YES"
                    'mAuthLevelid = CInt(dr.Item("AuthLevelid"))

                    'Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader
                    'If dr IsNot Nothing Then
                    '    dr.Read()
                    '    If dr.HasRows Then
                    '        '        mAuthLevel = "YES"
                    '        '                            mAuthLevelid = CInt(dr.Item("AuthLevelid"))
                    '        mMOCSecurity.DeleteMOCs = True
                    '        mMOCSecurity.UpdateMOCs = True
                    '    Else
                    '        '                           '        mAuthLevel = "NO"
                    '        '                          mAuthLevelid = 1
                    '        mMOCSecurity.DeleteMOCs = False
                    '        mMOCSecurity.UpdateMOCs = False
                    '    End If

                    'End If

                    ''rsMOCCategory                     
                    'mCategory.DataSource = ds.Tables(1).CreateDataReader
                    'mCategory.DataTextField = "MOCCategory"
                    'mCategory.DataValueField = "MOCCategory"

                    'rsMOCClassification                     
                    mClassification.DataSource = ds.Tables(2).CreateDataReader
                    mClassification.DataTextField = "MOCClassification"
                    mClassification.DataValueField = "MOCClassification"

                    'rsMOCSystem  
                    SystemDT = ds.Tables(3).CreateDataReader

                End If
            End If

        Catch ex As Exception
            Throw New Data.DataException("Error Getting MOC Data")
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
            paramCollection = Nothing
        End Try
    End Sub

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

    Private mAuthLevelid As Integer
    Private mAuthLevel As String


    Public Sub New(ByVal userName As String, ByVal facility As String, ByVal inActiveFlag As String, Optional ByVal division As String = "", Optional ByVal busArea As String = "", Optional ByVal lineBreak As String = "", Optional ByVal MOCNumber As String = "")

        GetData(userName, facility, inActiveFlag, division, busArea, lineBreak, MOCNumber)
        'If MOCNumber.Length = 0 Then
        '    Me.mCurrentPageMode = PageMode.NewMOC
        'Else
        '    Me.mCurrentPageMode = PageMode.Update
        'End If
    End Sub

End Class

<Serializable()> _
Public Class clsCurrentMOC
    Public Property Division() As String
        Get
            Return mDivision
        End Get
        Set(ByVal value As String)
            mDivision = value.Trim
        End Set
    End Property
    Private mDivision As String
    Public Property SiteID() As String
        Get
            Return mSiteID
        End Get
        Set(ByVal value As String)
            mSiteID = value.Trim
        End Set
    End Property
    Private mSiteID As String
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
    Public Property FunctionalLocation() As String
        Get
            Return mFunctionalLocation
        End Get
        Set(ByVal value As String)
            mFunctionalLocation = value.Trim
        End Set
    End Property
    Private mFunctionalLocation As String = String.Empty
    Public Property MOCComment() As String
        Get
            Return mMOCComment
        End Get
        Set(ByVal value As String)
            mMOCComment = value.Trim
        End Set
    End Property
    Private mMOCComment As String = String.Empty
    Public Property Title() As String
        Get
            Return mTitle
        End Get
        Set(ByVal value As String)
            mTitle = value.Trim
        End Set
    End Property
    Private mTitle As String
    Public Property Status() As String
        Get
            Return mStatus
        End Get
        Set(ByVal value As String)
            mStatus = value.Trim
        End Set
    End Property
    Private mStatus As String
    Private mMOCNumber As String
    Public Property MOCNumber() As String
        Get
            Return mMOCNumber
        End Get
        Set(ByVal value As String)
            mMOCNumber = value.Trim
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
    Public Property AICompDate() As String
        Get
            Return mActionCompDate
        End Get
        Set(ByVal value As String)
            mActionCompDate = value
        End Set
    End Property
    Private mActionCompDate As String
    Public Property Impact() As String
        Get
            Return mImpact
        End Get
        Set(ByVal value As String)
            mImpact = value.Trim
        End Set
    End Property
    Private mImpact As String
    Private mSavings As String = "0"
    Public Property Savings() As String
        Get
            Return mSavings
        End Get
        Set(ByVal value As String)
            mSavings = value
        End Set
    End Property
    Private mCosts As String = "0"
    Public Property Costs() As String
        Get
            Return mCosts
        End Get
        Set(ByVal value As String)
            mCosts = value
        End Set
    End Property
    Public Property UserName() As String
        Get
            Return mUserName
        End Get
        Set(ByVal value As String)
            mUserName = value.Trim
        End Set
    End Property
    Private mUserName As String

    Public Property MOCCoordinator() As String
        Get
            Return mMOCCoordinator
        End Get
        Set(ByVal value As String)
            mMOCCoordinator = value.Trim
        End Set
    End Property
    Private mMOCCoordinator As String
    Public Property MOCCoordinatorEmail() As String
        Get
            Return mMOCCoordinatorEmail
        End Get
        Set(ByVal value As String)
            mMOCCoordinatorEmail = value.Trim
        End Set
    End Property
    Private mMOCCoordinatorEmail As String
    Public Property MOCType() As String
        Get
            Return mMOCType
        End Get
        Set(ByVal value As String)
            mMOCType = value.Trim
        End Set
    End Property
    Private mMOCType As String

    Public Property MOCCoordinatorName() As String
        Get
            Return mMOCCoordinatorName
        End Get
        Set(ByVal value As String)
            mMOCCoordinatorName = value
        End Set
    End Property
    Private mMOCCoordinatorName As String
    Private mInsertUsername As String
    Public Property Category() As String
        Get
            Return mCategory
        End Get
        Set(ByVal value As String)
            mCategory = value.Trim
        End Set
    End Property
    Private mCategory As String = String.Empty
    Public Property SubCategory() As String
        Get
            Return mSubCategory
        End Get
        Set(ByVal value As String)
            mSubCategory = value.Trim
        End Set
    End Property
    Private mSubCategory As String
    Public Property EquipSubCategory() As String
        Get
            Return mEquipSubCategory
        End Get
        Set(ByVal value As String)
            mEquipSubCategory = value.Trim
        End Set
    End Property
    Private mEquipSubCategory As String
    Public Property MarketChannelSubCategory() As String
        Get
            Return mMarketChannelSubCategory
        End Get
        Set(ByVal value As String)
            mMarketChannelSubCategory = value.Trim
        End Set
    End Property
    Private mMarketChannelSubCategory As String
    Public Property Classification() As String
        Get
            Return mClassification
        End Get
        Set(ByVal value As String)
            mClassification = value.Trim
        End Set
    End Property
    Private mClassification As String = String.Empty
    Public Property SystemFacility() As String
        Get
            Return mSystemFacility
        End Get
        Set(ByVal value As String)
            mSystemFacility = value.Trim
        End Set
    End Property
    Private mSystemFacility As String = String.Empty
    Public Property SystemPerson() As String
        Get
            Return mSystemPerson
        End Get
        Set(ByVal value As String)
            mSystemPerson = value.Trim
        End Set
    End Property
    Private mSystemPerson As String = String.Empty
    Public Property SystemPriority() As String
        Get
            Return mSystemPriority
        End Get
        Set(ByVal value As String)
            mSystemPriority = value.Trim
        End Set
    End Property
    Private mSystemPriority As String = String.Empty
    Public Property SystemDaysAfter() As String
        Get
            Return mSystemDaysAfter
        End Get
        Set(ByVal value As String)
            mSystemDaysAfter = value.Trim
        End Set
    End Property
    Private mSystemDaysAfter As String = String.Empty
    Public Property SystemDueDate() As String
        Get
            Return mSystemDueDate
        End Get
        Set(ByVal value As String)
            mSystemDueDate = value
        End Set
    End Property
    Private mSystemDueDate As String = String.Empty
    Public Property SystemTaskItem() As String
        Get
            Return mSystemTaskItem
        End Get
        Set(ByVal value As String)
            mSystemTaskItem = value.Trim
        End Set
    End Property
    Private mSystemTaskItem As String = String.Empty
    Public Property System() As String
        Get
            Return mSystem
        End Get
        Set(ByVal value As String)
            mSystem = value.Trim
        End Set
    End Property
    Private mSystem As String = String.Empty
    Public Property SystemStatus() As String
        Get
            Return mSystemStatus
        End Get
        Set(ByVal value As String)
            mSystemStatus = value.Trim
        End Set
    End Property
    Private mSystemStatus As String = String.Empty
    Public Property SystemTitle() As String
        Get
            Return mSystemTitle
        End Get
        Set(ByVal value As String)
            mSystemTitle = value.Trim
        End Set
    End Property
    Private mSystemTitle As String = String.Empty
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
    Private mApproved As String
    Public Property Approved() As String
        Get
            Return mApproved
        End Get
        Set(ByVal value As String)
            mApproved = value
        End Set
    End Property
    Private mApprovalDate As String
    Public Property ApprovalDate() As String
        Get
            Return mApprovalDate
        End Get
        Set(ByVal value As String)
            mApprovalDate = value
        End Set
    End Property
    Private mApproverUserName As String
    Public Property ApproverUserName() As String
        Get
            Return mApproverUserName
        End Get
        Set(ByVal value As String)
            mApproverUserName = value
        End Set
    End Property
    Private mRoleSeqID As String = String.Empty
    Public Property RoleSeqID() As String
        Get
            Return mRoleSeqID
        End Get
        Set(ByVal value As String)
            mRoleSeqID = value.Trim
        End Set
    End Property
    Private mNotificationL1List As String = String.Empty
    Public Property NotificationL1List() As String
        Get
            Return mNotificationL1List
        End Get
        Set(ByVal value As String)
            mNotificationL1List = value
        End Set
    End Property
    Private mNotificationL2List As String = String.Empty
    Public Property NotificationL2List() As String
        Get
            Return mNotificationL2List
        End Get
        Set(ByVal value As String)
            mNotificationL2List = value
        End Set
    End Property

    Private mNotificationL3List As String = String.Empty
    Public Property NotificationL3List() As String
        Get
            Return mNotificationL3List
        End Get
        Set(ByVal value As String)
            mNotificationL3List = value
        End Set
    End Property

    Private mNotificationEList As String = String.Empty
    Public Property NotificationEList() As String
        Get
            Return mNotificationEList
        End Get
        Set(ByVal value As String)
            mNotificationEList = value
        End Set
    End Property

    Private mUSDTicket As String = String.Empty
    Public Property USDTicket() As String
        Get
            Return mUSDTicket
        End Get
        Set(ByVal value As String)
            mUSDTicket = value
        End Set
    End Property

    Private mMTTTaskHeaderSeqId As String = String.Empty
    Public Property MTTTaskHeaderSeqId() As String
        Get
            Return mMTTTaskHeaderSeqId
        End Get
        Set(ByVal value As String)
            mMTTTaskHeaderSeqId = value
        End Set
    End Property

    Private mRequired As String = String.Empty
    Public Property Required() As String
        Get
            Return mRequired
        End Get
        Set(ByVal value As String)
            mRequired = value
        End Set
    End Property
    '***************************************
    'MJP - Added on 5/10/2010
    Private mAllNotificationToList As String = String.Empty
    Public Property AllNotificationToList() As String
        Get
            Return mAllNotificationToList
        End Get
        Set(ByVal value As String)
            mAllNotificationToList = value
        End Set
    End Property

    Private mAllNotificationCopyList As String = String.Empty
    Public Property AllNotificationcopyList() As String
        Get
            Return mAllNotificationCopyList
        End Get
        Set(ByVal value As String)
            mAllNotificationCopyList = value
        End Set
    End Property
    '***************************************
    Private mComments As String = String.Empty
    Public Property Comments() As String
        Get
            Return mComments
        End Get
        Set(ByVal value As String)
            mComments = value
        End Set
    End Property

    Private mRoles As String = String.Empty
    Public Property Roles() As String
        Get
            Return mRoles
        End Get
        Set(ByVal value As String)
            mRoles = value
        End Set
    End Property
    Private mNotificationL1FullName As String = String.Empty
    Public Property NotificationL1FullName() As String
        Get
            Return mNotificationL1FullName
        End Get
        Set(ByVal value As String)
            mNotificationL1FullName = value
        End Set
    End Property
    Private mNotificationL2FullName As String = String.Empty
    Public Property NotificationL2FullName() As String
        Get
            Return mNotificationL2FullName
        End Get
        Set(ByVal value As String)
            mNotificationL2FullName = value
        End Set
    End Property
    Private mNotificationL3FullName As String = String.Empty
    Public Property NotificationL3FullName() As String
        Get
            Return mNotificationL3FullName
        End Get
        Set(ByVal value As String)
            mNotificationL3FullName = value
        End Set
    End Property
    Private mNotificationEFullName As String = String.Empty
    Public Property NotificationEFullName() As String
        Get
            Return mNotificationEFullName
        End Get
        Set(ByVal value As String)
            mNotificationEFullName = value
        End Set
    End Property

    Private mNotificationEmail As String = String.Empty
    Public Property NotificationEmail() As String
        Get
            Return mNotificationEmail
        End Get
        Set(ByVal value As String)
            mNotificationEmail = value
        End Set
    End Property
    Private mTaskCount As String = String.Empty
    Public Property TaskCount() As String
        Get
            Return mTaskCount
        End Get
        Set(ByVal value As String)
            mTaskCount = value
        End Set
    End Property
    Private mAttachmentCount As String = String.Empty
    Public Property AttachmentCount() As String
        Get
            Return mAttachmentCount
        End Get
        Set(ByVal value As String)
            mAttachmentCount = value
        End Set
    End Property
    Private mApprovalL1DT As Data.DataTableReader

    Public Property ApprovalL1DT() As Data.DataTableReader
        Get
            Return mApprovalL1DT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mApprovalL1DT = value
        End Set
    End Property
    Private mApprovalL2DT As Data.DataTableReader

    Public Property ApprovalL2DT() As Data.DataTableReader
        Get
            Return mApprovalL2DT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mApprovalL2DT = value
        End Set
    End Property
    Private mApprovalL3DT As Data.DataTableReader

    Public Property ApprovalL3DT() As Data.DataTableReader
        Get
            Return mApprovalL3DT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mApprovalL3DT = value
        End Set
    End Property

    Private mInformedDT As Data.DataTableReader

    Public Property InformedDT() As Data.DataTableReader
        Get
            Return mInformedDT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mInformedDT = value
        End Set
    End Property

    Private mMOCCommentsDT As Data.DataTableReader

    Public Property MOCCommentsDT() As Data.DataTableReader
        Get
            Return mMOCCommentsDT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mMOCCommentsDT = value
        End Set
    End Property

    Private mMOCPendingTempTasksDT As Data.DataTableReader

    Public Property MOCPendingTempTasksDT() As Data.DataTableReader
        Get
            Return mMOCPendingTempTasksDT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mMOCPendingTempTasksDT = value
        End Set
    End Property

    Private mMOCTemplateTasksDaysAfterDT As Data.DataTableReader

    Public Property MOCTemplateTasksDaysAfterDT() As Data.DataTableReader
        Get
            Return mMOCTemplateTasksDaysAfterDT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mMOCTemplateTasksDaysAfterDT = value
        End Set
    End Property

    Private mMOCTemplateTasksDueDateDT As Data.DataTableReader

    Public Property MOCTemplateTasksDueDateDT() As Data.DataTableReader
        Get
            Return mMOCTemplateTasksDueDateDT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mMOCTemplateTasksDueDateDT = value
        End Set
    End Property
    Private mMOCTasksDT As Data.DataTableReader

    Public Property MOCTasksDT() As Data.DataTableReader
        Get
            Return mMOCTasksDT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mMOCTasksDT = value
        End Set
    End Property

    Private mMOCClassQuestionsDT As Data.DataTableReader

    Public Property MOCClassQuestionsDT() As Data.DataTableReader
        Get
            Return mMOCClassQuestionsDT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mMOCClassQuestionsDT = value
        End Set
    End Property

    Private mMOCCatQuestionsDT As Data.DataTableReader

    Public Property MOCCatQuestionsDT() As Data.DataTableReader
        Get
            Return mMOCCatQuestionsDT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mMOCCatQuestionsDT = value
        End Set
    End Property

    Private mOwner As String = String.Empty
    Public Property Owner() As String
        Get
            Return mOwner
        End Get
        Set(ByVal value As String)
            mOwner = value
        End Set
    End Property

    Private mOwnerName As String = String.Empty
    Public Property OwnerName() As String
        Get
            Return mOwnerName
        End Get
        Set(ByVal value As String)
            mOwnerName = value
        End Set
    End Property

    Private mOwnerEmail As String = String.Empty
    Public Property OwnerEmail() As String
        Get
            Return mOwnerEmail
        End Get
        Set(ByVal value As String)
            mOwnerEmail = value
        End Set
    End Property

    Private mWorkOrder As String = String.Empty
    Public Property WorkOrder() As String
        Get
            Return mWorkOrder
        End Get
        Set(ByVal value As String)
            mWorkOrder = value
        End Set
    End Property

    Private mCompDate As String = String.Empty
    Public Property CompDate() As String
        Get
            Return mCompDate
        End Get
        Set(ByVal value As String)
            mCompDate = value
        End Set
    End Property

    Private mStatusDesc As String = String.Empty
    Public Property StatusDesc() As String
        Get
            Return mStatusDesc
        End Get
        Set(ByVal value As String)
            mStatusDesc = value
        End Set
    End Property


    Public Shared Sub DeleteCurrentMOC(ByVal MOCNumber As Integer, ByVal userName As String)
        RI.SharedFunctions.Trace("clsEnterMOC.vb", CStr(MOCNumber) + " DeleteCurrentMOC ")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_MOCNUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = MOCNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_USERNAME"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.newmoc.DELETEMOC")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Deleting " & MOCNumber)
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Public Shared Function DeleteMOCApproval(ByVal MOCNumber As String, ByVal userName As String, ByVal approvalType As String, ByVal approvalseqid As String) As String
        'Public Function SaveMOCApproval(ByVal mocnumber As String, ByVal username As String, ByVal type As String) As String
        RI.SharedFunctions.Trace("clsEnterMOC.vb", MOCNumber + " DeleteMOCApproval ")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_MOCNUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = MOCNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_approvalseqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = approvalseqid
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_USERNAME"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_ApprovalType"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = approvalType
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.newmoc.DeleteMOCApproval")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Deleting MOC Approver:" & userName & ", MOC Number:" & MOCNumber)
            End If
        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Function InsertMOCApproval(ByVal mocNumber As String, ByVal username As String, ByVal approvalType As String, ByVal Required As String) As String
        RI.SharedFunctions.Trace("clsEnterMOC.vb", mocNumber + " InsertMOCApproval ")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim connection As String = String.Empty
        Dim provider As String = String.Empty
        Dim roleSeqId As String = String.Empty
        Dim rolePlantCode As String = String.Empty

        Dim userNameArray As String() = username.Split(CChar("/"))

        'Check input paramaters
        Try

            If userNameArray.Length > 1 Then
                roleSeqId = userNameArray(1)
                rolePlantCode = userNameArray(0)
                username = String.Empty
            Else
                username = userNameArray(0)
            End If

            If UCase(username) <> "ALL" Then
                paramCollection = New OracleParameterCollection

                param = New OracleParameter
                param.ParameterName = "IN_MOCNUMBER"
                param.OracleDbType = OracleDbType.Integer
                param.Direction = Data.ParameterDirection.Input
                param.Value = mocNumber
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "IN_USERNAME"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = username
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "IN_APPROVALTYPE"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = approvalType
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "IN_CreatedUSERNAME"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = Me.UserName
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "IN_Required"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = Required
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "IN_Roleseqid"
                param.OracleDbType = OracleDbType.Number
                param.Direction = Data.ParameterDirection.Input
                param.Value = roleSeqId
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "IN_RolePlantCode"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = rolePlantCode
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "out_status"
                param.OracleDbType = OracleDbType.Number
                param.Direction = Data.ParameterDirection.Output
                paramCollection.Add(param)
                Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.newmoc.InsertNewApproval")
                Return returnStatus
                If CDbl(returnStatus) <> 0 Then
                    Throw New Data.DataException("Error Saving Approval " & mocNumber)
                End If
                'Next
            End If
        Catch ex As Exception
            Throw
        End Try
        Return ""
    End Function
    Public Function SaveMOCApproval(ByVal mocnumber As String, ByVal username As String, ByVal type As String, ByVal Roles As String, ByVal Responded As String) As String
        '(ByVal MOCNumber As Integer, ByVal userName As String)
        RI.SharedFunctions.Trace("clsEnterMOC.vb", mocnumber + " SaveMOCApproval ")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Dim ActiveFlag As String = String.Empty

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_MOCNUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = mocnumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_USERNAME"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_APPROVED"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Approved
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_APPROVEDTYPE"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = type
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_COMMENTS"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Comments
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_ROLES"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Roles
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_RESPONDED"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Responded
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.newmoc.SaveMOCApproval")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Approval " & mocnumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveMOCApprovalBySeqId(ByVal mocnumber As String, ByVal username As String, ByVal type As String, ByVal Roles As String, ByVal RowSeqID As String) As String
        RI.SharedFunctions.Trace("clsEnterMOC.vb", mocnumber + " SaveMOCApprovalBySeqId ")
        '(ByVal MOCNumber As Integer, ByVal userName As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Dim ActiveFlag As String = String.Empty

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_MOCNUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = mocnumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_ROWSEQID"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = RowSeqID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_APPROVED"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Approved
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_APPROVEDTYPE"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = type
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_COMMENTS"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Comments
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_USERNAME"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.newmoc.SaveMOCApprovalBySeqID")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Approval " & mocnumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveMOC() As String
        RI.SharedFunctions.Trace("clsEnterMOC.vb", " SaveMOC ")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        'Dim ds As System.Data.DataSet  = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters
        Try
            '       in_SiteID  IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_SiteID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.SiteID
            paramCollection.Add(param)
            '      in_StartDate  IN varchar2,
            param = New OracleParameter
            Dim startDate As String = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Me.StartDate, "EN-US", "G")
            param.ParameterName = "in_StartDate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = startDate
            'FormatDateTime(CDate(Me.StartDate), DateFormat.ShortDate) & " " & FormatDateTime(CDate(Me.StartDate), DateFormat.ShortTime)
            paramCollection.Add(param)
            param = New OracleParameter
            Dim endDate As String = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Me.EndDate, "EN-US", "G")
            param.ParameterName = "in_EndDate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = endDate
            'FormatDateTime(CDate(Me.EndDate), DateFormat.ShortDate) & " " & FormatDateTime(CDate(Me.EndDate), DateFormat.ShortTime)
            paramCollection.Add(param)
            '      in_BusinessUnit IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_BusinessUnit"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.BusinessUnit
            paramCollection.Add(param)
            '      in_Area IN varchar2,
            'param = New OracleParameter
            'param.ParameterName = "in_Area"
            'param.OracleDbType = OracleDbType.VarChar
            'param.Direction = Data.ParameterDirection.Input
            'param.Value = Me.Area
            'paramCollection.Add(param)
            '      in_Line IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Line"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Line
            paramCollection.Add(param)
            '      in_Title IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Title"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Title
            paramCollection.Add(param)

            '      in_Description IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Description"
            param.OracleDbType = OracleDbType.NClob
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Description
            paramCollection.Add(param)
            '      in_MOCType IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_MOCType"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.MOCType
            paramCollection.Add(param)
            '      in_Impact IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Impact"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Impact
            paramCollection.Add(param)
            '      in_Savings IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Savings"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = CLng(Me.Savings) 'FormatNumber(Me.Savings, 0)
            paramCollection.Add(param)
            '      in_UserName IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_UserName"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.UserName
            paramCollection.Add(param)
            '      in_MOCNumber IN number,
            param = New OracleParameter
            param.ParameterName = "in_MOCNumber"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            If Me.MOCNumber.Length = 0 Then
                param.Value = DBNull.Value
            Else
                param.Value = MOCNumber
            End If
            paramCollection.Add(param)

            '      in_Category IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Category"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Category
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_SubCategory"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.SubCategory
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_EquipSubCategory"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.EquipSubCategory
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_MarketChannelSubCategory"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.MarketChannelSubCategory
            paramCollection.Add(param)

            '      in_Classification IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Classification"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Classification
            paramCollection.Add(param)

            '      in_EquipId IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_EquipID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.FunctionalLocation
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Comment"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.MOCComment
            paramCollection.Add(param)

            '      in_Costs IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Costs"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = CLng(Me.Costs) ' FormatNumber(Me.Costs, 0)
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_status"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Status
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Owner"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Owner
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_WorkOrder"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.WorkOrder
            paramCollection.Add(param)

            '      out_MOCNumber OUT number,
            param = New OracleParameter
            param.ParameterName = "out_MOCNumber"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)
            '      out_status OUT number
            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.newmoc.UPDATEMOC")
            If returnStatus <> "0" And returnStatus <> "777" And returnStatus <> "" Then
                Throw New Data.DataException("Error Saving " & Me.MOCNumber)
            Else
                Me.MOCNumber = CStr(paramCollection.Item("out_MOCNumber").Value)
                RI.SharedFunctions.Trace("clsEnterMOC.vb", Me.MOCNumber + " SaveMOC ")
            End If
            Return returnStatus

        Catch ex As Exception
            Throw
        End Try
        Return MOCNumber
    End Function

    Private Sub GetMOC()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        RI.SharedFunctions.Trace("clsEnterMOC.vb", MOCNumber + " GetMOC ")

        'Check input paramaters

        Try
            If Not IsNumeric(MOCNumber) Then Throw New Data.DataException("Invalid MOC Number was specified - " & MOCNumber)

            param = New OracleParameter
            param.ParameterName = "in_MOCnumber"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = MOCNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOC"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCCategory"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCSubCategory"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsMOCClassification"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsMOCApprovalsL1"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsMOCApprovalsL2"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsMOCApprovalsL3"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsMOCInformed"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCSystem"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCEquipSubCategory"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCMarketChannelCategory"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsMOCComments"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCPendingTemplateTasks"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCTasks"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "NewMOC.GetMOCRecord_" & MOCNumber
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.newmoc.GETMOC", key, 0)
            'RI.SharedFunctions.InsertAuditRecord("clscurrentmoc.getmoc", "retrieved recordsets " & ds.Tables.Count)
            Dim dr As Data.DataTableReader = Nothing
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 7 Then
                    dr = ds.Tables(0).CreateDataReader
                    Dim dr2 As Data.DataTableReader = ds.Tables(1).CreateDataReader
                    Dim selectedCategory As String = String.Empty
                    Dim sb As New StringBuilder
                    If dr2 IsNot Nothing Then
                        If dr2.HasRows Then
                            Do While dr2.Read()
                                If sb.Length >= 0 Then sb.Append(",")
                                sb.Append(dr2.Item("moccategory"))
                            Loop
                            Me.Category = DataClean(sb.ToString)
                        End If
                    End If
                    Dim dr3 As Data.DataTableReader = ds.Tables(2).CreateDataReader
                    Dim selectedSubCategory As String = String.Empty
                    Dim sb3 As New StringBuilder
                    If dr3 IsNot Nothing Then
                        If dr3.HasRows Then
                            Do While dr3.Read()
                                If sb3.Length >= 0 Then sb3.Append(",")
                                sb3.Append(dr3.Item("mocsubcategory"))
                            Loop
                            Me.SubCategory = DataClean(sb3.ToString)
                        End If
                    End If

                    'Dim dr4 As Data.DataTableReader = ds.Tables(3).CreateDataReader
                    'Dim selectedClassification As String = String.Empty
                    'Dim sb4 As New StringBuilder

                    'If dr4 IsNot Nothing Then
                    '    If dr4.HasRows Then
                    '        Do While dr4.Read()
                    '            If sb4.Length >= 0 Then sb4.Append(",")
                    '            sb4.Append(dr4.Item("mocclassification"))
                    '        Loop
                    '        Me.Classification = sb4.ToString
                    '    End If
                    'End If

                    'Dim dr5 As Data.DataTableReader = ds.Tables(3).CreateDataReader
                    'If dr5 IsNot Nothing Then
                    '    Me.ApprovalL1DT = dr5
                    'End If

                    'Dim dr6 As Data.DataTableReader = ds.Tables(4).CreateDataReader
                    'If dr6 IsNot Nothing Then
                    '    Me.ApprovalL2DT = dr6
                    'End If

                    'Dim dr7 As Data.DataTableReader = ds.Tables(5).CreateDataReader
                    'If dr7 IsNot Nothing Then
                    '    Me.ApprovalL3DT = dr7
                    'End If

                    'Dim dr10 As Data.DataTableReader = ds.Tables(6).CreateDataReader
                    'If dr10 IsNot Nothing Then
                    '    Me.InformedDT = dr10
                    'End If

                    'System Changes
                    Dim dr11 As Data.DataTableReader = ds.Tables(3).CreateDataReader
                    Dim sb9, sb11, sb12, sb13, sb14, sb15, sb16, sb17, sb18 As New StringBuilder

                    If dr11 IsNot Nothing Then
                        If dr11.HasRows Then
                            Do While dr11.Read()
                                If sb9.Length >= 0 Then sb9.Append(",")
                                sb9.Append(dr11.Item("mocsystem_seq_id"))
                                If sb11.Length >= 0 Then sb11.Append(",")
                                sb11.Append(dr11.Item("roleplantcode"))
                                If sb12.Length >= 0 Then sb12.Append(",")
                                sb12.Append(dr11.Item("priority"))
                                If sb13.Length >= 0 Then sb13.Append(",")
                                sb13.Append(dr11.Item("daysafterapproval").ToString)
                                If sb14.Length >= 0 Then sb14.Append(",")
                                sb14.Append(dr11.Item("username").ToString)
                                If sb15.Length >= 0 Then sb15.Append(",")
                                sb15.Append(dr11.Item("taskitemseqid").ToString)
                                If sb16.Length >= 0 Then sb16.Append(",")
                                sb16.Append(dr11.Item("duedate").ToString)
                                If sb17.Length >= 0 Then sb17.Append(",")
                                sb17.Append(dr11.Item("taskstatus").ToString)
                                If sb18.Length >= 0 Then sb18.Append(",")
                                sb18.Append(dr11.Item("title").ToString)
                            Loop
                            Me.System = sb9.ToString
                            Me.SystemFacility = sb11.ToString
                            Me.SystemPriority = sb12.ToString
                            Me.SystemDaysAfter = sb13.ToString
                            Me.SystemPerson = sb14.ToString
                            Me.SystemTaskItem = sb15.ToString
                            Me.SystemDueDate = sb16.ToString
                            Me.SystemStatus = sb17.ToString
                            Me.SystemTitle = sb18.ToString
                        End If
                    End If

                    Dim dr12 As Data.DataTableReader = ds.Tables(4).CreateDataReader
                    Dim selectedEquipSubCategory As String = String.Empty
                    Dim sb30 As New StringBuilder
                    If dr12 IsNot Nothing Then
                        If dr12.HasRows Then
                            Do While dr12.Read()
                                'If sb30.Length >= 0 Then sb30.Append(",")
                                sb30.Append(dr12.Item("mocsubcategory"))
                            Loop
                            Me.EquipSubCategory = sb30.ToString

                        End If
                    End If

                    Dim dr13 As Data.DataTableReader = ds.Tables(5).CreateDataReader
                    Dim selectedMarketChannelSubCategory As String = String.Empty
                    Dim sb31 As New StringBuilder
                    If dr13 IsNot Nothing Then
                        If dr13.HasRows Then
                            Do While dr13.Read()
                                'If sb30.Length >= 0 Then sb30.Append(",")
                                sb31.Append(dr13.Item("mocsubcategory"))
                            Loop
                            Me.MarketChannelSubCategory = sb31.ToString

                        End If
                    End If

                    'Dim dr13 As Data.DataTableReader = ds.Tables(5).CreateDataReader
                    'If dr13 IsNot Nothing Then
                    '    Me.MOCCommentsDT = dr13
                    'End If

                    Dim dr14 As Data.DataTableReader = ds.Tables(6).CreateDataReader
                    If dr14 IsNot Nothing Then
                        Me.MOCPendingTempTasksDT = dr14
                    End If

                    Dim dr15 As Data.DataTableReader = ds.Tables(7).CreateDataReader
                    If dr15 IsNot Nothing Then
                        Me.MOCTasksDT = dr15
                    End If

                    'Dim dr16 As Data.DataTableReader = ds.Tables(12).CreateDataReader
                    'If dr16 IsNot Nothing Then
                    '    Me.MOCClassQuestionsDT = dr16
                    'End If

                    'Dim dr17 As Data.DataTableReader = ds.Tables(13).CreateDataReader
                    'If dr17 IsNot Nothing Then
                    '    Me.MOCCatQuestionsDT = dr17
                    'End If

                    'Dim dr12 As Data.DataTableReader = ds.Tables(11).CreateDataReader
                    'If dr12 IsNot Nothing Then
                    '    If dr12.HasRows Then
                    '        Me.DeleteMOCs = True
                    '    End If
                    'End If

                    If dr IsNot Nothing Then
                        If dr.HasRows Then
                            dr.Read()
                            With Me

                                '.MOCCoordinator = DataClean(dr.Item("MOCcoordusername"))

                                'Location
                                .Division = DataClean(dr.Item("Division"))
                                .SiteID = DataClean(dr.Item("Siteid"))

                                '***********Don't like using the itemarray property - need to change
                                If ds.Tables(0).Rows.Count > 1 Then
                                    Dim i As Integer
                                    For i = 0 To ds.Tables(0).Rows.Count - 1
                                        If i = 0 Then
                                            .BusinessUnit = DataClean(ds.Tables(0).Rows.Item(i).ItemArray(12)) & " - " & DataClean(ds.Tables(0).Rows.Item(i).ItemArray(13))
                                        Else
                                            .BusinessUnit = .BusinessUnit & "," & DataClean(ds.Tables(0).Rows.Item(i).ItemArray(12)) & " - " & DataClean(ds.Tables(0).Rows.Item(i).ItemArray(13))
                                        End If    '.BusinessUnit = .BusinessUnit & "," & DataClean(dr.Item("risuperarea")) & "-" & DataClean(dr.Item("Subarea")) & "-" & DataClean(dr.Item("Area"))
                                    Next
                                Else
                                    .BusinessUnit = DataClean(dr.Item("risuperarea")) & " - " & DataClean(dr.Item("Subarea"))
                                End If
                                .Area = DataClean(dr.Item("SubArea"))
                                If DataClean(dr.Item("Linebreak")) = "" Then
                                    .Line = DataClean(dr.Item("Area"))
                                Else
                                    .Line = DataClean(dr.Item("Area")) & " - " & DataClean(dr.Item("Linebreak"))
                                End If

                                'MOC
                                .StartDate = CleanDate(dr.Item("EventDate"), DateFormat.ShortDate)
                                '.StartDate = DataClean(dr.Item("EventDate"))
                                .EndDate = DataClean(dr.Item("IncidentEndDate"))
                                .EndDate = CleanDate(dr.Item("IncidentEndDate"), DateFormat.ShortDate)
                                'If .EndDate.Length = 0 Then .EndDate = .StartDate
                                '.StartDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(.StartDate, "EN-US")
                                '.EndDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(.EndDate, "EN-US")

                                'Don't forget about the hours and minutes
                                .mImpact = DataClean(dr.Item("Impact"))
                                .Impact = DataClean(dr.Item("Impact"))
                                .Savings = (DataClean(dr.Item("Savings"), CStr(0))) 'FormatNumber(dr.Item("Savings"), 0) 'ToString(dr.Item("Savings"))
                                .Costs = (DataClean(dr.Item("Costs"), CStr(0)))
                                .Title = DataClean(dr.Item("Incident"))
                                .Status = DataClean(dr.Item("Status"))
                                .StatusDesc = DataClean(dr.Item("StatusDescription"))
                                .WorkOrder = DataClean(dr.Item("workorder"))
                                .Description = DataClean(dr.Item("Description"))
                                .MOCType = DataClean(dr.Item("MOCType"))
                                'If dr4.HasRows Then
                                .Classification = DataClean(dr.Item("mocclassification"))
                                'End If
                                'Action Item Completion Date
                                .AICompDate = DataClean(dr.Item("ACTIONITEMSCOMPDATE"))
                                .FunctionalLocation = DataClean(dr.Item("EQUIPMENTID"))
                                .MOCCoordinator = DataClean(dr.Item("username"))
                                .MOCCoordinatorEmail = DataClean(dr.Item("email"))
                                .MOCCoordinatorName = DataClean(dr.Item("moccoordinatorname"))
                                .TaskCount = DataClean(dr.Item("TaskCount"))
                                .AttachmentCount = DataClean(dr.Item("AttachmentCount"))
                                'Creation                                
                                .UserName = DataClean(dr.Item("username"))
                                .InsertUsername = GetPersonName(DataClean(dr.Item("username")))
                                .InsertDate = CleanDate(dr.Item("recorddate"), DateFormat.ShortDate)
                                .LastUpdatedBy = GetPersonName(DataClean(dr.Item("updateusername")))
                                .LastUpdatedDate = CleanDate(dr.Item("updatedate"), DateFormat.ShortDate)
                                .USDTicket = dr.Item("USDTicketGenerated").ToString
                                .MTTTaskHeaderSeqId = dr.Item("mtttaskheaderseqid").ToString
                                .Owner = dr.Item("ownerusername").ToString
                                .OwnerName = dr.Item("ownername").ToString
                                .OwnerEmail = dr.Item("owneremail").ToString
                                .CompDate = CleanDate(dr.Item("CompletionDate"), DateFormat.ShortDate)

                            End With
                            Me.GetMOCComments()
                            Me.GetMOCApprovals()
                            Me.GetApprovalsList()
                            Me.GetMOCDefaultQuestions()
                        End If
                    End If

                End If
            End If

        Catch ex As Exception
            Throw New Data.DataException("Error Getting MOC Data")
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Sub
    Public Function SaveMOCSystem(ByVal mocnumber As String, ByVal systemseqid As String, ByVal roleseqid As String, ByVal siteid As String, ByVal username As String, ByVal priority As String, ByVal daysafter As String, ByVal taskTitle As String) As String
        RI.SharedFunctions.Trace("clsEnterMOC.vb", mocnumber + " SaveMOCSystem ")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_MOCNUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = mocnumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_SYSTEMSEQ"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = systemseqid
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_RESPONSIBLE"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_SITEID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = siteid
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_ROLE"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = roleseqid
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_PRIORITY"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = priority
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_DAYSAFTER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = daysafter
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_USERNAME"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.UserName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_tasktitle"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = taskTitle
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.newmoc.SaveMOCSystem")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving System Record " & mocnumber)
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteMOCSystem(ByVal mocnumber As String, ByVal systemseqid As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_MOCNUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = mocnumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_SYSTEMSEQ"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = systemseqid
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.newmoc.DeleteMOCSystem")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Deleting System Record " & mocnumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CreateSystemTasks() As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_MOCNUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = MOCNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.newmoc.CreateMTTSystemTasks")
            'CheckTemplateTasks()
            Return returnStatus

            'If CDbl(returnStatus) <> 0 Then
            'Throw New Data.DataException("Error Saving System Tasks " & MOCNumber)
            'End If
        Catch ex As Exception
            Throw New Data.DataException("Error Saving System Tasks " & MOCNumber)
        End Try
    End Function
    Public Sub CheckTemplateTasks()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As Data.DataTableReader = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_Mocnumber"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = MOCNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCTasks"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "GetIndTasks" & MOCNumber

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.newmoc.GetMOCTasks", key, 0)

            If ds IsNot Nothing Then
                dr = ds.Tables(0).CreateDataReader
            End If

            Dim strTaskId, strTaskHeaderId As String

            If dr IsNot Nothing Then
                If dr.HasRows Then
                    Do While dr.Read
                        strTaskId = dr.Item("taskitemseqid").ToString
                        strTaskHeaderId = dr.Item("mtttaskheaderseqid").ToString
                        GetAndSendImmediateEmail(strTaskId, strTaskHeaderId)
                    Loop
                End If
            End If

        Catch ex As Exception
            Throw
            dr = Nothing
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Sub
    Public Function SaveMOCClassQuestionBySeqId(ByVal questionseqid As String, ByVal username As String, ByVal type As String, ByVal answer As String) As String
        RI.SharedFunctions.Trace("clsEnterMOC.vb", " SaveMOCClassQuestionBySeqId ")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_MOCNUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = mocnumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_QUESTIONSEQID"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = questionseqid
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_ANSWER"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = answer
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_USERNAME"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.newmoc.SaveMOCClassQuestion")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Class Questions " & MOCNumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveMOCCatQuestionBySeqId(ByVal questionseqid As String, ByVal username As String, ByVal type As String, ByVal answer As String) As String
        RI.SharedFunctions.Trace("clsEnterMOC.vb", " SaveMOCCatQuestionBySeqId ")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_MOCNUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = MOCNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_QUESTIONSEQID"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = questionseqid
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_ANSWER"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = answer
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_USERNAME"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.newmoc.SaveMOCCatQuestion")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Category Questions " & MOCNumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetMOCComments() As Data.DataTableReader
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As Data.DataTableReader = Nothing
        'Check input paramaters

        Try
            param = New OracleParameter
            param.ParameterName = "IN_MOCNUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.MOCNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCComments"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "GetMOCComments" & MOCNumber

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.newmoc.GetMOCComments", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count = 1 Then
                    dr = ds.Tables(0).CreateDataReader
                    If dr IsNot Nothing Then
                        Me.MOCCommentsDT = dr
                    End If

                End If
            End If


        Catch ex As Exception
            Throw
            dr = Nothing
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
            GetMOCComments = dr
        End Try
    End Function
    Public Function GetMOCApprovals() As Data.DataTableReader
        RI.SharedFunctions.Trace("clsEnterMOC.vb", " GetMOCApprovals ")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As Data.DataTableReader = Nothing
        Dim dr1 As Data.DataTableReader = Nothing
        Dim dr2 As Data.DataTableReader = Nothing
        Dim dr3 As Data.DataTableReader = Nothing
        'Check input paramaters

        Try
            param = New OracleParameter
            param.ParameterName = "IN_MOCNUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.MOCNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCApprovalsL1"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCApprovalsL2"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCApprovalsL3"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCInformed"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "GetMOCApprovals" & MOCNumber

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.newmoc.GetMOCApprovals", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count = 4 Then
                    dr = ds.Tables(0).CreateDataReader
                    If dr IsNot Nothing Then
                        Me.ApprovalL1DT = dr
                    End If

                    dr1 = ds.Tables(1).CreateDataReader
                    If dr1 IsNot Nothing Then
                        Me.ApprovalL2DT = dr1
                    End If

                    dr2 = ds.Tables(2).CreateDataReader
                    If dr2 IsNot Nothing Then
                        Me.ApprovalL3DT = dr2
                    End If

                    dr3 = ds.Tables(3).CreateDataReader
                    If dr3 IsNot Nothing Then
                        Me.InformedDT = dr3
                    End If

                End If
            End If


        Catch ex As Exception
            Throw
            dr = Nothing
            dr1 = Nothing
            dr2 = Nothing
            dr3 = Nothing
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
            GetMOCApprovals = dr
        End Try
    End Function

    Public Function GetApprovalsList() As Data.DataTableReader
        RI.SharedFunctions.Trace("clsEnterMOC.vb", " GetApprovalsList ")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As Data.DataTableReader = Nothing
        Dim dr1 As Data.DataTableReader = Nothing
        Dim dr2 As Data.DataTableReader = Nothing
        Dim dr3 As Data.DataTableReader = Nothing
        Dim dr4 As Data.DataTableReader = Nothing
        Dim dr5 As Data.DataTableReader = Nothing
        Dim dr6 As Data.DataTableReader = Nothing
        'Check input paramaters

        Try
            param = New OracleParameter
            param.ParameterName = "IN_MOCNUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.MOCNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsInformedList"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsL1List"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsL2List"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsL3List"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsL1Comments"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsL2Comments"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsL3Comments"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "GetApprovalsList" & MOCNumber

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.newmoc.GetCurrentApproverList", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count = 7 Then
                    dr = ds.Tables(0).CreateDataReader
                    dr1 = ds.Tables(1).CreateDataReader
                    dr2 = ds.Tables(2).CreateDataReader
                    dr3 = ds.Tables(3).CreateDataReader
                    dr4 = ds.Tables(4).CreateDataReader
                    dr5 = ds.Tables(5).CreateDataReader
                    dr6 = ds.Tables(6).CreateDataReader
                End If
            End If

            Dim L1List As New StringBuilder
            Dim L2List As New StringBuilder
            Dim L3List As New StringBuilder
            Dim EList As New StringBuilder
            Dim L1Name As New StringBuilder
            Dim L2Name As New StringBuilder
            Dim L3Name As New StringBuilder
            Dim EName As New StringBuilder
            Dim L1Comments As New StringBuilder
            Dim L2Comments As New StringBuilder
            Dim L3Comments As New StringBuilder
            'Dim NotificationLeader As String = String.Empty

            ' Add comments to approval levels.  Don't have to worry about roles for E approver types because
            ' roles assigned to Informed are broken out into individual approver records.
            If dr IsNot Nothing Then
                If dr.HasRows Then
                    Do While dr.Read
                        If EList.Length > 0 Then EList.Append(",")
                        If EName.Length > 0 Then EName.Append(", ")
                        EList.Append(DataClean(dr.Item("Email")))
                        EName.Append(DataClean(dr.Item("FirstName")) & " " & DataClean(dr.Item("LastName")))
                        If dr.Item("Comments").ToString <> "" Then
                            EName = EName.Append(" - " & dr.Item("Comments").ToString)
                        End If
                    Loop
                    Me.NotificationEList = Replace(EList.ToString, ",,", ",")
                    Me.NotificationEFullName = Replace(EName.ToString, ",,", ",")
                End If
            End If
            If dr1 IsNot Nothing Then
                If dr1.HasRows Then
                    Do While dr1.Read
                        'If DataClean(dr.Item("approval_type")).ToUpper = "L1" Then
                        If L1List.Length > 0 Then L1List.Append(",")
                        'If L1Name.Length > 0 Then L1Name.Append(",")
                        L1List.Append(DataClean(dr1.Item("Email")))
                    Loop
                    'Me.NotificationL1FullName = Replace(L1Name.ToString, ",,", ",")
                    Me.NotificationL1List = Replace(L1List.ToString, ",,", ",")
                End If
            End If
            If dr2 IsNot Nothing Then
                If dr2.HasRows Then
                    Do While dr2.Read
                        'ElseIf DataClean(dr.Item("approval_type")).ToUpper = "L2" Then
                        If L2List.Length > 0 Then L2List.Append(",")
                        'If L2Name.Length > 0 Then L2Name.Append(",")
                        L2List.Append(DataClean(dr2.Item("Email")))
                        'L2Name.Append(DataClean(dr2.Item("FirstName")) & " " & DataClean(dr2.Item("LastName")))
                    Loop
                    Me.NotificationL2List = Replace(L2List.ToString, ",,", ",")
                    'Me.NotificationL2FullName = Replace(L2Name.ToString, ",,", ",")
                End If
            End If
            If dr3 IsNot Nothing Then
                If dr3.HasRows Then
                    Do While dr3.Read
                        'ElseIf DataClean(dr.Item("approval_type")).ToUpper = "L3" Then
                        If L3List.Length > 0 Then L3List.Append(",")
                        'If L3Name.Length > 0 Then L3Name.Append(",")
                        L3List.Append(DataClean(dr3.Item("Email")))
                        'L3Name.Append(DataClean(dr3.Item("FirstName")) & " " & DataClean(dr3.Item("LastName")))
                    Loop
                    'Me.NotificationLeader = NotificationLeader

                    Me.NotificationL3List = Replace(L3List.ToString, ",,", ",")
                    'Me.NotificationL3FullName = Replace(L3Name.ToString, ",,", ",")
                End If
            End If

            If dr4 IsNot Nothing Then
                If dr4.HasRows Then
                    Do While dr4.Read
                        If L1Name.Length > 0 Then L1Name.Append(", ")
                        L1Name.Append(DataClean(dr4.Item("fullname")))
                        If dr4.Item("Comments").ToString <> "" Then
                            L1Name = L1Name.Append(" - " & dr4.Item("Comments").ToString & "*")
                        End If
                    Loop
                    Me.NotificationL1FullName = Replace(L1Name.ToString, ",,", ",")
                    Me.NotificationL1FullName = Replace(NotificationL1FullName.ToString, "*,", vbCrLf)
                    Me.NotificationL1FullName = Replace(NotificationL1FullName.ToString, "*", vbCrLf)
                End If
            End If

            If dr5 IsNot Nothing Then
                If dr5.HasRows Then
                    Do While dr5.Read
                        If L2Name.Length > 0 Then L2Name.Append(", ")
                        L2Name.Append(DataClean(dr5.Item("fullname")))
                        If dr5.Item("Comments").ToString <> "" Then
                            L2Name = L2Name.Append(" - " & dr5.Item("Comments").ToString & "*")
                        End If
                    Loop
                    Me.NotificationL2FullName = Replace(L2Name.ToString, ",,", ",")
                    Me.NotificationL2FullName = Replace(NotificationL2FullName.ToString, "*,", vbCrLf)
                    Me.NotificationL2FullName = Replace(NotificationL2FullName.ToString, "*", vbCrLf)
                End If
            End If

            If dr6 IsNot Nothing Then
                If dr6.HasRows Then
                    Do While dr6.Read
                        If L3Name.Length > 0 Then L3Name.Append(", ")
                        L3Name.Append(DataClean(dr6.Item("fullname")))
                        If dr6.Item("Comments").ToString <> "" Then
                            L3Name = L3Name.Append(" - " & dr6.Item("Comments").ToString & "*")
                        End If
                    Loop
                    Me.NotificationL3FullName = Replace(L3Name.ToString, ",,", ",")
                    Me.NotificationL3FullName = Replace(NotificationL3FullName.ToString, "*,", vbCrLf)
                    Me.NotificationL3FullName = Replace(NotificationL3FullName.ToString, "*", vbCrLf)
                End If
            End If

        Catch ex As Exception
            Throw
            dr = Nothing
            dr1 = Nothing
            dr2 = Nothing
            dr3 = Nothing
            dr4 = Nothing
            dr5 = Nothing
            dr6 = Nothing
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
            GetApprovalsList = dr
        End Try
    End Function

    Public Function GetMOCDefaultQuestions() As Data.DataTableReader
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As Data.DataTableReader = Nothing
        Dim dr1 As Data.DataTableReader = Nothing
        'Check input paramaters

        Try
            param = New OracleParameter
            param.ParameterName = "IN_MOCNUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.MOCNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCClassQuestions"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCCatQuestions"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

           
            Dim key As String = "GetMOCQuestions" & MOCNumber

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.newmoc.GetMOCQuestions", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count = 2 Then
                    dr = ds.Tables(0).CreateDataReader

                    If dr IsNot Nothing Then
                        Me.MOCClassQuestionsDT = dr
                    End If

                    dr1 = ds.Tables(1).CreateDataReader
                    If dr1 IsNot Nothing Then
                        Me.MOCCatQuestionsDT = dr1
                    End If


                End If
            End If

            

        Catch ex As Exception
            Throw
            dr = Nothing
            dr1 = Nothing
            
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
            GetMOCDefaultQuestions = dr
        End Try
    End Function


    Private Function GetPersonName(ByVal user As String) As String
        RI.SharedFunctions.Trace("clsEnterMOC.vb", " GetPersonName ")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty
        Dim ret As String = String.Empty

        Try
            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = user
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsPersonName"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "GetPersonName_" & user
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.newmoc.GetPersonName", key, 24)

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
                ds.Dispose()
                ds = Nothing
            End If
            GetPersonName = ret
        End Try
    End Function


    'ADDED James Butler
    'Replaced IP GetUserEmail that was using an undocumented "wm_concat" function
    'stopped working in Oracle 12.

    Public Shared Function GetUserEmail(ByVal userList As String) As String
        RI.SharedFunctions.Trace("clsEnterMOC.vb", " GetUserEmail ")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty
        Dim ret As String = String.Empty
        Dim key As String = userList



        Try

            userList = DataCleanEmailList(userList)


            If userList.Substring(0, 1) = "'" Then
                'do nothing
            Else
                userList = "'" & userList

            End If

            If userList.Substring(userList.Length - 1) = "'" Then
                'do nothing
            Else
                userList = userList & "'"

            End If


            ds = RI.SharedFunctions.GetOracleDataSet("SELECT listagg(EMAIL,',')  within group (order by EMAIL) as email FROM REFEMPLOYEE WHERE USERNAME IN (" & userList & ")")

            If ds IsNot Nothing Then
                If ds.Tables.Count = 1 Then
                    Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader
                    If dr IsNot Nothing Then
                        If dr.HasRows Then
                            dr.Read()
                            'Email List
                            ret = DataClean(dr.Item("EMAIL"))
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
            GetUserEmail = ret
        End Try
    End Function


    'Public Shared Function GetUserEmail(ByVal userList As String) As String
    '    Dim paramCollection As New OracleParameterCollection
    '    Dim param As New OracleParameter
    '    Dim ds As System.Data.DataSet = Nothing
    '    Dim ActiveFlag As String = String.Empty
    '    Dim ret As String = String.Empty 

    '    Try
    '        param = New OracleParameter
    '        param.ParameterName = "in_usernameList"
    '        param.OracleDbType = OracleDbType.VarChar
    '        param.Direction = Data.ParameterDirection.Input
    '        param.Value = userList
    '        paramCollection.Add(param)

    '        param = New OracleParameter
    '        param.ParameterName = "rsUserEmailList"
    '        param.OracleDbType = OracleDbType.Cursor
    '        param.Direction = Data.ParameterDirection.Output
    '        paramCollection.Add(param)

    '        Dim key As String = "GetUserEmail"
    '        ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.newmoc.GetUserEmail", key, 0)

    '        If ds IsNot Nothing Then
    '            If ds.Tables.Count = 1 Then
    '                Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader
    '                If dr IsNot Nothing Then
    '                    If dr.HasRows Then
    '                        dr.Read()
    '                        'Email List
    '                        ret = DataClean(dr.Item("EMAIL"))
    '                    End If
    '                End If
    '            End If
    '        End If

    '    Catch ex As Exception
    '        Throw
    '    Finally
    '        If ds IsNot Nothing Then
    '            ds.Dispose()
    '            ds = Nothing
    '        End If
    '        GetUserEmail = ret
    '    End Try
    'End Function
    Public Function GetAuthLevel(ByVal user As String, ByVal site As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ret As String = String.Empty 

        Try
            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = user
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = site
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsAuthLevel"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "GetAuthLevel_" & user
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.newmoc.GetAuthLevel", key, 8)

            If ds IsNot Nothing Then
                If ds.Tables.Count = 1 Then
                    Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader
                    If dr IsNot Nothing Then
                        If dr.HasRows Then
                            dr.Read()
                            With Me
                                ret = DataClean(dr.Item("AuthLevel"))
                            End With
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
            GetAuthLevel = ret
        End Try
    End Function
    Public Sub New(ByVal MOCNum As String)
        If MOCNum IsNot Nothing And MOCNum.Length > 0 Then
            MOCNumber = MOCNum
            GetMOC()
            'GetAuthLevel(Me.UserName)
        End If
    End Sub
    Public Sub New()
        MOCNumber = String.Empty
    End Sub

    Public Shared Function GetAndSendImmediateEmail(ByVal taskItemNumber As String, ByVal TaskHeaderNumber As String) As Boolean
        RI.SharedFunctions.Trace("clsEnterMOC.vb", " GetAndSendImmediateEmail ")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim refSite As String = "&refsite=MOC"

        Dim sbEmailBody As New System.Text.StringBuilder
        Dim strRecType As String = ""
        Dim strDueDate As String = ""
        Dim strEmailAddress As String = ""
        Dim strTitle As String = ""
        Dim strHeaderTitle As String = ""
        Dim strActivity As String = ""
        Dim strDescription As String = ""
        Dim strResponsible As String = ""
        Dim strCreatedBy As String = ""
        Dim strTaskHeaderID As String = ""
        Dim strTaskItemID As String = ""
        Dim strSubject As String = ""
        Dim strMsg As String = ""
        Dim strBody As String = ""
        Dim strFooter As String = ""
        Dim strHeading As String = ""
        Dim strDB As String
        'Dim ipLoc As New IP.Bids.Localization.WebLocalization(rowItem.RESPONSIBLE_DEFAULTLANGUAGE, "RI")

        Try
            param = New OracleParameter
            param.ParameterName = "in_taskid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = taskItemNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsIndTask"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "MTTBATCHEMAILS_" & taskItemNumber
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.MTTBATCHEMAILS.INDTASKLISTING", key, 0)
            If ds IsNot Nothing Then
                If ds.Tables.Count = 1 Then
                    Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader
                    Do While dr.Read

                        'Task List
                        strRecType = DataClean(dr.Item("rectype"))
                        strHeaderTitle = DataClean(dr.Item("TASKHEADERTITLE"))
                        strActivity = dr.Item("ACTIVITYNAME").ToString
                        strEmailAddress = DataClean(dr.Item("responsible_email"))
                        strDueDate = DataClean(dr.Item("item_duedate"))
                        strTitle = DataClean(dr.Item("ITEM_TITLE"))
                        strDescription = dr.Item("ITEM_DESCRIPTION").ToString
                        strCreatedBy = dr.Item("WHOLE_NAME_CREATEDBY_PERSON").ToString
                        strTaskItemID = taskItemNumber.ToString
                        strTaskHeaderID = dr.Item("TASKHEADERSEQID").ToString
                        If dr.Item("WHOLE_NAME_RESPONSIBLE_PERSON").ToString.Trim.Length = 0 Then
                            strResponsible = dr.Item("ROLEDESCRIPTION").ToString & " (" & dr.Item("RESPONSIBLE_ROLE_NAMES").ToString & ")"
                        Else
                            strResponsible = dr.Item("WHOLE_NAME_RESPONSIBLE_PERSON").ToString
                        End If

                        Dim v_td As String() = {"<TD>", "</TD>"}

                        'strDB = "RIDEV"
                        'If HttpContext.Current.Request.UserHostAddress = "127.0.0.1" Then
                        '    strDB = "RIDEV"
                        'ElseIf HttpContext.Current.Request.ServerVariables("SERVER_NAME").ToLower.StartsWith("ridev") Then
                        '    strDB = "RIDEV"
                        'ElseIf HttpContext.Current.Request.ServerVariables("SERVER_NAME").ToLower.StartsWith("ritest") Then
                        '    strDB = "RITEST"
                        'Else
                        '    strDB = "RI"
                        'End If

                        strDB = RI.SharedFunctions.GetServerUrl()

                        sbEmailBody = New System.Text.StringBuilder
                        sbEmailBody.Append("<P><font size =2 face=Arial><B><U>" & strRecType & "</B></U></FONT><BR>")
                        sbEmailBody.Append("<TABLE border=1 cellpadding='2' cellspacing='0' style='border-color:black' width='100%'><font size =2 face=Arial><TR valign=top><B><TD width=10%>Due Date{1}<TD width=20%>Header Info{1}<TD width=20%>Title{1}")
                        sbEmailBody.Append("{0}Description{1}<TD width=10% wrap=hard>Comments{1}{0}Created By{1}")
                        sbEmailBody.Append("</B></TR>")
                        sbEmailBody.Append("<BR><TR valign=top><font size=2>{0}" & strDueDate & "{1}")

                        sbEmailBody.Append("{0}" & strHeaderTitle & " (" & strActivity & "){1}")
                        'sbEmailBody.Append("{0}<A HREF=HTTP://" & strDB & "TaskTracker/TaskDetails.aspx?HeaderNumber=" & strTaskHeaderID & "&TaskNumber=" & strTaskItemID & refSite & ">" & strTitle & "</A>{1}")
                        sbEmailBody.Append("{0}<A HREF=http://gpitasktracker.graphicpkg.com/TaskDetails.aspx?HeaderNumber=" & strTaskHeaderID & "&TaskNumber=" & strTaskItemID & refSite & ">" & strTitle & "</A>{1}")
                        sbEmailBody.Append("{0}" & strDescription & "{1}")
                        'sbEmailBody.Append("{0}" & strComments & "{1}")
                        sbEmailBody.Append("{0}" & "" & "{1}")
                        sbEmailBody.Append("{0}" & strCreatedBy & "{1}")
                        sbEmailBody.Append("</TR></TABLE>")

                        'sbEmailBody = String.Format(sbEmailBody, v_td)
                        strMsg = sbEmailBody.ToString
                        strMsg = String.Format(strMsg, v_td)

                        strSubject = "Manufacturing Task Tracker tasks that were entered that you are responsible for."
                        strHeading = "<HTML><BODY><font size=2 face=Arial><B>Here are the tasks from Manufacturing Task Tracker that were entered today that you are responsible for.  Click Title to view or update (assign to another person, add comments, complete task by entering the closed date).</B>"
                        strFooter = "</HTML></BODY>"

                        strBody = strHeading & "<BR>" & strMsg.ToString & strFooter
                        strBody = RI.SharedFunctions.cleanString(strBody, "<br>")
                        RI.SharedFunctions.SendEmail(strEmailAddress, "manufacturing.task@graphicpkg.com", strSubject, strBody)

                    Loop
                End If
            End If
        Catch
            Return False
            Throw New Data.DataException("Error Sending Immediate EMail")
        Finally
        End Try
        Return True
    End Function
    Public Function SaveMOCUSD(ByVal mocnumber As String, ByVal desc As String) As String
        RI.SharedFunctions.Trace("clsEnterMOC.vb", " SaveMOCUSD ")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_MOCNUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = mocnumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_DESC"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = desc
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.newmoc.SaveMOCUSD")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving MOC USD Info " & mocnumber)
            End If

        Catch ex As Exception
            Throw New Data.DataException("Error Saving MOC USD Info " & ex.ToString)
        End Try
    End Function
    Public Sub GetMOCTemplateTasks()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New OracleParameter
            param.ParameterName = "in_MOCnumber"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = MOCNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsTemplateTasksDaysAfter"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsTemplateTasksDue"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)


            Dim key As String = "NewMOC.GetMOCTemplateTasks_" & MOCNumber
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.newmoc.TemplateTaskList", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 2 Then
                    Dim dr1 As Data.DataTableReader = ds.Tables(0).CreateDataReader
                    If dr1 IsNot Nothing Then
                        Me.MOCTemplateTasksDaysAfterDT = dr1
                    End If

                    Dim dr2 As Data.DataTableReader = ds.Tables(1).CreateDataReader
                    If dr2 IsNot Nothing Then
                        Me.MOCTemplateTasksDueDateDT = dr2
                    End If
                End If
            End If

        Catch ex As Exception
            Throw New Data.DataException("Error Selecting Template Tasks for " & MOCNumber)
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Sub

    Public Function SaveMOCTemplateTasks(ByVal mocnumber As String, ByVal temptaskseqid As String, ByVal RespUsername As String, ByVal RespRole As String, ByVal Facility As String, ByVal Description As String, ByVal DaysAfter As String, ByVal DueDate As String, ByVal username As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_MOCNumber"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = mocnumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_TempTaskSeqID"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = temptaskseqid
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_ResponsibleUsername"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RespUsername
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_ResponsibleRoleSeqID"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = RespRole
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_RespRolePlantCode"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Facility
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Description"
            param.OracleDbType = OracleDbType.NClob
            param.Direction = Data.ParameterDirection.Input
            param.Value = Description
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_DaysAfter"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = DaysAfter
            paramCollection.Add(param)

            Dim strdueDate As String = ""
            If IsNothing(DueDate) Then
                strdueDate = ""
            Else
                strdueDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(DueDate, "EN-US", "d")
                'RI.SharedFunctions.FormatDateTimeToEnglish(CDate(Me.dueDate))
            End If

            param = New OracleParameter
            param.ParameterName = "in_DueDate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strdueDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)


            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.newmoc.SaveMOCTempTasks")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Template Tasks " & mocnumber)
            End If

        Catch ex As Exception
            Throw New Data.DataException("Error Saving Template Tasks " & mocnumber)
        End Try
    End Function
    Public Function CreateMOCTemplateTasks() As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_MOCNUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = MOCNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.newmoc.CreateMTTTasks")
            'CheckTemplateTasks()
            Return returnStatus

            'If CDbl(returnStatus) <> 0 Then
            'Throw New Data.DataException("Error Saving System Tasks " & MOCNumber)
            'End If
        Catch ex As Exception
            Throw New Data.DataException("Error Saving Template Tasks " & MOCNumber)
        End Try
    End Function

    Public Function CopyMOC() As String
        RI.SharedFunctions.Trace("clsEnterMOC.vb", Me.MOCNumber + " CopyMOC ")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_MOCNumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.MOCNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_UserName"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.UserName
            paramCollection.Add(param)

            '      out_MOCNumber OUT number,
            param = New OracleParameter
            param.ParameterName = "out_MOCNumber"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)
            '      out_status OUT number
            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.newmoc.CopyMOC")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving " & Me.MOCNumber)
            Else
                Me.MOCNumber = CStr(paramCollection.Item("out_MOCNumber").Value)
            End If
        Catch ex As Exception
            Throw
        End Try
        Return MOCNumber
    End Function
End Class
Public Class clsCurrentMOCDraftStatus
    Public Function SaveMOCDraftStatus(ByVal mocnumber As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Dim ActiveFlag As String = String.Empty

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_MOCNUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = mocnumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.newmoc.SaveMOCDraftStatus")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Draft Status " & mocnumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

End Class

Public Class clsMOCSecurity
    Public Structure MOCSecurity
        Dim IsAdmin As Boolean
        Dim MarketChannelUser As Boolean
        Dim MarketChannelRole As String
    End Structure

    Private mMOCSecurity As MOCSecurity
    Public ReadOnly Property Security() As MOCSecurity
        Get
            Return mMOCSecurity
        End Get
    End Property
    Private Sub GetData(ByVal userName As String, ByVal siteID As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters

        Try
            param = New OracleParameter
            param.ParameterName = "in_Username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_SiteID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = siteID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsAuthLevel"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "MOC.GetAuthLevel" & userName
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.newMOC.GetAuthLevel", key, 2)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then

                    Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader

                    If dr IsNot Nothing Then
                        dr.Read()
                        If dr.HasRows Then
                            mMOCSecurity.IsAdmin = True
                        Else
                            mMOCSecurity.IsAdmin = False
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

    Public Sub New(ByVal userName As String, ByVal SiteID As String)

        GetData(userName, SiteID)

    End Sub

    Public Sub New(ByVal userName As String)

        MarketChannelSecurity(userName)

    End Sub

    Private Sub MarketChannelSecurity(ByVal userName As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters

        Try
            param = New OracleParameter
            param.ParameterName = "in_Username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsAuthLevel"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "MOC.MOCMarketChannel" & userName
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.newMOC.MOCMarketChannel", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then

                    Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader

                    If dr IsNot Nothing Then
                        dr.Read()
                        If dr.HasRows Then
                            mMOCSecurity.MarketChannelUser = True
                            mMOCSecurity.MarketChannelRole = dr.Item("rolename").ToString
                        Else
                            mMOCSecurity.MarketChannelUser = False
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
End Class