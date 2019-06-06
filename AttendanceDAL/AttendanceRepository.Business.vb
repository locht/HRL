Imports System.Data.Objects
Imports System.Configuration
Imports System.Linq.Expressions
Imports LinqKit.Extensions
Imports System.Data.Entity
Imports System.Text
Imports System.Runtime.CompilerServices
Imports System.Data.Common
Imports System.Threading
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic
Imports System.Reflection
Imports Framework.Data.DataAccess
Imports Oracle.DataAccess.Client
Imports System.Globalization
Imports HistaffFrameworkPublic.HistaffFrameworkEnum



Partial Public Class AttendanceRepository
    Dim ls_AT_SWIPE_DATADTO As New List(Of AT_SWIPE_DATADTO)
#Region "CONFIG TEMPLATE "
    Public Function GET_CONFIG_TEMPLATE(ByVal MACHINE_TYPE As Decimal?) As DataSet
        Using cls As New DataAccess.QueryData
            Dim dtData As DataSet = cls.ExecuteStore("PKG_AT_LIST.GET_CONFIG_TEMPLATE",
                                           New With {.P_MACHINE_TYPE = MACHINE_TYPE,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR}, False)
            Return dtData
        End Using
        Return Nothing
    End Function
#End Region

    Public Function ToDate(ByVal item As Object)
        If IsDBNull(item) Then
            Return Nothing
        Else
            Return DateTime.ParseExact(item, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture)
        End If
    End Function
    Public Function ToDecimal(ByVal item As Object)
        If IsDBNull(item) Then
            Return Nothing
        Else
            Return CDec(item)
        End If
    End Function

    Public Function IMPORT_AT_SWIPE_DATA(ByVal log As UserLog, ByVal DATA_IN As String) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.IMPORT_AT_SWIPE_DATA",
                                               New With {.P_USER = log.Username.ToUpper,
                                                         .P_DATA = DATA_IN,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function IMPORT_AT_SWIPE_DATA_V1(ByVal log As UserLog, ByVal DATA_IN As String, ByVal Machine_type As Decimal) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_AT_PROCESS.IMPORT_AT_SWIPE_DATA",
                                               New With {.P_MACHINE_TYPE = Machine_type,
                                                         .P_DATA = DATA_IN,
                                                         .P_USER = log.Username,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function



#Region "Di som ve muon"
    Public Function GetDSVM(ByVal _filter As AT_LATE_COMBACKOUTDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LATE_COMBACKOUTDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_LATE_COMBACKOUT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From type In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.TYPE_DSVM).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)

            Dim lst = query.Select(Function(p) New AT_LATE_COMBACKOUTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .TYPE_DMVS_NAME = p.type.NAME,
                                       .TO_HOUR = p.p.TO_HOUR,
                                       .FROM_HOUR = p.p.FROM_HOUR,
                                       .MINUTE = p.p.MINUTE,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .REMARK = p.p.REMARK,
                                       .ORG_ID = p.p.ORG_ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})
            If _filter.IS_TERMINATE Then
                lst = lst.Where(Function(f) f.WORK_STATUS = 257)
                If _filter.WORKINGDAY.HasValue Then
                    lst = lst.Where(Function(f) f.TER_LAST_DATE <= _filter.WORKINGDAY)
                End If
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.VN_FULLNAME.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                lst = lst.Where(Function(f) f.VN_FULLNAME.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If _filter.FROM_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY <= _filter.END_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TYPE_DMVS_NAME) Then
                lst = lst.Where(Function(f) f.TYPE_DMVS_NAME.ToLower().Contains(_filter.TYPE_DMVS_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(f) f.REMARK.ToLower().Contains(_filter.REMARK.ToLower()))
            End If
            If _filter.MINUTE.HasValue Then
                lst = lst.Where(Function(f) f.MINUTE = _filter.MINUTE)
            End If


            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetLate_CombackoutById(ByVal _id As Decimal?) As AT_LATE_COMBACKOUTDTO
        Try

            Dim query = From p In Context.AT_LATE_COMBACKOUT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From type In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.TYPE_DSVM).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_LATE_COMBACKOUTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .TYPE_DMVS_NAME = p.type.NAME,
                                       .TYPE_DMVS_ID = p.p.TYPE_DSVM,
                                       .TO_HOUR = p.p.TO_HOUR,
                                       .FROM_HOUR = p.p.FROM_HOUR,
                                       .MINUTE = p.p.MINUTE,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .REMARK = p.p.REMARK,
                                       .ORG_ID = p.p.ORG_ID,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ImportLate_combackout(ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objLate_combackoutData As AT_LATE_COMBACKOUT
        Dim exits As Boolean?
        Dim employee_id As Decimal?
        Dim org_id As Decimal?

        Dim sql = (From e In Context.HU_EMPLOYEE
                          From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                          Where e.EMPLOYEE_CODE = objLate_combackout.EMPLOYEE_CODE And e.JOIN_DATE <= objLate_combackout.WORKINGDAY And _
                          (e.TER_EFFECT_DATE Is Nothing Or _
                           (e.TER_EFFECT_DATE IsNot Nothing And _
                            e.TER_EFFECT_DATE > objLate_combackout.WORKINGDAY)) And w.EFFECT_DATE <= objLate_combackout.WORKINGDAY
                    Order By w.EFFECT_DATE Descending
                    Select w).FirstOrDefault
        If sql IsNot Nothing Then
            employee_id = sql.EMPLOYEE_ID
            org_id = sql.ORG_ID
        Else
            Exit Function
        End If

        If Not employee_id Is Nothing Then
            Try
                exits = (From p In Context.AT_LATE_COMBACKOUT _
                Where p.EMPLOYEE_ID = employee_id And p.WORKINGDAY = objLate_combackout.WORKINGDAY And p.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID).Any
                If exits Then
                    Dim objlate = (From p In Context.AT_LATE_COMBACKOUT _
                                   Where p.EMPLOYEE_ID = employee_id And p.WORKINGDAY = objLate_combackout.WORKINGDAY And p.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID).FirstOrDefault
                    objlate.EMPLOYEE_ID = employee_id
                    objlate.ORG_ID = org_id
                    objlate.TITLE_ID = sql.TITLE_ID
                    objlate.WORKINGDAY = objLate_combackout.WORKINGDAY
                    objlate.REMARK = objLate_combackout.REMARK
                    objlate.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID
                    objlate.MINUTE = objLate_combackout.MINUTE
                    objlate.TO_HOUR = objLate_combackout.TO_HOUR
                    objlate.FROM_HOUR = objLate_combackout.FROM_HOUR
                Else
                    objLate_combackoutData = New AT_LATE_COMBACKOUT
                    objLate_combackoutData.ID = Utilities.GetNextSequence(Context, Context.AT_LATE_COMBACKOUT.EntitySet.Name)
                    objLate_combackoutData.EMPLOYEE_ID = employee_id
                    objLate_combackoutData.ORG_ID = org_id
                    objLate_combackoutData.TITLE_ID = sql.TITLE_ID
                    objLate_combackoutData.MINUTE = objLate_combackout.MINUTE
                    objLate_combackoutData.TO_HOUR = objLate_combackout.TO_HOUR
                    objLate_combackoutData.FROM_HOUR = objLate_combackout.FROM_HOUR
                    objLate_combackoutData.WORKINGDAY = objLate_combackout.WORKINGDAY
                    objLate_combackoutData.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID
                    objLate_combackoutData.REMARK = objLate_combackout.REMARK
                    Context.AT_LATE_COMBACKOUT.AddObject(objLate_combackoutData)
                End If
                Context.SaveChanges(log)
                Return True
            Catch ex As Exception
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
                Throw ex
            End Try
        Else
            Return False
        End If
    End Function

    Public Function InsertLate_combackout(ByVal objRegisterDMVSList As List(Of AT_LATE_COMBACKOUTDTO), ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objLate_combackoutData As AT_LATE_COMBACKOUT
        Dim objData As New AT_LATE_COMBACKOUTDTO
        Dim exits As Boolean?
        Dim employee_id As Decimal?
        Dim org_id As Decimal?
        Try
            For index = 0 To objRegisterDMVSList.Count - 1
                objData = objRegisterDMVSList(index)
                Dim sql = (From e In Context.HU_EMPLOYEE
                                  From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                                  Where e.EMPLOYEE_CODE = objData.EMPLOYEE_CODE And e.JOIN_DATE <= objLate_combackout.WORKINGDAY And _
                                  (e.TER_EFFECT_DATE Is Nothing Or _
                                   (e.TER_EFFECT_DATE IsNot Nothing And _
                                    e.TER_EFFECT_DATE > objLate_combackout.WORKINGDAY)) And w.EFFECT_DATE <= objLate_combackout.WORKINGDAY
                            Order By w.EFFECT_DATE Descending
                            Select w).FirstOrDefault
                If sql IsNot Nothing Then
                    employee_id = sql.EMPLOYEE_ID
                    org_id = sql.ORG_ID
                Else
                    Exit Function
                End If
                If Not employee_id Is Nothing Then
                    exits = (From p In Context.AT_LATE_COMBACKOUT _
                    Where p.EMPLOYEE_ID = employee_id And p.WORKINGDAY = objLate_combackout.WORKINGDAY And p.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID).Any
                    If exits Then
                        Dim objlate = (From p In Context.AT_LATE_COMBACKOUT _
                                       Where p.EMPLOYEE_ID = employee_id And p.WORKINGDAY = objLate_combackout.WORKINGDAY And p.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID).FirstOrDefault
                        objlate.EMPLOYEE_ID = employee_id
                        objlate.ORG_ID = org_id
                        objlate.TITLE_ID = sql.TITLE_ID
                        objlate.WORKINGDAY = objLate_combackout.WORKINGDAY
                        objlate.REMARK = objLate_combackout.REMARK
                        objlate.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID
                        objlate.MINUTE = objLate_combackout.MINUTE
                        objlate.TO_HOUR = objLate_combackout.TO_HOUR
                        objlate.FROM_HOUR = objLate_combackout.FROM_HOUR
                    Else
                        objLate_combackoutData = New AT_LATE_COMBACKOUT
                        objLate_combackoutData.ID = Utilities.GetNextSequence(Context, Context.AT_LATE_COMBACKOUT.EntitySet.Name)
                        objLate_combackoutData.EMPLOYEE_ID = employee_id
                        objLate_combackoutData.ORG_ID = org_id
                        objLate_combackoutData.TITLE_ID = sql.TITLE_ID
                        objLate_combackoutData.MINUTE = objLate_combackout.MINUTE
                        objLate_combackoutData.TO_HOUR = objLate_combackout.TO_HOUR
                        objLate_combackoutData.FROM_HOUR = objLate_combackout.FROM_HOUR
                        objLate_combackoutData.WORKINGDAY = objLate_combackout.WORKINGDAY
                        objLate_combackoutData.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID
                        objLate_combackoutData.REMARK = objLate_combackout.REMARK
                        Context.AT_LATE_COMBACKOUT.AddObject(objLate_combackoutData)
                    End If
                    Context.SaveChanges(log)
                End If
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateLate_combackout(ByVal _validate As AT_LATE_COMBACKOUTDTO)
        Dim query
        Try
            If _validate.WORKINGDAY.HasValue Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_LATE_COMBACKOUT
                             Where p.WORKINGDAY = _validate.WORKINGDAY And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID And p.ID <> _validate.ID And p.TYPE_DSVM = _validate.TYPE_DMVS_ID).Any
                Else
                    query = (From p In Context.AT_LATE_COMBACKOUT
                             Where p.WORKINGDAY = _validate.WORKINGDAY And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID And p.TYPE_DSVM = _validate.TYPE_DMVS_ID).Any
                End If
                If query Then
                    Return True
                Else
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyLate_combackout(ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objLate_combackoutData As New AT_LATE_COMBACKOUT With {.ID = objLate_combackout.ID}
        Try
            Dim objlate = (From p In Context.AT_LATE_COMBACKOUT Where p.ID = objLate_combackout.ID).FirstOrDefault
            'objlate.EMPLOYEE_ID = objLate_combackout.EMPLOYEE_ID không cho phép sửa nhân viên
            objlate.WORKINGDAY = objLate_combackout.WORKINGDAY
            objlate.ORG_ID = objLate_combackout.ORG_ID
            objlate.REMARK = objLate_combackout.REMARK
            objlate.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID
            objlate.MINUTE = objLate_combackout.MINUTE
            objlate.TO_HOUR = objLate_combackout.TO_HOUR
            objlate.FROM_HOUR = objLate_combackout.FROM_HOUR
            Context.SaveChanges(log)
            gID = objLate_combackoutData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function DeleteLate_combackout(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstl As List(Of AT_LATE_COMBACKOUT)
        Try
            lstl = (From p In Context.AT_LATE_COMBACKOUT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstl.Count - 1
                Context.AT_LATE_COMBACKOUT.DeleteObject(lstl(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function
#End Region

#Region "Cham cong may"
    Public Function Init_TimeTImesheetMachines(ByVal _param As ParamDTO,
                                               ByVal log As UserLog,
                                               ByVal p_fromdate As Date,
                                               ByVal p_enddate As Date,
                                               ByVal P_ORG_ID As Decimal,
                                               ByVal lstEmployee As List(Of Decimal?),
                                               ByVal p_delAll As Decimal,
                                               ByVal codecase As String) As Boolean
        Try
            Dim obj As New AT_ACTION_LOGDTO
            Using cls As New DataAccess.NonQueryData
                Dim Period = (From w In Context.AT_PERIOD Where w.START_DATE.Value.Year = p_fromdate.Year And w.START_DATE.Value.Month = p_fromdate.Month).FirstOrDefault
                obj.PERIOD_ID = Period.ID
                LOG_AT(_param, log, lstEmployee, "TỔNG HỢP BẢNG CÔNG GỐC", obj, P_ORG_ID)
                If codecase = "ctrlTimeTimesheet_machine_case1" Then
                    cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_HOSE",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = P_ORG_ID,
                                                         .P_PERIOD_ID = Period.ID,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                         .P_DELETE_ALL = p_delAll})
                Else
                    cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_ALL",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = P_ORG_ID,
                                                         .P_PERIOD_ID = Period.ID,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                         .P_DELETE_ALL = p_delAll})
                End If
                Return True

            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function
    Public Function GetMachines(ByVal _filter As AT_TIME_TIMESHEET_MACHINETDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "EMPLOYEE_ID, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MACHINETDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_TIME_TIMESHEET_MACHINET
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From m In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From obj_att In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANCE).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper.ToUpper)

            'If _filter.IS_TERMINATE Then
            '    query = query.Where(Function(f) f.e.WORK_STATUS = 257 And f.e.TER_LAST_DATE >= _filter.FROM_DATE)
            'End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.e.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                query = query.Where(Function(f) f.e.FULLNAME_VN.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If _filter.WORKINGDAY.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY = _filter.WORKINGDAY)
            End If
            If Not String.IsNullOrEmpty(_filter.SHIFT_CODE) Then
                query = query.Where(Function(f) f.p.SHIFT_CODE.ToLower().Contains(_filter.SHIFT_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MANUAL_CODE) Then
                query = query.Where(Function(f) f.m.CODE.ToLower().Contains(_filter.MANUAL_CODE.ToLower()))
            End If
            If _filter.SHIFTIN.HasValue Then
                query = query.Where(Function(f) f.p.VALIN1 = _filter.SHIFTIN)
            End If
            If _filter.SHIFTOUT.HasValue Then
                query = query.Where(Function(f) f.p.VALIN4 = _filter.SHIFTOUT)
            End If
            If _filter.SHIFTBACKOUT.HasValue Then
                query = query.Where(Function(f) f.p.VALIN2 = _filter.SHIFTBACKOUT)
            End If
            If _filter.SHIFTBACKIN.HasValue Then
                query = query.Where(Function(f) f.p.VALIN3 = _filter.SHIFTBACKIN)
            End If
            If _filter.WORKINGHOUR.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGHOUR = _filter.WORKINGHOUR)
            End If
            If Not String.IsNullOrEmpty(_filter.LEAVE_CODE) Then
                query = query.Where(Function(f) f.p.LEAVE_CODE.ToLower().Contains(_filter.LEAVE_CODE.ToLower()))
            End If

            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY <= _filter.END_DATE)
            End If

            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_MACHINETDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .STAFF_RANK_NAME = p.s.NAME,
                                       .ORG_ID = p.p.ORG_ID,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .SHIFT_ID = p.p.SHIFT_ID,
                                       .SHIFT_CODE = p.p.SHIFT_CODE,
                                       .MANUAL_CODE = p.m.CODE,
                                       .LATE = p.p.LATE,
                                       .WORKINGHOUR = p.p.WORKINGHOUR,
                                       .SHIFTIN = p.p.VALIN1,
                                       .SHIFTBACKOUT = p.p.VALIN2,
                                       .SHIFTBACKIN = p.p.VALIN3,
                                       .SHIFTOUT = p.p.VALIN4,
                                       .COMEBACKOUT = p.p.COMEBACKOUT,
                                       .TIMEVALIN = p.p.TIMEVALIN,
                                       .TIMEVALOUT = p.p.TIMEVALOUT,
                                       .OBJECT_ATTENDANCE = p.p.OBJECT_ATTENDANCE,
                                       .OBJECT_ATTENDANCE_NAME = p.obj_att.NAME_VN,
                                       .OBJECT_ATTENDANCE_CODE = p.p.OBJECT_ATTENDANCE_CODE,
                                       .MIN_DEDUCT = p.p.MIN_DEDUCT,
                                       .MIN_DEDUCT_WORK = p.p.MIN_DEDUCT_WORK,
                                       .MIN_EARLY = p.p.MIN_EARLY,
                                       .MIN_IN_WORK = p.p.MIN_IN_WORK,
                                       .MIN_LATE = p.p.MIN_LATE,
                                       .MIN_LATE_EARLY = p.p.MIN_LATE_EARLY,
                                       .MIN_ON_LEAVE = p.p.MIN_ON_LEAVE,
                                       .MIN_OUT_WORK = p.p.MIN_OUT_WORK,
                                       .MIN_OUT_WORK_DEDUCT = p.p.MIN_OUT_WORK_DEDUCT})
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    'Public Function GetMachines(ByVal _filter As AT_TIME_TIMESHEET_MACHINETDTO,
    '                                 ByVal _param As ParamDTO,
    '                                 Optional ByRef Total As Integer = 0,
    '                                 Optional ByVal PageIndex As Integer = 0,
    '                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
    '                                 Optional ByVal Sorts As String = "EMPLOYEE_ID, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MACHINETDTO)
    '    Try
    '        Dim lst As List(Of AT_TIME_TIMESHEET_MACHINETDTO) = New List(Of AT_TIME_TIMESHEET_MACHINETDTO)
    '        Using cls As New DataAccess.QueryData
    '            Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GETMACHINES",
    '                                       New With {.P_USERNAME = log.Username.ToUpper,
    '                                                 .P_ORGID = _param.ORG_ID,
    '                                                 .P_FROM_DATE = _filter.FROM_DATE,
    '                                                 .P_TO_DATE = _filter.END_DATE,
    '                                                 .P_OUT = cls.OUT_CURSOR})
    '            If dtData IsNot Nothing Then
    '                lst = (From row As DataRow In dtData.Rows
    '                   Select New AT_TIME_TIMESHEET_MACHINETDTO With {.ID = row("ID").ToString(),
    '                                                    .EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString(),
    '                                                   .VN_FULLNAME = row("VN_FULLNAME").ToString(),
    '                                                   .EMPLOYEE_ID = row("EMPLOYEE_ID").ToString(),
    '                                                   .TITLE_NAME = row("TITLE_NAME").ToString(),
    '                                                   .ORG_NAME = row("ORG_NAME").ToString(),
    '                                                   .ORG_DESC = row("ORG_DESC").ToString(),
    '                                                   .STAFF_RANK_NAME = row("STAFF_RANK_NAME").ToString(),
    '                                                   .ORG_ID = row("ORG_ID").ToString(),
    '                                                   .WORKINGDAY = If(row("WORKINGDAY") IsNot Nothing, ToDate(row("WORKINGDAY")), Nothing),
    '                                                   .SHIFTIN = If(row("SHIFTIN") IsNot Nothing, ToDate(row("SHIFTIN")), Nothing),
    '                                                   .SHIFTBACKOUT = If(row("SHIFTBACKOUT") IsNot Nothing, ToDate(row("SHIFTBACKOUT")), Nothing),
    '                                                   .SHIFTBACKIN = If(row("SHIFTBACKIN") IsNot Nothing, ToDate(row("SHIFTBACKIN")), Nothing),
    '                                                   .SHIFTOUT = If(row("SHIFTOUT") IsNot Nothing, ToDate(row("SHIFTOUT")), Nothing),
    '                                                   .SHIFT_ID = If(row("SHIFT_ID") IsNot Nothing, ToDecimal(row("SHIFT_ID")), Nothing),
    '                                                   .SHIFT_CODE = If(row("SHIFT_CODE") IsNot Nothing, row("SHIFT_CODE").ToString(), Nothing),
    '                                                   .MANUAL_CODE = If(row("MANUAL_CODE") IsNot Nothing, row("MANUAL_CODE").ToString(), Nothing),
    '                                                   .LATE = If(row("LATE") IsNot Nothing, ToDecimal(row("LATE")), Nothing),
    '                                                   .WORKINGHOUR = If(row("WORKINGHOUR") IsNot Nothing, ToDecimal(row("WORKINGHOUR")), Nothing),
    '                                                   .COMEBACKOUT = If(row("COMEBACKOUT") IsNot Nothing, ToDecimal(row("COMEBACKOUT")), Nothing),
    '                                                   .SALARIED_HOUR = If(row("SALARIED_HOUR") IsNot Nothing, ToDecimal(row("SALARIED_HOUR")), Nothing),
    '                                                  .NOTSALARIED_HOUR = If(row("NOTSALARIED_HOUR") IsNot Nothing, ToDecimal(row("NOTSALARIED_HOUR")), Nothing)
    '                                              }).ToList
    '                Total = lst.Count()
    '                lst = (From l In lst Order By Sorts Select l
    '                Skip PageIndex * PageSize
    '                Take PageSize).ToList
    '            End If
    '        End Using
    '        Return lst
    '    Catch ex As Exception
    '        WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
    '        Throw ex
    '    End Try
    'End Function
#End Region

