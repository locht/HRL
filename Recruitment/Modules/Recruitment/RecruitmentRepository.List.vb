Imports Recruitment.RecruitmentBusiness
Imports Framework.UI

Partial Class RecruitmentRepository

#Region "Hoadm - List"

#Region "CostCenter"

    Public Function GetCostCenter(ByVal _filter As CostCenterDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CostCenterDTO)
        Dim lstCostCenter As List(Of CostCenterDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstCostCenter = rep.GetCostCenter(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCostCenter
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCostCenter(ByVal objCostCenter As CostCenterDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.InsertCostCenter(objCostCenter, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateCostCenter(ByVal objCostCenter As CostCenterDTO) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ValidateCostCenter(objCostCenter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCostCenter(ByVal objCostCenter As CostCenterDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ModifyCostCenter(objCostCenter, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveCostCenter(ByVal lstCostCenter As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ActiveCostCenter(lstCostCenter, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCostCenter(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.DeleteCostCenter(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "ExamsDtl"

    Public Function GetExamsDtl(ByVal _filter As ExamsDtlDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EXAMS_ORDER") As List(Of ExamsDtlDTO)
        Dim lstExamsDtl As List(Of ExamsDtlDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstExamsDtl = rep.GetExamsDtl(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstExamsDtl
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateExamsDtl(ByVal objExams As ExamsDtlDTO) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.UpdateExamsDtl(objExams, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteExamsDtl(ByVal obj As ExamsDtlDTO) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.DeleteExamsDtl(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#End Region

End Class
