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
    Public Function ValidateLeaveSheetDetail(ByVal objValidate As AT_LEAVESHEETDTO) As Decimal
        Try

            Dim Date_from As Date = If(IsDate(objValidate.LEAVE_FROM), objValidate.LEAVE_FROM, DateTime.Now)
            Dim Date_to As Date = If(IsDate(objValidate.LEAVE_TO), objValidate.LEAVE_TO, DateTime.Now)
            ''check nghi theo buoi trong 1 ngay
            If DateDiff(DateInterval.Day, Date_from, Date_to) = 0 Then
                Dim c = (From p In Context.AT_LEAVESHEET Where p.LEAVE_FROM = objValidate.LEAVE_FROM And p.LEAVE_TO = objValidate.LEAVE_TO _
                     And p.EMPLOYEE_ID = objValidate.EMPLOYEE_ID And (p.STATUS = 1 Or p.STATUS = 0 Or p.STATUS = 8001) _
                     And (p.FROM_SESSION = objValidate.FROM_SESSION Or p.TO_SESSION = objValidate.TO_SESSION) And p.ID <> objValidate.ID)
                Dim c2 = (From p In Context.AT_LEAVESHEET Where p.LEAVE_FROM = objValidate.LEAVE_FROM And p.LEAVE_TO = objValidate.LEAVE_TO _
                         And p.EMPLOYEE_ID = objValidate.EMPLOYEE_ID And (p.STATUS = 1 Or p.STATUS = 0 Or p.STATUS = 8001) _
                         And p.FROM_SESSION <> objValidate.FROM_SESSION And p.TO_SESSION <> objValidate.TO_SESSION And p.ID <> objValidate.ID)
                If c.Count > 0 Then
                    Return 1
                ElseIf c2.Count > 0 Then
                    Return 0
                End If
            End If

            '' kiem tr nghi theo buoi neu ngay bat dau trung voi ngay ket thuc truoc do
            Dim checkSession = (From p In Context.AT_LEAVESHEET Where p.LEAVE_TO = objValidate.LEAVE_FROM And p.EMPLOYEE_ID = objValidate.EMPLOYEE_ID And p.ID <> objValidate.ID).FirstOrDefault
            If checkSession IsNot Nothing Then
                Dim ss1 = (From p In Context.OT_OTHER_LIST Where p.ID = checkSession.TO_SESSION).FirstOrDefault
                Dim ss2 = (From p In Context.OT_OTHER_LIST Where p.ID = objValidate.FROM_SESSION).FirstOrDefault
                If ss1.CODE.ToUpper = "MOR" AndAlso ss2.CODE.ToUpper = "AFT" Then
                    Return 0
                Else
                    Return 1
                End If
            End If


            Dim q = (From p In Context.AT_LEAVESHEET_DETAIL
                 From e In Context.AT_LEAVESHEET.Where(Function(f) f.ID = p.LEAVESHEET_ID)
                 Where p.EMPLOYEE_ID = objValidate.EMPLOYEE_ID And (e.STATUS = 1 Or e.STATUS = 0 Or e.STATUS = 8001) _
                 And p.LEAVE_DAY >= objValidate.LEAVE_FROM And p.LEAVE_DAY <= objValidate.LEAVE_TO _
                 And (p.LEAVESHEET_ID <> objValidate.ID Or objValidate.ID = 0) Select p).ToList()
            If q.Count > 0 Then
                Return 1
            End If
            Dim check_calcu1 = (From p In Context.AT_SHIFT_REG_MNG Where Month(objValidate.LEAVE_FROM) = Month(p.WORKING_DAY) And p.EMPLOYEE_ID = objValidate.EMPLOYEE_ID)
            Dim check_calcu2 = (From p In Context.AT_SHIFT_REG_MNG Where Month(objValidate.LEAVE_TO) = Month(p.WORKING_DAY) And p.EMPLOYEE_ID = objValidate.EMPLOYEE_ID)

            If check_calcu1.Count > 0 AndAlso check_calcu2.Count > 0 Then
                Return 0
            Else
                Return 2
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GetLeaveSheet_ById(ByVal Leave_SheetID As Decimal, ByVal Struct As Decimal) As DataSet
        Dim dsData As New DataSet()
        Try
            Using cls As New DataAccess.QueryData
                dsData = cls.ExecuteStore("PKG_AT_LEAVESHEET.GET_LEAVESHEET_BYID",
                                               New With {.P_ID = Leave_SheetID,
                                                         .P_TRUCT = Struct,
                                                         .P_LEAVE = cls.OUT_CURSOR,
                                                         .P_LEAVE_DETAIL = cls.OUT_CURSOR}, False)
            End Using
            Return dsData
        Catch ex As Exception
        Finally
            dsData.Dispose()
        End Try
    End Function
    Public Function GetLeaveSheet_Detail_ByDate(ByVal employee_id As Decimal, ByVal fromDate As Date, ByVal toDate As Date, manualId As Decimal) As DataTable
        Dim dData As New DataTable()
        Try
            Using cls As New DataAccess.QueryData
                dData = cls.ExecuteStore("PKG_AT_LEAVESHEET.GET_LEAVE_SHEET_DETAIL_BYDATE",
                                               New With {.P_EMPLOYEE_ID = employee_id,
                                                         .P_FROM_DATE = fromDate,
                                                         .P_TO_DATE = toDate,
                                                         .P_MANUAL_ID = manualId,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
            End Using
            Return dData
        Catch ex As Exception
        Finally
            dData.Dispose()
        End Try
    End Function

    Public Function SaveLeaveSheet(ByVal dsLeaveSheet As DataSet, ByVal log As UserLog, Optional ByRef gID As Decimal = 0) As Boolean
        Dim rPH As DataRow
        Dim CT As DataTable = New DataTable()
        Dim oProps() As PropertyInfo = Nothing
        Dim objCT As AT_LEAVESHEET_DETAIL
        Try
            If dsLeaveSheet.Tables(0) IsNot Nothing AndAlso dsLeaveSheet.Tables(0).Rows.Count = 1 Then
                rPH = dsLeaveSheet.Tables(0).Rows(0)
            Else
                Return False
            End If
            If dsLeaveSheet.Tables(1) IsNot Nothing AndAlso dsLeaveSheet.Tables(1).Rows.Count > 0 Then
                CT = dsLeaveSheet.Tables(1)
            Else
                Return False
            End If
            'ket thuc kiem tra va gan du lieu
            If IsNumeric(rPH("ID")) AndAlso rPH("ID") > 0 Then
                'update
                Dim ID_LEAVE As Decimal = CDec(rPH("ID"))
                Dim objPH = (From p In Context.AT_LEAVESHEET Where p.ID = ID_LEAVE Select p).SingleOrDefault
                objPH.MODIFIED_BY = rPH("EMPLOYEE_NAME").ToString
                objPH.MODIFIED_DATE = DateTime.Now
                objPH.MODIFIED_LOG = log.Ip + "\" + log.ComputerName
                oProps = objPH.GetType().GetProperties()
                For Each pi As PropertyInfo In oProps
                    Try
                        pi.SetValue(objPH, rPH(pi.Name), Nothing)
                    Catch ex As Exception
                        Continue For
                    End Try
                Next pi
                Dim lst = (From p In Context.AT_LEAVESHEET_DETAIL Where p.LEAVESHEET_ID = objPH.ID).ToList()
                For index = 0 To lst.Count - 1
                    Context.AT_LEAVESHEET_DETAIL.DeleteObject(lst(index))
                Next
                For Each row As DataRow In CT.Rows
                    objCT = New AT_LEAVESHEET_DETAIL
                    oProps = objCT.GetType().GetProperties()
                    For Each pi As PropertyInfo In oProps
                        Try
                            pi.SetValue(objCT, row(pi.Name), Nothing)
                        Catch ex As Exception
                            Continue For
                        End Try
                    Next pi
                    objCT.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET_DETAIL.EntitySet.Name)
                    objCT.LEAVESHEET_ID = objPH.ID
                    Context.AT_LEAVESHEET_DETAIL.AddObject(objCT)
                Next

                Using cls As New DataAccess.QueryData
                    Dim dData = cls.ExecuteStore("PKG_AT_PROCESS.DELETE_PROCESS_APPROVED_STATUS",
                                                   New With {.P_ID = ID_LEAVE,
                                                             .P_OUT = cls.OUT_NUMBER}, True)
                End Using
            Else
                'insert 
                Dim objPH As New AT_LEAVESHEET

                oProps = objPH.GetType().GetProperties()
                For Each pi As PropertyInfo In oProps
                    Try
                        pi.SetValue(objPH, rPH(pi.Name), Nothing)
                    Catch ex As Exception
                        Continue For
                    End Try
                Next pi
                objPH.CREATED_BY = rPH("CREATED_BY").ToString
                objPH.MODIFIED_BY = rPH("MODIFIED_BY").ToString
                objPH.CREATED_DATE = DateTime.Now
                objPH.MODIFIED_DATE = DateTime.Now
                objPH.CREATED_LOG = log.Ip + "\" + log.ComputerName
                objPH.MODIFIED_LOG = log.Ip + "\" + log.ComputerName
                objPH.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET.EntitySet.Name)
                gID = objPH.ID
                Context.AT_LEAVESHEET.AddObject(objPH)
                For Each row As DataRow In CT.Rows
                    objCT = New AT_LEAVESHEET_DETAIL
                    oProps = objCT.GetType().GetProperties()
                    For Each pi As PropertyInfo In oProps
                        Try
                            pi.SetValue(objCT, row(pi.Name), Nothing)
                        Catch ex As Exception
                            Continue For
                        End Try
                    Next pi
                    objCT.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET_DETAIL.EntitySet.Name)
                    objCT.LEAVESHEET_ID = objPH.ID
                    Context.AT_LEAVESHEET_DETAIL.AddObject(objCT)
                Next
            End If
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Return False
        Finally
            CT.Dispose()
            rPH = Nothing
            objCT = Nothing
        End Try
    End Function

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
            Dim lstID = (From p In Context.AT_LEAVESHEET_DETAIL Where (p.LEAVE_DAY >= _filter.FROM_DATE OrElse Not _filter.FROM_DATE.HasValue) And (p.LEAVE_DAY <= _filter.END_DATE OrElse Not _filter.END_DATE.HasValue) Select p.LEAVESHEET_ID).ToList.Distinct.ToList()

            Dim query = From p In Context.AT_LEAVESHEET
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From m In Context.AT_SYMBOLS.Where(Function(f) f.ID = p.MANUAL_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From ss1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.FROM_SESSION).DefaultIfEmpty
                        From ss2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TO_SESSION).DefaultIfEmpty
                        From reason In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.REASON_LEAVE).DefaultIfEmpty
                        From huv In Context.HUV_AT_LEAVESHEET_DETAIL.Where(Function(f) f.LEAVESHEET_ID = p.ID).DefaultIfEmpty
                        From cre In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.CREATED_BY_EMP).DefaultIfEmpty
                        From modi In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.MODIFIED_BY_EMP).DefaultIfEmpty
                        From res In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.RESTORED_BY).DefaultIfEmpty
                        Where (lstID.Contains(p.ID) And (p.STATUS = 1 Or p.STATUS = 8001))
            If _filter.ISTEMINAL Then
                query = query.Where(Function(f) f.e.WORK_STATUS = 257)
                If _filter.WORKINGDAY.HasValue Then
                    query = query.Where(Function(f) f.e.TER_LAST_DATE <= _filter.WORKINGDAY)
                End If
            End If
            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(f) f.p.LEAVE_FROM >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                query = query.Where(Function(f) f.p.LEAVE_TO <= _filter.END_DATE)
            End If

            If _filter.MANUAL_ID IsNot Nothing Then
                query = query.Where(Function(f) f.p.MANUAL_ID <= _filter.MANUAL_ID)
            End If

            If Not String.IsNullOrEmpty(_filter.SEARCH_EMPLOYEE) Then
                query = query.Where(Function(f) f.e.EMPLOYEE_CODE.ToLower().Contains(_filter.SEARCH_EMPLOYEE.ToLower()) Or f.e.FULLNAME_VN.ToLower().Contains(_filter.SEARCH_EMPLOYEE.ToLower()))
            End If

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
            If _filter.LEAVE_FROM.HasValue Then
                query = query.Where(Function(f) f.p.LEAVE_FROM = _filter.LEAVE_FROM)
            End If
            If _filter.LEAVE_TO.HasValue Then
                query = query.Where(Function(f) f.p.LEAVE_TO = _filter.LEAVE_TO)
            End If
            If Not String.IsNullOrEmpty(_filter.MANUAL_NAME) Then
                query = query.Where(Function(f) f.m.WNAME.ToLower().Contains(_filter.MANUAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                query = query.Where(Function(f) f.p.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.FROM_SESSION_NAME) Then
                query = query.Where(Function(f) f.ss1.NAME_VN.ToLower().Contains(_filter.FROM_SESSION_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TO_SESSION_NAME) Then
                query = query.Where(Function(f) f.ss2.NAME_VN.ToLower().Contains(_filter.TO_SESSION_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.REASON_LEAVE_NAME) Then
                query = query.Where(Function(f) f.reason.NAME_VN.ToLower().Contains(_filter.REASON_LEAVE_NAME.ToLower()))
            End If

            Dim lst = query.Select(Function(p) New AT_LEAVESHEETDTO With {
                                                                       .ID = p.p.ID,
                                                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                                       .TITLE_NAME = p.t.NAME_VN,
                                                                       .ORG_NAME = p.o.NAME_VN,
                                                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                                                       .ORG_ID = p.e.ORG_ID,
                                                                       .LEAVE_FROM = p.p.LEAVE_FROM,
                                                                       .LEAVE_TO = p.p.LEAVE_TO,
                                                                       .MANUAL_NAME = p.m.WNAME,
                                                                       .MANUAL_ID = p.p.MANUAL_ID,
                                                                       .NOTE = p.p.NOTE,
                                                                       .DAY_NUM = p.p.DAY_NUM,
                                                                       .CREATED_BY = p.p.CREATED_BY,
                                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                                       .CREATED_LOG = p.p.CREATED_LOG,
                                                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                                                       .STATUS = p.p.STATUS,
                                                                       .STATUS_NAME = p.ot.NAME_VN,
                                                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                                                       .IMPORT = If(p.p.IMPORT = -1, "x", ""),
                                                                       .FROM_SESSION_NAME = p.ss1.NAME_VN,
                                                                       .TO_SESSION_NAME = p.ss2.NAME_VN,
                                                                       .REASON_LEAVE_NAME = p.reason.NAME_VN,
                                                                       .DAY_LIST = p.huv.DAY_LIST,
                                                                       .CREATED_BY_EMP = p.p.CREATED_BY_EMP,
                                                                       .CREATED_BY_EMP_NAME = p.cre.FULLNAME_VN,
                                                                       .MODIFIED_BY_EMP = p.p.MODIFIED_BY_EMP,
                                                                       .MODIFIED_BY_EMP_NAME = p.modi.FULLNAME_VN,
                                                                       .RESTORED_BY = p.p.RESTORED_BY,
                                                                       .RESTORED_BY_NAME = p.res.FULLNAME_VN,
                                                                       .RESTORED_DATE = p.p.RESTORED_DATE,
                                                                       .RESTORED_REASON = p.p.RESTORED_REASON,
                                                                       .IS_APP = p.p.IS_APP})

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
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
                    Dim prc = (From p In Context.PROCESS_APPROVED_STATUS Where p.ID_REGGROUP = lstl.ID)
                    For Each it In prc
                        Context.PROCESS_APPROVED_STATUS.DeleteObject(it)
                    Next
                    Dim details = (From r In Context.AT_LEAVESHEET_DETAIL Where r.LEAVESHEET_ID = lstl.ID).ToList
                    If Not details Is Nothing Then
                        For index1 = 0 To details.Count - 1
                            Context.AT_LEAVESHEET_DETAIL.DeleteObject(details(index1))
                        Next
                    End If
                    Context.AT_LEAVESHEET.DeleteObject(lstl)
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

    Public Function GetLeaveSheet_Portal(ByVal _filter As AT_LEAVESHEETDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LEAVESHEETDTO)
        Try
            Dim query = From p In Context.AT_LEAVESHEET
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(F) F.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From m In Context.AT_SYMBOLS.Where(Function(f) f.ID = p.MANUAL_ID).DefaultIfEmpty
                        From cre In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.CREATED_BY_EMP).DefaultIfEmpty
                        From modi In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.MODIFIED_BY_EMP).DefaultIfEmpty
                        From res In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.RESTORED_BY).DefaultIfEmpty
                        From reason In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.REASON_LEAVE).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS).DefaultIfEmpty
                        From nb In Context.AT_COMPENSATORY.Where(Function(F) F.EMPLOYEE_ID = p.EMPLOYEE_ID And F.YEAR = _filter.FROM_DATE.Value.Year).DefaultIfEmpty()
                        From ss1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.FROM_SESSION).DefaultIfEmpty
                        From ss2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TO_SESSION).DefaultIfEmpty
                        Where p.CREATED_BY_EMP = _filter.CREATED_BY_EMP

            '        From pas In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.APP_STATUS = 0 _
            'And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.ID And h.APP_STATUS = 0).Min(Function(k) k.APP_LEVEL))).DefaultIfEmpty()() _
            'From ee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = pas.EMPLOYEE_APPROVED And p.STATUS <> 2).DefaultIfEmpty()


            'Dim approveList = From p In query
            '                  From pas In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.p.ID And f.APP_STATUS = 0 _
            '                And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.p.ID And h.APP_STATUS = 0).Min(Function(k) k.APP_LEVEL))).DefaultIfEmpty() _
            'From ee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = pas.EMPLOYEE_APPROVED).DefaultIfEmpty()

            'GET LEAVE_SHEET BY EMPLOYEE ID
            'If _filter.EMPLOYEE_ID.HasValue Then
            '    query = query.Where(Function(f) f.p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            'End If
            If IsNumeric(_filter.STATUS) Then
                query = query.Where(Function(f) f.p.STATUS = _filter.STATUS)
            End If
            If IsNumeric(_filter.MANUAL_ID) Then
                query = query.Where(Function(f) f.p.MANUAL_ID = _filter.MANUAL_ID)
            End If
            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(f) f.p.LEAVE_FROM >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                query = query.Where(Function(f) f.p.LEAVE_TO <= _filter.END_DATE)
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
                query = query.Where(Function(f) f.m.WNAME.ToLower().Contains(_filter.MANUAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                query = query.Where(Function(f) f.p.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.IMPORT) Then
                query = query.Where(Function(f) f.p.IMPORT = -1)
            End If
            If Not String.IsNullOrEmpty(_filter.REASON) Then
                query = query.Where(Function(f) f.p.REASON.ToLower().Contains(_filter.REASON.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.FROM_SESSION_NAME) Then
                query = query.Where(Function(f) f.ss1.NAME_VN.ToLower().Contains(_filter.FROM_SESSION_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TO_SESSION_NAME) Then
                query = query.Where(Function(f) f.ss2.NAME_VN.ToLower().Contains(_filter.TO_SESSION_NAME.ToLower()))
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
                                                                       .MANUAL_NAME = p.m.WNAME,
                                                                       .MANUAL_ID = p.p.MANUAL_ID,
                                                                       .NOTE = p.p.NOTE,
                                                                       .DAY_NUM = p.p.DAY_NUM,
                                                                       .CREATED_BY = p.p.CREATED_BY,
                                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                                       .CREATED_LOG = p.p.CREATED_LOG,
                                                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                                                       .STATUS = p.p.STATUS,
                                                                       .STATUS_NAME = p.ot.NAME_VN,
                                                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                                                       .IMPORT = If(p.p.IMPORT = -1, "x", ""),
                                                                       .REASON = p.p.REASON,
                                                                       .FROM_SESSION = p.p.FROM_SESSION,
                                                                       .FROM_SESSION_NAME = p.ss1.NAME_VN,
                                                                       .TO_SESSION = p.p.TO_SESSION,
                                                                       .TO_SESSION_NAME = p.ss2.NAME_VN,
                                                                       .CREATED_BY_EMP = p.p.CREATED_BY_EMP,
                                                                       .CREATED_BY_EMP_NAME = p.cre.FULLNAME_VN,
                                                                       .MODIFIED_BY_EMP = p.p.MODIFIED_BY_EMP,
                                                                       .MODIFIED_BY_EMP_NAME = p.modi.FULLNAME_VN,
                                                                       .RESTORED_BY = p.p.RESTORED_BY,
                                                                       .RESTORED_BY_NAME = p.res.FULLNAME_VN,
                                                                       .RESTORED_DATE = p.p.RESTORED_DATE,
                                                                       .RESTORED_REASON = p.p.RESTORED_REASON,
                                                                       .REASON_LEAVE = p.p.REASON_LEAVE,
                                                                       .REASON_LEAVE_NAME = p.reason.NAME_VN})

            If Not String.IsNullOrEmpty(_filter.FROM_SESSION_NAME) Then
                lst = lst.Where(Function(f) f.FROM_SESSION_NAME.ToLower().Contains(_filter.FROM_SESSION_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TO_SESSION_NAME) Then
                lst = lst.Where(Function(f) f.TO_SESSION_NAME.ToLower().Contains(_filter.TO_SESSION_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.CREATED_BY_EMP_NAME) Then
                lst = lst.Where(Function(f) f.CREATED_BY_EMP_NAME.ToLower().Contains(_filter.CREATED_BY_EMP_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MODIFIED_BY_EMP_NAME) Then
                lst = lst.Where(Function(f) f.MODIFIED_BY_EMP_NAME.ToLower().Contains(_filter.MODIFIED_BY_EMP_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.RESTORED_BY_NAME) Then
                lst = lst.Where(Function(f) f.RESTORED_BY_NAME.ToLower().Contains(_filter.RESTORED_BY_NAME.ToLower()))
            End If
            If IsDate(_filter.RESTORED_DATE) Then
                lst = lst.Where(Function(f) f.RESTORED_DATE = _filter.RESTORED_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.REASON_LEAVE_NAME) Then
                lst = lst.Where(Function(f) f.REASON_LEAVE_NAME.ToLower().Contains(_filter.REASON_LEAVE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.RESTORED_REASON) Then
                lst = lst.Where(Function(f) f.RESTORED_REASON.ToLower().Contains(_filter.RESTORED_REASON.ToLower()))
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

    Public Function InsertSendLetter(ByVal objSend As AtSendApproveLetterDTO) As Boolean
        Dim objSendData As New AT_SEND_APPROVE_LETTER
        Try
            objSendData.ID = Utilities.GetNextSequence(Context, Context.AT_SEND_APPROVE_LETTER.EntitySet.Name)
            objSendData.LEAVE_ID = objSend.LEAVE_ID
            objSendData.ACTION_DATE = objSend.ACTION_DATE
            objSendData.ACTION_STATUS = objSend.ACTION_STATUS
            objSendData.ACTION_TYPE = objSend.ACTION_TYPE
            objSendData.USERNAME = objSend.USERNAME
            objSendData.RUN_START = objSend.RUN_START
            objSendData.RUN_END = objSend.RUN_END
            objSendData.RUN_ROW = objSend.RUN_ROW
            Context.AT_SEND_APPROVE_LETTER.AddObject(objSendData)
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertSendLetter")
            Throw ex
        End Try
    End Function

    Public Function GetDayNum(ByVal objLeave As AT_LEAVESHEETDTO) As Decimal
        Try
            Dim dayNum As Decimal = 0
            Dim Date_from As Date = If(IsDate(objLeave.LEAVE_FROM), objLeave.LEAVE_FROM, DateTime.Now)
            Dim Date_to As Date = If(IsDate(objLeave.LEAVE_TO), objLeave.LEAVE_TO, DateTime.Now)
            dayNum = DateDiff(DateInterval.Day, Date_from, Date_to) + 1
            Dim ss1 As String = (From p In Context.OT_OTHER_LIST Where p.ID = objLeave.FROM_SESSION Select p.CODE).FirstOrDefault
            Dim ss2 As String = (From p In Context.OT_OTHER_LIST Where p.ID = objLeave.TO_SESSION Select p.CODE).FirstOrDefault

            '' kiem tra co duoc phep dang ky nghi hay dang ky len khong, neu co thi tru di ngay nghi hang tuan voi ngay le
            Dim count_x = (From p In Context.AT_SYMBOLS Where p.ID = objLeave.MANUAL_ID And (p.IS_LEAVE_WEEKLY = -1 Or p.IS_LEAVE_HOLIDAY = -1))
            '' neu khong
            If count_x.Count < 1 Then

                ' kiem tra ngay bat dau nghi co phai la ngay nghi hang tuan hay ngay le khong
                Dim holyday_1 = (From p In Context.AT_HOLIDAY Where p.WORKINGDAY = Date_from)
                Dim weekend_1 = (From p In Context.AT_SHIFT_REG_MNG Where p.WORKING_DAY = Date_from And p.EMPLOYEE_ID = objLeave.EMPLOYEE_ID And p.WEEKEND IsNot Nothing)
                ' neu khong thi tru theo buoi dang ky
                If weekend_1.Count < 1 AndAlso holyday_1.Count < 1 Then
                    If ss1 = "AFT" Then
                        dayNum -= New Decimal(0.5)
                    End If
                Else
                    'neu co thi khong tinh ngay do
                    dayNum -= 1
                End If
                ' kiem tra ngay ket thuc nghi co phai la ngay nghi hang tuan hay ngay le khong
                Dim holyday_2 = (From p In Context.AT_HOLIDAY Where p.WORKINGDAY = Date_from)
                Dim weekend_2 = (From p In Context.AT_SHIFT_REG_MNG Where p.WORKING_DAY = Date_to And p.EMPLOYEE_ID = objLeave.EMPLOYEE_ID And p.WEEKEND IsNot Nothing)
                ' neu khong thi tru theo buoi dang ky
                If weekend_2.Count < 1 AndAlso holyday_2.Count < 1 Then
                    If ss2 = "MOR" Then
                        dayNum -= New Decimal(0.5)
                    End If
                Else
                    'neu co thi khong tinh ngay do
                    dayNum -= 1
                End If
                Date_from = Date_from.AddDays(1)
                Date_to = Date_to.AddDays(-1)
                'Dim query = (From p In Context.AT_SHIFT_REG_MNG Where p.WORKING_DAY >= Date_from And p.WORKING_DAY <= Date_to And p.EMPLOYEE_ID = objLeave.EMPLOYEE_ID)
                'For Each item In query
                '    If Not IsNothing(item.WEEKEND) OrElse Not IsNothing(item.HOLYDAY) Then
                '        dayNum -= 1
                '    End If
                'Next
                While Date_from <= Date_to
                    Dim check_wk = (From p In Context.AT_SHIFT_REG_MNG Where p.WORKING_DAY = Date_from And p.WEEKEND IsNot Nothing And p.EMPLOYEE_ID = objLeave.EMPLOYEE_ID).Count
                    Dim check_hld = (From p In Context.AT_HOLIDAY Where p.WORKINGDAY = Date_from And p.ACTFLG.ToUpper = "A").Count
                    If check_hld > 0 OrElse check_wk > 0 Then
                        dayNum -= 1
                    End If
                    Date_from = DateAdd(DateInterval.Day, 1, Date_from)
                End While
            Else
                Dim check_weekend = (From p In Context.AT_SYMBOLS Where p.ID = objLeave.MANUAL_ID And p.IS_LEAVE_WEEKLY = -1)
                Dim check_holiday = (From p In Context.AT_SYMBOLS Where p.ID = objLeave.MANUAL_ID And p.IS_LEAVE_HOLIDAY = -1)

                '' neu cho phep dang ky nghiu hang tuan va nghi len
                If check_holiday.Count > 0 AndAlso check_weekend.Count > 0 Then
                    If ss1 = "AFT" Then
                        dayNum -= New Decimal(0.5)
                    End If
                    If ss2 = "MOR" Then
                        dayNum -= New Decimal(0.5)
                    End If
                ElseIf check_holiday.Count > 0 AndAlso check_weekend.Count < 1 Then
                    '' neu chi cho phep dang ky nghi le
                    Dim weekend_1 = (From p In Context.AT_SHIFT_REG_MNG Where p.WORKING_DAY = Date_from And p.EMPLOYEE_ID = objLeave.EMPLOYEE_ID And p.WEEKEND IsNot Nothing)
                    If weekend_1.Count < 1 Then
                        If ss1 = "AFT" Then
                            dayNum -= New Decimal(0.5)
                        End If
                    Else
                        dayNum -= 1
                    End If
                    Dim weekend_2 = (From p In Context.AT_SHIFT_REG_MNG Where p.WORKING_DAY = Date_to And p.EMPLOYEE_ID = objLeave.EMPLOYEE_ID And p.WEEKEND IsNot Nothing)
                    If weekend_2.Count < 1 Then
                        If ss2 = "MOR" Then
                            dayNum -= New Decimal(0.5)
                        End If
                    Else
                        dayNum -= 1
                    End If
                    Date_from = Date_from.AddDays(1)
                    Date_to = Date_to.AddDays(-1)
                    While Date_from <= Date_to
                        Dim check_wk = (From p In Context.AT_SHIFT_REG_MNG Where p.WORKING_DAY = Date_from And p.WEEKEND IsNot Nothing And p.EMPLOYEE_ID = objLeave.EMPLOYEE_ID).Count
                        If check_wk > 0 Then
                            dayNum -= 1
                        End If
                        Date_from = DateAdd(DateInterval.Day, 1, Date_from)
                    End While
                Else
                    '' chi cho phep dang ky cuoi tuan
                    Dim holyday_1 = (From p In Context.AT_HOLIDAY Where p.WORKINGDAY = Date_from)
                    If holyday_1.Count < 1 Then
                        If ss1 = "AFT" Then
                            dayNum -= New Decimal(0.5)
                        End If
                    Else
                        dayNum -= 1
                    End If
                    Dim holyday_2 = (From p In Context.AT_HOLIDAY Where p.WORKINGDAY = Date_from)
                    If holyday_2.Count < 1 Then
                        If ss2 = "MOR" Then
                            dayNum -= New Decimal(0.5)
                        End If
                    Else
                        dayNum -= 1
                    End If
                    Date_from = Date_from.AddDays(1)
                    Date_to = Date_to.AddDays(-1)
                    While Date_from <= Date_to
                        Dim check_hld = (From p In Context.AT_HOLIDAY Where p.WORKINGDAY = Date_from And p.ACTFLG.ToUpper = "A").Count
                        If check_hld > 0 Then
                            dayNum -= 1
                        End If
                        Date_from = DateAdd(DateInterval.Day, 1, Date_from)
                    End While
                End If

            End If

            Return dayNum
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteLeaveOT")
            Throw ex
        End Try
    End Function

    Public Function UPDATE_AT_LEAVESHEET(ByVal P_LSTID As String, ByVal P_RESTORED_REASON As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_AT_LEAVESHEET.UPDATE_AT_LEAVESHEET",
                                 New With {.P_LSTID = P_LSTID,
                                           .P_RESTORED_REASON = P_RESTORED_REASON,
                                           .P_USERNAME = log.Username,
                                           .P_OUT = cls.OUT_CURSOR})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
            Return False
        End Try
    End Function
End Class
