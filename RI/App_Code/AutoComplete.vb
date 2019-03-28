Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Collections.Generic


''' <summary>
''' AutoComplete is an ASP.NET AJAX extender that can be attached to any TextBox control, 
''' and will associate that control with a popup panel to display words that begin with the 
''' prefix typed into the textbox.  The dropdown with candidate words supplied by a 
''' web service is positioned on the bottom left of the text box. 
''' </summary>
''' <remarks>Example Code - http://asp.net/ajax/control-toolkit/live/</remarks>
<WebService(Namespace:="http://RI/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<System.Web.Script.Services.ScriptService()> _
Public Class AutoComplete
    Inherits System.Web.Services.WebService

    ''' <summary>
    ''' Web Service for the AutoComplete functionality for the Functional Location List
    ''' </summary>
    ''' <param name="prefixText">Contains the text that will be used for the search query</param>
    ''' <param name="count">Max items to return</param>
    ''' <param name="contextKey">Delimited string containing the SiteID @@ Business Unit @@ Area @@ Line</param>
    ''' <returns>Array of strings</returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=180), Script.Services.ScriptMethod()> _
    Public Function GetListOfEquipmentIDs(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
        'TODO: Convert embeded sql to an Oracle Procedure
        Dim items As New List(Of String)
        Dim dr As Devart.Data.Oracle.OracleDataReader = Nothing
        Dim sql As String = "select * from (Select distinct rcfaflid,a.siteid,b.sitename,equipmentid,equipmentdesc FROM RefEQUIPMENT a, refsite b, refsitearea c WHERE a.siteid = b.siteid and a.siteid = c.siteid {2}  and rcfaflid<>' ' and  equipmentid like'{0}%' order by b.sitename,equipmentid) where rownum<{1}"
        Dim site As String = " and a.siteid='{0}' "
        Dim businessUnit As String = " and RISuperArea='{0}' "
        Dim area As String = " and SubArea='{0}' "
        Dim line As String = " and Area='{0}' "

        Try
            If count = 0 Then count = 10
            If contextKey.Length > 0 Then
                Dim contextList As String() = Split(contextKey, "@@")
                If contextList.Length = 4 Then
                    If contextList(0).Length > 0 Then
                        site = String.Format(site, contextList(0))
                    Else
                        site = String.Empty
                    End If
                    If contextList(1).Length > 0 Then
                        businessUnit = String.Format(businessUnit, contextList(1))
                    Else
                        businessUnit = String.Empty
                    End If
                    If contextList(2).Length > 0 Then
                        area = String.Format(area, contextList(2))
                    Else
                        area = String.Empty
                    End If
                    If contextList(3).Length > 0 Then
                        line = String.Format(line, contextList(3))
                    Else
                        line = String.Empty
                    End If

                End If
            Else
                site = String.Empty
            End If

            sql = String.Format(sql, prefixText, count, site & businessUnit & area & line)
            dr = RI.SharedFunctions.GetOracleDataReader(sql)
            If dr IsNot Nothing Then
                Do While dr.Read
                    items.Add(dr.Item("equipmentid") & "  *  " & dr.Item("equipmentdesc"))
                Loop
            End If

        Catch ex As Exception
            Throw
        Finally
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
            GetListOfEquipmentIDs = items.ToArray
        End Try

    End Function

    ''' <summary>
    ''' Web Service for the AutoComplete functionality for the Functional Location List
    ''' </summary>
    ''' <param name="prefixText"></param>
    ''' <param name="count">Max items to return</param>
    ''' <param name="contextKey">Delimited string containing the SiteID @@ Business Unit @@ Area @@ Line</param>
    ''' <returns>Array of strings</returns>
    ''' <remarks></remarks>
    <Services.WebMethod(cacheduration:=180), Script.Services.ScriptMethod()> _
        Public Function GetListOfEquipmentDescriptions(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
        'TODO: Embeded SQL should be move to a package
        Dim items As New List(Of String)
        Dim dr As Devart.Data.Oracle.OracleDataReader = Nothing
        Dim sql As String = "Select * from (Select distinct rcfaflid,a.siteid,b.sitename,equipmentid,equipmentdesc FROM RefEQUIPMENT a, refsite b, refsitearea c WHERE a.siteid = b.siteid and a.siteid = c.siteid {2}  and rcfaflid<>' ' and  upper(equipmentdesc) like upper('%{0}%') order by b.sitename,equipmentid) where  rownum<{1} "
        Dim site As String = " and a.siteid='{0}' "
        Dim businessUnit As String = " and RISuperArea='{0}' "
        Dim area As String = " and SubArea='{0}' "
        Dim line As String = " and Area='{0}' "

        Try
            If count = 0 Then count = 10
            If contextKey.Length > 0 Then
                Dim contextList As String() = Split(contextKey, "@@")
                If contextList.Length = 4 Then
                    If contextList(0).Length > 0 Then
                        site = String.Format(site, contextList(0))
                    Else
                        site = String.Empty
                    End If
                    If contextList(1).Length > 0 Then
                        businessUnit = String.Format(businessUnit, contextList(1))
                    Else
                        businessUnit = String.Empty
                    End If
                    If contextList(2).Length > 0 Then
                        area = String.Format(area, contextList(2))
                    Else
                        area = String.Empty
                    End If
                    If contextList(3).Length > 0 Then
                        line = String.Format(line, contextList(3))
                    Else
                        line = String.Empty
                    End If

                End If
            Else
                site = String.Empty
            End If
            sql = String.Format(sql, prefixText, count, site & businessUnit & area & line)
            dr = RI.SharedFunctions.GetOracleDataReader(sql)
            If dr IsNot Nothing Then
                Do While dr.Read
                    items.Add(dr.Item("equipmentdesc") & "  *  " & dr.Item("equipmentid"))
                Loop
            End If

        Catch ex As Exception
            Throw
        Finally
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
            GetListOfEquipmentDescriptions = items.ToArray
        End Try
    End Function
End Class
