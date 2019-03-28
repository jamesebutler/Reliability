Option Explicit On
Option Strict On

Imports Microsoft.VisualBasic
Imports System.Data
Imports system.Security.Principal
Imports System.DirectoryServices
Imports System.Web.TraceContext
Imports Devart.Data.Oracle


Namespace RI
    ''' <summary>
    ''' The CurrentUserProfile module contains properties and procedures for creating a user profile for the current user
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class CurrentUserProfile
        Private _Username As String = String.Empty
        Private _DefaultFacility As String = String.Empty
        Private _Domain As String = "GRAPHICPKG"
        Private _Division As String = String.Empty
        Private _InActiveFlag As String = String.Empty
        Private _BusType As String = String.Empty
        Private _FullName As String = String.Empty
        Private _Groups() As String
        Private _PropertyKeys() As String
        Private _PropertyValues() As String
        Private _DistinguishedName As String = String.Empty
        Private _MemberOf As String = String.Empty
        Private _EMail As String = String.Empty
        Private _ProfileTable As String = String.Empty
        Private _AuthLevel As String = "NO"
        Private _AuthLevelID As Integer = 0
        Private _DivestedLocation As Boolean = False
        Private _DefaultLanguage As String = ""



        ''' <summary>
        ''' Provides the email address for the current user if one exist
        ''' </summary>
        ''' <value></value>
        ''' <returns>String - Returns the email address for the current user if one exist</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Email() As String
            Get
                Return _EMail
            End Get
        End Property

        Public ReadOnly Property DivestedLocation() As Boolean
            Get
                Return _DivestedLocation
            End Get
        End Property

        Public ReadOnly Property DefaultLanguage() As String
            Get
                Return _DefaultLanguage
            End Get
        End Property

        ''' <summary>
        ''' Provides the Default Division for the current user
        ''' </summary>
        ''' <value></value>
        ''' <returns>String - returns the Default Division for the current user</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property DefaultDivision() As String
            Get
                Return _Division
            End Get
        End Property

        ''' <summary>
        ''' Provides the Business Type for the current user
        ''' </summary>
        ''' <value></value>
        ''' <returns>String - Returns the Business Type for the current user</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property BusType() As String
            Get
                Return _BusType
            End Get
        End Property

        ''' <summary>
        ''' Provides the InActiveFlag value for the current user
        ''' </summary>
        ''' <value></value>
        ''' <returns>String - Returns the InActiveFlag value for the current user.  Possible values are D - Divested, Y - Yes, N - No</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property InActiveFlag() As String
            Get
                Return _InActiveFlag
            End Get
        End Property

        ''' <summary>
        ''' Provides the Default Facility for the current user
        ''' </summary>
        ''' <value></value>
        ''' <returns>String - Returns the Default Facility for the current user</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property DefaultFacility() As String
            Get
                Return _DefaultFacility
            End Get
        End Property

        ''' <summary>
        ''' Provides the network username for the current user (i.e. mjpope)
        ''' </summary>
        ''' <value></value>
        ''' <returns>String - Returns the network username for the current user (i.e. mjpope)</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Username() As String
            Get
                Return _Username
            End Get
        End Property

        ''' <summary>
        ''' Provides the domain name for the current user (i.e. naipaper)
        ''' </summary>
        ''' <value></value>
        ''' <returns>String - Returns the domain name for the current user (i.e. naipaper)</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property DomainName() As String
            Get
                Return _Domain
            End Get
        End Property

        ''' <summary>
        ''' Provides the Full name of the current user as specified in LDAP
        ''' </summary>
        ''' <value></value>
        ''' <returns>String - Returns the full name of the current user as specified in LDAP</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property FullName() As String
            Get
                Return _FullName
            End Get
        End Property

        Public ReadOnly Property ProfileTable() As String
            Get
                Return _ProfileTable
            End Get

        End Property

        Public ReadOnly Property GroupName() As String
            Get
                Return _MemberOf
            End Get
        End Property
        Public ReadOnly Property DistinguishedName() As String
            Get
                Return _DistinguishedName
            End Get
        End Property
        Public ReadOnly Property AuthLevel() As String
            Get
                Return Me._AuthLevel
            End Get
        End Property
        Public ReadOnly Property AuthLevelID() As Integer
            Get
                Return Me._AuthLevelID
            End Get
        End Property
        Private Sub BuildProfileTable()
            Dim sb As New StringBuilder
            Dim tblRow As String = "<tr><th nowrap=nowrap width=200 align=right>{0}</th><td align=left>{1}</td></tr>"
            With Me
                sb.Append("<table width=600 border=1 class='help' cellpadding=2 cellspacing=1>")
                sb.Append(String.Format(tblRow, "Full Name:", ._FullName))
                sb.Append(String.Format(tblRow, "User Name:", ._Domain & "\" & ._Username))
                sb.Append(String.Format(tblRow, "Email Address:", .Email))
                sb.Append(String.Format(tblRow, "Default Facility:", ._DefaultFacility))
                sb.Append(String.Format(tblRow, "Default Division:", ._Division))
                sb.Append(String.Format(tblRow, "Business Type:", ._BusType))
                sb.Append(String.Format(tblRow, "Inactive Flag:", ._InActiveFlag))
                sb.Append(String.Format(tblRow, "Group Name:", ._MemberOf))
                sb.Append(String.Format(tblRow, "Divested Location:", ._DivestedLocation))
                sb.Append(String.Format(tblRow, "Default Language:", ._DefaultLanguage))
                sb.Append("</table>")
                ._ProfileTable = sb.ToString
            End With
        End Sub

        ''' <summary>
        ''' Provides the name for the current user (i.e. naipaper\username)
        ''' </summary>
        ''' <returns>String - Returns the name for the current user (i.e. naipaper\username)</returns>
        ''' <remarks></remarks>
        Public Shared Function GetCurrentUser() As String
            Dim User As System.Security.Principal.IPrincipal
            Dim currentUser As String = String.Empty

            Try
                If System.Web.HttpContext.Current.Session IsNot Nothing Then
                    If System.Web.HttpContext.Current.Session.Item("CurrentUser") IsNot Nothing Then
                        currentUser = CStr(System.Web.HttpContext.Current.Session.Item("CurrentUser"))
                    End If
                End If
                If currentUser Is Nothing Then currentUser = String.Empty
                If currentUser.Length = 0 Then
                    If System.Web.HttpContext.Current.User.Identity.IsAuthenticated Then
                        User = System.Web.HttpContext.Current.User
                        If User IsNot Nothing Then
                            currentUser = User.Identity.Name
                            If System.Web.HttpContext.Current.Session IsNot Nothing Then
                                System.Web.HttpContext.Current.Session.Add("CurrentUser", currentUser)
                            End If
                        End If
                    Else
                        'We probably need to force a login
                    End If
                    'Supply a username and domain below to impersonate a user                    
                End If
            Catch ex As Exception
                Throw
            End Try
            Return currentUser
        End Function

        ''' <summary>
        ''' Parses the CN field from the ADS Path
        ''' </summary>
        ''' <param name="path">String - Active Directory Service Path(ADS Path)</param>
        ''' <returns>String - the CN field from the ADS Path</returns>
        ''' <remarks></remarks>
        Private Function TrimToName(ByVal path As String) As String
            Dim parts() As String
            Dim cn As String = String.Empty
            Try
                parts = path.Split(CChar(","))
                cn = parts(0).Replace("CN=", String.Empty)
            Catch ex As Exception
                Throw
            Finally
                TrimToName = cn
            End Try
        End Function

        ''' <summary>
        ''' Creates a new instance of the AuthUser public class for the current user.
        ''' </summary>        
        Public Sub GetAuthUser() 'As CurrentUserProfile
            'Dim myAuthUser As CurrentUserProfile = Nothing
            Dim username As String
            Dim strServername As String = System.Web.HttpContext.Current.Request("Server_Name").ToLower

            Try
                username = GetCurrentUser()

                If username.Length > 0 Then
                    System.Web.HttpContext.Current.Session.Add("CurrentUser", username)
                    GetUserProfile(username)

                    Dim fullUsername As String() = username.Split(CChar("\"))
                    If fullUsername.Length = 2 Then
                        If fullUsername(0).ToUpper = "ICC_GO" Then
                            Dim newUserName As String = String.Empty
                            newUserName = Me.GetNAIPAPERUserNameFromEmail(Me.Email)
                            System.Web.HttpContext.Current.Session.Add("CurrentUser", "NA\" & newUserName)
                            GetUserProfile("NA\" & newUserName)
                        End If
                    End If


                    'System.Web.HttpContext.Current.Session.Add("CurrentUser", currentUser)
                End If
            Catch ex As Exception
                Throw
            Finally
                'GetAuthUser = myAuthUser
                'If myAuthUser IsNot Nothing Then myAuthUser = Nothing
            End Try
        End Sub

        Public Sub AuthenticateUser(ByVal username As String, ByVal password As String) 'As CurrentUserProfile
            Dim strServername As String = System.Web.HttpContext.Current.Request("Server_Name").ToLower

            Try
                If username.Length > 0 Then
                    HttpContext.Current.Session.Remove("FindOne_" & username)
                    System.Web.HttpContext.Current.Session.Add("CurrentUser", username)
                    GetUserProfile(username, password)

                    Dim fullUsername As String() = username.Split(CChar("\"))
                    If fullUsername.Length = 2 Then
                        If fullUsername(0).ToUpper = "ICC_GO" Then
                            Dim newUserName As String = String.Empty
                            newUserName = Me.GetNAIPAPERUserNameFromEmail(Me.Email)
                            System.Web.HttpContext.Current.Session.Add("CurrentUser", "NA\" & newUserName)
                            GetUserProfile("NA\" & newUserName)
                        End If
                    End If


                    'System.Web.HttpContext.Current.Session.Add("CurrentUser", currentUser)
                End If
            Catch 'ex As Exception
                System.Web.HttpContext.Current.Session.Remove("CurrentUser")
                'GetAuthUser()
                Throw
            Finally
                'GetAuthUser = myAuthUser
                'If myAuthUser IsNot Nothing Then myAuthUser = Nothing
            End Try
        End Sub

        ''' <summary>
        ''' Populates the AuthUser public class with the User Profile information for the current user
        ''' </summary>
        ''' <param name="username">String - The current user (i.e. naipaper\username) </param>
        ''' <remarks>Assumes that the following import statements have been added:
        ''' -- Imports system.Security.Principal
        ''' -- Imports System.DirectoryServices</remarks>
        Private Sub GetUserProfile(ByVal username As String, Optional ByVal password As String = "")
            Dim enTry As DirectoryEntry = Nothing
            Dim srch As New DirectorySearcher
            Dim userDomain As String()
            Dim ldapUser As String = System.Configuration.ConfigurationManager.AppSettings("LDAPUserName").ToString()
            Dim ldapPassword As String = System.Configuration.ConfigurationManager.AppSettings("LDAPPassword").ToString
            Dim ldapPath As String = System.Configuration.ConfigurationManager.AppSettings("LDAPPath").ToString

            Try
                If ldapUser Is Nothing Or ldapUser.Length = 0 Then
                    Throw New Exception("LDAP UserName is missing from Web.config")
                    Exit Sub
                End If
                If ldapPassword Is Nothing Or ldapPassword.Length = 0 Then
                    Throw New Exception("LDAP Password is missing from Web.config")
                    Exit Sub
                End If
                If ldapPath Is Nothing Or ldapPath.Length = 0 Then
                    Throw New Exception("LDAP Path is missing from Web.config")
                    Exit Sub
                End If
                If username.Length = 0 Then
                    Throw New UnauthorizedAccessException("Required Username is missing!")
                    Exit Sub
                End If

                'enTry = New DirectoryEntry(ldapPath, ldapUser, ldapPassword, AuthenticationTypes.Secure)
                If password.Length = 0 Then
                    enTry = New DirectoryEntry(ldapPath, ldapUser, ldapPassword, AuthenticationTypes.Secure)
                Else
                    enTry = New DirectoryEntry("LDAP://" & DomainName, username, password, AuthenticationTypes.Secure)
                End If
                'enTry = New DirectoryEntry("LDAP://CN=LDAP WEBUser,CN=Users,DC=na,DC=ipaper,DC=com", "naipaper\LDAPWEBUser", "1dapW3Bus3r", AuthenticationTypes.Secure)                

                userDomain = username.Split(CChar("\"))

                If userDomain.Length = 2 Then
                    _Username = userDomain(1)
                    _Domain = userDomain(0)
                Else
                    Throw New UnauthorizedAccessException("Invalid username specified (" & username & ").")
                    Exit Sub
                End If

                enTry.Path = "LDAP://" & DomainName

                srch = New DirectorySearcher(enTry)

                srch.Filter = "(&(objectClass=user)(samAccountName=" & _Username & "))"

                Dim results As SearchResult = Nothing '= srch.FindOne
                results = TryCast(HttpContext.Current.Session.Item("FindOne_" & _Username), SearchResult)
                If results Is Nothing Then results = srch.FindOne

                'Dim ienum As IEnumerator
                'ienum = results.Properties.PropertyNames.GetEnumerator
                'Dim sb As New StringBuilder
                'While ienum.MoveNext

                '    If results.Properties.Item(CStr(ienum.Current)).Count > 0 Then
                '        'If CStr(results.Properties(ienum.Current.ToString)(0)) IsNot Nothing Then
                '        If sb.Length > 0 Then sb.Append(",")
                '        sb.Append(ienum.Current.ToString) '& " = " & CStr(results.Properties(ienum.Current.ToString)(0)))
                '    End If
                '    'End If
                'End While
                'Dim bf As Runtime.Serialization.Formatters.Binary.BinaryFormatter = Nothing
                'Dim ms As IO.MemoryStream = Nothing
                'Dim LDAPResults() As Byte

                'bf = New Runtime.Serialization.Formatters.Binary.BinaryFormatter
                'ms = New IO.MemoryStream
                'bf.Serialize(ms, results)
                'LDAPResults = ms.ToArray 'RI.SharedFunctions.CompressgZip(ms.ToArray)
                'results = CType(bf.Deserialize(ms), SearchResult)

                Dim al As New ArrayList()
                Dim pl As New ArrayList()

                If results IsNot Nothing Then
                    HttpContext.Current.Session.Add("FindOne_" & _Username, results)
                    Dim obj As Object
                    For Each obj In results.Properties("MemberOf")
                        al.Add(TrimToName(obj.ToString))
                    Next

                    _Groups = CType(al.ToArray(GetType(String)), String())

                    If results.Properties.Item("samAccountName").Count > 0 Then
                        _Username = CStr(results.Properties("samAccountName")(0))
                    End If
                    If results.Properties.Item("displayname").Count > 0 Then
                        _FullName = CStr(results.Properties("displayname")(0))
                    ElseIf results.Properties.Item("name").Count > 0 Then
                        _FullName = CStr(results.Properties("name")(0))
                    End If
                    _FullName = _FullName.Replace("_0", "")
                    If results.Properties.Item("memberof").Count > 0 Then
                        _MemberOf = CStr(results.Properties("memberof")(0))
                    End If
                    If results.Properties.Item("Distinguishedname").Count > 0 Then
                        _DistinguishedName = CStr(results.Properties("Distinguishedname")(0))
                        If results.Properties.Item("CN").Count > 0 Then
                            Dim cn As String = CStr(results.Properties.Item("CN")(0))
                            _DistinguishedName = _DistinguishedName.Replace(cn, cn.Replace("-", "_"))
                        End If
                    End If
                    If results.Properties("mail").Count > 0 Then
                        _EMail = RI.SharedFunctions.ToTitleCase(CStr(results.Properties("mail")(0)), True)
                    End If
                End If
                'Dim ouFacility As String = ""
                'Dim pos As Integer = InStr(_DistinguishedName, "-")
                'If pos > 0 Then
                '    ouFacility = Mid(_DistinguishedName, pos + 1, 3)
                'End If
                SetProfileDefaults()
                'BuildProfileTable()
            Catch ex As Exception
                Dim grp() As String = Nothing
                _Groups = grp
                Throw
            Finally
                srch.Dispose()
                enTry.Dispose()
            End Try
        End Sub

        'Public Function GetLdapUser(ByVal userName As String, ByVal domainName As String, ByVal password As String, Optional ByVal propertiesToLoad As ArrayList = Nothing) As LdapProfile
        '    ' setting up the lookup to AD
        '    Dim ldapUser As String = System.Configuration.ConfigurationManager.AppSettings("LDAPUserName").ToString()
        '    Dim ldapPassword As String = System.Configuration.ConfigurationManager.AppSettings("LDAPPassword").ToString
        '    Dim ldapPath As String = System.Configuration.ConfigurationManager.AppSettings("LDAPPath").ToString
        '    Dim ldap As New LdapProfile
        '    Dim dt As New DataTable("AD_Users")
        '    Dim adEntry As DirectoryEntry
        '    Dim adSearcher As DirectorySearcher

        '    If password.Length = 0 Then
        '        adEntry = New DirectoryEntry(ldapPath, ldapUser, ldapPassword, AuthenticationTypes.Secure)
        '    Else
        '        adEntry = New DirectoryEntry("LDAP://" & domainName, userName, password, AuthenticationTypes.Secure)
        '    End If


        '    'enTry.Path = "LDAP://" & DomainName
        '    If domainName.ToUpper = "NAIPAPER" Then
        '        adEntry.Path = "LDAP://na.ipaper.com"
        '    ElseIf domainName.ToUpper = "ICC_GO" Then
        '        adEntry.Path = "LDAP://myinland.com"
        '    Else
        '        adEntry.Path = "LDAP://" & domainName
        '    End If
        '    'adEntry.Path = "LDAP://" & domainName

        '    ' define which fields to retrieve from AD

        '    Try


        '        adSearcher = New DirectorySearcher(adEntry)
        '        If adSearcher IsNot Nothing Then
        '            With adSearcher
        '                .Filter = "(&(objectClass=user)(samAccountName=" & userName & ")(objectCategory=person))"
        '                .PropertyNamesOnly = False
        '                .CacheResults = True
        '                If propertiesToLoad IsNot Nothing Then
        '                    For i As Integer = 0 To propertiesToLoad.Count - 1
        '                        .PropertiesToLoad.Add(CStr(propertiesToLoad(i)))
        '                    Next
        '                End If

        '            End With
        '        End If
        '        ' define a datatable and add the results to it

        '        Dim adResults As SearchResult

        '        adResults = adSearcher.FindOne


        '        Dim dr As DataRow

        '        If adResults IsNot Nothing Then
        '            Dim propertyCount As Integer = 0
        '            propertyCount = propertyCount + 1
        '            ' add the results to the datatable
        '            dr = dt.NewRow()
        '            Dim ienum As IEnumerator
        '            ienum = adResults.Properties.PropertyNames.GetEnumerator

        '            Try
        '                '                        Dim PropertyNameValueCollection As New Specialized.OrderedDictionary ' COMMENTED BY CODEIT.RIGHT
        '                While ienum.MoveNext
        '                    If propertyCount = 1 Then
        '                        If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                            dt.Columns.Add(New DataColumn(ienum.Current.ToString, GetType(System.String)))
        '                        End If
        '                    End If
        '                    If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                        dr.Item(ienum.Current.ToString) = (adResults.Properties.Item(ienum.Current.ToString)(0))
        '                    End If

        '                    Select Case ienum.Current.ToString.ToUpper
        '                        Case UCase("distinguishedname")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Distinguishedname = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("countrycode")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Countrycode = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("cn")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.CN = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("lastlogoff")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Lastlogoff = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("useraccountcontrol")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Useraccountcontrol = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("usncreated")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Usncreated = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                            'Case UCase("objectguid")
        '                            'If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                            'ldap.objectguid = (adResults.Properties.Item(ienum.Current.ToString)(0))
        '                            'End If
        '                        Case UCase("postalcode")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Postalcode = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("whenchanged")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Whenchanged = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("memberof")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Memberof = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("accountexpires")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Accountexpires = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("displayname")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Displayname = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("employeenumber")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Employeenumber = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("primarygroupid")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Primarygroupid = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("streetaddress")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Streetaddress = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("badpwdcount")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Badpwdcount = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("objectclass")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Objectclass = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("objectcategory")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Objectcategory = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("instancetype")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Instancetype = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("homedrive")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Homedrive = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("samaccounttype")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Samaccounttype = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("homedirectory")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Homedirectory = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("whencreated")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Whencreated = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("lastlogon")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Lastlogon = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("l")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.L = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("st")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.ST = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("co")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.CO = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("title")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Title = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("c")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.C = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("samaccountname")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Samaccountname = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("employeetype")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Employeetype = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("givenname")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Givenname = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("mail")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Mail = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("adspath")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Adspath = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("lockouttime")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Lockouttime = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("scriptpath")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Scriptpath = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("pwdlastset")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Pwdlastset = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("logoncount")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Logoncount = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("codepage")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Codepage = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("name")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Name = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("physicaldeliveryofficename")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Physicaldeliveryofficename = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("usnchanged")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Usnchanged = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                            'Case UCase("dscorepropagationdata")
        '                            '    If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                            '        ldap.dscorepropagationdata = (adResults.Properties.Item(ienum.Current.ToString)(0))
        '                            '    End If
        '                        Case UCase("userprincipalname")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Userprincipalname = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("badpasswordtime")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Badpasswordtime = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("objectsid")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                'ldap.objectsid = (adResults.Properties.Item(ienum.Current.ToString)(0))
        '                            End If
        '                        Case UCase("sn")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.SN = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                        Case UCase("telephonenumber")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Telephonenumber = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                            'Case UCase("logonhours")
        '                            '    If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                            '        ldap.logonhours = (adResults.Properties.Item(ienum.Current.ToString)(0))
        '                            '    End If
        '                        Case UCase("lastlogontimestamp")
        '                            If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                                ldap.Lastlogontimestamp = CStr((adResults.Properties.Item(ienum.Current.ToString)(0)))
        '                            End If
        '                    End Select

        '                    'If adResults.Properties.Item(CStr(ienum.Current)).Count > 0 Then
        '                    '    PropertyNameValueCollection.Add(ienum.Current.ToString, adResults.Properties.Item(ienum.Current.ToString)(0))
        '                    'End If
        '                End While
        '                If ldap.Distinguishedname.Contains("OU=Services") = True Then
        '                    ldap = Nothing
        '                End If
        '            Catch
        '                ldap = Nothing
        '                Throw
        '            End Try
        '            dt.Rows.Add(dr)
        '        End If
        '        If ldap IsNot Nothing Then
        '            ldap.PropertyNameValueDataTable = dt
        '        End If
        '    Catch
        '        ldap = Nothing
        '        Throw
        '    Finally
        '        dt = Nothing
        '        adEntry = Nothing
        '        adSearcher = Nothing
        '    End Try

        '    Return ldap

        'End Function

        Public Function GetNAIPAPERUserNameFromEmail(ByVal emailAddress As String) As String
            Dim ldapUser As String = System.Configuration.ConfigurationManager.AppSettings("LDAPUserName").ToString()
            Dim ldapPassword As String = System.Configuration.ConfigurationManager.AppSettings("LDAPPassword").ToString
            Dim ldapPath As String = System.Configuration.ConfigurationManager.AppSettings("LDAPPath").ToString
            'Dim ldap As New LdapProfile
            Dim adEntry As DirectoryEntry
            Dim userName As String = String.Empty
            Dim adSearcher As DirectorySearcher

            Try
                adEntry = New DirectoryEntry(ldapPath, ldapUser, ldapPassword, AuthenticationTypes.Secure)
                adEntry.Path = "LDAP://GRAPHICPKG.PRI"

                adSearcher = New DirectorySearcher(adEntry)
                If adSearcher IsNot Nothing Then
                    With adSearcher
                        .Filter = "(&(objectClass=user)(OBJECTCATEGORY=Person)(mail=" & emailAddress & "))"
                        .PropertyNamesOnly = False
                        .CacheResults = True
                        .SizeLimit = 5 'SizeLimit
                        .PropertiesToLoad.Add("SAMACCOUNTNAME") 'Username                    
                    End With
                    Dim adResults As SearchResult

                    adResults = adSearcher.FindOne

                    If adResults IsNot Nothing Then
                        If adResults.Properties.Item("samaccountname").Count > 0 Then
                            userName = CStr(adResults.Properties.Item("samaccountname").Item(0))
                        End If
                    End If
                End If
            Catch
                Throw
            Finally
                adSearcher = Nothing
                adEntry = Nothing
            End Try
            'IP.Bids.SharedFunctions.InsertAuditRecord("GetNAIPAPERUserNameFromEmail", String.Format("Username:{0}, Email Address:{1}", userName, emailAddress))
            Return userName
        End Function

        'Private Sub GetUserProfileFromWS(ByVal username As String)
        '    Dim userDomain As String()

        '    Try
        '        If username.Length = 0 Then
        '            Throw New UnauthorizedAccessException("Required Username is missing!")
        '            Exit Sub
        '        End If

        '        userDomain = username.Split(CChar("\"))

        '        If userDomain.Length = 2 Then
        '            _Username = userDomain(1)
        '            _Domain = userDomain(0)
        '        Else
        '            Throw New UnauthorizedAccessException("Invalid username specified (" & username & ").")
        '            Exit Sub
        '        End If
        '        Dim ld As New DevLDAP.Service
        '        ld.UseDefaultCredentials = True
        '        Dim ldap As DevLDAP.LdapProfile
        '        ldap = TryCast(HttpContext.Current.Session.Item("LDAPProfile_" & _Username), DevLDAP.LdapProfile)
        '        If ldap Is Nothing Then
        '            ldap = ld.GetUser(_Username, _Domain)
        '            If ldap IsNot Nothing Then
        '                HttpContext.Current.Session.Add("LDAPProfile_" & _Username, ldap)
        '            End If
        '        End If
        '        If ldap IsNot Nothing Then
        '            _DistinguishedName = ldap.distinguishedname
        '            _Username = ldap.samaccountname
        '            _Groups = Split(ldap.memberof, ",")
        '            _MemberOf = ldap.memberof
        '            If ldap.displayname.Length > 0 Then
        '                _FullName = ldap.displayname
        '            Else
        '                _FullName = ldap.name
        '            End If

        '            'Todo: Check whether to put in svetogorsk.com instead of ipaper.com
        '            If ldap.mail IsNot Nothing Then
        '                If ldap.mail.Length > 0 Then
        '                    _EMail = ldap.mail
        '                Else
        '                    '_EMail = _Username.Trim & "@ipaper.com"
        '                End If
        '            Else
        '                ' _EMail = _Username.Trim & "@ipaper.com"
        '            End If
        '        End If

        '        'Dim ouFacility As String = ""
        '        'Dim pos As Integer = InStr(_DistinguishedName, "-")
        '        'If pos > 0 Then
        '        '    ouFacility = Mid(_DistinguishedName, pos + 1, 3)
        '        'End If
        '        SetProfileDefaults()
        '        BuildProfileTable()



        '    Catch ex As Exception
        '        Dim grp() As String = Nothing
        '        _Groups = grp
        '        Throw
        '    Finally
        '    End Try
        'End Sub

        Private Function GetUserDefaultsFromPackage(ByVal user As String, ByVal groupName As String, ByVal ouFacility As String) As OracleDataReader
            Dim conCust As OracleConnection = Nothing
            Dim cmdSQL As OracleCommand = Nothing
            Dim connection As String = String.Empty
            Dim provider As String = String.Empty
            Dim ds As DataSet = Nothing
            Dim daData As OracleDataAdapter = Nothing
            Dim cnConnection As OracleConnection = Nothing
            Dim userDomain As String() = Nothing
            Dim dr As OracleDataReader = Nothing

            Try
                If connection.Length = 0 Then
                    connection = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
                End If
                If provider.Length = 0 Then
                    provider = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ProviderName
                End If


                If user IsNot Nothing And user.Length > 0 Then
                    userDomain = user.Split(CChar("\"))

                    If userDomain.Length = 2 Then
                        _Username = userDomain(1).ToUpper
                        _Domain = userDomain(0).ToUpper
                    Else
                        Throw New UnauthorizedAccessException("Invalid username specified (" & user & ").")
                        Exit Function
                    End If
                Else
                    Throw New UnauthorizedAccessException("Invalid username specified (" & user & ").")
                    Exit Function
                End If

                cmdSQL = New OracleCommand
                With cmdSQL
                    cnConnection = New OracleConnection(connection)
                    cnConnection.Open()
                    .Connection = cnConnection
                    .CommandText = "ri.UserDefaults"
                    .CommandType = CommandType.StoredProcedure
                    Dim param As New OracleParameter

                    param.ParameterName = "IN_USERNAME"
                    param.OracleDbType = OracleDbType.VarChar
                    param.Value = _Username
                    param.Direction = ParameterDirection.Input
                    .Parameters.Add(param)

                    param = New OracleParameter
                    param.ParameterName = "IN_DOMAIN"
                    param.OracleDbType = OracleDbType.VarChar
                    param.Value = _Domain
                    param.Direction = ParameterDirection.Input
                    .Parameters.Add(param)

                    param = New OracleParameter
                    param.ParameterName = "IN_GROUPNAME"
                    param.OracleDbType = OracleDbType.VarChar
                    param.Value = groupName
                    param.Direction = ParameterDirection.Input
                    .Parameters.Add(param)

                    param = New OracleParameter
                    param.ParameterName = "IN_OUFACILITY"
                    param.OracleDbType = OracleDbType.VarChar
                    param.Size = 3
                    param.Value = ouFacility
                    param.Direction = ParameterDirection.Input
                    .Parameters.Add(param)

                    param = New OracleParameter
                    param.ParameterName = "rsDivision"
                    param.OracleDbType = OracleDbType.Cursor
                    param.Direction = ParameterDirection.Output
                    .Parameters.Add(param)

                    param = New OracleParameter
                    param.ParameterName = "rsSite"
                    param.OracleDbType = OracleDbType.Cursor
                    param.Direction = ParameterDirection.Output
                    .Parameters.Add(param)

                    param = New OracleParameter
                    param.ParameterName = "rsOUSite"
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
                Throw New DataException("GetUserDefaultsFromPackage", ex)
                If Not conCust Is Nothing Then conCust = Nothing
            Finally
                GetUserDefaultsFromPackage = dr
                If Not daData Is Nothing Then daData = Nothing
                If Not ds Is Nothing Then ds = Nothing
                If Not cmdSQL Is Nothing Then cmdSQL = Nothing
                'cnConnection.Close()
            End Try
        End Function
        '' ' <summary>
        '' ' Sets the facility siteid and business type for the current user.  
        '' ' If the DefaultDivision is blank the DefaultDivision and InActiveFlag will be set as well.
        '' ' </summary>
        '' ' <param name="user">String - The current user (i.e. naipaper\username)</param>
        '' ' <remarks></remarks>

        'Private Sub SetProfileDefaults(ByVal user As String)
        '    Dim sql As String = "Select r1.siteid,division,r2.INACTIVE_FLAG,BusType from reladmin.refemployee r1,reladmin.REFSITE r2 where r1.siteid=r2.siteid and UPPER(r1.username)=UPPER('{0}') and UPPER(r1.domain)=UPPER('{1}')"
        '    Dim siteID As String = String.Empty
        '    Dim userDomain As String() = Nothing
        '    Dim dr As OracleDataReader = Nothing

        '    Try
        '        If user IsNot Nothing And user.Length > 0 Then
        '            userDomain = user.Split(CChar("\"))

        '            If userDomain.Length = 2 Then
        '                _Username = userDomain(1).ToUpper
        '                _Domain = userDomain(0).ToUpper
        '            Else
        '                Throw New UnauthorizedAccessException("Invalid username specified (" & user & ").")
        '                Exit Sub
        '            End If
        '        Else
        '            Throw New UnauthorizedAccessException("Invalid username specified (" & user & ").")
        '            Exit Sub
        '        End If

        '        sql = String.Format(sql, Username, DomainName)
        '        dr = SharedFunctions.GetOracleDataReader(sql)
        '        If Not dr Is Nothing Then
        '            dr.Read()
        '            If Not dr.Item("SiteID") Is Nothing Then
        '                siteID = dr.Item("SiteID").ToString
        '                If _Division.Length = 0 Then _Division = CStr(dr.Item("Division"))
        '                If _InActiveFlag.Length = 0 Then _InActiveFlag = CStr(dr.Item("INACTIVE_FLAG"))
        '                _BusType = CStr(dr.Item("BusType"))
        '            End If
        '        End If
        '        _DefaultFacility = siteID
        '    Catch ex As Exception
        '        Throw
        '    Finally
        '        If dr IsNot Nothing Then
        '            dr.Close()
        '            dr = Nothing
        '        End If

        '    End Try

        'End Sub
        Public Sub SetProfileDefaults()
            Dim siteID As String = String.Empty
            Dim dr As OracleDataReader = Nothing
            Dim user As String = Me.DomainName & "\" & Me.Username
            Dim groupName As String = Me._MemberOf
            Try
                Dim ouFacility As String = ""
                Dim pos As Integer = InStr(_DistinguishedName, "-")
                If pos > 0 Then
                    ouFacility = Mid(_DistinguishedName, pos + 1, 3)
                End If


                dr = GetUserDefaultsFromPackage(user, groupName, ouFacility)
                If Not dr Is Nothing Then
                    dr.Read()
                    If dr.HasRows = True Then
                        If Not dr.Item("DIVISION") Is Nothing Then
                            _Division = CStr(RI.SharedFunctions.DataClean(dr.Item("Division")))
                        End If
                        Me._DivestedLocation = True
                    End If
                    dr.NextResult()
                    If dr.HasRows = True Then
                        dr.Read()
                        If Not dr.Item("SiteID") Is Nothing Then
                            siteID = RI.SharedFunctions.DataClean(dr.Item("SiteID")).ToString
                            If _Division.Length = 0 Then _Division = CStr(RI.SharedFunctions.DataClean(dr.Item("Division")))
                            If _InActiveFlag.Length = 0 Then _InActiveFlag = CStr(RI.SharedFunctions.DataClean(dr.Item("INACTIVE_FLAG")))
                            _BusType = CStr(RI.SharedFunctions.DataClean(dr.Item("BusType")))
                            If RI.SharedFunctions.DataClean(dr.Item("default_language")) IsNot Nothing And RI.SharedFunctions.DataClean(dr.Item("default_language")) IsNot Nothing Then
                                _DefaultLanguage = CStr(RI.SharedFunctions.DataClean(dr.Item("default_language")))
                            End If

                            If _EMail.Length = 0 Then 'This user doesn't have an email address listed in LDAP
                                If dr.Item("Email") IsNot DBNull.Value Then
                                    _EMail = CStr(RI.SharedFunctions.DataClean(dr.Item("Email")))
                                Else
                                    'This code should not be executed
                                    _EMail = _Username & "@Graphicpkg.com"
                                    RI.SharedFunctions.InsertAuditRecord("SetProfileDefaults", _Username & " has a null email address in the refEmployee table")
                                End If
                            End If
                        End If
                    Else
                        dr.NextResult()
                        If dr.HasRows = True Then
                            dr.Read()
                            If Not dr.Item("SiteID") Is Nothing Then
                                siteID = RI.SharedFunctions.DataClean(dr.Item("SiteID")).ToString
                                If _Division.Length = 0 Then _Division = CStr(RI.SharedFunctions.DataClean(dr.Item("Division")))
                                If _InActiveFlag.Length = 0 Then _InActiveFlag = CStr(RI.SharedFunctions.DataClean(dr.Item("INACTIVE_FLAG")))
                                _BusType = CStr(RI.SharedFunctions.DataClean(dr.Item("BusType")))
                                If _DefaultLanguage.Length = 0 Then
                                    _DefaultLanguage = RI.SharedFunctions.DataClean(CStr(dr.Item("default_language")), "en-US")
                                End If
                            End If
                        Else
                            siteID = "AL"
                            If _Division.Length = 0 Then _Division = "ALL"
                            If _InActiveFlag.Length = 0 Then _InActiveFlag = "N"
                            _BusType = "PM"
                        End If

                    End If
                    'dr.NextResult()
                    'If dr.HasRows = True Then
                    '    dr.Read()
                    '    If Not dr.Item("authlevel") Is Nothing Then
                    '        Me._AuthLevel = CStr(dr.Item("authlevel"))
                    '        Me._AuthLevelID = CInt(dr.Item("authlevelid"))
                    '    Else
                    '        Me._AuthLevel = "NO"
                    '        Me._AuthLevelID = 0
                    '    End If
                    'End If
                End If
                If _EMail.Length = 0 Then
                    'This code should not be executed b/c we should always have a valid email address
                    _EMail = _Username & "@graphicpkg.com"
                    RI.SharedFunctions.InsertAuditRecord("SetProfileDefaults", _Username & " is not listed in the refEmployee table")
                End If
                BuildProfileTable()
                _DefaultFacility = siteID
            Catch ex As Exception
                Throw
            Finally
                If dr IsNot Nothing Then
                    dr.Close()
                    dr = Nothing
                End If

            End Try

        End Sub
        ''' <summary>
        ''' Creates a new instance of the CurrentUserProfile class
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            GetAuthUser()            
        End Sub
    End Class
    <Serializable()> _
       Public Class WhoisOnline
        Inherits CollectionBase
        Implements IList, ICollection, IEnumerable

        ''' <summary>
        ''' Gets the Report Parameter at the specified index.
        ''' </summary>
        ''' <param name="index">Int32 - The zero-based index of the Report Parameter. </param>
        ''' <value></value>
        ''' <returns>ReportParameter - Gets the Report Parameter at the specified index.</returns>
        ''' <remarks></remarks>
        Default Public ReadOnly Property Item(ByVal index As Int32) As CurrentUserProfile
            Get
                Return CType(List.Item(index), CurrentUserProfile)
            End Get
        End Property

        ''' <summary>
        ''' Adds an item to the Report Parameter collection.
        ''' </summary>
        ''' <param name="Item">ReportParameter - The item that will be added as an Report Parameter</param>
        ''' <returns>Integer - The position into which the new element was inserted</returns>
        ''' <remarks></remarks>
        Public Function Add(ByVal Item As CurrentUserProfile) As Integer
            Return List.Add(Item)
        End Function

        ''' <summary>
        ''' Removes an item from the Report Parameter collection
        ''' </summary>
        ''' <param name="index">Int32 - The index of the item that will be removed from the collection</param>
        ''' <remarks></remarks>
        Public Sub Remove(ByVal index As Int32)
            ' Check to see if there is a widget at the supplied index.
            If index > Count - 1 Or index < 0 Then
                ' If no index exists, the operation is cancelled.            
            Else
                ' Invokes the RemoveAt method of the List object.
                List.RemoveAt(index)
            End If
        End Sub

        ''' <summary>
        ''' Sorts the elements in the entire Report Parameter collection 
        ''' using the System.IComparable implementation of each element.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Sort()
            InnerList.Sort()
        End Sub

        ''' <summary>
        ''' Searches for the username and returns the 
        ''' zero-based index of the first occurrence within the entire 
        ''' collection
        ''' </summary>
        ''' <param name="name">String - The name of the user to find</param>
        ''' <returns>Integer - The index of the item that will be found in the collection</returns>
        ''' <remarks></remarks>
        Public Overloads Function IndexOf(ByVal name As String) As Integer
            Dim index As Integer = 0
            Dim item As CurrentUserProfile

            ' Brute force
            For Each item In Me
                If item.Username = name Then
                    Return index
                End If
                index += 1
            Next
            Return -1
        End Function

        ''' <summary>
        ''' Determines whether a user is in the current collection.
        ''' </summary>
        ''' <param name="name">String - The name of the user to find</param>
        ''' <returns>Boolean - true if item is found in the collection; otherwise, false.</returns>
        ''' <remarks></remarks>
        Public Overloads Function Contains(ByVal name As String) As Boolean
            Return (-1 <> IndexOf(name))
        End Function

    End Class

End Namespace