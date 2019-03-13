Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Common
Imports Common.CommonBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_ProgramSchedule
    Inherits Common.CommonView
    Private store As New RecruitmentStoreProcedure()
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
#Region "Properties"
    Dim str As Integer
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            hidProgramID.Value = Request.Params("PROGRAM_ID")
            If IsPostBack Then
                CreateDataFilter()
                rgData.Rebind()
            End If
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            'rgData.ClientSettings.EnablePostBackOnRowClick = True
            rgData.SetFilter()
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit)
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                Dim rep As New RecruitmentRepository
                Dim objPro = rep.GetProgramByID(New ProgramDTO With {.ID = Decimal.Parse(hidProgramID.Value)})
                lblOrgName.Text = objPro.ORG_NAME
                hidOrg.Value = objPro.ORG_ID
                lblTitle.Text = objPro.TITLE_NAME
                hidTitle.Value = objPro.TITLE_ID
                lblSendDate.Text = objPro.SEND_DATE
                lblRequestNo.Text = objPro.REQUEST_NUMBER
                lblCode.Text = objPro.CODE
                lblJobName.Text = objPro.JOB_NAME
                lblCandidate.Text = objPro.CANDIDATE_COUNT
                lblCanReceivedCount.Text = objPro.CANDIDATE_RECEIVED
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    'Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
    '    Try
    '        Dim objCandidate As New CandidateDTO
    '        Dim rep As New RecruitmentRepository

    '        Select Case CType(e.Item, RadToolBarButton).CommandName

    '            Case TOOLBARITEM_DELETE
    '                'Kiểm tra các điều kiện để xóa.
    '                If rgData.SelectedItems.Count = 0 Then
    '                    ShowMessage(Translate("Bạn phải chọn ngày thi cần xóa!"), Utilities.NotifyType.Error)
    '                    Exit Sub
    '                End If
    '                'Hiển thị Confirm delete.
    '                ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
    '                ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
    '                ctrlMessageBox.DataBind()
    '                ctrlMessageBox.Show()
    '        End Select
    '        'UpdateControlState()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    'Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
    '    Try
    '        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then

    '        End If
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try

    'End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgData.PageIndexChanged
        Try
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"
    Private Sub CreateDataFilter()
        Try
            rgData.DataSource = Nothing
            rgData.DataSource = store.GET_PROGRAM_SCHCEDULE_LIST(Decimal.Parse(hidProgramID.Value))

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Try
            Dim url = e.Argument
            If (url.Contains("reload=1")) Then
                rgData.CurrentPageIndex = 0
                rgData.Rebind()
                If rgData.Items IsNot Nothing AndAlso rgData.Items.Count > 0 Then
                    rgData.Items(0).Selected = True
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

End Class