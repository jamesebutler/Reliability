Imports Devart.Data.Oracle

Partial Class ucMOCClass
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

    Public Property Classification() As String
        Get
            If DisplayMode = MOCMode.Search Then
                Return RI.SharedFunctions.GetCheckBoxValues(_cblClass)
            Else
                Return _rblClass.SelectedValue
            End If
        End Get
        Set(ByVal value As String)
            If DisplayMode = MOCMode.Search Then
                RI.SharedFunctions.SetCheckBoxValues(_cblClass, value)
            Else
                If _rblClass.Items.FindByValue(value) IsNot Nothing Then
                    _rblClass.SelectedValue = value
                End If
            End If
        End Set

    End Property

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        PopulateClass()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "MOC") Then
            Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "MOC", Page.ResolveClientUrl("~/MOC/MOC.js"))
        End If
        _cblClass.Width = Unit.Percentage(100)
        _cblClass.RepeatDirection = RepeatDirection.Vertical
        _cblClass.RepeatColumns = "10"
    End Sub
    Public Sub RefreshDisplay()
        PopulateClass()
    End Sub
    Private Sub DisplayClass()

        If Me.DisplayMode = MOCMode.Enter Then
            Me._cblClass.Visible = False
            Me._rblClass.Visible = True
        Else
            Me._cblClass.Visible = True
            Me._rblClass.Visible = False
        End If
    End Sub
    Private Sub PopulateClass()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim AllFlag As Boolean
        Dim ipLoc As New IP.Bids.Localization.WebLocalization()

        'Check input paramaters

        Try
            param = New OracleParameter
            param.ParameterName = "rsMOCClass"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "ViewMOC.DropdownClass"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.ViewMOC.ViewDropdownClass", key, 3)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 0 Then
                    _cblClass.DataSource = ds.Tables(0).CreateDataReader
                    _cblClass.DataTextField = "mocclassification"
                    _cblClass.DataValueField = "mocclassification"
                    _cblClass.DataBind()
                End If
            End If

            If DisplayMode = MOCMode.Search Then AllFlag = True
            If AllFlag = True Then
                _cblClass.Items.Insert(0, "All")
                _cblClass.Attributes.Add("onClick", "unCheckNo(this," & ds.Tables(0).Rows.Count + 1 & ");")
                If _cblClass.SelectedIndex < 0 Then
                    If _cblClass.Items.FindByValue("All") IsNot Nothing Then
                        _cblClass.Items.FindByValue("All").Selected = True
                    End If
                End If
                ipLoc.LocalizeListControl(_cblClass)
            Else
                _rblClass.RepeatDirection = RepeatDirection.Horizontal
                _rblClass.DataSource = ds.Tables(0).CreateDataReader
                _rblClass.DataTextField = "mocclassification"
                _rblClass.DataValueField = "mocclassification"
                _rblClass.DataBind()
                ipLoc.LocalizeListControl(_rblClass)
            End If


            Me.DisplayClass()

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

    'Private Sub PopulateCategory()
    '    Dim AllFlag As Boolean
    '    Dim li As New OrderedDictionary  'Hashtable

    '    If DisplayMode = MOCMode.Search Then AllFlag = True
    '    'ViewUpdate = New clsViewMOC()

    '    'If ViewUpdate IsNot Nothing Then
    '    'End If


    '    GetData()

    '    If AllFlag = True Then
    '        li.Insert(0, "All", "All")
    '        '_cblCategory.RepeatDirection = RepeatDirection.Horizontal
    '        'RI.SharedFunctions.BindList(_cblCategory, li, False, False)
    '        _cblCategory.Attributes.Add("onClick", "unCheckNo(this," & li.Count & ");")
    '        If _cblCategory.SelectedIndex < 0 Then
    '            If _cblCategory.Items.FindByValue("All") IsNot Nothing Then
    '                _cblCategory.Items.FindByValue("All").Selected = True
    '            End If
    '        End If
    '    End If

    'End Sub

End Class
