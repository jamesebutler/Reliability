Partial Class ucMTTResponsible
    Inherits System.Web.UI.UserControl

    Event LocationChanged()
    Public Event UserChanged(ByVal sender As Object, ByVal args As EventArgs)

    Private mAutoPostBack As Boolean
    Public Property AutoPostBack() As Boolean
        Get
            Return mAutoPostBack
        End Get
        Set(ByVal value As Boolean)
            mAutoPostBack = value
        End Set
    End Property

    Private mFacilityValue As String = String.Empty
    Public Property FacilityValue() As String
        Get
            If mFacilityValue.ToUpper = "ALL" Then mFacilityValue = String.Empty
            Return mFacilityValue 'Me._ddlFacility.SelectedValue
        End Get
        Set(ByVal value As String)
            Me._cddlMOCFacility.SelectedValue = value
            mFacilityValue = value
        End Set
    End Property

    Private mFacilityName As String = String.Empty
    Public ReadOnly Property FacilityName() As String
        Get
            If mFacilityName.Length = 0 Then
                If _ddlFacility.Items.FindByValue(FacilityValue) IsNot Nothing Then
                    mFacilityName = _ddlFacility.Items.FindByValue(FacilityValue).Text
                End If
            End If
            'If Me._ddlFacility.SelectedItem IsNot Nothing Then
            Return mFacilityName 'Me._ddlFacility.SelectedItem.Text
            'Else
            'Return ""
            'End If
        End Get
    End Property
    Private mResponsibleValue As String = String.Empty
    Public Property ResponsibleValue() As String
        Get
            If mResponsibleValue.ToUpper = "ALL" Then mResponsibleValue = String.Empty
            Return mResponsibleValue
        End Get
        Set(ByVal value As String)
            Me._cddlMOCResponsible.SelectedValue = value
            mResponsibleValue = value
        End Set
    End Property

    Private RIResources As New IP.Bids.Localization.WebLocalization
     Private Sub SetDDLValues()
        If Request.Form(_ddlFacility.UniqueID) IsNot Nothing Then
            mFacilityValue = Request.Form(_ddlFacility.UniqueID)
        End If
        If _ddlFacility.Items.FindByValue(mFacilityValue) IsNot Nothing Then
            mFacilityName = _ddlFacility.Items.FindByValue(mFacilityValue).Text
        End If
        'If _ddlFacility.SelectedItem IsNot Nothing Then mFacilityName = Me._ddlFacility.SelectedItem.Text
        If Request.Form(_ddlResponsible.UniqueID) IsNot Nothing Then
            mResponsibleValue = Request.Form(_ddlResponsible.UniqueID)
        End If
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        SetDDLValues()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sc As ScriptManager
        sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
        If sc IsNot Nothing Then
            Dim loService As New ServiceReference
            loService.InlineScript = True
            loService.Path = "~/CascadingLists.asmx"
            sc.Services.Add(loService)
        End If
        '      DisplayDropDowns()
        'If Not Page.IsPostBack Then
        'LayoutTable()
        'End If
        If AutoPostBack = True Then
            Me._ddlFacility.AutoPostBack = True
        Else
            Me._ddlFacility.AutoPostBack = False
        End If
    End Sub
    Private Sub Hide(ByRef obj As WebControl)
        If obj IsNot Nothing Then
            obj.Style.Add("Display", "none")
            'obj.Visible = False
        End If
    End Sub
    Private Sub AddCascadidingDropDown(ByVal cddlID As String, ByVal category As String, ByVal loadingText As String, ByVal promptText As String, ByVal serviceMethod As String, ByVal servicePath As String, ByVal targetControlID As String, ByVal contextKey As String)
        Dim cddl As New AjaxControlToolkit.CascadingDropDown
        cddl.ID = ID
        cddl.Category = category
        cddl.LoadingText = loadingText
        cddl.PromptText = promptText
        cddl.ServiceMethod = serviceMethod
        cddl.ServicePath = servicePath
        cddl.TargetControlID = targetControlID
        cddl.ContextKey = contextKey
        If contextKey.Length > 0 Then cddl.UseContextKey = True
        If cddl IsNot Nothing Then Me.Page.Controls.Add(cddl)
    End Sub

    Protected Sub _ddlFacility_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlFacility.SelectedIndexChanged
        RaiseEvent LocationChanged()
    End Sub

    Protected Sub _ddlResponsible_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlResponsible.SelectedIndexChanged
        'If Me.AutoPostBack Then
        RaiseEvent UserChanged(sender, e)
        'End If
    End Sub
End Class