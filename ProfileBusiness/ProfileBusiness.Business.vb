Imports ProfileBusiness.ServiceContracts
Imports ProfileDAL
Imports Framework.Data
Imports System.ServiceModel.Activation
Imports System.Reflection
Imports System.Configuration

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness
       
#Region "EmployeeCriteriaRecord"
        Public Function EmployeeCriteriaRecord(ByVal _filter As EmployeeCriteriaRecordDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of EmployeeCriteriaRecordDTO) _
              Implements ServiceContracts.IProfileBusiness.EmployeeCriteriaRecord
            Try
                Dim rep As New ProfileRepository
                Return rep.EmployeeCriteriaRecord(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw
            End Try
        End Function

#End Region
    End Class
End Namespace