Option Explicit On
Option Strict On

Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Threading

Namespace RI
    ''' <summary>
    ''' The ErrorLogger class is responsible for the handling and storage of errors
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ErrorLogger
        ''' <summary>
        ''' This function creates an error event that can be logged into an event database
        ''' </summary>
        ''' <param name="msgId">Int32 - Message ID for event to be logged</param>
        ''' <param name="msgSource">String - Source information about the error</param>
        ''' <param name="message">String - Event Description</param>
        ''' <param name="appException">Exception - Represents errors that occur during application execution</param>
        ''' <param name="appName">String - Represents the name of the current application (i.e. Reliability)</param>
        ''' <param name="conString">String - The connection string for logging the errors</param> 
        ''' <remarks></remarks>
        Public Shared Sub CreateErrorEvent(ByVal msgId As Int32, ByVal msgSource As String, ByVal message As String, ByVal appException As Exception, ByVal conString As String, ByVal appName As String)
            Dim newEventItem As EventItem
            Dim userName As String
            Dim sessionId As String
            Dim newThread As Thread

            Try
                If msgId < 0 Then msgId = 0
                If msgSource.Length = 0 Then msgSource = "Source Unknown"
                If message.Length = 0 Then message = "Missing Error Description"
                If appException Is Nothing Then
                    appException = New Exception("Unknown Error")
                End If

                If System.Web.HttpContext.Current.User.Identity.IsAuthenticated Then
                    userName = CurrentUserProfile.GetCurrentUser
                    If userName Is Nothing Or userName.Length = 0 Then
                        userName = "UserNotAuthenticated"
                    End If
                Else
                    userName = "UserNotAuthenticated"
                End If

                If System.Web.HttpContext.Current.Session Is Nothing Then
                    sessionId = "none"
                Else
                    sessionId = System.Web.HttpContext.Current.Session.SessionID
                    If sessionId.Length = 0 Or sessionId Is Nothing Then
                        sessionId = "none"
                    End If
                End If

                newEventItem = New EventItem(msgId, msgSource, message, userName, sessionId, appException, conString, appName)

                ' spin up a new thread and don't wait for it to finish so that control can be immediately
                '   handed back to the calling application
                If newEventItem IsNot Nothing Then
                    newThread = New Thread(AddressOf newEventItem.LogSQL)
                    newThread.Start()
                End If

            Catch e As Exception
                ' BPM - do nothing right now.  Eat the exception and don't throw it up to the
                '       caller
                newEventItem = Nothing
            End Try
        End Sub

        ''' <summary>
        ''' This class contains the properties and procedures needed to log the last error
        ''' </summary>
        ''' <remarks></remarks>
        Private Class EventItem

            Private m_msgId As Int32
            Private m_msgSource As String
            Private m_message As String
            Private m_userName As String
            Private m_sessionId As String
            Private m_appException As Exception
            Private m_connectionString As String
            Private m_applicationName As String

            ''' <summary>
            ''' Creates a new instance of the EventItem class
            ''' </summary>
            ''' <param name="msgId">Int32 - Message ID for event to be logged</param>
            ''' <param name="msgSource">String - Source information about the error</param>
            ''' <param name="message">String - Event Description</param>
            ''' <param name="appException">Exception - Represents errors that occur during application execution</param>
            ''' <param name="appName">String - Represents the name of the current application (i.e. Reliability)</param>
            ''' <param name="conString">String - The connection string for logging the errors</param> 
            ''' <param name="userName">String - The current user name</param>
            ''' <param name="sessionId">String - Current sessionid at the time of the error</param>
            ''' <remarks></remarks>          
            Sub New(ByVal msgId As Int32, ByVal msgSource As String, ByVal message As String, ByVal userName As String, ByVal sessionId As String, ByRef appException As Exception, ByVal conString As String, ByVal appName As String)
                Me.m_msgId = msgId
                Me.m_msgSource = msgSource
                Me.m_message = message
                Me.m_userName = userName
                Me.m_sessionId = sessionId
                Me.m_appException = appException
                Me.m_applicationName = appName
                Me.m_connectionString = conString
            End Sub
            

            ''' <summary>
            ''' This routine logs the current event into the specified database
            ''' </summary>
            ''' <remarks>The Web.Config file should contain a value for EventLog and AppName
            ''' (i.e. <add key="AppName" value="Reliability"/>, <add name="EventLog" connectionString="Data Source=s29edev13;Initial Catalog=eArch2Dev;User ID=webdevdbo;Password=password;"/>
            ''' This procedure assumes that the errors are being logged into a SQL Server database</remarks>
            Public Sub LogSQL()

                Dim sqlConn As SqlConnection = Nothing
                Dim sqlCom As SqlCommand = Nothing
                Dim sqlParam As SqlParameter = Nothing

                Dim loggedEvent As Boolean = False
                Dim connectString As String
                Dim appName As String
                Dim methodName As String
                Dim componentName As String
                Dim computerName As String
                Dim sourceArray() As String = Nothing
                Dim logId As Int64 = 0

                Try
                    If m_msgSource IsNot Nothing And m_msgSource.Length > 0 Then
                        sourceArray = m_msgSource.Split((".").ToCharArray)
                    End If
                    If sourceArray.Length > 1 Then
                        componentName = sourceArray(0)
                        methodName = sourceArray(1)
                    Else
                        componentName = ""
                        methodName = sourceArray(0)
                    End If

                    computerName = System.Net.Dns.GetHostName

                    If Me.ConnectionString Is Nothing Or Me.ConnectionString.Length = 0 Then
                        connectString = ConfigurationManager.ConnectionStrings.Item("EventLog").ConnectionString
                    Else
                        connectString = Me.ConnectionString
                    End If
                    If Me.ApplicationName Is Nothing Or Me.ApplicationName.Length = 0 Then
                        appName = ConfigurationManager.AppSettings("AppName")
                    Else
                        appName = Me.ApplicationName
                    End If


                    If Not (connectString Is Nothing) And Not (appName Is Nothing) Then

                        sqlConn = New SqlConnection
                        sqlConn.ConnectionString = connectString
                        sqlConn.Open()
                        sqlCom = New SqlCommand
                        sqlCom.Connection = sqlConn
                        sqlCom.CommandType = Data.CommandType.StoredProcedure
                        sqlCom.CommandText = "usp_InsertEventLogName"

                        sqlParam = New SqlParameter
                        sqlParam.Direction = Data.ParameterDirection.ReturnValue
                        sqlParam.ParameterName = "ReturnValue"
                        sqlCom.Parameters.Add(sqlParam)

                        sqlCom.Parameters.Add("@AppName", Data.SqlDbType.VarChar, 32).Value = appName
                        sqlCom.Parameters.Add("@MsgID", Data.SqlDbType.Int, 4).Value = m_msgId
                        sqlCom.Parameters.Add("@UserID", Data.SqlDbType.VarChar, 255).Value = m_userName
                        sqlCom.Parameters.Add("@SessionID", Data.SqlDbType.VarChar, 38).Value = m_sessionId
                        sqlCom.Parameters.Add("@Type", Data.SqlDbType.Int, 4).Value = 0
                        sqlCom.Parameters.Add("@Code", Data.SqlDbType.Int, 4).Value = 0
                        sqlCom.Parameters.Add("@Severity", Data.SqlDbType.Int, 4).Value = 0
                        sqlCom.Parameters.Add("@Category", Data.SqlDbType.Int, 4).Value = 4
                        sqlCom.Parameters.Add("@Description", Data.SqlDbType.VarChar, 255).Value = m_message
                        sqlCom.Parameters.Add("@UserDescription", Data.SqlDbType.VarChar, 255).Value = ""
                        sqlCom.Parameters.Add("@MethodName", Data.SqlDbType.VarChar, 255).Value = methodName
                        sqlCom.Parameters.Add("@ComponentName", Data.SqlDbType.VarChar, 255).Value = componentName
                        sqlCom.Parameters.Add("@ComputerName", Data.SqlDbType.VarChar, 255).Value = computerName
                        sqlCom.Parameters.Add("@EventTimeStamp", Data.SqlDbType.DateTime).Value = Date.Now

                        sqlCom.ExecuteNonQuery()

                        If Not AppException Is Nothing Then

                            Dim stackTrace As String = AppException.ToString()
                            Dim i As Int32
                            Dim chunkLength As Int32

                            logId = CType(sqlCom.Parameters("ReturnValue").Value, Int64)

                            sqlCom = New SqlCommand
                            sqlCom.Connection = sqlConn
                            sqlCom.CommandType = Data.CommandType.StoredProcedure
                            sqlCom.CommandText = "usp_InsertEventLogData"
                            sqlCom.Parameters.Add("@logID", Data.SqlDbType.Int, 4).Value = logId
                            sqlParam = sqlCom.Parameters.Add("@MsgData", Data.SqlDbType.VarChar, 255)

                            For i = 0 To stackTrace.Length Step 255
                                If stackTrace.Length < 255 Then
                                    chunkLength = stackTrace.Length - 1
                                Else
                                    If stackTrace.Length - i < 255 Then
                                        chunkLength = stackTrace.Length - i
                                    Else
                                        chunkLength = 255
                                    End If
                                End If
                                sqlParam.Value = stackTrace.Substring(i, chunkLength)
                                sqlCom.ExecuteNonQuery()
                            Next

                        End If

                    End If

                Catch e As Exception
                    'System.Web.HttpContext.Current.Server.ClearError()
                Finally
                    If Not (sqlCom Is Nothing) Then sqlCom.Dispose()
                    If Not (sqlConn Is Nothing) Then
                        If sqlConn.State = Data.ConnectionState.Open Then
                            sqlConn.Close()
                            sqlConn.Dispose()
                        End If
                    End If
                End Try

            End Sub

            ''' <summary>
            ''' Gets the Message ID for the current event
            ''' </summary>
            ''' <returns>Int32- Message ID for the current event</returns>
            ''' <remarks></remarks>
            Private ReadOnly Property MsgId() As Int32
                Get
                    Return m_msgId
                End Get                
            End Property

            ''' <summary>
            ''' Gets or sets the message for the current event
            ''' </summary>
            ''' <returns>String - The message for the current event</returns>
            ''' <remarks></remarks>
            Private ReadOnly Property Message() As String
                Get
                    Return m_message
                End Get               
            End Property

            ''' <summary>
            ''' Gets information about the source of the current event
            ''' </summary>
            ''' <returns>String - Source information about the current event</returns>
            ''' <remarks></remarks>
            Private ReadOnly Property MsgSource() As String
                Get
                    Return m_msgSource
                End Get               
            End Property

            ''' <summary>
            ''' Gets the user that created the current event
            ''' </summary>
            ''' <returns>String - The user that created the current event</returns>
            ''' <remarks></remarks>
            Private ReadOnly Property UserName() As String
                Get
                    Return m_userName
                End Get               
            End Property

            ''' <summary>
            ''' Gets the sessionid for the user that created the event
            ''' </summary>
            ''' <returns>String - The sessionid for the user that created the event</returns>
            ''' <remarks></remarks>
            Private ReadOnly Property SessionId() As String
                Get
                    Return m_sessionId
                End Get                
            End Property

            ''' <summary>
            ''' Gets the exception that was created by the current user
            ''' </summary>
            ''' <returns>Exception - the exception that was created by the current user</returns>
            ''' <remarks></remarks>
            Private ReadOnly Property AppException() As Exception
                Get
                    Return m_appException
                End Get                
            End Property

            ''' <summary>
            ''' Gets the connection string that will be used to log the event
            ''' </summary>
            ''' <returns>String - the connection string that will be used to log the event</returns>
            ''' <remarks></remarks>
            Private ReadOnly Property ConnectionString() As String
                Get
                    Return m_connectionString
                End Get              
            End Property

            ''' <summary>
            ''' Gets the name of the current application
            ''' </summary>
            ''' <returns>String - The name of the current application</returns>
            ''' <remarks></remarks>
            Private ReadOnly Property ApplicationName() As String
                Get
                    Return m_applicationName
                End Get

            End Property
        End Class
    End Class

End Namespace