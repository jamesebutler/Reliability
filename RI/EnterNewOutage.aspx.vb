
Partial Class RI_EnterNewOutage
    Inherits RIBasePage

    '    Dim enterRI As clsEnterNewRI
    '    Dim currentRI As clsCurrentRI
    '    Dim selectedFacility As String = String.Empty
    '    Dim selectedBusArea As String = String.Empty
    '    Dim selectedLine As String = String.Empty
    '    Dim userProfile As RI.CurrentUserProfile = Nothing

    '    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
    '        'Response.Redirect("../../riindex.asp", True)     

    '    End Sub

    '    Private Sub DisplayUpdateMenu()
    '        Dim ret As String = "return false;"
    '        'Me._pnlUpdateButtons.Visible = True
    '        'Me._btnAnalysisWorkspace.OnClientClick = Master.GetPopupWindowJS("~/ri/Analysisworkspace.aspx?RINumber=" & currentRI.RINumber, , 800, 600, False, False, True) & ret
    '        'Me._btnDocumentCauses.OnClientClick = "" & ret
    '        'Me._btnAttachments.OnClientClick = Master.GetPopupWindowJS("~/ri/FileUpload.aspx?RINumber=" & currentRI.RINumber, , 773, 400) & ret
    '        'Me._btnActionItems.OnClientClick = Master.GetPopupWindowJS("~/ri/FileUpload.aspx?RINumber=" & currentRI.RINumber, , 773, 400) & ret
    '    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '        RI.SharedFunctions.DisablePageCache(Response)

        '        userProfile = RI.SharedFunctions.GetUserProfile

        '        If Not Page.IsPostBack Then
        '            If Request.QueryString("RINumber") IsNot Nothing Then
        '                currentRI = New clsCurrentRI(Request.QueryString("RINumber"))
        '                If currentRI.SiteID Is Nothing Then currentRI = Nothing
        '                If currentRI IsNot Nothing Then
        '                    enterRI = New clsEnterNewRI(userProfile.Username, currentRI.SiteID, userProfile.InActiveFlag, , currentRI.BusinessUnit, currentRI.Linebreak, Request.QueryString("RINumber"))
        '                    selectedFacility = currentRI.SiteID
        '                    selectedBusArea = currentRI.BusinessUnit
        '                    selectedLine = currentRI.Linebreak
        '                End If

        '                Dim clsSearch As clsViewSearch = Session("clsSearch")
        '                If clsSearch IsNot Nothing Then
        '                    Dim dr As Data.DataTableReader = clsSearch.Search
        '                    If dr IsNot Nothing Then
        '                        If dr.HasRows Then
        '                            '_gvIncidentListing.DataSource = dr
        '                            '_gvIncidentListing.DataBind()
        '                            clsSearch.SearchData = Nothing
        '                            dr = Nothing
        '                            'dr.Close()
        '                            'If _gvIncidentListing.Rows.Count > 9 Then
        '                            '    _pnlGrid.Height = "300"
        '                            'End If
        '                            'Me._btnViewSearch.Visible = True
        '                        End If
        '                    End If
        '                End If
        '            Else
        '                selectedFacility = userProfile.DefaultFacility
        '                enterRI = New clsEnterNewRI(userProfile.Username, selectedFacility, userProfile.InActiveFlag, userProfile.DefaultDivision, selectedBusArea, selectedLine)
        '                If Me._functionalLocationTree.Text.Length = 0 Then
        '                    Me._functionalLocationTree.PopulateFunctionalLocation(selectedFacility)
        '                End If
        '                Me._lblCreatedBy.Text = String.Format(Resources.Shared.lblCreatedBy, Resources.Shared.lblNone)
        '                Me._lblCreatedDate.Text = String.Format(Resources.Shared.lblCreationDate, Resources.Shared.lblNone)
        '                Me._lblLastUpdateDate.Text = String.Format(Resources.Shared.lblLastUpdateDate, Resources.Shared.lblNone)
        '                Me._lblUpdatedBy.Text = String.Format(Resources.Shared.lblLastUpdatedBy, Resources.Shared.lblNone)
        '                End

        '                If enterRI IsNot Nothing Then
        '                    If enterRI.CurrentPageMode = clsEnterNewRI.PageMode.NewRI Then
        Master.SetBanner("Enter New Outage Plan")
        '                        Me._btnSubmit.Text = "Submit New Reliability Incident"
        '                    Else
        '                        Master.SetBanner("Update Incident '" & currentRI.RINumber & "'")
        '                        _btnSubmit.Text = "Update Reliability Incident"
        '                        DisplayUpdateMenu()
        '                    End If
        '                    ' Me._btnSubmit.OnClientClick = "UltimateSpellClick('" & _spellIncidentDescription.ClientID.ToString & "');"
        '                    PopulateData()
        '                End If
        '                If currentRI IsNot Nothing Then
        '                    'Populate Screen with current RI values
        '                    GetIncident()
        '                End If

        '            End If

        '            'Me._lblFailureClassHeader.Attributes.Add("onclick", "Javascript:ScrollControlInView('" & _lblAvgFailureClass.ClientID & "');")
    End Sub

    '    Protected Sub _ddlFacility_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlFacility.SelectedIndexChanged
    '        selectedFacility = Me._ddlFacility.SelectedValue
    '        selectedBusArea = String.Empty
    '        selectedLine = String.Empty
    '        Me._ddlBusinessUnit.SelectedIndex = -1
    '        enterRI = New clsEnterNewRI(userProfile.Username, selectedFacility, userProfile.InActiveFlag, userProfile.DefaultDivision, selectedBusArea, selectedLine)
    '        Me._functionalLocationTree.PopulateFunctionalLocation(selectedFacility)
    '        PopulateData()
    '    End Sub
    '    Private Sub GetIncident()
    '        If currentRI IsNot Nothing Then
    '            Me._incidentType.RefreshDisplay()
    '            With currentRI
    '                'State
    '                If .RCFALeader.ToUpper.Trim <> "NONE" Then
    '                    If _ddlAnalysisLead.Items.FindByValue(.RCFALeader) IsNot Nothing Then
    '                        Me._ddlAnalysisLead.SelectedValue = .RCFALeader
    '                    Else
    '                        Me._ddlAnalysisLead.Items.Add(New ListItem(.RCFALeaderName, .RCFALeader))
    '                    End If
    '                End If

    '                Me._lblCreatedBy.Text = String.Format(Resources.Shared.lblCreatedBy, .CreatedBy)
    '                Me._lblCreatedDate.Text = String.Format(Resources.Shared.lblCreationDate, .CreationDate)
    '                Me._lblLastUpdateDate.Text = String.Format(Resources.Shared.lblLastUpdateDate, .LastUpdatedDate)
    '                Me._lblUpdatedBy.Text = String.Format(Resources.Shared.lblLastUpdatedBy, .LastUpdatedBy)

    '                'RCFAAnalysisCompDate?????
    '                'RCFAActionCompDate???
    '                'Me._lblRINumber.Text = .RINumber
    '                'Location
    '                Me._txtAnalysisCompleted.Text = .RCFAAnalysisCompDate
    '                Me._txtCorrectiveActionsCompleted.Text = .RCFAActionCompDate
    '                Me._ddlFacility.SelectedValue = .SiteID
    '                Me._ddlBusinessUnit.SelectedValue = .BusinessUnit
    '                Me._ddlLineBreak.SelectedValue = .Linebreak
    '                'If .FunctionalLocation.Length > 0 Then
    '                'Me._functionalLocationTree.Text = .FunctionalLocation
    '                'Else
    '                Me._functionalLocationTree.SetEquipmentDescription(.FunctionalLocation)
    '                'End If

    '                'Incident                
    '                Me._startDateTime.DateValue = .IncidentStartDate
    '                Me._endDateTime.DateValue = .IncidentEndDate
    '                Me._txtDownTime.Text = .Downtime
    '                Me._txtIncidentTitle.Text = .IncidentTitle
    '                Me._ddlCrew.SelectedValue = .Crew
    '                Me._ddlShift.SelectedValue = .Shift
    '                Me._txtIncidentDescription.Text = .IncidentDescription
    '                Me._txtCost.Text = String.Format("{0:c}", CDec(.Cost))

    '                '.lossOpportunity = 
    '                Me._txtFinancialImpact.Text = String.Format("{0:c}", CDec(.TotalCost))

    '                'Incident Type
    '                '.RTS=                
    '                Me._incidentType.Recordable = .Recordable
    '                Me._incidentType.Chronic = .Chronic
    '                Me._incidentType.Quality = .RCFAQuality
    '                If .RCFA.ToUpper = "YES" Then
    '                    Me._incidentType.RCFA = .RCFALevel
    '                Else
    '                    Me._incidentType.RCFA = "No"
    '                End If
    '                Me._incidentType.CertifiedKill = .CertifiedKill
    '                Me._incidentType.Safety = .RCFASafety

    '                'Incdent Classification

    '                Me._IncidentClassification.TriggerValue = .RCFATriggerDesc
    '                Me._IncidentClassification.TypeValue = .Type
    '                Me._IncidentClassification.CauseValue = .Cause
    '                Me._IncidentClassification.PreventionValue = .Prevention
    '                _IncidentClassification.ProcessValue = .Process
    '                _IncidentClassification.ComponentValue = .Component

    '                'Other
    '                Me._ddlFailedMaterial.SelectedValue = .RCFAFailedLocation
    '                Me._txtConditionsInfluencingFailure.Text = .RCFACondition
    '                Me._txtPeopleToInterview.Text = .RCFAPeople
    '                Me._txtTeamMembers.Text = .RCFATeamMembers
    '            End With
    '        End If
    '    End Sub
    '    Private Sub PopulateData()
    '        If enterRI IsNot Nothing Then
    '            RI.SharedFunctions.BindList(Me._ddlFacility, enterRI.Facility, False, True)
    '            RI.SharedFunctions.BindList(Me._ddlBusinessUnit, enterRI.BusinessUnitArea, False, True)
    '            RI.SharedFunctions.BindList(Me._ddlLineBreak, enterRI.LineBreak, False, True)
    '            RI.SharedFunctions.BindList(Me._ddlCrew, enterRI.Crew, False, True)
    '            RI.SharedFunctions.BindList(Me._ddlShift, enterRI.Shift, False, True)
    '            'RI.SharedFunctions.BindList(Me._ddlTrigger, enterRI.Trigger, False, True)
    '            If selectedFacility.Length = 0 Then
    '                Dim userProfile As RI.CurrentUserProfile = RI.SharedFunctions.GetUserProfile
    '                If userProfile IsNot Nothing Then
    '                    If _ddlFacility.Items.FindByValue(userProfile.DefaultFacility) IsNot Nothing Then
    '                        _ddlFacility.SelectedValue = userProfile.DefaultFacility
    '                    End If
    '                End If
    '            End If

    '            RI.SharedFunctions.BindList(Me._ddlFailedMaterial, enterRI.FailedMaterial, True, True, "Siteid='" & selectedFacility & "'")
    '            'PopulateIncidentSolution()
    '            PopulateAnalysisLeader()
    '            If userProfile.AuthLevel.ToUpper = "YES" Then
    '                'Me._ddlAnalysisLead.Enabled = True
    '            Else
    '                'Me._ddlAnalysisLead.Enabled = False
    '                Me._ddlAnalysisLead.Items.Clear()
    '            End If
    '            With _IncidentClassification
    '                enterRI.PopulateCauses()
    '                .TriggerData = enterRI.Trigger
    '                .TypeData = enterRI.Type
    '                .ComponentData = enterRI.Component
    '                .PreventionData = enterRI.Prevention
    '                .CauseData = enterRI.Cause
    '                .ProcessData = enterRI.EquipmentProcess
    '                .DataBind()
    '            End With
    '        End If
    '    End Sub
    '    Private Sub PopulateIncidentSolution()
    '        'Dim selectedCause As String = String.Empty
    '        'Dim selectedProcess As String = String.Empty
    '        'Dim CauseFilter As String = String.Empty
    '        'Dim ProcessFilter As String = String.Empty

    '        'Try
    '        '    selectedCause = Request(Me._ddlCause.UniqueID.ToString)
    '        '    If selectedCause Is Nothing Then selectedCause = String.Empty
    '        '    If selectedCause.Length > 0 Then
    '        '        CauseFilter = "Cause='" & selectedCause & "'"
    '        '    Else
    '        '        CauseFilter = ""
    '        '    End If

    '        '    selectedProcess = Request(Me._ddlEquipmentProcess.UniqueID.ToString)
    '        '    If selectedProcess Is Nothing Then selectedProcess = String.Empty
    '        '    If selectedProcess.Length > 0 Then
    '        '        ProcessFilter = "Process='" & selectedProcess & "'"
    '        '    Else
    '        '        ProcessFilter = ""
    '        '    End If

    '        '    If ProcessFilter.Length > 0 And CauseFilter.Length > 0 Then
    '        '        ProcessFilter = ProcessFilter & " and " & CauseFilter
    '        '    End If

    '        '    If enterRI IsNot Nothing Then
    '        '        enterRI.PopulateCauses(selectedProcess)
    '        '        RI.SharedFunctions.BindList(Me._ddlCause, enterRI.Type, True, True)
    '        '        RI.SharedFunctions.BindList(Me._ddlPrevention, enterRI.Prevention, False, True)
    '        '        RI.SharedFunctions.BindList(Me._ddlReason, enterRI.Cause, True, True, CauseFilter)
    '        '        RI.SharedFunctions.BindList(Me._ddlEquipmentProcess, enterRI.EquipmentProcess, True, True, CauseFilter)
    '        '        RI.SharedFunctions.BindList(Me._ddlComponent, enterRI.Component, True, True, ProcessFilter)
    '        '    End If

    '        '    If _ddlCause.Items.FindByValue(selectedCause) IsNot Nothing Then
    '        '        _ddlCause.Items.FindByValue(selectedCause).Selected = True
    '        '    Else
    '        '        _ddlCause.SelectedIndex = 0
    '        '    End If

    '        'Catch ex As Exception
    '        '    Throw
    '        'Finally

    '        'End Try
    '    End Sub


    '    Private Sub PopulateAnalysisLeader()

    '        Try
    '            If enterRI IsNot Nothing Then
    '                'enterRI.PopulateAnalysisLeader(Me._ddlFacility.SelectedValue, Me._ddlBusinessUnit.SelectedValue)
    '                RI.SharedFunctions.BindList(Me._ddlAnalysisLead, enterRI.AnalysisLeader, False, True)
    '            End If
    '        Catch ex As Exception
    '            Throw
    '        Finally

    '        End Try
    '    End Sub


    '    Protected Sub _startDateTime_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles _startDateTime.Load
    '        If Me._startDateTime.DateValue.Length = 0 Then
    '            Me._startDateTime.DateValue = Now().ToShortDateString
    '        End If
    '    End Sub

    '    Protected Sub _endDateTime_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles _endDateTime.Load
    '        If Me._endDateTime.DateValue.Length = 0 Then
    '            Me._endDateTime.DateValue = Now().ToShortDateString
    '        End If
    '    End Sub

    '    Protected Sub _ddlLineBreak_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlLineBreak.SelectedIndexChanged
    '        selectedFacility = _ddlFacility.SelectedValue
    '        selectedBusArea = _ddlBusinessUnit.SelectedValue
    '        selectedLine = _ddlLineBreak.SelectedValue
    '        enterRI = New clsEnterNewRI(userProfile.Username, selectedFacility, userProfile.InActiveFlag, userProfile.DefaultDivision, selectedBusArea, selectedLine)
    '        Me._functionalLocationTree.PopulateFunctionalLocation(selectedFacility, selectedBusArea, selectedLine)
    '        PopulateData()
    '    End Sub

    '    Private Sub GetControlState()
    '        If Request.Form(Me._ddlFacility.UniqueID) IsNot Nothing Then
    '            selectedFacility = Request.Form(Me._ddlFacility.UniqueID)
    '            Me._functionalLocationTree.PopulateFunctionalLocation(selectedFacility)
    '        End If
    '        If Request.Form(Me._ddlBusinessUnit.UniqueID) IsNot Nothing Then
    '            selectedBusArea = Request.Form(Me._ddlBusinessUnit.UniqueID)
    '        End If
    '        If Request.Form(Me._ddlLineBreak.UniqueID) IsNot Nothing Then
    '            selectedLine = Request.Form(Me._ddlLineBreak.UniqueID)
    '            Me._functionalLocationTree.PopulateFunctionalLocation(selectedFacility, selectedBusArea, selectedLine)
    '        End If
    '    End Sub
    '    'Private Sub HandlePostBack()

    '    '    If RI.SharedFunctions.CausedPostBack(Me._ddlBusinessUnit.ID.ToString) Or _ddlBusinessUnit.SelectedValue.Length > 0 Then
    '    '        _ddlBusinessUnit_SelectedIndexChanged(Nothing, Nothing)
    '    '    End If
    '    '    If RI.SharedFunctions.CausedPostBack(Me._ddlFacility.ID.ToString) Then

    '    '    End If
    '    '    If RI.SharedFunctions.CausedPostBack(Me._ddlLineBreak.ID.ToString) Then
    '    '        'Me._functionalLocationTree.PopulateFunctionalLocation(selectedFacility, Me._ddlBusinessUnit.SelectedValue, Me._ddlLineBreak.SelectedValue)
    '    '    End If
    '    'End Sub

    '    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
    '        If enterRI IsNot Nothing Then
    '            enterRI = Nothing
    '        End If
    '    End Sub

    '    Protected Sub _ddlBusinessUnit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlBusinessUnit.SelectedIndexChanged
    '        selectedFacility = _ddlFacility.SelectedValue
    '        selectedBusArea = _ddlBusinessUnit.SelectedValue
    '        selectedLine = String.Empty
    '        enterRI = New clsEnterNewRI(userProfile.Username, selectedFacility, userProfile.InActiveFlag, userProfile.DefaultDivision, selectedBusArea, selectedLine)
    '        PopulateData()

    '    End Sub

    '    Protected Sub _btnCalculateDowntime_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnCalculateDowntime.Click
    '        Dim diff As TimeSpan
    '        If IsDate(Me._startDateTime.DateTime) And IsDate(Me._endDateTime.DateTime) Then
    '            If Me._startDateTime.DateTime <= Me._endDateTime.DateTime Then
    '                diff = Me._endDateTime.DateTime.Subtract(Me._startDateTime.DateTime)
    '                Me._txtDownTime.Text = Math.Round(diff.TotalMinutes / 60, 2)
    '            Else
    '                MsgBox("Incident End Date should be greater than Incident Start Date", MsgBoxStyle.Information, "RI")
    '                Me._txtDownTime.Text = String.Empty
    '            End If
    '        End If

    '    End Sub

    '    Protected Sub _btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnSubmit.Click
    '        If Page.IsValid Then
    '            currentRI = New clsCurrentRI()
    '            With currentRI
    '                'State
    '                .RCFALeader = Me._ddlAnalysisLead.SelectedValue
    '                'RCFAAnalysisCompDate?????
    '                'RCFAActionCompDate???

    '                'Location
    '                .SiteID = Me._ddlFacility.SelectedValue
    '                .BusinessUnit = Me._ddlBusinessUnit.SelectedValue
    '                .Area = Me._ddlLineBreak.SelectedValue
    '                .FunctionalLocation = Me._functionalLocationTree.Text

    '                'Incident
    '                .IncidentStartDate = Me._startDateTime.DateTime.ToShortDateString
    '                .IncidentEndDate = Me._endDateTime.DateTime.ToShortDateString
    '                .Downtime = Me._txtDownTime.Text
    '                .IncidentTitle = Me._txtIncidentTitle.Text
    '                .Crew = Me._ddlCrew.SelectedValue
    '                .Shift = Me._ddlShift.SelectedValue
    '                .IncidentDescription = Me._txtIncidentDescription.Text
    '                .Cost = Me._txtCost.Text
    '                '.lossOpportunity = 
    '                .TotalCost = Me._txtFinancialImpact.Text

    '                'Incident Type
    '                '.RTS=                
    '                .Recordable = Me._incidentType.Recordable
    '                .Chronic = Me._incidentType.Chronic
    '                .RCFAQuality = Me._incidentType.Quality
    '                .RCFA = Me._incidentType.RCFA
    '                .CertifiedKill = Me._incidentType.CertifiedKill
    '                .RCFASafety = Me._incidentType.Safety

    '                'Incdent Classification
    '                .RCFATriggerDesc = Me._IncidentClassification.TriggerValue
    '                .Type = _IncidentClassification.TypeValue
    '                .Cause = _IncidentClassification.CauseValue
    '                .Prevention = _IncidentClassification.ProcessValue
    '                .Process = _IncidentClassification.ProcessValue
    '                .Component = _IncidentClassification.ComponentValue

    '                'Other
    '                .RCFAFailedLocation = Me._ddlFailedMaterial.SelectedValue
    '                .RCFACondition = Me._txtConditionsInfluencingFailure.Text
    '                .RCFAPeople = Me._txtPeopleToInterview.Text
    '                .RCFATeamMembers = Me._txtTeamMembers.Text
    '            End With
    '        End If
    '    End Sub

    '    Protected Sub _IncidentClassification_IncidentClassificationChanged() Handles _IncidentClassification.IncidentClassificationChanged
    '        selectedFacility = Me._ddlFacility.SelectedValue
    '        selectedBusArea = Me._ddlBusinessUnit.SelectedValue
    '        selectedLine = Me._ddlLineBreak.SelectedValue

    '        enterRI = New clsEnterNewRI(userProfile.Username, selectedFacility, userProfile.InActiveFlag, userProfile.DefaultDivision, selectedBusArea, selectedLine)
    '        Me.PopulateData()
    '    End Sub
End Class
