﻿Imports Framework.UI
Imports Common.CommonBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlUserFunctionAddEdit
    Inherits CommonView
    Public Property UserID As Decimal
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Trạng thái hiển thị trang
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ViewShowed As Boolean
        Get
            Return ViewState(Me.ID & "_ViewShowed")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_ViewShowed") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Mã tài khoản người dùng cũ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserID_Old As Decimal
        Get
            Return ViewState(Me.ID & "_UserID_Old")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_UserID_Old") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' UserFunction
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserFunction As List(Of FunctionDTO)
        Get
            Return ViewState(Me.ID & "_UserFunction")
        End Get
        Set(ByVal value As List(Of FunctionDTO))
            ViewState(Me.ID & "_UserFunction") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Số lượng dòng lớn nhất
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MaximumRows As Integer
        Get
            If ViewState(Me.ID & "_MaximumRows") Is Nothing Then
                Return 0
            End If
            Return ViewState(Me.ID & "_MaximumRows")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_MaximumRows") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Bản ghi đã được chọn
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SelectedItem As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItem") Is Nothing Then
                ViewState(Me.ID & "_SelectedItem") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItem")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItem") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' page size
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PageSize As Integer
        Get
            If ViewState(Me.ID & "_PageSize") Is Nothing Then
                Return rgGrid.PageSize
            End If
            Return ViewState(Me.ID & "_PageSize")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_PageSize") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo, thiết lập cho các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgGrid.AllowCustomPaging = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Hiển thị các thành phần trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try

        Catch ex As Exception

        End Try
    End Sub
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Làm mới các thiết lập, các giá trị của các control trên trang về mặc định
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If UserID = Nothing Then Exit Sub
            If Not ViewShowed Then Exit Sub
            Dim Sorts As String = rgGrid.MasterTableView.SortExpressions.GetSortString()
            Dim filter As New FunctionDTO
            filter.FID = rgGrid.MasterTableView.GetColumn("FID").CurrentFilterValue
            filter.NAME = rgGrid.MasterTableView.GetColumn("NAME").CurrentFilterValue
            filter.FUNCTION_GROUP_NAME = rgGrid.MasterTableView.GetColumn("FUNCTION_GROUP_NAME").CurrentFilterValue
            filter.MODULE_NAME = rgGrid.MasterTableView.GetColumn("MODULE_NAME").CurrentFilterValue
            If UserID_Old = Nothing Or UserID_Old <> UserID _
                Or UserFunction Is Nothing Or Message = CommonMessage.ACTION_SAVED _
                 Or Message = CommonMessage.ACTION_UPDATED Then
                Dim rep As New CommonRepository
                If Sorts IsNot Nothing Then
                    UserFunction = rep.GetUserFunctionNotPermision(UserID, filter, rgGrid.CurrentPageIndex, PageSize, MaximumRows, Sorts)
                Else
                    UserFunction = rep.GetUserFunctionNotPermision(UserID, filter, rgGrid.CurrentPageIndex, PageSize, MaximumRows)
                End If
            End If

            'Đưa dữ liệu vào Grid
            rgGrid.VirtualItemCount = MaximumRows
            rgGrid.DataSource = UserFunction
            If Message <> CommonMessage.ACTION_UPDATED Then rgGrid.DataBind()

            UserID_Old = UserID
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Command khi click các item trên toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, Telerik.Web.UI.RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If CurrentState = CommonMessage.STATE_NEW Then
                        Dim lst As List(Of CommonBusiness.UserFunctionDTO)
                        Dim rep As New CommonRepository
                        CurrentState = CommonMessage.STATE_NORMAL
                        lst = RepareData()
                        If rep.InsertUserFunction(lst) Then
                            Refresh(CommonMessage.ACTION_SAVED)
                            'Gửi thông điệp cho Parent View
                            Me.Send(CommonMessage.ACTION_SUCCESS)
                        Else
                            'Gửi thông điệp cho Parent View
                            Me.Send(CommonMessage.ACTION_UNSUCCESS)
                        End If
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    'Gửi thông điệp cho Parent View
                    Me.Send(CommonMessage.ACTION_UPDATED)
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Xử lý sự kiện Itemcommand của rad grid rgGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_ItemCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles rgGrid.ItemCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.CommandName = RadGrid.FilterCommandName Then
                Refresh(CommonMessage.ACTION_SAVED)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện page index changed khi thay đổi giá trị trang
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgGrid.PageIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SaveGridChecked()
            rgGrid.CurrentPageIndex = e.NewPageIndex
            Refresh(CommonMessage.ACTION_SAVED)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện page size changed khi thay đổi page size
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_PageSizeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles rgGrid.PageSizeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            PageSize = e.NewPageSize
            Refresh(CommonMessage.ACTION_SAVED)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện ItemDataBound của rgGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgGrid.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                Dim id As Decimal = Decimal.Parse(datarow("ID").Text)
                If SelectedItem IsNot Nothing AndAlso SelectedItem.Contains(id) Then
                    datarow.Selected = True
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
#End Region

#Region "Custom"
    ''' <summary>
    ''' Phương thức lưu danh sách các item được chọn trên grid
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub SaveGridChecked()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            For Each dr As Telerik.Web.UI.GridDataItem In rgGrid.Items
                Dim id As Decimal = Decimal.Parse(dr("ID").Text)
                If dr.Selected Then
                    If Not SelectedItem.Contains(id) Then SelectedItem.Add(id)
                Else
                    If SelectedItem.Contains(id) Then SelectedItem.Remove(id)
                End If
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <lastupdate>
    ''' 26/07/2017 10:18
    ''' </lastupdate>
    ''' <summary>
    ''' Hàm sửa chữa dữ liệu 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function RepareData() As List(Of CommonBusiness.UserFunctionDTO)
        Dim lst As New List(Of CommonBusiness.UserFunctionDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SaveGridChecked()
            For Each dr As Decimal In SelectedItem
                Dim item As New CommonBusiness.UserFunctionDTO
                item.ALLOW_CREATE = If(cbALLOW_CREATE.Checked, 1, 0)
                item.ALLOW_DELETE = If(cbALLOW_DELETE.Checked, 1, 0)
                item.ALLOW_EXPORT = If(cbALLOW_EXPORT.Checked, 1, 0)
                item.ALLOW_IMPORT = If(cbALLOW_IMPORT.Checked, 1, 0)
                item.ALLOW_MODIFY = If(cbALLOW_MODIFY.Checked, 1, 0)
                item.ALLOW_PRINT = If(cbALLOW_PRINT.Checked, 1, 0)
                item.ALLOW_SPECIAL1 = If(cbALLOW_SPECIAL1.Checked, 1, 0)
                item.ALLOW_SPECIAL2 = If(cbALLOW_SPECIAL2.Checked, 1, 0)
                item.ALLOW_SPECIAL3 = If(cbALLOW_SPECIAL3.Checked, 1, 0)
                item.ALLOW_SPECIAL4 = If(cbALLOW_SPECIAL4.Checked, 1, 0)
                item.ALLOW_SPECIAL5 = If(cbALLOW_SPECIAL5.Checked, 1, 0)
                item.FUNCTION_ID = dr
                item.USER_ID = UserID
                lst.Add(item)
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

        Return lst
    End Function
#End Region

End Class