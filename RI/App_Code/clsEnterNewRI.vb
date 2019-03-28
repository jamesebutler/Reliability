Option Explicit On
Option Strict On


Imports Microsoft.VisualBasic
Imports Devart.Data.Oracle
Imports RI.SharedFunctions
Imports System.Data

<Serializable()> _
Public Class clsEnterNewRI
    Public Structure RISecurity
        Dim DeleteIncidents As Boolean
        Dim SaveIncidents As Boolean
        Dim AssignAnalysisLeader As Boolean
        Dim MarkAnalysisComplete As Boolean
    End Structure

    Enum PageMode
        Update = 1
        NewRI = 2
    End Enum


    Private mIncidentSecurity As RISecurity
    Public ReadOnly Property IncidentSecurity() As RISecurity
        Get
            Return mIncidentSecurity
        End Get
    End Property
    Private mCurrentPageMode As PageMode = PageMode.NewRI
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
            'param.ParameterName = "rsBusinessUnitArea"
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

            'param = New OracleParameter
            'param.ParameterName = "rsFailedMaterial"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsAuthLevel"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

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

            param = New OracleParameter
            param.ParameterName = "rsCapital"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "NewIncident.Dropdownddl_" & facility & "_" & division & "_" & inActiveFlag
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.Dropdownddl", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 5 Then
                    'Facility                    
                    'mFacility.DataSource = ds.Tables(0).CreateDataReader
                    'mFacility.DataTextField = "SiteName"
                    'mFacility.DataValueField = "SiteId"
                    'If ds.Tables(0).Rows.Count > 0 Then
                    '    mCMMSType = ds.Tables(0).Rows(0).Item("CMMSType").ToString
                    'End If
                    ''rsBusinessUnitArea                     
                    'mBusinessUnitArea.DataSource = ds.Tables(1).CreateDataReader
                    'mBusinessUnitArea.DataTextField = "BusArea"
                    'mBusinessUnitArea.DataValueField = "BusArea"

                    ''rsCrew                   
                    'mCrew.DataSource = ds.Tables(0).CreateDataReader
                    'mCrew.DataTextField = "Crew"
                    'mCrew.DataValueField = "Crew"

                    ''rsShift                    
                    'mShift.DataSource = ds.Tables(1).CreateDataReader
                    'mShift.DataTextField = "Shift"
                    'mShift.DataValueField = "Shift"

                    ''rsFailedMaterial                    
                    'mFailedMaterial.DataSource = ds.Tables(2).CreateDataReader
                    'mFailedMaterial.DataTextField = "FailedLocation"
                    'mFailedMaterial.DataValueField = "FailedLocation"

                    'rsAuthLevel()
                    Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader
                    If dr IsNot Nothing Then
                        dr.Read()
                        If dr.HasRows Then
                            mAuthLevel = "YES"
                            mAuthLevelid = CInt(dr.Item("AuthLevelid"))
                            'mIncidentSecurity.DeleteIncidents = True
                            mIncidentSecurity.MarkAnalysisComplete = True
                        Else
                            mAuthLevel = "NO"
                            mAuthLevelid = 1
                            'mIncidentSecurity.DeleteIncidents = False
                            mIncidentSecurity.MarkAnalysisComplete = True
                        End If

                    End If

                    'Physical Causes                        
                    mPhysicalCauses.DataSource = ds.Tables(1).CreateDataReader
                    mPhysicalCauses.DataTextField = "Phys_Cause"
                    mPhysicalCauses.DataValueField = "Phys_Cause"

                    'Latent Causes                    
                    mLatentCauses.DataSource = ds.Tables(2).CreateDataReader
                    mLatentCauses.DataTextField = "Latent_Cause"
                    mLatentCauses.DataValueField = "Latent_Cause"

                    'Human Causes                    
                    mHumanCauses.DataSource = ds.Tables(3).CreateDataReader
                    mHumanCauses.DataTextField = "Human_Cause"
                    mHumanCauses.DataValueField = "Human_Cause"

                    'rsCapital
                    mCapital.DataSource = ds.Tables(4).CreateDataReader
                    mCapital.DataTextField = "capital_desc"
                    mCapital.DataValueField = "capital_seq_id"
                End If
            End If
            'If busArea.Length > 0 Then
            'GetDataForBusArea(facility, inActiveFlag, division, busArea, lineBreak, userName)
            GetNotificationType(facility, userName, busArea, lineBreak)
            GetDeleteAccess(facility, userName)
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
    Private Sub GetDeleteAccess(ByVal facility As String, ByVal userName As String)
        Dim fl As New FunctionalLocationLookup
        Dim ret As String = fl.GetDeleteAccess(facility, userName)
        If ret IsNot Nothing Then
            If IsNumeric(ret) Then
                If CInt(ret) >= 3 Then
                    mIncidentSecurity.DeleteIncidents = True
                Else
                    mIncidentSecurity.DeleteIncidents = False
                End If
            Else
                mIncidentSecurity.DeleteIncidents = False
            End If
        End If
    End Sub
    Private Sub PopulateTrigger(ByVal facility As String, ByVal inActiveFlag As String, Optional ByVal division As String = "", Optional ByVal busArea As String = "", Optional ByVal lineBreak As String = "")

        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty
        Dim BusinessUnit As String = String.Empty
        Dim Area As String = String.Empty

        'Check input paramaters

        Try

            If InStr(lineBreak, "-") > 0 Then
                Dim line As String() = Split(lineBreak, "-")
                lineBreak = line(1)
            End If
            If InStr(busArea, "-") > 0 Then
                Dim tmp As String() = Split(busArea, "-")
                If tmp.Length = 2 Then
                    BusinessUnit = tmp(0).Trim
                    Area = tmp(1).Trim
                End If
            End If
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
            param.ParameterName = "in_busunit"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = BusinessUnit
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_area"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Area
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Linebreak"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = lineBreak
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsTrigger"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "NewIncident.DropDownDDLTrigger" & facility & "_" & division & "_" & inActiveFlag & "_" & busArea & "_" & lineBreak
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.DropDownDDLTrigger", key, 3)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then

                    'rsTrigger                    
                    mTrigger.DataSource = ds.Tables(0).CreateDataReader
                    mTrigger.DataTextField = "TRIGGERDESC"
                    mTrigger.DataValueField = "TRIGGERDESC"

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
    Private Sub GetDataForBusArea(ByVal facility As String, ByVal inActiveFlag As String, Optional ByVal division As String = "", Optional ByVal busArea As String = "", Optional ByVal lineBreak As String = "", Optional ByVal userName As String = "")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters

        Try
            'param = New OracleParameter
            'param.ParameterName = "in_siteid"
            'param.OracleDbType = OracleDbType.VarChar
            'param.Direction = Data.ParameterDirection.Input
            'param.Value = facility
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "in_inactiveflag"
            'param.OracleDbType = OracleDbType.VarChar
            'param.Direction = Data.ParameterDirection.Input
            'param.Value = inActiveFlag
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "in_Division"
            'param.OracleDbType = OracleDbType.VarChar
            'param.Direction = Data.ParameterDirection.Input
            'param.Value = division
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "in_BusArea"
            'param.OracleDbType = OracleDbType.VarChar
            'param.Direction = Data.ParameterDirection.Input
            'param.Value = busArea
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsLineLineBreak"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsAnalysisLead"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "rsNotificationType"
            'param.OracleDbType = OracleDbType.Cursor
            'param.Direction = Data.ParameterDirection.Output
            'paramCollection.Add(param)

            Dim key As String = "NewIncident.DROPDOWNDDLBUSAREA_" & facility & "_" & division & "_" & inActiveFlag & "_" & busArea
            'ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.DROPDOWNDDLBUSAREA", key, 3)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 2 Then

                    'rsLineLineBreak                     
                    mLineBreak.DataSource = ds.Tables(0).CreateDataReader
                    mLineBreak.DataTextField = "LineBreak"
                    mLineBreak.DataValueField = "LineBreak"

                    'rsAnalysisLead                    
                    mAnalysisLeader.DataSource = ds.Tables(1).CreateDataReader
                    mAnalysisLeader.DataTextField = "Leader"
                    mAnalysisLeader.DataValueField = "UserName"

                    ''rsNotificationType
                    'Dim dr As System.Data.DataTableReader = Nothing
                    'dr = ds.Tables(1).CreateDataReader
                    'If dr IsNot Nothing Then
                    '    dr.Read()
                    '    If dr.HasRows Then

                    '    End If
                    'End If
                End If
            End If

            'End If
            'PopulateTrigger(facility, inActiveFlag, division, busArea, LineBreak)

        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Sub

    'Public Sub GetDataForLineBreak(ByVal facility As String, ByVal inActiveFlag As String, Optional ByVal division As String = "", Optional ByVal busArea As String = "", Optional ByVal lineBreak As String = "", Optional ByVal userName As String = "")
    '    Try
    '        GetNotificationType(facility, userName, busArea, lineBreak)
    '        'End If
    '        PopulateTrigger(facility, inActiveFlag, division, busArea, lineBreak)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Private Sub GetNotificationType(ByVal facility As String, ByVal userName As String, Optional ByVal busArea As String = "", Optional ByVal lineBreak As String = "")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty
        Dim businessUnit As String = String.Empty
        Dim Area As String = String.Empty
        Dim Line As String = String.Empty
        Dim businessUnitArea As String()
        Dim LineLineBreak As String()
        'Check input paramaters

        Try
            If busArea.Length > 1 Then
                businessUnitArea = busArea.Split(CChar("-"))
                If businessUnitArea.Length = 2 Then
                    businessUnit = businessUnitArea(0).Trim
                    Area = businessUnitArea(1).Trim
                ElseIf businessUnitArea.Length = 1 Then
                    businessUnit = businessUnitArea(0).Trim
                End If
            End If
            If lineBreak.Length > 1 Then
                LineLineBreak = lineBreak.Split(CChar("-"))
                If LineLineBreak.Length = 2 Then
                    Line = LineLineBreak(0).Trim
                ElseIf LineLineBreak.Length = 1 Then
                    Line = LineLineBreak(0).Trim
                End If
            End If


            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = facility
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_BusUnit"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = businessUnit
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
            param.ParameterName = "rsNotificationType"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "NewIncident.GetNotificationType" & facility & "_" & busArea & "_" & userName
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.GetNotificationType", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    'rsNotificationType
                    Dim dr As System.Data.DataTableReader = Nothing
                    dr = ds.Tables(0).CreateDataReader
                    If dr IsNot Nothing Then
                        dr.Read()
                        If dr.HasRows Then
                            mNotificationType = DataClean(dr.Item("NotifyType"))
                            mIncidentSecurity.AssignAnalysisLeader = True
                        Else
                            mNotificationType = String.Empty
                            mIncidentSecurity.AssignAnalysisLeader = False
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

        End Try
    End Sub

    Public Sub PopulateCauses(Optional ByVal process As String = "")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

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

            Dim key As String = "RINEWINCIDENT.DropDownDDLCauses" '"Causes_" & process
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.DropDownDDLCauses", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 5 Then
                    'rsType
                    mType.DataSource = ds.Tables(0).CreateDataReader
                    mType.DataTextField = "Cause"
                    mType.DataValueField = "Cause"

                    'rsCause
                    mCause.DataSource = ds.Tables(1).CreateDataReader
                    mCause.DataTextField = "Reason"
                    mCause.DataValueField = "Reason"

                    'rsPrevention
                    mPrevention.DataSource = ds.Tables(2).CreateDataReader
                    mPrevention.DataTextField = "Prevention"
                    mPrevention.DataValueField = "Prevention"


                    'rsProcess
                    mEquipmentProcess.DataSource = ds.Tables(3).CreateDataReader
                    mEquipmentProcess.DataTextField = "Process"
                    mEquipmentProcess.DataValueField = "Process"

                    'rsComponent
                    mComponent.DataSource = ds.Tables(4).CreateDataReader
                    mComponent.DataTextField = "Component"
                    mComponent.DataValueField = "Component"


                End If
            End If

        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Sub

    Public ReadOnly Property CMMSType() As String
        Get
            Return mCMMSType
        End Get
    End Property
    Public ReadOnly Property BusinessUnitArea() As clsData
        Get
            Return mBusinessUnitArea
        End Get
    End Property
    Public ReadOnly Property Crew() As clsData
        Get
            Return mCrew
        End Get
    End Property
    Public ReadOnly Property FailedMaterial() As clsData
        Get
            Return mFailedMaterial
        End Get
    End Property
    Public ReadOnly Property AnalysisLeader() As clsData
        Get
            Return mAnalysisLeader
        End Get
    End Property
    Public ReadOnly Property Trigger() As clsData
        Get
            Return mTrigger
        End Get
    End Property
    Public ReadOnly Property LineBreak() As clsData
        Get
            Return mLineBreak
        End Get
    End Property
    Public ReadOnly Property EquipmentProcess() As clsData
        Get
            Return mEquipmentProcess
        End Get
    End Property
    Public ReadOnly Property Component() As clsData
        Get
            Return mComponent
        End Get
    End Property
    Public ReadOnly Property Type() As clsData
        Get
            Return mType
        End Get
    End Property
    Public ReadOnly Property Prevention() As clsData
        Get
            Return mPrevention
        End Get
    End Property
    Public ReadOnly Property Cause() As clsData
        Get
            Return mCause
        End Get
    End Property
    Public ReadOnly Property Facility() As clsData
        Get
            Return mFacility
        End Get
    End Property
    Public ReadOnly Property Shift() As clsData
        Get
            Return mShift
        End Get
    End Property
    Public ReadOnly Property NotificationType() As String
        Get
            Return mNotificationType
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
    Public Property PhysicalCauses() As clsData
        Get
            Return mPhysicalCauses
        End Get
        Set(ByVal value As clsData)
            mPhysicalCauses = value
        End Set
    End Property
    Public Property LatentCauses() As clsData
        Get
            Return mLatentCauses
        End Get
        Set(ByVal value As clsData)
            mLatentCauses = value
        End Set
    End Property
    Public Property HumanCauses() As clsData
        Get
            Return mHumanCauses
        End Get
        Set(ByVal value As clsData)
            mHumanCauses = value
        End Set
    End Property
    Public Property Capital() As clsData
        Get
            Return mCapital
        End Get
        Set(ByVal value As clsData)
            mCapital = value
        End Set
    End Property
    Public Property Verification() As clsData
        Get
            Return mVerification
        End Get
        Set(ByVal value As clsData)
            mVerification = value
        End Set
    End Property
    Public Property VerificationWeeksAfter() As clsData
        Get
            Return mVerificationWeeksAfter
        End Get
        Set(ByVal value As clsData)
            mVerificationWeeksAfter = value
        End Set
    End Property
    Public Property VerificationResp() As clsData
        Get
            Return mVerificationResp
        End Get
        Set(ByVal value As clsData)
            mVerificationResp = value
        End Set
    End Property
    Public Property VerificationDueDate() As clsData
        Get
            Return mVerificationDueDate
        End Get
        Set(ByVal value As clsData)
            mVerificationDueDate = value
        End Set
    End Property
    Public Property VerificationClosedDate() As clsData
        Get
            Return mVerificationClosedDate
        End Get
        Set(ByVal value As clsData)
            mVerificationClosedDate = value
        End Set
    End Property
    Public Property VerificationComment() As clsData
        Get
            Return mVerificationComment
        End Get
        Set(ByVal value As clsData)
            mVerificationComment = value
        End Set
    End Property
    Public Property VerRespName() As clsData
        Get
            Return mVerRespName
        End Get
        Set(ByVal value As clsData)
            mVerRespName = value
        End Set
    End Property
    Public Property VerTaskHeader() As clsData
        Get
            Return mVerTaskHeader
        End Get
        Set(ByVal value As clsData)
            mVerTaskHeader = value
        End Set
    End Property
    Public Property VerTaskItem() As clsData
        Get
            Return mVerTaskItem
        End Get
        Set(ByVal value As clsData)
            mVerTaskItem = value
        End Set
    End Property
    Private mAuthLevelid As Integer
    Private mAuthLevel As String
    Private mFacility As New clsData
    Private mBusinessUnitArea As New clsData
    Private mCause As New clsData
    Private mPrevention As New clsData
    Private mType As New clsData
    Private mComponent As New clsData
    Private mEquipmentProcess As New clsData
    Private mLineBreak As New clsData
    Private mTrigger As New clsData
    Private mAnalysisLeader As New clsData
    Private mFailedMaterial As New clsData
    Private mCrew As New clsData
    Private mShift As New clsData
    Private mCMMSType As String = String.Empty
    Private mPhysicalCauses As New clsData
    Private mLatentCauses As New clsData
    Private mHumanCauses As New clsData
    Private mCapital As New clsData
    Private mVerification As New clsData
    Private mVerificationResp As New clsData
    Private mVerificationWeeksAfter As New clsData
    Private mVerificationDueDate As New clsData
    Private mVerificationClosedDate As New clsData
    Private mVerificationComment As New clsData
    Private mVerRespName As New clsData
    Private mVerTaskHeader As New clsData
    Private mVerTaskItem As New clsData
    Private mNotificationType As String = String.Empty

    Public Sub New(ByVal userName As String, ByVal facility As String, ByVal inActiveFlag As String, Optional ByVal division As String = "", Optional ByVal busArea As String = "", Optional ByVal lineBreak As String = "", Optional ByVal riNumber As String = "")

        'If lineBreak.Length > 0 Then
        '    Me.GetDataForLineBreak(facility, inActiveFlag, division, busArea, lineBreak, userName)
        'ElseIf busArea.Length > 0 Then
        '    Me.GetDataForBusArea(facility, inActiveFlag, division, busArea, lineBreak, userName)
        'Else
        GetData(userName, facility, inActiveFlag, division, busArea, lineBreak)
        'End If       

        If riNumber.Length = 0 Then
            Me.mCurrentPageMode = PageMode.NewRI
        Else
            Me.mCurrentPageMode = PageMode.Update
        End If
    End Sub
    Public Sub New()

    End Sub
