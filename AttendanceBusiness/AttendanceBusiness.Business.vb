Imports AttendanceBusiness.ServiceContracts
Imports AttendanceDAL
Imports Framework.Data
Imports System.Collections.Generic
Imports LinqKit

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace AttendanceBusiness.ServiceImplementations
    Partial Public Class AttendanceBusiness
        Function ReadCheckInOutData(ByVal dateFrom As Date, ByVal dateTo As Date) As Boolean Implements IAttendanceBusiness.ReadCheckInOutData
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.ReadCheckInOutData(dateFrom, dateTo)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Return False
            End Try
        End Function
        Function Upd_TimeTImesheetMachines(ByVal LstObj As List(Of AT_TIME_TIMESHEET_MACHINETDTO), Optional ByVal log As UserLog = Nothing) As Boolean Implements IAttendanceBusiness.Upd_TimeTImesheetMachines
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.Upd_TimeTImesheetMachines(LstObj, log)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Return False
            End Try
        End Function
        Function IMPORT_TIMESHEET_MACHINE(ByVal ListobjImport As List(Of AT_TIME_TIMESHEET_MACHINETDTO)) As Boolean Implements IAttendanceBusiness.IMPORT_TIMESHEET_MACHINE
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.IMPORT_TIMESHEET_MACHINE(ListobjImport)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Return False
            End Try
        End Function
