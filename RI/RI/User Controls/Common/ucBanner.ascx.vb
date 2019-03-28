
Partial Class RI_User_Controls_Common_ucBanner
    Inherits System.Web.UI.UserControl
    Public Property DisplayBanner() As Boolean
        Get
            Return Me._pnlBanner.Visible
        End Get
        Set(ByVal value As Boolean)
            Me._pnlBanner.Visible = value
        End Set
    End Property
    Private mBannerMessage As String = String.Empty
    Public Property BannerMessage() As String
        Get
            Return mBannerMessage
        End Get
        Set(ByVal value As String)
            mBannerMessage = value
        End Set
    End Property
    Private mDisplayPopupBanner As Boolean = False
    Public Property DisplayPopupBanner() As Boolean
        Get
            Return mDisplayPopupBanner
        End Get
        Set(ByVal value As Boolean)
            mDisplayPopupBanner = value
        End Set
    End Property
    Private mUseMessageAsPageTitle As Boolean = False
    Public Property UseMessageAsPageTitle() As Boolean
        Get
            Return mUseMessageAsPageTitle
        End Get
        Set(ByVal value As Boolean)
            mUseMessageAsPageTitle = value
        End Set
    End Property
    Public Sub SetBanner()
        Dim sb As New StringBuilder
        If displayPopupBanner = False Then
            sb.Append("~/RIBanner.aspx?TextMessage=")
        Else
            sb.Append("~/RIPopupBanner.aspx?TextMessage=")
            Me._imgLogo.Visible = False
            Me._imgLogo.Width = 1
            Me._imgLogo.Height = 1
            Me._imgLogo.ImageUrl = "~/images/blackspacer.gif"
            _imgRightSpacer.Width = 1
            Me._pnlBanner.HorizontalAlign = HorizontalAlign.Center
        End If
        sb.Append(Replace(BannerMessage, ":", " "))
        Dim msg As String = LCase(BannerMessage)
        'Select Case msg
        'Case LCase(Resources.Shared.lblSwitchRI)
        '    Me._imgHeader.ImageUrl = "~/images/back_RIViewList.jpg"
        'Case LCase(Resources.Shared.lblPhysicalCauses)
        '    Me._imgHeader.ImageUrl = "~/images/back_PhysicalCauses.jpg"
        'Case LCase(Resources.Shared.lblLatentCauses)
        '    Me._imgHeader.ImageUrl = "~/images/back_LatentCauses.jpg"
        'Case LCase(Resources.Shared.lblChangeAnalysis)
        '    Me._imgHeader.ImageUrl = "~/images/back_ChangeAnalysis.jpg"
        'Case LCase(Resources.Shared.lblCommentsForEmail)
        '    Me._imgHeader.ImageUrl = "~/images/back_CommentsForEmail.jpg"
        'Case LCase(Resources.Shared.lblhelp)
        '    Me._imgHeader.ImageUrl = "~/images/back_help.jpg"
        'Case Else
        If msg.Length > 0 Then
            Me._imgHeader.ImageUrl = sb.ToString
        Else
            Me._imgHeader.ImageUrl = "~/images/blank.gif"
        End If
        'End Select

        If UseMessageAsPageTitle = True Then SetPageTitle()
    End Sub
    Private Sub SetPageTitle(Optional ByVal applicationName As String = "Reliability")
        'Page.Title = "International Paper | " & applicationName & " | " & BannerMessage
        Page.Title = applicationName & " | " & BannerMessage
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.SetBanner()
    End Sub
End Class
