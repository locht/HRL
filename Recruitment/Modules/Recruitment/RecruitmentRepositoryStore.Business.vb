Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities


Partial Class RecruitmentRepository
    'Private rep As New HistaffFrameworkRepository

#Region "Chuyển sang hồ sơ nhân viên"

    'Lấy danh sách trang thái ứng viên
    Public Function GET_STATUS_CANDIDATE() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_NEW.GET_STATUS_CANDIDATE", New List(Of Object)(New Object() {}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)

        End If
        Return dt
    End Function   

	
    'Lấy danh sách nhân viên xác nhận trúng tuyển
    Public Function GET_LIST_EMPLOYEE_ELECT(ByVal P_ID As Int32, ByVal P_STATUS As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_NEW.GET_LIST_EMPLOYEE_ELECT", New List(Of Object)(New Object() {P_ID, P_STATUS}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Lấy danh sách đợt tuyển dụng theo: đơn vị & year
    Public Function GET_LIST_EMPLOYEE_EXAMS(ByVal P_ID As Int32, ByVal P_EMPLOYEE_CODE As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_NEW.GET_LIST_EMPLOYEE_EXAMS", New List(Of Object)(New Object() {P_ID, P_EMPLOYEE_CODE}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Lấy danh sách nguyện vọng của nhân viên
    Public Function GET_LIST_EMPLOYEE_ASPIRATION(ByVal P_ID As Int32, ByVal P_STATUS As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_NEW.GET_LIST_EMP_ASPIRATION", New List(Of Object)(New Object() {P_ID, P_STATUS}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Cập nhật nguyện vọng
    Public Function UPDATE_ASPIRATION(ByVal P_CADIDATE_ID As Integer, ByVal P_PLACE_WORK As String, ByVal P_RECEIVE_FROM As Date?, ByVal P_RECEIVE_TO As Date?, _
                                      ByVal P_PROBATION_FROM As Date?, ByVal P_PROBATION_TO As Date?, ByVal P_TIME_WORK As Decimal?, ByVal P_STARTDATE_WORK As Date?, ByVal P_PROBATION_SALARY As Decimal?, _
                                      ByVal P_OFFICAL_SALARY As Decimal?, ByVal P_OTHER_SUGGESTIONS As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT_NEW.UPDATE_ASPIRATION", New List(Of Object)(New Object() {P_CADIDATE_ID, _
                                      P_PLACE_WORK, P_RECEIVE_FROM, P_RECEIVE_TO, P_PROBATION_FROM, P_PROBATION_TO, P_TIME_WORK, P_STARTDATE_WORK, P_PROBATION_SALARY, P_OFFICAL_SALARY, P_OTHER_SUGGESTIONS, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Cập nhật trạng thái ứng viên
    Public Function UPDATE_CANDIDATE_STATUS(ByVal P_CADIDATE_LST As String, ByVal P_STATUS_CODE As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT_NEW.UPDATE_CANDIDATE_STATUS", New List(Of Object)(New Object() {P_CADIDATE_LST, P_STATUS_CODE, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Đếm số lượng ứng viên đã thành nhân viên
    Public Function COUNT_NUMBER_RC(ByVal P_PROGRAMID As Integer) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT_NEW.COUNT_NUMBER_RC", New List(Of Object)(New Object() {P_PROGRAMID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Thư mời nhận việc
    Public Function LETTER_RECIEVE(ByVal P_ID As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_NEW.LETTER_RECIEVE", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Xuất tờ trình ký HĐLĐ thử việc
    Public Function CONTRACT_RECIEVE(ByVal P_ID As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_NEW.CONTRACT_RECIEVE", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    ' Gửi mail tiếp nhân nhận LĐ
    Public Function EMAIL_RECIEVE(ByVal P_ID As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_NEW.EMAIL_CT_RECIEVE", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Đẩy dữ liệu từ table ứng viên sang nhân viên
    Public Function INSERT_CADIDATE_EMPLOYEE(ByVal P_CADIDATE_LST As String, ByVal P_USER As String, ByVal P_LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT_NEW.INSERT_CADIDATE_EMPLOYEE", New List(Of Object)(New Object() {P_CADIDATE_LST, P_USER, P_LOG, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function INSERT_EMPLOYEE_CADIDATE(ByVal empID As Decimal,
                                            ByVal orgID As Decimal,
                                            ByVal titleID As Decimal,
                                            ByVal programID As Decimal, ByVal P_USER As String, ByVal P_LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT_NEW.INSERT_EMPLOYEE_CADIDATE", New List(Of Object)(New Object() {empID, orgID, titleID, programID, P_USER, P_LOG, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
#End Region


End Class
