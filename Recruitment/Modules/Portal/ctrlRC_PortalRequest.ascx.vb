Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Common
Imports Telerik.Web.UI
Imports HistaffFrameworkPublic
Imports System.IO
Imports Aspose.Cells
Imports WebAppLog

Public Class ctrlRC_PortalRequest
    Inherits Common.CommonView

    Protected WithEvents RequestView As Framework.UI.ViewBase
    Private store As New RecruitmentStoreProcedure()
    'Dim sListRejectID As String = ""
    Dim dsDataComper As New DataTable
    Dim _myLog As New MyLog()
    Dim pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Recruitment/Modules/Recruitment/Business/" + Me.GetType().Name.ToString()
    Private rep As New HistaffFrameworkRepository
#Region "Property"
    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property

    Property sListRejectID As String
        Get
            Return ViewState(Me.ID & "_ListRejectID")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_ListRejectID") = value
        End Set
    End Property

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("ORG_ID", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("TITLE_ID", GetType(String))
                dt.Columns.Add("Work_Location_name", GetType(String))
                dt.Columns.Add("Work_Location_ID", GetType(String))
                dt.Columns.Add("SEND_DATE", GetType(String))
                dt.Columns.Add("EXPECTED_JOIN_DATE", GetType(String))
                dt.Columns.Add("LABOR_TYPE_NAME", GetType(String))
                dt.Columns.Add("LABOR_TYPE_ID", GetType(String))
                dt.Columns.Add("RC_RECRUIT_PROPERTY_NAME", GetType(String))
                dt.Columns.Add("RC_RECRUIT_PROPERTY", GetType(String))
                dt.Columns.Add("RECRUIT_REASON_NAME", GetType(String))
                dt.Columns.Add("RECRUIT_REASON_ID", GetType(String))
                dt.Columns.Add("RECRUIT_NUMBER", GetType(String))
                dt.Columns.Add("IS_OVER_LIMIT_NAME", GetType(String))
                dt.Columns.Add("IS_OVER_LIMIT", GetType(String))
                dt.Columns.Add("IS_SUPPORT_NAME", GetType(String))
                dt.Columns.Add("IS_SUPPORT", GetType(String))
                dt.Columns.Add("RECRUIT_REASON", GetType(String))
                dt.Columns.Add("LEARNING_LEVEL_NAME", GetType(String))
                dt.Columns.Add("LEARNING_LEVEL_ID", GetType(String))
                dt.Columns.Add("AGE_FROM", GetType(String))
                dt.Columns.Add("AGE_TO", GetType(String))
                dt.Columns.Add("QUALIFICATION_NAME", GetType(String))
                dt.Columns.Add("QUALIFICATION", GetType(String))
                dt.Columns.Add("LANGUAGE_NAME", GetType(String))
                dt.Columns.Add("LANGUAGE", GetType(String))
                dt.Columns.Add("LANGUAGELEVEL_NAME", GetType(String))
                dt.Columns.Add("LANGUAGELEVEL", GetType(String))
                dt.Columns.Add("LANGUAGESCORES", GetType(String))
                dt.Columns.Add("FOREIGN_ABILITY", GetType(String))
                dt.Columns.Add("EXPERIENCE_NUMBER", GetType(String))
                dt.Columns.Add("GENDER_PRIORITY_NAME", GetType(String))
                dt.Columns.Add("GENDER_PRIORITY", GetType(String))
                dt.Columns.Add("COMPUTER_LEVEL_NAME", GetType(String))
                dt.Columns.Add("COMPUTER_LEVEL", GetType(String))
                dt.Columns.Add("COMPUTER_APP_LEVEL", GetType(String))
                dt.Columns.Add("DESCRIPTION", GetType(String))
                dt.Columns.Add("MAINTASK", GetType(String))
                'dt.Columns.Add("SPECIALSKILLS_NAME", GetType(String))
                'dt.Columns.Add("SPECIALSKILLS", GetType(String))
                dt.Columns.Add("REQUEST_EXPERIENCE", GetType(String))
                dt.Columns.Add("REQUEST_OTHER", GetType(String))
                dt.Columns.Add("STATUS_NAME", GetType(String))
                dt.Columns.Add("STATUS_ID", GetType(String))
                'dt.Columns.Add("REMARK", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    Property dtDataImport As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImport")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImport") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dtData As DataTable
            Using rep As New RecruitmentRepository
                dtData = rep.GetOtherList("RC_REQUEST_STATUS", True)
                FillRadCombobox(cboStatus, dtData, "NAME", "ID")
            End Using

            'Dim item1 As New RadComboBoxItem()
            'item1.Text = "Dành cho Chi nhánh: Tuyển bổ sung các vị trí thuộc cấp bậc công nhân"
            'item1.Value = 0
            'cboPrintSupport.Items.Add(item1)
            'Dim item2 As New RadComboBoxItem()
            'item2.Text = "Dành cho Chi nhánh: Tuyển bổ sung các vị trí khác cấp bậc công nhân, tuyển mới tất cả các vị trí"
            'item2.Value = 1
            'cboPrintSupport.Items.Add(item2)
            'Dim item3 As New RadComboBoxItem()
            'item3.Text = "Tổng công ty: Tuyển tất cả các vị trí "
            'item3.Value = 2
            'cboPrintSupport.Items.Add(item3)

            ' Load cbo chức danh(Vị trí tuyển dụng)    
            'Dim ds As DataSet = rep.ExecuteToDataSet("PKG_PROFILE.GET_LIST_TITLE")
            'FillRadCombobox(cboRecruitment, ds.Tables(0), "NAME_VN", "ID")
            'cboRecruitment.DataBind()
            'cboRecruitment.Items.Insert(0, New RadComboBoxItem("", "-1"))
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            ''TODO: DAIDM comment
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Delete,
                                       ToolbarItem.ApproveBatch)
            CType(MainToolBar.Items(3), RadToolBarButton).Text = "Gửi phê duyệt"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New RecruitmentRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteRequest(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_APPROVE
                    Dim lstWaitingList As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstWaitingList.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.UpdateStatusRequest(lstWaitingList, RecruitmentCommon.RC_REQUEST_STATUS.WAIT_ID) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
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

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If item.GetDataKeyValue("STATUS_ID") = RecruitmentCommon.RC_REQUEST_STATUS.NOT_APPROVE_ID Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái không phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_APPROVE
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate("Bạn muốn gửi phê duyệt?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_APPROVE
                UpdateControlState()
            End If      
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged

        Try
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
            rwPopup.VisibleOnPageLoad = False
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow("ORG_DESC").Text)
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        rgData.CurrentPageIndex = 0
        rgData.MasterTableView.SortExpressions.Clear()
        rgData.Rebind()
    End Sub
#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New RequestDTO
        Dim rep As New RecruitmentRepository
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgData.DataSource = New List(Of RequestDTO)
                Exit Function
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                               .IS_DISSOLVE = ctrlOrg.IsDissolve}
            If cboStatus.SelectedValue <> "" Then
                _filter.STATUS_ID = Decimal.Parse(cboStatus.SelectedValue)
            End If
            'If _param.ORG_ID <> 1 Then
            '    Dim dtData As DataTable
            '    dtData = store.GET_TITLE_IN_PLAN(_param.ORG_ID, 0)
            '    FillRadCombobox(cboRecruitment, dtData, "NAME", "ID")
            '    _filter.TITLE_NAME = cboRecruitment.Text
            'Else
            '    cboRecruitment.Text = ""
            'End If
            '''''ẩn tìm kiếm năm
            'If IsNumeric(rnYear.Value) Then
            '    _filter.YEAR = rnYear.Value
            'End If
            If rdFromDate.SelectedDate IsNot Nothing Then
                _filter.FROM_DATE = rdFromDate.SelectedDate
            End If
            If rdToDate.SelectedDate IsNot Nothing Then
                _filter.TO_DATE = rdToDate.SelectedDate
            End If

            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of RequestDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GetRequest(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
            Else
                lstData = rep.GetRequest(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
            End If

            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
            Return lstData.ToTable
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region


    Function loadToGrid() As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dtData.Rows.Count = 0 Then
                ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            'Dim is_Validate As Boolean
            Dim _validate As New RequestDTO
            Dim rep As New RecruitmentRepository
            Dim store As New RecruitmentStoreProcedure
            dtData.TableName = "DATA"
            dtDataImport = dtData.Clone
            dtError = dtData.Clone
            Dim irow = 4
            Dim irowEm = 4

            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                sError = "Ban/Phòng"
                ImportValidate.IsValidList("ORG_NAME", "ORG_ID", row, rowError, isError, sError)
                sError = "Vị trí tuyển dụng"
                ImportValidate.IsValidList("TITLE_NAME", "TITLE_ID", row, rowError, isError, sError)
                sError = "Ngày gửi yêu cầu không được để trống"
                ImportValidate.IsValidDate("SEND_DATE", row, rowError, isError, sError)
                sError = "Ngày cần đáp ứng không được để trống"
                ImportValidate.IsValidDate("EXPECTED_JOIN_DATE", row, rowError, isError, sError)
                sError = "Số lượng cần tuyển"
                ImportValidate.IsValidList("RECRUIT_NUMBER", "RECRUIT_NUMBER", row, rowError, isError, sError)
                sError = "Lý do tuyển dụng"
                ImportValidate.IsValidList("RECRUIT_REASON_NAME", "RECRUIT_REASON_ID", row, rowError, isError, sError)
                sError = "Trình độ học vấn"
                ImportValidate.IsValidList("LEARNING_LEVEL_NAME", "LEARNING_LEVEL_ID", row, rowError, isError, sError)
                sError = "Độ tuổi từ"
                ImportValidate.IsValidList("AGE_FROM", "AGE_FROM", row, rowError, isError, sError)
                sError = "Độ tuổi đến"
                ImportValidate.IsValidList("AGE_TO", "AGE_TO", row, rowError, isError, sError)
                sError = "Nghiệp vụ chuyên môn"
                ImportValidate.IsValidList("QUALIFICATION_NAME", "QUALIFICATION", row, rowError, isError, sError)
                sError = "Chưa nhập Mô tả công việc"
                ImportValidate.EmptyValue("DESCRIPTION", row, rowError, isError, sError)
                If isError Then
                    row("STT") = irow
                    rowError("STT") = irow
                    row("ORG_NAME") = rowError("ORG_NAME").ToString
                    row("TITLE_NAME") = rowError("TITLE_NAME").ToString
                    row("SEND_DATE") = rowError("SEND_DATE").ToString
                    row("EXPECTED_JOIN_DATE") = rowError("EXPECTED_JOIN_DATE").ToString
                    row("RECRUIT_NUMBER") = rowError("RECRUIT_NUMBER").ToString
                    row("RECRUIT_REASON_NAME") = rowError("RECRUIT_REASON_NAME").ToString
                    row("LEARNING_LEVEL_NAME") = rowError("LEARNING_LEVEL_NAME").ToString
                    row("AGE_FROM") = rowError("AGE_FROM").ToString
                    row("AGE_TO") = rowError("AGE_TO").ToString
                    row("QUALIFICATION_NAME") = rowError("QUALIFICATION_NAME").ToString
                    row("DESCRIPTION") = rowError("DESCRIPTION").ToString
                    dtError.Rows.Add(rowError)
                Else
                    'Check exist
                    Dim dto As New RequestDTO
                    dto.ORG_ID = CDec(row("ORG_ID"))
                    dto.TITLE_ID = CDec(row("TITLE_ID"))
                    dto.SEND_DATE = ToDate(row("SEND_DATE"))
                    dto.EXPECTED_JOIN_DATE = ToDate(row("EXPECTED_JOIN_DATE"))
                    If store.CheckExistRequest(dto) > 0 Then
                        rowError("STT") = irow
                        rowError("ORG_NAME") = "Đã có tuyển dụng này."
                        dtError.Rows.Add(rowError)
                    Else
                        dtDataImport.ImportRow(row)
                    End If
                End If
                irow = irow + 1
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("EXPORTREPORT") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_yeucautuyendung_Error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            Return Not isError
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class