#Region "Quan ly vao ra"
        Function GetDataInout(ByVal _filter As AT_DATAINOUTDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "EMPLOYEE_CODE, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_DATAINOUTDTO) Implements IAttendanceBusiness.GetDataInout

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetDataInout(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertDataInout(ByVal lstDataInout As List(Of AT_DATAINOUTDTO), ByVal fromDate As Date, ByVal toDate As Date,
                                        ByVal log As UserLog) As Boolean _
                                    Implements ServiceContracts.IAttendanceBusiness.InsertDataInout
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertDataInout(lstDataInout, fromDate, toDate, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyDataInout(ByVal objDataInout As AT_DATAINOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyDataInout
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyDataInout(objDataInout, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function DeleteDataInout(ByVal lstDataInout() As AT_DATAINOUTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteDataInout
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteDataInout(lstDataInout)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "lam bu"
        Public Function CALCULATE_ENTITLEMENT_NB(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As Framework.Data.UserLog) As Boolean Implements IAttendanceBusiness.CALCULATE_ENTITLEMENT_NB

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CALCULATE_ENTITLEMENT_NB(param, listEmployeeId, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function GetNB(ByVal _filter As AT_COMPENSATORYDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_COMPENSATORYDTO) Implements IAttendanceBusiness.GetNB

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetNB(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "PHEP NAM"
        Public Function GetEntitlement(ByVal _filter As AT_ENTITLEMENTDTO,
                                 ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_ENTITLEMENTDTO) Implements IAttendanceBusiness.GetEntitlement

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetEntitlement(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CALCULATE_ENTITLEMENT(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As Framework.Data.UserLog) As Boolean Implements IAttendanceBusiness.CALCULATE_ENTITLEMENT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CALCULATE_ENTITLEMENT(param, listEmployeeId, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CALCULATE_ENTITLEMENT_HOSE(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As Framework.Data.UserLog) As Boolean Implements IAttendanceBusiness.CALCULATE_ENTITLEMENT_HOSE

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CALL_ENTITLEMENT_HOSE(param, listEmployeeId, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function AT_ENTITLEMENT_PREV_HAVE(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As Framework.Data.UserLog) As Boolean Implements IAttendanceBusiness.AT_ENTITLEMENT_PREV_HAVE

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.AT_ENTITLEMENT_PREV_HAVE(param, listEmployeeId, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckPeriodMonth(ByVal year As Integer, ByVal PeriodId As Integer, ByRef PeriodNext As Integer) As Boolean Implements IAttendanceBusiness.CheckPeriodMonth

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CheckPeriodMonth(year, PeriodId, PeriodNext)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ImportEntitlementLeave(ByVal P_DOCXML As String, ByVal P_USER As String, ByVal P_PERIOD As Decimal) As Boolean Implements IAttendanceBusiness.ImportEntitlementLeave

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.ImportEntitlementLeave(P_DOCXML, P_USER, P_PERIOD)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "XEP CA"

        Public Function GET_WORKSIGN(ByVal param As AT_WORKSIGNDTO, ByVal log As Framework.Data.UserLog) As System.Data.DataSet Implements IAttendanceBusiness.GET_WORKSIGN

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_WORKSIGN(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertWorkSign(ByVal objWorkSigns As List(Of AT_WORKSIGNDTO), ByVal objWork As AT_WORKSIGNDTO, ByVal p_fromdate As Date, ByVal p_endDate As Date?, ByVal log As UserLog, ByRef gID As Decimal) Implements ServiceContracts.IAttendanceBusiness.InsertWorkSign
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertWorkSign(objWorkSigns, objWork, p_fromdate, p_endDate, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertWORKSIGNByImport(ByVal dtData As DataTable,
                                           ByVal period_id As Decimal,
                                           ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertWORKSIGNByImport
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertWorkSignByImport(dtData, period_id, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateWORKSIGN(ByVal objWORKSIGN As AT_WORKSIGNDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateWORKSIGN
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateWorkSign(objWORKSIGN)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyWORKSIGN(ByVal objWORKSIGN As AT_WORKSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyWORKSIGN
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyWorkSign(objWORKSIGN, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function DeleteWORKSIGN(ByVal lstWORKSIGN() As AT_WORKSIGNDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteWORKSIGN
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteWorkSign(lstWORKSIGN)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GETSIGNDEFAULT(ByVal param As ParamDTO, ByVal log As Framework.Data.UserLog) As System.Data.DataTable Implements IAttendanceBusiness.GETSIGNDEFAULT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GETSIGNDEFAULT(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function Del_WorkSign_ByEmp(ByVal employee_id As String, ByVal p_From As Date, ByVal p_to As Date) As Boolean Implements ServiceContracts.IAttendanceBusiness.Del_WorkSign_ByEmp
            Using rep As New AttendanceRepository
                Try

                    Return rep.Del_WorkSign_ByEmp(employee_id, p_From, p_to)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "XEP CA"

        Public Function GET_ProjectAssign(ByVal param As AT_PROJECT_ASSIGNDTO, ByVal log As Framework.Data.UserLog) As System.Data.DataSet Implements IAttendanceBusiness.GET_ProjectAssign

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_ProjectAssign(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertProjectAssign(ByVal objProjectAssigns As List(Of AT_PROJECT_ASSIGNDTO), ByVal objWork As AT_PROJECT_ASSIGNDTO, ByVal p_fromdate As Date, ByVal p_endDate As Date?, ByVal log As UserLog, ByRef gID As Decimal) Implements ServiceContracts.IAttendanceBusiness.InsertProjectAssign
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertProjectAssign(objProjectAssigns, objWork, p_fromdate, p_endDate, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function


        Public Function ModifyProjectAssign(ByVal objProjectAssign As AT_PROJECT_ASSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyProjectAssign
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyProjectAssign(objProjectAssign, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function DeleteProjectAssign(ByVal lstProjectAssign() As AT_PROJECT_ASSIGNDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteProjectAssign
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteProjectAssign(lstProjectAssign)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "May cham cong"
        Function IMPORT_AT_SWIPE_DATA(ByVal log As UserLog, ByVal DATA_IN As String) As Boolean Implements IAttendanceBusiness.IMPORT_AT_SWIPE_DATA
            Try
                Using rep As New AttendanceRepository
                    Try
                        Dim isSusses = rep.IMPORT_AT_SWIPE_DATA(log, DATA_IN)
                        Return isSusses
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function



        Function GetSwipeData(ByVal _filter As AT_SWIPE_DATADTO,
                              Optional ByVal PageIndex As Integer = 0,
                              Optional ByVal PageSize As Integer = Integer.MaxValue,
                              Optional ByRef Total As Integer = 0,
                              Optional ByVal Sorts As String = "iTime_id, VALTIME desc") As List(Of AT_SWIPE_DATADTO) Implements IAttendanceBusiness.GetSwipeData

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetSwipeData(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Function InsertSwipeData(ByVal objSwipeData As List(Of AT_SWIPE_DATADTO), ByVal machine As AT_TERMINALSDTO, ByVal P_FROMDATE As Date?, ByVal P_ENDDATE As Date?, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IAttendanceBusiness.InsertSwipeData

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.InsertSwipeData(objSwipeData, machine, P_FROMDATE, P_ENDDATE, log, gID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function ImportSwipeDataAuto(ByVal lstSwipeData As List(Of AT_SWIPE_DATADTO)) As Boolean _
            Implements IAttendanceBusiness.ImportSwipeDataAuto

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.ImportSwipeDataAuto(lstSwipeData)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function InsertSwipeDataImport(ByVal objDelareRice As List(Of AT_SWIPE_DATADTO), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IAttendanceBusiness.InsertSwipeDataImport

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.InsertSwipeDataImport(objDelareRice, log, gID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function ImportSwipeData(ByVal dtData As DataTable, ByVal log As UserLog) As Boolean Implements IAttendanceBusiness.ImportSwipeData

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.ImportSwipeData(dtData, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Di Muon ve som"
        Function GetLate_combackout(ByVal _filter As AT_LATE_COMBACKOUTDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LATE_COMBACKOUTDTO) Implements IAttendanceBusiness.GetDSVM

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetDSVM(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetLate_CombackoutById(ByVal _id As Decimal?) As AT_LATE_COMBACKOUTDTO Implements ServiceContracts.IAttendanceBusiness.GetLate_CombackoutById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetLate_CombackoutById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ImportLate_combackout(ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ImportLate_combackout
            Using rep As New AttendanceRepository
                Try

                    Return rep.ImportLate_combackout(objLate_combackout, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertLate_combackout(ByVal objRegisterDMVSList As List(Of AT_LATE_COMBACKOUTDTO), ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertLate_combackout
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertLate_combackout(objRegisterDMVSList, objLate_combackout, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateLate_combackout(ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateLate_combackout
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateLate_combackout(objLate_combackout)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyLate_combackout(ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyLate_combackout
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyLate_combackout(objLate_combackout, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteLate_combackout(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteLate_combackout
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteLate_combackout(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Dang ly lam them"
        Function GetRegisterOT(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_OT_REGISTRATIONDTO) Implements IAttendanceBusiness.GetRegisterOT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetRegisterOT(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetRegisterById(ByVal _id As Decimal?) As AT_REGISTER_OTDTO Implements ServiceContracts.IAttendanceBusiness.GetRegisterById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetRegisterById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ValidateRegisterOT(ByVal objLate_combackout As AT_REGISTER_OTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateRegisterOT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateRegisterOT(objLate_combackout)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertRegisterOT(ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertRegisterOT
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertRegisterOT(objRegisterOT, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertDataRegisterOT(ByVal objRegisterOTList As List(Of AT_REGISTER_OTDTO), ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertDataRegisterOT
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertDataRegisterOT(objRegisterOTList, objRegisterOT, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyRegisterOT(ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyRegisterOT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyRegisterOT(objRegisterOT, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function DeleteRegisterOT(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteRegisterOT
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteRegisterOT(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckImporAddNewtOT(ByVal objRegisterOT As AT_REGISTER_OTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.CheckImporAddNewtOT
            Using rep As New AttendanceRepository
                Try
                    Return rep.CheckImporAddNewtOT(objRegisterOT)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckDataListImportAddNew(ByVal objRegisterOTList As List(Of AT_REGISTER_OTDTO), ByVal objRegisterOT As AT_REGISTER_OTDTO, ByRef strEmployeeCode As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.CheckDataListImportAddNew
            Using rep As New AttendanceRepository
                Try
                    Return rep.CheckDataListImportAddNew(objRegisterOTList, objRegisterOT, strEmployeeCode)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function


        Public Function ApproveRegisterOT(ByVal lstData As List(Of AT_REGISTER_OTDTO),
                                          ByVal status_id As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ApproveRegisterOT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ApproveRegisterOT(lstData, status_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Bang cham cong may"
        Function GetMachines(ByVal _filter As AT_TIME_TIMESHEET_MACHINETDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "EMPLOYEE_ID, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MACHINETDTO) Implements IAttendanceBusiness.GetMachines

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetMachines(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function Init_TimeTImesheetMachines(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal p_fromdate As Date, ByVal p_enddate As Date, ByVal P_ORG_ID As Decimal, ByVal lstEmployee As List(Of Decimal?), ByVal p_delAll As Decimal, ByVal codecase As String) As Boolean Implements IAttendanceBusiness.Init_TimeTImesheetMachines

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.Init_TimeTImesheetMachines(_param, log, p_fromdate, p_enddate, P_ORG_ID, lstEmployee, p_delAll, codecase)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Bang cham cong tay"
        Public Function GetCCT(ByVal param As AT_TIME_TIMESHEET_DAILYDTO,
                               ByVal log As Framework.Data.UserLog) As System.Data.DataSet _
                           Implements IAttendanceBusiness.GetCCT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetCCT(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetCCT_Origin(ByVal param As AT_TIME_TIMESHEET_DAILYDTO,
                                      ByVal log As Framework.Data.UserLog) As DataTable _
            Implements IAttendanceBusiness.GetCCT_Origin

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetCCT_Origin(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function ModifyLeaveSheetDaily(ByVal objLeave As AT_TIME_TIMESHEET_DAILYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IAttendanceBusiness.ModifyLeaveSheetDaily

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.ModifyLeaveSheetDaily(objLeave, log, gID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function InsertLeaveSheetDaily(ByVal dtData As DataTable, ByVal log As UserLog, ByVal PeriodID As Decimal) As DataTable Implements IAttendanceBusiness.InsertLeaveSheetDaily

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.InsertLeaveSheetDaily(dtData, log, PeriodID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetTimeSheetDailyById(ByVal obj As AT_TIME_TIMESHEET_DAILYDTO) As AT_TIME_TIMESHEET_DAILYDTO Implements ServiceContracts.IAttendanceBusiness.GetTimeSheetDailyById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetTimeSheetDailyById(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Bang cong lam them"

        Function Cal_TimeTImesheet_OT(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal p_period_id As Decimal?, ByVal P_ORG_ID As Decimal, ByVal lstEmployee As List(Of Decimal?)) As Boolean Implements IAttendanceBusiness.Cal_TimeTImesheet_OT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.Cal_TimeTImesheet_OT(_param, log, p_period_id, P_ORG_ID, lstEmployee)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetSummaryOT(ByVal param As AT_TIME_TIMESHEET_OTDTO, ByVal log As Framework.Data.UserLog) As System.Data.DataSet Implements IAttendanceBusiness.GetSummaryOT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetSummaryOT(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Function Cal_TimeTImesheet_NB(ByVal _param As ParamDTO,
                                      ByVal log As UserLog,
                                      ByVal p_period_id As Decimal?,
                                      ByVal P_ORG_ID As Decimal,
                                      ByVal lstEmployee As List(Of Decimal?)) As Boolean _
                                  Implements IAttendanceBusiness.Cal_TimeTImesheet_NB

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.Cal_TimeTImesheet_NB(_param, log, p_period_id, P_ORG_ID, lstEmployee)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetSummaryNB(ByVal param As AT_TIME_TIMESHEET_NBDTO,
                                     ByVal log As Framework.Data.UserLog) As System.Data.DataSet _
            Implements IAttendanceBusiness.GetSummaryNB

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetSummaryNB(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Function ModifyLeaveSheetOt(ByVal objLeave As AT_TIME_TIMESHEET_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IAttendanceBusiness.ModifyLeaveSheetOt

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.ModifyLeaveSheetOt(objLeave, log, gID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function InsertLeaveSheetOt(ByVal objLeave As AT_TIME_TIMESHEET_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IAttendanceBusiness.InsertLeaveSheetOt

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.InsertLeaveSheetOt(objLeave, log, gID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetTimeSheetOtById(ByVal obj As AT_TIME_TIMESHEET_OTDTO) As AT_TIME_TIMESHEET_OTDTO Implements ServiceContracts.IAttendanceBusiness.GetTimeSheetOtById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetTimeSheetOtById(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Tổng hợp công"
        Function GetTimeSheet(ByVal _filter As AT_TIME_TIMESHEET_MONTHLYDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MONTHLYDTO) Implements IAttendanceBusiness.GetTimeSheet

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetTimeSheet(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CAL_TIME_TIMESHEET_MONTHLY(ByVal param As ParamDTO, ByVal codecase As String, ByVal lstEmployee As List(Of Decimal?), ByVal log As Framework.Data.UserLog) As Boolean Implements IAttendanceBusiness.CAL_TIME_TIMESHEET_MONTHLY

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CAL_TIME_TIMESHEET_MONTHLY(param, codecase, lstEmployee, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateTimesheet(ByVal _validate As AT_TIME_TIMESHEET_MONTHLYDTO, ByVal sType As String, ByVal log As UserLog) _
            Implements IAttendanceBusiness.ValidateTimesheet

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.ValidateTimesheet(_validate, sType, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Dang Ky công"
        Function GetLeaveSheet(ByVal _filter As AT_LEAVESHEETDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LEAVESHEETDTO) Implements IAttendanceBusiness.GetLeaveSheet

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetLeaveSheet(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetLeaveById(ByVal _id As Decimal?) As AT_LEAVESHEETDTO Implements ServiceContracts.IAttendanceBusiness.GetLeaveById
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetLeaveById(_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTotalPHEPNAM(ByVal P_EMPLOYEE_ID As Integer,
                                      ByVal P_YEAR As Integer,
                                      ByVal P_TYPE_LEAVE_ID As Integer) As DataTable Implements ServiceContracts.IAttendanceBusiness.GetTotalPHEPNAM
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetTotalPHEPNAM(P_EMPLOYEE_ID, P_YEAR, P_TYPE_LEAVE_ID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTotalPHEPBU(ByVal P_EMPLOYEE_ID As Integer,
                                     ByVal P_YEAR As Integer,
                                     ByVal P_TYPE_LEAVE_ID As Integer) As DataTable Implements ServiceContracts.IAttendanceBusiness.GetTotalPHEPBU
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetTotalPHEPBU(P_EMPLOYEE_ID, P_YEAR, P_TYPE_LEAVE_ID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTotalDAY(ByVal P_EMPLOYEE_ID As Integer,
                                ByVal P_TYPE_MANUAL As Integer,
                                ByVal P_FROM_DATE As Date,
                                ByVal P_TO_DATE As Date) As DataTable Implements ServiceContracts.IAttendanceBusiness.GetTotalDAY
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetTotalDAY(P_EMPLOYEE_ID, P_TYPE_MANUAL, P_FROM_DATE, P_TO_DATE)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetCAL_DAY_LEAVE_OLD(ByVal P_EMPLOYEE_ID As Integer,
                               ByVal P_FROM_DATE As Date,
                               ByVal P_TO_DATE As Date) As DataTable Implements ServiceContracts.IAttendanceBusiness.GetCAL_DAY_LEAVE_OLD
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetCAL_DAY_LEAVE_OLD(P_EMPLOYEE_ID, P_FROM_DATE, P_TO_DATE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPhepNam(ByVal _id As Decimal?, ByVal _year As Decimal?) As AT_ENTITLEMENTDTO Implements ServiceContracts.IAttendanceBusiness.GetPhepNam
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetPhepNam(_id, _year)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPHEPBUCONLAI(ByVal lstEmpID As List(Of AT_LEAVESHEETDTO), ByVal _year As Decimal?) As List(Of AT_LEAVESHEETDTO) Implements ServiceContracts.IAttendanceBusiness.GetPHEPBUCONLAI
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetPHEPBUCONLAI(lstEmpID, _year)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetNghiBu(ByVal _id As Decimal?, ByVal _year As Decimal?) As AT_COMPENSATORYDTO Implements ServiceContracts.IAttendanceBusiness.GetNghiBu
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetNghiBu(_id, _year)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateLeaveSheet(ByVal objtime As AT_LEAVESHEETDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateLeaveSheet
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateLeaveSheet(objtime)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertLeaveSheet(ByVal objRegisterOT As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertLeaveSheet
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertLeaveSheet(objRegisterOT, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertLeaveSheetList(ByVal objRegisterOTList As List(Of AT_LEAVESHEETDTO), ByVal objRegisterOT As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertLeaveSheetList
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertLeaveSheetList(objRegisterOTList, objRegisterOT, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyLeaveSheet(ByVal objRegisterOT As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyLeaveSheet
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyLeaveSheet(objRegisterOT, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteLeaveSheet(ByVal lstID As List(Of AT_LEAVESHEETDTO)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteLeaveSheet
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteLeaveSheet(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function checkLeaveImport(ByVal dtData As DataTable) As DataTable Implements ServiceContracts.IAttendanceBusiness.checkLeaveImport
            Using rep As New AttendanceRepository
                Try

                    Return rep.checkLeaveImport(dtData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Khai bao cong com"
        Function GetDelareRice(ByVal _filter As AT_TIME_RICEDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_RICEDTO) Implements IAttendanceBusiness.GetDelareRice

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetDelareRice(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetDelareRiceById(ByVal _id As Decimal?) As AT_TIME_RICEDTO Implements ServiceContracts.IAttendanceBusiness.GetDelareRiceById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetDelareRiceById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ActiveDelareRice(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveDelareRice
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveDelareRice(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ValidateDelareRice(ByVal objDelareRice As AT_TIME_RICEDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateDelareRice
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateDelareRice(objDelareRice)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertDelareRice(ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertDelareRice
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertDelareRice(objDelareRice, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertDelareRiceList(ByVal objDelareRiceList As List(Of AT_TIME_RICEDTO), ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertDelareRiceList
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertDelareRiceList(objDelareRiceList, objDelareRice, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyDelareRice(ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyDelareRice
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyDelareRice(objDelareRice, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function DeleteDelareRice(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteDelareRice
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteDelareRice(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "quan ly cham cong bu tru"
        Function GetOffSettingTimeKeeping(ByVal _filter As AT_OFFFSETTINGDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_OFFFSETTINGDTO) Implements IAttendanceBusiness.GetOffSettingTimeKeeping

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetOffSettingTimeKeeping(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetOffSettingTimeKeepingById(ByVal _id As Decimal?) As AT_OFFFSETTINGDTO Implements ServiceContracts.IAttendanceBusiness.GetOffSettingTimeKeepingById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetOffSettingTimeKeepingById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmployeeTimeKeepingID(ByVal ComId As Decimal) As List(Of AT_OFFFSETTING_EMPDTO) _
           Implements ServiceContracts.IAttendanceBusiness.GetEmployeeTimeKeepingID
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetEmployeeTimeKeepingID(ComId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "Khai bao điều chỉnh thâm niên phép"
        Function GetDelareEntitlementNB(ByVal _filter As AT_DECLARE_ENTITLEMENTDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_DECLARE_ENTITLEMENTDTO) Implements IAttendanceBusiness.GetDelareEntitlementNB

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetDelareEntitlementNB(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetDelareEntitlementNBById(ByVal _id As Decimal?) As AT_DECLARE_ENTITLEMENTDTO Implements ServiceContracts.IAttendanceBusiness.GetDelareEntitlementNBById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetDelareEntitlementNBById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveDelareEntitlementNB(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveDelareEntitlementNB
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveDelareEntitlementNB(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertDelareEntitlementNB(ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertDelareEntitlementNB
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertDelareEntitlementNB(objDelareEntitlementNB, log, gID, checkMonthNB, checkMonthNP)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertMultipleDelareEntitlementNB(ByVal objDelareEntitlementlist As List(Of AT_DECLARE_ENTITLEMENTDTO), ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertMultipleDelareEntitlementNB
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertMultipleDelareEntitlementNB(objDelareEntitlementlist, objDelareEntitlementNB, log, gID, checkMonthNB, checkMonthNP)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ImportDelareEntitlementNB(ByVal dtData As DataTable, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean Implements ServiceContracts.IAttendanceBusiness.ImportDelareEntitlementNB
            Using rep As New AttendanceRepository
                Try

                    Return rep.ImportDelareEntitlementNB(dtData, log, gID, checkMonthNB, checkMonthNP)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyDelareEntitlementNB(ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyDelareEntitlementNB
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyDelareEntitlementNB(objDelareEntitlementNB, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteDelareEntitlementNB(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteDelareEntitlementNB
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteDelareEntitlementNB(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteOffTimeKeeping(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteOffTimeKeeping
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteOffTimeKeeping(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateMonthThamNien(ByVal objHOLIDAYGR As AT_DECLARE_ENTITLEMENTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateMonthThamNien
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateMonthThamNien(objHOLIDAYGR)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateMonthPhepNam(ByVal objHOLIDAYGR As AT_DECLARE_ENTITLEMENTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateMonthPhepNam
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateMonthPhepNam(objHOLIDAYGR)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateMonthNghiBu(ByVal objHOLIDAYGR As AT_DECLARE_ENTITLEMENTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateMonthNghiBu
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateMonthNghiBu(objHOLIDAYGR)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Bang cong com"
        Function Cal_TimeTImesheet_Rice(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal p_period_id As Decimal?, ByVal P_ORG_ID As Decimal, ByVal lstEmployee As List(Of Decimal?)) As Boolean Implements IAttendanceBusiness.Cal_TimeTImesheet_Rice

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.Cal_TimeTImesheet_Rice(_param, log, p_period_id, P_ORG_ID, lstEmployee)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetSummaryRice(ByVal param As AT_TIME_TIMESHEET_RICEDTO, ByVal log As Framework.Data.UserLog) As System.Data.DataSet Implements IAttendanceBusiness.GetSummaryRice

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetSummaryRice(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function ModifyLeaveSheetRice(ByVal objLeave As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IAttendanceBusiness.ModifyLeaveSheetRice

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.ModifyLeaveSheetRice(objLeave, log, gID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function ApprovedTimeSheetRice(ByVal objLeave As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IAttendanceBusiness.ApprovedTimeSheetRice

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.ApprovedTimeSheetRice(objLeave, log, gID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function InsertLeaveSheetRice(ByVal objLeave As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IAttendanceBusiness.InsertLeaveSheetRice

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.InsertLeaveSheetRice(objLeave, log, gID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetTimeSheetRiceById(ByVal obj As AT_TIME_TIMESHEET_RICEDTO) As AT_TIME_TIMESHEET_RICEDTO Implements ServiceContracts.IAttendanceBusiness.GetTimeSheetRiceById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetTimeSheetRiceById(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "IPORTAL - View phiếu lương"

        Public Function CheckPeriod(ByVal PeriodId As Integer, ByVal EmployeeId As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.CheckPeriod
            Try
                Dim rep As New AttendanceRepository
                Return rep.CheckPeriod(PeriodId, EmployeeId)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function GetTimeSheetPortal(ByVal _filter As AT_TIME_TIMESHEET_MONTHLYDTO,
                                   ByVal _param As ParamDTO,
                                   Optional ByRef Total As Integer = 0,
                                   Optional ByVal PageIndex As Integer = 0,
                                   Optional ByVal PageSize As Integer = Integer.MaxValue,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MONTHLYDTO) Implements IAttendanceBusiness.GetTimeSheetPortal

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetTimeSheetPortal(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "LOG"
        Function GetActionLog(ByVal _filter As AT_ACTION_LOGDTO,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByVal Sorts As String = "ACTION_DATE desc") As List(Of AT_ACTION_LOGDTO) Implements IAttendanceBusiness.GetActionLog

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetActionLog(_filter, Total, PageIndex, PageSize, Sorts)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function DeleteActionLogsPA(ByVal lstDeleteIds As List(Of Decimal)) As Integer Implements IAttendanceBusiness.DeleteActionLogsAT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.DeleteActionLogsAT(lstDeleteIds)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Portal quan ly nghi phep, nghi bu"
        Function GetTotalDayOff(ByVal _filter As TotalDayOffDTO, Optional ByVal log As UserLog = Nothing) As TotalDayOffDTO Implements IAttendanceBusiness.GetTotalDayOff

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetTotalDayOff(_filter, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GET_TIME_MANUAL(ByVal _filter As TotalDayOffDTO, Optional ByVal log As UserLog = Nothing) As TotalDayOffDTO Implements IAttendanceBusiness.GET_TIME_MANUAL

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_TIME_MANUAL(_filter, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GET_AT_PORTAL_REG(ByVal P_ID As Decimal, ByVal P_EMPLOYEE As Decimal, ByVal P_DATE_TIME As Date) As DataTable Implements IAttendanceBusiness.GET_AT_PORTAL_REG

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_AT_PORTAL_REG(P_ID, P_EMPLOYEE, P_DATE_TIME)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GET_AT_PORTAL_REG_OT(ByVal P_ID As Decimal, ByVal P_EMPLOYEE As Decimal, ByVal P_DATE_TIME As Date) As DataTable Implements IAttendanceBusiness.GET_AT_PORTAL_REG_OT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_AT_PORTAL_REG_OT(P_ID, P_EMPLOYEE, P_DATE_TIME)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetHistoryLeave(ByVal _filter As HistoryLeaveDTO,
                                    Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "REGDATE DESC", Optional ByVal log As UserLog = Nothing) As List(Of HistoryLeaveDTO) Implements IAttendanceBusiness.GetHistoryLeave

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetHistoryLeave(_filter, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function




#End Region

#Region "Check nhân viên có thuộc dự án khi chấm công"
        Public Function CheckExistEm_Pro(ByVal lstID As Decimal, ByVal FromDate As Date, ByVal ToDate As Date, ByVal IDPro As Decimal) As Boolean
            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CheckExistEm_Pro(lstID, FromDate, ToDate, IDPro)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "cham cong"
        Public Function CheckTimeSheetApproveVerify(ByVal obj As List(Of AT_PROCESS_DTO), ByVal type As String, ByRef itemError As AT_PROCESS_DTO) As Boolean _
           Implements IAttendanceBusiness.CheckTimeSheetApproveVerify
            Using rep As New AttendanceRepository
                Try
                    Return rep.CheckTimeSheetApproveVerify(obj, type, itemError)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "quan ly cham cong bu tru"
        Public Function InsertOffSettingTime(ByVal objOffSetting As AT_OFFFSETTINGDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
          Implements IAttendanceBusiness.InsertOffSettingTime
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertOffSettingTime(objOffSetting, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyOffSettingTime(ByVal objOffSetting As AT_OFFFSETTINGDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
           Implements IAttendanceBusiness.ModifyOffSettingTime
            Using rep As New AttendanceRepository
                Try
                    Return rep.ModifyOffSettingTime(objOffSetting, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "OT"
        Public Function CheckRegDateBetweenJoinAndTerDate(ByVal empId As Decimal, ByVal regDate As Date) As Boolean _
            Implements IAttendanceBusiness.CheckRegDateBetweenJoinAndTerDate
            Using rep As New AttendanceRepository
                Try
                    Return rep.CheckRegDateBetweenJoinAndTerDate(empId, regDate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetOtRegistration(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                         Optional ByRef Total As Integer = 0,
                                         Optional ByVal PageIndex As Integer = 0,
                                         Optional ByVal PageSize As Integer = Integer.MaxValue,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of AT_OT_REGISTRATIONDTO) _
                    Implements IAttendanceBusiness.GetOtRegistration
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetOtRegistration(_filter, Total, PageIndex, PageSize, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertOtRegistration(ByVal obj As AT_OT_REGISTRATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
                    Implements IAttendanceBusiness.InsertOtRegistration
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertOtRegistration(obj, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyotRegistration(ByVal obj As AT_OT_REGISTRATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
                    Implements IAttendanceBusiness.ModifyotRegistration
            Using rep As New AttendanceRepository
                Try
                    Return rep.ModifyotRegistration(obj, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function SendApproveOtRegistration(ByVal obj As List(Of AT_OT_REGISTRATIONDTO), ByVal log As UserLog) As Boolean _
                    Implements IAttendanceBusiness.SendApproveOtRegistration
            Using rep As New AttendanceRepository
                Try
                    Return rep.SendApproveOTRegistration(obj, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ApproveOtRegistration(ByVal obj As List(Of AT_OT_REGISTRATIONDTO), ByVal empId As Decimal, ByVal log As UserLog) As Boolean _
                    Implements IAttendanceBusiness.ApproveOtRegistration
            Using rep As New AttendanceRepository
                Try
                    Return rep.ApproveOtRegistration(obj, empId, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ValidateOtRegistration(ByVal _validate As AT_OT_REGISTRATIONDTO) _
                    Implements IAttendanceBusiness.ValidateOtRegistration
            Using rep As New AttendanceRepository
                Try
                    Return rep.ValidateOtRegistration(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function HRReviewOtRegistration(ByVal lst As List(Of Decimal), ByVal log As UserLog) As Boolean _
                    Implements IAttendanceBusiness.HRReviewOtRegistration
            Using rep As New AttendanceRepository
                Try
                    Return rep.HRReviewOtRegistration(lst, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteOtRegistration(ByVal lstId As List(Of Decimal)) As Boolean _
                    Implements IAttendanceBusiness.DeleteOtRegistration
            Using rep As New AttendanceRepository
                Try
                    Return rep.DeleteOtRegistration(lstId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "SHIFT CYCLE"
        Public Function GetShiftCycle(ByVal _filter As AT_SHIFTCYCLEDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of AT_SHIFTCYCLEDTO) _
                    Implements IAttendanceBusiness.GetShiftCycle
            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetShiftCycle(_filter, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetEmployeeShifts(ByVal employee_Id As Decimal, ByVal fromDate As Date, ByVal toDate As Date) As List(Of EMPLOYEE_SHIFT_DTO) Implements IAttendanceBusiness.GetEmployeeShifts

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetEmployeeShifts(employee_Id, fromDate, toDate)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

        Public Function INPORT_AT_OT_REGISTRATION(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.INPORT_AT_OT_REGISTRATION
            Using rep As New AttendanceRepository
                Try

                    Return rep.INPORT_AT_OT_REGISTRATION(P_DOCXML, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_OT_REGISTRATION_EXIT(ByVal P_EMP_CODE As String, ByVal P_DATE As String, ByVal P_HESO As String) As Integer Implements ServiceContracts.IAttendanceBusiness.CHECK_OT_REGISTRATION_EXIT
            Using rep As New AttendanceRepository
                Try

                    Return rep.CHECK_OT_REGISTRATION_EXIT(P_EMP_CODE, P_DATE, P_HESO)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_LEAVE_EXITS(ByVal P_EMP_CODE As String, ByVal P_DATE As String, ByVal P_MANUAL_ID As Decimal, ByVal P_CA As Decimal) As Integer Implements ServiceContracts.IAttendanceBusiness.CHECK_LEAVE_EXITS
            Using rep As New AttendanceRepository
                Try

                    Return rep.CHECK_LEAVE_EXITS(P_EMP_CODE, P_DATE, P_MANUAL_ID, P_CA)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_LEAVE_SHEET(ByVal P_EMP_CODE As String, ByVal P_DATE As String, ByVal P_CA As Decimal) As Decimal Implements ServiceContracts.IAttendanceBusiness.CHECK_LEAVE_SHEET
            Using rep As New AttendanceRepository
                Try

                    Return rep.CHECK_LEAVE_SHEET(P_EMP_CODE, P_DATE, P_CA)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_EMPLOYEE(ByVal P_EMP_CODE As String) As Integer Implements ServiceContracts.IAttendanceBusiness.CHECK_EMPLOYEE
            Using rep As New AttendanceRepository
                Try
                    Return rep.CHECK_EMPLOYEE(P_EMP_CODE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function INPORT_NB(ByVal P_DOCXML As String, ByVal log As UserLog, ByVal P_PERIOD_ID As Integer) As Boolean Implements ServiceContracts.IAttendanceBusiness.INPORT_NB
            Using rep As New AttendanceRepository
                Try

                    Return rep.INPORT_NB(P_DOCXML, log, P_PERIOD_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function INPORT_NB_PREV(ByVal P_DOCXML As String, ByVal log As UserLog, ByVal P_YEAR As Integer) As Boolean Implements ServiceContracts.IAttendanceBusiness.INPORT_NB_PREV
            Using rep As New AttendanceRepository
                Try

                    Return rep.INPORT_NB_PREV(P_DOCXML, log, P_YEAR)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#Region "PORTAL REGISTRATION SHIFT"
        Public Function GetAtRegShift(ByVal _filter As AtPortalRegistrationShiftDTO,
                                     ByRef Total As Integer,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of AtPortalRegistrationShiftDTO) Implements ServiceContracts.IAttendanceBusiness.GetAtRegShift
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAtRegShift(_filter, Total, PageIndex, PageSize, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function AddAtShift(ByVal objAtShift As AtPortalRegistrationShiftDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.AddAtShift
            Using rep As New AttendanceRepository
                Try

                    Return rep.AddAtShift(objAtShift)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAtShift(ByVal _lst_id As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAtShift
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAtShift(_lst_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetRegShiftByID(ByVal _id As Decimal) As AtPortalRegistrationShiftDTO Implements ServiceContracts.IAttendanceBusiness.GetRegShiftByID
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetRegShiftByID(_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
    End Class
End Namespace