#Region "Cham cong tay"

    Public Function GetCCT(ByVal param As AT_TIME_TIMESHEET_DAILYDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_CCT",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_PAGE_INDEX = param.PAGE_INDEX,
                                                         .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                         .P_EMPLOYEE_NAME = param.VN_FULLNAME,
                                                         .P_ORG_NAME = param.ORG_NAME,
                                                         .P_TITLE_NAME = param.TITLE_NAME,
                                                         .P_PAGE_SIZE = param.PAGE_SIZE,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_TERMINATE = param.IS_TERMINATE,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Return dtData '.P_COLOR = param.COLOR,
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetCCT_Origin(ByVal param As AT_TIME_TIMESHEET_DAILYDTO, ByVal log As UserLog) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_CCT_ORIGIN",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                         .P_EMPLOYEE_NAME = param.VN_FULLNAME,
                                                         .P_ORG_NAME = param.ORG_NAME,
                                                         .P_TITLE_NAME = param.TITLE_NAME,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_TERMINATE = param.IS_TERMINATE,
                                                         .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetTimeSheetDailyById(ByVal obj As AT_TIME_TIMESHEET_DAILYDTO) As AT_TIME_TIMESHEET_DAILYDTO
        Try
            Dim query =
                      From e In Context.HU_EMPLOYEE
                      From p In Context.AT_TIME_TIMESHEET_DAILY.Where(Function(f) f.EMPLOYEE_ID = e.ID And f.WORKINGDAY = obj.WORKINGDAY).DefaultIfEmpty
                      From m In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID).DefaultIfEmpty
                      From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                      From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                      From s In Context.AT_SHIFT.Where(Function(f) f.ID = p.SHIFT_ID).DefaultIfEmpty
                      Where e.ID = obj.EMPLOYEE_ID
            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_DAILYDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.e.ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .SHIFT_NAME = "[" & p.s.CODE & "] " & p.s.NAME_VN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .SHIFT_ID = p.p.SHIFT_ID,
                                       .MANUAL_CODE = p.m.CODE,
                                       .MANUAL_ID = p.p.MANUAL_ID,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertLeaveSheetDaily(ByVal dtData As DataTable, ByVal log As UserLog, ByVal PeriodID As Decimal) As Boolean
        Try
            dtData.Columns(0).ColumnName = "E_ID"
            Dim dsData As New DataSet
            dsData.Tables.Add(dtData)
            Dim strXML = dsData.GetXml()
            For i As Int32 = 1 To dtData.Columns.Count - 1
                strXML = strXML.Replace("<" + dtData.Columns(i).ColumnName + " />", String.Empty)
            Next
            'strXML = strXML.Replace("\n", "").Replace("\r", "").Replace("\t", "")
            strXML = strXML.Replace(Chr(13), String.Empty).Replace(Chr(10), String.Empty)
            strXML = strXML.Replace(" ", String.Empty)
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.IMPORT_TIMESHEET_CTT",
                                 New With {.P_XML = strXML,
                                           .P_USERNAME = log.Username.ToUpper,
                                           .P_PERIOD_ID = PeriodID})
            End Using

            'Dim startDate As Date

            'Dim Period = (From w In Context.AT_PERIOD Where w.ID = PeriodID).FirstOrDefault

            'Using conMng As New ConnectionManager
            '    Using conn As New OracleConnection(conMng.GetConnectionString())
            '        Using cmd As New OracleCommand()
            '            Using resource As New DataAccess.OracleCommon()
            '                Try
            '                    conn.Open()
            '                    cmd.Connection = conn
            '                    cmd.CommandType = CommandType.StoredProcedure
            '                    cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.INSERT_WORKSIGN_DATE"
            '                    cmd.Transaction = cmd.Connection.BeginTransaction()

            '                    For Each row As DataRow In dtData.Rows
            '                        cmd.Parameters.Clear()
            '                        Dim objParam = New With {.P_EMPLOYEEID = row("EMPLOYEE_ID").ToString,
            '                                                 .P_PERIODId = Period.ID,
            '                                                 .P_USERNAME = log.Username.ToUpper,
            '                                                 .P_D1 = Utilities.Obj2Decima(row("D1"), 0),
            '                                                 .P_D2 = Utilities.Obj2Decima(row("D2"), 0),
            '                                                 .P_D3 = Utilities.Obj2Decima(row("D3"), 0),
            '                                                 .P_D4 = Utilities.Obj2Decima(row("D4"), 0),
            '                                                 .P_D5 = Utilities.Obj2Decima(row("D5"), 0),
            '                                                 .P_D6 = Utilities.Obj2Decima(row("D6"), 0),
            '                                                 .P_D7 = Utilities.Obj2Decima(row("D7"), 0),
            '                                                 .P_D8 = Utilities.Obj2Decima(row("D8"), 0),
            '                                                 .P_D9 = Utilities.Obj2Decima(row("D9"), 0),
            '                                                 .P_D10 = Utilities.Obj2Decima(row("D10"), 0),
            '                                                 .P_D11 = Utilities.Obj2Decima(row("D11"), 0),
            '                                                 .P_D12 = Utilities.Obj2Decima(row("D12"), 0),
            '                                                 .P_D13 = Utilities.Obj2Decima(row("D13"), 0),
            '                                                 .P_D14 = Utilities.Obj2Decima(row("D14"), 0),
            '                                                 .P_D15 = Utilities.Obj2Decima(row("D15"), 0),
            '                                                 .P_D16 = Utilities.Obj2Decima(row("D16"), 0),
            '                                                 .P_D17 = Utilities.Obj2Decima(row("D17"), 0),
            '                                                 .P_D18 = Utilities.Obj2Decima(row("D18"), 0),
            '                                                 .P_D19 = Utilities.Obj2Decima(row("D19"), 0),
            '                                                 .P_D20 = Utilities.Obj2Decima(row("D20"), 0),
            '                                                 .P_D21 = Utilities.Obj2Decima(row("D21"), 0),
            '                                                 .P_D22 = Utilities.Obj2Decima(row("D22"), 0),
            '                                                 .P_D23 = Utilities.Obj2Decima(row("D23"), 0),
            '                                                 .P_D24 = Utilities.Obj2Decima(row("D24"), 0),
            '                                                 .P_D25 = Utilities.Obj2Decima(row("D25"), 0),
            '                                                 .P_D26 = Utilities.Obj2Decima(row("D26"), 0),
            '                                                 .P_D27 = Utilities.Obj2Decima(row("D27"), 0),
            '                                                 .P_D28 = Utilities.Obj2Decima(row("D28"), 0),
            '                                                 .P_D29 = Utilities.Obj2Decima(row("D29"), 0),
            '                                                 .P_D30 = Utilities.Obj2Decima(row("D30"), 0),
            '                                                 .P_D31 = Utilities.Obj2Decima(row("D31"), 0)}

            '                        If objParam IsNot Nothing Then
            '                            For Each info As PropertyInfo In objParam.GetType().GetProperties()
            '                                Dim bOut As Boolean = False
            '                                Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
            '                                If para IsNot Nothing Then
            '                                    cmd.Parameters.Add(para)
            '                                End If
            '                            Next
            '                        End If
            '                        cmd.ExecuteNonQuery()
            '                    Next

            '                    cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.UPDATE_LEAVESHEET_DAILY"
            '                    cmd.Parameters.Clear()
            '                    Dim objParam1 = New With {.P_STARTDATE = Period.START_DATE.Value,
            '                                             .P_ENDDATE = Period.END_DATE.Value,
            '                                             .P_USERNAME = log.Username.ToUpper}

            '                    If objParam1 IsNot Nothing Then
            '                        For Each info As PropertyInfo In objParam1.GetType().GetProperties()
            '                            Dim bOut As Boolean = False
            '                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam1, Nothing), bOut)
            '                            If para IsNot Nothing Then
            '                                cmd.Parameters.Add(para)
            '                            End If
            '                        Next
            '                    End If

            '                    cmd.ExecuteNonQuery()

            '                    cmd.Transaction.Commit()
            '                Catch ex As Exception
            '                    WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            '                    cmd.Transaction.Rollback()
            '                    Throw ex
            '                    Return False
            '                Finally
            '                    'Dispose all resource
            '                    cmd.Dispose()
            '                    conn.Close()
            '                    conn.Dispose()
            '                End Try
            '            End Using
            '        End Using
            '    End Using
            'End Using
            'Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
            Return False
        End Try
    End Function

    Public Function ModifyLeaveSheetDaily(ByVal objLeave As AT_TIME_TIMESHEET_DAILYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            'Dim emp = (From e In Context.HU_EMPLOYEE Where e.ID = objLeave.EMPLOYEE_ID).FirstOrDefault
            'Dim Period = (From w In Context.AT_PERIOD Where w.START_DATE <= objLeave.WORKINGDAY And objLeave.WORKINGDAY <= w.END_DATE).FirstOrDefault

            'Dim TimeSheetDaily = (From r In Context.AT_TIME_TIMESHEET_DAILY Where r.EMPLOYEE_ID = emp.ID And r.WORKINGDAY = objLeave.WORKINGDAY).FirstOrDefault
            'Dim manual_code As AT_TIME_MANUAL
            'If TimeSheetDaily IsNot Nothing AndAlso TimeSheetDaily.MANUAL_ID IsNot Nothing Then
            '    manual_code = (From m In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = TimeSheetDaily.MANUAL_ID)).FirstOrDefault
            'End If

            'Dim lstEmployee As New List(Of Decimal?)
            'lstEmployee.Add(emp.ID)
            'Dim obj As New AT_ACTION_LOGDTO
            'obj.EMPLOYEE_ID = emp.ID
            'If manual_code IsNot Nothing Then
            '    obj.OLD_VALUE = manual_code.CODE
            'End If
            'obj.NEW_VALUE = objLeave.MANUAL_CODE
            'obj.PERIOD_ID = Period.ID
            'LOG_AT(New ParamDTO, log, lstEmployee, "CHỈNH SỬA XỬ LÝ DỮ LIỆU CHẤM CÔNG", obj, Nothing)
            'TimeSheetDaily.MANUAL_ID = objLeave.MANUAL_ID

            'Context.SaveChanges(log)

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.UPDATE_TIMESHEET_CTT",
                                 New With {.P_FROMDATE = objLeave.FROM_DATE,
                                           .P_TODATE = objLeave.END_DATE,
                                           .P_EMP_ID = objLeave.EMPLOYEE_ID,
                                           .P_MANUAL_ID = objLeave.MANUAL_ID,
                                           .P_USERNAME = log.Username.ToUpper})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#Region "Bang tong hop lam them"

    Public Function Cal_TimeTImesheet_OT(ByVal _param As ParamDTO,
                                         ByVal log As UserLog,
                                         ByVal p_period_id As Decimal?,
                                         ByVal P_ORG_ID As Decimal,
                                         ByVal lstEmployee As List(Of Decimal?)) As Boolean
        Try
            'Using cls As New DataAccess.NonQueryData
            '    cls.ExecuteSQL("DELETE FROM SE_EMPLOYEE_CHOSEN S WHERE  UPPER(S.USING_USER) ='" + log.Username.ToUpper + "'")
            'End Using
            'For Each emp As Decimal? In lstEmployee
            '    Dim objNew As New SE_EMPLOYEE_CHOSEN
            '    objNew.EMPLOYEE_ID = emp
            '    objNew.USING_USER = log.Username.ToUpper
            '    Context.SE_EMPLOYEE_CHOSEN.AddObject(objNew)
            'Next
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = p_period_id
            LOG_AT(_param, log, lstEmployee, "TỔNG HỢP CÔNG LÀM THÊM GIỜ", obj, P_ORG_ID)
            Using cls As New DataAccess.NonQueryData
                'cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIMETIMESHEET_OT",
                '                               New With {.P_USERNAME = log.Username.ToUpper,
                '                                         .P_ORG_ID = P_ORG_ID,
                '                                         .P_PERIOD_ID = p_period_id,
                '                                         .P_ISDISSOLVE = _param.IS_DISSOLVE})

                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_ALL",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = P_ORG_ID,
                                                         .P_PERIOD_ID = p_period_id,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE})
                Return True
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetSummaryOT(ByVal param As AT_TIME_TIMESHEET_OTDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_SUMMARY_OT",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_PAGE_INDEX = param.PAGE_INDEX,
                                                         .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                         .P_PAGE_SIZE = param.PAGE_SIZE,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_EMPLOYEE_NAME = param.VN_FULLNAME,
                                                         .P_ORG_NAME = param.ORG_NAME,
                                                         .P_TITLE_NAME = param.TITLE_NAME,
                                                         .P_STAFF_RANK_NAME = param.STAFF_RANK_NAME,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function Cal_TimeTImesheet_NB(ByVal _param As ParamDTO,
                                         ByVal log As UserLog,
                                         ByVal p_period_id As Decimal?,
                                         ByVal P_ORG_ID As Decimal,
                                         ByVal lstEmployee As List(Of Decimal?)) As Boolean
        Try
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = p_period_id
            LOG_AT(_param, log, lstEmployee, "TỔNG HỢP CÔNG NGHỈ BÙ", obj, P_ORG_ID)
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIMETIMESHEET_NB",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = P_ORG_ID,
                                                         .P_PERIOD_ID = p_period_id,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE})
                Return True
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetSummaryNB(ByVal param As AT_TIME_TIMESHEET_NBDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_SUMMARY_NB",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_PAGE_INDEX = param.PAGE_INDEX,
                                                         .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                         .P_PAGE_SIZE = param.PAGE_SIZE,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_EMPLOYEE_NAME = param.VN_FULLNAME,
                                                         .P_ORG_NAME = param.ORG_NAME,
                                                         .P_TITLE_NAME = param.TITLE_NAME,
                                                         .P_STAFF_RANK_NAME = param.STAFF_RANK_NAME,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetTimeSheetOtById(ByVal obj As AT_TIME_TIMESHEET_OTDTO) As AT_TIME_TIMESHEET_OTDTO
        Try
            Dim query = From p In Context.AT_TIME_TIMESHEET_OT
                      From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                      From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                      From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                      Where p.ID = obj.ID
            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_OTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .PERIOD_ID = p.p.PERIOD_ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_ID = p.p.ORG_ID,
                                       .NUMBER_FACTOR_PAY = p.p.NUMBER_FACTOR_PAY,
                                       .NUMBER_FACTOR_CP = p.p.NUMBER_FACTOR_CP,
                                       .BACKUP_MONTH_BEFFORE = p.p.BACKUP_MONTH_BEFORE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertLeaveSheetOt(ByVal objTimeSheetDaily As AT_TIME_TIMESHEET_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTimeSheetData As New AT_TIME_TIMESHEET_OT
        Try
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyLeaveSheetOt(ByVal objLeave As AT_TIME_TIMESHEET_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim emp = (From e In Context.HU_EMPLOYEE Where e.EMPLOYEE_CODE.Equals(objLeave.EMPLOYEE_CODE)).FirstOrDefault
            Dim TimeSheetDaily = (From r In Context.AT_TIME_TIMESHEET_OT Where r.EMPLOYEE_ID = emp.ID And r.PERIOD_ID = objLeave.PERIOD_ID).FirstOrDefault
            TimeSheetDaily.NUMBER_FACTOR_PAY = objLeave.NUMBER_FACTOR_PAY
            TimeSheetDaily.NUMBER_FACTOR_CP = objLeave.NUMBER_FACTOR_CP
            TimeSheetDaily.BACKUP_MONTH_BEFORE = objLeave.BACKUP_MONTH_BEFFORE
            Dim congNB = Decimal.Parse((TimeSheetDaily.NUMBER_FACTOR_CP + TimeSheetDaily.BACKUP_MONTH_BEFORE) / 8)
            TimeSheetDaily.CONGHIBU = Math.Round(congNB, 1)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "Tổng hợp công"

    Public Function GetTimeSheet(ByVal _filter As AT_TIME_TIMESHEET_MONTHLYDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MONTHLYDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_TIME_TIMESHEET_MONTHLY
                        From ot In Context.AT_TIME_TIMESHEET_OT.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID And f.PERIOD_ID = p.PERIOD_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                        From w In Context.HU_WORKING.Where(Function(f) f.ID = p.DECISION_ID).DefaultIfEmpty
                        From obj_att In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANCE).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = w.ORG_ID).DefaultIfEmpty
                        From po In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID)
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where p.PERIOD_ID = _filter.PERIOD_ID()

            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_MONTHLYDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .ORG_ID = p.p.ORG_ID,
                                       .PERIOD_ID = p.po.ID,
                                       .PERIOD_STANDARD = p.po.PERIOD_STANDARD,
                                       .STAFF_RANK_NAME = p.s.NAME,
                                       .DECISION_START = p.p.DECISION_START,
                                       .DECISION_END = p.p.DECISION_END,
                                       .WORKING_X = p.p.WORKING_X,
                                       .WORKING_F = p.p.WORKING_F,
                                       .WORKING_E = p.p.WORKING_E,
                                       .WORKING_A = p.p.WORKING_A,
                                       .WORKING_H = p.p.WORKING_H,
                                       .WORKING_D = p.p.WORKING_D,
                                       .WORKING_C = p.p.WORKING_C,
                                       .WORKING_T = p.p.WORKING_T,
                                       .WORKING_Q = p.p.WORKING_Q,
                                       .WORKING_N = p.p.WORKING_N,
                                       .WORKING_P = p.p.WORKING_P,
                                       .WORKING_L = p.p.WORKING_L,
                                       .WORKING_R = p.p.WORKING_R,
                                       .WORKING_S = p.p.WORKING_S,
                                       .WORKING_B = p.p.WORKING_B,
                                       .WORKING_K = p.p.WORKING_K,
                                       .WORKING_J = p.p.WORKING_J,
                                       .WORKING_TS = p.p.WORKING_TS,
                                       .WORKING_O = p.p.WORKING_O,
                                       .WORKING_V = p.p.WORKING_V,
                                       .WORKING_ADD = p.p.WORKING_ADD,
                                       .WORKING_MEAL = p.p.WORKING_MEAL,
                                       .LATE = p.p.LATE,
                                       .COMEBACKOUT = p.p.COMEBACKOUT,
                                       .TOTAL_W_NOSALARY = p.p.TOTAL_W_NOSALARY,
                                       .TOTAL_W_SALARY = p.p.TOTAL_W_SALARY,
                                       .TOTAL_WORKING = p.p.TOTAL_WORKING + If(p.ot.TOTAL_FACTOR_CONVERT Is Nothing, 0, p.ot.TOTAL_FACTOR_CONVERT),
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .TOTAL_FACTOR = p.ot.TOTAL_FACTOR_CONVERT,
                                       .TOTAL_FACTOR1 = p.ot.TOTAL_FACTOR1,
                                       .TOTAL_FACTOR1_5 = p.ot.TOTAL_FACTOR1_5,
                                       .TOTAL_FACTOR2 = p.ot.TOTAL_FACTOR2,
                                       .TOTAL_FACTOR2_7 = p.ot.TOTAL_FACTOR2_7,
                                       .TOTAL_FACTOR3 = p.ot.TOTAL_FACTOR3,
                                       .TOTAL_FACTOR3_9 = p.ot.TOTAL_FACTOR3_9,
                                       .WORKING_DA = p.p.WORKING_DA,
                                       .OBJECT_ATTENDANCE = p.p.OBJECT_ATTENDANCE,
                                       .OBJECT_ATTENDANCE_NAME = p.obj_att.NAME_VN,
                                       .MIN_OUT_WORK = p.p.MIN_OUT_WORK,
                                       .MIN_ON_LEAVE = p.p.MIN_ON_LEAVE,
                                       .MIN_DEDUCT = p.p.MIN_DEDUCT,
                                       .MIN_LATE = p.p.MIN_LATE,
                                       .MIN_LATE_EARLY = p.p.MIN_LATE_EARLY,
                                       .MIN_IN_WORK = p.p.MIN_IN_WORK,
                                       .MIN_DEDUCT_WORK = p.p.MIN_DEDUCT_WORK,
                                       .MIN_OUT_WORK_DEDUCT = p.p.MIN_OUT_WORK_DEDUCT,
                                       .MIN_EARLY = p.p.MIN_EARLY})

            'If _filter.IS_TERMINATE Then
            '    lst = lst.Where(Function(f) f.WORK_STATUS = 257)
            '    If _filter.WORKINGDAY.HasValue Then
            '        lst = lst.Where(Function(f) f.TER_LAST_DATE <= _filter.WORKINGDAY)
            '    End If
            'End If
            'Dim dateNow = Date.Now.Date
            'If Not _filter.IS_TERMINATE Then
            '    lst = lst.Where(Function(f) f.WORK_STATUS <> 257 Or (f.WORK_STATUS = 257 And f.TER_LAST_DATE >= dateNow) Or f.WORK_STATUS Is Nothing)
            'End If
            If _filter.EMPLOYEE_ID.HasValue Then
                lst = lst.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.VN_FULLNAME.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                lst = lst.Where(Function(f) f.VN_FULLNAME.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.WORKING_A.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_A = _filter.WORKING_A)
            End If
            If _filter.WORKING_B.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_B = _filter.WORKING_B)
            End If
            If _filter.WORKING_C.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_C = _filter.WORKING_C)
            End If
            If _filter.WORKING_D.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_D = _filter.WORKING_D)
            End If
            If _filter.WORKING_E.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_E = _filter.WORKING_E)
            End If
            If _filter.WORKING_F.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_F = _filter.WORKING_F)
            End If
            If _filter.WORKING_H.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_H = _filter.WORKING_H)
            End If
            If _filter.WORKING_J.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_J = _filter.WORKING_J)
            End If
            If _filter.WORKING_K.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_K = _filter.WORKING_K)
            End If
            If _filter.WORKING_L.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_L = _filter.WORKING_L)
            End If
            If _filter.WORKING_N.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_N = _filter.WORKING_N)
            End If
            If _filter.WORKING_O.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_O = _filter.WORKING_O)
            End If
            If _filter.WORKING_F.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_P = _filter.WORKING_P)
            End If
            If _filter.WORKING_Q.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_Q = _filter.WORKING_Q)
            End If
            If _filter.WORKING_R.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_R = _filter.WORKING_R)
            End If
            If _filter.WORKING_S.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_S = _filter.WORKING_S)
            End If
            If _filter.WORKING_T.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_T = _filter.WORKING_T)
            End If
            If _filter.WORKING_TS.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_TS = _filter.WORKING_TS)
            End If
            If _filter.WORKING_V.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_V = _filter.WORKING_V)
            End If
            If _filter.WORKING_X.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_X = _filter.WORKING_X)
            End If
            If _filter.TOTAL_WORKING.HasValue Then
                lst = lst.Where(Function(f) f.TOTAL_WORKING = _filter.TOTAL_WORKING)
            End If
            If _filter.WORKING_ADD.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_ADD = _filter.WORKING_ADD)
            End If
            If _filter.LATE.HasValue Then
                lst = lst.Where(Function(f) f.LATE = _filter.LATE)
            End If
            If _filter.COMEBACKOUT.HasValue Then
                lst = lst.Where(Function(f) f.COMEBACKOUT = _filter.COMEBACKOUT)
            End If
            If _filter.TOTAL_W_SALARY.HasValue Then
                lst = lst.Where(Function(f) f.TOTAL_W_SALARY = _filter.TOTAL_W_SALARY)
            End If
            If _filter.TOTAL_W_NOSALARY.HasValue Then
                lst = lst.Where(Function(f) f.TOTAL_W_NOSALARY = _filter.TOTAL_W_NOSALARY)
            End If
            If _filter.WORKING_MEAL.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_MEAL = _filter.WORKING_MEAL)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CAL_TIME_TIMESHEET_MONTHLY(ByVal _param As ParamDTO, ByVal codecase As String, ByVal lstEmployee As List(Of Decimal?), ByVal log As UserLog) As Boolean
        Try
            'Using cls As New DataAccess.NonQueryData
            '    cls.ExecuteSQL("DELETE FROM SE_EMPLOYEE_CHOSEN S WHERE  UPPER(S.USING_USER) ='" + log.Username.ToUpper + "'")
            'End Using
            'Dim dDay = Date.Now
            'For Each emp As Decimal? In lstEmployee
            '    Dim objNew As New SE_EMPLOYEE_CHOSEN
            '    objNew.EMPLOYEE_ID = emp
            '    objNew.USING_USER = log.Username.ToUpper
            '    objNew.WORKINGDAY = dDay
            '    dDay = dDay.AddDays(1)
            '    Context.SE_EMPLOYEE_CHOSEN.AddObject(objNew)
            'Next
            'Context.SaveChanges()
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = _param.PERIOD_ID
            LOG_AT(_param, log, lstEmployee, "TỔNG HỢP BẢNG CÔNG TỔNG HỢP", obj, _param.ORG_ID)
            Using cls As New DataAccess.NonQueryData
                If codecase = "ctrlTimesheetSummary_case1" Then
                    cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIMESHEET_MONTHLY_HOSE",
                                               New With {.P_USERNAME = log.Username,
                                                         .P_PERIOD_ID = _param.PERIOD_ID,
                                                         .P_ORG_ID = _param.ORG_ID,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE})
                Else
                    cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_MONTHLY",
                                               New With {.P_USERNAME = log.Username,
                                                         .P_PERIOD_ID = _param.PERIOD_ID,
                                                         .P_ORG_ID = _param.ORG_ID,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE})
                End If
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function ValidateTimesheet(ByVal _validate As AT_TIME_TIMESHEET_MONTHLYDTO, ByVal sType As String, ByVal log As UserLog)
        Try
            Select Case sType
                Case "BEYOND_STANDARD"
                    Using cls As New DataAccess.QueryData
                        cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                         New With {.P_USERNAME = log.Username.ToUpper,
                                                   .P_ORGID = _validate.ORG_ID,
                                                   .P_ISDISSOLVE = _validate.IS_DISSOLVE})
                    End Using
                    Dim query = (From p In Context.AT_TIME_TIMESHEET_MONTHLY
                                From po In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID)
                                From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And
                                                                          f.USERNAME.ToUpper = log.Username.ToUpper)
                                Where p.PERIOD_ID = _validate.PERIOD_ID And
                                (p.TOTAL_WORKING IsNot Nothing AndAlso
                                 (p.TOTAL_WORKING - If(p.WORKING_V Is Nothing, 0, p.WORKING_V)) > po.PERIOD_STANDARD))
                    Return (query.Count = 0)

            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "Lam thêm"
    Public Function GetRegisterOT(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_OT_REGISTRATIONDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_OT_REGISTRATION
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From typeot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OT_TYPE_ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS).DefaultIfEmpty
                        From s In Context.SE_USER.Where(Function(f) f.USERNAME = p.MODIFIED_BY).DefaultIfEmpty
                        Where p.STATUS = 1
            'From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)


            'If _filter.IS_NB IsNot Nothing Then
            '    query = query.Where(Function(f) f.p.IS_NB = _filter.IS_NB)
            'End If

            'If _filter.TYPE_INPUT IsNot Nothing Then
            '    query = query.Where(Function(f) f.p.TYPE_INPUT = _filter.TYPE_INPUT)
            'End If

            Dim lst = query.Select(Function(p) New AT_OT_REGISTRATIONDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .FULLNAME = p.e.FULLNAME_VN,
                                       .TITLE_ID = p.t.ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .DEPARTMENT_NAME = p.o.NAME_VN,
                                       .DEPARTMENT_ID = p.o.ID,
                                       .SIGN_ID = p.p.SIGN_ID,
                                       .SIGN_CODE = p.p.SIGN_CODE,
                                       .OT_TYPE_ID = p.p.OT_TYPE_ID,
                                       .OT_TYPE_NAME = p.typeot.NAME_VN,
                                       .REGIST_DATE = p.p.REGIST_DATE,
                                       .ID_REGGROUP = p.p.ID_REGGROUP,
                                       .FROM_AM = p.p.FROM_AM,
                                       .TO_AM = p.p.TO_AM,
                                       .FROM_PM = p.p.FROM_PM,
                                       .TO_PM = p.p.TO_PM,
                                       .FROM_AM_MN = p.p.FROM_AM_MN,
                                       .TO_AM_MN = p.p.TO_AM_MN,
                                       .FROM_PM_MN = p.p.FROM_PM_MN,
                                       .TO_PM_MN = p.p.TO_PM_MN,
                                       .FROM_HOUR_AM = "",
                                       .TO_HOUR_AM = "",
                                       .FROM_HOUR_PM = "",
                                       .TO_HOUR_PM = "",
                                       .TOTAL_OT = p.p.TOTAL_OT,
                                       .OT_100 = p.p.OT_100,
                                       .OT_150 = p.p.OT_150,
                                       .OT_200 = p.p.OT_200,
                                       .OT_210 = p.p.OT_210,
                                       .OT_270 = p.p.OT_270,
                                       .OT_300 = p.p.OT_300,
                                       .OT_370 = p.p.OT_370,
                                       .STATUS = p.p.STATUS,
                                       .STATUS_NAME = p.status.NAME_VN,
                                       .REASON = p.p.REASON,
                                       .NOTE = p.p.NOTE,
                                       .IS_DELETED = p.p.IS_DELETED,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .MODIFIED_NAME = p.s.FULLNAME,
                                       .CREATED_DATE = p.p.CREATED_DATE})

            'If _filter.IS_TERMINATE Then
            '    lst = lst.Where(Function(f) f.WORK_STATUS = 257)
            '    If _filter.WORKINGDAY.HasValue Then
            '        lst = lst.Where(Function(f) f.TER_LAST_DATE <= _filter.WORKINGDAY)
            '    Else
            '        lst = lst.Where(Function(f) f.TER_LAST_DATE <= Date.Now)
            '    End If
            'End If
            'Dim dateNow = Date.Now.Date
            'If Not _filter.IS_TERMINATE Then
            '    lst = lst.Where(Function(f) f.WORK_STATUS <> 257 Or (f.WORK_STATUS = 257 And f.TER_LAST_DATE >= dateNow) Or f.WORK_STATUS Is Nothing)
            'End If
            If _filter.REGIST_DATE_FROM.HasValue Then
                lst = lst.Where(Function(f) f.REGIST_DATE >= _filter.REGIST_DATE_FROM)
            End If
            If _filter.REGIST_DATE_TO.HasValue Then
                lst = lst.Where(Function(f) f.REGIST_DATE <= _filter.REGIST_DATE_TO)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.FULLNAME) Then
                lst = lst.Where(Function(f) f.FULLNAME.ToLower().Contains(_filter.FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If _filter.REGIST_DATE.HasValue Then
                lst = lst.Where(Function(f) f.REGIST_DATE = _filter.REGIST_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If

            'For Each item As AT_OT_REGISTRATIONDTO In lst
            '    item.FROM_HOUR_AM = If(item.FROM_AM IsNot Nothing, item.FROM_AM.ToString + ":" + If(item.FROM_AM_MN = 30, item.FROM_AM_MN.ToString, "00"), "")
            '    item.TO_HOUR_AM = If(item.TO_AM IsNot Nothing, item.TO_AM.ToString + ":" + If(item.TO_AM_MN = 30, item.TO_AM_MN.ToString, "00"), "")
            '    item.FROM_HOUR_PM = If(item.FROM_PM IsNot Nothing, item.FROM_PM.ToString + ":" + If(item.FROM_PM_MN = 30, item.FROM_PM_MN.ToString, "00"), "")
            '    item.TO_HOUR_PM = If(item.TO_PM IsNot Nothing, item.TO_PM.ToString + ":" + If(item.TO_PM_MN = 30, item.TO_PM_MN.ToString, "00"), "")
            'Next
            Dim tempLst = lst.ToList
            For i = 0 To tempLst.Count - 1
                tempLst(i).FROM_HOUR_AM = If(tempLst(i).FROM_AM IsNot Nothing, tempLst(i).FROM_AM.ToString + ":" + If(tempLst(i).FROM_AM_MN = 30, tempLst(i).FROM_AM_MN.ToString, "00"), "")
                tempLst(i).TO_HOUR_AM = If(tempLst(i).TO_AM IsNot Nothing, tempLst(i).TO_AM.ToString + ":" + If(tempLst(i).TO_AM_MN = 30, tempLst(i).TO_AM_MN.ToString, "00"), "")
                tempLst(i).FROM_HOUR_PM = If(tempLst(i).FROM_PM IsNot Nothing, tempLst(i).FROM_PM.ToString + ":" + If(tempLst(i).FROM_PM_MN = 30, tempLst(i).FROM_PM_MN.ToString, "00"), "")
                tempLst(i).TO_HOUR_PM = If(tempLst(i).TO_PM IsNot Nothing, tempLst(i).TO_PM.ToString + ":" + If(tempLst(i).TO_PM_MN = 30, tempLst(i).TO_PM_MN.ToString, "00"), "")
            Next

            'tempLst = tempLst.OrderBy(Sorts)
            'tempLst = tempLst.Skip(PageIndex * PageSize).Take(PageSize)
            Return tempLst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetRegisterById(ByVal _id As Decimal?) As AT_REGISTER_OTDTO
        Try

            Dim query = From p In Context.AT_REGISTER_OT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.HS_OT And f.TYPE_CODE = "HS_OT").DefaultIfEmpty
                        From typeot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_OT And f.TYPE_CODE = "TYPE_OT").DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_REGISTER_OTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .TYPE_OT = p.typeot.ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .FROM_HOUR = p.p.FROM_HOUR,
                                       .TO_HOUR = p.p.TO_HOUR,
                                       .BREAK_HOUR = p.p.BREAK_HOUR,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .IS_NB = p.p.IS_NB,
                                       .NOTE = p.p.NOTE,
                                       .HS_OT = p.p.HS_OT,
                                       .HS_OT_NAME = p.ot.CODE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertRegisterOT(ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim employee_id As Decimal?
        Dim org_id As Decimal?

        'Dim emp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = objRegisterOT.EMPLOYEE_CODE And p.WORK_STATUS <> 257 And p.TER_LAST_DATE > objRegisterOT.WORKINGDAY).FirstOrDefault
        Dim emp = (From e In Context.HU_EMPLOYEE
                                From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                                Where e.EMPLOYEE_CODE = objRegisterOT.EMPLOYEE_CODE And e.JOIN_DATE <= objRegisterOT.WORKINGDAY And _
                                (e.TER_EFFECT_DATE Is Nothing Or _
                                 (e.TER_EFFECT_DATE IsNot Nothing And _
                                  e.TER_EFFECT_DATE >= objRegisterOT.WORKINGDAY)) And w.EFFECT_DATE <= objRegisterOT.WORKINGDAY
                          Order By w.EFFECT_DATE Descending
                          Select w).FirstOrDefault
        If emp IsNot Nothing Then
            employee_id = emp.EMPLOYEE_ID
            org_id = emp.ORG_ID
        Else
            Exit Function
        End If
        Try
            Dim exists = (From r In Context.AT_REGISTER_OT
                          Where r.EMPLOYEE_ID = employee_id And
                          r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                          objRegisterOT.FROM_HOUR < r.TO_HOUR And
                          objRegisterOT.TO_HOUR > r.FROM_HOUR And
                          r.TYPE_INPUT = objRegisterOT.TYPE_INPUT).Any
            If exists Then
                Dim obj = (From r In Context.AT_REGISTER_OT
                           Where r.EMPLOYEE_ID = employee_id And
                           r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                           objRegisterOT.FROM_HOUR < r.TO_HOUR And
                           objRegisterOT.TO_HOUR > r.FROM_HOUR And
                           r.TYPE_INPUT = objRegisterOT.TYPE_INPUT).FirstOrDefault
                obj.FROM_HOUR = objRegisterOT.FROM_HOUR
                obj.TO_HOUR = objRegisterOT.TO_HOUR
                obj.HOUR = objRegisterOT.HOUR
                obj.NOTE = objRegisterOT.NOTE
                obj.TYPE_INPUT = objRegisterOT.TYPE_INPUT
                obj.IS_NB = objRegisterOT.IS_NB
                obj.HS_OT = objRegisterOT.HS_OT
                obj.TYPE_OT = objRegisterOT.TYPE_OT
            Else
                Dim objRegisterOTData As New AT_REGISTER_OT
                objRegisterOTData.ID = Utilities.GetNextSequence(Context, Context.AT_REGISTER_OT.EntitySet.Name)
                objRegisterOTData.EMPLOYEE_ID = employee_id
                objRegisterOTData.WORKINGDAY = objRegisterOT.WORKINGDAY
                objRegisterOTData.FROM_HOUR = objRegisterOT.FROM_HOUR
                objRegisterOTData.TO_HOUR = objRegisterOT.TO_HOUR
                objRegisterOTData.NOTE = objRegisterOT.NOTE
                objRegisterOTData.TYPE_INPUT = objRegisterOT.TYPE_INPUT
                objRegisterOTData.HOUR = objRegisterOT.HOUR
                objRegisterOTData.HS_OT = objRegisterOT.HS_OT
                objRegisterOTData.IS_NB = objRegisterOT.IS_NB
                objRegisterOTData.TYPE_OT = objRegisterOT.TYPE_OT
                objRegisterOTData.APPROVE_ID = 0
                objRegisterOTData.BREAK_HOUR = objRegisterOT.BREAK_HOUR
                Context.AT_REGISTER_OT.AddObject(objRegisterOTData)
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertDataRegisterOT(ByVal objRegisterOTList As List(Of AT_REGISTER_OTDTO), ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim employee_id As Decimal?
        Dim org_id As Decimal?
        Dim objData As New AT_REGISTER_OTDTO
        Try
            For index = 0 To objRegisterOTList.Count - 1
                objData = objRegisterOTList(index)
                'Dim emp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = objRegisterOT.EMPLOYEE_CODE And p.WORK_STATUS <> 257 And p.TER_LAST_DATE > objRegisterOT.WORKINGDAY).FirstOrDefault
                Dim emp = (From e In Context.HU_EMPLOYEE
                                        From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                                        Where e.EMPLOYEE_CODE = objData.EMPLOYEE_CODE And e.JOIN_DATE <= objRegisterOT.WORKINGDAY And _
                                        (e.TER_EFFECT_DATE Is Nothing Or _
                                         (e.TER_EFFECT_DATE IsNot Nothing And _
                                          e.TER_EFFECT_DATE >= objRegisterOT.WORKINGDAY)) And w.EFFECT_DATE <= objRegisterOT.WORKINGDAY
                                  Order By w.EFFECT_DATE Descending
                                  Select w).FirstOrDefault
                If emp IsNot Nothing Then
                    employee_id = emp.EMPLOYEE_ID
                    org_id = emp.ORG_ID
                Else
                    Continue For
                End If
                Dim exists = (From r In Context.AT_REGISTER_OT
                              Where r.EMPLOYEE_ID = employee_id And
                              r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                              objRegisterOT.FROM_HOUR = r.TO_HOUR And
                              objRegisterOT.TO_HOUR = r.FROM_HOUR And
                              r.TYPE_INPUT = objRegisterOT.TYPE_INPUT).Any
                If exists Then
                    Dim obj = (From r In Context.AT_REGISTER_OT
                               Where r.EMPLOYEE_ID = employee_id And
                               r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                               objRegisterOT.FROM_HOUR = r.TO_HOUR And
                               objRegisterOT.TO_HOUR = r.FROM_HOUR And
                               r.TYPE_INPUT = objRegisterOT.TYPE_INPUT).FirstOrDefault

                    obj.FROM_HOUR = objRegisterOT.FROM_HOUR
                    obj.TO_HOUR = objRegisterOT.TO_HOUR
                    obj.HOUR = objRegisterOT.HOUR
                    obj.NOTE = objRegisterOT.NOTE
                    obj.TYPE_INPUT = objRegisterOT.TYPE_INPUT
                    obj.IS_NB = objRegisterOT.IS_NB
                    obj.HS_OT = objRegisterOT.HS_OT
                    obj.TYPE_OT = objRegisterOT.TYPE_OT
                    obj.BREAK_HOUR = objRegisterOT.BREAK_HOUR
                Else
                    Dim objRegisterOTData As New AT_REGISTER_OT
                    objRegisterOTData.ID = Utilities.GetNextSequence(Context, Context.AT_REGISTER_OT.EntitySet.Name)
                    objRegisterOTData.EMPLOYEE_ID = employee_id
                    objRegisterOTData.WORKINGDAY = objRegisterOT.WORKINGDAY
                    objRegisterOTData.FROM_HOUR = objRegisterOT.FROM_HOUR
                    objRegisterOTData.TO_HOUR = objRegisterOT.TO_HOUR
                    objRegisterOTData.NOTE = objRegisterOT.NOTE
                    objRegisterOTData.TYPE_INPUT = objRegisterOT.TYPE_INPUT
                    objRegisterOTData.HOUR = objRegisterOT.HOUR
                    objRegisterOTData.HS_OT = objRegisterOT.HS_OT
                    objRegisterOTData.IS_NB = objRegisterOT.IS_NB
                    objRegisterOTData.TYPE_OT = objRegisterOT.TYPE_OT
                    objRegisterOTData.APPROVE_ID = 0
                    objRegisterOTData.BREAK_HOUR = objRegisterOT.BREAK_HOUR
                    Context.AT_REGISTER_OT.AddObject(objRegisterOTData)
                End If
                Context.SaveChanges(log)
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyRegisterOT(ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objRegisterOTData As New AT_REGISTER_OT With {.ID = objRegisterOT.ID}
        Try
            Dim exists = (From r In Context.AT_REGISTER_OT Where r.ID = objRegisterOT.ID).Any
            If exists Then
                Dim obj = (From r In Context.AT_REGISTER_OT Where r.ID = objRegisterOT.ID).FirstOrDefault
                obj.WORKINGDAY = objRegisterOT.WORKINGDAY
                obj.FROM_HOUR = objRegisterOT.FROM_HOUR
                obj.TO_HOUR = objRegisterOT.TO_HOUR
                obj.HS_OT = objRegisterOT.HS_OT
                obj.TYPE_INPUT = objRegisterOT.TYPE_INPUT
                obj.IS_NB = objRegisterOT.IS_NB
                obj.HOUR = objRegisterOT.HOUR
                obj.NOTE = objRegisterOT.NOTE
                obj.TYPE_OT = objRegisterOT.TYPE_OT
                obj.BREAK_HOUR = objRegisterOT.BREAK_HOUR
            Else
                Return False
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateRegisterOT(ByVal _validate As AT_REGISTER_OTDTO)
        Dim query
        Try
            If _validate.WORKINGDAY.HasValue Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_REGISTER_OT
                             Where p.WORKINGDAY = _validate.WORKINGDAY And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_REGISTER_OT
                             Where p.WORKINGDAY = _validate.WORKINGDAY).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeleteRegisterOT(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstl As List(Of AT_REGISTER_OT)
        Try
            lstl = (From p In Context.AT_REGISTER_OT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstl.Count - 1
                Context.AT_REGISTER_OT.DeleteObject(lstl(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

    Public Function CheckImporAddNewtOT(ByVal objRegisterOT As AT_REGISTER_OTDTO) As Boolean
        Dim employee_id As Decimal?
        Dim org_id As Decimal?
        Dim emp = (From e In Context.HU_EMPLOYEE
                                From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                                Where e.EMPLOYEE_CODE = objRegisterOT.EMPLOYEE_CODE And e.JOIN_DATE <= objRegisterOT.WORKINGDAY And _
                                (e.TER_EFFECT_DATE Is Nothing Or _
                                 (e.TER_EFFECT_DATE IsNot Nothing And _
                                  e.TER_EFFECT_DATE >= objRegisterOT.WORKINGDAY)) And w.EFFECT_DATE <= objRegisterOT.WORKINGDAY
                          Order By w.EFFECT_DATE Descending
                          Select w).FirstOrDefault
        If emp IsNot Nothing Then
            employee_id = emp.EMPLOYEE_ID
            org_id = emp.ORG_ID
        Else
            Exit Function
        End If
        Try
            Dim exists

            If objRegisterOT.ID IsNot Nothing Then
                exists = (From r In Context.AT_REGISTER_OT
                          Where r.EMPLOYEE_ID = employee_id And
                          r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                          objRegisterOT.FROM_HOUR < r.TO_HOUR And
                          objRegisterOT.TO_HOUR > r.FROM_HOUR And
                          r.TYPE_INPUT = objRegisterOT.TYPE_INPUT And
                          r.ID <> objRegisterOT.ID).Any
            Else
                exists = (From r In Context.AT_REGISTER_OT
                          Where r.EMPLOYEE_ID = employee_id And
                          r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                          objRegisterOT.FROM_HOUR < r.TO_HOUR And
                          objRegisterOT.TO_HOUR > r.FROM_HOUR And
                          r.TYPE_INPUT = objRegisterOT.TYPE_INPUT).Any
            End If
            If exists Then ' có dữ liệu trả lại false không cho phép đăng ký tiếp 
                Return False
            Else 'không  có dữ liệu trả lại true cho phép đăng ký tiếp 
                Return True
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CheckDataListImportAddNew(ByVal objRegisterOTList As List(Of AT_REGISTER_OTDTO), ByVal objRegisterOT As AT_REGISTER_OTDTO, ByRef strEmployeeCode As String) As Boolean
        Dim employee_id As Decimal?
        Dim org_id As Decimal?
        Dim objData As New AT_REGISTER_OTDTO
        Try
            For index = 0 To objRegisterOTList.Count - 1
                objData = objRegisterOTList(index)
                'Dim emp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = objRegisterOT.EMPLOYEE_CODE And p.WORK_STATUS <> 257 And p.TER_LAST_DATE > objRegisterOT.WORKINGDAY).FirstOrDefault
                Dim emp = (From e In Context.HU_EMPLOYEE
                                        From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                                        Where e.EMPLOYEE_CODE = objData.EMPLOYEE_CODE And e.JOIN_DATE <= objRegisterOT.WORKINGDAY And _
                                        (e.TER_EFFECT_DATE Is Nothing Or _
                                         (e.TER_EFFECT_DATE IsNot Nothing And _
                                          e.TER_EFFECT_DATE >= objRegisterOT.WORKINGDAY)) And w.EFFECT_DATE <= objRegisterOT.WORKINGDAY
                                  Order By w.EFFECT_DATE Descending
                                  Select w).FirstOrDefault
                If emp IsNot Nothing Then
                    employee_id = emp.EMPLOYEE_ID
                    org_id = emp.ORG_ID
                Else
                    Continue For
                End If
                Dim exists = (From r In Context.AT_REGISTER_OT
                              Where r.EMPLOYEE_ID = employee_id And
                              r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                              objRegisterOT.FROM_HOUR < r.TO_HOUR And
                              objRegisterOT.TO_HOUR > r.FROM_HOUR And
                              r.TYPE_INPUT = objRegisterOT.TYPE_INPUT).Any
                If exists Then
                    strEmployeeCode = strEmployeeCode & objData.EMPLOYEE_CODE & ","
                End If
            Next
            If Not strEmployeeCode.Equals("") Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ApproveRegisterOT(ByVal lstData As List(Of AT_REGISTER_OTDTO), ByVal status_id As Decimal) As Boolean
        Dim lstID As New List(Of Decimal?)
        Try
            lstID = (From p In lstData Select p.ID).ToList
            Dim lstl = (From p In Context.AT_REGISTER_OT Where lstID.Contains(p.ID)).ToList
            For Each obj In lstl
                Dim objData = (From p In lstData
                               Where p.ID = obj.ID).FirstOrDefault
                If objData IsNot Nothing Then
                    obj.APPROVE_DATE = Date.Now
                    obj.APPROVE_HOUR = objData.APPROVE_HOUR
                    obj.APPROVE_ID = status_id
                End If
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "Khai bao cong com"
    Public Function GetDelareRice(ByVal _filter As AT_TIME_RICEDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_RICEDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_TIME_RICE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)

            Dim lst = query.Select(Function(p) New AT_TIME_RICEDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .ORG_ID = p.p.ORG_ID,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .ACTFLG = p.p.ACTFLG,
                                       .PRICE = p.p.PRICE,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            Dim dateNow = Date.Now.Date
            If _filter.IS_TERMINATE Then
                lst = lst.Where(Function(f) f.WORK_STATUS = 257 And f.TER_LAST_DATE <= dateNow)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                lst = lst.Where(Function(f) f.VN_FULLNAME.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If _filter.WORKINGDAY.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY = _filter.WORKINGDAY)
            End If
            'If _filter.IS_TERMINATE Then
            '    lst = lst.Where(Function(f) f.e.WORK_STATUS = 257)
            '    If _filter.WORKINGDAY.HasValue Then
            '        lst = lst.Where(Function(f) f.e.TER_LAST_DATE <= _filter.WORKINGDAY)
            '    End If
            'End If
            If _filter.FROM_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY <= _filter.END_DATE)
            End If
            If _filter.PRICE.HasValue Then
                lst = lst.Where(Function(f) f.PRICE = _filter.PRICE)
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveDelareRice(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_TIME_RICE)
        Try
            lstData = (From p In Context.AT_TIME_RICE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetDelareRiceById(ByVal _id As Decimal?) As AT_TIME_RICEDTO
        Try

            Dim query = From p In Context.AT_TIME_RICE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_TIME_RICEDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_ID = p.p.ORG_ID,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .ACTFLG = p.p.ACTFLG,
                                       .PRICE = p.p.PRICE,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .NOTE = p.p.NOTE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertDelareRice(ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim employee_id As Decimal?
        Dim org_id As Decimal?
        Try
            Dim exists = (From r In Context.AT_TIME_RICE Where r.EMPLOYEE_ID = objDelareRice.EMPLOYEE_ID And r.WORKINGDAY = objDelareRice.WORKINGDAY).Any

            If exists Then
                Dim obj = (From r In Context.AT_TIME_RICE Where r.EMPLOYEE_ID = objDelareRice.EMPLOYEE_ID And r.WORKINGDAY = objDelareRice.WORKINGDAY).FirstOrDefault
                obj.PRICE = objDelareRice.PRICE
                obj.WORKINGDAY = objDelareRice.WORKINGDAY
            Else
                Dim objDelareRiceData As New AT_TIME_RICE
                objDelareRiceData.ID = Utilities.GetNextSequence(Context, Context.AT_TIME_RICE.EntitySet.Name)
                Dim emp = (From p In Context.HU_EMPLOYEE Where p.ID = objDelareRice.EMPLOYEE_ID).FirstOrDefault
                objDelareRiceData.EMPLOYEE_ID = emp.ID
                employee_id = emp.ID
                org_id = emp.ORG_ID
                objDelareRiceData.WORKINGDAY = objDelareRice.WORKINGDAY
                objDelareRiceData.ORG_ID = emp.ORG_ID
                objDelareRiceData.PRICE = objDelareRice.PRICE
                objDelareRiceData.STAFF_RANK_ID = objDelareRice.STAFF_RANK_ID
                objDelareRiceData.TITLE_ID = objDelareRice.TITLE_ID
                objDelareRiceData.NOTE = objDelareRice.NOTE
                Context.AT_TIME_RICE.AddObject(objDelareRiceData)
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertDelareRiceList(ByVal objDelareRiceList As List(Of AT_TIME_RICEDTO), ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New AT_TIME_RICEDTO
        Try
            For index = 0 To objDelareRiceList.Count - 1
                objData = objDelareRiceList(index)
                Dim exists = (From r In Context.AT_TIME_RICE Where r.EMPLOYEE_ID = objData.EMPLOYEE_ID And r.WORKINGDAY = objDelareRice.WORKINGDAY).Any
                If exists Then
                    Dim obj = (From r In Context.AT_TIME_RICE Where r.EMPLOYEE_ID = objData.EMPLOYEE_ID And r.WORKINGDAY = objDelareRice.WORKINGDAY).FirstOrDefault
                    obj.PRICE = objDelareRice.PRICE
                    obj.WORKINGDAY = objDelareRice.WORKINGDAY
                Else
                    Dim objDelareRiceData As New AT_TIME_RICE
                    objDelareRiceData.ID = Utilities.GetNextSequence(Context, Context.AT_TIME_RICE.EntitySet.Name)
                    objDelareRiceData.EMPLOYEE_ID = objData.EMPLOYEE_ID
                    objDelareRiceData.ORG_ID = objData.ORG_ID ' trường hợp này phải lưu org hiện tại của nhân viên
                    objDelareRiceData.WORKINGDAY = objDelareRice.WORKINGDAY
                    objDelareRiceData.PRICE = objDelareRice.PRICE
                    objDelareRiceData.STAFF_RANK_ID = objDelareRice.STAFF_RANK_ID
                    objDelareRiceData.TITLE_ID = objDelareRice.TITLE_ID
                    objDelareRiceData.NOTE = objDelareRice.NOTE
                    Context.AT_TIME_RICE.AddObject(objDelareRiceData)
                End If
                Context.SaveChanges(log)
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateDelareRice(ByVal _validate As AT_TIME_RICEDTO)
        Dim query
        Try
            Dim exists = (From r In Context.AT_TIME_RICE Where r.ID = _validate.ID And r.EMPLOYEE_ID = _validate.EMPLOYEE_ID And
                                                                                          r.WORKINGDAY = _validate.WORKINGDAY).Any

            If _validate.WORKINGDAY.HasValue Then
                If exists And _validate.ID <> 0 Then
                    query = (From p In Context.AT_TIME_RICE
                             Where p.ID <> _validate.ID And p.WORKINGDAY = _validate.WORKINGDAY And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID).Any
                Else
                    query = (From p In Context.AT_TIME_RICE
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID And p.WORKINGDAY = _validate.WORKINGDAY).Any
                End If
                If query Then
                    Return True
                Else
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyDelareRice(ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objDelareRiceData As New AT_TIME_RICE With {.ID = objDelareRice.ID}
        Try
            Dim exists = (From r In Context.AT_TIME_RICE Where r.ID = objDelareRice.ID).Any
            If exists Then
                Dim obj = (From r In Context.AT_TIME_RICE Where r.ID = objDelareRice.ID).FirstOrDefault
                obj.ORG_ID = objDelareRice.ORG_ID
                obj.PRICE = objDelareRice.PRICE
                obj.NOTE = objDelareRice.NOTE
            Else
                Return False
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function DeleteDelareRice(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstl As List(Of AT_TIME_RICE)
        Try
            lstl = (From p In Context.AT_TIME_RICE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstl.Count - 1
                Context.AT_TIME_RICE.DeleteObject(lstl(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function
#End Region
#Region "quan ly cham cong bu tru"
    Public Function GetOffSettingTimeKeeping(ByVal _filter As AT_OFFFSETTINGDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_OFFFSETTINGDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_OFFSETTING_TIMEKEEPING
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_BT).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)

            Dim lst = query.Select(Function(p) New AT_OFFFSETTINGDTO With {
                                      .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .FULLNAME_VN = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .FROMDATE = p.p.FROMDATE,
                                       .TODATE = p.p.TODATE,
                                       .TYPE_BT = p.p.TYPE_BT,
                                       .REMARK = p.p.REMARK,
                                       .TYPE_NAME = p.ot.NAME_VN,
                                       .MINUTES_BT = p.p.MINUTES_BT,
                                        .WORK_STATUS = p.e.WORK_STATUS,
                                        .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            'If _filter.IS_TERMINATE Then
            '    query = query.Where(Function(f) f.e.WORK_STATUS = 257 And f.e.TER_LAST_DATE <= Date.Now)
            'End If
            Dim dateNow = Date.Now.Date
            If Not _filter.IS_TERMINATE Then
                lst = lst.Where(Function(f) f.WORK_STATUS <> 257 Or (f.WORK_STATUS = 257 And f.TER_LAST_DATE >= dateNow) Or f.WORK_STATUS Is Nothing)
            End If
            If _filter.FROMDATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.FROMDATE >= _filter.FROMDATE)
            End If
            If _filter.TODATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.TODATE >= _filter.TODATE)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.FULLNAME_VN) Then
                lst = lst.Where(Function(f) f.FULLNAME_VN.ToLower().Contains(_filter.FULLNAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Dim lst_Tem As List(Of AT_OFFFSETTINGDTO) = lst.ToList
            Dim objData As New AT_OFFFSETTINGDTO
            For index = 0 To lst_Tem.Count - 1
                objData = lst_Tem(index)

                Dim dtCount As DataTable = CountTimeKeeping_Emp(objData.ID.ToString())
                If (dtCount.Rows.Count > 0) And (dtCount.Rows(0)(0) > 1) Then
                    lst_Tem(index).EMPLOYEE_CODE = "Nhiều nhân viên"
                    lst_Tem(index).FULLNAME_VN = "Nhiều nhân viên"
                    lst_Tem(index).TITLE_NAME = "Nhiều nhân viên"
                    lst_Tem(index).ORG_NAME = "Nhiều nhân viên"
                End If
            Next

            Return lst_Tem

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function CountTimeKeeping_Emp(ByVal group_id As String) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteSQL("SELECT COUNT(*) FROM AT_OFFSETTING_TIMEKEEPING_EMP TE WHERE TE.GROUP_ID ='" + group_id + "'")

                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function GetEmployeeTimeKeepingID(ByVal ComId As Decimal) As List(Of AT_OFFFSETTING_EMPDTO)
        Try
            Dim q = (From d In Context.AT_OFFSETTING_TIMEKEEPING_EMP Where d.GROUP_ID = ComId
                    From e In Context.HU_EMPLOYEE.Where(Function(e) e.ID = d.EMPLOYEE_ID)
                    From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = e.ORG_ID)
                    From t In Context.HU_TITLE.Where(Function(t) t.ID = e.TITLE_ID)
                    Select New AT_OFFFSETTING_EMPDTO With {.EMPLOYEE_ID = d.EMPLOYEE_ID,
                                                  .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                  .FULLNAME_VN = e.FULLNAME_VN,
                                                  .ORG_NAME = o.NAME_VN,
                                                  .TITLE_ID = e.TITLE_ID,
                                                  .ORG_ID = e.ORG_ID,
                                                  .TITLE_NAME = t.NAME_VN}).ToList
            Return q.Distinct.ToList()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetOffSettingTimeKeepingById(ByVal _id As Decimal?) As AT_OFFFSETTINGDTO
        Try

            Dim query = From p In Context.AT_OFFSETTING_TIMEKEEPING
                        From ce In Context.AT_OFFSETTING_TIMEKEEPING_EMP.Where(Function(f) f.GROUP_ID = p.ID)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = ce.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_BT).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_OFFFSETTINGDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .FULLNAME_VN = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .TYPE_BT = p.p.TYPE_BT,
                                       .TYPE_NAME = p.ot.NAME_VN,
                                       .MINUTES_BT = p.p.MINUTES_BT,
                                       .REMARK = p.p.REMARK,
                                       .FROMDATE = p.p.FROMDATE,
                                       .TODATE = p.p.FROMDATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region
#Region "Khai báo điều chỉnh thâm niên phép"
    Public Function GetDelareEntitlementNB(ByVal _filter As AT_DECLARE_ENTITLEMENTDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_DECLARE_ENTITLEMENTDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_DECLARE_ENTITLEMENT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)

            Dim lst = query.Select(Function(p) New AT_DECLARE_ENTITLEMENTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .ORG_ID = p.e.ORG_ID,
                                       .YEAR = p.p.YEAR,
                                       .JOIN_DATE = p.p.JOIN_DATE,
                                       .YEAR_NB = p.p.YEAR_NB,
                                       .YEAR_ENTITLEMENT = p.p.YEAR_ENTITLEMENT,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .ADJUST_MONTH_TN = p.p.ADJUST_MONTH_TN,
                                       .REMARK_TN = p.p.REMARK_TN,
                                       .ADJUST_ENTITLEMENT = p.p.ADJUST_ENTITLEMENT,
                                       .ADJUST_MONTH_ENTITLEMENT = p.p.ADJUST_MONTH_ENTITLEMENT,
                                       .REMARK_ENTITLEMENT = p.p.REMARK_ENTITLEMENT,
                                       .START_MONTH_TN = p.p.START_MONTH_TN,
                                       .START_MONTH_EXTEND = p.p.START_MONTH_EXTEND,
                                       .ADJUST_NB = p.p.ADJUST_NB,
                                       .START_MONTH_NB = p.p.START_MONTH_NB,
                                       .REMARK_NB = p.p.REMARK_NB,
                                       .MONTH_EXTENSION_NB = p.p.MONTH_EXTENSION_NB,
                                       .COM_PAY = p.p.COM_PAY,
                                       .ENT_PAY = p.p.ENT_PAY,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            'If _filter.IS_TERMINATE Then
            '    query = query.Where(Function(f) f.e.WORK_STATUS = 257 And f.e.TER_LAST_DATE <= Date.Now)
            'End If
            Dim dateNow = Date.Now.Date
            If Not _filter.IS_TERMINATE Then
                lst = lst.Where(Function(f) f.WORK_STATUS <> 257 Or (f.WORK_STATUS = 257 And f.TER_LAST_DATE >= dateNow) Or f.WORK_STATUS Is Nothing)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.VN_FULLNAME.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                lst = lst.Where(Function(f) f.VN_FULLNAME.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.ADJUST_MONTH_TN.HasValue Then
                lst = lst.Where(Function(f) f.ADJUST_MONTH_TN = _filter.ADJUST_MONTH_TN)
            End If
            If _filter.ADJUST_ENTITLEMENT.HasValue Then
                lst = lst.Where(Function(f) f.ADJUST_ENTITLEMENT = _filter.ADJUST_ENTITLEMENT)
            End If
            If _filter.ADJUST_MONTH_ENTITLEMENT.HasValue Then
                lst = lst.Where(Function(f) f.ADJUST_MONTH_ENTITLEMENT = _filter.ADJUST_MONTH_ENTITLEMENT)
            End If

            If _filter.START_MONTH_TN.HasValue Then
                lst = lst.Where(Function(f) f.START_MONTH_TN = _filter.START_MONTH_TN)
            End If
            If _filter.YEAR.HasValue Then
                lst = lst.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.START_MONTH_EXTEND.HasValue Then
                lst = lst.Where(Function(f) f.START_MONTH_EXTEND = _filter.START_MONTH_EXTEND)
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK_TN) Then
                lst = lst.Where(Function(f) f.REMARK_TN.ToLower().Contains(_filter.REMARK_TN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK_ENTITLEMENT) Then
                lst = lst.Where(Function(f) f.REMARK_ENTITLEMENT.ToLower().Contains(_filter.REMARK_ENTITLEMENT.ToLower()))
            End If
            If _filter.YEAR_NB.HasValue Then
                lst = lst.Where(Function(f) f.YEAR_NB = _filter.YEAR_NB)
            End If
            If _filter.YEAR_ENTITLEMENT.HasValue Then
                lst = lst.Where(Function(f) f.YEAR_ENTITLEMENT = _filter.YEAR_ENTITLEMENT)
            End If
            If _filter.MONTH_EXTENSION_NB.HasValue Then
                lst = lst.Where(Function(f) f.MONTH_EXTENSION_NB = _filter.MONTH_EXTENSION_NB)
            End If
            If _filter.COM_PAY.HasValue Then
                lst = lst.Where(Function(f) f.COM_PAY = _filter.COM_PAY)
            End If
            If _filter.ENT_PAY.HasValue Then
                lst = lst.Where(Function(f) f.ENT_PAY = _filter.ENT_PAY)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveDelareEntitlementNB(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        Try

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetDelareEntitlementNBById(ByVal _id As Decimal?) As AT_DECLARE_ENTITLEMENTDTO
        Try

            Dim query = From p In Context.AT_DECLARE_ENTITLEMENT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_DECLARE_ENTITLEMENTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .YEAR = p.p.YEAR,
                                       .YEAR_NB = p.p.YEAR_NB,
                                       .YEAR_ENTITLEMENT = p.p.YEAR_ENTITLEMENT,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .ADJUST_MONTH_TN = p.p.ADJUST_MONTH_TN,
                                       .REMARK_TN = p.p.REMARK_TN,
                                       .JOIN_DATE = p.p.JOIN_DATE,
                                       .ADJUST_ENTITLEMENT = p.p.ADJUST_ENTITLEMENT,
                                       .ADJUST_MONTH_ENTITLEMENT = p.p.ADJUST_MONTH_ENTITLEMENT,
                                       .REMARK_ENTITLEMENT = p.p.REMARK_ENTITLEMENT,
                                       .START_MONTH_TN = p.p.START_MONTH_TN,
                                       .START_MONTH_EXTEND = p.p.START_MONTH_EXTEND,
                                       .ADJUST_NB = p.p.ADJUST_NB,
                                       .START_MONTH_NB = p.p.START_MONTH_NB,
                                       .REMARK_NB = p.p.REMARK_NB,
                                       .MONTH_EXTENSION_NB = p.p.MONTH_EXTENSION_NB,
                                       .COM_PAY = p.p.COM_PAY,
                                       .ENT_PAY = p.p.ENT_PAY,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function InsertOffSettingTime(ByVal objOffSetting As AT_OFFFSETTINGDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objOffsettingData As New AT_OFFSETTING_TIMEKEEPING

        Try
            ' thêm kỷ luật
            objOffsettingData.ID = Utilities.GetNextSequence(Context, Context.AT_OFFSETTING_TIMEKEEPING.EntitySet.Name)
            objOffSetting.ID = objOffsettingData.ID
            objOffsettingData.REMARK = objOffSetting.REMARK
            objOffsettingData.MINUTES_BT = objOffSetting.MINUTES_BT
            objOffsettingData.FROMDATE = objOffSetting.FROMDATE
            objOffsettingData.EMPLOYEE_ID = objOffSetting.EMPLOYEE_ID
            objOffsettingData.TODATE = objOffSetting.TODATE
            objOffsettingData.TYPE_BT = objOffSetting.TYPE_BT
            objOffsettingData.CREATED_DATE = DateTime.Now
            objOffsettingData.CREATED_BY = log.Username
            objOffsettingData.CREATED_LOG = log.ComputerName
            objOffsettingData.MODIFIED_DATE = DateTime.Now
            objOffsettingData.MODIFIED_BY = log.Username
            objOffsettingData.MODIFIED_LOG = log.ComputerName


            Context.AT_OFFSETTING_TIMEKEEPING.AddObject(objOffsettingData)
            InsertObjectType(objOffSetting)
            Context.SaveChanges(log)
            gID = objOffsettingData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Private Sub InsertObjectType(ByVal objOffSetting As AT_OFFFSETTINGDTO)
        Dim objDataEmp As AT_OFFSETTING_TIMEKEEPING_EMP
        Dim rep As New AttendanceRepository

        If objOffSetting.OFFFSETTING_EMP IsNot Nothing Then
            'xoa danh sach nhan viên cũ truoc khi insert lại
            Dim objD = (From d In Context.AT_OFFSETTING_TIMEKEEPING_EMP Where d.GROUP_ID = objOffSetting.ID).ToList
            For Each obj As AT_OFFSETTING_TIMEKEEPING_EMP In objD
                Context.AT_OFFSETTING_TIMEKEEPING_EMP.DeleteObject(obj)
            Next

            'insert nhan vien mới
            For Each obj As AT_OFFFSETTING_EMPDTO In objOffSetting.OFFFSETTING_EMP
                objDataEmp = New AT_OFFSETTING_TIMEKEEPING_EMP
                objDataEmp.ID = Utilities.GetNextSequence(Context, Context.AT_OFFSETTING_TIMEKEEPING_EMP.EntitySet.Name)
                objDataEmp.GROUP_ID = objOffSetting.ID
                objDataEmp.EMPLOYEE_ID = obj.EMPLOYEE_ID
                objDataEmp.TITILE_ID = obj.TITLE_ID
                objDataEmp.ORG_ID = obj.ORG_ID
                objDataEmp.TYPE_BT = objOffSetting.TYPE_BT
                objDataEmp.WORKING_DAY = obj.WORKING_DAY
                Context.AT_OFFSETTING_TIMEKEEPING_EMP.AddObject(objDataEmp)
            Next
        End If
    End Sub
    Public Function ModifyOffSettingTime(ByVal objOffSetting As AT_OFFFSETTINGDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objOffSettingData As New AT_OFFSETTING_TIMEKEEPING With {.ID = objOffSetting.ID}

        Try
            Context.AT_OFFSETTING_TIMEKEEPING.Attach(objOffSettingData)
            ' sửa kỷ luật
            objOffSettingData.ID = objOffSetting.ID
            objOffSettingData.REMARK = objOffSetting.REMARK
            objOffSettingData.TYPE_BT = objOffSetting.TYPE_BT
            objOffSettingData.MINUTES_BT = objOffSetting.MINUTES_BT
            objOffSettingData.FROMDATE = objOffSetting.FROMDATE
            objOffSettingData.EMPLOYEE_ID = objOffSetting.EMPLOYEE_ID
            objOffSettingData.TODATE = objOffSetting.TODATE
            objOffSettingData.MODIFIED_DATE = DateTime.Now
            objOffSettingData.MODIFIED_BY = log.Username
            objOffSettingData.MODIFIED_LOG = log.ComputerName
            ' thêm quyết định            
            InsertObjectType(objOffSetting)
            Context.SaveChanges(log)
            gID = objOffSettingData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function InsertDelareEntitlementNB(ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean
        Try
            ' check nghỉ bù chỉ được gia hạn 1 lần trong năm
            If objDelareEntitlementNB.MONTH_EXTENSION_NB IsNot Nothing Then
                If objDelareEntitlementNB.ID IsNot Nothing Then ' trường hợp sửa phải kiểm tra khác id hiện tại
                    checkMonthNB = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID And t.MONTH_EXTENSION_NB IsNot Nothing And t.ID <> objDelareEntitlementNB.ID And t.YEAR = objDelareEntitlementNB.YEAR).Any
                Else
                    checkMonthNB = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID And t.MONTH_EXTENSION_NB IsNot Nothing And t.YEAR = objDelareEntitlementNB.YEAR).Any
                End If
            End If
            ' check nghỉ phép chỉ được gia hạn 1 lần trong năm
            If objDelareEntitlementNB.START_MONTH_EXTEND IsNot Nothing Then
                If objDelareEntitlementNB.ID IsNot Nothing Then ' trường hợp sửa phải kiểm tra khác id hiện tại
                    checkMonthNP = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID And t.START_MONTH_EXTEND IsNot Nothing And t.ID <> objDelareEntitlementNB.ID And t.YEAR = objDelareEntitlementNB.YEAR).Any
                Else
                    checkMonthNP = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID And t.START_MONTH_EXTEND IsNot Nothing And t.YEAR = objDelareEntitlementNB.YEAR).Any
                End If
            End If
            If checkMonthNB = False And checkMonthNP = False Then
                Dim exists = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.ID = objDelareEntitlementNB.ID).Any
                If exists Then
                    Dim obj = (From r In Context.AT_DECLARE_ENTITLEMENT Where r.ID = objDelareEntitlementNB.ID And r.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID).FirstOrDefault
                    obj.ADJUST_MONTH_TN = objDelareEntitlementNB.ADJUST_MONTH_TN
                    obj.ADJUST_MONTH_ENTITLEMENT = objDelareEntitlementNB.ADJUST_MONTH_ENTITLEMENT
                    obj.ADJUST_ENTITLEMENT = objDelareEntitlementNB.ADJUST_ENTITLEMENT
                    obj.START_MONTH_TN = objDelareEntitlementNB.START_MONTH_TN
                    obj.START_MONTH_EXTEND = objDelareEntitlementNB.START_MONTH_EXTEND
                    obj.YEAR = objDelareEntitlementNB.YEAR
                    obj.YEAR_NB = objDelareEntitlementNB.YEAR_NB
                    obj.YEAR_ENTITLEMENT = objDelareEntitlementNB.YEAR_ENTITLEMENT
                    obj.REMARK_TN = objDelareEntitlementNB.REMARK_TN
                    obj.REMARK_ENTITLEMENT = objDelareEntitlementNB.REMARK_ENTITLEMENT
                    obj.ADJUST_NB = objDelareEntitlementNB.ADJUST_NB
                    obj.START_MONTH_NB = objDelareEntitlementNB.START_MONTH_NB
                    obj.REMARK_NB = objDelareEntitlementNB.REMARK_NB
                    obj.MONTH_EXTENSION_NB = objDelareEntitlementNB.MONTH_EXTENSION_NB
                    obj.COM_PAY = objDelareEntitlementNB.COM_PAY
                    obj.ENT_PAY = objDelareEntitlementNB.ENT_PAY
                    obj.JOIN_DATE = objDelareEntitlementNB.JOIN_DATE
                Else
                    Dim objDelareEntitlementNBData As New AT_DECLARE_ENTITLEMENT
                    objDelareEntitlementNBData.ID = Utilities.GetNextSequence(Context, Context.AT_DECLARE_ENTITLEMENT.EntitySet.Name)
                    objDelareEntitlementNBData.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID
                    objDelareEntitlementNBData.ADJUST_MONTH_TN = objDelareEntitlementNB.ADJUST_MONTH_TN
                    objDelareEntitlementNBData.ADJUST_MONTH_ENTITLEMENT = objDelareEntitlementNB.ADJUST_MONTH_ENTITLEMENT
                    objDelareEntitlementNBData.ADJUST_ENTITLEMENT = objDelareEntitlementNB.ADJUST_ENTITLEMENT
                    objDelareEntitlementNBData.YEAR = objDelareEntitlementNB.YEAR
                    objDelareEntitlementNBData.YEAR_NB = objDelareEntitlementNB.YEAR_NB
                    objDelareEntitlementNBData.YEAR_ENTITLEMENT = objDelareEntitlementNB.YEAR_ENTITLEMENT
                    objDelareEntitlementNBData.START_MONTH_TN = objDelareEntitlementNB.START_MONTH_TN
                    objDelareEntitlementNBData.START_MONTH_EXTEND = objDelareEntitlementNB.START_MONTH_EXTEND
                    objDelareEntitlementNBData.REMARK_TN = objDelareEntitlementNB.REMARK_TN
                    objDelareEntitlementNBData.REMARK_ENTITLEMENT = objDelareEntitlementNB.REMARK_ENTITLEMENT
                    objDelareEntitlementNBData.ADJUST_NB = objDelareEntitlementNB.ADJUST_NB
                    objDelareEntitlementNBData.START_MONTH_NB = objDelareEntitlementNB.START_MONTH_NB
                    objDelareEntitlementNBData.REMARK_NB = objDelareEntitlementNB.REMARK_NB
                    objDelareEntitlementNBData.MONTH_EXTENSION_NB = objDelareEntitlementNB.MONTH_EXTENSION_NB
                    objDelareEntitlementNBData.COM_PAY = objDelareEntitlementNB.COM_PAY
                    objDelareEntitlementNBData.ENT_PAY = objDelareEntitlementNB.ENT_PAY
                    objDelareEntitlementNBData.JOIN_DATE = objDelareEntitlementNB.JOIN_DATE
                    Context.AT_DECLARE_ENTITLEMENT.AddObject(objDelareEntitlementNBData)
                End If
                Context.SaveChanges(log)
                Return True
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertMultipleDelareEntitlementNB(ByVal objDelareEntitlementlist As List(Of AT_DECLARE_ENTITLEMENTDTO), ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean
        Try
            Dim objData As New AT_DECLARE_ENTITLEMENTDTO

            For index = 0 To objDelareEntitlementlist.Count - 1
                objData = objDelareEntitlementlist(index)
                ' check nghỉ bù chỉ được gia hạn 1 lần trong năm
                If objDelareEntitlementNB.MONTH_EXTENSION_NB IsNot Nothing Then
                    checkMonthNB = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = objData.EMPLOYEE_ID And t.MONTH_EXTENSION_NB IsNot Nothing And t.YEAR = objDelareEntitlementNB.YEAR).Any
                End If
                ' check nghỉ phép chỉ được gia hạn 1 lần trong năm
                If objDelareEntitlementNB.START_MONTH_EXTEND IsNot Nothing Then
                    checkMonthNP = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = objData.EMPLOYEE_ID And t.START_MONTH_EXTEND IsNot Nothing And t.YEAR = objDelareEntitlementNB.YEAR).Any
                End If
            Next


            If checkMonthNB = False And checkMonthNP = False Then
                For index = 0 To objDelareEntitlementlist.Count - 1
                    objData = objDelareEntitlementlist(index)
                    Dim exists = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.ID = objDelareEntitlementNB.ID).Any
                    If exists Then
                        Dim obj = (From r In Context.AT_DECLARE_ENTITLEMENT Where r.ID = objDelareEntitlementNB.ID And r.EMPLOYEE_ID = objData.EMPLOYEE_ID).FirstOrDefault
                        obj.ADJUST_MONTH_TN = objDelareEntitlementNB.ADJUST_MONTH_TN
                        obj.ADJUST_MONTH_ENTITLEMENT = objDelareEntitlementNB.ADJUST_MONTH_ENTITLEMENT
                        obj.ADJUST_ENTITLEMENT = objDelareEntitlementNB.ADJUST_ENTITLEMENT
                        obj.START_MONTH_TN = objDelareEntitlementNB.START_MONTH_TN
                        obj.START_MONTH_EXTEND = objDelareEntitlementNB.START_MONTH_EXTEND
                        obj.YEAR = objDelareEntitlementNB.YEAR
                        obj.YEAR_NB = objDelareEntitlementNB.YEAR_NB
                        obj.YEAR_ENTITLEMENT = objDelareEntitlementNB.YEAR_ENTITLEMENT
                        obj.REMARK_TN = objDelareEntitlementNB.REMARK_TN
                        obj.REMARK_ENTITLEMENT = objDelareEntitlementNB.REMARK_ENTITLEMENT
                        obj.ADJUST_NB = objDelareEntitlementNB.ADJUST_NB
                        obj.START_MONTH_NB = objDelareEntitlementNB.START_MONTH_NB
                        obj.REMARK_NB = objDelareEntitlementNB.REMARK_NB
                        obj.MONTH_EXTENSION_NB = objDelareEntitlementNB.MONTH_EXTENSION_NB
                        obj.COM_PAY = objDelareEntitlementNB.COM_PAY
                        obj.ENT_PAY = objDelareEntitlementNB.ENT_PAY
                    Else
                        Dim objDelareEntitlementNBData As New AT_DECLARE_ENTITLEMENT
                        objDelareEntitlementNBData.ID = Utilities.GetNextSequence(Context, Context.AT_DECLARE_ENTITLEMENT.EntitySet.Name)
                        objDelareEntitlementNBData.EMPLOYEE_ID = objData.EMPLOYEE_ID
                        objDelareEntitlementNBData.ADJUST_MONTH_TN = objDelareEntitlementNB.ADJUST_MONTH_TN
                        objDelareEntitlementNBData.ADJUST_MONTH_ENTITLEMENT = objDelareEntitlementNB.ADJUST_MONTH_ENTITLEMENT
                        objDelareEntitlementNBData.ADJUST_ENTITLEMENT = objDelareEntitlementNB.ADJUST_ENTITLEMENT
                        objDelareEntitlementNBData.YEAR = objDelareEntitlementNB.YEAR
                        objDelareEntitlementNBData.YEAR_NB = objDelareEntitlementNB.YEAR_NB
                        objDelareEntitlementNBData.YEAR_ENTITLEMENT = objDelareEntitlementNB.YEAR_ENTITLEMENT
                        objDelareEntitlementNBData.START_MONTH_TN = objDelareEntitlementNB.START_MONTH_TN
                        objDelareEntitlementNBData.START_MONTH_EXTEND = objDelareEntitlementNB.START_MONTH_EXTEND
                        objDelareEntitlementNBData.REMARK_TN = objDelareEntitlementNB.REMARK_TN
                        objDelareEntitlementNBData.REMARK_ENTITLEMENT = objDelareEntitlementNB.REMARK_ENTITLEMENT
                        objDelareEntitlementNBData.ADJUST_NB = objDelareEntitlementNB.ADJUST_NB
                        objDelareEntitlementNBData.START_MONTH_NB = objDelareEntitlementNB.START_MONTH_NB
                        objDelareEntitlementNBData.REMARK_NB = objDelareEntitlementNB.REMARK_NB
                        objDelareEntitlementNBData.MONTH_EXTENSION_NB = objDelareEntitlementNB.MONTH_EXTENSION_NB
                        objDelareEntitlementNBData.COM_PAY = objDelareEntitlementNB.COM_PAY
                        objDelareEntitlementNBData.ENT_PAY = objDelareEntitlementNB.ENT_PAY
                        Context.AT_DECLARE_ENTITLEMENT.AddObject(objDelareEntitlementNBData)
                    End If
                    Context.SaveChanges(log)
                Next
                Return True
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ImportDelareEntitlementNB(ByVal dtData As DataTable, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean
        Try
            For Each row As DataRow In dtData.Rows
                Dim employee_id As New Decimal
                Dim year As New Decimal
                employee_id = Utilities.Obj2Decima(row("EMPLOYEE_ID"))
                year = Utilities.Obj2Decima(row("YEAR"))

                ' check nghỉ bù chỉ được gia hạn 1 lần trong năm
                If row("MONTH_EXTENSION_NB") IsNot Nothing Then
                    checkMonthNB = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = employee_id And t.MONTH_EXTENSION_NB IsNot Nothing And t.YEAR = year).Any
                End If
                ' check nghỉ phép chỉ được gia hạn 1 lần trong năm
                If row("START_MONTH_EXTEND") IsNot Nothing Then
                    checkMonthNP = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = employee_id And t.START_MONTH_EXTEND IsNot Nothing And t.YEAR = year).Any
                End If
            Next

            If checkMonthNB = False And checkMonthNP = False Then
                For Each row As DataRow In dtData.Rows
                    Dim objDelareEntitlementNBData As New AT_DECLARE_ENTITLEMENT
                    objDelareEntitlementNBData.ID = Utilities.GetNextSequence(Context, Context.AT_DECLARE_ENTITLEMENT.EntitySet.Name)
                    objDelareEntitlementNBData.EMPLOYEE_ID = Utilities.Obj2Decima(row("EMPLOYEE_ID"))
                    objDelareEntitlementNBData.ADJUST_MONTH_TN = Utilities.Obj2Decima(row("ADJUST_MONTH_TN"))
                    objDelareEntitlementNBData.ADJUST_MONTH_ENTITLEMENT = Utilities.Obj2Decima(row("ADJUST_MONTH_ENTITLEMENT"))
                    objDelareEntitlementNBData.ADJUST_ENTITLEMENT = Utilities.Obj2Decima(row("ADJUST_ENTITLEMENT"))
                    objDelareEntitlementNBData.YEAR = Utilities.Obj2Decima(row("YEAR"))
                    objDelareEntitlementNBData.YEAR_NB = Utilities.Obj2Decima(row("YEAR"))
                    objDelareEntitlementNBData.YEAR_ENTITLEMENT = Utilities.Obj2Decima(row("YEAR"))
                    objDelareEntitlementNBData.START_MONTH_TN = Utilities.Obj2Decima(row("START_MONTH_TN"))
                    objDelareEntitlementNBData.START_MONTH_EXTEND = Utilities.Obj2Decima(row("START_MONTH_EXTEND"))
                    objDelareEntitlementNBData.REMARK_TN = row("REMARK_TN")
                    objDelareEntitlementNBData.REMARK_ENTITLEMENT = Utilities.Obj2Decima(row("REMARK_ENTITLEMENT"))
                    objDelareEntitlementNBData.ADJUST_NB = Utilities.Obj2Decima(row("ADJUST_NB"))
                    objDelareEntitlementNBData.START_MONTH_NB = Utilities.Obj2Decima(row("START_MONTH_NB"))
                    objDelareEntitlementNBData.REMARK_NB = row("REMARK_NB")
                    objDelareEntitlementNBData.MONTH_EXTENSION_NB = Utilities.Obj2Decima(row("MONTH_EXTENSION_NB"))
                    objDelareEntitlementNBData.COM_PAY = Utilities.Obj2Decima(row("COM_PAY"))
                    objDelareEntitlementNBData.ENT_PAY = Utilities.Obj2Decima(row("ENT_PAY"))
                    Context.AT_DECLARE_ENTITLEMENT.AddObject(objDelareEntitlementNBData)
                    Context.SaveChanges(log)
                Next
                Return True
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyDelareEntitlementNB(ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objDelareEntitlementNBData As New AT_DECLARE_ENTITLEMENT With {.ID = objDelareEntitlementNB.ID}
        Try
            Dim exists = (From r In Context.AT_DECLARE_ENTITLEMENT Where r.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID).Any
            If exists Then
                Dim obj = (From r In Context.AT_DECLARE_ENTITLEMENT Where r.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID).FirstOrDefault
                obj.ADJUST_MONTH_TN = objDelareEntitlementNB.ADJUST_MONTH_TN
                obj.ADJUST_MONTH_ENTITLEMENT = objDelareEntitlementNB.ADJUST_MONTH_ENTITLEMENT
                obj.ADJUST_ENTITLEMENT = objDelareEntitlementNB.ADJUST_ENTITLEMENT
                obj.START_MONTH_TN = objDelareEntitlementNB.START_MONTH_TN
                obj.YEAR = objDelareEntitlementNB.YEAR
                obj.YEAR_NB = objDelareEntitlementNB.YEAR_NB
                obj.YEAR_ENTITLEMENT = objDelareEntitlementNBData.YEAR_ENTITLEMENT
                obj.START_MONTH_EXTEND = objDelareEntitlementNB.START_MONTH_EXTEND
                obj.REMARK_TN = objDelareEntitlementNB.REMARK_TN
                obj.REMARK_ENTITLEMENT = objDelareEntitlementNB.REMARK_ENTITLEMENT
                obj.MONTH_EXTENSION_NB = objDelareEntitlementNB.MONTH_EXTENSION_NB
                obj.COM_PAY = objDelareEntitlementNB.COM_PAY
                obj.ENT_PAY = objDelareEntitlementNB.ENT_PAY
                obj.JOIN_DATE = objDelareEntitlementNB.JOIN_DATE
            Else
                Return False
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeleteDelareEntitlementNB(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstl As List(Of AT_DECLARE_ENTITLEMENT)
        Try
            lstl = (From p In Context.AT_DECLARE_ENTITLEMENT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstl.Count - 1
                Context.AT_DECLARE_ENTITLEMENT.DeleteObject(lstl(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function
    Public Function DeleteOffTimeKeeping(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstl As List(Of AT_OFFSETTING_TIMEKEEPING)
        Dim lsttt As List(Of AT_OFFSETTING_TIMEKEEPING_EMP)
        Try
            lstl = (From p In Context.AT_OFFSETTING_TIMEKEEPING Where lstID.Contains(p.ID)).ToList
            lsttt = (From x In Context.AT_OFFSETTING_TIMEKEEPING_EMP Where lstID.Contains(x.GROUP_ID)).ToList
            For index = 0 To lstl.Count - 1
                Context.AT_OFFSETTING_TIMEKEEPING.DeleteObject(lstl(index))
            Next
            For index = 0 To lsttt.Count - 1
                Context.AT_OFFSETTING_TIMEKEEPING_EMP.DeleteObject(lsttt(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

    Public Function ValidateMonthThamNien(ByVal _validate As AT_DECLARE_ENTITLEMENTDTO)
        Dim query
        Try
            If _validate.START_MONTH_TN IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_DECLARE_ENTITLEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.START_MONTH_TN = _validate.START_MONTH_TN _
                             And p.YEAR = _validate.YEAR _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_DECLARE_ENTITLEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.START_MONTH_TN = _validate.START_MONTH_TN _
                             And p.YEAR = _validate.YEAR).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateMonthPhepNam(ByVal _validate As AT_DECLARE_ENTITLEMENTDTO)
        Dim query
        Try
            If _validate.ADJUST_MONTH_ENTITLEMENT IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_DECLARE_ENTITLEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.ADJUST_MONTH_ENTITLEMENT = _validate.ADJUST_MONTH_ENTITLEMENT _
                             And p.YEAR = _validate.YEAR _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_DECLARE_ENTITLEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.ADJUST_MONTH_ENTITLEMENT = _validate.ADJUST_MONTH_ENTITLEMENT _
                             And p.YEAR = _validate.YEAR).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateMonthNghiBu(ByVal _validate As AT_DECLARE_ENTITLEMENTDTO)
        Dim query
        Try
            If _validate.START_MONTH_NB IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_DECLARE_ENTITLEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.START_MONTH_NB = _validate.START_MONTH_NB _
                             And p.YEAR = _validate.YEAR _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_DECLARE_ENTITLEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.START_MONTH_NB = _validate.START_MONTH_NB _
                             And p.YEAR = _validate.YEAR).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "Bang tong hop công cơm"
    Public Function Cal_TimeTImesheet_Rice(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal p_period_id As Decimal?, ByVal P_ORG_ID As Decimal, ByVal lstEmployee As List(Of Decimal?)) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_RICE",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = P_ORG_ID,
                                                         .P_PERIOD_ID = p_period_id,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE})
                Return True
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetSummaryRice(ByVal param As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_SUMMARY_RICE",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_PAGE_INDEX = param.PAGE_INDEX,
                                                         .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                         .P_PAGE_SIZE = param.PAGE_SIZE,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_EMPLOYEE_NAME = param.VN_FULLNAME,
                                                         .P_ORG_NAME = param.ORG_NAME,
                                                         .P_TITLE_NAME = param.TITLE_NAME,
                                                         .P_STAFF_RANK_NAME = param.STAFF_RANK_NAME,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetTimeSheetRiceById(ByVal obj As AT_TIME_TIMESHEET_RICEDTO) As AT_TIME_TIMESHEET_RICEDTO
        Try
            Dim query = From p In Context.AT_TIME_TIMESHEET_RICE
                      From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                      From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                      From s In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                      From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                      Where p.ID = obj.ID
            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_RICEDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .PERIOD_ID = p.p.PERIOD_ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .STAFF_RANK_NAME = p.s.NAME,
                                       .nday_rice = p.p.NDAY_RICE,
                                       .total_rice = p.p.TOTAL_RICE,
                                       .total_rice_declare = p.p.TOTAL_RICE_DECLARE,
                                       .total_rice_price = p.p.TOTAL_RICE_PRICE,
                                       .rice_edit = p.p.RICE_EDIT,
                                       .ORG_ID = p.p.ORG_ID,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertLeaveSheetRice(ByVal objTimeSheetDaily As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim TimeSheetDaily = (From r In Context.AT_TIME_TIMESHEET_RICE Where r.EMPLOYEE_ID = objTimeSheetDaily.EMPLOYEE_ID And r.PERIOD_ID = objTimeSheetDaily.PERIOD_ID).FirstOrDefault

            If TimeSheetDaily IsNot Nothing Then
                TimeSheetDaily.RICE_EDIT = objTimeSheetDaily.rice_edit
                TimeSheetDaily.TOTAL_RICE = TimeSheetDaily.TOTAL_RICE_DECLARE + TimeSheetDaily.TOTAL_RICE_PRICE + objTimeSheetDaily.rice_edit
                Context.SaveChanges(log)
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyLeaveSheetRice(ByVal objLeave As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim TimeSheetDaily = (From r In Context.AT_TIME_TIMESHEET_RICE Where r.EMPLOYEE_ID = objLeave.EMPLOYEE_ID And r.PERIOD_ID = objLeave.PERIOD_ID).FirstOrDefault
            TimeSheetDaily.RICE_EDIT = objLeave.rice_edit
            TimeSheetDaily.TOTAL_RICE = TimeSheetDaily.TOTAL_RICE_DECLARE + TimeSheetDaily.TOTAL_RICE_PRICE + objLeave.rice_edit
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ApprovedTimeSheetRice(ByVal objLeave As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim TimeSheetDaily = (From r In Context.AT_TIME_TIMESHEET_RICE Where r.EMPLOYEE_ID = objLeave.EMPLOYEE_ID And r.PERIOD_ID = objLeave.PERIOD_ID).FirstOrDefault
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#Region "Đăng ký công"
    Public Function GetLeaveSheet(ByVal _filter As AT_LEAVESHEETDTO,
                                       ByVal _param As ParamDTO,
                                       Optional ByRef Total As Integer = 0,
                                       Optional ByVal PageIndex As Integer = 0,
                                       Optional ByVal PageSize As Integer = Integer.MaxValue,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LEAVESHEETDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_LEAVESHEET
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(F) F.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From m In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID).DefaultIfEmpty
                        From l1 In Context.AT_FML.Where(Function(f) f.ID = m.MORNING_ID).DefaultIfEmpty
                        From l2 In Context.AT_FML.Where(Function(f) f.ID = m.AFTERNOON_ID).DefaultIfEmpty
                        From en In Context.AT_ENTITLEMENT.Where(Function(F) F.EMPLOYEE_ID = p.EMPLOYEE_ID And p.WORKINGDAY.Value.Year = F.YEAR).DefaultIfEmpty
                        From nb In Context.AT_COMPENSATORY.Where(Function(F) F.EMPLOYEE_ID = p.EMPLOYEE_ID And F.YEAR = _filter.FROM_DATE.Value.Year).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
            If _filter.ISTEMINAL Then
                query = query.Where(Function(f) f.e.WORK_STATUS = 257)
                If _filter.WORKINGDAY.HasValue Then
                    query = query.Where(Function(f) f.e.TER_LAST_DATE <= _filter.WORKINGDAY)
                End If
            End If
            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY <= _filter.END_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.e.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.e.FULLNAME_VN.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                query = query.Where(Function(f) f.e.FULLNAME_VN.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                query = query.Where(Function(f) f.s.NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.BALANCE_NOW.HasValue Then
                query = query.Where(Function(f) f.p.BALANCE_NOW = _filter.BALANCE_NOW)
            End If
            If _filter.NGHIBUCONLAI.HasValue Then
                query = query.Where(Function(f) f.nb.CUR_HAVE = _filter.NGHIBUCONLAI)
            End If
            If _filter.LEAVE_FROM.HasValue Then
                query = query.Where(Function(f) f.p.LEAVE_FROM = _filter.LEAVE_FROM)
            End If
            If _filter.LEAVE_TO.HasValue Then
                query = query.Where(Function(f) f.p.LEAVE_TO = _filter.LEAVE_TO)
            End If
            If Not String.IsNullOrEmpty(_filter.MANUAL_NAME) Then
                query = query.Where(Function(f) f.m.NAME.ToLower().Contains(_filter.MANUAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MORNING_NAME) Then
                query = query.Where(Function(f) f.l1.NAME_VN.ToLower().Contains(_filter.MORNING_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.AFTERNOON_NAME) Then
                query = query.Where(Function(f) f.l2.NAME_VN.ToLower().Contains(_filter.AFTERNOON_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                query = query.Where(Function(f) f.p.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            Dim lst = query.Select(Function(p) New AT_LEAVESHEETDTO With {
                                                                       .ID = p.p.ID,
                                                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                                       .TITLE_NAME = p.t.NAME_VN,
                                                                       .STAFF_RANK_ID = p.e.STAFF_RANK_ID,
                                                                       .STAFF_RANK_NAME = p.s.NAME,
                                                                       .ORG_NAME = p.o.NAME_VN,
                                                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                                                       .ORG_ID = p.e.ORG_ID,
                                                                       .LEAVE_FROM = p.p.LEAVE_FROM,
                                                                       .LEAVE_TO = p.p.LEAVE_TO,
                                                                       .MANUAL_NAME = p.m.NAME,
                                                                       .MANUAL_ID = p.p.MANUAL_ID,
                                                                       .MORNING_ID = p.m.MORNING_ID,
                                                                       .AFTERNOON_ID = p.m.AFTERNOON_ID,
                                                                       .MORNING_NAME = p.l1.NAME_VN,
                                                                       .AFTERNOON_NAME = p.l2.NAME_VN,
                                                                       .BALANCE_NOW = If((p.m.MORNING_ID = 251 Or p.m.AFTERNOON_ID = 251), p.en.CUR_HAVE, Nothing),
                                                                       .NGHIBUCONLAI = If((p.m.MORNING_ID = 251 Or p.m.AFTERNOON_ID = 251), p.nb.CUR_HAVE, Nothing),
                                                                       .WORKINGDAY = p.p.WORKINGDAY,
                                                                       .NOTE = p.p.NOTE,
                                                                       .IS_WORKING_DAY = p.p.IS_WORKING_DAY,
                                                                       .IN_PLAN_DAYS = p.p.IN_PLAN_DAYS,
                                                                       .NOT_IN_PLAN_DAYS = p.p.NOT_IN_PLAN_DAYS,
                                                                       .DAY_NUM = p.p.DAY_NUM,
                                                                       .EMP_APPROVES_NAME = p.p.EMP_APPROVES_NAME,
                                                                       .NOTE_APP = p.p.NOTE_APP,
                                                                       .CREATED_BY = p.p.CREATED_BY,
                                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                                       .CREATED_LOG = p.p.CREATED_LOG,
                                                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})
            Dim sql = (From T In lst
                            Group T By
                              T.LEAVE_FROM,
                              T.LEAVE_TO,
                              T.EMPLOYEE_ID
                             Into g = Group
                            Select
                              ID = CType(g.Max(Function(p) p.ID), Decimal?),
                              EMPLOYEE_CODE = g.Max(Function(p) p.EMPLOYEE_CODE),
                              VN_FULLNAME = g.Max(Function(p) p.VN_FULLNAME),
                              TITLE_NAME = g.Max(Function(p) p.TITLE_NAME),
                              STAFF_RANK_NAME = g.Max(Function(p) p.STAFF_RANK_NAME),
                              STAFF_RANK_ID = g.Max(Function(p) p.STAFF_RANK_ID),
                              ORG_NAME = g.Max(Function(p) p.ORG_NAME),
                              ORG_DESC = g.Max(Function(p) p.ORG_DESC),
                              ORG_ID = CType(g.Max(Function(p) p.ORG_ID), Decimal?),
                              WORKINGDAY = CType(g.Max(Function(p) p.WORKINGDAY), DateTime?),
                              MANUAL_ID = g.Max(Function(p) p.MANUAL_ID),
                              MANUAL_NAME = g.Max(Function(p) p.MANUAL_NAME),
                              MORNING_ID = g.Max(Function(p) p.MORNING_ID),
                              AFTERNOON_ID = g.Max(Function(p) p.AFTERNOON_ID),
                              MORNING_NAME = g.Max(Function(p) p.MORNING_NAME),
                              AFTERNOON_NAME = g.Max(Function(p) p.AFTERNOON_NAME),
                              BALANCE_NOW = g.Max(Function(p) p.BALANCE_NOW),
                              NGHIBUCONLAI = g.Max(Function(p) p.NGHIBUCONLAI),
                              NOTE = g.Max(Function(p) p.NOTE),
                              IS_WORKING_DAY = g.Max(Function(p) If(p.IS_WORKING_DAY = False, 0, -1)),
                              IN_PLAN_DAYS = g.Max(Function(p) p.IN_PLAN_DAYS),
                              NOT_IN_PLAN_DAYS = g.Max(Function(p) p.NOT_IN_PLAN_DAYS),
                              DAY_NUM = g.Max(Function(p) p.DAY_NUM),
                              EMP_APPROVES_NAME = g.Max(Function(p) p.EMP_APPROVES_NAME),
                              CREATED_DATE = CType(g.Max(Function(p) p.CREATED_DATE), DateTime?),
                              CREATED_BY = g.Max(Function(p) p.CREATED_BY),
                              CREATED_LOG = g.Max(Function(p) p.CREATED_LOG),
                              MODIFIED_DATE = CType(g.Max(Function(p) p.MODIFIED_DATE), DateTime?),
                              MODIFIED_BY = g.Max(Function(p) p.MODIFIED_BY),
                              MODIFIED_LOG = g.Max(Function(p) p.MODIFIED_LOG),
                              EMPLOYEE_ID,
                              LEAVE_FROM,
                              LEAVE_TO,
                              NOTE_APP = g.Max(Function(p) p.NOTE_APP)).ToList

            Dim ls = sql.Select(Function(f) New AT_LEAVESHEETDTO With {
                                                                       .ID = f.ID,
                                                                       .EMPLOYEE_CODE = f.EMPLOYEE_CODE,
                                                                       .VN_FULLNAME = f.VN_FULLNAME,
                                                                       .EMPLOYEE_ID = f.EMPLOYEE_ID,
                                                                       .TITLE_NAME = f.TITLE_NAME,
                                                                       .STAFF_RANK_ID = f.STAFF_RANK_ID,
                                                                       .STAFF_RANK_NAME = f.STAFF_RANK_NAME,
                                                                       .ORG_NAME = f.ORG_NAME,
                                                                       .ORG_DESC = f.ORG_DESC,
                                                                       .ORG_ID = f.ORG_ID,
                                                                       .LEAVE_FROM = f.LEAVE_FROM,
                                                                       .LEAVE_TO = f.LEAVE_TO,
                                                                       .MANUAL_ID = f.MANUAL_ID,
                                                                       .MANUAL_NAME = f.MANUAL_NAME,
                                                                       .MORNING_ID = f.MORNING_ID,
                                                                       .AFTERNOON_ID = f.AFTERNOON_ID,
                                                                       .MORNING_NAME = f.MORNING_NAME,
                                                                       .AFTERNOON_NAME = f.AFTERNOON_NAME,
                                                                       .BALANCE_NOW = f.BALANCE_NOW,
                                                                       .NGHIBUCONLAI = f.NGHIBUCONLAI,
                                                                       .WORKINGDAY = f.WORKINGDAY,
                                                                       .NOTE = f.NOTE,
                                                                       .IS_WORKING_DAY = f.IS_WORKING_DAY,
                                                                       .IN_PLAN_DAYS = f.IN_PLAN_DAYS,
                                                                       .NOT_IN_PLAN_DAYS = f.NOT_IN_PLAN_DAYS,
                                                                       .DAY_NUM = f.DAY_NUM,
                                                                       .EMP_APPROVES_NAME = f.EMP_APPROVES_NAME,
                                                                       .CREATED_BY = f.CREATED_BY,
                                                                       .CREATED_DATE = f.CREATED_DATE,
                                                                       .CREATED_LOG = f.CREATED_LOG,
                                                                       .MODIFIED_BY = f.MODIFIED_BY,
                                                                       .MODIFIED_DATE = f.MODIFIED_DATE,
                                                                       .MODIFIED_LOG = f.MODIFIED_LOG,
                                                                       .NOTE_APP = f.NOTE_APP
                                                                        }).AsQueryable

            ls = ls.OrderBy(Sorts)
            Total = ls.Count
            ls = ls.Skip(PageIndex * PageSize).Take(PageSize)
            Return ls.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetPhepNam(ByVal _id As Decimal?, ByVal _year As Decimal?) As AT_ENTITLEMENTDTO
        Try

            Dim query = From p In Context.AT_ENTITLEMENT
                        Where p.EMPLOYEE_ID = _id And p.YEAR = _year
                        Order By p.ID Descending
            Dim lst = query.Select(Function(p) New AT_ENTITLEMENTDTO With {
                                       .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                       .CUR_HAVE = p.CUR_HAVE,
                                       .TOTAL_HAVE = p.TOTAL_HAVE,
                                       .CUR_REMAIN = p.CUR_HAVE}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetTotalDAY(ByVal P_EMPLOYEE_ID As Integer,
                                ByVal P_TYPE_MANUAL As Integer,
                                ByVal P_FROM_DATE As Date,
                                ByVal P_TO_DATE As Date) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CALCULATOR_DAY",
                                           New With {.P_FROM_DATE = P_FROM_DATE,
                                                     .P_TO_DATE = P_TO_DATE,
                                                     .p_EMPLOYEE_ID = P_EMPLOYEE_ID,
                                                     .p_TYPE_MANUAL = P_TYPE_MANUAL,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetCAL_DAY_LEAVE_OLD(ByVal P_EMPLOYEE_ID As Integer,
                                        ByVal P_FROM_DATE As Date,
                                        ByVal P_TO_DATE As Date) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_DAY_LEAVE_OLD",
                                           New With {.P_FROM_DATE = P_FROM_DATE,
                                                     .P_TO_DATE = P_TO_DATE,
                                                     .p_EMPLOYEE_ID = P_EMPLOYEE_ID,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetTotalPHEPNAM(ByVal P_EMPLOYEE_ID As Integer,
                                      ByVal P_YEAR As Integer,
                                      ByVal P_TYPE_LEAVE_ID As Integer) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_TOTAL_PHEPNAM",
                                           New With {.P_EMPLOYEE_ID = P_EMPLOYEE_ID,
                                                     .P_YEAR = P_YEAR,
                                                     .P_TYPE_LEAVE_ID = P_TYPE_LEAVE_ID,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetTotalPHEPBU(ByVal P_EMPLOYEE_ID As Integer,
                                    ByVal P_YEAR As Integer,
                                    ByVal P_TYPE_LEAVE_ID As Integer) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_TOTAL_PHEPBU",
                                           New With {.P_EMPLOYEE_ID = P_EMPLOYEE_ID,
                                                     .P_YEAR = P_YEAR,
                                                     .P_TYPE_LEAVE_ID = P_TYPE_LEAVE_ID,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetNghiBu(ByVal _id As Decimal?, ByVal _year As Decimal?) As AT_COMPENSATORYDTO
        Try

            Dim query = From p In Context.AT_COMPENSATORY
                        Where p.EMPLOYEE_ID = _id And p.YEAR = _year
            Dim lst = query.Select(Function(p) New AT_COMPENSATORYDTO With {
                                       .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                       .TOTAL_HAVE = p.TOTAL_HAVE,
                                       .CUR_HAVE = p.CUR_HAVE}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetPHEPBUCONLAI(ByVal lstEmpID As List(Of AT_LEAVESHEETDTO), ByVal _year As Decimal?) As List(Of AT_LEAVESHEETDTO)
        Dim objData As New List(Of AT_LEAVESHEETDTO)
        Try
            For index = 0 To lstEmpID.Count - 1
                Dim employeeID As Decimal = lstEmpID(index).EMPLOYEE_ID
                Dim query = From e In Context.HU_EMPLOYEE
                            From o In Context.HU_ORGANIZATION.Where(Function(F) F.ID = e.ORG_ID).DefaultIfEmpty
                            From t In Context.HU_TITLE.Where(Function(F) F.ID = e.TITLE_ID).DefaultIfEmpty
                            From p In Context.AT_ENTITLEMENT.Where(Function(F) F.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                            From b In Context.AT_COMPENSATORY.Where(Function(F) F.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                            Where p.EMPLOYEE_ID = employeeID And p.YEAR = _year
                If query IsNot Nothing Then
                    Dim lst = query.Select(Function(p) New AT_LEAVESHEETDTO With {
                                          .EMPLOYEE_ID = p.e.ID,
                                          .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                          .VN_FULLNAME = p.e.FULLNAME_VN,
                                          .TITLE_NAME = p.t.NAME_VN,
                                          .ORG_ID = p.e.ORG_ID,
                                          .ORG_NAME = p.o.NAME_VN,
                                          .BALANCE_NOW = p.p.CUR_HAVE,
                                          .NBCL = p.b.CUR_HAVE}).FirstOrDefault
                    If (lst IsNot Nothing) Then
                        objData.Add(lst)
                    End If
                End If
            Next

            Return objData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetLeaveById(ByVal _id As Decimal?) As AT_LEAVESHEETDTO
        Try

            Dim query = From p In Context.AT_LEAVESHEET
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From s In Context.HU_STAFF_RANK.Where(Function(F) F.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From m In Context.AT_TIME_MANUAL.Where(Function(F) F.ID = p.MANUAL_ID).DefaultIfEmpty
                        From en In Context.AT_ENTITLEMENT.Where(Function(F) F.EMPLOYEE_ID = p.EMPLOYEE_ID And p.WORKINGDAY.Value.Year = F.YEAR).DefaultIfEmpty()
                        From nb In Context.AT_COMPENSATORY.Where(Function(F) F.EMPLOYEE_ID = p.EMPLOYEE_ID And p.WORKINGDAY.Value.Year = F.YEAR).DefaultIfEmpty()
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_LEAVESHEETDTO With {
                                                                    .ID = p.p.ID,
                                                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                                       .TITLE_NAME = p.t.NAME_VN,
                                                                       .STAFF_RANK_ID = p.e.STAFF_RANK_ID,
                                                                       .STAFF_RANK_NAME = p.s.NAME,
                                                                       .ORG_NAME = p.o.NAME_VN,
                                                                       .ORG_ID = p.e.ORG_ID,
                                                                       .LEAVE_FROM = p.p.LEAVE_FROM,
                                                                       .LEAVE_TO = p.p.LEAVE_TO,
                                                                       .MANUAL_ID = p.p.MANUAL_ID,
                                                                       .MANUAL_NAME = p.m.NAME,
                                                                       .MORNING_ID = p.m.MORNING_ID,
                                                                       .AFTERNOON_ID = p.m.AFTERNOON_ID,
                                                                       .BALANCE_NOW = p.en.CUR_HAVE,
                                                                       .NBCL = p.nb.CUR_HAVE,
                                                                       .WORKINGDAY = p.p.WORKINGDAY,
                                                                       .NOTE = p.p.NOTE,
                                                                       .IS_WORKING_DAY = p.p.IS_WORKING_DAY,
                                                                       .DAY_NUM = p.p.DAY_NUM,
                                                                       .IN_PLAN_DAYS = p.p.IN_PLAN_DAYS,
                                                                       .NOT_IN_PLAN_DAYS = p.p.NOT_IN_PLAN_DAYS,
                                                                       .NOTE_APP = p.p.NOTE_APP,
                                                                       .CREATED_BY = p.p.CREATED_BY,
                                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                                       .CREATED_LOG = p.p.CREATED_LOG,
                                                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertLeaveSheet(ByVal objLeave As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim fromdate As Date?
        Try
            Dim exists = (From r In Context.AT_LEAVESHEET Where r.EMPLOYEE_ID = objLeave.EMPLOYEE_ID And r.WORKINGDAY <= objLeave.LEAVE_TO And r.WORKINGDAY >= objLeave.LEAVE_FROM).Any
            If exists Then ' trường hợp nếu đã có bản ghi trùng thì xóa đi và cập nhật bản ghi mới nhất
                Dim details = (From r In Context.AT_LEAVESHEET Where r.EMPLOYEE_ID = objLeave.EMPLOYEE_ID And r.WORKINGDAY <= objLeave.LEAVE_TO And r.WORKINGDAY >= objLeave.LEAVE_FROM).ToList
                For index = 0 To details.Count - 1
                    Context.AT_LEAVESHEET.DeleteObject(details(index))
                Next
                Dim objLeaveData As AT_LEAVESHEET
                'Dim emp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE.ToLower().Contains(objLeave.EMPLOYEE_CODE.ToLower())).FirstOrDefault
                fromdate = objLeave.LEAVE_FROM
                While objLeave.LEAVE_FROM <= objLeave.LEAVE_TO
                    If objLeave.LEAVE_FROM.Value.DayOfWeek = DayOfWeek.Sunday Then
                        objLeave.LEAVE_FROM = objLeave.LEAVE_FROM.Value.AddDays(1)
                        Continue While
                    End If
                    objLeaveData = New AT_LEAVESHEET
                    objLeaveData.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET.EntitySet.Name)
                    objLeaveData.WORKINGDAY = objLeave.LEAVE_FROM
                    Dim emp = (From e In Context.HU_EMPLOYEE
                               Where e.EMPLOYEE_CODE = objLeave.EMPLOYEE_CODE And e.JOIN_DATE <= objLeaveData.WORKINGDAY And
                                 (e.TER_EFFECT_DATE Is Nothing Or
                                  (e.TER_EFFECT_DATE IsNot Nothing And
                                   e.TER_EFFECT_DATE >= objLeaveData.WORKINGDAY))
                               Select e).FirstOrDefault

                    objLeaveData.EMPLOYEE_ID = emp.ID
                    objLeaveData.LEAVE_FROM = fromdate
                    objLeaveData.LEAVE_TO = objLeave.LEAVE_TO
                    objLeaveData.MANUAL_ID = objLeave.MANUAL_ID
                    objLeaveData.AFTERNOON_ID = objLeave.AFTERNOON_ID
                    objLeaveData.MORNING_ID = objLeave.MORNING_ID
                    objLeaveData.NOTE = objLeave.NOTE
                    objLeave.LEAVE_FROM = objLeave.LEAVE_FROM.Value.AddDays(1)
                    Context.AT_LEAVESHEET.AddObject(objLeaveData)
                End While
                Context.SaveChanges(log)
            Else
                Dim objLeaveData As AT_LEAVESHEET
                fromdate = objLeave.LEAVE_FROM
                While objLeave.LEAVE_FROM <= objLeave.LEAVE_TO
                    If objLeave.LEAVE_FROM.Value.DayOfWeek = DayOfWeek.Sunday Then
                        objLeave.LEAVE_FROM = objLeave.LEAVE_FROM.Value.AddDays(1)
                        Continue While
                    End If
                    objLeaveData = New AT_LEAVESHEET
                    objLeaveData.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET.EntitySet.Name)
                    objLeaveData.WORKINGDAY = objLeave.LEAVE_FROM
                    Dim emp = (From e In Context.HU_EMPLOYEE
                               Where e.EMPLOYEE_CODE = objLeave.EMPLOYEE_CODE And e.JOIN_DATE <= objLeaveData.WORKINGDAY And
                               (e.TER_EFFECT_DATE Is Nothing Or
                                (e.TER_EFFECT_DATE IsNot Nothing And
                                 e.TER_EFFECT_DATE >= objLeaveData.WORKINGDAY))
                               Select e).FirstOrDefault

                    If emp IsNot Nothing Then
                        objLeaveData.EMPLOYEE_ID = emp.ID
                        objLeaveData.LEAVE_FROM = fromdate
                        objLeaveData.LEAVE_TO = objLeave.LEAVE_TO
                        objLeaveData.MANUAL_ID = objLeave.MANUAL_ID
                        objLeaveData.AFTERNOON_ID = objLeave.AFTERNOON_ID
                        objLeaveData.MORNING_ID = objLeave.MORNING_ID
                        objLeaveData.NOTE = objLeave.NOTE
                        objLeave.LEAVE_FROM = objLeave.LEAVE_FROM.Value.AddDays(1)
                        Context.AT_LEAVESHEET.AddObject(objLeaveData)
                    Else
                        objLeave.LEAVE_FROM = objLeave.LEAVE_FROM.Value.AddDays(1)
                    End If

                End While
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertLeaveSheetList(ByVal objLeaveList As List(Of AT_LEAVESHEETDTO), ByVal objLeave As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim fromdate As Date?
        Dim fromdateNext As Date?
        Dim objDataList As New AT_LEAVESHEETDTO
        Try
            For iteam = 0 To objLeaveList.Count - 1
                objDataList = objLeaveList(iteam)

                Dim exists = (From r In Context.AT_LEAVESHEET Where r.EMPLOYEE_ID = objDataList.EMPLOYEE_ID And r.WORKINGDAY <= objLeave.LEAVE_TO And r.WORKINGDAY >= objLeave.LEAVE_FROM).Any
                If exists Then ' trường hợp nếu đã có bản ghi trùng thì xóa đi và cập nhật bản ghi mới nhất
                    Dim details = (From r In Context.AT_LEAVESHEET Where r.EMPLOYEE_ID = objDataList.EMPLOYEE_ID And r.WORKINGDAY <= objLeave.LEAVE_TO And r.WORKINGDAY >= objLeave.LEAVE_FROM).ToList
                    For index = 0 To details.Count - 1
                        Context.AT_LEAVESHEET.DeleteObject(details(index))
                    Next
                    Dim objLeaveData As AT_LEAVESHEET
                    'Dim emp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE.ToLower().Contains(objLeave.EMPLOYEE_CODE.ToLower())).FirstOrDefault
                    fromdate = objLeave.LEAVE_FROM
                    fromdateNext = objLeave.LEAVE_FROM
                    While fromdateNext <= objLeave.LEAVE_TO
                        objLeaveData = New AT_LEAVESHEET
                        objLeaveData.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET.EntitySet.Name)
                        objLeaveData.WORKINGDAY = fromdateNext
                        Dim emp = (From e In Context.HU_EMPLOYEE
                                   Where e.EMPLOYEE_CODE = objDataList.EMPLOYEE_CODE And e.JOIN_DATE <= objLeaveData.WORKINGDAY And
                                     (e.TER_EFFECT_DATE Is Nothing Or
                                      (e.TER_EFFECT_DATE IsNot Nothing And
                                       e.TER_EFFECT_DATE >= objLeaveData.WORKINGDAY))
                                   Select e).FirstOrDefault
                        If emp IsNot Nothing Then
                            objLeaveData.EMPLOYEE_ID = emp.ID
                            objLeaveData.LEAVE_FROM = fromdate
                            objLeaveData.LEAVE_TO = objLeave.LEAVE_TO
                            objLeaveData.MANUAL_ID = objLeave.MANUAL_ID
                            objLeaveData.AFTERNOON_ID = objLeave.AFTERNOON_ID
                            objLeaveData.MORNING_ID = objLeave.MORNING_ID
                            'objLeaveData.NOTE = objLeave.NOTE
                            objLeaveData.IS_WORKING_DAY = objLeave.IS_WORKING_DAY
                            objLeaveData.DAY_NUM = objLeave.DAY_NUM
                            objLeaveData.NOTE_APP = objLeave.NOTE_APP
                            fromdateNext = fromdateNext.Value.AddDays(1)
                            Context.AT_LEAVESHEET.AddObject(objLeaveData)
                        End If
                    End While
                    Context.SaveChanges(log)
                Else
                    Dim objLeaveData As AT_LEAVESHEET
                    fromdate = objLeave.LEAVE_FROM
                    fromdateNext = objLeave.LEAVE_FROM
                    While fromdateNext <= objLeave.LEAVE_TO
                        objLeaveData = New AT_LEAVESHEET
                        objLeaveData.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET.EntitySet.Name)
                        objLeaveData.WORKINGDAY = fromdateNext
                        Dim emp = (From e In Context.HU_EMPLOYEE
                                   Where e.EMPLOYEE_CODE = objDataList.EMPLOYEE_CODE And e.JOIN_DATE <= objLeaveData.WORKINGDAY And
                                   (e.TER_EFFECT_DATE Is Nothing Or
                                    (e.TER_EFFECT_DATE IsNot Nothing And
                                     e.TER_EFFECT_DATE >= objLeaveData.WORKINGDAY))
                                   Select e).FirstOrDefault
                        If emp IsNot Nothing Then
                            objLeaveData.EMPLOYEE_ID = emp.ID
                            objLeaveData.LEAVE_FROM = fromdate
                            objLeaveData.LEAVE_TO = objLeave.LEAVE_TO
                            objLeaveData.MANUAL_ID = objLeave.MANUAL_ID
                            objLeaveData.AFTERNOON_ID = objLeave.AFTERNOON_ID
                            objLeaveData.MORNING_ID = objLeave.MORNING_ID
                            objLeaveData.IS_WORKING_DAY = objLeave.IS_WORKING_DAY
                            objLeaveData.DAY_NUM = objLeave.DAY_NUM
                            'objLeaveData.NOTE = objLeave.NOTE
                            objLeaveData.NOTE_APP = objLeave.NOTE_APP
                            fromdateNext = fromdateNext.Value.AddDays(1)
                            Context.AT_LEAVESHEET.AddObject(objLeaveData)
                        Else
                            fromdateNext = fromdateNext.Value.AddDays(1)
                        End If

                    End While
                End If
                Context.SaveChanges(log)
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateLeaveSheet(ByVal _validate As AT_LEAVESHEETDTO)
        Dim query
        Try
            Dim dDay = _validate.LEAVE_FROM
            While dDay <= _validate.LEAVE_TO
                _validate.WORKINGDAY = dDay
                If _validate.WORKINGDAY.HasValue Then
                    If _validate.ID <> 0 Then
                        query = (From p In Context.AT_LEAVESHEET
                                 Where p.WORKINGDAY = _validate.WORKINGDAY And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID And p.ID <> _validate.ID).Any
                    Else
                        query = (From p In Context.AT_LEAVESHEET
                                 Where p.WORKINGDAY = _validate.WORKINGDAY And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID).Any
                    End If
                    If query Then
                        Return True
                    End If
                    dDay = dDay.Value.AddDays(1)
                End If
            End While
            Return False
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyLeaveSheet(ByVal objLeave As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim leave = (From r In Context.AT_LEAVESHEET Where r.ID = objLeave.ID).FirstOrDefault
            Dim details = (From r In Context.AT_LEAVESHEET Where r.LEAVE_FROM = leave.LEAVE_FROM And r.LEAVE_TO = leave.LEAVE_TO).ToList
            For index = 0 To details.Count - 1
                Context.AT_LEAVESHEET.DeleteObject(details(index))
            Next
            Dim fromdate As Date?
            Dim objLeaveData As AT_LEAVESHEET
            'Dim emp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE.ToLower().Contains(objLeave.EMPLOYEE_CODE.ToLower())).FirstOrDefault
            fromdate = objLeave.LEAVE_FROM
            While objLeave.LEAVE_FROM <= objLeave.LEAVE_TO
                objLeaveData = New AT_LEAVESHEET
                objLeaveData.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET.EntitySet.Name)
                objLeaveData.WORKINGDAY = objLeave.LEAVE_FROM
                Dim emp = (From e In Context.HU_EMPLOYEE
                           Where e.EMPLOYEE_CODE = objLeave.EMPLOYEE_CODE And e.JOIN_DATE <= objLeaveData.WORKINGDAY And
                             (e.TER_EFFECT_DATE Is Nothing Or
                              (e.TER_EFFECT_DATE IsNot Nothing And
                               e.TER_EFFECT_DATE >= objLeaveData.WORKINGDAY))
                           Select e).FirstOrDefault

                objLeaveData.EMPLOYEE_ID = emp.ID
                objLeaveData.LEAVE_FROM = fromdate
                objLeaveData.LEAVE_TO = objLeave.LEAVE_TO
                objLeaveData.MANUAL_ID = objLeave.MANUAL_ID
                objLeaveData.AFTERNOON_ID = objLeave.AFTERNOON_ID
                objLeaveData.MORNING_ID = objLeave.MORNING_ID
                'objLeaveData.NOTE = objLeave.NOTE
                objLeaveData.NOTE_APP = objLeave.NOTE_APP
                objLeaveData.IS_WORKING_DAY = objLeave.IS_WORKING_DAY
                objLeaveData.DAY_NUM = objLeave.DAY_NUM
                objLeave.LEAVE_FROM = objLeave.LEAVE_FROM.Value.AddDays(1)
                Context.AT_LEAVESHEET.AddObject(objLeaveData)
            End While
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function DeleteLeaveSheet(ByVal lstID As List(Of AT_LEAVESHEETDTO)) As Boolean
        Dim lstl As AT_LEAVESHEET
        Dim id As Decimal = 0
        Try

            For index = 0 To lstID.Count - 1
                id = lstID(index).ID
                lstl = (From p In Context.AT_LEAVESHEET Where id = p.ID).FirstOrDefault
                If Not lstl Is Nothing Then
                    Dim details = (From r In Context.AT_LEAVESHEET Where r.LEAVE_FROM = lstl.LEAVE_FROM And r.LEAVE_TO = lstl.LEAVE_TO).ToList
                    If Not details Is Nothing Then
                        For index1 = 0 To details.Count - 1
                            Context.AT_LEAVESHEET.DeleteObject(details(index1))
                        Next
                    End If
                End If
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteLeaveOT")
            Throw ex
        End Try
    End Function

    Public Function checkLeaveImport(ByVal dtData As DataTable) As DataTable
        Dim dtDataError As DataTable = dtData.Clone
        Dim dtDataSave As New DataTable
        Dim dtDataUserPHEPNAM As New DataTable
        Dim dtDataUserPHEPBU As New DataTable
        Dim totalPhep As Object
        Dim totalBu As Decimal = 0
        Dim rowError As DataRow
        Dim rowDataSave As DataRow
        Dim isError As Boolean = False
        Dim at_entilement As New AT_ENTITLEMENTDTO
        Dim at_compensatory As New AT_COMPENSATORYDTO
        Dim employee_id As Decimal?
        Dim org_id As Decimal?
        Dim irow = 8
        Try
            dtDataSave.Columns.Add("EMPLOYEE_CODE")
            dtDataSave.Columns.Add("MANUAL_ID")
            dtDataSave.Columns.Add("MORNING_ID")
            dtDataSave.Columns.Add("AFTERNOON_ID")
            dtDataSave.Columns.Add("TOTAL_DAY_ENT", GetType(Decimal)) ' tổng ngày nghỉ phép
            dtDataSave.Columns.Add("TOTAL_DAY_COM", GetType(Decimal)) ' tổng ngày nghỉ bù

            For Each row As DataRow In dtData.Rows
                Dim empCode As String = row("EMPLOYEE_CODE")
                Dim fromDate As Date
                DateTime.TryParseExact(row("LEAVE_FROM"), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, fromDate)
                Dim toDate As Date
                DateTime.TryParseExact(row("LEAVE_TO"), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, toDate)
                'Dim toDate As Date = Date.Parse(row("LEAVE_TO"))
                Dim manualId As Decimal = Utilities.Obj2Decima(row("MANUAL_ID"))
                Dim monningId As Decimal = Utilities.Obj2Decima(row("MORNING_ID"))
                Dim afternoonId As Decimal = Utilities.Obj2Decima(row("AFTERNOON_ID"))
                Dim emp = (From e In Context.HU_EMPLOYEE
                           Where e.EMPLOYEE_CODE = empCode And e.JOIN_DATE <= fromDate And
                               (e.TER_EFFECT_DATE Is Nothing Or
                                (e.TER_EFFECT_DATE IsNot Nothing And
                                 e.TER_EFFECT_DATE >= fromDate))
                           Select e).FirstOrDefault
                If emp IsNot Nothing Then
                    employee_id = emp.ID
                    org_id = emp.ORG_ID
                Else
                    Exit For
                End If

                ' tính tổng số ngày nghỉ của 1 nv trừ thứ 7 và chủ nhật.
                rowDataSave = dtDataSave.NewRow
                rowDataSave("EMPLOYEE_CODE") = empCode
                rowDataSave("MANUAL_ID") = manualId
                rowDataSave("MORNING_ID") = monningId
                rowDataSave("AFTERNOON_ID") = afternoonId
                rowDataSave("TOTAL_DAY_ENT") = If(monningId = afternoonId, GetTotalDAY(employee_id, 251, fromDate, toDate).Rows(0)(0).ToString, Utilities.Obj2Decima(GetTotalDAY(employee_id, 251, fromDate, toDate).Rows(0)(0) / 2))
                rowDataSave("TOTAL_DAY_COM") = If(monningId = afternoonId, GetTotalDAY(employee_id, 255, fromDate, toDate).Rows(0)(0).ToString, Utilities.Obj2Decima(GetTotalDAY(employee_id, 255, fromDate, toDate).Rows(0)(0) / 2))
                dtDataSave.Rows.Add(rowDataSave)


                ' khởi tạo dòng trong datatable lỗi.
                rowError = dtDataError.NewRow
                '1. lấy phép năm đã đăng ký.
                dtDataUserPHEPNAM = GetTotalPHEPNAM(employee_id, fromDate.Year, Utilities.Obj2Decima(row("MANUAL_ID")))
                '2. lấy phép năm được phép nghỉ trong năm.
                at_entilement = GetPhepNam(employee_id, fromDate.Year)
                '3 phép bù được phép nghỉ trong năm.
                at_compensatory = GetNghiBu(employee_id, fromDate.Year)
                '4. tổng số ngày đăng ký của nhân viên trên file import.
                'totalDayRes = GetTotalDAY(employee_id, 251, Date.Parse(row("LEAVE_FROM")), Date.Parse(row("LEAVE_TO")))
                totalPhep = dtDataSave.Compute("SUM(TOTAL_DAY_ENT)", "EMPLOYEE_CODE = " & empCode & " AND (MORNING_ID = 251 OR AFTERNOON_ID = 251)")

                ' nếu là kiểu đăng ký nghỉ phép
                If Utilities.Obj2Decima(row("MORNING_ID")) = 251 Or Utilities.Obj2Decima(row("AFTERNOON_ID")) = 251 Then
                    If dtDataUserPHEPNAM IsNot Nothing And at_entilement IsNot Nothing Then
                        If at_entilement.TOTAL_HAVE.Value - (dtDataUserPHEPNAM.Rows(0)(0) + totalPhep) < -3 Then
                            rowError("MANUAL_NAME") = "Tổng số ngày nghỉ phép của bạn trong năm nay đã vượt quá mức cho phép."
                            isError = True
                        End If
                    End If
                    'ElseIf Utilities.Obj2Decima(row("MORNING_ID")) = 251 Or Utilities.Obj2Decima(row("AFTERNOON_ID")) = 251 Then
                    '    If dtDataUserPHEPNAM IsNot Nothing And at_entilement IsNot Nothing Then
                    '        If at_entilement.TOTAL_HAVE.Value - (dtDataUserPHEPNAM.Rows(0)(0) + totalPhep / 2) < -3 Then
                    '            rowError("MANUAL_NAME") = "Tổng số ngày nghỉ phép của bạn trong năm nay đã vượt quá mức cho phép."
                    '            isError = True
                    '        End If
                    '    End If
                End If
                ' nếu là kiểu đăng ký nghỉ bù
                dtDataUserPHEPBU = GetTotalPHEPBU(employee_id, fromDate.Year, Utilities.Obj2Decima(row("MANUAL_ID")))
                'totalDayRes = GetTotalDAY(employee_id, 255, Date.Parse(row("LEAVE_FROM")), Date.Parse(row("LEAVE_TO")))
                totalBu = Utilities.Obj2Decima(dtDataSave.Compute("SUM(TOTAL_DAY_COM)", "EMPLOYEE_CODE = " & empCode & " AND (MORNING_ID = 255 OR AFTERNOON_ID = 255)"))

                If Utilities.Obj2Decima(row("MORNING_ID")) = 255 Or Utilities.Obj2Decima(row("AFTERNOON_ID")) = 255 Then
                    If dtDataUserPHEPBU IsNot Nothing And at_compensatory IsNot Nothing Then
                        If at_compensatory.TOTAL_HAVE.Value - (dtDataUserPHEPBU.Rows(0)(0) + totalBu) < 0 Then
                            rowError("MANUAL_NAME") = "Tổng số ngày nghỉ bù của bạn đã vượt quá mức cho phép."
                            isError = True
                        End If
                    End If
                    'ElseIf Utilities.Obj2Decima(row("MORNING_ID")) = 255 Or Utilities.Obj2Decima(row("AFTERNOON_ID")) = 255 Then
                    '    If dtDataUserPHEPBU IsNot Nothing And at_compensatory IsNot Nothing Then
                    '        If at_compensatory.TOTAL_HAVE.Value - (dtDataUserPHEPBU.Rows(0)(0) + totalBu / 2) < 0 Then
                    '            rowError("MANUAL_NAME") = "Tổng số ngày nghỉ bù của bạn đã vượt quá mức cho phép."
                    '            isError = True
                    '        End If
                    '    End If
                End If
                If isError Then
                    rowError("STT") = irow
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    rowError("VN_FULLNAME") = row("VN_FULLNAME").ToString
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    rowError("ORG_PATH") = row("ORG_PATH").ToString
                    rowError("TITLE_NAME") = row("TITLE_NAME").ToString
                    dtDataError.Rows.Add(rowError)
                End If
                irow = irow + 1
                isError = False
            Next
            Return dtDataError
        Catch ex As Exception
            Throw ex
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
        End Try
    End Function

#End Region

#Region "NGHI BU"
    ''' <summary>
    ''' Tổng hợp nghỉ bù
    ''' </summary>
    ''' <param name="param"></param>
    ''' <param name="listEmployeeId"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CALCULATE_ENTITLEMENT_NB(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As UserLog) As Boolean
        Try
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = param.PERIOD_ID
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CALL_ENTITLEMENT_NB",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_PERIOD = param.PERIOD_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE})
            End Using
            LOG_AT(param, log, listEmployeeId, "TỔNG HỢP NGHỈ BÙ", obj, param.ORG_ID)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function
    ''' <summary>
    ''' Lấy thông tin nghỉ bù theo params
    ''' </summary>
    ''' <param name="_filter"></param>
    ''' <param name="_param"></param>
    ''' <param name="Total"></param>
    ''' <param name="PageIndex"></param>
    ''' <param name="PageSize"></param>
    ''' <param name="Sorts"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNB(ByVal _filter As AT_COMPENSATORYDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_COMPENSATORYDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim per = (From p In Context.AT_PERIOD Where p.ID = _filter.PERIOD_ID).FirstOrDefault
            Dim thang = Month(per.END_DATE)

            Dim query = From ot In Context.AT_COMPENSATORY
                        From period In Context.AT_PERIOD.Where(Function(f) f.ID = ot.PERIOD_ID).DefaultIfEmpty
                        From E In Context.HU_EMPLOYEE.Where(Function(f) f.ID = ot.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = E.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = E.TITLE_ID).DefaultIfEmpty()
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = E.STAFF_RANK_ID).DefaultIfEmpty()
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) E.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where ot.YEAR = _filter.YEAR

            Dim dateStart = per.END_DATE
            If Not _filter.ISTEMINAL Then
                query = query.Where(Function(f) f.E.WORK_STATUS Is Nothing Or f.E.WORK_STATUS <> 257 Or
                                    (f.E.WORK_STATUS = 257 And f.E.TER_LAST_DATE >= dateStart))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.E.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.FULLNAME_VN) Then
                query = query.Where(Function(f) f.E.FULLNAME_VN.ToLower().Contains(_filter.FULLNAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME_VN) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                query = query.Where(Function(f) f.c.NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PERIOD_NAME) Then
                query = query.Where(Function(f) f.period.PERIOD_NAME.ToLower().Contains(_filter.PERIOD_NAME.ToLower()))
            End If
            If _filter.CUR_USED1.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED1 = _filter.CUR_USED1)
            End If
            If _filter.CUR_USED2.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED2 = _filter.CUR_USED2)
            End If
            If _filter.CUR_USED3.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED3 = _filter.CUR_USED3)
            End If
            If _filter.CUR_USED4.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED4 = _filter.CUR_USED4)
            End If
            If _filter.CUR_USED5.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED5 = _filter.CUR_USED5)
            End If
            If _filter.CUR_USED6.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED6 = _filter.CUR_USED6)
            End If
            If _filter.CUR_USED7.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED7 = _filter.CUR_USED7)
            End If
            If _filter.CUR_USED8.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED8 = _filter.CUR_USED8)
            End If
            If _filter.CUR_USED9.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED9 = _filter.CUR_USED9)
            End If
            If _filter.CUR_USED10.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED10 = _filter.CUR_USED10)
            End If
            If _filter.CUR_USED11.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED11 = _filter.CUR_USED11)
            End If
            If _filter.CUR_USED12.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED12 = _filter.CUR_USED12)
            End If
            If _filter.CUR_HAVE.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_HAVE = _filter.CUR_HAVE)
            End If
            If _filter.AL_T1.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T1 = _filter.AL_T1)
            End If
            If _filter.AL_T2.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T2 = _filter.AL_T2)
            End If
            If _filter.AL_T3.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T3 = _filter.AL_T3)
            End If
            If _filter.AL_T4.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T4 = _filter.AL_T4)
            End If
            If _filter.AL_T5.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T5 = _filter.AL_T5)
            End If
            If _filter.AL_T6.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T6 = _filter.AL_T6)
            End If
            If _filter.AL_T7.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T7 = _filter.AL_T7)
            End If
            If _filter.AL_T8.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T8 = _filter.AL_T8)
            End If
            If _filter.AL_T9.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T9 = _filter.AL_T9)
            End If
            If _filter.AL_T10.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T10 = _filter.AL_T10)
            End If
            If _filter.AL_T11.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T11 = _filter.AL_T11)
            End If
            If _filter.AL_T12.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T12 = _filter.AL_T12)
            End If
            If _filter.CUR_HAVE.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_HAVE = _filter.CUR_HAVE)
            End If
            If _filter.CUR_USED.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED = _filter.CUR_USED)
            End If
            If _filter.TOTAL_CUR_HAVE.HasValue Then
                query = query.Where(Function(f) f.ot.TOTAL_HAVE = _filter.TOTAL_CUR_HAVE)
            End If

            Dim lst = query.Select(Function(p) New AT_COMPENSATORYDTO With {
                                       .ID = p.ot.ID,
                                       .EMPLOYEE_ID = p.ot.EMPLOYEE_ID,
                                       .FULLNAME_VN = p.E.FULLNAME_VN,
                                       .EMPLOYEE_CODE = p.E.EMPLOYEE_CODE,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .TITLE_NAME_VN = p.t.NAME_VN,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .PERIOD_NAME = p.period.PERIOD_NAME,
                                       .PREV_HAVE = p.ot.PREV_HAVE,
                                       .AL_T1 = p.ot.AL_T1,
                                       .AL_T2 = If(thang >= 2, p.ot.AL_T2, 0),
                                       .AL_T3 = If(thang >= 3, p.ot.AL_T3, 0),
                                       .AL_T4 = If(thang >= 4, p.ot.AL_T4, 0),
                                       .AL_T5 = If(thang >= 5, p.ot.AL_T5, 0),
                                       .AL_T6 = If(thang >= 6, p.ot.AL_T6, 0),
                                       .AL_T7 = If(thang >= 7, p.ot.AL_T7, 0),
                                       .AL_T8 = If(thang >= 8, p.ot.AL_T8, 0),
                                       .AL_T9 = If(thang >= 9, p.ot.AL_T9, 0),
                                       .AL_T10 = If(thang >= 10, p.ot.AL_T10, 0),
                                       .AL_T11 = If(thang >= 11, p.ot.AL_T11, 0),
                                       .AL_T12 = If(thang >= 12, p.ot.AL_T12, 0),
                                       .CUR_USED1 = p.ot.CUR_USED1,
                                       .CUR_USED2 = If(thang >= 2, p.ot.CUR_USED2, 0),
                                       .CUR_USED3 = If(thang >= 3, p.ot.CUR_USED3, 0),
                                       .CUR_USED4 = If(thang >= 4, p.ot.CUR_USED4, 0),
                                       .CUR_USED5 = If(thang >= 5, p.ot.CUR_USED5, 0),
                                       .CUR_USED6 = If(thang >= 6, p.ot.CUR_USED6, 0),
                                       .CUR_USED7 = If(thang >= 7, p.ot.CUR_USED7, 0),
                                       .CUR_USED8 = If(thang >= 8, p.ot.CUR_USED8, 0),
                                       .CUR_USED9 = If(thang >= 9, p.ot.CUR_USED9, 0),
                                       .CUR_USED10 = If(thang >= 10, p.ot.CUR_USED10, 0),
                                       .CUR_USED11 = If(thang >= 11, p.ot.CUR_USED11, 0),
                                       .CUR_USED12 = If(thang >= 12, p.ot.CUR_USED12, 0),
                                       .CUR_HAVE1 = p.ot.CUR_HAVE1,
                                       .CUR_HAVE2 = p.ot.CUR_HAVE2,
                                       .CUR_HAVE3 = p.ot.CUR_HAVE3,
                                       .CUR_HAVE4 = p.ot.CUR_HAVE4,
                                       .CUR_HAVE5 = p.ot.CUR_HAVE5,
                                       .CUR_HAVE6 = p.ot.CUR_HAVE6,
                                       .CUR_HAVE7 = p.ot.CUR_HAVE7,
                                       .CUR_HAVE8 = p.ot.CUR_HAVE8,
                                       .CUR_HAVE9 = p.ot.CUR_HAVE9,
                                       .CUR_HAVE10 = p.ot.CUR_HAVE10,
                                       .CUR_HAVE11 = p.ot.CUR_HAVE11,
                                       .CUR_HAVE12 = p.ot.CUR_HAVE12,
                                       .CUR_HAVE = p.ot.CUR_HAVE,
                                       .CUR_USED = p.ot.CUR_USED,
                                       .TOTAL_CUR_HAVE = p.ot.TOTAL_HAVE,
                                       .CREATED_DATE = p.ot.CREATED_DATE})

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Dim lstResult = lst.ToList
            For Each item As AT_COMPENSATORYDTO In lstResult
                'item.TOTAL_CUR_HAVE = Cal_TOTAL_CUR_HAVE(thang, item)
                'item.CUR_USED = item.CUR_USED1 + item.CUR_USED2 + item.CUR_USED3 + item.CUR_USED4 + item.CUR_USED5 + item.CUR_USED6 _
                '                + item.CUR_USED7 + item.CUR_USED8 + item.CUR_USED9 + item.CUR_USED10 + item.CUR_USED11 + item.CUR_USED12
                'item.CUR_HAVE = Cal_CUR_HAVE(thang, item)

                item.TOTAL_CUR_HAVE = If(item.TOTAL_CUR_HAVE = 0, Nothing, item.TOTAL_CUR_HAVE)
                item.CUR_USED = If(item.CUR_USED = 0, Nothing, item.CUR_USED)
                item.CUR_HAVE = If(item.CUR_HAVE = 0, Nothing, item.CUR_HAVE)

                item.AL_T1 = If(item.AL_T1 = 0, Nothing, item.AL_T1)
                item.AL_T2 = If(item.AL_T2 = 0, Nothing, item.AL_T2)
                item.AL_T3 = If(item.AL_T3 = 0, Nothing, item.AL_T3)
                item.AL_T4 = If(item.AL_T4 = 0, Nothing, item.AL_T4)
                item.AL_T5 = If(item.AL_T5 = 0, Nothing, item.AL_T5)
                item.AL_T6 = If(item.AL_T6 = 0, Nothing, item.AL_T6)
                item.AL_T7 = If(item.AL_T7 = 0, Nothing, item.AL_T7)
                item.AL_T8 = If(item.AL_T8 = 0, Nothing, item.AL_T8)
                item.AL_T9 = If(item.AL_T9 = 0, Nothing, item.AL_T9)
                item.AL_T10 = If(item.AL_T10 = 0, Nothing, item.AL_T10)
                item.AL_T11 = If(item.AL_T11 = 0, Nothing, item.AL_T11)
                item.AL_T12 = If(item.AL_T12 = 0, Nothing, item.AL_T12)

                item.CUR_USED1 = If(item.CUR_USED1 = 0, Nothing, item.CUR_USED1)
                item.CUR_USED2 = If(item.CUR_USED2 = 0, Nothing, item.CUR_USED2)
                item.CUR_USED3 = If(item.CUR_USED3 = 0, Nothing, item.CUR_USED3)
                item.CUR_USED4 = If(item.CUR_USED4 = 0, Nothing, item.CUR_USED4)
                item.CUR_USED5 = If(item.CUR_USED5 = 0, Nothing, item.CUR_USED5)
                item.CUR_USED6 = If(item.CUR_USED6 = 0, Nothing, item.CUR_USED6)
                item.CUR_USED7 = If(item.CUR_USED7 = 0, Nothing, item.CUR_USED7)
                item.CUR_USED8 = If(item.CUR_USED8 = 0, Nothing, item.CUR_USED8)
                item.CUR_USED9 = If(item.CUR_USED9 = 0, Nothing, item.CUR_USED9)
                item.CUR_USED10 = If(item.CUR_USED10 = 0, Nothing, item.CUR_USED10)
                item.CUR_USED11 = If(item.CUR_USED11 = 0, Nothing, item.CUR_USED11)
                item.CUR_USED12 = If(item.CUR_USED12 = 0, Nothing, item.CUR_USED12)
            Next

            Return lstResult
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function Cal_TOTAL_CUR_HAVE(ByVal thang As Int32, ByRef item As AT_COMPENSATORYDTO) As Decimal
        Select Case thang
            Case 1
                Return item.PREV_HAVE + item.AL_T1
            Case 2
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2
            Case 3
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2 - item.CUR_USED2 + item.AL_T3
            Case 4
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2 - item.CUR_USED2 + item.AL_T3 - item.CUR_USED3 + item.AL_T4
            Case 5
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2 - item.CUR_USED2 + item.AL_T3 - item.CUR_USED3 + item.AL_T4 - item.CUR_USED4 + item.AL_T5
            Case 6
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2 - item.CUR_USED2 + item.AL_T3 - item.CUR_USED3 + item.AL_T4 _
                        - item.CUR_USED4 + item.AL_T5 - item.CUR_USED5 + item.AL_T6
            Case 7
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2 - item.CUR_USED2 + item.AL_T3 - item.CUR_USED3 + item.AL_T4 _
                        - item.CUR_USED4 + item.AL_T5 - item.CUR_USED5 + item.AL_T6 - item.CUR_USED6 + item.AL_T7
            Case 8
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2 - item.CUR_USED2 + item.AL_T3 - item.CUR_USED3 + item.AL_T4 _
                        - item.CUR_USED4 + item.AL_T5 - item.CUR_USED5 + item.AL_T6 - item.CUR_USED6 + item.AL_T7 - item.CUR_USED7 + item.AL_T8
            Case 9
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2 - item.CUR_USED2 + item.AL_T3 - item.CUR_USED3 + item.AL_T4 _
                        - item.CUR_USED4 + item.AL_T5 - item.CUR_USED5 + item.AL_T6 - item.CUR_USED6 + item.AL_T7 - item.CUR_USED7 + item.AL_T8 _
                        - item.CUR_USED8 + item.AL_T9
            Case 10
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2 - item.CUR_USED2 + item.AL_T3 - item.CUR_USED3 + item.AL_T4 _
                        - item.CUR_USED4 + item.AL_T5 - item.CUR_USED5 + item.AL_T6 - item.CUR_USED6 + item.AL_T7 - item.CUR_USED7 + item.AL_T8 _
                        - item.CUR_USED8 + item.AL_T9 - item.CUR_USED9 + item.AL_T10
            Case 11
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2 - item.CUR_USED2 + item.AL_T3 - item.CUR_USED3 + item.AL_T4 _
                        - item.CUR_USED4 + item.AL_T5 - item.CUR_USED5 + item.AL_T6 - item.CUR_USED6 + item.AL_T7 - item.CUR_USED7 + item.AL_T8 _
                        - item.CUR_USED8 + item.AL_T9 - item.CUR_USED9 + item.AL_T10 - item.CUR_USED10 + item.AL_T11
            Case 12
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2 - item.CUR_USED2 + item.AL_T3 - item.CUR_USED3 + item.AL_T4 _
                        - item.CUR_USED4 + item.AL_T5 - item.CUR_USED5 + item.AL_T6 - item.CUR_USED6 + item.AL_T7 - item.CUR_USED7 + item.AL_T8 _
                        - item.CUR_USED8 + item.AL_T9 - item.CUR_USED9 + item.AL_T10 - item.CUR_USED10 + item.AL_T11 - item.CUR_USED11 + item.AL_T12
            Case Else
                Return 0
        End Select
    End Function

    Public Function Cal_CUR_HAVE(ByVal thang As Int32, ByRef item As AT_COMPENSATORYDTO) As Decimal
        Select Case thang
            Case 1
                Return item.TOTAL_CUR_HAVE - item.CUR_USED1
            Case 2
                Return item.TOTAL_CUR_HAVE - item.CUR_USED2
            Case 3
                Return item.TOTAL_CUR_HAVE - item.CUR_USED3
            Case 4
                Return item.TOTAL_CUR_HAVE - item.CUR_USED4
            Case 5
                Return item.TOTAL_CUR_HAVE - item.CUR_USED5
            Case 6
                Return item.TOTAL_CUR_HAVE - item.CUR_USED6
            Case 7
                Return item.TOTAL_CUR_HAVE - item.CUR_USED7
            Case 8
                Return item.TOTAL_CUR_HAVE - item.CUR_USED8
            Case 9
                Return item.TOTAL_CUR_HAVE - item.CUR_USED9
            Case 10
                Return item.TOTAL_CUR_HAVE - item.CUR_USED10
            Case 11
                Return item.TOTAL_CUR_HAVE - item.CUR_USED11
            Case 12
                Return item.TOTAL_CUR_HAVE - item.CUR_USED12
            Case Else
                Return 0
        End Select
    End Function
#End Region

#Region "Quan ly vao ra"
    Public Function GetDataInout(ByVal _filter As AT_DATAINOUTDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "EMPLOYEE_CODE, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_DATAINOUTDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_DATA_INOUT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From s In Context.HU_STAFF_RANK.Where(Function(f) e.STAFF_RANK_ID = f.ID).DefaultIfEmpty

            If _filter.IS_TERMINATE Then
                query = query.Where(Function(f) f.e.WORK_STATUS = 257)
                If _filter.END_DATE.HasValue Then
                    query = query.Where(Function(f) f.e.TER_LAST_DATE <= _filter.END_DATE)
                End If
            End If
            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY <= _filter.END_DATE)
            End If
            If _filter.WORKINGDAY.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY = _filter.WORKINGDAY)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.e.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                query = query.Where(Function(f) f.e.FULLNAME_VN.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                query = query.Where(Function(f) f.s.NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            Dim lst = query.Select(Function(p) New AT_DATAINOUTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .STAFF_RANK_ID = p.e.STAFF_RANK_ID,
                                       .STAFF_RANK_NAME = p.s.NAME,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .VALIN1 = p.p.VALIN1,
                                       .VALIN2 = p.p.VALIN2,
                                       .VALIN3 = p.p.VALIN3,
                                       .VALIN4 = p.p.VALIN4,
                                       .VALIN5 = p.p.VALIN5,
                                       .VALIN6 = p.p.VALIN6,
                                       .VALIN7 = p.p.VALIN7,
                                       .VALIN8 = p.p.VALIN8,
                                       .VALIN9 = p.p.VALIN9,
                                       .VALIN10 = p.p.VALIN10,
                                       .VALIN11 = p.p.VALIN11,
                                       .VALIN12 = p.p.VALIN12,
                                       .VALIN13 = p.p.VALIN13,
                                       .VALIN14 = p.p.VALIN14,
                                       .VALIN15 = p.p.VALIN15,
                                       .VALIN16 = p.p.VALIN16,
                                       .VALOUT1 = p.p.VALOUT1,
                                       .VALOUT2 = p.p.VALOUT2,
                                       .VALOUT3 = p.p.VALOUT3,
                                       .VALOUT4 = p.p.VALOUT4,
                                       .VALOUT5 = p.p.VALOUT5,
                                       .VALOUT6 = p.p.VALOUT6,
                                       .VALOUT7 = p.p.VALOUT7,
                                       .VALOUT8 = p.p.VALOUT8,
                                       .VALOUT9 = p.p.VALOUT9,
                                       .VALOUT10 = p.p.VALOUT10,
                                       .VALOUT11 = p.p.VALOUT11,
                                       .VALOUT12 = p.p.VALOUT12,
                                       .VALOUT13 = p.p.VALOUT13,
                                       .VALOUT14 = p.p.VALOUT14,
                                       .VALOUT15 = p.p.VALOUT15,
                                       .VALOUT16 = p.p.VALOUT16,
                                       .CREATED_DATE = p.p.CREATED_DATE})
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetiTimeByEmployeeCode(ByVal objid As Decimal?) As Decimal?
        Try
            Return (From e In Context.HU_EMPLOYEE
                    Where e.ID = objid
                    Select e.ITIME_ID).FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
        End Try
    End Function

    Public Function InsertDataInout(ByVal lstDataInout As List(Of AT_DATAINOUTDTO), ByVal fromDate As Date, ByVal toDate As Date,
                                    ByVal log As UserLog) As Boolean
        Dim objDataInoutData As New AT_DATA_INOUT
        Try
            'THEM DU LIEU VAO 
            Dim itime = GetiTimeByEmployeeCode(lstDataInout(0).EMPLOYEE_ID)
            If itime IsNot Nothing OrElse itime <> 0 Then
                Using conMng As New ConnectionManager
                    Using conn As New OracleConnection(conMng.GetConnectionString())
                        Using cmd As New OracleCommand()
                            Try
                                conn.Open()
                                cmd.Connection = conn
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.INSERT_TIME_CARD"
                                cmd.Transaction = cmd.Connection.BeginTransaction()
                                For Each objDataInout In lstDataInout
                                    cmd.Parameters.Clear()
                                    Using resource As New DataAccess.OracleCommon()
                                        Dim objParam = New With {.P_TIMEID = itime,
                                                                 .P_VALTIME = objDataInout.VALIN1,
                                                                 .P_USERNAME = log.Username.ToUpper}

                                        If objParam IsNot Nothing Then
                                            For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                                Dim bOut As Boolean = False
                                                Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                                If para IsNot Nothing Then
                                                    cmd.Parameters.Add(para)
                                                End If
                                            Next
                                        End If
                                        cmd.ExecuteNonQuery()
                                    End Using
                                Next
                                cmd.Transaction.Commit()
                            Catch ex As Exception
                                cmd.Transaction.Rollback()
                            Finally
                                'Dispose all resource
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()
                            End Try
                        End Using
                    End Using
                End Using

                ' UPDATE DATA TO AT_DATAINOUTDTO
                Using cls As New DataAccess.QueryData
                    cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.UPDATE_DATAINOUT",
                                                   New With {.P_ITIMEID = itime,
                                                             .P_USERNAME = log.Username.ToUpper,
                                                             .P_FROMDATE = fromDate,
                                                             .P_ENDDATE = toDate})
                End Using
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyDataInout(ByVal objDataInout As AT_DATAINOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objDataInoutData As New AT_DATA_INOUT With {.ID = objDataInout.ID}
        Try
            Context.AT_DATA_INOUT.Attach(objDataInoutData)

            Context.SaveChanges(log)
            gID = objDataInoutData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function DeleteDataInout(ByVal lstDataInout() As AT_DATAINOUTDTO) As Boolean
        Dim lstDataInoutData As List(Of AT_DATA_INOUT)
        Dim lstIDDataInout As List(Of Decimal?) = (From p In lstDataInout.ToList Select p.ID).ToList
        Dim empId As Decimal?
        Dim iTime As Decimal?
        Try
            ' xoa du lieu ben bang tai du lieu may cham cong
            lstDataInoutData = (From p In Context.AT_DATA_INOUT Where lstIDDataInout.Contains(p.ID)).ToList
            empId = (From p In Context.AT_DATA_INOUT Where lstIDDataInout.Contains(p.ID)).FirstOrDefault.EMPLOYEE_ID
            ' lay ma quet the
            iTime = GetiTimeByEmployeeCode(empId)

            Dim swipe = (From p In Context.AT_SWIPE_DATA.Where(Function(f) f.ITIME_ID = iTime)).ToList
            For index = 0 To swipe.Count - 1
                Context.AT_SWIPE_DATA.DeleteObject(swipe(index))
            Next

            For index = 0 To lstDataInoutData.Count - 1
                Context.AT_DATA_INOUT.DeleteObject(lstDataInoutData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function DeleteDataInoutById(ByVal id As Decimal?) As Boolean
        Dim lstDataInoutData As AT_DATA_INOUT
        Try
            lstDataInoutData = (From p In Context.AT_DATA_INOUT Where p.ID = id).FirstOrDefault
            Context.AT_DATA_INOUT.DeleteObject(lstDataInoutData)
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

#End Region

#Region "PHEP NAM"
    Public Function CALCULATE_ENTITLEMENT(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As UserLog) As Boolean
        Try
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = param.PERIOD_ID
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CALL_ENTITLEMENT",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE})
            End Using

            LOG_AT(param, log, listEmployeeId, "TỔNG HỢP NGHỈ PHÉP", obj, param.ORG_ID)

            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function CALL_ENTITLEMENT_HOSE(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As UserLog) As Boolean
        Try
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = param.PERIOD_ID
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CALL_ENTITLEMENT_HOSE",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE})
            End Using

            LOG_AT(param, log, listEmployeeId, "TỔNG HỢP NGHỈ PHÉP", obj, param.ORG_ID)

            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function AT_ENTITLEMENT_PREV_HAVE(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As UserLog) As Boolean
        Try
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = param.PERIOD_ID
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.AT_ENTITLEMENT_PREV_HAVE",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE})
            End Using

            LOG_AT(param, log, listEmployeeId, "KẾT NGHỈ PHÉP", obj, param.ORG_ID)

            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function CheckPeriodMonth(ByVal year As Integer, ByVal PeriodId As Integer, ByRef PeriodNext As Integer) As Boolean
        Try

            Dim query = (From p In Context.AT_PERIOD
                         Where p.ID = PeriodId And p.MONTH = 12).FirstOrDefault


            If query IsNot Nothing Then

                Dim query1 = (From p In Context.AT_PERIOD
                         Where p.YEAR = year + 1 And p.MONTH = 1).FirstOrDefault
                PeriodNext = query1.ID
                Return False
            Else
                PeriodNext = 0S
                Return True
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function GetEntitlement(ByVal _filter As AT_ENTITLEMENTDTO,
                                  ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_ENTITLEMENTDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim per = (From p In Context.AT_PERIOD Where p.ID = _filter.CAL_PERIOD).FirstOrDefault
            Dim thang = Month(per.END_DATE)

            Dim query = From en In Context.AT_ENTITLEMENT
                        From E In Context.HU_EMPLOYEE.Where(Function(f) f.ID = en.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = E.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = E.TITLE_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = E.STAFF_RANK_ID).DefaultIfEmpty
                        From p In Context.AT_PERIOD.Where(Function(f) f.ID = en.PERIOD_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) E.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where en.PERIOD_ID = _filter.CAL_PERIOD

            Dim dateStart = per.END_DATE
            If Not _filter.IS_TERMINATE Then
                query = query.Where(Function(f) f.E.WORK_STATUS Is Nothing Or f.E.WORK_STATUS <> 257 Or
                                  (f.E.WORK_STATUS = 257 And f.E.TER_LAST_DATE >= dateStart))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.E.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.FULLNAME_VN) Then
                query = query.Where(Function(f) f.E.FULLNAME_VN.ToLower().Contains(_filter.FULLNAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME_VN) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                query = query.Where(Function(f) f.c.NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.JOIN_DATE_STATE.HasValue Then
                query = query.Where(Function(f) f.E.JOIN_DATE = _filter.JOIN_DATE_STATE)
            End If
            If Not String.IsNullOrEmpty(_filter.PERIOD_NAME) Then
                query = query.Where(Function(f) f.p.PERIOD_NAME.ToLower().Contains(_filter.PERIOD_NAME.ToLower()))
            End If
            If _filter.WORKING_TIME_HAVE.HasValue Then
                query = query.Where(Function(f) f.en.WORKING_TIME_HAVE = _filter.WORKING_TIME_HAVE)
            End If
            If _filter.BALANCE_WORKING_TIME.HasValue Then
                query = query.Where(Function(f) f.en.BALANCE_WORKING_TIME = _filter.BALANCE_WORKING_TIME)
            End If
            If _filter.PREV_HAVE.HasValue Then
                query = query.Where(Function(f) f.en.PREV_HAVE = _filter.PREV_HAVE)
            End If
            If _filter.EXPIREDATE.HasValue Then
                query = query.Where(Function(f) f.en.EXPIREDATE = _filter.EXPIREDATE)
            End If
            If _filter.CUR_USED1.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED1 = _filter.CUR_USED1)
            End If
            If _filter.CUR_USED2.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED2 = _filter.CUR_USED2)
            End If
            If _filter.CUR_USED3.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED3 = _filter.CUR_USED3)
            End If
            If _filter.CUR_USED4.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED4 = _filter.CUR_USED4)
            End If
            If _filter.CUR_USED5.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED5 = _filter.CUR_USED5)
            End If
            If _filter.CUR_USED6.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED6 = _filter.CUR_USED6)
            End If
            If _filter.CUR_USED7.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED7 = _filter.CUR_USED7)
            End If
            If _filter.CUR_USED8.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED8 = _filter.CUR_USED8)
            End If
            If _filter.CUR_USED9.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED9 = _filter.CUR_USED9)
            End If
            If _filter.CUR_USED10.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED10 = _filter.CUR_USED10)
            End If
            If _filter.CUR_USED11.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED11 = _filter.CUR_USED11)
            End If
            If _filter.CUR_USED12.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED12 = _filter.CUR_USED12)
            End If
            If _filter.CUR_USED.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED = _filter.CUR_USED)
            End If
            If _filter.CUR_HAVE.HasValue Then
                query = query.Where(Function(f) f.en.CUR_HAVE = _filter.CUR_HAVE)
            End If
            If _filter.TOTAL_CUR_HAVE.HasValue Then
                query = query.Where(Function(f) f.en.TOTAL_HAVE = _filter.TOTAL_CUR_HAVE)
            End If

            If _filter.SENIORITYHAVE.HasValue Then
                query = query.Where(Function(f) f.en.SENIORITYHAVE = _filter.SENIORITYHAVE)
            End If

            If _filter.TOTAL_HAVE1.HasValue Then
                query = query.Where(Function(f) f.en.TOTAL_HAVE1 = _filter.TOTAL_HAVE1)
            End If

            Dim lst = query.Select(Function(p) New AT_ENTITLEMENTDTO With {
                                       .ID = p.en.ID,
                                       .EMPLOYEE_ID = p.en.EMPLOYEE_ID,
                                       .FULLNAME_VN = p.E.FULLNAME_VN,
                                       .EMPLOYEE_CODE = p.E.EMPLOYEE_CODE,
                                       .TITLE_NAME_VN = p.t.NAME_VN,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .JOIN_DATE_STATE = p.E.JOIN_DATE,
                                       .PERIOD_NAME = p.p.PERIOD_NAME,
                                       .WORKING_TIME_HAVE = p.en.WORKING_TIME_HAVE,
                                       .PREV_HAVE = p.en.PREV_HAVE,
                                       .EXPIREDATE = p.en.EXPIREDATE,
                                       .BALANCE_WORKING_TIME = p.en.BALANCE_WORKING_TIME,
                                       .TOTAL_CUR_HAVE = p.en.TOTAL_HAVE,
                                       .CUR_USED1 = p.en.CUR_USED1,
                                       .CUR_USED2 = If(thang >= 2, p.en.CUR_USED2, 0),
                                       .CUR_USED3 = If(thang >= 3, p.en.CUR_USED3, 0),
                                       .CUR_USED4 = If(thang >= 4, p.en.CUR_USED4, 0),
                                       .CUR_USED5 = If(thang >= 5, p.en.CUR_USED5, 0),
                                       .CUR_USED6 = If(thang >= 6, p.en.CUR_USED6, 0),
                                       .CUR_USED7 = If(thang >= 7, p.en.CUR_USED7, 0),
                                       .CUR_USED8 = If(thang >= 8, p.en.CUR_USED8, 0),
                                       .CUR_USED9 = If(thang >= 9, p.en.CUR_USED9, 0),
                                       .CUR_USED10 = If(thang >= 10, p.en.CUR_USED10, 0),
                                       .CUR_USED11 = If(thang >= 11, p.en.CUR_USED11, 0),
                                       .CUR_USED12 = If(thang >= 12, p.en.CUR_USED12, 0),
                                       .CUR_USED = p.en.CUR_USED,
                                       .CUR_HAVE = p.en.CUR_HAVE,
                                       .CUR_HAVE1 = p.en.CUR_HAVE1,
                                       .CUR_HAVE2 = p.en.CUR_HAVE2,
                                       .CUR_HAVE3 = p.en.CUR_HAVE3,
                                       .CUR_HAVE4 = p.en.CUR_HAVE4,
                                       .CUR_HAVE5 = p.en.CUR_HAVE5,
                                       .CUR_HAVE6 = p.en.CUR_HAVE6,
                                       .CUR_HAVE7 = p.en.CUR_HAVE7,
                                       .CUR_HAVE8 = p.en.CUR_HAVE8,
                                       .CUR_HAVE9 = p.en.CUR_HAVE9,
                                       .CUR_HAVE10 = p.en.CUR_HAVE10,
                                       .CUR_HAVE11 = p.en.CUR_HAVE11,
                                       .CUR_HAVE12 = p.en.CUR_HAVE12,
                                       .AL_T1 = p.en.AL_T1,
                                       .AL_T2 = p.en.AL_T2,
                                       .AL_T3 = p.en.AL_T3,
                                       .AL_T4 = p.en.AL_T4,
                                       .AL_T5 = p.en.AL_T5,
                                       .AL_T6 = p.en.AL_T6,
                                       .AL_T7 = p.en.AL_T7,
                                       .AL_T8 = p.en.AL_T8,
                                       .AL_T9 = p.en.AL_T9,
                                       .AL_T10 = p.en.AL_T10,
                                       .AL_T11 = p.en.AL_T11,
                                       .AL_T12 = p.en.AL_T12,
                                       .CREATED_DATE = p.en.CREATED_DATE,
                                       .CREATED_BY = p.en.CREATED_BY,
                                       .SENIORITYHAVE = p.en.SENIORITYHAVE,
                                       .TOTAL_HAVE1 = p.en.TOTAL_HAVE1,
                                       .TIME_OUTSIDE_COMPANY = p.en.TIME_OUTSIDE_COMPANY,
                                       .TIME_SENIORITY = p.en.TIME_SENIORITY,
                                       .MONTH_SENIORITY_CHANGE = p.en.MONTH_SENIORITY_CHANGE,
                                       .TIME_SENIORITY_AFTER_CHANGE = p.en.TIME_SENIORITY_AFTER_CHANGE,
                                       .SENIORITY = p.en.SENIORITY,
                                       .PREVTOTAL_HAVE = p.en.PREVTOTAL_HAVE,
                                       .SENIORITY_EDIT = p.en.SENIORITY_EDIT,
                                       .PREV_USED = p.en.PREV_USED})
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            'Dim lstResult = lst.ToList
            'For Each item As AT_ENTITLEMENTDTO In lstResult
            '    item.CUR_USED = item.CUR_USED1 + item.CUR_USED2 + item.CUR_USED3 + item.CUR_USED4 + item.CUR_USED5 + item.CUR_USED6 _
            '                    + item.CUR_USED7 + item.CUR_USED8 + item.CUR_USED9 + item.CUR_USED10 + item.CUR_USED11 + item.CUR_USED12
            '    item.PREV_HAVE = If(item.PREV_HAVE = 0, Nothing, item.PREV_HAVE)
            '    item.TOTAL_CUR_HAVE = If(item.TOTAL_CUR_HAVE = 0, Nothing, item.TOTAL_CUR_HAVE)
            '    item.CUR_USED = If(item.CUR_USED = 0, Nothing, item.CUR_USED)
            '    item.CUR_HAVE = If(item.CUR_HAVE = 0, Nothing, item.CUR_HAVE)

            '    item.CUR_USED1 = If(item.CUR_USED1 = 0, Nothing, item.CUR_USED1)
            '    item.CUR_USED2 = If(item.CUR_USED2 = 0, Nothing, item.CUR_USED2)
            '    item.CUR_USED3 = If(item.CUR_USED3 = 0, Nothing, item.CUR_USED3)
            '    item.CUR_USED4 = If(item.CUR_USED4 = 0, Nothing, item.CUR_USED4)
            '    item.CUR_USED5 = If(item.CUR_USED5 = 0, Nothing, item.CUR_USED5)
            '    item.CUR_USED6 = If(item.CUR_USED6 = 0, Nothing, item.CUR_USED6)
            '    item.CUR_USED7 = If(item.CUR_USED7 = 0, Nothing, item.CUR_USED7)
            '    item.CUR_USED8 = If(item.CUR_USED8 = 0, Nothing, item.CUR_USED8)
            '    item.CUR_USED9 = If(item.CUR_USED9 = 0, Nothing, item.CUR_USED9)
            '    item.CUR_USED10 = If(item.CUR_USED10 = 0, Nothing, item.CUR_USED10)
            '    item.CUR_USED11 = If(item.CUR_USED11 = 0, Nothing, item.CUR_USED11)
            '    item.CUR_USED12 = If(item.CUR_USED12 = 0, Nothing, item.CUR_USED12)
            'Next

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ImportEntitlementLeave(ByVal P_DOCXML As String, ByVal P_USER As String, ByVal P_PERIOD As Decimal) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.IMPORT_ENTITLEMENT_LEAVE",
                                 New With {.P_DOCXML = P_DOCXML, .P_USER = P_USER, .P_PERIOD = P_PERIOD})
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
#End Region

