Imports Framework.Data
Imports LinqKit
Imports System.Data.Objects.DataClasses
Imports System.Data.Common
Imports System.Data.Entity
Imports System.Threading
Imports Framework.Data.System.Linq.Dynamic

Imports System.Configuration
Imports System.Reflection

Partial Public Class AttendanceRepository

#Region "kỲ CÔNG"
    Public Function LOAD_PERIOD(ByVal obj As AT_PERIODDTO, ByVal log As UserLog) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.LOAD_PERIOD",
                                               New With {.P_ORG_ID = obj.ORG_ID,
                                                         .P_YEAR = obj.YEAR,
                                                         .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Check trạng thái kỳ công
    ''' </summary>
    ''' <param name="_param"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IS_PERIODSTATUS(ByVal _param As PARAMDTO, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim iCount = (From p In Context.AT_PERIOD
                        From po In Context.AT_ORG_PERIOD.Where(Function(f) f.PERIOD_ID = p.ID)
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) po.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where p.ID = _param.PERIOD_ID).Count
            If iCount = 0 Then
                Return True
            End If

            Dim query = (From p In Context.AT_PERIOD
                        From po In Context.AT_ORG_PERIOD.Where(Function(f) f.PERIOD_ID = p.ID)
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) po.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where po.STATUSCOLEX = 1 And
                        p.ID = _param.PERIOD_ID And
                        po.ORG_ID <> 1).Any

            Return query

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Check trạng thái tính kỳ công
    ''' </summary>
    ''' <param name="_param"></param>
    ''' <param name="isAfter"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IS_PERIOD_PAYSTATUS(ByVal _param As ParamDTO, ByVal isAfter As Boolean, ByVal log As UserLog) As Boolean
        Try
            Dim period_id As Decimal = 0
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            period_id = _param.PERIOD_ID

            If isAfter Then
                Dim endDate_after = (From p In Context.AT_PERIOD
                              Where p.ID = _param.PERIOD_ID
                              Select p.END_DATE).FirstOrDefault
                If endDate_after IsNot Nothing Then
                    endDate_after = endDate_after.Value.AddDays(-1)
                    Dim period_after = (From p In Context.AT_PERIOD
                                        Where p.END_DATE = _param.ENDDATE).FirstOrDefault

                    If period_after IsNot Nothing Then
                        period_id = period_after.ID
                    End If
                End If
            End If

            Dim query = (From p In Context.AT_PERIOD
                        From po In Context.AT_ORG_PERIOD.Where(Function(f) f.PERIOD_ID = p.ID)
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) po.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where po.STATUSPAROX = _param.STATUS And
                        p.ID = period_id And
                        po.ORG_ID <> 1).Any

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function IS_PERIODSTATUS_BY_DATE(ByVal _param As PARAMDTO, ByVal log As UserLog) As Boolean
        Try
            Dim emp As HU_EMPLOYEE
            If Not String.IsNullOrEmpty(_param.EMPLOYEE_CODE) Then
                emp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE.ToLower().Equals(_param.EMPLOYEE_CODE.ToLower())).FirstOrDefault
            Else
                emp = (From p In Context.HU_EMPLOYEE Where p.ID = _param.EMPLOYEE_ID).FirstOrDefault
            End If

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = emp.ORG_ID,
                                           .P_ISDISSOLVE = False})
            End Using

            Dim exists = (From p In Context.AT_PERIOD
                        From po In Context.AT_ORG_PERIOD.Where(Function(f) f.PERIOD_ID = p.ID)
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) po.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where po.STATUSCOLEX = 1 And p.START_DATE <= _param.FROMDATE And
                        p.END_DATE >= _param.FROMDATE And po.ORG_ID <> 1).Any
            ' NEU KY CONG MO
            If exists <= 0 And _param.ENDDATE.HasValue Then
                exists = (From p In Context.AT_PERIOD
                        From po In Context.AT_ORG_PERIOD.Where(Function(f) f.PERIOD_ID = p.ID)
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) po.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where po.STATUSCOLEX = 1 And p.START_DATE <= _param.ENDDATE And
                        p.END_DATE >= _param.ENDDATE And po.ORG_ID <> 1).Any
            End If
            Return exists
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function LOAD_PERIODBylinq(ByVal obj As AT_PERIODDTO, ByVal log As UserLog) As List(Of AT_PERIODDTO)
        Try
            Dim query = From p In Context.AT_PERIOD
                                Where p.YEAR = obj.YEAR
                                Order By p.START_DATE
            Dim lst = query.Select(Function(p) New AT_PERIODDTO With {
                                       .PERIOD_ID = p.ID,
                                       .YEAR = p.YEAR,
                                       .PERIOD_NAME = p.PERIOD_NAME,
                                       .START_DATE = p.START_DATE,
                                       .END_DATE = p.END_DATE}).ToList
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function LOAD_PERIODByID(ByVal obj As AT_PERIODDTO, ByVal log As UserLog) As AT_PERIODDTO
        Try
            Dim query = From p In Context.AT_PERIOD
                        Where p.ID = obj.PERIOD_ID
            Dim lst = query.Select(Function(p) New AT_PERIODDTO With {
                                       .PERIOD_ID = p.ID,
                                       .YEAR = p.YEAR,
                                       .PERIOD_NAME = p.PERIOD_NAME,
                                       .START_DATE = p.START_DATE,
                                       .END_DATE = p.END_DATE}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function CLOSEDOPEN_PERIOD(ByVal param As ParamDTO, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CLOSEDOPEN_PERIOD",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_STATUS = param.STATUS,
                                                         .P_PERIOD_ID = param.PERIOD_ID})
            End Using

            If param.STATUS = 0 Then
                ' TINH phep nam
                Using cls As New DataAccess.NonQueryData
                    cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CALL_ENTITLEMENT",
                                                   New With {.P_USERNAME = log.Username.ToUpper,
                                                             .P_ORG_ID = param.ORG_ID,
                                                             .P_PERIOD_ID = param.PERIOD_ID,
                                                             .P_ISDISSOLVE = param.IS_DISSOLVE})
                End Using
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    

#End Region

#Region "Ham Dung chung"
    Public Function CheckPeriodClose(ByVal lstEmp As List(Of Decimal),
                                       ByVal startdate As Date, ByVal enddate As Date, ByRef sAction As String) As Boolean
        Dim lstPeriod As List(Of AT_PERIOD)
        Try
            lstPeriod = (From p In Context.AT_PERIOD
                             Where (startdate >= p.START_DATE And startdate <= p.END_DATE) _
                             Or (enddate >= p.START_DATE And enddate <= p.END_DATE) _
                             Or (startdate <= p.START_DATE And enddate >= p.END_DATE)).ToList

            If lstPeriod.Count = 0 Then
                sAction = "Chưa thiết lập kỳ công. Thao tác thực hiện không thành công"
                Return True
            End If

            Dim lstOrg = (From p In Context.HU_EMPLOYEE Where lstEmp.Contains(p.ID) Select p.ORG_ID).ToList
            For Each item In lstPeriod
                Dim lstStatus = (From p In Context.AT_ORG_PERIOD Where p.PERIOD_ID = item.ID And lstOrg.Contains(p.ORG_ID)).ToList
                For Each status In lstStatus
                    If status.STATUSCOLEX = 0 Then
                        sAction = "Bảng công hiện tại đã khóa. Thao tác thực hiện không thành công."
                        Return False
                    End If
                Next
            Next
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, ".CheckPeriodClose")
        End Try
    End Function
#End Region

#Region "Máy chấm công"
    Public Function GetTerminal(ByVal obj As AT_TERMINALSDTO, ByVal log As UserLog) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_TERMINAL",
                                               New With {.P_ID = obj.ID,
                                                         .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetTerminalAuto() As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_TERMINAL_AUTO",
                                               New With {.P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function UpdateTerminalLastTime(ByVal obj As AT_TERMINALSDTO) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_UPDATE_TERMINAL_LASTIME",
                                 New With {.P_ID = obj.ID,
                                           .P_DATE = obj.MODIFIED_DATE})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function UpdateTerminalStatus(ByVal obj As AT_TERMINALSDTO) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_STATUS_TERMINAL_LASTIME",
                                 New With {.P_ID = obj.ID,
                                           .P_DATE = obj.MODIFIED_DATE,
                                           .P_STATUS = obj.TERMINAL_STATUS,
                                           .P_ROW = obj.TERMINAL_ROW})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "Export Data By Org"
    Public Function GetDataFromOrg(ByVal obj As PARAMDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GETDATAFROMORG",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = obj.ORG_ID,
                                                         .P_ISDISSOLVE = obj.IS_DISSOLVE,
                                                         .P_EXPORT_TYPE = obj.P_EXPORT_TYPE,
                                                         .P_PERIOD_ID = obj.PERIOD_ID,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CUR2 = cls.OUT_CURSOR,
                                                         .P_CUR3 = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

End Class
