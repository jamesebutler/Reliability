Imports Devart.Data.Oracle

Partial Class MOC_MOCDefaultNotification
    Inherits RIBasePage

    Dim selectedFacility As String = String.Empty
    Dim selectedBusArea As String = String.Empty
    Dim selectedLine As String = String.Empty
    Dim userProfile As RI.CurrentUserProfile = Nothing

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.ShowMOCMenu()
        Master.SetBanner("MOC Notification")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            userProfile = RI.SharedFunctions.GetUserProfile

            Dim sc As ScriptManager
            sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
            If sc IsNot Nothing Then
                Dim SiteService As New ServiceReference
                SiteService.InlineScript = False
                SiteService.Path = "~/RIMOCSharedWS.asmx"
                sc.Services.Add(SiteService)
            End If

            ScriptManager.RegisterClientScriptInclude(Me._udpLocation, _udpLocation.GetType, "MOCNotification", Page.ResolveClientUrl("~/moc/MOCNotification.js"))

            'Me._ddlFacility.Attributes.Add("onchange", String.Format("javascript:GetEndorsers('{0}','{1}','{2}','{3}','{4}','{5}');", _ddlFacility.ClientID, Me._slbNotification.ApproverL1ListID, Me._slbNotification.ApproverL2ListID, Me._slbNotification.ApproverL3ListID, Me._slbNotification.InformedListID, Me._slbNotification.AvailableListID))
            'Me._ddlBusinessUnit.Attributes.Add("onchange", String.Format("javascript:GetEndorsers('{0}','{1}','{2}','{3}','{4}','{5}');", _ddlFacility.ClientID, Me._slbNotification.ApproverL1ListID, Me._slbNotification.ApproverL2ListID, Me._slbNotification.ApproverL3ListID, Me._slbNotification.InformedListID, Me._slbNotification.AvailableListID))
            'Me._ddlArea.Attributes.Add("onchange", String.Format("javascript:GetEndorsers('{0}','{1}','{2}','{3}','{4}','{5}');", _ddlFacility.ClientID, Me._slbNotification.ApproverL1ListID, Me._slbNotification.ApproverL2ListID, Me._slbNotification.ApproverL3ListID, Me._slbNotification.InformedListID, Me._slbNotification.AvailableListID))
            'Me._ddlLineBreak.Attributes.Add("onchange", String.Format("javascript:GetEndorsers('{0}','{1}','{2}','{3}','{4}','{5}');", _ddlFacility.ClientID, Me._slbNotification.ApproverL1ListID, Me._slbNotification.ApproverL2ListID, Me._slbNotification.ApproverL3ListID, Me._slbNotification.InformedListID, Me._slbNotification.AvailableListID))

            If Page.IsPostBack Then
                'Me._cddlFacility.SelectedValue = Request.QueryString("Facility")
                'Me._cddlArea.SelectedValue = Request.QueryString("BusUnit")
                PopulateNotificationList(Me._ddlFacility.SelectedValue)

            Else
                'If _ddlFacility.SelectedValue.Length = 0 Then
                If Request.QueryString("Facility") <> "" Then
                    Me._cddlFacility.SelectedValue = Request.QueryString("Facility")
                    PopulateNotificationList(Request.QueryString("Facility"))
                Else
                    If userProfile IsNot Nothing Then
                        ' If _ddlFacility.Items.FindByValue(userProfile.DefaultFacility) IsNot Nothing Then
                        '_ddlFacility.SelectedValue = userProfile.DefaultFacility
                        'End If
                        Me._cddlFacility.SelectedValue = userProfile.DefaultFacility
                        PopulateNotificationList(userProfile.DefaultFacility)
                    End If
                End If
                'End If
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub PopulateNotificationList(ByVal SiteID As String)
        ', ByVal busunit As String, ByVal area As String, ByVal line As String)
        Try
            Dim paramCollection As New OracleParameterCollection
            Dim param As New OracleParameter
            Dim ds As System.Data.DataSet = Nothing
            Dim ds2 As System.Data.DataSet = Nothing
            Dim ds3 As System.Data.DataSet = Nothing
            Dim ds4 As System.Data.DataSet = Nothing
            Dim dr As Data.DataTableReader = Nothing

            Dim busunit As String = _ddlBusinessUnit.SelectedValue
            If busunit = "" Then
                busunit = "All"
            End If
            Dim area As String = _ddlArea.SelectedValue
            If area = "" Then
                area = "All"
            End If
            Dim line As String = _ddlLineBreak.SelectedValue
            If line = "" Then
                line = "All"
            End If

            'Get the initial list of approvers based on tblmocnotification table.  This should only show up when an MOC is created.
            'If an existing MOC, only show facility and person ddl.
            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = SiteID
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_BusUnit"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = busunit
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_area"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = area
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_line"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = line
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsInformedList"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsL1List"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsL2List"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsL3List"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            'ds3 = RI.SharedFunctions.GetDSFromPackage(paramCollection, "MOCMaint.GetBUANotificationList2", "GetBUANotificationList", 0)
            ds3 = RI.SharedFunctions.GetDSFromPackage(paramCollection, "NewMOC.GetBUANotificationList", "GetBUANotificationList", 0)

            dr = ds3.Tables(0).CreateDataReader
            Dim dr2 As Data.DataTableReader = ds3.Tables(1).CreateDataReader
            Dim dr3 As Data.DataTableReader = ds3.Tables(2).CreateDataReader
            Dim dr4 As Data.DataTableReader = ds3.Tables(3).CreateDataReader

            Me._gvInformed.DataSource = dr
            Me._gvInformed.DataBind()

            Me._gvL1Approvers.DataSource = dr2
            Me._gvL1Approvers.DataBind()

            Me._gvL2Approvers.DataSource = dr3
            Me._gvL2Approvers.DataBind()

            Me._gvL3Approvers.DataSource = dr4
            Me._gvL3Approvers.DataBind()

        Catch ex As Exception
            Throw
        End Try

    End Sub
End Class
