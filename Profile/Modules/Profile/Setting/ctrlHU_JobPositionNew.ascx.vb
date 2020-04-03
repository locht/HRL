Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Common
Imports Telerik.Web.UI
Imports WebAppLog
Imports System.IO

Public Class ctrlHU_JobPositionNew
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = True

#Region "Property & Variable"

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
    Public ReadOnly Property PageId As String
        Get
            Return Me.ID
        End Get
    End Property

    Public Property JobId As Decimal
        Get
            Return ViewState(Me.ID & "_JobId")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_JobId") = value
        End Set
    End Property

    Public Property Title_Id As Decimal
        Get
            Return ViewState(Me.Id & "_Title_Id")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.Id & "_Title_Id") = value
        End Set
    End Property

#End Region

#Region "Page"

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
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            GetParams()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub UpdateControlState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL

                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT

                Case CommonMessage.STATE_DELETE

                Case "Nothing"
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                Case CommonMessage.TOOLBARITEM_SAVE
                    Call SaveData()

                    UpdateControlState(CommonMessage.STATE_NORMAL)
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Function & Sub"

    Private Sub SaveData()
        Try
            Dim rep As New ProfileRepository
            If ntxtNumber.Value Is Nothing Then
                ShowMessage(Translate("Bạn phải nhập Số phân bản."), Utilities.NotifyType.Warning)
            End If

            If rep.INSERT_JOB_POSITION_AUTO(JobId, Title_Id, ntxtNumber.Value) Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                ntxtNumber.ReadOnly = True
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetParams()
        Try
            If CurrentState Is Nothing Then
                JobId = Request.Params("ID")
                Title_Id = Request.Params("TITLE_ID")
                txtCode.Text = Request.Params("CODE")
                txtJobName.Text = Request.Params("JOB_NAME")

                UpdateControlState(CurrentState)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region
End Class