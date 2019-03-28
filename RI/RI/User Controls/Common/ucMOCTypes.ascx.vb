
Partial Class ucMOCTypes
    Inherits System.Web.UI.UserControl

    Public Enum MOCMode
        Search = 0
        Enter = 1
    End Enum

    Private mDisplayMode As MOCMode = MOCMode.Search
    Public Property DisplayMode() As MOCMode
        Get
            Return mDisplayMode
        End Get
        Set(ByVal value As MOCMode)
            mDisplayMode = value
        End Set
    End Property
    Public Property Types() As String
        Get
            If DisplayMode = MOCMode.Search Then
                Return RI.SharedFunctions.GetCheckBoxValues(_cblType)
            Else
                Return _rblType.SelectedValue
            End If
        End Get
        Set(ByVal value As String)
            If DisplayMode = MOCMode.Search Then
                RI.SharedFunctions.SetCheckBoxValues(_cblType, value)
            Else
                If _rblType.Items.FindByValue(value) IsNot Nothing Then
                    _rblType.SelectedValue = value
                End If
            End If
        End Set
    End Property

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        PopulateTypes()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "IncidentTypes") Then Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "IncidentTypes", Page.ResolveClientUrl("~/ri/User Controls/Common/IncidentType.js"))
        '        Dim popupJS As String = "Javascript:displayModalPopUpWindow('{0}','{1}','{2}','{3}','{4}');"
        'displayModalPopUpWindow(url,name,title,w,h)
    End Sub
    Public Sub RefreshDisplay()
        PopulateTypes()
    End Sub
    Private Sub DisplayTypes()

        If Me.DisplayMode = MOCMode.Enter Then

            Me._cblType.Visible = False
            Me._rblType.Visible = True
        Else
            Me._cblType.Visible = True
            Me._rblType.Visible = False
        End If

    End Sub
    
    Private Sub PopulateTypes()
        Dim AllFlag As Boolean
        Dim li As New OrderedDictionary  'Hashtable
        Dim ipLoc As New IP.Bids.Localization.WebLocalization()

        If DisplayMode = MOCMode.Search Then AllFlag = True

        With li
            .Clear()
            .Add("Permanent", "Permanent")
            .Add("Trial/Temporary", "Trial/Temporary")
            '.Add("Temporary", "Temporary")
            .Add("Emergency", "Emergency")
        End With

        If AllFlag = True Then
            li.Insert(0, "All", "All")
            _cblType.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_cblType, li, False, False)
            _cblType.Attributes.Add("onClick", "unCheckNo(this," & li.Count & ");")
            If _cblType.SelectedIndex < 0 Then
                If _cblType.Items.FindByValue("All") IsNot Nothing Then
                    _cblType.Items.FindByValue("All").Selected = True
                End If
            End If
            ipLoc.LocalizeListControl(_cblType)
        Else
            _rblType.RepeatDirection = RepeatDirection.Horizontal
            RI.SharedFunctions.BindList(_rblType, li, False, False)
            '_rblType.Attributes.Add("onClick", "HideDate('" & _rblType.ClientID & "');")
            If _rblType.SelectedIndex < 0 Then
                If _rblType.Items.FindByValue("Permanent") IsNot Nothing Then
                    _rblType.Items.FindByValue("Permanent").Selected = True
                End If
            End If
            ipLoc.LocalizeListControl(_rblType)
        End If

        Me.DisplayTypes()

    End Sub

End Class
