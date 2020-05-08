Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlPortalEmpHealth
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase

#Region "Property"
    Public Property GridList As DataTable
        Get
            Return PageViewState(Me.ID & "_GridList")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_GridList") = value
        End Set
    End Property
    Public Property EmployeeID As Decimal
#End Region

#Region "Page"
    Public Overrides Property MustAuthorize As Boolean = False
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Xét các trạng thái của grid rgHealth
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileStoreProcedure
        Try
            rgHealth.SetFilter()
            SetValueObjectByRadGrid(rgHealth, New ContractDTO)

            If Not IsPostBack Then
                GridList = rep.GET_HEALTH_BY_ID(EmployeeID)
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                If Message = CommonMessage.ACTION_SAVED Then
                    GridList = rep.GET_HEALTH_BY_ID(EmployeeID)
                End If
            End If

            'Đưa dữ liệu vào Grid
            If Me.GridList IsNot Nothing Then
                rgHealth.DataSource = Me.GridList
                rgHealth.DataBind()
            Else
                rgHealth.DataSource = New List(Of ContractDTO)
                rgHealth.DataBind()
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

    Private Sub rgHealth_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgHealth.NeedDataSource
        Try
            If IsPostBack Then Exit Sub
            SetValueObjectByRadGrid(rgHealth, New ContractDTO)

            Dim rep As New ProfileStoreProcedure
            GridList = rep.GET_HEALTH_BY_ID(EmployeeID)
            rgHealth.DataSource = GridList

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

#End Region
End Class