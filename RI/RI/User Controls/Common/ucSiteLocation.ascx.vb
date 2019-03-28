Partial Class ucSiteLocation
    Inherits System.Web.UI.UserControl

    Event LocationChanged()

    Private mAutoPostBack As Boolean
    Public Property AutoPostBack() As Boolean
        Get
            Return mAutoPostBack
        End Get
        Set(ByVal value As Boolean)
            mAutoPostBack = value
        End Set
    End Property
    Private mDisplayAsSingleRow As Boolean
    Public Property DisplayAsSingleRow() As Boolean
        Get
            Return mDisplayAsSingleRow
        End Get
        Set(ByVal value As Boolean)
            mDisplayAsSingleRow = value
        End Set
    End Property
    Private mHideDivision As Boolean
    Public Property HideDivision() As Boolean
        Get
            Return mHideDivision
        End Get
        Set(ByVal value As Boolean)
            mHideDivision = value
        End Set
    End Property
    Private mHideBusinessUnit As Boolean
    Public Property HideBusinessUnit() As Boolean
        Get
            Return mHideBusinessUnit
        End Get
        Set(ByVal value As Boolean)
            mHideBusinessUnit = value
        End Set
    End Property

    Private mHideArea As Boolean
    Public Property HideArea() As Boolean
        Get
            Return mHideArea
        End Get
        Set(ByVal value As Boolean)
            mHideArea = value
        End Set
    End Property

    Private mHideLine As Boolean
    Public Property HideLine() As Boolean
        Get
            Return mHideLine
        End Get
        Set(ByVal value As Boolean)
            mHideLine = value
        End Set
    End Property

    Private mHideLineBreak As Boolean
    Public Property HideLineBreak() As Boolean
        Get
            Return mHideLineBreak
        End Get
        Set(ByVal value As Boolean)
            mHideLineBreak = value
        End Set
    End Property
    Private mDivisionValue As String = String.Empty
    Public Property DivisionValue() As String
        Get
            If mDivisionValue.ToUpper = "ALL" Then mDivisionValue = String.Empty
            Return mDivisionValue 'Me._ddlDivision.SelectedValue
        End Get
        Set(ByVal value As String)
            Me._cddlDivision.SelectedValue = value
            mDivisionValue = value
        End Set
    End Property

    Private mFacilityValue As String = String.Empty
    Public Property FacilityValue() As String
        Get
            If mFacilityValue.ToUpper = "ALL" Then mFacilityValue = String.Empty
            Return mFacilityValue 'Me._ddlFacility.SelectedValue
        End Get
        Set(ByVal value As String)
            Me._cddlFacility.SelectedValue = value
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
    Private mBusinessUnitValue As String = String.Empty
    Public Property BusinessUnitValue() As String
        Get
            If mBusinessUnitValue.ToUpper = "ALL" Then mBusinessUnitValue = String.Empty
            Return mBusinessUnitValue 'Me._ddlBusinessUnit.SelectedValue
        End Get
        Set(ByVal value As String)
            Me._cddlBusinessUnit.SelectedValue = value
            mBusinessUnitValue = value
        End Set
    End Property

    Private mAreaValue As String = String.Empty
    Public Property AreaValue() As String
        Get
            If mAreaValue.ToUpper = "ALL" Then mAreaValue = String.Empty
            Return mAreaValue '_ddlArea.SelectedValue
        End Get
        Set(ByVal value As String)
            _cddlArea.SelectedValue = value
            mAreaValue = value
        End Set
    End Property

    Private mLineValue As String = String.Empty
    Public Property LineValue() As String
        Get
            If mLineValue.ToUpper = "ALL" Then mLineValue = String.Empty
            Return mLineValue '_ddlLine.SelectedValue
        End Get
        Set(ByVal value As String)
            _cddlLine.SelectedValue = value
            mLineValue = value
        End Set
    End Property

    Private mLineBreakValue As String = String.Empty
    Public Property LineBreakValue() As String
        Get
            If mLineBreakValue.ToUpper = "ALL" Then mLineBreakValue = String.Empty
            Return mLineBreakValue '_ddlLineBreak.SelectedValue
        End Get
        Set(ByVal value As String)
            _cddlLineBreak.SelectedValue = value
            mLineBreakValue = value
        End Set
    End Property

    Private RIResources As New IP.Bids.Localization.WebLocalization
    Public Overrides Sub DataBind()
        ''MyBase.DataBind()
        'If Division IsNot Nothing Then
        '    RI.SharedFunctions.BindList(_ddlDivision, Division, True, True)
        'End If
        'If Facility IsNot Nothing Then
        '    RI.SharedFunctions.BindList(_ddlFacility, Facility, True, True)
        'End If
        'If BusinessUnit IsNot Nothing Then
        '    RI.SharedFunctions.BindList(_ddlBusinessUnit, BusinessUnit, True, True)
        'End If
        'If Area IsNot Nothing Then
        '    RI.SharedFunctions.BindList(_ddlArea, Area, True, True)
        'End If
        'If Line IsNot Nothing Then
        '    RI.SharedFunctions.BindList(_ddlLine, Line, True, True)
        'End If
        'If LineBreak IsNot Nothing Then
        '    RI.SharedFunctions.BindList(_ddlLineBreak, LineBreak, True, True)
        'End If
    End Sub
    Private Sub SetDivisionJS()
        Dim sb As New StringBuilder
        Dim js As String = String.Empty
        Dim divID As String = _ddlDivision.ClientID

        sb.AppendLine("function SetDivision_{0}(){")
        sb.AppendLine("var site = $get('{1}');") '<%=_ddlFacility.clientid %>');
        sb.AppendLine("var div = $get('{0}');") '<%=_ddlDivision.clientid %>');
        sb.AppendLine("if (div!=null){div.selectedIndex;}")
        sb.AppendLine("ret = CascadingLists.GetSiteDivision(site.value,OnSetDivisionComplete_{0}, OnSetDivisionTimeOut, OnSetDivisionError);")
        sb.AppendLine("return(true);}")
        js = sb.ToString
        js = js.Replace("{0}", divID)
        js = js.Replace("{1}", Me._ddlFacility.ClientID)
        'js = String.Format(js, Me._ddlDivision.ClientID, Me._ddlFacility.ClientID)
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType, "SetDivision_" & divID, js, True)
        'Me._ddlFacility.Attributes.Add("onchange", "SetDivision_" & divID & "();")

        sb.Length = 0
        sb.AppendLine("function OnSetDivisionComplete_{0}(arg) {")
        sb.AppendLine("var behaviors =  Sys.UI.Behavior.getBehaviors($get('{0}'));arg=arg.trim();")
        sb.AppendLine("for(var i=0;i<behaviors.length;i++) {")
        sb.AppendLine("var behave = behaviors[i]; ")
        sb.AppendLine("if(behave._parentControlID == '{1}')")
        sb.AppendLine("{behave._selectedValue=arg.trim();}}}")

        'sb.AppendLine("function SetDefaultDivison(value){")
        'sb.AppendLine("var div = $get('{0}');var selIndex=-1;")
        'sb.AppendLine("if (div !=null){")
        'sb.AppendLine("  for(var i=0;i<div.options.length;i++){if (div.options[i].value==value){div.options.selectedIndex=i}}}")
        'sb.AppendLine("}")
        js = sb.ToString
        js = js.Replace("{0}", divID)
        js = js.Replace("{1}", Me._ddlFacility.ClientID)
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType, "SetDivisionComplete" & divID, js, True)

        sb.Length = 0
        sb.AppendLine("function OnSetDivisionTimeOut(arg) {alert('TimeOut OnSecurityTimeOut');}")
        sb.AppendLine("function OnSetDivisionError(arg) {alert('Error encountered OnSecurityError');}")
        js = sb.ToString
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType, "SetDivisionTimeOut", js, True)




    End Sub
    Private Sub SetDDLValues()
        If Request.Form(_ddlFacility.UniqueID) IsNot Nothing Then
            mFacilityValue = Request.Form(_ddlFacility.UniqueID)
        End If
        If _ddlFacility.Items.FindByValue(mFacilityValue) IsNot Nothing Then
            mFacilityName = _ddlFacility.Items.FindByValue(mFacilityValue).Text
        End If
        'If _ddlFacility.SelectedItem IsNot Nothing Then mFacilityName = Me._ddlFacility.SelectedItem.Text
        If Request.Form(_ddlBusinessUnit.UniqueID) IsNot Nothing Then
            mBusinessUnitValue = Request.Form(_ddlBusinessUnit.UniqueID)
        End If
        If Request.Form(_ddlDivision.UniqueID) IsNot Nothing Then
            mDivisionValue = Request.Form(_ddlDivision.UniqueID)
        End If
        If Request.Form(_ddlArea.UniqueID) IsNot Nothing Then
            mAreaValue = Request.Form(_ddlArea.UniqueID)
        End If
        If Request.Form(_ddlLine.UniqueID) IsNot Nothing Then
            mLineValue = Request.Form(_ddlLine.UniqueID)
        End If
        If Request.Form(_ddlLineBreak.UniqueID) IsNot Nothing Then
            mLineBreakValue = Request.Form(_ddlLineBreak.UniqueID)
        End If
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        SetDDLValues()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '_ddlDivision.AutoPostBack = Me.DivisionAutoPostBack
        '_ddlFacility.AutoPostBack = Me.FacilityAutoPostBack
        '_ddlBusinessUnit.AutoPostBack = Me.BusinessUnitAutoPostBack

        Dim sc As ScriptManager
        sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
        If sc IsNot Nothing Then
            Dim loService As New ServiceReference
            loService.InlineScript = True
            loService.Path = "~/CascadingLists.asmx"
            sc.Services.Add(loService)
        End If
        DisplayDropDowns()
        'If Not Page.IsPostBack Then
        LayoutTable()
        'End If
        If HideDivision = False Then
            SetDivisionJS()
        Else
            _cddlFacility.ParentControlID = ""
        End If
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
        cddl.Category = Category
        cddl.LoadingText = LoadingText
        cddl.PromptText = PromptText
        cddl.ServiceMethod = ServiceMethod
        cddl.ServicePath = ServicePath
        cddl.TargetControlID = targetControlID
        cddl.ContextKey = contextKey
        If contextKey.Length > 0 Then cddl.UseContextKey = True
        If cddl IsNot Nothing Then Me.Page.Controls.Add(cddl)
    End Sub
    Private Sub DisplayDropDowns()

        'Me._lblFacility.Text = RIResources.GetResourceValue("Facility", True, "Shared")
        If HideLineBreak = True Then
            Hide(_ddlLineBreak)
            Me._lblLineBreak.Visible = False
            Me._cddlLineBreak.Enabled = False
        Else
            Me._ddlLineBreak.Visible = True
            Me._lblLineBreak.Visible = True
            'Me._lblLineBreak.Text = RIResources.GetResourceValue("LineBreak", True, "Shared")
        End If
        If HideLine = True Then
            Hide(_ddlLine)
            Me._lblLine.Visible = False
            Me._cddlLine.Enabled = False
            'Me._cddlLine.TargetControlID = Nothing
            'disable child cascading ddls
            Me._cddlLineBreak.Enabled = False
            'Me._cddlLineBreak.TargetControlID = Nothing
        Else
            Me._ddlLine.Visible = True
            Me._lblLine.Visible = True
            'Me._lblLine.Text = RIResources.GetResourceValue("Line", True, "Shared")
        End If
        If HideArea = True Then
            Hide(_ddlArea)
            Me._lblArea.Visible = False
            Me._cddlArea.Enabled = False
            'disable child cascading ddl
            Me._cddlLine.Enabled = False
            Me._cddlLineBreak.Enabled = False

        Else
            Me._ddlArea.Visible = True
            Me._lblArea.Visible = True
            'Me._lblArea.Text = RIResources.GetResourceValue("Area", True, "Shared")
        End If
        If HideBusinessUnit = True Then
            Hide(_ddlBusinessUnit)
            Me._lblBusinessUnit.Visible = False
            Me._cddlBusinessUnit.Enabled = False
            'disable child cascading ddls
            Me._cddlArea.Enabled = False
            Me._cddlLine.Enabled = False
            Me._cddlLineBreak.Enabled = False

        Else
            Me._ddlBusinessUnit.Visible = True
            Me._lblBusinessUnit.Visible = True
            'Me._lblBusinessUnit.Text = RIResources.GetResourceValue("BusinessUnit", True, "Shared")
        End If

        If HideDivision = True Then
            Hide(_ddlDivision)
            Me._lblDivision.Visible = False
            Me._cddlDivision.Enabled = False
        Else
            Me._ddlDivision.Visible = True
            Me._lblDivision.Visible = True
            'Me._lblDivision.Text = RIResources.GetResourceValue("Division", True, "Shared")
        End If




    End Sub
  
    Private Sub LayoutTable()
        Dim tbl As Table = Me._tblSite
        Dim rowCount As Integer = 0
        Dim columnCount As Integer = 6
        Dim columnWidth As Integer = 0
        Dim currentCell As Integer = 0
        Dim currentRow As Integer = 1
        Dim displayHeaderOnSecondLine As Boolean = False

        'Dim Fac As String = _ddlFacility.SelectedValue
        'Dim div As String = _ddlDivision.SelectedValue
        'Dim bus As String = _ddlBusinessUnit.SelectedValue
        'Dim area As String = _ddlArea.SelectedValue
        'Dim line As String = _ddlLine.SelectedValue
        'Dim linebreak As String = _ddlLineBreak.SelectedValue

        'Clear existing rows
        'tbl.Rows.Clear()

        'Determine the number of columns and rows
        If Me.HideDivision = True Then columnCount = columnCount - 1
        If Me.HideBusinessUnit = True Then columnCount = columnCount - 1
        If Me.HideArea = True Then columnCount = columnCount - 1
        If Me.HideLine = True Then columnCount = columnCount - 1
        If Me.HideLineBreak = True Then columnCount = columnCount - 1

        'If columnCount <= 3 Then Me.DisplayAsSingleRow = True

        If Me.DisplayAsSingleRow = True Then
            displayHeaderOnSecondLine = True
            rowCount = 1
            Dim tr As New TableRow
            tr.CssClass = "Border"
            'tbl.Rows(0).CssClass = "Border"
            For i As Integer = 0 To columnCount - 1
                tr.Cells.Add(New TableCell)
            Next
            tbl.Rows.Add(tr)
        Else
            rowCount = 2
            columnCount = Math.Ceiling(columnCount / 2)
            Dim tr As New TableRow
            Dim tr2 As New TableRow
            tr.CssClass = "Border"
            tr2.CssClass = "Border"
            For i As Integer = 0 To columnCount - 1
                tr.Cells.Add(New TableCell)
                tr2.Cells.Add(New TableCell)
            Next
            tbl.Rows.Add(tr)
            tbl.Rows.Add(tr2)
        End If
        columnWidth = Math.Ceiling(100 / columnCount)

        If HideDivision = False Then
            tbl.Rows(currentRow).Cells(currentCell).Controls.Add(Me._lblDivision)
            If displayHeaderOnSecondLine Then tbl.Rows(currentRow).Cells(currentCell).Controls.Add(New LiteralControl("<br/>"))
            tbl.Rows(currentRow).Cells(currentCell).Controls.Add(Me._ddlDivision)
            tbl.Rows(currentRow).Cells(currentCell).Width = Unit.Percentage(25)
            calculateCurrentRowandCell(currentCell, currentRow, columnCount)
        End If

        tbl.Rows(currentRow).Cells(currentCell).Controls.Add(Me._lblFacility)
        If displayHeaderOnSecondLine Then tbl.Rows(currentRow).Cells(currentCell).Controls.Add(New LiteralControl("<br/>"))
        tbl.Rows(currentRow).Cells(currentCell).Controls.Add(Me._ddlFacility)
        tbl.Rows(currentRow).Cells(currentCell).Width = Unit.Percentage(35)
        calculateCurrentRowandCell(currentCell, currentRow, columnCount)

        If HideBusinessUnit = False Then
            tbl.Rows(currentRow).Cells(currentCell).Controls.Add(Me._lblBusinessUnit)
            If displayHeaderOnSecondLine Then tbl.Rows(currentRow).Cells(currentCell).Controls.Add(New LiteralControl("<br/>"))
            tbl.Rows(currentRow).Cells(currentCell).Controls.Add(Me._ddlBusinessUnit)
            tbl.Rows(currentRow).Cells(currentCell).Width = Unit.Percentage(40)
            calculateCurrentRowandCell(currentCell, currentRow, columnCount)
        End If

        If HideArea = False Then
            tbl.Rows(currentRow).Cells(currentCell).Controls.Add(Me._lblArea)
            If displayHeaderOnSecondLine Then tbl.Rows(currentRow).Cells(currentCell).Controls.Add(New LiteralControl("<br/>"))
            tbl.Rows(currentRow).Cells(currentCell).Controls.Add(Me._ddlArea)
            'tbl.Rows(currentRow).Cells(currentCell).Width = Unit.Percentage(columnWidth)
            calculateCurrentRowandCell(currentCell, currentRow, columnCount)
        End If

        If HideLine = False Then
            tbl.Rows(currentRow).Cells(currentCell).Controls.Add(Me._lblLine)
            If displayHeaderOnSecondLine Then tbl.Rows(currentRow).Cells(currentCell).Controls.Add(New LiteralControl("<br/>"))
            tbl.Rows(currentRow).Cells(currentCell).Controls.Add(Me._ddlLine)
            'tbl.Rows(currentRow).Cells(currentCell).Width = Unit.Percentage(columnWidth)
            calculateCurrentRowandCell(currentCell, currentRow, columnCount)
        End If

        If HideLineBreak = False Then
            tbl.Rows(currentRow).Cells(currentCell).Controls.Add(Me._lblLineBreak)
            If displayHeaderOnSecondLine Then tbl.Rows(currentRow).Cells(currentCell).Controls.Add(New LiteralControl("<br/>"))
            tbl.Rows(currentRow).Cells(currentCell).Controls.Add(Me._ddlLineBreak)
            'tbl.Rows(currentRow).Cells(currentCell).Width = Unit.Percentage(columnWidth)
            'calculateCurrentRowandCell(currentCell, currentRow, columnCount)
        End If
        tbl.Rows(0).Visible = False
        '_cddlFacility.SelectedValue = Fac
        '_cddlDivision.SelectedValue = div
        '_cddlBusinessUnit.SelectedValue = bus
        '_cddlArea.SelectedValue = area
        '_cddlLine.SelectedValue = line
        '_cddlLineBreak.SelectedValue = linebreak
    End Sub
    Private Sub calculateCurrentRowandCell(ByRef currentCell As Integer, ByRef currentRow As Integer, ByVal columnCount As Integer)
        If currentCell + 1 < columnCount Then
            currentCell = currentCell + 1
        Else
            currentCell = 0
            currentRow = currentRow + 1
        End If
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