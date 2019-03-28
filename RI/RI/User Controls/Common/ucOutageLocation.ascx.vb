
Partial Class ucOutageLocation
    Inherits System.Web.UI.UserControl

    Event LocationChanged()

    Private mDivisionAutoPostBack As Boolean
    Public Property DivisionAutoPostBack() As Boolean
        Get
            Return mDivisionAutoPostBack
        End Get
        Set(ByVal value As Boolean)
            mDivisionAutoPostBack = value
        End Set
    End Property

    Private mFacilityAutoPostBack As Boolean
    Public Property FacilityAutoPostBack() As Boolean
        Get
            Return mFacilityAutoPostBack
        End Get
        Set(ByVal value As Boolean)
            mFacilityAutoPostBack = value
        End Set
    End Property

    Private mBusinessUnitAutoPostBack As Boolean
    Public Property BusinessUnitAutoPostBack() As Boolean
        Get
            Return mBusinessUnitAutoPostBack
        End Get
        Set(ByVal value As Boolean)
            mBusinessUnitAutoPostBack = value
        End Set
    End Property

    Private mDivision As clsData
    Public Property Division() As clsData
        Get
            Return mDivision
        End Get
        Set(ByVal value As clsData)
            mDivision = value
        End Set
    End Property

    Private mFacility As clsData
    Public Property Facility() As clsData
        Get
            Return mFacility
        End Get
        Set(ByVal value As clsData)
            mFacility = value
        End Set
    End Property

    Private mBusinessUnit As clsData
    Public Property BusinessUnit() As clsData
        Get
            Return mBusinessUnit
        End Get
        Set(ByVal value As clsData)
            mBusinessUnit = value
        End Set
    End Property

    Private mArea As clsData
    Public Property Area() As clsData
        Get
            Return mArea
        End Get
        Set(ByVal value As clsData)
            mArea = value
        End Set
    End Property

    Private mLine As clsData
    Public Property Line() As clsData
        Get
            Return mLine
        End Get
        Set(ByVal value As clsData)
            mLine = value
        End Set
    End Property

    'Private mLineBreak As clsData
    'Public Property LineBreak() As clsData
    '    Get
    '        Return mLineBreak
    '    End Get
    '    Set(ByVal value As clsData)
    '        mLineBreak = value
    '    End Set
    'End Property

    Private mDivisionValue As String
    Public Property DivisionValue() As String
        Get
            Return Me._ddlDivision.SelectedValue
        End Get
        Set(ByVal value As String)
            Me._ddlDivision.SelectedValue = value
        End Set
    End Property

    Private mFacilityValue As String
    Public Property FacilityValue() As String
        Get
            Return Me._ddlFacility.SelectedValue
        End Get
        Set(ByVal value As String)
            Me._ddlFacility.SelectedValue = value
        End Set
    End Property

    Private mBusinessUnitValue As String
    Public Property BusinessUnitValue() As String
        Get
            Return Me._ddlBusinessUnit.SelectedValue
        End Get
        Set(ByVal value As String)
            Me._ddlBusinessUnit.SelectedValue = value
        End Set
    End Property

    Private mAreaValue As String
    Public Property AreaValue() As String
        Get
            Return _ddlArea.SelectedValue
        End Get
        Set(ByVal value As String)
            _ddlArea.SelectedValue = value
        End Set
    End Property

    Private mLineValue As String
    Public Property LineValue() As String
        Get
            Return _ddlLine.SelectedValue
        End Get
        Set(ByVal value As String)
            _ddlLine.SelectedValue = value
        End Set
    End Property

    'Private mLineBreakValue As String
    'Public Property LineBreakValue() As String
    '    Get
    '        Return _ddlLineBreak.SelectedValue
    '    End Get
    '    Set(ByVal value As String)
    '        _ddlLineBreak.SelectedValue = value
    '    End Set
    'End Property

    Public Overrides Sub DataBind()
        'MyBase.DataBind()
        If Division IsNot Nothing Then
            RI.SharedFunctions.BindList(_ddlDivision, Division, True, True)
        End If
        If Facility IsNot Nothing Then
            RI.SharedFunctions.BindList(_ddlFacility, Facility, True, True)
        End If
        If BusinessUnit IsNot Nothing Then
            RI.SharedFunctions.BindList(_ddlBusinessUnit, BusinessUnit, True, True)
        End If
        If Area IsNot Nothing Then
            RI.SharedFunctions.BindList(_ddlArea, Area, True, True)
        End If
        If Line IsNot Nothing Then
            RI.SharedFunctions.BindList(_ddlLine, Line, True, True)
        End If
        'If LineBreak IsNot Nothing Then
        '    RI.SharedFunctions.BindList(_ddlLineBreak, LineBreak, True, True)
        'End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _ddlDivision.AutoPostBack = Me.DivisionAutoPostBack
        _ddlFacility.AutoPostBack = Me.FacilityAutoPostBack
        _ddlBusinessUnit.AutoPostBack = Me.BusinessUnitAutoPostBack
    End Sub

    Protected Sub _ddlDivision_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlDivision.SelectedIndexChanged
        RaiseEvent LocationChanged()
    End Sub

    Protected Sub _ddlFacility_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlFacility.SelectedIndexChanged
        RaiseEvent LocationChanged()
    End Sub

    Protected Sub _ddlBusinessUnit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlBusinessUnit.SelectedIndexChanged
        RaiseEvent LocationChanged()
    End Sub
End Class
