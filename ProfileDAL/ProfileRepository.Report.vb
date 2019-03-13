Imports System.Linq.Expressions
Imports LinqKit.Extensions
Imports System.Data.Common
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic
Imports System.Data.Entity
Imports System.Text.RegularExpressions
Imports Framework.Data.SystemConfig
Imports System.Reflection

Partial Class ProfileRepository
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Lay danh sach bao cao
    ''' </summary>
    ''' <param name="_filter"></param>
    ''' <param name="PageIndex"></param>
    ''' <param name="PageSize"></param>
    ''' <param name="Total"></param>
    ''' <param name="_param"></param>
    ''' <param name="Sorts"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetReportList(ByVal _filter As WorkingDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO)

        Try
            Dim wSTT = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.HU_WORKING
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From sal_group In Context.PA_SALARY_GROUP.Where(Function(f) p.SAL_GROUP_ID = f.ID).DefaultIfEmpty
                        From sal_level In Context.PA_SALARY_LEVEL.Where(Function(f) p.SAL_LEVEL_ID = f.ID).DefaultIfEmpty
                        From sal_rank In Context.PA_SALARY_RANK.Where(Function(f) p.SAL_RANK_ID = f.ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From deci_type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DECISION_TYPE_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        Select New WorkingDTO With {.ID = p.ID,
                                                    .DECISION_NO = p.DECISION_NO,
                                                    .DECISION_TYPE_ID = p.DECISION_TYPE_ID,
                                                    .DECISION_TYPE_NAME = deci_type.NAME_VN,
                                                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                    .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                    .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                    .ORG_NAME = o.NAME_VN,
                                                    .ORG_DESC = o.DESCRIPTION_PATH,
                                                    .TITLE_NAME = t.NAME_VN,
                                                    .EFFECT_DATE = p.EFFECT_DATE,
                                                    .EXPIRE_DATE = p.EXPIRE_DATE,
                                                    .STATUS_ID = p.STATUS_ID,
                                                    .STATUS_NAME = status.NAME_VN,
                                                    .SAL_BASIC = p.SAL_BASIC,
                                                    .IS_MISSION = p.IS_MISSION,
                                                    .IS_WAGE = p.IS_WAGE,
                                                    .IS_PROCESS = p.IS_PROCESS,
                                                    .IS_3B = p.IS_3B,
                                                    .IS_MISSION_SHORT = p.IS_MISSION,
                                                    .IS_WAGE_SHORT = p.IS_WAGE,
                                                    .IS_PROCESS_SHORT = p.IS_PROCESS,
                                                    .IS_3B_SHORT = p.IS_3B,
                                                    .SAL_GROUP_ID = p.SAL_GROUP_ID,
                                                    .SAL_GROUP_NAME = sal_group.NAME,
                                                    .SAL_LEVEL_ID = p.SAL_LEVEL_ID,
                                                    .SAL_LEVEL_NAME = sal_level.NAME,
                                                    .SAL_RANK_ID = p.SAL_RANK_ID,
                                                    .SAL_RANK_NAME = sal_rank.RANK,
                                                    .COST_SUPPORT = p.COST_SUPPORT,
                                                    .PERCENT_SALARY = p.PERCENT_SALARY,
                                                    .CREATED_DATE = p.CREATED_DATE}


            ' danh sách thông tin quá trình công tác
            Dim working = query

            working = working.OrderBy(Sorts)
            Total = working.Count
            working = working.Skip(PageIndex * PageSize).Take(PageSize)

            Return working.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function
End Class
