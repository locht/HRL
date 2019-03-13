Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Telerik.Web.UI

Public Class ctrlInsInformationsMng
    Inherits Common.CommonView
    'Private WithEvents viewRegister As ctrlShiftPlanningRegister

    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlOrgSULPPopup As ctrlFindOrgPopup

    Public Property popup As RadWindow
    Public Property popupId As String
    Public Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

#Region "Property & Variable"

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


    Public ReadOnly Property PageId As String
        Get
            Return Me.ID
        End Get
    End Property

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

            rgGridData.AllowCustomPaging = True
            rgGridData.PageSize = Common.Common.DefaultPageSize
            popup = CType(Me.Page, AjaxPage).PopupWindow
            popup.Title = "Thông tin Bảo hiểm chi tiết"
            popupId = popup.ClientID

            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Export,
                                       ToolbarItem.HIExt,
                                       ToolbarItem.HIUpdateInfo)

            CType(Me.MainToolBar.Items(4), RadToolBarButton).Text = "Gia hạn thẻ BHYT"
            CType(Me.MainToolBar.Items(5), RadToolBarButton).Text = "Import thông tin BH"            
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            'rgGridData.SetFilter()
            If Not IsPostBack Then
                txtID.Text = "0"
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            Call LoadDataGrid()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                Case CommonMessage.TOOLBARITEM_EDIT
                    If txtID.Text = "0" Then
                        ShowMessage(Translate("Bạn chưa chọn nội dung cần sửa."), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgGridData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nội dung cần xóa."), NotifyType.Warning)
                        Exit Sub
                    End If
                    If CheckData_Delete() Then
                        ctrlMessageBox.MessageText = Translate("Bạn có muốn xóa dữ liệu ?")
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        ShowMessage(Translate("Dữ liệu đang dùng không được xóa"), NotifyType.Warning)
                    End If

                Case CommonMessage.TOOLBARITEM_HIEXT
                    Response.Redirect("Default.aspx?mid=Insurance&fid=ctrlInsHealthExt&group=Business")

                Case CommonMessage.TOOLBARITEM_HIUPDATEINFO
                    Response.Redirect("Default.aspx?mid=Insurance&fid=ctrlInsHealthImport&group=Business")

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Call Export()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgGridData_ExcelExportCellFormatting(ByVal sender As Object, ByVal e As ExportCellFormattingEventArgs) Handles rgGridData.ExportCellFormatting
        Try
            e.Cell.Style("mso-number-format") = "\@"
        Catch ex As Exception

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
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Function CheckData_Delete() As Boolean
        Try
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub btnFIND_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFind.Click
        Call LoadDataGrid()
    End Sub

#End Region

#Region "Function & Sub"

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

            Using xls As New ExcelCommon

                Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsInfomation(Common.Common.GetUsername(), 0 _
                                                                                        , txtEMPLOYEEID_SEARCH.Text _
                                                                                        , InsCommon.getNumber(ctrlOrg.CurrentValue) _
                                                                                        , InsCommon.getNumber(IIf(chkSTATUS.Checked, 1, 0)) _
                                                                                        , InsCommon.getNumber(ctrlOrg.IsDissolve)
                                                                                        )
                lstSource.TableName = "TABLE"

                Dim bCheck = xls.ExportExcelTemplate(Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/" & Request.Params("group") & "/InsInfomation.xlsx"),
                    "ThongTinBH", lstSource, dtVariable, Response, _error, ExcelCommon.ExportType.Excel)
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

    Private Sub LoadDataGrid(Optional ByVal IsDataBind As Boolean = True)
        Try
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsInfomation(Common.Common.GetUserName(), InsCommon.getNumber(0) _
                                                                    , txtEMPLOYEEID_SEARCH.Text _
                                                                    , InsCommon.getNumber(ctrlOrg.CurrentValue) _
                                                                    , InsCommon.getNumber(IIf(chkSTATUS.Checked, 1, 0)) _
                                                                    , InsCommon.getNumber(IIf(ctrlOrg.IsDissolve, 1, 0))
                                                                    )
            Dim maximumRows As Double = 0
            Dim startRowIndex As Double = 0
            Dim dtb As New DataTable
            If lstSource.Rows.Count = 0 Then
                rgGridData.DataSource = lstSource
                rgGridData.MasterTableView.VirtualItemCount = 0
                rgGridData.CurrentPageIndex = rgGridData.MasterTableView.CurrentPageIndex
                If IsDataBind Then
                    rgGridData.DataBind()
                End If
                Return
            Else
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
                dtb = lstSource.Clone()
                For i As Integer = startRowIndex To maximumRows - 1
                    dtb.ImportRow(lstSource.Rows(i))
                Next
            End If
            rgGridData.DataSource = dtb
            rgGridData.MasterTableView.VirtualItemCount = lstSource.Rows.Count
            rgGridData.CurrentPageIndex = rgGridData.MasterTableView.CurrentPageIndex
            If IsDataBind Then
                rgGridData.DataBind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub DeleteData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            Dim isFail As Boolean
            Dim isResult As Boolean
            Dim lstID As String = ""
            For i As Integer = 0 To rgGridData.SelectedItems.Count - 1
                Dim item As GridDataItem = rgGridData.SelectedItems(i)
                If i = rgGridData.SelectedItems.Count - 1 Then
                    lstID = lstID & item("ID").Text.ToString
                Else
                    lstID = lstID & item("ID").Text.ToString & ","
                End If
                isResult = rep.DeleteInsInfomation(Common.Common.GetUserName(), InsCommon.getString(lstID))
                If isResult = False Then
                    isFail = True
                End If
            Next
            If isFail = False Then
                Refresh("UpdateView")
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try
            LoadDataGrid()
        Catch ex As Exception

        End Try
    End Sub
End Class