#Region "WorkSign"
    Public Function GET_WORKSIGN(ByVal param As AT_WORKSIGNDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                'Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_WORKSIGN",
                '                               New With {.P_USERNAME = log.Username.ToUpper,
                '                                         .P_ORG_ID = param.ORG_ID,
                '                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                '                                         .P_PAGE_INDEX = param.PAGE_INDEX,
                '                                         .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                '                                         .P_PAGE_SIZE = param.PAGE_SIZE,
                '                                         .P_PERIOD_ID = param.PERIOD_ID,
                '                                         .P_VN_FULLNAME = param.VN_FULLNAME.ToUpper,
                '                                         .P_TITLE_NAME = param.TITLE_NAME.ToUpper,
                '                                         .P_ORG_NAME = param.ORG_NAME.ToUpper,
                '                                         .P_CUR = cls.OUT_CURSOR,
                '                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_WORKSIGN",
                                              New With {.P_USERNAME = log.Username.ToUpper,
                                                        .P_ORG_ID = param.ORG_ID,
                                                        .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                        .P_PAGE_INDEX = param.PAGE_INDEX,
                                                        .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                        .P_PAGE_SIZE = param.PAGE_SIZE,
                                                        .P_PERIOD_ID = param.PERIOD_ID,
                                                        .P_CUR = cls.OUT_CURSOR,
                                                        .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
        Return Nothing
    End Function
    ''' <summary>
    ''' Thêm mới xếp ca làm việc sử dụng import
    ''' </summary>
    ''' <param name="dtData"></param>
    ''' <param name="period_id"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertWorkSignByImport(ByVal dtData As DataTable,
                                           ByVal period_id As Decimal,
                                           ByVal log As UserLog) As Boolean
        Try
            Dim Period = (From w In Context.AT_PERIOD Where w.ID = period_id).FirstOrDefault

            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Using resource As New DataAccess.OracleCommon()
                            Try
                                conn.Open()
                                cmd.Connection = conn
                                cmd.Transaction = cmd.Connection.BeginTransaction()
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.INSERT_WORKSIGN_DATE"

                                For Each row As DataRow In dtData.Rows
                                    cmd.Parameters.Clear()
                                    Dim objParam = New With {.P_EMPLOYEEID = row("EMPLOYEE_ID").ToString,
                                                             .P_PERIODId = period_id,
                                                             .P_USERNAME = log.Username.ToUpper,
                                                             .P_D1 = Utilities.Obj2Decima(row("D1"), Nothing),
                                                             .P_D2 = Utilities.Obj2Decima(row("D2"), Nothing),
                                                             .P_D3 = Utilities.Obj2Decima(row("D3"), Nothing),
                                                             .P_D4 = Utilities.Obj2Decima(row("D4"), Nothing),
                                                             .P_D5 = Utilities.Obj2Decima(row("D5"), Nothing),
                                                             .P_D6 = Utilities.Obj2Decima(row("D6"), Nothing),
                                                             .P_D7 = Utilities.Obj2Decima(row("D7"), Nothing),
                                                             .P_D8 = Utilities.Obj2Decima(row("D8"), Nothing),
                                                             .P_D9 = Utilities.Obj2Decima(row("D9"), Nothing),
                                                             .P_D10 = Utilities.Obj2Decima(row("D10"), Nothing),
                                                             .P_D11 = Utilities.Obj2Decima(row("D11"), Nothing),
                                                             .P_D12 = Utilities.Obj2Decima(row("D12"), Nothing),
                                                             .P_D13 = Utilities.Obj2Decima(row("D13"), Nothing),
                                                             .P_D14 = Utilities.Obj2Decima(row("D14"), Nothing),
                                                             .P_D15 = Utilities.Obj2Decima(row("D15"), Nothing),
                                                             .P_D16 = Utilities.Obj2Decima(row("D16"), Nothing),
                                                             .P_D17 = Utilities.Obj2Decima(row("D17"), Nothing),
                                                             .P_D18 = Utilities.Obj2Decima(row("D18"), Nothing),
                                                             .P_D19 = Utilities.Obj2Decima(row("D19"), Nothing),
                                                             .P_D20 = Utilities.Obj2Decima(row("D20"), Nothing),
                                                             .P_D21 = Utilities.Obj2Decima(row("D21"), Nothing),
                                                             .P_D22 = Utilities.Obj2Decima(row("D22"), Nothing),
                                                             .P_D23 = Utilities.Obj2Decima(row("D23"), Nothing),
                                                             .P_D24 = Utilities.Obj2Decima(row("D24"), Nothing),
                                                             .P_D25 = Utilities.Obj2Decima(row("D25"), Nothing),
                                                             .P_D26 = Utilities.Obj2Decima(row("D26"), Nothing),
                                                             .P_D27 = Utilities.Obj2Decima(row("D27"), Nothing),
                                                             .P_D28 = Utilities.Obj2Decima(row("D28"), Nothing),
                                                             .P_D29 = Utilities.Obj2Decima(row("D29"), Nothing),
                                                             .P_D30 = Utilities.Obj2Decima(row("D30"), Nothing),
                                                             .P_D31 = Utilities.Obj2Decima(row("D31"), Nothing)}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                Next

                                cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.IMPORT_WORKSIGN_DATE"
                                cmd.Parameters.Clear()
                                Dim objParam1 = New With {.P_STARTDATE = Period.START_DATE.Value,
                                                         .P_ENDDATE = Period.END_DATE.Value,
                                                         .P_USERNAME = log.Username.ToUpper}

                                If objParam1 IsNot Nothing Then
                                    For Each info As PropertyInfo In objParam1.GetType().GetProperties()
                                        Dim bOut As Boolean = False
                                        Dim para = resource.GetParameter(info.Name, info.GetValue(objParam1, Nothing), bOut)
                                        If para IsNot Nothing Then
                                            cmd.Parameters.Add(para)
                                        End If
                                    Next
                                End If

                                cmd.ExecuteNonQuery()

                                cmd.Transaction.Commit()
                            Catch ex As Exception
                                cmd.Transaction.Rollback()
                                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
                                Throw ex
                            Finally
                                'Dispose all resource
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()
                            End Try
                        End Using
                    End Using
                End Using
            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Thêm mới xếp ca làm việc
    ''' </summary>
    ''' <param name="objWorkSigns"></param>
    ''' <param name="objWork"></param>
    ''' <param name="p_fromdate"></param>
    ''' <param name="p_endDate"></param>
    ''' <param name="log"></param>
    ''' <param name="gID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertWorkSign(ByVal objWorkSigns As List(Of AT_WORKSIGNDTO), ByVal objWork As AT_WORKSIGNDTO, ByVal p_fromdate As Date, ByVal p_endDate As Date?, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objWorkSign As New AT_WORKSIGNDTO
        Dim p_fromDateBefor As Date = p_fromdate
        Dim CODE_OFF As String = ""
        CODE_OFF = "OFF"
        Try
            For index = 0 To objWorkSigns.Count - 1
                objWorkSign = objWorkSigns(index)
                p_fromdate = p_fromDateBefor
                While p_fromdate <= p_endDate
                    Dim objWorkSignData As New AT_WORKSIGN
                    ' kiem tra da ton tai chua
                    Dim exist = (From c In Context.AT_WORKSIGN
                                 Where c.EMPLOYEE_ID = objWorkSign.EMPLOYEE_ID And _
                                 c.WORKINGDAY = p_fromdate).Any
                    If exist Then

                        Dim query = (From c In Context.AT_WORKSIGN
                                     Where c.EMPLOYEE_ID = objWorkSign.EMPLOYEE_ID And _
                                     c.WORKINGDAY = p_fromdate).FirstOrDefault

                        Dim shiftIDU = (From f In Context.AT_SHIFT Where f.ID = objWork.SHIFT_ID Select f).FirstOrDefault

                        Dim shiftOffu = (From f In Context.AT_SHIFT Where f.CODE = CODE_OFF Select f).FirstOrDefault
                        If Not shiftOffu Is Nothing Then
                            If p_fromdate.DayOfWeek = DayOfWeek.Sunday And Not String.IsNullOrEmpty(shiftOffu.ID) Then
                                If shiftIDU.SUNDAY.Value > 0 Then
                                    query.SHIFT_ID = shiftOffu.ID
                                Else
                                    query.SHIFT_ID = objWork.SHIFT_ID
                                End If
                            ElseIf p_fromdate.DayOfWeek = DayOfWeek.Saturday And Not String.IsNullOrEmpty(shiftOffu.ID) Then
                                If shiftIDU.SATURDAY.Value > 0 Then
                                    query.SHIFT_ID = shiftOffu.ID 'shiftIDU.SATURDAY
                                Else
                                    query.SHIFT_ID = objWork.SHIFT_ID
                                End If
                            Else
                                query.SHIFT_ID = objWork.SHIFT_ID
                            End If
                        End If
                        Context.SaveChanges(log)
                        p_fromdate = p_fromdate.AddDays(1)
                        Continue While
                    End If
                    objWorkSignData.ID = Utilities.GetNextSequence(Context, Context.AT_WORKSIGN.EntitySet.Name)
                    objWorkSignData.EMPLOYEE_ID = objWorkSign.EMPLOYEE_ID
                    objWorkSignData.WORKINGDAY = p_fromdate

                    Dim shiftId = (From f In Context.AT_SHIFT Where f.ID = objWork.SHIFT_ID Select f).FirstOrDefault
                    Dim shiftOff = (From f In Context.AT_SHIFT Where f.CODE = CODE_OFF Select f).FirstOrDefault
                    If p_fromdate.DayOfWeek = DayOfWeek.Sunday And Not String.IsNullOrEmpty(shiftOff.ID) Then
                        If shiftId.SUNDAY.Value > 0 Then
                            objWorkSignData.SHIFT_ID = shiftOff.ID
                        Else
                            objWorkSignData.SHIFT_ID = objWork.SHIFT_ID
                        End If
                    ElseIf p_fromdate.DayOfWeek = DayOfWeek.Saturday And Not String.IsNullOrEmpty(shiftOff.ID) Then
                        If shiftId.SATURDAY.Value > 0 Then
                            objWorkSignData.SHIFT_ID = shiftOff.ID 'shiftIDU.SATURDAY
                        Else
                            objWorkSignData.SHIFT_ID = objWork.SHIFT_ID
                        End If
                    Else
                        objWorkSignData.SHIFT_ID = objWork.SHIFT_ID
                    End If
                    objWorkSignData.PERIOD_ID = objWork.PERIOD_ID
                    Context.AT_WORKSIGN.AddObject(objWorkSignData)
                    Context.SaveChanges(log)
                    p_fromdate = p_fromdate.AddDays(1)
                    gID = objWorkSignData.ID
                End While
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Validate tồn tại xếp ca làm việc của  nhân viên
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateWorkSign(ByVal _validate As AT_WORKSIGNDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_WORKSIGN
                             Where p.SHIFT_ID = _validate.SHIFT_ID _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_WORKSIGN
                             Where p.SHIFT_ID = _validate.SHIFT_ID).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Sửa thông tin xếp ca làm việc
    ''' </summary>
    ''' <param name="objWorkSign"></param>
    ''' <param name="log"></param>
    ''' <param name="gID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ModifyWorkSign(ByVal objWorkSign As AT_WORKSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objWorkSignData As New AT_WORKSIGN With {.ID = objWorkSign.ID}
        Try
            Context.AT_WORKSIGN.Attach(objWorkSignData)
            objWorkSignData.EMPLOYEE_ID = objWorkSign.EMPLOYEE_ID
            objWorkSignData.WORKINGDAY = objWorkSign.WORKINGDAY
            objWorkSignData.PERIOD_ID = objWorkSign.PERIOD_ID
            objWorkSignData.SHIFT_ID = objWorkSign.SHIFT_ID
            Context.SaveChanges(log)
            gID = objWorkSignData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function
    ''' <summary>
    ''' Xóa thông tin xếp ca làm việc
    ''' </summary>
    ''' <param name="lstWorkSign"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteWorkSign(ByVal lstWorkSign() As AT_WORKSIGNDTO) As Boolean
        Dim lstWorkSignData As List(Of AT_WORKSIGN)
        Dim lstIDWorkSign As List(Of Decimal) = (From p In lstWorkSign.ToList Select p.ID).ToList
        Try

            lstWorkSignData = (From p In Context.AT_WORKSIGN Where lstIDWorkSign.Contains(p.ID)).ToList
            For index = 0 To lstWorkSignData.Count - 1
                Context.AT_WORKSIGN.DeleteObject(lstWorkSignData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function
    ''' <summary>
    ''' Xóa ca làm việc của một nhân viên
    ''' </summary>
    ''' <param name="employee_id"></param>
    ''' <param name="p_From"></param>
    ''' <param name="p_to"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Del_WorkSign_ByEmp(ByVal employee_id As Decimal, ByVal p_From As Date, ByVal p_to As Date) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.DELETE_WORKSIGN",
                                               New With {.P_EMPLOYEE_ID = employee_id,
                                                         .P_FROM = p_From,
                                                         .P_TO = p_to}, False)
                Return True
            End Using
            Return False

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function
    ''' <summary>
    ''' Lấy ca mặc định trong xếp ca
    ''' </summary>
    ''' <param name="param"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GETSIGNDEFAULT(ByVal param As ParamDTO, ByVal log As UserLog) As DataTable
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GETSIGNDEFAULT",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try


    End Function
#End Region

#Region "ProjectAssign"
    Public Function GET_ProjectAssign(ByVal param As AT_PROJECT_ASSIGNDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_PROJECT_BUSINESS.GET_ProjectAssign",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_PAGE_INDEX = param.PAGE_INDEX,
                                                         .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                         .P_PAGE_SIZE = param.PAGE_SIZE,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_PROJECT_ID = param.PROJECT_ID,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Function InsertProjectAssign(ByVal objProjectAssigns As List(Of AT_PROJECT_ASSIGNDTO), ByVal objWork As AT_PROJECT_ASSIGNDTO, ByVal p_fromdate As Date, ByVal p_endDate As Date?, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objProjectAssign As New AT_PROJECT_ASSIGNDTO
        Dim p_fromDateBefor As Date = p_fromdate
        Try
            For index = 0 To objProjectAssigns.Count - 1
                objProjectAssign = objProjectAssigns(index)
                p_fromdate = p_fromDateBefor
                While p_fromdate <= p_endDate
                    Dim objProjectAssignData As New AT_PROJECT_ASSIGN
                    ' kiem tra da ton tai chua
                    Dim exist = (From c In Context.AT_PROJECT_ASSIGN
                                 Where c.EMPLOYEE_ID = objProjectAssign.EMPLOYEE_ID And _
                                 c.WORKINGDAY = p_fromdate).Any
                    If exist Then

                        Dim query = (From c In Context.AT_PROJECT_ASSIGN
                                     Where c.EMPLOYEE_ID = objProjectAssign.EMPLOYEE_ID And _
                                     c.WORKINGDAY = p_fromdate).FirstOrDefault

                        query.PROJECT_ID = objWork.PROJECT_ID
                        query.PROJECT_WORK_ID = objWork.PROJECT_WORK_ID
                        query.HOURS = objWork.HOURS

                        Context.SaveChanges(log)
                        p_fromdate = p_fromdate.AddDays(1)
                        Continue While
                    End If
                    objProjectAssignData.ID = Utilities.GetNextSequence(Context, Context.AT_PROJECT_ASSIGN.EntitySet.Name)
                    objProjectAssignData.EMPLOYEE_ID = objProjectAssign.EMPLOYEE_ID
                    objProjectAssignData.WORKINGDAY = p_fromdate
                    objProjectAssignData.PERIOD_ID = objWork.PERIOD_ID
                    objProjectAssignData.PROJECT_ID = objWork.PROJECT_ID
                    objProjectAssignData.PROJECT_WORK_ID = objWork.PROJECT_WORK_ID
                    objProjectAssignData.HOURS = objWork.HOURS
                    Context.AT_PROJECT_ASSIGN.AddObject(objProjectAssignData)
                    Context.SaveChanges(log)
                    p_fromdate = p_fromdate.AddDays(1)
                    gID = objProjectAssignData.ID
                End While
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyProjectAssign(ByVal objProjectAssign As AT_PROJECT_ASSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objProjectAssignData As New AT_PROJECT_ASSIGN With {.ID = objProjectAssign.ID}
        Try
            Context.AT_PROJECT_ASSIGN.Attach(objProjectAssignData)
            objProjectAssignData.EMPLOYEE_ID = objProjectAssign.EMPLOYEE_ID
            objProjectAssignData.WORKINGDAY = objProjectAssign.WORKINGDAY
            objProjectAssignData.PERIOD_ID = objProjectAssign.PERIOD_ID
            objProjectAssignData.HOURS = objProjectAssign.HOURS
            Context.SaveChanges(log)
            gID = objProjectAssignData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function DeleteProjectAssign(ByVal lstProjectAssign() As AT_PROJECT_ASSIGNDTO) As Boolean
        Dim lstProjectAssignData As List(Of AT_PROJECT_ASSIGN)
        Dim lstIDProjectAssign As List(Of Decimal) = (From p In lstProjectAssign.ToList Select p.ID).ToList
        Try

            lstProjectAssignData = (From p In Context.AT_PROJECT_ASSIGN Where lstIDProjectAssign.Contains(p.ID)).ToList
            For index = 0 To lstProjectAssignData.Count - 1
                Context.AT_PROJECT_ASSIGN.DeleteObject(lstProjectAssignData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

#End Region

#Region "TAI DU LIEU MAY CHAM CONG"
    ''' <summary>
    ''' Lấy danh sách dữ liệu máy chấm công
    ''' </summary>
    ''' <param name="_filter"></param>
    ''' <param name="PageIndex"></param>
    ''' <param name="PageSize"></param>
    ''' <param name="Total"></param>
    ''' <param name="Sorts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSwipeData(ByVal _filter As AT_SWIPE_DATADTO,
                                    ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "iTime_id, VALTIME desc") As List(Of AT_SWIPE_DATADTO)
        Try
            Dim query = From p In Context.AT_SWIPE_DATA
                        From machine_type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MACHINE_TYPE).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
            Dim lst = query.Select(Function(p) New AT_SWIPE_DATADTO With {
                                       .ID = p.p.ID,
                                       .ITIME_ID = p.p.ITIME_ID,
                                       .ITIME_ID_S = p.p.ITIME_ID,
                                       .TERMINAL_ID = p.p.TERMINAL_ID,
                                       .TERMINAL_CODE = If(p.p.TERMINAL_ID = 1, "Máy vào", If(p.p.TERMINAL_ID = 2, "Máy ra", "")),
                                       .MACHINE_TYPE = p.p.MACHINE_TYPE,
                                       .MACHINE_TYPE_NAME = p.machine_type.NAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .VALTIME = p.p.VALTIME})

            'If _filter.ITIME_ID <> "" Then
            '    lst = lst.Where(Function(f) f.ITIME_ID = _filter.ITIME_ID)
            'End If
            If (_filter.TERMINAL_CODE <> "") Then
                lst = lst.Where(Function(f) f.TERMINAL_CODE.ToUpper.Contains(_filter.TERMINAL_CODE.ToUpper))
            End If
            If (_filter.EMPLOYEE_CODE <> "") Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If (_filter.EMPLOYEE_NAME <> "") Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If (_filter.ITIME_ID_S <> "") Then
                lst = lst.Where(Function(f) f.ITIME_ID_S.ToUpper.Contains(_filter.ITIME_ID_S.ToUpper))
            End If
            If (_filter.MACHINE_TYPE_NAME <> "") Then
                lst = lst.Where(Function(f) f.MACHINE_TYPE_NAME.ToUpper.Contains(_filter.MACHINE_TYPE_NAME.ToUpper))
            End If

            If Not IsNothing(_filter.MACHINE_TYPE) Then
                lst = lst.Where(Function(f) f.MACHINE_TYPE = _filter.MACHINE_TYPE)
            End If

            If _filter.WORKINGDAY.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY = _filter.WORKINGDAY)
            End If
            If _filter.FROM_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY <= _filter.END_DATE)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Thêm mới dữ liệu chấm công
    ''' </summary>
    ''' <param name="objSwipeData"></param>
    ''' <param name="machine"></param>
    ''' <param name="P_FROMDATE"></param>
    ''' <param name="P_ENDDATE"></param>
    ''' <param name="log"></param>
    ''' <param name="gID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertSwipeData(ByVal objSwipeData As List(Of AT_SWIPE_DATADTO),
                                    ByVal machine As AT_TERMINALSDTO,
                                    ByVal P_FROMDATE As Date?,
                                    ByVal P_ENDDATE As Date?,
                                    ByVal log As UserLog,
                                    ByRef gID As Decimal) As Boolean
        Dim sv_SDK1 As New zkemkeeper.CZKEM
        sv_GetLogData(sv_SDK1, machine, P_FROMDATE, P_ENDDATE)
        Dim objWorkSignData As New AT_SWIPE_DATA
        Try
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Try
                            conn.Open()
                            cmd.Connection = conn
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.INSERT_TIME_CARD"
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            For index = 0 To ls_AT_SWIPE_DATADTO.Count - 1
                                cmd.Parameters.Clear()
                                Using resource As New DataAccess.OracleCommon()
                                    Dim objParam = New With {.P_TIMEID = ls_AT_SWIPE_DATADTO(index).ITIME_ID,
                                                             .P_VALTIME = ls_AT_SWIPE_DATADTO(index).VALTIME,
                                                             .P_USERNAME = log.Username.ToUpper}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                    gID = objWorkSignData.ID
                                End Using
                            Next
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            cmd.Transaction.Rollback()
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using


            ' UPDATE DATA TO AT_DATAINOUTDTO
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.UPDATE_DATAINOUT",
                                               New With {.P_ITIMEID = 0,
                                                         .P_USERNAME = log.Username.ToUpper,
                                                         .P_FROMDATE = P_FROMDATE,
                                                         .P_ENDDATE = P_ENDDATE})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' nhập danh sách chấm công theo file mẫu
    ''' </summary>
    ''' <param name="ls_AT_SWIPE_DATADTO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ImportSwipeDataAuto(ByVal ls_AT_SWIPE_DATADTO As List(Of AT_SWIPE_DATADTO)) As Boolean
        Dim endDate As Date?
        Dim fromDate As Date?
        Try
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Try
                            conn.Open()
                            cmd.Connection = conn
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.INSERT_TIME_CARD_AUTO"
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            For index = 0 To ls_AT_SWIPE_DATADTO.Count - 1
                                cmd.Parameters.Clear()
                                Dim objDataInout = ls_AT_SWIPE_DATADTO(index)
                                If endDate Is Nothing Then
                                    endDate = objDataInout.VALTIME.Value.Date
                                Else
                                    If objDataInout.VALTIME.Value.Date > endDate Then
                                        endDate = objDataInout.VALTIME.Value.Date
                                    End If
                                End If

                                If fromDate Is Nothing Then
                                    fromDate = objDataInout.VALTIME.Value.Date
                                Else
                                    If objDataInout.VALTIME.Value.Date < fromDate Then
                                        fromDate = objDataInout.VALTIME.Value.Date
                                    End If
                                End If

                                Using resource As New DataAccess.OracleCommon()
                                    Dim objParam = New With {.P_TIMEID = objDataInout.ITIME_ID,
                                                             .P_TERMINAL_ID = objDataInout.TERMINAL_ID,
                                                             .P_VALTIME = objDataInout.VALTIME,
                                                             .P_USERNAME = "ADMIN"}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                End Using
                            Next
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
                            Throw ex
                            cmd.Transaction.Rollback()
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using
            If endDate IsNot Nothing And fromDate IsNot Nothing Then
                Using cls As New DataAccess.QueryData
                    cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.UPDATE_DATAINOUT",
                                                   New With {.P_ITIMEID = 0,
                                                             .P_USERNAME = "ADMIN",
                                                             .P_FROMDATE = fromDate,
                                                             .P_ENDDATE = endDate})
                End Using

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Private Sub sv_GetLogData(ByVal sv_SDK1 As zkemkeeper.CZKEM,
                              ByVal machine As AT_TERMINALSDTO,
                              ByVal p_fromdate As Date?,
                              ByVal p_EndDate As Date?)
        Dim sv_ErrorNo As Integer = 0
        Dim mv_Port As String = machine.PORT
        Dim message As String = ""
        Dim rowSwipe As AT_SWIPE_DATADTO
        Dim sv_vErrorCode As Integer = 0
        Dim sv_vRet As Boolean = False
        Dim sv_i As Integer = 1
        Dim mv_IP As String
        mv_IP = machine.TERMINAL_IP
        Dim mv_MachineNumber = 1
        Try
            If mv_IP <> "" Then
                If Not String.IsNullOrEmpty(machine.PASS) Then
                    sv_SDK1.SetCommPassword(machine.PASS)
                End If
                Try

                    If sv_SDK1.Connect_Net(mv_IP, mv_Port) Then
                        sv_SDK1.EnableDevice(1, False) 'disable the device
                        Dim idwEnrollNumber As Integer
                        Dim idwVerifyMode As Integer
                        Dim idwInOutMode As Integer
                        Dim idwYear As Integer
                        Dim idwMonth As Integer
                        Dim idwDay As Integer
                        Dim idwHour As Integer
                        Dim idwMinute As Integer
                        Dim idwSecond As Integer
                        Dim idwWorkcode As Integer
                        If mv_IP <> "0" Then
                            ls_AT_SWIPE_DATADTO.Clear()
                        End If
                        If sv_vRet Then
                            sv_SDK1.GetLastError(sv_ErrorNo)
                            While sv_ErrorNo <> 0
                                sv_SDK1.EnableDevice(mv_MachineNumber, False)

                                If sv_SDK1.ReadGeneralLogData(mv_MachineNumber) Then
                                    While sv_SDK1.SSR_GetGeneralLogData(mv_MachineNumber, idwEnrollNumber,
                                                                        idwVerifyMode, idwInOutMode,
                                                                        idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond,
                                                                        idwWorkcode)
                                        If idwYear > Date.Now.Year Then
                                            Continue While
                                        End If
                                        If (New Date(idwYear, idwMonth, idwDay) >= CDate(p_fromdate) And New Date(idwYear, idwMonth, idwDay) <= CDate(p_EndDate)) Then
                                            rowSwipe = New AT_SWIPE_DATADTO
                                            rowSwipe.ITIME_ID = idwEnrollNumber
                                            rowSwipe.VALTIME = New Date(idwYear, idwMonth, idwDay, idwHour, idwMinute, 0)
                                            ls_AT_SWIPE_DATADTO.Add(rowSwipe)
                                        End If
                                    End While
                                End If
                                sv_SDK1.GetLastError(sv_ErrorNo)
                            End While
                        Else
                            sv_SDK1.GetLastError(sv_vErrorCode)
                        End If
                    Else
                    End If
                Catch ex As Exception

                Finally
                    sv_SDK1.EnableDevice(1, True)
                    sv_SDK1.Disconnect()
                End Try
            Else
                If String.IsNullOrEmpty(mv_IP) Then
                Else
                    message &= "IP " + mv_IP + " không đúng!" & vbCrLf
                End If
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Sub

    Public Function InsertSwipeDataImport(ByVal lstData As List(Of AT_SWIPE_DATADTO), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim fromDate As Date?
            Dim endDate As New Date?
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Try
                            conn.Open()
                            cmd.Connection = conn
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.INSERT_TIME_CARD"
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            For Each objDataInout In lstData
                                If endDate Is Nothing Then
                                    endDate = objDataInout.WORKINGDAY
                                Else
                                    If objDataInout.WORKINGDAY > endDate Then
                                        endDate = objDataInout.WORKINGDAY
                                    End If
                                End If

                                If fromDate Is Nothing Then
                                    fromDate = objDataInout.WORKINGDAY
                                Else
                                    If objDataInout.WORKINGDAY < fromDate Then
                                        fromDate = objDataInout.WORKINGDAY
                                    End If
                                End If
                                cmd.Parameters.Clear()
                                Using resource As New DataAccess.OracleCommon()
                                    Dim objParam = New With {.P_TIMEID = objDataInout.ITIME_ID,
                                                             .P_TERMINALID = objDataInout.TERMINAL_ID,
                                                             .P_VALTIME = objDataInout.VALTIME,
                                                             .P_USERNAME = log.Username.ToUpper}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                End Using
                            Next
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            cmd.Transaction.Rollback()
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using

            ' UPDATE DATA TO AT_DATAINOUTDTO
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.UPDATE_DATAINOUT",
                                               New With {.P_ITIMEID = 0,
                                                         .P_USERNAME = log.Username.ToUpper,
                                                         .P_FROMDATE = fromDate,
                                                         .P_ENDDATE = endDate})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ImportSwipeData(ByVal dtData As DataTable, ByVal log As UserLog) As Boolean
        Try
            Dim dsData As New DataSet
            dsData.Tables.Add(dtData)
            Dim strXml = dsData.GetXml()

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.IMPORT_SWIPE_DATA",
                                               New With {.P_XML = strXml,
                                                         .P_USERNAME = log.Username.ToUpper})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#Region "LOG"
    Public Function GetActionLog(ByVal _filter As AT_ACTION_LOGDTO,
                                        ByRef Total As Integer,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        Optional ByVal Sorts As String = "ACTION_DATE desc") As List(Of AT_ACTION_LOGDTO)

        Try
            Dim query = From p In Context.AT_ACTION_LOG
                        From e In Context.SE_USER.Where(Function(f) f.USERNAME.ToUpper = p.USERNAME.ToUpper)
                        From r In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty

            Dim lst = query.Select(Function(p) New AT_ACTION_LOGDTO With {
                                       .ID = p.p.ID,
                                       .username = p.p.USERNAME,
                                       .fullname = p.e.FULLNAME,
                                       .email = p.e.EMAIL,
                                       .mobile = p.e.TELEPHONE,
                                       .action_name = p.p.ACTION_NAME,
                                       .action_date = p.p.ACTION_DATE,
                                       .object_name = p.p.OBJECT_NAME,
                                       .PERIOD_ID = p.p.PERIOD_ID,
                                       .PERIOD_NAME = p.r.PERIOD_NAME,
                                       .ip = p.p.IP,
                                       .computer_name = p.p.COMPUTER_NAME,
                                       .action_type = p.p.ACTION_TYPE,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .NEW_VALUE = p.p.NEW_VALUE,
                                       .OLD_VALUE = p.p.OLD_VALUE})


            If Not String.IsNullOrEmpty(_filter.username) Then
                lst = lst.Where(Function(f) f.username.ToLower().Contains(_filter.username.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.fullname) Then
                lst = lst.Where(Function(f) f.fullname.ToLower().Contains(_filter.fullname.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.email) Then
                lst = lst.Where(Function(f) f.email.ToLower().Contains(_filter.email.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.mobile) Then
                lst = lst.Where(Function(f) f.mobile.ToLower().Contains(_filter.mobile.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.action_name) Then
                lst = lst.Where(Function(f) f.action_name.ToLower().Contains(_filter.action_name.ToLower()))
            End If
            If _filter.action_date.HasValue Then
                lst = lst.Where(Function(f) f.action_date >= _filter.action_date)
            End If
            If Not String.IsNullOrEmpty(_filter.object_name) Then
                lst = lst.Where(Function(f) f.object_name.ToLower().Contains(_filter.object_name.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ip) Then
                lst = lst.Where(Function(f) f.ip.ToLower().Contains(_filter.ip.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.computer_name) Then
                lst = lst.Where(Function(f) f.computer_name.ToLower().Contains(_filter.computer_name.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.action_type) Then
                lst = lst.Where(Function(f) f.action_type.ToLower().Contains(_filter.action_type.ToLower()))
            End If
            If _filter.EMPLOYEE_ID.HasValue Then
                lst = lst.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.NEW_VALUE) Then
                lst = lst.Where(Function(f) f.NEW_VALUE.ToLower().Contains(_filter.NEW_VALUE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PERIOD_NAME) Then
                lst = lst.Where(Function(f) f.PERIOD_NAME.ToLower().Contains(_filter.PERIOD_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.OLD_VALUE) Then
                lst = lst.Where(Function(f) f.OLD_VALUE.ToLower().Contains(_filter.OLD_VALUE.ToLower()))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DeleteActionLogsAT(ByVal lstDeleteIds As List(Of Decimal)) As Integer
        Dim lstData As List(Of AT_ACTION_LOG)
        Try
            lstData = (From p In Context.AT_ACTION_LOG Where lstDeleteIds.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.AT_ACTION_LOG.DeleteObject(lstData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function LOG_AT(ByVal _param As ParamDTO,
                           ByVal log As UserLog,
                           ByVal lstEmployee As List(Of Decimal?),
                           ByVal Object_Name As String,
                           ByVal action As AT_ACTION_LOGDTO,
                           ByVal org_id As Decimal?) As Boolean
        Dim ActionId As Decimal?
        Dim action_log As New AT_ACTION_LOG
        action_log.ID = Utilities.GetNextSequence(Context, Context.AT_ACTION_LOG.EntitySet.Name)
        ActionId = action_log.ID
        action_log.USERNAME = log.Username.ToUpper
        action_log.IP = log.Ip
        action_log.ACTION_NAME = log.ActionName
        action_log.ACTION_DATE = DateTime.Now
        action_log.OBJECT_NAME = Object_Name
        action_log.COMPUTER_NAME = log.ComputerName
        action_log.ORG_ID = org_id
        action_log.EMPLOYEE_ID = action.EMPLOYEE_ID
        action_log.OLD_VALUE = action.OLD_VALUE
        action_log.PERIOD_ID = action.PERIOD_ID
        action_log.NEW_VALUE = action.NEW_VALUE
        Context.AT_ACTION_LOG.AddObject(action_log)
        If lstEmployee.Count > 0 Then
            Dim action_logOrg As AT_ACTION_ORG_LOG
            For Each emp As Decimal? In lstEmployee
                action_logOrg = New AT_ACTION_ORG_LOG
                action_logOrg.ID = Utilities.GetNextSequence(Context, Context.AT_ACTION_ORG_LOG.EntitySet.Name)
                action_logOrg.EMPLOYEE_ID = emp
                action_logOrg.ACTION_LOG_ID = ActionId
                Context.AT_ACTION_ORG_LOG.AddObject(action_logOrg)
            Next
        Else
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.INSERT_CHOSEN_LOGORG",
                             New With {.P_USERNAME = log.Username.ToUpper,
                                       .P_ORGID = _param.ORG_ID,
                                       .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                       .P_ACTION_ID = ActionId})
            End Using
        End If
        Context.SaveChanges()
        Return True
    End Function
#End Region

#Region "IPORTAL - View bảng công"

    Public Function CheckPeriod(ByVal PeriodId As Integer, ByVal EmployeeId As Decimal) As Boolean
        Try
            Dim emp As HU_EMPLOYEE
            emp = (From p In Context.HU_EMPLOYEE Where p.ID = EmployeeId).FirstOrDefault

            Dim query = (From p In Context.AT_ORG_PERIOD
                         Where p.PERIOD_ID = PeriodId And p.ORG_ID = emp.ORG_ID).FirstOrDefault


            If query IsNot Nothing Then

                If query.STATUSCOLEX = 0 Then
                    Return query.STATUSCOLEX = 0
                Else
                    Return -1
                End If
            Else
                Return (query Is Nothing)
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function
    ''' <summary>
    ''' Lấy thông tin bảng công tổng hợp
    ''' </summary>
    ''' <param name="_filter"></param>
    ''' <param name="_param"></param>
    ''' <param name="Total"></param>
    ''' <param name="PageIndex"></param>
    ''' <param name="PageSize"></param>
    ''' <param name="Sorts"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTimeSheetPortal(ByVal _filter As AT_TIME_TIMESHEET_MONTHLYDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MONTHLYDTO)
        Try

            Dim query = From p In Context.AT_TIME_TIMESHEET_MONTHLY
                        From ot In Context.AT_TIME_TIMESHEET_OT.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID And f.PERIOD_ID = p.PERIOD_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From po In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID)
                 Where (p.PERIOD_ID = _filter.PERIOD_ID)

            If _filter.IS_TERMINATE Then
                query = query.Where(Function(f) f.e.WORK_STATUS = 257)
                If _filter.WORKINGDAY.HasValue Then
                    query = query.Where(Function(f) f.e.TER_LAST_DATE <= _filter.WORKINGDAY)
                End If
            End If
            If _filter.EMPLOYEE_ID.HasValue Then
                query = query.Where(Function(f) f.p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.e.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.e.FULLNAME_VN.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                query = query.Where(Function(f) f.s.NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.WORKING_A.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_A = _filter.WORKING_A)
            End If
            If _filter.WORKING_B.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_B = _filter.WORKING_B)
            End If
            If _filter.WORKING_C.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_C = _filter.WORKING_C)
            End If
            If _filter.WORKING_D.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_D = _filter.WORKING_D)
            End If
            If _filter.WORKING_E.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_E = _filter.WORKING_E)
            End If
            If _filter.WORKING_F.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_F = _filter.WORKING_F)
            End If
            If _filter.WORKING_H.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_H = _filter.WORKING_H)
            End If
            If _filter.WORKING_J.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_J = _filter.WORKING_J)
            End If
            If _filter.WORKING_K.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_K = _filter.WORKING_K)
            End If
            If _filter.WORKING_L.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_L = _filter.WORKING_L)
            End If
            If _filter.WORKING_N.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_N = _filter.WORKING_N)
            End If
            If _filter.WORKING_O.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_O = _filter.WORKING_O)
            End If
            If _filter.WORKING_P.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_P = _filter.WORKING_P)
            End If
            If _filter.WORKING_Q.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_Q = _filter.WORKING_Q)
            End If
            If _filter.WORKING_R.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_R = _filter.WORKING_R)
            End If
            If _filter.WORKING_S.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_S = _filter.WORKING_S)
            End If
            If _filter.WORKING_T.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_T = _filter.WORKING_T)
            End If
            If _filter.WORKING_TS.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_TS = _filter.WORKING_TS)
            End If
            If _filter.WORKING_V.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_V = _filter.WORKING_V)
            End If
            If _filter.WORKING_X.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_X = _filter.WORKING_X)
            End If
            If _filter.TOTAL_WORKING.HasValue Then
                query = query.Where(Function(f) f.p.TOTAL_WORKING = _filter.TOTAL_WORKING)
            End If
            If _filter.LATE.HasValue Then
                query = query.Where(Function(f) f.p.LATE = _filter.LATE)
            End If
            If _filter.COMEBACKOUT.HasValue Then
                query = query.Where(Function(f) f.p.COMEBACKOUT = _filter.COMEBACKOUT)
            End If
            If _filter.TOTAL_W_SALARY.HasValue Then
                query = query.Where(Function(f) f.p.TOTAL_W_SALARY = _filter.TOTAL_W_SALARY)
            End If
            If _filter.TOTAL_W_NOSALARY.HasValue Then
                query = query.Where(Function(f) f.p.TOTAL_W_NOSALARY = _filter.TOTAL_W_NOSALARY)
            End If
            If _filter.WORKING_MEAL.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_MEAL = _filter.WORKING_MEAL)
            End If
            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_MONTHLYDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .ORG_ID = p.p.ORG_ID,
                                       .PERIOD_ID = p.po.ID,
                                       .PERIOD_STANDARD = p.po.PERIOD_STANDARD,
                                       .STAFF_RANK_NAME = p.s.NAME,
                                       .DECISION_START = p.p.DECISION_START,
                                       .DECISION_END = p.p.DECISION_END,
                                       .WORKING_X = p.p.WORKING_X,
                                       .WORKING_F = p.p.WORKING_F,
                                       .WORKING_E = p.p.WORKING_E,
                                       .WORKING_A = p.p.WORKING_A,
                                       .WORKING_H = p.p.WORKING_H,
                                       .WORKING_D = p.p.WORKING_D,
                                       .WORKING_C = p.p.WORKING_C,
                                       .WORKING_T = p.p.WORKING_T,
                                       .WORKING_Q = p.p.WORKING_Q,
                                       .WORKING_N = p.p.WORKING_N,
                                       .WORKING_P = p.p.WORKING_P,
                                       .WORKING_L = p.p.WORKING_L,
                                       .WORKING_R = p.p.WORKING_R,
                                       .WORKING_S = p.p.WORKING_S,
                                       .WORKING_B = p.p.WORKING_B,
                                       .WORKING_K = p.p.WORKING_K,
                                       .WORKING_J = p.p.WORKING_J,
                                       .WORKING_TS = p.p.WORKING_TS,
                                       .WORKING_O = p.p.WORKING_O,
                                       .WORKING_V = p.p.WORKING_V,
                                       .WORKING_ADD = p.p.WORKING_ADD,
                                       .WORKING_MEAL = p.p.WORKING_MEAL,
                                       .LATE = p.p.LATE,
                                       .COMEBACKOUT = p.p.COMEBACKOUT,
                                       .TOTAL_W_NOSALARY = p.p.TOTAL_W_NOSALARY,
                                       .TOTAL_W_SALARY = p.p.TOTAL_W_SALARY,
                                       .TOTAL_WORKING = p.p.TOTAL_WORKING + If(p.ot.TOTAL_FACTOR_CONVERT Is Nothing, 0, p.ot.TOTAL_FACTOR_CONVERT),
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .TOTAL_FACTOR = p.ot.TOTAL_FACTOR_CONVERT,
                                       .TOTAL_FACTOR1 = p.ot.TOTAL_FACTOR1,
                                       .TOTAL_FACTOR1_5 = p.ot.TOTAL_FACTOR1_5,
                                       .TOTAL_FACTOR2 = p.ot.TOTAL_FACTOR2,
                                       .TOTAL_FACTOR2_7 = p.ot.TOTAL_FACTOR2_7,
                                       .TOTAL_FACTOR3 = p.ot.TOTAL_FACTOR3,
                                       .TOTAL_FACTOR3_9 = p.ot.TOTAL_FACTOR3_9,
                                       .WORKING_DA = p.p.WORKING_DA})
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "Portal quan ly nghi phep, nghi bu"
    Public Function GetTotalDayOff(ByVal _filter As TotalDayOffDTO,
                                    Optional ByVal log As UserLog = Nothing) As TotalDayOffDTO
        Try
            Dim result As New TotalDayOffDTO
            Dim ListManul As New AT_TIME_MANUAL
            ListManul = (From p In Context.AT_TIME_MANUAL
                         Where (p.MORNING_ID = 251 Or p.AFTERNOON_ID = 251) And p.ID = _filter.TIME_MANUAL_ID).FirstOrDefault
            Using cls As New DataAccess.QueryData

                ' nghi bu 
                If (_filter.LEAVE_TYPE = 255) Then
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.MANAGEMENT_TOTAL_COMPENSATORY",
                                     New With {.P_EMPLOYEE_ID = _filter.EMPLOYEE_ID,
                                                .P_ID_PORTAL_REG = _filter.ID_PORTAL_REG,
                                               .P_DATE_TIME = _filter.DATE_REGISTER,
                                               .P_OUT = cls.OUT_CURSOR})
                    If dtData IsNot Nothing AndAlso dtData.Rows.Count = 1 Then
                        result.EMPLOYEE_ID = _filter.EMPLOYEE_ID
                        result.DATE_REGISTER = _filter.DATE_REGISTER
                        result.LEAVE_TYPE = _filter.LEAVE_TYPE
                        result.TOTAL_DAY = dtData.Rows(0)("TONG_NB")
                        result.USED_DAY = dtData.Rows(0)("DA_NB")
                        result.REST_DAY = dtData.Rows(0)("NB_CON_LAI")
                        result.LIMIT_DAY = If(IsDBNull(dtData.Rows(0)("LIMIT_DAY")), Nothing, dtData.Rows(0)("LIMIT_DAY"))
                        result.LIMIT_YEAR = If(IsDBNull(dtData.Rows(0)("LIMIT_YEAR")), Nothing, dtData.Rows(0)("LIMIT_YEAR"))
                    End If
                    'nghi phep
                ElseIf (_filter.LEAVE_TYPE = 251) Then
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.MANAGEMENT_TOTAL_ENTITLEMENT",
                                    New With {.P_EMPLOYEE_ID = _filter.EMPLOYEE_ID,
                                              .P_ID_PORTAL_REG = _filter.ID_PORTAL_REG,
                                              .P_DATE_TIME = _filter.DATE_REGISTER,
                                              .P_OUT = cls.OUT_CURSOR})
                    If dtData IsNot Nothing AndAlso dtData.Rows.Count = 1 Then
                        result.EMPLOYEE_ID = _filter.EMPLOYEE_ID
                        result.DATE_REGISTER = _filter.DATE_REGISTER
                        result.LEAVE_TYPE = _filter.LEAVE_TYPE
                        result.TOTAL_DAY = dtData.Rows(0)("PHEP_TRONG_NAM")
                        result.USED_DAY = dtData.Rows(0)("PHEP_DA_NGHI")
                        result.REST_DAY = dtData.Rows(0)("PHEP_CON_LAI")
                        result.LIMIT_DAY = If(IsDBNull(dtData.Rows(0)("LIMIT_DAY")), Nothing, dtData.Rows(0)("LIMIT_DAY"))
                        result.LIMIT_YEAR = If(IsDBNull(dtData.Rows(0)("LIMIT_YEAR")), Nothing, dtData.Rows(0)("LIMIT_YEAR"))

                        result.PREV_HAVE = dtData.Rows(0)("PREV_HAVE")
                        result.PREV_USED = dtData.Rows(0)("PREV_USED")
                        result.PREVTOTAL_HAVE = dtData.Rows(0)("PREVTOTAL_HAVE")
                        result.SENIORITYHAVE = dtData.Rows(0)("SENIORITYHAVE")
                        result.TOTAL_HAVE1 = dtData.Rows(0)("TOTAL_HAVE1")
                        result.TIME_OUTSIDE_COMPANY = dtData.Rows(0)("TIME_OUTSIDE_COMPANY")
                    End If
                End If
            End Using
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GET_TIME_MANUAL(ByVal _filter As TotalDayOffDTO,
                                    Optional ByVal log As UserLog = Nothing) As TotalDayOffDTO
        Try
            Dim result As New TotalDayOffDTO
            Using cls As New DataAccess.QueryData

                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_TIME_MANUAL",
                                 New With {.P_ID = _filter.LEAVE_TYPE,
                                           .P_DATE_TIME = _filter.DATE_REGISTER,
                                           .P_EMPLOYEE_ID = _filter.EMPLOYEE_ID,
                                           .P_OUT = cls.OUT_CURSOR})
                If dtData IsNot Nothing AndAlso dtData.Rows.Count = 1 Then
                    result.LIMIT_DAY = If(IsDBNull(dtData.Rows(0)("LIMIT_DAY")), Nothing, dtData.Rows(0)("LIMIT_DAY"))
                    result.LIMIT_YEAR = If(IsDBNull(dtData.Rows(0)("LIMIT_YEAR")), Nothing, dtData.Rows(0)("LIMIT_YEAR"))
                    result.USED_DAY = dtData.Rows(0)("PHEP_DA_NGHI")
                End If
            End Using
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetHistoryLeave(ByVal _filter As HistoryLeaveDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "REGDATE DESC", Optional ByVal log As UserLog = Nothing) As List(Of HistoryLeaveDTO)
        Try
            Dim result As New List(Of HistoryLeaveDTO)
            Using cls As New DataAccess.QueryData
                'lich su
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.MANAGEMENT_HISTORY_LEAVE",
                                 New With {.P_EMPLOYEE_ID = _filter.EMPLOYEE_ID,
                                           .P_FROM_DATE = _filter.FROM_DATE,
                                           .P_TO_DATE = _filter.TO_DATE,
                                           .P_OUT = cls.OUT_CURSOR})
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New HistoryLeaveDTO With {
                                 .EMPLOYEE_ID = _filter.EMPLOYEE_ID,
                                 .EMPLOYEE_CODE = dr("EMPLOYEE_CODE"),
                                 .EMPLOYEE_NAME = dr("EMPLOYEE_NAME"),
                                 .REGDATE = If(dr("REG_DATE") IsNot Nothing, ToDate(dr("REG_DATE")), Nothing),
                                 .FROM_DATE = If(dr("FROM_DATE") IsNot Nothing, ToDate(dr("FROM_DATE")), Nothing),
                                  .TO_DATE = If(dr("TO_DATE") IsNot Nothing, ToDate(dr("TO_DATE")), Nothing),
                                  .FROM_HOUR = If(dr("FROM_HOUR") IsNot Nothing, ToDate(dr("FROM_HOUR")), Nothing),
                                  .TO_HOUR = If(dr("TO_HOUR") IsNot Nothing, ToDate(dr("TO_HOUR")), Nothing),
                                 .SIGN_ID = Decimal.Parse(dr("SIGN_ID")),
                                 .SIGN_CODE = dr("SIGN_CODE"),
                                 .SIGN_NAME = dr("SIGN_NAME"),
                                 .APPROVE_DATE = If(dr("APPROVE_DATE") IsNot Nothing, ToDate(dr("APPROVE_DATE")), Nothing),
                                 .APPROVE_STATUS = Decimal.Parse(dr("APPROVE_STATUS"))})
                    Next
                End If
            End Using
            If _filter.SIGN_ID.HasValue Then
                result = result.Where(Function(f) f.SIGN_ID = _filter.SIGN_ID)
            End If
            'result = result.OrderBy(Sorts)
            'Total = result.Count
            'result = result.Skip(PageIndex * PageSize).Take(PageSize)
            Return result.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region
