Imports System.Data
Imports Devart.Data.Oracle


Partial Class RI_MOC
    Inherits RIBasePage

    Dim userProfile As RI.CurrentUserProfile = Nothing
    Dim currentTemplateTask As clsMOCTemplateTask
    Dim ipLoc As New IP.Bids.Localization.WebLocalization()
    Dim selectedDesc As String = ""

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("MOC Task Templates", True))
        Master.ShowMOCMenu()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        userProfile = RI.SharedFunctions.GetUserProfile

        If _rblMaintType.SelectedValue = "Classification" Then
            Me._trCat.Visible = False
            Me._trClass.Visible = True
            
            'PopulateClassCat()
            'PopulateFacility()
        ElseIf _rblMaintType.SelectedValue = "Category" Then
            Me._trClass.Visible = False
            Me._trCat.Visible = True
            'selectedDesc = _ddlCategory.SelectedItem.Text

            'PopulateClassCat()
        End If

        If Not Page.IsPostBack Then
            PopulateClassCat()

            Me._ucNewResponsible.FacilityValue = userProfile.DefaultFacility
            Me._ucNewResponsible.ResponsibleValue = userProfile.Username
            getRoles()
            'GetApprovalList("TE", Me._ddlHiddenRoles.ClientID)
            '_ddlHiddenRoles.DataSource = GetApprovalList("TE", _ddlHiddenRoles.ClientID)
            '_ddlHiddenRoles.DataBind()
            '_ddlNewResponsible.DataSource = GetApprovalList("TE", _ddlNewResponsible.ClientID)
            '_ddlNewResponsible.DataBind()
            GetTemplateTasks()
        Else
            'GetTemplateTasks()
        End If

        '_ddlNewResponsible.Attributes.Add("onchange", "this.blur;GetEmployeeWRoles('" & ddlEditFacility.ClientID & "','" & ddlEditSystemPerson.ClientID & "');")

        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "MOCTemp") Then
            Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "MOCTemp", Page.ResolveClientUrl("~/moc/datamaintenance/moctasktemplate.js"))
        End If

        Dim sc As ScriptManager
        sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
        If sc IsNot Nothing Then
            Dim loService As New ServiceReference
            loService.InlineScript = False
            loService.Path = "~/RISharedWS.asmx"
            sc.Services.Add(loService)
        End If

    End Sub

    Private Sub GetRolesOLD()
        Dim paramCollection As New Devart.Data.Oracle.OracleParameterCollection
        Dim param As New Devart.Data.Oracle.OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            'Check input paramaters
            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsRoles"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "GetMTTRoles_"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.GetMTTRoles", key, 2)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    'Roles                     
                    Dim dr As System.Data.DataTableReader = ds.Tables(0).CreateDataReader
                    'Dim roles() As New clsData

                    With _ddlHiddenRoles
                        .DataSource = ds.Tables(0).CreateDataReader()
                        .DataTextField = "roledescription"
                        .DataValueField = "roleseqid"
                        .DataBind()
                    End With

                End If
            End If

        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                ds = Nothing
            End If
        End Try

    End Sub
    Protected Sub _btnSaveTemplate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnSaveTemplate.Click
        Dim strClass, strClassDesc As String
        Dim strCategory, strCategoryDesc As String

        Try

            If _ddlClass.Visible = "true" Then
                strClass = _ddlClass.SelectedValue
                strClassDesc = _ddlClass.SelectedItem.Text
                strCategory = "0"
                strCategoryDesc = ""
            Else
                strClass = "0"
                strClassDesc = ""
                strCategory = _ddlCategory.SelectedValue
                strCategoryDesc = _ddlCategory.SelectedItem.Text
            End If
            currentTemplateTask = New clsMOCTemplateTask

            If _hfTaskHeaderSeqid.Value = "" Then
                'If Me._ddlClass.SelectedValue <> "" Then
                currentTemplateTask.CheckTemplate(strClass, strCategory, userProfile.DefaultFacility)
                'End If
                If currentTemplateTask.templateSeqID = "" Then
                    currentTemplateTask.UserName = userProfile.Username
                    currentTemplateTask.siteID = userProfile.DefaultFacility
                    currentTemplateTask.CreateNewMOCTemplate(strClass, strClassDesc, strCategory, strCategoryDesc)
                    _hfTaskHeaderSeqid.Value = currentTemplateTask.templateSeqID
                End If
            End If

            If currentTemplateTask.templateSeqID > 0 Or _hfTaskHeaderSeqid.Value <> "" Then
                If currentTemplateTask.templateSeqID = "" Then
                    currentTemplateTask.templateSeqID = _hfTaskHeaderSeqid.Value
                End If
                If _tbNewTitle.Text <> "" And _tbNewDaysAfter.Text <> "" Then
                    With currentTemplateTask
                        .taskSeqID = ""
                        .title = RI.SharedFunctions.DataClean(_tbNewTitle.Text)
                        .Description = _tbNewDesc.Text
                        .Priority = _ddlNewPriority.SelectedValue
                        .daysAfter = _tbNewDaysAfter.Text
                        '.daysAfter = _NewDate.DaysAfter
                        '.dueDate = RI.SharedFunctions.DataClean(_NewDate.DateValue)
                        If IsNumeric(_ucNewResponsible.ResponsibleValue) = True Then
                            .responsibleRole = _ucNewResponsible.ResponsibleValue
                            .responsibleRolePlantCode = _ucNewResponsible.FacilityValue
                        Else
                            .responsibleRolePlantCode = _ucNewResponsible.FacilityValue
                            .responsibleUsername = _ucNewResponsible.ResponsibleValue
                            '.responsibleRole = _ddlNewResponsible.SelectedValue
                            '.responsibleRolePlantCode = userProfile.DefaultFacility
                        End If
                        If _cbNewRequired.Checked Then
                            .required = "Y"
                        End If
                        .UserName = userProfile.Username
                    End With
                    currentTemplateTask.SaveTemplateTasks()

                End If
            End If

            '    Dim strRoleSeqIDs As String = ""
            Dim return2 As String = ""
            For Each r As GridViewRow In Me._grvTasksPage.DirtyRows
                currentTemplateTask = New clsMOCTemplateTask
                Dim taskSeqID As HiddenField = TryCast(r.FindControl(String.Format("_hfTaskItemSeqID", r.RowIndex)), HiddenField)
                Dim title As TextBox = TryCast(r.FindControl(String.Format("_tbTitle", r.RowIndex)), TextBox)
                Dim description As AdvancedTextBox.AdvancedTextBox = TryCast(r.FindControl(String.Format("_tbDesc", r.RowIndex)), AdvancedTextBox.AdvancedTextBox)
                Dim priority As DropDownList = TryCast(r.FindControl(String.Format("_ddlPriority", r.RowIndex)), DropDownList)
                Dim daysAfter As TextBox = TryCast(r.FindControl(String.Format("_tbDaysAfter", r.RowIndex)), TextBox)
                'Dim MOCDate As ucMOCDate = TryCast(r.FindControl(String.Format("_MOCDate", r.RowIndex)), ucMOCDate)
                Dim required As CheckBox = TryCast(r.FindControl(String.Format("_cbRequired", r.RowIndex)), CheckBox)

                'Dim responsibleRole As DropDownList = TryCast(r.FindControl(String.Format("_ucResponsible", r.RowIndex)), DropDownList)
                Dim responsibleRole As ucMTTResponsible = TryCast(r.FindControl(String.Format("_ucResponsible", r.RowIndex)), ucMTTResponsible)

                With currentTemplateTask
                    .title = RI.SharedFunctions.DataClean(title.Text)
                    .Description = RI.SharedFunctions.DataClean(description.Text)
                    .daysAfter = daysAfter.Text
                    '.daysAfter = MOCDate.DaysAfter
                    '.dueDate = RI.SharedFunctions.DataClean(MOCDate.DateValue)
                    .UserName = userProfile.Username
                    If required.Checked = "True" Then
                        .required = "Y"
                    Else
                        .required = ""
                    End If
                    .Priority = priority.SelectedValue
                    If IsNumeric(responsibleRole.ResponsibleValue) = True Then
                        .responsibleRole = responsibleRole.ResponsibleValue
                        .responsibleRolePlantCode = responsibleRole.FacilityValue
                    Else
                        .responsibleRolePlantCode = responsibleRole.FacilityValue
                        .responsibleUsername = responsibleRole.ResponsibleValue
                    End If
                    
                    '            .TemplateSeqID = Me._ddlTemplateTasks.SelectedValue
                    .taskSeqID = taskSeqID.Value
                End With

                return2 = currentTemplateTask.SaveTemplateTasks()

                If return2 <> "0" Then
                    Me._lblStatus.Text = "Error Occurred During Save."
                End If

            Next

            ClearNewRow()
            GetTemplateTasks(currentTemplateTask.templateSeqID)

        Catch ex As Exception
            Throw
        Finally
        End Try
    End Sub
    Sub getRoles()
        Dim paramCollection As New Devart.Data.Oracle.OracleParameterCollection
        Dim param As New Devart.Data.Oracle.OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            'Check input paramaters
            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsRoles"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "GetMTTRoles_"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.GetMTTRoles", key, 2)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    'Roles                     
                    Dim dr As System.Data.DataTableReader = ds.Tables(0).CreateDataReader

                    'With _ddlHiddenRoles
                    '  .DataSource = ds.Tables(0).CreateDataReader()
                    '   .DataTextField = "roledescription"
                    '     .DataValueField = "roleseqid"
                    '    .DataBind()
                    ' End With
                    ' ipLoc.LocalizeListControl(_ddlHiddenRoles)

                    With _ddlNewResponsible
                        .DataSource = ds.Tables(0).CreateDataReader()
                        .DataTextField = "roledescription"
                        .DataValueField = "roleseqid"
                        .DataBind()
                    End With
                    ipLoc.LocalizeListControl(_ddlNewResponsible)

                End If
            End If

        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                ds = Nothing
            End If

        End Try
    End Sub
    Public Function GetApprovalList(ByVal siteID As String, ByVal userControlId As String) As Collections.Generic.List(Of ListItem)
        Dim roleDescription As String = String.Empty
        Dim ddlList As New Collections.Generic.List(Of ListItem)

        'ddlList.Add(New ListItem(userControlId, CStr(0)))

        Dim ds As DataSet = Nothing
        Dim paramCollection As New OracleParameterCollection

        Dim param As New OracleParameter

        param = New OracleParameter
        param.ParameterName = "in_siteId"
        param.OracleDbType = OracleDbType.VarChar
        param.Value = siteID
        param.Direction = ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsResponsibleList"
        param.OracleDbType = OracleDbType.Cursor
        param.Direction = ParameterDirection.Output
        paramCollection.Add(param)

        ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "NewMOC.GetResponsibleList", "MOCPerson" & siteID, 0)

        Dim drResp As DataTableReader
        drResp = ds.CreateDataReader

        If drResp IsNot Nothing Then
            Do While drResp.Read
                Dim spaceChar As String = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                With ddlList
                    If drResp.Item("RoleDescription") <> roleDescription Then 'New Group
                        'No Roleseqid indicates individual
                        Dim roleItem As New ListItem
                        roleDescription = drResp.Item("RoleDescription")
                        roleItem.Text = drResp.Item("RoleDescription").ToUpper
                        If drResp.Item("RoleSeqID") IsNot DBNull.Value Then
                            roleItem.Value = drResp.Item("RoleSeqID") & "/" & drResp.Item("UserName")
                        End If

                        'If ddlList.Count > 0 Then
                        'Dim blankItem As New ListItem
                        'With blankItem
                        ' .Attributes.Add("disabled", "true")
                        ' .Text = ""
                        ' .Value = -1
                        'End With
                        'ddlList.Add(blankItem)
                        'End If

                        If roleDescription.Length > 0 Then
                            roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:black; font-size:Larger;")
                            ddlList.Add(roleItem)
                        Else
                            roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:black;")
                            roleItem.Attributes.Add("disabled", "true")
                            ddlList.Add(roleItem)
                        End If

                    End If

                    Dim useritem As New ListItem
                    With useritem
                        .Text = Server.HtmlDecode(spaceChar & drResp.Item("Name"))
                        If drResp.Item("RoleSeqid").ToString <> "" Then
                            .Value = drResp.Item("RoleSeqID")
                        Else
                            .Value = drResp.Item("UserName")
                        End If
                    End With
                    ddlList.Add(useritem)

                End With

            Loop

        End If
        Return ddlList
    End Function


    Protected Sub _grvTasksPage_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _grvTasksPage.RowDataBound
        If e.Row.RowIndex >= 0 Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim deleteButton As Button = TryCast(e.Row.FindControl("_btnDelete"), Button)
                If deleteButton IsNot Nothing Then
                    deleteButton.OnClientClick = "Javascript:ConfirmDelete('" & e.Row.DataItem("taskitemseqid").ToString & "'," & e.Row.RowIndex & ",'" & Me._grvTasksPage.ClientID & "');return false;"
                End If
            End If
        End If

    End Sub

    Protected Sub GetTemplateTasks(Optional ByVal strTemplateSeqID As String = Nothing)
        If strTemplateSeqID = "" Then
            currentTemplateTask = New clsMOCTemplateTask
            If _ddlClass.Visible = "true" Then
                With currentTemplateTask
                    strTemplateSeqID = .CheckTemplate(_ddlClass.SelectedValue, "", userProfile.DefaultFacility)
                End With
                'strTemplateSeqID = Me._ddlClass.SelectedValue
            Else
                With currentTemplateTask
                    strTemplateSeqID = .CheckTemplate("", _ddlCategory.SelectedValue, userProfile.DefaultFacility)
                End With
            End If
            strTemplateSeqID = currentTemplateTask.templateSeqID
            'strTemplateSeqID = Me._ddlClass.SelectedValue
            'Else
            '  strTemplateSeqID = Me._hfTaskHeaderSeqid.Value
        End If

        'If strTemplateSeqID <> "" Then
        'ddlClass.SelectedValue = strTemplateSeqID
        'End If

        Dim paramCollection As New Devart.Data.Oracle.OracleParameterCollection
        Dim param As New Devart.Data.Oracle.OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            Dim key As String
            'Check input paramaters
            paramCollection.Clear()

            key = "GetTemplateTasks_" & Me._ddlClass.SelectedItem.Value
            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "in_TaskHeader"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strTemplateSeqID
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsTemplateTasks"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.moctasktemplatemaint.TemplateTaskList", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    'Tasks                     
                    Dim dr As System.Data.DataTableReader = ds.Tables(0).CreateDataReader
                    Dim dt As System.Data.DataTable = ds.Tables(0)
                    Me._grvTasksPage.DataSource = dt
                    _grvTasksPage.AutoGenerateColumns = False
                    _grvTasksPage.DataBind()
                    _grvTasksPage.Visible = True
                    '_tbTemplateName.Text = _ddlClass.SelectedValue
                End If
            Else
                _grvTasksPage.DataSource = Nothing
                _grvTasksPage.DataBind()
            End If

        Catch ex As Exception
            Throw New Data.DataException("GetTemplateTasks", ex)
        Finally
            If ds IsNot Nothing Then
                ds = Nothing
            End If

        End Try

    End Sub

    Private Sub PopulateClassCat()
        Try
            Dim clsClassCategory As New clsMOCClassCategory
            clsClassCategory.GetDDLData()

            RI.SharedFunctions.BindList(Me._ddlClass, clsClassCategory.Classification, False, False)
            RI.SharedFunctions.BindList(Me._ddlCategory, clsClassCategory.Category, False, False)

        Catch ex As Exception
            Throw New Data.DataException("PopulateClassCat", ex)
        Finally
        End Try
    End Sub

    Protected Sub _ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlCategory.SelectedIndexChanged
        selectedDesc = _ddlCategory.SelectedItem.Text
        currentTemplateTask = New clsMOCTemplateTask
        'If _hfTaskHeaderSeqid.Value = "" Then
        If Me._ddlCategory.SelectedValue <> "" Then
            currentTemplateTask.CheckTemplate("", _ddlCategory.SelectedValue, userProfile.DefaultFacility)
            Me._hfTaskHeaderSeqid.Value = currentTemplateTask.templateSeqID
        End If
        'End If
        If currentTemplateTask.templateSeqID <> "" Then
            GetTemplateTasks(currentTemplateTask.templateSeqID)
            Me._hfTaskHeaderSeqid.Value = currentTemplateTask.templateSeqID
        Else
            Me._grvTasksPage.DataSource = Nothing
            Me._grvTasksPage.DataBind()
        End If
        ClearNewRow()
    End Sub

    Protected Sub _ddlClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlClass.SelectedIndexChanged
        selectedDesc = _ddlClass.SelectedItem.Text
        currentTemplateTask = New clsMOCTemplateTask
        'If _hfTaskHeaderSeqid.Value = "" Then
        If Me._ddlClass.SelectedValue <> "" Then
            currentTemplateTask.CheckTemplate(_ddlClass.SelectedValue, "", userProfile.DefaultFacility)
        End If
        'End If
        If currentTemplateTask.templateSeqID <> "" Then
            GetTemplateTasks(currentTemplateTask.templateSeqID)
            Me._hfTaskHeaderSeqid.Value = currentTemplateTask.templateSeqID
        Else
            Me._grvTasksPage.DataSource = Nothing
            Me._grvTasksPage.DataBind()
            Me._hfTaskHeaderSeqid.Value = currentTemplateTask.templateSeqID
        End If
        ClearNewRow()
    End Sub

    Protected Sub _grvTasksPage_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles _grvTasksPage.DataBinding
        _grvTasksPage.EmptyDataText = "No Existing Tasks for " & selectedDesc
    End Sub

    Protected Sub _rblMaintType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _rblMaintType.SelectedIndexChanged
        userProfile = RI.SharedFunctions.GetUserProfile
        If currentTemplateTask Is Nothing Then
            currentTemplateTask = New clsMOCTemplateTask
        End If
        If _rblMaintType.SelectedValue = "Classification" Then
            selectedDesc = _ddlClass.SelectedItem.Text
            currentTemplateTask.CheckTemplate(_ddlClass.SelectedValue, _ddlCategory.SelectedValue, userProfile.DefaultFacility)
        ElseIf _rblMaintType.SelectedValue = "Category" Then
            selectedDesc = _ddlCategory.SelectedItem.Text
            currentTemplateTask.CheckTemplate(_ddlClass.SelectedValue, _ddlCategory.SelectedValue, userProfile.DefaultFacility)
        End If
        Me._hfTaskHeaderSeqid.Value = Nothing

        ClearNewRow()
        GetTemplateTasks()

    End Sub

    Protected Sub ClearNewRow()
        With Me
            ._NewDate.DateValue = ""
            ._NewDate.DaysAfter = ""
            ' need to clear out date control ._NewDate.da()
            ._tbNewTitle.Text = ""
            ._tbNewDesc.Text = ""
            ._cbNewRequired.Checked = False
        End With
    End Sub
End Class
