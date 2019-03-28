Option Explicit On
Option Strict On

'Imports Microsoft.VisualBasic
Imports Devart.Data.Oracle
Imports RI.SharedFunctions

<Serializable()> _
Public Class clsMOCTemplateTask
    Public Property templateSeqID() As String
        Get
            Return mTemplateSeqID
        End Get
        Set(ByVal value As String)
            mTemplateSeqID = value.Trim
        End Set
    End Property
    Private mTemplateSeqID As String

    Public Property taskSeqID() As String
        Get
            Return mTaskSeqID
        End Get
        Set(ByVal value As String)
            mTaskSeqID = value.Trim
        End Set
    End Property
    Private mTaskSeqID As String

    Public Property headerSeqID() As String
        Get
            Return mHeaderSeqID
        End Get
        Set(ByVal value As String)
            mHeaderSeqID = value.Trim
        End Set
    End Property
    Private mHeaderSeqID As String

    Public Property title() As String
        Get
            Return mTitle
        End Get
        Set(ByVal value As String)
            mTitle = value.Trim
        End Set
    End Property
    Private mTitle As String

    Public Property Description() As String
        Get
            Return mDescription
        End Get
        Set(ByVal value As String)
            mDescription = value.Trim
        End Set
    End Property
    Private mDescription As String

    Public Property Priority() As String
        Get
            Return mPriority
        End Get
        Set(ByVal value As String)
            mPriority = value.Trim
        End Set
    End Property
    Private mPriority As String

    Public Property responsibleUsername() As String
        Get
            Return mResponsibleUsername
        End Get
        Set(ByVal value As String)
            mResponsibleUsername = value.Trim
        End Set
    End Property
    Private mResponsibleUsername As String

    Public Property responsibleRole() As String
        Get
            Return mResponsibleRole
        End Get
        Set(ByVal value As String)
            mResponsibleRole = value.Trim
        End Set
    End Property
    Private mResponsibleRole As String

    Public Property responsibleRolePlantCode() As String
        Get
            Return mResponsibleRolePlantCode
        End Get
        Set(ByVal value As String)
            mResponsibleRolePlantCode = value
        End Set
    End Property
    Private mResponsibleRolePlantCode As String

    Public Property daysAfter() As String
        Get
            Return mDaysAfter
        End Get
        Set(ByVal value As String)
            mDaysAfter = value.Trim
        End Set
    End Property
    Private mDaysAfter As String

    Public Property dueDate() As String
        Get
            Return mDueDate
        End Get
        Set(ByVal value As String)
            mDueDate = value.Trim
        End Set
    End Property
    Private mDueDate As String

    Public Property UserName() As String
        Get
            Return mUserName
        End Get
        Set(ByVal value As String)
            mUserName = value.Trim
        End Set
    End Property
    Private mUserName As String

    Public Property insertUsername() As String
        Get
            Return mInsertUsername
        End Get
        Set(ByVal value As String)
            mInsertUsername = value
        End Set
    End Property
    Private mInsertUsername As String

    Public Property insertDate() As String
        Get
            Return mInsertDate
        End Get
        Set(ByVal value As String)
            mInsertDate = value
        End Set
    End Property
    Private mInsertDate As String

    Public Property updateUsername() As String
        Get
            Return mupdateUsername
        End Get
        Set(ByVal value As String)
            mupdateUsername = value
        End Set
    End Property
    Private mUpdateUsername As String

    Public Property updateDate() As String
        Get
            Return mUpdateDate
        End Get
        Set(ByVal value As String)
            mUpdateDate = value
        End Set
    End Property
    Private mUpdateDate As String

    Public Property required() As String
        Get
            Return mRequired
        End Get
        Set(ByVal value As String)
            mRequired = value
        End Set
    End Property
    Private mRequired As String

    Public Property siteID() As String
        Get
            Return mSiteID
        End Get
        Set(ByVal value As String)
            mSiteID = value
        End Set
    End Property
    Private mSiteID As String

    Public Function SaveTemplateTasks() As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ActiveFlag As String = String.Empty
        Dim returnStatus As String
        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_taskitem"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            If Me.taskSeqID.Length = 0 Then
                param.Value = System.DBNull.Value
            Else
                param.Value = taskSeqID
            End If
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_taskheader"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.templateSeqID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_title"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.title
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_description"
            param.OracleDbType = OracleDbType.NClob
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Description
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_daysafter"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.daysAfter
            paramCollection.Add(param)

            If IsNothing(Me.dueDate) Then
                Dim dueDate As String = ""
            Else
                Dim dueDate As String = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Me.dueDate, "EN-US", "d")
                'RI.SharedFunctions.FormatDateTimeToEnglish(CDate(Me.dueDate))
            End If
            param = New OracleParameter
            param.ParameterName = "in_duedate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = dueDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_priority"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Priority
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_respusername"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.responsibleUsername
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_RespRole"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.responsibleRole
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Required"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.required
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_RespRolePlantCode"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.responsibleRolePlantCode
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_UserName"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.UserName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            returnStatus = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.moctasktemplatemaint.UpdateTemplateTaskItem")

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving " & Me.headerSeqID)
            End If
        Catch ex As Exception
            Throw
        End Try
        Return returnStatus
    End Function

    'Public Function ReplicateOutageTasks(ByVal templateseqid As Integer, ByVal outagenumber As String) As String
    '    Dim paramCollection As New OracleParameterCollection
    '    Dim param As New OracleParameter
    '    Dim ds As System.Data.DataSet = Nothing

    '    'Check input paramaters
    '    Try
    '        param = New OracleParameter
    '        param.ParameterName = "in_OutageNumber"
    '        param.OracleDbType = OracleDbType.VarChar
    '        param.Direction = Data.ParameterDirection.Input
    '        param.Value = outagenumber
    '        paramCollection.Add(param)

    '        param = New OracleParameter
    '        param.ParameterName = "in_TemplateSeqId"
    '        param.OracleDbType = OracleDbType.Number
    '        param.Direction = Data.ParameterDirection.Input
    '        param.Value = templateseqid
    '        paramCollection.Add(param)

    '        param = New OracleParameter
    '        param.ParameterName = "out_status"
    '        param.OracleDbType = OracleDbType.Number
    '        param.Direction = Data.ParameterDirection.Output
    '        paramCollection.Add(param)

    '        Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.MTTOutageReplication.ReplicateOutageTasks")
    '        Return returnStatus

    '        If CDbl(returnStatus) <> 0 Then
    '            Throw New Data.DataException("Error Saving Outage Template Tasks " & outagenumber)
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function CreateNewMOCTemplate(ByVal strClassSeqid As String, ByVal strClassDesc As String, ByVal strCatSeqid As String, ByVal strCatDesc As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_Username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = UserName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_ClassificationSeqID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strClassSeqid
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_ClassificationDesc"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strClassDesc
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_CategorySeqID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strCatSeqid
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_CategoryDesc"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strCatDesc
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_SiteID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = siteID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_taskheader"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.moctasktemplatemaint.CreateNewTemplateHeader")

            If CDbl(returnStatus) <> 0 Then
                Me.templateSeqID = Nothing
                Throw New Data.DataException("Error Saving New MOC Task Template Header")
            Else
                Me.templateSeqID = CStr(paramCollection.Item("out_taskheader").Value)
            End If
            Return returnStatus

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckTemplate(ByVal strClassSeqid As String, ByVal strCatSeqid As String, ByVal strSiteID As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_ClassificationSeqID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strClassSeqid
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_CategorySeqID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strCatSeqid
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_SiteID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strSiteID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_taskheader"
            param.OracleDbType = OracleDbType.VarChar
            param.Size = 10
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.moctasktemplatemaint.CheckTemplate")

            If CDbl(returnStatus) <> 0 Then
                Me.templateSeqID = Nothing
                Throw New Data.DataException("Error CheckTemplate")
            Else
                Me.templateSeqID = CStr(paramCollection.Item("out_taskheader").Value)
            End If
            Return returnStatus

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
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = TemplateSeqID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_TaskSeqID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = TaskSeqID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.moctasktemplatemaint.DeleteTemplateTask")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Deleting Template:" & TemplateSeqID & ", Task SEqID:" & TaskSeqID)
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Public Sub New()
        'Template = String.Empty
    End Sub
End Class
<Serializable()> _
Public Class clsMOCClassCategory

    Private mClassification As New clsData
    Public ReadOnly Property Classification() As clsData
        Get
            Return mClassification
        End Get
    End Property
    Private mCategory As New clsData
    Public ReadOnly Property Category() As clsData
        Get
            Return mCategory
        End Get
    End Property
    
    Private Sub GetData()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters

        Try
            paramCollection.Clear()
            param = New OracleParameter
            param.ParameterName = "rsClassification"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsCategory"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "moctasktemplatemaint.GetClassCategoryList", "GetMOCClassificationList", 4)

            '            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.ViewMOC.ViewDropdownddl", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 2 Then
                    Classification.DataSource = ds.Tables(0).CreateDataReader
                    Classification.DataTextField = "mocclassification"
                    Classification.DataValueField = "mocclassification_seq_id"

                    Category.DataSource = ds.Tables(1).CreateDataReader
                    Category.DataTextField = "moccategory"
                    Category.DataValueField = "mocsubcategory_seq_id"

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
    Public Sub New()
        'GetData(Facility)
    End Sub
    Public Sub GetDDLData()
        GetData()
    End Sub

End Class
