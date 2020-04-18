Imports System.Data.Objects
Imports Framework.Data
Imports LinqKit
Imports System.Data.Objects.DataClasses
Imports System.Data.Common
Imports System.Data.Entity
Imports System.Threading
Imports Framework.Data.SystemConfig
Imports Framework.Data.System.Linq.Dynamic
Imports System.Configuration
Imports System.Reflection

Partial Public Class AttendanceRepository

    ''' <summary>
    ''' Lấy danh sách nhân viên thuộc đối tượng nhân viên hoặc đối tượng nghỉ bù hoặc cả 2 bằng null
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetObjEmpCompe(ByVal _filter As AT_ObjectEmpployeeCompensatoryDTO,
                                        ByVal _param As ParamDTO,
                                          Optional ByRef Total As Integer = 0,
                                          Optional ByVal PageIndex As Integer = 0,
                                          Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of AT_ObjectEmpployeeCompensatoryDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim lst As List(Of AT_ObjectEmpployeeCompensatoryDTO) = New List(Of AT_ObjectEmpployeeCompensatoryDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_OBJECT_EMPLOYEE_COMPENSATORY",
                                           New With {.P_SE_ORG_ID = _param.ORG_ID,
                                                     .P_SE_USERNAME = log.Username.ToUpper,
                                                     .P_SE_ISDISSOLVE = _param.IS_DISSOLVE,
                                                    .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    lst = (From row As DataRow In dtData.Rows
                       Select New AT_ObjectEmpployeeCompensatoryDTO With {.STT = row("STT").ToString(),
                                                   .ID = row("ID").ToString(),
                                                   .EMPLOYEE_ID = row("EMPLOYEE_ID").ToString(),
                                                   .EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString(),
                                                   .FULLNAME_VN = row("FULLNAME_VN").ToString(),
                                                   .TITLE_ID = row("TITLE_ID").ToString(),
                                                   .TITLE_NAME = row("TITLE_NAME").ToString(),
                                                    .ORG_ID = row("ORG_ID").ToString(),
                                                   .ORG_NAME = row("ORG_NAME").ToString(),
                                                   .SAL_LEVEL_ID = row("SAL_LEVEL_ID").ToString(),
                                                   .SAL_LEVEL_NAME = row("SAL_LEVEL_NAME").ToString(),
                                                   .SAL_RANK_ID = row("SAL_RANK_ID").ToString(),
                                                   .SAL_RANK_NAME = row("SAL_RANK_NAME").ToString(),
                                                   .OBJ_EMP_ID = row("OBJ_EMP_ID").ToString(),
                                                    .OBJ_EMP_NAME = row("OBJ_EMP_NAME").ToString(),
                                                   .OBJ_CSL_ID = row("OBJ_CSL_ID").ToString(),
                                                   .OBJ_CSL_NAME = row("OBJ_CSL_NAME").ToString(),
                                                   .WORK_STATUS = row("WORK_STATUS").ToString(),
                                                    .WORK_STATUS_NAME = row("WORK_STATUS_NAME").ToString(),
                                                     .CREATED_DATE = ToDate(row("CREATED_DATE"))
                                                  })
                    'TÌM KIẾM BÊN KHUNG TÌM KIẾM
                    If _filter.WORK_STATUS Then
                        lst = lst.Where(Function(f) f.WORK_STATUS = 257)                       
                    End If
                    If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE_NAME) Then
                        lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE_NAME.ToLower()) Or f.FULLNAME_VN.ToLower().Contains(_filter.EMPLOYEE_CODE_NAME.ToLower()))
                    End If
                    If _filter.OBJ_EMP_ID Then
                        lst = lst.Where(Function(f) f.OBJ_EMP_ID = _filter.OBJ_EMP_ID)
                    End If
                    If _filter.OBJ_CSL_ID Then
                        lst = lst.Where(Function(f) f.OBJ_CSL_ID = _filter.OBJ_CSL_ID)
                    End If

                    'TÌM KIẾM TRÊN GRID
                    If Not String.IsNullOrEmpty(_filter.OBJ_EMP_NAME) Then
                        lst = lst.Where(Function(f) f.OBJ_EMP_NAME.ToLower().Contains(_filter.OBJ_EMP_NAME.ToLower()))
                    End If
                    If Not String.IsNullOrEmpty(_filter.OBJ_CSL_NAME) Then
                        lst = lst.Where(Function(f) f.OBJ_CSL_NAME.ToLower().Contains(_filter.OBJ_CSL_NAME.ToLower()))
                    End If
                    If Not String.IsNullOrEmpty(_filter.SAL_LEVEL_NAME) Then
                        lst = lst.Where(Function(f) f.SAL_LEVEL_NAME.ToLower().Contains(_filter.SAL_LEVEL_NAME.ToLower()))
                    End If
                    If Not String.IsNullOrEmpty(_filter.SAL_RANK_NAME) Then
                        lst = lst.Where(Function(f) f.SAL_RANK_NAME.ToLower().Contains(_filter.SAL_RANK_NAME.ToLower()))
                    End If
                    If Not String.IsNullOrEmpty(_filter.WORK_STATUS_NAME) Then
                        lst = lst.Where(Function(f) f.WORK_STATUS_NAME.ToLower().Contains(_filter.WORK_STATUS_NAME.ToLower()))
                    End If
                    If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                        lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
                    End If
                    If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                        lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
                    End If
                    If Not String.IsNullOrEmpty(_filter.FULLNAME_VN) Then
                        lst = lst.Where(Function(f) f.FULLNAME_VN.ToLower().Contains(_filter.FULLNAME_VN.ToLower()))
                    End If
                    If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                        lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
                    End If

                    lst = lst.OrderBy(Function(f) Sorts)
                    Total = lst.Count
                    lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
                End If
            End Using

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
End Class