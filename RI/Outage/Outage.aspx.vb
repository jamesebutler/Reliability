Imports RI
Imports RI.SharedFunctions
Imports Devart.Data.Oracle
Imports System.Xml
Partial Class RI_Outage
    Inherits RIBasePage

    Dim clsSearch As New clsOutageViewSearch()
    'Dim ExcelSelect As clsOutageExcelSelect
    'Dim HoldSortExpression As String

    Public Property SearchSortDirection() As String
        Get
            Return Session("SearchSortDirection")
        End Get
        Set(ByVal value As String)
            Session("SearchSortDirection") = value
        End Set
    End Property
    Public Property Status() As String
        Get
            Return RI.SharedFunctions.GetCheckBoxValues(_cblSDCategory)
        End Get
        Set(ByVal value As String)
            RI.SharedFunctions.SetCheckBoxValues(_cblSDCategory, value)
        End Set
    End Property

    Public Function GetUploadURL(ByVal strfile As String) As String
        Dim strURL As String
        Dim strServername As String = Request("Server_Name").ToLower
        'If you are running on your local pc, the files will be saved to ridev.
        If strServername.Contains("localhost") Then
            strServername = "http://ridev/Uploads/"
        Else
            strServername = "http://" & strServername & "/Uploads/"
        End If

        strURL = strServername & strfile
        Return strURL
    End Function
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.ShowOutageMenu()
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("ViewOutage", True, "Outage"))
        If Request.QueryString("Dest") = "Chart" Then
            Web.UI.ScriptManager.RegisterStartupScript(Me, Page.GetType, "pop", "PopupWindow('http://techweb/reliability/Other Links/2007 Outage Schedule -5-31dw.mpp','OutageChart',800,600,'yes','no','yes');", True)
            'Response.Redirect("outage.aspx")
        End If
    End Sub

    Function GetUserid() As String
        Dim userProfile As RI.CurrentUserProfile = Nothing
        userProfile = RI.SharedFunctions.GetUserProfile
        Return userProfile.Username
    End Function

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim userProfile As RI.CurrentUserProfile = RI.SharedFunctions.GetUserProfile
        Dim clsSearch As New clsOutageViewSearch
        'Dim clsSearch As clsOutageViewSearch = Session("clsOutageSearch")
        'Dim clsSearchUpdate As clsOutageViewUpdate = Session("clsOutageSearchUpdate")

        'clsSearch = Session("clsOutageSearch")
        ScriptManager.RegisterStartupScript(Me.Page, Page.GetType, "GetGlobalJSVarOutage", GetGlobalJSVarOutage, True)

        'If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "DHTMLWindow") Then
        '    Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "DHTMLWindow", Page.ResolveClientUrl("~/windowfiles/dhtmlwindow.js"))
        'End If
        'If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "ModalWindow") Then
        '    Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "ModalWindow", Page.ResolveClientUrl("~/modalfiles/modal.js"))
        'End If
        'If Page.IsPostBack Then
        'viewupdate = New clsContractor()
        '    Me._lblRecCount.Text = "0"
        'Else
        '    'ViewUpdate = New clsOutageViewUpdate(userProfile.DefaultFacility, userProfile.InActiveFlag, userProfile.DefaultDivision)
        'End If

        'ALA 09272010 - Change so that CheckAll selects all Categories, it was not selecting Field Day
        If Not Page.IsPostBack Then
            PopulateData()
            'RI.SharedFunctions.BindList(Me._ddlResources, clsSearch.ResourcesList, False, True)
            'RI.SharedFunctions.BindList(Me._ddlContractor, clsSearch.Contractor, False, True)
            _cblSDCategory.Attributes.Add("onClick", "checkAll(this,9);")
            _DateRange.StartDate = DatePart(DateInterval.Month, Today()) & "/1/" & DatePart(DateInterval.Year, Today())
            _DateRange.EndDate = "12/31/" & DatePart(DateInterval.Year, Today()) + 1
            If userProfile IsNot Nothing Then
                _siteLocation.FacilityValue = userProfile.DefaultFacility
                _siteLocation.DivisionValue = userProfile.DefaultDivision
            End If
        End If

        Dim sc As ScriptManager
        sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
        If sc IsNot Nothing Then
            Dim loService As New ServiceReference
            loService.InlineScript = True
            loService.Path = "~/CascadingLists.asmx" '"~/outage/OutageCascadingLists.asmx"
            sc.Services.Add(loService)
        End If
        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "Outage") Then
            Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "Outage", Page.ResolveClientUrl("~/Outage/Outage.js"))
        End If

        'Me._btnGantt.OnClientClick = Master.GetPopupWindowJS("~/Outage/GanttViewerAnyChart.aspx", "Gantt", 800, 600, True, False, False)
        'helper = New GridViewHelper(Me._gvOutageListing, False)
        'helper.RegisterGroup("GroupTitle", True, True)
        'helper.RegisterGroup("StartDate", True, True)
        'helper.ApplyGroupSort()

    End Sub

    Protected Sub _btnViewUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnViewUpdate.Click
        'Dim clsSearch As New clsOutageViewSearch
        Dim CallSource As String = String.Empty
        Dim dr As OracleDataReader = Nothing
        Dim drnew As Data.DataTableReader
        Dim ipLoc As New IP.Bids.Localization.DataLocalization(Master.RIRESOURCES)
        Dim sExclude As New ArrayList

        SearchSortDirection = "Asc"
        clsSearch.SelectStatement = " * "
        CallSource = "View"
        CallDatabase("", CallSource)
        clsSearch = Session.Item("clsOutageSearch")

        If clsSearch IsNot Nothing Then
            dr = clsSearch.Search
            sExclude.Add("StartDate")
            sExclude.Add("OutageNumber")
            sExclude.Add("SHUTDOWNCATEGORY")
            drnew = ipLoc.LocalizeData(dr, sExclude)
            Me._gvOutageListing.DataSource = drnew
            Me._gvOutageListing.DataBind()
            'Response.Flush() 
        End If


        'Me._gvOutageListing.DataSource = clsSearch.Search
        'Me._gvOutageListing.DataBind()
        'Me._gvOutageListing.Visible = True
        ''Me._pnlGanttChart.Visible = False
        _ifrGantt.Attributes.CssStyle("Display") = "none"
        Dim RecordCount As Integer = _gvOutageListing.Rows.Count
        Me._lblRecCount.Text = RecordCount

    End Sub

    Protected Sub _btnGantt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnGantt.Click
        'Dim clsSearch As New clsOutageViewSearch
        Dim CallSource As String = String.Empty
        SearchSortDirection = "Asc"
        clsSearch.SelectStatement = " * "
        CallSource = "View"
        CallDatabase("", CallSource)
        clsSearch = Session.Item("clsOutageSearch")

        'Web.UI.ScriptManager.RegisterStartupScript(_upViewScreen, _upViewScreen.GetType, "pop", "calcIframeHeight('" & Me._ifrGantt.ClientID & "')", True)
        'Me._gvOutageListing.DataSource = clsSearch.Search
        'Me._gvOutageListing.DataBind()
        _ifrGantt.Attributes.Add("onload", "javascript:calcIframeHeight('" & Me._ifrGantt.ClientID & "')")
        _ifrGantt.Attributes.CssStyle("display") = ""
        'Me._OutageGanttChart.GetGanttChart()
        'Me._pnlGanttChart.Visible = True
        Me._gvOutageListing.Visible = False
        'Me._OutageGanttChart.Visible = True
        'Dim RecordCount As Integer = _gvOutageListing.Rows.Count
        'Me._lblRecCount.Text = RecordCount
    End Sub


    Private Sub CallDatabase(ByVal Orderby As String, ByVal CallSource As String)

        Dim sqlWhere As String = String.Empty
        Dim sqlOrderby As String = String.Empty
        Dim AndOr As String = String.Empty
        'Dim clsSearch As New clsOutageViewSearch

        If Orderby.Length = 0 Then
            Orderby = ""
        Else
            If CallSource = "View" Or CallSource = "Sort" Then
                clsSearch.SelectStatement = ""
            End If
        End If
        If Not (_siteLocation.DivisionValue = "" Or _siteLocation.DivisionValue = "All") Then
            clsSearch.Division = _siteLocation.DivisionValue
        End If
        If Not (_siteLocation.FacilityValue = "" Or _siteLocation.FacilityValue = "AL") Then
            clsSearch.Facility = _siteLocation.FacilityValue
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

        If Me._DateRange.StartDate.Length > 0 Then
            clsSearch.StartDate = _DateRange.StartDate
            clsSearch.EndDate = _DateRange.EndDate
        End If

        If Not (Me._cblSDCategory.SelectedValue = "") Then
            clsSearch.SDCategory = RI.SharedFunctions.GetCheckBoxValues(_cblSDCategory)
        End If

        If Not (Me._tbTitleSearch.Text = "") Then
            clsSearch.Title = _tbTitleSearch.Text
        End If

        If Not (Me._ddlOutageCoord.SelectedValue = "") Then
            clsSearch.OutageCoord = _ddlOutageCoord.SelectedValue
        End If

        If Not (Me._ddlContractor.SelectedValue = "") Then
            clsSearch.Contractor = Me._ddlContractor.SelectedValue
        End If

        If (Me._cbAnnualOutage.Checked = True) Then
            clsSearch.AnnualFlag = "Y"
        End If

        If Not (Me._ddlResources.SelectedValue = "") Then
            clsSearch.Resources = Me._ddlResources.SelectedValue
        End If

        clsSearch.AndOr = "And"

        clsSearch.OrderBy = Orderby

        Session.Remove("clsOutageSearch")
        Session.Add("clsOutageSearch", clsSearch)

    End Sub

    Private Sub _gvOutageListing_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _gvOutageListing.RowCreated

        If e.Row.RowType = DataControlRowType.Footer Then

            ' Get the number of items in the Rows collection.
            Dim RecordCount As Integer = _gvOutageListing.Rows.Count
            Me._lblRecCount.Text = RecordCount

        End If

    End Sub

    Private Sub _gvOutageListing_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles _gvOutageListing.Sorting
        Dim Orderby As String = String.Empty
        Dim CallSource As String = String.Empty
        Dim sortDir As SortDirection = SortDirection.Ascending
        'Dim clsSearch As clsOutageViewSearch
        Dim SortExpPrev As String = CType(Session.Item("OutageSortExpPrev"), String)


        If Me.SearchSortDirection = "DESC" And (e.SortExpression Is Nothing Or e.SortExpression = SortExpPrev) Then
            Orderby = e.SortExpression & " DESC"
            Me.SearchSortDirection = "ASC"
            sortDir = SortDirection.Descending
        Else
            Orderby = e.SortExpression & " ASC"
            Me.SearchSortDirection = "DESC"
            sortDir = SortDirection.Ascending
        End If

        SortExpPrev = e.SortExpression

        Session.Remove("OutageSortExpPrev")
        Session.Add("OutageSortExpPrev", SortExpPrev)

        CallSource = "Sort"
        CallDatabase(Orderby, CallSource)
        clsSearch = Session.Item("clsOutageSearch")
        Me._gvOutageListing.DataSource = clsSearch.Search
        Me._gvOutageListing.DataBind()
        RI.SharedFunctions.AddGlyph(_gvOutageListing, _gvOutageListing.HeaderRow, e.SortExpression, sortDir)

    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If Not Page.IsPostBack Then
            SetSelectedValue()
        End If
    End Sub

    Protected Sub _btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnExcel.Click
        Dim clsExcel As New clsOutageViewSearch
        Dim CallSource As String = String.Empty
        SearchSortDirection = "Asc"
        clsExcel.SelectStatement = " * "
        CallSource = "View"


        Dim sExclude As New ArrayList
        Dim ipLoc As New IP.Bids.Localization.DataLocalization(Master.RIRESOURCES)
        Dim dr As OracleDataReader = Nothing
        Dim drnew As Data.DataTableReader
        sExclude.Add("OutageNumber")
        sExclude.Add("SHUTDOWNCATEGORY")

        CallExcelDatabase("", CallSource)
        clsExcel = Session.Item("clsExcelSearch")
        If clsExcel IsNot Nothing Then
            dr = clsExcel.ExcelSearch
            drnew = ipLoc.LocalizeData(dr, sExclude)
            Me._gvOutageListing.DataSource = drnew
            Me._gvOutageListing.DataBind()
            'Response.Flush() 
        End If
        'Me._gvOutageListing.DataSource = clsExcel.ExcelSearch
        Master.DisplayExcel(clsExcel.ExcelSearch)

        'Dim key As String
        'key = "OutageExcelSearch_" & clsExcel.Facility & "_" & clsExcel.Division & "_" & clsExcel.BusinessUnit & "_" & clsExcel.Area & "_" & clsExcel.Line & "_" & clsExcel.Title & "_" & clsExcel.OutageCoord & "_" & clsExcel.SDCategory & "_" & clsExcel.StartDate & "_" & clsExcel.EndDate & "_" & clsExcel.OrderBy
        'If HttpRuntime.Cache.Item(key) IsNot Nothing Then
        'Web.UI.ScriptManager.RegisterStartupScript(Me, Page.GetType, "pop", "PopupWindow('excel.aspx?id=1','Excel',800,600,'yes','no','yes');", True)
        'End If
    End Sub
    'Protected Sub _btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnExcel.Click
    '    Dim clsExcel As New clsOutageViewSearch
    '    Dim CallSource As String = String.Empty
    '    Dim sExclude As New ArrayList
    '    Dim ipLoc As New IP.Bids.Localization.DataLocalization(Master.RIRESOURCES)
    '    Dim dr As OracleDataReader = Nothing
    '    Dim drnew As Data.DataTableReader

    '    SearchSortDirection = "Asc"
    '    clsExcel.SelectStatement = " * "
    '    CallSource = "View"
    '    CallExcelDatabase("", CallSource)
    '    clsExcel = Session.Item("clsExcelSearch")

    '    If clsExcel IsNot Nothing Then
    '        dr = clsExcel.ExcelSearch
    '        sExclude.Add("OutageNumber")
    '        sExclude.Add("SHUTDOWNCATEGORY")
    '        drnew = ipLoc.LocalizeData(dr, sExclude)
    '        Me._gvOutageListing.DataSource = drnew
    '        Me._gvOutageListing.DataBind()
    '        'Response.Flush() 
    '    End If
    '    'Me._gvOutageListing.DataSource = clsExcel.ExcelSearch
    '    CallSource = "Excel"
    '    CallExcelDatabase(clsExcel.OrderBy, CallSource)
    '    clsExcel = Session.Item("clsSearch")
    '    'sExclude.Add("Downtime")
    '    'dr = clsExcel.Search
    '    If dr.HasRows Then
    '        '    Master.DisplayExcel(SharedFunctions.WriteExcelXml(dr, sExclude))
    '        Master.DisplayExcel(dr)
    '        'Master.DisplayExcel(clsExcel.ExcelSearch)
    '    End If
    '    'Dim key As String
    '    'key = "OutageExcelSearch_" & clsExcel.Facility & "_" & clsExcel.Division & "_" & clsExcel.BusinessUnit & "_" & clsExcel.Area & "_" & clsExcel.Line & "_" & clsExcel.Title & "_" & clsExcel.OutageCoord & "_" & clsExcel.SDCategory & "_" & clsExcel.StartDate & "_" & clsExcel.EndDate & "_" & clsExcel.OrderBy
    '    'If HttpRuntime.Cache.Item(key) IsNot Nothing Then
    '    'Web.UI.ScriptManager.RegisterStartupScript(Me, Page.GetType, "pop", "PopupWindow('excel.aspx?id=1','Excel',800,600,'yes','no','yes');", True)
    '    'End If
    'End Sub
    Private Sub CallExcelDatabase(ByVal Orderby As String, ByVal CallSource As String)

        Dim sqlWhere As String = String.Empty
        Dim sqlOrderby As String = String.Empty
        Dim AndOr As String = String.Empty
        Dim clsSearch As New clsOutageViewSearch

        If Orderby.Length = 0 Then
            Orderby = ""
        Else
            If CallSource = "View" Or CallSource = "Sort" Then
                clsSearch.SelectStatement = ""
            End If
        End If
        If Not (_siteLocation.DivisionValue = "" Or _siteLocation.DivisionValue = "All") Then
            clsSearch.Division = _siteLocation.DivisionValue
        End If
        If Not (_siteLocation.FacilityValue = "" Or _siteLocation.FacilityValue = "AL") Then
            clsSearch.Facility = _siteLocation.FacilityValue
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

        If Me._DateRange.StartDate.Length > 0 Then
            clsSearch.StartDate = _DateRange.StartDate
            clsSearch.EndDate = _DateRange.EndDate
        End If

        If Not (Me._cblSDCategory.SelectedValue = "") Then
            clsSearch.SDCategory = RI.SharedFunctions.GetCheckBoxValues(_cblSDCategory)
        End If

        If Not (Me._tbTitleSearch.Text = "") Then
            clsSearch.Title = _tbTitleSearch.Text
        End If

        If Not (Me._ddlOutageCoord.SelectedValue = "") Then
            clsSearch.OutageCoord = _ddlOutageCoord.SelectedValue
        End If

        If Not (Me._ddlContractor.SelectedValue = "") Then
            clsSearch.Contractor = Me._ddlContractor.SelectedValue
        End If

        If (Me._cbAnnualOutage.Checked = "True") Then
            clsSearch.AnnualFlag = "Y"
        End If

        If Not (Me._ddlResources.SelectedValue = "") Then
            clsSearch.Resources = _ddlResources.SelectedValue
        End If

        clsSearch.AndOr = "And"

        clsSearch.OrderBy = Orderby

        Session.Remove("clsExcelSearch")
        Session.Add("clsExcelSearch", clsSearch)

    End Sub

    'Protected Sub _btnViewChart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnViewChart.Click
    '    Web.UI.ScriptManager.RegisterStartupScript(Me, Page.GetType, "pop", "PopupWindow('http://techweb/reliability/Other Links/2007 Outage Schedule -5-31dw.mpp','OutageChart',800,600,'yes','no','yes');", True)
    'End Sub
    Private Sub SetSelectedValue()
        'Dim clsSearch As clsOutageViewSearch = Session("clsOutageSearch")
        clsSearch = Session("clsOutageSearch")
        'Dim dr As OracleDataReader = Nothing
        Dim dr As Devart.Data.Oracle.OracleDataReader = Nothing
        'Dim dr As OracleDataReader = Nothing
        Try
            RI.SharedFunctions.DisablePageCache(Response)
            If clsSearch IsNot Nothing Then
                dr = clsSearch.Search
                If dr IsNot Nothing Then
                    If dr.HasRows Then
                        'Reselect Controls

                        _siteLocation.FacilityValue = clsSearch.Facility
                        _siteLocation.BusinessUnitValue = clsSearch.BusinessUnit
                        _siteLocation.AreaValue = clsSearch.Area
                        _siteLocation.LineValue = clsSearch.Line
                        _siteLocation.DivisionValue = clsSearch.Division
                        '_cddlCoordinator.SelectedValue = clsSearch.OutageCoord
                        _tbTitleSearch.Text = clsSearch.Title

                        Me._DateRange.SelectedDateRange = -1
                        Me._DateRange.StartDate = clsSearch.StartDate
                        Me._DateRange.EndDate = clsSearch.EndDate

                        If clsSearch.SDCategory IsNot Nothing Then
                            Me.Status = clsSearch.SDCategory
                        End If
                        'Display the Search results
                        Me._gvOutageListing.DataSource = clsSearch.Search
                        Me._gvOutageListing.DataBind()
                        Dim RecordCount As Integer = _gvOutageListing.Rows.Count
                        Me._lblRecCount.Text = RecordCount
                    End If
                End If
                'Else
                'Me._DateRange.SelectedDateRange = RI_User_Controls_Common_ucDateRange.range.YearToDate
            End If
        Catch ex As Exception
            Throw
        Finally
            If dr IsNot Nothing Then dr = Nothing
            '  If Not dr.IsClosed Then dr.Close()
        End Try
    End Sub


    Private Function GetGlobalJSVarOutage() As String
        Dim sb As New StringBuilder
        Dim ddlFac As DropDownList = CType(Me._siteLocation.FindControl("_ddlFacility"), DropDownList)
        If ddlFac IsNot Nothing Then
            sb.Append("var site = $get('")
            sb.Append(ddlFac.ClientID)
            sb.Append("');")
            sb.AppendLine()
            sb.Append("var facility = $get('")
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

        Return sb.ToString
    End Function

    Private Sub PopulateData()
        'Dim ContractorList As New clsContractor()
        'Dim clsViewSearch As New clsOutageViewUpdate()

        RI.SharedFunctions.BindList(Me._ddlContractor, clsSearch.ContractorList, False, True)
        RI.SharedFunctions.BindList(Me._ddlResources, clsSearch.ResourcesList, False, True)
        'RI.SharedFunctions.BindList(Me._ddlContractor, clsSearch.Contractor, False, True)

        'Dim paramCollection As New OracleParameterCollection
        'Dim param As New OracleParameter
        'Dim ds As System.Data.DataSet = Nothing
        'Dim ActiveFlag As String = String.Empty

        'Try
        '    param = New OracleParameter
        '    param.ParameterName = "rsContractor"
        '    param.OracleDbType = OracleDbType.Cursor
        '    param.Direction = Data.ParameterDirection.Output
        '    paramCollection.Add(param)

        '    Dim key As String = "Contractors"
        '    ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.contractorddl", key, 0)

        '    If ds IsNot Nothing Then
        '        If ds.Tables.Count >= 1 Then
        '            'Contractor                    
        '            _ddlContractor.DataSource = ds.Tables(0).CreateDataReader
        '            _ddlContractor.DataTextField = "Contractor"
        '            _ddlContractor.DataValueField = "ContractorSeqId"
        '            _ddlContractor.DataBind()
        '            _ddlContractor.Items.Insert(0, "")
        '        End If
        '    End If
        'Catch ex As Exception
        '    Throw
        'Finally
        '    If ds IsNot Nothing Then
        '        ds.Dispose()
        '        ds = Nothing
        '    End If
        '    paramCollection = Nothing
        'End Try
    End Sub

End Class
