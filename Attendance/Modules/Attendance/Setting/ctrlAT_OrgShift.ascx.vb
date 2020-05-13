﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Telerik.Web.UI
Imports Common.Common
Imports Common.CommonMessage
Imports WebAppLog
Imports System.IO
Imports System.Globalization
Imports System.Drawing
Imports Aspose.Cells
Imports Profile
Imports Profile.ProfileBusiness
Imports Attendance
Imports Attendance.AttendanceBusiness

Public Class ctrlAT_OrgShift
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Setting/" + Me.GetType().Name.ToString()
    Protected WithEvents RequestView As ViewBase

#Region "Property"

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property
    Property ID_Item As Decimal
        Get
            Return ViewState(Me.ID & "_ID_Item")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ID_Item") = value
        End Set
    End Property
    Property ComboBoxDataDTO As Profile.ProfileBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ComboBoxDataDTO")
        End Get
        Set(ByVal value As Profile.ProfileBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ComboBoxDataDTO") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Property popupId As String
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            SetGridFilter(rgData)
            Dim popup As RadWindow
            popup = CType(Me.Page, AjaxPage).PopupWindow
            popupId = popup.ClientID

            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Sync, ToolbarItem.Export)
            CType(MainToolBar.Items(3), RadToolBarButton).Text = "Cập nhật hàng loạt"
            'CType(MainToolBar.Items(4), RadToolBarButton)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
            ctrlOrg.Enabled = True

            'ctrlOrg.AutoPostBack = True
            'ctrlOrg.LoadDataAfterLoaded = True
            'ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            'ctrlOrg.CheckBoxes = TreeNodeTypes.None
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New AttendanceRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub UpdateControlState()
        Dim rep As New AttendanceRepository
        Try
            Select Case CurrentState
                'Case CommonMessage.STATE_DELETE
                '    Dim lstDeletes As New List(Of Decimal)
                '    For idx = 0 To rgData.SelectedItems.Count - 1
                '        Dim item As GridDataItem = rgData.SelectedItems(idx)
                '        lstDeletes.Add(item.GetDataKeyValue("ID"))
                '    Next
                '    CurrentState = CommonMessage.STATE_NORMAL
                '    If rep.DeletePrograms(lstDeletes) Then
                '        Refresh("UpdateView")
                '        UpdateControlState()
                '    Else
                '        ShowMessage(Translate("Xóa thất bại, vui lòng kiểm tra lại!"), NotifyType.Error)
                '        UpdateControlState()
                '    End If
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '''
    ''' <summary>
    ''' Đổ dữ liệu được chọn từ grid lên control input
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Using rep As New ProfileRepository
                Dim dtData As New DataTable

                dtData = rep.GetOtherList("EMPLOYEE_OBJECT", True)
                'FillRadCombobox(cbo_OBJ_EMP_Search, dtData, "NAME", "ID", False)
                'dtData = rep.GetOtherList("COMPENSATORY_OBJECT", True)
                'FillRadCombobox(cbo_OBJ_CSL_Search, dtData, "NAME", "ID", False)

                'dtData = rep.GetOtherList("COMPENSATORY_OBJECT", True)
                'FillRadCombobox(cbo_OBJ_CSL_updateAll, dtData, "NAME", "ID", False)
                'dtData = rep.GetOtherList("EMPLOYEE_OBJECT", True)
                'FillRadCombobox(cbo_OBJ_EMP_updateAll, dtData, "NAME", "ID", False)

                'dtData = rep.GetOtherList("WORK_STATUS", True)
                'Dim dr2 As DataRow = dtData.NewRow
                'dr2("NAME") = "Chưa thiết lập trạng thái"
                'dr2("ID") = 9999
                'dtData.Rows.Add(dr2)
                'FillRadCombobox(cboStatus, dtData, "NAME", "ID", False)

            End Using

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New AttendanceRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT
                    Dim isCheck As Boolean = False
                    For Each item As GridDataItem In rgData.MasterTableView.Items
                        If item.Selected Then
                            isCheck = True
                            item.Edit = True
                        End If
                    Next

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                    rgData.MasterTableView.Rebind()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    For Each item As GridDataItem In rgData.MasterTableView.Items
                        item.Edit = False
                    Next
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                    rgData.MasterTableView.Rebind()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim lst As New List(Of AT_ObjectEmpployeeCompensatoryDTO)
                    Dim objEdit As New AT_ObjectEmpployeeCompensatoryDTO
                    For Each item As GridDataItem In rgData.EditItems
                        If item.Edit = True Then
                            Dim edit = CType(item, GridEditableItem)
                            Dim cbo_OBJ_EMP_ID As RadComboBox
                            Dim cbo_OBJ_CSL_ID As RadComboBox

                            cbo_OBJ_EMP_ID = CType(edit.FindControl("cbo_OBJ_EMP_NAME"), RadComboBox)
                            cbo_OBJ_CSL_ID = CType(edit.FindControl("cbo_OBJ_CSL_NAME"), RadComboBox)
                            Dim obj As New AT_ObjectEmpployeeCompensatoryDTO
                            With obj
                                .ID = item.GetDataKeyValue("ID")
                                .OBJ_EMP_ID = If(cbo_OBJ_EMP_ID.SelectedValue <> "", Decimal.Parse(cbo_OBJ_EMP_ID.SelectedValue), Nothing)
                                .OBJ_CSL_ID = If(cbo_OBJ_CSL_ID.SelectedValue <> "", Decimal.Parse(cbo_OBJ_CSL_ID.SelectedValue), Nothing)
                            End With
                            lst.Add(obj)
                        End If
                    Next
                    Using repUpdate As New AttendanceRepository
                        If repUpdate.Update_ObjectEandC(lst, objEdit, "Update_ObjectEandC_EachOne") Then
                            CurrentState = CommonMessage.STATE_NORMAL
                            For Each item As GridDataItem In rgData.MasterTableView.Items
                                item.Edit = False
                            Next
                            rgData.Rebind()
                            UpdateControlState()
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SYNC
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Vui lòng chọn các nhân viên cần cập nhật"), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lst As New List(Of AT_ObjectEmpployeeCompensatoryDTO)
                    Dim objEdit As New AT_ObjectEmpployeeCompensatoryDTO
                    'objEdit.OBJ_EMP_ID = If(cbo_OBJ_EMP_updateAll.SelectedValue <> "", Decimal.Parse(cbo_OBJ_EMP_updateAll.SelectedValue), Nothing)
                    'objEdit.OBJ_CSL_ID = If(cbo_OBJ_CSL_updateAll.SelectedValue <> "", Decimal.Parse(cbo_OBJ_CSL_updateAll.SelectedValue), Nothing)
                    For Each item As GridDataItem In rgData.SelectedItems()
                        Dim obj As New AT_ObjectEmpployeeCompensatoryDTO
                        With obj
                            .ID = item.GetDataKeyValue("ID")
                            .OBJ_EMP_ID = If(objEdit.OBJ_EMP_ID <> 0, objEdit.OBJ_EMP_ID, If(item.GetDataKeyValue("OBJ_EMP_ID") <> 0, item.GetDataKeyValue("OBJ_EMP_ID"), 0))
                            .OBJ_CSL_ID = If(objEdit.OBJ_CSL_ID <> 0, objEdit.OBJ_CSL_ID, If(item.GetDataKeyValue("OBJ_CSL_ID") <> 0, item.GetDataKeyValue("OBJ_CSL_ID"), 0))
                        End With
                        lst.Add(obj)
                    Next
                    Using repUpdate As New AttendanceRepository
                        If repUpdate.Update_ObjectEandC(lst, objEdit, "Update_ObjectEandC_EachOne") Then
                            CurrentState = CommonMessage.STATE_NORMAL
                            For Each item As GridDataItem In rgData.MasterTableView.Items
                                item.Edit = False
                            Next
                            rgData.Rebind()
                            UpdateControlState()
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "Object_Employee_Compensatory")
                        Else
                            ShowMessage(Translate("Không có dữ liệu để suất Excel"), NotifyType.Warning)
                        End If
                    End Using
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim rep As New AttendanceRepository
        Try
            'CreateDataFilter()

            Dim _param = New Attendance.AttendanceBusiness.ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}
            Dim datalist As DataTable
            datalist = rep.GetOrgShiftList(_param)
            If datalist IsNot Nothing Then

                If Not IsPostBack Then
                    DesignGrid(datalist)
                End If
                rgData.DataSource = datalist
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New AT_ObjectEmpployeeCompensatoryDTO
        Dim rep As New AttendanceRepository
        Dim total As Integer = 0
        Dim bCheck As Boolean = False
        Try

            Dim _param = New Attendance.AttendanceBusiness.ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of AT_ObjectEmpployeeCompensatoryDTO)
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetObjEmpCompe(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetObjEmpCompe(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    lstData = rep.GetObjEmpCompe(_filter, _param, MaximumRows, rgData.CurrentPageIndex, rgData.PageSize)
                Else
                    lstData = rep.GetObjEmpCompe(_filter, _param, MaximumRows, rgData.CurrentPageIndex, rgData.PageSize)
                End If
            End If
            'rgData.VirtualItemCount = MaximumRows
            ' rgData.DataSource = lstData
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
#Region "Custom"
    Protected Sub DesignGrid(ByVal dt As DataTable)

        Dim rColCheck As GridClientSelectColumn
        Dim rCol As GridCheckBoxColumn
        'Dim rCol As GridTemplateColumn
        rgData.MasterTableView.Columns.Clear()
        For Each column As DataColumn In dt.Columns
            If column.ColumnName = "ID" Then
                rColCheck = New GridClientSelectColumn()

                rgData.MasterTableView.Columns.Add(rColCheck)
                rColCheck.HeaderStyle.Width = 30
                rColCheck.HeaderStyle.HorizontalAlign = HorizontalAlign.Center

            Else
                rCol = New GridCheckBoxColumn()
                rgData.MasterTableView.Columns.Add(rCol)
                rCol.HeaderStyle.Width = 30
                rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rCol.HeaderText = Translate(column.ColumnName)
                rCol.AllowFiltering = False
            End If

        Next
    End Sub

#End Region

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try

            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

End Class