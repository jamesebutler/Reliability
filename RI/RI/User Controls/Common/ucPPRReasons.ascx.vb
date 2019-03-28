Imports Devart.Data.Oracle
Partial Class ucPPRReasons
    Inherits System.Web.UI.UserControl
    Private Structure TabList
        Const SlabLoss As String = "Slab Loss"
        Const Downtime As String = "PPR Downtime"
        Const PaperLoss As String = "Paper Loss"
        Private Active As Boolean
    End Structure
    Public Event SelectedIndexChanged()

    Public ReadOnly Property PPRReason() As String
        Get

            Return _selectedReason.Value
        End Get

    End Property

    'Private Function GetSelectedValue() As String

    'End Function
    Private Function GetPPRReasonList() As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New OracleParameter
            param.ParameterName = "rsReasons"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "PPRReasons"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "RI.PPRReasons", key, 3)

            'Me._udpAnalysisState.Update()
        Catch ex As Exception
            Throw
        Finally
            GetPPRReasonList = ds
            If ds IsNot Nothing Then
                ds = Nothing
            End If

        End Try
    End Function
    Private Sub PopulateList()
        Dim ds As Data.DataSet
        ds = Me.GetPPRReasonList
        ds.Tables(0).DefaultView.RowFilter = "reportparametertype='" & TabList.Downtime & "'"
        If ds IsNot Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                Me._rblDowntime.DataSource = ds.Tables(0).DefaultView
                Me._rblDowntime.DataValueField = "ReportParameters"
                Me._rblDowntime.DataTextField = "ReportParameters"
                Me._rblDowntime.RepeatDirection = RepeatDirection.Horizontal
                Me._rblDowntime.DataBind()
            End If
        End If
        ds.Tables(0).DefaultView.RowFilter = "reportparametertype='" & TabList.PaperLoss & "'"
        If ds IsNot Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                Me._rblPaperLoss.DataSource = ds.Tables(0).DefaultView
                Me._rblPaperLoss.DataValueField = "ReportParameters"
                Me._rblPaperLoss.DataTextField = "ReportParameters"
                Me._rblPaperLoss.RepeatDirection = RepeatDirection.Horizontal
                Me._rblPaperLoss.DataBind()
            End If
        End If
        ds.Tables(0).DefaultView.RowFilter = "reportparametertype='" & TabList.SlabLoss & "'"
        If ds IsNot Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                Me._rblSlabLoss.DataSource = ds.Tables(0).DefaultView
                Me._rblSlabLoss.DataValueField = "ReportParameters"
                Me._rblSlabLoss.DataTextField = "ReportParameters"
                Me._rblSlabLoss.RepeatDirection = RepeatDirection.Horizontal
                Me._rblSlabLoss.DataBind()
            End If
        End If

    End Sub
    Public Sub Populate()
        If Page.EnableViewState = False Then
            PopulateList()
            SetControlState()
        Else
            If Not Page.IsPostBack Then
                PopulateList()
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
    End Sub
    Private Sub SetControlState()
        If RI.SharedFunctions.CausedPostBack(Me._rblDowntime.ID) Then
            If Request.Form(_rblDowntime.UniqueID) IsNot Nothing Then
                If Me._rblDowntime.Items.FindByValue(Request.Form(_rblDowntime.UniqueID)) IsNot Nothing Then
                    _rblDowntime.Items.FindByValue(Request.Form(_rblDowntime.UniqueID)).Selected = True
                End If
                _selectedReason.Value = Request.Form(_rblDowntime.UniqueID)
                Tabs.ActiveTabIndex = 0
            End If
        End If

        If RI.SharedFunctions.CausedPostBack(Me._rblPaperLoss.ID) Then
            If Request.Form(_rblPaperLoss.UniqueID) IsNot Nothing Then
                If Me._rblPaperLoss.Items.FindByValue(Request.Form(_rblPaperLoss.UniqueID)) IsNot Nothing Then
                    _rblPaperLoss.Items.FindByValue(Request.Form(_rblPaperLoss.UniqueID)).Selected = True
                End If
                _selectedReason.Value = Request.Form(_rblPaperLoss.UniqueID)
                Tabs.ActiveTabIndex = 1
            End If
        End If

        If Request.Form(_rblSlabLoss.UniqueID) IsNot Nothing Then
            If RI.SharedFunctions.CausedPostBack(Me._rblSlabLoss.ID) Then
                If Me._rblSlabLoss.Items.FindByValue(Request.Form(_rblSlabLoss.UniqueID)) IsNot Nothing Then
                    _rblSlabLoss.Items.FindByValue(Request.Form(_rblSlabLoss.UniqueID)).Selected = True
                End If
                _selectedReason.Value = Request.Form(_rblSlabLoss.UniqueID)
                Tabs.ActiveTabIndex = 2
            End If
        End If
    End Sub
    Private Sub ClearSelections(ByVal excludedControl As String)
        If excludedControl = TabList.Downtime Then
            For i As Integer = 0 To Me._rblPaperLoss.Items.Count - 1
                _rblPaperLoss.Items(i).Selected = False
            Next
            For i As Integer = 0 To Me._rblSlabLoss.Items.Count - 1
                _rblSlabLoss.Items(i).Selected = False
            Next
        ElseIf excludedControl = TabList.PaperLoss Then
            For i As Integer = 0 To Me._rblDowntime.Items.Count - 1
                _rblDowntime.Items(i).Selected = False
            Next
            For i As Integer = 0 To Me._rblSlabLoss.Items.Count - 1
                _rblSlabLoss.Items(i).Selected = False
            Next
        ElseIf excludedControl = TabList.SlabLoss Then
            For i As Integer = 0 To Me._rblDowntime.Items.Count - 1
                _rblDowntime.Items(i).Selected = False
            Next
            For i As Integer = 0 To Me._rblPaperLoss.Items.Count - 1
                _rblPaperLoss.Items(i).Selected = False
            Next
        End If
    End Sub
    Protected Sub _rblDowntime_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _rblDowntime.SelectedIndexChanged
        _selectedReason.Value = Me._rblDowntime.SelectedValue
        ClearSelections(TabList.Downtime)
        RaiseEvent SelectedIndexChanged()
    End Sub

    Protected Sub _rblPaperLoss_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _rblPaperLoss.SelectedIndexChanged
        _selectedReason.Value = Me._rblPaperLoss.SelectedValue
        ClearSelections(TabList.PaperLoss)
        RaiseEvent SelectedIndexChanged()
    End Sub

    Protected Sub _rblSlabLoss_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _rblSlabLoss.SelectedIndexChanged
        _selectedReason.Value = Me._rblSlabLoss.SelectedValue
        ClearSelections(TabList.SlabLoss)
        RaiseEvent SelectedIndexChanged()
    End Sub
End Class
