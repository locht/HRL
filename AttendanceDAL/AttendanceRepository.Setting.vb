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
                       Select New AT_ObjectEmpployeeCompensatoryDTO With {.STT = Decimal.Parse(row("STT")),
                                                   .ID = Decimal.Parse(row("ID")),
                                                   .EMPLOYEE_ID = Decimal.Parse(row("EMPLOYEE_ID")),
                                                   .EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString,
                                                   .FULLNAME_VN = row("FULLNAME_VN").ToString,
                                                   .TITLE_ID = Decimal.Parse(row("TITLE_ID")),
                                                   .TITLE_NAME = row("TITLE_NAME").ToString,
                                                    .ORG_ID = Decimal.Parse(row("ORG_ID")),
                                                   .ORG_NAME = row("ORG_NAME").ToString,
                                                   .SAL_LEVEL_ID = Decimal.Parse(row("SAL_LEVEL_ID")),
                                                   .SAL_LEVEL_NAME = row("SAL_LEVEL_NAME").ToString,
                                                   .SAL_RANK_ID = Decimal.Parse(row("SAL_RANK_ID")),
                                                   .SAL_RANK_NAME = row("SAL_RANK_NAME").ToString,
                                                   .OBJ_EMP_ID = Decimal.Parse(row("OBJ_EMP_ID")),
                                                    .OBJ_EMP_NAME = row("OBJ_EMP_NAME").ToString,
                                                   .OBJ_CSL_ID = Decimal.Parse(row("OBJ_CSL_ID")),
                                                   .OBJ_CSL_NAME = row("OBJ_CSL_NAME").ToString,
                                                   .WORK_STATUS = Decimal.Parse(row("WORK_STATUS")),
                                                    .WORK_STATUS_NAME = row("WORK_STATUS_NAME").ToString
                                                  }).ToList
                    'TÌM KIẾM BÊN KHUNG TÌM KIẾM
                    If _filter.WORK_STATUS Then
                        If _filter.WORK_STATUS = 9999 Then
                            _filter.WORK_STATUS = 0
                        Else
                            _filter.WORK_STATUS = _filter.WORK_STATUS
                        End If
                        lst = lst.Where(Function(f) f.WORK_STATUS = _filter.WORK_STATUS Or If(_filter.CHECK_WORK_STATUS_LEAVE, f.WORK_STATUS = 257, f.WORK_STATUS = _filter.WORK_STATUS)).ToList
                    Else
                        lst = lst.Where(Function(f) f.WORK_STATUS >= 0 Or If(_filter.CHECK_WORK_STATUS_LEAVE, f.WORK_STATUS = 257, f.WORK_STATUS >= 0)).ToList
                    End If
                    If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE_NAME) Then
                        lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE_NAME.ToLower()) Or f.FULLNAME_VN.ToLower().Contains(_filter.EMPLOYEE_CODE_NAME.ToLower())).ToList
                    End If

                    If _filter.OBJ_EMP_ID IsNot Nothing Then
                        lst = lst.Where(Function(f) f.OBJ_EMP_ID = _filter.OBJ_EMP_ID).ToList
                    End If

                    If _filter.OBJ_CSL_ID IsNot Nothing Then
                        lst = lst.Where(Function(f) f.OBJ_CSL_ID = _filter.OBJ_CSL_ID).ToList
                    End If

                    'If _filter.OBJ_EMP_ID Is Nothing Then
                    '    If _filter.OBJ_CSL_ID Then
                    '        lst = lst.Where(Function(f) f.OBJ_EMP_ID = 0 Or f.OBJ_CSL_ID = _filter.OBJ_CSL_ID).ToList
                    '    Else
                    '        lst = lst.Where(Function(f) f.OBJ_EMP_ID = 0).ToList
                    '    End If
                    'End If
                    'If _filter.OBJ_CSL_ID Is Nothing Then
                    '    If _filter.OBJ_EMP_ID Then
                    '        lst = lst.Where(Function(f) f.OBJ_CSL_ID = 0 Or f.OBJ_EMP_ID = _filter.OBJ_EMP_ID).ToList
                    '    Else
                    '        lst = lst.Where(Function(f) f.OBJ_CSL_ID = 0).ToList
                    '    End If
                    'End If

                    'If _filter.OBJ_EMP_ID Or _filter.OBJ_CSL_ID Then
                    '    lst = lst.Where(Function(f) f.OBJ_EMP_ID = _filter.OBJ_EMP_ID Or f.OBJ_CSL_ID = _filter.OBJ_CSL_ID).ToList
                    'End If

                    'TÌM KIẾM TRÊN GRID
                    If _filter.STT Then
                        lst = lst.Where(Function(f) f.STT = _filter.STT).ToList
                    End If
                    If Not String.IsNullOrEmpty(_filter.OBJ_EMP_NAME) Then
                        lst = lst.Where(Function(f) f.OBJ_EMP_NAME.ToLower().Contains(_filter.OBJ_EMP_NAME.ToLower())).ToList
                    End If
                    If Not String.IsNullOrEmpty(_filter.OBJ_CSL_NAME) Then
                        lst = lst.Where(Function(f) f.OBJ_CSL_NAME.ToLower().Contains(_filter.OBJ_CSL_NAME.ToLower())).ToList
                    End If
                    If Not String.IsNullOrEmpty(_filter.SAL_LEVEL_NAME) Then
                        lst = lst.Where(Function(f) f.SAL_LEVEL_NAME.ToLower().Contains(_filter.SAL_LEVEL_NAME.ToLower())).ToList
                    End If
                    If Not String.IsNullOrEmpty(_filter.SAL_RANK_NAME) Then
                        lst = lst.Where(Function(f) f.SAL_RANK_NAME.ToLower().Contains(_filter.SAL_RANK_NAME.ToLower())).ToList
                    End If
                    If Not String.IsNullOrEmpty(_filter.WORK_STATUS_NAME) Then
                        lst = lst.Where(Function(f) f.WORK_STATUS_NAME.ToLower().Contains(_filter.WORK_STATUS_NAME.ToLower())).ToList
                    End If
                    If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                        lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower())).ToList
                    End If
                    If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                        lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower())).ToList
                    End If
                    If Not String.IsNullOrEmpty(_filter.FULLNAME_VN) Then
                        lst = lst.Where(Function(f) f.FULLNAME_VN.ToLower().Contains(_filter.FULLNAME_VN.ToLower())).ToList
                    End If
                    If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                        lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower())).ToList
                    End If


                    lst = (From l In lst
                  Select l).ToList()
                    Total = lst.Count
                    lst = (From l In lst.Skip(PageIndex * PageSize).Take(PageSize)
                          Select l).ToList()
                End If
            End Using

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Cập nhật đối tượng nhân viên và đối tượng nghỉ bù
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Update_ObjectEandC(ByVal list As List(Of AT_ObjectEmpployeeCompensatoryDTO),
                                       ByVal objEdit As AT_ObjectEmpployeeCompensatoryDTO,
                                        ByVal code_func As String) As Boolean
        Try
            Dim obj
            If code_func = "Update_ObjectEandC_EachOne" Then
                For Each item In list
                    Using cls As New DataAccess.QueryData
                        obj = New With {.P_EMP_ID = "|" + item.ID.ToString + "|",
                                        .P_OBJ_EMP_ID = item.OBJ_EMP_ID,
                                        .P_OBJ_COM_ID = item.OBJ_CSL_ID,
                                        .P_OUT = cls.OUT_NUMBER}
                        cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.UPDATE_OBJECT_EMPLOYEE_COMPENSATORY", obj)
                    End Using
                Next
            End If
            If code_func = "Update_ObjectEandC_All" Then
                For Each item In list
                    Using cls As New DataAccess.QueryData
                        obj = New With {.P_EMP_ID = "|" + item.ID.ToString + "|",
                                        .P_OBJ_EMP_ID = item.OBJ_EMP_ID,
                                        .P_OBJ_COM_ID = item.OBJ_CSL_ID,
                                        .P_OUT = cls.OUT_NUMBER}
                        cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.UPDATE_OBJECT_EMPLOYEE_COMPENSATORY", obj)
                    End Using
                Next
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Return False
            Throw ex
        End Try
    End Function

    Public Function GetOrgShiftList(ByVal strId As String, Optional ByVal log As UserLog = Nothing) As DataTable
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.AT_ORGSHIFT_LIST",
                                           New With {.P_USERNAME = log.Username.ToUpper,
                                                     .P_ORG_ID = strId,
                                                     .P_OUT = cls.OUT_CURSOR})
                Return dtData

            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertOrgShifT(ByVal list As List(Of AT_ORG_SHIFT_DTO), Optional ByVal log As UserLog = Nothing) As Boolean
        Try

            For Each item In list
                Dim objData As New AT_ORG_SHIFT
                objData.ID = Utilities.GetNextSequence(Context, Context.AT_ORG_SHIFT.EntitySet.Name)
                objData.ORG_ID = item.ORG_ID
                objData.SHIFT_CODE = item.SHIFT_CODE
                Context.AT_ORG_SHIFT.AddObject(objData)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeleteAtOrgShift(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim lst = (From p In Context.AT_ORG_SHIFT Where lstID.Contains(p.ORG_ID)).ToList
            For index = 0 To lst.Count - 1
                Context.AT_ORG_SHIFT.DeleteObject(lst(index))
            Next

            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function


#Region "List Salary Fomuler"
    Public Function GetAllFomulerGroup(ByVal _filter As ATFormularDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CFDESC ASC") As List(Of ATFormularDTO)
        Try
            Dim lst = (From q In Context.AT_FORMULAR
                       Select New ATFormularDTO With {.ID = q.ID,
                                               .FML_NAME = q.FML_NAME,
                                               .CFDESC = q.CFDESC,
                                               .STATUS = q.STATUS,
                                               .EFFECT_DATE = q.EFFECT_DATE,
                                               .EXPIRE_DATE = q.EXPIRE_DATE,
                                               .CREATED_BY = q.CREATED_BY,
                                               .CREATED_DATE = q.CREATED_DATE,
                                               .CREATED_LOG = q.CREATED_LOG,
                                               .MODIFIED_BY = q.MODIFIED_BY,
                                               .MODIFIED_DATE = q.MODIFIED_DATE,
                                               .MODIFIED_LOG = q.MODIFIED_LOG
                                               })
            If _filter.FML_NAME <> "" Then
                lst = lst.Where(Function(p) p.FML_NAME.ToUpper.Contains(_filter.FML_NAME.ToUpper))
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

    '    Public Function InsertFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
    '        Dim objData As New PA_FORMULER_GROUP
    '        Try
    '            objData.ID = Utilities.GetNextSequence(Context, Context.AT_PERIOD.EntitySet.Name)
    '            objData.TYPE_PAYMENT = objPeriod.TYPE_PAYMENT
    '            objData.OBJ_SAL_ID = objPeriod.OBJ_SAL_ID
    '            objData.NAME_VN = objPeriod.NAME_VN
    '            objData.NAME_EN = objPeriod.NAME_EN
    '            objData.START_DATE = objPeriod.START_DATE
    '            objData.END_DATE = objPeriod.END_DATE
    '            objData.STATUS = objPeriod.STATUS
    '            objData.SDESC = objPeriod.SDESC
    '            objData.IDX = objPeriod.IDX
    '            Context.PA_FORMULER_GROUP.AddObject(objData)
    '            Context.SaveChanges(log)
    '            gID = objData.ID
    '            Return True
    '        Catch ex As Exception
    '            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
    '            Throw ex
    '        End Try

    '    End Function
    '    Public Function ModifyFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
    '        Dim objData As New PA_FORMULER_GROUP With {.ID = objPeriod.ID}
    '        Try
    '            Context.PA_FORMULER_GROUP.Attach(objData)
    '            objData.OBJ_SAL_ID = objPeriod.OBJ_SAL_ID
    '            objData.NAME_VN = objPeriod.NAME_VN
    '            objData.NAME_EN = objPeriod.NAME_EN
    '            objData.START_DATE = objPeriod.START_DATE
    '            objData.END_DATE = objPeriod.END_DATE
    '            objData.STATUS = objPeriod.STATUS
    '            objData.SDESC = objPeriod.SDESC
    '            objData.IDX = objPeriod.IDX
    '            Context.SaveChanges(log)
    '            gID = objData.ID
    '            Return True
    '        Catch ex As Exception
    '            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
    '            Throw ex
    '        End Try
    '    End Function

    '    Public Function DeleteFomulerGroup(ByVal lstDelete As PAFomulerGroup) As Boolean
    '        Dim objData As List(Of PA_FORMULER_GROUP) = (From p In Context.PA_FORMULER_GROUP Where p.ID = lstDelete.ID).ToList
    '        Try
    '            For Each item In objData
    '                Context.PA_FORMULER_GROUP.DeleteObject(item)
    '            Next
    '            Return True
    '        Catch ex As Exception
    '            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
    '            Throw ex
    '        End Try
    '    End Function

    '    Public Function GetListAllSalary(ByVal gID As Decimal) As List(Of PAFomuler)
    '        Try
    '            Dim query = From p In Context.PA_LISTSALARIES
    '                        From g In Context.PA_FORMULER_GROUP.Where(Function(g) g.OBJ_SAL_ID = p.OBJ_SAL_ID)
    '                        From f In Context.PA_FORMULER.Where(Function(f) f.GROUP_FML = g.ID And f.COL_NAME = p.COL_NAME).DefaultIfEmpty
    '                        Where g.ID = gID And p.IS_DELETED = 0 And p.IS_SUMARISING = -1 And p.IS_IMPORT = 0 Order By f.INDEX_FML Ascending, f.COL_NAME Ascending
    '            Dim obj = query.Select(Function(o) New PAFomuler With
    '                        {.ID = o.p.ID,
    '                         .COL_NAME = o.p.COL_NAME,
    '                         .NAME_VN = o.p.NAME_VN,
    '                         .NAME_EN = o.p.NAME_EN,
    '                         .COL_INDEX = o.f.INDEX_FML,
    '                         .FORMULER = o.f.FORMULER
    '                        }).ToList
    '            Return obj
    '        Catch ex As Exception
    '            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
    '            Throw ex
    '        End Try
    '    End Function
    '    Public Function GetListInputColumn(ByVal gID As Decimal) As DataTable
    '        Try
    '            Dim query = From p In Context.PA_LISTSALARIES
    '                        From g In Context.PA_FORMULER_GROUP.Where(Function(g) g.OBJ_SAL_ID = p.OBJ_SAL_ID)
    '                        Where p.STATUS = "A" And g.ID = gID Order By p.NAME_VN Ascending, p.COL_INDEX Ascending
    '            Dim obj = query.Select(Function(f) New PAListSalariesDTO With
    '                        {.ID = f.p.ID,
    '                         .COL_INDEX = f.p.COL_INDEX,
    '                         .COL_NAME = f.p.COL_NAME,
    '                         .NAME_VN = f.p.NAME_VN & " - (" & f.p.COL_NAME & ")",
    '                         .NAME_EN = f.p.NAME_EN
    '                        }).ToList
    '            Return obj.ToTable()
    '        Catch ex As Exception
    '            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
    '            Throw ex
    '        End Try
    '    End Function
    '    Public Function GetListSalColunm(ByVal gID As Decimal) As DataTable
    '        Try
    '            Dim query = From p In Context.PA_LISTSAL
    '                        Where p.STATUS = "A" And p.GROUP_TYPE = gID Order By p.NAME_VN, p.COL_INDEX Ascending
    '            Dim obj = query.Select(Function(f) New PAListSalDTO With
    '                        {.ID = f.ID,
    '                         .COL_INDEX = f.COL_INDEX,
    '                         .COL_NAME = f.COL_NAME,
    '                         .NAME_VN = f.NAME_VN,
    '                         .NAME_EN = f.NAME_EN
    '                        }).ToList
    '            Return obj.ToTable()
    '        Catch ex As Exception
    '            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
    '            Throw ex
    '        End Try
    '    End Function
    '    Public Function GetListCalculation() As List(Of OT_OTHERLIST_DTO)
    '        Try
    '            Dim query = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
    '                         Where p.ACTFLG = "A" And t.CODE = "CALCULATION" Order By p.CREATED_DATE Descending
    '                         Select New OT_OTHERLIST_DTO With {
    '                             .ID = p.ID,
    '                             .CODE = p.CODE,
    '                             .NAME_EN = p.NAME_EN,
    '                             .NAME_VN = p.NAME_VN,
    '                .TYPE_ID = p.TYPE_ID
    '                         }).ToList
    '            Return query
    '        Catch ex As Exception
    '            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
    '            Throw ex
    '        End Try
    '    End Function
    '    Public Function CopyFomuler(ByRef F_ID As Decimal,
    '                                    ByVal log As UserLog, ByRef T_ID As Decimal) As Boolean


    '        Try
    '            Using cls As New DataAccess.NonQueryData
    '                cls.ExecuteStore("PKG_PA_SETTING.COPY_FORMULER_SALARY",
    '                                           New With {.OBJ_SAL_FROM = F_ID,
    '                                                     .OBJ_SAL_TO = T_ID})
    '            End Using

    '            Return True
    '        Catch ex As Exception
    '            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
    '            Throw ex
    '        End Try

    '    End Function
    '    Public Function SaveFomuler(ByVal objData As AT_FORMULAR, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
    '        Dim objInsert As AT_FORMULAR
    '        Dim iCount As Integer = 0
    '        Try
    '            objInsert = (From p In Context.AT_FORMULAR Where p. = objData.COL_NAME And p.GROUP_FML = objData.GROUP_FML).SingleOrDefault
    '            If objInsert Is Nothing Then
    '                objInsert = New PA_FORMULER
    '                objInsert.ID = Utilities.GetNextSequence(Context, Context.PA_FORMULER.EntitySet.Name)
    '                objInsert.COL_NAME = objData.COL_NAME
    '                objInsert.INDEX_FML = objData.INDEX_FML
    '                objInsert.GROUP_FML = objData.GROUP_FML
    '                objInsert.FORMULER = objData.FORMULER
    '                objInsert.CREATED_BY = objData.CREATED_BY
    '                objInsert.CREATED_DATE = objData.CREATED_DATE
    '                objInsert.CREATED_LOG = objData.CREATED_LOG
    '                objInsert.MODIFIED_BY = objData.MODIFIED_BY
    '                objInsert.MODIFIED_DATE = objData.MODIFIED_DATE
    '                objInsert.MODIFIED_LOG = objData.MODIFIED_LOG
    '                Context.PA_FORMULER.AddObject(objInsert)
    '            Else
    '                objInsert.COL_NAME = objData.COL_NAME
    '                objInsert.INDEX_FML = objData.INDEX_FML
    '                objInsert.GROUP_FML = objData.GROUP_FML
    '                objInsert.FORMULER = objData.FORMULER
    '                objInsert.MODIFIED_BY = objData.MODIFIED_BY
    '                objInsert.MODIFIED_DATE = objData.MODIFIED_DATE
    '                objInsert.MODIFIED_LOG = objData.MODIFIED_LOG
    '            End If
    '            Context.SaveChanges(log)
    '            gID = objInsert.ID
    '            Return True
    '        Catch ex As Exception
    '            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
    '            Throw ex
    '        End Try
    '    End Function
    '    Public Function CheckFomuler(ByVal sCol As String, ByVal sFormuler As String, ByVal objID As Decimal) As Boolean
    '        Try
    '            Using cls As New DataAccess.NonQueryData
    '                Dim sql As String = ""
    '                Dim sql1 As String = ""
    '                Dim sql2 As String = ""
    '                sql = "UPDATE TEMP_CALCULATE T SET T." & sCol & " = NVL(" & sFormuler & ",0)"
    '                sql1 = "UPDATE TEMP_CALCULATE_SUM T SET T." & sCol & " = NVL(" & sFormuler & ",0)"
    '                sql2 = "UPDATE PA_INCOME_TAX_SUM T SET T." & sCol & " = NVL(" & sFormuler & ",0)"
    '                sql &= " WHERE 1=0 "
    '                sql1 &= " WHERE 1=0 "
    '                sql2 &= " WHERE 1=0 "
    '                If objID = 11 Then
    '                    cls.ExecuteSQL(sql2)

    '                Else
    '                    cls.ExecuteSQL(sql)
    '                    cls.ExecuteSQL(sql1)
    '                End If

    '            End Using
    '            Return True
    '        Catch ex As Exception
    '            Return False
    '        End Try
    '    End Function

    Public Function ActiveFolmulerGroup(ByVal lstID As Decimal, ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
        Dim lstData As AT_FORMULAR
        Try
            lstData = (From p In Context.AT_FORMULAR Where p.ID = lstID).SingleOrDefault
            lstData.STATUS = bActive
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function GetListInputColumn(ByVal gID As Decimal) As DataSet
        Try
            Using cls As New DataAccess.QueryData

                Dim dtData = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_SYMBOLS",
                                           New With {.P_GRP_ID = gID,
                                                    .P_CUR = cls.OUT_CURSOR,
                                                    .P_CUR1 = cls.OUT_CURSOR
                                                   }, False)

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            'Throw ex
            'Return New System.Data.DataSet()
        End Try
        'Try
        '    Dim query = From p In Context.AT_SYMBOLS
        '                From g In Context.OT_OTHER_LIST.Where(Function(g) g.ID = p.WGROUPID)
        '                Where p.STATUS = -1 Order By p.WINDEX Ascending
        '    Dim obj = query.Select(Function(f) New AT_SymbolsDTO With
        '                {.ID = f.p.ID,
        '                 .WINDEX = f.p.WINDEX,
        '                 .WNAME = f.p.WNAME,
        '                 .WGROUP_NAME = f.g.NAME_VN,
        '                 .WCODE = f.p.WCODE,
        '                 .WGROUPID = f.p.WGROUPID
        '                }).ToList
        '    Return obj.ToTable()
        'Catch ex As Exception
        '    WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
        '    Throw ex
        'End Try
    End Function
    Public Function GetListCalculation() As List(Of OT_OTHERLIST_DTO)
        Try
            Dim query = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                         Where p.ACTFLG = "A" And t.CODE = "CALCULATION" Order By p.CREATED_DATE Descending
                         Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                .TYPE_ID = p.TYPE_ID
                         }).ToList
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function PRU_SYNCHFORMULAR(ByVal gID As Decimal, Optional ByVal log As UserLog = Nothing) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.PRU_SYNCHFORMULAR",
                                           New With {.P_ID = gID,
                                                     .P_USERNAME = log.Username.ToUpper,
                                                    .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            'Throw ex
            Return New DataTable()
        End Try
    End Function
    Public Function CheckFomuler(ByVal sCol As String, ByVal sFormuler As String, ByVal objID As Decimal) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData
                Dim sql As String = ""
                Dim sql1 As String = ""
                Dim sql2 As String = ""
                sql = "UPDATE AT_DATA_ALL T SET T." & sCol & " = NVL(" & sFormuler & ",0)"
                'sql1 = "UPDATE TEMP_CALCULATE_SUM T SET T." & sCol & " = NVL(" & sFormuler & ",0)"
                'sql2 = "UPDATE PA_INCOME_TAX_SUM T SET T." & sCol & " = NVL(" & sFormuler & ",0)"
                sql &= " WHERE 1=0 "
                'sql1 &= " WHERE 1=0 "
                'sql2 &= " WHERE 1=0 "
                cls.ExecuteSQL(sql)
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function SaveFomuler(ByVal objData As ATFml_DetailDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objInsert As AT_FML_DETAIL
        Dim iCount As Integer = 0
        Try
            objInsert = (From p In Context.AT_FML_DETAIL Where p.WCODE = objData.WCODE And p.GFID = objData.GFID).SingleOrDefault
            If objInsert Is Nothing Then
                objInsert = New AT_FML_DETAIL
                objInsert.ID = Utilities.GetNextSequence(Context, Context.AT_FML_DETAIL.EntitySet.Name)
                objInsert.WCODE = objData.WCODE
                objInsert.FINDEX = objData.FINDEX
                objInsert.GFID = objData.GFID
                objInsert.FORMULAR = objData.FORMULAR
                objInsert.CREATED_BY = objData.CREATED_BY
                objInsert.CREATED_DATE = objData.CREATED_DATE
                objInsert.CREATED_LOG = objData.CREATED_LOG
                objInsert.MODIFIED_BY = objData.MODIFIED_BY
                objInsert.MODIFIED_DATE = objData.MODIFIED_DATE
                objInsert.MODIFIED_LOG = objData.MODIFIED_LOG
                Context.AT_FML_DETAIL.AddObject(objInsert)
            Else
                objInsert.WCODE = objData.WCODE
                objInsert.FINDEX = objData.FINDEX
                objInsert.GFID = objData.GFID
                objInsert.FORMULAR = objData.FORMULAR
                objInsert.MODIFIED_BY = objData.MODIFIED_BY
                objInsert.MODIFIED_DATE = objData.MODIFIED_DATE
                objInsert.MODIFIED_LOG = objData.MODIFIED_LOG
            End If
            Context.SaveChanges(log)
            gID = objInsert.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

End Class