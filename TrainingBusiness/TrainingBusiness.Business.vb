Imports TrainingBusiness.ServiceContracts
Imports TrainingDAL
Imports Framework.Data
Imports System.Collections.Generic

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace TrainingBusiness.ServiceImplementations
    Partial Class TrainingBusiness

#Region "Otherlist"

        Public Function GetCourseList() As List(Of CourseDTO) Implements ServiceContracts.ITrainingBusiness.GetCourseList
            Try
                Dim rep As New TrainingRepository
                Return rep.GetCourseList()
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetIDCourseList(ByVal idSelected As String) As List(Of CourseDTO) Implements ServiceContracts.ITrainingBusiness.GetIDCourseList
            Try
                Dim rep As New TrainingRepository
                Return rep.GetIDCourseList(idSelected)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetTitlesByOrgs(ByVal orgIds As List(Of Decimal), ByVal langCode As String) As List(Of PlanTitleDTO) Implements ServiceContracts.ITrainingBusiness.GetTitlesByOrgs
            Try
                Dim rep As New TrainingRepository
                Return rep.GetTitlesByOrgs(orgIds, langCode)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetWIByTitle(ByVal orgIds As List(Of Decimal), ByVal langCode As String) As List(Of PlanTitleDTO) Implements ServiceContracts.ITrainingBusiness.GetWIByTitle
            Try
                Dim rep As New TrainingRepository
                Return rep.GetWIByTitle(orgIds, langCode)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetEntryAndFormByCourseID(ByVal CourseId As Decimal, ByVal langCode As String) As CourseDTO Implements ServiceContracts.ITrainingBusiness.GetEntryAndFormByCourseID
            Try
                Dim rep As New TrainingRepository
                Return rep.GetEntryAndFormByCourseID(CourseId, langCode)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Plan"
        Public Function GetPlans(ByVal filter As PlanDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PlanDTO) Implements ServiceContracts.ITrainingBusiness.GetPlans
            Try
                Dim rep As New TrainingRepository
                Return rep.GetPlans(filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function GetPlanById(ByVal Id As Decimal) As PlanDTO Implements ServiceContracts.ITrainingBusiness.GetPlanById
            Try
                Dim rep As New TrainingRepository
                Return rep.GetPlanById(Id)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function InsertPlan(ByVal plan As PlanDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertPlan
            Try
                Dim rep As New TrainingRepository
                Return rep.InsertPlan(plan, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ModifyPlan(ByVal plan As PlanDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyPlan
            Try
                Dim rep As New TrainingRepository
                Return rep.ModifyPlan(plan, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function DeletePlans(ByVal lstId As List(Of Decimal)) As Boolean Implements ServiceContracts.ITrainingBusiness.DeletePlans
            Try
                Dim rep As New TrainingRepository
                Return rep.DeletePlans(lstId)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Request"

        Public Function GetTrainingRequests(ByVal filter As RequestDTO,
                                         ByVal PageIndex As Integer, ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of RequestDTO) _
                                     Implements ServiceContracts.ITrainingBusiness.GetTrainingRequests
            Try
                Dim rep As New TrainingRepository
                Return rep.GetTrainingRequests(filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetTrainingRequestsByID(ByVal filter As RequestDTO) As RequestDTO _
                                     Implements ServiceContracts.ITrainingBusiness.GetTrainingRequestsByID
            Try
                Dim rep As New TrainingRepository
                Return rep.GetTrainingRequestsByID(filter)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetEmployeeByPlanID(ByVal filter As RequestDTO) As List(Of RequestEmpDTO) _
                                     Implements ServiceContracts.ITrainingBusiness.GetEmployeeByPlanID
            Try
                Dim rep As New TrainingRepository
                Return rep.GetEmployeeByPlanID(filter)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetEmployeeByImportRequest(ByRef lstEmpCode As List(Of RequestEmpDTO)) As String _
                                     Implements ServiceContracts.ITrainingBusiness.GetEmployeeByImportRequest
            Try
                Dim rep As New TrainingRepository
                Return rep.GetEmployeeByImportRequest(lstEmpCode)
            Catch ex As Exception
                Throw ex
            End Try
        End Function



        Function InsertRequest(ByVal Request As RequestDTO,
                                  ByVal lstEmp As List(Of RequestEmpDTO),
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.ITrainingBusiness.InsertRequest
            Try
                Dim rep As New TrainingRepository
                Return rep.InsertRequest(Request, lstEmp, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ModifyRequest(ByVal Request As RequestDTO,
                                  ByVal lstEmp As List(Of RequestEmpDTO),
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
                              Implements ServiceContracts.ITrainingBusiness.ModifyRequest
            Try
                Dim rep As New TrainingRepository
                Return rep.ModifyRequest(Request, lstEmp, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function UpdateStatusTrainingRequests(ByVal lstID As List(Of Decimal), ByVal status As Decimal) As Boolean _
                              Implements ServiceContracts.ITrainingBusiness.UpdateStatusTrainingRequests
            Try
                Dim rep As New TrainingRepository
                Return rep.UpdateStatusTrainingRequests(lstID, status)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteTrainingRequests(ByVal lstRequestID As List(Of Decimal)) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteTrainingRequests
            Try
                Dim rep As New TrainingRepository
                Return rep.DeleteTrainingRequests(lstRequestID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function GetPlanRequestByID(ByVal Id As Decimal) As PlanDTO Implements ServiceContracts.ITrainingBusiness.GetPlanRequestByID
            Try
                Dim rep As New TrainingRepository
                Return rep.GetPlanRequestByID(Id)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Program"
        Public Function GetRequestsForProgram(ByVal ReqID As Decimal) As RequestDTO Implements ServiceContracts.ITrainingBusiness.GetRequestsForProgram
            Try
                Dim rep As New TrainingRepository
                Return rep.GetRequestsForProgram(ReqID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetPrograms(ByVal filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO) Implements ServiceContracts.ITrainingBusiness.GetPrograms
            Try
                Dim rep As New TrainingRepository
                Return rep.GetPrograms(filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetPlan_Cost_Detail(ByVal Id As Decimal) As List(Of CostDetailDTO) Implements ServiceContracts.ITrainingBusiness.GetPlan_Cost_Detail
            Try
                Dim rep As New TrainingRepository
                Return rep.GetPlan_Cost_Detail(Id)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Function GetProgramById(ByVal Id As Decimal) As ProgramDTO Implements ServiceContracts.ITrainingBusiness.GetProgramById
            Try
                Dim rep As New TrainingRepository
                Return rep.GetProgramById(Id)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function GetProgramByChooseFormId(ByVal Id As Decimal) As ProgramDTO _
            Implements ServiceContracts.ITrainingBusiness.GetProgramByChooseFormId
            Try
                Dim rep As New TrainingRepository
                Return rep.GetProgramByChooseFormId(Id)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function InsertProgram(ByVal Program As ProgramDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertProgram
            Try
                Dim rep As New TrainingRepository
                Return rep.InsertProgram(Program, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ModifyProgram(ByVal Program As ProgramDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyProgram
            Try
                Dim rep As New TrainingRepository
                Return rep.ModifyProgram(Program, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function DeletePrograms(ByVal lstId As List(Of Decimal)) As Boolean Implements ServiceContracts.ITrainingBusiness.DeletePrograms
            Try
                Dim rep As New TrainingRepository
                Return rep.DeletePrograms(lstId)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ValidateClassProgram(ByVal lstId As List(Of Decimal)) As Boolean Implements ServiceContracts.ITrainingBusiness.ValidateClassProgram
            Try
                Dim rep As New TrainingRepository
                Return rep.ValidateClassProgram(lstId)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Prepare"

        Public Function GetPrepare(ByVal _filter As ProgramPrepareDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramPrepareDTO) Implements ServiceContracts.ITrainingBusiness.GetPrepare
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetPrepare(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertPrepare(ByVal objPrepare As ProgramPrepareDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertPrepare
            Try
                Return TrainingRepositoryStatic.Instance.InsertPrepare(objPrepare, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyPrepare(ByVal objPrepare As ProgramPrepareDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyPrepare
            Try
                Return TrainingRepositoryStatic.Instance.ModifyPrepare(objPrepare, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeletePrepare(ByVal lstPrepare() As ProgramPrepareDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.DeletePrepare
            Try
                Return TrainingRepositoryStatic.Instance.DeletePrepare(lstPrepare)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Class"

        Public Function GetClass(ByVal _filter As ProgramClassDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramClassDTO) Implements ServiceContracts.ITrainingBusiness.GetClass
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetClass(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetClassByID(ByVal _filter As ProgramClassDTO) As ProgramClassDTO _
            Implements ServiceContracts.ITrainingBusiness.GetClassByID
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetClassByID(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertClass(ByVal objClass As ProgramClassDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertClass
            Try
                Return TrainingRepositoryStatic.Instance.InsertClass(objClass, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyClass(ByVal objClass As ProgramClassDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyClass
            Try
                Return TrainingRepositoryStatic.Instance.ModifyClass(objClass, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteClass(ByVal lstClass() As ProgramClassDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteClass
            Try
                Return TrainingRepositoryStatic.Instance.DeleteClass(lstClass)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Class Student"

        Public Function GetEmployeeNotByClassID(ByVal _filter As ProgramClassStudentDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramClassStudentDTO) _
            Implements ServiceContracts.ITrainingBusiness.GetEmployeeNotByClassID
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetEmployeeNotByClassID(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetEmployeeByClassID(ByVal _filter As ProgramClassStudentDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramClassStudentDTO) _
            Implements ServiceContracts.ITrainingBusiness.GetEmployeeByClassID
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetEmployeeByClassID(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertClassStudent(ByVal lst As List(Of ProgramClassStudentDTO), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.ITrainingBusiness.InsertClassStudent
            Try
                Return TrainingRepositoryStatic.Instance.InsertClassStudent(lst, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteClassStudent(ByVal lst As List(Of ProgramClassStudentDTO), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.ITrainingBusiness.DeleteClassStudent
            Try
                Return TrainingRepositoryStatic.Instance.DeleteClassStudent(lst, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "ClassSchedule"

        Public Function GetClassSchedule(ByVal _filter As ProgramClassScheduleDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramClassScheduleDTO) Implements ServiceContracts.ITrainingBusiness.GetClassSchedule
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetClassSchedule(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetClassScheduleByID(ByVal _filter As ProgramClassScheduleDTO) As ProgramClassScheduleDTO _
            Implements ServiceContracts.ITrainingBusiness.GetClassScheduleByID
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetClassScheduleByID(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertClassSchedule(ByVal objClassSchedule As ProgramClassScheduleDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertClassSchedule
            Try
                Return TrainingRepositoryStatic.Instance.InsertClassSchedule(objClassSchedule, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyClassSchedule(ByVal objClassSchedule As ProgramClassScheduleDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyClassSchedule
            Try
                Return TrainingRepositoryStatic.Instance.ModifyClassSchedule(objClassSchedule, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteClassSchedule(ByVal lstClassSchedule() As ProgramClassScheduleDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteClassSchedule
            Try
                Return TrainingRepositoryStatic.Instance.DeleteClassSchedule(lstClassSchedule)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "ProgramCommit"

        Public Function GetProgramCommit(ByVal _filter As ProgramCommitDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramCommitDTO) Implements ServiceContracts.ITrainingBusiness.GetProgramCommit
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetProgramCommit(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function


        Public Function UpdateProgramCommit(ByVal lst As List(Of ProgramCommitDTO),
                                   ByVal log As UserLog) As Boolean Implements ServiceContracts.ITrainingBusiness.UpdateProgramCommit
            Try
                Return TrainingRepositoryStatic.Instance.UpdateProgramCommit(lst, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region


#Region "ProgramResult"

        Public Function GetProgramResult(ByVal _filter As ProgramResultDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramResultDTO) Implements ServiceContracts.ITrainingBusiness.GetProgramResult
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetProgramResult(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function


        Public Function UpdateProgramResult(ByVal lst As List(Of ProgramResultDTO),
                                   ByVal log As UserLog) As Boolean Implements ServiceContracts.ITrainingBusiness.UpdateProgramResult
            Try
                Return TrainingRepositoryStatic.Instance.UpdateProgramResult(lst, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region


#Region "ProgramCost"

        Public Function GetProgramCost(ByVal _filter As ProgramCostDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramCostDTO) Implements ServiceContracts.ITrainingBusiness.GetProgramCost
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetProgramCost(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertProgramCost(ByVal objProgramCost As ProgramCostDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertProgramCost
            Try
                Return TrainingRepositoryStatic.Instance.InsertProgramCost(objProgramCost, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateProgramCost(ByVal objProgramCost As ProgramCostDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.ValidateProgramCost
            Try
                Return TrainingRepositoryStatic.Instance.ValidateProgramCost(objProgramCost)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyProgramCost(ByVal objProgramCost As ProgramCostDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyProgramCost
            Try
                Return TrainingRepositoryStatic.Instance.ModifyProgramCost(objProgramCost, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteProgramCost(ByVal lstProgramCost() As ProgramCostDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteProgramCost
            Try
                Return TrainingRepositoryStatic.Instance.DeleteProgramCost(lstProgramCost)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region


#Region "Reimbursement"

        Public Function GetReimbursement(ByVal _filter As ReimbursementDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ReimbursementDTO) Implements ServiceContracts.ITrainingBusiness.GetReimbursement
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetReimbursement(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertReimbursement(ByVal objReimbursement As ReimbursementDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertReimbursement
            Try
                Return TrainingRepositoryStatic.Instance.InsertReimbursement(objReimbursement, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateReimbursement(ByVal objReimbursement As ReimbursementDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.ValidateReimbursement
            Try
                Return TrainingRepositoryStatic.Instance.ValidateReimbursement(objReimbursement)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyReimbursement(ByVal objReimbursement As ReimbursementDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyReimbursement
            Try
                Return TrainingRepositoryStatic.Instance.ModifyReimbursement(objReimbursement, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region

#Region "ChooseForm"

        Public Function GetChooseForm(ByVal _filter As ChooseFormDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ChooseFormDTO) Implements ServiceContracts.ITrainingBusiness.GetChooseForm
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetChooseForm(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertChooseForm(ByVal objChooseForm As ChooseFormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertChooseForm
            Try
                Return TrainingRepositoryStatic.Instance.InsertChooseForm(objChooseForm, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateChooseForm(ByVal objChooseForm As ChooseFormDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.ValidateChooseForm
            Try
                Return TrainingRepositoryStatic.Instance.ValidateChooseForm(objChooseForm)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyChooseForm(ByVal objChooseForm As ChooseFormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyChooseForm
            Try
                Return TrainingRepositoryStatic.Instance.ModifyChooseForm(objChooseForm, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteChooseForm(ByVal lst() As ChooseFormDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteChooseForm
            Try
                Return TrainingRepositoryStatic.Instance.DeleteChooseForm(lst)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region

#Region "AssessmentResult"

        Public Function GetEmployeeAssessmentResult(ByVal _filter As AssessmentResultDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of AssessmentResultDTO) _
                                    Implements ServiceContracts.ITrainingBusiness.GetEmployeeAssessmentResult
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetEmployeeAssessmentResult(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetAssessmentResultByID(ByVal _filter As AssessmentResultDtlDTO) As List(Of AssessmentResultDtlDTO) _
            Implements ServiceContracts.ITrainingBusiness.GetAssessmentResultByID
            Try
                Return TrainingRepositoryStatic.Instance.GetAssessmentResultByID(_filter)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function UpdateAssessmentResult(ByVal obj As AssessmentResultDTO,
                                   ByVal log As UserLog) As Boolean _
                               Implements ServiceContracts.ITrainingBusiness.UpdateAssessmentResult
            Try
                Return TrainingRepositoryStatic.Instance.UpdateAssessmentResult(obj, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region

#Region "TranningRecord"
        Public Function GetListEmployeePaging(ByVal _filter As RecordEmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of RecordEmployeeDTO) _
              Implements ServiceContracts.ITrainingBusiness.GetListEmployeePaging

            Try
                Return TrainingRepositoryStatic.Instance.GetListEmployeePaging(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetEmployeeRecord(ByVal _filter As RecordEmployeeDTO,
                                  ByVal PageIndex As Integer,
                                  ByVal PageSize As Integer,
                                  ByRef Total As Integer, ByVal _param As ParamDTO,
                                  Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                  Optional ByVal log As UserLog = Nothing) As List(Of RecordEmployeeDTO) _
      Implements ServiceContracts.ITrainingBusiness.GetEmployeeRecord

            Try
                Return TrainingRepositoryStatic.Instance.GetEmployeeRecord(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Employee Title Course"
        Public Function GetEmployeeTitleCourse(ByVal filter As EmployeeTitleCourseDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "HU_EMPLOYEE_ID desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of EmployeeTitleCourseDTO) Implements ServiceContracts.ITrainingBusiness.GetEmployeeTitleCourse
            Try
                Dim rep As New TrainingRepository
                Return rep.GetEmployeeTitleCourse(filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
    End Class
End Namespace
