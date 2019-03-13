Imports Framework.UI
Imports Telerik.Web.UI
Imports Common.CommonBusiness

<ValidationProperty("SelectedValue")>
Public Class ctrlFindOrgPopup
    Inherits CommonView
    Delegate Sub OrganizationSelectedDelegate(ByVal sender As Object, ByVal e As OrganizationSelectedEventArgs)
    Event OrganizationSelected As OrganizationSelectedDelegate
    Delegate Sub CancelClick(ByVal sender As Object, ByVal e As EventArgs)
    Event CancelClicked As CancelClick
    Public Overrides Property MustAuthorize As Boolean = False

    Public Property CheckChildNodes As Boolean
        Get
            Return ctrlOrg1.CheckChildNodes
        End Get
        Set(ByVal value As Boolean)
            ctrlOrg1.CheckChildNodes = value
        End Set
    End Property

    Public Property ShowCheckBoxes As TreeNodeTypes
        Get
            Return IIf(ViewState(Me.ID & "_ShowCheckBoxes") Is Nothing, TreeNodeTypes.None, ViewState(Me.ID & "_ShowCheckBoxes"))
        End Get
        Set(ByVal value As TreeNodeTypes)
            ViewState(Me.ID & "_ShowCheckBoxes") = value
        End Set
    End Property

    Public Property OrganizationType As OrganizationType
        Get
            Return ViewState(Me.ID & "_OrganizationType")
        End Get
        Set(ByVal value As OrganizationType)
            ViewState(Me.ID & "_OrganizationType") = value
        End Set
    End Property

    Public Property LoadAllOrganization As Boolean
        Get
            Return ctrlOrg1.LoadAllOrganization
        End Get
        Set(ByVal value As Boolean)
            ctrlOrg1.LoadAllOrganization = value
        End Set
    End Property

    Public Function GetAllChild(ByVal _orgId As Decimal) As List(Of Decimal)
        Return ctrlOrg1.GetAllChild(_orgId)
    End Function

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ctrlOrg1.AutoPostBack = False
            ctrlOrg1.LoadDataAfterLoaded = True
            ctrlOrg1.OrganizationType = OrganizationType
            ctrlOrg1.CheckBoxes = ShowCheckBoxes
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function CurrentStructureInfo() As List(Of OrganizationStructureDTO)
        Return ctrlOrg1.CurrentStructureInfo
    End Function

    Public Function CurrentItemDataObject() As Object
        Return ctrlOrg1.CurrentItemDataObject
    End Function
    Public Function CheckedValueKeys() As List(Of Decimal)
        Return ctrlOrg1.CheckedValueKeys
    End Function
    Public Function ListOrgChecked() As List(Of OrganizationDTO)
        Return ctrlOrg1.ListOrg_Checked

    End Function

    Public ReadOnly Property IsDissolve As Boolean
        Get
            Return ctrlOrg1.IsDissolve
        End Get
    End Property

    Public Sub Show()
        rwMessage.VisibleOnPageLoad = True
        rwMessage.Visible = True
    End Sub

    Public Sub Hide()
        rwMessage.VisibleOnPageLoad = False
        rwMessage.Visible = False
    End Sub

    Private Sub btnYES_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYES.Click
        Hide()
        RaiseEvent OrganizationSelected(sender, _
                                        New OrganizationSelectedEventArgs( _
                                            ctrlOrg1.CheckedValues, _
                                            ctrlOrg1.CheckedValueKeys, _
                                            ctrlOrg1.GetCheckedTexts, _
                                            ctrlOrg1.CurrentText, _
                                            ctrlOrg1.CurrentValue, _
                                            ctrlOrg1.CurrentItemDataObject,
                                            ctrlOrg1.CurrentCode))
    End Sub

    Private Sub btnNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNO.Click
        RaiseEvent CancelClicked(sender, e)
        Hide()
    End Sub
End Class

Public Class OrganizationSelectedEventArgs
    Inherits EventArgs
    Public Sub New(ByVal _SelectedValues As List(Of String), _
                   ByVal _CheckedValueDecimals As List(Of Decimal), _
                   ByVal _SelectedTexts As List(Of String), _
                   ByVal _CurrentText As String, _
                   ByVal _CurrentValue As String, _
                   ByVal _CurrentItemDataObject As Object, _
                   ByVal _CurrentCode As String)
        SelectedValues = _SelectedValues
        CheckedValueDecimals = _CheckedValueDecimals
        SelectedTexts = _SelectedTexts
        CurrentValue = _CurrentValue
        CurrentText = _CurrentText
        CurrentCode = _CurrentCode
        CurrentItemDataObject = _CurrentItemDataObject
    End Sub
    Public SelectedValues As List(Of String)
    Public CheckedValueDecimals As List(Of Decimal)
    Public SelectedTexts As List(Of String)
    Public CurrentText As String
    Public CurrentCode As String
    Public CurrentValue As String
    Public CurrentItemDataObject As Object
End Class


