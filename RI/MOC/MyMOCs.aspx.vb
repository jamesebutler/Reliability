Imports RI
Imports RI.SharedFunctions
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Devart.Data.Oracle

Partial Class MOC_MyMOCs
    Inherits RIBasePage

    Dim currentMOC As clsCurrentMOCDetail
    Dim currentMOCDetail As clsCurrentMOC
    Dim userProfile As RI.CurrentUserProfile = Nothing


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.ShowMOCMenu()
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("MyMOCs", True, "MOC"))
    End Sub

    Function GetUserid() As String
        Dim userProfile As RI.CurrentUserProfile = Nothing
        userProfile = RI.SharedFunctions.GetUserProfile
        Return userProfile.Username
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ScriptManager.RegisterClientScriptInclude(Me.Page, Page.GetType, "MyMOCs", Page.ResolveClientUrl("~/moc/MyMOCs.js"))
        GetMOCs()

    End Sub
    Protected Sub GetMOCs()
        userProfile = RI.SharedFunctions.GetUserProfile
        Dim clsSearch As New clsMyMOCs

        clsSearch.Approver = GetUserid()
        Dim CallSource As String = String.Empty
        CallSource = "View"
        Master.RIRESOURCES.GetResourceValue("MyMOCs", True, "MOC")

        Me._gvMOCListing.DataSource = clsSearch.Search
        Me._gvMOCListing.DataBind()
            Me._gvMOCListing.Visible = True
            Dim RecordCount As Integer = _gvMOCListing.Rows.Count
            Me._lblRecCount.Text = RecordCount & " " & Master.RIRESOURCES.GetResourceValue("MOCs are awaiting your approval or review.")
            If RecordCount = 0 Then
                Me._btnSave.Visible = False
            End If

            Me._gvMOCDraftListing.DataSource = clsSearch.DraftsDT
            Me._gvMOCDraftListing.DataBind()
            Me._gvMOCDraftListing.Visible = True
            Dim DraftRecordCount As Integer = _gvMOCDraftListing.Rows.Count
            If DraftRecordCount = 0 Then
                _spDrafts.Visible = False
            Else
            _lblInterestHeader.Visible = True
            Me._lbDrafts.Text = DraftRecordCount & " MOCs " & Master.RIRESOURCES.GetResourceValue(" Draft ")
        End If

            Me._gvMOCOnHoldListing.DataSource = clsSearch.OnHoldDT
            Me._gvMOCOnHoldListing.DataBind()
            Me._gvMOCOnHoldListing.Visible = True
            Dim OnHoldRecordCount As Integer = _gvMOCOnHoldListing.Rows.Count
        If OnHoldRecordCount = 0 Then
            _spOnHold.Visible = False
        Else
            _lblInterestHeader.Visible = True
            Me._lbOnHold.Text = OnHoldRecordCount & " MOCs " & Master.RIRESOURCES.GetResourceValue(" On Hold")
            End If

            Me._gvImplOverride.DataSource = clsSearch.ImplOverrideDT
            Me._gvImplOverride.DataBind()
            Me._gvImplOverride.Visible = True
            Dim implRecordCount As Integer = _gvImplOverride.Rows.Count
            If implRecordCount > 0 Then
            Me._lbImplOverride.Text = implRecordCount & " MOCs " & Master.RIRESOURCES.GetResourceValue(" Implemented with open tasks WITHOUT All Approvals")
            Me._lbImplOverride.ForeColor = System.Drawing.Color.Red
            Else
                _spImplOverride.Visible = False
            End If

            Me._gvComplOverride.DataSource = clsSearch.CompOverrideDT
            Me._gvComplOverride.DataBind()
            Me._gvComplOverride.Visible = True
            Dim ComplRecordCount As Integer = _gvComplOverride.Rows.Count
            If ComplRecordCount > 0 Then
            Me._lbComplOverride.Text = ComplRecordCount & " MOCs " & Master.RIRESOURCES.GetResourceValue(" Implemented and tasks completed WITHOUT All Approvals")
            Me._lbComplOverride.ForeColor = System.Drawing.Color.Red
            Else
                _spComplOverride.Visible = False
            End If

            Me._gvPendingOwner.DataSource = clsSearch.PendingOwnerDT
            Me._gvPendingOwner.DataBind()
            Me._gvPendingOwner.Visible = True
            Dim PendingOwnRecordCount As Integer = _gvPendingOwner.Rows.Count
            If PendingOwnRecordCount = 0 Then
                _spPendingOwner.Visible = False
            Else
            _lblInterestHeader.Visible = True
            Me._lbPendingOwner.Text = PendingOwnRecordCount & " MOCs " & Master.RIRESOURCES.GetResourceValue(" Pending that you are the Initiator or Owner")
        End If

            Me._gvApprovedNotImpl.DataSource = clsSearch.ApprovedNotImplemDT
            Me._gvApprovedNotImpl.DataBind()
            Me._gvApprovedNotImpl.Visible = True
            Dim ApproveNotImplRecordCount As Integer = _gvApprovedNotImpl.Rows.Count
            If ApproveNotImplRecordCount = 0 Then
                _spApprovedNotImplemented.Visible = False
            Else
                Me._lbApproveNotImpl.Text = ApproveNotImplRecordCount & " MOCs " & Master.RIRESOURCES.GetResourceValue(" Approved that have not been Implemented")
            End If


    End Sub
    Protected Sub _btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnSave.Click

        Dim i As Integer = 0
        Dim intApprovalSeqid, intRow As Integer
        Dim strComments As String = String.Empty
        Dim strApproval As String = String.Empty
        Dim strLevel As String = String.Empty
        Dim strMOCNumber As String = String.Empty
        Dim cblApproval As CheckBoxList
        Dim tbComments As TextBox
        Dim lbLevel, lbMoCNumber As Label
        Dim hfEmail As HiddenField
        Dim returnStatus As String = ""
        Dim approvalEmailMsg As String = ""
        Dim approvalEmailSubMsg As String = ""

        'for me._gvMOCListing.dirtyrows
        For i = 0 To _gvMOCListing.Rows.Count - 1
            intRow = _gvMOCListing.Rows.Item(i).DataItemIndex
            intApprovalSeqID = _gvMOCListing.DataKeys.Item(intRow).Value
            lbMOCNumber = CType(_gvMOCListing.Rows(intRow).FindControl("_lblMOCNumber"), Label)
            cblApproval = CType(_gvMOCListing.Rows(intRow).FindControl("_cbApproval"), CheckBoxList)
            tbComments = CType(_gvMOCListing.Rows(intRow).FindControl("_tbComment"), TextBox)
            lbLevel = CType(_gvMOCListing.Rows(intRow).FindControl("_lbLevel"), Label)
            hfEmail = CType(_gvMOCListing.Rows(intRow).FindControl("_hfInitiatorEmail"), HiddenField)

            strMOCNumber = lbMoCNumber.Text
            strLevel = lbLevel.Text
            strComments = tbComments.Text
            strApproval = cblApproval.SelectedValue
            strApproval = Mid(strApproval, 1, 1)
            'strInitiatorEmail = hfEmail.Value
            Dim strEmailList As String = ""
            Dim strCCEmailList As String = ""

            If strApproval <> "" Then

                'Get MOC Detail information for sending to appropriate people and listing correct information if any emails are sent.
                currentMOC = New clsCurrentMOCDetail(strMOCNumber)
                strCCEmailList = currentMOC.MOCCoordinatorEmail & "," & currentMOC.MOCOwnerEmail

                'If just checking that MOC was reviewed, update moc approval table.  No notification, task creation or USD creation needed.
                returnStatus = currentMOC.SaveMOCApproval(intApprovalSeqid, strMOCNumber, GetUserid(), strApproval, strLevel, strComments)

                If returnStatus = "999" Then
                    'MOC was not approved.  Email Initiator and current level approvers.
                    If strLevel = "L1" Then
                        strEmailList = currentMOC.NotificationL1List
                    ElseIf strLevel = "L2" Then
                        strEmailList = currentMOC.NotificationL1List & "," & currentMOC.NotificationL2List
                    ElseIf strLevel = "L3" Then
                        strEmailList = currentMOC.NotificationL1List & "," & currentMOC.NotificationL2List & "," & currentMOC.NotificationL3List
                    End If
                    EmailNotApproved(strMOCNumber, strEmailList, strCCEmailList, strComments)
                ElseIf returnStatus = "888" Then
                    'MOC level was approved.  Need to check each notification list to see who should get the Email.
                    If strLevel = "L1" And currentMOC.NotificationL2List <> "" Then
                        strEmailList = currentMOC.NotificationL1List & "," & currentMOC.NotificationL2List
                        approvalEmailMsg = RI.SharedFunctions.LocalizeValue("MOC Approval Requested")
                        approvalEmailSubMsg = RI.SharedFunctions.LocalizeValue("L1L2") '"All Level 1 Approvers have approved this MOC and awaiting Level 2 approval"
                        'approvalEmailSubMsg = "All Level 1 Approvers have approved this MOC and awaiting Level 2 approval"
                    ElseIf strLevel = "L1" And currentMOC.NotificationL2List = "" Then
                        approvalEmailSubMsg = RI.SharedFunctions.LocalizeValue("MOC Approved")
                        strEmailList = currentMOC.NotificationL1List & "," & currentMOC.NotificationEList
                    ElseIf strLevel = "L2" And currentMOC.NotificationL3List <> "" Then
                        approvalEmailMsg = RI.SharedFunctions.LocalizeValue("MOC Approval Requested")
                        approvalEmailSubMsg = "All Level 1 & Level 2 Approvers have approved this MOC and awaiting Level 3 approval"
                        strEmailList = currentMOC.NotificationL1List & "," & currentMOC.NotificationL2List & "," & currentMOC.NotificationL3List
                    ElseIf strLevel = "L2" And currentMOC.NotificationL3List = "" Then
                        approvalEmailSubMsg = RI.SharedFunctions.LocalizeValue("MOC Approved")
                        strEmailList = currentMOC.NotificationL1List & "," & currentMOC.NotificationL2List & "," & currentMOC.NotificationEList
                    ElseIf strLevel = "L3" Then
                        approvalEmailSubMsg = RI.SharedFunctions.LocalizeValue("MOC Approved") & " - " & currentMOC.BusinessArea & ": " & currentMOC.Title
                        strEmailList = currentMOC.NotificationL1List & "," & currentMOC.NotificationL2List & "," & currentMOC.NotificationL3List & "," & currentMOC.NotificationEList
                        'Call the create system tasks proc.  Proc double-checks that MOC was approved.
                        currentMOC.CreateSystemTasks(strMOCNumber)
                    Else
                        approvalEmailSubMsg = RI.SharedFunctions.LocalizeValue("MOC Approved") & " - " & currentMOC.BusinessArea & ": " & currentMOC.Title
                        strEmailList = currentMOC.NotificationL1List & "," & currentMOC.NotificationL2List & "," & currentMOC.NotificationL3List & "," & currentMOC.NotificationEList
                        'Call the create system tasks proc.  Proc double-checks that MOC was approved.
                        currentMOC.CreateSystemTasks(strMOCNumber)
                    End If
                    'approvalEmailMsg = "MOC Approved - " & currentMOC.BusinessUnit & ": " & currentMOC.Title
                    'This indicates that all L1 approvers have approved the moc and an email should be sent to L2 approvers.
                    'EmailApproved(strMOCNumber, strEmailList, userProfile.Email, Master.RIRESOURCES.GetResourceValue("MOC Approved") & " - " & currentMOC.BusinessArea & ": " & currentMOC.Title)
                    EmailApproved(strMOCNumber, strEmailList, strCCEmailList, approvalEmailMsg, approvalEmailSubMsg)
                    ''Call the create system tasks proc.  Proc double-checks that MOC was approved.
                    ''currentMOC.CreateSystemTasks(strMOCNumber)
                ElseIf returnStatus = "777" Then
                    'Level MOC approved.  Need to email next level approvers so they know about the MOC.
                    If strLevel = "L1" And currentMOC.NotificationL2List <> "" Then
                        strEmailList = currentMOC.NotificationL2List
                        EmailApproved(strMOCNumber, strEmailList, userProfile.Email, Master.RIRESOURCES.GetResourceValue("All Level 1 Approvers have approved this MOC and awaiting Level 2 approval"))
                    ElseIf strLevel = "L2" And currentMOC.NotificationL3List <> "" Then
                        strEmailList = currentMOC.NotificationL3List
                        EmailApproved(strMOCNumber, strEmailList, userProfile.Email, Master.RIRESOURCES.GetResourceValue("All Level 1 & Level 2 Approvers have approved this MOC and awaiting Level 3 approval"))
                    End If
                End If
                If currentMOC.USDTicket <> "Y" Then
                    Dim strSummary = currentMOC.SiteID & " - MOC " & strMOCNumber & " " & currentMOC.Title 'currentMOC.Title
                    Dim strDesc = currentMOC.SiteID & " - MOC " & strMOCNumber & " initiated by " & currentMOC.MOCCoordinator & " http://ri/RI/MOC/entermoc.aspx?mocnumber=" & strMOCNumber & " " & currentMOC.Title
                    If RI.SharedFunctions.DataClean(currentMOC.SubCategory).Length > 0 Then

                        If currentMOC.SubCategory.Contains("PI") = True Or currentMOC.SubCategory.Contains("Proficy") Then
                            Me.CreateUSDTicket(currentMOC.SiteID, strSummary, strDesc, currentMOC.MOCCoordinatorName, strMOCNumber)
                        End If
                    End If

                End If
            End If

        Next
        Response.Redirect(Page.AppRelativeVirtualPath, True)

    End Sub

    Protected Sub _gvMOCListing_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _gvMOCListing.RowDataBound
        'If it a pending review MOCS, they should only have option to select Yes
        Dim lbApproveCheckBox As CheckBoxList = CType(e.Row.Cells(0).FindControl("_cbApproval"), CheckBoxList)
        Dim lbComments As TextBox = CType(e.Row.Cells(0).FindControl("_tbComment"), TextBox)
        Dim lbType As Label = CType(e.Row.Cells(0).FindControl("_lbLevel"), Label)

        If e.Row.RowIndex >= 0 Then
            Dim appType As String
            appType = Trim(lbType.Text)
            If appType <> "E" Then
                'If sender.DataKeys(e.Row.RowIndex).Value = userProfile.Username Then
                lbApproveCheckBox.Items.Add(RI.SharedFunctions.LocalizeValue("No"))
            End If
        End If
    End Sub

    Private Sub EmailNotApproved(ByVal MOCNumber As String, Optional ByVal toList As String = "", Optional ByVal ccList As String = "", Optional ByVal Comments As String = "")

        Dim Body As New StringBuilder
        Dim commonBody As New StringBuilder
        Dim spacer As String = "&nbsp;&nbsp;&nbsp;&nbsp;"
        Dim url As String = String.Empty
        Dim urlUpdateMOC As String = "<p><a href='{0}'>" & Master.RIRESOURCES.GetResourceValue("Click here to View/Update Information") & "</a></p>"
        Dim additionalText As String = "<h2>**** THIS IS A TEST MOC NOTIFICATION ****</H2>" & toList
        Dim urlHost As String
        Dim MOCStatus As String = String.Empty

        Dim subjectForNotificationList As String = ""

        'Dim MOCNumber As String = Request.QueryString("MOCNumber")
        currentMOCDetail = New clsCurrentMOC(MOCNumber)
        subjectForNotificationList = Master.RIRESOURCES.GetResourceValue("MOC NOT Approved")
        If currentMOCDetail Is Nothing Then Throw New Exception("Error Getting Current MOC " & MOCNumber)

        If Request.UserHostAddress = "127.0.0.1" Then
            urlHost = "http://gpiri.graphicpkg.com/moc/"
        Else
            If Request.ServerVariables("HTTP_HOST").ToLower.Contains("ridev") Then
                urlHost = "Http://ridev.ipaper.com/ri/moc/"
            ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritest") Then
                urlHost = "Http://ritest.ipaper.com/ri/moc/"
            ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritrain") Then
                urlHost = "Http://ritrain.ipaper.com/ri/moc/"
            Else
                urlHost = "http://gpiri.graphicpkg.com/moc/"
                additionalText = String.Empty
            End If

        End If

        url = urlHost & "EnterMOC.aspx?MOCNumber=" & currentMOCDetail.MOCNumber & "#Approval"
        Body.Append("<html><head><title>" & Master.RIRESOURCES.GetResourceValue("Assign") & "</title></head>")
        Body.Append("<body bgcolor=""#FFFFFF"">")
        Body.Append(additionalText)
        Body.Append("<p><font size = ""2"" face=""Arial""><strong>")

        Body.Append(Master.RIRESOURCES.GetResourceValue("This MOC was not approved by") & " " & userProfile.FullName & ".<br>")
        Body.Append(Comments & "</strong><br></p>")

        '*****Start Common Body***************
        'commonBody.AppendLine("<br>")
        commonBody.Append("<br>")
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("MOC Number") & ":</strong>" & spacer & currentMOCDetail.MOCNumber)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Title") & ":</strong>" & spacer & currentMOCDetail.Title)
        If currentMOCDetail.Description.Length > 0 Then
            commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Description") & ":</strong>" & spacer & RI.SharedFunctions.DataClean(currentMOCDetail.Description))
        End If

        commonBody.Append("<strong><li>" & RI.SharedFunctions.LocalizeValue("Initiator/Owner") & ":</strong>" & spacer & Me.currentMOCDetail.MOCCoordinatorName & "/" & Me.currentMOCDetail.OwnerName)
        commonBody.Append("<strong><li>" & RI.SharedFunctions.LocalizeValue("Implementation Date") & ":</strong>" & spacer & RI.SharedFunctions.LocalizeValue(currentMOCDetail.StartDate))
        commonBody.Append("<strong><li>" & RI.SharedFunctions.LocalizeValue("Expiration Date") & ":</strong>" & spacer & RI.SharedFunctions.LocalizeValue(currentMOCDetail.EndDate))
        commonBody.Append("<strong><li>" & RI.SharedFunctions.LocalizeValue("Area") & ":</strong>" & spacer & RI.SharedFunctions.LocalizeValue(currentMOCDetail.BusinessUnit) & " " & RI.SharedFunctions.LocalizeValue(currentMOCDetail.Line))
        commonBody.Append("<strong><li>" & RI.SharedFunctions.LocalizeValue("Type") & ":</strong>" & spacer & RI.SharedFunctions.LocalizeValue(currentMOCDetail.MOCType))
        ''commonBody.Append("<strong><li>Category:</strong>" & spacer & Replace(Mid(Me.currentMOC.Category, 2), ",", ", "))
        commonBody.Append("<strong><li>" & RI.SharedFunctions.LocalizeValue("Classification") & ":</strong>" & spacer & RI.SharedFunctions.LocalizeValue(Me.currentMOCDetail.Classification))
        commonBody.Append("<strong><li>" & RI.SharedFunctions.LocalizeValue("Level 1 Approvers") & ":</strong>" & spacer & Me.currentMOCDetail.NotificationL1FullName)
        commonBody.Append("<strong><li>" & RI.SharedFunctions.LocalizeValue("Level 2 Approvers") & ":</strong>" & spacer & Me.currentMOCDetail.NotificationL2FullName)
        commonBody.Append("<strong><li>" & RI.SharedFunctions.LocalizeValue("Level 3 Approvers") & ":</strong>" & spacer & Me.currentMOCDetail.NotificationL3FullName)
        commonBody.Append("<strong><li>" & RI.SharedFunctions.LocalizeValue("Informed") & ":</strong>" & spacer & Me.currentMOCDetail.NotificationEFullName)

        commonBody.Append("</ul>")
        '******End Common Body ************

        Body.Append(commonBody.ToString)

        urlUpdateMOC = String.Format(urlUpdateMOC, url)

        Body.Append(urlUpdateMOC)
        Body.Append("</body></html>")

        Dim msgForNotificationList As String = Body.ToString 'String.Format(Body.ToString, 0)

        RI.SharedFunctions.SendEmail(toList, userProfile.Email, subjectForNotificationList, msgForNotificationList, userProfile.FullName, ccList)
        'RI.SharedFunctions.SendEmail("Amy.Albrinck@ipaper.com", userProfile.Email, subjectForNotificationList, msgForNotificationList, userProfile.FullName, "amy.albrinck@ipaper.com")

    End Sub

    Private Sub EmailApproved(ByVal MOCNumber As String, Optional ByVal toList As String = "", Optional ByVal ccList As String = "", Optional ByVal Comments As String = "", Optional ByVal SubMsg As String = "")

        Dim Body As New StringBuilder
        Dim commonBody As New StringBuilder
        Dim spacer As String = "&nbsp;&nbsp;&nbsp;&nbsp;"
        Dim url As String = String.Empty
        Dim urlUpdateMOC As String = "<p><a href='{0}'>" & RI.SharedFunctions.LocalizeValue("Click here to View/Update Information") & "</a></p>"
        Dim additionalText As String = "<h2>**** THIS IS A TEST MOC NOTIFICATION ****</H2>" & toList
        Dim EmailSubMsg As String = SubMsg
        
        Dim urlHost As String
        Dim MOCStatus As String = String.Empty

        Dim subjectForNotificationList As String = EmailSubMsg

        currentMOCDetail = New clsCurrentMOC(MOCNumber)

        If currentMOCDetail Is Nothing Then Throw New Exception("Error Getting Current MOC " & MOCNumber)


        If Request.UserHostAddress = "127.0.0.1" Then
            urlHost = "http://gpiri.graphicpkg.com/moc/"
        Else
            If Request.ServerVariables("HTTP_HOST").ToLower.Contains("ridev") Then
                urlHost = "Http://ridev.ipaper.com/ri/moc/"
            ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritest") Then
                urlHost = "Http://ritest.ipaper.com/ri/moc/"
            ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritrain") Then
                urlHost = "Http://ritrain/ri/moc/"
            Else
                urlHost = "http://gpiri.graphicpkg.com/moc/"
                additionalText = String.Empty
            End If

        End If

        url = urlHost & "EnterMOC.aspx?MOCNumber=" & currentMOCDetail.MOCNumber & "#Approval"
        Body.Append("<html><head><title>Assign</title></head>")
        Body.Append("<body bgcolor=""#FFFFFF"">")
        Body.Append(additionalText)
        Body.Append("<p><font size = ""2"" face=""Arial""><strong>")
        Body.Append(EmailSubMsg)

        Body.Append("</strong><br></p>")

        '*****Start Common Body***************
        commonBody.Append("<br>")
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("MOCNumber") & ":</strong>" & spacer & currentMOCDetail.MOCNumber)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Title") & ":</strong>" & spacer & currentMOCDetail.Title)
        If currentMOCDetail.Description.Length > 0 Then
            commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Description") & ":</strong>" & spacer & RI.SharedFunctions.DataClean(currentMOCDetail.Description))
        End If

        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Initiator/Owner") & ":</strong>" & spacer & Me.currentMOC.MOCCoordinatorName & "/" & currentMOC.MOCOwnerName)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Implementation Date") & ":</strong>" & spacer & currentMOCDetail.StartDate)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Expiration Date") & ":</strong>" & spacer & currentMOCDetail.EndDate)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Area") & ":</strong>" & spacer & currentMOCDetail.BusinessUnit & " " & currentMOCDetail.Line)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Type") & "Type:</strong>" & spacer & currentMOCDetail.MOCType)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Classification") & ":</strong>" & spacer & Me.currentMOCDetail.Classification)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Level 1 Approvers") & ":</strong>" & spacer & Me.currentMOCDetail.NotificationL1FullName)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Level 2 Approvers") & ":</strong>" & spacer & Me.currentMOCDetail.NotificationL2FullName)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Level 3 Approvers") & ":</strong>" & spacer & Me.currentMOCDetail.NotificationL3FullName)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Informed") & ":</strong>" & spacer & Me.currentMOCDetail.NotificationEFullName)

        commonBody.Append("</ul>")

        '******End Common Body ************

        Body.Append(commonBody.ToString)

        urlUpdateMOC = String.Format(urlUpdateMOC, url)

        Body.Append(urlUpdateMOC)
        Body.Append("</body></html>")

        Dim msgForNotificationList As String = Body.ToString 'String.Format(Body.ToString, 0)

        RI.SharedFunctions.SendEmail(toList, currentMOCDetail.MOCCoordinatorEmail, subjectForNotificationList, msgForNotificationList, currentMOCDetail.MOCCoordinatorName, ccList)
        'RI.SharedFunctions.SendEmail("amy.albrinck@ipaper.com", currentMOC.MOCCoordinatorEmail, subjectForNotificationList, msgForNotificationList, userProfile.FullName, "amy.Albrinck@ipaper.com")

    End Sub

    Protected Sub CreateUSDTicket(ByVal Facility As String, ByVal Summary As String, ByVal Desc As String, ByVal ReportedBy As String, ByVal MOCNumber As String)

        Dim dbParam As New SqlParameter
        Dim dbCmd As New SqlClient.SqlCommand
        Dim cn As SqlClient.SqlConnection = Nothing

        Dim USDGenerationOn As String = ConfigurationManager.AppSettings("USDGeneration")
        Dim connectString As String
        Dim UniqueKey As String
        Dim key As String

        Try

            If USDGenerationOn <> "True" Then Exit Sub

            UniqueKey = Facility & "-MOC-" & Now()
            connectString = ConfigurationManager.ConnectionStrings.Item("USDSqlServer").ConnectionString

            cn = New SqlConnection(connectString)

            cn.Open()
            'dbCmd = dbPF.CreateCommand
            dbCmd.Connection = cn
            dbCmd.CommandType = CommandType.StoredProcedure
            dbCmd.CommandText = "dbo.up_Insert_Ticket"

            dbCmd.Parameters.Add("@FacilityAbbr", SqlDbType.VarChar, Facility.Length).Value = Facility
            dbCmd.Parameters.Add("@Application", SqlDbType.VarChar, 50).Value = "MOC"
            dbCmd.Parameters.Add("@Summary", SqlDbType.VarChar, 150).Value = Summary
            dbCmd.Parameters.Add("@Description", SqlDbType.VarChar, 250).Value = Desc
            dbCmd.Parameters.Add("@ReportedBy", SqlDbType.VarChar, 50).Value = ReportedBy
            dbCmd.Parameters.Add("@Priority", SqlDbType.Int, 2).Value = 4
            dbCmd.Parameters.Add("@Type", SqlDbType.VarChar, 100).Value = "RFS"
            dbCmd.Parameters.Add("@UniqueKey", SqlDbType.VarChar, 100).Value = UniqueKey

            'SqlParameter pvNewId = new SqlParameter();
            dbParam.ParameterName = "@IndexID"
            dbParam.DbType = DbType.String
            dbParam.Size = 100
            dbParam.Direction = ParameterDirection.Output
            dbCmd.Parameters.Add(dbParam)

            key = dbCmd.ExecuteNonQuery()
            If key = "-1" Then
                ' resave MOC to indicate ticket created.
                currentMOC.SaveMOCUSD(MOCNumber, key)
            End If
        Catch ex As Exception
            Throw
        Finally
            If Not dbCmd Is Nothing Then
                dbCmd = Nothing
            End If
            If Not (cn Is Nothing) Then
                If (cn.State = ConnectionState.Open) Then
                    cn.Close()
                End If
            End If
            If Not (dbParam Is Nothing) Then
                dbParam = Nothing
            End If
        End Try

    End Sub

End Class
