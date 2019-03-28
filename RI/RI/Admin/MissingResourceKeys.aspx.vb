Imports System.Data
Partial Class RI_Admin_MissingResourceKeys
    Inherits RIBasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.ClientScript.IsClientScriptIncludeRegistered("Master") Then
            Page.ClientScript.RegisterClientScriptInclude("Master", Page.ResolveClientUrl("~/MasterPage.js"))
        End If
        DisplayMissingKeys()
    End Sub
    Private Function GetMissingKeyData() As DataTable
        Dim riResources As New IP.Bids.Localization.WebLocalization
        If riResources Is Nothing Then
            Return Nothing
            Exit Function
        End If
        Dim englishResources As New IP.Bids.Localization.WebLocalization("EN-US", riResources.ApplicationId)
        If englishResources Is Nothing Then
            Return Nothing
            Exit Function
        End If

        Dim dt As New DataTable("MissingKeys")
        dt.Columns.Add(New DataColumn("Key"))
        dt.Columns.Add(New DataColumn("Resource Type"))
        dt.Columns.Add(New DataColumn("English Value"))
        Dim list As SortedList = riResources.GetMissingResourceKeys
        For Each lsd As DictionaryEntry In list
            Dim newRow As DataRow
            newRow = dt.NewRow
            newRow.Item("Key") = lsd.Key
            newRow.Item("Resource Type") = lsd.Value
            newRow.Item("English Value") = englishResources.GetResourceValue(lsd.Key, False, lsd.Value)
            dt.Rows.Add(newRow)
        Next
        Return dt
        englishResources = Nothing
        riResources = Nothing
    End Function
    Private Sub DisplayMissingKeys()
      
        _gvMissingKeys.DataSource = GetMissingKeyData()
        _gvMissingKeys.DataBind()
        _gvMissingKeys.GridLines = GridLines.Both
        _gvMissingKeys.CellSpacing = 2
    End Sub

    Protected Sub _displayExcel_DisplayExcel_Click() Handles _displayExcel.DisplayExcel_Click
        Me._displayExcel.DisplayExcel(GetMissingKeyData.CreateDataReader)
    End Sub
End Class
