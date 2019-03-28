Imports System.Data
Imports Devart.Data.Oracle
Imports RI
Partial Class Outage_Reporting
    Inherits RIBasePage
    Friend Class SiteCriteria

        Public Sub New()
            Facility = String.Empty
            'FacilityName = String.Empty
            InActiveFlag = "N"
            Division = String.Empty
            'BusType = "All"
            Key = ""
        End Sub

        Private mFacility As String
        Private mInActiveFlag As String
        Private mDivision As String
        Private mBusType As String
        Private mKey As String
        'Private mFacilityName As String

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
    Private mCurrentReport As String = ""
    Private mCurrentSortOrder As String = ""

    Public Property ReportInactiveFlag() As String
        Get
            Return Session("ReportInactiveFlag")
        End Get
        Set(ByVal value As String)
            Session.Remove("ReportInactiveFlag")
            Session.Add("ReportInactiveFlag", value)
        End Set
    End Property
    Public Property CurrentReport() As String
        Get
            Return mCurrentReport
        End Get
        Set(ByVal value As String)
            If value <> "ReportSelect" Then
                mCurrentReport = value
            End If
        End Set
    End Property

    Public Property CurrentSortOrder() As String
        Get
            Return mCurrentSortOrder
        End Get
        Set(ByVal value As String)
            mCurrentSortOrder = value
        End Set
    End Property
    Public Property SDCategory() As String
        Get
            Return RI.SharedFunctions.GetCheckBoxValues(Me._cblSDCategory)
        End Get
        Set(ByVal value As String)
            RI.SharedFunctions.SetCheckBoxValues(_cblSDCategory, value)
        End Set
    End Property
    Private mReportTitle As String
    Private mReportSortValue As String
    Private mReportSortText As String
    Private mReportName As String
    Private mReportInactiveFlag As String

    Dim _ddlFacility As New DropDownList
    Dim _ddlDivision As New DropDownList
    Dim _ddlLineBreak As New DropDownList
    Dim _ddlLine As New DropDownList
    Dim _ddlBusinessUnit As New DropDownList
    Dim _ddlArea As New DropDownList
    Dim _ifrDownTime As New Web.UI.WebControls.Panel
    Structure ReportParameterType
        Const Calendar As String = "Calendar" 'dont think we need
        Const Contractor As String = "Contractor"
        Const SDCategory As String = "SD Category"
        Const DateRange As String = "Date Range"
        Const Division As String = "Division"
        Const Facility As String = "Facility" 'dont think we need
        Const Site As String = "Site"
        Const AnnualOutage As String = "Annual Outage"
        Const Coordinator As String = "OutageCoord"
        Const OutageNumber As String = "Outage Number"
        Const TaskActionItems As String = "Task Item Listing"
        Private Active As Boolean
    End Structure

    Structure RequiredReportParameters
        Dim Calendar As Boolean
        Dim DateRange As Boolean
        Dim SDCategory As Boolean
        Dim Coordinator As Boolean
        Dim Contractor As Boolean
        Dim AnnualOutage As Boolean
        Dim Division As Boolean
        Dim Facility As Boolean
        Dim Site As Boolean
        Dim OutageNumber As Boolean
        Dim TaskActionItems As Boolean
    End Structure

    Structure ReportTitles
        Const OutageNoContractor As String = "Outages With No Contractors Assigned"
        Const ContractorConflict As String = "Contractor Conflict"
        Const BusinessOutlook As String = "Business Outlook"
        Const OutageGantt As String = "Outage Gantt Chart"
        Const OutageAnnualPreparedness As String = "Annual Preparedness Meeting Schedule"
        Const AnnualScorecard As String = "Annual Completeness Scorecard"
        Const TurbineGeneratorScorecard As String = "Turbine Generator Scorecard"
        Const AnnualOutage3YrScorecard As String = "Annual Outage 3 Year Scorecard"
        'Const OutageDetailSummary As String = "Outage Detail Summary"
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
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            Master.ShowOutageMenu()
            Master.SetBanner(Master.RIRESOURCES.GetResourceValue("OutageReporting", True, "Outage"))
            If Not Page.IsPostBack Then
                PopulateReports(CurrentReport)
            End If
            Me.PopulateDropDowns()
        Catch ex As Exception
            Throw
            'Finally
            '    If ds IsNot Nothing Then ds = Nothing
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim userProfile As RI.CurrentUserProfile = RI.SharedFunctions.GetUserProfile
        Dim targetEvent As String()
        Dim selectedValue As String = String.Empty
        Dim sortValue As String = String.Empty

        _cblSDCategory.Attributes.Add("onClick", "checkAll(this,9);")

        If Page.IsPostBack And Request.Form("__EventTarget") IsNot Nothing Then
            If Request.Form("__EventTarget").Contains("ddlReportList") Then
                targetEvent = Request.Form("__EventTarget").Split("$")
                CurrentReport = Request.Form(Request.Form("__EventTarget"))
            Else
                CurrentReport = Request.Form(Me._ddlReportList.UniqueID)
            End If
            If Request.Form("__EventTarget").Contains("_ddlReportSortValue") Then
                targetEvent = Request.Form("__EventTarget").Split("$")
                sortValue = Request.Form(Request.Form("__EventTarget"))
            Else
                sortValue = Request.Form(Me._ddlReportSortValue.UniqueID)
            End If

            PopulateReports(CurrentReport)
            _ddlReportList_SelectedIndexChanged(CurrentReport, sortValue)
            If CurrentReport.Length > 0 Then
                Me.ReportSortValue = sortValue
                Me.ReportTitle = CurrentReport
                Me.ReportSortText = Me._ddlReportSortValue.SelectedItem.Text
                Me.ReportInactiveFlag = ReportInactiveFlag
            End If
        End If

        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "Outage") Then
            Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "Outage", Page.ResolveClientUrl("~/Outage/Outage.js"))
        End If


        '    '_DateRange.StartDate = DatePart(DateInterval.Month, Today()) & "/1/" & DatePart(DateInterval.Year, Today())
        '    '_DateRange.EndDate = "12/31/" & DatePart(DateInterval.Year, Today()) + 1
        '    If userProfile IsNot Nothing Then
        '        _ucSiteLocation.FacilityValue = userProfile.DefaultFacility
        '        _ucSiteLocation.DivisionValue = userProfile.DefaultDivision
        '    End If
        'End If


    End Sub
    Private Function GetReportTitlesDSFromPackage() As DataSet
        Dim conCust As Devart.Data.Oracle.OracleConnection = Nothing
        Dim cmdSQL As OracleCommand = Nothing
        Dim connection As String = String.Empty
        Dim provider As String = String.Empty
        Dim ds As DataSet = Nothing
        Dim daData As OracleDataAdapter = Nothing
        Dim cnConnection As OracleConnection = Nothing

        Try
            If Cache.Item("OUTAGE_REPORTTITLES") IsNot Nothing Then
                ds = CType(Cache.Item("OUTAGE_REPORTTITLES"), DataSet)
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
                .CommandText = "Outagereports.ReportTitles"
                .CommandType = CommandType.StoredProcedure
                Dim param As New OracleParameter

                param = New OracleParameter
                param.ParameterName = "rsReportTitles"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = ParameterDirection.Output
                .Parameters.Add(param)

                param = New OracleParameter
                param.ParameterName = "rsReportSortValues"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = ParameterDirection.Output
                .Parameters.Add(param)
            End With

            ds = New DataSet()
            ds.EnforceConstraints = False
            daData = New OracleDataAdapter(cmdSQL)
            daData.Fill(ds)
            ds.EnforceConstraints = True


            If ds IsNot Nothing Then Cache.Insert("Outage_REPORTTITLES", ds, Nothing, DateTime.Now.AddHours(6), TimeSpan.Zero)
        Catch ex As Exception
            Cache.Remove("Outage_REPORTTITLES")
            Throw New DataException("GetReportTitlesDSFromPackage", ex)
            If Not conCust Is Nothing Then conCust = Nothing
            Return Nothing
        Finally
            GetReportTitlesDSFromPackage = ds
            If Not daData Is Nothing Then daData = Nothing
            If Not ds Is Nothing Then ds = Nothing
            If Not cmdSQL Is Nothing Then cmdSQL = Nothing
        End Try
    End Function
    Private Sub PopulateReports(ByVal selectedValue As String)
        Dim ds As DataSet = Nothing
        Dim io As New System.IO.StringWriter

        Try
            ds = GetReportTitlesDSFromPackage()

            If ds IsNot Nothing Then
                Me._ddlReportList.DataTextField = "ReportTitle"
                Me._ddlReportList.DataValueField = "ReportTitle"
                Me._ddlReportList.DataSource = ds.Tables(0).CreateDataReader
                Me._ddlReportList.DataBind()
                Me._ddlReportList.Items.Insert(0, New ListItem("-- Please select a Report --", ""))

                If _ddlReportList.Items.FindByValue(selectedValue) IsNot Nothing Then
                    _ddlReportList.SelectedValue = selectedValue
                End If

            End If
        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then ds = Nothing
        End Try
    End Sub
    Private Sub PopulateSortOrder(Optional ByVal selectedValue As String = "")
        Dim ds As DataSet = Nothing
        Dim sql As String = String.Empty
        Dim rowFilter As String = "ReportTitle='{0}'"
        Dim io As New System.IO.StringWriter

        Try
            ds = GetReportTitlesDSFromPackage()

            If ds IsNot Nothing Then
                ds.Tables(1).DefaultView.RowFilter = String.Format(rowFilter, CurrentReport)
                Me._ddlReportSortValue.DataTextField = "REPORTSORTVALUE"
                Me._ddlReportSortValue.DataValueField = "REPORTNAME"
                Me._ddlReportSortValue.DataSource = ds.Tables(1).DefaultView
                Me._ddlReportSortValue.DataBind()
                ReportInactiveFlag = ds.Tables(1).DefaultView.Item(0).Item("INACTIVE_FLAG").ToString

                If _ddlReportSortValue.Items.FindByValue(selectedValue) IsNot Nothing Then
                    _ddlReportSortValue.SelectedValue = selectedValue
                Else
                    If _ddlReportSortValue.Items.Count > 0 Then
                        _ddlReportSortValue.SelectedIndex = 0
                    End If
                End If

                Me.ReportTitle = Me._ddlReportList.SelectedValue
                Me.ReportSortValue = Me._ddlReportSortValue.SelectedValue
                Me.ReportSortText = Replace(Me._ddlReportSortValue.SelectedItem.Text, " ", "")

            End If
        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then ds = Nothing
        End Try
    End Sub

    Protected Sub _ddlReportSortValue_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlReportSortValue.DataBound
        Dim ddl As New DropDownList
        For i As Integer = 0 To _ddlReportSortValue.Items.Count - 1
            If ddl.Items.FindByText(Me._ddlReportSortValue.Items(i).Text) Is Nothing Then
                If (Me._ddlReportSortValue.Items(i).Text.Length > 0) Then
                    ddl.Items.Add(New ListItem(Me._ddlReportSortValue.Items(i).Text, Me._ddlReportSortValue.Items(i).Value))
                End If
            End If
        Next

        If ddl.Items.Count > 0 Then
            _ddlReportSortValue.Items.Clear()
            For i As Integer = 0 To ddl.Items.Count - 1
                _ddlReportSortValue.Items.Add(New ListItem(ddl.Items(i).Text, ddl.Items(i).Value))
            Next
        Else
            'Let's hide the sort Order option
            Me._lblReportSortValue.Visible = False
            Me._ddlReportSortValue.Visible = False
        End If
    End Sub

    Public Sub PopulateDropDowns()
        Dim criteria As New SiteCriteria
        Dim ds As DataSet = Nothing
        'Dim savedXML As String = String.Empty
        'Dim io As New System.IO.StringWriter                

        Try
            ds = GetDropDownDSFromPackage(criteria)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    'Outage Coordinator
                    _ddlOutageCoord.DataSource = ds.Tables(0).CreateDataReader
                    _ddlOutageCoord.Items.Clear()
                    _ddlOutageCoord.DataTextField = "coordname"
                    _ddlOutageCoord.DataValueField = "username"
                    _ddlOutageCoord.DataBind()
                    _ddlOutageCoord.Items.Insert(0, "")
                    _ddlOutageCoord.SelectedIndex = 0

                    'Contractor
                    _ddlContractor.DataSource = ds.Tables(1).CreateDataReader
                    _ddlContractor.Items.Clear()
                    _ddlContractor.DataTextField = "companyname"
                    _ddlContractor.DataValueField = "contractorseqid"
                    _ddlContractor.DataBind()
                    _ddlContractor.Items.Insert(0, "")
                    _ddlContractor.SelectedIndex = 0

                End If
            End If
        Catch ex As Exception
            Throw
        Finally
            'If Not ds Is Nothing Then ds = Nothing
        End Try
    End Sub
    Private Function GetDropDownDSFromPackage(ByVal criteria As SiteCriteria) As DataSet
        Dim conCust As Devart.Data.Oracle.OracleConnection = Nothing
        Dim cmdSQL As OracleCommand = Nothing
        Dim connection As String = String.Empty
        Dim provider As String = String.Empty
        Dim ds As DataSet = Nothing
        Dim daData As OracleDataAdapter = Nothing
        Dim cnConnection As OracleConnection = Nothing
        Dim key As String = String.Empty
        Dim Rowid As String = ""
        Try
            'key = SharedFunctions.CreateKey(criteria)
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
                .CommandText = "OutageReports.ReportDDL"
                .CommandType = CommandType.StoredProcedure
                Dim param As New OracleParameter

                param.ParameterName = "in_siteid"
                param.OracleDbType = OracleDbType.VarChar
                param.Value = criteria.Facility
                param.Direction = ParameterDirection.Input
                .Parameters.Add(param)

                param = New OracleParameter
                param.ParameterName = "rsCoord"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = ParameterDirection.Output
                .Parameters.Add(param)

                param = New OracleParameter
                param.ParameterName = "rsContractor"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = ParameterDirection.Output
                .Parameters.Add(param)
            End With

            ds = New DataSet()
            ds.EnforceConstraints = False
            daData = New OracleDataAdapter(cmdSQL)
            daData.Fill(ds)
            ds.EnforceConstraints = True
            If ds IsNot Nothing Then Cache.Insert(key, ds, Nothing, DateTime.Now.AddHours(0), TimeSpan.Zero)
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
    Protected Sub _ddlReportList_SelectedIndexChanged(ByVal SelectedValue As String, ByVal SortValue As String) '(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlReportList.SelectedIndexChanged
        Dim criteria As New SiteCriteria
        If SelectedValue.Length > 0 Then
            Me._ddlReportSortValue.Visible = True
            Me._lblReportSortValue.Visible = True
            Me.ReportSortValue = SortValue
            Me.ReportTitle = CurrentReport
            'Me.ReportSortText = Me._ddlReportSortValue.SelectedItem.Text
            Me.ReportInactiveFlag = ReportInactiveFlag

            PopulateSortOrder(SortValue)
        Else
            Me._ddlReportSortValue.Visible = False
            Me._lblReportSortValue.Visible = False
        End If

        PopulateReportParameters(criteria)

        If RI.SharedFunctions.CausedPostBack(Me._ddlReportList.UniqueID) Then
            Me._upSite.Update()
        End If
    End Sub
    'Private Sub PopulateReportListingControls(ByVal application As String)
    '    'Dim reportTitleList As System.Collections.Generic.List(Of ReportTitles) = Nothing
    '    Dim reportSortValueList As System.Collections.Generic.List(Of ReportSortValue) = Nothing
    '    Dim reportParameterList As System.Collections.Generic.List(Of ReportParameters) = Nothing
    '    Dim reportSelection As New ReportSelectionBll(application)
    '    Dim availableReportParms As ReportSelectionBll.RequiredReportParameters

    '    If reportSelection IsNot Nothing Then
    '        If Me._ddlReportList.SelectedIndex > 0 Then
    '            reportSortValueList = reportSelection.GetReportSortValues(Me._ddlReportList.SelectedValue)
    '            If reportSortValueList IsNot Nothing AndAlso reportSortValueList.Count > 0 Then
    '                Dim currentSortValue As String = _ddlReportSortValue.SelectedValue
    '                _ddlReportSortValue.Items.Clear()
    '                For Each item As ReportSortValues In reportSortValueList
    '                    Me._ddlReportSortValue.Items.Add(New ListItem(item.ReportSortValue, item.ReportName))
    '                Next
    '                If Me._ddlReportSortValue.Items.Count > 0 Then
    '                    If Me._ddlReportSortValue.Items.Count = 1 Then
    '                        Me._lblReportSortValue.Visible = False
    '                        Me._ddlReportSortValue.Visible = False
    '                    Else
    '                        Me._lblReportSortValue.Visible = True
    '                        Me._ddlReportSortValue.Visible = True
    '                        _lblReportSortValue.Text = "Sort Order"
    '                    End If
    '                    If _ddlReportSortValue.Items.FindByValue(currentSortValue) IsNot Nothing Then
    '                        _ddlReportSortValue.Items.FindByValue(currentSortValue).Selected = True
    '                    Else
    '                        Me._ddlReportSortValue.SelectedIndex = 0
    '                    End If
    '                    'Me._ddlReportSortValue.SelectedIndex = 0
    '                Else
    '                    Me._lblReportSortValue.Visible = False
    '                    Me._ddlReportSortValue.Visible = False
    '                End If
    '            Else
    '                Me._lblReportSortValue.Visible = False
    '                Me._ddlReportSortValue.Visible = False
    '            End If

    '            reportParameterList = reportSelection.GetReportParameters(Me._ddlReportList.SelectedValue)
    '            If reportParameterList IsNot Nothing AndAlso reportParameterList.Count > 0 Then
    '                'Hide report options until requested
    '                ' _taskSearch.Visible = False

    '                For Each item As ReportParameters In reportParameterList
    '                    'Build Report Selection Screen

    '                    Select Case item.ReportParameterType.ToLower
    '                        Case "site"
    '                            availableReportParms.Site = True
    '                        Case "report type"
    '                            _lblReportSortValue.Text = "Report Type"
    '                            availableReportParms.ReportType = True
    '                        Case "task type"
    '                            availableReportParms.Types = True
    '                        Case "created by"
    '                            availableReportParms.CreatedBy = True
    '                        Case "task activity"
    '                            availableReportParms.Activity = True
    '                        Case "due date"
    '                            availableReportParms.EstimatedDueDate = True
    '                        Case "calendar"
    '                            availableReportParms.DueDate = True
    '                        Case "task status"
    '                            availableReportParms.TaskStatus = True
    '                        Case "responsible"
    '                            availableReportParms.Responsible = True
    '                        Case "header date"
    '                            availableReportParms.HeaderDate = True
    '                        Case "task listing"
    '                            availableReportParms.TaskListing = True
    '                        Case "source system"
    '                            availableReportParms.SourceSystem = True
    '                        Case "title"
    '                            availableReportParms.Title = True
    '                    End Select
    '                Next
    '                Me._trReportSelectionHeader.Visible = True
    '                'Me._trReportSelection.Visible = True
    '                '_taskSearch.Visible = True
    '                Me._pnlCalendarSearch.Visible = True
    '                AvailableReportParameters = availableReportParms
    '                DisplaySearchSelections()
    '            End If
    '        Else
    '            Me._trReportSelectionHeader.Visible = False
    '            Me._pnlCalendarSearch.Visible = False
    '            Me._divReportSearchButtons.Visible = False
    '            Me._divViewSearchButtons.Visible = False
    '        End If

    '    End If


    '    'End If
    '    'Me.ReselectControls()
    'End Sub
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
                    '_rblDateRange.ID = "_rblDateRange"
                    '_rblDateRange.AutoPostBack = True

                    '_rblDateRange.RepeatColumns = 3
                    '_rblDateRange.RepeatDirection = RepeatDirection.Vertical
                    '_rblDateRange.RepeatLayout = RepeatLayout.Table
                    '_rblDateRange.DataSource = ds.Tables(0).DefaultView
                    '_rblDateRange.Items.Clear()
                    '_rblDateRange.DataTextField = "REPORTPARAMETERS"
                    '_rblDateRange.DataValueField = "REPORTPARAMETERS"
                    '_rblDateRange.DataBind()
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
                        ' url = ds.Tables(0).DefaultView.Item(0).Item("ReportParameters").ToString & "&sv=" & ReportSortValue
                        url = "http://gpitasktracker.graphicpkg.com/ReportSelection.aspx?rn=Task Item Listing&sv=" & ReportSortValue
                    Else
                        url = "http://gpitasktracker.graphicpkg.com/ReportSelection.aspx?rn=Task Item Listing&sv=" & ReportSortValue
                    End If
                    iframe.InnerHtml = "<iframe src='" & url & "' border='0' frameborder='0' width='100%' height='600px'/>"
                    _ifrDownTime.Controls.Add(iframe)
                    _ifrDownTime.CssClass = "iframe"
                End If

                'Facility
                ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.Facility, Me.ReportTitle)
                If ds.Tables(0).DefaultView.Count > 0 Then
                    requiredParms.Facility = True
                End If

                'Division CAC 10/12/16
                ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.Division, Me.ReportTitle)
                If ds.Tables(0).DefaultView.Count > 0 Then
                    'Me._ddlDivision.Enabled = True
                    'Me._ddlDivision.Visible = True
                    'Me._lblDivision.Visible = True
                    requiredParms.Division = True
                End If

                ''Calendar
                'ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.Calendar, Me.ReportTitle)
                'If ds.Tables(0).DefaultView.Count > 0 Then
                '    requiredParms.Calendar = True
                'End If

                'Shutdown Category                               
                ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.SDCategory, Me.ReportTitle)
                ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
                If ds.Tables(0).DefaultView.Count > 0 Then
                    requiredParms.SDCategory = True
                End If

                'Contractor                               
                ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.Contractor, Me.ReportTitle)
                ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
                If ds.Tables(0).DefaultView.Count > 0 Then
                    requiredParms.Contractor = True
                End If

                'Annual Outage                               
                ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.AnnualOutage, Me.ReportTitle)
                ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
                If ds.Tables(0).DefaultView.Count > 0 Then
                    requiredParms.AnnualOutage = True
                End If

                'Coordinator                               
                ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.Coordinator, Me.ReportTitle)
                ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
                If ds.Tables(0).DefaultView.Count > 0 Then
                    requiredParms.Coordinator = True
                End If


                'OutageNumber                              
                ds.Tables(0).DefaultView.RowFilter = String.Format(rowFilter, ReportParameterType.OutageNumber, Me.ReportTitle)
                ds.Tables(0).DefaultView.Sort = "PARAMETERSORT,REPORTPARAMETERTYPE"
                If ds.Tables(0).DefaultView.Count > 0 Then
                    requiredParms.OutageNumber = True
                End If

            End If
            Session.Add("requiredParms", requiredParms)
            DisplayReportParameters()
        Catch ex As Exception

        Finally
            If ds IsNot Nothing Then ds = Nothing
        End Try
    End Sub
    'Protected Sub _rblDateRange_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles _rblDateRange.SelectedIndexChanged
    '    Dim todaysDate As Date = Now
    '    Dim targetEvent As String()
    '    Dim eventValue As String
    '    Dim selectedValue As String = String.Empty
    '    If Page.IsPostBack And Request.Form("__EventTarget") IsNot Nothing Then
    '        If Request.Form("__EventTarget").Contains("_rblDateRange") Then
    '            targetEvent = Request.Form("__EventTarget").Split("$")
    '            eventValue = targetEvent(targetEvent.Length - 1).ToString
    '            selectedValue = _rblDateRange.Items(eventValue).Value.ToString
    '        Else
    '            Exit Sub
    '        End If
    '    End If

    '    Select Case LCase(selectedValue)
    '        Case "last month"
    '            SetDateRange(range.LastMonth)
    '        Case "last 3 months"
    '            SetDateRange(range.Last3Months)
    '        Case "last year to date"
    '            SetDateRange(range.LastYearToDate)
    '        Case "year to date"
    '            SetDateRange(range.YearToDate)
    '        Case "1st quarter"
    '            SetDateRange(range.FirstQuarter)
    '        Case "2nd quarter"
    '            SetDateRange(range.SecondQuarter)
    '        Case "3rd quarter"
    '            SetDateRange(range.ThirdQuarter)
    '        Case "4th quarter"
    '            SetDateRange(range.FourthQuarter)
    '        Case "entered last 7 days"
    '            SetDateRange(range.EnteredLast7Days)
    '        Case "last year"
    '            SetDateRange(range.LastYear)
    '            'Remember to set the Last 7 Days Flag
    '        Case Else
    '            'SetDateRange(range.LastMonth)
    '    End Select
    'End Sub
    'Private Enum range
    '    LastMonth = 1
    '    Last3Months = 2
    '    LastYearToDate = 3
    '    YearToDate = 4
    '    FirstQuarter = 5
    '    SecondQuarter = 6
    '    ThirdQuarter = 7
    '    FourthQuarter = 8
    '    EnteredLast7Days = 9
    '    LastYear = 10
    '    EndOfYear = 11
    'End Enum
    'Private Sub SetDateRange(ByVal dtRange As range)
    '    Dim todaysDate As Date = Now

    '    Select Case dtRange
    '        Case range.LastMonth
    '            _DateRange.StartDate = DateSerial(Year(todaysDate), Month(todaysDate) - 1, 1)
    '            _DateRange.EndDate = DateSerial(Year(todaysDate), Month(todaysDate), 0)
    '        Case range.Last3Months
    '            _DateRange.StartDate = DateSerial(Year(todaysDate), Month(todaysDate) - 3, 1)
    '            _DateRange.EndDate = DateSerial(Year(todaysDate), Month(todaysDate), 0)
    '        Case range.LastYearToDate '"last year to date"
    '            _DateRange.StartDate = DateSerial(Year(todaysDate) - 1, 1, 1)
    '            _DateRange.EndDate = todaysDate.ToShortDateString
    '        Case range.YearToDate '"year to date"
    '            _DateRange.StartDate = DateSerial(Year(todaysDate), 1, 1)
    '            _DateRange.EndDate = todaysDate.ToShortDateString
    '        Case range.FirstQuarter '"1st quarter"
    '            _DateRange.StartDate = DateSerial(Year(todaysDate), 1, 1)
    '            _DateRange.EndDate = DateSerial(Year(todaysDate), 4, 0)
    '        Case range.SecondQuarter '"2nd quarter"
    '            _DateRange.StartDate = DateSerial(Year(todaysDate), 4, 1)
    '            _DateRange.EndDate = DateSerial(Year(todaysDate), 7, 0)
    '        Case range.ThirdQuarter '"3rd quarter"
    '            _DateRange.StartDate = DateSerial(Year(todaysDate), 7, 1)
    '            _DateRange.EndDate = DateSerial(Year(todaysDate), 10, 0)
    '        Case range.FourthQuarter '"4th quarter"
    '            _DateRange.StartDate = DateSerial(Year(todaysDate), 10, 1)
    '            _DateRange.EndDate = DateSerial(Year(todaysDate), 13, 0)
    '        Case range.EnteredLast7Days '"entered last 7 days"
    '            _DateRange.StartDate = todaysDate.AddDays(-7).ToShortDateString  'DateSerial(Year(todaysDate), Month(todaysDate), -7)
    '            _DateRange.EndDate = todaysDate.ToShortDateString
    '            'Remember to set the Last 7 Days Flag
    '        Case range.LastYear
    '            _DateRange.StartDate = DateSerial(Year(todaysDate) - 1, 1, 1)
    '            _DateRange.EndDate = DateSerial(Year(todaysDate) - 1, 12, 31)
    '        Case range.EndOfYear
    '            _DateRange.StartDate = DateSerial(Year(todaysDate), 1, 1)
    '            _DateRange.EndDate = DateSerial(Year(todaysDate), 12, 31)
    '        Case Else
    '    End Select
    'End Sub
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
            key = "OUTAGE_REPORTPARAMETERS"
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
                .CommandText = "OutageReports.ReportParameters"
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
    Private Sub DisplayReportParameters()
        Dim tblRow As Integer = 1
        Dim tblHeaderrow As Integer = 0
        Dim tblCell As Integer = 0
        Dim totalTblRows As Integer = _tblMain.Rows.Count - 1
        Dim requiredParms As RequiredReportParameters = Session.Item("requiredParms")

        Try

            If requiredParms.SDCategory = True Then
                _tblOutageStatus.Visible = True
            End If
            If requiredParms.Site Then
                'Me.AddControlToTable(_ucSiteLocation, 2, 1, 0, Resources.Shared.lblDivision, Me._tblSite, True, False, "","None",True)
                Me._ucSiteLocation.Visible = True
                Me._tblSite.Visible = "true"
                Me._tblSite.Rows(1).Visible = False
                Me._tblSite.Rows(2).Visible = False
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

            Else
                Me._tblSite.Visible = False
            End If

            If requiredParms.DateRange = True Then
                Me._tblMain.Visible = "true"
                Me.AddControlToTable(_DateRange, tblRow, tblHeaderrow, tblCell, ReportParameterType.DateRange, _tblMain, True, False)
            End If

            If requiredParms.TaskActionItems = True Then
                AddControlToTable(_ifrDownTime, tblRow, tblHeaderrow, tblCell, ReportParameterType.TaskActionItems, _tblMain, True)
                Me._tblMain.Visible = "True"
            End If

            If requiredParms.Contractor = True Then
                _pnlContractor.Visible = "true"
            End If
            If requiredParms.Coordinator = True Then
                _pnlCoord.Visible = "true"
            End If
            If requiredParms.AnnualOutage = True Then
                _pnlAnnual.Visible = "true"
            End If

            If requiredParms.OutageNumber = True Then
                _pnlOutageNumber.Visible = "true"
            End If

            Me._pnlButtons.Visible = "True"

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
    Public Sub SetReportDefaults()
        Try

            Me._DateRange.SelectedDateRange = RI_User_Controls_Common_ucDateRange.range.EndOfYear
            Select Case ReportTitle
                Case ReportTitles.AnnualScorecard
                    'Me._DateRange.SelectedDateRange = RI_User_Controls_Common_ucDateRange.range.LastYear
                    Me._DateRange.SelectedDateRange = RI_User_Controls_Common_ucDateRange.range.EndOfYear
                    'Me._DateRange. = RI_User_Controls_Common_ucDateRange.range.EndOfYear
                    'Me._DateRange.EndDate = RI_User_Controls_Common_ucDateRange.range.EndOfYear
            End Select


            ''If Me._cblDateRange.SelectedIndex = -1 Then
            'If Me._cblDateRange.Items.FindByValue("All") IsNot Nothing Then
            '    Me._cblDateRange.SelectedIndex = -1
            '    Me._cblDateRange.Items.FindByValue("All").Selected = True
            'End If
            ''End If
            '
            '               Case ReportTitles.OutageNoContractor
            '          Me._DateRange.SelectedDateRange = RI_User_Controls_Common_ucDateRange.range.Last12Months
            'Me._rblChartType.Items.FindByValue("Category").Selected = True

            '         End Select
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

            If ReportTitles.OutageNoContractor = ReportTitle Then
                ReportSortValue = "OutageNoContractor"
                ReportTitle = "OutageNoContractor"
            End If

            If ReportTitles.ContractorConflict = ReportTitle Then
                ReportSortValue = "ContractorConflict"
                ReportTitle = "ContractorConflict"
            End If

            If ReportTitles.BusinessOutlook = ReportTitle Then
                ReportSortValue = "BusinessOutlook"
                ReportTitle = "BusinessOutlook"
            End If

            If ReportTitles.OutageGantt = ReportTitle Then
                ReportSortValue = "OutageGantt"
                ReportTitle = "OutageGantt"
            End If

            If ReportTitles.OutageAnnualPreparedness = ReportTitle Then
                ReportSortValue = "OutageAnnualPreparedness"
                ReportTitle = "OutageAnnualPreparedness"
            End If

            If ReportTitles.AnnualScorecard = ReportTitle Then
                ReportSortValue = "AnnualScorecard"
                ReportTitle = "AnnualScorecard"
            End If

            If ReportTitles.TurbineGeneratorScorecard = ReportTitle Then
                ReportSortValue = "TurbineGeneratorScorecard"
                ReportTitle = "TurbineGeneratorScorecard"
            End If

            If ReportTitles.AnnualOutage3YrScorecard = ReportTitle Then
                ReportSortValue = "AnnualOutage3YrScorecard"
                ReportTitle = "AnnualOutage3YrScorecard"
            End If

            'CAC 09/06/13 Removed Outage Detail report from program - Obsolete Report"
            'If ReportTitles.OutageDetailSummary = ReportTitle Then
            'ReportSortValue = "OutageDetailSummary"
            'ReportTitle = "OutageDetailSummary"
            'End If

            r.Add(New ReportParameter("Report", ReportSortValue))
            r.Add(New ReportParameter("ReportTitle", ReportTitle))

            'Common Params
            'If Me._ddlDivision.SelectedValue.Length > 0 Then
            If Me._ucSiteLocation.DivisionValue.Length > 0 Then
                'Cathy Cox 09/06/13 removed changing & to and for BI4 upgrade
                'r.Add(New ReportParameter("Division", Replace(Me._ucSiteLocation.DivisionValue, "&", "and")))
                'r.Add(New ReportParameter("InDivision", Replace(Me._ucSiteLocation.DivisionValue, "&", "and")))
                r.Add(New ReportParameter("Division", Me._ucSiteLocation.DivisionValue))
                r.Add(New ReportParameter("InDivision", Me._ucSiteLocation.DivisionValue))
            Else
                r.Add(New ReportParameter("Division", "All"))
                r.Add(New ReportParameter("InDivision", "All"))
            End If
            If Me._ucSiteLocation.FacilityValue.Length > 0 And Me._ucSiteLocation.FacilityName <> "All" Then
                r.Add(New ReportParameter("PlantName", Me._ucSiteLocation.FacilityName))
                r.Add(New ReportParameter("INSiteID", Me._ucSiteLocation.FacilityValue))
            Else
                r.Add(New ReportParameter("PlantName", "All"))
                r.Add(New ReportParameter("INSiteID", "All"))
            End If
            If Me._ucSiteLocation.BusinessUnitValue.Length > 0 Then
                r.Add(New ReportParameter("BusUnit", Me._ucSiteLocation.BusinessUnitValue))
            Else
                r.Add(New ReportParameter("BusUnit", "All"))
            End If
            If Me._ucSiteLocation.AreaValue.Length > 0 Then
                r.Add(New ReportParameter("Area", Me._ucSiteLocation.AreaValue))
                r.Add(New ReportParameter("InAreca", Me._ucSiteLocation.AreaValue))
            Else
                r.Add(New ReportParameter("Area", "All"))
                r.Add(New ReportParameter("InArea", "All"))
            End If
            If Me._ucSiteLocation.LineValue.Length > 0 Then
                r.Add(New ReportParameter("Line", Me._ucSiteLocation.LineValue))
                r.Add(New ReportParameter("InLine", Me._ucSiteLocation.LineValue))
            Else
                r.Add(New ReportParameter("Line", "All"))
                r.Add(New ReportParameter("InLine", "All"))
            End If


            'If Me._cblSDCategory.SelectedValue = "All" Then
            r.Add(New ReportParameter("SDCategory", Me.SDCategory))
            'End If
            'Me._cblActivity.Items.FindByValue("Inspection")
            If Me._cblSDCategory.Items.FindByValue("Black Mill (No Power/Steam)") IsNot Nothing AndAlso Me._cblSDCategory.Items.FindByValue("Black Mill (No Power/Steam)").Selected Then
                r.Add(New ReportParameter("BlackMill", "Black"))
            Else
                r.Add(New ReportParameter("BlackMill", "N/A"))
            End If

            If Me._cblSDCategory.Items.FindByValue("Cold Mill (No Steam)") IsNot Nothing AndAlso Me._cblSDCategory.Items.FindByValue("Cold Mill (No Steam)").Selected Then
                r.Add(New ReportParameter("ColdMill", "Cold"))
            Else
                r.Add(New ReportParameter("ColdMill", "N/A"))
            End If

            If Me._cblSDCategory.Items.FindByValue("Total Mill (Utilities Available)") IsNot Nothing AndAlso Me._cblSDCategory.Items.FindByValue("Total Mill (Utilities Available)").Selected Then
                r.Add(New ReportParameter("TotalMill", "Total"))
            Else
                r.Add(New ReportParameter("TotalMill", "N/A"))
            End If

            If Me._cblSDCategory.Items.FindByValue("Partial Mill") IsNot Nothing AndAlso Me._cblSDCategory.Items.FindByValue("Partial Mill").Selected Then
                r.Add(New ReportParameter("PartialMill", "Partial"))
            Else
                r.Add(New ReportParameter("PartialMill", "N/A"))
            End If

            If Me._cblSDCategory.Items.FindByValue("Field Day") IsNot Nothing AndAlso Me._cblSDCategory.Items.FindByValue("Field Day").Selected Then
                r.Add(New ReportParameter("FieldDay", "Field"))
            Else
                r.Add(New ReportParameter("FieldDay", "N/A"))
            End If

            If Me._cblSDCategory.Items.FindByValue("Major Project") IsNot Nothing AndAlso Me._cblSDCategory.Items.FindByValue("Major Project").Selected Then
                r.Add(New ReportParameter("MajorProject", "Major"))
            Else
                r.Add(New ReportParameter("MajorProject", "N/A"))
            End If

            If Me._cblSDCategory.Items.FindByValue("LOO") IsNot Nothing AndAlso Me._cblSDCategory.Items.FindByValue("LOO").Selected Then
                r.Add(New ReportParameter("LOO", "LOO"))
            Else
                r.Add(New ReportParameter("LOO", "N/A"))
            End If


            If Me._cblSDCategory.Items.FindByValue("TG") IsNot Nothing AndAlso Me._cblSDCategory.Items.FindByValue("TG").Selected Then
                r.Add(New ReportParameter("TG", "TG"))
            Else
                r.Add(New ReportParameter("TG", "N/A"))
            End If


            'r.Add(New ReportParameter("StartDate", CStr((Me._DateRange.StartDate))))
            'r.Add(New ReportParameter("EndDate", CStr((Me._DateRange.EndDate))))
            r.Add(New ReportParameter("StartDate", Me._DateRange.StartDate))
            r.Add(New ReportParameter("EndDate", Me._DateRange.EndDate))
            r.Add(New ReportParameter("StartDay", (Day(Me._DateRange.StartDateText))))
            r.Add(New ReportParameter("StartMonth", (Month(Me._DateRange.StartDateText))))
            r.Add(New ReportParameter("StartYear", (Year(Me._DateRange.StartDateText))))
            r.Add(New ReportParameter("EndDay", (Day(Me._DateRange.EndDateText))))
            r.Add(New ReportParameter("EndMonth", (Month(Me._DateRange.EndDateText))))
            r.Add(New ReportParameter("EndYear", (Year(Me._DateRange.EndDateText))))


            If Me._tbOutageNumber.Text.Length > 0 Then
                r.Add(New ReportParameter("OutageNumber", Me._tbOutageNumber.Text))
            Else
                r.Add(New ReportParameter("OutageNumber", "All"))
            End If

            If Me._ddlContractor.SelectedValue.Length > 0 Then
                r.Add(New ReportParameter("Contractor", Me._ddlContractor.SelectedValue))
            Else
                r.Add(New ReportParameter("Contractor", "All"))
            End If

            If Me._ddlOutageCoord.SelectedValue.Length > 0 Then
                r.Add(New ReportParameter("OutageCoord", Me._ddlOutageCoord.SelectedValue))
            Else
                r.Add(New ReportParameter("OutageCoord", "All"))
            End If

            If Me._cbAnnualOutage.Checked = True Then
                r.Add(New ReportParameter("AnnualOutage", "Y"))
            Else
                r.Add(New ReportParameter("AnnualOutage", ""))
            End If


            'Select Case ReportTitle
            '    Case ReportTitles.ContractorConflict
            '        r.Add(New ReportParameter("Division", ""))
            '        r.Add(New ReportParameter("PlantName", ""))
            '        r.Add(New ReportParameter("Contractor", ""))
            '        r.Add(New ReportParameter("OutageCoord", ""))
            '        r.Add(New ReportParameter("StartDay", ""))
            '        r.Add(New ReportParameter("StartMonth", ""))
            '        r.Add(New ReportParameter("StartYear", ""))
            '        r.Add(New ReportParameter("EndDay", ""))
            '        r.Add(New ReportParameter("EndMonth", ""))
            '        r.Add(New ReportParameter("EndYear", ""))
            '        'Case ReportTitles.MOCParetoCharts
            '        'r.Add(New ReportParameter("IN_REPORT", Me._rblChartType.SelectedValue))
            'End Select

            Dim sb As New StringBuilder
            Dim ReportPage As String = String.Empty
            Dim url As String = ""
            If r IsNot Nothing Then
                For i As Integer = 0 To r.Count - 1
                    sb.Append("&" & r.Item(i).name & "=" & Server.UrlEncode(r.Item(i).value))
                Next

                If Request.UserHostAddress = "127.0.0.1" Or Request.UserHostAddress = "http://s29edev13/riajax" Then
                    'url = "http://ridev/CEReporting/frmCrystalReportOutage.aspx?Parm=" & Replace(sb.ToString, " ", "%20")
                    url = "http://ridev/CEReporting/CrystalReportDisplay.aspx?Parm=" & Replace(sb.ToString, " ", "%20")

                ElseIf Request.ServerVariables("SERVER_NAME") = "ridev" Then
                    'url = "http://ridev/CEReporting/frmCrystalReportOutage.aspx?Parm=" & Replace(sb.ToString, " ", "%20")
                    url = "http://ridev/CEReporting/CrystalReportDisplay.aspx?Parm=" & Replace(sb.ToString, " ", "%20")
                ElseIf Request.ServerVariables("SERVER_NAME") = "ritest" Then
                    'url = "http://ritest/CEReporting/frmCrystalReportOutage.aspx?Parm=" & Replace(sb.ToString, " ", "%20")
                    url = "http://ritest/CEReporting/CrystalReportDisplay.aspx?Parm=" & Replace(sb.ToString, " ", "%20")
                Else
                    'url = "http://ri/CEReporting/frmCrystalReportOutage.aspx?Parm=" & Replace(sb.ToString, " ", "%20")
                    url = "http://gpimv.graphicpkg.com/CEReporting/CrystalReportDisplay.aspx?Parm=" & Replace(sb.ToString, " ", "%20")
                End If

                ''If r.Contains("ReportPage") = True Then
                ''ReportPage = r.Item(r.IndexOf("ReportPage")).value
                ''End If

                ''If ReportPage.Length = 0 Then ReportPage = "frmCrystalReportOutage.aspx"
                'If Request.UserHostAddress = "127.0.0.1" Or Request.UserHostAddress = "http://s29edev13/riajax" Then
                '    Response.Redirect("http://ridev/CEReporting/" & ReportPage & "?Param=" & Replace(sb.ToString, " ", "%20"))
                'Else
                '    'Response.Write("Request.UserHostAddress=" & Request.UserHostAddress)
                '    'Server.Transfer("../CEReporting/frmCrystalReport.aspx?Param=" & Replace(sb.ToString, " ", "%20"))
                '    Response.Redirect("../../CEReporting/" & ReportPage & "?Param=" & Replace(sb.ToString, " ", "%20"), True)
                '    url = "http://ri/CEReporting/frmCrystalReportOutage.aspx" & Replace(sb.ToString, " ", "%20")
                'End If
            End If
            Web.UI.ScriptManager.RegisterStartupScript(Me._upSite, _upSite.GetType, "pop", "PopupWindow('" & url & "','CrystalReport',800,600,'yes','no','yes');", True)
            RI.SharedFunctions.InsertAuditRecord("RIReport", url)
            'Session.Add("CrystalReport", r)
            'Web.UI.ScriptManager.RegisterStartupScript(_upSite, _upSite.GetType, "pop", "PopupWindow('Report.aspx','CrystalReport',800,600,'yes','no','yes');", True)

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Protected Sub _btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnReset.Click
        Response.Redirect("Outage/Reporting.aspx", False)
        'SetSiteDefaults()
        'SetReportDefaults()
        'Me._ucCalendar.resetcalendar()
    End Sub

End Class
