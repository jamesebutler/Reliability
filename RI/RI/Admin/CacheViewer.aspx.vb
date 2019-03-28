
Imports System 
Imports System.Data 
Imports System.Configuration 
Imports System.Collections 
Imports System.Web 
Imports System.Web.Security 
Imports System.Web.UI 
Imports System.Web.UI.WebControls 
Imports System.Web.UI.WebControls.WebParts 
Imports System.Web.UI.HtmlControls 
Imports System.Text 

Partial Public Class RI_Admin_DisplayCache
    Inherits RIBasePage

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("Cache Viewer"))
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            GetAndDisplayCacheItems()
        Else
            'These have to be bound to fire the event. This method 
            'Bind the link buttons, but without the rest of the over head 
            CreateAndBindRemoveButtons()
        End If
    End Sub

    Private Sub GetAndDisplayCacheItems()
        phTable.Controls.Clear()
        'this stringbuilder gets used throughout the method as needed. 
        Dim sb As New StringBuilder()

        'Grab the context item, for reference so it has local scope to 
        'save having to get walk back and get the Context item 
        Dim ctx As HttpContext = HttpContext.Current



        Dim xlist As Collections.SortedList = Nothing
        Dim myCache As New SortedList
        Dim objItem As Object
        Dim strName As String = String.Empty

        For Each objItem In ctx.Cache
            strName = objItem.Key
            If myCache.Contains(strName) = False Then
                myCache.Add(strName, ctx.Cache(strName).GetType().ToString())
            End If
        Next
        Dim d As IDictionaryEnumerator = myCache.GetEnumerator 'ctx.Cache.GetEnumerator()
        While d.MoveNext()
            ' Create HTML table for display of each item in cache 
            Dim t As New HtmlTable()
            'Set General Table Properties 
            t.BorderColor = "000000"
            t.CellPadding = 10
            t.CellSpacing = 0
            t.Border = 1
            t.Width = Unit.Percentage(98).ToString

            Dim r As New HtmlTableRow()
            Dim CacheKeyTableCell As New HtmlTableCell()
            CacheKeyTableCell.VAlign = "top"

            'Clear the stringbuilder and add the CachItem Type and value 
            sb.Length = 0
            sb.Append("<B>Cache Item Type: </B>")
            sb.Append(ctx.Cache(d.Key.ToString()).[GetType]().ToString())
            sb.Append("<BR /><B>Value: </B>")
            sb.Append(Server.HtmlEncode(d.Value.ToString()))

            'Add the Item Type 
            CacheKeyTableCell.Controls.Add(New LiteralControl(sb.ToString()))
            r.Cells.Add(CacheKeyTableCell)

            Dim r2 As New HtmlTableRow
            Dim CacheValueTableCell As New HtmlTableCell()
            Select Case d.Value.ToString()
                Case "System.Data.DataSet"
                    'This is the fun code, that turns dataset tables, 
                    'into HTML Tables 
                    'Coutner to Create a label 
                    Dim tblCounter As Integer = 0
                    'Create a new dataset to be used as a copy of the cached dataset 
                    'We do this, becuase some of the values get manipulated and we 
                    'don't want to chagne the actual values. 
                    Dim ds As New DataSet()
                    'Get copy of dataset from Cache 
                    ds = DirectCast(ctx.Cache(d.Key.ToString()), DataSet).Copy()
                    'Iterate throguht the tables in the dataset 
                    For Each dt As DataTable In ds.Tables
                        'Set flag to see if the table has rows. 
                        'We won't add empty tables to the view 
                        Dim rowsProcessed As Boolean = False
                        For Each dr As DataRow In dt.Rows
                            'Found rows, set flag to true 
                            rowsProcessed = True
                            For x As Integer = 0 To dr.ItemArray.GetLength(0) - 1
                                'loop through each colomn int he DataRow and convert 
                                'any text, to HTML text. A lot of cached data, is data that 
                                'gets rendered as XML, or HTML, so this makes the data human 
                                'readable. This is the reason for working with a copy of the 
                                'dataset, and not the real cached item. 
                                'This is my sloppy way to catch nulls, and any other 
                                'conversion errors. 
                                Try
                                    Dim columnData As String = Server.HtmlEncode(dr(x).ToString())
                                    dr(x) = columnData
                                Catch
                                End Try
                            Next
                        Next
                        'Add populated tables to ValueTableCell, via 
                        'new datagrid.AutoGenerateColumns 
                        If rowsProcessed Then
                            'Add Seperation bar between Tables 
                            If tblCounter > 0 Then
                                CacheValueTableCell.Controls.Add(New LiteralControl("<HR>"))
                            End If
                            Dim dg As New DataGrid()
                            dg.CssClass = "Border"
                            dg.GridLines = GridLines.Both
                            dg.CellSpacing = 2
                            dg.AutoGenerateColumns = True
                            dg.BorderColor = Drawing.Color.Black
                            dg.BorderWidth = Unit.Pixel(2)
                            dg.HeaderStyle.CssClass = "BorderSecondary"
                            dg.HeaderStyle.Font.Bold = True
                            dg.DataSource = dt
                            dg.DataBind()
                            dg.Width = Unit.Percentage(100)
                            'Set some styles on the columns for readability 
                            For Each c As DataGridColumn In dg.Columns
                                c.ItemStyle.HorizontalAlign = HorizontalAlign.Left
                                c.ItemStyle.VerticalAlign = VerticalAlign.Top
                            Next
                            'Add a Table Counter Label 
                            sb.Length = 0
                            sb.Append("DataTable ")
                            sb.Append(tblCounter.ToString())
                            sb.Append("<BR />")
                            CacheValueTableCell.Controls.Add(New LiteralControl(sb.ToString()))
                            CacheValueTableCell.Controls.Add(dg)
                        End If
                        tblCounter += 1
                    Next
                    r2.Cells.Add(CacheValueTableCell)
                    ds = Nothing
                    Exit Select
                Case "IP.Bids.Localization.ResourceDataDictionary"
                    Dim list As IP.Bids.Localization.ResourceDataDictionary = TryCast(ctx.Cache(d.Key.ToString()), IP.Bids.Localization.ResourceDataDictionary)
                    '  ds = DirectCast(ctx.Cache(d.Key.ToString()), DataSet).Copy()
                    If list IsNot Nothing Then
                        Dim dt As New DataTable(d.Key.ToString)
                        dt.Columns.Add(New DataColumn("Resource Key"))
                        dt.Columns.Add(New DataColumn("Resource Type"))
                        dt.Columns.Add(New DataColumn("Resource Value"))
                        For Each lsd As DictionaryEntry In list
                            Dim newRow As DataRow
                            Dim data As IP.Bids.Localization.ResourceDataItem = TryCast(lsd.Value, IP.Bids.Localization.ResourceDataItem)
                            newRow = dt.NewRow
                            newRow.Item("Resource Key") = data.ResourceKey
                            newRow.Item("Resource Type") = data.ResourceType
                            newRow.Item("Resource Value") = data.ResourceValue
                            dt.Rows.Add(newRow)
                        Next
                        Dim gv As New GridView
                        dt.DefaultView.Sort = "[Resource Type], [Resource Key]"
                        gv.DataSource = dt.DefaultView
                        gv.DataBind()
                        gv.GridLines = GridLines.Both
                        gv.CellSpacing = 2
                        'Dim ls As New Web.UI.WebControls.RadioButtonList

                        'ls.DataSource = list
                        'ls.DataTextField = "Key"
                        'ls.DataValueField = "Value"
                        'ls.DataBind()
                        CacheValueTableCell.Controls.Add(New LiteralControl("<HR>"))
                        CacheValueTableCell.Controls.Add(gv)
                        r2.Cells.Add(CacheValueTableCell)
                        gv = Nothing
                    End If
                Case "System.Collections.SortedList"
                    Dim list As SortedList = TryCast(ctx.Cache(d.Key.ToString()), SortedList)
                    '  ds = DirectCast(ctx.Cache(d.Key.ToString()), DataSet).Copy()
                    If list IsNot Nothing Then
                        Dim dt As New DataTable(d.Key.ToString)
                        dt.Columns.Add(New DataColumn("Key"))
                        dt.Columns.Add(New DataColumn("Resource Type"))

                        For Each lsd As DictionaryEntry In list
                            Dim newRow As DataRow
                            newRow = dt.NewRow
                            newRow.Item("Key") = lsd.Key
                            newRow.Item("Resource Type") = lsd.Value
                            dt.Rows.Add(newRow)
                        Next
                        Dim gv As New GridView
                        gv.DataSource = dt
                        gv.DataBind()
                        gv.GridLines = GridLines.Both
                        gv.CellSpacing = 2
                        'Dim ls As New Web.UI.WebControls.RadioButtonList

                        'ls.DataSource = list
                        'ls.DataTextField = "Key"
                        'ls.DataValueField = "Value"
                        'ls.DataBind()
                        CacheValueTableCell.Controls.Add(New LiteralControl("<HR>"))
                        CacheValueTableCell.Controls.Add(gv)
                        r2.Cells.Add(CacheValueTableCell)
                        gv = Nothing
                    End If
                Case Else
                    Exit Select
            End Select

            'Add the row to the HTML table 
            t.Rows.Add(r)
            t.Rows.Add(r2)

            'Start <P> Tag 

            phTable.Controls.Add(New LiteralControl("<table width='100%' border=1 class='help'><tr><td>"))

            'Create and add Remove Link Button 
            phTable.Controls.Add(CreateRemoveLinkButton(d.Key.ToString()))

            'Add a spacer between the remove link button and the open/close javascript 
            phTable.Controls.Add(New LiteralControl("&nbsp;"))

            'Create and add the anchor tag to open/close javascript 
            Dim a As New HtmlAnchor()
            a.HRef = "Javascript:OpenOrCloseSpan('Span_" + d.Key.ToString() + "')"
            a.Controls.Add(New LiteralControl("Open/Close"))
            phTable.Controls.Add(a)

            'Create and add the Name of the Cache Item (preeceded with a space) 
            Dim CacheItemDisplayName As New System.Text.StringBuilder()
            CacheItemDisplayName.Append("&nbsp;")
            CacheItemDisplayName.Append(d.Key.ToString())
            'CacheItemDisplayName.Append("&nbsp;")
            'CacheItemDisplayName.Append(Server.HtmlEncode(d.Value.ToString()))           

            'Add the Open Span Tag and ID for javascript to open and close 
            CacheItemDisplayName.Append("<span id=""span_")
            CacheItemDisplayName.Append(d.Key.ToString())
            CacheItemDisplayName.Append(""" style=""Display: None"">")
            phTable.Controls.Add(New LiteralControl(CacheItemDisplayName.ToString()))

            'Add the data table to the placeholder 
            phTable.Controls.Add(t)
            'Add the closing Span and P tags. 
            phTable.Controls.Add(New LiteralControl("</span></td></tr></table>"))

        End While
    End Sub

    Private Function CreateRemoveLinkButton(ByVal buttonID As String) As LinkButton
        Dim RemoveLinkButton As New LinkButton()
        RemoveLinkButton.Text = "Remove"
        'Set the ID of the LinkButton to the Cache Key Name. 
        'This is going to be used in the eventhandler, to remove item from cache 
        RemoveLinkButton.ID = buttonID
        AddHandler RemoveLinkButton.Click, AddressOf RemoveLinkButton_Click
        'Wire up event 
        Return RemoveLinkButton
    End Function

    Private Sub CreateAndBindRemoveButtons()
        Dim ctx As HttpContext = HttpContext.Current
        Dim d As IDictionaryEnumerator = ctx.Cache.GetEnumerator()
        While d.MoveNext()
            phTable.Controls.Add(CreateRemoveLinkButton(d.Key.ToString()))
        End While
    End Sub

    Private Sub RemoveLinkButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim ctx As HttpContext = HttpContext.Current
        'Cast sender as LinkButton and Check if still in cache 
        If ctx.Cache(CType(sender.ID, String)) IsNot Nothing Then
            'Remove item from Cache 
            ctx.Cache.Remove(CType(sender.ID, String))
        End If
        GetAndDisplayCacheItems()
    End Sub

    Public Sub DeleteEntireCache()
        Dim objItem As Object
        Dim strName As String = String.Empty
        For Each objItem In Cache
            strName = objItem.Key
            'Comment the If..Then if you want to see ALL (System, etc.) items the cache
            'We don't want to see ASP.NET cached system items or ASP.NET Worker Processes
            If (Left(strName, 7) <> "System.") And (Left(strName, 7) <> "ISAPIWo") Then
                If Cache.Item(strName) IsNot Nothing Then
                    Cache.Remove(strName)
                End If
            End If
        Next
        GetAndDisplayCacheItems()
    End Sub
    Protected Sub _btnDeleteAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnDeleteAll.Click
        DeleteEntireCache()
    End Sub

    Protected Sub _btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnRefresh.Click
        Me.phTable.Controls.Clear()
        GetAndDisplayCacheItems()
    End Sub
End Class