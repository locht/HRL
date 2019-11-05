Imports RecruitmentDAL
Imports Framework.Data
Imports System.Collections.Generic
Imports RecruitmentBusiness.ServiceContracts


' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace RecruitmentBusiness.ServiceImplementations
    Partial Class RecruitmentBusiness

#Region "PlanReg"

        Public Function GetPlanReg(ByVal _filter As PlanRegDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal isSearch As Boolean = False,
                                        Optional ByVal log As UserLog = Nothing) As List(Of PlanRegDTO) Implements ServiceContracts.IRecruitmentBusiness.GetPlanReg
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetPlanReg(_filter, PageIndex, PageSize, Total, _param, Sorts, isSearch, log)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetPlanRegByID(ByVal _filter As PlanRegDTO) As PlanRegDTO Implements ServiceContracts.IRecruitmentBusiness.GetPlanRegByID
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetPlanRegByID(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertPlanReg(ByVal objPlanReg As PlanRegDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.InsertPlanReg
            Try
                Return RecruitmentRepositoryStatic.Instance.InsertPlanReg(objPlanReg, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyPlanReg(ByVal objPlanReg As PlanRegDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.ModifyPlanReg
            Try
                Return RecruitmentRepositoryStatic.Instance.ModifyPlanReg(objPlanReg, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeletePlanReg(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IRecruitmentBusiness.DeletePlanReg
            Try
                Return RecruitmentRepositoryStatic.Instance.DeletePlanReg(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdateStatusPlanReg(ByVal lstID As List(Of Decimal), status As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.UpdateStatusPlanReg
            Try
                Return RecruitmentRepositoryStatic.Instance.UpdateStatusPlanReg(lstID, status)
            Catch ex As Exception

                Throw ex
            End Try
        End Function


#End Region

#Region "PlanSummary"

        Public Function GetPlanSummary(ByVal _year As Decimal, ByVal _param As ParamDTO, ByVal log As UserLog) As DataTable Implements ServiceContracts.IRecruitmentBusiness.GetPlanSummary
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetPlanSummary(_year, _param, log)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region

#Region "Request"

        Public Function GetRequest(ByVal _filter As RequestDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of RequestDTO) Implements ServiceContracts.IRecruitmentBusiness.GetRequest
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetRequest(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetRequestByID(ByVal _filter As RequestDTO) As RequestDTO Implements ServiceContracts.IRecruitmentBusiness.GetRequestByID
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetRequestByID(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        
        Public Function InsertRequest(ByVal objRequest As RequestDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.InsertRequest
            Try
                Return RecruitmentRepositoryStatic.Instance.InsertRequest(objRequest, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyRequest(ByVal objRequest As RequestDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.ModifyRequest
            Try
                Return RecruitmentRepositoryStatic.Instance.ModifyRequest(objRequest, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteRequest(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IRecruitmentBusiness.DeleteRequest
            Try
                Return RecruitmentRepositoryStatic.Instance.DeleteRequest(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdateStatusRequest(ByVal lstID As List(Of Decimal),
                                            status As Decimal,
                                            ByVal log As UserLog) As Boolean Implements ServiceContracts.IRecruitmentBusiness.UpdateStatusRequest
            Try
                Return RecruitmentRepositoryStatic.Instance.UpdateStatusRequest(lstID, status, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function


#End Region

#Region "Program"

        Public Function GetProgram(ByVal _filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO) Implements ServiceContracts.IRecruitmentBusiness.GetProgram
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetProgram(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetProgramSearch(ByVal _filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO) Implements ServiceContracts.IRecruitmentBusiness.GetProgramSearch
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetProgramSearch(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetProgramByID(ByVal _filter As ProgramDTO) As ProgramDTO Implements ServiceContracts.IRecruitmentBusiness.GetProgramByID
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetProgramByID(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyProgram(ByVal objProgram As ProgramDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.ModifyProgram
            Try
                Return RecruitmentRepositoryStatic.Instance.ModifyProgram(objProgram, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function XuatToTrinh(ByVal sID As Decimal) As DataTable _
            Implements ServiceContracts.IRecruitmentBusiness.XuatToTrinh
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.XuatToTrinh(sID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try

        End Function

#Region "ProgramExams"

        Public Function GetProgramExams(ByVal _filter As ProgramExamsDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EXAMS_ORDER") As List(Of ProgramExamsDTO) Implements ServiceContracts.IRecruitmentBusiness.GetProgramExams
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetProgramExams(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetProgramExamsByID(ByVal _filter As ProgramExamsDTO) As ProgramExamsDTO _
            Implements ServiceContracts.IRecruitmentBusiness.GetProgramExamsByID
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetProgramExamsByID(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function UpdateProgramExams(ByVal objExams As ProgramExamsDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IRecruitmentBusiness.UpdateProgramExams
            Try
                Return RecruitmentRepositoryStatic.Instance.UpdateProgramExams(objExams, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteProgramExams(ByVal obj As ProgramExamsDTO) As Boolean Implements ServiceContracts.IRecruitmentBusiness.DeleteProgramExams
            Try
                Return RecruitmentRepositoryStatic.Instance.DeleteProgramExams(obj)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#End Region

#Region "Candidate"

        Public Function CheckExistCandidate(ByVal strEmpCode As String) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.CheckExistCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.CheckExistCandidate(strEmpCode)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateInsertCandidate(ByVal sEmpId As String, ByVal sID_No As String, ByVal sFullName As String,
                                               ByVal dBirthDate As Date, ByVal sType As String) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.ValidateInsertCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.ValidateInsertCandidate(sEmpId, sID_No, sFullName, dBirthDate, sType)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCandidateFamily_ByID(ByVal sCandidateID As Decimal) As CandidateFamilyDTO _
            Implements ServiceContracts.IRecruitmentBusiness.GetCandidateFamily_ByID
            Try
                Dim rep As New RecruitmentRepository
                Return rep.GetCandidateFamily_ByID(sCandidateID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCandidateImage(ByVal gEmpID As Decimal, ByRef sError As String) As Byte() _
            Implements ServiceContracts.IRecruitmentBusiness.GetCandidateImage
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateImage(gEmpID, sError)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CreateNewCandidateCode() As CandidateDTO _
                               Implements ServiceContracts.IRecruitmentBusiness.CreateNewCandidateCode

            Try
                Dim rep As New RecruitmentRepository
                Return rep.CreateNewCandidateCode()
            Catch ex As Exception
                WriteExceptionLog(ex, "InsertCandidateCode")
                Throw ex
            End Try

        End Function
        Public Function InsertCandidate(ByVal objEmp As CandidateDTO, ByVal log As UserLog, ByRef gID As Decimal, _
                                        ByRef _strEmpCode As String, _
                                        ByVal _imageBinary As Byte(), _
                                         ByVal objEmpCV As CandidateCVDTO, _
                                         ByVal objEmpEdu As CandidateEduDTO, _
                                         ByVal objEmpOther As CandidateOtherInfoDTO, _
                                         ByVal objEmpHealth As CandidateHealthDTO, _
                                         ByVal objEmpExpect As CandidateExpectDTO, _
                                         ByVal objEmpFamily As CandidateFamilyDTO) As Boolean _
                                Implements ServiceContracts.IRecruitmentBusiness.InsertCandidate

            Try
                Dim rep As New RecruitmentRepository
                Return rep.InsertCandidate(objEmp, log, gID, _strEmpCode, _imageBinary, objEmpCV, objEmpEdu _
                                             , objEmpOther, objEmpHealth, objEmpExpect, objEmpFamily)
            Catch ex As Exception
                WriteExceptionLog(ex, "InsertCandidate")
                Throw ex
            End Try

        End Function

        Public Function ModifyCandidate(ByVal objEmp As CandidateDTO, ByVal log As UserLog, ByRef gID As Decimal, _
                                        ByVal _imageBinary As Byte(), _
                                         ByVal objEmpCV As CandidateCVDTO, _
                                         ByVal objEmpEdu As CandidateEduDTO, _
                                        ByVal objEmpOther As CandidateOtherInfoDTO, _
                                         ByVal objEmpHealth As CandidateHealthDTO, _
                                         ByVal objEmpExpect As CandidateExpectDTO, _
                                         ByVal objEmpFamily As CandidateFamilyDTO) As Boolean _
                                Implements ServiceContracts.IRecruitmentBusiness.ModifyCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.ModifyCandidate(objEmp, log, gID, _imageBinary, objEmpCV, objEmpEdu, _
                                             objEmpOther, objEmpHealth, objEmpExpect, objEmpFamily)
            Catch ex As Exception
                WriteExceptionLog(ex, "ModifyCandidate")
                Throw ex
            End Try

        End Function

        Public Function GetListCandidatePaging(ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                     ByVal _filter As CandidateDTO,
                                     Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO) _
              Implements ServiceContracts.IRecruitmentBusiness.GetListCandidatePaging
            Try
                Dim rep As New RecruitmentRepository
                Return rep.GetListCandidatePaging(PageIndex, PageSize, Total, _filter, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetFindCandidatePaging(ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                     ByVal _filter As CandidateDTO,
                                     Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO) _
              Implements ServiceContracts.IRecruitmentBusiness.GetFindCandidatePaging
            Try
                Dim rep As New RecruitmentRepository
                Return rep.GetFindCandidatePaging(PageIndex, PageSize, Total, _filter, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetListCandidateTransferPaging(ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                     ByVal _filter As CandidateDTO,
                                     Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO) _
              Implements ServiceContracts.IRecruitmentBusiness.GetListCandidateTransferPaging
            Try
                Dim rep As New RecruitmentRepository
                Return rep.GetListCandidateTransferPaging(PageIndex, PageSize, Total, _filter, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetListCandidate(ByVal _filter As CandidateDTO,
                                     Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO) _
              Implements ServiceContracts.IRecruitmentBusiness.GetListCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.GetListCandidate(_filter, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteCandidate(ByVal lstEmpID As List(Of Decimal), ByVal log As UserLog, ByRef sError As String) As Boolean _
                Implements ServiceContracts.IRecruitmentBusiness.DeleteCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.DeleteCandidate(lstEmpID, log, sError)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        ''' <summary>
        ''' Lay thong tin nhan vien tu CandidateCode
        ''' </summary>
        ''' <param name="sCandidateCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetCandidateInfo(ByVal sCandidateCode As String) As CandidateDTO _
                                             Implements ServiceContracts.IRecruitmentBusiness.GetCandidateInfo
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateInfo(sCandidateCode)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try

        End Function

        Public Function GetCandidateCV(ByVal sCandidateID As Decimal) As CandidateCVDTO _
                                                    Implements ServiceContracts.IRecruitmentBusiness.GetCandidateCV
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateCV(sCandidateID)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetCandidateEdu(ByVal sCandidateID As Decimal) As CandidateEduDTO _
                                                    Implements ServiceContracts.IRecruitmentBusiness.GetCandidateEdu
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateEdu(sCandidateID)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        

        Public Function GetCandidateOtherInfo(ByVal sCandidateID As Decimal) As CandidateOtherInfoDTO _
                                                 Implements ServiceContracts.IRecruitmentBusiness.GetCandidateOtherInfo
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateOtherInfo(sCandidateID)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetCandidateHealthInfo(ByVal sCandidateID As Decimal) As CandidateHealthDTO _
                                                    Implements ServiceContracts.IRecruitmentBusiness.GetCandidateHealthInfo
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateHealthInfo(sCandidateID)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetCandidateExpectInfo(ByVal sCandidateID As Decimal) As CandidateExpectDTO _
                                                    Implements ServiceContracts.IRecruitmentBusiness.GetCandidateExpectInfo
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateExpectInfo(sCandidateID)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetCandidateHistory(ByVal sCandidateID As Decimal, ByVal sCandidateIDNO As Decimal) As List(Of CandidateHistoryDTO) _
                                            Implements ServiceContracts.IRecruitmentBusiness.GetCandidateHistory
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateHistory(sCandidateID, sCandidateIDNO)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function UpdateProgramCandidate(ByVal lstCanID As List(Of Decimal), programID As Decimal, ByVal log As UserLog) As Boolean _
                Implements ServiceContracts.IRecruitmentBusiness.UpdateProgramCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.UpdateProgramCandidate(lstCanID, programID, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdateStatusCandidate(ByVal lstCanID As List(Of Decimal), statusID As String, ByVal log As UserLog) As Boolean _
                Implements ServiceContracts.IRecruitmentBusiness.UpdateStatusCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.UpdateStatusCandidate(lstCanID, statusID, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdatePontentialCandidate(ByVal lstCanID As List(Of Decimal), bCheck As Boolean, ByVal log As UserLog) As Boolean _
                Implements ServiceContracts.IRecruitmentBusiness.UpdatePontentialCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.UpdatePontentialCandidate(lstCanID, bCheck, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdateBlackListCandidate(ByVal lstCanID As List(Of Decimal), bCheck As Boolean, ByVal log As UserLog) As Boolean _
                Implements ServiceContracts.IRecruitmentBusiness.UpdateBlackListCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.UpdateBlackListCandidate(lstCanID, bCheck, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdateReHireCandidate(ByVal lstCanID As List(Of Decimal), bCheck As Boolean, ByVal log As UserLog) As Boolean _
                Implements ServiceContracts.IRecruitmentBusiness.UpdateReHireCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.UpdateReHireCandidate(lstCanID, bCheck, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCandidateImport() As DataSet _
              Implements ServiceContracts.IRecruitmentBusiness.GetCandidateImport
            Try
                Dim rep As New RecruitmentRepository
                Return rep.GetCandidateImport
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ImportCandidate(lst As List(Of CandidateImportDTO), ByVal log As UserLog) As Boolean _
                      Implements ServiceContracts.IRecruitmentBusiness.ImportCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.ImportCandidate(lst, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function TransferHSNVToCandidate(empID As Decimal,
                                            orgID As Decimal,
                                            titleID As Decimal,
                                            programID As Decimal,
                                            ByVal log As UserLog) As Boolean _
                      Implements ServiceContracts.IRecruitmentBusiness.TransferHSNVToCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.TransferHSNVToCandidate(empID, orgID, titleID, programID, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#Region "CandidateFamily"
        Public Function GetCandidateFamily(ByVal _filter As CandidateFamilyDTO) As List(Of CandidateFamilyDTO) _
            Implements ServiceContracts.IRecruitmentBusiness.GetCandidateFamily
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetCandidateFamily(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCandidateFamily(ByVal objFamily As CandidateFamilyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.InsertCandidateFamily
            Try
                Return RecruitmentRepositoryStatic.Instance.InsertCandidateFamily(objFamily, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCandidateFamily(ByVal objFamily As CandidateFamilyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.ModifyCandidateFamily
            Try
                Return RecruitmentRepositoryStatic.Instance.ModifyCandidateFamily(objFamily, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteCandidateFamily(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.DeleteCandidateFamily
            Try
                Return RecruitmentRepositoryStatic.Instance.DeleteCandidateFamily(lstDecimals, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateFamily(ByVal objFamily As CandidateFamilyDTO) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.ValidateFamily
            Try
                Return RecruitmentRepositoryStatic.Instance.ValidateFamily(objFamily)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
#End Region

#Region "Cá nhân tự đào tạo"
        Public Function GetCandidateTrainSinger(ByVal _filter As TrainSingerDTO) As List(Of TrainSingerDTO) _
            Implements ServiceContracts.IRecruitmentBusiness.GetCandidateTrainSinger
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateTrainSinger(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCandidateTrainSinger(ByVal objTrainSinger As TrainSingerDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.InsertCandidateTrainSinger
            Try
                Dim rep As New RecruitmentRepository
                Return rep.InsertCandidateTrainSinger(objTrainSinger, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCandidateTrainSinger(ByVal objTrainSinger As TrainSingerDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.ModifyCandidateTrainSinger
            Try
                Dim rep As New RecruitmentRepository
                Return rep.ModifyCandidateTrainSinger(objTrainSinger, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteCandidateTrainSinger(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.DeleteCandidateTrainSinger
            Try
                Dim rep As New RecruitmentRepository
                Return rep.DeleteCandidateTrainSinger(lstDecimals, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region

#Region "Người tham chiếu"
        Public Function GetCandidateReference(ByVal _filter As CandidateReferenceDTO) As List(Of CandidateReferenceDTO) _
            Implements ServiceContracts.IRecruitmentBusiness.GetCandidateReference
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateReference(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCandidateReference(ByVal objReference As CandidateReferenceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.InsertCandidateReference
            Try
                Dim rep As New RecruitmentRepository
                Return rep.InsertCandidateReference(objReference, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCandidateReference(ByVal objReference As CandidateReferenceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.ModifyCandidateReference
            Try
                Dim rep As New RecruitmentRepository
                Return rep.ModifyCandidateReference(objReference, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteCandidateReference(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.DeleteCandidateReference
            Try
                Dim rep As New RecruitmentRepository
                Return rep.DeleteCandidateReference(lstDecimals, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region
#Region "Trước khi vào MLG"
        Public Function GetCandidateBeforeWT(ByVal _filter As CandidateBeforeWTDTO) As List(Of CandidateBeforeWTDTO) _
            Implements ServiceContracts.IRecruitmentBusiness.GetCandidateBeforeWT
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateBeforeWT(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCandidateBeforeWT(ByVal objCandidateBeforeWT As CandidateBeforeWTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.InsertCandidateBeforeWT
            Try
                Dim rep As New RecruitmentRepository
                Return rep.InsertCandidateBeforeWT(objCandidateBeforeWT, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCandidateBeforeWT(ByVal objCandidateBeforeWT As CandidateBeforeWTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.ModifyCandidateBeforeWT
            Try
                Dim rep As New RecruitmentRepository
                Return rep.ModifyCandidateBeforeWT(objCandidateBeforeWT, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteCandidateBeforeWT(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.DeleteCandidateBeforeWT
            Try
                Dim rep As New RecruitmentRepository
                Return rep.DeleteCandidateBeforeWT(lstDecimals, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region

#End Region

#Region "ProgramSchedule"

        Public Function GetProgramSchedule(ByVal _filter As ProgramScheduleDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramScheduleDTO) Implements ServiceContracts.IRecruitmentBusiness.GetProgramSchedule
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetProgramSchedule(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetProgramScheduleByID(ByVal _filter As ProgramScheduleDTO) As ProgramScheduleDTO Implements ServiceContracts.IRecruitmentBusiness.GetProgramScheduleByID
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetProgramScheduleByID(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Function GetCandidateNotScheduleByScheduleID(ByVal _filter As ProgramScheduleCanDTO) As List(Of ProgramScheduleCanDTO) _
                            Implements ServiceContracts.IRecruitmentBusiness.GetCandidateNotScheduleByScheduleID
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetCandidateNotScheduleByScheduleID(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Function GetCandidateScheduleByScheduleID(ByVal _filter As ProgramScheduleCanDTO) As List(Of ProgramScheduleCanDTO) _
                                    Implements ServiceContracts.IRecruitmentBusiness.GetCandidateScheduleByScheduleID
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetCandidateScheduleByScheduleID(_filter)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function UpdateProgramSchedule(ByVal objExams As ProgramScheduleDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IRecruitmentBusiness.UpdateProgramSchedule
            Try
                Return RecruitmentRepositoryStatic.Instance.UpdateProgramSchedule(objExams, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "CandidateResult"

        Public Function GetCandidateResult(ByVal _filter As ProgramScheduleCanDTO) As List(Of ProgramScheduleCanDTO) Implements ServiceContracts.IRecruitmentBusiness.GetCandidateResult
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetCandidateResult(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function UpdateCandidateResult(ByVal lst As List(Of ProgramScheduleCanDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.IRecruitmentBusiness.UpdateCandidateResult
            Try
                Return RecruitmentRepositoryStatic.Instance.UpdateCandidateResult(lst, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Cost"

        Public Function GetCost(ByVal _filter As CostDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CostDTO) Implements ServiceContracts.IRecruitmentBusiness.GetCost
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetCost(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function UpdateCost(ByVal objExams As CostDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IRecruitmentBusiness.UpdateCost
            Try
                Return RecruitmentRepositoryStatic.Instance.UpdateCost(objExams, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateCost(ByVal objCost As CostDTO) As Boolean Implements ServiceContracts.IRecruitmentBusiness.ValidateCost
            Try
                Return RecruitmentRepositoryStatic.Instance.ValidateCost(objCost)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteCost(ByVal obj As CostDTO) As Boolean Implements ServiceContracts.IRecruitmentBusiness.DeleteCost
            Try
                Return RecruitmentRepositoryStatic.Instance.DeleteCost(obj)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "CV Pool"

        Public Function GetCVPoolEmpRecord(ByVal _filter As CVPoolEmpDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of CVPoolEmpDTO) Implements ServiceContracts.IRecruitmentBusiness.GetCVPoolEmpRecord
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetCVPoolEmpRecord(_filter, PageIndex, PageSize, Total, Sorts, log)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region
    End Class
End Namespace