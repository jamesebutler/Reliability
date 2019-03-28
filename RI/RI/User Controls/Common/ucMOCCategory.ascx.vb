Imports Devart.Data.Oracle

Partial Class ucMOCCategory
    Inherits System.Web.UI.UserControl

    'Protected WithEvents chkBoxLst As System.Web.UI.WebControls.CheckBox
    'Protected WithEvents chkBoxList2 As System.Web.UI.WebControls.CheckBoxList
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
    Public Property Category() As String
        Get
            Dim i As Integer
            Dim strCat As String = Nothing

            For i = 0 To parentRepeater.Items.Count - 1
                Dim cbl As CheckBox = Me.parentRepeater.Items(i).FindControl("_cbCategory")
                If cbl.Checked = True Then
                    strCat = strCat & cbl.Text & ","
                End If
            Next
            If strCat <> "" Then
                strCat = Mid(strCat, 1, strCat.Length - 1)
                'strCat = RI.SharedFunctions.LocalizeValue(strCat)
            End If
            Return strCat
        End Get
        Set(ByVal value As String)
            Dim i As Integer
            If DisplayMode = MOCMode.Search Then
                For i = 0 To parentRepeater.Items.Count - 1
                    Dim list As String() = value.Split(CChar(","))
                    If list.Length > 0 Then
                        For j As Integer = 0 To list.Length - 1

                            Dim cbl As CheckBox = Me.parentRepeater.Items(i).FindControl("_cbCategory")
                            If cbl.Text = list(j) Then
                                cbl.Checked = True
                            End If
                        Next
                    End If
                Next
            End If

        End Set

    End Property
    Public Property SubCategory() As String
        Get
            Dim i As Integer
            Dim strCat As String = Nothing
            Dim strCatFinal As String = Nothing

            For i = 0 To parentRepeater.Items.Count - 1
                Dim cbl As CheckBoxList = Me.parentRepeater.Items(i).FindControl("_cblSubCategory")
                strCat = RI.SharedFunctions.GetCheckBoxValues(cbl)
                If strCat <> "" Then
                    strCatFinal = strCatFinal & strCat & ","
                End If
                'Dim ddl As DropDownList = Me.parentRepeater.Items(i).FindControl("_ddlSubCategory")
                'strCat = ddl.SelectedValue
                'If strCat <> "" Then
                'strCatFinal = strCatFinal & strCat & ","
                'End If
            Next
            Return strCatFinal
            'Return strCat
        End Get
        Set(ByVal value As String)
            Dim i As Integer
            For i = 0 To parentRepeater.Items.Count - 1
                Dim cbl As CheckBoxList = Me.parentRepeater.Items(i).FindControl("_cblSubCategory")
                RI.SharedFunctions.SetCheckBoxValues(cbl, value)
            Next
        End Set

    End Property
    Public Property EquipSubCategory() As String
        Get
            Dim i As Integer
            Dim strEquipCat As String = Nothing
            Dim strCatFinal As String = Nothing

            For i = 0 To parentRepeater.Items.Count - 1
                Dim cbl As CheckBox = Me.parentRepeater.Items(i).FindControl("_cbCategory")
                If cbl.Checked = True And cbl.Text = "Equipment" Then
                    Dim ddl As DropDownList = Me.parentRepeater.Items(i).FindControl("_ddlSubCategory")
                    strEquipCat = strEquipCat & ddl.SelectedValue
                End If
            Next

            Return strEquipCat
        End Get

        Set(ByVal value As String)
            Dim i As Integer
            For i = 0 To parentRepeater.Items.Count - 1
                Dim cbl As CheckBox = Me.parentRepeater.Items(i).FindControl("_cbCategory")
                If cbl.Checked = True And cbl.Text = "Equipment" Then
                    Dim ddl As DropDownList = Me.parentRepeater.Items(i).FindControl("_ddlSubCategory")
                    ddl.SelectedValue = value
                End If
            Next
        End Set

    End Property

    Public Property MarketChannelSubCategory() As String
        Get
            Dim i As Integer
            Dim strEquipCat As String = Nothing
            Dim strCatFinal As String = Nothing

            For i = 0 To parentRepeater.Items.Count - 1
                Dim cbl As CheckBox = Me.parentRepeater.Items(i).FindControl("_cbCategory")
                If cbl.Checked = True And cbl.Text = "Market Channel" Then
                    Dim ddl As DropDownList = Me.parentRepeater.Items(i).FindControl("_ddlSubCategory")
                    strEquipCat = strEquipCat & ddl.SelectedValue
                End If
            Next

            Return strEquipCat
        End Get

        Set(ByVal value As String)
            Dim i As Integer
            For i = 0 To parentRepeater.Items.Count - 1
                Dim cbl As CheckBox = Me.parentRepeater.Items(i).FindControl("_cbCategory")
                If cbl.Checked = True And cbl.Text = "Market Channel" Then
                    Dim ddl As DropDownList = Me.parentRepeater.Items(i).FindControl("_ddlSubCategory")
                    ddl.SelectedValue = value
                End If
            Next
        End Set

    End Property
    Public Property VisibleDropDown() As Boolean
        Get
            Return Me._lblCategory.Visible
        End Get
        Set(ByVal value As Boolean)
            Me._lblCategory.Visible = value
        End Set
    End Property
    Public Property enablePanel() As Boolean
        Get
            Return Me._Pnl.Enabled
        End Get
        Set(ByVal value As Boolean)
            Me._Pnl.Enabled = value
        End Set
    End Property
    

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Page.IsPostBack = False Or Me.IsViewStateEnabled = False Then
            PopulateCategory()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "MOC") Then
            Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "MOC", Page.ResolveClientUrl("~/MOC/MOC.js"))
        End If
        'If Not IsPostBack Then
        '    PopulateCategory()
        'End If
    End Sub
    Public Sub RefreshDisplay()
        PopulateCategory()
    End Sub

    Private Sub PopulateCategory(Optional ByVal userName As String = "", Optional ByVal facility As String = "")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As New System.Data.DataSet
        Dim ipLoc As New IP.Bids.Localization.WebLocalization()

        Try
            param = New OracleParameter
            param.ParameterName = "rsMOCCategory"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsMOCSubCategory"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            'ds = New dataset
            Dim key As String = "ViewMOC.CatSubCatList" ' & facility & "_" & division & "_" & inActiveFlag & "_" & userName
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.ViewMOC.CatSubCatList", key, 3)

            parentRepeater.DataSource = ds.Tables(0)
            parentRepeater.DataBind()


        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
            paramCollection = Nothing
        End Try
    End Sub

    'Protected Sub parentRepeater_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles parentRepeater.ItemCreated
    '    Dim drv As Data.DataRowView = TryCast(e.Item.DataItem, Data.DataRowView)
    '    Dim strEquip = drv.Row.Item(0).ToString
    '    'RI.SharedFunctions.LocalizeValue(drv.Row.Item(0).ToString)

    '    Dim ipLoc As New IP.Bids.Localization.WebLocalization()

    '    Dim cbCategory As CheckBox
    '    cbCategory = TryCast(e.Item.FindControl("_cbCategory"), CheckBox)
    '    If cbCategory.Text IsNot Nothing Then
    '        cbCategory.Text = RI.SharedFunctions.LocalizeValue(strEquip)
    '    End If

    'End Sub

    Public Sub ParentRepeater_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles parentRepeater.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim drv As Data.DataRowView = TryCast(e.Item.DataItem, Data.DataRowView)
            Dim strEquip = drv.Row.Item(0).ToString
            
            Dim dsGet As Data.DataSet
            Dim stGet As String = "SELECT mocsubcategory FROM refmocsubcategory a, refmoccategory b WHERE a.moccategory_seq_id = b.moccategory_seq_id and moccategory = '" & strEquip & "' order by mocsubcategory"
            Dim cbCurrent As CheckBoxList = Nothing
            Dim ddlCurrent As DropDownList = Nothing
            Dim ipLoc As New IP.Bids.Localization.WebLocalization()

            dsGet = New Data.DataSet()
            dsGet = RI.SharedFunctions.GetOracleDataSet(stGet)

            If dsGet IsNot Nothing Then
                Dim intSubCatCount As Integer
                intSubCatCount = dsGet.Tables(0).Rows.Count
                Dim cbCategory As CheckBox
                cbCategory = TryCast(e.Item.FindControl("_cbCategory"), CheckBox)

                If intSubCatCount >= 0 Then
                    'If strEquip = "Equipment" Then
                    '    ddlCurrent = TryCast(e.Item.FindControl("_ddlSubCategory"), DropDownList)
                    '    ddlCurrent.DataSource = dsGet
                    '    ddlCurrent.DataTextField = "MOCSubCategory"
                    '    ddlCurrent.DataValueField = "MOCSubCategory"
                    '    ddlCurrent.DataBind()
                    'Else
                    cbCurrent = TryCast(e.Item.FindControl("_cblSubCategory"), CheckBoxList)
                    cbCurrent.DataSource = dsGet
                    cbCurrent.DataTextField = "MOCSubCategory"
                    cbCurrent.DataValueField = "MOCSubCategory"
                    cbCurrent.DataBind()
                    ipLoc.LocalizeListControl(cbCurrent)

                    'cbCurrent.ClientID = "SubCategory"
                    'cbCurrent.Attributes.Add("onClick", "fnCheckParent('" & strEquip & "','" & e.Item.ClientID & "__cbCategory');")
                    cbCurrent.Attributes.Add("onClick", "fnCheckParent('" & strEquip & "','" & cbCategory.ClientID & "');")
                    'cbCategory.Attributes.Add("onClick", "fnUnCheckChild('" & e.Item.ClientID & "__cblSubCategory'," & intSubCatCount & ");")
                    cbCategory.Attributes.Add("onClick", "fnUnCheckChild('" & cbCurrent.ClientID & "'," & intSubCatCount & ");")
                    'cbCurrent.Attributes.Add("onClick", "alert('" & e.Item.ClientID & "'); fnCheckParent('" & strEquip & "','" & cbCategory.ClientID & "');")
                    ddlCurrent = TryCast(e.Item.FindControl("_ddlSubCategory"), DropDownList)

                    If strEquip = "Equipment" Then
                        ddlCurrent.DataSource = dsGet
                        ddlCurrent.DataTextField = "MOCSubCategory"
                        ddlCurrent.DataValueField = "MOCSubCategory"
                        ddlCurrent.DataBind()
                        cbCurrent.Visible = "false"
                        ddlCurrent.Items.Insert(0, New ListItem(String.Empty, String.Empty))
                        ddlCurrent.Attributes.Add("onClick", "fnCheckParent('" & strEquip & "','" & cbCategory.ClientID & "');")
                        ipLoc.LocalizeListControl(ddlCurrent)
                    ElseIf strEquip = "Market Channel" Then
                        stGet = "SELECT mocsubcategory FROM refmocsubcategory a, refmoccategory b WHERE a.moccategory_seq_id = b.moccategory_seq_id and moccategory = '" & strEquip & "' order by mocsubcategory"
                        dsGet = New Data.DataSet()
                        dsGet = RI.SharedFunctions.GetOracleDataSet(stGet)
                        ddlCurrent.DataSource = dsGet
                        ddlCurrent.DataTextField = "mocsubcategory"
                        ddlCurrent.DataValueField = "mocsubcategory"
                        ddlCurrent.DataBind()
                        cbCurrent.Visible = "false"
                        ddlCurrent.Items.Insert(0, New ListItem(String.Empty, String.Empty))
                        ddlCurrent.Attributes.Add("onClick", "fnCheckParent('" & strEquip & "','" & cbCategory.ClientID & "');")
                        ipLoc.LocalizeListControl(ddlCurrent)
                    Else
                        ddlCurrent.Visible = "false"
                    End If
                End If
                
            End If
            If cbCurrent.SelectedIndex < 0 Then
                If cbCurrent.Items.FindByValue("All") IsNot Nothing Then
                    cbCurrent.Items.FindByValue("All").Selected = True
                End If
            End If
            
        End If
    End Sub
End Class
