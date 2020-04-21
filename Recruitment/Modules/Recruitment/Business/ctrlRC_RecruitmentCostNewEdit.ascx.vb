Imports Framework.UI
Imports Common
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports System.IO

Public Class ctrlRC_RecruitmentCostNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Private rep As New HistaffFrameworkRepository
    'Private proRespository As New ProfileRepository
    Public Overrides Property MustAuthorize As Boolean = True
    Private store As New RecruitmentStoreProcedure()
#Region "Property"

    '0 - normal
    '1 - Employee
    '2 - Org
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
    Dim IDSelect As Decimal?

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(tbarMain)
            GetParams()
            Refresh()
            UpdateControlState()
            'GetTotalEmployeeByTitleID()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        InitControl()
    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try

            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim obj = rep.GetRcCostByID(New RecruitmentCostDTO With {.ID = IDSelect})
                    txtOrgName.Text = obj.ORG_NAME
                    hidID.Value = obj.ID
                    hidOrgID.Value = obj.ORG_ID
                    rntxtYear.Value = obj.YEAR
                    rntxtMonth.Value = obj.MONTH
                    rntxtHeadhunt.Value = obj.HEADHUNT_COST
                    rntxtRecruitCost.Value = obj.RECRUIT_COST
                    rntxtToolCost.Value = obj.RC_TOOL_COST
                    rntxtTrademark.Value = obj.RC_TRADEMARK
                    rntxtJoinCost.Value = obj.JOIN_COST
                    rntxtPrintCost.Value = obj.PRINT_COST
                    rntxtOtherCost.Value = obj.OTHER_COST
                    txtRemark.Text = obj.REMARK
                    rntxtTotalCost.Value = obj.TOTAL_COST

                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW

            End Select

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick

        Dim rep As New RecruitmentRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim obj As New RecruitmentCostDTO
                        obj.ORG_ID = hidOrgID.Value
                        obj.YEAR = rntxtYear.Value
                        obj.MONTH = rntxtMonth.Value
                        obj.HEADHUNT_COST = rntxtHeadhunt.Value
                        obj.RECRUIT_COST = rntxtRecruitCost.Value
                        obj.RC_TOOL_COST = rntxtToolCost.Value
                        obj.RC_TRADEMARK = rntxtTrademark.Value
                        obj.JOIN_COST = rntxtJoinCost.Value
                        obj.PRINT_COST = rntxtPrintCost.Value
                        obj.OTHER_COST = rntxtOtherCost.Value
                        obj.REMARK = txtRemark.Text
                        obj.TOTAL_COST = rntxtTotalCost.Value
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertRecruitCost(obj) Then
                                    ''POPUPTOLINK
                                    Dim str As String = "getRadWindow().close('1');"
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ClosePopup", str, True)

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                                rep.Dispose()
                            Case CommonMessage.STATE_EDIT
                                obj.ID = hidID.Value
                                If rep.ModifyRecruitCost(obj) Then
                                    ''POPUPTOLINK
                                    Dim str As String = "getRadWindow().close('1');"
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ClosePopup", str, True)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                                rep.Dispose()
                        End Select

                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Dim str As String = "getRadWindow().close('1');"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ClosePopup", str, True)
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        UpdateControlState()
    End Sub

    

    Protected Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrg.Click
        Try
            isLoadPopup = 2
            UpdateControlState()
            ctrlFindOrgPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue 'gán org đã chọn vào hiddenfield
                txtOrgName.Text = orgItem.NAME_VN
                If orgItem.DESCRIPTION_PATH IsNot Nothing AndAlso orgItem.DESCRIPTION_PATH <> "" Then
                    txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                End If

            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked, ctrlFindOrgPopup.CancelClicked
        isLoadPopup = 0
    End Sub

#End Region

#Region "Custom"

    Public Overrides Sub UpdateControlState()
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    rntxtYear.Value = DateTime.Now.Year
            End Select
            Select Case isLoadPopup
                Case 2
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                    phFindOrg.Controls.Add(ctrlFindOrgPopup)
                    ctrlFindOrgPopup.Show()
            End Select
        Catch ex As Exception
            Throw ex
        End Try
        ChangeToolbarState()
    End Sub

    Private Sub GetDataCombo()
        Try
            Dim dtData As DataTable
            Using rep As New RecruitmentRepository
                dtData = rep.GetOtherList("RC_RECRUIT_REASON", True)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetParams()
        Try
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    IDSelect = Decimal.Parse(Request.Params("ID"))
                End If
                If IDSelect IsNot Nothing Then
                    Refresh("UpdateView")
                Else
                    Refresh("InsertView")
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region
    
End Class