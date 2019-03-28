Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports AjaxControlToolkit

<WebService(Namespace:="http://RI/")> _
<System.Web.Script.Services.ScriptService()> _
Public Class NEMA

    <Services.WebMethod(cacheduration:=180), Script.Services.ScriptMethod()> _
    Public Function GetHP(ByVal knownCategoryValues As String, ByVal category As String, ByVal contextKey As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim dr As System.Data.DataTableReader = Nothing

        Try
            If contextKey Is Nothing Then
                Return Nothing
                Exit Function
            End If

            dr = clsNEMAMotor.PopulateHP(contextKey)
            If dr.HasRows Then
                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("HP"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("HP"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
                values.Add(New CascadingDropDownNameValue("> 200", 201))
            End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetHP", , ex)
        Finally
            GetHP = values.ToArray            
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
        End Try

    End Function
    <Services.WebMethod(cacheduration:=180), Script.Services.ScriptMethod()> _
       Public Function GetRPM(ByVal knownCategoryValues As String, ByVal category As String, ByVal contextKey As String) As CascadingDropDownNameValue()
        Dim values As New Generic.List(Of CascadingDropDownNameValue)
        Dim dr As System.Data.DataTableReader = Nothing
        Dim knownCategoryValuesDictionary As New StringDictionary
        Dim HP As String = String.Empty
        Try
            If contextKey Is Nothing Then
                Return Nothing
                Exit Function
            End If
            knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
            
            If knownCategoryValuesDictionary.ContainsKey("HP") Then
                HP = RI.SharedFunctions.DataClean(knownCategoryValuesDictionary("HP"))
            End If
            dr = clsNEMAMotor.PopulateRPM(contextKey, HP)
            If dr.HasRows Then
                'values.Add(New CascadingDropDownNameValue("<900", "0", False))
                Do While dr.Read()
                    Dim val As String = RI.SharedFunctions.DataClean(dr.Item("Speed"))
                    Dim desc As String = RI.SharedFunctions.DataClean(dr.Item("Speed"))
                    values.Add(New CascadingDropDownNameValue(desc, val))
                Loop
                values.Add(New CascadingDropDownNameValue("< 900", 899))
            End If


        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetRPM", , ex)
        Finally
            GetRPM = values.ToArray
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
        End Try

    End Function
    Public Class MotorPrice
        Public Price As Decimal
        Public ClientID As String
        Public Efficiency As Decimal
        Public Efficiency900 As Decimal
    End Class
    <Services.WebMethod(cacheduration:=0), Script.Services.ScriptMethod()> _
      Public Function GetNewMotorPrice(ByVal MotorType As String, ByVal HP As Decimal, ByVal RPM As Integer, ByVal ClientID As String) As MotorPrice
        Dim dr As Devart.Data.Oracle.OracleDataReader = Nothing
        Dim price As Decimal = 0
        Dim efficiency As Decimal = 0
        Dim mp As New MotorPrice
        Try
            dr = clsNEMAMotor.GetNewMotorPrice(MotorType, HP, RPM)
            If dr.HasRows Then
                Do While dr.Read()
                    mp.Price = RI.SharedFunctions.DataClean(dr.Item("NetPrice"))
                    mp.Efficiency = RI.SharedFunctions.DataClean(dr.Item("Efficiency"))
                    mp.Efficiency900 = RI.SharedFunctions.DataClean(dr.Item("OLDEFFICIENCY"))
                Loop
            End If

            'dr = clsNEMAMotor.GetEfficiency(MotorType, HP, RPM)
            'If dr.HasRows Then
            '    Do While dr.Read()
            '        efficiency = RI.SharedFunctions.DataClean(dr.Item("Efficiency"))
            '    Loop
            'End If

        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetNewMotorPrice", , ex)
        Finally
            mp.ClientID = ClientID
            GetNewMotorPrice = mp
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
        End Try

    End Function
End Class
