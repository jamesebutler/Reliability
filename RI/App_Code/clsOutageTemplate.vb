Option Explicit On
Option Strict On

Imports Devart.Data.Oracle
Imports RI.SharedFunctions
<Serializable()> _
Public Class clsOutageTemplate
    Public Structure OutageSecurity
        Dim DeleteOutages As Boolean
        Dim SaveOutages As Boolean
    End Structure

    'Enum PageMode
    '    Update = 1
    '    NewOutage = 2
    'End Enum

    Private mOutageSecurity As OutageSecurity
    Public ReadOnly Property IncidentSecurity() As OutageSecurity
        Get
            Return mOutageSecurity
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
    Public Property TaskListing() As System.Data.DataTableReader
        Get
            Return mTaskListing
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mTaskListing = value
        End Set
    End Property
    Private mAuthLevelid As Integer
    Private mAuthLevel As String
    Private mOutageTemplate As New clsData
    Private mTemplateTasks As New clsData
    Private mTaskListing As System.Data.DataTableReader
    'Private mCurrentPageMode As PageMode = PageMode.NewOutage
    'Public ReadOnly Property CurrentPageMode() As PageMode
    '    Get
    '        Return mCurrentPageMode
    '    End Get
    'End Property
    Private Sub GetData()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        
        Try
            param = New OracleParameter
            param.ParameterName = "rsOutageTemplate"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "Outage.Templates"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.OutageMaint.Templates", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    'rsOutageTemplate                  
                    mOutageTemplate.DataSource = ds.Tables(0).CreateDataReader
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

    Public Sub GetTaskListing(ByVal strTaskHeader As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim key As String

        Try
            'paramCollection.Clear()

            key = "GetTemplateTasks_" & strTaskHeader

            param = New OracleParameter
            param.ParameterName = "in_TaskHeader"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strTaskHeader
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsTaskItemList"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.GetOutageTemplateTaskItemList", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count = 1 Then
                    Me.TaskListing = ds.Tables(0).CreateDataReader
                Else
                    TaskListing = Nothing
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
    Public Function Search(ByVal strTaskHeader As String) As System.Data.DataTableReader
        'Perform Search 
        GetTaskListing(strTaskHeader)
        Return TaskListing
    End Function

    Public Sub New()
        GetData()
    End Sub

End Class

<Serializable()> _
Public Class clsCurrentTemplateTask
    Public Property Title() As String
        Get
            Return mTitle
        End Get
        Set(ByVal value As String)
            mTitle = value.Trim
        End Set
    End Property
    Private mTitle As String
    Public Property TaskSeqID() As String
        Get
            Return mTaskSeqID
        End Get
        Set(ByVal value As String)
            mTaskSeqID = value.Trim
        End Set
    End Property
    Private mTaskSeqID As String
    Public Property TemplateSeqID() As String
        Get
            Return mTemplateSeqID
        End Get
        Set(ByVal value As String)
            mTemplateSeqID = value.Trim
        End Set
    End Property
    Private mTemplateSeqID As String
    Public Property Description() As String
        Get
            Return mDescription
        End Get
        Set(ByVal value As String)
            mDescription = value.Trim
        End Set
    End Property
    Private mDescription As String
    Public Property primaryRole() As String
        Get
            Return mPrimaryRole
        End Get
        Set(ByVal value As String)
            mPrimaryRole = value.Trim
        End Set
    End Property
    Private mPrimaryRole As String
    Public Property weeksBefore() As String
        Get
            Return mWeeksBefore
        End Get
        Set(ByVal value As String)
            mWeeksBefore = value.Trim
        End Set
    End Property
    Private mWeeksBefore As String
    Public Property weeksAfter() As String
        Get
            Return mWeeksAfter
        End Get
        Set(ByVal value As String)
            mWeeksAfter = value.Trim
        End Set
    End Property
    Private mWeeksAfter As String
    Public Property UserName() As String
        Get
            Return mUserName
        End Get
        Set(ByVal value As String)
            mUserName = value.Trim
        End Set
    End Property
    Private mUserName As String

    Public Property leadTimeDays() As String
        Get
            Return mLeadTimeDays
        End Get
        Set(ByVal value As String)
            mLeadTimeDays = value.Trim
        End Set
    End Property
    Private mLeadTimeDays As String

    Public Property areaRole1() As String
        Get
            Return mAreaRole1
        End Get
        Set(ByVal value As String)
            mAreaRole1 = value
        End Set
    End Property
    Private mAreaRole1 As String
    Public Property areaRole2() As String
        Get
            Return mAreaRole2
        End Get
        Set(ByVal value As String)
            mAreaRole2 = value
        End Set
    End Property
    Private mAreaRole2 As String
    Public Property areaRole3() As String
        Get
            Return mAreaRole3
        End Get
        Set(ByVal value As String)
            mAreaRole3 = value
        End Set
    End Property
    Private mAreaRole3 As String
    Public Property areaRole4() As String
        Get
            Return mAreaRole4
        End Get
        Set(ByVal value As String)
            mAreaRole4 = value
        End Set
    End Property
    Private mAreaRole4 As String
    Public Property areaRole5() As String
        Get
            Return mAreaRole5
        End Get
        Set(ByVal value As String)
            mAreaRole5 = value
        End Set
    End Property
    Private mAreaRole5 As String
    Public Property InsertUsername() As String
        Get
            Return mInsertUsername
        End Get
        Set(ByVal value As String)
            mInsertUsername = value
        End Set
    End Property
    Private mInsertUsername As String
    Public Property InsertDate() As String
        Get
            Return mInsertDate
        End Get
        Set(ByVal value As String)
            mInsertDate = value
        End Set
    End Property
    Private mInsertDate As String

    Public Property LastUpdatedBy() As String
        Get
            Return mLastUpdatedBy
        End Get
        Set(ByVal value As String)
            mLastUpdatedBy = value
        End Set
    End Property
    Private mLastUpdatedBy As String

    Public Property LastUpdatedDate() As String
        Get
            Return mLastUpdatedDate
        End Get
        Set(ByVal value As String)
            mLastUpdatedDate = value
        End Set
    End Property
    Private mLastUpdatedDate As String
    Public Property MTTTemplateID() As String
        Get
            Return mMTTTemplateID
        End Get
        Set(ByVal value As String)
            mMTTTemplateID = value
        End Set
    End Property
    Private mMTTTemplateID As String

    Public Function SaveTemplateTasks() As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ActiveFlag As String = String.Empty
        Dim returnStatus As String
        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_taskitem"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            If Me.TaskSeqID.Length = 0 Then
                param.Value = System.DBNull.Value
            Else
                param.Value = TaskSeqID
            End If
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_taskheader"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = TemplateSeqID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Title"
            param.oracledbtype = oracledbtype.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Title
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_UserName"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.UserName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Description"
            param.oracledbtype = oracledbtype.NClob
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Description
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_weeksAfter"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.weeksAfter
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_weeksBefore"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.weeksBefore
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_leadTimeDays"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.leadTimeDays
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_PrimaryRole"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = primaryRole
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_areaRole1"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = areaRole1
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_areaRole2"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = areaRole2
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_areaRole3"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = areaRole3
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_areaRole4"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = areaRole4
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_areaRole5"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = areaRole5
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_taskitem"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            returnStatus = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.OutageMaint.UpdateTemplateTaskItem")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving " & Me.TemplateSeqID)
            Else
                Me.TaskSeqID = CStr(paramCollection.Item("out_taskitem").Value)
            End If
        Catch ex As Exception
            Throw
        End Try
        Return returnStatus
    End Function
    Public Function SaveTemplateTasks(ByVal outagenumber As String, ByVal templatetask As String, ByVal roletasks As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_TaskSeqID"
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
    Public Function CreateNewOutageMTTTemplate(ByVal username As String, ByVal strtemplateseqid As String, ByVal desc As String) As String
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
            param.ParameterName = "in_TemplateSeqID"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strtemplateseqid
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

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.OutageMaint.CreateNewOutageTemplate")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving New Outage Template " & strtemplateseqid)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Shared Sub DeleteTemplateTask(ByVal TemplateSeqID As Integer, ByVal TaskSeqID As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "IN_TemplateSeqID"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = TemplateSeqID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_TaskSeqID"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = TaskSeqID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.Outage.DeleteOutageResource")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Deleting Template:" & TemplateSeqID & ", Task SEqID:" & TaskSeqID)
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub
    Public Function CreateNewTemplateHeader(ByVal username As String, ByVal desc As String) As String
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
            param.ParameterName = "in_templatedesc"
            param.oracledbtype = oracledbtype.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = desc
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_TemplateSeqID"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.oracledbtype = oracledbtype.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.OutageMaint.CreateNewTemplateHeader")
            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving New Outage Template Header")
            Else
                Me.TemplateSeqID = CStr(paramCollection.Item("out_TemplateSeqID").Value)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Sub New(ByVal OutageNum As String)
    'If OutageNum IsNot Nothing And OutageNum.Length > 0 Then
    'OutageNumber = OutageNum
    ' GetOutage()
    'End If
    'End Sub
    Public Sub New()
        'OutageNumber = String.Empty
    End Sub
End Class
