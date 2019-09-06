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
    Public Function ValidateLeaveSheetDetail(ByVal objValidate As AT_LEAVESHEETDTO) As Boolean
        Try
            Dim q = (From p In Context.AT_LEAVESHEET_DETAIL Where p.EMPLOYEE_ID = objValidate.EMPLOYEE_ID And p.LEAVE_DAY >= objValidate.LEAVE_FROM And p.LEAVE_DAY <= objValidate.LEAVE_TO And (p.LEAVESHEET_ID <> objValidate.ID Or objValidate.ID = 0) Select p).ToList()
            If q.Count > 0 Then
                Return False
            Else
                Return True
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

    Public Function SaveLeaveSheet(ByVal dsLeaveSheet As DataSet, ByVal log As UserLog) As Boolean
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
                objPH.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET.EntitySet.Name)
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
            Context.SaveChanges(log)
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
                        From s In Context.HU_STAFF_RANK.Where(Function(F) F.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From m In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS).DefaultIfEmpty
                        From nb In Context.AT_COMPENSATORY.Where(Function(F) F.EMPLOYEE_ID = p.EMPLOYEE_ID And F.YEAR = _filter.FROM_DATE.Value.Year).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where (lstID.Contains(p.ID))
            If _filter.ISTEMINAL Then
                query = query.Where(Function(f) f.e.WORK_STATUS = 257)
                If _filter.WORKINGDAY.HasValue Then
                    query = query.Where(Function(f) f.e.TER_LAST_DATE <= _filter.WORKINGDAY)
                End If
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
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                query = query.Where(Function(f) f.p.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.IMPORT) Then
                query = query.Where(Function(f) f.p.IMPORT = -1)
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
                                                                       .NOTE = p.p.NOTE,
                                                                       .DAY_NUM = p.p.DAY_NUM,
                                                                       .EMP_APPROVES_NAME = p.p.EMP_APPROVES_NAME,
                                                                       .CREATED_BY = p.p.CREATED_BY,
                                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                                       .CREATED_LOG = p.p.CREATED_LOG,
                                                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                                                       .STATUS = p.p.STATUS,
                                                                       .STATUS_NAME = p.ot.NAME_VN,
                                                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                                                       .IMPORT = If(p.p.IMPORT = -1, "x", "")})

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
                    Context.AT_LEAVESHEET.DeleteObject(lstl)
                    Dim details = (From r In Context.AT_LEAVESHEET_DETAIL Where r.LEAVESHEET_ID = lstl.ID).ToList
                    If Not details Is Nothing Then
                        For index1 = 0 To details.Count - 1
                            Context.AT_LEAVESHEET_DETAIL.DeleteObject(details(index1))
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
                        From m In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS).DefaultIfEmpty
                        From nb In Context.AT_COMPENSATORY.Where(Function(F) F.EMPLOYEE_ID = p.EMPLOYEE_ID And F.YEAR = _filter.FROM_DATE.Value.Year).DefaultIfEmpty

            'GET LEAVE_SHEET BY EMPLOYEE ID
            If _filter.EMPLOYEE_ID.HasValue Then
                query = query.Where(Function(f) f.p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
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
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                query = query.Where(Function(f) f.p.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.IMPORT) Then
                query = query.Where(Function(f) f.p.IMPORT = -1)
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
                                                                       .NOTE = p.p.NOTE,
                                                                       .DAY_NUM = p.p.DAY_NUM,
                                                                       .EMP_APPROVES_NAME = p.p.EMP_APPROVES_NAME,
                                                                       .CREATED_BY = p.p.CREATED_BY,
                                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                                       .CREATED_LOG = p.p.CREATED_LOG,
                                                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                                                       .STATUS = p.p.STATUS,
                                                                       .STATUS_NAME = p.ot.NAME_VN,
                                                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                                                       .IMPORT = If(p.p.IMPORT = -1, "x", "")})

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

End Class
