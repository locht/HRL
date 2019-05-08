Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlPortalConcurrently
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase

#Region "Property"
    Public Property GridList As List(Of TitleConcurrentDTO)
        Get
            Return PageViewState(Me.ID & "_GridList")
        End Get
        Set(ByVal value As List(Of TitleConcurrentDTO))
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

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            If Not IsPostBack Then
                GirdConfig(rgDiscipline)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileBusinessRepository
        Try
            rgDiscipline.SetFilter()
            SetValueObjectByRadGrid(rgDiscipline, New TitleConcurrentDTO)

            If Not IsPostBack Then
                GridList = rep.GetConcurrentlyProccess(EmployeeID)
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                If Message = CommonMessage.ACTION_SAVED Then
                    GridList = rep.GetConcurrentlyProccess(EmployeeID)
                End If
            End If

            'Đưa dữ liệu vào Grid
            If Me.GridList IsNot Nothing Then
                rgDiscipline.DataSource = Me.GridList
                rgDiscipline.DataBind()
            Else
                rgDiscipline.DataSource = New List(Of TitleConcurrentDTO)
                rgDiscipline.DataBind()
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

    Private Sub rgDiscipline_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgDiscipline.NeedDataSource
        Try
            If IsPostBack Then Exit Sub
            SetValueObjectByRadGrid(rgDiscipline, New TitleConcurrentDTO)

            Dim rep As New ProfileBusinessRepository
            GridList = rep.GetConcurrentlyProccess(EmployeeID)
            rgDiscipline.DataSource = GridList

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

#End Region
End Class