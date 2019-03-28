Partial Class RI_EventTracking
    Inherits RIBasePage

    Dim EventView As clsEventView
    Dim userProfile As RI.CurrentUserProfile = Nothing

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("Availability Tracking"))
        Me._btnAddEvent.OnClientClick = "Javascript:viewPopUp('EnterSimpleRI.aspx?OrigApp=PI','fu');return false"

    End Sub

    Function GetUserid() As String
        userProfile = RI.SharedFunctions.GetUserProfile
        Return userProfile.Username
    End Function

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ' Response.Redirect("http://rigpi.ipaper.com/RI/ri/eventtracking.aspx")
            userProfile = RI.SharedFunctions.GetUserProfile
            Dim sc As ScriptManager
            sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
            ScriptManager.RegisterClientScriptInclude(Me._upEvents, _upEvents.GetType, "EventTracking", Page.ResolveClientUrl("~/ri/EventTracking.js"))

            If Not Page.IsPostBack Then

                SetDefaults()

                'Me._DateRange.SelectedDateRange = RI_User_Controls_Common_ucDateRange.range.EnteredLast7Days
                'Me._DateRange.showtime = True
                Me._startEndDates.SelectedDateRange = RI_User_Controls_Common_ucStartEndDates.range.EnteredLast7Days

                '_gvEvents.DataSource = Nothing
                '_gvEvents.DataBind()
                '
                GetEvents()
            Else
                For i = 0 To Me._gvEvents.Rows.Count - 1
                    Dim rwgEventDetail As RealWorld.Grids.BulkEditGridView = Me._gvEvents.Rows(i).FindControl("_gvSplitEventDetail")
                    rwgEventDetail.DataSource = ViewState("child" & Me._gvEvents.DataKeys.Item(i).Value)
                    rwgEventDetail.DataBind()
                Next
            End If

        Catch ex As Exception
            Throw
            Session.Remove("clsSearch")
        End Try
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
    Protected Sub _btnViewUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnViewUpdate.Click
        GetEvents()
    End Sub

    Protected Sub GetEvents()
        Dim clsEvent As New clsEventView()
        Dim dr As Devart.Data.Oracle.OracleDataReader = Nothing

        Try
            clsEvent.Facility = _siteLocation.FacilityValue

            If Not (_siteLocation.BusinessUnitValue = "") And Not (_siteLocation.BusinessUnitValue = "All") Then
                clsEvent.BusinessUnit = _siteLocation.BusinessUnitValue
            End If
            If Not (_siteLocation.AreaValue = "") And Not (_siteLocation.AreaValue = "All") Then
                clsEvent.Area = _siteLocation.AreaValue
            End If
            If Not (_siteLocation.LineValue = "") And Not (_siteLocation.LineValue = "All") Then
                clsEvent.Line = _siteLocation.LineValue
            End If
            If Not (_siteLocation.LineBreakValue = "") And Not (_siteLocation.BusinessUnitValue = "All") Then
                clsEvent.LineBreak = _siteLocation.LineBreakValue
            End If

            clsEvent.StartDate = Me._startEndDates.StartDate
            clsEvent.EndDate = Me._startEndDates.EndDate

            clsEvent.Type = Me._rblEventShow.SelectedValue

            If clsEvent.GetDataTable IsNot Nothing Then
                _gvEvents.DataSource = clsEvent.SearchDT
                _gvEvents.DataBind()
                If _gvEvents.Rows.Count > 0 Then
                    _gvEvents.HeaderRow.TableSection = TableRowSection.TableHeader
                End If
            End If

            Session.Remove("clsEventListing")
            Session.Add("clsEventListing", clsEvent)

        Catch ex As Exception
            Throw
        Finally
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
        End Try
    End Sub
 
    Protected Sub _btnSaveEvents_Click(sender As Object, e As EventArgs) Handles _btnSaveEvents.Click
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim intRow As Integer
        Dim status As String

        Try
            For i = 0 To Me._gvEvents.DirtyRows.Count - 1
                Dim clsEvent As New clsEventUpdate
                intRow = _gvEvents.DirtyRows.Item(i).DataItemIndex
                With clsEvent
                    .RINumber = Me._gvEvents.DataKeys.Item(intRow).Value
                    .SchedDT = CType(Me._gvEvents.Rows(intRow).FindControl("_ddlSchedUnsched"), DropDownList).SelectedValue
                    .Type = CType(Me._gvEvents.Rows(intRow).FindControl("_ddlType"), DropDownList).SelectedValue
                    If .Type.Length > 0 Then
                        .Type = Replace(.Type, ">", ":")
                    Else
                        .Type = String.Empty
                    End If
                    .Process = CType(Me._gvEvents.Rows(intRow).FindControl("_ddlProcess"), DropDownList).SelectedValue
                    If .Process.Length > 0 Then
                        ''.Type = Mid(.Process, InStr(.Process, "-") + 2)
                        ''.Process = Mid(.Process, 1, InStr(.Process, "-") - 2)
                        .Process = Replace(.Process, ">", ":")
                    Else
                        .Process = String.Empty
                    End If
                    .Component = CType(Me._gvEvents.Rows(intRow).FindControl("_ddlComponent"), DropDownList).SelectedValue
                    If .Component.Length > 0 Then
                        .Component = Replace(.Component, ">", ":")
                    Else
                        .Component = String.Empty
                    End If
                    .Cause = CType(Me._gvEvents.Rows(intRow).FindControl("_ddlReason"), DropDownList).SelectedValue
                    If .Cause.Length > 0 Then
                        .Cause = Replace(.Cause, ">", ":")
                    Else
                        .Cause = String.Empty
                    End If
                    .Crew = CType(Me._gvEvents.Rows(intRow).FindControl("_ddlCrew"), DropDownList).SelectedValue
                    .comment = CType(Me._gvEvents.Rows(intRow).FindControl("_atbComments"), AdvancedTextBox.AdvancedTextBox).Text
                    .Username = userProfile.Username
                    status = clsEvent.SaveEvent()
                End With
            Next

            For i = 0 To Me._gvEvents.Rows.Count - 1
                'intRow = _gvEvents.Rows.Item(i).DataItemIndex
                Dim hdnSplitRowCount As HiddenField = Me._gvEvents.Rows(i).FindControl("_hdnRowCount")
                'If hdnSplitRowCount > 0 Then
                'If _gvEvents.Rows.RowType = DataControlRowType.DataRow Then
                '.comment = CType(Me._gvEvents.Rows(intRow).FindControl("_atbComments"), AdvancedTextBox.AdvancedTextBox).Text

                Dim rwgEventDetail As GridView = CType(Me._gvEvents.Rows(i).FindControl("_gvSplitEventDetail"), GridView)
                If rwgEventDetail IsNot Nothing Then
                    For j = 0 To rwgEventDetail.Rows.Count - 1

                        Dim clsSplitEvent As New clsEventUpdate
                        intRow = rwgEventDetail.Rows.Item(j).DataItemIndex
                        'intRow = rwgEventDetail.DirtyRows.Item(i).DataItemIndex
                        With clsSplitEvent
                            .RINumber = rwgEventDetail.DataKeys.Item(intRow).Value
                            .SchedDT = CType(rwgEventDetail.Rows(intRow).FindControl("_ddlSplitSchedUnsched"), DropDownList).SelectedValue
                            .Type = Request.Form(rwgEventDetail.Rows(intRow).FindControl("_ddlSplitType").UniqueID)
                            '.Type = CType(rwgEventDetail.Rows(intRow).FindControl("_ddlSplitType"), DropDownList).SelectedValue
                            If .Type.Length > 0 Then
                                .Type = Replace(.Type, ">", ":")
                            Else
                                .Type = String.Empty
                            End If
                            .Process = Request.Form(rwgEventDetail.Rows(intRow).FindControl("_ddlSplitProcess").UniqueID)
                            '.Process = CType(rwgEventDetail.Rows(intRow).FindControl("_ddlSplitProcess"), DropDownList).SelectedValue
                            If .Process.Length > 0 Then
                                .Process = Replace(.Process, ">", ":")
                            Else
                                .Process = String.Empty
                            End If
                            .Component = Request.Form(rwgEventDetail.Rows(intRow).FindControl("_ddlSplitComponent").UniqueID)
                            '.Component = CType(rwgEventDetail.Rows(intRow).FindControl("_ddlSplitComponent"), DropDownList).SelectedValue
                            If .Component.Length > 0 Then
                                .Component = Replace(.Component, ">", ":")
                            Else
                                .Component = String.Empty
                            End If
                            .Cause = Request.Form(rwgEventDetail.Rows(intRow).FindControl("_ddlSplitReason").UniqueID)
                            '.Cause = CType(rwgEventDetail.Rows(intRow).FindControl("_ddlSplitReason"), DropDownList).SelectedValue
                            If .Cause.Length > 0 Then
                                .Cause = Replace(.Cause, ">", ":")
                            Else
                                .Cause = String.Empty
                            End If

                            .Crew = Request.Form(rwgEventDetail.Rows(intRow).FindControl("_ddlSplitCrew").UniqueID)
                            '.Crew = CType(rwgEventDetail.Rows(intRow).FindControl("_ddlSplitCrew"), DropDownList).SelectedValue
                            '.Crew = CType(rwgEventDetail.Rows(intRow).FindControl("_hdnSplitCrew"), HiddenField).Value

                            .comment = Request.Form(rwgEventDetail.Rows(intRow).FindControl("_atbSplitComment").UniqueID)
                            .Username = userProfile.Username
                            status = clsSplitEvent.SaveEvent()
                        End With
                    Next
                End If
                'End If
            Next

            GetEvents()
        Catch
            Throw
        End Try
    End Sub
    Protected Sub gvEventDetail_RowDataBound(sender As Object, e As GridViewRowEventArgs)

        Dim hdRowChange As HiddenField = TryCast(e.Row.FindControl("_rowChanged"), HiddenField)
        If hdRowChange IsNot Nothing Then
            Dim rowChangedJS As String = String.Format("document.getElementById('{0}').value='changed';", hdRowChange.ClientID)
            Dim ddlCrew As DropDownList = TryCast(e.Row.FindControl("_ddlSplitCrew"), DropDownList)
            If ddlCrew IsNot Nothing Then
                ddlCrew.Attributes.Add("onChange", rowChangedJS)
            End If
        End If

        Dim _ddlSplitSchedUnsched As DropDownList = TryCast(e.Row.FindControl("_ddlSplitSchedUnsched"), DropDownList)
        If _ddlSplitSchedUnsched IsNot Nothing Then
            If e.Row.DataItem("scheddt").ToString = "Yes" Then
                _ddlSplitSchedUnsched.SelectedValue = "Yes"
            Else
                _ddlSplitSchedUnsched.SelectedValue = "No"
            End If
        End If

    End Sub

    Protected Sub _gvEvents_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles _gvEvents.RowDataBound

        Try


            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim clsEvent As New clsEventView()
                clsEvent.RINumber = e.Row.DataItem("rinumber").ToString
                clsEvent.Type = Me._rblEventShow.SelectedValue

                'e.Row.Attributes.Add("ondblclick", "showDetail(this);")
                Dim pnlEvents As Panel = e.Row.FindControl("_pnlParent")

                If clsEvent.GetEventsDataTable.Rows.Count > 0 Then
                    Dim rwgEventDetail As RealWorld.Grids.BulkEditGridView = e.Row.FindControl("_gvSplitEventDetail")
                    pnlEvents.Visible = False
                    ViewState("child" & e.Row.DataItem("rinumber").ToString) = clsEvent.SearchDT
                    rwgEventDetail.DataSource = ViewState("child" & e.Row.DataItem("rinumber").ToString)
                    'rwgEventDetail.DataSource = clsEvent.SearchDT
                    rwgEventDetail.DataBind()

                    Dim hdnRowCount As HiddenField = e.Row.FindControl("_hdnRowCount")
                    hdnRowCount.Value = rwgEventDetail.Rows.Count
                Else
                    'Dim ddlBUA As DropDownList = e.Row.FindControl("_ddlBUA")
                    'ddlBUA.Items.Add(e.Row.DataItem("bua").ToString)

                    ' Dim cddlCrew As AjaxControlToolkit.CascadingDropDown = e.Row.FindControl("_cddlCrew")
                    'Dim Event_Crew As String = e.Row.DataItem("crew").ToString
                    'cddlCrew.SelectedValue = Event_Crew

                    'Dim cddlType As AjaxControlToolkit.CascadingDropDown = e.Row.FindControl("_cddlTypes")
                    'Dim Event_Type As String = e.Row.DataItem("cause").ToString()
                    'cddlType.SelectedValue = Event_Type

                    'Dim cddlProcess As AjaxControlToolkit.CascadingDropDown = e.Row.FindControl("_cddlProcess1")
                    Dim ddlProcess As DropDownList = e.Row.FindControl("_ddlProcess")
                    Dim Event_Process As String = e.Row.DataItem("process").ToString

                    Dim Event_ProcessType = Event_Process
                    '' Dim Event_ProcessType = Event_Process & " - " & Event_Type

                    If Event_Process = "" Or Event_Process Is Nothing Then

                        '    'Dim ddlType As DropDownList = e.Row.FindControl("_ddlType")
                        '    'cddlType.ParentControlID = String.Empty
                        '    'cddlProcess.ParentControlID = ddlType.ClientID
                        '    'cddlType.DataBind()
                        '    '    Else
                        '    '        _cddlTypes.ParentControlID = _ddlProcess.ClientID
                        '    '    End If

                        Dim Default_Process As String = e.Row.DataItem("defaultprocess").ToString
                        ddlProcess.SelectedValue = Default_Process
                    Else
                        ddlProcess.SelectedValue = Event_ProcessType
                    End If

                    'Dim cddlComponent As AjaxControlToolkit.CascadingDropDown = e.Row.FindControl("_cddlComponent")
                    'Dim Event_Component As String = e.Row.DataItem("component").ToString
                    'cddlComponent.SelectedValue = Event_Component

                    'Dim cddlReason As AjaxControlToolkit.CascadingDropDown = e.Row.FindControl("_cddlReason")
                    'Dim Event_Reason As String = e.Row.DataItem("reason").ToString
                    'cddlReason.SelectedValue = Event_Reason

                    Dim ddlSchedDT As DropDownList = e.Row.FindControl("_ddlSchedUnsched")
                    If e.Row.DataItem("scheddt").ToString = "Yes" Then
                        ddlSchedDT.SelectedValue = "Yes"
                    Else
                        ddlSchedDT.SelectedValue = "No"
                    End If
                    'Else
                    pnlEvents.Visible = True
                End If

            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub _btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnExcel.Click
        Dim clsExcel As New clsEventView
        Dim CallSource As String = String.Empty
        CallSource = "View"
        Dim sExclude As New ArrayList
        Dim ipLoc As New IP.Bids.Localization.DataLocalization(Master.RIRESOURCES)
        Dim dr As Devart.Data.Oracle.OracleDataReader = Nothing
        Dim drnew As Data.DataTableReader
        sExclude.Add("RINumber")

        CallExcelDatabase("", CallSource)
        clsExcel = Session.Item("clsExcelSearch")
        If clsExcel IsNot Nothing Then
            dr = clsExcel.ExcelSearch
            drnew = ipLoc.LocalizeData(dr, sExclude)
            'Me._gvEvents.DataSource = drnew
            'Me._gvEvents.DataBind()
        End If

        Master.DisplayExcel(clsExcel.ExcelSearch)

        'Dim key As String
        'key = "OutageExcelSearch_" & clsExcel.Facility & "_" & clsExcel.Division & "_" & clsExcel.BusinessUnit & "_" & clsExcel.Area & "_" & clsExcel.Line & "_" & clsExcel.Title & "_" & clsExcel.OutageCoord & "_" & clsExcel.SDCategory & "_" & clsExcel.StartDate & "_" & clsExcel.EndDate & "_" & clsExcel.OrderBy
        'If HttpRuntime.Cache.Item(key) IsNot Nothing Then
        'Web.UI.ScriptManager.RegisterStartupScript(Me, Page.GetType, "pop", "PopupWindow('excel.aspx?id=1','Excel',800,600,'yes','no','yes');", True)
        'End If
    End Sub

    Private Sub CallExcelDatabase(ByVal Orderby As String, ByVal CallSource As String)

        Dim sqlWhere As String = String.Empty
        Dim sqlOrderby As String = String.Empty
        Dim AndOr As String = String.Empty
        Dim clsSearch As New clsEventView

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
            clsSearch.StartDate = Me._startEndDates.StartDate
            clsSearch.EndDate = Me._startEndDates.EndDate
        End If

        clsSearch.Type = Me._rblEventShow.SelectedValue

        Session.Remove("clsExcelSearch")
        Session.Add("clsExcelSearch", clsSearch)

    End Sub

    Private Sub _rblEventShow_SelectedIndexChanged(sender As Object, e As EventArgs) Handles _rblEventShow.SelectedIndexChanged
        GetEvents()
    End Sub

    Protected Sub _gvEvents_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles _gvEvents.RowCommand
        Dim status As String
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = _gvEvents.Rows(index)

            Dim rIndex As Integer = CType(e.CommandArgument, Integer)
            Dim thisID As String = _gvEvents.DataKeys(rIndex).Value.ToString()

            If (e.CommandName = "Select") Then
                _lbSplitRINumber.Text = thisID 'Row.DataItem("rinumber").ToString
                _lbBUA.Text = CType(row.FindControl("_tbAreaLine"), Label).Text()
                _tbStart.Text = CType(row.FindControl("_tbStartDate"), Label).Text()
                _tbEnd.Text = CType(row.FindControl("_tbEndDate"), Label).Text()
                _tbStart2.Text = Nothing
                _tbEnd2.Text = Nothing
                _tbStart3.Text = Nothing
                _tbEnd3.Text = Nothing
                _hdnStartDate.Value = CType(row.FindControl("_tbStartDate"), Label).Text()
                _hdnEndDate.Value = CType(row.FindControl("_tbEndDate"), Label).Text()
                _lbParentStartDate.Text = CType(row.FindControl("_tbStartDate"), Label).Text()
                _lbParentEndDate.Text = CType(row.FindControl("_tbEndDate"), Label).Text()
                '_rvEnd.MinimumValue = Now() '_lbParentStartDate.Text
                '_rvEnd.MaximumValue = Now()  '_lbParentEndDate.Text
                '_rvEnd.maximumvalue = _hdnEndDate.Value
                GetEvents()
                popup.Show()

            ElseIf (e.CommandName = "Remove") Then
                Dim clsEvent As New clsEventUpdate
                With clsEvent
                    .RINumber = thisID
                    .Username = userProfile.Username
                    status = clsEvent.RemoveSplitEvent()
                    GetEvents()
                End With
            End If
        Catch
            Throw
        End Try
    End Sub


    Protected Sub _btnUpdateSplits_Click(sender As Object, e As EventArgs) Handles _btnUpdateSplits.Click
        Dim status As String

        Try
            Dim clsEvent As New clsEventUpdate
            With clsEvent
                .RINumber = Me._lbSplitRINumber.Text
                .Crew = _ddlCrew1.SelectedValue
                .SchedDT = _ddlSchedUnsched1.SelectedValue
                .startDate = _hdnStartDate.Value
                .endDate = _tbEnd.Text
                .Process = Me._ddlSplitProcess1.SelectedValue '.ProcessValue
                .Type = Me._ddlSplitType1.SelectedValue
                If .Type.Length > 0 Then
                    .Type = Replace(.Type, ">", ":")
                Else
                    .Type = String.Empty
                End If
                .Component = Me._ddlSplitComponent1.SelectedValue
                .Cause = Me._ddlSplitReason1.SelectedValue
                .Username = userProfile.Username
                status = clsEvent.SaveSplitEvent()
            End With

            clsEvent = Nothing
            clsEvent = New clsEventUpdate
            With clsEvent
                .RINumber = Me._lbSplitRINumber.Text
                .Crew = Me._ddlCrew2.SelectedValue
                .SchedDT = _ddlSchedUnsched2.SelectedValue
                .startDate = _hdnStart2.Value.ToString()
                .endDate = _tbEnd2.Text
                .Process = Me._ddlSplitProcess2.SelectedValue
                .Type = Me._ddlSplitType2.SelectedValue
                If .Type.Length > 0 Then
                    .Type = Replace(.Type, ">", ":")
                Else
                    .Type = String.Empty
                End If
                .Component = Me._ddlSplitComponent2.SelectedValue
                .Cause = Me._ddlSplitReason2.SelectedValue
                .Username = userProfile.Username
                status = clsEvent.SaveSplitEvent()
            End With

            If Me._hdnEnd3.Value.ToString() <> "" Then

                clsEvent = Nothing
                clsEvent = New clsEventUpdate
                With clsEvent
                    .RINumber = Me._lbSplitRINumber.Text
                    .Crew = Me._ddlCrew3.SelectedValue
                    .SchedDT = _ddlSchedUnsched3.SelectedValue
                    .startDate = _hdnStart3.Value.ToString()
                    .endDate = _hdnEndDate.Value.ToString
                    .Process = Me._ddlSplitProcess3.SelectedValue
                    .Type = Me._ddlSplitType3.SelectedValue
                    If .Type.Length > 0 Then
                        .Type = Replace(.Type, ">", ":")
                    Else
                        .Type = String.Empty
                    End If
                    .Component = Me._ddlSplitComponent3.SelectedValue
                    .Cause = Me._ddlSplitReason3.SelectedValue
                    .Username = userProfile.Username
                    status = clsEvent.SaveSplitEvent()
                End With
            End If

            GetEvents()

        Catch
            Throw
        End Try
    End Sub


End Class