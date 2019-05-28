Imports Common.CommonBusiness
Imports Framework.UI


Partial Public Class CommonRepository
    Inherits CommonRepositoryBase

#Region "Case Config"
    Public Function GetCaseConfigByID(ByVal codename As String, ByVal codecase As String) As Integer
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetCaseConfigByID(codename, codecase)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Controls Manage"
    ''' <summary>
    ''' Hiển thị d/sach Tên page điều chỉnh control
    ''' </summary>
    ''' <param name="_filter"></param>
    ''' <param name="PageIndex"></param>
    ''' <param name="PageSize"></param>
    ''' <param name="Total"></param>
    ''' <param name="Sorts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_FunctionWithControl_List(ByVal _filter As FunctionDTO,
                            ByVal PageIndex As Integer,
                            ByVal PageSize As Integer,
                            ByRef Total As Integer,
                            Optional ByVal Sorts As String = "NAME ASC") As List(Of FunctionDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.Get_FunctionWithControl_List(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function Get_FunctionWithControl_List(ByVal _filter As FunctionDTO, Optional ByVal Sorts As String = "NAME ASC")
        Using rep As New CommonBusinessClient
            Try
                Return rep.Get_FunctionWithControl_List(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function Insert_Update_Control_List(ByVal _id As String, ByVal Config As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.Insert_Update_Control_List(_id, Config, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "User List"
    Public Function GetUser(ByVal _filter As UserDTO,
                        ByVal PageIndex As Integer,
                        ByVal PageSize As Integer,
                        ByRef Total As Integer,
                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUser(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetUserList(ByVal _filter As UserDTO,
                        ByVal PageIndex As Integer,
                        ByVal PageSize As Integer,
                        ByRef Total As Integer,
                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserList(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateUser(ByVal _validate As UserDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ValidateUser(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertUser(ByVal objUser As UserDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertUser(objUser, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyUser(ByVal objUser As UserDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ModifyUser(objUser, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteUser(ByVal lstUser As List(Of Decimal), ByRef _error As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.DeleteUser(lstUser, _error, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateUserListStatus(ByVal _lstUserID As List(Of Decimal), ByVal _status As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateUserListStatus(_lstUserID, _status, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function SyncUserList(ByRef _newUser As String, ByRef _modifyUser As String, ByRef _deleteUser As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.SyncUserList(_newUser, _modifyUser, _deleteUser, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ResetUserPassword(ByVal _userid As List(Of System.Decimal), ByVal _minLength As Integer, ByVal _hasLowerChar As Boolean, ByVal _hasUpperChar As Boolean, ByVal _hasNumbericChar As Boolean, ByVal _hasSpecialChar As Boolean) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ResetUserPassword(_userid, _minLength, _hasLowerChar, _hasUpperChar, _hasNumbericChar, _hasSpecialChar)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function SendMailConfirmUserPassword(ByVal _userid As List(Of System.Decimal), ByVal _subject As String, ByVal _content As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.SendMailConfirmUserPassword(_userid, _subject, _content)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetUserNeedSendMail(ByVal _groupid As System.Decimal) As List(Of UserDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserNeedSendMail(_groupid)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Group List"

    Public Function GetGroupListToComboListBox() As List(Of GroupDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetGroupListToComboListBox()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetGroupList(ByVal _filter As GroupDTO,
                        ByVal PageIndex As Integer,
                        ByVal PageSize As Integer,
                        ByRef Total As Integer,
                        Optional ByVal Sorts As String = "MODIFIED_DATE desc") As List(Of GroupDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetGroupList(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateGroupList(ByVal _validate As GroupDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ValidateGroupList(_validate)
            Catch ex As Exception
                rep.Abort()

                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertGroup(ByVal lst As GroupDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertGroup(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateGroup(ByVal lst As GroupDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateGroup(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteGroup(ByVal lstID As List(Of Decimal), ByRef _error As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.DeleteGroup(lstID, _error, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateGroupStatus(ByVal _lstID As List(Of System.Decimal), ByVal _ACTFLG As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateGroupStatus(_lstID, _ACTFLG, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Function List"

    Public Function GetFunctionList(ByVal _filter As FunctionDTO,
                        ByVal PageIndex As Integer,
                        ByVal PageSize As Integer,
                        ByRef Total As Integer,
                        Optional ByVal Sorts As String = "NAME asc") As List(Of FunctionDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetFunctionList(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateFunctionList(ByVal _validate As FunctionDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ValidateFunctionList(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateFunctionList(ByVal _item As List(Of FunctionDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateFunctionList(_item, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertFunctionList(ByVal _item As FunctionDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertFunctionList(_item, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetModuleList() As List(Of ModuleDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetModuleList()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetFunction(ByVal functionId As String) As FunctionDTO
        Using rep As New CommonBusinessClient
            Try
                If Common.FunctionListDataCache Is Nothing Then
                    Common.FunctionListDataCache = rep.GetFunctionList(New FunctionDTO, 0, -1, -1, "MODIFIED_DATE desc")
                End If
                Return (From p In Common.FunctionListDataCache Where p.FID = functionId).FirstOrDefault
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveFunctions(ByVal lstFunction As List(Of FunctionDTO), ByVal sActive As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ActiveFunctions(lstFunction, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Group User"

    Public Function GetUserListInGroup(ByVal GroupID As Decimal,
                                        ByVal _filter As UserDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserListInGroup(GroupID, _filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetUserListOutGroup(ByVal GroupID As Decimal,
                                        ByVal _filter As UserDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserListOutGroup(GroupID, _filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertUserGroup(ByVal GroupID As System.Decimal, ByVal lst As List(Of Decimal)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertUserGroup(GroupID, lst, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteUserGroup(ByVal GroupID As System.Decimal, ByVal lst As List(Of Decimal)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.DeleteUserGroup(GroupID, lst, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Group Function"

    Public Function GetGroupFunctionPermision(ByVal GroupID As Decimal,
                                                ByVal _filter As GroupFunctionDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "FUNCTION_NAME asc") As List(Of GroupFunctionDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetGroupFunctionPermision(GroupID, _filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetGroupFunctionNotPermision(ByVal GroupID As Decimal,
                                                    ByVal _filter As FunctionDTO,
                                                    ByVal PageIndex As Integer,
                                                    ByVal PageSize As Integer,
                                                    ByRef Total As Integer,
                                                    Optional ByVal Sorts As String = "NAME asc") As List(Of FunctionDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetGroupFunctionNotPermision(GroupID, _filter, PageIndex, PageSize, Total, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertGroupFunction(ByVal lst As List(Of GroupFunctionDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertGroupFunction(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CopyGroupFunction(ByVal groupCopyID As Decimal, ByVal groupID As Decimal) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.CopyGroupFunction(groupCopyID, groupID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateGroupFunction(ByVal lst As List(Of GroupFunctionDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateGroupFunction(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteGroupFunction(ByVal lst As List(Of Decimal)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.DeleteGroupFunction(lst)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Group Report"
    ''' <summary>
    ''' Lấy danh sách Report đã phân quyền theo nhóm tài khoản
    ''' </summary>
    ''' <param name="_groupID">ID nhóm tài khoản</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGroupReportList(ByVal _groupID As Decimal) As List(Of GroupReportDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetGroupReportList(_groupID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    ''' <summary>
    ''' Lấy danh sách Report đã phân quyền theo nhóm tài khoản có Filter
    ''' </summary>
    ''' <param name="_groupID">ID nhóm tài khoản</param>
    ''' <param name="_filter">bộ lọc</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGroupReportListFilter(ByVal _groupID As Decimal, ByVal _filter As GroupReportDTO) As List(Of GroupReportDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetGroupReportListFilter(_groupID, _filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    ''' <summary>
    ''' Cập nhập danh sách Report
    ''' </summary>
    ''' <param name="_groupID">ID nhóm tài khoản</param>
    ''' <param name="_lstReport">Danh sách report cần cập nhập</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateGroupReport(ByVal _groupID As Decimal, ByVal _lstReport As List(Of GroupReportDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateGroupReport(_groupID, _lstReport)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "User Function"

    Public Function GetUserFunctionPermision(ByVal UserID As Decimal,
                                                ByVal _filter As UserFunctionDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "FUNCTION_NAME asc") As List(Of UserFunctionDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserFunctionPermision(UserID, _filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetUserFunctionNotPermision(ByVal UserID As Decimal,
                                                    ByVal _filter As FunctionDTO,
                                                    ByVal PageIndex As Integer,
                                                    ByVal PageSize As Integer,
                                                    ByRef Total As Integer,
                                                    Optional ByVal Sorts As String = "NAME asc") As List(Of FunctionDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserFunctionNotPermision(UserID, _filter, PageIndex, PageSize, Total, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertUserFunction(ByVal lst As List(Of UserFunctionDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertUserFunction(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CopyUserFunction(ByVal UserCopyID As Decimal, ByVal UserID As Decimal) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.CopyUserFunction(UserCopyID, UserID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateUserFunction(ByVal lst As List(Of UserFunctionDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateUserFunction(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteUserFunction(ByVal lst As List(Of Decimal)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.DeleteUserFunction(lst)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "User Organization"

    Public Function GetUserOrganization(ByVal UserID As Decimal) As List(Of Decimal)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserOrganization(UserID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateUserOrganization(ByVal OrgIDs As List(Of UserOrgAccessDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Dim r As Boolean = True
                If Not rep.DeleteUserOrganization(OrgIDs(0).USER_ID) Then Return False
                Return rep.UpdateUserOrganization(OrgIDs)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetListOrgImport(ByVal orgID As List(Of System.Decimal)) As List(Of OrganizationDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetListOrgImport(orgID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "User Report"
    ''' <summary>
    ''' Lấy danh sách Report đã phân quyền theo nhóm tài khoản
    ''' </summary>
    ''' <param name="_UserID">ID nhóm tài khoản</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserReportList(ByVal _UserID As Decimal) As List(Of UserReportDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserReportList(_UserID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    ''' <summary>
    ''' Lấy danh sách Report đã phân quyền theo nhóm tài khoản có Filter
    ''' </summary>
    ''' <param name="_UserID">ID nhóm tài khoản</param>
    ''' <param name="_filter">bộ lọc</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetUserReportListFilter(ByVal _UserID As Decimal, ByVal _filter As UserReportDTO) As List(Of UserReportDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserReportListFilter(_UserID, _filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    ''' <summary>
    ''' Cập nhập danh sách Report
    ''' </summary>
    ''' <param name="_UserID">ID nhóm tài khoản</param>
    ''' <param name="_lstReport">Danh sách report cần cập nhập</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateUserReport(ByVal _UserID As Decimal, ByVal _lstReport As List(Of UserReportDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateUserReport(_UserID, _lstReport)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Employee"

    Public Function GetEmployeeToPopupFind(_filter As EmployeePopupFindListDTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                            Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetEmployeeToPopupFind(_filter, PageIndex, PageSize, Total, Sorts, Log, _param)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetEmployeeToPopupFind2(ByVal _filter As EmployeePopupFindListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal request_id As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                        Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetEmployeeToPopupFind2(_filter, PageIndex, PageSize, Total, request_id, Sorts, Log, _param)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetEmployeeToPopupFind_EmployeeID(ByVal _empId As List(Of Decimal)) As List(Of EmployeePopupFindDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetEmployeeToPopupFind_EmployeeID(_empId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Title"

    Public Function GetTitle(ByVal Filter As TitleDTO,
                                ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "NAME_VN asc") As List(Of TitleDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetTitle(Filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetTitleFromId(ByVal _lstIds As List(Of Decimal)) As List(Of TitleDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetTitleFromId(_lstIds)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "AccessLog"

    Public Function GetAccessLog(ByVal filter As AccessLogFilter,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                 Optional ByVal Sorts As String = "LoginDate desc") As List(Of AccessLog)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetAccessLog(filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using


    End Function

    Public Function GetAccessLog(ByVal filter As AccessLogFilter,
                                 Optional ByVal Sorts As String = "LoginDate desc") As List(Of AccessLog)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetAccessLog(filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using


    End Function

    Public Function InsertAccessLog(ByVal _accesslog As AccessLog) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertAccessLog(_accesslog)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "ActionLog"

    Public Function GetActionLog(ByVal filter As ActionLogFilter,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                 Optional ByVal Sorts As String = "ActionDate desc") As List(Of ActionLog)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetActionLog(filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetActionLog(ByVal filter As ActionLogFilter,
                                 Optional ByVal Sorts As String = "ActionDate desc") As List(Of ActionLog)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetActionLog(filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetActionLogByObjectId(ByVal ObjectId As Decimal) As List(Of ActionLog)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetActionLogByObjectId(ObjectId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetActionLogByID(ByVal gID As Decimal) As List(Of AuditLogDtl)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetActionLogByID(gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteActionLogs(ByVal lstDeleteIds As List(Of Decimal)) As Integer
        Using rep As New CommonBusinessClient
            Try
                Return rep.DeleteActionLogs(lstDeleteIds)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using


    End Function

#End Region

End Class
