Imports System.Net
Imports System.Data.OleDb
Partial Class Multilingual
    Inherits RIBasePage

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("GlobalizationDemo", True))
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim ipLoc As New IP.Bids.Localization.WebLocalization()

        Dim sql As String = "select * from (SELECT distinct SubArea FROM reladmin.line_system_vw where siteid in ('SG')  order by subarea) where rownum < 11"
        Dim ds As System.Data.DataSet = Nothing
        Dim cacheKey As String = RI.SharedFunctions.CreateKey(sql)
        If Cache.Item(cacheKey) Is Nothing Then
            ds = RI.SharedFunctions.GetOracleDataSet(sql)
            If ds IsNot Nothing Then
                Cache.Add(cacheKey, ds, Nothing, Now.AddHours(1), Nothing, CacheItemPriority.Default, Nothing)
            End If

        Else
            ds = CType(Cache.Item(cacheKey), Data.DataSet)
            If ds Is Nothing Then ds = RI.SharedFunctions.GetOracleDataSet(sql)
        End If
      
        If ds IsNot Nothing Then
            With Me._swaplist
                Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader 'Master.RIRESOURCES.LocalizeData(ds.Tables(0).CreateDataReader, "Area", False)
                .DataSource = dr
                .DataTextField = "subarea"
                .DataValueField = "subarea"
                .LocalizeData = True
                .DataBind()
            End With

            With _ddlAreas
                .Items.Clear()
                .DataSource = ds.Tables(0).CreateDataReader
                .DataTextField = "subarea"
                .DataValueField = "subarea"
                .DataBind()
            End With

            With _cblAreas
                .Items.Clear()
                .DataSource = ds.Tables(0).CreateDataReader
                .DataTextField = "subarea"
                .DataValueField = "subarea"
                .RepeatColumns = 2
                .RepeatDirection = RepeatDirection.Horizontal
                .DataBind()
            End With
            With _rblAreas
                .Items.Clear()
                .DataSource = ds.Tables(0).CreateDataReader
                .DataTextField = "subarea"
                .DataValueField = "subarea"
                .RepeatColumns = 2
                .RepeatDirection = RepeatDirection.Horizontal
                .DataBind()
            End With
            Master.RIRESOURCES.LocalizeListControl(_ddlAreas)
            Master.RIRESOURCES.LocalizeListControl(_rblAreas)
            Master.RIRESOURCES.LocalizeListControl(_cblAreas)


        End If

        ds = Nothing

        Master.ShowRIMenu()
        Dim sql2 As String = "SELECT distinct RISuperArea FROM reladmin.line_system_vw where siteid='SG'  order by RISuperArea "
        Dim ds2 As System.Data.DataSet = Nothing
        Dim cacheKey2 As String = RI.SharedFunctions.CreateKey(sql2)
        If Cache.Item(cacheKey2) Is Nothing Then
            ds2 = RI.SharedFunctions.GetOracleDataSet(sql2)
            If ds2 IsNot Nothing Then
                Cache.Add(cacheKey2, ds2, Nothing, Now.AddHours(1), Nothing, CacheItemPriority.Default, Nothing)
            End If
        Else
            ds2 = CType(Cache.Item(cacheKey2), Data.DataSet)
            If ds2 Is Nothing Then ds2 = RI.SharedFunctions.GetOracleDataSet(sql2)
        End If
        With Me._lblBusinessunit
            .DataSource = ds2.Tables(0).CreateDataReader
            .DataTextField = "RISuperArea"
            .DataValueField = "RISuperArea"
            .DataBind()
        End With
        Master.RIRESOURCES.LocalizeListControl(_lblBusinessunit)
        Me._lnkResourceFile.Text = String.Empty
        Dim dr2 As Data.DataTableReader
        sql = "SELECT distinct eventdate,RCFA_TYPE,incident,sitename,risuperarea,subarea,area,rinumber,totcost,cost FROM viewupdatescreen where upper(localename)=upper('{0}') and rownum <20  and siteid='SG'"
        sql = String.Format(sql, Master.RIRESOURCES.CurrentLocale)
        Dim ds3 As New Data.DataSet
        ds3 = TryCast(Cache.Item("TestGridViewData_" & Master.RIRESOURCES.CurrentLocale), Data.DataSet)
        If ds3 Is Nothing Then
            ds3 = RI.SharedFunctions.GetOracleDataSet(sql)
            Cache.Add("TestGridViewData", ds3, Nothing, Now.AddDays(1), Nothing, CacheItemPriority.Default, Nothing)
        End If

        dr2 = ds3.CreateDataReader

        _txtCostField.Text = IP.Bids.Localization.Currency.GetLocalizedCurrency(250569.96)
        Dim sExclude As New ArrayList
        sExclude.Add("RINumber")        
        Dim ipLocalize As New IP.Bids.Localization.DataLocalization(Master.RIRESOURCES)
        dr2 = ipLocalize.LocalizeData(dr2, sExclude)
        With _gv
            .datasource = dr2
            .databind()
        End With
        ds3 = Nothing

        Dim stext As String = Me.TranslateGoogle("This is a test", "en-us", "ru-ru")
        'With _gv
        '    Dim dr As Oracle.DataAccess.Client.OracleDataReader = GetOracleDataReader("select EQUIPMENTDESC from reladmin.refequipment where siteid='SG' and rownum <20")
        '    .DataSource = dr 'GetOleDBRS("select EQUIPMENTDESC from reladmin.refequipment where siteid='SG' and rownum <25")
        '    .DataBind()
        'End With
        'Me._text.Text = IP.BIDS.ResourceExpression.GetGlobalResourceObject("test")
        'Response.Charset = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ANSICodePage
        'Me._text.Text = Master.RIRESOURCES.TranslateGoogle(Me._text.Text, PreviousCulture, Master.RIRESOURCES.LocaleLanguage)
        'Me._lblDisplay.Text = Master.RIRESOURCES.TranslateGoogle("This is a test of How I'm doing.", Master.RIRESOURCES.DefaultLocaleLanguage, Master.RIRESOURCES.LocaleLanguage)

    End Sub

    Protected Sub _displayExcel_DisplayExcel_Click() Handles _displayExcel.DisplayExcel_Click
        Dim dr2 As Data.DataTableReader
        Dim sql As String = "SELECT distinct eventdate,RCFA_TYPE,incident,sitename,risuperarea,subarea,area,rinumber,totcost,cost FROM viewupdatescreen where rownum <20  and siteid='AU' and area is not null"
        Dim ds3 As New Data.DataSet
        ds3 = TryCast(Cache.Item("TestGridViewData"), Data.DataSet)
        If ds3 Is Nothing Then
            ds3 = RI.SharedFunctions.GetOracleDataSet(sql)
            Cache.Add("TestGridViewData", ds3, Nothing, Now.AddDays(1), Nothing, CacheItemPriority.Default, Nothing)
        End If

        dr2 = ds3.CreateDataReader


        Dim sExclude As New ArrayList
        sExclude.Add("RINumber")
        sExclude.Add("Cost")

        Dim ipLocalize As New IP.Bids.Localization.DataLocalization(Master.RIRESOURCES)
        dr2 = ipLocalize.LocalizeData(dr2, sExclude)

        
        ipLocalize.ExportToExcel(ds3.CreateDataReader, "c:\Multilingual.xls", sExclude)
        Me._lnkResourceFile.Text = "c:\Multilingual.xls"
        Me._lnkResourceFile.NavigateUrl = "file:///" & Replace(Me._lnkResourceFile.Text, "\", "/")

        Me._displayExcel.DisplayExcel(dr2)
        ds3 = Nothing

        
    End Sub

    ''' <summary> 
    ''' Translates a string into another language using Google's Translation Pages.   
    ''' </summary> 
    ''' <param name="Text"></param> 
    ''' <param name="FromCulture"> 
    ''' Two letter culture (en of en-us, fr of fr-ca, de of de-ch) 
    ''' </param> 
    ''' <param name="ToCulture"> 
    ''' Two letter culture 
    ''' </param> 
    Public Function TranslateGoogle(ByVal Text As String, ByVal FromCulture As String, ByVal ToCulture As String) As String
        Dim ret As String = String.Empty
        Dim ci As New System.Globalization.CultureInfo(FromCulture)
        Try
            If Text.Length = 0 Then
                Return ""
                Exit Function
            End If
            FromCulture = FromCulture.ToLower()
            ToCulture = ToCulture.ToLower()

            Dim Tokens As String() = FromCulture.Split("-"c)
            If Tokens.Length > 1 Then
                FromCulture = Tokens(0)
            End If

            Tokens = ToCulture.Split("-"c)
            If Tokens.Length > 1 Then
                ToCulture = Tokens(0)
            End If

            Dim LangPair As String = FromCulture + "|" + ToCulture
            If FromCulture.ToLower = ToCulture.ToLower Then
                Return Text
                Exit Function
            End If
            Dim url As String = String.Format("http://www.google.com/translate_t?langpair={0}", LangPair)

            'System.Text.Encoding.GetEncoding(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ANSICodePage)
            Dim Result As String = ReadHtmlPage(url, "text=" & Text, System.Text.Encoding.GetEncoding(ci.TextInfo.ANSICodePage))

            ret = HttpUtility.HtmlDecode(ExtractString(Result, "<div id=result_box dir=""ltr"">", "</div>", False, False))
        Catch ex As Exception
            Throw New ApplicationException("::TranslateGoogle -" & Text & " -- " & FromCulture & " - " & ToCulture, ex)
        End Try
        Return ret
    End Function
    Private Function ExtractString(ByVal Source As String, ByVal BeginDelim As String, ByVal EndDelim As String, ByVal CaseSensitive As Boolean, ByVal AllowMissingEndDelimiter As Boolean) As String
        If Not String.IsNullOrEmpty(Source) Then
            Dim At1 As Integer
            Dim At2 As Integer
            If CaseSensitive Then
                At1 = Source.IndexOf(BeginDelim)
                If (At1 = -1) Then
                    Return ""
                End If
                At2 = Source.IndexOf(EndDelim, CInt((At1 + BeginDelim.Length)))
            Else
                Dim Lower As String = Source.ToLower
                At1 = Lower.IndexOf(BeginDelim.ToLower)
                If (At1 = -1) Then
                    Return ""
                End If
                At2 = Lower.IndexOf(EndDelim.ToLower, CInt((At1 + BeginDelim.Length)))
            End If
            If (AllowMissingEndDelimiter AndAlso (At2 = -1)) Then
                Return Source.Substring((At1 + BeginDelim.Length))
            End If
            If ((At1 > -1) AndAlso (At2 > 1)) Then
                Return Source.Substring((At1 + BeginDelim.Length), ((At2 - At1) - BeginDelim.Length))
            End If
        End If
        Return ""
    End Function
    Private Function ReadHtmlPage(ByVal url As String, ByVal postData As String, ByVal encoding As System.Text.Encoding) As String
        Dim result As String = ""

        'Dim strPost As String = "Text=This is a test"
        Dim myWriter As IO.StreamWriter = Nothing

        Try
            Dim objRequest As Net.HttpWebRequest = Net.WebRequest.Create(url)
            objRequest.Method = "POST"
            objRequest.ContentLength = postData.Length
            objRequest.ContentType = "application/x-www-form-urlencoded"
            objRequest.KeepAlive = False
            objRequest.ConnectionGroupName = Guid.NewGuid().ToString()

            Try

                objRequest.Timeout = 300000
                myWriter = New IO.StreamWriter(objRequest.GetRequestStream(), encoding) 'System.Text.Encoding.GetEncoding(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ANSICodePage))
                myWriter.Write(postData)
            Catch e As Exception
                Return e.Message
            Finally
                If Not myWriter Is Nothing Then myWriter.Close()
            End Try

            Dim objResponse As Net.HttpWebResponse = objRequest.GetResponse()
            Dim sr As IO.StreamReader
            sr = New IO.StreamReader(objResponse.GetResponseStream(), System.Text.Encoding.GetEncoding(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ANSICodePage))
            result = sr.ReadToEnd()
            If Not sr Is Nothing Then sr.Close()
        Catch ex As Exception
            Throw New ApplicationException("::ReadHtmlPage -" & url & " - " & postData, ex)
        End Try
        Return result
    End Function

    Protected Sub _btnExportResources_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnExportResources.Click
        Me._lnkResourceFile.Text = Master.RIRESOURCES.ExportToResourceFile("c:\")
        Me._lnkResourceFile.NavigateUrl = Me._lnkResourceFile.Text
    End Sub
End Class
