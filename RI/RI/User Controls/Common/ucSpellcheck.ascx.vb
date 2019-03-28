
Partial Class RI_User_Controls_Common_ucSpellcheck
    Inherits System.Web.UI.UserControl
    Private mControlIdsToCheck As String = String.Empty
    Public Property ControlIdsToCheck() As String
        Get
            Return mControlIdsToCheck 'Me._spellIncidentDescription.ControlIdsToCheck
        End Get
        Set(ByVal value As String)
            mControlIdsToCheck = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me._btnSpell.OnClientClick = "UltimateSpellClick('" & Me._spellIncidentDescription.ClientID & "');return false;"
        If ControlIdsToCheck.Length > 0 Then
            setControlIdsToCheck()
        End If
        Me._spellIncidentDescription.Dictionary = System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper
        If System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper <> "EN-US" Then
            Me._btnSpell.Visible = False
        Else
            Me._btnSpell.Visible = True
        End If
    End Sub

    Private Sub setControlIdsToCheck()
        Dim ctl As String() = Split(ControlIdsToCheck, ",")
        Dim parentid As String = Me.ClientID.Remove(InStr(Me.ClientID, Me.ID) - 1)
        Dim newValue As String
        Dim sb As New StringBuilder
        If ctl.Length > 0 Then
            For i As Integer = 0 To ctl.Length - 1
                If sb.Length > 0 Then
                    sb.Append(",")
                End If
                sb.Append(parentid)
                sb.Append(ctl(i).Trim)
            Next
            newValue = sb.ToString
        Else
            newValue = mControlIdsToCheck
        End If
        Me._spellIncidentDescription.ControlIdsToCheck = newValue
    End Sub
End Class
