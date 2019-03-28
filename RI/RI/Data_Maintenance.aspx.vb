Imports IP.Bids.Localization

Partial Class Data_Maintenance
    Inherits RIBasePage

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        'Master.PageName = Master.GetLocalizedValue("Data Maintenance", False)
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("Data Maintenance"))
        RI.SharedFunctions.InitializeDataMaintenance(_RIDataMaintenance, Page.IsPostBack)
        'Dim userProfile = RI.SharedFunctions.GetUserProfile
        'Dim defaults As New RIUserDefaults.CurrentUserDefaults(userProfile.Username, RIUserDefaults.Applications.RI, ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString)
        '_RIDataMaintenance.ConnectionString = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
        '_RIDataMaintenance.UserName = userProfile.Username
        '_RIDataMaintenance.PopulateData()
        'If Not Page.IsPostBack Then
        '    If defaults.DoesDefaultValueExist(RIUserDefaults.UserProfileTypes.PlantCode) Then
        '        _RIDataMaintenance.DefaultSite = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.PlantCode).ToString
        '    End If
        'End If

        'Dim appList = _RIDataMaintenance.GetApplicationList
        'For Each item In appList
        '    item.ApplicationName = RI.SharedFunctions.LocalizeValue(item.ApplicationName, True)
        'Next
        'Dim maintenanceFunctions = _RIDataMaintenance.GetMaintenanceFunctions
        'For Each item In maintenanceFunctions
        '    item.PageName = RI.SharedFunctions.LocalizeValue(item.PageName, True)
        '    item.Description = RI.SharedFunctions.LocalizeValue(item.Description, True)
        'Next
        '_RIDataMaintenance.SiteText = RI.SharedFunctions.LocalizeValue(_RIDataMaintenance.SiteText)
        '_RIDataMaintenance.ApplicationText = RI.SharedFunctions.LocalizeValue(_RIDataMaintenance.ApplicationText)
        '_RIDataMaintenance.FunctionName = RI.SharedFunctions.LocalizeValue(_RIDataMaintenance.FunctionName, True)
        '_RIDataMaintenance.FunctionNameDescription = RI.SharedFunctions.LocalizeValue(_RIDataMaintenance.FunctionNameDescription, True)
        '_RIDataMaintenance.PopulateMaintenanceTable(maintenanceFunctions)
        '_RIDataMaintenance.PopulateApplicationList(appList)
    End Sub

    Protected Sub _RIDataMaintenance_DataMaintenanceLoaded(sender As Object, e As EventArgs) Handles _RIDataMaintenance.DataMaintenanceLoaded
        RI.SharedFunctions.PopulateDataMaintenance(_RIDataMaintenance, Page.IsPostBack)
    End Sub
End Class
