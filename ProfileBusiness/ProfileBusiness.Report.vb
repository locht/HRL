Imports ProfileBusiness.ServiceContracts
Imports ProfileDAL
Imports Framework.Data
Imports System.ServiceModel.Activation
Imports System.Reflection
Imports System.Configuration

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness
        '

#Region "Reports"

#Region "Dynamic"
        Function GetConditionColumn(ByVal _ConditionID As Decimal) As List(Of RptDynamicColumnDTO) Implements IProfileBusiness.GetConditionColumn
            Using rep As New ProfileRepository
                Try

                    Return rep.GetConditionColumn(_ConditionID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function GetListReportName(ByVal _ViewId As Decimal) As List(Of HuDynamicConditionDTO) Implements IProfileBusiness.GetListReportName
            Using rep As New ProfileRepository
                Try

                    Return rep.GetListReportName(_ViewId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteDynamicReport(ByVal ID As Decimal, ByVal log As UserLog) As Boolean Implements IProfileBusiness.DeleteDynamicReport
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteDynamicReport(ID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function SaveDynamicReport(ByVal _report As HuDynamicConditionDTO, ByVal _col As List(Of HuConditionColDTO), ByVal log As UserLog) As Boolean Implements IProfileBusiness.SaveDynamicReport
            Using rep As New ProfileRepository
                Try

                    Return rep.SaveDynamicReport(_report, _col, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetDynamicReportList() As Dictionary(Of Decimal, String) Implements IProfileBusiness.GetDynamicReportList
            Using rep As New ProfileRepository
                Try

                    Return rep.GetDynamicReportList()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        ''' <summary>
        ''' Lấy danh sách các cột của DynamicReport
        ''' </summary>
        ''' <param name="_reportID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetDynamicReportColumn(ByVal _reportID As Decimal) As List(Of RptDynamicColumnDTO) Implements IProfileBusiness.GetDynamicReportColumn

            Using rep As New ProfileRepository
                Try

                    Return rep.GetDynamicReportColumn(_reportID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        ''' <summary>
        ''' Lấy dữ liệu báo cáo động
        ''' </summary>
        ''' <param name="column">Danh sách các cột cần lấy</param>
        ''' <param name="condition">Expression điều kiện</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetDynamicReport(ByVal _reportID As Decimal,
                                         ByVal orgID As Decimal,
                                         ByVal isDissolve As Decimal,
                                         ByVal chkTerminate As Decimal,
                                         ByVal chkHasTerminate As Decimal,
                                         ByVal column As List(Of String),
                                         ByVal condition As String,
                                         ByVal log As UserLog) As DataTable _
                                     Implements ServiceContracts.IProfileBusiness.GetDynamicReport
            Using rep As New ProfileRepository
                Try
                    Return rep.GetDynamicReport(_reportID, orgID, isDissolve, chkTerminate, chkHasTerminate, column, condition, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#End Region

    End Class
End Namespace