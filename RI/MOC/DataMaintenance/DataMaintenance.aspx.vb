
Partial Class MOCData_Maintenance
    Inherits RIBasePage
    Dim userProfile As RI.CurrentUserProfile = Nothing
    Dim currentSecurity As clsMOCSecurity

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.ShowMOCMenu()
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("MOC Data Maintenance", True, "MOC"))
        'Dim userProfile = RI.SharedFunctions.GetUserProfile
        'Dim defaults As New RIUserDefaults.CurrentUserDefaults(userProfile.Username, RIUserDefaults.Applications.MOC, ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString)
        '_RIDataMaintenance.ConnectionString = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
        '_RIDataMaintenance.UserName = userProfile.Username
        'If Not Page.IsPostBack Then
        '    If defaults.DoesDefaultValueExist(RIUserDefaults.UserProfileTypes.PlantCode) Then
        '        _RIDataMaintenance.DefaultSite = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.PlantCode).ToString
        '    End If
        'End If
        '    _RIDataMaintenance.PopulateData()
        RI.SharedFunctions.InitializeDataMaintenance(_RIDataMaintenance, Page.IsPostBack)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'userProfile = RI.SharedFunctions.GetUserProfile
        'currentSecurity = New clsMOCSecurity(userProfile.Username, userProfile.DefaultFacility)
        'If currentSecurity.Security.IsAdmin Then
        '    Me._hlMOCTaskTemplates.Visible = True
        'Else
        '    Me._hlMOCTaskTemplates.Visible = False
        'End If
    End Sub
    Protected Sub _RIDataMaintenance_DataMaintenanceLoaded(sender As Object, e As EventArgs) Handles _RIDataMaintenance.DataMaintenanceLoaded
        RI.SharedFunctions.PopulateDataMaintenance(_RIDataMaintenance, Page.IsPostBack)
    End Sub
End Class
