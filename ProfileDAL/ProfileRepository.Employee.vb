Imports System.IO
Imports Framework.Data
Imports System.Data.Objects
Imports Framework.Data.System.Linq.Dynamic
Imports Framework.Data.DataAccess
Imports Oracle.DataAccess.Client
Imports System.Reflection

Partial Class ProfileRepository

#Region "Employee"
    Public Function GetEmpInfomations(ByVal orgIDs As List(Of Decimal),
                                      ByVal _filter As EmployeeDTO,
                                      ByVal PageIndex As Integer,
                                      ByVal PageSize As Integer,
                                      ByRef Total As Integer,
                                      Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                      Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO)
        Try
            Dim Employees = (From e In Context.HU_EMPLOYEE
                             From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                             From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                             Where orgIDs.Contains(e.ORG_ID) Order By e.EMPLOYEE_CODE
                            Select New EmployeeDTO With {
                                .ID = e.ID,
                                .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                .FULLNAME_VN = e.FULLNAME_VN,
                                .TITLE_ID = e.TITLE_ID,
                                .TITLE_NAME_VN = t.NAME_VN,
                                .BIRTH_DATE = cv.BIRTH_DATE,
                                .MOBILE_PHONE = cv.MOBILE_PHONE,
                                .IMAGE = cv.IMAGE,
                                .WORK_STATUS = e.WORK_STATUS,
                                .JOIN_DATE = e.JOIN_DATE
                                })
            If _filter.IS_TER = True Then
                'Return Employees.ToList()
            Else
                Employees = Employees.Where(Function(f) f.WORK_STATUS <> 257)
            End If
            If _filter.TITLE_NAME_VN <> "" Then
                Employees = Employees.Where(Function(f) f.TITLE_NAME_VN.ToUpper().IndexOf(_filter.TITLE_NAME_VN.ToUpper) >= 0)
            End If
            If _filter.FULLNAME_VN <> "" Then
                Employees = Employees.Where(Function(f) f.FULLNAME_VN.ToUpper().IndexOf(_filter.FULLNAME_VN.ToUpper) >= 0)
            End If
            If IsDate(_filter.BIRTH_DATE) Then
                Employees = Employees.Where(Function(f) f.BIRTH_DATE = _filter.BIRTH_DATE)
            End If
            If _filter.MOBILE_PHONE <> "" Then
                Employees = Employees.Where(Function(f) f.MOBILE_PHONE.ToUpper().IndexOf(_filter.MOBILE_PHONE.ToUpper) >= 0)
            End If
            Employees = Employees.OrderBy(Sorts)
            Total = Employees.Count
            Employees = Employees.Skip(PageIndex * PageSize).Take(PageSize)

            Dim lstEmp = Employees.ToList

            For Each emp In lstEmp
                emp.IMAGE_BINARY = GetEmployeeImage(emp.ID, "", False, emp.IMAGE)
            Next
            Return lstEmp
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' Lấy danh sách nhân viên ko phân trang
    ''' </summary>
    ''' <param name="_orgIds"></param>
    ''' <param name="_filter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListEmployee(ByVal _orgIds As List(Of Decimal), ByVal _filter As EmployeeDTO) As List(Of EmployeeDTO)
        Try

            Dim wstt = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            Dim query As ObjectQuery(Of EmployeeDTO)
            query = (From p In Context.HU_EMPLOYEE
                     From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                     From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                     From c In Context.HU_CONTRACT.Where(Function(c) c.ID = p.CONTRACT_ID)
                     From t In Context.HU_CONTRACT_TYPE.Where(Function(t) t.ID = c.CONTRACT_TYPE_ID)
                     Where _orgIds.Contains(p.ORG_ID) Order By p.EMPLOYEE_CODE
                        Select New EmployeeDTO With {
                         .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                         .ID = p.ID,
                         .FULLNAME_VN = p.FULLNAME_VN,
                         .FULLNAME_EN = p.FULLNAME_EN,
                         .ORG_ID = p.ORG_ID,
                         .ORG_NAME = org.NAME_VN,
                         .ORG_DESC = org.DESCRIPTION_PATH,
                         .TITLE_ID = p.TITLE_ID,
                         .JOIN_DATE = p.JOIN_DATE,
                         .TITLE_NAME_VN = title.NAME_VN,
                         .CONTRACT_TYPE_ID = c.CONTRACT_TYPE_ID,
                         .CONTRACT_TYPE_NAME = t.NAME,
                         .WORK_STATUS = p.WORK_STATUS,
                         .CONTRACT_NO = c.CONTRACT_NO})

            If _filter.CONTRACT_TYPE_ID IsNot Nothing Then
                query = query.Where(Function(p) p.CONTRACT_TYPE_ID = _filter.CONTRACT_TYPE_ID)
            End If

            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.EMPLOYEE_CODE.ToUpper().IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0 Or _
                                                p.FULLNAME_VN.ToUpper().IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0)
            End If

            If _filter.CONTRACT_NO <> "" Then
                query = query.Where(Function(p) p.CONTRACT_NO = _filter.CONTRACT_NO)
            End If

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")

            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy danh sách nhân viên ko phân trang bao gồm image để hiển thị lên org chart
    ''' </summary>
    ''' <param name="_orgIds"></param>
    ''' <param name="_filter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeOrgChart(ByVal lstOrg As List(Of Decimal), Optional ByVal log As UserLog = Nothing) As List(Of OrgChartDTO)

        Try
            Context.ExecuteStoreCommand("DELETE SE_CHOSEN_ORG WHERE USERNAME ='" & log.Username.ToUpper & "'")
            For Each i In lstOrg
                Dim obj = New SE_CHOSEN_ORG
                obj.ORG_ID = i
                obj.USERNAME = log.Username.ToUpper
                Context.SE_CHOSEN_ORG.AddObject(obj)
            Next
            Context.SaveChanges()
            Dim dateNow = Date.Now.Date
            Dim query = (From org In Context.HU_ORGANIZATION.Where(Function(f) f.CHK_ORGCHART = -1)
                         From p In Context.HU_EMPLOYEE.Where(Function(f) f.ID = org.REPRESENTATIVE_ID).DefaultIfEmpty
                         From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                         From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                         From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = org.ID And f.USERNAME = log.Username.ToUpper)
                         From orgcount In Context.HUV_ORGANIZATION_EMP_COUNT.Where(Function(f) f.ID = org.ID)
                         Where org.ACTFLG = "A" And (org.DISSOLVE_DATE Is Nothing Or _
                                                     (org.DISSOLVE_DATE IsNot Nothing And _
                                                      org.DISSOLVE_DATE > dateNow))
                         Order By p.EMPLOYEE_CODE
                         Select New OrgChartDTO With {
                             .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                             .FIRST_NAME_VN = p.FIRST_NAME_VN,
                             .LAST_NAME_VN = p.LAST_NAME_VN,
                             .FULLNAME_VN = p.FULLNAME_VN,
                             .TITLE_NAME_VN = title.NAME_VN,
                             .IMAGE = cv.IMAGE,
                             .ID = org.ID,
                             .ORG_NAME = org.NAME_VN,
                             .EMP_COUNT = orgcount.EMP_COUNT,
                             .PARENT_ID = org.PARENT_ID,
                             .MOBILE_PHONE = cv.MOBILE_PHONE,
                             .WORK_EMAIL = cv.WORK_EMAIL})

            Dim lstEmp = query.ToList

            For Each emp In lstEmp
                emp.ORG_NAME = emp.ORG_NAME & " (" + "Tổng số nhân viên:" & emp.EMP_COUNT & ")"
                emp.IMAGE_BINARY = GetEmployeeImage(emp.ID, "", False, emp.IMAGE)
            Next
            Return lstEmp
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


    Public Function GetListEmployeePortal(ByVal _filter As EmployeeDTO) As List(Of EmployeeDTO)
        Try
            Dim wstt = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID

            Dim query = From p In Context.HU_EMPLOYEE
                        Order By p.EMPLOYEE_CODE

            Dim lst = query.Select(Function(p) New EmployeeDTO With {
                                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                       .ID = p.ID,
                                       .FULLNAME_VN = p.FULLNAME_VN,
                                       .TER_EFFECT_DATE = p.TER_EFFECT_DATE,
                                       .DIRECT_MANAGER = p.DIRECT_MANAGER,
                                       .WORK_STATUS = p.WORK_STATUS})

            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                lst = lst.Where(Function(p) Not p.WORK_STATUS.HasValue Or _
                                    (p.WORK_STATUS.HasValue And _
                                     ((p.WORK_STATUS <> terID) Or (p.WORK_STATUS = terID And p.TER_EFFECT_DATE > dateNow))))

            End If

            If _filter.DIRECT_MANAGER IsNot Nothing Then
                lst = lst.Where(Function(p) p.DIRECT_MANAGER = _filter.DIRECT_MANAGER)
            End If

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' Lấy sanh sách nhân viên có phân trang
    ''' </summary>
    ''' <param name="PageIndex"></param>
    ''' <param name="PageSize"></param>
    ''' <param name="Total"></param>
    ''' <param name="_orgIds"></param>
    ''' <param name="_filter"></param>
    ''' <param name="Sorts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListEmployeePaging(ByVal _filter As EmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim fileDirectory = ""
            fileDirectory = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage"
            Dim wstt = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID

            Dim query = From p In Context.HU_EMPLOYEE
                        From pv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(F) p.STAFF_RANK_ID = F.ID).DefaultIfEmpty
                        From health In Context.HU_EMPLOYEE_HEALTH.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(f) p.WORK_STATUS = f.ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From c In Context.HU_CONTRACT.Where(Function(c) c.ID = p.CONTRACT_ID).DefaultIfEmpty
                        From ct In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
                        Order By p.EMPLOYEE_CODE

            Dim lst = query.Select(Function(p) New EmployeeDTO With {
                             .EMPLOYEE_CODE = p.p.EMPLOYEE_CODE,
                             .ID = p.p.ID,
                             .EMPLOYEE_ID = p.p.ID,
                             .DIRECT_MANAGER = p.p.DIRECT_MANAGER,
                             .FULLNAME_VN = p.p.FULLNAME_VN,
                             .FULLNAME_EN = p.p.FULLNAME_EN,
                             .ORG_ID = p.p.ORG_ID,
                             .ORG_NAME = p.org.NAME_VN,
                             .ORG_DESC = p.org.DESCRIPTION_PATH,
                             .TITLE_ID = p.p.TITLE_ID,
                             .TITLE_NAME_VN = p.title.NAME_VN,
                             .STAFF_RANK_ID = p.p.STAFF_RANK_ID,
                             .STAFF_RANK_NAME = p.s.NAME,
                             .CONTRACT_TYPE_ID = p.ct.ID,
                             .CONTRACT_TYPE_NAME = p.ct.NAME,
                             .TER_EFFECT_DATE = p.p.TER_EFFECT_DATE,
                             .WORK_STATUS = p.p.WORK_STATUS,
                             .WORK_STATUS_NAME = p.o.NAME_VN,
                             .JOIN_DATE = p.p.JOIN_DATE,
                             .JOIN_DATE_STATE = p.p.JOIN_DATE_STATE,
                             .LAST_WORKING_ID = p.p.LAST_WORKING_ID,
                             .CONTRACT_NO = p.p.HU_CONTRACT_NOW.CONTRACT_NO,
                             .CONTRACT_ID = p.p.CONTRACT_ID,
                             .ID_NO = p.pv.ID_NO,
                             .GHI_CHU_SUC_KHOE = p.health.GHI_CHU_SUC_KHOE,
                             .NHOM_MAU = p.health.NHOM_MAU,
                             .IMAGE = p.pv.IMAGE,
                             .ITIME_ID = p.p.ITIME_ID,
                             .MODIFIED_DATE = p.p.MODIFIED_DATE})

            If _filter.TITLE_ID IsNot Nothing Then
                lst = lst.Where(Function(p) p.TITLE_ID = _filter.TITLE_ID)
            End If
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                lst = lst.Where(Function(p) Not p.WORK_STATUS.HasValue Or _
                                    (p.WORK_STATUS.HasValue And _
                                     ((p.WORK_STATUS <> terID) Or (p.WORK_STATUS = terID And p.TER_EFFECT_DATE > dateNow))))

            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper().IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0)
            End If

            If _filter.FULLNAME_VN <> "" Then
                lst = lst.Where(Function(p) p.FULLNAME_VN.ToUpper().IndexOf(_filter.FULLNAME_VN.ToUpper) >= 0)
            End If

            If _filter.TITLE_NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.TITLE_NAME_VN.ToUpper().IndexOf(_filter.TITLE_NAME_VN.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper().IndexOf(_filter.ORG_NAME.ToUpper) >= 0)
            End If

            If _filter.STAFF_RANK_NAME <> "" Then
                lst = lst.Where(Function(p) p.STAFF_RANK_NAME.ToUpper().IndexOf(_filter.STAFF_RANK_NAME.ToUpper) >= 0)
            End If

            If _filter.GHI_CHU_SUC_KHOE <> "" Then
                lst = lst.Where(Function(p) p.GHI_CHU_SUC_KHOE.ToUpper().IndexOf(_filter.GHI_CHU_SUC_KHOE.ToUpper) >= 0)
            End If

            If _filter.JOIN_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE = _filter.JOIN_DATE)
            End If

            If _filter.JOIN_DATE_STATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE_STATE = _filter.JOIN_DATE_STATE)
            End If

            If _filter.FROM_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE >= _filter.FROM_DATE)
            End If

            If _filter.TO_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE <= _filter.TO_DATE)
            End If

            If _filter.WORK_STATUS_NAME <> "" Then
                lst = lst.Where(Function(p) p.WORK_STATUS_NAME.ToUpper().IndexOf(_filter.WORK_STATUS_NAME.ToUpper) >= 0)
            End If

            If _filter.ID_NO <> "" Then
                lst = lst.Where(Function(p) p.ID_NO.ToUpper().IndexOf(_filter.ID_NO.ToUpper) >= 0)
            End If

            If _filter.DIRECT_MANAGER IsNot Nothing Then
                lst = lst.Where(Function(p) p.DIRECT_MANAGER = _filter.DIRECT_MANAGER)
            End If

            If _filter.EMPLOYEE_ID IsNot Nothing Then
                lst = lst.Where(Function(p) p.ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.MustHaveContract Then
                lst = lst.Where(Function(p) p.CONTRACT_ID.HasValue)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Dim lstEmp = lst.ToList

            For Each emp In lstEmp
                emp.IMAGE_BINARY = GetEmployeeImage(emp.ID, "", False, emp.IMAGE)
            Next
            Return lstEmp

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetChartEmployee(ByVal _filter As EmployeeDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim fileDirectory = ""
            fileDirectory = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage"
            Dim wstt = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID

            Dim query = From p In Context.HU_EMPLOYEE
                        From pv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(F) p.STAFF_RANK_ID = F.ID).DefaultIfEmpty
                        From health In Context.HU_EMPLOYEE_HEALTH.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(f) p.WORK_STATUS = f.ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From ot In Context.HU_ORG_TITLE.Where(Function(f) f.TITLE_ID = title.ID And f.ORG_ID = org.ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From c In Context.HU_CONTRACT.Where(Function(c) c.ID = p.CONTRACT_ID).DefaultIfEmpty
                        From ct In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
                        Order By p.EMPLOYEE_CODE

            Dim lst = query.Select(Function(p) New EmployeeDTO With {
                             .EMPLOYEE_CODE = p.p.EMPLOYEE_CODE,
                             .ID = p.p.ID,
                             .EMPLOYEE_ID = p.p.ID,
                             .DIRECT_MANAGER = p.p.DIRECT_MANAGER,
                             .FULLNAME_VN = p.p.FULLNAME_VN,
                             .FULLNAME_EN = p.p.FULLNAME_EN,
                             .ORG_ID = p.p.ORG_ID,
                             .ORG_NAME = p.org.NAME_VN,
                             .ORG_DESC = p.org.DESCRIPTION_PATH,
                             .TITLE_ID = p.p.TITLE_ID,
                             .TITLE_NAME_VN = p.title.NAME_VN,
                             .STAFF_RANK_ID = p.p.STAFF_RANK_ID,
                             .STAFF_RANK_NAME = p.s.NAME,
                             .CONTRACT_TYPE_ID = p.ct.ID,
                             .CONTRACT_TYPE_NAME = p.ct.NAME,
                             .TER_EFFECT_DATE = p.p.TER_EFFECT_DATE,
                             .WORK_STATUS = p.p.WORK_STATUS,
                             .WORK_STATUS_NAME = p.o.NAME_VN,
                             .JOIN_DATE = p.p.JOIN_DATE,
                             .JOIN_DATE_STATE = p.p.JOIN_DATE_STATE,
                             .LAST_WORKING_ID = p.p.LAST_WORKING_ID,
                             .CONTRACT_NO = p.p.HU_CONTRACT_NOW.CONTRACT_NO,
                             .ID_NO = p.pv.ID_NO,
                             .GHI_CHU_SUC_KHOE = p.health.GHI_CHU_SUC_KHOE,
                             .NHOM_MAU = p.health.NHOM_MAU,
                             .IMAGE = p.pv.IMAGE,
                             .ITIME_ID = p.p.ITIME_ID,
                             .MODIFIED_DATE = p.p.MODIFIED_DATE,
                             .PARENT_ID = p.ot.PARENT_ID
                             })
            ',            .PARENT_ID = p.ot.PARENT_ID
            If _filter.TITLE_ID IsNot Nothing Then
                lst = lst.Where(Function(p) p.TITLE_ID = _filter.TITLE_ID)
            End If
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                lst = lst.Where(Function(p) Not p.WORK_STATUS.HasValue Or _
                                    (p.WORK_STATUS.HasValue And _
                                     ((p.WORK_STATUS <> terID) Or (p.WORK_STATUS = terID And p.TER_EFFECT_DATE > dateNow))))

            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper().IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0)
            End If

            If _filter.FULLNAME_VN <> "" Then
                lst = lst.Where(Function(p) p.FULLNAME_VN.ToUpper().IndexOf(_filter.FULLNAME_VN.ToUpper) >= 0)
            End If

            If _filter.TITLE_NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.TITLE_NAME_VN.ToUpper().IndexOf(_filter.TITLE_NAME_VN.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper().IndexOf(_filter.ORG_NAME.ToUpper) >= 0)
            End If

            If _filter.STAFF_RANK_NAME <> "" Then
                lst = lst.Where(Function(p) p.STAFF_RANK_NAME.ToUpper().IndexOf(_filter.STAFF_RANK_NAME.ToUpper) >= 0)
            End If

            If _filter.GHI_CHU_SUC_KHOE <> "" Then
                lst = lst.Where(Function(p) p.GHI_CHU_SUC_KHOE.ToUpper().IndexOf(_filter.GHI_CHU_SUC_KHOE.ToUpper) >= 0)
            End If

            If _filter.JOIN_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE = _filter.JOIN_DATE)
            End If

            If _filter.JOIN_DATE_STATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE_STATE = _filter.JOIN_DATE_STATE)
            End If

            If _filter.FROM_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE >= _filter.FROM_DATE)
            End If

            If _filter.TO_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE <= _filter.TO_DATE)
            End If

            If _filter.WORK_STATUS_NAME <> "" Then
                lst = lst.Where(Function(p) p.WORK_STATUS_NAME.ToUpper().IndexOf(_filter.WORK_STATUS_NAME.ToUpper) >= 0)
            End If

            If _filter.ID_NO <> "" Then
                lst = lst.Where(Function(p) p.ID_NO.ToUpper().IndexOf(_filter.ID_NO.ToUpper) >= 0)
            End If

            If _filter.DIRECT_MANAGER IsNot Nothing Then
                lst = lst.Where(Function(p) p.DIRECT_MANAGER = _filter.DIRECT_MANAGER)
            End If

            If _filter.EMPLOYEE_ID IsNot Nothing Then
                lst = lst.Where(Function(p) p.ID = _filter.EMPLOYEE_ID)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Dim lstEmp = lst.ToList

            For Each emp In lstEmp
                emp.IMAGE_BINARY = GetEmployeeImage(emp.ID, "", False, emp.IMAGE)
            Next
            Return lstEmp

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetListEmployeeChart(ByVal _filter As EmployeeDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO)
        Try
            Dim fileDirectory = ""
            fileDirectory = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage"
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim wstt = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID

            Dim query = From p In Context.HU_EMPLOYEE
                        From pv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(F) p.STAFF_RANK_ID = F.ID).DefaultIfEmpty
                        From health In Context.HU_EMPLOYEE_HEALTH.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(f) p.WORK_STATUS = f.ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From c In Context.HU_CONTRACT.Where(Function(c) c.ID = p.CONTRACT_ID).DefaultIfEmpty
                        From ct In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
                        Order By p.EMPLOYEE_CODE

            Dim lst = query.Select(Function(p) New EmployeeDTO With {
                             .EMPLOYEE_CODE = p.p.EMPLOYEE_CODE,
                             .ID = p.p.ID,
                             .EMPLOYEE_ID = p.p.ID,
                             .DIRECT_MANAGER = p.p.DIRECT_MANAGER,
                             .FULLNAME_VN = p.p.FULLNAME_VN,
                             .FULLNAME_EN = p.p.FULLNAME_EN,
                             .ORG_ID = p.p.ORG_ID,
                             .ORG_NAME = p.org.NAME_VN,
                             .ORG_DESC = p.org.DESCRIPTION_PATH,
                             .TITLE_ID = p.p.TITLE_ID,
                             .TITLE_NAME_VN = p.title.NAME_VN,
                             .STAFF_RANK_ID = p.p.STAFF_RANK_ID,
                             .STAFF_RANK_NAME = p.s.NAME,
                             .CONTRACT_TYPE_ID = p.ct.ID,
                             .CONTRACT_TYPE_NAME = p.ct.NAME,
                             .TER_EFFECT_DATE = p.p.TER_EFFECT_DATE,
                             .WORK_STATUS = p.p.WORK_STATUS,
                             .WORK_STATUS_NAME = p.o.NAME_VN,
                             .JOIN_DATE = p.p.JOIN_DATE,
                             .JOIN_DATE_STATE = p.p.JOIN_DATE_STATE,
                             .LAST_WORKING_ID = p.p.LAST_WORKING_ID,
                             .CONTRACT_NO = p.p.HU_CONTRACT_NOW.CONTRACT_NO,
                             .ID_NO = p.pv.ID_NO,
                             .GHI_CHU_SUC_KHOE = p.health.GHI_CHU_SUC_KHOE,
                             .IMAGE = fileDirectory & "\" & If(p.pv.IMAGE.Trim().Length > 0, p.pv.IMAGE, "\NoImage.jpg"),
                             .ITIME_ID = p.p.ITIME_ID,
                             .MODIFIED_DATE = p.p.MODIFIED_DATE})
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy thông tin nhân viên từ EmployeeCode
    ''' </summary>
    ''' <param name="sEmployeeCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeByEmployeeID(ByVal empID As Decimal) As EmployeeDTO
        Try
            Try

                If empID = 0 Then Return Nothing
                Dim query As New EmployeeDTO
                query =
                    (From e In Context.HU_EMPLOYEE
                     From title In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID)
                     From direct In Context.HU_EMPLOYEE.Where(Function(f) f.ID = e.DIRECT_MANAGER).DefaultIfEmpty
                     From level In Context.HU_EMPLOYEE.Where(Function(f) f.ID = e.LEVEL_MANAGER).DefaultIfEmpty
                     From obj In Context.PA_OBJECT_SALARY.Where(Function(f) f.ID = e.PA_OBJECT_SALARY_ID).DefaultIfEmpty
                     From staffRank In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                     From c In Context.HU_CONTRACT.Where(Function(c) c.ID = e.CONTRACT_ID).DefaultIfEmpty
                     From org In Context.HU_ORGANIZATION.Where(Function(t) t.ID = e.ORG_ID).DefaultIfEmpty
                     From t In Context.HU_CONTRACT_TYPE.Where(Function(t) t.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
                     From titlegroup In Context.OT_OTHER_LIST.Where(Function(f) f.ID = title.TITLE_GROUP_ID And
                                                                        f.TYPE_ID = 2000).DefaultIfEmpty
                     From workstatus In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.WORK_STATUS And
                                                                        f.TYPE_ID = 59).DefaultIfEmpty
                     From empstatus In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.EMP_STATUS And
                                                                        f.TYPE_ID = 2235).DefaultIfEmpty
                     Where e.ID = empID
                     Select New EmployeeDTO With {
                         .ID = e.ID,
                         .FIRST_NAME_EN = e.FIRST_NAME_EN,
                         .FIRST_NAME_VN = e.FIRST_NAME_VN,
                         .LAST_NAME_EN = e.LAST_NAME_EN,
                         .LAST_NAME_VN = e.LAST_NAME_VN,
                         .FULLNAME_EN = e.FULLNAME_EN,
                         .FULLNAME_VN = e.FULLNAME_VN,
                         .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                         .EMPLOYEE_CODE_OLD = e.EMPLOYEE_CODE_OLD,
                         .BOOKNO = e.BOOK_NO,
                         .EMPLOYEE_NAME_OTHER = e.EMPLOYEE_NAME_OTHER,
                         .IMAGE = e.HU_EMPLOYEE_CV.IMAGE,
                         .TITLE_ID = e.TITLE_ID,
                         .LAST_WORKING_ID = e.LAST_WORKING_ID,
                         .TITLE_NAME_EN = title.NAME_EN,
                         .TITLE_NAME_VN = title.NAME_VN,
                         .TITLE_GROUP_NAME = titlegroup.NAME_VN,
                         .ORG_ID = e.ORG_ID,
                         .ORG_NAME = org.NAME_VN,
                         .ORG_DESC = org.DESCRIPTION_PATH,
                         .CONTRACT_ID = e.CONTRACT_ID,
                         .WORK_STATUS = e.WORK_STATUS,
                         .WORK_STATUS_NAME = workstatus.NAME_VN,
                         .EMP_STATUS = e.EMP_STATUS,
                         .EMP_STATUS_NAME = empstatus.NAME_VN,
                         .DIRECT_MANAGER = e.DIRECT_MANAGER,
                         .DIRECT_MANAGER_NAME = direct.FULLNAME_VN,
                         .LEVEL_MANAGER = e.LEVEL_MANAGER,
                         .LEVEL_MANAGER_NAME = level.FULLNAME_VN,
                         .JOIN_DATE = e.JOIN_DATE,
                         .JOIN_DATE_STATE = e.JOIN_DATE_STATE,
                         .CONTRACT_TYPE_NAME = t.NAME,
                         .CONTRACT_NO = e.HU_CONTRACT_NOW.CONTRACT_NO,
                         .CONTRACT_EFFECT_DATE = e.HU_CONTRACT_NOW.START_DATE,
                         .CONTRACT_EXPIRE_DATE = e.HU_CONTRACT_NOW.EXPIRE_DATE,
                         .PA_OBJECT_SALARY_ID = e.PA_OBJECT_SALARY_ID,
                         .PA_OBJECT_SALARY_NAME = obj.NAME_VN,
                         .STAFF_RANK_ID = e.STAFF_RANK_ID,
                         .STAFF_RANK_NAME = staffRank.NAME,
                         .ITIME_ID = e.ITIME_ID,
                .TER_EFFECT_DATE = e.TER_EFFECT_DATE
                     }).FirstOrDefault
                WriteExceptionLog(Nothing, "Getmployee1", "iProfile")
                Dim emp As New EmployeeDTO
                emp = query
                WriteExceptionLog(Nothing, "Getmployee2", "iProfile")
                If (emp IsNot Nothing) Then
                    emp.lstPaper = (From p In Context.HU_EMPLOYEE_PAPER
                                    Where p.EMPLOYEE_ID = emp.ID
                                    Select p.HU_PAPER_ID.Value).ToList
                    emp.lstPaperFiled = (From p In Context.HU_EMPLOYEE_PAPER_FILED
                                         Where p.EMPLOYEE_ID = emp.ID
                                         Select p.HU_PAPER_ID.Value).ToList
                End If
                Return emp
            Catch ex As Exception
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
                Throw ex
            End Try
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Hàm đọc ảnh hồ sơ của nhân viên thành binary
    ''' </summary>
    ''' <param name="gEmpID"></param>
    ''' <param name="sError"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeImage(ByVal gEmpID As Decimal, ByRef sError As String,
                                     Optional ByVal isOneEmployee As Boolean = True,
                                     Optional ByVal img_link As String = "") As Byte()
        Try


            Dim sEmployeeImage As String = ""
            If Not isOneEmployee Then
                sEmployeeImage = img_link
            Else
                sEmployeeImage = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = gEmpID _
                                  Select p.IMAGE).FirstOrDefault
            End If

            Dim _imageBinary() As Byte = Nothing
            Dim fileDirectory = ""
            Dim filepath = ""
            Dim filepathDefault = ""
            fileDirectory = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage"
            Dim _fileInfo As IO.FileInfo
            If sEmployeeImage IsNot Nothing AndAlso sEmployeeImage.Trim().Length > 0 Then
                filepath = fileDirectory & "\" & sEmployeeImage
            Else
                filepath = fileDirectory & "\NoImage.jpg"
            End If
            filepathDefault = fileDirectory & "\NoImage.jpg"
            'Kiểm tra file có tồn tại ko
            If File.Exists(filepath) Then
                _fileInfo = New FileInfo(filepath)
            Else
                _fileInfo = New FileInfo(filepathDefault) 'Nếu ko có thì lấy ảnh mặc định
            End If

            Dim _FStream As New IO.FileStream(_fileInfo.FullName, IO.FileMode.Open, IO.FileAccess.Read)
            Dim _NumBytes As Long = _fileInfo.Length
            Dim _BinaryReader As New IO.BinaryReader(_FStream)
            _imageBinary = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
            _fileInfo = Nothing
            _NumBytes = 0
            _FStream.Close()
            _FStream.Dispose()
            _BinaryReader.Close()
            Return _imageBinary
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Thêm mới nhân viên
    ''' </summary>
    ''' <param name="objEmp"></param>
    ''' <param name="log"></param>
    ''' <param name="gID"></param>
    ''' <param name="objEmpCV">Thông tin bảng HU_EMPLOYEE_CV</param>
    ''' <param name="objEmpSalary">Thông tin bảng HU_EMPLOYEE_SALARY</param>
    ''' <param name="objEmpEdu">Thông tin bảng HU_EMPLOYEE_EDUCATION</param>
    ''' <param name="objEmpOther">Thông tin bảng HU_EMPLOYEE_OTHER_INFO</param>
    ''' <param name="objEmpHealth">Thông tin bảng HU_EMPLOYEE_HEALTH</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertEmployee(ByVal objEmp As EmployeeDTO, ByVal log As UserLog, ByRef gID As Decimal,
                                        ByRef _strEmpCode As String,
                                        ByVal _imageBinary As Byte(),
                                        Optional ByVal objEmpCV As EmployeeCVDTO = Nothing, _
                                        Optional ByVal objEmpEdu As EmployeeEduDTO = Nothing, _
                                        Optional ByVal objEmpHealth As EmployeeHealthDTO = Nothing) As Boolean

        Try
            'Thông tin insert vào bảng HU_EMPLOYEE.
            Dim objEmpData As New HU_EMPLOYEE
            Dim EMPCODE As String = String.Empty
            If EMPCODE.Length = 4 Then
                objEmpData.ITIME_ID = EMPCODE.Substring(1)
            End If

            objEmpData.ID = Utilities.GetNextSequence(Context, Context.HU_EMPLOYEE.EntitySet.Name)
            'Sinh mã nhân viên động
            Dim empCodeDB As Double = (From p In Context.HU_EMPLOYEE Order By p.EMPLOYEE_CODE Descending Select p.EMPLOYEE_CODE).FirstOrDefault
            Dim checkEMP As Integer = 0

            Do
                empCodeDB += 1
                EMPCODE = String.Format("{0}", Format(empCodeDB, "00000"))
                checkEMP = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = EMPCODE Select p.ID).Count
            Loop Until checkEMP = 0

            objEmpData.EMPLOYEE_CODE = EMPCODE
            _strEmpCode = EMPCODE
            objEmpData.EMPLOYEE_NAME_OTHER = objEmp.EMPLOYEE_NAME_OTHER
            objEmpData.EMPLOYEE_CODE_OLD = objEmp.EMPLOYEE_CODE_OLD
            objEmpData.BOOK_NO = objEmp.BOOKNO
            objEmpData.FIRST_NAME_VN = objEmp.FIRST_NAME_VN
            objEmpData.LAST_NAME_VN = objEmp.LAST_NAME_VN
            objEmpData.FIRST_NAME_EN = objEmp.FIRST_NAME_EN
            objEmpData.LAST_NAME_EN = objEmp.LAST_NAME_EN
            objEmpData.FULLNAME_EN = objEmpData.FIRST_NAME_EN & " " & objEmpData.LAST_NAME_EN
            objEmpData.FULLNAME_VN = objEmpData.FIRST_NAME_VN & " " & objEmpData.LAST_NAME_VN
            objEmpData.ORG_ID = objEmp.ORG_ID
            objEmpData.WORK_STATUS = objEmp.WORK_STATUS
            objEmpData.TITLE_ID = objEmp.TITLE_ID
            objEmpData.DIRECT_MANAGER = objEmp.DIRECT_MANAGER
            objEmpData.LEVEL_MANAGER = objEmp.LEVEL_MANAGER
            objEmpData.STAFF_RANK_ID = objEmp.STAFF_RANK_ID
            objEmpData.ITIME_ID = objEmp.ITIME_ID
            objEmpData.PA_OBJECT_SALARY_ID = 1 
            Context.HU_EMPLOYEE.AddObject(objEmpData)
            'End Thông tin insert vào bảng HU_EMPLOYEE.

            ' Insert bảng HU_EMPLOYEE_PAPER

            If objEmp.lstPaper IsNot Nothing AndAlso objEmp.lstPaper.Count > 0 Then
                For Each i In objEmp.lstPaper
                    Dim objEmpPaperData As New HU_EMPLOYEE_PAPER
                    objEmpPaperData.ID = Utilities.GetNextSequence(Context, Context.HU_EMPLOYEE_PAPER.EntitySet.Name)
                    objEmpPaperData.HU_PAPER_ID = i
                    objEmpPaperData.EMPLOYEE_ID = objEmpData.ID
                    Context.HU_EMPLOYEE_PAPER.AddObject(objEmpPaperData)
                Next
            End If
            If objEmp.lstPaperFiled IsNot Nothing AndAlso objEmp.lstPaperFiled.Count > 0 Then
                For Each i In objEmp.lstPaperFiled
                    Dim objEmpPaperData As New HU_EMPLOYEE_PAPER_FILED
                    objEmpPaperData.ID = Utilities.GetNextSequence(Context, Context.HU_EMPLOYEE_PAPER_FILED.EntitySet.Name)
                    objEmpPaperData.HU_PAPER_ID = i
                    objEmpPaperData.EMPLOYEE_ID = objEmpData.ID
                    Context.HU_EMPLOYEE_PAPER_FILED.AddObject(objEmpPaperData)
                Next
            End If
            'Start thông tin insert vào bảng HU_EMPLOYEE_CV
            Dim objEmpCVData As New HU_EMPLOYEE_CV

            If objEmpCV IsNot Nothing Then
                objEmpCVData.EMPLOYEE_ID = objEmpData.ID 'Khóa ngoại vừa mới tạo 
                objEmpCVData.GENDER = objEmpCV.GENDER
                If objEmpCV.IMAGE <> "" Then
                    objEmpCVData.IMAGE = objEmp.EMPLOYEE_CODE & objEmpCV.IMAGE 'Lưu Image thành dạng E10012.jpg.                    
                End If
                objEmpCVData.BIRTH_DATE = objEmpCV.BIRTH_DATE
                objEmpCVData.BIRTH_PLACE = objEmpCV.BIRTH_PLACE
                objEmpCVData.MARITAL_STATUS = objEmpCV.MARITAL_STATUS
                objEmpCVData.RELIGION = objEmpCV.RELIGION
                objEmpCVData.NATIVE = objEmpCV.NATIVE
                objEmpCVData.NATIONALITY = objEmpCV.NATIONALITY
                objEmpCVData.PER_ADDRESS = objEmpCV.PER_ADDRESS
                objEmpCVData.PER_PROVINCE = objEmpCV.PER_PROVINCE
                objEmpCVData.PER_DISTRICT = objEmpCV.PER_DISTRICT
                objEmpCVData.WORKPLACE_ID = objEmpCV.WORKPLACE_ID
                objEmpCVData.INS_REGION_ID = objEmpCV.INS_REGION_ID
                objEmpCVData.PER_WARD = objEmpCV.PER_WARD
                objEmpCVData.HOME_PHONE = objEmpCV.HOME_PHONE
                objEmpCVData.MOBILE_PHONE = objEmpCV.MOBILE_PHONE
                objEmpCVData.ID_NO = objEmpCV.ID_NO
                
                objEmpCVData.ID_DATE = objEmpCV.ID_DATE
                objEmpCVData.ID_PLACE = objEmpCV.ID_PLACE
                objEmpCVData.PASS_NO = objEmpCV.PASS_NO
                objEmpCVData.PASS_DATE = objEmpCV.PASS_DATE
                objEmpCVData.PASS_EXPIRE = objEmpCV.PASS_EXPIRE
                objEmpCVData.PASS_PLACE = objEmpCV.PASS_PLACE
                objEmpCVData.VISA = objEmpCV.VISA
                objEmpCVData.VISA_DATE = objEmpCV.VISA_DATE
                objEmpCVData.VISA_EXPIRE = objEmpCV.VISA_EXPIRE
                objEmpCVData.VISA_PLACE = objEmpCV.VISA_PLACE
                objEmpCVData.WORK_PERMIT = objEmpCV.WORK_PERMIT
                objEmpCVData.WORK_PERMIT_DATE = objEmpCV.WORK_PERMIT_DATE
                objEmpCVData.WORK_PERMIT_EXPIRE = objEmpCV.WORK_PERMIT_EXPIRE
                objEmpCVData.WORK_PERMIT_PLACE = objEmpCV.WORK_PERMIT_PLACE
                objEmpCVData.WORK_EMAIL = objEmpCV.WORK_EMAIL
                objEmpCVData.NAV_ADDRESS = objEmpCV.NAV_ADDRESS
                objEmpCVData.NAV_PROVINCE = objEmpCV.NAV_PROVINCE
                objEmpCVData.NAV_DISTRICT = objEmpCV.NAV_DISTRICT
                objEmpCVData.NAV_WARD = objEmpCV.NAV_WARD
                objEmpCVData.PIT_CODE = objEmpCV.PIT_CODE
                objEmpCVData.PER_EMAIL = objEmpCV.PER_EMAIL
                objEmpCVData.CONTACT_PER = objEmpCV.CONTACT_PER
                objEmpCVData.CONTACT_PER_PHONE = objEmpCV.CONTACT_PER_PHONE
                objEmpCVData.CAREER = objEmpCV.CAREER
                objEmpCVData.NGAY_VAO_DOAN = objEmpCV.NGAY_VAO_DOAN
                objEmpCVData.NOI_VAO_DOAN = objEmpCV.NOI_VAO_DOAN
                objEmpCVData.CHUC_VU_DOAN = objEmpCV.CHUC_VU_DOAN
                objEmpCVData.DOAN_PHI = objEmpCV.DOAN_PHI
                objEmpCVData.NGAY_VAO_DANG = objEmpCV.NGAY_VAO_DANG
                objEmpCVData.NOI_VAO_DANG = objEmpCV.NOI_VAO_DANG
                objEmpCVData.CHUC_VU_DANG = objEmpCV.CHUC_VU_DANG
                objEmpCVData.DANG_PHI = objEmpCV.DANG_PHI
                objEmpCVData.BANK_ID = objEmpCV.BANK_ID
                objEmpCVData.BANK_BRANCH_ID = objEmpCV.BANK_BRANCH_ID
                objEmpCVData.BANK_NO = objEmpCV.BANK_NO
                objEmpCVData.IS_PERMISSION = objEmpCV.IS_PERMISSION
                objEmpCVData.IS_PAY_BANK = objEmpCV.IS_PAY_BANK
                '-----------------------------------------------
                objEmpCVData.PROVINCENQ_ID = objEmpCV.PROVINCENQ_ID
                objEmpCVData.OPPTION1 = objEmpCV.OPPTION1
                objEmpCVData.OPPTION2 = objEmpCV.OPPTION2
                objEmpCVData.OPPTION3 = objEmpCV.OPPTION3
                objEmpCVData.OPPTION4 = objEmpCV.OPPTION4
                objEmpCVData.OPPTION5 = objEmpCV.OPPTION5
                objEmpCVData.OPPTION6 = objEmpCV.OPPTION6
                objEmpCVData.OPPTION7 = objEmpCV.OPPTION7
                objEmpCVData.OPPTION8 = objEmpCV.OPPTION8
                objEmpCVData.OPPTION9 = objEmpCV.OPPTION9
                objEmpCVData.OPPTION10 = objEmpCV.OPPTION10
                objEmpCVData.PROVINCEEMP_ID = objEmpCV.PROVINCEEMP_ID
                objEmpCVData.DISTRICTEMP_ID = objEmpCV.DISTRICTEMP_ID
                objEmpCVData.WARDEMP_ID = objEmpCV.WARDEMP_ID

                objEmpCVData.HANG_THUONG_BINH = objEmpCV.HANG_THUONG_BINH
                objEmpCVData.THUONG_BINH = objEmpCV.THUONG_BINH
                objEmpCVData.DV_XUAT_NGU_QD = objEmpCV.DV_XUAT_NGU_QD
                objEmpCVData.NGAY_XUAT_NGU_QD = objEmpCV.NGAY_XUAT_NGU_QD
                objEmpCVData.NGAY_NHAP_NGU_QD = objEmpCV.NGAY_NHAP_NGU_QD
                objEmpCVData.QD = objEmpCV.QD
                objEmpCVData.DV_XUAT_NGU_CA = objEmpCV.DV_XUAT_NGU_CA
                objEmpCVData.NGAY_XUAT_NGU_CA = objEmpCV.NGAY_XUAT_NGU_CA
                objEmpCVData.NGAY_NHAP_NGU_CA = objEmpCV.NGAY_NHAP_NGU_CA
                objEmpCVData.NGAY_TG_BAN_NU_CONG = objEmpCV.NGAY_TG_BAN_NU_CONG
                objEmpCVData.CV_BAN_NU_CONG = objEmpCV.CV_BAN_NU_CONG
                objEmpCVData.NU_CONG = objEmpCV.NU_CONG
                objEmpCVData.NGAY_TG_BANTT = objEmpCV.NGAY_TG_BANTT
                objEmpCVData.CV_BANTT = objEmpCV.CV_BANTT
                objEmpCVData.CONG_DOAN = objEmpCV.CONG_DOAN
                objEmpCVData.CA = objEmpCV.CA
                objEmpCVData.DANG = objEmpCV.DANG
                objEmpCVData.SKILL = objEmpCV.SKILL
                objEmpCVData.NGAY_VAO_DANG_DB = objEmpCV.NGAY_VAO_DANG_DB
                objEmpCVData.NGAY_VAO_DANG = objEmpCV.NGAY_VAO_DANG
                objEmpCVData.CHUC_VU_DANG = objEmpCV.CHUC_VU_DANG
                objEmpCVData.DOAN_PHI = objEmpCV.DOAN_PHI
                objEmpCVData.CHUC_VU_DOAN = objEmpCV.CHUC_VU_DOAN
                objEmpCVData.NGAY_VAO_DOAN = objEmpCV.NGAY_VAO_DOAN
                objEmpCVData.GD_CHINH_SACH = objEmpCV.GD_CHINH_SACH
                
                '-----------------------------------------------

                Context.HU_EMPLOYEE_CV.AddObject(objEmpCVData)

            End If

            'End thông tin insert vào bảng HU_EMPLOYEE_CV

            'Start thông tin insert vào bảng HU_EMPLOYEE_EDUCATION
            If objEmpEdu IsNot Nothing Then
                Dim objEmpEduData As New HU_EMPLOYEE_EDUCATION
                objEmpEduData.EMPLOYEE_ID = objEmpData.ID
                objEmpEduData.ACADEMY = objEmpEdu.ACADEMY
                objEmpEduData.MAJOR = objEmpEdu.MAJOR
                objEmpEduData.MAJOR_REMARK = objEmpEdu.MAJOR_REMARK
                objEmpEduData.LANGUAGE = objEmpEdu.LANGUAGE
                objEmpEduData.LANGUAGE_LEVEL = objEmpEdu.LANGUAGE_LEVEL
                objEmpEduData.LANGUAGE_MARK = objEmpEdu.LANGUAGE_MARK
                objEmpEduData.GRADUATE_SCHOOL_ID = objEmpEdu.GRADUATE_SCHOOL_ID
                objEmpEduData.TRAINING_FORM = objEmpEdu.TRAINING_FORM
                objEmpEduData.LEARNING_LEVEL = objEmpEdu.LEARNING_LEVEL
                objEmpEduData.GRADUATION_YEAR = objEmpEdu.GRADUATION_YEAR
                objEmpEduData.QLNN = objEmpEdu.QLNN
                objEmpEduData.LLCT = objEmpEdu.LLCT
                objEmpEduData.TDTH = objEmpEdu.TDTH
                objEmpEduData.DIEM_XLTH = objEmpEdu.DIEM_XLTH
                Context.HU_EMPLOYEE_EDUCATION.AddObject(objEmpEduData)
            End If


            'End thông tin insert vào bảng HU_EMPLOYEE_EDUCATION

            'Start thông tin insert vào bảng HU_EMPLOYEE_HEALTH
            If objEmpHealth IsNot Nothing Then
                Dim objEmpHealthData As New HU_EMPLOYEE_HEALTH
                objEmpHealthData.EMPLOYEE_ID = objEmpData.ID
                objEmpHealthData.CHIEU_CAO = objEmpHealth.CHIEU_CAO
                objEmpHealthData.CAN_NANG = objEmpHealth.CAN_NANG
                objEmpHealthData.NHOM_MAU = objEmpHealth.NHOM_MAU
                objEmpHealthData.HUYET_AP = objEmpHealth.HUYET_AP
                objEmpHealthData.MAT_TRAI = objEmpHealth.MAT_TRAI
                objEmpHealthData.MAT_PHAI = objEmpHealth.MAT_PHAI
                objEmpHealthData.LOAI_SUC_KHOE = objEmpHealth.LOAI_SUC_KHOE
                objEmpHealthData.TAI_MUI_HONG = objEmpHealth.TAI_MUI_HONG
                objEmpHealthData.RANG_HAM_MAT = objEmpHealth.RANG_HAM_MAT
                objEmpHealthData.TIM = objEmpHealth.TIM
                objEmpHealthData.PHOI_NGUC = objEmpHealth.PHOI_NGUC
                objEmpHealthData.VIEM_GAN_B = objEmpHealth.VIEM_GAN_B
                objEmpHealthData.DA_HOA_LIEU = objEmpHealth.DA_HOA_LIEU
                objEmpHealthData.GHI_CHU_SUC_KHOE = objEmpHealth.GHI_CHU_SUC_KHOE
                objEmpHealthData.TTSUCKHOE = objEmpHealth.TTSUCKHOE
                Context.HU_EMPLOYEE_HEALTH.AddObject(objEmpHealthData)
            End If

            'Ghi ảnh vào thư mục
            If _imageBinary IsNot Nothing AndAlso _imageBinary.Length > 0 Then
                Dim savepath = ""
                savepath = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage"
                If Not Directory.Exists(savepath) Then
                    Directory.CreateDirectory(savepath)
                End If
                'Xóa ảnh cũ của nhân viên.
                Dim sFile() As String = Directory.GetFiles(savepath, objEmpCVData.IMAGE)
                For Each s In sFile
                    File.Delete(s)
                Next
                Dim ms As New MemoryStream(_imageBinary)
                ' instance a filestream pointing to the
                ' storatge folder, use the original file name
                ' to name the resulting file

                Dim fs As New FileStream(savepath & "\" & objEmpCVData.IMAGE, FileMode.Create)
                ms.WriteTo(fs)

                ' clean up
                ms.Close()
                fs.Close()
                fs.Dispose()
            End If
            Context.SaveChanges(log)
            'Dim user = (From p In Context.SE_USER Where p.EMPLOYEE_CODE = EMPCODE).FirstOrDefault
            'If objEmpCV.WORK_EMAIL <> "" Then
            '    If user Is Nothing Then
            '        Dim _new As New SE_USER
            '        Dim EncryptData As New EncryptData
            '        _new.ID = Utilities.GetNextSequence(Context, Context.SE_USER.EntitySet.Name)
            '        _new.EFFECT_DATE = Date.Now
            '        _new.EMPLOYEE_CODE = EMPCODE
            '        _new.FULLNAME = objEmpData.FULLNAME_VN
            '        _new.EMAIL = objEmpCVData.WORK_EMAIL
            '        _new.TELEPHONE = objEmpCVData.MOBILE_PHONE
            '        _new.IS_AD = True
            '        _new.IS_APP = False
            '        _new.IS_PORTAL = True
            '        _new.IS_CHANGE_PASS = "-1"
            '        _new.ACTFLG = "A"
            '        _new.PASSWORD = EncryptData.EncryptString(_strEmpCode)
            '        _new.USERNAME = objEmpCV.WORK_EMAIL.ToUpper
            '        Context.SE_USER.AddObject(_new)
            '    End If
            'End If
            'Context.SaveChanges(log)
            gID = objEmpData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Sửa thông tin nhân viên
    ''' </summary>
    ''' <param name="objEmp"></param>
    ''' <param name="log"></param>
    ''' <param name="gID"></param>
    ''' <param name="objEmpCV"></param>
    ''' <param name="objEmpEdu"></param>
    ''' <param name="objEmpOther"></param>
    ''' <param name="objEmpSalary"></param>
    ''' <param name="objEmpHealth"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ModifyEmployee(ByVal objEmp As EmployeeDTO, ByVal log As UserLog, ByRef gID As Decimal, _
                                        ByVal _imageBinary As Byte(), _
                                        Optional ByVal objEmpCV As EmployeeCVDTO = Nothing, _
                                        Optional ByVal objEmpEdu As EmployeeEduDTO = Nothing, _
                                        Optional ByVal objEmpHealth As EmployeeHealthDTO = Nothing) As Boolean

        Try
            Dim dateChange = DateTime.Now
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Try
                            'Open connection
                            cmd.Connection = conn
                            cmd.CommandType = CommandType.StoredProcedure
                            conn.Open()
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            If objEmp.IS_HISTORY Then
                                cmd.CommandText = "PKG_PROFILE_BUSINESS.INSERT_EMP_HISTORY"

                                Dim obj = New With {.P_EMPLOYEE_ID = objEmp.ID,
                                                    .P_USER_BY = log.Username,
                                                    .P_USER_LOG = log.Ip & "-" & log.ComputerName,
                                                    .P_DATE_CHANGE = dateChange,
                                                    .P_TYPE_CHANGE = 1}

                                'Add parameter
                                If obj IsNot Nothing Then
                                    Dim idx As Integer = 0
                                    For Each info As PropertyInfo In obj.GetType().GetProperties()
                                        Dim bOut As Boolean = False
                                        Dim oraCom As New OracleCommon
                                        Dim para = oraCom.GetParameter(info.Name, info.GetValue(obj, Nothing), bOut)
                                        If para IsNot Nothing Then
                                            cmd.Parameters.Add(para)
                                            idx += 1
                                        End If
                                    Next
                                End If

                                cmd.ExecuteNonQuery()
                            End If

                            ModifyEmployeeByLinq(objEmp, log, gID, _imageBinary, objEmpCV, objEmpEdu, objEmpHealth)

                            If objEmp.IS_HISTORY Then
                                cmd.Parameters.Clear()
                                Dim obj = New With {.P_EMPLOYEE_ID = objEmp.ID,
                                                    .P_USER_BY = log.Username,
                                                    .P_USER_LOG = log.Ip & "-" & log.ComputerName,
                                                    .P_DATE_CHANGE = dateChange,
                                                    .P_TYPE_CHANGE = 2}
                                If obj IsNot Nothing Then
                                    Dim idx As Integer = 0
                                    For Each info As PropertyInfo In obj.GetType().GetProperties()
                                        Dim bOut As Boolean = False
                                        Dim oraCom As New OracleCommon
                                        Dim para = oraCom.GetParameter(info.Name, info.GetValue(obj, Nothing), bOut)
                                        If para IsNot Nothing Then
                                            cmd.Parameters.Add(para)
                                            idx += 1
                                        End If
                                    Next
                                End If
                                cmd.ExecuteNonQuery()
                            End If
                            cmd.Transaction.Commit()


                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()

                        Catch ex As Exception
                            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
                            cmd.Transaction.Rollback()
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                            Throw ex
                        End Try
                    End Using
                End Using
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

        Return True
    End Function

    Private Sub ModifyEmployeeByLinq(ByVal objEmp As EmployeeDTO, ByVal log As UserLog, ByRef gID As Decimal, _
                                        ByVal _imageBinary As Byte(), _
                                        Optional ByVal objEmpCV As EmployeeCVDTO = Nothing, _
                                        Optional ByVal objEmpEdu As EmployeeEduDTO = Nothing, _
                                        Optional ByVal objEmpHealth As EmployeeHealthDTO = Nothing)
        Try
            Dim objEmpData As New HU_EMPLOYEE With {.ID = objEmp.ID}
            '----Start modify HU_EMPLOYEE---

            objEmpData = (From p In Context.HU_EMPLOYEE Where p.ID = objEmp.ID).FirstOrDefault
            '----------------------------------------------
            objEmpData.EMPLOYEE_NAME_OTHER = objEmp.EMPLOYEE_NAME_OTHER
            objEmpData.EMPLOYEE_CODE_OLD = objEmp.EMPLOYEE_CODE_OLD
            objEmpData.BOOK_NO = objEmp.BOOKNO
            '==============================================
            objEmpData.EMPLOYEE_CODE = objEmp.EMPLOYEE_CODE
            objEmpData.FIRST_NAME_VN = objEmp.FIRST_NAME_VN
            objEmpData.LAST_NAME_VN = objEmp.LAST_NAME_VN
            objEmpData.FIRST_NAME_EN = objEmp.FIRST_NAME_EN
            objEmpData.LAST_NAME_EN = objEmp.LAST_NAME_EN
            objEmpData.FULLNAME_EN = objEmpData.FIRST_NAME_EN & " " & objEmpData.LAST_NAME_EN
            objEmpData.FULLNAME_VN = objEmpData.FIRST_NAME_VN & " " & objEmpData.LAST_NAME_VN
            objEmpData.ORG_ID = objEmp.ORG_ID
            objEmpData.WORK_STATUS = objEmp.WORK_STATUS
            objEmpData.TITLE_ID = objEmp.TITLE_ID
            objEmpData.DIRECT_MANAGER = objEmp.DIRECT_MANAGER
            objEmpData.LEVEL_MANAGER = objEmp.LEVEL_MANAGER
            objEmpData.STAFF_RANK_ID = objEmp.STAFF_RANK_ID
            objEmpData.ITIME_ID = objEmp.ITIME_ID
            objEmpData.PA_OBJECT_SALARY_ID = 1 'objEmp.PA_OBJECT_SALARY_ID
            Dim lstPaperDelete = (From p In Context.HU_EMPLOYEE_PAPER Where p.EMPLOYEE_ID = objEmpData.ID).ToList
            For Each item In lstPaperDelete
                Context.HU_EMPLOYEE_PAPER.DeleteObject(item)
            Next
            If objEmp.lstPaper IsNot Nothing AndAlso objEmp.lstPaper.Count > 0 Then
                For Each i In objEmp.lstPaper
                    Dim objEmpPaperData As New HU_EMPLOYEE_PAPER
                    objEmpPaperData.ID = Utilities.GetNextSequence(Context, Context.HU_EMPLOYEE_PAPER.EntitySet.Name)
                    objEmpPaperData.HU_PAPER_ID = i
                    objEmpPaperData.EMPLOYEE_ID = objEmpData.ID
                    Context.HU_EMPLOYEE_PAPER.AddObject(objEmpPaperData)
                Next
            End If

            Dim lstPaperFiledDelete = (From p In Context.HU_EMPLOYEE_PAPER_FILED Where p.EMPLOYEE_ID = objEmpData.ID).ToList
            For Each item In lstPaperFiledDelete
                Context.HU_EMPLOYEE_PAPER_FILED.DeleteObject(item)
            Next
            If objEmp.lstPaperFiled IsNot Nothing AndAlso objEmp.lstPaperFiled.Count > 0 Then
                For Each i In objEmp.lstPaperFiled
                    Dim objEmpPaperData As New HU_EMPLOYEE_PAPER_FILED
                    objEmpPaperData.ID = Utilities.GetNextSequence(Context, Context.HU_EMPLOYEE_PAPER_FILED.EntitySet.Name)
                    objEmpPaperData.HU_PAPER_ID = i
                    objEmpPaperData.EMPLOYEE_ID = objEmpData.ID
                    Context.HU_EMPLOYEE_PAPER_FILED.AddObject(objEmpPaperData)
                Next
            End If
            'End Thông tin modify bảng HU_EMPLOYEE.
            Dim bUpdateCV As Boolean
            'Start thông tin modify vào bảng HU_EMPLOYEE_CV
            Dim objEmpCVData As HU_EMPLOYEE_CV
            If objEmpCV IsNot Nothing Then
                bUpdateCV = False
                objEmpCVData = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = objEmp.ID).SingleOrDefault
                If objEmpCVData Is Nothing Then 'Them moi
                    objEmpCVData = New HU_EMPLOYEE_CV With {.EMPLOYEE_ID = objEmp.ID}
                Else 'Update
                    bUpdateCV = True
                End If
                If objEmpCV.IMAGE <> "" Then
                    objEmpCVData.IMAGE = objEmp.EMPLOYEE_CODE & objEmpCV.IMAGE 'Lưu Image thành dạng E10012.jpg.                    
                End If
                objEmpCVData.GENDER = objEmpCV.GENDER
                objEmpCVData.BIRTH_DATE = objEmpCV.BIRTH_DATE
                objEmpCVData.BIRTH_PLACE = objEmpCV.BIRTH_PLACE
                objEmpCVData.MARITAL_STATUS = objEmpCV.MARITAL_STATUS
                objEmpCVData.RELIGION = objEmpCV.RELIGION
                objEmpCVData.NATIVE = objEmpCV.NATIVE
                objEmpCVData.NATIONALITY = objEmpCV.NATIONALITY
                objEmpCVData.PER_ADDRESS = objEmpCV.PER_ADDRESS
                objEmpCVData.PER_PROVINCE = objEmpCV.PER_PROVINCE
                objEmpCVData.PER_DISTRICT = objEmpCV.PER_DISTRICT
                objEmpCVData.WORKPLACE_ID = objEmpCV.WORKPLACE_ID
                objEmpCVData.INS_REGION_ID = objEmpCV.INS_REGION_ID
                objEmpCVData.PER_WARD = objEmpCV.PER_WARD
                objEmpCVData.HOME_PHONE = objEmpCV.HOME_PHONE
                objEmpCVData.MOBILE_PHONE = objEmpCV.MOBILE_PHONE
                objEmpCVData.ID_NO = objEmpCV.ID_NO

                objEmpCVData.ID_DATE = objEmpCV.ID_DATE
                objEmpCVData.ID_PLACE = objEmpCV.ID_PLACE
                objEmpCVData.PASS_NO = objEmpCV.PASS_NO
                objEmpCVData.PASS_DATE = objEmpCV.PASS_DATE
                objEmpCVData.PASS_EXPIRE = objEmpCV.PASS_EXPIRE
                objEmpCVData.PASS_PLACE = objEmpCV.PASS_PLACE
                objEmpCVData.VISA = objEmpCV.VISA
                objEmpCVData.VISA_DATE = objEmpCV.VISA_DATE
                objEmpCVData.VISA_EXPIRE = objEmpCV.VISA_EXPIRE
                objEmpCVData.VISA_PLACE = objEmpCV.VISA_PLACE
                objEmpCVData.WORK_PERMIT = objEmpCV.WORK_PERMIT
                objEmpCVData.WORK_PERMIT_DATE = objEmpCV.WORK_PERMIT_DATE
                objEmpCVData.WORK_PERMIT_EXPIRE = objEmpCV.WORK_PERMIT_EXPIRE
                objEmpCVData.WORK_PERMIT_PLACE = objEmpCV.WORK_PERMIT_PLACE
                objEmpCVData.WORK_EMAIL = objEmpCV.WORK_EMAIL
                objEmpCVData.NAV_ADDRESS = objEmpCV.NAV_ADDRESS
                objEmpCVData.NAV_PROVINCE = objEmpCV.NAV_PROVINCE
                objEmpCVData.NAV_DISTRICT = objEmpCV.NAV_DISTRICT
                objEmpCVData.NAV_WARD = objEmpCV.NAV_WARD
                objEmpCVData.PIT_CODE = objEmpCV.PIT_CODE
                objEmpCVData.PER_EMAIL = objEmpCV.PER_EMAIL
                objEmpCVData.CONTACT_PER = objEmpCV.CONTACT_PER
                objEmpCVData.CONTACT_PER_PHONE = objEmpCV.CONTACT_PER_PHONE
                objEmpCVData.CAREER = objEmpCV.CAREER

                objEmpCVData.NGAY_VAO_DOAN = objEmpCV.NGAY_VAO_DOAN
                objEmpCVData.NOI_VAO_DOAN = objEmpCV.NOI_VAO_DOAN
                objEmpCVData.CHUC_VU_DOAN = objEmpCV.CHUC_VU_DOAN
                objEmpCVData.DOAN_PHI = objEmpCV.DOAN_PHI
                objEmpCVData.NGAY_VAO_DANG = objEmpCV.NGAY_VAO_DANG
                objEmpCVData.NOI_VAO_DANG = objEmpCV.NOI_VAO_DANG
                objEmpCVData.CHUC_VU_DANG = objEmpCV.CHUC_VU_DANG
                objEmpCVData.DANG_PHI = objEmpCV.DANG_PHI
                objEmpCVData.BANK_ID = objEmpCV.BANK_ID
                objEmpCVData.BANK_BRANCH_ID = objEmpCV.BANK_BRANCH_ID
                objEmpCVData.BANK_NO = objEmpCV.BANK_NO
                objEmpCVData.IS_PERMISSION = objEmpCV.IS_PERMISSION
                objEmpCVData.IS_PAY_BANK = objEmpCV.IS_PAY_BANK
                objEmpCVData.PROVINCENQ_ID = objEmpCV.PROVINCENQ_ID
                '-------------------------------------------------
                objEmpCVData.OPPTION1 = objEmpCV.OPPTION1
                objEmpCVData.OPPTION2 = objEmpCV.OPPTION2
                objEmpCVData.OPPTION3 = objEmpCV.OPPTION3
                objEmpCVData.OPPTION4 = objEmpCV.OPPTION4
                objEmpCVData.OPPTION5 = objEmpCV.OPPTION5
                objEmpCVData.OPPTION6 = objEmpCV.OPPTION6
                objEmpCVData.OPPTION7 = objEmpCV.OPPTION7
                objEmpCVData.OPPTION8 = objEmpCV.OPPTION8
                objEmpCVData.OPPTION9 = objEmpCV.OPPTION9
                objEmpCVData.OPPTION10 = objEmpCV.OPPTION10
                objEmpCVData.PROVINCEEMP_ID = objEmpCV.PROVINCEEMP_ID
                objEmpCVData.DISTRICTEMP_ID = objEmpCV.DISTRICTEMP_ID
                objEmpCVData.WARDEMP_ID = objEmpCV.WARDEMP_ID

                objEmpCVData.HANG_THUONG_BINH = objEmpCV.HANG_THUONG_BINH
                objEmpCVData.THUONG_BINH = objEmpCV.THUONG_BINH
                objEmpCVData.DV_XUAT_NGU_QD = objEmpCV.DV_XUAT_NGU_QD
                objEmpCVData.NGAY_XUAT_NGU_QD = objEmpCV.NGAY_XUAT_NGU_QD
                objEmpCVData.NGAY_NHAP_NGU_QD = objEmpCV.NGAY_NHAP_NGU_QD
                objEmpCVData.QD = objEmpCV.QD
                objEmpCVData.DV_XUAT_NGU_CA = objEmpCV.DV_XUAT_NGU_CA
                objEmpCVData.NGAY_XUAT_NGU_CA = objEmpCV.NGAY_XUAT_NGU_CA
                objEmpCVData.NGAY_NHAP_NGU_CA = objEmpCV.NGAY_NHAP_NGU_CA
                objEmpCVData.NGAY_TG_BAN_NU_CONG = objEmpCV.NGAY_TG_BAN_NU_CONG
                objEmpCVData.CV_BAN_NU_CONG = objEmpCV.CV_BAN_NU_CONG
                objEmpCVData.NU_CONG = objEmpCV.NU_CONG
                objEmpCVData.NGAY_TG_BANTT = objEmpCV.NGAY_TG_BANTT
                objEmpCVData.CV_BANTT = objEmpCV.CV_BANTT
                objEmpCVData.CONG_DOAN = objEmpCV.CONG_DOAN
                objEmpCVData.CA = objEmpCV.CA
                objEmpCVData.DANG = objEmpCV.DANG
                objEmpCVData.SKILL = objEmpCV.SKILL
                objEmpCVData.NGAY_VAO_DANG_DB = objEmpCV.NGAY_VAO_DANG_DB
                objEmpCVData.NGAY_VAO_DANG = objEmpCV.NGAY_VAO_DANG
                objEmpCVData.CHUC_VU_DANG = objEmpCV.CHUC_VU_DANG
                objEmpCVData.DOAN_PHI = objEmpCV.DOAN_PHI
                objEmpCVData.CHUC_VU_DOAN = objEmpCV.CHUC_VU_DOAN
                objEmpCVData.NGAY_VAO_DOAN = objEmpCV.NGAY_VAO_DOAN
                objEmpCVData.GD_CHINH_SACH = objEmpCV.GD_CHINH_SACH
                
                '------------------------------------------------
                If bUpdateCV = False Then
                    Context.HU_EMPLOYEE_CV.AddObject(objEmpCVData)
                End If
            End If
            'End thông tin insert vào bảng HU_EMPLOYEE_EDUCATION


            'Start thông tin modify vào bảng HU_EMPLOYEE_EDUCATION
            If objEmpEdu IsNot Nothing Then
                Dim bUpdateEdu As Boolean
                Dim objEmpEduData As HU_EMPLOYEE_EDUCATION
                bUpdateEdu = False
                objEmpEduData = (From p In Context.HU_EMPLOYEE_EDUCATION Where p.EMPLOYEE_ID = objEmp.ID).SingleOrDefault
                If objEmpEduData Is Nothing Then 'Them moi
                    objEmpEduData = New HU_EMPLOYEE_EDUCATION With {.EMPLOYEE_ID = objEmp.ID}
                Else 'Update
                    bUpdateEdu = True
                End If
                objEmpEduData.ACADEMY = objEmpEdu.ACADEMY
                objEmpEduData.MAJOR = objEmpEdu.MAJOR
                objEmpEduData.MAJOR_REMARK = objEmpEdu.MAJOR_REMARK
                objEmpEduData.LANGUAGE = objEmpEdu.LANGUAGE
                objEmpEduData.LANGUAGE_LEVEL = objEmpEdu.LANGUAGE_LEVEL
                objEmpEduData.LANGUAGE_MARK = objEmpEdu.LANGUAGE_MARK
                objEmpEduData.GRADUATE_SCHOOL_ID = objEmpEdu.GRADUATE_SCHOOL_ID
                objEmpEduData.TRAINING_FORM = objEmpEdu.TRAINING_FORM
                objEmpEduData.LEARNING_LEVEL = objEmpEdu.LEARNING_LEVEL
                objEmpEduData.GRADUATION_YEAR = objEmpEdu.GRADUATION_YEAR
                objEmpEduData.QLNN = objEmpEdu.QLNN
                objEmpEduData.LLCT = objEmpEdu.LLCT
                objEmpEduData.TDTH = objEmpEdu.TDTH
                objEmpEduData.DIEM_XLTH = objEmpEdu.DIEM_XLTH
                If bUpdateEdu = False Then
                    Context.HU_EMPLOYEE_EDUCATION.AddObject(objEmpEduData)
                End If
            End If
            'End thông tin insert vào bảng HU_EMPLOYEE_EDUCATION

            'Start thông tin modify vào bảng HU_EMPLOYEE_HEALTH
            If objEmpHealth IsNot Nothing Then
                Dim bUpdateHealth As Boolean
                Dim objEmpHealthData As HU_EMPLOYEE_HEALTH
                bUpdateHealth = False
                objEmpHealthData = (From p In Context.HU_EMPLOYEE_HEALTH Where p.EMPLOYEE_ID = objEmp.ID).SingleOrDefault
                If objEmpHealthData Is Nothing Then 'Them moi
                    objEmpHealthData = New HU_EMPLOYEE_HEALTH With {.EMPLOYEE_ID = objEmp.ID}
                Else 'Update
                    bUpdateHealth = True
                End If
                objEmpHealthData.CHIEU_CAO = objEmpHealth.CHIEU_CAO
                objEmpHealthData.CAN_NANG = objEmpHealth.CAN_NANG
                objEmpHealthData.NHOM_MAU = objEmpHealth.NHOM_MAU
                objEmpHealthData.HUYET_AP = objEmpHealth.HUYET_AP
                objEmpHealthData.MAT_TRAI = objEmpHealth.MAT_TRAI
                objEmpHealthData.MAT_PHAI = objEmpHealth.MAT_PHAI
                objEmpHealthData.LOAI_SUC_KHOE = objEmpHealth.LOAI_SUC_KHOE
                objEmpHealthData.TAI_MUI_HONG = objEmpHealth.TAI_MUI_HONG
                objEmpHealthData.RANG_HAM_MAT = objEmpHealth.RANG_HAM_MAT
                objEmpHealthData.TIM = objEmpHealth.TIM
                objEmpHealthData.PHOI_NGUC = objEmpHealth.PHOI_NGUC
                objEmpHealthData.VIEM_GAN_B = objEmpHealth.VIEM_GAN_B
                objEmpHealthData.DA_HOA_LIEU = objEmpHealth.DA_HOA_LIEU
                objEmpHealthData.GHI_CHU_SUC_KHOE = objEmpHealth.GHI_CHU_SUC_KHOE
                objEmpHealthData.TTSUCKHOE = objEmpHealth.TTSUCKHOE
                If bUpdateHealth = False Then
                    Context.HU_EMPLOYEE_HEALTH.AddObject(objEmpHealthData)
                End If
            End If
            'End thông tin insert vào bảng HU_EMPLOYEE_HEALTH

            If _imageBinary IsNot Nothing AndAlso _imageBinary.Length > 0 Then
                Dim savepath = ""
                savepath = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage"
                If Not Directory.Exists(savepath) Then
                    Directory.CreateDirectory(savepath)
                End If
                'Xóa ảnh cũ của nhân viên.
                Dim sFile() As String = Directory.GetFiles(savepath, objEmpCVData.IMAGE)
                For Each s In sFile
                    File.Delete(s)
                Next
                Dim ms As New MemoryStream(_imageBinary)
                ' instance a filestream pointing to the
                ' storatge folder, use the original file name
                ' to name the resulting file

                Dim fs As New FileStream(savepath & "\" & objEmpCVData.IMAGE, FileMode.Create)
                ms.WriteTo(fs)

                ' clean up
                ms.Close()
                fs.Close()
                fs.Dispose()
            End If
            Context.SaveChanges(log)
            ' Sua vao tai khoan

            'Dim user = (From p In Context.SE_USER Where p.EMPLOYEE_CODE = objEmpData.EMPLOYEE_CODE).FirstOrDefault
            'If objEmpCV.WORK_EMAIL <> "" Then
            '    If user Is Nothing Then
            '        Dim _new As New SE_USER
            '        Dim EncryptData As New EncryptData
            '        _new.ID = Utilities.GetNextSequence(Context, Context.SE_USER.EntitySet.Name)
            '        _new.EFFECT_DATE = Date.Now
            '        _new.EMPLOYEE_CODE = objEmpData.EMPLOYEE_CODE
            '        _new.FULLNAME = objEmpData.FULLNAME_VN
            '        _new.EMAIL = objEmpCVData.WORK_EMAIL
            '        _new.TELEPHONE = objEmpCVData.MOBILE_PHONE
            '        _new.IS_AD = True
            '        _new.IS_APP = False
            '        _new.IS_PORTAL = True
            '        _new.IS_CHANGE_PASS = "-1"
            '        _new.ACTFLG = "A"
            '        _new.PASSWORD = EncryptData.EncryptString(objEmpData.EMPLOYEE_CODE)
            '        _new.USERNAME = objEmpCV.WORK_EMAIL.ToUpper
            '        Context.SE_USER.AddObject(_new)
            '    End If
            'End If
            'Context.SaveChanges(log)
            gID = objEmpData.ID

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, "Profile.InsertEmployeeByLinq")
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Hàm xóa nhân viên
    ''' </summary>
    ''' <param name="lstEmpID"></param>
    ''' <param name="log"></param>
    ''' <param name="sError"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteEmployee(ByVal lstEmpID As List(Of Decimal), ByVal log As UserLog, ByRef sError As String) As Boolean
        Dim lstEmpDelete As List(Of HU_EMPLOYEE)
        Try
            lstEmpDelete = (From p In Context.HU_EMPLOYEE Where lstEmpID.Contains(p.ID)).ToList
            For i As Int16 = 0 To lstEmpDelete.Count - 1
                'Kiểm tra nhân viên đó có hợp đồng ko. Nếu ko có thì xóa, nếu có thì lưu và cảnh báo.
                Dim objDelete = lstEmpDelete(i)
                Dim empID As Decimal = objDelete.ID
                Dim query As ObjectQuery(Of ContractDTO)
                Try
                    query = (From p In Context.HU_CONTRACT Where p.EMPLOYEE_ID = empID
                           Select New ContractDTO With {
                            .EMPLOYEE_ID = p.EMPLOYEE_ID})

                    If query.ToList.Count > 0 Then 'Nếu có hợp đồng
                        sError = sError & "," & objDelete.EMPLOYEE_CODE 'Lưu lại EMPLOYEE_CODE để cảnh báo.
                    Else 'Nếu ko có thì xóa nhân viên
                        '---Start xóa nhân viên-----------------------------------------------------------
                        '1. Xóa EMPLOYEE_CV.
                        Dim lstEmpCVDelete = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = empID).ToList
                        For idx As Int16 = 0 To lstEmpCVDelete.Count - 1
                            Context.HU_EMPLOYEE_CV.DeleteObject(lstEmpCVDelete(idx))
                        Next
                        '3. Xóa HU_EMPLOYEE_HEALTH
                        Dim lstEmpHealthDelete = (From p In Context.HU_EMPLOYEE_HEALTH Where p.EMPLOYEE_ID = empID).ToList
                        For idx As Int16 = 0 To lstEmpHealthDelete.Count - 1
                            Context.HU_EMPLOYEE_HEALTH.DeleteObject(lstEmpHealthDelete(idx))
                        Next

                        '6. Xóa HU_FAMILY
                        Dim lstFamilyDelete = (From p In Context.HU_FAMILY Where p.EMPLOYEE_ID = empID).ToList
                        For idx As Int16 = 0 To lstFamilyDelete.Count - 1
                            Context.HU_FAMILY.DeleteObject(lstFamilyDelete(idx))
                        Next
                        '7. Xóa HU_WORKING_BEFORE
                        Dim lstWorkingBeforeDelete = (From p In Context.HU_WORKING_BEFORE Where p.EMPLOYEE_ID = empID).ToList
                        For idx As Int16 = 0 To lstWorkingBeforeDelete.Count - 1
                            Context.HU_WORKING_BEFORE.DeleteObject(lstWorkingBeforeDelete(idx))
                        Next

                        '9. Xóa HU_WELFARE_MNG
                        Dim lstWelfareMngDelete = (From p In Context.HU_WELFARE_MNG Where p.EMPLOYEE_ID = empID).ToList
                        For idx As Int16 = 0 To lstWelfareMngDelete.Count - 1
                            Context.HU_WELFARE_MNG.DeleteObject(lstWelfareMngDelete(idx))
                        Next
                        '10. Xóa HU_EMPLOYEE_EDUCATION
                        Dim lstEduDelete = (From p In Context.HU_EMPLOYEE_EDUCATION Where p.EMPLOYEE_ID = empID).ToList
                        For idx As Int16 = 0 To lstEduDelete.Count - 1
                            Context.HU_EMPLOYEE_EDUCATION.DeleteObject(lstEduDelete(idx))
                        Next

                        '11. Xóa SE_USER.
                        Dim lstSeUserDelete = (From p In Context.SE_USER Where p.EMPLOYEE_CODE = objDelete.EMPLOYEE_CODE).ToList
                        For idx As Int16 = 0 To lstSeUserDelete.Count - 1
                            Context.SE_USER.DeleteObject(lstSeUserDelete(idx))
                        Next
                        '12. Xóa HU_EMPLOYEE_TRAIN
                        Dim lstTrainDelete = (From p In Context.HU_EMPLOYEE_TRAIN Where p.EMPLOYEE_ID = empID).ToList
                        For idx As Int16 = 0 To lstTrainDelete.Count - 1
                            Context.HU_EMPLOYEE_TRAIN.DeleteObject(lstTrainDelete(idx))
                        Next
                        'Xóa Working
                        Dim lstWorkingUserDelete = (From p In Context.HU_WORKING Where p.EMPLOYEE_ID = empID).ToList
                        For idx As Int16 = 0 To lstWorkingUserDelete.Count - 1
                            Context.HU_WORKING.DeleteObject(lstWorkingUserDelete(idx))
                        Next

                        Context.HU_EMPLOYEE.DeleteObject(lstEmpDelete(i))

                        '---End xóa nhân viên ------------------------------------------------------------
                    End If
                Catch ex As Exception
                    WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
                    Throw ex
                End Try
            Next
            If sError = "" Then
                Context.SaveChanges()
                Return True
            End If
            Return False
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
        Return True
    End Function

    Public Function GetEmployeeAllByID(ByVal sEmployeeID As Decimal,
                                  ByRef empCV As EmployeeCVDTO,
                                  ByRef empEdu As EmployeeEduDTO,
                                  ByRef empHealth As EmployeeHealthDTO) As Boolean
        Try
            empCV = (From cv In Context.HU_EMPLOYEE_CV
                     From g In Context.OT_OTHER_LIST.Where(Function(f) cv.GENDER = f.ID).DefaultIfEmpty
                     From ft In Context.OT_OTHER_LIST.Where(Function(f) cv.MARITAL_STATUS = f.ID).DefaultIfEmpty
                     From rl In Context.OT_OTHER_LIST.Where(Function(f) cv.RELIGION = f.ID).DefaultIfEmpty
                     From nt In Context.OT_OTHER_LIST.Where(Function(f) cv.NATIVE = f.ID).DefaultIfEmpty
                     From na In Context.HU_NATION.Where(Function(f) cv.NATIONALITY = f.ID).DefaultIfEmpty
                     From bank In Context.HU_BANK.Where(Function(f) cv.BANK_ID = f.ID).DefaultIfEmpty
                     From bankbranch In Context.HU_BANK_BRANCH.Where(Function(f) cv.BANK_BRANCH_ID = f.ID).DefaultIfEmpty
                     From per_pro In Context.HU_PROVINCE.Where(Function(f) cv.PER_PROVINCE = f.ID).DefaultIfEmpty
                     From per_dis In Context.HU_DISTRICT.Where(Function(f) cv.PER_DISTRICT = f.ID).DefaultIfEmpty
                     From per_ward In Context.HU_WARD.Where(Function(f) cv.PER_WARD = f.ID).DefaultIfEmpty
                     From nav_pro In Context.HU_PROVINCE.Where(Function(f) cv.NAV_PROVINCE = f.ID).DefaultIfEmpty
                     From nav_dis In Context.HU_DISTRICT.Where(Function(f) cv.NAV_DISTRICT = f.ID).DefaultIfEmpty
                     From nav_ward In Context.HU_WARD.Where(Function(f) cv.NAV_WARD = f.ID).DefaultIfEmpty
                     From workplace In Context.HU_PROVINCE.Where(Function(f) cv.WORKPLACE_ID = f.ID).DefaultIfEmpty
                     From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = cv.EMPLOYEE_ID).DefaultIfEmpty
                     From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID).DefaultIfEmpty
                    From region In Context.OT_OTHER_LIST.Where(Function(f) f.ID = org.REGION_ID).DefaultIfEmpty
                    From emp_pro In Context.HU_PROVINCE.Where(Function(f) cv.PER_PROVINCE = f.ID).DefaultIfEmpty
                     From emp_dis In Context.HU_DISTRICT.Where(Function(f) cv.PER_DISTRICT = f.ID).DefaultIfEmpty
                     From emp_ward In Context.HU_WARD.Where(Function(f) cv.PER_WARD = f.ID).DefaultIfEmpty
            Where (cv.EMPLOYEE_ID = sEmployeeID)
                     Select New EmployeeCVDTO With {
                         .EMPLOYEE_ID = cv.EMPLOYEE_ID,
                         .GENDER = cv.GENDER,
                         .GENDER_NAME = g.NAME_VN,
                         .BIRTH_DATE = cv.BIRTH_DATE,
                         .BIRTH_PLACE = cv.BIRTH_PLACE,
                         .MARITAL_STATUS = cv.MARITAL_STATUS,
                         .MARITAL_STATUS_NAME = ft.NAME_VN,
                         .RELIGION = cv.RELIGION,
                         .RELIGION_NAME = rl.NAME_VN,
                         .NATIVE = cv.NATIVE,
                         .NATIVE_NAME = nt.NAME_VN,
                         .NATIONALITY = cv.NATIONALITY,
                         .NATIONALITY_NAME = na.NAME_VN,
                         .PER_ADDRESS = cv.PER_ADDRESS,
                         .PER_PROVINCE = cv.PER_PROVINCE,
                         .PER_PROVINCE_NAME = per_pro.NAME_VN,
                         .PER_DISTRICT = cv.PER_DISTRICT,
                         .PER_DISTRICT_NAME = per_dis.NAME_VN,
                         .PER_WARD = cv.PER_WARD,
                         .PER_WARD_NAME = per_ward.NAME_VN,
                         .WORKPLACE_ID = cv.WORKPLACE_ID,
                         .WORKPLACE_NAME = workplace.NAME_VN,
                         .INS_REGION_ID = cv.INS_REGION_ID,
                         .INS_REGION_NAME = region.NAME_VN,
                         .HOME_PHONE = cv.HOME_PHONE,
                         .MOBILE_PHONE = cv.MOBILE_PHONE,
                         .ID_NO = cv.ID_NO,
                         .ID_DATE = cv.ID_DATE,
                         .ID_PLACE = cv.ID_PLACE,
                         .PASS_NO = cv.PASS_NO,
                         .PASS_DATE = cv.PASS_DATE,
                         .PASS_EXPIRE = cv.PASS_EXPIRE,
                         .PASS_PLACE = cv.PASS_PLACE,
                         .VISA = cv.VISA,
                         .VISA_DATE = cv.VISA_DATE,
                         .VISA_EXPIRE = cv.VISA_EXPIRE,
                         .VISA_PLACE = cv.VISA_PLACE,
                         .WORK_PERMIT = cv.WORK_PERMIT,
                         .WORK_PERMIT_DATE = cv.WORK_PERMIT_DATE,
                         .WORK_PERMIT_EXPIRE = cv.WORK_PERMIT_EXPIRE,
                         .WORK_PERMIT_PLACE = cv.WORK_PERMIT_PLACE,
                         .WORK_EMAIL = cv.WORK_EMAIL,
                         .NAV_ADDRESS = cv.NAV_ADDRESS,
                         .NAV_PROVINCE = cv.NAV_PROVINCE,
                         .NAV_PROVINCE_NAME = nav_pro.NAME_VN,
                         .NAV_DISTRICT = cv.NAV_DISTRICT,
                         .NAV_DISTRICT_NAME = nav_dis.NAME_VN,
                         .NAV_WARD = cv.NAV_WARD,
                         .NAV_WARD_NAME = nav_ward.NAME_VN,
                         .PIT_CODE = cv.PIT_CODE,
                         .PER_EMAIL = cv.PER_EMAIL,
                         .CONTACT_PER = cv.CONTACT_PER,
                         .CONTACT_PER_PHONE = cv.CONTACT_PER_PHONE,
                         .CAREER = cv.CAREER,
                         .DANG_PHI = cv.DANG_PHI,
                         .DOAN_PHI = cv.DOAN_PHI,
                         .NGAY_VAO_DANG = cv.NGAY_VAO_DANG,
                         .NGAY_VAO_DOAN = cv.NGAY_VAO_DOAN,
                         .CHUC_VU_DANG = cv.CHUC_VU_DANG,
                         .CHUC_VU_DOAN = cv.CHUC_VU_DOAN,
                         .NOI_VAO_DANG = cv.NOI_VAO_DANG,
                         .NOI_VAO_DOAN = cv.NOI_VAO_DOAN,
                         .BANK_BRANCH_ID = cv.BANK_BRANCH_ID,
                         .BANK_BRANCH_NAME = bankbranch.NAME,
                         .BANK_ID = cv.BANK_ID,
                         .BANK_NAME = bank.NAME,
                         .IS_PERMISSION = cv.IS_PERMISSION,
                         .OPPTION1 = cv.OPPTION1,
                         .OPPTION2 = cv.OPPTION2,
                         .OPPTION3 = cv.OPPTION3,
                         .OPPTION4 = cv.OPPTION4,
                         .OPPTION5 = cv.OPPTION5,
                         .OPPTION6 = cv.OPPTION6,
                         .OPPTION7 = cv.OPPTION7,
                         .OPPTION8 = cv.OPPTION8,
                         .OPPTION9 = cv.OPPTION9,
                         .OPPTION10 = cv.OPPTION10,
                         .GD_CHINH_SACH = cv.GD_CHINH_SACH,
                         .THUONG_BINH = CType(cv.THUONG_BINH, Boolean),
                         .DV_XUAT_NGU_QD = cv.DV_XUAT_NGU_QD,
                         .NGAY_XUAT_NGU_QD = cv.NGAY_XUAT_NGU_QD,
                         .NGAY_NHAP_NGU_QD = cv.NGAY_NHAP_NGU_QD,
                         .QD = CType(cv.QD, Boolean),
                         .DV_XUAT_NGU_CA = cv.DV_XUAT_NGU_CA,
                         .NGAY_XUAT_NGU_CA = cv.NGAY_XUAT_NGU_CA,
                         .NGAY_NHAP_NGU_CA = cv.NGAY_NHAP_NGU_CA,
                         .NGAY_TG_BAN_NU_CONG = cv.NGAY_TG_BAN_NU_CONG,
                         .CV_BAN_NU_CONG = cv.CV_BAN_NU_CONG,
                         .NU_CONG = CType(cv.NU_CONG, Boolean),
                         .NGAY_TG_BANTT = cv.NGAY_TG_BANTT,
                         .CV_BANTT = cv.CV_BANTT,
                         .CONG_DOAN = CType(cv.CONG_DOAN, Boolean),
                         .CA = cv.CA,
                         .DANG = cv.DANG,
                         .SKILL = cv.SKILL,
                         .NGAY_VAO_DANG_DB = cv.NGAY_VAO_DANG_DB,
                         .HANG_THUONG_BINH = cv.HANG_THUONG_BINH,
                         .PROVINCEEMP_ID = cv.PROVINCEEMP_ID,
                         .PROVINCEEMP_NAME = emp_pro.NAME_VN,
                         .DISTRICTEMP_NAME = emp_dis.NAME_VN,
                         .WARDEMP_NAME = emp_ward.NAME_VN,
                         .DISTRICTEMP_ID = cv.DISTRICTEMP_ID,
                         .WARDEMP_ID = cv.WARDEMP_ID,
                         .PROVINCENQ_ID = cv.PROVINCENQ_ID,
                         .BANK_NO = cv.BANK_NO,
                         .IS_PAY_BANK = cv.IS_PAY_BANK}).FirstOrDefault

            empEdu = (From edu In Context.HU_EMPLOYEE_EDUCATION
                     From a In Context.OT_OTHER_LIST.Where(Function(f) f.ID = edu.ACADEMY).DefaultIfEmpty
                     From m In Context.OT_OTHER_LIST.Where(Function(f) f.ID = edu.MAJOR).DefaultIfEmpty
                     From train In Context.OT_OTHER_LIST.Where(Function(f) f.ID = edu.TRAINING_FORM).DefaultIfEmpty
                     From learn In Context.OT_OTHER_LIST.Where(Function(f) f.ID = edu.LEARNING_LEVEL).DefaultIfEmpty
                     From ll1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = edu.LANGUAGE_LEVEL).DefaultIfEmpty
                     From school In Context.OT_OTHER_LIST.Where(Function(f) f.ID = edu.GRADUATE_SCHOOL_ID).DefaultIfEmpty
                     Where edu.EMPLOYEE_ID = sEmployeeID
                     Select New EmployeeEduDTO With {
                         .EMPLOYEE_ID = edu.EMPLOYEE_ID,
                         .ACADEMY = edu.ACADEMY,
                         .ACADEMY_NAME = a.NAME_VN,
                         .MAJOR = edu.MAJOR,
                         .MAJOR_NAME = m.NAME_VN,
                         .MAJOR_REMARK = edu.MAJOR_REMARK,
                         .TRAINING_FORM = edu.TRAINING_FORM,
                         .TRAINING_FORM_NAME = train.NAME_VN,
                         .LEARNING_LEVEL = edu.LEARNING_LEVEL,
                         .LEARNING_LEVEL_NAME = learn.NAME_VN,
                         .LANGUAGE = edu.LANGUAGE,
                         .LANGUAGE_LEVEL = edu.LANGUAGE_LEVEL,
                         .LANGUAGE_LEVEL_NAME = ll1.NAME_VN,
                         .LANGUAGE_MARK = edu.LANGUAGE_MARK,
                         .GRADUATE_SCHOOL_ID = edu.GRADUATE_SCHOOL_ID,
                         .GRADUATE_SCHOOL_NAME = school.NAME_VN,
                         .QLNN = edu.QLNN,
                         .LLCT = edu.LLCT,
                         .TDTH = edu.TDTH,
                         .DIEM_XLTH = edu.DIEM_XLTH,
                         .GRADUATION_YEAR = edu.GRADUATION_YEAR}).FirstOrDefault

            empHealth = (From e In Context.HU_EMPLOYEE_HEALTH
                         Where e.EMPLOYEE_ID = sEmployeeID
                         Select New EmployeeHealthDTO With {
                             .EMPLOYEE_ID = e.EMPLOYEE_ID,
                             .CHIEU_CAO = e.CHIEU_CAO,
                             .CAN_NANG = e.CAN_NANG,
                             .NHOM_MAU = e.NHOM_MAU,
                             .HUYET_AP = e.HUYET_AP,
                             .MAT_TRAI = e.MAT_TRAI,
                             .MAT_PHAI = e.MAT_PHAI,
                             .LOAI_SUC_KHOE = e.LOAI_SUC_KHOE,
                             .TAI_MUI_HONG = e.TAI_MUI_HONG,
                             .RANG_HAM_MAT = e.RANG_HAM_MAT,
                             .TIM = e.TIM,
                             .PHOI_NGUC = e.PHOI_NGUC,
                             .VIEM_GAN_B = e.VIEM_GAN_B,
                             .DA_HOA_LIEU = e.DA_HOA_LIEU,
                             .TTSUCKHOE = e.TTSUCKHOE,
                             .GHI_CHU_SUC_KHOE = e.GHI_CHU_SUC_KHOE}).FirstOrDefault

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


    Public Function ValidateEmployee(ByVal sType As String, ByVal sEmpCode As String, ByVal value As String) As Boolean
        Try
            Select Case sType
                Case "EXIST_ID_NO_TERMINATE"
                    Return (From p In Context.HU_TERMINATE
                            From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                            From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                            Where cv.ID_NO = value And p.IS_NOHIRE = True And p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID).Count = 0

                Case "EXIST_ID_NO"
                    If sEmpCode <> "" Then
                        Return (From e In Context.HU_EMPLOYEE
                             From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                            Where cv.ID_NO = value And e.EMPLOYEE_CODE <> sEmpCode).Count = 0
                    Else

                        Return (From e In Context.HU_EMPLOYEE
                             From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                            Where cv.ID_NO = value).Count = 0
                    End If


                Case "EXIST_TIME_ID"
                    If sEmpCode <> "" Then
                        Return (From e In Context.HU_EMPLOYEE
                             Where e.ITIME_ID = value And e.EMPLOYEE_CODE <> sEmpCode).Count = 0
                    Else
                        Return (From e In Context.HU_EMPLOYEE
                            Where e.ITIME_ID = value).Count = 0
                    End If

                Case "EXIST_WORK_EMAIL"
                    'Return (From p In Context.HU_EMPLOYEE
                    '        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID)
                    '        Where p.EMPLOYEE_CODE <> sEmpCode And cv.WORK_EMAIL.ToUpper = value.ToUpper).Count = 0

                    If sEmpCode <> "" Then
                        Return (From p In Context.HU_EMPLOYEE
                                From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID)
                             Where p.EMPLOYEE_CODE <> sEmpCode And cv.WORK_EMAIL.ToUpper = value.ToUpper).Count = 0
                    Else
                        Return (From p In Context.HU_EMPLOYEE
                                From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID)
                            Where cv.WORK_EMAIL.ToUpper = value.ToUpper).Count = 0
                    End If
                Case "EXIST_BANK_NO"
                    Return (From p In Context.HU_EMPLOYEE
                            From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID)
                            Where cv.BANK_NO.ToUpper = value.ToUpper).Count = 0
            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateEmployee")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Hàm kiểm tra nhân viên có hợp đồng chưa.
    ''' </summary>
    ''' <param name="strEmpCode"></param>
    ''' <returns>True: Nếu có hợp đồng</returns>
    ''' <remarks></remarks>
    Public Function CheckEmpHasContract(ByVal strEmpCode As String) As Boolean
        Try
            Return (From p In Context.HU_CONTRACT Where p.HU_EMPLOYEE.EMPLOYEE_CODE = strEmpCode And p.OT_STATUS.CODE = "1").Count > 0
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#Region "EmployeeTrain"
    Public Function InsertEmployeeTrain(ByVal objEmployeeTrain As EmployeeTrainDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Dim objEmployeeTrainData As New HU_EMPLOYEE_TRAIN
            objEmployeeTrainData.ID = Utilities.GetNextSequence(Context, Context.HU_EMPLOYEE_TRAIN.EntitySet.Name)
            objEmployeeTrainData.EMPLOYEE_ID = objEmployeeTrain.EMPLOYEE_ID
            objEmployeeTrainData.FROM_DATE = objEmployeeTrain.FROM_DATE
            objEmployeeTrainData.TO_DATE = objEmployeeTrain.TO_DATE
            objEmployeeTrainData.SCHOOL_NAME = objEmployeeTrain.SCHOOL_NAME
            objEmployeeTrainData.TRAINING_FORM = objEmployeeTrain.TRAINING_FORM
            objEmployeeTrainData.HIGHEST_LEVEL = objEmployeeTrain.HIGHEST_LEVEL
            objEmployeeTrainData.LEARNING_LEVEL = objEmployeeTrain.LEARNING_LEVEL
            objEmployeeTrainData.MAJOR = objEmployeeTrain.MAJOR
            objEmployeeTrainData.GRADUATE_YEAR = objEmployeeTrain.GRADUATE_YEAR
            objEmployeeTrainData.MARK = objEmployeeTrain.MARK
            objEmployeeTrainData.TRAINING_CONTENT = objEmployeeTrain.TRAINING_CONTENT

            objEmployeeTrainData.CREATED_DATE = DateTime.Now
            objEmployeeTrainData.CREATED_BY = log.Username
            objEmployeeTrainData.CREATED_LOG = log.ComputerName
            objEmployeeTrainData.MODIFIED_DATE = DateTime.Now
            objEmployeeTrainData.MODIFIED_BY = log.Username
            objEmployeeTrainData.MODIFIED_LOG = log.ComputerName
            Context.HU_EMPLOYEE_TRAIN.AddObject(objEmployeeTrainData)
            Context.SaveChanges(log)
            gID = objEmployeeTrainData.ID
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyEmployeeTrain(ByVal objEmployeeTrain As EmployeeTrainDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objEmployeeTrainData As New HU_EMPLOYEE_TRAIN With {.ID = objEmployeeTrain.ID}
        Try
            objEmployeeTrainData = (From p In Context.HU_EMPLOYEE_TRAIN Where p.ID = objEmployeeTrain.ID).FirstOrDefault
            objEmployeeTrainData.EMPLOYEE_ID = objEmployeeTrain.EMPLOYEE_ID
            objEmployeeTrainData.FROM_DATE = objEmployeeTrain.FROM_DATE
            objEmployeeTrainData.TO_DATE = objEmployeeTrain.TO_DATE
            objEmployeeTrainData.SCHOOL_NAME = objEmployeeTrain.SCHOOL_NAME
            objEmployeeTrainData.TRAINING_FORM = objEmployeeTrain.TRAINING_FORM
            objEmployeeTrainData.HIGHEST_LEVEL = objEmployeeTrain.HIGHEST_LEVEL
            objEmployeeTrainData.LEARNING_LEVEL = objEmployeeTrain.LEARNING_LEVEL
            objEmployeeTrainData.MAJOR = objEmployeeTrain.MAJOR
            objEmployeeTrainData.GRADUATE_YEAR = objEmployeeTrain.GRADUATE_YEAR
            objEmployeeTrainData.MARK = objEmployeeTrain.MARK
            objEmployeeTrainData.TRAINING_CONTENT = objEmployeeTrain.TRAINING_CONTENT

            objEmployeeTrainData.MODIFIED_DATE = DateTime.Now
            objEmployeeTrainData.MODIFIED_BY = log.Username
            objEmployeeTrainData.MODIFIED_LOG = log.ComputerName
            Context.SaveChanges(log)
            gID = objEmployeeTrainData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateEmployeeTrain(ByVal objValidate As EmployeeTrainDTO) As Boolean
        Try
            Dim query As ObjectQuery(Of EmployeeTrainDTO)
            Dim lstEmpTrain As New List(Of EmployeeTrainDTO)
            'Kiểm tra đã có mức học vấn cao nhất chưa.
            If objValidate.ID > 0 Then
                query = (From p In Context.HU_EMPLOYEE_TRAIN Where p.EMPLOYEE_ID = objValidate.EMPLOYEE_ID _
                     And p.HIGHEST_LEVEL = -1 _
                     And p.ID <> objValidate.ID
                    Select New EmployeeTrainDTO With {
                        .ID = p.ID,
                        .EMPLOYEE_ID = p.EMPLOYEE_ID})
            Else
                query = (From p In Context.HU_EMPLOYEE_TRAIN Where p.EMPLOYEE_ID = objValidate.EMPLOYEE_ID _
                     And p.HIGHEST_LEVEL = -1
                    Select New EmployeeTrainDTO With {
                        .ID = p.ID,
                        .EMPLOYEE_ID = p.EMPLOYEE_ID})
            End If

            lstEmpTrain = query.ToList
            If lstEmpTrain.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
            Throw ex
        End Try

    End Function

    Public Function GetEmployeeTrain(ByVal _filter As EmployeeTrainDTO) As List(Of EmployeeTrainDTO)
        Dim query As ObjectQuery(Of EmployeeTrainDTO)
        Try
            query = (From p In Context.HU_EMPLOYEE_TRAIN
                     From q In Context.OT_OTHER_LIST.Where(Function(f) p.TRAINING_FORM = f.ID).DefaultIfEmpty
                     From o In Context.OT_OTHER_LIST.Where(Function(f) p.LEARNING_LEVEL = f.ID).DefaultIfEmpty
                     From r In Context.OT_OTHER_LIST.Where(Function(f) p.MAJOR = f.ID).DefaultIfEmpty
                     From s In Context.OT_OTHER_LIST.Where(Function(f) p.MARK = f.ID).DefaultIfEmpty
                   Select New EmployeeTrainDTO With {
                    .ID = p.ID,
                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                    .FROM_DATE = p.FROM_DATE,
                    .TO_DATE = p.TO_DATE,
                    .SCHOOL_NAME = p.SCHOOL_NAME,
                    .TRAINING_FORM = p.TRAINING_FORM,
                    .TRAINING_FORM_NAME = q.NAME_VN,
                    .HIGHEST_LEVEL = p.HIGHEST_LEVEL,
                    .LEARNING_LEVEL = p.LEARNING_LEVEL,
                    .LEARNING_LEVEL_NAME = o.NAME_VN,
                    .MAJOR = p.MAJOR,
                    .MAJOR_NAME = r.NAME_VN,
                    .GRADUATE_YEAR = p.GRADUATE_YEAR,
                    .MARK = p.MARK,
                    .MARK_NAME = s.NAME_VN,
                    .TRAINING_CONTENT = p.TRAINING_CONTENT,
                    .CREATED_DATE = p.CREATED_DATE,
                    .CREATED_BY = p.CREATED_BY,
                    .CREATED_LOG = p.CREATED_LOG,
                    .MODIFIED_DATE = p.MODIFIED_DATE,
                    .MODIFIED_BY = p.MODIFIED_BY,
                    .MODIFIED_LOG = p.MODIFIED_LOG})
            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.ID <> 0 Then
                query = query.Where(Function(p) p.ID = _filter.ID)
            End If
            'If _filter.HIGHEST_LEVEL = True Or _filter.HIGHEST_LEVEL = False Then
            '    query = query.Where(Function(p) p.HIGHEST_LEVEL = _filter.HIGHEST_LEVEL)
            'End If 
            Dim ret = query.ToList
            For Each item As EmployeeTrainDTO In ret
                If item.FROM_DATE.HasValue Then
                    item.FMONTH = item.FROM_DATE.Value.Month
                    item.FYEAR = item.FROM_DATE.Value.Year
                End If
                If item.TO_DATE.HasValue Then
                    item.TMONTH = item.TO_DATE.Value.Month
                    item.TYEAR = item.TO_DATE.Value.Year
                End If
            Next

            Return ret
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeTrainByID(ByVal EmployeeID As Decimal) As EmployeeTrainDTO
        Dim query As EmployeeTrainDTO
        Try
            query = (From p In Context.HU_EMPLOYEE_TRAIN
                     From q In Context.OT_OTHER_LIST.Where(Function(f) p.TRAINING_FORM = f.ID).DefaultIfEmpty
                     From o In Context.OT_OTHER_LIST.Where(Function(f) p.LEARNING_LEVEL = f.ID).DefaultIfEmpty
                     From r In Context.OT_OTHER_LIST.Where(Function(f) p.MAJOR = f.ID).DefaultIfEmpty
                     From s In Context.OT_OTHER_LIST.Where(Function(f) p.MARK = f.ID).DefaultIfEmpty
                     Where p.EMPLOYEE_ID = EmployeeID
                   Select New EmployeeTrainDTO With {
                    .ID = p.ID,
                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                    .FROM_DATE = p.FROM_DATE,
                    .TO_DATE = p.TO_DATE,
                    .SCHOOL_NAME = p.SCHOOL_NAME,
                    .TRAINING_FORM = p.TRAINING_FORM,
                    .TRAINING_FORM_NAME = q.NAME_VN,
                    .HIGHEST_LEVEL = p.HIGHEST_LEVEL,
                    .LEARNING_LEVEL = p.LEARNING_LEVEL,
                    .LEARNING_LEVEL_NAME = o.NAME_VN,
                    .MAJOR = p.MAJOR,
                    .MAJOR_NAME = r.NAME_VN,
                    .GRADUATE_YEAR = p.GRADUATE_YEAR,
                    .MARK = p.MARK,
                    .MARK_NAME = s.NAME_VN,
                    .TRAINING_CONTENT = p.TRAINING_CONTENT,
                    .CREATED_DATE = p.CREATED_DATE,
                    .CREATED_BY = p.CREATED_BY,
                    .CREATED_LOG = p.CREATED_LOG,
                    .MODIFIED_DATE = p.MODIFIED_DATE,
                    .MODIFIED_BY = p.MODIFIED_BY,
                    .MODIFIED_LOG = p.MODIFIED_LOG}).SingleOrDefault
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteEmployeeTrain(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_EMPLOYEE_TRAIN)
        Try
            lst = (From p In Context.HU_EMPLOYEE_TRAIN Where lstDecimals.Contains(p.ID)).ToList
            For i As Int16 = 0 To lst.Count - 1
                Context.HU_EMPLOYEE_TRAIN.DeleteObject(lst(i))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
#End Region

#Region "WorkingBefore"
    Public Function GetEmpWorkingBefore(ByVal _filter As WorkingBeforeDTO) As List(Of WorkingBeforeDTO)
        Dim query As ObjectQuery(Of WorkingBeforeDTO)
        Try
            query = (From p In Context.HU_WORKING_BEFORE
                   Select New WorkingBeforeDTO With {
                    .ID = p.ID,
                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                    .COMPANY_NAME = p.COMPANY_NAME,
                    .COMPANY_ADDRESS = p.COMPANY_ADDRESS,
                    .TELEPHONE = p.TELEPHONE,
                    .JOIN_DATE = p.JOIN_DATE,
                    .END_DATE = p.END_DATE,
                    .SALARY = p.SALARY,
                    .TITLE_NAME = p.TITLE_NAME,
                    .LEVEL_NAME = p.LEVEL_NAME,
                    .TER_REASON = p.TER_REASON})
            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.ID <> 0 Then
                query = query.Where(Function(p) p.ID = _filter.ID)
            End If
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertWorkingBefore(ByVal objWorkingBefore As WorkingBeforeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Dim objWorkingBeforeData As New HU_WORKING_BEFORE
            objWorkingBeforeData.ID = Utilities.GetNextSequence(Context, Context.HU_WORKING_BEFORE.EntitySet.Name)
            objWorkingBeforeData.EMPLOYEE_ID = objWorkingBefore.EMPLOYEE_ID
            objWorkingBeforeData.COMPANY_ADDRESS = objWorkingBefore.COMPANY_ADDRESS
            objWorkingBeforeData.COMPANY_NAME = objWorkingBefore.COMPANY_NAME
            objWorkingBeforeData.END_DATE = objWorkingBefore.END_DATE
            objWorkingBeforeData.SALARY = objWorkingBefore.SALARY
            objWorkingBeforeData.TELEPHONE = objWorkingBefore.TELEPHONE
            objWorkingBeforeData.TER_REASON = objWorkingBefore.TER_REASON
            objWorkingBeforeData.LEVEL_NAME = objWorkingBefore.LEVEL_NAME
            objWorkingBeforeData.TITLE_NAME = objWorkingBefore.TITLE_NAME
            objWorkingBeforeData.JOIN_DATE = objWorkingBefore.JOIN_DATE
            objWorkingBeforeData.END_DATE = objWorkingBefore.END_DATE
            objWorkingBeforeData.CREATED_DATE = DateTime.Now
            objWorkingBeforeData.CREATED_BY = log.Username
            objWorkingBeforeData.CREATED_LOG = log.ComputerName
            objWorkingBeforeData.MODIFIED_DATE = DateTime.Now
            objWorkingBeforeData.MODIFIED_BY = log.Username
            objWorkingBeforeData.MODIFIED_LOG = log.ComputerName
            Context.HU_WORKING_BEFORE.AddObject(objWorkingBeforeData)
            Context.SaveChanges(log)
            gID = objWorkingBeforeData.ID
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyWorkingBefore(ByVal objWorkingBefore As WorkingBeforeDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objWorkingBeforeData As New HU_WORKING_BEFORE With {.ID = objWorkingBefore.ID}
        Try
            objWorkingBeforeData = (From p In Context.HU_WORKING_BEFORE Where p.ID = objWorkingBefore.ID).FirstOrDefault
            objWorkingBeforeData.EMPLOYEE_ID = objWorkingBefore.EMPLOYEE_ID
            objWorkingBeforeData.COMPANY_ADDRESS = objWorkingBefore.COMPANY_ADDRESS
            objWorkingBeforeData.COMPANY_NAME = objWorkingBefore.COMPANY_NAME
            objWorkingBeforeData.END_DATE = objWorkingBefore.END_DATE
            objWorkingBeforeData.SALARY = objWorkingBefore.SALARY
            objWorkingBeforeData.TELEPHONE = objWorkingBefore.TELEPHONE
            objWorkingBeforeData.TER_REASON = objWorkingBefore.TER_REASON
            objWorkingBeforeData.LEVEL_NAME = objWorkingBefore.LEVEL_NAME
            objWorkingBeforeData.TITLE_NAME = objWorkingBefore.TITLE_NAME
            objWorkingBeforeData.JOIN_DATE = objWorkingBefore.JOIN_DATE
            objWorkingBeforeData.END_DATE = objWorkingBefore.END_DATE
            objWorkingBeforeData.MODIFIED_DATE = DateTime.Now
            objWorkingBeforeData.MODIFIED_BY = log.Username
            objWorkingBeforeData.MODIFIED_LOG = log.ComputerName
            Context.SaveChanges(log)
            gID = objWorkingBeforeData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteWorkingBefore(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_WORKING_BEFORE)
        Try
            lst = (From p In Context.HU_WORKING_BEFORE Where lstDecimals.Contains(p.ID)).ToList
            For i As Int16 = 0 To lst.Count - 1
                Context.HU_WORKING_BEFORE.DeleteObject(lst(i))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region

#End Region

#Region "Employee Proccess"

    ''' <summary>
    ''' Lấy danh sách nhân thân
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFamily(ByVal _empId As Decimal) As List(Of FamilyDTO)
        Try
            Dim query As List(Of FamilyDTO)
            query = (From p In Context.HU_FAMILY
                     From r In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RELATION_ID).DefaultIfEmpty
                     Where p.EMPLOYEE_ID = _empId
                     Order By p.CREATED_DATE
                     Select New FamilyDTO With {
                     .RELATION_ID = p.RELATION_ID,
                     .RELATION_NAME = r.NAME_VN,
                     .FULLNAME = p.FULLNAME,
                     .BIRTH_DATE = p.BIRTH_DATE,
                     .ID_NO = p.ID_NO,
                     .DEDUCT_REG = p.DEDUCT_REG,
                     .IS_DEDUCT = p.IS_DEDUCT,
                     .DEDUCT_FROM = p.DEDUCT_FROM,
                     .DEDUCT_TO = p.DEDUCT_TO,
                     .ADDRESS = p.ADDRESS,
                     .REMARK = p.REMARK,
                     .CREATED_DATE = p.CREATED_DATE}).ToList
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy quá trình công tác trước khi vào công ty
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWorkingBefore(ByVal _empId As Decimal) As List(Of WorkingBeforeDTO)
        Try
            Dim query As List(Of WorkingBeforeDTO)
            query = (From p In Context.HU_WORKING_BEFORE
                     Where p.EMPLOYEE_ID = _empId
                     Order By p.JOIN_DATE
                     Select New WorkingBeforeDTO With {
                         .ID = p.ID,
                         .EMPLOYEE_ID = p.EMPLOYEE_ID,
                         .COMPANY_NAME = p.COMPANY_NAME,
                         .COMPANY_ADDRESS = p.COMPANY_ADDRESS,
                         .TELEPHONE = p.TELEPHONE,
                         .JOIN_DATE = p.JOIN_DATE,
                         .END_DATE = p.END_DATE,
                         .SALARY = p.SALARY,
                         .TITLE_NAME = p.TITLE_NAME,
                         .LEVEL_NAME = p.LEVEL_NAME,
                         .TER_REASON = p.TER_REASON}).ToList()
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy quá trình công tác trong công ty, bao gồm quá trình công tác trước khi dùng phần mềm vào sau khi dùng phần mềm.
    ''' </summary>
    ''' <param name="_empCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWorkingProccess(ByVal _empId As Decimal?,
                                       Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_EMP_3B",
                                 New With {.P_USERNAME = log.Username,
                                           .P_EMPLOYEE_ID = _empId})
            End Using

            Dim query = From p In Context.HU_WORKING
                        From chosen In Context.SE_CHOSEN_EMP_3B.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID And
                                                                          f.USERNAME = log.Username.ToUpper)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From sal_group In Context.PA_SALARY_GROUP.Where(Function(f) p.SAL_GROUP_ID = f.ID).DefaultIfEmpty
                        From sal_level In Context.PA_SALARY_LEVEL.Where(Function(f) p.SAL_LEVEL_ID = f.ID).DefaultIfEmpty
                        From sal_rank In Context.PA_SALARY_RANK.Where(Function(f) p.SAL_RANK_ID = f.ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From staffRank In Context.HU_STAFF_RANK.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                        From deci_type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DECISION_TYPE_ID).DefaultIfEmpty
                        From obj_att In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANCE).DefaultIfEmpty
                        Where p.IS_MISSION = True And
                        p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID
                        Order By p.EFFECT_DATE Descending
                        Select New WorkingDTO With {.ID = p.ID,
                                                    .DECISION_NO = p.DECISION_NO,
                                                    .DECISION_TYPE_ID = p.DECISION_TYPE_ID,
                                                    .DECISION_TYPE_NAME = deci_type.NAME_VN,
                                                    .OBJECT_ATTENDANCE = p.OBJECT_ATTENDANCE,
                                                    .OBJECT_ATTENDANCE_NAME = obj_att.NAME_VN,
                                                    .FILING_DATE = p.FILING_DATE,
                                                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                    .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                    .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                    .ORG_NAME2 = o.ORG_NAME2,
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
                                                    .IS_MISSION_SHORT = p.IS_MISSION,
                                                    .IS_WAGE_SHORT = p.IS_WAGE,
                                                    .IS_PROCESS_SHORT = p.IS_PROCESS,
                                                    .SAL_GROUP_ID = p.SAL_GROUP_ID,
                                                    .SAL_GROUP_NAME = sal_group.NAME,
                                                    .SAL_LEVEL_ID = p.SAL_LEVEL_ID,
                                                    .SAL_LEVEL_NAME = sal_level.NAME,
                                                    .SAL_RANK_ID = p.SAL_RANK_ID,
                                                    .SAL_RANK_NAME = sal_rank.RANK,
                                                    .SIGN_DATE = p.SIGN_DATE,
                                                    .SIGN_NAME = p.SIGN_NAME,
                                                    .SIGN_TITLE = p.SIGN_TITLE,
                                                    .STAFF_RANK_NAME = staffRank.NAME,
                                                    .COST_SUPPORT = p.COST_SUPPORT,
                                                    .CREATED_DATE = p.CREATED_DATE}


            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy quá trình lương
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSalaryProccess(ByVal _empId As Decimal,
                                       Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO)
        Try


            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_EMP_3B",
                                 New With {.P_USERNAME = log.Username,
                                           .P_EMPLOYEE_ID = _empId})
            End Using

            Dim query = From p In Context.HU_WORKING
                    From chosen In Context.SE_CHOSEN_EMP_3B.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID And _
                                                                      f.USERNAME = log.Username.ToUpper)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From sal_group In Context.PA_SALARY_GROUP.Where(Function(f) p.SAL_GROUP_ID = f.ID).DefaultIfEmpty
                        From sal_level In Context.PA_SALARY_LEVEL.Where(Function(f) p.SAL_LEVEL_ID = f.ID).DefaultIfEmpty
                        From sal_rank In Context.PA_SALARY_RANK.Where(Function(f) p.SAL_RANK_ID = f.ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From deci_type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DECISION_TYPE_ID).DefaultIfEmpty
                        From staffrak In Context.HU_STAFF_RANK.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                        Where p.IS_WAGE = True And p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID
                        Order By p.EFFECT_DATE Descending
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
                                                    .STAFF_RANK_ID = p.STAFF_RANK_ID,
                                                    .STAFF_RANK_NAME = staffrak.NAME,
                                                    .SAL_BASIC = p.SAL_BASIC,
                                                    .IS_MISSION = p.IS_MISSION,
                                                    .IS_WAGE = p.IS_WAGE,
                                                    .IS_PROCESS = p.IS_PROCESS,
                                                    .IS_MISSION_SHORT = p.IS_MISSION,
                                                    .IS_WAGE_SHORT = p.IS_WAGE,
                                                    .IS_PROCESS_SHORT = p.IS_PROCESS,
                                                    .SAL_GROUP_ID = p.SAL_GROUP_ID,
                                                    .SAL_GROUP_NAME = sal_group.NAME,
                                                    .SAL_LEVEL_ID = p.SAL_LEVEL_ID,
                                                    .SAL_LEVEL_NAME = sal_level.NAME,
                                                    .SAL_RANK_ID = p.SAL_RANK_ID,
                                                    .SAL_RANK_NAME = sal_rank.RANK,
                                                    .SIGN_DATE = p.SIGN_DATE,
                                                    .SIGN_NAME = p.SIGN_NAME,
                                                    .SIGN_TITLE = p.SIGN_TITLE,
                                                    .SAL_TOTAL = p.SAL_TOTAL + If((From a In Context.HU_WORKING_ALLOW.Where(Function(f) f.HU_WORKING_ID = p.ID) Select a.AMOUNT).Sum Is Nothing, 0, (From a In Context.HU_WORKING_ALLOW.Where(Function(f) f.HU_WORKING_ID = p.ID) Select a.AMOUNT).Sum),
                                                    .COST_SUPPORT = p.COST_SUPPORT,
                                                    .PERCENT_SALARY = p.PERCENT_SALARY,
                                                    .CREATED_DATE = p.CREATED_DATE}

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy quá trình phúc lợi
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWelfareProccess(ByVal _empId As Decimal) As List(Of WelfareMngDTO)
        Try
            Dim query As List(Of WelfareMngDTO)
            query = (From p In Context.HU_WELFARE_MNG
                     From l In Context.HU_WELFARE_LIST.Where(Function(l) l.ID = p.WELFARE_ID)
                     Where p.EMPLOYEE_ID = _empId Order By p.EFFECT_DATE
                     Select New WelfareMngDTO With {
                     .WELFARE_NAME = l.NAME,
                     .EFFECT_DATE = p.EFFECT_DATE,
                     .EXPIRE_DATE = p.EXPIRE_DATE,
                     .MONEY = p.MONEY}).ToList()
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy quá trình hợp đồng
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractProccess(ByVal _empId As Decimal) As List(Of ContractDTO)
        Try
            Dim query As List(Of ContractDTO)
            query = (From p In Context.HU_CONTRACT
                     From t In Context.HU_CONTRACT_TYPE.Where(Function(t) t.ID = p.CONTRACT_TYPE_ID)
                     Where p.EMPLOYEE_ID = _empId _
                     And p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Order By p.START_DATE
                     Select New ContractDTO With {
                     .CONTRACTTYPE_NAME = t.NAME,
                     .START_DATE = p.START_DATE,
                     .EXPIRE_DATE = p.EXPIRE_DATE,
                     .CONTRACT_NO = p.CONTRACT_NO,
                     .SIGNER_NAME = p.SIGNER_NAME,
                     .SIGNER_TITLE = p.SIGNER_TITLE,
                     .SIGN_DATE = p.SIGN_DATE}).ToList()
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy quá trình khen thưởng
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommendProccess(ByVal _empId As Decimal) As List(Of CommendDTO)
        Try
            Dim query As List(Of CommendDTO)
            query = (From p In Context.HU_COMMEND
                     From ce In Context.HU_COMMEND_EMP.Where(Function(ce) ce.HU_COMMEND_ID = p.ID).DefaultIfEmpty
                     From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = ce.HU_EMPLOYEE_ID).DefaultIfEmpty
                     From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = emp.ORG_ID).DefaultIfEmpty
                     From lv In Context.HU_COMMEND_LEVEL.Where(Function(f) f.ID = p.COMMEND_LEVEL).DefaultIfEmpty
                     From t In Context.HU_COMMEND_LIST.Where(Function(f) f.ID = p.COMMEND_TYPE).DefaultIfEmpty
                     From title In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID).DefaultIfEmpty
                     From dhkt In Context.HU_COMMEND_LIST.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                     From httt In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.COMMEND_PAY And f.TYPE_CODE = "COMMEND_PAY" And f.ACTFLG = "A").DefaultIfEmpty
            Where (ce.HU_EMPLOYEE_ID = _empId And p.STATUS_ID = ProfileCommon.COMMEND_STATUS.APPROVE_ID)
                     Order By p.EFFECT_DATE
                     Select New CommendDTO With {
                     .DECISION_NO = p.NO,
                     .EFFECT_DATE = p.EFFECT_DATE,
                     .EXPIRE_DATE = p.EXPIRE_DATE,
                     .TITLE_NAME = title.NAME_VN,
                     .COMMEND_LEVEL_NAME = lv.NAME,
                     .ORG_NAME = o.NAME_VN,
                     .COMMEND_TYPE_NAME = t.NAME,
                     .REMARK = p.REMARK,
                     .MONEY = ce.MONEY,
                     .COMMEND_TITLE_NAME = dhkt.NAME,
                     .YEAR = p.YEAR,
                     .COMMEND_PAY_NAME = httt.NAME_VN,
                     .SIGNER_NAME = p.SIGNER_NAME}).ToList()

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy quá trình kỷ luật
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDisciplineProccess(ByVal _empId As Decimal) As List(Of DisciplineDTO)
        Try
            Dim query As List(Of DisciplineDTO)
            query = (From p In Context.HU_DISCIPLINE
                     From de In Context.HU_DISCIPLINE_EMP.Where(Function(de) de.HU_DISCIPLINE_ID = p.ID)
                     From lv In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DISCIPLINE_LEVEL).DefaultIfEmpty
                     From t In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DISCIPLINE_TYPE).DefaultIfEmpty
                     From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = de.HU_EMPLOYEE_ID)
                     From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = emp.ORG_ID)
                     From title In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID).DefaultIfEmpty
                     From reason In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DISCIPLINE_REASON And f.ACTFLG = "A").DefaultIfEmpty
                     Where de.HU_EMPLOYEE_ID = _empId And p.STATUS_ID = ProfileCommon.OT_DISCIPLINE_STATUS.APPROVE_ID Order By p.EFFECT_DATE
                     Select New DisciplineDTO With {
                     .DECISION_NO = p.NO,
                     .EFFECT_DATE = p.EFFECT_DATE,
                     .DISCIPLINE_DATE = p.EFFECT_DATE,
                     .EXPIRE_DATE = p.EXPIRE_DATE,
                     .DISCIPLINE_LEVEL_NAME = lv.NAME_VN,
                     .TITLE_NAME = title.NAME_VN,
                     .ORG_NAME = o.NAME_VN,
                     .DISCIPLINE_TYPE_NAME = t.NAME_VN,
                     .MONEY = de.MONEY,
                     .SIGN_DATE = p.SIGN_DATE,
                     .DISCIPLINE_REASON_NAME = reason.NAME_VN}).ToList()
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy quá trình đóng bảo hiểm
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInsuranceProccess(ByVal _empId As Decimal) As DataTable
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE.INSURANCE_PROCESSS",
                                                    New With {.P_EMPLOYEEID = _empId,
                                                                .CUR = cls.OUT_CURSOR})
                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeHistory(ByVal _empId As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_BUSINESS.GET_EMP_HISTORY",
                                           New With {.P_EMPLOYEE_ID = _empId,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy Qua trinh danh gia KPI
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAssessKPIEmployee(ByVal _empId As Decimal) As List(Of EmployeeAssessmentDTO)
        Try
            Dim lst As List(Of EmployeeAssessmentDTO) = New List(Of EmployeeAssessmentDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_PE_ASSESS_EMP_BY_EMP",
                                           New With {.P_EMPLOYEE_ID = _empId,
                                                    .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    lst = (From row As DataRow In dtData.Rows
                       Select New EmployeeAssessmentDTO With {.ID = row("ID").ToString(),
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
                                                   .PE_STATUS_ID = row("PE_STATUS_ID").ToString(),
                                                   .PE_STATUS_NAME = row("PE_STATUS_NAME").ToString(),
                                                    .CLASSIFICATION_NAME = row("CLASSIFICATION_NAME").ToString()
                                                  }).Where(Function(f) f.PE_STATUS_ID = ProfileCommon.OT_PEASSESSMENT.STATUS_ASS).ToList
                End If
            End Using

            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    'Qua trinh nang luc
    Public Function GetCompetencyEmployee(ByVal _empId As Decimal) As List(Of EmployeeCompetencyDTO)
        Try
            Dim query = From stand In Context.HU_COMPETENCY_STANDARD
                     From Competency In Context.HU_COMPETENCY.Where(Function(f) f.ID = stand.COMPETENCY_ID)
                     From Competencygroup In Context.HU_COMPETENCY_GROUP.Where(Function(f) f.ID = Competency.COMPETENCY_GROUP_ID)
                     From ass In Context.HU_COMPETENCY_ASS.Where(Function(f) stand.TITLE_ID = f.TITLE_ID).DefaultIfEmpty
                     From p In Context.HU_COMPETENCY_ASSDTL.Where(Function(f) f.COMPETENCY_ASS_ID = ass.ID And
                                                                      f.COMPETENCY_ID = stand.COMPETENCY_ID).DefaultIfEmpty
                     From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = ass.EMPLOYEE_ID).DefaultIfEmpty
                     From period In Context.HU_COMPETENCY_PERIOD.Where(Function(f) f.ID = ass.COMPETENCY_PERIOD_ID).DefaultIfEmpty
                     Where ass.EMPLOYEE_ID = _empId
                     Select New EmployeeCompetencyDTO With {
                         .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                         .EMPLOYEE_ID = ass.EMPLOYEE_ID,
                         .EMPLOYEE_NAME = e.FULLNAME_VN,
                         .TITLE_ID = stand.TITLE_ID,
                         .COMPETENCY_GROUP_ID = Competency.COMPETENCY_GROUP_ID,
                         .COMPETENCY_GROUP_NAME = Competencygroup.NAME,
                         .COMPETENCY_PERIOD_ID = period.ID,
                         .COMPETENCY_PERIOD_NAME = period.NAME,
                         .COMPETENCY_PERIOD_YEAR = period.YEAR,
                         .COMPETENCY_ID = stand.COMPETENCY_ID,
                         .COMPETENCY_NAME = Competency.NAME,
                         .LEVEL_NUMBER_STANDARD = stand.LEVEL_NUMBER,
                         .LEVEL_NUMBER_EMP = p.LEVEL_NUMBER,
                         .REMARK = p.REMARK,
                         .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "GetCompetencyEmployee")
            Throw ex
        End Try
    End Function


#End Region

#Region "Qua trinh dao tao trong cong ty"
    Public Function GetEmployeeTrainForCompany(ByVal _filter As EmployeeTrainForCompanyDTO) As List(Of EmployeeTrainForCompanyDTO)
        Try
            Dim query = From re In Context.TR_REQUEST_EMPLOYEE
                         From r In Context.TR_REQUEST.Where(Function(f) f.ID = re.TR_REQUEST_ID).DefaultIfEmpty
                         From c In Context.TR_COURSE.Where(Function(f) f.ID = r.TR_COURSE_ID).DefaultIfEmpty
                         From ce In Context.TR_CERTIFICATE.Where(Function(f) f.ID = c.TR_CERTIFICATE_ID)
                         From pr In Context.TR_PROGRAM.Where(Function(f) f.TR_REQUEST_ID = r.ID).DefaultIfEmpty
                         From prg In Context.TR_PROGRAM_GROUP.Where(Function(f) f.ID = c.TR_PROGRAM_GROUP).DefaultIfEmpty
                         From tf In Context.OT_OTHER_LIST.Where(Function(f) f.ID = c.TR_TRAIN_FIELD).DefaultIfEmpty
                         From tfr In Context.OT_OTHER_LIST.Where(Function(f) f.ID = r.TRAIN_FORM_ID).DefaultIfEmpty
                         From lang In Context.OT_OTHER_LIST.Where(Function(f) f.ID = pr.TR_LANGUAGE_ID).DefaultIfEmpty
                         From result In Context.TR_PROGRAM_RESULT.Where(Function(f) f.TR_PROGRAM_ID = pr.ID And f.EMPLOYEE_ID = _filter.EMPLOYEE_ID).DefaultIfEmpty
                         From rank In Context.OT_OTHER_LIST.Where(Function(f) f.ID = result.TR_RANK_ID).DefaultIfEmpty
                         From pcomit In Context.TR_PROGRAM_COMMIT.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID And f.TR_PROGRAM_ID = pr.ID).DefaultIfEmpty
                         Where re.EMPLOYEE_ID = _filter.EMPLOYEE_ID
                         Order By re.ID Descending

            Dim lst = query.Select(Function(p) New EmployeeTrainForCompanyDTO With {
                                         .ID = _filter.EMPLOYEE_ID,
                                         .EMPLOYEE_ID = _filter.EMPLOYEE_ID,
                                         .TR_COURSE_ID = p.r.TR_COURSE_ID,
                                         .TR_COURSE_NAME = p.c.NAME,
                                         .TR_PROGRAM_ID = p.pr.ID,
                                         .TR_PROGRAM_NAME = p.pr.NAME,
                                         .TR_PROGRAM_GROUP_ID = p.prg.ID,
                                         .TR_PROGRAM_GROUP_NAME = p.prg.NAME,
                                         .FIELDS_ID = p.tf.ID,
                                         .TR_TRAIN_FIELD_NAME = p.tf.NAME_VN,
                                         .TR_TRAIN_FORM_ID = p.r.TRAIN_FORM_ID,
                                         .TR_TRAIN_FORM_NAME = p.tfr.NAME_VN,
                                         .DURATION = p.pr.DURATION,
                                         .START_DATE = p.pr.START_DATE,
                                         .END_DATE = p.pr.END_DATE,
                                         .DURATION_HC = p.pr.DURATION_HC,
                                         .DURATION_OT = p.pr.DURATION_OT,
                                         .COST_TOTAL = p.pr.COST_TOTAL,
                                         .COST_OF_STUDENT = p.pr.COST_STUDENT,
                                         .COST_TOTAL_USD = p.pr.COST_TOTAL_US,
                                         .COST_OF_STUDENT_USD = p.pr.COST_STUDENT_US,
                                         .NO_OF_STUDENT = p.pr.STUDENT_NUMBER,
                                         .IS_REIMBURSE = p.pr.IS_REIMBURSE,
                                         .TR_LANGUAGE_ID = p.pr.TR_LANGUAGE_ID,
                                         .TR_LANGUAGE_NAME = p.lang.NAME_VN,
                                         .TR_UNIT_NAME = p.pr.CENTERS,
                                         .CONTENT = p.pr.CONTENT,
                                         .TARGET_TRAIN = p.pr.TARGET_TRAIN,
                                         .VENUE = p.pr.VENUE,
                                         .IS_EXAMS = If(p.result.RETEST_SCORE Is Nothing, "Không", "Có"),
                                         .IS_END = If(p.result.IS_END = -1, True, False),
                                         .IS_REACH = If(p.result.IS_REACH = -1, "Đạt", "Không đạt"),
                                         .IS_CERTIFICATE = p.result.IS_CERTIFICATE,
                                         .CERTIFICATE_NO = p.result.CERTIFICATE_NO,
                                         .CERTIFICATE_DATE = p.result.CERTIFICATE_DATE,
                                         .CER_RECEIVE_DATE = p.result.CER_RECEIVE_DATE,
                                         .CERTIFICATE_DURATION = p.ce.DURATION,
                                         .COMITMENT_TRAIN_NO = p.pcomit.COMMIT_NO,
                                         .COMMIT_WORK = p.result.COMMIT_WORKMONTH,
                                         .COMITMENT_START_DATE = p.result.COMMIT_STARTDATE,
                                         .COMITMENT_END_DATE = p.result.COMMIT_ENDDATE,
                                         .RANK_ID = p.rank.ID,
                                         .RANK_NAME = p.rank.NAME_VN,
                                         .TOEIC_FINAL_SCORE = p.result.FINAL_SCORE,
                                        .REMARK = p.r.REMARK})
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "Quá trình đào tạo ngoài vào công ty"
    Public Function GetProcessTraining(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTO,
                                Optional ByRef PageIndex As Integer = 0,
                                  Optional ByVal PageSize As Integer = Integer.MaxValue,
                                  Optional ByVal Total As Integer = 0,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO)
        Try

            Dim query = From p In Context.HU_PRO_TRAIN_OUT_COMPANY
                        From ot In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.FORM_TRAIN_ID And F.TYPE_ID = 142).DefaultIfEmpty
                        From ott In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.TYPE_TRAIN_ID And F.TYPE_ID = 143).DefaultIfEmpty
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_ID) Then
                query = query.Where(Function(f) f.p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If

            'If Not String.IsNullOrEmpty(_filter.ARISING_NAME_OT) Then
            '    query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ARISING_NAME_OT.ToLower()))
            'End If
            'If Not String.IsNullOrEmpty(_filter.NOTE) Then
            '    query = query.Where(Function(f) f.p.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            'End If

            Dim lst = query.Select(Function(p) New HU_PRO_TRAIN_OUT_COMPANYDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .FROM_DATE = p.p.FROM_DATE,
                                       .TO_DATE = p.p.TO_DATE,
                                       .YEAR_GRA = p.p.YEAR_GRA,
                                       .NAME_SHOOLS = p.p.NAME_SHOOLS,
                                       .FORM_TRAIN_ID = p.p.FORM_TRAIN_ID,
                                       .FORM_TRAIN_NAME = p.ot.NAME_VN,
                                       .UPLOAD_FILE = p.p.UPLOAD_FILE,
                                       .FILE_NAME = p.p.FILE_NAME,
                                       .SPECIALIZED_TRAIN = p.p.SPECIALIZED_TRAIN,
                                       .RESULT_TRAIN = p.p.RESULT_TRAIN,
                                       .CERTIFICATE = p.p.CERTIFICATE,
                                       .EFFECTIVE_DATE_FROM = p.p.EFFECTIVE_DATE_FROM,
                                       .EFFECTIVE_DATE_TO = p.p.EFFECTIVE_DATE_TO,
                                       .TYPE_TRAIN_ID = p.p.TYPE_TRAIN_ID,
                                       .TYPE_TRAIN_NAME = p.ott.NAME_VN,
                                       .RECEIVE_DEGREE_DATE = p.p.RECEIVE_DEGREE_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertProcessTraining(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New HU_PRO_TRAIN_OUT_COMPANY
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.HU_PRO_TRAIN_OUT_COMPANY.EntitySet.Name)
            objTitleData.FROM_DATE = objTitle.FROM_DATE
            objTitleData.TO_DATE = objTitle.TO_DATE
            objTitleData.YEAR_GRA = objTitle.YEAR_GRA
            objTitleData.NAME_SHOOLS = objTitle.NAME_SHOOLS
            objTitleData.UPLOAD_FILE = objTitle.UPLOAD_FILE
            objTitleData.FILE_NAME = objTitle.FILE_NAME
            objTitleData.FORM_TRAIN_ID = objTitle.FORM_TRAIN_ID
            objTitleData.SPECIALIZED_TRAIN = objTitle.SPECIALIZED_TRAIN
            objTitleData.RESULT_TRAIN = objTitle.RESULT_TRAIN
            objTitleData.CERTIFICATE = objTitle.CERTIFICATE
            objTitleData.EFFECTIVE_DATE_FROM = objTitle.EFFECTIVE_DATE_FROM
            objTitleData.EFFECTIVE_DATE_TO = objTitle.EFFECTIVE_DATE_TO
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.TYPE_TRAIN_ID = objTitle.TYPE_TRAIN_ID
            objTitleData.RECEIVE_DEGREE_DATE = objTitle.RECEIVE_DEGREE_DATE
            Context.HU_PRO_TRAIN_OUT_COMPANY.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyProcessTraining(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New HU_PRO_TRAIN_OUT_COMPANY With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.FROM_DATE = objTitle.FROM_DATE
            objTitleData.TO_DATE = objTitle.TO_DATE
            objTitleData.UPLOAD_FILE = objTitle.UPLOAD_FILE
            objTitleData.FILE_NAME = objTitle.FILE_NAME
            objTitleData.YEAR_GRA = objTitle.YEAR_GRA
            objTitleData.NAME_SHOOLS = objTitle.NAME_SHOOLS
            objTitleData.FORM_TRAIN_ID = objTitle.FORM_TRAIN_ID
            objTitleData.SPECIALIZED_TRAIN = objTitle.SPECIALIZED_TRAIN
            objTitleData.RESULT_TRAIN = objTitle.RESULT_TRAIN
            objTitleData.CERTIFICATE = objTitle.CERTIFICATE
            objTitleData.EFFECTIVE_DATE_FROM = objTitle.EFFECTIVE_DATE_FROM
            objTitleData.EFFECTIVE_DATE_TO = objTitle.EFFECTIVE_DATE_TO
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.TYPE_TRAIN_ID = objTitle.TYPE_TRAIN_ID
            objTitleData.RECEIVE_DEGREE_DATE = objTitle.RECEIVE_DEGREE_DATE
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteProcessTraining(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstInsChangeTypeData As List(Of HU_PRO_TRAIN_OUT_COMPANY)
        Try
            lstInsChangeTypeData = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstInsChangeTypeData.Count - 1
                Context.HU_PRO_TRAIN_OUT_COMPANY.DeleteObject(lstInsChangeTypeData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function
#End Region

#Region "EmployeeEdit"

    Public Function InsertEmployeeEdit(ByVal objEmployeeEdit As EmployeeEditDTO,
                                             ByVal log As UserLog,
                                             ByRef gID As Decimal) As Boolean
        Try

            Dim objEmployeeEditData As New HU_EMPLOYEE_EDIT
            objEmployeeEditData.ID = Utilities.GetNextSequence(Context, Context.HU_EMPLOYEE_EDIT.EntitySet.Name)
            objEmployeeEditData.EMPLOYEE_ID = objEmployeeEdit.EMPLOYEE_ID
            objEmployeeEditData.ID_DATE = objEmployeeEdit.ID_DATE
            objEmployeeEditData.ID_NO = objEmployeeEdit.ID_NO
            objEmployeeEditData.ID_PLACE = objEmployeeEdit.ID_PLACE
            objEmployeeEditData.MARITAL_STATUS = objEmployeeEdit.MARITAL_STATUS
            objEmployeeEditData.NAV_ADDRESS = objEmployeeEdit.NAV_ADDRESS
            objEmployeeEditData.NAV_DISTRICT = objEmployeeEdit.NAV_DISTRICT
            objEmployeeEditData.NAV_PROVINCE = objEmployeeEdit.NAV_PROVINCE
            objEmployeeEditData.NAV_WARD = objEmployeeEdit.NAV_WARD
            objEmployeeEditData.PER_ADDRESS = objEmployeeEdit.PER_ADDRESS
            objEmployeeEditData.PER_DISTRICT = objEmployeeEdit.PER_DISTRICT
            objEmployeeEditData.PER_PROVINCE = objEmployeeEdit.PER_PROVINCE
            objEmployeeEditData.PER_WARD = objEmployeeEdit.PER_WARD
            objEmployeeEditData.STATUS = 0
            Context.HU_EMPLOYEE_EDIT.AddObject(objEmployeeEditData)
            Context.SaveChanges(log)
            gID = objEmployeeEditData.ID
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyEmployeeEdit(ByVal objEmployeeEdit As EmployeeEditDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objEmployeeEditData As New HU_EMPLOYEE_EDIT With {.ID = objEmployeeEdit.ID}
        Try
            objEmployeeEditData = (From p In Context.HU_EMPLOYEE_EDIT Where p.ID = objEmployeeEdit.ID).FirstOrDefault
            objEmployeeEditData.EMPLOYEE_ID = objEmployeeEdit.EMPLOYEE_ID
            objEmployeeEditData.ID_DATE = objEmployeeEdit.ID_DATE
            objEmployeeEditData.ID_NO = objEmployeeEdit.ID_NO
            objEmployeeEditData.ID_PLACE = objEmployeeEdit.ID_PLACE
            objEmployeeEditData.MARITAL_STATUS = objEmployeeEdit.MARITAL_STATUS
            objEmployeeEditData.NAV_ADDRESS = objEmployeeEdit.NAV_ADDRESS
            objEmployeeEditData.NAV_DISTRICT = objEmployeeEdit.NAV_DISTRICT
            objEmployeeEditData.NAV_PROVINCE = objEmployeeEdit.NAV_PROVINCE
            objEmployeeEditData.NAV_WARD = objEmployeeEdit.NAV_WARD
            objEmployeeEditData.PER_ADDRESS = objEmployeeEdit.PER_ADDRESS
            objEmployeeEditData.PER_DISTRICT = objEmployeeEdit.PER_DISTRICT
            objEmployeeEditData.PER_PROVINCE = objEmployeeEdit.PER_PROVINCE
            objEmployeeEditData.PER_WARD = objEmployeeEdit.PER_WARD
            objEmployeeEditData.STATUS = 0

            Context.SaveChanges(log)
            gID = objEmployeeEditData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GetEmployeeEditByID(ByVal _filter As EmployeeEditDTO) As EmployeeEditDTO
        Dim objEmpEdit As EmployeeEditDTO
        Try
            Dim existEdit = (From p In Context.HU_EMPLOYEE_EDIT
                             Where p.EMPLOYEE_ID = _filter.EMPLOYEE_ID And _
                             p.STATUS <> 2).Any

            If existEdit Then
                objEmpEdit = (From p In Context.HU_EMPLOYEE_EDIT
                              From nav_pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.NAV_PROVINCE).DefaultIfEmpty
                              From nav_dis In Context.HU_DISTRICT.Where(Function(f) f.ID = p.NAV_DISTRICT).DefaultIfEmpty
                              From nav_ward In Context.HU_WARD.Where(Function(f) f.ID = p.NAV_WARD).DefaultIfEmpty
                              From per_pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PER_PROVINCE).DefaultIfEmpty
                              From per_dis In Context.HU_DISTRICT.Where(Function(f) f.ID = p.PER_DISTRICT).DefaultIfEmpty
                              From per_ward In Context.HU_WARD.Where(Function(f) f.ID = p.PER_WARD).DefaultIfEmpty
                              From marital In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MARITAL_STATUS).DefaultIfEmpty
                              Where p.EMPLOYEE_ID = _filter.EMPLOYEE_ID And p.STATUS <> 2
                             Select New EmployeeEditDTO With {
                                 .ID = p.ID,
                                 .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                 .ID_DATE = p.ID_DATE,
                                 .ID_NO = p.ID_NO,
                                 .ID_PLACE = p.ID_PLACE,
                                 .MARITAL_STATUS = p.MARITAL_STATUS,
                                 .MARITAL_STATUS_NAME = marital.NAME_VN,
                                 .NAV_ADDRESS = p.NAV_ADDRESS,
                                 .NAV_DISTRICT = p.NAV_DISTRICT,
                                 .NAV_DISTRICT_NAME = nav_dis.NAME_VN,
                                 .NAV_PROVINCE = p.NAV_PROVINCE,
                                 .NAV_PROVINCE_NAME = nav_pro.NAME_VN,
                                 .NAV_WARD = p.NAV_WARD,
                                 .NAV_WARD_NAME = nav_ward.NAME_VN,
                                 .PER_ADDRESS = p.PER_ADDRESS,
                                 .PER_DISTRICT = p.PER_DISTRICT,
                                 .PER_DISTRICT_NAME = per_dis.NAME_VN,
                                 .PER_PROVINCE = p.PER_PROVINCE,
                                 .PER_PROVINCE_NAME = per_pro.NAME_VN,
                                 .PER_WARD = p.PER_WARD,
                                 .PER_WARD_NAME = per_ward.NAME_VN,
                                 .REASON_UNAPROVE = p.REASON_UNAPROVE,
                                 .STATUS = p.STATUS,
                                 .STATUS_NAME = If(p.STATUS = 0, "Chưa gửi duyệt",
                                                   If(p.STATUS = 1, "Chờ phê duyệt",
                                                      If(p.STATUS = 2, "Phê duyệt",
                                                         If(p.STATUS = 3, "Không phê duyệt", ""))))}).FirstOrDefault
            Else
                objEmpEdit = (From p In Context.HU_EMPLOYEE_CV
                              From nav_pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.NAV_PROVINCE).DefaultIfEmpty
                              From nav_dis In Context.HU_DISTRICT.Where(Function(f) f.ID = p.NAV_DISTRICT).DefaultIfEmpty
                              From nav_ward In Context.HU_WARD.Where(Function(f) f.ID = p.NAV_WARD).DefaultIfEmpty
                              From per_pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PER_PROVINCE).DefaultIfEmpty
                              From per_dis In Context.HU_DISTRICT.Where(Function(f) f.ID = p.PER_DISTRICT).DefaultIfEmpty
                              From per_ward In Context.HU_WARD.Where(Function(f) f.ID = p.PER_WARD).DefaultIfEmpty
                              From marital In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MARITAL_STATUS).DefaultIfEmpty
                              Where p.EMPLOYEE_ID = _filter.EMPLOYEE_ID
                              Select New EmployeeEditDTO With {
                                  .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                  .ID_DATE = p.ID_DATE,
                                  .ID_NO = p.ID_NO,
                                  .ID_PLACE = p.ID_PLACE,
                                  .MARITAL_STATUS = p.MARITAL_STATUS,
                                  .MARITAL_STATUS_NAME = marital.NAME_VN,
                                 .NAV_ADDRESS = p.NAV_ADDRESS,
                                 .NAV_DISTRICT = p.NAV_DISTRICT,
                                 .NAV_DISTRICT_NAME = nav_dis.NAME_VN,
                                 .NAV_PROVINCE = p.NAV_PROVINCE,
                                 .NAV_PROVINCE_NAME = nav_pro.NAME_VN,
                                 .NAV_WARD = p.NAV_WARD,
                                 .NAV_WARD_NAME = nav_ward.NAME_VN,
                                 .PER_ADDRESS = p.PER_ADDRESS,
                                 .PER_DISTRICT = p.PER_DISTRICT,
                                 .PER_DISTRICT_NAME = per_dis.NAME_VN,
                                 .PER_PROVINCE = p.PER_PROVINCE,
                                 .PER_PROVINCE_NAME = per_pro.NAME_VN,
                                 .PER_WARD = p.PER_WARD,
                                 .PER_WARD_NAME = per_ward.NAME_VN}).FirstOrDefault
            End If

            Return objEmpEdit
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetApproveEmployeeEdit(ByVal _filter As EmployeeEditDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of EmployeeEditDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = (From p In Context.HU_EMPLOYEE_EDIT
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                              From nav_pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.NAV_PROVINCE).DefaultIfEmpty
                              From nav_dis In Context.HU_DISTRICT.Where(Function(f) f.ID = p.NAV_DISTRICT).DefaultIfEmpty
                              From nav_ward In Context.HU_WARD.Where(Function(f) f.ID = p.NAV_WARD).DefaultIfEmpty
                              From per_pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PER_PROVINCE).DefaultIfEmpty
                              From per_dis In Context.HU_DISTRICT.Where(Function(f) f.ID = p.PER_DISTRICT).DefaultIfEmpty
                              From per_ward In Context.HU_WARD.Where(Function(f) f.ID = p.PER_WARD).DefaultIfEmpty
                              From marital In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MARITAL_STATUS).DefaultIfEmpty
                              From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID _
                                                                             And f.USERNAME = log.Username.ToUpper)
                        Where p.STATUS = 1
                        Select New EmployeeEditDTO With {
                            .ID = p.ID,
                            .EMPLOYEE_ID = p.EMPLOYEE_ID,
                            .EMPLOYEE_NAME = e.FULLNAME_VN,
                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                            .ID_DATE = p.ID_DATE,
                            .ID_NO = p.ID_NO,
                            .ID_PLACE = p.ID_PLACE,
                            .MARITAL_STATUS = p.MARITAL_STATUS,
                            .MARITAL_STATUS_NAME = marital.NAME_VN,
                            .NAV_ADDRESS = p.NAV_ADDRESS,
                            .NAV_DISTRICT = p.NAV_DISTRICT,
                            .NAV_DISTRICT_NAME = nav_dis.NAME_VN,
                            .NAV_PROVINCE = p.NAV_PROVINCE,
                            .NAV_PROVINCE_NAME = nav_pro.NAME_VN,
                            .NAV_WARD = p.NAV_WARD,
                            .NAV_WARD_NAME = nav_ward.NAME_VN,
                            .PER_ADDRESS = p.PER_ADDRESS,
                            .PER_DISTRICT = p.PER_DISTRICT,
                            .PER_DISTRICT_NAME = per_dis.NAME_VN,
                            .PER_PROVINCE = p.PER_PROVINCE,
                            .PER_PROVINCE_NAME = per_pro.NAME_VN,
                            .PER_WARD = p.PER_WARD,
                            .PER_WARD_NAME = per_ward.NAME_VN,
                            .STATUS = p.STATUS,
                            .STATUS_NAME = If(p.STATUS = 0, "Chưa gửi duyệt",
                                              If(p.STATUS = 1, "Chờ phê duyệt",
                                                 If(p.STATUS = 2, "Phê duyệt",
                                                    If(p.STATUS = 3, "Không phê duyệt", ""))))})

            If _filter.STATUS_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.STATUS_NAME.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If

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

    Public Function DeleteEmployeeEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_EMPLOYEE_EDIT)
        Try
            lst = (From p In Context.HU_EMPLOYEE_EDIT Where lstDecimals.Contains(p.ID)).ToList
            For i As Int16 = 0 To lst.Count - 1
                Context.HU_EMPLOYEE_EDIT.DeleteObject(lst(i))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function SendEmployeeEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean

        Try
            Dim lstObj = (From p In Context.HU_EMPLOYEE_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lstObj
                item.SEND_DATE = Date.Now
                item.STATUS = 1
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function UpdateStatusEmployeeEdit(ByVal lstID As List(Of Decimal),
                                                   ByVal status As String,
                                                   ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_EMPLOYEE_EDIT)
        Dim sStatus() As String = status.Split(":")

        Try
            lst = (From p In Context.HU_EMPLOYEE_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lst
                item.STATUS = sStatus(0)
                item.REASON_UNAPROVE = sStatus(1)

                If sStatus(0) = 2 Then
                    Dim objEmployeeData As HU_EMPLOYEE_CV
                    objEmployeeData = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = item.EMPLOYEE_ID).FirstOrDefault
                    objEmployeeData.ID_DATE = item.ID_DATE
                    objEmployeeData.ID_NO = item.ID_NO
                    objEmployeeData.ID_PLACE = item.ID_PLACE
                    objEmployeeData.MARITAL_STATUS = item.MARITAL_STATUS
                    objEmployeeData.NAV_ADDRESS = item.NAV_ADDRESS
                    objEmployeeData.NAV_DISTRICT = item.NAV_DISTRICT
                    objEmployeeData.NAV_PROVINCE = item.NAV_PROVINCE
                    objEmployeeData.NAV_WARD = item.NAV_WARD
                    objEmployeeData.PER_ADDRESS = item.PER_ADDRESS
                    objEmployeeData.PER_DISTRICT = item.PER_DISTRICT
                    objEmployeeData.PER_PROVINCE = item.PER_PROVINCE
                    objEmployeeData.PER_WARD = item.PER_WARD
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "IPORTAL - Quá trình đào tạo ngoài vào công ty"

    Public Function GetProcessTrainingEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
        Dim query As ObjectQuery(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
        Try
            query = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT
                     From ot In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.FORM_TRAIN_ID And F.TYPE_ID = 142).DefaultIfEmpty
                     Select New HU_PRO_TRAIN_OUT_COMPANYDTOEDIT With {
                        .ID = p.ID,
                        .EMPLOYEE_ID = p.EMPLOYEE_ID,
                        .FROM_DATE = p.FROM_DATE,
                        .TO_DATE = p.TO_DATE,
                        .YEAR_GRA = p.YEAR_GRA,
                        .NAME_SHOOLS = p.NAME_SHOOLS,
                        .FORM_TRAIN_ID = p.FORM_TRAIN_ID,
                        .FORM_TRAIN_NAME = ot.NAME_VN,
                        .SPECIALIZED_TRAIN = p.SPECIALIZED_TRAIN,
                        .RESULT_TRAIN = p.RESULT_TRAIN,
                        .CERTIFICATE = p.CERTIFICATE,
                        .EFFECTIVE_DATE_FROM = p.EFFECTIVE_DATE_FROM,
                        .EFFECTIVE_DATE_TO = p.EFFECTIVE_DATE_TO,
                        .CREATED_BY = p.CREATED_BY,
                        .CREATED_DATE = p.CREATED_DATE,
                        .CREATED_LOG = p.CREATED_LOG,
                        .MODIFIED_BY = p.MODIFIED_BY,
                        .MODIFIED_DATE = p.MODIFIED_DATE,
                        .MODIFIED_LOG = p.MODIFIED_LOG,
                        .REASON_UNAPROVE = p.REASON_UNAPROVE,
                        .FK_PKEY = p.FK_PKEY,
                        .STATUS = p.STATUS,
                        .STATUS_NAME = If(p.STATUS = 0, "Chưa gửi duyệt",
                                           If(p.STATUS = 1, "Chờ phê duyệt",
                                              If(p.STATUS = 2, "Phê duyệt",
                                                 If(p.STATUS = 3, "Không phê duyệt", ""))))})

            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.STATUS <> "" Then
                query = query.Where(Function(p) p.STATUS = _filter.STATUS)
            End If
            If _filter.ID <> 0 Then
                query = query.Where(Function(p) p.ID = _filter.ID)
            End If

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetApproveProcessTrainingEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_ID desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT
                        From ot In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.FORM_TRAIN_ID And F.TYPE_ID = 142).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        Where p.STATUS = 1
                        Select New HU_PRO_TRAIN_OUT_COMPANYDTOEDIT With {
                            .ID = p.ID,
                            .EMPLOYEE_ID = p.EMPLOYEE_ID,
                            .FROM_DATE = p.FROM_DATE,
                            .TO_DATE = p.TO_DATE,
                            .YEAR_GRA = p.YEAR_GRA,
                            .NAME_SHOOLS = p.NAME_SHOOLS,
                            .FORM_TRAIN_ID = p.FORM_TRAIN_ID,
                            .FORM_TRAIN_NAME = ot.NAME_VN,
                            .SPECIALIZED_TRAIN = p.SPECIALIZED_TRAIN,
                            .RESULT_TRAIN = p.RESULT_TRAIN,
                            .CERTIFICATE = p.CERTIFICATE,
                            .EFFECTIVE_DATE_FROM = p.EFFECTIVE_DATE_FROM,
                            .EFFECTIVE_DATE_TO = p.EFFECTIVE_DATE_TO,
                            .CREATED_BY = p.CREATED_BY,
                            .CREATED_DATE = p.CREATED_DATE,
                            .CREATED_LOG = p.CREATED_LOG,
                            .MODIFIED_BY = p.MODIFIED_BY,
                            .MODIFIED_DATE = p.MODIFIED_DATE,
                            .MODIFIED_LOG = p.MODIFIED_LOG,
                            .FK_PKEY = p.FK_PKEY,
                            .STATUS = p.STATUS,
                            .STATUS_NAME = If(p.STATUS = 0, "Chưa gửi duyệt",
                                              If(p.STATUS = 1, "Chờ phê duyệt",
                                                 If(p.STATUS = 2, "Phê duyệt",
                                                    If(p.STATUS = 3, "Không phê duyệt", ""))))})


            If _filter.YEAR_GRA IsNot Nothing Then
                query = query.Where(Function(p) p.YEAR_GRA = _filter.YEAR_GRA)
            End If

            If _filter.NAME_SHOOLS IsNot Nothing Then
                query = query.Where(Function(p) p.NAME_SHOOLS.ToUpper.Contains(_filter.NAME_SHOOLS.ToUpper))
            End If

            If _filter.FORM_TRAIN_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.FORM_TRAIN_NAME.ToUpper.Contains(_filter.FORM_TRAIN_NAME.ToUpper))
            End If

            If _filter.SPECIALIZED_TRAIN IsNot Nothing Then
                query = query.Where(Function(p) p.SPECIALIZED_TRAIN.ToUpper.Contains(_filter.SPECIALIZED_TRAIN.ToUpper))
            End If

            If _filter.RESULT_TRAIN IsNot Nothing Then
                query = query.Where(Function(p) p.RESULT_TRAIN.ToUpper.Contains(_filter.RESULT_TRAIN.ToUpper))
            End If

            If _filter.CERTIFICATE IsNot Nothing Then
                query = query.Where(Function(p) p.CERTIFICATE.ToUpper.Contains(_filter.CERTIFICATE.ToUpper))
            End If

            If _filter.STATUS_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.STATUS_NAME.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If

            If _filter.EFFECTIVE_DATE_FROM IsNot Nothing Then
                query = query.Where(Function(p) p.EFFECTIVE_DATE_FROM = _filter.EFFECTIVE_DATE_FROM)
            End If

            If _filter.EFFECTIVE_DATE_TO IsNot Nothing Then
                query = query.Where(Function(p) p.EFFECTIVE_DATE_TO = _filter.EFFECTIVE_DATE_TO)
            End If

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

    Public Function InsertProcessTrainingEdit(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New HU_PRO_TRAIN_OUT_COMPANY_EDIT
        Dim iCount As Integer = 0
        Try
            ' do tên bảng với tên sequence khác nhau
            Dim seq As Decimal?
            Dim tbl_name As String = "HU_PRO_TRAIN_OUT_COMPANY_EDIT"
            While (True)
                Try
                    seq = Context.ExecuteStoreQuery(Of Decimal?)("select SEQ_HU_PRO_TRAINOUTCOMPANYEDIT.nextval from DUAL").FirstOrDefault
                    Dim maxID As Decimal? = Context.ExecuteStoreQuery(Of Decimal?)("select Max(ID) from " & tbl_name).FirstOrDefault
                    If maxID IsNot Nothing AndAlso maxID >= seq Then
                        Continue While
                    End If
                    Exit While
                Catch ex As Exception
                    WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
                End Try
            End While

            objTitleData.ID = seq
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.FROM_DATE = objTitle.FROM_DATE
            objTitleData.TO_DATE = objTitle.TO_DATE
            objTitleData.YEAR_GRA = objTitle.YEAR_GRA
            objTitleData.NAME_SHOOLS = objTitle.NAME_SHOOLS
            objTitleData.FORM_TRAIN_ID = objTitle.FORM_TRAIN_ID
            objTitleData.SPECIALIZED_TRAIN = objTitle.SPECIALIZED_TRAIN
            objTitleData.RESULT_TRAIN = objTitle.RESULT_TRAIN
            objTitleData.CERTIFICATE = objTitle.CERTIFICATE
            objTitleData.EFFECTIVE_DATE_FROM = objTitle.EFFECTIVE_DATE_FROM
            objTitleData.EFFECTIVE_DATE_TO = objTitle.EFFECTIVE_DATE_TO
            objTitleData.STATUS = 0
            objTitleData.REASON_UNAPROVE = objTitle.REASON_UNAPROVE
            objTitleData.FK_PKEY = objTitle.FK_PKEY
            Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyProcessTrainingEdit(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New HU_PRO_TRAIN_OUT_COMPANY_EDIT With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT Where p.ID = objTitle.ID).FirstOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.FROM_DATE = objTitle.FROM_DATE
            objTitleData.TO_DATE = objTitle.TO_DATE
            objTitleData.YEAR_GRA = objTitle.YEAR_GRA
            objTitleData.NAME_SHOOLS = objTitle.NAME_SHOOLS
            objTitleData.FORM_TRAIN_ID = objTitle.FORM_TRAIN_ID
            objTitleData.SPECIALIZED_TRAIN = objTitle.SPECIALIZED_TRAIN
            objTitleData.RESULT_TRAIN = objTitle.RESULT_TRAIN
            objTitleData.CERTIFICATE = objTitle.CERTIFICATE
            objTitleData.EFFECTIVE_DATE_FROM = objTitle.EFFECTIVE_DATE_FROM
            objTitleData.EFFECTIVE_DATE_TO = objTitle.EFFECTIVE_DATE_TO
            objTitleData.REASON_UNAPROVE = objTitle.REASON_UNAPROVE
            objTitleData.FK_PKEY = objTitle.FK_PKEY
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteProcessTrainingEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstInsChangeTypeData As List(Of HU_PRO_TRAIN_OUT_COMPANY_EDIT)
        Try
            lstInsChangeTypeData = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT Where lstDecimals.Contains(p.ID)).ToList
            For index = 0 To lstInsChangeTypeData.Count - 1
                Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT.DeleteObject(lstInsChangeTypeData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

    Public Function CheckExistProcessTrainingEdit(ByVal pk_key As Decimal) As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT
        Try
            Dim query = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT
                         Where p.STATUS <> 2 And p.FK_PKEY = pk_key
                         Select New HU_PRO_TRAIN_OUT_COMPANYDTOEDIT With {
                             .ID = p.ID,
                             .STATUS = p.STATUS}).FirstOrDefault

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function SendProcessTrainingEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean

        Try
            Dim lstObj = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lstObj
                item.SEND_DATE = Date.Now
                item.STATUS = 1
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function UpdateStatusProcessTrainingEdit(ByVal lstID As List(Of Decimal),
                                                   ByVal status As String,
                                                   ByVal log As UserLog) As Boolean

        Dim lst As List(Of HU_PRO_TRAIN_OUT_COMPANY_EDIT)
        Dim sStatus() As String = status.Split(":")

        Try
            lst = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lst
                item.STATUS = sStatus(0)
                item.REASON_UNAPROVE = sStatus(1)

                If sStatus(0) = 2 Then
                    If item.FK_PKEY IsNot Nothing Then
                        Dim objProcessTrainData As New HU_PRO_TRAIN_OUT_COMPANY
                        objProcessTrainData = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY Where p.ID = item.FK_PKEY).FirstOrDefault
                        objProcessTrainData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        objProcessTrainData.FROM_DATE = item.FROM_DATE
                        objProcessTrainData.TO_DATE = item.TO_DATE
                        objProcessTrainData.YEAR_GRA = item.YEAR_GRA
                        objProcessTrainData.NAME_SHOOLS = item.NAME_SHOOLS
                        objProcessTrainData.FORM_TRAIN_ID = item.FORM_TRAIN_ID
                        objProcessTrainData.SPECIALIZED_TRAIN = item.SPECIALIZED_TRAIN
                        objProcessTrainData.RESULT_TRAIN = item.RESULT_TRAIN
                        objProcessTrainData.CERTIFICATE = item.CERTIFICATE
                        objProcessTrainData.EFFECTIVE_DATE_FROM = item.EFFECTIVE_DATE_FROM
                        objProcessTrainData.EFFECTIVE_DATE_TO = item.EFFECTIVE_DATE_TO
                    Else
                        Dim objProcessTrainData As New HU_PRO_TRAIN_OUT_COMPANY
                        objProcessTrainData.ID = Utilities.GetNextSequence(Context, Context.HU_PRO_TRAIN_OUT_COMPANY.EntitySet.Name)
                        objProcessTrainData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        objProcessTrainData.FROM_DATE = item.FROM_DATE
                        objProcessTrainData.TO_DATE = item.TO_DATE
                        objProcessTrainData.YEAR_GRA = item.YEAR_GRA
                        objProcessTrainData.NAME_SHOOLS = item.NAME_SHOOLS
                        objProcessTrainData.FORM_TRAIN_ID = item.FORM_TRAIN_ID
                        objProcessTrainData.SPECIALIZED_TRAIN = item.SPECIALIZED_TRAIN
                        objProcessTrainData.RESULT_TRAIN = item.RESULT_TRAIN
                        objProcessTrainData.CERTIFICATE = item.CERTIFICATE
                        objProcessTrainData.EFFECTIVE_DATE_FROM = item.EFFECTIVE_DATE_FROM
                        objProcessTrainData.EFFECTIVE_DATE_TO = item.EFFECTIVE_DATE_TO
                        Context.HU_PRO_TRAIN_OUT_COMPANY.AddObject(objProcessTrainData)
                    End If
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function


