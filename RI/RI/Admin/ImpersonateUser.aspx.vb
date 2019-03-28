Option Explicit On
Option Strict On

Imports RI
Imports System.Data
Imports Devart.Data.Oracle
Partial Class RI_Help_ImpersonateUser
    Inherits System.Web.UI.Page

    Protected Sub _btnImpersonateUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnImpersonateUser.Click
        Dim currentProfile As CurrentUserProfile = Nothing

        Session.Remove("CurrentUser")
        Session.Add("CurrentUser", Me._ddlUser.SelectedValue)
        Session.Remove("clsSearch")
        currentProfile = RI.SharedFunctions.GetUserProfile
        'System.Web.HttpContext.Current.Session.Add("CurrentUser", currentUser)
        If currentProfile IsNot Nothing Then
            With currentProfile
                _divUserProfile.InnerHtml = .ProfileTable
                Master.SetCurrentUserLabel(.DomainName & "\" & .Username, .FullName)
            End With
        End If

    End Sub
    Private Function GetUserListDSFromPackage() As OracleDataReader
        Dim cmdSQL As OracleCommand = Nothing
        Dim connection As String = String.Empty
        Dim provider As String = String.Empty
        Dim dr As OracleDataReader = Nothing
        Dim daData As OracleDataAdapter = Nothing
        Dim cnConnection As OracleConnection = Nothing

        Try
            If connection.Length = 0 Then
                connection = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
            End If
            If provider.Length = 0 Then
                provider = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ProviderName
            End If
            cmdSQL = New OracleCommand
            With cmdSQL
                cnConnection = New OracleConnection(connection)
                cnConnection.Open()
                .Connection = cnConnection
                .CommandText = "ri.Testusers"
                .CommandType = CommandType.StoredProcedure
                Dim param As New OracleParameter

                param = New OracleParameter
                param.ParameterName = "RSTESTUSERS"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = ParameterDirection.Output
                .Parameters.Add(param)
            End With

            'ds = New DataSet()
            'ds.EnforceConstraints = False
            'daData = New OracleDataAdapter(cmdSQL)
            'daData.Fill(ds)
            'ds.EnforceConstraints = True
            dr = cmdSQL.ExecuteReader(CommandBehavior.CloseConnection)
        Catch ex As Exception
            Return Nothing
            Throw New DataException("GetUserListDSFromPackage", ex)
            If Not cnConnection Is Nothing Then cnConnection = Nothing
        Finally
            GetUserListDSFromPackage = dr
            If Not daData Is Nothing Then daData = Nothing
            'If Not ds Is Nothing Then ds = Nothing
            If Not cmdSQL Is Nothing Then cmdSQL = Nothing
            'cnConnection.Close()
            'If Not cnConnection Is Nothing Then cnConnection = Nothing
        End Try
    End Function
    Private Sub PopulateUsers()
        Dim savedXML As String = String.Empty
        Dim io As New System.IO.StringWriter
        Dim sb As New StringBuilder
        Dim sbUser As New StringBuilder
        Try
            'savedXML = SharedFunctions.GetDataFromSQLServerCache("TestUsers", 1440)
            Dim dr As OracleDataReader = GetUserListDSFromPackage()
            'dr = GetUserListDSFromPackage()
            If dr IsNot Nothing Then                
                If dr.HasRows = True Then
                    While dr.Read
                        sb.Length = 0
                        sb.Append(dr.Item("Name"))
                        sb.Append(" (")
                        sb.Append(dr.Item("Location"))
                        sb.Append(")")

                        sbUser.Length = 0
                        sbUser.Append(dr.Item("Domain"))
                        sbUser.Append("\")
                        sbUser.Append(dr.Item("UserName"))

                        _ddlUser.Items.Add(New ListItem(sb.ToString, sbUser.ToString))
                    End While

                End If
            End If            
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner("Impersonate User")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim currentProfile As CurrentUserProfile = Nothing
        Dim username As String
        username = CurrentUserProfile.GetCurrentUser
        currentProfile = CType(Session.Item(Replace(username, "\", "_")), CurrentUserProfile) ' = CType(Session("LDAP"), AuthUser) '= AuthUser.GetAuthUser()
        If currentProfile IsNot Nothing Then
            With currentProfile
               _divUserProfile.InnerHtml = .ProfileTable
            End With
        End If
        If Not Page.IsPostBack Then
            PopulateUsers()
        End If
    End Sub
End Class
