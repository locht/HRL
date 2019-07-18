Imports System.Transactions
Imports System.Web
Imports Framework.Data
Imports System.Data.Objects
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Data.EntityClient
Imports Framework.Data.System.Linq.Dynamic
Imports System.Data.Entity
Imports System.Text.RegularExpressions
Imports System.Reflection

Partial Class ProfileRepository
#Region "calculator salary "
    Public Function Calculator_Salary(ByVal data_in As String) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.CALCULATOR_SALARY",
                                                    New With {.P_DATA_IN = data_in,
                                                                .CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "EmployeeCriteriaRecord"
    Public Function EmployeeCriteriaRecord(ByVal _filter As EmployeeCriteriaRecordDTO,
                                              ByVal PageIndex As Integer,
                                              ByVal PageSize As Integer,
                                              ByRef Total As Integer, ByVal _param As ParamDTO,
                                              Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                              Optional ByVal log As UserLog = Nothing) As List(Of EmployeeCriteriaRecordDTO)

        Try
            'Dim result As List(Of EmployeeCriteriaRecordDTO)
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From Competency In Context.HU_COMPETENCY
                        From stand In Context.HU_COMPETENCY_STANDARD.Where(Function(f) f.COMPETENCY_ID = Competency.ID).DefaultIfEmpty
                        From Competencygroup In Context.HU_COMPETENCY_GROUP.Where(Function(f) f.ID = Competency.COMPETENCY_GROUP_ID)
                        From ass In Context.HU_COMPETENCY_ASS.Where(Function(f) stand.TITLE_ID = f.TITLE_ID).DefaultIfEmpty
                        From p In Context.HU_COMPETENCY_ASSDTL.Where(Function(f) f.COMPETENCY_ASS_ID = ass.ID And
                                                                         f.COMPETENCY_ID = stand.COMPETENCY_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = ass.EMPLOYEE_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = stand.TITLE_ID And f.ID = e.TITLE_ID)
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)

            Dim lst = query.Select(Function(p) New EmployeeCriteriaRecordDTO With {
                                        .EMPLOYEE_ID = p.e.ID,
                                        .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                        .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                        .ORG_ID = p.e.ORG_ID,
                                        .ORG_NAME = p.org.NAME_VN,
                                        .TITLE_ID = p.e.TITLE_ID,
                                        .TITLE_NAME = p.title.NAME_VN,
                                        .COMPETENCY_GROUP_ID = p.Competencygroup.ID,
                                        .COMPETENCY_GROUP_NAME = p.Competencygroup.NAME,
                                        .COMPETENCY_ID = p.Competency.ID,
                                        .COMPETENCY_NAME = p.Competency.NAME,
                                        .LEVEL_NUMBER = p.p.LEVEL_NUMBER
                                        })

            'Lọc theo nhom nang luc
            If _filter.COMPETENCY_GROUP_ID.ToString() <> "" Then
                lst = lst.Where(Function(p) p.COMPETENCY_GROUP_ID = _filter.COMPETENCY_GROUP_ID)
            End If
            'Lọc theo muc
            If (_filter.LEVEL_NUMBER IsNot Nothing) Then
                lst = lst.Where(Function(p) p.LEVEL_NUMBER >= _filter.LEVEL_NUMBER)
            End If
            'Lọc theo nang luc
            'If _filter.COMPETENCY_ID.ToString() <> "" Then
            '    lst = lst.Where(Function(p) p.COMPETENCY_ID = _filter.COMPETENCY_ID)
            'End If
            'p => p.Genres.Any(x => listOfGenres.Contains(x)
            '(p.WORK_STATUS.HasValue And p.WORK_STATUS = 257 And (p.TER_EFFECT_DATE <= dateNow Or p.TER_LAST_DATE <= dateNow))

            If _filter.LST_COMPETENCY_ID.Count > 0 Then
                Dim result = From lstResult In lst
                              From filter In _filter.LST_COMPETENCY_ID.Where(Function(f) f = lstResult.COMPETENCY_ID)
                Dim lst_result = result.Select(Function(p) New EmployeeCriteriaRecordDTO With {
                                       .EMPLOYEE_ID = p.lstResult.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.lstResult.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.lstResult.EMPLOYEE_NAME,
                                       .ORG_ID = p.lstResult.ORG_ID,
                                       .ORG_NAME = p.lstResult.ORG_NAME,
                                       .TITLE_ID = p.lstResult.TITLE_ID,
                                       .TITLE_NAME = p.lstResult.TITLE_NAME,
                                       .COMPETENCY_GROUP_ID = p.lstResult.COMPETENCY_GROUP_ID,
                                       .COMPETENCY_GROUP_NAME = p.lstResult.COMPETENCY_GROUP_NAME,
                                       .COMPETENCY_ID = p.lstResult.COMPETENCY_ID,
                                       .COMPETENCY_NAME = p.lstResult.COMPETENCY_NAME,
                                       .LEVEL_NUMBER = p.lstResult.LEVEL_NUMBER
                                       })
                lst_result = lst_result.OrderBy(Sorts)
                Total = lst_result.Count
                lst_result = lst_result.Skip(PageIndex * PageSize).Take(PageSize)

                Dim lstData = lst_result.ToList
                For Each item In lstData
                    If item.LEVEL_NUMBER IsNot Nothing Then
                        item.LEVEL_NUMBER_ASS_NAME = item.LEVEL_NUMBER.Value.ToString & "/4"
                    End If
                Next
                Return lstData
            Else
                lst = lst.OrderBy(Sorts)
                Total = lst.Count
                lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

                Dim lstData = lst.ToList
                For Each item In lstData
                    If item.LEVEL_NUMBER IsNot Nothing Then
                        item.LEVEL_NUMBER_ASS_NAME = item.LEVEL_NUMBER.Value.ToString & "/4"
                    End If
                Next
                Return lstData
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

    Public Function GetHoSoLuongImport() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataSet = cls.ExecuteStore("PKG_PROFILE_INTEGRATED.GET_HOSOLUONG_IMPORT",
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
