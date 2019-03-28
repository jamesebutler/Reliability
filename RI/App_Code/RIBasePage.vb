Option Explicit On
Option Strict On
Imports RI
Imports Microsoft.VisualBasic


''' <summary>
''' This class should be inherited by all pages to insure proper error handling
''' </summary>
''' <remarks>This page will contain all events or functions that should be inherited by all pages</remarks>
Public Class RIBasePage
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' This event captures all page level errors and logs them appropriately
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
        'Log Errors Here
        RI.SharedFunctions.HandleError()
    End Sub

    'Protected Overrides Sub SavePageStateToPersistenceMedium(ByVal state As Object)
    '    'MyBase.SavePageStateToPersistenceMedium(state)
    '    'Generate unique key for this viewstate
    '    Dim CacheViewState As Boolean = False

    '    If ConfigurationManager.AppSettings("CacheViewState") IsNot Nothing Then
    '        CacheViewState = CType(ConfigurationManager.AppSettings("CacheViewState"), Boolean)
    '    End If
    '    If CacheViewState = True Then
    '        Dim str As String = "VIEWSTATE#" + Request.UserHostAddress & "#" & DateTime.Now.Ticks.ToString()
    '        'Save viewstate data in cache
    '        Cache.Add(str, state, Nothing, DateTime.Now.AddMinutes(Session.Timeout), TimeSpan.Zero, CacheItemPriority.Default, Nothing)
    '        ClientScript.RegisterHiddenField("__VIEWSTATE_KEY", str)
    '        'Keep the viewstate hidden variable but with no data to avoid error
    '        ClientScript.RegisterHiddenField("__VIEWSTATE", "")
    '    Else
    '        MyBase.SavePageStateToPersistenceMedium(state)
    '    End If
    'End Sub
    'Protected Overrides Function LoadPageStateFromPersistenceMedium() As Object
    '    Dim CacheViewState As Boolean = False

    '    If ConfigurationManager.AppSettings("CacheViewState") IsNot Nothing Then
    '        CacheViewState = CType(ConfigurationManager.AppSettings("CacheViewState"), Boolean)
    '    End If
    '    If CacheViewState = True Then
    '        'Return MyBase.LoadPageStateFromPersistenceMedium()
    '        'Load back viewstate from cache
    '        Dim str As String = Request.Form("__VIEWSTATE_KEY")
    '        'Check validity of viewstate key
    '        If (Not str.StartsWith("VIEWSTATE#")) Then
    '            Throw New Exception("Invalid viewstate key:" + str)
    '        End If
    '        Return Cache.Item(str)
    '    Else
    '        Return MyBase.LoadPageStateFromPersistenceMedium()
    '    End If
    'End Function
    Protected Overrides Sub InitializeCulture()      
        MyBase.InitializeCulture()
        InitCulture()
    End Sub
    Private Sub InitCulture()
        Dim userprofile As RI.CurrentUserProfile = Nothing
        Dim defaultCulture As String = ""
        Dim selectedCulture As String = ""
        Dim cultureBeingUsed As String = ""
        Dim allKeys() As String = Request.Form.AllKeys
        Dim cultureIsSet As Boolean

        Try            

            'Look to see if the user has selected a different language
            For i As Integer = 0 To allKeys.Length - 1
                If allKeys(i) IsNot Nothing Then
                    If allKeys(i).Contains("_rblLanguages") Then
                        If Request.Form(allKeys(i).ToString) IsNot Nothing And Request.Form(allKeys(i).ToString).Length > 0 Then
                            selectedCulture = Request.Form(allKeys(i).ToString)
                        End If
                        Exit For
                    End If
                End If
            Next

            If selectedCulture.Length > 0 Then
                cultureBeingUsed = RI.SharedFunctions.InitCulture(selectedCulture)
                If cultureBeingUsed <> "Auto" And cultureBeingUsed.Length > 0 Then
                    cultureIsSet = True
                Else
                    cultureIsSet = False
                End If
            End If

            If cultureIsSet = False Then
                'Populate the current culture from the User Profile table
                userprofile = RI.SharedFunctions.GetUserProfile
                If userprofile IsNot Nothing Then defaultCulture = userprofile.DefaultLanguage
                userprofile = Nothing

                cultureBeingUsed = RI.SharedFunctions.InitCulture(defaultCulture)
                If cultureBeingUsed <> "Auto" And cultureBeingUsed.Length > 0 Then
                    cultureIsSet = True
                Else
                    cultureIsSet = False
                End If
            End If

            Dim CultureCookie As HttpCookie
            If cultureIsSet = False Then
                'Use the last selected culture from the cookies
                If Request.Cookies("SelectedCulture") IsNot Nothing Then
                    cultureBeingUsed = RI.SharedFunctions.InitCulture(RI.SharedFunctions.DataClean(Request.Cookies("SelectedCulture").Value, "EN-US"))
                End If
                If cultureBeingUsed <> "Auto" And cultureBeingUsed.Length > 0 Then
                    cultureIsSet = True
                Else
                    cultureIsSet = False
                    Me.UICulture = "Auto"
                    Me.Culture = "Auto"
                    If Request.Cookies("SelectedCulture") IsNot Nothing Then
                        Response.Cookies("SelectedCulture").Expires = DateTime.Now.AddDays(-1)
                    End If
                End If
               
            Else
                CultureCookie = New HttpCookie("SelectedCulture")
                CultureCookie.Value = cultureBeingUsed
                Response.SetCookie(CultureCookie)
            End If

            'If cultureBeingUsed.ToUpper = "RU-RU" Then Response.Charset = "Windows-1251"
        Catch
            Throw
        Finally
            userprofile = Nothing
        End Try
    End Sub
    'Public Function SetCulture(ByVal culture As String) As Boolean
    '    Dim returnValue As Boolean
    '    If (culture <> "Auto") Then
    '        Try
    '            'Dim ci As New System.Globalization.CultureInfo(culture)
    '            Dim ci As System.Globalization.CultureInfo
    '            ci = System.Globalization.CultureInfo.GetCultureInfo(culture)
    '            System.Threading.Thread.CurrentThread.CurrentCulture = ci
    '            System.Threading.Thread.CurrentThread.CurrentUICulture = ci
    '            Me.UICulture = culture
    '            Me.Culture = culture
    '            returnValue = True
    '        Catch ex As ArgumentNullException
    '            'System.ArgumentNullException: name is null.
    '            returnValue = False
    '        Catch ex As System.ArgumentException
    '            'System.ArgumentException: name specifies a culture that is not supported.
    '            returnValue = False
    '        Catch
    '            Throw
    '        End Try
    '    End If
    '    Return returnValue
    'End Function
