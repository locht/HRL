Imports Framework.Data
Imports System.Data.Objects.DataClasses
Imports System.Data.Common
Imports System.Data.Entity
Imports System.Threading
Imports Framework.Data.System.Linq.Dynamic
Imports Framework.Data.SystemConfig
Imports System.Configuration
Imports System.Data.Objects
Imports System.IO
Imports System.Reflection

Partial Class PerformanceRepository

#Region "PE_PERSONAL Quản lý dữ liệu đánh giá cá nhân"

    Public Function GetEmployeeImport(ByVal group_obj_id As Decimal, ByVal period_id As Decimal, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_EMPLOYEE_EXPORT",
                                           New With {.P_USERNAME = log.Username,
                                                     .P_ORG_ID = 46,
                                                     .P_ISDISSOLVE = False,
                                                     .P_PERIOD_ID = period_id,
                                                     .P_OBJECT_ID = group_obj_id,
                                                     .P_CUR = cls.OUT_CURSOR}, False)

                Return dsData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetCriteriaImport(ByVal group_obj_id As Decimal) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_CRITERIA_IMPORT",
                                           New With {.P_OBJECT_ID = group_obj_id,
                                                        .P_CUR = cls.OUT_CURSOR}, False)

                Return dsData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ImportEmployeeAssessment(ByVal dtData As DataTable,
                                             ByVal periodID As Decimal,
                                             ByVal group_obj_ID As Decimal,
                                             ByVal log As UserLog) As Boolean
        Try
            For i = 0 To dtData.Rows.Count - 1
                For j = 9 To dtData.Columns.Count - 2
                    Dim objData As New PE_EMPLOYEE_ASSESSMENT_DTL
                    Dim CriteriaId As Decimal
                    Dim CandidateID As Decimal
                    CriteriaId = Replace(dtData.Columns(j).ColumnName, "ID_", "")
                    CandidateID = dtData.Rows(i)(1)
                    Dim exists = (From r In Context.PE_EMPLOYEE_ASSESSMENT_DTL _
                                 Where r.PE_EMPLOYEE_ASSESSMENT_ID = CandidateID And
                                 r.CRITERIA_ID = CriteriaId And
                                 r.PERIOD_ID = periodID And
                                 r.GROUP_OBJ_ID = group_obj_ID).Any
                    If exists Then
                        Dim obj = (From r In Context.PE_EMPLOYEE_ASSESSMENT_DTL _
                                 Where r.PE_EMPLOYEE_ASSESSMENT_ID = CandidateID And
                                 r.CRITERIA_ID = CriteriaId And
                                 r.PERIOD_ID = periodID And
                                 r.GROUP_OBJ_ID = group_obj_ID).FirstOrDefault
                        obj.POINT = If(dtData(i)(j) = "", Nothing, dtData(i)(j))
                        obj.CLASSIFICATION = dtData(i)("CLASSIFICATION")
                        Context.SaveChanges(log)
                    Else
                        objData.ID = Utilities.GetNextSequence(Context, Context.PE_EMPLOYEE_ASSESSMENT_DTL.EntitySet.Name)
                        objData.PE_EMPLOYEE_ASSESSMENT_ID = CandidateID
                        objData.CRITERIA_ID = CriteriaId
                        objData.POINT = If(dtData(i)(j) = "", Nothing, dtData(i)(j))
                        objData.PERIOD_ID = periodID
                        objData.GROUP_OBJ_ID = group_obj_ID
                        objData.CLASSIFICATION = dtData(i)("CLASSIFICATION")
                        Context.PE_EMPLOYEE_ASSESSMENT_DTL.AddObject(objData)
                        Context.SaveChanges(log)
                    End If
                Next
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Performance")
            Return False
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeImportAssessment(ByVal _param As ParamDTO,
                                                ByVal obj As PE_EMPLOYEE_ASSESSMENT_DTLDTO,
                                                ByVal P_PAGE_INDEX As Decimal,
                                                ByVal P_PAGE_SIZE As Decimal,
                                                ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_EMPLOYEE_ASS_IMPORT",
                                           New With {.P_USERNAME = log.Username,
                                                     .P_ORG_ID = _param.ORG_ID,
                                                     .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                     .P_PERIOD_ID = obj.PERIOD_ID,
                                                     .P_OBJECT_ID = obj.GROUP_OBJ_ID,
                                                     .P_PAGE_INDEX = P_PAGE_INDEX,
                                                     .P_PAGE_SIZE = P_PAGE_SIZE,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CURCOUNR = cls.OUT_CURSOR}, False)

                Return dsData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "PE_ORGANIZATION Đánh giá đơn vị"
    Public Function GetPe_Organization(ByVal _filter As PE_ORGANIZATIONDTO,
                            ByVal _param As ParamDTO,
                            Optional ByVal PageIndex As Integer = 0,
                            Optional ByVal PageSize As Integer = Integer.MaxValue,
                            Optional ByRef Total As Integer = 0,
                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                            Optional ByVal log As UserLog = Nothing) As List(Of PE_ORGANIZATIONDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.PE_ORGANIZATION
                        From d In Context.PE_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                        From o In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Select New PE_ORGANIZATIONDTO With {
                                       .ID = p.ID,
                                       .PERIOD_ID = p.PERIOD_ID,
                                       .PERIOD_NAME = d.NAME,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = o.NAME_VN,
                                       .ORG_DESC = o.DESCRIPTION_PATH,
                                       .REMARK = p.REMARK,
                                       .RESULT = p.RESULT,
                                       .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If _filter.PERIOD_NAME <> "" Then
                lst = lst.Where(Function(p) p.PERIOD_NAME.ToUpper.Contains(_filter.PERIOD_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.RESULT <> "" Then
                lst = lst.Where(Function(p) p.RESULT.ToUpper.Contains(_filter.RESULT.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPe_Organization")
            Throw ex
        End Try

    End Function

    Public Function GetPe_OrganizationByID(ByVal _filter As PE_ORGANIZATIONDTO) As PE_ORGANIZATIONDTO

        Try

            Dim query = From p In Context.PE_ORGANIZATION
                      From d In Context.PE_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                      From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                      Where p.ID = _filter.ID
                      Select New PE_ORGANIZATIONDTO With {
                                      .ID = p.ID,
                                       .PERIOD_ID = p.PERIOD_ID,
                                       .PERIOD_NAME = d.NAME,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = o.NAME_VN,
                                       .REMARK = p.REMARK,
                                       .RESULT = p.RESULT,
                                       .CREATED_DATE = p.CREATED_DATE}

            Dim obj = query.FirstOrDefault
            Return obj
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPe_OrganizationByID")
            Throw ex
        End Try


    End Function

    Public Function InsertPe_Organization(ByVal objData As PE_ORGANIZATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPlanRegData As New PE_ORGANIZATION
        Try
            objPlanRegData.ID = Utilities.GetNextSequence(Context, Context.PE_ORGANIZATION.EntitySet.Name)
            objPlanRegData.PERIOD_ID = objData.PERIOD_ID
            objPlanRegData.ORG_ID = objData.ORG_ID
            objPlanRegData.RESULT = objData.RESULT
            objPlanRegData.REMARK = objData.REMARK
            Context.PE_ORGANIZATION.AddObject(objPlanRegData)
            Context.SaveChanges(log)
            gID = objPlanRegData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertRegisterNo")
            Throw ex
        End Try
    End Function

    Public Function ModifyPe_Organization(ByVal objData As PE_ORGANIZATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPlanRegData As New PE_ORGANIZATION With {.ID = objData.ID}
        Try
            objPlanRegData = (From p In Context.PE_ORGANIZATION Where p.ID = objData.ID).SingleOrDefault
            objPlanRegData.PERIOD_ID = objData.PERIOD_ID
            objPlanRegData.ORG_ID = objData.ORG_ID
            objPlanRegData.RESULT = objData.RESULT
            objPlanRegData.REMARK = objData.REMARK
            Context.SaveChanges(log)
            gID = objPlanRegData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyRegisterNo")
            Throw ex
        End Try

    End Function

    Public Function DeletePe_Organization(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstRegData As List(Of PE_ORGANIZATION)
        Try
            lstRegData = (From p In Context.PE_ORGANIZATION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstRegData.Count - 1
                Context.PE_ORGANIZATION.DeleteObject(lstRegData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeletePlanReg")
            Throw ex
        End Try

    End Function
#End Region

#Region "Assessment"

    Public Function GetAssessment(ByVal _filter As AssessmentDTO, ByVal PageIndex As Integer,
                                  ByVal PageSize As Integer,
                                  ByRef Total As Integer,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentDTO)

        Try
            Dim query = From p In Context.PE_CRITERIA_OBJECT_GROUP
                     From b In Context.PE_EMPLOYEE_ASSESSMENT.Where(Function(d) d.OBJECT_GROUP_ID = p.OBJECT_GROUP_ID)
                      From obj In Context.PE_CRITERIA.Where(Function(f) f.ID = p.CRITERIA_ID)
                      From a In Context.PE_ASSESSMENT.Where(Function(s) s.PE_EMPLOYEE_ASSESSMENT_ID = b.ID And s.PE_PERIO_ID = b.PERIOD_ID _
                                                                And s.PE_OBJECT_ID = p.OBJECT_GROUP_ID And s.PE_CRITERIA_ID = p.CRITERIA_ID).DefaultIfEmpty
                      From o In Context.OT_OTHER_LIST.Where(Function(c) c.ID = a.STATUS_EMP_ID).DefaultIfEmpty
                      From stt In Context.OT_OTHER_LIST.Where(Function(f) f.ID = b.PE_STATUS).DefaultIfEmpty
            Where (b.OBJECT_GROUP_ID = _filter.PE_OBJECT_ID And b.EMPLOYEE_ID = _filter.EMPLOYEE_ID _
                  And b.PERIOD_ID = _filter.PE_PERIO_ID)
                    Select New AssessmentDTO With {
                            .ID = a.ID,
                            .PE_EMPLOYEE_ASSESSMENT_ID = a.PE_EMPLOYEE_ASSESSMENT_ID,
                            .EMPLOYEE_ID = a.EMPLOYEE_ID,
                            .PE_PERIO_ID = a.PE_PERIO_ID,
                            .PE_OBJECT_ID = a.PE_OBJECT_ID,
                            .PE_CRITERIA_ID = obj.ID,
                            .RESULT = a.RESULT,
                            .STATUS_EMP_ID = a.STATUS_EMP_ID,
                            .DIRECT_ID = a.DIRECT_ID,
                            .UPDATE_DATE = a.UPDATE_DATE,
                            .REMARK = a.REMARK,
                            .RESULT_DIRECT = a.RESULT_DIRECT,
                            .ASS_DATE = a.ASS_DATE,
                            .REMARK_DIRECT = a.REMARK_DIRECT,
                            .RESULT_CONVERT = a.RESULT_CONVERT,
                            .ACTFLG = If(a.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                            .CREATED_DATE = a.CREATED_DATE,
                            .PE_CRITERIA_CODE = obj.CODE,
                            .PE_CRITERIA_NAME = obj.NAME,
                            .EXPENSE = p.EXPENSE,
                            .AMONG = p.AMONG,
                            .FROM_DATE = p.FROM_DATE,
                            .TO_DATE = p.TO_DATE,
                            .STATUS_EMP_NAME = o.NAME_VN,
                            .PE_STATUS_NAME = stt.NAME_VN,
                            .LINK_POPUP = a.ID,
                            .RESULT_CONVERT_QL = a.RESULT_CONVERT_QL}
            Dim lst = query
            If _filter.PE_CRITERIA_CODE <> "" Then
                lst = lst.Where(Function(p) p.PE_CRITERIA_CODE.ToUpper.Contains(_filter.PE_CRITERIA_CODE.ToUpper))
            End If
            If _filter.PE_CRITERIA_NAME <> "" Then
                lst = lst.Where(Function(p) p.PE_CRITERIA_NAME.ToUpper.Contains(_filter.PE_CRITERIA_NAME.ToUpper))
            End If
            If _filter.EXPENSE <> Nothing Then
                lst = lst.Where(Function(p) p.EXPENSE.ToString().Contains(_filter.EXPENSE.ToString()))
            End If
            If _filter.AMONG <> Nothing Then
                lst = lst.Where(Function(p) p.AMONG.ToString().Contains(_filter.AMONG.ToString()))
            End If
            If _filter.FROM_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.FROM_DATE.ToString().Contains(_filter.FROM_DATE.ToString()))
            End If
            If _filter.TO_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.TO_DATE.ToString().Contains(_filter.TO_DATE.ToString()))
            End If
            If _filter.RESULT <> Nothing Then
                lst = lst.Where(Function(p) p.RESULT.ToString().Contains(_filter.RESULT.ToString()))
            End If
            If _filter.UPDATE_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.UPDATE_DATE.ToString().Contains(_filter.UPDATE_DATE.ToString()))
            End If
            If _filter.RESULT_DIRECT <> Nothing Then
                lst = lst.Where(Function(p) p.RESULT_DIRECT.ToString().Contains(_filter.RESULT_DIRECT.ToString()))
            End If
            If _filter.RESULT_CONVERT <> Nothing Then
                lst = lst.Where(Function(p) p.RESULT_CONVERT.ToString().Contains(_filter.RESULT_CONVERT.ToString()))
            End If
            If _filter.RESULT_CONVERT_QL <> Nothing Then
                lst = lst.Where(Function(p) p.RESULT_CONVERT.ToString().Contains(_filter.RESULT_CONVERT_QL.ToString()))
            End If
            If _filter.ASS_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.ASS_DATE.ToString().Contains(_filter.ASS_DATE.ToString()))
            End If
            If _filter.STATUS_EMP_NAME <> Nothing Then
                lst = lst.Where(Function(p) p.STATUS_EMP_NAME.ToString().Contains(_filter.STATUS_EMP_NAME.ToString()))
            End If
            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function ModifyAssessment(ByVal objAssessment As AssessmentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objAssessmentData As New PE_ASSESSMENT
        Try
            If objAssessment.ID <= 0 Then
                objAssessmentData.ID = Utilities.GetNextSequence(Context, Context.PE_ASSESSMENT.EntitySet.Name)
                objAssessmentData.PE_EMPLOYEE_ASSESSMENT_ID = objAssessment.PE_EMPLOYEE_ASSESSMENT_ID
                objAssessmentData.EMPLOYEE_ID = objAssessment.EMPLOYEE_ID
                objAssessmentData.PE_PERIO_ID = objAssessment.PE_PERIO_ID
                objAssessmentData.PE_OBJECT_ID = objAssessment.PE_OBJECT_ID
                objAssessmentData.PE_CRITERIA_ID = objAssessment.PE_CRITERIA_ID
                objAssessmentData.RESULT = objAssessment.RESULT
                objAssessmentData.STATUS_EMP_ID = objAssessment.STATUS_EMP_ID
                If objAssessment.DIRECT_ID <> objAssessment.EMPLOYEE_ID Then
                    objAssessmentData.DIRECT_ID = objAssessment.DIRECT_ID
                End If
                objAssessmentData.UPDATE_DATE = objAssessment.UPDATE_DATE
                objAssessmentData.REMARK = objAssessment.REMARK
                objAssessmentData.RESULT_DIRECT = objAssessment.RESULT_DIRECT
                objAssessmentData.ASS_DATE = objAssessment.ASS_DATE
                objAssessmentData.REMARK_DIRECT = objAssessment.REMARK_DIRECT
                objAssessmentData.RESULT_CONVERT = objAssessment.RESULT_CONVERT
                objAssessmentData.RESULT_CONVERT_QL = objAssessment.RESULT_CONVERT_QL
                Context.PE_ASSESSMENT.AddObject(objAssessmentData)
                Context.SaveChanges(log)
                gID = objAssessmentData.ID
            Else
                Dim objAssessmentUpdate As New PE_ASSESSMENT With {.ID = objAssessment.ID}
                objAssessmentUpdate = (From p In Context.PE_ASSESSMENT Where p.ID = objAssessment.ID).FirstOrDefault
                objAssessmentUpdate.RESULT = objAssessment.RESULT
                objAssessmentUpdate.STATUS_EMP_ID = objAssessment.STATUS_EMP_ID
                If objAssessment.DIRECT_ID <> objAssessment.EMPLOYEE_ID Then
                    objAssessmentUpdate.DIRECT_ID = objAssessment.DIRECT_ID
                End If
                objAssessmentUpdate.UPDATE_DATE = objAssessment.UPDATE_DATE
                objAssessmentUpdate.REMARK = objAssessment.REMARK
                objAssessmentUpdate.RESULT_DIRECT = objAssessment.RESULT_DIRECT
                objAssessmentUpdate.ASS_DATE = objAssessment.ASS_DATE
                objAssessmentUpdate.REMARK_DIRECT = objAssessment.REMARK_DIRECT
                objAssessmentUpdate.RESULT_CONVERT = objAssessment.RESULT_CONVERT
                objAssessmentUpdate.RESULT_CONVERT_QL = objAssessment.RESULT_CONVERT_QL
                Context.SaveChanges(log)
                gID = objAssessmentUpdate.ID
            End If
            Dim objUpdate As New PE_EMPLOYEE_ASSESSMENT With {.OBJECT_GROUP_ID = objAssessment.PE_OBJECT_ID _
                                                                      And .PERIOD_ID = objAssessment.PE_PERIO_ID _
                                                                      And .EMPLOYEE_ID = objAssessment.EMPLOYEE_ID}
            objUpdate = (From p In Context.PE_EMPLOYEE_ASSESSMENT
                                   Where p.OBJECT_GROUP_ID = objAssessment.PE_OBJECT_ID _
                                   And p.PERIOD_ID = objAssessment.PE_PERIO_ID _
                                   And p.EMPLOYEE_ID = objAssessment.EMPLOYEE_ID).FirstOrDefault
            objUpdate.PE_STATUS = objAssessment.PE_STATUS
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function UpdateStatusEmployeeAssessment(ByVal obj As AssessmentDTO,
                                                   ByVal log As UserLog) As Boolean
        Try

            Dim objUpdate As New PE_EMPLOYEE_ASSESSMENT With {.OBJECT_GROUP_ID = obj.PE_OBJECT_ID _
                                                                       And .PERIOD_ID = obj.PE_PERIO_ID _
                                                                       And .EMPLOYEE_ID = obj.EMPLOYEE_ID}
            objUpdate = (From p In Context.PE_EMPLOYEE_ASSESSMENT
                                   Where p.OBJECT_GROUP_ID = obj.PE_OBJECT_ID _
                                   And p.PERIOD_ID = obj.PE_PERIO_ID _
                                   And p.EMPLOYEE_ID = obj.EMPLOYEE_ID).FirstOrDefault
            objUpdate.PE_STATUS = obj.PE_STATUS
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function
    Public Function GetTotalPointRating(ByVal obj As AssessmentDTO) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_PE_ASSESS",
                                           New With {.P_PE_EMPLOYEE_ASSESSMENT_ID = obj.PE_EMPLOYEE_ASSESSMENT_ID,
                                                     .P_EMPLOYEE_ID = obj.EMPLOYEE_ID,
                                                     .P_PE_PERIO_ID = obj.PE_PERIO_ID,
                                                     .P_PE_OBJECT_ID = obj.PE_OBJECT_ID,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function
#End Region

#Region "Common"
    Public Function GetListEmployeePortal(ByVal _filter As EmployeeDTO) As List(Of EmployeeDTO)
        Try
            Dim wstt = PerformanceCommon.OT_WORK_STATUS.TERMINATE_ID

            Dim query = From p In Context.HU_EMPLOYEE Where p.ID = _filter.DIRECT_MANAGER
                        Order By p.EMPLOYEE_CODE

            Dim lst = query.Select(Function(p) New EmployeeDTO With {
                                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                       .ID = p.ID,
                                       .FULLNAME_VN = p.FULLNAME_VN,
                                       .TER_EFFECT_DATE = p.TER_EFFECT_DATE,
                                       .DIRECT_MANAGER = p.DIRECT_MANAGER,
                                       .WORK_STATUS = p.WORK_STATUS})

            'Dim dateNow = Date.Now.Date
            'Dim terID = PerformanceCommon.OT_WORK_STATUS.TERMINATE_ID
            'If Not _filter.IS_TER Then
            '    lst = lst.Where(Function(p) Not p.WORK_STATUS.HasValue Or _
            '                        (p.WORK_STATUS.HasValue And _
            '                         ((p.WORK_STATUS <> terID) Or (p.WORK_STATUS = terID And p.TER_EFFECT_DATE > dateNow))))

            'End If

            'If _filter.DIRECT_MANAGER IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.DIRECT_MANAGER = _filter.DIRECT_MANAGER)
            'End If
            'If _filter.ID <> Nothing Then
            '    lst = lst.Where(Function(p) p.ID = _filter.ID)
            'End If
            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function
#End Region

#Region "PORTAL QL PKI cua nhan vien"

    Public Function GetAssessmentDirect(ByVal _empId As Decimal, ByVal _year As Decimal?, ByVal _status As Decimal?) As List(Of AssessmentDirectDTO)
        Try
            Dim lst As List(Of AssessmentDirectDTO) = New List(Of AssessmentDirectDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_PE_ASSESS_EMP_BY_DIRECT",
                                           New With {.P_DIRECT_ID = _empId,
                                                     .P_YEAR = _year,
                                                     .P_STATUS = _status,
                                                    .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    lst = (From row As DataRow In dtData.Rows
                       Select New AssessmentDirectDTO With {.ID = row("ID").ToString(),
                                                           .PE_PERIO_ID = row("PE_PERIO_ID").ToString(),
                                                           .PE_STATUS_ID = row("PE_STATUS_ID").ToString(),
                                                           .EMPLOYEE_ID = row("EMPLOYEE_ID").ToString(),
                                                           .EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString(),
                                                           .EMPLOYEE_NAME = row("EMPLOYEE_NAME").ToString(),
                                                            .ORG_ID = row("ORG_ID").ToString(),
                                                            .ORG_NAME = row("ORG_NAME").ToString(),
                                                            .TITLE_NAME = row("TITLE_NAME").ToString(),
                                                           .PE_PERIO_YEAR = row("PE_PERIO_YEAR").ToString(),
                                                           .PE_PERIO_NAME = row("PE_PERIO_NAME").ToString(),
                                                            .PE_PERIO_TYPE_ASS = row("PE_PERIO_TYPE_ASS").ToString(),
                                                           .PE_PERIO_TYPE_ASS_NAME = row("PE_PERIO_TYPE_ASS_NAME").ToString(),
                                                           .PE_PERIO_START_DATE = ToDate(row("PE_PERIO_START_DATE")),
                                                           .PE_PERIO_END_DATE = ToDate(row("PE_PERIO_END_DATE")),
                                                           .RESULT_CONVERT = row("RESULT_CONVERT").ToString(),
                                                           .PE_STATUS_NAME = row("PE_STATUS_NAME").ToString(),
                                                            .CLASSIFICATION_NAME = row("CLASSIFICATION_NAME").ToString(),
                                                            .CLASSIFICATION_NAME_NV = row("CLASSIFICATION_NAME_NV").ToString(),
                                                            .RESULT_CONVERT_NV = row("RESULT_CONVERT_NV").ToString(),
                                                            .OBJECT_GROUP_ID = row("OBJECT_GROUP_ID").ToString()
                                                          }).ToList
                End If
            End Using
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "GetAssessmentDirect")
            Throw ex
        End Try
    End Function

    Public Function GET_DM_STATUS() As DataTable
        Try
            Dim dtData As DataTable
            Using cls As New DataAccess.QueryData
                dtData = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_DM_STATUS",
                                           New With {.P_CUR = cls.OUT_CURSOR})

            End Using
            Return dtData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "GET_DM_STATUS")
            Throw ex
        End Try
    End Function

    Public Function UpdateStatusAssessmentDirect(ByVal obj As Decimal, ByVal log As UserLog) As Boolean
        Try
            'Dim objUpdate As New PE_EMPLOYEE_ASSESSMENT With {.ID = obj}
            'objUpdate = (From p In Context.PE_EMPLOYEE_ASSESSMENT
            '                       Where p.ID = obj).FirstOrDefault
            'objUpdate.PE_STATUS = 6141
            'Context.SaveChanges(log)
            'Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function GetKPIAssessEmp(ByVal _empId As Decimal) As List(Of AssessmentDirectDTO)
        Try
            Dim lst As List(Of AssessmentDirectDTO) = New List(Of AssessmentDirectDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_PE_ASSESS_EMP_BY_EMP",
                                           New With {.P_EMPLOYEE_ID = _empId,
                                                    .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    lst = (From row As DataRow In dtData.Rows
                       Select New AssessmentDirectDTO With {.ID = row("ID").ToString(),
                                                   .EMPLOYEE_ID = row("EMPLOYEE_ID").ToString(),
                                                   .EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString(),
                                                   .EMPLOYEE_NAME = row("EMPLOYEE_NAME").ToString(),
                                                   .PE_PERIO_YEAR = row("PE_PERIO_YEAR").ToString(),
                                                   .PE_PERIO_NAME = row("PE_PERIO_NAME").ToString(),
                                                    .PE_PERIO_TYPE_ASS = row("PE_PERIO_TYPE_ASS").ToString(),
                                                   .PE_PERIO_TYPE_ASS_NAME = row("PE_PERIO_TYPE_ASS_NAME").ToString(),
                                                   .PE_PERIO_START_DATE = ToDate(row("PE_PERIO_START_DATE")),
                                                   .PE_PERIO_END_DATE = ToDate(row("PE_PERIO_END_DATE")),
                                                   .RESULT_CONVERT = row("RESULT_CONVERT").ToString(),
                                                   .PE_STATUS_NAME = row("PE_STATUS_NAME").ToString(),
                                                   .CLASSIFICATION_NAME = row("CLASSIFICATION_NAME").ToString(),
                                                   .OBJECT_GROUP_ID = row("OBJECT_GROUP_ID").ToString(),
                                                   .PE_PERIO_ID = row("PE_PERIO_ID").ToString(),
                                                  .RESULT_CONVERT_NV = row("RESULT_CONVERT_NV").ToString(),
                                                  .CLASSIFICATION_NAME_NV = row("CLASSIFICATION_NAME_NV").ToString()
                                                  }).ToList
                End If
            End Using
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "GetKPIAssessEmp")
            Throw ex
        End Try
    End Function


    Public Function CHECK_APP(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable
        Try
            Dim dtData As DataTable
            Using cls As New DataAccess.QueryData
                dtData = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.CHECK_APP",
                                           New With {.P_EMPLOYEE_ID = _empId,
                                                     .P_PE_PERIOD_ID = __PE_PERIOD_ID,
                                                    .P_CUR = cls.OUT_CURSOR})

            End Using
            Return dtData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "CHECK_APP")
            Throw ex
        End Try
    End Function

    Public Function GET_PE_ASSESSMENT_HISTORY(ByVal _Id As Decimal) As DataTable
        Try
            Dim dtData As DataTable
            Using cls As New DataAccess.QueryData
                dtData = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_PE_ASSESSMENT_HISTORY",
                                           New With {.P_ID = _Id,
                                                    .P_CUR = cls.OUT_CURSOR})

            End Using
            Return dtData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "GET_PE_ASSESSMENT_HISTORY")
            Throw ex
        End Try
    End Function

    Public Function CHECK_APP_1(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable
        Try
            Dim dtData As DataTable
            Using cls As New DataAccess.QueryData
                dtData = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.CHECK_APP_1",
                                           New With {.P_EMPLOYEE_ID = _empId,
                                                     .P_PE_PERIOD_ID = __PE_PERIOD_ID,
                                                    .P_CUR = cls.OUT_CURSOR})

            End Using
            Return dtData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "CHECK_APP_1")
            Throw ex
        End Try
    End Function

    Public Function CHECK_APP_2(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable
        Try
            Dim dtData As DataTable
            Using cls As New DataAccess.QueryData
                dtData = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.CHECK_APP_2",
                                           New With {.P_EMPLOYEE_ID = _empId,
                                                     .P_PE_PERIOD_ID = __PE_PERIOD_ID,
                                                    .P_CUR = cls.OUT_CURSOR})

            End Using
            Return dtData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "CHECK_APP_1")
            Throw ex
        End Try
    End Function

    Public Function PRI_PERFORMACE_APP(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.PRI_PERFORMACE_APP",
                                           New With {.P_EMPLOYEE_ID = _empId,
                                                     .P_PE_PERIOD_ID = __PE_PERIOD_ID})

            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "PRI_PERFORMACE_APP")
            Throw ex
        End Try
    End Function

    Public Function INSERT_PE_ASSESSMENT_HISTORY(ByVal P_PE_PE_ASSESSMENT_ID As Decimal,
                                                 ByVal P_RESULT_DIRECT As String,
                                                 ByVal P_ASS_DATE As Date?,
                                                 ByVal P_REMARK_DIRECT As String,
                                                 ByVal P_CREATED_BY As String,
                                                 ByVal P_CREATED_LOG As String,
                                                 ByVal P_EMPLOYEE_ID As Decimal,
                                                 ByVal P_SIGN_ID As Decimal) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.INSERT_PE_ASSESSMENT_HISTORY",
                                           New With {.P_PE_PE_ASSESSMENT_ID = P_PE_PE_ASSESSMENT_ID,
                                                     .P_RESULT_DIRECT = P_RESULT_DIRECT,
                                                     .P_ASS_DATE = P_ASS_DATE,
                                                     .P_REMARK_DIRECT = P_REMARK_DIRECT,
                                                     .P_CREATED_BY = P_CREATED_BY,
                                                     .P_CREATED_LOG = P_CREATED_LOG,
                                                     .P_EMPLOYEE_ID = P_EMPLOYEE_ID,
                                                     .P_SIGN_ID = P_SIGN_ID})

            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "PRI_PERFORMACE_APP")
            Throw ex
        End Try
    End Function



    Public Function PRI_PERFORMACE_PROCESS(ByVal P_EMPLOYEE_APP_ID As Decimal, ByVal P_EMPLOYEE_ID As Decimal,
                                           ByVal P_PE_PERIOD_ID As Decimal, ByVal P_STATUS_ID As Decimal,
                                           ByVal P_NOTES As String) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.PRI_PERFORMACE_PROCESS",
                                           New With {.P_EMPLOYEE_APP_ID = P_EMPLOYEE_APP_ID,
                                                     .P_EMPLOYEE_ID = P_EMPLOYEE_ID,
                                                     .P_PE_PERIOD_ID = P_PE_PERIOD_ID,
                                                     .P_STATUS_ID = P_STATUS_ID,
                                                     .P_NOTES = P_NOTES})

            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "PRI_PERFORMACE_APP")
            Throw ex
        End Try
    End Function


