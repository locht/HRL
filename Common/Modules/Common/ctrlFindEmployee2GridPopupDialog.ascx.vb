Imports Framework.UI
Imports Telerik.Web.UI
Imports Common
Imports Common.CommonBusiness
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Framework.UI.Utilities

Public Class ctrlFindEmployee2GridPopupDialog
    Inherits CommonView

    Delegate Sub EmployeeSelectedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Event EmployeeSelected As EmployeeSelectedDelegate
    Delegate Sub CancelClick(ByVal sender As Object, ByVal e As EventArgs)
    Event CancelClicked As CancelClick
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"

    Public Property LoadAllOrganization As Boolean
    Public Property MustHaveContract As Boolean
    Public Property IsHideTerminate As Boolean
    Public Property is_CheckNode As Boolean
    Public Property IsOnlyWorkingWithoutTer As Boolean
    Public Property IS_3B As String
    Public Property Enabled As Boolean
    Public Property CurrentValue As String
    Public Property MultiSelect As Boolean
    Public Property popupId As String
    Public WithEvents AjaxManager As RadAjaxManager
    Public AjaxLoading As RadAjaxLoadingPanel
    Public Property AjaxManagerId As String
    Public Property AjaxLoadingId As String
    Public Property Is_Load_CtrlOrg As Boolean
    Public Property MaximumRows As Integer
        Get
            If PageViewState(Me.ID & "_MaximumRows") Is Nothing Then
                Return 0
            End If
            Return PageViewState(Me.ID & "_MaximumRows")
        End Get
        Set(ByVal value As Integer)
            PageViewState(Me.ID & "_MaximumRows") = value
        End Set
    End Property

    Public Property SelectedItem As List(Of String)
        Get
            If PageViewState(Me.ID & "_SelectedItem") Is Nothing Then
                PageViewState(Me.ID & "_SelectedItem") = New List(Of String)
            End If
            Return PageViewState(Me.ID & "_SelectedItem")
        End Get
        Set(ByVal value As List(Of String))
            PageViewState(Me.ID & "_SelectedItem") = value
        End Set
    End Property

    Public Property CommendDate As Date
        Get
            Return ViewState(Me.ID & "_CommendDate")
        End Get
        Set(value As Date)
            ViewState(Me.ID & "_CommendDate") = value
        End Set
    End Property

    Public Property CommendList_Code As String
        Get
            Return ViewState(Me.ID & "_CommendList_Code")
        End Get
        Set(value As String)
            ViewState(Me.ID & "_CommendList_Code") = value
        End Set
    End Property

    Public Property PageSize As Integer
        Get
            If PageViewState(Me.ID & "_PageSize") Is Nothing Then
                Return rgvEmployees.PageSize
            End If
            Return PageViewState(Me.ID & "_PageSize")
        End Get
        Set(ByVal value As Integer)
            PageViewState(Me.ID & "_PageSize") = value
        End Set
    End Property

    Public Property EmployeeList As List(Of EmployeePopupFindListDTO)
        Get
            Return ViewState(Me.ID & "_EmployeeList")
        End Get
        Set(value As List(Of EmployeePopupFindListDTO))
            ViewState(Me.ID & "_EmployeeList") = value
        End Set
    End Property

    Public Property dtTable As DataTable
        Get
            Return ViewState(Me.ID & "_dtTable")
        End Get
        Set(value As DataTable)
            ViewState(Me.ID & "_dtTable") = value
        End Set
    End Property

    Property DataSource As DataTable
        Get
            If ViewState(Me.ID & "_DataSource") Is Nothing Then
                Dim dt As New DataTable("Employees")
                dt.Columns.Add("EMPLOYEE_ID", GetType(Decimal))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("FULLNAME_VN", GetType(String))
                dt.Columns.Add("ORG_ID", GetType(Decimal))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("JOIN_DATE", GetType(Date))
                ViewState(Me.ID & "_DataSource") = dt
            End If
            Return ViewState(Me.ID & "_DataSource")
        End Get
        Set(value As DataTable)
            ViewState(Me.ID & "_DataSource") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewInit(e As System.EventArgs)
        Try
            Dim popup As RadWindow
            popup = CType(Me.Page, AjaxPage).PopupWindow
            popup.Height = 400
            popup.Width = 500
            popupId = popup.ClientID

            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxLoading = CType(Me.Page, AjaxPage).AjaxLoading
            AjaxManagerId = AjaxManager.ClientID
            AjaxLoadingId = AjaxLoading.ClientID

            rgvEmployees.SetFilter()
            rgvEmployees.AllowCustomPaging = True
            rgvEmployees.PageSize = 20

            Common.BuildToolbar(tbarExport, ToolbarItem.Import, ToolbarItem.Export, ToolbarItem.Save)
            tbarExport.Items(0).Text = Translate("")
            tbarExport.Items(1).Text = Translate("")
            tbarExport.Items(2).Text = Translate("Chọn")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            MustHaveContract = If(Request.Params("MustHaveContract") Is Nothing, True, Request.Params("MustHaveContract"))
            CurrentValue = If(Request.Params("CurrentValue") Is Nothing, "", Request.Params("Enabled"))
            MultiSelect = If(Request.Params("MultiSelect") Is Nothing, True, Request.Params("MultiSelect"))
            LoadAllOrganization = If(Request.Params("LoadAllOrganization") Is Nothing, False, Request.Params("LoadAllOrganization"))
            IsHideTerminate = If(Request.Params("IsHideTerminate") Is Nothing, False, Request.Params("IsHideTerminate"))
            is_CheckNode = If(Request.Params("is_CheckNode") Is Nothing, False, Request.Params("is_CheckNode"))
            IsOnlyWorkingWithoutTer = If(Request.Params("IsOnlyWorkingWithoutTer") Is Nothing, False, Request.Params("IsOnlyWorkingWithoutTer"))
            IS_3B = If(Request.Params("IS_3B") Is Nothing, 0, Request.Params("IS_3B"))
            Is_Load_CtrlOrg = If(Request.Params("Is_Load_CtrlOrg") Is Nothing, True, Request.Params("Is_Load_CtrlOrg"))

            rgvEmployees.AllowMultiRowSelection = MultiSelect
            rgvDataPrepare.DataSource = DataSource
            ctrlOrg.AutoPostBack = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrg.CheckChildNodes = True
            If is_CheckNode Then
                ctrlOrg.CheckBoxes = TreeNodeTypes.All
            End If
            ctrlOrg.CurrentValue = CurrentValue
            ctrlOrg.LoadAllOrganization = LoadAllOrganization
            'ctrlOrg.Visible = Is_Load_CtrlOrg
            LeftPane.Visible = Is_Load_CtrlOrg
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try

        Catch ex As Exception
            Throw
        End Try

    End Sub
