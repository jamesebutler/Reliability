Imports IP.BIDS.Localization
Partial Class ucContractorSwapList
    Inherits System.Web.UI.UserControl


    Public Property AvailableTextField() As String
        Get
            Return Me._lblAllFields.Text
        End Get
        Set(ByVal value As String)
            Me._lblAllFields.Text = value
        End Set
    End Property

    Public Property SelectedTextField() As String
        Get
            Return Me._lblSelectedFields.Text
        End Get
        Set(ByVal value As String)
            Me._lblSelectedFields.Text = value
        End Set
    End Property

    Private mLocalizeData As Boolean

    Public Property LocalizeData() As Boolean
        Get
            Return mLocalizeData
        End Get
        Set(ByVal value As Boolean)
            mLocalizeData = value
        End Set
    End Property
    Public Property SelectedDataTextField() As String
        Get
            Dim o As Object = ViewState("SelectedDataTextField")
            If o Is Nothing Then
                Return String.Empty
            Else
                Return CStr(o)
            End If
        End Get
        Set(ByVal value As String)
            ViewState("SelectedDataTextField") = value
        End Set
    End Property
    Public Property SelectedDataValueField() As String
        Get
            Dim o As Object = ViewState("SelectedDataValueField")
            If o Is Nothing Then
                Return String.Empty
            Else
                Return CStr(o)
            End If
        End Get
        Set(ByVal value As String)
            ViewState("SelectedDataValueField") = value
        End Set
    End Property
    Private mSelectedDataSource As System.Data.DataTableReader = Nothing
    Public Property SelectedDataSource() As System.Data.DataTableReader
        Get
            Return mSelectedDataSource
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mSelectedDataSource = value
        End Set
    End Property

    Public Property DataTextField() As String
        Get
            Dim o As Object = ViewState("DataTextField")
            If o Is Nothing Then
                Return String.Empty
            Else
                Return CStr(o)
            End If
        End Get
        Set(ByVal value As String)
            ViewState("DataTextField") = value
        End Set
    End Property
    Public Property DataValueField() As String
        Get
            Dim o As Object = ViewState("DataValueField")
            If o Is Nothing Then
                Return String.Empty
            Else
                Return CStr(o)
            End If
        End Get
        Set(ByVal value As String)
            ViewState("DataValueField") = value
        End Set
    End Property
    Private mDataSource As System.Data.DataTableReader = Nothing
    Public Property DataSource() As System.Data.DataTableReader
        Get
            Return mDataSource
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mDataSource = value
        End Set
    End Property

    Public Property SelectedValue() As String
        Get
            Return Replace(RI.SharedFunctions.GetListBoxValues(_lbSelectedFields), ",,", ",")
        End Get
        Set(ByVal value As String)
            RI.SharedFunctions.SetListBoxValues(_lbSelectedFields, value)
            RemoveSelectedItemsFromAll()
        End Set
    End Property
    Public Function GetListBoxText(ByVal cb As ListBox) As String
        Dim sb As New StringBuilder
        For i As Integer = 0 To cb.Items.Count - 1
            If cb.Items(i) IsNot Nothing Then
                ' List the selected items
                If sb.Length > 0 Then sb.Append(",")
                sb.Append(cb.Items(i).Text.Trim)
            End If
        Next
        Return sb.ToString
    End Function
    Public ReadOnly Property SelectedText() As String
        Get
            Return GetListBoxText(_lbSelectedFields)
        End Get
        'Set(ByVal value As String)
        '    RI.SharedFunctions.SetListBoxValues(_lbSelectedFields, value)
        '    RemoveSelectedItemsFromAll()
        'End Set
    End Property
    Private Sub RemoveSelectedItemsFromAll()
        If Me._lbSelectedFields.Items.Count > 0 Then
            For i As Integer = 0 To _lbSelectedFields.Items.Count - 1
                If Me._lbAllFields.Items.FindByValue(_lbSelectedFields.Items(i).Value.Trim) IsNot Nothing Then
                    _lbAllFields.Items.Remove(Me._lbAllFields.Items.FindByValue(_lbSelectedFields.Items(i).Value.Trim))
                End If
            Next
        End If
    End Sub
    Private Sub RemoveBlanks()
        If Me._lbSelectedFields.Items.Count > 0 Then
            For i As Integer = 0 To _lbSelectedFields.Items.Count - 1
                If _lbSelectedFields.Items(i).Value.Trim.Length = 0 Then
                    _lbSelectedFields.Items.Remove(_lbSelectedFields.Items(i))
                End If
            Next
        End If
        If Me._lbAllFields.Items.Count > 0 Then
            For i As Integer = 0 To _lbAllFields.Items.Count - 1
                If _lbAllFields.Items(i).Value.Trim.Length = 0 Then
                    _lbAllFields.Items.Remove(Me._lbAllFields.Items(i))
                End If
            Next
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "SwapListBox") Then Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "SwapListBox", Page.ResolveClientUrl("~/ri/User Controls/Common/SwapListBox.js"))
        Dim hiddenFields As String = "','" & Me._hdfAllFields.ClientID.ToString & "','" & Me._hdSelectedFields.ClientID.ToString
        If Not Page.ClientScript.IsOnSubmitStatementRegistered(Page.GetType, "SwapListPost") Then Page.ClientScript.RegisterOnSubmitStatement(Page.GetType, "SwapListPost", "selectAll('" & Me._lbAllFields.ClientID.ToString & "',  '" & Me._lbSelectedFields.ClientID.ToString & hiddenFields & "');")
        Me._btnMoveSelected.OnClientClick = "moveDualList( this.form." & Me._lbAllFields.ClientID.ToString & ",  this.form." & Me._lbSelectedFields.ClientID.ToString & ", false,false );return false;"
        Me._btnMoveAll.OnClientClick = "moveDualList( this.form." & Me._lbAllFields.ClientID.ToString & ",  this.form." & Me._lbSelectedFields.ClientID.ToString & ", true ,false);return false;"
        Me._btnRemoveAll.OnClientClick = "moveDualList( this.form." & Me._lbSelectedFields.ClientID.ToString & ",  this.form." & Me._lbAllFields.ClientID.ToString & ", true,true );return false;"
        Me._btnRemoveSelected.OnClientClick = "moveDualList( this.form." & Me._lbSelectedFields.ClientID.ToString & ",  this.form." & Me._lbAllFields.ClientID.ToString & ", false,true );return false;"
        'Me._btnMoveUp.OnClientClick = "move(this.form,true, this.form." & Me._lbSelectedFields.ClientID.ToString & ");return false;"
        'Me._btnMoveDown.OnClientClick = "move(this.form,false, this.form." & Me._lbSelectedFields.ClientID.ToString & ");return false;"
        Me._lbAllFields.Attributes.Add("onDblClick", "moveDualList( this.form." & Me._lbAllFields.ClientID.ToString & ",  this.form." & Me._lbSelectedFields.ClientID.ToString & ", false,false );return false;")
        Me._lbSelectedFields.Attributes.Add("onDblClick", "moveDualList( this.form." & Me._lbSelectedFields.ClientID.ToString & ",  this.form." & Me._lbAllFields.ClientID.ToString & ", false ,true);return false;")
        'move(f,bDir,sName)
        If Page.IsPostBack Then
            Dim allList As String() = Me._hdfAllFields.Value.Split(",")
            Dim moveToAllList As New OrderedDictionary
            Dim moveToSelectedList As New OrderedDictionary
            If allList.Length > 1 Then
                For i As Integer = 0 To allList.Length - 1
                    If allList(i).Length > 0 Then
                        Dim nextItem As ListItem = _lbAllFields.Items.FindByValue(allList(i))
                        If nextItem IsNot Nothing Then
                            Dim val As String = nextItem.Value
                            Dim txt As String = nextItem.Text
                            moveToAllList.Add(val, txt)
                            _lbAllFields.Items.Remove(nextItem)
                        Else
                            'we need to know why nextItem is null
                            Dim doNothing As String = ""
                        End If
                    End If
                Next
            End If
            Dim selectedList As String() = Me._hdSelectedFields.Value.Split(",")
            If selectedList.Length > 1 Then
                For i As Integer = 0 To selectedList.Length - 1
                    If selectedList(i).Length > 0 Then
                        Dim nextItem As ListItem = _lbAllFields.Items.FindByValue(selectedList(i))
                        If nextItem IsNot Nothing Then
                            Dim val As String = nextItem.Value
                            Dim txt As String = nextItem.Text
                            moveToSelectedList.Add(val, txt)
                            _lbAllFields.Items.Remove(nextItem)
                        Else
                            Dim selItem As ListItem = _lbSelectedFields.Items.FindByValue(selectedList(i))
                            If selItem IsNot Nothing Then
                                moveToSelectedList.Add(selItem.Value, selItem.Text)
                                _lbSelectedFields.Items.Remove(selItem)
                            End If
                        End If
                        '_lbSelectedFields.Items.Add(selectedList(i))
                    End If
                Next
            End If
            If moveToAllList.Count > 0 Then
                _lbAllFields.Items.Clear()
                For Each obj As DictionaryEntry In moveToAllList
                    _lbAllFields.Items.Add(New ListItem(obj.Value, obj.Key))
                Next
            End If
            If moveToSelectedList.Count > 0 Then
                _lbSelectedFields.Items.Clear()
                For Each obj As DictionaryEntry In moveToSelectedList
                    _lbSelectedFields.Items.Add(New ListItem(obj.Value, obj.Key))
                Next
            End If
            Me.RemoveSelectedItemsFromAll()
        End If
    End Sub
    Public Overrides Sub DataBind()
        Try
            If Me.DataSource IsNot Nothing Then
                With Me._lbAllFields
                    Dim dr As Data.DataTableReader = DataSource
                    Dim textField As String = DataTextField
                    Dim valueField As String = DataValueField
                    .Items.Clear()
                    While dr.Read()
                        .Items.Add(New ListItem(dr.Item(textField), dr.Item(valueField).ToString))
                    End While
                    dr.Close()
                    dr = Nothing
                End With
            End If
            If Me.SelectedDataSource IsNot Nothing Then
                With Me._lbSelectedFields
                    Dim dr As Data.DataTableReader = SelectedDataSource
                    Dim textField As String = SelectedDataTextField
                    Dim valueField As String = SelectedDataValueField
                    .Items.Clear()
                    While dr.Read()
                        .Items.Add(New ListItem(dr.Item(textField), dr.Item(valueField).ToString))
                    End While
                    dr.Close()
                    dr = Nothing
                End With
            End If
        Catch ex As Exception
            RI.SharedFunctions.HandleError("DataBind", , ex)
        End Try
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        RemoveBlanks()
        If LocalizeData = True Then
            Dim RIRESOURCES As New IP.Bids.Localization.WebLocalization
            If LocalizeData Then RIRESOURCES.LocalizeListControl(_lbAllFields)
            If LocalizeData Then RIRESOURCES.LocalizeListControl(_lbSelectedFields)
            RIRESOURCES = Nothing
        End If
    End Sub
End Class
