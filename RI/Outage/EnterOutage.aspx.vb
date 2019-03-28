Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Devart.Data.Oracle
Imports System.Collections



Partial Class Outage_EnterOutage
    Inherits RIBasePage

    Dim enterOutage As clsEnterOutage
    Dim currentOutage As clsCurrentOutage
    Dim selectedFacility As String = String.Empty
    Dim selectedBusArea As String = String.Empty
    Dim selectedLine As String = String.Empty
    Dim selectedOutageCoord As String = String.Empty
    Dim userProfile As RI.CurrentUserProfile = Nothing

    Dim templateUsed As Boolean = False

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.ShowOutageMenu()
    End Sub

    Private Sub DisplayUpdateMenu()
        Dim confirmMessage As String = "localizedText.ConfirmRedirect" 'Master.RIRESOURCES.GetResourceValue("YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES")
        Me._btnAttachments.OnClientClick = "Javascript:viewPopUp('FileUpload.aspx?OutageNumber=" & currentOutage.OutageNumber & "'," & confirmMessage & ",'fu');return false"
        Me._pnlUpdateButtons.Visible = True
        If currentOutage.MTTTaskHeaderID = "" Then
            Me._btnTasks.OnClientClick = "Javascript:CreateTaskHeader('" & currentOutage.Title & "','" & currentOutage.OutageNumber & "','" & currentOutage.StartDate & "','" & currentOutage.EndDate & "','" & currentOutage.SiteID & "','OUTAGE','OTHER','" & currentOutage.UserName & "','" & currentOutage.InsertDate & "');return false"
        Else
            Me._btnTasks.OnClientClick = "Javascript:OnMTTComplete('" & currentOutage.MTTTaskHeaderID & "');return false"
        End If
    End Sub
   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            userProfile = RI.SharedFunctions.GetUserProfile
            RI.SharedFunctions.DisablePageCache(Response)
            Dim confirmMessage As String = "localizedText.ConfirmRedirect" 'Master.RIRESOURCES.GetResourceValue("YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES")

            Dim sc As ScriptManager
            sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
            If sc IsNot Nothing Then
                Dim calService As New ServiceReference
                calService.InlineScript = False
                calService.Path = "~/CalculateDowntime.asmx"
                sc.Services.Add(calService)
            End If

            'will not need following if block when we move webservice to mtt project
            If sc IsNot Nothing Then
                Dim MTTService As New ServiceReference
                MTTService.InlineScript = False
                MTTService.Path = "~/MOC/TaskTracker.asmx"
                sc.Services.Add(MTTService)
            End If

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

            'If sc IsNot Nothing Then
            '    Dim MTTService As New ServiceReference
            '    MTTService.InlineScript = False
            '    MTTService.Path = "~/Outage/OutageWS.asmx"
            '    sc.Services.Add(MTTService)
            'End If

            ScriptManager.RegisterStartupScript(Me.Page, Page.GetType, "GetGlobalJSVarOutage", GetGlobalJSVarOutage, True)


            If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "EnterOutage") Then
                Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "EnterOutage", Page.ResolveClientUrl("~/outage/EnterOutage.js"))
            End If
            Master.UnloadEvents = False

            'Make sure hidden field for checking if conflict record updated is hidden
            '_lblConflictChanged.Style.Add("display", "none")

            'Me._rblSDCategory.Attributes.Add("onclick", "checkOutageType('" & Me._rblSDCategory.ClientID & "','" & Me._txtTitle.ClientID & "','" & Me._ddlOutageCoord.ClientID & "','" & Me._ddlTemplateTasks.ClientID & "');")
            'Me._txtTitle.Attributes.Add("onchange", "checkOutageType('" & Me._rblSDCategory.ClientID & "','" & Me._txtTitle.ClientID & "','" & Me._ddlOutageCoord.ClientID & "','" & Me._ddlTemplateTasks.ClientID & "');")
            'Me._ddlOutageCoord.Attributes.Add("onchange", "checkOutageType('" & Me._rblSDCategory.ClientID & "','" & Me._txtTitle.ClientID & "','" & Me._ddlOutageCoord.ClientID & "','" & Me._ddlTemplateTasks.ClientID & "');")
            Me._ddlTemplateTasks.Attributes.Add("onchange", "EnableButtons('" & Me._btnViewTemplateTasks.ClientID & "','" & Me._ddlTemplateTasks.ClientID & "');")

            If Request.QueryString("OutageNumber") IsNot Nothing Then
                currentOutage = New clsCurrentOutage(Request.QueryString("OutageNumber"))
            End If
            Me.ButtonTemplateTasks.Style.Add("Display", "none")


            If Not Page.IsPostBack Then
                '10/2017 ALA - Only the assigned outage coordinator can change planned dates for annual outages.  
                If Request.QueryString("OutageNumber") IsNot Nothing Then
                    currentOutage = New clsCurrentOutage(Request.QueryString("OutageNumber"))
                    If currentOutage.SiteID Is Nothing Then currentOutage = Nothing
                    If currentOutage IsNot Nothing Then
                        enterOutage = New clsEnterOutage(userProfile.Username, currentOutage.SiteID, userProfile.InActiveFlag, , currentOutage.BusinessUnit, currentOutage.Line, Request.QueryString("OutageNumber"))
                        selectedFacility = currentOutage.SiteID
                        selectedBusArea = currentOutage.BusinessUnit
                        selectedLine = currentOutage.Line
                        selectedOutageCoord = currentOutage.OutageCoordinator
                        If UCase(selectedOutageCoord) <> UCase(userProfile.Username) And
                                currentOutage.AnnualFlag = "Y" Then
                            Me._PlannedCalendar.enabled = "False"
                        End If
                        'If currentOutage.MTTTemplateID <> "" Or Me._ddlTemplateTasks.SelectedValue = "" Then
                        '    Me._btnViewTemplateTasks.Style.Add("Display", "none")
                        '    'Else
                        '    '   Me._btnViewTemplateTasks.Style.Add("Display", "")
                        'End If
                        If Me._ddlTemplateTasks.SelectedValue = "" And currentOutage.MTTTemplateID = "" Then
                            Me._btnViewTemplateTasks.Style.Add("Display", "none")
                        End If
                        If currentOutage.TaskCount > 0 Then
                            Me._btnScorecard.Visible = "true"
                            Dim urlPath As String
                            If Request.UserHostAddress = "127.0.0.1" Or Request.UserHostAddress = "http://s29edev13/riajax" Then
                                urlPath = "http://ridev.ipaper.com/CEReporting/"
                            Else
                                urlPath = "../../CEReporting/"
                            End If
                            Dim scorecardURL As String = String.Format(urlPath & "CrystalReportDisplay.aspx?Report=OutageScorecard&OutageNumber={0}&Localename=" & Master.RIRESOURCES.CurrentLocale, currentOutage.OutageNumber)
                            Me._btnScorecard.OnClientClick = Master.GetPopupWindowJS(scorecardURL, "OutageScorecard", 600, 300, True, True, True) & ";return false;"
                        End If
                    Else
                        Response.Redirect("~/outage/enteroutage.aspx")
                    End If

                    Me._btnTasks.Text = Master.RIRESOURCES.GetResourceValue("Task Items", True, "Shared") & " (" & CStr(currentOutage.TaskCount) & ")"
                    Me._btnMajorScope.Text = Master.RIRESOURCES.GetResourceValue("MajorScope", True, "Shared") & " (" & CStr(currentOutage.ScopeCount) & ")"
                    Me._btnMajorScope.OnClientClick = "Javascript:viewPopUp('OutageScope.aspx?OutageNumber=" & currentOutage.OutageNumber & "'," & confirmMessage & ",'aw');return true"
                    Me._btnCritique.Text = Master.RIRESOURCES.GetResourceValue("Critique", True, "Shared")
                    Me._btnCritique.OnClientClick = "Javascript:viewPopUp('Critique.aspx?OutageNumber=" & currentOutage.OutageNumber & "'," & confirmMessage & ",'aw');return true"
                    Me._btnHistory.RINumber = currentOutage.OutageNumber

                Else
                    If _ddlFacility.Items.FindByValue(userProfile.DefaultFacility) IsNot Nothing Then
                        _ddlFacility.SelectedValue = userProfile.DefaultFacility
                    End If
                    selectedFacility = userProfile.DefaultFacility
                    enterOutage = New clsEnterOutage(userProfile.Username, selectedFacility, userProfile.InActiveFlag, userProfile.DefaultDivision, selectedBusArea, selectedLine)

                    Me._lblCreatedBy.Text = String.Format(Master.RIRESOURCES.GetResourceValue("CreatedBy", True, "Shared"), Master.RIRESOURCES.GetResourceValue("None", True, "Shared"))
                    Me._lblCreatedDate.Text = String.Format(Master.RIRESOURCES.GetResourceValue("CreationDate", True, "Shared"), Master.RIRESOURCES.GetResourceValue("None", True, "Shared"))
                    Me._lblLastUpdateDate.Text = String.Format(Master.RIRESOURCES.GetResourceValue("LastUpdateDate", True, "Shared"), Master.RIRESOURCES.GetResourceValue("None", True, "Shared"))
                    Me._lblUpdatedBy.Text = String.Format(Master.RIRESOURCES.GetResourceValue("LastUpdatedBy", True, "Shared"), Master.RIRESOURCES.GetResourceValue("None", True, "Shared"))

                    Me._pnlContractor.Visible = False
                    Me._pnlResources.Visible = False
                    Me._tblAssessment.Visible = False

                    'If Not Page.IsPostBack Then
                    Dim englishDate As String = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Now, "EN-US", "d")
                    Me._startEnd.StartDate = englishDate
                    Me._startEnd.EndDate = englishDate
                    'We do not want the Actual Start End Dates to be populated when an Outage is initially entered.
                    Me._ActualStartEnd.ClearCalendar()

                    'End If
                End If

            End If

            If enterOutage IsNot Nothing Then
                If enterOutage.CurrentPageMode = clsEnterOutage.PageMode.NewOutage Then
                    Master.SetBanner(Master.RIRESOURCES.GetResourceValue("EnterNewOutage", True, "OUTAGE"))
                    _btnSubmit.Text = Master.RIRESOURCES.GetResourceValue("SaveNewOutage", True, "OUTAGE")
                    Me._btnAttachments.Visible = False
                    Me._btnTasks.Visible = False
                    Me._btnResources.Visible = False
                    Me._btnShowContractorSLB.Visible = False
                    Me._btnMajorScope.Visible = False
                    Me._btnCritique.Visible = False
                    Me._lblTemplates.Style.Add("display", "none")
                    Me._ddlTemplateTasks.Style.Add("display", "none")
                    Me._btnViewTemplateTasks.Style.Add("Display", "none")
                    Me._btnHistory.Visible = False
                Else
                    Master.SetBanner(Master.RIRESOURCES.GetResourceValue("UpdateOutage", True, "OUTAGE") & " " & currentOutage.OutageNumber)
                    _btnSubmit.Text = Master.RIRESOURCES.GetResourceValue("SaveOutage", True, "OUTAGE")
                    DisplayUpdateMenu()
                    'Me._btnTasks.Visible = True
                End If
                'Me._btnHistory.RINumber = currentOutage.OutageNumber
                PopulateData()
               
            End If

            'End If
            '' 8/16/2011 - We always want to show business unit area regardless of category selection.
            'Dim sdCat As String
            'sdCat = Me._rblSDCategory.SelectedValue
            'If sdCat = "Field Day" Or sdCat = "Partial Mill" Then
            'Me._tcBusinessUnitArea.Style.Add("Display", "")
            'Me._tcBusinessUnit.Style.Add("Display", "")
            'Else
            Me._tcBusinessUnitArea.Style.Add("Display", "")
            Me._tcBusinessUnit.Style.Add("Display", "")
            'End If

            _btnSubmit.OnClientClick = "ConfirmBeforeLeave=false;"

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub _ddlFacility_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlFacility.SelectedIndexChanged
        selectedFacility = Me._ddlFacility.SelectedValue
        selectedBusArea = String.Empty
        selectedLine = String.Empty
        enterOutage = New clsEnterOutage(userProfile.Username, selectedFacility, userProfile.InActiveFlag, userProfile.DefaultDivision, selectedBusArea, selectedLine)
        PopulateData()
        'We do not want the Actual Start End Dates to be populated when facility changes
        Me._ActualStartEnd.ClearCalendar()
    End Sub

    Private Sub GetOutage()
        If currentOutage IsNot Nothing Then
            'Added to reinstate the contractor and resource data if email has already processed datatable
            If Not currentOutage.ContractorDT Is Nothing Then
                If currentOutage.ContractorDT.IsClosed = True Then
                    currentOutage = New clsCurrentOutage(currentOutage.OutageNumber)
                End If
            End If
            If Not currentOutage.ResourceDT Is Nothing Then
                If currentOutage.ResourceDT.IsClosed = True Then
                    currentOutage = New clsCurrentOutage(currentOutage.OutageNumber)
                End If
            End If

            With currentOutage
                Me._rblSDCategory.SelectedValue = .SDCategory
                If .OutageCoordinator.ToUpper.Trim <> "NONE" Then
                    If Me._ddlOutageCoord.Items.FindByValue(.OutageCoordinator) IsNot Nothing Then
                        Me._ddlOutageCoord.SelectedValue = .OutageCoordinator
                    Else
                        Me._ddlOutageCoord.Items.Add(New ListItem(.OutageCoordinator, .OutageCoordinator))
                    End If
                End If

                If .MRLead.ToUpper.Trim <> "NONE" Then
                    If Me._ddlMRLead.Items.FindByValue(.MRLead) IsNot Nothing Then
                        Me._ddlMRLead.SelectedValue = .MRLead
                    Else
                        Me._ddlMRLead.Items.Add(New ListItem(.MRLead, .MRLead))
                    End If
                End If

                'Change names of labels
                Me._lblCreatedBy.Text = String.Format(Master.RIRESOURCES.GetResourceValue("CreatedBy", True, "Shared"), .InsertUsername)
                Me._lblCreatedDate.Text = String.Format(Master.RIRESOURCES.GetResourceValue("CreationDate", True, "Shared"), .InsertDate)
                Me._lblLastUpdateDate.Text = String.Format(Master.RIRESOURCES.GetResourceValue("LastUpdateDate", True, "Shared"), .LastUpdatedDate)
                Me._lblUpdatedBy.Text = String.Format(Master.RIRESOURCES.GetResourceValue("LastUpdatedBy", True, "Shared"), .LastUpdatedBy)
                Me._lblTemplateTaskMsg.Text = .TemplateMsg

                'Location
                Me._ddlFacility.SelectedValue = .SiteID

                'Business Unit
                Dim i As Integer
                For i = 0 To (Me._lbBusinessUnit.Items.Count - 1)

                    If InStr(.BusinessUnit, _lbBusinessUnit.Items(i).Value) Then
                        _lbBusinessUnit.Items(i).Selected = True
                    End If
                Next

                'Outage        
                Me._startEnd.StartDate = .StartDate
                Me._startEnd.EndDate = .EndDate
                If .ProposedEndDate <> "" Then
                    Me._trProposed.Visible = True
                    Me._ProposedStartEnd.Enabled = False

                Else
                    Me._trProposed.Visible = False
                End If
                _ProposedStartEnd.StartDate = .ProposedStartDate
                _ProposedStartEnd.EndDate = .ProposedEndDate
                Me._hlMOC.NavigateUrl = "~/MOC/EnterMOC.aspx?MOCNumber=" & .MocNumber
                If .ActualStartDate = "" Then
                    Me._ActualStartEnd.ClearCalendar()
                Else
                    Me._ActualStartEnd.StartDate = .ActualStartDate
                    Me._ActualStartEnd.EndDate = .ActualEndDate
                End If
                Me._txtDownTime.Text = .Downtime
                Me._txtActualDowntime.Text = .ActualDowntime
                Me._txtPlannedCost.Text = .Cost
                Me._txtActualCost.Text = .ActualCost
                Me._txtPlannedCapital.Text = .PlannedCapitalCost
                Me._txtActualCapital.Text = .ActualCapitalCost
                Me._txtTitle.Text = .Title
                Me._txtDescription.Text = .Description
                Me._txtComments.Text = .Comments
                Me._tbTGComments.Text = .TGComments

                'Show any contractors for this Outage
                'Added check for null contractor CAC 
                If .ContractorDT Is Nothing Then
                    Me._pnlContractor.Visible = False
                Else
                    If .ContractorDT.HasRows = True Then
                        Me._gvContractor.DataSource = .ContractorDT
                        Me._gvContractor.DataBind()
                    Else
                        Me._pnlContractor.Visible = False
                        'Me._lblContractors.Visible = False
                    End If
                End If


                'Show any resources for this Outage
                'Added check for null resource CAC 
                If .ResourceDT Is Nothing Then
                    Me._pnlResources.Visible = False
                Else
                    If .ResourceDT.HasRows = True Then
                        Me._gvResources.DataSource = .ResourceDT
                        Me._gvResources.DataBind()
                    Else
                        Me._pnlResources.Visible = False
                        'Me._lblResources.Visible = False
                    End If
                End If

                'Disable the template selection if use has already selected a template.
                If .MTTTemplateID <> Nothing Then
                    Me._ddlTemplateTasks.SelectedValue = .MTTTemplateID
                    Me._ddlTemplateTasks.Enabled = False
                    'Me._btnViewTemplateTasks.Enabled = False
                    templateUsed = True
                End If

                If .AnnualFlag = "Y" Then
                    Me._cbAnnualOutage.Checked = "True"
                    'Me._btnCritique.Visible = "True"
                    'Else
                    ' Me._btnCritique.Visible = "False"
                End If

                Me._AssessDate.DateValue = .AssessmentDate
                Me._tbFEPAScore.Text = .FEPAScore
                Me._tbTGMCMFScore.Text = .TGMCMFScore
                Me._tbOverallScore.Text = .OverallScore
                Me._tbCommIssuesCnt.Text = .CommercialIssuesCnt

                Me._tblAssessment.Visible = True

                'Populate contractor list - is hidden
                PopulateContractor(.OutageNumber)
                PopulateResources(.OutageNumber)

                'RI.SharedFunctions.BindList(Me._ddlHiddenRoles, currentOutage.RolesDT, False, False)

            End With

        End If
    End Sub
    Private Sub PopulateData()
        If enterOutage IsNot Nothing Then
            RI.SharedFunctions.BindList(Me._ddlFacility, enterOutage.Facility, False, True)
            RI.SharedFunctions.BindList(Me._lbBusinessUnit, enterOutage.BusinessUnitArea, False, False, , True)
            RI.SharedFunctions.BindList(Me._ddlMRLead, enterOutage.MRLead, False, True)
            RI.SharedFunctions.BindList(Me._ddlOutageCoord, enterOutage.OutageCoordinator, False, True)
            RI.SharedFunctions.BindList(Me._ddlTemplateTasks, enterOutage.OutageTemplate, False, False)
      
            Me._ddlTemplateTasks.Items.Insert(0, "")
            Me._ddlTemplateTasks.SelectedValue = ""

            'Set facility to what user is set up in refemployee
            If _ddlFacility.SelectedValue.Length = 0 Then
                Dim userProfile As RI.CurrentUserProfile = RI.SharedFunctions.GetUserProfile
                If userProfile IsNot Nothing Then
                    If _ddlFacility.Items.FindByValue(userProfile.DefaultFacility) IsNot Nothing Then
                        _ddlFacility.SelectedValue = userProfile.DefaultFacility
                    End If
                End If
            End If

            If enterOutage.IncidentSecurity.DeleteOutages Then
                Me._btnDelete.Visible = True
            Else
                Me._btnDelete.Visible = False
            End If

            Me._btnSubmit.Visible = True

        End If
    End Sub

    Private Sub GetControlState()
        If Request.Form(Me._ddlFacility.UniqueID) IsNot Nothing Then
            selectedFacility = Request.Form(Me._ddlFacility.UniqueID)
        End If
        If Request.Form(Me._lbBusinessUnit.UniqueID) IsNot Nothing Then
            selectedBusArea = Request.Form(Me._lbBusinessUnit.UniqueID)
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If currentOutage IsNot Nothing Then
            GetOutage()
            'Me._btnHistory.RINumber = currentOutage.OutageNumber
            'Me._btnHistory.RefreshHistory()
        End If
    End Sub

    Protected Sub _btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnSubmit.Click
        Dim msg As New StringBuilder
        '     If _ddlTemplateTasks.Enabled = "True" And 
        If templateUsed = False And _ddlTemplateTasks.Enabled = "True" And _ddlTemplateTasks.SelectedValue <> "" Then
            msg.Append(Master.RIRESOURCES.GetResourceValue("You have selected a template.  Any tasks associated with this template will be created automatically.  Click OK to save this Outage record and create the tasks."))
        End If
        If msg.ToString.Length > 0 Then
            msg.Append("</ul>")
            _messageBox.Title = "Template Tasks"
            _messageBox.Message = Master.RIRESOURCES.GetResourceValue("WARNING:") & "<br><ul>" & msg.ToString & "<br>"
            ' & Master.RIRESOURCES.GetResourceValue("Do you want to continue?")
            _messageBox.Width = 400
            _messageBox.CancelScript = "disableButton(); return false;"
            '_MessageBox.OKScript = "javascript:window.location='" & Page.ResolveClientUrl("~/outage/Enteroutage.aspx?OutageNumber=" & Request.QueryString("OutageNumber")) & "'"
            _messageBox.ShowMessage()
        Else
            SaveOutage(True)
        End If
    End Sub

    Protected Sub _btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnDelete.Click
        If Request.QueryString("OutageNumber") IsNot Nothing Then
            Dim outageNumber As String = Request.QueryString("OutageNumber")
            If outageNumber > 0 Then
                clsCurrentOutage.DeleteCurrentOutage(outageNumber, userProfile.Username)
                'MsgBox(outageNumber & " has been deleted", MsgBoxStyle.Information)
                Response.Redirect("~/outage/outage.aspx", True)
            End If
        End If
    End Sub

    Protected Sub SaveOutage(Optional ByVal refreshPage As Boolean = True)
        If Page.IsValid Then
            If Request.QueryString("OutageNumber") IsNot Nothing Then
                If currentOutage.MTTTemplateID <> Nothing Then
                    templateUsed = True
                End If
            End If

            currentOutage = New clsCurrentOutage()
            Dim EmailConflict As Boolean = False
            If Me._lblConflictChanged.Value = "Y" Then 'And currentOutage.OutageCoordinator <> "" Then
                EmailConflict = True
            End If

            With currentOutage
                'Coordinator
                .OutageCoordinator = Me._ddlOutageCoord.SelectedValue
                If .OutageCoordinator.Length = 0 Then .OutageCoordinator = "NONE"
                If Me._ddlOutageCoord.SelectedItem IsNot Nothing Then
                    .OutageCoordinator = Me._ddlOutageCoord.SelectedItem.Text
                Else
                    .OutageCoordinator = "NONE"
                End If

                'MR Lead
                .MRLead = Me._ddlMRLead.SelectedValue
                If .MRLead.Length = 0 Then .MRLead = "NONE"
                If Me._ddlMRLead.SelectedItem IsNot Nothing Then
                    .MRLead = Me._ddlMRLead.SelectedItem.Value
                Else
                    .MRLead = "NONE"
                End If
                'Location
                .SiteID = Me._ddlFacility.SelectedValue

                'Shutdown Category
                'Dim sdCat As String
                'sdCat = Me._rblSDCategory.SelectedValue
                'If sdCat = "Field Day" Or sdCat = "Partial Mill" Then

                'Business Unit/Area
                Dim i As Integer
                Dim recordCnt As Integer = 0
                For i = 0 To Me._lbBusinessUnit.Items.Count - 1
                    If Me._lbBusinessUnit.Items(i).Selected = True Then
                        recordCnt = recordCnt + 1
                        Dim busArea As String() = Split(Me._lbBusinessUnit.Items(i).Value.ToString, "-")
                        If recordCnt = 1 Then
                            If busArea.Length = 3 Then
                                .BusinessUnit = busArea(0).Trim & "~"
                                .Area = busArea(1).Trim & "~"
                                .Line = busArea(2).Trim & "~"
                            End If
                        Else
                            If busArea.Length = 3 Then
                                .BusinessUnit = .BusinessUnit & busArea(0).Trim & "~"
                                .Area = .Area & busArea(1).Trim & "~"
                                .Line = .Line & busArea(2).Trim & "~"
                            End If

                        End If
                    End If
                Next

                'Outage Planned Dates
                .StartDate = CStr(Me._startEnd.StartDate)  '.ToShortDateString
                .EndDate = CStr(Me._startEnd.EndDate) '.ToShortDateString
                If Convert.ToDateTime(.EndDate, New System.Globalization.CultureInfo("EN-US")) < Convert.ToDateTime(.StartDate, New System.Globalization.CultureInfo("EN-US")) Then
                    'If IsDate(Me._startEnd.StartDate) And IsDate(Me._startEnd.EndDate) Then
                    'If Me._startEnd.StartDate > Me._startEnd.EndDate Then
                    '.EndDate = Me._startEnd.StartDate
                    .EndDate = .StartDate
                    'Dim startDT As DateTime = Me._startEnd.StartDate
                    'End If
                End If

                'Outage Actual Dates
                'Checking values - if they are equal today midnight then user control is defaulting and
                ' start and end dates should be blank.
                If Convert.ToDateTime(Me._ActualStartEnd.StartDate, New System.Globalization.CultureInfo("EN-US")) = Today() Then
                    .ActualStartDate = ""
                    .ActualEndDate = ""
                Else

                    .ActualStartDate = CStr(Me._ActualStartEnd.StartDate)  '.ToShortDateString
                    .ActualEndDate = CStr(Me._ActualStartEnd.EndDate) '.ToShortDateString
                    If Convert.ToDateTime(.ActualEndDate, New System.Globalization.CultureInfo("EN-US")) < Convert.ToDateTime(.ActualStartDate, New System.Globalization.CultureInfo("EN-US")) Then
                        .ActualEndDate = .ActualStartDate
                    End If
                End If

                'Downtime
                If IsNumeric(Me._txtDownTime.Text) Then
                    .Downtime = RI.SharedFunctions.DataClean(Me._txtDownTime.Text, "0")
                Else
                    Me._txtDownTime.Text = 0
                    .Downtime = 0
                End If

                If .Downtime.Length = 0 Or .Downtime = "0" Or CInt(.Downtime) = 0 Then
                    'Calculate the downtime based on start and end
                    Dim cd As New CalculateDowntime
                    .Downtime = cd.Calculate(_startEnd.StartDate, Me._startEnd.EndDate)
                End If
                .Downtime = IP.Bids.Localization.Numbers.GetLocalizedNumber(.Downtime, "EN-US")

                'Actual Downtime
                If IsNumeric(Me._txtActualDowntime.Text) Then
                    .ActualDowntime = RI.SharedFunctions.DataClean(Me._txtActualDowntime.Text, "0")
                Else
                    Me._txtActualDowntime.Text = 0
                    .ActualDowntime = 0
                End If

                If .ActualDowntime.Length = 0 Or .ActualDowntime = "0" Or CInt(.ActualDowntime) = 0 Then
                    'Calculate the downtime based on start and end
                    Dim cd As New CalculateDowntime
                    .ActualDowntime = cd.Calculate(_ActualStartEnd.StartDate, Me._ActualStartEnd.EndDate)
                End If
                .ActualDowntime = IP.Bids.Localization.Numbers.GetLocalizedNumber(.ActualDowntime, "EN-US")

                .Cost = RI.SharedFunctions.DataClean(Me._txtPlannedCost.Text, "0")
                .ActualCost = RI.SharedFunctions.DataClean(Me._txtActualCost.Text, "0")
                .PlannedCapitalCost = RI.SharedFunctions.DataClean(Me._txtPlannedCapital.Text, "0")
                .ActualCapitalCost = RI.SharedFunctions.DataClean(Me._txtActualCapital.Text, "0")
                .Title = RI.SharedFunctions.DataClean(Me._txtTitle.Text)
                .Description = RI.SharedFunctions.DataClean(Me._txtDescription.Text)

                'Dim outagechangeflag As String
                If Me._tbDateChangeComments.Text Is Nothing Or Me._tbDateChangeComments.Text = "" Then
                    .Comments = RI.SharedFunctions.DataClean(Me._txtComments.Text)
                Else
                    .Comments = RI.SharedFunctions.DataClean(Me._txtComments.Text) & " " & RI.SharedFunctions.DataClean(Me._tbDateChangeComments.Text)
                End If

                .OutageCoordinator = Me._ddlOutageCoord.SelectedValue
                .SDCategory = Me._rblSDCategory.SelectedValue

                .FEPAScore = Me._tbFEPAScore.Text
                .TGMCMFScore = Me._tbTGMCMFScore.Text
                .OverallScore = Me._tbOverallScore.Text
                .CommercialIssuesCnt = Me._tbCommIssuesCnt.Text
                .TGComments = Me._tbTGComments.Text

                .UserName = userProfile.Username
                If Request.QueryString("OutageNumber") IsNot Nothing Then
                    .OutageNumber = Request.QueryString("OutageNumber")
                End If

                If Me._cbAnnualOutage.Checked = "True" Then
                    .AnnualFlag = "Y"
                Else
                    .AnnualFlag = "N"
                End If

                .AssessmentDate = CStr(Me._AssessDate.DateValue)

                'currentOutage.TemplateTasks = Mid(sb, 2)
                '_mpeTemplates.Hide()
                .OutageTemplate = Me._ddlTemplateTasks.SelectedItem.Value

                .OutageNumber = .SaveOutage()

                If Me._tbDateChangeComments.Text Is Nothing Or Me._tbDateChangeComments.Text = "" Then
                    ' skip
                    _hfDateComment.Value = .ChangeDateMsg
                    _hfDateChangeFlag.Value = .ChangeDateFlag
                Else
                    EmailDateChanges(_hfDateComment.Value, _hfDateChangeFlag.Value)
                End If

                'Repopulate Contractor data if Outage dates have changed
                If .ChangeDateFlag = "Y" Then
                    Me._gvContractor.DataSource = .ContractorDT
                    Me._gvContractor.DataBind()
                    'If .ContractorDT IsNot Nothing Then
                    'EmailConflict = True
                    'End If
                End If

        If templateUsed = False Then
            'Determine if the user selected a template

            If Me._ddlTemplateTasks.SelectedItem.Value <> "" Then

                Dim returnCode As String
                returnCode = .SaveOutageEntireTemplate(.OutageNumber, Me._ddlTemplateTasks.SelectedValue)

                If returnCode <> "0" Then
                    Me._lblTemplateTaskMsg.Text = "Error Occurred Creating Template Tasks"
                End If

                Dim returnCode2 As String
                returnCode2 = .ReplicateOutageTasks(Me._ddlTemplateTasks.SelectedValue, .OutageNumber)
                If returnCode2 <> "0" Then
                    Me._lblTemplateTaskMsg.Text = "Error Occurred Creating Template Tasks"
                End If

                GetAndSendOutageTemplateEmail(.OutageNumber, userProfile.Email)

            End If
        End If


        Dim returnStatus As String = ""

        For Each row As GridViewRow In _gvContractor.Rows
            Dim lb As Label = row.FindControl("_lblseqid")
            Dim contractorseqid As String
            contractorseqid = lb.Text

            ' Dim contractorseqid As Label = ._gvContractor.SelectedDataKey.Value(j)
            Dim comments As TextBox = CType(row.FindControl("_txtCommentsContractor"), TextBox)
            Dim headCount As TextBox = CType(row.FindControl("_txtHeadCount"), TextBox)
            Dim dtStartDate, dtEndDate As String
            'Dim tbStartDate, tbEndDate As TextBox
            Dim ucstartDate, ucEndDate, ucStartEndDate As UserControl

            ucstartDate = CType(row.FindControl("_DateTimeStart"), UserControl)
            ucEndDate = CType(row.FindControl("_DateTimeEnd"), UserControl)
            ucStartEndDate = CType(row.FindControl("_ContractorstartEnd"), UserControl)
            dtStartDate = CType(ucStartEndDate.FindControl("_txtstartdate"), TextBox).Text
            dtEndDate = CType(ucStartEndDate.FindControl("_txtenddate"), TextBox).Text

            'tbStartDate = CType(ucstartDate.FindControl("_txtDate"), TextBox)
            'dtStartDate = tbStartDate.Text
            If IsDBNull(dtStartDate) Or Not IsDate(dtStartDate) Then dtStartDate = DBNull.Value.ToString

            'tbEndDate = CType(ucEndDate.FindControl("_txtDate"), TextBox)
            'dtEndDate = tbEndDate.Text
            If IsDBNull(dtEndDate) Or Not IsDate(dtEndDate) Then dtEndDate = DBNull.Value.ToString

            currentOutage.ContractorComments = comments.Text
            currentOutage.ContractorStartDate = dtStartDate
            currentOutage.ContractorEndDate = dtEndDate
            If headCount.Text <> "" Then
                currentOutage.ContractorHeadCount = Convert.ToInt32(headCount.Text)
            Else
                currentOutage.ContractorHeadCount = 0
            End If

            returnStatus = currentOutage.SaveOutageContractor(Request.QueryString("OutageNumber"), contractorseqid.ToString)

            Dim gvConflict As GridView = CType(row.FindControl("_gvConflicts"), GridView)

            For Each row2 As GridViewRow In gvConflict.Rows
                Dim conflictoutage As Label = CType(row2.FindControl("_lbConflictOutage"), Label)
                Dim conflictstatus As DropDownList = CType(row2.FindControl("_ddlConflictStatus"), DropDownList)
                Dim Status As String = conflictstatus.SelectedValue
                Dim Conflictcomments As TextBox = CType(row2.FindControl("_txtConflictComment"), TextBox)
                returnStatus = currentOutage.SaveContractorConflict(Request.QueryString("OutageNumber"), contractorseqid.ToString, conflictoutage.Text, Status, Conflictcomments.Text)
                ' EmailConflict = True
            Next

        Next

        For Each row As GridViewRow In _gvResources.Rows
            Dim lb As Label = row.FindControl("_lblResourceSeqid")
            Dim resourceseqid As String
            resourceseqid = lb.Text

            Dim comments As TextBox = CType(row.FindControl("_txtCommentsResource"), TextBox)
            Dim dtStartDate, dtEndDate As String
            Dim ucstartDate, ucEndDate, ucStartEndDate As UserControl

            ucstartDate = CType(row.FindControl("_DateTimeStart"), UserControl)
            ucEndDate = CType(row.FindControl("_DateTimeEnd"), UserControl)
            ucStartEndDate = CType(row.FindControl("_ResourcesstartEnd"), UserControl)
            dtStartDate = CType(ucStartEndDate.FindControl("_txtstartdate"), TextBox).Text
            dtEndDate = CType(ucStartEndDate.FindControl("_txtenddate"), TextBox).Text

            If IsDBNull(dtStartDate) Or Not IsDate(dtStartDate) Then dtStartDate = DBNull.Value.ToString

            If IsDBNull(dtEndDate) Or Not IsDate(dtEndDate) Then dtEndDate = DBNull.Value.ToString

            currentOutage.ResourceComments = comments.Text
            currentOutage.ResourceStartDate = dtStartDate
            currentOutage.ResourceEndDate = dtEndDate

            returnStatus = currentOutage.SaveOutageResource(Request.QueryString("OutageNumber"), resourceseqid.ToString)
        Next

        If Request.QueryString("OutageNumber") IsNot Nothing Then
            .OutageNumber = Request.QueryString("OutageNumber")
        End If

        If EmailConflict = True Then
            EmailConflicts()
        End If

                'Me._btnOutageDateChangeHidden_Click("", "")
                'Me._mpeOutageDateChange.Enabled = "true"

                'Me._udpLocation.Update()
                'ViewState("ShowTasksModalPopup") = True

                If .ChangeDateFlag = "Y" Or .ChangeDateFlag = "Pending" Then
                    'GetOutage()
                    EmailConflicts()
                    Me._tbDateChangeComments.Focus()
                    If .ChangeDateFlag = "Pending" Then
                        Me._lblOutageDateChange.Text = RI.SharedFunctions.LocalizeValue("Outage Date Changes Need to be approved.  MOC Created.") '& "<br>" & RI.SharedFunctions.LocalizeValue("Outage Date Changes Need to be approved.  MOC Created.")
                    End If
                    Me._mpeOutageDateChange.Show()
                Else
                    If .OutageNumber.Length > 0 And refreshPage = True Then
                Response.Redirect(Page.AppRelativeVirtualPath & "?OutageNumber=" & .OutageNumber, True)
            End If
        End If


            End With

        End If

    End Sub
    Private Sub PopulateContractor(ByVal outageNumber As Integer)
        Dim sqlOutage As String = String.Empty
        Dim ds As System.Data.DataSet = Nothing
        Dim ds3 As System.Data.DataSet = Nothing

        sqlOutage = String.Format("Select DISTINCT b.contractorseqid, " & _
        " initcap(c.firstname) firstname, initcap(c.lastname) lastname, rtrim(companyname) || DECODE(work_type,NULL,NULL,'-'||work_type)  contractor " & _
        " FROM  tbloutagecontractor b, refcontractor c, refworktype d " & _
        " where b.contractorseqid = c.contractorseqid and b.outagenumber = {0} and " & _
        " c.work_type_seq_id = d.work_type_seq_id (+) order by 4 ", outageNumber)

        Dim sqlAllMinusSelected As String = String.Format("SELECT DISTINCT contractorseqid," & _
        " initcap(c.firstname) firstname, initcap(c.lastname) lastname, rtrim(companyname) || DECODE(work_type,NULL,NULL,'-'||work_type)  contractor " & _
        " FROM  refcontractor c, refworktype d WHERE contractorseqid NOT IN (select contractorseqid from " & _
        " tbloutagecontractor where outagenumber = {0}) and c.work_type_seq_id = d.work_type_seq_id(+) ORDER BY 4", outageNumber)

        ds = RI.SharedFunctions.GetOracleDataSet(sqlOutage)
        ds3 = RI.SharedFunctions.GetOracleDataSet(sqlAllMinusSelected)

        Session.Add("OutageNumber", outageNumber)

        If ds IsNot Nothing Then
            With Me._slbContractorList
                .DataSource = ds3.Tables(0).CreateDataReader
                .DataTextField = "contractor"
                .DataValueField = "contractorseqid"
                .SelectedDataSource = ds.Tables(0).CreateDataReader
                .SelectedDataTextField = "contractor"
                .SelectedDataValueField = "contractorseqid"
                .DataBind()
                '.LocalizeData = False
            End With
            _pnlContractor.Style.Add("Display", "block")
            SelectedContractor.style.add("Display", "block")
            'Me._pnlContractor.Visible = True
        End If
    End Sub

    Private Function GetGlobalJSVarOutage() As String
        Dim sb As New StringBuilder

        sb.Append("var facility= $get('")
        sb.Append(Me._ddlFacility.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var businessUnit= $get('")
        sb.Append(Me._tcBusinessUnit.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var businessUnitArea= $get('")
        sb.Append(Me._tcBusinessUnitArea.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var startDate= $get('")
        sb.Append(Me._startEnd.ClientID)
        sb.Append("__txtStartDate');")
        sb.AppendLine()
        sb.Append("var startHrs=$get('")
        sb.Append(Me._startEnd.ClientID)
        sb.Append("__ddlStartHrs');")
        sb.AppendLine()
        sb.Append("var startMins=$get('")
        sb.Append(Me._startEnd.ClientID)
        sb.Append("__ddlStartMins');")
        sb.AppendLine()

        sb.Append("var endDate= $get('")
        sb.Append(Me._startEnd.ClientID)
        sb.Append("__txtEndDate');")
        sb.AppendLine()
        sb.Append("var endHrs=$get('")
        sb.Append(Me._startEnd.ClientID)
        sb.Append("__ddlEndHrs');")
        sb.AppendLine()
        sb.Append("var endMins=$get('")
        sb.Append(Me._startEnd.ClientID)
        sb.Append("__ddlEndMins');")
        sb.AppendLine()
        sb.Append("var downtime=$get('")
        sb.Append(Me._txtDownTime.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var description=$get('")
        sb.Append(Me._txtDescription.ClientID)
        sb.Append("');")
        sb.AppendLine()

        sb.Append("var ActualStartDate= $get('")
        sb.Append(Me._ActualStartEnd.ClientID)
        sb.Append("__txtStartDate');")
        sb.AppendLine()
        sb.Append("var ActualStartHrs=$get('")
        sb.Append(Me._ActualStartEnd.ClientID)
        sb.Append("__ddlStartHrs');")
        sb.AppendLine()
        sb.Append("var ActualStartMins=$get('")
        sb.Append(Me._ActualStartEnd.ClientID)
        sb.Append("__ddlStartMins');")
        sb.AppendLine()

        sb.Append("var ActualEndDate= $get('")
        sb.Append(Me._ActualStartEnd.ClientID)
        sb.Append("__txtEndDate');")
        sb.AppendLine()
        sb.Append("var ActualEndHrs=$get('")
        sb.Append(Me._ActualStartEnd.ClientID)
        sb.Append("__ddlEndHrs');")
        sb.AppendLine()
        sb.Append("var ActualEndMins=$get('")
        sb.Append(Me._ActualStartEnd.ClientID)
        sb.Append("__ddlEndMins');")
        sb.AppendLine()
        sb.Append("var lblConflictChanged=$get('")
        sb.Append(Me._lblConflictChanged.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var ActualDowntime=$get('")
        sb.Append(Me._txtActualDowntime.ClientID)
        sb.Append("');")
        sb.AppendLine()

        Return sb.ToString
    End Function

    Protected Sub _btnAttachments_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnAttachments.Click
        SaveOutage(True)
        'Me._btnAttachments.OnClientClick = Master.GetPopupWindowJS("~/Outage/FileUpload.aspx?OutageNumber=" & currentOutage.OutageNumber, , 773, 400, True)
        'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "FileUpload", Master.GetPopupWindowJS("~/Outage/FileUpload.aspx?OutageNumber=" & currentOutage.OutageNumber, , 773, 400, True), True)
    End Sub

    Protected Sub _gvContractors_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _gvContractor.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim paramCollection As New Devart.Data.Oracle.OracleParameterCollection
            Dim param As New Devart.Data.Oracle.OracleParameter
            Dim ds As System.Data.DataSet = Nothing

            'Check input paramaters

            'Try
            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "in_OutageNumber"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = currentOutage.OutageNumber
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "in_ContractorSeqID"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = e.Row.DataItem("ContractorSeqID").ToString
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsContractorConflicts"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "GetContractorConflicts"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.GetContractorConflicts", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then

                    Dim dr As System.Data.DataTableReader = ds.Tables(0).CreateDataReader
                    Dim gv As GridView = e.Row.FindControl("_gvConflicts")

                    gv.DataSource = ds.Tables(0)
                    gv.DataBind()


                End If
            End If

            'Dim lbstartDate As UserControl = CType(e.Row.Cells(0).FindControl("_ContractorstartEnd"), UserControl)
            'Dim lbendDate As Label = CType(e.Row.Cells(0).FindControl("_lblenddate"), Label)
            Dim ucstartDate As ucStartEndCalendar = CType(e.Row.Cells(0).FindControl("_DateTimeStart"), ucStartEndCalendar)
            Dim ucEndDate As ucStartEndCalendar = CType(e.Row.Cells(0).FindControl("_DateTimeEnd"), ucStartEndCalendar)
            Dim ucStartEndDate As ucStartEndCalendar = CType(e.Row.Cells(0).FindControl("_ContractorstartEnd"), ucStartEndCalendar)
            Dim dtStartDate As TextBox = CType(ucStartEndDate.FindControl("_txtstartdate"), TextBox)
            Dim dtEndDate As TextBox = CType(ucStartEndDate.FindControl("_txtenddate"), TextBox)
            Dim iBStartEnd As ImageButton = CType(ucStartEndDate.FindControl("_imgStartCal"), ImageButton)

            If ucStartEndDate IsNot Nothing Then
                ucStartEndDate.CalendarCommitScript = "ConflictFieldChanged()"
                ucStartEndDate.RefreshCalendarOnClick()
                'ucstartDate.CalendarCommitScript = "ConflictFieldChanged(" + _lblConflictChanged.ClientID + ");"
                'dtStartDate.Attributes.Add("CalendarCommitScript", "ConflictFieldChanged(" + _lblConflictChanged.ClientID + ");")
            End If
            If dtEndDate IsNot Nothing Then
                ucStartEndDate.CalendarCommitScript = "ConflictFieldChanged()"
                ucStartEndDate.RefreshCalendarOnClick()
                'dtEndDate.Attributes.Add("OnChange", "ConflictFieldChanged(" + _lblConflictChanged.ClientID + ");")
            End If
        End If

    End Sub
    Protected Sub ConflictRow(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Dim ddl As DropDownList = (e.Row.Cells(0).FindControl("_ddlConflictStatus"))
        Dim txtComment As AdvancedTextBox.AdvancedTextBox = (e.Row.Cells(0).FindControl("_txtConflictComment"))
        Dim img As HtmlImage = e.Row.FindControl("bulletimg")
        'Dim lblConflictChanged As Label = Form.FindControl("_lblConflictChanged")
        If ddl IsNot Nothing Then
            ddl.Attributes.Add("OnChange", "ChangeImage(" + ddl.ClientID + "," + img.ClientID + ");ConflictFieldChanged();")
            'ddl.Attributes.Add("OnChange", "ConflictFieldChanged(" + _lblConflictChanged.ClientID + ");")
            If ddl.SelectedValue = "C" Then
                img.Src = "../Images/bullet_red.png"
            ElseIf ddl.SelectedValue = "I" Then
                img.Src = "../Images/bullet_yellow.png"
            Else
                img.Src = "../Images/bullet_green.png"
            End If
        End If
        If txtComment IsNot Nothing Then
            txtComment.Attributes.Add("OnChange", "ConflictFieldChanged();")
        End If
    End Sub

    Protected Sub _gvContractors_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles _gvContractor.RowDeleting
        clsCurrentOutage.DeleteOutageContractor(currentOutage.OutageNumber, Me._gvContractor.DataKeys.Item(e.RowIndex).Value.ToString)
        currentOutage = New clsCurrentOutage(currentOutage.OutageNumber)
    End Sub

    Protected Sub _gvContractors_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles _gvContractor.RowEditing
        Me._gvContractor.EditIndex = e.NewEditIndex
    End Sub

    Protected Sub _btnAddContractor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnAdd.Click
        Dim selectedContractor As String = Me._slbContractorList.SelectedValue

        If selectedContractor IsNot Nothing AndAlso selectedContractor.Length > 0 AndAlso currentOutage IsNot Nothing Then
            currentOutage.InsertOutageContractor(currentOutage.OutageNumber, selectedContractor)

            'EmailConflicts checks for the existence of conflicts and will email the coordinators if any exist.
            EmailConflicts()

            Response.Clear()
            Response.Redirect(Page.AppRelativeVirtualPath & "?OutageNumber=" & currentOutage.OutageNumber, True)
            Response.End()
        End If
    End Sub
    'Protected Sub _ddlTemplateTasks_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlTemplateTasks.SelectedIndexChanged
    '    'ViewState("ShowTasksModalPopup") = False

    '    ''If Not Page.IsPostBack Then

    '    'If Me._ddlTemplateTasks.SelectedValue <> "" Then
    '    '    Me._ddlTemplateTasks.Enabled = True
    '    '    '_btnViewTemplateTasks.Style.Add("Display", "block")
    '    '    'Else
    '    '    'Me._btnViewTemplateTasks.Style.Add("Display", "none")
    '    'End If

    '    'Dim strTemplateName As String = _ddlTemplateTasks.SelectedItem.Text


    '    'Dim paramCollection As New Devart.Data.Oracle.OracleParameterCollection
    '    'Dim param As New Devart.Data.Oracle.OracleParameter
    '    'Dim ds As System.Data.DataSet = Nothing

    '    'Try

    '    '    'Check input paramaters

    '    '    If ViewState("ShowTasksModalPopup") <> "True" Then
    '    '        param = New Devart.Data.Oracle.OracleParameter
    '    '        param.ParameterName = "rsRoles"
    '    '        param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
    '    '        param.Direction = Data.ParameterDirection.Output
    '    '        paramCollection.Add(param)

    '    '        Dim key As String = "GetMTTRoles_"
    '    '        ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.GetMTTRoles", key, 2)

    '    '        If ds IsNot Nothing Then
    '    '            If ds.Tables.Count >= 1 Then
    '    '                'Roles                     
    '    '                Dim dr As System.Data.DataTableReader = ds.Tables(0).CreateDataReader

    '    '                With _ddlHiddenRoles
    '    '                    .DataSource = ds.Tables(0).CreateDataReader()
    '    '                    .DataTextField = "roledescription"
    '    '                    .DataValueField = "roleseqid"
    '    '                    .DataBind()
    '    '                End With

    '    '            End If
    '    '        End If

    '    '        'paramCollection.Clear()

    '    '        'param = New Devart.Data.Oracle.OracleParameter
    '    '        'param.ParameterName = "in_TaskHeader"
    '    '        'param.OracleDbType = Devart.Data.Oracle.OracleDbType.VarChar
    '    '        'param.Direction = Data.ParameterDirection.Input
    '    '        'param.Value = Me._ddlTemplateTasks.SelectedItem.Value
    '    '        'paramCollection.Add(param)

    '    '        'param = New Devart.Data.Oracle.OracleParameter
    '    '        'param.ParameterName = "rsTaskItemList"
    '    '        'param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
    '    '        'param.Direction = Data.ParameterDirection.Output
    '    '        'paramCollection.Add(param)

    '    '        'key = "GetTemplateTasks_" & Me._ddlTemplateTasks.SelectedItem.Value
    '    '        'ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.GetOutageTemplateTaskItemList", key, 2)

    '    '        'If ds IsNot Nothing Then
    '    '        '    If ds.Tables.Count >= 1 Then
    '    '        '        'Tasks                     
    '    '        '        Dim dr As System.Data.DataTableReader = ds.Tables(0).CreateDataReader
    '    '        '        Dim dt As System.Data.DataTable = ds.Tables(0)
    '    '        '        Me._grvTasksPage.DataSource = dt
    '    '        '        _grvTasksPage.AutoGenerateColumns = False
    '    '        '        _grvTasksPage.DataBind()
    '    '        '        _grvTasksPage.Visible = True
    '    '        '        If InStr(strTemplateName, "Turbine Generator") > 0 Then
    '    '        '            '_grvTasksPage.Columns.Item(0).ItemStyle = "display:none"
    '    '        '            'Me._btnViewTemplateTasks.Style.Add("Display", "none")
    '    '        '            '_grvTasksPage.Columns.Item(0)..style.add()
    '    '        '        End If
    '    '        '        'Me._dlTasks.DataSource = dt
    '    '        '        '_dlTasks.DataBind()
    '    '        '        '_dlTasks.Visible = True

    '    '        '        If _grvTasksPage.PageCount = 1 Then
    '    '        '            Me._lbNewTemplateName.Visible = "True"
    '    '        '            _cbSaveTemplate.Visible = "true"
    '    '        '            Me._tbTemplateName.Visible = "true"
    '    '        '        Else
    '    '        '            Me._lbNewTemplateName.Visible = "False"
    '    '        '            _cbSaveTemplate.Visible = "false"
    '    '        '            Me._tbTemplateName.Visible = "false"
    '    '        '        End If
    '    '        '    End If
    '    '        'End If

    '    '        'Me._udptasks.Update()
    '    '    End If

    '    '    If Not Page.IsPostBack Then

    '    '        _mpeTemplates.Show()
    '    '    End If
    '    'Catch ex As Exception
    '    '    Throw
    '    'Finally
    '    '    If ds IsNot Nothing Then
    '    '        ds = Nothing
    '    '    End If

    '    'End Try
    '    ''End If

    'End Sub
    Protected Sub GetTemplateTasks()
        ViewState("ShowTasksModalPopup") = False

        If Me._ddlTemplateTasks.SelectedValue <> "" Then
            Me._ddlTemplateTasks.Enabled = True
            '_btnViewTemplateTasks.Style.Add("Display", "block")
            'Else
            'Me._btnViewTemplateTasks.Style.Add("Display", "none")
        End If

        Dim strTemplateName As String = _ddlTemplateTasks.SelectedItem.Text

        Dim paramCollection As New Devart.Data.Oracle.OracleParameterCollection
        Dim param As New Devart.Data.Oracle.OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            'Check input paramaters

            If ViewState("ShowTasksModalPopup") <> "True" Then
                param = New Devart.Data.Oracle.OracleParameter
                param.ParameterName = "rsRoles"
                param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
                param.Direction = Data.ParameterDirection.Output
                paramCollection.Add(param)

                Dim key As String = "GetMTTRoles_"
                ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.GetMTTRoles", key, 2)

                If ds IsNot Nothing Then
                    If ds.Tables.Count >= 1 Then
                        'Roles                     
                        Dim dr As System.Data.DataTableReader = ds.Tables(0).CreateDataReader
                        Dim roles As New clsData
                        With roles
                            .DataSource = ds.Tables(0).CreateDataReader()
                            .DataTextField = "roledescription"
                            .DataValueField = "roleseqid"
                        End With
                        'With _ddlHiddenRoles
                        '    .DataSource = ds.Tables(0).CreateDataReader()
                        '    .DataTextField = "roledescription"
                        '    .DataValueField = "roleseqid"
                        '    .DataBind()

                        'End With
                        RI.SharedFunctions.BindList(Me._ddlHiddenRoles, roles, False, False)

                    End If
                End If

                paramCollection.Clear()

                
                key = "GetTemplateTasks_" & Me._ddlTemplateTasks.SelectedItem.Value
                param = New Devart.Data.Oracle.OracleParameter
                param.ParameterName = "in_TaskHeader"
                param.OracleDbType = Devart.Data.Oracle.OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = Me._ddlTemplateTasks.SelectedItem.Value
                paramCollection.Add(param)

                param = New Devart.Data.Oracle.OracleParameter
                param.ParameterName = "rsTaskItemList"
                param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
                param.Direction = Data.ParameterDirection.Output
                paramCollection.Add(param)

                ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.GetOutageTemplateTaskItemList", key, 2)

                If ds IsNot Nothing Then
                    If ds.Tables.Count >= 1 Then
                        'Tasks                     
                        Dim dr As System.Data.DataTableReader = ds.Tables(0).CreateDataReader
                        Dim dt As System.Data.DataTable = ds.Tables(0)
                        Me._grvTasksPage.DataSource = dt
                        _grvTasksPage.AutoGenerateColumns = False
                        _grvTasksPage.DataBind()
                        _grvTasksPage.Visible = True
                        'If InStr(strTemplateName, "Turbine Generator") > 0 Then
                        '_grvTasksPage.Columns.Item(0).Visible = False
                        'Me._btnViewTemplateTasks.Style.Add("Display", "none")
                        '_grvTasksPage.Columns.Item(0).style.add()
                        'End If

                    End If
                End If

                Me._udptasks.Update()
            End If

        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                ds = Nothing
            End If
        End Try

    End Sub
    Public Sub GetAndSendOutageTemplateEmail(ByVal OutageNumber As Integer, ByVal LastUpdateUserName As String)
        Dim clsSearch As New clsOutageTemplateEmail
        Dim CallSource As String = String.Empty
        Dim dr As Data.DataRow = Nothing
        Dim dt As Data.DataTable = Nothing

        Dim table As clsOutageTemplateEmail = Nothing
        Dim emptyCursor As Object = Nothing
        Dim retVal As Boolean
        Dim v_td As String() = {"<TD>", "</TD>"}
        Dim sbEmailBody As New System.Text.StringBuilder
        Dim strPrevRecType As String
        Dim strEmailAddress As String = ""
        Dim strRecType As String
        Dim strSiteName As String
        Dim strTitle As String
        Dim strTemplateTitle As String = ""
        Dim strTemplateNbr As String = ""
        Dim strTaskHeaderNbr As String = ""
        Dim strFailureFlag As String = ""
        Dim strWarningFlag As String = ""

        Dim strRoleDescription As String

        Dim strSubject As String = ""
        Dim strMsg As String = ""
        Dim strBody As String = ""
        Dim strFooter As String = ""
        Dim strHeading1 As String = ""
        Dim strHeading2 As String = ""
        Dim strDB As String = ""

        Try
            If Request.UserHostAddress = "127.0.0.1" Then
                strDB = "http://gpiri.graphicpkg.com"
            Else
                If Request.ServerVariables("HTTP_HOST").ToLower.Contains("ridev") Then
                    strDB = "ridev.ipaper.com"
                ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritest") Then
                    strDB = "ritest.ipaper.com"
                ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritrain") Then
                    strDB = "ritrain.ipaper.com"
                Else
                    strDB = "http://gpiri.graphicpkg.com"
                End If

            End If

            If clsSearch IsNot Nothing Then
                dt = clsSearch.GetDataTable(OutageNumber.ToString)

                strPrevRecType = String.Empty
                sbEmailBody = New System.Text.StringBuilder
                sbEmailBody.Append("<TABLE border='1' cellpadding='2' cellspacing='0' style='background-color:black'>")
                For Each dr In dt.Rows
                    strEmailAddress = userProfile.Email 'dr.Item("EMAIL").ToString
                    ' strEmailAddress = "amy.albrinck@ipaper.com"

                    ' strRecType = ipLoc.GetResourceValue(dr.Item("EMAIL_SECTION").ToString)
                    strRecType = dr.Item("EMAIL_SECTION").ToString

                    strSiteName = dr.Item("SITENAME").ToString

                    strTitle = dr.Item("STATUS").ToString

                    strTemplateTitle = dr.Item("TEMPLATE_TITLE").ToString
                    strTemplateNbr = dr.Item("TEMPLATESEQID").ToString
                    strTaskHeaderNbr = dr.Item("TASKHEADERSEQID").ToString

                    strRoleDescription = dr.Item("ROLEDESCRIPTION").ToString

                    'sbEmailBody.Append("<P><font size =2 face=Arial><B><U>" & strRecType & "</B></U></FONT><BR>")
                    'sbEmailBody.Append("<TABLE border=2 cellpadding='2' cellspacing='0' style='border-color:black'><font size =2 face=Arial><TR valign=top><B>")
                    'sbEmailBody.Append("</B></TR>")

                    If strRecType = "1Failure" And strPrevRecType <> strRecType Then
                        sbEmailBody.Append("<TR style='background-color:white'  ><TD bgcolor=red colspan=3>" & "FAILURES" & "</TD></TR>")
                        strPrevRecType = strRecType
                        strFailureFlag = "Y"
                    Else
                        If strRecType = "2Warning" And strPrevRecType <> strRecType Then
                            sbEmailBody.Append("<TR style='background-color:white' ><TD  bgcolor=yellow colspan=3>" & "WARNINGS" & "</TD></TR>")
                            strPrevRecType = strRecType
                            strWarningFlag = "Y"
                        Else
                            If strPrevRecType <> strRecType Then
                                sbEmailBody.Append("<TR style='background-color:white' ><TD bgcolor=green colspan=3>" & "COMPLETED SUCCESSFULLY" & "</TD></TR>")
                                strPrevRecType = strRecType
                            End If
                        End If
                    End If

                    sbEmailBody.Append("<TR style='background-color:white'><TD>" & strSiteName & "</TD> <TD>" & strTitle & "</TD> <TD>" & strRoleDescription & "</TD></TR>")

                    If strFailureFlag = "Y" Then
                        strSubject = "Outage Template Tasks Created with FAILURES"
                    Else
                        If strFailureFlag <> "Y" And strWarningFlag = "Y" Then
                            strSubject = "Outage Template Tasks with WARNINGS"
                        Else
                            If strFailureFlag <> "Y" And strWarningFlag <> "Y" Then
                                strSubject = "Outage Template Tasks Complete"
                            End If
                        End If
                    End If

                Next

                sbEmailBody.Append("</TR></TABLE>")
                strMsg = sbEmailBody.ToString

                strHeading1 = "<HTML><BODY><font size=3 face=Arial><B>Manufacturing Task Tracker Tasks were created for Outage " & OutageNumber & ".</B><BR><BR><B>"
                'strHeading2 = "<A HREF=HTTP://" & strDB & "/RI/Outage/EnterOutage.aspx?OUtageNumber=" & OutageNumber & ">" & strTemplateTitle & "</A></B><BR><BR>"
                strHeading2 = "<A HREF=http://gpiri.graphicpkg.com/RI/Outage/EnterOutage.aspx?OUtageNumber=" & OutageNumber & ">" & strTemplateTitle & "</A></B><BR><BR>"
                strFooter = "</HTML></BODY>"
                'sbEmailBody.Append("{0}<A HREF=HTTP://" & strDB & "/TaskTracker/TaskHeader.aspx?HeaderNumber=" & strTaskHeaderID & ">" & strHeaderTitle & " (" & strActivity & ")</A>{1}")

                strBody = strHeading1 & strHeading2 & "<BR>" & strMsg.ToString & strFooter
                strBody = RI.SharedFunctions.cleanString(strBody, "<br>")
                'strEmailAddress = "amy.albrinck@ipaper.com"
                If strEmailAddress <> "" Then
                    RI.SharedFunctions.SendEmail(strEmailAddress, "manufacturing.task@graphicpkg.com", strSubject, strBody)
                End If
                strBody = ""
                retVal = True

            End If

        Catch
            retVal = False
            Throw
        Finally
            dr = Nothing
            dt = Nothing
        End Try
        'Return retVal
        dr = Nothing
    End Sub

    Protected Sub _MessageBox_OKClick() Handles _messageBox.OKClick
        SaveOutage(True)
    End Sub


    Protected Sub _btnViewTemplateTasks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnViewTemplateTasks.Click
        Try
            GetTemplateTasks()
            _mpeTemplates.Show()
            ViewState("ShowTasksModalPopup") = True

        Catch ex As Exception
            Throw
        Finally
        End Try

    End Sub


    Protected Sub _btnOutageDateChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnOutageDateChange.Click
        'Dim OutageChangeFlag As String
        Try
            If Me._tbDateChangeComments.Text Is Nothing Or Me._tbDateChangeComments.Text = "" Then
                Me._tbDateChangeComments.Text = Master.RIRESOURCES.GetResourceValue("Outage Dates Changed On") & ": " & Convert.ToDateTime(Date.Today, New System.Globalization.CultureInfo("EN-US"))
                SaveOutageComment(True)
            Else
                ' Me._tbDateChangeComments.Text = Me._tbDateChangeComments.Text & " " & Master.RIRESOURCES.GetResourceValue("Outage Dates Changed On") & ": " & Convert.ToDateTime(Date.Today, New System.Globalization.CultureInfo("EN-US"))
                SaveOutageComment(True)
            End If

        Catch ex As Exception
            Throw
        Finally
        End Try

    End Sub


    Protected Sub _btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnCreate.Click
        userProfile = RI.SharedFunctions.GetUserProfile
        currentOutage = New clsCurrentOutage(Request.QueryString("OutageNumber"))
        Dim strRole1, strRole2, strRole3, strRole4, strRole5 As String
        Dim strRoleSeqIDs As String = ""
        Dim cntRowChecked As Integer = 0

        Dim returns As String
        For Each row As GridViewRow In Me._grvTasksPage.Rows
            strRoleSeqIDs = ""
            Dim cb As CheckBox = row.FindControl("_cbStatus")

            If cb.Checked = True And row.Enabled = True Then
                cntRowChecked = 1
                'sb = sb & "~" & row.Cells(1).Text.ToString & "~"
                Dim taskSeqID As HiddenField
                taskSeqID = row.FindControl("_hfTaskItemSeqID")
                '.TemplateTasks = .TemplateTasks & taskSeqID.Value.ToString & "~"
                Dim roleddl1 As DropDownList
                roleddl1 = CType(row.FindControl("_ddlTemplateRole"), DropDownList)
                strRole1 = Request.Form(roleddl1.UniqueID)

                Dim roleddl2 As DropDownList
                roleddl2 = CType(row.FindControl("_ddlTemplateRole2"), DropDownList)
                strRole2 = Request.Form(roleddl2.UniqueID)

                Dim roleddl3 As DropDownList
                roleddl3 = CType(row.FindControl("_ddlTemplateRole3"), DropDownList)
                strRole3 = Request.Form(roleddl3.UniqueID)

                Dim roleddl4 As DropDownList
                roleddl4 = CType(row.FindControl("_ddlTemplateRole4"), DropDownList)
                strRole4 = Request.Form(roleddl4.UniqueID)

                Dim roleddl5 As DropDownList
                roleddl5 = CType(row.FindControl("_ddlTemplateRole5"), DropDownList)
                strRole5 = Request.Form(roleddl5.UniqueID)

                If strRole1 <> "" Or strRole2 <> "" Or strRole3 <> "" Or strRole4 <> "" Or strRole5 <> "" Then
                    strRoleSeqIDs = strRoleSeqIDs & strRole1 & "," & strRole2 & "," & strRole3 & "," & strRole4 & "," & strRole5 & ","
                Else
                    strRoleSeqIDs = ""
                End If

                returns = currentOutage.SaveTemplateTasks(currentOutage.OutageNumber, taskSeqID.Value.ToString, strRoleSeqIDs)

                If returns <> "0" Then
                    Me._lblTaskStatus.Text = "Error Occurred Saving tasks to tbloutagemtttemplatetasks"
                Else
                    'row.BackColor = Drawing.Color.Green
                    row.Enabled = False
                End If
            End If
        Next

        If cntRowChecked = 0 Then
            Me._lblTaskStatus.Text = "NO Tasks were selected"
            Exit Sub
        End If

        returns = currentOutage.ReplicateOutageTasks(Me._ddlTemplateTasks.SelectedValue, currentOutage.OutageNumber)

        If returns <> "0" Then
            Me._lblTaskStatus.Text = "Error Occurred Creating Tasks"
        Else
            Me._lblTaskStatus.Text = "Tasks Created Successfully"
            Me._hfTasksCreated.Value = "Y"
            If Me._grvTasksPage.PageCount = Me._grvTasksPage.PageIndex + 1 Then
                GetAndSendOutageTemplateEmail(currentOutage.OutageNumber, userProfile.Email)
            End If
            If Me._cbSaveTemplate.Checked = True Then
                Dim strTemplateName As String
                strTemplateName = Me._tbTemplateName.Text
                Dim return3 As String
                return3 = currentOutage.CreateNewOutageMTTTemplate(userProfile.Username, currentOutage.OutageNumber, strTemplateName)
                Me._lblTaskStatus.Text = "New Template and Tasks Created Successfully"
            End If

        End If
    End Sub
    Private Sub EmailConflicts()

        Dim clsConflicts As New clsOutageConflicts
        Dim dr As Data.DataRow = Nothing
        Dim dt As Data.DataTable = Nothing

        Dim table As clsOutageTemplateEmail = Nothing
        Dim Body As New StringBuilder
        Dim commonBody As New StringBuilder
        Dim spacer As String = "&nbsp;&nbsp;&nbsp;&nbsp;"
        Dim url As String = String.Empty
        Dim iploc As New IP.Bids.Localization.WebLocalization

        Dim strConflictStatus As String = String.Empty
        Dim strOutageTitle As String = String.Empty
        Dim strConflictOutageTitle As String = String.Empty
        Dim strConflictOutageNumber As String = String.Empty
        Dim strConflictComment As String = String.Empty
        Dim strContractor As String = String.Empty
        Dim strConflictSite As String = String.Empty
        Dim strEmailAddress As String = String.Empty
        Dim strContractorStart As Date
        Dim strContractorEnd As Date
        Dim strCoordName, strCoordPhone, strCoordEmail As String

        Dim additonalTextForEmail As String = "<p>" & Master.RIRESOURCES.GetResourceValue("Additional Information") & " {0}:<br>"
        Dim additionalText As String = "<h2>" & Master.RIRESOURCES.GetResourceValue("**** THIS IS A TEST INCIDENT NOTIFICATION ****") & "</H2>"

        Dim urlHost As String

        Dim OutageNumber As String = Request.QueryString("OutageNumber")
        currentOutage = New clsCurrentOutage(OutageNumber)

        If clsConflicts IsNot Nothing Then
            strEmailAddress = currentOutage.CoordEmail

            dt = clsConflicts.GetDataTable(OutageNumber)

            If dt.Rows.Count = 0 Then
                Exit Sub
            Else
                
                If Request.UserHostAddress = "127.0.0.1" Then
                    urlHost = "http://gpiri.graphicpkg.com/ri/outage/"
                Else
                    If Request.ServerVariables("HTTP_HOST").ToLower.Contains("ridev") Then
                        urlHost = "Http://ridev.ipaper.com/ri/outage/"
                    ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritest") Then
                        urlHost = "Http://ritest.ipaper.com/ri/outage/"
                    ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritrain") Then
                        urlHost = "Http://ritrain/ri/outage/"
                    Else
                        'urlHost = "Http://rigpi.ipaper.com/ri/outage/"
                        urlHost = "http://gpiri.graphicpkg.com/ri/Outage/"
                        additionalText = String.Empty
                    End If

                End If

                url = urlHost & "EnterOutage.aspx?OutageNumber=" & currentOutage.OutageNumber
                Body.Append(RI.SharedFunctions.cleanString(additionalText, "<br>"))
                Body.Append("</table><P style=font-size:15px;font-family:Arial><B><U><A HREF=" & urlHost & "EnterOutage.aspx?OutageNumber=" & OutageNumber & ">" & OutageNumber & " - " & currentOutage.Title & "</A></B></U></FONT><BR>")
                Body.Append("OUTAGE CONTRACTOR DATES IN CONFLICT WITH")

                Body.Append("<TABLE border=1 style='font-family:Arial; font-size:12px;' ><TR valign=top><B><td>Status (Select to update){1}<TD width=20%>Contractor{1}<TD>Start Date{1}<TD>End Date{1}<TD width=8%> Site{1}<TD width=30%> Title{1}<TD width=10%>Coordinator{1}")
                Body.Append("</B></TR>")

                For Each dr In dt.Rows
                    strConflictStatus = dr.Item("conflictStatus").ToString

                    strContractor = dr.Item("contractorname").ToString
                    strContractorStart = dr.Item("prework_startdate").ToString

                    strContractorEnd = dr.Item("postwork_enddate").ToString

                    strConflictSite = dr.Item("conflictSitename")
                    strConflictOutageTitle = dr.Item("conflictoutagetitle").ToString & " (" & dr.Item("conflictoutagenumber") & ")"
                    strConflictComment = dr.Item("conflictcomment").ToString

                    strConflictOutageNumber = dr.Item("ConflictOutageNumber").ToString

                    strCoordName = dr.Item("outagecoordname").ToString
                    strCoordEmail = dr.Item("outagecoordemail").ToString
                    strCoordPhone = dr.Item("outagecoordphone").ToString

                    commonBody.Append("<BR><TR valign=top>")
                    'sbEmailBody.Append("{0}<A HREF=HTTP://" & strDB & "/RI/Outage/EnterOutage.aspx?OutageNumber=" & strOutageNumber & ">" & strOutageNumber & "-" & strTitle & "</A>{1}")
                    If strConflictStatus = "C" Then commonBody.Append("{0}<A style='background-color:red; color:black' HREF=" & urlHost & "EnterOutage.aspx?OutageNumber=" & strConflictOutageNumber & ">CONFLICT</A>")
                    If strConflictStatus = "I" Then commonBody.Append("{0}<A style='background-color: yellow; color:black' HREF=" & urlHost & "EnterOutage.aspx?OutageNumber=" & strConflictOutageNumber & ">INVESTIGATING</A>")
                    If strConflictStatus = "N" Then commonBody.Append("{0}<A style='background-color: green; color:black' HREF=" & urlHost & "EnterOutage.aspx?OutageNumber=" & strConflictOutageNumber & ">INVESTIGATED-NO CONFLICT</A>{1}")
                    commonBody.Append("{0}" & strContractor & "<br>" & strConflictComment & "{1}")
                    commonBody.Append("{0}" & strContractorStart.ToShortDateString & "{1}")
                    commonBody.Append("{0}" & strContractorEnd.ToShortDateString & "{1}")
                    commonBody.Append("{0}" & strConflictSite & "{1}")
                    commonBody.Append("{0}" & strConflictOutageTitle & "{1}")
                    commonBody.Append("{0}" & strCoordName & "<br>" & strCoordEmail & "<br>" & strCoordPhone & "{1}")

                    If strCoordEmail <> "" Then
                        strEmailAddress = strEmailAddress & "," & strCoordEmail
                    End If
                Next

                Dim v_td As String() = {"<TD>", "</TD>"}

                commonBody.Append("</ul>")
                '******End Common Body ************

                Body.Append(commonBody.ToString)
                Dim strBody, strFooter As String

                strBody = String.Format(Body.ToString, v_td)
                strFooter = "</HTML></BODY>"
                strBody = "<P style=font-size:20px;font-family:Arial><B>" & strBody.ToString & strFooter

                Body.Append("</body></html>")

                Dim subjectForNotificationList As String = Master.RIRESOURCES.GetResourceValue("Contractor Conflict(s) Exist for Outage") & " " & OutageNumber
                Dim msgForNotificationList As String = String.Format(Body.ToString, v_td)

                RI.SharedFunctions.SendEmail(strEmailAddress, userProfile.Email, subjectForNotificationList, msgForNotificationList, userProfile.FullName)
            End If
        End If

    End Sub

    Private Sub EmailDateChanges(ByVal ChangeDateMsg As String, ByVal ChangeDateFlag As String)

        Dim dr As Data.DataRow = Nothing
        Dim dt As New Data.DataTable

        Dim Body As New StringBuilder
        Dim commonBody As New StringBuilder
        Dim spacer As String = "&nbsp;&nbsp;&nbsp;&nbsp;"
        Dim url As String = String.Empty
        Dim iploc As New IP.Bids.Localization.WebLocalization

        Dim strContractor As String = String.Empty
        Dim strPerson As String = String.Empty
        Dim strEmailAddress As String = String.Empty
        Dim strContractorStart As Date
        Dim strContractorEnd As Date
        Dim strResourceStart As Date
        Dim strResourceEnd As Date

        Dim additonalTextForEmail As String = "<p>" & Master.RIRESOURCES.GetResourceValue("Additional Information") & " {0}:<br>"
        Dim additionalText As String = "<h2>" & Master.RIRESOURCES.GetResourceValue("**** THIS IS A TEST INCIDENT NOTIFICATION ****") & "</H2>"

        Dim urlHost As String
        Dim urlMOCHost As String

        Dim OutageNumber As String = Request.QueryString("OutageNumber")
        currentOutage = New clsCurrentOutage(OutageNumber)


        If Request.UserHostAddress = "127.0.0.1" Then
            urlHost = "Http://ridev.ipaper.com/ri/outage/"
            urlMOCHost = "Http://ridev.ipaper.com/ri/moc/"
        Else
            If Request.ServerVariables("HTTP_HOST").ToLower.Contains("ridev") Then
                urlHost = "Http://ridev.ipaper.com/ri/outage/"
                urlMOCHost = "Http://ridev.ipaper.com/ri/moc/"
            ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritest") Then
                urlHost = "Http://ritest.ipaper.com/ri/outage/"
                urlMOCHost = "Http://ritest.ipaper.com/ri/moc/"
            ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritrain") Then
                urlHost = "Http://ritrain/ri/outage/"
                urlMOCHost = "Http://ritrain/ri/moc/"
            Else
                urlHost = "http://gpiri.graphicpkg.com/ri/outage/"
                urlMOCHost = "http://gpiri.graphicpkg.com/ri/moc/"
                additionalText = String.Empty
            End If

        End If
        Dim subjectForNotificationList As String
        Dim EmailDateMsg As String
        EmailDateMsg = Master.RIRESOURCES.GetResourceValue("PrevStartDate") & ": " & IP.Bids.Localization.DateTime.GetLocalizedDateTime(RI.SharedFunctions.CDateFromEnglishDate(ChangeDateMsg.Substring(InStr(1, ChangeDateMsg, "Original Start Date:") + 19, 10)), Master.RIRESOURCES.CurrentLocale, "d")
        EmailDateMsg = EmailDateMsg & "  " & Master.RIRESOURCES.GetResourceValue("NewStartDate") & ": " & IP.Bids.Localization.DateTime.GetLocalizedDateTime(RI.SharedFunctions.CDateFromEnglishDate(ChangeDateMsg.Substring(InStr(1, ChangeDateMsg, "New Start Date:") + 14, 10)), Master.RIRESOURCES.CurrentLocale, "d")
        EmailDateMsg = EmailDateMsg & "  " & Master.RIRESOURCES.GetResourceValue("PrevEndDate") & ": " & IP.Bids.Localization.DateTime.GetLocalizedDateTime(RI.SharedFunctions.CDateFromEnglishDate(ChangeDateMsg.Substring(InStr(1, ChangeDateMsg, "Original End Date:") + 17, 10)), Master.RIRESOURCES.CurrentLocale, "d")
        EmailDateMsg = EmailDateMsg & "  " & Master.RIRESOURCES.GetResourceValue("NewEndDate") & ": " & IP.Bids.Localization.DateTime.GetLocalizedDateTime(RI.SharedFunctions.CDateFromEnglishDate(ChangeDateMsg.Substring(InStr(1, ChangeDateMsg, "New End Date:") + 12)), Master.RIRESOURCES.CurrentLocale, "d")
        url = urlHost & "EnterOutage.aspx?OutageNumber=" & currentOutage.OutageNumber
        Body.Append(RI.SharedFunctions.CleanString(additionalText, "<br>"))
        Body.Append("<h2>" & currentOutage.SiteName & "</H2>")
        Body.Append("</table><P><font size=2 face=Arial><B><U><A HREF=" & urlHost & "EnterOutage.aspx?OutageNumber=" & OutageNumber & ">" & OutageNumber & " - " & currentOutage.Title & "</A></B></U></FONT><BR>")

        If ChangeDateFlag = "Y" Then
            Body.Append("<br><B>" & Master.RIRESOURCES.GetResourceValue("OUTAGE DATES HAVE CHANGED") & ": " & "</B>" & EmailDateMsg) 'currentOutage.ChangeDateMsg)
            Body.Append("<br><br><B>" & Master.RIRESOURCES.GetResourceValue("Comments/Reason for Change in Outage Dates") & ": " & "</B>" & currentOutage.Comments)
            Body.Append("<br><br><B>" & Master.RIRESOURCES.GetResourceValue("Earliest Preparedness Assessment Date is Scheduled") & ":  " & "</B>" & currentOutage.AssessmentDate)
            strEmailAddress = currentOutage.CoordEmail & "," & currentOutage.TaskEmails & "," & currentOutage.ApproverEmails ' "CATHY.COX@IPAPER.COM" 
            subjectForNotificationList = Master.RIRESOURCES.GetResourceValue("Outage Date Change for") & ":   " & currentOutage.SiteName & "  " & currentOutage.Title & "  (#" & OutageNumber & ")"
        Else
            Body.Append("<br><B>" & Master.RIRESOURCES.GetResourceValue("OUTAGE DATES HAVE NOT CHANGED PENDING MOC APPROVAL") & ": " & "</B>" & EmailDateMsg) 'currentOutage.ChangeDateMsg)
            Body.Append("<br><br><font size=2 face=Arial><B><U><A HREF=" & urlMOCHost & "EnterMOC.aspx?MOCNumber=" & currentOutage.MocNumber & ">" & currentOutage.MocNumber & " - Link to MOC</A></B></U></FONT><BR>")
            Body.Append("<br><br><B>" & Master.RIRESOURCES.GetResourceValue("Comments/Reason for Change in Outage Dates") & ": " & "</B>" & currentOutage.Comments)
            Body.Append("<br><br><B>" & Master.RIRESOURCES.GetResourceValue("Earliest Preparedness Assessment Date is Scheduled") & ":  " & "</B>" & currentOutage.AssessmentDate)
            strEmailAddress = currentOutage.CoordEmail & "," & currentOutage.ApproverEmails
            subjectForNotificationList = Master.RIRESOURCES.GetResourceValue("Outage Date Change PENDING MOC Approval for") & ":   " & currentOutage.SiteName & "  " & currentOutage.Title & "  (#" & OutageNumber & ")"
        End If


        dt.Load(currentOutage.ContractorDT)
        If dt.Rows.Count > 0 And ChangeDateFlag = "Y" Then
            Body.Append("<br><br><B>" & Master.RIRESOURCES.GetResourceValue("Notify Contractors and Resources of Outage Date Changes As Appropriate") & "</B>")
            Body.Append("<TABLE border=1><TR valign=top><font size=2 face=Arial><B><TD width=20%>")
            Body.Append(Master.RIRESOURCES.GetResourceValue("Contractor") & "{1}" & "<TD>")
            Body.Append(Master.RIRESOURCES.GetResourceValue("StartDate") & "{1}" & "<TD>")
            Body.Append(Master.RIRESOURCES.GetResourceValue("EndDate") & "{1}")
            Body.Append("</B></TR>")

            For Each dr In dt.Rows
                strContractor = dr.Item("companyname").ToString
                strContractorStart = dr.Item("startdate").ToString
                strContractorEnd = dr.Item("enddate").ToString
                commonBody.Append("<BR><TR valign=top><font size=2 face=Arial>")
                commonBody.Append("{0}" & strContractor & "{1}")
                commonBody.Append("{0}" & strContractorStart.ToShortDateString & "{1}")
                commonBody.Append("{0}" & strContractorEnd.ToShortDateString & "{1}")
            Next
            Body.Append("</TR>")
        End If

        commonBody.Append("</ul>")
        Body.Append(commonBody.ToString)

        commonBody = New StringBuilder

        dt.Clear()
        dt.Load(currentOutage.ResourceDT)
        If dt.Rows.Count > 0 Then
            Body.Append("<TR valign=top><font size=2 face=Arial><B><TD width=20%>")
            Body.Append(Master.RIRESOURCES.GetResourceValue("Resource") & "{1}" & "<TD>")
            Body.Append(Master.RIRESOURCES.GetResourceValue("StartDate") & "{1}" & "<TD>")
            Body.Append(Master.RIRESOURCES.GetResourceValue("EndDate") & "{1}")
            Body.Append("</B></TR>")
            For Each dr In dt.Rows
                strPerson = dr.Item("person").ToString
                strResourceStart = dr.Item("startdate").ToString
                strResourceEnd = dr.Item("enddate").ToString
                commonBody.Append("<BR><TR valign=top><font size=2 face=Arial>")
                commonBody.Append("{0}" & strPerson & "{1}")
                commonBody.Append("{0}" & strResourceStart.ToShortDateString & "{1}")
                commonBody.Append("{0}" & strResourceEnd.ToShortDateString & "{1}")
            Next
            Body.Append("</TR>")
        End If

        'Body.Append("</TABLE>")
        Dim v_td As String() = {"<TD>", "</TD>"}

        commonBody.Append("</ul>")
        '******End Common Body ************

        Body.Append(commonBody.ToString)
        Dim strBody, strFooter As String

        strBody = String.Format(Body.ToString, v_td)
        strFooter = "</HTML></BODY>"
        strBody = "<P><font size =1 face=Arial><B>" & strBody.ToString & strFooter

        Body.Append("</body></html>")

        Dim msgForNotificationList As String = String.Format(Body.ToString, v_td)

        RI.SharedFunctions.SendEmail(strEmailAddress, userProfile.Email, subjectForNotificationList, msgForNotificationList, userProfile.FullName)

    End Sub
    Private Sub PopulateResources(ByVal outageNumber As Integer)
        Dim sqlOutage As String = String.Empty
        Dim dsAll As System.Data.DataSet = Nothing
        Dim ds As System.Data.DataSet = Nothing

        sqlOutage = String.Format("SELECT DISTINCT b.resourceseqid," & _
        " initcap(discipline)|| '-' ||  initcap(d.lastname) || ',' || initcap(d.firstname) as outageresource " & _
        " FROM  tbloutageresources b, refresources c, refemployee d " & _
        " where b.resourceseqid = c.resourceseqid and c.username = d.username and b.outagenumber = {0} " & _
        " order by 2 ", outageNumber)

        Dim sqlAllMinusSelected As String = String.Format("SELECT DISTINCT resourceseqid," & _
        " initcap(discipline)|| '-' ||  initcap(d.lastname) || ',' || initcap(d.firstname)  as outageresource " & _
        " FROM  refresources c, refemployee d WHERE c.username = d.username and resourceseqid NOT IN (select resourceseqid from " & _
        " tbloutageresources where outagenumber = {0}) ORDER BY 2", outageNumber)

        ds = RI.SharedFunctions.GetOracleDataSet(sqlOutage)
        dsAll = RI.SharedFunctions.GetOracleDataSet(sqlAllMinusSelected)

        Session.Add("OutageNumber", outageNumber)

        If dsAll IsNot Nothing Then
            With Me._slbResourceList
                .DataSource = dsAll.Tables(0).CreateDataReader()
                .DataTextField = "outageresource"
                .DataValueField = "resourceseqid"
                .SelectedDataSource = ds.Tables(0).CreateDataReader
                .SelectedDataTextField = "outageresource"
                .SelectedDataValueField = "resourceseqid"
                .DataBind()
            End With
            '_pnlResources.Visible = True
            _pnlResources.Style.Add("Display", "block")
            'Me._mpeAddResources.Show()
        End If
    End Sub
    Protected Sub _gvResources_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles _gvResources.RowDeleting
        clsCurrentOutage.DeleteOutageResource(currentOutage.OutageNumber, Me._gvResources.DataKeys.Item(e.RowIndex).Value.ToString)
        currentOutage = New clsCurrentOutage(currentOutage.OutageNumber)
    End Sub

    Protected Sub _gvResources_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles _gvResources.RowEditing
        Me._gvContractor.EditIndex = e.NewEditIndex
    End Sub

    Protected Sub _btnAddResource_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnAddResource.Click
        Dim selectedResource As String = Me._slbResourceList.SelectedValue

        If selectedResource IsNot Nothing AndAlso selectedResource.Length > 0 AndAlso currentOutage IsNot Nothing Then
            currentOutage.InsertOutageResources(currentOutage.OutageNumber, selectedResource)

            Response.Clear()
            Response.Redirect(Page.AppRelativeVirtualPath & "?OutageNumber=" & currentOutage.OutageNumber, True)
            Response.End()
        End If
    End Sub

    Protected Sub _lbBusinessUnit_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles _lbBusinessUnit.DataBound
        Dim riResources As IP.Bids.Localization.WebLocalization
        If HttpContext.Current.Request.Cookies("SelectedCulture") IsNot Nothing Then
            Dim lang As String = HttpContext.Current.Request.Cookies("SelectedCulture").Value
            Dim appID As String = ConfigurationManager.AppSettings("ResourceApplicationID")
            riResources = New IP.Bids.Localization.WebLocalization(lang, appID)
        Else
            riResources = New IP.Bids.Localization.WebLocalization()
        End If
        If riResources.CurrentLocale.ToUpper <> "EN-US" Then
            Dim i As Integer
            For i = 0 To Me._lbBusinessUnit.Items.Count - 1
                Dim dataValues As String() = Split(_lbBusinessUnit.Items(i).Text, "-")
                Dim newDataValue As String = String.Empty
                If dataValues.Length > 1 Then
                    Dim sb As New StringBuilder
                    For j As Integer = 0 To dataValues.Length - 1
                        sb.Append(riResources.GetResourceValue(dataValues(j).Trim))
                        If j < dataValues.Length - 1 Then
                            sb.Append("-")
                        End If
                    Next
                    newDataValue = sb.ToString
                End If
                Me._lbBusinessUnit.Items(i).Text = newDataValue
            Next
        End If
    End Sub

    Protected Sub _grvTasksPage_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles _grvTasksPage.DataBound
        'If _grvTasksPage.PageCount = 1 Then
        '    Me._lbNewTemplateName.Visible = "True"
        '    _cbSaveTemplate.Visible = "true"
        '    Me._tbTemplateName.Visible = "true"
        'Else
        '    Me._lbNewTemplateName.Visible = "False"
        '    _cbSaveTemplate.Visible = "false"
        '    Me._tbTemplateName.Visible = "false"
        'End If
    End Sub

    Protected Sub _grvTasksPage_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _grvTasksPage.RowDataBound
        Dim cbChecked As CheckBox = CType(e.Row.Cells(0).FindControl("_cbStatus"), CheckBox)
        Dim hfTaskRoleCnt As HiddenField = CType(e.Row.Cells(0).FindControl("_hfRoleCount"), HiddenField)
        Dim hfTaskRoleSeq As HiddenField = CType(e.Row.Cells(0).FindControl("_hfRoleSeqId"), HiddenField)
        Dim hfTaskItemSeqID As HiddenField = CType(e.Row.Cells(0).FindControl("_hfTaskItemSeqID"), HiddenField)
        Dim ddlTemplateRole As DropDownList = CType(e.Row.Cells(0).FindControl("_ddlTemplateRole"), DropDownList)
        Dim ddlTemplateRole2 As DropDownList = CType(e.Row.Cells(0).FindControl("_ddlTemplateRole2"), DropDownList)
        Dim ddlTemplateRole3 As DropDownList = CType(e.Row.Cells(0).FindControl("_ddlTemplateRole3"), DropDownList)
        Dim ddlTemplateRole4 As DropDownList = CType(e.Row.Cells(0).FindControl("_ddlTemplateRole4"), DropDownList)
        Dim ddlTemplateRole5 As DropDownList = CType(e.Row.Cells(0).FindControl("_ddlTemplateRole5"), DropDownList)

        Dim strRoleSeq As String()
        Dim intGridRowCount As Integer = _grvTasksPage.Rows.Count

        If e.Row.RowIndex >= 0 Then
            Dim strTaskItemSeq As String
            strTaskItemSeq = hfTaskItemSeqID.Value
            Dim strTaskRoleCnt As String = hfTaskRoleCnt.Value
            Dim strTaskRoleSeq As String = hfTaskRoleSeq.Value

            Dim strTaskItemSeqID As String = strTaskItemSeq '"1408261"
            Dim strTaskHeaderSeqID As String = Me._ddlTemplateTasks.SelectedValue '"21016"
            If strTaskHeaderSeqID = Nothing Then
                strTaskHeaderSeqID = currentOutage.MTTTemplateID
            End If
            Dim role As Integer
            If strTaskRoleSeq <> "" Then
                strRoleSeq = Split(strTaskRoleSeq, ",")
                Dim i As Integer
                If strRoleSeq.Length = 1 Then
                    For i = 0 To _ddlHiddenRoles.Items.Count - 1
                        ddlTemplateRole.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                    Next
                    role = strRoleSeq(0)
                    ddlTemplateRole.SelectedValue = role
                ElseIf strRoleSeq.Length = 2 Then
                    For i = 0 To _ddlHiddenRoles.Items.Count - 1
                        ddlTemplateRole.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                        ddlTemplateRole2.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                    Next
                    role = strRoleSeq(0)
                    ddlTemplateRole.SelectedValue = role
                    role = strRoleSeq(1)
                    ddlTemplateRole2.SelectedValue = role
                ElseIf strRoleSeq.Length = 3 Then
                    For i = 0 To _ddlHiddenRoles.Items.Count - 1
                        ddlTemplateRole.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                        ddlTemplateRole2.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                        ddlTemplateRole3.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                    Next
                    role = strRoleSeq(0)
                    ddlTemplateRole.SelectedValue = role
                    role = strRoleSeq(1)
                    ddlTemplateRole2.SelectedValue = role
                    role = strRoleSeq(2)
                    ddlTemplateRole3.SelectedValue = role
                ElseIf strRoleSeq.Length = 4 Then
                    For i = 0 To _ddlHiddenRoles.Items.Count - 1
                        ddlTemplateRole.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                        ddlTemplateRole2.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                        ddlTemplateRole3.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                        ddlTemplateRole4.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                    Next
                    role = strRoleSeq(0)
                    ddlTemplateRole.SelectedValue = role
                    role = strRoleSeq(1)
                    ddlTemplateRole2.SelectedValue = role
                    role = strRoleSeq(2)
                    ddlTemplateRole3.SelectedValue = role
                    role = strRoleSeq(3)
                    ddlTemplateRole4.SelectedValue = role
                ElseIf strRoleSeq.Length = 5 Then
                    For i = 0 To _ddlHiddenRoles.Items.Count - 1
                        ddlTemplateRole.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                        ddlTemplateRole2.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                        ddlTemplateRole3.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                        ddlTemplateRole4.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                        ddlTemplateRole5.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                    Next
                    role = strRoleSeq(0)
                    ddlTemplateRole.SelectedValue = role
                    role = strRoleSeq(1)
                    ddlTemplateRole2.SelectedValue = role
                    role = strRoleSeq(2)
                    ddlTemplateRole3.SelectedValue = role
                    role = strRoleSeq(3)
                    ddlTemplateRole4.SelectedValue = role
                    role = strRoleSeq(4)
                    ddlTemplateRole5.SelectedValue = role
                End If

            End If

            If ddlTemplateRole IsNot Nothing Then
                ddlTemplateRole.Attributes.Add("onfocus", "copyDDL('" & _ddlHiddenRoles.ClientID & "','" & ddlTemplateRole.ClientID & "');javascript: this.style.width='auto';")
                ddlTemplateRole.Attributes.Add("onblur", "javascript: this.style.width='100%';")
            End If
            If ddlTemplateRole2 IsNot Nothing Then
                ddlTemplateRole2.Attributes.Add("onfocus", "copyDDL('" & _ddlHiddenRoles.ClientID & "','" & ddlTemplateRole2.ClientID & "');javascript: this.style.width='auto';")
                ddlTemplateRole2.Attributes.Add("onblur", "javascript: this.style.width='100%';")
            End If
            If ddlTemplateRole3 IsNot Nothing Then
                ddlTemplateRole3.Attributes.Add("onfocus", "copyDDL('" & _ddlHiddenRoles.ClientID & "','" & ddlTemplateRole3.ClientID & "');javascript: this.style.width='auto';")
                ddlTemplateRole3.Attributes.Add("onblur", "javascript: this.style.width='100%';")
            End If
            If ddlTemplateRole4 IsNot Nothing Then
                ddlTemplateRole4.Attributes.Add("onfocus", "copyDDL('" & _ddlHiddenRoles.ClientID & "','" & ddlTemplateRole4.ClientID & "');javascript: this.style.width='auto';")
                ddlTemplateRole4.Attributes.Add("onblur", "javascript: this.style.width='100%';")
            End If
            If ddlTemplateRole5 IsNot Nothing Then
                ddlTemplateRole5.Attributes.Add("onfocus", "copyDDL('" & _ddlHiddenRoles.ClientID & "','" & ddlTemplateRole5.ClientID & "');javascript: this.style.width='auto';")
                ddlTemplateRole5.Attributes.Add("onblur", "javascript: this.style.width='100%';")
            End If

            Dim sqlOutage As String = String.Empty
            Dim dsAll As System.Data.DataSet = Nothing
            Dim ds As System.Data.DataSet = Nothing

            sqlOutage = String.Format("SELECT templatetaskseqid, roleseqids " & _
            " FROM tbloutagemtttemplatetasks " & _
            "WHERE outagenumber = {0} " & _
            "and templatetaskseqid = {1} " & _
            "and processed = 'Y'", currentOutage.OutageNumber, strTaskItemSeqID)

            ds = RI.SharedFunctions.GetOracleDataSet(sqlOutage)
            Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader

            If dr.HasRows Then
                e.Row.Enabled = False
                _btnCreateAll.Visible = False
                ds = Nothing
            End If

        End If
    End Sub

    Protected Sub _grvTasksPage_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles _grvTasksPage.PageIndexChanging
        GetTemplateTasks()

        _grvTasksPage.PageIndex = e.NewPageIndex
        _grvTasksPage.DataBind()

        'If _grvTasksPage.PageCount = 1 Or e.NewPageIndex + 1 = _grvTasksPage.PageCount Then
        '    Me._lbNewTemplateName.Visible = "True"
        '    _cbSaveTemplate.Visible = "true"
        '    Me._tbTemplateName.Visible = "true"
        'Else
        '    Me._lbNewTemplateName.Visible = "False"
        '    _cbSaveTemplate.Visible = "false"
        '    Me._tbTemplateName.Visible = "false"

        'End If

        _lblTaskStatus.Text = ""

    End Sub

    Protected Sub _grvTasksPage_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _grvTasksPage.RowCreated
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim control As New LiteralControl()
            'new control  
            control.Text = ("Pages ")
            ' add text  
            Dim table As Table = TryCast(e.Row.Cells(0).Controls(0), Table)
            ' get the pager table  
            Dim newCell As New TableCell()
            'Create new cell  
            newCell.Controls.AddAt(0, control)
            'Add contol   
            table.Rows(0).Cells.AddAt(0, newCell)
        End If
    End Sub

    Protected Sub _btnCreateAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnCreateAll.Click
        currentOutage = New clsCurrentOutage(Request.QueryString("OutageNumber"))
        Dim returns As String
        returns = currentOutage.SaveOutageEntireTemplate(currentOutage.OutageNumber, Me._ddlTemplateTasks.SelectedValue)

        If returns <> "0" Then
            Me._lblTaskStatus.Text = "Error Occurred Creating Template Tasks"
        Else
            Dim return2 As String
            return2 = currentOutage.ReplicateOutageTasks(Me._ddlTemplateTasks.SelectedValue, currentOutage.OutageNumber)
            If return2 <> "0" Then
                Me._lblTemplateTaskMsg.Text = "Error Occurred Creating Template Tasks"
            End If

            GetAndSendOutageTemplateEmail(currentOutage.OutageNumber, userProfile.Email)

            If currentOutage.OutageNumber.Length > 0 Then
                Response.Redirect(Page.AppRelativeVirtualPath & "?OutageNumber=" & currentOutage.OutageNumber, True)
            End If
        End If

    End Sub

    Protected Sub _mpeTemplates_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles _mpeTemplates.Load
        Dim strTemplateName As String = _ddlTemplateTasks.SelectedItem.Text
        If InStr(strTemplateName, "Turbine Generator") > 0 Then
            _grvTasksPage.Columns.Item(0).Visible = False
            _grvTasksPage.Columns.Item(1).ItemStyle.Width = 150
            _grvTasksPage.Columns.Item(2).ItemStyle.Width = 50
            _grvTasksPage.Columns.Item(3).ItemStyle.Width = 50
            _grvTasksPage.Columns.Item(4).ItemStyle.Width = 150
            _grvTasksPage.Columns.Item(6).Visible = False
            _grvTasksPage.Columns.Item(7).Visible = False
            _grvTasksPage.Columns.Item(8).Visible = False
            _grvTasksPage.Columns.Item(9).Visible = False
            _grvTasksPage.Columns.Item(5).Visible = False
            Me._btnCreate.Visible = False
            Me._divNewTemplate.Visible = False
            Me._lblTemplateHeading.Text = "These are the tasks associated with the selected template.<br>Click Create All Tasks to create the tasks associated with this template. Click Close to return to Outage page."
        End If

    End Sub

    Protected Sub SaveOutageComment(Optional ByVal refreshPage As Boolean = True)
        If Page.IsValid Then
            Dim returnStatus As String = ""
            currentOutage = New clsCurrentOutage(Request.QueryString("OutageNumber"))

            With currentOutage
                .OutageNumber = Request.QueryString("OutageNumber")
                'Dim outagechangeflag As String
                If Me._tbDateChangeComments.Text Is Nothing Or Me._tbDateChangeComments.Text = "" Then
                    .Comments = RI.SharedFunctions.DataClean(Me._txtComments.Text)
                Else
                    .Comments = RI.SharedFunctions.DataClean(Me._txtComments.Text) & " " & RI.SharedFunctions.DataClean(Me._tbDateChangeComments.Text)
                End If

                returnStatus = .SaveOutageComment()

                If refreshPage = True Then
                    EmailDateChanges(_hfDateComment.Value, _hfDateChangeFlag.Value)
                    Response.Redirect(Page.AppRelativeVirtualPath & "?OutageNumber=" & .OutageNumber, True)
                End If

            End With

        End If

    End Sub
    'Protected Sub _ddlHiddenRoles_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlHiddenRoles.DataBound
    '    RI.SharedFunctions.LocalizeValue(Me._ddlHiddenRoles.SelectedValue)
    'End Sub
End Class
