
Partial Class RI_EnterSimpleRI
    Inherits RIBasePage


    Dim enterRI As clsEnterSimpleRI
    Dim currentRI As clsCurrentSimpleRI
    Dim selectedFacility As String = String.Empty
    Dim selectedBusArea As String = String.Empty
    Dim selectedLine As String = String.Empty
    Dim userProfile As RI.CurrentUserProfile = Nothing


    'Private Sub DisplayUpdateMenu()
    '    Dim ret As String = "return false;"
    '    Dim urlhost As String
    '    Dim confirmMessage As String = "localizedText.ConfirmRedirect"

    '    If Request.UserHostAddress = "127.0.0.1" Then
    '        urlhost = "Http://ridev.ipaper.com/"
    '    Else
    '        If Request.ServerVariables("HTTP_HOST").ToLower.Contains("ridev") Then
    '            urlhost = "Http://ridev.ipaper.com/"
    '        ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritest") Then
    '            urlhost = "Http://ritest.ipaper.com/"
    '        ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritrain") Then
    '            urlhost = "Http://ritrain.ipaper.com/"
    '        Else
    '            urlhost = "Http://rigpi.ipaper.com/"
    '        End If
    '    End If

    'End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            _IncidentClassification.ShowPrevention = False
            userProfile = RI.SharedFunctions.GetUserProfile
            RI.SharedFunctions.DisablePageCache(Response)

            DisplayViewSearch()

            Dim sc As ScriptManager
            sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
            If sc IsNot Nothing Then
                Dim calService As New ServiceReference
                calService.InlineScript = False
                calService.Path = "~/CalculateDowntime.asmx"
                sc.Services.Add(calService)

            End If

            ScriptManager.RegisterStartupScript(Me._udpLocation, _udpLocation.GetType, "GetGlobalJSVar", GetGlobalJSVar, True)

            ScriptManager.RegisterClientScriptInclude(Me._udpLocation, _udpLocation.GetType, "EnterNewRI", Page.ResolveClientUrl("~/ri/EnterNewRI.js"))

            Dim iploc As New IP.Bids.Localization.WebLocalization

            If Request.QueryString("RINumber") IsNot Nothing And IsNumeric(Request.QueryString("RINumber")) Then
                currentRI = New clsCurrentSimpleRI(Request.QueryString("RINumber"))
                If currentRI.SiteID Is Nothing Then currentRI = Nothing
                If currentRI IsNot Nothing Then
                    enterRI = New clsEnterSimpleRI(userProfile.Username, currentRI.SiteID, userProfile.InActiveFlag, , currentRI.BusinessUnit, currentRI.Linebreak, Request.QueryString("RINumber"))

                    If Page.IsPostBack Then
                        selectedFacility = Me._ddlFacility.SelectedValue
                        selectedBusArea = Me._ddlBusinessUnit.SelectedValue
                        selectedLine = Me._ddlLineBreak.SelectedValue
                    Else
                        selectedFacility = currentRI.SiteID
                        selectedBusArea = currentRI.BusinessUnit
                        selectedLine = currentRI.Linebreak
                    End If
                Else
                    Response.Clear()
                    Response.Redirect("~/ri/EnterSimpleri.aspx", True)
                    Response.End()
                End If

            Else 'New DT Record
                If Page.IsPostBack Then
                    selectedFacility = Me._ddlFacility.SelectedValue
                    selectedBusArea = Me._ddlBusinessUnit.SelectedValue
                    selectedLine = Me._ddlLineBreak.SelectedValue
                Else
                    selectedFacility = userProfile.DefaultFacility
                    Dim englishDate As String = IP.Bids.Localization.DateTime.GetLocalizedDateTime(Now, "EN-US", "d")
                    Me._startEndCal.StartDate = englishDate
                    Me._startEndCal.EndDate = englishDate
                End If

                enterRI = New clsEnterSimpleRI(userProfile.Username, selectedFacility, userProfile.InActiveFlag, userProfile.DefaultDivision, selectedBusArea, selectedLine)

                Me._lblCreatedBy.Text = String.Format(Master.RIRESOURCES.GetResourceValue("CreatedBy", True, "Shared"), Master.RIRESOURCES.GetResourceValue("None", True, "Shared"))
                Me._lblCreatedDate.Text = String.Format(Master.RIRESOURCES.GetResourceValue("CreationDate", True, "Shared"), Master.RIRESOURCES.GetResourceValue("None", True, "Shared"))
                Me._lblLastUpdateDate.Text = String.Format(Master.RIRESOURCES.GetResourceValue("LastUpdateDate", True, "Shared"), Master.RIRESOURCES.GetResourceValue("None", True, "Shared"))
                Me._lblUpdatedBy.Text = String.Format(Master.RIRESOURCES.GetResourceValue("LastUpdatedBy", True, "Shared"), Master.RIRESOURCES.GetResourceValue("None", True, "Shared"))
            End If

            If enterRI IsNot Nothing Then
                If enterRI.CurrentPageMode = clsEnterNewRI.PageMode.NewRI Then
                    Master.SetBanner(Master.RIRESOURCES.GetResourceValue("EnterDowntime", True, "Shared"))
                    _btnSubmit.Text = Master.RIRESOURCES.GetResourceValue("SaveNewDowntime", True, "Shared")
                    If Request.QueryString("OrigApp") = "PI" Then
                        _txtIncidentTitle.Text = "Availablity"
                        _txtIncidentTitle.ReadOnly = True
                    End If
                Else
                    Master.SetBanner(Master.RIRESOURCES.GetResourceValue("UpdateDowntime", True, "Shared") & " '" & currentRI.RINumber & "'")
                    _btnSubmit.Text = Master.RIRESOURCES.GetResourceValue("SaveDowntime", True, "Shared")
                    _btnNewDT.Visible = True
                    If enterRI.IncidentSecurity.DeleteIncidents Then
                        _btnDelete.Visible = True
                    End If
                    'DisplayUpdateMenu()
                End If
            End If

            _btnSubmit.OnClientClick = "ConfirmBeforeLeave=false;"
            Me._btnDelete.OnClientClick = "ConfirmBeforeLeave=false;"

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

            _cddlFacility.LoadingText = " ... "
            _cddlBusArea.LoadingText = " ... "
            _cddlLineBreak.LoadingText = " ... "

            Dim ci As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentCulture
            Dim validChars As String = Join(ci.NumberFormat.NativeDigits, "") & ci.NumberFormat.NumberDecimalSeparator

            FilteredTextBoxExtender3.ValidChars = validChars
            FilteredTextBoxExtender3.FilterInterval = 1
            _revDowntime.ValidationExpression = String.Format("^-?\d*(\{0}\d+)?$", ci.NumberFormat.NumberDecimalSeparator)
            _revDowntime.ErrorMessage = Master.RIRESOURCES.GetResourceValue("ValidDowntime", False, "Shared")

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub DisplayViewSearch()
        'Dim dr As Data.OracleClient.OracleDataReader = Nothing
        Try
            Dim clsSearch As clsViewSearch = CType(Session("clsSearch"), clsViewSearch)
            If clsSearch IsNot Nothing Then
                ' Me._btnViewSearch.Visible = True
                ' Me._btnViewSearch.OnClientClick = "displayViewListWindow('" & Page.ResolveClientUrl("~/RI/ViewList.aspx") & "','" & Master.RIRESOURCES.GetResourceValue("ViewList", False, "Shared") & "'); return false;"
            End If
        Catch ex As Exception

        Finally
        End Try
    End Sub
    Private Sub GetIncident()
        If currentRI IsNot Nothing Then
           With currentRI
                'State
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

                If .schedDT = "Yes" Then
                    Me._rblSchedUnsched.Items.FindByValue("Scheduled").Selected = True
                Else
                    Me._rblSchedUnsched.Items.FindByValue("Unscheduled").Selected = True
                End If

                If _ddlFacility.Items.FindByValue(.SiteID) IsNot Nothing Then Me._ddlFacility.SelectedValue = .SiteID
                If _ddlBusinessUnit.Items.FindByValue(.BusinessUnit) IsNot Nothing Then Me._ddlBusinessUnit.SelectedValue = .BusinessUnit
                If _ddlLineBreak.Items.FindByValue(.Linebreak) IsNot Nothing Then Me._ddlLineBreak.SelectedValue = .Linebreak
                Me._cddlFacility.SelectedValue = .SiteID
                Me._cddlLineBreak.SelectedValue = .Linebreak
                Me._cddlBusArea.SelectedValue = .BusinessUnit

                _IncidentClassification.TypeValue = .Type
                _IncidentClassification.CauseValue = .Cause
                _IncidentClassification.PreventionValue = .Prevention
                _IncidentClassification.ProcessValue = .Process
                _IncidentClassification.ComponentValue = .Component
               
                'Incident       
                Me._startEndCal.StartDate = .IncidentStartDate
                Me._startEndCal.EndDate = .IncidentEndDate
                If IsNumeric(.Downtime) Then
                    Me._txtDownTime.Text = IP.Bids.Localization.Numbers.GetLocalizedNumber(.Downtime)
                Else
                    Me._txtDownTime.Text = 0
                End If
                Me._txtIncidentTitle.Text = .IncidentTitle

            End With
        End If
    End Sub

    Private Sub PopulateData()
        If enterRI IsNot Nothing Then
            If _ddlFacility.SelectedValue.Length = 0 Then
                SetDefaults()
            End If

            If userProfile.DistinguishedName.ToUpper.Contains("LOVELAND") Or userProfile.DistinguishedName.ToUpper.Contains("MEMPHIS") Or userProfile.DefaultFacility = selectedFacility Or userProfile.DefaultFacility.ToUpper = "AL" Then
                _btnSpell.Visible = True
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

    Private Sub GetControlState()
        If Request.Form(Me._ddlFacility.UniqueID) IsNot Nothing Then
            selectedFacility = Request.Form(Me._ddlFacility.UniqueID)
        End If
        If Request.Form(Me._ddlBusinessUnit.UniqueID) IsNot Nothing Then
            selectedBusArea = Request.Form(Me._ddlBusinessUnit.UniqueID)
        End If
        If Request.Form(Me._ddlLineBreak.UniqueID) IsNot Nothing Then
            selectedLine = Request.Form(Me._ddlLineBreak.UniqueID)
        End If
    End Sub

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

    'Protected Sub _IncidentClassification_IncidentClassificationChanged() Handles _IncidentClassification.IncidentClassificationChanged
    '    selectedFacility = Me._ddlFacility.SelectedValue
    '    selectedBusArea = Me._ddlBusinessUnit.SelectedValue
    '    selectedLine = Me._ddlLineBreak.SelectedValue

    '    Me.PopulateData()
    'End Sub

    Protected Sub _btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnDelete.Click
        If Request.QueryString("RINumber") IsNot Nothing Then
            Dim riNumber As Integer = CInt(Request.QueryString("RINumber"))
            If riNumber > 0 Then
                clsCurrentRI.DeleteCurrentRI(CInt(riNumber), userProfile.Username)
                Response.Redirect("~/ri/ViewUpdateSearch.aspx", True)
            End If
        Else
            Response.Clear()
            Response.Redirect("~/ri/EnterSimpleRI.aspx", True)
            Response.End()
        End If
    End Sub

    Private Sub SaveIncident(Optional ByVal refreshPage As Boolean = True, Optional ByVal sendEmail As Boolean = True, Optional ByVal riNumber As String = "")
        If Page.IsValid Then
            currentRI = New clsCurrentSimpleRI(riNumber)

            With currentRI

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

                'If .Downtime.Length = 0 Or .Downtime = "0" Or CInt(.Downtime) = 0 Then
            '    'Calculate the downtime based on start and end
            '    Dim cd As New CalculateDowntime
            '    .Downtime = cd.Calculate(_startEndCal.StartDate, Me._startEndCal.EndDate)

            '    If .Downtime >= 16 Then
            '        If Me._incidentType.Recordable = "Yes" Then
            '            If Me._incidentType.SRR.Length > 0 Then
            '                .SRR = "Any Process DT >= 16 Hr," & Me._incidentType.SRR
            '            Else
            '                .SRR = "Any Process DT >= 16 Hr"
            '            End If
            '        End If
            '    End If
            'End If

                .Downtime = IP.Bids.Localization.Numbers.GetLocalizedNumber(.Downtime, "EN-US")
                .IncidentTitle = RI.SharedFunctions.DataClean(Me._txtIncidentTitle.Text)
            
                'Incdent Classification
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

                If Me._rblSchedUnsched.Items.FindByValue("Scheduled").Selected = True Then
                    .schedDT = "Yes"
                Else
                    .schedDT = "No"
                End If

                .origApp = Request.QueryString("OrigApp")

                .UserName = userProfile.Username
                If Request.QueryString("RINumber") IsNot Nothing Then
                    .RINumber = Request.QueryString("RINumber")
                End If
                .RINumber = .SaveIncident()

                If .RINumber.Length > 0 And refreshPage = True And .RINumber <> Request.QueryString("RINumber") Then
                    Response.Clear()
                    Response.Redirect(Page.AppRelativeVirtualPath & "?RINumber=" & .RINumber & "&OrigApp=" & Request.QueryString("OrigApp"), True)
                    Response.End()
                Else
                    currentRI = New clsCurrentSimpleRI(.RINumber)
                End If

            End With
        End If
    End Sub


    Private Function GetGlobalJSVar() As String
        Dim sb As New StringBuilder

        sb.Append("var facilityClient = $get('")
        sb.Append(Me._ddlFacility.ClientID)
        sb.Append("');")

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
        sb.Append("var busArea = $get('")
        sb.Append(_ddlBusinessUnit.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var lineBreak = $get('")
        sb.Append(_ddlLineBreak.ClientID)
        sb.Append("');")
        sb.AppendLine()

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
        Return sb.ToString
    End Function

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        PopulateData()
        If currentRI IsNot Nothing Then
            'Populate Screen with current RI values
            GetIncident()
        End If
    End Sub

    Private Sub SetDefaults()
        Dim defaults As New RIUserDefaults.CurrentUserDefaults(userProfile.Username, RIUserDefaults.Applications.Availability, ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString)

        If defaults.DoesDefaultValueExist(RIUserDefaults.UserProfileTypes.SiteId) Then
            Me._cddlFacility.SelectedValue = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.SiteId)
        End If

        If defaults.DoesDefaultValueExist(RIUserDefaults.UserProfileTypes.BusinessUnit) Then
            Me._cddlBusArea.SelectedValue = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.BusinessUnit) & " - " & defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.Area)
        End If

        If defaults.DoesDefaultValueExist(RIUserDefaults.UserProfileTypes.Line) Then
            Me._cddlLineBreak.SelectedValue = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.Line)
        End If

    End Sub

    Protected Sub _btnNewDT_Click(sender As Object, e As EventArgs) Handles _btnNewDT.Click
        Response.Clear()
        Response.Redirect("~/ri/EnterSimpleri.aspx", True)
        Response.End()
    End Sub
End Class
