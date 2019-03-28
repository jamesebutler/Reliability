Imports System.Data
Imports Devart.Data.Oracle
Imports RI

'6/30/08 - No Need to localize the Franklin CMMS Pareto Reports
'09/16/13 - Removed CMMS Pareto charts - Now available on Maintenance Dashboard
Partial Class ucSiteDropdowns
    Inherits System.Web.UI.UserControl
    <Serializable()> _
    Friend Class SiteCriteria

        Public Sub New()
            Facility = String.Empty
            InActiveFlag = "N"
            Division = String.Empty
            BusType = "All"
            Key = ""
        End Sub

        Private mFacility As String
        Private mInActiveFlag As String
        Private mDivision As String
        Private mBusType As String
        Private mKey As String

        Public Property Facility() As String
            Get
                Return mFacility
            End Get
            Set(ByVal value As String)
                mFacility = value
            End Set
        End Property

        Public Property BusType() As String
            Get
                Return mBusType
            End Get
            Set(ByVal value As String)
                mBusType = value
            End Set
        End Property
        Public Property InActiveFlag() As String
            Get
                Return mInActiveFlag
            End Get
            Set(ByVal value As String)
                mInActiveFlag = value
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
        Public Property Key() As String
            Get
                Return mKey
            End Get
            Set(ByVal value As String)
                mKey = value
            End Set
        End Property
    End Class
    <Serializable()> _
    Friend Class ReportCriteria

        Public Sub New()

        End Sub

    End Class
    'Public Const DateRange As String = "DATE RANGE"
    'Public Const EstimatedDueDate As String = "ESTIMATED DUE DATE"
    'Public Const IncidentStatus As String = "INCIDENT STATUS"
    'Public Const IncidentType As String = "INCIDENT TYPE"
    'Public Const Person As String = "PERSON"
    'Public Const RCFAStatus As String = "RCFA STATUS"
    Private mReportTitle As String
    Private mReportSortValue As String
    Private mReportSortText As String
    Private mReportName As String
    Private mReportInactiveFlag As String

    Dim _rblIncidentStatus As New RadioButtonList
    'Dim _ddlPerson As New DropDownList
    'Dim _cddlPerson As New DropDownList
    Dim _cblDateRange As New CheckBoxList
    Dim _cblIncidentType As New CheckBoxList
    'Dim _cblConstrainedArea As New CheckBoxList
    Dim _rblDateRange As New RadioButtonList
    Dim _rblActionItems As New RadioButtonList
    Dim _rblRCFAStatus As New RadioButtonList
    Dim _rblChartType As New RadioButtonList
    Dim _rblReportType As New RadioButtonList
    Dim _rblChronicType As New RadioButtonList
    Dim _rblDownTime As New RadioButtonList
    Dim _rblReasonLevel As New RadioButtonList
    Dim _rblRTSIncidentType As New RadioButtonList
    Dim _rblPPRIncidentType As New RadioButtonList
    Dim _rblSRRIncidentType As New RadioButtonList
    Dim _rblMonthlyReport As New RadioButtonList
    Dim _ifrDownTime As New Web.UI.WebControls.Panel
    Dim _ddlCMMSFacility As New DropDownList
    Dim _ddlCMMSFranklinReports As New DropDownList
    Dim _ddlCMMSFranklinArea As New DropDownList
    Dim _rblCMMSFranklinTime As New RadioButtonList
    Dim _ddlPPRFacility As New DropDownList
    Dim _txtParetoNumbers As New TextBox
    
    Dim _ddlFacility As New DropDownList
    Dim _ddlDivision As New DropDownList
    Dim _ddlLineBreak As New DropDownList
    Dim _ddlLine As New DropDownList
    Dim _ddlBusinessUnit As New DropDownList
    Dim _ddlArea As New DropDownList

    Dim ipLoc As New IP.Bids.Localization.WebLocalization()

    Structure ReportParameterType

        Const ActionItemStatus As String = "Action Item Status"
        Const ActionItemsListed As String = "Action Items Listed"
        Const Calendar As String = "Calendar"
        Const ChartType As String = "Chart Type"
        Const DateRange As String = "Date Range"
        Const Division As String = "Division"
        Const EstimatedDueDate As String = "Estimated Due Date"
        Const Facility As String = "Facility"
        Const IncidentType As String = "Incident Type"
        Const Person As String = "Person"
        Const RCFAStatus As String = "RCFA Status"
        Const Site As String = "Site"
        Const ReportType As String = "Report Type"
        Const ChronicType As String = "Chronic Type"
        Const DownTime As String = "Downtime"
        Const ReasonLevel As String = "Reason Level"
        Const RTSIncidentType As String = "RTS Incident Type"
        Const PPRIncidentType As String = "PPR Incident Type"
        Const SRRIncidentType As String = "SRR Incident Type"
        Const ConstrainedArea As String = "Constrained Area"
        Const MonthlyReport As String = "Monthly Report"
        Const PaperDowntime As String = "Paper Downtime"
        Const CMMSFacility As String = "CMMSFacility"
        Const CMMSReports As String = "CMMSReports"
        Const PPRMills As String = "PPR Mills"
        Const TaskActionItems As String = "Task Item Listing"
        Private Active As Boolean
    End Structure

    Structure RequiredReportParameters
        Dim ActionItemStatus As Boolean
        Dim ActionItemsListed As Boolean
        Dim Calendar As Boolean
        Dim ChartType As Boolean
        Dim DateRange As Boolean
        Dim Division As Boolean
        Dim EstimatedDueDate As Boolean
        Dim Facility As Boolean
        Dim IncidentType As Boolean
        Dim Person As Boolean
        Dim RCFAStatus As Boolean
        Dim Site As Boolean
        Dim ReportType As Boolean
        Dim ChronicType As Boolean
        Dim DownTime As Boolean
        Dim ReasonLevel As Boolean
        Dim RTSIncidentType As Boolean
        Dim PPRIncidentType As Boolean
        Dim SRRIncidentType As Boolean
        Dim ConstrainedArea As Boolean
        Dim MonthlyReport As Boolean
        Dim PaperDownTime As Boolean
        Dim CMMSFacility As Boolean
        Dim CMMSFranklin As Boolean
        Dim PPRMills As Boolean       
        Dim PPRReasons As Boolean
        Dim TaskActionItems As Boolean
    End Structure

    Structure ReportTitles
        Const ActionItems As String = "Action Items"
        Const IncidentListing As String = "Incident Listing"
        Const IncidentListingChartsByGroup As String = "Incident Listing Charts by Group"
        Const InvestigationExecutiveSummary As String = "Investigation Executive Summary"
        Const ManagementSummary As String = "Management Summary"
        Const ParetoChartsCMMS As String = "Pareto Charts - CMMS"
        Const ParetoChartsIncidents As String = "Pareto Charts - Incidents"
        Const PCF As String = "PCF Certified Kill"
        Const RRandRCFAIncidentCompleteness As String = "RR and RCFA Incident Completeness"
        Const ReliabilityIndex As String = "Reliability Index"
        Const ReliabilityReportingScorecard As String = "Reliability Reporting Scorecard"
        Const ReliabilityTrackingSystem As String = "Reliability Tracking System (RTS)"
        Const EnterpriseSummaryReporting As String = "Enterprise Summary Reporting"
        Const PaperDowntime As String = "Paper Downtime"
        Const RCFACommitmentStatus As String = "RCFA Commitment Status"
        Const AvailabilityMonthlySummary As String = "Availability Monthly Summary"
        Private Active As Boolean
    End Structure


    Public Property ReportTitle() As String
        Get
            If mReportTitle Is Nothing Then
                mReportTitle = String.Empty
            End If
            Return mReportTitle

        End Get
        Set(ByVal value As String)
            mReportTitle = value
        End Set
    End Property

    Public Property ReportSortValue() As String
        Get
            Return mReportSortValue
        End Get
        Set(ByVal value As String)
            mReportSortValue = value
        End Set
    End Property
    Public Property ReportSortText() As String
        Get
            Return mReportSortText
        End Get
        Set(ByVal value As String)
            mReportSortText = value
        End Set
    End Property

    Public Property Division() As String
        Get
            'Return _ddlDivision.SelectedValue
            Return _ucSiteLocation.DivisionValue
        End Get
        Set(ByVal value As String)
            If _ucSiteLocation.DivisionValue IsNot Nothing Then '._ddlDivision.Items.FindByValue(value) IsNot Nothing Then
                _ucSiteLocation.DivisionValue = value
                'Else
                '   _ucSiteLocation._ddlDivision.SelectedIndex = -1
            End If
        End Set
    End Property
    Public Property Facility() As String
        Get
            Return _ddlFacility.SelectedValue
        End Get
        Set(ByVal value As String)
            If _ddlFacility.Items.FindByValue(value) IsNot Nothing Then
                _ddlFacility.SelectedValue = value
            Else
                _ddlFacility.SelectedIndex = -1
            End If
        End Set
    End Property
    Public Property BusinessUnit() As String
        Get
            Return _ddlBusinessUnit.SelectedValue
        End Get
        Set(ByVal value As String)
            If _ddlBusinessUnit.Items.FindByValue(value) IsNot Nothing Then
                _ddlBusinessUnit.SelectedValue = value
            Else
                _ddlBusinessUnit.SelectedIndex = -1
            End If
        End Set
    End Property
    Public Property Area() As String
        Get
            Return _ddlArea.SelectedValue
        End Get
        Set(ByVal value As String)
            If _ddlArea.Items.FindByValue(value) IsNot Nothing Then
                _ddlArea.SelectedValue = value
            Else
                _ddlArea.SelectedIndex = -1
            End If
        End Set
    End Property
    Public Property Line() As String
        Get
            Return _ddlLine.SelectedValue
        End Get
        Set(ByVal value As String)
            If _ddlLine.Items.FindByValue(value) IsNot Nothing Then
                _ddlLine.SelectedValue = value
            Else
                _ddlLine.SelectedIndex = -1
            End If
        End Set
    End Property
    Public Property LineBreak() As String
        Get
            Return _ddlLineBreak.SelectedValue
        End Get
        Set(ByVal value As String)
            If _ddlLineBreak.Items.FindByValue(value) IsNot Nothing Then
                _ddlLineBreak.SelectedValue = value
            Else
                _ddlLineBreak.SelectedIndex = -1
            End If
        End Set
    End Property
    Public Property ReportInactiveFlag() As String
        Get
            Return mReportInactiveFlag
        End Get
        Set(ByVal value As String)
            mReportInactiveFlag = value
            If mReportInactiveFlag = "N" Then
                Me._btnSubmit.Enabled = True
            Else
                Me._btnSubmit.Enabled = False
            End If
        End Set
    End Property
    Public Function Convert_Date(ByVal strDate As String) As Date
        'Date.TryParseExact(strDate, "MM/DD/YYYY", Nothing, Globalization.DateTimeStyles.None, dtsdate)
        Dim dtsdate() As String = strDate.Split("/")
        If dtsdate.Length = 3 Then
            Return New Date(dtsdate(2), dtsdate(0), dtsdate(1))
        Else
            Return Nothing
        End If
        'Return Date.ParseExact(strDate, "MM/DD/YYYY", Nothing)
    End Function

    Private Function GetPersonDropDownDSFromPackage(ByVal criteria As SiteCriteria) As DataSet       
        Dim conCust As Devart.Data.Oracle.OracleConnection = Nothing
        Dim cmdSQL As OracleCommand = Nothing
        Dim connection As String = String.Empty
        Dim provider As String = String.Empty
        Dim ds As DataSet = Nothing
        Dim daData As OracleDataAdapter = Nothing
        Dim cnConnection As OracleConnection = Nothing
        Dim key As String

        Try
            key = SharedFunctions.CreateKey(criteria)
            key = criteria.Key & "_Person_" & key
            If Cache.Item(key) IsNot Nothing Then
                ds = CType(Cache.Item(key), DataSet)
                Exit Try
            End If
            If connection.Length = 0 Then
                connection = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
            End If
            If provider.Length = 0 Then
                provider = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ProviderName
            End If
            cmdSQL = New OracleCommand
            With cmdSQL
                cnConnection = New OracleConnection(connection)
                cnConnection.Open()
                .Connection = cnConnection
                .CommandText = "ri.personddl"
                .CommandType = CommandType.StoredProcedure
                Dim param As New OracleParameter

                param.ParameterName = "in_siteid"
                param.OracleDbType = OracleDbType.VarChar
                param.Value = criteria.Facility
                param.Direction = ParameterDirection.Input
                .Parameters.Add(param)

                param = New OracleParameter
                param.ParameterName = "rsPerson"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = ParameterDirection.Output
                .Parameters.Add(param)
            End With

            ds = New DataSet()
            ds.EnforceConstraints = False
            daData = New OracleDataAdapter(cmdSQL)
            daData.Fill(ds)
            ds.EnforceConstraints = True
            If ds IsNot Nothing Then Cache.Insert(key, ds, Nothing, DateTime.Now.AddHours(6), TimeSpan.Zero)
        Catch ex As Exception
            Return Nothing
            Throw New DataException("GetPersonDropDownDSFromPackage", ex)
            If Not conCust Is Nothing Then conCust = Nothing
        Finally
            GetPersonDropDownDSFromPackage = ds
            If Not daData Is Nothing Then daData = Nothing
            If Not ds Is Nothing Then ds = Nothing
            If Not cmdSQL Is Nothing Then cmdSQL = Nothing
            If cnConnection IsNot Nothing Then
                If cnConnection.State = ConnectionState.Open Then cnConnection.Close()
                cnConnection = Nothing
            End If
        End Try
    End Function
    Private Function GetReportParametersDSFromPackage() As DataSet
        Dim conCust As Devart.Data.Oracle.OracleConnection = Nothing
        Dim cmdSQL As OracleCommand = Nothing
        Dim connection As String = String.Empty
        Dim provider As String = String.Empty
        Dim ds As DataSet = Nothing
        Dim daData As OracleDataAdapter = Nothing
        Dim cnConnection As OracleConnection = Nothing
        Dim key As String

        Try
            key = "RI_REPORTPARAMETERS"
            If Cache.Item(key) IsNot Nothing Then
                ds = CType(Cache.Item(key), DataSet)
                Exit Try
            End If
            If connection.Length = 0 Then
                connection = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
            End If
            If provider.Length = 0 Then
                provider = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ProviderName
            End If
            cmdSQL = New OracleCommand
            With cmdSQL
                cnConnection = New OracleConnection(connection)
                cnConnection.Open()
                .Connection = cnConnection
                .CommandText = "ri.ReportParameters"
                .CommandType = CommandType.StoredProcedure
                Dim param As New OracleParameter

                param = New OracleParameter
                param.ParameterName = "rsReportParameters"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = ParameterDirection.Output
                .Parameters.Add(param)
            End With

            ds = New DataSet()
            ds.EnforceConstraints = False
            daData = New OracleDataAdapter(cmdSQL)
            daData.Fill(ds)
            ds.EnforceConstraints = True
            If ds IsNot Nothing Then Cache.Insert(key, ds, Nothing, DateTime.Now.AddHours(6), TimeSpan.Zero)
        Catch ex As Exception
            Return Nothing
            Throw New DataException("GetReportParametersDSFromPackage", ex)
            If Not conCust Is Nothing Then conCust = Nothing
        Finally
            GetReportParametersDSFromPackage = ds
            If Not daData Is Nothing Then daData = Nothing
            If Not ds Is Nothing Then ds = Nothing
            If Not cmdSQL Is Nothing Then cmdSQL = Nothing
            If cnConnection IsNot Nothing Then
                If cnConnection.State = ConnectionState.Open Then cnConnection.Close()
                cnConnection = Nothing
            End If
        End Try
    End Function
    Private Function GetDropDownDSFromPackage(ByVal criteria As SiteCriteria) As DataSet
        Dim conCust As Devart.Data.Oracle.OracleConnection = Nothing
        Dim cmdSQL As OracleCommand = Nothing
        Dim connection As String = String.Empty
        Dim provider As String = String.Empty
        Dim ds As DataSet = Nothing
        Dim daData As OracleDataAdapter = Nothing
        Dim cnConnection As OracleConnection = Nothing
        Dim key As String
        Dim Rowid As String = ""
        Try
            key = SharedFunctions.CreateKey(criteria)
            key = criteria.Key & "_" & key
            If Cache.Item(key) IsNot Nothing Then
                ds = CType(Cache.Item(key), DataSet)
                Exit Try
            End If
            If connection.Length = 0 Then
                connection = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
            End If
            If provider.Length = 0 Then
                provider = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ProviderName
            End If
            cmdSQL = New OracleCommand
            With cmdSQL
                cnConnection = New OracleConnection(connection)
                cnConnection.Open()
                .Connection = cnConnection
                .CommandText = "RI.ReportDDL"
                .CommandType = CommandType.StoredProcedure
                Dim param As New OracleParameter

                param.ParameterName = "in_siteid"
                param.OracleDbType = OracleDbType.VarChar
                param.Value = criteria.Facility
                param.Direction = ParameterDirection.Input
                .Parameters.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_inactiveflag"
                param.OracleDbType = OracleDbType.VarChar
                param.Value = criteria.InActiveFlag
                param.Direction = ParameterDirection.Input
                .Parameters.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_Division"
                param.OracleDbType = OracleDbType.VarChar
                param.Value = criteria.Division
                param.Direction = ParameterDirection.Input
                .Parameters.Add(param)

                param = New OracleParameter
                param.ParameterName = "rsDivision"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = ParameterDirection.Output
                .Parameters.Add(param)

                param = New OracleParameter
                param.ParameterName = "rsFacility"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = ParameterDirection.Output
                .Parameters.Add(param)

                param = New OracleParameter
                param.ParameterName = "rsBusinessUnit"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = ParameterDirection.Output
                .Parameters.Add(param)

                param = New OracleParameter
                param.ParameterName = "rsArea"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = ParameterDirection.Output
                .Parameters.Add(param)

                param = New OracleParameter
                param.ParameterName = "rsLine"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = ParameterDirection.Output
                .Parameters.Add(param)

                param = New OracleParameter
                param.ParameterName = "rsLinebreak"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = ParameterDirection.Output
                .Parameters.Add(param)

                param = New OracleParameter
                param.ParameterName = "rsPerson"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = ParameterDirection.Output
                .Parameters.Add(param)
            End With

            ds = New DataSet()
            ds.EnforceConstraints = False
            daData = New OracleDataAdapter(cmdSQL)
            daData.Fill(ds)
            ds.EnforceConstraints = True
            If ds IsNot Nothing Then Cache.Insert(key, ds, Nothing, DateTime.Now.AddHours(6), TimeSpan.Zero)
        Catch ex As Exception
            Return Nothing
            Throw New DataException("GetDSFromPackage", ex)
            If Not conCust Is Nothing Then conCust = Nothing
        Finally
            GetDropDownDSFromPackage = ds
            If Not daData Is Nothing Then daData = Nothing
            If Not ds Is Nothing Then ds = Nothing
            If Not cmdSQL Is Nothing Then cmdSQL = Nothing
            If cnConnection IsNot Nothing Then
                If cnConnection.State = ConnectionState.Open Then cnConnection.Close()
                cnConnection = Nothing
            End If
        End Try
    End Function
    Private Sub PopulateControlState()
        If Page.IsPostBack Then
            'Set the SelectedValues to the form values
            'If _ddlFacility.UniqueID IsNot Nothing Then
            '    If Request.Form(_ddlFacility.UniqueID.ToString) IsNot Nothing Then
            '        Facility = Request.Form(_ddlFacility.UniqueID.ToString)
            '        Division = Request.Form(_ddlDivision.UniqueID.ToString)
            '        Area = Request.Form(_ddlArea.UniqueID.ToString)
            '        BusinessUnit = Request.Form(_ddlBusinessUnit.UniqueID.ToString)
            '        Line = Request.Form(_ddlLine.UniqueID.ToString)
            '        LineBreak = Request.Form(_ddlLineBreak.UniqueID.ToString)
            '    End If
            'End If
            'If _ucSiteLocation.FacilityValue IsNot Nothing Then
            '    Facility = _ucSiteLocation.FacilityValue
            '    Division = _ucSiteLocation.DivisionValue
            '    Area = _ucSiteLocation.AreaValue
            '    BusinessUnit = _ucSiteLocation.BusinessUnitValue
            '    Line = _ucSiteLocation.LineValue
            '    LineBreak = _ucSiteLocation.LineBreakValue
            'End If
        End If
        'End If
    End Sub
    Private Function PopulatePerson(ByVal criteria As SiteCriteria) As DataSet
        Dim ds As DataSet = Nothing
        Dim savedXML As String = String.Empty
        Dim io As New System.IO.StringWriter
        Try
            'savedXML = SharedFunctions.GetDataFromSQLServerCache("Person_" & criteria.Facility, 1440)

            'If savedXML.Length = 0 Then
            '    ds = GetPersonDropDownDSFromPackage(criteria)
            '    If ds IsNot Nothing Then
            '        io = New System.IO.StringWriter
            '        ds.WriteXml(io, XmlWriteMode.WriteSchema)
            '        SharedFunctions.StoreDataIntoSQLServerCache("Person_" & criteria.Facility, io.ToString)
            '    End If
            'Else
            'use cached xml
            ds = New DataSet
            ds.EnforceConstraints = False
            ds.ReadXml(New System.IO.StringReader(savedXML))
            'End If

        Catch ex As Exception
        Finally
            PopulatePerson = ds
            If Not ds Is Nothing Then ds = Nothing

        End Try
    End Function
    Public Sub PopulateDropdowns()
        Dim criteria As New SiteCriteria
        Dim ds As DataSet = Nothing
        'Dim savedXML As String = String.Empty
        'Dim io As New System.IO.StringWriter
        'Dim User As System.Security.Principal.IPrincipal
        'User = System.Web.HttpContext.Current.User

        'Dim username As String
        'username = CurrentUserProfile.GetCurrentUser

        'Dim userProfile As CurrentUserProfile = CType(Session.Item(Replace(username, "\", "_")), CurrentUserProfile)
        Try
            'If userProfile IsNot Nothing Then
            '_ucSiteLocation.FacilityValue = userProfile.DefaultFacility
            '_ucSiteLocation.DivisionValue = userProfile.DefaultDivision
            'criteria.Facility = userProfile.DefaultFacility
            'criteria.Division = userProfile.DefaultDivision
            'criteria.InActiveFlag = userProfile.InActiveFlag
            'criteria.BusType = userProfile.BusType
            'criteria.Key = "SiteCriteria"
            'Session.Add("SiteCriteria", criteria)
            'End If
            'If _ddlFacility.UniqueID IsNot Nothing Then
            '    If Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_ddlFacility") IsNot Nothing Then
            '        criteria.Facility = Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_ddlFacility")
            '    End If
            'End If
            'If _ddlDivision.UniqueID IsNot Nothing Then
            '    If Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_ddlDivision") IsNot Nothing Then
            '        criteria.Division = Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_ddlDivision")
            '    End If
            'End If

            PopulateControlState()
            PopulateReportParameters(criteria)
            'Me._upSite.Update()
        Catch ex As Exception
            Throw
        Finally
            If Not ds Is Nothing Then ds = Nothing
        End Try
    End Sub
    Public Sub PopulateSiteDropdown()
        Dim criteria As New SiteCriteria
        'Dim ds As DataSet = Nothing
        'Dim savedXML As String = String.Empty
        'Dim io As New System.IO.StringWriter                

        Dim username As String
        username = CurrentUserProfile.GetCurrentUser
        Dim userProfile As RI.CurrentUserProfile = Nothing
        userProfile = RI.SharedFunctions.GetUserProfile

        'Dim userProfile As CurrentUserProfile
        'Dim userProfile As CurrentUserProfile = CType(Session.Item(Replace(username, "\", "_")), CurrentUserProfile)
        If Session.Item(Replace(username, "\", "_")) IsNot Nothing Then userProfile = CType(Session.Item(Replace(username, "\", "_")), CurrentUserProfile)

        Try
            'Me._ddlFacility.ID = "_ddlFacility"
            ''Me._ddlDivision.ID = "_ddlDivision"
            'Me._ddlDivision.EnableViewState = False
            'Me._ddlArea.ID = "_ddlArea"
            'Me._ddlBusinessUnit.ID = "_ddlBusinessUnit"
            'Me._ddlLineBreak.ID = "_ddlLineBreak"
            'Me._ddlLine.ID = "_ddlLine"

            'This code is used to populate with the users default facility when a report
            'is first selected.
            If _ucSiteLocation.FacilityValue = "" Then
                If userProfile IsNot Nothing Then
                    'criteria.Facility = userProfile.DefaultFacility
                    'criteria.Division = userProfile.DefaultDivision
                    'criteria.InActiveFlag = userProfile.InActiveFlag
                    'criteria.BusType = userProfile.BusType
                    _ucSiteLocation.FacilityValue = userProfile.DefaultFacility
                    _ucSiteLocation.DivisionValue = userProfile.DefaultDivision
                    _ucSiteLocation.BusinessUnitValue = "All"
                    _ucSiteLocation.AreaValue = "All"
                    _ucSiteLocation.LineValue = "All"
                    _ucSiteLocation.LineBreakValue = "All"
                Else
                    _ucSiteLocation.FacilityValue = "AL"
                    _ucSiteLocation.DivisionValue = "All"
                    _ucSiteLocation.BusinessUnitValue = "All"
                    _ucSiteLocation.AreaValue = "All"
                    _ucSiteLocation.LineValue = "All"
                    _ucSiteLocation.LineBreakValue = "All"
                    'criteria.Facility = "AL"
                    'criteria.Division = "All"
                    'criteria.InActiveFlag = "N"
                    'criteria.BusType = "PM"
                End If
            End If
            'Commented out by Cathy Cox - data retrieved in cascadinglists

            'criteria.Key = "SiteCriteria"
            'If Me._ddlFacility.UniqueID IsNot Nothing Then
            '    If Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_ddlFacility") IsNot Nothing Then
            '        criteria.Facility = Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_ddlFacility")
            '    End If
            'End If
            'If Me._ddlDivision.UniqueID IsNot Nothing Then
            '    If Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_ddlDivision") IsNot Nothing Then
            '        criteria.Division = Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_ddlDivision")
            '    End If
            'End If

            '' ds = GetDropDownDSFromPackage(criteria)

            ''If savedXML.Length = 0 Then
            ''    ds = GetDropDownDSFromPackage(criteria)
            ''    If ds IsNot Nothing Then
            ''        ds.WriteXml(io, XmlWriteMode.WriteSchema)
            ''        SharedFunctions.StoreDataIntoSQLServerCache(criteria, io.ToString)
            ''    End If
            ''Else
            ''    'use cached xml
            ''    ds = New DataSet
            ''    ds.EnforceConstraints = False
            ''    ds.ReadXml(New System.IO.StringReader(savedXML))
            ''End If
            'If ds IsNot Nothing Then
            '    If ds.Tables.Count >= 5 Then
            '        'Division
            '        _ddlDivision.DataSource = ds.Tables(0).CreateDataReader
            '        _ddlDivision.Items.Clear()
            '        _ddlDivision.DataTextField = "Division"
            '        _ddlDivision.DataValueField = "Division"
            '        _ddlDivision.DataBind()
            '        _ddlDivision.SelectedIndex = 0

            '        'Facility
            '        _ddlFacility.DataSource = ds.Tables(1).CreateDataReader
            '        _ddlFacility.Items.Clear()
            '        _ddlFacility.DataTextField = "SiteName"
            '        _ddlFacility.DataValueField = "SiteID"
            '        _ddlFacility.DataBind()

            '        If _ddlFacility.Items.FindByValue(criteria.Facility) IsNot Nothing Then
            '            _ddlFacility.SelectedValue = criteria.Facility
            '            Dim divisionValue As String = ds.Tables(1).DefaultView.Item(_ddlFacility.SelectedIndex).Item("Division").ToString
            '            If _ddlFacility.SelectedValue = "AL" Then
            '                If Request.Form(_ddlDivision.UniqueID.ToString) IsNot Nothing Then
            '                    divisionValue = Request.Form(_ddlDivision.UniqueID.ToString)
            '                End If
            '            End If
            '            If _ddlDivision.Items.FindByValue(divisionValue) IsNot Nothing Then
            '                _ddlDivision.SelectedIndex = 0
            '                _ddlDivision.SelectedValue = divisionValue
            '                criteria.Division = divisionValue
            '            End If

            '        End If

            '        'Business Unit
            '        _ddlBusinessUnit.ClearSelection()
            '        _ddlBusinessUnit.DataSource = ds.Tables(2).CreateDataReader
            '        _ddlBusinessUnit.DataTextField = "RISuperArea"
            '        _ddlBusinessUnit.DataValueField = "RISuperArea"
            '        _ddlBusinessUnit.Items.Clear()
            '        _ddlBusinessUnit.DataBind()
            '        _ddlBusinessUnit.Items.Insert(0, "")
            '        _ddlBusinessUnit.SelectedIndex = 0

            '        'Area
            '        _ddlArea.DataSource = ds.Tables(3).CreateDataReader

            '        _ddlArea.DataTextField = "SubArea"
            '        _ddlArea.DataValueField = "SubArea"
            '        _ddlArea.Items.Clear()
            '        _ddlArea.DataBind()
            '        _ddlArea.Items.Insert(0, "")

            '        'Line Systems        
            '        _ddlLine.DataSource = ds.Tables(4).CreateDataReader
            '        _ddlLine.Items.Clear()
            '        _ddlLine.DataTextField = "Area"
            '        _ddlLine.DataValueField = "Area"
            '        _ddlLine.DataBind()
            '        _ddlLine.Items.Insert(0, "")
            '        _ddlLine.SelectedIndex = 0

            '        'Line Break
            '        _ddlLineBreak.DataSource = ds.Tables(5).CreateDataReader
            '        _ddlLineBreak.Items.Clear()
            '        _ddlLineBreak.DataTextField = "LineBreak"
            '        _ddlLineBreak.DataValueField = "LineBreak"
            '        _ddlLineBreak.DataBind()
            '        _ddlLineBreak.Items.Insert(0, "")
            '        _ddlLineBreak.SelectedIndex = 0


            '        _ddlPerson.ID = "_ddlPerson"
            '        _ddlPerson.DataSource = ds.Tables(6).CreateDataReader
            '        _ddlPerson.EnableViewState = True
            '        _ddlPerson.Items.Clear()
            '        _ddlPerson.DataTextField = "Person"
            '        _ddlPerson.DataValueField = "UserName"
            '        _ddlPerson.DataBind()
            '        _ddlPerson.Items.Insert(0, "All")
            '        _ddlPerson.SelectedIndex = 0
            '        'ViewState.Add("Person", ds.Tables(6))
            '    End If
            'End If
        Catch ex As Exception
            Throw
        Finally
            'If Not ds Is Nothing Then ds = Nothing
        End Try
    End Sub

    Private Sub PopulateReportParameters(ByVal criteria As SiteCriteria)
        Dim ds As DataSet = Nothing
        Dim rowFilter As String = "REPORTPARAMETERTYPE = '{0}' and REPORTTITLE='{1}'"
        Dim requiredParms As New RequiredReportParameters
        Dim io As New System.IO.StringWriter
        Dim trigger As New AsyncPostBackTrigger

        Dim ipLoc As New IP.Bids.Localization.WebLocalization()

        Try
            ds = Me.GetReportParametersDSFromPackage

            If ds IsNot Nothing Then
                ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.Site, Me.ReportTitle)
                ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
                'Incident Status or Action Item Listing
                If ds.Tables(0).DefaultView.Count > 0 Then
                    requiredParms.Site = True
                    'Me._ddlFacility.SkinID = "SiteDropDown"
                    'Me._ddlFacility.ID = "_ddlFacility"
                    '_ddlFacility.AutoPostBack = True
                    'AddHandler _ddlFacility.SelectedIndexChanged, AddressOf _ddlFacility_SelectedIndexChanged

                    ''trigger.ControlID = Me._ddlFacility.ID
                    ''trigger.EventName = "SelectedIndexChanged"
                    ''_upSite.Triggers.Add(trigger)

                    ''Me._ddlDivision.SkinID = "SiteDropDown"
                    ''Me._ddlDivision.ID = "_ddlDivision"                    
                    'Me._ddlBusinessUnit.SkinID = "SiteDropDown"
                    'Me._ddlBusinessUnit.ID = "_ddlBusinessUnit"
                    'Me._ddlArea.SkinID = "SiteDropDown"
                    'Me._ddlArea.ID = "_ddlArea"
                    'Me._ddlLine.SkinID = "SiteDropDown"
                    'Me._ddlLine.ID = "_ddlLine"
                    'Me._ddlLineBreak.SkinID = "SiteDropDown"
                    'Me._ddlLineBreak.ID = "_ddlLineBreak"
                Else
                    requiredParms.Site = False
                End If

                ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.ActionItemStatus, Me.ReportTitle)
                ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
                'Incident Status or Action Item Listing
                If ds.Tables(0).DefaultView.Count > 0 Then
                    requiredParms.ActionItemStatus = True
                    _rblIncidentStatus.ID = "_rblIncidentStatus"
                    _rblIncidentStatus.EnableViewState = False
                    _rblIncidentStatus.AutoPostBack = True
                    AddHandler _rblIncidentStatus.SelectedIndexChanged, AddressOf _rblIncidentStatus_SelectedIndexChanged
                    _rblIncidentStatus.RepeatColumns = 0
                    _rblIncidentStatus.RepeatDirection = RepeatDirection.Horizontal
                    _rblIncidentStatus.RepeatLayout = RepeatLayout.Table
                    _rblIncidentStatus.DataSource = ds.Tables(0).DefaultView
                    _rblIncidentStatus.Items.Clear()
                    _rblIncidentStatus.DataTextField = "REPORTPARAMETERS"
                    _rblIncidentStatus.DataValueField = "REPORTPARAMETERS"
                    _rblIncidentStatus.DataBind()
                    ipLoc.LocalizeListControl(_rblIncidentStatus)
                End If

                'Removed Person from report selection (CAC - 02/27/2012)

                'Person
                _ddlPerson.Visible = False

                'ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.Person, Me.ReportTitle)
                'ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
                'If ds.Tables(0).DefaultView.Count > 0 Then
                '    requiredParms.Person = True
                '    _ddlPerson.Visible = True
                'End If
            Else
                _ddlPerson.Visible = False
            End If

            'Estimated Due Date
            ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.EstimatedDueDate, Me.ReportTitle)
            ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
            If ds.Tables(0).DefaultView.Count > 0 Then
                requiredParms.EstimatedDueDate = True
                _cblDateRange.ID = "_cblDateRange"
                _cblDateRange.AutoPostBack = False
                '_cblDateRange_SelectedIndexChanged
                'AddHandler _cblDateRange.SelectedIndexChanged, AddressOf _cblDateRange_SelectedIndexChanged
                _cblDateRange.RepeatDirection = RepeatDirection.Horizontal
                _cblDateRange.RepeatLayout = RepeatLayout.Table
                _cblDateRange.DataSource = ds.Tables(0).DefaultView
                _cblDateRange.DataTextField = "REPORTPARAMETERS"
                _cblDateRange.DataValueField = "REPORTPARAMETERS"
                _cblDateRange.DataBind()
                _cblDateRange.Attributes.Add("onClick", Me.AddClientScript(ClientControls.EstimatedDueDate))
                ipLoc.LocalizeListControl(_cblDateRange)
            End If

            'Chart Type                
            ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.ChartType, Me.ReportTitle)
            ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
            'Incident Status or Action Item Listing
            If ds.Tables(0).DefaultView.Count > 0 Then
                requiredParms.ChartType = True
                _rblChartType.ID = "_rblChartType"
                _rblChartType.AutoPostBack = False
                AddHandler _rblChartType.SelectedIndexChanged, AddressOf _rblChartType_SelectedIndexChanged
                _rblChartType.RepeatColumns = 5
                _rblChartType.RepeatDirection = RepeatDirection.Vertical
                _rblChartType.RepeatLayout = RepeatLayout.Table
                _rblChartType.DataSource = ds.Tables(0).DefaultView
                _rblChartType.Items.Clear()
                _rblChartType.DataTextField = "REPORTPARAMETERS"
                _rblChartType.DataValueField = "REPORTPARAMETERS"
                _rblChartType.DataBind()
                ipLoc.LocalizeListControl(_rblChartType)
                'If Request.Form(_rblChartType.UniqueID.ToString) IsNot Nothing Then
                '    _rblChartType.SelectedValue = Request.Form(_rblChartType.UniqueID.ToString)
                'End If
                'If _rblChartType.Items.FindByValue("Human Root Cause") IsNot Nothing Then
                '    _rblChartType.Items.FindByValue("Human Root Cause").Enabled = False
                'End If
            End If

            'DownTime                
            ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.DownTime, Me.ReportTitle)
            ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
            'Incident Status or Action Item Listing
            If ds.Tables(0).DefaultView.Count > 0 Then
                requiredParms.DownTime = True
                _rblDownTime.ID = "_rblDownTime"
                _rblDownTime.AutoPostBack = False
                'AddHandler _rblChartType.SelectedIndexChanged, AddressOf _rblChartType_SelectedIndexChanged
                _rblDownTime.RepeatColumns = 1
                _rblDownTime.RepeatDirection = RepeatDirection.Vertical
                _rblDownTime.RepeatLayout = RepeatLayout.Table
                _rblDownTime.DataSource = ds.Tables(0).DefaultView
                _rblDownTime.Items.Clear()
                _rblDownTime.DataTextField = "REPORTPARAMETERS"
                _rblDownTime.DataValueField = "REPORTPARAMETERS"
                _rblDownTime.DataBind()
                ipLoc.LocalizeListControl(_rblDownTime)
            End If

            'Reason Level                
            ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.ReasonLevel, Me.ReportTitle)
            ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
            'Incident Status or Action Item Listing
            If ds.Tables(0).DefaultView.Count > 0 Then
                requiredParms.ReasonLevel = True
                _rblReasonLevel.ID = "_rblReasonLevel"
                _rblReasonLevel.AutoPostBack = False
                'AddHandler _rblChartType.SelectedIndexChanged, AddressOf _rblChartType_SelectedIndexChanged
                _rblReasonLevel.RepeatColumns = 2
                _rblReasonLevel.RepeatDirection = RepeatDirection.Vertical
                _rblReasonLevel.RepeatLayout = RepeatLayout.Table
                _rblReasonLevel.DataSource = ds.Tables(0).DefaultView
                _rblReasonLevel.Items.Clear()
                _rblReasonLevel.DataTextField = "REPORTPARAMETERS"
                _rblReasonLevel.DataValueField = "REPORTPARAMETERS"
                _rblReasonLevel.DataBind()
                ipLoc.LocalizeListControl(_rblReasonLevel)
            End If

            'Incident Type
            ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.IncidentType, Me.ReportTitle)
            ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
            If ds.Tables(0).DefaultView.Count > 0 Then
                requiredParms.IncidentType = True
                '_cblIncidentType.ID = "_cblIncidentType"
                'If ReportTitle = ReportTitles.IncidentListing Then
                '    _cblIncidentType.AutoPostBack = True
                'ElseIf ReportTitle = ReportTitles.InvestigationExecutiveSummary Then
                '    _cblIncidentType.AutoPostBack = True
                'Else
                '    _cblIncidentType.AutoPostBack = False 'True       
                'End If
                'AddHandler _cblIncidentType.SelectedIndexChanged, AddressOf _cblIncidentType_SelectedIndexChanged
                '_cblIncidentType.RepeatColumns = 3
                '_cblIncidentType.RepeatDirection = RepeatDirection.Vertical
                '_cblIncidentType.RepeatLayout = RepeatLayout.Table
                '_cblIncidentType.DataSource = ds.Tables(0).DefaultView
                '_cblIncidentType.Items.Clear()
                '_cblIncidentType.DataTextField = "REPORTPARAMETERS"
                '_cblIncidentType.DataValueField = "REPORTPARAMETERS"
                '_cblIncidentType.DataBind()
                '_cblIncidentType.Attributes.Add("onClick", AddClientScript(ClientControls.IncidentType))
            End If

            'RTS Incident Type
            ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.RTSIncidentType, Me.ReportTitle)
            ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
            If ds.Tables(0).DefaultView.Count > 0 Then
                requiredParms.RTSIncidentType = True
                _rblRTSIncidentType.ID = "_rblRTSIncidentType"
                _rblRTSIncidentType.AutoPostBack = False
                'AddHandler _cblIncidentType.SelectedIndexChanged, AddressOf _cblIncidentType_SelectedIndexChanged
                _rblRTSIncidentType.RepeatColumns = 3
                _rblRTSIncidentType.RepeatDirection = RepeatDirection.Vertical
                _rblRTSIncidentType.RepeatLayout = RepeatLayout.Table
                _rblRTSIncidentType.DataSource = ds.Tables(0).DefaultView
                _rblRTSIncidentType.Items.Clear()
                _rblRTSIncidentType.DataTextField = "REPORTPARAMETERS"
                _rblRTSIncidentType.DataValueField = "REPORTPARAMETERS"
                _rblRTSIncidentType.DataBind()
                ipLoc.LocalizeListControl(_rblRTSIncidentType)
            End If

            'Date Range                               
            ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.DateRange, Me.ReportTitle)
            ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
            If ds.Tables(0).DefaultView.Count > 0 Then
                requiredParms.DateRange = True
                _rblDateRange.ID = "_rblDateRange"
                _rblDateRange.AutoPostBack = True
                '_rblDateRange.SelectedIndexChanged = new EventHandler _rblDateRange_SelectedIndexChanged  'Add("SelectedIndexChanged", "_rblDateRange_SelectedIndexChanged")
                AddHandler _rblDateRange.SelectedIndexChanged, AddressOf _rblDateRange_SelectedIndexChanged

                _rblDateRange.RepeatColumns = 3
                _rblDateRange.RepeatDirection = RepeatDirection.Vertical
                _rblDateRange.RepeatLayout = RepeatLayout.Table
                _rblDateRange.DataSource = ds.Tables(0).DefaultView
                _rblDateRange.Items.Clear()
                _rblDateRange.DataTextField = "REPORTPARAMETERS"
                _rblDateRange.DataValueField = "REPORTPARAMETERS"
                _rblDateRange.DataBind()
                _rblDateRange_SelectedIndexChanged(Nothing, Nothing)
                '_rblDateRange.Attributes.Add("onclick", "DateRange_SelectedIndexChanged(" & _rblDateRange.Items.Count.ToString & ",'" & me._ucCalendar.st ");return false;")
                ipLoc.LocalizeListControl(_rblDateRange)
            End If

            '_cblConstrainedArea
            'ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.ConstrainedArea, Me.ReportTitle)
            'ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
            'If ds.Tables(0).DefaultView.Count > 0 Then
            '    requiredParms.ConstrainedArea = True
            '    _cblConstrainedArea.ID = "_cblConstrainedArea"
            '    '_cblConstrainedArea.AutoPostBack = True
            '    ' AddHandler _cblConstrainedArea.SelectedIndexChanged, AddressOf _cblConstrainedArea_SelectedIndexChanged

            '    _cblConstrainedArea.RepeatColumns = 1
            '    _cblConstrainedArea.RepeatDirection = RepeatDirection.Vertical
            '    _cblConstrainedArea.RepeatLayout = RepeatLayout.Table
            '    _cblConstrainedArea.DataSource = ds.Tables(0).DefaultView
            '    _cblConstrainedArea.Items.Clear()
            '    _cblConstrainedArea.DataTextField = "REPORTPARAMETERS"
            '    _cblConstrainedArea.DataValueField = "REPORTPARAMETERS"

            '    _cblConstrainedArea.DataBind()
            '    '_cblConstrainedArea.SelectedValue.ToString("No")
            '    'If Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_cblConstrainedArea") IsNot Nothing Then
            '    '    _cblConstrainedArea.SelectedValue = Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_cblConstrainedArea")
            '    'End If

            '    ipLoc.LocalizeListControl(_cblConstrainedArea)
            'End If


            'Task Action Items
            ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.TaskActionItems, Me.ReportTitle)
            ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
            If ds.Tables(0).DefaultView.Count > 0 Then
                requiredParms.TaskActionItems = True
                Me._ifrDownTime.ID = "_ifrDownTime"
                _ifrDownTime.Visible = True
                _ifrDownTime.BorderWidth = 0
                Dim iframe As New HtmlGenericControl
                Dim url As String
                If ds.Tables(0).DefaultView.Item(0).Item("ReportParameters") IsNot Nothing Then
                    'this needs to be tested - jeb
                    url = ds.Tables(0).DefaultView.Item(0).Item("ReportParameters").ToString & "&sv=" & ReportSortValue

                Else
                    'url = "http://ridev/TaskTracker/ReportSelection.aspx?rn=Task Item Listing&sv=" & ReportSortValue
                    url = "http://gpitasktracker.graphicpkg.com/ReportSelection.aspx?rn=Task Item Listing&sv=" & ReportSortValue
                End If
                iframe.InnerHtml = "<iframe src='" & url & "' border='0' frameborder='0' width='100%' height='600px'/>"
                _ifrDownTime.Controls.Add(iframe)
                _ifrDownTime.CssClass = "iframe"
            End If

            'Action Items
            ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.ActionItemsListed, Me.ReportTitle)
            ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
            If ds.Tables(0).DefaultView.Count > 0 Then
                requiredParms.ActionItemsListed = True
                _rblActionItems.ID = "_rblActionItems"
                If Me.ReportTitle = ReportTitles.ActionItems Then
                    _rblActionItems.AutoPostBack = True
                Else
                    _rblActionItems.AutoPostBack = False
                End If
                AddHandler _rblActionItems.SelectedIndexChanged, AddressOf _rblActionItems_SelectedIndexChanged
                _rblActionItems.RepeatColumns = 3
                _rblActionItems.RepeatDirection = RepeatDirection.Vertical
                _rblActionItems.RepeatLayout = RepeatLayout.Table
                _rblActionItems.DataSource = ds.Tables(0).DefaultView
                _rblActionItems.Items.Clear()
                _rblActionItems.DataTextField = "REPORTPARAMETERS"
                _rblActionItems.DataValueField = "REPORTPARAMETERS"
                _rblActionItems.DataBind()
                ipLoc.LocalizeListControl(_rblActionItems)
            End If

            '_rblReportType
            ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.ReportType, Me.ReportTitle)
            ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
            If ds.Tables(0).DefaultView.Count > 0 Then
                requiredParms.ReportType = True
                _rblReportType.ID = "_rblReportType"
                _rblReportType.AutoPostBack = True
                AddHandler _rblReportType.SelectedIndexChanged, AddressOf _rblreportType_SelectedIndexChanged
                _rblReportType.RepeatColumns = 4
                _rblReportType.RepeatDirection = RepeatDirection.Vertical
                _rblReportType.RepeatLayout = RepeatLayout.Table
                _rblReportType.DataSource = ds.Tables(0).DefaultView
                _rblReportType.Items.Clear()
                _rblReportType.DataTextField = "REPORTPARAMETERS"
                _rblReportType.DataValueField = "REPORTPARAMETERS"
                _rblReportType.DataBind()
                If Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_rblReportType") IsNot Nothing Then
                    _rblReportType.SelectedValue = Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_rblReportType")
                End If
                ipLoc.LocalizeListControl(_rblReportType)
            End If

            '_rblChronicType
            ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.ChronicType, Me.ReportTitle)
            ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
            If ds.Tables(0).DefaultView.Count > 0 Then
                requiredParms.ChronicType = True
                _rblChronicType.ID = "_rblChronicType"
                '_rblChronicType.AutoPostBack = True
                AddHandler _rblChronicType.SelectedIndexChanged, AddressOf _rblChronicType_SelectedIndexChanged
                _rblChronicType.RepeatColumns = 4
                _rblChronicType.RepeatDirection = RepeatDirection.Vertical
                _rblChronicType.RepeatLayout = RepeatLayout.Table
                _rblChronicType.DataSource = ds.Tables(0).DefaultView
                _rblChronicType.Items.Clear()
                _rblChronicType.DataTextField = "REPORTPARAMETERS"
                _rblChronicType.DataValueField = "REPORTPARAMETERS"
                _rblChronicType.DataBind()
                If Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_rblChronicType") IsNot Nothing Then
                    _rblChronicType.SelectedValue = Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_rblChronicType")
                End If
                ipLoc.LocalizeListControl(_rblChronicType)
            End If

            'Facility
            ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.Facility, Me.ReportTitle)
            If ds.Tables(0).DefaultView.Count > 0 Then
                'Me._ddlFacility.Enabled = True
                'Me._ddlFacility.Visible = True

                requiredParms.Facility = True
                '_ddlFacility.AutoPostBack = True
                AddHandler _ddlFacility.SelectedIndexChanged, AddressOf _ddlFacility_SelectedIndexChanged

                'trigger.ControlID = Me._ddlFacility.ID
                'trigger.EventName = "SelectedIndexChanged"
                '_upSite.Triggers.Add(trigger)
            End If

            'division
            ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.Division, Me.ReportTitle)
            If ds.Tables(0).DefaultView.Count > 0 Then
                'Me._ddlDivision.Enabled = True
                'Me._ddlDivision.Visible = True
                'Me._lblDivision.Visible = True
                requiredParms.Division = True
            End If

            'Calendar
            ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.Calendar, Me.ReportTitle)
            If ds.Tables(0).DefaultView.Count > 0 Then
                requiredParms.Calendar = True
            End If

            'Monthly Report
            ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.MonthlyReport, Me.ReportTitle)
            ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
            If ds.Tables(0).DefaultView.Count > 0 Then
                requiredParms.MonthlyReport = True
                _rblMonthlyReport.ID = "_rblMonthlyReport"
                '_rblReportType.AutoPostBack = True
                'AddHandler _rblReportType.SelectedIndexChanged, AddressOf _rblreportType_SelectedIndexChanged
                _rblMonthlyReport.RepeatColumns = 0
                _rblMonthlyReport.RepeatDirection = RepeatDirection.Horizontal
                _rblMonthlyReport.RepeatLayout = RepeatLayout.Table
                _rblMonthlyReport.DataSource = ds.Tables(0).DefaultView
                _rblMonthlyReport.Items.Clear()
                _rblMonthlyReport.DataTextField = "REPORTPARAMETERS"
                _rblMonthlyReport.DataValueField = "REPORTPARAMETERS"
                _rblMonthlyReport.DataBind()
                If Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_rblMonthlyReport") IsNot Nothing Then
                    _rblMonthlyReport.SelectedValue = Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_rblMonthlyReport")
                End If
                ipLoc.LocalizeListControl(_rblMonthlyReport)
            End If

            'RCFA Status
            ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.RCFAStatus, Me.ReportTitle)
            ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
            If ds.Tables(0).DefaultView.Count > 0 Then
                requiredParms.RCFAStatus = True
                _rblRCFAStatus.ID = "_rblRCFAStatus"
                '_rblRCFAStatus.RepeatColumns = 

                _rblRCFAStatus.RepeatLayout = RepeatLayout.Table
                If ReportTitle = "Management Summary" Then
                    _rblRCFAStatus.RepeatColumns = 0
                    _rblRCFAStatus.RepeatDirection = RepeatDirection.Horizontal
                Else
                    _rblRCFAStatus.RepeatColumns = 3
                    _rblRCFAStatus.RepeatDirection = RepeatDirection.Vertical
                End If
                _rblRCFAStatus.DataSource = ds.Tables(0).DefaultView
                _rblRCFAStatus.Items.Clear()
                _rblRCFAStatus.DataTextField = "REPORTPARAMETERS"
                _rblRCFAStatus.DataValueField = "REPORTPARAMETERS"
                _rblRCFAStatus.DataBind()
                _rblRCFAStatus.Visible = True

                ipLoc.LocalizeListControl(_rblRCFAStatus)
            Else
                _rblRCFAStatus.Visible = False
            End If

            'Paper Downtime
            ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.PaperDowntime, Me.ReportTitle)
            ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
            If ds.Tables(0).DefaultView.Count > 0 Then
                requiredParms.PaperDownTime = True
                Me._ifrDownTime.ID = "_ifrDownTime"
                _ifrDownTime.Visible = True
                Dim iframe As New HtmlGenericControl
                Dim url As String
                If ds.Tables(0).DefaultView.Item(0).Item("ReportParameters") IsNot Nothing Then
                    url = ds.Tables(0).DefaultView.Item(0).Item("ReportParameters").ToString
                Else
                    url = "http://millview/MillView/MenuPaper/MenuDowntime/selectionpage.aspx"
                End If
                iframe.InnerHtml = "<iframe src='" & url & "' width='100%' height='600px'/>"
                _ifrDownTime.Controls.Add(iframe)
                _ifrDownTime.CssClass = "iframe"
            End If

            'PPR Mill Machine Selection
            ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.PPRMills, Me.ReportTitle)
            ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
            If ds.Tables(0).DefaultView.Count > 0 Then
                requiredParms.PPRMills = True
                Me.PopulatePPRFacility()
                If _ddlPPRFacility.Items.FindByText(criteria.Facility) IsNot Nothing Then
                    _ddlPPRFacility.Items.FindByText(criteria.Facility).Selected = True
                End If
                If _ddlPPRFacility.SelectedValue.Length > 0 Then
                    _pprMillMachineSelection.PopulateMachines(Me._ddlPPRFacility.SelectedValue)
                End If
            End If

            'CAC 09/16/13 Removed CMMS Pareto charts - Now available on Maintenance Dashboard
            'CMMS Facility
            'ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.CMMSFacility, Me.ReportTitle)
            'ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
            'If ds.Tables(0).DefaultView.Count > 0 Then
            '    requiredParms.CMMSFacility = True
            '    PopulateCMMSFacility()
            '    _ddlCMMSFacility.ID = "_ddlCMMSFacility"
            '    _ddlCMMSFacility.SelectedValue = criteria.Facility
            '    If Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_ddlcmmsFacility") IsNot Nothing Then
            '        'Augusta, Eastover, Franklin, Georgetown, Louisiana, Mansfield, Pineville, Prattville, Riegelwood, Riverdale, Savannah, Terre Haute, Texarkana, Ticonderoga, Vicksburg,
            '        'If Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_ddlcmmsFacility") = "Franklin" Or Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_ddlcmmsFacility") = "FR" Then
            '        '    _pnlHelp.Visible = True
            '        '    PopulateHelp(Insructions.FranklinCMMS)
            '        '    requiredParms.CMMSFranklin = True
            '        'Else
            '        _pnlHelp.Visible = False
            '        'End If

            '    End If
            '    '_rblFacility.RepeatColumns = 8
            '    '_rblFacility.RepeatLayout = RepeatLayout.Table                
            '    '_rblFacility.RepeatDirection = RepeatDirection.Horizontal
            'End If

            'End If
            Session.Add("requiredParms", requiredParms)
            DisplayReportParameters()
        Catch ex As Exception

        Finally
            If ds IsNot Nothing Then ds = Nothing
        End Try
    End Sub
    Enum Insructions
        FranklinCMMS = 1
    End Enum
    Private Sub PopulateHelp(ByVal name As Insructions)
        Dim sb As New StringBuilder
        'Select Case name
        '    Case Insructions.FranklinCMMS
        '        sb.Append("<h3>Please select a report, an Area and Time Frame and click on the Report</h3>")
        '        sb.Append("<ul><li>The paretos represent the labor costs with blue bar and number of work orders with a red line. </li>")
        '        sb.Append("<li>The material cost of these work orders is shown in the individual work order detail (not represented on the graph).</li>")
        '        sb.Append("<li>Repair work orders pareto include emergency, break-in, and corrective action work orders.</li>")
        '        sb.Append("<li>All work orders pareto displays all labor cost associated with all work orders (including indirect, capital, safety, contractor and standard PM tasks.</li></ul>")
        '        Me._liInstructions.Text = sb.ToString
        '    Case Else
        Me._liInstructions.Text = sb.ToString
        'End Select
    End Sub
    Private Sub DisplayReportParameters()
        Dim tblRow As Integer = 1
        Dim tblHeaderrow As Integer = 0
        Dim tblCell As Integer = 0
        Dim totalTblRows As Integer = _tblMain.Rows.Count - 1
        'Dim tblSiteRow As Integer = 2
        'Dim tblSiteHeaderRow As Integer = 1
        'Dim tblSiteCell As Integer = 0
        Dim requiredParms As RequiredReportParameters = Session.Item("requiredParms")

        Try


            If requiredParms.Site Then
                'Me.AddControlToTable(_ucSiteLocation, 2, 1, 0, Resources.Shared.lblDivision, Me._tblSite, True, False, "","None",True)
                Me._ucSiteLocation.Visible = True
                Me._tblSite.Rows(1).Visible = False
                Me._tblSite.Rows(2).Visible = False
                'Me.AddControlToTable(_ddlDivision, 2, 1, 0, Resources.Shared.lblDivision, Me._tblSite, False, False, "")
                'Me.AddControlToTable(_ddlFacility, 2, 1, 1, Resources.Shared.lblFacility, Me._tblSite, False, False, "")
                'Me.AddControlToTable(_ddlBusinessUnit, 2, 1, 2, Resources.Shared.lblBusinessUnit, Me._tblSite, False, False, "")
                'Me.AddControlToTable(_ddlArea, 4, 3, 0, Resources.Shared.lblArea, Me._tblSite, False, False, "")
                'Me.AddControlToTable(_ddlLine, 4, 3, 1, Resources.Shared.lblLine, Me._tblSite, False, False, "")
                'Me.AddControlToTable(_ddlLineBreak, 4, 3, 2, Resources.Shared.lblLineBreak, Me._tblSite, False, False, "")
            ElseIf requiredParms.Division = True And requiredParms.Facility = True Then
                Me._ucSiteLocation.HideArea = True
                Me._ucSiteLocation.HideBusinessUnit = True
                Me._ucSiteLocation.HideLine = True
                Me._ucSiteLocation.HideLineBreak = True
                Me._ucSiteLocation.DisplayAsSingleRow = True

                'Me._ucSiteLocation.HideDivision = True
                Me._ucSiteLocation.Visible = True
                Me._tblSite.Rows(1).Visible = False
                Me._tblSite.Rows(2).Visible = False
                'Me.AddControlToTable(_ucSiteLocation, 2, 1, 0, Resources.Shared.lblDivision, Me._tblSite, True, False, "", "None", True)


                'Me.AddControlToTable(_ddlDivision, 2, 1, 0, Resources.Shared.lblDivision, Me._tblSite, False, False, "")
                'Me.AddControlToTable(_ddlFacility, 2, 1, 1, Resources.Shared.lblFacility, Me._tblSite, False, False, "")
                'For i As Integer = _tblSite.Rows.Count To (4) Step -1
                '    _tblSite.Rows.Remove(_tblSite.Rows(i - 1))
                'Next
                '_tblSite.Rows(1).Cells.RemoveAt(2)
                '_tblSite.Rows(2).Cells.RemoveAt(2)

            ElseIf requiredParms.Facility Then
                'Me._ucSiteLocation.HideDivision = True
                Me._ucSiteLocation.HideArea = True
                Me._ucSiteLocation.HideBusinessUnit = True
                Me._ucSiteLocation.HideLine = True
                Me._ucSiteLocation.HideLineBreak = True
                Me._ucSiteLocation.Visible = True

                'Me.AddControlToTable(_ucSiteLocation, 2, 1, 0, Resources.Shared.lblDivision, Me._tblSite, True, False, "", "None", True)


                'Me.AddControlToTable(_ddlFacility, 2, 1, 0, Resources.Shared.lblFacility, Me._tblSite, False, False, "")
                'For i As Integer = _tblSite.Rows.Count To (4) Step -1
                '    _tblSite.Rows.Remove(_tblSite.Rows(i - 1))
                'Next
                '_tblSite.Rows(1).Cells.RemoveAt(2)
                '_tblSite.Rows(2).Cells.RemoveAt(2)
                '_tblSite.Rows(1).Cells.RemoveAt(1)
                '_tblSite.Rows(2).Cells.RemoveAt(1)
            Else
                Me._tblSite.Visible = False
            End If


            'If requiredParms.ConstrainedArea = True Then
            '    Me.AddControlToTable(_cblConstrainedArea, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue(ReportParameterType.ConstrainedArea), _tblMain, True)
            'End If


            'If requiredParms.CMMSFacility Then
            '    Me._tblSite.Visible = True
            '    Me._tblMain.Visible = False
            '    Me._ucSiteLocation.HideDivision = True
            '    Me._ucSiteLocation.HideArea = True
            '    Me._ucSiteLocation.HideBusinessUnit = True
            '    Me._ucSiteLocation.HideLine = True
            '    Me._ucSiteLocation.HideLineBreak = True
            '    'Me.AddControlToTable(_ucSiteLocation, 2, 1, 0, Resources.Shared.lblDivision, Me._tblSite, True, False, "", "None", True)

            '    AddControlToTable(_ddlCMMSFacility, 2, 1, 0, ipLoc.GetResourceValue(ReportParameterType.Facility), _tblSite, False, False, "")
            '    'For i As Integer = _tblSite.Rows.Count To (4) Step -1
            '    '    _tblSite.Rows.Remove(_tblSite.Rows(i - 1))
            '    'Next
            '    '_tblSite.Rows(1).Cells.RemoveAt(2)
            '    '_tblSite.Rows(2).Cells.RemoveAt(2)
            '    '_tblSite.Rows(1).Cells.RemoveAt(1)
            '    '_tblSite.Rows(2).Cells.RemoveAt(1)

            '    'If requiredParms.CMMSFranklin Then
            '    '    Me.AddControlToTable(Me._ddlCMMSFranklinReports, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue("Reports"), _tblMain, False)
            '    '    Me.AddControlToTable(Me._ddlCMMSFranklinArea, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue("Area"), _tblMain, False)
            '    '    Me.AddControlToTable(Me._rblCMMSFranklinTime, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue("Time"), _tblMain, True)
            '    '    Me._tblMain.Visible = True
            '    'Else

            '    'End If                                              
            'End If

            If requiredParms.MonthlyReport Then
                Me.AddControlToTable(_rblMonthlyReport, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue(ReportParameterType.MonthlyReport), _tblMain, True)
            End If
            If requiredParms.ActionItemStatus Then
                Me.AddControlToTable(_rblIncidentStatus, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue(ReportParameterType.ActionItemStatus), _tblMain, False)
            End If
            If requiredParms.EstimatedDueDate = True And requiredParms.Calendar = True Then
                Me.AddControlToTable(_cblDateRange, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue(ReportParameterType.EstimatedDueDate), _tblMain, False)
                Me.AddControlToTable(_ucCalendar, tblRow, tblHeaderrow, tblCell, String.Empty, _tblMain, False, True)
            ElseIf requiredParms.Calendar = True Then
                Me.AddControlToTable(_DateRange, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue(ReportParameterType.Calendar), _tblMain, True, False)
            End If
            If requiredParms.DateRange = True Then
                Me.AddControlToTable(_rblDateRange, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue(ReportParameterType.DateRange), _tblMain, False)
            End If

            If requiredParms.ChartType = True Then
                Me.AddControlToTable(_rblChartType, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue(ReportParameterType.ChartType), _tblMain, True)
            End If
            If requiredParms.ReportType = True Then
                Me.AddControlToTable(_rblReportType, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue(ReportParameterType.ReportType), _tblMain, False)
            End If
            If requiredParms.ChronicType = True Then
                Me.AddControlToTable(_rblChronicType, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue(ReportParameterType.ChronicType), _tblMain, False)
            End If
            If requiredParms.ReasonLevel = True Then
                Me.AddControlToTable(_rblReasonLevel, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue(ReportParameterType.ReasonLevel), _tblMain, False)
            End If
            If requiredParms.DownTime = True Then
                Me.AddControlToTable(_rblDownTime, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue(ReportParameterType.DownTime), _tblMain, False)
            End If
            If requiredParms.TaskActionItems = True Then
                AddControlToTable(_ifrDownTime, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue(ReportParameterType.TaskActionItems), _tblMain, True)
            End If
            'Changed by Cathy Cox
            If requiredParms.IncidentType = True Then
                'Me.AddControlToTable(_cblIncidentType, tblRow, tblHeaderrow, tblCell, ReportParameterType.IncidentType, _tblMain, False)
                Me.AddControlToTable(_ucIncidentType, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue(ReportParameterType.IncidentType), _tblMain, True, False, , "White", True)
            End If
            If requiredParms.RTSIncidentType = True Then
                Me.AddControlToTable(_rblRTSIncidentType, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue(ReportParameterType.IncidentType), _tblMain, False)
            End If
            If requiredParms.RCFAStatus = True Then
                Me.AddControlToTable(_rblRCFAStatus, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue(ReportParameterType.RCFAStatus), _tblMain, False)
            End If
            If requiredParms.ActionItemsListed = True Then
                Me.AddControlToTable(_rblActionItems, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue(ReportParameterType.ActionItemsListed), _tblMain, False)
            End If

            'Removed Person from report selection (CAC - 02/27/2012)
            'If requiredParms.Person = True Then
            '    Me._cddlPerson.Enabled = True
            '    Me.AddControlToTable(_ddlPerson, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue(ReportParameterType.Person), _tblMain, False)
            'Else
            Me._cddlPerson.Enabled = False
            'End If

            'If requiredParms.PPRMills Then
            '    AddControlToTable(Me._txtParetoNumbers, tblRow, tblHeaderrow, tblCell, "Pareto Numbers", _tblMain, False)
            '    Me.AddControlToTable(Me._ucCalendar, tblRow, tblHeaderrow, tblCell, ReportParameterType.Calendar, _tblMain, False)
            '    AddControlToTable(_pprMillMachineSelection, tblRow, tblHeaderrow, tblCell, "PPR Machines", _tblMain, False)
            'End If
            If requiredParms.PaperDownTime Then
                'Me.AddControlToTable(_ddlPPRFacility, 2, 1, 0, Resources.Shared.lblFacility, Me._tblSite, False, False, "")
                'For i As Integer = _tblSite.Rows.Count To (4) Step -1
                '    _tblSite.Rows.Remove(_tblSite.Rows(i - 1))
                'Next
                '_tblSite.Rows(1).Cells.RemoveAt(2)
                '_tblSite.Rows(2).Cells.RemoveAt(2)
                '_tblSite.Rows(1).Cells.RemoveAt(1)
                '_tblSite.Rows(2).Cells.RemoveAt(1)
                '_tblSite.Visible = True
                'AddControlToTable(_pprReasons, tblRow, tblHeaderrow, tblCell, ReportParameterType.PaperDowntime, _tblMain, False)
                '_pprReasons.Populate()
                AddControlToTable(_ifrDownTime, tblRow, tblHeaderrow, tblCell, ipLoc.GetResourceValue(ReportParameterType.PaperDowntime), _tblMain, True)
            End If

            'Me.AddControlToTable(Me._ucIncidentType, tblRow, tblHeaderrow, tblCell, ReportParameterType.IncidentType, _tblMain, False)
            SetReportDefaults()
            If tblCell = 1 Then tblRow += 2
            If tblRow < 1 Then tblRow = 1
            For i As Integer = totalTblRows To (tblRow - 1) Step -1 'tblRow + 1 Step -1
                _tblMain.Rows.Remove(_tblMain.Rows(i))
            Next

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub SetSiteDefaults()
        Dim username As String
        username = CurrentUserProfile.GetCurrentUser

        'Me._ddlFacility.SelectedIndex = -1
        'Me._ddlDivision.SelectedIndex = -1
        'Me._ddlLineBreak.SelectedIndex = -1
        'Me._ddlLine.SelectedIndex = -1
        'Me._ddlBusinessUnit.SelectedIndex = -1
        'Me._ddlArea.SelectedIndex = -1

        Dim userProfile As CurrentUserProfile = CType(Session.Item(Replace(username, "\", "_")), CurrentUserProfile)
        If userProfile IsNot Nothing Then
            'If _ddlDivision.Items.FindByValue(userProfile.DefaultDivision) IsNot Nothing Then
            '    _ddlDivision.SelectedValue = userProfile.DefaultDivision
            'End If
            'If _ddlFacility.Items.FindByValue(userProfile.DefaultFacility) IsNot Nothing Then
            '_ddlFacility.SelectedValue = userProfile.DefaultFacility
            'End If
            'If _upSite.UpdateMode = UpdatePanelUpdateMode.Conditional Then
            '    Me._upSite.Update()
            'End If
        End If
    End Sub

    'Public Sub PopulateCMMSFranklinDropdowns()        

    '    'Franklin Reports
    '    With _ddlCMMSFranklinReports
    '        '.Items.Clear()
    '        .ID = "_ddlCMMSFranklinReports"
    '        .Items.Add(New ListItem("Repair Work Orders- Labor Cost", "30"))
    '        .Items.Add(New ListItem("Repair Work Orders- Labor Cost(Gen Equip excluded)", "31"))
    '        .Items.Add(New ListItem("Repair Work Orders- Total Cost", "32"))
    '        .Items.Add(New ListItem("Repair Work Orders- Total Cost(Gen Equip excluded)", "33"))
    '        .Items.Add(New ListItem("All Work Orders - Labor Cost", "995"))
    '        .Items.Add(New ListItem("All Work Orders - Labor Cost(Gen Equip excluded)", "996"))
    '        .Items.Add(New ListItem("All Work Orders - Total Cost", "997"))
    '        .Items.Add(New ListItem("All Work Orders - Total Cost(Gen Equip excluded)", "998"))
    '        .Items.Add(New ListItem("Pumps - Packing & Seal Failure - Labor Cost", "999"))
    '        .Items.Add(New ListItem("Pumps - Packing & Seal Failure - Total Cost", "1000"))
    '        If Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_ddlCMMSFranklinReports") IsNot Nothing Then
    '            If .Items.FindByValue(Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_ddlCMMSFranklinReports")) IsNot Nothing Then
    '                .SelectedValue = Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_ddlCMMSFranklinReports")
    '            End If
    '        End If
    '    End With
    '    'ipLoc.LocalizeListControl(_ddlCMMSFranklinReports)
    '    SharedFunctions.SortDropDown(Me._ddlCMMSFranklinReports)

    '    With _ddlCMMSFranklinArea
    '        .ID = "_ddlCMMSFranklinArea"
    '        '.Items.Clear()
    '        .Items.Add(New ListItem("All", "Franklin Mill"))
    '        .Items.Add(New ListItem("Bleach Stock", "Bleach (380-396)"))
    '        .Items.Add(New ListItem("Brown Stock", "Brown (331-348)"))
    '        .Items.Add(New ListItem("Cook & Dyeroom", "Cook Room (440)"))
    '        .Items.Add(New ListItem("Fiber Recycling", "Fiber Recycling (430-433,438)"))
    '        .Items.Add(New ListItem("All Fibers", "Fibers (331-348,370-396)"))
    '        .Items.Add(New ListItem("Lime Kiln", "Lime Kiln (370-377)"))
    '        .Items.Add(New ListItem("Power", "Power (510-540,546)"))
    '        .Items.Add(New ListItem("Paper Machine #1", "Paper Machine #1 (401)"))
    '        .Items.Add(New ListItem("Paper Machine #3", "Paper Machine #3 (403)"))
    '        .Items.Add(New ListItem("Paper Machine #4", "Paper Machine #4 (404)"))
    '        .Items.Add(New ListItem("Paper Machine #5", "Paper Machine #5 (405)"))
    '        .Items.Add(New ListItem("Paper Machine #6", "Paper Machine #6 (406)"))
    '        .Items.Add(New ListItem("All Paper Mill", "Paper Mill (400-425,440)"))
    '        .Items.Add(New ListItem("All Power & Recovery", "Power & Recovery (350-364,399,510-540,546)"))
    '        .Items.Add(New ListItem("Recovery", "Recovery (350-364,399)"))
    '        .Items.Add(New ListItem("Sheet Finishing", "Sheet Finishing (600-643)"))
    '        .Items.Add(New ListItem("Roll Finishing", "Roll Finishing (449-495)"))
    '        .Items.Add(New ListItem("Power Boilers", "Power Boilers (510-524,529)"))
    '        .Items.Add(New ListItem("Turbines", "Turbines (530-540)"))
    '        .Items.Add(New ListItem("H2O Treatment", "H2O Treatment (525-526,546)"))
    '        .Items.Add(New ListItem("Woodyard", "Woodyard (301-327)"))
    '        .Items.Add(New ListItem("WWC", "WWC (545,550-553,570)"))
    '        If Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_ddlCMMSFranklinArea") IsNot Nothing Then
    '            If .Items.FindByValue(Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_ddlCMMSFranklinArea")) IsNot Nothing Then
    '                .SelectedValue = Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_ddlCMMSFranklinArea")
    '            End If
    '        End If
    '    End With

    '    'ipLoc.LocalizeListControl(_ddlCMMSFranklinArea)
    '    SharedFunctions.SortDropDown(Me._ddlCMMSFranklinArea)
    '    With _rblCMMSFranklinTime
    '        '.Items.Clear()
    '        .ID = "_rblCMMSFranklinTime"
    '        .Items.Add(New ListItem("Last 6 Months", "Last 6 Months"))
    '        .Items.Add(New ListItem("Last 12 Months", "Last 12 Months"))
    '        .Items.Add(New ListItem("Year to Date", "Year to Date"))
    '        .Items.Add(New ListItem("Last Year YTD", "Last Year - Year to Date"))
    '        .AutoPostBack = True
    '        .RepeatDirection = RepeatDirection.Horizontal
    '        If Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_rblCMMSFranklinTime") IsNot Nothing Then
    '            If .Items.FindByValue(Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_rblCMMSFranklinTime")) IsNot Nothing Then
    '                .SelectedValue = Request.Form("ctl00$_cphMain$_ucSiteDropdowns$_rblCMMSFranklinTime")
    '            End If
    '        Else
    '            .SelectedIndex = 0
    '        End If
    '    End With
    '    'ipLoc.LocalizeListControl(_rblCMMSFranklinTime)


    'End Sub
    'Public Sub PopulateCMMSFacility()
    '    Dim paramCollection As New OracleParameterCollection
    '    Dim param As New OracleParameter
    '    Dim ds As System.Data.DataSet = Nothing

    '    Try
    '        param = New OracleParameter
    '        param.ParameterName = "rsFacility"
    '        param.OracleDbType = OracleDbType.Cursor
    '        param.Direction = Data.ParameterDirection.Output
    '        paramCollection.Add(param)

    '        ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "RI.CMMSFacility", "CMMSFacility", 3)
    '        If ds IsNot Nothing Then
    '            If ds.Tables.Count >= 1 Then
    '                'SiteName
    '                _ddlCMMSFacility.DataSource = ds.Tables(0).CreateDataReader
    '                _ddlCMMSFacility.Items.Clear()
    '                _ddlCMMSFacility.DataTextField = "SiteName"
    '                _ddlCMMSFacility.DataValueField = "SiteID"
    '                _ddlCMMSFacility.DataBind()
    '                '_ddlCMMSFacility.Items.Insert(0, "")
    '                _ddlCMMSFacility.AutoPostBack = True
    '                ipLoc.LocalizeListControl(_ddlCMMSFacility)
    '                AddHandler _ddlCMMSFacility.SelectedIndexChanged, AddressOf _ddlCMMSFacility_SelectedIndexChanged
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    Finally
    '        If ds IsNot Nothing Then
    '            ds = Nothing
    '        End If
    '    End Try
    'End Sub

    Public Sub PopulatePPRFacility()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim con As String = ConfigurationManager.ConnectionStrings.Item("dbCnPRODpmdb").ConnectionString
        Dim prov As String = ConfigurationManager.ConnectionStrings.Item("dbCnPRODpmdb").ProviderName
        Dim trigger As New AsyncPostBackTrigger
        Try
            param = New OracleParameter
            param.ParameterName = "rsFacility"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            'ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "RI.PPRFacility", "PPRFacility", 3)
            ds = RI.SharedFunctions.GetOracleDataSet(Resources.Sql.sqlPPRSite, con, prov)
            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    'SiteName                    
                    _ddlPPRFacility.DataSource = ds.Tables(0).CreateDataReader
                    _ddlPPRFacility.Items.Clear()
                    _ddlPPRFacility.DataTextField = "Name"
                    _ddlPPRFacility.DataValueField = "Site"
                    _ddlPPRFacility.DataBind()
                    _ddlPPRFacility.Items.Insert(0, "")
                    _ddlPPRFacility.AutoPostBack = True
                    AddHandler _ddlPPRFacility.SelectedIndexChanged, AddressOf _ddlPPRFacility_SelectedIndexChanged
                    _ddlPPRFacility.ID = "_ddlPPRFacility"
                    'trigger.ControlID = Me._ddlPPRFacility.ID
                    'trigger.EventName = "SelectedIndexChanged"
                    '_upSite.Triggers.Add(trigger)
                    ipLoc.LocalizeListControl(_ddlPPRFacility)
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

    Public Sub SetReportDefaults()
        Try

            Select Case ReportTitle
                Case ReportTitles.ActionItems
                    If _rblIncidentStatus.Items.FindByValue("Incomplete") IsNot Nothing Then
                        _rblIncidentStatus.SelectedIndex = -1
                        _rblIncidentStatus.Items.FindByValue("Incomplete").Selected = True
                    End If
                    'If _cblIncidentType.SelectedIndex = -1 Then
                    'If _cblIncidentType.Items.FindByValue("All") IsNot Nothing Then
                    '    _cblIncidentType.SelectedIndex = -1
                    '    _cblIncidentType.Items.FindByValue("All").Selected = True
                    'End If
                    If _rblIncidentStatus.SelectedValue <> "All" And _rblIncidentStatus.SelectedValue <> "Complete" Then
                        _cblDateRange.SelectedIndex = -1
                        SetDefaultDateRange(range.YearToDate)
                        '_ucCalendar.ShowCalendar = False
                        _ucCalendar.Visible = False
                        Me._cblDateRange.Visible = True
                    Else
                        SetDateRange(range.YearToDate)
                        '_ucCalendar.ShowCalendar = True
                        _ucCalendar.Visible = True
                        _ucCalendar.ShowFutureDates = False
                        Me._cblDateRange.Visible = False
                    End If
                    'End If

                    'If Me._cblDateRange.SelectedIndex = -1 Then
                    If Me._cblDateRange.Items.FindByValue("All") IsNot Nothing Then
                        Me._cblDateRange.SelectedIndex = -1
                        Me._cblDateRange.Items.FindByValue("All").Selected = True
                    End If
                    'End If

                    'If Me._ddlPerson.Items.FindByValue("All") IsNot Nothing Then
                    '    Me._ddlPerson.SelectedIndex = -1
                    '    Me._ddlPerson.Items.FindByValue("All").Selected = True
                    'End If

                    'If _rblConstrainedArea.Items.FindByValue("All") IsNot Nothing Then
                    '    _rblConstrainedArea.SelectedIndex = -1
                    '    _rblConstrainedArea.Items.FindByValue("All").Selected = True
                    'End If

                Case ReportTitles.IncidentListing ' "Incident Listing"
                    If _rblRCFAStatus.Items.FindByValue("Open") IsNot Nothing Then
                        _rblRCFAStatus.SelectedIndex = -1
                        _rblRCFAStatus.Items.FindByValue("Open").Selected = True
                    End If

                    If _rblActionItems.Items.FindByValue("Incomplete") IsNot Nothing Then
                        _rblActionItems.SelectedIndex = -1
                        _rblActionItems.Items.FindByValue("Incomplete").Selected = True
                    End If

                    'If _rblConstrainedArea.Items.FindByValue("All") IsNot Nothing Then
                    '    _rblConstrainedArea.SelectedIndex = -1
                    '    _rblConstrainedArea.Items.FindByValue("All").Selected = True
                    'End If

                    Me._DateRange.SelectedDateRange = RI_User_Controls_Common_ucDateRange.range.YearToDate

                    'If _cblIncidentType.Items.FindByValue("All") IsNot Nothing Then
                    '    _cblIncidentType.SelectedIndex = -1
                    '    _cblIncidentType.Items.FindByValue("All").Selected = True
                    '    _rblRCFAStatus.Enabled = True
                    'End If

                    'Me._rblDateRange.Visible = True
                    'If _rblDateRange.Items.FindByValue("Year To Date") IsNot Nothing Then
                    '    _rblDateRange.SelectedIndex = -1
                    '    _rblDateRange.Items.FindByValue("Year To Date").Selected = True
                    '    'SetDateRange(range.YearToDate)
                    '    SetDefaultDateRange(range.YearToDate)
                    'End If

                    'Me._ucCalendar.ShowCalendar = True

                    'If Me._ddlPerson.Items.FindByValue("All") IsNot Nothing Then
                    '    Me._ddlPerson.SelectedIndex = -1
                    '    Me._ddlPerson.Items.FindByValue("All").Selected = True
                    'End If

                Case ReportTitles.RRandRCFAIncidentCompleteness '"RR and RCFA Incident Completeness"
                    'Me._rblDateRange.Visible = True
                    'If _rblDateRange.Items.FindByValue("Year To Date") IsNot Nothing Then
                    '    _rblDateRange.SelectedIndex = -1
                    '    _rblDateRange.Items.FindByValue("Year To Date").Selected = True
                    '    SetDefaultDateRange(range.YearToDate)
                    'End If
                    'Me._ucCalendar.ShowCalendar = True

                    Me._DateRange.SelectedDateRange = RI_User_Controls_Common_ucDateRange.range.YearToDate

                    If Me._rblReportType.SelectedIndex = -1 Then
                        If Me._rblReportType.Items.FindByValue("Detail") IsNot Nothing Then
                            Me._rblReportType.SelectedIndex = -1
                            Me._rblReportType.Items.FindByValue("Detail").Selected = True
                        End If
                    End If
                Case ReportTitles.InvestigationExecutiveSummary '"Investigation Executive Summary"              

                    Me._DateRange.SelectedDateRange = RI_User_Controls_Common_ucDateRange.range.YearToDate
                    'Me._rblDateRange.Visible = True
                    'If _rblDateRange.Items.FindByValue("Year To Date") IsNot Nothing Then
                    '    _rblDateRange.SelectedIndex = -1
                    '    _rblDateRange.Items.FindByValue("Year To Date").Selected = True
                    '    SetDefaultDateRange(range.YearToDate)
                    'End If
                    'Me._ucCalendar.ShowCalendar = True
                    'Me._rblDateRange.Visible = True
                    'If _cblIncidentType.SelectedIndex = -1 Then
                    'If _cblIncidentType.Items.FindByValue("All") IsNot Nothing Then
                    '    _cblIncidentType.SelectedIndex = -1
                    '    _cblIncidentType.Items.FindByValue("All").Selected = True
                    'End If
                    'End If
                    'If _rblRCFAStatus.SelectedIndex = -1 Then
                    If _rblRCFAStatus.Items.FindByValue("Open") IsNot Nothing Then
                        _rblRCFAStatus.SelectedIndex = -1
                        _rblRCFAStatus.Items.FindByValue("Open").Selected = True
                    End If
                    'End If

                    'If _rblConstrainedArea.Items.FindByValue("All") IsNot Nothing Then
                    '    _rblConstrainedArea.SelectedIndex = -1
                    '    _rblConstrainedArea.Items.FindByValue("All").Selected = True
                    'End If

                Case ReportTitles.ManagementSummary
                    If Me._rblReportType.SelectedIndex = -1 Then
                        If Me._rblReportType.Items.FindByValue("Area") IsNot Nothing Then
                            Me._rblReportType.SelectedIndex = -1
                            Me._rblReportType.Items.FindByValue("Area").Selected = True
                        End If
                    End If

                Case ReportTitles.PCF '"PCF Certified Kill"
                    _rblReportType.AutoPostBack = False
                    Me._DateRange.SelectedDateRange = RI_User_Controls_Common_ucDateRange.range.YearToDate
                    If Me._rblReportType.SelectedIndex = -1 Then
                        'If Me._rblReportType.Items.FindByValue("P/CF Certified Kills Only") IsNot Nothing Then
                        'Me._rblReportType.SelectedIndex = -1
                        Me._rblReportType.Items.FindByValue("P/CF Certified Kills Only").Selected = True
                    End If
                    'End If
                    If Me._rblChronicType.SelectedIndex = -1 Then
                        'If Me._rblChronicType.Items.FindByValue("All") IsNot Nothing Then
                        'Me._rblChronicType.SelectedIndex = -1
                        Me._rblChronicType.Items.FindByValue("Both Area and Mill Top").Selected = True
                    End If
                    'End If


                    'Case ReportTitles.ParetoChartsCMMS '"Pareto Charts - CMMS"
                    '    'If Me._rblCMMSFranklinTime.Items.FindByValue("Last 6 Months") IsNot Nothing Then
                    '    '    Me._rblCMMSFranklinTime.Items.FindByValue("Last 6 Months").Selected = True
                    '    'End If
                    '    If _ddlCMMSFacility.Items.FindByValue("") IsNot Nothing Then

                    '    End If

                Case ReportTitles.ParetoChartsIncidents '"Pareto Charts - Incidents"
                    'Me._rblDateRange.Visible = True
                    'If _rblDateRange.Items.FindByValue("Year To Date") IsNot Nothing Then
                    '    _rblDateRange.SelectedIndex = -1
                    '    _rblDateRange.Items.FindByValue("Year To Date").Selected = True
                    '    SetDefaultDateRange(range.YearToDate)
                    'End If
                    'Me._ucCalendar.ShowCalendar = True
                    'If _rblChartType.SelectedIndex = -1 Then

                    Me._DateRange.SelectedDateRange = RI_User_Controls_Common_ucDateRange.range.YearToDate

                    If _rblChartType.Items.FindByValue("Latent Root Cause") IsNot Nothing Then
                        _rblChartType.SelectedIndex = -1
                        _rblChartType.Items.FindByValue("Latent Root Cause").Selected = True
                    End If
                    'End If
                    'If _cblIncidentType.SelectedIndex = -1 Then
                    'If _cblIncidentType.Items.FindByValue("All") IsNot Nothing Then
                    '    _cblIncidentType.SelectedIndex = -1
                    '    _cblIncidentType.Items.FindByValue("All").Selected = True
                    'End If
                    'End If

                    'If _rblConstrainedArea.Items.FindByValue("All") IsNot Nothing Then
                    '    _rblConstrainedArea.SelectedIndex = -1
                    '    _rblConstrainedArea.Items.FindByValue("All").Selected = True
                    'End If

                Case ReportTitles.ReliabilityIndex '"Reliability Index"
                    'Me._ucCalendar.ShowCalendar = False
                    'Me._ddlLineBreak.Enabled = False
                    'Me._ddlLine.Enabled = False
                    'Me._ddlBusinessUnit.Enabled = False
                    'Me._ddlArea.Enabled = False
                    If Me._rblDateRange.Items.FindByValue("2 Year") IsNot Nothing Then
                        _rblDateRange.SelectedIndex = -1
                        _rblDateRange.Items.FindByValue("2 Year").Selected = True
                    End If
                    'tblRow = -1
                Case ReportTitles.EnterpriseSummaryReporting
                    'If Me._rblMonthlyReport.SelectedIndex < 0 Then
                    Me._rblMonthlyReport.SelectedIndex = 0
                    'End If
                Case ReportTitles.ReliabilityReportingScorecard '"Reliability Reporting Scorecard"
                    Me._DateRange.SelectedDateRange = RI_User_Controls_Common_ucDateRange.range.LastMonth
                    'Me._rblDateRange.Visible = True
                    'If _rblDateRange.Items.FindByValue("Last Month") IsNot Nothing Then
                    '    _rblDateRange.SelectedIndex = -1
                    '    _rblDateRange.Items.FindByValue("Last Month").Selected = True
                    '    SetDefaultDateRange(range.LastMonth)
                    'End If
                    'Me._ucCalendar.ShowCalendar = True
                Case ReportTitles.ReliabilityTrackingSystem '"Reliability Tracking System (RTS)"                   
                    'SetDefaultDateRange(range.YearToDate)
                    'Me._ucCalendar.ShowCalendar = True
                    Me._DateRange.SelectedDateRange = RI_User_Controls_Common_ucDateRange.range.YearToDate

                    If Me._rblReportType.Items.FindByValue("Reason Level Summary").Selected = False Then
                        Me._rblReasonLevel.Enabled = False
                    End If
                    If _rblReportType.Items.FindByValue("Summary") IsNot Nothing Then
                        _rblReportType.SelectedIndex = -1
                        _rblReportType.Items.FindByValue("Summary").Selected = True
                    End If
                    If Me._rblDownTime.Items.FindByValue("All") IsNot Nothing Then
                        Me._rblDownTime.SelectedIndex = -1
                        Me._rblDownTime.Items.FindByValue("All").Selected = True
                    End If
                    If Me._rblRTSIncidentType.Items.FindByValue("All") IsNot Nothing Then
                        Me._rblRTSIncidentType.SelectedIndex = -1
                        Me._rblRTSIncidentType.Items.FindByValue("All").Selected = True
                    End If

                    If Me._rblReasonLevel.Items.FindByValue("Reason Level 1") IsNot Nothing Then
                        Me._rblReasonLevel.SelectedIndex = -1
                        Me._rblReasonLevel.Items.FindByValue("Reason Level 1").Selected = True
                    End If
                Case ReportTitles.RCFACommitmentStatus '"RCFA Commitment Status"
                    Me._DateRange.SelectedDateRange = RI_User_Controls_Common_ucDateRange.range.TMinus15MthToTMinus3Mth

                    'If _rblConstrainedArea.Items.FindByValue("All") IsNot Nothing Then
                    '    _rblConstrainedArea.SelectedIndex = -1
                    '    _rblConstrainedArea.Items.FindByValue("All").Selected = True
                    'End If
                Case ReportTitles.IncidentListingChartsByGroup '"Pareto Charts - Incidents"
                    'Me._rblDateRange.Visible = True
                    'If _rblDateRange.Items.FindByValue("Year To Date") IsNot Nothing Then
                    '    _rblDateRange.SelectedIndex = -1
                    '    _rblDateRange.Items.FindByValue("Year To Date").Selected = True
                    '    SetDefaultDateRange(range.YearToDate)
                    'End If
                    'Me._ucCalendar.ShowCalendar = True
                    'If _rblChartType.SelectedIndex = -1 Then

                    If _rblChartType.Items.FindByValue("All") IsNot Nothing Then
                        _rblChartType.SelectedIndex = -1
                        _rblChartType.Items.FindByValue("All").Selected = True
                    End If

            End Select
        Catch ex As Exception
            Throw
        End Try
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '_txtStartDate.ReadOnly = True
        '_txtEndDate.ReadOnly = True
        Try

            If Page.IsPostBack Then
                If ReportTitle.Length > 0 Then
                    Me.PopulateSiteDropdown()
                    Me.PopulateDropdowns()
                End If

            End If
            _rblIncidentStatus.EnableViewState = False ' As New RadioButtonList
            _ddlPerson.EnableViewState = False ' As New DropDownList
            _cblDateRange.EnableViewState = False ' As New CheckBoxList
            _cblIncidentType.EnableViewState = False ' As New CheckBoxList
            '_cblConstrainedArea.EnableViewState = False ' As New CheckBoxList
            _rblDateRange.EnableViewState = False ' As New RadioButtonList
            _rblActionItems.EnableViewState = False ' As New RadioButtonList
            _rblRCFAStatus.EnableViewState = False ' As New RadioButtonList
            _rblChartType.EnableViewState = False ' As New RadioButtonList
            _rblReportType.EnableViewState = False ' As New RadioButtonList
            _rblChronicType.EnableViewState = False ' As New RadioButtonList
            'Dim sb As New StringBuilder
            'sb.Append("DisplayReport();")
            'ScriptManager.RegisterClientScriptInclude(Me._udpReportParameters, _udpReportParameters.GetType, "ReportSelection", ("User Controls/ReportSelection.js"))
            ScriptManager.RegisterClientScriptInclude(_upSite.Page, _upSite.GetType, "ReportSelection", Page.ResolveClientUrl("~/ri/User Controls/ReportSelection.js"))
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub _ddlFacility_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'ViewState.Remove("Person")
        'PopulateSiteDropdown()
        'Me._udpReportParameters.Update()
    End Sub
    Protected Sub _ddlPPRFacility_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Me._pprMillMachineSelection.PopulateMachines(Me._ddlPPRFacility.SelectedValue)
    End Sub
    'Protected Sub _ddlCMMSFacility_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    'If _ddlCMMSFacility.SelectedItem.Value = "Franklin" Then
    '    If _ddlCMMSFacility.SelectedValue = "FR" Then
    '        'Me.PopulateCMMSFranklinDropdowns()
    '        Me._btnSubmit.Enabled = True
    '    ElseIf _ddlCMMSFacility.SelectedValue = "AU" Then
    '        Me._btnSubmit.Enabled = True
    '    ElseIf _ddlCMMSFacility.SelectedValue = "EO" Then
    '        Me._btnSubmit.Enabled = True
    '    Else
    '        Me._btnSubmit.Enabled = True
    '    End If
    'End Sub
    Protected Sub _rblActionItems_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim targetEvent As String()
        Dim eventValue As String
        Dim selectedValue As String = String.Empty
        If Page.IsPostBack And Request.Form("__EventTarget") IsNot Nothing Then
            If Request.Form("__EventTarget").Contains("_rblActionItems") Then
                targetEvent = Request.Form("__EventTarget").Split("$")
                eventValue = targetEvent(targetEvent.Length - 1).ToString
                selectedValue = sender.items(eventValue).value.ToString
            Else
                Exit Sub
            End If
        End If

        If sender.SelectedValue = "Incomplete" Then
            Dim rbl As RadioButtonList = Page.FindControl("_cblDateRange")
            If rbl IsNot Nothing Then
                rbl.Visible = False
                'Me._ucCalendar.ShowCalendar = True
                Me._ucCalendar.Visible = True
            End If
            ' _divDateRangeList.Visible = True
            '_divDateRangeCalendar.Visible = False
        Else
            Dim rbl As RadioButtonList = Page.FindControl("_cblDateRange")
            If rbl IsNot Nothing Then
                rbl.Visible = False
                'Me._ucCalendar.ShowCalendar = True
                Me._ucCalendar.Visible = True
            End If
            '_divDateRangeList.Visible = False
            '_divDateRangeCalendar.Visible = True
        End If
    End Sub
    Protected Sub _rblChartType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim targetEvent As String()
        Dim eventValue As String
        Dim selectedValue As String = String.Empty
        If Page.IsPostBack And Request.Form("__EventTarget") IsNot Nothing Then
            If Request.Form("__EventTarget").Contains("_rblChartType") Then
                targetEvent = Request.Form("__EventTarget").Split("$")
                eventValue = targetEvent(targetEvent.Length - 1).ToString
                selectedValue = sender.items(eventValue).value.ToString
            Else
                Exit Sub
            End If
        End If

        'If sender.SelectedValue = "Physical Root Cause" Or sender.SelectedValue = "Latent Root Cause" Or sender.SelectedValue = "Human Root Cause" Then
        '    Me._cblIncidentType.Enabled = True
        'Else
        '    Me._cblIncidentType.Enabled = False
        'End If
    End Sub

    Protected Sub _rblreportType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim targetEvent As String()
        Dim eventValue As String
        Dim selectedValue As String = String.Empty
        If Page.IsPostBack And Request.Form("__EventTarget") IsNot Nothing Then
            If Request.Form("__EventTarget").Contains("_rblReportType") Then
                targetEvent = Request.Form("__EventTarget").Split("$")
                eventValue = targetEvent(targetEvent.Length - 1).ToString
                selectedValue = sender.items(eventValue).value.ToString
            Else
                Exit Sub
            End If
        End If

        If sender.SelectedValue = "Reason Level Summary" Then
            Me._rblReasonLevel.Enabled = True
        Else
            Me._rblReasonLevel.Enabled = False
        End If
    End Sub
    Protected Sub _rblChronicType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim targetEvent As String()
        Dim eventValue As String
        Dim selectedValue As String = String.Empty
        If Page.IsPostBack And Request.Form("__EventTarget") IsNot Nothing Then
            If Request.Form("__EventTarget").Contains("_rblChronicType") Then
                targetEvent = Request.Form("__EventTarget").Split("$")
                eventValue = targetEvent(targetEvent.Length - 1).ToString
                selectedValue = sender.items(eventValue).value.ToString
            Else
                Exit Sub
            End If
        End If

        'If sender.SelectedValue = "Reason Level Summary" Then
        '    Me._rblReasonLevel.Enabled = True
        'Else
        '    Me._rblReasonLevel.Enabled = False
        'End If
    End Sub

    Protected Sub _rblIncidentStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim targetEvent As String()
        'Dim eventValue As String
        'Dim selectedValue As String = String.Empty
        'If Page.IsPostBack And Request.Form("__EventTarget") IsNot Nothing Then
        '    If Request.Form("__EventTarget").Contains("_rblIncidentStatus") Then
        '        targetEvent = Request.Form("__EventTarget").Split("$")
        '        eventValue = targetEvent(targetEvent.Length - 1).ToString
        '        selectedValue = sender.items(eventValue).value.ToString

        '    End If        
        'End If

        If sender.SelectedValue = "Incomplete" Then

            If _cblDateRange IsNot Nothing Then
                _cblDateRange.Visible = True
                'Me._ucCalendar.ShowCalendar = False
                Me._ucCalendar.Visible = False
            End If
            ' _divDateRangeList.Visible = True
            '_divDateRangeCalendar.Visible = False
        Else

            If _cblDateRange IsNot Nothing Then
                _cblDateRange.Visible = False
                If _cblDateRange.Items.FindByValue("All") IsNot Nothing Then
                    _cblDateRange.SelectedValue = "All"
                End If
                'Me._ucCalendar.ShowCalendar = True
                Me._ucCalendar.Visible = True
                If _ucCalendar.StartDate.Length = 0 And _ucCalendar.EndDate.Length = 0 Then
                    _ucCalendar.StartDate = DateSerial(Year(Today), 1, 1)
                    _ucCalendar.EndDate = Today.ToShortDateString
                End If
            End If
            '_divDateRangeList.Visible = False
            '_divDateRangeCalendar.Visible = True
        End If
    End Sub


    Protected Sub _cblDateRange_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles _cblDateRange.SelectedIndexChanged

        'Dim targetEvent = Request.Form("__EventTarget").Split("$")
        'Dim eventValue As String = targetEvent(targetEvent.length - 1).ToString
        'Dim selectedValue As String = sender.items(eventValue).value.ToString

        Dim targetEvent As String()
        Dim eventValue As String
        Dim selectedValue As String = String.Empty
        If Page.IsPostBack And Request.Form("__EventTarget") IsNot Nothing Then
            If Request.Form("__EventTarget").Contains("_cblDateRange") Then
                targetEvent = Request.Form("__EventTarget").Split("$")
                eventValue = targetEvent(targetEvent.Length - 1).ToString
                selectedValue = sender.items(eventValue).value.ToString
            Else
                Exit Sub
            End If
        End If

        Dim itm As ListItem

        If selectedValue = "All" Then

            For Each itm In sender.Items
                itm.Selected = False
                'itm.Enabled = False
            Next
            itm = sender.Items.FindByValue("All")
            If itm IsNot Nothing Then
                'itm.Enabled = True
                itm.Selected = True
            End If
        Else
            itm = sender.Items.FindByValue("All")
            If itm IsNot Nothing Then
                'itm.Enabled = True
                itm.Selected = False
            End If
        End If
    End Sub

    Protected Sub _rblDateRange_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles _rblDateRange.SelectedIndexChanged
        Dim todaysDate As Date = Now
        Dim targetEvent As String()
        Dim eventValue As String
        Dim selectedValue As String = String.Empty
        If Page.IsPostBack And Request.Form("__EventTarget") IsNot Nothing Then
            If Request.Form("__EventTarget").Contains("_rblDateRange") Then
                targetEvent = Request.Form("__EventTarget").Split("$")
                eventValue = targetEvent(targetEvent.Length - 1).ToString
                selectedValue = _rblDateRange.Items(eventValue).Value.ToString
            Else
                Exit Sub
            End If
        End If

        Select Case LCase(selectedValue)
            Case "last month"
                SetDateRange(range.LastMonth)
            Case "last 3 months"
                SetDateRange(range.Last3Months)
            Case "last year to date"
                SetDateRange(range.LastYearToDate)
            Case "year to date"
                SetDateRange(range.YearToDate)
            Case "1st quarter"
                SetDateRange(range.FirstQuarter)
            Case "2nd quarter"
                SetDateRange(range.SecondQuarter)
            Case "3rd quarter"
                SetDateRange(range.ThirdQuarter)
            Case "4th quarter"
                SetDateRange(range.FourthQuarter)
            Case "entered last 7 days"
                SetDateRange(range.EnteredLast7Days)
            Case "last year"
                SetDateRange(range.LastYear)
                'Remember to set the Last 7 Days Flag
            Case Else
                'SetDateRange(range.LastMonth)
        End Select
    End Sub
    Private Enum range
        LastMonth = 1
        Last3Months = 2
        LastYearToDate = 3
        YearToDate = 4
        FirstQuarter = 5
        SecondQuarter = 6
        ThirdQuarter = 7
        FourthQuarter = 8
        EnteredLast7Days = 9
        LastYear = 10
        EndOfYear = 11
    End Enum
    Private Sub SetDateRange(ByVal dtRange As range)
        Dim todaysDate As Date = Now

        Select Case dtRange
            Case range.LastMonth
                _ucCalendar.StartDate = DateSerial(Year(todaysDate), Month(todaysDate) - 1, 1)
                _ucCalendar.EndDate = DateSerial(Year(todaysDate), Month(todaysDate), 0)
            Case range.Last3Months
                _ucCalendar.StartDate = DateSerial(Year(todaysDate), Month(todaysDate) - 3, 1)
                _ucCalendar.EndDate = DateSerial(Year(todaysDate), Month(todaysDate), 0)
            Case range.LastYearToDate '"last year to date"
                _ucCalendar.StartDate = DateSerial(Year(todaysDate) - 1, 1, 1)
                _ucCalendar.EndDate = todaysDate.ToShortDateString
            Case range.YearToDate '"year to date"
                _ucCalendar.StartDate = DateSerial(Year(todaysDate), 1, 1)
                _ucCalendar.EndDate = todaysDate.ToShortDateString
            Case range.FirstQuarter '"1st quarter"
                _ucCalendar.StartDate = DateSerial(Year(todaysDate), 1, 1)
                _ucCalendar.EndDate = DateSerial(Year(todaysDate), 4, 0)
            Case range.SecondQuarter '"2nd quarter"
                _ucCalendar.StartDate = DateSerial(Year(todaysDate), 4, 1)
                _ucCalendar.EndDate = DateSerial(Year(todaysDate), 7, 0)
            Case range.ThirdQuarter '"3rd quarter"
                _ucCalendar.StartDate = DateSerial(Year(todaysDate), 7, 1)
                _ucCalendar.EndDate = DateSerial(Year(todaysDate), 10, 0)
            Case range.FourthQuarter '"4th quarter"
                _ucCalendar.StartDate = DateSerial(Year(todaysDate), 10, 1)
                _ucCalendar.EndDate = DateSerial(Year(todaysDate), 13, 0)
            Case range.EnteredLast7Days '"entered last 7 days"
                _ucCalendar.StartDate = todaysDate.AddDays(-7).ToShortDateString  'DateSerial(Year(todaysDate), Month(todaysDate), -7)
                _ucCalendar.EndDate = todaysDate.ToShortDateString
                'Remember to set the Last 7 Days Flag
            Case range.LastYear
                _ucCalendar.StartDate = DateSerial(Year(todaysDate) - 1, 1, 1)
                _ucCalendar.EndDate = DateSerial(Year(todaysDate) - 1, 12, 31)
            Case range.EndOfYear
                _ucCalendar.StartDate = DateSerial(Year(todaysDate), 1, 1)
                _ucCalendar.EndDate = DateSerial(Year(todaysDate), 12, 31)
            Case Else
        End Select
    End Sub
    Private Sub SetDefaultDateRange(ByVal dtRange As range)
        Dim todaysDate As Date = Now
        _ucCalendar.SelectedDateRange = dtRange
        'Select Case dtRange
        '    Case range.LastMonth
        '        _ucCalendar.DefaultStartDate = DateSerial(Year(todaysDate), Month(todaysDate) - 1, 1)
        '        _ucCalendar.DefaultEndDate = DateSerial(Year(todaysDate), Month(todaysDate), 0)
        '    Case range.Last3Months
        '        _ucCalendar.DefaultStartDate = DateSerial(Year(todaysDate), Month(todaysDate) - 3, 1)
        '        _ucCalendar.DefaultEndDate = DateSerial(Year(todaysDate), Month(todaysDate), 0)
        '    Case range.LastYearToDate '"last year to date"
        '        _ucCalendar.DefaultStartDate = DateSerial(Year(todaysDate) - 1, 1, 1)
        '        _ucCalendar.DefaultEndDate = todaysDate.ToShortDateString
        '    Case range.YearToDate '"year to date"
        '        _ucCalendar.DefaultStartDate = DateSerial(Year(todaysDate), 1, 1)
        '        _ucCalendar.DefaultEndDate = todaysDate.ToShortDateString
        '    Case range.FirstQuarter '"1st quarter"
        '        _ucCalendar.DefaultStartDate = DateSerial(Year(todaysDate), 1, 1)
        '        _ucCalendar.DefaultEndDate = DateSerial(Year(todaysDate), 4, 0)
        '    Case range.SecondQuarter '"2nd quarter"
        '        _ucCalendar.DefaultStartDate = DateSerial(Year(todaysDate), 4, 1)
        '        _ucCalendar.DefaultEndDate = DateSerial(Year(todaysDate), 7, 0)
        '    Case range.ThirdQuarter '"3rd quarter"
        '        _ucCalendar.DefaultStartDate = DateSerial(Year(todaysDate), 7, 1)
        '        _ucCalendar.DefaultEndDate = DateSerial(Year(todaysDate), 10, 0)
        '    Case range.FourthQuarter '"4th quarter"
        '        _ucCalendar.DefaultStartDate = DateSerial(Year(todaysDate), 10, 1)
        '        _ucCalendar.DefaultEndDate = DateSerial(Year(todaysDate), 13, 0)
        '    Case range.EnteredLast7Days '"entered last 7 days"
        '        _ucCalendar.DefaultStartDate = todaysDate.AddDays(-7).ToShortDateString  'DateSerial(Year(todaysDate), Month(todaysDate), -7)
        '        _ucCalendar.DefaultEndDate = todaysDate.ToShortDateString
        '        'Remember to set the Last 7 Days Flag
        '    Case range.LastYear
        '        _ucCalendar.DefaultStartDate = DateSerial(Year(todaysDate) - 1, 1, 1)
        '        _ucCalendar.DefaultEndDate = DateSerial(Year(todaysDate) - 1, 12, 31)
        '    Case range.EndOfYear
        '        _ucCalendar.DefaultStartDate = DateSerial(Year(todaysDate), 1, 1)
        '        _ucCalendar.DefaultEndDate = DateSerial(Year(todaysDate), 12, 31)
        '    Case Else
        'End Select
    End Sub
    Protected Sub _cblIncidentType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles _cblIncidentType.SelectedIndexChanged
        Dim itm As ListItem
        Try
            Dim targetEvent As String()
            Dim eventValue As String
            Dim selectedValue As String = String.Empty
            If Page.IsPostBack And Request.Form("__EventTarget") IsNot Nothing Then
                If Request.Form("__EventTarget").Contains("_cblIncidentType") Then
                    targetEvent = Request.Form("__EventTarget").Split("$")
                    eventValue = targetEvent(targetEvent.Length - 1).ToString
                    selectedValue = sender.items(eventValue).value.ToString
                Else
                    selectedValue = sender.selectedvalue
                    'Exit Sub
                End If
            End If

            If selectedValue = "All" Then

                For Each itm In sender.Items
                    itm.Selected = False
                Next
                itm = sender.Items.FindByValue("All")
                If itm IsNot Nothing Then
                    itm.Selected = True
                End If
            Else
                itm = sender.Items.FindByValue("All")
                If itm IsNot Nothing Then
                    itm.Selected = False
                End If
            End If
            If ReportTitle = ReportTitles.IncidentListing Or ReportTitle = ReportTitles.InvestigationExecutiveSummary Then
                Dim _rblRCFAStatus As RadioButtonList = Me.FindControl("_rblRCFAStatus")
                If _rblRCFAStatus IsNot Nothing Then
                    If sender.Items.FindByValue("All").Selected Or sender.Items.FindByValue("RCFA").Selected Then
                        _rblRCFAStatus.Visible = True
                        _rblRCFAStatus.Enabled = True
                    Else
                        _rblRCFAStatus.Visible = True
                        _rblRCFAStatus.Enabled = False
                    End If
                End If
            Else
                '_rblRCFAStatus.Visible = False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub AddControlToTable(ByRef wcNew As WebControl, ByRef tblRow As Integer, ByRef tblHeaderRow As Integer, ByRef tblCell As Integer, ByVal lblHeader As String, ByRef tbl As Table, ByVal mergeCells As Boolean, Optional ByVal sharedCell As Boolean = False, Optional ByVal labelSkin As String = "LabelWhite", Optional ByVal BackColor As String = "None", Optional ByVal HideHeaderRow As Boolean = False)
        Try
            Dim lbl As New Label
            lbl.Text = lblHeader
            lbl.SkinID = labelSkin

            If sharedCell = True Then
                If tblCell = 0 Then
                    tblCell = 1
                    tblRow -= 2
                    tblHeaderRow -= 2
                Else
                    tblCell = 0

                End If
            End If

            If mergeCells = True Then
                Dim span As Integer = tbl.Rows(tblHeaderRow).Cells.Count
                For i As Integer = tbl.Rows(tblHeaderRow).Cells.Count - 1 To tblCell Step -1
                    tbl.Rows(tblHeaderRow).Cells.RemoveAt(i)
                Next
                Dim cell As New TableCell
                cell.ColumnSpan = span
                cell.Width = Unit.Percentage(100)
                tbl.Rows(tblHeaderRow).CssClass = "Header"
                tbl.Rows(tblHeaderRow).Cells.Add(cell)

                cell = New TableCell
                cell.ColumnSpan = span
                cell.Width = Unit.Percentage(100)
                For i As Integer = tbl.Rows(tblRow).Cells.Count - 1 To 0 Step -1
                    tbl.Rows(tblRow).Cells.RemoveAt(i)
                Next

                tbl.Rows(tblRow).CssClass = "Border"
                tbl.Rows(tblRow).Cells.Add(cell)
                tblCell = 0
            Else
                tbl.Rows(tblHeaderRow).Cells(tblCell).Width = Unit.Percentage(50)
            End If
            wcNew.Visible = True
            tbl.Rows(tblHeaderRow).VerticalAlign = VerticalAlign.Top
            tbl.Rows(tblRow).VerticalAlign = VerticalAlign.Top
            tbl.Rows(tblHeaderRow).Cells(tblCell).HorizontalAlign = HorizontalAlign.Left
            tbl.Rows(tblHeaderRow).Cells(tblCell).Controls.Add(lbl)
            tbl.Rows(tblRow).Cells(tblCell).HorizontalAlign = HorizontalAlign.Left
            tbl.Rows(tblRow).Cells(tblCell).Controls.Add(wcNew)
            If tblCell = 1 Or mergeCells = True Then
                tblRow += 2
                tblHeaderRow += 2
            End If
            If mergeCells = True Then
                tblCell = 0
            Else
                If tblCell = 0 Then
                    tblCell = 1
                Else
                    tblCell = 0
                End If
            End If

        Catch ex As Exception
            Throw
        End Try

    End Sub
    Private Sub AddControlToTable(ByRef wcNew As UserControl, ByRef tblRow As Integer, ByRef tblHeaderRow As Integer, ByRef tblCell As Integer, ByVal lblHeader As String, ByRef tbl As Table, ByVal mergeCells As Boolean, Optional ByVal sharedCell As Boolean = False, Optional ByVal labelSkin As String = "LabelWhite", Optional ByVal BackColor As String = "None", Optional ByVal HideHeaderRow As Boolean = False)
        Try
            Dim lbl As New Label
            lbl.Text = lblHeader
            lbl.SkinID = labelSkin

            If sharedCell = True Then
                If tblCell = 0 Then
                    tblCell = 1
                    tblRow -= 2
                    tblHeaderRow -= 2
                Else
                    tblCell = 0

                End If
            End If
            If mergeCells = True Then
                Dim span As Integer = tbl.Rows(tblHeaderRow).Cells.Count
                For i As Integer = tbl.Rows(tblHeaderRow).Cells.Count - 1 To tblCell Step -1
                    tbl.Rows(tblHeaderRow).Cells.RemoveAt(i)
                Next
                'tbl.Rows(tblHeaderRow).Cells.RemoveAt(1)
                'tbl.Rows(tblHeaderRow).Cells.RemoveAt(0)
                Dim cell As New TableCell
                cell.ColumnSpan = span
                cell.Width = Unit.Percentage(100)
                tbl.Rows(tblHeaderRow).CssClass = "Header"
                tbl.Rows(tblHeaderRow).Cells.Add(cell)

                cell = New TableCell
                cell.ColumnSpan = span
                cell.Width = Unit.Percentage(100)
                For i As Integer = tbl.Rows(tblRow).Cells.Count - 1 To 0 Step -1
                    tbl.Rows(tblRow).Cells.RemoveAt(i)
                Next
                'tbl.Rows(tblRow).Cells.RemoveAt(1)
                'tbl.Rows(tblRow).Cells.RemoveAt(0)

                If BackColor = "White" Then
                    tbl.Rows(tblRow).BackColor = Drawing.Color.White
                End If

                tbl.Rows(tblRow).CssClass = "Border"
                tbl.Rows(tblRow).Cells.Add(cell)
                tblCell = 0
            Else
                tbl.Rows(tblHeaderRow).Cells(tblCell).Width = Unit.Percentage(50)
            End If
            wcNew.Visible = True
            tbl.Rows(tblHeaderRow).VerticalAlign = VerticalAlign.Top
            tbl.Rows(tblRow).VerticalAlign = VerticalAlign.Top
            tbl.Rows(tblHeaderRow).Cells(tblCell).HorizontalAlign = HorizontalAlign.Left

            If HideHeaderRow = "False" Then
                tbl.Rows(tblHeaderRow).Cells(tblCell).Controls.Add(lbl)
            Else
                tbl.Rows(tblHeaderRow).Visible = False
            End If

            tbl.Rows(tblRow).Cells(tblCell).HorizontalAlign = HorizontalAlign.Left
            tbl.Rows(tblRow).Cells(tblCell).Controls.Add(wcNew)
            If tblCell = 1 Or mergeCells = True Then
                tblRow += 2
                tblHeaderRow += 2
            End If
            If mergeCells = True Then
                tblCell = 0
            Else
                If tblCell = 0 Then
                    tblCell = 1
                Else
                    tblCell = 0
                End If
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Protected Sub _btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnSubmit.Click
        Try


            Dim r As New ReportParameters
            Dim criteria As SiteCriteria
            Dim requiredParms As RequiredReportParameters = Session.Item("requiredParms")
            'If requiredParms Is Nothing Then requiredParms = New RequiredReportParameters

            criteria = CType(Session("SiteCriteria"), SiteCriteria)
            If criteria Is Nothing Then criteria = New SiteCriteria

            If ReportTitles.ParetoChartsIncidents = ReportTitle Then
                If _rblChartType.SelectedValue = "Physical Root Cause" Then
                    ReportSortValue = Replace(ReportSortValue, "Latent", "Physical") '"ParetoChartPhysical" & Replace(ReportSortValue, " ", "")
                ElseIf _rblChartType.SelectedValue = "Latent Root Cause" Then
                    ReportSortValue = Replace(ReportSortValue, "Latent", "Latent") '"ParetoChartLatent" & Replace(ReportSortText, " ", "")
                ElseIf _rblChartType.SelectedValue = "Human Root Cause" Then
                    ReportSortValue = Replace(ReportSortValue, "Latent", "Human") '"ParetoChartHuman" & Replace(ReportSortText, " ", "")
                Else
                    ReportSortValue = Replace(ReportSortValue, "Latent", "Incident") '"ParetoChartIncident" & Replace(ReportSortText, " ", "")
                End If
            ElseIf ReportTitles.ReliabilityTrackingSystem = ReportTitle Then
                If Me._rblReportType.SelectedValue = "Reason Level Summary" Then
                    ReportSortValue = "RTSSummaryByReasonLevel"
                End If
            ElseIf ReportTitles.ManagementSummary = ReportTitle Then
                Select Case Me._rblReportType.SelectedValue
                    Case "Area"
                        ReportSortValue = "ManagementSummarybyArea"
                    Case "Business Unit/Area"
                        ReportSortValue = "ManagementSummarybyBusiness"
                    Case "Scorecard"
                        ReportSortValue = "ReliabilityReportingScorecard"
                    Case "MTBRR"
                        ReportSortValue = "MTBRR"
                End Select
            ElseIf ReportTitles.ReliabilityIndex = ReportTitle Then
                Select Case Me._rblDateRange.SelectedValue
                    Case "2 Year"
                        ReportSortValue = "ReliabilityIndex1YearPrior"
                    Case "3 Year"
                        ReportSortValue = "ReliabilityIndex2003"
                End Select
            ElseIf ReportTitles.EnterpriseSummaryReporting = ReportTitle Then
                'Display the Sharepoint reports
                Dim url As String = "http://itsp.ipaper.com/sites/ReliabilityIT/Shared%20Documents/Enterprise%20Summary%20Reliability%20Reporting/{0}"
                url = String.Format(url, Me._rblMonthlyReport.SelectedValue)
                Web.UI.ScriptManager.RegisterStartupScript(_upSite, _upSite.GetType, "pop", "PopupWindow('" & url & "','MonthlyReport',800,600,'yes','no','yes');", True)
                Exit Sub
                'ElseIf ReportTitles.ParetoChartsCMMS = ReportTitle Then
                '    'If requiredParms.CMMSFranklin = True Then
                '    '    r.Add(New ReportParameter("Report", Me._ddlCMMSFranklinReports.SelectedValue))
                '    '    r.Add(New ReportParameter("Area", Me._ddlCMMSFranklinArea.SelectedValue))
                '    '    r.Add(New ReportParameter("TimeFrame", Me._rblCMMSFranklinTime.SelectedValue))
                '    '    r.Add(New ReportParameter("ReportPage", "frmcrystalreportFRWO.aspx"))
                '    '    Session.Add("CrystalReport", r)
                '    '    Web.UI.ScriptManager.RegisterStartupScript(_upSite, _upSite.GetType, "pop", "PopupWindow('Report.aspx','CrystalReport',800,600,'yes','no','yes');", True)
                '    'Else
                '    'Dim url As String = "http://ridev/rireports/{0}_CMMS_Prod.pdf" ' "../../RIReports/{0}_CMMS_Prod.pdf" 
                '    Dim url As String = "../../RIReports/{0}_CMMS_Prod.pdf"
                '    url = String.Format(url, _ddlCMMSFacility.SelectedValue)
                '    Web.UI.ScriptManager.RegisterStartupScript(_upSite, _upSite.GetType, "pop", "PopupWindow('" & url & "','MonthlyReport',800,600,'yes','no','yes');", True)
                '    'End If
                '    Exit Sub
            ElseIf ReportTitles.AvailabilityMonthlySummary = ReportTitle Then
                ReportSortValue = "AvailabilityMonthlySummary"
             End If
            r.Add(New ReportParameter("Report", ReportSortValue))
            r.Add(New ReportParameter("ReportTitle", ReportTitle))

            'Common Params
            'If Me._ddlDivision.SelectedValue.Length > 0 Then
            If Me._ucSiteLocation.DivisionValue.Length > 0 Then
                r.Add(New ReportParameter("Division", Me._ucSiteLocation.DivisionValue))
                'r.Add(New ReportParameter("Division", Replace(Me._ucSiteLocation.DivisionValue, "&", "and")))
            Else
                r.Add(New ReportParameter("Division", "All"))
            End If

            If Me._ucSiteLocation.FacilityValue.Length > 0 And Me._ucSiteLocation.FacilityValue <> "AL" Then
                If ReportTitles.InvestigationExecutiveSummary = ReportTitle Then
                    r.Add(New ReportParameter("Site", Me._ucSiteLocation.FacilityValue))
                Else
                    Dim dr As OracleDataReader = Nothing
                    Dim sSiteName As String = "All"

                    dr = GetSiteName()
                    If Not dr Is Nothing Then
                        dr.Read()
                        If dr.HasRows = True Then
                            If Not dr.Item("SITENAME") Is Nothing Then
                                sSiteName = CStr(dr.Item("SITENAME")).Replace("&", "@@@")
                            End If
                        End If
                    End If
                    'r.Add(New ReportParameter("Site", Me._ucSiteLocation.FacilityName))
                    r.Add(New ReportParameter("Site", sSiteName))
                End If
            Else
                If ReportTitles.InvestigationExecutiveSummary = ReportTitle Then
                    r.Add(New ReportParameter("Site", "AL"))
                Else
                    r.Add(New ReportParameter("Site", "All"))
                End If
            End If



            If Me._ucSiteLocation.BusinessUnitValue.Length > 0 Then
                r.Add(New ReportParameter("BusUnit", Me._ucSiteLocation.BusinessUnitValue))
            Else
                r.Add(New ReportParameter("BusUnit", "All"))
            End If
            If Me._ucSiteLocation.AreaValue.Length > 0 Then
                r.Add(New ReportParameter("Area", Me._ucSiteLocation.AreaValue))
            Else
                r.Add(New ReportParameter("Area", "All"))
            End If
            If Me._ucSiteLocation.LineValue.Length > 0 Then
                r.Add(New ReportParameter("LineSystem", Me._ucSiteLocation.LineValue))
            Else
                r.Add(New ReportParameter("LineSystem", "All"))
            End If
            If criteria.BusType.Length > 0 Then
                r.Add(New ReportParameter("BusType", criteria.BusType))
            Else
                r.Add(New ReportParameter("BusType", "All"))
            End If
            If Me._ucSiteLocation.LineBreakValue.Length > 0 Then
                r.Add(New ReportParameter("LineBreak", Me._ucSiteLocation.LineBreakValue))
            Else
                r.Add(New ReportParameter("LineBreak", "All"))
            End If


            If ReportTitles.InvestigationExecutiveSummary = ReportTitle Then
                r.Add(New ReportParameter("Localename", ipLoc.CurrentLocale))
            End If

            If criteria.InActiveFlag.Length = 0 Then
                r.Add(New ReportParameter("InactiveFlag", "All"))
            Else
                r.Add(New ReportParameter("InactiveFlag", criteria.InActiveFlag))
            End If

            'Always move All to Person from report selection (CAC - 02/27/2012)
            'If Me._ddlPerson.SelectedValue.Length = 0 Then
            r.Add(New ReportParameter("Person", "All"))
            'Else
            '    r.Add(New ReportParameter("Person", Me._ddlPerson.SelectedValue))
            'End If
            'If Me._ddlPerson.SelectedIndex = -1 Then
            '    r.Add(New ReportParameter("PersonName", "All"))
            'Else
            '    r.Add(New ReportParameter("PersonName", Me._ddlPerson.SelectedItem.Text))
            'End If

            'Select Case ReportTitle
            '    Case ReportTitles.ActionItems

            If requiredParms.ActionItemStatus Then
                If Me._rblIncidentStatus.SelectedIndex = -1 Then
                    r.Add(New ReportParameter("Status", "Incomplete"))
                Else
                    r.Add(New ReportParameter("Status", Me._rblIncidentStatus.SelectedValue))
                End If
            End If

            If requiredParms.ReportType Then
                r.Add(New ReportParameter("ReportType", Me._rblReportType.SelectedValue))
            End If

            If requiredParms.ChronicType Then
                r.Add(New ReportParameter("ChronicType", Me._rblChronicType.SelectedValue))
            End If


            If Me._ucIncidentType.IRISNumber = "" Then
                r.Add(New ReportParameter("IRISNumber", 0))
            Else
                r.Add(New ReportParameter("IRISNumber", Me._ucIncidentType.IRISNumber))
            End If

            If Me._ucIncidentType.SearchMode = 1 Then
                r.Add(New ReportParameter("ANDOR", "AND"))
            Else
                r.Add(New ReportParameter("ANDOR", "OR"))
            End If

            If Me._ucIncidentType.Recordable = "" Then
                r.Add(New ReportParameter("Recordable", "All"))
            Else
                If Me._ucIncidentType.Recordable = "Yes" Then
                    r.Add(New ReportParameter("Recordable", "Yes"))
                Else
                    r.Add(New ReportParameter("Recordable", "No"))
                End If
            End If

            If Me._ucIncidentType.RCFA = "" Then
                r.Add(New ReportParameter("RCFA", "All"))
            Else
                If Mid(Me._ucIncidentType.RCFA, 1, 3) = "All" Then
                    r.Add(New ReportParameter("RCFA", "Yes"))
                Else
                    r.Add(New ReportParameter("RCFA", Me._ucIncidentType.RCFA))
                End If
            End If

            If Me._ucIncidentType.Chronic = "" Then
                'This option will not qualify anything for chronic
                r.Add(New ReportParameter("Chronic", "All"))
            Else
                If Mid(Me._ucIncidentType.Chronic, 1, 3) = "All" Then
                    'This option will pull all Chronic
                    r.Add(New ReportParameter("Chronic", "Yes"))
                Else
                    'This option will pull any or All options selected for Chronic
                    r.Add(New ReportParameter("Chronic", Me._ucIncidentType.Chronic))
                End If
            End If

            If Me._ucIncidentType.Quality = "" Then
                'This option will not qualify anything for Quality
                r.Add(New ReportParameter("Quality", "All"))
            Else
                If Mid(Me._ucIncidentType.Quality, 1, 3) = "All" Then
                    'This option will pull all Quality
                    r.Add(New ReportParameter("Quality", "Yes"))
                Else
                    'This option will pull any or All options selected for Quality
                    r.Add(New ReportParameter("Quality", Me._ucIncidentType.Quality))
                End If
            End If

            If Me._ucIncidentType.CertifiedKill = "" Then
                r.Add(New ReportParameter("CertifiedKill", "All"))
            Else
                If Me._ucIncidentType.CertifiedKill = "Yes" Then
                    r.Add(New ReportParameter("CertifiedKill", "Yes"))
                Else
                    r.Add(New ReportParameter("CertifiedKill", "No"))
                End If
            End If

            If Me._ucIncidentType.RTS = "" Then
                r.Add(New ReportParameter("RTS", "All"))
            Else
                If Me._ucIncidentType.RTS = "No" Then
                    r.Add(New ReportParameter("RTS", "No"))
                Else
                    r.Add(New ReportParameter("RTS", Me._ucIncidentType.RTS))
                End If
            End If

            If Me._ucIncidentType.PPR = "" Then
                r.Add(New ReportParameter("PPR", "All"))
            Else
                If Me._ucIncidentType.PPR = "No" Then
                    r.Add(New ReportParameter("PPR", "No"))
                Else
                    r.Add(New ReportParameter("PPR", "Yes"))
                End If
            End If


            'If Me._cblDateRange.Items.FindByValue("Next 7 Days").Selected = True Then
            '    r.Add(New ReportParameter("Next 7 Days", "Next 7 Days"))
            'Else
            '    r.Add(New ReportParameter("Next 7 Days", "No"))
            'End If

            If Me._ucIncidentType.ConstrainedAreas = "" Then
                r.Add(New ReportParameter("ConstrainedArea", "All"))
            Else
                r.Add(New ReportParameter("ConstrainedArea", "Yes"))

            End If



            If Me._ucIncidentType.SRR = "" Then
                r.Add(New ReportParameter("SRR", "All"))
            Else
                r.Add(New ReportParameter("SRR", Me._ucIncidentType.SRR))
            End If
            '    If Me._ucIncidentType.SRR = "Any Process DT >= 16 Hr" Then
            '        r.Add(New ReportParameter("SRR", "DNT"))
            '    Else
            '        If Me._ucIncidentType.SRR = "Financial Impact >= $100000" Then
            '            r.Add(New ReportParameter("SRR", "FIN"))
            '        Else
            '            r.Add(New ReportParameter("SRR", "BTH"))
            '        End If
            '    End If
            'End If

            If Me._ucIncidentType.SchedUnsched = "" Then
                r.Add(New ReportParameter("SchedDT", "All"))
            Else
                r.Add(New ReportParameter("SchedDT", Me._ucIncidentType.SchedUnsched))
            End If


            If Me._ucIncidentType.Safety = "" Then
                r.Add(New ReportParameter("Safety", "All"))
                r.Add(New ReportParameter("SafetyList", "All"))
            Else
                If Mid(Me._ucIncidentType.Safety, 1, 3) = "All" Then
                    r.Add(New ReportParameter("Safety", "YesAll"))
                    r.Add(New ReportParameter("SafetyList", "Yes"))
                Else
                    r.Add(New ReportParameter("Safety", "Yes"))
                    r.Add(New ReportParameter("SafetyList", Me._ucIncidentType.Safety))
                End If
            End If

            If InStr(Me._ucIncidentType.Safety, "Fire") > 0 Then
                r.Add(New ReportParameter("Fire", "*Fire*"))
            Else
                r.Add(New ReportParameter("Fire", "All"))
            End If
            If InStr(Me._ucIncidentType.Safety, "First Aid") > 0 Then
                r.Add(New ReportParameter("FirstAid", "*First Aid*"))
            Else
                r.Add(New ReportParameter("FirstAid", "All"))
            End If
            If InStr(Me._ucIncidentType.Safety, "Lost Work Day") > 0 Then
                r.Add(New ReportParameter("LostWorkDay", "*Lost Work Day*"))
            Else
                r.Add(New ReportParameter("LostWorkDay", "All"))
            End If
            If InStr(Me._ucIncidentType.Safety, "Motor Vehicle") > 0 Then
                r.Add(New ReportParameter("MotorVehicle", "*Motor Vehicle*"))
            Else
                r.Add(New ReportParameter("MotorVehicle", "All"))
            End If
            If InStr(Me._ucIncidentType.Safety, "Near Miss") > 0 Then
                r.Add(New ReportParameter("NearMiss", "*Near Miss*"))
            Else
                r.Add(New ReportParameter("NearMiss", "All"))
            End If
            If InStr(Me._ucIncidentType.Safety, "Property Damage") > 0 Then
                r.Add(New ReportParameter("PropertyDamage", "*Property Damage*"))
            Else
                r.Add(New ReportParameter("PropertyDamage", "All"))
            End If
            If InStr(Me._ucIncidentType.Safety, "Safety Recordable") > 0 Then
                r.Add(New ReportParameter("SFRecordable", "*Safety Recordable*"))
            Else
                r.Add(New ReportParameter("SFRecordable", "All"))
            End If
            If InStr(Me._ucIncidentType.Safety, "Safety Citation") > 0 Then
                r.Add(New ReportParameter("SFCitation", "*Safety Citation*"))
            Else
                r.Add(New ReportParameter("SFCitation", "All"))
            End If
            If InStr(Me._ucIncidentType.Safety, "Safety Inspection") > 0 Then
                r.Add(New ReportParameter("SFInspection", "*Safety Inspection*"))
            Else
                r.Add(New ReportParameter("SFInspection", "All"))
            End If
            If InStr(Me._ucIncidentType.Safety, "Safety Complaint") > 0 Then
                r.Add(New ReportParameter("SFComplaint", "*Safety Complaint*"))
            Else
                r.Add(New ReportParameter("SFComplaint", "All"))
            End If
            If InStr(Me._ucIncidentType.Safety, ",Release") > 0 Or Mid(Me._ucIncidentType.Safety, 1, 7) = "Release" Then
                r.Add(New ReportParameter("Release", "*Release*"))
            Else
                r.Add(New ReportParameter("Release", "All"))
            End If
            If InStr(Me._ucIncidentType.Safety, "Reportable Release") > 0 Then
                r.Add(New ReportParameter("RepRelease", "*Reportable Release*"))
            Else
                r.Add(New ReportParameter("RepRelease", "All"))
            End If
            If InStr(Me._ucIncidentType.Safety, "Permit Excursion") > 0 Then
                r.Add(New ReportParameter("PermitExcursion", "*Permit Excursion*"))
            Else
                r.Add(New ReportParameter("PermitExcursion", "All"))
            End If
            If InStr(Me._ucIncidentType.Safety, "Spill") > 0 Then
                r.Add(New ReportParameter("Spill", "*Spill*"))
            Else
                r.Add(New ReportParameter("Spill", "All"))
            End If
            If InStr(Me._ucIncidentType.Safety, "Environmental Consent Decree") > 0 Then
                r.Add(New ReportParameter("ENVConsentDecree", "*Environmental Consent Decree*"))
            Else
                r.Add(New ReportParameter("ENVConsentDecree", "All"))
            End If
            If InStr(Me._ucIncidentType.Safety, "Environmental Inspection") > 0 Then
                r.Add(New ReportParameter("ENVInspection", "*Environmental Inspection*"))
            Else
                r.Add(New ReportParameter("ENVInspection", "All"))
            End If
            If InStr(Me._ucIncidentType.Safety, "Environmental Audit") > 0 Then
                r.Add(New ReportParameter("ENVAudit", "*Environmental Audit*"))
            Else
                r.Add(New ReportParameter("ENVAudit", "All"))
            End If
            If InStr(Me._ucIncidentType.Safety, "Environmental Complaint") > 0 Then
                r.Add(New ReportParameter("ENVComplaint", "*Environmental Complaint*"))
            Else
                r.Add(New ReportParameter("ENVComplaint", "All"))
            End If
            If InStr(Me._ucIncidentType.Safety, "Environmental Notice") > 0 Then
                r.Add(New ReportParameter("ENVNotice", "*Environmental Notice*"))
            Else
                r.Add(New ReportParameter("ENVNotice", "All"))
            End If
            If InStr(Me._ucIncidentType.Safety, "Other") > 0 Then
                r.Add(New ReportParameter("Other", "*Other*"))
            Else
                r.Add(New ReportParameter("Other", "All"))
            End If


            'If Me._cblIncidentType.Items.FindByValue("Top Chronic") IsNot Nothing Then
            '    If Me._cblIncidentType.Items.FindByValue("Top Chronic").Selected = True Then
            '        r.Add(New ReportParameter("TopChronic", "TopChronic"))
            '    Else
            '        r.Add(New ReportParameter("TopChronic", "No"))
            '    End If
            'Else
            '    r.Add(New ReportParameter("TopChronic", "No"))
            'End If

            'If Me._cblIncidentType.Items.FindByValue("Safety") IsNot Nothing Then
            '    If Me._cblIncidentType.Items.FindByValue("Safety").Selected = True Then
            '        r.Add(New ReportParameter("Safety", "Safety"))
            '    Else
            '        r.Add(New ReportParameter("Safety", "No"))
            '    End If
            'Else
            '    r.Add(New ReportParameter("Safety", "No"))
            'End If

            'If Me._cblIncidentType.Items.FindByValue("Quality") IsNot Nothing Then
            '    If Me._cblIncidentType.Items.FindByValue("Quality").Selected = True Then
            '        r.Add(New ReportParameter("Quality", "Quality"))
            '    Else
            '        r.Add(New ReportParameter("Quality", "No"))
            '    End If
            'Else
            '    r.Add(New ReportParameter("Quality", "No"))
            'End If

            'If Me._cblIncidentType.Items.FindByValue("Automated Reminder") IsNot Nothing Then
            '    If Me._cblIncidentType.Items.FindByValue("Automated Reminder").Selected = True Then
            '        r.Add(New ReportParameter("AutoRemind", "AutoRemind"))
            '    Else
            '        r.Add(New ReportParameter("AutoRemind", "No"))
            '    End If
            'Else
            '    r.Add(New ReportParameter("AutoRemind", "No"))
            'End If

            If requiredParms.RTSIncidentType = True Then
                If Me._rblRTSIncidentType.SelectedIndex = -1 Then
                    r.Add(New ReportParameter("IncidentType", "All"))
                Else
                    r.Add(New ReportParameter("IncidentType", Me._rblRTSIncidentType.SelectedValue))
                End If
            End If
            If requiredParms.DownTime = True Then
                If Me._rblDownTime.SelectedIndex = -1 Then
                    r.Add(New ReportParameter("DownTime", "All"))
                Else
                    r.Add(New ReportParameter("DownTime", Me._rblDownTime.SelectedValue))
                End If
            End If
            If requiredParms.ReasonLevel = True Then
                If Me._rblReasonLevel.SelectedIndex = -1 Then
                    r.Add(New ReportParameter("ReasonLevel", "All"))
                Else
                    r.Add(New ReportParameter("ReasonLevel", Me._rblReasonLevel.SelectedValue))
                End If
            End If
            If requiredParms.EstimatedDueDate Then
                If Me._cblDateRange.SelectedIndex = -1 Then
                    r.Add(New ReportParameter("EstDueDate", "All"))
                    r.Add(New ReportParameter("Overdue", "No"))
                    r.Add(New ReportParameter("Next 7 Days", "No"))
                    r.Add(New ReportParameter("Next 14 Days", "No"))
                    r.Add(New ReportParameter("Next 30 Days", "No"))
                Else
                    If Me._cblDateRange.Items.FindByValue("All").Selected = True Then
                        r.Add(New ReportParameter("EstDueDate", "All"))
                    Else
                        r.Add(New ReportParameter("EstDueDate", "No"))
                    End If

                    If Me._cblDateRange.Items.FindByValue("Overdue").Selected = True Then
                        r.Add(New ReportParameter("Overdue", "Overdue"))
                    Else
                        r.Add(New ReportParameter("Overdue", "No"))
                    End If
                    If Me._cblDateRange.Items.FindByValue("Next 7 Days").Selected = True Then
                        r.Add(New ReportParameter("Next 7 Days", "Next 7 Days"))
                    Else
                        r.Add(New ReportParameter("Next 7 Days", "No"))
                    End If
                    If Me._cblDateRange.Items.FindByValue("Next 14 Days").Selected = True Then
                        r.Add(New ReportParameter("Next 14 Days", "Next 14 Days"))
                    Else
                        r.Add(New ReportParameter("Next 14 Days", "No"))
                    End If
                    If Me._cblDateRange.Items.FindByValue("Next 30 Days").Selected = True Then
                        r.Add(New ReportParameter("Next 30 Days", "Next 30 Days"))
                    Else
                        r.Add(New ReportParameter("Next 30 Days", "No"))
                    End If
                End If
            End If

            'If requiredParms.Calendar And requiredParms.EstimatedDueDate Then
            '    r.Add(New ReportParameter("StartDate", Me._ucCalendar.StartDate))
            '    r.Add(New ReportParameter("EndDate", Me._ucCalendar.EndDate))
            'Else
            '    r.Add(New ReportParameter("StartDate", Me._DateRange.StartDate))
            '    r.Add(New ReportParameter("EndDate", Me._DateRange.EndDate))
            'End If

            'Cathy Cox 08/28/13 Added parameter changes for BI4 upgrade to Open Doc methodology

            r.Add(New ReportParameter("personname", "All"))
            r.Add(New ReportParameter("enteredlast7days", "All"))
            'CStr(Convert_Date(Me._DateRange.StartDate).Month)
            If requiredParms.Calendar And requiredParms.EstimatedDueDate Then
                r.Add(New ReportParameter("startmonth", CStr(Convert_Date(Me._ucCalendar.StartDate).Month)))
                r.Add(New ReportParameter("startday", CStr(Convert_Date(Me._ucCalendar.StartDate).Day)))
                r.Add(New ReportParameter("startyear", CStr(Convert_Date(Me._ucCalendar.StartDate).Year)))
                r.Add(New ReportParameter("endmonth", CStr(Convert_Date(Me._ucCalendar.EndDate).Month)))
                r.Add(New ReportParameter("endday", CStr(Convert_Date(Me._ucCalendar.EndDate).Day)))
                r.Add(New ReportParameter("endyear", CStr(Convert_Date(Me._ucCalendar.EndDate).Year)))
            Else
                r.Add(New ReportParameter("startmonth", CStr(Convert_Date(Me._DateRange.StartDate).Month)))
                r.Add(New ReportParameter("startday", CStr(Convert_Date(Me._DateRange.StartDate).Day)))
                r.Add(New ReportParameter("startyear", CStr(Convert_Date(Me._DateRange.StartDate).Year)))
                r.Add(New ReportParameter("endmonth", CStr(Convert_Date(Me._DateRange.EndDate).Month)))
                r.Add(New ReportParameter("endday", CStr(Convert_Date(Me._DateRange.EndDate).Day)))
                r.Add(New ReportParameter("endyear", CStr(Convert_Date(Me._DateRange.EndDate).Year)))
            End If

            If requiredParms.RCFAStatus Then
                If Me._rblRCFAStatus.SelectedIndex = -1 Then
                    r.Add(New ReportParameter("Status", "All"))
                Else
                    '03/12/14 removed dependence on RCFA field, also removed replace for & - no longer needed CAC
                    'If Me._cblIncidentType.Items.FindByValue("All").Selected = True Or Me._cblIncidentType.Items.FindByValue("RCFA").Selected Then
                    'If Me._ucIncidentType.RCFA <> "No" And Me._ucIncidentType.RCFA <> "" Then
                    'End If
                    'If Me._cblIncidentType.SelectedValue = "All" Or Me._cblIncidentType.SelectedValue = "All" Then
                    'r.Add(New ReportParameter("Division", Replace(Me._ucSiteLocation.DivisionValue, "&", "and")))
                    r.Add(New ReportParameter("Status", Me._rblRCFAStatus.SelectedValue))
                    'r.Add(New ReportParameter("Status", Replace(Me._rblRCFAStatus.SelectedValue, "&", "and")))
                    'Else
                    'r.Add(New ReportParameter("Status", "All"))
                    'End If
                End If
            End If

            If requiredParms.ActionItemsListed Then
                If Me._rblActionItems.SelectedIndex = -1 Then
                    r.Add(New ReportParameter("ActionsListed", "Incomplete"))
                Else
                    r.Add(New ReportParameter("ActionsListed", Me._rblActionItems.SelectedValue))
                End If
            End If

            If requiredParms.DateRange Then
                If Me._rblDateRange.SelectedValue = "Entered Last 7 Days" Then
                    r.Add(New ReportParameter("EnteredLast7Days", "Yes"))
                Else
                    r.Add(New ReportParameter("EnteredLast7Days", "All"))
                End If
            End If

            If requiredParms.ChartType Then
                If Me._rblChartType.SelectedIndex = -1 Then
                    r.Add(New ReportParameter("ChartType", "Latent Root Cause"))
                Else
                    r.Add(New ReportParameter("ChartType", Me._rblChartType.SelectedValue))
                End If
            End If


            Session.Add("CrystalReport", r)
            Web.UI.ScriptManager.RegisterStartupScript(_upSite, _upSite.GetType, "pop", "PopupWindow('Report.aspx','CrystalReport',800,600,'yes','no','yes');", True)

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Protected Sub _btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnReset.Click
        Response.Redirect("Reporting.aspx", False)
        'SetSiteDefaults()
        'SetReportDefaults()
        'Me._ucCalendar.resetcalendar()
    End Sub

    Protected Sub _ucCalendar_CalendarUpdated() Handles _ucCalendar.DateRangeChanged 'CalendarUpdated
        Me._rblDateRange.SelectedIndex = -1
    End Sub

    Private Sub SetDDLProperCase(ByRef sender As DropDownList)
        Try
            For i As Integer = 0 To sender.Items.Count - 1
                sender.Items(i).Text = SharedFunctions.ProperCase(sender.Items(i).Text)
            Next
        Catch ex As Exception
            Throw New Exception("SetDDLProperCase", ex.InnerException)
        End Try
    End Sub
    Enum ClientControls
        IncidentType = 1
        EstimatedDueDate = 2
    End Enum

    Private Function AddClientScript(ByVal controlID As ClientControls) As String
        Dim sb As New StringBuilder

        Select Case controlID
            Case ClientControls.IncidentType
                'sb.Append("function IncidentTypeOnClick(listCount){")
                'sb.Append(" if (event.srcElement.id=='")
                'sb.Append(frm)
                'sb.Append("cblIncidentType_0')")
                'sb.Append("{for (i = 1; i<listCount;i++){")
                'sb.Append("var cbl = document.getElementById('ctl00__cphMain__ucSiteDropdowns__cblIncidentType_' + i);  ")
                'sb.Append(" if (cbl != null){cbl.checked=false;}}}")
                'sb.Append(" else {var cbl = document.getElementById('ctl00__cphMain__ucSiteDropdowns__cblIncidentType_0' );  ")

                'sb.Append(" if (cbl != null){cbl.checked=false;}}}")
                'Page.ClientScript.RegisterClientScriptBlock(Page.GetType, "IncidentTypeOnClick", sb.ToString, False)
                'Web.UI.ScriptManager.RegisterStartupScript(Me._udpReportParameters, Page.GetType, "IncidentTypeOnClick", sb.ToString, True)
                sb.Length = 0
                sb.Append("IncidentTypeOnClick('")
                sb.Append(Me._cblIncidentType.Items.Count.ToString)
                sb.Append("')")
            Case ClientControls.EstimatedDueDate
                sb.Append("EstimatedDueDateOnClick('")
                sb.Append(Me._cblDateRange.Items.Count.ToString)
                sb.Append("')")
        End Select
        Return sb.ToString
    End Function
    Public Function GetSiteName() As OracleDataReader
        Dim conCust As Devart.Data.Oracle.OracleConnection = Nothing
        Dim cmdSQL As OracleCommand = Nothing
        Dim connection As String = String.Empty
        Dim provider As String = String.Empty
        Dim ds As DataSet = Nothing
        Dim daData As OracleDataAdapter = Nothing
        Dim cnConnection As Devart.Data.Oracle.OracleConnection = Nothing
        Dim userDomain As String() = Nothing
        Dim dr As OracleDataReader = Nothing

        Try
            If connection.Length = 0 Then
                connection = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
            End If
            If provider.Length = 0 Then
                provider = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ProviderName
            End If

            cmdSQL = New OracleCommand
            With cmdSQL
                cnConnection = New OracleConnection(connection)
                cnConnection.Open()
                .Connection = cnConnection
                .CommandText = "ri.GetSiteName"
                .CommandType = CommandType.StoredProcedure
                Dim param As New OracleParameter

                param.ParameterName = "insiteid"
                param.OracleDbType = OracleDbType.VarChar
                param.Value = Me._ucSiteLocation.FacilityValue
                param.Direction = ParameterDirection.Input
                .Parameters.Add(param)

                param = New OracleParameter
                param.ParameterName = "rssitename"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = ParameterDirection.Output
                .Parameters.Add(param)

            End With

            dr = cmdSQL.ExecuteReader(CommandBehavior.CloseConnection)
        Catch ex As Exception
            Return Nothing
            Throw New DataException("GetSiteName", ex)
            If Not conCust Is Nothing Then conCust = Nothing
        Finally
            GetSiteName = dr
            If Not daData Is Nothing Then daData = Nothing
            If Not ds Is Nothing Then ds = Nothing
            If Not cmdSQL Is Nothing Then cmdSQL = Nothing
            'cnConnection.Close()
        End Try
    End Function
End Class
