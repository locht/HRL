﻿Imports Common
Imports Framework.UI.Utilities
Imports System.Web.UI
Imports Telerik.Web.UI
Imports System.Reflection
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities

Partial Class ProfileStoreProcedure
    Public Function EmployeeCV_GetInfo(ByVal employeeId As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.READ_EMPLOYEE_CV", New List(Of Object)(New Object() {employeeId}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    ' In cv nhan vien
    Public Function PRINT_CV(ByVal ID As Decimal?) As DataSet
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.PRINT_CV", _
                                                 New List(Of Object)(New Object() {ID}))
        Return ds
    End Function
    ' Chuyen trang thai control
    Public Sub DisableControls(ByVal control As System.Web.UI.Control, ByVal status As Boolean)
        For Each c As System.Web.UI.Control In control.Controls
            If TypeOf c Is RadTabStrip Then
            Else
                ' Get the Enabled property by reflection.
                Dim type As Type = c.GetType
                Dim prop As PropertyInfo = type.GetProperty("Enabled")

                ' Set it to False to disable the control.
                If Not prop Is Nothing Then
                    prop.SetValue(c, status, Nothing)
                End If
                ' Recurse into child controls.
                If c.Controls.Count > 0 Then
                    Me.DisableControls(c, status)
                End If
            End If

        Next
    End Sub
    ' Clear data control
    Public Sub ResetControlValue(ByVal control As System.Web.UI.Control)
        For Each ctrl As System.Web.UI.Control In control.Controls
            If TypeOf ctrl Is RadComboBox Then
                TryCast(ctrl, RadComboBox).SelectedValue = Nothing

                TryCast(ctrl, RadComboBox).Text = ""
            ElseIf TypeOf ctrl Is TextBox Then
                TryCast(ctrl, TextBox).Text = ""
            ElseIf TypeOf ctrl Is RadTextBox Then
                TryCast(ctrl, RadTextBox).Text = ""
            ElseIf TypeOf ctrl Is RadNumericTextBox Then
                TryCast(ctrl, RadNumericTextBox).Value = 0
            ElseIf TypeOf ctrl Is RadDatePicker Then
                TryCast(ctrl, RadDatePicker).SelectedDate = Nothing
            End If
            ' Recurse into child controls.
            If ctrl.Controls.Count > 0 Then
                Me.ResetControlValue(ctrl)
            End If
        Next
    End Sub
End Class