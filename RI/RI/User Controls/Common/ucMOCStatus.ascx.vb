
Partial Class ucMOCStatus
    Inherits System.Web.UI.UserControl

    Public Enum MOCMode
        Search = 0
        Enter = 1
    End Enum

    Public Event StatusDDLChanged(ByVal sender As DropDownList)
    Public Event StatusChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    Private mDisplayMode As MOCMode = MOCMode.Search
    Public Property DisplayMode() As MOCMode
        Get
            Return mDisplayMode
        End Get
        Set(ByVal value As MOCMode)
            mDisplayMode = value
        End Set
    End Property
    Private mAllowPostback As Boolean
    Public Property AllowPostBack() As Boolean
        Get
            Return mAllowPostback
        End Get
        Set(ByVal value As Boolean)
            mAllowPostback = value
        End Set
    End Property
    Public Property Status() As String
        Get
            If DisplayMode = MOCMode.Search Then
                Return RI.SharedFunctions.GetCheckBoxValues(_cblStatus)
            Else
                Return _ddlStatus.SelectedValue
            End If
        End Get
        Set(ByVal value As String)
            If DisplayMode = MOCMode.Search Then
                RI.SharedFunctions.SetCheckBoxValues(_cblStatus, value)
            Else
                If _ddlStatus.Items.FindByValue(value) IsNot Nothing Then
                    _ddlStatus.SelectedValue = value

                End If
                Select Case _ddlStatus.SelectedValue
                    Case "Draft"
                        For i = 0 To _ddlStatus.Items.Count - 1
                            If _ddlStatus.Items(i).Value = "Approval Requested" Or
                                _ddlStatus.Items(i).Value = "On Hold" Or
                                _ddlStatus.Items(i).Value = "Draft" Then
                                _ddlStatus.Items(i).Attributes.Remove("disabled")
                            End If
                        Next
                    Case "On Hold"
                        For i = 0 To _ddlStatus.Items.Count - 1
                            If _ddlStatus.Items(i).Value = "Remove From Hold" Or
                                _ddlStatus.Items(i).Value = "On Hold" Then
                                _ddlStatus.Items(i).Attributes.Remove("disabled")
                            End If
                        Next
                    Case "Approval Requested", "Approved", "No Approvers", "Informed Only"
                        For i = 0 To _ddlStatus.Items.Count - 1
                            If _ddlStatus.Items(i).Value = "On Hold" Or
                                _ddlStatus.Items(i).Value = "Implemented" Then
                                _ddlStatus.Items(i).Attributes.Remove("disabled")
                            End If
                        Next
                End Select
                RaiseEvent StatusChanged(Me._ddlStatus, Nothing)

            End If
        End Set
    End Property

    Private mInfo As String
    Public Property Info() As String
        Get
            Return mInfo
        End Get
        Set(ByVal value As String)
            Me._lbInfo.Text = value
        End Set
    End Property

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        PopulateStatus()
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim popupJS As String = "Javascript:displayModalPopUpWindow('{0}','{1}','{2}','{3}','{4}');"
        Dim ipLoc As New IP.Bids.Localization.WebLocalization()
        Me._hypStatusDefinition.NavigateUrl = "javascript:var x=dhtmlmodal.open('StatusDefinition', 'div', '_divStatusDefinition', '" & ipLoc.GetResourceValue("Definition", False, "Shared") & "', 'width=800px,height=300px,center=1,resize=0,scrolling=1');"
    End Sub
    Public Sub RefreshDisplay()
        'PopulateStatus()
    End Sub
    Private Sub DisplayStatus()
        If Me.DisplayMode = MOCMode.Enter Then
            Me._cblStatus.Visible = False
            Me._ddlStatus.Visible = True
        Else
            Me._cblStatus.Visible = True
            Me._ddlStatus.Visible = False
        End If

    End Sub

    Private Sub PopulateStatus()
        Dim AllFlag As Boolean
        Dim li As New OrderedDictionary  'Hashtable
        Dim ipLoc As New IP.Bids.Localization.WebLocalization()

        If DisplayMode = MOCMode.Search Then AllFlag = True

        With li
            .Clear()
            .Add("Draft", "Draft")
            .Add("On Hold", "On Hold")
            .Add("Not Approved", "Not Approved")
            .Add("No Approvers", "No Approvers")
            .Add("Approval Requested", "Approval Requested")
            .Add("Approved", "Approved")
            .Add("Implemented", "Implemented")
            .Add("Completed", "Completed")
        End With

        If AllFlag = True Then
            li.Insert(0, "All", "All")
            _cblStatus.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_cblStatus, li, False, False)
            _cblStatus.Attributes.Add("onClick", "unCheckNo(this, " & li.Count & ");")
            If _cblStatus.SelectedIndex < 0 Then
                If _cblStatus.Items.FindByValue("All") IsNot Nothing Then
                    _cblStatus.Items.FindByValue("All").Selected = True
                End If
            End If
            ipLoc.LocalizeListControl(_cblStatus)
        Else
            '_rblStatus.RepeatDirection = RepeatDirection.Horizontal
            'RI.SharedFunctions.BindList(_ddlStatus, li, False, False)
            '_rblStatus.Attributes.Add("onClick", "HideDate('" & _rblStatus.ClientID & "');")
            'If _rblStatus.SelectedIndex < 0 Then
            '    If _rblStatus.Items.FindByValue("Permanent") IsNot Nothing Then
            '        _rblStatus.Items.FindByValue("Permanent").Selected = True
            '    End If
            'End If
            ipLoc.LocalizeListControl(_ddlStatus)
        End If

        Me.DisplayStatus()

    End Sub
    Protected Sub _ddlStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlStatus.SelectedIndexChanged
        RaiseEvent StatusChanged(sender, e)
    End Sub

    Private Sub ucMOCStatus_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If Me.Attributes.Item("onChange") IsNot Nothing Then
            Me._ddlStatus.Attributes.Add("onChange", Me.Attributes.Item("onChange"))
        End If
        Me._ddlStatus.AutoPostBack = AllowPostBack

    End Sub

    Private Function GetStatusJSMOC() As String
        Dim sb As New StringBuilder

        sb.Append("var ddlStatus = $get('")
        sb.Append(Me._ddlStatus.ClientID)
        sb.Append("');")

        sb.AppendLine()
        sb.Append("var cblStatus= $get('")
        sb.Append(Me._cblStatus.ClientID)
        sb.Append("');")

        Return sb.ToString
    End Function
End Class
