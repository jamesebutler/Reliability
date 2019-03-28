Imports Microsoft.VisualBasic
Imports Devart.Data.Oracle
Imports System.IO

<Serializable()> _
Public Class clsMOCViewSearch
    Public Property Division() As String
        Get
            Return mDivision
        End Get
        Set(ByVal value As String)
            mDivision = value
        End Set
    End Property
    Public Property Facility() As String
        Get
            Return mFacility
        End Get
        Set(ByVal value As String)
            mFacility = value
        End Set
    End Property
    Public Property BusinessUnit() As String
        Get
            Return mBusinessUnit
        End Get
        Set(ByVal value As String)
            mBusinessUnit = value
        End Set
    End Property
    Public Property Area() As String
        Get
            Return mArea
        End Get
        Set(ByVal value As String)
            mArea = value
        End Set
    End Property
    Public Property Line() As String
        Get
            Return mLine
        End Get
        Set(ByVal value As String)
            mLine = value
        End Set
    End Property
    Public Property LineBreak() As String
        Get
            Return mLineBreak
        End Get
        Set(ByVal value As String)
            mLineBreak = value
        End Set
    End Property
    Public Property StartDate() As String
        Get
            Return mStartDate
        End Get
        Set(ByVal value As String)
            mStartDate = value
        End Set
    End Property
    Public Property EndDate() As String
        Get
            Return mEndDate
        End Get
        Set(ByVal value As String)
            mEndDate = value
        End Set
    End Property
    Public Property DateRange() As String
        Get
            Return mDateRange
        End Get
        Set(ByVal value As String)
            mDateRange = value
        End Set
    End Property
    Public Property Type() As String
        Get
            Return mType
        End Get
        Set(ByVal value As String)
            mType = value
        End Set
    End Property

    Public Property Planner() As String
        Get
            Return mPlanner
        End Get
        Set(ByVal value As String)
            mPlanner = value
        End Set
    End Property
    Public Property Category() As String
        Get
            Return mCategory
        End Get
        Set(ByVal value As String)
            mCategory = value
        End Set
    End Property
    Public Property SubCategory() As String
        Get
            Return mSubCategory
        End Get
        Set(ByVal value As String)
            mSubCategory = value
        End Set
    End Property
    Public Property Classification() As String
        Get
            Return mClassification
        End Get
        Set(ByVal value As String)
            mClassification = value
        End Set
    End Property
    Public Property Initiator() As String
        Get
            Return mInitiator
        End Get
        Set(ByVal value As String)
            mInitiator = value
        End Set
    End Property
    Public Property Owner() As String
        Get
            Return mMOCOwner
        End Get
        Set(ByVal value As String)
            mMOCOwner = value
        End Set
    End Property
    Public Property Status() As String
        Get
            Return mStatus
        End Get
        Set(ByVal value As String)
            mStatus = value
        End Set
    End Property
    Public Property MOCNumber() As String
        Get
            Return mMOCNumber
        End Get
        Set(ByVal value As String)
            mMOCNumber = value
        End Set
    End Property
    Public Property Title() As String
        Get
            Return mTitle
        End Get
        Set(ByVal value As String)
            mTitle = value
        End Set
    End Property

    Public Property Username() As String
        Get
            Return mUsername
        End Get
        Set(ByVal value As String)
            mUsername = value
        End Set
    End Property

    Public Property OrderBy() As String
        Get
            Return mOrderBy
        End Get
        Set(ByVal value As String)
            mOrderBy = value
        End Set
    End Property

    Public Property SearchData() As System.Data.DataTableReader
        Get
            Return mSearchData
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mSearchData = value
        End Set
    End Property
    Public Property ExcelData() As System.Data.DataTableReader
        Get
            Return mSearchData
        End Get
        Set(ByVal value As System.Data.DataTableReader)
            mSearchData = value
        End Set
    End Property
    Public Function Search() As System.Data.DataTableReader
        'Perform Search 
        GetMOCListing()
        Return SearchData
    End Function
    Private Sub GetMOCListing()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = "N"

        'Check input paramaters

        Try

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Facility
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Division"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Division
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_BusinessUnit"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = BusinessUnit
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Area"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Area
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Line"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Line
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_LineBreak"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = LineBreak
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_StartDate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = StartDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_EndDate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = EndDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Type"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Type
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Planner"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Planner
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Category"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Category
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_SubCategory"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = SubCategory
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Classification"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Classification
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_OrderBy"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = OrderBy
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Initiator"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Initiator
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Status"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Status
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_MOCNumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = MOCNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_MOCOwner"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Owner
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Title"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Title
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "RS"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "MOCViewSearch_" & Facility & "_" & Division & "_" & BusinessUnit & "_" & Area & "_" & Line & "_" & Planner & "_" & Type & "_" & Category & "_" & Classification & "_" & StartDate & "_" & EndDate & "_" & OrderBy
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.ViewMOC.MOCListing", key, 0)
            If ds IsNot Nothing Then

                If ds.Tables.Count = 1 Then
                    Me.SearchData = ds.Tables(0).CreateDataReader
                Else
                    SearchData = Nothing
                End If
            End If
            If Me.SearchData.HasRows = False Then
                HttpRuntime.Cache.Remove(key)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function ExcelSearch() As System.Data.DataTableReader
        GetExcelListing()
        Return ExcelData
    End Function
    Private Sub GetExcelListing()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = "N"

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Facility
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Division"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Division
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_BusinessUnit"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = BusinessUnit
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Area"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Area
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Line"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Line
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_LineBreak"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = LineBreak
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_StartDate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = StartDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_EndDate"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = EndDate
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Type"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Type
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Planner"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Planner
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Category"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Category
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Classification"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Classification
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_OrderBy"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = OrderBy
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Initiator"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Initiator
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Status"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Status
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_MOCNumber"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = MOCNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_MOCOwner"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Owner
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Title"
            param.OracleDbType = OracleDbType.NVarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Title
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "RS"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "MOCExcelSearch_" & Facility & "_" & Division & "_" & BusinessUnit & "_" & Area & "_" & Line & "_" & LineBreak & "_" & StartDate & "_" & EndDate & "_" & Type & "_" & Category & "_" & Classification & "_" & OrderBy & "_" & Status
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.ViewMOC.MOCExcelListing", key, 1)
            If ds IsNot Nothing Then
                If ds.Tables.Count = 1 Then
                    Me.ExcelData = ds.Tables(0).CreateDataReader
                Else
                    ExcelData = Nothing
                End If
            End If
            If Me.ExcelData.HasRows = False Then
                HttpRuntime.Cache.Remove(key)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private mSearchData As System.Data.DataTableReader
    Private mDivision As String
    Private mFacility As String
    Private mBusinessUnit As String
    Private mArea As String
    Private mLine As String
    Private mLineBreak As String
    Private mStartDate As String
    Private mEndDate As String
    Private mDateRange As String
    Private mType As String
    Private mPlanner As String
    Private mCategory As String
    Private mSubCategory As String
    Private mClassification As String
    Private mOrderBy As String
    Private mSelect As String
    Private mInitiator As String
    Private mStatus As String
    Private mMOCNumber As String
    Private mMOCOwner As String
    Private mTitle As String
    Private mUsername As String