#End Region

#Region "IPORTAL - Qúa trình công tác trước khi vào công ty"
    Public Function GetWorkingBeforeEdit(ByVal _filter As WorkingBeforeDTOEdit) As List(Of WorkingBeforeDTOEdit)
        Dim query As ObjectQuery(Of WorkingBeforeDTOEdit)
        Try
            query = (From p In Context.HU_WORKING_BEFORE_EDIT
                   Select New WorkingBeforeDTOEdit With {
                    .ID = p.ID,
                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                    .COMPANY_NAME = p.COMPANY_NAME,
                    .COMPANY_ADDRESS = p.COMPANY_ADDRESS,
                    .TELEPHONE = p.TELEPHONE,
                    .JOIN_DATE = p.JOIN_DATE,
                    .END_DATE = p.END_DATE,
                    .SALARY = p.SALARY,
                    .TITLE_NAME = p.TITLE_NAME,
                    .LEVEL_NAME = p.LEVEL_NAME,
                    .TER_REASON = p.TER_REASON,
                    .REASON_UNAPROVE = p.REASON_UNAPROVE,
                    .FK_PKEY = p.FK_PKEY,
                         .STATUS = p.STATUS,
                         .STATUS_NAME = If(p.STATUS = 0, "Chưa gửi duyệt",
                                           If(p.STATUS = 1, "Chờ phê duyệt",
                                              If(p.STATUS = 2, "Phê duyệt",
                                                 If(p.STATUS = 3, "Không phê duyệt", ""))))})
            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.ID <> 0 Then
                query = query.Where(Function(p) p.ID = _filter.ID)
            End If
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetApproveWorkingBeforeEdit(ByVal _filter As WorkingBeforeDTOEdit,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_ID desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of WorkingBeforeDTOEdit)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = (From p In Context.HU_WORKING_BEFORE_EDIT
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        Where p.STATUS = 1
                        Select New WorkingBeforeDTOEdit With {
                            .ID = p.ID,
                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                    .COMPANY_NAME = p.COMPANY_NAME,
                    .COMPANY_ADDRESS = p.COMPANY_ADDRESS,
                    .TELEPHONE = p.TELEPHONE,
                    .JOIN_DATE = p.JOIN_DATE,
                    .END_DATE = p.END_DATE,
                    .SALARY = p.SALARY,
                    .TITLE_NAME = p.TITLE_NAME,
                    .LEVEL_NAME = p.LEVEL_NAME,
                    .TER_REASON = p.TER_REASON,
                    .FK_PKEY = p.FK_PKEY,
                         .STATUS = p.STATUS,
                            .STATUS_NAME = If(p.STATUS = 0, "Chưa gửi duyệt",
                                              If(p.STATUS = 1, "Chờ phê duyệt",
                                                 If(p.STATUS = 2, "Phê duyệt",
                                                    If(p.STATUS = 3, "Không phê duyệt", ""))))})



            If _filter.COMPANY_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.COMPANY_NAME.ToUpper.Contains(_filter.COMPANY_NAME.ToUpper))
            End If

            If _filter.COMPANY_ADDRESS IsNot Nothing Then
                query = query.Where(Function(p) p.COMPANY_ADDRESS.ToUpper.Contains(_filter.COMPANY_ADDRESS.ToUpper))
            End If

            If _filter.TELEPHONE IsNot Nothing Then
                query = query.Where(Function(p) p.TELEPHONE.ToUpper.Contains(_filter.TELEPHONE.ToUpper))
            End If

            If _filter.SALARY IsNot Nothing Then
                query = query.Where(Function(p) p.SALARY = _filter.SALARY)
            End If

            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            If _filter.LEVEL_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.LEVEL_NAME.ToUpper.Contains(_filter.LEVEL_NAME.ToUpper))
            End If

            If _filter.STATUS_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.STATUS_NAME.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If

            If _filter.TER_REASON IsNot Nothing Then
                query = query.Where(Function(p) p.TER_REASON.ToUpper.Contains(_filter.TER_REASON.ToUpper))
            End If

            If _filter.JOIN_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.JOIN_DATE = _filter.JOIN_DATE)
            End If

            If _filter.END_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.END_DATE = _filter.END_DATE)
            End If



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


    Public Function InsertWorkingBeforeEdit(ByVal objWorkingBefore As WorkingBeforeDTOEdit, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Dim objWorkingBeforeData As New HU_WORKING_BEFORE_EDIT
            objWorkingBeforeData.ID = Utilities.GetNextSequence(Context, Context.HU_WORKING_BEFORE_EDIT.EntitySet.Name)
            objWorkingBeforeData.EMPLOYEE_ID = objWorkingBefore.EMPLOYEE_ID
            objWorkingBeforeData.COMPANY_ADDRESS = objWorkingBefore.COMPANY_ADDRESS
            objWorkingBeforeData.COMPANY_NAME = objWorkingBefore.COMPANY_NAME
            objWorkingBeforeData.END_DATE = objWorkingBefore.END_DATE
            objWorkingBeforeData.SALARY = objWorkingBefore.SALARY
            objWorkingBeforeData.TELEPHONE = objWorkingBefore.TELEPHONE
            objWorkingBeforeData.TER_REASON = objWorkingBefore.TER_REASON
            objWorkingBeforeData.LEVEL_NAME = objWorkingBefore.LEVEL_NAME
            objWorkingBeforeData.TITLE_NAME = objWorkingBefore.TITLE_NAME
            objWorkingBeforeData.JOIN_DATE = objWorkingBefore.JOIN_DATE
            objWorkingBeforeData.END_DATE = objWorkingBefore.END_DATE
            objWorkingBeforeData.CREATED_DATE = DateTime.Now
            objWorkingBeforeData.CREATED_BY = log.Username
            objWorkingBeforeData.CREATED_LOG = log.ComputerName
            objWorkingBeforeData.MODIFIED_DATE = DateTime.Now
            objWorkingBeforeData.MODIFIED_BY = log.Username
            objWorkingBeforeData.MODIFIED_LOG = log.ComputerName
            objWorkingBeforeData.STATUS = 0
            objWorkingBeforeData.REASON_UNAPROVE = objWorkingBefore.REASON_UNAPROVE
            objWorkingBeforeData.FK_PKEY = objWorkingBefore.FK_PKEY
            Context.HU_WORKING_BEFORE_EDIT.AddObject(objWorkingBeforeData)
            Context.SaveChanges(log)
            gID = objWorkingBeforeData.ID
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyWorkingBeforeEdit(ByVal objWorkingBefore As WorkingBeforeDTOEdit,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objWorkingBeforeData As New HU_WORKING_BEFORE_EDIT With {.ID = objWorkingBefore.ID}
        Try
            objWorkingBeforeData = (From p In Context.HU_WORKING_BEFORE_EDIT Where p.ID = objWorkingBefore.ID).FirstOrDefault
            objWorkingBeforeData.EMPLOYEE_ID = objWorkingBefore.EMPLOYEE_ID
            objWorkingBeforeData.COMPANY_ADDRESS = objWorkingBefore.COMPANY_ADDRESS
            objWorkingBeforeData.COMPANY_NAME = objWorkingBefore.COMPANY_NAME
            objWorkingBeforeData.END_DATE = objWorkingBefore.END_DATE
            objWorkingBeforeData.SALARY = objWorkingBefore.SALARY
            objWorkingBeforeData.TELEPHONE = objWorkingBefore.TELEPHONE
            objWorkingBeforeData.TER_REASON = objWorkingBefore.TER_REASON
            objWorkingBeforeData.LEVEL_NAME = objWorkingBefore.LEVEL_NAME
            objWorkingBeforeData.TITLE_NAME = objWorkingBefore.TITLE_NAME
            objWorkingBeforeData.JOIN_DATE = objWorkingBefore.JOIN_DATE
            objWorkingBeforeData.END_DATE = objWorkingBefore.END_DATE
            objWorkingBeforeData.MODIFIED_DATE = DateTime.Now
            objWorkingBeforeData.MODIFIED_BY = log.Username
            objWorkingBeforeData.MODIFIED_LOG = log.ComputerName
            objWorkingBeforeData.REASON_UNAPROVE = objWorkingBefore.REASON_UNAPROVE
            objWorkingBeforeData.FK_PKEY = objWorkingBefore.FK_PKEY
            Context.SaveChanges(log)
            gID = objWorkingBeforeData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteWorkingBeforeEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_WORKING_BEFORE_EDIT)
        Try
            lst = (From p In Context.HU_WORKING_BEFORE_EDIT Where lstDecimals.Contains(p.ID)).ToList
            For i As Int16 = 0 To lst.Count - 1
                Context.HU_WORKING_BEFORE_EDIT.DeleteObject(lst(i))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function CheckExistWorkingBeforeEdit(ByVal pk_key As Decimal) As WorkingBeforeDTOEdit
        Try
            Dim query = (From p In Context.HU_WORKING_BEFORE_EDIT
                         Where p.STATUS <> 2 And p.FK_PKEY = pk_key
                         Select New WorkingBeforeDTOEdit With {
                             .ID = p.ID,
                             .STATUS = p.STATUS}).FirstOrDefault

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function SendWorkingBeforeEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean

        Try
            Dim lstObj = (From p In Context.HU_WORKING_BEFORE_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lstObj
                item.SEND_DATE = Date.Now
                item.STATUS = 1
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function UpdateStatusWorkingBeforeEdit(ByVal lstID As List(Of Decimal),
                                                   ByVal status As String,
                                                   ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_WORKING_BEFORE_EDIT)
        Dim sStatus() As String = status.Split(":")

        Try
            lst = (From p In Context.HU_WORKING_BEFORE_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lst
                item.STATUS = sStatus(0)
                item.REASON_UNAPROVE = sStatus(1)

                If sStatus(0) = 2 Then
                    If item.FK_PKEY IsNot Nothing Then
                        Dim objWorkingBeforeData As New HU_WORKING_BEFORE
                        objWorkingBeforeData = (From p In Context.HU_WORKING_BEFORE Where p.ID = item.FK_PKEY).FirstOrDefault
                        objWorkingBeforeData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        objWorkingBeforeData.COMPANY_ADDRESS = item.COMPANY_ADDRESS
                        objWorkingBeforeData.COMPANY_NAME = item.COMPANY_NAME
                        objWorkingBeforeData.END_DATE = item.END_DATE
                        objWorkingBeforeData.SALARY = item.SALARY
                        objWorkingBeforeData.TELEPHONE = item.TELEPHONE
                        objWorkingBeforeData.TER_REASON = item.TER_REASON
                        objWorkingBeforeData.LEVEL_NAME = item.LEVEL_NAME
                        objWorkingBeforeData.TITLE_NAME = item.TITLE_NAME
                        objWorkingBeforeData.JOIN_DATE = item.JOIN_DATE
                        objWorkingBeforeData.END_DATE = item.END_DATE

                    Else
                        Dim objWorkingBeforeData As New HU_WORKING_BEFORE
                        objWorkingBeforeData.ID = Utilities.GetNextSequence(Context, Context.HU_WORKING_BEFORE.EntitySet.Name)
                        objWorkingBeforeData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        objWorkingBeforeData.COMPANY_ADDRESS = item.COMPANY_ADDRESS
                        objWorkingBeforeData.COMPANY_NAME = item.COMPANY_NAME
                        objWorkingBeforeData.END_DATE = item.END_DATE
                        objWorkingBeforeData.SALARY = item.SALARY
                        objWorkingBeforeData.TELEPHONE = item.TELEPHONE
                        objWorkingBeforeData.TER_REASON = item.TER_REASON
                        objWorkingBeforeData.LEVEL_NAME = item.LEVEL_NAME
                        objWorkingBeforeData.TITLE_NAME = item.TITLE_NAME
                        objWorkingBeforeData.JOIN_DATE = item.JOIN_DATE
                        objWorkingBeforeData.END_DATE = item.END_DATE
                        Context.HU_WORKING_BEFORE.AddObject(objWorkingBeforeData)
                    End If
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function


#End Region

#Region "Portal Xem diem, chuc nang tuong ung voi khoa hoc"
    Public Function GetPortalCompetencyCourse(ByVal _empId As Decimal) As List(Of EmployeeCriteriaRecordDTO)
        Try
            Dim query = From stand In Context.HU_COMPETENCY_STANDARD
                     From Competency In Context.HU_COMPETENCY.Where(Function(f) f.ID = stand.COMPETENCY_ID)
                     From Competencygroup In Context.HU_COMPETENCY_GROUP.Where(Function(f) f.ID = Competency.COMPETENCY_GROUP_ID)
                     From ass In Context.HU_COMPETENCY_ASS.Where(Function(f) stand.TITLE_ID = f.TITLE_ID).DefaultIfEmpty
                     From p In Context.HU_COMPETENCY_ASSDTL.Where(Function(f) f.COMPETENCY_ASS_ID = ass.ID And
                                                                      f.COMPETENCY_ID = stand.COMPETENCY_ID).DefaultIfEmpty
                     From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = ass.EMPLOYEE_ID).DefaultIfEmpty
                     From period In Context.HU_COMPETENCY_PERIOD.Where(Function(f) f.ID = ass.COMPETENCY_PERIOD_ID).DefaultIfEmpty
                     From compCourse In Context.HU_COMPETENCY_COURSE.Where(Function(f) f.COMPETENCY_ID = Competency.ID).DefaultIfEmpty
                     From Course In Context.TR_COURSE.Where(Function(f) f.ID = compCourse.TR_COURSE_ID).DefaultIfEmpty
                     Where ass.EMPLOYEE_ID = _empId
                     Select New EmployeeCriteriaRecordDTO With {
                         .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                         .EMPLOYEE_ID = ass.EMPLOYEE_ID,
                         .EMPLOYEE_NAME = e.FULLNAME_VN,
                         .TITLE_ID = stand.TITLE_ID,
                         .COMPETENCY_GROUP_ID = Competency.COMPETENCY_GROUP_ID,
                         .COMPETENCY_GROUP_NAME = Competencygroup.NAME,
                         .COMPETENCY_PERIOD_ID = period.ID,
                         .COMPETENCY_PERIOD_NAME = period.NAME,
                         .COMPETENCY_PERIOD_YEAR = period.YEAR,
                         .COMPETENCY_ID = stand.COMPETENCY_ID,
                         .COMPETENCY_NAME = Competency.NAME,
                         .LEVEL_NUMBER_STANDARD = stand.LEVEL_NUMBER,
                         .LEVEL_NUMBER_ASS = p.LEVEL_NUMBER,
                         .TR_COURSE_ID = compCourse.TR_COURSE_ID,
                         .TR_COURSE_NAME = Course.NAME,
                         .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            'Return lst.ToList
            Dim lstData = lst.ToList
            For Each item In lstData
                If item.LEVEL_NUMBER_STANDARD IsNot Nothing Then
                    item.LEVEL_NUMBER_STANDARD_NAME = item.LEVEL_NUMBER_STANDARD.Value.ToString & "/4"
                End If
                If item.LEVEL_NUMBER_ASS IsNot Nothing Then
                    item.LEVEL_NUMBER_ASS_NAME = item.LEVEL_NUMBER_ASS.Value.ToString & "/4"
                End If
                Dim levelStandar As Decimal = If(item.LEVEL_NUMBER_STANDARD IsNot Nothing, item.LEVEL_NUMBER_STANDARD, 0)
                Dim levelEmployee As Decimal = If(item.LEVEL_NUMBER_ASS IsNot Nothing, item.LEVEL_NUMBER_ASS, 0)
                If levelEmployee >= levelStandar Then
                    item.TR_COURSE_NAME = ""
                    item.TR_COURSE_ID = Nothing
                End If
            Next
            Return lstData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "GetCompetencyEmployee")
            Throw ex
        End Try
    End Function



#End Region


    Public Function ModifyEmployeeHuFile(ByVal objEmployeeHuF As HuFileDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objEmployeeHuFile As New HU_FILE With {.ID = objEmployeeHuF.ID}
        Try
            objEmployeeHuFile = (From p In Context.HU_FILE Where p.ID = objEmployeeHuF.ID).FirstOrDefault
            objEmployeeHuFile.EMPLOYEE_ID = objEmployeeHuF.EMPLOYEE_ID
            objEmployeeHuFile.MODIFIED_DATE = DateTime.Now
            objEmployeeHuFile.MODIFIED_BY = log.Username
            objEmployeeHuFile.MODIFIED_LOG = log.ComputerName
            Context.SaveChanges(log)
            gID = objEmployeeHuFile.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
End Class
