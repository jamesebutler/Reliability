Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Devart.Data.Oracle
Imports System.Web.Mail

Partial Class RI_Outage
    Inherits RIBasePage

    Dim userProfile As RI.CurrentUserProfile = Nothing
    Dim currentCritique As clsCritique
    Public Property OutageNumber() As String
        Get
            If mOutageNumber.Length = 0 Then mOutageNumber = Request.QueryString("OutageNumber")
            Return mOutageNumber
        End Get
        Set(ByVal value As String)
            mOutageNumber = value
        End Set
    End Property
    Private mOutageNumber As String = String.Empty

   

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Master.SetBanner(Master.RIRESOURCES.GetResourceValue("Critique", True))
        Master.SetBanner("Critique")
        Master.ShowPopupMenu()
        'These parameters will remove the Print button - Master.ShowPopupMenu(Nothing, 0, True)
        'Master.HideMenu()
        currentCritique = New clsCritique(OutageNumber)


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        userProfile = RI.SharedFunctions.GetUserProfile

        'If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "OutageScope") Then
        '    Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "OutageScope", Page.ResolveClientUrl("~/outage/OutageScope.js"))
        'End If

        Dim sc As ScriptManager
        sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
        If sc IsNot Nothing Then
            Dim loService As New ServiceReference
            loService.InlineScript = True
            loService.Path = "~/CascadingLists.asmx" '"~/outage/OutageCascadingLists.asmx"
            sc.Services.Add(loService)
        End If

        ScriptManager.RegisterStartupScript(Page, Page.GetType, "PerformanceDateTimePicker", "$('.PerformanceDateTimePicker').datetimepicker();", True)
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "SortGrid_" & _gvcritique.ClientID, String.Format(System.Globalization.CultureInfo.CurrentCulture, "CritiqueTableSorter('{0}');", _gvcritique.ClientID), True)
        '_btnAdd.OnClientClick = "Javascript:ScrollControlInView('" & Me._tbNewDesc.ClientID & "');"

        If Not Page.IsPostBack Then
            If currentCritique Is Nothing Then currentCritique = New clsCritique(OutageNumber)
            GetCritique()
            Me._cddlNewCritiqueSubCategory.ContextKey = OutageNumber
            Dim urlPath As String
            If Request.UserHostAddress = "127.0.0.1" Or Request.UserHostAddress = "http://s29edev13/riajax" Then
                urlPath = "http://ridev.ipaper.com/CEReporting/"
            Else
                urlPath = "../../CEReporting/"
            End If

            Dim reportURL As String = String.Format(urlPath & "CrystalReportDisplay.aspx?Report=OutageCritique&OutageNumber={0}&Localename=" & Master.RIRESOURCES.CurrentLocale, currentCritique.OutageNumber)
            Me._btnReport.OnClientClick = Master.GetPopupWindowJS(reportURL, "OutageCritique", 600, 300, True, True, True) & ";return false;"

        End If

    End Sub

    Private Sub GetCritique()

        Try
            'If Not currentCritique.CritiqueItemDT Is Nothing Then
            '    If currentCritique.CritiqueItemDT.IsClosed = True Then
            '        currentCritique = New clsCritique(OutageNumber)
            '    End If
            'End If
            If currentCritique.CritiqueItemDT IsNot Nothing Then
                _gvcritique.DataSource = currentCritique.CritiqueItemDT
                _gvcritique.DataBind()
                If _gvcritique.Rows.Count > 0 Then
                    _gvcritique.HeaderRow.TableSection = TableRowSection.TableHeader
                End If
            End If

            If currentCritique.CritiquePerformanceDT IsNot Nothing Then
                _gvcritiqueperformance.DataSource = currentCritique.CritiquePerformanceDT
                _gvcritiqueperformance.DataBind()
            End If

            If currentCritique.CritiqueWODT IsNot Nothing And currentCritique.CritiqueWODT.HasRows Then
                _gvcritiqueWO.DataSource = currentCritique.CritiqueWODT
                _gvcritiqueWO.DataBind()
            Else
                CreateWORecords()
            End If

            If currentCritique.CritiqueQADT IsNot Nothing And currentCritique.CritiqueQADT.HasRows Then
                _gvcritiqueQA.DataSource = currentCritique.CritiqueQADT
                _gvcritiqueQA.DataBind()
            Else
                CreateQARecords()
                Response.Redirect(Page.AppRelativeVirtualPath & "?OutageNumber=" & currentCritique.OutageNumber, True)
            End If

            Me._lblOutageTitle.Text = currentCritique.OutageTitle
            Me._lblOutageNumber.Text = " (" & currentCritique.OutageNumber & ") "
            Me._tbActualCost.Text = currentCritique.ActualCost.ToString("C")
            Me._tbPlannedCost.Text = currentCritique.Cost.ToString("C")
            'CostVariance = ActualCost - Cost
            Me._tbCostVariance.Text = currentCritique.CostVariance.ToString("C")


            Populatebusunit()

            PopulateBusunitAreaLine()
            ' RI.SharedFunctions.BindList(Me._ddlNewBusUnit, currentCritique.mBusUnit, False, True)

            If currentCritique.CritiqueAvailDT IsNot Nothing Then
                _gvcritiqueavailability.DataSource = currentCritique.CritiqueAvailDT
                _gvcritiqueavailability.DataBind()
            End If

        Catch ex As Exception
            Throw
        Finally

        End Try

    End Sub
   
    Protected Sub _btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnSave.Click, _btnSave1.Click, _btnAdd.Click, _btnAddAvail.Click, _btnAddPerf.Click

        UpdateCritiqueAvailability()
        UpdateCritiquePerformance()
        UpdateCritiqueWO()
        UpdateCritiqueQA()
        UpdateOutageCost()
        UpdateCritiqueItem()
        GetCritique()


    End Sub
    Protected Sub _btnSaveClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnSaveClose.Click, _btnSaveClose1.Click

        UpdateCritiqueAvailability()
        UpdateCritiquePerformance()
        UpdateCritiqueWO()
        UpdateCritiqueQA()
        UpdateOutageCost()
        UpdateCritiqueItem(False)

        'Response.Write("<script language='javascript'> { try{window.opener.location.reload(true);}catch(err){}window.close(); }</script>")
        'Response.Write("<script language='javascript'> { try{window.opener.updateItemCounts();}catch(err){}window.close(); }</script>")
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "windowclose", "window.close();", True)
        'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "windowclose", "window.close();", True)
        'Response.Write("<script language='javascript'> { window.close(); }</script>")
        'Response.Write("<script language='javascript'> { try{window.close();}catch(err){} }</script>")


    End Sub
    Protected Sub UpdateOutageCost(Optional ByVal refreshPage As Boolean = True)
        Dim i As Integer = 0
        ' Dim intAvailSeqId, intRow As Integer
        Dim status As String

        Try
            With currentCritique
                .UserName = userProfile.Username
                .Cost = Me._tbPlannedCost.Text
                .ActualCost = Me._tbActualCost.Text
                status = .SaveCritiqueCost(OutageNumber)
            End With
            
        Catch
            Throw
        End Try
    End Sub
    Protected Sub UpdateCritiqueAvailability(Optional ByVal refreshPage As Boolean = True)
        Dim i As Integer = 0
        Dim intAvailSeqId, intRow As Integer
        Dim status As String

        Try
            If currentCritique Is Nothing Then currentCritique = New clsCritique(OutageNumber)

            'Check whether we are dealing with the first record.  If so, you will not have a seqid so pass in a "0"
            With currentCritique
                .UserName = userProfile.Username
                If Me._ddlNewBusUnitAreaAvail.SelectedValue <> Nothing And Me._ddlNewBusUnitAreaAvail.SelectedValue <> "" Then
                    .Downtime1 = Me._tbNewDowntime1.Text
                    .Downtime2 = Me._tbNewDowntime2.Text
                    .Downtime3 = Me._tbNewDowntime3.Text
                    .Downtime4 = Me._tbNewDowntime4.Text
                    .Downtime5 = Me._tbNewDowntime5.Text
                    .Downtime6 = Me._tbNewDowntime6.Text
                    .Downtime7 = Me._tbNewDowntime7.Text
                    .BusinessUnitAreaLine = Me._ddlNewBusUnitAreaAvail.SelectedValue
                    status = .SaveCritiqueAvail(OutageNumber, 0)
                End If

                For i = 0 To __gvcritiqueavailability.DirtyRows.Count - 1
                    intRow = _gvcritiqueavailability.DirtyRows.Item(i).DataItemIndex
                    intAvailSeqId = Me._gvcritiqueavailability.DataKeys.Item(intRow).Value
                    .Downtime1 = CType(Me._gvcritiqueavailability.Rows(intRow).FindControl("_tbDowntime1"), TextBox).Text
                    .Downtime2 = CType(Me._gvcritiqueavailability.Rows(intRow).FindControl("_tbDowntime2"), TextBox).Text
                    .Downtime3 = CType(Me._gvcritiqueavailability.Rows(intRow).FindControl("_tbDowntime3"), TextBox).Text
                    .Downtime4 = CType(Me._gvcritiqueavailability.Rows(intRow).FindControl("_tbDowntime4"), TextBox).Text
                    .Downtime5 = CType(Me._gvcritiqueavailability.Rows(intRow).FindControl("_tbDowntime5"), TextBox).Text
                    .Downtime6 = CType(Me._gvcritiqueavailability.Rows(intRow).FindControl("_tbDowntime6"), TextBox).Text
                    .Downtime7 = CType(Me._gvcritiqueavailability.Rows(intRow).FindControl("_tbDowntime30Days"), TextBox).Text
                    status = currentCritique.SaveCritiqueAvail(OutageNumber, intAvailSeqId)
                Next
            End With
        Catch
            Throw
        End Try
    End Sub
    Protected Sub _gvcritiqueavailability_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles _gvcritiqueavailability.RowDeleting
        Try
            Dim strSeqid As Integer = Convert.ToInt32(_gvcritiqueavailability.DataKeys(e.RowIndex).Value)
            clsCritique.DeleteCritiqueAvail(strSeqid, userProfile.Username)
            currentCritique = New clsCritique(OutageNumber)
            GetCritique()

        Catch ex As Exception
            Throw New Exception("_gvcritiqueavailability.DeleteCommand", ex.InnerException)
        End Try

    End Sub

    Protected Sub UpdateCritiquePerformance(Optional ByVal refreshPage As Boolean = True)
        Dim i As Integer = 0
        Dim intPerfSeqId, intRow As Integer
        Dim status As String

        Try

            If currentCritique Is Nothing Then currentCritique = New clsCritique(OutageNumber)

            'Check whether we are dealing with the first record.  If so, you will not have a seqid so pass in a "0"
            With currentCritique
                .UserName = userProfile.Username
                If (Me._tbNewPerformanceItem.Text Is Nothing Or Me._tbNewPerformanceItem.Text = "") And (Me._ddlNewBusUnitArea Is Nothing Or Me._ddlNewBusUnitArea.SelectedValue = "") Then
                    .UserName = userProfile.Username
                Else
                    If Me._tbNewPerformanceItem.Text IsNot Nothing And Me._tbNewPerformanceItem.Text <> "" Then
                        .PerformanceItemDesc = Me._tbNewPerformanceItem.Text
                    Else
                        .PerformanceItemDesc = Me._ddlNewBusUnitArea.SelectedValue
                    End If
                    .PerformancePlannedStart = Me._tbNewPlannedStart.Text
                    .PerformancePlannedEnd = Me._tbNewPlannedEnd.Text
                    .PerformanceActualStart = Me._tbNewActualStart.Text
                    .PerformanceActualEnd = Me._tbNewActualEnd.Text
                    status = .SaveCritiquePerformance(OutageNumber, 0)
                End If


                For i = 0 To __gvcritiqueperformance.DirtyRows.Count - 1
                    intRow = _gvcritiqueperformance.DirtyRows.Item(i).DataItemIndex
                    intPerfSeqId = Me._gvcritiqueperformance.DataKeys.Item(intRow).Value
                    .PerformanceItemDesc = CType(Me._gvcritiqueperformance.Rows(intRow).FindControl("_lblPerformanceType"), Label).Text
                    .PerformancePlannedStart = CType(Me._gvcritiqueperformance.Rows(intRow).FindControl("_tbPlannedStart"), TextBox).Text
                    .PerformancePlannedEnd = CType(Me._gvcritiqueperformance.Rows(intRow).FindControl("_tbPlannedEnd"), TextBox).Text
                    .PerformanceActualStart = CType(Me._gvcritiqueperformance.Rows(intRow).FindControl("_tbActualStart"), TextBox).Text
                    .PerformanceActualEnd = CType(Me._gvcritiqueperformance.Rows(intRow).FindControl("_tbActualEnd"), TextBox).Text
                    status = currentCritique.SaveCritiquePerformance(OutageNumber, intPerfSeqId)

                Next

                Me._tbNewPerformanceItem.Text = ""
                Me._tbNewPlannedEnd.Text = ""
                Me._tbNewPlannedStart.Text = ""
                Me._tbNewActualEnd.Text = ""
                Me._tbNewActualStart.Text = ""

            End With
        Catch
            Throw
        End Try
    End Sub

    Protected Sub UpdateCritiqueItem(Optional ByVal refreshPage As Boolean = True)
        Dim i As Integer = 0
        Dim intCritiqueSeqId, intRow As Integer
        Dim status As String

        Try

            If currentCritique Is Nothing Then currentCritique = New clsCritique(OutageNumber)

            'Check whether we are dealing with the first record.  If so, you will not have a seqid so pass in a "0"
            With currentCritique
                .UserName = userProfile.Username
                If Me._tbNewDesc.Text <> Nothing Then
                    .CritiqueCategory = Me._ddlNewCritiqueCategory.SelectedValue
                    .CritiqueSubCategory = Me._ddlNewCritiqueSubCategory.SelectedValue
                    .BusinessUnit = Me._ddlNewBusUnit.SelectedValue
                    .CritiqueItemDesc = Me._tbNewDesc.Text
                    status = .SaveCritiqueItem(OutageNumber, 0)
                End If


                For i = 0 To _gvcritique.DirtyRows.Count - 1
                    intRow = _gvcritique.DirtyRows.Item(i).DataItemIndex
                    intCritiqueSeqId = Me._gvcritique.DataKeys.Item(intRow).Value
                    .CritiqueCategory = CType(Me._gvcritique.Rows(intRow).FindControl("_ddlCritiqueCategory"), DropDownList).SelectedValue
                    .CritiqueSubCategory = CType(Me._gvcritique.Rows(intRow).FindControl("_ddlCritiqueSubCategory"), DropDownList).SelectedValue
                    .BusinessUnit = CType(Me._gvcritique.Rows(intRow).FindControl("_ddlBusUnit"), DropDownList).SelectedValue
                    .CritiqueItemDesc = CType(Me._gvcritique.Rows(intRow).FindControl("_tbDesc"), TextBox).Text
                    status = currentCritique.SaveCritiqueItem(OutageNumber, intCritiqueSeqId)

                Next

            End With
            If refreshPage = True Then
                Response.Redirect(Page.AppRelativeVirtualPath & "?OutageNumber=" & currentCritique.OutageNumber, True)
            End If
        Catch
            Throw
        End Try
    End Sub

    Protected Sub _gvcritiqueperformance_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _gvcritiqueperformance.RowDataBound
        If e.Row.RowIndex = 0 Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim deleteButton As LinkButton = e.Row.FindControl("_lnkBtnPerformance")
                Dim tbPerformanceType As Label = e.Row.FindControl("_tbPerformanceType")
                Dim tbPlannedStart As TextBox = e.Row.FindControl("_tbPlannedStart")
                Dim tbPlannedEnd As TextBox = e.Row.FindControl("_tbPlannedEnd")
                tbPlannedStart.ReadOnly = "True"
                tbPlannedStart.CssClass = ""
                tbPlannedEnd.CssClass = ""
                tbPlannedEnd.ReadOnly = "True"
                If deleteButton IsNot Nothing Then
                    deleteButton.Visible = "False"
                End If
            End If
        End If

    End Sub
    Protected Sub _gvcritiqueperformance_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles _gvcritiqueperformance.RowDeleting
        Try
            Dim strSeqid As Integer = Convert.ToInt32(_gvcritiqueperformance.DataKeys(e.RowIndex).Value)
            clsCritique.DeleteCritiquePerf(strSeqid, userProfile.Username)
            currentCritique = New clsCritique(OutageNumber)
            GetCritique()

        Catch ex As Exception
            Throw New Exception("_gvcritiqueperformnace.DeleteCommand", ex.InnerException)
        End Try

    End Sub
    Protected Sub _gvcritique_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _gvcritique.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            If currentCritique Is Nothing Then currentCritique = New clsCritique(OutageNumber)
            Dim cddlCritiqueCategory As AjaxControlToolkit.CascadingDropDown = e.Row.FindControl("_cddlCritiqueCategory")
            Dim Critique_Category As String = e.Row.DataItem("critiquecategoryseqid").ToString()
            ' Dim Critique_Category As String = e.Row.DataItem("critique_category") & ";" & e.Row.DataItem("critiquecategoryseqid").ToString()
            cddlCritiqueCategory.SelectedValue = Critique_Category
            Dim cddlCritiqueSubCategory As AjaxControlToolkit.CascadingDropDown = e.Row.FindControl("_cddlCritiqueSubCategory")
            Dim Critique_SubCategory As String = e.Row.DataItem("critiquesubcategoryseqid").ToString
            cddlCritiqueSubCategory.SelectedValue = Critique_SubCategory
            Dim ddlBusUnit As DropDownList = e.Row.FindControl("_ddlBusUnit")
            ddlBusUnit.DataSource = currentCritique.mBusUnit
            ddlBusUnit.DataTextField = "busunit"
            ddlBusUnit.DataValueField = "busunit"
            ddlBusUnit.DataBind()
            ddlBusUnit.Items.Insert(0, "")
            Dim busunit As String = e.Row.DataItem("busunit").ToString
            If ddlBusUnit.Items.FindByValue(busunit) IsNot Nothing Then
                ddlBusUnit.Items.FindByValue(busunit).Selected = True
            End If

        End If
    End Sub
    Protected Sub _gvcritique_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles _gvcritique.RowDeleting
        Try
            Dim strSeqid As Integer = Convert.ToInt32(_gvcritique.DataKeys(e.RowIndex).Value)
            clsCritique.DeleteCritique(strSeqid, userProfile.Username)
            currentCritique = New clsCritique(OutageNumber)
            GetCritique()

        Catch ex As Exception
            Throw New Exception("_gvcritiqueperformnace.DeleteCommand", ex.InnerException)
        End Try

    End Sub
    Protected Sub UpdateCritiqueWO(Optional ByVal refreshPage As Boolean = True)
        Dim i As Integer = 0
        Dim intWOSeqId, intRow As Integer
        Dim status As String

        Try
            'Check whether we are dealing with the first record.  If so, you will not have a seqid so pass in a "0"
            With currentCritique
                .UserName = userProfile.Username
                If __gvcritiqueWO.Rows.Count = 1 Then
                    status = currentCritique.SaveCritiqueWO(OutageNumber, 0)
                Else
                    For i = 0 To __gvcritiqueWO.DirtyRows.Count - 1
                        intRow = _gvcritiqueWO.DirtyRows.Item(i).DataItemIndex
                        intWOSeqId = Me._gvcritiqueWO.DataKeys.Item(intRow).Value
                        .PlannedMech = CType(Me._gvcritiqueWO.Rows(intRow).FindControl("_tbPlannedMechanical"), TextBox).Text
                        .PlannedElec = CType(Me._gvcritiqueWO.Rows(intRow).FindControl("_tbPlannedElectrical"), TextBox).Text
                        .PlannedCont = CType(Me._gvcritiqueWO.Rows(intRow).FindControl("_tbPlannedContracted"), TextBox).Text
                        .CompletedMech = CType(Me._gvcritiqueWO.Rows(intRow).FindControl("_tbCompletedMechanical"), TextBox).Text
                        .CompletedElec = CType(Me._gvcritiqueWO.Rows(intRow).FindControl("_tbCompletedElectrical"), TextBox).Text
                        .CompletedCont = CType(Me._gvcritiqueWO.Rows(intRow).FindControl("_tbCompletedContracted"), TextBox).Text
                        status = currentCritique.SaveCritiqueWO(OutageNumber, intWOSeqId)
                    Next
                End If
            End With
            
        Catch
            Throw
        End Try
    End Sub
    Protected Sub _gvcritiquewo_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _gvcritiqueWO.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblWOType As Label = e.Row.FindControl("_lblWOType")
            Dim lblPercent_Mech As Label = e.Row.FindControl("_lblPercent_Mech")
            Dim lblPercent_Elect As Label = e.Row.FindControl("_lblPercent_Elect")
            Dim lblPercent_Cont As Label = e.Row.FindControl("_lblPercent_Cont")
            Dim lblPercent_Total As Label = e.Row.FindControl("_lblPercent_Total")
            Dim tbPlannedMechanical As TextBox = e.Row.FindControl("_tbPlannedMechanical")
            Dim tbPlannedElectrical As TextBox = e.Row.FindControl("_tbPlannedElectrical")
            Dim tbPlannedContracted As TextBox = e.Row.FindControl("_tbPlannedContracted")
            Dim tbCompletedMechanical As TextBox = e.Row.FindControl("_tbCompletedMechanical")
            Dim tbCompletedElectrical As TextBox = e.Row.FindControl("_tbCompletedElectrical")
            Dim tbCompletedContracted As TextBox = e.Row.FindControl("_tbCompletedContracted")
            Dim tbCompletedMechanicalPercent As Label = e.Row.FindControl("_tbCompletedMechanicalPercent")
            Dim tbCompletedElectricalPercent As Label = e.Row.FindControl("_tbCompletedElectricalPercent")
            Dim tbCompletedContractedPercent As Label = e.Row.FindControl("_tbCompletedContractedPercent")
            Dim tbTotalPercent As Label = e.Row.FindControl("_tbTotalPercent")
            If lblWOType.Text = "Percentage Add On % (calculated)" Then
                tbPlannedMechanical.ReadOnly = "True"
                tbPlannedElectrical.ReadOnly = "True"
                tbPlannedContracted.ReadOnly = "True"
                tbCompletedMechanical.Visible = "False"
                tbCompletedElectrical.Visible = "False"
                tbCompletedContracted.Visible = "False"
                tbCompletedMechanicalPercent.Visible = "False"
                tbCompletedElectricalPercent.Visible = "False"
                tbCompletedContractedPercent.Visible = "False"
                tbTotalPercent.Visible = "False"
                lblPercent_Mech.Visible = "True"
                lblPercent_Elect.Visible = "True"
                lblPercent_Cont.Visible = "True"
                lblPercent_Total.Visible = "True"
            Else
                lblPercent_Mech.Visible = "False"
                lblPercent_Elect.Visible = "False"
                lblPercent_Cont.Visible = "False"
                lblPercent_Total.Visible = "False"
            End If
        End If


    End Sub


    Protected Sub UpdateCritiqueQA(Optional ByVal refreshPage As Boolean = True)
        Dim i As Integer = 0
        Dim intQASeqId, intRow As Integer
        Dim status As String

        Try
            'Check whether we are dealing with the first record.  If so, you will not have a seqid so pass in a "0"
            With currentCritique
                .UserName = userProfile.Username

                For i = 0 To __gvcritiqueQA.DirtyRows.Count - 1
                    intRow = _gvcritiqueQA.DirtyRows.Item(i).DataItemIndex
                    intQASeqId = Me._gvcritiqueQA.DataKeys.Item(intRow).Value
                    .QAPlannedCount = CType(Me._gvcritiqueQA.Rows(intRow).FindControl("_tbQAPlannedCount"), TextBox).Text
                    .QACompleteCount = CType(Me._gvcritiqueQA.Rows(intRow).FindControl("_tbQACompleteCount"), TextBox).Text
                    status = currentCritique.SaveCritiqueQA(OutageNumber, intQASeqId)
                Next
            End With

        Catch
            Throw
        End Try
    End Sub
    'Protected Sub _gvcritiquewo_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _gvcritiqueWO.RowDataBound
    '    If _gvcritiqueWO.Rows.Count > 0 Then
    '        Dim footerRow As GridViewRow = Me._gvcritiqueWO.FooterRow()
    '        Dim MyTotPlannedMech As Double = Session("PlannedMech")
    '        'Dim MyTotCostString As String = "$" & FormatNumber(MyTotCost, 0) 'MyTotCost.ToString("$#,##0;($#,##0);$0")
    '        Dim MyTotFinclImpact As Double = Session("totFinclImpact")
    '        'Dim MyTotFinclImpactString As String = "$" & FormatNumber(MyTotFinclImpact, 0) 'MyTotFinclImpact.ToString("$#,##0;($#,##0);$0")

    '        footerRow.Cells(7).Text = "Totals"
    '        footerRow.Cells(8).Text = MyTotCostString
    '        footerRow.Cells(9).Text = MyTotFinclImpactString
    '    End If
    'End Sub
    Public Sub Populatebusunit()
        Me._ddlNewBusUnit.DataSource = currentCritique.mBusUnit
        Me._ddlNewBusUnit.DataTextField = "busunit"
        Me._ddlNewBusUnit.DataValueField = "busunit"
        Me._ddlNewBusUnit.DataBind()
        Me._ddlNewBusUnit.Items.Insert(0, "")
        'If _ddlNewBusUnit.SelectedIndex = 0 Then
        ' If _ddlNewBusUnit.Items.FindByValue("Millwide - Other") IsNot Nothing Then
        '_ddlNewBusUnit.Items.FindByValue("Millwide - Other").Selected = True
        'End If
        'End If
    End Sub
    Public Sub PopulateBusunitAreaLine()
        Me._ddlNewBusUnitArea.DataSource = currentCritique.mBusUnitAreaLine
        Me._ddlNewBusUnitArea.DataTextField = "busunitarealine"
        Me._ddlNewBusUnitArea.DataValueField = "busunitarealine"
        Me._ddlNewBusUnitArea.DataBind()
        Me._ddlNewBusUnitArea.Items.Insert(0, "")
        Me._ddlNewBusUnitAreaAvail.DataSource = currentCritique.mBusUnitAreaLine
        Me._ddlNewBusUnitAreaAvail.DataTextField = "busunitarealine"
        Me._ddlNewBusUnitAreaAvail.DataValueField = "busunitarealine"
        Me._ddlNewBusUnitAreaAvail.DataBind()
        Me._ddlNewBusUnitAreaAvail.Items.Insert(0, "")
    End Sub
    Protected Sub CreateWORecords(Optional ByVal refreshPage As Boolean = True)
        Dim i As Integer = 0
        Dim intWOSeqId As Integer = 0
        Dim status As String

        Try
            'Check whether we are dealing with the first record.  If so, you will not have a seqid so pass in a "0"
            With currentCritique
                .UserName = userProfile.Username
                status = currentCritique.SaveCritiqueWO(OutageNumber, intWOSeqId)
            End With

        Catch
            Throw
        End Try
    End Sub
    Protected Sub CreateQARecords(Optional ByVal refreshPage As Boolean = True)
        Dim i As Integer = 0
        Dim intQASeqId As Integer = 0
        Dim status As String

        Try
            'Check whether we are dealing with the first record.  If so, you will not have a seqid so pass in a "0"
            With currentCritique
                .UserName = userProfile.Username
                status = currentCritique.SaveCritiqueQA(OutageNumber, intQASeqId)
            End With
        Catch
            Throw
        End Try
    End Sub
End Class
