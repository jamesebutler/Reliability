Imports IP.BIDS.Localization
Partial Class ucMOCSwapList
    Inherits System.Web.UI.UserControl

    Public ReadOnly Property AvailableListID() As String
        Get
            Return Me._lbAllFields.ClientID
        End Get
    End Property
    Public ReadOnly Property AvailableID() As ListBox
        Get
            Return Me._lbAllFields
        End Get
    End Property
    Public ReadOnly Property ApproverL2ListID() As String
        Get
            Return Me._lbApproversL2.ClientID
        End Get
    End Property
    Public ReadOnly Property ApproverL2ID() As ListBox
        Get
            Return Me._lbApproversL2
        End Get
    End Property
    Public ReadOnly Property ApproverL3ListID() As String
        Get
            Return Me._lbApproversL3.ClientID
        End Get
    End Property
    Public ReadOnly Property ApproverL3ID() As ListBox
        Get
            Return Me._lbApproversL3
        End Get
    End Property
    Public ReadOnly Property ApproverL1ListID() As String
        Get
            Return Me._lbApproversL1.ClientID
        End Get
    End Property
    Public ReadOnly Property ApproverL1ID() As ListBox
        Get
            Return Me._lbApproversL1
        End Get
    End Property

    Public ReadOnly Property InformedListID() As String
        Get
            Return Me._lbInformed.ClientID
        End Get
    End Property
    Public ReadOnly Property InformedID() As ListBox
        Get
            Return Me._lbInformed
        End Get
    End Property
    Private mLocalizeData As Boolean

    ''' <summary>
    ''' Gets or sets a boolean to determine if the data inside this control should be localized
    ''' </summary>
    ''' <returns>True - Entered data will be Localized, False - Data is displayed as entered</returns>
    ''' <remarks></remarks>
    Public Property LocalizeData() As Boolean
        Get
            Return mLocalizeData
        End Get
        Set(ByVal value As Boolean)
            mLocalizeData = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the field of the data source that provides the text content of the upper right listbox list items.  
    ''' </summary> 
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

    ''' <summary>
    ''' Gets or sets the field of the data source that provides the value of each list item in the upper right list box. 
    ''' </summary> 
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
    '''<summary>
    '''Gets or sets the object from which the data-bound control retrieves its list of data items.
    '''</summary>
    Public Property SelectedDataSource() As System.Data.DataTableReader
        Get
            Return mSelectedDataSource
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mSelectedDataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the field of the data source that provides the text content of the lower right listbox list items.  
    ''' </summary> 
    Public Property SecondarySelectedDataTextField() As String
        Get
            Dim o As Object = ViewState("SecondarySelectedDataTextField")
            If o Is Nothing Then
                Return String.Empty
            Else
                Return CStr(o)
            End If
        End Get
        Set(ByVal value As String)
            ViewState("SecondarySelectedDataTextField") = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the field of the data source that provides the value of each list item in the lower right list box. 
    ''' </summary> 
    Public Property SecondarySelectedDataValueField() As String
        Get
            Dim o As Object = ViewState("SecondarySelectedDataValueField")
            If o Is Nothing Then
                Return String.Empty
            Else
                Return CStr(o)
            End If
        End Get
        Set(ByVal value As String)
            ViewState("SecondarySelectedDataValueField") = value
        End Set
    End Property

    Private mSecondarySelectedDataSource As System.Data.DataTableReader = Nothing

    '''<summary>
    '''Gets or sets the object from which the data-bound control retrieves its list of data items.
    '''</summary>
    Public Property SecondarySelectedDataSource() As System.Data.DataTableReader
        Get
            Return mSecondarySelectedDataSource
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mSecondarySelectedDataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the field of the data source that provides the text content of the lower right listbox list items.  
    ''' </summary> 
    Public Property ThirdSelectedDataTextField() As String
        Get
            Dim o As Object = ViewState("ThirdSelectedDataTextField")
            If o Is Nothing Then
                Return String.Empty
            Else
                Return CStr(o)
            End If
        End Get
        Set(ByVal value As String)
            ViewState("ThirdSelectedDataTextField") = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the field of the data source that provides the value of each list item in the lower right list box. 
    ''' </summary> 
    Public Property ThirdSelectedDataValueField() As String
        Get
            Dim o As Object = ViewState("ThirdSelectedDataValueField")
            If o Is Nothing Then
                Return String.Empty
            Else
                Return CStr(o)
            End If
        End Get
        Set(ByVal value As String)
            ViewState("ThirdSelectedDataValueField") = value
        End Set
    End Property
    Private mThirdSelectedDataSource As System.Data.DataTableReader = Nothing

    '''<summary>
    '''Gets or sets the object from which the data-bound control retrieves its list of data items.
    '''</summary>
    Public Property ThirdSelectedDataSource() As System.Data.DataTableReader
        Get
            Return mThirdSelectedDataSource
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mThirdSelectedDataSource = value
        End Set
    End Property
    Private mInformedSelectedDataSource As System.Data.DataTableReader = Nothing
    '''<summary>
    '''Gets or sets the object from which the data-bound control retrieves its list of data items.
    '''</summary>
    Public Property InformedSelectedDataSource() As System.Data.DataTableReader
        Get
            Return mInformedSelectedDataSource
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mInformedSelectedDataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Gets or sets the field of the data source that provides the text content of the lower right listbox list items.  
    ''' </summary> 
    Public Property InformedSelectedDataTextField() As String
        Get
            Dim o As Object = ViewState("InformedSelectedDataTextField")
            If o Is Nothing Then
                Return String.Empty
            Else
                Return CStr(o)
            End If
        End Get
        Set(ByVal value As String)
            ViewState("InformedSelectedDataTextField") = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the field of the data source that provides the value of each list item in the lower right list box. 
    ''' </summary> 
    Public Property InformedSelectedDataValueField() As String
        Get
            Dim o As Object = ViewState("InformedSelectedDataValueField")
            If o Is Nothing Then
                Return String.Empty
            Else
                Return CStr(o)
            End If
        End Get
        Set(ByVal value As String)
            ViewState("InformedSelectedDataValueField") = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the field of the data source that provides the text content of the left listbox list items.  
    ''' </summary> 
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

    ''' <summary>
    ''' Gets or sets the field of the data source that provides the value of each list item in the left list box. 
    ''' </summary> 
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
    '''<summary>
    '''Gets or sets the object from which the data-bound control retrieves its list of data items.
    '''</summary>
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
            Return Replace(RI.SharedFunctions.GetListBoxValues(_lbApproversL1), ",,", ",")
        End Get
        Set(ByVal value As String)
            RI.SharedFunctions.SetListBoxValues(_lbApproversL1, value)
            RemoveSelectedItemsFromAll()
        End Set
    End Property

    Public Property SelectedSecondaryValue() As String
        Get
            Return Replace(RI.SharedFunctions.GetListBoxValues(_lbApproversL2), ",,", ",")
        End Get
        Set(ByVal value As String)
            RI.SharedFunctions.SetListBoxValues(_lbApproversL2, value)
            RemoveSelectedItemsFromAll()
        End Set
    End Property
    Public Property SelectedThirdValue() As String
        Get
            Return Replace(RI.SharedFunctions.GetListBoxValues(_lbApproversL3), ",,", ",")
        End Get
        Set(ByVal value As String)
            RI.SharedFunctions.SetListBoxValues(_lbApproversL3, value)
            RemoveSelectedItemsFromAll()
        End Set
    End Property
    Public Property SelectedInformedValue() As String
        Get
            Return Replace(RI.SharedFunctions.GetListBoxValues(_lbInformed), ",,", ",")
        End Get
        Set(ByVal value As String)
            RI.SharedFunctions.SetListBoxValues(_lbInformed, value)
            RemoveSelectedItemsFromAll()
        End Set
    End Property
    'This property will be used to know which users were selected for the First Level listbox.
    Public ReadOnly Property HiddenSelectedValue() As String
        Get
            Return Me._hdSelectedL1Fields.Value
        End Get
    End Property
    'This property will be used to know which users were selected for the Second Level listbox.
    Public ReadOnly Property HiddenSelectedSecondaryValue() As String
        Get
            Return Me._hdSelectedL2Fields.Value
        End Get
    End Property
    'This property will be used to know which users were selected for the Third Level listbox.
    Public ReadOnly Property HiddenSelectedThirdValue() As String
        Get
            Return Me._hdSelectedL3Fields.Value
        End Get
    End Property
    Public ReadOnly Property HiddenInformedSecondaryValue() As String
        Get
            Return Me._hdSelectedInformed.Value
        End Get
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
            Return GetListBoxText(_lbApproversL1)
        End Get
    End Property
    Public ReadOnly Property SelectedSecondaryText() As String
        Get
            Return GetListBoxText(Me._lbApproversL2)
        End Get
    End Property
    Public ReadOnly Property SelectedThirdText() As String
        Get
            Return GetListBoxText(Me._lbApproversL3)
        End Get
    End Property
    Public ReadOnly Property SelectedInformedText() As String
        Get
            Return GetListBoxText(Me._lbInformed)
        End Get
    End Property
    Private Sub RemoveSelectedItemsFromAll()
        If Me._lbApproversL1.Items.Count > 0 Then
            For i As Integer = 0 To _lbApproversL1.Items.Count - 1
                If Me._lbAllFields.Items.FindByValue(_lbApproversL1.Items(i).Value.Trim) IsNot Nothing Then
                    _lbAllFields.Items.Remove(Me._lbAllFields.Items.FindByValue(_lbApproversL1.Items(i).Value.Trim))
                End If
            Next
        End If

        If Me._lbApproversL2.Items.Count > 0 Then
            For i As Integer = 0 To _lbApproversL2.Items.Count - 1
                If Me._lbAllFields.Items.FindByValue(_lbApproversL2.Items(i).Value.Trim) IsNot Nothing Then
                    _lbAllFields.Items.Remove(Me._lbAllFields.Items.FindByValue(_lbApproversL2.Items(i).Value.Trim))
                End If
            Next
        End If

        If Me._lbApproversL3.Items.Count > 0 Then
            For i As Integer = 0 To _lbApproversL3.Items.Count - 1
                If Me._lbAllFields.Items.FindByValue(_lbApproversL3.Items(i).Value.Trim) IsNot Nothing Then
                    _lbAllFields.Items.Remove(Me._lbAllFields.Items.FindByValue(_lbApproversL3.Items(i).Value.Trim))
                End If
            Next
        End If

        If Me._lbInformed.Items.Count > 0 Then
            For i As Integer = 0 To _lbInformed.Items.Count - 1
                If Me._lbAllFields.Items.FindByValue(_lbInformed.Items(i).Value.Trim) IsNot Nothing Then
                    _lbAllFields.Items.Remove(Me._lbAllFields.Items.FindByValue(_lbInformed.Items(i).Value.Trim))
                End If
            Next
        End If
    End Sub
    Private Sub RemoveBlanks()
        If Me._lbApproversL1.Items.Count > 0 Then
            For i As Integer = 0 To _lbApproversL1.Items.Count - 1
                If _lbApproversL1.Items(i).Value.Trim.Length = 0 Then
                    _lbApproversL1.Items.Remove(_lbApproversL1.Items(i))
                End If
            Next
        End If
        If Me._lbApproversL2.Items.Count > 0 Then
            For i As Integer = 0 To _lbApproversL2.Items.Count - 1
                If _lbApproversL2.Items(i).Value.Trim.Length = 0 Then
                    _lbApproversL2.Items.Remove(_lbApproversL2.Items(i))
                End If
            Next
        End If
        If Me._lbApproversL3.Items.Count > 0 Then
            For i As Integer = 0 To _lbApproversL3.Items.Count - 1
                If _lbApproversL3.Items(i).Value.Trim.Length = 0 Then
                    _lbApproversL3.Items.Remove(_lbApproversL3.Items(i))
                End If
            Next
        End If
        If Me._lbInformed.Items.Count > 0 Then
            For i As Integer = 0 To _lbInformed.Items.Count - 1
                If _lbInformed.Items(i).Value.Trim.Length = 0 Then
                    _lbInformed.Items.Remove(_lbInformed.Items(i))
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

        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "MOCSwapListBox") Then Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "MOCSwapListBox", Page.ResolveClientUrl("~/ri/User Controls/Common/MOCSwapListBox.js"))

        Dim hiddenFields As String = "','" & Me._hdfAllFields.ClientID.ToString & "','" & Me._hdSelectedL1Fields.ClientID.ToString & "','" & Me._hdSelectedL2Fields.ClientID.ToString & "','" & Me._hdSelectedL3Fields.ClientID.ToString & "','" & Me._hdSelectedInformed.ClientID.ToString
        'lbList1,lbList2,lbList3,allHidden,selectedHidden,secondarySelectedHidden
        If Not Page.ClientScript.IsOnSubmitStatementRegistered(Page.GetType, "MOCSwapListPost") Then Page.ClientScript.RegisterOnSubmitStatement(Page.GetType, "MOCSwapListPost", "MOCSwapList.selectAll('" & Me._lbAllFields.ClientID.ToString & "',  '" & Me._lbApproversL1.ClientID.ToString & "','" & Me._lbApproversL2.ClientID.ToString & "','" & Me._lbApproversL3.ClientID.ToString & "','" & Me._lbInformed.ClientID.ToString & hiddenFields & "');")


        Me._btnMoveSelected.OnClientClick = BuildMoveDualListJS(_lbAllFields, _lbApproversL1, _lbApproversL2, _lbApproversL3, _lbInformed, False, False) '"MOCSwapList.moveDualList( this.form." & Me._lbAllFields.ClientID.ToString & ",  this.form." & Me._lbApproversL1.ClientID.ToString & ", false,false );return false;"
        Me._btnMoveAll.OnClientClick = BuildMoveDualListJS(_lbAllFields, _lbApproversL1, _lbApproversL2, _lbApproversL3, _lbInformed, True, False) '"MOCSwapList.moveDualList( this.form." & Me._lbAllFields.ClientID.ToString & ",  this.form." & Me._lbApproversL1.ClientID.ToString & ", true ,false);return false;"

        Me._btnRemoveAll.OnClientClick = BuildMoveDualListJS(_lbAllFields, _lbApproversL1, _lbApproversL2, _lbApproversL3, _lbInformed, True, False, True) '"MOCSwapList.moveDualList( this.form." & Me._lbApproversL1.ClientID.ToString & ",  this.form." & Me._lbAllFields.ClientID.ToString & ", true,true );return false;"
        Me._btnRemoveSelected.OnClientClick = BuildMoveDualListJS(_lbAllFields, _lbApproversL1, _lbApproversL2, _lbApproversL3, _lbInformed, False, False, True) '"MOCSwapList.moveDualList( this.form." & Me._lbApproversL1.ClientID.ToString & ",  this.form." & Me._lbAllFields.ClientID.ToString & ", false,true );return false;"

        'Me._lbAllFields.Attributes.Add("Title", "Title is here")
        Me._lbAllFields.Attributes.Add("onDblClick", BuildMoveDualListJS(_lbAllFields, _lbApproversL1, _lbApproversL2, _lbApproversL3, _lbInformed, False, False))

        Me._lbApproversL1.Attributes.Add("onDblClick", "MOCSwapList.moveDualList( this.form." & _lbApproversL1.ClientID.ToString & ",  this.form." & Me._lbAllFields.ClientID.ToString & ", false ,true);return false;")
        Me._lbApproversL2.Attributes.Add("onDblClick", "MOCSwapList.moveDualList( this.form." & _lbApproversL2.ClientID.ToString & ",  this.form." & Me._lbAllFields.ClientID.ToString & ", false ,true);return false;")
        Me._lbApproversL3.Attributes.Add("onDblClick", "MOCSwapList.moveDualList( this.form." & _lbApproversL3.ClientID.ToString & ",  this.form." & Me._lbAllFields.ClientID.ToString & ", false ,true);return false;")
        Me._lbInformed.Attributes.Add("onDblClick", "MOCSwapList.moveDualList( this.form." & _lbInformed.ClientID.ToString & ",  this.form." & Me._lbAllFields.ClientID.ToString & ", false ,true);return false;")

        Me._lbApproversL1.Attributes.Add("onFocus", "this.form." & Me._rbL1Approvers.ClientID & ".checked=true;this.className='Border';this.form." & Me._lbApproversL2.ClientID & ".className='';this.form." & Me._lbInformed.ClientID & ".className='';this.form." & Me._lbApproversL3.ClientID & ".className='';")
        Me._lbApproversL2.Attributes.Add("onFocus", "this.form." & Me._rbL2Approvers.ClientID & ".checked=true;this.className='Border';this.form." & Me._lbApproversL1.ClientID & ".className='';this.form." & Me._lbInformed.ClientID & ".className='';this.form." & Me._lbApproversL3.ClientID & ".className='';")
        Me._lbApproversL3.Attributes.Add("onFocus", "this.form." & Me._rbL3Approvers.ClientID & ".checked=true;this.className='Border';this.form." & Me._lbApproversL1.ClientID & ".className='';this.form." & Me._lbInformed.ClientID & ".className='';this.form." & Me._lbApproversL2.ClientID & ".className='';")
        Me._lbInformed.Attributes.Add("onFocus", "this.form." & Me._rbInformed.ClientID & ".checked=true;this.className='Border';this.form." & Me._lbApproversL1.ClientID & ".className='';this.form." & Me._lbApproversL2.ClientID & ".className='';this.form." & Me._lbApproversL3.ClientID & ".className='';")

        Me._rbL1Approvers.Attributes.Add("onClick", "this.form." & Me._rbL1Approvers.ClientID & ".checked=true;this.form." & Me._lbApproversL1.ClientID & ".className='Border';this.form." & Me._lbApproversL2.ClientID & ".className='';this.form." & Me._lbInformed.ClientID & ".className='';this.form." & Me._lbApproversL3.ClientID & ".className=''")
        Me._rbL2Approvers.Attributes.Add("onClick", "this.form." & Me._rbL2Approvers.ClientID & ".checked=true;this.form." & Me._lbApproversL2.ClientID & ".className='Border';this.form." & Me._lbApproversL1.ClientID & ".className='';this.form." & Me._lbInformed.ClientID & ".className='';this.form." & Me._lbApproversL3.ClientID & ".className=''")
        Me._rbL3Approvers.Attributes.Add("onClick", "this.form." & Me._rbL3Approvers.ClientID & ".checked=true;this.form." & Me._lbApproversL3.ClientID & ".className='Border';this.form." & Me._lbApproversL1.ClientID & ".className='';this.form." & Me._lbInformed.ClientID & ".className='';this.form." & Me._lbApproversL2.ClientID & ".className=''")

        Me._rbInformed.Attributes.Add("onClick", "this.form." & Me._rbInformed.ClientID & ".checked=true;this.form." & Me._lbInformed.ClientID & ".className='Border';this.form." & Me._lbApproversL1.ClientID & ".className='';this.form." & Me._lbApproversL2.ClientID & ".className='';this.form." & Me._lbApproversL3.ClientID & ".className=''")

        If Page.IsPostBack Then
            Dim allList As String() = Me._hdfAllFields.Value.Split(",")
            Dim moveToAllList As New OrderedDictionary
            Dim moveToSelectedList As New OrderedDictionary
            Dim moveToSecondarySelectedList As New OrderedDictionary
            Dim moveToL3SelectedList As New OrderedDictionary
            Dim moveToInformedSelectedList As New OrderedDictionary

            'Left Listbox
            If allList.Length > 1 Then
                For i As Integer = 0 To allList.Length - 1
                    If allList(i).Length > 0 Then
                        Dim nextItem As ListItem = _lbAllFields.Items.FindByValue(allList(i))
                        If nextItem IsNot Nothing Then
                            Dim val As String = nextItem.Value
                            Dim txt As String = nextItem.Text
                            If Not moveToAllList.Contains(val) Then
                                moveToAllList.Add(val, txt)
                            End If
                            _lbAllFields.Items.Remove(nextItem)
                        Else
                            'we need to know why nextItem is null
                            Dim doNothing As String = ""
                        End If
                    End If
                Next
            End If

            Dim selectedList As String() = Me._hdSelectedL1Fields.Value.Split(",")
            '
            If selectedList.Length > 1 Then
                For i As Integer = 0 To selectedList.Length - 1
                    If selectedList(i).Length > 0 Then
                        Dim nextItem As ListItem = _lbAllFields.Items.FindByValue(selectedList(i))
                        If nextItem IsNot Nothing Then
                            Dim val As String = nextItem.Value
                            Dim txt As String = nextItem.Text
                            If Not moveToSelectedList.Contains(val) Then
                                moveToSelectedList.Add(val, txt)
                            End If
                            _lbAllFields.Items.Remove(nextItem)
                        Else
                            Dim selItem As ListItem = _lbApproversL1.Items.FindByValue(selectedList(i))
                            If selItem IsNot Nothing Then
                                If Not moveToSelectedList.Contains(selItem.Value) Then
                                    moveToSelectedList.Add(selItem.Value, selItem.Text)
                                End If
                                _lbApproversL1.Items.Remove(selItem)
                            End If
                        End If
                        '_lbSelectedFields.Items.Add(selectedList(i))
                    End If
                Next
            End If

            Dim selectedSecondaryList As String() = Me._hdSelectedL2Fields.Value.Split(",")
            If selectedSecondaryList.Length > 1 Then
                For i As Integer = 0 To selectedSecondaryList.Length - 1
                    If selectedSecondaryList(i).Length > 0 Then
                        Dim nextItem As ListItem = _lbAllFields.Items.FindByValue(selectedSecondaryList(i))
                        If nextItem IsNot Nothing Then
                            Dim val As String = nextItem.Value
                            Dim txt As String = nextItem.Text
                            If Not moveToSecondarySelectedList.Contains(val) Then
                                moveToSecondarySelectedList.Add(val, txt)
                            End If
                            _lbAllFields.Items.Remove(nextItem)
                        Else
                            Dim selItem As ListItem = _lbApproversL2.Items.FindByValue(selectedSecondaryList(i))
                            If selItem IsNot Nothing Then
                                If Not moveToSecondarySelectedList.Contains(selItem.Value) Then
                                    moveToSecondarySelectedList.Add(selItem.Value, selItem.Text)
                                End If
                                _lbApproversL2.Items.Remove(selItem)
                            End If
                        End If
                        '_lbSelectedFields.Items.Add(selectedList(i))
                    End If
                Next
            End If

            Dim selectedInformedList As String() = Me._hdSelectedInformed.Value.Split(",")
            If selectedInformedList.Length > 1 Then
                For i As Integer = 0 To selectedInformedList.Length - 1
                    If selectedInformedList(i).Length > 0 Then
                        Dim nextItem As ListItem = _lbAllFields.Items.FindByValue(selectedInformedList(i))
                        If nextItem IsNot Nothing Then
                            Dim val As String = nextItem.Value
                            Dim txt As String = nextItem.Text
                            If Not moveToInformedSelectedList.Contains(val) Then
                                moveToInformedSelectedList.Add(val, txt)
                            End If
                            _lbAllFields.Items.Remove(nextItem)
                        Else
                            Dim selItem As ListItem = _lbInformed.Items.FindByValue(selectedInformedList(i))
                            If selItem IsNot Nothing Then
                                If Not moveToInformedSelectedList.Contains(selItem.Value) Then
                                    moveToInformedSelectedList.Add(selItem.Value, selItem.Text)
                                End If
                                _lbInformed.Items.Remove(selItem)
                            End If
                        End If
                        '_lbSelectedFields.Items.Add(selectedList(i))
                    End If
                Next
            End If

            Dim selectedL3List As String() = Me._hdSelectedL3Fields.Value.Split(",")
            If selectedL3List.Length > 1 Then
                For i As Integer = 0 To selectedL3List.Length - 1
                    If selectedL3List(i).Length > 0 Then
                        Dim nextItem As ListItem = _lbAllFields.Items.FindByValue(selectedL3List(i))
                        If nextItem IsNot Nothing Then
                            Dim val As String = nextItem.Value
                            Dim txt As String = nextItem.Text
                            If Not moveToL3SelectedList.Contains(val) Then
                                moveToL3SelectedList.Add(val, txt)
                            End If
                            _lbAllFields.Items.Remove(nextItem)
                        Else
                            Dim selItem As ListItem = Me._lbApproversL3.Items.FindByValue(selectedL3List(i))
                            If selItem IsNot Nothing Then
                                If Not moveToL3SelectedList.Contains(selItem.Value) Then
                                    moveToL3SelectedList.Add(selItem.Value, selItem.Text)
                                End If
                                Me._lbApproversL3.Items.Remove(selItem)
                            End If
                        End If
                        '_lbSelectedFields.Items.Add(selectedList(i))
                    End If
                Next
            End If

            _lbAllFields.Items.Clear()
            If moveToAllList.Count > 0 Then
                For Each obj As DictionaryEntry In moveToAllList
                    _lbAllFields.Items.Add(New ListItem(obj.Value, obj.Key))
                Next
            End If

            _lbApproversL1.Items.Clear()
            If moveToSelectedList.Count > 0 Then
                For Each obj As DictionaryEntry In moveToSelectedList
                    _lbApproversL1.Items.Add(New ListItem(obj.Value, obj.Key))
                Next
            End If

            _lbApproversL2.Items.Clear()
            If moveToSecondarySelectedList.Count > 0 Then
                For Each obj As DictionaryEntry In moveToSecondarySelectedList
                    _lbApproversL2.Items.Add(New ListItem(obj.Value, obj.Key))
                Next
            End If

            _lbApproversL3.Items.Clear()
            If moveToSecondarySelectedList.Count > 0 Then
                For Each obj As DictionaryEntry In moveToL3SelectedList
                    _lbApproversL3.Items.Add(New ListItem(obj.Value, obj.Key))
                Next
            End If

            _lbInformed.Items.Clear()
            If moveToInformedSelectedList.Count > 0 Then
                For Each obj As DictionaryEntry In moveToInformedSelectedList
                    _lbInformed.Items.Add(New ListItem(obj.Value, obj.Key))
                Next
            End If

        End If
        Me.RemoveSelectedItemsFromAll()
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
                With Me._lbApproversL1
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
            If Me.SecondarySelectedDataSource IsNot Nothing Then
                With Me._lbApproversL2
                    Dim dr As Data.DataTableReader = SecondarySelectedDataSource
                    Dim textField As String = SecondarySelectedDataTextField
                    Dim valueField As String = SecondarySelectedDataValueField
                    .Items.Clear()
                    While dr.Read()
                        .Items.Add(New ListItem(dr.Item(textField), dr.Item(valueField).ToString))
                    End While
                    dr.Close()
                    dr = Nothing
                End With
            End If
            If Me.ThirdSelectedDataSource IsNot Nothing Then
                With Me._lbApproversL3
                    Dim dr As Data.DataTableReader = ThirdSelectedDataSource
                    Dim textField As String = ThirdSelectedDataTextField
                    Dim valueField As String = ThirdSelectedDataValueField
                    .Items.Clear()
                    While dr.Read()
                        .Items.Add(New ListItem(dr.Item(textField), dr.Item(valueField).ToString))
                    End While
                    dr.Close()
                    dr = Nothing
                End With
            End If
            If Me.InformedSelectedDataSource IsNot Nothing Then
                With Me._lbInformed
                    Dim dr As Data.DataTableReader = InformedSelectedDataSource
                    Dim textField As String = InformedSelectedDataTextField
                    Dim valueField As String = InformedSelectedDataValueField
                    .Items.Clear()
                    While dr.Read()
                        .Items.Add(New ListItem(dr.Item(textField), dr.Item(valueField).ToString))
                    End While
                    dr.Close()
                    dr = Nothing
                End With
            End If
            Me.RemoveSelectedItemsFromAll()
        Catch ex As Exception
            RI.SharedFunctions.HandleError("DataBind", , ex)
        End Try
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        RemoveBlanks()
        If LocalizeData = True Then
            Dim RIRESOURCES As New IP.Bids.Localization.WebLocalization
            If LocalizeData Then RIRESOURCES.LocalizeListControl(_lbAllFields)
            If LocalizeData Then RIRESOURCES.LocalizeListControl(_lbApproversL1)
            If LocalizeData Then RIRESOURCES.LocalizeListControl(_lbApproversL2)
            If LocalizeData Then RIRESOURCES.LocalizeListControl(_lbApproversL3)
            If LocalizeData Then RIRESOURCES.LocalizeListControl(_lbInformed)
            RIRESOURCES = Nothing
        End If
    End Sub

    Function BuildMoveDualListJS(ByVal list1 As ListBox, ByVal list2 As ListBox, ByVal list3 As ListBox, ByVal list4 As ListBox, ByVal list5 As ListBox, ByVal moveAll As Boolean, ByVal sortList As Boolean, Optional ByVal reverseMove As Boolean = False) As String
        Dim js As New StringBuilder

        If moveAll = True Then
            If reverseMove = False Then
                js.Append("if (this.form.")
                js.Append(Me._rbL1Approvers.ClientID)
                js.Append(".checked==true){")
                js.Append("MOCSwapList.moveAllDualList( this.form.")
                js.Append(list1.ClientID.ToString)
                js.Append(",")
                js.Append("this.form.")
                js.Append(list2.ClientID.ToString)
                js.Append(",")
                js.Append(moveAll.ToString.ToLower)
                js.Append(",")
                js.Append(sortList.ToString.ToLower)
                js.Append(")}")
                js.Append("else")
                js.Append(" if (this.form.")
                js.Append(Me._rbL2Approvers.ClientID)
                js.Append(".checked==true){")
                js.Append("MOCSwapList.moveAllDualList( this.form.")
                js.Append(list1.ClientID.ToString)
                js.Append(",")
                js.Append("this.form.")
                js.Append(list3.ClientID.ToString)
                js.Append(",")
                js.Append(moveAll.ToString.ToLower)
                js.Append(",")
                js.Append(sortList.ToString.ToLower)
                js.Append(")}")
                js.Append("else")
                js.Append(" if (this.form.")
                js.Append(Me._rbL3Approvers.ClientID)
                js.Append(".checked==true){")
                js.Append("MOCSwapList.moveAllDualList( this.form.")
                js.Append(list1.ClientID.ToString)
                js.Append(",")
                js.Append("this.form.")
                js.Append(list4.ClientID.ToString)
                js.Append(",")
                js.Append(moveAll.ToString.ToLower)
                js.Append(",")
                js.Append(sortList.ToString.ToLower)
                js.Append(")}")
                js.Append("else {")
                js.Append("MOCSwapList.moveAllDualList( this.form.")
                js.Append(list1.ClientID.ToString)
                js.Append(",")
                js.Append("this.form.")
                js.Append(list5.ClientID.ToString)
                js.Append(",")
                js.Append(moveAll.ToString.ToLower)
                js.Append(",")
                js.Append(sortList.ToString.ToLower)
                js.Append(")}")
                js.Append(" return false;")
                '"MOCSwapList.moveDualList( this.form." & Me._lbAllFields.ClientID.ToString & ",  this.form." & Me._lbApproversL1.ClientID.ToString & ", false,false );return false;"
            Else
                js.Append("if (this.form.")
                js.Append(Me._rbL1Approvers.ClientID)
                js.Append(".checked==true){")
                js.Append("MOCSwapList.moveAllDualList( this.form.")
                js.Append(list2.ClientID.ToString)
                js.Append(",")
                js.Append("this.form.")
                js.Append(list1.ClientID.ToString)
                js.Append(",")
                js.Append(moveAll.ToString.ToLower)
                js.Append(",")
                js.Append(sortList.ToString.ToLower)
                js.Append(")}")
                js.Append("else ")
                js.Append("if (this.form.")
                js.Append(Me._rbL2Approvers.ClientID)
                js.Append(".checked==true){")
                js.Append("MOCSwapList.moveAllDualList( this.form.")
                js.Append(list3.ClientID.ToString)
                js.Append(",")
                js.Append("this.form.")
                js.Append(list1.ClientID.ToString)
                js.Append(",")
                js.Append(moveAll.ToString.ToLower)
                js.Append(",")
                js.Append(sortList.ToString.ToLower)
                js.Append(")}")
                js.Append("else ")
                js.Append("if (this.form.")
                js.Append(Me._rbL3Approvers.ClientID)
                js.Append(".checked==true){")
                js.Append("MOCSwapList.moveAllDualList( this.form.")
                js.Append(list4.ClientID.ToString)
                js.Append(",")
                js.Append("this.form.")
                js.Append(list1.ClientID.ToString)
                js.Append(",")
                js.Append(moveAll.ToString.ToLower)
                js.Append(",")
                js.Append(sortList.ToString.ToLower)
                js.Append(")}")
                js.Append("else ")
                js.Append("if (this.form.")
                js.Append(Me._rbInformed.ClientID)
                js.Append(".checked==true){")
                js.Append("MOCSwapList.moveAllDualList( this.form.")
                js.Append(list5.ClientID.ToString)
                js.Append(",")
                js.Append("this.form.")
                js.Append(list1.ClientID.ToString)
                js.Append(",")
                js.Append(moveAll.ToString.ToLower)
                js.Append(",")
                js.Append(sortList.ToString.ToLower)
                js.Append(")}")
                js.Append(" return false;")
            End If
        Else
            If reverseMove = False Then
                js.Append("if (this.form.")
                js.Append(Me._rbL1Approvers.ClientID)
                js.Append(".checked==true){")
                js.Append("MOCSwapList.moveDualList( this.form.")
                js.Append(list1.ClientID.ToString)
                js.Append(",")
                js.Append("this.form.")
                js.Append(list2.ClientID.ToString)
                js.Append(",")
                js.Append(moveAll.ToString.ToLower)
                js.Append(",")
                js.Append(sortList.ToString.ToLower)
                js.Append(")}")
                js.Append("else")
                js.Append(" if (this.form.")
                js.Append(Me._rbL2Approvers.ClientID)
                js.Append(".checked==true){")
                js.Append("MOCSwapList.moveDualList( this.form.")
                js.Append(list1.ClientID.ToString)
                js.Append(",")
                js.Append("this.form.")
                js.Append(list3.ClientID.ToString)
                js.Append(",")
                js.Append(moveAll.ToString.ToLower)
                js.Append(",")
                js.Append(sortList.ToString.ToLower)
                js.Append(")}")
                js.Append("else")
                js.Append(" if (this.form.")
                js.Append(Me._rbL3Approvers.ClientID)
                js.Append(".checked==true){")
                js.Append("MOCSwapList.moveDualList( this.form.")
                js.Append(list1.ClientID.ToString)
                js.Append(",")
                js.Append("this.form.")
                js.Append(list4.ClientID.ToString)
                js.Append(",")
                js.Append(moveAll.ToString.ToLower)
                js.Append(",")
                js.Append(sortList.ToString.ToLower)
                js.Append(")}")
                js.Append("else {")
                js.Append("MOCSwapList.moveDualList( this.form.")
                js.Append(list1.ClientID.ToString)
                js.Append(",")
                js.Append("this.form.")
                js.Append(list5.ClientID.ToString)
                js.Append(",")
                js.Append(moveAll.ToString.ToLower)
                js.Append(",")
                js.Append(sortList.ToString.ToLower)
                js.Append(")}")
                js.Append(" return false;")
            Else
                js.Append("if (this.form.")
                js.Append(Me._rbL1Approvers.ClientID)
                js.Append(".checked==true){")
                js.Append("MOCSwapList.moveDualList( this.form.")
                js.Append(list2.ClientID.ToString)
                js.Append(",")
                js.Append("this.form.")
                js.Append(list1.ClientID.ToString)
                js.Append(",")
                js.Append(moveAll.ToString.ToLower)
                js.Append(",")
                js.Append(sortList.ToString.ToLower)
                js.Append(")}")
                js.Append("else ")
                js.Append("if (this.form.")
                js.Append(Me._rbL2Approvers.ClientID)
                js.Append(".checked==true){")
                js.Append("MOCSwapList.moveDualList( this.form.")
                js.Append(list3.ClientID.ToString)
                js.Append(",")
                js.Append("this.form.")
                js.Append(list1.ClientID.ToString)
                js.Append(",")
                js.Append(moveAll.ToString.ToLower)
                js.Append(",")
                js.Append(sortList.ToString.ToLower)
                js.Append(")}")
                js.Append("else ")
                js.Append("if (this.form.")
                js.Append(Me._rbL3Approvers.ClientID)
                js.Append(".checked==true){")
                js.Append("MOCSwapList.moveDualList( this.form.")
                js.Append(list4.ClientID.ToString)
                js.Append(",")
                js.Append("this.form.")
                js.Append(list1.ClientID.ToString)
                js.Append(",")
                js.Append(moveAll.ToString.ToLower)
                js.Append(",")
                js.Append(sortList.ToString.ToLower)
                js.Append(")}")
                js.Append("else ")
                js.Append("if (this.form.")
                js.Append(Me._rbInformed.ClientID)
                js.Append(".checked==true){")
                js.Append("MOCSwapList.moveDualList( this.form.")
                js.Append(list5.ClientID.ToString)
                js.Append(",")
                js.Append("this.form.")
                js.Append(list1.ClientID.ToString)
                js.Append(",")
                js.Append(moveAll.ToString.ToLower)
                js.Append(",")
                js.Append(sortList.ToString.ToLower)
                js.Append(")}")
                js.Append(" return false;")
            End If
        End If
        Return (js.ToString)
    End Function

    Public Sub HideButtons()
        Try
            Me._btnMoveAll.Visible = False
            Me._btnMoveSelected.Visible = False
            Me._btnRemoveAll.Visible = False
            Me._btnRemoveSelected.Visible = False
        Catch ex As Exception
            RI.SharedFunctions.HandleError("HideButtons", , ex)
        End Try
    End Sub

End Class