#Region "ListControlLocalization"
    ''' <summary> 
    ''' Overrides the common functionality for the creation 
    ''' of controls within the page. 
    ''' </summary> 
    Protected Overloads Overrides Sub CreateChildControls()
        ' Find the form control in which all asp.net (or custom) controls 
        ' must be hosted. 
        'For Each c As Control In Me.Controls
        '    If TypeOf c Is System.Web.UI.MasterPage Then
        '        For Each cc As Control In c.Controls
        '            If TypeOf cc Is System.Web.UI.HtmlControls.HtmlForm Then
        '                FindListControls(cc)
        '            End If
        '        Next
        '    End If
        'Next

        ' Call the base implementation 
        MyBase.CreateChildControls()
    End Sub

    Private Sub FindListControls(ByVal c As Control)
        Try
            For i As Integer = 0 To (c.Controls.Count - 1)
                If i < c.Controls.Count Then
                    If TypeOf c.Controls(i) Is DropDownList Then
                        Dim ddl As DropDownList
                        ddl = TryCast(c.Controls(i), DropDownList)
                        AddHandler ddl.PreRender, AddressOf ListControlDataBound
                    ElseIf TypeOf c.Controls(i) Is CheckBoxList Then
                        Dim lcl As CheckBoxList = TryCast(c.Controls(i), CheckBoxList)
                        AddHandler lcl.PreRender, AddressOf ListControlDataBound
                    ElseIf TypeOf c.Controls(i) Is RadioButtonList Then
                        Dim lcl As RadioButtonList = TryCast(c.Controls(i), RadioButtonList)
                        AddHandler lcl.PreRender, AddressOf ListControlDataBound
                    ElseIf TypeOf c.Controls(i) Is ListControl Then
                        Dim lcl As ListControl = TryCast(c.Controls(i), ListControl)
                        AddHandler lcl.PreRender, AddressOf ListControlDataBound
                    End If
                    If c.Controls(i).HasControls Then
                        FindListControls(c.Controls(i))
                    End If
                End If
            Next
        Catch ex As Exception
            Throw New Exception("FindListControls", ex.InnerException)
        End Try
    End Sub
    Public Sub ListControlDataBound(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim RIRESOURCES As New IP.BIDS.Localization.WebLocalization
        Dim lc As ListControl = TryCast(sender, ListControl)
        RIRESOURCES.LocalizeListControl(lc)
    End Sub
#End Region
End Class
