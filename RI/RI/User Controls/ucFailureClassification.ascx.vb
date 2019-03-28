
Partial Class RI_User_Controls_ucFailureClassification
    Inherits System.Web.UI.UserControl

#Region "Properties"
    Public Property ConstrainedAreas() As String
        Get
            If Me._rblConstrainedArea.SelectedItem IsNot Nothing Then
                Return Me._rblConstrainedArea.SelectedItem.Value
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            If _rblConstrainedArea.Items.FindByValue(value) IsNot Nothing Then
                _rblConstrainedArea.Items.FindByValue(value).Selected = True
            End If
        End Set
    End Property

    Public Property CriticalityRating() As String
        Get
            If Me._rblCriticalityRating.SelectedItem IsNot Nothing Then
                Return Me._rblCriticalityRating.SelectedItem.Value
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            If _rblCriticalityRating.Items.FindByValue(value) IsNot Nothing Then
                _rblCriticalityRating.Items.FindByValue(value).Selected = True
            End If
        End Set
    End Property

    Public Property LifeExpectancy() As String
        Get
            If Me._rblLifeExpectancy.SelectedItem IsNot Nothing Then
                Return Me._rblLifeExpectancy.SelectedItem.Value
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            If _rblLifeExpectancy.Items.FindByValue(value) IsNot Nothing Then
                _rblLifeExpectancy.Items.FindByValue(value).Selected = True
            End If
        End Set
    End Property

    Public Property EquipmentCare() As String
        Get
            If Me._rblEquipmentCare.SelectedItem IsNot Nothing Then
                Return Me._rblEquipmentCare.SelectedItem.Value
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            If _rblEquipmentCare.Items.FindByValue(value) IsNot Nothing Then
                _rblEquipmentCare.Items.FindByValue(value).Selected = True
            End If
        End Set
    End Property
#End Region

    ''' <summary>
    ''' Creates global javascript variables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGlobalJSVar() As String
        Dim sb As New StringBuilder
        Dim RIRESOURCES As New IP.Bids.Localization.WebLocalization
        sb.Append("var divFailureClassPointerT1 = $get('")
        sb.Append(Me._divFailureClassPointerT1.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var divFailureClassPointerT2 = $get('")
        sb.Append(Me._divFailureClassPointerT2.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var divFailureClassPointerT3 = $get('")
        sb.Append(Me._divFailureClassPointerT3.ClientID)
        sb.Append("');")
        sb.AppendLine()

        sb.Append("var rblConstrainedArea = $get('")
        sb.Append(_rblConstrainedArea.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var rblCriticalityRating = $get('")
        sb.Append(_rblCriticalityRating.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var rblLifeExpectancy = $get('")
        sb.Append(_rblLifeExpectancy.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var rblEquipmentCare = $get('")
        sb.Append(_rblEquipmentCare.ClientID)
        sb.Append("');")
        sb.Append("var totalClassifiactionValue = $get('")
        sb.Append(Me._txtFailureClass.ClientID)
        sb.Append("');")
        sb.Append("var lblClassificationTierValue = $get('")
        sb.Append(Me._lblClassificationTierValue.ClientID)
        sb.Append("');")
        sb.Append("var TierOneFailure ='")
        sb.Append(RIRESOURCES.GetResourceValue("Tier One Failure"))
        sb.Append("';")
        sb.Append("var TierTwoFailure ='")
        sb.Append(RIRESOURCES.GetResourceValue("Tier Two Failure"))
        sb.Append("';")
        sb.Append("var TierThreeFailure ='")
        sb.Append(RIRESOURCES.GetResourceValue("Tier Three Failure"))
        sb.Append("';")
        sb.AppendLine()
        Return sb.ToString
    End Function

    Public Sub CalculateFailureClassificationScore()
        Dim RIRESOURCES As New IP.Bids.Localization.WebLocalization
        Dim score As Integer = 0

        If Me.ConstrainedAreas.Length > 0 AndAlso IsNumeric(Me.ConstrainedAreas) Then
            score += CInt(Me.ConstrainedAreas)
        End If
        If Me.EquipmentCare.Length > 0 AndAlso IsNumeric(Me.EquipmentCare) Then
            score += CInt(Me.EquipmentCare)
        End If
        If Me.LifeExpectancy.Length > 0 AndAlso IsNumeric(Me.LifeExpectancy) Then
            score += CInt(Me.LifeExpectancy)
        End If
        If Me.CriticalityRating.Length > 0 AndAlso IsNumeric(Me.CriticalityRating) Then
            score += CInt(Me.CriticalityRating)
        End If
        _txtFailureClass.Text = score

        Me._divFailureClassPointerT1.Style.Item("Display") = "None"
        Me._divFailureClassPointerT2.Style.Item("Display") = "None"
        Me._divFailureClassPointerT3.Style.Item("Display") = "None"

        If score >= 19 Then
            Me._divFailureClassPointerT1.Style.Item("Display") = ""
            _lblClassificationTierValue.Text = RIRESOURCES.GetResourceValue("Tier One", False)
        ElseIf score >= 14 Then
            Me._divFailureClassPointerT2.Style.Item("Display") = ""
            _lblClassificationTierValue.Text = RIRESOURCES.GetResourceValue("Tier Two", False)
        ElseIf score >= 8 Then
            Me._divFailureClassPointerT3.Style.Item("Display") = ""
            _lblClassificationTierValue.Text = RIRESOURCES.GetResourceValue("Tier Three", False)
        Else
            'I'm not 100% sure that we should display the arrow when the score is less than 8
            Me._divFailureClassPointerT3.Style.Item("Display") = ""
            _lblClassificationTierValue.Text = RIRESOURCES.GetResourceValue("Tier Three", False)
        End If


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CalculateFailureClassificationScore()
        Page.ClientScript.RegisterStartupScript(Page.GetType, Me.ID & "GetGlobalJSVar", GetGlobalJSVar, True)
        Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "FailureClassification", Page.ResolveClientUrl("~/RI/User Controls/ucFailureClassification.js"))
    End Sub
End Class