#End Region

#Region "Event"

    Private Sub tbarExport_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles tbarExport.ButtonClick
        Try
            Select Case CType(e.Item, Telerik.Web.UI.RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EXPORT
                    For i As Integer = 0 To rgvDataPrepare.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgvDataPrepare.SelectedItems(i)
                        For Each rows As DataRow In DataSource.Rows
                            If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For
                            If rows("EMPLOYEE_ID") = item.GetDataKeyValue("EMPLOYEE_ID") Then
                                rows.Delete()
                                GoTo NEXT_FOR
                            End If
                        Next
NEXT_FOR:
                    Next
                    DataSource.AcceptChanges()
                    rgvDataPrepare.Rebind()
                Case CommonMessage.TOOLBARITEM_IMPORT
                    Dim EmployeeList_Choice As New List(Of EmployeePopupFindListDTO)
                    For Each item As GridDataItem In rgvEmployees.SelectedItems
                        ' If item.Selected = True Then
                        Dim empPopup As New EmployeePopupFindListDTO
                        empPopup.EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
                        empPopup.EMPLOYEE_CODE = item.GetDataKeyValue("EMPLOYEE_CODE")
                        empPopup.FULLNAME_VN = item.GetDataKeyValue("FULLNAME_VN")
                        empPopup.ORG_ID = item.GetDataKeyValue("ORG_ID")
                        empPopup.ORG_NAME = item.GetDataKeyValue("ORG_NAME")
                        empPopup.TITLE_NAME = item.GetDataKeyValue("TITLE_NAME")
                        empPopup.JOIN_DATE = item.GetDataKeyValue("JOIN_DATE")
                        EmployeeList_Choice.Add(empPopup)
                        ' End If
                    Next
                    If EmployeeList_Choice.Count > 0 Then
                        For Each item As EmployeePopupFindListDTO In EmployeeList_Choice
                            If DataSource IsNot Nothing AndAlso DataSource.Rows.Count > 0 Then
                                Dim _ck = (From p In DataSource Where p.Field(Of Decimal)("EMPLOYEE_ID") = item.EMPLOYEE_ID).ToList
                                If _ck.Count = 0 Then
                                    Dim newRow As DataRow = DataSource.NewRow()
                                    newRow("EMPLOYEE_ID") = item.EMPLOYEE_ID
                                    newRow("EMPLOYEE_CODE") = item.EMPLOYEE_CODE
                                    newRow("FULLNAME_VN") = item.FULLNAME_VN
                                    newRow("ORG_ID") = If(item.ORG_ID Is Nothing, 0, item.ORG_ID)
                                    newRow("ORG_NAME") = item.ORG_NAME
                                    newRow("TITLE_NAME") = item.TITLE_NAME
                                    newRow("JOIN_DATE") = item.JOIN_DATE
                                    DataSource.Rows.Add(newRow)
                                End If
                            Else
                                Dim newRow As DataRow = DataSource.NewRow()
                                newRow("EMPLOYEE_ID") = item.EMPLOYEE_ID
                                newRow("EMPLOYEE_CODE") = item.EMPLOYEE_CODE
                                newRow("FULLNAME_VN") = item.FULLNAME_VN
                                newRow("ORG_ID") = If(item.ORG_ID Is Nothing, 0, item.ORG_ID)
                                newRow("ORG_NAME") = item.ORG_NAME
                                newRow("TITLE_NAME") = item.TITLE_NAME
                                newRow("JOIN_DATE") = item.JOIN_DATE
                                DataSource.Rows.Add(newRow)
                            End If
                        Next
                        rgvDataPrepare.VirtualItemCount = DataSource.Rows.Count
                        rgvDataPrepare.MasterTableView.DataSource = DataSource
                        rgvDataPrepare.MasterTableView.Rebind()
                    End If
                Case CommonMessage.TOOLBARITEM_SAVE
                    GetEmployeeSelected()
                    hidSelected.Value = ""

                    If rgvDataPrepare.SelectedItems.Count = 0 Then
                        ShowMessage("Bạn chưa chọn bản ghi nào! không thể thực hiện thao tác này.", Utilities.NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each sId As String In SelectedItem
                        hidSelected.Value &= ";" & sId
                    Next
                    If hidSelected.Value <> "" Then
                        hidSelected.Value = hidSelected.Value.Substring(1)
                    End If
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", "btnYesClick();", True)
            End Select
        Catch ex As Exception
            ShowMessage(ex.Message, Framework.UI.Utilities.NotifyType.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Hiển thị thông tin lên lưới dữ liệu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgvEmployees_NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgvEmployees.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgvEmployees_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgvEmployees.PageIndexChanged
        Try
            GetEmployeeSelected()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load dữ liệu khi chọn Org
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        SelectedItem = Nothing
        rgvEmployees.CurrentPageIndex = 0
        rgvEmployees.Rebind()
    End Sub
#End Region

#Region "Custom"

    ''' <summary>
    ''' Load dữ liệu trên lưới
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Try
            Dim rep As New CommonRepository
            Dim _filter As New EmployeePopupFindListDTO
            Dim Sorts As String

            Dim orgID As Decimal? = 0
            Dim request_ID As String = ""
            Dim lstOrgStr As String = ""
            Dim listOrg As New List(Of Decimal)
            'Get all org
            listOrg = ctrlOrg.CheckedValueKeys
            For i As Integer = 0 To listOrg.Count - 1
                If i = listOrg.Count - 1 Then
                    lstOrgStr = lstOrgStr & listOrg(i).ToString
                Else
                    lstOrgStr = lstOrgStr & listOrg(i).ToString & ","
                End If
            Next

            If ctrlOrg.CurrentValue <> "" Then
                orgID = Decimal.Parse(ctrlOrg.CurrentValue)
            End If
            Dim _para = New CommonBusiness.ParamDTO With {.ORG_ID = orgID,
                                                          .IS_PORTAL_AT_SHIFT = If(IsNumeric(Session("PortalAtShift")), CDec(Session("PortalAtShift")), 0), _
                                               .IS_DISSOLVE = ctrlOrg.IsDissolve}

            SetValueObjectByRadGrid(rgvEmployees, _filter)

            Sorts = rgvEmployees.MasterTableView.SortExpressions.GetSortString()
            Dim repCp As New HistaffFrameworkRepository
            If Sorts IsNot Nothing Then
                If is_CheckNode Then
                    'Insert into ORG_TEMP_TABLE --> return: Request_id
                    'from Request_id --> return list org chosen.
                    Dim obj1 As Object = repCp.ExecuteStoreScalar("PKG_HCM_SYSTEM.PROC_INSERT_ORG_TABLE_TEMP", New List(Of Object)(New Object() {lstOrgStr, OUT_STRING}))
                    If obj1 IsNot Nothing Then
                        request_ID = obj1(0).ToString
                    End If
                    EmployeeList = rep.GetEmployeeToPopupFind2(_filter, rgvEmployees.CurrentPageIndex, PageSize,
                                                          MaximumRows, request_ID, Sorts, _para)
                    'DELETE ORG_TEMP_TABLE
                    Dim obj2 As Object = repCp.ExecuteStoreScalar("PKG_HCM_SYSTEM.PROC_DEL_ORG_TABLE_TEMP", New List(Of Object)(New Object() {request_ID, OUT_NUMBER}))
                    If obj2 Is Nothing OrElse Decimal.Parse(obj2(0).ToString) = 0 Then
                        ShowMessage("Đã xảy ra lỗi trong quá trình lấy dữ liệu nhân viên! Vui lòng liên hệ Administrator.", Utilities.NotifyType.Error)
                        Exit Function
                    End If
                Else
                    EmployeeList = rep.GetEmployeeToPopupFind(_filter, rgvEmployees.CurrentPageIndex, PageSize,
                                                          MaximumRows, Sorts, _para)
                End If
            Else
                If is_CheckNode Then
                    'Insert into ORG_TEMP_TABLE --> return: Request_id
                    'from Request_id --> return list org chosen.

                    Dim obj1 As Object = repCp.ExecuteStoreScalar("PKG_HCM_SYSTEM.PROC_INSERT_ORG_TABLE_TEMP", New List(Of Object)(New Object() {lstOrgStr, OUT_STRING}))
                    If obj1 IsNot Nothing Then
                        request_ID = obj1(0).ToString
                    End If
                    EmployeeList = rep.GetEmployeeToPopupFind2(_filter, rgvEmployees.CurrentPageIndex, PageSize,
                                                          MaximumRows, request_ID, "EMPLOYEE_CODE asc", _para)
                    'DELETE ORG_TEMP_TABLE
                    Dim obj2 As Object = repCp.ExecuteStoreScalar("PKG_HCM_SYSTEM.PROC_DEL_ORG_TABLE_TEMP", New List(Of Object)(New Object() {request_ID, OUT_NUMBER}))
                    If obj2 Is Nothing OrElse Decimal.Parse(obj2(0).ToString) = 0 Then
                        ShowMessage("Đã xảy ra lỗi trong quá trình lấy dữ liệu nhân viên! Vui lòng liên hệ Administrator.", Utilities.NotifyType.Error)
                        Exit Function
                    End If
                Else
                    EmployeeList = rep.GetEmployeeToPopupFind(_filter, rgvEmployees.CurrentPageIndex, PageSize,
                                                          MaximumRows, "EMPLOYEE_CODE asc", _para)
                End If
            End If
            rep.Dispose()
            rgvEmployees.VirtualItemCount = MaximumRows
            rgvEmployees.DataSource = EmployeeList
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    Private Sub GetEmployeeSelected()
        For Each dr As Telerik.Web.UI.GridDataItem In rgvDataPrepare.Items
            Dim id As String = dr.GetDataKeyValue("EMPLOYEE_ID")
            If dr.Selected Then
                If Not SelectedItem.Contains(id) Then SelectedItem.Add(id)
            Else
                If SelectedItem.Contains(id) Then SelectedItem.Remove(id)
            End If
        Next
        rgvDataPrepare.VirtualItemCount = MaximumRows
    End Sub
#End Region

End Class