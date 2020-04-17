Imports ProfileBusiness.ServiceContracts
Imports ProfileDAL
Imports Framework.Data
Imports System.ServiceModel.Activation
Imports System.Reflection
Imports System.Configuration

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness

#Region "WelfareMng"
        Public Function GetlistWelfareEMP(ByVal Id As Integer) As List(Of Welfatemng_empDTO) Implements ServiceContracts.IProfileBusiness.GetlistWelfareEMP
            Try
                Using rep As New ProfileRepository
                    Dim dt = rep.GetlistWelfareEMP(Id)
                    Return dt
                End Using
            Catch ex As Exception

            End Try
        End Function
        Function GetWelfareListAuto(ByVal _filter As WelfareMngDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       ByVal log As UserLog) As DataTable Implements ServiceContracts.IProfileBusiness.GetWelfareListAuto
            Try
                Using rep As New ProfileRepository
                    Dim dt = rep.GetWelfareListAuto(_filter, PageIndex, PageSize, Total, log)
                    Return dt
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GET_DETAILS_EMP(ByVal P_ID As Decimal, ByVal P_WELFARE_ID As Decimal, ByVal P_DATE As Date,
                                       ByVal UserLog As UserLog) As DataTable Implements ServiceContracts.IProfileBusiness.GET_DETAILS_EMP
            Try
                Using rep As New ProfileRepository
                    Dim dt = rep.GET_DETAILS_EMP(P_ID, P_WELFARE_ID, P_DATE, UserLog)
                    Return dt
                End Using
            Catch ex As Exception

            End Try
        End Function
        Public Function GET_EXPORT_EMP(ByVal P_WELFARE_ID As Decimal, ByVal P_DATE As Date,
                                        ByVal UserLog As UserLog) As DataSet Implements ServiceContracts.IProfileBusiness.GET_EXPORT_EMP
            Try
                Using rep As New ProfileRepository
                    Dim dt = rep.GET_EXPORT_EMP(P_WELFARE_ID, P_DATE, UserLog)
                    Return dt
                End Using
            Catch ex As Exception
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
                Throw ex
            End Try
        End Function
        Public Function GET_INFO_EMPLOYEE(ByVal P_EMP_CODE As String) As DataTable Implements ServiceContracts.IProfileBusiness.GET_INFO_EMPLOYEE
            Try
                Using rep As New ProfileRepository
                    Dim dt = rep.GET_INFO_EMPLOYEE(P_EMP_CODE)
                    Return dt
                End Using
            Catch ex As Exception

            End Try
        End Function
        Public Function GetWelfareMng(ByVal _filter As WelfareMngDTO, ByVal IsDissolve As Integer, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal UserLog As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WelfareMngDTO) Implements ServiceContracts.IProfileBusiness.GetWelfareMng
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetWelfareMng(_filter, IsDissolve, PageIndex, PageSize, Total, UserLog, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetWelfareMngById(ByVal Id As Integer
                                        ) As WelfareMngDTO Implements ServiceContracts.IProfileBusiness.GetWelfareMngById
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetWelfareMngById(Id)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetComboboxPeriod(ByVal year As Decimal) As List(Of ATPeriodDTO) Implements ServiceContracts.IProfileBusiness.GetComboboxPeriod
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetComboboxPeriod(year)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetYearPeriod() As List(Of ATPeriodDTO) Implements ServiceContracts.IProfileBusiness.GetYearPeriod
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetYearPeriod()
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckWelfareMngEffect(ByVal _filter As List(Of WelfareMngDTO)) As Boolean Implements ServiceContracts.IProfileBusiness.CheckWelfareMngEffect
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.CheckWelfareMngEffect(_filter)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertWelfareMng(ByVal lstWelfareMng As WelfareMngDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.InsertWelfareMng
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertWelfareMng(lstWelfareMng, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyWelfareMng(ByVal lstWelfareMng As WelfareMngDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyWelfareMng
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyWelfareMng(lstWelfareMng, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveWelfareMng(ByVal objWelfareMng() As WelfareMngDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteWelfareMng
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteWelfareMng(objWelfareMng, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region
#Region "quan ly tai nan lao dong"
        Public Function InsertSafeLaborMng(ByVal lstSafeLaborMng As SAFELABOR_MNGDTO,
                               ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.InsertSafeLaborMng
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertSafeLaborMng(lstSafeLaborMng, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifySafeLaborMng(ByVal lstSafeLaborMng As SAFELABOR_MNGDTO,
                                 ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ModifySafeLaborMng
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifySafeLaborMng(lstSafeLaborMng, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetSafeLaborMng(ByVal _filter As SAFELABOR_MNGDTO, ByVal IsDissolve As Integer, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       ByVal UserLog As UserLog,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SAFELABOR_MNGDTO) Implements ServiceContracts.IProfileBusiness.GetSafeLaborMng
            Using rep As New ProfileRepository
                Try
                    Return rep.GetSafeLaborMng(_filter, IsDissolve, PageIndex, PageSize, Total, UserLog, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetSafeLaborMngById(ByVal Id As Integer
                                        ) As SAFELABOR_MNGDTO Implements ServiceContracts.IProfileBusiness.GetSafeLaborMngById
            Using rep As New ProfileRepository
                Try
                    Return rep.GetSafeLaborMngById(Id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckCodeSafe(ByVal code As String, ByVal id As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.CheckCodeSafe
            Using rep As New ProfileRepository
                Try
                    Return rep.CheckCodeSafe(code, id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteSafeLaborMng(ByVal lstWelfareMng() As SAFELABOR_MNGDTO,
                                   ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteSafeLaborMng
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteSafeLaborMng(lstWelfareMng, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
    End Class
End Namespace
