Imports System.Data
Imports Devart.Data.Oracle
Imports RI
Partial Class MOC_Reporting
    Inherits RIBasePage

    Private mCurrentReport As String = ""
    Private mCurrentSortOrder As String = ""

    Public Property ReportInactiveFlag() As String
        Get
            Return Session("ReportInactiveFlag")
        End Get
        Set(ByVal value As String)
            Session.Remove("ReportInactiveFlag")
            Session.Add("ReportInactiveFlag", value)
        End Set
    End Property
    Public Property CurrentReport() As String
        Get
            Return mCurrentReport
        End Get
        Set(ByVal value As String)
            If value <> "ReportSelect" Then
                mCurrentReport = value
            End If
        End Set
    End Property

    Public Property CurrentSortOrder() As String
        Get
            Return mCurrentSortOrder
        End Get
        Set(ByVal value As String)
            'Session.Remove("CurrentSortOrder")
            'Session.Add("CurrentSortOrder", value)
            mCurrentSortOrder = value
        End Set
    End Property


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            Master.ShowMOCMenu()
            Master.SetBanner(Master.RIRESOURCES.GetResourceValue("MOC Reporting", True, "MOC"))
            If Not Page.IsPostBack Then
                PopulateReports(CurrentReport)
            End If
        Catch ex As Exception
            Throw
            'Finally
            '    If ds IsNot Nothing Then ds = Nothing
        End Try
    End Sub

    Private Function GetReportTitlesDSFromPackage() As DataSet
        Dim conCust As Devart.Data.Oracle.OracleConnection = Nothing
        Dim cmdSQL As OracleCommand = Nothing
        Dim connection As String = String.Empty
        Dim provider As String = String.Empty
        Dim ds As DataSet = Nothing
        Dim daData As OracleDataAdapter = Nothing
        Dim cnConnection As OracleConnection = Nothing

        Try
            If Cache.Item("MOC_REPORTTITLES") IsNot Nothing Then
                ds = CType(Cache.Item("MOC_REPORTTITLES"), DataSet)
                Exit Try
            End If
            If connection.Length = 0 Then
                connection = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
            End If
            If provider.Length = 0 Then
                provider = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ProviderName
            End If
            cmdSQL = New OracleCommand
            With cmdSQL
                cnConnection = New OracleConnection(connection)
                cnConnection.Open()
                .Connection = cnConnection
                .CommandText = "MOCReports.ReportTitles"
                .CommandType = CommandType.StoredProcedure
                Dim param As New OracleParameter

                param = New OracleParameter
                param.ParameterName = "rsReportTitles"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = ParameterDirection.Output
                .Parameters.Add(param)

                param = New OracleParameter
                param.ParameterName = "rsReportSortValues"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = ParameterDirection.Output
                .Parameters.Add(param)
            End With

            ds = New DataSet()
            ds.EnforceConstraints = False
            daData = New OracleDataAdapter(cmdSQL)
            daData.Fill(ds)
            ds.EnforceConstraints = True

            If ds IsNot Nothing Then Cache.Insert("MOC_REPORTTITLES", ds, Nothing, DateTime.Now.AddHours(6), TimeSpan.Zero)
        Catch ex As Exception
            Cache.Remove("MOC_REPORTTITLES")
            Throw New DataException("GetReportTitlesDSFromPackage", ex)
            If Not conCust Is Nothing Then conCust = Nothing
            Return Nothing
        Finally
            GetReportTitlesDSFromPackage = ds
            If Not daData Is Nothing Then daData = Nothing
            If Not ds Is Nothing Then ds = Nothing
            If Not cmdSQL Is Nothing Then cmdSQL = Nothing
        End Try
    End Function
    Private Sub PopulateReports(ByVal selectedValue As String)
        Dim ds As DataSet = Nothing
        Dim io As New System.IO.StringWriter

        Try
            ds = GetReportTitlesDSFromPackage()

            If ds IsNot Nothing Then
                Me._ddlReportList.DataTextField = "ReportTitle"
                Me._ddlReportList.DataValueField = "ReportTitle"
                Me._ddlReportList.DataSource = ds.Tables(0).CreateDataReader
                Me._ddlReportList.DataBind()
                Me._ddlReportList.Items.Insert(0, New ListItem("-- Please select a Report --", ""))

                If _ddlReportList.Items.FindByValue(selectedValue) IsNot Nothing Then
                    _ddlReportList.SelectedValue = selectedValue
                End If

            End If
        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then ds = Nothing
        End Try
    End Sub
    Private Sub PopulateSortOrder(Optional ByVal selectedValue As String = "")
        Dim ds As DataSet = Nothing
        Dim sql As String = String.Empty
        Dim rowFilter As String = "ReportTitle='{0}'"
        Dim io As New System.IO.StringWriter

        Try
            ds = GetReportTitlesDSFromPackage()

            If ds IsNot Nothing Then
                ds.Tables(1).DefaultView.RowFilter = String.Format(rowFilter, CurrentReport)
                Me._ddlReportSortValue.DataTextField = "REPORTSORTVALUE"
                Me._ddlReportSortValue.DataValueField = "REPORTNAME"
                Me._ddlReportSortValue.DataSource = ds.Tables(1).DefaultView
                Me._ddlReportSortValue.DataBind()
                ReportInactiveFlag = ds.Tables(1).DefaultView.Item(0).Item("INACTIVE_FLAG").ToString
                Me._ucMOCReports.ReportInactiveFlag = ReportInactiveFlag
                If _ddlReportSortValue.Items.FindByValue(selectedValue) IsNot Nothing Then
                    _ddlReportSortValue.SelectedValue = selectedValue
                Else
                    If _ddlReportSortValue.Items.Count > 0 Then
                        _ddlReportSortValue.SelectedIndex = 0
                    End If
                End If
                Me._ucMOCReports.ReportTitle = Me._ddlReportList.SelectedValue
                Me._ucMOCReports.ReportBOETitle = Me._ddlReportSortValue.SelectedValue
                Me._ucMOCReports.ReportSortText = Replace(Me._ddlReportSortValue.SelectedItem.Text, " ", "")

            End If
        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then ds = Nothing
        End Try
    End Sub

    Protected Sub _ddlReportSortValue_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlReportSortValue.DataBound
        Dim ddl As New DropDownList
        For i As Integer = 0 To _ddlReportSortValue.Items.Count - 1
            If ddl.Items.FindByText(Me._ddlReportSortValue.Items(i).Text) Is Nothing Then
                If (Me._ddlReportSortValue.Items(i).Text.Length > 0) Then
                    ddl.Items.Add(New ListItem(Me._ddlReportSortValue.Items(i).Text, Me._ddlReportSortValue.Items(i).Value))
                End If
            End If
        Next
        If ddl.Items.Count > 0 Then
            _ddlReportSortValue.Items.Clear()
            For i As Integer = 0 To ddl.Items.Count - 1
                _ddlReportSortValue.Items.Add(New ListItem(ddl.Items(i).Text, ddl.Items(i).Value))
            Next
        Else
            'Let's hide the sort Order option
            Me._lblReportSortValue.Visible = False
            Me._ddlReportSortValue.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim targetEvent As String()
        Dim selectedValue As String = String.Empty
        Dim sortValue As String = String.Empty

        If Page.IsPostBack And Request.Form("__EventTarget") IsNot Nothing Then
            If Request.Form("__EventTarget").Contains("ddlReportList") Then
                targetEvent = Request.Form("__EventTarget").Split("$")
                CurrentReport = Request.Form(Request.Form("__EventTarget"))
            Else
                CurrentReport = Request.Form(Me._ddlReportList.UniqueID)
            End If
            If Request.Form("__EventTarget").Contains("_ddlReportSortValue") Then
                targetEvent = Request.Form("__EventTarget").Split("$")
                sortValue = Request.Form(Request.Form("__EventTarget"))
            Else
                sortValue = Request.Form(Me._ddlReportSortValue.UniqueID)
            End If

            PopulateReports(CurrentReport)
            _ddlReportList_SelectedIndexChanged(CurrentReport, sortValue)
            'If CurrentReport <> "MOC Listing" Then
            '    Me.ucMOCReport.Visible = True
            '    'ElseIf CurrentReport = "Management Summary" Then
            '    '    Me._lblReportSortValue.Visible = False
            '    '    Me._ddlReportSortValue.Visible = False
            'End If
            If CurrentReport.Length > 0 Then
                Me._ucMOCReports.ReportBOETitle = sortValue
                Me._ucMOCReports.ReportTitle = CurrentReport
                Me._ucMOCReports.ReportSortText = Me._ddlReportSortValue.SelectedItem.Text
                'Me._ucMOCReports.ReportSortValue = Request.Form(Me._ddlReportSortValue.UniqueID)
                Me._ucMOCReports.ReportInactiveFlag = ReportInactiveFlag
                'Me._ucMOCReports.Visible = True
            End If
        End If
    End Sub

    Protected Sub _ddlReportList_SelectedIndexChanged(ByVal SelectedValue As String, ByVal SortValue As String) '(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlReportList.SelectedIndexChanged
        If SelectedValue.Length > 0 Then
            Me._ddlReportSortValue.Visible = True
            Me._lblReportSortValue.Visible = True
            Me._ucMOCReports.Visible = True

            PopulateSortOrder(SortValue)
        Else
            Me._ddlReportSortValue.Visible = False
            Me._lblReportSortValue.Visible = False
            Me._ucMOCReports.Visible = False
        End If
        If RI.SharedFunctions.CausedPostBack(Me._ddlReportList.UniqueID) Then
            Me._upReporting.Update()
        End If
    End Sub

    Protected Sub _ddlReportSortValue_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlReportSortValue.SelectedIndexChanged
        Me._ucMOCReports.ReportSortText = Replace(Me._ddlReportSortValue.SelectedItem.Text, " ", "")
    End Sub
End Class
