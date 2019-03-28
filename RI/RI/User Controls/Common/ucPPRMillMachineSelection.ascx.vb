Imports System.Data
Partial Class ucPPRMillMachineSelection
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
        
    Public Sub PopulateMachines(ByVal pprMill As String)
        Dim sql As String = Resources.Sql.sqlPPRMachines
        Dim con As String = ConfigurationManager.ConnectionStrings.Item("dbCnPPR").ConnectionString
        Dim prov As String = ConfigurationManager.ConnectionStrings.Item("dbCnPPR").ProviderName
        Dim ds As DataSet = Nothing
        con = Replace(con, "xx", pprMill)
        ds = RI.SharedFunctions.GetOracleDataSet(sql, con, prov)
        If ds IsNot Nothing Then
            Me._cblMachines.DataSource = ds
            Me._cblMachines.DataTextField = "MACH_DESCRIPTION"
            Me._cblMachines.DataValueField = "MACH_NUMBER"
            Me._cblMachines.DataBind()
        End If
    End Sub
End Class
