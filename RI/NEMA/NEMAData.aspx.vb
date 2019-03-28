Imports System.Data
Imports Devart.Data.Oracle

Partial Class NEMA_NEMAData
    Inherits System.Web.UI.Page

    Protected Sub _gvNEMAData_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles _gvNEMAData.DataBound
        Try
            Dim dr As OracleDataReader = clsNEMAMotor.GetNEMAUsers(RI.CurrentUserProfile.GetCurrentUser)
            If dr IsNot Nothing AndAlso dr.HasRows Then
                'mzawadsk,rscutel
                Me._gvNEMAData.Columns(3).Visible = True
            Else
                Me._gvNEMAData.Columns(3).Visible = False
            End If
            If dr.IsClosed = False Then dr.Close()
            dr = Nothing
        Catch
            Throw
        End Try
    End Sub

    Protected Sub SqlDataSource1_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSource1.Updating
        e.Cancel = True
    End Sub

    Protected Sub _gvNEMAData_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles _gvNEMAData.RowUpdating
        Dim NEMATYPE As String = e.OldValues.Item("NEMATYPE")
        Dim HP As Decimal = e.OldValues.Item("HP")
        Dim SPEED As Decimal = e.OldValues.Item("SPEED")
        Dim NETPRICE As Decimal = e.NewValues.Item("NETPRICE")
        Dim EFFICIENCY As Decimal = e.NewValues.Item("EFFICIENCY")
        Dim OLDEFFICIENCY As Decimal = e.NewValues.Item("OLDEFFICIENCY")
        Dim FRAMESIZE As String = e.NewValues.Item("FRAMESIZE")
        Dim paramCollection As New OracleParameterCollection
        Dim UpdateParameter As New OracleParameter()

        'With UpdateParameter
        UpdateParameter.ParameterName = "IN_NETPRICE"
        UpdateParameter.Value = FormatNumber(NETPRICE, 2)
        UpdateParameter.OracleDbType = OracleDbType.Number
        UpdateParameter.Direction = Data.ParameterDirection.Input
        paramCollection.Add(UpdateParameter)

        UpdateParameter = New OracleParameter
        UpdateParameter.ParameterName = "IN_EFFICIENCY"
        UpdateParameter.Value = FormatNumber(EFFICIENCY, 2)
        UpdateParameter.OracleDbType = OracleDbType.Number
        UpdateParameter.Direction = Data.ParameterDirection.Input
        paramCollection.Add(UpdateParameter)

        UpdateParameter = New OracleParameter
        UpdateParameter.ParameterName = "IN_OLDEFFICIENCY"
        UpdateParameter.Value = FormatNumber(OLDEFFICIENCY, 2)
        UpdateParameter.OracleDbType = OracleDbType.Number
        UpdateParameter.Direction = Data.ParameterDirection.Input
        paramCollection.Add(UpdateParameter)

        UpdateParameter = New OracleParameter
        UpdateParameter.ParameterName = "IN_FRAMESIZE"
        UpdateParameter.Value = FRAMESIZE
        UpdateParameter.OracleDbType = OracleDbType.NVarChar
        UpdateParameter.Direction = Data.ParameterDirection.Input
        paramCollection.Add(UpdateParameter)

        UpdateParameter = New OracleParameter
        UpdateParameter.ParameterName = "IN_HP"
        UpdateParameter.Value = FormatNumber(HP, 0)
        UpdateParameter.OracleDbType = OracleDbType.Number
        UpdateParameter.Direction = Data.ParameterDirection.Input
        paramCollection.Add(UpdateParameter)

        UpdateParameter = New OracleParameter
        UpdateParameter.ParameterName = "IN_NEMATYPE"
        UpdateParameter.Value = NEMATYPE
        UpdateParameter.OracleDbType = OracleDbType.NVarChar
        UpdateParameter.Direction = Data.ParameterDirection.Input
        paramCollection.Add(UpdateParameter)

        UpdateParameter = New OracleParameter
        UpdateParameter.ParameterName = "IN_SPEED"
        UpdateParameter.Value = FormatNumber(SPEED, 0)
        UpdateParameter.OracleDbType = OracleDbType.Number
        UpdateParameter.Direction = Data.ParameterDirection.Input
        paramCollection.Add(UpdateParameter)

        UpdateParameter = New OracleParameter
        UpdateParameter.ParameterName = "out_status"
        UpdateParameter.OracleDbType = OracleDbType.Number
        UpdateParameter.Direction = Data.ParameterDirection.Output
        paramCollection.Add(UpdateParameter)
        'End With
        Dim retVal As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "RELADMIN.NEMAMOTOR.UPDATENEMADATA")
    End Sub

End Class
