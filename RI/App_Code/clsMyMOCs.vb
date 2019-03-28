Imports Microsoft.VisualBasic
imports devart.data.oracle
Imports System.IO

<Serializable()> _
Public Class clsMyMOCs
    
    Public Property Approver() As String
        Get
            Return mApprover
        End Get
        Set(ByVal value As String)
            mApprover = value
        End Set
    End Property

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

    Private mDrafts As Data.DataTableReader

    Public Property DraftsDT() As Data.DataTableReader
        Get
            Return mDrafts
        End Get
        Set(ByVal value As Data.DataTableReader)
            mDrafts = value
        End Set
    End Property

    Private mOnHold As Data.DataTableReader

    Public Property OnHoldDT() As Data.DataTableReader
        Get
            Return mOnHold
        End Get
        Set(ByVal value As Data.DataTableReader)
            mOnHold = value
        End Set
    End Property

    Private mImplOverride As Data.DataTableReader

    Public Property ImplOverrideDT() As Data.DataTableReader
        Get
            Return mImplOverride
        End Get
        Set(ByVal value As Data.DataTableReader)
            mImplOverride = value
        End Set
    End Property

    Private mCompOverride As Data.DataTableReader

    Public Property CompOverrideDT() As Data.DataTableReader
        Get
            Return mCompOverride
        End Get
        Set(ByVal value As Data.DataTableReader)
            mCompOverride = value
        End Set
    End Property

    Private mPendingOwner As Data.DataTableReader

    Public Property PendingOwnerDT() As Data.DataTableReader
        Get
            Return mPendingOwner
        End Get
        Set(ByVal value As Data.DataTableReader)
            mPendingOwner = value
        End Set
    End Property

    Private mApprovedNotImplem As Data.DataTableReader
    Public Property ApprovedNotImplemDT() As Data.DataTableReader
        Get
            Return mApprovedNotImplem
        End Get
        Set(ByVal value As Data.DataTableReader)
            mApprovedNotImplem = value
        End Set
    End Property
    Public Property SearchData() As System.Data.DataTableReader
        Get
            Return mSearchData
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mSearchData = value
        End Set
    End Property
    Public Function Search() As System.Data.DataTableReader
        'Perform Search 
        GetMOCListing()
        Return SearchData
    End Function
    Private Sub GetMOCListing()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        
        'Check input paramaters

        Try

            param = New OracleParameter
            param.ParameterName = "in_username"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Approver
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCs"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCDrafts"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCOnHold"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCImplOverride"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCCompOverride"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsOwnerPendingMOCs"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsApprovedNotImplementedMOCs"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "MyMOCs_" & Approver
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.MocMyMOcs.GetMYMOCs", key, 0)
            If ds IsNot Nothing Then

                If ds.Tables.Count = 7 Then
                    Me.SearchData = ds.Tables(0).CreateDataReader
                    Dim dr2 As Data.DataTableReader = ds.Tables(1).CreateDataReader
                    If dr2 IsNot Nothing Then
                        Me.DraftsDT = dr2
                    End If
                    Dim dr3 As Data.DataTableReader = ds.Tables(2).CreateDataReader
                    If dr3 IsNot Nothing Then
                        Me.OnHoldDT = dr3
                    End If
                    Dim dr4 As Data.DataTableReader = ds.Tables(3).CreateDataReader
                    If dr4 IsNot Nothing Then
                        Me.ImplOverrideDT = dr4
                    End If
                    Dim dr5 As Data.DataTableReader = ds.Tables(4).CreateDataReader
                    If dr5 IsNot Nothing Then
                        Me.CompOverrideDT = dr5
                    End If
                    Dim dr6 As Data.DataTableReader = ds.Tables(5).CreateDataReader
                    If dr6 IsNot Nothing Then
                        Me.PendingOwnerDT = dr6
                    End If
                    Dim dr7 As Data.DataTableReader = ds.Tables(6).CreateDataReader
                    If dr7 IsNot Nothing Then
                        Me.ApprovedNotImplemDT() = dr7
                    End If
                Else
                    SearchData = Nothing
                    DraftsDT = Nothing
                    OnHoldDT = Nothing
                    ImplOverrideDT = Nothing
                    CompOverrideDT = Nothing
                    PendingOwnerDT = Nothing
                    ApprovedNotImplemDT = Nothing
                End If
            End If

            If Me.SearchData.HasRows = False Then
                HttpRuntime.Cache.Remove(key)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private mSearchData As System.Data.DataTableReader
    Private mApprover As String

