Imports Common
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities

Partial Class TrainingStoreProcedure

#Region "Program Group"
    Public Function ProgramGroupCreate(ByVal code As String,
                                       ByVal name As String,
                                       ByVal remark As String,
                                       ByVal userBy As String,
                                       ByVal userLog As String) As Int32
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_TRAINING.CREATE_PROGRAM_GROUP", New List(Of Object)(New Object() {code,
                                                                                                                        name,
                                                                                                                        remark,
                                                                                                                        userBy,
                                                                                                                        userLog,
                                                                                                                        OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function ProgramGroupGetList() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.READ_PROGRAM_GROUP_LIST")
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function StatusProgramGroupGetList() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.READ_PROGRAM_GROUP_LIST_STATUS")
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function ProgramGroupUpdate(ByVal id As Int32,
                                       ByVal code As String,
                                       ByVal name As String,
                                       ByVal remark As String,
                                       ByVal userBy As String,
                                       ByVal userLog As String) As Int32
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_TRAINING.UPDATE_PROGRAM_GROUP", New List(Of Object)(New Object() {id,
                                                                                                                        code,
                                                                                                                        name,
                                                                                                                        remark,
                                                                                                                        userBy,
                                                                                                                        userLog,
                                                                                                                        OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function ProgramGroupDelete(ByVal id As String) As Int32
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_TRAINING.DELETE_PROGRAM_GROUP", New List(Of Object)(New Object() {id, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
    Public Function CheckGroupDelete(ByVal id As String) As String
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_TRAINING.CHECK_PROGRAM_GROUP", New List(Of Object)(New Object() {id, OUT_NUMBER}))
        If Int32.Parse(obj(0).ToString()) > 0 Then
            Return "Chương trình đang được sử dụng, không thể xóa được"
        Else
            Return ""
        End If

    End Function
    Public Function ProgramGroupActive(ByVal id As String,
                                       ByVal active As Int32) As Int32
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_TRAINING.ACTIVE_PROGRAM_GROUP", New List(Of Object)(New Object() {id,
                                                                                                                          active,
                                                                                                                          OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function ProgramGroupHas(ByVal id As Int32, ByVal code As String) As Boolean
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_TRAINING.HAS_PROGRAM_GROUP", New List(Of Object)(New Object() {id,
                                                                                                                       code,
                                                                                                                       OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString()) = 0
    End Function
#End Region

