﻿Imports Common
Imports Framework.UI.Utilities
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
End Class