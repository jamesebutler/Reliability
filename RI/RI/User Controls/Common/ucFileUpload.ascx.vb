
Partial Class RI_User_Controls_ucFileUpload
    Inherits System.Web.UI.UserControl
    Public Property FileNameLabel() As String
        Get
            If Me._lblFileName.Text.Length = 0 Then
                Return "File Name:"
            Else
                Return Me._lblFileName.Text
            End If
        End Get

        Set(ByVal value As String)
            Me._lblFileName.Text = value
        End Set
    End Property
    Public Property FileDescLabel() As String
        Get
            If Me._lblFileDesc.Text.Length = 0 Then
                Return "Description:"
            Else
                Return Me._lblFileDesc.Text
            End If
        End Get

        Set(ByVal value As String)
            Me._lblFileDesc.Text = value
        End Set
    End Property
    Public Property UploadButtonLabel() As String
        Get
            If Me._btnUpload.Text.Length = 0 Then
                Return "Upload File:"
            Else
                Return Me._btnUpload.Text
            End If
        End Get

        Set(ByVal value As String)
            Me._btnUpload.Text = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

        End If
    End Sub
End Class