End Class
Public Class clsCurrentMOCDetail
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

    Public Property MOCOwnerEmail() As String
        Get
            Return mMOCOwnerEmail
        End Get
        Set(ByVal value As String)
            mMOCOwnerEmail = value.Trim
        End Set
    End Property
    Private mMOCOwnerEmail As String
    Public Property Title() As String
        Get
            Return mTitle
        End Get
        Set(ByVal value As String)
            mTitle = value.Trim
        End Set
    End Property
    Private mTitle As String
    Public Property BusinessArea() As String
        Get
            Return mBusinessArea
        End Get
        Set(ByVal value As String)
            mBusinessArea = value.Trim
        End Set
    End Property
    Private mBusinessArea As String
    Public Property SubCategory() As String
        Get
            Return mSubCategory
        End Get
        Set(ByVal value As String)
            mSubCategory = value.Trim
        End Set
    End Property
    Private mSubCategory As String = String.Empty
    Public Property SiteID() As String
        Get
            Return mSiteID
        End Get
        Set(ByVal value As String)
            mSiteID = value.Trim
        End Set
    End Property
    Private mSiteID As String
    Public Property USDTicket() As String
        Get
            Return mUSDTicket
        End Get
        Set(ByVal value As String)
            mUSDTicket = value
        End Set
    End Property
    Private mUSDTicket As String = String.Empty
    Public Property MOCCoordinatorName() As String
        Get
            Return mMOCCoordinatorName
        End Get
        Set(ByVal value As String)
            mMOCCoordinatorName = value
        End Set
    End Property
    Private mMOCCoordinatorName As String = String.Empty
    Public Property MOCOwnerName() As String
        Get
            Return mMOCOwnerName
        End Get
        Set(ByVal value As String)
            mMOCOwnerName = value
        End Set
    End Property
    Private mMOCOwnerName As String = String.Empty
    Public Sub New(ByVal MOCNumber As String)

        GetMOCDetail(MOCNumber)

    End Sub

    Public Function GetMOCDetail(ByVal inMOCNumber As String) As Data.DataTableReader
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As Data.DataTableReader = Nothing
        Dim dr1 As Data.DataTableReader = Nothing
        Dim dr2 As Data.DataTableReader = Nothing
        Dim dr3 As Data.DataTableReader = Nothing
        Dim dr4 As Data.DataTableReader = Nothing
        'Check input paramaters

        Try
            param = New OracleParameter
            param.ParameterName = "IN_MOCNUMBER"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = inMOCNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsInformedList"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsL1List"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsL2List"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsL3List"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCDetail"
            param.oracledbtype = oracledbtype.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "GetMOCDetail" & inMOCNumber

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.mocmymocs.GetMOCDetail", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 4 Then
                    dr = ds.Tables(0).CreateDataReader
                    dr1 = ds.Tables(1).CreateDataReader
                    dr2 = ds.Tables(2).CreateDataReader
                    dr3 = ds.Tables(3).CreateDataReader
                    dr4 = ds.Tables(4).CreateDataReader
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

            If dr IsNot Nothing Then
                If dr.HasRows Then
                    Do While dr.Read
                        If EList.Length > 0 Then EList.Append(",")
                        If EName.Length > 0 Then EName.Append(",")
                        EList.Append(dr.Item("Email"))
                        EName.Append(dr.Item("FirstName") & " " & dr.Item("LastName"))
                    Loop
                    Me.NotificationEList = Replace(EList.ToString, ",,", ",")
                    Me.NotificationEFullName = Replace(EName.ToString, ",,", ",")
                End If
            End If
            If dr1 IsNot Nothing Then
                If dr1.HasRows Then
                    Do While dr1.Read
                        If L1List.Length > 0 Then L1List.Append(",")
                        If L1Name.Length > 0 Then L1Name.Append(",")
                        L1List.Append(dr1.Item("Email"))
                        L1Name.Append(dr1.Item("FirstName") & " " & dr1.Item("LastName"))
                    Loop
                    Me.NotificationL1FullName = Replace(L1Name.ToString, ",,", ",")
                    Me.NotificationL1List = Replace(L1List.ToString, ",,", ",")
                End If
            End If
            If dr2 IsNot Nothing Then
                If dr2.HasRows Then
                    Do While dr2.Read
                        If L2List.Length > 0 Then L2List.Append(",")
                        If L2Name.Length > 0 Then L2Name.Append(",")
                        L2List.Append(dr2.Item("Email"))
                        L2Name.Append(dr2.Item("FirstName") & " " & dr2.Item("LastName"))
                    Loop
                    Me.NotificationL2List = Replace(L2List.ToString, ",,", ",")
                    Me.NotificationL2FullName = Replace(L2Name.ToString, ",,", ",")
                End If
            End If
            If dr3 IsNot Nothing Then
                If dr3.HasRows Then
                    Do While dr3.Read
                        If L3List.Length > 0 Then L3List.Append(",")
                        If L3Name.Length > 0 Then L3Name.Append(",")
                        L3List.Append(dr3.Item("Email"))
                        L3Name.Append(dr3.Item("FirstName") & " " & dr3.Item("LastName"))
                    Loop
                    Me.NotificationL3List = Replace(L3List.ToString, ",,", ",")
                    Me.NotificationL3FullName = Replace(L3Name.ToString, ",,", ",")
                End If
            End If
            If dr4 IsNot Nothing Then
                If dr4.HasRows Then
                    dr4.Read()
                    Me.Title = dr4.Item("Title")
                    Me.BusinessArea = dr4.Item("BusinessArea")
                    
                    Me.SubCategory = dr4.Item("SubCategory").ToString
                    Me.USDTicket = dr4.Item("usdticketgenerated").ToString
                    Me.MOCCoordinatorName = dr4.Item("MOCCoordinatorName").ToString
                    Me.SiteID = dr4.Item("SiteID").ToString
                    Me.MOCCoordinatorEmail = dr4.Item("initiatoremail").ToString
                    Me.MOCOwnerEmail = dr4.Item("owneremail").ToString
                    Me.MOCOwnerName = dr4.Item("mocownername").ToString
                End If
            End If


        Catch ex As Exception
            Throw New Data.DataException("Error getting detail for " & inMOCNumber)
            dr = Nothing
        Finally
            If ds IsNot Nothing Then
                ds = Nothing
            End If
            GetMOCDetail = dr
        End Try
    End Function

    
    Public Function CreateSystemTasks(ByVal inMOCNumber As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_MOCNUMBER"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = inMOCNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.newmoc.CreateMTTTasks")
            Return returnStatus

        Catch ex As Exception
            Throw New Data.DataException("Error Saving System Tasks " & inMOCNumber)
        End Try
    End Function

    Public Function SaveMOCUSD(ByVal mocnumber As String, ByVal desc As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_MOCNUMBER"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = mocnumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_DESC"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = desc
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.newmoc.SaveMOCUSD")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving MOC USD Info " & mocnumber)
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveMOCApproval(ByVal approvalseqid As String, ByVal mocnumber As String, ByVal username As String, ByVal strApproved As String, ByVal strApprovedType As String, ByVal strComments As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

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
            param.Value = approvalseqid
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_APPROVED"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strApproved
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_APPROVEDTYPE"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strApprovedType
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "IN_COMMENTS"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strComments
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

            'Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.mocmymocs.SaveMOCApproval")
            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.newmoc.SaveMOCApprovalBySeqID")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Approval " & mocnumber)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

End Class

