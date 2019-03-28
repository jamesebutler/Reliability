Option Explicit On
Option Strict On

Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Namespace RI
    ''' <summary>
    ''' This class contains the properties for the Report Parameters
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class ReportParameter
        Private m_name As String
        Private m_value As String

        ''' <summary>
        ''' Gets or sets the name of the parameter
        ''' </summary>
        ''' <value>String - the name of the parameter</value>
        ''' <returns>String - the name of the parameter</returns>
        ''' <remarks></remarks>
        Public Property name() As String
            Get
                Return m_name
            End Get
            Set(ByVal Value As String)
                m_name = Value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the value of the parameter
        ''' </summary>
        ''' <value>String - The value of the parameter</value>
        ''' <returns>String - The value of the parameter</returns>
        ''' <remarks></remarks>
        Public Property value() As String
            Get
                Return m_value
            End Get
            Set(ByVal Value As String)
                m_value = Value
            End Set
        End Property

        ''' <summary>
        ''' Creates a new instance of the ReportParameter class
        ''' </summary>
        ''' <param name="name">String - The name of the parameter</param>
        ''' <param name="value">String - The value of the parameter</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal name As String, ByVal value As String)
            m_name = name
            m_value = value
        End Sub

    End Class

    ''' <summary>
    ''' This class is a collection of report parameters
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class ReportParameters
        Inherits CollectionBase
        Implements IList, ICollection, IEnumerable

        ''' <summary>
        ''' Gets the Report Parameter at the specified index.
        ''' </summary>
        ''' <param name="index">Int32 - The zero-based index of the Report Parameter. </param>
        ''' <value></value>
        ''' <returns>ReportParameter - Gets the Report Parameter at the specified index.</returns>
        ''' <remarks></remarks>
        Default Public ReadOnly Property Item(ByVal index As Int32) As ReportParameter
            Get
                Return CType(List.Item(index), ReportParameter)
            End Get
        End Property

        ''' <summary>
        ''' Adds an item to the Report Parameter collection.
        ''' </summary>
        ''' <param name="Item">ReportParameter - The item that will be added as an Report Parameter</param>
        ''' <returns>Integer - The position into which the new element was inserted</returns>
        ''' <remarks></remarks>
        Public Function Add(ByVal Item As ReportParameter) As Integer
            Return List.Add(Item)
        End Function

        ''' <summary>
        ''' Removes an item from the Report Parameter collection
        ''' </summary>
        ''' <param name="index">Int32 - The index of the item that will be removed from the collection</param>
        ''' <remarks></remarks>
        Public Sub Remove(ByVal index As Int32)
            ' Check to see if there is a widget at the supplied index.
            If index > Count - 1 Or index < 0 Then
                ' If no index exists, the operation is cancelled.            
            Else
                ' Invokes the RemoveAt method of the List object.
                List.RemoveAt(index)
            End If
        End Sub

        ''' <summary>
        ''' Sorts the elements in the entire Report Parameter collection 
        ''' using the System.IComparable implementation of each element.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Sort()
            InnerList.Sort()
        End Sub

        ''' <summary>
        ''' Searches for the report parameter name and returns the 
        ''' zero-based index of the first occurrence within the entire 
        ''' Report Parameter collection
        ''' </summary>
        ''' <param name="name">String - The name of the report parameter to find</param>
        ''' <returns>Integer - The index of the item that will be found in the collection</returns>
        ''' <remarks></remarks>
        Public Overloads Function IndexOf(ByVal name As String) As Integer
            Dim index As Integer = 0
            Dim item As ReportParameter

            ' Brute force
            For Each item In Me
                If item.name = name Then
                    Return index
                End If
                index += 1
            Next
            Return -1
        End Function

        ''' <summary>
        ''' Determines whether an element is in the Report Parameter collection.
        ''' </summary>
        ''' <param name="name">String - The name of the report parameter to find</param>
        ''' <returns>Boolean - true if item is found in the Report Parameter collection; otherwise, false.</returns>
        ''' <remarks></remarks>
        Public Overloads Function Contains(ByVal name As String) As Boolean
            Return (-1 <> IndexOf(name))
        End Function

        ''' <summary>
        ''' Returns an enumerator for the entire Report Parameter collection.
        ''' </summary>
        ''' <returns>IEnumerator - Returns an enumerator for the entire Report Parameter collection.</returns>
        ''' <remarks></remarks>
        Public Overloads Function GetEnumerator() As IEnumerator
            Return Me.InnerList.GetEnumerator
        End Function

    End Class



End Namespace