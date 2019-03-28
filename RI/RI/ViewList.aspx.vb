Imports Devart.Data.Oracle

Partial Class RI_ViewList2
    Inherits RIBasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DisplayViewSearch()
    End Sub
    Private Sub DisplayViewSearch()
        Dim dr As OracleDataReader = Nothing
        Try
            Dim clsSearch As clsViewSearch = CType(Session("clsSearch"), clsViewSearch)
            If clsSearch IsNot Nothing And Not Page.IsPostBack Then
                dr = clsSearch.Search
                Dim drnew As Data.DataTableReader
                Dim sExclude As New ArrayList
                Dim RIResources As New IP.Bids.Localization.WebLocalization
                Dim ipLoc As New IP.Bids.Localization.DataLocalization(RIResources)
                If dr IsNot Nothing Then
                    sExclude.Add("RINumber")
                    sExclude.Add("Incident")
                    sExclude.Add("RCFA_TYPE")
                    drnew = ipLoc.LocalizeData(dr, sExclude)
                    If drnew.HasRows Then
                        'Me._rplIncidentList.DataSource = drnew
                        'Me._rplIncidentList.DataBind()
                        _gvIncidentListing.DataSource = drnew
                        _gvIncidentListing.DataBind()
                        clsSearch.SearchData = Nothing
                        'If _gvIncidentListing.Rows.Count > 9 Then
                        '    _pnlIncidentListing.Height = CType("300", Unit)
                        'End If
                        'If _gvIncidentListing.Rows.Count > 0 Then
                        '    Me._lblRecordCount.Text = "Record Count:"
                        '    Me._lblRecCount.Text = _gvIncidentListing.Rows.Count
                        'Else
                        '    Me._lblRecordCount.Text = "No Records Found"
                        '    Me._lblRecCount.Text = ""
                        'End If
                        _pnlIncidentListing.Visible = True
                        'Me._pnlViewSearch.Visible = True
                        'Response.Flush()
                    End If
                End If
            End If
        Catch ex As Exception

        Finally
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
        End Try
    End Sub
End Class
