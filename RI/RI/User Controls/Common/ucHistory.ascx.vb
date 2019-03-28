Imports System.Data
Imports Devart.Data.Oracle


Partial Class RI_User_Controls_Common_ucHistory
    Inherits System.Web.UI.UserControl

    Dim RILocalize As New IP.Bids.Localization.WebLocalization
    'Dim iploc As New IP.Bids.Localization.DataLocalization

    Private mRINumber As String = String.Empty
    Private mApplid As String = String.Empty
    Public Property RINumber() As String
        Get
            Return mRINumber
        End Get
        Set(ByVal value As String)
            mRINumber = value
        End Set
    End Property
    Public Property Applid() As String
        Get
            Return mApplid
        End Get
        Set(ByVal value As String)
            mApplid = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        RefreshHistory()
    End Sub

    Public Sub RefreshHistory()
        If RINumber.Length > 0 Then
            GetHistory()
            If Me._grvHistory.Rows.Count = 0 Then
                Me._btnHistory.Enabled = False
                Me._btnHistory.ToolTip = RI.SharedFunctions.LocalizeValue("NoRecordsFound")
            End If
            Me.Visible = True
        Else
            Me.Visible = False
        End If
    End Sub
    Private Sub GetHistory()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters

        Try
            param = New OracleParameter
            param.ParameterName = "in_rinumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = riNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsGetAuditRecords"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            If Applid = "Outage" Then
                Dim key As String = "GetOutageAuditRecords_" & RINumber
                ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RIAUDIT.GetOutageAuditRecords", key, 0)
            Else
                Dim key As String = "GetAuditRecords_" & RINumber
                ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RIAUDIT.GetAuditRecords", key, 0)
            End If

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    'History                     
                    Dim dr As DataTableReader = ds.Tables(0).CreateDataReader
                    Dim excludedFields As New ArrayList()
                    excludedFields.Add("RINumber")
                    Dim RIData As New IP.Bids.Localization.DataLocalization(RILocalize)
                    dr = RIData.LocalizeData(dr, excludedFields)
                    Me._grvHistory.DataSource = dr
                    _grvHistory.AutoGenerateColumns = False
                    _grvHistory.DataBind()
                    'If _grvHistory.Rows.Count > 9 Then
                    Me._pnlBody.Height = "300"
                    'Else
                    '   Me._pnlBody.Height = "75"
                    'End If
                End If
            End If

        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                ds = Nothing
            End If

        End Try
    End Sub

    Protected Sub _grvHistory_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _grvHistory.RowDataBound
        Dim historymsg As String = String.Empty
        Try
            If e.Row.RowIndex > 0 Then
                If RILocalize.CurrentLocale.ToUpper <> "EN-US" Then
                    If e.Row.Cells(3).Text.Contains("Outage Date Change") Then
                        historymsg = RI.SharedFunctions.LocalizeValue("OUTAGE DATES HAVE CHANGED") & ": " & RI.SharedFunctions.LocalizeValue("PrevStartDate") & ": " & IP.Bids.Localization.DateTime.GetLocalizedDateTime(RI.SharedFunctions.CDateFromEnglishDate(e.Row.Cells(3).Text.Substring(InStr(1, e.Row.Cells(3).Text, "PrevStartDate=") + 13, 10)), RILocalize.CurrentLocale, "d")
                        historymsg = historymsg & "  " & RI.SharedFunctions.LocalizeValue("NewStartDate") & ": " & IP.Bids.Localization.DateTime.GetLocalizedDateTime(RI.SharedFunctions.CDateFromEnglishDate(e.Row.Cells(3).Text.Substring(InStr(1, e.Row.Cells(3).Text, "NewStartDate=") + 12, 10)), RILocalize.CurrentLocale, "d")
                        historymsg = historymsg & "  " & RI.SharedFunctions.LocalizeValue("PrevEndDate") & ": " & IP.Bids.Localization.DateTime.GetLocalizedDateTime(RI.SharedFunctions.CDateFromEnglishDate(e.Row.Cells(3).Text.Substring(InStr(1, e.Row.Cells(3).Text, "PrevEndDate=") + 11, 10)), RILocalize.CurrentLocale, "d")
                        historymsg = historymsg & "  " & RI.SharedFunctions.LocalizeValue("NewEndDate") & ": " & IP.Bids.Localization.DateTime.GetLocalizedDateTime(RI.SharedFunctions.CDateFromEnglishDate(e.Row.Cells(3).Text.Substring(InStr(1, e.Row.Cells(3).Text, "NewEndDate=") + 10)), RILocalize.CurrentLocale, "d")
                        e.Row.Cells(3).Text = historymsg
                    End If
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
