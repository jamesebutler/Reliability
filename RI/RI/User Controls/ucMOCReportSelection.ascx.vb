Imports System.Data
Imports Devart.Data.Oracle
Imports RI


Partial Class ucMOCReports
    Inherits System.Web.UI.UserControl
    <Serializable()> _
    Friend Class SiteCriteria

        Public Sub New()
            Facility = String.Empty
            'FacilityName = String.Empty
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
        'Public Property FacilityName() As String
        '    Get
        '        Return mFacilityName
        '    End Get
        '    Set(ByVal value As String)
        '        mFacilityName = value
        '    End Set
        'End Property
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
    'Public Const Person As String = "PERSON"
    Private mReportTitle As String
    Private mReportSortValue As String
    Private mReportSortText As String
    Private mReportName As String
    Private mReportInactiveFlag As String

    'Dim _rblIncidentStatus As New RadioButtonList
    'Dim _ddlPerson As New DropDownList
    Dim _cblDateRange As New CheckBoxList
    Dim _cblCategory As New CheckBox
    Dim _cblSubCategory As New CheckBoxList
    Dim _cblClassification As New CheckBoxList
    Dim _rblChartType As New RadioButtonList

    Dim _rblDateRange As New RadioButtonList
    Dim _txtParetoNumbers As New TextBox

    Dim _ddlFacility As New DropDownList
    Dim _ddlDivision As New DropDownList
    Dim _ddlLineBreak As New DropDownList
    Dim _ddlLine As New DropDownList
    Dim _ddlBusinessUnit As New DropDownList
    Dim _ddlArea As New DropDownList
    Dim _ifrDownTime As New Web.UI.WebControls.Panel

    Structure ReportParameterType
        Const Calendar As String = "Calendar"
        Const Category As String = "Category"
        Const Classification As String = "Classification"
        Const DateRange As String = "Date Range"
        Const Division As String = "Division"
        Const Facility As String = "Facility"
        Const Site As String = "Site"
        Const ChartType As String = "Chart Type"
        Const TaskActionItems As String = "Task Item Listing"
        Const Type As String = "Type"
        Const MOCDetail As String = "MOCDetail"
        Private Active As Boolean
    End Structure

    Structure RequiredReportParameters
        Dim Calendar As Boolean
        Dim Category As Boolean
        Dim Classification As Boolean
        Dim DateRange As Boolean
        Dim ChartType As Boolean
        Dim Division As Boolean
        Dim Facility As Boolean
        Dim Site As Boolean
        Dim TaskActionItems As Boolean
        Dim Type As Boolean
        Dim MOCDetail As Boolean
    End Structure

    Structure ReportTitles
        Const MOCListing As String = "MOC Listing"
        Const MOCListingTasks As String = "MOC Listing with Tasks"
        Const MOCParetoCharts As String = "Pareto Charts - MOC"
        Const MOCPendingApproval As String = "MOC Pending Approval Review"
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

    Public Property ReportBOETitle() As String
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
            key = "MOC_REPORTPARAMETERS"
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
                .CommandText = "MOCReports.ReportParameters"
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
                .CommandText = "MOCReports.ReportDDL"
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
        Dim savedXML As String = String.Empty
        Dim io As New System.IO.StringWriter
        Dim User As System.Security.Principal.IPrincipal
        User = System.Web.HttpContext.Current.User

        Dim username As String
        username = CurrentUserProfile.GetCurrentUser

        Dim userProfile As CurrentUserProfile = CType(Session.Item(Replace(username, "\", "_")), CurrentUserProfile)
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
        Dim ds As DataSet = Nothing
        Dim savedXML As String = String.Empty
        Dim io As New System.IO.StringWriter

        Dim username As String
        username = CurrentUserProfile.GetCurrentUser

        Dim userProfile As CurrentUserProfile = Nothing
        userProfile = RI.SharedFunctions.GetUserProfile
        If Session.Item(Replace(username, "\", "_")) IsNot Nothing Then userProfile = CType(Session.Item(Replace(username, "\", "_")), CurrentUserProfile)

        Try
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
        Catch ex As Exception
            Throw
        Finally
            If Not ds Is Nothing Then ds = Nothing
        End Try
    End Sub

    Private Sub PopulateReportParameters(ByVal criteria As SiteCriteria)
        Dim ds As DataSet = Nothing
        Dim rowFilter As String = "REPORTPARAMETERTYPE = '{0}' and REPORTTITLE='{1}'"
        Dim requiredParms As New RequiredReportParameters
        Dim io As New System.IO.StringWriter
        Dim trigger As New AsyncPostBackTrigger
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
                Else
                    requiredParms.Site = False
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
                End If

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
                        url = "http://gpitasktracker.graphicpkg.com/ReportSelection.aspx?rn=Task Item Listing&sv=" & ReportBOETitle
                        ' url = ds.Tables(0).DefaultView.Item(0).Item("ReportParameters").ToString & "&sv=" & ReportBOETitle
                    Else
                        url = "http://gpitasktracker.graphicpkg.com/ReportSelection.aspx?rn=Task Item Listing&sv=" & ReportBOETitle
                        'url = "http://ridev/TaskTracker/ReportSelection.aspx?rn=Task Item Listing&sv=" & ReportBOETitle
                    End If
                    iframe.InnerHtml = "<iframe src='" & url & "' border='0' frameborder='0' width='100%' height='600px'/>"
                    _ifrDownTime.Controls.Add(iframe)
                    _ifrDownTime.CssClass = "iframe"
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

                'Category                               
                ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.Category, Me.ReportTitle)
                ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
                If ds.Tables(0).DefaultView.Count > 0 Then
                    requiredParms.Category = True
                End If

                'Classification                               
                ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.Classification, Me.ReportTitle)
                ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
                If ds.Tables(0).DefaultView.Count > 0 Then
                    requiredParms.Classification = True
                End If

                'Type                               
                ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.Type, Me.ReportTitle)
                ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
                If ds.Tables(0).DefaultView.Count > 0 Then
                    requiredParms.Type = True
                End If

                'MOC Detail                               
                ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.MOCDetail, Me.ReportTitle)
                ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
                If ds.Tables(0).DefaultView.Count > 0 Then
                    requiredParms.MOCDetail = True
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
                    'If Request.Form(_rblChartType.UniqueID.ToString) IsNot Nothing Then
                    '    _rblChartType.SelectedValue = Request.Form(_rblChartType.UniqueID.ToString)
                    'End If
                    'If _rblChartType.Items.FindByValue("Human Root Cause") IsNot Nothing Then
                    '    _rblChartType.Items.FindByValue("Human Root Cause").Enabled = False
                    'End If
                End If
            End If
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
    'Private Sub PopulateHelp(ByVal name As Insructions)
    '    Dim sb As New StringBuilder
    '    Select Case name
    '        Case Insructions.FranklinCMMS
    '            sb.Append("<h3>Please select a report, an Area and Time Frame and click on the Report</h3>")
    '            sb.Append("<ul><li>The paretos represent the labor costs with blue bar and number of work orders with a red line. </li>")
    '            sb.Append("<li>The material cost of these work orders is shown in the individual work order detail (not represented on the graph).</li>")
    '            sb.Append("<li>Repair work orders pareto include emergency, break-in, and corrective action work orders.</li>")
    '            sb.Append("<li>All work orders pareto displays all labor cost associated with all work orders (including indirect, capital, safety, contractor and standard PM tasks.</li></ul>")
    '            Me._liInstructions.Text = sb.ToString
    '        Case Else
    '            Me._liInstructions.Text = sb.ToString
    '    End Select
    'End Sub
    Private Sub DisplayReportParameters()
        Dim tblRow As Integer = 1
        Dim tblHeaderrow As Integer = 0
        Dim tblCell As Integer = 0
        Dim totalTblRows As Integer = _tblMain.Rows.Count - 1
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
            'If requiredParms.Category = True Then
            'Me.AddControlToTable(_ucMOCCategory, tblRow, tblHeaderrow, tblCell, ReportParameterType.Category, _tblMain, True, False, , , True)
            'End If

            If requiredParms.DateRange = True Then
                Me.AddControlToTable(_DateRange, tblRow, tblHeaderrow, tblCell, ReportParameterType.DateRange, _tblMain, True, False)
            End If

            If requiredParms.Type = True Then
                Me.AddControlToTable(_ucMOCType, tblRow, tblHeaderrow, tblCell, ReportParameterType.Type, _tblMain, True, False, , , True)
            End If

            If requiredParms.Classification = True Then
                Me.AddControlToTable(_ucMOCClassification, tblRow, tblHeaderrow, tblCell, ReportParameterType.Classification, _tblMain, True, False, , , True)
            End If
            
            If requiredParms.ChartType = True Then
                Me.AddControlToTable(_rblChartType, tblRow, tblHeaderrow, tblCell, ReportParameterType.ChartType, _tblMain, True, False, , , True)
            End If
            If requiredParms.TaskActionItems = True Then
                AddControlToTable(_ifrDownTime, tblRow, tblHeaderrow, tblCell, ReportParameterType.TaskActionItems, _tblMain, True)
            End If

            If requiredParms.Category = True Then
                Me._ucMOCCategory.Visible = "true"
            Else
                Me._ucMOCCategory.Visible = "false"
            End If

            If requiredParms.MOCDetail = True Then
                Me._trMOCDetail.Visible = "true"
            Else
                Me._trMOCDetail.Visible = "false"
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

        Dim userProfile As CurrentUserProfile = CType(Session.Item(Replace(username, "\", "_")), CurrentUserProfile)
        If userProfile IsNot Nothing Then
            'If _ddlDivision.Items.FindByValue(userProfile.DefaultDivision) IsNot Nothing Then
            '    _ddlDivision.SelectedValue = userProfile.DefaultDivision
            'End If
            'If _ddlFacility.Items.FindByValue(userProfile.DefaultFacility) IsNot Nothing Then
            '    _ddlFacility.SelectedValue = userProfile.DefaultFacility
            'End If
            'If _upSite.UpdateMode = UpdatePanelUpdateMode.Conditional Then
            '    Me._upSite.Update()
            'End If
        End If
    End Sub

    Public Sub SetReportDefaults()
        Try

            Select Case ReportTitle
                Case ReportTitles.MOCListing
                    
                    Me._DateRange.SelectedDateRange = RI_User_Controls_Common_ucDateRange.range.Last12Months

                    'If Me._cblDateRange.SelectedIndex = -1 Then
                    If Me._cblDateRange.Items.FindByValue("All") IsNot Nothing Then
                        Me._cblDateRange.SelectedIndex = -1
                        Me._cblDateRange.Items.FindByValue("All").Selected = True
                    End If
                    'End If
              
                Case ReportTitles.MOCParetoCharts
                    Me._DateRange.SelectedDateRange = RI_User_Controls_Common_ucDateRange.range.Last12Months
                    Me._rblChartType.Items.FindByValue("Category").Selected = True

            End Select
        Catch ex As Exception
            Throw
        End Try
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Page.IsPostBack Then
                If ReportTitle.Length > 0 Then
                    Me.PopulateSiteDropdown()
                    Me.PopulateDropdowns()
                End If
            End If
            _cblDateRange.EnableViewState = False ' As New CheckBoxList
            _cblCategory.EnableViewState = False ' As New CheckBoxList
            _cblSubCategory.EnableViewState = False ' As New CheckBoxList
            _rblDateRange.EnableViewState = False ' As New RadioButtonList
            _cblClassification.EnableViewState = False ' As New RadioButtonList
            'Dim sb As New StringBuilder
            'sb.Append("DisplayReport();")
            'ScriptManager.RegisterClientScriptInclude(Me._udpReportParameters, _udpReportParameters.GetType, "ReportSelection", ("User Controls/ReportSelection.js"))
            ScriptManager.RegisterClientScriptInclude(_upSite.Page, _upSite.GetType, "ReportSelection", Page.ResolveClientUrl("~/ri/User Controls/ReportSelection.js"))
            ScriptManager.RegisterClientScriptInclude(_upSite.Page, _upSite.GetType, "MOC", Page.ResolveClientUrl("~/MOC/MOC.js"))

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub _ddlFacility_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'ViewState.Remove("Person")
        'PopulateSiteDropdown()
        'Me._udpReportParameters.Update()
    End Sub
    Protected Sub _cblDateRange_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles _cblDateRange.SelectedIndexChanged
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

        Select Case dtRange
            Case range.LastMonth
                _ucCalendar.DefaultStartDate = DateSerial(Year(todaysDate), Month(todaysDate) - 1, 1)
                _ucCalendar.DefaultEndDate = DateSerial(Year(todaysDate), Month(todaysDate), 0)
            Case range.Last3Months
                _ucCalendar.DefaultStartDate = DateSerial(Year(todaysDate), Month(todaysDate) - 3, 1)
                _ucCalendar.DefaultEndDate = DateSerial(Year(todaysDate), Month(todaysDate), 0)
            Case range.LastYearToDate '"last year to date"
                _ucCalendar.DefaultStartDate = DateSerial(Year(todaysDate) - 1, 1, 1)
                _ucCalendar.DefaultEndDate = todaysDate.ToShortDateString
            Case range.YearToDate '"year to date"
                _ucCalendar.DefaultStartDate = DateSerial(Year(todaysDate), 1, 1)
                _ucCalendar.DefaultEndDate = todaysDate.ToShortDateString
            Case range.FirstQuarter '"1st quarter"
                _ucCalendar.DefaultStartDate = DateSerial(Year(todaysDate), 1, 1)
                _ucCalendar.DefaultEndDate = DateSerial(Year(todaysDate), 4, 0)
            Case range.SecondQuarter '"2nd quarter"
                _ucCalendar.DefaultStartDate = DateSerial(Year(todaysDate), 4, 1)
                _ucCalendar.DefaultEndDate = DateSerial(Year(todaysDate), 7, 0)
            Case range.ThirdQuarter '"3rd quarter"
                _ucCalendar.DefaultStartDate = DateSerial(Year(todaysDate), 7, 1)
                _ucCalendar.DefaultEndDate = DateSerial(Year(todaysDate), 10, 0)
            Case range.FourthQuarter '"4th quarter"
                _ucCalendar.DefaultStartDate = DateSerial(Year(todaysDate), 10, 1)
                _ucCalendar.DefaultEndDate = DateSerial(Year(todaysDate), 13, 0)
            Case range.EnteredLast7Days '"entered last 7 days"
                _ucCalendar.DefaultStartDate = todaysDate.AddDays(-7).ToShortDateString  'DateSerial(Year(todaysDate), Month(todaysDate), -7)
                _ucCalendar.DefaultEndDate = todaysDate.ToShortDateString
                'Remember to set the Last 7 Days Flag
            Case range.LastYear
                _ucCalendar.DefaultStartDate = DateSerial(Year(todaysDate) - 1, 1, 1)
                _ucCalendar.DefaultEndDate = DateSerial(Year(todaysDate) - 1, 12, 31)
            Case range.EndOfYear
                _ucCalendar.DefaultStartDate = DateSerial(Year(todaysDate), 1, 1)
                _ucCalendar.DefaultEndDate = DateSerial(Year(todaysDate), 12, 31)
            Case Else
        End Select
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

            If ReportTitles.MOCListing = ReportTitle Then
                ReportBOETitle = "MOC Listing"
                ReportTitle = "MOC Listing"
            End If

            If ReportTitles.MOCListingTasks = ReportTitle Then
                ReportBOETitle = "MOC Listing with Tasks"
                ReportTitle = "MOC Listing with Tasks"
            End If

            If ReportTitles.MOCParetoCharts = ReportTitle Then
                ReportBOETitle = "MOCParetoCharts" & Replace(ReportSortText, " ", "")
                ReportTitle = "Pareto Charts - MOC"
            End If
          
            If ReportTitles.MOCPendingApproval = ReportTitle Then
                ReportBOETitle = "MOC Pending Approval Review" & Replace(ReportSortText, " ", "")
                ReportTitle = "MOC Pending Approval Review"
            End If

            r.Add(New ReportParameter("Report", ReportBOETitle))
            r.Add(New ReportParameter("ReportTitle", ReportTitle))

            'Common Params
            'CAC 09/09/13 Updated parameters for BI4 upgrade

            'If Me._ddlDivision.SelectedValue.Length > 0 Then
            If Me._ucSiteLocation.DivisionValue.Length > 0 Then
                'r.Add(New ReportParameter("Division", Replace(Me._ucSiteLocation.DivisionValue, "&", "and")))
                r.Add(New ReportParameter("in_Division", Me._ucSiteLocation.DivisionValue))
            Else
                r.Add(New ReportParameter("in_Division", ""))
            End If
            If Me._ucSiteLocation.FacilityValue.Length > 0 Then
                r.Add(New ReportParameter("in_Siteid", Me._ucSiteLocation.FacilityValue))
            Else
                r.Add(New ReportParameter("in_Siteid", ""))
            End If
            If Me._ucSiteLocation.BusinessUnitValue.Length > 0 Then
                r.Add(New ReportParameter("in_BusinessUnit", Me._ucSiteLocation.BusinessUnitValue))
            Else
                r.Add(New ReportParameter("in_BusinessUnit", ""))
            End If
            If Me._ucSiteLocation.AreaValue.Length > 0 Then
                r.Add(New ReportParameter("in_Area", Me._ucSiteLocation.AreaValue))
            Else
                r.Add(New ReportParameter("in_Area", ""))
            End If
            If Me._ucSiteLocation.LineValue.Length > 0 Then
                r.Add(New ReportParameter("in_Line", Me._ucSiteLocation.LineValue))
            Else
                r.Add(New ReportParameter("in_Line", ""))
            End If

            If Me._ucSiteLocation.LineBreakValue.Length > 0 Then
                r.Add(New ReportParameter("in_LineBreak", Me._ucSiteLocation.LineBreakValue))
            Else
                r.Add(New ReportParameter("in_LineBreak", ""))
            End If

            r.Add(New ReportParameter("IN_STARTDATE", CStr(Me._DateRange.StartDate)))
            r.Add(New ReportParameter("IN_ENDDATE", CStr(Me._DateRange.EndDate)))


            Select Case ReportTitle
                Case ReportTitles.MOCListing
                    r.Add(New ReportParameter("IN_TYPE", ""))
                    r.Add(New ReportParameter("IN_PLANNER", ""))
                    r.Add(New ReportParameter("IN_CATEGORY", Me._ucMOCCategory.Category))
                    r.Add(New ReportParameter("IN_CLASSIFICATION", Me._ucMOCClassification.Classification))
                    r.Add(New ReportParameter("IN_ORDERBY", ""))
                Case ReportTitles.MOCParetoCharts
                    r.Add(New ReportParameter("IN_REPORT", Me._rblChartType.SelectedValue))
                Case ReportTitles.MOCPendingApproval
                    r.Add(New ReportParameter("IN_TYPE", Me._ucMOCType.Types))
                    r.Add(New ReportParameter("IN_PLANNER", Me._ddlInitiator.SelectedValue))
                    r.Add(New ReportParameter("IN_CATEGORY", Me._ucMOCCategory.Category))
                    r.Add(New ReportParameter("IN_CLASSIFICATION", Me._ucMOCClassification.Classification))
                    r.Add(New ReportParameter("IN_MOCNUMBER", Me._tbMOCNumber.Text))
                    r.Add(New ReportParameter("IN_APPROVER", Me._ddlApprover.SelectedValue))
                    r.Add(New ReportParameter("IN_ORDERBY", Replace(ReportSortText, " ", "")))
            End Select


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
    Protected Sub _ucCalendar_CalendarUpdated() Handles _ucCalendar.CalendarUpdated
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
End Class
