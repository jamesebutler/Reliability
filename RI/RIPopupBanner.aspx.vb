Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

Partial Class RIPopupBanner
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Load the Image to be written on.
        Dim textMessage As String = String.Empty
        Dim ft As Font = New Font("Arial", 14, FontStyle.Bold)
        Dim bitMapImage As Bitmap = New System.Drawing.Bitmap(Server.MapPath("Images\Reliability_back_popup.jpg"))

        If Request.QueryString("textMessage") IsNot Nothing Then
            textMessage = Request.QueryString("textMessage")
        Else
            textMessage = RI.SharedFunctions.LocalizeValue("ReliabilityReporting")
        End If
        DisplayImageFromText(textMessage, Brushes.White, 14, ft, bitMapImage)

    End Sub
    Public Sub DisplayImageFromText(ByVal msg As String, ByVal brush As System.Drawing.Brush, ByVal margins As Integer, ByVal fon As Font, ByVal bitMapImage As Bitmap)
        Try
            Dim graphicImage As Graphics = Graphics.FromImage(bitMapImage)
            Dim imgWidth As Integer = 0
            Dim imgHeight As Integer = 0

            If bitMapImage IsNot Nothing Then
                imgWidth = bitMapImage.Width
                imgHeight = bitMapImage.Height
            End If

            msg = RI.SharedFunctions.AdjustTextForDisplay(msg, imgWidth - 20 - margins, fon)

            'Smooth graphics is nice.
            Dim objSF As New StringFormat()
            objSF.Alignment = StringAlignment.Center
            objSF.LineAlignment = StringAlignment.Center
            objSF.Trimming = StringTrimming.Character
            graphicImage.SmoothingMode = SmoothingMode.AntiAlias
            graphicImage.DrawString(msg, fon, Brushes.White, New Rectangle(0, 0, imgWidth, imgHeight), objSF)
            Response.ContentType = "image/jpeg"
            bitMapImage.Save(Response.OutputStream, ImageFormat.Jpeg)

            graphicImage.Dispose()
            bitMapImage.Dispose()
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
