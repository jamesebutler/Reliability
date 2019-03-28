Imports System.Data
Imports System.IO
Imports System.Xml
'Imports system.Runtime.InteropServices
Partial Class DisplayExcel
    Inherits RIBasePage
    Protected Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
        Dim ex As Exception
        ex = Server.GetLastError
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Dim currentCulture As String = System.Globalization.CultureInfo.CurrentCulture.Name
            If currentCulture.ToLower <> "en-us" Then
                Dim ci As System.Globalization.CultureInfo
                ci = System.Globalization.CultureInfo.GetCultureInfo("en-us")
                System.Threading.Thread.CurrentThread.CurrentCulture = ci
                System.Threading.Thread.CurrentThread.CurrentUICulture = ci

            End If
            Dim excelDoc As System.IO.StreamWriter
            ' Send output to response stream
            excelDoc = New System.IO.StreamWriter(Me.Response.OutputStream)
            Dim excelFile As String = "RIExcel" & Now.ToString.Replace(" ", "_").Replace(":", "_") & ".xls"

            Try
                'Dim excel As Object = CreateObject("Excel.Application")
                'excelVersion = excel.Version         
                'Marshal.ReleaseComObject(excel)

                If Session("ExcelXML") IsNot Nothing Then
                    Response.Clear()
                    'If excelVersion > 10 Then
                    Response.AddHeader("Content-Disposition", "attachment; FileName=" & excelFile)
                    'End If
                    Response.Buffer = False
                    'Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.AddHeader("cache-control", "must-revalidate")
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.ms-excel"
                    Response.ContentEncoding = System.Text.Encoding.UTF7

                    excelDoc.Write(Session("ExcelXML"))



                    'Page.ClientScript.RegisterStartupScript(Page.GetType, "CloseWindow", sb.ToString, False)
                Else
                    Response.Write("<h2>We are missing the Excel Data</h2>")
                End If
            Catch ex As Exception
                'Response.Write(ex.Message)
                Server.ClearError()

            Finally
                Session.Remove("ExcelXML")
                If currentCulture.ToLower <> "en-us" Then
                    Dim ci As System.Globalization.CultureInfo
                    ci = System.Globalization.CultureInfo.GetCultureInfo(currentCulture)
                    System.Threading.Thread.CurrentThread.CurrentCulture = ci
                    System.Threading.Thread.CurrentThread.CurrentUICulture = ci

                End If
                excelDoc.Flush()
                excelDoc.Close()
                Response.End()
            End Try
        End If
    End Sub
   
End Class
