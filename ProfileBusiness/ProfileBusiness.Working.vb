﻿Imports ProfileBusiness.ServiceContracts
Imports ProfileDAL
Imports Framework.Data
Imports System.ServiceModel.Activation
Imports System.Reflection
Imports System.Configuration

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness

#Region "Working"

        Public Function getDtByEmpIDandEffectdate(ByVal obj As WorkingDTO) As List(Of WorkingDTO) _
                      Implements ServiceContracts.IProfileBusiness.getDtByEmpIDandEffectdate
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.getDtByEmpIDandEffectdate(obj)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function getValue_ExRate_F_T(ByVal _filter As WorkingDTO) As WorkingDTO Implements ServiceContracts.IProfileBusiness.getValue_ExRate_F_T
            Using rep As New ProfileRepository
                Try

                    Dim obj = rep.getValue_ExRate_F_T(_filter)
                    Return obj
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ApproveListChangeInfoMng(ByVal listID As List(Of Decimal), ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ApproveListChangeInfoMng
            Using rep As New ProfileRepository
                Try
                    Return rep.ApproveListChangeInfoMng(listID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckHasFile(ByVal id As List(Of Decimal)) As Decimal Implements IProfileBusiness.CheckHasFile
            Try
                Using rep As New ProfileRepository
                    Return rep.CheckHasFile(id)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Function ApproveWorkings(ByVal ids As List(Of Decimal), Optional ByVal log As UserLog = Nothing) As CommandResult Implements IProfileBusiness.ApproveWorkings
            Using rep As New ProfileRepository
                Return rep.ApproveWorkings(ids, log)
            End Using
        End Function
        Public Function GetWorking(ByVal _filter As WorkingDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO) Implements ServiceContracts.IProfileBusiness.GetWorking
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.GetWorking(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetWorkingAllowance1(ByVal _filter As HUAllowanceDTO,
                                       ByVal _param As ParamDTO,
                                       ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc",
                                       Optional ByVal log As UserLog = Nothing) As List(Of HUAllowanceDTO) Implements ServiceContracts.IProfileBusiness.GetWorkingAllowance1
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetWorkingAllowance1(_filter, _param, PageIndex, PageSize, Total, Sorts, log)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertWorkingAllowance(ByVal objWorkingAllowance As HUAllowanceDTO,
                                     ByVal log As UserLog,
                                     ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertWorkingAllowance
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertWorkingAllowance(objWorkingAllowance, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteWorkingAllowance(ByVal lstWorkingAllowance() As HUAllowanceDTO,
                                 ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteWorkingAllowance
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteWorkingAllowance(lstWorkingAllowance, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyWorkingAllowanceNew(ByVal objWorkingAllowance As HUAllowanceDTO,
                                       ByVal log As UserLog,
                                       ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyWorkingAllowanceNew
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyWorkingAllowanceNew(objWorkingAllowance, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateNewEdit(ByVal objWorkingAllowance As HUAllowanceDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateNewEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.ValidateNewEdit(objWorkingAllowance)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetWorkingAllowance(ByVal _filter As WorkingAllowanceDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer,
                               Optional ByVal Sorts As String = "EMPLOYEE_CODE") As List(Of WorkingAllowanceDTO) Implements ServiceContracts.IProfileBusiness.GetWorkingAllowance
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetWorkingAllowance(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyWorkingAllowance(ByVal objWorking As WorkingAllowanceDTO,
                                               ByVal log As UserLog,
                                               ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyWorkingAllowance
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyWorkingAllowance(objWorking, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetLastWorkingSalary(ByVal _filter As WorkingDTO) As WorkingDTO Implements ServiceContracts.IProfileBusiness.GetLastWorkingSalary
            Using rep As New ProfileRepository
                Try

                    Dim obj = rep.GetLastWorkingSalary(_filter)
                    Return obj
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetWorkingByID(ByVal _filter As WorkingDTO) As WorkingDTO _
            Implements ServiceContracts.IProfileBusiness.GetWorkingByID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetWorkingByID(_filter)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmployeCurrentByID(ByVal _filter As WorkingDTO) As WorkingDTO _
            Implements ServiceContracts.IProfileBusiness.GetEmployeCurrentByID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetEmployeCurrentByID(_filter)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertWorking(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertWorking
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertWorking(objWorking, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyWorking(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyWorking
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyWorking(objWorking, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertListWorking1(ByVal objWorking As WorkingDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.InsertListWorking1
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertListWorking1(objWorking, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertWorking1(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertWorking1
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertWorking1(objWorking, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyWorking1(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyWorking1
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyWorking1(objWorking, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteWorking(ByVal objWorking As WorkingDTO) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteWorking
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteWorking(objWorking)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateWorking(ByVal sType As String, ByVal obj As WorkingDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ValidateWorking
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateWorking(sType, obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValEffectdateByEmpCode(ByVal emp_code As String, ByVal effect_date As Date) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ValEffectdateByEmpCode
            Using rep As New ProfileRepository
                Try

                    Return rep.ValEffectdateByEmpCode(emp_code, effect_date)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAllowanceByDate(ByVal _filter As WorkingAllowanceDTO) As List(Of WorkingAllowanceDTO) _
            Implements ServiceContracts.IProfileBusiness.GetAllowanceByDate
            Using rep As New ProfileRepository
                Try

                    Return rep.GetAllowanceByDate(_filter)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAllowanceByWorkingID(ByVal _filter As WorkingAllowanceDTO) As List(Of WorkingAllowanceDTO) _
            Implements ServiceContracts.IProfileBusiness.GetAllowanceByWorkingID
            Using rep As New ProfileRepository
                Try

                    Return rep.GetAllowanceByWorkingID(_filter)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteWorking3B(ByVal objWorking As WorkingDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteWorking3B
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteWorking3B(objWorking)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetChangeInfoImport(ByVal param As ParamDTO, ByVal log As UserLog) As DataSet _
            Implements ServiceContracts.IProfileBusiness.GetChangeInfoImport
            Using rep As New ProfileRepository
                Try

                    Return rep.GetChangeInfoImport(param, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ImportChangeInfo(ByVal lstData As List(Of WorkingDTO),
                                     ByRef dtError As DataTable,
                                     ByVal log As UserLog) As Boolean _
                                 Implements ServiceContracts.IProfileBusiness.ImportChangeInfo
            Using rep As New ProfileRepository
                Try

                    Return rep.ImportChangeInfo(lstData, dtError, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function UnApproveWorking(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.UnApproveWorking
            Using rep As New ProfileRepository
                Try

                    Return rep.UnApproveWorking(objWorking, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "3B"

        Public Function GetWorking3B(ByVal _filter As WorkingDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO) _
                           Implements ServiceContracts.IProfileBusiness.GetWorking3B
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetWorking3B(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function


        Public Function InsertWorking3B(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.InsertWorking3B
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertWorking3B(objWorking, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyWorking3b(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyWorking3B
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyWorking3B(objWorking, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function


#End Region

#Region "Job Description"
        Public Function GetIdJobPosition(ByVal empCode As String) As Decimal Implements ServiceContracts.IProfileBusiness.GetIdJobPosition
            Using rep As New ProfileRepository
                Try
                    Return rep.GetIdJobPosition(empCode)
                Catch ex As Exception

                End Try
            End Using
        End Function
        Public Function CheckJobIdInWorking(ByVal jobId As Decimal, ByVal empId As Decimal) As Decimal Implements ServiceContracts.IProfileBusiness.CheckJobIdInWorking
            Using rep As New ProfileRepository
                Try
                    Return rep.CheckJobIdInWorking(jobId, empId)
                Catch ex As Exception

                End Try
            End Using
        End Function

        Public Function CheckJobIdInProfile(ByVal jobId As Decimal) As Decimal Implements ServiceContracts.IProfileBusiness.CheckJobIdInProfile
            Using rep As New ProfileRepository
                Try
                    Return rep.CheckJobIdInProfile(jobId)
                Catch ex As Exception

                End Try
            End Using
        End Function
        Public Function CheckEffectDayWorking(ByVal p_date As Date, ByVal empId As Decimal) As Decimal Implements ServiceContracts.IProfileBusiness.CheckEffectDayWorking
            Using rep As New ProfileRepository
                Try
                    Return rep.CheckEffectDayWorking(p_date, empId)
                Catch ex As Exception

                End Try
            End Using
        End Function
        Public Function CheckDecision(ByVal decision As String) As Decimal Implements ServiceContracts.IProfileBusiness.CheckDecision
            Using rep As New ProfileRepository
                Try
                    Return rep.CheckDecision(decision)
                Catch ex As Exception

                End Try
            End Using
        End Function
        Public Function GetJobDescription(ByVal _filter As JobDescriptionDTO,
                                        ByVal _param As ParamDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of JobDescriptionDTO) Implements ServiceContracts.IProfileBusiness.GetJobDescription
            Using rep As New ProfileRepository
                Try
                    Return rep.GetJobDescription(_filter, _param, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InserJobDescription(ByVal objJobDes As JobDescriptionDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.InserJobDescription
            Using rep As New ProfileRepository
                Try
                    Return rep.InserJobDescription(objJobDes, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyJobDescription(ByVal objJobDes As JobDescriptionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyJobDescription
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyJobDescription(objJobDes, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteJobDescretion(ByVal objJobDes As JobDescriptionDTO) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteJobDescretion
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteJobDescretion(objJobDes)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetJobDesByID(ByVal ID As Decimal) As JobDescriptionDTO Implements ServiceContracts.IProfileBusiness.GetJobDesByID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetJobDesByID(ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "export ChangeInfo"
        Public Function GetExportChangeInfo(ByVal org_id As Decimal) As DataSet Implements ServiceContracts.IProfileBusiness.GetExportChangeInfo
            Using rep As New ProfileRepository
                Try
                    Return rep.GetExportChangeInfo(org_id)
                Catch ex As Exception

                End Try
            End Using

        End Function
#End Region
    End Class
End Namespace
