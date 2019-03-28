Imports Devart.Data.Oracle
Partial Class RI_Admin_DowntimeMessage
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner("Downtime Message")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
    End Sub
    Private Sub SaveDowntimeMessage()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim showMessage As Integer = 1

        Try
            param = New OracleParameter
            param.ParameterName = "inMessageStartDate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = FormatDateTime(CDate(Me._startEnd.StartDate), DateFormat.ShortDate) & " " & FormatDateTime(CDate(Me._startEnd.StartDate), DateFormat.ShortTime)
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "inMessageEndDate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = FormatDateTime(CDate(Me._startEnd.EndDate), DateFormat.ShortDate) & " " & FormatDateTime(CDate(Me._startEnd.EndDate), DateFormat.ShortTime)
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "inMessage"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me._messageBox.Text
            paramCollection.Add(param)

            If Me._cbShowMessage.Checked = False Then
                showMessage = 0
            End If
            param = New OracleParameter
            param.ParameterName = "inShowMessage"
            param.OracleDbType = OracleDbType.Integer
            param.Direction = Data.ParameterDirection.Input
            param.Value = showMessage
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)
            Dim ret As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "RI.UpdateDowntimeMessage")
            
        Catch ex As Exception
            Throw
        Finally            

        End Try
    End Sub
    Private Sub LoadDowntimeData()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim dr As OracleDataReader = Nothing

        Try            
            param = New OracleParameter
            param.ParameterName = "rsDowntimeMessage"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "Reladmin.RI.GetDowntimeMessage")
            Me._messageBox.Text = String.Empty
            If dr IsNot Nothing Then
                If dr.HasRows Then
                    dr.Read()
                    Me._messageBox.Text = RI.SharedFunctions.DataClean(dr.Item("Message"))
                    Me._startEnd.StartDate = RI.SharedFunctions.DataClean(dr.Item("MessageStartDate"), Now)
                    Me._startEnd.EndDate = RI.SharedFunctions.DataClean(dr.Item("MessageEndDate"), Now)
                    Me._cbShowMessage.Checked = RI.SharedFunctions.DataClean(dr.Item("ShowMessage"), 1)
                End If
            End If
           

        Catch ex As Exception
            Throw
        Finally
            If dr IsNot Nothing Then
                dr.Close()
            End If

        End Try
    End Sub

    Protected Sub _btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnSubmit.Click
        SaveDowntimeMessage()
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If Not Page.IsPostBack Then
            'load downtime data
            LoadDowntimeData()
        End If
    End Sub

    Protected Sub _btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnCancel.Click
        LoadDowntimeData()
    End Sub
End Class
