
Partial Class RI_DataMaintenance_DataMaintenance
    Inherits RIBasePage

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        'Master.PageName = Master.GetLocalizedValue("Data Maintenance", False)
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("Data Maintenance"))
        Dim userProfile = RI.SharedFunctions.GetUserProfile
        Dim defaults As New RIUserDefaults.CurrentUserDefaults(userProfile.Username, RIUserDefaults.Applications.RI, ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString)
        _RIDataMaintenance.ConnectionString = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
        _RIDataMaintenance.UserName = userProfile.Username
        If Not Page.IsPostBack Then
            If defaults.DoesDefaultValueExist(RIUserDefaults.UserProfileTypes.PlantCode) Then
                _RIDataMaintenance.DefaultSite = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.PlantCode).ToString
            End If
        End If
        _RIDataMaintenance.PopulateData()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub
End Class
