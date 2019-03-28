Imports Devart.Data.Oracle

' A Albrinck 5/2014 Added code for new analysis workspace tables.

Partial Class RI_EnterNewRI
    Inherits RIBasePage


    Dim enterRI As clsEnterNewRI
    Dim currentRI As clsCurrentRI
    Dim selectedFacility As String = String.Empty
    Dim selectedBusArea As String = String.Empty
    Dim selectedLine As String = String.Empty
    Dim userProfile As RI.CurrentUserProfile = Nothing
    Dim OriginalRCFALeader As String = String.Empty


    Private Sub DisplayUpdateMenu()
        Dim ret As String = "return false;"
        Dim urlhost As String
        Me._pnlUpdateButtons.Visible = True
        'Me._btnAnalysisWorkspace.OnClientClick = Master.GetPopupWindowJS("~/ri/Analysisworkspace.aspx?RINumber=" & currentRI.RINumber & "&SiteId=" & currentRI.SiteID, , 800, 600, True, False, True) & ret
        Dim confirmMessage As String = "localizedText.ConfirmRedirect" 'Master.RIRESOURCES.GetResourceValue("YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES")
        'confirmMessage = confirmMessage.Replace("'", "|")
        'If the old analysis workspace was already entered, continue to use those pages.  Otherwise, go to new analysis page.
        If currentRI.WorkspaceCount > 0 Then
            Me._btnAnalysisWorkspace.OnClientClick = "Javascript:viewPopUp('Analysisworkspace.aspx?RINumber=" & currentRI.RINumber & "'," & confirmMessage & ",'aw');return false"
        Else
            Me._btnAnalysisWorkspace.OnClientClick = "Javascript:viewPopUp('AWFailureEvent.aspx?RINumber=" & currentRI.RINumber & "&awcount=" & currentRI.FailureEventCount & "'," & confirmMessage & ",'aw');return false"
        End If
        Me._btnAttachments.OnClientClick = "Javascript:viewPopUp('FileUpload.aspx?RINumber=" & currentRI.RINumber & "'," & confirmMessage & ",'fu');return false"
        'Me._btnActionItems.OnClientClick = "Javascript:viewPopUp('CorrectiveActionItems.aspx?RINumber=" & currentRI.RINumber & " &SiteId=" & currentRI.SiteID & "'," & confirmMessage & ",'ai');return false"

        If Request.UserHostAddress = "127.0.0.1" Then
            ' urlhost = "Http://ridev.ipaper.com/"
            urlhost = "http://gpitasktracker.graphicpkg.com/"
        Else
            If Request.ServerVariables("HTTP_HOST").ToLower.Contains("ridev") Then
                'urlhost = "Http://ridev.ipaper.com/"
                urlhost = "http://gpitasktracker.graphicpkg.com/"
            ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritest") Then
                'urlhost = "Http://ritest.ipaper.com/"
                urlhost = "http://gpitasktracker.graphicpkg.com/"
            ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritrain") Then
                'urlhost = "Http://ritrain.ipaper.com/"
                urlhost = "http://gpitasktracker.graphicpkg.com/"
            Else
                'urlhost = "Http://rigpi.ipaper.com/"
                urlhost = "http://gpitasktracker.graphicpkg.com/"
            End If
        End If

        If currentRI.TaskHeaderSeqId Is Nothing Or currentRI.TaskHeaderSeqId = "" Then
            Me._btnActionItems.OnClientClick = "Javascript:CreateTaskHeader('" & currentRI.RINumber & "','" & userProfile.Username & "','" & currentRI.CreationDate & "');return false"
        Else
            'Me._btnActionItems.OnClientClick = "Javascript:viewPopUp('" & urlhost & "TaskTracker/TaskDetails.aspx?HeaderNumber=" & currentRI.TaskHeaderSeqId & "&TaskNumber=0&RefSite=RI'," & confirmMessage & ",'MTT');return false"
            Me._btnActionItems.OnClientClick = "Javascript:viewPopUp('" & urlhost & "/Popups/TaskList.aspx?HeaderNumber=" & currentRI.TaskHeaderSeqId & "&RefSite=RI&InFrame=false&AllowEdit=true&ShowHeaderInfo=false'," & confirmMessage & ",'MTT');return false"
        End If

        If currentRI.Verification = "Yes" And currentRI.VerificationDueDate IsNot Nothing And currentRI.VerificationDueDate <> "" Then
            Me._btnUpdateVerifyTask.Visible = "True"
            Me._btnUpdateVerifyTask.OnClientClick = "Javascript:viewPopUp('" & urlhost & "/TaskDetails.aspx?HeaderNumber=" & currentRI.VerTaskHeader & "&TaskNumber=" & currentRI.VerTaskItem & "&refsite=RI&InFrame=false&AllowEdit=true&ShowHeaderInfo=false'," & confirmMessage & ",'MTT');return false"
        Else
            Me._btnUpdateVerifyTask.Visible = "False"
        End If
        '_btnCauses.OnClientClick = "Javascript:ScrollControlInView('" & _lblDocumentCauses.ClientID & "');" & _lblDocumentCauses.ClientID & ".click();return false"
        'Page.ResolveClientUrl()
        ' <a target="_top" href="Javascript:viewIncident('<%#string.format(Page.ResolveClientUrl("~/RI/EnterNewRI.aspx?RINumber={0}"),EVAL("RINumber")) %>');">
        '	<%#Eval("Incident")%>
        'Me._btnAttachments.OnClientClick = Master.GetPopupWindowJS("~/ri/FileUpload.aspx?RINumber=" & currentRI.RINumber, , 773, 400, True) & ret
        'Me._btnActionItems.OnClientClick = Master.GetPopupWindowJS("~/ri/CorrectiveActionItems.aspx?RINumber=" & currentRI.RINumber & " &SiteId=" & currentRI.SiteID, , 773, 400, True, False, True) & ret
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            userProfile = RI.SharedFunctions.GetUserProfile
            RI.SharedFunctions.DisablePageCache(Response)

            DisplayViewSearch()

            Dim sc As ScriptManager
            sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
            If sc IsNot Nothing Then
                Dim loService As New ServiceReference
                loService.InlineScript = False
                loService.Path = "~/ri/FunctionalLocation.asmx"
                sc.Services.Add(loService)

                Dim calService As New ServiceReference
                calService.InlineScript = False
                calService.Path = "~/CalculateDowntime.asmx"
                sc.Services.Add(calService)

                Dim MTTService As New ServiceReference
                MTTService.InlineScript = False
                MTTService.Path = "~/MOC/TaskTracker.asmx"
                sc.Services.Add(MTTService)

                'Dim MTTService As New ServiceReference '.TaskTracker
                'MTTService.InlineScript = False
                'MTTService.Path = "http://ridev.ipaper.com/tasktracker/webservices/TaskTracker.asmx"
                'sc.Services.Add(MTTService)
            End If

            'If Not Page.ClientScript.IsStartupScriptRegistered(Page.GetType, "GetGlobalJSVar") Then
            '    Page.ClientScript.RegisterStartupScript(Page.GetType, "GetGlobalJSVar", GetGlobalJSVar, True)
            'End If
            ScriptManager.RegisterStartupScript(Me._udpLocation, _udpLocation.GetType, "GetGlobalJSVar", GetGlobalJSVar, True)
            'If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "DHTMLWindow") Then
            '    Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "DHTMLWindow", Page.ResolveClientUrl("~/windowfiles/dhtmlwindow.js"))
            'End If
            'ScriptManager.RegisterClientScriptInclude(Me._udpLocation, _udpLocation.GetType, "DHTMLWindow", Page.ResolveClientUrl("~/windowfiles/dhtmlwindow.js"))
            'If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "ModalWindow") Then
            '    Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "ModalWindow", Page.ResolveClientUrl("~/modalfiles/modal.js"))
            'End If
            'ScriptManager.RegisterClientScriptInclude(Me._udpLocation, _udpLocation.GetType, "ModalWindow", Page.ResolveClientUrl("~/modalfiles/modal.js"))
            'If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "EnterNewRI") Then
            '    Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "EnterNewRI", Page.ResolveClientUrl("~/ri/EnterNewRI.js"))
            'End If
            ScriptManager.RegisterClientScriptInclude(Me._udpLocation, _udpLocation.GetType, "EnterNewRI", Page.ResolveClientUrl("~/ri/EnterNewRI.js"))
            Me._hypRepairCostDefinition.NavigateUrl = "javascript:var x=dhtmlmodal.open('RepairCostDefinition', 'div', '_divRepairCostDefinition', '" & Master.RIRESOURCES.GetResourceValue("Cost Definition", False, "Shared") & "', 'width=400px,height=250px,center=1,resize=0,scrolling=1');"
            'Me._hypRepairCostDefinition.NavigateUrl = "javascript:var x=dhtmlmodal.open('RepairCostDefinition', 'div', '_divRepairCostDefinition', 'Cost Definition', 'width=400px,height=250px,center=1,resize=0,scrolling=1');"

            Me._hypFinancialImpactDefinition.NavigateUrl = "javascript:var x=dhtmlmodal.open('FinancialImpactDefinition', 'div', '_divFinancialImpactDefinition', '" & Master.RIRESOURCES.GetResourceValue("Total Financial Impact Definition", False, "Shared") & "', 'width=500px,height=250px,center=1,resize=0,scrolling=1');"
            Me._hypDefLatentCauses.NavigateUrl = "javascript:var x=dhtmlmodal.open('LatentDefinition', 'div', '_divLatentDef', '" & Master.RIRESOURCES.GetResourceValue("Latent Definition", False, "Shared") & "', 'width=800px,height=400px,center=1,resize=0,scrolling=1');"
            Me._hypPhysicalDef.NavigateUrl = "javascript:var x=dhtmlmodal.open('PhysicalDefinition', 'div', '_divPhysicalDef', '" & Master.RIRESOURCES.GetResourceValue("Physical Definition", False, "Shared") & "', 'width=800px,height=500px,center=1,resize=0,scrolling=1');"
            'Master.UnloadEvents = False
            'RI.SharedFunctions.DisablePageCache(Response)
            'Human Causes definition hyperlink  klf 6/2010
            Dim iploc As New IP.Bids.Localization.WebLocalization
            Dim popupJS As String = "Javascript:displayModalPopUpWindow('{0}','{1}','{2}','{3}','{4}');"
            Me._hypDefHumanCauses.NavigateUrl = String.Format(popupJS, Page.ResolveUrl("~/ri/files/ManagingHumCause" & iploc.CurrentLocale & ".pdf#zoom=80%"), "HC", "Managing Human Cause", "900", "725")

            'Added default=No for now 07/10/14 CAC
            'Me._rblVerification.Items.FindByValue("No").Selected = True

            If Request.QueryString("RINumber") IsNot Nothing And IsNumeric(Request.QueryString("RINumber")) Then
                currentRI = New clsCurrentRI(Request.QueryString("RINumber"))
                If currentRI.SiteID Is Nothing Then currentRI = Nothing
                If currentRI IsNot Nothing Then
                    enterRI = New clsEnterNewRI(userProfile.Username, currentRI.SiteID, userProfile.InActiveFlag, , currentRI.BusinessUnit, currentRI.Linebreak, Request.QueryString("RINumber"))

                    If Page.IsPostBack Then
                        selectedFacility = Me._ddlFacility.SelectedValue
                        selectedBusArea = Me._ddlBusinessUnit.SelectedValue
                        selectedLine = Me._ddlLineBreak.SelectedValue
                    Else
                        selectedFacility = currentRI.SiteID
                        selectedBusArea = currentRI.BusinessUnit
                        selectedLine = currentRI.Linebreak
                    End If
                    SetupFunctionalLocation()
                    OriginalRCFALeader = currentRI.RCFALeader
                Else
                    'Me._messageBox.Message = Request.QueryString("RINumber") & " is not a valid RINumber"
                    'Me._messageBox.Title = "Invalid RINumber"
                    'Me._messageBox.ButtonType = ucMessageBox.ButtonTypes.OK
                    'Me._messageBox.ShowMessage()
                    Response.Clear()
                    Response.Redirect("~/ri/EnterNewri.aspx", True)
                    Response.End()
                End If

                If Request.QueryString("ChangeAnalysis") IsNot Nothing Then
                    If currentRI IsNot Nothing Then
                        Dim IsRCFAInProgress As Boolean
                        Dim rcfaChangeMsg As String = String.Empty
                        If currentRI.RCFAAnalysisCompDate.Length = 0 Then
                            IsRCFAInProgress = True
                        End If
                        If IsRCFAInProgress Then ' RCFA State has been changed to In Progress
                            rcfaChangeMsg = Master.RIRESOURCES.GetResourceValue("RCFA state has already been changed to In Progress")
                            Me._txtChangeAnalysisComments.Visible = False
                            Me._btnChangeAnalysisOK.Visible = False
                            Me._btnChangeAnalysisCancel.Text = "OK"
                            Me.SpellCheckChangeAnalysis.Visible = False
                        Else
                            rcfaChangeMsg = Master.RIRESOURCES.GetResourceValue("Enter comments that will be sent via email to RCFA Leader and will set the analysis to In Progress state.")
                            Me._txtChangeAnalysisComments.Visible = True
                        End If
                        Me._lblChangeAnalysiMsg.Text = rcfaChangeMsg
                        _mpeChangeAnalysis.Show()
                    End If
                End If
            Else ' New RI
                If Page.IsPostBack Then
                    selectedFacility = Me._ddlFacility.SelectedValue
                    selectedBusArea = Me._ddlBusinessUnit.SelectedValue
                    selectedLine = Me._ddlLineBreak.SelectedValue
                Else
                    selectedFacility = userProfile.DefaultFacility
                    Dim englishDate As String = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Now, "EN-US", "d")
                    Me._startEndCal.StartDate = englishDate
                    Me._startEndCal.EndDate = englishDate
                    Me._rblVerification.Items.FindByValue("No").Selected = True
                End If


                enterRI = New clsEnterNewRI(userProfile.Username, selectedFacility, userProfile.InActiveFlag, userProfile.DefaultDivision, selectedBusArea, selectedLine)
                SetupFunctionalLocation()
                If Me._functionalLocationTree.Text.Length = 0 Then
                    Me._functionalLocationTree.PopulateFunctionalLocation(selectedFacility)
                End If

                Me._lblCreatedBy.Text = String.Format(Master.RIRESOURCES.GetResourceValue("CreatedBy", True, "Shared"), Master.RIRESOURCES.GetResourceValue("None", True, "Shared"))
                Me._lblCreatedDate.Text = String.Format(Master.RIRESOURCES.GetResourceValue("CreationDate", True, "Shared"), Master.RIRESOURCES.GetResourceValue("None", True, "Shared"))
                Me._lblLastUpdateDate.Text = String.Format(Master.RIRESOURCES.GetResourceValue("LastUpdateDate", True, "Shared"), Master.RIRESOURCES.GetResourceValue("None", True, "Shared"))
                Me._lblUpdatedBy.Text = String.Format(Master.RIRESOURCES.GetResourceValue("LastUpdatedBy", True, "Shared"), Master.RIRESOURCES.GetResourceValue("None", True, "Shared"))
            End If

            If enterRI IsNot Nothing Then
                If enterRI.CurrentPageMode = clsEnterNewRI.PageMode.NewRI Then
                    Master.SetBanner(Master.RIRESOURCES.GetResourceValue("EnterNewIncident", True, "Shared"))
                    _btnSubmit.Text = Master.RIRESOURCES.GetResourceValue("SaveNewReliabilityIncident", True, "Shared")
                Else
                    Master.SetBanner(Master.RIRESOURCES.GetResourceValue("UpdateIncident", True, "Shared") & " '" & currentRI.RINumber & "'")
                    _btnSubmit.Text = Master.RIRESOURCES.GetResourceValue("SaveReliabilityIncident", True, "Shared") '"Save Reliability Incident"
                    'Master.DeselectMenu()
                    'im newMenuCol As New MenuItemCollection
                    'ewMenuCol.Remove("Enter New RI")
                    DisplayUpdateMenu()
                End If
                Dim lookUp As New FunctionalLocationLookup
                Dim AssignLeader As String = lookUp.GetNotificationType(selectedFacility, selectedBusArea, selectedLine, userProfile.Username)
                lookUp = Nothing

                If AssignLeader.Length > 0 Then
                    Me._ddlAnalysisLead.Visible = True
                    Me._lblCommentsForEmail.Visible = True
                    Me._txtForEmail.Visible = True
                    Me._txtAnalysisLead.Visible = True
                    Me._txtAnalysisLead.Style.Add("display", "none")
                    Me._ddlAnalysisLead.Style.Add("display", "inline")
                Else
                    Me._ddlAnalysisLead.Visible = True
                    Me._ddlAnalysisLead.Style.Add("display", "none")
                    Me._txtAnalysisLead.Style.Add("display", "inline")
                    _txtAnalysisLead.Visible = True
                    'Me._ddlAnalysisLead.CssClass = "Disabled"
                    Me._ddlAnalysisLead.Items.Clear()
                    Me._lblCommentsForEmail.Visible = True
                    Me._lblCommentsForEmail.Style.Add("display", "none")
                    Me._txtForEmail.Visible = True
                    Me._txtForEmail.Style.Add("display", "none")
                End If
            End If
            _btnSubmit.OnClientClick = "ConfirmBeforeLeave=false;"
            Me._btnMarkAnalysisComplete.OnClientClick = "ConfirmBeforeLeave=false;"
            Me._btnDelete.OnClientClick = "ConfirmBeforeLeave=false;"
            If currentRI IsNot Nothing Then

                If currentRI.PPR = "Yes" Then
                    'Me._ddlFacility.Enabled = False
                    ''Me._ddlBusinessUnit.Visible = False
                    'Me._ddlBusinessUnit.Enabled = False
                    'Me._ddlLineBreak.Enabled = False
                    'Me._ddlCrew.Enabled = False
                    'Me._ddlShift.Enabled = False
                    'Me._ddlAnalysisLead.Enabled = False
                    'Me._ddlTrigger.Enabled = False
                    'Me._ddlFailedMaterial.Enabled = False
                    Me._txtDownTime.Enabled = False
                    Me._startEndCal.Enabled = False
                    Me._btnCalculateDowntime.Enabled = False
                    Me._btnDelete.Enabled = False
                    _locationRow.Disabled = True
                End If

                Me._btnHistory.RINumber = currentRI.RINumber
                Dim urlPath As String
                If Request.UserHostAddress = "127.0.0.1" Or Request.UserHostAddress = "http://s29edev13/riajax" Then
                    urlPath = "http://ridev.ipaper.com/CEReporting/"
                Else
                    urlPath = "http://gpimv.graphicpkg.com/cereporting/"
                    'urlPath = "../../CEReporting/"
                End If
                'CAC 0910/13 Updated link for BI4 upgrade
                'Dim executiveSummaryURL As String = String.Format(urlPath & "frmCrystalReport.aspx?Report=ExecutiveSummary&RINumber={0}&Localename=" & Master.RIRESOURCES.CurrentLocale, currentRI.RINumber)
                Dim executiveSummaryURL As String = String.Format(urlPath & "CrystalReportDisplay.aspx?Report=ExecutiveSummary&RINumber={0}&Localename=" & Master.RIRESOURCES.CurrentLocale, currentRI.RINumber)
                Me._btnExecutiveSummary.OnClientClick = Master.GetPopupWindowJS(executiveSummaryURL, "ExecutiveSummary", 600, 300, True, True, True) & ";return false;"
            End If
            'Failure Classification Defaults        
            'If Me._txtTotalClassification.Text.Length = 0 Then
            '    SetDefaultFailureClassification()
            'Else
            '    CalculateFailureClassification()
            'End If

            If userProfile.DistinguishedName.ToUpper.Contains("LOVELAND") Or userProfile.DistinguishedName.ToUpper.Contains("MEMPHIS") Or userProfile.DefaultFacility.ToUpper = "AL" Or userProfile.DefaultFacility.ToUpper = "TY" Then 'Global Technology group = TY 10/7/2013
                _btnSpell.Visible = True
                _btnSubmit.Visible = True
            ElseIf userProfile.DefaultFacility = selectedFacility Then
                _btnSpell.Visible = True
                _btnSubmit.Visible = True
                Me._cddlFacility.ContextKey = selectedFacility
            Else
                _btnSpell.Attributes.CssStyle.Add("display", "none")
                _btnSubmit.Style.Add("display", "none")
                Me._cddlFacility.ContextKey = selectedFacility '& "," & userProfile.DefaultFacility
            End If
            If userProfile.DivestedLocation = True Then
                _btnSpell.Visible = False
                _btnSubmit.Visible = False
            End If
            _btnCauses.OnClientClick = "Javascript:ScrollControlInView('" & _lblDocumentCauses.ClientID & "');" & _lblDocumentCauses.ClientID & ".click();return false"

            _udpUpdateMenu.Update()

            _cddlFacility.LoadingText = " ... "
            _cddlBusArea.LoadingText = " ... "
            _cddlLineBreak.LoadingText = " ... "
            _cddlAnalysisLead.LoadingText = " ... "
            _cddlTrigger.LoadingText = " ... "
            _cddlCrew.LoadingText = " ... "
            _cddlShift.LoadingText = " ... "
            _cddlFailedMaterial.LoadingText = " ... "

            Dim ci As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentCulture
            Dim validChars As String = Join(ci.NumberFormat.NativeDigits, "") & ci.NumberFormat.NumberDecimalSeparator

            FilteredTextBoxExtender3.ValidChars = validChars
            FilteredTextBoxExtender3.FilterInterval = 1
            _revDowntime.ValidationExpression = String.Format("^-?\d*(\{0}\d+)?$", ci.NumberFormat.NumberDecimalSeparator)
            _revDowntime.ErrorMessage = Master.RIRESOURCES.GetResourceValue("ValidDowntime", False, "Shared")
            _cpeDocumentCauses.CollapsedText = Master.RIRESOURCES.GetResourceValue("ShowDocumentCauses", True, "Shared")
            _cpeDocumentCauses.ExpandedText = Master.RIRESOURCES.GetResourceValue("HideDocumentCauses", True, "Shared")
            '_imgClassCategory.ImageUrl = Page.ResolveUrl("~/images/ClassCategory_" & Master.RIRESOURCES.CurrentLocale & ".gif")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetUser() As String
        If userProfile IsNot Nothing Then
            Return userProfile.Username
        Else
            Return ""
        End If
    End Function
    Private Sub SetupFunctionalLocation()
        With _functionalLocationTree
            If selectedBusArea.Length > 0 Then
                Dim splitBusArea As String()
                splitBusArea = Split(selectedBusArea, ",")
                If splitBusArea.Length = 2 Then
                    .BusinessUnit = splitBusArea(0)
                    .Area = splitBusArea(1)
                ElseIf splitBusArea.Length = 1 Then
                    .BusinessUnit = splitBusArea(0)
                    .Area = String.Empty
                End If
            End If
            If selectedLine.Length > 0 Then
                Dim splitLine As String()
                splitLine = Split(selectedLine, ",")
                If splitLine.Length >= 1 Then
                    .Line = splitLine(0)
                End If
            End If
            .Facility = selectedFacility
            .SetContextKey()
        End With
    End Sub

    Private Sub DisplayViewSearch()
        Dim dr As OracleDataReader = Nothing
        Try
            Dim clsSearch As clsViewSearch = CType(Session("clsSearch"), clsViewSearch)
            If clsSearch IsNot Nothing Then
                Me._btnViewSearch.Visible = True
                Me._btnViewSearch.OnClientClick = "displayViewListWindow('" & Page.ResolveClientUrl("~/RI/ViewList.aspx") & "','" & Master.RIRESOURCES.GetResourceValue("ViewList", False, "Shared") & "'); return false;"
            End If
        Catch ex As Exception

        Finally
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
        End Try
    End Sub
    'Private Sub SetDefaultFailureClassification()
    '    'Failure Classification Defaults
    '    Me._rblClassChronic.SelectedIndex = 1
    '    Me._rblClassCost.SelectedIndex = 1
    '    'Me._rblClassDisplay.SelectedIndex = 1
    '    Me._rblClassLife.SelectedIndex = 1
    '    Me._rblClassPlanned.SelectedIndex = 1
    '    Me._rblClassRepair.SelectedIndex = 1

    '    CalculateFailureClassification()
    'End Sub

    'Private Sub CalculateFailureClassification()
    '    Dim sb As New StringBuilder
    '    sb.Append("CalculateClass('")
    '    sb.Append(Me._rblClassCost.ClientID)
    '    sb.Append("','")
    '    sb.Append(Me._rblClassLife.ClientID)
    '    sb.Append("','")
    '    sb.Append(Me._rblClassPlanned.ClientID)
    '    sb.Append("','")
    '    sb.Append(Me._rblClassRepair.ClientID)
    '    sb.Append("','")
    '    sb.Append(Me._rblClassChronic.ClientID)
    '    sb.Append("','")
    '    sb.Append(Me._rblClassDisplay.ClientID)
    '    sb.Append("','")
    '    sb.Append(Me._txtTotalClassification.ClientID)
    '    sb.Append("','")
    '    sb.Append(Me._txtAverageClassification.ClientID)
    '    sb.Append("','")
    '    sb.Append(Me._txtFailureClass.ClientID)
    '    sb.Append("');")
    '    Me._rblClassDisplay.Attributes.Add("onclick", sb.ToString)
    '    Me._rblClassCost.Attributes.Add("onclick", sb.ToString)
    '    Me._rblClassLife.Attributes.Add("onclick", sb.ToString)
    '    Me._rblClassPlanned.Attributes.Add("onclick", sb.ToString)
    '    Me._rblClassChronic.Attributes.Add("onclick", sb.ToString)
    '    Me._rblClassRepair.Attributes.Add("onclick", sb.ToString)

    '    Dim TotalClassification As Integer
    '    Dim AvgClassification As Decimal
    '    TotalClassification = CInt(_rblClassChronic.Text) + CInt(_rblClassCost.Text) + CInt(_rblClassLife.Text) + CInt(_rblClassPlanned.Text) + CInt(_rblClassRepair.Text)
    '    AvgClassification = CDec(TotalClassification / 5)
    '    Me._txtAverageClassification.Text = CStr(AvgClassification)
    '    Me._txtFailureClass.Text = CStr(AvgClassification)
    '    Me._txtTotalClassification.Text = CStr(TotalClassification)
    '    Me._rblClassDisplay.SelectedIndex = CInt(Math.Round(AvgClassification) - 1)
    'End Sub

    Private Sub GetIncident()
        If currentRI IsNot Nothing Then
            Me._incidentType.RefreshDisplay()
            With currentRI
                'State
                If .RCFALeader.ToUpper.Trim <> "NONE" Then
                    If _ddlAnalysisLead.Items.FindByValue(.RCFALeader) IsNot Nothing Then
                        Me._ddlAnalysisLead.SelectedValue = .RCFALeader
                        Me._cddlAnalysisLead.SelectedValue = .RCFALeader
                        Me._cddlAnalysisLead.ContextKey = .RCFALeader & "|" & .RCFALeaderName
                    Else
                        Me._ddlAnalysisLead.Items.Add(New ListItem(.RCFALeaderName, .RCFALeader))
                        Me._ddlAnalysisLead.SelectedValue = .RCFALeader
                        Me._cddlAnalysisLead.SelectedValue = .RCFALeader
                        Me._cddlAnalysisLead.ContextKey = .RCFALeader & "|" & .RCFALeaderName
                    End If
                    If _ddlAnalysisLead.SelectedItem IsNot Nothing Then
                        _txtAnalysisLead.Text = _ddlAnalysisLead.SelectedItem.Text
                    End If
                End If


                Me._btnActionItems.Text = Master.RIRESOURCES.GetResourceValue("Action Items", True, "Shared") & " (" & CStr(.ActionCount) & ")"
                Me._btnAnalysisWorkspace.Text = Master.RIRESOURCES.GetResourceValue("AWTitle", True, "Shared") & " (" & CStr(.WorkspaceCount + .FailureEventCount) & ")"
                Me._btnAttachments.Text = Master.RIRESOURCES.GetResourceValue("Attachments", True, "Shared") & " (" & CStr(.AttachmentCount) & ")"
                'Me._lblCreatedBy.Text = String.Format(Resources.Shared.lblCreatedBy, .CreatedBy)
                'Me._lblCreatedDate.Text = String.Format(Resources.Shared.lblCreationDate, .CreationDate)
                'Me._lblLastUpdateDate.Text = String.Format(Resources.Shared.lblLastUpdateDate, .LastUpdatedDate)
                'Me._lblUpdatedBy.Text = String.Format(Resources.Shared.lblLastUpdatedBy, .LastUpdatedBy)
                Me._lblCreatedBy.Text = String.Format(Master.RIRESOURCES.GetResourceValue("CreatedBy", True, "Shared"), .CreatedBy) 'Master.RIRESOURCES.GetResourceValue("None", True, "Shared"))
                Me._lblCreatedDate.Text = String.Format(Master.RIRESOURCES.GetResourceValue("CreationDate", True, "Shared"), .CreationDate) 'IP.Bids.Localization.DateTime.GetLocalizedDateTime(.CreationDate, Master.RIRESOURCES.CurrentLocale, "d")) 'Master.RIRESOURCES.GetResourceValue("None", True, "Shared"))
                If .LastUpdatedDate <> "" Then
                    Me._lblLastUpdateDate.Text = String.Format(Master.RIRESOURCES.GetResourceValue("LastUpdateDate", True, "Shared"), .LastUpdatedDate) 'IP.Bids.Localization.DateTime.GetLocalizedDateTime(.LastUpdatedDate, Master.RIRESOURCES.CurrentLocale, "d")) 'Master.RIRESOURCES.GetResourceValue("None", True, "Shared"))
                Else
                    Me._lblLastUpdateDate.Text = String.Format(Master.RIRESOURCES.GetResourceValue("LastUpdateDate", True, "Shared"), Master.RIRESOURCES.GetResourceValue("None", True, "Shared"))
                End If
                If .LastUpdatedBy <> "" Then
                    Me._lblUpdatedBy.Text = String.Format(Master.RIRESOURCES.GetResourceValue("LastUpdatedBy", True, "Shared"), .LastUpdatedBy) 'Master.RIRESOURCES.GetResourceValue("None", True, "Shared"))
                Else
                    Me._lblUpdatedBy.Text = String.Format(Master.RIRESOURCES.GetResourceValue("LastUpdatedBy", True, "Shared"), Master.RIRESOURCES.GetResourceValue("None", True, "Shared"))
                End If
                'Me._lblRINumber.Text = .RINumber
                'Location
                Me._txtAnalysisCompleted.Text = .RCFAAnalysisCompDate
                Me._txtCorrectiveActionsCompleted.Text = .RCFAActionCompDate

                If _ddlFacility.Items.FindByValue(.SiteID) IsNot Nothing Then Me._ddlFacility.SelectedValue = .SiteID
                If _ddlBusinessUnit.Items.FindByValue(.BusinessUnit) IsNot Nothing Then Me._ddlBusinessUnit.SelectedValue = .BusinessUnit
                If _ddlLineBreak.Items.FindByValue(.Linebreak) IsNot Nothing Then Me._ddlLineBreak.SelectedValue = .Linebreak
                Me._cddlFacility.SelectedValue = .SiteID
                Me._cddlLineBreak.SelectedValue = .Linebreak
                Me._cddlBusArea.SelectedValue = .BusinessUnit

                If .FunctionalLocation.Length > 0 Then
                    Me._functionalLocationTree.Text = .FunctionalLocation
                    If Me._functionalLocationTree.Text.Length > 0 Then
                        Me._functionalLocationTree.SetEquipmentDescription(.FunctionalLocation)
                    End If
                End If

                ' Me._incidentType.ConstrainedAreas_rblConstrainedAreas.ClearSelection()
                'If Me._rblConstrainedAreas.Items.FindByValue(.ConstrainedAreas) IsNot Nothing Then
                '    Me._rblConstrainedAreas.Items.FindByValue(.ConstrainedAreas).Selected = True
                'End If
                'Me._rblConstrainedAreas.Enabled = False


                Me._ddlCriticality.ClearSelection()
                If Me._ddlCriticality.Items.FindByValue(.Criticality) IsNot Nothing Then
                    Me._ddlCriticality.Items.FindByValue(.Criticality).Selected = True
                End If

                'Incident       
                Me._startEndCal.StartDate = .IncidentStartDate
                Me._startEndCal.EndDate = .IncidentEndDate
                'Me._startDateTime.DateValue = .IncidentStartDate
                'Me._endDateTime.DateValue = .IncidentEndDate
                If IsNumeric(.Downtime) Then
                    Me._txtDownTime.Text = IP.Bids.Localization.Numbers.GetLocalizedNumber(.Downtime)
                Else
                    Me._txtDownTime.Text = 0
                End If
                Me._txtIncidentTitle.Text = .IncidentTitle
                If _ddlCrew.Items.FindByValue(.Crew) IsNot Nothing Then Me._ddlCrew.SelectedValue = .Crew
                If _ddlShift.Items.FindByValue(.Shift) IsNot Nothing Then Me._ddlShift.SelectedValue = .Shift
                Me._cddlCrew.SelectedValue = .Crew
                Me._cddlShift.SelectedValue = .Shift
                Me._txtIncidentDescription.Text = .IncidentDescription
                Me._txtCost.Text = .Cost ' String.Format("{0:c}", FormatCurrency(.Cost, 0))

                Me._txtAnnualizedSavings.Text = .AnnualizedSavings ' String.Format("{0:c}", FormatCurrency(.AnnualizedSavings, 0))
                Me._txtCostofSolution.Text = .CostofSolution 'String.Format("{0:c}", FormatCurrency(.CostofSolution, 0))
                Me._ddlCapital.SelectedValue = .Capital
                Me._txtFinancialImpact.Text = .TotalCost ' String.Format("{0:c}", FormatCurrency(.TotalCost, 0))

                'Incident Type
                Me._incidentType.IRISNumber = .IRISNumber
                Me._incidentType.RTS = .RTS
                Me._incidentType.PPR = .PPR

                'If .PPR = "Yes" Then
                '    Me._ddlFacility.Enabled = False
                '    'Me._ddlBusinessUnit.Visible = False
                '    Me._ddlBusinessUnit.Enabled = False
                '    Me._ddlLineBreak.Enabled = False
                '    Me._ddlCrew.Enabled = False
                '    Me._ddlShift.Enabled = False
                '    Me._ddlAnalysisLead.Enabled = False
                '    Me._ddlTrigger.Enabled = False
                '    Me._ddlFailedMaterial.Enabled = False
                '    Me._txtDownTime.Enabled = False
                '    Me._startEndCal.Enabled = False
                '    Me._btnCalculateDowntime.Enabled = False
                '    Me._btnDelete.Enabled = False
                'End If

                'Me._incidentType.RTS = .
                Me._incidentType.Recordable = .Recordable
                Me._incidentType.Chronic = .Chronic
                Me._incidentType.Quality = .RCFAQuality
                If .RCFA.ToUpper = "YES" Then
                    Me._incidentType.RCFA = .RCFALevel
                Else
                    Me._incidentType.RCFA = "No"
                End If
                Me._incidentType.CertifiedKill = .CertifiedKill
                If .ConstrainedAreas = "Yes" Then
                    Me._incidentType.ConstrainedAreas = "Constrained Area"
                Else
                    Me._incidentType.ConstrainedAreas = "No"
                End If

                Me._incidentType.Safety = .RCFASafety
                Me._incidentType.SRR = .SRR
                Me._incidentType.SchedUnsched = .SchedUnsched
                'me._incidentType.RTS =  
                'Incdent Classification

                'If .Type.Length > 0 Then
                '    If enterRI IsNot Nothing Then

                '    End If
                'End If
                If _ddlTrigger.Items.FindByValue(.RCFATriggerDesc) IsNot Nothing Then
                    _ddlTrigger.SelectedValue = .RCFATriggerDesc
                    _cddlTrigger.ContextKey = ""
                Else
                    _ddlTrigger.Items.Add(.RCFATriggerDesc)
                    _ddlTrigger.SelectedValue = .RCFATriggerDesc
                    _cddlTrigger.ContextKey = .RCFATriggerDesc
                End If
                _cddlTrigger.SelectedValue = .RCFATriggerDesc

                '_IncidentClassification.TriggerValue = .RCFATriggerDesc
                _IncidentClassification.TypeValue = .Type
                _IncidentClassification.CauseValue = .Cause
                _IncidentClassification.PreventionValue = .Prevention
                _IncidentClassification.ProcessValue = .Process
                _IncidentClassification.ComponentValue = .Component

                'Other
                If _ddlFailedMaterial.Items.FindByValue(.RCFAFailedLocation) IsNot Nothing Then Me._ddlFailedMaterial.SelectedValue = .RCFAFailedLocation
                Me._cddlFailedMaterial.SelectedValue = .RCFAFailedLocation
                Me._txtConditionsInfluencingFailure.Text = .RCFACondition
                Me._txtPeopleToInterview.Text = .RCFAPeople
                Me._txtTeamMembers.Text = .RCFATeamMembers

                'Document Causes
                RI.SharedFunctions.SetCheckBoxValues(Me._cblPhysical, .PhysicalCauses)
                RI.SharedFunctions.SetCheckBoxValues(Me._cblLatentCauses, .LatentCauses)
                RI.SharedFunctions.SetCheckBoxValues(Me._cblHuman, .HumanCauses)
                Me._txtOtherHuman.Text = .OtherHumanCauses
                Me._txtOtherLatent.Text = .OtherLatentCauses
                Me._txtOtherPhysical.Text = .OtherPhysicalCauses

                ''Failure classification
                'Me._rblClassCost.SelectedValue = .ClassCost
                'Me._rblClassChronic.SelectedValue = .ClassChronic
                'Me._rblClassLife.SelectedValue = .ClassLife
                'Me._rblClassPlanned.SelectedValue = .ClassSchedule
                'Me._rblClassRepair.SelectedValue = .ClassImpact
                'Me.CalculateFailureClassification()

                'New Failure Classification
                Me._FailureClassification.ConstrainedAreas = .ClassificationConstrainedAreas
                Me._FailureClassification.CriticalityRating = .ClassificationCriticality
                Me._FailureClassification.EquipmentCare = .ClassificationEquipmentCare
                Me._FailureClassification.LifeExpectancy = .ClassificationLifeExpectancy
                Me._FailureClassification.CalculateFailureClassificationScore()

                'Verification CAC 7/16/14
                If .Verification <> "" Then
                    If .Verification = "Yes" Then
                        _rblVerification.Items.FindByValue("Yes").Selected = True
                    Else
                        _rblVerification.Items.FindByValue("No").Selected = True
                    End If
                Else
                    _rblVerification.Items.FindByValue("No").Selected = True
                End If


                Me._tbPerson.Text = .VerRespName
                Me._tbWeeksAfter.Text = .VerificationWeeksAfter
                Me._tbVerComment.Text = .VerificationComment
                Me._tbClosedDate.Text = .VerificationClosedDate
                Me._tbDueDate.Text = .VerificationDueDate
                If Me._tbClosedDate.Text <> "" Then
                    Me._ddlPerson.Visible = False
                    Me._tbPerson.Visible = True
                    Me._tbWeeksAfter.Enabled = False
                    Me._rblVerification.Enabled = False
                Else
                    Me._ddlPerson.SelectedValue = .VerificationResp
                    Me._cddlPerson.SelectedValue = .VerificationResp
                    Me._ddlPerson.Visible = True
                    Me._tbPerson.Visible = False
                End If

            End With
        End If
    End Sub
    Private Sub PopulateStaticData()
        If enterRI IsNot Nothing Then
            RI.SharedFunctions.BindList(_cblHuman, enterRI.HumanCauses, True, False, enterRI.HumanCauses.DataValueField & " not like 'Other%'", True, True)
            RI.SharedFunctions.BindList(Me._cblLatentCauses, enterRI.LatentCauses, True, False, enterRI.LatentCauses.DataValueField & " not like 'Other%'", True, True)
            RI.SharedFunctions.BindList(Me._cblPhysical, enterRI.PhysicalCauses, True, False, enterRI.PhysicalCauses.DataValueField & " not like 'Other%'", True, True)
            RI.SharedFunctions.BindList(Me._ddlCapital, enterRI.Capital, True, True)
        End If
        'Response.Flush()
    End Sub
    Private Sub PopulateData()
        If enterRI IsNot Nothing Then
            If _ddlFacility.SelectedValue.Length = 0 Then
                If userProfile IsNot Nothing Then
                    If _ddlFacility.Items.FindByValue(userProfile.DefaultFacility) IsNot Nothing Then
                        _ddlFacility.SelectedValue = userProfile.DefaultFacility
                    End If
                    Me._cddlFacility.SelectedValue = userProfile.DefaultFacility
                End If
            End If

            'Dim lookUp As New FunctionalLocationLookup
            'Dim AssignLeader As String = lookUp.GetNotificationType(Me._ddlFacility.SelectedValue, Me._ddlBusinessUnit.SelectedValue, Me._ddlLineBreak.SelectedValue, userProfile.Username)
            'lookUp = Nothing

            'If AssignLeader.Length > 0 Then
            '    Me._ddlAnalysisLead.Visible = True
            '    Me._lblCommentsForEmail.Visible = True
            '    Me._txtForEmail.Visible = True
            '    Me._txtAnalysisLead.Visible = True
            '    Me._txtAnalysisLead.Style.Add("display", "none")
            'Else
            '    Me._ddlAnalysisLead.Visible = True
            '    Me._ddlAnalysisLead.Style.Add("display", "none")
            '    _txtAnalysisLead.Visible = True
            '    'Me._ddlAnalysisLead.CssClass = "Disabled"
            '    Me._ddlAnalysisLead.Items.Clear()
            '    Me._lblCommentsForEmail.Visible = True
            '    Me._lblCommentsForEmail.Style.Add("display", "none")
            '    Me._txtForEmail.Visible = True
            '    Me._txtForEmail.Style.Add("display", "none")
            'End If
            If enterRI.IncidentSecurity.DeleteIncidents Then
                Me._btnDelete.Visible = True
            Else
                Me._btnDelete.Visible = True
                Me._btnDelete.Style.Add("display", "none")
            End If
            'End If
            If userProfile.DistinguishedName.ToUpper.Contains("LOVELAND") Or userProfile.DistinguishedName.ToUpper.Contains("MEMPHIS") Or userProfile.DefaultFacility = selectedFacility Or userProfile.DefaultFacility.ToUpper = "AL" Or userProfile.DefaultFacility.ToUpper = "TY" Then 'TY is Global Technology
                '                _btnSpell.Visible = True
                _btnSubmit.Visible = True
            Else
                _btnSpell.Attributes.CssStyle.Add("display", "none")
                _btnSubmit.Style.Add("display", "none")
                Me._ddlFacility.Enabled = False
            End If
            If userProfile.DivestedLocation = True Then
                _btnSpell.Visible = False
                _btnSubmit.Visible = False
            End If
        End If
    End Sub
    Public Function FacilitiesAllowedToUpdate() As String
        If userProfile.DistinguishedName.ToUpper.Contains("LOVELAND") Or userProfile.DistinguishedName.ToUpper.Contains("MEMPHIS") Then ' Or userProfile.DefaultFacility = selectedFacility Then
            Return "AL"
        Else
            Return userProfile.DefaultFacility
        End If
    End Function

    'Private Sub PopulateAnalysisLeader()
    '    Try
    '        If enterRI IsNot Nothing Then
    '            RI.SharedFunctions.BindList(Me._ddlAnalysisLead, enterRI.AnalysisLeader, False, True)
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    Finally

    '    End Try
    'End Sub

    Protected Sub _startEndCal_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles _startEndCal.Load
        'If currentRI Is Nothing Then
        '    Dim englishDate As String = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Now, "EN-US", "d")
        '    'If Me._startEndCal.StartDate.Length = 0 Then
        '    Me._startEndCal.StartDate = englishDate
        '    'End If
        '    'If Me._startEndCal.EndDate.Length = 0 Then
        '    Me._startEndCal.EndDate = englishDate
        '    ' End If
        'End If
    End Sub

    'Protected Sub _startDateTime_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles _startDateTime.Load
    '    If Me._startDateTime.DateValue.Length = 0 Then
    '        Me._startDateTime.DateValue = Now().ToShortDateString
    '    End If
    'End Sub

    'Protected Sub _endDateTime_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles _endDateTime.Load
    '    If Me._endDateTime.DateValue.Length = 0 Then
    '        Me._endDateTime.DateValue = Now().ToShortDateString
    '    End If
    'End Sub

    Private Sub GetControlState()
        If Request.Form(Me._ddlFacility.UniqueID) IsNot Nothing Then
            selectedFacility = Request.Form(Me._ddlFacility.UniqueID)
            SetupFunctionalLocation()
            Me._functionalLocationTree.PopulateFunctionalLocation(selectedFacility)
        End If
        If Request.Form(Me._ddlBusinessUnit.UniqueID) IsNot Nothing Then
            selectedBusArea = Request.Form(Me._ddlBusinessUnit.UniqueID)
        End If
        If Request.Form(Me._ddlLineBreak.UniqueID) IsNot Nothing Then
            selectedLine = Request.Form(Me._ddlLineBreak.UniqueID)
            Me._functionalLocationTree.PopulateFunctionalLocation(selectedFacility, selectedBusArea, selectedLine)
        End If
    End Sub
    'Private Sub HandlePostBack()

    '    If RI.SharedFunctions.CausedPostBack(Me._ddlBusinessUnit.ID.ToString) Or _ddlBusinessUnit.SelectedValue.Length > 0 Then
    '        _ddlBusinessUnit_SelectedIndexChanged(Nothing, Nothing)
    '    End If
    '    If RI.SharedFunctions.CausedPostBack(Me._ddlFacility.ID.ToString) Then

    '    End If
    '    If RI.SharedFunctions.CausedPostBack(Me._ddlLineBreak.ID.ToString) Then
    '        'Me._functionalLocationTree.PopulateFunctionalLocation(selectedFacility, Me._ddlBusinessUnit.SelectedValue, Me._ddlLineBreak.SelectedValue)
    '    End If
    'End Sub

    'Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
    '    If enterRI IsNot Nothing Then
    '        enterRI = Nothing            
    '    End If
    '    userProfile = Nothing
    'End Sub



    Protected Sub _btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnSubmit.Click
        If Request.QueryString("RINumber") IsNot Nothing And IsNumeric(Request.QueryString("RINumber")) Then
            SaveIncident(True, True, Request.QueryString("RINumber"))
        Else
            SaveIncident(True)
        End If
        Response.Clear()
        Response.Redirect(Page.AppRelativeVirtualPath & "?RINumber=" & Request.QueryString("RINumber"), True)
        Response.End()
    End Sub


    Protected Sub _IncidentClassification_IncidentClassificationChanged() Handles _IncidentClassification.IncidentClassificationChanged
        selectedFacility = Me._ddlFacility.SelectedValue
        selectedBusArea = Me._ddlBusinessUnit.SelectedValue
        selectedLine = Me._ddlLineBreak.SelectedValue

        SetupFunctionalLocation()
        Me.PopulateData()
    End Sub

    Private Sub EmailIncident(Optional ByVal sendAnalysisComplete As Boolean = False, Optional ByVal sendChangeAnalysis As Boolean = False, Optional ByVal analysisLeaderHasChanged As Boolean = False, Optional ByVal sendNotificationEmail As Boolean = False, Optional ByVal sendEHSEmail As Boolean = False)

        Dim Body As New StringBuilder
        Dim commonBody As New StringBuilder
        Dim spacer As String = "&nbsp;&nbsp;&nbsp;&nbsp;"
        Dim isSafetyIncident As Boolean = False
        Dim isAnalysisLeaderAssigned As Boolean = False
        Dim AnalysisLeaderText As String = String.Empty
        Dim TextForAnalysisLeader As String = String.Empty
        Dim url As String = String.Empty
        Dim iploc As New IP.Bids.Localization.WebLocalization

        'Dim additonalTextForEmail As String = "<p>Additional Information RCFA Additional Information regarding leading and conducting Root Cause Failure Analysis on this event from {0}:<br>"
        'Dim urlToAnalysisLeader As String = "<p><a href='{0}'>Click Here to begin Updating Analyisis</a></p>"
        'Dim urlAssignAnalysisLeader As String = "<p><a href='{0}'>Click here to View/Update Information or Assign a Leader for this event</a></p>"
        'Dim urlExecutiveSummary As String = "<p><a href='{0}'>Click here to View Executive Summary for this RCFA</a></p>"
        'Dim urlChangeAnalysisBack As String = "<p><a href='{0}'>Click here to Change Analysis back to in Progress for this RCFA</a></p>"
        'Dim additionalText As String = "<h2>**** THIS IS A TEST INCIDENT NOTIFICATION ****</H2>"

        Dim additonalTextForEmail As String = "<p>" & Master.RIRESOURCES.GetResourceValue("Additional Information RCFA") & " {0}:<br>"
        Dim urlToAnalysisLeader As String = "<p><a href='{0}'>" & Master.RIRESOURCES.GetResourceValue("Click Here to begin Updating Analyisis") & "</a></p>"
        Dim urlAssignAnalysisLeader As String = "<p><a href='{0}'> " & Master.RIRESOURCES.GetResourceValue("Click here to View/Update") & "</a></p>"
        Dim urlExecutiveSummary As String = "<p><a href='{0}'>" & Master.RIRESOURCES.GetResourceValue("Click here to View Executive Summary for this RCFA") & "</a></p>"
        Dim urlChangeAnalysisBack As String = "<p><a href='{0}'>" & Master.RIRESOURCES.GetResourceValue("Click here to Change Analysis back to in Progress for this RCFA") & "</a></p>"
        Dim additionalText As String = "<h2>" & Master.RIRESOURCES.GetResourceValue("**** THIS IS A TEST INCIDENT NOTIFICATION ****") & "</H2>"

        Dim urlHost As String
        Dim executiveSummaryURL As String
        Dim crystalURL As String
        Dim RIStatus As String = String.Empty

        'If currentRI Is Nothing Then
        If Request.QueryString("RINumber") IsNot Nothing Then
            Dim riNumber As String = Request.QueryString("RINumber")
            currentRI = New clsCurrentRI(riNumber)
            RIStatus = Master.RIRESOURCES.GetResourceValue("Updated") & " "
            'previousEmailSent = False
            If currentRI Is Nothing Then Throw New Exception(Master.RIRESOURCES.GetResourceValue("Error Getting Current Incident") & " " & riNumber)
        ElseIf currentRI IsNot Nothing Then
            Dim riNumber As String = currentRI.RINumber
            currentRI = New clsCurrentRI(riNumber)
            RIStatus = Master.RIRESOURCES.GetResourceValue("New") & " "
            If sendEHSEmail And currentRI IsNot Nothing Then
                currentRI.GetEHSNotificationList("Millwide", Me._incidentType.GetEHSType)
            End If
            'previousEmailSent = True
            If currentRI Is Nothing Then Throw New Exception(Master.RIRESOURCES.GetResourceValue("Error Getting Current Incident") & " " & riNumber)
        End If
        If currentRI.RCFASafety.Length > 0 Then isSafetyIncident = True
        If currentRI.RCFALeader.Length > 0 And currentRI.RCFALeader.ToLower <> "none" Then isAnalysisLeaderAssigned = True

        If Request.UserHostAddress = "127.0.0.1" Then
            urlHost = "Http://ridev.ipaper.com/ri/ri/"
            urlHost = "http://gpiri.graphicpkg.com/ri/"
            crystalURL = "http://ridev.ipaper.com/CEReporting/"
        Else
            If Request.ServerVariables("HTTP_HOST").ToLower.Contains("ridev") Then
                ' urlHost = "Http://ridev.ipaper.com/ri/ri/"
                urlHost = "http://gpiri.graphicpkg.com/ri/"
                crystalURL = "http://ridev.ipaper.com/CEReporting/"
            ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritest") Then
                'urlHost = "Http://ritest.ipaper.com/ri/ri/"
                urlHost = "http://gpiri.graphicpkg.com/ri/"
                crystalURL = "http://ritest.ipaper.com/CEReporting/"
            ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritrain") Then
                ' urlHost = "Http://ritrain.ipaper.com/ri/ri/"
                urlHost = "http://gpiri.graphicpkg.com/ri/"
                crystalURL = "http://ritrain.ipaper.com/CEReporting/"
            Else
                'urlHost = "Http://rigpi.ipaper.com/ri/ri/"
                'crystalURL = "http://rigpi.ipaper.com/CEReporting/"
                urlHost = "http://gpiri.graphicpkg.com/ri/"
                crystalURL = "http://gpimv.graphicpkg.com/cereporting/"
                additionalText = String.Empty
            End If

        End If

        url = urlHost & "EnterNewRI.aspx?RINumber=" & currentRI.RINumber
        Body.Append("<html><head><title>Assign</title></head>")
        Body.Append("<body bgcolor=""#FFFFFF"">")
        Body.Append(RI.SharedFunctions.CleanString(additionalText, "<br>"))
        Body.Append("<p><font size = ""2"" face=""Arial""><strong>")

        If sendChangeAnalysis = True Then
            Body.Append("<br><strong>" & Master.RIRESOURCES.GetResourceValue("RCFA Analysis Changed to In Progress") & "</strong></p>")
        ElseIf sendAnalysisComplete = True Then
            Body.Append("<strong>" & Master.RIRESOURCES.GetResourceValue("RCFA Analysis Completed") & "</strong></p>")
            If currentRI IsNot Nothing Then
                With currentRI
                    If .WorkspaceCount + .FailureEventCount = 0 Or (.PhysicalCauses.Length = 0 And .OtherPhysicalCauses.Length = 0) Or (.HumanCauses.Length = 0 And .OtherHumanCauses.Length = 0) Or (.LatentCauses.Length = 0 And .OtherLatentCauses.Length = 0) Or .ActionCount = 0 Or .Type.Length = 0 Or .Cause.Length = 0 Or .Process.Length = 0 Or .Component.Length = 0 Or .Prevention.Length = 0 Then
                        Body.Append("<BR><font color=""#FF0000""><strong>" & Master.RIRESOURCES.GetResourceValue("Analysis Complete Warning") & "</font></strong>")
                        'If .RCFAAnalysisCompDate.Length > 0 Then
                        ' Body.Append("<br>&nbsp;&nbsp;<font color=""#FF0000""><strong>**Analysis Previously Submitted</font></strong>")
                        'End If
                        If .WorkspaceCount = 0 And .FailureEventCount = 0 Then
                            'Body.Append("<br>&nbsp;&nbsp;<font color=""#FF0000""><strong>**No Analysis Workspace Identified</font></strong>")
                            Body.Append("<br>&nbsp;&nbsp;<font color=""#FF0000""><strong>**" & Master.RIRESOURCES.GetResourceValue("No Analysis Workspace Identified") & "</font></strong>")
                        End If

                        If .PhysicalCauses.Length = 0 And .OtherPhysicalCauses.Length = 0 Then
                            Body.Append("<br>&nbsp;&nbsp;<font color=""#FF0000""><strong>**" & Master.RIRESOURCES.GetResourceValue("No Physical Root Identified") & "</font></strong>")
                        End If
                        If .HumanCauses.Length = 0 And .OtherHumanCauses.Length = 0 Then
                            Body.Append("<br>&nbsp;&nbsp;<font color=""#FF0000""><strong>" & Master.RIRESOURCES.GetResourceValue("No Human Root Identified") & "</font></strong>")
                        End If
                        If .LatentCauses.Length = 0 And .OtherLatentCauses.Length = 0 Then
                            Body.Append("<br>&nbsp;&nbsp;<font color=""#FF0000""><strong>**" & Master.RIRESOURCES.GetResourceValue("No Latent Root Identified") & "</font></strong>")
                        End If
                        If .ActionCount = 0 Then
                            Body.Append("<br>&nbsp;&nbsp;<font color=""#FF0000""><strong>**" & Master.RIRESOURCES.GetResourceValue("No Corrective Action Items Defined") & "</font></strong>")
                        End If
                        If .Type.Length = 0 Then
                            Body.Append("<br>&nbsp;&nbsp;<font color=""#FF0000""><strong>**" & Master.RIRESOURCES.GetResourceValue("No RI Type Identified") & "</font></strong> ")
                        End If
                        If .Cause.Length = 0 Then
                            Body.Append("<br>&nbsp;&nbsp;<font color=""#FF0000""><strong>**" & Master.RIRESOURCES.GetResourceValue("No RI Cause Identified") & "</font></strong> ")
                        End If
                        If .Process.Length = 0 Then
                            Body.Append("<br>&nbsp;&nbsp;<font color=""#FF0000""><strong>**" & Master.RIRESOURCES.GetResourceValue("No RI Process Identified") & "</font></strong> ")
                        End If
                        If .Component.Length = 0 Then
                            Body.Append("<br>&nbsp;&nbsp;<font color=""#FF0000""><strong>**" & Master.RIRESOURCES.GetResourceValue("No RI Component Identified") & "</font> </strong>")
                        End If
                        If .Prevention.Length = 0 Then
                            Body.Append("<br>&nbsp;&nbsp;<font color=""#FF0000""><strong>**" & Master.RIRESOURCES.GetResourceValue("No RI Prevention Identified") & "</font></strong>")
                        End If
                    End If
                End With
            End If
        Else
            If Me._ddlAnalysisLead.SelectedValue = "" Or _ddlAnalysisLead.SelectedValue.ToUpper = "NONE" Then
                Body.Append("<br>" & Master.RIRESOURCES.GetResourceValue("Bus Unit Mgr Review") & "</strong><br>{0}</p>")
            Else
                Body.Append("<br>{0}</p>")
            End If
        End If

        '*****Start Common Body***************
        commonBody.AppendLine("")
        commonBody.Append("<ul>")
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("RINumber") & ":</strong>" & spacer & currentRI.RINumber)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Reliability Recordable") & ":</strong>" & spacer & Master.RIRESOURCES.GetResourceValue(currentRI.Recordable))
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("RCFA") & ":</strong>" & spacer & Master.RIRESOURCES.GetResourceValue(currentRI.RCFA))
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("IncidentTitle") & ":</strong>" & spacer & Me._txtIncidentTitle.Text)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Event Date") & ":</strong>" & spacer & Date.Parse(Me._startEndCal.StartDate, New System.Globalization.CultureInfo("EN-US")).ToString)  'IP.Bids.Localization.DateTime.GetLocalizedDateTime(Me._startEndCal.StartDate, iploc.CurrentLocale, "d"))
        If Me._txtIncidentDescription.Text.Length > 0 Then
            commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Description") & ":</strong>" & spacer & RI.SharedFunctions.CleanString(_txtIncidentDescription.Text, "<br>"))
        End If
        'commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Area") & ":</strong>" & spacer & Me._ddlBusinessUnit.SelectedValue & " " & Me._ddlLineBreak.SelectedValue)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Area") & ":</strong>" & spacer & Me._ddlBusinessUnit.SelectedItem.Text & " " & Me._ddlLineBreak.SelectedItem.Text)
        If Me._functionalLocationTree.Text.Length > 0 Then
            commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Equipment Desc") & ":</strong>" & spacer & _functionalLocationTree.Text)
        End If
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Cost") & "$:</strong>" & spacer & Me._txtCost.Text)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Total Financial Impact ") & "$:</strong>" & spacer & Me._txtFinancialImpact.Text)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Downtime(hrs)") & ":</strong>" & spacer & Me._txtDownTime.Text)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Chronic") & ":</strong>" & spacer & Master.RIRESOURCES.GetResourceValue(Me._incidentType.Chronic))
        If Me._ddlTrigger.SelectedValue.Length > 0 Then
            commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Trigger") & ":</strong>" & spacer & _ddlTrigger.SelectedItem.Text)
            'commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Trigger") & ":</strong>" & spacer & _ddlTrigger.SelectedValue)
        End If
        If Me._incidentType.Safety.Length > 0 Then
            commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Safety") & ":</strong>" & spacer & Me._incidentType.Safety)
        End If
        If Me._incidentType.IRISNumber.Length > 0 Then
            commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("IRIS Number") & ":</strong>" & spacer & Me._incidentType.IRISNumber)
        End If

        commonBody.Append("</ul>")
        '******End Common Body ************

        Body.Append(commonBody.ToString)



        'executiveSummaryURL = String.Format(crystalURL & "frmCrystalReport.aspx?Report=ExecutiveSummary&RINumber={0}", currentRI.RINumber)
        'executiveSummaryURL = String.Format(crystalURL & "frmCrystalReport.aspx?Report=ExecutiveSummary&RINumber={0}&Localename=" & Master.RIRESOURCES.CurrentLocale, currentRI.RINumber)
        executiveSummaryURL = String.Format(crystalURL & "CrystalReportDisplay.aspx?Report=ExecutiveSummary&RINumber={0}&Localename=" & Master.RIRESOURCES.CurrentLocale, currentRI.RINumber)
        urlToAnalysisLeader = String.Format(urlToAnalysisLeader, url)
        urlAssignAnalysisLeader = String.Format(urlAssignAnalysisLeader, url)
        urlExecutiveSummary = String.Format(urlExecutiveSummary, executiveSummaryURL)
        urlChangeAnalysisBack = String.Format(urlChangeAnalysisBack, url & "&ChangeAnalysis=True")

        If sendChangeAnalysis = True Then
            'Request.QueryString("RINumber")
            ' RCFA Analysis Changed to In Progress
            Dim subjectForAnalysisLeader As String = Master.RIRESOURCES.GetResourceValue("Incident Changed to In Progress")
            If Me._txtChangeAnalysisComments.Text.Length > 0 Then
                Body.Append(String.Format(additonalTextForEmail, userProfile.FullName))
                Body.Append(RI.SharedFunctions.CleanString(Me._txtChangeAnalysisComments.Text, "<br>"))
                Body.Append("</p>")
            End If
            Body.Append(urlToAnalysisLeader)
            Body.Append("</body></html>")
            Dim msgForAnalysisLeader As String = String.Format(Body.ToString, TextForAnalysisLeader)
            If currentRI.NotificationLeaderEmail.Length > 0 Then
                RI.SharedFunctions.SendEmail(currentRI.NotificationLeaderEmail, userProfile.Email, subjectForAnalysisLeader, msgForAnalysisLeader, userProfile.FullName)
            Else
                RI.SharedFunctions.SendEmail(currentRI.NotificationToList, userProfile.Email, subjectForAnalysisLeader, msgForAnalysisLeader, userProfile.FullName)
            End If
        ElseIf sendAnalysisComplete = True Then


            Body.Append(urlExecutiveSummary)
            Body.Append(urlChangeAnalysisBack)
            Body.Append("</body></html>")
            Dim subjectForNotificationList As String = Master.RIRESOURCES.GetResourceValue("Analysis Complete") & " - " & RI.SharedFunctions.CleanString(currentRI.IncidentTitle)
            Dim msgForNotificationList As String = String.Format(Body.ToString, AnalysisLeaderText)
            RI.SharedFunctions.SendEmail(currentRI.NotificationToList, userProfile.Email, subjectForNotificationList, Body.ToString, userProfile.FullName, currentRI.NotificationcopyList)

        Else
            If isAnalysisLeaderAssigned And analysisLeaderHasChanged = True Then
                '                AnalysisLeaderText = String.Format("{0} has been assigned as the Analysis Leader for the Following Event", currentRI.RCFALeaderName)
                AnalysisLeaderText = String.Format("{0} " & Master.RIRESOURCES.GetResourceValue("Analysis Leader Assigned"), currentRI.RCFALeaderName)
                'TextForAnalysisLeader = String.Format("{0}, <br><br>You have been assigned as the Analysis Leader for the Following Event", currentRI.RCFALeaderName)
                TextForAnalysisLeader = String.Format("{0}, <br><br> " & Master.RIRESOURCES.GetResourceValue("You have been assigned as the Analysis Leader"), currentRI.RCFALeaderName)

                Dim msgForNotificationList As String = String.Format(Body.ToString & urlAssignAnalysisLeader & "</body></html>", AnalysisLeaderText)
                Dim subjectForAnalysisLeader As String = RIStatus & Master.RIRESOURCES.GetResourceValue("RI - You have been assigned as the Analysis Leader") & ": " & RI.SharedFunctions.CleanString(currentRI.IncidentTitle)
                Dim subjectForNotificationList As String = RIStatus & Master.RIRESOURCES.GetResourceValue("RI initiated in ") & " " & Me._ddlBusinessUnit.SelectedItem.Text & ": " & currentRI.IncidentTitle
                'Dim subjectForAnalysisLeader As String = RIStatus & "RI - You have been assigned as the Analysis Leader - " & currentRI.IncidentTitle
                'Dim subjectForNotificationList As String = RIStatus & "RI initiated in " & currentRI.BusinessUnit & ": " & currentRI.IncidentTitle
                If Me._txtForEmail.Text.Length > 0 Then
                    Body.Append(String.Format(additonalTextForEmail, userProfile.FullName))
                    Body.Append(Me._txtForEmail.Text)
                    Body.Append("</p>")
                End If
                Body.Append(urlToAnalysisLeader)
                Body.Append("</body></html>")

                Dim msgForAnalysisLeader As String = String.Format(Body.ToString, TextForAnalysisLeader)
                'Email for AL
                RI.SharedFunctions.SendEmail(currentRI.NotificationLeaderEmail, userProfile.Email, subjectForAnalysisLeader, msgForAnalysisLeader, userProfile.FullName)
                'Email for NL
                If sendNotificationEmail = True Then
                    RI.SharedFunctions.SendEmail(currentRI.NotificationToList, userProfile.Email, subjectForNotificationList, msgForNotificationList, userProfile.FullName, currentRI.NotificationcopyList)
                End If
            Else
                If sendNotificationEmail = True Then
                    If currentRI.RCFA.ToUpper <> "NO" Then
                        If isAnalysisLeaderAssigned = True Then
                            AnalysisLeaderText = String.Format("{0} " & Master.RIRESOURCES.GetResourceValue("Analysis Leader Assigned"), currentRI.RCFALeaderName)
                            'AnalysisLeaderText = String.Format("{0} " & Master.RIRESOURCES.GetResourceValue("Analysis Leader Assigned") & "has been assigned as the Analysis Leader for the Following Event", currentRI.RCFALeaderName)
                        Else
                            AnalysisLeaderText = String.Format("{0} " & Master.RIRESOURCES.GetResourceValue("Assign Analysis Leader"), currentRI.NotificationToFullName)
                            'AnalysisLeaderText = String.Format("{0} " & Master.RIRESOURCES.GetResourceValue("Assign Analysis Leader") & "is responsible for assigning an Analysis Leader for the Following Event", currentRI.NotificationToFullName)
                        End If
                    End If

                    If Me._txtForEmail.Text.Length > 0 Then
                        Body.Append(String.Format(additonalTextForEmail, userProfile.FullName))
                        Body.Append(Me._txtForEmail.Text)
                        Body.Append("</p>")
                    End If
                    Body.Append(urlAssignAnalysisLeader)
                    Body.Append("</body></html>")
                    'Dim subjectForNotificationList As String = RIStatus & " " & Master.RIRESOURCES.GetResourceValue("RI initiated in ") & " " & currentRI.BusinessUnit & ": " & currentRI.IncidentTitle
                    Dim subjectForNotificationList As String = RIStatus & " " & Master.RIRESOURCES.GetResourceValue("RI initiated in ") & " " & Me._ddlBusinessUnit.SelectedItem.Text & ": " & RI.SharedFunctions.CleanString(currentRI.IncidentTitle)
                    Dim msgForNotificationList As String = String.Format(Body.ToString, AnalysisLeaderText)
                    RI.SharedFunctions.SendEmail(currentRI.NotificationToList, userProfile.Email, subjectForNotificationList, msgForNotificationList, userProfile.FullName, currentRI.NotificationcopyList)
                End If
            End If
        End If
    End Sub

    Protected Sub _btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnDelete.Click
        If Request.QueryString("RINumber") IsNot Nothing Then
            Dim riNumber As Integer = CInt(Request.QueryString("RINumber"))
            If riNumber > 0 Then
                clsCurrentRI.DeleteCurrentRI(CInt(riNumber), userProfile.Username)
                'MsgBox(riNumber & " has been deleted", MsgBoxStyle.Information)
                Response.Redirect("~/ri/ViewUpdateSearch.aspx", True)
                'Server.Transfer("~/ri/ViewUpdateSearch.aspx", False)
            End If
        Else
            Response.Clear()
            Response.Redirect("~/ri/EnterNewri.aspx", True)
            Response.End()
        End If
    End Sub

    Protected Sub _btnMarkAnalysisComplete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnMarkAnalysisComplete.Click
        'Save existing data
        If Request.QueryString("RINumber") IsNot Nothing Then
            SaveIncident(False, False, Request.QueryString("RINumber"))
        Else
            SaveIncident(False)
        End If
        'Validate Existence of RCFA data
        Dim msg As New StringBuilder
        If Page.IsValid Then
            If currentRI IsNot Nothing Then
                With currentRI
                    If .RCFAAnalysisCompDate.Length > 0 Then
                        msg.Append("<li>" & Master.RIRESOURCES.GetResourceValue("Analysis Previously Submitted") & "</li>")
                    End If
                    'I need to check Analysis Workspace
                    If .WorkspaceCount = 0 And .FailureEventCount = 0 Then
                        msg.Append("<li>" & Master.RIRESOURCES.GetResourceValue("No Analysis Workspace Identified") & "</li>")
                    End If

                    If .PhysicalCauses.Length = 0 And .OtherPhysicalCauses.Length = 0 Then
                        msg.Append("<li>" & Master.RIRESOURCES.GetResourceValue("No Physical Root Identified") & "</li>")
                    End If
                    If .HumanCauses.Length = 0 And .OtherHumanCauses.Length = 0 Then
                        msg.Append("<li>" & Master.RIRESOURCES.GetResourceValue("No Human Root Identified") & "</li>")
                    End If
                    If .LatentCauses.Length = 0 And .OtherLatentCauses.Length = 0 Then
                        msg.Append("<li>" & Master.RIRESOURCES.GetResourceValue("No Latent Root Identified") & "</li>")
                    End If
                    'If .r.Length = 0 Then
                    If .ActionCount = 0 Then
                        msg.Append("<li>" & Master.RIRESOURCES.GetResourceValue("No Corrective Action Items Defined") & " </li>")
                    End If
                    If .Type.Length = 0 Then
                        msg.Append("<li>" & Master.RIRESOURCES.GetResourceValue("No RI Type Identified") & "</li> ")
                    End If
                    If .Cause.Length = 0 Then
                        msg.Append("<li>" & Master.RIRESOURCES.GetResourceValue("No RI Cause Identified") & "</li> ")
                    End If
                    If .Process.Length = 0 Then
                        msg.Append("<li>" & Master.RIRESOURCES.GetResourceValue("No RI Process Identified") & "</li> ")
                    End If
                    If .Component.Length = 0 Then
                        msg.Append("<li>" & Master.RIRESOURCES.GetResourceValue("No RI Component Identified") & "</li> ")
                    End If
                    If .Prevention.Length = 0 Then
                        msg.Append("<li>" & Master.RIRESOURCES.GetResourceValue("No RI Prevention Identified") & "</li> ")
                    End If
                    If msg.ToString.Length > 0 Then
                        msg.Append("</ul>")
                        _messageBox.Title = Master.RIRESOURCES.GetResourceValue("Analysis Complete Warning")
                        _messageBox.Message = "<center><h2>" & Master.RIRESOURCES.GetResourceValue("WARNING:") & "</h2></center><br><ul>" & msg.ToString & "<br><br>" & Master.RIRESOURCES.GetResourceValue("Do you want to continue?")
                        _messageBox.Width = 200
                        _messageBox.CancelScript = "javascript:window.location='" & Page.ResolveClientUrl("~/ri/Enternewri.aspx?RINumber=" & Request.QueryString("RINumber")) & "'"
                        _messageBox.ShowMessage()
                        'Dim ret As MsgBoxResult = MsgBox("WARNING:" & vbCrLf & vbCrLf & msg.ToString & vbCrLf & "<br>Do you want to continue?", MsgBoxStyle.YesNo)
                        'RI.SharedFunctions.ASPNET_MsgBox("WARNING:" & vbCrLf & vbCrLf & msg.ToString & vbCrLf & "<br>Do you want to continue?")                       
                    Else
                        If Request.QueryString("RINumber") IsNot Nothing Then
                            clsCurrentRI.MarkAnalysisComplete(userProfile.Username, Request.QueryString("RINumber"))
                            EmailIncident(True)
                            Response.Clear()
                            Response.Redirect(Page.AppRelativeVirtualPath & "?RINumber=" & Request.QueryString("RINumber"), True)
                            Response.End()
                            'Server.Transfer(Page.AppRelativeVirtualPath & "?RINumber=" & Request.QueryString("RINumber"), False)
                        End If
                    End If

                End With
            End If
        End If
    End Sub

    Private Sub SaveIncident(Optional ByVal refreshPage As Boolean = True, Optional ByVal sendEmail As Boolean = True, Optional ByVal riNumber As String = "")
        If Page.IsValid Then
            currentRI = New clsCurrentRI(riNumber)
            Dim sendEmailFlag As Boolean = False
            Dim previousEmailSent As Boolean = False
            Dim analysisLeaderHasChanged As Boolean = False
            Dim sendNotificationEmail As Boolean = False
            Dim sendEHSEmail As Boolean = False

            With currentRI
                'GetIncident()
                OriginalRCFALeader = .RCFALeader
                'State
                Dim lookUp As New FunctionalLocationLookup
                Dim AssignLeader As String = lookUp.GetNotificationType(Me._ddlFacility.SelectedValue, Me._ddlBusinessUnit.SelectedValue, Me._ddlLineBreak.SelectedValue, userProfile.Username)
                lookUp = Nothing
                If AssignLeader.Length > 0 Then
                    .RCFALeader = Me._ddlAnalysisLead.SelectedValue
                    If .RCFALeader.Length = 0 Then .RCFALeader = "NONE"
                    If Me._ddlAnalysisLead.SelectedItem IsNot Nothing Then
                        .RCFALeaderName = Me._ddlAnalysisLead.SelectedItem.Text
                    Else
                        .RCFALeaderName = "NONE"
                    End If
                End If
                .RCFAAnalysisCompDate = Me._txtAnalysisCompleted.Text
                .RCFAActionCompDate = Me._txtCorrectiveActionsCompleted.Text

                'Location
                .SiteID = Me._ddlFacility.SelectedValue
                Dim busArea As String() = Split(Me._ddlBusinessUnit.SelectedValue, "-")
                If busArea.Length = 2 Then
                    .BusinessUnit = busArea(0).Trim
                    .Area = busArea(1).Trim
                End If
                Dim line As String() = Split(Me._ddlLineBreak.SelectedValue, "-")
                If line.Length = 2 Then

                    If line(1).Trim.Length = 0 Or line(1).ToUpper.Trim = "NONE" Then
                        .Linebreak = DBNull.Value.ToString
                    Else
                        .Linebreak = line(1).Trim
                    End If
                    .Line = line(0).Trim
                ElseIf line.Length = 1 Then
                    .Linebreak = DBNull.Value.ToString
                    .Line = line(0).Trim
                End If
                .FunctionalLocation = RI.SharedFunctions.DataClean(Me._functionalLocationTree.Text)
                '    If _rblConstrainedAreas.SelectedItem Is Not Nothing Then
                ' currentRI.ConstrainedAreas = _rblConstrainedAreas.SelectedValues
                ' Else
                ' currentRI.ConstrainedAreas = "No"
                ' End If

                'If _rblConstrainedAreas.SelectedItem IsNot Nothing Then
                '    currentRI.ConstrainedAreas = _rblConstrainedAreas.SelectedValue
                'Else
                '    currentRI.ConstrainedAreas = "No"
                'End If

                If _ddlCriticality.SelectedItem IsNot Nothing Then
                    currentRI.Criticality = RI.SharedFunctions.DataClean(_ddlCriticality.SelectedValue, 0)
                Else
                    currentRI.Criticality = 0
                End If
                'Incident
                .IncidentStartDate = CStr(Me._startEndCal.StartDate) '.ToShortDateString
                .IncidentEndDate = CStr(Me._startEndCal.EndDate) '.ToShortDateString
                If Convert.ToDateTime(.IncidentEndDate, New System.Globalization.CultureInfo("EN-US")) < Convert.ToDateTime(.IncidentStartDate, New System.Globalization.CultureInfo("EN-US")) Then
                    .IncidentEndDate = .IncidentStartDate
                End If
                If IsNumeric(Me._txtDownTime.Text) Then
                    .Downtime = RI.SharedFunctions.DataClean(Me._txtDownTime.Text, "0")
                Else
                    Me._txtDownTime.Text = 0
                    .Downtime = 0
                End If

                .SRR = Me._incidentType.SRR
                If .Downtime.Length = 0 Or .Downtime = "0" Or CInt(.Downtime) = 0 Then
                    'Calculate the downtime based on start and end
                    Dim cd As New CalculateDowntime
                    .Downtime = cd.Calculate(_startEndCal.StartDate, Me._startEndCal.EndDate)

                    If .Downtime >= 16 Then
                        If Me._incidentType.Recordable = "Yes" Then
                            If Me._incidentType.SRR.Length > 0 Then
                                .SRR = "Any Process DT >= 16 Hr," & Me._incidentType.SRR
                            Else
                                .SRR = "Any Process DT >= 16 Hr"
                            End If
                        End If
                    End If
                End If



                .Downtime = IP.Bids.Localization.Numbers.GetLocalizedNumber(.Downtime, "EN-US")
                .IncidentTitle = RI.SharedFunctions.DataClean(Me._txtIncidentTitle.Text)
                .Crew = Me._ddlCrew.SelectedValue
                .Shift = Me._ddlShift.SelectedValue
                .IncidentDescription = RI.SharedFunctions.DataClean(Me._txtIncidentDescription.Text)
                _txtCost.Text = Replace(_txtCost.Text, ",", "")
                _txtCostofSolution.Text = Replace(_txtCostofSolution.Text, ",", "")
                _txtAnnualizedSavings.Text = Replace(_txtAnnualizedSavings.Text, ",", "")
                _txtFinancialImpact.Text = Replace(_txtFinancialImpact.Text, ",", "")
                .Cost = RI.SharedFunctions.DataClean(Me._txtCost.Text, CStr(0)) 'FormatNumber(RI.SharedFunctions.DataClean(Me._txtCost.Text, CStr(0)), 0)
                '.LossOpportunity = FormatNumber(RI.SharedFunctions.DataClean(Me._txtLossOpportunity.Text, 0))
                .Capital = Me._ddlCapital.SelectedValue
                .CostofSolution = RI.SharedFunctions.DataClean(Me._txtCostofSolution.Text, CStr(0))
                .AnnualizedSavings = RI.SharedFunctions.DataClean(Me._txtAnnualizedSavings.Text, CStr(0))

                .TotalCost = RI.SharedFunctions.DataClean(Me._txtFinancialImpact.Text, CStr(0))

                If .RINumber.Length = 0 Then
                    previousEmailSent = False
                Else
                    'Did we send a previous email based on the rules of the saved incident?            
                    If .Recordable.ToLower = "yes" _
                    Or .RCFA.ToLower <> "no" _
                    Or (.RCFASafety.Contains("Safety Recordable") Or .RCFASafety.Contains("Reportable Release")) Then _
                    previousEmailSent = True
                    'Or .RCFASafety.Length > 0 Then _
                    sendNotificationEmail = False
                    'Or (.RCFALeader.ToLower <> OriginalRCFALeader.ToLower And OriginalRCFALeader.Length > 0) _
                End If

                'Incident Type
                '.RTS=                
                .Recordable = Me._incidentType.Recordable
                .Chronic = Me._incidentType.Chronic
                .RCFAQuality = Me._incidentType.Quality
                .RCFA = Me._incidentType.RCFA

                .CertifiedKill = Me._incidentType.CertifiedKill

                If Me._incidentType.ConstrainedAreas IsNot Nothing Then
                    If Me._incidentType.ConstrainedAreas = "Constrained Area" Then
                        .ConstrainedAreas = "Yes"
                    End If
                End If

                .RCFASafety = Me._incidentType.Safety
                .IRISNumber = Me._incidentType.IRISNumber

                'Should we send an email?              
                If .Recordable.ToLower = "yes" _
                Or .RCFA.ToLower <> "no" _
                Or (.RCFASafety.Contains("Safety Recordable") Or .RCFASafety.Contains("Reportable Release")) Then _
                'Or .RCFASafety.Length > 0 _
                    'Then
                    sendEmailFlag = True
                    If previousEmailSent = False Then sendNotificationEmail = True
                End If

                'Or (.RCFALeader.ToLower <> OriginalRCFALeader.ToLower And .RINumber.Length > 0 And .RCFALeader.Length > 0) _

                If previousEmailSent = True Then 'And sendEmailFlag = True Then 'And .RCFALeader.ToLower = OriginalRCFALeader.ToLower Then
                    sendEmailFlag = False
                End If

                If .RCFALeader.ToLower <> OriginalRCFALeader.ToLower And .RCFALeader.Length > 0 And .RCFALeader.ToUpper <> "NONE" Then
                    analysisLeaderHasChanged = True
                    sendEmailFlag = True
                End If


                'Incdent Classification
                .RCFATriggerDesc = _ddlTrigger.SelectedValue 'Me._IncidentClassification.TriggerValue
                If _IncidentClassification.TypeValue.Length > 0 Then
                    .Type = Replace(_IncidentClassification.TypeValue, ">", ":")
                Else
                    .Type = String.Empty
                End If
                If _IncidentClassification.CauseValue.Length > 0 Then
                    .Cause = Replace(_IncidentClassification.CauseValue, ">", ":")
                Else
                    .Cause = String.Empty
                End If
                If _IncidentClassification.PreventionValue.Length > 0 Then
                    .Prevention = Replace(_IncidentClassification.PreventionValue, ">", ":")
                Else
                    .Prevention = String.Empty
                End If
                If _IncidentClassification.ProcessValue.Length > 0 Then
                    .Process = Replace(_IncidentClassification.ProcessValue, ">", ":")
                Else
                    .Process = String.Empty
                End If
                If _IncidentClassification.ComponentValue.Length > 0 Then
                    .Component = Replace(_IncidentClassification.ComponentValue, ">", ":")
                Else
                    .Component = String.Empty
                End If
                'Other
                .RCFAFailedLocation = Me._ddlFailedMaterial.SelectedValue
                .RCFACondition = RI.SharedFunctions.DataClean(Me._txtConditionsInfluencingFailure.Text)
                .RCFAPeople = RI.SharedFunctions.DataClean(Me._txtPeopleToInterview.Text)
                .RCFATeamMembers = RI.SharedFunctions.DataClean(Me._txtTeamMembers.Text)

                ''Failure Classification
                '.ClassChronic = Me._rblClassChronic.SelectedValue
                '.ClassCost = Me._rblClassCost.SelectedValue
                '.ClassImpact = Me._rblClassRepair.SelectedValue
                '.ClassSchedule = Me._rblClassPlanned.SelectedValue
                '.ClassLife = Me._rblClassLife.SelectedValue

                'New Failure Classification
                .ClassificationConstrainedAreas = Me._FailureClassification.ConstrainedAreas
                .ClassificationCriticality = Me._FailureClassification.CriticalityRating
                .ClassificationEquipmentCare = Me._FailureClassification.EquipmentCare
                .ClassificationLifeExpectancy = Me._FailureClassification.LifeExpectancy

                'Document Causes
                .HumanCauses = RI.SharedFunctions.GetCheckBoxValues(Me._cblHuman)
                .OtherHumanCauses = Me._txtOtherHuman.Text
                .PhysicalCauses = RI.SharedFunctions.GetCheckBoxValues(Me._cblPhysical)
                .OtherLatentCauses = Me._txtOtherLatent.Text
                .LatentCauses = RI.SharedFunctions.GetCheckBoxValues(Me._cblLatentCauses)
                .OtherPhysicalCauses = Me._txtOtherPhysical.Text

                'Verification CAC 7/16/14
                '.VerificationComment = Me._tbVerComment.Text
                .Verification = Me._rblVerification.SelectedValue
                .VerificationResp = Me._ddlPerson.SelectedValue
                .VerificationWeeksAfter = Me._tbWeeksAfter.Text

                .SchedUnsched = Me._incidentType.SchedUnsched


                .UserName = userProfile.Username
                If Request.QueryString("RINumber") IsNot Nothing Then
                    .RINumber = Request.QueryString("RINumber")
                End If
                .RINumber = .SaveIncident()
                If sendEmailFlag = True And sendEmail = True Then
                    If (.RCFASafety.Contains("Safety Recordable") Or .RCFASafety.Contains("Reportable Release")) Then
                        sendEHSEmail = True
                        EmailIncident(, , analysisLeaderHasChanged, sendNotificationEmail, sendEHSEmail)
                    Else
                        EmailIncident(, , analysisLeaderHasChanged, sendNotificationEmail, sendEHSEmail)
                    End If

                End If


                If .IRISNumber.Length = 0 And .RCFASafety.Length > 0 And (.RCFASafety.Contains("Safety Recordable")) Then
                    'Check to see if this is a new incident
                    If Request.QueryString("RINumber") Is Nothing Then
                        'New Incident
                        'Determine if this is a Safety or Environmental event
                        Dim eventType As String = Me._incidentType.GetEHSType
                        Dim taskDesc As String = "Enter incident into IRIS and update RI with IRIS number"
                        Dim taskResource As String = .GetEHSNotificationList("Millwide", eventType)
                        Dim dtEstDate As String = FormatDateTime(Now.AddHours(24), DateFormat.ShortDate)
                        'Create Corrective Action
                        Dim TaskUserName(1) As String
                        If taskResource.Length = 0 Then
                            TaskUserName(1) = userProfile.Email
                            TaskUserName(0) = userProfile.Username
                        Else
                            TaskUserName = taskResource.Split("-")
                        End If
                        'If taskResource.Length = 0 Then taskResource = userProfile.Username
                        'Dim TaskUserName() As String = taskResource.Split("@")
                        .CreateCorrectiveAction(TaskUserName(0), "High", taskDesc, "", "0", "", "", "", dtEstDate, DBNull.Value.ToString)
                        'Send Email
                        Dim emailSubject As String = "New Action Item Assigned to you from " & .RINumber & ": " & .IncidentTitle
                        Dim emailFrom As String = userProfile.Email
                        Dim emailTo As String = TaskUserName(1) 'taskResource '& "@ipaper.com"
                        Dim emailSB As New StringBuilder
                        Dim urlHost As String = String.Empty

                        emailSB.Append("<html><head><title>Action Item</title></head>")
                        emailSB.Append("<body bgcolor=""#FFFFFF"">")
                        emailSB.Append("<p><strong>The following Action Item has been assigned to you:</strong></p>")
                        emailSB.Append("<ul>")
                        emailSB.Append("<li><strong>Task Description:&nbsp;</strong>")
                        emailSB.Append(taskDesc)
                        emailSB.Append("</li>")
                        emailSB.Append("<li><strong>")
                        emailSB.Append("Priority:&nbsp;</strong> High</li>")
                        emailSB.Append("<li><strong>")
                        emailSB.Append("DueDate:&nbsp;</strong>")
                        emailSB.Append(dtEstDate)
                        emailSB.Append("</li><li><strong>")
                        emailSB.Append("Additional Information:&nbsp;</strong>None</li></ul>")
                        emailSB.Append("<p><A HREF={0}EnterNewRI.aspx?RINumber={1}>" & "Click here to Review Incident and Update Corrective Action Task</A></p></body></html>")
                        If Request.UserHostAddress = "127.0.0.1" Then
                            urlHost = "Http://ridev.ipaper.com/ri/ri/"
                        Else
                            If Request.ServerVariables("HTTP_HOST").ToLower.Contains("ridev") Then
                                urlHost = "Http://ridev.ipaper.com/ri/ri/"
                            ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritest") Then
                                urlHost = "Http://ritest.ipaper.com/ri/ri/"
                            ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritrain") Then
                                urlHost = "Http://ritrain.ipaper.com/ri/ri/"
                            Else
                                'urlHost = "Http://rigpi.ipaper.com/ri/ri/"
                                urlHost = "http://gpiri.graphicpkg.com/ri/"
                            End If

                        End If
                        Dim emailBody As String = emailSB.ToString
                        emailBody = String.Format(emailBody, urlHost, .RINumber)
                        RI.SharedFunctions.SendEmail(emailTo, emailFrom, emailSubject, emailBody, userProfile.FullName)
                    End If
                End If

                If .RINumber.Length > 0 And refreshPage = True And .RINumber <> Request.QueryString("RINumber") Then
                    Response.Clear()
                    Response.Redirect(Page.AppRelativeVirtualPath & "?RINumber=" & .RINumber, True)
                    Response.End()
                    'Server.Transfer(Page.AppRelativeVirtualPath & "?RINumber=" & .RINumber)
                    'Dim script As String = "window.location='" & Page.AppRelativeVirtualPath & "?RINumber=" & .RINumber & "'"
                    'Page.ClientScript.RegisterStartupScript(Page.GetType, "UpdateIncident", script, True)
                Else
                    currentRI = New clsCurrentRI(.RINumber)
                End If
            End With
        End If
    End Sub

    Protected Sub _messageBox_OKClick() Handles _messageBox.OKClick
        If Request.QueryString("RINumber") IsNot Nothing Then
            clsCurrentRI.MarkAnalysisComplete(userProfile.Username, Request.QueryString("RINumber"))
            EmailIncident(True)
            'Server.Transfer(Page.AppRelativeVirtualPath & "?RINumber=" & Request.QueryString("RINumber"))
            Response.Clear()
            Response.Redirect(Page.AppRelativeVirtualPath & "?RINumber=" & Request.QueryString("RINumber"), True)
            Response.End()
        End If
    End Sub

    Protected Sub _btnChangeAnalysisOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnChangeAnalysisOK.Click
        Dim RINumber As String = String.Empty
        Me._txtAnalysisCompleted.Text = String.Empty
        If Request.QueryString("RINumber") IsNot Nothing Then
            RINumber = Request.QueryString("RINumber")
            currentRI = New clsCurrentRI(RINumber)
            If currentRI IsNot Nothing Then
                clsCurrentRI.MarkAnalysisIncomplete(userProfile.Username, RINumber)
            End If
            EmailIncident(False, True)
        End If
        Response.Clear()
        Response.Redirect(Page.AppRelativeVirtualPath & "?RINumber=" & RINumber, True)
        Response.End()
    End Sub

    Private Function GetGlobalJSVar() As String
        Dim sb As New StringBuilder

        sb.Append("var facilityClient = $get('")
        sb.Append(Me._ddlFacility.ClientID)
        sb.Append("');")

        sb.AppendLine()
        sb.Append("var currentUserName = '")
        sb.Append(GetUser())
        sb.Append("';")
        sb.AppendLine()
        sb.Append("var btnSubmit =$get('")
        sb.Append(_btnSubmit.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var btnDelete =$get('")
        sb.Append(_btnDelete.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var btnSpell=$get('")
        sb.Append(_btnSpell.ClientID)
        sb.Append("__btnSpell');")
        sb.AppendLine()
        sb.Append("var facilitiesAllowedToUpdate ='")
        sb.Append(FacilitiesAllowedToUpdate())
        sb.Append("';")
        sb.AppendLine()
        sb.Append("var busArea = $get('")
        sb.Append(_ddlBusinessUnit.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var lineBreak = $get('")
        sb.Append(_ddlLineBreak.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var ddlAnalysisLead =$get('")
        sb.Append(_ddlAnalysisLead.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var lastAnalysisLead =$get('")
        sb.Append(_ddlAnalysisLead.ClientID)
        sb.Append("').value;")
        sb.AppendLine()
        sb.Append("var txtAnalysisLead =$get('")
        sb.Append(_txtAnalysisLead.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var lblCommentsForEmail=$get('")
        sb.Append(_lblCommentsForEmail.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var txtForEmail=$get('")
        sb.Append(_txtForEmail.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var txtAutoComplete =$get('")
        sb.Append(_functionalLocationTree.ClientID)
        sb.Append("__txtFunctionalLocationSearch');")
        sb.AppendLine()
        sb.Append("var txtAutoComplete2 =$get('")
        sb.Append(_functionalLocationTree.ClientID)
        sb.Append("__txtFunctionalLocationSearch2');")

        sb.Append("var startDate= $get('")
        sb.Append(Me._startEndCal.ClientID)
        sb.Append("__txtStartDate');")
        sb.AppendLine()
        sb.Append("var startHrs=$get('")
        sb.Append(Me._startEndCal.ClientID)
        sb.Append("__ddlStartHrs');")
        sb.AppendLine()
        sb.Append("var startMins=$get('")
        sb.Append(Me._startEndCal.ClientID)
        sb.Append("__ddlStartMins');")
        sb.AppendLine()

        sb.Append("var endDate= $get('")
        sb.Append(Me._startEndCal.ClientID)
        sb.Append("__txtEndDate');")
        sb.AppendLine()
        sb.Append("var endHrs=$get('")
        sb.Append(Me._startEndCal.ClientID)
        sb.Append("__ddlEndHrs');")
        sb.AppendLine()
        sb.Append("var endMins=$get('")
        sb.Append(Me._startEndCal.ClientID)
        sb.Append("__ddlEndMins');")
        sb.AppendLine()
        sb.Append("var downtime=$get('")
        sb.Append(Me._txtDownTime.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var finclimpact=$get('")
        sb.Append(Me._txtFinancialImpact.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var imgTextForEmail=$get('")
        sb.Append(Me._imgTextForEmail.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var ddlCriticality =$get('")
        sb.Append(_ddlCriticality.ClientID)
        sb.Append("');")
        sb.Append("var rblConstrainedAreas =$get('")
        sb.Append(Me._incidentType.ConstrainedAreasID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var cblSRR =$get('")
        sb.Append(Me._incidentType.SRRID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var rblRecordable =$get('")
        sb.Append(Me._incidentType.RecordableID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var titleClient = $get('")
        sb.Append(Me._txtIncidentTitle.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var description = $get('")
        sb.Append(Me._txtIncidentDescription.ClientID)
        sb.Append("');")

        sb.AppendLine()
        Return sb.ToString
    End Function

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        PopulateData()
        PopulateStaticData()
        If currentRI IsNot Nothing Then
            'Populate Screen with current RI values
            GetIncident()
            Me._btnHistory.RINumber = currentRI.RINumber
            Me._btnHistory.RefreshHistory()
        End If
    End Sub



End Class
