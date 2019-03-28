Imports RI
Imports RI.SharedFunctions
Imports Devart.Data.Oracle
Imports System.Data



Partial Class RI_Workspace
    Inherits RIBasePage

    Dim currentRIWorkspace As clsRIWorkspace
    Dim currentWorkspaceEntry As clsRIWorkspaceNew ' As clsRIWorkspace
    Dim failureEvents As clsRIWorkspace
    Dim userProfile As RI.CurrentUserProfile = Nothing
    Dim strRINumber As String
    Dim intAWCount As Integer
    Dim strRIEventSeqID As String
    Dim modesChanged, causesChanged, reasonsChanged, rootcausesChanged As Boolean


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("AWTitle", True))
        Master.HideMainMenu()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            userProfile = RI.SharedFunctions.GetUserProfile
            strRINumber = Request.QueryString("RINumber")
            intAWCount = Request.QueryString("awcount")
            strRIEventSeqID = Request.QueryString("eventseqid")

            Dim sc As ScriptManager
            sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
            If sc IsNot Nothing Then
                Dim loService As New ServiceReference
                loService.InlineScript = True
                loService.Path = "~/WSCascadingLists.asmx"
                sc.Services.Add(loService)
            End If

            If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "AWNew") Then
                Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "AWNew", Page.ResolveClientUrl("~/RI/AWFailureEvent.js"))
            End If

            If strRINumber IsNot Nothing Then
                currentRIWorkspace = New clsRIWorkspace(strRINumber, strRIEventSeqID)
                'If an analysis has been entered or started, show listing of records.  Otherwise, show Failure Event DDL.
                If strRIEventSeqID IsNot Nothing Then
                    'populateFailureEvent()
                    Me._btnAdd.Visible = "false"
                    Me._tblEvent.Visible = "true"
                    If currentRIWorkspace.EventSEQ = "" Then
                        Me._tbFailureEvent.Text = currentRIWorkspace.EventDesc
                        Me._ddlFailureEvent.SelectedValue = ""
                        Me._ddlFailureEvent.Enabled = "false"

                    Else
                        Me._ddlFailureEvent.SelectedValue = currentRIWorkspace.EventSEQ
                        Me._tbFailureEvent.Enabled = "false"
                    End If


                    'If currentRIWorkspace.ModesDT.HasRows = True Then
                    '    Do While currentRIWorkspace.ModesDT.Read()
                    '        For i As Integer = 0 To currentRIWorkspace.ModesDT.FieldCount - 1
                    '            ' Me._tbFailureModeOther.Text = currentRIWorkspace.ModesDT("failuremodeother")
                    '        Next
                    '    Loop
                    'End If

                    'If currentRIWorkspace.CausesDT.HasRows = True Then
                    ' Me._gvFailureCause.DataSource = currentRIWorkspace.CausesDT
                    ' Me._gvFailureCause.DataBind()
                    'End If

                    Me._tblMode.Visible = "true"
                    'Me._tblProvenFailureCause.Visible = "true"

                Else
                    If currentRIWorkspace.RIFailureEvents IsNot Nothing And intAWCount > 0 Then
                        BuildRepeater()
                        Me._tblEvent.Visible = "false"
                        Me._btnAdd.Visible = "true"
                    Else
                        If Not Page.IsPostBack Then
                            _btnAddModes.Visible = "true"
                            Me._tblEvent.Visible = "true"
                            'Me._tblProvenFailureCause.Visible = "true"
                            Me._tblMode.Visible = "true"
                            Me._tbFailureModeOther.Visible = "false"
                            'Me._tblMostLikelyFailureCause.Visible = "true"
                            'Me._tblRootCause.Visible = "true"
                            _divUpdateAWObjects.Visible = "true"
                            Me._tbEventUpdate.Visible = "false"
                            '_tbFailureModeUpdate2.Visible = "false"
                            Me._tbModeUpdate.Visible = "false"
                            Me._tbCauseUpdate.Visible = "true"
                            Me._tbReasonUpdate.Visible = "true"
                            Me._tblRootCauseUpdate.Visible = "true"
                            _btnList.Visible = "true"
                            btnSpellCheck.Visible = "true"
                        Else
                            Me._tblMode.Visible = "true"
                            Me._tbFailureModeOther.Visible = "true"

                        End If
                    End If

                        If Not Page.IsPostBack Then
                            currentRIWorkspace.GetFailureEvents()
                            If currentRIWorkspace.FailureEvents.DataSource.HasRows = True Then
                                RI.SharedFunctions.BindList(Me._ddlFailureEvent, currentRIWorkspace.FailureEvents, True, True)
                            End If
                            Me._ddlFailureEvent.SelectedValue = ""

                        End If

                End If

            End If

            'Me._ddlFailureEvent.Attributes.Add("onchange", "DisableEnable('" & Me._ddlFailureEvent.ClientID & "','" & Me._tbFailureEvent.ClientID & "');")
            Me._tbFailureEvent.Attributes.Add("onchange", "DisableEnable('" & Me._ddlFailureEvent.ClientID & "','" & Me._tbFailureEvent.ClientID & "');")
            Me._tbFailureModeOther.Attributes.Add("onblur", "ShowNextTextBox('" & Me._tbFailureModeOther.ClientID & "','" & Me._tbFailureModeOther2.ClientID & "');")
            Me._tbFailureModeOther2.Attributes.Add("onblur", "ShowNextTextBox('" & Me._tbFailureModeOther2.ClientID & "','" & Me._tbFailureModeOther3.ClientID & "');")
            Me._tbFailureModeOther3.Attributes.Add("onblur", "ShowNextTextBox('" & Me._tbFailureModeOther3.ClientID & "','" & Me._tbFailureModeOther4.ClientID & "');")
            Me._tbFailureModeOther4.Attributes.Add("onblur", "ShowNextTextBox('" & Me._tbFailureModeOther4.ClientID & "','" & Me._tbFailureModeOther5.ClientID & "');")


            If Me._ddlFailureEvent.SelectedValue = "" And _tbFailureEvent.Text <> "" Then
                _tbFailureEvent.Enabled = True
                _ddlFailureEvent.Enabled = False
            ElseIf Me._ddlFailureEvent.SelectedValue <> "" And _tbFailureEvent.Text = "" Then
                _tbFailureEvent.Enabled = False
                _ddlFailureEvent.Enabled = True
            End If


        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub _ddlFailureEvent_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlFailureEvent.SelectedIndexChanged
        If _ddlFailureEvent.SelectedValue = "" Then
            _lblFailureModeDesc.Text = "Enter ALL the Ways This Type of Failure Event Can Occur or Has Ocurred in the Past"
            _tblMode.Visible = False
        Else
            populateFailureMode()
        End If
    End Sub

    Sub populateFailureMode()
        Dim FailureEventSeq As String = _ddlFailureEvent.SelectedValue

        Try
            _tblMode.Visible = True

            currentRIWorkspace.GetFailureModes(FailureEventSeq)
            If currentRIWorkspace.FailureModes.DataSource.HasRows = True Then

                RI.SharedFunctions.BindList(Me._cblFailureMode, currentRIWorkspace.FailureModes, True, False)
            End If

            'If Me._hfFailureEvent.Value <> "" Then
            '    Dim clsEvent As New clsRIWorkspaceNew
            '    clsEvent.RIFailureSeq = Me._hfFailureEvent.Value
            '    clsEvent.GetRIFailureModes()
            '    RI.SharedFunctions.SetCheckBoxValues(Me._cblFailureMode, clsEvent.RIFailureModesSelected)


            '    If clsEvent.RIFailureModeDesc <> "" Then
            '        Me._tbFailureModeOther.Text = clsEvent.RIFailureModeDesc
            '        Me._tbFailureModeOther.Visible = True
            '    End If
            '    If clsEvent.RIFailureModeDesc2 <> "" Then
            '        Me._tbFailureModeOther2.Text = clsEvent.RIFailureModeDesc2
            '        Me._tbFailureModeOther2.Visible = True
            '    End If
            '    If clsEvent.RIFailureModeDesc3 <> "" Then
            '        Me._tbFailureModeOther3.Text = clsEvent.RIFailureModeDesc3
            '        Me._tbFailureModeOther3.Visible = True
            '    End If
            '    If clsEvent.RIFailureModeDesc4 <> "" Then
            '        Me._tbFailureModeOther4.Text = clsEvent.RIFailureModeDesc4
            '        Me._tbFailureModeOther4.Visible = True
            '    End If
            '    If clsEvent.RIFailureModeDesc5 <> "" Then
            '        Me._tbFailureModeOther5.Text = clsEvent.RIFailureModeDesc5
            '        Me._tbFailureModeOther5.Visible = True
            '    End If

            'End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("PopulateFailureMode", , ex)
        End Try
    End Sub

    Sub populateFailureModeUpdate()
        Dim FailureEventSeq As String = _ddlFailureEventUpdate.SelectedValue

        Try
            currentRIWorkspace.GetFailureModes(FailureEventSeq)
            If currentRIWorkspace.FailureModes.DataSource.HasRows = True Then
                RI.SharedFunctions.BindList(Me._cblFailureModeUpdate, currentRIWorkspace.FailureModes, True, False)
            End If

            If Me._hfFailureEvent.Value <> "" Then
                Dim clsEvent As New clsRIWorkspaceNew
                clsEvent.RIFailureSeq = Me._hfFailureEvent.Value
                clsEvent.GetRIFailureModes()
                RI.SharedFunctions.SetCheckBoxValues(Me._cblFailureModeUpdate, clsEvent.RIFailureModesSelected)

                _gvFailureModeUpdate.DataSource = clsEvent.GetRIFailureModeDesc
                _gvFailureModeUpdate.DataBind()
                _gvFailureModeUpdate.Visible = True

            End If
            _tbEventUpdate.Visible = "true"
            _tbModeUpdate.Visible = "true"

            _rpAW.Visible = "false"

        Catch ex As Exception
            RI.SharedFunctions.HandleError("PopulateFailureModeUpdate", , ex)
        End Try
    End Sub


    'Protected Sub _btnAddEvent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnAddEvent.Click
    '    Dim FailureEventSeq As String = _ddlFailureEvent.SelectedValue
    '    Dim FailureEventOther As String = Me._tbFailureEvent.Text

    '    Try

    '        'If FailureEventSeq = "" And FailureEventOther = "" Then
    '        'MsgBox("You must select an event or enter one")
    '        'Else
    '        Dim clsEvent As New clsRIWorkspaceNew
    '        With clsEvent
    '            .RIFailureSeq = _hfFailureEvent.Value
    '            .RINumber = strRINumber
    '            .RefFailureEventSeq = _ddlFailureEvent.SelectedValue
    '            .RIFailureEventDesc = Me._tbFailureEvent.Text
    '            .Username = userProfile.Username

    '            .RIFailureSeq = .SaveRIFailureEvent
    '            Me._hfFailureEvent.Value = .RIFailureSeq
    '        End With

    '        If Me._tbFailureEvent.Text <> "" Then
    '            '_tblEvent.Disabled = True
    '            Me._ddlFailureEvent.Enabled = False
    '        End If

    '        _tblMode.Visible = True
    '        populateFailureMode()


    '        'End If

    '    Catch ex As Exception
    '        'ds = Nothing
    '        RI.SharedFunctions.HandleError("_btnAddEvent_Click", , ex)
    '    Finally
    '        'If Not ds Is Nothing Then ds = Nothing
    '    End Try
    'End Sub

    Protected Sub _btnAddModes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnAddModes.Click
        'We are saving the event and modes in the click event
        Dim FailureEventSeq As String = _ddlFailureEvent.SelectedValue
        Dim FailureEventOther As String = Me._tbFailureEvent.Text

        Dim FailureModeSeqs As String = RI.SharedFunctions.GetCheckBoxValues(_cblFailureMode)
        Dim FailureModeOther As String = Me._tbFailureModeOther.Text
        Dim FailureModeOther2 As String = Me._tbFailureModeOther2.Text
        Dim FailureModeOther3 As String = Me._tbFailureModeOther3.Text
        Dim FailureModeOther4 As String = Me._tbFailureModeOther4.Text
        Dim FailureModeOther5 As String = Me._tbFailureModeOther5.Text

        Try
            Dim clsEvent As New clsRIWorkspaceNew
            With clsEvent
                .RIFailureSeq = _hfFailureEvent.Value
                .RINumber = strRINumber
                .RefFailureEventSeq = _ddlFailureEvent.SelectedValue
                .RIFailureEventDesc = Me._tbFailureEvent.Text
                .Username = userProfile.Username

                .RIFailureSeq = .SaveRIFailureEvent
                Me._hfFailureEvent.Value = .RIFailureSeq

            End With

            If FailureModeSeqs <> "" Or FailureModeOther <> "" Or FailureModeOther2 <> "" Or FailureModeOther3 <> "" Or FailureModeOther4 <> "" Then

                With clsEvent
                    .RefFailureModeSeq = FailureModeSeqs
                    .RIFailureModeDesc = FailureModeOther
                    .RIFailureModeDesc2 = FailureModeOther2
                    .RIFailureModeDesc3 = FailureModeOther3
                    .RIFailureModeDesc4 = FailureModeOther4
                    .RIFailureModeDesc5 = FailureModeOther5
                    .Username = userProfile.Username
                    .SaveRIFailureMode()
                End With

                populateRIFailureModes()
                Me._btnAddModes.Visible = "false"
                Me._btnFailureModeUpdate.Visible = "true"
            End If
        Catch ex As Exception
            RI.SharedFunctions.HandleError("_btnAddModes_Click", , ex)
        Finally
        End Try
    End Sub
    'Sub populateRIFailureEvent()

    '    Dim FailureEventSeq As String = Me._hfFailureEvent.Value

    '    Try
    '        Dim clsSearch As New clsRIWorkspaceNew(FailureEventSeq)

    '        Me._tbFailureEvent.Text = clsSearch.RIFailureEventDesc
    '        Me._ddlFailureEvent.SelectedValue = clsSearch.RefFailureEventSeq
    '        Me._tblEvent.Visible = True

    '    Catch ex As Exception
    '        Throw
    '    Finally
    '    End Try
    'End Sub
    Sub populateRIFailureEventUpdate()

        Dim FailureEventSeq As String = Me._hfFailureEvent.Value

        Try
            Dim clsSearch As New clsRIWorkspaceNew(FailureEventSeq)
            currentRIWorkspace.GetFailureEvents()

            RI.SharedFunctions.BindList(Me._ddlFailureEventUpdate, currentRIWorkspace.FailureEvents, True, True)
            If clsSearch.RIFailureEventDesc = "" Then
                _tbFailureEventUpdate.Visible = False
                _ddlFailureEventUpdate.Visible = True
            Else
                '_tbFailureEventUpdate.Text = FixCrLf(clsSearch.RIFailureEventDesc)
                _tbFailureEventUpdate.Text = clsSearch.RIFailureEventDesc
            End If

            If clsSearch.RefFailureEventSeq = "" Then
                _ddlFailureEventUpdate.Visible = False
                _tbFailureEventUpdate.Visible = True
            Else
                _ddlFailureEventUpdate.SelectedValue = clsSearch.RefFailureEventSeq
            End If

            _rpAW.Visible = "false"

        Catch ex As Exception
            Throw
        Finally
        End Try
    End Sub
    Sub populateRIFailureModes()

        Dim FailureEventSeq As String = Me._hfFailureEvent.Value

        Try
            Dim clsSearch As New clsRIWorkspaceNew
            clsSearch.RIFailureSeq = FailureEventSeq

            '_gvFailureCause.DataSource = clsSearch.GetRIFailureModeDetail
            '_gvFailureCause.DataBind()
            '_gvFailureCause.AutoGenerateColumns = False
            '_gvFailureCause.Visible = True

            '' Me._bgFailureMode.DataSource = clsSearch.GetRIFailureModeDetail
            ''Me._bgFailureMode.DataBind()

            ''Me._tbFailureModeOther.Text = clsSearch.RIFailureModeDesc
            'Me._tblProvenFailureCause.Visible = "true"

            '_ddlFailureEventUpdate.Enabled = "true"
            Me._divNewAWObjects.Visible = "false"
            Me._divUpdateAWObjects.Visible = "true"
            'Me._hfFailureEvent.Value = FailureEventSeq
            populateRIFailureEventUpdate()
            populateFailureModeUpdate()
            BuildCauseRepeater()
            BuildReasonRepeater()
            BuildRootCauseRepeater()
            _btnList.Visible = "True"
            btnSpellCheck.Visible = "True"

        Catch ex As Exception
            Throw
        Finally
        End Try
    End Sub


    'Protected Sub _gvFailureCause_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _gvFailureCause.RowDataBound
    '    'Dim hfRIFailureModeSeqID As HiddenField = CType(e.Row.Cells(0).FindControl("_hfRIFailureModeSeq"), HiddenField)
    '    Dim hfFailureModeSeqID As HiddenField = CType(e.Row.Cells(0).FindControl("_hfFailureModeSeq"), HiddenField)
    '    Dim cblFailureCause As CheckBoxList = CType(e.Row.Cells(0).FindControl("_cblFailureCause"), CheckBoxList)
    '    Dim lbFailureCause As Label = CType(e.Row.Cells(0).FindControl("_lbMode"), Label)
    '    Dim tbFailureCauseOther As TextBox = CType(e.Row.Cells(0).FindControl("_tbFailureCauseOther"), TextBox)
    '    Dim tbFailureCauseOther2 As TextBox = CType(e.Row.Cells(0).FindControl("_tbFailureCauseOther2"), TextBox)
    '    Dim tbFailureCauseOther3 As TextBox = CType(e.Row.Cells(0).FindControl("_tbFailureCauseOther3"), TextBox)
    '    Dim tbFailureCauseOther4 As TextBox = CType(e.Row.Cells(0).FindControl("_tbFailureCauseOther4"), TextBox)
    '    Dim tbFailureCauseOther5 As TextBox = CType(e.Row.Cells(0).FindControl("_tbFailureCauseOther5"), TextBox)


    '    If e.Row.RowIndex >= 0 Then
    '        'If e.Row.RowType = DataControlRowType.DataRow Then
    '        '    'Now, reference the Button control that the Delete ButtonColumn has been rendered to 
    '        '    Dim deleteButton As Button = TryCast(e.Row.FindControl("_btnDelete"), Button)
    '        '    If deleteButton IsNot Nothing Then
    '        '        'We can now add the onclick event handler                
    '        '        deleteButton.OnClientClick = "Javascript:ConfirmDelete('" & e.Row.DataItem("taskitemseqid").ToString & "'," & e.Row.RowIndex & ",'" & Me._grvTasksPage.ClientID & "');return false;"
    '        '    End If
    '        'End If

    '        Dim ModeSeq As String
    '        ModeSeq = hfFailureModeSeqID.Value

    '        If ModeSeq <> "" Then
    '            currentRIWorkspace.GetFailureCauses(ModeSeq)
    '            If currentRIWorkspace.FailureCauses.DataSource.HasRows = True Then

    '                RI.SharedFunctions.BindList(cblFailureCause, currentRIWorkspace.FailureCauses, True, False)
    '            End If

    '            Dim clsSearch As New clsRIWorkspaceNew
    '            clsSearch.RIFailureSeq = Me._hfFailureEvent.Value

    '            clsSearch.GetRIFailureCauses()
    '            RI.SharedFunctions.SetCheckBoxValues(cblFailureCause, clsSearch.RIFailureModesSelected)

    '        Else

    '            lbFailureCause.Visible = "true"
    '        End If

    '        tbFailureCauseOther.Attributes.Add("onchange", "ShowNextTextBox('" & tbFailureCauseOther.ClientID & "','" & tbFailureCauseOther2.ClientID & "');")
    '        tbFailureCauseOther2.Attributes.Add("onchange", "ShowNextTextBox('" & tbFailureCauseOther2.ClientID & "','" & tbFailureCauseOther3.ClientID & "');")
    '        tbFailureCauseOther3.Attributes.Add("onchange", "ShowNextTextBox('" & tbFailureCauseOther3.ClientID & "','" & tbFailureCauseOther4.ClientID & "');")
    '        tbFailureCauseOther4.Attributes.Add("onchange", "ShowNextTextBox('" & tbFailureCauseOther4.ClientID & "','" & tbFailureCauseOther5.ClientID & "');")

    '    End If

    'End Sub

    'Protected Sub _btnAddCauses_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnAddCauses.Click
    '    For Each r As GridViewRow In Me._gvFailureCause.Rows
    '        Dim hfRImodeSeqID As HiddenField = TryCast(r.FindControl(String.Format("_hfRIFailureModeSeq", r.RowIndex)), HiddenField)

    '        Dim cblfailureCauseSeqs As CheckBoxList = TryCast(r.FindControl(String.Format("_cblFailureCause", r.RowIndex)), CheckBoxList)
    '        Dim tbfailureCauseOther As TextBox = TryCast(r.FindControl(String.Format("_tbFailureCauseOther", r.RowIndex)), TextBox)
    '        Dim tbfailureCauseOther2 As TextBox = TryCast(r.FindControl(String.Format("_tbFailureCauseOther2", r.RowIndex)), TextBox)
    '        Dim tbfailureCauseOther3 As TextBox = TryCast(r.FindControl(String.Format("_tbFailureCauseOther3", r.RowIndex)), TextBox)
    '        Dim tbfailureCauseOther4 As TextBox = TryCast(r.FindControl(String.Format("_tbFailureCauseOther4", r.RowIndex)), TextBox)
    '        Dim tbfailureCauseOther5 As TextBox = TryCast(r.FindControl(String.Format("_tbFailureCauseOther5", r.RowIndex)), TextBox)

    '        Dim paramCollection As New OracleParameterCollection
    '        Dim param As New OracleParameter
    '        Dim ds As System.Data.DataSet = Nothing
    '        Dim FailureEventSeq As String = Me._hfFailureEvent.Value
    '        Dim FailureCauseSeqs As String = RI.SharedFunctions.GetCheckBoxValues(cblfailureCauseSeqs)
    '        Dim FailureCauseOther As String = tbfailureCauseOther.Text
    '        Dim FailureCauseOther2 As String = tbfailureCauseOther2.Text
    '        Dim FailureCauseOther3 As String = tbfailureCauseOther3.Text
    '        Dim FailureCauseOther4 As String = tbfailureCauseOther4.Text
    '        Dim FailureCauseOther5 As String = tbfailureCauseOther5.Text

    '        Dim clsEvent As New clsRIWorkspaceNew

    '        With clsEvent

    '            .RIFailureSeq = _hfFailureEvent.Value
    '            .RIFailureModeSeq = hfRImodeSeqID.Value
    '            .RIFailureCausesSelected = FailureCauseSeqs
    '            .RIFailureCauseDesc = FailureCauseOther
    '            .RIFailureCauseDesc2 = FailureCauseOther2
    '            .RIFailureCauseDesc3 = FailureCauseOther3
    '            .RIFailureCauseDesc4 = FailureCauseOther4
    '            .RIFailureCauseDesc5 = FailureCauseOther5
    '            .SaveRIFailureCause()

    '        End With

    '    Next

    '    populateRIFailureCauses()

    'End Sub

    'Sub populateRIFailureCauses()

    '    Dim FailureEventSeq As String = Me._hfFailureEvent.Value

    '    Try

    '        Dim clsSearch As New clsRIWorkspaceNew
    '        clsSearch.RIFailureSeq = FailureEventSeq

    '        _gvFailureReason.DataSource = clsSearch.GetRIFailureCauseDetail
    '        _gvFailureReason.DataBind()
    '        _gvFailureReason.AutoGenerateColumns = False
    '        _gvFailureReason.Visible = True

    '        Me._tblMostLikelyFailureCause.Visible = "true"

    '    Catch ex As Exception
    '        Throw
    '    Finally

    '    End Try
    'End Sub

    'Protected Sub _btnAddReasons_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnAddReasons.Click
    '    For Each r As GridViewRow In Me._gvFailureReason.Rows
    '        Dim hfRICauseSeqID As HiddenField = TryCast(r.FindControl(String.Format("_hfRIFailureCauseSeq", r.RowIndex)), HiddenField)

    '        Dim cblfailureCauseSeqs As CheckBoxList = TryCast(r.FindControl(String.Format("_cblFailureCauseReas", r.RowIndex)), CheckBoxList)
    '        Dim tbfailureReasonOther As TextBox = TryCast(r.FindControl(String.Format("_tbFailureReasonOther", r.RowIndex)), TextBox)
    '        Dim tbfailureReasonOther2 As TextBox = TryCast(r.FindControl(String.Format("_tbFailureReasonOther2", r.RowIndex)), TextBox)
    '        Dim tbfailureReasonOther3 As TextBox = TryCast(r.FindControl(String.Format("_tbFailureReasonOther3", r.RowIndex)), TextBox)
    '        Dim tbfailureReasonOther4 As TextBox = TryCast(r.FindControl(String.Format("_tbFailureReasonOther4", r.RowIndex)), TextBox)
    '        Dim tbfailureReasonOther5 As TextBox = TryCast(r.FindControl(String.Format("_tbFailureReasonOther5", r.RowIndex)), TextBox)

    '        Dim FailureCauseSeq As String = hfRICauseSeqID.Value
    '        Dim FailureReasonSeqs As String = RI.SharedFunctions.GetCheckBoxValues(cblfailureCauseSeqs)
    '        Dim FailureReasonOther As String = tbfailureReasonOther.Text
    '        Dim FailureReasonOther2 As String = tbfailureReasonOther2.Text
    '        Dim FailureReasonOther3 As String = tbfailureReasonOther3.Text
    '        Dim FailureReasonOther4 As String = tbfailureReasonOther4.Text
    '        Dim FailureReasonOther5 As String = tbfailureReasonOther5.Text

    '        Dim clsEvent As New clsRIWorkspaceNew

    '        With clsEvent

    '            .RIFailureSeq = _hfFailureEvent.Value
    '            .RIFailureCauseSeq = hfRICauseSeqID.Value
    '            .RIFailureReasonsSelected = FailureReasonSeqs
    '            .RIFailureReasonDesc = FailureReasonOther
    '            .RIFailureReasonDesc2 = FailureReasonOther2
    '            .RIFailureReasonDesc3 = FailureReasonOther3
    '            .RIFailureReasonDesc4 = FailureReasonOther4
    '            .RIFailureReasonDesc5 = FailureReasonOther5
    '            .SaveRIFailureReason()

    '        End With

    '    Next

    '    populateRIFailureCauseReasons()
    'End Sub
    'Sub populateRIFailureCauseReasons()

    '    Dim FailureEventSeq As String = Me._hfFailureEvent.Value

    '    Try

    '        Dim clsSearch As New clsRIWorkspaceNew
    '        clsSearch.RIFailureSeq = FailureEventSeq

    '        _gvFailureCauseReason.DataSource = clsSearch.GetRIFailureCauseReasDetail
    '        _gvFailureCauseReason.DataBind()
    '        _gvFailureCauseReason.AutoGenerateColumns = False
    '        _gvFailureCauseReason.Visible = True

    '        Me._tblRootCause.Visible = "true"

    '    Catch ex As Exception
    '        Throw
    '    Finally
    '    End Try
    'End Sub
    'Protected Sub _btnAddRootCause_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnAddRootCause.Click
    '    For Each r As GridViewRow In Me._gvFailureCauseReason.Rows
    '        Dim hfRICauseReasSeqID As HiddenField = TryCast(r.FindControl(String.Format("_hfRIFailureCauseReasSeq", r.RowIndex)), HiddenField)
    '        Dim cblfailureRootCauseSeqs As CheckBoxList = TryCast(r.FindControl(String.Format("_cblFailureRootCause", r.RowIndex)), CheckBoxList)
    '        Dim tbfailureRootCause As TextBox = TryCast(r.FindControl(String.Format("_tbRootCause", r.RowIndex)), TextBox)

    '        Dim FailureReasonSeq As String = hfRICauseReasSeqID.Value
    '        Dim FailureRootCauseSeqs As String = RI.SharedFunctions.GetCheckBoxValues(cblfailureRootCauseSeqs)
    '        Dim FailureRootCause As String = tbfailureRootCause.Text

    '        Dim clsEvent As New clsRIWorkspaceNew

    '        With clsEvent

    '            .RIFailureSeq = _hfFailureEvent.Value
    '            .RIFailureReasonSeq = FailureReasonSeq
    '            .RIFailureRootCausesSelected = FailureRootCauseSeqs
    '            .RIFailureRootCauseDesc = FailureRootCause
    '            .Username = userProfile.Username
    '            .SaveRIFailureRootCause()

    '        End With

    '    Next

    '    HideAWObjects()
    '    BuildRepeater()

    'End Sub
    Sub HideAWObjects()
        _divNewAWObjects.Visible = False
    End Sub

    Sub BuildRepeater()
        'This sub builds the repeater that shows the failure event listing
        Try
            Dim clsSearch As New clsRIWorkspaceNew
            clsSearch.RINumber = strRINumber

            _rpAW.DataSource = clsSearch.GetRIFailureEventsAll
            _rpAW.DataBind()
            _rpAW.Visible = True

        Catch ex As Exception
            RI.SharedFunctions.HandleError("BuildRepeater", , ex)
        Finally
        End Try
    End Sub

    Sub BuildCauseRepeater()

        Try
            Dim clsSearch As New clsRIWorkspaceNew
            clsSearch.RINumber = strRINumber
            clsSearch.RIFailureSeq = Me._hfFailureEvent.Value

            Me._rpFailureCauseUpdate.DataSource = clsSearch.GetRIFailureModeHeading
            _rpFailureCauseUpdate.DataBind()
            _rpFailureCauseUpdate.Visible = True

            'If _rpFailureCauseUpdate.Items.Count = 0 Then
            '_tbCauseUpdate.Visible = "false"
            'Else
            _tbCauseUpdate.Visible = "true"
            'End If

            _rpAW.Visible = "false"

        Catch ex As Exception
            RI.SharedFunctions.HandleError("BuildCauseRepeater", , ex)
        Finally
        End Try
    End Sub

    Sub BuildReasonRepeater()

        Try
            Dim clsSearch As New clsRIWorkspaceNew
            clsSearch.RINumber = strRINumber
            clsSearch.RIFailureSeq = Me._hfFailureEvent.Value

            _rpFailureReasonUpdate.DataSource = clsSearch.GetRIFailureCauseDesc '.GetRIFailureCauseDetail
            _rpFailureReasonUpdate.DataBind()
            _rpFailureReasonUpdate.Visible = True

            'If _rpFailureReasonUpdate.Items.Count = 0 Then
            '_tbReasonUpdate.Visible = "false"
            'Else
            _tbReasonUpdate.Visible = "true"
            'End If

            _rpAW.Visible = "false"

        Catch ex As Exception
            RI.SharedFunctions.HandleError("BuildReasonRepeater", , ex)
        Finally
        End Try
    End Sub

    Sub BuildRootCauseRepeater()

        Try
            Dim clsSearch As New clsRIWorkspaceNew
            clsSearch.RINumber = strRINumber
            clsSearch.RIFailureSeq = Me._hfFailureEvent.Value

            _rpFailureRootCauseUpdate.DataSource = clsSearch.GetRIFailureCauseReasDesc '.GetRIFailureCauseReasHeading '.GetRIFailureCauseReasDetail
            _rpFailureRootCauseUpdate.DataBind()
            _rpFailureRootCauseUpdate.Visible = True

            'If _rpFailureRootCauseUpdate.Items.Count = 0 Then
            '_tblRootCauseUpdate.Visible = "false"
            'Else
            _tblRootCauseUpdate.Visible = "true"
            'End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("BuildRootCauseRepeater", , ex)
        Finally
        End Try
    End Sub
    Protected Sub _btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnAdd.Click
        _btnAddModes.Visible = "true"
        ClearObjects()
        Response.Redirect(Page.AppRelativeVirtualPath & "?RINumber=" & strRINumber & "&awcount=0")
    End Sub
    Protected Sub ClearObjects()
        Me._tbFailureModeOther.Text = Nothing
        Me._tbFailureModeOther2.Text = Nothing
        Me._tbFailureModeOther3.Text = Nothing
        Me._tbFailureModeOther4.Text = Nothing
        Me._ddlFailureEventUpdate.SelectedValue = ""
    End Sub

    Protected Sub _rpAW_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles _rpAW.ItemCommand
        Dim currentRIFailureEvent As clsRIWorkspace
        Dim strRIFailureEventSeq As String
        strRIFailureEventSeq = e.CommandArgument
        Dim hfRIFailureEventSeqID As HiddenField = CType(e.Item.FindControl("_hfRIFailureEventSeq2"), HiddenField)

        If e.CommandName = "Delete" And e.CommandArgument IsNot Nothing Then
            currentRIFailureEvent = New clsRIWorkspace
            clsRIWorkspace.DeleteRIFailureEvent(strRINumber, strRIFailureEventSeq, userProfile.Username)
            BuildRepeater()
        ElseIf e.CommandName = "Edit" And e.CommandArgument IsNot Nothing Then
            'Show/Hide appropriate objects
            _divNewAWObjects.Visible = "false"
            _divUpdateAWObjects.Visible = "true"
            _tbEventUpdate.Visible = "true"
            _tbModeUpdate.Visible = "true"

            _hfFailureEvent.Value = hfRIFailureEventSeqID.Value

            populateRIFailureEventUpdate()
            populateFailureModeUpdate()
            BuildCauseRepeater()
            BuildReasonRepeater()
            BuildRootCauseRepeater()

            'Show/Hide appropriate buttons
            _btnList.Visible = "True"
            btnSpellCheck.Visible = "True"
            _btnFailureModeUpdate.Visible = "True"
            _btnAddModes.Visible = "false"
        End If
    End Sub

    Protected Sub _rpAW_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles _rpAW.ItemDataBound
        Dim hfRIFailureEventSeqID As HiddenField = CType(e.Item.FindControl("_hfRIFailureEventSeq2"), HiddenField)
        Dim gvFailureMode As GridView = CType(e.Item.FindControl("_gvAWModes"), GridView)
        If hfRIFailureEventSeqID IsNot Nothing Then
            Dim paramCollection As New OracleParameterCollection
            Dim param As New OracleParameter
            Dim ds As System.Data.DataSet = Nothing
            Dim key As String = String.Empty
            Dim FailureEventSeq As String = hfRIFailureEventSeqID.Value


            Dim clsSearch As New clsRIWorkspaceNew
            clsSearch.RIFailureSeq = FailureEventSeq

            gvFailureMode.DataSource = clsSearch.GetRIFailureModeDetail
            gvFailureMode.DataBind()
            gvFailureMode.AutoGenerateColumns = False
            gvFailureMode.Visible = True

        End If
    End Sub
    'Protected Sub _gvFailureReason_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _gvFailureReason.RowDataBound
    '    Dim lbFailureCause As Label = CType(e.Row.Cells(0).FindControl("_lbMode"), Label)
    '    Dim hfFailureCauseSeq As HiddenField = CType(e.Row.Cells(0).FindControl("_hfFailureCauseSeq"), HiddenField)
    '    Dim cblFailureCause As CheckBoxList = CType(e.Row.Cells(0).FindControl("_cblFailureCauseReas"), CheckBoxList)
    '    Dim tbFailureReasonOther As TextBox = CType(e.Row.Cells(0).FindControl("_tbFailureReasonOther"), TextBox)
    '    Dim tbFailureReasonOther2 As TextBox = CType(e.Row.Cells(0).FindControl("_tbFailureReasonOther2"), TextBox)
    '    Dim tbFailureReasonOther3 As TextBox = CType(e.Row.Cells(0).FindControl("_tbFailureReasonOther3"), TextBox)
    '    Dim tbFailureReasonOther4 As TextBox = CType(e.Row.Cells(0).FindControl("_tbFailureReasonOther4"), TextBox)
    '    Dim tbFailureReasonOther5 As TextBox = CType(e.Row.Cells(0).FindControl("_tbFailureReasonOther5"), TextBox)

    '    If e.Row.RowIndex >= 0 Then

    '        Dim CauseSeq As String
    '        CauseSeq = hfFailureCauseSeq.Value

    '        If CauseSeq <> "" Then
    '            currentRIWorkspace.GetFailureCauseReasons(CauseSeq)
    '            If currentRIWorkspace.FailureCauseReasons.DataSource.HasRows = True Then

    '                RI.SharedFunctions.BindList(cblFailureCause, currentRIWorkspace.FailureCauseReasons, True, False)
    '            End If
    '        End If

    '        tbFailureReasonOther.Attributes.Add("onblur", "ShowNextTextBox('" & tbFailureReasonOther.ClientID & "','" & tbFailureReasonOther2.ClientID & "');")
    '        tbFailureReasonOther2.Attributes.Add("onblur", "ShowNextTextBox('" & tbFailureReasonOther2.ClientID & "','" & tbFailureReasonOther3.ClientID & "');")
    '        tbFailureReasonOther3.Attributes.Add("onblur", "ShowNextTextBox('" & tbFailureReasonOther3.ClientID & "','" & tbFailureReasonOther4.ClientID & "');")
    '        tbFailureReasonOther4.Attributes.Add("onblur", "ShowNextTextBox('" & tbFailureReasonOther4.ClientID & "','" & tbFailureReasonOther5.ClientID & "');")

    '    End If

    'End Sub

    'Protected Sub _gvFailureCauseReason_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _gvFailureCauseReason.RowDataBound
    '    Dim hfFailureCauseReasSeq As HiddenField = CType(e.Row.Cells(0).FindControl("_hfFailureCauseReasSeq"), HiddenField)
    '    Dim cblFailureRootCause As CheckBoxList = CType(e.Row.Cells(0).FindControl("_cblFailureRootCause"), CheckBoxList)

    '    If e.Row.RowIndex >= 0 Then

    '        Dim CauseReasSeq As String
    '        CauseReasSeq = hfFailureCauseReasSeq.Value

    '        If CauseReasSeq <> "" Then
    '            currentRIWorkspace.GetFailureRootCauses(CauseReasSeq)
    '            If currentRIWorkspace.FailureRootCauses.DataSource.HasRows = True Then

    '                RI.SharedFunctions.BindList(cblFailureRootCause, currentRIWorkspace.FailureRootCauses, True, False)
    '            End If
    '        End If

    '    End If

    'End Sub

    Protected Function FixCrLf(ByVal value As Object) As String
        If IsDBNull(value) Then
            Return "" 'String.Empty
        Else
            Dim RIResources As New IP.Bids.Localization.WebLocalization
            Dim ipLoc As New IP.Bids.Localization.DataLocalization(RIResources)
            value = value.replace(Environment.NewLine, "<br />")
            value = SharedFunctions.LocalizeValue(value)
            value = value.replace(Chr(13), "<br />")
            Return value '.Replace(Environment.NewLine, "<br />")
        End If
    End Function

    Protected Sub _tbFailureEvent_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _tbFailureEvent.TextChanged
        If _tbFailureEvent.Text <> "" Then
            populateFailureMode()
        Else
            _tblMode.Visible = False
        End If
    End Sub

    'Protected Sub _tbFailureEventUpdate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _tbFailureEventUpdate.TextChanged
    '    If _tbFailureEventUpdate.Text <> "" Then
    '        populateFailureModeUpdate()
    '    Else
    '        _tblMode.Visible = False
    '    End If
    'End Sub

    Protected Sub _tbFailureEvent_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles _tbFailureEvent.Unload
        If _tbFailureEvent.Text = "" Then
            _lblFailureModeDesc.Text = "Enter ALL the Ways This Type of Failure Event Can Occur or Has Ocurred in the Past"
            _tblMode.Visible = False
        Else
            _tblMode.Visible = True
        End If
    End Sub

    Protected Sub _rpFailureCauseUpdate_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles _rpFailureCauseUpdate.ItemDataBound
        Dim hfRIFailureModeSeqID As HiddenField = CType(e.Item.FindControl("_hfRIFailureModeSeq"), HiddenField)
        Dim hfFailureModeSeqID As HiddenField = CType(e.Item.FindControl("_hfFailureModeSeq"), HiddenField)
        Dim gvFailureCause As GridView = CType(e.Item.FindControl("_gvfailureCauseUpdate"), GridView)
        Dim cblFailureCause As CheckBoxList = CType(e.Item.FindControl("_cblFailureCauseUpdateNew"), CheckBoxList)

        If hfRIFailureModeSeqID IsNot Nothing Then
            Dim paramCollection As New OracleParameterCollection
            Dim param As New OracleParameter
            Dim ds As System.Data.DataSet = Nothing
            Dim key As String = String.Empty
            Dim RIFailureModeSeq As String = hfRIFailureModeSeqID.Value
            Dim FailureModeSeq As String = hfFailureModeSeqID.Value

            Dim clsSearch As New clsRIWorkspaceNew
            clsSearch.RIFailureModeSeq = RIFailureModeSeq

            gvFailureCause.DataSource = clsSearch.GetRIFailureCauseHeading
            gvFailureCause.DataBind()
            gvFailureCause.AutoGenerateColumns = False
            gvFailureCause.Visible = True

            If gvFailureCause.Rows.Count = 0 Then
                currentRIWorkspace.GetFailureCauses(FailureModeSeq)
                If currentRIWorkspace.FailureCauses.DataSource.HasRows = True Then
                    RI.SharedFunctions.BindList(cblFailureCause, currentRIWorkspace.FailureCauses, True, False)
                End If
            End If
        End If

    End Sub

    Protected Sub _btnFailureModeUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnFailureModeUpdate.Click
        Me._tbEventUpdate.Visible = "true"
        '_tbFailureModeUpdate2.Visible = "false"
        Me._tbModeUpdate.Visible = "true"
        _btnAddModes.Visible = "false"
        SaveRootCauses(sender, e)
        SaveReasons(sender, e)
        SaveCauses(sender, e)
        SaveModes(sender, e)
        _tbFailureModeUpdate2.Text = ""
    End Sub

    Sub _gvFailureCauseUpdateBind(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Dim hfFailureModeSeqID As HiddenField = CType(e.Row.Cells(0).FindControl("_hfFailureModeSeq"), HiddenField)
        Dim cblFailureCause As CheckBoxList = CType(e.Row.Cells(0).FindControl("_cblFailureCauseUpdate"), CheckBoxList)
        Dim tbFailureCause As TextBox = CType(e.Row.Cells(0).FindControl("_tbFailureCauseOtherUpdate"), TextBox)

        If e.Row.RowIndex = 0 Then
            Dim ModeSeq As String
            ModeSeq = hfFailureModeSeqID.Value

            If ModeSeq <> "" Then
                currentRIWorkspace.GetFailureCauses(ModeSeq)
                If currentRIWorkspace.FailureCauses.DataSource.HasRows = True Then
                    RI.SharedFunctions.BindList(cblFailureCause, currentRIWorkspace.FailureCauses, True, False)
                End If

                Dim clsSearch As New clsRIWorkspaceNew
                clsSearch.RIFailureSeq = Me._hfFailureEvent.Value

                clsSearch.GetRIFailureCauses()
                RI.SharedFunctions.SetCheckBoxValues(cblFailureCause, clsSearch.RIFailureCausesSelected)

                If tbFailureCause.Text = "" Then
                    tbFailureCause.Visible = False
                End If
            End If

        ElseIf e.Row.RowIndex >= 0 Then
            If tbFailureCause.Text = "" Then
                tbFailureCause.Visible = False
            End If
        End If

    End Sub

    Protected Sub _rpFailureReasonUpdate_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles _rpFailureReasonUpdate.ItemDataBound
        Dim hfRIFailureCauseSeqID As HiddenField = CType(e.Item.FindControl("_hfRIFailureCauseSeq"), HiddenField)
        Dim hfFailureCauseSeqID As HiddenField = CType(e.Item.FindControl("_hfFailureCauseSeq"), HiddenField)
        Dim gvFailureReason As GridView = CType(e.Item.FindControl("_gvfailureReasonUpdate"), GridView)
        Dim cblFailureReasons As CheckBoxList = CType(e.Item.FindControl("_cblFailureReasonUpdateNew"), CheckBoxList)

        If hfRIFailureCauseSeqID IsNot Nothing Then
            Dim paramCollection As New OracleParameterCollection
            Dim param As New OracleParameter
            Dim ds As System.Data.DataSet = Nothing
            Dim RIFailureCauseSeq As String = hfRIFailureCauseSeqID.Value
            Dim FailureCauseSeq As String = hfFailureCauseSeqID.Value

            Dim clsSearch As New clsRIWorkspaceNew
            clsSearch.RIFailureCauseSeq = RIFailureCauseSeq

            gvFailureReason.DataSource = clsSearch.GetRIFailureReasonHeading
            gvFailureReason.DataBind()
            gvFailureReason.AutoGenerateColumns = False
            gvFailureReason.Visible = True

            If gvFailureReason.Rows.Count = 0 Then
                currentRIWorkspace.GetFailureCauseReasons(FailureCauseSeq)
                If currentRIWorkspace.FailureCauseReasons.DataSource.HasRows = True Then
                    RI.SharedFunctions.BindList(cblFailureReasons, currentRIWorkspace.FailureCauseReasons, True, False)
                End If
            End If
        End If
    End Sub

    Sub _gvFailureReasonUpdateBind(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Dim hfFailureCauseSeqID As HiddenField = CType(e.Row.Cells(0).FindControl("_hfFailureCauseSeq"), HiddenField)
        Dim cblFailureReason As CheckBoxList = CType(e.Row.Cells(0).FindControl("_cblFailureReasonUpdate"), CheckBoxList)
        Dim tbFailureReason As TextBox = CType(e.Row.Cells(0).FindControl("_tbFailureReasonOtherUpdate"), TextBox)

        If e.Row.RowIndex = 0 Then
            Dim CauseSeq As String
            CauseSeq = hfFailureCauseSeqID.Value

            If CauseSeq <> "" Then
                currentRIWorkspace.GetFailureCauseReasons(CauseSeq)
                If currentRIWorkspace.FailureCauseReasons.DataSource.HasRows = True Then
                    RI.SharedFunctions.BindList(cblFailureReason, currentRIWorkspace.FailureCauseReasons, True, False)
                End If

                Dim clsSearch As New clsRIWorkspaceNew
                clsSearch.RIFailureSeq = Me._hfFailureEvent.Value

                clsSearch.GetRIFailureCauseReasDetail()
                RI.SharedFunctions.SetCheckBoxValues(cblFailureReason, clsSearch.RIFailureReasonsSelected)

                If tbFailureReason.Text = "" Then
                    tbFailureReason.Visible = False
                End If
            Else
                cblFailureReason.Visible = False
            End If

        End If

        If e.Row.RowIndex >= 0 Then
            If tbFailureReason.Text = "" Then
                tbFailureReason.Visible = False
            End If

        End If
    End Sub

    Protected Sub _btnFailureCauseUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnFailureCauseUpdate.Click
        SaveCauses(sender, e)
    End Sub
    Sub SaveCauses(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If causesChanged = True Then

                'Each Mode can have 1:M causes.  The cause can be predefined (reffailurecause) or manually entered text.
                'Repeater is used so we can group the causes under the parent mode
                For Each i As RepeaterItem In _rpFailureCauseUpdate.Items
                    'Grid used because each mode can have multiple children
                    'Grid will show existing causes
                    Dim gvFailureCause As GridView = TryCast(i.FindControl(String.Format("_gvfailureCauseUpdate")), GridView)
                    Dim hfRIFailureModeSeq As HiddenField = TryCast(i.FindControl(String.Format("_hfRIFailureModeSeq")), HiddenField)
                    Dim FailureModeSeq As String = hfRIFailureModeSeq.Value
                    'These items are for entering new causes
                    Dim FailureCauseOthertb As TextBox = TryCast(i.FindControl(String.Format("_tbFailureCauseOtherUpdate2")), TextBox)
                    Dim cblfailureCauseOtherSeqs As CheckBoxList = TryCast(i.FindControl(String.Format("_cblFailureCauseUpdateNew")), CheckBoxList)

                    For Each r As GridViewRow In gvFailureCause.Rows
                        Dim hfRIFailureCauseSeq As HiddenField = TryCast(r.FindControl(String.Format("_hfRIFailureCauseSeq", r.RowIndex)), HiddenField)
                        Dim cblfailureCauseSeqs As CheckBoxList = TryCast(r.FindControl(String.Format("_cblFailureCauseUpdate", r.RowIndex)), CheckBoxList)
                        Dim tbfailureCauseOther As TextBox = TryCast(r.FindControl(String.Format("_tbFailureCauseOtherUpdate", r.RowIndex)), TextBox)

                        Dim FailureEventSeq As String = Me._hfFailureEvent.Value
                        Dim FailureCauseSeqs As String = RI.SharedFunctions.GetCheckBoxValues(cblfailureCauseSeqs)
                        Dim FailureCauseOther As String = tbfailureCauseOther.Text

                        Dim clsEvent As New clsRIWorkspaceNew


                        With clsEvent

                            .RIFailureSeq = _hfFailureEvent.Value
                            .RIFailureModeSeq = FailureModeSeq
                            .RIFailureCauseSeq = hfRIFailureCauseSeq.Value
                            .RIFailureCausesSelected = FailureCauseSeqs
                            .RIFailureCauseDesc = FailureCauseOther
                            .Username = userProfile.Username

                            '.RIFailureCauseDesc2 = FailureCauseOther2
                            'if .RIFailureCauseDesc <> Nothing Or .RIFailureCausesSelected <> Nothing Then
                            .SaveRIFailureCause()
                            'End If
                            If .RIFailureCauseDesc = "" And tbfailureCauseOther.Visible = "true" Then 'FailureCauseSeqs = "" Then
                                .DeleteRIFailureCause()
                            End If
                        End With

                    Next

                    Dim FailureCauseSeqsNew As String
                    If Not cblfailureCauseOtherSeqs Is Nothing Then
                        FailureCauseSeqsNew = RI.SharedFunctions.GetCheckBoxValues(cblfailureCauseOtherSeqs)
                    Else
                        FailureCauseSeqsNew = Nothing
                    End If

                    Dim FailureCauseOtherNew As String = FailureCauseOthertb.Text
                    If FailureCauseOtherNew <> "" Or FailureCauseSeqsNew <> "" Then
                        Dim clsEvent As New clsRIWorkspaceNew

                        With clsEvent

                            .RIFailureSeq = _hfFailureEvent.Value
                            .RIFailureModeSeq = FailureModeSeq
                            .RIFailureCausesSelected = FailureCauseSeqsNew
                            .RIFailureCauseDesc = FailureCauseOtherNew
                            .SaveRIFailureCause()

                        End With
                    End If
                Next
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("SaveCauses", , ex)
        Finally
        End Try
    End Sub
    Sub _gvFailureRootCauseUpdateBind(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Dim hfFailureCauseReasSeqID As HiddenField = CType(e.Row.Cells(0).FindControl("_hfFailureReasonSeq"), HiddenField)
        Dim cblFailureRootCause As CheckBoxList = CType(e.Row.Cells(0).FindControl("_cblFailureRootCauseUpdate"), CheckBoxList)
        Dim tbFailureRootCause As TextBox = CType(e.Row.Cells(0).FindControl("_tbFailureRootCauseOtherUpdate"), TextBox)

        If e.Row.RowIndex = 0 Then
            Dim ReasonSeq As String
            ReasonSeq = hfFailureCauseReasSeqID.Value

            If ReasonSeq <> "" Then
                currentRIWorkspace.GetFailureRootCauses(ReasonSeq)
                If currentRIWorkspace.FailureRootCauses.DataSource.HasRows = True Then
                    RI.SharedFunctions.BindList(cblFailureRootCause, currentRIWorkspace.FailureRootCauses, True, False)
                End If
                Dim clsSearch As New clsRIWorkspaceNew
                clsSearch.RIFailureSeq = Me._hfFailureEvent.Value

                clsSearch.GetRIFailureRootCauses()
                RI.SharedFunctions.SetCheckBoxValues(cblFailureRootCause, clsSearch.RIFailureRootCausesSelected)
            Else
                cblFailureRootCause.Visible = False
            End If

            If tbFailureRootCause.Text = "" Then
                tbFailureRootCause.Visible = False
            End If

        
        End If

        If e.Row.RowIndex >= 0 Then
            If tbFailureRootCause.Text = "" Then
                tbFailureRootCause.Visible = False
            End If

        End If

    End Sub

    Protected Sub _rpFailureRootCauseUpdate_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles _rpFailureRootCauseUpdate.ItemDataBound
        Dim hfRIFailureCauseSeqID As HiddenField = CType(e.Item.FindControl("_hfRIFailureCauseReasSeq"), HiddenField)
        Dim hfFailureReasSeqID As HiddenField = CType(e.Item.FindControl("_hfFailureCauseReasSeq"), HiddenField)
        Dim gvFailureRootCause As GridView = CType(e.Item.FindControl("_gvfailureRootCauseUpdate"), GridView)
        Dim cblFailureRootCause As CheckBoxList = CType(e.Item.FindControl("_cblFailureRootCauseUpdateNew"), CheckBoxList)

        If hfRIFailureCauseSeqID IsNot Nothing Then
            Dim paramCollection As New OracleParameterCollection
            Dim param As New OracleParameter
            Dim ds As System.Data.DataSet = Nothing
            Dim key As String = String.Empty
            Dim RIFailureCauseSeq As String = hfRIFailureCauseSeqID.Value
            Dim FailureCauseSeq As String = hfFailureReasSeqID.Value

            Dim clsSearch As New clsRIWorkspaceNew
            clsSearch.RIFailureReasonSeq = RIFailureCauseSeq

            gvFailureRootCause.DataSource = clsSearch.GetRIFailureRootCauseUnique
            gvFailureRootCause.DataBind()
            gvFailureRootCause.AutoGenerateColumns = False
            gvFailureRootCause.Visible = True

            If gvFailureRootCause.Rows.Count = 0 Then
                currentRIWorkspace.GetFailureRootCauses(FailureCauseSeq)
                If currentRIWorkspace.FailureRootCauses.DataSource.HasRows = True Then

                    RI.SharedFunctions.BindList(cblFailureRootCause, currentRIWorkspace.FailureRootCauses, True, False)
                End If
            End If

        End If
    End Sub

    Sub SaveRootCauses(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            For Each i As RepeaterItem In Me._rpFailureRootCauseUpdate.Items
                Dim gvFailureRootCause As GridView = TryCast(i.FindControl(String.Format("_gvfailureRootCauseUpdate")), GridView)
                Dim hfRIFailureReasonSeq As HiddenField = TryCast(i.FindControl(String.Format("_hfRIFailureCauseReasSeq")), HiddenField)
                Dim FailureReasonSeq As String = hfRIFailureReasonSeq.Value
                Dim FailureRootCauseOthertb As TextBox = TryCast(i.FindControl(String.Format("_tbFailureRootCauseOtherUpdate2")), TextBox)
                Dim cblfailureRootCauseOtherSeqs As CheckBoxList = TryCast(i.FindControl(String.Format("_cblFailureRootCauseUpdateNew")), CheckBoxList)

                For Each r As GridViewRow In gvFailureRootCause.Rows
                    Dim hfRIFailureRootCauseSeq As HiddenField = TryCast(r.FindControl(String.Format("_hfRIFailureRootCauseSeq", r.RowIndex)), HiddenField)
                    Dim cblfailureRootCauseSeqs As CheckBoxList = TryCast(r.FindControl(String.Format("_cblFailureRootCauseUpdate", r.RowIndex)), CheckBoxList)
                    Dim tbfailureRootCauseOther As TextBox = TryCast(r.FindControl(String.Format("_tbFailureRootCauseOtherUpdate", r.RowIndex)), TextBox)

                    Dim FailureEventSeq As String = Me._hfFailureEvent.Value
                    Dim FailureRootCauseSeqs As String = RI.SharedFunctions.GetCheckBoxValues(cblfailureRootCauseSeqs)
                    Dim FailureRootCauseOther As String = tbfailureRootCauseOther.Text

                    Dim clsEvent As New clsRIWorkspaceNew

                    With clsEvent

                        .RIFailureSeq = _hfFailureEvent.Value
                        .RIFailureReasonSeq = FailureReasonSeq
                        .RIFailureRootCauseSeq = hfRIFailureRootCauseSeq.Value
                        .RIFailureRootCausesSelected = FailureRootCauseSeqs
                        .RIFailureRootCauseDesc = FailureRootCauseOther
                        .SaveRIFailureRootCause()

                    End With
                Next

                'Check for entry of new root cause record
                Dim FailureRootCauseSeqsNew As String
                If Not cblfailureRootCauseOtherSeqs Is Nothing Then
                    FailureRootCauseSeqsNew = RI.SharedFunctions.GetCheckBoxValues(cblfailureRootCauseOtherSeqs)
                Else
                    FailureRootCauseSeqsNew = Nothing
                End If

                Dim FailureRootCauseNew As String = FailureRootCauseOthertb.Text
                If FailureRootCauseNew <> "" Or FailureRootCauseSeqsNew <> "" Then

                    Dim clsEvent As New clsRIWorkspaceNew

                    With clsEvent

                        .RIFailureSeq = _hfFailureEvent.Value
                        .RIFailureReasonSeq = FailureReasonSeq
                        .RIFailureRootCauseDesc = FailureRootCauseNew
                        .RIFailureRootCausesSelected = FailureRootCauseSeqsNew
                        .SaveRIFailureRootCause()

                    End With
                End If
            Next

            'SaveModes(sender, e)

            'BuildRepeater()
            'Me._divUpdateAWObjects.Visible = False
        Catch ex As Exception
            RI.SharedFunctions.HandleError("SaveRootCauses", , ex)
        Finally
        End Try
    End Sub
    'Protected Sub _btnFailureRootCauseUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnFailureRootCauseUpdate.Click
    '    saveRootCauses(sender, e)

    'End Sub

    Protected Sub _btnList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnList.Click
        Me._divUpdateAWObjects.Visible = "false"
        Me._divNewAWObjects.Visible = "false"
        ClearObjects()
        Me._btnFailureModeUpdate.Visible = "false"
        Me._btnAddModes.Visible = "false"
        'Me._gvAW.Visible = "True"

        BuildRepeater()
        _btnList.Visible = "false"
        _tblEvent.Visible = "false"
        btnSpellCheck.Visible = "False"


    End Sub

    Protected Sub _btnFailureReasonUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnFailureReasonUpdate.Click
        SaveReasons(sender, e)

    End Sub
    Sub SaveReasons(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

       
            If reasonsChanged = True Then

                For Each i As RepeaterItem In _rpFailureReasonUpdate.Items
                    Dim gvFailureReason As GridView = TryCast(i.FindControl(String.Format("_gvfailureReasonUpdate")), GridView)
                    Dim hfRIFailureCauseSeq As HiddenField = TryCast(i.FindControl(String.Format("_hfRIFailureCauseSeq")), HiddenField)
                    Dim FailureCauseSeq As String = hfRIFailureCauseSeq.Value
                    Dim FailureReasonOthertb As TextBox = TryCast(i.FindControl(String.Format("_tbFailureReasonOtherUpdate2")), TextBox)
                    Dim cblfailureReasonOtherSeqs As CheckBoxList = TryCast(i.FindControl(String.Format("_cblFailureReasonUpdateNew")), CheckBoxList)

                    For Each r As GridViewRow In gvFailureReason.Rows
                        Dim hfRIFailureReasonSeq As HiddenField = TryCast(r.FindControl(String.Format("_hfRIFailureReasonSeq", r.RowIndex)), HiddenField)
                        Dim cblfailureReasonSeqs As CheckBoxList = TryCast(r.FindControl(String.Format("_cblFailureReasonUpdate", r.RowIndex)), CheckBoxList)
                        Dim tbfailureReasonOther As TextBox = TryCast(r.FindControl(String.Format("_tbFailureReasonOtherUpdate", r.RowIndex)), TextBox)

                        Dim FailureEventSeq As String = Me._hfFailureEvent.Value
                        Dim FailureReasonSeqs As String = RI.SharedFunctions.GetCheckBoxValues(cblfailureReasonSeqs)
                        Dim FailureReasonOther As String = tbfailureReasonOther.Text

                        If tbfailureReasonOther.Visible = "true" Or cblfailureReasonSeqs.Visible = "true" Then

                            Dim clsEvent As New clsRIWorkspaceNew

                            With clsEvent

                                .RIFailureSeq = _hfFailureEvent.Value
                                .RIFailureCauseSeq = FailureCauseSeq
                                .RIFailureReasonSeq = hfRIFailureReasonSeq.Value
                                .RIFailureReasonsSelected = FailureReasonSeqs
                                .RIFailureReasonDesc = FailureReasonOther
                                .Username = userProfile.Username

                                '.RIFailureCauseDesc2 = FailureCauseOther2
                                'If .RIFailureReasonDesc <> Nothing Or .RIFailureReasonsSelected <> Nothing Then
                                .SaveRIFailureReason()
                                'End If
                            End With
                        End If
                    Next

                    'Check for entry of new reason record
                    Dim FailureReasonSeqsNew As String
                    If Not cblfailureReasonOtherSeqs Is Nothing Then
                        FailureReasonSeqsNew = RI.SharedFunctions.GetCheckBoxValues(cblfailureReasonOtherSeqs)
                    Else
                        FailureReasonSeqsNew = Nothing
                    End If

                    Dim FailureReasonNew As String = FailureReasonOthertb.Text
                    If FailureReasonNew <> "" Or FailureReasonSeqsNew <> "" Then
                        Dim clsEvent As New clsRIWorkspaceNew

                        With clsEvent

                            .RIFailureSeq = _hfFailureEvent.Value
                            .RIFailureCauseSeq = FailureCauseSeq
                            .RIFailureReasonDesc = FailureReasonNew
                            .RIFailureReasonsSelected = FailureReasonSeqsNew
                            .SaveRIFailureReason()

                        End With
                    End If
                Next
            End If
        Catch ex As Exception
            RI.SharedFunctions.HandleError("SaveReasons", , ex)
        Finally
        End Try
    End Sub
    Sub SaveModes(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim RIFailureEventSEQID As String = _hfFailureEvent.Value
        Dim FailureEventSeq As String = _ddlFailureEvent.SelectedValue
        Dim FailureEventOther As String = _tbFailureEventUpdate.Text
        Dim FailureModeOthertb As String = _tbFailureModeUpdate2.Text

        Try

            'Update event only if it was and entered event and not predefined event
            If _tbFailureEventUpdate.Visible = "true" Then
                Dim clsEvent As New clsRIWorkspaceNew
                With clsEvent
                    .RIFailureSeq = RIFailureEventSEQID
                    .RINumber = strRINumber
                    '.RefFailureEventSeq = FailureEventSeq
                    .RIFailureEventDesc = FailureEventOther
                    .Username = userProfile.Username

                    .RIFailureSeq = .SaveRIFailureEvent
                    Me._hfFailureEvent.Value = .RIFailureSeq
                End With
            End If

            If modesChanged = True Then

                Dim FailureModeSeqs As String = RI.SharedFunctions.GetCheckBoxValues(_cblFailureModeUpdate)

                'If FailureModeSeqs <> "" Then
                If Me._cblFailureModeUpdate.Visible = "true" Then
                    Dim clsModeEvent As New clsRIWorkspaceNew
                    With clsModeEvent
                        .RIFailureModeSeq = "0"
                        .RIFailureSeq = RIFailureEventSEQID
                        .RefFailureModeSeq = FailureModeSeqs
                        .Username = userProfile.Username
                        .SaveRIFailureMode()
                    End With

                    'If mode checkboxes were visible and none selected or some deselected, need to refresh page.
                    'If FailureModeSeqs = "" Then
                    'populateFailureModeUpdate()

                    'BuildCauseRepeater()
                    'BuildReasonRepeater()
                    'BuildRootCauseRepeater()
                    'Exit Sub
                End If
                'End If

                'End If

                For Each r As GridViewRow In Me._gvFailureModeUpdate.Rows
                    Dim hfRImodeSeqID As HiddenField = TryCast(r.FindControl(String.Format("_hfRIFailureModeSeq", r.RowIndex)), HiddenField)

                    Dim tbfailureModeOther As TextBox = TryCast(r.FindControl(String.Format("_tbFailureModeUpdate", r.RowIndex)), TextBox)
                    Dim strFailureModeText As String = tbfailureModeOther.Text

                    If strFailureModeText = "" Then
                        Dim clsEvent2 As New clsRIWorkspaceNew
                        With clsEvent2
                            .RIFailureModeSeq = hfRImodeSeqID.Value
                            .Username = userProfile.Username
                            .DeleteRIFailureMode()
                        End With

                        'Need to rebuild cause/reason/rootcause in case deleted mode had dependencies
                        'populateFailureModeUpdate()
                        'BuildCauseRepeater()
                        'BuildReasonRepeater()
                        'BuildRootCauseRepeater()
                        'Exit Sub

                    Else
                        Dim clsEvent2 As New clsRIWorkspaceNew
                        With clsEvent2
                            .RIFailureSeq = RIFailureEventSEQID
                            .RIFailureModeSeq = hfRImodeSeqID.Value
                            .RIFailureModeDesc = strFailureModeText
                            .RefFailureModeSeq = FailureModeSeqs
                            .Username = userProfile.Username
                            .SaveRIFailureMode()
                        End With
                    End If

                Next

                'Check if new mode has been entered
                If FailureModeOthertb <> "" Then
                    Dim clsEvent2 As New clsRIWorkspaceNew

                    With clsEvent2

                        .RIFailureSeq = RIFailureEventSEQID
                        .RIFailureModeDesc = FailureModeOthertb
                        .Username = userProfile.Username
                        .SaveRIFailureMode()

                    End With
                End If

                _hfFailureEvent.Value = RIFailureEventSEQID

                'SaveRootCauses(sender, e)
                'SaveReasons(sender, e)
                'SaveCauses(sender, e)
                populateFailureModeUpdate()
            End If

            BuildCauseRepeater()
            BuildReasonRepeater()
            BuildRootCauseRepeater()

        Catch ex As Exception
            RI.SharedFunctions.HandleError("SaveModes", , ex)
        Finally
        End Try
    End Sub
    Sub modeChange(ByVal sender As Object, ByVal e As System.EventArgs)
        modesChanged = True
    End Sub
    Sub causeChange(ByVal sender As Object, ByVal e As System.EventArgs)
        causesChanged = True
    End Sub
    Sub reasonChange(ByVal sender As Object, ByVal e As System.EventArgs)
        reasonsChanged = True
    End Sub

End Class
