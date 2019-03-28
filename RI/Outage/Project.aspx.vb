Imports System.io
Imports Devart.Data.Oracle
Imports System.Data.SqlClient
Imports System.Data

Partial Class Project
    Inherits RIBasePage
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.ShowOutageMenu()
        Master.SetBanner("View Enterprise Chart")
    End Sub
 
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Dim strURL As String
            'strURL = "http://techweb/reliability/Other Links/2007 Outage Schedule -5-31dw.mpp"

            ''You can set propeties for the new window.....

            ''dim strCommand as string = "vlarge1=window.open('" & strurl & 

            ''"','vlarge','resizable=no,scrollbars=no,status=no, toolbar=no,height=450,widt 

            ''h=575,left=150,top=100');vlarge1.focus();" & vbCrLf 

            ''Or you can use this if you don't want to size it..... 

            'Dim strCommand As String = "window.open('" & strURL & "','','');" & vbCrLf
            'Response.Write("<script>" & vbCrLf)
            'Response.Write(strCommand & vbCrLf)
            'Response.Write("<" & Chr(47) & "script>")

            ''Response.Redirect("http://techweb/reliability/Other Links/2007 Outage Schedule -5-31dw.mpp")

        Catch ex As Exception
            Throw
        End Try
    End Sub
    
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        'Response.Redirect("outage.aspx")

    End Sub
End Class