#End Region

#Region "CBNS xem KPI cua nhan vien"
    Public Function GetDirectKPIEmployee(ByVal filter As AssessmentDirectDTO, ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "EMPLOYEE_ID desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of AssessmentDirectDTO)
        Try
            Dim lst As List(Of AssessmentDirectDTO) = New List(Of AssessmentDirectDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_PE_ASSESS_EMP_BY_ORG",
                                           New With {.P_YEAR = filter.PE_PERIO_YEAR,
                                                     .P_ORGID = _param.ORG_ID,
                                                     .P_USERNAME = log.Username,
                                                    .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    Dim query = From row As DataRow In dtData.Rows
                      Select New AssessmentDirectDTO With {.ID = row("ID").ToString(),
                                                           .OBJECT_GROUP_ID = row("OBJECT_GROUP_ID").ToString(),
                                                           .PE_PERIO_ID = row("PE_PERIO_ID").ToString(),
                                                           .PE_STATUS_ID = row("PE_STATUS_ID").ToString(),
                                                           .EMPLOYEE_ID = row("EMPLOYEE_ID").ToString(),
                                                           .EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString(),
                                                           .EMPLOYEE_NAME = row("EMPLOYEE_NAME").ToString(),
                                                            .ORG_ID = row("ORG_ID").ToString(),
                                                            .ORG_NAME = row("ORG_NAME").ToString(),
                                                            .TITLE_NAME = row("TITLE_NAME").ToString(),
                                                           .PE_PERIO_YEAR = row("PE_PERIO_YEAR").ToString(),
                                                           .PE_PERIO_NAME = row("PE_PERIO_NAME").ToString(),
                                                            .PE_PERIO_TYPE_ASS = row("PE_PERIO_TYPE_ASS").ToString(),
                                                           .PE_PERIO_TYPE_ASS_NAME = row("PE_PERIO_TYPE_ASS_NAME").ToString(),
                                                           .PE_PERIO_START_DATE = ToDate(row("PE_PERIO_START_DATE")),
                                                           .PE_PERIO_END_DATE = ToDate(row("PE_PERIO_END_DATE")),
                                                           .RESULT_CONVERT = row("RESULT_CONVERT").ToString(),
                                                           .PE_STATUS_NAME = row("PE_STATUS_NAME").ToString(),
                                                           .RESULT_CONVERT_NV = row("RESULT_CONVERT_NV").ToString(),
                                                           .CLASSIFICATION_NAME_NV = row("CLASSIFICATION_NAME_NV").ToString(),
                    .CLASSIFICATION_NAME = row("CLASSIFICATION_NAME").ToString()}
                    Dim lst1 = query
                    'lst1 = lst1.OrderBy(Sorts)
                    Total = lst1.Count
                    lst1 = lst1.Skip(PageIndex * PageSize).Take(PageSize)
                    Return lst1.ToList
                End If
            End Using
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetDirectKPIEmployee")
            Throw ex
        End Try
    End Function
#End Region

#Region "Bieu do xep hang nhan vien"

    Public Function GetAssessRatingEmployee(ByVal filter As AssessRatingDTO) As List(Of AssessRatingDTO)
        Try
            Dim lst As List(Of AssessRatingDTO) = New List(Of AssessRatingDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_PE_ASSESS_RATING",
                                           New With {.P_YEAR = filter.PERIOD_YEAR,
                                                     .P_PERIOD_ID = filter.PERIOD_ID,
                                                     .P_TYPEASS_ID = filter.PERIOD_TYPEASS_ID,
                                                     .P_DIRECT_ID = filter.DIRECT_ID,
                                                     .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    lst = (From row As DataRow In dtData.Rows
                       Select New AssessRatingDTO With {.CLASSIFICATION_ID = row("CLASSIFICATION_ID").ToString(),
                                                           .CLASSIFICATION_CODE = row("CLASSIFICATION_CODE").ToString(),
                                                           .CLASSIFICATION_NAME = row("CLASSIFICATION_NAME").ToString(),
                                                           .COUNT_EMP = row("COUNT_EMP").ToString()
                                                          }).ToList
                End If
            End Using
            Return lst
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetAssessRatingEmployee")
            Throw ex
        End Try
    End Function
    Public Function GetAssessRatingEmployeeOrg(ByVal filter As AssessRatingDTO, Optional ByVal log As UserLog = Nothing) As List(Of AssessRatingDTO)
        Try
            Dim lst As List(Of AssessRatingDTO) = New List(Of AssessRatingDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_PE_ASSESS_RATING_ORG",
                                           New With {.P_YEAR = filter.PERIOD_YEAR,
                                                     .P_PERIOD_ID = filter.PERIOD_ID,
                                                     .P_TYPEASS_ID = filter.PERIOD_TYPEASS_ID,
                                                     .P_USERNAME = log.Username,
                                                     .P_ORGID = filter.ORG_ID,
                                                     .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    lst = (From row As DataRow In dtData.Rows
                       Select New AssessRatingDTO With {.CLASSIFICATION_ID = row("CLASSIFICATION_ID").ToString(),
                                                           .CLASSIFICATION_CODE = row("CLASSIFICATION_CODE").ToString(),
                                                           .CLASSIFICATION_NAME = row("CLASSIFICATION_NAME").ToString(),
                                                           .COUNT_EMP = row("COUNT_EMP").ToString()
                                                          }).ToList
                End If
            End Using
            Return lst
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetAssessRatingEmployeeOrg")
            Throw ex
        End Try
    End Function

#End Region

    Public Function PRINT_PE_ASSESS(ByVal empID As Decimal, ByVal period As Decimal, ByVal obj As Decimal) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_PERFORMANCE_LIST.PRINT_PE_ASSESS",
                                           New With {.P_EMPLOYEE_ID = empID,
                                                     .P_PE_PERIOD_ID = period,
                                                     .P_OBJECT_GROUP_ID = obj,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                     .P_CUR2 = cls.OUT_CURSOR}, False) ' FALSE : no datatable

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#Region "xem ket qua tong hop"
    Public Function GetListEmployeePaging1(ByVal _filter As KPI_EVALUATEDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of KPI_EVALUATEDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim fileDirectory = ""
            Dim str As String = "Kiêm nhiệm"
            fileDirectory = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage"
            Dim wstt = 257

            Dim query = From mbo In Context.PE_KPI_EVALUATE
                        From p In Context.HU_EMPLOYEE.Where(Function(f) f.ID = mbo.EMPLOYEE_ID)
                        From abc In Context.PE_EVALUATE_PERIOD.Where(Function(f) f.EMPLOYEE_ID = p.ID)
                        From period In Context.PE_PERIOD.Where(Function(f) f.ID = mbo.KPI_ID).DefaultIfEmpty
                        From sal_level In Context.PA_SALARY_LEVEL.Where(Function(f) f.ID = mbo.SALARYLEVEL_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = mbo.CLASSFICATION).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) mbo.ORG_ID = f.ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) mbo.TITLE_ID = f.ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) mbo.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Order By p.EMPLOYEE_CODE()

            Dim lst = query.Select(Function(p) New KPI_EVALUATEDTO With {
                             .EMPLOYEE_CODE = p.p.EMPLOYEE_CODE,
                             .ID = p.p.ID,
                             .FULLNAME = p.p.FULLNAME_VN,
                             .ORG_NAME = p.org.NAME_VN,
                             .TITLE_NAME = p.title.NAME_VN,
                             .TER_EFFECT_DATE = p.p.TER_EFFECT_DATE,
                             .WORK_STATUS = p.p.WORK_STATUS,
                             .SALARY_LEVEL_ID = p.mbo.SALARYLEVEL_ID,
                             .SALARY_LEVEL = p.sal_level.NAME,
                             .KPI_EVALUATE = p.mbo.KPI_ID,
                             .FINANCE_TT = p.mbo.FINANCE_TT,
                             .FINANCE_TTX = p.mbo.FINANCE_TTX,
                             .JOIN_DATE = p.mbo.JOIN_DATE,
                             .END_DATE = p.mbo.END_DATE,
                             .CUSTOMER_TT = p.mbo.CUSTOMER_TT,
                             .CUSTOMER_TTX = p.mbo.CUSTOMER_TTX,
                             .PROCESS_TT = p.mbo.PROCESS_TT,
                             .PROCESS_TTX = p.mbo.PROCESS_TTX,
                             .LEARN_TT = p.mbo.LEARN_TT,
                             .LEARN_TTX = p.mbo.LEARN_TTX,
                             .SUM_TT = p.mbo.SUM_TT,
                             .SUM_TTX = p.mbo.SUM_TTX,
                             .SUM_RATE_KPI = p.mbo.SUM_RATE_KPI,
                             .CLASSFICATION_ID = p.mbo.CLASSFICATION,
                             .CLASSFICATION = p.ot.NAME_VN,
                             .COMMENTS = p.mbo.COMMENTS,
                             .REMARK = p.mbo.REMARK
                         })

            Dim dateNow = Date.Now.Date
            Dim terID = 257
            If Not _filter.IS_TER Then
                lst = lst.Where(Function(p) Not p.WORK_STATUS.HasValue Or _
                                    (p.WORK_STATUS.HasValue And _
                                     ((p.WORK_STATUS <> terID) Or (p.WORK_STATUS = terID And p.TER_EFFECT_DATE > dateNow))))

            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper().IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0)
            End If
            If _filter.KPI_EVALUATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.KPI_EVALUATE = _filter.KPI_EVALUATE)
            End If
            If _filter.FULLNAME <> "" Then
                lst = lst.Where(Function(p) p.FULLNAME.ToUpper().IndexOf(_filter.FULLNAME.ToUpper) >= 0)
            End If

            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper().IndexOf(_filter.TITLE_NAME.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper().IndexOf(_filter.ORG_NAME.ToUpper) >= 0)
            End If
            If _filter.FINANCE_TT IsNot Nothing Then
                lst = lst.Where(Function(p) p.FINANCE_TT = _filter.FINANCE_TT)
            End If

            If _filter.FINANCE_TTX IsNot Nothing Then
                lst = lst.Where(Function(p) p.FINANCE_TTX = _filter.FINANCE_TTX)
            End If
            If _filter.CUSTOMER_TT IsNot Nothing Then
                lst = lst.Where(Function(p) p.CUSTOMER_TT = _filter.CUSTOMER_TT)
            End If
            If _filter.CUSTOMER_TTX IsNot Nothing Then
                lst = lst.Where(Function(p) p.CUSTOMER_TTX = _filter.CUSTOMER_TTX)
            End If
            If _filter.PROCESS_TT IsNot Nothing Then
                lst = lst.Where(Function(p) p.PROCESS_TT = _filter.PROCESS_TT)
            End If
            If _filter.PROCESS_TTX IsNot Nothing Then
                lst = lst.Where(Function(p) p.PROCESS_TTX = _filter.PROCESS_TTX)
            End If
            If _filter.LEARN_TT IsNot Nothing Then
                lst = lst.Where(Function(p) p.LEARN_TT = _filter.LEARN_TT)
            End If
            If _filter.LEARN_TTX IsNot Nothing Then
                lst = lst.Where(Function(p) p.LEARN_TTX = _filter.LEARN_TTX)
            End If
            If _filter.SUM_TT IsNot Nothing Then
                lst = lst.Where(Function(p) p.SUM_TT = _filter.SUM_TT)
            End If
            If _filter.SUM_TTX IsNot Nothing Then
                lst = lst.Where(Function(p) p.SUM_TTX = _filter.SUM_TTX)
            End If
            If _filter.SUM_RATE_KPI IsNot Nothing Then
                lst = lst.Where(Function(p) p.SUM_RATE_KPI = _filter.SUM_RATE_KPI)
            End If
            If _filter.COMMENTS IsNot Nothing Then
                lst = lst.Where(Function(p) p.COMMENTS.ToUpper.Contains(_filter.COMMENTS.ToUpper))
            End If
            If _filter.CLASSFICATION IsNot Nothing Then
                lst = lst.Where(Function(p) p.CLASSFICATION.ToUpper.Contains(_filter.CLASSFICATION.ToUpper))
            End If
            If _filter.REMARK IsNot Nothing Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If


            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Dim lstEmp = lst.ToList


            Return lstEmp

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetLstPeriod1(ByVal year As Decimal) As DataTable
        Dim dt As New DataTable
        Try
            dt = (From p In Context.PE_PERIOD
                  Where p.ACTFLG = "A"
                  Where p.YEAR = year Select p.ID, p.NAME).ToList.ToTable()

            Return dt
        Catch ex As Exception

        End Try
    End Function
#End Region
#Region "danh gia kpis"
    Public Function CheckEmployee_Exits(ByVal empCode As String) As Integer
        Dim objEmp As HU_EMPLOYEE
        Dim result As Integer
        Try
            Dim query = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = empCode.Trim())
            Dim lst = query.Select(Function(f) New EmployeeDTO With
                                              {.ID = f.ID})
            If lst.Count > 0 Then
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
    Public Function GetListEmployeePaging(ByVal _filter As KPI_EVALUATEDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of KPI_EVALUATEDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim fileDirectory = ""
            Dim str As String = "Kiêm nhiệm"
            fileDirectory = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage"
            Dim wstt = 257

            Dim query = From mbo In Context.PE_KPI_EVALUATE
                        From p In Context.HU_EMPLOYEE.Where(Function(f) f.ID = mbo.EMPLOYEE_ID)
                        From period In Context.PE_PERIOD.Where(Function(f) f.ID = mbo.KPI_ID).DefaultIfEmpty
                        From sal_level In Context.PA_SALARY_LEVEL.Where(Function(f) f.ID = mbo.SALARYLEVEL_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = mbo.CLASSFICATION).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) mbo.ORG_ID = f.ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) mbo.TITLE_ID = f.ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) mbo.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Order By p.EMPLOYEE_CODE()

            Dim lst = query.Select(Function(p) New KPI_EVALUATEDTO With {
                             .EMPLOYEE_CODE = p.p.EMPLOYEE_CODE,
                             .ID = p.p.ID,
                             .FULLNAME = p.p.FULLNAME_VN,
                             .ORG_NAME = p.org.NAME_VN,
                             .TITLE_NAME = p.title.NAME_VN,
                             .TER_EFFECT_DATE = p.p.TER_EFFECT_DATE,
                             .WORK_STATUS = p.p.WORK_STATUS,
                             .SALARY_LEVEL_ID = p.mbo.SALARYLEVEL_ID,
                             .SALARY_LEVEL = p.sal_level.NAME,
                             .KPI_EVALUATE = p.mbo.KPI_ID,
                             .FINANCE_TT = p.mbo.FINANCE_TT,
                             .FINANCE_TTX = p.mbo.FINANCE_TTX,
                             .JOIN_DATE = p.mbo.JOIN_DATE,
                             .END_DATE = p.mbo.END_DATE,
                             .CUSTOMER_TT = p.mbo.CUSTOMER_TT,
                             .CUSTOMER_TTX = p.mbo.CUSTOMER_TTX,
                             .PROCESS_TT = p.mbo.PROCESS_TT,
                             .PROCESS_TTX = p.mbo.PROCESS_TTX,
                             .LEARN_TT = p.mbo.LEARN_TT,
                             .LEARN_TTX = p.mbo.LEARN_TTX,
                             .SUM_TT = p.mbo.SUM_TT,
                             .SUM_TTX = p.mbo.SUM_TTX,
                             .SUM_RATE_KPI = p.mbo.SUM_RATE_KPI,
                             .CLASSFICATION_ID = p.mbo.CLASSFICATION,
                             .CLASSFICATION = p.ot.NAME_VN,
                             .COMMENTS = p.mbo.COMMENTS,
                             .REMARK = p.mbo.REMARK
                         })

            Dim dateNow = Date.Now.Date
            Dim terID = 257
            If Not _filter.IS_TER Then
                lst = lst.Where(Function(p) Not p.WORK_STATUS.HasValue Or _
                                    (p.WORK_STATUS.HasValue And _
                                     ((p.WORK_STATUS <> terID) Or (p.WORK_STATUS = terID And p.TER_EFFECT_DATE > dateNow))))

            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper().IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0)
            End If
            If _filter.KPI_EVALUATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.KPI_EVALUATE = _filter.KPI_EVALUATE)
            End If
            If _filter.FULLNAME <> "" Then
                lst = lst.Where(Function(p) p.FULLNAME.ToUpper().IndexOf(_filter.FULLNAME.ToUpper) >= 0)
            End If

            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper().IndexOf(_filter.TITLE_NAME.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper().IndexOf(_filter.ORG_NAME.ToUpper) >= 0)
            End If
            If _filter.FINANCE_TT IsNot Nothing Then
                lst = lst.Where(Function(p) p.FINANCE_TT = _filter.FINANCE_TT)
            End If

            If _filter.FINANCE_TTX IsNot Nothing Then
                lst = lst.Where(Function(p) p.FINANCE_TTX = _filter.FINANCE_TTX)
            End If
            If _filter.CUSTOMER_TT IsNot Nothing Then
                lst = lst.Where(Function(p) p.CUSTOMER_TT = _filter.CUSTOMER_TT)
            End If
            If _filter.CUSTOMER_TTX IsNot Nothing Then
                lst = lst.Where(Function(p) p.CUSTOMER_TTX = _filter.CUSTOMER_TTX)
            End If
            If _filter.PROCESS_TT IsNot Nothing Then
                lst = lst.Where(Function(p) p.PROCESS_TT = _filter.PROCESS_TT)
            End If
            If _filter.PROCESS_TTX IsNot Nothing Then
                lst = lst.Where(Function(p) p.PROCESS_TTX = _filter.PROCESS_TTX)
            End If
            If _filter.LEARN_TT IsNot Nothing Then
                lst = lst.Where(Function(p) p.LEARN_TT = _filter.LEARN_TT)
            End If
            If _filter.LEARN_TTX IsNot Nothing Then
                lst = lst.Where(Function(p) p.LEARN_TTX = _filter.LEARN_TTX)
            End If
            If _filter.SUM_TT IsNot Nothing Then
                lst = lst.Where(Function(p) p.SUM_TT = _filter.SUM_TT)
            End If
            If _filter.SUM_TTX IsNot Nothing Then
                lst = lst.Where(Function(p) p.SUM_TTX = _filter.SUM_TTX)
            End If
            If _filter.SUM_RATE_KPI IsNot Nothing Then
                lst = lst.Where(Function(p) p.SUM_RATE_KPI = _filter.SUM_RATE_KPI)
            End If
            If _filter.COMMENTS IsNot Nothing Then
                lst = lst.Where(Function(p) p.COMMENTS.ToUpper.Contains(_filter.COMMENTS.ToUpper))
            End If
            If _filter.CLASSFICATION IsNot Nothing Then
                lst = lst.Where(Function(p) p.CLASSFICATION.ToUpper.Contains(_filter.CLASSFICATION.ToUpper))
            End If
            If _filter.REMARK IsNot Nothing Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If


            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Dim lstEmp = lst.ToList


            Return lstEmp

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetExportKPI(ByVal id As Decimal) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataSet = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GETEXPORTKPI",
                                                         New With {.ID = id,
                                                                   .P_CUR = cls.OUT_CURSOR,
                                                                   .P_CUR1 = cls.OUT_CURSOR,
                                                                   .P_CUR2 = cls.OUT_CURSOR}, False)
                Return dsdata
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetlistYear() As DataTable
        Dim dtData As New DataTable
        Try
            dtData = (From p In Context.PE_PERIOD
                      Where p.ACTFLG = "A"
                      Group p By p.YEAR Into Group
                      Select New With {.Year = YEAR, .ID = YEAR}).ToList.ToTable()
            Return dtData
        Catch ex As Exception

        End Try
    End Function
    Public Function GetLstPeriod(ByVal year As Decimal) As DataTable
        Dim dt As New DataTable
        Try
            dt = (From p In Context.PE_PERIOD
                  Where p.ACTFLG = "A" And p.TYPE_ASS = 7941
                  Where p.YEAR = year Select p.ID, p.NAME).ToList.ToTable()

            Return dt
        Catch ex As Exception

        End Try
    End Function
    Public Function GetPeriodDate(ByVal id As Decimal) As PeriodDTO
        Try
            Dim obj = From p In Context.PE_PERIOD
                      Where p.ID = id And p.ACTFLG = "A"
                      Select New PeriodDTO With {.START_DATE = p.START_DATE,
                                                 .END_DATE = p.END_DATE}


            Return obj.FirstOrDefault
        Catch ex As Exception

        End Try
    End Function
#End Region
End Class
