Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic
Imports System.Reflection

Partial Class ProfileRepository
#Region "evaluate"
    Public Function GetTrainingEvaluateEmp(ByVal _empId As Decimal) As List(Of TrainningEvaluateDTO)

        Try
            Dim query = From p In Context.HU_TRAININGEVALUATE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From ot In Context.PE_PERIOD.Where(Function(f) f.ID = p.EVALUATE_ID).DefaultIfEmpty
                        From ot1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RANK_ID).DefaultIfEmpty
                        From ot2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.CAPACITY_ID).DefaultIfEmpty
                        Where p.EMPLOYEE_ID = _empId
            ' lọc điều kiện
            Dim trainingforeign = query.Select(Function(p) New TrainningEvaluateDTO With {
                                                .ID = p.p.ID,
                                                .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                                .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                .ORG_ID = p.e.ID,
                                                .ORG_NAME = p.o.NAME_VN,
                                                .ORG_DESC = p.o.DESCRIPTION_PATH,
                                                .TITLE_ID = p.p.TITLE_ID,
                                                .TITLE_NAME = p.t.NAME_VN,
                                                .SIGN_DATE = p.p.SIGN_DATE,
                                                .DECISION_NO = p.p.DECISION_NO,
                                                .EFFECT_DATE = p.p.EFFECT_DATE,
                                                .EVALUATE_ID = p.p.EVALUATE_ID,
                                                .EVALUATE_NAME = p.ot.NAME,
                                                .RANK_ID = p.p.RANK_ID,
                                                .YEAR = p.p.YEAR,
                                                .REMARK = p.p.REMARK,
                                                .RANK_NAME = p.ot1.NAME_VN,
                                                .CAPACITY_ID = p.p.CAPACITY_ID,
                                                .CAPACITY_NAME = p.ot2.NAME_VN,
                                                .CONTENT = p.p.CONTENT,
                                                .CREATED_DATE = p.p.CREATED_DATE
                                                })
            Return trainingforeign.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function GetTrainingEvaluate(ByVal _filter As TrainningEvaluateDTO, ByVal PageIndex As Integer,
                              ByVal PageSize As Integer,
                              ByRef Total As Integer, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc",
                              Optional ByVal log As UserLog = Nothing) As List(Of TrainningEvaluateDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            'Dim query = From p In Context.HU_CONTRACT Order By p.HU_EMPLOYEE.EMPLOYEE_CODE
            '            From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
            '            From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
            '            From c In Context.HU_CONTRACT_TYPE.Where(Function(f) p.CONTRACT_TYPE_ID = f.ID)
            '            From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
            '            From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
            '            From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
            '                                                           f.USERNAME = log.Username.ToUpper)

            Dim query = From p In Context.HU_TRAININGEVALUATE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                       From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                       From ot In Context.PE_PERIOD.Where(Function(f) f.ID = p.EVALUATE_ID).DefaultIfEmpty
                        From ot1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RANK_ID).DefaultIfEmpty
                          From ot2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.CAPACITY_ID).DefaultIfEmpty
                            From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                            f.USERNAME = log.Username.ToUpper)
            ' lọc điều kiện
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> terID) Or (p.e.WORK_STATUS = terID And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            ' select thuộc tính
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.SIGN_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.SIGN_DATE = _filter.SIGN_DATE)
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.t.NAME_VN = _filter.TITLE_NAME)
            End If
            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.o.NAME_VN = _filter.ORG_NAME)
            End If
            If _filter.EVALUATE_ID IsNot Nothing Then
                query = query.Where(Function(p) p.p.EVALUATE_ID = _filter.EVALUATE_ID)
            End If
            If _filter.YEAR IsNot Nothing Then
                query = query.Where(Function(p) p.p.YEAR = _filter.YEAR)
            End If
            If _filter.CONTENT IsNot Nothing Then
                query = query.Where(Function(p) p.p.CONTENT = _filter.CONTENT)
            End If

            Dim trainingforeign = query.Select(Function(p) New TrainningEvaluateDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .ORG_ID = p.e.ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .TITLE_ID = p.p.TITLE_ID,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .SIGN_DATE = p.p.SIGN_DATE,
                                            .DECISION_NO = p.p.DECISION_NO,
                                            .EFFECT_DATE = p.p.EFFECT_DATE,
                                            .EVALUATE_ID = p.p.EVALUATE_ID,
                                            .EVALUATE_NAME = p.ot.NAME,
                                            .RANK_ID = p.p.RANK_ID,
                                            .YEAR = p.p.YEAR,
                                            .REMARK = p.p.REMARK,
                                            .RANK_NAME = p.ot1.NAME_VN,
                                            .CAPACITY_ID = p.p.CAPACITY_ID,
                                            .CAPACITY_NAME = p.ot2.NAME_VN,
                                            .CONTENT = p.p.CONTENT,
            .CREATED_DATE = p.p.CREATED_DATE
                                            })

            trainingforeign = trainingforeign.OrderBy(Sorts)
            Total = trainingforeign.Count
            trainingforeign = trainingforeign.Skip(PageIndex * PageSize).Take(PageSize)
            Return trainingforeign.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function InsertTrainingEvaluate(ByVal objContract As TrainningEvaluateDTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_TRAININGEVALUATE
        Try
            objContractData.ID = Utilities.GetNextSequence(Context, Context.HU_TRAININGEVALUATE.EntitySet.Name)
            objContract.ID = objContractData.ID
            objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            objContractData.TITLE_ID = objContract.TITLE_ID
            objContractData.ORG_ID = objContract.ORG_ID
            objContractData.DECISION_NO = objContract.DECISION_NO
            objContractData.SIGN_DATE = objContract.SIGN_DATE
            objContractData.EFFECT_DATE = objContract.EFFECT_DATE
            objContractData.EVALUATE_ID = objContract.EVALUATE_ID
            objContractData.RANK_ID = objContract.RANK_ID
            objContractData.CAPACITY_ID = objContract.CAPACITY_ID
            objContractData.REMARK = objContract.REMARK
            objContractData.CONTENT = objContract.CONTENT
            objContractData.YEAR = objContract.YEAR
            Context.HU_TRAININGEVALUATE.AddObject(objContractData)
            ' Phê duyệt
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
    Public Function ModifyTrainingEvaluate(ByVal objContract As TrainningEvaluateDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_TRAININGEVALUATE With {.ID = objContract.ID}
        Try
            objContractData = (From p In Context.HU_TRAININGEVALUATE Where p.ID = objContract.ID).FirstOrDefault

            objContract.ID = objContractData.ID
            objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            objContractData.TITLE_ID = objContract.TITLE_ID
            objContractData.ORG_ID = objContract.ORG_ID
            objContractData.DECISION_NO = objContract.DECISION_NO
            objContractData.SIGN_DATE = objContract.SIGN_DATE
            objContractData.EFFECT_DATE = objContract.EFFECT_DATE
            objContractData.EVALUATE_ID = objContract.EVALUATE_ID
            objContractData.RANK_ID = objContract.RANK_ID
            objContractData.CAPACITY_ID = objContract.CAPACITY_ID
            objContractData.REMARK = objContract.REMARK
            objContractData.CONTENT = objContract.CONTENT
            objContractData.YEAR = objContract.YEAR
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
    Public Function GetTrainingEvaluateById(ByVal _filter As TrainningEvaluateDTO) As TrainningEvaluateDTO
        Try
            Dim query = From p In Context.HU_TRAININGEVALUATE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                       From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                       From ot In Context.PE_PERIOD.Where(Function(f) p.EVALUATE_ID = f.ID)
                      From ot1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RANK_ID).DefaultIfEmpty
                     From ot2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.CAPACITY_ID).DefaultIfEmpty
            Where (p.ID = _filter.ID)
                        Select New TrainningEvaluateDTO With {.ID = p.ID,
                                                     .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                     .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                     .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                     .ORG_ID = e.ID,
                                                     .ORG_NAME = o.NAME_VN,
                                                     .ORG_DESC = o.DESCRIPTION_PATH,
                                                     .TITLE_NAME = t.NAME_VN,
                                                     .SIGN_DATE = p.SIGN_DATE,
                                                     .CONTENT = p.CONTENT,
                                                     .DECISION_NO = p.DECISION_NO,
                                                    .EVALUATE_ID = p.EVALUATE_ID,
                                                    .EVALUATE_NAME = ot.NAME,
                                                    .RANK_ID = p.RANK_ID,
                                                    .RANK_NAME = ot1.NAME_VN,
                                                    .CAPACITY_ID = p.CAPACITY_ID,
                                                    .CAPACITY_NAME = ot2.NAME_VN,
                                                    .YEAR = p.YEAR,
                                                    .EFFECT_DATE = p.EFFECT_DATE,
                                                    .REMARK = p.REMARK
                            }
            Dim result = query.FirstOrDefault
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function
    Public Function DeleteTrainingEvaluate(ByVal objContract As TrainningEvaluateDTO) As Boolean
        Dim objContractData As HU_TRAININGEVALUATE
        Try
            ' Xóa  hợp đồng
            objContractData = (From p In Context.HU_TRAININGEVALUATE Where objContract.ID = p.ID).SingleOrDefault
            If Not objContractData Is Nothing Then
                Context.HU_TRAININGEVALUATE.DeleteObject(objContractData)
                Context.SaveChanges()
                Return True
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
#End Region
#Region "TRANINGMANAGE"
    Public Function GetListTrainingManageByEmpID(ByVal _filter As TrainningManageDTO, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningManageDTO)

        Try

            'Using cls As New DataAccess.QueryData
            '    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
            '                     New With {.P_USERNAME = log.Username,
            '                               .P_ORGID = _param.ORG_ID,
            '                               .P_ISDISSOLVE = _param.IS_DISSOLVE})
            'End Using
            'Dim query = From p In Context.HU_CONTRACT Order By p.HU_EMPLOYEE.EMPLOYEE_CODE
            '            From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
            '            From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
            '            From c In Context.HU_CONTRACT_TYPE.Where(Function(f) p.CONTRACT_TYPE_ID = f.ID)
            '            From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
            '            From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
            '            From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
            '                                                           f.USERNAME = log.Username.ToUpper)

            Dim query = From p In Context.HU_TRAININGMANAGE.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                       From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                       From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TRAINING_ID)
            ' lọc điều kiện
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> terID) Or (p.e.WORK_STATUS = terID And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            ' select thuộc tính
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.FROM_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE <= _filter.TO_DATE)
            End If
            If _filter.START_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE = _filter.START_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.EXPIRE_DATE = _filter.EXPIRE_DATE)
            End If
            If _filter.DEGREE_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.DEGREE_DATE = _filter.DEGREE_DATE)
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.t.NAME_VN = _filter.TITLE_NAME)
            End If
            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.o.NAME_VN = _filter.ORG_NAME)
            End If
            If _filter.LOCATION IsNot Nothing Then
                query = query.Where(Function(p) p.p.LOCATION = _filter.LOCATION)
            End If
            If _filter.DEGREE_EXPIRE_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.DEGREE_EXPIRE_DATE = _filter.DEGREE_EXPIRE_DATE)
            End If

            Dim trainingforeign = query.Select(Function(p) New TrainningManageDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .ORG_ID = p.p.ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .TITLE_ID = p.p.TITLE_ID,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .START_DATE = p.p.START_DATE,
                                            .EXPIRE_DATE = p.p.EXPIRE_DATE,
                                            .DEGREE_DATE = p.p.DEGREE_DATE,
                                            .PROGRAM_TRAINING = p.p.PROGRAM_TRAINING,
                                            .TRAINNING_ID = p.p.TRAINING_ID,
                                            .TRAINNING_NAME = p.ot.NAME_VN,
                                            .CERTIFICATE = p.p.CERTIFICATE,
                                            .UNIT = p.p.UNIT,
                                            .COST = p.p.COST,
                                            .RESULT_TRAIN = p.p.RESULT_TRAIN,
                                            .DEGREE_EXPIRE_DATE = p.p.DEGREE_EXPIRE_DATE,
                                            .REMARK = p.p.REMARK,
                                            .LOCATION = p.p.LOCATION,
                                            .CREATED_DATE = p.p.CREATED_DATE
                                            })

            trainingforeign = trainingforeign.OrderBy(Sorts)
            'Total = trainingforeign.Count
            'trainingforeign = trainingforeign.Skip(PageIndex * PageSize).Take(PageSize)
            Return trainingforeign.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetTrainingManage(ByVal _filter As TrainningManageDTO, ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of TrainningManageDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            'Dim query = From p In Context.HU_CONTRACT Order By p.HU_EMPLOYEE.EMPLOYEE_CODE
            '            From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
            '            From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
            '            From c In Context.HU_CONTRACT_TYPE.Where(Function(f) p.CONTRACT_TYPE_ID = f.ID)
            '            From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
            '            From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
            '            From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
            '                                                           f.USERNAME = log.Username.ToUpper)

            Dim query = From p In Context.HU_TRAININGMANAGE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TRAINING_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                  f.USERNAME = log.Username.ToUpper)
            ' lọc điều kiện
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> terID) Or (p.e.WORK_STATUS = terID And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            ' select thuộc tính
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.FROM_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE <= _filter.TO_DATE)
            End If
            If _filter.START_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE = _filter.START_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.EXPIRE_DATE = _filter.EXPIRE_DATE)
            End If
            If _filter.DEGREE_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.DEGREE_DATE = _filter.DEGREE_DATE)
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.t.NAME_VN = _filter.TITLE_NAME)
            End If
            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.o.NAME_VN = _filter.ORG_NAME)
            End If
            If _filter.LOCATION IsNot Nothing Then
                query = query.Where(Function(p) p.p.LOCATION = _filter.LOCATION)
            End If
            If _filter.DEGREE_EXPIRE_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.DEGREE_EXPIRE_DATE = _filter.DEGREE_EXPIRE_DATE)
            End If

            Dim trainingforeign = query.Select(Function(p) New TrainningManageDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .ORG_ID = p.e.ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .TITLE_ID = p.p.TITLE_ID,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .START_DATE = p.p.START_DATE,
                                            .EXPIRE_DATE = p.p.EXPIRE_DATE,
                                            .DEGREE_DATE = p.p.DEGREE_DATE,
                                            .PROGRAM_TRAINING = p.p.PROGRAM_TRAINING,
                                            .TRAINNING_ID = p.p.TRAINING_ID,
                                            .TRAINNING_NAME = p.ot.NAME_VN,
                                            .CERTIFICATE = p.p.CERTIFICATE,
                                            .UNIT = p.p.UNIT,
                                            .COST = p.p.COST,
                                            .RESULT_TRAIN = p.p.RESULT_TRAIN,
                                            .DEGREE_EXPIRE_DATE = p.p.DEGREE_EXPIRE_DATE,
                                            .REMARK = p.p.REMARK,
                                            .LOCATION = p.p.LOCATION,
                                            .CREATED_DATE = p.p.CREATED_DATE
                                            })

            trainingforeign = trainingforeign.OrderBy(Sorts)
            Total = trainingforeign.Count
            trainingforeign = trainingforeign.Skip(PageIndex * PageSize).Take(PageSize)
            Return trainingforeign.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function InsertTrainingManage(ByVal objContract As TrainningManageDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_TRAININGMANAGE
        Try
            objContractData.ID = Utilities.GetNextSequence(Context, Context.HU_TRAININGMANAGE.EntitySet.Name)
            objContract.ID = objContractData.ID
            objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            objContractData.TITLE_ID = objContract.TITLE_ID
            objContractData.ORG_ID = objContract.ORG_ID
            objContractData.START_DATE = objContract.START_DATE
            objContractData.EXPIRE_DATE = objContract.EXPIRE_DATE
            objContractData.DEGREE_DATE = objContract.DEGREE_DATE
            objContractData.PROGRAM_TRAINING = objContract.PROGRAM_TRAINING
            objContractData.TRAINING_ID = objContract.TRAINNING_ID
            objContractData.CERTIFICATE = objContract.CERTIFICATE
            objContractData.UNIT = objContract.UNIT
            objContractData.RESULT_TRAIN = objContract.RESULT_TRAIN
            objContractData.REMARK = objContract.REMARK
            objContractData.DEGREE_EXPIRE_DATE = objContract.DEGREE_EXPIRE_DATE
            objContractData.COST = objContract.COST
            objContractData.LOCATION = objContract.LOCATION

            Context.HU_TRAININGMANAGE.AddObject(objContractData)
            ' Phê duyệt
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
    Public Function ModifyTrainingManage(ByVal objContract As TrainningManageDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_TRAININGMANAGE With {.ID = objContract.ID}
        Try
            objContractData = (From p In Context.HU_TRAININGMANAGE Where p.ID = objContract.ID).FirstOrDefault
            objContract.ID = objContractData.ID
            objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            objContractData.TITLE_ID = objContract.TITLE_ID
            objContractData.ORG_ID = objContract.ORG_ID
            objContractData.START_DATE = objContract.START_DATE
            objContractData.EXPIRE_DATE = objContract.EXPIRE_DATE
            objContractData.DEGREE_DATE = objContract.DEGREE_DATE
            objContractData.PROGRAM_TRAINING = objContract.PROGRAM_TRAINING
            objContractData.TRAINING_ID = objContract.TRAINNING_ID
            objContractData.CERTIFICATE = objContract.CERTIFICATE
            objContractData.UNIT = objContract.UNIT
            objContractData.RESULT_TRAIN = objContract.RESULT_TRAIN
            objContractData.REMARK = objContract.REMARK
            objContractData.DEGREE_EXPIRE_DATE = objContract.DEGREE_EXPIRE_DATE
            objContractData.COST = objContract.COST
            objContractData.LOCATION = objContract.LOCATION
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
    Public Function GetTrainingManageById(ByVal _filter As TrainningManageDTO) As TrainningManageDTO
        Try
            Dim query = From p In Context.HU_TRAININGMANAGE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                       From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                       From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TRAINING_ID).DefaultIfEmpty
            Where (p.ID = _filter.ID)
                        Select New TrainningManageDTO With {.ID = p.ID,
                                                     .START_DATE = p.START_DATE,
                                                     .EXPIRE_DATE = p.EXPIRE_DATE,
                                                     .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                     .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                     .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                     .ORG_ID = e.ID,
                                                     .ORG_NAME = o.NAME_VN,
                                                     .ORG_DESC = o.DESCRIPTION_PATH,
                                                     .TITLE_NAME = t.NAME_VN,
                                                     .LOCATION = p.LOCATION,
                                                     .TRAINNING_ID = p.TRAINING_ID,
                                                     .TRAINNING_NAME = ot.NAME_VN,
                                                      .DEGREE_DATE = p.DEGREE_DATE,
                                                      .PROGRAM_TRAINING = p.PROGRAM_TRAINING,
                                                    .CERTIFICATE = p.CERTIFICATE,
                                                    .UNIT = p.UNIT,
                                                    .COST = p.COST,
                                                    .RESULT_TRAIN = p.RESULT_TRAIN,
                                                    .DEGREE_EXPIRE_DATE = p.DEGREE_EXPIRE_DATE,
                                                    .REMARK = p.REMARK
                            }
            Dim result = query.FirstOrDefault
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function
    Public Function DeleteTrainingManage(ByVal objContract As TrainningManageDTO) As Boolean
        Dim objContractData As HU_TRAININGMANAGE
        Try
            ' Xóa  hợp đồng
            objContractData = (From p In Context.HU_TRAININGMANAGE Where objContract.ID = p.ID).SingleOrDefault
            If Not objContractData Is Nothing Then
                Context.HU_TRAININGMANAGE.DeleteObject(objContractData)
                Context.SaveChanges()
                Return True
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region
#Region "TrainingForeign"
    Public Function GetTrainingForeign(ByVal _filter As TrainningForeignDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of TrainningForeignDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            'Dim query = From p In Context.HU_CONTRACT Order By p.HU_EMPLOYEE.EMPLOYEE_CODE
            '            From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
            '            From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
            '            From c In Context.HU_CONTRACT_TYPE.Where(Function(f) p.CONTRACT_TYPE_ID = f.ID)
            '            From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
            '            From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
            '            From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
            '                                                           f.USERNAME = log.Username.ToUpper)

            Dim query = From p In Context.HU_TRAININGFOREIGN
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                         From org In Context.HU_ORGANIZATION_V.Where(Function(f) f.ID = o.ID).DefaultIfEmpty
                          From o1 In Context.HU_ORGANIZATION.Where(Function(f) f.ID = org.ID3).DefaultIfEmpty
                       From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                       From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TRAINNING_ID).DefaultIfEmpty
                          From ot1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.VISA_ID).DefaultIfEmpty
                      From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                 f.USERNAME = log.Username.ToUpper)
            ' lọc điều kiện
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> terID) Or (p.e.WORK_STATUS = terID And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            ' select thuộc tính
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.FROM_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE <= _filter.TO_DATE)
            End If
            If _filter.START_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE = _filter.START_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.EXPIRE_DATE = _filter.EXPIRE_DATE)
            End If
            If _filter.SIGN_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.SIGN_DATE = _filter.SIGN_DATE)
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.t.NAME_VN = _filter.TITLE_NAME)
            End If
            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.o.NAME_VN.ToUpper = _filter.ORG_NAME.ToUpper)
            End If
            If _filter.LOCATION IsNot Nothing Then
                query = query.Where(Function(p) p.p.LOCATION = _filter.LOCATION)
            End If
            If _filter.CONTENT IsNot Nothing Then
                query = query.Where(Function(p) p.p.CONTENT = _filter.CONTENT)
            End If

            Dim trainingforeign = query.Select(Function(p) New TrainningForeignDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .ORG_ID = p.e.ID,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .TITLE_ID = p.p.TITLE_ID,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .TRAINNING_NAME = p.ot.NAME_VN,
                                            .START_DATE = p.p.START_DATE,
                                            .EXPIRE_DATE = p.p.EXPIRE_DATE,
                                            .SIGN_DATE = p.p.SIGN_DATE,
                                            .DECISION_NO = p.p.DECISION_NO,
                                            .LOCATION = p.p.LOCATION,
                                            .CONTENT = p.p.CONTENT,
            .CREATED_DATE = p.p.CREATED_DATE,
                                            .PLACE_FROM = p.p.PLACE_FROM,
                                            .PLACE_TO = p.p.PLACE_TO,
                                            .VISA_ID = p.p.VISA_ID,
                                            .VISA_NAME = p.ot1.NAME_VN,
                                            .NUMBER_VISA = p.p.NUMBER_VISA,
                                            .DATE_NC_VISA = p.p.DATE_NC_VISA,
                                            .DATE_HH_VISA = p.p.DATE_HH_VISA,
                                            .PLACE_VISA = p.p.PLACE_VISA,
                                            .COST_VISA = p.p.COST_VISA,
                                            .NUMBER_DATE = p.p.NUMBER_DATE,
                                            .COST_KH = p.p.COST_KH,
                                            .COST_WORK = p.p.COST_WORK,
                                            .COST_HOTEL = p.p.COST_HOTEL,
                                            .COST_ANOTHER = p.p.COST_ANOTHER,
                                            .COST_GO = p.p.COST_GO,
                                            .CHK_COSTWORK = p.p.CHK_COSTWORK,
                                            .SUM_COST = p.p.SUM_COST,
                                            .ORG_NAME = If(p.o1.UNIT_RANK_ID = 7762, p.org.NAME_C3, Nothing),
                                            .ORG_NAME2 = p.org.NAME_C2,
                                            .ORG_NAME4 = If(p.o1.UNIT_RANK_ID = 7763, p.org.NAME_C3, Nothing)
                                            })

            trainingforeign = trainingforeign.OrderBy(Sorts)
            Total = trainingforeign.Count
            trainingforeign = trainingforeign.Skip(PageIndex * PageSize).Take(PageSize)
            Return trainingforeign.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function InsertTrainingForeign(ByVal objContract As TrainningForeignDTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_TRAININGFOREIGN
        Try
            objContractData.ID = Utilities.GetNextSequence(Context, Context.HU_TRAININGFOREIGN.EntitySet.Name)
            objContract.ID = objContractData.ID
            objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            objContractData.TITLE_ID = objContract.TITLE_ID
            objContractData.ORG_ID = objContract.ORG_ID
            objContractData.START_DATE = objContract.START_DATE
            objContractData.EXPIRE_DATE = objContract.EXPIRE_DATE
            objContractData.DECISION_NO = objContract.DECISION_NO
            objContractData.SIGN_DATE = objContract.SIGN_DATE
            objContractData.TRAINNING_ID = objContract.TRAINNING_ID
            objContractData.TRAINNING_NAME = objContract.TRAINNING_NAME
            objContractData.LOCATION = objContract.LOCATION
            objContractData.CONTENT = objContract.CONTENT
            objContractData.PLACE_FROM = objContract.PLACE_FROM
            objContractData.PLACE_TO = objContract.PLACE_TO
            objContractData.VISA_ID = objContract.VISA_ID
            objContractData.NUMBER_VISA = objContract.NUMBER_VISA
            objContractData.DATE_NC_VISA = objContract.DATE_NC_VISA
            objContractData.DATE_HH_VISA = objContract.DATE_HH_VISA
            objContractData.PLACE_VISA = objContract.PLACE_VISA
            objContractData.COST_VISA = objContract.COST_VISA
            objContractData.NUMBER_DATE = objContract.NUMBER_DATE
            objContractData.COST_KH = objContract.COST_KH
            objContractData.COST_WORK = objContract.COST_WORK
            objContractData.COST_HOTEL = objContract.COST_HOTEL
            objContractData.COST_ANOTHER = objContract.COST_ANOTHER
            objContractData.COST_GO = objContract.COST_GO
            objContractData.SUM_COST = objContract.SUM_COST
            objContractData.CHK_COSTWORK = objContract.CHK_COSTWORK
            Context.HU_TRAININGFOREIGN.AddObject(objContractData)
            ' Phê duyệt
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
    Public Function ModifyTrainingForeign(ByVal objContract As TrainningForeignDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_TRAININGFOREIGN With {.ID = objContract.ID}
        Try
            objContractData = (From p In Context.HU_TRAININGFOREIGN Where p.ID = objContract.ID).FirstOrDefault

            objContract.ID = objContractData.ID
            objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            objContractData.TITLE_ID = objContract.TITLE_ID
            objContractData.ORG_ID = objContract.ORG_ID
            objContractData.START_DATE = objContract.START_DATE
            objContractData.EXPIRE_DATE = objContract.EXPIRE_DATE
            objContractData.DECISION_NO = objContract.DECISION_NO
            objContractData.SIGN_DATE = objContract.SIGN_DATE
            objContractData.TRAINNING_ID = objContract.TRAINNING_ID
            objContractData.TRAINNING_NAME = objContract.TRAINNING_NAME
            objContractData.LOCATION = objContract.LOCATION
            objContractData.CONTENT = objContract.CONTENT
            objContractData.PLACE_FROM = objContract.PLACE_FROM
            objContractData.PLACE_TO = objContract.PLACE_TO
            objContractData.VISA_ID = objContract.VISA_ID
            objContractData.NUMBER_VISA = objContract.NUMBER_VISA
            objContractData.DATE_NC_VISA = objContract.DATE_NC_VISA
            objContractData.DATE_HH_VISA = objContract.DATE_HH_VISA
            objContractData.PLACE_VISA = objContract.PLACE_VISA
            objContractData.COST_VISA = objContract.COST_VISA
            objContractData.NUMBER_DATE = objContract.NUMBER_DATE
            objContractData.COST_KH = objContract.COST_KH
            objContractData.COST_WORK = objContract.COST_WORK
            objContractData.COST_HOTEL = objContract.COST_HOTEL
            objContractData.COST_ANOTHER = objContract.COST_ANOTHER
            objContractData.COST_GO = objContract.COST_GO
            objContractData.SUM_COST = objContract.SUM_COST
            objContractData.CHK_COSTWORK = objContract.CHK_COSTWORK

            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
    Public Function GetTrainingForeignById(ByVal _filter As TrainningForeignDTO) As TrainningForeignDTO
        Try
            Dim query = From p In Context.HU_TRAININGFOREIGN
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION_V.Where(Function(f) f.ID = o.ID)
                        From o1 In Context.HU_ORGANIZATION.Where(Function(f) f.ID = org.ID3).DefaultIfEmpty
                       From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                       From ot In Context.OT_OTHER_LIST.Where(Function(f) p.TRAINNING_ID = f.ID).DefaultIfEmpty
            Where (p.ID = _filter.ID)
                        Select New TrainningForeignDTO With {.ID = p.ID,
                                                     .START_DATE = p.START_DATE,
                                                     .EXPIRE_DATE = p.EXPIRE_DATE,
                                                     .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                     .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                     .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                     .ORG_ID = e.ID,
                                                     .ORG_DESC = o.DESCRIPTION_PATH,
                                                     .TITLE_NAME = t.NAME_VN,
                                                     .SIGN_DATE = p.SIGN_DATE,
                                                     .LOCATION = p.LOCATION,
                                                      .CONTENT = p.CONTENT,
                                                      .DECISION_NO = p.DECISION_NO,
                                                      .TRAINNING_ID = p.TRAINNING_ID,
                                                      .TRAINNING_NAME = ot.NAME_VN,
                                                     .PLACE_FROM = p.PLACE_FROM,
                                                             .PLACE_TO = p.PLACE_TO,
                                                             .VISA_ID = p.VISA_ID,
                                                             .DATE_NC_VISA = p.DATE_NC_VISA,
                                                             .DATE_HH_VISA = p.DATE_HH_VISA,
                                                             .PLACE_VISA = p.PLACE_VISA,
                                                             .COST_VISA = p.COST_VISA,
                                                             .NUMBER_DATE = p.NUMBER_DATE,
                                                             .NUMBER_VISA = p.NUMBER_VISA,
                                                             .COST_KH = p.COST_KH,
                                                             .COST_WORK = p.COST_WORK,
                                                             .COST_HOTEL = p.COST_HOTEL,
                                                             .COST_ANOTHER = p.COST_ANOTHER,
                                                             .COST_GO = p.COST_GO,
                                                             .CHK_COSTWORK = If(p.CHK_COSTWORK = -1, True, False),
                                                             .SUM_COST = p.SUM_COST,
                                                             .ORG_NAME2 = org.NAME_C2,
                                                             .ORG_NAME4 = org.NAME_C4,
                                                             .ORG_NAME = org.NAME_C3,
                                                             .unit_rank_id = o1.UNIT_RANK_ID
                            }
            Dim result = query.FirstOrDefault
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function
    Public Function DeleteTrainingForeign(ByVal objContract() As TrainningForeignDTO) As Boolean
        Dim objContractData As HU_TRAININGFOREIGN
        Try
            Dim lstTRAININGFOREIGNData As List(Of HU_TRAININGFOREIGN)
            Dim lstIDTRAININGFOREIGN As List(Of Decimal) = (From p In objContract.ToList Select p.ID).ToList
            lstTRAININGFOREIGNData = (From p In Context.HU_TRAININGFOREIGN Where lstIDTRAININGFOREIGN.Contains(p.ID)).ToList
            For index = 0 To lstTRAININGFOREIGNData.Count - 1
                Context.HU_TRAININGFOREIGN.DeleteObject(lstTRAININGFOREIGNData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
#End Region

#Region "Contract"
    'update ngay thanh ly vao hop dong
    Public Function UpdateDateToContract(ByVal id As Decimal, ByVal day As Date, ByVal remark As String) As Boolean
        Dim objContractData As New HU_CONTRACT With {.ID = id}
        Try
            objContractData = (From p In Context.HU_CONTRACT Where p.ID = id).FirstOrDefault
            objContractData.ID = id
            objContractData.LIQUIDATION_DATE = day
            objContractData.REMARK_LIQUIDATION = remark
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'check phê duyệt và đã có đính kèm file hay chưa
    'yêu cầu nếu phê duyệt thì phải có phải đính kèm
    Public Function CheckHasFileFileContract(ByVal id As List(Of Decimal)) As Decimal
        Try
            Dim filecontracts = Context.HU_FILECONTRACT.Where(Function(p) p.STATUS_ID = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID And id.Contains(p.ID)).ToList()
            For Each filecontract As HU_FILECONTRACT In filecontracts
                If filecontract.FILENAME Is Nothing Or filecontract.FILENAME = "" Then
                    Return 1
                End If
            Next
            Return 2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'check phê duyệt và đã có đính kèm file hay chưa
    'yêu cầu nếu phê duyệt thì phải có phải đính kèm
    Public Function CheckHasFileContract(ByVal id As List(Of Decimal)) As Decimal
        Try
            Dim contracts = Context.HU_CONTRACT.Where(Function(p) p.STATUS_ID = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID And id.Contains(p.ID)).ToList()
            For Each contract As HU_CONTRACT In contracts
                If contract.FILENAME Is Nothing Or contract.FILENAME = "" Then
                    Return 1
                End If
            Next
            Return 2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ApproveListContract(ByVal listID As List(Of Decimal), ByVal log As UserLog) As Boolean

        Try
            Dim item As Decimal = 0
            For idx = 0 To listID.Count - 1
                item = listID(idx)
                Dim objContract = (From p In Context.HU_CONTRACT Where item = p.ID).FirstOrDefault
                Dim objContractData As New ContractDTO
                objContractData.ID = item
                objContractData.CONTRACT_NO = objContract.CONTRACT_NO
                objContractData.CONTRACTTYPE_ID = objContract.CONTRACT_TYPE_ID
                objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
                objContractData.START_DATE = objContract.START_DATE
                objContractData.EXPIRE_DATE = objContract.EXPIRE_DATE
                objContractData.REMARK = objContract.REMARK
                objContractData.SIGN_DATE = objContract.SIGN_DATE
                objContractData.SIGN_ID = objContract.SIGN_ID
                objContractData.SIGNER_NAME = objContract.SIGNER_NAME
                objContractData.SIGNER_TITLE = objContract.SIGNER_TITLE
                objContractData.WORKING_ID = objContract.WORKING_ID
                objContract.STATUS_ID = 447
                objContractData.STATUS_ID = 447
                objContractData.MORNING_START = objContract.MORNING_START
                objContractData.MORNING_STOP = objContract.MORNING_STOP
                objContractData.AFTERNOON_START = objContract.AFTERNOON_START
                objContractData.AFTERNOON_STOP = objContract.AFTERNOON_STOP
                objContractData.TITLE_ID = objContract.TITLE_ID
                objContractData.ORG_ID = objContract.ORG_ID
                'call quan ly truc tiep,doi tuong cham cong,dôi tuong lao dong,bac nhan vien
                Dim objEmployee = (From p In Context.HU_EMPLOYEE Where objContractData.EMPLOYEE_ID = p.ID).FirstOrDefault
                objContractData.DIRECT_MANAGER = objEmployee.DIRECT_MANAGER
                objContractData.STAFF_RANK_ID = objEmployee.STAFF_RANK_ID
                objContractData.OBJECT_LABOUR = objEmployee.OBJECT_LABOR
                objContractData.OBJECTTIMEKEEPING = objEmployee.OBJECTTIMEKEEPING
                If objContractData.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                    'objContractData.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID
                    'objContract.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID
                    ApproveContract(objContractData)
                    'ACVUSA-291 k tu dong tao quyet dinh nua
                    'If IsFirstContract(objContractData) Then
                    '    InsertDecision(objContractData)
                    'End If
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetContract(ByVal _filter As ContractDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of ContractDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.HU_CONTRACT Order By p.HU_EMPLOYEE.EMPLOYEE_CODE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From c In Context.HU_CONTRACT_TYPE.Where(Function(f) p.CONTRACT_TYPE_ID = f.ID)
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                          From l In Context.HU_LOCATION.Where(Function(f) f.ID = p.ID_SIGN_CONTRACT).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)


            ' lọc điều kiện
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> terID) Or (p.e.WORK_STATUS = terID And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.CONTRACTTYPE_NAME <> "" Then
                query = query.Where(Function(p) p.c.NAME.ToUpper.Contains(_filter.CONTRACTTYPE_NAME.ToUpper))
            End If
            If _filter.SIGNER_NAME <> "" Then
                query = query.Where(Function(p) p.p.SIGNER_NAME.ToUpper.Contains(_filter.SIGNER_NAME.ToUpper))
            End If
            If _filter.FROM_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE <= _filter.TO_DATE)
            End If
            If _filter.START_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE = _filter.START_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.EXPIRE_DATE = _filter.EXPIRE_DATE)
            End If
            If _filter.SIGN_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.SIGN_DATE = _filter.SIGN_DATE)
            End If
            If _filter.STATUS_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.status.NAME_VN.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If

            If _filter.MORNING_START IsNot Nothing Then
                query = query.Where(Function(p) p.p.MORNING_START = _filter.MORNING_START)
            End If
            If _filter.MORNING_STOP IsNot Nothing Then
                query = query.Where(Function(p) p.p.MORNING_STOP = _filter.MORNING_STOP)
            End If
            If _filter.AFTERNOON_START IsNot Nothing Then
                query = query.Where(Function(p) p.p.AFTERNOON_START = _filter.AFTERNOON_START)
            End If
            If _filter.AFTERNOON_STOP IsNot Nothing Then
                query = query.Where(Function(p) p.p.AFTERNOON_STOP = _filter.AFTERNOON_STOP)
            End If

            ' select thuộc tính
            Dim contract = query.Select(Function(p) New ContractDTO With {
                                            .ID = p.p.ID,
                                            .CONTRACTTYPE_ID = p.p.CONTRACT_TYPE_ID,
                                            .CONTRACTTYPE_NAME = p.c.NAME,
                                            .CONTRACT_NO = p.p.CONTRACT_NO,
                                            .START_DATE = p.p.START_DATE,
                                            .EXPIRE_DATE = p.p.EXPIRE_DATE,
                                            .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .ORG_ID = p.e.ID,
                                            .LIQUIDATION_DATE = p.p.LIQUIDATION_DATE,
                                            .REMARK_LIQUIDATION = p.p.REMARK_LIQUIDATION,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .SIGN_DATE = p.p.SIGN_DATE,
                                            .SIGNER_NAME = p.p.SIGNER_NAME,
                                            .SIGNER_TITLE = p.l.CODE,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .STATUS_ID = p.p.STATUS_ID,
                                            .STATUS_NAME = p.status.NAME_VN,
                                            .STATUS_CODE = p.status.CODE,
                                            .MORNING_STOP = p.p.MORNING_STOP,
                                            .MORNING_START = p.p.MORNING_START,
                                            .AFTERNOON_START = p.p.AFTERNOON_START,
                                            .AFTERNOON_STOP = p.p.AFTERNOON_STOP,
                                            .CONTRACTTYPE_CODE = p.c.CODE})

            contract = contract.OrderBy(Sorts)
            Total = contract.Count
            contract = contract.Skip(PageIndex * PageSize).Take(PageSize)

            Return contract.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function GetContractByID(ByVal _filter As ContractDTO) As ContractDTO
        Try
            Dim query = From p In Context.HU_CONTRACT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) p.EMPLOYEE_ID = f.ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From c In Context.HU_CONTRACT_TYPE.Where(Function(f) p.CONTRACT_TYPE_ID = f.ID)
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From staffrank In Context.HU_STAFF_RANK.Where(Function(f) e.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                        From w In Context.HU_WORKING.Where(Function(f) p.WORKING_ID = f.ID).DefaultIfEmpty
                        From sal_group In Context.PA_SALARY_GROUP.Where(Function(f) w.SAL_GROUP_ID = f.ID).DefaultIfEmpty
                        From sal_level In Context.PA_SALARY_LEVEL.Where(Function(f) w.SAL_LEVEL_ID = f.ID).DefaultIfEmpty
                        From sal_rank In Context.PA_SALARY_RANK.Where(Function(f) w.SAL_RANK_ID = f.ID).DefaultIfEmpty
                        From taxTable In Context.OT_OTHER_LIST.Where(Function(f) f.ID = w.TAX_TABLE_ID).DefaultIfEmpty
                        From sal_type In Context.PA_SALARY_TYPE.Where(Function(f) w.SAL_TYPE_ID = f.ID).DefaultIfEmpty
                        From form_work In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.FORM_WORK).DefaultIfEmpty
                        From lo In Context.HU_LOCATION.Where(Function(f) f.ID = p.ID_SIGN_CONTRACT).DefaultIfEmpty
                        Where p.ID = _filter.ID
                        Select New ContractDTO With {.ID = p.ID,
                                                     .CONTRACTTYPE_ID = c.ID,
                                                     .CONTRACTTYPE_NAME = c.NAME,
                                                     .CONTRACT_NO = p.CONTRACT_NO,
                                                     .START_DATE = p.START_DATE,
                                                     .EXPIRE_DATE = p.EXPIRE_DATE,
                                                     .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                     .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                     .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                     .ORG_ID = e.ID,
                                                     .ORG_NAME = o.NAME_VN,
                                                     .ORG_DESC = o.DESCRIPTION_PATH,
                                                     .TITLE_NAME = t.NAME_VN,
                                                     .SIGN_ID = p.SIGN_ID,
                                                     .SIGN_ID2 = p.SIGN_ID2,
                                                     .SIGNER_NAME2 = p.SIGNER_NAME2,
                                                     .SIGNER_TITLE2 = p.SIGNER_TITLE2,
                                                     .NAME_SIGN_CONTRACT = lo.LOCATION_VN_NAME,
                                                     .ID_SIGN_CONTRACT = p.ID_SIGN_CONTRACT,
                                                     .SIGN_DATE = p.SIGN_DATE,
                                                     .SIGNER_NAME = p.SIGNER_NAME,
                                                     .SIGNER_TITLE = p.SIGNER_TITLE,
                                                     .CREATED_DATE = p.CREATED_DATE,
                                                     .STATUS_NAME = p.OT_STATUS.NAME_VN,
                                                     .WORKING_ID = w.ID,
                                                     .DECISION_NO = w.DECISION_NO,
                                                     .REMARK = p.REMARK,
                                                     .SAL_BASIC = w.SAL_BASIC,
                                                     .PERCENT_SALARY = w.PERCENTSALARY,
                                                     .SAL_GROUP_ID = w.SAL_GROUP_ID,
                                                     .SAL_GROUP_NAME = sal_group.NAME,
                                                     .SAL_LEVEL_ID = w.SAL_LEVEL_ID,
                                                     .SAL_LEVEL_NAME = sal_level.NAME,
                                                     .SAL_RANK_ID = w.SAL_RANK_ID,
                                                     .SAL_RANK_NAME = sal_rank.RANK,
                                                     .STATUS_ID = p.STATUS_ID,
                                                     .STAFF_RANK_ID = e.STAFF_RANK_ID,
                                                     .STAFF_RANK_NAME = staffrank.NAME,
                                                     .WORK_STATUS = e.WORK_STATUS,
                                                     .MORNING_STOP = p.MORNING_STOP,
                                                     .MORNING_START = p.MORNING_START,
                                                     .AFTERNOON_START = p.AFTERNOON_START,
                                                     .AFTERNOON_STOP = p.AFTERNOON_STOP,
                                                     .ATTACH_FILE = p.ATTACH_FILE,
                                                     .FILENAME = p.FILENAME,
                                                     .WORK_TO_DO = p.WORK_TO_DO,
                                                     .NUMBER_AUTHORITY = p.NUMBER_AUTHORITY,
                                                     .AUTHORITY = If(p.AUTHORITY = -1, True, False),
                                                     .MONEY_RISK = p.MONEY_RISK,
                                                     .FORM_WORK = p.FORM_WORK,
                                                     .FORM_WORK_NAME = form_work.NAME_VN
                                                   }

            Dim result = query.FirstOrDefault
            If result IsNot Nothing Then
                If result.WORKING_ID > 0 Then
                    result.Working = (From w In Context.HU_WORKING
                                      From taxTable In Context.OT_OTHER_LIST.Where(Function(f) f.ID = w.TAX_TABLE_ID).DefaultIfEmpty
                                      From sal_type In Context.PA_SALARY_TYPE.Where(Function(f) w.SAL_TYPE_ID = f.ID).DefaultIfEmpty
                                      Where w.ID = result.WORKING_ID
                                      Select New WorkingDTO With
                    {
                                .ALLOWANCE_TOTAL = w.ALLOWANCE_TOTAL,
                                .SAL_INS = w.SAL_INS,
                                .TAX_TABLE_ID = w.TAX_TABLE_ID,
                                .SAL_TOTAL = w.SAL_TOTAL,
                                .SAL_TYPE_ID = w.SAL_TYPE_ID,
                                .SAL_BASIC = w.SAL_BASIC,
                            .ID = w.ID,
                            .TAX_TABLE_Name = taxTable.NAME_VN,
                            .SAL_TYPE_NAME = sal_type.NAME,
                                .PERCENT_SALARY = w.PERCENTSALARY,
                                .OTHERSALARY1 = w.OTHERSALARY1,
                                .OTHERSALARY2 = w.OTHERSALARY2,
                                .OTHERSALARY3 = w.OTHERSALARY3,
                                .DECISION_NO = w.DECISION_NO,
                                .EFFECT_DATE = w.EFFECT_DATE
                                }).FirstOrDefault
                End If
                If result.Working IsNot Nothing Then
                    result.Working.lstAllowance = (From p In Context.HU_WORKING_ALLOW
                                                   From allow In Context.HU_ALLOWANCE_LIST.Where(Function(f) f.ID = p.ALLOWANCE_LIST_ID)
                                                   Where p.HU_WORKING_ID = result.Working.ID
                                                   Select New WorkingAllowanceDTO With {.ALLOWANCE_LIST_ID = p.ALLOWANCE_LIST_ID,
                                                                                 .ALLOWANCE_LIST_NAME = allow.NAME,
                                                                                 .AMOUNT = p.AMOUNT,
                                                                                 .EFFECT_DATE = p.EFFECT_DATE,
                                                                                 .EXPIRE_DATE = p.EXPIRE_DATE,
                                                                                 .IS_INSURRANCE = p.IS_INSURRANCE}).ToList
                    If result.Working.lstAllowance IsNot Nothing AndAlso result.Working.lstAllowance.Count > 0 Then
                        result.Working.ALLOWANCE_TOTAL = result.Working.lstAllowance.Sum(Function(f) f.AMOUNT)
                    End If
                End If

            End If
            result.ListAttachFiles = (From p In Context.HU_ATTACHFILES.Where(Function(f) f.FK_ID = _filter.ID)
                                      Select New AttachFilesDTO With {.ID = p.ID,
                                                                      .FK_ID = p.FK_ID,
                                                                      .FILE_TYPE = p.FILE_TYPE,
                                                                      .FILE_PATH = p.FILE_PATH,
                                                                      .CONTROL_NAME = p.CONTROL_NAME,
                                                                      .ATTACHFILE_NAME = p.ATTACHFILE_NAME}).ToList()


            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function
    'k cho tạo hd mới khi hd cũ còn hiệu lực
    Public Function ValidContract(ByVal empid As Decimal, ByVal rd_date As Date) As Boolean
        Try
            'lay hop dong gan nhat
            Dim check = (From p In Context.HU_CONTRACT
                         Where p.EMPLOYEE_ID = empid
                          Order By p.ID Descending).FirstOrDefault()
            If check Is Nothing Then
                Return True
            End If
            If check.EXPIRE_DATE >= rd_date Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ValidContract1(ByVal empCode As String, ByVal effectdate As Date) As Boolean
        Try
            Dim check = (From p In Context.HU_CONTRACT
                         From e In Context.HU_EMPLOYEE Where e.EMPLOYEE_CODE = empCode
                         Where p.EMPLOYEE_ID = e.ID
                         Select New ContractDTO With {
                             .START_DATE = p.START_DATE}).ToList
            For Each item In check
                If effectdate < item.START_DATE Then
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ValidateContract(ByVal sType As String, ByVal _validate As ContractDTO) As Boolean
        Try
            ' note: đồng bộ phê duyệt
            Select Case sType
                Case "EXIST_EFFECT_DATE"
                    Return (From e In Context.HU_CONTRACT
                            Where e.EMPLOYEE_ID = _validate.EMPLOYEE_ID And
                            e.START_DATE >= _validate.START_DATE And
                            e.ID <> _validate.ID And
                            e.LIQUIDATION_DATE Is Nothing And
                            e.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID).Count = 0
                Case "EXIST_CONTRACT_NO"
                    Return (From p In Context.HU_CONTRACT
                            Where p.CONTRACT_NO.ToUpper = _validate.CONTRACT_NO.ToUpper _
                            And p.ID <> _validate.ID).Count = 0
            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateContract")
            Throw ex
        End Try
    End Function

    Public Function InsertContract(ByVal objContract As ContractDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_CONTRACT
        Try
            objContractData.ID = Utilities.GetNextSequence(Context, Context.HU_CONTRACT.EntitySet.Name)
            objContract.ID = objContractData.ID
            objContractData.CONTRACT_NO = objContract.CONTRACT_NO
            objContractData.CONTRACT_TYPE_ID = objContract.CONTRACTTYPE_ID
            objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            objContractData.START_DATE = objContract.START_DATE
            objContractData.EXPIRE_DATE = objContract.EXPIRE_DATE
            objContractData.REMARK = objContract.REMARK
            objContractData.SIGN_DATE = objContract.SIGN_DATE
            objContractData.SIGN_ID = objContract.SIGN_ID
            objContractData.SIGN_ID2 = objContract.SIGN_ID2
            objContractData.SIGNER_NAME2 = objContract.SIGNER_NAME2
            objContractData.SIGNER_TITLE2 = objContract.SIGNER_TITLE2
            objContractData.SIGNER_NAME = objContract.SIGNER_NAME
            objContractData.SIGNER_TITLE = objContract.SIGNER_TITLE
            objContractData.WORKING_ID = objContract.WORKING_ID
            objContractData.ID_SIGN_CONTRACT = objContract.ID_SIGN_CONTRACT
            objContractData.STATUS_ID = objContract.STATUS_ID
            objContractData.MORNING_START = objContract.MORNING_START
            objContractData.MORNING_STOP = objContract.MORNING_STOP
            objContractData.AFTERNOON_START = objContract.AFTERNOON_START
            objContractData.AFTERNOON_STOP = objContract.AFTERNOON_STOP
            objContractData.TITLE_ID = objContract.TITLE_ID
            objContractData.ORG_ID = objContract.ORG_ID
            objContractData.ATTACH_FILE = objContract.ATTACH_FILE
            objContractData.FILENAME = objContract.FILENAME
            '
            objContractData.FORM_WORK = objContract.FORM_WORK
            objContractData.MORNING_START = objContract.MORNING_START
            objContractData.MORNING_STOP = objContract.MORNING_STOP
            objContractData.AFTERNOON_START = objContract.AFTERNOON_START
            objContractData.AFTERNOON_STOP = objContract.AFTERNOON_STOP
            objContractData.NUMBER_AUTHORITY = objContract.NUMBER_AUTHORITY
            objContractData.AUTHORITY = objContract.AUTHORITY
            objContractData.WORK_TO_DO = objContract.WORK_TO_DO
            objContractData.MONEY_RISK = objContract.MONEY_RISK
            Context.HU_CONTRACT.AddObject(objContractData)
            ' Phê duyệt
            If objContract.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                ApproveContract(objContract)
                '	ACVUSA-291 k tu dong tao quyet dinh nua
                'If IsFirstContract(objContract) Then
                '    InsertDecision(objContract)
                'End If
                Dim conType = Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = objContractData.CONTRACT_TYPE_ID).FirstOrDefault()
                'check hop dong chinh thuc moi insert filecontract
                Dim typeIdHDCT As Decimal = 6359
                If conType IsNot Nothing AndAlso conType.TYPE_ID = typeIdHDCT Then
                    InsertFileContractWhenApprove(objContract, log)
                End If
            End If
            If objContract.ListAttachFiles IsNot Nothing Then
                For Each File As AttachFilesDTO In objContract.ListAttachFiles
                    Dim objFile As New HU_ATTACHFILES
                    objFile.ID = Utilities.GetNextSequence(Context, Context.HU_ATTACHFILES.EntitySet.Name)
                    objFile.FK_ID = objContractData.ID
                    objFile.FILE_PATH = File.FILE_PATH
                    objFile.ATTACHFILE_NAME = File.ATTACHFILE_NAME
                    objFile.CONTROL_NAME = File.CONTROL_NAME
                    objFile.FILE_TYPE = File.FILE_TYPE
                    Context.HU_ATTACHFILES.AddObject(objFile)
                Next
            End If
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
    Private Sub InsertFileContractWhenApprove(ByVal objContract As ContractDTO, ByVal log As UserLog)
        Try
            Dim fileContract As New HU_FILECONTRACT
            If objContract IsNot Nothing Then
                fileContract.ID = Utilities.GetNextSequence(Context, Context.HU_FILECONTRACT.EntitySet.Name)
                Dim outNum = GET_NEXT_APPENDIX_ORDER(0, objContract.ID, objContract.EMPLOYEE_ID)
                Dim order = String.Format("{0}", Format(outNum, "00"))
                fileContract.ID_CONTRACT = objContract.ID
                fileContract.START_DATE = objContract.START_DATE
                fileContract.EXPIRE_DATE = objContract.EXPIRE_DATE
                fileContract.CONTRACT_NO = objContract.CONTRACT_NO
                fileContract.SIGN_DATE = objContract.SIGN_DATE
                fileContract.SIGN_ID = objContract.SIGN_ID
                fileContract.SIGN_ORG_ID = objContract.ORG_ID
                fileContract.SIGNER_NAME = objContract.SIGNER_NAME
                fileContract.SIGNER_TITLE = objContract.SIGNER_TITLE
                fileContract.SIGN_ID2 = objContract.SIGN_ID2
                fileContract.SIGNER_NAME2 = objContract.SIGNER_NAME2
                fileContract.SIGNER_TITLE2 = objContract.SIGNER_TITLE2
                fileContract.EMP_ID = objContract.EMPLOYEE_ID
                fileContract.WORKING_ID = objContract.WORKING_ID
                fileContract.STATUS_ID = 447
                fileContract.APPEND_TYPEID = 11
                fileContract.STT = outNum
                fileContract.APPEND_NUMBER = objContract.CONTRACT_NO + "-" + order
                fileContract.CREATED_DATE = Date.Now
                fileContract.CREATED_BY = log.Username
                fileContract.CREATED_LOG = log.Username
                fileContract.MODIFIED_DATE = Date.Now
                fileContract.MODIFIED_BY = log.Username
                fileContract.MODIFIED_LOG = log.Username
                Context.HU_FILECONTRACT.AddObject(fileContract)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function IsFirstContract(ByVal contractDto As ContractDTO) As Boolean
        'dong nhat loai phe duyet là 447 nen sua lai
        Return Context.HU_CONTRACT.Count(Function(p) p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID And p.EMPLOYEE_ID = contractDto.EMPLOYEE_ID) = 0
    End Function
    Private Sub InsertDecision(ByVal contractDto As ContractDTO)
        Dim recruitDecision = (From otherList In Context.OT_OTHER_LIST
                               From otherListType In Context.OT_OTHER_LIST_TYPE.Where(Function(f) f.CODE = ProfileCommon.OT_DECISION_TYPE.Name And f.ID = otherList.TYPE_ID And otherList.CODE = "QDTD")
                               Select otherList).FirstOrDefault
        'Where otherList.CODE = ProfileCommon.OT_DECISION_TYPE.RecruitDecision
        'If recruitDecision Is Nothing Then
        '    Throw New Exception("Chưa tạo quyết định tuyển dụng, Code='QDTD'")
        'End If

        Dim objDecision = (From wk In Context.HU_WORKING
                           Where wk.DECISION_TYPE_ID = recruitDecision.ID And wk.EMPLOYEE_ID = contractDto.EMPLOYEE_ID
                           Select wk).FirstOrDefault
        Dim updateWorking = (From wk In Context.HU_WORKING
                             Where (wk.STATUS_ID = 447 Or wk.STATUS_ID = 446) And wk.IS_WAGE = -1 And wk.IS_MISSION = 0 And wk.EMPLOYEE_ID = contractDto.EMPLOYEE_ID Order By wk.EFFECT_DATE Descending Select wk).FirstOrDefault
        Dim result = (From p In Context.HU_WORKING_ALLOW
                                                   From allow In Context.HU_ALLOWANCE_LIST.Where(Function(f) f.ID = p.ALLOWANCE_LIST_ID)
                                                   Where p.HU_WORKING_ID = contractDto.WORKING_ID
                                                   Select New WorkingAllowanceDTO With {.AMOUNT = p.AMOUNT,
                                                                                 .IS_INSURRANCE = p.IS_INSURRANCE}).ToList
        result.Sum(Function(f) f.AMOUNT)
        If objDecision IsNot Nothing Then
            Context.HU_WORKING.DeleteObject(objDecision)
        End If
        Context.HU_WORKING.AddObject(New HU_WORKING() With {.ID = Utilities.GetNextSequence(Context, Context.HU_WORKING.EntitySet.Name),
                                     .ORG_ID = contractDto.ORG_ID,
                                     .TITLE_ID = contractDto.TITLE_ID,
                                     .EMPLOYEE_ID = contractDto.EMPLOYEE_ID,
                                     .EFFECT_DATE = contractDto.START_DATE,
                                     .STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID,
                                     .SIGN_ID = contractDto.SIGN_ID,
                                     .SIGN_TITLE = contractDto.SIGNER_TITLE,
                                     .SIGN_DATE = contractDto.SIGN_DATE,
                                      .DIRECT_MANAGER = contractDto.DIRECT_MANAGER,
                                     .STAFF_RANK_ID = contractDto.STAFF_RANK_ID,
                                     .OBJECT_LABOR = contractDto.OBJECT_LABOUR,
                                     .SIGN_NAME = contractDto.SIGNER_NAME,
                                     .ALLOWANCE_TOTAL = result.Sum(Function(f) f.AMOUNT),
                                     .SAL_INS = updateWorking.SAL_INS,
                                    .OBJECT_ATTENDANCE = contractDto.OBJECTTIMEKEEPING,
                                     .TAX_TABLE_ID = updateWorking.TAX_TABLE_ID,
                                     .SAL_TOTAL = updateWorking.SAL_TOTAL,
                                     .SAL_TYPE_ID = updateWorking.SAL_TYPE_ID,
                                      .SAL_BASIC = updateWorking.SAL_BASIC,
                                    .DECISION_TYPE_ID = recruitDecision.ID,
                                    .IS_MISSION = -1,
                                    .IS_3B = 0,
                                    .IS_PROCESS = -1,
                                     .IS_WAGE = 0
                                     })
    End Sub
    'ham kiem tra neu 2 hop dong ct thi k cho insert
    Public Function CheckNotAllow(ByVal empid As Decimal) As Boolean
        Try
            Dim check = (From p In Context.HU_CONTRACT
                       From ct In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = p.CONTRACT_TYPE_ID).DefaultIfEmpty
                       From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = ct.TYPE_ID).DefaultIfEmpty
                       Where p.EMPLOYEE_ID = empid And ot.CODE = "HD").ToList.Count
            If check >= 2 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ModifyContract(ByVal objContract As ContractDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_CONTRACT With {.ID = objContract.ID}
        Try
            objContractData = (From p In Context.HU_CONTRACT Where p.ID = objContract.ID).FirstOrDefault

            objContractData.ID = objContract.ID
            objContractData.CONTRACT_NO = objContract.CONTRACT_NO
            objContractData.CONTRACT_TYPE_ID = objContract.CONTRACTTYPE_ID
            objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            objContractData.EXPIRE_DATE = objContract.EXPIRE_DATE
            objContractData.STATUS_ID = objContract.STATUS_ID
            objContractData.REMARK = objContract.REMARK
            objContractData.SIGN_DATE = objContract.SIGN_DATE
            objContractData.SIGN_ID = objContract.SIGN_ID
            objContractData.SIGN_ID2 = objContract.SIGN_ID2
            objContractData.SIGNER_NAME2 = objContract.SIGNER_NAME2
            objContractData.SIGNER_TITLE2 = objContract.SIGNER_TITLE2
            objContractData.SIGNER_NAME = objContract.SIGNER_NAME
            objContractData.SIGNER_TITLE = objContract.SIGNER_TITLE
            objContractData.START_DATE = objContract.START_DATE
            objContractData.WORKING_ID = objContract.WORKING_ID
            objContractData.ID_SIGN_CONTRACT = objContract.ID_SIGN_CONTRACT
            objContractData.MORNING_START = objContract.MORNING_START
            objContractData.MORNING_STOP = objContract.MORNING_STOP
            objContractData.AFTERNOON_START = objContract.AFTERNOON_START
            objContractData.AFTERNOON_STOP = objContract.AFTERNOON_STOP
            objContractData.TITLE_ID = objContract.TITLE_ID
            objContractData.ORG_ID = objContract.ORG_ID
            objContractData.ATTACH_FILE = objContract.ATTACH_FILE
            objContractData.FILENAME = objContract.FILENAME
            '
            objContractData.FORM_WORK = objContract.FORM_WORK
            objContractData.MORNING_START = objContract.MORNING_START
            objContractData.MORNING_STOP = objContract.MORNING_STOP
            objContractData.AFTERNOON_START = objContract.AFTERNOON_START
            objContractData.AFTERNOON_STOP = objContract.AFTERNOON_STOP
            objContractData.NUMBER_AUTHORITY = objContract.NUMBER_AUTHORITY
            objContractData.AUTHORITY = objContract.AUTHORITY
            objContractData.WORK_TO_DO = objContract.WORK_TO_DO
            objContractData.MONEY_RISK = objContract.MONEY_RISK
            ' Phê duyệt
            If objContract.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                ApproveContract(objContract)
                '	ACVUSA-291 k tu dong tao quyet dinh nua
                'If IsFirstContract(objContract) Then
                '    InsertDecision(objContract)
                'End If
            End If
            'xoa nhung file attach cu
            Dim lstAtt = (From p In Context.HU_ATTACHFILES Where p.FK_ID = objContractData.ID).ToList()
            For index = 0 To lstAtt.Count - 1
                Context.HU_ATTACHFILES.DeleteObject(lstAtt(index))
            Next

            For Each File As AttachFilesDTO In objContract.ListAttachFiles
                Dim objFile As New HU_ATTACHFILES
                objFile.ID = Utilities.GetNextSequence(Context, Context.HU_ATTACHFILES.EntitySet.Name)
                objFile.FK_ID = objContractData.ID
                objFile.FILE_PATH = File.FILE_PATH
                objFile.ATTACHFILE_NAME = File.ATTACHFILE_NAME
                objFile.CONTROL_NAME = File.CONTROL_NAME
                objFile.FILE_TYPE = File.FILE_TYPE
                Context.HU_ATTACHFILES.AddObject(objFile)
            Next
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function

    Public Sub ApproveContract(ByVal objContract As ContractDTO)
        Try
            If Format(objContract.START_DATE, "yyyyMMdd") > Format(Date.Now, "yyyyMMdd") Then
                Exit Sub
            End If
            ' Update hợp đồng mới nhất sang employee
            Dim Employee As HU_EMPLOYEE = (From p In Context.HU_EMPLOYEE Where p.ID = objContract.EMPLOYEE_ID).FirstOrDefault
            Employee.CONTRACT_ID = objContract.ID
            Employee.MODIFIED_DATE = Date.Now
            Dim lstCtrTypeAllow As New List(Of Decimal)
            lstCtrTypeAllow.Add(6)
            lstCtrTypeAllow.Add(7)
            lstCtrTypeAllow.Add(12)
            lstCtrTypeAllow.Add(13)
            lstCtrTypeAllow.Add(14)
            lstCtrTypeAllow.Add(287)
            Dim Type_ID_ct As HU_CONTRACT_TYPE = (From p In Context.HU_CONTRACT_TYPE Where objContract.CONTRACTTYPE_ID = p.ID).FirstOrDefault
            If Employee.JOIN_DATE_STATE Is Nothing And Type_ID_ct.TYPE_ID = 6359 Then
                'Employee.JOIN_DATE_STATE = objContract.START_DATE
                Dim empOther As HU_EMPLOYEE_CV = (From p In Context.HU_EMPLOYEE_CV
                                                  Where p.EMPLOYEE_ID = objContract.EMPLOYEE_ID).FirstOrDefault

                If empOther IsNot Nothing Then
                    empOther.DOAN_PHI = True
                End If
            End If

            ' Update trạng thái Đang làm việc\
            'ACV CAP NHAT BEN QUYET DINH
            'Employee.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.WORKING_ID

            Dim STR As ContractTypeDTO = (From p In Context.HU_CONTRACT_TYPE
                                          From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_ID).DefaultIfEmpty
                                 Where p.ID = objContract.CONTRACTTYPE_ID
                                 Select New ContractTypeDTO With {
                                     .CODE = ot.CODE
                                     }).FirstOrDefault
            'neu hop dong phe duyet la thu viec thi up date vao join_date_state con neu chinh thuc thi 
            'update vào join_date_state và join_date cua hu_employee
            'CAP NHAT THEO QUYET DINH
            If STR.CODE = "HDTV" Then
                Employee.EMP_STATUS = 8
                'If Employee.JOIN_DATE Is Nothing Then
                '    Employee.JOIN_DATE = objContract.START_DATE
                'End If
                'If Employee.SENIORITY_DATE Is Nothing Then
                '    Employee.SENIORITY_DATE = objContract.START_DATE
                'End If
            Else
                Employee.EMP_STATUS = 9
                'If Employee.JOIN_DATE Is Nothing Then
                '    Employee.JOIN_DATE = objContract.START_DATE
                'End If
                'If Employee.SENIORITY_DATE Is Nothing Then
                '    Employee.SENIORITY_DATE = objContract.START_DATE
                'End If
                'If Employee.JOIN_DATE_STATE Is Nothing Then
                '    Employee.JOIN_DATE_STATE = objContract.START_DATE
                'End If
            End If
            ' update  bảo hiểm
            'Dim wokingSalary As WorkingDTO = (From w In Context.HU_WORKING
            '                                  Where w.ID = objContract.WORKING_ID
            '                                  Select New WorkingDTO With {
            '                                      .SAL_BASIC = w.SAL_BASIC,
            '                                      .EMPLOYEE_ID = w.EMPLOYEE_ID}).FirstOrDefault
            'If wokingSalary IsNot Nothing AndAlso wokingSalary.SAL_BASIC <> 0 Then
            '    Dim ContactType As ContractTypeDTO = (From c In Context.HU_CONTRACT_TYPE
            '                                          Where c.ID = objContract.CONTRACTTYPE_ID And (c.BHTN <> 0 Or c.BHXH <> 0 Or c.BHYT <> 0)
            '                                          Select New ContractTypeDTO With {
            '                                              .BHTN = c.BHTN,
            '                                              .BHXH = c.BHXH,
            '                                              .BHYT = c.BHYT}).FirstOrDefault
            '    If ContactType IsNot Nothing Then
            '        Dim Ins_Arising As INS_ARISING = (From p In Context.INS_ARISING
            '                                          Where p.EMPLOYEE_ID = objContract.EMPLOYEE_ID).FirstOrDefault

            '        If Ins_Arising Is Nothing Then
            '            Ins_Arising = New INS_ARISING
            '            Ins_Arising.ID = Utilities.GetNextSequence(Context, Context.INS_ARISING.EntitySet.Name)
            '            Ins_Arising.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            '            Ins_Arising.EFFECTDATE = objContract.START_DATE
            '            Ins_Arising.DATE_CHANGE = objContract.START_DATE
            '            Ins_Arising.STATUS = 0
            '            Context.INS_ARISING.AddObject(Ins_Arising)
            '        End If
            '        Ins_Arising.ISBHTN = Utilities.Obj2Decima(ContactType.BHTN)
            '        Ins_Arising.ISBHXH = Utilities.Obj2Decima(ContactType.BHXH)
            '        Ins_Arising.ISBHYT = Utilities.Obj2Decima(ContactType.BHYT)
            '    End If
            'End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Sub


    Public Function DeleteContract(ByVal objContract As ContractDTO) As Boolean
        Dim objContractData As HU_CONTRACT
        Try
            ' Xóa  hợp đồng
            objContractData = (From p In Context.HU_CONTRACT Where objContract.ID = p.ID).SingleOrDefault
            If Not objContractData Is Nothing Then
                Context.HU_CONTRACT.DeleteObject(objContractData)
                Context.SaveChanges()
                Return True
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function


    Public Function CreateContractNo(ByVal objContract As ContractDTO) As String
        Try
            Dim employeeCode As String = objContract.EMPLOYEE_CODE.Trim.ToUpper
            If employeeCode.Length < 2 Then
                Return String.Empty
            End If
            Dim str As String
            'Dim nameTypeContract As ContractTypeDTO = (From p In Context.HU_CONTRACT_TYPE
            '                                           From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_ID)
            '                                           Where p.ID = objContract.CONTRACTTYPE_ID
            '                                Select New ContractTypeDTO With {
            '                                    .ID = p.ID,
            '                                    .CODE = ot.CODE}).FirstOrDefault()
            'Dim codeLocation As LocationDTO = (From p In Context.HU_LOCATION Where p.ID = objContract.ID_SIGN_CONTRACT
            '                                     Select New LocationDTO With {
            '                                         .ID = p.ID,
            '                                         .CODE = p.CODE}).FirstOrDefault()
            'If nameTypeContract.CODE = "HDTV" Then
            '    str = employeeCode.ToString + "/".ToString() + Year(objContract.START_DATE).ToString() + "/" + "HDTV".ToString() + If(codeLocation.CODE IsNot Nothing, "/".ToString(), Nothing) + codeLocation.CODE
            'Else
            '    str = employeeCode.ToString + "/".ToString() + Year(objContract.START_DATE).ToString() + "/" + "HDLD".ToString() + If(codeLocation.CODE IsNot Nothing, "/".ToString(), Nothing) + codeLocation.CODE
            'End If
            Dim codeContract = (From p In Context.HU_ORGANIZATION
                             Where p.ID = objContract.ORG_ID
                             Select p.CONTRACT_CODE).FirstOrDefault()
            Dim query = (From ct In Context.HU_CONTRACT
                       Where ct.EMPLOYEE_ID = objContract.EMPLOYEE_ID And ct.CONTRACT_TYPE_ID = objContract.CONTRACTTYPE_ID)
            Dim no = query.Count
            str = employeeCode.ToString + "/" + Year(objContract.START_DATE).ToString() + "/" + codeContract + If(no = 0, Nothing, no.ToString())
            Return String.Format("{0}", str)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function CheckContractNo(ByVal objContract As ContractDTO) As Boolean
        Try
            Dim query = (From p In Context.HU_CONTRACT
                         Where p.CONTRACT_NO = objContract.CONTRACT_NO And
                         p.ID <> objContract.ID).FirstOrDefault
            Return (query Is Nothing)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GetContractEmployeeByID(ByVal gID As Decimal) As EmployeeDTO
        Dim obj As EmployeeDTO
        Try
            obj = (From p In Context.HU_EMPLOYEE
                   From staffrank In Context.HU_STAFF_RANK.Where(Function(f) p.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                   From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID)
                   From org In Context.HU_ORGANIZATION_V.Where(Function(f) f.ID = o.ID)
                   From o1 In Context.HU_ORGANIZATION.Where(Function(f) f.ID = org.ID3).DefaultIfEmpty
                   From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID)
                   From working In Context.HU_WORKING.Where(Function(f) f.ID = p.LAST_WORKING_ID).DefaultIfEmpty
                   Where p.ID = gID
                   Select New EmployeeDTO With {
                       .ID = p.ID,
                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                       .FULLNAME_VN = p.FULLNAME_VN,
                       .ORG_ID = p.ORG_ID,
                       .ORG_CODE = o.CODE,
                       .TITLE_ID = t.ID,
                       .TITLE_NAME_VN = t.NAME_VN,
                       .JOIN_DATE = p.JOIN_DATE,
                       .DIRECT_MANAGER = p.DIRECT_MANAGER,
                       .OBJECTTIMEKEEPING = p.OBJECTTIMEKEEPING,
                       .STAFF_RANK_ID = p.STAFF_RANK_ID,
                       .OBJECT_LABOR = p.OBJECT_LABOR,
                       .STAFF_RANK_NAME = staffrank.NAME,
                       .WORK_STATUS = p.WORK_STATUS,
                       .TER_EFFECT_DATE = p.TER_EFFECT_DATE,
                       .LAST_WORKING_ID = p.LAST_WORKING_ID,
                       .SAL_BASIC = working.SAL_BASIC,
                       .ORG_NAME2 = org.NAME_C2,
                       .ORG_NAME = org.NAME_C3,
                       .ORG_NAME4 = org.NAME_C4,
                       .unit_rank_id = o1.UNIT_RANK_ID,
                       .COST_SUPPORT = working.COST_SUPPORT}).FirstOrDefault

            Dim ctract = (From p In Context.HU_CONTRACT
                          Where p.EMPLOYEE_ID = gID And p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID
                          Order By p.START_DATE Descending).FirstOrDefault
            If ctract IsNot Nothing Then
                obj.CONTRACT_NO = ctract.CONTRACT_NO
                obj.CONTRACT_EFFECT_DATE = ctract.START_DATE
                obj.CONTRACT_EXPIRE_DATE = ctract.EXPIRE_DATE
            End If
            Return obj

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function UnApproveContract(ByVal objContract As ContractDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_CONTRACT With {.ID = objContract.ID}
        Try
            objContractData = (From p In Context.HU_CONTRACT Where p.ID = objContract.ID).FirstOrDefault
            objContractData.STATUS_ID = objContract.STATUS_ID
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GetCheckContractTypeID(ByVal listID As String) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_BUSINESS.GET_CHECK_TYPE_CONTRACT",
                                           New With {.P_LIST_ID = listID,
                                                     .PV_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function checkFromDate(ByVal objContract As ContractDTO) As Boolean
        Try
            Dim query = (From p In Context.HU_CONTRACT
                         Where p.EMPLOYEE_ID = objContract.EMPLOYEE_ID And p.EXPIRE_DATE <= objContract.START_DATE).FirstOrDefault
            Return query Is Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "Manage annual leave plans (ALP)"
    Public Function GetListALPByEmpID(ByVal _filter As TrainningManageDTO, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningManageDTO)

        Try

            'Using cls As New DataAccess.QueryData
            '    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
            '                     New With {.P_USERNAME = log.Username,
            '                               .P_ORGID = _param.ORG_ID,
            '                               .P_ISDISSOLVE = _param.IS_DISSOLVE})
            'End Using
            'Dim query = From p In Context.HU_CONTRACT Order By p.HU_EMPLOYEE.EMPLOYEE_CODE
            '            From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
            '            From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
            '            From c In Context.HU_CONTRACT_TYPE.Where(Function(f) p.CONTRACT_TYPE_ID = f.ID)
            '            From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
            '            From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
            '            From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
            '                                                           f.USERNAME = log.Username.ToUpper)

            Dim query = From p In Context.HU_ANNUALLEAVE_PLANS.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                       From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
            ' lọc điều kiện
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> terID) Or (p.e.WORK_STATUS = terID And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            ' select thuộc tính
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.FROM_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE <= _filter.TO_DATE)
            End If
            If _filter.START_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE = _filter.START_DATE)
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.t.NAME_VN = _filter.TITLE_NAME)
            End If
            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.o.NAME_VN = _filter.ORG_NAME)
            End If

            Dim trainingforeign = query.Select(Function(p) New TrainningManageDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .ORG_ID = p.p.ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .TITLE_ID = p.p.TITLE_ID,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .START_DATE = p.p.START_DATE,
                                            .EXPIRE_DATE = p.p.END_DATE,
                                            .REMARK = p.p.REMARK,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .WORK_STATUS = p.p.YEAR,
                                            .COST = p.p.DAY_NUMBER
                                            })

            trainingforeign = trainingforeign.OrderBy(Sorts)
            'Total = trainingforeign.Count
            'trainingforeign = trainingforeign.Skip(PageIndex * PageSize).Take(PageSize)
            Return trainingforeign.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetALP(ByVal _filter As TrainningManageDTO, ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of TrainningManageDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.HU_ANNUALLEAVE_PLANS
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                  f.USERNAME = log.Username.ToUpper)
            ' lọc điều kiện
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> terID) Or (p.e.WORK_STATUS = terID And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            ' select thuộc tính
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.FROM_DATE IsNot Nothing And _filter.TO_DATE Is Nothing Then
                query = query.Where(Function(p) p.p.START_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE IsNot Nothing And _filter.FROM_DATE Is Nothing Then
                query = query.Where(Function(p) p.p.START_DATE <= _filter.TO_DATE)
            End If

            If _filter.FROM_DATE IsNot Nothing And _filter.TO_DATE IsNot Nothing Then
                query = query.Where(Function(p) (_filter.FROM_DATE >= p.p.START_DATE And p.p.END_DATE >= _filter.FROM_DATE) Or (_filter.TO_DATE >= p.p.START_DATE And p.p.END_DATE >= _filter.TO_DATE))
            End If


            If _filter.START_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE = _filter.START_DATE)
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.t.NAME_VN = _filter.TITLE_NAME)
            End If
            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.o.NAME_VN = _filter.ORG_NAME)
            End If
            Dim trainingforeign = query.Select(Function(p) New TrainningManageDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .ORG_ID = p.e.ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .TITLE_ID = p.p.TITLE_ID,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .START_DATE = p.p.START_DATE,
                                            .EXPIRE_DATE = p.p.END_DATE,
                                            .REMARK = p.p.REMARK,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .WORK_STATUS = p.p.YEAR,
                                            .COST = p.p.DAY_NUMBER
                                            })

            trainingforeign = trainingforeign.OrderBy(Sorts)
            Total = trainingforeign.Count
            trainingforeign = trainingforeign.Skip(PageIndex * PageSize).Take(PageSize)
            Return trainingforeign.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function InsertALP(ByVal objContract As TrainningManageDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_ANNUALLEAVE_PLANS
        Try
            objContractData.ID = Utilities.GetNextSequence(Context, Context.HU_ANNUALLEAVE_PLANS.EntitySet.Name)
            objContract.ID = objContractData.ID
            objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            objContractData.TITLE_ID = objContract.TITLE_ID
            objContractData.ORG_ID = objContract.ORG_ID
            objContractData.START_DATE = objContract.START_DATE
            objContractData.END_DATE = objContract.EXPIRE_DATE
            objContractData.DAY_NUMBER = objContract.COST
            objContractData.REMARK = objContract.REMARK
            objContractData.YEAR = objContract.WORK_STATUS
            Context.HU_ANNUALLEAVE_PLANS.AddObject(objContractData)
            ' Phê duyệt
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
    Public Function ModifyALP(ByVal objContract As TrainningManageDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_ANNUALLEAVE_PLANS With {.ID = objContract.ID}
        Try
            objContractData = (From p In Context.HU_ANNUALLEAVE_PLANS Where p.ID = objContract.ID).FirstOrDefault
            objContract.ID = objContractData.ID
            objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            objContractData.TITLE_ID = objContract.TITLE_ID
            objContractData.ORG_ID = objContract.ORG_ID
            objContractData.START_DATE = objContract.START_DATE
            objContractData.END_DATE = objContract.EXPIRE_DATE
            objContractData.REMARK = objContract.REMARK
            objContractData.DAY_NUMBER = objContract.COST
            objContractData.YEAR = objContract.WORK_STATUS
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
    Public Function GetALPById(ByVal _filter As TrainningManageDTO) As TrainningManageDTO
        Try
            Dim query = From p In Context.HU_ANNUALLEAVE_PLANS
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                       From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
            Where (p.ID = _filter.ID)
                        Select New TrainningManageDTO With {.ID = p.ID,
                                                     .START_DATE = p.START_DATE,
                                                     .EXPIRE_DATE = p.END_DATE,
                                                     .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                     .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                     .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                     .ORG_ID = e.ID,
                                                     .ORG_NAME = o.NAME_VN,
                                                     .ORG_DESC = o.DESCRIPTION_PATH,
                                                     .TITLE_NAME = t.NAME_VN,
                                                    .COST = p.DAY_NUMBER,
                                                    .REMARK = p.REMARK,
                                                    .WORK_STATUS = p.YEAR
                            }
            Dim result = query.FirstOrDefault
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function
    Public Function DeleteALP(ByVal objContract As TrainningManageDTO) As Boolean
        Dim objContractData As HU_ANNUALLEAVE_PLANS
        Try
            ' Xóa  hợp đồng
            objContractData = (From p In Context.HU_ANNUALLEAVE_PLANS Where objContract.ID = p.ID).SingleOrDefault
            If Not objContractData Is Nothing Then
                Context.HU_ANNUALLEAVE_PLANS.DeleteObject(objContractData)
                Context.SaveChanges()
                Return True
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function CheckEmployee_Exits(ByVal empCode As String) As Integer
        Dim objEmp As HU_EMPLOYEE
        Dim result As Integer
        Try
            objEmp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = empCode.Replace(" ", "")).SingleOrDefault
            If objEmp IsNot Nothing Then
                result = objEmp.ID
            Else
                result = 0
            End If
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function CheckEmployee_Terminate(ByVal empCode As String) As Integer
        Dim objEmp As HU_EMPLOYEE
        Dim result As Integer
        Try
            objEmp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = empCode.Replace(" ", "") And p.WORK_STATUS = 257).SingleOrDefault
            If objEmp IsNot Nothing Then
                result = objEmp.ID
            Else
                result = 0
            End If
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function CheckEmployee_EffectDate_exits(ByVal empCode As String, ByVal effect_date As String) As Integer
        Dim objEmp As HU_EMPLOYEE
        Dim result As Integer
        Try
            objEmp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = empCode.Replace(" ", "") And p.WORK_STATUS = 257).SingleOrDefault
            If objEmp IsNot Nothing Then
                result = objEmp.ID
            Else
                result = 0
            End If
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetEmpIdByCode(ByVal empcode As String) As EmployeeDTO
        Try
            Dim em = (From p In Context.HU_EMPLOYEE
                     Where p.EMPLOYEE_CODE = empcode
                     Select New EmployeeDTO With {
                         .EMPLOYEE_ID = p.ID}).FirstOrDefault
            Return em
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CheckEmployee_Contract_Count(ByVal empCode As String) As Integer
        Dim count As Decimal
        Dim result As Integer
        Try
            count = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = empCode.Replace(" ", "")
                      From c In Context.HU_CONTRACT Where p.ID = c.EMPLOYEE_ID
                      From ct In Context.HU_CONTRACT_TYPE Where c.CONTRACT_TYPE_ID = ct.ID
                      Where ct.TYPE_ID = 6359 And c.CONTRACT_TYPE_ID <> 5
                      Select New ContractDTO With {
                          .EMPLOYEE_ID = p.ID}).Count
            If count >= 2 Then
                result = 0 'k cho add
            Else
                result = 1
            End If
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function EffectDate_Check_Same(ByVal emp_code As String, ByVal effect_date As Date) As Boolean
        Dim empID As Decimal = 0D
        Try
            empID = (From p In Context.HU_EMPLOYEE.Where(Function(f) f.EMPLOYEE_CODE.ToLower = emp_code.ToLower) Select p.ID).FirstOrDefault
            Dim query = (From p In Context.HU_CONTRACT.Where(Function(f) f.EMPLOYEE_ID = empID And f.START_DATE = effect_date))
            Return query.Count = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ImportAnnualLeave(ByVal P_DOCXML As String, ByVal P_USER As String, ByVal P_YEAR As Decimal) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_LIST.IMPORT_ANNUALLEAVE_PLANS",
                                 New With {.P_DOCXML = P_DOCXML, .P_USER = P_USER, .P_YEAR = P_YEAR})
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

