Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common.CommonBusiness
Imports Telerik.Web.UI

Public Class ctrlGroupOrganization
    Inherits CommonView
    Public Property GroupInfo As GroupDTO

#Region "Property"

    Public Property GroupOganization As List(Of Decimal)
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_GroupOganization") = value
        End Set
        Get
            Return PageViewState(Me.ID & "_GroupOganization")
        End Get
    End Property

    Public Property GroupOganizationFunction As List(Of Decimal)
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_GroupOganizationFunction") = value
        End Set
        Get
            Return PageViewState(Me.ID & "_GroupOganizationFunction")
        End Get
    End Property

    Public Property GroupID_Old As Decimal
        Get
            Return PageViewState(Me.ID & "_GroupID_Old")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_GroupID_Old") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            Me.MainToolBar = tbarMain
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit,
                                        ToolbarItem.Seperator,
                                        ToolbarItem.Save,
                                        ToolbarItem.Cancel)
            orgLoca.AutoPostBack = False
            orgLoca.CheckBoxes = TreeNodeTypes.All
            orgLoca.CheckChildNodes = True
            orgLoca.CheckParentNodes = False
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            orgLoca.SetProperty("GroupOf", "1")
            UpdateControlStatus()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub UpdateControlStatus()
        If CurrentState = CommonMessage.STATE_EDIT Then
            Me.MainToolBar.Items(0).Enabled = False

            Me.MainToolBar.Items(2).Enabled = True
            Me.MainToolBar.Items(3).Enabled = True

            orgLoca.Enabled = True
        Else
            If GroupInfo IsNot Nothing Then
                Me.MainToolBar.Items(0).Enabled = True
            Else
                Me.MainToolBar.Items(0).Enabled = False
            End If
            Me.MainToolBar.Items(2).Enabled = False
            Me.MainToolBar.Items(3).Enabled = False

            orgLoca.Enabled = False
        End If
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If GroupInfo Is Nothing Then Exit Sub

            If GroupID_Old = Nothing Or GroupID_Old <> GroupInfo.ID Or _
                Message = CommonMessage.ACTION_SAVED Or GroupOganization Is Nothing Then
                Dim rep As New CommonRepository
                GroupOganization = rep.GetGroupOrganization(GroupInfo.ID)
            End If

            'Đưa dữ liệu vào Grid
            orgLoca.CheckedValueKeys = GroupOganization
            'orgFunc.CheckedValueDecimals = GroupOganizationFunction

            'Thay đổi trạng thái các control
            UpdateControlStatus()

            GroupID_Old = GroupInfo.ID
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New CommonRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                    'Thay đổi trạng thái các control
                    UpdateControlStatus()
                    'Gửi thông điệp cho Parent View
                    Me.Send(CommonMessage.ACTION_UPDATING)
                Case CommonMessage.TOOLBARITEM_SAVE
                    If CurrentState = CommonMessage.STATE_EDIT Then
                        Dim lst As List(Of CommonBusiness.GroupOrgAccessDTO)
                        'Dim lstFunc As List(Of CommonBusiness.SE_GROUP_ORG_FUN_ACCESS)
                        lst = GetListOrgID()
                        'lstFunc = GetListOrgFuncID()
                        If lst.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ORG), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        rep.DeleteGroupOrganization(GroupInfo.ID)
                        If rep.UpdateGroupOrganization(lst) Then
                            Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            Refresh(CommonMessage.ACTION_SAVED)

                        Else
                            Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                        End If
                    End If
                    CurrentState = CommonMessage.STATE_NORMAL
                    'Thay đổi trạng thái các control
                    UpdateControlStatus()
                    'Gửi thông điệp cho Parent View
                    Me.Send(CommonMessage.ACTION_UPDATED)
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    'Thay đổi trạng thái các control
                    UpdateControlStatus()
                    Refresh()
                    'Gửi thông điệp cho Parent View
                    Me.Send(CommonMessage.ACTION_UPDATED)
            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

    Protected Function GetListOrgID() As List(Of CommonBusiness.GroupOrgAccessDTO)
        Dim lst As New List(Of CommonBusiness.GroupOrgAccessDTO)
        Dim lstOrgID As List(Of String) = orgLoca.CheckedValueGroups
        For i = 0 To lstOrgID.Count - 1
            lst.Add(New CommonBusiness.GroupOrgAccessDTO() With {.GROUP_ID = GroupInfo.ID,
                                                                   .ORG_ID = Decimal.Parse(lstOrgID(i))})
        Next
        Return lst
    End Function

#End Region

End Class