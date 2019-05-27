﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlPortalWorking
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase

#Region "Property"
    Public Property GridList As List(Of WorkingDTO)
        Get
            Return PageViewState(Me.ID & "_GridList")
        End Get
        Set(ByVal value As List(Of WorkingDTO))
            PageViewState(Me.ID & "_GridList") = value
        End Set
    End Property
    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
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
    ''' 06/07/2017 18:11
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Xét các trạng thái của grid rgContract
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Not IsPostBack Then
                GirdConfig(rgWorking)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileBusinessRepository
        Try
            rgWorking.SetFilter()
            SetValueObjectByRadGrid(rgWorking, New WorkingDTO)

            If Not IsPostBack Then
                GridList = rep.GetWorkingProccess(EmployeeID)
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                If Message = CommonMessage.ACTION_SAVED Then
                    GridList = rep.GetWorkingProccess(EmployeeID)
                End If
            End If

            'Đưa dữ liệu vào Grid
            If Me.GridList IsNot Nothing Then
                rgWorking.DataSource = Me.GridList
                rgWorking.DataBind()
            Else
                rgWorking.DataSource = New List(Of WorkingDTO)
                rgWorking.DataBind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

    Private Sub rgWorking_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgWorking.NeedDataSource
        Try
            If IsPostBack Then Exit Sub

            Dim rep As New ProfileBusinessRepository
            GridList = rep.GetWorkingProccess(EmployeeID)
            rgWorking.DataSource = GridList

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

#End Region
End Class