
Partial Class ucEventClassification
    Inherits System.Web.UI.UserControl

    Dim Type As String
    Dim Cause As String
    Dim Process As String
    Dim Component As String

    Public Event IncidentClassificationChanged()
    Private mCascadingList As Boolean = False
    Public Property CascadingLists() As Boolean
        Get
            Return mCascadingList
        End Get
        Set(ByVal value As Boolean)
            mCascadingList = value
        End Set
    End Property

    Private mAutoPostBack As Boolean
    Public Property AutoPostBack() As Boolean
        Get
            Return mAutoPostBack
        End Get
        Set(ByVal value As Boolean)
            mAutoPostBack = value
        End Set
    End Property
    Private mSelectedType As String = String.Empty
    Public Property SelectedType() As String
        Get
            Return mSelectedType
        End Get
        Set(ByVal value As String)
            mSelectedType = value
        End Set
    End Property
    Private mSelectedProcess As String = String.Empty
    Public Property SelectedProcess() As String
        Get
            Return mSelectedProcess
        End Get
        Set(ByVal value As String)
            mSelectedProcess = value
        End Set
    End Property

    Public Property TypeValue() As String
        Get
            Return Me._ddlType.SelectedValue
        End Get
        Set(ByVal value As String)
            If _ddlType.Items.FindByValue(value) IsNot Nothing Then
                _ddlType.Items.FindByValue(value).Selected = True
            End If
            Me._cddlTypes.SelectedValue = value
        End Set
    End Property

    Public Property CauseValue() As String
        Get
            Return Me._ddlCause.SelectedValue
        End Get
        Set(ByVal value As String)
            If _ddlCause.Items.FindByValue(value) IsNot Nothing Then
                _ddlCause.Items.FindByValue(value).Selected = True
            End If
            Me._ccdlReasons.SelectedValue = value
        End Set
    End Property

    Public Property ComponentValue() As String
        Get
            Return Me._ddlComponent.SelectedValue
        End Get
        Set(ByVal value As String)
            If _ddlComponent.Items.FindByValue(value) IsNot Nothing Then
                _ddlComponent.Items.FindByValue(value).Selected = True
            End If
            Me._cddlComponent.SelectedValue = value
        End Set
    End Property

    Public Property ProcessValue() As String
        Get
            Return Me._ddlProcess.SelectedValue
        End Get
        Set(ByVal value As String)
            If _ddlProcess.Items.FindByValue(value) IsNot Nothing Then
                _ddlProcess.Items.FindByValue(value).Selected = True
            End If
            Me._cddlProcess.SelectedValue = value
        End Set
    End Property

    Private mTypeData As clsData
    Public Property TypeData() As clsData
        Get
            Return mTypeData
        End Get
        Set(ByVal value As clsData)
            mTypeData = value
        End Set
    End Property

    Private mCauseData As clsData
    Public Property CauseData() As clsData
        Get
            Return mCauseData
        End Get
        Set(ByVal value As clsData)
            mCauseData = value
        End Set
    End Property

    Private mProcessData As clsData
    Public Property ProcessData() As clsData
        Get
            Return mProcessData
        End Get
        Set(ByVal value As clsData)
            mProcessData = value
        End Set
    End Property

    Private mComponentData As clsData
    Public Property ComponentData() As clsData
        Get
            Return mComponentData
        End Get
        Set(ByVal value As clsData)
            mComponentData = value
        End Set
    End Property

    Private mTriggerData As clsData
    Public Property TriggerData() As clsData
        Get
            Return mTriggerData
        End Get
        Set(ByVal value As clsData)
            mTriggerData = value
        End Set
    End Property
    Public Overrides Sub DataBind()
        'Dim selectedCause As String = String.Empty
        'Dim selectedProcess As String = String.Empty
        Dim CauseFilter As String = String.Empty
        Dim ProcessFilter As String = String.Empty

        Me._ccdlReasons.Enabled = False
        Me._cddlComponent.Enabled = False
        Me._cddlProcess.Enabled = False
        Me._cddlTypes.Enabled = False

        If SelectedType Is Nothing Or SelectedType.Length = 0 Then
            If Request(Me._ddlType.UniqueID) IsNot Nothing Then
                SelectedType = Request(Me._ddlType.UniqueID)
            End If
        End If
        If SelectedProcess Is Nothing Or SelectedProcess.Length = 0 Then
            If Request(Me._ddlProcess.UniqueID) IsNot Nothing Then
                SelectedProcess = Request(Me._ddlProcess.UniqueID)
            End If
        End If

        If SelectedType Is Nothing Then SelectedType = String.Empty
        If SelectedType.Length > 0 Then
            CauseFilter = "Cause='" & SelectedType & "'"
        Else
            CauseFilter = String.Empty '"Cause='No Data'"           
        End If

        If SelectedProcess Is Nothing Then SelectedProcess = String.Empty
        If SelectedProcess.Length > 0 And SelectedType.Length > 0 Then
            ProcessFilter = "Process='" & SelectedProcess & "'"
        Else
            ProcessFilter = String.Empty '"Process='No Data'"
            _ddlProcess.SelectedValue = Nothing
            _ddlComponent.SelectedValue = Nothing
            _ddlCause.SelectedValue = Nothing

        End If

        If ProcessFilter.Length > 0 And CauseFilter.Length > 0 Then
            ProcessFilter = ProcessFilter & " and " & CauseFilter
        End If

        If AutoPostBack = False Then
            CauseFilter = String.Empty
            ProcessFilter = String.Empty

        End If
        'MyBase.DataBind()        
        If TypeData IsNot Nothing Then
            RI.SharedFunctions.BindList(_ddlType, TypeData, True, True)
        End If
        If CauseData IsNot Nothing Then
            RI.SharedFunctions.BindList(_ddlCause, CauseData, True, True, CauseFilter, True, True)
        End If
        If ProcessData IsNot Nothing Then
            RI.SharedFunctions.BindList(_ddlProcess, ProcessData, True, True, CauseFilter, True, True)
        End If
        If ComponentData IsNot Nothing Then
            RI.SharedFunctions.BindList(_ddlComponent, ComponentData, True, True, ProcessFilter, True, True)
        End If

        If Me.AutoPostBack = True Then
            If SelectedType.Length = 0 Then
                _ddlProcess.SelectedIndex = 0
                SelectedProcess = String.Empty
                Me._ddlProcess.Enabled = False
            Else
                Me._ddlProcess.Enabled = True
            End If
            If SelectedProcess.Length = 0 Then
                Me._ddlComponent.SelectedIndex = 0
                Me._ddlComponent.Enabled = False
                _ddlCause.SelectedIndex = 0
                Me._ddlCause.Enabled = False
            Else
                Me._ddlComponent.Enabled = True
                Me._ddlProcess.Enabled = True
                Me._ddlCause.Enabled = True
            End If

        End If

    End Sub

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        ' _ddlProcess.ID = Me.ID
        '_ddlType.ID = Me.ID
        '_ddlComponent.ID = Me.ID
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
    End Sub

End Class
