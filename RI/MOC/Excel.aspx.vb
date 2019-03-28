Imports System.io
Imports Devart.Data.Oracle
Imports System.Data.SqlClient
Imports System.Data

Partial Class Excel
    Inherits RIBasePage

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub
    Private Sub DisableControls(ByVal gv As Control)
        Dim lb As LinkButton = New LinkButton()
        Dim l As Literal = New Literal()
        Dim name As String = String.Empty

        Try
            For i As Integer = 0 To (gv.Controls.Count - 1)
                If i < gv.Controls.Count Then
                    If TypeOf gv.Controls(i) Is LinkButton Then
                        l.Text = CType(gv.Controls(i), LinkButton).Text
                        gv.Controls.Remove(gv.Controls(i))
                        gv.Controls.AddAt(i, l)
                    ElseIf TypeOf gv.Controls(i) Is DropDownList Then
                        l.Text = CType(gv.Controls(i), DropDownList).Text
                        gv.Controls.Remove(gv.Controls(i))
                        gv.Controls.AddAt(i, l)
                    ElseIf TypeOf gv.Controls(i) Is HyperLink Then
                        l.Text = CType(gv.Controls(i), HyperLink).Text
                        gv.Controls.Remove(gv.Controls(i))
                        gv.Controls.AddAt(i, l)
                    ElseIf TypeOf gv.Controls(i) Is Image Then
                        gv.Controls.Remove(gv.Controls(i))
                    Else
                        'Trace.Write("log", gv.Controls(i).GetType.ToString)
                        'System.Diagnostics.Debug.WriteLine(gv.Controls(i).GetType.ToString)
                    End If
                    If gv.Controls(i).HasControls Then
                        DisableControls(gv.Controls(i))
                    End If
                End If
            Next
        Catch ex As Exception
            Throw New Exception("DisableControls", ex.InnerException)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim id As String = CType(Request.QueryString("id"), String)
        Dim ds As DataSet = Nothing
        Dim sql As String = String.Empty

        Dim fileName As String = "MOC"
        Dim excelData As String = String.Empty

        If id Is Nothing Then id = 0
        Try
            Select Case id
                Case 0

                Case 1
                    Dim clsExcel As New clsMOCViewSearch
                    clsExcel = Session.Item("clsExcelSearch")
                    Dim key As String
                    key = "MOCExcelSearch_" & clsExcel.Facility & "_" & clsExcel.Division & "_" & clsExcel.BusinessUnit & "_" & clsExcel.Area & "_" & clsExcel.Line & "_" & clsExcel.LineBreak & "_" & clsExcel.StartDate & "_" & clsExcel.EndDate & "_" & clsExcel.Type & "_" & clsExcel.Category & "_" & clsExcel.Classification & "_" & clsExcel.OrderBy
                    If HttpRuntime.Cache.Item(key) IsNot Nothing Then
                        ds = CType(HttpRuntime.Cache.Item(key), DataSet)
                    End If
                Case Else
                    sql = String.Empty
            End Select
            If sql.Length > 0 Then
                'ds = Globals.GetDSFromCache(sql, 60)
                ''ds = Globals.GetDS(sql)
            End If

            If ds IsNot Nothing Then

                With Me._grdExcel
                    .AllowPaging = False
                    .AllowSorting = False
                    .AutoGenerateColumns = True
                    .DataSource = ds
                    .DataBind()
                End With
                excelData = GetExcelData(_grdExcel)
                ExportToExcel(excelData, fileName)
            End If

        Catch ex As Exception
            Throw New Exception("Page_Load", ex.InnerException)
        End Try
    End Sub
    Private Function GetExcelData(ByRef gvMaster As GridView) As String
        Dim str As String = String.Empty
        Try
            DisableControls(gvMaster)
            gvMaster.AllowPaging = "False"
            gvMaster.EnableSortingAndPagingCallbacks = False
            gvMaster.AllowSorting = False
            gvMaster.HeaderStyle.ForeColor = Drawing.Color.Black
            gvMaster.HeaderRow.BackColor = Drawing.Color.Gray
            gvMaster.BorderWidth = 1
            Dim stringWrite As System.IO.StringWriter = New System.IO.StringWriter()
            Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)

            gvMaster.RenderControl(htmlWrite)

            str = (stringWrite.ToString())
            'gvMaster.DeleteRow(1)
        Catch ex As Exception
            Throw New Exception("GetExcelData", ex.InnerException)
        Finally
            GetExcelData = str
        End Try
    End Function
    Private Sub ExportToExcel(ByVal gridData As String, ByVal fName As String)
        Dim fileName As String = fName & "_" & Replace(Now.ToShortDateString, "/", "_") & "_.xls"
        Try
            Me.EnableViewState = False

            Response.Clear()

            'Response.AddHeader("content-disposition", "attachment;filename=" & fileName)

            Response.Charset = ""

            'If you want the option to open the Excel file without saving than 
            'comment out the line below 
            'Response.Cache.SetCacheability(HttpCacheability.NoCache)

            'Response.ContentType = "application/vnd.xls"
            Response.ContentType = "application/vnd.ms-excel"
            Response.ContentEncoding = System.Text.Encoding.UTF7
            'Dim stringWrite As System.IO.StringWriter = New System.IO.StringWriter()
            'Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)

            'gvMaster.RenderControl(htmlWrite)

            Response.Write(gridData.ToString())
            'Page.ClientScript.RegisterStartupScript(Page.GetType, "Close", "closeWindow();", True)
            Response.Flush()
            'Response.End()
        Catch ex As Exception
            'Response.Write("Error exporting to excel" & ex.Message)
            Server.ClearError()
            Err.Clear()
            'Throw New Exception("ExportToExcel", ex.InnerException)
        End Try
    End Sub
End Class
