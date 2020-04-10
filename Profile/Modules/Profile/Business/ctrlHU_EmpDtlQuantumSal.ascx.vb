Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlHU_EmpDtlQuantumSal
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    'Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"
    Public Property dtData As DataTable
        Get
            Return PageViewState(Me.ID & "_DtData")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_DtData") = value
        End Set
    End Property
    Public Property EmployeeID As Decimal
    'Thông tin cơ bản của nhân viên.
    Property EmployeeInfo As EmployeeDTO
        Get
            Return PageViewState(Me.ID & "_EmployeeInfo")
        End Get
        Set(ByVal value As EmployeeDTO)
            PageViewState(Me.ID & "_EmployeeInfo") = value
        End Set
    End Property
    'Lưu lại đang ở View nào để load khi dùng nút Next và Previous để chuyển sang xem thông tin nhân viên khác.
    Public Property CurrentPlaceHolder As String
        Get
            Return PageViewState(Me.ID & "_CurrentPlaceHolder")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_CurrentPlaceHolder") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ctrlEmpBasicInfo.SetProperty("EmployeeInfo", EmployeeInfo)
            Refresh()
            Me.CurrentPlaceHolder = Me.ViewName
            ctrlEmpBasicInfo.SetProperty("CurrentPlaceHolder", Me.CurrentPlaceHolder)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgGrid.SetFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")


        Try
            Dim rep As New ProfileBusinessRepository
            If EmployeeInfo IsNot Nothing Then
                EmployeeID = EmployeeInfo.ID
                dtData = rep.GetSalaryChanged(EmployeeID)
            End If

            'Đưa dữ liệu vào Grid
            If Me.dtData IsNot Nothing Then
                rgGrid.DataSource = Me.dtData
                rgGrid.DataBind()
            Else
                rgGrid.DataSource = New List(Of CommendDTO)
                rgGrid.DataBind()
            End If
            rep.Dispose()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

    Private Sub rgGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGrid.NeedDataSource
        Try
            If IsPostBack Then Exit Sub

            Dim rep As New ProfileBusinessRepository
            rgGrid.DataSource = rep.GetSalaryChanged(EmployeeID)
            rep.Dispose()
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

#End Region
End Class