End Class

<Serializable()> _
Public Class clsCurrentRI
    Public Property SiteID() As String
        Get
            Return mSiteID
        End Get
        Set(ByVal value As String)
            mSiteID = value.Trim
        End Set
    End Property
    Private mSiteID As String = String.Empty
    Public Property IncidentStartDate() As String
        Get
            Return mEventDate
        End Get
        Set(ByVal value As String)
            mEventDate = value.Trim
        End Set
    End Property
    Private mEventDate As String = String.Empty
    Public Property IncidentEndDate() As String
        Get
            Return mIncidentEndDate
        End Get
        Set(ByVal value As String)
            mIncidentEndDate = value.Trim
        End Set
    End Property
    Private mIncidentEndDate As String = String.Empty
    Public Property BusinessUnit() As String
        Get
            Return mBusinessUnit
        End Get
        Set(ByVal value As String)
            mBusinessUnit = value.Trim
        End Set
    End Property
    Private mBusinessUnit As String = String.Empty
    Public Property Area() As String
        Get
            Return mArea
        End Get
        Set(ByVal value As String)
            mArea = value.Trim
        End Set
    End Property
    Private mArea As String = String.Empty
    Public Property Line() As String
        Get
            Return mLine
        End Get
        Set(ByVal value As String)
            mLine = value.Trim
        End Set
    End Property
    Private mLine As String = String.Empty
    Public Property IncidentTitle() As String
        Get
            Return mIncidentTitle
        End Get
        Set(ByVal value As String)
            mIncidentTitle = value.Trim
        End Set
    End Property
    Private mIncidentTitle As String = String.Empty
    Public Property Type() As String
        Get
            Return mType
        End Get
        Set(ByVal value As String)
            mType = value.Trim
        End Set
    End Property
    Private mType As String = String.Empty
    Public Property Cause() As String
        Get
            Return mCause
        End Get
        Set(ByVal value As String)
            mCause = value.Trim
        End Set
    End Property
    Private mCause As String = String.Empty
    Public Property Process() As String
        Get
            Return mProcess
        End Get
        Set(ByVal value As String)
            mProcess = value.Trim
        End Set
    End Property
    Private mProcess As String = String.Empty
    Public Property Component() As String
        Get
            Return mComponent
        End Get
        Set(ByVal value As String)
            mComponent = value.Trim
        End Set
    End Property
    Private mComponent As String = String.Empty
    Public Property FunctionalLocation() As String
        Get
            Return mFunctionalLocation
        End Get
        Set(ByVal value As String)
            mFunctionalLocation = value.Trim
        End Set
    End Property
    Private mFunctionalLocation As String = String.Empty

    Public Property RTS() As String
        Get
            Return mRTS
        End Get
        Set(ByVal value As String)
            mRTS = value
        End Set
    End Property
    Private mRTS As String = String.Empty
    Public Property PPR() As String
        Get
            Return mPPR
        End Get
        Set(ByVal value As String)
            mPPR = value
        End Set
    End Property
    Private mPPR As String = String.Empty
    Public Property Recordable() As String
        Get
            Return mRecordable
        End Get
        Set(ByVal value As String)
            mRecordable = value.Trim
        End Set
    End Property
    Private mRecordable As String = String.Empty
    Public Property RCFA() As String
        Get
            Return mRCFA
        End Get
        Set(ByVal value As String)
            mRCFA = value.Trim
        End Set
    End Property
    Private mRCFA As String = String.Empty
    Public Property SRR() As String
        Get
            Return mSRR
        End Get
        Set(ByVal value As String)
            mSRR = value.Trim
        End Set
    End Property
    Private mSRR As String = String.Empty
    Public Property RCFALevel() As String
        Get
            Return mRCFALevel
        End Get
        Set(ByVal value As String)
            mRCFALevel = value.Trim
        End Set
    End Property
    Private mRCFALevel As String = String.Empty
    Public Property Prevention() As String
        Get
            Return mPrevention
        End Get
        Set(ByVal value As String)
            mPrevention = value.Trim
        End Set
    End Property
    Private mLossOpportunity As String = String.Empty
    Public Property LossOpportunity() As String
        Get
            Return mLossOpportunity
        End Get
        Set(ByVal value As String)
            mLossOpportunity = value
        End Set
    End Property
    Private mPrevention As String = String.Empty
    Public Property Cost() As String
        Get
            Return mCost
        End Get
        Set(ByVal value As String)
            mCost = value.Trim
        End Set
    End Property
    Private mCost As String = String.Empty
    Public Property TotalCost() As String
        Get
            Return mTotalCost
        End Get
        Set(ByVal value As String)
            mTotalCost = value.Trim
        End Set
    End Property
    Private mRINumber As String = String.Empty
    Public Property RINumber() As String
        Get
            Return mRINumber
        End Get
        Set(ByVal value As String)
            mRINumber = value.Trim
        End Set
    End Property
    Private mTotalCost As String = String.Empty
    Public Property IncidentDescription() As String
        Get
            Return mIncidentDescription
        End Get
        Set(ByVal value As String)
            mIncidentDescription = value.Trim
        End Set
    End Property
    Private mIncidentDescription As String = String.Empty
    Public Property Downtime() As String
        Get
            Return mDowntime
        End Get
        Set(ByVal value As String)
            mDowntime = value.Trim
        End Set
    End Property
    Private mDowntime As String = String.Empty
    Public Property Recorddate() As String
        Get
            Return mRecorddate
        End Get
        Set(ByVal value As String)
            mRecorddate = value.Trim
        End Set
    End Property
    Private mRecorddate As String = String.Empty
    Public Property UserName() As String
        Get
            Return mUserName
        End Get
        Set(ByVal value As String)
            mUserName = value.Trim
        End Set
    End Property
    Private mUserName As String = String.Empty
    Public Property Linebreak() As String
        Get
            Return mLinebreak
        End Get
        Set(ByVal value As String)
            mLinebreak = value.Trim
        End Set
    End Property
    Private mLinebreak As String = String.Empty
    Public Property RCFASafety() As String
        Get
            Return mRCFASafety
        End Get
        Set(ByVal value As String)
            mRCFASafety = value.Trim
        End Set
    End Property
    Private mRCFASafety As String = String.Empty
    Public Property RCFAQuality() As String
        Get
            Return mRCFAQuality
        End Get
        Set(ByVal value As String)
            mRCFAQuality = value.Trim
        End Set
    End Property
    Private mRCFAQuality As String = String.Empty
    Public Property Crew() As String
        Get
            Return mCrew
        End Get
        Set(ByVal value As String)
            mCrew = value.Trim
        End Set
    End Property
    Private mCrew As String = String.Empty
    Public Property Shift() As String
        Get
            Return mShift
        End Get
        Set(ByVal value As String)
            mShift = value.Trim
        End Set
    End Property
    Private mShift As String = String.Empty
    Public Property ClassCost() As String
        Get
            Return mClassCost
        End Get
        Set(ByVal value As String)
            mClassCost = value.Trim
        End Set
    End Property
    Private mClassCost As String = String.Empty
    Public Property ClassLife() As String
        Get
            Return mClassLife
        End Get
        Set(ByVal value As String)
            mClassLife = value.Trim
        End Set
    End Property
    Private mClassLife As String = String.Empty
    Public Property ClassSchedule() As String
        Get
            Return mClassSchedule
        End Get
        Set(ByVal value As String)
            mClassSchedule = value.Trim
        End Set
    End Property
    Private mClassSchedule As String = String.Empty
    Public Property ClassImpact() As String
        Get
            Return mClassImpact
        End Get
        Set(ByVal value As String)
            mClassImpact = value.Trim
        End Set
    End Property
    Private mClassImpact As String = String.Empty
    Public Property ClassChronic() As String
        Get
            Return mClassChronic
        End Get
        Set(ByVal value As String)
            mClassChronic = value.Trim
        End Set
    End Property
    Private mClassChronic As String = String.Empty
    Public Property RCFALeader() As String
        Get
            Return mRCFALeader
        End Get
        Set(ByVal value As String)
            mRCFALeader = value.Trim
        End Set
    End Property
    Private mRCFALeader As String = String.Empty

    Public Property RCFALeaderName() As String
        Get
            Return mRCFALeaderName
        End Get
        Set(ByVal value As String)
            mRCFALeaderName = value
        End Set
    End Property
    Private mRCFALeaderName As String = String.Empty
    Public Property CertifiedKill() As String
        Get
            Return mCertifiedKill
        End Get
        Set(ByVal value As String)
            mCertifiedKill = value.Trim
        End Set
    End Property
    Private mCertifiedKill As String = String.Empty
    Public Property Chronic() As String
        Get
            Return mChronic
        End Get
        Set(ByVal value As String)
            mChronic = value.Trim
        End Set
    End Property
    Private mChronic As String = String.Empty
    Public Property RCFACondition() As String
        Get
            Return mRCFACondition
        End Get
        Set(ByVal value As String)
            mRCFACondition = value.Trim
        End Set
    End Property
    Private mRCFACondition As String = String.Empty
    Public Property RCFATeamMembers() As String
        Get
            Return mRCFATeamMembers
        End Get
        Set(ByVal value As String)
            mRCFATeamMembers = value.Trim
        End Set
    End Property
    Private mRCFATeamMembers As String = String.Empty
    Public Property RCFAPeople() As String
        Get
            Return mRCFAPeople
        End Get
        Set(ByVal value As String)
            mRCFAPeople = value.Trim
        End Set
    End Property
    Private mRCFAPeople As String = String.Empty
    Public Property RCFATriggerDesc() As String
        Get
            Return mRCFATriggerDesc
        End Get
        Set(ByVal value As String)
            mRCFATriggerDesc = value.Trim
        End Set
    End Property
    Private mRCFATriggerDesc As String = String.Empty
    Private mPhysicalCauses As String = String.Empty
    Public Property PhysicalCauses() As String
        Get
            Return mPhysicalCauses
        End Get
        Set(ByVal value As String)
            mPhysicalCauses = value
        End Set
    End Property
    Private mHumanCauses As String = String.Empty
    Public Property HumanCauses() As String
        Get
            Return mHumanCauses
        End Get
        Set(ByVal value As String)
            mHumanCauses = value
        End Set
    End Property
    Private mLatentCauses As String = String.Empty
    Public Property LatentCauses() As String
        Get
            Return mLatentCauses
        End Get
        Set(ByVal value As String)
            mLatentCauses = value
        End Set
    End Property
    Private mOtherPhysicalCauses As String = String.Empty
    Public Property OtherPhysicalCauses() As String
        Get
            Return mOtherPhysicalCauses
        End Get
        Set(ByVal value As String)
            mOtherPhysicalCauses = value
        End Set
    End Property
    Private mOtherHumanCauses As String = String.Empty
    Public Property OtherHumanCauses() As String
        Get
            Return mOtherHumanCauses
        End Get
        Set(ByVal value As String)
            mOtherHumanCauses = value
        End Set
    End Property
    Private mOtherLatentCauses As String = String.Empty
    Public Property OtherLatentCauses() As String
        Get
            Return mOtherLatentCauses
        End Get
        Set(ByVal value As String)
            mOtherLatentCauses = value
        End Set
    End Property
    Private mRCFAFailedLocation As String = String.Empty
    Public Property RCFAFailedLocation() As String
        Get
            Return mRCFAFailedLocation
        End Get
        Set(ByVal value As String)
            mRCFAFailedLocation = value.Trim
        End Set
    End Property
    Private mCreatedBy As String = String.Empty
    Public Property CreatedBy() As String
        Get
            Return mCreatedBy
        End Get
        Set(ByVal value As String)
            mCreatedBy = value
        End Set
    End Property

    Private mCreationDate As String = String.Empty
    Public Property CreationDate() As String
        Get
            Return mCreationDate
        End Get
        Set(ByVal value As String)
            mCreationDate = value
        End Set
    End Property

    Private mLastUpdatedBy As String = String.Empty
    Public Property LastUpdatedBy() As String
        Get
            Return mLastUpdatedBy
        End Get
        Set(ByVal value As String)
            mLastUpdatedBy = value
        End Set
    End Property

    Private mLastUpdatedDate As String = String.Empty
    Public Property LastUpdatedDate() As String
        Get
            Return mLastUpdatedDate
        End Get
        Set(ByVal value As String)
            mLastUpdatedDate = value
        End Set
    End Property

    Private mRCFAAnalysisCompDate As String = String.Empty
    Public Property RCFAAnalysisCompDate() As String
        Get
            Return mRCFAAnalysisCompDate
        End Get
        Set(ByVal value As String)
            mRCFAAnalysisCompDate = value
        End Set
    End Property

    Private mRCFAActionCompDate As String = String.Empty
    Public Property RCFAActionCompDate() As String
        Get
            Return mRCFAActionCompDate
        End Get
        Set(ByVal value As String)
            mRCFAActionCompDate = value
        End Set
    End Property

    Private mNotificationLeader As String = String.Empty
    Public Property NotificationLeader() As String
        Get
            Return mNotificationLeader
        End Get
        Set(ByVal value As String)
            mNotificationLeader = value
        End Set
    End Property

    Private mNotificationLeaderEmail As String = String.Empty
    Public Property NotificationLeaderEmail() As String
        Get
            Return mNotificationLeaderEmail
        End Get
        Set(ByVal value As String)
            mNotificationLeaderEmail = value
        End Set
    End Property

    Private mNotificationToList As String = String.Empty
    Public Property NotificationToList() As String
        Get
            Return mNotificationToList
        End Get
        Set(ByVal value As String)
            mNotificationToList = value
        End Set
    End Property

    Private mNotificationCopyList As String = String.Empty
    Public Property NotificationcopyList() As String
        Get
            Return mNotificationCopyList
        End Get
        Set(ByVal value As String)
            mNotificationCopyList = value
        End Set
    End Property

    Private mNotificationToFullName As String = String.Empty
    Public Property NotificationToFullName() As String
        Get
            Return mNotificationToFullName
        End Get
        Set(ByVal value As String)
            mNotificationToFullName = value
        End Set
    End Property
    Private mIRISNumber As String = String.Empty
    Public Property IRISNumber() As String
        Get
            Return mIRISNumber
        End Get
        Set(ByVal value As String)
            mIRISNumber = value
        End Set
    End Property
    Private mCapital As String = String.Empty
    Public Property Capital() As String
        Get
            Return mCapital
        End Get
        Set(ByVal value As String)
            mCapital = value
        End Set
    End Property
    Private mCostofSolution As String = "0"
    Public Property CostofSolution() As String
        Get
            Return mCostofSolution
        End Get
        Set(ByVal value As String)
            mCostofSolution = value
        End Set
    End Property
    Private mConstrainedAreas As String = String.Empty
    Public Property ConstrainedAreas() As String
        Get
            Return mConstrainedAreas
        End Get
        Set(ByVal value As String)
            mConstrainedAreas = value
        End Set
    End Property

    Private mClassificationConstrainedAreas As String = ""
    Public Property ClassificationConstrainedAreas() As String
        Get
            Return mClassificationConstrainedAreas
        End Get
        Set(ByVal value As String)
            mClassificationConstrainedAreas = value
        End Set
    End Property

    Private mClassificationCriticality As String = ""
    Public Property ClassificationCriticality() As String
        Get
            Return mClassificationCriticality
        End Get
        Set(ByVal value As String)
            mClassificationCriticality = value
        End Set
    End Property

    Private mClassificationLifeExpectancy As String
    Public Property ClassificationLifeExpectancy() As String
        Get
            Return mClassificationLifeExpectancy
        End Get
        Set(ByVal value As String)
            mClassificationLifeExpectancy = value
        End Set
    End Property

    Private mClassificationEquipmentCare As String
    Public Property ClassificationEquipmentCare() As String
        Get
            Return mClassificationEquipmentCare
        End Get
        Set(ByVal value As String)
            mClassificationEquipmentCare = value
        End Set
    End Property

    Private mCriticality As Integer
    Public Property Criticality() As Integer
        Get
            Return mCriticality
        End Get
        Set(ByVal value As Integer)
            mCriticality = value
        End Set
    End Property

    Private mAnnualizedSavings As String = "0"
    Public Property AnnualizedSavings() As String
        Get
            Return mAnnualizedSavings
        End Get
        Set(ByVal value As String)
            mAnnualizedSavings = value
        End Set
    End Property
    Private mAttachmentCount As Integer = 0
    Public Property AttachmentCount() As Integer
        Get
            Return mAttachmentCount
        End Get
        Set(ByVal value As Integer)
            mAttachmentCount = value
        End Set
    End Property
    Private mWorkspaceCount As Integer = 0
    Public Property WorkspaceCount() As Integer
        Get
            Return mWorkspaceCount
        End Get
        Set(ByVal value As Integer)
            mWorkspaceCount = value
        End Set
    End Property
    Private mFailureEventCount As Integer = 0
    Public Property FailureEventCount() As Integer
        Get
            Return mFailureEventCount
        End Get
        Set(ByVal value As Integer)
            mFailureEventCount = value
        End Set
    End Property
    Private mActionCount As Integer = 0
    Public Property ActionCount() As Integer
        Get
            Return mActionCount
        End Get
        Set(ByVal value As Integer)
            mActionCount = value
        End Set
    End Property
    Private mTaskHeaderSeqId As String = String.Empty
    Public Property TaskHeaderSeqId() As String
        Get
            Return mTaskHeaderSeqId
        End Get
        Set(ByVal value As String)
            mTaskHeaderSeqId = value
        End Set
    End Property
    Private mVerification As String = String.Empty
    Public Property Verification() As String
        Get
            Return mVerification
        End Get
        Set(ByVal value As String)
            mVerification = value
        End Set
    End Property
    Private mVerificationWeeksAfter As String = String.Empty
    Public Property VerificationWeeksAfter() As String
        Get
            Return mVerificationWeeksAfter
        End Get
        Set(ByVal value As String)
            mVerificationWeeksAfter = value
        End Set
    End Property
    Private mVerificationResp As String = String.Empty
    Public Property VerificationResp() As String
        Get
            Return mVerificationResp
        End Get
        Set(ByVal value As String)
            mVerificationResp = value
        End Set
    End Property
    Private mVerificationDueDate As String = String.Empty
    Public Property VerificationDueDate() As String
        Get
            Return mVerificationDueDate
        End Get
        Set(ByVal value As String)
            mVerificationDueDate = value
        End Set
    End Property
    Private mVerificationClosedDate As String = String.Empty
    Public Property VerificationClosedDate() As String
        Get
            Return mVerificationClosedDate
        End Get
        Set(ByVal value As String)
            mVerificationClosedDate = value
        End Set
    End Property
    Private mVerificationComment As String = String.Empty
    Public Property VerificationComment() As String
        Get
            Return mVerificationComment
        End Get
        Set(ByVal value As String)
            mVerificationComment = value
        End Set
    End Property
    Private mVerRespName As String = String.Empty
    Public Property VerRespName() As String
        Get
            Return mVerRespName
        End Get
        Set(ByVal value As String)
            mVerRespName = value
        End Set
    End Property
    Private mVerTaskHeader As String = String.Empty
    Public Property VerTaskHeader() As String
        Get
            Return mVerTaskHeader
        End Get
        Set(ByVal value As String)
            mVerTaskHeader = value
        End Set
    End Property
    Private mVerTaskItem As String = String.Empty
    Public Property VerTaskItem() As String
        Get
            Return mVerTaskItem
        End Get
        Set(ByVal value As String)
            mVerTaskItem = value
        End Set
    End Property
    Private mSchedUnsched As String = String.Empty
    Public Property SchedUnsched() As String
        Get
            Return mSchedUnsched
        End Get
        Set(ByVal value As String)
            mSchedUnsched = value
        End Set
    End Property

    Public Shared Sub DeleteCurrentRI(ByVal riNumber As Integer, ByVal userName As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters
        Try
            '       in_SiteID  IN varchar2,
            param = New OracleParameter
            param.ParameterName = "IN_RINUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = riNumber
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

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.RINEWINCIDENT.DELETERINUMBER")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Deleting " & riNumber)
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Public Shared Sub MarkAnalysisIncomplete(ByVal UserName As String, ByVal RINumber As Integer)
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
            param.ParameterName = "in_Username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = UserName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.RINEWINCIDENT.ResetAnalysis")
            If CInt(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Marking Analysis Incomplete for " & RINumber)
            End If
        Catch ex As Exception
            Throw New Data.DataException("Error Marking Analysis Incomplete for " & RINumber)
        Finally
        End Try

    End Sub
    Public Shared Sub MarkAnalysisComplete(ByVal UserName As String, ByVal RINumber As Integer)
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
            param.ParameterName = "in_Username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = UserName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.RINEWINCIDENT.MarkAnalysisComplete")
            If CInt(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Marking Analysis Complete for " & RINumber)
            End If
        Catch ex As Exception
            Throw New Data.DataException("Error Marking Analysis Complete for " & RINumber)
        Finally
        End Try

    End Sub
    Public Sub CreateCorrectiveAction(ByVal strResource As String, ByVal strPriority As String, ByVal strTaskDesc As String, ByVal strComments As String, ByVal ActionId As String, ByVal strWO As String, ByVal strRepeatUnits As String, ByVal strRepeatUnitsQty As String, ByVal dtEstDate As String, ByVal dtCompDate As String)

        Dim dr As Devart.Data.Oracle.OracleDataReader = Nothing
        Dim status As String
        Dim paramCollection As New OracleParameterCollection

        Try

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_actionid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = ActionId
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_rinumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RINumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_taskdesc"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strTaskDesc
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_estcompdate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = dtEstDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_actcompdate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = dtCompDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_priority"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strPriority
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_comments"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strComments
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_resource"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strResource
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_wonumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strWO
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_repeatunits"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strRepeatUnits
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_repeatunitsqty"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strRepeatUnitsQty
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_userid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = UserName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "riactions.IncidentActions")

        Catch ex As Exception
            Throw New Data.DataException("CreateCorrectiveAction", ex)
        Finally
            If Not dr Is Nothing Then dr = Nothing
        End Try

    End Sub
    Public Function SaveIncident() As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
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
            '      in_EventDate  IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_EventDate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input

            'Dim eventDate As String = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Me.IncidentStartDate, "EN-US", "G")

            'eventDate.ToString(New System.Globalization.CultureInfo("EN-US"))
            'param.Value = FormatDateTime(CDate(Me.IncidentStartDate), DateFormat.ShortDate) & " " & FormatDateTime(CDate(Me.IncidentStartDate), DateFormat.ShortTime)
            'param.Value = FormatDateTime(CDate(eventDate), DateFormat.ShortDate) & " " & FormatDateTime(CDate(eventDate), DateFormat.ShortTime)
            param.Value = Me.IncidentStartDate 'eventDate
            paramCollection.Add(param)
            '      in_IncidentEndDate IN varchar2,
            'Dim endDate As String = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Me.IncidentEndDate, "EN-US", "G")
            param = New OracleParameter
            param.ParameterName = "in_IncidentEndDate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = IncidentEndDate 'endDate
            'param.Value = FormatDateTime(CDate(Me.IncidentEndDate), DateFormat.ShortDate) & " " & FormatDateTime(CDate(Me.IncidentEndDate), DateFormat.ShortTime)
            paramCollection.Add(param)
            '      in_BusinessUnit IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_BusinessUnit"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.BusinessUnit
            paramCollection.Add(param)
            '      in_Area IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Area"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Area
            paramCollection.Add(param)
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
            param.Value = Me.IncidentTitle
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
            '      in_Equipmentid IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Equipmentid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.FunctionalLocation
            paramCollection.Add(param)
            '      in_Recordable IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Recordable"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Recordable
            paramCollection.Add(param)
            '      in_RCFA IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_RCFA"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RCFA
            paramCollection.Add(param)

            '      in_SRR IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_SRR"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.SRR
            'If Me.SRR.Length > 0 Then
            '    If Me.SRR = "Any Constrained Process DT => 16 Hr" Then
            '        param.Value = "DNT"
            '    Else
            '        If Me.SRR = "Financial Impact >= $250,000" Then
            '            param.Value = "FIN"
            '        Else
            '            param.Value = "BTH"
            '        End If
            '    End If
            'Else
            '    param.Value = ""
            'End If
            paramCollection.Add(param)


            '      in_Prevention IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Prevention"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Prevention
            paramCollection.Add(param)
            '      in_Cost IN number,
            param = New OracleParameter
            param.ParameterName = "in_Cost"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = CLng(Me.Cost) 'FormatNumber(Me.Cost, 0)
            paramCollection.Add(param)
            '      in_TotCost IN number,
            param = New OracleParameter
            param.ParameterName = "in_TotCost"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = CLng(Me.TotalCost) ' FormatNumber(Me.TotalCost, 0)
            paramCollection.Add(param)
            '      in_LossOpportunity IN number,
            'param = New OracleParameter
            'param.ParameterName = "in_LossOpportunity"
            'param.OracleDbType = OracleDbType.Number
            'param.Direction = Data.ParameterDirection.Input
            'param.Value = FormatNumber(Me.LossOpportunity, 0)
            'paramCollection.Add(param)

            'in_AnnualizedSavings IN number,
            param = New OracleParameter
            param.ParameterName = "in_AnnualizedSavings"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = CLng(Me.AnnualizedSavings) ' FormatNumber(Me.AnnualizedSavings, 0)
            paramCollection.Add(param)
            'in_CostofSolution IN number,
            param = New OracleParameter
            param.ParameterName = "in_CostofSolution"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = CLng(Me.CostofSolution) ' FormatNumber(Me.CostofSolution, 0)
            paramCollection.Add(param)
            'in_Capital IN number,
            param = New OracleParameter
            param.ParameterName = "in_Capital"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            If Me.Capital.Length > 0 Then
                param.Value = Me.Capital
            End If
            paramCollection.Add(param)

            '      in_Description IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Description"
            param.OracleDbType = OracleDbType.NClob
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.IncidentDescription
            paramCollection.Add(param)
            '      in_Downtime IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Downtime"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Replace(Me.Downtime, ",", "")
            paramCollection.Add(param)
            '      in_UserName IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_UserName"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.UserName
            paramCollection.Add(param)
            '      in_Linebreak IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Linebreak"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Linebreak
            paramCollection.Add(param)
            '      in_RCFASafety IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_RCFASafety"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RCFASafety
            paramCollection.Add(param)
            '      in_RCFAQuality IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_RCFAQuality"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RCFAQuality
            paramCollection.Add(param)
            '      in_Crew IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Crew"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Crew
            paramCollection.Add(param)
            '      in_Shift IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Shift"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Shift
            paramCollection.Add(param)
            '      in_ClassCost IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_ClassCost"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = CLng(DataClean(Me.ClassCost, CStr(0))) 'FormatNumber(DataClean(Me.ClassCost, CStr(0)), 0)
            paramCollection.Add(param)
            '      in_ClassLife IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_ClassLife"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = CLng(DataClean(Me.ClassLife, CStr(0))) 'FormatNumber(DataClean(Me.ClassLife, CStr(0)), 0)
            paramCollection.Add(param)
            '      in_ClassSchedule IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_ClassSchedule"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = CLng(DataClean(Me.ClassSchedule, CStr(0))) ' FormatNumber(DataClean(Me.ClassSchedule, CStr(0)), 0)
            paramCollection.Add(param)
            '      in_ClassImpact IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_ClassImpact"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = CLng(DataClean(Me.ClassImpact, CStr(0))) 'FormatNumber(DataClean(Me.ClassImpact, CStr(0)), 0)
            paramCollection.Add(param)
            '      in_ClassChronic IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_ClassChronic"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = CLng(DataClean(Me.ClassChronic, CStr(0))) 'FormatNumber(DataClean(Me.ClassChronic, CStr(0)), 0)
            paramCollection.Add(param)
            '      in_RCFALeader IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_RCFALeader"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RCFALeader
            paramCollection.Add(param)
            '      in_CertifiedKill IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_CertifiedKill"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.CertifiedKill
            paramCollection.Add(param)
            '      in_Chronic IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Chronic"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Chronic
            paramCollection.Add(param)
            '      in_RCFACondition IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_RCFACondition"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RCFACondition
            paramCollection.Add(param)
            '      in_RCFATeam IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_RCFATeam"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RCFATeamMembers
            paramCollection.Add(param)
            '      in_RCFAPeople IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_RCFAPeople"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RCFAPeople
            paramCollection.Add(param)
            '      in_RCFATriggerDesc IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_RCFATriggerDesc"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RCFATriggerDesc
            paramCollection.Add(param)
            '      in_RCFAFailedLocation IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_RCFAFailedLocation"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RCFAFailedLocation
            paramCollection.Add(param)
            '      in_PhysicalCauses IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Physical_Causes"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.PhysicalCauses
            paramCollection.Add(param)
            '      in_Latent_Causes IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Latent_Causes"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.LatentCauses
            paramCollection.Add(param)
            '      in_Human_Causes IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Human_Causes"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.HumanCauses
            paramCollection.Add(param)
            '      in_OtherPhysical IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_OtherPhysical"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.OtherPhysicalCauses
            paramCollection.Add(param)
            '      in_OtherLatent IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_OtherLatent"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.OtherLatentCauses
            paramCollection.Add(param)
            '      in_OtherHuman IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_OtherHuman"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.OtherHumanCauses
            paramCollection.Add(param)
            '      in_RINumber IN number,
            param = New OracleParameter
            param.ParameterName = "in_RINumber"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            If Me.RINumber.Length = 0 Then
                param.Value = System.DBNull.Value
            Else
                param.Value = RINumber
            End If
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_IRISNumber"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            If Me.IRISNumber.Length = 0 Then
                param.Value = System.DBNull.Value
            Else
                param.Value = IRISNumber
            End If
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_ConstrainedArea"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = DataClean(ConstrainedAreas)
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Criticality"
            param.OracleDbType = OracleDbType.Integer
            param.Direction = Data.ParameterDirection.Input
            param.Value = DataClean(Criticality, CStr(0))
            paramCollection.Add(param)

            'New Failure Classification
            param = New OracleParameter
            param.ParameterName = "in_ClassConstrainedArea"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = DataClean(Me.ClassificationConstrainedAreas)
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_ClassCriticalityRating"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = DataClean(Me.ClassificationCriticality)
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_ClassLifeExpectancy"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = DataClean(Me.ClassificationLifeExpectancy)
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_ClassEquipmentCare"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = DataClean(Me.ClassificationEquipmentCare)
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Verification"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = DataClean(Me.Verification)
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Verification_Responsible"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = DataClean(Me.VerificationResp)
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_WeeksAfter"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = DataClean(Me.VerificationWeeksAfter)
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_SchedUnsched"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = DataClean(Me.SchedUnsched)
            paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "in_VerificationDueDate"
            'param.OracleDbType = OracleDbType.VarChar
            'param.Direction = Data.ParameterDirection.Input
            'param.Value = DataClean(Me.VerificationDueDate)
            'paramCollection.Add(param)

            'param = New OracleParameter
            'param.ParameterName = "in_VerificationComment"
            'param.OracleDbType = OracleDbType.VarChar
            'param.Direction = Data.ParameterDirection.Input
            'param.Value = DataClean(Me.VerificationComment)
            'paramCollection.Add(param)

            '      out_RINumber OUT number,
            param = New OracleParameter
            param.ParameterName = "out_RINumber"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)
            '      out_status OUT number
            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)
            'ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.UPDATEINCIDENT")
            'If ds IsNot Nothing Then

            'End If
            ' Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.RINEWINCIDENT.UPDATEINCIDENT")
            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.RINEWINCIDENT.UPDATEINCIDENT")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving RINumber=[" & Me.RINumber & "] ORA " & returnStatus)
            Else
                Me.RINumber = CStr(paramCollection.Item("out_RINumber").Value)
            End If
        Catch ex As Exception
            Throw
        End Try
        'Dim NotificationListDR As Data.DataTableReader = GetNotificationList()
        'Dim ToList As New StringBuilder
        'Dim CopyList As New StringBuilder
        'Dim ToName As New StringBuilder
        'Dim NotificationLeader As String = String.Empty

        'If NotificationListDR IsNot Nothing Then
        '    If NotificationListDR.HasRows Then
        '        Do While NotificationListDR.Read
        '            If DataClean(NotificationListDR.Item("Notifytype")).ToUpper = "T" Then
        '                If ToList.Length > 0 Then ToList.Append(",")
        '                If ToName.Length > 0 Then ToName.Append(",")
        '                ToList.Append(DataClean(NotificationListDR.Item("Email")))
        '                ToName.Append(DataClean(NotificationListDR.Item("FirstName")) & " " & DataClean(NotificationListDR.Item("LastName")))
        '            Else ' Assume Copy
        '                If CopyList.Length > 0 Then CopyList.Append(",")
        '                CopyList.Append(DataClean(NotificationListDR.Item("Email")))
        '            End If
        '            NotificationLeader = DataClean(NotificationListDR.Item("LeaderName"))
        '            NotificationLeaderEmail = DataClean(NotificationListDR.Item("LeaderEmail"))
        '        Loop
        '        Me.NotificationLeader = NotificationLeader

        '        Me.NotificationToFullName = ToName.ToString
        '        Me.NotificationToList = ToList.ToString
        '        Me.NotificationcopyList = CopyList.ToString
        '        NotificationListDR = Nothing
        '    End If
        'End If
        Return RINumber
    End Function
    Private Sub GetRI()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters

        Try
            If Not IsNumeric(RINumber) Then Throw New Data.DataException("Invalid RI Number was specified - " & RINumber)

            param = New OracleParameter
            param.ParameterName = "in_rinumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = RINumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rs"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsSafety"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "NewIncident.GetIncidentListing_" & RINumber
            'ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.GETINCIDENTLISTING", key, 0)
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.GETINCIDENTLISTING", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 2 Then
                    Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader
                    Dim dr2 As Data.DataTableReader = ds.Tables(1).CreateDataReader
                    Dim selectedSafety As String = String.Empty
                    Dim sb As New StringBuilder
                    If dr2 IsNot Nothing Then
                        If dr2.HasRows Then
                            Do While dr2.Read()
                                If sb.Length >= 0 Then sb.Append(",")
                                sb.Append(dr2.Item("safety_desc"))
                            Loop
                            Me.RCFASafety = sb.ToString
                        End If
                    End If
                    If dr IsNot Nothing Then
                        If dr.HasRows Then
                            dr.Read()
                            With Me
                                'State

                                .RCFALeader = DataClean(dr.Item("RCFALEADER"))
                                .RCFALeaderName = Me.GetRCFALeader(.RCFALeader)
                                .RCFAAnalysisCompDate = CleanDate(dr.Item("RCFAAnalysisCompDate"), DateFormat.ShortDate)
                                .RCFAActionCompDate = CleanDate(dr.Item("RCFAActionCompDate"), DateFormat.ShortDate)

                                'Location
                                .SiteID = DataClean(dr.Item("Siteid"))
                                .BusinessUnit = DataClean(dr.Item("risuperarea_fmt")) '& " - " & DataClean(dr.Item("SubArea"))
                                .Area = DataClean(dr.Item("SubArea"))
                                .FunctionalLocation = DataClean(dr.Item("Equipmentid"))
                                .Line = DataClean(dr.Item("Area"))
                                .Linebreak = DataClean(dr.Item("linebreak"))
                                If .Line.Length > 0 And .Linebreak.Length > 0 Then
                                    .Linebreak = .Line & " - " & .Linebreak
                                ElseIf .Line.Length > 0 Then
                                    .Linebreak = .Line
                                Else
                                    .Linebreak = "None" & " - " & .Linebreak
                                End If

                                'If .Linebreak.IndexOf("-") < 0 Then .Linebreak = .Linebreak & " - None"
                                'Incident
                                .IncidentStartDate = DataClean(dr.Item("EventDate"))
                                .IncidentEndDate = DataClean(dr.Item("IncidentEndDate"))

                                If .IncidentEndDate.Length = 0 Then .IncidentEndDate = .IncidentStartDate
                                .IncidentStartDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(.IncidentStartDate, "EN-US")
                                .IncidentEndDate = IP.Bids.Localization.DateTime.GetLocalizedDateTime(.IncidentEndDate, "EN-US")


                                'Don't forget about the hours and minutes
                                .Downtime = DataClean(dr.Item("Downtime"))
                                .IncidentTitle = DataClean(dr.Item("Incident"))
                                .Crew = DataClean(dr.Item("Crew"))
                                .Shift = DataClean(dr.Item("Shift"))
                                .IncidentDescription = DataClean(dr.Item("Description"))
                                .Cost = DataClean(dr.Item("Cost"), CStr(0))
                                .LossOpportunity = DataClean(dr.Item("LossOpportunity"), CStr(0))
                                .TotalCost = DataClean(dr.Item("TotCost"), CStr(0))
                                .Capital = DataClean(dr.Item("capital_seq_id"))
                                .AnnualizedSavings = (DataClean(dr.Item("AnnualizedSavings"), CStr(0)))
                                .CostofSolution = (DataClean(dr.Item("CostofSolution"), CStr(0)))
                                'Incident Type
                                If DataClean(dr.Item("ORIGINATINGAPPLICATION")).Length > 0 Then
                                    If DataClean(dr.Item("ORIGINATINGAPPLICATION")).ToLower = "rts" Then
                                        .RTS = "Yes"
                                    Else
                                        .RTS = "No"
                                    End If
                                    If DataClean(dr.Item("ORIGINATINGAPPLICATION")).ToLower = "ppr" Then
                                        .PPR = "Yes"
                                    Else
                                        .PPR = "No"
                                    End If
                                Else
                                    .RTS = "No"
                                    .PPR = "No"
                                End If

                                'If DataClean(dr.Item("SRR")).Length > 0 Then
                                '    If DataClean(dr.Item("SRR")).ToLower = "dnt" Then
                                '        .SRR = "Any Constrained Process DT => 16 Hr"
                                '    Else
                                '        If DataClean(dr.Item("SRR")).ToLower = "fin" Then
                                '            .SRR = "Financial Impact >= $250,000"
                                '        Else
                                '            .SRR = "Both"
                                '        End If
                                '    End If
                                'Else
                                '    .SRR = ""
                                'End If

                                .SRR = DataClean(dr.Item("SRR"))
                                .Recordable = DataClean(dr.Item("Recordable"))
                                .Chronic = DataClean(dr.Item("CHRONIC"))
                                .RCFAQuality = DataClean(dr.Item("RCFAQuality"))
                                .RCFA = DataClean(dr.Item("RCFA"))
                                .RCFALevel = DataClean(dr.Item("RCFALevel"))
                                .CertifiedKill = DataClean(dr.Item("CertifiedKill"))

                                'Incdent Classification
                                .RCFATriggerDesc = DataClean(dr.Item("RCFATRIGGERDESC"))
                                .Type = DataClean(dr.Item("cause"))
                                .Cause = DataClean(dr.Item("Reason"))
                                .Prevention = DataClean(dr.Item("Prevention"))
                                .Process = DataClean(dr.Item("Process"))
                                .Component = DataClean(dr.Item("Component"))

                                If .Type.Length > 0 Then .Type = Replace(.Type, ":", ">")
                                If .Cause.Length > 0 Then .Cause = Replace(.Cause, ":", ">")
                                If .Prevention.Length > 0 Then .Prevention = Replace(.Prevention, ":", ">")
                                If .Process.Length > 0 Then .Process = Replace(.Process, ":", ">")
                                If .Component.Length > 0 Then .Component = Replace(.Component, ":", ">")
                                'Other
                                .RCFAFailedLocation = DataClean(dr.Item("RCFAFailedLocation"))
                                .RCFACondition = DataClean(dr.Item("RCFACondition"))
                                .RCFAPeople = DataClean(dr.Item("RCFAPeople"))
                                .RCFATeamMembers = DataClean(dr.Item("RCFATeam"))

                                'Creation
                                .CreatedBy = GetRCFALeader(DataClean(dr.Item("username")))
                                .CreationDate = CleanDate(dr.Item("recorddate"), DateFormat.ShortDate)
                                .LastUpdatedBy = GetRCFALeader(DataClean(dr.Item("updateusername")))
                                .LastUpdatedDate = CleanDate(dr.Item("updatedate"), DateFormat.ShortDate)

                                'Failure Classification
                                .ClassChronic = DataClean(dr.Item("ClassChronic"), CStr(2))
                                .ClassCost = DataClean(dr.Item("ClassCost"), CStr(2))
                                .ClassImpact = DataClean(dr.Item("ClassImpact"), CStr(2))
                                .ClassLife = DataClean(dr.Item("ClassLife"), CStr(2))
                                .ClassSchedule = DataClean(dr.Item("ClassSchedule"), CStr(2))

                                'New Failure Classification
                                .ClassificationConstrainedAreas = DataClean(dr.Item("CLASSCONSTRAINEDAREAS"), CStr(2))
                                .ClassificationCriticality = DataClean(dr.Item("CLASSCRITICALITY"), CStr(2))
                                .ClassificationEquipmentCare = DataClean(dr.Item("CLASSEQUIPMENTCARE"), CStr(2))
                                .ClassificationLifeExpectancy = DataClean(dr.Item("CLASSLIFEEXPECTANCY"), CStr(2))

                                'Physical Causes                                
                                Dim sbPhysicalCauses As New StringBuilder
                                If DataClean(dr.Item("RCFAPCDEFECTMGF")).Length > 0 Then
                                    sbPhysicalCauses.Append(",")
                                    sbPhysicalCauses.Append("Defect in Mfg")
                                End If

                                If DataClean(dr.Item("RCFAPCMATERIALSELINCORRECT")).Length > 0 Then
                                    sbPhysicalCauses.Append(",")
                                    sbPhysicalCauses.Append("Material Select Incorrect")
                                End If

                                If DataClean(dr.Item("RCFAPCUNINTENDEDOPER")).Length > 0 Then
                                    sbPhysicalCauses.Append(",")
                                    sbPhysicalCauses.Append("Unintended Operation")
                                End If

                                If DataClean(dr.Item("RCFAPCPROCESSCHG")).Length > 0 Then
                                    sbPhysicalCauses.Append(",")
                                    sbPhysicalCauses.Append("Process Change")
                                End If

                                If DataClean(dr.Item("RCFAPCDMGSHIP")).Length > 0 Then
                                    sbPhysicalCauses.Append(",")
                                    sbPhysicalCauses.Append("Damage in Shipping")
                                End If

                                If DataClean(dr.Item("RCFAPCEQUIPSIZE")).Length > 0 Then
                                    sbPhysicalCauses.Append(",")
                                    sbPhysicalCauses.Append("Equipment Size/Spec")
                                End If

                                If DataClean(dr.Item("RCFAPCMAINTOVERSIGHT")).Length > 0 Then
                                    sbPhysicalCauses.Append(",")
                                    sbPhysicalCauses.Append("Maintenance Oversight")
                                End If

                                If DataClean(dr.Item("RCFAPCMEASURE")).Length > 0 Then
                                    sbPhysicalCauses.Append(",")
                                    sbPhysicalCauses.Append("Measurement Error")
                                End If

                                If DataClean(dr.Item("RCFAPCCOMPSOFTWAREERROR")).Length > 0 Then
                                    sbPhysicalCauses.Append(",")
                                    sbPhysicalCauses.Append("Computer Software Error")
                                End If

                                If DataClean(dr.Item("RCFAPCCONTAMINATION")).Length > 0 Then
                                    sbPhysicalCauses.Append(",")
                                    sbPhysicalCauses.Append("Contamination")
                                End If

                                If DataClean(dr.Item("RCFAPCSTARTUPFAIL")).Length > 0 Then
                                    sbPhysicalCauses.Append(",")
                                    sbPhysicalCauses.Append("Startup Failure")
                                End If

                                If DataClean(dr.Item("RCFAPCINCORRECTPART")).Length > 0 Then
                                    sbPhysicalCauses.Append(",")
                                    sbPhysicalCauses.Append("Incorrect Part")
                                End If

                                If DataClean(dr.Item("RCFAPCWEATHER")).Length > 0 Then
                                    sbPhysicalCauses.Append(",")
                                    sbPhysicalCauses.Append("Weather Conditions")
                                End If

                                If DataClean(dr.Item("RCFAPCPOWERFAILOUTSIDEAREA")).Length > 0 Then
                                    sbPhysicalCauses.Append(",")
                                    sbPhysicalCauses.Append("Power Failure Outside Area")
                                End If
                                If DataClean(dr.Item("RCFAPCOther")).Length > 0 Then
                                    sbPhysicalCauses.Append(",")
                                    sbPhysicalCauses.Append("Other Physical Cause")
                                    .OtherPhysicalCauses = DataClean(dr.Item("RCFAPCOther"))
                                End If
                                .PhysicalCauses = sbPhysicalCauses.ToString


                                Dim sbLatentCauses As New StringBuilder
                                If DataClean(dr.Item("RCFALCTRAINDEF")).Length > 0 Then
                                    sbLatentCauses.Append(",")
                                    sbLatentCauses.Append("Training Deficiency")
                                End If
                                If DataClean(dr.Item("RCFALCMWSDEF")).Length > 0 Then
                                    sbLatentCauses.Append(",")
                                    sbLatentCauses.Append("MWS Deficiency")
                                End If
                                If DataClean(dr.Item("RCFALCCOMMUNICATEDEF")).Length > 0 Then
                                    sbLatentCauses.Append(",")
                                    sbLatentCauses.Append("Communication Deficiency")
                                End If
                                If DataClean(dr.Item("RCFALCPRACTICE")).Length > 0 Then
                                    sbLatentCauses.Append(",")
                                    sbLatentCauses.Append("Practice/Experience")
                                End If
                                If DataClean(dr.Item("RCFALCPROCEDUREDEF")).Length > 0 Then
                                    sbLatentCauses.Append(",")
                                    sbLatentCauses.Append("Procedural Deficiency")
                                End If
                                If DataClean(dr.Item("RCFALCSPECIFICDEF")).Length > 0 Then
                                    sbLatentCauses.Append(",")
                                    sbLatentCauses.Append("Specification Deficiency")
                                End If
                                If DataClean(dr.Item("RCFALCENFORCEPOLICY")).Length > 0 Then
                                    sbLatentCauses.Append(",")
                                    sbLatentCauses.Append("Enforcement of Policy")
                                End If
                                If DataClean(dr.Item("RCFALCMGMTCHG")).Length > 0 Then
                                    sbLatentCauses.Append(",")
                                    sbLatentCauses.Append("Management Change")
                                End If
                                If DataClean(dr.Item("RCFALCOther")).Length > 0 Then
                                    sbLatentCauses.Append(",")
                                    sbLatentCauses.Append("Other Latent Cause")
                                    .OtherLatentCauses = DataClean(dr.Item("RCFALCOther"))
                                End If

                                .LatentCauses = sbLatentCauses.ToString


                                Dim sbHumanCauses As StringBuilder = New StringBuilder
                                If DataClean(dr.Item("RCFAHCPROCEDURE")).Length > 0 Then
                                    sbHumanCauses.Append(",")
                                    sbHumanCauses.Append("Failed to Follow Procedure")
                                End If
                                If DataClean(dr.Item("RCFAHCLACKKNOWLEDGE")).Length > 0 Then
                                    sbHumanCauses.Append(",")
                                    sbHumanCauses.Append("Lack Knowledge/Not Trained")
                                End If
                                If DataClean(dr.Item("RCFAHCPOORCOMMUNICATION")).Length > 0 Then
                                    sbHumanCauses.Append(",")
                                    sbHumanCauses.Append("Poor Communication")
                                End If
                                If DataClean(dr.Item("RCFAHCINADVERTENTERROR")).Length > 0 Then
                                    sbHumanCauses.Append(",")
                                    sbHumanCauses.Append("Inadvertent Error")
                                End If
                                If DataClean(dr.Item("RCFAHCCALCRISK")).Length > 0 Then
                                    sbHumanCauses.Append(",")
                                    sbHumanCauses.Append("Calculated Risk")
                                End If
                                If DataClean(dr.Item("RCFAHCPREVENTED")).Length > 0 Then
                                    sbHumanCauses.Append(",")
                                    sbHumanCauses.Append("Circumstance Prevent Resp")
                                End If
                                If DataClean(dr.Item("RCFAHUMANCAUSE")).Length > 0 Then
                                    sbHumanCauses.Append(",")
                                    sbHumanCauses.Append("Other Human Cause")
                                    .OtherHumanCauses = DataClean(dr.Item("RCFAHUMANCAUSE"))
                                End If

                                .HumanCauses = sbHumanCauses.ToString
                                .Capital = DataClean(dr.Item("capital_seq_id"))
                                .AttachmentCount = CInt(DataClean(dr.Item("AttachmentCount"), CStr(0)))
                                .ActionCount = CInt(DataClean(dr.Item("ActionCount"), CStr(0)))
                                .WorkspaceCount = CInt(DataClean(dr.Item("WorkspaceCount"), CStr(0)))
                                .FailureEventCount = CInt(DataClean(dr.Item("FailureEventCount"), CStr(0)))
                                .IRISNumber = DataClean(dr.Item("IRISNumber"))
                                .ConstrainedAreas = DataClean(dr.Item("ConstrainedAreas"), CStr(0))
                                .Criticality = CInt(DataClean(dr.Item("Criticality"), CStr(0)))
                                .TaskHeaderSeqId = DataClean(dr.Item("MTTTaskHeaderSeqId"))

                                ' Added for RI Verification modification CAC 7/16/14
                                .Verification = DataClean(dr.Item("Verification"))
                                .VerificationComment = DataClean(dr.Item("VerTaskComment"))
                                .VerificationDueDate = DataClean(dr.Item("DueDate"))
                                .VerificationClosedDate = DataClean(dr.Item("ClosedDate"))
                                .VerificationResp = DataClean(dr.Item("Responsibleusername"))
                                .VerificationWeeksAfter = DataClean(dr.Item("WeeksAfter"))
                                .VerRespName = DataClean(dr.Item("VerRespName"))
                                .VerTaskHeader = DataClean(dr.Item("VerTaskHeader"))
                                .VerTaskItem = DataClean(dr.Item("VerTaskItem"))

                                .SchedUnsched = DataClean(dr.Item("SchedDT"))

                            End With
                            Me.GetNotificationList()
                        Else
                            mSiteID = Nothing
                        End If
                    End If



                End If
            End If
        Catch ex As ArgumentException
            HttpContext.Current.Server.ClearError()
        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                ds = Nothing
            End If

        End Try
    End Sub

    Private Function GetRCFALeader(ByVal user As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty
        Dim ret As String = user
        'Check input paramaters

        Try
            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = user
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsRCFALeader"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "GetRCFALeader_" & user
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.GetRCFALeader", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count = 1 Then
                    Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader
                    If dr IsNot Nothing Then
                        If dr.HasRows Then
                            dr.Read()
                            With Me
                                'RCFALeader
                                ret = DataClean(dr.Item("FirstName")) & " " & DataClean(dr.Item("LastName"))
                            End With
                        End If
                    End If
                    dr = Nothing

                End If
            End If

        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                ds = Nothing
            End If

            GetRCFALeader = ret
        End Try
    End Function
    Private Function GetNotificationList() As Data.DataTableReader
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As Data.DataTableReader = Nothing
        'Check input paramaters

        Try
            param = New OracleParameter
            param.ParameterName = "IN_RINUMBER"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.RINumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "RSNOTIFICATIONLIST"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "GetNotificationList" & RINumber

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.GetNotificationList", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count = 1 Then
                    dr = ds.Tables(0).CreateDataReader
                End If
            End If

            Dim ToList As New StringBuilder
            Dim CopyList As New StringBuilder
            Dim ToName As New StringBuilder
            Dim NotificationLeader As String = String.Empty

            If dr IsNot Nothing Then
                If dr.HasRows Then
                    Do While dr.Read
                        If DataClean(dr.Item("Notifytype")).ToUpper = "T" Then
                            If ToList.Length > 0 Then ToList.Append(",")
                            If ToName.Length > 0 Then ToName.Append(",")
                            ToList.Append(DataClean(dr.Item("Email")))
                            ToName.Append(DataClean(dr.Item("FirstName")) & " " & DataClean(dr.Item("LastName")))
                        Else ' Assume Copy
                            If CopyList.Length > 0 Then CopyList.Append(",")
                            CopyList.Append(DataClean(dr.Item("Email")))
                        End If
                        If DataClean(dr.Item("LeaderName")).Length > 0 Then
                            NotificationLeader = DataClean(dr.Item("LeaderName"))
                            If DataClean(dr.Item("LeaderEmail")).Length > 0 Then
                                NotificationLeaderEmail = DataClean(dr.Item("LeaderEmail"))
                            Else
                                'This code should never get executed b/c the procedure should always return an email address
                                NotificationLeaderEmail = NotificationLeader & "@graphicpkg.com"
                                RI.SharedFunctions.InsertAuditRecord("GetNotificationList", "A valid email address was missing for this user [" & NotificationLeader & "]")
                            End If
                        End If

                    Loop
                    'Me.NotificationLeader = NotificationLeader

                    Me.NotificationToFullName = Replace(ToName.ToString, ",,", ",")
                    Me.NotificationToList = Replace(ToList.ToString, ",,", ",")
                    Me.NotificationcopyList = Replace(CopyList.ToString, ",,", ",")
                    If Me.NotificationToFullName Is Nothing Then
                        Me.NotificationToFullName = String.Empty
                    End If
                    If Me.NotificationToList Is Nothing Then
                        Me.NotificationToList = String.Empty
                    End If
                    If Me.NotificationcopyList Is Nothing Then
                        Me.NotificationcopyList = String.Empty
                    End If
                End If
            End If
        Catch ex As Exception
            Throw
            dr = Nothing
        Finally
            If ds IsNot Nothing Then
                ds = Nothing
            End If
            GetNotificationList = dr
        End Try
    End Function
    Public Function GetEHSNotificationList(ByVal BusinessUnit As String, ByVal Area As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim dr As Devart.Data.Oracle.OracleDataReader
        'Check input paramaters

        Try
            param = New OracleParameter
            param.ParameterName = "in_BusinessUnit"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = ParameterDirection.Input
            param.Value = BusinessUnit
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Area"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = ParameterDirection.Input
            param.Value = Area
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Facility"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SiteID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "RSNOTIFICATIONLIST"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "GetEHSNotificationList"
            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "Reladmin.RINEWINCIDENT.GetEHSNotificationList")

            Dim ToList As New StringBuilder
            Dim CopyList As New StringBuilder
            Dim NotificationLeader As String = String.Empty
            Dim NotificationLeaderList As New StringBuilder

            If dr IsNot Nothing Then
                If dr.HasRows Then
                    Do While dr.Read
                        If DataClean(dr.Item("Notifytype")).ToUpper = "T" Then
                            If ToList.Length > 0 Then ToList.Append(",")
                            'ToList.Append(DataClean(dr.Item("UserName")) & "@ipaper.com")
                            ToList.Append(DataClean(dr.Item("EMail")))
                            If NotificationLeaderList.Length > 0 Then NotificationLeaderList.Append(",")
                            NotificationLeaderList.Append(DataClean(dr.Item("UserName")) & "-" & DataClean(dr.Item("EMail")))
                        Else ' Assume Copy
                            If CopyList.Length > 0 Then CopyList.Append(",")
                            'CopyList.Append(DataClean(dr.Item("UserName")) & "@ipaper.com")
                            CopyList.Append(DataClean(dr.Item("EMail")))
                            If NotificationLeaderList.Length > 0 Then NotificationLeaderList.Append(",")
                            NotificationLeaderList.Append(DataClean(dr.Item("UserName")) & "-" & DataClean(dr.Item("EMail")))
                        End If
                    Loop
                    'If ToList.Length > 0 Then NotificationLeader = ToList.ToString

                    'If CopyList.Length > 0 Then
                    '    If NotificationLeader.Length > 0 Then NotificationLeader = NotificationLeader & ","
                    '    NotificationLeader = NotificationLeader & CopyList.ToString
                    'End If
                    NotificationLeader = NotificationLeaderList.ToString
                End If
            End If
            Dim leaders As String() = Split(NotificationLeader, ",")
            If leaders.Length > 0 Then NotificationLeader = leaders(0)
            GetEHSNotificationList = NotificationLeader

            'Me.NotificationToFullName = Replace(ToName.ToString, ",,", ",")
            Me.NotificationToList = Replace(ToList.ToString, ",,", ",")
            Me.NotificationcopyList = Replace(CopyList.ToString, ",,", ",")
        Catch ex As Exception
            Throw
            dr = Nothing
        Finally
            If ds IsNot Nothing Then
                ds = Nothing
            End If

        End Try
    End Function
    Public Sub New(ByVal riNum As String)

        If riNum IsNot Nothing And riNum.Length > 0 Then
            RINumber = riNum
            GetRI()
        Else
            RINumber = String.Empty
        End If
    End Sub
    Public Sub New()
        RINumber = String.Empty
    End Sub
End Class