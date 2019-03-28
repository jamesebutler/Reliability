Imports System.Data
Imports Devart.Data.Oracle
Imports System

Partial Class ucCultureSelection
    Inherits System.Web.UI.UserControl
    Private mRILocalization As New IP.Bids.Localization.WebLocalization
    Private mLocaleLanguages As New Hashtable
    Private userprofile As RI.CurrentUserProfile = Nothing
    Public Property LocaleLanguages() As Hashtable
        Get
            mLocaleLanguages = mRILocalization.ApplicationLocaleList           
            Return mLocaleLanguages
        End Get
        Set(ByVal value As Hashtable)
            mLocaleLanguages = value
        End Set
    End Property

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
       
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        userprofile = RI.SharedFunctions.GetUserProfile
        Dim languages As New Hashtable
        Dim defaultLanguage As String = userprofile.DefaultLanguage
        Dim currentLanguage As String = System.Threading.Thread.CurrentThread.CurrentCulture.Name
        If LocaleLanguages.Count = 0 Then
            languages.Add(defaultLanguage, defaultLanguage)
        Else
            languages = LocaleLanguages
        End If

        'If Not Page.IsPostBack Then
        Me._rblLanguages.Items.Clear()
        For Each lng As DictionaryEntry In languages
            If lng.Value.ToString.ToUpper <> "UNKNOWN" Then
                Me._rblLanguages.Items.Add(New ListItem(lng.Value.ToString, lng.Key))
            End If
        Next
        If _rblLanguages.Items.FindByValue(currentLanguage) IsNot Nothing Then
            _rblLanguages.Items.FindByValue(currentLanguage).Selected = True
        End If
        'End If

        If System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper <> defaultLanguage Then
            Me._lblCurrentLanguage.Text = System.Threading.Thread.CurrentThread.CurrentCulture.EnglishName & ": " & System.Threading.Thread.CurrentThread.CurrentCulture.NativeName
        Else
            Me._lblCurrentLanguage.Text = System.Threading.Thread.CurrentThread.CurrentCulture.EnglishName
        End If


    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        mRILocalization.Dispose()
    End Sub

    Protected Sub _btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnOK.Click
        'Save the default language
        SaveDefaultLanguage(userprofile.Username, Me._rblLanguages.SelectedValue)
        Server.Transfer(Page.AppRelativeVirtualPath & Request.Url.Query, False)
        '(in_username IN varchar2,in_defaultlanguage in varchar2)
    End Sub
    Private Sub SaveDefaultLanguage(ByVal userName As String, ByVal defaultLanguage As String)
        'Dim conCust As Devart.Data.Oracle.OracleConnection = Nothing
        Dim cmdSQL As OracleCommand = Nothing
        Dim connection As String = String.Empty
        Dim provider As String = String.Empty
        ' Dim daData As OracleDataAdapter = Nothing
        Dim cnConnection As Devart.Data.Oracle.OracleConnection = Nothing
        Dim userDomain As String() = Nothing

        Try
            If connection.Length = 0 Then
                connection = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
            End If
            If provider.Length = 0 Then
                provider = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ProviderName
            End If


            If userName Is Nothing OrElse userName.Length = 0 Then
                Throw New UnauthorizedAccessException("Invalid username specified (" & userName & ").")
                Exit Sub
            End If


            cmdSQL = New OracleCommand
            With cmdSQL
                cnConnection = New OracleConnection(connection)
                cnConnection.Open()
                .Connection = cnConnection
                .CommandText = "ri.SaveDefaultLanguage"
                .CommandType = CommandType.StoredProcedure
                Dim param As New OracleParameter

                param.ParameterName = "IN_USERNAME"
                param.OracleDbType = OracleDbType.VarChar
                param.Value = userName
                param.Direction = ParameterDirection.Input
                .Parameters.Add(param)

                param = New OracleParameter
                param.ParameterName = "IN_DEFAULTLANGUAGE"
                param.OracleDbType = OracleDbType.VarChar
                param.Value = defaultLanguage
                param.Direction = ParameterDirection.Input
                .Parameters.Add(param)
            End With


            Dim rowsAffected As Integer = cmdSQL.ExecuteNonQuery()
        Catch
            Throw
        Finally
            If cnConnection IsNot Nothing Then cnConnection = Nothing
            If Not cmdSQL Is Nothing Then cmdSQL = Nothing
            'cnConnection.Close()
        End Try
    End Sub
End Class
