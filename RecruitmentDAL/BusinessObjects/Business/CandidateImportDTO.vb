Public Class CandidateImportDTO
    Public Property can As CandidateDTO
    Public Property can_cv As CandidateCVDTO
    Public Property can_edu As CandidateEduDTO
    Public Property can_exp As List(Of CandidateBeforeWTDTO)
    Public Property can_family As List(Of CandidateFamilyDTO)
    Public Property can_health As CandidateHealthDTO
    Public Property can_expect As CandidateExpectDTO
    Public Property can_training As List(Of CandidateTrainingDTO)
    Public Property can_other As CandidateOtherInfoDTO
End Class
