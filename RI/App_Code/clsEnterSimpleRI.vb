Option Explicit On
Option Strict On


Imports Microsoft.VisualBasic
Imports Devart.Data.Oracle
Imports RI.SharedFunctions
<Serializable()> _
Public Class clsEnterSimpleRI
    Public Structure RISecurity
        Dim DeleteIncidents As Boolean
        Dim SaveIncidents As Boolean
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
                    Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader
                    If dr IsNot Nothing Then
                        dr.Read()

                    End If
                End If
            End If

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

    Public ReadOnly Property BusinessUnitArea() As clsData
        Get
            Return mBusinessUnitArea
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
    Private mFacility As New clsData
    Private mBusinessUnitArea As New clsData
    Private mCause As New clsData
    Private mPrevention As New clsData
    Private mType As New clsData
    Private mComponent As New clsData
    Private mEquipmentProcess As New clsData
    Private mLineBreak As New clsData
    
    Public Sub New(ByVal userName As String, ByVal facility As String, ByVal inActiveFlag As String, Optional ByVal division As String = "", Optional ByVal busArea As String = "", Optional ByVal lineBreak As String = "", Optional ByVal riNumber As String = "")

        GetData(userName, facility, inActiveFlag, division, busArea, lineBreak)

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
Public Class clsCurrentSimpleRI
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
    Public Property Prevention() As String
        Get
            Return mPrevention
        End Get
        Set(ByVal value As String)
            mPrevention = value.Trim
        End Set
    End Property
    Private mPrevention As String = String.Empty

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
    Private mRINumber As String = String.Empty
    Public Property RINumber() As String
        Get
            Return mRINumber
        End Get
        Set(ByVal value As String)
            mRINumber = value.Trim
        End Set
    End Property
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

    Private mSchedDT As String = String.Empty
    Public Property schedDT() As String
        Get
            Return mSchedDT
        End Get
        Set(ByVal value As String)
            mSchedDT = value
        End Set
    End Property

    Private mOrigApp As String = String.Empty
    Public Property origApp() As String
        Get
            Return mOrigApp
        End Get
        Set(ByVal value As String)
            mOrigApp = value
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
            '      in_Recordable IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Recordable"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Recordable
            paramCollection.Add(param)
            '      in_Prevention IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_Prevention"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.Prevention
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
            '      in_SchedDT IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_SchedDowntime"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.schedDT
            paramCollection.Add(param)

            '      in_SchedDT IN varchar2,
            param = New OracleParameter
            param.ParameterName = "in_OrigApplication"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me.OrigApp
            paramCollection.Add(param)

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

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.RINEWINCIDENT.UPDATESIMPLEINCIDENT")
            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving RINumber=[" & Me.RINumber & "] ORA " & returnStatus)
            Else
                Me.RINumber = CStr(paramCollection.Item("out_RINumber").Value)
            End If
        Catch ex As Exception
            Throw
        End Try

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

            Dim key As String = "NewIncident.GetSimpleIncidentListing_" & RINumber
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.GETINCIDENTLISTING", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 2 Then
                    Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader
                    Dim dr2 As Data.DataTableReader = ds.Tables(1).CreateDataReader
                    Dim sb As New StringBuilder

                    If dr IsNot Nothing Then
                        If dr.HasRows Then
                            dr.Read()
                            With Me
                                'Location
                                .SiteID = DataClean(dr.Item("Siteid"))
                                .BusinessUnit = DataClean(dr.Item("risuperarea_fmt")) '& " - " & DataClean(dr.Item("SubArea"))
                                .Area = DataClean(dr.Item("SubArea"))
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

                                If DataClean(dr.Item("SCHEDDT")) = "Yes" Then
                                    .schedDT = "Yes"
                                Else
                                    .schedDT = "No"
                                End If

                                ''Incident Type
                                'If DataClean(dr.Item("ORIGINATINGAPPLICATION")).Length > 0 Then
                                '    If DataClean(dr.Item("ORIGINATINGAPPLICATION")).ToLower = "simpledt" Then
                                '        .SimpleDT = "Yes"
                                '    Else
                                '        .SimpleDT = "No"
                                '    End If
                                'Else
                                '    .SimpleDT = "No"
                                'End If

                                .Recordable = DataClean(dr.Item("Recordable"))
                                .RCFA = DataClean(dr.Item("RCFA"))

                                'Incdent Classification
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


                                'Creation
                                .CreatedBy = GetUserName(DataClean(dr.Item("username")))
                                .CreationDate = CleanDate(dr.Item("recorddate"), DateFormat.ShortDate)
                                .LastUpdatedBy = GetUserName(DataClean(dr.Item("updateusername")))
                                .LastUpdatedDate = CleanDate(dr.Item("updatedate"), DateFormat.ShortDate)

                            End With
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
    Private Function GetUserName(ByVal user As String) As String
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

            GetUserName = ret
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