Imports RI
Imports RI.SharedFunctions
Imports Devart.Data.Oracle
Partial Class RI_ViewUpdateSearch
    Inherits RIBasePage

    Dim ViewUpdate As clsViewUpdate
    Dim ExcelSelect As clsExcelSelect
    Dim HoldSortExpression As String
    Dim userProfile As RI.CurrentUserProfile = Nothing

    Public Property SearchSortDirection() As String
        Get
            Return Session("SearchSortDirection")
        End Get
        Set(ByVal value As String)
            Session("SearchSortDirection") = value
        End Set
    End Property
    Public Property RCFAStatus() As String
        Get
            Return RI.SharedFunctions.GetCheckBoxValues(_cblRCFAStatus)
        End Get
        Set(ByVal value As String)
            RI.SharedFunctions.SetCheckBoxValues(_cblRCFAStatus, value)
        End Set
    End Property
    'Public Property ActionDue() As String
    '    Get
    '        Return RI.SharedFunctions.GetCheckBoxValues(_cblActionDue)
    '    End Get
    '    Set(ByVal value As String)
    '        RI.SharedFunctions.SetCheckBoxValues(_cblActionDue, value)
    '    End Set
    'End Property

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Response.Redirect("../../riview.asp", True)
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("ViewIncidents"))
        'Master.SetBanner("ViewIncidents")

    End Sub
    Function GetUserid() As String
        'Dim userProfile As RI.CurrentUserProfile = Nothing
        userProfile = RI.SharedFunctions.GetUserProfile
        Return userProfile.Username
    End Function


    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            ' Dim ipLoc As New IP.Bids.Localization.WebLocalization()

            userProfile = RI.SharedFunctions.GetUserProfile
            Dim clsSearch As clsViewSearch = Session("clsSearch")

            If Page.IsPostBack Then
                ViewUpdate = New clsViewUpdate(_siteLocation.FacilityValue, userProfile.InActiveFlag, _siteLocation.DivisionValue)
                'ViewUpdate = New clsViewUpdate(_SiteLocation.FacilityValue, userProfile.InActiveFlag, _SiteLocation.DivisionValue)
                Me._lblRecCount.Text = Master.RIRESOURCES.GetResourceValue("NoRecordsFound")
                'Me._SiteLocation.DivisionValue = 
            Else
                Me._lblRecCount.Visible = False ' 
                Me._lblRecordCount.Visible = False '
                'ViewUpdate = New clsViewUpdate("TI", "N", "P&C Papers")
                If clsSearch IsNot Nothing Then
                    ViewUpdate = New clsViewUpdate(clsSearch.Facility, userProfile.InActiveFlag, clsSearch.Division)
                Else
                    ViewUpdate = New clsViewUpdate(userProfile.DefaultFacility, userProfile.InActiveFlag, userProfile.DefaultDivision)
                End If

            End If
            ScriptManager.RegisterClientScriptInclude(Me._upViewScreen, _upViewScreen.GetType, "ViewUpdate", Page.ResolveClientUrl("~/ri/ViewUpdate.js"))
            ScriptManager.RegisterStartupScript(Me._upViewScreen, _upViewScreen.GetType, "GetGlobalJSVar", GetGlobalJSVar, True)
            'Me._cblRCFAStatus.Attributes.Add(")


            'Me._DateRange.EndDate = Now
            If ViewUpdate IsNot Nothing Then
                'Me._SiteLocation.Facility = ViewUpdate.Facility
                'Me._SiteLocation.BusinessUnit = ViewUpdate.BusinessUnit
                'Me._SiteLocation.Area = ViewUpdate.Area
                'Me._SiteLocation.Line = ViewUpdate.Line
                'Me._SiteLocation.LineBreak = ViewUpdate.LineBreak
                'Me._SiteLocation.Division = ViewUpdate.Division
                'Me._SiteLocation.DataBind()
                Me._IncidentClassification.TypeData = ViewUpdate.Type
                Me._IncidentClassification.CauseData = ViewUpdate.Cause
                Me._IncidentClassification.ProcessData = ViewUpdate.Process
                Me._IncidentClassification.ComponentData = ViewUpdate.Component
                Me._IncidentClassification.PreventionData = ViewUpdate.Prevention
                'Me._IncidentClassification.TriggerData = ViewUpdate.Trigger
                Me._IncidentClassification.DataBind()
                'BindList(Me._ddlRCFALeader, ViewUpdate.Person, False, True)
                'BindList(Me._ddlCrew, ViewUpdate.Crew, False, True)
                'BindList(Me._ddlShift, ViewUpdate.Shift, False, True)
                BindList(Me._ddlPhysicalCauses, ViewUpdate.PhysicalCauses, False, True)
                BindList(Me._ddlLatentCauses, ViewUpdate.LatentCauses, False, True)
                BindList(Me._ddlHumanCauses, ViewUpdate.HumanCauses, False, True)
            End If

            'RCFA Status


            If Not Page.IsPostBack Then
                _cblRCFAStatus.Attributes.Add("onClick", "checkAll(this,6);")
                '_cblActionDue.Attributes.Add("onClick", "checkAll(this,5);")
                Me._btnExcel1.OnClientClick = "document.getElementById('" & Me._btnExcel.ClientID & "').click();return false;"
                If userProfile IsNot Nothing Then

                    'SetDefaults()
                    _siteLocation.DivisionValue = userProfile.DefaultDivision
                    _siteLocation.FacilityValue = userProfile.DefaultFacility

                    _siteLocation.BusinessUnitValue = "All"
                    _siteLocation.AreaValue = "All"
                    _siteLocation.LineValue = "All"
                    _siteLocation.LineBreakValue = "All"

                    'If _ddlFacility.Items.FindByValue(userProfile.DefaultFacility) IsNot Nothing Then
                    '    _ddlFacility.SelectedValue = userProfile.DefaultFacility
                    'End If
                    'Me._cddlFacility.SelectedValue = userProfile.DefaultFacility

                    'If _ddlDivision.Items.FindByValue(userProfile.DefaultDivision) IsNot Nothing Then
                    '    _ddlDivision.SelectedValue = userProfile.DefaultDivision
                    'End If
                    '_cddlDivision.SelectedValue = userProfile.DefaultDivision
                End If
                'SetSelectedValue()
            Else
                'Dim dr As Data.DataTableReader
                'dr = ViewUpdate.FacilityDivision.DataSource
                'Do While dr.Read
                '    If dr.Item("Siteid") = _ddlFacility.SelectedValue Then
                '        If dr.Item("SiteID") <> "AL" Then
                '            If _ddlDivision.Items.FindByValue(dr.Item("Division")) IsNot Nothing Then
                '                _ddlDivision.SelectedValue = dr.Item("Division")
                '            End If
                '            _cddlDivision.SelectedValue = dr.Item("Division")
                '        End If
                '    End If
                'Loop
            End If
            _btnViewUpdate1.OnClientClick = "Javascript:ScrollControlInView('" & _btnViewUpdate.ClientID & "');showIncidentListing();"
            _btnViewUpdate.OnClientClick = "Javascript:ScrollControlInView('" & _btnViewUpdate.ClientID & "');showIncidentListing();"
            '_btnViewUpdate.Attributes.Add("onclick", "DisableButton(this);" & Page.ClientScript.GetPostBackEventReference(Me, _btnViewUpdate.ID.ToString()))
            Dim sc As ScriptManager
            sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
            If sc IsNot Nothing Then
                Dim loService As New ServiceReference
                loService.InlineScript = True
                loService.Path = "~/CascadingLists.asmx"
                sc.Services.Add(loService)
            End If


        Catch ex As Exception
            Throw
            Session.Remove("clsSearch")
        End Try
    End Sub


    Protected Sub _btnViewUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnViewUpdate.Click
        Dim clsSearch As New clsViewSearch
        Dim CallSource As String = String.Empty
        Dim dr As OracleDataReader = Nothing
        Dim drnew As Data.DataTableReader
        Dim sExclude As New ArrayList
        Dim ipLoc As New IP.Bids.Localization.DataLocalization(Master.RIRESOURCES)


        Try
            SearchSortDirection = "Asc"
            clsSearch.SelectStatement = " * "
            CallSource = "View"
            CallDatabase("Facility,Event Date,", CallSource)
            clsSearch = Session.Item("clsSearch")

            

            If clsSearch IsNot Nothing Then
                dr = clsSearch.Search
                sExclude.Add("RINumber")
                sExclude.Add("INCIDENT")
                sExclude.Add("RCFA_TYPE")
                drnew = ipLoc.LocalizeData(dr, sExclude)
                Me._gvIncidentListing.DataSource = drnew
                Me._gvIncidentListing.DataBind()
                'Response.Flush() 
            End If

           

            'Dim dr As OracleDataReader = clsSearch.Search
            'Dim dt As Data.DataTable = RI.SharedFunctions.ConvertReaderToDataTable(dr)
            '_divGrid.InnerHtml = DisplayTableGrid(dt)
            'Me._lblRecCount.Text = dt.Rows.Count
            Me._lblRecCount.Visible = True
            Me._lblRecordCount.Visible = True
            Me._cpeIncidentListing.Collapsed = False
            'me._cpeIncidentListing.
        Catch ex As Exception
            Throw
        Finally
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
        End Try
        'Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Page.GetType, "scrollcontrolview", "ScrollControlInView('" & _btnViewUpdate.ClientID & "');", True)
        'Page.MaintainScrollPositionOnPostBack = False
    End Sub
    Private Sub CallDatabase(ByVal Orderby As String, ByVal CallSource As String)
        Dim sqlWhere As String = String.Empty
        Dim sqlOrderby As String = String.Empty
        Dim AndOr As String = String.Empty
        Dim clsSearch As New clsViewSearch

        'With Me._SiteLocation
        If Orderby.Length = 0 Then
            Orderby = "Facility,Event Date,"
        Else
            If CallSource = "View" Or CallSource = "Sort" Then
                clsSearch.SelectStatement = ""
            Else
                clsSearch.SelectStatement = Me._sblSelectColumns.SelectedValue & ","
            End If
        End If
        If Not (_siteLocation.DivisionValue = "") Then
            clsSearch.Division = _siteLocation.DivisionValue
        End If
        If Not (_siteLocation.FacilityValue = "" Or _siteLocation.FacilityValue = "AL") Then
            clsSearch.Facility = _siteLocation.FacilityValue
            _gvIncidentListing.Columns(1).Visible = False
        Else
            _gvIncidentListing.Columns(1).Visible = True
        End If
        If Not (_siteLocation.BusinessUnitValue = "") And Not (_siteLocation.BusinessUnitValue = "All") Then
            clsSearch.BusinessUnit = _siteLocation.BusinessUnitValue
        End If
        If Not (_siteLocation.AreaValue = "") And Not (_siteLocation.AreaValue = "All") Then
            clsSearch.Area = _siteLocation.AreaValue
        End If
        If Not (_siteLocation.LineValue = "") And Not (_siteLocation.LineValue = "All") Then
            clsSearch.Line = _siteLocation.LineValue
        End If
        If Not (_siteLocation.LineBreakValue = "") And Not (_siteLocation.BusinessUnitValue = "All") Then
            clsSearch.LineBreak = _siteLocation.LineBreakValue
        End If
        'End With
        'If Me._DateRange.StartDate.Length > 0 Then
        clsSearch.StartDate = Me._DateRange.StartDate
        clsSearch.EndDate = Me._DateRange.EndDate
        'End If

        If Not (Me._cblRCFAStatus.SelectedValue = "") Then
            clsSearch.RCFAStatus = RI.SharedFunctions.GetCheckBoxValues(_cblRCFAStatus)
            'clsSearch.RCFAStatus = _cblRCFAStatus.SelectedValue
        End If

        'If Not (Me._cblActionDue.SelectedValue = "") Then
        '    clsSearch.ActionDue = RI.SharedFunctions.GetCheckBoxValues(_cblActionDue)
        'End If

        If Not (Me._txtRINumber.Text = "") Then
            clsSearch.RINumber = _txtRINumber.Text
        End If

        If Not (Me._ddlRCFALeader.SelectedValue = "") Then
            clsSearch.RCFALeader = _ddlRCFALeader.SelectedValue
        End If

        If Not (Me._ddlCrew.SelectedValue = "") Then
            clsSearch.Crew = _ddlCrew.SelectedValue
        End If

        If Not (Me._ddlShift.SelectedValue = "") Then
            clsSearch.Shift = _ddlShift.SelectedValue
        End If

        If Not (Me._txtTitleSearch.Text = "") Then
            clsSearch.Title = UCase(Trim(Me._txtTitleSearch.Text))
        End If

        If Not (Me._txtFinancialImpact.Text = "") Then
            clsSearch.FinancialImpact = Me._txtFinancialImpact.Text
        End If

        If Not (Me._IncidentType.IRISNumber = "") Then
            clsSearch.IRISNumber = Me._IncidentType.IRISNumber
        End If

        If Not (Me._IncidentType.RTS = "") Then
            clsSearch.RTS = Me._IncidentType.RTS
        End If


        'If Not (Me._IncidentType.PPR = "") Then
        clsSearch.PPR = Me._IncidentType.PPR
        'End If

        'If Not (Me._IncidentType.Recordable = "") Then
        clsSearch.Recordable = Me._IncidentType.Recordable
        'End If

        If Not (Me._IncidentType.RCFA = "") Then
            clsSearch.RCFA = Me._IncidentType.RCFA
        End If

        If Not (Me._IncidentType.SRR = "") Then
            clsSearch.SRR = Me._IncidentType.SRR
        End If

        If Not (Me._IncidentType.Quality = "") Then
            clsSearch.Quality = Me._IncidentType.Quality
        End If

        If Not (Me._IncidentType.Safety = "") Then
            clsSearch.Safety = Me._IncidentType.Safety
        End If

        If Not (Me._IncidentType.Chronic = "") Then
            clsSearch.Chronic = Me._IncidentType.Chronic
        End If

        If Not (Me._IncidentType.CertifiedKill = "") Then
            clsSearch.CertifiedKill = Me._IncidentType.CertifiedKill
        End If

        If Not (Me._IncidentType.ConstrainedAreas = "") Then
            clsSearch.ConstrainedAreas = "Yes" 'Me._IncidentType.ConstrainedAreas
        End If

        If Not (Me._IncidentType.SchedUnsched = "") Then
            clsSearch.SchedUnsched = Me._IncidentType.SchedUnsched
        End If

        If Not (Me._cbVerification.Checked = True) Then
            clsSearch.Verification = "All"
        Else
            clsSearch.Verification = "Yes"
        End If

        If Not (_ddlTrigger.SelectedValue = "") Then
            clsSearch.Trigger = _ddlTrigger.SelectedValue
        End If

        If Not (Me._IncidentClassification.TypeValue = "") Then
            clsSearch.Type = Me._IncidentClassification.TypeValue
        End If

        If Not (Me._IncidentClassification.CauseValue = "") Then
            clsSearch.Cause = Me._IncidentClassification.CauseValue
        End If
        If Not (Me._IncidentClassification.ProcessValue = "") Then
            clsSearch.Process = Me._IncidentClassification.ProcessValue
        End If
        If Not (Me._IncidentClassification.ComponentValue = "") Then
            clsSearch.Component = Me._IncidentClassification.ComponentValue
        End If
        If Not (Me._IncidentClassification.PreventionValue = "") Then
            clsSearch.Prevention = Me._IncidentClassification.PreventionValue
        End If
        If Not (Me._ddlPhysicalCauses.SelectedValue = "") Then
            clsSearch.PhysicalCauses = _ddlPhysicalCauses.SelectedValue
        End If
        If Not (Me._ddlLatentCauses.SelectedValue = "") Then
            clsSearch.LatentCauses = _ddlLatentCauses.SelectedValue
        End If
        If Not (Me._ddlHumanCauses.SelectedValue = "") Then
            clsSearch.HumanCauses = _ddlHumanCauses.SelectedValue
        End If
        'If Me._cbConstrainedAreas.Checked = True Then
        '    clsSearch.ConstrainedAreas = "Yes"
        '    'Else
        '    '    clsSearch.ConstrainedAreas = "All"
        'End If

        If Not (Me._ddlCriticality.SelectedValue = "") Then
            clsSearch.Criticality = CInt(_ddlCriticality.SelectedValue)
        End If

        clsSearch.AndOr = Me._IncidentType.SearchMode
        clsSearch.OrderBy = Orderby


        Session.Remove("clsSearch")
        Session.Add("clsSearch", clsSearch)



    End Sub

    Protected Sub _gvIncidentListing_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles _gvIncidentListing.DataBound

        If _gvIncidentListing.Rows.Count > 0 Then
            Dim footerRow As GridViewRow = Me._gvIncidentListing.FooterRow()
            Dim MyTotCost As Double = Session("totCost")
            Dim MyTotCostString As String = "$" & FormatNumber(MyTotCost, 0) 'MyTotCost.ToString("$#,##0;($#,##0);$0")
            Dim MyTotFinclImpact As Double = Session("totFinclImpact")
            Dim MyTotFinclImpactString As String = "$" & FormatNumber(MyTotFinclImpact, 0) 'MyTotFinclImpact.ToString("$#,##0;($#,##0);$0")

            footerRow.Cells(7).Text = "Totals"
            footerRow.Cells(8).Text = MyTotCostString
            footerRow.Cells(9).Text = MyTotFinclImpactString
        End If

    End Sub

    Private Sub _gvIncidentListing_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _gvIncidentListing.RowCreated
        Dim totCost As Double = 0
        Dim totFinclImpact As Double = 0
        Dim row As GridViewRow


        If e.Row.RowType = DataControlRowType.Footer Then

            ' Get the number of items in the Rows collection.
            Dim RecordCount As Integer = _gvIncidentListing.Rows.Count
            If RecordCount = 0 Then
                Me._lblRecCount.Text = Master.RIRESOURCES.GetResourceValue("NoRecordsFound")
            Else
                Me._lblRecCount.Text = RecordCount
            End If
            Me._lblRecCount.Visible = True
            Me._lblRecordCount.Visible = True


            If _gvIncidentListing.Rows.Count > 0 Then
                For Each row In _gvIncidentListing.Rows
                    Dim _Cost As WebControls.Literal = TryCast(row.FindControl("_Cost"), Literal)
                    Dim _TotCost As WebControls.Literal = TryCast(row.FindControl("_TotCost"), Literal)
                    If _Cost IsNot Nothing Then
                        totCost = totCost + _Cost.Text
                        _Cost.Text = "$" & _Cost.Text
                    End If
                    If _TotCost IsNot Nothing Then
                        totFinclImpact = totFinclImpact + _TotCost.Text
                        _TotCost.Text = "$" & _TotCost.Text
                    End If
                Next
            End If
            ' If the GridView control contains any records, display 
            ' the last name of each author in the GridView control.
            '    If count = 0 Then

            '        .Text = "No Data Found"

            '        Dim row As GridViewRow
            '        For Each row In AuthorsGridView.Rows

            '            Message.Text &= row.Cells(0).Text & "<br />"

            '        Next

            '    End If
        ElseIf e.Row.RowType = DataControlRowType.DataRow Then
            Dim _Cost As WebControls.Literal = TryCast(e.Row.FindControl("_Cost"), Literal)
            Dim _TotCost As WebControls.Literal = TryCast(e.Row.FindControl("_TotCost"), Literal)
            If _Cost IsNot Nothing Then
                _Cost.Text = FormatNumber(e.Row.DataItem("Cost"), 0)
            End If
            If _TotCost IsNot Nothing Then
                _TotCost.Text = FormatNumber(e.Row.DataItem("TotCost"), 0)
            End If
        End If
        Session.Remove("totCost")
        Session.Add("totCost", totCost)
        Session.Remove("totFinclImpact")
        Session.Add("totFinclImpact", totFinclImpact)




    End Sub

    Private Sub _gvIncidentListing_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles _gvIncidentListing.Sorting
        Dim Orderby As String = String.Empty
        Dim CallSource As String = String.Empty
        Dim sortDir As SortDirection = SortDirection.Ascending
        Dim clsSearch As clsViewSearch
        Dim SortExpPrev As String = CType(Session.Item("SortExpPrev"), String)
        Dim dr As OracleDataReader = Nothing
        Dim drnew As Data.DataTableReader
        Dim sExclude As New ArrayList
        Dim ipLoc As New IP.Bids.Localization.DataLocalization(Master.RIRESOURCES)
        Try
            If (e.SortExpression = "COST" Or e.SortExpression = "TOTCOST") And e.SortExpression <> SortExpPrev Then
                Orderby = e.SortExpression & " DESC"
                Me.SearchSortDirection = "ASC"
                sortDir = SortDirection.Descending
            Else
                If Me.SearchSortDirection = "DESC" And (e.SortExpression Is Nothing Or e.SortExpression = SortExpPrev) Then
                    Orderby = e.SortExpression & " DESC"
                    Me.SearchSortDirection = "ASC"
                    sortDir = SortDirection.Descending
                Else
                    Orderby = e.SortExpression & " ASC"
                    Me.SearchSortDirection = "DESC"
                    sortDir = SortDirection.Ascending
                End If
            End If

            SortExpPrev = e.SortExpression

            Session.Remove("SortExpPrev")
            Session.Add("SortExpPrev", SortExpPrev)

            CallSource = "Sort"
            CallDatabase(Orderby, CallSource)
            clsSearch = Session.Item("clsSearch")
            sExclude.Add("RINumber")
            sExclude.Add("Description")
            sExclude.Add("Incident")
            If clsSearch IsNot Nothing Then
                dr = clsSearch.Search
                drnew = ipLoc.LocalizeData(dr, sExclude)
                Dim dt As Data.DataTable = RI.SharedFunctions.ConvertReaderToDataTable(drnew)
                If (e.SortExpression = "COST" Or e.SortExpression = "TOTCOST" Or e.SortExpression.ToUpper = UCase("EventDate")) Then
                    Me._gvIncidentListing.DataSource = dt.DefaultView
                Else
                    If dt.Columns.Contains(e.SortExpression) Then
                        dt.DefaultView.Sort = Orderby
                    End If
                    Me._gvIncidentListing.DataSource = dt.DefaultView
                    End If
                    Me._gvIncidentListing.DataBind()
                    RI.SharedFunctions.AddGlyph(_gvIncidentListing, _gvIncidentListing.HeaderRow, e.SortExpression, sortDir)
            End If
        Catch
            Throw
        Finally
            drnew = Nothing
            dr = Nothing
            ipLoc = Nothing
        End Try

    End Sub

    'Protected Sub _btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnExcel.Click
    ' Dim clsSearch As New clsViewSearch
    ''clsSearch.SelectStatement = " siteid,eventdate, sitename,business_unit, area, line, linebreak, rinumber, title, cost, financial_impact, division, recordable, rcfa, rcfalevel, rcfatriggerdesc, rcfasafety, rcfaquality, chronic, certifiedkill,crew,shift, type,cause,process,component,prevention,leader,analysiscompdate,actioncompdate,description,safety,quality, rcfa_type, originatingapplication"
    '   CallDatabase("EVENTDATE")

    '  Web.UI.ScriptManager.RegisterStartupScript(Me, Page.GetType, "pop", "PopupWindow('excel.aspx?id=4','Excel',800,600,'yes','no','yes');", True)

    'End Sub

    Private Sub _pnlExcelSelect_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles _pnlExcelSelect.Load
        'Dim UserName As RI.CurrentUserProfile
        Dim clsExcel As New clsExcelSelect(GetUserid())
        If clsExcel IsNot Nothing Then

            With Me._sblSelectColumns
                .DataSource = clsExcel.ColumnName.DataSource
                .DataTextField = clsExcel.ColumnName.DataTextField
                .DataValueField = clsExcel.ColumnName.DataValueField
                '.SelectedValue() = "Business_Unit, Area, Line"
                '.SelectedDataSource = clsExcel.ColumnList.DataSource
                '.SelectedDataTextField = clsExcel.ColumnList.DataTextField
                '.SelectedDataValueField = clsExcel.ColumnList.DataValueField
                .LocalizeData = True
                .DataBind()
                If Not Page.IsPostBack Then
                    Dim dr As Data.DataTableReader = clsExcel.ColumnList.DataSource
                    dr.Read()
                    If dr.HasRows Then
                        .SelectedValue() = dr.Item(clsExcel.ColumnList.DataValueField)
                    Else
                        .SelectedValue() = "RINumber,Event Date,Division,Business_Unit,Area,Line,Line Break,Title,Functional Location,Equip_Description,Recordable,RCFA,Chronic,Certified Kill,Type,Cause,Equip/Process,Prevention,Component,Mill_Equp,Downtime,Repair Cost,Fincl_Impact "
                        'RINumber "RINumber", DIVISION "Division",SITENAME "Facility",
                    End If
                End If
            End With
        End If
    End Sub

    Protected Sub _btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnOK.Click
        Dim clsSearch As New clsViewSearch
        Dim CallSource As String = String.Empty
        'Dim userProfile As RI.CurrentUserProfile = RI.SharedFunctions.GetUserProfile
        Dim dr As Data.DataTableReader = Nothing
        Dim drnew2 As OracleDataReader = Nothing
        Dim sExclude As New ArrayList
        Dim ipLoc As New IP.Bids.Localization.DataLocalization(Master.RIRESOURCES)


        Try
            ' This code is to repopulate the grid after the excel spreadsheet is displayed.
            CallSource = "View"
            CallDatabase("Facility,Event Date,", CallSource)
            clsSearch = Session.Item("clsSearch")
            sExclude.Add("RINumber")
            sExclude.Add("Description")
            sExclude.Add("Incident")
            sExclude.Add("SUBAREA")
            If clsSearch IsNot Nothing Then
                'MJP 5/11/2015 - Commented out the code below so that we only display the Excel file when the user clicks on the Excel button.  This should speed up the response time.
                'dr = clsSearch.Search
                'drnew = ipLoc.LocalizeData(dr, sExclude)
                'Me._gvIncidentListing.DataSource = drnew
                'Me._gvIncidentListing.DataBind()
            End If
            'clsSearch.AndOr = Me._IncidentType.SearchMode
            clsSearch.OrderBy = Me._sblSelectColumns.SelectedValue
            'clsSearch.SelectStatement = Me._sblSelectColumns.SelectedValue
            CallSource = "Excel"
            CallDatabase(clsSearch.OrderBy, CallSource)
            clsSearch = Session.Item("clsSearch")
            'sExclude.Add("Downtime")
            sExclude.Add("Functional Location")
            Dim dt As Data.DataTable = clsSearch.GetDataTable
            dr = dt.CreateDataReader '.clsSearch.Search
            'If dr.HasRows Then
            'drnew = ipLoc.LocalizeData(dr, sExclude)
            Master.DisplayExcel(SharedFunctions.WriteExcelXML(dr, sExclude))
            'drnew = ipLoc.LocalizeData(dt.CreateDataReader, sExclude)
            'Me._gvIncidentListing.DataSource = drnew
            'Me._gvIncidentListing.DataBind()

            'End If

            UpdateExcelList(userProfile.Username, clsSearch.SelectStatement)
            'Master.DisplayExcel(clsSearch.Search)
            'Master.DisplayExcel(drnew)


            'Web.UI.ScriptManager.RegisterStartupScript(Me, Page.GetType, "pop", "PopupWindow('excel.aspx?id=4','Excel',800,600,'yes','yes','yes');", True)           
        Catch
            Throw
        Finally
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
        End Try

    End Sub

    Private Sub UpdateExcelList(ByVal User As String, ByVal InSelect As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        'Dim ds As System.Data.DataSet = Nothing
        Dim status As String
        Dim dr As OracleDataReader = Nothing
        'Check input paramaters

        Try

            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = User
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_selectlist"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = InSelect
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "reladmin.riview.excelupdatelist")
            If status <> "0" Then
                Throw New Data.DataException("UpdateExcelList")
            End If

        Catch ex As Exception
            Throw New Data.DataException("UpdateExcelList", ex)
        Finally
            If Not dr Is Nothing Then dr = Nothing
        End Try
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If Not Page.IsPostBack Then
            SetSelectedValue()
        End If
        
    End Sub
    Private Sub SetSelectedValue()
        Dim clsSearch As clsViewSearch = CType(Session("clsSearch"), clsViewSearch)
        Dim dr As OracleDataReader = Nothing
        Dim drnew As Data.DataTableReader
        Dim sExclude As New ArrayList
        Dim ipLoc As New IP.Bids.Localization.DataLocalization(Master.RIRESOURCES)

        Try
            RI.SharedFunctions.DisablePageCache(Response)
            If clsSearch IsNot Nothing Then
                clsSearch.SelectStatement = ""

               
                'Reselect Controls
                '_siteLocation.FacilityValue = clsSearch.Facility
                '                _siteLocation.BusinessUnitValue = clsSearch.BusinessUnit
                _siteLocation.DivisionValue = clsSearch.Division
                _siteLocation.FacilityValue = clsSearch.Facility
                _siteLocation.BusinessUnitValue = clsSearch.BusinessUnit
                _siteLocation.AreaValue = clsSearch.Area
                _siteLocation.LineValue = clsSearch.Line
                _siteLocation.LineBreakValue = clsSearch.LineBreak
                
                Me._IncidentClassification.TypeValue = clsSearch.Type
                Me._IncidentClassification.CauseValue = clsSearch.Cause
                Me._IncidentClassification.ProcessValue = clsSearch.Process
                Me._IncidentClassification.ComponentValue = clsSearch.Component
                Me._IncidentClassification.PreventionValue = clsSearch.Prevention
                _ddlTrigger.SelectedValue = clsSearch.Trigger

                Me._DateRange.SelectedDateRange = -1
                Me._DateRange.StartDate = clsSearch.StartDate
                Me._DateRange.EndDate = clsSearch.EndDate
                'List remaining controls here

                Me._cddlRCFALeader.SelectedValue = clsSearch.RCFALeader
                Me._cddlCrew.SelectedValue = clsSearch.Crew
                Me._cddlShift.SelectedValue = clsSearch.Shift
                Me._ddlPhysicalCauses.SelectedValue = clsSearch.PhysicalCauses
                Me._ddlLatentCauses.SelectedValue = clsSearch.LatentCauses
                Me._ddlHumanCauses.SelectedValue = clsSearch.HumanCauses
                Me._txtRINumber.Text = clsSearch.RINumber
                Me._txtFinancialImpact.Text = clsSearch.FinancialImpact
                Me._txtTitleSearch.Text = clsSearch.Title

                Me._IncidentType.DisplayMode = ucIncidentTypes.IncidentMode.Search
                'Me._IncidentType.SearchMode = clsSearch.AndOr
                If clsSearch.IRISNumber IsNot Nothing Then
                    Me._IncidentType.IRISNumber = clsSearch.IRISNumber
                End If
                If clsSearch.Chronic IsNot Nothing Then
                    Me._IncidentType.Chronic = clsSearch.Chronic
                End If
                If clsSearch.CertifiedKill IsNot Nothing Then
                    Me._IncidentType.CertifiedKill = clsSearch.CertifiedKill
                End If
                If clsSearch.ConstrainedAreas IsNot Nothing Then
                    Me._IncidentType.ConstrainedAreas = clsSearch.ConstrainedAreas
                End If
                If clsSearch.RCFA IsNot Nothing Then
                    Me._IncidentType.RCFA = clsSearch.RCFA
                End If
                If clsSearch.Recordable IsNot Nothing Then
                    Me._IncidentType.Recordable = clsSearch.Recordable
                End If
                If clsSearch.Safety IsNot Nothing Then
                    Me._IncidentType.Safety = clsSearch.Safety
                End If
                If clsSearch.Quality IsNot Nothing Then
                    Me._IncidentType.Quality = clsSearch.Quality
                End If
                If clsSearch.RTS IsNot Nothing Then
                    Me._IncidentType.RTS = clsSearch.RTS
                End If
                If clsSearch.PPR IsNot Nothing Then
                    Me._IncidentType.PPR = clsSearch.PPR
                End If
                If clsSearch.SRR IsNot Nothing Then
                    Me._IncidentType.SRR = clsSearch.SRR
                End If

                If clsSearch.RCFAStatus IsNot Nothing Then
                    Me.RCFAStatus = clsSearch.RCFAStatus
                End If
                'If clsSearch.ActionDue IsNot Nothing Then
                '    Me.ActionDue = clsSearch.ActionDue
                'End If

                'If clsSearch.ConstrainedAreas.Length > 0 Then
                '    'If _cbConstrainedAreas.Checked = True Items.FindByValue(clsSearch.ConstrainedAreas) IsNot Nothing Then
                '    _cbConstrainedAreas.Checked = True
                '    'Items.FindByValue(clsSearch.ConstrainedAreas).Selected = True
                '    'If
                'End If

                If _ddlCriticality.Items.FindByValue(clsSearch.Criticality) IsNot Nothing Then
                    _ddlCriticality.Items.FindByValue(clsSearch.Criticality).Selected = True
                End If

                dr = clsSearch.Search
                If dr IsNot Nothing Then
                    If dr.HasRows Then
                        sExclude.Add("RINumber")
                        sExclude.Add("INCIDENT")
                        sExclude.Add("RCFA_TYPE")
                        drnew = ipLoc.LocalizeData(dr, sExclude)
                        'Display the Search results
                        Me._gvIncidentListing.DataSource = drnew
                        Me._gvIncidentListing.DataBind()
                        If Not (clsSearch.Facility = "" Or clsSearch.Facility = "AL") Then
                            'clsSearch.Facility = _ddlFacility.SelectedValue
                            _gvIncidentListing.Columns(1).Visible = False
                        Else
                            _gvIncidentListing.Columns(1).Visible = True
                        End If
                    End If
                End If
            Else
                Me._DateRange.SelectedDateRange = RI_User_Controls_Common_ucDateRange.range.YearToDate
            End If
        Catch ex As Exception
            Throw
        Finally
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
        End Try
    End Sub

    Private Function GetGlobalJSVar() As String
        Dim sb As New StringBuilder
        Dim ddlFac As DropDownList = CType(Me._siteLocation.FindControl("_ddlFacility"), DropDownList)
        If ddlFac IsNot Nothing Then
            sb.Append("var site = $get('")
            sb.Append(ddlFac.ClientID)
            sb.Append("');")
            sb.AppendLine()
        End If

        Dim ddlDiv As DropDownList = CType(Me._siteLocation.FindControl("_ddlDivision"), DropDownList)
        If ddlDiv IsNot Nothing Then
            sb.Append("var division = $get('")
            sb.Append(ddlDiv.ClientID)
            sb.Append("');")
            sb.AppendLine()
        End If       
        sb.Append("var incidentListing = $get('")
        sb.Append(Me._pnlIncidentListing.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var imgListingGrid = $get('")
        sb.Append(Me._imgIncidentListing.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var lblRecordCount = $get('")
        sb.Append(Me._lblRecordCount.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var lblRecCount = $get('")
        sb.Append(Me._lblRecCount.ClientID)
        sb.Append("');")
        sb.AppendLine()

        Return sb.ToString
    End Function

    Function DisplayTableGrid(ByVal dt As Data.DataTable) As String

        Dim sb As New StringBuilder
        sb.Append("<TABLE cellspacing='0' rules='all' border='1' style='border-collapse:collapse;'>")
        'Build Header Row
        sb.Append("<TR>")
        For i As Integer = 0 To dt.Columns.Count - 1
            sb.Append("<TH>")
            sb.Append(dt.Columns(i).ColumnName)
            sb.Append("</TH>")
        Next
        sb.Append("</TR>")


        For Each row As Data.DataRow In dt.Rows
            sb.Append("<TR>")
            For Each dc As Data.DataColumn In dt.Columns
                sb.Append("<TD>")
                sb.Append(row(dc))
                sb.Append("</TD>")
            Next
            sb.Append("</TR>")
        Next            '
        sb.Append("</TABLE>")
        Return sb.ToString
    End Function

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        ViewUpdate = Nothing
        ExcelSelect = Nothing
        userProfile = Nothing
    End Sub

    Private Sub SetDefaults()
        Dim defaults As New RIUserDefaults.CurrentUserDefaults(userProfile.Username, RIUserDefaults.Applications.Availability, ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString)

        If defaults.DoesDefaultValueExist(RIUserDefaults.UserProfileTypes.SiteId) Then
            _siteLocation.FacilityValue = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.SiteId).ToString
        End If

        If defaults.DoesDefaultValueExist(RIUserDefaults.UserProfileTypes.BusinessUnit) Then
            _siteLocation.BusinessUnitValue = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.BusinessUnit).ToString
        Else
            _siteLocation.BusinessUnitValue = "All"
        End If

        If defaults.DoesDefaultValueExist(RIUserDefaults.UserProfileTypes.Area) Then
            _siteLocation.AreaValue = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.Area)
        Else
            _siteLocation.AreaValue = "All"
        End If

        If defaults.DoesDefaultValueExist(RIUserDefaults.UserProfileTypes.Line) Then
            _siteLocation.LineValue = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.Line)
        Else
            _siteLocation.LineValue = "All"
        End If

        If defaults.DoesDefaultValueExist(RIUserDefaults.UserProfileTypes.Machine) Then
            _siteLocation.LineBreakValue = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.Machine)
        Else
            _siteLocation.LineBreakValue = "All"
        End If


    End Sub
End Class
