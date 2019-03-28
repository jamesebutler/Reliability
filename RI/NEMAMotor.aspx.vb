
Partial Class NEMAMotor
    Inherits System.Web.UI.Page
    Protected Sub _ucDiagram_CancelClicked() Handles _ucDiagram.CancelClicked
        NextStep(0) ' First Step
    End Sub
    
    Protected Sub _ucDiagramAboveNema_CancelClicked() Handles _ucDiagramAboveNema.CancelClicked
        AboveNextStep(0)
    End Sub

    Private Sub NextStep(ByVal currentStep As Integer)
        Dim HistoryReport As String = ""
        Dim sb As New StringBuilder
        Dim bulletList As String = "<li>{0}</li>"
        If Session("HistoryReport") IsNot Nothing Then
            HistoryReport = Session("HistoryReport")
            sb.Append(HistoryReport)
        End If
        With _ucDiagram
            'If currentStep < 21 Then SetNextButtonJS(currentStep, 1)
            If .Decision = True Then
                .AutoAnswer = "Yes"
            Else
                .AutoAnswer = "No"
            End If

            Select Case currentStep
                Case 0
                    HistoryReport = String.Empty
                    sb.Length = 0
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Input
                    .DiagramText = "NEMA Frame Motor Input"
                    .YesNextStep = 1
                    .NoNextStep = 1
                    .PreviousStep = 0
                    .CurrentMotorType = User_Controls_ucDiagram.MotorType.NEMA
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.NextCancel
                    sb.Append("<b>" & .DiagramText & "</b><br>")
                Case 1
                    'Get previous data
                    Dim inputString As String = "<ul style='text-align:left'><li>Motor HP: {0}</li><li>Motor RPM: {1}</li>" '<li>Repair Price: {2}</li><li>% Efficiency: {3}</li><li>New Motor Price: {4}</li><li>New Motor % Efficiency: {5}</li></ul>"

                    sb.Append(String.Format(inputString, IIf(.HP > 200, "> 200", .HP), IIf(.RPM < 900, "< 900", .RPM))) ', .RepairPrice, .Efficiency, .NewMotorPrice, .NewEfficiency))

                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Decision
                    .DiagramText = "Are there any extenuating circumstances such as special frame, system drive motor, etc. which prevent new motor purchase?"
                    If .PreviousStep = 0 Then
                        .Decision = False
                        .AutoAnswer = "No"
                    End If
                    .YesNextStep = 2
                    .NoNextStep = 3
                    .PreviousStep = 0
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.BackNextCancel
                    sb.Append(String.Format(bulletList, .DiagramText))
                Case 2

                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")

                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Process
                    .DiagramText = "Repair Motor to IP spec 3691 (latest version)"
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.ReportCancel
                    .YesNextStep = 21
                    .NoNextStep = 21
                    .PreviousStep = 1
                    sb.Append(String.Format(bulletList, .DiagramText))
                Case 3 'Decision based on user input for Motor HP

                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    'no
                    .Decision = False
                    .AutoAnswer = "No"
                    'If .HP <= 100 And .RPM >= 1200 Then
                    '    'yes
                    '    .Decision = True
                    '    .AutoAnswer = "Yes"
                    'Else
                    '    'no
                    '    .Decision = False
                    '    .AutoAnswer = "No"
                    'End If
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Decision
                    .DiagramText = "Is the motor less than 5 years old? "
                    .YesNextStep = 4
                    .NoNextStep = 5
                    .PreviousStep = 1
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.BackNextCancel
                    sb.Append(String.Format(bulletList, .DiagramText))
                Case 4

                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Process
                    .DiagramText = "Replace / Repair under warranty to 3614 new motor specifictions"
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.ReportCancel
                    .YesNextStep = 21
                    .NoNextStep = 21
                    .PreviousStep = 3
                    sb.Append(String.Format(bulletList, .DiagramText))
                Case 5

                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Decision
                    .DiagramText = "Is the motor older than 15 years? "
                    'If .Efficiency > 0 Then 'Efficiency has been supplied
                    .Decision = True
                    .AutoAnswer = "Yes"
                    'Else
                    '    .Decision = False
                    '    .AutoAnswer = "No"
                    'End If
                    .YesNextStep = 7
                    .NoNextStep = 6
                    .PreviousStep = 3
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.BackNextCancel
                    sb.Append(String.Format(bulletList, .DiagramText))
                Case 6 'Decision based on user input for motor speed

                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    If .HP <= 200 And .RPM >= 900 Then
                        .Decision = True
                        .AutoAnswer = "Yes"
                    Else
                        'no
                        .Decision = False
                        .AutoAnswer = "No"
                    End If

                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.AutoDecision
                    .DiagramText = "Is the motor 200 hp or less & 900 rpm or faster?"
                    .YesNextStep = 8
                    .NoNextStep = 9
                    .PreviousStep = 5
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.BackNextCancel
                    sb.Append(String.Format(bulletList, .DiagramText))
                    'Use OEM motor vendor supplied efficiency estimation data to estimate old motor efficiency
                Case 7

                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Process
                    .DiagramText = "Replace Motor"
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.ReportCancel
                    .YesNextStep = 21
                    .NoNextStep = 21
                    .PreviousStep = 5
                    sb.Append(String.Format(bulletList, .DiagramText))
                Case 8

                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Process
                    .DiagramText = "Replace Motor"
                    .YesNextStep = 21
                    .NoNextStep = 21
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.ReportCancel
                    .PreviousStep = 6
                    sb.Append(String.Format(bulletList, .DiagramText))
                Case 9
                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Decision
                    'If .OldEfficiency > 0 Then
                    '    .DiagramText = "Is % efficiency stated on nameplate?<br/><center><font color='#ff0000'>" & .OldEfficiency & "%</font></center>"
                    '    .Decision = True
                    '    .AutoAnswer = "Yes"
                    'Else
                    .DiagramText = "Is % efficiency stated on nameplate?"
                    .Decision = True
                    .AutoAnswer = "Yes"
                    'End If
                    .YesNextStep = 23
                    .NoNextStep = 10
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.BackNextCancel
                    .PreviousStep = 6
                    sb.Append(String.Format(bulletList, .DiagramText))

                Case 10 'Mark will provide a spreadsheet with the efficiency data

                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Data
                    .DiagramText = "Use OEM motor vendor supplied efficiency estimation data to estimate old motor efficiency"
                    .YesNextStep = 23
                    .NoNextStep = 23
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.BackNextCancel
                    .PreviousStep = 9
                    sb.Append(String.Format(bulletList, .DiagramText))
                    'Case 10

                    '    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    '    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.PredefinedProcess
                    '    .DiagramText = "Motor must be rewound"
                    '    .YesNextStep = 11
                    '    .NoNextStep = 11
                    '    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.BackNextCancel
                    '    .PreviousStep = 8
                    '    sb.Append(String.Format(bulletList, .DiagramText))
                Case 11

                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Decision
                    .DiagramText = "Is the existing winding integrity good for minimum 20 year life?"
                    .YesNextStep = 13
                    .NoNextStep = 12
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.BackNextCancel
                    .PreviousStep = 9
                    sb.Append(String.Format(bulletList, .DiagramText))
                    .Decision = False
                    .AutoAnswer = "No"
                Case 12
                    .AutoAnswer = "No"
                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Process
                    .DiagramText = "Motor must be rewound"
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.BackNextCancel
                    .YesNextStep = 13
                    .NoNextStep = 13
                    .PreviousStep = 11
                    sb.Append(String.Format(bulletList, .DiagramText))
                Case 13

                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Decision
                    .DiagramText = "Can the motor be repaired per IP LV motor repair spec 3691 (including rewind and install Inpro<sup>™</sup> Seal if necessary) & meet new motor spec 3614 performance?"
                    .YesNextStep = 15
                    .NoNextStep = 14
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.BackNextCancel
                    .PreviousStep = 11
                    sb.Append(String.Format(bulletList, .DiagramText))
                Case 14 'Calculate value
                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Process
                    .DiagramText = "Replace Motor"
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.ReportCancel
                    .YesNextStep = 21
                    .NoNextStep = 21
                    .PreviousStep = 13
                    sb.Append(String.Format(bulletList, .DiagramText))
                    'sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    '.SelectedDiagram = User_Controls_ucDiagram.DiagramView.Decision
                    '.DiagramText = "Can the motor be updated to meet IP spec 3614 (and rewound if necessary) and still meet repair and energy cost hurdle(1)?"

                    ''Dim annualSavings As Decimal = (.Efficiency - .OldEfficiency) * .HP * 340
                    'If .Efficiency = 0 And .RPM = 900 Then
                    '    .Efficiency = .OldEfficiency
                    'End If
                    ''Dim annualSavings As Decimal = FormatNumber((.NewEfficiency - .Efficiency) / 100 * .HP * 340, 2)
                    ''Dim costHurdle As Decimal = FormatNumber((.NewMotorPrice - .RepairPrice) - (2 * annualSavings), 2)
                    ''If costHurdle > 0 Then
                    ''    'Met cost hurdle
                    ''    .AutoAnswer = "Yes"
                    ''    .Decision = True
                    ''Else
                    ''    'Did not meet cost hurdle
                    ''    .AutoAnswer = "No"
                    ''    .Decision = False
                    ''End If
                    '.Decision = False
                    '.YesNextStep = 15
                    '.NoNextStep = 12
                    '.ButtonMode = User_Controls_ucDiagram.ButtonStyle.BackNextCancel
                    '.PreviousStep = 13
                    'sb.Append(String.Format(bulletList, .DiagramText))
                    ''Dim annualSavingsFormula As String = String.Format("Annual Savings = [({0} - {1})/100 * {2} * 340]={3}", .NewEfficiency, .Efficiency, .HP, annualSavings)
                    ''Dim costHurdleFormula As String = String.Format("Cost Hurdle = [({0} - {1}) - (2 * {2}]) = {3}", .NewMotorPrice, .RepairPrice, annualSavings, costHurdle)
                    ''sb.Append("<ul><li>" & annualSavingsFormula & "</li></ul>")
                    ''sb.Append("<ul><li>" & costHurdleFormula & "</li></ul>")
                Case 15

                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Decision
                    .DiagramText = "Are there other extenuating circumstances(2)that would prevent repairing of the motor?"
                    .Decision = False
                    .YesNextStep = 16
                    .NoNextStep = 24
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.BackNextCancel
                    .PreviousStep = 13
                    sb.Append(String.Format(bulletList, .DiagramText))
                Case 16

                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Process
                    .DiagramText = "Replace Motor"
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.ReportCancel
                    .YesNextStep = 21
                    .NoNextStep = 21
                    .PreviousStep = 15
                    sb.Append(String.Format(bulletList, .DiagramText))
                Case 17 'Make a decision based on motor cost data


                    'Get previous data
                    Dim inputString As String = "<ul style='text-align:left'><li>Motor HP: {0}</li><li>Motor RPM: {1}</li><li>Repair Price: {2}</li><li>% Efficiency: {3}</li><li>New Motor Price: {4}</li><li>New Motor % Efficiency: {5}</li></ul>"
                    sb.Append(String.Format(inputString, IIf(.HP > 200, "> 200", .HP), IIf(.RPM < 900, "< 900", .RPM), .RepairPrice, .Efficiency, .NewMotorPrice, .NewEfficiency))

                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.AutoDecision
                    .DiagramText = String.Format("Is repair cost [{0}%] more than 65% of new motor cost? ", FormatNumber(.RepairPrice / .NewMotorPrice * 100, 0))
                    Dim repairFormula As String = "{0}/"
                    If .RepairPrice / .NewMotorPrice * 100 > 65 Then
                        .AutoAnswer = "Yes"
                        .Decision = True
                    Else
                        .AutoAnswer = "No"
                        .Decision = False
                    End If

                    .YesNextStep = 16
                    .NoNextStep = 18
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.BackNextCancel
                    .PreviousStep = 15
                    sb.Append(String.Format(bulletList, .DiagramText))
                Case 18

                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.AutoDecision
                    .DiagramText = "Does the motor repair meet repair and energy cost hurdle(1)?"
                    'Dim annualSavings As Decimal = (.Efficiency - .OldEfficiency) * .HP * 340
                    If .Efficiency = 0 And .RPM = 900 Then
                        .Efficiency = .OldEfficiency
                    End If
                    Dim annualSavings As Decimal = FormatNumber((.NewEfficiency - .Efficiency) / 100 * .HP * 340, 2)
                    'Dim costHurdle As Decimal = FormatNumber((.NewMotorPrice - .RepairPrice) - (2 * annualSavings), 2)
                    Dim costHurdle As Decimal = FormatNumber(((0.65 * .NewMotorPrice) - .RepairPrice) - (2 * annualSavings), 2)
                    If costHurdle > 0 Then
                        'If costHurdle > 0.65 * .NewMotorPrice Then
                        'Met cost hurdle
                        .AutoAnswer = "Yes"
                        .Decision = True
                        'Else
                        '    .AutoAnswer = "No"
                        '    .Decision = False
                        'End If
                    Else
                        'Did not meet cost hurdle
                        .AutoAnswer = "No"
                        .Decision = False
                    End If
                    .YesNextStep = 19
                    .NoNextStep = 20
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.BackNextCancel
                    .PreviousStep = 17
                    sb.Append(String.Format(bulletList, .DiagramText))
                    Dim annualSavingsFormula As String = String.Format("Annual Savings = [({0} - {1})/100 * {2} * 340]={3}", .NewEfficiency, .Efficiency, .HP, annualSavings)
                    Dim costHurdleFormula As String = String.Format("Cost Hurdle = [((0.65*{0}) - {1}) - (2 * {2}]) = {3}", .NewMotorPrice, .RepairPrice, annualSavings, costHurdle)
                    sb.Append("<ul><li>" & annualSavingsFormula & "</li></ul>")
                    sb.Append("<ul><li>" & costHurdleFormula & "</li></ul>")
                Case 19

                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Process
                    .DiagramText = "Repair Motor to IP spec 3691 (latest version)"
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.ReportCancel
                    .YesNextStep = 21
                    .NoNextStep = 21
                    .PreviousStep = 18
                    sb.Append(String.Format(bulletList, .DiagramText))
                Case 20

                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Process
                    .DiagramText = "Replace Motor"
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.ReportCancel
                    .YesNextStep = 21
                    .NoNextStep = 21
                    .PreviousStep = 18
                    sb.Append(String.Format(bulletList, .DiagramText))

                Case 21
                    sb.Append("</ul>")
                    HistoryReport = sb.ToString
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Report
                    .DiagramText = HistoryReport
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.Restart
                    .NoNextStep = 0
                    .PreviousStep = 0
                Case 22
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Input
                    .DiagramText = "NEMA Frame Motor Input"
                    .YesNextStep = 17
                    .NoNextStep = 17
                    .PreviousStep = 15
                    .CurrentMotorType = User_Controls_ucDiagram.MotorType.NEMA
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.NextCancel
                    sb.Append("<b>" & .DiagramText & "</b><br>")
                Case 23
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.InputOldEfficiency
                    .DiagramText = "Enter % Efficiency"
                    .YesNextStep = 11
                    .NoNextStep = 11
                    .PreviousStep = 9
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.BackNextCancel
                    sb.Append(String.Format(bulletList, .DiagramText))
                Case 24
                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.InputRepairPrice
                    .DiagramText = "Enter Repair Cost"
                    .YesNextStep = 25
                    .NoNextStep = 25
                    .PreviousStep = 15
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.BackNextCancel
                    sb.Append(String.Format(bulletList, .DiagramText))
                Case 25
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.InputNewPrice
                    .DiagramText = "Enter New Motor Cost"
                    .YesNextStep = 26
                    .NoNextStep = 26
                    .PreviousStep = 25
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.BackNextCancel
                    sb.Append(String.Format(bulletList, .DiagramText))
                Case 26
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.InputNewEfficiency
                    .DiagramText = "Enter New Motor Efficiency"
                    .YesNextStep = 17
                    .NoNextStep = 17
                    .PreviousStep = 25
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.BackNextCancel
                    sb.Append(String.Format(bulletList, .DiagramText))
            End Select
            If currentStep >= 0 And currentStep <= 20 Then Me._imgNemaDiagram.ImageUrl = String.Format("~/NEMA/NEMA2_files/Nema_Steps{0}.jpg", currentStep)
            HistoryReport = sb.ToString
            Session("HistoryReport") = HistoryReport
            If .NoNextStep = 0 Then
                '   MsgBox(HistoryReport)
            End If
        End With

    End Sub

    Private Sub SetNextButtonJS(ByVal currentStep As Integer, ByVal tab As Integer)
        Dim sb As New StringBuilder
        sb.Append("$get('")
        Select Case tab
            Case 0
                'sb.Append(Me._ifrAboveNEMA.ClientID)
                sb.Append("').contentWindow.frmToolbar.findCurrentStep(")
                sb.Append(currentStep.ToString)
                sb.Append(");")
                Me.btnAboveNemaMove.Attributes.Add("onclick", sb.ToString)
            Case 1
                'sb.Append(Me._ifrNEMA.ClientID)
                sb.Append("').contentWindow.frmToolbar.findCurrentStep(")
                sb.Append(currentStep.ToString)
                sb.Append(");")
                Me.btnNemaMove.Attributes.Add("onclick", sb.ToString)
        End Select

    End Sub

    Private Sub AboveNextStep(ByVal currentStep As Integer)
        Dim HistoryReport As String = ""
        Dim sb As New StringBuilder
        Dim bulletList As String = "<li>{0}</li>"
        If Session("HistoryAboveNemaReport") IsNot Nothing Then
            HistoryReport = Session("HistoryAboveNemaReport")
            sb.Append(HistoryReport)
        End If
        With _ucDiagramAboveNema
            .CurrentStep = currentStep
            'SetNextButtonJS(currentStep, 0)
            'Me._ifrAboveNEMA.Attributes("src") = "NEMA/Above.htm?step=" & currentStep
            .SuggestedAnswer = ""
            If .Decision = True Then
                .AutoAnswer = "Yes"
            Else
                .AutoAnswer = "No"
            End If
            Select Case currentStep
                Case 0
                    HistoryReport = String.Empty
                    sb.Length = 0
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Input
                    .DiagramText = "Above NEMA Frame Motor Input"
                    .YesNextStep = 1
                    .NoNextStep = 1
                    .PreviousStep = 0
                    .CurrentMotorType = User_Controls_ucDiagram.MotorType.AboveNEMA
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.NextCancel
                    sb.Append("<b>" & .DiagramText & "</b><br>")
                Case 1
                    'Get previous data
                    Dim AES As Decimal
                    AES = (.NewEfficiency - .Efficiency) / 100 * .HP * 340
                    Dim REC As Decimal
                    REC = (.NewMotorPrice - .RepairPrice) - (2 * AES)

                    Dim inputString As String = "<ul style='text-align:left'><li>Motor HP: {0}</li><li>Motor RPM: {1}</li><li>Repair Price: {2}</li><li>% Efficiency: {3}</li><li>New Motor Price: {4}</li><li>New Motor % Efficiency: {5}</li><li>Annual Savings: {6}</li>Cost Hurdle: {7}<li></li>"
                    sb.Append(String.Format(inputString, .HP, "N/A", .RepairPrice, .Efficiency, .NewMotorPrice, .NewEfficiency, AES, REC))

                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Decision
                    .DiagramText = "Are there any extenuating circumstances such as delivery, special frame, etc. which prevent new motor purchase?"
                    .YesNextStep = 2
                    .NoNextStep = 3
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.NextCancel
                    sb.Append("<b>" & .DiagramText & "</b><br>")
                Case 2
                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Process
                    .DiagramText = "Repair Motor to IP spec 3692 (latest version)"
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.ReportCancel
                    .YesNextStep = 13
                    .NoNextStep = 13
                    sb.Append("<b>" & .DiagramText & "</b><br>")
                Case 3
                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Decision
                    .DiagramText = "Is the existing winding integrity good for minimum 20 year life?  Note:  Exceptions can be made as appropriate."
                    .YesNextStep = 7
                    .NoNextStep = 4
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.NextCancel
                    sb.Append("<b>" & .DiagramText & "</b><br>")
                Case 4
                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.PredefinedProcess
                    .DiagramText = "If repaired, Motor must be rewound per IP Spec 3692 (latest version)"
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.NextCancel
                    .YesNextStep = 7
                    .NoNextStep = 7
                    sb.Append("<b>" & .DiagramText & "</b><br>")
                Case 5
                    'Is this a user decision or system decision
                    Dim AES As Decimal
                    AES = (.NewEfficiency - .Efficiency) / 100 * .HP * 340
                    Dim REC As Decimal
                    REC = (.NewMotorPrice - .RepairPrice) - (2 * AES)
                    If REC > 0 Then
                        .Decision = True
                        .AutoAnswer = "Yes"
                    Else
                        .Decision = False
                        .AutoAnswer = "No"
                    End If
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.AutoDecision
                    .DiagramText = "Can the motor be rewound <br/>(with good laminations that have no significant damage and core loss tests good) and repaired per IP spec 3692 (latest version) and still meet repair and energy cost hurdle (1)?"
                    .YesNextStep = 7
                    .NoNextStep = 6
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.NextCancel
                    sb.Append("<b>" & .DiagramText & "</b><br>")
                Case 6
                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Process
                    .DiagramText = "Replace Motor"
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.ReportCancel
                    .YesNextStep = 13
                    .NoNextStep = 13
                    sb.Append("<b>" & .DiagramText & "</b><br>")
                Case 7
                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Decision
                    .DiagramText = "Are there other extenuating <br/>circumstances (2) that would prevent proper repair of the motor?" ' or is the proper repair cost of these items prohibitive per the repair and energy cost hurdle (1)?"
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.NextCancel
                    Dim AES As Decimal
                    AES = (.NewEfficiency - .Efficiency) / 100 * .HP * 340
                    Dim REC As Decimal
                    REC = (.NewMotorPrice - .RepairPrice) - (2 * AES)
                    If REC > 0 Then
                        .Decision = False
                        '.SuggestedAnswer = "Passed - Repair and Energy Cost Hurdle"
                        '.AutoAnswer = "No, Passed - Repair and Energy Cost Hurdle"
                    Else
                        .Decision = True
                        '.SuggestedAnswer = "Failed - Repair and Energy Cost Hurdle"
                        '.AutoAnswer = "Yes, Failed - Repair and Energy Cost Hurdle"
                    End If
                    .YesNextStep = 8
                    .NoNextStep = 9
                    sb.Append("<b>" & .DiagramText & "</b><br>")
                Case 8
                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Process
                    .DiagramText = "Replace Motor"
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.ReportCancel
                    .YesNextStep = 13
                    .NoNextStep = 13
                    sb.Append("<b>" & .DiagramText & "</b><br>")
                Case 9
                    'Data for this decision will be entered by user
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.AutoDecision
                    .DiagramText = "Is repair cost more than 70% of new motor cost?"
                    .YesNextStep = 8
                    .NoNextStep = 10
                    If .RepairPrice > (.NewMotorPrice * 0.7) Then
                        .Decision = True
                        .AutoAnswer = "Yes"
                    Else
                        .Decision = False
                        .AutoAnswer = "No"
                    End If
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.NextCancel
                    sb.Append("<b>" & .DiagramText & "</b><br>")
                Case 10
                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.AutoDecision
                    .DiagramText = "Does motor meet repair and energy cost hurdle (1)?"
                    .YesNextStep = 12
                    .NoNextStep = 11
                    '
                    Dim AES As Decimal
                    AES = (.NewEfficiency - .Efficiency) / 100 * .HP * 340
                    Dim REC As Decimal
                    REC = (.NewMotorPrice - .RepairPrice) - (2 * AES)
                    If REC > 0 Then
                        .Decision = True
                        .AutoAnswer = "Yes"
                    Else
                        .Decision = False
                        .AutoAnswer = "No"
                    End If
                    '
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.NextCancel
                    sb.Append("<b>" & .DiagramText & "</b><br>")
                Case 11
                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Process
                    .DiagramText = "Replace Motor"
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.ReportCancel
                    .YesNextStep = 13
                    .NoNextStep = 13
                    sb.Append("<b>" & .DiagramText & "</b><br>")
                Case 12
                    sb.Append("<ul><li>" & .AutoAnswer & "</li></ul>")
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Process
                    .DiagramText = "Repair Motor to IP spec 3692 (latest version)"
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.ReportCancel
                    .YesNextStep = 13
                    .NoNextStep = 13
                    sb.Append("<b>" & .DiagramText & "</b><br>")
                Case 13
                    sb.Append("</ul>")
                    HistoryReport = sb.ToString
                    .SelectedDiagram = User_Controls_ucDiagram.DiagramView.Report
                    .DiagramText = HistoryReport
                    .ButtonMode = User_Controls_ucDiagram.ButtonStyle.Restart
                    .NoNextStep = 0
                    .PreviousStep = 0
            End Select
            If currentStep >= 0 And currentStep <= 12 Then Me._imgAboveNemaDiagram.ImageUrl = String.Format("~/NEMA/NEMA2_files/ANema_Steps{0}.jpg?v=1", currentStep)
            HistoryReport = sb.ToString
            Session("HistoryAboveNemaReport") = HistoryReport
        End With
    End Sub

    Protected Sub _ucDiagram_NextStepClicked() Handles _ucDiagram.NextStepClicked
        If Me._ucDiagram.SelectedDiagram = User_Controls_ucDiagram.DiagramView.Decision Then
            If _ucDiagram.Decision = True Then 'yes then
                NextStep(_ucDiagram.YesNextStep)
            Else
                NextStep(_ucDiagram.NoNextStep)
            End If
        ElseIf Me._ucDiagram.SelectedDiagram = User_Controls_ucDiagram.DiagramView.Input Then
            'TODO Get The New Motor Price
            NextStep(_ucDiagram.YesNextStep)
        Else
            NextStep(_ucDiagram.YesNextStep)
        End If
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SetBanner("Above NEMA/NEMA Frame Motor")
        Master.ShowPopupMenu(Nothing, 0, True)
    End Sub


    Protected Sub _ucDiagramAboveNema_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ucDiagramAboveNema.Load
        'If Not Page.IsPostBack Then
        '    With Me._ucDiagramAboveNema
        '        AboveNextStep(1) ' First Step
        '    End With
        'End If
    End Sub

    Protected Sub _ucDiagramAboveNema_PrevStepClicked() Handles _ucDiagramAboveNema.PrevStepClicked
        AboveNextStep(_ucDiagramAboveNema.PreviousStep)
    End Sub
    Protected Sub _ucDiagram_PrevStepClicked() Handles _ucDiagram.PrevStepClicked
        NextStep(_ucDiagram.PreviousStep)
    End Sub
    Protected Sub _ucDiagramAboveNema_NextStepClicked() Handles _ucDiagramAboveNema.NextStepClicked
        If Me._ucDiagramAboveNema.SelectedDiagram = User_Controls_ucDiagram.DiagramView.Decision Then
            If _ucDiagramAboveNema.Decision = True Then 'yes then
                AboveNextStep(_ucDiagramAboveNema.YesNextStep)
            Else
                AboveNextStep(_ucDiagramAboveNema.NoNextStep)
            End If
        Else
            AboveNextStep(_ucDiagramAboveNema.YesNextStep)
        End If
    End Sub

    Protected Sub _tabNEMA_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _tabNEMA.ActiveTabChanged
        If _tabNEMA.ActiveTabIndex = 0 Then
            Me._ucDiagram.EnableValidation = False
            Me._ucDiagramAboveNema.EnableValidation = True
        Else
            Me._ucDiagram.EnableValidation = True
            Me._ucDiagramAboveNema.EnableValidation = False
        End If
    End Sub


    Protected Sub _tabNEMA_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles _tabNEMA.Load
        If Not Page.IsPostBack Then
            AboveNextStep(0) ' First Step
            NextStep(0)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ' SetNextButtonJS( 1, 0)
            'SetNextButtonJS(0, 1)
            Me._tabAboveNEMA.Enabled = True
            Me._tabNEMA.ActiveTabIndex = 1
            Me._ucDiagram.EnableValidation = True
            Me._ucDiagramAboveNema.EnableValidation = False
        End If
        'Me._btnShowData.OnClientClick = String.Format("displayViewListWindow('{0}','NEMA Motors Data');return false;", Page.ResolveClientUrl("~/NEMA/NEMAData.aspx"))
    End Sub

    
   
End Class
