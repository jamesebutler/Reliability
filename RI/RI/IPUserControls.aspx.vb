
Partial Class RI_IPUserControls
    Inherits RIBasePage


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner("IP User Controls")

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sql As String = Resources.Sql.sqlFacility
        Dim ds As System.Data.DataSet = Nothing
        ds = RI.SharedFunctions.GetOracleDataSet(sql)
        If ds IsNot Nothing Then
            With Me._swapListBox
                .DataSource = ds.Tables(0).CreateDataReader
                .DataTextField = "SiteName"
                .DataValueField = "SiteName"
                .DataBind()
            End With
        End If
        'With _ddlTest1
        '    .DataSource = RI.SharedFunctions.GetOracleDataSet("SELECT distinct(cause) FROM(reladmin.tblricause) ORDER by cause")
        '    .DataTextField = "cause"
        '    .DataValueField = "cause"
        '    .DataBind()
        'End With
        'With _ddlTest2
        '    .DataSource = RI.SharedFunctions.GetOracleDataSet("SELECT distinct reason FROM reladmin.tblrireason ORDER by reason")
        '    .DataTextField = "reason"
        '    .DataValueField = "reason"
        '    .DataBind()
        'End With
        'With _ddlTest3
        '    .DataSource = RI.SharedFunctions.GetOracleDataSet("SELECT distinct prevention FROM reladmin.tblriprevention ORDER by prevention")
        '    .DataTextField = "prevention"
        '    .DataValueField = "prevention"
        '    .DataBind()
        'End With
        'With _ddlTest4
        '    .DataSource = RI.SharedFunctions.GetOracleDataSet("SELECT distinct component FROM reladmin.tblricomponent ORDER by component")
        '    .DataTextField = "component"
        '    .DataValueField = "component"
        '    .DataBind()
        'End With
        'With _ddlTest5
        '    .DataSource = RI.SharedFunctions.GetOracleDataSet("SELECT distinct process FROM reladmin.tblriprocess ORDER by process")
        '    .DataTextField = "process"
        '    .DataValueField = "process"
        '    .DataBind()
        'End With
        ds = Nothing
    End Sub

    Protected Sub _displayExcel_DisplayExcel_Click() Handles _displayExcel.DisplayExcel_Click
        Dim sql As String = Resources.Sql.sqlFacility
        Dim ds As System.Data.DataSet = Nothing
        ds = RI.SharedFunctions.GetOracleDataSet(sql)
        Me._displayExcel.DisplayExcel(ds)
        ds = Nothing
    End Sub
End Class
