Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports Common
Imports Common.Common
Imports Common.CommonMessage
Public Class ctrlEmpBasicInfo
    Inherits CommonView
    Dim employeeCode As String
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Properties"
    Property EmployeeInfo As EmployeeDTO
        Get
            Return PageViewState(Me.ID & "_EmployeeInfo")
        End Get
        Set(ByVal value As EmployeeDTO)
            PageViewState(Me.ID & "_EmployeeInfo") = value
        End Set
    End Property

#End Region

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        LoadEmployeeInfo()
        UpdateControlState()
    End Sub

    ''' <summary>
    ''' Lấy thông tin chi tiết của nhân viên từ EmployeeCode
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadEmployeeInfo()
        Try
            If EmployeeInfo IsNot Nothing Then
                txtEmployeeCODE1.Text = EmployeeInfo.EMPLOYEE_CODE
                txtFullName.Text = EmployeeInfo.FULLNAME_VN
                hidID.Value = EmployeeInfo.ID.ToString()
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        UpdateControlState()
    End Sub
End Class