
Partial Class RI_Admin_Cache
    Inherits System.Web.UI.Page

    Function GetCacheItems() As SortedList
        Dim objItem As Object
        Dim strName As String

        'Dim myCacheHashTable As New Hashtable()
        Dim myCache As New SortedList

        For Each objItem In Cache
            strName = objItem.Key
            'Comment the If..Then if you want to see ALL (System, etc.) items the cache
            'We don't want to see ASP.NET cached system items or ASP.NET Worker Processes
            'If (Left(strName, 7) <> "System.") And (Left(strName, 7) <> "ISAPIWo") Then
            'If myCacheHashTable.ContainsKey(strName) = False Then
            '    myCacheHashTable.Add(strName, Cache(strName).GetType().ToString())
            'End If
            If myCache.Contains(strName) = False Then
                myCache.Add(strName, Cache(strName).GetType().ToString())
            End If
            'End If
        Next
        myCache.Add("Cache.EffectivePrivateBytesLimit", Cache.EffectivePrivateBytesLimit)
        GetCacheItems = myCache
    End Function

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner("Cache Viewer")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            DisplayCache
        End If
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
        DisplayCache()
    End Sub

    Protected Sub _grdCache2_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles _grdCache2.RowDeleting
        Try
            Dim myCacheItemNameLabel As String
            myCacheItemNameLabel = Me._grdCache2.Rows(e.RowIndex).Cells(1).Text

            Dim myCacheItemName As String
            myCacheItemName = myCacheItemNameLabel
            If (Not IsNothing(Cache(myCacheItemName))) Then
                Cache.Remove(myCacheItemName)
            End If

        Catch myException As Exception
            RemoveMessage.Text = "The item was not found in the cache or there was an error while removing the cached item."
        End Try

        'Typically you will want to reload the page after
        'readding an item. This ensure that we don't
        'remove the item again each time we load the page
        'Response.Redirect("~/admin/cacheviewer.aspx", True)


    End Sub
    Public Sub DisplayCache()
        Me._grdCache2.DataSource = GetCacheItems()
        Me._grdCache2.DataBind()
    End Sub
    Protected Sub _btnDeleteAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnDeleteAll.Click
        DeleteEntireCache()
    End Sub

    Protected Sub _btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnRefresh.Click
        DisplayCache()
    End Sub
End Class
