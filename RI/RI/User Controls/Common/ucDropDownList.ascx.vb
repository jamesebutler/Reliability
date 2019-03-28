
Partial Class RI_User_Controls_Common_ucDropDownList
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me._btnDropDown.BackColor = Drawing.Color.White
        Me._btnDropDown.ForeColor = Drawing.Color.Black
        Me._btnDropDown.Text = "v"
        Me._btnDropDown.OnClientClick = "return false"
    End Sub
End Class
