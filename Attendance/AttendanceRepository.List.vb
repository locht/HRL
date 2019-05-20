Imports Attendance.AttendanceBusiness
Imports Framework.UI

Partial Class AttendanceRepository
    Inherits AttendanceRepositoryBase
    Public Function GetLeaveRegistrationListByLM(ByVal _filter As AT_PORTAL_REG_DTO,
                               Optional ByRef Total As Integer = 0,
                               Optional ByVal PageIndex As Integer = 0,
                               Optional ByVal PageSize As Integer = Integer.MaxValue,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PORTAL_REG_DTO)
        Using rep As New AttendanceBusinessClient
            Try

                Return rep.GetLeaveRegistrationListByLM(_filter, Total, PageIndex, PageSize, Sorts, Me.Log)
            Catch ex As Exception

                Throw ex
            End Try
        End Using
    End Function

    Public Function getSetUpAttEmp(ByVal _filter As SetUpCodeAttDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                       Optional ByVal PageSize As Integer = Integer.MaxValue,
                                       Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SetUpCodeAttDTO)
        Dim lstSetUpAttEmp As List(Of SetUpCodeAttDTO)
        Using rep As New AttendanceBusinessClient
            Try
                lstSetUpAttEmp = rep.getSetUpAttEmp(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstSetUpAttEmp
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return lstSetUpAttEmp
    End Function

    Public Function InsertSetUpAttEmp(ByVal objValue As SetUpCodeAttDTO,
                                     ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertSetUpAttEmp(objValue, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ModifySetUpAttEmp(ByVal objValue As SetUpCodeAttDTO,ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifySetUpAttEmp(objValue, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function DeleteSetUpAttEmp(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteSetUpAttEmp(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function CheckValidateMACC(ByVal obj As SetUpCodeAttDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CheckValidateMACC(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function CheckValidateAPPROVE_DATE(ByVal obj As SetUpCodeAttDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CheckValidateAPPROVE_DATE(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#Region "Holiday"

    Public Function GetHoliday(ByVal _filter As AT_HOLIDAYDTO,
                                      Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAYDTO)
        Dim lstHoliday As List(Of AT_HOLIDAYDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstHoliday = rep.GetHoliday(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstHoliday
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetHoliday_Hose(ByVal _filter As AT_HOLIDAYDTO,
                                      Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAYDTO)
        Dim lstHoliday As List(Of AT_HOLIDAYDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstHoliday = rep.GetHoliday_Hose(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstHoliday
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertHoliday(ByVal objHoliday As AT_HOLIDAYDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertHOLIDAY(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function InsertHoliday_Hose(ByVal objHoliday As AT_HOLIDAYDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertHoliday_Hose(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateHoliday(ByVal objHoliday As AT_HOLIDAYDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateHOLIDAY(objHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ValidateHOLIDAY_Hose(ByVal objHoliday As AT_HOLIDAYDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateHOLIDAY_Hose(objHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyHoliday(ByVal objHoliday As AT_HOLIDAYDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyHOLIDAY(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveHoliday(ByVal lstHoliday As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveHoliday(lstHoliday, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteHoliday(ByVal lstHoliday As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteHOLIDAY(lstHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteHoliday_Hose(ByVal lstHoliday As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteHOLIDAY_Hose(lstHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Holiday Gerenal"

    Public Function GetHolidayGerenal(ByVal _filter As AT_HOLIDAY_GENERALDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAY_GENERALDTO)
        Dim lstHoliday As List(Of AT_HOLIDAY_GENERALDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstHoliday = rep.GetHolidayGerenal(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstHoliday
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertHolidayGerenal(ByVal objHoliday As AT_HOLIDAY_GENERALDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertHolidayGerenal(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateHolidayGerenal(ByVal objHoliday As AT_HOLIDAY_GENERALDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateHolidayGerenal(objHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyHolidayGerenal(ByVal objHoliday As AT_HOLIDAY_GENERALDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyHolidayGerenal(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveHolidayGerenal(ByVal lstHoliday As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveHolidayGerenal(lstHoliday, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteHolidayGerenal(ByVal lstHoliday As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteHolidayGerenal(lstHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "danh mục kiểu công"
    Public Function GetSignByPage(ByVal pagecode As String) As List(Of AT_TIME_MANUALDTO)
        Dim lstManual As List(Of AT_TIME_MANUALDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstManual = rep.GetSignByPage(pagecode)
                Return lstManual
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetAT_FML(ByVal _filter As AT_FMLDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_FMLDTO)
        Dim lstHoliday As List(Of AT_FMLDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstHoliday = rep.GetAT_FML(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstHoliday
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertAT_FML(ByVal objHoliday As AT_FMLDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_FML(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_FML(ByVal objHoliday As AT_FMLDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_FML(objHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_FML(ByVal objHoliday As AT_FMLDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_FML(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_FML(ByVal lstHoliday As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_FML(lstHoliday, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_FML(ByVal lstHoliday As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_FML(lstHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "danh mục ca làm việc"
    Public Function GetAT_GSIGN(ByVal _filter As AT_GSIGNDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_GSIGNDTO)
        Dim lstAt_GSIGN As List(Of AT_GSIGNDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstAt_GSIGN = rep.GetAT_GSIGN(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstAt_GSIGN
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertAT_GSIGN(ByVal objHoliday As AT_GSIGNDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_GSIGN(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_GSIGN(ByVal objHoliday As AT_GSIGNDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_GSIGN(objHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_GSIGN(ByVal objHoliday As AT_GSIGNDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_GSIGN(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_GSIGN(ByVal lstAT_GSIGN As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_GSIGN(lstAT_GSIGN, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_GSIGN(ByVal lstAT_GSIGN As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_GSIGN(lstAT_GSIGN)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "quyết định phạt đi muộn về sớm"
    Public Function GetAT_DMVS(ByVal _filter As AT_DMVSDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_DMVSDTO)
        Dim lstDMVS As List(Of AT_DMVSDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstDMVS = rep.GetAT_DMVS(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstDMVS
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertAT_DMVS(ByVal objDMVS As AT_DMVSDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_DMVS(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_DMVS(ByVal objDMVS As AT_DMVSDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_DMVS(objDMVS)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_DMVS(ByVal objDMVS As AT_DMVSDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_DMVS(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_DMVS(ByVal lstDMVS As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_DMVS(lstDMVS, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_DMVS(ByVal lstDMVS As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_DMVS(lstDMVS)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Danh mục ca làm việc"
    Public Function GetAT_SHIFT(ByVal _filter As AT_SHIFTDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                Optional ByVal PageSize As Integer = Integer.MaxValue,
                                Optional ByRef Total As Integer = 0,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SHIFTDTO)
        Dim lstDMVS As List(Of AT_SHIFTDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstDMVS = rep.GetAT_SHIFT(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstDMVS
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertAT_SHIFT(ByVal objDMVS As AT_SHIFTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_SHIFT(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_SHIFT(ByVal objDMVS As AT_SHIFTDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_SHIFT(objDMVS)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_SHIFT(ByVal objDMVS As AT_SHIFTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_SHIFT(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_SHIFT(ByVal lstDMVS As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_SHIFT(lstDMVS, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_SHIFT(ByVal lstDMVS As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_SHIFT(lstDMVS)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetAT_TIME_MANUALBINCOMBO() As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetAT_TIME_MANUALBINCOMBO()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateCombobox(ByVal cbxData As ComboBoxDataDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateCombobox(cbxData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Thiết lập số ngày nghỉ theo đối tượng"
    Public Function GetAT_Holiday_Object(ByVal _filter As AT_HOLIDAY_OBJECTDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAY_OBJECTDTO)
        Dim lstHolidayObj As List(Of AT_HOLIDAY_OBJECTDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstHolidayObj = rep.GetAT_Holiday_Object(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstHolidayObj
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertAT_Holiday_Object(ByVal objDMVS As AT_HOLIDAY_OBJECTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_Holiday_Object(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_Holiday_Object(ByVal objDMVS As AT_HOLIDAY_OBJECTDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_Holiday_Object(objDMVS)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_Holiday_Object(ByVal objDMVS As AT_HOLIDAY_OBJECTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_Holiday_Object(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_Holiday_Object(ByVal lstHolidayObj As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_Holiday_Object(lstHolidayObj, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_Holiday_Object(ByVal lstHolidayObj As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_Holiday_Object(lstHolidayObj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Thiết lập đối tượng chấm công theo cấp nhân sự"
    Public Function GetAT_SETUP_SPECIAL(ByVal _filter As AT_SETUP_SPECIALDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_SPECIALDTO)
        Dim lstSetUp_SP As List(Of AT_SETUP_SPECIALDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstSetUp_SP = rep.GetAT_SETUP_SPECIAL(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstSetUp_SP
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertAT_SETUP_SPECIAL(ByVal objDMVS As AT_SETUP_SPECIALDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_SETUP_SPECIAL(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_SETUP_SPECIAL(ByVal objDMVS As AT_SETUP_SPECIALDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_SETUP_SPECIAL(objDMVS)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_SETUP_SPECIAL(ByVal objDMVS As AT_SETUP_SPECIALDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_SETUP_SPECIAL(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_SETUP_SPECIAL(ByVal lstSetUp As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_SETUP_SPECIAL(lstSetUp, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteAT_SETUP_SPECIAL(ByVal lstSetUp As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_SETUP_SPECIAL(lstSetUp)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Thiết lập đối tượng chấm công theo nhân viên"
    Public Function GetAT_SETUP_TIME_EMP(ByVal _filter As AT_SETUP_TIME_EMPDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_TIME_EMPDTO)
        Dim lstSetUp_SP As List(Of AT_SETUP_TIME_EMPDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstSetUp_SP = rep.GetAT_SETUP_TIME_EMP(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstSetUp_SP
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertAT_SETUP_TIME_EMP(ByVal objDMVS As AT_SETUP_TIME_EMPDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_SETUP_TIME_EMP(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_SETUP_TIME_EMP(ByVal objDMVS As AT_SETUP_TIME_EMPDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_SETUP_TIME_EMP(objDMVS)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_SETUP_TIME_EMP(ByVal objDMVS As AT_SETUP_TIME_EMPDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_SETUP_TIME_EMP(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_SETUP_TIME_EMP(ByVal lstSetUp As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_SETUP_TIME_EMP(lstSetUp, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteAT_SETUP_TIME_EMP(ByVal lstSetUp As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_SETUP_TIME_EMP(lstSetUp)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Thiết lập máy chấm công"
    Public Function GetAT_TERMINAL(ByVal _filter As AT_TERMINALSDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TERMINALSDTO)
        Dim lstTerminal As List(Of AT_TERMINALSDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstTerminal = rep.GetAT_TERMINAL(_filter, PageIndex, PageSize, Total, Sorts, Log)
                Return lstTerminal
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetAT_TERMINAL_STATUS(ByVal _filter As AT_TERMINALSDTO,
                                        Optional ByVal PageIndex As Integer = 0,
                                            Optional ByVal PageSize As Integer = Integer.MaxValue,
                                            Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TERMINALSDTO)
        Dim lstTerminal As List(Of AT_TERMINALSDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstTerminal = rep.GetAT_TERMINAL_STATUS(_filter, PageIndex, PageSize, Total, Sorts, Log)
                Return lstTerminal
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertAT_TERMINAL(ByVal objTerminal As AT_TERMINALSDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_TERMINAL(objTerminal, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_TERMINAL(ByVal objTerminal As AT_TERMINALSDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_TERMINAL(objTerminal)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_TERMINAL(ByVal objTerminal As AT_TERMINALSDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_TERMINAL(objTerminal, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_TERMINAL(ByVal lstTerminal As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_TERMINAL(lstTerminal, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_TERMINAL(ByVal lstTerminal As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_TERMINAL(lstTerminal)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Thiết lập chấm công đặc biệt"
    Public Function GetAT_SIGNDEFAULT(ByVal _filter As AT_SIGNDEFAULTDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SIGNDEFAULTDTO)
        Dim lstSign As List(Of AT_SIGNDEFAULTDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstSign = rep.GetAT_SIGNDEFAULT(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
                Return lstSign
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetAT_ListShift() As DataTable
        Dim lstSign As DataTable

        Using rep As New AttendanceBusinessClient
            Try
                lstSign = rep.GetAT_ListShift()
                Return lstSign
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetAT_PERIOD() As DataTable
        Dim lstSign As DataTable

        Using rep As New AttendanceBusinessClient
            Try
                lstSign = rep.GetAT_PERIOD()
                Return lstSign
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetEmployeeID(ByVal employee_code As String, ByVal period_id As Decimal) As DataTable
        Dim lstSign As DataTable

        Using rep As New AttendanceBusinessClient
            Try
                lstSign = rep.GetEmployeeID(employee_code, period_id)
                Return lstSign
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetEmployeeIDInSign(ByVal employee_code As String) As DataTable
        Dim lstSign As DataTable

        Using rep As New AttendanceBusinessClient
            Try
                lstSign = rep.GetEmployeeIDInSign(employee_code)
                Return lstSign
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetEmployeeByTimeID(ByVal time_id As Decimal) As DataTable
        Dim lstSign As DataTable

        Using rep As New AttendanceBusinessClient
            Try
                lstSign = rep.GetEmployeeByTimeID(time_id)
                Return lstSign
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertAT_SIGNDEFAULT(ByVal objSign As AT_SIGNDEFAULTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_SIGNDEFAULT(objSign, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_SIGNDEFAULT(ByVal objSign As AT_SIGNDEFAULTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_SIGNDEFAULT(objSign, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_SIGNDEFAULT(ByVal objSign As AT_SIGNDEFAULTDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_SIGNDEFAULT(objSign)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_SIGNDEFAULT(ByVal lstSign As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_SIGNDEFAULT(lstSign, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_SIGNDEFAULT(ByVal lstSign As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_SIGNDEFAULT(lstSign)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Dang ky lam them tren iportal"
    Public Function GET_REG_PORTAL(ByVal empid As Decimal, ByVal startdate As Date, ByVal enddate As Date,
                                                                ByVal strId As String, ByVal type As String) As List(Of APPOINTMENT_DTO)
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GET_REG_PORTAL(empid, startdate, enddate, strId, type)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function GET_TOTAL_OT_APPROVE(ByVal empid As Decimal, ByVal enddate As Date) As Decimal
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GET_TOTAL_OT_APPROVE(empid, enddate)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function AT_CHECK_ORG_PERIOD_STATUS_OT(ByVal LISTORG As String, ByVal PERIOD As Decimal) As Int32
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.AT_CHECK_ORG_PERIOD_STATUS_OT(LISTORG, PERIOD)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function GET_LIST_HOURS() As DataTable
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GET_LIST_HOURS()
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function GET_LIST_MINUTE() As DataTable
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GET_LIST_MINUTE()
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function PRI_PROCESS_APP(ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal process_type As String, ByVal totalHours As Decimal, ByVal totalDay As Decimal, ByVal sign_id As Integer, ByVal id_reggroup As Integer) As Int32
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.PRI_PROCESS_APP(employee_id, period_id, process_type, totalHours, totalDay, sign_id, id_reggroup)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function GET_SEQ_PORTAL_RGT() As Decimal
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GET_SEQ_PORTAL_RGT()
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function GET_ORGID(ByVal EMPID As Integer) As Int32
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GET_ORGID(EMPID)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function GET_PERIOD(ByVal DATE_CURRENT As Date) As Int32
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GET_PERIOD(DATE_CURRENT)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function AT_CHECK_EMPLOYEE(ByVal EMPID As Decimal, ByVal ENDDATE As Date) As Int32
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.AT_CHECK_EMPLOYEE(EMPID, ENDDATE)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function GET_TOTAL_OT_APPROVE3(ByVal EMPID As Decimal?, ByVal ENDDATE As Date) As Decimal
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GET_TOTAL_OT_APPROVE3(EMPID, ENDDATE)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function CHECK_RGT_OT(ByVal EMPID As Decimal, ByVal STARTDATE As Date, ByVal ENDDATE As Date, _
                                 ByVal FROM_HOUR As String, ByVal TO_HOUR As String, ByVal HOUR_RGT As Decimal) As Int32
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.CHECK_RGT_OT(EMPID, STARTDATE, ENDDATE, FROM_HOUR, TO_HOUR, HOUR_RGT)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
#End Region

#Region "Đăng ký nghỉ trên iportal"
    Public Function GetHolidayByCalenderToTable(ByVal startdate As Date, ByVal enddate As Date) As DataTable
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GetHolidayByCalenderToTable(startdate, enddate)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function GetRegisterAppointmentInPortalByEmployee(ByVal empid As Decimal, ByVal startdate As Date, ByVal enddate As Date,
                                                                ByVal listSign As List(Of AT_TIME_MANUALDTO), ByVal status As List(Of Short)) As List(Of AT_TIMESHEET_REGISTERDTO)

        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GetRegisterAppointmentInPortalByEmployee(empid, startdate, enddate, listSign, status)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try

    End Function
    Public Function GetRegisterAppointmentInPortalOT(ByVal empid As Decimal, ByVal startdate As Date, ByVal enddate As Date,
                                                               ByVal listSign As List(Of OT_OTHERLIST_DTO), ByVal status As List(Of Short)) As List(Of AT_TIMESHEET_REGISTERDTO)

        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GetRegisterAppointmentInPortalOT(empid, startdate, enddate, listSign, status)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try

    End Function
    Public Function GetTotalLeaveInYear(ByVal empid As Decimal, ByVal p_year As Decimal) As Decimal

        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GetTotalLeaveInYear(empid, p_year)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try

    End Function

    Public Function InsertPortalRegister(ByVal itemRegister As AttendanceBusiness.AT_PORTAL_REG_DTO) As Boolean
        Try
            _isAvailable = False

            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.InsertPortalRegister(itemRegister, Me.Log)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try

    End Function

    Public Function GetHolidayByCalender(ByVal startdate As Date, ByVal enddate As Date) As List(Of Date)
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GetHolidayByCalender(startdate, enddate)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function DeletePortalRegisterByDate(ByVal listappointment As List(Of AT_TIMESHEET_REGISTERDTO), ByVal listSign As List(Of AT_TIME_MANUALDTO)) As Boolean
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.DeletePortalRegisterByDate(listappointment, listSign)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try

    End Function
    Public Function DeletePortalRegisterByDateOT(ByVal listappointment As List(Of AT_TIMESHEET_REGISTERDTO), ByVal listSign As List(Of OT_OTHERLIST_DTO)) As Boolean
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.DeletePortalRegisterByDateOT(listappointment, listSign)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try

    End Function

    Public Function DeletePortalRegister(ByVal id As Decimal) As Boolean
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.DeletePortalRegister(id)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try

    End Function

    Public Function SendRegisterToApprove(ByVal objLstRegisterId As List(Of Decimal), ByVal process As String, ByVal currentUrl As String) As String
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.SendRegisterToApprove(objLstRegisterId, process, currentUrl)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try

    End Function

#End Region

#Region "Phê duyệt đăng ký nghỉ trên iportal"
    Public Function GetListSignCode(ByVal gSignCode As String) As List(Of AT_TIME_MANUALDTO)
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GetListSignCode(gSignCode)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function GetListWaitingForApprove(ByVal approveId As Decimal, ByVal process As String, ByVal filter As AttendanceBusiness.ATRegSearchDTO) As List(Of AttendanceBusiness.AT_PORTAL_REG_DTO)
        Try
            _isAvailable = False

            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GetListWaitingForApprove(approveId, process, filter)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function GetListWaitingForApproveOT(ByVal approveId As Decimal, ByVal process As String, ByVal filter As AttendanceBusiness.ATRegSearchDTO) As List(Of AttendanceBusiness.AT_PORTAL_REG_DTO)
        Try
            _isAvailable = False

            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GetListWaitingForApproveOT(approveId, process, filter)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function ApprovePortalRegister(ByVal regID As Decimal, ByVal approveId As Decimal,
                                          ByVal status As Integer, ByVal note As String,
                                          ByVal currentUrl As String, ByVal process As String,
                                          Optional ByVal isLog As Boolean = True) As Boolean
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    Dim _log As Common.CommonBusiness.UserLog
                    If Not isLog Then
                        _log = New Common.CommonBusiness.UserLog With {.ActionName = "U",
                                                                        .ComputerName = "Auto",
                                                                        .Email = "Auto",
                                                                        .Fullname = "Auto",
                                                                        .Ip = "Auto",
                                                                        .Username = "Auto",
                                                                        .Mobile = "Auto"}
                    Else
                        _log = Log
                    End If
                    Return rep.ApprovePortalRegister(regID, approveId, status, note, currentUrl, process, _log)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function GetEmployeeList() As DataTable
        Try
            _isAvailable = False

            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GetEmployeeList()
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function GetLeaveDay(ByVal dDate As Date) As DataTable
        Try
            _isAvailable = False

            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GetLeaveDay(dDate)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
#End Region

#Region "Thiết lập kiểu công"

    Public Function GetAT_TIME_MANUAL(ByVal _filter As AT_TIME_MANUALDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TIME_MANUALDTO)
        Dim lstHoliday As List(Of AT_TIME_MANUALDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstHoliday = rep.GetAT_TIME_MANUAL(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstHoliday
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetAT_TIME_MANUALById(ByVal _id As Decimal?) As AT_TIME_MANUALDTO
        Dim lstHoliday As AT_TIME_MANUALDTO
        Using rep As New AttendanceBusinessClient
            Try
                lstHoliday = rep.GetAT_TIME_MANUALById(_id)
                Return lstHoliday
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertAT_TIME_MANUAL(ByVal objHoliday As AT_TIME_MANUALDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_TIME_MANUAL(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_TIME_MANUAL(ByVal objHoliday As AT_TIME_MANUALDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_TIME_MANUAL(objHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_TIME_MANUAL(ByVal objHoliday As AT_TIME_MANUALDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_TIME_MANUAL(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_TIME_MANUAL(ByVal lstHoliday As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_TIME_MANUAL(lstHoliday, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_TIME_MANUAL(ByVal lstHoliday As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_TIME_MANUAL(lstHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetDataImportCO() As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetDataImportCO()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Danh mục tham số hệ thống"
    Public Function GetListParamItime(ByVal _filter As AT_LISTPARAM_SYSTEAMDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_LISTPARAM_SYSTEAMDTO)
        Dim lstHoliday As List(Of AT_LISTPARAM_SYSTEAMDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstHoliday = rep.GetListParamItime(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstHoliday
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertListParamItime(ByVal lstData As AT_LISTPARAM_SYSTEAMDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertListParamItime(lstData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateListParamItime(ByVal lstData As AT_LISTPARAM_SYSTEAMDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateListParamItime(lstData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyListParamItime(ByVal lstData As AT_LISTPARAM_SYSTEAMDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyListParamItime(lstData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveListParamItime(ByVal lstData As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveListParamItime(lstData, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteListParamItime(ByVal lstData As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteListParamItime(lstData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region ""
    Public Function AutoGenCode(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.AutoGenCode(firstChar, tableName, colName)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As AttendanceCommonTABLE_NAME) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CheckExistInDatabase(lstID, table)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function CheckExistInDatabaseAT_SIGNDEFAULT(ByVal lstID As List(Of Decimal), ByVal lstWorking As List(Of Date), ByVal lstShift As List(Of Decimal), ByVal table As AttendanceCommonTABLE_NAME) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CheckExistInDatabaseAT_SIGNDEFAULT(lstID, lstWorking, lstShift, table)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#Region "AT_PROJECT_TITLE"
    Public Function GetAT_PROJECT_TITLE(ByVal _filter As AT_PROJECT_TITLEDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PROJECT_TITLEDTO)
        Dim lstHoliday As List(Of AT_PROJECT_TITLEDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstHoliday = rep.GetAT_PROJECT_TITLE(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstHoliday
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertAT_PROJECT_TITLE(ByVal objHoliday As AT_PROJECT_TITLEDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_PROJECT_TITLE(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_PROJECT_TITLE(ByVal objHoliday As AT_PROJECT_TITLEDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_PROJECT_TITLE(objHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_PROJECT_TITLE(ByVal objHoliday As AT_PROJECT_TITLEDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_PROJECT_TITLE(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_PROJECT_TITLE(ByVal lstHoliday As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_PROJECT_TITLE(lstHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "AT_PROJECT"
    Public Function GetAT_PROJECT(ByVal _filter As AT_PROJECTDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PROJECTDTO)
        Dim lstHoliday As List(Of AT_PROJECTDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstHoliday = rep.GetAT_PROJECT(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstHoliday
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertAT_PROJECT(ByVal objHoliday As AT_PROJECTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_PROJECT(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_PROJECT(ByVal objHoliday As AT_PROJECTDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_PROJECT(objHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_PROJECT(ByVal objHoliday As AT_PROJECTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_PROJECT(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_PROJECT(ByVal lstHoliday As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_PROJECT(lstHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateOtherList(ByVal objOtherList As OT_OTHERLIST_DTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                CacheManager.ClearValue("OT_OTHER_LIST_" & IIf(True, "Blank", "NoBlank"))
                Return rep.ValidateOtherList(objOtherList)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "AT_PROJECT_WORK"
    Public Function GetAT_PROJECT_WORK(ByVal _filter As AT_PROJECT_WORKDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PROJECT_WORKDTO)
        Dim lstHoliday As List(Of AT_PROJECT_WORKDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstHoliday = rep.GetAT_PROJECT_WORK(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstHoliday
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertAT_PROJECT_WORK(ByVal objHoliday As AT_PROJECT_WORKDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_PROJECT_WORK(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_PROJECT_WORK(ByVal objHoliday As AT_PROJECT_WORKDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_PROJECT_WORK(objHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_PROJECT_WORK(ByVal objHoliday As AT_PROJECT_WORKDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_PROJECT_WORK(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_PROJECT_WORK(ByVal lstHoliday As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_PROJECT_WORK(lstHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "AT_PROJECT_EMP"
    Public Function GetAT_PROJECT_EMP(ByVal _filter As AT_PROJECT_EMPDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PROJECT_EMPDTO)
        Dim lstHoliday As List(Of AT_PROJECT_EMPDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstHoliday = rep.GetAT_PROJECT_EMP(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstHoliday
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertAT_PROJECT_EMP(ByVal objHoliday As AT_PROJECT_EMPDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_PROJECT_EMP(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_PROJECT_EMP(ByVal objHoliday As AT_PROJECT_EMPDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_PROJECT_EMP(objHoliday, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_PROJECT_EMP(ByVal lstHoliday As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_PROJECT_EMP(lstHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

    Public Function GET_PE_ASSESS_MESS(ByVal EMP As Decimal?) As DataTable
        Try
            Using rep As New AttendanceBusinessClient
                Try
                    Return rep.GET_PE_ASSESS_MESS(EMP)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#Region "cham cong"
    Public Function GetLeaveRegistrationList(ByVal _filter As AT_PORTAL_REG_DTO,
                                Optional ByRef Total As Integer = 0,
                                Optional ByVal PageIndex As Integer = 0,
                                Optional ByVal PageSize As Integer = Integer.MaxValue,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PORTAL_REG_DTO)
        Using rep As New AttendanceBusinessClient
            Try

                Return rep.GetLeaveRegistrationList(_filter, Total, PageIndex, PageSize, Sorts, Me.Log)
            Catch ex As Exception

                Throw ex
            End Try
        End Using
    End Function
    Public Function ApprovePortalRegList(ByVal obj As List(Of AT_PORTAL_REG_LIST_DTO)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try

                Return rep.ApprovePortalRegList(obj, Me.Log)
            Catch ex As Exception

                Throw ex
            End Try
        End Using
    End Function
    Public Function DeletePortalReg(ByVal lstId As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try

                Return rep.DeletePortalReg(lstId)
            Catch ex As Exception

                Throw ex
            End Try
        End Using
    End Function
#End Region
#Region "Chan cong newedit"
    Public Function GetEmployeeInfor(ByVal P_EmpId As Decimal?, ByVal P_Org_ID As Decimal?, Optional ByVal fromDate As Date? = Nothing) As DataTable
        Using rep As New AttendanceBusinessClient
            Try

                Return rep.GetEmployeeInfor(P_EmpId, P_Org_ID, fromDate)
            Catch ex As Exception

                Throw ex
            End Try
        End Using
    End Function
    Public Function GetLeaveRegistrationById(ByVal _filter As AT_PORTAL_REG_DTO) As AT_PORTAL_REG_DTO
        Using rep As New AttendanceBusinessClient
            Try

                Return rep.GetLeaveRegistrationById(_filter)
            Catch ex As Exception

                Throw ex
            End Try
        End Using
    End Function
    Public Function GetLeaveEmpDetail(ByVal employee_Id As Decimal, ByVal fromDate As Date, ByVal toDate As Date, Optional ByVal isUpdate As Boolean = False) As List(Of LEAVE_DETAIL_EMP_DTO)
        Using rep As New AttendanceBusinessClient
            Try

                Return rep.GetLeaveEmpDetail(employee_Id, fromDate, toDate, isUpdate)
            Catch ex As Exception

                Throw ex
            End Try
        End Using
    End Function
    Public Function GetLeaveRegistrationDetailById(ByVal listId As Decimal) As List(Of AT_PORTAL_REG_DTO)
        Using rep As New AttendanceBusinessClient
            Try

                Return rep.GetLeaveRegistrationDetailById(listId)
            Catch ex As Exception

                Throw ex
            End Try
        End Using
    End Function
    Public Function InsertPortalRegList(ByVal obj As AT_PORTAL_REG_LIST_DTO, ByVal lstObjDetail As List(Of AT_PORTAL_REG_DTO), ByRef gID As Decimal, ByRef itemExist As AT_PORTAL_REG_DTO, ByRef isOverAnnualLeave As Boolean) As Boolean
        Using rep As New AttendanceBusinessClient
            Try

                Return rep.InsertPortalRegList(obj, lstObjDetail, Me.Log, gID, itemExist, isOverAnnualLeave)
            Catch ex As Exception

                Throw ex
            End Try
        End Using
    End Function
    Public Function ModifyPortalRegList(ByVal obj As AT_PORTAL_REG_DTO, ByVal itemRegister As AttendanceBusiness.AT_PORTAL_REG_DTO) As Boolean
        Try
            _isAvailable = False
        Using rep As New AttendanceBusinessClient
                Try
                    Return rep.ModifyPortalRegList(obj, itemRegister, Me.Log)
                Catch ex As Exception
                    Throw ex
                End Try

            End Using
        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
   
#End Region

End Class
