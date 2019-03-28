
Partial Class RI_User_Controls_ucTranslatel
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        Me.src.Items.Clear()
        Me.dst.Items.Clear()
        Dim RILocalization As New IP.Bids.Localization.WebLocalization
        Dim languageList As Hashtable = RILocalization.ApplicationLocaleList
        Dim currentLocale As String = Left(RILocalization.CurrentLocale, 2).ToLower
        For Each dic As DictionaryEntry In languageList
            Dim selected As Boolean = False
            src.Items.Add(New ListItem(dic.Value, Left(dic.Key, 2).ToLower))
            dst.Items.Add(New ListItem(dic.Value, Left(dic.Key, 2).ToLower))
        Next

        If src.Items.FindByValue(currentLocale) IsNot Nothing Then
            src.Items.FindByValue(currentLocale).Selected = True
        End If
        dst.SelectedIndex = -1
    End Sub
End Class
