Option Explicit On
Option Strict On

Imports Devart.Data.Oracle
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports RI
'Namespace RI
Partial Class NEMAMaster
    Inherits System.Web.UI.MasterPage

    'Public RIRESOURCES As New IP.Bids.Localization.WebLocalization
    'Public DATEXREF As New IP.MTIS.Localization.DateTime
    'Public NUMBERXREF As New IP.MTIS.Localization.Numbers
    'Public CURRENCYXREF As New IP.MTIS.Localization.Currency

    Public Sub PageRefresh(ByVal minutes As Integer)
        Dim meta As New HtmlMeta
        meta.Attributes.Add("http-equiv", "refresh")
        meta.Attributes.Add("content", minutes.ToString)
        Head.Controls.AddAt(0, meta)

    End Sub
    Dim mUnloadEvents As Boolean = False
    Public Property UnloadEvents() As Boolean
        Get
            Return mUnloadEvents
        End Get
        Set(ByVal value As Boolean)
            mUnloadEvents = value
        End Set
    End Property
    Public Property DisplayBanner() As Boolean
        Get
            Return Me._banner.Visible
        End Get
        Set(ByVal value As Boolean)
            _banner.Visible = value
            _footer.Visible = value
        End Set
    End Property
    Public Property ContentHeight() As String
        Get
            Return Me._Content.Style.Item("Height")
        End Get
        Set(ByVal value As String)
            Me._Content.Style.Item("Height") = value
        End Set
    End Property
    Public Property MasterPageWidth() As String
        Get
            Return Me._tblMaster.Style.Item("Width")
        End Get
        Set(ByVal value As String)
            'style="width: 99%"
            Me._tblMaster.Style.Item("Width") = value
        End Set
    End Property
    Public Sub SetBanner(ByVal textMessage As String, Optional ByVal displayPopupBanner As Boolean = False)
        'Me._banner.SetBanner(textMessage, displayPopupBanner)
        If _banner IsNot Nothing Then
            Me._banner.BannerMessage = textMessage
            Me._banner.DisplayPopupBanner = displayPopupBanner
            Me._banner.UseMessageAsPageTitle = True
        End If
    End Sub
    Public Sub DisplayExcel(ByRef ds As System.Data.DataSet)
        Me._displayExcel.DisplayExcel(ds)
    End Sub
    Public Sub DisplayExcel(ByRef dr As OracleDataReader)
        Me._displayExcel.DisplayExcel(dr)
    End Sub
    Public Sub DisplayExcel(ByRef dr As System.Data.DataTableReader)
        Me._displayExcel.DisplayExcel(dr)
    End Sub
    Public Sub DisplayExcel(ByVal excelXML As String)
        Me._displayExcel.DisplayExcel(excelXML)
    End Sub
    Public Function GetPopupWindowJS(ByVal url As String, Optional ByVal pageName As String = "newWindow", Optional ByVal windowWidth As Integer = 300, Optional ByVal windowHeight As Integer = 300, Optional ByVal showScrollBars As Boolean = False, Optional ByVal showMenu As Boolean = False, Optional ByVal openFullScreen As Boolean = False) As String
        Dim sb As New StringBuilder
        sb.Append("javascript:PopupWindow('")
        sb.Append(Page.ResolveClientUrl(url))
        sb.Append("','")
        sb.Append(pageName)
        sb.Append("','")
        sb.Append(windowWidth)
        sb.Append("','")
        sb.Append(windowHeight)
        sb.Append("','")
        If showScrollBars = True Then
            sb.Append("yes")
        Else
            sb.Append("no")
        End If
        sb.Append("','")
        If showMenu = True Then
            sb.Append("yes")
        Else
            sb.Append("no")
        End If
        sb.Append("','")
        If openFullScreen = True Then
            sb.Append("true")
        Else
            sb.Append("false")
        End If
        sb.Append("');")
        Return sb.ToString
    End Function

    Public Function GetSaveChangesBeforeLeavingJS(ByVal url As String, Optional ByVal ConfirmMessage As String = "") As String
        Dim sb As New StringBuilder
        Dim returnValue As String = String.Empty
        If ConfirmMessage.Length = 0 Then
            'ConfirmMessage = RIRESOURCES.GetResourceValue("YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES", True, "Shared")
        End If

        ConfirmMessage = RI.SharedFunctions.DataClean(ConfirmMessage)
        ConfirmMessage = ConfirmMessage.Replace("'", "\'")
        sb.Append("javascript:viewIncident(""{0}"",""{1}"");")
        returnValue = String.Format(sb.ToString, url, ConfirmMessage)
        Return returnValue
    End Function

    Public Sub ShowPopUpMessage(ByVal message As String, ByVal title As String, ByVal buttonType As ucMessageBox.ButtonTypes, ByVal OKScript As String, ByVal CancelScript As String)
        With Me._ConfirmMessage
            .ButtonType = buttonType
            .Message = message
            .CancelScript = CancelScript
            .OKScript = OKScript
            .Title = title
            .ShowMessage()
        End With

    End Sub

    Public Sub SetCurrentUserLabel(ByVal currentUser As String, ByVal fullName As String)
        _lblCurrentUser.Text = GetDataBaseName() & ",  Current User: " & fullName & " (" & currentUser & ")"
    End Sub

    Public Function GetDataBaseName() As String
        If Application.Item("Servicename") IsNot Nothing Then
            Return CStr(Application.Item("Servicename"))
        Else
            Return "Unknown"
        End If
    End Function

    Public Sub HideMainMenu()
        _mainMenu.Items.Clear()
        _mainMenuBottom.Items.Clear()
        _mainMenu.Visible = False
        _mainMenuBottom.Visible = False
    End Sub
    Public Sub ShowPopupMenu(ByVal newMenu As MenuItemCollection, Optional ByVal index As Integer = 0, Optional ByVal ClearCurrentMenuItems As Boolean = False)
        Dim menuIndex As Integer = index
        ShowPopupMenu()

        If ClearCurrentMenuItems = True Then
            _mainMenu.Items.Clear()
            _mainMenuBottom.Items.Clear()
        End If
        If newMenu IsNot Nothing Then
            For i As Integer = 0 To newMenu.Count - 1
                Dim btmMenu As New MenuItem(newMenu(i).Text, newMenu(i).Value, newMenu(i).ImageUrl, newMenu(i).NavigateUrl, newMenu(i).Target)
                Dim topMenu As MenuItem = New MenuItem(newMenu(i).Text, newMenu(i).Value, newMenu(i).ImageUrl, newMenu(i).NavigateUrl, newMenu(i).Target)

                If menuIndex > newMenu.Count - 1 Then menuIndex = newMenu.Count - 1
                Me._mainMenu.Items.AddAt(menuIndex, topMenu)
                Me._mainMenuBottom.Items.AddAt(menuIndex, btmMenu)

                If newMenu(i).ChildItems.Count > 0 Then
                    For Each childMenu As MenuItem In newMenu(i).ChildItems
                        Dim btnChildMenu As New MenuItem(childMenu.Text, childMenu.Value, childMenu.ImageUrl, childMenu.NavigateUrl, childMenu.Target)
                        Dim topChildMenu As New MenuItem(childMenu.Text, childMenu.Value, childMenu.ImageUrl, childMenu.NavigateUrl, childMenu.Target)
                        Me._mainMenu.Items(menuIndex).ChildItems.Add(topChildMenu)
                        Me._mainMenuBottom.Items(menuIndex).ChildItems.Add(btnChildMenu)
                    Next
                End If
                menuIndex += 1
            Next
            Me.selectMenu()
        End If
    End Sub

    Private Function GetGlobalLocalizeText() As String
        'Dim confirmMessage As String = RIRESOURCES.GetResourceValue("YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES")
        'confirmMessage = confirmMessage.Replace("'", "\'")
        Dim sb As New StringBuilder
        sb.Append("var localizedText = new function(){")
        sb.Append("this.Close = 'Close';}") '& RIRESOURCES.GetResourceValue("Close") & "';")
        'sb.Append("this.FunctionalLocationTree = '" & RIRESOURCES.GetResourceValue("FunctionalLocation") & "';")
        'sb.Append("this.ConfirmDelete='" & RIRESOURCES.GetResourceValue("ConfirmDelete") & "';")
        'sb.Append("this.ConfirmRedirect='" & RIRESOURCES.GetResourceValue(confirmMessage) & "';")
        'sb.Append("this.EstDueandTaskDescRequired='" & RIRESOURCES.GetResourceValue("EstDueandTaskDescRequired") & "';")
        'sb.Append("this.EstimatedDueDateRequired='" & RIRESOURCES.GetResourceValue("EstimatedDueDateRequired") & "';")
        'sb.Append("this.TaskDescriptionRequired='" & RIRESOURCES.GetResourceValue("TaskDescriptionRequired") & "';")
        'sb.Append("this.SelectStartEnd='" & RIRESOURCES.GetResourceValue("SELECT START AND END DATE") & "';")
        'sb.Append("this.SelectEstimatedCompletionDate='" & RIRESOURCES.GetResourceValue("SELECT ESTIMATED COMPLETION DATE") & "';")
        'sb.Append("this.SelectDateCompleted='" & RIRESOURCES.GetResourceValue("SELECT DATE COMPLETED") & "';")
        'sb.Append("};")
        'Dim returnVal As String = sb.ToString
        'Return returnVal
        Return sb.ToString
    End Function
    'Public Sub AddChildMenu(ByVal newMenu As MenuItemCollection)
    '    _menuSub.Items.Clear()
    '    '_menuSubBottom.Items.Clear()
    '    For i As Integer = 0 To newMenu.Count - 1
    '        Dim btmMenu As New MenuItem(newMenu(i).Text, newMenu(i).Value, newMenu(i).ImageUrl, newMenu(i).NavigateUrl, newMenu(i).Target)
    '        Dim topMenu As MenuItem = New MenuItem(newMenu(i).Text, newMenu(i).Value, newMenu(i).ImageUrl, newMenu(i).NavigateUrl, newMenu(i).Target)

    '        _menuSub.Items.Add(topMenu)
    '        '_menuSubBottom.Items.Add(btmMenu)
    '    Next
    'End Sub
    Public Sub HideMenu()
        Me._mainMenu.DataSourceID = ""
        Me._mainMenuBottom.DataSourceID = ""
        _mainMenu.Visible = False
        _mainMenuBottom.Visible = False
    End Sub
    Public Sub selectMenu()
        Dim ThisPage As String = Page.AppRelativeVirtualPath
        Dim SlashPos As Integer = InStrRev(ThisPage, "/")
        Dim PageName As String = Right(ThisPage, Len(ThisPage) - SlashPos)
        Dim ClientQry As String = Page.ClientQueryString

        'Deselect All menu items
        For i As Integer = 0 To _mainMenu.Items.Count - 1
            _mainMenu.Items(i).Selected = False
            _mainMenuBottom.Items(i).Selected = False
            If _mainMenu.Items(i).NavigateUrl.Contains(PageName) Then
                _mainMenu.Items(i).Selected = True
                _mainMenuBottom.Items(i).Selected = True
            End If
        Next


        If ClientQry.Contains("RINumber=") Then 'Deselect Current page from menu
            _mainMenu.StaticSelectedStyle.CssClass = "normallink"
            _mainMenuBottom.StaticSelectedStyle.CssClass = "normallink"
        Else
            _mainMenu.StaticSelectedStyle.CssClass = "selectedlink"
            _mainMenuBottom.StaticSelectedStyle.CssClass = "selectedlink"
        End If


    End Sub
    Public Sub ShowPopupMenu()
        Me._mainMenu.DataSourceID = ""
        Me._mainMenuBottom.DataSourceID = ""
        _mainMenu.Items.Clear()
        _mainMenuBottom.Items.Clear()
        'Me._mainMenu.Items.Add(New MenuItem("Close", "Close", Nothing, "javascript:window.close();"))
        'Me._mainMenu.Items.Add(New MenuItem(RIRESOURCES.GetResourceValue("PrintPage"), "Print", Nothing, "javascript:window.print();"))
        'Me._mainMenuBottom.Items.Add(New MenuItem("Close", "Close", Nothing, "javascript:window.close();"))
        'Me._mainMenuBottom.Items.Add(New MenuItem(RIRESOURCES.GetResourceValue("PrintPage"), "Print", Nothing, "javascript:window.print();"))

    End Sub

    Public Sub ShowAdminMenu()
        Me.SiteMapDataSource1.SiteMapProvider = "AdminSiteMapProvider"
        Me._mainMenu.DataBind()
    End Sub

    Public Sub ShowOutageMenu()
        Me.SiteMapDataSource1.SiteMapProvider = "OutageSiteMapProvider"
        Me._mainMenu.DataSourceID = "SiteMapDataSource1"
        Me._mainMenuBottom.DataSourceID = "SiteMapDataSource1"
        Me._mainMenu.DataBind()
        Me._mainMenuBottom.DataBind()
    End Sub

    Public Sub ShowMOCMenu()
        Me.SiteMapDataSource1.SiteMapProvider = "MOCSiteMapProvider"
        Me._mainMenu.DataSourceID = "SiteMapDataSource1"
        Me._mainMenuBottom.DataSourceID = "SiteMapDataSource1"
        Me._mainMenu.DataBind()
        Me._mainMenuBottom.DataBind()
    End Sub

    Public Sub ShowRIMenu()
        'Dim newMenu As New MenuItemCollection
        'Dim menuIndex As Integer = 0
        'Dim parentMenu As New MenuItem
        'Dim mnuURL As String = "GetSaveChangesBeforeLeavingJS('{0}')" '"javascript:viewIncident('{0}');" 

        'Dim PromptToSave As Boolean
        'Me._mainMenu.DataSourceID = ""
        'Me._mainMenuBottom.DataSourceID = ""
        '_mainMenu.Items.Clear()
        '_mainMenuBottom.Items.Clear()

        'If Page.AppRelativeVirtualPath.ToUpper.Contains("ENTERNEWRI.ASPX") Then PromptToSave = True
        'newMenu.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuEnterNewRI", True), " ", Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/EnterNewRI.aspx")), "~/RI/EnterNewRI.aspx"))))
        'newMenu.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuViewUpdate", True), "", Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/ViewUpdateSearch.aspx")), "~/RI/ViewUpdateSearch.aspx")))) '("~/RI/ViewUpdateSearch.aspx")))
        'newMenu.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuReports", True), "", Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/Reporting.aspx")), "~/RI/Reporting.aspx")))) '("~/RI/Reporting.aspx")))
        'newMenu.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuDataMaintenance", True), "", Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/Data_Maintenance.aspx")), "~/RI/Data_Maintenance.aspx")))) '("~/RI/Data_Maintenance.aspx")))
        ''newMenu.Add(New MenuItem(RIRESOURCES.Value("mnuHelp"), ""))
        'parentMenu.Text = RIRESOURCES.GetResourceValue("mnuHelp", True)
        'parentMenu.NavigateUrl = CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/Help/Default.aspx")), "~/RI/Help/Default.aspx")) '"~/RI/Help/Default.aspx"
        'parentMenu.Value = ""

        'parentMenu.ChildItems.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuUsingMyHelp", True), Nothing, Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/Help/UsingMyHelp.aspx")), "~/RI/Help/UsingMyHelp.aspx")))) ' "~/RI/Help/UsingMyHelp.aspx", ""))
        'parentMenu.ChildItems.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuOnlineTraining", True), Nothing, Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/Help/OnlineTraining.aspx")), "~/RI/Help/OnlineTraining.aspx")))) '"~/RI/Help/OnlineTraining.aspx", ""))
        'parentMenu.ChildItems.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuEnhancements", True), Nothing, Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/Help/Enhancements.aspx")), "~/RI/Help/Enhancements.aspx")))) '"~/RI/Help/Enhancements.aspx", ""))
        'If RI.SharedFunctions.IsAdminUser(My.User.Name) Then
        '    parentMenu.ChildItems.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuImpersonateUser", True), Nothing, Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/Admin/ImpersonateUser.aspx")), "~/RI/Admin/ImpersonateUser.aspx")))) '"~/RI/Admin/ImpersonateUser.aspx", ""))
        '    parentMenu.ChildItems.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuCacheViewer", True), Nothing, Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/Admin/CacheViewer.aspx")), "~/RI/Admin/CacheViewer.aspx")))) '"~/RI/Admin/CacheViewer.aspx", ""))
        '    parentMenu.ChildItems.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuConfiguration", True), Nothing, Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/Admin/DowntimeMessage.aspx")), "~/RI/Admin/DowntimeMessage.aspx")))) '"~/RI/Admin/DowntimeMessage.aspx", ""))
        '    parentMenu.ChildItems.Add(New MenuItem("Manage Offline Status", Nothing, Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/Admin/ManageOfflineStatus.aspx")), "~/RI/Admin/ManageOfflineStatus.aspx")))) '"~/RI/Admin/DowntimeMessage.aspx", ""))
        '    parentMenu.ChildItems.Add(New MenuItem("Missing Keys", Nothing, Nothing, "javascript:DisplayMissingKeys('" & Page.ResolveClientUrl("~/RI/Admin/MissingResourceKeys.aspx") & "');", ""))

        'End If
        'newMenu.Add(parentMenu)
        'Me.ShowPopupMenu(newMenu, 0, True)

        ''For i As Integer = 0 To newMenu.Count - 1
        ''    Dim btmMenu As New MenuItem(newMenu(i).Text, newMenu(i).Value, newMenu(i).ImageUrl, newMenu(i).NavigateUrl, newMenu(i).Target)
        ''    Dim topMenu As MenuItem = New MenuItem(newMenu(i).Text, newMenu(i).Value, newMenu(i).ImageUrl, newMenu(i).NavigateUrl, newMenu(i).Target)

        ''    If menuIndex > newMenu.Count - 1 Then menuIndex = newMenu.Count - 1
        ''    Me._mainMenu.Items.AddAt(menuIndex, topMenu)
        ''    Me._mainMenuBottom.Items.AddAt(menuIndex, btmMenu)
        ''    menuIndex += 1
        ''Next




    End Sub



    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Dim formatter As New BinaryFormatter
        'Dim ms As New MemoryStream
        'Dim userData As String = String.Empty
        Try



            ''Change this to use cache
            'If Cache.Item("LocaleLanguages") Is Nothing Then
            '    If RIRESOURCES IsNot Nothing Then
            '        If RIRESOURCES.ApplicationLocaleList.Count > 0 Then
            '            Cache.Add("LocaleLanguages", RIRESOURCES.ApplicationLocaleList, Nothing, Now.AddHours(1), Nothing, CacheItemPriority.Default, Nothing)
            '        Else
            '            RIRESOURCES.Initialize()

            '        End If
            '    End If
            'End If

            'RIRESOURCES.PrintDocumentAndView()
            Server.ScriptTimeout = 9000
            Dim username As String
            username = CurrentUserProfile.GetCurrentUser
            'If Not Roles.RoleExists("Admin") Then
            '    Roles.CreateRole("Admin")
            'End If
            'If Not Roles.IsUserInRole(username, "Admin") Then

            '    Roles.AddUserToRole(username, "Admin")
            'Else
            '    Roles.RemoveUserFromRole(username, "Admin")
            'End If
            'If Not Page.IsPostBack Then
            'Dim userIsAuthenticated As Boolean = My.User.IsAuthenticated                
            Dim userProfile As CurrentUserProfile = Nothing
            userProfile = RI.SharedFunctions.GetUserProfile ' TryCast(Session.Item(Replace(username, "\", "_")), CurrentUserProfile) ' = CType(Session("LDAP"), AuthUser) '= AuthUser.GetAuthUser()


            'If userProfile Is Nothing Then
            '    userProfile = New CurrentUserProfile
            '    If Not userProfile Is Nothing Then
            '        Session.Add(Replace(username, "\", "_"), userProfile)
            '    End If
            'End If
            If userProfile IsNot Nothing Then
                SetCurrentUserLabel(userProfile.DomainName & "\" & userProfile.Username, userProfile.FullName)
                'Dim currentUsers As WhoisOnline
                'If Cache.Item("UsersOnline") IsNot Nothing Then
                '    currentUsers = CType(Cache.Item("UsersOnline"), WhoisOnline)
                'Else
                '    currentUsers = New WhoisOnline
                'End If
                'If currentUsers.Contains(userProfile.Username) Then
                '    currentUsers.Remove(currentUsers.IndexOf(userProfile.Username))
                'End If
                'currentUsers.Add(userProfile)
                'Cache.Insert("UsersOnline", currentUsers)

            Else
                SetCurrentUserLabel(System.Web.HttpContext.Current.User.Identity.Name, "Unknown")
            End If
            ShowRIMenu()
        Catch ex As Exception
            Server.ClearError()

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not Page.ClientScript.IsOnSubmitStatementRegistered(Page.GetType, "PrepareValidationSummary") Then
        '    Page.ClientScript.RegisterOnSubmitStatement(Page.GetType, "PrepareValidationSummary", "CheckForm('" & _ValidationSummaryMessage.MessageClientID & "','" & _ValidationSummaryMessage.MessageTriggerClientID & "')")
        'End If
        Page.ClientScript.RegisterClientScriptInclude("dropincontent", Page.ResolveClientUrl("~/dropincontent.js"))
        'Page.ClientScript.RegisterStartupScript(Page.GetType, "unload", " var ConfirmBeforeLeave=" & CStr(UnloadEvents).ToLower & ";", True)
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType, "GetGlobalLocalizeText", GetGlobalLocalizeText(), True)
        If Not Page.ClientScript.IsClientScriptIncludeRegistered("Master") Then
            Page.ClientScript.RegisterClientScriptInclude("Master", Page.ResolveClientUrl("~/MasterPage.js"))
        End If
        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "DHTMLWindow") Then
            Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "DHTMLWindow", Page.ResolveClientUrl("~/windowfiles/dhtmlwindow.js"))
        End If
        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "ModalWindow") Then
            Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "ModalWindow", Page.ResolveClientUrl("~/modalfiles/modal.js"))
        End If
        'Me._ValidationSummaryMessage.Title = "Warning!"
        'Me._ValidationSummaryMessage.ButtonType = ucMessageBox.ButtonTypes.OK

        Me.selectMenu()

    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Server.ScriptTimeout = 90
        'Me.RIRESOURCES.Dispose()
    End Sub

    Protected Sub _scriptManager_AsyncPostBackError(ByVal sender As Object, ByVal e As System.Web.UI.AsyncPostBackErrorEventArgs) Handles _scriptManager.AsyncPostBackError

    End Sub

    Public Sub New()

    End Sub
End Class

'End Namespace