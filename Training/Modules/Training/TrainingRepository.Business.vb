Imports Training.TrainingBusiness
Imports Framework.UI

Partial Class TrainingRepository

#Region "Otherlist"

    Public Function GetCourseList() As List(Of CourseDTO)
        Dim lstCourse As List(Of CourseDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstCourse = rep.GetCourseList()
                Return lstCourse
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetTitlesByOrgs(ByVal orgIds As List(Of Decimal), ByVal langCode As String) As List(Of PlanTitleDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetTitlesByOrgs(orgIds, langCode)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetEntryAndFormByCourseID(ByVal CourseId As Decimal, ByVal langCode As String) As CourseDTO
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetEntryAndFormByCourseID(CourseId, langCode)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetCenters() As List(Of CenterDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetCenters()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Plan"

    Public Function GetPlans(ByVal filter As PlanDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PlanDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetPlans(filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPlanById(ByVal Id As Decimal) As PlanDTO
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetPlanById(Id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertPlan(ByVal plan As PlanDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertPlan(plan, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyPlan(ByVal plan As PlanDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyPlan(plan, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeletePlans(ByVal lstPlanIDs As List(Of Decimal)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeletePlans(lstPlanIDs)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Request"

    Public Function GetTrainingRequests(ByVal filter As RequestDTO,
                                         ByVal PageIndex As Integer, ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of RequestDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetTrainingRequests(filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetTrainingRequestsByID(ByVal filter As RequestDTO) As RequestDTO
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetTrainingRequestsByID(filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetEmployeeByImportRequest(ByRef lstEmpCode As List(Of RequestEmpDTO)) As String
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetEmployeeByImportRequest(lstEmpCode)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetEmployeeByPlanID(ByVal filter As RequestDTO) As List(Of RequestEmpDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetEmployeeByPlanID(filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function


    Public Function InsertRequest(ByVal Request As RequestDTO,
                                  lstEmp As List(Of RequestEmpDTO),
                                  ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertRequest(Request, lstEmp, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyRequest(ByVal Request As RequestDTO,
                                  lstEmp As List(Of RequestEmpDTO),
                                  ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyRequest(Request, lstEmp, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function UpdateStatusTrainingRequests(ByVal lstID As List(Of Decimal), ByVal status As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.UpdateStatusTrainingRequests(lstID, status)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteTrainingRequests(ByVal lstRequestID As List(Of Decimal)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteTrainingRequests(lstRequestID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPlanRequestByID(ByVal Id As Decimal) As PlanDTO
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetPlanRequestByID(Id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Program"
    Public Function GetRequestsForProgram(ByVal ReqID As Decimal) As RequestDTO
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetRequestsForProgram(ReqID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetPrograms(ByVal filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetPrograms(filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetPlan_Cost_Detail(ByVal Id As Decimal) As List(Of CostDetailDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetPlan_Cost_Detail(Id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetProgramById(ByVal Id As Decimal) As ProgramDTO
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetProgramById(Id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetProgramByChooseFormId(ByVal Id As Decimal) As ProgramDTO
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetProgramByChooseFormId(Id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertProgram(ByVal Program As ProgramDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertProgram(Program, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyProgram(ByVal Program As ProgramDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyProgram(Program, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeletePrograms(ByVal lstProgramIDs As List(Of Decimal)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeletePrograms(lstProgramIDs)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Prepare"

    Public Function GetPrepare(ByVal _filter As ProgramPrepareDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramPrepareDTO)
        Dim lstPrepare As List(Of ProgramPrepareDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstPrepare = rep.GetPrepare(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstPrepare
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertPrepare(ByVal objPrepare As ProgramPrepareDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertPrepare(objPrepare, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyPrepare(ByVal objPrepare As ProgramPrepareDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyPrepare(objPrepare, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeletePrepare(ByVal lstPrepare As List(Of ProgramPrepareDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeletePrepare(lstPrepare)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Class"

    Public Function GetClass(ByVal _filter As ProgramClassDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramClassDTO)
        Dim lstClass As List(Of ProgramClassDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstClass = rep.GetClass(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClass
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetClassByID(ByVal _filter As ProgramClassDTO) As ProgramClassDTO

        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetClassByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertClass(ByVal objClass As ProgramClassDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertClass(objClass, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyClass(ByVal objClass As ProgramClassDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyClass(objClass, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteClass(ByVal lstClass As List(Of ProgramClassDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteClass(lstClass)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


#End Region

#Region "ClassStudent"

    Public Function GetEmployeeByClassID(ByVal _filter As ProgramClassStudentDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramClassStudentDTO)
        Dim lstClass As List(Of ProgramClassStudentDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstClass = rep.GetEmployeeByClassID(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClass
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetEmployeeNotByClassID(ByVal _filter As ProgramClassStudentDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramClassStudentDTO)
        Dim lstClass As List(Of ProgramClassStudentDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstClass = rep.GetEmployeeNotByClassID(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClass
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertClassStudent(ByVal lst As List(Of ProgramClassStudentDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertClassStudent(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using


    End Function

    Public Function DeleteClassStudent(ByVal lst As List(Of ProgramClassStudentDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteClassStudent(lst, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "ClassSchedule"

    Public Function GetClassSchedule(ByVal _filter As ProgramClassScheduleDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramClassScheduleDTO)
        Dim lstClassSchedule As List(Of ProgramClassScheduleDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstClassSchedule = rep.GetClassSchedule(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClassSchedule
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetClassScheduleByID(ByVal _filter As ProgramClassScheduleDTO) As ProgramClassScheduleDTO

        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetClassScheduleByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertClassSchedule(ByVal objClassSchedule As ProgramClassScheduleDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertClassSchedule(objClassSchedule, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyClassSchedule(ByVal objClassSchedule As ProgramClassScheduleDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyClassSchedule(objClassSchedule, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteClassSchedule(ByVal lstClassSchedule As List(Of ProgramClassScheduleDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteClassSchedule(lstClassSchedule)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


#End Region

#Region "ProgramCommit"


    Public Function GetProgramCommit(ByVal _filter As ProgramCommitDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramCommitDTO)
        Dim lstClassSchedule As List(Of ProgramCommitDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstClassSchedule = rep.GetProgramCommit(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClassSchedule
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateProgramCommit(ByVal lst As List(Of ProgramCommitDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.UpdateProgramCommit(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function



#End Region

#Region "ProgramResult"


    Public Function GetProgramResult(ByVal _filter As ProgramResultDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramResultDTO)
        Dim lstClassSchedule As List(Of ProgramResultDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstClassSchedule = rep.GetProgramResult(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClassSchedule
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateProgramResult(ByVal lst As List(Of ProgramResultDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.UpdateProgramResult(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function



#End Region

#Region "ProgramCost"

    Public Function GetProgramCost(ByVal _filter As ProgramCostDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramCostDTO)
        Dim lstProgramCost As List(Of ProgramCostDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstProgramCost = rep.GetProgramCost(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstProgramCost
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertProgramCost(ByVal objProgramCost As ProgramCostDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertProgramCost(objProgramCost, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateProgramCost(ByVal objProgramCost As ProgramCostDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ValidateProgramCost(objProgramCost)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyProgramCost(ByVal objProgramCost As ProgramCostDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyProgramCost(objProgramCost, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteProgramCost(ByVal lstProgramCost As List(Of ProgramCostDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteProgramCost(lstProgramCost)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Reimbursement"

    Public Function GetReimbursement(ByVal _filter As ReimbursementDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ReimbursementDTO)
        Dim lstReimbursement As List(Of ReimbursementDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstReimbursement = rep.GetReimbursement(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstReimbursement
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertReimbursement(ByVal objReimbursement As ReimbursementDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertReimbursement(objReimbursement, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateReimbursement(ByVal objReimbursement As ReimbursementDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ValidateReimbursement(objReimbursement)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyReimbursement(ByVal objReimbursement As ReimbursementDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyReimbursement(objReimbursement, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "ChooseForm"

    Public Function GetChooseForm(ByVal _filter As ChooseFormDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ChooseFormDTO)
        Dim lstChooseForm As List(Of ChooseFormDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstChooseForm = rep.GetChooseForm(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstChooseForm
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertChooseForm(ByVal objChooseForm As ChooseFormDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertChooseForm(objChooseForm, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateChooseForm(ByVal objChooseForm As ChooseFormDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ValidateChooseForm(objChooseForm)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyChooseForm(ByVal objChooseForm As ChooseFormDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyChooseForm(objChooseForm, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteChooseForm(ByVal lst As List(Of ChooseFormDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteChooseForm(lst)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "ChooseForm"

    Public Function GetEmployeeAssessmentResult(ByVal _filter As AssessmentResultDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of AssessmentResultDTO)
       
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetEmployeeAssessmentResult(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetAssessmentResultByID(ByVal _filter As AssessmentResultDtlDTO) As List(Of AssessmentResultDtlDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetAssessmentResultByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateAssessmentResult(ByVal obj As AssessmentResultDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.UpdateAssessmentResult(obj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Employee_record"

    Public Function GetListEmployeePaging(ByVal _filter As RecordEmployeeDTO, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of RecordEmployeeDTO)
        Dim lstCertificate As List(Of RecordEmployeeDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstCertificate = rep.GetListEmployeePaging(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Log)
                Return lstCertificate
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetListEmployeePaging(ByVal _filter As RecordEmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of RecordEmployeeDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetListEmployeePaging(_filter, PageIndex, PageSize, Total, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function GetEmployeeRecord(ByVal _filter As RecordEmployeeDTO, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of RecordEmployeeDTO)
        Dim lstCertificate As List(Of RecordEmployeeDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstCertificate = rep.GetEmployeeRecord(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Log)
                Return lstCertificate
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetEmployeeRecord(ByVal _filter As RecordEmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of RecordEmployeeDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetEmployeeRecord(_filter, PageIndex, PageSize, Total, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Employee Title Course"
    Public Function GetEmployeeTitleCourse(ByVal filter As EmployeeTitleCourseDTO, ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "HU_EMPLOYEE_ID desc") As List(Of EmployeeTitleCourseDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetEmployeeTitleCourse(filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region
End Class