#Region "Check nhân viên có thuộc dự án khi chấm công"
    Public Function CheckExistEm_Pro(ByVal lstID As Decimal, ByVal FromDate As Date, ByVal ToDate As Date, ByVal IDPro As Decimal) As Boolean
        Return True
    End Function
#End Region

#Region "OT"
    Public Function GetOtRegistration(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of AT_OT_REGISTRATIONDTO)
        Try
            Using cls As New DataAccess.QueryData
                Dim userIdOrMngId As Decimal
                Dim dt As DataTable
                Dim obj
                If _filter.P_MANAGER_ID.ToString <> "" Then
                    userIdOrMngId = _filter.P_MANAGER_ID
                    obj = New With {.P_EMPLOYEE_APP_ID = userIdOrMngId,
                                    .P_STATUS = _filter.STATUS,
                                    .P_FROM_DATE = _filter.REGIST_DATE_FROM,
                                    .P_TO_DATE = _filter.REGIST_DATE_TO,
                                    .P_RESULT = cls.OUT_CURSOR}
                    dt = cls.ExecuteStore("PKG_AT_PROCESS.PRS_GETOT_BY_APPROVE", obj)
                Else
                    userIdOrMngId = Decimal.Parse(_filter.P_USER)
                    obj = New With {.P_EMPLOYEE_APP_ID = userIdOrMngId,
                                    .P_STATUS = _filter.STATUS,
                                    .P_FROM_DATE = _filter.REGIST_DATE_FROM,
                                    .P_TO_DATE = _filter.REGIST_DATE_TO,
                                    .P_REG_DATE = _filter.REGIST_DATE,
                                    .P_RESULT = cls.OUT_CURSOR}
                    dt = cls.ExecuteStore("PKG_AT_PROCESS.PRS_GETOT_BY_EMPLOYEE", obj)
                End If
                Dim lst As New List(Of AT_OT_REGISTRATIONDTO)
                For Each row As DataRow In dt.Rows
                    Dim dto As New AT_OT_REGISTRATIONDTO
                    dto.ID = row("ID")
                    dto.EMPLOYEE_ID = row("EMPLOYEE_ID")
                    dto.EMPLOYEE_CODE = row("EMPLOYEE_CODE")
                    dto.FULLNAME = row("FULLNAME_VN")
                    dto.DEPARTMENT_ID = row("ORG_ID")
                    dto.DEPARTMENT_NAME = row("ORG_NAME")
                    dto.TITLE_ID = row("TITLE_ID")
                    dto.TITLE_NAME = row("TITLE_NAME")
                    dto.REGIST_DATE = row("REGIST_DATE")
                    dto.SIGN_ID = row("SIGN_ID")
                    dto.SIGN_CODE = row("SIGN_CODE")
                    dto.OT_TYPE_ID = row("OT_TYPE_ID")
                    dto.OT_TYPE_NAME = row("OT_TYPE_NAME")
                    dto.ID_REGGROUP = row("ID_REGGROUP")
                    dto.TOTAL_OT = row("TOTAL_OT")
                    dto.OT_100 = row("OT_100")
                    dto.OT_150 = row("OT_150")
                    dto.OT_200 = row("OT_200")
                    dto.OT_210 = row("OT_210")
                    dto.OT_270 = row("OT_270")
                    dto.OT_300 = row("OT_300")
                    dto.OT_370 = row("OT_370")
                    dto.FROM_AM = If(row("FROM_AM").ToString <> "", row("FROM_AM"), Nothing)
                    dto.FROM_AM_MN = If(row("FROM_AM_MN").ToString <> "", row("FROM_AM_MN"), Nothing)
                    dto.TO_AM = If(row("TO_AM").ToString <> "", row("TO_AM"), Nothing)
                    dto.TO_AM_MN = If(row("TO_AM_MN").ToString <> "", row("TO_AM_MN"), Nothing)
                    dto.FROM_PM = If(row("FROM_PM").ToString <> "", row("FROM_PM"), Nothing)
                    dto.FROM_PM_MN = If(row("FROM_PM_MN").ToString <> "", row("FROM_PM_MN"), Nothing)
                    dto.TO_PM = If(row("TO_PM").ToString <> "", row("TO_PM"), Nothing)
                    dto.TO_PM_MN = If(row("TO_PM_MN").ToString <> "", row("TO_PM_MN"), Nothing)
                    dto.STATUS = row("STATUS")
                    dto.STATUS_NAME = row("STATUS_NAME")
                    dto.REASON = If(row("REASON").ToString <> "", row("REASON"), Nothing)
                    dto.NOTE = If(row("NOTE").ToString <> "", row("NOTE"), Nothing)
                    dto.IS_DELETED = row("IS_DELETED")
                    dto.CREATED_BY = row("CREATED_BY")
                    dto.CREATED_DATE = row("CREATED_DATE")
                    dto.CREATED_LOG = row("CREATED_LOG")
                    dto.MODIFIED_BY = row("MODIFIED_BY")
                    dto.MODIFIED_DATE = row("MODIFIED_DATE")
                    dto.MODIFIED_LOG = row("MODIFIED_LOG")
                    dto.MODIFIED_NAME = row("FULLNAME")
                    lst.Add(dto)
                Next
                If _filter.REGIST_DATE_FROM.HasValue And _filter.REGIST_DATE_TO.HasValue Then
                    lst = (From p In lst.AsEnumerable Where p.REGIST_DATE <= _filter.REGIST_DATE_TO And p.REGIST_DATE >= _filter.REGIST_DATE_FROM
                           Select p).ToList
                Else
                    If _filter.REGIST_DATE_FROM.HasValue Then
                        lst = (From f In lst.AsEnumerable Where f.REGIST_DATE >= _filter.REGIST_DATE_FROM
                               Select f).ToList
                    ElseIf _filter.REGIST_DATE_TO.HasValue Then
                        lst = (From f In lst.AsEnumerable Where f.REGIST_DATE <= _filter.REGIST_DATE_TO
                               Select f).ToList
                    End If
                End If
                If _filter.STATUS > 0 Then
                    lst = (From f In lst.AsEnumerable Where f.STATUS = _filter.STATUS
                               Select f).ToList
                End If

                If Not String.IsNullOrEmpty(_filter.STATUS_NAME) Then
                    lst = (From f In lst.AsEnumerable Where f.STATUS_NAME.ToLower().Contains(_filter.STATUS_NAME.ToLower())
                               Select f).ToList
                End If

                If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                    lst = (From f In lst.AsEnumerable Where f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower())
                               Select f).ToList
                End If

                If Not String.IsNullOrEmpty(_filter.FULLNAME) Then
                    lst = (From f In lst.AsEnumerable Where f.FULLNAME.ToLower().Contains(_filter.FULLNAME.ToLower())
                               Select f).ToList
                End If

                If Not String.IsNullOrEmpty(_filter.DEPARTMENT_NAME) Then
                    lst = (From f In lst.AsEnumerable Where f.DEPARTMENT_NAME.ToLower().Contains(_filter.DEPARTMENT_NAME.ToLower())
                               Select f).ToList
                End If

                If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                    lst = (From f In lst.AsEnumerable Where f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower())
                               Select f).ToList
                End If

                'If _filter.REGIST_DATE.HasValue Then
                '    lst = (From f In lst.AsEnumerable Where f.REGIST_DATE >= _filter.REGIST_DATE
                '               Select f).ToList
                'End If

                If Not String.IsNullOrEmpty(_filter.SIGN_CODE) Then
                    lst = (From f In lst.AsEnumerable Where f.SIGN_CODE.ToLower().Contains(_filter.SIGN_CODE.ToLower())
                               Select f).ToList
                End If

                If Not String.IsNullOrEmpty(_filter.OT_TYPE_NAME) Then
                    lst = (From f In lst.AsEnumerable Where f.OT_TYPE_NAME.ToLower().Contains(_filter.OT_TYPE_NAME.ToLower())
                               Select f).ToList
                End If

                If Not _filter.P_MANAGER_ID.HasValue And _filter.EMPLOYEE_ID > 0 Then
                    lst = (From f In lst.AsEnumerable Where f.EMPLOYEE_ID = _filter.EMPLOYEE_ID
                               Select f).ToList
                End If
                If _filter.ID > 0 Then
                    lst = (From f In lst.AsEnumerable Where f.ID = _filter.ID
                               Select f).ToList
                End If
                'lst = lst.Where(Function(f) f.IS_DELETED = 0)

                If _filter.OT_100.HasValue Then
                    lst = (From f In lst.AsEnumerable Where f.OT_100 = _filter.OT_100
                               Select f).ToList
                End If

                If _filter.OT_100.HasValue Then
                    lst = (From f In lst.AsEnumerable Where f.OT_100 = _filter.OT_100
                               Select f).ToList
                End If

                If _filter.OT_150.HasValue Then
                    lst = (From f In lst.AsEnumerable Where f.OT_150 = _filter.OT_150
                               Select f).ToList
                End If

                If _filter.OT_200.HasValue Then
                    lst = (From f In lst.AsEnumerable Where f.OT_200 = _filter.OT_200
                               Select f).ToList
                End If

                If _filter.OT_210.HasValue Then
                    lst = (From f In lst.AsEnumerable Where f.OT_210 = _filter.OT_210
                               Select f).ToList
                End If

                If _filter.OT_270.HasValue Then
                    lst = (From f In lst.AsEnumerable Where f.OT_270 = _filter.OT_270
                               Select f).ToList
                End If

                If _filter.OT_300.HasValue Then
                    lst = (From f In lst.AsEnumerable Where f.OT_300 = _filter.OT_300
                               Select f).ToList
                End If

                If _filter.OT_370.HasValue Then
                    lst = (From f In lst.AsEnumerable Where f.OT_370 = _filter.OT_370
                               Select f).ToList
                End If

                If Not String.IsNullOrEmpty(_filter.NOTE) Then
                    lst = (From f In lst.AsEnumerable Where f.NOTE.ToLower().Contains(_filter.NOTE.ToLower())
                               Select f).ToList
                End If

                If Not String.IsNullOrEmpty(_filter.REASON) Then
                    lst = (From f In lst.AsEnumerable Where f.REASON.ToLower().Contains(_filter.REASON.ToLower())
                               Select f).ToList
                End If

                If Not String.IsNullOrEmpty(_filter.STATUS_NAME) Then
                    lst = (From f In lst.AsEnumerable Where f.STATUS_NAME.ToLower().Contains(_filter.STATUS_NAME.ToLower())
                               Select f).ToList
                End If

                If Not String.IsNullOrEmpty(_filter.MODIFIED_BY) Then
                    lst = (From f In lst.AsEnumerable Where f.MODIFIED_BY.ToLower().Contains(_filter.MODIFIED_BY.ToLower())
                               Select f).ToList
                End If

                If _filter.MODIFIED_DATE.HasValue Then
                    lst = (From f In lst.AsEnumerable Where f.MODIFIED_DATE = _filter.MODIFIED_DATE
                               Select f).ToList
                End If

                lst = (From f In lst.AsEnumerable
                       Order By f.CREATED_DATE Descending
                       Select f).ToList
                Total = lst.Count
                lst = (From f In lst.AsEnumerable
                      Skip PageIndex * PageSize
                      Take PageSize
                      Select f).ToList
                'lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
                Return lst.ToList
            End Using
            Total = 0
            Return New List(Of AT_OT_REGISTRATIONDTO)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function GetOtRegistrationForTimeSheet(ByVal _filter As AT_OT_REGISTRATIONDTO) As List(Of AT_OT_REGISTRATIONDTO)
        Try
            Dim query = From r In Context.AT_OT_REGISTRATION
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = r.EMPLOYEE_ID)
                        Where r.STATUS = PortalStatus.ApprovedByLM
            Dim lst = query.Select(Function(p) New AT_OT_REGISTRATIONDTO With {
                                       .ID = p.r.ID,
                                       .EMPLOYEE_ID = p.e.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .FULLNAME = p.e.FULLNAME_VN,
                                       .DEPARTMENT_ID = p.e.ORG_ID,
                                       .REGIST_DATE = p.r.REGIST_DATE,
                                       .SIGN_ID = p.r.SIGN_ID,
                                       .SIGN_CODE = p.r.SIGN_CODE,
                                       .OT_TYPE_ID = p.r.OT_TYPE_ID,
                                       .TOTAL_OT = If(p.r.TOTAL_OT Is Nothing, 0, p.r.TOTAL_OT),
                                       .OT_100 = If(p.r.OT_100 Is Nothing, 0, p.r.OT_100),
                                       .OT_150 = If(p.r.OT_150 Is Nothing, 0, p.r.OT_150),
                                       .OT_200 = If(p.r.OT_200 Is Nothing, 0, p.r.OT_200),
                                       .OT_210 = If(p.r.OT_210 Is Nothing, 0, p.r.OT_210),
                                       .OT_270 = If(p.r.OT_270 Is Nothing, 0, p.r.OT_270),
                                       .OT_300 = If(p.r.OT_300 Is Nothing, 0, p.r.OT_300),
                                       .OT_370 = If(p.r.OT_370 Is Nothing, 0, p.r.OT_370),
                                       .FROM_AM = p.r.FROM_AM,
                                       .FROM_AM_MN = p.r.FROM_AM_MN,
                                       .TO_AM = p.r.TO_AM,
                                       .TO_AM_MN = p.r.TO_AM_MN,
                                       .FROM_PM = p.r.FROM_PM,
                                       .FROM_PM_MN = p.r.FROM_PM_MN,
                                       .TO_PM = p.r.TO_PM,
                                       .TO_PM_MN = p.r.TO_PM_MN,
                                       .STATUS = p.r.STATUS,
                                       .REASON = p.r.REASON,
                                       .NOTE = p.r.NOTE,
                                       .IS_DELETED = p.r.IS_DELETED,
                                       .CREATED_BY = p.r.CREATED_BY,
                                       .CREATED_DATE = p.r.CREATED_DATE,
                                       .CREATED_LOG = p.r.CREATED_LOG,
                                       .MODIFIED_DATE = p.r.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.r.MODIFIED_LOG})
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function
    Public Function InsertOtRegistration(ByVal obj As AT_OT_REGISTRATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New AT_OT_REGISTRATION
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.AT_OT_REGISTRATION.EntitySet.Name)
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.OT_TYPE_ID = obj.OT_TYPE_ID
            objData.REASON = obj.REASON
            objData.NOTE = obj.NOTE
            objData.IS_DELETED = obj.IS_DELETED
            objData.OT_100 = obj.OT_100
            objData.OT_150 = obj.OT_150
            objData.OT_200 = obj.OT_200
            objData.OT_210 = obj.OT_210
            objData.OT_270 = obj.OT_270
            objData.OT_300 = obj.OT_300
            objData.OT_370 = obj.OT_370
            objData.FROM_AM = obj.FROM_AM
            objData.FROM_AM_MN = obj.FROM_AM_MN
            objData.TO_AM = obj.TO_AM
            objData.TO_AM_MN = obj.TO_AM_MN
            objData.FROM_PM = obj.FROM_PM
            objData.FROM_PM_MN = obj.FROM_PM_MN
            objData.TO_PM = obj.TO_PM
            objData.TO_PM_MN = obj.TO_PM_MN
            objData.REGIST_DATE = obj.REGIST_DATE
            objData.SIGN_CODE = obj.SIGN_CODE
            objData.SIGN_ID = obj.SIGN_ID
            objData.STATUS = obj.STATUS
            objData.TOTAL_OT = obj.TOTAL_OT
            objData.ID_REGGROUP = Utilities.GetNextSequence(Context, Context.AT_PORTAL_APP.EntitySet.Name)
            Context.AT_OT_REGISTRATION.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyotRegistration(ByVal obj As AT_OT_REGISTRATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New AT_OT_REGISTRATION With {.ID = obj.ID}
        Try
            Context.AT_OT_REGISTRATION.Attach(objData)
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.OT_TYPE_ID = obj.OT_TYPE_ID
            objData.REASON = obj.REASON
            objData.NOTE = obj.NOTE
            objData.IS_DELETED = obj.IS_DELETED
            objData.OT_100 = obj.OT_100
            objData.OT_150 = obj.OT_150
            objData.OT_200 = obj.OT_200
            objData.OT_210 = obj.OT_210
            objData.OT_270 = obj.OT_270
            objData.OT_300 = obj.OT_300
            objData.OT_370 = obj.OT_370
            objData.FROM_AM = obj.FROM_AM
            objData.FROM_AM_MN = obj.FROM_AM_MN
            objData.TO_AM = obj.TO_AM
            objData.TO_AM_MN = obj.TO_AM_MN
            objData.FROM_PM = obj.FROM_PM
            objData.FROM_PM_MN = obj.FROM_PM_MN
            objData.TO_PM = obj.TO_PM
            objData.TO_PM_MN = obj.TO_PM_MN
            objData.REGIST_DATE = obj.REGIST_DATE
            objData.SIGN_CODE = obj.SIGN_CODE
            objData.SIGN_ID = obj.SIGN_ID
            objData.STATUS = obj.STATUS
            objData.TOTAL_OT = obj.TOTAL_OT
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function SendApproveOTRegistration(ByVal obj As List(Of AT_OT_REGISTRATIONDTO), ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                For Each item In obj
                    Dim dtCheckSendApprove As DataTable = cls.ExecuteStore("PKG_AT_ATTENDANCE_PORTAL.CHECK_SEND_APPROVE",
                                                                           New With {.P_ID = item.ID.ToString, .P_OUT_CUR = cls.OUT_CURSOR})
                    Dim processType As String = "OVERTIME"
                    Dim periodId As Integer = Int32.Parse(dtCheckSendApprove.Rows(0)("PERIOD_ID").ToString())
                    Dim signId As Integer = Int32.Parse(dtCheckSendApprove.Rows(0)("SIGN_ID").ToString())
                    Dim totalHours As Decimal = Decimal.Parse(dtCheckSendApprove.Rows(0)("SUMVAL").ToString())
                    Dim IdGroup1 As Decimal = Decimal.Parse(dtCheckSendApprove.Rows(0)("ID_REGGROUP").ToString())
                    Dim priProcessApp = New With {.P_EMPLOYEE_ID = item.EMPLOYEE_ID, .P_PERIOD_ID = periodId, .P_PROCESS_TYPE = processType, .P_TOTAL_HOURS = totalHours, .P_TOTAL_DAY = 0, .P_SIGN_ID = signId, .P_ID_REGGROUP = IdGroup1, .P_RESULT = cls.OUT_NUMBER}
                    Dim store = cls.ExecuteStore("PKG_AT_PROCESS.PRI_PROCESS_APP", priProcessApp)
                    Dim outNumber As Integer = Int32.Parse(priProcessApp.P_RESULT)
                    If outNumber <> 0 Then
                        Return False
                    End If
                Next
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function ApproveOtRegistration(ByVal obj As List(Of AT_OT_REGISTRATIONDTO), ByVal empId As Decimal, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                For Each item In obj
                    Dim processType As String = "OVERTIME"
                    Dim periodId As Integer = Context.AT_PERIOD.Where(Function(f) f.START_DATE <= item.REGIST_DATE AndAlso item.REGIST_DATE <= f.END_DATE).Select(Function(f) f.ID).FirstOrDefault
                    Dim IdGroup1 As Decimal = item.ID_REGGROUP
                    Dim priProcess = New With {.P_EMPLOYEE_APP_ID = empId, .P_EMPLOYEE_ID = item.EMPLOYEE_ID, .P_PE_PERIOD_ID = periodId, .P_STATUS_ID = item.STATUS, .P_PROCESS_TYPE = processType, .P_NOTES = item.REASON, .P_ID_REGGROUP = IdGroup1, .P_RESULT = cls.OUT_NUMBER}
                    Dim store = cls.ExecuteStore("PKG_AT_PROCESS.PRI_PROCESS", priProcess)
                    Dim outNumber As Integer = Int32.Parse(priProcess.P_RESULT)
                    If outNumber <> 0 Then
                        Return False
                    End If
                Next
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function ValidateOtRegistration(ByVal _validate As AT_OT_REGISTRATIONDTO)
        Dim query
        Try
            If _validate.ID <> 0 Then
                query = (From p In Context.AT_OT_REGISTRATION
                         Where p.ID <> _validate.ID _
                           And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                           And p.REGIST_DATE = _validate.REGIST_DATE _
                           And p.IS_DELETED = 0).FirstOrDefault()
                Return (Not query Is Nothing)
            Else
                query = (From p In Context.AT_OT_REGISTRATION
                         Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                         And p.REGIST_DATE = _validate.REGIST_DATE _
                         And p.IS_DELETED = 0 _
                         And p.SIGN_ID.HasValue).FirstOrDefault()
                Return (Not query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function HRReviewOtRegistration(ByVal lst As List(Of Decimal), ByVal log As UserLog) As Boolean
        Try
            Dim listOTRegistration = (From record In Context.AT_OT_REGISTRATION Where lst.Contains(record.ID))
            For Each item In listOTRegistration
                Context.AT_OT_REGISTRATION.Attach(item)
                item.HR_REVIEW = True
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function DeleteOtRegistration(ByVal lstId As List(Of Decimal)) As Boolean
        Try
            Dim deletedLeavePlan = (From record In Context.AT_OT_REGISTRATION Where lstId.Contains(record.ID))
            For Each item In deletedLeavePlan
                Context.AT_OT_REGISTRATION.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GET_AT_PORTAL_REG(ByVal P_ID As Decimal, ByVal P_EMPLOYEE As Decimal, ByVal P_DATE_TIME As Date) As DataTable
        Try
            Dim dtData As DataTable

            If P_EMPLOYEE = 0 Then
                Dim LstRegister = (From p In Context.AT_PORTAL_REG
                             Where p.ID = P_ID Select p).FirstOrDefault

                P_EMPLOYEE = LstRegister.ID_EMPLOYEE
                P_DATE_TIME = LstRegister.FROM_DATE
            End If

            Using cls As New DataAccess.QueryData

                dtData = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_AT_PORTAL_REG",
                                 New With {.P_ID = P_ID,
                                           .P_EMPLOYEE = P_EMPLOYEE,
                                           .P_DATE_TIME = P_DATE_TIME,
                                           .P_OUT = cls.OUT_CURSOR})
            End Using
            Return dtData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function GET_AT_PORTAL_REG_OT(ByVal P_ID As Decimal, ByVal P_EMPLOYEE As Decimal, ByVal P_DATE_TIME As Date) As DataTable
        Try
            Dim dtData As DataTable

            If P_EMPLOYEE = 0 Then
                Dim LstRegister = (From p In Context.AT_PORTAL_REG
                             Where p.ID = P_ID Select p).FirstOrDefault

                P_EMPLOYEE = LstRegister.ID_EMPLOYEE
                P_DATE_TIME = LstRegister.FROM_DATE
            End If

            Using cls As New DataAccess.QueryData

                dtData = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_AT_PORTAL_REG_OT",
                                 New With {.P_ID = P_ID,
                                           .P_EMPLOYEE = P_EMPLOYEE,
                                           .P_DATE_TIME = P_DATE_TIME,
                                           .P_OUT = cls.OUT_CURSOR})
            End Using
            Return dtData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region
#Region "cham cong"
    Public Function CheckTimeSheetApproveVerify(ByVal obj As List(Of AT_PROCESS_DTO), ByVal type As String, ByRef itemError As AT_PROCESS_DTO) As Boolean
        Try
            Dim checkData = Nothing
            'For Each it In obj
            '    If type = "OT" Then
            '        checkData = (From p In Context.AT_TIMESHEET
            '                     Where p.EMPLOYEE_ID = it.EMPLOYEE_ID And p.YEAR_ID = it.FROM_DATE.Value.Year And p.MONTH_ID = it.FROM_DATE.Value.Month _
            '                           And (p.STATUS = PortalStatus.ApprovedByLM Or p.STATUS = PortalStatus.VerifiedByHR Or p.STATUS = PortalStatus.WaitingForApproval)).FirstOrDefault()
            '        If checkData IsNot Nothing Then
            '            itemError = it
            '            Return False
            '        End If
            '    ElseIf type = "LEAVE" Then
            '        checkData = (From p In Context.AT_TIMESHEET
            '                     Where p.EMPLOYEE_ID = it.EMPLOYEE_ID And ((p.YEAR_ID = it.FROM_DATE.Value.Year And p.MONTH_ID = it.FROM_DATE.Value.Month)) _
            '                           And (p.STATUS = PortalStatus.ApprovedByLM Or p.STATUS = PortalStatus.VerifiedByHR Or p.STATUS = PortalStatus.WaitingForApproval)).FirstOrDefault()
            '        If checkData IsNot Nothing Then
            '            itemError = it
            '            itemError.TO_DATE = Nothing
            '            Return False
            '        End If

            '        checkData = (From p In Context.AT_TIMESHEET
            '                     Where p.EMPLOYEE_ID = it.EMPLOYEE_ID And (p.YEAR_ID = it.TO_DATE.Value.Year And p.MONTH_ID = it.TO_DATE.Value.Month) _
            '                           And (p.STATUS = PortalStatus.ApprovedByLM Or p.STATUS = PortalStatus.VerifiedByHR Or p.STATUS = PortalStatus.WaitingForApproval)).FirstOrDefault()
            '        If checkData IsNot Nothing Then
            '            itemError = it
            '            itemError.FROM_DATE = Nothing
            '            Return False
            '        End If
            '    End If
            'Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#Region "SHIFT CYCLE"
    Public Function GetShiftCycle(ByVal _filter As AT_SHIFTCYCLEDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of AT_SHIFTCYCLEDTO)
        Try
            Dim query = From p In Context.AT_SHIFTCYCLE
                        From s In Context.AT_SHIFT.Where(Function(f) f.ID = p.SHIFT_ID)
                        From e In Context.AT_FML.Where(Function(f) f.ID = p.FML01_ID).DefaultIfEmpty
                        Where e.ACTFLG = "A"

            Dim lst = query.Select(Function(p) New AT_SHIFTCYCLEDTO With {
                                       .ID = p.p.ID,
                                       .SHIFT_ID = p.p.SHIFT_ID,
                                       .SHIFT_NAME = p.s.NAME_VN,
                                       .STARTDATE = p.p.STARTDATE,
                                       .ENDDATE = p.p.ENDDATE,
                                       .NOTE = p.p.NOTE,
                                       .IS_DELETED = p.p.IS_DELETED,
                                       .FML01_ID = p.p.FML01_ID,
                                       .FML_NAME = "[" & p.e.CODE & "] " & p.e.NAME_VN,
                                       .FML02_ID = p.p.FML02_ID,
                                       .FML03_ID = p.p.FML03_ID,
                                       .FML04_ID = p.p.FML04_ID,
                                       .FML05_ID = p.p.FML05_ID,
                                       .FML06_ID = p.p.FML06_ID,
                                       .FML07_ID = p.p.FML07_ID,
                                       .FML08_ID = p.p.FML08_ID,
                                       .FML09_ID = p.p.FML09_ID,
                                       .FML10_ID = p.p.FML10_ID,
                                       .FML11_ID = p.p.FML11_ID,
                                       .FML12_ID = p.p.FML12_ID,
                                       .FML13_ID = p.p.FML13_ID,
                                       .FML14_ID = p.p.FML14_ID,
                                       .FML15_ID = p.p.FML15_ID,
                                       .FML16_ID = p.p.FML16_ID,
                                       .FML17_ID = p.p.FML17_ID,
                                       .FML18_ID = p.p.FML18_ID,
                                       .FML19_ID = p.p.FML19_ID,
                                       .FML20_ID = p.p.FML20_ID,
                                       .FML21_ID = p.p.FML21_ID,
                                       .FML22_ID = p.p.FML22_ID,
                                       .FML23_ID = p.p.FML23_ID,
                                       .FML24_ID = p.p.FML24_ID,
                                       .FML25_ID = p.p.FML25_ID,
                                       .FML26_ID = p.p.FML26_ID,
                                       .FML27_ID = p.p.FML27_ID,
                                       .FML28_ID = p.p.FML28_ID,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.SHIFT_NAME) Then
                lst = lst.Where(Function(f) f.SHIFT_NAME.ToLower().Contains(_filter.SHIFT_NAME.ToLower()))
            End If
            If _filter.SHIFT_ID.HasValue Then
                lst = lst.Where(Function(f) f.SHIFT_ID = _filter.SHIFT_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.FML_NAME) Then
                lst = lst.Where(Function(f) f.FML_NAME.ToLower().Contains(_filter.FML_NAME.ToLower()))
            End If
            If _filter.FML01_ID.HasValue Then
                lst = lst.Where(Function(f) f.FML01_ID = _filter.FML01_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If _filter.STARTDATE.HasValue Then
                lst = lst.Where(Function(f) f.STARTDATE >= _filter.STARTDATE)
            End If
            If _filter.ENDDATE.HasValue Then
                lst = lst.Where(Function(f) f.ENDDATE <= _filter.ENDDATE)
            End If
            If _filter.ID > 0 Then
                lst = lst.Where(Function(f) f.ID = _filter.ID)
            End If
            lst = lst.Where(Function(f) f.IS_DELETED = 0)

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function
    Public Function GetEmployeeShifts(ByVal employee_Id As Decimal, ByVal fromDate As Date, ByVal toDate As Date) As List(Of EMPLOYEE_SHIFT_DTO)
        Try
            Dim query = From p In Context.AT_WORKSIGN
                         From s In Context.AT_SHIFT.Where(Function(f) f.ID = p.SHIFT_ID)
                         Where p.EMPLOYEE_ID = employee_Id And p.WORKINGDAY >= fromDate And p.WORKINGDAY <= toDate
                         Select New EMPLOYEE_SHIFT_DTO With {
                                                                .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                                .EFFECTIVEDATE = p.WORKINGDAY,
                                                                .ID_SIGN = s.ID,
                                                                .SIGN_CODE = s.CODE
                                                            }

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
        Finally
            _isAvailable = True
        End Try
    End Function
#End Region
End Class