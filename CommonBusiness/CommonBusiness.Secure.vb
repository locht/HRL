﻿Imports CommonBusiness.ServiceContracts
Imports CommonDAL
Imports Framework.Data
Imports System.Collections.Generic
Imports Framework.Data.SystemConfig

Namespace CommonBusiness.ServiceImplementations
    Partial Public Class CommonBusiness
        Implements ICommonBusiness
#Region "Group Organization"

        Public Function GetGroupOrganization(ByVal _groupID As Decimal) As List(Of Decimal) Implements ServiceContracts.ICommonBusiness.GetGroupOrganization
            Using rep As New CommonRepository
                Try
                    Return rep.GetGroupOrganization(_groupID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetGroupOrganizationFunction(ByVal _groupID As Decimal) As List(Of Decimal) Implements ServiceContracts.ICommonBusiness.GetGroupOrganizationFunction
            Using rep As New CommonRepository
                Try
                    Return rep.GetGroupOrganizationFunction(_groupID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteGroupOrganization(ByVal _groupId As Decimal) Implements ServiceContracts.ICommonBusiness.DeleteGroupOrganization
            Using rep As New CommonRepository
                Try
                    Return rep.DeleteGroupOrganization(_groupId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateGroupOrganization(ByVal _lstOrg As List(Of GroupOrgAccessDTO)) As Boolean Implements ServiceContracts.ICommonBusiness.UpdateGroupOrganization
            Using rep As New CommonRepository
                Try
                    Return rep.UpdateGroupOrganization(_lstOrg)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateGroupOrganizationFunction(ByVal _lstOrg As List(Of GroupOrgFunAccessDTO)) As Boolean Implements ServiceContracts.ICommonBusiness.UpdateGroupOrganizationFunction
            Using rep As New CommonRepository
                Try
                    Return rep.UpdateGroupOrganizationFunction(_lstOrg)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "Case config"
        Public Function GetCaseConfigByID(ByVal codename As String, ByVal codecase As String) As Integer Implements ServiceContracts.ICommonBusiness.GetCaseConfigByID
            Using rep As New CommonRepository
                Try
                    Return rep.GetCaseConfigByID(codename, codecase)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Controls Manage"
        Public Function Get_FunctionWithControl_List(ByVal _filter As FunctionDTO,
                            ByVal PageIndex As Integer,
                            ByVal PageSize As Integer,
                            ByRef Total As Integer,
                            Optional ByVal Sorts As String = "NAME ASC") As List(Of FunctionDTO) Implements ServiceContracts.ICommonBusiness.Get_FunctionWithControl_List
            Using rep As New CommonRepository
                Try
                    Return rep.Get_FunctionWithControl_List(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function Insert_Update_Control_List(ByVal _id As String, ByVal Config As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.Insert_Update_Control_List
            Using rep As New CommonRepository
                Try
                    Return rep.Insert_Update_Control_List(_id, Config, log)
                Catch ex As Exception
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
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommonDAL.UserDTO) Implements ServiceContracts.ICommonBusiness.GetUser
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetUser(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetUserList(ByVal _filter As UserDTO,
                                ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommonDAL.UserDTO) Implements ServiceContracts.ICommonBusiness.GetUserList
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetUserList(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateUser(ByVal _validate As CommonDAL.UserDTO) As Boolean Implements ServiceContracts.ICommonBusiness.ValidateUser
            Using rep As New CommonRepository
                Try

                    Return rep.ValidateUser(_validate)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertUser(ByVal _user As UserDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.InsertUser
            Using rep As New CommonRepository
                Try

                    Return rep.InsertUser(_user, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyUser(ByVal _user As UserDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.ModifyUser
            Using rep As New CommonRepository
                Try

                    Return rep.ModifyUser(_user, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteUser(ByVal _lstUserID As List(Of Decimal), ByRef _error As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.DeleteUser
            Using rep As New CommonRepository
                Try

                    Return rep.DeleteUser(_lstUserID, _error, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function SyncUserList(ByRef _newUser As String, ByRef _modifyUser As String, ByRef _deleteUser As String, ByVal log As Framework.Data.UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.SyncUserList
            Using rep As New CommonRepository
                Try

                    Return rep.SyncUserList(_newUser, _modifyUser, _deleteUser, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateUserListStatus(ByVal _lstUserID As List(Of System.Decimal), ByVal _status As String, ByVal log As Framework.Data.UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.UpdateUserListStatus
            Using rep As New CommonRepository
                Try

                    Return rep.UpdateUserListStatus(_lstUserID, _status, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ResetUserPassword(ByVal _userid As List(Of System.Decimal), ByVal _minLength As Integer, ByVal _hasLowerChar As Boolean, ByVal _hasUpperChar As Boolean, ByVal _hasNumbericChar As Boolean, ByVal _hasSpecialChar As Boolean) As Boolean Implements ServiceContracts.ICommonBusiness.ResetUserPassword
            Using rep As New CommonRepository
                Try

                    Return rep.ResetUserPassword(_userid, _minLength, _hasLowerChar, _hasUpperChar, _hasNumbericChar, _hasSpecialChar)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function SendMailConfirmUserPassword(ByVal _userid As List(Of System.Decimal), ByVal _subject As String, ByVal _content As String) As Boolean Implements ServiceContracts.ICommonBusiness.SendMailConfirmUserPassword
            Using rep As New CommonRepository
                Try

                    Return rep.SendMailConfirmUserPassword(_userid, _subject, _content)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetUserNeedSendMail(ByVal _groupid As System.Decimal) As List(Of CommonDAL.UserDTO) Implements ServiceContracts.ICommonBusiness.GetUserNeedSendMail
            Using rep As New CommonRepository
                Try

                    Return rep.GetUserNeedSendMail(_groupid)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Group List"

        Public Function GetGroupListToComboListBox() As List(Of CommonDAL.GroupDTO) Implements ServiceContracts.ICommonBusiness.GetGroupListToComboListBox
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetGroupListToComboListBox()
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetGroupList(ByVal _filter As GroupDTO,
                            ByVal PageIndex As Integer,
                            ByVal PageSize As Integer,
                            ByRef Total As Integer,
                            Optional ByVal Sorts As String = "MODIFIED_DATE desc") As List(Of CommonDAL.GroupDTO) Implements ServiceContracts.ICommonBusiness.GetGroupList
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetGroupList(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateGroupList(ByVal _validate As CommonDAL.GroupDTO) As Boolean Implements ServiceContracts.ICommonBusiness.ValidateGroupList
            Using rep As New CommonRepository
                Try

                    Return rep.ValidateGroupList(_validate)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertGroup(ByVal lst As CommonDAL.GroupDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.InsertGroup
            Using rep As New CommonRepository
                Try

                    Return rep.InsertGroup(lst, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateGroup(ByVal lst As CommonDAL.GroupDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.UpdateGroup
            Using rep As New CommonRepository
                Try

                    Return rep.UpdateGroup(lst, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteGroup(ByVal GroupID As List(Of Decimal), ByRef _error As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.DeleteGroup
            Using rep As New CommonRepository
                Try

                    Return rep.DeleteGroup(GroupID, _error, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateGroupStatus(ByVal _lstID As List(Of System.Decimal), ByVal _ACTFLG As String, ByVal log As Framework.Data.UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.UpdateGroupStatus
            Using rep As New CommonRepository
                Try

                    Return rep.UpdateGroupStatus(_lstID, _ACTFLG, log)
                Catch ex As Exception

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
                            Optional ByVal Sorts As String = "NAME asc") As List(Of CommonDAL.FunctionDTO) Implements ServiceContracts.ICommonBusiness.GetFunctionList
            Using rep As New CommonRepository
                Try
                    Return rep.GetFunctionList(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateFunctionList(ByVal _validate As CommonDAL.FunctionDTO) As Boolean Implements ServiceContracts.ICommonBusiness.ValidateFunctionList
            Using rep As New CommonRepository
                Try

                    Return rep.ValidateFunctionList(_validate)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateFunctionList(ByVal _item As List(Of CommonDAL.FunctionDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.UpdateFunctionList
            Using rep As New CommonRepository
                Try

                    Return rep.UpdateFunctionList(_item, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertFunctionList(ByVal _item As FunctionDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.InsertFunctionList
            Using rep As New CommonRepository
                Try

                    Return rep.InsertFunctionList(_item, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetModuleList() As List(Of CommonDAL.ModuleDTO) Implements ServiceContracts.ICommonBusiness.GetModuleList
            Using rep As New CommonRepository
                Try

                    Return rep.GetModuleList()
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveFunctions(ByVal lstFunction As List(Of FunctionDTO), ByVal sActive As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.ActiveFunctions
            Using rep As New CommonRepository
                Try

                    Return rep.ActiveFunctions(lstFunction, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Group User"

        Public Function GetUserListInGroup(ByVal GroupID As System.Decimal,
                                        ByVal _filter As UserDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommonDAL.UserDTO) Implements ServiceContracts.ICommonBusiness.GetUserListInGroup
            Using rep As New CommonRepository
                Try

                    Return rep.GetUserListInGroup(GroupID, _filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetUserListOutGroup(ByVal GroupID As System.Decimal,
                                        ByVal _filter As UserDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommonDAL.UserDTO) Implements ServiceContracts.ICommonBusiness.GetUserListOutGroup
            Using rep As New CommonRepository
                Try

                    Return rep.GetUserListOutGroup(GroupID, _filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertUserGroup(ByVal _groupID As Decimal, ByVal _lstUserID As List(Of Decimal), ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.InsertUserGroup
            Using rep As New CommonRepository
                Try

                    Return rep.InsertUserGroup(_groupID, _lstUserID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteUserGroup(ByVal _groupID As Decimal, ByVal _lstUserID As List(Of Decimal), ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.DeleteUserGroup
            Using rep As New CommonRepository
                Try

                    Return rep.DeleteUserGroup(_groupID, _lstUserID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Group Function"
        Public Function GetGroupFunctionPermision(ByVal GroupID As System.Decimal,
                                                    ByVal _filter As GroupFunctionDTO,
                                                    ByVal PageIndex As Integer,
                                                    ByVal PageSize As Integer,
                                                    ByRef Total As Integer,
                                                    Optional ByVal Sorts As String = "FUNCTION_CODE asc") As List(Of CommonDAL.GroupFunctionDTO) Implements ServiceContracts.ICommonBusiness.GetGroupFunctionPermision
            Using rep As New CommonRepository
                Try

                    Return rep.GetGroupFunctionPermision(GroupID, _filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetGroupFunctionNotPermision(ByVal GroupID As Decimal,
                                                    ByVal _filter As FunctionDTO,
                                                    ByVal PageIndex As Integer,
                                                    ByVal PageSize As Integer,
                                                    ByRef Total As Integer,
                                                    ByVal log As UserLog,
                                                    Optional ByVal Sorts As String = "NAME asc") As List(Of FunctionDTO) Implements ServiceContracts.ICommonBusiness.GetGroupFunctionNotPermision
            Using rep As New CommonRepository
                Try

                    Return rep.GetGroupFunctionNotPermision(GroupID, _filter, PageIndex, PageSize, Total, log, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteGroupFunction(ByVal lst As List(Of Decimal)) As Boolean Implements ServiceContracts.ICommonBusiness.DeleteGroupFunction
            Using rep As New CommonRepository
                Try

                    Return rep.DeleteGroupFunction(lst)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertGroupFunction(ByVal lst As List(Of CommonDAL.GroupFunctionDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.InsertGroupFunction
            Using rep As New CommonRepository
                Try

                    Return rep.InsertGroupFunction(lst, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function


        Public Function CopyGroupFunction(ByVal groupCopyID As Decimal, ByVal groupID As Decimal, ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.CopyGroupFunction
            Using rep As New CommonRepository
                Try

                    Return rep.CopyGroupFunction(groupCopyID, groupID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateGroupFunction(ByVal lst As List(Of CommonDAL.GroupFunctionDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.UpdateGroupFunction
            Using rep As New CommonRepository
                Try

                    Return rep.UpdateGroupFunction(lst, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Group Report"
        Public Function GetGroupReportList(ByVal _groupID As System.Decimal) As List(Of CommonDAL.GroupReportDTO) Implements ServiceContracts.ICommonBusiness.GetGroupReportList
            Using rep As New CommonRepository
                Try

                    Return rep.GetGroupReportList(_groupID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetGroupReportListFilter(ByVal _groupID As System.Decimal, ByVal _filter As CommonDAL.GroupReportDTO) As List(Of CommonDAL.GroupReportDTO) Implements ServiceContracts.ICommonBusiness.GetGroupReportListFilter
            Using rep As New CommonRepository
                Try

                    Return rep.GetGroupReportListFilter(_groupID, _filter)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateGroupReport(ByVal _groupID As System.Decimal, ByVal _lstReport As List(Of CommonDAL.GroupReportDTO)) As Boolean Implements ServiceContracts.ICommonBusiness.UpdateGroupReport
            Using rep As New CommonRepository
                Try

                    Return rep.UpdateGroupReport(_groupID, _lstReport)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "User Function"
        Public Function GetUserFunctionPermision(ByVal UserID As System.Decimal,
                                                    ByVal _filter As UserFunctionDTO,
                                                    ByVal PageIndex As Integer,
                                                    ByVal PageSize As Integer,
                                                    ByRef Total As Integer,
                                                    Optional ByVal Sorts As String = "FUNCTION_CODE asc") As List(Of CommonDAL.UserFunctionDTO) Implements ServiceContracts.ICommonBusiness.GetUserFunctionPermision
            Using rep As New CommonRepository
                Try

                    Return rep.GetUserFunctionPermision(UserID, _filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetUserFunctionNotPermision(ByVal UserID As Decimal,
                                                    ByVal _filter As FunctionDTO,
                                                    ByVal PageIndex As Integer,
                                                    ByVal PageSize As Integer,
                                                    ByRef Total As Integer,
                                                    ByVal log As UserLog,
                                                    Optional ByVal Sorts As String = "NAME asc") As List(Of FunctionDTO) Implements ServiceContracts.ICommonBusiness.GetUserFunctionNotPermision
            Using rep As New CommonRepository
                Try

                    Return rep.GetUserFunctionNotPermision(UserID, _filter, PageIndex, PageSize, Total, log, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteUserFunction(ByVal lst As List(Of Decimal)) As Boolean Implements ServiceContracts.ICommonBusiness.DeleteUserFunction
            Using rep As New CommonRepository
                Try

                    Return rep.DeleteUserFunction(lst)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertUserFunction(ByVal lst As List(Of CommonDAL.UserFunctionDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.InsertUserFunction
            Using rep As New CommonRepository
                Try

                    Return rep.InsertUserFunction(lst, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function


        Public Function CopyUserFunction(ByVal UserCopyID As Decimal, ByVal UserID As Decimal, ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.CopyUserFunction
            Using rep As New CommonRepository
                Try

                    Return rep.CopyUserFunction(UserCopyID, UserID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateUserFunction(ByVal lst As List(Of CommonDAL.UserFunctionDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.UpdateUserFunction
            Using rep As New CommonRepository
                Try

                    Return rep.UpdateUserFunction(lst, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "User Organization"

        Public Function GetUserOrganization(ByVal UserID As System.Decimal) As List(Of Decimal) Implements ServiceContracts.ICommonBusiness.GetUserOrganization
            Using rep As New CommonRepository
                Try

                    Return rep.GetUserOrganization(UserID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteUserOrganization(ByVal _UserId As System.Decimal) As Object Implements ServiceContracts.ICommonBusiness.DeleteUserOrganization
            Using rep As New CommonRepository
                Try

                    Return rep.DeleteUserOrganization(_UserId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateUserOrganization(ByVal OrgIDs As List(Of CommonDAL.UserOrgAccessDTO)) As Boolean Implements ServiceContracts.ICommonBusiness.UpdateUserOrganization
            Using rep As New CommonRepository
                Try

                    Return rep.UpdateUserOrganization(OrgIDs)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetListOrgImport(ByVal orgID As List(Of System.Decimal)) As List(Of CommonDAL.OrganizationDTO) Implements ServiceContracts.ICommonBusiness.GetListOrgImport
            Using rep As New CommonRepository
                Try

                    Return rep.GetListOrgImport(orgID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "User Report"
        Public Function GetUserReportList(ByVal _UserID As System.Decimal) As List(Of CommonDAL.UserReportDTO) Implements ServiceContracts.ICommonBusiness.GetUserReportList
            Using rep As New CommonRepository
                Try

                    Return rep.GetUserReportList(_UserID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetUserReportListFilter(ByVal _UserID As System.Decimal, ByVal _filter As CommonDAL.UserReportDTO) As List(Of CommonDAL.UserReportDTO) Implements ServiceContracts.ICommonBusiness.GetUserReportListFilter
            Using rep As New CommonRepository
                Try

                    Return rep.GetUserReportListFilter(_UserID, _filter)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateUserReport(ByVal _UserID As System.Decimal, ByVal _lstReport As List(Of CommonDAL.UserReportDTO)) As Boolean Implements ServiceContracts.ICommonBusiness.UpdateUserReport
            Using rep As New CommonRepository
                Try

                    Return rep.UpdateUserReport(_UserID, _lstReport)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Employee"
        Public Function GetEmployeeSignToPopupFind(_filter As EmployeePopupFindListDTO,
                                           ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                           ByRef Total As Integer,
                                           Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                           Optional ByVal log As UserLog = Nothing,
                                           Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO) _
                                       Implements ServiceContracts.ICommonBusiness.GetEmployeeSignToPopupFind
            Using rep As New CommonRepository
                Try

                    Return rep.GetEmployeeSignToPopupFind(_filter, PageIndex, PageSize, Total, Sorts, log, _param)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmployeeToPopupFind(_filter As EmployeePopupFindListDTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                            Optional ByVal log As UserLog = Nothing,
                                            Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO) _
                                        Implements ServiceContracts.ICommonBusiness.GetEmployeeToPopupFind
            Using rep As New CommonRepository
                Try

                    Return rep.GetEmployeeToPopupFind(_filter, PageIndex, PageSize, Total, Sorts, log, _param)
                Catch ex As Exception

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
                                    Optional ByVal log As UserLog = Nothing,
                                    Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO) _
                                Implements ServiceContracts.ICommonBusiness.GetEmployeeToPopupFind2
            Using rep As New CommonRepository
                Try

                    Return rep.GetEmployeeToPopupFind2(_filter, PageIndex, PageSize, Total, request_id, Sorts, log, _param)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmployeeToPopupFind_EmployeeID(ByVal _empId As List(Of System.Decimal)) As List(Of CommonDAL.EmployeePopupFindDTO) Implements ServiceContracts.ICommonBusiness.GetEmployeeToPopupFind_EmployeeID
            Using rep As New CommonRepository
                Try

                    Return rep.GetEmployeeToPopupFind_EmployeeID(_empId)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmployeeID(ByVal _empId As Decimal) As EmployeePopupFindDTO Implements ServiceContracts.ICommonBusiness.GetEmployeeID
            Using rep As New CommonRepository
                Try
                    Return rep.GetEmployeeID(_empId)
                Catch ex As Exception
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
                                Optional ByVal Sorts As String = "NAME_VN asc") As List(Of TitleDTO) Implements ServiceContracts.ICommonBusiness.GetTitle
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetTitle(Filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetTitleFromId(ByVal _lstIds As List(Of Decimal)) As List(Of TitleDTO) Implements ServiceContracts.ICommonBusiness.GetTitleFromId
            Using rep As New CommonRepository
                Try

                    Return rep.GetTitleFromId(_lstIds)
                Catch ex As Exception
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
                                     Optional ByVal Sorts As String = "LoginDate desc") As List(Of AccessLog) _
                                 Implements ServiceContracts.ICommonBusiness.GetAccessLog
            Using rep As New CommonRepository
                Try
                    Return rep.GetAccessLog(filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using

        End Function

        Public Function InsertAccessLog(ByVal _accesslog As AccessLog) As Boolean _
            Implements ServiceContracts.ICommonBusiness.InsertAccessLog
            Using rep As New CommonRepository
                Try
                    Return rep.InsertAccessLog(_accesslog)
                Catch ex As Exception
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
                                     Optional ByVal Sorts As String = "ActionDate desc") As List(Of ActionLog) _
                                 Implements ServiceContracts.ICommonBusiness.GetActionLog

            Using rep As New CommonRepository
                Try
                    Return rep.GetActionLog(filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetActionLogByObjectId(ByVal ObjectId As Decimal) As List(Of ActionLog) _
            Implements ICommonBusiness.GetActionLogByObjectId

            Using rep As New CommonRepository
                Try
                    Return rep.GetActionLog(ObjectId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetActionLogByID(ByVal gID As Decimal) As List(Of AuditLogDtl) _
            Implements ICommonBusiness.GetActionLogByID

            Using rep As New CommonRepository
                Try
                    Return rep.GetActionLogByID(gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteActionLogs(ByVal lstDeleteIds As List(Of Decimal)) As Integer Implements ICommonBusiness.DeleteActionLogs

            Using rep As New CommonRepository
                Try
                    Return rep.DeleteActionLogs(lstDeleteIds)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "System Config"
        Public Function GetConfig(ByVal eModule As ModuleID) As Dictionary(Of String, String) Implements ServiceContracts.ICommonBusiness.GetConfig
            Using rep As New CommonRepository
                Try

                    Return rep.GetConfig(eModule)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateConfig(ByVal _lstConfig As Dictionary(Of String, String), ByVal eModule As ModuleID) As Boolean Implements ServiceContracts.ICommonBusiness.UpdateConfig
            Using rep As New CommonRepository
                Try

                    Return rep.UpdateConfig(_lstConfig, eModule)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "MailTemplate"
        Public Function GetMailTemplate(ByVal _filter As MailTemplateDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of MailTemplateDTO) Implements ServiceContracts.ICommonBusiness.GetMailTemplate
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetMailTemplate(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetMailTemplateBaseCode(ByVal code As String, ByVal group As String) As MailTemplateDTO Implements ServiceContracts.ICommonBusiness.GetMailTemplateBaseCode
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetMailTemplateBaseCode(code, group)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertMailTemplate(ByVal mailTemplate As MailTemplateDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.InsertMailTemplate
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.InsertMailTemplate(mailTemplate, log)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyMailTemplate(ByVal mailTemplate As MailTemplateDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.ModifyMailTemplate
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.ModifyMailTemplate(mailTemplate, log)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteMailTemplate(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.ICommonBusiness.DeleteMailTemplate
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.DeleteMailTemplate(lstID)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckValidEmailTemplate(ByVal code As String, ByVal group As String) As Boolean Implements ServiceContracts.ICommonBusiness.CheckValidEmailTemplate
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.CheckValidEmailTemplate(code, group)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region
    End Class
End Namespace