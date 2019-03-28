Option Explicit On
Option Strict On
Imports RI
Partial Class _Default
    Inherits RIBasePage

    Dim ViewUpdate As clsViewUpdate

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        RI.SharedFunctions.ResponseRedirect("~/ri/ViewUpdateSearch.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
    End Sub

   
End Class