#Region "PLHD"
    Public Function GetContractForm(ByVal formID As Decimal) As OtherListDTO
        Try
            Dim query = (From p In Context.OT_OTHER_LIST Where p.ID = formID
                         Select New OtherListDTO With {.ID = p.ID,
                                                       .CODE = p.CODE,
                                                       .NAME_VN = p.NAME_VN}).FirstOrDefault
            Return query
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GET_PROCESS_PLCONTRACT(ByVal P_EMP_CODE As String) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_HU_IPROFILE_EMPLOYEE.GET_PROCESS_PLCONTRACT",
                                           New With {.P_EMPID = P_EMP_CODE,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function EXPORT_PLHD(ByVal _param As ParamDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_HU_IPROFILE_EMPLOYEE.EXPORT_PLHD",
                                           New With {.P_USERNAME = log.Username.ToUpper,
                                                     .P_ORGID = _param.ORG_ID,
                                                     .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR}, False) ' FALSE : no datatable

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function CHECK_EMPLOYEE(ByVal P_EMP_CODE As String) As Integer
        Try
            Dim result As Integer
            If (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = P_EMP_CODE AndAlso p.IS_KIEM_NHIEM Is Nothing).Count > 0 Then
                result = 1
            Else
                result = 0
            End If

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function CHECK_CONTRACT(ByVal P_ID As Decimal) As Integer
        Try
            Dim result As Integer
            If (From p In Context.HU_CONTRACT Where p.ID = P_ID).Count > 0 Then
                result = 1
            Else
                result = 0
            End If

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function CHECK_SALARY(ByVal P_ID As Decimal) As Integer
        Try
            Dim result As Integer
            If (From p In Context.HU_WORKING Where p.ID = P_ID).Count > 0 Then
                result = 1
            Else
                result = 0
            End If

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function CHECK_CONTRACT_EXITS(ByVal P_CONTRACT As Decimal, ByVal P_EMP_CODE As String, ByVal P_DATE As Date) As Integer
        Try
            Dim result As Integer
            If (From p In Context.HU_FILECONTRACT
                From c In Context.HU_CONTRACT.Where(Function(f) f.ID = p.ID_CONTRACT)
                From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = c.EMPLOYEE_ID)
                Where p.ID_CONTRACT = P_CONTRACT And p.START_DATE = P_DATE And e.EMPLOYEE_CODE = P_EMP_CODE).Count > 0 Then
                result = 1
            Else
                result = 0
            End If

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function CHECK_SIGN(ByVal P_EMP_CODE As String) As Integer
        Try
            Dim result As Integer
            If (From p In Context.HU_SIGNER Where p.SIGNER_CODE = P_EMP_CODE).Count > 0 Then
                result = 1
            Else
                result = 0
            End If

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function INPORT_PLHD(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_HU_IPROFILE_EMPLOYEE.INPORT_PLHD",
                                 New With {.P_DOCXML = P_DOCXML,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
            Return False
        End Try
    End Function

    Public Function GET_PROCESS_PLCONTRACT_PORTAL(ByVal P_EMP_ID As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_HU_IPROFILE_EMPLOYEE.GET_PROCESS_PLCONTRACT_PORTAL",
                                           New With {.P_EMPID = P_EMP_ID,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


#End Region

    Public Function INPORT_EMP(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_HU_IPROFILE_EMPLOYEE.INPORT_EMP",
                                 New With {.P_DOCXML = P_DOCXML, .P_USER = P_USER})
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function


    Public Function GetContractImport() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataSet = cls.ExecuteStore("PKG_PROFILE_INTEGRATED.GET_CONTRACT_IMPORT",
                                                         New With {.P_CUR = cls.OUT_CURSOR,
                                                                   .P_CUR1 = cls.OUT_CURSOR,
                                                                   .P_CUR2 = cls.OUT_CURSOR,
                                                                   .P_CUR3 = cls.OUT_CURSOR,
                                                                   .P_CUR4 = cls.OUT_CURSOR,
                                                                   .P_CUR5 = cls.OUT_CURSOR}, False)
                Return dsdata
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
End Class
