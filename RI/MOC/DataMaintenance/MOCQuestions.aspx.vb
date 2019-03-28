Imports Devart.Data.Oracle
Partial Class MOC_Questions
    Inherits RIBasePage

    Dim userProfile As RI.CurrentUserProfile = Nothing

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.ShowMOCMenu()
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("Questions", True, "MOC"))

        '_lblMainHeading.Text = RI.SharedFunctions.LocalizeValue("The page is used to manage questions that should be applied to MOC's.")

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            userProfile = RI.SharedFunctions.GetUserProfile

            Dim sc As ScriptManager
            sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
            If sc IsNot Nothing Then
                Dim SiteService As New ServiceReference
                SiteService.InlineScript = False
                SiteService.Path = "~/MOCWS.asmx"
                sc.Services.Add(SiteService)
            End If

            If Not Page.IsPostBack Then

                SetDefaults()

            End If

            PopulateClassCat()
        Catch ex As Exception
            Throw New Data.DataException("Page Load", ex)
        End Try
    End Sub
    Private Sub SetDefaults()
        Dim defaults As New RIUserDefaults.CurrentUserDefaults(userProfile.Username, RIUserDefaults.Applications.MOC, ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString)

        If defaults.DoesDefaultValueExist(RIUserDefaults.UserProfileTypes.SiteId) Then
            _hfDefaultBusiness.Value = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.Business).ToString()
            _hfDefaultSiteID.Value = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.SiteId).ToString
            '_ddlFacility.SelectedValue = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.SiteId).ToString
            'Me._cddlFacility.SelectedValue = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.SiteId).ToString
        End If

        _hfUserID.Value = userProfile.Username

        Me._hfAuthLevel.Value = GetAuthLevel(userProfile.Username)

    End Sub
    Private Sub PopulateClassCat()
        Try
            Dim paramCollection As New OracleParameterCollection
            Dim param As New OracleParameter
            Dim ds3 As System.Data.DataSet = Nothing

            paramCollection.Clear()
            param = New OracleParameter
            param.ParameterName = "rsClassification"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsCategory"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            ds3 = RI.SharedFunctions.GetDSFromPackage(paramCollection, "MOCMaint.GetClassCategoryList", "GetClassificationList", 0)
            With Me._ddlClassification
                .DataSource = ds3.Tables(0).CreateDataReader
                .DataTextField = "mocclassification"
                .DataValueField = "mocclassification_seq_id"
                .DataBind()
                '.Items.Insert(0, New ListItem(String.Empty, String.Empty))
            End With

            With Me._ddlCategory
                .DataSource = ds3.Tables(1).CreateDataReader
                .DataTextField = "moccategory"
                .DataValueField = "mocsubcategory_seq_id"
                .DataBind()
            End With

        Catch ex As Exception
            Throw New Data.DataException("PopulateClassCat", ex)
        Finally
        End Try
    End Sub

    Public Function GetAuthLevel(ByVal user As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ret As String = String.Empty
        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = user
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsAuthLevel"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "GetAuthLevel_" & user
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.MOCMaint.GetAuthLevel", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count = 1 Then
                    Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader
                    If dr IsNot Nothing Then
                        If dr.HasRows Then
                            dr.Read()
                            With Me
                                ret = RI.SharedFunctions.DataClean(dr.Item("AuthLevel"))
                            End With
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                ds = Nothing
            End If
            GetAuthLevel = ret
        End Try
    End Function

End Class
