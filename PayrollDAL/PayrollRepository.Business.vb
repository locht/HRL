Imports System.Text
Imports System.Linq.Expressions
Imports LinqKit.Extensions
Imports System.Data.Common
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic
Imports System.Data.Entity
Imports System.Text.RegularExpressions
Imports System.Data.Objects
Imports Oracle.DataAccess.Client
Imports System.Reflection

Partial Public Class PayrollRepository

#Region "Calculate Salary"
    Public Function Load_Calculate_Load(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        Try
            Using Sql As New DataAccess.NonQueryData
                ' Tải dữ liệu trên bảng dữ liệu tính toán
                'Sql.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA_CALCULATE",
                '                 New With {.P_PERIOD_ID = PeriodId,
                '                           .P_ORG_ID = OrgId,
                '                           .P_ISDISSOLVE = IsDissolve,
                '                           .P_USERNAME = log.Username,
                '                           .P_ISLOAD = IsLoad})
                '' Chạy công thức tính toán trên bảng dữ liệu lương trong kỳ
                'Sql.ExecuteStore("PKG_PA_BUSINESS.CALCULATE_DATA_TEMP",
                '                 New With {.P_PERIOD_ID = PeriodId,
                '                           .P_ORG_ID = OrgId,
                '                           .P_ISDISSOLVE = IsDissolve,
                '                           .P_USERNAME = log.Username})
                '' Tải dữ liệu sang bảng lương tổng hợp
                'Sql.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA_SUM",
                '                New With {.P_PERIOD_ID = PeriodId,
                '                          .P_ORG_ID = OrgId,
                '                          .P_ISDISSOLVE = IsDissolve,
                '                          .P_USERNAME = log.Username,
                '                          .P_ISLOAD = IsLoad})
                Sql.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA",
                               New With {.P_PERIOD_ID = PeriodId,
                                         .P_ORG_ID = OrgId,
                                         .P_ISDISSOLVE = IsDissolve,
                                         .P_USERNAME = log.Username,
                                         .P_ISLOAD = IsLoad})

            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function

    Public Function Calculate_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        Try
            Using Sql As New DataAccess.NonQueryData
                Sql.ExecuteStore("PKG_PA_BUSINESS.CALCULATE_DATA_SUM",
                                 New With {.P_PERIOD_ID = PeriodId,
                                           .P_ORG_ID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function
    Public Function Load_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        Try
            Using Sql As New DataAccess.NonQueryData
                Sql.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA_SUM",
                                 New With {.P_PERIOD_ID = PeriodId,
                                           .P_ORG_ID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve,
                                           .P_USERNAME = log.Username,
                                           .P_ISLOAD = IsLoad})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function
    Public Function Calculate_data_temp(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        Try
            Using Sql As New DataAccess.NonQueryData
                Sql.ExecuteStore("PKG_PA_BUSINESS.CALCULATE_DATA_TEMP",
                                 New With {.P_PERIOD_ID = PeriodId,
                                           .P_ORG_ID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function
    Public Function Load_data_calculate(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        Try
            Using Sql As New DataAccess.NonQueryData
                Sql.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA_CALCULATE",
                                 New With {.P_PERIOD_ID = PeriodId,
                                           .P_ORG_ID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve,
                                           .P_USERNAME = log.Username,
                                           .P_ISLOAD = IsLoad})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function
    Public Function GetLitsCalculate(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer,
                                     ByVal log As UserLog, Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataSet

        Try
            Using cls As New DataAccess.QueryData
                'cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                '                 New With {.P_USERNAME = log.Username,
                '                           .P_ORGID = OrgId,
                '                           .P_ISDISSOLVE = IsDissolve})

                Dim dtData As DataSet = cls.ExecuteStore("PKG_PA_BUSINESS.GET_LIST_CALCULATE",
                                           New With {.P_ORGID = OrgId,
                                                     .P_ISDISSOLVE = IsDissolve,
                                                     .P_USERNAME = log.Username,
                                                     .P_PERIOD_ID = PeriodId,
                                                     .P_SORT = Sorts,
                                                     .IS_LOAD = IsLoad,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function LoadCalculate(ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal listEmployee As List(Of String),
                                     ByVal log As UserLog, Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataSet

        Try

            Using cls As New DataAccess.NonQueryData
                cls.ExecuteSQL("DELETE SE_CHOSEN_CALCULATE")
                If listEmployee.Count <= 0 Then
                    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_CALCULATE",
                                     New With {.P_USERNAME = log.Username,
                                               .P_ORGID = OrgId,
                                               .P_ISDISSOLVE = IsDissolve})
                Else
                    For Each emp As String In listEmployee
                        Dim objNew As New SE_CHOSEN_CALCULATE
                        objNew.EMPLOYEEID = Utilities.Obj2Decima(emp)
                        objNew.USERNAME = log.Username
                        Context.SE_CHOSEN_CALCULATE.AddObject(objNew)
                    Next
                    Context.SaveChanges()
                End If
            End Using
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_PA_BUSINESS.GET_IMPORTSALARY",
                                           New With {.P_ORG_ID = OrgId,
                                                     .P_PERIOD_ID = PeriodId,
                                                     .P_USERNAME = log.Username,
                                                     .P_SORT = Sorts,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetListSalaryVisibleCol() As List(Of PAListSalariesDTO)
        Try
            Dim query = From s In Context.PA_LISTSALARIES
                        Where s.IS_VISIBLE = True And s.STATUS = "A"
                        Order By s.COL_INDEX
                       Select New PAListSalariesDTO With {
                                        .ID = s.ID,
                                        .TYPE_PAYMENT = s.TYPE_PAYMENT,
                                        .COL_NAME = s.COL_NAME,
                                        .NAME_EN = s.NAME_EN,
                                        .NAME_VN = s.NAME_VN,
                                        .COL_INDEX = s.COL_INDEX,
                                        .CREATED_DATE = s.CREATED_DATE,
                                        .IS_VISIBLE = s.IS_VISIBLE,
                                        .STATUS = s.STATUS}

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ActiveOrDeactive(ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal Active As Integer, ByVal log As UserLog) As Boolean
        Try

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function
#End Region
#Region "importbonus"
    Public Function GetlistYear() As DataTable
        Dim dtData As New DataTable
        Try
            dtData = (From p In Context.AT_SETUP_BONUS
                      Group p By p.YEAR Into Group
                      Select New With {.Year = YEAR, .ID = YEAR}).ToList.ToTable()
            Return dtData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetListGrBonus(ByVal year As Decimal) As DataTable
        Dim dtData As New DataTable
        Try
            dtData = (From p In Context.AT_SETUP_BONUS
                     Where p.ACTFLG = "A" And p.YEAR = year
                     Select New With {p.ID, p.NAME_BONUS}).ToList.ToTable

            Return dtData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetGrBonus() As DataTable
        Dim dtData As New DataTable
        Try
            dtData = (From p In Context.PA_SALARY_TYPE
                      Where p.IS_INCENTIVE = -1 And p.ACTFLG = "A"
                      Select New With {p.ID, p.NAME}).ToList.ToTable

            Return dtData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    'get ds nhan vien thoa dk
    Public Function GetListEmpBonus(ByVal periodBonus As Decimal, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve})
            End Using


        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region
#Region "Import Bonus"

    Public Function GetImportBonus(ByVal Year As Integer, ByVal obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve})
            End Using
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA_IMPORT_BONUS",
                                           New With {.P_USERNAME = log.Username,
                                                     .P_ORG_ID = OrgId,
                                                     .YEAR = Year,
                                                     .P_PERIOD_ID = PeriodId,
                                                     .P_OBJ_SAL_ID = obj_sal_id,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

#Region "Import Salary"

    Public Function GetImportSalary(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve})
            End Using
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA_IMPORT",
                                           New With {.P_USERNAME = log.Username,
                                                     .P_ORG_ID = OrgId,
                                                     .P_PERIOD_ID = PeriodId,
                                                     .P_OBJ_SAL_ID = obj_sal_id,
                                                     .P_EMPLOYEE_ID = EmployeeId,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GET_DATA_SEND_MAIL(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable

        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.GET_DATA_SEND_MAIL",
                                           New With {.P_USERNAME = log.Username,
                                                     .P_ORG_ID = OrgId,
                                                     .P_PERIOD_ID = PeriodId,
                                                     .P_ISDISSOLVE = IsDissolve,
                                                     .P_OBJ_SAL_ID = obj_sal_id,
                                                     .P_EMPLOYEE_ID = EmployeeId,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetMappingSalary(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer) As DataTable

        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA_MAPPING",
                                           New With {.P_OBJ_SAL_ID = obj_sal_id,
                                                     .P_PERIOD_ID = PeriodId,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetMappingSalaryImport(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer) As DataTable

        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA_MAPPING_IMPORT",
                                           New With {.P_OBJ_SAL_ID = obj_sal_id,
                                                     .P_PERIOD_ID = PeriodId,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetSalaryList() As List(Of PAListSalariesDTO)
        Try
            Dim query = From p In Context.PA_LISTSALARIES
            Dim lst = query.Select(Function(s) New PAListSalariesDTO With {
                                        .ID = s.ID,
                                        .TYPE_PAYMENT = s.TYPE_PAYMENT,
                                        .COL_NAME = s.COL_NAME,
                                        .NAME_EN = s.NAME_EN,
                                        .NAME_VN = s.NAME_VN,
                                        .COL_INDEX = s.COL_INDEX,
                                        .CREATED_DATE = s.CREATED_DATE,
                                        .IS_IMPORT = s.IS_IMPORT,
                                        .OBJ_SAL_ID = s.OBJ_SAL_ID
                                    }).Where(Function(f) f.IS_IMPORT = -1 And f.OBJ_SAL_ID = 1)
            lst = lst.OrderBy("COL_INDEX ASC")
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetSalaryList_TYPE(ByVal POBJ_SAL_ID As Decimal) As List(Of PAListSalariesDTO)
        Try
            Dim query = From p In Context.PA_LISTSALARIES
             Where ((POBJ_SAL_ID = 2 And p.OBJ_SAL_ID = 1) Or p.OBJ_SAL_ID = POBJ_SAL_ID) And p.IS_IMPORT = -1
            Dim lst = query.Select(Function(s) New PAListSalariesDTO With {
                                        .ID = s.ID,
                                        .COL_NAME = s.COL_NAME,
                                        .NAME_EN = s.NAME_EN,
                                        .NAME_VN = s.COL_NAME & " : " & s.NAME_VN,
                                        .COL_INDEX = s.COL_INDEX,
                                        .CREATED_DATE = s.CREATED_DATE,
                                        .IS_IMPORT = s.IS_IMPORT,
                                        .OBJ_SAL_ID = s.OBJ_SAL_ID
                                    })
            lst = lst.OrderBy("COL_INDEX ASC")
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function SaveImport(ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean
        Try
            Using conMng As New DataAccess.ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Dim cmd As New OracleCommand
                    Dim sqlInsert = lstColVal.Aggregate(Function(cur, [next]) cur & "," & [next])
                    Dim sqlInsert_Temp As String
                    conn.Open()
                    cmd.Connection = conn
                    cmd.Transaction = cmd.Connection.BeginTransaction()
                    RecordSussces = 0
                    For Each dr As DataRow In dtData.Rows
                        If dr("ID").ToString() Is DBNull.Value OrElse dr("ID").ToString() = "" Then
                            Continue For
                        End If
                        sqlInsert_Temp = "," & sqlInsert & ","
                        Dim sqlInsertVal = ""
                        For Each parm As String In lstColVal
                            If Not dr(parm).ToString Is DBNull.Value AndAlso dr(parm).ToString <> "" Then
                                If Not Integer.TryParse(dr(parm).ToString(), 1) Then
                                    sqlInsertVal &= "'" & dr(parm).ToString & "',"
                                Else
                                    sqlInsertVal &= dr(parm).ToString & ","
                                End If
                            Else
                                sqlInsert_Temp = sqlInsert_Temp.Replace("," & parm & ",", ",")
                            End If
                        Next
                        If sqlInsertVal <> "" Then
                            sqlInsertVal = sqlInsertVal.Remove(sqlInsertVal.Length - 1, 1)
                        End If
                        If sqlInsert_Temp = "," Then
                            Continue For
                        End If
                        If sqlInsert_Temp <> "" Then
                            sqlInsert_Temp = sqlInsert_Temp.Remove(0, 1)
                            sqlInsert_Temp = sqlInsert_Temp.Remove(sqlInsert_Temp.Length - 1, 1)
                        End If
                        cmd.CommandText = "PKG_PA_BUSINESS.IMPORT_SALARY"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Clear()
                        cmd.Parameters.Add("P_SALARY_GROUP_ID", SalaryGroup).Value = SalaryGroup
                        cmd.Parameters.Add("P_PERIOD_SALARY_ID", Period).Value = Period
                        cmd.Parameters.Add("P_EMPLOYEE_ID", dr("ID"))
                        cmd.Parameters.Add("P_CREATED_USER", log.Username)
                        cmd.Parameters.Add("P_CREATED_LOG", log.Ip)
                        cmd.Parameters.Add("P_LISTCOL", sqlInsert_Temp)
                        cmd.Parameters.Add("P_LISTVAL", sqlInsertVal)

                        Dim r As Integer = 0
                        r = cmd.ExecuteNonQuery()
                        RecordSussces += 1
                    Next
                    cmd.Transaction.Commit()
                    cmd.Dispose()
                    conn.Close()
                    conn.Dispose()
                End Using
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function SaveImportBonus(ByVal Org_Id As Decimal, ByVal Year As Decimal, ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean
        Try
            Using conMng As New DataAccess.ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Dim cmd As New OracleCommand
                    Dim sqlInsert = lstColVal.Aggregate(Function(cur, [next]) cur & "," & [next])

                    conn.Open()
                    cmd.Connection = conn
                    cmd.Transaction = cmd.Connection.BeginTransaction()
                    RecordSussces = 0
                    For Each dr As DataRow In dtData.Rows
                        If sqlInsert.Contains(dr("PAYMENTSOURCES_ID")) Then
                            cmd.CommandText = "PKG_PA_BUSINESS.IMPORT_BONUS"
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.Clear()
                            cmd.Parameters.Add("P_ORG_ID", Org_Id).Value = Org_Id
                            cmd.Parameters.Add("P_YEAR", Year).Value = Year
                            cmd.Parameters.Add("P_SALARY_GROUP_ID", SalaryGroup).Value = SalaryGroup
                            cmd.Parameters.Add("P_PERIOD_SALARY_ID", Period).Value = Period
                            cmd.Parameters.Add("P_CREATED_USER", log.Username)
                            cmd.Parameters.Add("P_CREATED_LOG", log.Ip)
                            cmd.Parameters.Add("P_PAYMENTSOURCES_ID", dr("PAYMENTSOURCES_ID")).Value = dr("PAYMENTSOURCES_ID")
                            cmd.Parameters.Add("P_MONEY", dr("MONEY")).Value = dr("MONEY")
                            Dim r As Integer = 0
                            r = cmd.ExecuteNonQuery()
                            RecordSussces += 1
                        End If
                    Next
                    cmd.Transaction.Commit()
                    cmd.Dispose()
                    conn.Close()
                    conn.Dispose()
                End Using
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function SaveImportSalary_Fund_Mapping(ByVal Year As Decimal, ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean
        Try
            Using conMng As New DataAccess.ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Dim cmd As New OracleCommand
                    conn.Open()
                    cmd.Connection = conn
                    cmd.Transaction = cmd.Connection.BeginTransaction()
                    RecordSussces = 0
                    For Each dr As DataRow In dtData.Rows
                        For Each dc As DataColumn In dtData.Columns
                            If (dc.ColumnName <> "COL_NAME" And dc.ColumnName <> "NAME_VN") AndAlso Not dr(dc.ColumnName) Is DBNull.Value AndAlso dr(dc.ColumnName) = "1" Then
                                cmd.CommandText = "PKG_PA_BUSINESS.IMPORT_SALARY_FUND_MAPPING"
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.Parameters.Clear()
                                cmd.Parameters.Add("P_YEAR", Year).Value = Year
                                cmd.Parameters.Add("P_PERIOD_ID", Period).Value = Period
                                cmd.Parameters.Add("P_SALARY_GROUP", SalaryGroup).Value = SalaryGroup

                                cmd.Parameters.Add("P_SALARY_NAME", dr("COL_NAME")).Value = dr("COL_NAME")
                                cmd.Parameters.Add("P_SALARY_FUND", dc.ColumnName).Value = dc.ColumnName

                                cmd.Parameters.Add("P_CREATED_BY", log.Username)
                                cmd.Parameters.Add("P_CREATED_LOG", log.Ip)
                                Dim r As Integer = 0
                                r = cmd.ExecuteNonQuery()
                                RecordSussces += 1
                            End If
                        Next
                    Next
                    cmd.Transaction.Commit()
                    cmd.Dispose()
                    conn.Close()
                    conn.Dispose()
                End Using
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

#Region "IPORTAL - View phiếu lương"
    Public Function GetPayrollSheetSum(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable

        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.LOAD_PAYROLL_SHEET_SUM",
                                           New With {.P_PERIOD_ID = PeriodId,
                                                     .P_EMPLOYEE = EmployeeId,
                                                     .P_SORT = Sorts,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetPayrollSheetSumSheet(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog) As DataTable

        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.LOAD_PAYROLL_SHEET",
                                           New With {.P_PERIOD_ID = PeriodId,
                                                     .P_EMPLOYEE = EmployeeId,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function CHECK_OPEN_CLOSE(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog) As DataTable

        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.CHECK_OPEN_CLOSE",
                                           New With {.P_PERIOD_ID = PeriodId,
                                                     .P_EMPLOYEE = EmployeeId,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function


    Public Function CheckPeriod(ByVal PeriodId As Integer, ByVal EmployeeId As Decimal) As Boolean
        Try
            'Dim emp As HU_EMPLOYEE
            'emp = (From p In Context.HU_EMPLOYEE Where p.ID = EmployeeId).FirstOrDefault

            'Dim query = (From p In Context.AT_ORG_PERIOD
            '             Where p.PERIOD_ID = PeriodId And p.ORG_ID = emp.ORG_ID).FirstOrDefault

            'If query IsNot Nothing Then
            '    Return query.STATUSCOLEX = 0
            'Else
            '    Return (query Is Nothing)
            'End If
            Dim empquery = (From p In Context.HU_EMPLOYEE Where p.ID = EmployeeId)
            Dim emp = empquery.Select(Function(p) New EmployeeDTO With {
                                       .ID = p.ID,
                                       .ORG_ID = p.ORG_ID
                                       }).FirstOrDefault
            Dim query = (From p In Context.AT_ORG_PERIOD
                         Where p.PERIOD_ID = PeriodId And p.ORG_ID = emp.ORG_ID).FirstOrDefault

            If query IsNot Nothing Then
                Return query.STATUSCOLEX = 0
            Else
                Return (query Is Nothing)
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

#End Region

    Public Function GetImportSalaryTNCN(ByVal obj_sal_id As Integer, ByVal taxId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve})
            End Using
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA_IMPORT_TNCN",
                                           New With {.P_USERNAME = log.Username,
                                                     .P_ORG_ID = OrgId,
                                                     .P_PERIOD_TAX = taxId,
                                                     .P_OBJ_SAL_ID = obj_sal_id,
                                                     .P_EMPLOYEE_ID = EmployeeId,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function SaveImportTNCN(ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal taxId As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean
        Try
            Using conMng As New DataAccess.ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Dim cmd As New OracleCommand
                    Dim sqlInsert = lstColVal.Aggregate(Function(cur, [next]) cur & "," & [next])
                    Dim sqlInsert_Temp As String
                    conn.Open()
                    cmd.Connection = conn
                    cmd.Transaction = cmd.Connection.BeginTransaction()
                    RecordSussces = 0
                    For Each dr As DataRow In dtData.Rows
                        If dr("ID").ToString() Is DBNull.Value OrElse dr("ID").ToString() = "" Then
                            Continue For
                        End If
                        sqlInsert_Temp = "," & sqlInsert & ","
                        Dim sqlInsertVal = ""
                        For Each parm As String In lstColVal
                            If Not dr(parm).ToString Is DBNull.Value AndAlso dr(parm).ToString <> "" Then
                                If Not Integer.TryParse(dr(parm).ToString(), 1) Then
                                    sqlInsertVal &= "'" & dr(parm).ToString.Replace(".", Nothing).Replace(",", ".") & "',"
                                Else
                                    sqlInsertVal &= dr(parm).ToString & ","
                                End If
                            Else
                                sqlInsert_Temp = sqlInsert_Temp.Replace("," & parm & ",", ",")
                            End If
                        Next
                        If sqlInsertVal <> "" Then
                            sqlInsertVal = sqlInsertVal.Remove(sqlInsertVal.Length - 1, 1)
                        End If
                        If sqlInsert_Temp = "," Then
                            Continue For
                        End If
                        If sqlInsert_Temp <> "" Then
                            sqlInsert_Temp = sqlInsert_Temp.Remove(0, 1)
                            sqlInsert_Temp = sqlInsert_Temp.Remove(sqlInsert_Temp.Length - 1, 1)
                        End If
                        cmd.CommandText = "PKG_PA_BUSINESS.IMPORT_SALARY_TNCN"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Clear()
                        cmd.Parameters.Add("P_SALARY_GROUP_ID", SalaryGroup).Value = SalaryGroup
                        cmd.Parameters.Add("P_PERIOD_SALARY_ID", Period).Value = Period
                        cmd.Parameters.Add("P_EMPLOYEE_ID", dr("ID"))
                        cmd.Parameters.Add("P_PERIOD_TAX", taxId).Value = taxId
                        cmd.Parameters.Add("P_CREATED_USER", log.Username)
                        cmd.Parameters.Add("P_CREATED_LOG", log.Ip)
                        cmd.Parameters.Add("P_LISTCOL", sqlInsert_Temp)
                        cmd.Parameters.Add("P_LISTVAL", sqlInsertVal)

                        Dim r As Integer = 0
                        r = cmd.ExecuteNonQuery()
                        RecordSussces += 1
                    Next
                    cmd.Transaction.Commit()
                    cmd.Dispose()
                    conn.Close()
                    conn.Dispose()
                End Using
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function


End Class

