Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Common
Imports Telerik.Web.UI
Imports Common.CommonBusiness

Public Class ctrlContract_Liquidate
    Inherits CommonView

    Delegate Sub FilterApplyDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Event FilterApply As FilterApplyDelegate
    Delegate Sub CancelClick(ByVal sender As Object, ByVal e As EventArgs)
    Event CancelClicked As CancelClick
    Public Overrides Property MustAuthorize As Boolean = False

    Dim log As New UserLog
    Dim psp As New ProfileStoreProcedure
    Dim com As New CommonProcedureNew
    Dim cons As New Contant_OtherList_Iprofile

    Protected WithEvents ctrlFindEmployeeSignPopup As ctrlFindEmployeePopup
#Region "Property"
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Property popupId As String

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            UpdateControlState()
        Catch ex As Exception

        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            log = LogHelper.GetUserLog
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                Case CommonMessage.TOOLBARITEM_SAVE
                    Call SaveData()


            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Function & Sub"

    Private Sub SaveData()
        Try
            If dtpLiquiDate.SelectedDate Is Nothing Then
                ShowMessage("Bạn chưa chọn ngày thanh lý", Utilities.NotifyType.Warning)
                Exit Sub
            End If
            Dim rep As New ProfileBusinessRepository
            Dim check = rep.UpdateDateToContract(Request.Params("idCT"), dtpLiquiDate.SelectedDate, txtRemark.Text)
            If check = True Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", "btnCancelClick();", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Success)
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

End Class