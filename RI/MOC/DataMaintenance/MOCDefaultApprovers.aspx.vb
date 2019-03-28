Imports Devart.Data.Oracle
Partial Class MOC_DefaultApprovers
    Inherits RIBasePage

    'Dim selectedFacility As String = String.Empty
    Dim userProfile As RI.CurrentUserProfile = Nothing

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.ShowMOCMenu()
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("default approvers", True, "MOC"))

        _lblMainHeading.Text = RI.SharedFunctions.LocalizeValue("The page is used to select which default approvers will show up when an MOC is initiated.")

        _lblHeading.Text = RI.SharedFunctions.LocalizeValue("Default Approver Maintenance for ")

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            userProfile = RI.SharedFunctions.GetUserProfile

            Dim sc As ScriptManager
            sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
            If sc IsNot Nothing Then
                Dim SiteService As New ServiceReference
                SiteService.InlineScript = False
                SiteService.Path = "~/RIMOCSharedWS.asmx"
                sc.Services.Add(SiteService)
            End If

            If Not Page.IsPostBack Then
                _cddlFacility.SelectedValue = userProfile.DefaultFacility
                _cddlFacility1.SelectedValue = userProfile.DefaultFacility
                _cddlBusUNit.SelectedValue = "All"
                _cddlArea.SelectedValue = "All"
                _cddlLineBreak.SelectedValue = "All"
                If _rblMaintType.SelectedValue = "Business Unit" Then
                    PopulateNotificationList(userProfile.DefaultFacility)
                End If
                PopulateClassCat()

            Else
                _cddlFacility.SelectedValue = Me._ddlFacility.SelectedValue
                'PopulateClassCat()
                '_ddlFacility.SelectedValue = userProfile.DefaultFacility
                If _rblMaintType.SelectedValue = "Classification" Then
                    GetClassData()
                    'PopulateFacility()
                ElseIf _rblMaintType.SelectedValue = "Category" Then
                    GetCategoryData()
                    Me._tcCategory.Visible = True
                    Me._tcBusinessUnit.Visible = False
                    Me._tcArea.Visible = False
                    Me._tcLine.Visible = False
                Else
                    PopulateNotificationList(Me._ddlFacility.SelectedValue)
                End If
            End If

            Dim authlevel As String
            authlevel = GetAuthLevel(userProfile.Username, Me._cddlFacility.SelectedValue)
            'authlevel = GetAuthLevel("YBROOKS", Me._ddlFacility.SelectedValue)
            If authlevel = "MILLADMIN" Then
                Me._btnAdd.Visible = True
                Me._tblApprover.Visible = True
                Me._gvInformed.Columns(5).Visible = True
                'Me._gvL1Approvers.Columns(5).Visible = True
                Me._gvL1.Columns(5).Visible = True
                Me._gvL2.Columns(5).Visible = True
                Me._gvL3.Columns(5).Visible = True
                'Me._gvL2Approvers.Columns(5).Visible = True
                'Me._gvL3Approvers.Columns(5).Visible = True
                Me._gvCategory.Columns(4).Visible = True
                Me._gvClass.Columns(4).Visible = True
            Else
                Me._btnAdd.Visible = False
                Me._tblApprover.Visible = False
                Me._gvCategory.Columns(4).Visible = False
                Me._gvClass.Columns(4).Visible = False
                Me._gvInformed.Columns(5).Visible = False
                Me._gvL1.Columns(5).Visible = False
                Me._gvL2.Columns(5).Visible = False
                Me._gvL3.Columns(5).Visible = False
                'Me._gvL1Approvers.Columns(5).Visible = False
                'Me._gvL2Approvers.Columns(5).Visible = False
                'Me._gvL3Approvers.Columns(5).Visible = False
            End If

        Catch ex As Exception
            Throw New Data.DataException("Page Load", ex)
        End Try
    End Sub

    Protected Sub _btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnAdd.Click
        If _rblMaintType.SelectedValue = "Classification" Then
            SaveClassNotification()
        ElseIf _rblMaintType.SelectedValue = "Category" Then
            SaveCatNotification()
        Else
            UpdateBUANotification()
            PopulateNotificationList(Me._ddlFacility.SelectedValue)
        End If
    End Sub
    Private Sub PopulateClassCat()
        Try
            Dim paramCollection As New OracleParameterCollection
            Dim param As New OracleParameter
            Dim ds3 As System.Data.DataSet = Nothing

            param = New OracleParameter
            param.ParameterName = "in_SiteID"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = _cddlFacility.SelectedValue
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

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

            ds3 = RI.SharedFunctions.GetDSFromPackage(paramCollection, "MOCMaint.GetSiteClassCategoryList", "GetClassificationList", 0)
            With Me._ddlClass
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

    'Private Sub PopulateNotificationList(ByVal SiteID As String)
    '    ', ByVal busunit As String, ByVal area As String, ByVal line As String)
    '    Try
    '        Dim paramCollection As New OracleParameterCollection
    '        Dim param As New OracleParameter
    '        Dim ds As System.Data.DataSet = Nothing
    '        Dim ds2 As System.Data.DataSet = Nothing
    '        Dim ds3 As System.Data.DataSet = Nothing
    '        Dim ds4 As System.Data.DataSet = Nothing

    '        'Get Initial list of people based on site for the MOC
    '        param = New OracleParameter
    '        param.ParameterName = "in_siteid"
    '        param.OracleDbType = OracleDbType.VarChar
    '        param.Value = SiteID
    '        param.Direction = Data.ParameterDirection.Input
    '        paramCollection.Add(param)

    '        param = New OracleParameter
    '        param.ParameterName = "rsPerson"
    '        param.OracleDbType = OracleDbType.Cursor
    '        param.Direction = Data.ParameterDirection.Output
    '        paramCollection.Add(param)

    '        ds2 = RI.SharedFunctions.GetDSFromPackage(paramCollection, "RI.PersonDDL", "MOCPerson_" & SiteID, 3)

    '        'If ds2 IsNot Nothing Then
    '        '    With Me._slbNotification
    '        '        .DataSource = ds2.Tables(0).CreateDataReader
    '        '        .DataTextField = "Person"
    '        '        .DataValueField = "username"
    '        '        '.EndorserSelectedDataSource = Nothing
    '        '        '.SelectedDataSource = Nothing
    '        '        '.SecondarySelectedDataSource = Nothing
    '        '        '.ThirdSelectedDataSource = Nothing
    '        '        .DataBind()
    '        '    End With
    '        'End If
    '        paramCollection.Clear()

    '        param = New OracleParameter
    '        param.ParameterName = "rsClassification"
    '        param.OracleDbType = OracleDbType.Cursor
    '        param.Direction = Data.ParameterDirection.Output
    '        paramCollection.Add(param)

    '        param = New OracleParameter
    '        param.ParameterName = "rsCategory"
    '        param.OracleDbType = OracleDbType.Cursor
    '        param.Direction = Data.ParameterDirection.Output
    '        paramCollection.Add(param)

    '    Catch ex As Exception
    '        Throw
    '    End Try

    'End Sub
    'Private Sub GetClassRecords()
    '    Dim dr As OracleDataReader = Nothing

    '    dr = GetClassRecordsDRFromPackage()

    '    If dr IsNot Nothing Then

    '        With Me._gvClass
    '            .DataSource = dr
    '            .DataBind()
    '        End With
    '    End If

    'End Sub
    'Private Function GetRecordsDRFromPackage() As OracleDataReader
    '    Dim dr As OracleDataReader = Nothing
    '    Dim paramCollection As New OracleParameterCollection

    '    Try

    '        Dim param As New OracleParameter

    '        paramCollection.Clear()
    '        param = New OracleParameter
    '        param.ParameterName = "in_ClassSeqID"
    '        param.OracleDbType = OracleDbType.VarChar
    '        param.Value = Me._ddlClass.SelectedValue
    '        param.Direction = Data.ParameterDirection.Input
    '        paramCollection.Add(param)

    '        param = New OracleParameter
    '        param.ParameterName = "in_SiteID"
    '        param.OracleDbType = OracleDbType.VarChar
    '        param.Value = _ddlFacility.SelectedValue
    '        param.Direction = Data.ParameterDirection.Input
    '        paramCollection.Add(param)

    '        param = New OracleParameter
    '        param.ParameterName = "rsClassList"
    '        param.OracleDbType = OracleDbType.Cursor
    '        param.Direction = Data.ParameterDirection.Output
    '        paramCollection.Add(param)

    '        dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "MOCMaint.GetClassNotificationList")
    '    Catch ex As Exception
    '        Throw New Data.DataException("GetClassNotificationList", ex)
    '        Return Nothing
    '    Finally
    '        GetRecordsDRFromPackage = dr
    '    End Try
    'End Function

    'Private Sub SaveNotification(ByVal strSiteid As String, ByVal strClass As String, ByVal strNotificationList As String, ByVal strNotifyType As String)
    '    Dim paramCollection As New OracleParameterCollection
    '    Dim param As New OracleParameter
    '    Dim showMessage As Integer = 1

    '    Try
    '        param = New OracleParameter
    '        param.ParameterName = "in_SiteId"
    '        param.OracleDbType = OracleDbType.VarChar
    '        param.Direction = Data.ParameterDirection.Input
    '        param.Value = strSiteid
    '        paramCollection.Add(param)

    '        param = New OracleParameter
    '        param.ParameterName = "in_Classification"
    '        param.OracleDbType = OracleDbType.VarChar
    '        param.Direction = Data.ParameterDirection.Input
    '        param.Value = strClass
    '        paramCollection.Add(param)

    '        param = New OracleParameter
    '        param.ParameterName = "in_NotificationList"
    '        param.OracleDbType = OracleDbType.VarChar
    '        param.Direction = Data.ParameterDirection.Input
    '        param.Value = strNotificationList
    '        paramCollection.Add(param)

    '        param = New OracleParameter
    '        param.ParameterName = "in_NotifyType"
    '        param.OracleDbType = OracleDbType.VarChar
    '        param.Direction = Data.ParameterDirection.Input
    '        param.Value = strNotifyType
    '        paramCollection.Add(param)

    '        param = New OracleParameter
    '        param.ParameterName = "in_UserName"
    '        param.OracleDbType = OracleDbType.VarChar
    '        param.Direction = Data.ParameterDirection.Input
    '        param.Value = userProfile.Username
    '        paramCollection.Add(param)

    '        param = New OracleParameter
    '        param.ParameterName = "out_status"
    '        param.OracleDbType = OracleDbType.Number
    '        param.Direction = Data.ParameterDirection.Output
    '        paramCollection.Add(param)
    '        Dim ret As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "MOCMaint.UpdateMOCNotification")

    '    Catch ex As Exception
    '        Throw New Data.DataException("UpdateMOCNotification", ex)
    '    Finally

    '    End Try
    'End Sub

    Public Function GetAuthLevel(ByVal user As String, ByVal site As String) As String
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
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = site
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsAuthLevel"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "GetAuthLevel_" & user
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.newmoc.GetAuthLevel", key, 0)

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


    Protected Sub _ddlClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlClass.SelectedIndexChanged
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds4 As System.Data.DataSet = Nothing

        paramCollection.Clear()
        param = New OracleParameter
        param.ParameterName = "in_SiteID"
        param.OracleDbType = OracleDbType.VarChar
        param.Value = _ddlFacility.SelectedValue
        param.Direction = Data.ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsClassList"
        param.OracleDbType = OracleDbType.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)

        ds4 = RI.SharedFunctions.GetDSFromPackage(paramCollection, "MOCMaint.GetClassNotificationList")

        With Me._gvClass
            .DataSource = ds4.Tables(0).CreateDataReader
            .DataBind()
        End With
    End Sub


    Protected Sub SaveClassNotification()

        Dim strSiteId, strClass, strApprovalLevel, strRequired As String
        Dim strUsername As String = ""
        Dim strRoleSeqId As String = ""
        Dim strRoleSiteId As String = ""

        Try
            strSiteId = Me._ddlFacility.SelectedValue
            strClass = Me._ddlClass.SelectedValue
            strApprovalLevel = Me._ddlApproval.SelectedValue
            strRequired = Me._cbRequired.Checked
            If IsNumeric(Me._ddlPeople.SelectedValue) Then
                strRoleSeqId = Me._ddlPeople.SelectedValue
                strRoleSiteId = Me._ddlFacility2.SelectedValue
                strUsername = ""
            Else
                strUsername = Me._ddlPeople.SelectedValue
            End If

            If strRequired = "False" Then
                strRequired = "N"
            Else
                strRequired = "Y"
            End If

            UpdateClassification(strSiteId, strClass, strUsername, strApprovalLevel, strRequired, strRoleSeqId, strRoleSiteId)

            GetClassData()
        Catch
            Throw New Data.DataException("SaveClassNotification")
        End Try
    End Sub
    Private Function UpdateClassification(ByVal strSiteID As String, ByVal strClass As String, ByVal strUsername As String, ByVal strApprovalLevel As String, ByVal strRequired As String, ByVal strRoleSeqId As String, ByVal strRoleSiteId As String) As String

        Dim dr As OracleDataReader = Nothing
        Dim status As String
        Dim paramCollection As New OracleParameterCollection

        Try

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_SiteId"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strSiteID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Class"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strClass
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Username"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strUsername
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_NotifyType"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strApprovalLevel
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Required"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strRequired
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_RoleSeqId"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strRoleSeqId
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_RoleSiteId"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strRoleSiteId
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_UpdateUsername"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userProfile.Username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "MOCMaint.UpdateMOCClassNotification")
            If status <> 0 Then
                Throw New Data.DataException("UpdateClassNotification Oracle Error:" & status)
            End If

        Catch ex As Exception
            Throw New Data.DataException("UpdateClassNotification", ex)
            status = -1
        Finally
            If Not dr Is Nothing Then dr = Nothing
        End Try
        Return status
    End Function

    Protected Sub _gvClass_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles _gvClass.RowDeleting
        Dim dr As OracleDataReader = Nothing
        Dim status As String
        Dim paramCollection As New OracleParameterCollection

        Try

            Dim param As New OracleParameter
            param = New OracleParameter
            param.ParameterName = "in_ClassNotify_SeqId"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me._gvClass.DataKeys.Item(e.RowIndex).Value
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "MOCMaint.DeleteMOCClassNotification")
            If status <> 0 Then
                Throw New Data.DataException("DeleteMOCClassNotification Oracle Error:" & status)
            End If

            GetClassData()

        Catch ex As Exception
            Throw New Data.DataException("DeleteMOCClassNotification", ex)
            status = -1
        Finally
            If Not dr Is Nothing Then dr = Nothing
        End Try
    End Sub
    Protected Sub _gvCategory_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles _gvCategory.RowDeleting
        Dim status As String
        Dim paramCollection As New OracleParameterCollection

        Try

            Dim param As New OracleParameter
            param = New OracleParameter
            param.ParameterName = "in_CatNotify_SeqId"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me._gvCategory.DataKeys.Item(e.RowIndex).Value
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "MOCMaint.DeleteMOCCatNotification")
            If status <> 0 Then
                Throw New Data.DataException("DeleteMOCCatNotification Oracle Error:" & status)
            End If

            GetCategoryData()

        Catch ex As Exception
            Throw New Data.DataException("DeleteMOCCatNotification", ex)
            status = -1
        Finally
        End Try
    End Sub
    Protected Sub _rblMaintType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _rblMaintType.SelectedIndexChanged
        If _rblMaintType.SelectedValue = "Classification" Then
            _tcClass.Visible = "true"
            _tcCategory.Visible = "false"
            _tcBusinessUnit.Visible = "false"
            _tcArea.Visible = "false"
            _tcLine.Visible = "false"
            Me._gvClass.Visible = "True"
            Me._gvCategory.Visible = "false"
            _pnlBUA.Visible = "false"
            GetClassData()
            Me._ddlFacility2.Enabled = "true"
        ElseIf _rblMaintType.SelectedValue = "Category" Then
            _tcClass.Visible = "false"
            _tcCategory.Visible = "true"
            _tcBusinessUnit.Visible = "false"
            _tcArea.Visible = "false"
            _tcLine.Visible = "false"

            Me._gvClass.Visible = "false"
            Me._gvCategory.Visible = "true"
            _pnlBUA.Visible = "false"
            Me._ddlFacility2.Enabled = "true"
            GetCategoryData()
        Else
            PopulateNotificationList(Me._ddlFacility.SelectedValue)
            _tcClass.Visible = "false"
            _tcCategory.Visible = "false"
            _tcBusinessUnit.Visible = "true"
            _tcArea.Visible = "true"
            _tcLine.Visible = "true"
            Me._ddlFacility.Enabled = "false"
            Me._gvClass.Visible = "false"
            Me._gvCategory.Visible = "false"
            _pnlBUA.Visible = "true"
            'Me._ddlFacility2.SelectedValue = userProfile.DefaultFacility

            'Me._ddlFacility2.Enabled = "false"
        End If
    End Sub
    Private Sub GetClassData()
        Dim dr As OracleDataReader = Nothing

        Try

            dr = GetClassRecordsDRFromPackage()
            If dr IsNot Nothing Then
                With Me._gvClass
                    .DataSource = dr
                    .DataBind()
                End With
            End If

        Catch ex As Exception
            Throw New Data.DataException("GetClassData", ex)
        End Try

    End Sub
    Private Function GetClassRecordsDRFromPackage() As OracleDataReader
        Dim dr As OracleDataReader = Nothing
        Dim paramCollection As New OracleParameterCollection

        Try

            Dim param As New OracleParameter

            paramCollection.Clear()
            param = New OracleParameter
            param.ParameterName = "in_SiteID"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = _ddlFacility.SelectedValue
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsClassList"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "MOCMaint.GetClassNotificationList")

        Catch ex As Exception
            Throw New Data.DataException("GetClassRecordsDRFromPackage", ex)
            Return Nothing
        Finally
            GetClassRecordsDRFromPackage = dr
        End Try
    End Function
    Private Sub GetCategoryData()
        Dim dr As OracleDataReader = Nothing

        Try
            dr = GetCatRecordsDRFromPackage()

            If dr IsNot Nothing Then
                With Me._gvCategory
                    .DataSource = dr
                    .DataBind()
                End With
            End If
        Catch ex As Exception
            Throw New Data.DataException("GetClassRecordsDRFromPackage", ex)
        Finally
            dr = Nothing
        End Try
    End Sub
    Private Function GetCatRecordsDRFromPackage() As OracleDataReader
        Dim dr As OracleDataReader = Nothing
        Dim paramCollection As New OracleParameterCollection

        Try

            Dim param As New OracleParameter

            paramCollection.Clear()
            param = New OracleParameter
            param.ParameterName = "in_CatSeqID"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = Me._ddlCategory.SelectedValue
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_SiteID"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = _ddlFacility.SelectedValue
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsCategoryList"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "MOCMaint.GetCatNotificationList")
        Catch ex As Exception
            Throw New Data.DataException("GetCatRecordsDRFromPackage", ex)
            Return Nothing
        Finally
            GetCatRecordsDRFromPackage = dr
        End Try
    End Function

    Protected Sub SaveCatNotification()

        Dim strSiteId, strCat, strApprovalLevel, strRequired As String
        Dim strUsername As String = ""
        Dim strRoleSeqId As String = ""
        Dim strRoleSiteId As String = ""

        Try
            strSiteId = Me._ddlFacility.SelectedValue
            strCat = Me._ddlCategory.SelectedValue
            strApprovalLevel = Me._ddlApproval.SelectedValue
            strRequired = Me._cbRequired.Checked

            '_hfPeople
            If IsNumeric(Me._ddlPeople.SelectedValue) Then
                'If IsNumeric(_hfPeople.Value) Then
                strRoleSiteId = Me._ddlFacility2.SelectedValue
                strRoleSeqId = Me._ddlPeople.SelectedValue
                'strRoleSeqId = _hfPeople.Value
                strUsername = ""
            Else
                strUsername = Me._ddlPeople.SelectedValue
                'strUsername = _hfPeople.Value
            End If

            If strRequired = "False" Then
                strRequired = "N"
            Else
                strRequired = "Y"
            End If

            UpdateCategory(strSiteId, strCat, strUsername, strApprovalLevel, strRequired, strRoleSeqId, strRoleSiteId)

            Me.GetCategoryData()

        Catch ex As Exception
            Throw New Data.DataException("SaveCatNotification", ex)
        End Try
    End Sub
    Private Function UpdateCategory(ByVal strSiteID As String, ByVal strCategory As String, ByVal strUsername As String, ByVal strApprovalLevel As String, ByVal strRequired As String, ByVal strRoleSeqId As String, ByVal strRoleSiteId As String) As String

        Dim status As String
        Dim paramCollection As New OracleParameterCollection

        Try

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_SiteId"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strSiteID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Cat"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strCategory
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Username"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strUsername
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_NotifyType"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strApprovalLevel
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Required"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strRequired
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_RoleSeqId"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strRoleSeqId
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_RoleSiteId"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strRoleSiteId
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_UpdateUsername"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userProfile.Username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "MOCMaint.UpdateMOCCatNotification")
            If status <> 0 Then
                Throw New Data.DataException("UpdateMOCCatNotification Oracle Error:" & status)
            End If

        Catch ex As Exception
            Throw New Data.DataException("UpdateCategory", ex)
            status = -1
        Finally
        End Try
        Return status
    End Function


    Private Sub PopulateNotificationList(ByVal SiteID As String)
        ', ByVal busunit As String, ByVal area As String, ByVal line As String)
        Try
            Dim paramCollection As New OracleParameterCollection
            Dim param As New OracleParameter
            Dim ds3 As System.Data.DataSet = Nothing
            Dim dr As Data.DataTableReader = Nothing

            Dim busunit As String = _ddlBusinessUnit.SelectedValue
            If busunit = "" Then
                busunit = "All"
            End If
            Dim area As String = _ddlArea.SelectedValue
            If area = "" Then
                area = "All"
            End If
            Dim line As String = _ddlLineBreak.SelectedValue
            If line = "" Then
                line = "All"
            End If

            'Get the initial list of approvers based on tblmocnotification table.  This should only show up when an MOC is created.
            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = SiteID
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_BusUnit"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = busunit
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_area"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = area
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_line"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = line
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsInformedList"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsL1List"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsL2List"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsL3List"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            ds3 = RI.SharedFunctions.GetDSFromPackage(paramCollection, "MOCMaint.GetBUANotificationList2", "GetBUANotificationList", 0)

            dr = ds3.Tables(0).CreateDataReader
            Dim dr2 As Data.DataTableReader = ds3.Tables(1).CreateDataReader
            Dim dr3 As Data.DataTableReader = ds3.Tables(2).CreateDataReader
            Dim dr4 As Data.DataTableReader = ds3.Tables(3).CreateDataReader

            Me._gvInformed.DataSource = dr
            Me._gvInformed.DataBind()

            Me._gvL1.DataSource = dr2 'dt.Select("notifytype='L1'")
            Me._gvL1.DataBind()

            Me._gvL2.DataSource = dr3
            Me._gvL2.DataBind()

            Me._gvL3.DataSource = dr4
            Me._gvL3.DataBind()

            'Me._gvL2Approvers.DataSource = dr3
            'Me._gvL2Approvers.DataBind()

            'Me._gvL3Approvers.DataSource = dr4
            'Me._gvL3Approvers.DataBind()

        Catch ex As Exception
            Throw New Data.DataException("PopulateNotificationList", ex)
        Finally
        End Try

    End Sub

    Sub UpdateBUANotification()
        Try
            Dim paramCollection As New OracleParameterCollection
            Dim param As New OracleParameter
            Dim status As String = String.Empty
            Dim strRequired As String = String.Empty
            Dim strRoleSeqId As String = String.Empty
            Dim strRoleSiteID As String = String.Empty
            Dim strUsername As String = String.Empty

            strRequired = Me._cbRequired.Checked
            If IsNumeric(_ddlPeople.SelectedValue) Then
                strRoleSeqId = Me._ddlPeople.SelectedValue
                strRoleSiteID = Me._ddlFacility2.SelectedValue
                strUsername = ""
            Else
                strUsername = Me._ddlPeople.SelectedValue
            End If

            If strRequired = "False" Then
                strRequired = "N"
            Else
                strRequired = "Y"
            End If

            Dim busunit As String = _ddlBusinessUnit.SelectedValue
            If busunit = "" Then
                busunit = "All"
            End If
            Dim area As String = _ddlArea.SelectedValue
            If area = "" Then
                area = "All"
            End If
            Dim line As String = _ddlLineBreak.SelectedValue
            If line = "" Then
                line = "All"
            End If

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = _ddlFacility.SelectedValue
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_BusinessUnit"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = busunit
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_area"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = area
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_line"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = line
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_NotifyType"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = Me._ddlApproval.SelectedValue
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Username"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = strUsername
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Required"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strRequired
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_RoleSeqId"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strRoleSeqId
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_RoleSiteId"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strRoleSiteID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_UpdateUsername"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userProfile.Username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "MOCMaint.UpdateMOCNotification")
            If status <> 0 Then
                Throw New Data.DataException("UpdateBUANotification Oracle Error:" & status)
            End If

        Catch ex As Exception
            Throw New Data.DataException("UpdateBUANotification", ex)
        End Try

    End Sub

    Protected Sub DeleteBUA(ByVal deleteRecord As String)
        Dim status As String
        Dim paramCollection As New OracleParameterCollection

        Try

            Dim param As New OracleParameter
            param = New OracleParameter
            param.ParameterName = "in_BUANotify_SeqId"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = deleteRecord
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "MOCMaint.DeleteMOCBUANotification")
            If status <> 0 Then
                Throw New Data.DataException("DeleteMOCBUANotification Oracle Error:" & status)
            End If

            PopulateNotificationList(_ddlFacility.SelectedValue)

        Catch ex As Exception
            Throw New Data.DataException("DeleteMOCBUANotification", ex)
            status = -1
        Finally
            status = Nothing
        End Try
    End Sub

    Protected Sub _gvInformed_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles _gvInformed.RowDeleting
        Dim strDeleteRecord As String = Me._gvInformed.DataKeys.Item(e.RowIndex).Value

        DeleteBUA(strDeleteRecord)

    End Sub

    Protected Sub _gvL1Approvers_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles _gvL1.RowDeleting
        Dim strDeleteRecord As String = Me._gvL1.DataKeys.Item(e.RowIndex).Value

        DeleteBUA(strDeleteRecord)
    End Sub

    Protected Sub _gvL2Approvers_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles _gvL2.RowDeleting
        Dim strDeleteRecord As String = Me._gvL2.DataKeys.Item(e.RowIndex).Value

        DeleteBUA(strDeleteRecord)
    End Sub

    Protected Sub _gvL3Approvers_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles _gvL3.RowDeleting
        Dim strDeleteRecord As String = Me._gvL3.DataKeys.Item(e.RowIndex).Value

        DeleteBUA(strDeleteRecord)
    End Sub

End Class
