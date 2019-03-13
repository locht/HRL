﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Attendance.AttendanceBusiness
Imports Telerik.Web.UI
Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Telerik.Charting
Imports WebAppLog

Public Class ctrlAT_DBTimeOtByOrg
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Dashboard/" + Me.GetType().Name.ToString()
#Region "Property"
    ''' <lastupdate>
    ''' 22/08/2017 14:12
    ''' </lastupdate>
    ''' <summary>
    ''' Giá trị session StatisticType
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StatisticType As String
        Get
            Return Session(Me.ID & "_StatisticType")
        End Get
        Set(ByVal value As String)
            Session(Me.ID & "_StatisticType") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 22/08/2017 14:12
    ''' </lastupdate>
    ''' <summary>
    ''' Danh sách nghỉ bù, nghỉ phép, tổng công nghỉ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StatisticData As List(Of StatisticDTO)
        Get
            Return Session(Me.ID & "_StatisticData")
        End Get
        Set(ByVal value As List(Of StatisticDTO))
            Session(Me.ID & "_StatisticData") = value
        End Set
    End Property

#End Region

#Region "Page"
    Private width As Integer
    Private height As Integer
    Public data As String ''{name: 'Tokyo',data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]},\n
    Public categories As String
    Public enabled As String
    Public type As String
    ''' <lastupdate>
    ''' 22/08/2017 14:12
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức load các thành phần trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Request("width") IsNot Nothing Then
                width = CInt(Request("width"))
            End If
            If Request("height") IsNot Nothing Then
                height = CInt(Request("height"))
            End If
            Dim resize As Integer = 0
            If Request("resize") IsNot Nothing Then
                resize = CInt(Request("resize"))
            End If
            If Not IsPostBack Then
                Refresh(CommonMessage.ACTION_UPDATED)
            Else
                Refresh()
            End If
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 22/08/2017 14:12
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức Bind dữ liệu cho các combobox year, month, treeview tổ chức 
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            LoadYear()
            LoadMonth()
            cboYear.SelectedValue = Date.Now.Year
            If Date.Now.Month = 1 Then
                cboYear.SelectedValue = Date.Now.Year - 1
                cboMonth.SelectedValue = 12
            Else
                cboMonth.SelectedValue = Date.Now.Month - 1
            End If

            If Common.Common.OrganizationLocationDataSession Is Nothing Then
                Dim rep1 As New CommonRepository
                rep1.GetOrganizationLocationTreeView()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 22/08/2017 14:12
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức làm mới các thiết lập giá trị các control trên trang với Message mặc định là ""
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If width > 1000 Then
                btnMax.Text = "Restore"
            Else
                btnMax.Text = "Maximize"
            End If
            data = String.Empty
            If StatisticData Is Nothing Or Message = CommonMessage.ACTION_UPDATED Then
                StatisticData = rep.GetStatisticTimeOtByOrg(Utilities.ObjToInt(cboYear.SelectedValue), Utilities.ObjToInt(cboMonth.SelectedValue))
            End If
            If StatisticData.Count > 0 Then
                data = String.Empty
                'Select Case cboType.SelectedValue
                '    Case "EMP_COUNT_YEAR", "EMP_COUNT_MON"
                type = "column"
                enabled = "false"
                categories = "xAxis: { type: 'category', labels: { rotation: 0 }},"
                data = "{name: 'Tổng số giờ',data: ["
                For index As Integer = 0 To StatisticData.Count - 1
                    If index = StatisticData.Count - 1 Then
                        data &= "['" & StatisticData(index).NAME & "', " & StatisticData(index).VALUE & "]"
                    Else
                        data &= "['" & StatisticData(index).NAME & "', " & StatisticData(index).VALUE & "],"
                    End If
                Next
                data &= "], tooltip: { valueSuffix: ''}}"
            Else
                data = String.Empty
                type = "column"
                enabled = "false"
                categories = "xAxis: { type: 'category', labels: { rotation: 0 }},"
                data = "{name: 'Tổng số giờ',data: [], tooltip: { valueSuffix: ''}}"
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 22/08/2017 14:12
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện TextChanged cho control cboYear
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cboYear_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboYear.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            LoadMonth()
            Refresh(CommonMessage.ACTION_UPDATED)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
        
    End Sub
    ''' <lastupdate>
    ''' 22/08/2017 14:12
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedIndexChanged cho combobox cboMonth
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cboMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboMonth.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh(CommonMessage.ACTION_UPDATED)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub


#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 22/08/2017 14:12
    ''' </lastupdate>
    ''' <summary>
    ''' Phương phức load dữ liệu cho combobox cboYear
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadYear()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim table As New DataTable
            table.Columns.Add("YEAR", GetType(Integer))
            table.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow
            For index = 2015 To Date.Now.Year + 1
                row = table.NewRow
                row("ID") = index
                row("YEAR") = index
                table.Rows.Add(row)
            Next
            FillRadCombobox(cboYear, table, "YEAR", "ID")
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 22/08/2017 14:12
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức load dữ liệu cho combobox cboMonth
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadMonth()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim tableMonth As New DataTable
            tableMonth.Columns.Add("MONTH", GetType(String))
            tableMonth.Columns.Add("ID", GetType(String))
            Dim rowMonth As DataRow
            For index = 1 To 13
                rowMonth = tableMonth.NewRow

                If index = 0 Then
                    rowMonth("ID") = 0
                    rowMonth("MONTH") = ""
                    tableMonth.Rows.Add(rowMonth)
                ElseIf index = 13 Then
                    rowMonth("ID") = index
                    rowMonth("MONTH") = "Tất cả"
                    tableMonth.Rows.Add(rowMonth)
                Else
                    rowMonth("ID") = index
                    rowMonth("MONTH") = index
                    tableMonth.Rows.Add(rowMonth)
                End If
            Next
            FillRadCombobox(cboMonth, tableMonth, "MONTH", "ID")
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
       

    End Sub

#End Region
End Class