End Class
<Serializable()> _
Public Class clsViewMOCDDL

    Private mInitiator As New clsData
    Public ReadOnly Property Initiator() As clsData
        Get
            Return mInitiator
        End Get
    End Property
    Public ReadOnly Property Status() As clsData
        Get
            Return mStatus
        End Get
    End Property
    Private mStatus As New clsData
    Public Property Facility() As String
        Get
            Return mFacility
        End Get
        Set(ByVal value As String)
            mFacility = value
        End Set
    End Property
    Private mFacility As String

    Private Sub GetData(Optional ByVal facility As String = "")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty

        'Check input paramaters

        Try
            param = New OracleParameter
            param.ParameterName = "in_Siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = facility
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsInitiator"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsStatus"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "ViewMOC.Dropdownddl" ' & facility & "_" & division & "_" & inActiveFlag & "_" & userName
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.ViewMOC.ViewDropdownddl", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    Initiator.DataSource = ds.Tables(0).CreateDataReader
                    Initiator.DataTextField = "initiator"
                    Initiator.DataValueField = "username"

                    Status.DataSource = ds.Tables(1).CreateDataReader
                    Status.DataTextField = "status"
                    Status.DataValueField = "status"

                End If
            End If

        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
            paramCollection = Nothing
        End Try
    End Sub
    Public Sub New()
        'GetData(Facility)
    End Sub
    Public Sub GetDDLData()
        GetData(Facility)
    End Sub

End Class

