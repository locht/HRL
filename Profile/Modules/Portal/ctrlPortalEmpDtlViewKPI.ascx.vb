﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlPortalEmpDtlViewKPI
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase

#Region "Property"
    Public Property GridList As List(Of EmployeeAssessmentDTO)
        Get
            Return PageViewState(Me.ID & "_GridList")
        End Get
        Set(ByVal value As List(Of EmployeeAssessmentDTO))
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
                GirdConfig(rgTraining)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileBusinessRepository
        Try
            rgTraining.SetFilter()
            SetValueObjectByRadGrid(rgTraining, New EmployeeAssessmentDTO)

            'Dim objEmployeeTrain As New EmployeeAssessmentDTO
            'objEmployeeTrain.EMPLOYEE_ID = EmployeeID

            If Not IsPostBack Then
                GridList = rep.GetAssessKPIEmployee(EmployeeID)
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                If Message = CommonMessage.ACTION_SAVED Then
                    GridList = rep.GetAssessKPIEmployee(EmployeeID)
                End If
            End If

            'Đưa dữ liệu vào Grid
            If Me.GridList IsNot Nothing Then
                rgTraining.DataSource = Me.GridList
                rgTraining.DataBind()
            Else
                rgTraining.DataSource = New List(Of EmployeeAssessmentDTO)
                rgTraining.DataBind()
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

    Private Sub rgCommend_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgTraining.NeedDataSource
        Try
            If IsPostBack Then Exit Sub
            SetValueObjectByRadGrid(rgTraining, New EmployeeAssessmentDTO)

            Dim rep As New ProfileBusinessRepository
            'Dim objEmployeeTrain As New EmployeeAssessmentDTO
            'objEmployeeTrain.EMPLOYEE_ID = EmployeeID

            GridList = rep.GetAssessKPIEmployee(EmployeeID)
            rgTraining.DataSource = GridList

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

#End Region
End Class