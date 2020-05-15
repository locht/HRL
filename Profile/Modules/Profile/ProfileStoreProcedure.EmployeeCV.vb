﻿Imports Common
Imports Framework.UI.Utilities
Imports System.Web.UI
Imports Telerik.Web.UI
Imports System.Reflection
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities

Partial Class ProfileStoreProcedure
#Region "EMAIL"
    Public Function GET_DATA_CONTRACT_FOR_EMAIL(ByVal contract_id As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_DASHBOARD.GET_DATA_CONTRACT_FOR_EMAIL", New List(Of Object)(New Object() {contract_id}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GET_MAIL_TEMPLATE(ByVal code As String, ByVal group As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_RECRUITMENT_EXPORT.GET_TEMPLATE_MAIL", New List(Of Object)(New Object() {code, group}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GET_MAILCC_DIRECT_HR(ByVal emp_id As Integer) As String
        Dim lstObj As New List(Of Object)()
        lstObj = hfr.ExecuteStoreScalar("PKG_RECRUITMENT_EXPORT.GET_MAILCC_DIRECT_HR", New List(Of Object)(New Object() {emp_id, LogHelper.CurrentUser.EMPLOYEE_ID, OUT_STRING}))
        If lstObj IsNot Nothing Then
            Return lstObj(0)
        Else
            Return String.Empty
        End If
    End Function
#End Region
#Region "Thong tin quyet dinh"
    Public Function INSERT_WORKING_BY_REMINDER(ByVal P_LSTID As String) As Int32
        Dim objects = hfr.ExecuteStoreScalar("PKG_HU_IPROFILE_WORKING.INSERT_WORKING_BY_REMINDER", _
                                             New List(Of Object)(New Object() {P_LSTID, OUT_NUMBER}))
        Return Int32.Parse(objects(0).ToString())
    End Function
#End Region
#Region "Lay thong tin thong ke nhanh"
    Public Function GET_INFO_REMINDER(ByVal username As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_DASHBOARD.GET_INFO_REMINDER", _
                                                 New List(Of Object)(New Object() {username.ToUpper}))
        If ds IsNot Nothing Then
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function

    Public Function GET_LIST_INFO_REMINDER(ByVal username As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_DASHBOARD.GET_LIST_INFO_REMINDER", _
                                                 New List(Of Object)(New Object() {username.ToUpper}))
        If ds IsNot Nothing Then
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function

    Public Function GET_JOB_POSSITION_BY_TITLE_ORG(ByVal _title_ID As Decimal, ByVal _org_id As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_BUSINESS.GET_JOB_POSSITION_BY_TITLE_ORG", _
                                                 New List(Of Object)(New Object() {_title_ID, _org_id}))
        If ds IsNot Nothing Then
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function

    Public Function GET_JOB_DESCRIPTION_BY_TITLE_ORG(ByVal _title_ID As Decimal, ByVal _org_id As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_BUSINESS.GET_JOB_DESCRIPTION_BY_TITLE_ORG", _
                                                 New List(Of Object)(New Object() {_title_ID, _org_id}))
        If ds IsNot Nothing Then
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function

    Public Function GET_DIRECT_MANAGER_BY_JOB_POS(ByVal _job_pos_ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_BUSINESS.GET_DIRECT_MANAGER_BY_JOB_POS", _
                                                 New List(Of Object)(New Object() {_job_pos_ID}))
        If ds IsNot Nothing Then
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function
#End Region
    Public Function Import_HoSoLuong(ByVal P_USER As String, ByVal P_DOCXML As String) As Boolean
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.IMPORT_HOSOLUONG", New List(Of Object)(New Object() {P_USER, P_DOCXML}))
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
            Return CBool(ds.Tables(0)(0)(0))
        End If
    End Function
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

#Region "Quản lý file"

#End Region
End Class