Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Devart.Data.Oracle
Imports System.Web.Mail

Partial Class RI_Outage
    Inherits RIBasePage

    Dim userProfile As RI.CurrentUserProfile = Nothing
    Dim currentOutageTemplate As clsOutageTemplate
    Dim currentTemplateTask As clsCurrentTemplateTask

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("OutageTemplate", True))
        Master.SetBanner("Manage Outage Templates")
        Master.ShowOutageMenu()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        userProfile = RI.SharedFunctions.GetUserProfile
        
        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "Outage") Then
            Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "Outage", Page.ResolveClientUrl("~/outage/outage.js"))
        End If
        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "OutageTemp") Then
            Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "OutageTemp", Page.ResolveClientUrl("~/outage/datamaintenance/outagetemplate.js"))
        End If

        Dim sc As ScriptManager
        sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
        If sc IsNot Nothing Then
            Dim loService As New ServiceReference
            loService.InlineScript = False
            loService.Path = "~/RISharedWS.asmx"
            sc.Services.Add(loService)
        End If

        '_rfvTemplateDesc.Enabled = False

        If Not Page.IsPostBack Then
            currentOutageTemplate = New clsOutageTemplate()
            RI.SharedFunctions.BindList(Me._ddlTemplateTasks, currentOutageTemplate.OutageTemplate, False, True)
            'Me._ddlTemplateTasks.Items.Insert(0, "--New Template--")
            'If _ddlTemplateTasks.SelectedValue IsNot Nothing Then
            If Request.QueryString("TemplateSeqID") IsNot Nothing Or _ddlTemplateTasks.SelectedValue <> "" Then
                GetTemplateTasks(Request.QueryString("TemplateSeqID"))
                _lblTemplateHeading.Visible = True
                'Me._ddlTemplateTasks.SelectedValue = Request.QueryString("TemplateSeqID")
                'Me._tblNewRow.Visible = "True"
                _btnNewCurrent.Visible = True
                _btnSaveTemplate.Visible = True
                _tblNewRow.Visible = True
                '_divNewTemplate.Visible = False
            Else
                _tblNewRow.Visible = False
                _btnNewCurrent.Visible = False
                _btnSaveTemplate.Visible = False
                _lblTemplateHeading.Visible = False 'End If
            End If
        End If

    End Sub

    Protected Sub GetTemplates()
        Dim paramCollection As New Devart.Data.Oracle.OracleParameterCollection
        Dim param As New Devart.Data.Oracle.OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsTemplates"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)


            Dim key As String = "GetMTTTemplates_"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.OutageMaint.Templates", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    'Templates                     
                    Dim dr As System.Data.DataTableReader = ds.Tables(0).CreateDataReader

                    With _ddlTemplateTasks
                        .DataSource = ds.Tables(0).CreateDataReader()
                        .DataTextField = "externalref"
                        .DataValueField = "taskheaderseqid"
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
    Private Sub GetRoles()
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
    'Private Sub GetTemplateTasks(ByVal TemplateSeqID As String)
    '    Dim paramCollection As New Devart.Data.Oracle.OracleParameterCollection
    '    Dim param As New Devart.Data.Oracle.OracleParameter
    '    Dim ds As System.Data.DataSet = Nothing

    '    Try

    '        'Check input paramaters
    '        param = New Devart.Data.Oracle.OracleParameter
    '        param.ParameterName = "rsRoles"
    '        param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
    '        param.Direction = Data.ParameterDirection.Output
    '        paramCollection.Add(param)

    '        Dim key As String = "GetMTTRoles_"
    '        ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.GetMTTRoles", key, 2)

    '        If ds IsNot Nothing Then
    '            If ds.Tables.Count >= 1 Then
    '                'Roles                     
    '                Dim dr As System.Data.DataTableReader = ds.Tables(0).CreateDataReader
    '                With _ddlHiddenRoles
    '                    .DataSource = ds.Tables(0).CreateDataReader()
    '                    .DataTextField = "roledescription"
    '                    .DataValueField = "roleseqid"
    '                    .DataBind()
    '                End With
    '                With _ddlNewPrimaryRole
    '                    .DataSource = ds.Tables(0).CreateDataReader()
    '                    .DataTextField = "roledescription"
    '                    .DataValueField = "roleseqid"
    '                    .DataBind()
    '                End With

    '            End If
    '        End If

    '        paramCollection.Clear()

    '        param = New Devart.Data.Oracle.OracleParameter
    '        param.ParameterName = "in_TaskHeader"
    '        param.OracleDbType = Devart.Data.Oracle.OracleDbType.VarChar
    '        param.Direction = Data.ParameterDirection.Input
    '        param.Value = TemplateSeqID
    '        paramCollection.Add(param)

    '        param = New Devart.Data.Oracle.OracleParameter
    '        param.ParameterName = "rsTaskItemList"
    '        param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
    '        param.Direction = Data.ParameterDirection.Output
    '        paramCollection.Add(param)

    '        key = "GetTemplateTasks_"
    '        ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.GetOutageTemplateTaskItemList", key, 0)

    '        If ds IsNot Nothing Then
    '            If ds.Tables.Count >= 1 Then
    '                'Tasks     
    '                Dim dt As System.Data.DataTable = ds.Tables(0)
    '                Dim dr As System.Data.DataTableReader = ds.Tables(0).CreateDataReader
    '                Me._grvTasksPage.DataSource = dt
    '                _grvTasksPage.DataBind()
    '                _grvTasksPage.Visible = True
    '            End If
    '        End If

    '    Catch ex As Exception
    '        Throw
    '    Finally
    '        If ds IsNot Nothing Then
    '            ds = Nothing
    '        End If

    '    End Try


    'End Sub

   
    'Protected Sub _grvTasksPage_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles _grvTasksPage.ItemDataBound
    '    Dim hfTaskRoleCnt As HiddenField = CType(e.Item.FindControl("_hfRoleCount"), HiddenField)
    '    Dim hfTaskRoleSeq As HiddenField = CType(e.Item.FindControl("_hfRoleSeqId"), HiddenField)
    '    Dim hfTaskItemSeqID As HiddenField = CType(e.Item.FindControl("_hfTaskItemSeqID"), HiddenField)
    '    Dim ddlTemplateRole As DropDownList = CType(e.Item.FindControl("_ddlTemplateRole"), DropDownList)
    '    Dim ddlTemplateRole2 As DropDownList = CType(e.Item.FindControl("_ddlTemplateRole2"), DropDownList)
    '    Dim ddlTemplateRole3 As DropDownList = CType(e.Item.FindControl("_ddlTemplateRole3"), DropDownList)
    '    Dim ddlTemplateRole4 As DropDownList = CType(e.Item.FindControl("_ddlTemplateRole4"), DropDownList)
    '    Dim ddlTemplateRole5 As DropDownList = CType(e.Item.FindControl("_ddlTemplateRole5"), DropDownList)

    '    Dim strRoleSeq As String()

    '    Dim strTaskItemSeq As String
    '    strTaskItemSeq = hfTaskItemSeqID.Value
    '    Dim strTaskRoleCnt As String = hfTaskRoleCnt.Value
    '    Dim strTaskRoleSeq As String = hfTaskRoleSeq.Value

    '    Dim strTaskItemSeqID As String = strTaskItemSeq '"1408261"
    '    Dim strTaskHeaderSeqID As String = Me._ddlTemplateTasks.SelectedValue '"21058"
    '    Dim role As Integer
    '    If strTaskRoleSeq <> "" Then
    '        strRoleSeq = Split(strTaskRoleSeq, ",")
    '        Dim i As Integer
    '        If strRoleSeq.Length = 1 Then
    '            For i = 0 To _ddlHiddenRoles.Items.Count - 1
    '                ddlTemplateRole.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
    '            Next
    '            role = strRoleSeq(0)
    '            ddlTemplateRole.SelectedValue = role
    '        ElseIf strRoleSeq.Length = 2 Then
    '            For i = 0 To _ddlHiddenRoles.Items.Count - 1
    '                ddlTemplateRole.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
    '                ddlTemplateRole2.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
    '            Next
    '            role = strRoleSeq(0)
    '            ddlTemplateRole.SelectedValue = role
    '            role = strRoleSeq(1)
    '            ddlTemplateRole2.SelectedValue = role
    '        ElseIf strRoleSeq.Length = 3 Then
    '            For i = 0 To _ddlHiddenRoles.Items.Count - 1
    '                ddlTemplateRole.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
    '                ddlTemplateRole2.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
    '                ddlTemplateRole3.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
    '            Next
    '            role = strRoleSeq(0)
    '            ddlTemplateRole.SelectedValue = role
    '            role = strRoleSeq(1)
    '            ddlTemplateRole2.SelectedValue = role
    '            role = strRoleSeq(2)
    '            ddlTemplateRole3.SelectedValue = role
    '        End If
    '    End If


    '    If ddlTemplateRole IsNot Nothing Then
    '        ddlTemplateRole.Attributes.Add("onfocus", "copyDDL('" & _ddlHiddenRoles.ClientID & "','" & ddlTemplateRole.ClientID & "');")
    '    End If

    '    If ddlTemplateRole2 IsNot Nothing Then
    '        ddlTemplateRole2.Attributes.Add("onfocus", "copyDDL('" & _ddlHiddenRoles.ClientID & "','" & ddlTemplateRole2.ClientID & "');")
    '    End If

    '    If ddlTemplateRole3 IsNot Nothing Then
    '        ddlTemplateRole3.Attributes.Add("onfocus", "copyDDL('" & _ddlHiddenRoles.ClientID & "','" & ddlTemplateRole3.ClientID & "');")
    '    End If

    '    If ddlTemplateRole4 IsNot Nothing Then
    '        ddlTemplateRole4.Attributes.Add("onfocus", "copyDDL('" & _ddlHiddenRoles.ClientID & "','" & ddlTemplateRole4.ClientID & "');")
    '    End If

    '    If ddlTemplateRole5 IsNot Nothing Then
    '        ddlTemplateRole5.Attributes.Add("onfocus", "copyDDL('" & _ddlHiddenRoles.ClientID & "','" & ddlTemplateRole5.ClientID & "');")
    '    End If

    'End Sub

    'Protected Sub _btnViewTemplateTasks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnViewTemplateTasks.Click
    '    GetTemplateTasks(Me._ddlTemplateTasks.SelectedValue)
    'End Sub

    Protected Sub _btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnSaveTemplate.Click
        currentTemplateTask = New clsCurrentTemplateTask
        Dim strRoleSeqIDs As String = ""
        Dim return2 As String = ""
        For Each r As GridViewRow In Me._grvTasksPage.DirtyRows
            Dim taskSeqID As HiddenField = TryCast(r.FindControl(String.Format("_hfTaskItemSeqID", r.RowIndex)), HiddenField)
            'taskSeqID = row.FindControl("_hfTaskItemSeqID")
            Dim title As TextBox = TryCast(r.FindControl(String.Format("_tbTitle", r.RowIndex)), TextBox)
            Dim description As AdvancedTextBox.AdvancedTextBox = TryCast(r.FindControl(String.Format("_tbDesc", r.RowIndex)), AdvancedTextBox.AdvancedTextBox)
            Dim weeksBefore As TextBox = TryCast(r.FindControl(String.Format("_tbWeeksBefore", r.RowIndex)), TextBox)
            Dim weeksAfter As TextBox = TryCast(r.FindControl(String.Format("_tbWeeksAfter", r.RowIndex)), TextBox)
            Dim leadTimeDays As TextBox = TryCast(r.FindControl(String.Format("_tbLeadTime", r.RowIndex)), TextBox)

            Dim primaryRole As DropDownList = TryCast(r.FindControl(String.Format("_ddlPrimaryRole", r.RowIndex)), DropDownList)
            Dim areaRole1 As DropDownList = TryCast(r.FindControl(String.Format("_ddlTemplateRole1", r.RowIndex)), DropDownList)
            Dim areaRole2 As DropDownList = TryCast(r.FindControl(String.Format("_ddlTemplateRole2", r.RowIndex)), DropDownList)
            Dim areaRole3 As DropDownList = TryCast(r.FindControl(String.Format("_ddlTemplateRole3", r.RowIndex)), DropDownList)
            Dim areaRole4 As DropDownList = TryCast(r.FindControl(String.Format("_ddlTemplateRole4", r.RowIndex)), DropDownList)
            Dim areaRole5 As DropDownList = TryCast(r.FindControl(String.Format("_ddlTemplateRole5", r.RowIndex)), DropDownList)

            With currentTemplateTask
                .Title = RI.SharedFunctions.DataClean(title.Text)
                .Description = RI.SharedFunctions.DataClean(description.Text)
                .weeksAfter = weeksAfter.Text
                .weeksBefore = weeksBefore.Text
                .leadTimeDays = leadTimeDays.Text
                .primaryRole = primaryRole.SelectedValue
                .areaRole1 = Request.Form(areaRole1.UniqueID)
                .areaRole2 = Request.Form(areaRole2.UniqueID)
                .areaRole3 = Request.Form(areaRole3.UniqueID)
                .areaRole4 = Request.Form(areaRole4.UniqueID)
                .areaRole5 = Request.Form(areaRole5.UniqueID)
                .TemplateSeqID = Me._ddlTemplateTasks.SelectedValue
                .TaskSeqID = taskSeqID.Value
            End With

            return2 = currentTemplateTask.SaveTemplateTasks()

            'If return2 <> "0" Then
            '    Me._lblStatus.Text = "Error Occurred During Save."
            'Else
            '    'Response.Redirect(Page.AppRelativeVirtualPath & "?TemplateSeqID=" & Me._ddlTemplateTasks.SelectedValue, True)
            '    Me._lblStatus.Text = "Updates successfully saved."
            '    GetTemplateTasks(Me._ddlTemplateTasks.SelectedValue)
            'End If

        Next
        'GetTemplateTasks()

        If return2 <> "0" Then
            Me._lblStatus.Text = "Error Occurred During Save."
        Else
            '    'Response.Redirect(Page.AppRelativeVirtualPath & "?TemplateSeqID=" & Me._ddlTemplateTasks.SelectedValue, True)
            Me._lblStatus.Text = "Updates successfully saved."
            GetTemplateTasks(Me._ddlTemplateTasks.SelectedValue)
        End If

        'If returns <> "0" Then
        '    Me.lblTaskStatus.Text = "Error Occurred Saving tasks to tempoutagetasks"
        'Else
        '    Me.lblTaskStatus.Text = "Tasks Created.  Go to next page or click close button."

        'End If
        'SaveOutage(True)

    End Sub

    Protected Sub _grvTasksPage_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _grvTasksPage.RowDataBound
        Dim hfTaskRoleCnt As HiddenField = CType(e.Row.Cells(0).FindControl("_hfRoleCount"), HiddenField)
        Dim hfTaskRoleSeq As HiddenField = CType(e.Row.Cells(0).FindControl("_hfRoleSeqId"), HiddenField)
        Dim hfTaskItemSeqID As HiddenField = CType(e.Row.Cells(0).FindControl("_hfTaskItemSeqID"), HiddenField)
        Dim hfPrimaryRoleSeqID As HiddenField = CType(e.Row.Cells(0).FindControl("_hfPrimaryRoleSeqid"), HiddenField)
        Dim ddlPrimaryRole As DropDownList = CType(e.Row.Cells(0).FindControl("_ddlPrimaryRole"), DropDownList)
        Dim ddlTemplateRole As DropDownList = CType(e.Row.Cells(0).FindControl("_ddlTemplateRole1"), DropDownList)
        Dim ddlTemplateRole2 As DropDownList = CType(e.Row.Cells(0).FindControl("_ddlTemplateRole2"), DropDownList)
        Dim ddlTemplateRole3 As DropDownList = CType(e.Row.Cells(0).FindControl("_ddlTemplateRole3"), DropDownList)
        Dim ddlTemplateRole4 As DropDownList = CType(e.Row.Cells(0).FindControl("_ddlTemplateRole4"), DropDownList)
        Dim ddlTemplateRole5 As DropDownList = CType(e.Row.Cells(0).FindControl("_ddlTemplateRole5"), DropDownList)

        Dim strRoleSeq As String()

        If e.Row.RowIndex >= 0 Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                'Now, reference the Button control that the Delete ButtonColumn has been rendered to 
                Dim deleteButton As Button = TryCast(e.Row.FindControl("_btnDelete"), Button)
                If deleteButton IsNot Nothing Then
                    'We can now add the onclick event handler                
                    deleteButton.OnClientClick = "Javascript:ConfirmDelete('" & e.Row.DataItem("taskitemseqid").ToString & "'," & e.Row.RowIndex & ",'" & Me._grvTasksPage.ClientID & "');return false;"
                End If
            End If

            Dim strTaskItemSeq As String
            strTaskItemSeq = hfTaskItemSeqID.Value
            Dim strTaskRoleCnt As String = hfTaskRoleCnt.Value
            Dim strTaskRoleSeq As String = hfTaskRoleSeq.Value

            Dim strTaskItemSeqID As String = strTaskItemSeq '"1408261"
            Dim strTaskHeaderSeqID As String = Me._ddlTemplateTasks.SelectedValue '"21016"
            Dim role As Integer

            Dim a As Integer
            For a = 0 To _ddlHiddenRoles.Items.Count - 1
                ddlPrimaryRole.Items.Insert(a, New ListItem(_ddlHiddenRoles.Items(a).Text, _ddlHiddenRoles.Items(a).Value))
            Next
            ddlPrimaryRole.SelectedValue = hfPrimaryRoleSeqID.Value

            If strTaskRoleSeq <> "" Then
                strRoleSeq = Split(strTaskRoleSeq, ",")
                Dim i As Integer
                If strRoleSeq.Length = 1 Then
                    For i = 0 To _ddlHiddenRoles.Items.Count - 1
                        ddlTemplateRole.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                    Next
                    role = strRoleSeq(0)
                    ddlTemplateRole.SelectedValue = role
                ElseIf strRoleSeq.Length = 2 Then
                    For i = 0 To _ddlHiddenRoles.Items.Count - 1
                        ddlTemplateRole.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                        ddlTemplateRole2.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                    Next
                    role = strRoleSeq(0)
                    ddlTemplateRole.SelectedValue = role
                    role = strRoleSeq(1)
                    ddlTemplateRole2.SelectedValue = role
                ElseIf strRoleSeq.Length = 3 Then
                    For i = 0 To _ddlHiddenRoles.Items.Count - 1
                        ddlTemplateRole.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                        ddlTemplateRole2.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                        ddlTemplateRole3.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                    Next
                    role = strRoleSeq(0)
                    ddlTemplateRole.SelectedValue = role
                    role = strRoleSeq(1)
                    ddlTemplateRole2.SelectedValue = role
                    role = strRoleSeq(2)
                    ddlTemplateRole3.SelectedValue = role
                ElseIf strRoleSeq.Length = 4 Then
                    For i = 0 To _ddlHiddenRoles.Items.Count - 1
                        ddlTemplateRole.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                        ddlTemplateRole2.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                        ddlTemplateRole3.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                        ddlTemplateRole4.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                    Next
                    role = strRoleSeq(0)
                    ddlTemplateRole.SelectedValue = role
                    role = strRoleSeq(1)
                    ddlTemplateRole2.SelectedValue = role
                    role = strRoleSeq(2)
                    ddlTemplateRole3.SelectedValue = role
                    role = strRoleSeq(3)
                    ddlTemplateRole4.SelectedValue = role
                ElseIf strRoleSeq.Length = 5 Then
                    For i = 0 To _ddlHiddenRoles.Items.Count - 1
                        ddlTemplateRole.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                        ddlTemplateRole2.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                        ddlTemplateRole3.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                        ddlTemplateRole4.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                        ddlTemplateRole5.Items.Insert(i, New ListItem(_ddlHiddenRoles.Items(i).Text, _ddlHiddenRoles.Items(i).Value))
                    Next
                    role = strRoleSeq(0)
                    ddlTemplateRole.SelectedValue = role
                    role = strRoleSeq(1)
                    ddlTemplateRole2.SelectedValue = role
                    role = strRoleSeq(2)
                    ddlTemplateRole3.SelectedValue = role
                    role = strRoleSeq(3)
                    ddlTemplateRole4.SelectedValue = role
                    role = strRoleSeq(4)
                    ddlTemplateRole5.SelectedValue = role
                End If

            End If

            If ddlTemplateRole IsNot Nothing Then
                ddlTemplateRole.Attributes.Add("onfocus", "copyDDL('" & _ddlHiddenRoles.ClientID & "','" & ddlTemplateRole.ClientID & "');javascript: this.style.width='100%';")
                'ddlTemplateRole.Attributes.Add("onblur", "javascript: this.style.width='100%';")
            End If
            If ddlTemplateRole2 IsNot Nothing Then
                ddlTemplateRole2.Attributes.Add("onfocus", "copyDDL('" & _ddlHiddenRoles.ClientID & "','" & ddlTemplateRole2.ClientID & "');javascript: this.style.width='100%';")
                'ddlTemplateRole2.Attributes.Add("onblur", "javascript: this.style.width='100%';")
            End If
            If ddlTemplateRole3 IsNot Nothing Then
                ddlTemplateRole3.Attributes.Add("onfocus", "copyDDL('" & _ddlHiddenRoles.ClientID & "','" & ddlTemplateRole3.ClientID & "');javascript: this.style.width='100%';")
                'ddlTemplateRole3.Attributes.Add("onblur", "javascript: this.style.width='100%';")
            End If
            If ddlTemplateRole4 IsNot Nothing Then
                ddlTemplateRole4.Attributes.Add("onfocus", "copyDDL('" & _ddlHiddenRoles.ClientID & "','" & ddlTemplateRole4.ClientID & "');javascript: this.style.width='100%';")
                'ddlTemplateRole4.Attributes.Add("onblur", "javascript: this.style.width='100%';")
            End If
            If ddlTemplateRole5 IsNot Nothing Then
                ddlTemplateRole5.Attributes.Add("onfocus", "copyDDL('" & _ddlHiddenRoles.ClientID & "','" & ddlTemplateRole5.ClientID & "');javascript: this.style.width='100%';")
                'ddlTemplateRole5.Attributes.Add("onblur", "javascript: this.style.width='100%';")
            End If

        End If

    End Sub

    Protected Sub _grvTasksPage_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles _grvTasksPage.PageIndexChanging
        GetTemplateTasks()
        _grvTasksPage.PageIndex = e.NewPageIndex
        _grvTasksPage.DataBind()
    End Sub

    Protected Sub GetTemplateTasks(Optional ByVal strTemplateSeqID As String = Nothing)
        If strTemplateSeqID <> "" Then
            _ddlTemplateTasks.SelectedValue = strTemplateSeqID
        End If
        'If Me._ddlTemplateTasks.SelectedValue <> "" Then
        '    Me._ddlTemplateTasks.Enabled = True
        'End If

        'Dim strTemplateName As String = _ddlTemplateTasks.SelectedItem.Text

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

                    With _ddlHiddenRoles
                        .DataSource = ds.Tables(0).CreateDataReader()
                        .DataTextField = "roledescription"
                        .DataValueField = "roleseqid"
                        .DataBind()
                    End With
                    With _ddlNewPrimaryRole
                        .DataSource = ds.Tables(0).CreateDataReader()
                        .DataTextField = "roledescription"
                        .DataValueField = "roleseqid"
                        .DataBind()
                    End With

                End If
            End If

            paramCollection.Clear()

            key = "GetTemplateTasks_" & Me._ddlTemplateTasks.SelectedItem.Value
            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "in_TaskHeader"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me._ddlTemplateTasks.SelectedValue
            paramCollection.Add(param)

            param = New Devart.Data.Oracle.OracleParameter
            param.ParameterName = "rsTaskItemList"
            param.OracleDbType = Devart.Data.Oracle.OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.GetOutageTemplateTaskItemList", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    'Tasks                     
                    Dim dr As System.Data.DataTableReader = ds.Tables(0).CreateDataReader
                    Dim dt As System.Data.DataTable = ds.Tables(0)
                    Me._grvTasksPage.DataSource = dt
                    _grvTasksPage.AutoGenerateColumns = False
                    _grvTasksPage.DataBind()
                    _grvTasksPage.Visible = True
                    _tbTemplateName.Text = _ddlTemplateTasks.SelectedValue
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

    'Protected Sub _btnCreateNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnCreateNew.Click
    '    Dim msg As New StringBuilder
    '    '     If _ddlTemplateTasks.Enabled = "True" And 
    '    msg.Append(Master.RIRESOURCES.GetResourceValue("You have selected a template.  Any tasks associated with this template will be created automatically.  Click OK to save this Outage record and create the tasks."))
    '    If msg.ToString.Length > 0 Then
    '        msg.Append("</ul>")
    '        _messageBox.Title = "Template Tasks"
    '        _messageBox.Message = Master.RIRESOURCES.GetResourceValue("WARNING:") & "<br><ul>" & msg.ToString & "<br>"
    '        ' & Master.RIRESOURCES.GetResourceValue("Do you want to continue?")
    '        _messageBox.Width = 400
    '        _messageBox.CancelScript = "disableButton(); return false;"
    '        '_MessageBox.OKScript = "javascript:window.location='" & Page.ResolveClientUrl("~/outage/Enteroutage.aspx?OutageNumber=" & Request.QueryString("OutageNumber")) & "'"
    '        _messageBox.ShowMessage()
    '    End If

    'End Sub

    Protected Sub _ddlTemplateTasks_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlTemplateTasks.SelectedIndexChanged
        Me._lblStatus.Text = ""
        If Me._ddlTemplateTasks.SelectedValue <> "" Then
            'If _ddlTemplateTasks.SelectedValue = "--New Template--" Then
            '    Me._divNewTemplate.Visible = True
            '    _tblNewRow.Visible = True
            '    _lblStatus.Text = "Enter New Template Name and task."
            '    Me._grvTasksPage.DataSource = Nothing
            '    Me._grvTasksPage.DataBind()
            'Else
            Me._divNewTemplate.Visible = False
            _lblTemplateHeading.Visible = "True"

            GetTemplateTasks(_ddlTemplateTasks.SelectedValue)
            _tblNewRow.Visible = True
            _btnNewCurrent.Visible = True
            If Me._grvTasksPage.Rows.Count > 0 Then
                _lblTemplateHeading.Text = "These are the tasks associated with the selected template.<br>To Add a Task, fill in the fields above and click Add.<br>Click Save Changes to Current Template to save any changes to the tasks below."
                _btnSaveTemplate.Visible = True
                _btnNewCurrent.Visible = True
            Else
                _lblTemplateHeading.Text = "There are no tasks associated with the template.  Please enter above information and click Add button to add a task."
                Me._btnSaveTemplate.Visible = "false"
                _btnNewCurrent.Visible = False
            End If
            'End If
        Else
            Me._grvTasksPage.DataSource = Nothing
        Me._grvTasksPage.DataBind()
        _tblNewRow.Visible = False
        _btnNewCurrent.Visible = False
            _btnSaveTemplate.Visible = False
            _lblTemplateHeading.Visible = False
        End If
    End Sub

    Protected Sub _btnCreateNewTemplate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnCreateNewCurrentTemplate.Click
        '_rfvTemplateDesc.Enabled = True
        Dim strTemplateName As String
        strTemplateName = Me._tbTemplateName.Text
        currentTemplateTask = New clsCurrentTemplateTask
        Dim returnStatus As String
        returnStatus = currentTemplateTask.CreateNewOutageMTTTemplate(userProfile.Username, Me._ddlTemplateTasks.SelectedValue, strTemplateName)
        If returnStatus = 0 Then
            Me._lblStatus.Text = "New Template and Tasks Created Successfully"
            currentOutageTemplate = New clsOutageTemplate()
            RI.SharedFunctions.BindList(Me._ddlTemplateTasks, currentOutageTemplate.OutageTemplate, False, True)
        End If
    End Sub


    Protected Sub _btnCreateTemplate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnCreateTemplate.Click
        _rfvNewTemplate.Enabled = True
        Dim strTemplateName As String
        strTemplateName = Me._tbNewTemplateName.Text
        currentTemplateTask = New clsCurrentTemplateTask
        Dim returnStatus As String
        returnStatus = currentTemplateTask.CreateNewTemplateHeader(userProfile.Username, strTemplateName)
        If returnStatus = 0 Then
            Me._lblStatus.Text = "New Template Created Successfully"
            currentOutageTemplate = New clsOutageTemplate()
            RI.SharedFunctions.BindList(Me._ddlTemplateTasks, currentOutageTemplate.OutageTemplate, False, True)
            Response.Redirect(Page.AppRelativeVirtualPath & "?TemplateSeqID=" & currentTemplateTask.TemplateSeqID, True)
            'Me._ddlTemplateTasks.SelectedValue = currentTemplateTask.TemplateSeqID
        End If
    End Sub

    Protected Sub _btnNewTask_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnNewTask.Click
        'If Me._ddlTemplateTasks.SelectedValue = "--New Template--" Then
        'Dim strTemplateName As String
        'strTemplateName = Me._tbNewTemplate.Text
        currentTemplateTask = New clsCurrentTemplateTask
        'Dim returnStatus As String
        'returnStatus = currentTemplateTask.CreateNewTemplateHeader(userProfile.Username, strTemplateName)
        'If returnStatus = 0 Then
        Me._lblStatus.Text = "New Template Created Successfully"
        currentOutageTemplate = New clsOutageTemplate()
        CreateNewTask(currentTemplateTask.TemplateSeqID)
        RI.SharedFunctions.BindList(Me._ddlTemplateTasks, currentOutageTemplate.OutageTemplate, False, True)
        Response.Redirect(Page.AppRelativeVirtualPath & "?TemplateSeqID=" & currentTemplateTask.TemplateSeqID, True)
        'Me._ddlTemplateTasks.SelectedValue = currentTemplateTask.TemplateSeqID
        'End If
        'End If
    End Sub
    Protected Sub CreateNewTask(Optional ByVal strTemplateSeqId As String = Nothing)
        currentTemplateTask = New clsCurrentTemplateTask
        Dim returnStatus As String

        With currentTemplateTask
            .Title = Me._tbNewTitle.Text
            .Description = Me._tbNewDesc.Text
            .weeksAfter = Me._tbNewWeeksAfter.Text
            .weeksBefore = Me._tbNewWeeksBefore.Text
            .leadTimeDays = Me._tbNewLeadTime.Text
            .primaryRole = Me._ddlNewPrimaryRole.SelectedValue
            .TemplateSeqID = Me._ddlTemplateTasks.SelectedValue
            .TaskSeqID = 0
            .UserName = userProfile.Username
        End With

        returnStatus = currentTemplateTask.SaveTemplateTasks()

        If returnStatus <> "0" Then
            Me._lblStatus.Text = "Error Occurred During Save."
        Else
            Me._lblStatus.Text = "Updates successfully saved."
            'GetTemplateTasks()
            Me._tbNewTitle.Text = ""
            Me._tbNewDesc.Text = ""
            Me._tbNewWeeksAfter.Text = ""
            Me._tbNewWeeksBefore.Text = ""
            'GetTemplateTasks(Me._ddlTemplateTasks.SelectedValue)
        End If

        'SaveOutage(True)

        Response.Redirect(Page.AppRelativeVirtualPath & "?TemplateSeqID=" & Me._ddlTemplateTasks.SelectedValue, True)
    End Sub

    Protected Sub _btnNewCurrent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnNewCurrent.Click
        _rfvTemplateDesc.Enabled = "true"
    End Sub
End Class
