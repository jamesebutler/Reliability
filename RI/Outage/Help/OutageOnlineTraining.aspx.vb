Imports RI
Imports RI.SharedFunctions
Imports Devart.Data.Oracle
Partial Class RI_Help_OnlineTraining
    Inherits RIBasePage


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner("Outage Online Training")
        Master.ShowOutageMenu()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim clsSearch As New clsOutageDemoList
        Dim CallSource As String = String.Empty
        Dim dr As OracleDataReader = Nothing
        Try
            If clsSearch IsNot Nothing Then
                dr = clsSearch.Search
                Me._gvDemoList.DataSource = dr
                Me._gvDemoList.DataBind()
                'Response.Flush() 
            End If
        Catch ex As Exception
            Throw
        Finally
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
        End Try
    End Sub
End Class
