Imports Devart.Data.Oracle
Partial Class RI_Help_AuditTracker
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner("Audit Tracker")
    End Sub
   
    Private Sub PopulateAuditEvents()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim dr As OracleDataReader = Nothing

        Try          
            param = New OracleParameter
            param.ParameterName = "rsAuditEvents"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "Reladmin.RIAudit.GetAuditEvents")
            If dr IsNot Nothing Then
                Me._cblEvents.Items.Clear()
                Me._cblEvents.DataSource = dr
                Me._cblEvents.DataValueField = "PROC_NAME"
                Me._cblEvents.DataTextField = "PROC_NAME"
                Me._cblEvents.DataBind()
                'Me._cblEvents.Items.Insert(0, "All")
            End If
        Catch ex As Exception
            RI.SharedFunctions.HandleError("PopulateAuditEvents", , ex)
        Finally
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
        End Try
    End Sub

    Private Sub GetAuditErrorRecords()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim dr As OracleDataReader = Nothing

        Try
            param = New OracleParameter
            param.ParameterName = "in_StartDate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me._dateRange.StartDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_EndDate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me._dateRange.EndDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_EventName"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            Dim Events As String = RI.SharedFunctions.GetCheckBoxValues(_cblEvents)
            If Events.Length > 0 Then
                param.Value = Replace(Events, ",", "','").ToUpper
            Else
                param.Value = ""
            End If
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_ErrDesc"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me._txtDesc.Text
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsGetAuditRecords"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "Reladmin.RIAudit.GetAuditErrorRecords")
            If dr IsNot Nothing Then
                Me._grvAudit.DataSource = dr
                Me._grvAudit.DataBind()
            End If
        Catch ex As Exception
            dr = Nothing
            RI.SharedFunctions.HandleError("GetAuditErrorRecords", , ex)
        Finally
            If Not dr Is Nothing Then
                dr.Close()
                dr = Nothing
            End If

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            PopulateAuditEvents()
        End If
    End Sub

    Protected Sub _btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnSearch.Click
        GetAuditErrorRecords()
    End Sub
End Class
