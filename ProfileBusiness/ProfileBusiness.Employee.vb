Imports ProfileBusiness.ServiceContracts
Imports ProfileDAL
Imports Framework.Data
Imports System.ServiceModel.Activation
Imports System.Reflection
Imports System.Configuration

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness

#Region "Employee "

        ''' <summary>
        ''' Lay thong tin nhan vien tu EmployeeCode
        ''' </summary>
        ''' <param name="sEmployeeCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetEmployeeByEmployeeID(ByVal empID As Decimal) As EmployeeDTO _
                                             Implements ServiceContracts.IProfileBusiness.GetEmployeeByEmployeeID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetEmployeeByEmployeeID(empID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmployeeImage(ByVal gEmpID As Decimal, ByRef sError As String) As Byte() _
            Implements ServiceContracts.IProfileBusiness.GetEmployeeImage
            Using rep As New ProfileRepository
                Try
                    Return rep.GetEmployeeImage(gEmpID, sError)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmployeeImage_PrintCV(ByVal gEmpID As Decimal) As String _
            Implements ServiceContracts.IProfileBusiness.GetEmployeeImage_PrintCV
            Using rep As New ProfileRepository
                Try
                    Return rep.GetEmployeeImage_PrintCV(gEmpID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CreateNewEMPLOYEECode() As EmployeeDTO _
                           Implements ServiceContracts.IProfileBusiness.CreateNewEMPLOYEECode

            Try
                Dim rep As New ProfileRepository
                Return rep.CreateNewEMPLOYEECode()
            Catch ex As Exception
                WriteExceptionLog(ex, "InsertCandidateCode")
                Throw ex
            End Try

        End Function
        Public Function InsertEmployee(ByVal objEmp As EmployeeDTO, ByVal log As UserLog, ByRef gID As Decimal, _
                                        ByRef _strEmpCode As String, _
                                        ByVal _imageBinary As Byte(), _
                                        Optional ByVal objEmpCV As EmployeeCVDTO = Nothing, _
                                        Optional ByVal objEmpEdu As EmployeeEduDTO = Nothing, _
                                        Optional ByVal objEmpHealth As EmployeeHealthDTO = Nothing, _
                                        Optional ByVal objEmpUniform As UniformSizeDTO = Nothing) As Boolean _
                                Implements ServiceContracts.IProfileBusiness.InsertEmployee


            Using rep As New ProfileRepository
                Try
                    Return rep.InsertEmployee(objEmp, log, gID, _strEmpCode, _imageBinary, IIf(objEmpCV IsNot Nothing, objEmpCV, Nothing) _
                                                 , IIf(objEmpEdu IsNot Nothing, objEmpEdu, Nothing) _
                                                 , IIf(objEmpHealth IsNot Nothing, objEmpHealth, Nothing), IIf(objEmpUniform IsNot Nothing, objEmpUniform, Nothing))
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyEmployee(ByVal objEmp As EmployeeDTO, ByVal log As UserLog, ByRef gID As Decimal, _
                                        ByVal _imageBinary As Byte(), _
                                        Optional ByVal objEmpCV As EmployeeCVDTO = Nothing, _
                                        Optional ByVal objEmpEdu As EmployeeEduDTO = Nothing, _
                                        Optional ByVal objEmpHealth As EmployeeHealthDTO = Nothing, _
                                        Optional ByVal objEmpUniform As UniformSizeDTO = Nothing) As Boolean _
                                Implements ServiceContracts.IProfileBusiness.ModifyEmployee
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyEmployee(objEmp, log, gID, _imageBinary, IIf(objEmpCV IsNot Nothing, objEmpCV, Nothing), _
                                                 IIf(objEmpEdu IsNot Nothing, objEmpEdu, Nothing), _
                                                 IIf(objEmpHealth IsNot Nothing, objEmpHealth, Nothing), IIf(objEmpUniform IsNot Nothing, objEmpUniform, Nothing))
                Catch ex As Exception
                    Throw ex
                End Try
            End Using

        End Function

        Public Function GetListEmployee(ByVal _orgIds As List(Of Decimal), ByVal _filter As EmployeeDTO) As List(Of EmployeeDTO) _
                Implements ServiceContracts.IProfileBusiness.GetListEmployee
            Using rep As New ProfileRepository
                Try
                    Return rep.GetListEmployee(_orgIds, _filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmployeeOrgChart(ByVal lstOrg As List(Of Decimal), Optional ByVal log As UserLog = Nothing) As List(Of OrgChartDTO) _
                                        Implements ServiceContracts.IProfileBusiness.GetEmployeeOrgChart
            Using rep As New ProfileRepository
                Try
                    Return rep.GetEmployeeOrgChart(lstOrg, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetListEmployeePaging(ByVal _filter As EmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO) _
              Implements ServiceContracts.IProfileBusiness.GetListEmployeePaging
            Using rep As New ProfileRepository
                Try
                    Return rep.GetListEmployeePaging(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetChartEmployee(ByVal _filter As EmployeeDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO) _
            Implements ServiceContracts.IProfileBusiness.GetChartEmployee
            Using rep As New ProfileRepository
#If DEBUG Then

                Return Nothing
#Else
                Return rep.GetChartEmployee(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
#End If
            End Using
        End Function
        Public Function GetListEmployeePortal(ByVal _filter As EmployeeDTO) As List(Of EmployeeDTO) _
              Implements ServiceContracts.IProfileBusiness.GetListEmployeePortal
            Using rep As New ProfileRepository
                Try
                    Return rep.GetListEmployeePortal(_filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteEmployee(ByVal lstEmpID As List(Of Decimal), ByVal log As UserLog, ByRef sError As String) As Boolean _
                Implements ServiceContracts.IProfileBusiness.DeleteEmployee
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteEmployee(lstEmpID, log, sError)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateEmployee(ByVal sType As String, ByVal sEmpCode As String, ByVal value As String) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ValidateEmployee
            Using rep As New ProfileRepository
                Try
                    Return rep.ValidateEmployee(sType, sEmpCode, value)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        ''' <summary>
        ''' Hàm kiểm tra nhân viên có hợp đồng hay chưa.
        ''' </summary>
        ''' <param name="strEmpCode"></param>
        ''' <returns>true: nếu có</returns>
        ''' <remarks></remarks>
        Public Function CheckEmpHasContract(ByVal strEmpCode As String) As Boolean _
            Implements ServiceContracts.IProfileBusiness.CheckEmpHasContract
            Using rep As New ProfileRepository
                Try
                    Return rep.CheckEmpHasContract(strEmpCode)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function GetLineManager(ByVal username As String) As List(Of EmployeeDTO) _
            Implements IProfileBusiness.GetLineManager
            Using rep As New ProfileRepository
                Try
                    Return rep.GetLineManager(username)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetOrgFromUsername(ByVal username As String) As Decimal? _
            Implements IProfileBusiness.GetOrgFromUsername
            Using rep As New ProfileRepository
                Try
                    Return rep.GetOrgFromUsername(username)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetOrganizationTreeByID(ByVal _filter As OrganizationTreeDTO) As OrganizationTreeDTO _
    Implements ServiceContracts.IProfileBusiness.GetOrganizationTreeByID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetOrganizationTreeByID(_filter)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetOrgtree(ByVal _org_id As Decimal) As DataTable Implements ServiceContracts.IProfileBusiness.GetOrgtree
            Using rep As New ProfileRepository
                Try

                    Dim dtData = rep.GetOrgtree(_org_id)
                    Return dtData
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "EmployeeCV"
        Public Function GetEmployeeAllByID(ByVal sEmployeeID As Decimal,
                                  ByRef empCV As EmployeeCVDTO,
                                  ByRef empEdu As EmployeeEduDTO,
                                  ByRef empHealth As EmployeeHealthDTO,
                                  ByRef empUniform As UniformSizeDTO) As Boolean _
                                            Implements ServiceContracts.IProfileBusiness.GetEmployeeAllByID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetEmployeeAllByID(sEmployeeID, empCV, empEdu, empHealth, empUniform)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "EmployeeTrain"
        Public Function GetEmployeeTrain(ByVal _filter As EmployeeTrainDTO) As List(Of EmployeeTrainDTO) _
            Implements ServiceContracts.IProfileBusiness.GetEmployeeTrain
            Using rep As New ProfileRepository
                Try
                    Return rep.GetEmployeeTrain(_filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetEmployeeTrainForCompany(ByVal _filter As EmployeeTrainForCompanyDTO) As List(Of EmployeeTrainForCompanyDTO) _
            Implements ServiceContracts.IProfileBusiness.GetEmployeeTrainForCompany
            Using rep As New ProfileRepository
                Try
                    Return rep.GetEmployeeTrainForCompany(_filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetEmployeeTrainByID(ByVal EmployeeID As Decimal) As EmployeeTrainDTO _
                    Implements ServiceContracts.IProfileBusiness.GetEmployeeTrainByID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetEmployeeTrainByID(EmployeeID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertEmployeeTrain(ByVal objFamily As EmployeeTrainDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.InsertEmployeeTrain
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertEmployeeTrain(objFamily, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyEmployeeTrain(ByVal objFamily As EmployeeTrainDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyEmployeeTrain
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyEmployeeTrain(objFamily, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteNVBlackList(ByVal id_no As String, ByVal log As UserLog) As Boolean _
             Implements ServiceContracts.IProfileBusiness.DeleteNVBlackList
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteNVBlackList(id_no, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteEmployeeTrain(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteEmployeeTrain
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteEmployeeTrain(lstDecimals, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateEmployeeTrain(ByVal objValidate As EmployeeTrainDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ValidateEmployeeTrain
            Using rep As New ProfileRepository
                Try
                    Return rep.ValidateEmployeeTrain(objValidate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "WorkingBefore"
        Public Function GetEmpWorkingBefore(ByVal _filter As WorkingBeforeDTO) As List(Of WorkingBeforeDTO) _
            Implements ServiceContracts.IProfileBusiness.GetEmpWorkingBefore
            Using rep As New ProfileRepository
                Try
                    Return rep.GetEmpWorkingBefore(_filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertWorkingBefore(ByVal objWorkingBefore As WorkingBeforeDTO, _
                                            ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.InsertWorkingBefore
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertWorkingBefore(objWorkingBefore, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyWorkingBefore(ByVal objWorkingBefore As WorkingBeforeDTO, _
                                            ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
                                        Implements ServiceContracts.IProfileBusiness.ModifyWorkingBefore
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyWorkingBefore(objWorkingBefore, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteWorkingBefore(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteWorkingBefore
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteWorkingBefore(lstDecimals, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "thong tin li lich ca nhan"
        Public Function GetEmpBackGround(ByVal _filter As EmployeeBackgroundDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EFFECTIVE_DATE desc") As List(Of EmployeeBackgroundDTO) _
            Implements ServiceContracts.IProfileBusiness.GetEmpBackGround
            Using rep As New ProfileRepository
                Try
                    Return rep.GetEmpBackGround(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertBackGround(ByVal objBackGround As EmployeeBackgroundDTO, _
                                            ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.InsertBackGround
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertBackGround(objBackGround, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyBackGround(ByVal objBackGround As EmployeeBackgroundDTO, _
                                            ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
                                        Implements ServiceContracts.IProfileBusiness.ModifyBackGround
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyBackGround(objBackGround, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteBackGround(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteBackGround
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteBackGround(lstDecimals, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Employee Proccess"

        Public Function GetCommendProccess(ByVal _empId As System.Decimal) As List(Of ProfileDAL.CommendDTO) _
            Implements ServiceContracts.IProfileBusiness.GetCommendProccess
            Using rep As New ProfileRepository
                Try
                    Return rep.GetCommendProccess(_empId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetContractProccess(ByVal _empId As System.Decimal) As List(Of ProfileDAL.ContractDTO) _
            Implements ServiceContracts.IProfileBusiness.GetContractProccess
            Using rep As New ProfileRepository
                Try
                    Return rep.GetContractProccess(_empId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetConcurrentlyProccess(ByVal _empId As System.Decimal) As List(Of ProfileDAL.TitleConcurrentDTO) _
            Implements ServiceContracts.IProfileBusiness.GetConcurrentlyProccess
            Using rep As New ProfileRepository
                Try
                    Return rep.GetConcurrentlyProccess(_empId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetDisciplineProccess(ByVal _empId As System.Decimal) As List(Of ProfileDAL.DisciplineDTO) _
            Implements ServiceContracts.IProfileBusiness.GetDisciplineProccess
            Using rep As New ProfileRepository
                Try
                    Return rep.GetDisciplineProccess(_empId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetInsuranceProccess(ByVal _empId As System.Decimal) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetInsuranceProccess
            Using rep As New ProfileRepository
                Try
                    Return rep.GetInsuranceProccess(_empId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmployeeHistory(ByVal _empId As System.Decimal) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetEmployeeHistory
            Using rep As New ProfileRepository
                Try
                    Return rep.GetEmployeeHistory(_empId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetFamily(ByVal _empId As System.Decimal) As List(Of ProfileDAL.FamilyDTO) _
            Implements ServiceContracts.IProfileBusiness.GetFamily
            Using rep As New ProfileRepository
                Try
                    Return rep.GetFamily(_empId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetSalaryProccess(ByVal _empId As Decimal,
                                       Optional ByVal log As UserLog = Nothing) As List(Of ProfileDAL.WorkingDTO) _
                                   Implements ServiceContracts.IProfileBusiness.GetSalaryProccess
            Using rep As New ProfileRepository
                Try
                    Return rep.GetSalaryProccess(_empId, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetWelfareProccess(ByVal _empId As System.Decimal) As List(Of ProfileDAL.WelfareMngDTO) _
            Implements ServiceContracts.IProfileBusiness.GetWelfareProccess
            Using rep As New ProfileRepository
                Try
                    Return rep.GetWelfareProccess(_empId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetWorkingBefore(ByVal _empId As System.Decimal) As List(Of ProfileDAL.WorkingBeforeDTO) _
            Implements ServiceContracts.IProfileBusiness.GetWorkingBefore
            Using rep As New ProfileRepository
                Try
                    Return rep.GetWorkingBefore(_empId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function GetWorkingProccess(ByVal _empId As Decimal?,
                                       Optional ByVal log As UserLog = Nothing) As List(Of ProfileDAL.WorkingDTO) _
                                   Implements ServiceContracts.IProfileBusiness.GetWorkingProccess
            Using rep As New ProfileRepository
                Try
                    Return rep.GetWorkingProccess(_empId, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        'Qua trinh danh gia KPI"
        Public Function GetAssessKPIEmployee(ByVal _empId As System.Decimal) As List(Of EmployeeAssessmentDTO) _
    Implements ServiceContracts.IProfileBusiness.GetAssessKPIEmployee
            Using rep As New ProfileRepository
                Try
                    Return rep.GetAssessKPIEmployee(_empId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        'Qua trinh nang luc
        Public Function GetCompetencyEmployee(ByVal _empId As System.Decimal) As List(Of EmployeeCompetencyDTO) _
Implements ServiceContracts.IProfileBusiness.GetCompetencyEmployee
            Using rep As New ProfileRepository
                Try
                    Return rep.GetCompetencyEmployee(_empId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        'Qua trinh ngach bac
        Public Function GetSalaryChanged(ByVal _empId As System.Decimal) As DataTable Implements ServiceContracts.IProfileBusiness.GetSalaryChanged
            Using rep As New ProfileRepository
                Try
                    Return rep.GetSalaryChanged(_empId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region " Quá trình đào tạo ngoài công ty"
        Public Function GetProcessTraining(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTO,
                                      Optional ByRef PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO) _
                                  Implements ServiceContracts.IProfileBusiness.GetProcessTraining
            Using rep As New ProfileRepository
                Try
                    Return rep.GetProcessTraining(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertProcessTraining(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTO,
                                              ByVal log As UserLog,
                                              ByRef gID As Decimal) As Boolean _
                                          Implements ServiceContracts.IProfileBusiness.InsertProcessTraining
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertProcessTraining(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyProcessTraining(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
                               Implements ServiceContracts.IProfileBusiness.ModifyProcessTraining
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyProcessTraining(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteProcessTraining(ByVal lstID As List(Of Decimal)) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteProcessTraining
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteProcessTraining(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetCertificateType() As List(Of OtherListDTO) _
            Implements ServiceContracts.IProfileBusiness.GetCertificateType
            Using rep As New ProfileRepository
                Try
                    Return rep.GetCertificateType()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "EmployeeEdit"
        Public Function GetChangedCVList(ByVal lstEmpEdit As List(Of EmployeeEditDTO)) As Dictionary(Of String, String) _
            Implements ServiceContracts.IProfileBusiness.GetChangedCVList
            Using rep As New ProfileRepository
                Try
                    Return rep.GetChangedCVList(lstEmpEdit)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmployeeEditByID(ByVal _filter As EmployeeEditDTO) As EmployeeEditDTO _
            Implements ServiceContracts.IProfileBusiness.GetEmployeeEditByID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetEmployeeEditByID(_filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertEmployeeEdit(ByVal objEmployeeEdit As EmployeeEditDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.InsertEmployeeEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertEmployeeEdit(objEmployeeEdit, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyEmployeeEdit(ByVal objEmployeeEdit As EmployeeEditDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyEmployeeEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyEmployeeEdit(objEmployeeEdit, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteEmployeeEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteEmployeeEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteEmployeeEdit(lstDecimals, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function SendEmployeeEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.SendEmployeeEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.SendEmployeeEdit(lstID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateStatusEmployeeEdit(ByVal lstID As List(Of Decimal),
                                                   ByVal status As String,
                                                   ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.UpdateStatusEmployeeEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.UpdateStatusEmployeeEdit(lstID, status, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveEmployeeEdit(ByVal _filter As EmployeeEditDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of EmployeeEditDTO) _
            Implements ServiceContracts.IProfileBusiness.GetApproveEmployeeEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.GetApproveEmployeeEdit(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "IPORTAL - Quá trình đào tạo ngoài công ty"
        Public Function GetProcessTrainingEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT) _
            Implements ServiceContracts.IProfileBusiness.GetProcessTrainingEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.GetProcessTrainingEdit(_filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertProcessTrainingEdit(ByVal objTrainOutCompany As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
                                Implements ServiceContracts.IProfileBusiness.InsertProcessTrainingEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertProcessTrainingEdit(objTrainOutCompany, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyProcessTrainingEdit(ByVal objTrainOutCompany As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyProcessTrainingEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyProcessTrainingEdit(objTrainOutCompany, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteProcessTrainingEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteProcessTrainingEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteProcessTrainingEdit(lstDecimals, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckExistProcessTrainingEdit(ByVal pk_key As Decimal) As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT _
            Implements ServiceContracts.IProfileBusiness.CheckExistProcessTrainingEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.CheckExistProcessTrainingEdit(pk_key)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function SendProcessTrainingEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.SendProcessTrainingEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.SendProcessTrainingEdit(lstID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateStatusProcessTrainingEdit(ByVal lstID As List(Of Decimal),
                                                   ByVal status As String,
                                                   ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.UpdateStatusProcessTrainingEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.UpdateStatusProcessTrainingEdit(lstID, status, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveProcessTrainingEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_ID desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT) _
            Implements ServiceContracts.IProfileBusiness.GetApproveProcessTrainingEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.GetApproveProcessTrainingEdit(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "IPORTAL - Quá trình công tác ngoài công ty"
        Public Function GetWorkingBeforeEdit(ByVal _filter As WorkingBeforeDTOEdit) As List(Of WorkingBeforeDTOEdit) _
            Implements ServiceContracts.IProfileBusiness.GetWorkingBeforeEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.GetWorkingBeforeEdit(_filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertWorkingBeforeEdit(ByVal objWorkingBefore As WorkingBeforeDTOEdit, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
             Implements ServiceContracts.IProfileBusiness.InsertWorkingBeforeEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertWorkingBeforeEdit(objWorkingBefore, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyWorkingBeforeEdit(ByVal objWorkingBefore As WorkingBeforeDTOEdit,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyWorkingBeforeEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyWorkingBeforeEdit(objWorkingBefore, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteWorkingBeforeEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteWorkingBeforeEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteWorkingBeforeEdit(lstDecimals, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckExistWorkingBeforeEdit(ByVal pk_key As Decimal) As WorkingBeforeDTOEdit _
            Implements ServiceContracts.IProfileBusiness.CheckExistWorkingBeforeEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.CheckExistWorkingBeforeEdit(pk_key)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function SendWorkingBeforeEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.SendWorkingBeforeEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.SendWorkingBeforeEdit(lstID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateStatusWorkingBeforeEdit(ByVal lstID As List(Of Decimal),
                                                   ByVal status As String,
                                                   ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.UpdateStatusWorkingBeforeEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.UpdateStatusWorkingBeforeEdit(lstID, status, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveWorkingBeforeEdit(ByVal _filter As WorkingBeforeDTOEdit,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_ID desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of WorkingBeforeDTOEdit) _
            Implements ServiceContracts.IProfileBusiness.GetApproveWorkingBeforeEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.GetApproveWorkingBeforeEdit(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Portal Xem diem, chuc nang tuong ung voi khoa hoc"
        Public Function GetPortalCompetencyCourse(ByVal _empId As System.Decimal) As List(Of EmployeeCriteriaRecordDTO) _
Implements ServiceContracts.IProfileBusiness.GetPortalCompetencyCourse
            Using rep As New ProfileRepository
                Try
                    Return rep.GetPortalCompetencyCourse(_empId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

        Public Function EXPORT_EMP(ByVal P_USERNAME As String, ByVal P_ORGID As Decimal, ByVal P_ISDISSOLVE As Boolean, ByVal P_STARTDATE As Date?, ByVal P_TODATE As Date?, ByVal P_IS_ALL As Boolean) As DataSet Implements ServiceContracts.IProfileBusiness.EXPORT_EMP
            Using rep As New ProfileRepository
                Try

                    Dim dtData = rep.EXPORT_EMP(P_USERNAME, P_ORGID, P_ISDISSOLVE, P_STARTDATE, P_TODATE, P_IS_ALL)
                    Return dtData
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
    End Class
End Namespace
