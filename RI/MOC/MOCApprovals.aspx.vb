Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Devart.Data.Oracle
Imports System.Web.Mail

Partial Class RI_MOCAApprovals
    Inherits RIBasePage

    Dim userProfile As RI.CurrentUserProfile = Nothing

    ''' <summary>
    ''' Stores the selected MOCNumber in Session
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns the MOCNumber</returns>
    ''' <remarks></remarks>
    Public Property MOCNumber() As String
        Get
            If mMOCNumber.Length = 0 Then mMOCNumber = Request.QueryString("MOCNumber")
            Return mMOCNumber
        End Get
        Set(ByVal value As String)
            mMOCNumber = value
        End Set
    End Property
    Private mMOCNumber As String = String.Empty
    Private msiteID As String = String.Empty
    Public Property siteID() As String
        Get
            If msiteID.Length = 0 Then msiteID = Request.QueryString("Siteid")
            Return msiteID
        End Get
        Set(ByVal value As String)
            msiteID = value
        End Set
    End Property
    Private dtPersonList As DataTable = Nothing

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner("Approvals", False)
        'Master.ShowPopupMenu()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Response.Redirect("~/MOC/EnterMOC.aspx?MOCNumber=" & MOCNumber)
        userProfile = RI.SharedFunctions.GetUserProfile
        Dim sc As ScriptManager
        sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
        If sc IsNot Nothing Then
            Dim loService As New ServiceReference
            loService.InlineScript = False
            loService.Path = "~/RIMOCSharedWS.asmx"
            sc.Services.Add(loService)
        End If
        If Not Page.IsPostBack Then
            PopulateNotificationList(3369)
        End If
        _ddlFacility.Attributes.Add("onchange", "this.blur;GetEmployee('" & _ddlFacility.ClientID & "','" & Me._lbAllFields.ID & "');")
        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "MOCSwapListBox") Then Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "MOCSwapListBox", Page.ResolveClientUrl("~/ri/User Controls/Common/MOCSwapListBox.js"))
        'Dim hiddenFields As String = "','" & Me._hdfAllFields.ClientID.ToString & "','" & Me._hdSelectedFields.ClientID.ToString & "','" & Me._hdSelectedSecondaryFields.ClientID.ToString & "','" & Me._hdSelectedThirdFields.ClientID.ToString & "','" & Me._hdSelectedInformed.ClientID.ToString

        'If Not Page.ClientScript.IsOnSubmitStatementRegistered(Page.GetType, "MOCSwapListPost") Then Page.ClientScript.RegisterOnSubmitStatement(Page.GetType, "MOCSwapListPost", "MOCSwapList.selectAll('" & Me._lbAllFields.ClientID.ToString & "',  '" & Me._lbApproversL1.ClientID.ToString & "','" & Me._lbApproversL2.ClientID.ToString & "','" & Me._lbApproversL3.ClientID.ToString & "','" & Me._lbInformed.ClientID.ToString & hiddenFields & "');")
        'Me._btnMoveSelected.OnClientClick = BuildMoveDualListJS(_lbAllFields, _lbApproversL1, _lbApproversL2, _lbApproversL3, _lbInformed, False, False) '"MOCSwapList.moveDualList( this.form." & Me._lbAllFields.ClientID.ToString & ",  this.form." & Me._lbApproversL1.ClientID.ToString & ", false,false );return false;"
        'Me._btnMoveAll.OnClientClick = BuildMoveDualListJS(_lbAllFields, _lbApproversL1, _lbApproversL2, _lbApproversL3, _lbInformed, True, False) '"MOCSwapList.moveDualList( this.form." & Me._lbAllFields.ClientID.ToString & ",  this.form." & Me._lbApproversL1.ClientID.ToString & ", true ,false);return false;"
        'Me._btnRemoveAll.OnClientClick = BuildMoveDualListJS(_lbAllFields, _lbApproversL1, _lbApproversL2, _lbApproversL3, _lbInformed, True, False, True) '"MOCSwapList.moveDualList( this.form." & Me._lbApproversL1.ClientID.ToString & ",  this.form." & Me._lbAllFields.ClientID.ToString & ", true,true );return false;"
        'Me._btnRemoveSelected.OnClientClick = BuildMoveDualListJS(_lbAllFields, _lbApproversL1, _lbApproversL2, _lbApproversL3, _lbInformed, False, False, True) '"MOCSwapList.moveDualList( this.form." & Me._lbApproversL1.ClientID.ToString & ",  this.form." & Me._lbAllFields.ClientID.ToString & ", false,true );return false;"

        'Me._lbAllFields.Attributes.Add("onDblClick", BuildMoveDualListJS(_lbAllFields, _lbApproversL1, _lbApproversL2, _lbApproversL3, _lbInformed, False, False))
        Me._lbApproversL1.Attributes.Add("onDblClick", "MOCSwapList.moveDualList( " & _lbApproversL1.ClientID.ToString & ", " & Me._lbAllFields.ClientID.ToString & ", false ,true);return false;")
        Me._lbApproversL2.Attributes.Add("onDblClick", "MOCSwapList.moveDualList( this.form." & _lbApproversL2.ClientID.ToString & ",  this.form." & Me._lbAllFields.ClientID.ToString & ", false ,true);return false;")
        Me._lbApproversL3.Attributes.Add("onDblClick", "MOCSwapList.moveDualList( this.form." & _lbApproversL3.ClientID.ToString & ",  this.form." & Me._lbAllFields.ClientID.ToString & ", false ,true);return false;")
        Me._lbInformed.Attributes.Add("onDblClick", "MOCSwapList.moveDualList( this.form." & _lbInformed.ClientID.ToString & ",  this.form." & Me._lbAllFields.ClientID.ToString & ", false ,true);return false;")
        Me._lbApproversL1.Attributes.Add("onFocus", "this.form." & Me._rbL1Approvers.ClientID & ".checked=true;this.className='Border';this.form." & Me._lbApproversL2.ClientID & ".className='';this.form." & Me._lbInformed.ClientID & ".className='';this.form." & Me._lbApproversL3.ClientID & ".className='';")
        Me._lbApproversL2.Attributes.Add("onFocus", "this.form." & Me._rbL2Approvers.ClientID & ".checked=true;this.className='Border';this.form." & Me._lbApproversL1.ClientID & ".className='';this.form." & Me._lbInformed.ClientID & ".className='';this.form." & Me._lbApproversL3.ClientID & ".className='';")
        Me._lbApproversL3.Attributes.Add("onFocus", "this.form." & Me._rbL3Approvers.ClientID & ".checked=true;this.className='Border';this.form." & Me._lbApproversL1.ClientID & ".className='';this.form." & Me._lbInformed.ClientID & ".className='';this.form." & Me._lbApproversL2.ClientID & ".className='';")
        Me._lbInformed.Attributes.Add("onFocus", "this.form." & Me._rbInformed.ClientID & ".checked=true;this.className='Border';this.form." & Me._lbApproversL1.ClientID & ".className='';this.form." & Me._lbApproversL2.ClientID & ".className='';this.form." & Me._lbApproversL3.ClientID & ".className='';")
        Me._rbL1Approvers.Attributes.Add("onClick", "this.form." & Me._rbL1Approvers.ClientID & ".checked=true;this.form." & Me._lbApproversL1.ClientID & ".className='Border';this.form." & Me._lbApproversL2.ClientID & ".className='';this.form." & Me._lbInformed.ClientID & ".className='';this.form." & Me._lbApproversL3.ClientID & ".className=''")
        Me._rbL2Approvers.Attributes.Add("onClick", "this.form." & Me._rbL2Approvers.ClientID & ".checked=true;this.form." & Me._lbApproversL2.ClientID & ".className='Border';this.form." & Me._lbApproversL1.ClientID & ".className='';this.form." & Me._lbInformed.ClientID & ".className='';this.form." & Me._lbApproversL3.ClientID & ".className=''")
        Me._rbL3Approvers.Attributes.Add("onClick", "this.form." & Me._rbL3Approvers.ClientID & ".checked=true;this.form." & Me._lbApproversL3.ClientID & ".className='Border';this.form." & Me._lbApproversL1.ClientID & ".className='';this.form." & Me._lbInformed.ClientID & ".className='';this.form." & Me._lbApproversL2.ClientID & ".className=''")
        Me._rbInformed.Attributes.Add("onClick", "this.form." & Me._rbInformed.ClientID & ".checked=true;this.form." & Me._lbInformed.ClientID & ".className='Border';this.form." & Me._lbApproversL1.ClientID & ".className='';this.form." & Me._lbApproversL2.ClientID & ".className='';this.form." & Me._lbApproversL3.ClientID & ".className=''")


        If Request("__EVENTARGUMENT") IsNot Nothing AndAlso Request("__EVENTARGUMENT") = "move" Then
            Dim value As String = _lbAllFields.SelectedValue
            Dim ary As New ArrayList
            If IsNumeric(value) = False Then

                Dim idx As Integer = _lbAllFields.SelectedIndex
                Dim item As ListItem = _lbAllFields.SelectedItem

                _lbAllFields.Items.Remove(item)
                _lbApproversL1.SelectedIndex = -1
                _lbApproversL1.Items.Add(item)
            Else
                For i As Integer = 0 To _lbAllFields.Items.Count - 1
                    Dim chkvalue As String = _lbAllFields.Items(i).Value
                    Dim chktext As String = _lbAllFields.Items(i).Text
                    Dim item As ListItem = _lbAllFields.Items(i)

                    If chkvalue = value Then
                        ary.Add(item)
                        _lbApproversL1.Items.Add(item)
                        '_lbAllFields.Items.Remove(item)
                    End If
                Next
                For j As Integer = 0 To ary.Count - 1
                    _lbAllFields.Items.Remove(ary(j))
                Next
            End If

        End If
        Me._lbAllFields.Attributes.Add("ondblclick", ClientScript.GetPostBackEventReference(Me._lbApproversL1, "move"))

    End Sub

    Private Sub PopulateNotificationList(ByVal mocNumber As Integer)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ds2 As System.Data.DataSet = Nothing
        Dim ds3 As System.Data.DataSet = Nothing
        Dim ds4 As System.Data.DataSet = Nothing
        Dim sqlTO As String = String.Empty
        Dim sqlE As String = String.Empty

        'Get Facility List and set to current Facility
        param = New OracleParameter
        param.ParameterName = "rsFacility"
        param.OracleDbType = OracleDbType.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)

        Dim key As String = "MOC.FacilityList"
        ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.FacilityList", key, 24)

        With _ddlFacility
            .DataSource = ds.Tables(0)
            .DataTextField = ds.Tables(0).Columns("SiteName").ColumnName.ToString
            .DataValueField = ds.Tables(0).Columns("SiteID").ColumnName.ToString
            .DataBind()
            .Items.Insert(0, "")

            .SelectedValue = "RW"
        End With
        
        Dim dr As Data.DataTableReader

        param = New OracleParameter
        paramCollection = New OracleParameterCollection

        Dim values As New Generic.List(Of String)
        Dim roleDescription As String = String.Empty
        Dim ddlList As New Collections.Generic.List(Of ListItem)
        Dim currentUserMode As Integer = 0

        param = New OracleParameter
        param.ParameterName = "in_plantcode"
        param.OracleDbType = OracleDbType.VarChar
        param.Value = "RW"
        param.Direction = Data.ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsResponsibleList"
        param.OracleDbType = OracleDbType.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)

        ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "NewMOC.GetResponsibleList", "MTTResponsible", 0)

        'Dim dr As Data.DataTableReader = ds.CreateDataReader
        dr = ds.CreateDataReader

        'Build the dropdownlist values
        If dr IsNot Nothing Then
            Do While dr.Read
                Dim spaceChar As String = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"

                    With Me._lbAllFields
                    If dr.Item("RoleDescription") <> roleDescription Then

                        'No Roleseqid indicates individual
                        Dim roleItem As New ListItem
                        roleDescription = dr.Item("RoleDescription")
                        roleItem.Text = dr.Item("RoleDescription").ToUpper
                        If dr.Item("RoleSeqID") IsNot DBNull.Value Then
                            roleItem.Value = dr.Item("RoleSeqID")
                        End If

                        If _lbAllFields.Items.Count > 0 Then
                            Dim blankItem As New ListItem
                            With blankItem
                                .Attributes.Add("disabled", "true")
                                .Text = ""
                                .Value = -1
                            End With
                            _lbAllFields.Items.Add(blankItem)
                        End If

                        If roleDescription.Length > 0 Then
                            roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:black; font-size:Larger;")
                            _lbAllFields.Items.Add(roleItem)
                        Else
                            roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:black;")
                            'roleItem.Attributes.Add("disabled", "true")
                            _lbAllFields.Items.Add(roleItem)
                        End If

                    End If

                    Dim useritem As New ListItem
                    With useritem
                        .Text = Server.HtmlDecode(spaceChar & dr.Item("Name"))
                        '.Value = 42
                        If dr.Item("RoleSeqID") IsNot DBNull.Value Then
                            .Value = dr.Item("RoleSeqID")
                        Else
                            .Value = dr.Item("Username")
                        End If
                    End With

                    _lbAllFields.Items.Add(useritem)

                End With

            Loop
        End If

        
        'If Request.QueryString("mocnumber") Is Nothing Then
        '    paramCollection.Clear()

        '    'Get the initial list of approvers based on tblmocnotification table.  This should only show up when an MOC is created.
        '    'If an existing MOC, only show facility and person ddl.
        '    param = New OracleParameter
        '    param.ParameterName = "in_Siteid"
        '    param.OracleDbType = OracleDbType.VarChar
        '    param.Value = "PN"
        '    param.Direction = Data.ParameterDirection.Input
        '    paramCollection.Add(param)

        '    param = New OracleParameter
        '    param.ParameterName = "in_BusUnitArea"
        '    param.OracleDbType = OracleDbType.VarChar
        '    param.Value = Me._ddlBusinessUnit.SelectedValue
        '    param.Direction = Data.ParameterDirection.Input
        '    paramCollection.Add(param)

        '    param = New OracleParameter
        '    param.ParameterName = "in_Line"
        '    param.OracleDbType = OracleDbType.VarChar
        '    param.Value = Me._ddlLineBreak.SelectedValue
        '    param.Direction = Data.ParameterDirection.Input
        '    paramCollection.Add(param)

        '    param = New OracleParameter
        '    param.ParameterName = "in_Class"
        '    param.OracleDbType = OracleDbType.VarChar
        '    param.Value = Me._MOCClass.Classification
        '    param.Direction = Data.ParameterDirection.Input
        '    paramCollection.Add(param)

        '    param = New OracleParameter
        '    param.ParameterName = "rsInformedList"
        '    param.OracleDbType = OracleDbType.Cursor
        '    param.Direction = Data.ParameterDirection.Output
        '    paramCollection.Add(param)

        '    param = New OracleParameter
        '    param.ParameterName = "rsL1List"
        '    param.OracleDbType = OracleDbType.Cursor
        '    param.Direction = Data.ParameterDirection.Output
        '    paramCollection.Add(param)

        '    param = New OracleParameter
        '    param.ParameterName = "rsL2List"
        '    param.OracleDbType = OracleDbType.Cursor
        '    param.Direction = Data.ParameterDirection.Output
        '    paramCollection.Add(param)

        '    param = New OracleParameter
        '    param.ParameterName = "rsL3List"
        '    param.OracleDbType = OracleDbType.Cursor
        '    param.Direction = Data.ParameterDirection.Output
        '    paramCollection.Add(param)

        '    ds3 = RI.SharedFunctions.GetDSFromPackage(paramCollection, "NewMOC.GetDefaultNotificationList", "GetDefaultNotificationList_" & mocNumber, 0)
        'End If
        'ds3 = RI.SharedFunctions.GetDSFromPackage(paramCollection, "NewMOC.GetBUANotificationList", "GetBUANotificationList_" & mocNumber, 0)
        'ds3 = RI.SharedFunctions.GetOracleDataSet(sqlE)

        ' ds4 = RI.SharedFunctions.GetOracleDataSet(sqlTO)

        'ds2 = RI.SharedFunctions.GetOracleDataSet(sqlAll)

        
    End Sub

    
End Class
