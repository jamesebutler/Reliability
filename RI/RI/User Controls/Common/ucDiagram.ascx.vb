
Partial Class User_Controls_ucDiagram
    Inherits System.Web.UI.UserControl

    Event NextStepClicked()
    Event CancelClicked()
    Event PrevStepClicked()

    Public Property EnableValidation() As Boolean
        Get
            Return Me._rfvHP.Enabled
        End Get
        Set(ByVal value As Boolean)
            Me._rfvHP.Enabled = value
            Me._rfvRPM.Enabled = value
        End Set
    End Property
   
    Public Property HP() As Decimal
        Get
            If Me.CurrentMotorType = MotorType.AboveNEMA Then
                If _txtHP.Text.Length > 0 Then
                    Return _txtHP.Text
                Else
                    Return 0
                End If

            Else
                If _ddlHP.SelectedValue.Length > 0 Then
                    Return _ddlHP.SelectedValue
                Else
                    Return 0
                End If
            End If
           
        End Get
        Set(ByVal value As Decimal)
            If Me.CurrentMotorType = MotorType.AboveNEMA Then
                _txtHP.Text = value
            Else
                If _ddlHP.Items.FindByValue(value) IsNot Nothing Then
                    _ddlHP.SelectedValue = value
                End If
            End If
            
        End Set
    End Property

    Public Property RPM() As Integer
        Get
            If _ddlRPM.SelectedValue.Length > 0 Then
                Return Me._ddlRPM.SelectedValue
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            If Me._ddlRPM.Items.FindByValue(value) IsNot Nothing Then
                Me._ddlRPM.SelectedValue = value
            End If
        End Set
    End Property

    Public Property RepairPrice() As Decimal
        Get
            If IsNumeric(Me._txtRepairPrice.Text) Then
                Return CDec(_txtRepairPrice.Text)
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Decimal)
            Me._txtRepairPrice.text = value
        End Set
    End Property

    Public Property Efficiency() As Decimal
        Get
            If IsNumeric(Me._txtEfficiency.Text) Then
                Return CDec(Me._txtEfficiency.Text)
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Decimal)
            _txtEfficiency.Text = value
        End Set
    End Property

    Public ReadOnly Property OldEfficiency() As Decimal
        Get
            If IsNumeric(Me._txtOldEfficiency.Text) Then
                Return CDec(_txtOldEfficiency.Text)
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property NewEfficiency() As Decimal
        Get
            If IsNumeric(Me._txtnewEfficiency.Text) Then
                Return CDec(_txtnewEfficiency.Text)
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property NewMotorPrice() As Decimal
        Get
            If IsNumeric(Me._txtNewMotorPrice.Text) Then
                Return CDec(_txtNewMotorPrice.Text)
            Else
                Return 0
            End If
        End Get
    End Property
   
    Public Enum ButtonStyle As Integer
        NextCancel = 1
        Restart = 2
        BackNextCancel = 3
        ReportCancel = 4
    End Enum
    Private mCurrentMotorType As MotorType
    Public Property CurrentMotorType() As MotorType
        Get
            Return mCurrentMotorType
        End Get
        Set(ByVal value As MotorType)
            mCurrentMotorType = value
        End Set
    End Property
    Public Enum MotorType As Integer
        NEMA = 1
        AboveNEMA = 2
    End Enum
    Public ReadOnly Property GetStartOverJS() As String
        Get
            Dim sb As New StringBuilder
            sb.Append("$get('")
            sb.Append(Me._btnCancel.ClientID)
            sb.Append("').click();")
            Return sb.ToString
        End Get
    End Property

    Private mButtonMode As ButtonStyle
    Public Property ButtonMode() As ButtonStyle
        Get
            Return mButtonMode
        End Get
        Set(ByVal value As ButtonStyle)
            mButtonMode = value
            DisplayButtons(value)
        End Set
    End Property
    Private mNumberOfSteps As Integer = 0
    Public Property NumberOfSteps() As Integer
        Get
            Return mNumberOfSteps
        End Get
        Set(ByVal value As Integer)
            mNumberOfSteps = value
        End Set
    End Property

    Public Property YesNextStep() As Integer
        Get
            Return _lblYesNextStep.Text
        End Get
        Set(ByVal value As Integer)
            _lblYesNextStep.Text = value
        End Set
    End Property

    Public Property CurrentStep() As Integer
        Get
            Return _lblCurrentStep.Text
        End Get
        Set(ByVal value As Integer)
            _lblCurrentStep.Text = value
        End Set
    End Property
    Public Property PreviousStep() As Integer
        Get
            Return _lblPreviousStep.Text
        End Get
        Set(ByVal value As Integer)
            _lblPreviousStep.Text = value
        End Set
    End Property
    Public Property NoNextStep() As Integer
        Get
            Return _lblNoNextStep.Text
        End Get
        Set(ByVal value As Integer)
            _lblNoNextStep.Text = value
        End Set
    End Property
    Public Enum DiagramView As Integer
        Decision = 0
        Process = 1
        PredefinedProcess = 2
        Data = 3
        Input = 4
        AutoDecision = 5
        Report = 6
        InputOldEfficiency = 7
        InputRepairPrice = 8
        InputNewPrice = 9
        InputNewEfficiency = 10
    End Enum
    Private mSelectedDiagram As DiagramView
    Public Property SelectedDiagram() As DiagramView
        Get
            Return mSelectedDiagram
        End Get
        Set(ByVal value As DiagramView)
            mSelectedDiagram = value
            Me._mvDiagram.ActiveViewIndex = value
        End Set
    End Property
    Public Property DiagramText() As String
        Get
            Select Case SelectedDiagram
                Case DiagramView.Data
                    Return Me._lbldata.text
                Case DiagramView.Decision
                    Return Me._lblDecision.Text
                Case DiagramView.PredefinedProcess
                    Return Me._lblpredefinedProcess.text
                Case DiagramView.Process
                    Return Me._lblProcess.Text
                Case DiagramView.Input
                    Return Me._lblInput.Text
                Case DiagramView.AutoDecision
                    Return Me._lblAutoDecision.Text
                Case DiagramView.Report
                    Return Me._divReportData.InnerHtml
                Case DiagramView.InputNewEfficiency
                    Return Me._lblInputNewEfficiency.Text
                Case DiagramView.InputNewPrice
                    Return Me._lblInputNewPrice.Text
                Case DiagramView.InputOldEfficiency
                    Return Me._lblInputOldEfficiency.Text
                Case DiagramView.InputRepairPrice
                    Return Me._lblInputRepairPrice.Text
                Case Else
                    Return ""
            End Select
        End Get
        Set(ByVal value As String)
            Select Case SelectedDiagram
                Case DiagramView.Data
                    Me._lbldata.text = value
                Case DiagramView.Decision
                    Me._lblDecision.Text = value
                Case DiagramView.PredefinedProcess
                    Me._lblpredefinedProcess.text = value
                Case DiagramView.Process
                    Me._lblProcess.Text = value
                Case DiagramView.Input
                    _lblInput.Text = value
                Case DiagramView.AutoDecision
                    _lblAutoDecision.Text = value
                Case DiagramView.Report
                    Me._divReportData.InnerHtml = value
                Case DiagramView.InputNewEfficiency
                    _lblInputNewEfficiency.Text = value
                Case DiagramView.InputNewPrice
                    _lblInputNewPrice.Text = value
                Case DiagramView.InputOldEfficiency
                    _lblInputOldEfficiency.Text = value
                Case DiagramView.InputRepairPrice
                    _lblInputRepairPrice.Text = value
            End Select
        End Set
    End Property
    Public Property AutoAnswer() As String
        Get
            Return Me._lblautoanswer.text
        End Get
        Set(ByVal value As String)
            _lblautoanswer.text = value
        End Set
    End Property
    Public Property SuggestedAnswer() As String
        Get
            Return Me._lblSuggestedDecision.Text
        End Get
        Set(ByVal value As String)
            Me._lblSuggestedDecision.Text = value
        End Set
    End Property
    Public Property Decision() As Boolean
        Get
            If _rblDecisionYesNo.SelectedValue.Length > 0 Then
                If Me._rblDecisionYesNo.SelectedValue.ToUpper = "FALSE" Or Me._rblDecisionYesNo.SelectedValue = False Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If

        End Get
        Set(ByVal value As Boolean)
            If _rblDecisionYesNo.Items.FindByValue(value) IsNot Nothing Then
                _rblDecisionYesNo.SelectedValue = value
            End If
        End Set
    End Property

    Public Property PDFLinkText() As String
        Get
            Return _lnkNemapdf.Text
        End Get
        Set(ByVal value As String)
            _lnkNemapdf.Text = value
        End Set
    End Property

    Public Property PDFLink() As String
        Get
            Return _lnkNemapdf.NavigateUrl
        End Get
        Set(ByVal value As String)            
            If value.Length > 0 Then
                _lnkNemapdf.NavigateUrl = value
            End If
        End Set
    End Property
    'Protected Sub _ddlSwitch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlSwitch.SelectedIndexChanged
    '    Me._mvDiagram.ActiveViewIndex = Me._ddlSwitch.SelectedValue
    'End Sub
    Private Function GetGlobalJSVar() As String
        Dim sb As New StringBuilder
        Dim Motor As String
        If Me.CurrentMotorType = MotorType.AboveNEMA Then
            Motor = "ABOVENEMA"
        Else
            Motor = "NEMA"
        End If
        sb.AppendLine()
        sb.Append(" var ddlHP_{0} = $get('")
        sb.Append(Me._ddlHP.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var motorType_{0} = '")
        sb.Append(Motor)
        sb.Append("';")
        sb.AppendLine()
        sb.Append("var ddlRPM_{0} =$get('")
        sb.Append(Me._ddlRPM.ClientID)
        sb.Append("');")
        'sb.AppendLine()
        'sb.Append("var txtNewMotorPrice_{0} =$get('")
        'sb.Append(Me._txtNewMotorPrice.ClientID)
        'sb.Append("');")
        'sb.AppendLine()
        'sb.Append("var txtOldEfficiency_{0}=$get('")
        'sb.Append(Me._txtOldEfficiency.ClientID)
        'sb.Append("');")
        'sb.AppendLine()
        'sb.Append("var txtNewEfficiency_{0}=$get('")
        'sb.Append(Me._txtNewEfficiency.ClientID)
        'sb.Append("');")
        
        Return String.Format(sb.ToString, Me.ClientID.ToString)
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sc As ScriptManager
        sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
        If sc IsNot Nothing Then
            Dim loService As New ServiceReference
            loService.InlineScript = False
            loService.Path = "~/NEMA.asmx"
            sc.Services.Add(loService)
        End If
        ScriptManager.RegisterClientScriptInclude(Me.Page, Page.GetType, "Diagram", Page.ResolveClientUrl("~/ri/User Controls/Common/Diagram.js"))
        ScriptManager.RegisterStartupScript(Me.Page, Page.GetType, "GetGlobalJSVar_" & Me.ClientID.ToString, GetGlobalJSVar, True)
        If _rblDecisionYesNo.SelectedItem Is Nothing Then
            Decision = True
        End If
        Dim Motor As String
        If Me.CurrentMotorType = MotorType.AboveNEMA Then
            Motor = "ABOVENEMA"
            ' Me._cddHP.ContextKey = "NEMA"
            _rfvHP.Enabled = True
            _rfvHP.ValidationGroup = Motor
            _rfvRPM.Enabled = False
            _rfvHP.ControlToValidate = Me._txtHP.ID
            '_cvEfficiency.Enabled = False
            'Me._rfvEfficiency.Enabled = True
            'Me._rfvEfficiency.ValidationGroup = Motor
            'Me._rfvNewEfficiency.Enabled = True
            'Me._rfvNewEfficiency.ValidationGroup = Motor
            'Me._rfvRepairPrice.Enabled = True
            'Me._rfvRepairPrice.ValidationGroup = Motor
            'Me._rfvNewMotorPrice.Enabled = True
            'Me._rfvNewMotorPrice.ValidationGroup = Motor
            Me._btnNext.ValidationGroup = Motor
            Me._btnBack.ValidationGroup = Motor
            Me._btnCancel.CausesValidation = False
            Me._lblRPM.Visible = False
            Me._lblHP.Visible = True
            Me._ddlHP.Visible = False
            Me._txthp.visible = True
            Me._ddlRPM.Visible = False
            'Me._txtEfficiency.ReadOnly = False
            'Me._txtNewEfficiency.ReadOnly = False
            'Me._txtNewMotorPrice.ReadOnly = False
            'Me._txtRepairPrice.ReadOnly = False
            'Me._txtNewMotorPrice.BackColor = Drawing.Color.White
            'Me._txtNewEfficiency.BackColor = Drawing.Color.White
            'If Me._txtEfficiency.Text.Length = 0 Then Me._txtEfficiency.Text = 0
            'If Me._txtNewEfficiency.Text.Length = 0 Then Me._txtNewEfficiency.Text = Me._txtEfficiency.Text
            'Me._txtEfficiency.Attributes.Add("onchange", "var newEff=document.getElementById('" & Me._txtNewEfficiency.ClientID & "'); if(newEff.value==0)newEff.value=this.value;")
            'Me._txtNewEfficiency.Attributes.Add("onchange", "var oldEff=document.getElementById('" & Me._txtEfficiency.ClientID & "'); if(oldEff.value==0)oldEff.value=this.value;")
            'Me._txtNewMotorPrice.Attributes.Add("readonly", "false")
            'Me._txtNewEfficiency.Attributes.Add("readonly", "false")
            _lnkNemapdf.NavigateUrl = "~/NEMA/AboveNemaMotors.pdf"
        Else
            'PopulateHP()
            Me._txtHP.Visible = False
            Motor = "NEMA"
            Me._cddHP.ContextKey = Motor
            Me._cddRPM.ContextKey = Motor
            Me._ddlRPM.Attributes.Add("onchange", "javascript:GetNewMotorPrice('" & Me.ClientID.ToString & "');")
            Me._ddlHP.Attributes.Add("onchange", "javascript:GetNewMotorPrice('" & Me.ClientID.ToString & "');")
            _rfvHP.ValidationGroup = Motor
            Me._rfvRPM.ValidationGroup = Motor
            Me._btnNext.ValidationGroup = Motor
            Me._btnBack.ValidationGroup = Motor
            Me._btnCancel.CausesValidation = False
            'Me._rfvEfficiency.Enabled = True
            'Me._rfvEfficiency.ValidationGroup = Motor
            'Me._rfvNewEfficiency.Enabled = False
            'Me._rfvNewEfficiency.ValidationGroup = Motor
            'Me._rfvRepairPrice.Enabled = True
            'Me._rfvRepairPrice.ValidationGroup = Motor
            'Me._rfvNewMotorPrice.Enabled = False
            'Me._rfvNewMotorPrice.ValidationGroup = Motor
            _lnkNemapdf.NavigateUrl = "~/NEMA/NemaMotors.pdf"
            'Me._txtNewMotorPrice.Attributes.Add("readonly", "true")
            'Me._txtNewEfficiency.Attributes.Add("readonly", "true")
        End If
       
    End Sub

    Protected Sub _btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnNext.Click

        RaiseEvent NextStepClicked()
    End Sub

    Protected Sub _btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnCancel.Click
        RaiseEvent CancelClicked()
    End Sub

    Protected Sub _btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnBack.Click
        RaiseEvent PrevStepClicked()
    End Sub
    Private Sub DisplayButtons(ByVal style As ButtonStyle)
        Select Case style
            Case ButtonStyle.NextCancel
                Me._btnBack.Visible = False
                Me._btnBack.Text = "Back"
                Me._btnNext.Visible = True
                Me._btnNext.Text = "Next"
                Me._btnCancel.Visible = True
                Me._btnCancel.Text = "Start Over"
            Case ButtonStyle.Restart
                Me._btnBack.Visible = True
                Me._btnBack.Text = "Back"
                Me._btnNext.Visible = False
                Me._btnNext.Text = "Next"
                Me._btnCancel.Visible = True
                Me._btnCancel.Text = "Start Over"
            Case ButtonStyle.BackNextCancel
                Me._btnBack.Visible = True
                Me._btnBack.Text = "Back"
                Me._btnNext.Visible = True
                Me._btnNext.Text = "Next"
                Me._btnCancel.Visible = True
                Me._btnCancel.Text = "Start Over"
            Case ButtonStyle.ReportCancel
                Me._btnBack.Visible = False
                Me._btnBack.Text = "Back"
                Me._btnNext.Visible = True
                Me._btnNext.Text = "Report"
                Me._btnCancel.Visible = True
                Me._btnCancel.Text = "Start Over"
        End Select
    End Sub
   
    'Private Sub PopulateHP()
    '    Dim hp As New ListItemCollection
    '    hp.Add(New ListItem("< 900", 899))
    '    hp.Add(New ListItem("900", 900))
    '    hp.Add(New ListItem("1200", 1200))
    '    hp.Add(New ListItem("1800", 1800))
    '    hp.Add(New ListItem("3600", 3600))

    '    With _ddlHP
    '        .Items.Clear()
    '        .DataSource = hp
    '        .DataBind()
    '    End With
    'End Sub

    'Private Sub PopulateRPM()
    '    Dim rpm As New ListItemCollection
    '    With rpm
    '        .Add(New ListItem( "
    '    End With
    'End Sub
   
End Class
