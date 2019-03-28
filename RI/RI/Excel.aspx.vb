Imports System.io
Imports Devart.Data.Oracle
Imports System.Data.SqlClient
Imports System.Data

Partial Class Excel
    Inherits RIBasePage

    'Private Sub DisableControls(ByVal gv As Control)
    '    Dim lb As LinkButton = New LinkButton()
    '    Dim l As Literal = New Literal()
    '    Dim name As String = String.Empty

    '    Try
    '        For i As Integer = 0 To (gv.Controls.Count - 1)
    '            If i < gv.Controls.Count Then
    '                If TypeOf gv.Controls(i) Is LinkButton Then
    '                    l.Text = CType(gv.Controls(i), LinkButton).Text
    '                    gv.Controls.Remove(gv.Controls(i))
    '                    gv.Controls.AddAt(i, l)
    '                ElseIf TypeOf gv.Controls(i) Is DropDownList Then
    '                    l.Text = CType(gv.Controls(i), DropDownList).Text
    '                    gv.Controls.Remove(gv.Controls(i))
    '                    gv.Controls.AddAt(i, l)
    '                ElseIf TypeOf gv.Controls(i) Is HyperLink Then
    '                    l.Text = CType(gv.Controls(i), HyperLink).Text
    '                    gv.Controls.Remove(gv.Controls(i))
    '                    gv.Controls.AddAt(i, l)
    '                ElseIf TypeOf gv.Controls(i) Is Image Then
    '                    gv.Controls.Remove(gv.Controls(i))
    '                Else
    '                    'Trace.Write("log", gv.Controls(i).GetType.ToString)
    '                    'System.Diagnostics.Debug.WriteLine(gv.Controls(i).GetType.ToString)
    '                End If
    '                If gv.Controls(i).HasControls Then
    '                    DisableControls(gv.Controls(i))
    '                End If
    '            End If
    '        Next
    '    Catch ex As Exception
    '        Throw New Exception("DisableControls", ex.InnerException)
    '    End Try
    'End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CreateExcel()
    End Sub
    Private Sub CreateExcel()
        Dim dr As OracleDataReader = Nothing
        Dim fileName As String = "RIFile"
        Dim excelData As String = String.Empty
        Dim clsSearch As New clsViewSearch
        clsSearch = Session.Item("clsSearch")
        If clsSearch Is Nothing Then Exit Sub

        Try
            Me.EnableViewState = False
            Response.Clear()
            Response.Buffer = False
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-excel"
            Response.ContentEncoding = System.Text.Encoding.UTF7

            dr = clsSearch.Search
            Dim sbExcel As New StringBuilder
            Dim alternatingRow As Boolean = True
            Dim CurrentRow As Integer = -1
            sbExcel.Append("<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:x='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'>")
            sbExcel.Append("<head></head><body>")

            sbExcel.Append("<style>")
            sbExcel.Append(".text{mso-style-parent:style0;font-family:Arial Unicode MS, sans-serif;mso-font-charset:0;mso-number-format:'\@';border:.5pt solid black;background:silver;mso-pattern:auto none;white-space:normal;}")
            sbExcel.Append("</style>")
            If dr IsNot Nothing Then
                sbExcel.Append("<Table width='100%' cellpadding=1 cellspacing=1 border=1>")
                Do While dr.Read()
                    CurrentRow = CurrentRow + 1
                    If CurrentRow = 0 Then 'Header Row
                        sbExcel.Append("<tr bgcolor='#000000' style='color: #FFFFFF'>")
                        For i As Integer = 0 To dr.FieldCount - 1
                            sbExcel.Append(AddTableCell(dr.GetName(i).ToString()))
                        Next
                    End If
                    If alternatingRow = False Then
                        sbExcel.Append("<tr>")
                        alternatingRow = True
                    Else
                        sbExcel.Append("<tr bgcolor='#CCCCCC'>")
                    End If

                    For i As Integer = 0 To dr.FieldCount - 1                    
                        sbExcel.Append(AddTableCell(dr.GetValue(i).ToString()))
                    Next
                    sbExcel.Append("</tr>")


                    If CurrentRow Mod 100 = 0 Then
                        Response.Write(sbExcel.ToString)
                        sbExcel.Length = 0
                        'Response.Flush()
                    End If

                Loop
                'totRows = CurrentRow
                'totColumns = dr.FieldCount
                'sbExcel.Append("<tr><TD></TD><TD></TD><TD></TD><TD></TD><TD></TD><TD>=SUM(F2:F7)</TD><TD>=SUM(G2:G7)</TD></tr>")
                sbExcel.Append("</table>")
                sbExcel.Append("</body></html>")
                Response.Write(sbExcel.ToString)
                'Response.Flush()
                Response.Close()
                'Response.End()
            End If
            'Response.Flush()

            
        Catch ex As Threading.ThreadAbortException
            Server.ClearError()
        Catch ex As Exception
            Throw
        Finally
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
            clsSearch.SelectStatement = ""
            clsSearch.OrderBy = ""
            Session.Remove("clsSearch")
            Session.Add("clsSearch", clsSearch)
            Response.End()
        End Try
    End Sub
    Private Function AddTableCell(ByVal data As String) As String
        If Not IsNumeric(data) Then
            '"<style>.text { mso-number-format:\@; } </style>"

            Return "<td class='text'>" & data & "</td>"
        Else
            Return "<td>" & data & "</td>"
        End If
    End Function
   
    'Private Function GetExcelData(ByRef gvMaster As GridView) As String
    '    Dim str As String = String.Empty
    '    Try            
    '        gvMaster.AllowPaging = False
    '        gvMaster.EnableSortingAndPagingCallbacks = False
    '        gvMaster.AllowSorting = False
    '        gvMaster.HeaderStyle.ForeColor = Drawing.Color.Black
    '        gvMaster.HeaderStyle.BackColor = Drawing.Color.Gray
    '        gvMaster.BorderWidth = 1


    '        'If gvMaster.Rows.Count() <> 0 Then
    '        'End If

    '        Dim stringWrite As System.IO.StringWriter = New System.IO.StringWriter()
    '        Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)

    '        gvMaster.RenderControl(htmlWrite)

    '        str = (stringWrite.ToString())
    '        'gvMaster.DeleteRow(1)
    '    Catch ex As Exception
    '        Throw New Exception("GetExcelData", ex.InnerException)
    '    Finally
    '        GetExcelData = str
    '    End Try
    'End Function
    Private Sub ExportToExcel(ByVal fName As String)
        Dim fileName As String = fName & "_" & Replace(Now.ToShortDateString, "/", "_") & "_.xls"
        Try
            Me.EnableViewState = False

            Response.Clear()

            'Response.AddHeader("content-disposition", "attachment;filename=" & fileName)

            Response.Charset = ""

            'If you want the option to open the Excel file without saving than 
            'comment out the line below 
            'Response.Cache.SetCacheability(HttpCacheability.NoCache)

            Response.ContentType = "application/vnd.ms-excel"
            Response.ContentEncoding = System.Text.Encoding.UTF7

            'Response.Write(gridData.ToString())
            Response.Flush()
        Catch ex As Exception
            'Response.Write("Error exporting to excel" & ex.Message)
            Server.ClearError()
            Err.Clear()
            'Throw New Exception("ExportToExcel", ex.InnerException)
        End Try
    End Sub

    'Protected Sub _grdExcel_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles _grdExcel.DataBinding
    '    Dim footerRow As GridViewRow = Me._grdExcel.FooterRow()
    '    Try
    '        footerRow.Cells(8).Text = "Cost Total"
    '        footerRow.Cells(9).Text = "Financial Impact Total"
    '    Catch ex As Exception
    '        Server.ClearError()
    '        Err.Clear()
    '    End Try

    'End Sub
End Class
