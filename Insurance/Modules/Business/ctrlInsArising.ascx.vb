Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Telerik.Web.UI
Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI.Calendar


Public Class ctrlInsArising
    Inherits Common.CommonView
    'Private WithEvents viewRegister As ctrlShiftPlanningRegister

#Region "Property & Variable"
    Public Property popup As RadWindow
    Public Property popupId As String

    Private Property LastStartDate As Nullable(Of Date)
        Get
            Return PageViewState("LastStartDate")
        End Get
        Set(ByVal value As Nullable(Of Date))
            PageViewState("LastStartDate") = value
        End Set
    End Property

    Private Property LastEndDate As Nullable(Of Date)
        Get
            Return PageViewState("LastEndDate")
        End Get
        Set(ByVal value As Nullable(Of Date))
            PageViewState("LastEndDate") = value
        End Set
    End Property

    'Private Property ListSign As List(Of SIGN_DTO)
    '    Get
    '        Return PageViewState(Me.ID & "_ListSign")
    '    End Get
    '    Set(ByVal value As List(Of SIGN_DTO))
    '        PageViewState(Me.ID & "_ListSign") = value
    '    End Set
    'End Property

    Public ReadOnly Property PageId As String
        Get
            Return Me.ID
        End Get
    End Property

    'Private Property ListPeriod As List(Of PERIOD_DTO)
    '    Get
    '        Return ViewState(Me.ID & "_ListPeriod")
    '    End Get
    '    Set(ByVal value As List(Of PERIOD_DTO))
    '        ViewState(Me.ID & "_ListPeriod") = value
    '    End Set
    'End Property

    Public Property InsCompList As DataTable
        Get
            Return ViewState(Me.ID & "_InsCompList")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_InsCompList") = value
        End Set
    End Property

    Public Property SelectedItemList As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_SelectedItemList")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_SelectedItemList") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            GetDataCombo()

            rgGridData.AllowCustomPaging = True
            rgGridData.PageSize = Common.Common.DefaultPageSize
            popup = CType(Me.Page, AjaxPage).PopupWindow
            popup.Behaviors = WindowBehaviors.Close
            popupId = popup.ClientID

            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Seperator, ToolbarItem.Delete,
                                       ToolbarItem.Export)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrg.CheckChildNodes = True
            ctrlOrg.CheckBoxes = TreeNodeTypes.All

            txtFROMDATE.SelectedDate = New Date(Now.Year, 1, 1)
            txtTODATE.SelectedDate = New Date(Now.Year, 12, 31)
            txtMONTH.SelectedDate = New Date(Now.Year, Now.Month, 1)

            'rgGridData.GroupingSettings.GroupContinuedFormatString = "... group continued from the previous page"
            'rgGridData.GroupingSettings.GroupContinuesFormatString = "Group continues on the nextpage"
            'rgGridData.GroupingSettings.GroupSplitDisplayFormat = " Showing {0} of {1}  items "

            rgGridData.GroupingSettings.GroupContinuedFormatString = "... "
            rgGridData.GroupingSettings.GroupContinuesFormatString = " "
            rgGridData.GroupingSettings.GroupSplitDisplayFormat = " Hiển thị {0} của {1} bản ghi "

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            'Me.rgGridData.SetFilter()
            If Not IsPostBack Then
                UpdateControlState(CommonMessage.STATE_NORMAL)
                UpdateToolbarState(CommonMessage.STATE_NORMAL)
                txtID.Text = "0"

                Call LoadDataGrid()
                ' Refresh()
                ' UpdateControlState()
            End If
            'rgGridData.Culture = Common.Common.SystemLanguage

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dic As New Dictionary(Of String, Control)

            dic.Add("ID", txtID)

            Utilities.OnClientRowSelectedChanged(rgGridData, dic)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")

        Try
            Call LoadDataGrid(True)
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub UpdateControlState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGridNotPostback(rgGridData, True)

                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgGridData, False)


                Case CommonMessage.STATE_DELETE

                Case "Nothing"
                    Utilities.EnabledGridNotPostback(rgGridData, True)
            End Select

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub UpdateToolbarState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGridNotPostback(rgGridData, True)

                Case CommonMessage.STATE_NEW

                Case CommonMessage.STATE_EDIT

                Case CommonMessage.STATE_DELETE

                Case "Nothing"
                    Utilities.EnabledGridNotPostback(rgGridData, True)

            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Event"

    Protected Sub btnFIND_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFind.Click
        Call LoadDataGrid()
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        'Dim objOrgFunction As New OrganizationDTO
        'Dim gID As Decimal
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    Call ResetForm()
                    CurrentState = CommonMessage.STATE_NEW
                    'txtCODE.Focus()
                Case CommonMessage.TOOLBARITEM_EDIT

                    CurrentState = CommonMessage.STATE_EDIT
                    'txtCODE.Focus()
                    'Case CommonMessage.TOOLBARITEM_ACTIVE, CommonMessage.TOOLBARITEM_DEACTIVE

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    'FillDropDownList(cboLevel_ID, ListComboData.LIST_ORG_LEVEL, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboLevel_ID.SelectedValue)
                    'FillDataByTree()
                Case CommonMessage.TOOLBARITEM_NEXT
                    Call SaveData()
                    CurrentState = CommonMessage.STATE_NORMAL
                Case CommonMessage.TOOLBARITEM_SAVE
                    If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn dữ liệu để chuyển sang quản lý biến động."), NotifyType.Warning)
                        Exit Sub
                    End If
                    'Check data trước khi chuyển
                    For Each dr As Telerik.Web.UI.GridDataItem In rgGridData.SelectedItems
                        If dr.GetDataKeyValue("EFFECT_DATE") > txtMONTH.SelectedDate Then
                            ShowMessage("Ngày khai báo phải lớn hơn ngày hiệu lực. Mã nhân viên: " & dr.GetDataKeyValue("EMPLOYEE_CODE").ToString() & " - " & dr.GetDataKeyValue("EFFECT_DATE") & ")", NotifyType.Warning, 13)
                            Exit Sub
                        End If
                        If DirectCast(dr.GetDataKeyValue("EFFECT_DATE"), Date).Day > 15 Then
                            If Format(DirectCast(dr.GetDataKeyValue("EFFECT_DATE"), Date), "yyyyMM") >= Format(txtMONTH.SelectedDate.Value, "yyyyMM") Then
                                ShowMessage("Tháng khai báo phải lớn hơn tháng của ngày hiệu lực. Lỗi: Mã nhân viên (" & dr.GetDataKeyValue("EMPLOYEE_CODE").ToString() & " - " & dr.GetDataKeyValue("EFFECT_DATE") & ")", NotifyType.Warning, 13)
                                Exit Sub
                            End If
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate("Bạn có muốn chuyển sang quản lý biến động?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_SAVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                    CurrentState = CommonMessage.STATE_NORMAL
                    'Lưu ghi chú
                    'Call SaveNote()
                    'CurrentState = CommonMessage.STATE_NORMAL
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nội dung cần xóa."), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate("Bạn có muốn xóa dữ liệu ?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                    CurrentState = CommonMessage.STATE_NORMAL
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Call Export()
            End Select
            UpdateControlState(CurrentState)
            UpdateToolbarState(CurrentState)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgGridData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGridData.NeedDataSource

        Call LoadDataGrid(False)

    End Sub

    Protected Function RepareDataForAction() As List(Of Decimal)
        Dim lst As New List(Of Decimal)
        For Each dr As Telerik.Web.UI.GridDataItem In rgGridData.SelectedItems
            lst.Add(dr.GetDataKeyValue("ID"))
        Next
        Return lst
    End Function

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Call DeleteData()
                Call ResetForm()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_SAVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Call SaveData()
                Call ResetForm()
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub


#End Region

#Region "Function & Sub"

    Private Sub ResetForm()
        Try
            txtID.Text = "0"
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SaveData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient

            If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                ShowMessage(Translate("Bạn chưa chọn nội dung cần chuyển sang Quản lý biến động!"), NotifyType.Warning)
                Exit Sub
            End If

            Dim isFail As Integer = 0
            Dim isResult As Integer = 0

            For i As Integer = 0 To rgGridData.SelectedItems.Count - 1
                Dim item As GridDataItem = rgGridData.SelectedItems(i)
                Dim id As Integer = Integer.Parse(item("ID").Text)
                Dim empid As Integer = Integer.Parse(item("EMPID").Text)
                isResult = rep.UpdateInsArising(Common.Common.GetUsername(), txtMONTH.SelectedDate, id, empid, InsCommon.getNumber(ddlINS_ARISING_TYPE_ID.SelectedValue))
                If isResult = 0 Then
                    isFail = 1
                End If
            Next
            If isFail = 0 Then
                Refresh("UpdateView")
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SaveNote()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient

            If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                ShowMessage(Translate("Bạn chưa chọn nội dung cần lưu."), NotifyType.Warning)
                Exit Sub
            End If

            Dim isFail As Integer = 0
            Dim isResult As Integer = 0
            For i As Integer = 0 To rgGridData.SelectedItems.Count - 1
                Dim item As GridDataItem = rgGridData.SelectedItems(i)
                Dim id As Integer = Integer.Parse(item("ID").Text)
                Dim txtBox As RadTextBox = DirectCast(item("NOTE").FindControl("rtbNote"), RadTextBox)
                Dim note = txtBox.Text
                isResult = rep.UpdateInsArisingNote(Common.Common.GetUsername(), id, note)
                If isResult = 0 Then
                    isFail = 1
                End If
            Next
            If isFail = 0 Then
                Refresh("UpdateView")
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DeleteData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            'objOrgFunction.ID = Decimal.Parse(hidID.Value)

            Dim isFail As Integer = 0
            Dim isResult As Boolean
            For i As Integer = 0 To rgGridData.SelectedItems.Count - 1
                Dim item As GridDataItem = rgGridData.SelectedItems(i)
                Dim id As Integer = Integer.Parse(item("ID").Text)
                isResult = rep.DeleteInsArising(Common.Common.GetUsername(), id)
                If isResult = False Then
                    isFail = 1
                End If
            Next
            If isFail = 0 Then
                Refresh("UpdateView")
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetDataCombo()
        Try
            Dim rep As New InsuranceBusinessClient
            Dim dtData = rep.GetOtherList("GROUP_ARISING_TYPE", Common.Common.SystemLanguage.Name, False) 'Loai nhóm bien dong
            FillRadCombobox(ddlGROUP_ARISING_TYPE_ID, dtData, "NAME", "ID", False)

            dtData = rep.GetInsListChangeType() 'Loai bien dong
            FillRadCombobox(ddlINS_ARISING_TYPE_ID, dtData, "ARISING_NAME", "ID", False)

            dtData = rep.GetInsListInsurance(False) 'Don vi bao hiem
            FillRadCombobox(ddlINSORG, dtData, "NAME", "ID", False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LoadDataGrid(Optional ByVal IsDataBind As Boolean = True)
        Try
            '--------------------ThanhNT added 22/12/2015---------------------------
            'Add condition for query data in db-
            'Get list org checked
            Dim lstOrg = ctrlOrg.CheckedValueKeys
            Dim lstOrgStr = ""
            If ctrlOrg.CheckedValueKeys.Count > 0 Then
                For i As Integer = 0 To lstOrg.Count - 1
                    If i = lstOrg.Count - 1 Then
                        lstOrgStr = lstOrgStr & lstOrg(i).ToString
                    Else
                        lstOrgStr = lstOrgStr & lstOrg(i).ToString & ","
                    End If
                Next
            End If

            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsArising(Common.Common.GetUsername(), txtFROMDATE.SelectedDate, txtTODATE.SelectedDate _
                                                                                                   , InsCommon.getNumber(ddlGROUP_ARISING_TYPE_ID.SelectedValue) _
                                                                                                   , lstOrgStr _
                                                                                                   , InsCommon.getNumber(ddlINSORG.SelectedValue))



            Dim maximumRows As Double = 0
            Dim startRowIndex As Double = 0
            Dim dtb As New DataTable
            dtb.TableName = "TBL"
            dtb.Columns.Add("ID", GetType(String))
            dtb.Columns.Add("EMPID", GetType(String))
            dtb.Columns.Add("EMPLOYEE_CODE", GetType(String))
            dtb.Columns.Add("FULL_NAME", GetType(String))
            dtb.Columns.Add("DEP_NAME", GetType(String))
            dtb.Columns.Add("TITLE_NAME", GetType(String))
            dtb.Columns.Add("EFFECT_DATE", GetType(Date))
            dtb.Columns.Add("ARISING_TYPE_NAME", GetType(String))
            dtb.Columns.Add("OLD_SAL", GetType(Decimal))
            dtb.Columns.Add("NEW_SAL", GetType(Decimal))
            dtb.Columns.Add("SI", GetType(Boolean))
            dtb.Columns.Add("HI", GetType(Boolean))
            dtb.Columns.Add("UI", GetType(Boolean))
            dtb.Columns.Add("ARISING_TYPE_ID", GetType(Decimal))
            dtb.Columns.Add("ARISING_GROUP_TYPE", GetType(String))
            dtb.Columns.Add("REASONS", GetType(String))
            dtb.Columns.Add("NOTE", GetType(String))
            dtb.Columns.Add("BHTNLD_BNN", GetType(Boolean))
            dtb.Columns.Add("ORG_DESC", GetType(String))
            dtb.Columns.Add("SOCIAL_NUMBER", GetType(String))

            If lstSource.Rows.Count > 0 Then
                Dim filterExp = rgGridData.MasterTableView.FilterExpression
                If lstSource.Select(filterExp).AsEnumerable.Count = 0 Then
                    rgGridData.DataSource = dtb
                    rgGridData.MasterTableView.GroupsDefaultExpanded = True
                    rgGridData.MasterTableView.VirtualItemCount = lstSource.Rows.Count
                    rgGridData.CurrentPageIndex = rgGridData.MasterTableView.CurrentPageIndex
                    If IsDataBind Then
                        rgGridData.DataBind()
                    End If
                    Return
                Else
                    lstSource = lstSource.Select(filterExp).AsEnumerable.CopyToDataTable
                End If
                startRowIndex = IIf(lstSource.Rows.Count <= rgGridData.PageSize, 0, rgGridData.CurrentPageIndex * rgGridData.PageSize)
                maximumRows = IIf(lstSource.Rows.Count <= rgGridData.PageSize, lstSource.Rows.Count, Math.Min(startRowIndex + rgGridData.PageSize, lstSource.Rows.Count))
                For i As Integer = startRowIndex To maximumRows - 1
                    Dim dr As DataRow = lstSource.Rows(i)
                    Dim drI As DataRow = dtb.NewRow
                    drI("ID") = dr("ID")
                    drI("EMPID") = dr("EMPID")
                    drI("EMPLOYEE_CODE") = dr("EMPLOYEE_CODE")
                    drI("FULL_NAME") = dr("FULL_NAME")
                    drI("DEP_NAME") = dr("DEP_NAME")
                    drI("TITLE_NAME") = dr("TITLE_NAME")
                    drI("EFFECT_DATE") = dr("EFFECT_DATE")
                    drI("ARISING_TYPE_NAME") = dr("ARISING_TYPE_NAME")
                    drI("OLD_SAL") = dr("OLD_SAL")
                    drI("NEW_SAL") = dr("NEW_SAL")
                    drI("SI") = IIf(dr("SI") = "0", False, True)
                    drI("HI") = IIf(dr("HI") = "0", False, True)
                    drI("UI") = IIf(dr("UI") = "0", False, True)
                    drI("ARISING_TYPE_ID") = dr("ARISING_TYPE_ID")
                    drI("ARISING_GROUP_TYPE") = dr("ARISING_GROUP_TYPE")
                    drI("REASONS") = dr("REASONS")
                    drI("NOTE") = dr("NOTE")
                    drI("BHTNLD_BNN") = IIf(dr("BHTNLD_BNN") = "0", False, True)
                    drI("ORG_DESC") = dr("ORG_DESC")
                    drI("SOCIAL_NUMBER") = dr("SOCIAL_NUMBER")
                    dtb.Rows.Add(drI)
                Next
            End If

            rgGridData.DataSource = dtb
            rgGridData.MasterTableView.GroupsDefaultExpanded = True
            rgGridData.MasterTableView.VirtualItemCount = lstSource.Rows.Count
            rgGridData.CurrentPageIndex = rgGridData.MasterTableView.CurrentPageIndex
            If IsDataBind Then
                rgGridData.DataBind()
            End If

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
        target = value
        Return value
    End Function

    Private Sub Export()
        Try
            Dim _error As String = ""

            Dim dtVariable = New DataTable
            dtVariable.Columns.Add(New DataColumn("FROM_DATE"))
            dtVariable.Columns.Add(New DataColumn("TO_DATE"))
            Dim drVariable = dtVariable.NewRow()
            drVariable("FROM_DATE") = Now.ToString("dd/MM/yyyy")
            drVariable("TO_DATE") = Now.ToString("dd/MM/yyyy")
            dtVariable.Rows.Add(drVariable)
            '--------------------ThanhNT added 22/12/2015---------------------------
            'Add condition for query data in db-
            'Get list org checked
            Dim lstOrg = ctrlOrg.CheckedValueKeys
            Dim lstOrgStr = ""
            If ctrlOrg.CheckedValueKeys.Count > 0 Then
                For i As Integer = 0 To lstOrg.Count - 1
                    If i = lstOrg.Count - 1 Then
                        lstOrgStr = lstOrgStr & lstOrg(i).ToString
                    Else
                        lstOrgStr = lstOrgStr & lstOrg(i).ToString & ","
                    End If
                Next
            End If
            Using xls As New ExcelCommon

                Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsArising(Common.Common.GetUsername(), txtFROMDATE.SelectedDate, txtTODATE.SelectedDate _
                                                                                                 , InsCommon.getNumber(ddlGROUP_ARISING_TYPE_ID.SelectedValue) _
                                                                                                 , lstOrgStr _
                                                                                                 , InsCommon.getNumber(ddlINSORG.SelectedValue))


                lstSource.TableName = "TABLE"

                Dim bCheck = xls.ExportExcelTemplate(Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/" & Request.Params("group") & "/InsArising.xlsx"),
                    "BienDongBH", lstSource, dtVariable, Response, _error, ExcelCommon.ExportType.Excel)
                If Not bCheck Then
                    Select Case _error
                        Case 1
                            ShowMessage(Translate("Mẫu báo cáo không tồn tại"), NotifyType.Warning)
                        Case 2
                            ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
                    End Select
                    Exit Sub
                End If
            End Using
        Catch ex As Exception

        End Try
    End Sub

#End Region

End Class