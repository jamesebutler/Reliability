
Partial Class ucIncidentTypes
    Inherits System.Web.UI.UserControl

    Dim mIncidentTypeData As IncidentTypeData

    Public Enum IncidentMode
        Search = 0
        Enter = 1
    End Enum

    Public Enum AndOr
        ORStatement = 0
        ANDStatement = 1
    End Enum
    Public Enum EHSTypes
        Safety = 0
        Environmental = 1
    End Enum
    Private mDisplayMode As IncidentMode = IncidentMode.Search
    Public Property DisplayMode() As IncidentMode
        Get
            Return mDisplayMode
        End Get
        Set(ByVal value As IncidentMode)
            mDisplayMode = value
        End Set
    End Property

    Public Property SearchMode() As AndOr
        Get
            If Me._rblIncidentTypeAnd.Checked = True Then
                Return AndOr.ANDStatement
            Else
                Return AndOr.ORStatement
            End If
        End Get
        Set(ByVal value As AndOr)
            If value = AndOr.ANDStatement Then
                Me._rblIncidentTypeAnd.Checked = True
                Me._rblIncidentTypeOr.Checked = False
            Else
                Me._rblIncidentTypeAnd.Checked = False
                Me._rblIncidentTypeOr.Checked = True
            End If
        End Set
    End Property

    Public Property RTS() As String
        Get
            If DisplayMode = IncidentMode.Search Then
                Return RI.SharedFunctions.GetCheckBoxValues(_cblRTS)
            Else
                Return _rblRTS.SelectedValue
            End If
        End Get
        Set(ByVal value As String)
            If DisplayMode = IncidentMode.Search Then
                RI.SharedFunctions.SetCheckBoxValues(_cblRTS, value)
            Else
                If _rblRTS.Items.FindByValue(value) IsNot Nothing Then
                    _rblRTS.SelectedValue = value
                    If value.ToLower = "yes" Then
                        _rblRTS.Items.FindByValue("No").Enabled = False
                        _rblRTS.Items.FindByValue("Yes").Enabled = True
                    Else
                        _rblRTS.Items.FindByValue("No").Enabled = True
                        _rblRTS.Items.FindByValue("Yes").Enabled = False
                    End If
                End If                
            End If

        End Set
    End Property
    Public Property PPR() As String
        Get
            If DisplayMode = IncidentMode.Search Then
                Return RI.SharedFunctions.GetCheckBoxValues(_cblPPR)
            Else
                Return _rblPPR.SelectedValue
            End If
        End Get
        Set(ByVal value As String)
            If DisplayMode = IncidentMode.Search Then
                _cblPPR.ClearSelection()
                RI.SharedFunctions.SetCheckBoxValues(_cblPPR, value)
            Else
                If _rblPPR.Items.FindByValue(value) IsNot Nothing Then
                    _rblPPR.SelectedValue = value
                    If value.ToLower = "yes" Then
                        _rblPPR.Items.FindByValue("No").Enabled = False
                        _rblPPR.Items.FindByValue("Yes").Enabled = True
                    Else
                        _rblPPR.Items.FindByValue("No").Enabled = True
                        _rblPPR.Items.FindByValue("Yes").Enabled = False
                    End If
                End If
            End If

        End Set
    End Property

    Public Property Recordable() As String
        Get
            If DisplayMode = IncidentMode.Search Then
                Return RI.SharedFunctions.GetCheckBoxValues(_cblRecordable)
            Else
                Return _rblRecordable.SelectedValue
            End If
        End Get
        Set(ByVal value As String)
            If DisplayMode = IncidentMode.Search Then
                _cblRecordable.ClearSelection()
                RI.SharedFunctions.SetCheckBoxValues(_cblRecordable, value)
            Else
                If _rblRecordable.Items.FindByValue(value) IsNot Nothing Then
                    _rblRecordable.SelectedValue = value
                End If
            End If
        End Set
    End Property

    Public Property Chronic() As String
        Get
            If DisplayMode = IncidentMode.Search Then
                Return RI.SharedFunctions.GetCheckBoxValues(_cblChronic)
            Else
                Return _rblChronic.SelectedValue
            End If
        End Get
        Set(ByVal value As String)
            If DisplayMode = IncidentMode.Search Then
                RI.SharedFunctions.SetCheckBoxValues(_cblChronic, value)
            Else
                If _rblChronic.Items.FindByValue(value) IsNot Nothing Then
                    _rblChronic.SelectedValue = value
                End If
            End If
        End Set
    End Property
    

    Public Property Quality() As String
        Get
            If DisplayMode = IncidentMode.Search Then
                Return RI.SharedFunctions.GetCheckBoxValues(_cblQuality)
            Else
                Return _cblQuality.SelectedValue
            End If
        End Get
        Set(ByVal value As String)
            If DisplayMode = IncidentMode.Search Then
                RI.SharedFunctions.SetCheckBoxValues(_cblQuality, value)
            Else
                If _cblQuality.Items.FindByValue(value) IsNot Nothing Then
                    _cblQuality.SelectedValue = value
                End If
            End If
        End Set
    End Property

    Public Property RCFA() As String
        Get
            If DisplayMode = IncidentMode.Search Then
                Return RI.SharedFunctions.GetCheckBoxValues(_cblRCFA)
            Else
                Return _rblRCFA.SelectedValue
            End If
        End Get
        Set(ByVal value As String)
            If DisplayMode = IncidentMode.Search Then
                RI.SharedFunctions.SetCheckBoxValues(_cblRCFA, value)
            Else
                If _rblRCFA.Items.FindByValue(value) IsNot Nothing Then
                    _rblRCFA.SelectedValue = value
                End If
            End If
        End Set
    End Property

    Public Property CertifiedKill() As String
        Get
            If DisplayMode = IncidentMode.Search Then
                Return RI.SharedFunctions.GetCheckBoxValues(_cblCertifiedKill)
            Else
                Return _rblCertifiedKill.SelectedValue
            End If
        End Get
        Set(ByVal value As String)
            If DisplayMode = IncidentMode.Search Then
                RI.SharedFunctions.SetCheckBoxValues(_cblCertifiedKill, value)
            Else
                If _rblCertifiedKill.Items.FindByValue(value) IsNot Nothing Then
                    _rblCertifiedKill.SelectedValue = value
                End If
            End If
        End Set
    End Property
    Public Property ConstrainedAreas() As String
        Get
            If DisplayMode = IncidentMode.Search Then
                Return RI.SharedFunctions.GetCheckBoxValues(_cblConstrainedAreas)
            Else
                Return _cblConstrainedAreas.SelectedValue
            End If
        End Get
        Set(ByVal value As String)
            If DisplayMode = IncidentMode.Search Then
                RI.SharedFunctions.SetCheckBoxValues(_cblConstrainedAreas, value)
            Else
                If _cblConstrainedAreas.Items.FindByValue(value) IsNot Nothing Then
                    _cblConstrainedAreas.SelectedValue = value
                End If
            End If
        End Set
    End Property
    Public Property SchedUnsched() As String
        Get
            If DisplayMode = IncidentMode.Search Then
                Return RI.SharedFunctions.GetCheckBoxValues(_cblSchedUnsched)
            Else
                Return _rblSchedUnsched.SelectedValue
            End If
        End Get
        Set(ByVal value As String)
            If DisplayMode = IncidentMode.Search Then
                RI.SharedFunctions.SetCheckBoxValues(_cblSchedUnsched, value)
            Else
                If _rblSchedUnsched.Items.FindByValue(value) IsNot Nothing Then
                    _rblSchedUnsched.SelectedValue = value
                End If
            End If
        End Set
    End Property
    Public ReadOnly Property ConstrainedAreasID() As String
        Get
            Return Me._cblConstrainedAreas.ClientID
        End Get
    End Property
    Public ReadOnly Property SRRID() As String
        Get
            Return Me._cblSRR.ClientID
        End Get
    End Property
    Public ReadOnly Property RecordableID() As String
        Get
            Return Me._rblRecordable.ClientID()
        End Get
    End Property
    Public Function GetEHSType() As String
        Dim index As Integer = -1
        For i As Integer = 0 To _cblSafety.Items.Count - 1
            If _cblSafety.Items(i).Selected Then
                index = i
            End If
        Next
        If index > 9 Then
            Return "Environmental"
        Else
            Return "Safety"
        End If
    End Function
    Public Property SRR() As String
        Get
            ' If DisplayMode = IncidentMode.Search Then
            Return RI.SharedFunctions.GetCheckBoxValues(_cblSRR)
            'Else
            'Return _cblSRR.SelectedValue
            'End If
        End Get
        Set(ByVal value As String)
            'If DisplayMode = IncidentMode.Search Then
            RI.SharedFunctions.SetCheckBoxValues(_cblSRR, value)
            ' Else
            'If _cblSRR.Items.FindByValue(value) IsNot Nothing Then
            '_cblSRR.SelectedValue = value
            'End If
            ' End If
        End Set
    End Property
    Public Property Safety() As String
        Get
            Return RI.SharedFunctions.GetCheckBoxValues(_cblSafety)
        End Get
        Set(ByVal value As String)
            RI.SharedFunctions.SetCheckBoxValues(_cblSafety, value)
        End Set
    End Property
    Public Property IRISNumber() As String
        Get
            Return _txtIRIS.Text
        End Get
        Set(ByVal value As String)
            _txtIRIS.Text = value
        End Set
    End Property

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Page.IsPostBack = False Or Me.IsViewStateEnabled = False Then
            PopulateIncidentTypes()
        End If
    End Sub
    


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim iploc As New IP.Bids.Localization.WebLocalization
        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "IncidentTypes") Then Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "IncidentTypes", Page.ResolveClientUrl("~/ri/User Controls/Common/IncidentType.js"))
        Dim popupJS As String = "Javascript:displayModalPopUpWindow('{0}','{1}','{2}','{3}','{4}');"
        'displayModalPopUpWindow(url,name,title,w,h)
        'Me._hplCertifiedKillDefinition.NavigateUrl = String.Format(popupJS, Page.ResolveUrl("~/ri/files/CertifiedKillDefinition.aspx"), "CK", "Гарантированно устранён", "870", "400")
        Me._hplCertifiedKillDefinition.NavigateUrl = "javascript:var x=dhtmlmodal.open('DefinitionCertifiedKill', 'div', '_divCertifiedKillDefinition', '" & iploc.GetResourceValue("Certified Kill Definition", False, "Shared") & "', 'width=870px,height=400px,center=1,resize=0,scrolling=1');"
        'Me._hplCertifiedKillDefinition.NavigateUrl = String.Format(popupJS, Page.ResolveUrl("~/ri/files/CertifiedKillDefinition.aspx"), "CK", iploc.GetResourceValue("Certified Kill", False, "Shared"), "870", "400")
        'Me._hplCertifiedKillDefinition.NavigateUrl = "String.Format(popupJS, Page.ResolveUrl('~/ri/files/CertifiedKillDefinition.aspx'), 'CK',' " & iploc.GetResourceValue("Certified Kill", False, "Shared") & " ', '870', '400');"
        Me._hypChronic.NavigateUrl = String.Format(popupJS, Page.ResolveUrl("~/ri/files/ManagingPCF" & iploc.CurrentLocale & ".pdf#view=fit"), "MC", "Managing Chronic", "900", "700")
        'Me._hypRecordableDefinition.NavigateUrl = String.Format(popupJS, Page.ResolveUrl("~/ri/files/CriteriaCost.aspx"), "CC", iploc.GetResourceValue("Criteria Cost", False, "Shared"), "870", "550")
        Me._hypRecordableDefinition.NavigateUrl = "javascript:var x=dhtmlmodal.open('DefinitionCriteriaCost1', 'div', '_divCriteriaCostDefinition', '" & iploc.GetResourceValue("Criteria Cost Definition", False, "Shared") & "', 'width=870px,height=800px,center=1,resize=0,scrolling=1');"


    End Sub
    Public Sub RefreshDisplay()
        PopulateIncidentTypes()
    End Sub
    Private Sub DisplayIncidentTypes()
        If Me.DisplayMode = IncidentMode.Enter Then
            _lblDisplayresults.Visible = False
            _rblIncidentTypeOr.Visible = False
            _rblIncidentTypeAnd.Visible = False
            Me._cblRTS.Visible = False
            Me._rblRTS.Visible = True
            Me._cblPPR.Visible = False
            Me._rblPPR.Visible = True
            Me._rblRecordable.Visible = True
            Me._cblRecordable.Visible = False
            Me._rblCertifiedKill.Visible = True
            Me._cblCertifiedKill.Visible = False
            Me._cblConstrainedAreas.Visible = True
            'Me._rblConstrainedAreas.Visible = True
            'Me._rblConstrainedAreas.Enabled = False
            Me._cblConstrainedAreas.Enabled = False
            Me._cblChronic.Visible = False
            Me._rblChronic.Visible = True
            Me._cblSRR.Visible = True
            'Me._rblSRR.Visible = True
            Me._cblQuality.Visible = True
            Me._cblRCFA.Visible = False
            Me._rblRCFA.Visible = True
            Me._cblSafety.Visible = True
            Me._txtIRIS.Visible = True
            Me._lblIRIS.Visible = True
            Me._txtIRIS.Visible = True
            Me._rblSchedUnsched.Visible = True
            Me._cblSchedUnsched.Visible = False
        Else
            _lblDisplayresults.Visible = True
            _rblIncidentTypeOr.Visible = True
            _rblIncidentTypeAnd.Visible = True
            If Me._rblIncidentTypeOr.Checked = False And Me._rblIncidentTypeAnd.Checked = False Then Me._rblIncidentTypeOr.Checked = True
            Me._cblRTS.Visible = True
            Me._rblRTS.Visible = False
            Me._cblPPR.Visible = True
            Me._rblPPR.Visible = False
            Me._rblRecordable.Visible = False
            Me._cblRecordable.Visible = True
            Me._rblCertifiedKill.Visible = False
            Me._cblCertifiedKill.Visible = True
            Me._cblConstrainedAreas.Visible = True
            'Me._rblConstrainedAreas.Visible = False
            Me._cblChronic.Visible = True
            Me._rblChronic.Visible = False
            Me._cblSRR.Visible = True
            'Me._rblSRR.Visible = False
            Me._cblQuality.Visible = True
            Me._cblRCFA.Visible = True
            Me._rblRCFA.Visible = False
            Me._cblSafety.Visible = True
            Me._lblIRIS.Visible = True
            Me._txtIRIS.Visible = True
            Me._hypRecordableDefinition.Visible = False
            Me._hplCertifiedKillDefinition.Visible = False
            Me._hypChronic.Visible = False
            Me._rblSchedUnsched.Visible = False
            Me._cblSchedUnsched.Visible = True

        End If
    End Sub
    'Private Function GetFormValue(ByVal id As WebControl) As String
    '    Dim retval As String = String.Empty
    '    If Request.Form.Item(id.UniqueID) IsNot Nothing Then
    '        retval = Request.Form.Item(id.UniqueID)
    '    End If
    '    Return retval
    'End Function
    Private Sub PopulateIncidentTypes()
        Dim AllFlag As Boolean
        Dim li As New OrderedDictionary  'Hashtable


        If DisplayMode = IncidentMode.Search Then AllFlag = True

        'RTS
        With li
            .Clear()
            '.Add(Resources.Shared.incidenttypeNo, "No")
            .Add("No", "No")
        End With
        If AllFlag = True Then 'Search Mode
            'li.Insert(0, "All", "All")
            li.Add("Downtime", "Downtime")
            li.Add("Slowback", "Slowback")

            _cblRTS.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_cblRTS, li, False, False, False, True)
            'RI.SharedFunctions.SetCheckBoxValues(_cblRTS, GetFormValue(_cblRTS))
            _cblRTS.Attributes.Add("onClick", "unCheckNo(this," & li.Count & ");")
            If _cblRTS.SelectedIndex < 0 Then
                If _cblRTS.Items.FindByValue("No") IsNot Nothing Then
                    _cblRTS.Items.FindByValue("No").Selected = True
                End If
            End If

        Else
            'li.Insert(0, Resources.Shared.incidenttypeYes, "Yes")
            li.Insert(0, "Yes", "Yes")
            _rblRTS.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_rblRTS, li, False, False, False, True)

            If _rblRTS.SelectedIndex < 0 Then
                If _rblRTS.Items.FindByValue("No") IsNot Nothing Then
                    _rblRTS.Items.FindByValue("No").Selected = True
                End If
                If _rblRTS.Items.FindByValue("Yes").Selected = True Then
                    _rblRTS.Items.FindByValue("No").Enabled = False
                    _rblRTS.Items.FindByValue("Yes").Enabled = True
                Else
                    _rblRTS.Items.FindByValue("No").Enabled = True
                    _rblRTS.Items.FindByValue("Yes").Enabled = False
                End If
            End If
        End If


        'PPR
        With li
            .Clear()
            '.Add(Resources.Shared.incidenttypeNo, "No")
            .Add("Yes", "Yes")
            .Add("No", "No")
        End With
        If AllFlag = True Then 'Search Mode
            'li.Insert(0, "All", "All")

            _cblPPR.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_cblPPR, li, False, False, False, True)
            'RI.SharedFunctions.SetCheckBoxValues(_cblPPR, GetFormValue(_cblPPR))
            _cblPPR.Attributes.Add("onClick", "unCheckNo(this," & li.Count & ");")
            If _cblPPR.SelectedIndex < 0 Then
                If _cblPPR.Items.FindByValue("No") IsNot Nothing Then
                    _cblPPR.Items.FindByValue("No").Selected = True
                End If
            End If

        Else
            'li.Insert(0, Resources.Shared.incidenttypeYes, "Yes")
            'li.Insert(0, "Yes", "Yes")
            _rblPPR.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_rblPPR, li, False, False, False, True)

            If _rblPPR.SelectedIndex < 0 Then
                If _rblPPR.Items.FindByValue("No") IsNot Nothing Then
                    _rblPPR.Items.FindByValue("No").Selected = True
                End If
                If _rblPPR.Items.FindByValue("Yes").Selected = True Then
                    _rblPPR.Items.FindByValue("No").Enabled = False
                    _rblPPR.Items.FindByValue("Yes").Enabled = True
                Else
                    _rblPPR.Items.FindByValue("No").Enabled = True
                    _rblPPR.Items.FindByValue("Yes").Enabled = False
                End If
            End If
        End If

        'If AllFlag = True Then 'Search Mode
        '    'li.Insert(0, "All", "All")
        '    _cblPPR.RepeatDirection = RepeatDirection.Horizontal
        '    RI.SharedFunctions.BindList(_cblPPR, li, False, False, False, True)
        '    _cblPPR.Attributes.Add("onClick", "CheckBoxToRadio(this," & li.Count & ");")
        '    If _cblPPR.SelectedIndex < 0 Then
        '        If _cblPPR.Items.FindByValue("No") IsNot Nothing Then
        '            _cblPPR.Items.FindByValue("No").Selected = True
        '        End If
        '    End If
        'Else
        '    _rblPPR.RepeatDirection = RepeatDirection.Horizontal
        '    RI.SharedFunctions.BindList(_rblPPR, li, False, False, False, True)
        '    If _rblPPR.SelectedIndex < 0 Then
        '        If _rblPPR.Items.FindByValue("Yes") IsNot Nothing Then
        '            _rblPPR.Items.FindByValue("Yes").Selected = True
        '        End If
        '    End If
        'End If

        'Recordable
        With li
            .Clear()
            '.Add(Resources.Shared.incidenttypeYes, "Yes")
            '.Add(Resources.Shared.incidenttypeNo, "No")
            .Add("Yes", "Yes")
            .Add("No", "No")
        End With
        If AllFlag = True Then 'Search Mode
            'li.Insert(0, "All", "All")
            _cblRecordable.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_cblRecordable, li, False, False, False, True)
            _cblRecordable.Attributes.Add("onClick", "CheckBoxToRadio(this," & li.Count & ");")
            If _cblRecordable.SelectedIndex < 0 Then
                If _cblRecordable.Items.FindByValue("Yes") IsNot Nothing Then
                    _cblRecordable.Items.FindByValue("Yes").Selected = True
                End If
            End If
        Else
            _rblRecordable.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_rblRecordable, li, False, False, False, True)
            If _rblRecordable.SelectedIndex < 0 Then
                If _rblRecordable.Items.FindByValue("Yes") IsNot Nothing Then
                    _rblRecordable.Items.FindByValue("Yes").Selected = True
                End If
            End If
        End If

        'Chronic
        With li
            .Clear()
            '.Add(Resources.Shared.incidenttypeNo, "No")
            '.Add(Resources.Shared.incidenttypeRegular, "Regular")
            '.Add(Resources.Shared.incidenttypeAreaTop, "Area Top")
            '.Add(Resources.Shared.incidenttypeMillTop, "Mill Top")
            .Add("No", "No")
            .Add("Regular", "Regular")
            .Add("Area Top", "Area Top")
            .Add("Mill Top", "Mill Top")
        End With
        If AllFlag = True Then 'Search(Mode)
            'li.Insert(1, Resources.Shared.incidenttypeAll, "All")
            li.Insert(1, "All", "All")
            _cblChronic.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_cblChronic, li, False, False, False, True)
            _cblChronic.Attributes.Add("onClick", "unCheckNo(this," & li.Count & ");")
        Else
            _rblChronic.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_rblChronic, li, False, False, False, True)
            If _rblChronic.SelectedIndex < 0 Then
                If _rblChronic.Items.FindByValue("No") IsNot Nothing Then
                    _rblChronic.Items.FindByValue("No").Selected = True
                End If
            End If
        End If





        'Quality
        With li
            .Clear()
            '.Add(Resources.Shared.incidenttypeAudit, "Audit")
            '.Add(Resources.Shared.incidenttypeOther, "Other")
            '.Add(Resources.Shared.incidenttypeQuality, "Quality")
            .Add("Audit", "Audit")
            .Add("Other", "Other")
            .Add("Quality", "Quality")
        End With
        If AllFlag = True Then
            'li.Insert(0, Resources.Shared.incidenttypeAll, "All")
            li.Insert(0, "All", "All")
            _cblQuality.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_cblQuality, li, False, False, False, True)
            _cblQuality.Attributes.Add("onClick", "checkAll(this," & li.Count & ");")
        Else
            _cblQuality.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_cblQuality, li, False, False, False, True)
            'use code to make this work like a radio button
            _cblQuality.Attributes.Add("onClick", "CheckBoxToRadio(this," & li.Count & ");")
        End If

        'RCFA
        With li
            .Clear()
            '.Add(Resources.Shared.incidenttypeNo, "No")
            .Add("No", "No")
            '.Add(Resources.Shared.incidenttypeIIR, "IIR")
            .Add("Mini RCFA", "Mini RCFA")
            '.Add(Resources.Shared.incidenttypeMiniRCFA, "Mini RCFA")
            '.Add(Resources.Shared.incidenttypeFullRCFA, "Full RCFA")
            .Add("Full RCFA", "Full RCFA")
        End With
        If AllFlag = True Then
            'li.Insert(1, Resources.Shared.incidenttypeAll, "All")
            li.Insert(1, "All", "All")
            _cblRCFA.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_cblRCFA, li, False, False, False, True)
            _cblRCFA.Attributes.Add("onClick", "unCheckNo(this," & li.Count & ");")
        Else
            _rblRCFA.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_rblRCFA, li, False, False, False, True)
            If _rblRCFA.SelectedIndex < 0 Then
                If _rblRCFA.Items.FindByValue("No") IsNot Nothing Then
                    _rblRCFA.Items.FindByValue("No").Selected = True
                End If
            End If
        End If

        'Certified Kill
        With li
            .Clear()
            '.Add(Resources.Shared.incidenttypeYes, "Yes")
            '.Add(Resources.Shared.incidenttypeNo, "No")
            .Add("Yes", "Yes")
            .Add("No", "No")
        End With
        If AllFlag = True Then
            'li.Insert(0, "All", "All")
            _cblCertifiedKill.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_cblCertifiedKill, li, False, False, False, True)
            _cblCertifiedKill.Attributes.Add("onClick", "CheckBoxToRadio(this," & li.Count & ");")
            If _cblCertifiedKill.SelectedIndex < 0 Then
                If _cblCertifiedKill.Items.FindByValue("All") IsNot Nothing Then
                    _cblCertifiedKill.Items.FindByValue("All").Selected = True
                End If
            End If
        Else
            _rblCertifiedKill.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_rblCertifiedKill, li, False, False, False, True)
            If _rblCertifiedKill.SelectedIndex < 0 Then
                If _rblCertifiedKill.Items.FindByValue("No") IsNot Nothing Then
                    _rblCertifiedKill.Items.FindByValue("No").Selected = True
                End If

            End If
        End If

        'Constrained Area
        With li
            .Clear()
            '.Add(Resources.Shared.incidenttypeAudit, "Audit")
            '.Add(Resources.Shared.incidenttypeOther, "Other")
            '.Add(Resources.Shared.incidenttypeQuality, "Quality")
            .Add("", "Constrained Area")
        End With
        If AllFlag = True Then
            'li.Insert(0, Resources.Shared.incidenttypeAll, "All")
            ' li.Insert(0, "All", "All")
            _cblConstrainedAreas.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_cblConstrainedAreas, li, False, False, False, True)
            _cblConstrainedAreas.Attributes.Add("onClick", "checkAll(this," & li.Count & ");")
        Else
            _cblConstrainedAreas.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_cblConstrainedAreas, li, False, False, False, True)
            'use code to make this work like a radio button
            '_cblConstrainedAreas.Attributes.Add("onClick", "CheckBoxToRadio(this," & li.Count & ");")
        End If

        'SRR
        With li
            .Clear()
            .Add("Any Process DT >= 16 Hr", "Any Process DT >= 16 Hr")
            .Add("Financial Impact >= $100000", "Financial Impact >= $100000")

            'If AllFlag = True Then
            '    .Add("Financial Impact >= $250000", "Financial Impact >= $250000")
            'End If



        End With

        'If AllFlag = True Then
        'li.Insert(0, Resources.Shared.incidenttypeAll, "All")
        'li.Insert(3, "All", "All")
        'End If
        _cblSRR.RepeatDirection = RepeatDirection.Horizontal
        RI.SharedFunctions.BindList(_cblSRR, li, False, False, False, True)
        _cblSRR.Attributes.Add("onClick", "checkAll(this," & li.Count & ");")

        '_cblSRR.Attributes.Add("onClick", "unCheckNo(this," & li.Count & ");")
        'Else
        '_cblSRR.RepeatDirection = RepeatDirection.Horizontal
        'RI.SharedFunctions.BindList(_cblSRR, li, False, False, False, True)

        'End If

        'Safety
        With li
            .Clear()
            .Add("Fire", "Fire")
            .Add("First Aid", "First Aid")
            .Add("Lost Work Day", "Lost Work Day")
            .Add("Motor Vehicle", "Motor Vehicle")
            .Add("Near Miss", "Near Miss")
            .Add("Property Damage", "Property Damage")
            .Add("Safety Recordable", "Safety Recordable")
            .Add("Safety Citation", "Safety Citation")
            .Add("Safety Inspection", "Safety Inspection")
            .Add("Safety Complaint", "Safety Complaint")
            .Add("Release", "Release")
            .Add("Reportable Release", "Reportable Release")
            .Add("Permit Excursion", "Permit Excursion")
            .Add("Spill", "Spill")
            .Add("Environmental Consent Decree", "Environmental Consent Decree")
            .Add("Environmental Inspection", "Environmental Inspection")
            .Add("Environmental Audit", "Environmental Audit")
            .Add("Environmental Complaint", "Environmental Complaint")
            .Add("Environmental Notice of Violation", "Environmental Notice of Violation")
            .Add("Other", "Other")


        End With
        If AllFlag = True Then
            li.Insert(0, "All", "All")
        End If
        _cblSafety.Width = Unit.Percentage(100)
        _cblSafety.RepeatDirection = RepeatDirection.Vertical
        _cblSafety.RepeatColumns = "5"
        RI.SharedFunctions.BindList(_cblSafety, li, False, False, False, True)
        _cblSafety.Attributes.Add("onClick", "checkAll(this," & li.Count & ");")


        'Scheduled/Unschedul;ed
        With li
            .Clear()
            '.Add(Resources.Shared.incidenttypeYes, "Yes")
            '.Add(Resources.Shared.incidenttypeNo, "No")
            .Add("Scheduled", "Yes")
            .Add("Unscheduled", "No")
        End With
        If AllFlag = True Then
            'li.Insert(0, "All", "All")
            _cblSchedUnsched.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_cblSchedUnsched, li, False, False, False, True)
            _cblSchedUnsched.Attributes.Add("onClick", "CheckBoxToRadio(this," & li.Count & ");")
            If _cblSchedUnsched.SelectedIndex < 0 Then
                If _cblSchedUnsched.Items.FindByValue("All") IsNot Nothing Then
                    _cblSchedUnsched.Items.FindByValue("All").Selected = True
                End If
            End If
        Else
            _rblSchedUnsched.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_rblSchedUnsched, li, False, False, False, True)
            If _rblSchedUnsched.SelectedIndex < 0 Then
                If _rblSchedUnsched.Items.FindByValue("No") IsNot Nothing Then
                    _rblSchedUnsched.Items.FindByValue("No").Selected = True
                End If

            End If
        End If
        Me.DisplayIncidentTypes()
    End Sub



    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        'This has to stay in the pre-render step otherwise the selected values are not 
        'properly passed to the database call
        'PopulateIncidentTypes()
    End Sub
    <Serializable()> _
    Friend Structure IncidentTypeData
        Dim Safety As String
        Dim RTS As String
        Dim PPR As String
        Dim Recordable As String
        Dim Chronic As String
        Dim SRR As String
        Dim Quality As String
        Dim RCFA As String
        Dim CertifiedKill As String
        Dim ConstrainedAreas As String
        Dim ConstrainedAreasID As String
        Dim SchedUnscheduled As String
    End Structure
End Class
