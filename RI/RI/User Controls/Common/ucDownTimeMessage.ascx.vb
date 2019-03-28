Imports Devart.Data.Oracle
Partial Class ucDownTimeMessage
    Inherits System.Web.UI.UserControl

    Public Enum DowntimeModes
        Preview = 1
        Automatic = 2
    End Enum
    Private mDowntimeMode As DowntimeModes = DowntimeModes.Automatic
    Public Property DowntimeMode() As DowntimeModes
        Get
            Return mDowntimeMode
        End Get
        Set(ByVal value As DowntimeModes)
            mDowntimeMode = value
        End Set
    End Property
    Private mMessageBox As String
    Public Property messageBox() As String
        Get
            Return mMessageBox
        End Get
        Set(ByVal value As String)
            mMessageBox = value
        End Set
    End Property

    Private mStartDateTime As String
    Public Property StartDateTime() As String
        Get
            Return mStartDateTime
        End Get
        Set(ByVal value As String)
            mStartDateTime = value
        End Set
    End Property

    Private mEndDateTime As String
    Public Property EndDateTime() As String
        Get
            Return mEndDateTime
        End Get
        Set(ByVal value As String)
            mEndDateTime = value
        End Set
    End Property

    Private mShowMessage As Integer
    Public Property ShowMessage() As Integer
        Get
            Return mShowMessage
        End Get
        Set(ByVal value As Integer)
            mShowMessage = value
        End Set
    End Property
    Private Sub LoadDowntimeData()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim dr As OracleDataReader = Nothing

        Try
            param = New OracleParameter
            param.ParameterName = "rsDowntimeMessage"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "Reladmin.RI.GetDowntimeMessage")
            If dr IsNot Nothing Then
                If dr.HasRows Then
                    dr.Read()
                    messageBox = RI.SharedFunctions.DataClean(dr.Item("Message"))
                    Me.StartDateTime = RI.SharedFunctions.DataClean(dr.Item("MessageStartDate"), Now)
                    Me.EndDateTime = RI.SharedFunctions.DataClean(dr.Item("MessageEndDate"), Now)
                    Me.ShowMessage = RI.SharedFunctions.DataClean(dr.Item("ShowMessage"), 1)
                End If
            End If


        Catch ex As Exception
            Throw
        Finally
            If dr IsNot Nothing Then
                dr.Close()
            End If

        End Try
    End Sub
    Private Sub DisplayMessage()
        Dim sb As New StringBuilder
        Dim messageJS As String
        sb.Append("dropinboxv2.innerHTML = buildMsg('" & Me.messageBox & "',false);")
        sb.Append("displaymsg('{0}','{1}','{2}','{3}',false);")
        messageJS = String.Format(sb.ToString, "RI Website", Me.messageBox, CreateJSDate(Me.StartDateTime), CreateJSDate(Me.EndDateTime))
        Page.ClientScript.RegisterStartupScript(Me.Page.GetType, "downtimeMessage", messageJS, True)
    End Sub
    Private Function CreateJSDate(ByVal dt As Date) As String
        'new Date(2008,1,4,18,00);
        Dim sb As New StringBuilder
        Dim newDate As String
        sb.Append("new Date({0},{1},{2},{3},{4})")
        newDate = String.Format(sb.ToString, dt.Year.ToString, dt.Month - 1, dt.Day.ToString, dt.Hour, dt.Minute)
        Return newDate
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If DowntimeMode = DowntimeModes.Automatic Then
            LoadDowntimeData()
            If ShowMessage = 1 Then
                DisplayMessage()
            End If
        End If
    End Sub
End Class
