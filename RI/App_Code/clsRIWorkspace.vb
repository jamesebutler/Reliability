Option Explicit On
Option Strict On

Imports Microsoft.VisualBasic
Imports Devart.Data.Oracle
Imports RI.SharedFunctions
<Serializable()> _
Public Class clsRIWorkspace
    Public Property EventDesc() As String
        Get
            Return mEventDesc
        End Get
        Set(ByVal value As String)
            mEventDesc = value.Trim
        End Set
    End Property
    Private mEventDesc As String

    Public Property EventSEQ() As String
        Get
            Return mEventSEQ
        End Get
        Set(ByVal value As String)
            mEventSEQ = value.Trim
        End Set
    End Property
    Private mEventSEQ As String

    Public Property ModesDT() As Data.DataTableReader
        Get
            Return mModesDT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mModesDT = value
        End Set
    End Property
    Private mModesDT As Data.DataTableReader

    Public Property CausesDT() As Data.DataTableReader
        Get
            Return mCausesDT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mCausesDT = value
        End Set
    End Property
    Private mCausesDT As Data.DataTableReader

    Public Property ReasonsDT() As Data.DataTableReader
        Get
            Return mReasonsDT
        End Get
        Set(ByVal value As Data.DataTableReader)
            mReasonsDT = value
        End Set
    End Property
    Private mReasonsDT As Data.DataTableReader
    Public Property RootCauseDesc() As String
        Get
            Return mRootCauseDesc
        End Get
        Set(ByVal value As String)
            mRootCauseDesc = value.Trim
        End Set
    End Property
    Private mRootCauseDesc As String


    Public Property Username() As String
        Get
            Return mUsername
        End Get
        Set(ByVal value As String)
            mUsername = value.Trim
        End Set
    End Property
    Private mUsername As String

    Private Sub GetRIFailureEvent()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New OracleParameter
            param.ParameterName = "in_rinumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RINumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failureeventseqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIEventSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsWorkspace"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsFailureEvent"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsFailureMode"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsFailureCause"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "rianalysisworkspace.rsWorkspace"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.rianalysisworkspace.RIWorkspace", key, 0)

            Dim dr As Data.DataTableReader = Nothing
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(1).CreateDataReader
                    If dr IsNot Nothing Then
                        If dr.HasRows Then
                            dr.Read()
                            With Me
                                'EventDesc
                                .EventDesc = DataClean(dr.Item("failureeventother"))
                                .EventSEQ = DataClean(dr.Item("failureeventseqid"))
                            End With
                        End If

                        'rsWorkspace                  
                        mRIFailureModes.DataSource = ds.Tables(0).CreateDataReader
                        mRIFailureModes.DataTextField = "rifailureeventseqid"
                        mRIFailureModes.DataValueField = "failureeventdesc"

                        Dim dr1 As Data.DataTableReader = ds.Tables(2).CreateDataReader
                        Dim sb1 As New StringBuilder
                        If dr1 IsNot Nothing Then
                            Me.ModesDT = dr1
                            'If dr1.HasRows Then
                            'Do While dr1.Read()
                            'If sb1.Length >= 0 Then sb1.Append(",")
                            'sb1.Append(dr1.Item("mocclassification"))
                            'Loop
                            'Me.Classification = sb4.ToString
                        End If
                        'End If

                        Dim dr2 As Data.DataTableReader = ds.Tables(3).CreateDataReader
                        If dr2 IsNot Nothing Then
                            Me.CausesDT = dr2
                        End If
                        'Dim dr3 As Data.DataTableReader = ds.Tables(4).CreateDataReader
                        'If dr3 IsNot Nothing Then
                        'Me.ReasonsDT = dr3
                        'End If

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
    
    Public Property RINumber() As String
        Get
            Return mRINumber
        End Get
        Set(ByVal value As String)
            mRINumber = value.Trim
        End Set
    End Property
    Public Property RIEventSeq() As String
        Get
            Return mRIEventSeq
        End Get
        Set(ByVal value As String)
            mRIEventSeq = value.Trim
        End Set
    End Property
    Private mFailureEvents As New clsData
    Public ReadOnly Property FailureEvents() As clsData
        Get
            Return mFailureEvents
        End Get
    End Property
    Private mFailureModes As New clsData
    Public ReadOnly Property FailureModes() As clsData
        Get
            Return mFailureModes
        End Get
    End Property
    Private mFailureCauses As New clsData
    Public ReadOnly Property FailureCauses() As clsData
        Get
            Return mFailureCauses
        End Get
    End Property
    Private mFailureCauseReasons As New clsData
    Public ReadOnly Property FailureCauseReasons() As clsData
        Get
            Return mFailureCauseReasons
        End Get
    End Property
    Private mFailureRootCauses As New clsData
    Public ReadOnly Property FailureRootCauses() As clsData
        Get
            Return mFailureRootCauses
        End Get
    End Property
    Public ReadOnly Property RIFailureEvents() As clsData
        Get
            Return mRIFailureEvents
        End Get
    End Property
    Public ReadOnly Property RIFailureModes() As clsData
        Get
            Return mRIFailureModes
        End Get
    End Property
    Public ReadOnly Property RIModes() As clsData
        Get
            Return mRIModesDT
        End Get
    End Property
    Private mRIModesDT As New clsData
    Private mAuthLevelid As Integer
    Private mRINumber As String
    Private mRIEventSeq As String
    Private mRIFailureEvents As New clsData
    Private mRIFailureModes As New clsData
    
    Public Sub GetFailureEvents()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New OracleParameter
            param.ParameterName = "rsFailureEvent"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "rianalysisworkspace.failureevents"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.rianalysisworkspace.FailureEvent", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    FailureEvents.DataSource = ds.Tables(0).CreateDataReader
                    FailureEvents.DataTextField = "failureeventdesc"
                    FailureEvents.DataValueField = "failureeventseqid"
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
    Public Sub GetRIFailureModes(ByVal inRIFailureEventSeq As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim key As String = String.Empty
        Dim RIFailureEventSeq As String = inRIFailureEventSeq

        Try
            key = "RIFailureModeSelected_" & RIFailureEventSeq

            param = New OracleParameter
            param.ParameterName = "in_rifailureeventseqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureEventSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsFailureModes"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsFailureModesUNique"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.rianalysisworkspace.RIWSFailureModes", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 0 Then
                    RIFailureModes.DataSource = ds.Tables(0).CreateDataReader
                    RIFailureModes.DataTextField = "failuremodedesc"
                    RIFailureModes.DataValueField = "failuremodeseqid"
                End If
            End If

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("GetRIFailureModes", , ex)
        Finally
            If Not ds Is Nothing Then ds = Nothing
        End Try
    End Sub

    Public Sub New(ByVal riNum As String, Optional ByVal RIEventSEQID As String = Nothing)

        If riNum IsNot Nothing And riNum.Length > 0 Then
            RINumber = riNum
            If RIEventSEQID IsNot Nothing Then
                RIEventSeq = RIEventSEQID
            End If
            GetRIFailureEvent()
        Else
            RINumber = String.Empty
        End If
    End Sub
    Public Sub New()
        RINumber = String.Empty
    End Sub

    Public Sub GetFailureModes(ByVal inFailureEventSeq As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim key As String = String.Empty
        Dim FailureEventSeq As String = inFailureEventSeq

        Try
            key = "FailureModeDDL_" & FailureEventSeq

            param = New OracleParameter
            param.ParameterName = "in_failureeventseqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = FailureEventSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsFailureMode"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.rianalysisworkspace.FailureMode", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 0 Then
                    FailureModes.DataSource = ds.Tables(0).CreateDataReader
                    FailureModes.DataTextField = "failuremodedesc"
                    FailureModes.DataValueField = "failuremodeseqid"
                End If
            End If

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("PopulateFailureMode", , ex)
        Finally
            If Not ds Is Nothing Then ds = Nothing
        End Try
    End Sub

    Public Sub GetFailureCauses(ByVal inFailureModeSeq As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim key As String = String.Empty
        Dim FailureModeSeq As String = inFailureModeSeq

        Try
            key = "FailureCauseDDL_" & FailureModeSeq

            param = New OracleParameter
            param.ParameterName = "in_failuremodeseqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = FailureModeSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsFailureCause"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.rianalysisworkspace.FailureCause", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 0 Then
                    FailureCauses.DataSource = ds.Tables(0).CreateDataReader
                    FailureCauses.DataTextField = "failurecausedesc"
                    FailureCauses.DataValueField = "failurecauseseqid"
                End If
            End If

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("GetFailureCauses", , ex)
        Finally
            If Not ds Is Nothing Then ds = Nothing
        End Try
    End Sub

    Public Sub GetFailureCauseReasons(ByVal inFailureCauseSeq As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim key As String = String.Empty
        Dim FailureCauseSeq As String = inFailureCauseSeq

        Try
            key = "FailureCauseReasDDL_" & FailureCauseSeq

            param = New OracleParameter
            param.ParameterName = "in_failurecauseseqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = FailureCauseSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsFailureCauseReason"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.rianalysisworkspace.FailureCauseReason", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 0 Then
                    FailureCauseReasons.DataSource = ds.Tables(0).CreateDataReader
                    FailureCauseReasons.DataTextField = "failurecausereasdesc"
                    FailureCauseReasons.DataValueField = "failurecausereasseqid"
                End If
            End If

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("GetFailureCauseReasons", , ex)
        Finally
            If Not ds Is Nothing Then ds = Nothing
        End Try
    End Sub

    Public Sub GetFailureRootCauses(ByVal inFailureCauseReasSeq As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim key As String = String.Empty
        Dim FailureCauseSeq As String = inFailureCauseReasSeq

        Try
            key = "FailureRootCauseDDL_" & inFailureCauseReasSeq

            param = New OracleParameter
            param.ParameterName = "in_failurecausereasseqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = FailureCauseSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsFailureRootCause"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.rianalysisworkspace.FailureRootCause", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 0 Then
                    FailureRootCauses.DataSource = ds.Tables(0).CreateDataReader
                    FailureRootCauses.DataTextField = "failurerootcausedesc"
                    FailureRootCauses.DataValueField = "failurerootcauseseqid"
                End If
            End If

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("GetFailureRootCauses", , ex)
        Finally
            If Not ds Is Nothing Then ds = Nothing
        End Try
    End Sub

    Public Shared Sub DeleteRIFailureEvent(ByVal RINumber As String, ByVal RIFailureEventSeq As String, ByVal userName As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New OracleParameter
            param.ParameterName = "in_RINumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RINumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_RIFailureEventSeq"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureEventSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.rianalysisworkspace.DeleteRIFailureEvent")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Deleting " & RIFailureEventSeq)
            End If
        Catch ex As Exception
            Throw
        Finally
            If Not ds Is Nothing Then ds = Nothing
        End Try
    End Sub

    Public Shared Sub DeleteRIFailureMode(ByVal RIFailureModeSeq As String, ByVal userName As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New OracleParameter
            param.ParameterName = "in_RIFailureModeSeqID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureModeSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.rianalysisworkspace.DeleteRIFailureMode")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Deleting " & RIFailureModeSeq)
            End If
        Catch ex As Exception
            Throw
        Finally
            If Not ds Is Nothing Then ds = Nothing
        End Try
    End Sub

End Class
<Serializable()> _
Public Class clsRIWorkspaceNew
    Private mRIFailureEvent As System.Data.DataTableReader
    Private mRIFailureModes As System.Data.DataTableReader
    Private mRIFailureModesHeading As System.Data.DataTableReader
    Private mRIFailureModesDesc As System.Data.DataTableReader
    Private mRIFailureCauses As System.Data.DataTableReader
    Private mRIFailureCausesHeading As System.Data.DataTableReader
    Private mRIFailureCausesDesc As System.Data.DataTableReader
    Private mRIFailureCauseReasons As System.Data.DataTableReader
    Private mRIFailureCauseReasonsDesc As System.Data.DataTableReader
    Private mRIFailureReasonHeading As System.Data.DataTableReader
    Private mRIFailureRootCauses As System.Data.DataTableReader
    Private mRIFailureRootCauseUnique As System.Data.DataTableReader
    Private mRIFailureRootCauseChecked As System.Data.DataTableReader
    Private mFailureModes As System.Data.DataTableReader
    Private mFailureCauses As System.Data.DataTableReader
    Private mRINumber As String
    Private mRIFailureEventSeq As String
    Private mRIFailureModeSeq As String
    Private mRIFailureCauseSeq As String
    Private mRIFailureReasonSeq As String
    Private mRIFailureRootCauseSeq As String

    Private mRefFailureEventSeq As String = String.Empty
    Private mRIFailureEventDesc As String
    Private mRefFailureModeSeq As String
    Private mRIFailureModeDesc As String
    Private mRIFailureModeDesc2 As String
    Private mRIFailureModeDesc3 As String
    Private mRIFailureModeDesc4 As String
    Private mRIFailureModeDesc5 As String
    Private mRIFailureModesSelected As String
    Private mRIFailureCauseDesc As String
    Private mRIFailureCauseDesc2 As String
    Private mRIFailureCauseDesc3 As String
    Private mRIFailureCauseDesc4 As String
    Private mRIFailureCauseDesc5 As String
    Private mRIFailureCausesSelected As String
    Private mRIFailureReasonDesc As String
    Private mRIFailureReasonDesc2 As String
    Private mRIFailureReasonDesc3 As String
    Private mRIFailureReasonDesc4 As String
    Private mRIFailureReasonDesc5 As String
    Private mRIFailureReasonsSelected As String
    Private mRIFailureRootCausesSelected As String
    Private mRIFailureRootCauseDesc As String
    Private mUsername As String


    Public Property RINumber() As String
        Get
            Return mRINumber
        End Get
        Set(ByVal value As String)
            mRINumber = value
        End Set
    End Property
    Public Property RIFailureEvent() As System.Data.DataTableReader
        Get
            Return mRIFailureEvent
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mRIFailureEvent = value
        End Set
    End Property

    Public Property RIFailureModes() As System.Data.DataTableReader
        Get
            Return mRIFailureModes
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mRIFailureModes = value
        End Set
    End Property
    Public Property RIFailureRootCauses() As System.Data.DataTableReader
        Get
            Return mRIFailureRootCauses
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mRIFailureRootCauses = value
        End Set
    End Property

    Public Property RIFailureModesHeading() As System.Data.DataTableReader
        Get
            Return mRIFailureModesHeading
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mRIFailureModesHeading = value
        End Set
    End Property
    Public Property RIFailureModesDesc() As System.Data.DataTableReader
        Get
            Return mRIFailureModesDesc
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mRIFailureModesDesc = value
        End Set
    End Property

    Public Property RIFailureCausesHeading() As System.Data.DataTableReader
        Get
            Return mRIFailureCausesHeading
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mRIFailureCausesHeading = value
        End Set
    End Property

    Public Property RIFailureReasonHeading() As System.Data.DataTableReader
        Get
            Return mRIFailureReasonHeading
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mRIFailureReasonHeading = value
        End Set
    End Property
    Public Property RIFailureRootCauseUnique() As System.Data.DataTableReader
        Get
            Return mRIFailureRootCauseUnique
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mRIFailureRootCauseUnique = value
        End Set
    End Property
    Public Property RIFailureRootCauseChecked() As System.Data.DataTableReader
        Get
            Return mRIFailureRootCauseChecked
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mRIFailureRootCauseChecked = value
        End Set
    End Property

    Public Property RIFailureSeq() As String
        Get
            Return mRIFailureEventSeq
        End Get
        Set(ByVal value As String)
            mRIFailureEventSeq = value
        End Set
    End Property
    Public Property RIFailureModeSeq() As String
        Get
            Return mRIFailureModeSeq
        End Get
        Set(ByVal value As String)
            mRIFailureModeSeq = value
        End Set
    End Property
    Public Property RIFailureCauseSeq() As String
        Get
            Return mRIFailureCauseSeq
        End Get
        Set(ByVal value As String)
            mRIFailureCauseSeq = value
        End Set
    End Property
    Public Property RIFailureReasonSeq() As String
        Get
            Return mRIFailureReasonSeq
        End Get
        Set(ByVal value As String)
            mRIFailureReasonSeq = value
        End Set
    End Property
    Public Property RIFailureRootCauseSeq() As String
        Get
            Return mRIFailureRootCauseSeq
        End Get
        Set(ByVal value As String)
            mRIFailureRootCauseSeq = value
        End Set
    End Property
    Public Property RIFailureCauses() As System.Data.DataTableReader
        Get
            Return mRIFailureCauses
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mRIFailureCauses = value
        End Set
    End Property
    Public Property RIFailureCausesDesc() As System.Data.DataTableReader
        Get
            Return mRIFailureCausesDesc
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mRIFailureCausesDesc = value
        End Set
    End Property
    Public Property FailureModes() As System.Data.DataTableReader
        Get
            Return mFailureModes
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mFailureModes = value
        End Set
    End Property
    Public Property FailureCauses() As System.Data.DataTableReader
        Get
            Return mFailureCauses
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mFailureCauses = value
        End Set
    End Property
    Public Property RIFailureCauseReasons() As System.Data.DataTableReader
        Get
            Return mRIFailureCauseReasons
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mRIFailureCauseReasons = value
        End Set
    End Property
    Public Property RIFailureCauseReasonsDesc() As System.Data.DataTableReader
        Get
            Return mRIFailureCauseReasonsDesc
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mRIFailureCauseReasonsDesc = value
        End Set
    End Property

    Public Property RefFailureEventSeq() As String
        Get
            Return mRefFailureEventSeq
        End Get
        Set(ByVal value As String)
            mRefFailureEventSeq = value
        End Set
    End Property

    Public Property RIFailureEventDesc() As String
        Get
            Return mRIFailureEventDesc
        End Get
        Set(ByVal value As String)
            mRIFailureEventDesc = value
        End Set
    End Property

    Public Property RefFailureModeSeq() As String
        Get
            Return mRefFailureModeSeq
        End Get
        Set(ByVal value As String)
            mRefFailureModeSeq = value
        End Set
    End Property


    Public Property RIFailureModesSelected() As String
        Get
            Return mRIFailureModesSelected
        End Get
        Set(ByVal value As String)
            mRIFailureModesSelected = value
        End Set
    End Property

    Public Property RIFailureModeDesc() As String
        Get
            Return mRIFailureModeDesc
        End Get
        Set(ByVal value As String)
            mRIFailureModeDesc = value
        End Set
    End Property

    Public Property RIFailureModeDesc2() As String
        Get
            Return mRIFailureModeDesc2
        End Get
        Set(ByVal value As String)
            mRIFailureModeDesc2 = value
        End Set
    End Property

    Public Property RIFailureModeDesc3() As String
        Get
            Return mRIFailureModeDesc3
        End Get
        Set(ByVal value As String)
            mRIFailureModeDesc3 = value
        End Set
    End Property

    Public Property RIFailureModeDesc4() As String
        Get
            Return mRIFailureModeDesc4
        End Get
        Set(ByVal value As String)
            mRIFailureModeDesc4 = value
        End Set
    End Property

    Public Property RIFailureModeDesc5() As String
        Get
            Return mRIFailureModeDesc5
        End Get
        Set(ByVal value As String)
            mRIFailureModeDesc5 = value
        End Set
    End Property

    Public Property RIFailureCausesSelected() As String
        Get
            Return mRIFailureCausesSelected
        End Get
        Set(ByVal value As String)
            mRIFailureCausesSelected = value
        End Set
    End Property

    Public Property RIFailureCauseDesc() As String
        Get
            Return mRIFailureCauseDesc
        End Get
        Set(ByVal value As String)
            mRIFailureCauseDesc = value
        End Set
    End Property

    Public Property RIFailureCauseDesc2() As String
        Get
            Return mRIFailureCauseDesc2
        End Get
        Set(ByVal value As String)
            mRIFailureCauseDesc2 = value
        End Set
    End Property

    Public Property RIFailureCauseDesc3() As String
        Get
            Return mRIFailureCauseDesc3
        End Get
        Set(ByVal value As String)
            mRIFailureCauseDesc3 = value
        End Set
    End Property

    Public Property RIFailureCauseDesc4() As String
        Get
            Return mRIFailureCauseDesc4
        End Get
        Set(ByVal value As String)
            mRIFailureCauseDesc4 = value
        End Set
    End Property

    Public Property RIFailureCauseDesc5() As String
        Get
            Return mRIFailureCauseDesc5
        End Get
        Set(ByVal value As String)
            mRIFailureCauseDesc5 = value
        End Set
    End Property
    Public Property RIFailureReasonsSelected() As String
        Get
            Return mRIFailureReasonsSelected
        End Get
        Set(ByVal value As String)
            mRIFailureReasonsSelected = value
        End Set
    End Property

    Public Property RIFailureReasonDesc() As String
        Get
            Return mRIFailureReasonDesc
        End Get
        Set(ByVal value As String)
            mRIFailureReasonDesc = value
        End Set
    End Property

    Public Property RIFailureReasonDesc2() As String
        Get
            Return mRIFailureReasonDesc2
        End Get
        Set(ByVal value As String)
            mRIFailureReasonDesc2 = value
        End Set
    End Property

    Public Property RIFailureReasonDesc3() As String
        Get
            Return mRIFailureReasonDesc3
        End Get
        Set(ByVal value As String)
            mRIFailureReasonDesc3 = value
        End Set
    End Property

    Public Property RIFailureReasonDesc4() As String
        Get
            Return mRIFailureReasonDesc4
        End Get
        Set(ByVal value As String)
            mRIFailureReasonDesc4 = value
        End Set
    End Property

    Public Property RIFailureReasonDesc5() As String
        Get
            Return mRIFailureReasonDesc5
        End Get
        Set(ByVal value As String)
            mRIFailureReasonDesc5 = value
        End Set
    End Property
    Public Property RIFailureRootCausesSelected() As String
        Get
            Return mRIFailureRootCausesSelected
        End Get
        Set(ByVal value As String)
            mRIFailureRootCausesSelected = value
        End Set
    End Property
    Public Property RIFailureRootCauseDesc() As String
        Get
            Return mRIFailureRootCauseDesc
        End Get
        Set(ByVal value As String)
            mRIFailureRootCauseDesc = value
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

    Public Function GetRIFailureEventsAll() As System.Data.DataTableReader
        GetRIFailureEvents()
        Return mRIFailureEvent
    End Function
    Public Sub GetRIFailureEvents()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "in_rinumber"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RINumber
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsFailureModes"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)


            Dim key As String = "rianalysisworkspace.faluremode"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.rianalysisworkspace.RIWSDetail", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count = 1 Then
                    Me.RIFailureEvent = ds.Tables(0).CreateDataReader
                    

                Else
                    RIFailureEvent = Nothing
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

    Public Function GetRIFailureModeDetail() As System.Data.DataTableReader
        GetRIFailureModes()
        Return mRIFailureModes
    End Function
    Public Function GetRIFailureModeHeading() As System.Data.DataTableReader
        GetRIFailureModes()
        Return mRIFailureModesHeading
    End Function
    Public Function GetRIFailureModeDesc() As System.Data.DataTableReader
        GetRIFailureModes()
        Return mRIFailureModesDesc
    End Function
    Public Sub GetRIFailureModes()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "in_rifailureeventseqid"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureSeq
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsFailureModes"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)


            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsFailureModesUnique"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsFailureModesDesc"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "rianalysisworkspace.failuremode"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.rianalysisworkspace.RIWSFailureModes", key, 0)

            Dim sbModes As New StringBuilder
            'Dim dr As Data.DataTableReader = Nothing
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader
                    Dim dr1 As Data.DataTableReader = ds.Tables(0).CreateDataReader
                    Me.RIFailureModes = ds.Tables(0).CreateDataReader
                    Me.RIFailureModesHeading = ds.Tables(1).CreateDataReader
                    Me.RIFailureModesDesc = ds.Tables(2).CreateDataReader
                    Dim i As Integer = 0
                    Do While dr.Read()
                        If sbModes.Length >= 0 Then sbModes.Append(",")
                        sbModes.Append(dr.Item("failuremodeseqid"))
                        If dr.Item("failuremodeseqid") Is System.DBNull.Value Then
                            i = i + 1
                            If i = 1 Then
                                Me.RIFailureModeDesc = dr.Item("failuremodedesc").ToString
                            End If
                            If Not IsDBNull(dr.Item("failuremodeseqid").ToString) And i = 2 Then
                                Me.RIFailureModeDesc2 = dr.Item("failuremodedesc").ToString
                            End If
                            If Not IsDBNull(dr.Item("failuremodeseqid").ToString) And i = 3 Then
                                Me.RIFailureModeDesc3 = dr.Item("failuremodedesc").ToString
                            End If
                            If i = 4 Then
                                Me.RIFailureModeDesc4 = dr.Item("failuremodedesc").ToString
                            End If
                            If Not IsDBNull(dr.Item("failuremodeseqid").ToString) And i = 5 Then
                                Me.RIFailureModeDesc5 = dr.Item("failuremodedesc").ToString
                            End If
                        End If
                    Loop
                    Me.RIFailureModesSelected = sbModes.ToString

                    'dr = ds.Tables(0).CreateDataReader
                    'If Me.RIFailureModes IsNot Nothing Then
                    '    If Me.RIFailureModes.HasRows Then
                    '        Me.RIFailureModes.Read()
                    '        With Me
                    '            'Mode Descriptions
                    '            .RIFailureModeDesc = DataClean(Me.RIFailureModes.Item("failuremodedesc"))
                    '            '.RIFailureModeDesc2 = DataClean(Me.RIFailureModes.Item("failuremodedesc2"))
                    '            '.RefFailureEventSeq = DataClean(Me.RIFailureModes.Item("failureeventseqid"))
                    '        End With
                    '    End If
                    'End If
                End If
            End If
            'If ds IsNot Nothing Then
            '    If ds.Tables.Count = 1 Then
            '        Me.RIFailureModes = ds.Tables(0).CreateDataReader
            '    Else
            '        RIFailureModes = Nothing
            '    End If
            'End If

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

    Public Function GetRIFailureCauseDetail() As System.Data.DataTableReader
        GetRIFailureCauses()
        Return mRIFailureCauses
    End Function
    Public Function GetRIFailureCauseDesc() As System.Data.DataTableReader
        GetRIFailureCauses()
        Return RIFailureCausesDesc
    End Function
    Public Sub GetRIFailureCauses()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "in_rifailureeventseqid"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureSeq
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsFailureCauses"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsFailureCauseDesc"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)


            Dim sbCauses As New StringBuilder
            Dim key As String = "rianalysisworkspace.failurecause"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.rianalysisworkspace.RIWSFailureCauses", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader
                    Me.RIFailureCauses = ds.Tables(0).CreateDataReader
                    Me.RIFailureCausesDesc = ds.Tables(1).CreateDataReader
                    Do While dr.Read()
                        If sbCauses.Length >= 0 Then sbCauses.Append(",")
                        sbCauses.Append(dr.Item("failurecauseseqid"))
                    Loop
                Else
                    RIFailureCauses = Nothing
                End If
            End If
            Me.RIFailureCausesSelected = sbCauses.ToString

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
    Public Function GetRIFailureCauseHeading() As System.Data.DataTableReader
        GetRIFailureCausesHeading()
        Return mRIFailureCausesHeading
    End Function
    Public Sub GetRIFailureCausesHeading()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "in_rifailuremodeseqid"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureModeSeq
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsFailureCauses"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)


            Dim key As String = "rianalysisworkspace.failuremode"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.rianalysisworkspace.RIFailureCausesDetail", key, 0)

            Dim sbModes As New StringBuilder
            'Dim dr As Data.DataTableReader = Nothing
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader
                    Me.RIFailureCausesHeading = ds.Tables(0).CreateDataReader
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
    Public Function GetRIFailureCauseReasDetail() As System.Data.DataTableReader
        GetRIFailureCauseReasons()
        Return mRIFailureCauseReasons
    End Function
    Public Function GetRIFailureCauseReasDesc() As System.Data.DataTableReader
        GetRIFailureCauseReasons()
        Return mRIFailureCauseReasonsDesc
    End Function
    Public Sub GetRIFailureCauseReasons()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "in_rifailureeventseqid"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureSeq
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsFailureCauseReasons"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsFailureCauseReasonDesc"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)


            Dim sbReasons As New StringBuilder
            Dim key As String = "rianalysisworkspace.failurecause"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.rianalysisworkspace.RIWSFailureCauseReasons", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader
                    Me.RIFailureCauseReasons = ds.Tables(0).CreateDataReader
                    Me.RIFailureCauseReasonsDesc = ds.Tables(1).CreateDataReader
                    Do While dr.Read()
                        If sbReasons.Length >= 0 Then sbReasons.Append(",")
                        sbReasons.Append(dr.Item("failurecausereasseqid"))
                    Loop
                Else
                    RIFailureCauses = Nothing
                End If
            End If
            Me.RIFailureReasonsSelected = sbReasons.ToString

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
    Public Function GetRIFailureRootCauseDetail() As System.Data.DataTableReader
        GetRIFailureRootCauses()
        Return mRIFailureRootCauses
    End Function
    Public Sub GetRIFailureRootCauses()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "in_rifailureeventseqid"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureSeq
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsFailureRootCauses"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)


            Dim sbReasons As New StringBuilder
            Dim key As String = "rianalysisworkspace.failurecause"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.rianalysisworkspace.RIWSFailureRootCauses", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count = 1 Then
                    Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader
                    Me.RIFailureRootCauses = ds.Tables(0).CreateDataReader
                    Do While dr.Read()
                        If sbReasons.Length >= 0 Then sbReasons.Append(",")
                        sbReasons.Append(dr.Item("failurerootcauseseqid"))
                    Loop
                Else
                    RIFailureRootCauses = Nothing
                End If
            End If
            Me.RIFailureRootCausesSelected = sbReasons.ToString

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
    Public Function GetFailureCauseDetail(ByVal ModeSeqid As String) As System.Data.DataTableReader
        GetFailureCauses(ModeSeqid)
        Return mFailureCauses
    End Function
    Public Sub GetFailureCauses(ByVal ModeSeqid As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "in_failuremodeseqid"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = ModeSeqid
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsFailureCause"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)


            Dim key As String = "rianalysisworkspace.failurecause"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.rianalysisworkspace.FailureCause", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count = 1 Then
                    Me.FailureCauses = ds.Tables(0).CreateDataReader
                Else
                    FailureCauses = Nothing
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
    Public Function GetRIFailureReasonHeading() As System.Data.DataTableReader
        LoadRIFailureReasons()
        Return mRIFailureReasonHeading
    End Function
    Public Function GetRIFailureReasonDetail() As System.Data.DataTableReader
        LoadRIFailureReasons()
        Return mRIFailureCauseReasonsDesc
    End Function
    Public Sub LoadRIFailureReasons()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "in_rifailurecauseseqid"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureCauseSeq
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "in_rifailureeventseqid"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureSeq
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsFailureReasonHeading"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsFailureReasonDetail"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)


            Dim key As String = "rianalysisworkspace.failurereasons"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.rianalysisworkspace.RIFailureReasonDetail", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    Me.RIFailureReasonHeading = ds.Tables(0).CreateDataReader
                    Me.RIFailureCauseReasonsDesc = ds.Tables(0).CreateDataReader
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
    Public Function SaveRIFailureEvent() As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_rinumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RINumber  '138676 'FailureEventSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_seqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RIFailureSeq 'FailureEventSeq ADD WHEN WE update records.
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failureeventseqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RefFailureEventSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failureeventdesc"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RIFailureEventDesc
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_EventSeqID"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.rianalysisworkspace.UpdateAWEvent")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving " & Me.RINumber)
            Else
                Me.RIFailureSeq = CStr(paramCollection.Item("out_EventSeqID").Value)
            End If

        Catch ex As Exception
            Throw
        End Try
        Return RIFailureSeq
    End Function

    Public Function SaveRIFailureMode() As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_seqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RIFailureModeSeq 'FailureEventSeq ADD WHEN WE update records.
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failureeventseqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RIFailureSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failuremodeseqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RefFailureModeSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failuremodedesc"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RIFailureModeDesc
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failuremodedesc2"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RIFailureModeDesc2
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failuremodedesc3"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RIFailureModeDesc3
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failuremodedesc4"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RIFailureModeDesc4
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failuremodedesc5"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RIFailureModeDesc5
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.rianalysisworkspace.UpdateAWModes")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving " & Me.RINumber)
            End If

        Catch ex As Exception
            Throw
        End Try
        Return RIFailureSeq
    End Function

    Public Function SaveRIFailureCause() As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_seqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureCauseSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failureeventseqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failuremodeseqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureModeSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failurecauseseqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureCausesSelected
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failurecausedesc"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureCauseDesc
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failurecausedesc2"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureCauseDesc2
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failurecausedesc3"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureCauseDesc3
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failurecausedesc4"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureCauseDesc4
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failurecausedesc5"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureCauseDesc5
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.rianalysisworkspace.UpdateAWCauses")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving ")
            End If

        Catch ex As Exception
            Throw
        End Try
        Return RIFailureSeq
    End Function
    Public Function SaveRIFailureReason() As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_seqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureReasonSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_rifailurecauseseqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureCauseSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failurecausereasseqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureReasonsSelected
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failurereasondesc"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureReasonDesc
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failurereasondesc2"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureReasonDesc2
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failurereasondesc3"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureReasonDesc3
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failurereasondesc4"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureReasonDesc4
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failurereasondesc5"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureReasonDesc5
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.rianalysisworkspace.UpdateAWCauseReasons")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Failure Reasons ")
            End If

        Catch ex As Exception
            Throw
        End Try
        Return RIFailureSeq
    End Function
    Public Function SaveRIFailureRootCause() As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_seqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureRootCauseSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_rifailurecausereasseqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureReasonSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failurerootcauseseqid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureRootCausesSelected
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_failurerootcause"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureRootCauseDesc
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.rianalysisworkspace.UpdateAWRootCause")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving Failure Root Cause")
            End If

        Catch ex As Exception
            Throw
        End Try
        Return RIFailureSeq
    End Function
    Public Function GetRIFailureRootCauseUnique() As System.Data.DataTableReader
        LoadRIFailureRootCauseUnique()
        Return mRIFailureRootCauseUnique
    End Function
    Public Sub LoadRIFailureRootCauseUnique()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "in_rifailurecausereasseqid"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureReasonSeq
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsFailureRootCause"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "rianalysisworkspace.failurereasons"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.rianalysisworkspace.RIFailureRootCauseDetail", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    Me.RIFailureRootCauseUnique = ds.Tables(0).CreateDataReader
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
    Public Sub New(ByVal RIEventSeq As String)
        If RIEventSeq IsNot Nothing And RIEventSeq.Length > 0 Then
            RIFailureSeq = RIEventSeq
            GetRIEvent()
        Else
            RIFailureSeq = String.Empty
        End If
    End Sub
    Public Sub GetRIEvent()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "in_failureeventseqid"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RIFailureSeq
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsEvent"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsModes"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsCauses"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)


            Dim key As String = "rianalysisworkspace.RIEventDetail"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.rianalysisworkspace.RIEventDetail", key, 0)

            Dim dr As Data.DataTableReader = Nothing
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    dr = ds.Tables(0).CreateDataReader
                    If dr IsNot Nothing Then
                        If dr.HasRows Then
                            dr.Read()
                            With Me
                                'EventDesc
                                .RIFailureEventDesc = DataClean(dr.Item("failureeventother"))
                                .RefFailureEventSeq = DataClean(dr.Item("failureeventseqid"))
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
            paramCollection = Nothing
        End Try
    End Sub

    Public Function DeleteRIFailureMode() As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_RIFailureModeSeqID"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RIFailureModeSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.rianalysisworkspace.DeleteRIFailureMode")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving " & Me.RINumber)
            End If

        Catch ex As Exception
            Throw
        End Try
        Return RIFailureSeq
    End Function

    Public Function DeleteRIFailureCause() As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_RIFailureCauseSeq"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RIFailureCauseSeq
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.rianalysisworkspace.DeleteRIFailureCause")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving " & Me.RINumber)
            End If

        Catch ex As Exception
            Throw
        End Try
        Return RIFailureSeq
    End Function
    Public Sub New()
        RIFailureSeq = String.Empty
    End Sub
End Class


