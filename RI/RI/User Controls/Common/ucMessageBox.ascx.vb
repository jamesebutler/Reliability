
Partial Class ucMessageBox
    Inherits System.Web.UI.UserControl

    Public Event OKClick()
    Public Event CancelClick()

    Public Enum ButtonTypes
        OK = 1
        OKCancel = 2
        YesNo = 3
    End Enum
    Private mCancelScript As String = String.Empty    
    Public Property CancelScript() As String
        Get
            Return mCancelScript
        End Get
        Set(ByVal value As String)
            mCancelScript = value
        End Set
    End Property
    Private mOKScript As String = String.Empty
    Public Property OKScript() As String
        Get
            Return mOKScript
        End Get
        Set(ByVal value As String)
            mOKScript = value
        End Set
    End Property
    Private mButtonType As ButtonTypes
    Public Property ButtonType() As ButtonTypes
        Get
            Return mButtonType
        End Get
        Set(ByVal value As ButtonTypes)
            mButtonType = value            
        End Set
    End Property
    Private mMessage As String = String.Empty
    Public Property Message() As String
        Get
            Return mMessage 'Me._divMessage.InnerHtml
        End Get
        Set(ByVal value As String)
            mMessage = value
            'Me._divMessage.InnerHtml = value
        End Set
    End Property

    Private mTitle As String = String.Empty
    Public Property Title() As String
        Get
            Return mTitle
            'Return Me._bannerTitle.BannerMessage
        End Get
        Set(ByVal value As String)
            mTitle = value 'Me._bannerTitle.BannerMessage = value
        End Set
    End Property

    Private mWidth As Integer = 400
    Public Property Width() As Integer
        Get
            Return mWidth 'Return Me._pnlMessageBox.Width.Value
        End Get
        Set(ByVal value As Integer)
            mWidth = value
            'Me._pnlMessageBox.Width = Unit.Pixel(value)
        End Set
    End Property

    Private mAllowPostback As Boolean = True
    Public Property AllowPostback() As Boolean
        Get
            Return mAllowPostback
        End Get
        Set(ByVal value As Boolean)
            mAllowPostback = value
        End Set
    End Property

    Public Sub ShowMessage()
        DisplayButtons()
        Me._divMessage.InnerHtml = Message
        _bannerTitle.BannerMessage = Title
        Me._pnlMessageBox.Width = Unit.Pixel(Width)
        _bannerTitle.SetBanner()
        Me._mpeMessage.Show()
    End Sub

    Public Sub HideMessage()
        Me._mpeMessage.Hide()
    End Sub

    Protected Sub _btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnOK.Click
        'If RI.SharedFunctions.CausedPostBack(Me._btnOK.UniqueID) Then      
        Me._mpeMessage.Hide()      
        RaiseEvent OKClick()
        'End If
    End Sub

    Protected Sub _btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnClose.Click
        ' If RI.SharedFunctions.CausedPostBack(Me._btnClose.UniqueID) Then        
        Me._mpeMessage.Hide()
        'End If
    End Sub
    Public ReadOnly Property OKClient() As Button
        Get
            Return Me._btnOK
        End Get
    End Property
    Public ReadOnly Property Cancel() As Button
        Get
            Return Me._btnClose
        End Get
    End Property
    Public ReadOnly Property OKClientID() As String
        Get
            Return Me._btnOK.ClientID
        End Get        
    End Property

    Public ReadOnly Property CancelClientID() As String
        Get
            Return Me._btnClose.ClientID
        End Get
    End Property

    Public ReadOnly Property MessageClientID() As String
        Get
            Return Me._divMessage.ClientID
        End Get
    End Property

    Public ReadOnly Property MessageTriggerClientID() As String
        Get
            Return Me._imbMessageBoxTrigger.ClientID
        End Get
    End Property

    Private Sub DisplayButtons()
        Dim IPLoc As New IP.Bids.Localization.WebLocalization
        Select Case ButtonType
            Case ButtonTypes.OK
                _mpeMessage.OkControlID = Me._btnClose.ClientID
                _mpeMessage.CancelControlID = Me._btnClose.ClientID
                Me._btnClose.Visible = True
                Me._btnOK.Visible = False
                Me._btnClose.Text = IPLoc.GetResourceValue("OK")
            Case ButtonTypes.OKCancel
                '_mpeMessage.OkControlID = Me._btnClose.ClientID
                '_mpeMessage.CancelControlID = Me._btnClose.ClientID
                If AllowPostback = True Then
                    _mpeMessage.OkControlID = Me._btnClose.ClientID
                    _mpeMessage.CancelControlID = Me._btnClose.ClientID
                Else
                    _mpeMessage.OkControlID = Me._btnOK.ClientID
                    _mpeMessage.CancelControlID = Me._btnClose.ClientID
                End If
                Me._btnClose.Visible = True
                Me._btnOK.Visible = True
                Me._btnOK.Text = IPLoc.GetResourceValue("OK")
                Me._btnClose.Text = IPLoc.GetResourceValue("Cancel")
            Case ButtonTypes.YesNo
                '_mpeMessage.OkControlID = Me._btnClose.ClientID
                '_mpeMessage.CancelControlID = Me._btnClose.ClientID
                If AllowPostback = True Then
                    _mpeMessage.OkControlID = Me._btnClose.ClientID
                    _mpeMessage.CancelControlID = Me._btnClose.ClientID
                Else
                    _mpeMessage.OkControlID = Me._btnOK.ClientID
                    _mpeMessage.CancelControlID = Me._btnClose.ClientID
                End If
                Me._btnClose.Visible = True
                Me._btnOK.Visible = True
                Me._btnOK.Text = IPLoc.GetResourceValue("Yes")
                Me._btnClose.Text = IPLoc.GetResourceValue("No")
            Case Else
                '_mpeMessage.CancelControlID = Me._btnOK.ClientID
                '_mpeMessage.OkControlID = Me._btnOK.ClientID
                Me._btnClose.Visible = False
                Me._btnOK.Visible = True
                Me._btnOK.Text = IPLoc.GetResourceValue("OK")
        End Select
        Dim returnValue As String = "returnValue" & Me.ClientID & "= "
        If CancelScript.Length > 0 Then
            Me._mpeMessage.OnCancelScript = CancelScript
        End If
        If OKScript.Length > 0 Then
            Me._mpeMessage.OnOkScript = OKScript
        End If
        '    Me._btnClose.OnClientClick = returnValue & "1;" & CancelScript
        'Else
        '    Me._btnClose.OnClientClick = returnValue & "1;"
        'End If
        'If okScript.Length > 0 Then
        '    Me._btnOK.OnClientClick = returnValue & "0;" & OKScript
        'Else
        '    Me._btnOK.OnClientClick = returnValue & "1;"
        'End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me._divMessage.InnerHtml = Message
        _bannerTitle.BannerMessage = Title
        Me._pnlMessageBox.Width = Unit.Pixel(Width)
        _bannerTitle.SetBanner()
        DisplayButtons()
        Page.ClientScript.RegisterStartupScript(Me.GetType, "", "var returnValue" & Me.ClientID & "; ", True)
    End Sub
End Class
