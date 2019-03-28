Option Explicit On
Option Strict On

Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports System.IO.Compression
Imports System.Xml
Imports System.Net
Imports System.Xml.Xsl
Imports System.IO
Imports System.Net.Mail
Imports System.Runtime.Serialization.Formatters.Binary
Imports Devart.Data.Oracle
Imports System.Drawing
Imports System.Collections.Generic

Imports System.Diagnostics





'Imports Oracle.DataAccess
Namespace RI
    ''' <summary>
    ''' This class contains a list of shared functions that can be used by any page
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Public Class SharedFunctions
        Inherits System.Web.HttpApplication

        Public tracing As Boolean = CBool(ConfigurationManager.AppSettings("Tracing"))
        Public tracingFunctions As Boolean = CBool(ConfigurationManager.AppSettings("TracingFunctions"))




        ''' <summary>
        ''' This function will return a unique key based on the value that has been provided
        ''' </summary>
        ''' <param name="criteria">Object - The value that will be converted to a unique key</param>
        ''' <returns>String - a unique key based on the value that has been provided</returns>
        ''' <remarks></remarks>
        Public Shared Function CreateKey(ByVal criteria As Object) As String
            Dim bf As BinaryFormatter = Nothing
            Dim ms As MemoryStream = Nothing
            Dim b() As Byte
            Dim key As String = String.Empty

            Try
                bf = New BinaryFormatter
                ms = New MemoryStream

                bf.Serialize(ms, criteria)

                If ms IsNot Nothing Then
                    b = CompressgZip(ms.ToArray)
                    If b IsNot Nothing Then
                        key = Convert.ToBase64String(b).GetHashCode.ToString  'Convert.ToBase64String(b)
                    End If
                End If
                Return key
            Catch ex As Exception
                Throw
                Return ""
            End Try
        End Function


        ''' <summary>
        ''' This function is used to remove undesirable characters from the data that has been provided
        ''' </summary>
        ''' <param name="inputValue"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Shared Function DataClean(ByVal inputValue As Object) As String
            Dim inValue As String = String.Empty

            Try

                'If sInput = DBNull Then sInput = ""
                If inputValue Is Nothing Or inputValue Is DBNull.Value Then
                    Return ""
                    inValue = String.Empty
                Else
                    inValue = inputValue.ToString
                End If

                If inValue.ToString.Length > 0 Then
                    'inValue = inValue.Replace("#", "No.")
                    'inValue = inValue.Replace("'", "''")
                    inValue = inValue.Replace("|", ":")
                    'inValue = inValue.Replace("&", " and ")
                    inValue = inValue.Replace("""", "'")
                Else
                    inValue = String.Empty
                End If
            Catch ex As ArgumentNullException
                Throw
                Return ""
            Catch ex As Exception
                Throw New ApplicationException("SQLClean -" & inputValue.ToString, ex)
                Return ""
            Finally
                DataClean = inValue.Trim
            End Try
        End Function

        Shared Function DataCleanEmailList(ByVal inputValue As Object) As String
            Dim inValue As String = String.Empty

            Try

                'If sInput = DBNull Then sInput = ""
                If inputValue Is Nothing Or inputValue Is DBNull.Value Then
                    Return ""
                    inValue = String.Empty
                Else
                    inValue = inputValue.ToString
                End If

                If inValue.ToString.Length > 0 Then
                    'inValue = inValue.Replace("#", "No.")
                    'inValue = inValue.Replace("'", "''")
                    inValue = inValue.Replace("|", ":")
                    'inValue = inValue.Replace("&", " and ")
                    inValue = inValue.Replace("""", "'")
                    inValue = inValue.Replace(",", "','")
                Else
                    inValue = String.Empty
                End If
            Catch ex As ArgumentNullException
                Throw
                Return ""
            Catch ex As Exception
                Throw New ApplicationException("SQLClean -" & inputValue.ToString, ex)
                Return ""
            Finally
                DataCleanEmailList = inValue.Trim
            End Try
        End Function

        'Shared Function isDateValue(ByVal dateValue As String) As Boolean
        '    Try
        '        Dim dtformatInfo As System.Globalization.DateTimeFormatInfo = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat
        '        Dim dt As DateTime = DateTime.Parse(dateValue, dtformatInfo)
        '        If dt.Year >= 2000 Then
        '            Return True
        '        Else
        '            Return False
        '        End If
        '    Catch
        '        Return False
        '    End Try
        'End Function
        Public Shared Function isDateValue(ByVal dateValue As String) As Boolean
            Try
                If dateValue.Length <= 4 Then Return False
                Dim dtformatInfo As System.Globalization.DateTimeFormatInfo = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat
                Dim dt As System.DateTime = System.DateTime.Parse(dateValue, dtformatInfo)
                If dt.Year >= 2000 Then
                    Return True
                Else
                    Return False
                End If
            Catch
                ' Throw
                Return False
            End Try
        End Function
        Shared Function isEnglishDate(ByVal dateValue As String) As Boolean
            Try
                Dim dtformatInfo As System.Globalization.DateTimeFormatInfo = System.Globalization.CultureInfo.CreateSpecificCulture("EN-US").DateTimeFormat
                Dim dt As DateTime = DateTime.Parse(dateValue, dtformatInfo)
                If dt.Year >= 2000 Then
                    Return True
                Else
                    Return False
                End If
            Catch
                Return False
            End Try
        End Function
        Shared Function DataClean(ByVal inputValue As Object, ByVal defaultvalue As String) As String
            Dim ret As String = DataClean(inputValue)
            If ret.Length = 0 Then ret = defaultvalue
            Return ret.Trim
        End Function

        Public Shared Function CleanDate(ByVal val As Object, ByVal dateFormat As Microsoft.VisualBasic.DateFormat) As String
            val = DataClean(val)
            If IsDate(val) Then
                Return FormatDateTime(CDate(val), dateFormat)
            Else
                Return String.Empty
            End If
        End Function

        ''' <summary>
        ''' This function converts the input parameter to it's Proper Case
        ''' </summary>
        ''' <param name="inputValue">String - Input value that needs to be converted</param>
        ''' <returns>String - The converted input parameter in it's Proper Case</returns>
        ''' <remarks></remarks>
        Shared Function ProperCase(ByVal inputValue As String) As String
            Dim txtlen As Integer
            Dim needCap As Boolean
            Dim i As Integer
            Dim ch As String

            Try
                inputValue = LCase(inputValue)
                txtlen = inputValue.Length
                needCap = True
                For i = 1 To txtlen
                    ch = Mid$(inputValue, i, 1)
                    If ch >= "a" And ch <= "z" Then
                        If needCap Then
                            Mid$(inputValue, i, 1) = UCase$(ch)
                            needCap = False
                        End If
                    Else
                        needCap = True
                    End If
                Next i
            Catch ex As Exception
                Throw
            Finally
                ProperCase = inputValue
            End Try
        End Function
        Shared Function IsAdminUser(ByVal name As String) As Boolean
            name = name.ToLower
            Dim adminUsers As New ArrayList
            Dim admUsrArr = ConfigurationManager.AppSettings("PowerUsers").ToString.ToLower.Split(New Char() {","c})
            adminUsers.AddRange(admUsrArr)
            Return adminUsers.Contains(name)
        End Function

        'Shared Function IsAdminUser(ByVal name As String) As Boolean
        '    name = name.ToLower
        '    Dim adminUsers As New ArrayList
        '    adminUsers.Add("NA\Surya.Nandury")
        '    adminUsers.Add("NA\Vara.Karyampudi")
        '    Return adminUsers.Contains(name)
        'End Function

        'Shared Function jExecuteSQL(ByVal sql As String, Optional ByVal connection As String = "", Optional ByVal provider As String = "") As OracleString
        '    Dim conCust As OracleConnection = Nothing
        '    Dim cmdSQL As OracleCommand = Nothing
        '    Dim dbPF As DbProviderFactory = Nothing
        '    Dim affectedRows As Integer = 0
        '    Dim Rowid As OracleString = String.Empty

        '    Try
        '        If connection.Length = 0 Then
        '            connection = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
        '        End If
        '        If provider.Length = 0 Then
        '            provider = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ProviderName
        '        End If

        '        dbPF = DbProviderFactories.GetFactory(provider)
        '        conCust = New OracleConnection(connection) 'CType(dbPF.CreateConnection, OracleConnection)
        '        'conCust.ConnectionString = connection
        '        conCust.Open()

        '        cmdSQL = CType(dbPF.CreateCommand, OracleCommand)
        '        cmdSQL.Connection = conCust
        '        cmdSQL.CommandType = CommandType.Text
        '        cmdSQL.CommandText = sql

        '        affectedRows = cmdSQL.ExecuteNonQuery()
        '        Rowid = cmdSQL.GetRowId
        '        If affectedRows = 0 Then Rowid = ""
        '    Catch ex As Exception
        '        Throw New DataException("ExecuteSQL - " & sql, ex)
        '    Finally
        '        conCust.Close()
        '        If Not conCust Is Nothing Then conCust = Nothing
        '        If Not cmdSQL Is Nothing Then cmdSQL = Nothing
        '        If Not dbPF Is Nothing Then dbPF = Nothing
        '        jExecuteSQL = Rowid
        '    End Try

        'End Function


        ''' <summary>
        ''' The shared function executes an Oracle SQL statement and returns a datareader
        ''' </summary>
        ''' <param name="sql">String - Valid SQL statement that will be executed</param>
        ''' <param name="connection">(Optional) - The connection string used to connect to Oracle</param>
        ''' <param name="provider">(Optional) - The provider for the Oracle connection</param>
        ''' <returns>OracleDataReader - </returns>
        ''' <remarks></remarks>
        Shared Function GetOracleDataReader(ByVal sql As String, Optional ByVal connection As String = "", Optional ByVal provider As String = "") As OracleDataReader
            Dim conCust As OracleConnection = Nothing
            Dim cmdSql As OracleCommand = Nothing

            Try
                If connection.Length = 0 Then
                    connection = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
                End If
                If provider.Length = 0 Then
                    provider = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ProviderName
                End If

                conCust = New OracleConnection(connection)
                conCust.Open()
                cmdSql = New OracleCommand(sql, conCust)

                Dim dr As OracleDataReader
                dr = cmdSql.ExecuteReader()

                Return dr
            Catch ex As Exception
                Return Nothing
                Throw New DataException("GetOracleDataReader - " & sql, ex)
            Finally
                If Not cmdSql Is Nothing Then cmdSql = Nothing
                If Not conCust Is Nothing Then conCust = Nothing
            End Try
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="sql"></param>
        ''' <param name="connection"></param>
        ''' <param name="provider"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Shared Function GetOracleDataSet(ByVal sql As String, Optional ByVal connection As String = "", Optional ByVal provider As String = "") As DataSet
            Dim conCust As OracleConnection = Nothing
            Dim cmdSql As OracleCommand = Nothing
            Dim dbPF As DbProviderFactory = Nothing
            Dim ds As New DataSet
            Dim myDataAdapter As New OracleDataAdapter()

            'Dim rowid As String = ""
            Try
                If connection.Length = 0 Then
                    connection = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
                End If
                'If provider.Length = 0 Then
                '    provider = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ProviderName
                'End If


                'dbPF = DbProviderFactories.GetFactory(provider)
                'conCust = CType(dbPF.CreateConnection, OracleConnection)
                'conCust.ConnectionString = connection
                conCust = New OracleConnection(connection)
                conCust.Open()
                ds.EnforceConstraints = False
                cmdSql = New OracleCommand(sql, conCust) ' CType(dbPF.CreateCommand, OracleCommand)
                'cmdSql.Connection = conCust
                'cmdSql.CommandText = sql
                myDataAdapter = New OracleDataAdapter(cmdSql)
                ds.Tables.Add("ResultTable")
                ds.Tables("ResultTable").BeginLoadData()
                myDataAdapter.Fill(ds.Tables("ResultTable"))
                ds.Tables("ResultTable").EndLoadData()

            Catch ex As Exception
                ds = Nothing
                'Return Nothing
                Throw 'ApplicationException("GetOracleDataSet - " & sql, ex)
            Finally
                GetOracleDataSet = ds
                conCust.Close()
                If Not conCust Is Nothing Then conCust = Nothing
                If Not cmdSql Is Nothing Then cmdSql = Nothing
                If Not dbPF Is Nothing Then dbPF = Nothing
                If Not myDataAdapter Is Nothing Then myDataAdapter = Nothing
                If Not ds Is Nothing Then ds = Nothing
            End Try
        End Function

        ''' <summary>
        ''' Compresses a byte array using gZip Compression
        ''' </summary>
        ''' <param name="b">an array of bytes</param>
        ''' <returns>a compressed array of bytes</returns>
        ''' <remarks></remarks>
        Public Shared Function CompressgZip(ByVal b() As Byte) As Byte()

            Dim ms As MemoryStream = Nothing
            Dim gZip As GZipStream = Nothing

            Try
                ms = New MemoryStream
                gZip = New GZipStream(ms, CompressionMode.Compress, False)
                gZip.Write(b, 0, b.Length)
                gZip.Close()
                Return ms.ToArray
            Catch ex As Exception
                Throw
            Finally
                Try
                    If Not gZip Is Nothing Then
                        gZip.Close()
                        gZip = Nothing
                    End If
                    If Not ms Is Nothing Then
                        ms.Close()
                        ms = Nothing
                    End If
                Catch ex As Exception
                End Try

            End Try

        End Function

        ''' <summary>
        ''' Decompresses a byte array using gZip Compression
        ''' </summary>
        ''' <param name="b">a compressed array of bytes</param>
        ''' <returns>an uncompressed array of bytes</returns>
        ''' <remarks></remarks>
        Public Shared Function DeCompressgZip(ByVal b() As Byte) As Byte()

            Dim ms As MemoryStream = Nothing
            Dim gZip As GZipStream = Nothing

            Dim bChunk(100) As Byte
            Dim size As Integer = 0

            Try
                ms = New MemoryStream
                gZip = New GZipStream(New MemoryStream(b), CompressionMode.Decompress, True)
                Do While True
                    size = gZip.Read(bChunk, 0, bChunk.Length)
                    If size <= 0 Then Exit Do
                    ms.Write(bChunk, 0, size)
                Loop
                gZip.Close()
                Return ms.ToArray
            Catch ex As Exception
                Throw
            Finally
                Try
                    If Not gZip Is Nothing Then
                        gZip.Close()
                        gZip = Nothing
                    End If
                    If Not ms Is Nothing Then
                        ms.Close()
                        ms = Nothing
                    End If
                Catch ex As Exception
                End Try
            End Try
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="dv"></param>
        ''' <param name="isPageIndexChanging"></param>
        ''' <param name="sortExp"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function SortDataTable(ByVal dv As DataView, ByVal isPageIndexChanging As Boolean, ByVal sortExp As String) As DataView
            Try
                If dv IsNot Nothing Then
                    ' Dim dv As DataView = New DataView(dt)           
                    If sortExp.Length > 0 Then
                        'If (isPageIndexChanging) Then
                        dv.Sort = String.Format("{0}", sortExp)
                        'Else
                        '   dv.Sort = String.Format("{0}", sortExp)
                        'End If
                    End If
                End If
            Catch ex As Exception
                Throw New ApplicationException("Default_SortDataTable", ex)
            Finally
                SortDataTable = dv
            End Try
        End Function

        ''' <summary>
        ''' This routine adds a Glyph symbol to a specified column within a Gridview
        ''' </summary>
        ''' <param name="grid"></param>
        ''' <param name="item"></param>
        ''' <param name="sortexp"></param>
        ''' <param name="sortDirec"></param>
        ''' <remarks></remarks>
        Public Shared Sub AddGlyph(ByVal grid As GridView, ByVal item As GridViewRow, ByVal sortexp As String, ByVal sortDirec As String)
            Dim glyph As New Label
            Try
                glyph.EnableTheming = False
                glyph.Font.Name = "webdings"
                glyph.Font.Size = FontUnit.Small
                glyph.Text = CStr(IIf(CDbl(sortDirec) = SortDirection.Ascending, "5", "6"))

                For i As Integer = 0 To grid.Columns.Count - 1
                    Dim colExpr As String = grid.Columns(i).SortExpression
                    If colExpr.Length > 0 And colExpr = sortexp Then
                        item.Cells(i).Controls.Add(glyph)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception("AddGlyph", ex)
            End Try

        End Sub

        Public Shared Function CleanString(ByVal strEdit As String) As String
            Return Regex.Replace(strEdit, "[\x00-\x1f]", "").Trim
        End Function

        Public Shared Function CleanString(ByVal strEdit As String, ByVal defaultValue As String) As String
            Return Regex.Replace(strEdit, "[\n]", defaultValue).Trim
        End Function


        ''' <summary>
        ''' Places a string resultset (XML) and it's criteria object into the Cache Table
        ''' </summary>
        ''' <param name="criteria">A Valid/Populated Criteria object</param>
        ''' <param name="resultSet">Typically the outerXML of an XMLDocument</param>
        ''' <remarks></remarks>
        Public Shared Sub kStoreDataIntoSqlServerCache1(ByVal criteria As Object, ByVal resultSet As String)

            Dim dbPF As DbProviderFactory = Nothing
            Dim dbConn As DbConnection = Nothing
            Dim dbCmd As DbCommand = Nothing
            Dim dbParam As DbParameter = Nothing

            Dim bf As BinaryFormatter = Nothing
            Dim ms As MemoryStream = Nothing
            Dim SQLServerCacheOn As String = ConfigurationManager.AppSettings("SQLServerCacheOn")
            Dim b() As Byte
            Dim key As String
            Dim connectString As String
            Dim providerName As String

            Try

                If SQLServerCacheOn <> "True" Then Exit Sub
                bf = New BinaryFormatter
                ms = New MemoryStream
                bf.Serialize(ms, criteria)
                b = CompressgZip(ms.ToArray)
                key = Convert.ToBase64String(b)
                b = CompressgZip(System.Text.Encoding.UTF8.GetBytes(resultSet.Trim))
                connectString = ConfigurationManager.ConnectionStrings.Item("Cache").ConnectionString
                providerName = ConfigurationManager.ConnectionStrings.Item("Cache").ProviderName
                dbPF = DbProviderFactories.GetFactory(providerName)
                dbConn = dbPF.CreateConnection
                dbConn.ConnectionString = connectString

                dbConn.Open()
                dbCmd = dbPF.CreateCommand
                dbCmd.Connection = dbConn
                dbCmd.CommandType = CommandType.StoredProcedure
                dbCmd.CommandText = "usp_InsertCache"

                dbParam = dbPF.CreateParameter()
                dbParam.ParameterName = "@Key"
                dbParam.DbType = DbType.AnsiString
                dbParam.Size = key.Length
                dbParam.Direction = ParameterDirection.Input
                dbParam.Value = key
                dbCmd.Parameters.Add(dbParam)

                dbParam = dbPF.CreateParameter()
                dbParam.ParameterName = "@Value"
                dbParam.DbType = DbType.Binary
                dbParam.Size = b.Length
                dbParam.Direction = ParameterDirection.Input
                dbParam.Value = b
                dbCmd.Parameters.Add(dbParam)

                dbParam = dbPF.CreateParameter()
                dbParam.ParameterName = "@Application"
                dbParam.DbType = DbType.String
                dbParam.Size = 50
                dbParam.Direction = ParameterDirection.Input
                dbParam.Value = ConfigurationManager.AppSettings("AppName")
                dbCmd.Parameters.Add(dbParam)

                dbCmd.ExecuteNonQuery()

            Catch ex As Exception
                Throw
            Finally
                If Not bf Is Nothing Then bf = Nothing
                If Not ms Is Nothing Then
                    ms.Close()
                    ms = Nothing
                End If
                If Not dbCmd Is Nothing Then
                    dbCmd = Nothing
                End If
                If Not (dbConn Is Nothing) Then
                    If (dbConn.State = ConnectionState.Open) Then
                        dbConn.Close()
                    End If
                End If
                If Not (dbParam Is Nothing) Then
                    dbParam = Nothing
                End If
            End Try

        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="criteria"></param>
        ''' <param name="resultSet"></param>
        ''' <remarks></remarks>
        Public Shared Sub kStoreDataIntoSqlServerCache(ByVal criteria As Object, ByVal resultSet As Object)

            Dim dbPF As DbProviderFactory = Nothing
            Dim dbConn As DbConnection = Nothing
            Dim dbCmd As DbCommand = Nothing
            Dim dbParam As DbParameter = Nothing
            Dim SQLServerCacheOn As String = ConfigurationManager.AppSettings("SQLServerCacheOn")
            Dim bf As BinaryFormatter = Nothing
            Dim ms As MemoryStream = Nothing
            Dim b() As Byte
            Dim key As String
            Dim connectString As String
            Dim providerName As String

            Try

                If SQLServerCacheOn <> "True" Then Exit Sub
                bf = New BinaryFormatter
                ms = New MemoryStream

                'bf.Serialize(ms, CType(criteria, SearchCriteria.Bookings))
                bf.Serialize(ms, criteria)

                b = CompressgZip(ms.ToArray)
                key = Convert.ToBase64String(b)

                bf = New BinaryFormatter
                ms = New MemoryStream
                bf.Serialize(ms, resultSet)
                b = CompressgZip(ms.ToArray)
                '
                'data = Convert.ToBase64String(b)

                'b = CompressgZip(System.Text.Encoding.UTF8.GetBytes(resultSet.Trim))

                connectString = ConfigurationManager.ConnectionStrings.Item("Cache").ConnectionString
                providerName = ConfigurationManager.ConnectionStrings.Item("Cache").ProviderName

                dbPF = DbProviderFactories.GetFactory(providerName)

                dbConn = dbPF.CreateConnection
                dbConn.ConnectionString = connectString
                dbConn.Open()

                dbCmd = dbPF.CreateCommand
                dbCmd.Connection = dbConn
                dbCmd.CommandType = CommandType.StoredProcedure
                dbCmd.CommandText = "usp_InsertCache"

                dbParam = dbPF.CreateParameter()
                dbParam.ParameterName = "@Key"
                dbParam.DbType = DbType.AnsiString
                dbParam.Size = key.Length
                dbParam.Direction = ParameterDirection.Input
                dbParam.Value = key
                dbCmd.Parameters.Add(dbParam)

                dbParam = dbPF.CreateParameter()
                dbParam.ParameterName = "@Value"
                dbParam.DbType = DbType.Binary
                dbParam.Size = b.Length
                dbParam.Direction = ParameterDirection.Input
                dbParam.Value = b
                dbCmd.Parameters.Add(dbParam)

                dbParam = dbPF.CreateParameter()
                dbParam.ParameterName = "@Application"
                dbParam.DbType = DbType.String
                dbParam.Size = 50
                dbParam.Direction = ParameterDirection.Input
                dbParam.Value = ConfigurationManager.AppSettings("AppName")
                dbCmd.Parameters.Add(dbParam)

                'dbcmd.Parameters.Add("@Key", SqlDbType.VarChar, key.Length).Value = key
                'dbcmd.Parameters.Add("@Value", SqlDbType.Image, b.Length).Value = b

                dbCmd.ExecuteNonQuery()

            Catch ex As Exception
                Throw
            Finally
                If Not bf Is Nothing Then bf = Nothing
                If Not ms Is Nothing Then
                    ms.Close()
                    'ms.Dispose()
                    ms = Nothing
                End If
                If Not dbCmd Is Nothing Then
                    'dbCmd.Dispose()
                End If
                If Not (dbConn Is Nothing) Then
                    If (dbConn.State = ConnectionState.Open) Then
                        dbConn.Close()
                    End If
                    'dbConn.Dispose()
                End If
                If Not (dbParam Is Nothing) Then
                    dbParam = Nothing
                End If
            End Try

        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="criteria"></param>
        ''' <remarks></remarks>
        Public Shared Sub DeleteDataFromSQLServerCache(ByVal criteria As Object)

            Dim dbPF As DbProviderFactory = Nothing
            Dim dbConn As DbConnection = Nothing
            Dim dbCmd As DbCommand = Nothing
            Dim dbParam As DbParameter = Nothing

            Dim bf As BinaryFormatter = Nothing
            Dim ms As MemoryStream = Nothing


            Try
                Dim b() As Byte
                Dim key As String
                Dim connectString As String
                Dim providerName As String

                bf = New BinaryFormatter
                ms = New MemoryStream

                'bf.Serialize(ms, CType(criteria, SearchCriteria.Bookings))
                bf.Serialize(ms, criteria)

                b = CompressgZip(ms.ToArray)
                key = Convert.ToBase64String(b)

                connectString = ConfigurationManager.ConnectionStrings.Item("Cache").ConnectionString
                providerName = ConfigurationManager.ConnectionStrings.Item("Cache").ProviderName

                dbPF = DbProviderFactories.GetFactory(providerName)

                dbConn = dbPF.CreateConnection
                dbConn.ConnectionString = connectString
                dbConn.Open()

                dbCmd = dbPF.CreateCommand
                dbCmd.Connection = dbConn
                dbCmd.CommandType = CommandType.StoredProcedure
                dbCmd.CommandText = "usp_DeleteCache"

                dbParam = dbPF.CreateParameter()
                dbParam.ParameterName = "@Key"
                dbParam.DbType = DbType.AnsiString
                dbParam.Size = key.Length
                dbParam.Direction = ParameterDirection.Input
                dbParam.Value = key
                dbCmd.Parameters.Add(dbParam)

                dbCmd.ExecuteNonQuery()

            Catch ex As Exception
                Throw
            Finally
                If Not bf Is Nothing Then bf = Nothing
                If Not ms Is Nothing Then
                    ms.Close()
                    'ms.Dispose()
                    ms = Nothing
                End If
                If Not dbCmd Is Nothing Then
                    'dbCmd.Dispose()
                End If
                If Not (dbConn Is Nothing) Then
                    If (dbConn.State = ConnectionState.Open) Then
                        dbConn.Close()
                    End If
                    'dbConn.Dispose()
                End If
                If Not (dbParam Is Nothing) Then
                    dbParam = Nothing
                End If
            End Try
        End Sub

        ''' <summary> 
        ''' 
        ''' </summary>
        ''' <param name="inputXml"></param>
        ''' <param name="xslFile"></param>
        ''' <param name="xsltArgs"></param>
        ''' <param name="frameURL"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function TransformXML(ByVal inputXml As String, ByVal xslFile As String, ByVal xsltArgs As XsltArgumentList, Optional ByVal frameURL As String = "") As String
            Dim doc As XmlDocument = Nothing
            Dim trans As XslCompiledTransform = Nothing
            Dim ioStream As IO.Stream = Nothing
            Dim sr As StreamReader = Nothing
            Dim xmlOut As XmlTextWriter = Nothing
            Dim xslt As XsltSettings = Nothing
            Dim resolver As XmlUrlResolver = Nothing

            Dim xml As String

            If inputXml.Length = 0 Then Throw New Exception("Input xml Is missing")

            If xslFile.Length = 0 Then Throw New Exception("xslFile Is missing")

            Try
                Dim xmlData As String = ""
                doc = New XmlDocument()
                xslt = New XsltSettings
                resolver = New XmlUrlResolver
                trans = New XslCompiledTransform

                resolver.Credentials = CredentialCache.DefaultCredentials
                xslt.EnableScript = True

                trans.Load(System.Web.HttpContext.Current.Server.MapPath(xslFile), xslt, resolver)
                doc.LoadXml(inputXml)
                If frameURL.Length > 0 Then
                    xmlData = doc.OuterXml
                    xmlData = Replace(xmlData, "href=""", "href=""" & frameURL)
                    doc.LoadXml(xmlData)
                End If
                ioStream = New MemoryStream

                xmlOut = New System.Xml.XmlTextWriter(ioStream, System.Text.Encoding.Default)

                trans.Transform(doc.CreateNavigator(), xsltArgs, xmlOut)
                ioStream.Position = 0
                sr = New StreamReader(ioStream)

                xml = sr.ReadToEnd
                Return xml
                'Catch ex As WebException
                'Throw
            Catch ex As Exception
                Throw
            Finally
                If Not doc Is Nothing Then
                    doc = Nothing
                End If
                If Not trans Is Nothing Then
                    trans = Nothing
                End If
                If Not ioStream Is Nothing Then
                    ioStream.Close()
                    ioStream.Dispose()
                    ioStream = Nothing
                End If
                If Not sr Is Nothing Then
                    sr.Close()
                    sr.Dispose()
                    sr = Nothing
                End If
                If Not xmlOut Is Nothing Then
                    xmlOut.Close()
                    xmlOut = Nothing
                End If
                If Not xslt Is Nothing Then
                    xslt = Nothing
                End If
                If Not resolver Is Nothing Then
                    resolver = Nothing
                End If
            End Try

        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="c"></param>
        ''' <remarks></remarks>
        Public Shared Sub DisableControls(ByVal c As Control)

            If TypeOf c Is LinkButton Then
                CType(c, LinkButton).Enabled = False
            ElseIf TypeOf c Is DropDownList Then
                CType(c, DropDownList).Enabled = False
            ElseIf TypeOf c Is HyperLink Then
                CType(c, HyperLink).NavigateUrl = Nothing
            ElseIf TypeOf c Is WebControl Then
                CType(c, WebControl).Enabled = False
            ElseIf TypeOf c Is LiteralControl Then
                CType(c, LiteralControl).Visible = False
            End If
            For Each child As Control In c.Controls
                DisableControls(child)
            Next
        End Sub
        Public Shared Function CausedPostBack(ByVal sender As String) As Boolean
            Dim currentRequest As System.Web.HttpContext
            currentRequest = System.Web.HttpContext.Current
            If currentRequest.Request.Form("__EventTarget") IsNot Nothing Then
                If currentRequest.Request.Form("__EventTarget").Contains(sender) Then
                    Return True
                Else
                    Return False
                End If
            End If
            Return False
        End Function

        Public Shared Function GetDSFromPackage(ByRef parms As OracleParameterCollection, ByVal packageName As String, Optional ByVal cacheKey As String = "", Optional ByVal cacheHours As Integer = 0) As DataSet
            Dim conCust As OracleConnection = Nothing
            Dim cmdSql As OracleCommand = Nothing
            Dim connection As String = String.Empty
            Dim provider As String = String.Empty
            Dim ds As DataSet = Nothing
            Dim daData As OracleDataAdapter = Nothing
            Dim cnConnection As OracleConnection = Nothing
            Dim sb As New StringBuilder

            Try

                'Dim cachedData As System.Web.Caching.Cache = HttpRuntime.Cache 'HttpContext.Current.Cache
                'If cachedData IsNot Nothing Then
                If cacheKey.Length > 0 And cacheHours > 0 Then
                    If HttpRuntime.Cache.Item(cacheKey) IsNot Nothing Then
                        ds = CType(HttpRuntime.Cache.Item(cacheKey), DataSet)
                        Exit Try
                    End If
                End If
                'End If

                If connection.Length = 0 Then
                    connection = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
                End If
                If provider.Length = 0 Then
                    provider = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ProviderName
                End If
                cmdSql = New OracleCommand

                With cmdSql
                    cnConnection = New OracleConnection(connection)
                    cnConnection.Open()
                    .Connection = cnConnection
                    .CommandText = packageName

                    .CommandType = CommandType.StoredProcedure
                    .PassParametersByName = True

                    For i As Integer = 0 To parms.Count - 1
                        If parms.Item(i).Value Is Nothing Then parms.Item(i).Value = DBNull.Value
                        Dim parm As New OracleParameter
                        parm.Direction = parms.Item(i).Direction
                        parm.DbType = parms.Item(i).DbType
                        parm.OracleDbType = parms.Item(i).OracleDbType
                        parm.Size = parms.Item(i).Size
                        If parms.Item(i).Direction = ParameterDirection.Input Or parms.Item(i).Direction = ParameterDirection.InputOutput Then
                            If parms.Item(i).Value IsNot Nothing Then
                                parm.Value = parms.Item(i).Value
                                If parm.Value.ToString = "" Then
                                    'parm.Value = DBNull.Value
                                End If

                            Else
                                If parm.OracleDbType = OracleDbType.NVarChar Then
                                    'parm.Value = DBNull.Value
                                    'parm.Size = 2
                                End If
                            End If
                        End If
                        parm.ParameterName = parms.Item(i).ParameterName
                        .Parameters.Add(parm)
                        If sb.Length > 0 Then sb.Append(", ")
                        sb.Append(parm.ParameterName)
                        sb.Append(" = ")
                        If parm.OracleDbType = OracleDbType.VarChar Then
                            If parm.Value IsNot Nothing Then
                                sb.Append("'" & parm.Value.ToString & "'")
            Else
                                sb.Append("Null")
                            End If
                        Else
                            sb.Append(parm.Value)
                        End If
                    Next

                End With

                ' ds = New DataSet()
                ' ds.EnforceConstraints = False
                ' daData = New OracleDataAdapter(cmdSql)

                Dim dataFillRetry As Integer = 0

                Dim fillWasSuccessful As Boolean = False

                Do While dataFillRetry < 5 And fillWasSuccessful = False

                    Try

                        ds = New DataSet()
                        ds.EnforceConstraints = False
                        daData = New OracleDataAdapter(cmdSql)

                        dataFillRetry += 1

                        daData.Fill(ds)

                        fillWasSuccessful = True

                    Catch ex As Exception

                        'Swallow exception

                    End Try

                Loop

                ' daData.Fill(ds)
                ds.EnforceConstraints = True
                If cacheHours > 0 And cacheKey.Length > 0 Then
                    If ds IsNot Nothing Then HttpRuntime.Cache.Insert(cacheKey, ds, Nothing, DateTime.Now.AddHours(cacheHours), TimeSpan.Zero)
                End If
            Catch ex As Exception
                ds = Nothing
                Throw New DataException("GetDSFromPackage - " & packageName & "-" & sb.ToString, ex)
                If Not conCust Is Nothing Then conCust = Nothing
            Finally
                GetDSFromPackage = ds
                If Not daData Is Nothing Then daData = Nothing
                If Not ds Is Nothing Then ds = Nothing
                If Not cmdSql Is Nothing Then cmdSql = Nothing
                If cnConnection IsNot Nothing Then
                    If cnConnection.State = ConnectionState.Open Then cnConnection.Close()
                    cnConnection = Nothing
                End If
            End Try
        End Function

        'Public Shared Function GetDSFromPackage(ByRef parms As OracleParameterCollection, ByVal packageName As String, Optional ByVal cacheKey As String = "", Optional ByVal cacheHours As Integer = 0) As DataSet
        '    Dim conCust As Data.OleDb.OleDbConnection = Nothing
        '    Dim cmdSQL As OleDb.OleDbCommand = Nothing
        '    Dim connection As String = String.Empty
        '    Dim provider As String = String.Empty
        '    Dim ds As DataSet = Nothing
        '    Dim daData As OleDb.OleDbDataAdapter = Nothing
        '    Dim cnConnection As OleDb.OleDbConnection = Nothing

        '    Try

        '        'Dim cachedData As System.Web.Caching.Cache = HttpRuntime.Cache 'HttpContext.Current.Cache
        '        'If cachedData IsNot Nothing Then
        '        If cacheKey.Length > 0 And cacheHours > 0 Then
        '            If HttpRuntime.Cache.Item(cacheKey) IsNot Nothing Then
        '                ds = CType(HttpRuntime.Cache.Item(cacheKey), DataSet)
        '                Exit Try
        '            End If
        '        End If
        '        'End If

        '        If connection.Length = 0 Then
        '            connection = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
        '        End If
        '        If provider.Length = 0 Then
        '            provider = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ProviderName
        '        End If
        '        cmdSQL = New OleDb.OleDbCommand
        '        HttpContext.Current.Session.CodePage = 65001
        '        With cmdSQL
        '            cnConnection = New Data.OleDb.OleDbConnection("Provider=MSDAORA;dsn=RCFATST;User ID=reladmin;password=reladmin1; Data Source=RCFATST")
        '            cnConnection.Open()
        '            .Connection = cnConnection
        '            .CommandText = packageName

        '            .CommandType = CommandType.StoredProcedure
        '            Dim sb As New StringBuilder
        '            For i As Integer = 0 To parms.Count - 1
        '                If parms.Item(i).Value Is Nothing Then parms.Item(i).Value = DBNull.Value
        '                Dim parm As New OleDb.OleDbParameter
        '                parm.Direction = parms.Item(i).Direction
        '                parm.DbType = parms.Item(i).DbType
        '                'parm. = parms.Item(i).oracledbtype
        '                parm.Size = parms.Item(i).Size
        '                If parms.Item(i).Direction = ParameterDirection.Input Or parms.Item(i).Direction = ParameterDirection.InputOutput Then
        '                    If parms.Item(i).Value IsNot Nothing Then
        '                        parm.Value = parms.Item(i).Value
        '                        If parm.Value.ToString = "" Then
        '                            'parm.Value = DBNull.Value
        '                        End If

        '                    Else
        '                        'If parm.DbType = Data.OleDb.Then Then
        '                        '    'parm.Value = DBNull.Value
        '                        '    'parm.Size = 2
        '                        'End If
        '                    End If
        '                End If
        '                parm.ParameterName = parms.Item(i).ParameterName
        '                .Parameters.Add(parm)
        '                If sb.Length > 0 Then sb.Append(",")
        '                'If parm.DbType = Data.SqlTypes.SqlString Then
        '                '    If parm.Value IsNot Nothing Then
        '                '        sb.Append("'" & parm.Value.ToString & "'")
        '                '    Else
        '                '        sb.Append("Null")
        '                '    End If
        '                'Else
        '                '    sb.Append(parm.Value)
        '                'End If
        '            Next

        '        End With

        '        ds = New DataSet()
        '        ds.EnforceConstraints = False
        '        daData = New OleDb.OleDbDataAdapter(cmdSQL)
        '        daData.Fill(ds)
        '        ds.EnforceConstraints = True
        '        If cacheHours > 0 And cacheKey.Length > 0 Then
        '            If ds IsNot Nothing Then HttpRuntime.Cache.Insert(cacheKey, ds, Nothing, DateTime.Now.AddHours(cacheHours), TimeSpan.Zero)
        '        End If
        '    Catch ex As Exception
        '        ds = Nothing
        '        Throw New DataException("GetDSFromPackage - " & packageName, ex)
        '        If Not conCust Is Nothing Then conCust = Nothing
        '    Finally
        '        GetDSFromPackage = ds
        '        If Not daData Is Nothing Then daData = Nothing
        '        If Not ds Is Nothing Then ds = Nothing
        '        If Not cmdSQL Is Nothing Then cmdSQL = Nothing
        '        If cnConnection IsNot Nothing Then
        '            If cnConnection.State = ConnectionState.Open Then cnConnection.Close()
        '            cnConnection = Nothing
        '        End If
        '    End Try
        'End Function
        Public Shared Function GetServerUrl() As String
            Dim serverUrl As String = "RIDEV"
            If HttpContext.Current.Request.ServerVariables("SERVER_NAME").ToLower.Contains("localhost") Then
                serverUrl = "RIDEV"
            ElseIf HttpContext.Current.Request.ServerVariables("SERVER_NAME").ToLower.StartsWith("ridev") Then
                serverUrl = "RIDEV"
            ElseIf HttpContext.Current.Request.ServerVariables("SERVER_NAME").ToLower.StartsWith("ritest") Then
                serverUrl = "RITEST"
            Else
                serverUrl = "RI"
            End If
            Return serverUrl & ".graphicpkg.com/"
        End Function
        Public Shared Sub ResponseRedirect(ByVal url As String) ', Optional ByVal endResponse As Boolean = False)
            RI.SharedFunctions.Trace("SharedFunctions.vb", "ResponseRedirect")
            Try
                HttpContext.Current.Response.Redirect(url, False) ' endResponse)
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            Catch ex As Exception
                HandleError("Response.Redirect", "Error attempting to redirect to [" & url & "]", ex)
            End Try
        End Sub

        Public Shared Function CallDROraclePackage(ByRef parms As OracleParameterCollection, ByVal packageName As String) As String 'OracleDataReader
            Dim conCust As OracleConnection = Nothing
            Dim cmdSQL As OracleCommand = Nothing
            Dim connection As String = String.Empty
            Dim provider As String = String.Empty
            Dim dr As OracleDataReader = Nothing
            Dim cnConnection As OracleConnection = Nothing
            Dim returnParamName As String = String.Empty
            Dim returnValue As String = String.Empty
            Dim returnParms As New StringCollection
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
                    .CommandText = packageName
                    .CommandType = CommandType.StoredProcedure
                    Dim sb As New StringBuilder
                    For i As Integer = 0 To parms.Count - 1
                        If parms.Item(i).Value Is Nothing Then parms.Item(i).Value = DBNull.Value
                        Dim parm As New OracleParameter
                        parm.Direction = parms.Item(i).Direction
                        parm.DbType = parms.Item(i).DbType
                        parm.OracleDbType = parms.Item(i).OracleDbType
                        parm.Size = parms.Item(i).Size
                        .PassParametersByName = True
                        If parms.Item(i).Direction = ParameterDirection.Input Or parms.Item(i).Direction = ParameterDirection.InputOutput Then
                            If parms.Item(i).Value IsNot Nothing Then
                                parm.Value = parms.Item(i).Value
                                If parm.Value.ToString = "" Then
                                    parm.IsNullable = True
                                    parm.Value = System.DBNull.Value
                                End If
                            Else
                                If parm.OracleDbType = OracleDbType.NVarChar Then
                                    'parm.Value = DBNull.Value
                                    'parm.Size = 2
                                End If
                            End If
                        ElseIf parms.Item(i).Direction = ParameterDirection.Output Then
                            returnParms.Add(parms.Item(i).ParameterName)
                            returnParamName = parms.Item(i).ParameterName
                        End If
                        parm.ParameterName = parms.Item(i).ParameterName
                        .Parameters.Add(parm)
                        If sb.Length > 0 Then sb.Append(",")
                        If parm.OracleDbType = OracleDbType.VarChar Then
                            If parm.Value IsNot Nothing Then
                                sb.Append(parm.ParameterName & "= '" & parm.Value.ToString & "' Type=" & parm.OracleDbType.ToString)
                            Else
                                sb.Append(parm.ParameterName & "= '" & "Null" & "' Type=" & parm.OracleDbType.ToString)
                            End If
                        Else
                            If parm.Value IsNot Nothing Then
                                sb.Append(parm.ParameterName & "= '" & parm.Value.ToString & "' Type=" & parm.OracleDbType.ToString)
                            Else
                                sb.Append(parm.ParameterName)
                            End If
                        End If
                        sb.AppendLine()

                    Next

                End With

                'dr = cmdSQL.ExecuteReader
                cmdSQL.ExecuteNonQuery()

                'Populate the original parms collection with the data from the output parameters
                For i As Integer = 0 To returnParms.Count - 1
                    parms.Item(cmdSQL.Parameters(returnParms.Item(i)).ToString).Value = cmdSQL.Parameters(returnParms.Item(i)).Value.ToString
                Next
                '// return the return value if there is one
                If returnParamName.Length > 0 Then
                    returnValue = cmdSQL.Parameters(returnParamName).Value.ToString
                Else
                    returnValue = CStr(0)
                End If

            Catch ex As Exception
                If returnValue.Length = 0 Then returnValue = "Error Occurred"
                Throw New DataException("CallDROraclePackage", ex)
                If Not conCust Is Nothing Then conCust = Nothing

            Finally
                CallDROraclePackage = returnValue
                If Not dr Is Nothing Then dr = Nothing
                If Not cmdSQL Is Nothing Then cmdSQL = Nothing
                If cnConnection IsNot Nothing Then
                    If cnConnection.State = ConnectionState.Open Then cnConnection.Close()
                    cnConnection = Nothing
                End If
            End Try
        End Function
        Public Shared Function GetDatabaseName() As String
            Dim paramCollection As New OracleParameterCollection
            Dim param As New OracleParameter
            Dim ds As System.Data.DataSet = Nothing
            Dim ret As String = String.Empty
            Try

                param = New OracleParameter
                param.ParameterName = "rsServiceName"
                param.oracledbtype = oracledbtype.Cursor
                param.Direction = Data.ParameterDirection.Output
                paramCollection.Add(param)

                Dim dr As OracleDataReader = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "Reladmin.RI.GETSERVICENAME")

                If dr IsNot Nothing Then
                    Do While dr.Read
                        If dr.Item("SERVICE_NAME") IsNot Nothing Then
                            ret = CStr(dr.Item("SERVICE_NAME"))
                        End If
                    Loop
                End If
            Catch ex As Exception
                Throw
            Finally
                GetDatabaseName = ret
            End Try
        End Function
        Public Shared Function GetOraclePackageDR(ByRef parms As OracleParameterCollection, ByVal packageName As String) As OracleDataReader
            Dim conCust As OracleConnection = Nothing
            Dim cmdSQL As OracleCommand = Nothing
            Dim connection As String = String.Empty
            Dim provider As String = String.Empty
            Dim dr As OracleDataReader = Nothing
            Dim cnConnection As OracleConnection = Nothing
            Dim returnParamName As String = String.Empty
            Dim returnValue As String = String.Empty
            Dim returnParms As New StringCollection
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
                    .CommandText = packageName
                    .CommandType = CommandType.StoredProcedure
                    .PassParametersByName = True
                    Dim sb As New StringBuilder
                    For i As Integer = 0 To parms.Count - 1
                        If parms.Item(i).Value Is Nothing Then parms.Item(i).Value = DBNull.Value
                        Dim parm As New OracleParameter
                        parm.Direction = parms.Item(i).Direction
                        parm.DbType = parms.Item(i).DbType
                        parm.oracledbtype = parms.Item(i).oracledbtype
                        parm.Size = parms.Item(i).Size
                        If parms.Item(i).Direction = ParameterDirection.Input Or parms.Item(i).Direction = ParameterDirection.InputOutput Then
                            If parms.Item(i).Value IsNot Nothing Then
                                parm.Value = parms.Item(i).Value
                                If parm.Value.ToString = "" Then
                                    parm.IsNullable = True
                                    parm.Value = System.DBNull.Value
                                End If
                            Else
                                If parm.oracledbtype = oracledbtype.NVarChar Then
                                    'parm.Value = DBNull.Value
                                    'parm.Size = 2
                                End If
                            End If
                        ElseIf parms.Item(i).Direction = ParameterDirection.Output Then
                            returnParms.Add(parms.Item(i).ParameterName)
                            returnParamName = parms.Item(i).ParameterName
                        End If
                        parm.ParameterName = parms.Item(i).ParameterName
                        .Parameters.Add(parm)
                        If sb.Length > 0 Then sb.Append(",")
                        If parm.oracledbtype = oracledbtype.VarChar Then
                            If parm.Value IsNot Nothing Then
                                sb.Append(parm.ParameterName & "= '" & parm.Value.ToString & "' Type=" & parm.oracledbtype.ToString)
                            Else
                                sb.Append(parm.ParameterName & "= '" & "Null" & "' Type=" & parm.oracledbtype.ToString)
                            End If
                        Else
                            If parm.Value IsNot Nothing Then
                                sb.Append(parm.ParameterName & "= '" & parm.Value.ToString & "' Type=" & parm.oracledbtype.ToString)
                            Else
                                sb.Append(parm.ParameterName)
                            End If
                        End If
                        sb.AppendLine()

                    Next

                End With

                dr = cmdSQL.ExecuteReader(Data.CommandBehavior.CloseConnection)
                'cmdSQL.ExecuteNonQuery()

                'Populate the original parms collection with the data from the output parameters
                For i As Integer = 0 To returnParms.Count - 1
                    parms.Item(cmdSQL.Parameters(returnParms.Item(i)).ToString).Value = cmdSQL.Parameters(returnParms.Item(i)).Value.ToString
                Next
                '// return the return value if there is one
                If returnParamName.Length > 0 Then
                    returnValue = cmdSQL.Parameters(returnParamName).Value.ToString
                Else
                    returnValue = CStr(0)
                End If

            Catch ex As Exception
                If returnValue.Length = 0 Then returnValue = "Error Occurred"
                Throw New DataException("GetOraclePackageDR", ex)
                If Not conCust Is Nothing Then conCust = Nothing

            Finally
                GetOraclePackageDR = dr
                If Not dr Is Nothing Then dr = Nothing
                If Not cmdSQL Is Nothing Then cmdSQL = Nothing
                'If cnConnection IsNot Nothing Then
                '    If cnConnection.State = ConnectionState.Open Then cnConnection.Close()
                '    cnConnection = Nothing
                'End If
            End Try
        End Function

        Shared Sub SortDropDown(ByRef dd As ListControl)
            Dim ar As ListItem() = Nothing
            Dim i As Integer = 0
            For Each li As ListItem In dd.Items
                ReDim Preserve ar(i)
                ar(i) = li
                i += 1
            Next
            Dim ar1 As Array = ar

            Array.Sort(ar1, New ListItemComparer)
            dd.Items.Clear()
            dd.Items.AddRange(CType(ar1, ListItem()))
        End Sub
        'Public Shared Sub BindList(ByVal ddl As DropDownList, ByVal obj As clsData)
        '    If obj.DataSource IsNot Nothing Then
        '        ddl.DataSource = obj.DataSource
        '        If obj.DataSource.Tables(0).Columns.Item(obj.DataTextField) IsNot Nothing Then
        '            ddl.DataTextField = obj.DataTextField
        '        End If
        '        If obj.DataSource.Tables(0).Columns.Item(obj.DataTextField) IsNot Nothing Then
        '            ddl.DataValueField = obj.DataTextField
        '        End If
        '        ddl.DataValueField = obj.DataValueField
        '        ddl.DataBind()
        '    End If
        'End Sub
        Public Shared Sub RemoveDuplicateItems(ByRef cbo As ListControl)
            Dim HT As New Hashtable
            Dim j As Integer


            If cbo.Items.Count > 0 Then
                While j < cbo.Items.Count
                    If HT.ContainsKey(cbo.Items(j)) Then
                        cbo.Items.RemoveAt(j)
                    Else
                        HT.Add(cbo.Items(j), cbo.Items(j))
                        j = j + 1
                    End If
                End While

                HT = Nothing
            End If
        End Sub

        Public Shared Sub BindList(ByRef rbl As ListControl, ByVal obj As clsData, ByVal forceUnique As Boolean, ByVal insertBlank As Boolean, Optional ByVal sortList As Boolean = True, Optional ByVal localizeList As Boolean = True)
            Dim RIResources As New IP.Bids.Localization.WebLocalization
            If obj.DataSource IsNot Nothing Then
                rbl.DataSource = obj.DataSource
                rbl.Items.Clear()
                rbl.DataTextField = obj.DataTextField
                rbl.DataValueField = obj.DataValueField

                rbl.DataBind()
                If insertBlank = True Then
                    rbl.Items.Insert(0, "")
                End If
                If forceUnique = True Then
                    RemoveDuplicateItems(rbl)
                End If

                If System.Web.HttpContext.Current.Request.Form(rbl.UniqueID.ToString) IsNot Nothing Then
                    Dim curValue As String = System.Web.HttpContext.Current.Request.Form(rbl.UniqueID.ToString)
                    If rbl.Items.FindByValue(curValue) IsNot Nothing And curValue.Length > 0 Then
                        rbl.SelectedValue = curValue
                    End If
                End If
            End If
            If localizeList = True And System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper <> "EN-US" Then
                If RIResources IsNot Nothing Then
                    RIResources.LocalizeListControl(rbl, sortList)
                End If
            End If
            obj.DataSource.Close()
            obj.DataSource = Nothing
        End Sub
        Public Shared Sub BindList(ByRef rbl As ListControl, ByVal obj As OrderedDictionary, ByVal forceUnique As Boolean, ByVal insertBlank As Boolean, Optional ByVal sortList As Boolean = True, Optional ByVal localizeList As Boolean = True)
            Dim RIResources As New IP.Bids.Localization.WebLocalization
            If obj IsNot Nothing Then
                rbl.DataSource = obj
                rbl.Items.Clear()
                rbl.DataTextField = "Key"
                rbl.DataValueField = "Value"

                rbl.DataBind()
                If insertBlank = True Then
                    rbl.Items.Insert(0, "")
                End If
                If forceUnique = True Then
                    RemoveDuplicateItems(rbl)
                End If

                If System.Web.HttpContext.Current.Request.Form(rbl.UniqueID.ToString) IsNot Nothing Then
                    Dim curValue As String = System.Web.HttpContext.Current.Request.Form(rbl.UniqueID.ToString)
                    If rbl.Items.FindByValue(curValue) IsNot Nothing And curValue.Length > 0 Then
                        rbl.SelectedValue = curValue
                    End If
                End If
            End If
            If localizeList = True And System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper <> "EN-US" Then
                If RIResources IsNot Nothing Then
                    RIResources.LocalizeListControl(rbl, sortList)
                End If
            End If

        End Sub
        Public Shared Sub BindList(ByRef rbl As ListControl, ByVal obj As clsData, ByVal forceUnique As Boolean, ByVal insertBlank As Boolean, ByVal filter As String, Optional ByVal sortList As Boolean = True, Optional ByVal localizeList As Boolean = True)

            Dim RIResources As New IP.Bids.Localization.WebLocalization
            If obj.DataSource IsNot Nothing Then

                If filter.Length = 0 Then
                    BindList(rbl, obj, forceUnique, insertBlank, sortList, localizeList)
                    Exit Sub
                End If
                Dim dr As DataTableReader = obj.DataSource
                Dim ds As DataSet = convertDataReaderToDataSet(dr)
                ds.Tables(0).DefaultView.RowFilter = filter
                rbl.Items.Clear()
                rbl.DataSource = ds.Tables(0).DefaultView
                rbl.DataTextField = obj.DataTextField
                rbl.DataValueField = obj.DataValueField

                rbl.DataBind()
                If insertBlank = True Then
                    rbl.Items.Insert(0, "")
                End If
                If forceUnique = True Then
                    RemoveDuplicateItems(rbl)
                End If

                If System.Web.HttpContext.Current.Request.Form(rbl.UniqueID.ToString) IsNot Nothing Then
                    Dim curValue As String = System.Web.HttpContext.Current.Request.Form(rbl.UniqueID.ToString)
                    If rbl.Items.FindByValue(curValue) IsNot Nothing And curValue.Length > 0 Then
                        rbl.SelectedValue = curValue
                    End If
                End If
            End If

            If localizeList = True And System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper <> "EN-US" Then
                If RIResources IsNot Nothing Then
                    RIResources.LocalizeListControl(rbl, sortList)
                End If
            End If
            obj.DataSource.Close()
            obj.DataSource = Nothing
        End Sub
        Public Shared Function convertDataReaderToDataSet(ByVal reader As DataTableReader) As DataSet
            Dim dataSet As DataSet = New DataSet
            Do
                Dim schemaTable As DataTable = reader.GetSchemaTable
                Dim dataTable As DataTable = New DataTable
                If Not (schemaTable Is Nothing) Then
                    Dim i As Integer = 0
                    While i < schemaTable.Rows.Count
                        Dim dataRow As DataRow = schemaTable.Rows(i)
                        Dim columnName As String = CType(dataRow("ColumnName"), String)
                        Dim column As DataColumn = New DataColumn(columnName, CType(dataRow("DataType"), Type))
                        dataTable.Columns.Add(column)
                        i += 1
                    End While
                    dataSet.Tables.Add(dataTable)
                    While reader.Read
                        Dim dataRow As DataRow = dataTable.NewRow
                        Dim j As Integer = 0
                        While j < reader.FieldCount
                            dataRow(j) = reader.GetValue(j)
                            j += 1
                        End While
                        dataTable.Rows.Add(dataRow)
                    End While
                Else
                    Dim column As DataColumn = New DataColumn("RowsAffected")
                    dataTable.Columns.Add(column)
                    dataSet.Tables.Add(dataTable)
                    Dim dataRow As DataRow = dataTable.NewRow
                    dataRow(0) = reader.RecordsAffected
                    dataTable.Rows.Add(dataRow)
                End If
            Loop While reader.NextResult
            Return dataSet
        End Function
        Public Shared Function ConvertReaderToDataTable(ByVal _reader As System.Data.Common.DbDataReader) As System.Data.DataTable
            Dim _dt As New System.Data.DataTable()
            Dim _dc As System.Data.DataColumn
            Dim _row As System.Data.DataRow
            Dim _al As New System.Collections.ArrayList()

            Try
                Dim _table As System.Data.DataTable = _reader.GetSchemaTable()
                For i As Integer = 0 To _table.Rows.Count - 1
                    _dc = New System.Data.DataColumn()
                    If Not _dt.Columns.Contains(_table.Rows(i)("ColumnName").ToString()) Then
                        _dc.ColumnName = _table.Rows(i)("ColumnName").ToString()
                        _dc.Unique = Convert.ToBoolean(DataClean(_table.Rows(i)("IsUnique"), "True"))
                        _dc.AllowDBNull = Convert.ToBoolean(DataClean(_table.Rows(i)("AllowDBNull"), "True"))
                        _dc.[ReadOnly] = Convert.ToBoolean(DataClean(_table.Rows(i)("IsReadOnly"), "True"))
                        _al.Add(_dc.ColumnName)
                        _dt.Columns.Add(_dc)
                    End If
                Next
                While _reader.Read()
                    _row = _dt.NewRow()
                    For i As Integer = 0 To _al.Count - 1
                        _row(DirectCast(_al(i), String)) = _reader(DirectCast(_al(i), String))
                    Next
                    _dt.Rows.Add(_row)
                End While
            Catch ex As Exception
                Throw
            End Try
            Return _dt
        End Function

        Private Class ListItemComparer
            Implements System.Collections.IComparer

            Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
                Dim a As ListItem = CType(x, ListItem)
                Dim b As ListItem = CType(y, ListItem)
                Dim c As New CaseInsensitiveComparer(System.Threading.Thread.CurrentThread.CurrentCulture)
                Return c.Compare(a.Text, b.Text)
            End Function

        End Class
        Public Shared Function GetUserProfile() As CurrentUserProfile
            Dim userProfile As CurrentUserProfile = Nothing
            Dim username As String
            username = CurrentUserProfile.GetCurrentUser
            Dim ses As System.Web.SessionState.HttpSessionState = HttpContext.Current.Session
            If ses IsNot Nothing Then
                If ses.Item(Replace(username, "\", "_")) IsNot Nothing Then
                    userProfile = CType(ses.Item(Replace(username, "\", "_")), CurrentUserProfile)
                End If
            End If
            If userProfile Is Nothing Then userProfile = New CurrentUserProfile
            If Not userProfile Is Nothing Then
                userProfile.SetProfileDefaults()
                ses.Add(Replace(username, "\", "_"), userProfile)
            End If
            Return userProfile
        End Function

        Shared Function AdjustTextForDisplay(ByVal text As String, ByVal targetWidth As Integer, ByVal ft As Font) As String
            Dim textSize As Drawing.SizeF = MeasureString(text, ft)
            Dim colWidth As Integer = targetWidth
            If textSize.Width > colWidth Then
                Dim delta As Integer = CType((textSize.Width - colWidth), Integer)
                Dim avgCharWidth As Integer = CType((textSize.Width / text.Length), Integer)
                Dim chrToTrim As Integer = CType((delta / avgCharWidth), Integer)
                Dim rawText As String = text.Substring(0, text.Length - (chrToTrim + 2)) '+ "..."
                Dim fmt As String = "{1}"
                Return String.Format(fmt, text, rawText)
            End If
            Return text
        End Function
        Shared Function MeasureString(ByVal text As String, ByVal fontInfo As Font) As SizeF
            Dim size As SizeF
            Dim emSize As Single = fontInfo.Size 'Convert.ToSingle(fontInfo.Size.Unit.Value + 1)
            If emSize = 0 Then
                emSize = 12
            End If
            Dim stringFont As Font = New Font(fontInfo.Name, emSize)
            Dim bmp As Bitmap = New Bitmap(1000, 100)
            Dim g As Graphics = Graphics.FromImage(bmp)
            size = g.MeasureString(text, stringFont)
            g.Dispose()
            Return size
        End Function

        Public Shared Function JoinValues(ByVal value As String) As String
            Return "'" & String.Join("','", value.Split(CChar(","))) & "'"
        End Function

        Public Shared Function GetCheckBoxValues(ByVal cb As CheckBoxList) As String
            Dim sb As New StringBuilder
            For i As Integer = 0 To cb.Items.Count - 1
                If cb.Items(i).Selected Then
                    ' List the selected items
                    If sb.Length > 0 Then sb.Append(",")
                    sb.Append(cb.Items(i).Value.Trim)
                End If
            Next
            Return sb.ToString
        End Function
        Public Shared Sub SetCheckBoxValues(ByRef cb As CheckBoxList, ByVal value As String, Optional ByVal delimeter As String = ",")
            value = DataClean(value, "")
            Dim list As String() = value.Split(CChar(","))
            If list.Length > 0 Then
                cb.ClearSelection()
                For i As Integer = 0 To list.Length - 1
                    If cb.Items.FindByValue(list(i).Trim) IsNot Nothing Then
                        cb.Items.FindByValue(list(i).Trim).Selected = True
                    End If
                Next
            Else
                If cb.Items.FindByValue(value) IsNot Nothing Then
                    cb.SelectedValue = value
                End If
            End If
        End Sub
        Public Shared Function GetListBoxValues(ByVal cb As ListBox) As String
            Dim sb As New StringBuilder
            For i As Integer = 0 To cb.Items.Count - 1
                If cb.Items(i) IsNot Nothing Then
                    ' List the selected items
                    If sb.Length > 0 Then sb.Append(",")
                    sb.Append(cb.Items(i).Value.Trim)
                End If
            Next
            Return sb.ToString
        End Function
        Public Shared Sub SetListBoxValues(ByRef cb As ListBox, ByVal value As String, Optional ByVal delimeter As String = ",")
            Dim list As String() = value.Split(CChar(","))
            If list.Length > 0 Then
                For i As Integer = 0 To list.Length - 1
                    If cb.Items.FindByValue(list(i).Trim) IsNot Nothing Then
                        cb.Items.FindByValue(list(i).Trim).Selected = True
                    Else
                        cb.Items.Add(list(i))
                    End If
                Next
            Else
                If cb.Items.FindByValue(value) IsNot Nothing Then
                    cb.SelectedValue = value
                End If
            End If
        End Sub
        Shared Sub DisablePageCache(ByVal resp As System.Web.HttpResponse)
            resp.Cache.SetCacheability(HttpCacheability.NoCache)
            resp.Cache.SetExpires(Now.AddSeconds(-1))
            resp.Cache.SetNoStore()
            resp.AppendHeader("Pragma", "no-cache")

        End Sub
        Shared Sub SendEmail(ByVal toaddress As String,
                             ByVal fromAddress As String,
                             ByVal subject As String,
                             ByVal body As String,
                             Optional ByVal displayName As String = "Graphic Packaging",
                             Optional ByVal carbonCopy As String = "",
                             Optional ByVal blindCarbonCopy As String = "",
                             Optional ByVal IsBodyHtml As Boolean = True,
                             Optional ByVal Attachment As Mail.AttachmentCollection = Nothing)

            RI.SharedFunctions.Trace("SharedFunctions.vb", subject + " SendEmail")

            Dim mail As System.Net.Mail.MailMessage = New MailMessage '= New MailMessage(New MailAddress(fromAddress, displayName), New MailAddress(toaddress))
            Dim riResources As New IP.Bids.Localization.WebLocalization

            Dim OkToSend As Boolean = False
            Dim inputAddress As New StringBuilder
            Try
                toaddress = DataClean(toaddress)
                fromAddress = DataClean(fromAddress)

                inputAddress.Append("<p>ToAddress:")
                inputAddress.Append(toaddress)
                inputAddress.Append("<br>")
                inputAddress.Append("fromAddress:")
                inputAddress.Append(fromAddress)
                inputAddress.Append("<br>")
                inputAddress.Append("carbonCopy:")
                inputAddress.Append(carbonCopy)
                inputAddress.Append("<br>")
                inputAddress.Append("blindCarbonCopy:")
                inputAddress.Append(blindCarbonCopy)
                inputAddress.Append("</p>")

                If toaddress IsNot Nothing AndAlso toaddress.Length > 0 Then
                    Dim toEmail As String() = Split(toaddress, ",")
                    For i As Integer = 0 To toEmail.Length - 1
                        If toEmail(i).Length > 0 And isEmail(toEmail(i)) Then
                            mail.To.Add(toEmail(i))
                        End If
                    Next
                    If mail.To.Count > 0 Then OkToSend = True
                    'Else
                    'Exit Sub
                End If

                If carbonCopy IsNot Nothing AndAlso carbonCopy.Length > 0 Then
                    Dim copyEmail As String() = Split(carbonCopy, ",")
                    For i As Integer = 0 To copyEmail.Length - 1
                        If copyEmail(i).Length > 0 And isEmail(copyEmail(i)) Then
                            mail.CC.Add(copyEmail(i))
                        End If
                    Next
                    If mail.CC.Count > 0 Then OkToSend = True
                End If

                If blindCarbonCopy IsNot Nothing AndAlso blindCarbonCopy.Length > 0 Then
                    Dim bccEmail As String() = Split(blindCarbonCopy, ",")
                    For i As Integer = 0 To bccEmail.Length - 1
                        If bccEmail(i).Length > 0 And isEmail(bccEmail(i)) Then
                            mail.Bcc.Add(bccEmail(i))
                        End If
                    Next
                    If mail.Bcc.Count > 0 Then OkToSend = True
                End If

                If fromAddress.Trim.Length > 0 And isEmail(fromAddress) Then
                    mail.From = New MailAddress(fromAddress, displayName)
                Else
                    mail.From = New MailAddress("DoNotReply@GraphicPkg.com", "Graphic Packaging International")
                End If
                mail.Priority = MailPriority.High
                mail.IsBodyHtml = IsBodyHtml

                'If HttpContext.Current.Request.UserHostAddress = "127.0.0.1" Or HttpContext.Current.Request.UserHostAddress = "http://ridev" Then
                '    'Return Receipt
                '    mail.Headers.Add("Disposition-Notification-To", fromAddress)
                'End If
                If Attachment IsNot Nothing Then
                    For Each singleAttachment As Mail.Attachment In Attachment
                        mail.Attachments.Add(singleAttachment)
                    Next
                End If

                'ALA 10/15/2014 - The next block of code was orginally added in 2011 to fix an issue with 
                ' the subject line of the email getting garbled. UNderlying issue was that when non-ascii 
                ' characters were being used, the size of the subject line was exceeded the Outlook limit.
                ' So we put in code to truncate any non-ASCII subject to 60 characters.  We did not know that
                ' vb.net will throw an error if you substr and the length parameter you pass in is larger than
                ' the length of the string.  
                Dim isNonAscii As Boolean
                For Each c As Char In subject
                    If AscW(c) > 127 Then
                        'Non Acsii character has been detected
                        isNonAscii = True
                        Exit For
                    End If
                Next
                If isNonAscii Then
                    If Len(subject) >= 60 Then
                        subject = subject.Substring(0, 60) & "..."
                    End If
                End If

                mail.Subject = CleanString(subject, " ")

                mail.Body = CleanString(body, "<br>")

                'Send the email message
                If OkToSend = True Then
                    RI.SharedFunctions.InsertAuditRecord("Send Email", "The following email has been sent -  " & Mid(body, 1, 3000) & "<br> Recipients:" & Mid(inputAddress.ToString, 1, 500))
                    Dim emailTryCount As Integer = 0
                    Dim emailSuccess As Boolean = False
                    Do While emailTryCount < 5 And emailSuccess = False
                        Dim client As SmtpClient = New SmtpClient()
                        Try
                            With client
                                emailTryCount += 1
                                .Timeout = 1000000
                                .Send(mail)
                                emailSuccess = True
                            End With
                        Catch ex As SmtpException
                            HttpContext.Current.Server.ClearError()
                            System.Threading.Thread.Sleep(1000)
                            RI.SharedFunctions.InsertAuditRecord("Send Email Error", "The following email was not sent - Retry(" & emailTryCount & ") -  " & Mid(body, 1, 3000) & "<br> Recipients:" & Mid(inputAddress.ToString, 1, 500))
                        Finally
                            client = Nothing
                        End Try

                    Loop
                Else
                    RI.SharedFunctions.InsertAuditRecord("Send Email", "This attempted email message was not sent b/c of a missing recipient.  " & Mid(body, 1, 3000) & inputAddress.ToString)
                    Dim additionalErrorMsg As String = String.Format("<b>" & riResources.GetResourceValue("An error occurred while trying to send an email to the following email address:") & "[{0}].<br>" & riResources.GetResourceValue("Please forward the below information to the person assigned and contact RI administrator to correct their email address.") & "</b>", inputAddress.ToString)
                    mail.Body = additionalErrorMsg & "<br><br>" & mail.Body
                    mail.To.Add(mail.From)
                    mail.Subject = riResources.GetResourceValue("Email Error") & " - " & mail.Subject
                    Dim emailTryCount As Integer = 0
                    Dim emailSuccess As Boolean = False
                    Do While emailTryCount < 5 And emailSuccess = False
                        Dim client As SmtpClient = New SmtpClient()
                        Try
                            With client
                                emailTryCount += 1
                                .Timeout = 1000000
                                .Send(mail)
                                RI.SharedFunctions.InsertAuditRecord("Send Email", "The following email has been sent to the sender because the To Address was invalid -  " & Mid(body, 1, 3000) & "<br> Recipients:" & inputAddress.ToString)
                                emailSuccess = True
                            End With
                        Catch ex As SmtpException
                            HttpContext.Current.Server.ClearError()
                            System.Threading.Thread.Sleep(1000)
                        Finally
                            client = Nothing
                        End Try

                    Loop
                End If
            Catch ex As SmtpException
                'Throw
                'Dim err As String = ex.Message
                'HttpContext.Current.Server.ClearError()
                RI.SharedFunctions.HandleError("Send Email", "This attempted email message was not sent b/c :" & ex.Message & "<br>" & body & inputAddress.ToString, ex)
            Catch ex As Exception
                'Dim err As String = ex.Message
                'HttpContext.Current.Server.ClearError()
                RI.SharedFunctions.HandleError("Send Email", "This attempted email message was not sent b/c :" & ex.Message & "<br>" & body & inputAddress.ToString, ex)
            Finally
                If mail IsNot Nothing Then mail = Nothing

            End Try
        End Sub
        Public Shared Function isEmail(ByVal inputEmail As String) As Boolean
            inputEmail = DataClean(inputEmail)
            Dim strRegex As String = "^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + "\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + ".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
            Dim re As New Regex(strRegex)
            If re.IsMatch(inputEmail) Then
                Return (True)
            Else
                Return (False)
            End If
        End Function
        Public Shared Sub InsertAuditRecord(ByVal sourceName As String, ByVal errorMessage As String)
            'INSERT INTO RCFA_AUDIT_LOG VALUES ('DeleteRINumber', SYSDATE, SUBSTR(V_ERRMSG,1,1000) );
            Dim paramCollection As New OracleParameterCollection
            Dim param As New OracleParameter
            Dim ds As System.Data.DataSet = Nothing

            Try

                param = New OracleParameter
                param.ParameterName = "in_name"
                param.oracledbtype = oracledbtype.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = sourceName
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_desc"
                param.oracledbtype = oracledbtype.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = errorMessage
                paramCollection.Add(param)

                Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.RIAUDIT.InsertErrorRecord")
                If CDbl(returnStatus) <> 0 Then
                    'Throw New Data.DataException("Error Marking Analysis Complete for " & RINumber)
                End If
            Catch ex As Exception
                HttpContext.Current.Server.ClearError()
                'Throw New Data.DataException("Error Marking Analysis Complete for " & RINumber)
            Finally
                param = Nothing
                paramCollection = Nothing
            End Try
        End Sub
        Public Shared Sub HandleError(Optional ByVal MethodName As String = "RI", Optional ByVal additionalErrMsg As String = "", Optional ByVal excep As Exception = Nothing)
            Dim le As Exception
            Dim errorMessage As StringBuilder = New StringBuilder
            Dim errorCount As Integer = 0
            Dim errMsg As String = String.Empty
            Dim chunkLength As Integer = 0
            Dim maxLen As Integer = 3500
            Dim redirectURL As String = "~/RI/Help/ErrorPage.aspx"
            Try
                If excep IsNot Nothing Then
                    le = excep
                Else
                    le = HttpContext.Current.Server.GetLastError
                End If

                If le IsNot Nothing Then
                    If le.Message.Contains("does not exist") Then
                        HttpContext.Current.Server.ClearError()
                        redirectURL = "~/PageNotFound.aspx?err=" & le.Message
                        Exit Try
                    ElseIf le.Message.Contains("Path 'OPTIONS' is forbidden.") Then
                        HttpContext.Current.Server.ClearError()
                        Exit Try
                    End If

                    Dim sb As New StringBuilder

                    Do While le IsNot Nothing
                        errorCount = errorCount + 1
                        'errorMessage.Length = 0
                        errorMessage.Append("<Table width=100% border=1 cellpadding=2 cellspacing=2 bgcolor='#cccccc'>")
                        errorMessage.Append("<tr><th colspan=2><h2>Page Error</h2></th>")
                        errorMessage.Append("<tr><td><b>User:</b></td><td>{0}</td></tr>")
                        errorMessage.Append("<tr><td><b>Exception #</b></td><td>{1}</td></tr>")
                        errorMessage.Append("<tr><td><b>Browser Info:</b></td><td>{2}</td></tr>")
                        errorMessage.Append("<tr><td><b>Page:</b></td><td>{3}</td></tr>")
                        errorMessage.Append("<tr><td><b>Time:</b></td><td>{4}</td></tr>")
                        errorMessage.Append("<tr><td><b>Details:</b></td><td>{5}</td></tr>")
                        errorMessage.Append("<tr><td><b>Additional Info:</b></td><td>{6}</td></tr>")
                        errorMessage.Append("</table>")
                        errMsg = errorMessage.ToString
                        errMsg = String.Format(errMsg, RI.CurrentUserProfile.GetCurrentUser, errorCount, HttpContext.Current.Request.ServerVariables("HTTP_USER_AGENT"), HttpContext.Current.Request.Url, FormatDateTime(Now, DateFormat.LongDate), le.ToString, additionalErrMsg)
                        additionalErrMsg = ""
                        le = le.InnerException
                        errorMessage.Length = 0

                        For i As Integer = 0 To errMsg.Length Step maxLen
                            If errMsg.Length < maxLen Then
                                chunkLength = errMsg.Length - 1
                            Else
                                If errMsg.Length - i < maxLen Then
                                    chunkLength = errMsg.Length - i
                                Else
                                    chunkLength = maxLen
                                End If
                            End If

                            Dim errValue As String = errMsg.Substring(i, chunkLength)
                            RI.SharedFunctions.InsertAuditRecord(MethodName, errValue)
                            System.Threading.Thread.Sleep(1000) ' Sleep for 1 second
                        Next
                    Loop
                End If
                HttpContext.Current.Session.Clear()
            Catch ex As Exception
                HttpContext.Current.Server.ClearError()
            Finally
                le = Nothing
                Try
                    HttpContext.Current.Server.ClearError()
                    HttpContext.Current.Response.Redirect(redirectURL, False)
                Catch e As Exception
                    HttpContext.Current.Server.ClearError()
                End Try
            End Try
        End Sub
        Public Shared Sub ASPNET_MsgBox(ByVal Message As String)

            System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)

            System.Web.HttpContext.Current.Response.Write("alert(""" & Message & """)" & vbCrLf)

            System.Web.HttpContext.Current.Response.Write("</SCRIPT>")

        End Sub
        Public Shared Sub ASPNET_confirmBox(ByVal Message As String)

            System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)

            System.Web.HttpContext.Current.Response.Write("confirm(""" & Message & """)" & vbCrLf)

            System.Web.HttpContext.Current.Response.Write("</SCRIPT>")

        End Sub
        ''' <summary>
        ''' Change the case of the first letter of each word to upper case.   
        ''' </summary>
        ''' <param name="text">The string to convert to title case.</param>
        ''' <param name="culture">The culture information to be used.</param>
        ''' <param name="forceCasing">When true, forces all words to be lower case before changing everything to title case.</param>
        ''' <returns>The string in title case.</returns>
        Public Shared Function ToTitleCase(ByVal text As String, ByVal culture As System.Globalization.CultureInfo, ByVal forceCasing As Boolean) As String
            If (forceCasing) Then
                Return culture.TextInfo.ToTitleCase(text.ToLower())
            Else
                Return culture.TextInfo.ToTitleCase(text)
            End If
        End Function

        ''' <summary>
        ''' Change the case of the first letter of each word to upper case.
        ''' </summary>
        ''' <param name="text">The string to convert to title case.</param>
        ''' <returns>The string in title case.</returns>
        ''' <remarks></remarks>
        Public Shared Function ToTitleCase(ByVal text As String) As String
            Return ToTitleCase(text, System.Threading.Thread.CurrentThread.CurrentCulture, False)
        End Function

        ''' <summary>
        ''' Change the case of the first letter of each word to upper case.
        ''' </summary>
        ''' <param name="text">The string to convert to title case</param>
        ''' <param name="forceCasing">When true, forces all words to be lower case before changing everything to title case</param>
        ''' <returns>The string in title case</returns>
        ''' <remarks></remarks>
        Public Shared Function ToTitleCase(ByVal text As String, ByVal forceCasing As Boolean) As String
            Return ToTitleCase(text, System.Threading.Thread.CurrentThread.CurrentCulture, forceCasing)
        End Function

        ''' <summary>
        ''' Change the case of the first letter of each word to upper case.
        ''' </summary>
        ''' <param name="text">The string to convert to title case</param>
        ''' <param name="culture">The culture information to be used</param>
        ''' <returns>The string in title case</returns>
        ''' <remarks></remarks>
        Public Shared Function ToTitleCase(ByVal text As String, ByVal culture As System.Globalization.CultureInfo) As String
            Return ToTitleCase(text, culture, False)
        End Function
        Public Shared Function InitCulture(ByVal selectedCulture As String) As String
            Dim userprofile As RI.CurrentUserProfile = RI.SharedFunctions.GetUserProfile
            Dim returnCulture As String = ""

            Try
                Dim cultureIsSet As Boolean

                If selectedCulture.Length > 0 Then
                    cultureIsSet = SetCulture(selectedCulture)
                    If cultureIsSet = True Then returnCulture = selectedCulture
                End If

                If cultureIsSet = False Then
                    returnCulture = "Auto"
                End If
            Catch
                Throw
            Finally

            End Try
            Return returnCulture
        End Function
        Public Shared Function SetCulture(ByVal culture As String) As Boolean
            Dim returnValue As Boolean
            If (culture <> "Auto") Then
                Try
                    'Dim ci As New System.Globalization.CultureInfo(culture)
                    Dim ci As System.Globalization.CultureInfo
                    ci = System.Globalization.CultureInfo.GetCultureInfo(culture)
                    System.Threading.Thread.CurrentThread.CurrentCulture = ci
                    System.Threading.Thread.CurrentThread.CurrentUICulture = ci
                    'Me.UICulture = culture
                    'Me.Culture = culture
                    returnValue = True
                Catch ex As ArgumentNullException
                    'System.ArgumentNullException: name is null.
                    returnValue = False
                Catch ex As System.ArgumentException
                    'System.ArgumentException: name specifies a culture that is not supported.
                    returnValue = False
                Catch
                    Throw
                End Try
            End If
            Return returnValue
        End Function

        Public Shared Function LocalizeValue(ByVal data As String) As String
            Dim riResources As IP.Bids.Localization.WebLocalization
            If HttpContext.Current.Request.Cookies("SelectedCulture") IsNot Nothing Then
                Dim lang As String = HttpContext.Current.Request.Cookies("SelectedCulture").Value
                Dim appID As String = ConfigurationManager.AppSettings("ResourceApplicationID")
                riResources = New IP.Bids.Localization.WebLocalization(lang, appID)
            Else
                riResources = New IP.Bids.Localization.WebLocalization()
            End If
            Return riResources.GetResourceValue(data)
        End Function

        Public Shared Function LocalizeValue(ByVal data As String, ByVal showNotFound As Boolean) As String
            Dim riResources As IP.Bids.Localization.WebLocalization
            If HttpContext.Current.Request.Cookies("SelectedCulture") IsNot Nothing Then
                Dim lang As String = HttpContext.Current.Request.Cookies("SelectedCulture").Value
                Dim appID As String = ConfigurationManager.AppSettings("ResourceApplicationID")
                riResources = New IP.Bids.Localization.WebLocalization(lang, appID)
            Else
                riResources = New IP.Bids.Localization.WebLocalization()
            End If
            Return riResources.GetResourceValue(data, showNotFound)
        End Function
        Public Shared Function LocalizeDropDownNameValue(ByVal data As AjaxControlToolkit.CascadingDropDownNameValue()) As AjaxControlToolkit.CascadingDropDownNameValue()
            Dim riResources As IP.Bids.Localization.WebLocalization
            If HttpContext.Current.Request.Cookies("SelectedCulture") IsNot Nothing Then
                Dim lang As String = HttpContext.Current.Request.Cookies("SelectedCulture").Value
                Dim appID As String = ConfigurationManager.AppSettings("ResourceApplicationID")
                riResources = New IP.Bids.Localization.WebLocalization(lang, appID)
            Else
                riResources = New IP.Bids.Localization.WebLocalization()
            End If
            If riResources.CurrentLocale.ToUpper <> "EN-US" Then
                Dim newData As New Generic.List(Of AjaxControlToolkit.CascadingDropDownNameValue)
                For i As Integer = 0 To data.Length - 1
                    newData.Add(New AjaxControlToolkit.CascadingDropDownNameValue(riResources.GetResourceValue(data(i).name), data(i).value, data(i).isDefaultValue))
                Next
                riResources = Nothing
                Return newData.ToArray
            Else
                Return data
            End If
        End Function

        Public Shared Function LocalizeDropDownNameValue(ByVal data As AjaxControlToolkit.CascadingDropDownNameValue(), ByVal compoundFieldDelimiter As String) As AjaxControlToolkit.CascadingDropDownNameValue()
            If compoundFieldDelimiter.Length = 0 Then
                Return LocalizeDropDownNameValue(data)
                Exit Function
            End If
            Dim riResources As IP.Bids.Localization.WebLocalization
            If HttpContext.Current.Request.Cookies("SelectedCulture") IsNot Nothing Then
                Dim lang As String = HttpContext.Current.Request.Cookies("SelectedCulture").Value
                Dim appID As String = ConfigurationManager.AppSettings("ResourceApplicationID")
                riResources = New IP.Bids.Localization.WebLocalization(lang, appID)
            Else
                riResources = New IP.Bids.Localization.WebLocalization()
            End If
            If riResources.CurrentLocale.ToUpper <> "EN-US" Then
                Dim newData As New Generic.List(Of AjaxControlToolkit.CascadingDropDownNameValue)
                For i As Integer = 0 To data.Length - 1
                    Dim dataValues As String() = Split(data(i).name, compoundFieldDelimiter)
                    Dim newDataValue As String
                    If dataValues.Length > 1 Then
                        Dim sb As New StringBuilder
                        For j As Integer = 0 To dataValues.Length - 1
                            sb.Append(riResources.GetResourceValue(dataValues(j).Trim))
                            If j < dataValues.Length - 1 Then
                                sb.Append(compoundFieldDelimiter)
                            End If
                        Next
                        newDataValue = sb.ToString
                    Else
                        newDataValue = data(i).name
                    End If
                    newData.Add(New AjaxControlToolkit.CascadingDropDownNameValue(newDataValue, data(i).value, data(i).isDefaultValue))
                Next
                riResources = Nothing
                Return newData.ToArray
            Else
                Return data
            End If
        End Function

        Public Shared Function FormatDateTimeToEnglish(ByVal dateValue As Date) As String
            Dim monthValue As Integer = dateValue.Month
            Dim dayValue As Integer = dateValue.Day
            Dim yearValue As Integer = dateValue.Year
            Dim newDate As Date = New Date(yearValue, monthValue, dayValue)
            Dim tmp As String = IP.Bids.Localization.DateTime.GetLocalizedDateTime(newDate, "EN-US", "d")
            Return tmp
        End Function

        Public Shared Function FormatDateTimeToEnglish(ByVal dateValue As Date, ByVal includeTime As Boolean) As String
            Dim monthValue As Integer = dateValue.Month
            Dim dayValue As Integer = dateValue.Day
            Dim yearValue As Integer = dateValue.Year
            Dim newDate As Date = New Date(yearValue, monthValue, dayValue)
            Dim tmp As String
            If includeTime = True Then
                tmp = IP.Bids.Localization.DateTime.GetLocalizedDateTime(newDate, "EN-US", "G")
            Else
                tmp = IP.Bids.Localization.DateTime.GetLocalizedDateTime(newDate, "EN-US", "d")
            End If
            Return tmp
        End Function
        Public Shared Function CDateFromEnglishDate(ByVal dateValue As String) As Date
            Dim newDate As Date
            Dim cI As System.Globalization.CultureInfo
            Try
                cI = System.Globalization.CultureInfo.GetCultureInfo("EN-US")
                newDate = Convert.ToDateTime(dateValue, cI)
            Catch
                Throw
            End Try
            Return newDate
        End Function

        Public Shared Function NewWriteExcelXML(ByRef source As Data.Common.DbDataReader, ByVal excludedFields As ArrayList) As String
            Dim gv As New GridView
            With gv
                .DataSource = source
                .AutoGenerateColumns = True
                .DataBind()
            End With

            Dim columnsExcludedFromFormat As New Hashtable

            Using sw As New StringWriter()
                Dim hw As New HtmlTextWriter(sw)
                'To Export all pages
                gv.AllowPaging = False
                gv.HeaderRow.BackColor = Color.White

                'For Each cell As TableCell In gv.HeaderRow.Cells
                '    cell.BackColor = gv.HeaderStyle.BackColor
                '    If excludedFields.Contains(cell.Text) Then
                '        columnsExcludedFromFormat.Add(1, cell.Text)
                '    End If
                'Next

                For col As Integer = 0 To gv.HeaderRow.Cells.Count - 1
                    gv.HeaderRow.Cells(col).BackColor = gv.HeaderStyle.BackColor
                    If excludedFields.Contains(gv.HeaderRow.Cells(col).Text) Then
                        columnsExcludedFromFormat.Add(col, gv.HeaderRow.Cells(col).Text)
                    End If
                    gv.HeaderRow.Cells(col).Text = LocalizeValue(gv.HeaderRow.Cells(col).Text)
                Next

                'Dim riResources As New IP.Bids.Localization.WebLocalization()
                For Each r As GridViewRow In gv.Rows
                    For col As Integer = 0 To r.Cells.Count - 1
                        If columnsExcludedFromFormat.ContainsKey(col) Then
                            r.Cells(col).Text = (r.Cells(col).Text)
                        ElseIf IsDate(r.Cells(col).Text) Then
                            r.Cells(col).Text = Format(FormatDateTimeToEnglish(CDate(r.Cells(col).Text)), "d-MMM-yy")
                        ElseIf IsNumeric(r.Cells(col).Text) Then
                            r.Cells(col).Text = FormatNumber(r.Cells(col).Text, 2)
                        Else
                            r.Cells(col).Text = LocalizeValue(r.Cells(col).Text)
                        End If
                    Next
                    'For Each c As TableCell In r.Cells
                    '    If IsDate(c.Text) Then
                    '        c.Text = FormatDateTime(CDate(c.Text), DateFormat.GeneralDate)
                    '    End If
                    '    If IsNumeric(c.Text) Then
                    '        c.Text = FormatNumber(c.Text, 2)
                    '    End If
                    'Next
                Next
                gv.RenderControl(hw)
                Return sw.ToString()
            End Using
            Exit Function
        End Function

        Public Shared Function WriteExcelXml(ByRef source As Data.Common.DbDataReader, ByVal excludedFields As ArrayList) As String



            Dim excelDoc As New System.Text.StringBuilder

            If excludedFields IsNot Nothing Then
                excludedFields.Sort()
            Else
                excludedFields = New ArrayList
            End If
            Dim startExcelXML As String = _
                "<?xml version='1.0' ?>" & vbCrLf & _
                "<Workbook " & _
                "xmlns='urn:schemas-microsoft-com:office:spreadsheet' " & vbCrLf & _
                "xmlns:o='urn:schemas-microsoft-com:office:office' " & vbCrLf & _
                "xmlns:x='urn:schemas-microsoft-com:office:excel' " & vbCrLf & _
                "xmlns:ss='urn:schemas-microsoft-com:office:spreadsheet'>" & vbCrLf & _
                "<Styles>" & vbCrLf & _
                "<Style ss:ID='Default' ss:Name='Normal'>" & vbCrLf & _
                "<Alignment ss:Vertical='Bottom'/>" & vbCrLf & _
                "<Borders/>" & vbCrLf & _
                "<Font/>" & vbCrLf & _
                "<Interior/>" & vbCrLf & _
                "<NumberFormat/>" & vbCrLf & _
                "<Protection/>" & vbCrLf & _
                "</Style>" & vbCrLf & _
                "<Style ss:ID='BoldColumn'>" & vbCrLf & _
                "<Font x:Family='Swiss' ss:Bold='1'/>" & vbCrLf & _
                "</Style>" & vbCrLf & _
                "<Style ss:ID=""s21"">" & vbCrLf & _
                "<NumberFormat ss:Format=""m/d/yyyy;@""/>" & vbCrLf & _
                "</Style>" & vbCrLf & _
                "<Style ss:ID=""s23"">" & vbCrLf & _
                "<NumberFormat ss:Format=""Standard""/>" & vbCrLf & _
                "</Style>" & vbCrLf & _
                "<Style ss:ID=""s24""><NumberFormat/></Style>" & vbCrLf & _
                "<Style ss:ID=""s26"">" & vbCrLf & _
                "<NumberFormat ss:Format=""[$-419]d\ mmm\ yy;@""/></Style>" & vbCrLf & _
                "<Style ss:ID=""s28"">" & vbCrLf & _
                "<NumberFormat ss:Format=""[ENG][$-409]d\-mmm\-yy;@""/></Style>" & vbCrLf & _
                "<Style ss:ID=""s66"">" & vbCrLf & _
                "<NumberFormat ss:Format=""[$-416]d\-mmm\-yy;@""/></Style>" & vbCrLf & _
                "<Style ss:ID=""s64""><NumberFormat ss:Format=""[$-40C]d\-mmm\-yy;@""/></Style>" & vbCrLf & _
                "</Styles>"

            Dim endExcelXML As String = "</Workbook>"
            Dim rowCount As Integer = 0
            Dim sheetCount As Integer = 0
            Dim newPage As Boolean = True
            Dim maxRows As Integer = 64000
            Dim iploc As New IP.Bids.Localization.WebLocalization

            ' Write XML header for Excel (changing single to double quotes)
            excelDoc.Append(Replace(startExcelXML, "'", Chr(34)))

            '// Write dataset
            Do While source.Read()
                rowCount = rowCount + 1

                'if the number of rows is > 64000 create a new page to continue output
                If (rowCount > maxRows) Then
                    rowCount = 0
                    newPage = True
                End If

                ' Start a new page?
                If (newPage) Then

                    newPage = False

                    ' Close out subsequent pages
                    If (sheetCount > 0) Then

                        excelDoc.AppendLine("</Table>")
                        excelDoc.AppendLine(" </Worksheet>")

                    End If

                    ' Start a new worksheet
                    sheetCount = sheetCount + 1
                    excelDoc.AppendLine("<Worksheet ss:Name=""Sheet" & sheetCount & """>")
                    excelDoc.AppendLine("<Table>")
                    excelDoc.AppendLine("<Row>")

                    '// Write column headers
                    Dim z As Integer = 0

                    Do While (z < source.FieldCount)
                        excelDoc.Append("<Cell ss:StyleID=""BoldColumn""><Data ss:Type=""String"">")
                        excelDoc.Append(iploc.GetResourceValue(source.GetName(z))) 'Tables(0).Columns(z).ColumnName)
                        excelDoc.AppendLine("</Data></Cell>")
                        z = z + 1
                    Loop

                    excelDoc.AppendLine("</Row>")

                End If

                ' Write out a row of data
                excelDoc.AppendLine("<Row>")

                Dim y As Integer = 0
                Dim sb As New System.Text.StringBuilder
                Dim xxx As New XmlTextWriter(New System.IO.StringWriter(sb))

                Do While (y < source.FieldCount)

                    Dim rowType As System.Type

                    rowType = source.Item(y).GetType()

                    Select Case (rowType.ToString)

                        Case "System.String"
                            If Not isDateValue(source.Item(y).ToString) Then
                                Dim XMLstring As String = source.Item(y).ToString

                                If excludedFields.BinarySearch(source.GetName(y), CaseInsensitiveComparer.Default) < 0 Then
                                    XMLstring = iploc.GetResourceValue(XMLstring)
                                End If
                                XMLstring = XMLstring.Trim
                                XMLstring = XMLstring.Replace("&", "&")
                                XMLstring = XMLstring.Replace(">", ">")
                                XMLstring = XMLstring.Replace("<", "<")


                                sb = New System.Text.StringBuilder
                                xxx = New XmlTextWriter(New System.IO.StringWriter(sb))
                                xxx.WriteString(XMLstring)
                                xxx.Flush()
                                XMLstring = sb.ToString()


                                excelDoc.Append("<Cell>" & _
                                    "<Data ss:Type=""String"">")
                                excelDoc.Append(XMLstring)
                                excelDoc.AppendLine("</Data></Cell>")
                            Else
                                Dim XMLDate As Date = CType(source.Item(y), Date)
                                Dim dtformat As String = "{0}-{1}-{2}T00:00:00.000"
                                Dim yr As String = CStr(XMLDate.Year)
                                Dim mon As String = CStr(XMLDate.Month)
                                Dim dy As String = CStr(XMLDate.Day)
                                If mon.Length <> 2 Then mon = "0" & mon
                                If dy.Length <> 2 Then dy = "0" & dy
                                Dim dt As String = String.Format(dtformat, yr, mon, dy)

                                Dim styleKey As String = "s21"
                                If System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper(System.Globalization.CultureInfo.CurrentCulture) = "EN-US" Then
                                    styleKey = "s28"
                                ElseIf System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper(System.Globalization.CultureInfo.CurrentCulture) = "RU-RU" Then
                                    styleKey = "s26"
                                ElseIf System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper(System.Globalization.CultureInfo.CurrentCulture) = "PT-BR" Then
                                    styleKey = "s66"
                                ElseIf System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper(System.Globalization.CultureInfo.CurrentCulture) = "FR-FR" Then
                                    styleKey = "s64"
                                End If
                                excelDoc.Append("<Cell ss:StyleID=""" & styleKey & """>" & _
                                    "<Data ss:Type=""DateTime"">")
                                excelDoc.Append(dt)
                                excelDoc.AppendLine("</Data></Cell>")
                            End If

                        Case "System.DateTime"
                            Dim XMLDate As Date = CType(source.Item(y), Date)
                            Dim dtformat As String = "{0}-{1}-{2}T00:00:00.000"
                            Dim yr As String = CStr(XMLDate.Year)
                            Dim mon As String = CStr(XMLDate.Month)
                            Dim dy As String = CStr(XMLDate.Day)
                            If mon.Length <> 2 Then mon = "0" & mon
                            If dy.Length <> 2 Then dy = "0" & dy
                            Dim dt As String = String.Format(dtformat, yr, mon, dy)

                            Dim styleKey As String = "s21"
                            If System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper(System.Globalization.CultureInfo.CurrentCulture) = "EN-US" Then
                                styleKey = "s28"
                            ElseIf System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper(System.Globalization.CultureInfo.CurrentCulture) = "RU-RU" Then
                                styleKey = "s26"
                            ElseIf System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper(System.Globalization.CultureInfo.CurrentCulture) = "PT-BR" Then
                                styleKey = "s66"
                            ElseIf System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper(System.Globalization.CultureInfo.CurrentCulture) = "FR-FR" Then
                                styleKey = "s64"
                            End If
                            excelDoc.Append("<Cell ss:StyleID=""" & styleKey & """>" & _
                                "<Data ss:Type=""DateTime"">")
                            excelDoc.Append(dt)
                            excelDoc.AppendLine("</Data></Cell>")

                            'excelDoc.Append("<Cell ss:StyleID=""s21"">" & _
                            '    "<Data ss:Type=""DateTime"">")
                            'excelDoc.Append(dt)
                            'excelDoc.AppendLine("</Data></Cell>")

                        Case "System.Boolean"
                            Dim XMLString As String = source.Item(y).ToString
                            excelDoc.Append("<Cell>" & _
                                "<Data ss:Type=""String"">")
                            If excludedFields.BinarySearch(source.GetName(y), CaseInsensitiveComparer.Default) < 0 Then
                                XMLString = iploc.GetResourceValue(XMLString)
                            End If
                            excelDoc.Append(XMLString)
                            excelDoc.AppendLine("</Data></Cell>")

                        Case "System.Int16", "System.Int32", "System.Int64", "System.Byte"
                            Dim XMLString As String = source.Item(y).ToString

                            If excludedFields.BinarySearch(source.GetName(y), CaseInsensitiveComparer.Default) < 0 Then
                                excelDoc.Append("<Cell ss:StyleID=""s23""><Data ss:Type=""Number"">")
                            Else
                                excelDoc.Append("<Cell ss:StyleID=""s24""><Data ss:Type=""Number"">")
                                '    <Cell ss:StyleID="s24"><Data ss:Type="Number">81657</Data></Cell>
                            End If
                            excelDoc.Append(XMLString)
                            excelDoc.AppendLine("</Data></Cell>")

                        Case "System.Decimal", "System.Double"
                            Dim XMLString As String = source.Item(y).ToString
                            '<Cell ss:StyleID="s23"><Data ss:Type="Number">234234</Data></Cell>
                            'excelDoc.Append("<Cell ss:StyleID=""s23"">" & _
                            '   "<Data ss:Type=""Number"">")
                            If excludedFields.BinarySearch(source.GetName(y), CaseInsensitiveComparer.Default) < 0 Then
                                '    XMLString = FormatNumber(XMLString, 2)
                                excelDoc.Append("<Cell ss:StyleID=""s23""><Data ss:Type=""Number"">")
                                excelDoc.Append(IP.Bids.Localization.Numbers.GetLocalizedNumber(CSng(XMLString), "en-us"))
                            Else
                                excelDoc.Append("<Cell ss:StyleID=""s24""><Data ss:Type=""Number"">")
                                excelDoc.Append(XMLString)

                            End If

                            excelDoc.AppendLine("</Data></Cell>")

                        Case "System.DBNull"
                            excelDoc.Append("<Cell>" & _
                                "<Data ss:Type=""String"">")
                            excelDoc.Append("")
                            excelDoc.AppendLine("</Data></Cell>")

                        Case Else

                            Throw New ArgumentException((rowType.ToString & " not handled."))

                    End Select

                    y = y + 1

                Loop

                excelDoc.AppendLine("</Row>")

            Loop

            ' Close out XML and flush
            excelDoc.AppendLine("</Table>")
            excelDoc.AppendLine("</Worksheet>")
            excelDoc.Append(endExcelXML)
            Return excelDoc.ToString

        End Function

        Public Shared Sub InitializeDataMaintenance(ByRef maintenanceControl As RIDataMaintenance.DataMaintenaceSelector, ByVal isPostBack As Boolean)
            Dim userProfile = RI.SharedFunctions.GetUserProfile
            Dim defaults As New RIUserDefaults.CurrentUserDefaults(userProfile.Username, RIUserDefaults.Applications.RI, ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString)
            maintenanceControl.ConnectionString = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
            maintenanceControl.UserName = userProfile.Username
            maintenanceControl.PopulateData()
            If Not isPostBack Then
                If defaults.DoesDefaultValueExist(RIUserDefaults.UserProfileTypes.PlantCode) Then
                    maintenanceControl.DefaultSite = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.PlantCode).ToString
                End If
            End If

            'Dim appList = maintenanceControl.GetApplicationList
            'For Each item In appList
            '    item.ApplicationName = RI.SharedFunctions.LocalizeValue(item.ApplicationName, True)
            'Next

            'maintenanceControl.SiteText = RI.SharedFunctions.LocalizeValue(maintenanceControl.SiteText, True)
            'maintenanceControl.ApplicationText = RI.SharedFunctions.LocalizeValue(maintenanceControl.ApplicationText, True)
            'maintenanceControl.FunctionName = RI.SharedFunctions.LocalizeValue(maintenanceControl.FunctionName, True)
            'maintenanceControl.FunctionNameDescription = RI.SharedFunctions.LocalizeValue(maintenanceControl.FunctionNameDescription, True)
            'maintenanceControl.PopulateApplicationList(appList)
            'Dim maintenanceFunctions = maintenanceControl.GetMaintenanceFunctions
            'For Each item In maintenanceFunctions
            '    item.PageName = RI.SharedFunctions.LocalizeValue(item.PageName, True)
            '    item.Description = RI.SharedFunctions.LocalizeValue(item.Description, True)
            'Next
            'maintenanceControl.PopulateMaintenanceTable(maintenanceFunctions)

        End Sub

        Public Shared Sub PopulateDataMaintenance(ByRef maintenanceControl As RIDataMaintenance.DataMaintenaceSelector, ByVal isPostBack As Boolean)
            Dim userProfile = RI.SharedFunctions.GetUserProfile
            'Dim defaults As New RIUserDefaults.CurrentUserDefaults(userProfile.Username, RIUserDefaults.Applications.RI, ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString)
            maintenanceControl.ConnectionString = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
            maintenanceControl.UserName = userProfile.Username
            'maintenanceControl.PopulateData()
            'If Not isPostBack Then
            '    If defaults.DoesDefaultValueExist(RIUserDefaults.UserProfileTypes.PlantCode) Then
            '        maintenanceControl.DefaultSite = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.PlantCode).ToString
            '    End If
            'End If
            '
           
            Dim appList = maintenanceControl.GetApplicationList
            For Each item In appList
                item.ApplicationName = RI.SharedFunctions.LocalizeValue(item.ApplicationName, True)
            Next

            maintenanceControl.SiteText = RI.SharedFunctions.LocalizeValue(maintenanceControl.SiteText, True)
            maintenanceControl.ApplicationText = RI.SharedFunctions.LocalizeValue(maintenanceControl.ApplicationText, True)
            maintenanceControl.FunctionName = RI.SharedFunctions.LocalizeValue(maintenanceControl.FunctionName, True)
            maintenanceControl.FunctionNameDescription = RI.SharedFunctions.LocalizeValue(maintenanceControl.FunctionNameDescription, True)
            If Not isPostBack Then
                maintenanceControl.SetSelectedSite(maintenanceControl.DefaultSite)
                maintenanceControl.SetSelectedApp(maintenanceControl.DefaultApplication)
            End If
            maintenanceControl.PopulateApplicationList(appList)
            Dim maintenanceFunctions = maintenanceControl.GetMaintenanceFunctions
            For Each item In maintenanceFunctions
                item.PageName = RI.SharedFunctions.LocalizeValue(item.PageName, True)
                item.Description = RI.SharedFunctions.LocalizeValue(item.Description, True)
            Next
            maintenanceControl.PopulateMaintenanceTable(maintenanceFunctions)

        End Sub

        Public Shared Function GetJson(ByVal dt As Data.DataTable) As String
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim rows As New System.Collections.Generic.List(Of System.Collections.Generic.Dictionary(Of String, Object))()
            'Dim rows As New List(Of Dictionary(Of String, Object))()
            Dim row As Dictionary(Of String, Object) = Nothing
            For Each dr As Data.DataRow In dt.Rows
                row = New Dictionary(Of String, Object)()
                For Each dc As Data.DataColumn In dt.Columns
                    'If dc.ColumnName.Trim() = "TAGNAME" Then
                    row.Add(dc.ColumnName.Trim(), dr(dc))
                    'End If
                Next
                rows.Add(row)
            Next
            Return serializer.Serialize(rows)
        End Function

        Public Shared Function GetLocalizedJQueryDateFormat() As String
            Return "d MM yy" '"d M yy" '"M d, yy"
        End Function


        Public Shared Sub Trace(ByVal sourceName As String, ByVal traceMessage As String)

            TraceStack()
            'ADDED 1/9/2019 JEB
            Try
                If CBool(ConfigurationManager.AppSettings("Tracing")) Then
                    Dim userProfile As RI.CurrentUserProfile = Nothing
                    Dim userName As String = String.Empty
                    userProfile = RI.SharedFunctions.GetUserProfile
                    userName = userProfile.Username
                    RI.SharedFunctions.InsertAuditRecord(userName + " " + sourceName, traceMessage + " Time: " + DateTime.Now.ToString)

                End If

            Catch ex As Exception
                HttpContext.Current.Server.ClearError()
                'Throw New Data.DataException("Error Marking Analysis Complete for " & RINumber)
            Finally

            End Try
        End Sub

        Public Shared Sub TraceFunctions(ByVal sourceName As String, ByVal traceMessage As String, ByVal log As Boolean)
            'ADDED 1/9/2019 JEB

            TraceStack()

            Try
                If log Then
                    Dim userProfile As RI.CurrentUserProfile = Nothing
                    Dim userName As String = String.Empty
                    userProfile = RI.SharedFunctions.GetUserProfile
                    userName = userProfile.Username
                    RI.SharedFunctions.InsertAuditRecord(userName + " " + sourceName, traceMessage + " Time: " + DateTime.Now.ToString)

                End If

            Catch ex As Exception
                HttpContext.Current.Server.ClearError()
                'Throw New Data.DataException("Error Marking Analysis Complete for " & RINumber)
            Finally

            End Try
        End Sub

        Public Shared Sub TraceStack()

            Dim SstackTrace As String = Environment.StackTrace

            'ADDED 1/9/2019 JEB
            Try
                If CBool(ConfigurationManager.AppSettings("TracingFunctions")) Then
                    Dim userProfile As RI.CurrentUserProfile = Nothing
                    Dim userName As String = String.Empty
                    userProfile = RI.SharedFunctions.GetUserProfile
                    userName = userProfile.Username
                    RI.SharedFunctions.InsertAuditRecord(userName + " " + "TraceStack", SstackTrace + " Time: " + DateTime.Now.ToString)
                End If


            Catch ex As Exception
                HttpContext.Current.Server.ClearError()
                'Throw New Data.DataException("Error Marking Analysis Complete for " & RINumber)
            Finally

            End Try


        End Sub



    End Class
End Namespace