#Region "Unit"
    Public Function UnitCreate(ByVal code As String,
                               ByVal name As String,
                               ByVal address As String,
                               ByVal phone As String,
                               ByVal fax As String,
                               ByVal web As String,
                               ByVal tax_code As String,
                               ByVal train_field As String,
                               ByVal represent As String,
                               ByVal contactPerson As String,
                               ByVal contactPhone As String,
                               ByVal contactEmail As String,
                               ByVal remark As String,
                                ByVal desc_u As String,
                               ByVal userBy As String,
                               ByVal userLog As String) As Int32
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_TRAINING.CREATE_UNIT", New List(Of Object)(New Object() {code,
                                                                                                                 name,
                                                                                                                 address,
                                                                                                                 phone,
                                                                                                                 fax,
                                                                                                                 web,
                                                                                                                 tax_code,
                                                                                                                 train_field,
                                                                                                                 represent,
                                                                                                                 contactPerson,
                                                                                                                 contactPhone,
                                                                                                                 contactEmail,
                                                                                                                 remark,
                                                                                                                  desc_u,
                                                                                                                 userBy,
                                                                                                                 userLog,
                                                                                                                 OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function GetProgramGroup() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.GET_PROGRAM_GROUP")
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    

    Public Function UnitGetList() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.READ_UNIT_LIST")
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function UnitUpdate(ByVal id As Int32,
                               ByVal code As String,
                               ByVal name As String,
                               ByVal address As String,
                               ByVal phone As String,
                               ByVal fax As String,
                               ByVal web As String,
                               ByVal tax_code As String,
                               ByVal train_field As String,
                               ByVal represent As String,
                               ByVal contactPerson As String,
                               ByVal contactPhone As String,
                               ByVal contactEmail As String,
                               ByVal remark As String,
                                ByVal desc_u As String,
                               ByVal userBy As String,
                               ByVal userLog As String) As Int32
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_TRAINING.UPDATE_UNIT", New List(Of Object)(New Object() {id,
                                                                                                                 code,
                                                                                                                 name,
                                                                                                                 address,
                                                                                                                 phone,
                                                                                                                 fax,
                                                                                                                 web,
                                                                                                                 tax_code,
                                                                                                                 train_field,
                                                                                                                 represent,
                                                                                                                 contactPerson,
                                                                                                                 contactPhone,
                                                                                                                 contactEmail,
                                                                                                                 remark,
                                                                                                                 desc_u,
                                                                                                                 userBy,
                                                                                                                 userLog,
                                                                                                                 OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
    Public Function CheckUnitDelete(ByVal id As String) As String
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_TRAINING.CHECK_DELETE_UNIT", New List(Of Object)(New Object() {id, OUT_NUMBER}))
        If Int32.Parse(obj(0).ToString()) > 0 Then
            Return "Chương trình đang được sử dụng, không thể xóa được"
        Else
            Return ""
        End If
    End Function
    Public Function UnitDelete(ByVal id As String) As Int32
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_TRAINING.DELETE_UNIT", New List(Of Object)(New Object() {id, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function UnitActive(ByVal id As String,
                               ByVal active As Int32) As Int32
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_TRAINING.ACTIVE_UNIT", New List(Of Object)(New Object() {id,
                                                                                                                 active,
                                                                                                                 OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
#End Region

#Region "Centers"
    Public Function GetCenters() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_COMMON_LIST.GET_TR_CENTER", New List(Of Object)({0, "vi-VN"}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
#End Region

#Region "Lecture - Teacher"
    Public Function GetLecture(ByVal sCenters As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.GET_TR_LECTURE_BY_CENTERS", New List(Of Object)({sCenters}))
        If ds IsNot Nothing Then
            If ds.Tables(0) IsNot Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function
    Public Function GetLaguageTeach() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.GET_LAGUAGE_TEACH", New List(Of Object)({}))
        If ds IsNot Nothing Then
            If ds.Tables(0) IsNot Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function
#End Region

#Region "Employee in Plan"
    Public Function CheckEmployeeByEmpCode(ByVal sEmpCode As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.CHECK_EMPLOYEE_BY_EMPCODE", New List(Of Object)({sEmpCode}))
        If ds IsNot Nothing Then
            If ds.Tables(0) IsNot Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function
#End Region

#Region "Class Schedule"
    Public Function GetSchedule(ByVal dID As Decimal, ByVal dFrom As Date, ByVal dTo As Date) As DataSet
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.GET_SCHEDULE", New List(Of Object)({dID, dFrom, dTo}))
        Return ds
    End Function
#End Region

#Region "plan"

    Public Function GET_PROGRAM_GROUP(Optional ByVal _IS_BLANK As Boolean = False) As DataTable
        Try
            Dim dt As New DataTable
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING_BUSINESS.GET_PROGRAM_GROUP", _
                                                     New List(Of Object)(New Object() {_IS_BLANK}))
            If ds IsNot Nothing Then
                If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                    dt = ds.Tables(0)
                End If
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GET_COURSE_BY_PROGRAM_GROUP(Optional ByVal _IS_BLANK As Boolean = False, Optional ByVal _PROGRAM_GROUP As Decimal = 0) As DataTable
        Try
            Dim dt As New DataTable
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING_BUSINESS.GET_COURSE_BY_PROGRAM_GROUP", _
                                                     New List(Of Object)(New Object() {_IS_BLANK, _PROGRAM_GROUP}))
            If ds IsNot Nothing Then
                If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                    dt = ds.Tables(0)
                End If
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

#End Region
End Class