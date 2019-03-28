Imports RI
Imports RI.SharedFunctions
Imports Devart.Data.Oracle

Partial Class MOC
    Inherits RIBasePage

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

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.ShowMOCMenu()
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("ViewMOC", True, "MOC"))
        'Me._DateRange.ChangeDateLabel = "true"
    End Sub

    Function GetUserid() As String
        userProfile = RI.SharedFunctions.GetUserProfile
        Return userProfile.Username
    End Function

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim userProfile As RI.CurrentUserProfile = RI.SharedFunctions.GetUserProfile
        Dim clsSearch As clsMOCViewSearch = Session("clsMOCSearch")

        If Not Page.IsPostBack Then
            Me._DateRange.SelectedDateRange = -1
            _DateRange.StartDate = DatePart(DateInterval.Month, Today()) & "/1/" & DatePart(DateInterval.Year, Today())
            '_DateRange.EndDate = "12/31/" & DatePart(DateInterval.Year, Today()) + 1
            If userProfile IsNot Nothing Then
                _siteLocation.FacilityValue = userProfile.DefaultFacility
                _siteLocation.DivisionValue = userProfile.DefaultDivision
                _siteLocation.BusinessUnitValue = "All"
                _siteLocation.AreaValue = "All"
                _siteLocation.LineValue = "All"
                _siteLocation.LineBreakValue = "All"
            End If
        End If

        'Dim clsDDL As New clsViewMOCDDL
        'clsDDL.GetDDLData()

        'RI.SharedFunctions.BindList(Me._ddlStatus, clsDDL.Status, False, True)

        Dim sc As ScriptManager
        sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
        If sc IsNot Nothing Then
            Dim loService As New ServiceReference
            loService.InlineScript = True
            loService.Path = "~/CascadingLists.asmx"
            sc.Services.Add(loService)
        End If
        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "MOC") Then
            Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "MOC", Page.ResolveClientUrl("~/MOC/MOC.js"))
        End If

    End Sub

    Private Sub SetDefaults()
        Dim defaults As New RIUserDefaults.CurrentUserDefaults(userProfile.Username, RIUserDefaults.Applications.MTT, ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString)

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

    Protected Sub _btnViewUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnViewUpdate.Click
        Dim clsSearch As New clsMOCViewSearch
        Dim CallSource As String = String.Empty
        SearchSortDirection = "Asc"
        CallSource = "View"
        CallDatabase("", CallSource)
        clsSearch = Session.Item("clsMOCSearch")
        Me._gvMOCListing.DataSource = clsSearch.Search
        Me._gvMOCListing.DataBind()
        Me._gvMOCListing.Visible = True
        Dim RecordCount As Integer = _gvMOCListing.Rows.Count
        Me._lblRecCount.Text = RecordCount
    End Sub

    Private Sub CallDatabase(ByVal Orderby As String, ByVal CallSource As String)
        'Me._MOCCategory.RefreshDisplay()

        Dim sqlOrderby As String = String.Empty
        Dim AndOr As String = String.Empty
        Dim clsSearch As New clsMOCViewSearch

        If Orderby.Length = 0 Then
            Orderby = ""
        End If

        If Not (_tbMocNumber.Text = "") Then
            clsSearch.MOCNumber = _tbMocNumber.Text
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

        If Not (_MOCType.Types = "") Then
            clsSearch.Type = _MOCType.Types
        End If

        If Not (_MOCCategory.Category = "") And Not (_MOCCategory.Category = "All") Then
            clsSearch.Category = _MOCCategory.Category
        End If

        If Not (_MOCCategory.SubCategory = "") And Not (_MOCCategory.SubCategory = "All") Then
            clsSearch.SubCategory = _MOCCategory.SubCategory
        End If

        If Not (_MOCClass.Classification = "") And Not (_MOCClass.Classification = "All") Then
            clsSearch.Classification = _MOCClass.Classification
        End If

        If Not (_ddlInitiator.SelectedValue = "") And Not (_ddlInitiator.SelectedValue = "All") Then
            clsSearch.Initiator = _ddlInitiator.SelectedValue
        End If

        'If Not (_ddlStatus.SelectedValue = "") And Not (_ddlStatus.SelectedValue = "All") Then
        '    clsSearch.Status = _ddlStatus.SelectedValue
        'End If

        If Not (MOCStatus.Status = "") And Not (MOCStatus.Status = "All") Then
            clsSearch.Status = MOCStatus.Status
        End If

        If Not (_siteLocation.FacilityValue = "" Or _siteLocation.FacilityValue = "AL") Then
            clsSearch.Facility = _siteLocation.FacilityValue
            _gvMOCListing.Columns(0).Visible = False
        Else
            _gvMOCListing.Columns(0).Visible = True
        End If

        If Not (_ddlOwner.SelectedValue = "") And Not (_ddlOwner.SelectedValue = "All") Then
            clsSearch.Owner = _ddlOwner.SelectedValue
        End If

        If Not (_txtTitleSearch.Text = "") Then
            clsSearch.Title = _txtTitleSearch.Text
        End If

        clsSearch.Username = GetUserid()

        clsSearch.OrderBy = Orderby

        Session.Remove("clsMOCSearch")
        Session.Add("clsMOCSearch", clsSearch)

    End Sub

    Private Sub _gvMOCListing_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _gvMOCListing.RowCreated

        If e.Row.RowType = DataControlRowType.Footer Then

            ' Get the number of items in the Rows collection.
            Dim RecordCount As Integer = _gvMOCListing.Rows.Count
            Me._lblRecCount.Text = RecordCount

        End If

    End Sub

    Private Sub _gvMOCListing_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles _gvMOCListing.Sorting
        Dim Orderby As String = String.Empty
        Dim CallSource As String = String.Empty
        Dim sortDir As SortDirection = SortDirection.Ascending
        Dim clsSearch As clsMOCViewSearch
        Dim SortExpPrev As String = CType(Session.Item("MOCSortExpPrev"), String)

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

        Session.Remove("MOCSortExpPrev")
        Session.Add("MOCSortExpPrev", SortExpPrev)

        CallSource = "Sort"
        CallDatabase(Orderby, CallSource)
        clsSearch = Session.Item("clsMOCSearch")
        Me._gvMOCListing.DataSource = clsSearch.Search
        Me._gvMOCListing.DataBind()
        RI.SharedFunctions.AddGlyph(_gvMOCListing, _gvMOCListing.HeaderRow, e.SortExpression, sortDir)

    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If Not Page.IsPostBack Then
            SetSelectedValue()
        End If
    End Sub

    Protected Sub _btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnExcel.Click
        Dim clsExcel As New clsMOCViewSearch
        Dim CallSource As String = String.Empty
        SearchSortDirection = "Asc"
        CallSource = "View"
        CallExcelDatabase("", CallSource)
        clsExcel = Session.Item("clsExcelSearch")
        Me._gvMOCListing.DataSource = clsExcel.ExcelSearch

        Dim ipLoc As New IP.Bids.Localization.DataLocalization(Master.RIRESOURCES)
        Dim dr As Data.DataTableReader = clsExcel.ExcelSearch
        'Master.DisplayExcel(ipLoc.WriteExcelXml(dr, New ArrayList))
        Master.DisplayExcel(SharedFunctions.WriteExcelXml(dr, New ArrayList))
        dr = Nothing

        'Dim key As String
        'key = "MOCExcelSearch_" & clsExcel.Facility & "_" & clsExcel.Division & "_" & clsExcel.BusinessUnit & "_" & clsExcel.Area & "_" & clsExcel.Line & "_" & clsExcel.LineBreak & "_" & clsExcel.StartDate & "_" & clsExcel.EndDate & "_" & clsExcel.Type & "_" & clsExcel.Category & "_" & clsExcel.Classification & "_" & clsExcel.OrderBy
        'If HttpRuntime.Cache.Item(key) IsNot Nothing Then
        ' Web.UI.ScriptManager.RegisterStartupScript(Me, Page.GetType, "pop", "PopupWindow('excel.aspx?id=1','Excel',800,600,'yes','no','yes');", True)
        'End If
    End Sub
    Private Sub CallExcelDatabase(ByVal Orderby As String, ByVal CallSource As String)

        Dim sqlOrderby As String = String.Empty
        Dim AndOr As String = String.Empty
        Dim clsSearch As New clsMOCViewSearch

        If Orderby.Length = 0 Then
            Orderby = ""
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

        If Not (_MOCType.Types = "") Then
            clsSearch.Type = _MOCType.Types
        End If

        If Not (_MOCCategory.Category = "") And Not (_MOCCategory.Category = "All") Then
            clsSearch.Category = _MOCCategory.Category
        End If

        If Not (_MOCCategory.SubCategory = "") And Not (_MOCCategory.SubCategory = "All") Then
            clsSearch.SubCategory = _MOCCategory.SubCategory
        End If

        If Not (_MOCClass.Classification = "") And Not (_MOCClass.Classification = "All") Then
            clsSearch.Classification = _MOCClass.Classification
        End If

        If Not (_ddlInitiator.SelectedValue = "") And Not (_ddlInitiator.SelectedValue = "All") Then
            clsSearch.Initiator = _ddlInitiator.SelectedValue
        End If

        If Not (_ddlOwner.SelectedValue = "") And Not (_ddlOwner.SelectedValue = "All") Then
            clsSearch.Owner = _ddlOwner.SelectedValue
        End If

        'If Not (_ddlStatus.SelectedValue = "") And Not (_ddlStatus.SelectedValue = "All") Then
        '    clsSearch.Status = _ddlStatus.SelectedValue
        'End If

        If Not (MOCStatus.Status = "") And Not (MOCStatus.Status = "All") Then
            clsSearch.Status = MOCStatus.Status
        End If

        clsSearch.OrderBy = Orderby

        Session.Remove("clsExcelSearch")
        Session.Add("clsExcelSearch", clsSearch)

    End Sub

    Private Sub SetSelectedValue()
        Dim clsSearch As clsMOCViewSearch = Session("clsMOCSearch")
        Dim dr As Data.DataTableReader = Nothing
        Try
            RI.SharedFunctions.DisablePageCache(Response)
            Me._MOCType.DisplayMode = ucMOCTypes.MOCMode.Search
            If clsSearch IsNot Nothing Then
                dr = clsSearch.Search
                If dr IsNot Nothing Then
                    If dr.HasRows Then
                        _siteLocation.FacilityValue = clsSearch.Facility
                        _siteLocation.AreaValue = clsSearch.Area
                        _siteLocation.LineValue = clsSearch.Line
                        _siteLocation.DivisionValue = clsSearch.Division

                        Me._DateRange.SelectedDateRange = -1
                        Me._DateRange.StartDate = clsSearch.StartDate
                        Me._DateRange.EndDate = clsSearch.EndDate

                        'Display the Search results
                        Me._gvMOCListing.DataSource = clsSearch.Search
                        Me._gvMOCListing.DataBind()
                        Dim RecordCount As Integer = _gvMOCListing.Rows.Count
                        Me._lblRecCount.Text = RecordCount

                    End If
                End If
            Else
                Me._DateRange.SelectedDateRange = RI_User_Controls_Common_ucDateRange.range.YearToDate
            End If
        Catch ex As Exception
            Throw
        Finally
            If dr IsNot Nothing Then dr = Nothing
        End Try
    End Sub

End Class
