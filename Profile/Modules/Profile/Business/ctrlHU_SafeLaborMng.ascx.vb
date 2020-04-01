﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Common
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlHU_SafeLaborMng
    Inherits Common.CommonView
    Protected WithEvents WelfareMng As ViewBase
    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
#Region "Property"

    ''' <summary>
    ''' List obj WelfareMngs
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WelfareMngs As List(Of SAFELABOR_MNGDTO)
        Get
            Return ViewState(Me.ID & "_WelfareMng")
        End Get
        Set(ByVal value As List(Of SAFELABOR_MNGDTO))
            ViewState(Me.ID & "_WelfareMng") = value
        End Set
    End Property
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 10/07/2017 09:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            rgWelfareMng.SetFilter()
            rgWelfareMng.AllowCustomPaging = True
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 09:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Xét các trạng thái của grid rgWelfareMng
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 09:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Try
            GetDataCombo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub GetDataCombo()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Try
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <lastupdate>
    ''' 10/07/2017 09:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ham khoi tao toolbar voi cac button them moi, sua, xuat file, xoa
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarWelfareMngs
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Seperator,
                                        ToolbarItem.Export,
                                       ToolbarItem.Delete)

            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True

            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 09:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Xet cac tuy chon gia tri cua Message la UpdateView, InsertView
    ''' Bind lai du lieu cho grid rgWelfareMng
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileBusinessRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        rgWelfareMng.Rebind()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        rgWelfareMng.Rebind()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                End Select
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 10/07/2017 09:30
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' Command them, sua phuc loi ca nhan, xoa phuc loi ca nhan, xuat file excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objOrgFunction As New OrganizationDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim sError As String = ""
        Dim rep As New ProfileBusinessRepository
        Dim id As String = ""
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ' Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_SafeLaborMngNewEdit&group=Business")
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                    If rgWelfareMng.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    ElseIf rgWelfareMng.SelectedItems.Count > 1 Then
                        ShowMessage(Translate("Bạn không thể sửa nhiều bản ghi! Không thể thực hiện thao tác này"), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgWelfareMng.SelectedItems
                        id = item.GetDataKeyValue("ID")
                    Next
                    'Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_SafeLaborMngNewEdit&group=Business&gUID=" + id)
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgWelfareMng.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    'For Each item As GridDataItem In rgWelfareMng.SelectedItems
                    '    If item.GetDataKeyValue("WORK_STATUS") = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                    '        ShowMessage(Translate("Nhân viên nghỉ việc. Không được xóa thông tin."), Utilities.NotifyType.Warning)
                    '        Exit Sub
                    '    End If
                    'Next
                    For Each item As GridDataItem In rgWelfareMng.SelectedItems
                        If item.GetDataKeyValue("EFFECT_DATE") <= Date.Now Then
                            ShowMessage(Translate("Không được xóa những phúc lợi đã tới ngày hiệu lực"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgWelfareMng.ExportExcel(Server, Response, dtData, "Welfare")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 09:30
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien SelectedNodeChanged cua control ctrlOrg
    ''' Xet lai cac thiet lap trang thai cho grid rgWelfareMng
    ''' Bind lai du lieu cho rgWelfareMng
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgWelfareMng.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 09:30
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command button cua control ctrkMessageBox
    ''' Cap nhat trang thai cua cac control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim rep As New ProfileBusinessRepository
                Dim objD As New List(Of WelfareMngDTO)
                Dim lst As New List(Of Decimal)
                For Each item As GridDataItem In rgWelfareMng.SelectedItems
                    Dim obj As New WelfareMngDTO
                    obj.ID = Utilities.ObjToDecima(item.GetDataKeyValue("ID"))
                    objD.Add(obj)
                Next
                If rep.DeleteWelfareMng(objD) Then
                    objD = Nothing
                    Refresh("UpdateView")
                End If
                rep.Dispose()
                UpdateControlState()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 09:30
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho rad grid 
    ''' Tao du lieu filter
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgWelfareMng.NeedDataSource

        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien ItemDataBound cho rad grin rgWelfareMng
    ''' Hien thi tooltip tren rgWelfareMng
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgWelfareMng.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow("ORG_DESC").Text)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

  

#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 10/07/2017 08:45
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc tao du lieu filter
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New SAFELABOR_MNGDTO
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            If ctrlOrg.CurrentValue <> "" Then
                _filter.ORG_ID = ctrlOrg.CurrentValue
            Else
                _filter.ORG_ID = 0
            End If
            
            Dim MaximumRows As Integer
            Dim Sorts As String = rgWelfareMng.MasterTableView.SortExpressions.GetSortString()
            SetValueObjectByRadGrid(rgWelfareMng, _filter)
            '_filter.IS_TER = chkTerminate.Checked
   
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetSafeLaborMng(_filter, ctrlOrg.IsDissolve, Sorts).ToTable()
                Else
                    Return rep.GetSafeLaborMng(_filter, ctrlOrg.IsDissolve).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.WelfareMngs = rep.GetSafeLaborMng(_filter, ctrlOrg.IsDissolve, rgWelfareMng.CurrentPageIndex, rgWelfareMng.PageSize, MaximumRows, Sorts)
                Else
                    Me.WelfareMngs = rep.GetSafeLaborMng(_filter, ctrlOrg.IsDissolve, rgWelfareMng.CurrentPageIndex, rgWelfareMng.PageSize, MaximumRows)
                End If
                rgWelfareMng.VirtualItemCount = MaximumRows
                rgWelfareMng.DataSource = WelfareMngs

            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Function

#End Region

End Class